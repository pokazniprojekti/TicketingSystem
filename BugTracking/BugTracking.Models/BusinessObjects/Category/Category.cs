using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BugTracking.Models
{
    public class Category : IBusinessObject
    {
       
        public short ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

     
        public bool Active { get; set; }
        
        [DataType(DataType.DateTime)]
      
        public Nullable<System.DateTime> LastModified { get; set; }

        [Required]
        public string Section { get; set; }
        public List<Ticket> Tickets { get; set; }
    

    }
}
