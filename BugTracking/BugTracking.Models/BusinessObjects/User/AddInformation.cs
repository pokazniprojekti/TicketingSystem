using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracking.Models
{
    public class AddInformation : IBusinessObject
    {
      
        public long ID { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

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

       
        public bool Active { get; set; }

        [DataType(DataType.DateTime)]
      
        public Nullable<System.DateTime> LastModified { get; set; }

        [Required]
        public long OrganizationID { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("([0-9]+)", ErrorMessage = ("For Telephone enter only numbers"))]
        public string TelephoneNumber { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }
    }
}
