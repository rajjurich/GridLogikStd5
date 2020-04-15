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
    
    public partial class meter
    {
        public meter()
        {
            this.alarms = new HashSet<alarm>();
            this.communicationdetaillinks = new HashSet<communicationdetaillink>();
            this.groupconfigurations = new HashSet<groupconfiguration>();
            this.ipwiseloggers = new HashSet<ipwiselogger>();
            this.parametermfs = new HashSet<parametermf>();
            this.txnloadblocks = new HashSet<txnloadblock>();
            this.standardalarms = new HashSet<standardalarm>();
            this.mstmetergroupdetails = new HashSet<mstmetergroupdetail>();
        }
    
        public long id { get; set; }
        public string metername { get; set; }
        public long metertypeid { get; set; }
        public long modelid { get; set; }
        public Nullable<int> ag { get; set; }
        public Nullable<int> frequency { get; set; }
        public string phase { get; set; }
        public string serialno { get; set; }
        public string meterno { get; set; }
        public Nullable<short> isdeleted { get; set; }
        public Nullable<long> ctprimary { get; set; }
        public Nullable<long> ctsecondary { get; set; }
        public Nullable<short> blockwisedata { get; set; }
        public Nullable<short> isactive { get; set; }
        public string location { get; set; }
        public string replacementremark { get; set; }
        public Nullable<long> replacedby { get; set; }
        public Nullable<System.DateTime> replacementdate { get; set; }
        public Nullable<long> rolloverlimit { get; set; }
        public Nullable<short> misemail { get; set; }
        public Nullable<short> mispopup { get; set; }
        public Nullable<short> missms { get; set; }
        public string parameter { get; set; }
        public Nullable<int> caltype { get; set; }
    
        public virtual ICollection<alarm> alarms { get; set; }
        public virtual ICollection<communicationdetaillink> communicationdetaillinks { get; set; }
        public virtual ICollection<groupconfiguration> groupconfigurations { get; set; }
        public virtual ICollection<ipwiselogger> ipwiseloggers { get; set; }
        public virtual metermodel metermodel { get; set; }
        public virtual metertype metertype { get; set; }
        public virtual ICollection<parametermf> parametermfs { get; set; }
        public virtual ICollection<txnloadblock> txnloadblocks { get; set; }
        public virtual ICollection<standardalarm> standardalarms { get; set; }
        public virtual ICollection<mstmetergroupdetail> mstmetergroupdetails { get; set; }
    }
}