using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DBC.Model
{
    public class SqlFormat
    {
        public string? Sql { get; set; }
        public object[]? parameters {  get; set; }
    }
}
