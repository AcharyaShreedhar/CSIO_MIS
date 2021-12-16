using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using System.Runtime.InteropServices;

namespace CSIO
{
    public partial class industrynaasari : Form
    {

        //for dragging the form ----------------------------
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        //FOR DRAGGING THE FORM ON DRAG OF TOP PANEL AS WELL AS THE FORM ITSELF

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }

            base.WndProc(ref m);
        }

        private void PanelTitle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        //--- end of code for form dragging -------------------

        public industrynaasari()
        {
            InitializeComponent();
        }
        public static string firmi;
        public static string oldmember;
        public static string newmember;
        public static string updatevalue="0";
        public static string transid;
        public static string openMode = "new";
        //new for new registration of industry, detail for existing record of industry, //update for namsari

        public industryreg myparent { get; set; } //this is required to call a method of parent from this child form

        public void DisplayNewData()
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }

            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     owner_industry.ownerid, owner_industry.industryid, GetNumberToUnicode(owner_industry.industryregno) As 'दर्ता नं',owner_industry.ownerfname AS 'नाम', owner_industry.ownerlname AS 'थर', setup_district_1.distunicodename AS 'ठेगाना', setup_vdc.vdcunicodename AS 'गा.पा.। न.पा.', GetNumberToUnicode(owner_industry.ownervdcward) AS 'वार्ड', owner_industry.ownertole AS 'टोल',    setup_citizenissueoff.citizen_officeunicodename AS 'जारी गर्ने कार्यालय', setup_district.distunicodename AS 'जारी जिल्ला', GetNumberToUnicode(owner_industry.citzdate) AS 'जारी मिति',   GetNumberToUnicode(owner_industry.citznum) AS 'ना.प्र.नं.', setup_subcategory_1.subcategory_unicodename AS 'बाबु।पति', owner_industry.ownergname AS 'बावु।पति', setup_subcategory_2.subcategory_unicodename AS 'बाजे।ससुरा', owner_industry.ownerfgname AS 'बाजे।ससुरा', setup_subcategory.subcategory_unicodename AS 'लिंग', owner_industry.comment AS 'कैफियत', GetNumberToUnicode(owner_industry.contact) As 'सम्पर्क नं', owner_industry.email As 'ईमेल' FROM         owner_industry INNER JOIN setup_district ON owner_industry.cddist = setup_district.distcode INNER JOIN setup_citizenissueoff ON owner_industry.ccio = setup_citizenissueoff.citizen_officeid INNER JOIN setup_vdc ON owner_industry.ownervdccode = setup_vdc.VDC_SID INNER JOIN     setup_district AS setup_district_1 ON owner_industry.ownerdistcode = setup_district_1.distcode INNER JOIN        setup_subcategory ON owner_industry.gender = setup_subcategory.subcategory_id INNER JOIN      setup_subcategory AS setup_subcategory_1 ON owner_industry.ownergrel = setup_subcategory_1.subcategory_id INNER JOIN       setup_subcategory AS setup_subcategory_2 ON owner_industry.ownerfgrel = setup_subcategory_2.subcategory_id WHERE     (owner_industry.industryid = @darta)", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@darta", label5.Text);
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView1.DataSource = tb;
            newmember = "प्रोपाइटर विवरण:";
            foreach (DataRow row in tb.Rows)
            {
                newmember = newmember +"-"+ row["नाम"].ToString() + " " + row["थर"].ToString() + ", " + row["गा.पा.। न.पा."].ToString() + " " + row["वार्ड"].ToString() + ", " + row["जारी जिल्ला"].ToString() + " ";
            }
            // MessageBox.Show(oldmember.ToString());
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;

            sqlcon.con.Close();

        }

        public void DisplayData(string indID)
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }

            string stmt = "SELECT owner_industry.ownerid, owner_industry.industryid, GetNumberToUnicode(owner_industry.industryregno) As 'दर्ता नं',owner_industry.ownerfname AS 'नाम', owner_industry.ownerlname AS 'थर', setup_district_1.distunicodename AS 'ठेगाना', setup_vdc.vdcunicodename AS 'गा.पा.। न.पा.', GetNumberToUnicode(owner_industry.ownervdcward) AS 'वार्ड', owner_industry.ownertole AS 'टोल',    setup_citizenissueoff.citizen_officeunicodename AS 'जारी गर्ने कार्यालय', setup_district.distunicodename AS 'जारी जिल्ला', GetNumberToUnicode(owner_industry.citzdate) AS 'जारी मिति',   GetNumberToUnicode(owner_industry.citznum) AS 'ना.प्र.नं.', setup_subcategory_1.subcategory_unicodename AS 'बाबु।पति', owner_industry.ownergname AS 'बावु।पति', setup_subcategory_2.subcategory_unicodename AS 'बाजे।ससुरा', owner_industry.ownerfgname AS 'बाजे।ससुरा', setup_subcategory.subcategory_unicodename AS 'लिंग', owner_industry.comment AS 'कैफियत', GetNumberToUnicode(owner_industry.contact) As 'सम्पर्क नं', owner_industry.email As 'ईमेल',owner_industry.inv_share as 'लगानी प्रतिशत' FROM owner_industry INNER JOIN setup_district ON owner_industry.cddist = setup_district.distcode INNER JOIN setup_citizenissueoff ON owner_industry.ccio = setup_citizenissueoff.citizen_officeid INNER JOIN setup_vdc ON owner_industry.ownervdccode = setup_vdc.VDC_SID INNER JOIN     setup_district AS setup_district_1 ON owner_industry.ownerdistcode = setup_district_1.distcode INNER JOIN        setup_subcategory ON owner_industry.gender = setup_subcategory.subcategory_id INNER JOIN      setup_subcategory AS setup_subcategory_1 ON owner_industry.ownergrel = setup_subcategory_1.subcategory_id INNER JOIN       setup_subcategory AS setup_subcategory_2 ON owner_industry.ownerfgrel = setup_subcategory_2.subcategory_id WHERE owner_industry.industryid =@darta";

            MySqlDataAdapter qry = new MySqlDataAdapter(stmt, sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@darta", indID);

            DataTable tb = new DataTable();

            qry.Fill(tb);
            dataGridView1.DataSource = tb;
            oldmember = "प्रोपाइटर विवरण:";
            foreach (DataRow row in tb.Rows)
            {
                oldmember = oldmember+"-"+row["नाम"].ToString() + " " + row["थर"].ToString() + ", " + row["गा.पा.। न.पा."].ToString() + " " + row["वार्ड"].ToString() + ", " + row["जारी जिल्ला"].ToString() + " ";
            }
           // MessageBox.Show(oldmember.ToString());
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;

            sqlcon.con.Close();
        }


        //public void getdataa(string darta, string dist, string vdc, string vdcname, string id, string firmtype,string indName="")
        //{
        //   // //darta_txt.Text = null;

        //   LabelRegNo.Text = darta.ToString();
        //   labelIndName.Text = indName;

        //   // //district_txt.Text = null;
        //   // //district_txt.Text = dist.ToString();
        //   // //vdc_txt.Text = null;
        //   // //vdc_txt.Text = vdc.ToString();
        //   // //ward_txt.Text = vdcname.ToString();
        //   // firmi = id.ToString();
        //   // if (firmtype.ToString() == "93")
        //   // {
        //   //     label7.Text = "प्रा.लि.को प्रथम सञ्चालकहरुको विवरण प्रविष्टि/संशोधन";
        //   //     label2.Text = "प्रा.लि.को प्रथम सञ्चालकको विवरण";
        //   // }


        //    //date_txt.Text = DateTime.Today.ToString("dd-MM-yyyy");
        //}

        public void transactionid()
        {
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }

                MySqlCommand cmd = new MySqlCommand("select industryregno, fiscalyear, officeid, officedist from setup_industryregid where industryregid=3", sqlcon.con);
                cmd.CommandType = CommandType.Text;
                // cmd.Connection = con;
                MySqlDataReader sdr = cmd.ExecuteReader();
                sdr.Read();
                transid = global.csioid.ToString().Trim() + sdr["industryregno"].ToString().Trim();

                sdr.Close(); //closing data reader
                sqlcon.con.Close();

                // if (sqlcon.con.State == ConnectionState.Closed)
                sqlcon.con.Open();

                MySqlCommand cmo = new MySqlCommand("update setup_industryregid SET industryregno= industryregno+1 WHERE industryregid=3", sqlcon.con);
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
           // connection = new SqlConnection(connectionString);
            try
            {
                ////connection.Open();
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
            //string connetionStrings = null;
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
            sqls = "SELECT VDC_SID,Vdcunicodename FROM  setup_vdc where DistCode='" + owner_dist.SelectedValue + "'";


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
                owner_vdc.DisplayMember = "Vdcunicodename";
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
            //string connetionStrings = null;
           // //SqlConnection connections;
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

                comboBox1.SelectedValue = row[0].ToString();
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
            sqls = " SELECT     distcode, distunicodename FROM setup_district WHERE     (distunicodename = '" + dist.ToString() + "')";

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

                owner_dist.SelectedValue = row[0].ToString();
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

        private void industrynaasari_Load(object sender, EventArgs e)
        {
            global.createBorderAround(this, Color.Teal, 2);

            d_date.Text = global.todaynepslash;
            user_txt.Text = global.username;
           
            button2.Enabled = false;
           // button5.Enabled = false;
            panel2.Enabled = false;
            DisplayData(firmi);
           
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection combData = new AutoCompleteStringCollection();
            FillDropDownList(combData);
            comboBox1.AutoCompleteCustomSource = combData;
            comboBox1.SelectedValue =  global.csiodist; ;

            owner_dist.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            owner_dist.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection dstrict = new AutoCompleteStringCollection();
            FillDropDownList1(dstrict);
            owner_dist.AutoCompleteCustomSource = dstrict;
            owner_dist.SelectedValue =  global.csiodist; ;


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

            //gender.AutoCompleteMode = AutoCompleteMode.Append;
            //gender.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection gndr = new AutoCompleteStringCollection();
            genderlist(gndr);
            gender.AutoCompleteCustomSource = gndr;
            gender.SelectedIndex = 0;


            //parentone.AutoCompleteMode = AutoCompleteMode.Append;
           // parentone.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection pone = new AutoCompleteStringCollection();
            parent1(pone);
            parentone.AutoCompleteCustomSource = pone;
            parentone.SelectedIndex = 0;

            //parenttwo.AutoCompleteMode = AutoCompleteMode.Append;
            //parenttwo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection ptwo = new AutoCompleteStringCollection();
            parent2(ptwo);
            parenttwo.AutoCompleteCustomSource = ptwo;
            parenttwo.SelectedIndex = 0;

            //if (this.Parent != null)
            //{
            //    this.Left = this.Parent.Width / 2 - this.Width / 2;
            //    this.Top = 0;
            //}

            if (updateid.Text == "" || updateid.Text == "0")
            {
                labelRajaswa.Visible = false;
                textRajaswa.Visible = false;
                labelKaifiyat.Visible = false;
                textKaifiyat.Visible = false;
            }
            else
            {
                labelRajaswa.Visible = true;
                textRajaswa.Visible = true;
                labelKaifiyat.Visible = true;
                textKaifiyat.Visible = true;
            }
        }
        public void getdataa(string darta, string regdat, string id, string firmtype,string industrynames)
        {
            LabelRegNo.Text = null;
            LabelRegNo.Text = darta.ToString();
            labelD_date.Text = null;
            labelD_date.Text = "दर्ता मिति:";
            labelD_date.Visible = true;
            d_date.Text = regdat.ToString();
            sanakhat_btn.Text = "फाईल स्क्यान";
            d_date.Visible = true;
            labelIndName.Text = industrynames.ToString();
            //vdc_txt.Text = vdc.ToString();
            //ward_txt.Text = vdcname.ToString();
            firmi = id.ToString();
            label5.Text = firmi.ToString();
            updateid.Text = "0";
            if (firmtype.ToString() == "93")
            {
                label7.Text = "प्रा.लि.को प्रथम सञ्चालकहरुको विवरण प्रविष्टि/संशोधन";
                label2.Text = "प्रा.लि.को प्रथम सञ्चालकको विवरण";
            }else if (firmtype.ToString() == "91")
            {
                label7.Text = "सञ्चालकको विवरण प्रविष्टि/संशोधन";
                label2.Text = "सञ्चालकको विवरण";
            }
            else if (firmtype.ToString() == "92")
            {
                label7.Text = "साझेदारहरुको विवरण प्रविष्टि/संशोधन";
                label2.Text = "साझेदारको विवरण";
            }

            DisplayData(firmi);

            //date_txt.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }
        public void getid(string indusid, string trans, string tran,string ddate,string ind_type,string indus_name,string regno)
        {
            
            label5.Text = null;
            label5.Text = indusid.ToString();
            firmi= indusid.ToString();
            darta_no.Text = null;
            darta_no.Text = trans.ToString();
            updateid.Text = null;
            updateid.Text = tran.ToString();
            d_date.Text = ddate.ToString();
            LabelRegNo.Text = regno.ToString();
            labelIndName.Text = indus_name.ToString();
            updatevalue = "3";
            if (updateid.Text == "132")
            {
                button1.Enabled=true;
                button1.Text = "नयाँ साझेदारी थप";
                label7.Text = "उद्योगको साझेदारहरुको बिवरण थप/घट गर्न";
            }

           // MessageBox.Show(label5.Text);
           // DisplayData(label5.Text);


            //date_txt.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            //button5.Enabled = false;

         //   DisplayData();
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
                if (LabelRegNo.Text.Length == 0 || LabelRegNo.Text == "")
                {
                    MessageBox.Show("उद्योगको दर्ता नं जाँच गर्नु होला । दर्ता नं टाइप गरी पुन प्रयास गर्नुहोस ।");
                }
                else {
                    if (!jari_dat.MaskFull)
                    {

                        MessageBox.Show("मिति पुरा राख्नुहोस ।");

                    }

                    else
                    {
                        if (updatevalue.ToString() == "2")
                        {
                            saveOwnerEditIndustry();
                            return;
                        } else if (updateid.Text == "0")
                        {
                            saveOwnerNewIndustry();
                            return;
                        }
                        else
                        {
                            transactionid();
                            if (sqlcon.con.State == ConnectionState.Closed)
                            {
                                sqlcon.con.Open();
                            }
                            //string firmi;
                            ////////////////////////////////////////
                            MySqlCommand command;
                            MySqlDataAdapter adapter = new MySqlDataAdapter();
                            DataSet ds = new DataSet();
                            string sql = "SELECT   industryregno from industryreg where industryid='" + label5.Text + "' ";

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




                            MySqlCommand qr = new MySqlCommand("INSERT INTO owner_industry(ownerid,industryid ,industryregno, ccio, cddist, citzdate, citznum, gender, ownerfname, ownerlname, ownerdistcode, ownervdccode, ownervdcward, ownertole, ownergrel, ownerfgrel, ownergname, ownerfgname, comment, updnepdate, upduser,contact,email,inv_share) VALUES(@ownerid,@firmid,@darta,@citizenoff,@citizendist,@jaridate,@citizennum,@gender,@ownerfname,@ownerlname,@ownerdist,@ownervdc,GetUnicodeToNumber(@ownerward),@ownertole,@parentone,@parenttwo,@parentonename,@parenttwoname,@comment,@nepdate,@user,GetUnicodeToNumber(@contact),@email,@inv_share)");

                            qr.CommandType = CommandType.Text;
                            qr.Parameters.AddWithValue("@ownerid", transid);
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
                            qr.Parameters.AddWithValue("@comment", textKaifiyat.Text);
                            // qr.Parameters.AddWithValue("@curdate", curdate);
                            qr.Parameters.AddWithValue("@nepdate", global.nepalidate);
                            qr.Parameters.AddWithValue("@user", global.username);
                            qr.Parameters.AddWithValue("@contact", contact_txt.Text);
                            qr.Parameters.AddWithValue("@email", email_txt.Text);
                            qr.Parameters.AddWithValue("@inv_share", textInv_share.Text);

                            // SqlCommand qr = new SqlCommand("INSERT INTO banijyaform('firmregno',[firmdist],[firmvdc] ,[firmward],tole ,[regdat],[renewdate],[firmscope],[firmnepname],[karobar],[revenue],[branch],comment,[usr],[updatdate],[updatnepdate])VALUES('" + darta_txt.Text + "','" + district_combo.SelectedValue + "','" + vdc_combo.SelectedValue + "','" + textBox1.Text + "','" + tole_txt.Text + "','" + darta_date.Text + "','" + renew_date.Text + "','" + firm_obj.SelectedValue + "','" + firm_name.Text + "','" + firm_karobar.Text + "','" + firm_capital.Text + "','" + branch_txt.Text + "','" + comment.Text + "','" + usr.Text + "','" + curdate + "','" + entry_nepdate.Text + "')", con);    
                            qr.Connection = sqlcon.con;
                            int n = qr.ExecuteNonQuery();


                            if (n > 0)
                            {

                                if (updateid.Text != "132")
                                {
                                    int j;

                                    j = dataGridView1.CurrentRow.Index;

                                    id_label.Text = dataGridView1.Rows[j].Cells[0].Value.ToString();
                                    id_label.Visible = false;

                                    MySqlCommand qrh = new MySqlCommand("INSERT INTO owner_hist_industry(ownerid, industryid ,industryregno, ccio, cddist, citzdate, citznum, gender, ownerfname, ownerlname, ownerdistcode, ownervdccode, ownervdcward, ownertole, ownergrel, ownerfgrel, ownergname, ownerfgname, comment, upddate, updnepdate, upduser, transid) SELECT ownerid,industryid,industryregno,ccio,cddist,citzdate,citznum,gender,ownerfname,ownerlname,ownerdistcode,ownervdccode,ownervdcward,ownertole,ownergrel,ownerfgrel,ownergname,ownerfgname,comment,upddate,updnepdate,upduser, @transid FROM owner_industry WHERE ownerid=@ownerid");




                                    qrh.CommandType = CommandType.Text;
                                    qrh.Parameters.AddWithValue("@ownerid", id_label.Text);
                                    qrh.Parameters.AddWithValue("@transid", updateid.Text);
                                    qrh.Connection = sqlcon.con;
                                    int nh = qrh.ExecuteNonQuery();

                                    if (nh > 0)
                                    {
                                        MySqlCommand qre = new MySqlCommand("DELETE FROM owner_industry WHERE ownerid=@ownerid");




                                        qre.CommandType = CommandType.Text;
                                        qre.Parameters.AddWithValue("@ownerid", id_label.Text);
                                        qre.Connection = sqlcon.con;
                                        int nht = qre.ExecuteNonQuery();

                                    }
                                }
                            }


                            DisplayNewData();
                            if (sqlcon.con.State == ConnectionState.Closed)
                            {
                                sqlcon.con.Open();
                            }
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

                            MySqlCommand qrr = new MySqlCommand("INSERT INTO update_industry (industryid,industryreg ,dartanum,updattitle,updatnew,oldrec,updatenepdate,updateuser,decisiondate,tax,comment) VALUES (@industryid, GetUnicodeToNumber(@industryregno), @dartanum, @updatetitle, @updatenewvalue, @oldvalue, @updatenepdate, @updateuser, @ddate, GetUnicodeToNumber(@tax), @comment)");

                            qrr.CommandType = CommandType.Text;
                            qrr.Parameters.AddWithValue("@industryid", industryid.ToString());
                            qrr.Parameters.AddWithValue("@industryregno", regno.ToString());
                            qrr.Parameters.AddWithValue("@dartanum", darta_no.Text);
                            qrr.Parameters.AddWithValue("@updatetitle", updateid.Text);
                            // qrr.Parameters.AddWithValue("@updatenew", owner_name.Text);
                            qrr.Parameters.AddWithValue("@updatenewvalue", newmember.ToString());
                            //concat(@updatenewvdc,GetNumberToUnicode(@updatenewward),@updatenewdist)
                            //qrr.Parameters.AddWithValue("@updatenewvdc", (owner_name.Text + " " + owner_lname.Text + ", " + owner_vdc.Text + "-" ));
                            //qrr.Parameters.AddWithValue("@updatenewward", (owner_ward.Text));
                            //qrr.Parameters.AddWithValue("@updatenewdist", ", " + owner_dist.Text);
                            if (updateid.Text == "132")
                            {
                                // qrr.Parameters.AddWithValue("@oldvalue", "साविकको साझेदारहरुमा नयाँ साझेदारी थप");
                                qrr.Parameters.AddWithValue("@oldvalue", oldmember.ToString());
                            }
                            else
                            {
                                //qrr.Parameters.AddWithValue("@oldvalue", (fname.ToString() + " " + lname.ToString() + ", " + vdc.ToString() + "-" + ward.ToString() + ", " + dist.ToString()));
                                qrr.Parameters.AddWithValue("@oldvalue", oldmember.ToString());
                            }
                            //qr.Parameters.AddWithValue("@transid", updateid.Text);
                            //qrr.Parameters.AddWithValue("@updatedate", curdate.ToString());
                            qrr.Parameters.AddWithValue("@updatenepdate", global.nepalidate);
                            qrr.Parameters.AddWithValue("@updateuser", user_txt.Text);
                            qrr.Parameters.AddWithValue("@ddate", d_date.Text);
                            qrr.Parameters.AddWithValue("@tax", textRajaswa.Text);
                            qrr.Parameters.AddWithValue("@comment", textKaifiyat.Text);


                            qrr.Connection = sqlcon.con;


                            int k = qrr.ExecuteNonQuery();

                            if (updateid.Text == "132")
                            {
                                sqlcon.con.Close();
                                dataGridView1.DataSource = null;
                                DisplayData(label5.Text);
                                //cleardata();
                                MessageBox.Show("उद्योगको साझेदारी थप संशोधन सफल भयो ।", "संशोधन", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                button1.Enabled = false;
                                button2.Enabled = false;
                                panel2.Enabled = false;
                            }
                            else
                            {
                                if (k > 0)
                                {
                                    //int j;

                                    //j = dataGridView1.CurrentRow.Index;

                                    //id_label.Text = dataGridView1.Rows[j].Cells[0].Value.ToString();
                                    //id_label.Visible = false;

                                    //MySqlCommand qrh = new MySqlCommand("INSERT INTO owner_hist_industry_update(ownerid, industryid ,industryregno, ccio, cddist, citzdate, citznum, gender, ownerfname, ownerlname, ownerdistcode, ownervdccode, ownervdcward, ownertole, ownergrel, ownerfgrel, ownergname, ownerfgname, comment, upddate, updnepdate, upduser, transid) SELECT ownerid,industryid,industryregno,ccio,cddist,citzdate,citznum,gender,ownerfname,ownerlname,ownerdistcode,ownervdccode,ownervdcward,ownertole,ownergrel,ownerfgrel,ownergname,ownerfgname,comment,upddate,updnepdate,upduser, @transid FROM owner_industry WHERE ownerid=@ownerid");




                                    //qrh.CommandType = CommandType.Text;
                                    //qrh.Parameters.AddWithValue("@ownerid", id_label.Text);
                                    //qrh.Parameters.AddWithValue("@transid", updateid.Text);
                                    //qrh.Connection = sqlcon.con;
                                    //int nh = qrh.ExecuteNonQuery();

                                    //if (nh > 0)
                                    //{
                                    //    MySqlCommand qre = new MySqlCommand("DELETE FROM owner_industry WHERE ownerid=@ownerid");




                                    //    qre.CommandType = CommandType.Text;
                                    //    qre.Parameters.AddWithValue("@ownerid", id_label.Text);
                                    //    qre.Connection = sqlcon.con;
                                    //    int nht = qre.ExecuteNonQuery();

                                    //    if (nht > 0)
                                    //    {


                                    sqlcon.con.Close();
                                    dataGridView1.DataSource = null;
                                    DisplayData(label5.Text);
                                    // cleardata();
                                    MessageBox.Show("उद्योग नामसारी संशोधन सफल भयो ।", "संशोधन", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                    button1.Enabled = false;
                                    button2.Enabled = false;
                                    panel2.Enabled = false;


                                }






                                // MessageBox.Show("प्रोपाइटरको विवरण सफलता पुर्वक सेभ भयो ।");
                                sqlcon.con.Close();
                                dataGridView1.DataSource = null;
                                DisplayData(label5.Text);



                            }
                            //INSERT INTO [CSIO].[dbo].[owner] ('firmid','firmregno','ccio','cddist','citzdate','citznum','gender','ownerfname','ownerlname','ownerdistcode','ownervdccode','ownervdcward','ownertole','ownergrel','ownerfgrel','ownergname','ownerfgname',comment,[upddate],'updnepdate','upduser') VALUES(<firmid, int,>,<firmregno, nvarchar(15),>,<ccio, int,>,<cddist, int,>,<citzdate, nchar(10),>,<citznum, nvarchar(20),>,<gender, int,>,<ownerfname, nvarchar(50),>,<ownerlname, nvarchar(50),>,<ownerdistcode, tinyint,>,<ownervdccode, int,>,<ownervdcward, int,>,<ownertole, nvarchar(25),>,<ownergrel, int,>,<ownerfgrel, int,>,<ownergname, nvarchar(50),>     ,<ownerfgname, nvarchar(50),> ,<comment, nvarchar(50),>,<upddate, datetime,>,<updnepdate, nchar(10),>,<upduser, nchar(10),>)}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void saveOwnerEditIndustry()
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

                    MySqlCommand qr = new MySqlCommand(" UPDATE owner_industry SET ccio = @citizenoff,cddist = @citizendist,citzdate = GetUnicodeToNumber(@jaridate),citznum = GetUnicodeToNumber(@citizennum),gender = @gender,ownerfname = @ownerfname,ownerlname = @ownerlname,ownerdistcode = @ownerdist,ownervdccode = @ownervdc,ownervdcward = GetUnicodeToNumber(@ownerward),ownertole = @ownertole,ownergrel = @parentone,ownerfgrel = @parenttwo,ownergname = @parentonename,ownerfgname = @parenttwoname,comment = @comment,upddate = @curdate,updnepdate = @nepdate,upduser = @user, contact=@contact, email=@email,inv_share=@inv_share  WHERE ownerid=@ownid");


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
                    qr.Parameters.AddWithValue("@comment", textKaifiyat.Text);
                    qr.Parameters.AddWithValue("@curdate", curdate);
                    qr.Parameters.AddWithValue("@nepdate", global.nepalidate);
                    qr.Parameters.AddWithValue("@user", global.username);
                    qr.Parameters.Add(new MySqlParameter("@contact", contact_txt.Text));
                    qr.Parameters.Add(new MySqlParameter("@email", email_txt.Text));
                    qr.Parameters.AddWithValue("@inv_share", textInv_share.Text);

                    qr.Connection = sqlcon.con;


                    //int n = qr.ExecuteNonQuery();



                    //int n = qr.ExecuteNonQuery();
                    int n = qr.ExecuteNonQuery();

                    if (n > 0)
                    {
                        
                        global.sync_dblog("owner_industry", label5.Text, "UPDATE", id_label.Text, "ownerid");
                        sqlcon.con.Close();
                        dataGridView1.DataSource = null;
                        DisplayData(label5.Text);
                        cleardata();
                        MessageBox.Show("संशोधन सफल भयो ।", "संशोधन", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        button2.Enabled = false;
                        //button5.Enabled = false;
                        panel2.Enabled = false;


                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void saveOwnerNewIndustry()
        {
            try
            {
                transactionid();
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


                    string curdate = DateTime.Now.ToString();
                    
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO owner_industry(ownerid,industryid ,industryregno, ccio, cddist, citzdate, citznum, gender, ownerfname, ownerlname, ownerdistcode, ownervdccode, ownervdcward, ownertole, ownergrel, ownerfgrel, ownergname, ownerfgname, comment, updnepdate, upduser,contact,email,csioid,inv_share)VALUES(@ownerid,@firmid,GetUnicodeToNumber(@darta),@citizenoff,@jaridistrict,GetUnicodeToNumber(@jaridate),GetUnicodeToNumber(@citizennum),@gender,@ownerfname,@ownerlname,@ownerdistrict,@ownervdc,GetUnicodeToNumber(@ownerward),@ownertole,@parentone,@parenttwo,@parentonename,@parenttwoname,@comment,GetUnicodeToNumber(@nepdate),@username,GetUnicodeToNumber(@contact),@email,@csioid,@inv_share)");

                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ownerid", transid);
                    cmd.Parameters.AddWithValue("@firmid", firmi);
                    cmd.Parameters.AddWithValue("@darta", LabelRegNo.Text);
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
                    cmd.Parameters.Add(new MySqlParameter("@comment", textKaifiyat.Text));
                    cmd.Parameters.Add(new MySqlParameter("@contact", contact_txt.Text));
                    cmd.Parameters.Add(new MySqlParameter("@email", email_txt.Text));
                    // cmd.Parameters.Add(new SqlParameter("@curdate", curdate));
                    cmd.Parameters.Add(new MySqlParameter("@nepdate", global.nepalidate));
                    cmd.Parameters.Add(new MySqlParameter("@username", global.username));
                    cmd.Parameters.AddWithValue("@csioid", global.csioid);
                    cmd.Parameters.AddWithValue("@inv_share", textInv_share.Text);
                    cmd.Connection = sqlcon.con;

                    int n = cmd.ExecuteNonQuery();

                    if (n > 0)
                    {
                        //String uid = global.getSingleDataFromTable("SELECT ownerid FROM owner_industry WHERE industryid='" +firmi.ToString() + "' AND  ownerfname='" + owner_name.Text + "' AND ownerlname='" + owner_lname.Text + "'");
                        global.sync_dblog("owner_industry", firmi.ToString(), "INSERT", transid.ToString(), "ownerid");
                       // global.sync_dblog("update_industry", id.ToString(), "INSERT", id.ToString(), "industryid");
                        MessageBox.Show("सञ्चालकको विवरण सफलतापुर्वक सेभ भयो ।","संचालक विवरण",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        sqlcon.con.Close();
                        dataGridView1.DataSource = null;
                        DisplayData(label5.Text);
                        cleardata();
                        button2.Enabled = false;
                        //button5.Enabled = false;
                        panel2.Enabled = false;
                        button1.Enabled = true;
                        //dataGridView1.DataSource = null;

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            button1.Enabled = true;
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
            textKaifiyat.Text = null;
            textRajaswa.Text = null;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            cleardata();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            //button5.Enabled = false;

            DisplayNewData();
            panel2.Enabled = true;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void citizen_num_TextChanged(object sender, EventArgs e)
        {

        }

        public void filescan()
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
                //ownrindst.MdiParent = this.MdiParent;
                //ownrindst.getdataa(textRegNum.Text.ToString(), transid.ToString());
                //this.Hide();
               // ownrindst.Show();
                ////////////////
                industryfileupload ownrindst = new industryfileupload();
                ownrindst.MdiParent = this.MdiParent;

                ownrindst.getdataa(LabelRegNo.Text, firmi.ToString());

                ownrindst.Show();
                this.Hide();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    
       
        private void button4_Click_1(object sender, EventArgs e)
        {
            
            
            try
            {
                if (updateid.Text == "0")
                {
                    filescan();
                }
                else
                {
                  int  j = dataGridView1.CurrentRow.Index;

                    id_label.Text = dataGridView1.Rows[j].Cells[0].Value.ToString();
                    sanakhatprintview ciname = new sanakhatprintview();
                    ciname.MdiParent = this.MdiParent;
                    ciname.getdata(label5.Text, id_label.Text, owner_name.Text + " " + owner_lname.Text, owner_dist.Text, owner_vdc.Text, owner_ward.Text, citizen_num.Text + "-" + comboBox1.Text);
                    //this.Hide();
                    ciname.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void parentone_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelParentone.Text = parentone.Text+ " को नाम: ";
        }

        private void parenttwo_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelParenttwo.Text = parenttwo.Text + " को नाम: ";
        }
        public void ownerdelete()
        {
            try
            {
                DialogResult result1 = MessageBox.Show("सञ्चालकको विवरण हटाउने हो ?",
         "सञ्चालकको विवरण हटाउन",
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
                    global.sync_dblog("owner_industry", firmi.ToString(), "DELETE", id_label.Text, "ownerid");

                    if (n > 0)
                    {

                        sqlcon.con.Close();
                        dataGridView1.DataSource = null;
                        DisplayData(label5.Text);
                        cleardata();
                        MessageBox.Show("सञ्चालकको विवरण सफलता पुर्वक हटाइयो ।", "विवरण हटाइएको", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        button2.Enabled = false;
                       // button5.Enabled = false;
                        panel2.Enabled = false;


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void updatedeleteowner()
        {
            try
            {
                DialogResult result1 = MessageBox.Show("सञ्चालकको विवरण हटाउने हो ?",
         "सञ्चालकको विवरण हटाउन",
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
                    global.sync_dblog("owner_industry", firmi.ToString(), "DELETE", id_label.Text, "ownerid");
                    if (n > 0)
                    { 
                        DisplayNewData();
                    if (sqlcon.con.State == ConnectionState.Closed)
                    {
                        sqlcon.con.Open();
                    }
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

                    MySqlCommand qrr = new MySqlCommand("INSERT INTO update_industry (industryid,industryreg ,dartanum,updattitle,updatnew,oldrec,updatenepdate,updateuser,decisiondate,tax,comment) VALUES (@industryid, GetUnicodeToNumber(@industryregno), @dartanum, @updatetitle, @updatenewvalue, @oldvalue, @updatenepdate, @updateuser, @ddate, GetUnicodeToNumber(@tax), @comment)");

                    qrr.CommandType = CommandType.Text;
                    qrr.Parameters.AddWithValue("@industryid", industryid.ToString());
                    qrr.Parameters.AddWithValue("@industryregno", regno.ToString());
                    qrr.Parameters.AddWithValue("@dartanum", darta_no.Text);
                    qrr.Parameters.AddWithValue("@updatetitle", updateid.Text);
                    // qrr.Parameters.AddWithValue("@updatenew", owner_name.Text);
                    qrr.Parameters.AddWithValue("@updatenewvalue", newmember.ToString());
                    //concat(@updatenewvdc,GetNumberToUnicode(@updatenewward),@updatenewdist)
                    //qrr.Parameters.AddWithValue("@updatenewvdc", (owner_name.Text + " " + owner_lname.Text + ", " + owner_vdc.Text + "-" ));
                    //qrr.Parameters.AddWithValue("@updatenewward", (owner_ward.Text));
                    //qrr.Parameters.AddWithValue("@updatenewdist", ", " + owner_dist.Text);
                  
                        qrr.Parameters.AddWithValue("@oldvalue", oldmember.ToString());
                   
                    //qr.Parameters.AddWithValue("@transid", updateid.Text);
                    //qrr.Parameters.AddWithValue("@updatedate", curdate.ToString());
                    qrr.Parameters.AddWithValue("@updatenepdate", global.nepalidate);
                    qrr.Parameters.AddWithValue("@updateuser", user_txt.Text);
                    qrr.Parameters.AddWithValue("@ddate", d_date.Text);
                    qrr.Parameters.AddWithValue("@tax", textRajaswa.Text);
                    qrr.Parameters.AddWithValue("@comment", textKaifiyat.Text);


                    qrr.Connection = sqlcon.con;


                    int k = qrr.ExecuteNonQuery();
                        global.sync_dblog("update_industry", industryid.ToString(), "INSERT", id_label.Text, "ownerid");
                        sqlcon.con.Close();
                        dataGridView1.DataSource = null;
                        DisplayData(firmi);
                        cleardata();
                        MessageBox.Show("सञ्चालकको विवरण सफलता पुर्वक हटाइयो ।", "विवरण हटाइएको", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        button2.Enabled = false;
                        // button5.Enabled = false;
                        panel2.Enabled = false;


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void button4_Click_2(object sender, EventArgs e)
        {
            if (updateid.Text == "0")
            {
                ownerdelete();
             }
            else
            {
                updatedeleteowner();
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
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


        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            button4.Enabled = true;
            button2.Enabled = true;
           // button5.Enabled = true;
            panel2.Enabled = true;
            updatevalue = "2";
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
            owner_name.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            owner_lname.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            district(dataGridView1.Rows[i].Cells[5].Value.ToString());
            // jari_dat.Text=dataGridView1.Rows[i].Cells[4].Value.ToString();
            vdcedit(dataGridView1.Rows[i].Cells[6].Value.ToString());
            owner_ward.Text = dataGridView1.Rows[i].Cells[7].Value.ToString();
            owner_tole.Text = dataGridView1.Rows[i].Cells[8].Value.ToString();
            //ownerdist(dataGridView1.Rows[i].Cells[6].Value.ToString());



            citzoffedit(dataGridView1.Rows[i].Cells[9].Value.ToString());

            citzdistedit(dataGridView1.Rows[i].Cells[10].Value.ToString());
            //industry_scale.SelectedText = dataGridView1.Rows[i].Cells[8].Value.ToString();
            jari_dat.Text = dataGridView1.Rows[i].Cells[11].Value.ToString();
            //industry_type.SelectedText = dataGridView1.Rows[i].Cells[9].Value.ToString();
            citizen_num.Text = dataGridView1.Rows[i].Cells[12].Value.ToString();
            //firm_obj.SelectedText = dataGridView1.Rows[i].Cells[10].Value.ToString();
            fatheredit(dataGridView1.Rows[i].Cells[13].Value.ToString());
            owner_parent1name.Text = dataGridView1.Rows[i].Cells[14].Value.ToString();
            forefatheredit(dataGridView1.Rows[i].Cells[15].Value.ToString());
            owner_parent2name.Text = dataGridView1.Rows[i].Cells[16].Value.ToString();
            genderedit(dataGridView1.Rows[i].Cells[17].Value.ToString());
            // firm_turnover.Text = dataGridView1.Rows[i].Cells[15].Value.ToString();
            textKaifiyat.Text = dataGridView1.Rows[i].Cells[18].Value.ToString();
            contact_txt.Text = dataGridView1.Rows[i].Cells[19].Value.ToString();
            email_txt.Text = dataGridView1.Rows[i].Cells[20].Value.ToString();
            textInv_share.Text= dataGridView1.Rows[i].Cells[21].Value.ToString();

        }

        private void industrynaasari_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (myparent!=null)
            myparent.displayOwnerLabels();
        }
    }
}
