using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using MySql.Data.MySqlClient;
using MySql.Data.MySqlClient;
namespace CSIO
{
    public partial class industrynameupdate : Form
    {
        public industrynameupdate()
        {
            InitializeComponent();
        }
        public int id;
        public string industnam;
        public string ddate;
        public string regno;
        public string ind_type;

        private void industrynameupdate_Load(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
            button10.Enabled = false;
            sajhedarithap.Enabled = false;
            electric.Enabled = false;
            button11.Enabled = false;
            decision_date.Text = global.nepalidate;

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
                transid.Text = global.csioid.ToString().Trim() + sdr["fy"].ToString().Trim() + sdr["transnum"].ToString().Trim();


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

        public void DisplayData()
        {
            // //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            // con.Open();

            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT industryreg.industryid, GetNumberToUnicode(industryreg.industryregno) AS 'दर्ता नं', industryreg.industrynepname AS 'उद्योगको नाम', setup_district.distunicodename AS 'जिल्ला',setup_vdc.vdcunicodename As 'गा.पा.। न.पा.',GetNumberToUnicode(industryreg.industryward) As 'वार्ड', industryreg.tole AS 'टोल', industryreg.branch AS 'शाखा',   setup_subcategory.subcategory_unicodename AS 'स्तर', setup_subcategory_2.subcategory_unicodename AS 'कानूनी स्वरूप',   setup_subcategory_1.subcategory_unicodename AS 'वर्ग', GetNumberToUnicode(industryreg.regdat) AS 'दर्ता मिति',  GetNumberToUnicode(industryreg.renewdate) AS 'नविकरण मिति', industryreg.karobar AS 'उद्योगको उद्देश्य', GetNumberToUnicode(industryreg.yearlyturnover) AS 'बार्षिक उत्पादन',  GetNumberToUnicode(industryreg.electricpower) AS 'विधुत शक्ती', GetNumberToUnicode(industryreg.statcapital) AS 'स्थिर पुँजी', GetNumberToUnicode(industryreg.varcapital) AS 'चालु पुँजी', GetNumberToUnicode(industryreg.femaleworker) As 'महिला कामदार', GetNumberToUnicode(industryreg.maleworker) As 'पुरूष कामदार', GetNumberToUnicode(industryreg.tax) As 'राजश्व', industryreg.comment AS 'कैफियत', GetNumberToUnicode(industryreg.techworker) As 'प्राविधिक', GetNumberToUnicode(industryreg.nontechworker) As 'अप्राविधिक', industryreg.rawmaterial As 'कच्चा पदार्थ', GetNumberToUnicode(industryreg.fileno) As 'फाइल नं', GetNumberToUnicode(industryreg.thelino) As 'ठेली', GetNumberToUnicode(industryreg.fiscalyear) As 'आ.व'  FROM  industryreg  INNER JOIN      setup_district ON industryreg.industrydist = setup_district.distcode INNER JOIN       setup_vdc ON industryreg.industryvdc = setup_vdc.VDC_SID INNER JOIN setup_subcategory          ON setup_subcategory.subcategory_id = industryreg.industryscale INNER JOIN      setup_subcategory AS setup_subcategory_2 ON industryreg.industrytype = setup_subcategory_2.subcategory_id INNER JOIN       setup_subcategory AS setup_subcategory_1 ON industryreg.industrycat = setup_subcategory_1.subcategory_id where (industryreg.industryregno=@darta) or (GetNumberToUnicode(industryreg.industryregno)=@darta) ", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@darta", darta_txt.Text);
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView1.DataSource = tb;
            dataGridView1.Columns[0].Visible = false;
            sqlcon.con.Close();

        }
        public void DisplayDataowner()
        {
            // //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            //con.Open();
            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     owner_industry.ownerid, owner_industry.ownerfname AS 'नाम', owner_industry.ownerlname AS 'थर', setup_district_1.distunicodename AS 'ठेगाना', setup_vdc.vdcunicodename AS 'गा.पा.। न.पा.', GetNumberToUnicode(owner_industry.ownervdcward) AS 'वार्ड', owner_industry.ownertole AS 'टोल',    setup_citizenissueoff.citizen_officeunicodename AS 'जारी गर्ने कार्यालय', setup_district.distunicodename AS 'जारी जिल्ला', GetNumberToUnicode(owner_industry.citzdate) AS 'जारी मिति',   GetNumberToUnicode(owner_industry.citznum) AS 'ना.प्र.नं.', setup_subcategory_1.subcategory_unicodename AS 'बाबु।पति', owner_industry.ownergname AS 'बावु।पति', setup_subcategory_2.subcategory_unicodename AS 'बाजे।ससुरा', owner_industry.ownerfgname AS 'बाजे।ससुरा', setup_subcategory.subcategory_unicodename AS 'लिंग', owner_industry.comment AS 'कैफियत', GetNumberToUnicode(owner_industry.contact) As 'सम्पर्क नं', owner_industry.email As 'ईमेल' FROM         owner_industry INNER JOIN setup_district ON owner_industry.cddist = setup_district.distcode INNER JOIN setup_citizenissueoff ON owner_industry.ccio = setup_citizenissueoff.citizen_officeid INNER JOIN setup_vdc ON owner_industry.ownervdccode = setup_vdc.VDC_SID INNER JOIN     setup_district AS setup_district_1 ON owner_industry.ownerdistcode = setup_district_1.distcode INNER JOIN        setup_subcategory ON owner_industry.gender = setup_subcategory.subcategory_id INNER JOIN      setup_subcategory AS setup_subcategory_1 ON owner_industry.ownergrel = setup_subcategory_1.subcategory_id INNER JOIN       setup_subcategory AS setup_subcategory_2 ON owner_industry.ownerfgrel = setup_subcategory_2.subcategory_id WHERE     (owner_industry.industryid =@darta)", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@darta", id.ToString());
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView2.DataSource = tb;

