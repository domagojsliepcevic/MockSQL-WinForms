﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockSQL.Models
{
    public class DBEntity
    {
        public string Name { get; set; }

        public string Schema { get; set; }

        public Database Database { get; set; }

        public override string ToString() => $"{Schema}.{Name}";
       
    }
}
