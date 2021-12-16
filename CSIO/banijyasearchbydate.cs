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
    public partial class banijyasearchbydate : Form
    {
        public banijyasearchbydate()
        {
            InitializeComponent();
            //int id;
        }
        public int id;
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        public void DisplayDataowner()
        {
            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     owner.ownerfname AS 'नाम', owner.ownerlname AS 'थर', setup_citizenissueoff.citizen_officeunicodename AS 'नागरिकता जारी गर्ने कार्यालय', setup_district.distunicodename AS 'जिल्ला',                       GetNumberToUnicode(owner.citzdate) AS 'जारी मिति', GetNumberToUnicode(owner.citznum) AS 'ना.प्र.नं.', setup_district_1.distunicodename AS 'जिल्ला', setup_vdc.vdcunicodename AS 'गा.वि.स । न.पा.',GetNumberToUnicode(owner.ownervdcward) As 'वार्ड', owner.ownertole AS 'टोल',                       setup_subcategory_1.subcategory_unicodename AS 'बाबु । पति', owner.ownergname AS 'बावु । पति नाम', setup_subcategory.subcategory_unicodename AS 'बाजे । ससुरा',                       owner.ownerfgname AS 'बाजे नाम', setup_subcategory_2.subcategory_unicodename AS 'लिंग', owner.comment AS 'कैफियत' FROM         owner INNER JOIN            setup_district ON owner.cddist = setup_district.distcode INNER JOIN          setup_citizenissueoff ON owner.ccio = setup_citizenissueoff.citizen_officeid INNER JOIN            setup_district AS setup_district_1 ON owner.ownerdistcode = setup_district_1.distcode INNER JOIN         setup_vdc ON owner.ownervdccode = setup_vdc.VDC_SID INNER JOIN        setup_subcategory AS setup_subcategory_1 ON owner.ownergrel = setup_subcategory_1.subcategory_id INNER JOIN           setup_subcategory ON owner.ownerfgrel = setup_subcategory.subcategory_id INNER JOIN      setup_subcategory AS setup_subcategory_2 ON owner.gender = setup_subcategory_2.subcategory_id WHERE     (owner.firmid = @darta)", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@darta", id.ToString());
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView2.DataSource = tb;

            sqlcon.con.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }

            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     banijyaform.firmid, ROW_NUMBER() OVER (ORDER BY firmid) AS 'सि नं',GetNumberToUnicode(banijyaform.firmregno) AS 'र.नं', banijyaform.firmnepname AS 'फर्मको नाम', setup_district.distunicodename AS 'जिल्ला',           setup_vdc.vdcunicodename AS 'गा.पा.। न.पा.', GetNumberToUnicode(banijyaform.firmward) AS 'वार्ड', banijyaform.tole AS 'टोल',setup_subcategory.subcategory_unicodename AS 'प्रकार',  GetNumberToUnicode(banijyaform.regdat) AS 'दर्ता मिति',  GetNumberToUnicode(banijyaform.renewdate) AS 'नविकरण', setup_subcategory_1.subcategory_unicodename AS 'उद्देश्य', banijyaform.karobar AS 'कारोवार बस्तुहरु', GetNumberToUnicode(banijyaform.revenue) AS 'पुँजी',  GetNumberToUnicode(banijyaform.tax) AS 'राजश्व',banijyaform.branch AS 'शाखा',  setup_subcategory_2.subcategory_unicodename AS 'कारोवार', GetNumberToUnicode(banijyaform.thelino) AS 'ठेली न‌', GetNumberToUnicode(banijyaform.fileno) AS 'फाइल न‌', GetNumberToUnicode(banijyaform.fiscalyear) AS 'आ.व', banijyaform.comment AS 'कैफियत', banijyaform.firmtype FROM         banijyaform INNER JOIN    setup_subcategory ON banijyaform.firmtype = setup_subcategory.subcategory_id INNER JOIN     setup_district ON banijyaform.firmdist = setup_district.distcode INNER JOIN    setup_vdc ON banijyaform.firmvdc = setup_vdc.VDC_SID INNER JOIN  setup_subcategory AS setup_subcategory_1 ON banijyaform.firmscope = setup_subcategory_1.subcategory_id  INNER JOIN   setup_subcategory AS setup_subcategory_2 ON banijyaform.karobartype = setup_subcategory_2.subcategory_id where  Getint(registrationdate)>=Getint(@datefrom) AND Getint(registrationdate)<=Getint(@dateto)", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@datefrom", darta_date.Text);
            qry.SelectCommand.Parameters.AddWithValue("@dateto", dartadateto.Text);
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView1.DataSource = tb;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[14].Visible = false;
            sqlcon.con.Close();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //button9.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            int i;

            i = dataGridView1.CurrentRow.Index;
            id = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());

            DisplayDataowner();
           
        }

        private void dataGridView1_Layout(object sender, LayoutEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int i;

                i = dataGridView1.CurrentRow.Index;

                string dartanum = dataGridView1.Rows[i].Cells[2].Value.ToString();
                //MessageBox.Show(dartanum.ToString());
                searchbanijya bd = new searchbanijya();
                bd.MdiParent = this.MdiParent;

               bd.getdataa(dartanum);


                bd.Show();
            }
            catch (Exception fes)
            {
                MessageBox.Show(fes.ToString());
            }
                
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i;

            i = dataGridView1.CurrentRow.Index;

            string dartan = dataGridView1.Rows[i].Cells[0].Value.ToString();
            string firmtype = dataGridView1.Rows[i].Cells[14].Value.ToString();
            //MessageBox.Show(dartanum.ToString());
            banijyaletterview lett = new banijyaletterview();
            lett.MdiParent = this.MdiParent;

            lett.getdata(dartan,firmtype);
            lett.Show();
        }

        private void banijyasearchbydate_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button3.Enabled = false;
        }
    }
}
