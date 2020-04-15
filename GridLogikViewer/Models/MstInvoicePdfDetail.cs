using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GridLogikViewer.Models
{
    public class MstInvoicePdfDetail
    {
        public string COMPANY_NAME { get; set; }
        public string MONTH { get; set; }
        public string ALLOTTE_NAME { get; set; }
        public string REF { get; set; }
        public string PRD { get; set; }
        public string PRRD { get; set; }
        public string UFFNO { get; set; }
        public string MSN { get; set; }
        public string UC { get; set; }

        public string METER_NUMBER { get; set; }
        public string METER { get; set; }
        public string DESCRIPTION { get; set; }
        public string PF { get; set; }
        public string ORKHW { get; set; }
        public string CRKHW { get; set; }
        public string CKHW { get; set; }
        public string UR { get; set; }
        public string AMOUNT { get; set; }
        public string PLF { get; set; }
        public string TC { get; set; }
        public string TA { get; set; }
        public string ARREAR { get; set; }
        public string TAR { get; set; }
    }

    public class MstInvoicePdfDetail_1
    {
        public string COMPANY_NAME { get; set; }
        public string MONTH { get; set; }
        public string ALLOTTE_NAME { get; set; }
        public string REF { get; set; }
        public string PRD { get; set; }
        public string PRRD { get; set; }
        public string UFFNO { get; set; }
        public string MSN { get; set; }
        public double? UC { get; set; }

        public string METER_NUMBER { get; set; }
        public string METER { get; set; }
        public string DESCRIPTION { get; set; }
        public double? PF { get; set; }
        public double? ORKHW { get; set; }
        public double? CRKHW { get; set; }
        public double? CKHW { get; set; }
        public double? UR { get; set; }
        public double? AMOUNT { get; set; }
        public string PLF { get; set; }
        public double? TC { get; set; }
        public double? TA { get; set; }
        public string ARREAR { get; set; }
        public double? TAR { get; set; }
    }
    public class InvoiceMeterData
    {
        public string CompanyCode { get; set; }
        public string MeterGroup { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }

    }
}