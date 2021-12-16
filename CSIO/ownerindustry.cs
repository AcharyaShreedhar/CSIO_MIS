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
    public partial class ownerindustry : Form
    {
        
        public ownerindustry()
        {
            InitializeComponent();
        }
        public static string firmi;
        public void DisplayData()
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

           // con.Open();
            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     owner_industry.ownerid, owner_industry.ownerfname AS 'नाम', owner_industry.ownerlname AS 'थर', setup_district_1.distunicodename AS 'ठेगाना', setup_vdc.vdcunicodename AS 'गा.पा.। न.पा.', GetNumberToUnicode(owner_industry.ownervdcward) AS 'वार्ड', owner_industry.ownertole AS 'टोल',    setup_citizenissueoff.citizen_officeunicodename AS 'जारी गर्ने कार्यालय', setup_district.distunicodename AS 'जारी जिल्ला', GetNumberToUnicode(owner_industry.citzdate) AS 'जारी मिति',   GetNumberToUnicode(owner_industry.citznum) AS 'ना.प्र.नं.', setup_subcategory_1.subcategory_unicodename AS 'बाबु।पति', owner_industry.ownergname AS 'बावु।पति', setup_subcategory_2.subcategory_unicodename AS 'बाजे।ससुरा', owner_industry.ownerfgname AS 'बाजे।ससुरा', setup_subcategory.subcategory_unicodename AS 'लिंग', owner_industry.comment AS 'कैफियत', GetNumberToUnicode(owner_industry.contact) As 'सम्पर्क नं', owner_industry.email As 'ईमेल' FROM         owner_industry INNER JOIN setup_district ON owner_industry.cddist = setup_district.distcode INNER JOIN setup_citizenissueoff ON owner_industry.ccio = setup_citizenissueoff.citizen_officeid INNER JOIN setup_vdc ON owner_industry.ownervdccode = setup_vdc.VDC_SID INNER JOIN     setup_district AS setup_district_1 ON owner_industry.ownerdistcode = setup_district_1.distcode INNER JOIN        setup_subcategory ON owner_industry.gender = setup_subcategory.subcategory_id INNER JOIN      setup_subcategory AS setup_subcategory_1 ON owner_industry.ownergrel = setup_subcategory_1.subcategory_id INNER JOIN       setup_subcategory AS setup_subcategory_2 ON owner_industry.ownerfgrel = setup_subcategory_2.subcategory_id WHERE     (owner_industry.industryid =@darta)", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@darta", firmi.ToString());
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView1.DataSource = tb;
           
            dataGridView1.Columns[0].Visible = false;
           
            sqlcon.con.Close();

        }

        public void getdataa(string darta, string dist, string vdc, string vdcname,string id,string firmtype,string industrynames)
        {
            darta_txt.Text = null;
            darta_txt.Text = darta.ToString();
            district_txt.Text = null;
            district_txt.Text = dist.ToString();
            vdc_txt.Text = null;
            vdc_txt.Text = vdc.ToString();
            ward_txt.Text = vdcname.ToString();
            firmi = id.ToString();
            if (firmtype.ToString() == "93") 
            {
                label7.Text = "प्रा.लि.को प्रथम सञ्चालकहरुको विवरण प्रविष्टि/संशोधन";
                label2.Text = "प्रा.लि.को प्रथम सञ्चालकको विवरण";
            }


            //date_txt.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }

        public void FillDropDownList1(AutoCompleteStringCollection dataCollections)
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
           // string connectionString = null;
            //SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
           // connectionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";
            sql = "SELECT distcode, distunicodename FROM setup_district";
           // connection = new MySqlConnection(sqlcon.con);
            try
            {
                //connection.Open();
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
            // string connetionStrings = null;
            //SqlConnection connections;
            //connetionStrings = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";

           
           
            MySqlCommand commands;
            MySqlDataAdapter adapters = new MySqlDataAdapter();
            DataSet dss = new DataSet();

            string sqls = null;
            //int distcodevalue = Convert.ToInt16(district_combo.SelectedValue);
           
            //sqls = "SELECT VdcCode,VdcNepnam FROM  setup_vdc where DistCode='" +district_combo.SelectedValue+ "'";
            sqls = "SELECT VDC_SID,vdcunicodename FROM  setup_vdc where DistCode='" + owner_dist.SelectedValue + "'";


            //connections = new SqlConnection(connetionStrings);
            //connections.Open();
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

            //string connetionString = null;
            //SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            //connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;
            sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (setup_subcategory.category_id = 1)";
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
            //string connetionString = null;
            //SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            //connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;
            sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (setup_subcategory.category_id = 2)";
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

            //string connetionString = null;
            //SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            //connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;
            sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (setup_subcategory.category_id = 3)";
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
            //string connetionString = null;
            //SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            //connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;
            sql = "SELECT     citizen_officeid,citizen_officeunicodename  FROM  setup_citizenissueoff";
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
           // string connetionStrings = null;
            //SqlConnection connections;
            MySqlCommand commands;
            MySqlDataAdapter adapters = new MySqlDataAdapter();
            DataSet dss = new DataSet();

            string sqls = null;
            //int distcodevalue = Convert.ToInt16(district_combo.SelectedValue);
            //connetionStrings = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";

            //sqls = "SELECT VdcCode,VdcNepnam FROM  setup_vdc where DistCode='" +district_combo.SelectedValue+ "'";
            sqls = " SELECT     distcode, distunicodename FROM         setup_district WHERE     (distunicodename = @dist)";
            
            //connections = new SqlConnection(connetionStrings);
            
                //connections.Open();
                commands = new MySqlCommand(sqls, sqlcon.con);
                adapters.SelectCommand = commands;
                adapters.SelectCommand.Parameters.AddWithValue("@dist", dist.ToString());
                adapters.Fill(dss);
                adapters.Dispose();
                commands.Dispose();
                sqlcon.con.Close();


                foreach (DataRow row in dss.Tables[0].Rows)
                {
                    
                   comboBox1.SelectedValue= row[0].ToString();
                }
            
        }
        
         public void district(string dist)
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
          //  string connetionStrings = null;
            //SqlConnection connections;
            MySqlCommand commands;
            MySqlDataAdapter adapters = new MySqlDataAdapter();
            DataSet dss = new DataSet();

            string sqls = null;
            //int distcodevalue = Convert.ToInt16(district_combo.SelectedValue);
            //connetionStrings = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";

            //sqls = "SELECT VdcCode,VdcNepnam FROM  setup_vdc where DistCode='" +district_combo.SelectedValue+ "'";
            sqls = " SELECT     distcode, distunicodename FROM         setup_district WHERE     (distunicodename =@dist)";
            
            //connections = new SqlConnection(connetionStrings);
            
                //connections.Open();
                commands = new MySqlCommand(sqls, sqlcon.con);
                adapters.SelectCommand = commands;
                adapters.SelectCommand.Parameters.AddWithValue("@dist", dist.ToString());
                adapters.Fill(dss);
                adapters.Dispose();
                commands.Dispose();
                sqlcon.con.Close();


                foreach (DataRow row in dss.Tables[0].Rows)
                {
                    
                   owner_dist.SelectedValue= row[0].ToString();
                }
            
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

       

        private void ownerindustry_Load(object sender, EventArgs e)
        {
            button4.Enabled = false;
            button2.Enabled = false;
            button5.Enabled = false;
            panel2.Enabled = false;
            DisplayData();
            // TODO: This line of code loads data into the 'cSIODataSet1.setup_district' table. You can move, or remove it, as needed.
            // this.setup_districtTableAdapter.Fill(this.cSIODataSet1.setup_district);

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
            owner_vdc.SelectedValue = 4260;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;
            button2.Enabled = true;
                    button5.Enabled = false;
                    
            DisplayData();
            panel2.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!jari_dat.MaskFull)
                {

                    MessageBox.Show("मिति पुरा राख्नुहोस ।");


                }
                else
                {
                    //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

                    if (sqlcon.con.State == ConnectionState.Closed)
                    {
                        sqlcon.con.Open();
                    }// con.Open();
                    //string firmi;
                    ////////////////////////////////////////
                    //MySqlCommand command;
                    //MySqlDataAdapter adapter = new MySqlDataAdapter();
                    //DataSet ds = new DataSet();
                    //string sql = "SELECT   industryid from industryreg where industryregno='" + darta_txt.Text + "' ";

                    ////string sql = "SELECT VdcCode, VdcNepnam FROM setup_vdc where setup_vdc.DistCode='" + comboBox3.ValueMember + "'";



                    //command = new SqlCommand(sql, con);
                    //adapter.SelectCommand = command;
                    //adapter.Fill(ds);
                    //adapter.Dispose();
                    //command.Dispose();


                    //foreach (DataRow row in ds.Tables[0].Rows)
                    //{
                    //    firmi = row[0].ToString();

                    //   // MessageBox.Show(firmi.ToString());
                    //}
                    // MessageBox.Show(firmi.ToString());

                    ////////////////////////////


                    string curdate = DateTime.Now.ToString();




                    MySqlCommand cmd = new MySqlCommand("INSERT INTO owner_industry(industryid ,industryregno, ccio, cddist, citzdate, citznum, gender, ownerfname, ownerlname, ownerdistcode, ownervdccode, ownervdcward, ownertole, ownergrel, ownerfgrel, ownergname, ownerfgname, comment, updnepdate, upduser,contact,email,csioid)VALUES(@firmid,GetUnicodeToNumber(@darta),@citizenoff,@jaridistrict,GetUnicodeToNumber(@jaridate),GetUnicodeToNumber(@citizennum),@gender,@ownerfname,@ownerlname,@ownerdistrict,@ownervdc,GetUnicodeToNumber(@ownerward),@ownertole,@parentone,@parenttwo,@parentonename,@parenttwoname,@comment,GetUnicodeToNumber(@nepdate),@username,GetUnicodeToNumber(@contact),@email,@csioid)");




                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@firmid", firmi);
                    cmd.Parameters.AddWithValue("@darta", darta_txt.Text);
                    cmd.Parameters.Add(new MySqlParameter("@citizenoff", citizen_off.SelectedValue));
                    cmd.Parameters.Add(new MySqlParameter("@jaridistrict", comboBox1.SelectedValue));
                    cmd.Parameters.Add(new MySqlParameter("@jaridate", jari_dat.Text));
                    cmd.Parameters.Add(new MySqlParameter("@citizennum", citizen_num.Text));
                    cmd.Parameters.Add(new MySqlParameter("@gender", gender.SelectedValue));
                    cmd.Parameters.Add(new MySqlParameter("@ownerfname", owner_name.Text));
                    cmd.Parameters.Add(new MySqlParameter("@ownerlname", owner_lname.Text));
                    cmd.Parameters.Add(new MySqlParameter("@ownerdistrict", owner_dist.SelectedValue));
                    cmd.Parameters.Add(new MySqlParameter("@ownervdc", owner_vdc.SelectedValue));
                    cmd.Parameters.Add(new MySqlParameter("@ownerward", owner_ward.Text));
                    cmd.Parameters.Add(new MySqlParameter("@ownertole", owner_tole.Text));
                    cmd.Parameters.Add(new MySqlParameter("@parentone", parentone.SelectedValue));
                    cmd.Parameters.Add(new MySqlParameter("@parenttwo", parenttwo.SelectedValue));
                    cmd.Parameters.Add(new MySqlParameter("@parentonename", owner_parent1name.Text));
                    cmd.Parameters.Add(new MySqlParameter("@parenttwoname", owner_parent2name.Text));
                    cmd.Parameters.Add(new MySqlParameter("@comment", comment.Text));
                    cmd.Parameters.Add(new MySqlParameter("@contact", contact_txt.Text));
                    cmd.Parameters.Add(new MySqlParameter("@email", email_txt.Text));
                    // cmd.Parameters.Add(new SqlParameter("@curdate", curdate));
                    cmd.Parameters.Add(new MySqlParameter("@nepdate", global.nepalidate));
                    cmd.Parameters.Add(new MySqlParameter("@username", global.username));
                    cmd.Parameters.AddWithValue("@csioid", global.csioid);
                    cmd.Connection = sqlcon.con;

                    //qr.Parameters.AddWithValue("@firmid", firmi);
                    // SqlCommand qr = new SqlCommand("INSERT INTO banijyaform('firmregno',[firmdist],[firmvdc] ,[firmward],tole ,[regdat],[renewdate],[firmscope],[firmnepname],[karobar],[revenue],[branch],comment,[usr],[updatdate],[updatnepdate])VALUES('" + darta_txt.Text + "','" + district_combo.SelectedValue + "','" + vdc_combo.SelectedValue + "','" + textBox1.Text + "','" + tole_txt.Text + "','" + darta_date.Text + "','" + renew_date.Text + "','" + firm_obj.SelectedValue + "','" + firm_name.Text + "','" + firm_karobar.Text + "','" + firm_capital.Text + "','" + branch_txt.Text + "','" + comment.Text + "','" + usr.Text + "','" + curdate + "','" + entry_nepdate.Text + "')", con);    
                    //qr.Connection = con;
                    int n = cmd.ExecuteNonQuery();

                    if (n > 0)
                    {
                        MessageBox.Show("प्रोपाइटरको विवरण सफलता पुर्वक सेभ भयो ।");
                        sqlcon.con.Close();
                        dataGridView1.DataSource = null;
                        DisplayData();
                        cleardata();
                        button2.Enabled = false;
                        button5.Enabled = false;
                        panel2.Enabled = false;
                        button1.Enabled = true;



                    }
                    //INSERT INTO [CSIO].[dbo].[owner] ('firmid','firmregno','ccio','cddist','citzdate','citznum','gender','ownerfname','ownerlname','ownerdistcode','ownervdccode','ownervdcward','ownertole','ownergrel','ownerfgrel','ownergname','ownerfgname',comment,[upddate],'updnepdate','upduser') VALUES(<firmid, int,>,<firmregno, nvarchar(15),>,<ccio, int,>,<cddist, int,>,<citzdate, nchar(10),>,<citznum, nvarchar(20),>,<gender, int,>,<ownerfname, nvarchar(50),>,<ownerlname, nvarchar(50),>,<ownerdistcode, tinyint,>,<ownervdccode, int,>,<ownervdcward, int,>,<ownertole, nvarchar(25),>,<ownergrel, int,>,<ownerfgrel, int,>,<ownergname, nvarchar(50),>     ,<ownerfgname, nvarchar(50),> ,<comment, nvarchar(50),>,<upddate, datetime,>,<updnepdate, nchar(10),>,<upduser, nchar(10),>)}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
         public void vdcedit(string vdc)
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
           // string connetionStrings = null;
            //SqlConnection connections;
            MySqlCommand commands;
            MySqlDataAdapter adapters = new MySqlDataAdapter();
            DataSet dss = new DataSet();

            string sqls = null;
            //int distcodevalue = Convert.ToInt16(district_combo.SelectedValue);
            //connetionStrings = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";

            //sqls = "SELECT VdcCode,VdcNepnam FROM  setup_vdc where DistCode='" +district_combo.SelectedValue+ "'";
            sqls = "SELECT VDC_SID,vdcunicodename FROM  setup_vdc where vdcunicodename=@vdc";


            //connections = new SqlConnection(connetionStrings);
            try
            {
                //connections.Open();
                commands = new MySqlCommand(sqls, sqlcon.con);
                adapters.SelectCommand = commands;
                adapters.SelectCommand.Parameters.AddWithValue("@vdc", vdc.ToString());
                adapters.Fill(dss);
                adapters.Dispose();
                commands.Dispose();
                sqlcon.con.Close();

                //vdc_combo.DataSource = dss.Tables[0];
                //vdc_combo.ValueMember = "VDC_SID";
                //vdc_combo.DisplayMember = "VdcNepnam";
                
                foreach (DataRow row in dss.Tables[0].Rows)
                {
                   owner_vdc.SelectedValue = row[0].ToString();
                }
               // vdc_combo.SelectedItem = vdc.ToString();

            }
            catch
            {
                //MessageBox.Show("Can not open connection ! inside vdc "+ex);
            }

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
         public void citzoffedit(string firmobj)
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
            sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (subcategory_unicodename = @firmobj)";
            //connection = new SqlConnection(connetionString);
            try
            {
                //connection.Open();
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                adapter.SelectCommand.Parameters.AddWithValue("@firmobj", firmobj.ToString());
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                sqlcon.con.Close();

                //firm_obj.DataSource = ds.Tables[0];
                //firm_obj.ValueMember = "subcategory_id";
                //firm_obj.DisplayMember = "subcategory_nepname";
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    citizen_off.SelectedValue=row[0].ToString();
                    //citizen_off.Items.Add(row[0].ToString()+" Y "+row[1].ToString());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }
         public void forefatheredit(string firmobj)
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
            sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (subcategory_unicodename = @firmobj)";
            //connection = new SqlConnection(connetionString);
            try
            {
                //connection.Open();
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                adapter.SelectCommand.Parameters.AddWithValue("@firmobj", firmobj.ToString());
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                sqlcon.con.Close();

                //firm_obj.DataSource = ds.Tables[0];
                //firm_obj.ValueMember = "subcategory_id";
                //firm_obj.DisplayMember = "subcategory_nepname";
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    parenttwo.SelectedValue=row[0].ToString();
                    //citizen_off.Items.Add(row[0].ToString()+" Y "+row[1].ToString());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }
         public void genderedit(string firmobj)
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
             sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (subcategory_unicodename = @firmobj)";
             //connection = new SqlConnection(connetionString);
             try
             {
                 //connection.Open();
                 command = new MySqlCommand(sql, sqlcon.con);
                 adapter.SelectCommand = command;
                 adapter.SelectCommand.Parameters.AddWithValue("@firmobj", firmobj.ToString());
                 adapter.Fill(ds);
                 adapter.Dispose();
                 command.Dispose();
                 sqlcon.con.Close();

                 //firm_obj.DataSource = ds.Tables[0];
                 //firm_obj.ValueMember = "subcategory_id";
                 //firm_obj.DisplayMember = "subcategory_nepname";
                 foreach (DataRow row in ds.Tables[0].Rows)
                 {
                     gender.SelectedValue = row[0].ToString();
                     //citizen_off.Items.Add(row[0].ToString()+" Y "+row[1].ToString());
                 }


             }
             catch (Exception ex)
             {
                 MessageBox.Show("Can not open connection ! " + ex);
             }

         }
        public void fatheredit(string firmobj)
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
            sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (subcategory_unicodename = @firmobj)";
            //connection = new SqlConnection(connetionString);
            try
            {
                //connection.Open();
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                adapter.SelectCommand.Parameters.AddWithValue("@firmobj", firmobj.ToString());
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                sqlcon.con.Close();

                //firm_obj.DataSource = ds.Tables[0];
                //firm_obj.ValueMember = "subcategory_id";
                //firm_obj.DisplayMember = "subcategory_nepname";
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    parentone.SelectedValue=row[0].ToString();
                    //citizen_off.Items.Add(row[0].ToString()+" Y "+row[1].ToString());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            button4.Enabled = true;
            button2.Enabled = false;
                    button5.Enabled = true;
                    panel2.Enabled = true;
            int i;
            //// string distcode=null;
            ////district_combo.SelectedText=null;
            ////vdc_combo.SelectedText = " ";
            ////industry_scale.SelectedText = " ";
            ////industry_type.SelectedText = " ";
            ////firm_obj.SelectedText = " ";
            ////textBox3.Text = " ";
            ////textBox4.Text = " ";
            // qr.Parameters.AddWithValue("@firmid", firmi);
            //    qr.Parameters.AddWithValue("@darta", darta_txt.Text);
            //    qr.Parameters.AddWithValue("@citizenoff", citizen_off.SelectedValue);
            //    qr.Parameters.AddWithValue("@citizendist", comboBox1.SelectedValue);
            //    qr.Parameters.AddWithValue("@jaridate", jari_dat.Text);
            //    qr.Parameters.AddWithValue("@citizennum", citizen_num.Text);
            //    qr.Parameters.AddWithValue("@gender", gender.SelectedValue);
            //    qr.Parameters.AddWithValue("@ownerfname", owner_name.Text);
            //    qr.Parameters.AddWithValue("@ownerlname", owner_lname.Text);
            //    qr.Parameters.AddWithValue("@ownerdist", owner_dist.SelectedValue);
            //    qr.Parameters.AddWithValue("@ownervdc", owner_vdc.SelectedValue);
            //    qr.Parameters.AddWithValue("@ownerward", owner_ward.Text);
            //    qr.Parameters.AddWithValue("@ownertole", owner_tole.Text);
            //    qr.Parameters.AddWithValue("@parentone", parentone.SelectedValue);
            //    qr.Parameters.AddWithValue("@parenttwo", parenttwo.SelectedValue);
            //    qr.Parameters.AddWithValue("@parentonename", owner_parent1name.Text);
            //    qr.Parameters.AddWithValue("@parenttwoname", owner_parent2name.Text);
            //    qr.Parameters.AddWithValue("@comment", comment.Text);
            //    qr.Parameters.AddWithValue("@curdate", curdate);
            //    qr.Parameters.AddWithValue("@nepdate", global.nepalidate);
            //    qr.Parameters.AddWithValue("@user", global.username);

            //SELECT owner_industry.ownerid,    owner_industry.ownerfname AS 'नाम', owner_industry.ownerlname AS 'थर', setup_district.distnepname AS 'जारी जिल्ला', owner_industry.citzdate AS 'जारी मिति',                       owner_industry.citznum AS 'ना.प्र.नं.', setup_district_1.distnepname AS 'ठेगाना', setup_vdc.VdcNepnam AS 'गा.पा.। न.पा.', owner_industry.ownertole AS 'टोल',                       owner_industry.ownergname AS 'बावु।पति', owner_industry.ownerfgname AS 'बाजे।ससुरा', owner_industry.comment AS 'कैफियत'
                     
            i = dataGridView1.CurrentRow.Index;
            
            id_label.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            id_label.Visible=false;
            owner_name.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            owner_lname.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            district(dataGridView1.Rows[i].Cells[3].Value.ToString());
           // jari_dat.Text=dataGridView1.Rows[i].Cells[4].Value.ToString();
            vdcedit(dataGridView1.Rows[i].Cells[4].Value.ToString());
            owner_ward.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
           owner_tole.Text=dataGridView1.Rows[i].Cells[6].Value.ToString();
            //ownerdist(dataGridView1.Rows[i].Cells[6].Value.ToString());
           
            
            
            citzoffedit(dataGridView1.Rows[i].Cells[7].Value.ToString());

            citzdistedit(dataGridView1.Rows[i].Cells[8].Value.ToString());
            //industry_scale.SelectedText = dataGridView1.Rows[i].Cells[8].Value.ToString();
            jari_dat.Text=dataGridView1.Rows[i].Cells[9].Value.ToString();
            //industry_type.SelectedText = dataGridView1.Rows[i].Cells[9].Value.ToString();
            citizen_num.Text=dataGridView1.Rows[i].Cells[10].Value.ToString();
            //firm_obj.SelectedText = dataGridView1.Rows[i].Cells[10].Value.ToString();
            fatheredit(dataGridView1.Rows[i].Cells[11].Value.ToString());
            owner_parent1name.Text = dataGridView1.Rows[i].Cells[12].Value.ToString();
           forefatheredit(dataGridView1.Rows[i].Cells[13].Value.ToString());
            owner_parent2name.Text = dataGridView1.Rows[i].Cells[14].Value.ToString();
            genderedit(dataGridView1.Rows[i].Cells[15].Value.ToString());
            // firm_turnover.Text = dataGridView1.Rows[i].Cells[15].Value.ToString();
            comment.Text = dataGridView1.Rows[i].Cells[16].Value.ToString();
            contact_txt.Text = dataGridView1.Rows[i].Cells[17].Value.ToString();
            email_txt.Text = dataGridView1.Rows[i].Cells[18].Value.ToString();
            //textBox4.Text = dataGridView1.Rows[i].Cells[17].Value.ToString();
            //fworker.Text = dataGridView1.Rows[i].Cells[18].Value.ToString();
            //maleworker.Text = dataGridView1.Rows[i].Cells[19].Value.ToString();
            //tax_txt.Text = dataGridView1.Rows[i].Cells[20].Value.ToString();
            //comment.Text = dataGridView1.Rows[i].Cells[21].Value.ToString();
        }
        public void cleardata()
        {
           // darta_txt.Text = null;
            // district_combo.SelectedValue=40;
          id_label.Text=null;
               // qr.Parameters.AddWithValue("@darta", darta_txt.Text);
               // qr.Parameters.AddWithValue("@citizenoff", citizen_off.SelectedValue);
               // qr.Parameters.AddWithValue("@citizendist", comboBox1.SelectedValue);
                //qr.Parameters.AddWithValue("@jaridate", jari_dat.Text);
                citizen_num.Text=null;
               // qr.Parameters.AddWithValue("@gender", gender.SelectedValue);
                owner_name.Text=null;
                owner_lname.Text=null;
                //qr.Parameters.AddWithValue("@ownerdist", owner_dist.SelectedValue);
                //qr.Parameters.AddWithValue("@ownervdc", owner_vdc.SelectedValue);
               owner_ward.Text=null;
                owner_tole.Text=null;
               // qr.Parameters.AddWithValue("@parentone", parentone.SelectedValue);
                //qr.Parameters.AddWithValue("@parenttwo", parenttwo.SelectedValue);
                owner_parent1name.Text=null;
                owner_parent2name.Text=null;
                comment.Text=null;
                contact_txt.Text = null;
                email_txt.Text = null;
               
        }


        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result1 = MessageBox.Show("विवरण संशोधन गर्ने हो ?",
           "उद्योगको विवरण संशोधन गर्न",
           MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result1 == DialogResult.Yes)
                {
                    if (sqlcon.con.State == ConnectionState.Closed)
                    {
                        sqlcon.con.Open();
                    }
                    //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");
                    string curdate = DateTime.Today.ToString();
                    // con.Open();

                    MySqlCommand qr = new MySqlCommand(" UPDATE owner_industry SET ccio = @citizenoff,cddist = @citizendist,citzdate = GetUnicodeToNumber(@jaridate),citznum = GetUnicodeToNumber(@citizennum),gender = @gender,ownerfname = @ownerfname,ownerlname = @ownerlname,ownerdistcode = @ownerdist,ownervdccode = @ownervdc,ownervdcward = GetUnicodeToNumber(@ownerward),ownertole = @ownertole,ownergrel = @parentone,ownerfgrel = @parenttwo,ownergname = @parentonename,ownerfgname = @parenttwoname,comment = @comment,upddate = @curdate,updnepdate = @nepdate,upduser = @user, contact=@contact, email=@email  WHERE ownerid=@ownid");


                    qr.CommandType = CommandType.Text;

                    qr.Parameters.AddWithValue("@ownid", id_label.Text);
                    // qr.Parameters.AddWithValue("@darta", darta_txt.Text);
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
                    qr.Parameters.AddWithValue("@curdate", curdate);
                    qr.Parameters.AddWithValue("@nepdate", global.nepalidate);
                    qr.Parameters.AddWithValue("@user", global.username);
                    qr.Parameters.Add(new MySqlParameter("@contact", contact_txt.Text));
                    qr.Parameters.Add(new MySqlParameter("@email", email_txt.Text));
                    qr.Connection = sqlcon.con;


                    //int n = qr.ExecuteNonQuery();



                    //int n = qr.ExecuteNonQuery();
                    int n = qr.ExecuteNonQuery();

                    if (n > 0)
                    {

                        sqlcon.con.Close();
                        dataGridView1.DataSource = null;
                        DisplayData();
                        cleardata();
                        MessageBox.Show("संशोधन सफल भयो ।", "संशोधन", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        button2.Enabled = false;
                        button5.Enabled = false;
                        panel2.Enabled = false;


                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result1 = MessageBox.Show("प्रोपाइटरको विवरण हटाउने हो ?",
         "प्रोपाइटरको विवरण हटाउन",
         MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result1 == DialogResult.Yes)
                {
                    if (sqlcon.con.State == ConnectionState.Closed)
                    {
                        sqlcon.con.Open();
                    }
                    //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");
                    string curdate = DateTime.Today.ToString();
                    //con.Open();

                    MySqlCommand qr = new MySqlCommand(" DELETE FROM owner_industry  WHERE ownerid=@ownid");


                    qr.CommandType = CommandType.Text;

                    qr.Parameters.AddWithValue("@ownid", id_label.Text);
                    qr.Connection = sqlcon.con;


                    //int n = qr.ExecuteNonQuery();
                    int n = qr.ExecuteNonQuery();

                    if (n > 0)
                    {

                        sqlcon.con.Close();
                        dataGridView1.DataSource = null;
                        DisplayData();
                        cleardata();
                        MessageBox.Show("प्रोपाइटरको विवरण सफलता पुर्वक हटाइयो ।", "विवरण हटाइएको", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        button2.Enabled = false;
                        button5.Enabled = false;
                        panel2.Enabled = false;


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                //int i;
                ////  string ind;
                //i = dataGridView1.CurrentRow.Index;
                //if (i < 0)
                //{
                //    MessageBox.Show("माथी 
                //}
                // id_txt.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                // MessageBox.Show(dataGridView1.Rows[i].Cells[9].Value.ToString());
                //  getindustrytype(dataGridView1.Rows[i].Cells[9].Value.ToString());
                // MessageBox.Show(ind_type.Text);
                //industryfileupload ownrindst = new industryfileupload();
               // ownrindst.MdiParent = this.MdiParent;

              //  ownrindst.getdataa(darta_txt.Text, district_txt.Text, vdc_txt.Text, ward_txt.Text, firmi.ToString());
                this.Hide();

               // ownrindst.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }

       
    }
