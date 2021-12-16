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
    public partial class banijyafirmclose : Form
    {
        public banijyafirmclose()
        {
            InitializeComponent();
        }
        public void getid(int indusid,string trans,string decisiondt)
        {
            label5.Text = null;
            label5.Text = indusid.ToString();
            darta_no.Text = trans.ToString();
            d_date.Text = decisiondt.ToString();
          //  updateid.Text = null;


            //date_txt.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }
        public void Cleardata()
        {
            renewdate.Text = null;
            textBox2.Text = null;
            textBox4.Text = null;
            darta_no.Text = null;
        }
        public void DisplayData()
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }

            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     banijyaform.firmid, GetNumberToUnicode(banijyaform.firmregno) AS 'र.नं', banijyaform.firmnepname AS 'फर्मको नाम' ,GetNumberToUnicode(banijyaform.renewdate) As 'नविकरण मिति' From  banijyaform where banijyaform.firmid=@darta ", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@darta", label5.Text);
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView1.DataSource = tb;
            dataGridView1.Columns[0].Visible = false;
            sqlcon.con.Close();

        }
       

        private void banijyafirmclose_Load(object sender, EventArgs e)
        {
            button4.Enabled = false;
            d_date.Text = global.todaynepslash;
            user_txt.Text = global.username;
            DisplayData();
            groupBox1.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cleardata();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            button4.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            MySqlCommand qr = new MySqlCommand("INSERT INTO banijyaclose (firmid ,dartanum,updattitle,applicationdate,updatenepdate,updateuser,decisiondate,tax,comment) VALUES (@industryid, GetUnicodeToNumber(@dartanum), @updatetitle, @updatenew, GetUnicodeToNumber(@updatenepdate), @updateuser, GetUnicodeToNumber(@ddate), GetUnicodeToNumber(@tax), @comment)");

            qr.CommandType = CommandType.Text;
            qr.Parameters.AddWithValue("@industryid", label5.Text);
           
            qr.Parameters.AddWithValue("@dartanum", darta_no.Text);
            qr.Parameters.AddWithValue("@updatetitle", updateid.Text);
            qr.Parameters.AddWithValue("@tax", textBox2.Text);
            //qr.Parameters.AddWithValue("@updatenew", textBox1.Text);
            //qr.Parameters.AddWithValue("@oldvalue", statvalue.ToString());

            qr.Parameters.AddWithValue("@updatenew", renewdate.Text);
           

            //qr.Parameters.AddWithValue("@transid", updateid.Text);
            //// qr.Parameters.AddWithValue("@updatedate", curdate.ToString());
            qr.Parameters.AddWithValue("@updatenepdate", global.nepalidate);
            qr.Parameters.AddWithValue("@updateuser", user_txt.Text);
            qr.Parameters.AddWithValue("@ddate", d_date.Text);
            qr.Parameters.AddWithValue("@comment", textBox4.Text);


            qr.Connection = sqlcon.con;
            global.sync_dblog("banijyaclose", label5.Text, "INSERT", label5.Text, "firmid");

            int k = qr.ExecuteNonQuery();
            //MySqlCommand command;
            //MySqlDataAdapter adapter = new MySqlDataAdapter();
            //DataSet ds = new DataSet();
            //string sql = "SELECT   ownerid from owner where firmid='" + label5.Text + "' ";

            ////string sql = "SELECT VdcCode, VdcNepnam FROM setup_vdc where setup_vdc.DistCode='" + comboBox3.ValueMember + "'";



            //command = new SqlCommand(sql, con);
            //adapter.SelectCommand = command;
            //adapter.Fill(ds);
            //adapter.Dispose();
            //command.Dispose();


            //foreach (DataRow row in ds.Tables[0].Rows)
            //{
            //    firmi = row[0].ToString();

            //    // MessageBox.Show(firmi.ToString());
            //}

            MySqlCommand qrh = new MySqlCommand("INSERT INTO owner_hist_banijya(ownerid, firmid ,firmregno, ccio, cddist, citzdate, citznum, gender, ownerfname, ownerlname, ownerdistcode, ownervdccode, ownervdcward, ownertole, ownergrel, ownerfgrel, ownergname, ownerfgname, comment, upddate, updnepdate, upduser,transid) SELECT ownerid,firmid,firmregno,ccio,cddist,citzdate,citznum,gender,ownerfname,ownerlname,ownerdistcode,ownervdccode,ownervdcward,ownertole,ownergrel,ownerfgrel,ownergname,ownerfgname,comment,upddate,updnepdate,upduser,@transid FROM owner WHERE firmid=@ownerid");




            qrh.CommandType = CommandType.Text;
            qrh.Parameters.AddWithValue("@ownerid", label5.Text);
            
            qrh.Parameters.AddWithValue("@transid", updateid.Text);
          
            qrh.Connection = sqlcon.con;
            int nh = qrh.ExecuteNonQuery();

            if (nh > 0)
            {
                global.sync_dblog("owner_hist_banijya", label5.Text, "INSERT", label5.Text, "ownerid");
                MySqlCommand qrform = new MySqlCommand("INSERT INTO banijyaform_hist(firmid,firmregno,firmtype,firmdist,firmvdc,firmward,tole,regdat ,renewdate,firmscope,firmnepname,karobar ,revenue,tax,branch,comment,usr,updatdate,updatnepdate,karobartype,decisionnepdate,updateuser,transid) SELECT firmid,firmregno,firmtype,firmdist,firmvdc,firmward,tole,regdat,renewdate,firmscope,firmnepname,karobar,revenue,tax,branch,comment,usr,updatdate ,updatnepdate ,karobartype,GetUnicodeToNumber(@decisionnepdate),@user,@transid FROM banijyaform where firmid=@firmid");
               
                qrform.CommandType = CommandType.Text;
                qrform.Parameters.AddWithValue("@firmid", label5.Text);
               // qrform.Parameters.AddWithValue("@firmid", id_label.Text);
                qrform.Parameters.AddWithValue("@transid", updateid.Text);
                qrform.Parameters.AddWithValue("@user", user_txt.Text);
                qrform.Parameters.AddWithValue("@decisionnepdate", d_date.Text);

                qrform.Connection = sqlcon.con;
                int qrf = qrform.ExecuteNonQuery();

                if (qrf > 0)
                {
                    global.sync_dblog("banijyaform_hist", label5.Text, "INSERT", label5.Text, "firmid");

                    MySqlCommand qre = new MySqlCommand("DELETE FROM owner WHERE firmid=@ownerid");




                    qre.CommandType = CommandType.Text;
                    qre.Parameters.AddWithValue("@ownerid", label5.Text);
                    qre.Connection = sqlcon.con;
                    int nht = qre.ExecuteNonQuery();
                    global.sync_dblog("owner", label5.Text, "DELETE", label5.Text, "firmid");
                    MySqlCommand qrclose = new MySqlCommand(" DELETE FROM banijyaform  WHERE  firmid=@ownerid");




                    qrclose.CommandType = CommandType.Text;
                    qrclose.Parameters.AddWithValue("@ownerid", label5.Text);
                    qrclose.Connection = sqlcon.con;
                    int nhee = qrclose.ExecuteNonQuery();
                    global.sync_dblog("banijyaform", label5.Text, "DELETE", label5.Text, "firmid");


                    if (nht > 0 && nhee > 0)
                    {


                        sqlcon.con.Close();
                        dataGridView1.DataSource = null;
                        //DisplayData();
                        //cleardata();
                        MessageBox.Show("वाणिज्य फर्मको लगतकट्टा सफल भयो ।", "लगतकट्टा", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        //button9.Enabled = false;
                        //button12.Enabled = false;
                        //panel2.Enabled = false;


                    }
                }
            }
        }

        private void darta_no_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
                    }
    }
}
