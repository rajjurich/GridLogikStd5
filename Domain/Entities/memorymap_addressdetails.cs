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
    
    public partial class memorymap_addressdetails
    {
        public long id { get; set; }
        public long modelid { get; set; }
        public long rangeid { get; set; }
        public string address { get; set; }
        public string parametername { get; set; }
        public string datatype { get; set; }
        public string instancedatamapping { get; set; }
        public Nullable<double> multiplicationfactor { get; set; }
        public string bytecount { get; set; }
        public Nullable<short> flag { get; set; }
        public Nullable<short> isdeleted { get; set; }
        public string columntype { get; set; }
        public string formula { get; set; }
        public string tablename { get; set; }
        public string grouptype { get; set; }
        public string types { get; set; }
    
        public virtual metermodel metermodel { get; set; }
        public virtual memorymap_range memorymap_range { get; set; }
    }
}
