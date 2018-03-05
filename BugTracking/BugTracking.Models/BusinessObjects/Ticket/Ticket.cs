using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BugTracking.Models
{
    public class Ticket : IBusinessObject
    {

        public long ID { get; set; }
    
        public string UserID { get; set; }
     
        public short CategoryID { get; set; }
     
        public byte PriorityID { get; set; }
     
        public byte StatusID { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> Deadline { get; set; }
       
        public string Subject { get; set; }
        public bool Active { get; set; }
        public Nullable<System.DateTime> LastModified { get; set; }
        public Nullable<long> AssigneeID { get; set; }
     
        public long ProductID { get; set; }
      
        public string Body { get; set; }
        public long OrganizationID { get; set; }

        public  Category Category { get; set; }
        public  Organization Organization { get; set; }
        public  Priority Priority { get; set; }
        public  Product Product { get; set; }
        public  Status Status { get; set; }
        public  User User { get; set; }

    }
}
