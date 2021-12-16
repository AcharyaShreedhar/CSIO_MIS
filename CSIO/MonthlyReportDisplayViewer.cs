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
    public partial class MonthlyReportDisplayViewer : Form
    {
        public MonthlyReportDisplayViewer()
        {
            InitializeComponent();
        }
        string datefrom;
        string dateto;
        string officeid;
        public void getdata(string datef,string datet,string offid)
        {
            datefrom = datef.ToString();
            dateto = datet.ToString();
            officeid = offid.ToString();
       
        }
        private void MonthlyReportDisplayViewer_Load(object sender, EventArgs e)
        {
            try
            {
                ReportDocument obj = new ReportDocument();
                string APPPATH;
                APPPATH = Environment.CurrentDirectory + "\\officemonthlyreport.rpt";
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
                //  MessageBox.Show("OwnerId:" + ownerid.ToString() + "Indistryid:" + reg.ToString());
                obj.SetParameterValue("datefrom", datefrom);
                obj.SetParameterValue("isCurs", "1");
                obj.SetParameterValue("dateto", dateto.ToString());
               obj.SetParameterValue("officename", global.csioffice.ToString());
               // obj.SetParameterValue("officename", officeid.ToString());
                //obj.SetParameterValue("newownername", newownernam.ToString());
                //obj.SetParameterValue("newownerdistrict", newownerdistrict.ToString());
                //obj.SetParameterValue("newownervdc", newownervdc.ToString());
                //obj.SetParameterValue("newownerward", newownerward.ToString());
                //obj.SetParameterValue("newownercitizenship", newownercitizenship.ToString());

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
