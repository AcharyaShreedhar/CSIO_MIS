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
    public partial class industrysomsodhanletterviewer : Form
    {
        public industrysomsodhanletterviewer()
        {
            InitializeComponent();
        }
        ReportDocument crypt = new ReportDocument();
        string reg;
        string darta;
        //System.Windows.Forms jitDebugging = "true";
        public void getdata(string regnot, string dartanum)
        {
            reg = regnot.ToString();
            darta = dartanum.ToString();

            // MessageBox.Show(reg.ToString());
            //date_txt.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }

        private void industrysomsodhanletterviewer_Load(object sender, EventArgs e)
        {
            try
            {
                getdata(reg, darta);
                //  MessageBox.Show(reg.ToString());
                //MessageBox.Show(darta.ToString());
                //crypt.Load(@"..\..\banijyaletter.rpt");
                //   crypt.Load(@"'"+Properties.Settings.Default.reportaddress+"'samsodhanrecord.rpt");
                ReportDocument cryRpt = new ReportDocument();
                TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                ConnectionInfo crConnectionInfo = new ConnectionInfo();
                Tables CrTables;

             // ServerFileReport.PATH_DELIMITER(
                cryRpt.Load(@"..\..\samsodhanrecord.rpt");
              //  cryRpt.Load(@"~/Reports/samsodhanrecord.rpt");

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
                //darta = "12";
                // crypt.Load(@"..\..\banijyasamsodhanletter.rpt");
             //   MessageBox.Show("Inside Viewer" + reg.ToString() + " " + darta.ToString());
                //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                cryRpt.SetDatabaseLogon(Properties.Settings.Default.username, Properties.Settings.Default.password);
                MySqlDataAdapter sda = new MySqlDataAdapter("industryprev", sqlcon.con);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter sdaa = new MySqlDataAdapter("industrysamsodhan", sqlcon.con);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter oname = new MySqlDataAdapter("industryowner", sqlcon.con);
                oname.SelectCommand.CommandType = CommandType.StoredProcedure;
                oname.SelectCommand.Parameters.AddWithValue("industryid", System.Data.SqlDbType.Int).Value = reg;
               // oname.SelectCommand.Parameters.Add("@industryid", System.Data.SqlDbType.Int).Value = reg;
                sdaa.SelectCommand.Parameters.AddWithValue("industryid", System.Data.SqlDbType.Int).Value = reg;
                sdaa.SelectCommand.Parameters.AddWithValue("dartanum", System.Data.SqlDbType.Int).Value = darta;
                sda.SelectCommand.Parameters.AddWithValue("industryid", System.Data.SqlDbType.Int).Value = reg;

                DataSet dt = new System.Data.DataSet();
                sda.Fill(dt, "industryprev");
                sdaa.Fill(dt, "industrysamsodhan");
            //   oname.Fill(dt, "industryowner");
                cryRpt.SetDataSource(dt);
                // crypt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = cryRpt;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


            this.KeyPreview = true;
            this.WindowState = FormWindowState.Maximized;

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
