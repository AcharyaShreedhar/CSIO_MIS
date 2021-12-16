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
    public partial class searchbanijya : Form
    {
        public searchbanijya()
        {
            InitializeComponent();
        }
        public int id;

        private void searchbanijya_Load(object sender, EventArgs e)
        {
            printcert_btn.Enabled = false;
            printletter_btn.Enabled = false;
        }
        public void getdataa(string darta)
        {
            darta_txt.Text = null;
            darta_txt.Text = darta.ToString();
            DisplayData();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DisplayData();
        }
        public void DisplayData()
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

          //  con.Open();

            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     banijyaform.firmid, banijyaform.firmtype As 'firmtype',GetNumberToUnicode(banijyaform.firmregno) AS 'र.नं', banijyaform.firmnepname AS 'फर्मको नाम', setup_district.distunicodename AS 'जिल्ला',           setup_vdc.vdcunicodename AS 'गा.पा.। न.पा.', GetNumberToUnicode(banijyaform.firmward) AS 'वार्ड', banijyaform.tole AS 'टोल',setup_subcategory.subcategory_unicodename AS 'प्रकार',  GetNumberToUnicode(banijyaform.regdat) AS 'दर्ता मिति',  GetNumberToUnicode(banijyaform.renewdate) AS 'नविकरण', setup_subcategory_1.subcategory_unicodename AS 'उद्देश्य', banijyaform.karobar AS 'कारोवार', GetNumberToUnicode(banijyaform.revenue) AS 'पुँजी',  GetNumberToUnicode(banijyaform.tax) AS 'राजश्व',banijyaform.branch AS 'शाखा', banijyaform.comment AS 'कैफियत' FROM         banijyaform INNER JOIN    setup_subcategory ON banijyaform.firmtype = setup_subcategory.subcategory_id INNER JOIN     setup_district ON banijyaform.firmdist = setup_district.distcode INNER JOIN    setup_vdc ON banijyaform.firmvdc = setup_vdc.VDC_SID INNER JOIN  setup_subcategory AS setup_subcategory_1 ON banijyaform.firmscope = setup_subcategory_1.subcategory_id where (GetNumberToUnicode(firmregno) = @darta) or (firmregno = @darta)", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@darta", darta_txt.Text);
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView1.DataSource = tb;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            sqlcon.con.Close();
        }

        public void DisplayDataowner()
        {
            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            //con.Open();
            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     owner.ownerfname AS 'नाम', owner.ownerlname AS 'थर', setup_citizenissueoff.citizen_officeunicodename AS 'नागरिकता जारी गर्ने कार्यालय', setup_district.distunicodename AS 'जिल्ला',                       GetNumberToUnicode(owner.citzdate) AS 'जारी मिति', GetNumberToUnicode(owner.citznum) AS 'ना.प्र.नं.', setup_district_1.distunicodename AS 'जिल्ला', setup_vdc.vdcunicodename AS 'गा.वि.स । न.पा.', owner.ownertole AS 'टोल',                       setup_subcategory_1.subcategory_unicodename AS 'बाबु । पति', owner.ownergname AS 'बावु । पति नाम', setup_subcategory.subcategory_unicodename AS 'बाजे । ससुरा',                       owner.ownerfgname AS 'बाजे नाम', setup_subcategory_2.subcategory_unicodename AS 'लिंग', owner.comment AS 'कैफियत' FROM         owner INNER JOIN            setup_district ON owner.cddist = setup_district.distcode INNER JOIN          setup_citizenissueoff ON owner.ccio = setup_citizenissueoff.citizen_officeid INNER JOIN            setup_district AS setup_district_1 ON owner.ownerdistcode = setup_district_1.distcode INNER JOIN         setup_vdc ON owner.ownervdccode = setup_vdc.VDC_SID INNER JOIN        setup_subcategory AS setup_subcategory_1 ON owner.ownergrel = setup_subcategory_1.subcategory_id INNER JOIN           setup_subcategory ON owner.ownerfgrel = setup_subcategory.subcategory_id INNER JOIN      setup_subcategory AS setup_subcategory_2 ON owner.gender = setup_subcategory_2.subcategory_id WHERE     (owner.firmid = @darta)", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@darta", id.ToString());
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView2.DataSource = tb;

            sqlcon.con.Close();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("कुनै रेकर्ड छानिएको छैन । कृपया रेकर्ड छान्नुहोस ।", "रेकर्ड छान्नुहोस");
            }
            else
            {
                int i;

                i = dataGridView1.CurrentRow.Index;

                string dartan = dataGridView1.Rows[i].Cells[0].Value.ToString();
                string firmtype = dataGridView1.Rows[i].Cells[1].Value.ToString();
              //  MessageBox.Show(firmtype.ToString());
                //MessageBox.Show(dartanum.ToString());
                reportviewer lett = new reportviewer();
                lett.MdiParent = this.MdiParent;

                lett.getdata(dartan, firmtype);
                lett.Show();
            }
            
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("कुनै रेकर्ड छानिएको छैन । कृपया रेकर्ड छान्नुहोस ।", "रेकर्ड छान्नुहोस");
            }
            else
            {
                int i;

                i = dataGridView1.CurrentRow.Index;

                string dartan = dataGridView1.Rows[i].Cells[0].Value.ToString();
                string firmtype = dataGridView1.Rows[i].Cells[1].Value.ToString();
               // MessageBox.Show(firmtype.ToString());
                //MessageBox.Show(dartanum.ToString());
                banijyaletterview lett = new banijyaletterview();
                lett.MdiParent = this.MdiParent;

                lett.getdata(dartan, firmtype);
                lett.Show();
            }
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            printcert_btn.Enabled = true;
            printletter_btn.Enabled = true;
            int i;

            i = dataGridView1.CurrentRow.Index;
            id = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
            DisplayDataowner();
        }  

    }
}
