//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.EntityModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductOrganisation
    {
        public long ID { get; set; }
        public long ID_Product { get; set; }
        public long ID_Organisation { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<System.DateTime> LastModified { get; set; }
    
        public virtual Organization Organization { get; set; }
        public virtual Product Product { get; set; }
    }
}
