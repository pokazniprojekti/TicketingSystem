using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracking.Models
{
    public class AspNetUser : IBusinessObject
    {
        [Required]
        [StringLength(128)]
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; set; }

        [Required]
        public bool EmailConfirmed { get; set; }

        [Required]
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        public bool PhoneNumberConfirmed { get; set; }

        [Required]
        public bool TwoFactorEnabled { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }

        [Required]
        public bool LockoutEnabled { get; set; }

        [Required]
        public int AccessFailedCount { get; set; }

        public bool? Active { get; set; }

        [Required]
        [StringLength(256)]
        public string UserName { get; set; }
 
        public List<AspNetUserRole> AspNetUserRoles { get; set; }
        public List<User> Users { get; set; }
    }
}
