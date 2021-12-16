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
    public partial class industryrenew : Form
    {
        public industryrenew()
        {
            InitializeComponent();
        }
        public void getid(int indusid, string trans,string regnos)
        {
            label5.Text = null;
            label5.Text = indusid.ToString();
            darta_no.Text = null;
            darta_no.Text = trans.ToString();
            regno.Text = regnos.ToString();



            //date_txt.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }
        public void Cleardata()
        {
            renewdate.Text = null;
            tax.Text = null;
            comment.Text = null;
            darta_no.Text = null;
        }
        public void DisplayData()
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }

            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

         //   con.Open();

            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     industryreg.industryid, GetNumberToUnicode(industryreg.industryregno) AS 'र.नं', industryreg.industrynepname AS 'उद्योगको नाम' , GetNumberToUnicode(industryreg.renewdate) As 'नविकरण मिति',setup_district.distunicodename AS 'जिल्ला',setup_vdc.vdcunicodename As 'गा.पा।न.पा.',GetNumberToUnicode(industryreg.industryward) As 'वार्ड' From  industryreg INNER JOIN      setup_district ON industryreg.industrydist = setup_district.distcode INNER JOIN       setup_vdc ON industryreg.industryvdc = setup_vdc.VDC_SID where industryreg.industryregno=GetNumberToUnicode(@darta) OR industryreg.industryregno=GetUnicodeToNumber(@darta) ", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@darta", regno.Text);
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView1.DataSource = tb;
            dataGridView1.Columns[0].Visible = false;
            sqlcon.con.Close();

        }
       

        private void industryrenew_Load(object sender, EventArgs e)
        {
            button4.Enabled = false;
            d_date.Text = global.todaynepslash;
           // user_txt.Text = global.username;
            DisplayData();
            groupBox1.Enabled = false;
            button1.Enabled = false;

            //Window State
            this.WindowState = FormWindowState.Normal;

            //POSITION and SIZE
            this.Left = (this.Parent.Width / 2) - (this.Width / 2) - 5;
            this.Top = 0;
            //border
            global.createBorderAround(this, Color.Teal, 2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
            button1.Enabled = true;
            button1.Enabled = true;
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            button4.Enabled = true;
        }
        public void transactionid()
        {
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }

                MySqlCommand cmd = new MySqlCommand("select fy,transnum from setup_transactionnumber where transid=1", sqlcon.con);
                cmd.CommandType = CommandType.Text;
                // cmd.Connection = con;
                MySqlDataReader sdr = cmd.ExecuteReader();

                sdr.Read();
                darta_no.Text = global.csioid.ToString().Trim() + sdr["fy"].ToString().Trim() + sdr["transnum"].ToString().Trim();


                sqlcon.con.Close();
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                MySqlCommand cmo = new MySqlCommand("update setup_transactionnumber SET transnum= transnum+1 WHERE transid=1", sqlcon.con);
                cmo.CommandType = CommandType.Text;
                //  cmo.Parameters.AddWithValue("@firmtype", firmtype.ToString());
                // int tid = Convert.ToInt32(cmo.ExecuteScalar());
                // cmo.Connection = sqlcon.con;


                //int n = qr.ExecuteNonQuery();

                int n = cmo.ExecuteNonQuery();
                // ind_type.Text = tid.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(darta_no.Text=="" || darta_no.Text==null)
            { 
                transactionid();
                //MessageBox.Show("TRNASFIRE");
            }
            
            int i;
            i = dataGridView1.CurrentRow.Index;
           string industryid = (dataGridView1.Rows[i].Cells[0].Value.ToString());
            string regno = dataGridView1.Rows[i].Cells[1].Value.ToString();
            string statvalue = dataGridView1.Rows[i].Cells[3].Value.ToString();


            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            
            //con.Open();
            ///////////////////////////////////////////////////////////



            //SqlCommand qr = new SqlCommand("INSERT INTO update_industry (industryid,industryreg ,dartanum,updattitle,updatnew,oldrec,[updatedate],updatenepdate,updateuser) VALUES (@industryid, @industryregno, @dartanum, @updatetitle, @updatenew, @oldvalue, @updatedate, @updatenepdate, @updateuser)");
            MySqlCommand qr = new MySqlCommand("INSERT INTO industryrenew (industryid,industryreg ,dartanum,updattitle,updatnew,oldrec,updatenepdate,updateuser,decisiondate,tax,comment) VALUES (@industryid, GetUnicodeToNumber(@industryregno), @dartanum, @updatetitle, GetUnicodeToNumber(@updatenew), GetUnicodeToNumber(@oldvalue), GetUnicodeToNumber(@updatenepdate), @updateuser, GetUnicodeToNumber(@ddate), GetUnicodeToNumber(@tax), @comment)");

            qr.CommandType = CommandType.Text;
            qr.Parameters.AddWithValue("@industryid", industryid.ToString());
            qr.Parameters.AddWithValue("@industryregno", regno.ToString());
            qr.Parameters.AddWithValue("@dartanum", darta_no.Text);
            qr.Parameters.AddWithValue("@updatetitle", updateid.Text);
            qr.Parameters.AddWithValue("@tax", tax.Text);
            //qr.Parameters.AddWithValue("@updatenew", textBox1.Text);
            //qr.Parameters.AddWithValue("@oldvalue", statvalue.ToString());

            qr.Parameters.AddWithValue("@updatenew", renewdate.Text);
            qr.Parameters.AddWithValue("@oldvalue", statvalue.ToString());

            //qr.Parameters.AddWithValue("@transid", updateid.Text);
            //// qr.Parameters.AddWithValue("@updatedate", curdate.ToString());
            qr.Parameters.AddWithValue("@updatenepdate", global.nepalidate);
            qr.Parameters.AddWithValue("@updateuser", global.username);
            qr.Parameters.AddWithValue("@ddate", d_date.Text);
            qr.Parameters.AddWithValue("@comment", comment.Text);


            qr.Connection = sqlcon.con;


            int k = qr.ExecuteNonQuery();

            if (k > 0)
            {

                //sqlcon.con.Close();
                // dataGridView1.DataSource = null;
                // DisplayData();
                // cleardata();
                MySqlCommand qrs = new MySqlCommand("UPDATE industryreg  SET renewdate = GetUnicodeToNumber(@txtbxthree)   WHERE industryid=@indid");


                qrs.CommandType = CommandType.Text;


                qrs.Parameters.AddWithValue("@txtbxthree", renewdate.Text);
                // qrs.Parameters.AddWithValue("@txtbxfour", textBox2.Text);

                // qrs.Parameters.AddWithValue("@comment", textBox4.Text);
                qrs.Parameters.AddWithValue("@indid", industryid.ToString());
                //qr.Parameters.AddWithValue("@currentdate", curdate);
                //qr.Parameters.AddWithValue("@nepdate", entry_nepdate.Text);
                //qr.Parameters.AddWithValue("@female", fworker.Text);
                //qr.Parameters.AddWithValue("@male", maleworker.Text);
                //qr.Parameters.AddWithValue("@taxs", tax_txt.Text);
                qrs.Connection = sqlcon.con;


                //int n = qr.ExecuteNonQuery();
                int n = qrs.ExecuteNonQuery();

                if (n > 0)
                {
                    global.sync_dblog("industryreg", industryid.ToString(), "UPDATE", darta_no.Text,"industryid");
                    global.sync_dblog("industryrenew", industryid.ToString(), "INSERT", darta_no.Text,"dartanum");
                    sqlcon.con.Close();
                    dataGridView1.DataSource = null;
                    DisplayData();

                    MessageBox.Show("उद्योगको नविकरण र्संशोधन सफल भयो ।", "संशोधन", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    Cleardata();
                    button1.Enabled = false;
                    groupBox1.Enabled = false;
                    //button9.Enabled = false;
                    //button12.Enabled = false;
                    //panel2.Enabled = false;


                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cleardata();
        }

        private void regno_Leave(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                int i;
                if (dataGridView1.Rows.Count > 0 && dataGridView1.Rows != null && (dataGridView1.SelectedRows.Count > 0))
                {

                    i = dataGridView1.CurrentRow.Index;

                    string dartan = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    //MessageBox.Show(dartanum.ToString());
                    letterview lt = new letterview();
                    lt.MdiParent = this.MdiParent;

                    lt.getdataa(dartan, "", "");
                    lt.Show();
                }
                else
                {
                    MessageBox.Show("कृपया रेकर्ड छान्नुहोला ।");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
