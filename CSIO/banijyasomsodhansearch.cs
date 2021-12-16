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
    public partial class banijyasomsodhansearch : Form
    {
        public banijyasomsodhansearch()
        {
            InitializeComponent();
        }
        public string id;
        public string darta;
        public string updateid;
        public string firmi;
        public string updatetitle;
        public void getdataa(string dartas, string firmtype)
        {
            darta = dartas.ToString();
            id = firmtype.ToString();
            darta_txt.Text = darta;
            // MessageBox.Show(reg.ToString());
            //date_txt.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }
        public void DisplayData()
        {
            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }

            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT   update_banijya.updatid,    update_banijya.firmid, GetNumberToUnicode(update_banijya.firmreg) AS 'र.नं.', GetNumberToUnicode(update_banijya.decisiondate) AS 'निर्णय मिति', update_banijya.dartanum AS 'दर्ता न‌',                       setup_subcategory.subcategory_unicodename AS 'सम्शोधन शिर्षक', update_banijya.updatnew AS 'सम्शोधन पछि', update_banijya.oldrec AS 'साविक विवरण',update_banijya.updattitle FROM         update_banijya INNER JOIN                      setup_subcategory ON update_banijya.updattitle = setup_subcategory.subcategory_id where ((update_banijya.firmreg=@darta) or (GetNumberToUnicode(update_banijya.firmreg)=@darta)) AND (Getint(GetUnicodeToNumber(update_banijya.updatenepdate))>=Getint(GetUnicodeToNumber(@datefrom)) AND Getint(GetUnicodeToNumber(update_banijya.updatenepdate))<=Getint(GetUnicodeToNumber(@dateto)) ) ORDER BY dartanum DESC", sqlcon.con);
       
            
            qry.SelectCommand.Parameters.AddWithValue("@darta", darta_txt.Text);
            qry.SelectCommand.Parameters.AddWithValue("@datefrom", darta_date.Text);
            qry.SelectCommand.Parameters.AddWithValue("@dateto", dartadateto.Text);
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView1.DataSource = tb;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            //  dataGridView1.Columns[8].Visible = false;
            sqlcon.con.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void banijyasomsodhansearch_Load(object sender, EventArgs e)
        {
            updateletter_btn.Enabled = false;
            darta_date.Text = global.nepalidate;
            dartadateto.Text = global.nepalidate;
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            updateletter_btn.Enabled = true;
            int i;

            i = dataGridView1.CurrentRow.Index;
            id = (dataGridView1.Rows[i].Cells[1].Value.ToString());
            darta = (dataGridView1.Rows[i].Cells[4].Value.ToString());
            updateid = (dataGridView1.Rows[i].Cells[0].Value.ToString());
            firmi = (dataGridView1.Rows[i].Cells[2].Value.ToString());
            updatetitle = (dataGridView1.Rows[i].Cells[8].Value.ToString());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            somsodhanviewer ciname = new somsodhanviewer();
            ciname.MdiParent = this.MdiParent;
         //   ciname.getdata(id,darta);
            //this.Hide();
            ciname.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            somsodhanviewer ciname = new somsodhanviewer();
            ciname.MdiParent = this.MdiParent;
            ciname.getdata(id, darta,bodartha_txt1.Text,bodartha_txt2.Text,updatetitle);
            //this.Hide();
            ciname.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            updatebanijyarecord ciname = new updatebanijyarecord();
            ciname.MdiParent = this.MdiParent;
            ciname.getdataa(updateid,updateid);
            //this.Hide();
            ciname.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void button_bodartha_Click(object sender, EventArgs e)
        {
            bodartha_txt2.Visible = true;
            remove_bodartha.Visible = true;
           add_bodartha.Visible = false;
        }

        private void remove_bodartha_Click(object sender, EventArgs e)
        {
            bodartha_txt2.Text = "";
            bodartha_txt2.Visible = false;
            remove_bodartha.Visible = false;
            add_bodartha.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
