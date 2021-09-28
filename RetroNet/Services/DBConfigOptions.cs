using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _90sTest.Services
{
    public class DBConfigOptions
    {
        public string RdsDbName { get; set; }
        public string RdsUsername { get; set; }
        public string RdsPassword { get; set; }
        public string RdsHostname { get; set; }
        public string RdsPort { get; set; }
    }
}
