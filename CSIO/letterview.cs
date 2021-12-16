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
    public partial class letterview : Form
    {
        ReportDocument crypt = new ReportDocument();
        public letterview()
        {
            InitializeComponent();
        }
        string reg;
        string bodarthatxt;
        string bodarthatxt1;
        int lettertype = 0;
        public void getdata(string darta, string bodartha,string bodarthaa)
        {
            reg = darta.ToString();
            bodarthatxt = bodartha.ToString();
            bodarthatxt1=bodarthaa.ToString();

            // MessageBox.Show(reg.ToString());
            //date_txt.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }
        public void getdataa(string darta, string bodartha, string bodarthaa)
        {
            reg = darta.ToString();
            bodarthatxt = bodartha.ToString();
            bodarthatxt1 = bodarthaa.ToString();
            lettertype = 1;

            // MessageBox.Show(reg.ToString());
            //date_txt.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }
        private void letterview_Load(object sender, EventArgs e)
        {
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                ReportDocument obj = new ReportDocument();
                string APPPATH;
                if (lettertype == 1)
                {
                    APPPATH = Environment.CurrentDirectory + "\\renewindustryletter.rpt";
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

                    MySqlDataAdapter qry = new MySqlDataAdapter("CALL industryrec(@indid)", sqlcon.con);
                    qry.SelectCommand.Parameters.AddWithValue("@indid", reg);
                    //qry.SelectCommand.Parameters.AddWithValue("@dateto", dateTO);
                    //qry.SelectCommand.Parameters.AddWithValue("@distid", distID);
                    DataTable tb = new DataTable();

                

                    qry.Fill(tb);
                    //MySqlDataAdapter qrys = new MySqlDataAdapter("CALL industryowner(@industryids)", sqlcon.con);
                    //qrys.SelectCommand.Parameters.AddWithValue("@industryids", reg);
                    ////qry.SelectCommand.Parameters.AddWithValue("@dateto", dateTO);
                    ////qry.SelectCommand.Parameters.AddWithValue("@distid", distID);
                    //DataTable tbs = new DataTable();



                    //qrys.Fill(tbs);

                    MySqlDataAdapter qryss = new MySqlDataAdapter("CALL GetOfficeName(@isCurs)", sqlcon.con);
                    qryss.SelectCommand.Parameters.AddWithValue("@isCurs", 1);
                    //qry.SelectCommand.Parameters.AddWithValue("@dateto", dateTO);
                    //qry.SelectCommand.Parameters.AddWithValue("@distid", distID);
                    DataTable tbss = new DataTable();



                    qryss.Fill(tbss);
                    // MessageBox.Show(global.csioid.ToString());
                    MySqlDataAdapter officen = new MySqlDataAdapter("CALL industryrenewReport(@inid)", sqlcon.con);
                    officen.SelectCommand.Parameters.AddWithValue("@inid", reg);
                    //qry.SelectCommand.Parameters.AddWithValue("@dateto", dateTO);
                    //qry.SelectCommand.Parameters.AddWithValue("@distid", distID);
                    DataTable off = new DataTable();



                    officen.Fill(off);
                    //string officebodartha1 = off.Rows[0]["bodarthoffice"].ToString() + ", " + off.Rows[0]["address"].ToString();
                    ////  string officebodartha2 = off.Rows[1]["bodarthoffice"].ToString();

                    DataSet dt = new DataSet();
                    qry.Fill(dt, "industryrec");
                    officen.Fill(dt, "industryrenewReport");
                    qryss.Fill(dt, "getofficename");
                   // officen.Fill(dt, "GetBodarthaOffice");
                    //  sdaa.Fill(dt, "industryowner");
                    obj.SetDataSource(dt);
                    // crypt.SetDataSource(dt);
                    //crystalReportViewer1.ReportSource = obj;
                    //crystalReportViewer1.Refresh();

                    //  obj.SetDatabaseLogon(Properties.Settings.Default.username, Properties.Settings.Default.password, Properties.Settings.Default.servername, Properties.Settings.Default.databasename);
                    //  obj.SetParameterValue("indid", reg);
                    obj.SetParameterValue("curdate", global.todaynepslash);
                    //obj.SetParameterValue("bodartha1", bodarthatxt1.ToString());
                    //obj.SetParameterValue("bodarthaoffice1", officebodartha1);
                    //  obj.SetParameterValue("bodarthaoffice2", officebodartha2);
                    // obj.SetParameterValue("officeids", global.csioid);
                    // obj.SetParameterValue("isCurs", "1");
                    // obj.SetParameterValue("recieptnum", billno);
                    // obj.SetParameterValue("nepalidate", global.todaynepslash);
                    //  obj.SetParameterValue("fy", fy);
                    crystalReportViewer1.ReportSource = obj;
                    crystalReportViewer1.Refresh();
                }
                else
                {
                    APPPATH = Environment.CurrentDirectory + "\\industryletter.rpt";
                
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

                MySqlDataAdapter qry = new MySqlDataAdapter("CALL industryrec(@indid)", sqlcon.con);
                  //  MessageBox.Show(reg.ToString());
                qry.SelectCommand.Parameters.AddWithValue("@indid", reg.ToString());
                //qry.SelectCommand.Parameters.AddWithValue("@dateto", dateTO);
                //qry.SelectCommand.Parameters.AddWithValue("@distid", distID);
                DataTable tb = new DataTable();

                string[] hh; //Headers 
                string[] vv; //Values

                qry.Fill(tb);
                   // MessageBox.Show(tb.Rows.Count.ToString());
                    MySqlDataAdapter qrys = new MySqlDataAdapter("CALL industryowner(@industryids)", sqlcon.con);
                qrys.SelectCommand.Parameters.AddWithValue("@industryids", reg.ToString());
                //qry.SelectCommand.Parameters.AddWithValue("@dateto", dateTO);
                //qry.SelectCommand.Parameters.AddWithValue("@distid", distID);
                DataTable tbs = new DataTable();



                qrys.Fill(tbs);
                  //  MessageBox.Show(tbs.Rows.Count.ToString());

                MySqlDataAdapter qryss = new MySqlDataAdapter("CALL GetOfficeName(@isCurs)", sqlcon.con);
                qryss.SelectCommand.Parameters.AddWithValue("@isCurs", 1);
                //qry.SelectCommand.Parameters.AddWithValue("@dateto", dateTO);
                //qry.SelectCommand.Parameters.AddWithValue("@distid", distID);
                DataTable tbss = new DataTable();



                qryss.Fill(tbss);
               // MessageBox.Show(global.csioid.ToString());
                MySqlDataAdapter officen = new MySqlDataAdapter("CALL GetBodarthaOffice(@officeid)", sqlcon.con);
                officen.SelectCommand.Parameters.AddWithValue("@officeid", global.csioid.ToString());
                //qry.SelectCommand.Parameters.AddWithValue("@dateto", dateTO);
                //qry.SelectCommand.Parameters.AddWithValue("@distid", distID);
                DataTable off = new DataTable();



                officen.Fill(off);
                string officebodartha1 = off.Rows[0]["bodarthoffice"].ToString()+", "+ off.Rows[0]["address"].ToString();
              //  string officebodartha2 = off.Rows[1]["bodarthoffice"].ToString();

                DataSet dt = new DataSet();
                qry.Fill(dt, "industryrec");
                qrys.Fill(dt, "industryowner");
                qryss.Fill(dt, "getofficename");
                    obj.SetDataSource(dt);
                obj.SetParameterValue("bodartha", bodarthatxt.ToString());
                obj.SetParameterValue("bodartha1", bodarthatxt1.ToString());
                obj.SetParameterValue("bodarthaoffice1", officebodartha1);
           
                crystalReportViewer1.ReportSource = obj;
                crystalReportViewer1.Refresh();
                }

                this.KeyPreview = true;
                this.WindowState = FormWindowState.Maximized;
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }



            //getdata(reg);
            ////MessageBox.Show(reg.ToString());
            //crypt.Load("D:\\CSIO\\CSIO\\CSIO\\industryletter.rpt");
            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.passwordname + "' ");
            //SqlDataAdapter sda = new SqlDataAdapter("industryrec", con);
            //sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            //// SqlDataAdapter sdaa = new SqlDataAdapter("industryowner", con);
            //// sdaa.SelectCommand.CommandType = CommandType.StoredProcedure;
            //// sda.SelectCommand.Parameters.Add("@industryid", System.Data.SqlDbType.Int).Value = reg;
            //sda.SelectCommand.Parameters.Add("@industryid", System.Data.SqlDbType.Int).Value =reg;
            //DataSet dt = new System.Data.DataSet();
            //sda.Fill(dt, "industryrec");
            //crypt.SetDataSource(dt);
            //crystalReportViewer1.ReportSource = crypt;
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
