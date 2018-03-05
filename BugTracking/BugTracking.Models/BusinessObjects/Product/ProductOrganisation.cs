using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracking.Models
{
    public class ProductOrganisation : IBusinessObject
    {

     
        public long ID { get; set; }

        [Required]
        public long ID_Product { get; set; }

        [Required]
        public long ID_Organisation { get; set; }

      
        public bool? Active { get; set; }

        [DataType(DataType.DateTime)]
     
        public DateTime? LastModified { get; set; }
        public Organization Organization { get; set; }
        public Product Product { get; set; }

    }
}
