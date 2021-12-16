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
    public partial class changebanijyaaddress : Form
    {
        public changebanijyaaddress()
        {
            InitializeComponent();
        }
        public string firmtransferid;
        private void changebanijyaaddress_Load(object sender, EventArgs e)
        {

            button4.Enabled = false;
            button1.Enabled = false;
            groupBox1.Enabled = false;
            d_date.Text = global.todaynepslash;
            user_txt.Text = global.username;


            DisplayData();


            district_combo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            district_combo.AutoCompleteSource = AutoCompleteSource.CustomSource;

            AutoCompleteStringCollection combData = new AutoCompleteStringCollection();
            FillDropDownList(combData);
            district_combo.AutoCompleteCustomSource = combData;

            district_combo.SelectedValue = 40;
        }
        public void FillDropDownList(AutoCompleteStringCollection dataCollection)
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            //string connetionString = null;
            //SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            //connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;
            sql = "SELECT distcode, distunicodename FROM setup_district ORDER BY distunicodename";
            //connection = new SqlConnection(connetionString);
            try
            {
                //connection.Open();
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                sqlcon.con.Close();

                district_combo.DataSource = ds.Tables[0];
                district_combo.ValueMember = "distcode";
                district_combo.DisplayMember = "distunicodename";
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    dataCollection.Add(row[1].ToString());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection inside district ! " + ex);
            }

        }

        public void FillDropDownListvdc(AutoCompleteStringCollection dataCollectionvdcs)
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            //SqlConnection connections;
            MySqlCommand commands;
            MySqlDataAdapter adapters = new MySqlDataAdapter();
            DataSet dss = new DataSet();

            string sqls = null;
            //int distcodevalue = Convert.ToInt16(district_combo.SelectedValue);
            //connetionStrings = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";

            //sqls = "SELECT VdcCode,VdcNepnam FROM  setup_vdc where DistCode='" +district_combo.SelectedValue+ "'";
            sqls = "SELECT VDC_SID,vdcunicodename FROM  setup_vdc where DistCode='" + district_combo.SelectedValue + "'";


            //connections = new SqlConnection(connetionStrings);
            try
            {
                //connections.Open();
                commands = new MySqlCommand(sqls, sqlcon.con);
                adapters.SelectCommand = commands;
                adapters.Fill(dss);
                adapters.Dispose();
                commands.Dispose();
                sqlcon.con.Close();

                vdc_combo.DataSource = dss.Tables[0];
                vdc_combo.ValueMember = "VDC_SID";
                vdc_combo.DisplayMember = "vdcunicodename";
                foreach (DataRow row in dss.Tables[0].Rows)
                {
                    dataCollectionvdcs.Add(row[1].ToString());
                }


            }
            catch
            {
                //MessageBox.Show("Can not open connection ! inside vdc "+ex);
            }

        }
        public void getid(int indusid,string trans)
        {
            label5.Text = null;
            label5.Text = indusid.ToString();

            darta_no.Text = trans.ToString();
            updateid.Text = null;

            //date_txt.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }
        public void getids(int indusid, string trans,string firmtransfer,string decisiondate)
        {
            label5.Text = null;
            label5.Text = indusid.ToString();

            darta_no.Text = trans.ToString();
            updateid.Text = firmtransfer.ToString();
            firmtransferid = firmtransfer.ToString();
            d_date.Text = decisiondate;
            heading_label.Text = "फर्मको स्थानान्तरण गर्नुहोस";

            //date_txt.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }


        public void district(string dist)
        {

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            //SqlConnection connections;
            MySqlCommand commands;
            MySqlDataAdapter adapters = new MySqlDataAdapter();
            DataSet dss = new DataSet();

            string sqls = null;
            //int distcodevalue = Convert.ToInt16(district_combo.SelectedValue);
            //connetionStrings = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";

            //sqls = "SELECT VdcCode,VdcNepnam FROM  setup_vdc where DistCode='" +district_combo.SelectedValue+ "'";
            sqls = " SELECT     distcode, distunicodename FROM         setup_district WHERE     (distunicodename = '" + dist.ToString() + "')";

            //connections = new SqlConnection(connetionStrings);

            //connections.Open();
            commands = new MySqlCommand(sqls, sqlcon.con);
            adapters.SelectCommand = commands;
            adapters.Fill(dss);
            adapters.Dispose();
            commands.Dispose();
            sqlcon.con.Close();


            foreach (DataRow row in dss.Tables[0].Rows)
            {

                district_combo.SelectedValue = row[0].ToString();
            }

        }
        public void DisplayData()
        {
            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }

            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     banijyaform.firmid, GetNumberToUnicode(banijyaform.firmregno) AS 'र.नं', banijyaform.firmnepname AS 'फर्मको नाम', setup_district.distunicodename AS 'जिल्ला',setup_vdc.vdcunicodename As 'गा.पा.। न.पा.',GetNumberToUnicode(banijyaform.firmward) As 'वार्ड', banijyaform.tole AS 'टोल' FROM          setup_district INNER JOIN    banijyaform ON setup_district.distcode = banijyaform.firmdist INNER JOIN    setup_vdc ON banijyaform.firmvdc = setup_vdc.VDC_SID  where banijyaform.firmid=@darta ", sqlcon.con);
            //  MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     industryreg.industryid, industryreg.industryregno AS 'दर्ता नं.', industryreg.industrynepname AS 'उद्योगको नाम' From  industryreg where industryreg.industryid=@darta ", con);
            qry.SelectCommand.Parameters.AddWithValue("@darta", label5.Text);
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView1.DataSource = tb;
            dataGridView1.Columns[0].Visible = false;

            sqlcon.con.Close();

        }
        public void vdcedit(string vdc)
        {

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            //SqlConnection connections;
            MySqlCommand commands;
            MySqlDataAdapter adapters = new MySqlDataAdapter();
            DataSet dss = new DataSet();

            string sqls = null;
            //int distcodevalue = Convert.ToInt16(district_combo.SelectedValue);
            //connetionStrings = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";

            //sqls = "SELECT VdcCode,VdcNepnam FROM  setup_vdc where DistCode='" +district_combo.SelectedValue+ "'";
            sqls = "SELECT VDC_SID,vdcunicodename FROM  setup_vdc where vdcunicodename='" + vdc.ToString() + "'";


            //connections = new SqlConnection(connetionStrings);
            try
            {
                //connections.Open();
                commands = new MySqlCommand(sqls, sqlcon.con);
                adapters.SelectCommand = commands;
                adapters.Fill(dss);
                adapters.Dispose();
                commands.Dispose();
                sqlcon.con.Close();

                //vdc_combo.DataSource = dss.Tables[0];
                //vdc_combo.ValueMember = "VDC_SID";
                //vdc_combo.DisplayMember = "VdcNepnam";

                foreach (DataRow row in dss.Tables[0].Rows)
                {
                    vdc_combo.SelectedValue = row[0].ToString();
                }
                // vdc_combo.SelectedItem = vdc.ToString();

            }
            catch
            {
                //MessageBox.Show("Can not open connection ! inside vdc "+ex);
            }

        }


        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            button4.Enabled = true;
            int i;

            i = dataGridView1.CurrentRow.Index;
            int industryid = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
            label5.Text = industryid.ToString();
     
        }
        public void firmtransfer()
        {
            try
            {
                int i;
                i = dataGridView1.CurrentRow.Index;
                int industryid = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                string regno = dataGridView1.Rows[i].Cells[1].Value.ToString();
                string district = dataGridView1.Rows[i].Cells[3].Value.ToString();
                string vdc = dataGridView1.Rows[i].Cells[4].Value.ToString();
                string ward = dataGridView1.Rows[i].Cells[5].Value.ToString();
                string tole = dataGridView1.Rows[i].Cells[6].Value.ToString();


                //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }

                // string curdate = DateTime.Today.ToString();

                //SqlCommand qr = new SqlCommand("INSERT INTO update_industry (industryid,industryreg ,dartanum,updattitle,updatnew,oldrec,[updatedate],updatenepdate,updateuser) VALUES (@industryid, @industryregno, @dartanum, @updatetitle, @updatenew, @oldvalue, @updatedate, @updatenepdate, @updateuser)");
                MySqlCommand qr = new MySqlCommand("INSERT INTO update_banijya (firmid,firmreg ,dartanum,updattitle,updatnew,oldrec,updatenepdate,updateuser,decisiondate,tax,comment) VALUES (@industryid, GetUnicodeToNumber(@industryregno), GetUnicodeToNumber(@dartanum), @updatetitle, concat(@updatenew1,GetNumberToUnicode(@updatenew2),@updatenew3), @oldvalue, GetUnicodeToNumber(@updatenepdate), @updateuser, GetUnicodeToNumber(@ddate), GetUnicodeToNumber(@tax), @comment)");

                qr.CommandType = CommandType.Text;
                qr.Parameters.AddWithValue("@industryid", industryid.ToString());
                qr.Parameters.AddWithValue("@industryregno", regno.ToString());
                qr.Parameters.AddWithValue("@dartanum", darta_no.Text);
                qr.Parameters.AddWithValue("@updatetitle", updateid.Text);
                qr.Parameters.AddWithValue("@tax", textBox2.Text);
                //qr.Parameters.AddWithValue("@updatenew", textBox1.Text);
                //qr.Parameters.AddWithValue("@oldvalue", statvalue.ToString());
                // MessageBox.Show(vdc_combo.Text);
                qr.Parameters.AddWithValue("@updatenew1", vdc_combo.Text + " ");
                qr.Parameters.AddWithValue("@updatenew2", textBox3.Text);
                qr.Parameters.AddWithValue("@updatenew3", " " + tole_txt.Text + ", " + district_combo.Text);
                qr.Parameters.AddWithValue("@oldvalue", vdc.ToString() + " " + ward.ToString() + " " + tole.ToString() + ", " + district.ToString());

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
                    MySqlCommand qrs = new MySqlCommand("UPDATE banijyaform  SET firmdist = @txtbxthree ,firmvdc = @txtbxfour ,firmward = GetUnicodeToNumber(@ward), tole = @tole, comment = @comment  WHERE firmid=@indid");


                    qrs.CommandType = CommandType.Text;


                    qrs.Parameters.AddWithValue("@txtbxthree", district_combo.SelectedValue);
                    qrs.Parameters.AddWithValue("@txtbxfour", vdc_combo.SelectedValue);
                    qrs.Parameters.AddWithValue("@ward", textBox3.Text);
                    qrs.Parameters.AddWithValue("@tole", tole_txt.Text);
                    qrs.Parameters.AddWithValue("@comment", textBox4.Text);
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
                        MySqlCommand qrh = new MySqlCommand("INSERT INTO owner_banijyatransfer(ownerid, firmid ,firmregno, ccio, cddist, citzdate, citznum, gender, ownerfname, ownerlname, ownerdistcode, ownervdccode, ownervdcward, ownertole, ownergrel, ownerfgrel, ownergname, ownerfgname, comment, upddate, updnepdate, upduser,transid) SELECT ownerid,firmid,firmregno,ccio,cddist,citzdate,citznum,gender,ownerfname,ownerlname,ownerdistcode,ownervdccode,ownervdcward,ownertole,ownergrel,ownerfgrel,ownergname,ownerfgname,comment,upddate,updnepdate,upduser,@transid FROM owner WHERE firmid=@ownerid");




                        qrh.CommandType = CommandType.Text;
                        qrh.Parameters.AddWithValue("@ownerid", label5.Text);

                        qrh.Parameters.AddWithValue("@transid", updateid.Text);

                        qrh.Connection = sqlcon.con;
                        int nh = qrh.ExecuteNonQuery();

                        MySqlCommand qrh1 = new MySqlCommand("INSERT INTO owner_hist_banijya(ownerid, firmid ,firmregno, ccio, cddist, citzdate, citznum, gender, ownerfname, ownerlname, ownerdistcode, ownervdccode, ownervdcward, ownertole, ownergrel, ownerfgrel, ownergname, ownerfgname, comment, upddate, updnepdate, upduser,transid) SELECT ownerid,firmid,firmregno,ccio,cddist,citzdate,citznum,gender,ownerfname,ownerlname,ownerdistcode,ownervdccode,ownervdcward,ownertole,ownergrel,ownerfgrel,ownergname,ownerfgname,comment,upddate,updnepdate,upduser,@transid FROM owner WHERE firmid=@ownerid");




                        qrh1.CommandType = CommandType.Text;
                        qrh1.Parameters.AddWithValue("@ownerid", label5.Text);

                        qrh1.Parameters.AddWithValue("@transid", updateid.Text);

                        qrh1.Connection = sqlcon.con;
                        int nh1 = qrh1.ExecuteNonQuery();

                        if (nh > 0)
                        {
                            MySqlCommand qrform = new MySqlCommand("INSERT INTO banijyaform_transfer(firmid,firmregno,firmtype,firmdist,firmvdc,firmward,tole,regdat ,renewdate,firmscope,firmnepname,karobar ,revenue,tax,branch,comment,usr,updatdate,updatnepdate,karobartype,decisionnepdate,updateuser,transid) SELECT firmid,firmregno,firmtype,firmdist,firmvdc,firmward,tole,regdat,renewdate,firmscope,firmnepname,karobar,revenue,tax,branch,comment,usr,updatdate ,updatnepdate ,karobartype,GetUnicodeToNumber(@decisionnepdate),@user,@transid FROM banijyaform where firmid=@firmid");

                            qrform.CommandType = CommandType.Text;
                            qrform.Parameters.AddWithValue("@firmid", label5.Text);
                            // qrform.Parameters.AddWithValue("@firmid", id_label.Text);
                            qrform.Parameters.AddWithValue("@transid", updateid.Text);
                            qrform.Parameters.AddWithValue("@user", user_txt.Text);
                            qrform.Parameters.AddWithValue("@decisionnepdate", d_date.Text);

                            qrform.Connection = sqlcon.con;
                            int qrf = qrform.ExecuteNonQuery();


                            MySqlCommand qrform1 = new MySqlCommand("INSERT INTO banijyaform_hist(firmid,firmregno,firmtype,firmdist,firmvdc,firmward,tole,regdat ,renewdate,firmscope,firmnepname,karobar ,revenue,tax,branch,comment,usr,updatdate,updatnepdate,karobartype,decisionnepdate,updateuser,transid) SELECT firmid,firmregno,firmtype,firmdist,firmvdc,firmward,tole,regdat,renewdate,firmscope,firmnepname,karobar,revenue,tax,branch,comment,usr,updatdate ,updatnepdate ,karobartype,GetUnicodeToNumber(@decisionnepdate),@user,@transid FROM banijyaform where firmid=@firmid");

                            qrform1.CommandType = CommandType.Text;
                            qrform1.Parameters.AddWithValue("@firmid", label5.Text);
                            // qrform.Parameters.AddWithValue("@firmid", id_label.Text);
                            qrform1.Parameters.AddWithValue("@transid", updateid.Text);
                            qrform1.Parameters.AddWithValue("@user", user_txt.Text);
                            qrform1.Parameters.AddWithValue("@decisionnepdate", d_date.Text);

                            qrform1.Connection = sqlcon.con;
                            int qrf1 = qrform1.ExecuteNonQuery();
                            if (qrf > 0)
                            {
                                MySqlCommand qre = new MySqlCommand("DELETE FROM owner WHERE firmid=@ownerid");




                                qre.CommandType = CommandType.Text;
                                qre.Parameters.AddWithValue("@ownerid", label5.Text);
                                qre.Connection = sqlcon.con;
                                int nht = qre.ExecuteNonQuery();

                                MySqlCommand qrclose = new MySqlCommand(" DELETE FROM banijyaform  WHERE  firmid=@ownerid");




                                qrclose.CommandType = CommandType.Text;
                                qrclose.Parameters.AddWithValue("@ownerid", label5.Text);
                                qrclose.Connection = sqlcon.con;
                                int nhee = qrclose.ExecuteNonQuery();


                                if (nht > 0 && nhee > 0)
                                {

                                    sqlcon.con.Close();
                                    dataGridView1.DataSource = null;
                                    DisplayData();
                                    groupBox1.Enabled = false;
                                    button4.Enabled = false;
                                    // button4.Enabled = false;
                                    button1.Enabled = false;
                                    // cleardata();
                                    MessageBox.Show("फर्मको स्थानान्तरण संशोधन सफल भयो ।", "संशोधन", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                    //button9.Enabled = false;
                                    //button12.Enabled = false;
                                    //panel2.Enabled = false;


                                }
                            }
                        }
                    }

                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void thausaari()
        {
            int i;
            i = dataGridView1.CurrentRow.Index;
            int industryid = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
            string regno = dataGridView1.Rows[i].Cells[1].Value.ToString();
            string district = dataGridView1.Rows[i].Cells[3].Value.ToString();
            string vdc = dataGridView1.Rows[i].Cells[4].Value.ToString();
            string ward = dataGridView1.Rows[i].Cells[5].Value.ToString();
            string tole = dataGridView1.Rows[i].Cells[6].Value.ToString();


            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }

            // string curdate = DateTime.Today.ToString();

            //SqlCommand qr = new SqlCommand("INSERT INTO update_industry (industryid,industryreg ,dartanum,updattitle,updatnew,oldrec,[updatedate],updatenepdate,updateuser) VALUES (@industryid, @industryregno, @dartanum, @updatetitle, @updatenew, @oldvalue, @updatedate, @updatenepdate, @updateuser)");
            MySqlCommand qr = new MySqlCommand("INSERT INTO update_banijya (firmid,firmreg ,dartanum,updattitle,updatnew,oldrec,updatenepdate,updateuser,decisiondate,tax,comment) VALUES (@industryid, GetUnicodeToNumber(@industryregno), GetUnicodeToNumber(@dartanum), @updatetitle, concat(@updatenew1,GetNumberToUnicode(@updatenew2),@updatenew3), @oldvalue, GetUnicodeToNumber(@updatenepdate), @updateuser, GetUnicodeToNumber(@ddate), GetUnicodeToNumber(@tax), @comment)");

            qr.CommandType = CommandType.Text;
            qr.Parameters.AddWithValue("@industryid", industryid.ToString());
            qr.Parameters.AddWithValue("@industryregno", regno.ToString());
            qr.Parameters.AddWithValue("@dartanum", darta_no.Text);
            qr.Parameters.AddWithValue("@updatetitle", updateid.Text);
            qr.Parameters.AddWithValue("@tax", textBox2.Text);
            //qr.Parameters.AddWithValue("@updatenew", textBox1.Text);
            //qr.Parameters.AddWithValue("@oldvalue", statvalue.ToString());
            // MessageBox.Show(vdc_combo.Text);
            qr.Parameters.AddWithValue("@updatenew1", vdc_combo.Text + " ");
            qr.Parameters.AddWithValue("@updatenew2", textBox3.Text );
            qr.Parameters.AddWithValue("@updatenew3", " " + tole_txt.Text + ", " + district_combo.Text);
            qr.Parameters.AddWithValue("@oldvalue", vdc.ToString() + " " + ward.ToString() + " " + tole.ToString() + ", " + district.ToString());

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
                MySqlCommand qrs = new MySqlCommand("UPDATE banijyaform  SET firmdist = @txtbxthree ,firmvdc = @txtbxfour ,firmward = GetUnicodeToNumber(@ward), tole = @tole, comment = @comment  WHERE firmid=@indid");


                qrs.CommandType = CommandType.Text;


                qrs.Parameters.AddWithValue("@txtbxthree", district_combo.SelectedValue);
                qrs.Parameters.AddWithValue("@txtbxfour", vdc_combo.SelectedValue);
                qrs.Parameters.AddWithValue("@ward", textBox3.Text);
                qrs.Parameters.AddWithValue("@tole", tole_txt.Text);
                qrs.Parameters.AddWithValue("@comment", textBox4.Text);
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
                    ////////////////////////////////////
                    ///
                    //MySqlCommand qrh = new MySqlCommand("INSERT INTO owner_hist_banijya(ownerid, firmid ,firmregno, ccio, cddist, citzdate, citznum, gender, ownerfname, ownerlname, ownerdistcode, ownervdccode, ownervdcward, ownertole, ownergrel, ownerfgrel, ownergname, ownerfgname, comment, upddate, updnepdate, upduser,transid) SELECT ownerid,firmid,firmregno,ccio,cddist,citzdate,citznum,gender,ownerfname,ownerlname,ownerdistcode,ownervdccode,ownervdcward,ownertole,ownergrel,ownerfgrel,ownergname,ownerfgname,comment,upddate,updnepdate,upduser,@transid FROM owner WHERE firmid=@ownerid");




                    //qrh.CommandType = CommandType.Text;
                    //qrh.Parameters.AddWithValue("@ownerid", label5.Text);

                    //qrh.Parameters.AddWithValue("@transid", updateid.Text);

                    //qrh.Connection = sqlcon.con;
                    //int nh = qrh.ExecuteNonQuery();

                    //if (nh > 0)
                    //{
                    //    MySqlCommand qrform = new MySqlCommand("INSERT INTO banijyaform_transfer(firmid,firmregno,firmtype,firmdist,firmvdc,firmward,tole,regdat ,renewdate,firmscope,firmnepname,karobar ,revenue,tax,branch,comment,usr,updatdate,updatnepdate,karobartype,decisionnepdate,updateuser,transid) SELECT firmid,firmregno,firmtype,firmdist,firmvdc,firmward,tole,regdat,renewdate,firmscope,firmnepname,karobar,revenue,tax,branch,comment,usr,updatdate ,updatnepdate ,karobartype,GetUnicodeToNumber(@decisionnepdate),@user,@transid FROM banijyaform where firmid=@firmid");

                    //    qrform.CommandType = CommandType.Text;
                    //    qrform.Parameters.AddWithValue("@firmid", label5.Text);
                    //    // qrform.Parameters.AddWithValue("@firmid", id_label.Text);
                    //    qrform.Parameters.AddWithValue("@transid", updateid.Text);
                    //    qrform.Parameters.AddWithValue("@user", user_txt.Text);
                    //    qrform.Parameters.AddWithValue("@decisionnepdate", d_date.Text);

                    //    qrform.Connection = sqlcon.con;
                    //    int qrf = qrform.ExecuteNonQuery();


                    //    MySqlCommand qrform1 = new MySqlCommand("INSERT INTO banijyaform_hist(firmid,firmregno,firmtype,firmdist,firmvdc,firmward,tole,regdat ,renewdate,firmscope,firmnepname,karobar ,revenue,tax,branch,comment,usr,updatdate,updatnepdate,karobartype,decisionnepdate,updateuser,transid) SELECT firmid,firmregno,firmtype,firmdist,firmvdc,firmward,tole,regdat,renewdate,firmscope,firmnepname,karobar,revenue,tax,branch,comment,usr,updatdate ,updatnepdate ,karobartype,GetUnicodeToNumber(@decisionnepdate),@user,@transid FROM banijyaform where firmid=@firmid");

                    //    qrform1.CommandType = CommandType.Text;
                    //    qrform1.Parameters.AddWithValue("@firmid", label5.Text);
                    //    // qrform.Parameters.AddWithValue("@firmid", id_label.Text);
                    //    qrform1.Parameters.AddWithValue("@transid", updateid.Text);
                    //    qrform1.Parameters.AddWithValue("@user", user_txt.Text);
                    //    qrform1.Parameters.AddWithValue("@decisionnepdate", d_date.Text);

                    //    qrform1.Connection = sqlcon.con;
                    //    int qrf1 = qrform1.ExecuteNonQuery();

                    //    MySqlCommand qrh1 = new MySqlCommand("INSERT INTO owner_banijyatransfer(ownerid, firmid ,firmregno, ccio, cddist, citzdate, citznum, gender, ownerfname, ownerlname, ownerdistcode, ownervdccode, ownervdcward, ownertole, ownergrel, ownerfgrel, ownergname, ownerfgname, comment, upddate, updnepdate, upduser,transid) SELECT ownerid,firmid,firmregno,ccio,cddist,citzdate,citznum,gender,ownerfname,ownerlname,ownerdistcode,ownervdccode,ownervdcward,ownertole,ownergrel,ownerfgrel,ownergname,ownerfgname,comment,upddate,updnepdate,upduser,@transid FROM owner WHERE firmid=@ownerid");




                    //    qrh1.CommandType = CommandType.Text;
                    //    qrh1.Parameters.AddWithValue("@ownerid", label5.Text);

                    //    qrh1.Parameters.AddWithValue("@transid", updateid.Text);

                    //    qrh1.Connection = sqlcon.con;
                    //    int nh1 = qrh1.ExecuteNonQuery();

                    //    if (qrf > 0 && qrf1>0)
                    //    {
                    //        MySqlCommand qre = new MySqlCommand("DELETE FROM owner WHERE firmid=@ownerid");




                    //        qre.CommandType = CommandType.Text;
                    //        qre.Parameters.AddWithValue("@ownerid", label5.Text);
                    //        qre.Connection = sqlcon.con;
                    //        int nht = qre.ExecuteNonQuery();

                    //        MySqlCommand qrclose = new MySqlCommand(" DELETE FROM banijyaform  WHERE  firmid=@ownerid");




                    //        qrclose.CommandType = CommandType.Text;
                    //        qrclose.Parameters.AddWithValue("@ownerid", label5.Text);
                    //        qrclose.Connection = sqlcon.con;
                    //        int nhee = qrclose.ExecuteNonQuery();


                    //        if (nht > 0 && nhee > 0)
                    //        {


                                ////////////////////////////////////

                                sqlcon.con.Close();
                                dataGridView1.DataSource = null;
                                DisplayData();
                                groupBox1.Enabled = false;
                                button4.Enabled = false;
                                // button4.Enabled = false;
                                button1.Enabled = false;
                                // cleardata();
                                MessageBox.Show("फर्मको ठेगाना संशोधन सफल भयो ।", "संशोधन", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                //button9.Enabled = false;
                                //button12.Enabled = false;
                                //panel2.Enabled = false;



                          //  }
                       // }
                   // }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (firmtransferid == "136")
            {
                firmtransfer();
            }
            else
            {
                thausaari();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
            button4.Enabled = true;
            button1.Enabled = true;
        }

        private void district_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            vdc_combo.Text = null;
            vdc_combo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            vdc_combo.AutoCompleteSource = AutoCompleteSource.CustomSource;

            AutoCompleteStringCollection combDataa = new AutoCompleteStringCollection();
            FillDropDownListvdc(combDataa);
            vdc_combo.AutoCompleteCustomSource = combDataa;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
       
    }
}
