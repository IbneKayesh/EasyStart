﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Infra
{
    public class EQResult
    {
        public EQResult()
        {
            success = false;
            rows = 0;
            messages = "Messages";
            entities = "Entities";
        }
        public bool success { get; set; }
        public int rows { get; set; }
        public string messages { get; set; }
        public string entities { get; set; }
    }
}
