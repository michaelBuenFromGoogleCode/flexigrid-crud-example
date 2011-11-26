using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using FlexigridCrudDemo.Models;

namespace FlexigridCrudDemo.Mappers
{
    public class EfDbContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Person>().HasRequired(x => x.Country).WithMany(x => x.Persons).Map(x => x.MapKey("Country_CountryId"));

            modelBuilder.Entity<Person>().Property(x => x.RowVersion).IsRowVersion();

            modelBuilder.Entity<Country>().HasKey(x => x.CountryId);
        
        }
    }
}