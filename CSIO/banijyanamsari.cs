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
    public partial class banijyanamsari : Form
    {
        public banijyanamsari()
        {
            InitializeComponent();
        }
        public static string firmi;
        public void DisplayData()
        {
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     owner.ownerid, owner.firmid, GetNumberToUnicode(owner.firmregno) As 'दर्ता नं',owner.ownerfname AS 'नाम', owner.ownerlname AS 'थर', setup_district_1.distunicodename AS 'ठेगाना जिल्ला', setup_vdc.vdcunicodename AS 'गा.पा.। न.पा.', GetNumberToUnicode(owner.ownervdcward) AS 'वार्ड', owner.ownertole AS 'टोल',    setup_citizenissueoff.citizen_officeunicodename AS 'जारी गर्ने कार्यालय', setup_district.distunicodename AS 'जारी जिल्ला', GetNumberToUnicode(owner.citzdate) AS 'जारी मिति',   GetNumberToUnicode(owner.citznum) AS 'ना.प्र.नं.', setup_subcategory_1.subcategory_unicodename AS 'बाबु।पति', owner.ownergname AS 'बावु।पति', setup_subcategory_2.subcategory_unicodename AS 'बाजे।ससुरा', owner.ownerfgname AS 'बाजे।ससुरा नाम', setup_subcategory.subcategory_unicodename AS 'लिंग', owner.comment AS 'कैफियत' FROM         owner INNER JOIN setup_district ON owner.cddist = setup_district.distcode INNER JOIN setup_citizenissueoff ON owner.ccio = setup_citizenissueoff.citizen_officeid INNER JOIN setup_vdc ON owner.ownervdccode = setup_vdc.VDC_SID INNER JOIN     setup_district AS setup_district_1 ON owner.ownerdistcode = setup_district_1.distcode INNER JOIN        setup_subcategory ON owner.gender = setup_subcategory.subcategory_id INNER JOIN      setup_subcategory AS setup_subcategory_1 ON owner.ownergrel = setup_subcategory_1.subcategory_id INNER JOIN       setup_subcategory AS setup_subcategory_2 ON owner.ownerfgrel = setup_subcategory_2.subcategory_id WHERE     (owner.firmid = @darta)", sqlcon.con);
                qry.SelectCommand.Parameters.AddWithValue("@darta", label5.Text);
                DataTable tb = new DataTable();
                qry.Fill(tb);
                dataGridView1.DataSource = tb;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;

                sqlcon.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }



        public void FillDropDownList1(AutoCompleteStringCollection dataCollections)
        {

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            //connectionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";
            sql = "SELECT distcode, distunicodename FROM setup_district";
           
            try
            {
               // //connection.Open();
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                sqlcon.con.Close();

                owner_dist.DataSource = ds.Tables[0];
                owner_dist.ValueMember = "distcode";
                owner_dist.DisplayMember = "distunicodename";
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    dataCollections.Add(row[1].ToString());
                }


            }
            catch
            {
                MessageBox.Show("Can not open connection ! ");
            }

        }
        public void Listvdc(AutoCompleteStringCollection dataCollectionvdc)
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }


            MySqlCommand commands;
            MySqlDataAdapter adapters = new MySqlDataAdapter();
            DataSet dss = new DataSet();

            string sqls = null;
            //int distcodevalue = Convert.ToInt16(district_combo.SelectedValue);

            //sqls = "SELECT VdcCode,VdcNepnam FROM  setup_vdc where DistCode='" +district_combo.SelectedValue+ "'";
            sqls = "SELECT VDC_SID,vdcunicodename FROM  setup_vdc where DistCode='" + owner_dist.SelectedValue + "'";

            try
            {

                commands = new MySqlCommand(sqls, sqlcon.con);
                adapters.SelectCommand = commands;
                adapters.Fill(dss);
                adapters.Dispose();
                commands.Dispose();
                sqlcon.con.Close();

                owner_vdc.DataSource = dss.Tables[0];
                owner_vdc.ValueMember = "VDC_SID";
                owner_vdc.DisplayMember = "vdcunicodename";
                foreach (DataRow row in dss.Tables[0].Rows)
                {
                    dataCollectionvdc.Add(row[1].ToString());
                }


            }
            catch
            {
                //MessageBox.Show("Connection error in VDC Connection");
            }

        }

        public void genderlist(AutoCompleteStringCollection dataCollection)
        {

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
           sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (setup_subcategory.category_id = 1)";
           
            try
            {
               
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                sqlcon.con.Close();

                gender.DataSource = ds.Tables[0];
                gender.ValueMember = "subcategory_id";
                gender.DisplayMember = "subcategory_unicodename";
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    dataCollection.Add(row[1].ToString());
                    //citizen_off.Items.Add(row[0].ToString()+" Y "+row[1].ToString());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }
        public void parent1(AutoCompleteStringCollection dataCollection)
        {

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
           // //connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;
            sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (setup_subcategory.category_id = 2)";
           // //connection = new SqlConnection(connetionString);
            try
            {
                ////connection.Open();
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                sqlcon.con.Close();

                parentone.DataSource = ds.Tables[0];
                parentone.ValueMember = "subcategory_id";
                parentone.DisplayMember = "subcategory_unicodename";
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    dataCollection.Add(row[1].ToString());
                    //citizen_off.Items.Add(row[0].ToString()+" Y "+row[1].ToString());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }
        public void parent2(AutoCompleteStringCollection dataCollection)
        {

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
           // //connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;
            sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (setup_subcategory.category_id = 3)";
           // //connection = new SqlConnection(connetionString);
            try
            {
                ////connection.Open();
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                sqlcon.con.Close();

                parenttwo.DataSource = ds.Tables[0];
                parenttwo.ValueMember = "subcategory_id";
                parenttwo.DisplayMember = "subcategory_unicodename";
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    dataCollection.Add(row[1].ToString());
                    //citizen_off.Items.Add(row[0].ToString()+" Y "+row[1].ToString());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }
        public void citizenofficedetail(AutoCompleteStringCollection dataCollection)
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
           // //connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;
            sql = "SELECT     citizen_officeid,citizen_officeunicodename  FROM  setup_citizenissueoff";
           // //connection = new SqlConnection(connetionString);
            try
            {
              //  //connection.Open();
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                sqlcon.con.Close();

                citizen_off.DataSource = ds.Tables[0];
                citizen_off.ValueMember = "citizen_officeid";
                citizen_off.DisplayMember = "citizen_officeunicodename";
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    dataCollection.Add(row[1].ToString());
                    //citizen_off.Items.Add(row[0].ToString()+" Y "+row[1].ToString());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }
        public void citzdistedit(string dist)
        {

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            MySqlCommand commands;
            MySqlDataAdapter adapters = new MySqlDataAdapter();
            DataSet dss = new DataSet();

            string sqls = null;
            //int distcodevalue = Convert.ToInt16(district_combo.SelectedValue);
           // //connetionStrings = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";

            //sqls = "SELECT VdcCode,VdcNepnam FROM  setup_vdc where DistCode='" +district_combo.SelectedValue+ "'";
            sqls = " SELECT     distcode, distnepname FROM         setup_district WHERE     (distnepname = '" + dist.ToString() + "')";



         
            commands = new MySqlCommand(sqls, sqlcon.con);
            adapters.SelectCommand = commands;
            adapters.Fill(dss);
            adapters.Dispose();
            commands.Dispose();
            sqlcon.con.Close();


            foreach (DataRow row in dss.Tables[0].Rows)
            {

                comboBox1.SelectedValue = row[0].ToString();
            }

        }

        public void district(string dist)
        {

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            MySqlCommand commands;
            MySqlDataAdapter adapters = new MySqlDataAdapter();
            DataSet dss = new DataSet();

            string sqls = null;
            //int distcodevalue = Convert.ToInt16(district_combo.SelectedValue);
           // //connetionStrings = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";

            //sqls = "SELECT VdcCode,VdcNepnam FROM  setup_vdc where DistCode='" +district_combo.SelectedValue+ "'";
            sqls = " SELECT     distcode, distnepname FROM         setup_district WHERE     (distnepname = '" + dist.ToString() + "')";

           // //connections = new SqlConnection(connetionStrings);

            commands = new MySqlCommand(sqls, sqlcon.con);
            adapters.SelectCommand = commands;
            adapters.Fill(dss);
            adapters.Dispose();
            commands.Dispose();
            sqlcon.con.Close();


            foreach (DataRow row in dss.Tables[0].Rows)
            {

                owner_dist.SelectedValue = row[0].ToString();
            }

        }
        public void FillDropDownList(AutoCompleteStringCollection dataCollection)
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            } 
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            ////connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;
            sql = "SELECT distcode, distunicodename FROM setup_district ORDER BY distnepname";
           // //connection = new SqlConnection(connetionString);
            try
            {
                ////connection.Open();
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                sqlcon.con.Close();

                comboBox1.DataSource = ds.Tables[0];
                comboBox1.ValueMember = "distcode";
                comboBox1.DisplayMember = "distunicodename";
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    dataCollection.Add(row[1].ToString());
                }


            }
            catch
            {
                MessageBox.Show("Can not open connection ! ");
            }

        }

       

        private void banijyanamsari_Load(object sender, EventArgs e)
        {
            d_date.Text = global.todaynepslash;
            user_txt.Text = global.username;

            button2.Enabled = false;
            button1.Enabled = false;
            panel2.Enabled = false;

            DisplayData();

            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection combData = new AutoCompleteStringCollection();
            FillDropDownList(combData);
            comboBox1.AutoCompleteCustomSource = combData;
            comboBox1.SelectedValue = 40;

            owner_dist.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            owner_dist.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection dstrict = new AutoCompleteStringCollection();
            FillDropDownList1(dstrict);
            owner_dist.AutoCompleteCustomSource = dstrict;
            owner_dist.SelectedValue = 40;


            //owner_vdc.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //owner_vdc.AutoCompleteSource = AutoCompleteSource.CustomSource;

            //AutoCompleteStringCollection combDataa = new AutoCompleteStringCollection();
            //Listvdc(combDataa);
            //owner_vdc.AutoCompleteCustomSource = combDataa;





            //comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //comboBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;

            //AutoCompleteStringCollection combDataa = new AutoCompleteStringCollection();
            //FillDropDownListvdc(combDataa);
            //comboBox2.AutoCompleteCustomSource = combDataa;
            //comboBox2.SelectedValue = 40;

            citizen_off.AutoCompleteMode = AutoCompleteMode.Append;
            citizen_off.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection ctzenoff = new AutoCompleteStringCollection();
            citizenofficedetail(ctzenoff);
            citizen_off.AutoCompleteCustomSource = ctzenoff;
            citizen_off.SelectedIndex = 0;

            gender.AutoCompleteMode = AutoCompleteMode.Append;
            gender.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection gndr = new AutoCompleteStringCollection();
            genderlist(gndr);
            gender.AutoCompleteCustomSource = gndr;
            gender.SelectedIndex = 0;


            parentone.AutoCompleteMode = AutoCompleteMode.Append;
            parentone.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection pone = new AutoCompleteStringCollection();
            parent1(pone);
            parentone.AutoCompleteCustomSource = pone;
            parentone.SelectedIndex = 0;

            parenttwo.AutoCompleteMode = AutoCompleteMode.Append;
            parenttwo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection ptwo = new AutoCompleteStringCollection();
            parent2(ptwo);
            parenttwo.AutoCompleteCustomSource = ptwo;
            parenttwo.SelectedIndex = 0;

        }
        public void getid(int indusid)
        {
            label5.Text = null;
            label5.Text = indusid.ToString();



            //date_txt.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            //button5.Enabled = false;

            DisplayData();
            panel2.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!jari_dat.MaskFull)
            {

                MessageBox.Show("मिति पुरा राख्नुहोस ।");


            }
            else
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                //string firmi;
                ////////////////////////////////////////
                MySqlCommand command;
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                DataSet ds = new DataSet();
                string sql = "SELECT   firmregno from banijyaform where firmid='" + label5.Text + "' ";

                //string sql = "SELECT VdcCode, VdcNepnam FROM setup_vdc where setup_vdc.DistCode='" + comboBox3.ValueMember + "'";



                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();


                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    firmi = row[0].ToString();

                    // MessageBox.Show(firmi.ToString());
                }
                // MessageBox.Show(firmi.ToString());

                ////////////////////////////


                string curdate = DateTime.Now.ToString();


                //SqlCommand qr = new SqlCommand("insert into banijyaform (firmregno,firmdist,firmvdc,firmward,tole,regdat,renewdate,firmscope,firmnepname,karobar,revenue,branch,comment,updatnepdate,usr,updatdate) values
                // ('" + darta_txt.Text + "','" + district_combo.SelectedValue + "','" + vdc_combo.SelectedValue + "','" + textBox1.Text + "','" + tole_txt.Text + "','" + darta_date.Text + "','" + renew_date.Text + "','" + firm_obj.SelectedValue + "',,'" + firm_name.Text + "''" + firm_karobar.Text + "','" + firm_capital.Text + "','" + branch_txt.Text + "','" + comment.Text + "','" + entry_nepdate.Text + "','" + usr.Text + "','" + curdate + "' )", con);
                //SqlCommand qr = new SqlCommand("INSERT INTO owner_industry(industryid,'industryregno', 'ccio', 'cddist', 'citzdate', 'citznum', 'gender', 'ownerfname', 'ownerlname')VALUES('" + darta_txt.Text + "','" + citizen_off.SelectedValue + "','" + comboBox1.SelectedValue + "','" + jari_dat.Text + "','" + citizen_num.Text + "','" + gender.SelectedValue + "','" + owner_name.Text + "','" + owner_lname.Text + "')", con);
                // SqlCommand qr = new SqlCommand("INSERT INTO owner_industry(industryid,'industryregno', 'ccio', 'cddist', 'citzdate', 'citznum', 'gender', 'ownerfname', 'ownerlname', 'ownerdistcode', 'ownervdccode', 'ownervdcward', 'ownertole', 'ownergrel', 'ownerfgrel', 'ownergname', 'ownerfgname', comment, [upddate], 'updnepdate', 'upduser')VALUES('" + firmi + "','" + darta_txt.Text + "','" + citizen_off.SelectedValue + "','" + comboBox1.SelectedValue + "','" + jari_dat.Text + "','" + citizen_num.Text + "','" + gender.SelectedValue + "','" + owner_name.Text + "','" + owner_lname.Text + "','" + owner_dist.SelectedValue + "','" + owner_vdc.SelectedValue + "','" + owner_ward.Text + "','" + owner_tole.Text + "','" + parentone.SelectedValue + "','" + parenttwo.SelectedValue + "','" + owner_parent1name.Text + "','" + owner_parent2name.Text + "','" + comment.Text + "','" + curdate + "','" + global.nepalidate + "','" + global.username + "')", con);


                // SqlCommand qr = new SqlCommand("INSERT INTO owner_industry(industryid ,'industryregno' ,'ccio' ,'cddist' ,'citzdate' ,'citznum','gender'  ,'ownerfname'  ,'ownerlname','ownerdistcode'  ,'ownervdccode'  ,'ownervdcward' ,'ownertole'  ,'ownergrel','ownerfgrel'  ,'ownergname' ,'ownerfgname' ,comment ,[upddate] ,'updnepdate' ,'upduser')  VALUES ('" + firmi + "' ,'" + darta_txt.Text + "' ,'" + citizen_off.SelectedValue + "' ,'" + comboBox1.SelectedValue + "' ,'" + jari_dat.Text + "' ,'" + citizen_num.Text + "'  ,'" + gender.SelectedValue + "','" + owner_name.Text + "','" + owner_lname.Text + "' ,'" + owner_dist.SelectedValue + "','" + owner_vdc.SelectedValue + "','" + owner_ward.Text + "','" + owner_tole.Text + "','" + parentone.SelectedValue + "','" + parenttwo.SelectedValue + "','" + owner_parent1name.Text + "','" + owner_parent2name.Text + "','" + comment.Text + "','" + curdate + "','" + global.nepalidate + "','" + global.username + "')", con);

                MySqlCommand qr = new MySqlCommand("INSERT INTO owner(firmid ,firmregno, ccio, cddist, citzdate, citznum, gender, ownerfname, ownerlname, ownerdistcode, ownervdccode, ownervdcward, ownertole, ownergrel, ownerfgrel, ownergname, ownerfgname, comment, updnepdate, upduser, contact)VALUES(@firmid,@darta,@citizenoff,@citizendist,@jaridate,@citizennum,@gender,@ownerfname,@ownerlname,@ownerdist,@ownervdc,@ownerward,@ownertole,@parentone,@parenttwo,@parentonename,@parenttwoname,@comment,@nepdate,@user,@contact)");




                qr.CommandType = CommandType.Text;
                qr.Parameters.AddWithValue("@firmid", label5.Text);
                qr.Parameters.AddWithValue("@darta", firmi.ToString());
                qr.Parameters.AddWithValue("@citizenoff", citizen_off.SelectedValue);
                qr.Parameters.AddWithValue("@citizendist", comboBox1.SelectedValue);
                qr.Parameters.AddWithValue("@jaridate", jari_dat.Text);
                qr.Parameters.AddWithValue("@citizennum", citizen_num.Text);
                qr.Parameters.AddWithValue("@gender", gender.SelectedValue);
                qr.Parameters.AddWithValue("@ownerfname", owner_name.Text);
                qr.Parameters.AddWithValue("@ownerlname", owner_lname.Text);
                qr.Parameters.AddWithValue("@ownerdist", owner_dist.SelectedValue);
                qr.Parameters.AddWithValue("@ownervdc", owner_vdc.SelectedValue);
                qr.Parameters.AddWithValue("@ownerward", owner_ward.Text);
                qr.Parameters.AddWithValue("@ownertole", owner_tole.Text);
                qr.Parameters.AddWithValue("@parentone", parentone.SelectedValue);
                qr.Parameters.AddWithValue("@parenttwo", parenttwo.SelectedValue);
                qr.Parameters.AddWithValue("@parentonename", owner_parent1name.Text);
                qr.Parameters.AddWithValue("@parenttwoname", owner_parent2name.Text);
                qr.Parameters.AddWithValue("@comment", comment.Text);
                qr.Parameters.AddWithValue("@contact", contact_txt.Text);
                // qr.Parameters.AddWithValue("@curdate", curdate);
                qr.Parameters.AddWithValue("@nepdate", global.nepalidate);
                qr.Parameters.AddWithValue("@user", global.username);
                //qr.Parameters.AddWithValue("@firmid", firmi);
                // SqlCommand qr = new SqlCommand("INSERT INTO banijyaform('firmregno',[firmdist],[firmvdc] ,[firmward],tole ,[regdat],[renewdate],[firmscope],[firmnepname],[karobar],[revenue],[branch],comment,[usr],[updatdate],[updatnepdate])VALUES('" + darta_txt.Text + "','" + district_combo.SelectedValue + "','" + vdc_combo.SelectedValue + "','" + textBox1.Text + "','" + tole_txt.Text + "','" + darta_date.Text + "','" + renew_date.Text + "','" + firm_obj.SelectedValue + "','" + firm_name.Text + "','" + firm_karobar.Text + "','" + firm_capital.Text + "','" + branch_txt.Text + "','" + comment.Text + "','" + usr.Text + "','" + curdate + "','" + entry_nepdate.Text + "')", con);    
                qr.Connection = sqlcon.con;
                int n = qr.ExecuteNonQuery();


                if (n > 0)
                {
                    // con.Open();
                    int i;
                    i = dataGridView1.CurrentRow.Index;
                    int industryid = int.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
                    string regno = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    string fname = dataGridView1.Rows[i].Cells[3].Value.ToString();
                    string lname = dataGridView1.Rows[i].Cells[4].Value.ToString();
                    string dist = dataGridView1.Rows[i].Cells[5].Value.ToString();
                    string vdc = dataGridView1.Rows[i].Cells[6].Value.ToString();
                    string ward = dataGridView1.Rows[i].Cells[7].Value.ToString();

                    MySqlCommand qrr = new MySqlCommand("INSERT INTO update_banijya (firmid,firmreg ,dartanum,updattitle,updatnew,oldrec,updatenepdate,updateuser,decisiondate,tax,comment) VALUES (@industryid, GetUnicodeToNumber(@industryregno), GetUnicodeToNumber(@dartanum), @updatetitle, @updatenew, @oldvalue, GetUnicodeToNumber(@updatenepdate), @updateuser, GetUnicodeToNumber(@ddate), GetUnicodeToNumber(@tax), @comment)");

                    qrr.CommandType = CommandType.Text;
                    qrr.Parameters.AddWithValue("@industryid", industryid.ToString());
                    qrr.Parameters.AddWithValue("@industryregno", regno.ToString());
                    qrr.Parameters.AddWithValue("@dartanum", darta_no.Text);
                    qrr.Parameters.AddWithValue("@updatetitle", updateid.Text);
                    // qrr.Parameters.AddWithValue("@updatenew", owner_name.Text);
                    // qrr.Parameters.AddWithValue("@oldvalue", fname.ToString());

                    qrr.Parameters.AddWithValue("@updatenew", (owner_name.Text + " " + owner_lname.Text + ", " + owner_vdc.Text + "-" + owner_ward.Text + ", " + owner_dist.Text));
                    qrr.Parameters.AddWithValue("@oldvalue", (fname.ToString() + " " + lname.ToString() + ", " + vdc.ToString() + "-" + ward.ToString() + ", " + dist.ToString()));

                    //qr.Parameters.AddWithValue("@transid", updateid.Text);
                    //qrr.Parameters.AddWithValue("@updatedate", curdate.ToString());
                    qrr.Parameters.AddWithValue("@updatenepdate", global.nepalidate);
                    qrr.Parameters.AddWithValue("@updateuser", user_txt.Text);
                    qrr.Parameters.AddWithValue("@ddate", d_date.Text);
                    qrr.Parameters.AddWithValue("@tax", textBox1.Text);
                    qrr.Parameters.AddWithValue("@comment", comment.Text);


                    qrr.Connection = sqlcon.con;


                    int k = qrr.ExecuteNonQuery();

                    if (k > 0)
                    {
                        int j;

                        j = dataGridView1.CurrentRow.Index;

                        id_label.Text = dataGridView1.Rows[j].Cells[0].Value.ToString();
                       // MessageBox.Show(id_label.Text);
                        //id_label.Visible = false;
                        //   owner_name.Text = dataGridView1.Rows[j].Cells[1].Value.ToString();
                        //   owner_lname.Text = dataGridView1.Rows[j].Cells[2].Value.ToString();
                        //   district(dataGridView1.Rows[j].Cells[3].Value.ToString());
                        //   // jari_dat.Text=dataGridView1.Rows[i].Cells[4].Value.ToString();
                        //  // vdcedit(dataGridView1.Rows[j].Cells[4].Value.ToString());
                        //   owner_ward.Text = dataGridView1.Rows[j].Cells[5].Value.ToString();
                        //   owner_tole.Text = dataGridView1.Rows[j].Cells[6].Value.ToString();
                        //   //ownerdist(dataGridView1.Rows[i].Cells[6].Value.ToString());



                        //  // citzoffedit(dataGridView1.Rows[j].Cells[7].Value.ToString());

                        //   citzdistedit(dataGridView1.Rows[j].Cells[8].Value.ToString());
                        //   //industry_scale.SelectedText = dataGridView1.Rows[i].Cells[8].Value.ToString();
                        //   jari_dat.Text = dataGridView1.Rows[j].Cells[9].Value.ToString();
                        //   //industry_type.SelectedText = dataGridView1.Rows[i].Cells[9].Value.ToString();
                        //   citizen_num.Text = dataGridView1.Rows[j].Cells[10].Value.ToString();
                        //   //firm_obj.SelectedText = dataGridView1.Rows[i].Cells[10].Value.ToString();
                        ////   fatheredit(dataGridView1.Rows[j].Cells[11].Value.ToString());
                        //   owner_parent1name.Text = dataGridView1.Rows[j].Cells[12].Value.ToString();
                        // //  forefatheredit(dataGridView1.Rows[j].Cells[13].Value.ToString());
                        //   owner_parent2name.Text = dataGridView1.Rows[j].Cells[14].Value.ToString();
                        //  // genderedit(dataGridView1.Rows[j].Cells[15].Value.ToString());
                        //   // firm_turnover.Text = dataGridView1.Rows[i].Cells[15].Value.ToString();
                        //   comment.Text = dataGridView1.Rows[j].Cells[16].Value.ToString();

                        //sqlcon.con.Close();
                        // dataGridView1.DataSource = null;
                        // DisplayData();
                        // cleardata();
                        MySqlCommand qrh = new MySqlCommand("INSERT INTO owner_hist_banijya(ownerid, firmid ,firmregno, ccio, cddist, citzdate, citznum, gender, ownerfname, ownerlname, ownerdistcode, ownervdccode, ownervdcward, ownertole, ownergrel, ownerfgrel, ownergname, ownerfgname, comment, upddate, updnepdate, upduser,transid) SELECT ownerid,firmid,firmregno,ccio,cddist,citzdate,citznum,gender,ownerfname,ownerlname,ownerdistcode,ownervdccode,ownervdcward,ownertole,ownergrel,ownerfgrel,ownergname,ownerfgname,comment,upddate,updnepdate,upduser,@transid FROM owner WHERE ownerid=@ownerid");




                        qrh.CommandType = CommandType.Text;
                        qrh.Parameters.AddWithValue("@ownerid", id_label.Text);
                        qrh.Parameters.AddWithValue("@transid", updateid.Text);
                        qrh.Connection = sqlcon.con;
                        int nh = qrh.ExecuteNonQuery();

                        if (nh > 0)
                        {
                            MySqlCommand qre = new MySqlCommand("DELETE FROM owner WHERE ownerid=@ownerid");




                            qre.CommandType = CommandType.Text;
                            qre.Parameters.AddWithValue("@ownerid", id_label.Text);
                            qre.Connection = sqlcon.con;
                            int nht = qre.ExecuteNonQuery();

                            if (nht > 0)
                            {


                                sqlcon.con.Close();
                                dataGridView1.DataSource = null;
                                DisplayData();
                                cleardata();
                                MessageBox.Show("वाणिज्य फर्मको नासारी संशोधन सफल भयो ।", "संशोधन", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                //button9.Enabled = false;
                                //button12.Enabled = false;
                                //panel2.Enabled = false;


                            }
                        }

                    }




                   // MessageBox.Show("प्रोपाइटरको विवरण सफलता पुर्वक सेभ भयो ।");
                    sqlcon.con.Close();
                    dataGridView1.DataSource = null;
                    DisplayData();



                }
                //INSERT INTO [CSIO].[dbo].[owner] ('firmid','firmregno','ccio','cddist','citzdate','citznum','gender','ownerfname','ownerlname','ownerdistcode','ownervdccode','ownervdcward','ownertole','ownergrel','ownerfgrel','ownergname','ownerfgname',comment,[upddate],'updnepdate','upduser') VALUES(<firmid, int,>,<firmregno, nvarchar(15),>,<ccio, int,>,<cddist, int,>,<citzdate, nchar(10),>,<citznum, nvarchar(20),>,<gender, int,>,<ownerfname, nvarchar(50),>,<ownerlname, nvarchar(50),>,<ownerdistcode, tinyint,>,<ownervdccode, int,>,<ownervdcward, int,>,<ownertole, nvarchar(25),>,<ownergrel, int,>,<ownerfgrel, int,>,<ownergname, nvarchar(50),>     ,<ownerfgname, nvarchar(50),> ,<comment, nvarchar(50),>,<upddate, datetime,>,<updnepdate, nchar(10),>,<upduser, nchar(10),>)}
            }

        }
        public void cleardata()
        {
            // darta_txt.Text = null;
            // district_combo.SelectedValue=40;
            id_label.Text = null;
            // qr.Parameters.AddWithValue("@darta", darta_txt.Text);
            // qr.Parameters.AddWithValue("@citizenoff", citizen_off.SelectedValue);
            // qr.Parameters.AddWithValue("@citizendist", comboBox1.SelectedValue);
            //qr.Parameters.AddWithValue("@jaridate", jari_dat.Text);
            citizen_num.Text = null;
            // qr.Parameters.AddWithValue("@gender", gender.SelectedValue);
            owner_name.Text = null;
            owner_lname.Text = null;
            //qr.Parameters.AddWithValue("@ownerdist", owner_dist.SelectedValue);
            //qr.Parameters.AddWithValue("@ownervdc", owner_vdc.SelectedValue);
            owner_ward.Text = null;
            owner_tole.Text = null;
            // qr.Parameters.AddWithValue("@parentone", parentone.SelectedValue);
            //qr.Parameters.AddWithValue("@parenttwo", parenttwo.SelectedValue);
            owner_parent1name.Text = null;
            owner_parent2name.Text = null;
            comment.Text = null;
            textBox1.Text = null;

        }

        private void owner_dist_SelectedIndexChanged(object sender, EventArgs e)
        {
            owner_vdc.Text = null;
            owner_vdc.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            owner_vdc.AutoCompleteSource = AutoCompleteSource.CustomSource;

            AutoCompleteStringCollection combDataa = new AutoCompleteStringCollection();
            Listvdc(combDataa);
            owner_vdc.AutoCompleteCustomSource = combDataa;
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            button1.Enabled = true;
            int j;

            j = dataGridView1.CurrentRow.Index;

            id_label.Text = dataGridView1.Rows[j].Cells[0].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cleardata();
        }

    }
}
