using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BugTracking.Models
{
    public class AspNetRole : IBusinessObject
    {
      
        public string Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

      
        public Nullable<bool> Active { get; set; }

        [DataType(DataType.DateTime)]
       
        public System.DateTime LastModified { get; set; }

        public List<AspNetUserRole> AspNetUserRoles { get; set; }

    }
}
