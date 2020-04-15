using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridLogik.ViewModels
{
    public class MstRoleMenuAccess
    {        
            public long rmarecid { get; set; }
            public string rmaroleid { get; set; }
            public string rmamnuid { get; set; }
            public Nullable<short> rmacreateaccess { get; set; }
            public Nullable<short> rmareadaccess { get; set; }
            public Nullable<short> rmaupdateaccess { get; set; }
            public Nullable<short> rmadeleteaccess { get; set; }
            public Nullable<short> rmaisdeleted { get; set; }
        
    }
}
