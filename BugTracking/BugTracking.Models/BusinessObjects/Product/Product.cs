using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracking.Models
{
   public class Product : IBusinessObject
    {
       
        public long ID { get; set; }

        [DisplayName("Product")]
        [Required]
        [StringLength(100)]
        public string Name_Prod { get; set; }

        [DisplayName("Description")]
        [Required]
        [StringLength(300)]
        public string Description_Prod { get; set; }

      
        public bool? Active { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? LastModified { get; set; }
        public List<ProductOrganisation> ProductOrganisations { get; set; }
        public List<Responsible> Responsibles { get; set; }
    }
}
