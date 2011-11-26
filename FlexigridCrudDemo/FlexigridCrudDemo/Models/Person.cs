using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FlexigridCrudDemo.Models
{
    public class Person : IValidatableObject
    {
        public virtual Guid PersonId { get; set; }
        public virtual string Username { get; set; }

        [Required]
        public virtual string Firstname { get; set; }
        public virtual string Lastname { get; set; }
        public virtual int FavoriteNumber { get; set; }
        public virtual byte[] RowVersion { get; set; }

        
        public virtual Country Country { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Lastname == "blah" && Firstname == "meh")
            {
                yield return new ValidationResult("Combined Lastname and Firstname cannot be blah and meh");                
            }
            
        }
    }
}