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
    
    public partial class reportscheduledetail
    {
        public long srid { get; set; }
        public string reportname { get; set; }
        public string frqtype { get; set; }
        public Nullable<System.DateTime> startdate { get; set; }
        public string triggertime { get; set; }
        public string weekdayname { get; set; }
        public string monthname { get; set; }
        public Nullable<long> dayofmonth { get; set; }
        public Nullable<short> isactive { get; set; }
        public Nullable<short> rptflag { get; set; }
    }
}
