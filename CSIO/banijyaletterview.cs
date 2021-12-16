using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
//using MySql.Data.MySqlClient;

namespace CSIO
{
    public partial class banijyaletterview : Form
    {
        public banijyaletterview()
        {
            InitializeComponent();
        }
        ReportDocument crypt = new ReportDocument();
        string reg;
        string firmtypes;
        string filename;
        public void getdata(string darta,string firmtype)
        {
            reg = darta.ToString();
            firmtypes = firmtype.ToString();

            // MessageBox.Show(reg.ToString());
            //date_txt.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }

        private void banijyaletterview_Load(object sender, EventArgs e)
        {
            //getdata(reg);
            try
            {
               // MessageBox.Show(firmtypes.ToString());
               
                ReportDocument obj = new ReportDocument();
                string APPPATH;
                if (firmtypes == "61")
                {
                    APPPATH = Environment.CurrentDirectory + "\\banijyaletter.rpt";
                }
                else
                {
                    APPPATH = Environment.CurrentDirectory + "\\banijyasajhedariletter.rpt";
                }
                //APPPATH = Environment.CurrentDirectory + @filename.ToString();
                obj.Load(APPPATH);
                //  obj.Load(@"..\..\accounttransaction.rpt");
                //obj.Load(@"+Properties.Settings.Default.username");
                ConnectionInfo crConnectionInfo = new ConnectionInfo();
                crConnectionInfo.LogonProperties.Remove(crConnectionInfo);
                crConnectionInfo.AllowCustomConnection = true;

                crConnectionInfo.IntegratedSecurity = true;
                crConnectionInfo.ServerName = Properties.Settings.Default.servername;
                //crConnectionInfo.ServerName = "127.0.0.1";
                crConnectionInfo.DatabaseName = Properties.Settings.Default.databasename;
                crConnectionInfo.UserID = Properties.Settings.Default.username;
                crConnectionInfo.Password = Properties.Settings.Default.password;
                TableLogOnInfo crTableLogoninfo = new TableLogOnInfo();
                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in obj.Database.Tables)
                {
                    crTableLogoninfo = CrTable.LogOnInfo;
                    crTableLogoninfo.ConnectionInfo = crConnectionInfo;
                }
                for (int i = 0; i < obj.Subreports.Count; i++)
                {
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in obj.Subreports[0].Database.Tables)
                    {
                        crTableLogoninfo = CrTable.LogOnInfo;
                        crTableLogoninfo.ConnectionInfo = crConnectionInfo;
                    }
                }

                //  obj.SetDatabaseLogon(Properties.Settings.Default.username, Properties.Settings.Default.password, Properties.Settings.Default.servername, Properties.Settings.Default.databasename);
                obj.SetParameterValue("firmids", reg);
                //obj.SetParameterValue("bodartha", bodarthatxt.ToString());
                //obj.SetParameterValue("bodartha1", bodarthatxt1.ToString());
                obj.SetParameterValue("officeids", global.csioid);
                obj.SetParameterValue("isCurs", "1");
                // obj.SetParameterValue("recieptnum", billno);
                // obj.SetParameterValue("nepalidate", global.todaynepslash);
                //  obj.SetParameterValue("fy", fy);
                crystalReportViewer1.ReportSource = obj;
                crystalReportViewer1.Refresh();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}
