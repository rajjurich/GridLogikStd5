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
    
    public partial class ipwiselogger
    {
        public long id { get; set; }
        public Nullable<long> meterid { get; set; }
        public string ipaddress { get; set; }
        public string comport { get; set; }
        public string portnumber { get; set; }
    
        public virtual meter meter { get; set; }
    }
}
