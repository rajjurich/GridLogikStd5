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
    
    public partial class consumptionexpmonthwise
    {
        public long meterid { get; set; }
        public Nullable<System.DateTime> tstamp { get; set; }
        public Nullable<double> kwh_exp { get; set; }
        public Nullable<double> kvah_exp { get; set; }
        public Nullable<double> kvarh_lag_exp { get; set; }
        public Nullable<double> kvarh_lead_exp { get; set; }
        public Nullable<double> cum_kwh { get; set; }
        public Nullable<double> cum_kvah { get; set; }
        public Nullable<double> cum_kvarh_lag { get; set; }
        public Nullable<double> cum_kvarh_lead { get; set; }
    }
}
