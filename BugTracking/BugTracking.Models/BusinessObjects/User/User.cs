using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BugTracking.Models
{
    public class User : IBusinessObject
    {
        [Required]
        public long ID { get; set; }

        [DisplayName("First Name")]
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(256)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        public bool Active { get; set; }

        [DisplayName("Last Modified")]
        [DataType(DataType.DateTime)]
        [Required]
        public Nullable<System.DateTime> LastModified { get; set; }

        [Required]
        public long OrganizationID { get; set; }
        
      
        [DisplayName("Telephone Number")]
        [Required]
        [StringLength(50)]
        [RegularExpression("([0-9]+)", ErrorMessage = ("For Telephone enter only numbers"))]
        public string TelephoneNumber { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        [DisplayName("Responsible Person")]
        public string FullName
        {
            get { return string.Format("{0}, {1}", this.FirstName, this.LastName); }
        }

        public AspNetUser AspNetUser { get; set; }
        public Organization Organization { get; set; }
        public List<Responsible> Responsibles { get; set; }
        public List<Ticket> Tickets { get; set; }

    }
}
