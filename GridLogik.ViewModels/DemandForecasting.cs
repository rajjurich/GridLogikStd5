using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class DemandForecasting
    {
        //public List<InputDemandForecast> inputdemandforecasts { get; set; }
        //public List<DCSG> dcsgs { get; set; }
        //public List<DemandForecast> demandforecasts { get; set; }
    }

    //public class InputDemandForecast
    //{
    //    public long? blockno { get; set; }
    //    public double? kw { get; set; }
    //    public long? location { get; set; }
    //    public DateTime? tstamp { get; set; }        
    //}

    //public class DCSG
    //{
    //    public long? blockno { get; set; }
    //    public decimal? sgvalue { get; set; }
    //    public long? stageid { get; set; }
    //    public DateTime? tstamp { get; set; }        
    //}

    //public class DemandForecast
    //{
    //    public long? blockno { get; set; }
    //    public double? forecastedvalue { get; set; }
    //    public string location { get; set; }
    //    public DateTime? tstamp { get; set; }        
    //}

    public class DemandForecast
    {
        public long? blockno { get; set; }
        public DateTime? tstamp { get; set; }
        public string rundate { get; set; }
        public decimal? sgvalue { get; set; }
        public double? kw { get; set; }
        public double? forecastedvalue { get; set; }
    }
}
