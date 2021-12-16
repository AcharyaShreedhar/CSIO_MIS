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
    public partial class industryclose : Form
    {
        public industryclose()
        {
            InitializeComponent();
        }
        public void getid(int indusid, string trans, string regnos)
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
            // con.Open();

            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     industryreg.industryid, GetNumberToUnicode(industryreg.industryregno) AS 'र.नं', industryreg.industrynepname AS 'उद्योगको नाम' , GetNumberToUnicode(industryreg.renewdate) As 'नविकरण मिति',setup_district.distunicodename AS 'जिल्ला',setup_vdc.vdcunicodename As 'गा.पा।न.पा.',GetNumberToUnicode(industryreg.industryward) As 'वार्ड' From  industryreg INNER JOIN      setup_district ON industryreg.industrydist = setup_district.distcode INNER JOIN       setup_vdc ON industryreg.industryvdc = setup_vdc.VDC_SID where industryreg.industryregno=GetNumberToUnicode(@darta) OR industryreg.industryregno=GetUnicodeToNumber(@darta) ", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@darta", regno.Text);
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView1.DataSource = tb;
            dataGridView1.Columns[0].Visible = false;
            sqlcon.con.Close();
        }
       

        private void industryclose_Load(object sender, EventArgs e)
        {
            button4.Enabled = false;
            d_date.Text = global.nepalidate;
            renewdate.Text = global.nepalidate;
            user_txt.Text = global.username;
            DisplayData();
            groupBox1.Enabled = false;
            button1.Enabled = false;
            button5.Enabled = false;

            //Window State
            this.WindowState = FormWindowState.Normal;

            //POSITION and SIZE
            this.Left = (this.Parent.Width / 2) - (this.Width / 2) - 5;
            this.Top = 0;
            //border
            global.createBorderAround(this, Color.Teal, 2);
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
        private void button4_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
          //  transactionid();
            int i;
            i = dataGridView1.CurrentRow.Index;
            string industryid = (dataGridView1.Rows[i].Cells[0].Value.ToString());
            label5.Text = industryid.ToString();
            string regno = dataGridView1.Rows[i].Cells[1].Value.ToString();
            string statvalue = dataGridView1.Rows[i].Cells[3].Value.ToString();
            button1.Enabled = true;
            button5.Enabled = true;
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
            try
            {
                if (darta_no.Text == "" || darta_no.Text == null)
                {
                    transactionid();
                    //MessageBox.Show("TRNASFIRE");
                }
                //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }

                MySqlCommand qr = new MySqlCommand("INSERT INTO industryclose (industryid ,dartanum,updattitle,applicationdate,updatenepdate,updateuser,decisiondate,tax,comment,csioid) VALUES (@industryid, @dartanum, @updatetitle, @updatenew, GetUnicodeToNumber(@updatenepdate), @updateuser, @ddate, @tax, @comment,@csioid)");

                qr.CommandType = CommandType.Text;
                qr.Parameters.AddWithValue("@industryid", label5.Text);

                qr.Parameters.AddWithValue("@dartanum", darta_no.Text);
                qr.Parameters.AddWithValue("@updatetitle", updateid.Text);
                qr.Parameters.AddWithValue("@tax", textBox2.Text);
                qr.Parameters.AddWithValue("@csioid", global.csioid);
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


                int k = qr.ExecuteNonQuery();
                global.sync_dblog("industryclose", label5.Text, "INSERT", label5.Text, "industryid");
              
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

                MySqlCommand qrh = new MySqlCommand("INSERT INTO owner_hist_industry(ownerid, industryid ,industryregno, ccio, cddist, citzdate, citznum, gender, ownerfname, ownerlname, ownerdistcode, ownervdccode, ownervdcward, ownertole, ownergrel, ownerfgrel, ownergname, ownerfgname, comment, upddate, updnepdate, upduser,transid) SELECT ownerid,industryid,industryregno,ccio,cddist,citzdate,citznum,gender,ownerfname,ownerlname,ownerdistcode,ownervdccode,ownervdcward,ownertole,ownergrel,ownerfgrel,ownergname,ownerfgname,comment,upddate,updnepdate,upduser,@transid FROM owner_industry WHERE industryid=@ownerid");




                qrh.CommandType = CommandType.Text;
                qrh.Parameters.AddWithValue("@ownerid", label5.Text);

                qrh.Parameters.AddWithValue("@transid", updateid.Text);

                qrh.Connection = sqlcon.con;
                int nh = qrh.ExecuteNonQuery();

                if (nh > 0)
                {
                    global.sync_dblog("owner_hist_industry", label5.Text, "INSERT", label5.Text, "industryid");
                    // Remaining to edit 
                    MySqlCommand qrform = new MySqlCommand("INSERT INTO industryreg_hist(industryid,industryregno,regdat ,renewdate ,industryscale,industrytype ,industrycat ,industrynepname ,branch  ,industrydist ,industryvdc,industryward ,tole ,karobar,yearlyturnover ,electricpower   ,statcapital,varcapital,comment ,femaleworker ,maleworker ,tax ,usr ,updatdate  ,updatnepdate ,decisionnepdate  ,updateuser,transid,officedist,csioid) SELECT industryid ,industryregno ,regdat ,renewdate ,industryscale ,industrytype  ,industrycat   ,industrynepname  ,branch ,industrydist ,industryvdc ,industryward  ,tole ,karobar  ,yearlyturnover  ,electricpower  ,statcapital ,varcapital  ,comment ,femaleworker ,maleworker  ,tax  ,usr  ,updatdate  ,updatnepdate,@decisionnepdate,@user,@transid,officedist,csioid   FROM industryreg where industryid=@firmid");

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
                        global.sync_dblog("industryreg_hist", label5.Text, "INSERT", label5.Text, "industryid");
                        MySqlCommand qre = new MySqlCommand("DELETE FROM owner_industry WHERE industryid=@ownerid");




                        qre.CommandType = CommandType.Text;
                        qre.Parameters.AddWithValue("@ownerid", label5.Text);
                        qre.Connection = sqlcon.con;
                        int nht = qre.ExecuteNonQuery();
                        global.sync_dblog("owner_industry", label5.Text, "DELETE", label5.Text, "industryid");
                        MySqlCommand qrclose = new MySqlCommand(" DELETE FROM industryreg  WHERE  industryid=@ownerid");




                        qrclose.CommandType = CommandType.Text;
                        qrclose.Parameters.AddWithValue("@ownerid", label5.Text);
                        qrclose.Connection = sqlcon.con;
                        int nhee = qrclose.ExecuteNonQuery();

                        global.sync_dblog("industryreg", label5.Text, "DELETE", label5.Text, "industryid");
                        if (nht > 0 && nhee > 0)
                        {


                            sqlcon.con.Close();
                            dataGridView1.DataSource = null;
                            //DisplayData();
                            //cleardata();
                            MessageBox.Show("उद्योगको लगतकट्टा सफल भयो ।", "लगतकट्टा", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            //button9.Enabled = false;
                            //button12.Enabled = false;
                            //panel2.Enabled = false;


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int j;

                j = dataGridView1.CurrentRow.Index;

                id_label.Text = dataGridView1.Rows[j].Cells[0].Value.ToString();
                sanakhatlagatkattapreview ciname = new sanakhatlagatkattapreview();
                ciname.MdiParent = this.MdiParent;
                ciname.getdata(label5.Text);
                //this.Hide();
                ciname.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void regno_Leave(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
