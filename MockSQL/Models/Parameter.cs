using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockSQL.Models
{
    public class Parameter
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public string Mode { get; set; }

        public override string ToString() => $"{Mode} {Name}({DataType})";
     
    }
}
