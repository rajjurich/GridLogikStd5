using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class RABTMeter
    {
        public long ID { get; set; }
    }

    public class RTABTMeterData
    {
        //current block
        public string cur_blockno { get; set; }
        public string cur_blocktime { get; set; }
        public decimal cur_dcmw { get; set; }
        public decimal cur_sgmw { get; set; }
        public decimal cur_netexbusmw { get; set; }
        public decimal cur_avghz { get; set; }
        public decimal cur_expdcperc { get; set; }
        public decimal cur_expscperc { get; set; }
        public decimal cur_backingdown { get; set; }
        public decimal cur_devmw { get; set; }
        public decimal cur_devrate { get; set; }
        public decimal cur_fuelrate { get; set; }
        public decimal cur_devrs { get; set; }
        public decimal cur_addidevrs { get; set; }
        public decimal cur_totaldevrs { get; set; }
        public decimal cur_fuelcost { get; set; }
        public decimal cur_netgainloss { get; set; }

        //previous block
        public string prev_blockno { get; set; }
        public string prev_blocktime { get; set; }
        public decimal prev_dcmw { get; set; }
        public decimal prev_sgmw { get; set; }
        public decimal prev_netexbusmw { get; set; }
        public decimal prev_avghz { get; set; }
        public decimal prev_expdcperc { get; set; }
        public decimal prev_expscperc { get; set; }
        public decimal prev_backingdown { get; set; }
        public decimal prev_devmw { get; set; }
        public decimal prev_devrate { get; set; }
        public decimal prev_fuelrate { get; set; }
        public decimal prev_devrs { get; set; }
        public decimal prev_addidevrs { get; set; }
        public decimal prev_totaldevrs { get; set; }
        public decimal prev_fuelcost { get; set; }
        public decimal prev_netgainloss { get; set; }

        //Decision Aspect Current Blk MW
        public decimal dacb_for100SG { get; set; }
        public decimal dacb_lopoverinj { get; set; }
        public decimal dacb_lopunderinj { get; set; }
        public decimal dacb_askrateoverinj { get; set; }
        public decimal dacb_askrateunderinj { get; set; }
        public decimal dacb_overinjoppblk { get; set; }
        public decimal dacb_underinjoppblk { get; set; }

        //decision aspect cumulative
        public decimal dacum_expdcperc { get; set; }
        public int dacum_posblk { get; set; }
        public int dacum_negblk { get; set; }
        public decimal dacum_bekfreq { get; set; }

        //last block data
        public string culb_dtdate { get; set; }
        public string culb_dttime { get; set; }
        public decimal culb_dcvalue { get; set; }
        public decimal culb_sgvalue { get; set; }
        public decimal culb_backingdown { get; set; }
        public decimal culb_export { get; set; }
        public decimal culb_import { get; set; }
        public decimal culb_netexp { get; set; }
        public decimal culb_underinjectionmwh { get; set; }
        public decimal culb_overinjectionmwh { get; set; }
        public decimal culb_underinjectionlacs { get; set; }
        public decimal culb_overinjectionlacs { get; set; }
        public decimal culb_devmwh { get; set; }
        public decimal culb_devmwhlacs { get; set; }
        public decimal culb_addidev { get; set; }
        public decimal culb_totaldev { get; set; }
        public decimal culb_fuelcost { get; set; }
        public decimal culb_netgainloss { get; set; }
        public decimal culb_uipayablemonth { get; set; }
        public decimal culb_uireceivablemonth { get; set; }
        public decimal culb_netuimonth { get; set; }
        public decimal culb_netgainlossmonth { get; set; }
    }


    public class AllMeterData
    {
        public decimal busfreq { get; set; }
        public decimal busvoltage { get; set; }
        public DateTime date { get; set; }
        public string time { get; set; }
        public string metername { get; set; }
        public decimal mw { get; set; }
        public decimal mvar { get; set; }
    }


    public class InstanceMeterData
    {
        public decimal hz { get; set; }
        public decimal vll { get; set; }
        public decimal cblk_avg_hz { get; set; }

        public decimal kw { get; set; }

        public decimal kwh_exp { get; set; }
        public decimal cblk_avg_mw { get; set; }
        public decimal kvar { get; set; }
        public decimal kvab { get; set; }
        public long meterid { get; set; }
        public string meter_name { get; set; }

        public DateTime? tstamp { get; set; }
        public DateTime? CurrentTime { get; set; }
    }


    public class CurAndPrevBlkData
    {
        public string blockno { get; set; }
        public string blocktime { get; set; }
        public decimal dcmw { get; set; }
        public decimal hz { get; set; }
        public decimal mw { get; set; }
        public decimal sgmw { get; set; }
        public decimal netexbusmw { get; set; }
        public decimal avghz { get; set; }
        public decimal expdcperc { get; set; }
        public decimal expscperc { get; set; }
        public decimal backingdown { get; set; }
        public decimal devmw { get; set; }
        public decimal devrate { get; set; }
        public decimal fuelrate { get; set; }
        public decimal devrs { get; set; }
        public decimal addidevrs { get; set; }
        public decimal totaldevrs { get; set; }
        public decimal fuelcost { get; set; }
        public decimal netgainloss { get; set; }

        public List<NextFourBlockData> nextfourblocks { get; set; }

        public decimal for100sg { get; set; }
        public decimal limopoverinj { get; set; }
        public decimal limopunderinj { get; set; }
        public decimal askrateoverinj { get; set; }
        public decimal askrateunderinj { get; set; }
        public decimal overinjoppblk { get; set; }
        public decimal underinjoppblk { get; set; }

        public decimal cumexpdcperc { get; set; }
        public int posblk { get; set; }
        public int negblk { get; set; }
    }

    public class RABTLastTwoBlockData
    {
        public List<RABTCurAndPrevBlkData> lasttwoblockdata { get; set; }
        public decimal for100sg { get; set; }
        public decimal limopoverinj { get; set; }
        public decimal limopunderinj { get; set; }
        public decimal askrateoverinj { get; set; }
        public decimal askrateunderinj { get; set; }
        public decimal overinjoppblk { get; set; }
        public decimal underinjoppblk { get; set; }
        public decimal cumexpdcperc { get; set; }
        public int posblk { get; set; }
        public int negblk { get; set; }

    }
    public class RABTCurAndPrevBlkData
    {
        public string blockno { get; set; }
        public string blocktime { get; set; }
        public decimal dcmw { get; set; }
        public decimal sgmw { get; set; }
        public decimal netexbusmw { get; set; }
        public decimal avghz { get; set; }
        public decimal hz { get; set; }
        public decimal mw { get; set; }
        public decimal expdcperc { get; set; }
        public decimal expscperc { get; set; }
        public decimal backingdown { get; set; }
        public decimal devmw { get; set; }
        public decimal devrate { get; set; }
        public decimal fuelrate { get; set; }
        public decimal devrs { get; set; }
        public decimal addidevrs { get; set; }
        public decimal totaldevrs { get; set; }
        public decimal fuelcost { get; set; }
        public decimal netgainloss { get; set; }

        public decimal? breakFrq { get; set; }
        public List<long> NewMeterIds { get; set; }
        public DateTime? CurrentTime { get; set; }


        //public List<NextFourBlockData> nextfourblocks { get; set; }


    }

    public class RABTNextFourBlock
    {
        public int blockNo { get; set; }
        public double dcvalue { get; set; }
        public double sgvalue { get; set; }
        public double ftfhz { get; set; }
    }

    public class CumLastBlkData
    {
        public string dtdate { get; set; }
        public string dttime { get; set; }
        public decimal dcvalue { get; set; }
        public decimal sgvalue { get; set; }
        public decimal backingdown { get; set; }
        public decimal export { get; set; }
        public decimal import { get; set; }
        public decimal netexp { get; set; }
        public decimal underinjectionmwh { get; set; }
        public decimal overinjectionmwh { get; set; }
        public decimal underinjectionlacs { get; set; }
        public decimal overinjectionlacs { get; set; }
        public decimal devmwh { get; set; }
        public decimal devmwhlacs { get; set; }
        public decimal addidev { get; set; }
        public decimal totaldev { get; set; }
        public decimal fuelcost { get; set; }
        public decimal netgainloss { get; set; }
        public decimal uipayablemonth { get; set; }
        public decimal uireceivablemonth { get; set; }
        public decimal netuimonth { get; set; }
        public decimal netgainlossmonth { get; set; }

        public List<decimal> ListDevMWData { get; set; }

        public decimal PosCount { get; set; }
        public decimal NegCount { get; set; }

        public List<decimal> ListAvgmw { get; set; }

        public List<decimal> ListSG { get; set; }

        public List<decimal> ListBlock { get; set; }
        public List<decimal> ListUIRate { get; set; }


    }

    public class NextFourBlockData
    {
        public int blockno { get; set; }
        public decimal dcvalue { get; set; }
        public decimal sgvalue { get; set; }
        public decimal ftfhz { get; set; }
    }

    public class DecisionAspectCum
    {
        public decimal curblkfuelcostdcsg { get; set; }
        public decimal expdcperc { get; set; }
        public int posblk { get; set; }
        public int negblk { get; set; }

        public decimal bekfreq { get; set; }
        public decimal avg_hz { get; set; }

        public List<decimal> avg { get; set; }
    }

    public class loadsurveylogsavg
    {
        public string blockno { get; set; }
        public Nullable<double> avg_hz { get; set; }
    }
}
