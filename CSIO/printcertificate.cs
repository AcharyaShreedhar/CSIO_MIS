using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using MySql.Data.MySqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using MySql.Data.MySqlClient;
using System.Data.Odbc;
namespace CSIO
{
    public partial class printcertificate : Form
    {
        ReportDocument crypt = new ReportDocument();
        
        public printcertificate()
        {
            InitializeComponent();
        }
        string reg;
        string indtype;
        string indnature;
        //string comment_txt;
        public void getdataa(string darta,string indtyp,string indnatur)
        {
             reg = darta.ToString();
            indtype = indtyp.ToString();
            indnature = indnatur.ToString();
           // MessageBox.Show(reg.ToString());
            //date_txt.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }
        public void sanaudhyog()
        {
           // MessageBox.Show(reg.ToString());
            getdataa(reg, indtype, indnature);
//MessageBox.Show("After" + reg.ToString());
            try {
                string filename;
                if (indtype == "131")
                {
                    filename = "\\industrylaghureport.rpt";
                    // crypt.Load(@"D:\CSIO project\Reports\industrylaghureport.rpt");//C#

                }
                else if (indnature == "93")
                {
                    filename = "\\industrypralireport.rpt";
                    //  crypt.Load(@"D:\CSIO project\Reports\industrypralireport.rpt");

                }
                else if (indnature == "92")
                {
                    // laghu();
                    //  MessageBox.Show("Sajhedari");
                    filename = "\\industrysajhedarireport.rpt";
                    //  crypt.Load(@"D:\CSIO project\Reports\industrysajhedarireport.rpt");
                }
                else
                {
                    //MessageBox.Show(reg.ToString());
                    // MessageBox.Show("Anya");
                    filename = "\\industryrecord.rpt";
                    //  crypt.Load(@"D:\CSIO project\Reports\industryrecord.rpt");

                }
                ReportDocument obj = new ReportDocument();
                string APPPATH;
                APPPATH = Environment.CurrentDirectory + filename.ToString();
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

                obj.SetDatabaseLogon(Properties.Settings.Default.username, Properties.Settings.Default.password, Properties.Settings.Default.servername, Properties.Settings.Default.databasename);
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                obj.SetParameterValue("industryid", reg);


                // obj.SetParameterValue("recieptnum", billno);
                // obj.SetParameterValue("nepalidate", global.todaynepslash);
                // obj.SetParameterValue("fy", fy);
                crystalReportViewer1.ReportSource = obj;
                crystalReportViewer1.Refresh();
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void laghu()
        {
            try
            {
                
                //MessageBox.Show(reg.ToString());
               // crypt.Load(@"..\..\industryrecordlaghu.rpt");
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
             //   crypt.SetDatabaseLogon(Properties.Settings.Default.username, Properties.Settings.Default.passwordname);
                MySqlDataAdapter sda = new MySqlDataAdapter("industryrec", sqlcon.con);

                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter sdaa = new MySqlDataAdapter("industryowner", sqlcon.con);
                sdaa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaa.SelectCommand.Parameters.AddWithValue("@industryid", reg);
                sda.SelectCommand.Parameters.AddWithValue("@industryid", reg);
                // crypt.SetParameterValue("@industryid", reg.ToString());

              //  DataTable dt = new DataTable();
              //  DataSet dt = new System.Data.DataSet();
                DataSet dt = new DataSet();
                sda.Fill(dt);
                //sda.Fill(dt, "industryrecordcertificate");
                sdaa.Fill(dt);
              //  crypt.SetDataSource(dt);
              //  CSIO.industryrecordlaghu laghu = new industryrecordlaghu();
              //  laghu.Database.Tables["industryrec"].SetDataSource(dt.Tables[0]);
               // laghu.Database.Tables["industryowner"].SetDataSource(dt.Tables[0]);
              //  laghu.Database.Tables["industryrecordcertificate"].SetDataSource(dt);
             //   CSIO.industryletterowner owner = new industryletterowner();
              //  owner.Database.Tables["industryowner"].SetDataSource(dt);
              //  // crypt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = null;
         //       crystalReportViewer1.ReportSource = laghu;
              //  crystalReportViewer1.ReportSource = owner;
               crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void printcertificate_Load(object sender, EventArgs e)
        {
            getdataa(reg, indtype, indnature);
            //MessageBox.Show("After" + reg.ToString());
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
               
                    string filename;
                    if (indtype == "131")
                    {
                    // Laghu Certificate
                    // filename = "\\industrylaghureport.rpt";
                    filename = "\\industryrecord.rpt";
                    // crypt.Load(@"D:\CSIO project\Reports\industrylaghureport.rpt");//C#

                }
                    else if (indnature == "93")
                    {
                        filename = "\\industrypralireport.rpt";
                        //  crypt.Load(@"D:\CSIO project\Reports\industrypralireport.rpt");

                    }
                    else if (indnature == "92")
                    {
                        // laghu();
                        //  MessageBox.Show("Sajhedari");
                        filename = "\\industrysajhedarireport.rpt";
                        //  crypt.Load(@"D:\CSIO project\Reports\industrysajhedarireport.rpt");
                    }
                    else
                    {
                        //MessageBox.Show(reg.ToString());
                        // MessageBox.Show("Anya");
                        filename = "\\industryrecord.rpt";
                        //  crypt.Load(@"D:\CSIO project\Reports\industryrecord.rpt");

                    }
                    ReportDocument obj = new ReportDocument();
                string APPPATH;
                APPPATH = Environment.CurrentDirectory + filename.ToString();
                obj.Load(APPPATH);
                //  obj.Load(@"..\..\accounttransaction.rpt");
                //obj.Load(@"+Properties.Settings.Default.username");
                /////////////////////////////////////////////////////////////////
                ConnectionInfo connInfo = new ConnectionInfo();

                ReportDocument cryRpt = new ReportDocument();
                TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                ConnectionInfo crConnectionInfo = new ConnectionInfo();
              //  Tables CrTables;
               // string filename;
                // ReportDocument crypt = new ReportDocument();
              //  filename = "\\rptcert.rpt";
                //  crypt.Load(@"..\..\restreport.rpt");
             //   string APPPATH;
                //APPPATH = Environment.CurrentDirectory + filename.ToString();
                //cryRpt.Load(APPPATH);
                //cryRpt.Load(@"..\..\samsodhanrecord.rpt");
                // cryRpt.Load(@"Reports\\samsodhanrecord.rpt");

                //crConnectionInfo.ServerName = Properties.Settings.Default.servername;
                //crConnectionInfo.DatabaseName = Properties.Settings.Default.databasename;
                //crConnectionInfo.UserID = Properties.Settings.Default.username;
                //crConnectionInfo.Password = Properties.Settings.Default.password;

                //CrTables = cryRpt.Database.Tables;
                //foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                //{
                //    crtableLogoninfo = CrTable.LogOnInfo;
                //    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                //    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                //}

                // crypt.Load(@"..\..\banijyasamsodhanletter.rpt");
                MySqlDataAdapter qry = new MySqlDataAdapter("CALL industryrec(@indid)", sqlcon.con);
                qry.SelectCommand.Parameters.AddWithValue("@indid", reg);
                //qry.SelectCommand.Parameters.AddWithValue("@dateto", dateTO);
                //qry.SelectCommand.Parameters.AddWithValue("@distid", distID);
                DataTable tb = new DataTable();

                string[] hh; //Headers 
                string[] vv; //Values

                qry.Fill(tb);
                MySqlDataAdapter qrys = new MySqlDataAdapter("CALL industryowner(@industryids)", sqlcon.con);
                qrys.SelectCommand.Parameters.AddWithValue("@industryids", reg);
                //qry.SelectCommand.Parameters.AddWithValue("@dateto", dateTO);
                //qry.SelectCommand.Parameters.AddWithValue("@distid", distID);
                DataTable tbs = new DataTable();



                qrys.Fill(tbs);
                DataSet dt = new DataSet();
                qry.Fill(dt, "industryrec");
                qrys.Fill(dt, "industryowner");
                //  sdaa.Fill(dt, "industryowner");
                obj.SetDataSource(dt);
                // crypt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = obj;
                crystalReportViewer1.Refresh();

                //obj.SetDatabaseLogon(Properties.Settings.Default.username, Properties.Settings.Default.password, Properties.Settings.Default.servername, Properties.Settings.Default.databasename);
                //if (sqlcon.con.State == ConnectionState.Closed)
                //{
                //    sqlcon.con.Open();
                //}
                //obj.SetParameterValue("industryid", reg);
                //obj.SetParameterValue("indid", reg);


                // obj.SetParameterValue("recieptnum", billno);
                // obj.SetParameterValue("nepalidate", global.todaynepslash);
                // obj.SetParameterValue("fy", fy);
                //crystalReportViewer1.ReportSource = obj;
                //crystalReportViewer1.Refresh();

                this.KeyPreview = true;
                this.WindowState = FormWindowState.Maximized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            //// MessageBox.Show(reg.ToString());
            //getdataa(reg, indtype, indnature);
            ////MessageBox.Show("After" + reg.ToString());
            //try
            //{
            //    string filename;
            //    if (indtype == "131")
            //    {
            //        filename="\\industrylaghureport.rpt";
            //        // crypt.Load(@"D:\CSIO project\Reports\industrylaghureport.rpt");//C#

            //    }
            //    else if (indnature == "93")
            //    {
            //        filename="\\industrypralireport.rpt";
            //        //  crypt.Load(@"D:\CSIO project\Reports\industrypralireport.rpt");

            //    }
            //    else if (indnature == "92")
            //    {
            //        // laghu();
            //        //  MessageBox.Show("Sajhedari");
            //        filename="\\industrysajhedarireport.rpt";
            //        //  crypt.Load(@"D:\CSIO project\Reports\industrysajhedarireport.rpt");
            //    }
            //    else
            //    {
            //        //MessageBox.Show(reg.ToString());
            //        // MessageBox.Show("Anya");
            //        filename="\\industryrecord.rpt";
            //        //  crypt.Load(@"D:\CSIO project\Reports\industryrecord.rpt");

            //    }
            //    ReportDocument obj = new ReportDocument();
            //    string APPPATH;
            //    APPPATH = Environment.CurrentDirectory + filename.ToString();
            //    obj.Load(APPPATH);
            //    //  obj.Load(@"..\..\accounttransaction.rpt");
            //    //obj.Load(@"+Properties.Settings.Default.username");
            //    ConnectionInfo crConnectionInfo = new ConnectionInfo();
            //    crConnectionInfo.LogonProperties.Remove(crConnectionInfo);
            //    crConnectionInfo.AllowCustomConnection = true;

            //    crConnectionInfo.IntegratedSecurity = true;
            //    crConnectionInfo.ServerName = Properties.Settings.Default.servername;
            //    //crConnectionInfo.ServerName = "127.0.0.1";
            //    crConnectionInfo.DatabaseName = Properties.Settings.Default.databasename;
            //    crConnectionInfo.UserID = Properties.Settings.Default.username;
            //    crConnectionInfo.Password = Properties.Settings.Default.password;
            //    TableLogOnInfo crTableLogoninfo = new TableLogOnInfo();
            //    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in obj.Database.Tables)
            //    {
            //        crTableLogoninfo = CrTable.LogOnInfo;
            //        crTableLogoninfo.ConnectionInfo = crConnectionInfo;
            //    }
            //    for (int i = 0; i < obj.Subreports.Count; i++)
            //    {
            //        foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in obj.Subreports[0].Database.Tables)
            //        {
            //            crTableLogoninfo = CrTable.LogOnInfo;
            //            crTableLogoninfo.ConnectionInfo = crConnectionInfo;
            //        }
            //    }

            //   // obj.SetDatabaseLogon(Properties.Settings.Default.username, Properties.Settings.Default.password, Properties.Settings.Default.servername, Properties.Settings.Default.databasename);
            //    if (sqlcon.con.State == ConnectionState.Closed)
            //    {
            //        sqlcon.con.Open();
            //    }
            //    obj.SetParameterValue("industryid", reg);
            //    obj.SetParameterValue("indid", reg);

               
            //    // obj.SetParameterValue("recieptnum", billno);
            //    // obj.SetParameterValue("nepalidate", global.todaynepslash);
            //    // obj.SetParameterValue("fy", fy);
            //    crystalReportViewer1.ReportSource = obj;
            //    crystalReportViewer1.Refresh();




/////////////////////////////////////////////////////////////////////////////////////////////

//                ReportDocument crypt = new ReportDocument();
//                TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
//                TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
//                ConnectionInfo crConnectionInfo = new ConnectionInfo();
//                Tables CrTables;

//                // cryRpt.Load(Application.StartupPath + @"\Report\banijyaletter.rpt");

//                // cryRpt.Load( @"..\..\banijyaletterowner.rpt");
//                if (indtype == "131")
//                {
//                   crypt.Load(@"..\..\industrylaghureport.rpt");
//                   // crypt.Load(@"D:\CSIO project\Reports\industrylaghureport.rpt");//C#

//                }
//                else if (indnature == "93")
//                {
//                  crypt.Load(@"..\..\industrypralireport.rpt");
//                  //  crypt.Load(@"D:\CSIO project\Reports\industrypralireport.rpt");

//                }
//                else if (indnature == "92")
//                {
//                    // laghu();
//                 //  MessageBox.Show("Sajhedari");
//                  crypt.Load(@"..\..\industrysajhedarireport.rpt");
//                  //  crypt.Load(@"D:\CSIO project\Reports\industrysajhedarireport.rpt");
//                }
//                else
//                {
//                    //MessageBox.Show(reg.ToString());
//                   // MessageBox.Show("Anya");
//                  crypt.Load(@"..\..\industryrecord.rpt");
//                  //  crypt.Load(@"D:\CSIO project\Reports\industryrecord.rpt");

//                }
               

//                crConnectionInfo.ServerName = Properties.Settings.Default.servername;
//                crConnectionInfo.DatabaseName = Properties.Settings.Default.databasename;
//                crConnectionInfo.UserID = Properties.Settings.Default.username;
//                crConnectionInfo.Password = Properties.Settings.Default.password;

//                CrTables = crypt.Database.Tables;
//                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
//                {
//                    crtableLogoninfo = CrTable.LogOnInfo;
//                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
//                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
//                }

//                if (sqlcon.con.State == ConnectionState.Closed)
//                {
//                    sqlcon.con.Open();
//                }
//////SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");
//                // SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.passwordname + "' ");
//                crypt.SetDatabaseLogon(Properties.Settings.Default.username, Properties.Settings.Default.password);
//                MySqlDataAdapter sda = new MySqlDataAdapter("industryrec", sqlcon.con);
//                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
//                MySqlDataAdapter sdaa = new MySqlDataAdapter("industryowner", sqlcon.con);
//                sdaa.SelectCommand.CommandType = CommandType.StoredProcedure;
//                sdaa.SelectCommand.Parameters.Add("industryid", System.Data.SqlDbType.Int).Value = reg;
//                sda.SelectCommand.Parameters.Add("industryid", System.Data.SqlDbType.Int).Value = reg;
//                DataSet dt = new System.Data.DataSet();
//                sda.Fill(dt, "industryrec");
//                sdaa.Fill(dt, "industryowner");
//                crypt.SetDataSource(dt);
//                // crypt.SetDataSource(dt);
//                // crystalReportViewer1.ReportSource = null;
//                crystalReportViewer1.ReportSource = crypt;
//                crystalReportViewer1.Refresh();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            
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



        private void industryrecord1_InitReport(object sender, EventArgs e)
        {

        }

        private void industryownerrpt1_InitReport(object sender, EventArgs e)
        {

        }

        private void industryownerrpt2_InitReport(object sender, EventArgs e)
        {

        }
    }
}
