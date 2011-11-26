using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexigridCrudDemo.Models;
using FluentNHibernate.Automapping;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using NHibernate;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;

namespace FlexigridCrudDemo.Mappers
{
    public static class ModelsMapper
    {
        static ISessionFactory _sf = null;
        public static ISessionFactory GetSessionFactory()
        {
            if (_sf != null) return _sf;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EfDbContext"].ConnectionString;



            var fc = Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2008.ConnectionString(connectionString))
                    .Mappings
                    (m =>


                            m.AutoMappings.Add
                            (
                                AutoMap.AssemblyOf<Person>(new CustomConfiguration())

                                // .Conventions.Add(ForeignKey.EndsWith("Id"))
                               .Conventions.Add<CustomForeignKeyConvention>()

                               .Conventions.Add<HasManyConvention>()
                               .Conventions.Add<RowversionConvention>()
                               
                            )


           );



            _sf = fc.BuildSessionFactory();
            return _sf;
        }


        class CustomConfiguration : DefaultAutomappingConfiguration
        {
            IList<Type> _objectsToMap = new List<Type>()
            {
                // whitelisted objects to map
                typeof(Person), typeof(Country)
            };
            public override bool ShouldMap(Type type) { return _objectsToMap.Any(x => x == type); }
            public override bool IsId(FluentNHibernate.Member member) { return member.Name == member.DeclaringType.Name + "Id"; }

            public override bool IsVersion(FluentNHibernate.Member member) { return member.Name == "RowVersion"; }
        }




        public class CustomForeignKeyConvention : ForeignKeyConvention
        {
            protected override string GetKeyName(FluentNHibernate.Member property, Type type)
            {
                if (property == null)
                    return type.Name + "Id";


                // make foreign key compatible with Entity Framework
                return type.Name + "_" + property.Name + "Id";
            }
        }


        class HasManyConvention : IHasManyConvention
        {

            public void Apply(IOneToManyCollectionInstance instance)
            {
                instance.Inverse();
                instance.Cascade.AllDeleteOrphan();
            }


        }

        class RowversionConvention : IVersionConvention
        {
            public void Apply(IVersionInstance instance) { instance.Generated.Always(); }
        }

    }//ModelsMapper
}