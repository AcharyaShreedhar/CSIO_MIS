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
    public partial class industryclosesearch : Form
    {
        public industryclosesearch()
        {
            InitializeComponent();
        }
        public int id;
        private void industryclosesearch_Load(object sender, EventArgs e)
        {
            button3.Enabled = false;

            //Window State
            this.WindowState = FormWindowState.Normal;

            //POSITION and SIZE
            this.Left = (this.Parent.Width / 2) - (this.Width / 2) - 5;
            this.Top = 0;
            //border
            global.createBorderAround(this, Color.Teal, 2);

        }
        public void DisplayData()
        {
            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");
            try
            {
                dataGridView1.DataSource = null;
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }

                MySqlDataAdapter qry = new MySqlDataAdapter(" SELECT     industryreg_hist.industryid, GetNumberToUnicode(industryreg_hist.industryregno) AS 'र‍. न‌ं', GetNumberToUnicode(industryreg_hist.regdat) AS 'दर्ता मिति', GetNumberToUnicode(industryreg_hist.renewdate) AS 'नविकरण मिति',  setup_subcategory_1.subcategory_unicodename AS 'स्तर', setup_subcategory.subcategory_unicodename AS 'प्रकार', setup_subcategory_2.subcategory_unicodename AS 'वर्ग', industryreg_hist.industrynepname AS 'उद्योगको नाम', industryreg_hist.branch AS 'शाखा', setup_district.distunicodename AS 'जिल्ला', setup_vdc.vdcunicodename AS 'गा.पा.। न.पा.',   GetNumberToUnicode(industryreg_hist.industryward) AS 'वार्ड', industryreg_hist.tole AS 'टोल', industryreg_hist.karobar AS 'कारोवार', GetNumberToUnicode(industryreg_hist.yearlyturnover) AS 'उत्पादकत्व(रु मा)',  GetNumberToUnicode(industryreg_hist.electricpower) AS 'विध्युत', GetNumberToUnicode(industryreg_hist.statcapital) AS 'स्थिर पुँजी', GetNumberToUnicode(industryreg_hist.varcapital) AS 'चालु पँजी', GetNumberToUnicode((industryreg_hist.statcapital+industryreg_hist.varcapital)) AS 'कुल पुँजी', GetNumberToUnicode(industryreg_hist.femaleworker) AS 'महिला कामदार', GetNumberToUnicode(industryreg_hist.maleworker) AS 'पुरुष कामदार', GetNumberToUnicode(industryreg_hist.tax) AS 'राजश्व',  GetNumberToUnicode(industryreg_hist.decisionnepdate) AS 'निर्णय मिति', industryreg_hist.usr AS 'युजर', industryreg_hist.comment AS 'कैफियत' FROM         industryreg_hist INNER JOIN  setup_subcategory AS setup_subcategory_1 ON industryreg_hist.industryscale = setup_subcategory_1.subcategory_id INNER JOIN  setup_subcategory ON industryreg_hist.industrytype = setup_subcategory.subcategory_id INNER JOIN   setup_subcategory AS setup_subcategory_2 ON industryreg_hist.industrycat = setup_subcategory_2.subcategory_id INNER JOIN     setup_district ON industryreg_hist.industrydist = setup_district.distcode INNER JOIN     setup_vdc ON industryreg_hist.industryvdc = setup_vdc.VDC_SID WHERE     (GetUnicodeToNumber(industryreg_hist.industryregno) = GetUnicodeToNumber(@darta)) and transid='128' ", sqlcon.con);
                qry.SelectCommand.Parameters.AddWithValue("@darta", darta_txt.Text);
                DataTable tb = new DataTable();
                qry.Fill(tb);
                dataGridView1.DataSource = tb;
                dataGridView1.Columns[0].Visible = false;
                sqlcon.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void DisplayDataowner()
        {
            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
           // con.Open();
            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     owner_hist_industry.ownerid, owner_hist_industry.industryid, GetNumberToUnicode(owner_hist_industry.industryregno) AS 'उद्योग दर्ता न‌ं', owner_hist_industry.ownerfname AS 'नाम',  owner_hist_industry.ownerlname AS 'थर', setup_district.distunicodename AS 'जिल्ला', setup_vdc.vdcunicodename AS 'गा.पा.। न.पा', GetNumberToUnicode(owner_hist_industry.ownervdcward) AS 'वार्ड',  owner_hist_industry.ownertole AS 'टोल', setup_subcategory_1.subcategory_unicodename AS 'बावु।पति', owner_hist_industry.ownergname AS 'बावुको नाम',  setup_subcategory.subcategory_unicodename AS 'बाजे।ससुरा', owner_hist_industry.ownerfgname AS 'वाजेको नाम', owner_hist_industry.comment AS 'कैफियत' FROM         setup_district INNER JOIN   owner_hist_industry ON setup_district.distcode = owner_hist_industry.ownerdistcode INNER JOIN      setup_vdc ON owner_hist_industry.ownervdccode = setup_vdc.VDC_SID INNER JOIN   setup_subcategory AS setup_subcategory_1 ON owner_hist_industry.ownergrel = setup_subcategory_1.subcategory_id INNER JOIN     setup_subcategory ON owner_hist_industry.ownerfgrel = setup_subcategory.subcategory_id WHERE     (owner_hist_industry.industryid = @darta)", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@darta", id.ToString());
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView2.DataSource = tb;
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[1].Visible = false;

            sqlcon.con.Close();

        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            button3.Enabled = true;
            int i;

            i = dataGridView1.CurrentRow.Index;
            id = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
            DisplayDataowner();
        }

        private void dataGridView1_RowHeaderMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            button3.Enabled = true;
            int i;

            i = dataGridView1.CurrentRow.Index;
            id = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
            DisplayDataowner();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i;

            i = dataGridView1.CurrentRow.Index;

            string dartan = dataGridView1.Rows[i].Cells[0].Value.ToString();
            //MessageBox.Show(dartanum.ToString());
            industrycloseletterviewer lettclose = new industrycloseletterviewer();
           lettclose.MdiParent = this.MdiParent;

            lettclose.getdata(dartan);
            lettclose.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            button3.Enabled = true;
            int i;

            i = dataGridView1.CurrentRow.Index;
            id = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
            DisplayDataowner();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
