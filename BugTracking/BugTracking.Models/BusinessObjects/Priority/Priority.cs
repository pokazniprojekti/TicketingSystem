using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BugTracking.Models
{
    public class Priority : IBusinessObject
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
        public Nullable<int> TimeIncrementInMinutes { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
