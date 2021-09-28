using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace _90sTest.Services
{
    public class RDSHelper
    {
        public static string GetRDSConnectionString(DBConfigOptions dBConfig)
        {
            //var appConfig = ConfigurationManager.AppSettings;

            string dbname = dBConfig.RdsDbName; //["RDS_DB_NAME"];

            if (string.IsNullOrEmpty(dbname)) return null;

            string username = dBConfig.RdsUsername;//appConfig["RDS_USERNAME"];
            string password = dBConfig.RdsPassword;//appConfig["RDS_PASSWORD"];
            string hostname = dBConfig.RdsHostname;//appConfig["RDS_HOSTNAME"];
            string port = dBConfig.RdsPort;//appConfig["RDS_PORT"];

            return "Data Source=" + hostname + ";Initial Catalog=" + dbname + ";User ID=" + username + ";Password=" + password + ";";
        }

        public static string GetRDSConnectionString(IConfiguration config)
        {
            string dbname = config.GetValue<string>("RdsDbName");
            string username = config.GetValue<string>("RdsUsername");
            string password = config.GetValue<string>("RdsPassword");
            string hostname = config.GetValue<string>("RdsHostname");
            string port = config.GetValue<string>("RdsPort");
            return "Data Source=" + hostname + ";Initial Catalog=" + dbname + ";User ID=" + username + ";Password=" + password + ";";
        }
    }
}
