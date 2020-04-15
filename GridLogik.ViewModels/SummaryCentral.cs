using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class SummaryCentral
    {
        public string meterid { get; set; }
        public string meteridName { get; set; }
        //public string unitstatus { get; set; }
        public double? genmw { get; set; }
        public double? grossgenmw { get; set; }
        public double? aux { get; set; }
        public double? dcmw { get; set; }
        public decimal? sgmw { get; set; }
        //public double? loadingscheduleED { get; set; }
        //public double? loadingscheduleUC { get; set; }
        //public double? avgfrequency { get; set; }
        //public double? netexport { get; set; }
        public double? uideviation { get; set; }
        //public double? uicharge { get; set; }
        //public double? feulcost { get; set; }
        //public double? plf { get; set; }
        //public double? apc { get; set; }
        //public double? unitheatindirect { get; set; }
        //public double? unitheatdirect { get; set; }
        //public double? coalconsumption { get; set; }
        //public double? unitefficiency { get; set; }
        //public double? generationcost { get; set; }
        public List<lineMeter> linemeterslist { get; set; }
    }

    public class lineMeter
    {
        public long meterid { get; set; }
        public double? genmw { get; set; }
        public string meteridName { get; set; }
        public string genID { get; set; }
    }
}
