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
    public partial class sanakhatlagatkattapreview : Form
    {
        public sanakhatlagatkattapreview()
        {
            InitializeComponent();
        }
        string reg;
        //string ownerid;
        //string newownernam;
        //string newownerdistrict;
        //string newownervdc;
        //string newownerward;
        //string newownercitizenship;

        public void getdata(string darta)
        {
            reg = darta.ToString();
          //  MessageBox.Show(reg.ToString());
           // ownerid = ownerids.ToString();
            //newownernam = newownername.ToString();
            //newownerdistrict = ownerdist.ToString();
            //newownervdc = ownervdc.ToString();
            //newownerward = ownerward.ToString();
            //newownercitizenship = ownercitizenno.ToString();



            // MessageBox.Show(reg.ToString());
            //date_txt.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }
        private void sanakhatlagatkattapreview_Load(object sender, EventArgs e)
        {
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                ReportDocument obj = new ReportDocument();
                string APPPATH;
                APPPATH = Environment.CurrentDirectory + "\\sanakhatindustrylagatkatta.rpt";
                obj.Load(APPPATH);
               
                //  obj.Load(@"..\..\accounttransaction.rpt");
                //obj.Load(@"+Properties.Settings.Default.username");
                /////////////////////////////////////////////////////////////////
                ConnectionInfo connInfo = new ConnectionInfo();

                ReportDocument cryRpt = new ReportDocument();
                TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                ConnectionInfo crConnectionInfo = new ConnectionInfo();
              

                // crypt.Load(@"..\..\banijyasamsodhanletter.rpt");
                MySqlDataAdapter qry = new MySqlDataAdapter("CALL industryrec(@indid)", sqlcon.con);
                qry.SelectCommand.Parameters.AddWithValue("@indid", reg);
                //qry.SelectCommand.Parameters.AddWithValue("@dateto", dateTO);
                //qry.SelectCommand.Parameters.AddWithValue("@distid", distID);
                DataTable tb = new DataTable();

           

                qry.Fill(tb);
                MySqlDataAdapter qrys = new MySqlDataAdapter("CALL industryowner(@industryids)", sqlcon.con);
                qrys.SelectCommand.Parameters.AddWithValue("@industryids", reg);
                //qry.SelectCommand.Parameters.AddWithValue("@dateto", dateTO);
                //qry.SelectCommand.Parameters.AddWithValue("@distid", distID);
                DataTable tbs = new DataTable();


                qrys.Fill(tbs);
                
                MySqlDataAdapter qrye = new MySqlDataAdapter("SELECT GetNepaliDateEsbisambat(@nepdate) As 'esb'", sqlcon.con);
                qrye.SelectCommand.Parameters.AddWithValue("@nepdate", global.todaynepslash);
                DataTable tbss = new DataTable();
                

                qrye.Fill(tbss);
                string esbisambat = tbss.Rows[0]["esb"].ToString();
                MySqlDataAdapter qryss = new MySqlDataAdapter("CALL GetOfficeName(@isCurs)", sqlcon.con);
                qryss.SelectCommand.Parameters.AddWithValue("@isCurs", 1);

                DataSet dt = new DataSet();
                qry.Fill(dt, "industryrec");
                qrys.Fill(dt, "industryowner");
                qryss.Fill(dt, "GetOfficeName");
                //  sdaa.Fill(dt, "industryowner");
               
                obj.SetDataSource(dt);
                obj.SetParameterValue("esbisambat", esbisambat.ToString());
                // crypt.SetDataSource(dt);
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
