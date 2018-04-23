using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyLover.Infrastructure
{
    public class RdsHelper
    {
        public static string GetRdsConnectionString(IConfiguration Configuration)
        {
            string hostname = Configuration["Data:RDS:hostname"]+","+ Configuration["Data:RDS:port"];
            string dbname = Configuration["Data:RDS:dbname"];
            string username = Configuration["Data:RDS:username"];
            string password = Configuration["Data:RDS:password"];
            //Data Source=.\\SQLEXPRESS;Initial Catalog=MoneyLover;Integrated Security=False;User ID=sa;Password=123456a@;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
            string connectionString = "Data Source=" + hostname+ ";Initial Catalog=" + dbname + ";User ID=" + username + ";Password=" + password + ";Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            return connectionString;
        }
    }
}
