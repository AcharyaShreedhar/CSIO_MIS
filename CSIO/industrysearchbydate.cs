using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.Office.Core;
using System.Drawing;

namespace CSIO
{
    public partial class industrysearchbydate : Form
    {
        public industrysearchbydate()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (sqlcon.con.State == ConnectionState.Closed)
            //    {
            //        sqlcon.con.Open();
            //    }
            //    MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     industryreg.industryid, GetNumberToUnicode(ROW_NUMBER() OVER (ORDER BY industryid)) AS 'सि नं',  GetNumberToUnicode(industryreg.industryregno) AS 'दर्ता नं', industryreg.industrynepname AS 'उद्योगको नाम'," +
            //        "concat(industryreg.tole, ', ', replace(replace(replace(replace(setup_vdc.vdcunicodename, 'पालिका', 'पा.'), 'नगर', 'न.'), 'गाउँ', 'गा.'), 'महा', 'म.'), '-', GetNumberToUnicode(industryreg.industryward), ', ', setup_district.distunicodename) as 'उद्योग रहेको स्थान', " +
            //        "industryreg.branch AS 'शाखा',   setup_subcategory.subcategory_unicodename AS 'स्तर', setup_subcategory_2.subcategory_unicodename AS 'कानूनी स्वरूप',   setup_subcategory_1.subcategory_unicodename AS 'वर्ग', GetNumberToUnicode(industryreg.regdat) AS 'दर्ता मिति',  GetNumberToUnicode(industryreg.renewdate) AS 'नविकरण मिति', industryreg.karobar AS 'उद्योगको उद्देश्य', GetNumberToUnicode(industryreg.yearlyturnover) AS 'बार्षिक उत्पादन',  GetNumberToUnicode(industryreg.electricpower) AS 'विधुत शक्ती', GetNumberToUnicode(industryreg.statcapital) AS 'स्थिर पुँजी', GetNumberToUnicode(industryreg.varcapital) AS 'चालु पुँजी', GetNumberToUnicode(industryreg.femaleworker) As 'महिला कामदार', GetNumberToUnicode(industryreg.maleworker) As 'पुरूष कामदार', GetNumberToUnicode(industryreg.tax) As 'राजश्व', industryreg.comment AS 'कैफियत', GetNumberToUnicode(industryreg.techworker) As 'प्राविधिक', GetNumberToUnicode(industryreg.nontechworker) As 'अप्राविधिक', industryreg.rawmaterial As 'कच्चा पदार्थ', GetNumberToUnicode(industryreg.fileno) As 'फाइल नं', GetNumberToUnicode(industryreg.thelino) As 'ठेली', GetNumberToUnicode(industryreg.fiscalyear) As 'आ.व' ,GetNumberToUnicode(industryreg.adhikrit_capital) As 'अधिकृत पुँजी', GetNumberToUnicode(industryreg.jari_capital) As 'जारी पुँजी' FROM                industryreg INNER JOIN      setup_district ON industryreg.industrydist = setup_district.distcode INNER JOIN       setup_vdc ON industryreg.industryvdc = setup_vdc.VDC_SID INNER JOIN setup_subcategory    ON setup_subcategory.subcategory_id = industryreg.industryscale INNER JOIN      setup_subcategory AS setup_subcategory_2 ON industryreg.industrytype = setup_subcategory_2.subcategory_id INNER JOIN       setup_subcategory AS setup_subcategory_1 ON industryreg.industrycat = setup_subcategory_1.subcategory_id where  ", sqlcon.con);

            //        //Getint(regdat)>=Getint(@datefrom) AND Getint(regdat)<=Getint(@dateto)", sqlcon.con);
            //    qry.SelectCommand.Parameters.AddWithValue("@datefrom", darta_dateFrom.Text);
            //    qry.SelectCommand.Parameters.AddWithValue("@dateto", darta_dateTo.Text);
            //    DataTable tb = new DataTable();
            //    qry.Fill(tb);
            //    dataGridView1.DataSource = tb;
            //    dataGridView1.Columns[0].Visible = false;
            //    sqlcon.con.Close();
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }
        
       

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            buttonCertificate.Enabled = true;
            buttonLetter.Enabled = true;
            int i;

            i = dataGridView1.CurrentRow.Index;
            string indId = dataGridView1.Rows[i].Cells[0].Value.ToString();
            
