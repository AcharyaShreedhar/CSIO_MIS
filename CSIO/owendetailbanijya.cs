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
    public partial class owendetailbanijya : Form
    {
        public static string firmi;
        public static string oldmember;
        public static string newmember;
        public static string transid;
        public owendetailbanijya()

        {
            
            InitializeComponent();
        }
        public int transtype = 0;
        public int opttype = 0;
        public string firmtypes;
        public void dartanumber()
        {
           

            /////////////////////////////
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }

            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT   firmregno from banijyaform where firmid= @darta", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@darta", firmi.ToString());
            DataTable tb = new DataTable();
            qry.Fill(tb);
                    
            foreach (DataRow row in tb.Rows)
            {
                darta_txt.Text = row["firmregno"].ToString();
            }
            // MessageBox.Show(oldmember.ToString());
            //dataGridView1.Columns[0].Visible = false;
            //  dataGridView1.Columns[1].Visible = false;

            sqlcon.con.Close();
        }
        public void DisplayNewData()
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }

            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     owner.ownerid, owner.ownerfname AS 'नाम', owner.ownerlname AS 'थर', setup_district_1.distunicodename AS 'ठेगाना', setup_vdc.Vdcunicodename AS 'गा.पा.। न.पा.', GetNumberToUnicode(owner.ownervdcward) AS 'वार्ड', owner.ownertole AS 'टोल',    setup_citizenissueoff.citizen_officeunicodename AS 'जारी गर्ने कार्यालय', setup_district.distunicodename AS 'जारी जिल्ला', GetNumberToUnicode(owner.citzdate) AS 'जारी मिति',   GetNumberToUnicode(owner.citznum) AS 'ना.प्र.नं.', setup_subcategory_1.subcategory_unicodename AS 'बाबु।पति', owner.ownergname AS 'बावु।पति', setup_subcategory_2.subcategory_unicodename AS 'बाजे।ससुरा', owner.ownerfgname AS 'बाजे।ससुरा', setup_subcategory.subcategory_unicodename AS 'लिंग', owner.comment AS 'कैफियत', owner.contact As 'सम्पर्क नं', owner.email As 'ईमेल' FROM         owner INNER JOIN setup_district ON owner.cddist = setup_district.distcode INNER JOIN setup_citizenissueoff ON owner.ccio = setup_citizenissueoff.citizen_officeid INNER JOIN setup_vdc ON owner.ownervdccode = setup_vdc.VDC_SID INNER JOIN     setup_district AS setup_district_1 ON owner.ownerdistcode = setup_district_1.distcode INNER JOIN        setup_subcategory ON owner.gender = setup_subcategory.subcategory_id INNER JOIN      setup_subcategory AS setup_subcategory_1 ON owner.ownergrel = setup_subcategory_1.subcategory_id INNER JOIN       setup_subcategory AS setup_subcategory_2 ON owner.ownerfgrel = setup_subcategory_2.subcategory_id WHERE     (owner.firmid = @darta)", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@darta", firmi.ToString());
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView1.DataSource = tb;
            if (firmtypes == "62")
            {
                newmember = "साझेदार विवरण:";
            }
            else
            {
                newmember = "प्रोपाइटर विवरण:";
            }
            foreach (DataRow row in tb.Rows)
            {
                newmember = newmember + " • " + row["नाम"].ToString() + " " + row["थर"].ToString() + ", " + row["गा.पा.। न.पा."].ToString() + " " + row["वार्ड"].ToString() + ", " + row["जारी जिल्ला"].ToString() + " ";
            }
            // MessageBox.Show(oldmember.ToString());
            dataGridView1.Columns[0].Visible = false;
          //  dataGridView1.Columns[1].Visible = false;

            sqlcon.con.Close();

        }
        public void DisplayData()
        {
            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }

          
            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     owner.ownerid, owner.ownerfname AS 'नाम', owner.ownerlname AS 'थर', setup_district_1.distunicodename AS 'ठेगाना', setup_vdc.Vdcunicodename AS 'गा.पा.। न.पा.', GetNumberToUnicode(owner.ownervdcward) AS 'वार्ड', owner.ownertole AS 'टोल',    setup_citizenissueoff.citizen_officeunicodename AS 'जारी गर्ने कार्यालय', setup_district.distunicodename AS 'जारी जिल्ला', GetNumberToUnicode(owner.citzdate) AS 'जारी मिति',   GetNumberToUnicode(owner.citznum) AS 'ना.प्र.नं.', setup_subcategory_1.subcategory_unicodename AS 'बाबु।पति', owner.ownergname AS 'बावु।पति', setup_subcategory_2.subcategory_unicodename AS 'बाजे।ससुरा', owner.ownerfgname AS 'बाजे।ससुरा', setup_subcategory.subcategory_unicodename AS 'लिंग', owner.comment AS 'कैफियत', owner.contact As 'सम्पर्क नं', owner.email As 'ईमेल' FROM         owner INNER JOIN setup_district ON owner.cddist = setup_district.distcode INNER JOIN setup_citizenissueoff ON owner.ccio = setup_citizenissueoff.citizen_officeid INNER JOIN setup_vdc ON owner.ownervdccode = setup_vdc.VDC_SID INNER JOIN     setup_district AS setup_district_1 ON owner.ownerdistcode = setup_district_1.distcode INNER JOIN        setup_subcategory ON owner.gender = setup_subcategory.subcategory_id INNER JOIN      setup_subcategory AS setup_subcategory_1 ON owner.ownergrel = setup_subcategory_1.subcategory_id INNER JOIN       setup_subcategory AS setup_subcategory_2 ON owner.ownerfgrel = setup_subcategory_2.subcategory_id WHERE     (owner.firmid = @darta)", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@darta", firmi.ToString());
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView1.DataSource = tb;
            if (firmtypes == "62")
            {
                oldmember = "साझेदार विवरण:";
            }
            else
            {
                oldmember = "प्रोपाइटर विवरण:";
            }
            foreach (DataRow row in tb.Rows)
            {
                oldmember = oldmember + " • " + row["नाम"].ToString() + " " + row["थर"].ToString() + ", " + row["गा.पा.। न.पा."].ToString() + " " + row["वार्ड"].ToString() + ", " + row["जारी जिल्ला"].ToString() + " ";
            }
            dataGridView1.Columns[0].Visible = false;
        
            //dataGridView1.Columns[18]. = new Font(DataGridView.DefaultFont.FontFamily.Name, FontStyle.Italic);
            //dataGridView1.Rows[dataGridView1.Rows.Count].Cells[18].Style.Font = new Font("Arial", 12, FontStyle.Bold | FontStyle.Underline);
            //dataGridView1.Rows[1].Cells[18].Style.Font = new Font("Arial", 12, FontStyle.Bold);
            
            // dataGridView1.Columns(18).DefaultCellStyle.Font = New Font(""Tahoma"", 15, FontStyle.Regular); 
            sqlcon.con.Close();
            
        }  

        public void getdataa(string darta, string firmname,  string id, string firmtype)
        {
            darta_txt.Text = null;
            darta_txt.Text = darta.ToString();
            firmname_txt.Text = null;
            firmname_txt.Text = firmname.ToString();
            //vdc_txt.Text = null;
            //vdc_txt.Text = vdc.ToString();
            //ward_txt.Text = vdcname.ToString();
            firmi = id.ToString();
            firmtypes = firmtype.ToString();
            if (firmtypes == "61")
            {
                label2.Text = "प्रोपाईटरको बिवरण";
                new_btn.Text = "नयाँ प्रोपाईटर";
            }
            else
            {
                label2.Text = "साझेदारको बिवरण";
                new_btn.Text = "नयाँ साझेदार";
            }
            //date_txt.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }
        public void getid(int id, string firmname, string darta, string firmtype,int opttypes,string updid,string decdate)
        {
            save_btn.Enabled = false;
            darta_txt.Text = null;
            //darta_txt.Text = darta.ToString();
            darta_no.Text = darta.ToString();
            firmname_txt.Text = null;
            firmname_txt.Text = firmname.ToString();
            opttype = opttypes;
            updateid.Text = updid.ToString();
            d_date.Text = decdate.ToString();
            if (updateid.Text == "135")
            {
                label2.Text = "साझेदार थप/घट";
                new_btn.Text = "साझेदार थप";
                delete_btn.Text = "साझेदार हटाउनुहोस";
            }
            //vdc_txt.Text = null;
            //vdc_txt.Text = vdc.ToString();
            //ward_txt.Text = vdcname.ToString();
            firmi = id.ToString();
            firmtypes = firmtype.ToString();
            dartanumber();
            if (firmtypes == "61")
            {
                label2.Text = "प्रोपाईटरको बिवरण";
                new_btn.Text = "नयाँ प्रोपाईटर";
            }
            //else
            //{
            //    label2.Text = "साझेदारको बिवरण";
            //    new_btn.Text = "नयाँ साझेदार";
            //    delete_btn.Text = "साझेदार हटाउनुहोस";
            //}
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
           // connection = new SqlConnection(connectionString);
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
            MySqlCommand commands;
            MySqlDataAdapter adapters = new MySqlDataAdapter();
            DataSet dss = new DataSet();

            string sqls = null;
            //int distcodevalue = Convert.ToInt16(district_combo.SelectedValue);
            //connetionStrings = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";

            // MessageBox.Show("Value of DID=" + district_combo.SelectedValue);
            sqls = "SELECT VDC_SID,vdcunicodename FROM  setup_vdc where DistCode=@dist";
            // sqls = "SELECT VdcCode,VdcNepnam FROM  setup_vdc where DistCode=distcodevalue";


            //connections = new SqlConnection(connetionStrings);
            try
            {
                //connections.Open();
                commands = new MySqlCommand(sqls, sqlcon.con);
                adapters.SelectCommand = commands;
                adapters.SelectCommand.Parameters.AddWithValue("@dist", owner_dist.SelectedValue);
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
                //MessageBox.Show("Can not open connection ! inside vdc ");
            }



            ////////////////////////////////////////////////////////////////////////////

            //string connectionString = null;
            ////SqlConnection connection;
            //MySqlCommand command;
            //MySqlDataAdapter adapter = new MySqlDataAdapter();
            //DataSet ds = new DataSet();
            
            //string sql = null;
            
            //connectionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.passwordname + "'";

            //sql = "SELECT     VDC_SID,  Vdcunicodename FROM  setup_vdc where DistCode='" + owner_dist.SelectedValue+ "'";

            //connection = new SqlConnection(connectionString);
            ////connection.Open();
            //try
            //{
                
            //    command = new MySqlCommand(sql, sqlcon.con);
            //    adapter.SelectCommand = command;
            //    adapter.Fill(ds);
            //    adapter.Dispose();
            //    command.Dispose();
            //    sqlcon.con.Close();

            //    owner_vdc.DataSource = ds.Tables[0];
            //    owner_vdc.ValueMember = "VDC_SID";
            //    owner_vdc.DisplayMember = "Vdcunicodename";
            //    foreach (DataRow row in ds.Tables[0].Rows)
            //    {
            //        dataCollectionvdc.Add(row[1].ToString());
            //    }


            //}
            //catch
            //{
            //   MessageBox.Show("Connection error"); 
            //}

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
        public void district(string dist)
        {

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            //string connetionStrings = null;
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

                owner_dist.SelectedValue = row[0].ToString();
            }

        }
        public void vdcedit(string vdc)
        {

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            //string connetionStrings = null;
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
                    citizen_off.SelectedValue = row[0].ToString();
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
                    parenttwo.SelectedValue = row[0].ToString();
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
                    parentone.SelectedValue = row[0].ToString();
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
                citizen_off.DisplayMember = "citizen_officeunicodename" ;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    dataCollection.Add( row[1].ToString());
                    //citizen_off.Items.Add(row[0].ToString()+" Y "+row[1].ToString());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! "+ex);
            }

        }
       
        //public class Item
       // {
        //    string Name { get; set; }
        //    string Value { get; set; }
        //}
        
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

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
                MessageBox.Show("कनेक्सन मा समस्या ! ");
            }

        }


        private void owendetailbanijya_Load(object sender, EventArgs e)
        {
            user_txt.Text = global.username;
            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            //con.Open();
            delete_btn.Enabled = false;
           // button5.Enabled = false;

            DisplayData();

            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection combData = new AutoCompleteStringCollection();
            FillDropDownList(combData);
            comboBox1.AutoCompleteCustomSource = combData;

            //comboBox1.SelectedValue = 40;

            owner_dist.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            owner_dist.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection dstrict = new AutoCompleteStringCollection();
            FillDropDownList1(dstrict);
            owner_dist.AutoCompleteCustomSource = dstrict;
            owner_dist.SelectedValue = 40;

       

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


           
            /////////////////////////////////////////


        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void name_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel8_Paint(object sender, PaintEventArgs e)
        {

        }
        public void checksajhedar()
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }


            ///////

            MySqlDataAdapter query = new MySqlDataAdapter("select * from owner where firmid=@firmname", sqlcon.con);
            query.SelectCommand.Parameters.AddWithValue("@firmname",firmi.ToString().Trim());
            DataTable tb = new System.Data.DataTable();
            query.Fill(tb);
            if (tb.Rows.Count > 0)
            {


                MessageBox.Show("प्राईभेट फर्ममा एक भन्दा बढी प्रोपाईटर ईन्ट्री गर्न मिल्दैन।", "प्रोपाईटर पहिले नै इन्ट्री सम्बन्धमा", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                save_btn.Enabled = false;




            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            save_btn.Enabled = true;
            panel2.Enabled = true;
            transtype = 1;
            cleardata();
            if (firmtypes == "61")
            {
               // checksajhedar();
            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label18_Click_1(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        public void transactionid()
        {
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }

                MySqlCommand cmd = new MySqlCommand("select industryregno, fiscalyear, officeid, officedist from setup_industryregid where industryregid=4", sqlcon.con);
                cmd.CommandType = CommandType.Text;
                // cmd.Connection = con;
                MySqlDataReader sdr = cmd.ExecuteReader();
                sdr.Read();
                transid = global.csioid.ToString().Trim() + sdr["industryregno"].ToString().Trim();

                sdr.Close(); //closing data reader
                sqlcon.con.Close();

                // if (sqlcon.con.State == ConnectionState.Closed)
                sqlcon.con.Open();

                MySqlCommand cmo = new MySqlCommand("update setup_industryregid SET industryregno= industryregno+1 WHERE industryregid=4", sqlcon.con);
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
            finally
            {
                sqlcon.con.Close();
            }

        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (opttype == 2)
            {
                sajhedarigaht();
            }
            else
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

                MySqlCommand qr = new MySqlCommand(" DELETE FROM owner  WHERE ownerid=@ownid");


                qr.CommandType = CommandType.Text;

                qr.Parameters.AddWithValue("@ownid", id_label.Text);
                qr.Connection = sqlcon.con;


                //int n = qr.ExecuteNonQuery();
                int n = qr.ExecuteNonQuery();

                if (n > 0)
                {
                        global.sync_dblog("owner", id_label.Text, "DELETE", id_label.Text, "ownerid");
                        sqlcon.con.Close();
                    dataGridView1.DataSource = null;
                    DisplayData();
                    cleardata();
                    MessageBox.Show("संशोधन सफल भयो ।", "संशोधन", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    save_btn.Enabled = false;
                    // button5.Enabled = false;
                    panel2.Enabled = false;


                }
            }
        }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        public void createnewuser()
        {
            if (transtype == 1)
            {
                DialogResult result1 = MessageBox.Show("नयाँ प्रोपाइटर इन्ट्री गर्ने हो ?",
      "प्रोपाइरटरको विवरण थप गर्न",
      MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result1 == DialogResult.Yes)
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
                        }
                        //con.Open();

                        //  MySqlDataAdapter adapter = new MySqlDataAdapter();
                        // DataSet ds = new DataSet();

                        //SqlDataAdapter adapter= new SqlDataAdapter("SELECT   firmid from banijyaform where GetNumberToUnicode(firmregno)=@darta ",con);


                        // //string sql = "SELECT VdcCode, VdcNepnam FROM setup_vdc where setup_vdc.DistCode='" + comboBox3.ValueMember + "'";



                        // //command = new SqlCommand(sql, con);
                        //adapter.SelectCommand.Parameters.AddWithValue("@darta", darta_txt.Text);
                        //// adapter.SelectCommand = command;
                        // adapter.Fill(ds);
                        // adapter.Dispose();
                        // //command.Dispose();


                        // foreach (DataRow row in ds.Tables[0].Rows)
                        // {
                        //      firmi = row[0].ToString();

                        //    // MessageBox.Show(firmi.ToString());
                        // }
                        // MessageBox.Show(firmi.ToString());

                        ////////////////////////////


                        // string curdate = DateTime.Now.ToString();


                        //SqlCommand qr = new SqlCommand("insert into banijyaform (firmregno,firmdist,firmvdc,firmward,tole,regdat,renewdate,firmscope,firmnepname,karobar,revenue,branch,comment,updatnepdate,usr,updatdate) values
                        // ('" + darta_txt.Text + "','" + district_combo.SelectedValue + "','" + vdc_combo.SelectedValue + "','" + textBox1.Text + "','" + tole_txt.Text + "','" + darta_date.Text + "','" + renew_date.Text + "','" + firm_obj.SelectedValue + "',,'" + firm_name.Text + "''" + firm_karobar.Text + "','" + firm_capital.Text + "','" + branch_txt.Text + "','" + comment.Text + "','" + entry_nepdate.Text + "','" + usr.Text + "','" + curdate + "' )", con);
                        //SqlCommand qr = new SqlCommand("INSERT INTO Owner('firmregno', 'ccio', 'cddist', 'citzdate', 'citznum', 'gender', 'ownerfname', 'ownerlname')VALUES('" + darta_txt.Text + "','" + citizen_off.SelectedValue + "','" + comboBox1.SelectedValue + "','" + jari_dat.Text + "','" + citizen_num.Text + "','" + gender.SelectedValue + "','" + owner_name.Text + "','" + owner_lname.Text + "')", con);
                        /////////    SqlCommand cmd = new SqlCommand("INSERT INTO owner('firmid','firmregno','ccio', 'cddist', 'citzdate', 'citznum',                          'gender', 'ownerfname', 'ownerlname', 'ownerdistcode', 'ownervdccode', 'ownervdcward', 'ownertole', 'ownergrel', 'ownerfgrel', 'ownergname', 'ownerfgname', comment, 'updnepdate', 'upduser')VALUES(@firmid,GetUnicodeToNumber(@dartano),@citizenoff,@jaridistrict,GetUnicodeToNumber(@jaridate),GetUnicodeToNumber(@citizennum),@gender,@ownerfname,@ownerlname,@ownerdistrict,@ownervdc,GetUnicodeToNumber(@ownerward),@ownertole,@parentone,@parenttwo,@parentonename,@parenttwoname,@comment,@nepdate,@username)");
                        try
                        {
                            transactionid();
                            MySqlCommand cmd = new MySqlCommand("INSERT INTO owner(ownerid,firmid,firmregno,ccio, cddist, citzdate, citznum,gender, ownerfname, ownerlname, ownerdistcode, ownervdccode, ownervdcward, ownertole, ownergrel, ownerfgrel, ownergname,ownerfgname, comment, updnepdate, upduser,contact,email)VALUES(@ownerid,@firmid,GetUnicodeToNumber(@dartano),@citizenoff,@jaridistrict,GetUnicodeToNumber(@jaridate),GetUnicodeToNumber(@citizennum), @gender,@ownerfname,@ownerlname,@ownerdistrict,@ownervdc,GetUnicodeToNumber(@ownerward),@ownertole,@parentone,@parenttwo,@parentonename,@parenttwoname,@comment,@nepdate,@username,GetUnicodeToNumber(@contact),@email)");

                            // SqlCommand cmd = new SqlCommand("INSERT INTO banijyaform('firmregno',[firmtype],[firmdist] ,[firmvdc] ,[firmward],tole,[regdat],[renewdate],[firmscope],[firmnepname],[karobar],[revenue],[branch],comment,[usr],[updatdate],[updatnepdate]) VALUES (@darta, @firmtype, @firmdistrict, @firmvdc, @ward, @tole, @dartadate, @renewdate, @firmobj, @firmname, @firmkarobar, @firmcapital, @firmbranch, @comment, @user, @curdate, @entrydate)");
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Add(new MySqlParameter("@ownerid", transid));
                            cmd.Parameters.Add(new MySqlParameter("@firmid", firmi));
                            cmd.Parameters.Add(new MySqlParameter("@dartano", darta_txt.Text));
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
                            cmd.Parameters.Add(new MySqlParameter("@nepdate", global.todaynepslash));
                            cmd.Parameters.Add(new MySqlParameter("@username", global.username));
                            cmd.Connection = sqlcon.con;

                            // SqlCommand qr = new SqlCommand("INSERT INTO banijyaform('firmregno',[firmdist],[firmvdc] ,[firmward],tole ,[regdat],[renewdate],[firmscope],[firmnepname],[karobar],[revenue],[branch],comment,[usr],[updatdate],[updatnepdate])VALUES('" + darta_txt.Text + "','" + district_combo.SelectedValue + "','" + vdc_combo.SelectedValue + "','" + textBox1.Text + "','" + tole_txt.Text + "','" + darta_date.Text + "','" + renew_date.Text + "','" + firm_obj.SelectedValue + "','" + firm_name.Text + "','" + firm_karobar.Text + "','" + firm_capital.Text + "','" + branch_txt.Text + "','" + comment.Text + "','" + usr.Text + "','" + curdate + "','" + entry_nepdate.Text + "')", con);    
                            int n = cmd.ExecuteNonQuery();

                            if (n > 0)
                            {
                                global.sync_dblog("owner", firmi, "INSERT", transid.ToString(), "ownerid");
                               
                                MessageBox.Show("प्रोपाइटरको विवरण सेभ भयो ।");
                                sqlcon.con.Close();
                                dataGridView1.DataSource = null;
                                DisplayData();
                                cleardata();
                                save_btn.Enabled = false;
                                //button5.Enabled = false;
                                panel2.Enabled = false;


                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        //INSERT INTO [CSIO].[dbo].[owner] ('firmid','firmregno','ccio','cddist','citzdate','citznum','gender','ownerfname','ownerlname','ownerdistcode','ownervdccode','ownervdcward','ownertole','ownergrel','ownerfgrel','ownergname','ownerfgname',comment,[upddate],'updnepdate','upduser') VALUES(<firmid, int,>,<firmregno, nvarchar(15),>,<ccio, int,>,<cddist, int,>,<citzdate, nchar(10),>,<citznum, nvarchar(20),>,<gender, int,>,<ownerfname, nvarchar(50),>,<ownerlname, nvarchar(50),>,<ownerdistcode, tinyint,>,<ownervdccode, int,>,<ownervdcward, int,>,<ownertole, nvarchar(25),>,<ownergrel, int,>,<ownerfgrel, int,>,<ownergname, nvarchar(50),>     ,<ownerfgname, nvarchar(50),> ,<comment, nvarchar(50),>,<upddate, datetime,>,<updnepdate, nchar(10),>,<upduser, nchar(10),>)}
                    }
                }
            }
            else if (transtype == 2)
            {
                DialogResult result1 = MessageBox.Show("विवरण संशोधन गर्ने हो ?",
      "प्रोपाइरटरको विवरण संशोधन गर्न",
      MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result1 == DialogResult.Yes)
                {
                    //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");
                    string curdate = DateTime.Today.ToString();
                    try
                    {
                        if (sqlcon.con.State == ConnectionState.Closed)
                        {
                            sqlcon.con.Open();
                        }

                        MySqlCommand qr = new MySqlCommand(" UPDATE owner SET ccio = @citizenoff,cddist = @citizendist,citzdate = GetUnicodeToNumber(@jaridate),citznum = GetUnicodeToNumber(@citizennum),gender = @gender,ownerfname = @ownerfname,ownerlname = @ownerlname,ownerdistcode = @ownerdist,ownervdccode = @ownervdc,ownervdcward = GetUnicodeToNumber(@ownerward),ownertole = @ownertole,ownergrel = @parentone,ownerfgrel = @parenttwo,ownergname = @parentonename,ownerfgname = @parenttwoname,comment = @comment, upddate = @curdate,updnepdate = @nepdate,upduser = @user, contact=@contact, email=@email, firmregno=@darta  WHERE ownerid=@ownid");


                        qr.CommandType = CommandType.Text;

                        qr.Parameters.AddWithValue("@ownid", id_label.Text);
                        qr.Parameters.AddWithValue("@darta", darta_txt.Text);
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
                        int n = qr.ExecuteNonQuery();

                        if (n > 0)
                        {
                            global.sync_dblog("owner", id_label.Text, "UPDATE", id_label.Text, "ownerid");
                            sqlcon.con.Close();
                            dataGridView1.DataSource = null;
                            DisplayData();
                            cleardata();
                            MessageBox.Show("प्रोपाइटरको विवरण सफलता पुर्वक हटाइयो ।", "विवरण हटाइएको", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            save_btn.Enabled = false;
                            //button5.Enabled = false;
                            panel2.Enabled = false;


                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }
            }
        }
        public void namsaridetail()
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
               


                string curdate = DateTime.Now.ToString();

                transactionid();
               

                MySqlCommand qr = new MySqlCommand("INSERT INTO owner(ownerid,firmid ,firmregno, ccio, cddist, citzdate, citznum, gender, ownerfname, ownerlname, ownerdistcode, ownervdccode, ownervdcward, ownertole, ownergrel, ownerfgrel, ownergname, ownerfgname, comment, updnepdate, upduser, contact)VALUES(@ownerid,@firmid,@darta,@citizenoff,@citizendist,@jaridate,@citizennum,@gender,@ownerfname,@ownerlname,@ownerdist,@ownervdc,@ownerward,@ownertole,@parentone,@parenttwo,@parentonename,@parenttwoname,@comment,@nepdate,@user,@contact)");




                qr.CommandType = CommandType.Text;
                qr.Parameters.AddWithValue("@ownerid", transid.ToString());
                qr.Parameters.AddWithValue("@firmid", firmi.ToString());
                qr.Parameters.AddWithValue("@darta", darta_txt.Text);
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
                    global.sync_dblog("owner", transid.ToString(), "INSERT", transid.ToString(), "ownerid");
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
                            DisplayNewData();
                            if (sqlcon.con.State == ConnectionState.Closed)
                            {
                                sqlcon.con.Open();
                            }
                            // con.Open();
                            //int i;
                            //i = dataGridView1.CurrentRow.Index;
                            //int industryid = int.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
                            //string regno = dataGridView1.Rows[i].Cells[2].Value.ToString();
                            //string fname = dataGridView1.Rows[i].Cells[3].Value.ToString();
                            //string lname = dataGridView1.Rows[i].Cells[4].Value.ToString();
                            //string dist = dataGridView1.Rows[i].Cells[5].Value.ToString();
                            //string vdc = dataGridView1.Rows[i].Cells[6].Value.ToString();
                            //string ward = dataGridView1.Rows[i].Cells[7].Value.ToString();

                            MySqlCommand qrr = new MySqlCommand("INSERT INTO update_banijya (firmid,firmreg ,dartanum,updattitle,updatnew,oldrec,updatenepdate,updateuser,decisiondate,tax,comment) VALUES (@industryid, GetUnicodeToNumber(@industryregno), GetUnicodeToNumber(@dartanum), @updatetitle, @updatenew, @oldvalue, GetUnicodeToNumber(@updatenepdate), @updateuser, GetUnicodeToNumber(@ddate), GetUnicodeToNumber(@tax), @comment)");

                            qrr.CommandType = CommandType.Text;
                            qrr.Parameters.AddWithValue("@industryid", firmi.ToString());
                            qrr.Parameters.AddWithValue("@industryregno", darta_txt.Text);
                            qrr.Parameters.AddWithValue("@dartanum", darta_no.Text);
                            qrr.Parameters.AddWithValue("@updatetitle", updateid.Text);
                            // qrr.Parameters.AddWithValue("@updatenew", owner_name.Text);
                            // qrr.Parameters.AddWithValue("@oldvalue", fname.ToString());

                            qrr.Parameters.AddWithValue("@updatenew", newmember.ToString());
                            //  qrr.Parameters.AddWithValue("@updatenew", (owner_name.Text + " " + owner_lname.Text + ", " + owner_vdc.Text + "-" + owner_ward.Text + ", " + owner_dist.Text));
                            qrr.Parameters.AddWithValue("@oldvalue", oldmember.ToString());

                            //qr.Parameters.AddWithValue("@transid", updateid.Text);
                            //qrr.Parameters.AddWithValue("@updatedate", curdate.ToString());
                            qrr.Parameters.AddWithValue("@updatenepdate", global.nepalidate);
                            qrr.Parameters.AddWithValue("@updateuser", global.username);
                            qrr.Parameters.AddWithValue("@ddate", d_date.Text);
                            qrr.Parameters.AddWithValue("@tax", tax_txt.Text);
                            qrr.Parameters.AddWithValue("@comment", comment.Text);


                            qrr.Connection = sqlcon.con;


                            int k = qrr.ExecuteNonQuery();

                            if (k > 0)
                            {
                             //   global.sync_dblog("update_banijya", id.ToString(), "INSERT", id.ToString(), "firmid");

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
        public void sajhedarigaht()
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
                //MySqlCommand command;
                //MySqlDataAdapter adapter = new MySqlDataAdapter();
                //DataSet ds = new DataSet();
                //string sql = "SELECT   firmregno from banijyaform where firmid='" + label5.Text + "' ";

                ////string sql = "SELECT VdcCode, VdcNepnam FROM setup_vdc where setup_vdc.DistCode='" + comboBox3.ValueMember + "'";



                //command = new MySqlCommand(sql, sqlcon.con);
                //adapter.SelectCommand = command;
                //adapter.Fill(ds);
                //adapter.Dispose();
                //command.Dispose();


                //foreach (DataRow row in ds.Tables[0].Rows)
                //{
                //    firmi = row[0].ToString();

                //    // MessageBox.Show(firmi.ToString());
                //}
                // MessageBox.Show(firmi.ToString());

                ////////////////////////////


                string curdate = DateTime.Now.ToString();


                 int j;

                        j = dataGridView1.CurrentRow.Index;

                        id_label.Text = dataGridView1.Rows[j].Cells[0].Value.ToString();

                        MySqlCommand qrh = new MySqlCommand("INSERT INTO owner_hist_banijya(ownerid, firmid ,firmregno, ccio, cddist, citzdate, citznum, gender, ownerfname, ownerlname, ownerdistcode, ownervdccode, ownervdcward, ownertole, ownergrel, ownerfgrel, ownergname, ownerfgname, comment, upddate, updnepdate, upduser,transid) SELECT ownerid,firmid,firmregno,ccio,cddist,citzdate,citznum,gender,ownerfname,ownerlname,ownerdistcode,ownervdccode,ownervdcward,ownertole,ownergrel,ownerfgrel,ownergname,ownerfgname,comment,upddate,updnepdate,upduser,@transid FROM owner WHERE ownerid=@ownerid");




                        qrh.CommandType = CommandType.Text;
                        qrh.Parameters.AddWithValue("@ownerid", id_label.Text);
                        qrh.Parameters.AddWithValue("@transid", updateid.Text);
                        qrh.Connection = sqlcon.con;
                        int nh = qrh.ExecuteNonQuery();

                        if (nh > 0)
                        {
                    global.sync_dblog("owner_hist_banijya", id_label.Text, "INSERT", id_label.Text, "ownerid");
                    MySqlCommand qre = new MySqlCommand("DELETE FROM owner WHERE ownerid=@ownerid");




                            qre.CommandType = CommandType.Text;
                            qre.Parameters.AddWithValue("@ownerid", id_label.Text);
                            qre.Connection = sqlcon.con;
                            int nht = qre.ExecuteNonQuery();

                            if (nht > 0)
                            {
                        global.sync_dblog("owner", id_label.Text, "DELETE", id_label.Text, "ownerid");
                        DisplayNewData();
                                if (sqlcon.con.State == ConnectionState.Closed)
                                {
                                    sqlcon.con.Open();
                                }
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

                                qrr.Parameters.AddWithValue("@updatenew", newmember.ToString());
                                qrr.Parameters.AddWithValue("@oldvalue", oldmember.ToString());

                                //qrr.Parameters.AddWithValue("@oldvalue", (fname.ToString() + " " + lname.ToString() + ", " + vdc.ToString() + "-" + ward.ToString() + ", " + dist.ToString()));

                                //qr.Parameters.AddWithValue("@transid", updateid.Text);
                                //qrr.Parameters.AddWithValue("@updatedate", curdate.ToString());
                                qrr.Parameters.AddWithValue("@updatenepdate", global.nepalidate);
                                qrr.Parameters.AddWithValue("@updateuser", user_txt.Text);
                                qrr.Parameters.AddWithValue("@ddate", d_date.Text);
                                qrr.Parameters.AddWithValue("@tax", tax_txt.Text);
                                qrr.Parameters.AddWithValue("@comment", comment.Text);


                                qrr.Connection = sqlcon.con;


                                int k = qrr.ExecuteNonQuery();

                                if (k > 0)
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




                    // MessageBox.Show("प्रोपाइटरको विवरण सफलता पुर्वक सेभ भयो ।");
                    sqlcon.con.Close();
                    dataGridView1.DataSource = null;
                    DisplayData();



                }
     
            }

        }
        public void sajhedarithap()
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


                transactionid();
                MySqlCommand qr = new MySqlCommand("INSERT INTO owner(ownerid,firmid ,firmregno, ccio, cddist, citzdate, citznum, gender, ownerfname, ownerlname, ownerdistcode, ownervdccode, ownervdcward, ownertole, ownergrel, ownerfgrel, ownergname, ownerfgname, comment, updnepdate, upduser, contact)VALUES(@ownerid,@firmid,@darta,@citizenoff,@citizendist,@jaridate,@citizennum,@gender,@ownerfname,@ownerlname,@ownerdist,@ownervdc,@ownerward,@ownertole,@parentone,@parenttwo,@parentonename,@parenttwoname,@comment,@nepdate,@user,@contact)");




                qr.CommandType = CommandType.Text;
                qr.Parameters.AddWithValue("@ownerid", transid.ToString());
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
                    global.sync_dblog("owner", transid.ToString(), "INSERT", transid.ToString(), "ownerid");
                    DisplayNewData();
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

                    qrr.Parameters.AddWithValue("@updatenew", newmember.ToString());
                    qrr.Parameters.AddWithValue("@oldvalue", oldmember.ToString());

                    //qrr.Parameters.AddWithValue("@oldvalue", (fname.ToString() + " " + lname.ToString() + ", " + vdc.ToString() + "-" + ward.ToString() + ", " + dist.ToString()));

                    //qr.Parameters.AddWithValue("@transid", updateid.Text);
                    //qrr.Parameters.AddWithValue("@updatedate", curdate.ToString());
                    qrr.Parameters.AddWithValue("@updatenepdate", global.nepalidate);
                    qrr.Parameters.AddWithValue("@updateuser", user_txt.Text);
                    qrr.Parameters.AddWithValue("@ddate", d_date.Text);
                    qrr.Parameters.AddWithValue("@tax", tax_txt.Text);
                    qrr.Parameters.AddWithValue("@comment", comment.Text);


                    qrr.Connection = sqlcon.con;


                    int k = qrr.ExecuteNonQuery();

                    if (k > 0)
                    {
                        


                                sqlcon.con.Close();
                                dataGridView1.DataSource = null;
                               // DisplayData();
                                cleardata();
                                MessageBox.Show("वाणिज्य फर्मको साझेदार थप संशोधन सफल भयो ।", "साझेदार थप संशोधन", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                //button9.Enabled = false;
                                //button12.Enabled = false;
                                //panel2.Enabled = false;



                    }




                    // MessageBox.Show("प्रोपाइटरको विवरण सफलता पुर्वक सेभ भयो ।");
                    sqlcon.con.Close();
                    dataGridView1.DataSource = null;
                    DisplayData();



                }
                //INSERT INTO [CSIO].[dbo].[owner] ('firmid','firmregno','ccio','cddist','citzdate','citznum','gender','ownerfname','ownerlname','ownerdistcode','ownervdccode','ownervdcward','ownertole','ownergrel','ownerfgrel','ownergname','ownerfgname',comment,[upddate],'updnepdate','upduser') VALUES(<firmid, int,>,<firmregno, nvarchar(15),>,<ccio, int,>,<cddist, int,>,<citzdate, nchar(10),>,<citznum, nvarchar(20),>,<gender, int,>,<ownerfname, nvarchar(50),>,<ownerlname, nvarchar(50),>,<ownerdistcode, tinyint,>,<ownervdccode, int,>,<ownervdcward, int,>,<ownertole, nvarchar(25),>,<ownergrel, int,>,<ownerfgrel, int,>,<ownergname, nvarchar(50),>     ,<ownerfgname, nvarchar(50),> ,<comment, nvarchar(50),>,<upddate, datetime,>,<updnepdate, nchar(10),>,<upduser, nchar(10),>)}
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if(opttype==0)
            {
                createnewuser();
            }
            if (opttype == 1)
            {
                namsaridetail();
            }
            if (opttype == 2)
            {
                sajhedarithap();
            }
            
        }

     

        private void tableLayoutPanel6_Paint(object sender, PaintEventArgs e)
        {

        }
      

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.passwordname + "' ");
            //con.Open();
            owner_vdc.Text = null;
            owner_vdc.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            owner_vdc.AutoCompleteSource = AutoCompleteSource.CustomSource;

            AutoCompleteStringCollection vdcdata = new AutoCompleteStringCollection();
            Listvdc(vdcdata);
            owner_vdc.AutoCompleteCustomSource = vdcdata;
        
            


        }

        private void jari_dat_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void darta_txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }
        public void citzdistedit(string dist)
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
            sqls = " SELECT     distcode, distunicodename FROM  setup_district WHERE     (distunicodename = @dist)";

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

                comboBox1.SelectedValue = row[0].ToString();
            }

        }
        

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            transtype = 2;
            delete_btn.Enabled = true;
            save_btn.Enabled = true;
            //button5.Enabled = true;
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
            id_label.Visible = false;
            owner_name.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            owner_lname.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            district(dataGridView1.Rows[i].Cells[3].Value.ToString());
            // jari_dat.Text=dataGridView1.Rows[i].Cells[4].Value.ToString();
            vdcedit(dataGridView1.Rows[i].Cells[4].Value.ToString());
            owner_ward.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
            owner_tole.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();
            //ownerdist(dataGridView1.Rows[i].Cells[6].Value.ToString());



            citzoffedit(dataGridView1.Rows[i].Cells[7].Value.ToString());

            citzdistedit(dataGridView1.Rows[i].Cells[8].Value.ToString());
            //industry_scale.SelectedText = dataGridView1.Rows[i].Cells[8].Value.ToString();
            jari_dat.Text = dataGridView1.Rows[i].Cells[9].Value.ToString();
            //industry_type.SelectedText = dataGridView1.Rows[i].Cells[9].Value.ToString();
            citizen_num.Text = dataGridView1.Rows[i].Cells[10].Value.ToString();
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
            //maleworker.Text = dataGridView1.Rows[i].Cells[19].Value.ToString();
            //tax_txt.Text = dataGridView1.Rows[i].Cells[20].Value.ToString();
            //comment.Text = dataGridView1.Rows[i].Cells[21].Value.ToString();
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
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            delete_btn.Enabled = true;
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel24_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel25_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel26_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel27_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel21_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           searchbanijya bd = new searchbanijya();
            bd.MdiParent = this.MdiParent;

            bd.getdataa(darta_txt.Text);


            bd.Show();
        }

        
    }
}
