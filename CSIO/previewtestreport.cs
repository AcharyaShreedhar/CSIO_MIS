using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using MySql.Data.MySqlClient;

namespace CSIO
{
    public partial class previewtestreport : Form
    {
        public previewtestreport()
        {
            InitializeComponent();
        }

        private void previewtestreport_Load(object sender, EventArgs e)
        {
            int n = 1;
            if(n==0)
            {
                

                    //  MessageBox.Show(reg.ToString());
                    //MessageBox.Show(darta.ToString());
                    //crypt.Load(@"..\..\banijyaletter.rpt");
                    //   crypt.Load(@"'"+Properties.Settings.Default.reportaddress+"'samsodhanrecord.rpt");
                    ReportDocument cryRpt = new ReportDocument();
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;
                  string filename;
                    // ReportDocument crypt = new ReportDocument();
                    filename = "\\rptcert.rpt";
                    //  crypt.Load(@"..\..\restreport.rpt");
                    string APPPATH;
                    APPPATH = Environment.CurrentDirectory + filename.ToString();
                cryRpt.Load(APPPATH);
                    //cryRpt.Load(@"..\..\samsodhanrecord.rpt");
                    // cryRpt.Load(@"Reports\\samsodhanrecord.rpt");

                    crConnectionInfo.ServerName = Properties.Settings.Default.servername;
                    crConnectionInfo.DatabaseName = Properties.Settings.Default.databasename;
                    crConnectionInfo.UserID = Properties.Settings.Default.username;
                    crConnectionInfo.Password = Properties.Settings.Default.password;

                    CrTables = cryRpt.Database.Tables;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    }

                // crypt.Load(@"..\..\banijyasamsodhanletter.rpt");
                MySqlDataAdapter qry = new MySqlDataAdapter("CALL industryrec(@indid)", sqlcon.con);
                qry.SelectCommand.Parameters.AddWithValue("@indid", 140767);
                //qry.SelectCommand.Parameters.AddWithValue("@dateto", dateTO);
                //qry.SelectCommand.Parameters.AddWithValue("@distid", distID);
                DataTable tb = new DataTable();

                string[] hh; //Headers 
                string[] vv; //Values

                qry.Fill(tb);
                MySqlDataAdapter qrys = new MySqlDataAdapter("CALL industryowner(@industryids)", sqlcon.con);
                qrys.SelectCommand.Parameters.AddWithValue("@industryids", 140767);
                //qry.SelectCommand.Parameters.AddWithValue("@dateto", dateTO);
                //qry.SelectCommand.Parameters.AddWithValue("@distid", distID);
                DataTable tbs = new DataTable();
               
            

                qrys.Fill(tbs);
                MessageBox.Show("Total No. of Rows in Owner Table=" + tbs.Rows.Count.ToString());
                DataSet dt = new DataSet();
                qry.Fill(dt, "industryrec");
                qrys.Fill(dt, "industryowner");
                //  sdaa.Fill(dt, "industryowner");
                cryRpt.SetDataSource(dt);
                // crypt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = cryRpt;
                crystalReportViewer1.Refresh();
                // //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");
                // cryRpt.SetDatabaseLogon(Properties.Settings.Default.username, Properties.Settings.Default.password);
                // MySqlDataAdapter sda = new MySqlDataAdapter("industryrec", sqlcon.con);
                // sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                // MySqlDataAdapter sdaa = new MySqlDataAdapter("industryowner", sqlcon.con);
                // sdaa.SelectCommand.CommandType = CommandType.StoredProcedure;
                // sdaa.SelectCommand.Parameters.AddWithValue("indid", System.Data.SqlDbType.Int).Value = 1407715;
                // sdaa.SelectCommand.Parameters.AddWithValue("industryids", System.Data.SqlDbType.Int).Value = 1407715;
                //// sda.SelectCommand.Parameters.AddWithValue("firmid", System.Data.SqlDbType.Int).Value = reg;
                //DataSet dt = new DataSet();
                //    sda.Fill(dt, "industryrec");
                //    sdaa.Fill(dt, "industryowner");
                //    cryRpt.SetDataSource(dt);
                //    // crypt.SetDataSource(dt);
                //    crystalReportViewer1.ReportSource = cryRpt;
                //    crystalReportViewer1.Refresh();
            }
                else
                {
                    ////////////////////////////////////////////////////////////////
                    ReportDocument obj = new ReportDocument();
                    string filename;
                    // ReportDocument crypt = new ReportDocument();
                    filename = "\\restreport.rpt";
                    //  crypt.Load(@"..\..\restreport.rpt");
                    string APPPATH;
                    APPPATH = Environment.CurrentDirectory + filename.ToString();
                    obj.Load(APPPATH);
                    crystalReportViewer1.ReportSource = obj;
                    crystalReportViewer1.Refresh();

                    this.KeyPreview = true;
                    this.WindowState = FormWindowState.Maximized;
                }
        }

        //printing report using shortcut key
        private void previewtestreport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.P && e.Modifiers == Keys.Control)
            {
                crystalReportViewer1.PrintReport();
                //report.PrintToPrinter(1, False, 0, 0);
            }
        }

        private void btnCloseReport_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
