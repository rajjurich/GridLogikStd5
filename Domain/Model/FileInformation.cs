﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class FileInformation
    {
        public string FullFileName { get; set; }
        public string FileName { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