            sqlcon.con.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            //global.sync_dblog("industryreg", id.ToString(), "UPDATE",transid.Text);
            //global.sync_dblog("industryupdate", id.ToString().ToString(), "INSERT",transid.Text);
            changeindustryname ciname = new changeindustryname();
            //ciname.MdiParent = this;
            ciname.getid(id, transid.Text,decision_date.Text);
            //this.Hide();
            ciname.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            capitalupdateindustry capt = new capitalupdateindustry();
           // capt.MdiParent = this.MdiParent;
            capt.getid(id, transid.Text,decision_date.Text);
            //this.Hide();
            capt.ShowDialog();
        }
        public void getindustrytype(string firmtype)
        {
            try
            {

                //      sqlcon.con.Open();
                ////  sql = "SELECT     subcategory_id  FROM setup_category WHERE ( subcategory_unicodename = @firmtype)";


                //  SqlCommand cmd = new SqlCommand("SELECT     subcategory_id  FROM setup_subcategory WHERE ( subcategory_unicodename = @firmtype)",  sqlcon.con);
                //  cmd.Parameters.AddWithValue("@firmtype", firmtype.ToString());
                //      SqlDataReader dr = cmd.ExecuteReader();
                //  while (dr.Read())
                //  {
                //      ind_type.Text = dr.GetString(0);

                //  }
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                MySqlCommand cmo = new MySqlCommand("SELECT subcategory_id  FROM setup_subcategory WHERE ( subcategory_unicodename = @firmtype)", sqlcon.con);
                cmo.CommandType = CommandType.Text;
                cmo.Parameters.AddWithValue("@firmtype", firmtype.ToString());
                int tid = Convert.ToInt32(cmo.ExecuteScalar());

                ind_type = tid.ToString();
                sqlcon.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            button8.Enabled = true;


            int i;

            i = dataGridView1.CurrentRow.Index;
            id = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
           regno = dataGridView1.Rows[i].Cells[1].Value.ToString();
            industnam = dataGridView1.Rows[i].Cells[2].Value.ToString();
            ddate = decision_date.Text;
            // MessageBox.Show(dataGridView1.Rows[i].Cells[9].Value.ToString());
            getindustrytype(dataGridView1.Rows[i].Cells[9].Value.ToString());
            DisplayDataowner();
            dataGridView2.Columns[0].Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            changeindustryaddress cia = new changeindustryaddress();
            //cia.MdiParent = this.MdiParent;
            cia.getid(id, transid.Text,decision_date.Text);
            //this.Hide();
            cia.ShowDialog();

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            changescopeindustry scope = new changescopeindustry();
           // scope.MdiParent = this.MdiParent;
            scope.getid(id, transid.Text,decision_date.Text);
            scope.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {

            try
            {
                string upid = "125";
                industrynaasari naamsari = new industrynaasari();
                naamsari.MdiParent = this.MdiParent;
                naamsari.getid(id.ToString(), transid.Text, upid.ToString(), decision_date.Text, ind_type.ToString(), industnam.ToString(), regno.ToString());
                naamsari.Show();
            } catch(Exception ex)
            {
                MessageBox.Show("Error");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            transactionid();
            sajhedarithap.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button9.Enabled = true;
            button10.Enabled = true;
            electric.Enabled = true;
            button11.Enabled = true;
            previous();
            global.sync_dblog("industryreg", id.ToString(), "UPDATE", id.ToString(), "industryid");
            global.sync_dblog("update_industry", id.ToString(), "INSERT", transid.Text, "dartanum");

        }
        public void previous()
        {
            try
            {
                // //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");
                string sajhedari = "";
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                MySqlDataAdapter qryo = new MySqlDataAdapter("SELECT * FROM owner_industry where industryid=@industryid ", sqlcon.con);
                qryo.SelectCommand.Parameters.AddWithValue("@industryid", id.ToString());
                DataTable tbs = new System.Data.DataTable();
                qryo.Fill(tbs);
                if (tbs.Rows.Count > 1)
                {
                    sajhedari = " समेत";
                }

                int i;

                i = dataGridView1.CurrentRow.Index;
                string regno = (dataGridView1.Rows[i].Cells[1].Value.ToString());
                string industryname = (dataGridView1.Rows[i].Cells[2].Value.ToString());
                string dist = (dataGridView1.Rows[i].Cells[3].Value.ToString());
                string vdc = (dataGridView1.Rows[i].Cells[4].Value.ToString());
                string ward = (dataGridView1.Rows[i].Cells[5].Value.ToString());
                string ownername = (dataGridView2.Rows[0].Cells[1].Value.ToString() + " " + dataGridView2.Rows[0].Cells[2].Value.ToString() + sajhedari.ToString());
                string owneraddress = dataGridView2.Rows[0].Cells[4].Value.ToString() + "-" + (dataGridView2.Rows[0].Cells[5].Value.ToString()).Trim() + ", " + dataGridView2.Rows[0].Cells[3].Value.ToString();
                string regdate = (dataGridView1.Rows[i].Cells[11].Value.ToString());
                // string vdc = (dataGridView1.Rows[i].Cells[7].Value.ToString());
                MySqlDataAdapter qry = new MySqlDataAdapter("SELECT * FROM industry_prev where industryid='" + id.ToString() + "' ", sqlcon.con);
                DataTable tb = new System.Data.DataTable();
                qry.Fill(tb);
                if (tb.Rows.Count > 0)
                {
                    MySqlCommand qr = new MySqlCommand("UPDATE industry_prev  SET industryname = @firmname, industryaddress = @address ,updatedate = getdate() ,updatenepdate = @updatenepdate ,ownername = @ownername ,owneraddress = @owneraddress, regdate=@regdat WHERE industryid=@ownerid");
                    qr.CommandType = CommandType.Text;
                    qr.Parameters.AddWithValue("@firmname", industryname.ToString());
                    qr.Parameters.AddWithValue("@ownerid", id.ToString());
                    qr.Parameters.AddWithValue("@address", vdc.ToString() + "-" + ward.ToString().Trim() + ", " + dist.ToString());
                    qr.Parameters.AddWithValue("@updatenepdate", global.nepalidate);
                    qr.Parameters.AddWithValue("@ownername", ownername.ToString());
                    qr.Parameters.AddWithValue("@owneraddress", owneraddress.ToString());
                    qr.Parameters.AddWithValue("@regdat", regdate.ToString());
                    qr.Connection = sqlcon.con;
                    int n = qr.ExecuteNonQuery();
                }
                else
                {
                    MySqlCommand qrh = new MySqlCommand(" INSERT INTO industry_prev(industryid,industryregno,industryname,industryaddress,updatenepdate,ownername,owneraddress,regdate) SELECT industryid,industryregno,industrynepname,@address,@updatenepdate,@ownername,@owneraddress,regdat FROM industryreg WHERE industryid=@ownerid");




                    qrh.CommandType = CommandType.Text;
                    qrh.Parameters.AddWithValue("@ownerid", id.ToString());
                    qrh.Parameters.AddWithValue("@address", vdc.ToString() + "-" + ward.ToString() + ", " + dist.ToString());
                    qrh.Parameters.AddWithValue("@updatenepdate", global.nepalidate);
                    //qr.Parameters.AddWithValue("@updatenepdate", global.todaynepslash);
                    qrh.Parameters.AddWithValue("@ownername", ownername.ToString());
                    qrh.Parameters.AddWithValue("@owneraddress", owneraddress.ToString());
                    //  qrh.Parameters.AddWithValue("@regdat", regdate.ToString());
                    qrh.Connection = sqlcon.con;
                    int nh = qrh.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("सञ्चालकको बिवरण प्रविष्ट नभएको वा बिवरणमा समस्या रहेकोले पुन:जाँच गरी प्रविष्ट गर्नु होला ।","बिवरणमा समस्या");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            industryrenew renew = new industryrenew();
            renew.MdiParent = this.MdiParent;
            renew.getid(id, transid.Text,regno.ToString());
            renew.Show();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            industryclose inclose = new industryclose();
            inclose.MdiParent = this.MdiParent;
            inclose.getid(id, transid.Text, regno.ToString());
            inclose.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string upid = "132";
            industrynaasari naamsari = new industrynaasari();
            naamsari.MdiParent = this.MdiParent;
            naamsari.getid(id.ToString(), transid.Text, upid.ToString(), decision_date.Text, ind_type.ToString(), industnam.ToString(), regno.ToString());
           
            naamsari.Show();
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            changeelectriccapacity scope = new changeelectriccapacity();
           // scope.MdiParent = this.MdiParent;
            scope.getid(id, transid.Text,decision_date.Text);
            scope.ShowDialog();
        }

        private void button11_Click_2(object sender, EventArgs e)
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
                    MySqlCommand qrs = new MySqlCommand("UPDATE industryreg  SET comment = @txtbxthree  WHERE industryid=@indid");


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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            industrysomsodhansearch iss = new industrysomsodhansearch();
            iss.MdiParent = this.MdiParent;
            iss.getdataa(regno.ToString(), transid.Text);
            iss.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            SearchIndustryCertLetter ift = new SearchIndustryCertLetter();
            ift.MdiParent = this.MdiParent;
            ift.Show();
        }
    }
}
