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
    public partial class somsodhanviewer : Form
    {
        public somsodhanviewer()
        {
            InitializeComponent();
        }
        ReportDocument crypt = new ReportDocument();
        string reg;
        string darta;
        string bodartha1;
        string bodartha2;
        string updateid;
        public void getdata(string regnot, string dartanum,string bod1,string bod2,string updatid)
        {
            reg = regnot.ToString();
            darta = dartanum.ToString();
            bodartha1 = bod1.ToString();
            bodartha2 = bod2.ToString();
            updateid = updatid.ToString();

            // MessageBox.Show(reg.ToString());
            //date_txt.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }


        private void somsodhanviewer_Load(object sender, EventArgs e)
        {

            try
            {
                // MessageBox.Show(firmtypes.ToString());

                ReportDocument obj = new ReportDocument();
                string APPPATH;
               // MessageBox.Show(updateid.ToString());
                if (updateid == "136")
                {
                    APPPATH = Environment.CurrentDirectory + "\\banijyalettertransfer.rpt";
                }
                else
                {
                    APPPATH = Environment.CurrentDirectory + "\\banijyasamsodhanletterhead.rpt";
                }
                //{
                //    APPPATH = Environment.CurrentDirectory + "\\banijyasajhedaricertdetail.rpt";
                //}
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
                obj.SetParameterValue("firmid", reg);
                //obj.SetParameterValue("bodartha", bodarthatxt.ToString());
                //obj.SetParameterValue("bodartha1", bodarthatxt1.ToString());
                obj.SetParameterValue("dartanums", darta);
                obj.SetParameterValue("isCurs", "1");
                obj.SetParameterValue("officeids", global.csioid);
                obj.SetParameterValue("bodartha1", bodartha1);
                obj.SetParameterValue("bodartha2", bodartha2);
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




            //////////////////////////////////////////////////////////////////////////////////



            // getdataa(reg);

            // ReportDocument cryRpt = new ReportDocument();
            // TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
            // TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            // ConnectionInfo crConnectionInfo = new ConnectionInfo();
            // Tables CrTables;



            // cryRpt.Load(@"..\..\banijyacertdetail.rpt");


            // crConnectionInfo.ServerName = Properties.Settings.Default.servername;
            // crConnectionInfo.DatabaseName = Properties.Settings.Default.databasename;
            // crConnectionInfo.UserID = Properties.Settings.Default.username;
            // crConnectionInfo.Password = Properties.Settings.Default.password;

            // CrTables = cryRpt.Database.Tables;
            // foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
            // {
            //     crtableLogoninfo = CrTable.LogOnInfo;
            //     crtableLogoninfo.ConnectionInfo = crConnectionInfo;
            //     CrTable.ApplyLogOnInfo(crtableLogoninfo);
            // }

            // if (sqlcon.con.State == ConnectionState.Closed)
            // {
            //     sqlcon.con.Open();
            // }

            // cryRpt.SetDatabaseLogon(Properties.Settings.Default.username, Properties.Settings.Default.password);

            // MySqlDataAdapter sda = new MySqlDataAdapter("banijyadetail", sqlcon.con);
            // sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            // MySqlDataAdapter sdaa = new MySqlDataAdapter("banijyaowner", sqlcon.con);
            // sdaa.SelectCommand.CommandType = CommandType.StoredProcedure;
            // sdaa.SelectCommand.Parameters.AddWithValue("firmid", System.Data.SqlDbType.Int).Value = reg;
            // sda.SelectCommand.Parameters.AddWithValue("firmid", System.Data.SqlDbType.Int).Value = reg;
            // DataSet dt = new System.Data.DataSet();
            // sda.Fill(dt, "banijyadetail");
            // sdaa.Fill(dt, "banijyaowner");
            //cryRpt.SetDataSource(dt);


            //crystalReportViewer1.ReportSource = cryRpt;
            //crystalReportViewer1.Refresh();

        

        }
    }
}
