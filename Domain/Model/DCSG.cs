using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class DCSG : dcsg
    {
        public Nullable<double> blockWiseAvgHz { get; set; }
    }
}
