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
    public partial class changebanijyaname : Form
    {
        public changebanijyaname()
        {
            InitializeComponent();
        }
        public void getid(int indusid, string trans,string ddate)
        {
            label5.Text = null;
            label5.Text = indusid.ToString();
            darta_no.Text = trans.ToString();
            updateid.Text = null;
            d_date.Text = ddate.ToString();



            //date_txt.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }
        public void Cleardata()
        {
            textBox1.Text = null;
            textBox2.Text = null;
            textBox4.Text = null;
            darta_no.Text = null;
        }
        public void DisplayData()
        {
            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }

            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     banijyaform.firmid, GetNumberToUnicode(banijyaform.firmregno) AS 'र.नं', banijyaform.firmnepname AS 'फर्मको नाम'  From  banijyaform where banijyaform.firmid=@darta ", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@darta", label5.Text);
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView1.DataSource = tb;
            dataGridView1.Columns[0].Visible = false;
            sqlcon.con.Close();

        }
        private void changebanijyaname_Load(object sender, EventArgs e)
        {
            d_date.Text = global.todaynepslash;
            user_txt.Text = global.username;
            DisplayData();
            groupBox1.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i;
            i = dataGridView1.CurrentRow.Index;
            int industryid = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
            string regno = dataGridView1.Rows[i].Cells[1].Value.ToString();
            string statvalue = dataGridView1.Rows[i].Cells[2].Value.ToString();


            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            ///////////////////////////////////////////////////////////



            //SqlCommand qr = new SqlCommand("INSERT INTO update_industry (industryid,industryreg ,dartanum,updattitle,updatnew,oldrec,[updatedate],updatenepdate,updateuser) VALUES (@industryid, @industryregno, @dartanum, @updatetitle, @updatenew, @oldvalue, @updatedate, @updatenepdate, @updateuser)");
            MySqlCommand qr = new MySqlCommand("INSERT INTO update_banijya (firmid,firmreg ,dartanum,updattitle,updatnew,oldrec,updatenepdate,updateuser,decisiondate,tax,comment) VALUES (@industryid, GetUnicodeToNumber(@industryregno), GetUnicodeToNumber(@dartanum), @updatetitle, @updatenew, @oldvalue, GetUnicodeToNumber(@updatenepdate), @updateuser, GetUnicodeToNumber(@ddate), GetUnicodeToNumber(@tax), @comment)");

            qr.CommandType = CommandType.Text;
            qr.Parameters.AddWithValue("@industryid", industryid.ToString());
            qr.Parameters.AddWithValue("@industryregno", regno.ToString());
            qr.Parameters.AddWithValue("@dartanum", darta_no.Text);
            qr.Parameters.AddWithValue("@updatetitle", updateid.Text);
            qr.Parameters.AddWithValue("@tax", textBox2.Text);
            //qr.Parameters.AddWithValue("@updatenew", textBox1.Text);
            //qr.Parameters.AddWithValue("@oldvalue", statvalue.ToString());

            qr.Parameters.AddWithValue("@updatenew", textBox1.Text);
            qr.Parameters.AddWithValue("@oldvalue", statvalue.ToString());

            //qr.Parameters.AddWithValue("@transid", updateid.Text);
            //// qr.Parameters.AddWithValue("@updatedate", curdate.ToString());
            qr.Parameters.AddWithValue("@updatenepdate", global.nepalidate);
            qr.Parameters.AddWithValue("@updateuser", user_txt.Text);
            qr.Parameters.AddWithValue("@ddate", d_date.Text);
            qr.Parameters.AddWithValue("@comment", textBox4.Text);


            qr.Connection = sqlcon.con;


            int k = qr.ExecuteNonQuery();

            if (k > 0)
            {

                //sqlcon.con.Close();
                // dataGridView1.DataSource = null;
                // DisplayData();
                // cleardata();
                MySqlCommand qrs = new MySqlCommand("UPDATE banijyaform  SET firmnepname = @txtbxthree   WHERE firmid=@indid");


                qrs.CommandType = CommandType.Text;


                qrs.Parameters.AddWithValue("@txtbxthree", textBox1.Text);
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

                    sqlcon.con.Close();
                    dataGridView1.DataSource = null;
                    DisplayData();
                
                    MessageBox.Show("फमको नाम र्संशोधन सफल भयो ।", "संशोधन", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    Cleardata();
                    //button9.Enabled = false;
                    //button12.Enabled = false;
                    //panel2.Enabled = false;


                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox2.Text = null;
            textBox4.Text = null;
        }
    }
}
