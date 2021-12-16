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

namespace CSIO
{
    public partial class banijyafirmcloseletterview : Form
    {
        public banijyafirmcloseletterview()
        {
            InitializeComponent();
        }
        ReportDocument crypt = new ReportDocument();
        string reg;
        public void getdata(string darta)
        {
            reg = darta.ToString();

            // MessageBox.Show(reg.ToString());
            //date_txt.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }

        private void banijyafirmcloseletterview_Load(object sender, EventArgs e)
        {
            getdata(reg);
            try
            {
                ReportDocument obj = new ReportDocument();
                string APPPATH;
                APPPATH = Environment.CurrentDirectory + "\\closebanijyaletterhead.rpt";
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
                //   MessageBox.Show(darta.ToString());

                //  obj.SetDatabaseLogon(Properties.Settings.Default.username, Properties.Settings.Default.password, Properties.Settings.Default.servername, Properties.Settings.Default.databasename);
                obj.SetParameterValue("firmids", reg);
                // obj.SetParameterValue("dartanums", darta);
                // obj.SetParameterValue("industryids", reg);
                obj.SetParameterValue("officeid", global.csioid);
                obj.SetParameterValue("isCurs", "1");
                // obj.SetParameterValue("isCurs", "1");
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



            //////////////////////////////////////////////////////////

        }


        // }

    }
}

