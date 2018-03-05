using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracking.Models
{
    public class Status : IBusinessObject
    {
      
        public byte ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

      
        public bool Active { get; set; }

        [DisplayName("Last Modified")]

        [DataType(DataType.DateTime)]
       
        public Nullable<System.DateTime> LastModified { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
