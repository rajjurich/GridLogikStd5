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
    
    public partial class manualrptlist
    {
        public manualrptlist()
        {
            this.manualreports = new HashSet<manualreport>();
        }
    
        public long rlistid { get; set; }
        public string reptname { get; set; }
    
        public virtual ICollection<manualreport> manualreports { get; set; }
    }
}
