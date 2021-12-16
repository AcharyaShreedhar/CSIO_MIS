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
    public partial class banijyasamsodhan : Form
    {
        public banijyasamsodhan()
        {
            InitializeComponent();
        }
        public int id;
        public void DisplayData()
        {
            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }

            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     banijyaform.firmid, GetNumberToUnicode(banijyaform.firmregno) AS 'र.नं', concat(banijyaform.firmnepname,'(',banijyaform.firmengname,')') AS 'फर्मको नाम', setup_district.distunicodename AS 'जिल्ला',           setup_vdc.vdcunicodename AS 'गा.पा.। न.पा.', GetNumberToUnicode(banijyaform.firmward) AS 'वार्ड', banijyaform.tole AS 'टोल',setup_subcategory.subcategory_unicodename AS 'प्रकार',  GetNumberToUnicode(banijyaform.regdat) AS 'दर्ता मिति',  GetNumberToUnicode(banijyaform.renewdate) AS 'नविकरण', setup_subcategory_1.subcategory_unicodename AS 'उद्देश्य', banijyaform.karobar AS 'कारोवार बस्तुहरु', GetNumberToUnicode(banijyaform.revenue) AS 'पुँजी',  GetNumberToUnicode(banijyaform.tax) AS 'राजश्व',banijyaform.branch AS 'शाखा',  setup_subcategory_2.subcategory_unicodename AS 'कारोवार', GetNumberToUnicode(banijyaform.thelino) AS 'ठेली न‌', GetNumberToUnicode(banijyaform.fileno) AS 'फाइल न‌', GetNumberToUnicode(banijyaform.fiscalyear) AS 'आ.व', banijyaform.comment AS 'कैफियत',banijyaform.firmengname,banijyaform.firmnepname AS 'फर्मको नाम',setup_darturtype.taxtypename As 'रसिद प्रकार', billno As 'रसिद नं',banijyaform.firmtype As 'Firm Type' FROM         banijyaform INNER JOIN    setup_subcategory ON banijyaform.firmtype = setup_subcategory.subcategory_id INNER JOIN     setup_district ON banijyaform.firmdist = setup_district.distcode INNER JOIN    setup_vdc ON banijyaform.firmvdc = setup_vdc.VDC_SID INNER JOIN  setup_subcategory AS setup_subcategory_1 ON banijyaform.firmscope = setup_subcategory_1.subcategory_id  INNER JOIN   setup_subcategory AS setup_subcategory_2 ON banijyaform.karobartype = setup_subcategory_2.subcategory_id INNER JOIN setup_darturtype ON setup_darturtype.taxtypeid=banijyaform.billtype where (GetNumberToUnicode(firmregno)=GetNumberToUnicode(@darta) or (firmregno)=@darta)", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@darta", darta_txt.Text);
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView1.DataSource = tb;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[20].Visible = false;
            dataGridView1.Columns[21].Visible = false;
            dataGridView1.Columns[24].Visible = false;
            
            sqlcon.con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            samsodhan_btn.Enabled = true;
            int i;

            i = dataGridView1.CurrentRow.Index;
            id = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
           
            DisplayDataowner();
           

            //dataGridView2.Columns[0].Visible = false;
        }

        private void banijyasamsodhan_Load(object sender, EventArgs e)
        {
            jari_dat.Text = global.nepalidate;
            samsodhan_btn.Enabled = false;
            nameupd_btn.Enabled = false;
            capitalupd_btn.Enabled = false;
            karobarupd_btn.Enabled = false;
            locationupd_btn.Enabled = false;
            namsariupd_btn.Enabled = false;
            renew_btn.Enabled = false;
            firmclose_btn.Enabled = false;
            branchupd_btn.Enabled = false;
            transfer_btn.Enabled = false;
            button_sajhedari.Enabled = false;
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

        private void button3_Click(object sender, EventArgs e)
        {
            changebanijyaname ciname = new changebanijyaname();
            ciname.MdiParent = this.MdiParent;
            ciname.getid(id, transid.Text, darta_txt.Text);
            //this.Hide();
            ciname.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            capitalupdatebanijya capt = new capitalupdatebanijya();
            capt.MdiParent = this.MdiParent;
            capt.getid(id, transid.Text, darta_txt.Text);
            //this.Hide();
            capt.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //changebanijyaaddress uadd = new changebanijyaaddress();
            //uadd.MdiParent = this.MdiParent;
            //uadd.getid(id, transid.Text);
            //uadd.Show();
            string disttransferid = "123";
            changebanijyaaddress uadd = new changebanijyaaddress();
            uadd.MdiParent = this.MdiParent;
            uadd.getids(id, transid.Text, disttransferid, jari_dat.Text);
            uadd.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            changescopebanijya karobar = new changescopebanijya();
            karobar.MdiParent = this.MdiParent;
            karobar.getid(id, transid.Text,jari_dat.Text);
            karobar.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int i;
            int opttype = 1;
            string updateid = "125";
            i = dataGridView1.CurrentRow.Index;
         //   id.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            string firmnames = dataGridView1.Rows[i].Cells[2].Value.ToString();
            string firmtype = dataGridView1.Rows[i].Cells[24].Value.ToString();
           // banijyanamsari namsari = new banijyanamsari();
            owendetailbanijya namsari = new owendetailbanijya();
            namsari.MdiParent = this.MdiParent;
            namsari.getid(id,firmnames.ToString(),transid.Text,firmtype.ToString(),opttype,updateid,jari_dat.Text);
            namsari.Show();
        }
        public void transactionid()
        {
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }

                MySqlCommand cmd = new MySqlCommand("select fy,transnum from setup_transactionbanijyanumber where transid=1", sqlcon.con);
                cmd.CommandType = CommandType.Text;
                // cmd.Connection = con;
                MySqlDataReader sdr = cmd.ExecuteReader();

                sdr.Read();
                transid.Text = global.csioid.ToString().Trim() + sdr["fy"].ToString().Trim() + sdr["transnum"].ToString().Trim();


                sqlcon.con.Close();
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                MySqlCommand cmo = new MySqlCommand("update setup_transactionbanijyanumber SET transnum= transnum+1 WHERE transid=1", sqlcon.con);
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
        private void button9_Click(object sender, EventArgs e)
        {
            transactionid();
            nameupd_btn.Enabled = true;
            capitalupd_btn.Enabled = true;
            karobarupd_btn.Enabled = true;
            locationupd_btn.Enabled = true;
            namsariupd_btn.Enabled = true;
            renew_btn.Enabled = true;
            firmclose_btn.Enabled = true;
            branchupd_btn.Enabled = true;
            transfer_btn.Enabled = true;
            button_sajhedari.Enabled = true;
            global.sync_dblog("banijyaform", id.ToString(), "UPDATE", id.ToString(), "firmid");
            global.sync_dblog("update_banijya", id.ToString(), "INSERT", transid.Text, "dartanum");
            previous();
        }
        public void previous()
        {
            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            int i;
              


            //MessageBox.Show(dataGridView2.Rows[0].Cells[0].Value.ToString());
            i = dataGridView1.CurrentRow.Index;

            string regno = (dataGridView1.Rows[i].Cells[1].Value.ToString());
            string industryname = (dataGridView1.Rows[i].Cells[2].Value.ToString());
            string dist = (dataGridView1.Rows[i].Cells[3].Value.ToString());
            string vdc = (dataGridView1.Rows[i].Cells[4].Value.ToString());
            string ward = (dataGridView1.Rows[i].Cells[5].Value.ToString());
            string ownername;
            string owneraddress = dataGridView2.Rows[0].Cells[7].Value.ToString() + "-" + (dataGridView2.Rows[0].Cells[8].Value.ToString()).Trim()+", "+dataGridView2.Rows[0].Cells[6].Value.ToString();
            string firmtype = dataGridView1.Rows[i].Cells[24].Value.ToString();
                        if (firmtype.ToString() == "135")
            {
                ownername = (dataGridView2.Rows[0].Cells[0].Value.ToString() + " " + dataGridView2.Rows[0].Cells[1].Value.ToString())+" समेत";
            }
            else
            {
                ownername = (dataGridView2.Rows[0].Cells[0].Value.ToString() + " " + dataGridView2.Rows[0].Cells[1].Value.ToString());
            }
            
              MySqlDataAdapter qry = new MySqlDataAdapter("SELECT * FROM banijya_prev where firmid='"+id.ToString()+"' ", sqlcon.con);
            DataTable tb = new System.Data.DataTable();
            qry.Fill(tb);
            if (tb.Rows.Count > 0)
            {
                MySqlCommand qr = new MySqlCommand("UPDATE banijya_prev  SET industryname = @firmname,industryaddress = @address ,updatedate = getdate() ,updatenepdate = @updatenepdate ,ownername = @ownername ,owneraddress = @owneraddress WHERE firmid=@ownerid");
                qr.CommandType = CommandType.Text;
                qr.Parameters.AddWithValue("@firmname", industryname.ToString());
                qr.Parameters.AddWithValue("@ownerid", id.ToString());
                qr.Parameters.AddWithValue("@address", vdc.ToString() + "-" + ward.ToString().Trim() + ", " + dist.ToString());
                qr.Parameters.AddWithValue("@updatenepdate", global.todaynepslash);
                qr.Parameters.AddWithValue("@ownername", ownername.ToString());
                qr.Parameters.AddWithValue("@owneraddress", owneraddress.ToString());
                qr.Connection = sqlcon.con;
                int n = qr.ExecuteNonQuery();
            }
            else
            {


                MySqlCommand qrh = new MySqlCommand("INSERT INTO banijya_prev(firmid,firmregno,industryname,regdate,industryaddress,updatenepdate,ownername,owneraddress) SELECT firmid,firmregno,firmnepname,regdat,@address,@updatenepdate, @ownername,@owneraddress FROM banijyaform WHERE firmid=@ownerid");




                qrh.CommandType = CommandType.Text;
                qrh.Parameters.AddWithValue("@ownerid", id.ToString());
                qrh.Parameters.AddWithValue("@address", vdc.ToString() + "-" + ward.ToString() + ", " + dist.ToString());
                qrh.Parameters.AddWithValue("@updatenepdate", global.todaynepslash);
                qrh.Parameters.AddWithValue("@ownername", ownername.ToString());
                qrh.Parameters.AddWithValue("@owneraddress", owneraddress.ToString());
                qrh.Connection = sqlcon.con;
                int nh = qrh.ExecuteNonQuery();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            banijyafirmrenew firmrenew = new banijyafirmrenew();
            firmrenew.MdiParent = this.MdiParent;
            firmrenew.getid(id);
            firmrenew.Show();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            banijyafirmclose firmrenew = new banijyafirmclose();
            firmrenew.MdiParent = this.MdiParent;
            firmrenew.getid(id,transid.Text,darta_txt.Text);
            firmrenew.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
           banijyabranchadd firmrenew = new banijyabranchadd();
            firmrenew.MdiParent = this.MdiParent;
            firmrenew.getid(id,transid.Text,jari_dat.Text);
            firmrenew.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                int i;
                int opttype = 2;
                string updateid = "135";
                i = dataGridView1.CurrentRow.Index;
                //   id.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                string firmnames = dataGridView1.Rows[i].Cells[2].Value.ToString();
                string firmtype = dataGridView1.Rows[i].Cells[24].Value.ToString();
                // banijyanamsari namsari = new banijyanamsari();
                owendetailbanijya namsari = new owendetailbanijya();
                namsari.MdiParent = this.MdiParent;
                namsari.getid(id, firmnames.ToString(), transid.Text, firmtype.ToString(), opttype, updateid, jari_dat.Text);
                namsari.Show();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            try {
                int i;

                if (dataGridView1.Rows.Count > 0)
                {

                    i = dataGridView1.CurrentRow.Index;
                    string darta = (dataGridView1.Rows[i].Cells[1].Value.ToString());
                    banijyasomsodhansearch ciname = new banijyasomsodhansearch();
                    ciname.MdiParent = this.MdiParent;
                    ciname.getdataa(darta.ToString(), transid.ToString());
                    //this.Hide();
                    ciname.Show();
                }
                else
                {
                    MessageBox.Show("डाटा छनौट गर्नुहोला ।", "छनौट समस्या ।");
                }
            }
            catch { }
            }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string disttransferid = "138";
            changebanijyaaddress uadd = new changebanijyaaddress();
            uadd.MdiParent = this.MdiParent;
            uadd.getids(id, transid.Text, disttransferid,jari_dat.Text);
            uadd.Show();
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            if (comment.Text == "")
            {
                MessageBox.Show("कृपया कैफियत प्रविष्ट गरी पुन प्रयास गर्नु होला ।");
                comment.Focus();
            }
            else
            {
                try
                {
                    // //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");
                    if (sqlcon.con.State == ConnectionState.Closed)
                    {
                        sqlcon.con.Open();
                    }
                    //con.Open();
                    MySqlCommand qrs = new MySqlCommand("UPDATE banijyafirm  SET comment = @txtbxthree  WHERE firmid=@indid");


                    qrs.CommandType = CommandType.Text;


                    qrs.Parameters.AddWithValue("@txtbxthree", comment.Text);
                    qrs.Parameters.AddWithValue("@indid", id.ToString());

                    qrs.Connection = sqlcon.con;


                    //int n = qr.ExecuteNonQuery();
                    int n = qrs.ExecuteNonQuery();

                    if (n > 0)
                    {
                        MessageBox.Show("प्रमाणपत्रमा  प्रिन्टको लागि कैफियत सफलतपुर्वक सेभ भयो ।");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}