using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GridLogikViewer.Areas.ABTScreen.Models
{
    public class RealTimeABTDisplayViewModel
    {
        [Display(Name = "From Date")]
        [Required(ErrorMessage = "From date is required")]
        public string FromDate { get; set; }

        public string BlockNo { get; set; }
        public string Date { get; set; }
        public long? TimeStamp { get; set; }
        public string BlockTime { get; set; }
        public decimal? DCValue { get; set; }
        public decimal? SGValue { get; set; }
        public double? NetExp { get; set; }
        public decimal? UIMW { get; set; }
        public decimal? FuelRate { get; set; }
        public double? AvgHz { get; set; }
        public double? UIRate { get; set; }
        public double? UIRs { get; set; }
        public double? FuelCharge { get; set; }
        public decimal? ProfitLoss { get; set; }
        public decimal? TotalDevRs { get; set; }

    }
}