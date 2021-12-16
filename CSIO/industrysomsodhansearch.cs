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
    public partial class industrysomsodhansearch : Form
    {
        public industrysomsodhansearch()
        {
            InitializeComponent();
        }
        public string id;
        public string darta;
        public string updateid;
        public void DisplayData()
        {
            try
            {
                //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }

                MySqlDataAdapter qry = new MySqlDataAdapter("SELECT  update_industry.updatid,   update_industry.industryid, GetNumberToUnicode(update_industry.industryreg) AS 'र.नं.', GetNumberToUnicode(update_industry.decisiondate) AS 'संशोधन निर्णय मिति', update_industry.dartanum AS 'दर्ता न‌ं',                       setup_subcategory.subcategory_unicodename AS 'सम्शोधन शिर्षक',update_industry.oldrec AS 'साविक विवरण', update_industry.updatnew AS 'सम्शोधन पछि',                        update_industry.updatenepdate AS 'निर्णय मिति' FROM         update_industry INNER JOIN                      setup_subcategory ON update_industry.updattitle = setup_subcategory.subcategory_id where ((update_industry.industryreg=@darta) or (GetNumberToUnicode(update_industry.industryreg)=@darta)) AND (Getint(GetUnicodeToNumber(update_industry.updatenepdate))>=Getint(GetUnicodeToNumber(@datefrom)) AND Getint(GetUnicodeToNumber(update_industry.updatenepdate))<=Getint(GetUnicodeToNumber(@dateto)) ) ORDER BY dartanum DESC", sqlcon.con);
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
                sqlcon.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            DisplayData();
        }
        public void getdataa(string dartas, string firmtype)
        {
            darta = dartas.ToString();
            id = firmtype.ToString();
            darta_txt.Text = darta;
            // MessageBox.Show(reg.ToString());
            //date_txt.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }
        private void industrysomsodhansearch_Load(object sender, EventArgs e)
        {
            button9.Enabled = false;
            darta_date.Text = global.nepalidate;
            dartadateto.Text = global.nepalidate;

            //Window State
            this.WindowState = FormWindowState.Normal;
            //POSITION and SIZE
            this.Left = (this.Parent.Width / 2) - (this.Width / 2) - 5;
            this.Top = 0;
            //border
            global.createBorderAround(this, Color.Teal, 2);
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            button9.Enabled = true;
            int i;

            i = dataGridView1.CurrentRow.Index;
            updateid = (dataGridView1.Rows[i].Cells[0].Value.ToString());
            id = (dataGridView1.Rows[i].Cells[1].Value.ToString());
            darta = (dataGridView1.Rows[i].Cells[4].Value.ToString());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            industrysamsodhanviewr ciname = new industrysamsodhanviewr();
            ciname.MdiParent = this.MdiParent;
            ciname.getdata(id, darta,bodartha_txt1.Text,bodartha_txt2.Text);
            //this.Hide();
            ciname.Show();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (int.Parse(global.usertype) > 1)
            {
                updateindustryrecord ciname = new updateindustryrecord();
                ciname.MdiParent = this.MdiParent;
                ciname.getdata(updateid, id);
                //this.Hide();
                ciname.Show();
            }
            else
            {
                MessageBox.Show("बिवरण संशोधन अधिकार तपाईलाई नभएकोले यो विवरण विवरण संशोधन कार्य सफल हुन सकेन । ", "अपर्याप्त अधिकार", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SearchIndustryCertLetter ift = new SearchIndustryCertLetter();
            ift.MdiParent = this.MdiParent;
            ift.Show();

            //try
            //{
            //    int i;

            //    i = dataGridView1.CurrentRow.Index;

            //    string dartanum = dataGridView1.Rows[i].Cells[4].Value.ToString();
            //    //MessageBox.Show(dartanum.ToString());
            //    reportviewer bd = new reportviewer();
            //    bd.MdiParent = this.MdiParent;

            //    // bd.getdataa(dartanum);


            //    bd.Show();
            //}
            //catch (Exception fes)
            //{
            //    MessageBox.Show(fes.ToString());
            //}


        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void add_bodartha_Click(object sender, EventArgs e)
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
