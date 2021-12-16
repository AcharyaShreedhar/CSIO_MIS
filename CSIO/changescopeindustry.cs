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
    public partial class changescopeindustry : Form
    {
        public changescopeindustry()
        {
            InitializeComponent();
        }

        private void changescopeindustry_Load(object sender, EventArgs e)
        {
           // d_date.Text = global.todaynepslash;
            user_txt.Text = global.username;
            DisplayData();
            groupBox1.Enabled = false;
            button4.Enabled = false;

        }
        public void getid(int indusid, string trans,string ddate)
        {
            label5.Text = null;
            label5.Text = indusid.ToString();
            darta_no.Text = null;
            darta_no.Text = trans.ToString();
            d_date.Text = ddate.ToString();



            //date_txt.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }
        public void DisplayData()
        {
            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }

            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     industryreg.industryid, GetNumberToUnicode(industryreg.industryregno) AS 'दर्ता नं.', industryreg.industrynepname AS 'उद्योगको नाम', industryreg.karobar As 'उद्देश्य' From  industryreg where industryreg.industryid=@darta ", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@darta", label5.Text);
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView1.DataSource = tb;
            dataGridView1.Columns[0].Visible = false;
            sqlcon.con.Close();

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
            try
            {
                int i;
                i = dataGridView1.CurrentRow.Index;
                int industryid = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                string regno = dataGridView1.Rows[i].Cells[1].Value.ToString();
                string statvalue = dataGridView1.Rows[i].Cells[3].Value.ToString();


                //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                ///////////////////////////////////////////////////////////






                //MySqlCommand command;
                //MySqlDataAdapter adapter = new MySqlDataAdapter();
                //DataSet ds = new DataSet();

                //string sql = null;

                //sql = "select * from industry_prev where industryid=@firmname";



                //    command = new SqlCommand(sql, con);
                //    adapter.SelectCommand = command;
                //    command.CommandType = CommandType.Text;

                //    command.Parameters.AddWithValue("@firmname", label5.Text);
                //    adapter.Fill(ds);
                //    adapter.Dispose();
                //    command.Dispose();

                //    int i = 0;
                //        i= ds.Tables[0].Rows.Count;
                //    //MessageBox.Show("Total Row" + i.ToString());


                //    //string curdate = DateTime.Today.ToString();
                //    //SqlCommand qrs = new SqlCommand("select count(*) from industryreg where industrynepname=@firmname");

                //    //MessageBox.Show(qrs.ToString());
                //    //    qrs.CommandType = CommandType.Text;

                //    //    qrs.Parameters.AddWithValue("@firmname", firm_name.Text.Trim());
                //    //    qrs.Connection = con;


                //    //    int n = qrs.ExecuteNonQuery();
                //    //   // reader = qrs.ExecuteReader();
                //    //   // MessageBox.Show(reader.HasRows.ToString());
                //    //DataTable dt = new DataTable();



                //        if (i > 0)
                //        {



                //            MessageBox.Show("उद्योगको नाम पहिले नै इन्टी भइ सकेको ले कृपया अर्को नाम इन्टी गर्नुहोस ।", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);


                //        }
                //        else
                //        {
                //        }



                /////////////////////////////////////////

                // string curdate = DateTime.Today.ToString();

                //SqlCommand qr = new SqlCommand("INSERT INTO update_industry (industryid,industryreg ,dartanum,updattitle,updatnew,oldrec,[updatedate],updatenepdate,updateuser) VALUES (@industryid, @industryregno, @dartanum, @updatetitle, @updatenew, @oldvalue, @updatedate, @updatenepdate, @updateuser)");
                MySqlCommand qr = new MySqlCommand("INSERT INTO update_industry (industryid,industryreg ,dartanum,updattitle,updatnew,oldrec,updatenepdate,updateuser,decisiondate,tax,comment) VALUES (@industryid, @industryregno, @dartanum, @updatetitle, @updatenew, @oldvalue, @updatenepdate, @updateuser, @ddate, GetUnicodeToNumber(@tax), @comment)");

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
                    MySqlCommand qrs = new MySqlCommand("UPDATE industryreg  SET karobar = @txtbxthree   WHERE industryid=@indid");


                    qrs.CommandType = CommandType.Text;


                    qrs.Parameters.AddWithValue("@txtbxthree", textBox1.Text);
                    //  qrs.Parameters.AddWithValue("@txtbxfour", textBox2.Text);

                    //  qrs.Parameters.AddWithValue("@comment", textBox4.Text);
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
                        // cleardata();
                        MessageBox.Show("संशोधन सफल भयो ।", "संशोधन", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        //button9.Enabled = false;
                        //button12.Enabled = false;
                        //panel2.Enabled = false;


                    }

                }
            }
            catch (Exception ex)
              {
                  MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox2.Text = null;
            textBox4.Text = null;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            button4.Enabled = true;
        }

    }
}
