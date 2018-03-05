using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracking.Models
{
    public class AspNetUserRole : IBusinessObject
    {
        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        [Required]
        [StringLength(128)]
        public string RoleId { get; set; }

     
        public bool Active { get; set; }

        [DataType(DataType.DateTime)]
       
        public Nullable<System.DateTime> LastModified { get; set; }
        public AspNetRole AspNetRole { get; set; }
        public AspNetUser AspNetUser { get; set; }
    }
}
