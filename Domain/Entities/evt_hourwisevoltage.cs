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
    
    public partial class evt_hourwisevoltage
    {
        public long meterid { get; set; }
        public Nullable<System.DateTime> ts { get; set; }
        public Nullable<double> avg_vrn { get; set; }
        public Nullable<double> avg_vbn { get; set; }
        public Nullable<double> avg_vyn { get; set; }
        public Nullable<double> avg_vry { get; set; }
        public Nullable<double> avg_vyb { get; set; }
        public Nullable<double> avg_vbr { get; set; }
        public Nullable<double> avg_vln { get; set; }
    }
}
