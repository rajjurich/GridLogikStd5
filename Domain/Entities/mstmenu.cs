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
    
    public partial class mstmenu
    {
        public mstmenu()
        {
            this.mstrolemenuaccesses = new HashSet<mstrolemenuaccess>();
        }
    
        public long mnurecid { get; set; }
        public string mnuitemname { get; set; }
        public double mnuitemposition { get; set; }
        public string link { get; set; }
        public Nullable<short> mnuisdeleted { get; set; }
        public Nullable<long> mnutype { get; set; }
        public Nullable<long> mnumodulid { get; set; }
    
        public virtual mstmodule mstmodule { get; set; }
        public virtual ICollection<mstrolemenuaccess> mstrolemenuaccesses { get; set; }
    }
}