            dataGridView2.DataSource = DisplayDataowner(indId);
            dataGridView2.Columns[0].Visible = false;
        }

        private void industrysearchbydate_Load(object sender, EventArgs e)
        {

            //FILLING COMBOs of industry
            string sqls;

            //स्तर
            sqls = "SELECT subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (setup_subcategory.category_id = 8)";

            global.fillCombo(combo_indScale, sqls, "subcategory_unicodename", "subcategory_id", " -- ");

            //कानुनी रूप
            sqls = "SELECT subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (setup_subcategory.category_id = 9)";
            
            global.fillCombo(combo_indType, sqls, "subcategory_unicodename","subcategory_id",  " -- ");

            //वर्ग
            sqls = "SELECT subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (setup_subcategory.category_id = 10)";

            global.fillCombo(combo_indCategory, sqls,  "subcategory_unicodename", "subcategory_id"," -- ");


            //province
            global.fillCombo(comboProvince, "SELECT provinceid, provincenep From setup_province", "provincenep", "provinceid");

            comboProvince.SelectedValue = global.myProvinceId;
            comboProvince.Enabled = false;

            //district
            global.fillCombo(comboDistrict, "SELECT distcode, distunicodename FROM setup_district WHERE distzonecd=" + comboProvince.SelectedValue, "distunicodename", "distcode", "सबै जिल्ला");


            //FOR MANTRALAY and NIRDESHANALAY - District is enabled -- for Karyalay it is disabled with setup district id
            if (Convert.ToInt32(global.useroffice_category) >= 3)
            {
                comboDistrict.SelectedValue = global.csiodist;
                comboDistrict.Enabled = false;
            }
            else
            {
                comboDistrict.SelectedIndex = 0; //सबै जिल्ला
                comboDistrict.Enabled = true;
            }

            global.fillCombo(district_combo, "SELECT distcode, distunicodename FROM setup_district ORDER BY distunicodename", "distunicodename", "distcode", " -- ", true);

            comboCapitalType.SelectedIndex = 0; //कूल पूँजी
                                                // district_combo.AutoCompleteCustomSource = combData;

            //Window STATE
            this.WindowState = FormWindowState.Normal;

            //POSITION and SIZE
            this.Left = (this.Parent.Width / 2) - (this.Width / 2) - 5;
            this.Top = 0;

            //border
            global.createBorderAround(this,Color.Teal, 2);

            //dataGridView1.AutoGenerateColumns = false;
        }

        public void firmtype(AutoCompleteStringCollection dataCollection)
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
            sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (setup_subcategory.category_id = 9)";
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

                combo_indType.DataSource = ds.Tables[0];
                combo_indType.ValueMember = "subcategory_id";
                combo_indType.DisplayMember = "subcategory_unicodename";
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
        public void firmscale(AutoCompleteStringCollection dataCollection)
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
            sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (setup_subcategory.category_id = 8)";
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

                combo_indScale.DataSource = ds.Tables[0];
                combo_indScale.ValueMember = "subcategory_id";
                combo_indScale.DisplayMember = "subcategory_unicodename";
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

        public void firmobj(AutoCompleteStringCollection dataCollection)
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
            sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (setup_subcategory.category_id = 10)";
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

                combo_indCategory.DataSource = ds.Tables[0];
                combo_indCategory.ValueMember = "subcategory_id";
                combo_indCategory.DisplayMember = "subcategory_unicodename";
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

        public void districtList(AutoCompleteStringCollection dataCollection)
        {

            ////string connetionString = null;
            // //SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            // //connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;
            sql = "SELECT distcode, distunicodename FROM setup_district ORDER BY distunicodename";
            ////connection = new SqlConnection(connetionString);
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
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

        public void getindustrynature(string firmtype)
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
                MySqlCommand cmo = new MySqlCommand("SELECT     subcategory_id  FROM setup_subcategory WHERE ( subcategory_unicodename = @firmtype)", sqlcon.con);
                cmo.CommandType = CommandType.Text;
                cmo.Parameters.AddWithValue("@firmtype", firmtype.ToString());
                int tid = Convert.ToInt32(cmo.ExecuteScalar());

                ind_nature.Text = tid.ToString();
                sqlcon.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

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

                ind_type.Text = tid.ToString();
                sqlcon.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }
        
        private void button2_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("कृपया पहिले उद्योगको विवरण खोज्नुहोस् ।", (sender as Button).Text.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("कृपया तालिकाबाट उद्योग छानेर पुन: प्रयास गर्नुहोला", (sender as Button).Text.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int i;

            i = dataGridView1.CurrentRow.Index;
            //string comment_text = comment.Text;

            string dartanum = dataGridView1.Rows[i].Cells[0].Value.ToString();
            getindustrytype(dataGridView1.Rows[i].Cells[9].Value.ToString());
            getindustrynature(dataGridView1.Rows[i].Cells[10].Value.ToString());
            //MessageBox.Show(dartanum.ToString());
            printcertificate bd = new printcertificate();
            bd.MdiParent = this.MdiParent;

            bd.getdataa(dartanum, ind_type.Text, ind_nature.Text);


            bd.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("कृपया पहिले उद्योगको विवरण खोज्नुहोस् ।", (sender as Button).Text.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("कृपया तालिकाबाट उद्योग छानेर पुन: प्रयास गर्नुहोला", (sender as Button).Text.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int i;

            i = dataGridView1.CurrentRow.Index;

            string dartan = dataGridView1.Rows[i].Cells[0].Value.ToString();
            //MessageBox.Show(dartanum.ToString());
            letterview lt = new letterview();
            lt.MdiParent = this.MdiParent;

            lt.getdata(dartan,"","");
            lt.Show();
        }

        private void chkRegDate_CheckedChanged(object sender, EventArgs e)
        {
            grpRegDate.Enabled = chkRegDate.Checked;
        }

        private void chkPropDetail_CheckedChanged(object sender, EventArgs e)
        {
            grpPropDetail.Enabled = chkPropDetail.Checked;
        }

        private void chkIndDetail_CheckedChanged(object sender, EventArgs e)
        {
            grpIndDetail.Enabled = chkIndDetail.Checked;
        }

        private void chkCapital_CheckedChanged(object sender, EventArgs e)
        {
            grpCapital.Enabled = chkCapital.Checked;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grpRegNo_Enter(object sender, EventArgs e)
        {

        }

        public DataTable DisplayDataowner(string indId)
        {
            DataTable tb = new DataTable();

            if (sqlcon.con.State == ConnectionState.Closed)
                sqlcon.con.Open();

            string mySqst = "SELECT owner_industry.ownerid, concat(owner_industry.ownerfname, ' ', owner_industry.ownerlname) AS 'नाम थर', concat(replace(replace(replace(replace(setup_vdc.vdcunicodename, 'पालिका', 'पा.'), 'नगर', 'न.'), 'गाउँ', 'गा.'), 'महा', 'म.'), '-', GetNumberToUnicode(owner_industry.ownervdcward), ', ', setup_district_1.distunicodename) as 'ठेगाना', setup_subcategory.subcategory_unicodename AS 'लिङ्ग',  GetNumberToUnicode(owner_industry.contact) As 'सम्पर्क नं', email As 'इमेल',  GetNumberToUnicode(owner_industry.citznum) AS 'नागरिकता.प्र.नं.',  concat(setup_citizenissueoff.citizen_officeunicodename, ' - ', setup_district.distunicodename) AS 'जारी गर्ने कार्यालय-जिल्ला',  GetNumberToUnicode(owner_industry.citzdate) AS 'जारी मिति', " +
                "concat(owner_industry.ownergname, ' (',setup_subcategory_1.subcategory_unicodename,')') AS 'बाबु/पतिको नाम'," +
                "concat(owner_industry.ownerfgname, ' (', setup_subcategory_2.subcategory_unicodename,')') AS 'बाजे/ससुराको नाम' ," +
                "GetNumberToUnicode(inv_share) as 'लगानी प्रतिशत'" +
                "FROM owner_industry INNER JOIN setup_district ON owner_industry.cddist = setup_district.distcode " +
                "INNER JOIN setup_citizenissueoff ON owner_industry.ccio = setup_citizenissueoff.citizen_officeid " +
                "INNER JOIN setup_vdc ON owner_industry.ownervdccode = setup_vdc.VDC_SID " +
                "INNER JOIN setup_district AS setup_district_1 ON owner_industry.ownerdistcode = setup_district_1.distcode " +
                "INNER JOIN setup_subcategory ON owner_industry.gender = setup_subcategory.subcategory_id " +
                "INNER JOIN setup_subcategory AS setup_subcategory_1 ON owner_industry.ownergrel = setup_subcategory_1.subcategory_id " +
                "INNER JOIN setup_subcategory AS setup_subcategory_2 ON owner_industry.ownerfgrel = setup_subcategory_2.subcategory_id " +
                "WHERE(owner_industry.industryid = @darta)";

            MySqlDataAdapter qry = new MySqlDataAdapter(mySqst, sqlcon.con);

            qry.SelectCommand.Parameters.AddWithValue("@darta", global.convertUnicodeToNum(indId));
            qry.Fill(tb);

            sqlcon.con.Close();

            return tb;
        }
       
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }

                MySqlDataAdapter sqDA=new MySqlDataAdapter();
                MySqlCommand sqCmd = new MySqlCommand();

                //NEW QUERY WITH OWNER INFO WHERE DATAGRID HAS MERGED CELLS FOR SAME INDUSTRY
                string SqStmt = "SELECT ir.industryid, ir.industrynepname AS 'उद्योगको नाम', GetNumberToUnicode(ir.industryregno) AS 'दर्ता नं', GetNumberToUnicode(ir.regdat) AS 'दर्ता मिति', GetNumberToUnicode(ir.renewdate) AS 'नविकरण गर्नुपर्ने मिति'," +
                  "concat(ir.tole, ', ', replace(replace(replace(replace(setup_vdc.vdcunicodename, 'पालिका', 'पा.'), 'नगर', 'न.'), 'गाउँ', 'गा.'), 'महा', 'म.'), '-', GetNumberToUnicode(ir.industryward), ', ', setup_district.distunicodename) as 'उद्योग रहेको स्थान', " +
                    " ssc1.subcategory_unicodename AS 'स्तर', ssc2.subcategory_unicodename AS 'कानूनी रुप' , ssc3.subcategory_unicodename AS 'वर्ग', ir.karobar AS 'उद्देश्य', ir.industry_nature AS 'प्रकृति', owner_industry.ownerid, " +
                    " concat(owner_industry.ownerfname, ' ', owner_industry.ownerlname) AS 'सञ्चालकको नाम' , " +
                    " concat(replace(replace(replace(replace(vdc2.vdcunicodename, 'पालिका', 'पा.'), 'नगर', 'न.'), 'गाउँ', 'गा.'), 'महा', 'म.'), '-', GetNumberToUnicode(owner_industry.ownervdcward), ', ', dist2.distunicodename) as 'ठेगाना', " +
                    " setup_subcategory.subcategory_unicodename AS 'लिङ्ग',  GetNumberToUnicode(owner_industry.contact) As 'सम्पर्क नं',  GetNumberToUnicode(owner_industry.citznum) AS 'नागरिकता.प्र.नं.',  concat(setup_citizenissueoff.citizen_officeunicodename, ' - ', dist_citizen.distunicodename) AS 'जारी गर्ने कार्यालय र जिल्ला',  GetNumberToUnicode(owner_industry.citzdate) AS 'जारी मिति', " +
                "concat(owner_industry.ownergname, ' (',setup_subcategory_1.subcategory_unicodename,')') AS 'बाबु/पतिको नाम'," +
                "concat(owner_industry.ownerfgname, ' (', setup_subcategory_2.subcategory_unicodename,')') AS 'बाजे/ससुराको नाम'  " +
                    "FROM industryreg as ir " +
                    "INNER JOIN setup_district ON ir.industrydist = setup_district.distcode " +
                    "INNER JOIN setup_vdc ON ir.industryvdc = setup_vdc.VDC_SID " +
                    "INNER JOIN setup_subcategory AS ssc1 ON ir.industryscale = ssc1.subcategory_id " +
                    "INNER JOIN setup_subcategory AS ssc2 ON ir.industrytype = ssc2.subcategory_id " +
                    "INNER JOIN setup_subcategory AS ssc3 ON ir.industrycat = ssc3.subcategory_id " +
                    "INNER JOIN owner_industry ON ir.industryid=owner_industry.industryid " +
                    "INNER JOIN setup_district as dist_citizen ON owner_industry.cddist = dist_citizen.distcode " + 
                    "INNER JOIN setup_citizenissueoff ON owner_industry.ccio = setup_citizenissueoff.citizen_officeid " +
                "INNER JOIN setup_vdc as vdc2 ON owner_industry.ownervdccode = vdc2.VDC_SID " +
                "INNER JOIN setup_district AS dist2 ON owner_industry.ownerdistcode = dist2.distcode " +
                "INNER JOIN setup_subcategory ON owner_industry.gender = setup_subcategory.subcategory_id " +
                "INNER JOIN setup_subcategory AS setup_subcategory_1 ON owner_industry.ownergrel = setup_subcategory_1.subcategory_id " +
                "INNER JOIN setup_subcategory AS setup_subcategory_2 ON owner_industry.ownerfgrel = setup_subcategory_2.subcategory_id ";

                //CREATING CONDITIONS AND PARAMETERS

                sqCmd.CommandText = SqStmt;

                string sqCondd = "";


                //District ID
                string distID = comboDistrict.SelectedValue.ToString();
                if (distID == "0") distID = "";
                //condition in SQL is LIKE '%distid%' -- so that data of every district will be seen in case of all is selected

                sqCondd += (sqCondd == "") ? " WHERE " : " AND ";
                sqCondd += " officedist LIKE concat('%',@officedist,'%')";
                sqCmd.CommandText = SqStmt + sqCondd;
                sqCmd.Parameters.AddWithValue("@officedist", distID);

                if (chkRegDate.Checked)
                {
                    string indtbl=""; //industryid for IN condition
                    if (radioDarta.Checked)
                    {
                        if (darta_dateFrom.MaskFull || darta_dateTo.MaskFull)
                        {
                            sqCondd += (sqCondd == "") ? " WHERE " : " AND ";
                            sqCondd += " (Getint(regdat)>=Getint(@datefrom) AND Getint(regdat)<=Getint(@dateto))";
                            sqCmd.CommandText = SqStmt + sqCondd;
                            sqCmd.Parameters.AddWithValue("@datefrom", darta_dateFrom.Text);
                            sqCmd.Parameters.AddWithValue("@dateto", darta_dateTo.Text);
                        }
                        else
                        {
                            MessageBox.Show("कृपया पुरा मिति राखेर पुन: प्रयास गर्नुहोला !", "मिति", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            darta_dateFrom.Focus();
                            darta_dateFrom.SelectAll();
                            return;
                        }
                      
                    }
                    else if (radioNabikarn.Checked)
                    {
                        indtbl = "industryrenew";
                    }
                    else if (radioSamshodhan.Checked)
                    {
                        indtbl = "update_industry";
                    }
                    else if (radioLagatKatta.Checked)
                    {
                        indtbl = "industryclose";
                    }

                    //NO NEED TO CHECK FOR industryreg -- get conditions for decision date
                    if (indtbl != "")
                    {
                        string idss = getIDsforCondition(indtbl);
                        if (!string.IsNullOrEmpty(idss))
                        {
                            sqCondd += (sqCondd == "") ? " WHERE " : " AND ";
                            sqCondd += "ir.industryid IN (" + idss + ")";
                        }
                    }

                    sqCmd.CommandText = SqStmt + sqCondd;
                }


                if (chkIndDetail.Checked)
                {
                    if (!string.IsNullOrWhiteSpace(darta_txt.Text))
                    {
                        //if (sqCondd == "") sqCondd += " WHERE" else sqCondd;

                        sqCondd += (sqCondd == "") ? " WHERE " : " AND ";
                        sqCondd += " ((ir.industryregno=@darta) OR (GetNumberToUnicode(ir.industryregno)=GetNumberToUnicode(@darta)))";
                        sqCmd.CommandText = SqStmt + sqCondd;
                        sqCmd.Parameters.AddWithValue("@darta", darta_txt.Text);
                    }

                    if (!string.IsNullOrWhiteSpace(industry_name.Text))
                    {
                        sqCondd += (sqCondd == "") ? " WHERE " : " AND ";
                        sqCondd +=" ir.industrynepname like @ind_name";
                        sqCmd.CommandText = SqStmt + sqCondd; 
                        sqCmd.Parameters.AddWithValue("@ind_name", '%' + industry_name.Text + '%');
                    }

                    if(combo_indScale.SelectedIndex>0)
                    {
                        //scale -स्तर
                        sqCondd += (sqCondd == "") ? " WHERE " : " AND ";
                        sqCondd += " ssc1.subcategory_id=" + combo_indScale.SelectedValue;
                        sqCmd.CommandText = SqStmt + sqCondd;
                        //sqCmd.Parameters.AddWithValue("@ind_name", '%' + industry_name.Text + '%');
                    }    
                        

                    if (combo_indType.SelectedIndex > 0)
                    {
                        //type - कानुनी रुप
                        sqCondd += (sqCondd == "") ? " WHERE " : " AND ";
                        sqCondd += " ssc2.subcategory_id=" + combo_indType.SelectedValue;
                        sqCmd.CommandText = SqStmt + sqCondd;
                    }
                            

                    if (combo_indCategory.SelectedIndex > 0)
                    {
                        //cateogory - वर्ग
                        sqCondd += (sqCondd == "") ? " WHERE " : " AND ";
                        sqCondd += " ssc3.subcategory_id=" + combo_indCategory.SelectedValue;
                        sqCmd.CommandText = SqStmt + sqCondd;
                    }

                    if (!string.IsNullOrWhiteSpace(industry_objective.Text))
                    {
                        sqCondd += (sqCondd == "") ? " WHERE " : " AND ";
                        sqCondd += "(ir.karobar like @ind_obj OR ir.industry_nature like @ind_obj)";
                        sqCmd.CommandText = SqStmt + sqCondd;
                        sqCmd.Parameters.AddWithValue("@ind_obj", '%' + industry_objective.Text + '%');
                    }

                }

                if (chkPropDetail.Checked)
                {
                    //first name
                    if (!string.IsNullOrWhiteSpace(owner_fname.Text.ToString()))
                    {
                        sqCondd += (sqCondd == "") ? " WHERE " : " AND ";
                        sqCondd += " (owner_industry.ownerfname like @owner_fname) ";
                        sqCmd.CommandText = SqStmt + sqCondd;
                        sqCmd.Parameters.AddWithValue("@owner_fname", '%' + owner_fname.Text + '%');
                    }

                   //last name
                    if (!string.IsNullOrWhiteSpace(owner_lname.Text.ToString()))
                    {
                        sqCondd += (sqCondd == "") ? " WHERE " : " AND ";
                        sqCondd += " (owner_industry.ownerlname like @owner_lname) ";
                        sqCmd.CommandText = SqStmt + sqCondd;
                        sqCmd.Parameters.AddWithValue("@owner_lname", '%' + owner_lname.Text + '%');
                    }

                    //citizenship number
                    if (!string.IsNullOrWhiteSpace(owner_cnum.Text.ToString()))
                    {
                        sqCondd += (sqCondd == "") ? " WHERE " : " AND ";
                        sqCondd += " (owner_industry.citznum like @owner_citznum) ";
                        sqCmd.CommandText = SqStmt + sqCondd;
                        sqCmd.Parameters.AddWithValue("@owner_citznum", '%' + owner_cnum.Text + '%');
                    }

                    //citizenship issue district

                    if (district_combo.SelectedIndex > 0)
                    {
                        //cateogory - वर्ग
                        sqCondd += (sqCondd == "") ? " WHERE " : " AND ";
                        sqCondd += " dist_citizen.distunicodename='" + district_combo.SelectedText + "'";
                        sqCmd.CommandText = SqStmt + sqCondd;
                    }


                    //citizenship issue date
                    if (owner_cdate.MaskFull)
                    {
                        sqCondd += (sqCondd == "") ? " WHERE " : " AND ";
                        sqCondd += " (Getint(owner_industry.citzdate)=Getint(@citzdate)) ";
                        sqCmd.CommandText = SqStmt + sqCondd;
                        sqCmd.Parameters.AddWithValue("@citzdate", owner_cdate.Text);
                    }
                }


                if (chkCapital.Checked)
                {
                    //first name
                    string capMin = "", capMax = "", capFld = "";

                    if (!string.IsNullOrWhiteSpace(textCapitalMin.Text.ToString()) && global.isNumeric(textCapitalMin.Text))
                    {
                        capMin = global.convertUnicodeToNum(textCapitalMin.Text.ToString());
                    }

                    if (!string.IsNullOrWhiteSpace(textCapitalMax.Text.ToString()) && global.isNumeric(textCapitalMax.Text))
                    {
                        capMax = global.convertUnicodeToNum(textCapitalMax.Text.ToString());
                    }

                    //check only if both minimum and maximum are blank
                    if(capMin=="" && capMax=="")
                    {
                        MessageBox.Show("कृपया न्युनतम वा अधिकतम वा दुवै पूँजी प्रविष्ट गरी पुन: प्रयास गर्नुहोला !", "उद्योग विवरण खोजी", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textCapitalMin.Focus();
                        textCapitalMin.SelectAll();
                        return;
                    }
                    else if(Convert.ToDecimal(capMin) > Convert.ToDecimal(capMax)) //min is more than max
                    {
                        MessageBox.Show("न्युनतम पुँजी भन्दा अधिकतम पुँजी कम हुन सक्दैन । \n कृपया सही पूँजी प्रविष्ट गरी पुन: प्रयास गर्नुहोला !", "उद्योग विवरण खोजी", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textCapitalMin.Focus();
                        textCapitalMin.SelectAll();
                        return;
                    }


                    //finding the field for capital - all or specific
                    switch (comboCapitalType.SelectedIndex)
                    {
                        case 0: //kul
                            capFld = "(statcapital+varcapital+adhikrit_capital+jari_capital)";
                            break;

                        case 1: //sthir
                            capFld = "statcapital";
                            break;

                        case 2: //chalu
                            capFld = "varcapital";
                            break;

                        case 3: // adhikrit puji
                            capFld = "adhikrit_capital";
                            break;

                        case 4: //jari puji
                            capFld = "jari_capital";
                            break;

                    }

                    if (capMin != "")
                    {
                        sqCondd += (sqCondd == "") ? " WHERE " : " AND ";
                        sqCondd += capFld + " >= @mincapital ";
                    }

                    if (capMax != "")
                    {
                        sqCondd += (sqCondd == "") ? " WHERE " : " AND ";
                        sqCondd += capFld + "  <= @maxcapital ";
                    }

                    sqCmd.CommandText = SqStmt + sqCondd;
                    sqCmd.Parameters.AddWithValue("@mincapital", capMin);
                    sqCmd.Parameters.AddWithValue("@maxcapital", capMax);
                }

                if(chkRenewCrossed.Checked)
                {
                    sqCondd += (sqCondd == "") ? " WHERE " : " AND ";
                    sqCondd += " ir.renewdate < getnepalidate(CURRENT_DATE) ";
                    sqCmd.CommandText = SqStmt + sqCondd;
                    //sqCmd.Parameters.AddWithValue("@citzdate", owner_cdate.Text);
                }

                    //qry.SelectCommand.Parameters.AddWithValue("@firmname", textBox1.Text + '%');

                    //Getint(regdat)>=Getint(@datefrom) AND Getint(regdat)<=Getint(@dateto)", sqlcon.con);
                    // qry.SelectCommand.Parameters.AddWithValue("@datefrom", darta_date.Text);
                    // qry.SelectCommand.Parameters.AddWithValue("@dateto", dartadateto.Text);

                //SqStmt= sqCmd.CommandText.ToString();

                sqCmd.Connection = sqlcon.con;
                sqDA.SelectCommand = sqCmd;
                
                DataTable tb = new DataTable();
                sqDA.Fill(tb);
                //MessageBox.Show(tb.Rows.Count.ToString());
                dataGridView1.DataSource = tb;
                dataGridView1.Columns[0].Visible = false; //industryid
                dataGridView1.Columns[11].Visible = false; //ownerid
                //dataGridView1.AutoResizeColumns();
                //dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                sqlcon.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private string getIDsforCondition(string tblname)
        {
            //string tblname="";
            //switch (type)
            //{
            //    case "nabikaran":
            //        tblname = "industryrenew";
            //        break;

            //    case "samshodhan":
            //        tblname = "update_industry";
            //        break;

            //    case "lagatkatta":
            //        tblname = "industryclose";
            //        break;
            //}

            string sqCond = "";
            string fromDate = global.convertUnicodeToNum(darta_dateFrom.Text.ToString());
            string toDate = global.convertUnicodeToNum(darta_dateTo.Text.ToString());

            string[] ids = global.getSingleFieldArrayFromTable("SELECT industryid from " + tblname + " WHERE (Getint(decisiondate) >= Getint('" + fromDate + "') AND Getint(decisiondate) <= Getint('" + toDate + "'))");

                for (int ii = 0; ii < ids.Length; ii++)
                {
                    sqCond += ids[ii];
                    if (ii < ids.Length - 1)
                        sqCond += ", ";
                }
            return sqCond;
        }

        private void PrepareDataGridForExport()
        {
            // CODE FOR THE EXCEL EXPORT
            string SqStmt = "SELECT ir.industryid, GetNumberToUnicode(ROW_NUMBER() OVER (ORDER BY industryid)) AS 'क्र.स.',  ir.industrynepname AS 'उद्योगको नाम', GetNumberToUnicode(ir.industryregno) AS 'दर्ता नं', GetNumberToUnicode(ir.regdat) AS 'दर्ता मिति'," +
                " GetNumberToUnicode(ir.renewdate) AS 'नविकरण गर्नुपर्ने मिति'," +
            "concat(ir.tole, ', ', replace(replace(replace(replace(setup_vdc.vdcunicodename, 'पालिका', 'पा.'), 'नगर', 'न.'), 'गाउँ', 'गा.'), 'महा', 'म.'), '-', GetNumberToUnicode(ir.industryward), ', ', setup_district.distunicodename) as 'उद्योग रहेको स्थान', " +
            " ir.branch AS 'शाखा', ssc1.subcategory_unicodename AS 'स्तर', ssc2.subcategory_unicodename AS 'कानूनी रुप', ssc3.subcategory_unicodename AS 'वर्ग', ir.karobar AS 'उद्देश्य', GetNumberToUnicode(ir.yearlyturnover) AS 'बार्षिक उत्पादन',  GetNumberToUnicode(ir.electricpower) AS 'विधुत शक्ती', GetNumberToUnicode(ir.statcapital) AS 'स्थिर पुँजी', GetNumberToUnicode(ir.varcapital) AS 'चालु पुँजी', GetNumberToUnicode(ir.adhikrit_capital) As 'अधिकृत पुँजी', GetNumberToUnicode(ir.jari_capital) As 'जारी पुँजी',  GetNumberToUnicode(ir.femaleworker) As 'महिला कामदार', GetNumberToUnicode(ir.maleworker) As 'पुरूष कामदार', GetNumberToUnicode(ir.fileno) As 'फाइल', GetNumberToUnicode(ir.thelino) As 'ठेली'" +
            "FROM industryreg as ir " +
            "INNER JOIN setup_district ON ir.industrydist = setup_district.distcode " +
            "INNER JOIN setup_vdc ON ir.industryvdc = setup_vdc.VDC_SID " +
            "INNER JOIN setup_subcategory AS ssc1 ON ir.industryscale = ssc1.subcategory_id " +
            "INNER JOIN setup_subcategory AS ssc2 ON ir.industrytype = ssc2.subcategory_id " +
            "INNER JOIN setup_subcategory AS ssc3 ON ir.industrycat = ssc3.subcategory_id ";

            //getting CONDITION USING IN FROM DatagridView1's first columns
            string SqCond="";

            for(int ii=0; ii<dataGridView1.Rows.Count;ii++)
            {
                SqCond += dataGridView1.Rows[ii].Cells[0].Value.ToString();
                if (ii < dataGridView1.Rows.Count - 1)
                    SqCond += ", ";
            }

            SqStmt = SqStmt + " WHERE industryid IN (" + SqCond + ")";

            if (sqlcon.con.State == ConnectionState.Closed)
                sqlcon.con.Open();

            MySqlDataAdapter sqDA = new MySqlDataAdapter();
            MySqlCommand sqCmd = new MySqlCommand();

            sqCmd.CommandText = SqStmt;

            sqCmd.Connection = sqlcon.con;
            sqDA.SelectCommand = sqCmd;

            DataTable tb = new DataTable();
            sqDA.Fill(tb);
            //MessageBox.Show(tb.Rows.Count.ToString());
            dataGridView2.DataSource = tb;
            dataGridView2.Columns[0].Visible = false;
            //dataGridView1.AutoResizeColumns();
            //dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            sqlcon.con.Close();
        }

        private void buttonExcelExport_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count==0)
            {
                MessageBox.Show("कृपया पहिले उद्योगको विवरण खोज्नुहोस् ।", "उद्योग विवरण Excel मा पठाउन", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // creating new WorkBook within Excel application  
            Excel.Application xlApp;
            Excel.Workbook xlwb;
            Excel.Worksheet xlws;
            Excel.Range xlrg;
            xlApp = new Excel.Application();
            //string template = Application.StartupPath + "\\csio_industry_detail_report_template_new4.xlsx";
            //xlwb = xlApp.Workbooks.Add(template);
            xlwb = xlApp.Workbooks.Add();
            xlws = xlwb.Worksheets[1];
            object misValue = System.Reflection.Missing.Value;
            

            try
            {
                //OFFICE HEADINGS
                string sqsm = "SELECT govtname,ministryname,departmentname,officeunicodename,office_address,provincename, GetNumberToUnicode(phone),GetNumberToUnicode(fax),email FROM setup_office where isCur=1";

                string[] offhead = global.getSingleRowFromTable(sqsm);

                string myHead = offhead[0];
                myHead += "\n " + offhead[1];
                myHead += "\n " + offhead[2];
                myHead += "\n " + offhead[3] + ", " + offhead[4];
                myHead += "\n " + offhead[5];

                //ADDRESS
                //string myAddr = "फोन नं:" + offhead[6] + "\n फ्याक्स नं: " + offhead[7] + "\n इमेल: " + offhead[8];

                //xlws.Cells[1, 1] = myHead;

                //xlrg = xlws.Range[xlws.Cells[1, 1], xlws.Cells[1, 10]];
                //xlrg.Merge();
                //xlrg.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //xlws.Cells[1, 1] = offhead[0];

                //SARKAR HEADING
                int hps=1; //header position
                xlrg = xlws.Range[xlws.Cells[hps,1], xlws.Cells[hps, 10]];
                xlrg.Merge();
                xlrg.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xlrg.Cells.Font.Size = "12";
                xlrg.Cells.Font.Bold = true;
                xlws.Cells[hps, 1] = offhead[0];

                //MANTRALAY HEADING
                hps++; 
                xlrg = xlws.Range[xlws.Cells[hps, 1], xlws.Cells[hps, 10]];
                xlrg.Merge();
                xlrg.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xlrg.Font.Size = "13";
                xlrg.Font.Bold = true;
                xlws.Cells[hps, 1] = offhead[1];

                //Nirdeshanalay HEADING
                if (Convert.ToInt32(global.useroffice_category) > 1 )
                {
                    hps++;
                    xlrg = xlws.Range[xlws.Cells[hps, 1], xlws.Cells[hps, 10]];
                    xlrg.Merge();
                    xlrg.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlrg.Font.Size = "13";
                    xlrg.Font.Bold = true;
                    xlws.Cells[hps, 1] = offhead[2];
                }

                //Karyalay HEADING
                if (Convert.ToInt32(global.useroffice_category) > 2)
                {
                    hps++;
                    xlrg = xlws.Range[xlws.Cells[hps, 1], xlws.Cells[hps, 10]];
                    xlrg.Merge();
                    xlrg.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlrg.Font.Size = "14";
                    xlrg.Font.Bold = true;
                    xlws.Cells[hps, 1] = offhead[3] + ", " + offhead[4];
                }

                hps++;
                xlrg = xlws.Range[xlws.Cells[hps, 1], xlws.Cells[hps, 10]];
                xlrg.Merge();
                xlrg.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xlrg.Font.Size = "12";
                xlrg.Font.Bold = true;
                xlws.Cells[hps, 1] = offhead[5];

                //ADDRESS HEADINGS AT THE RIGHT SIDE
                //Address - Phone Number
                xlrg = xlws.Range[xlws.Cells[1, 11], xlws.Cells[1, 13]];
                xlrg.Merge();
                xlws.Cells[1, 11] = "फोन नं:" + offhead[6];

                //Address - FAX
                xlrg = xlws.Range[xlws.Cells[2, 11], xlws.Cells[2, 13]];
                xlrg.Merge();
                xlws.Cells[2, 11] = "फ्याक्स नं: " + offhead[7];

                //Address - Email
                xlrg = xlws.Range[xlws.Cells[3, 11], xlws.Cells[3, 13]];
                xlrg.Merge();
                xlws.Cells[3, 11] = "इमेल: " + offhead[8];

                //REPORT HEADER
                //myHead = "मिति ";
                //myHead += darta_dateFrom.Text ;
                //myHead +=" देखि ";
                //myHead += darta_dateTo.Text;
                //myHead += " सम्मको उद्योगसम्बन्धी प्रगति विवरण" ;

                /*
                xlrg = xlws.Range[xlws.Cells[1, 1], xlws.Cells[hps, 1]];
                xlrg.Font.Name = "Noto Sans";
                xlrg.Font.Size = "12";
                xlrg.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;


                xlrg = xlws.Range[xlws.Cells[1, 11], xlws.Cells[3, 11]];
                xlrg.Font.Name = "Noto Sans";
                xlrg.Font.Size = "10";
                xlrg.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

                */

                hps++;

                xlrg = xlws.Range[xlws.Cells[hps, 1], xlws.Cells[hps, 10]];
                xlrg.Merge();
                xlws.Cells[hps, 1] = "उद्योग तथा संचालकहरूको सूची";
                xlrg.Cells.Font.Size = "13";

               string logogov= Application.StartupPath + "\\logogov.png";
                xlws.Shapes.AddPicture(@logogov, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 0, 0, 85, 58);

                //con.Open();
                if (sqlcon.con.State == ConnectionState.Closed) 
                    sqlcon.con.Open();

                //FILLING ANOTHER DATAGRID -- with more columns of industry but none of owner
                PrepareDataGridForExport();

                DataGridView tb = dataGridView2;
                DataTable otb = new DataTable();
                //INDUSTRY REGISTRtTION -- FIRST PART OF THE REPORT
                // FILLING THE DATA
                //as per the template - the first data is in 7th row but col is always 1
                //so added 7 in j (j+7, k+1)

                int fps, nps; //first position of data (starting from header) and new position for each row
                fps = nps = hps + 2; //this is for the new position of row (in excel)
                int j=0, k=0, m=0, n=0;

                //COLUMN HEADINGS of INDUSTRY
                for (k = 1; k < tb.Columns.Count; k++)
                {
                    xlws.Cells[nps, k] = tb.Columns[k].HeaderText;
                }
                
                k--; //n gets increased by 1 after for loop
                
                //merged heading above the column headers
                xlrg = xlws.Range[xlws.Cells[nps - 1, 1], xlws.Cells[nps-1, k]];
                xlrg.Merge();
                xlws.Cells[nps - 1, 1] = "उद्योगको विवरण";

                //COLUMN HEADINGS OF OWNER
                otb = DisplayDataowner("");
                for (n = 1; n < otb.Columns.Count; n++)
                {
                    xlws.Cells[nps, k+n] = otb.Columns[n].Caption;
                }
                n--; //n gets increased by 1 after for loop

                //merged heading above the column headers
                xlrg = xlws.Range[xlws.Cells[nps - 1, k+1], xlws.Cells[nps - 1, k+n]];
                xlrg.Merge();
                xlws.Cells[nps - 1, k + 1] = "साझेदार / प्रोपराइटर विवरण";

                //making the header cells bold, center and backcolor
                xlrg = xlws.Range[xlws.Cells[nps - 1, 1], xlws.Cells[nps, k+n]];
                xlrg.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xlrg.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                xlrg.Font.Bold = true;
                xlrg.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);

                nps++; //new position for row is just below the header cells
                for (j = 0; j < tb.Rows.Count; j++)
                {
                    otb = DisplayDataowner(tb.Rows[j].Cells[0].Value.ToString());

                    for (k = 1; k < tb.Columns.Count; k++)
                    {
                        //xlws.Cells[j + 7, k + 1] = tb.Rows[j].ItemArray[k].ToString();
                        xlws.Cells[nps+j, k] = tb.Rows[j].Cells[k].Value.ToString();

                        if(otb.Rows.Count>1)
                        {
                            xlrg = xlws.Range[xlws.Cells[nps+j, k], xlws.Cells[nps+j+otb.Rows.Count-1, k]];
                            xlrg.Merge();
                        }
                    }

                    for (m = 0; m < otb.Rows.Count; m++)
                    {
                        for (n = 1; n < otb.Columns.Count; n++)
                        {
                            xlws.Cells[nps+m+j, n+tb.Columns.Count-1] = otb.Rows[m].ItemArray[n].ToString();
                            //MessageBox.Show(otb.Rows[m].ItemArray[n].ToString());
                        }
                    }
                    if (otb.Rows.Count > 1)
                    {
                        nps += otb.Rows.Count - 1;
                    }
                }

                //FORMATTING THE EXCEL FILE

                //border
                xlrg = xlws.Range[xlws.Cells[fps-1, 1], xlws.Cells[nps+m+j-2, tb.Columns.Count+otb.Columns.Count-2]];
                xlrg.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                xlrg.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                //autofit
                xlws.Cells.Font.Name = "Kalimati";
                xlws.Columns.AutoFit();
                xlws.Rows.AutoFit();

                xlrg = xlws.Range[xlws.Cells[1, 1], xlws.Cells[hps-1, 1]];
                xlrg.Cells.Font.Name = "Noto Sans";

                xlrg = xlws.Range[xlws.Cells[1, 11], xlws.Cells[3, 11]];
                xlrg.Cells.Font.Name = "Noto Sans";

                //SAVING FILE
                //string filePath;
                //filePath = "D:\\csido_report.xlsx";

                //if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                //{
                //    filePath = folderBrowserDialog1.SelectedPath;
                //}
                //xlwb.SaveAs(filePath);



                //SAVING FILE IN USER DEFINED SPACE

                string filePath = "";

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.InitialDirectory = @Environment.SpecialFolder.MyDocuments.ToString();
                saveFileDialog1.Title = "Save text Files";
                saveFileDialog1.CheckFileExists = false;
                //saveFileDialog1.CheckPathExists = false;    
                saveFileDialog1.DefaultExt = ".xlsx";
                saveFileDialog1.FileName = "Industry_Report";
                saveFileDialog1.Filter = "Excel Workbook (*.xlsx)|*.xlsx|Excel 97-2003 Workbook (*.xls)|*.xls|CSV (*.csv)|*.csv|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveFileDialog1.FileName;
                    xlwb.SaveAs(filePath);
                    saveFileDialog1.Dispose();

                    DialogResult dlrg = MessageBox.Show("प्रतिवेदन सफलतापुर्वक MS Excel मा सेभ गरियो । \n\n के तपाईँ प्रतिवेदन अहिले हेर्न चाहनुहुन्छ?", "प्रतिवेदन", MessageBoxButtons.YesNo, MessageBoxIcon.None);

                    if (dlrg == DialogResult.Yes)
                    {
                        FileInfo fi = new FileInfo(filePath);
                        if (fi.Exists)
                        {
                            System.Diagnostics.Process.Start(@filePath); //open excel file
                        }
                        else
                        {
                            MessageBox.Show("प्रतिवेदन फाइल खोल्न सकिएन ! कृपया सम्बन्धित फोल्डरमा गई आफै खोल्नुहोला ।", "प्रतिवेदन", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    xlwb.Close(misValue, filePath, misValue);
                    //xlwb = null;
                    xlApp.Quit();
                    //xlApp = null;

                    global.releaseObject(xlws);
                    global.releaseObject(xlwb);
                    global.releaseObject(xlApp);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

                sqlcon.con.Close();

                //// Cleanup
                GC.Collect();
                GC.WaitForPendingFinalizers();


                // xlApp.Workbooks.Open(filePath);
                //xlApp.Visible = true;
                //xl

                ////Marshal.FinalReleaseComObject(xlRng);
                //Marshal.FinalReleaseComObject(xlws);

                //if (xlwb!=null)
                //{
                //xlwb.Close(misValue,template,misValue);
                //Marshal.FinalReleaseComObject(xlwb);
                //}

                //xlApp.Workbooks.Close();
                //xlApp.Quit();
                //Marshal.FinalReleaseComObject(xlApp);

            }
        }

        bool IsTheSameCellValue(int column, int row)
        {
            DataGridViewRow dgr1 = dataGridView1.Rows[row];
            DataGridViewRow dgr2 = dataGridView1.Rows[row-1];

            //IGNORE IF INDUSTRYID IS NOT SAME
            if (dgr1.Cells[0].Value.ToString() != dgr2.Cells[0].Value.ToString())
                return false;

            //IGNORE FOR OWNER DETAILS
            if (column > 8)
                return false;

            //if(dgr.Cells[0].ToString=dgr.Cells)

            //if(dgr.cells)

            DataGridViewCell cell1 = dataGridView1[column, row];
            DataGridViewCell cell2 = dataGridView1[column, row - 1];
            if (cell1.Value == null || cell2.Value == null)
            {
                return false;
            }
            return cell1.Value.ToString() == cell2.Value.ToString();
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //DataGridViewRow dgr = dataGridView1.Rows[e.RowIndex];
            e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
            if (e.RowIndex < 1 || e.ColumnIndex < 0)
                return;
            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            }
            else
            {
                e.AdvancedBorderStyle.Top = dataGridView1.AdvancedCellBorderStyle.Top;
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == 0)
                return;
            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.Value = "";
                e.FormattingApplied = true;
            }
        }

        private void buttonIndustryDetail_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count==0)
            {
                MessageBox.Show("कृपया पहिले उद्योगको विवरण खोज्नुहोस् ।", (sender as Button).Text.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("कृपया तालिकाबाट उद्योग छानेर पुन: प्रयास गर्नुहोला", (sender as Button).Text.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            industryreg inds = new industryreg();
            string dartanum=dataGridView1.SelectedRows[0].Cells[2].Value.ToString(); //darta Number
            string indid= dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); //industryid
            inds.DisplayData(dartanum);
            inds.DisplayIndividualData(indid);
            inds.MdiParent = this.MdiParent;
            inds.Show();
        }

        private void buttonProprieter_Click(object sender, EventArgs e)
        {
            DisplayIndustryOwner(sender);
        }

        private void DisplayIndustryOwner(object sender)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("कृपया पहिले उद्योगको विवरण खोज्नुहोस् ।", (sender as Button).Text.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("कृपया तालिकाबाट उद्योग वा संचालक छानेर पुन: प्रयास गर्नुहोला", (sender as Button).Text.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DataGridViewRow selrow = dataGridView1.SelectedRows[0];

                string regid = selrow.Cells[0].Value.ToString();
                string industnam = selrow.Cells[1].Value.ToString();
                string regnum = selrow.Cells[2].Value.ToString();
                string regdat = selrow.Cells[3].Value.ToString();
                string indtype = selrow.Cells[6].Value.ToString();
                string indtypeid = global.getSingleDataFromTable("SELECT subcategory_id FROM setup_subcategory WHERE subcategory_unicodename='" + indtype + "'");
                string ownid= selrow.Cells[10].Value.ToString();

                industrynaasari ownrindst = new industrynaasari();
                //ownrindst.MdiParent = this.MdiParent;
                ownrindst.getdataa(regnum, regdat, regid, indtypeid, industnam);
                ownrindst.ShowDialog();
           

           
        }

        private void buttonSamshodhan_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("कृपया पहिले उद्योगको विवरण खोज्नुहोस् ।", (sender as Button).Text.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("कृपया तालिकाबाट उद्योग छानेर पुन: प्रयास गर्नुहोला", (sender as Button).Text.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void buttonFileUpload_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("कृपया पहिले उद्योगको विवरण खोज्नुहोस् ।", (sender as Button).Text.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("कृपया तालिकाबाट उद्योग छानेर पुन: प्रयास गर्नुहोला", (sender as Button).Text.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            string indid = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); //industryid
            string dartanum = dataGridView1.SelectedRows[0].Cells[2].Value.ToString(); //darta Number

            industryfileupload ownrindst = new industryfileupload();
            ownrindst.MdiParent = this.MdiParent;
            ownrindst.getdataa(dartanum, indid);
            //this.Hide();
            ownrindst.Show();
        }

        public void getDatesFromOutside(string frDt, string toDt)
        {
            chkRegDate.Checked = true;
            darta_dateFrom.Text = frDt;
            darta_dateTo.Text = toDt;
        }
    }
}
