using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexigridCrudDemo.Models
{
    public class Country
    {
        public virtual Guid CountryId { get; set; }

        public virtual string CountryCode { get; set; }
        public virtual string CountryName { get; set; }

        public virtual IList<Person> Persons { get; set; }
    }
}