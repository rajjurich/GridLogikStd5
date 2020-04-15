using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Extension
{
    public class opcservertag : opc_server_tag
    {
        public string Metername { get; set; }
        public bool is_tag { get; set; }
        public int _priority { get; set; }
    }
}
