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
    
    public partial class mstuser
    {
        public long usrrecid { get; set; }
        public string usrid { get; set; }
        public string usrpassword { get; set; }
        public Nullable<long> usrroleid { get; set; }
        public string usremployeeid { get; set; }
        public Nullable<short> usrisactive { get; set; }
        public Nullable<System.DateTime> usrcreationdate { get; set; }
        public string usrcreatedbyuserid { get; set; }
        public string usrfirstname { get; set; }
        public string usrmiddlename { get; set; }
        public string usrlastname { get; set; }
        public string usremailid { get; set; }
        public string usrphoneno1 { get; set; }
        public string usrphoneno2 { get; set; }
        public Nullable<short> usrisdeleted { get; set; }
        public string usrtype { get; set; }
    
        public virtual mstrole mstrole { get; set; }
    }
}