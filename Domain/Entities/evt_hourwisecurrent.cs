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
    
    public partial class evt_hourwisecurrent
    {
        public long meterid { get; set; }
        public Nullable<System.DateTime> ts { get; set; }
        public Nullable<double> avg_ir { get; set; }
        public Nullable<double> avg_iy { get; set; }
        public Nullable<double> avg_ib { get; set; }
        public Nullable<double> avg_i { get; set; }
    }
}
