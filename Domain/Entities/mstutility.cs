//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class mstutility
    {
        public mstutility()
        {
            this.mstzones = new HashSet<mstzone>();
        }
    
        public long utilrecid { get; set; }
        public string utilid { get; set; }
        public string utilname { get; set; }
        public string utiladdress1 { get; set; }
        public string utiladdress2 { get; set; }
        public string utiladdress3 { get; set; }
        public string utilpin { get; set; }
        public Nullable<short> isdeleted { get; set; }
    
        public virtual ICollection<mstzone> mstzones { get; set; }
    }
}
