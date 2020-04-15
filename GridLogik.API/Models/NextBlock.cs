using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GridLogik.API.Models
{
    public class NextBlock
    {
        public List<long> meters { get; set; }
        public List<long?> stages { get; set; }
        public int numberOfBlocks { get; set; }
        public int numberOfDays { get; set; }        
    }
}