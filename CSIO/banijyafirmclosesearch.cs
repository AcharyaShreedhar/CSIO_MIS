using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CSIO
{
    public partial class banijyafirmclosesearch : Form
    {
        public banijyafirmclosesearch()
        {
            InitializeComponent();
        }
        public int id;
        private void button1_Click(object sender, EventArgs e)
        {
            DisplayData();
        }
        public void DisplayData()
        {
            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }

            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     banijyaform_hist.firmid, GetNumberToUnicode(banijyaform_hist.firmregno) AS 'र.नं', banijyaform_hist.firmnepname AS 'फर्मको नाम',GetNumberToUnicode(banijyaform_hist.decisionnepdate) AS 'लगतकट्टा निर्णय मिति', setup_district.distunicodename AS 'जिल्ला',           setup_vdc.vdcunicodename AS 'गा.पा.। न.पा.', GetNumberToUnicode(banijyaform_hist.firmward) AS 'वार्ड', banijyaform_hist.tole AS 'टोल',setup_subcategory.subcategory_unicodename AS 'प्रकार',  GetNumberToUnicode(banijyaform_hist.regdat) AS 'दर्ता मिति',  GetNumberToUnicode(banijyaform_hist.renewdate) AS 'नविकरण', setup_subcategory_1.subcategory_unicodename AS 'उद्देश्य', banijyaform_hist.karobar AS 'कारोवार', GetNumberToUnicode(banijyaform_hist.revenue) AS 'पुँजी',  GetNumberToUnicode(banijyaform_hist.tax) AS 'राजश्व',banijyaform_hist.branch AS 'शाखा', banijyaform_hist.comment AS 'कैफियत' FROM         banijyaform_hist INNER JOIN    setup_subcategory ON banijyaform_hist.firmtype = setup_subcategory.subcategory_id INNER JOIN     setup_district ON banijyaform_hist.firmdist = setup_district.distcode INNER JOIN    setup_vdc ON banijyaform_hist.firmvdc = setup_vdc.VDC_SID INNER JOIN  setup_subcategory AS setup_subcategory_1 ON banijyaform_hist.firmscope = setup_subcategory_1.subcategory_id where (GetNumberToUnicode(firmregno)=@darta or (firmregno)=@darta) and transid=128 ", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@darta", darta_txt.Text);
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView1.DataSource = tb;
            dataGridView1.Columns[0].Visible = false;
            sqlcon.con.Close();
        }

        public void DisplayDataowner()
        {
            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     owner_hist_banijya.ownerfname AS 'नाम', owner_hist_banijya.ownerlname AS 'थर', setup_citizenissueoff.citizen_officeunicodename AS 'नागरिकता जारी गर्ने कार्यालय', setup_district.distunicodename AS 'जिल्ला',                       GetNumberToUnicode(owner_hist_banijya.citzdate) AS 'जारी मिति', GetNumberToUnicode(owner_hist_banijya.citznum) AS 'ना.प्र.नं.', setup_district_1.distunicodename AS 'जिल्ला', setup_vdc.vdcunicodename AS 'गा.वि.स । न.पा.', owner_hist_banijya.ownertole AS 'टोल',                       setup_subcategory_1.subcategory_unicodename AS 'बाबु । पति', owner_hist_banijya.ownergname AS 'बावु । पति नाम', setup_subcategory.subcategory_unicodename AS 'बाजे । ससुरा',                       owner_hist_banijya.ownerfgname AS 'बाजे नाम', setup_subcategory_2.subcategory_unicodename AS 'लिंग', owner_hist_banijya.comment AS 'कैफियत' FROM         owner_hist_banijya INNER JOIN            setup_district ON owner_hist_banijya.cddist = setup_district.distcode INNER JOIN          setup_citizenissueoff ON owner_hist_banijya.ccio = setup_citizenissueoff.citizen_officeid INNER JOIN            setup_district AS setup_district_1 ON owner_hist_banijya.ownerdistcode = setup_district_1.distcode INNER JOIN         setup_vdc ON owner_hist_banijya.ownervdccode = setup_vdc.VDC_SID INNER JOIN        setup_subcategory AS setup_subcategory_1 ON owner_hist_banijya.ownergrel = setup_subcategory_1.subcategory_id INNER JOIN           setup_subcategory ON owner_hist_banijya.ownerfgrel = setup_subcategory.subcategory_id INNER JOIN      setup_subcategory AS setup_subcategory_2 ON owner_hist_banijya.gender = setup_subcategory_2.subcategory_id WHERE     (owner_hist_banijya.firmid = @darta)", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@darta", id.ToString());
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView2.DataSource = tb;

            sqlcon.con.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i;

            i = dataGridView1.CurrentRow.Index;

            string dartan = dataGridView1.Rows[i].Cells[0].Value.ToString();
            //MessageBox.Show(dartanum.ToString());
            banijyafirmcloseletterview lett = new banijyafirmcloseletterview();
            lett.MdiParent = this.MdiParent;

            lett.getdata(dartan);
            lett.Show();
        }

        private void banijyafirmclosesearch_Load(object sender, EventArgs e)
        {
            button3.Enabled = false;
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            button3.Enabled = true;
            int i;

            i = dataGridView1.CurrentRow.Index;
            id = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
            DisplayDataowner();
        }
    }
}
