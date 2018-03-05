using System;
using System.ComponentModel.DataAnnotations;

namespace BugTracking.Models
{
    public class Responsible : IBusinessObject
    {
     
        public long ID { get; set; }

        [Required]
        public long ID_Product { get; set; }

        [Required]
        public long ID_User { get; set; }

      
        public bool? Active { get; set; }

        [DataType(DataType.DateTime)]
      
        public DateTime? LastModified { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }

    }
}
