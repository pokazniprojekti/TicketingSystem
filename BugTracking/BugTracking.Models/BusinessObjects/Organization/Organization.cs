using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace BugTracking.Models
{

    public class Organization : IBusinessObject
    {
       
        public long ID { get; set; }

        [DisplayName("Organization Name")]

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Location { get; set; }

        [DisplayName("Telephone Number")]
        [StringLength(50)]
        [Required]
        [RegularExpression("([0-9]+)", ErrorMessage = ("For Telephone enter only numbers"))]
        public string TelephoneNumber { get; set; }

        [EmailAddress]
        [StringLength(50)]
        [Required]
        public string Email { get; set; }

      
        public bool Active { get; set; }

        [DisplayName("Last Modified")]
        [DataType(DataType.DateTime)]
      
        public Nullable<System.DateTime> LastModified { get; set; }

        public List<ProductOrganisation> ProductOrganisations { get; set; }
        public List<User> Users { get; set; }
    }
}
