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
    
    public partial class hourwise
    {
        public long meterid { get; set; }
        public Nullable<System.DateTime> ts { get; set; }
        public Nullable<double> avg_vrn { get; set; }
        public Nullable<double> avg_vbn { get; set; }
        public Nullable<double> avg_vyn { get; set; }
        public Nullable<double> avg_ir { get; set; }
        public Nullable<double> avg_iy { get; set; }
        public Nullable<double> avg_ib { get; set; }
        public Nullable<double> avg_kw { get; set; }
        public Nullable<double> avg_kvar { get; set; }
        public Nullable<double> avg_kva { get; set; }
        public Nullable<double> avg_pf { get; set; }
        public Nullable<double> kwh_import { get; set; }
        public Nullable<double> kwh_import_current { get; set; }
        public Nullable<double> kvarh_lag_imp { get; set; }
        public Nullable<double> kvarh_lag_imp_current { get; set; }
        public Nullable<double> kwh_export { get; set; }
        public Nullable<double> kwh_export_current { get; set; }
    }
}
