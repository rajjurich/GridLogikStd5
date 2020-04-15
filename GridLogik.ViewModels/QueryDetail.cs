using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class QueryDetail
    {
        public long Id { get; set; }
        public string QueryName { get; set; }
        public string Type { get; set; }
        public string QueryStatement { get; set; }
        public string ParameterFlag { get; set; }

    }
}
