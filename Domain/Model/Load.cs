using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Load
    {
        public string Day { get; set; }
        public double vrn { get; set; }
        public double vyn { get; set; }
        public double vbn { get; set; }
        public double ExBus { get; set; }

        public double SG { get; set; }

        public double AvgHz { get; set; }
        public double ir { get; set; }
        public double iy { get; set; }
        public double ib { get; set; }
        public double kwh { get; set; }
        public double kw { get; set; }
        public double kVARh { get; set; }
        public double pf { get; set; }
        public double kVA { get; set; }
        public DateTime? tsvrn { get; set; }
        public DateTime? tsvyn { get; set; }
        public DateTime? tsvbn { get; set; }

        public DateTime? tsir { get; set; }
        public DateTime? tsiy { get; set; }
        public DateTime? tsib { get; set; }
        public DateTime? tskwh { get; set; }
        public DateTime? tskw { get; set; }
        public DateTime? tskVARh { get; set; }
        public DateTime? tspf { get; set; }

        public DateTime? tskVA { get; set; }
        //Name change of report , add parameters kw,kwh,kvarh,pf

    }

    public class Load_base
    {
        public DateTime? date { get; set; }
        public double? vrn { get; set; }

        public double? vyn { get; set; }
        public double? vbn { get; set; }
        public double? ir { get; set; }
        public double? iy { get; set; }
        public double? ib { get; set; }
        public double? kwh { get; set; }
        public double? kw { get; set; }
        public double? kVARh { get; set; }
        public double? kVA { get; set; }
        public double? pf { get; set; }

        public DateTime? tsvrn { get; set; }
        public DateTime? tsvyn { get; set; }
        public DateTime? tsvbn { get; set; }

        public DateTime? tsir { get; set; }
        public DateTime? tsiy { get; set; }
        public DateTime? tsib { get; set; }
        public DateTime? tskwh { get; set; }
        public DateTime? tskw { get; set; }
        public DateTime? tskVARh { get; set; }

        public DateTime? tskVA { get; set; }
        public DateTime? tspf { get; set; }


    }

    public class LoadProfile_base
    {
        public DateTime? date { get; set; }
        public double? vrn { get; set; }
        public double? vbn { get; set; }
        public double? vyn { get; set; }
        public double? ir { get; set; }
        public double? iy { get; set; }
        public double? ib { get; set; }
        public double? kwh_imp { get; set; }
        public double? kwh_exp { get; set; }


    }
}
