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
    public partial class industrysamsodhanviewr : Form
    {
        public industrysamsodhanviewr()
        {
            InitializeComponent();
        }
        ReportDocument crypt = new ReportDocument();
        string reg;
        string darta;
        string bodartha1;
        string bodartha2;
        public void getdata(string regnot, string dartanum, string bod1, string bod2)
        {
            reg = regnot.ToString();
            darta = dartanum.ToString();
            bodartha1 = bod1.ToString();
            bodartha2 = bod2.ToString();
            

            // MessageBox.Show(reg.ToString());
            //date_txt.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }
        private void industrysamsodhanviewr_Load(object sender, EventArgs e)
        {
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                ReportDocument obj = new ReportDocument();
                string APPPATH;
                APPPATH = Environment.CurrentDirectory + "\\industrysomsodhanletterhead.rpt";
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
                ////////////////////////////////////////////////////////////////
              

                
                MySqlDataAdapter qrys = new MySqlDataAdapter("CALL industrysamsodhan(@dartanum)", sqlcon.con);
                qrys.SelectCommand.Parameters.AddWithValue("@dartanum", darta);
                //qry.SelectCommand.Parameters.AddWithValue("@dateto", dateTO);
                //qry.SelectCommand.Parameters.AddWithValue("@distid", distID);
                //DataTable tbs = new DataTable();

                MySqlDataAdapter qryp = new MySqlDataAdapter("CALL industryprev(@industryid)", sqlcon.con);
                qryp.SelectCommand.Parameters.AddWithValue("@industryid", reg);

                //qrys.Fill(tbs);

                MySqlDataAdapter qryss = new MySqlDataAdapter("CALL GetOfficeName(@isCurs)", sqlcon.con);
                qryss.SelectCommand.Parameters.AddWithValue("@isCurs", 1);
               
                //DataTable tbss = new DataTable();

               

                //qryss.Fill(tbss);
              //  MessageBox.Show(tbss.Rows.Count.ToString());
                // MessageBox.Show(global.csioid.ToString());
                MySqlDataAdapter officen = new MySqlDataAdapter("CALL GetBodarthaOffice(@officeid)", sqlcon.con);
                officen.SelectCommand.Parameters.AddWithValue("@officeid", global.csioid.ToString());
                //qry.SelectCommand.Parameters.AddWithValue("@dateto", dateTO);
                //qry.SelectCommand.Parameters.AddWithValue("@distid", distID);
                DataTable off = new DataTable();



                officen.Fill(off);
                string officebodartha1 = off.Rows[0]["bodarthoffice"].ToString() + ", " + off.Rows[0]["address"].ToString();
                //  string officebodartha2 = off.Rows[1]["bodarthoffice"].ToString();

                DataSet dt = new DataSet();
               qrys.Fill(dt, "industrysomsodhan");
                qryp.Fill(dt, "industryprev");
                
                qryss.Fill(dt, "GetOfficeName");
                officen.Fill(dt, "GetBodarthaOffice");
                //  sdaa.Fill(dt, "industryowner");
                obj.SetDataSource(dt);
             
                obj.SetParameterValue("bodartha1", bodartha1.ToString());
                obj.SetParameterValue("bodartha2", bodartha2.ToString());
                obj.SetParameterValue("bodarthaoffice1", officebodartha1);

                obj.SetParameterValue("curdate", global.todaynepslash);
                // crypt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = obj;
                crystalReportViewer1.Refresh();

                /////////////////////////////////////////////////////////
                //   MessageBox.Show(darta.ToString());

                // //  obj.SetDatabaseLogon(Properties.Settings.Default.username, Properties.Settings.Default.password, Properties.Settings.Default.servername, Properties.Settings.Default.databasename);
                // obj.SetParameterValue("indid", reg);
                // obj.SetParameterValue("dartanums", darta);
                //// obj.SetParameterValue("industryids", reg);
                // obj.SetParameterValue("officeids", global.csioid);
                // obj.SetParameterValue("isCurs", "1");
               
                // // obj.SetParameterValue("recieptnum", billno);
                // // obj.SetParameterValue("nepalidate", global.todaynepslash);
                // //  obj.SetParameterValue("fy", fy);
                // crystalReportViewer1.ReportSource = obj;
                // crystalReportViewer1.Refresh();

                this.KeyPreview = true;
                this.WindowState = FormWindowState.Maximized;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }



            //////////////////////////////////////////////////////////
        }

        //printing report using shortcut key
        private void reportviewer_KeyDown(object sender, KeyEventArgs e)
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
    