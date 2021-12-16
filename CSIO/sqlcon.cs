using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
//using MySql.Data.MySqlClient;

namespace CSIO
{
    class sqlcon
    {
        public static MySqlConnection con = new MySqlConnection("datasource='" + Properties.Settings.Default.servername + "';port='" + Properties.Settings.Default.port + "';username='" + Properties.Settings.Default.username + "';password='" + Properties.Settings.Default.password + "';database='" + Properties.Settings.Default.databasename + "';charset=utf8");
        // public static SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.passwordname + "' ");
    }
    class sqlcononline
    {
        public static MySqlConnection cononline = new MySqlConnection("datasource='" + Properties.Settings.Default.onlineservername + "';port='" + Properties.Settings.Default.port + "';username='" + Properties.Settings.Default.onlineusername + "';password='" + Properties.Settings.Default.onlinepassword + "';database='" + Properties.Settings.Default.onlinedatabasename + "';charset=utf8;Convert Zero Datetime=True");
        // public static SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.passwordname + "' ");
    }
}
