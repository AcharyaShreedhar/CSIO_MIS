using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;


namespace CSIO
{
    public partial class industryreportmonthly : Form
    {

        //for dragging the form ----------------------------
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,int Msg, int wParam, int lParam);

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


        public industryreportmonthly()
        {
            InitializeComponent();
        }
        int id;
        bool isReady = false;
        string myRepTitle = "";

        public string reportDomain;
        public string reportType;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                // MySqlDataAdapter qry = new MySqlDataAdapter("SELECT    industryreg.industryid, ROW_NUMBER() OVER (ORDER BY industryreg.industryid) AS 'सि.नं.', GetNumberToUnicode(industryreg.industryregno) AS 'दर्ता नं', industryreg.industrynepname AS 'उद्योगको नाम', setup_district.distunicodename AS 'जिल्ला',  setup_vdc.vdcunicodename AS 'गा.पा.। न.पा.', GetNumberToUnicode(industryreg.industryward) AS 'वार्ड', industryreg.tole AS 'टोल', industryreg.branch AS 'शाखा', setup_subcategory.subcategory_unicodename AS 'स्तर', setup_subcategory_2.subcategory_unicodename AS 'कानूनी स्वरूप', setup_subcategory_1.subcategory_unicodename AS 'वर्ग', GetNumberToUnicode(industryreg.regdat) AS 'दर्ता मिति', GetNumberToUnicode(industryreg.renewdate) AS 'नविकरण मिति', industryreg.karobar AS 'उद्योगको उद्देश्य', GetNumberToUnicode(industryreg.yearlyturnover) AS 'बार्षिक उत्पादन', GetNumberToUnicode(industryreg.electricpower) AS 'विधुत शक्ती', GetNumberToUnicode(industryreg.statcapital) AS 'स्थिर पुँजी', GetNumberToUnicode(industryreg.varcapital) AS 'चालु पुँजी', GetNumberToUnicode(industryreg.femaleworker) AS 'महिला कामदार', GetNumberToUnicode(industryreg.maleworker) AS 'पुरूष कामदार', GetNumberToUnicode(industryreg.tax) AS 'राजश्व', industryreg.comment AS 'कैफियत', GetNumberToUnicode(industryreg.techworker) AS 'प्राविधिक', GetNumberToUnicode(industryreg.nontechworker)  AS 'अप्राविधिक', industryreg.rawmaterial AS 'कच्चा पदार्थ', GetNumberToUnicode(industryreg.fileno) AS 'फाइल नं', GetNumberToUnicode(industryreg.thelino) AS 'ठेली', GetNumberToUnicode(industryreg.fiscalyear) AS 'आ.व', GetNumberToUnicode(industryreg.adhikrit_capital) AS 'अधिकृत पुँजी', GetNumberToUnicode(industryreg.jari_capital) AS 'जारी पुँजी',                       owner_industry.ownerfname AS 'प्रोपाईटर पहिलो नाम', owner_industry.ownerlname AS 'थर', setup_district_1.distunicodename AS 'जिल्ला', setup_vdc_1.vdcunicodename AS 'म न पा।न पा।गा पा',                       owner_industry.ownervdcward AS 'वार्ड', owner_industry.email AS 'ईमेल', owner_industry.contact AS 'सम्पर्क', owner_industry.citzdate AS 'नागरिकता जारी मिति', owner_industry.citznum AS 'नागरिकता नं' FROM         setup_subcategory INNER JOIN                      industryreg INNER JOIN                      setup_district ON industryreg.industrydist = setup_district.distcode INNER JOIN                      setup_vdc ON industryreg.industryvdc = setup_vdc.VDC_SID ON setup_subcategory.subcategory_id = industryreg.industryscale INNER JOIN                      setup_subcategory AS setup_subcategory_2 ON industryreg.industrytype = setup_subcategory_2.subcategory_id INNER JOIN                      setup_subcategory AS setup_subcategory_1 ON industryreg.industrycat = setup_subcategory_1.subcategory_id INNER JOIN                      owner_industry ON industryreg.industryid = owner_industry.industryid INNER JOIN                      setup_district AS setup_district_1 ON owner_industry.ownerdistcode = setup_district_1.distcode INNER JOIN                      setup_vdc AS setup_vdc_1 ON owner_industry.ownervdccode = setup_vdc_1.VDC_SID where  Getint(industryreg.regdat)>=Getint(@darta_date) AND Getint(industryreg.regdat)<=Getint(@dateto)", con);    

              //  MySqlDataAdapter qry = new MySqlDataAdapter("select  * from (select   ROW_NUMBER() OVER (ORDER BY view_industryallrecord.industryid) AS 'सि.नं.', *, row_number() over (partition by industryid order by industryid) as RowNbr from view_industryallrecord WHERE Getint(view_industryallrecord.regdat)>=Getint(@darta_date) AND Getint(view_industryallrecord.regdat)<=Getint(@dateto)) source where RowNbr = 1", con);
                MySqlDataAdapter qry = new MySqlDataAdapter("select  * from (select   industryreg.industryid, ROW_NUMBER() OVER (ORDER BY industryreg.industryid) AS 'सि.नं.', GetNumberToUnicode(industryreg.industryregno) AS 'दर्ता नं', industryreg.industrynepname AS 'उद्योगको नाम', setup_district.distunicodename AS 'उद्योगको जिल्ला',  setup_vdc.vdcunicodename AS 'गा.वि.स.', GetNumberToUnicode(industryreg.industryward) AS 'उद्योगको वार्ड', industryreg.tole AS 'उद्योगको टोल', industryreg.branch AS 'शाखा', setup_subcategory.subcategory_unicodename AS 'स्तर', setup_subcategory_2.subcategory_unicodename AS 'कानूनी स्वरूप', setup_subcategory_1.subcategory_unicodename AS 'वर्ग', GetNumberToUnicode(industryreg.regdat) AS 'दर्ता मिति', GetNumberToUnicode(industryreg.renewdate) AS 'नविकरण मिति', industryreg.karobar AS 'उद्योगको उद्देश्य', GetNumberToUnicode(industryreg.yearlyturnover) AS 'बार्षिक उत्पादन', GetNumberToUnicode(industryreg.electricpower) AS 'विधुत शक्ती', GetNumberToUnicode(industryreg.statcapital) AS 'स्थिर पुँजी', GetNumberToUnicode(industryreg.varcapital) AS 'चालु पुँजी', GetNumberToUnicode(industryreg.femaleworker) AS 'महिला कामदार', GetNumberToUnicode(industryreg.maleworker) AS 'पुरूष कामदार', GetNumberToUnicode(industryreg.tax) AS 'राजश्व', industryreg.comment AS 'कैफियत', GetNumberToUnicode(industryreg.techworker) AS 'प्राविधिक', GetNumberToUnicode(industryreg.nontechworker)  AS 'अप्राविधिक', industryreg.rawmaterial AS 'कच्चा पदार्थ', GetNumberToUnicode(industryreg.fileno) AS 'फाइल नं', GetNumberToUnicode(industryreg.thelino) AS 'ठेली', GetNumberToUnicode(industryreg.fiscalyear) AS 'आ.व', GetNumberToUnicode(industryreg.adhikrit_capital) AS 'अधिकृत पुँजी', GetNumberToUnicode(industryreg.jari_capital) AS 'जारी पुँजी',                       owner_industry.ownerfname AS 'प्रोपाईटर पहिलो नाम', owner_industry.ownerlname AS 'थर', setup_district_1.distunicodename AS 'जिल्ला', setup_vdc_1.vdcunicodename AS 'म न पा।न पा।गा पा',                       owner_industry.ownervdcward AS 'वार्ड', owner_industry.email AS 'ईमेल', owner_industry.contact AS 'सम्पर्क', owner_industry.citzdate AS 'नागरिकता जारी मिति', owner_industry.citznum AS 'नागरिकता नं',row_number() over (partition by industryreg.industryid order by industryreg.industryid) as RowNbr FROM         setup_subcategory INNER JOIN                      industryreg INNER JOIN                      setup_district ON industryreg.industrydist = setup_district.distcode INNER JOIN                      setup_vdc ON industryreg.industryvdc = setup_vdc.VDC_SID ON setup_subcategory.subcategory_id = industryreg.industryscale INNER JOIN                      setup_subcategory AS setup_subcategory_2 ON industryreg.industrytype = setup_subcategory_2.subcategory_id INNER JOIN                      setup_subcategory AS setup_subcategory_1 ON industryreg.industrycat = setup_subcategory_1.subcategory_id INNER JOIN                      owner_industry ON industryreg.industryid = owner_industry.industryid INNER JOIN                      setup_district AS setup_district_1 ON owner_industry.ownerdistcode = setup_district_1.distcode INNER JOIN                      setup_vdc AS setup_vdc_1 ON owner_industry.ownervdccode = setup_vdc_1.VDC_SID where  Getint(industryreg.regdat)>=Getint(@darta_date) AND Getint(industryreg.regdat)<=Getint(@dateto) ) source where RowNbr = 1", sqlcon.con);
               
                qry.SelectCommand.Parameters.AddWithValue("@darta_date", darta_date.Text);
                qry.SelectCommand.Parameters.AddWithValue("@dateto", darta_dateTo.Text);
                DataTable tb = new DataTable();
                qry.Fill(tb);
                dataGridView1.DataSource = tb;
                dataGridView1.Columns[0].Visible = false;
                sqlcon.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //public void firmtype(AutoCompleteStringCollection dataCollection)
        //{
        //    if (sqlcon.con.State == ConnectionState.Closed)
        //    {
        //        sqlcon.con.Open();
        //    }
        //    string connetionString = null;
        //    SqlConnection connection;
        //    MySqlCommand command;
        //    MySqlDataAdapter adapter = new MySqlDataAdapter();
        //    DataSet ds = new DataSet();

        //    string sql = null;
        //    connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'"; g = null;
        //    sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (setup_subcategory.category_id = 9)";
        //    connection = new SqlConnection(connetionString);
        //    try
        //    {
        //        connection.Open();
        //        command = new MySqlCommand(sql, sqlcon.con);
        //        adapter.SelectCommand = command;
        //        adapter.Fill(ds);
        //        adapter.Dispose();
        //        command.Dispose();
        //        sqlcon.con.Close();

        //        industry_type.DataSource = ds.Tables[0];
        //        industry_type.ValueMember = "subcategory_id";
        //        industry_type.DisplayMember = "subcategory_unicodename";
        //        foreach (DataRow row in ds.Tables[0].Rows)
        //        {
        //            dataCollection.Add(row[1].ToString());
        //            citizen_off.Items.Add(row[0].ToString() + " Y " + row[1].ToString());
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Can not open connection ! " + ex);
        //    }

        //}
        //public void firmscale(AutoCompleteStringCollection dataCollection)
        //{
        //    if (sqlcon.con.State == ConnectionState.Closed)
        //    {
        //        sqlcon.con.Open();
        //    }
        //    string connetionString = null;
        //    SqlConnection connection;
        //    MySqlCommand command;
        //    MySqlDataAdapter adapter = new MySqlDataAdapter();
        //    DataSet ds = new DataSet();

        //    string sql = null;
        //    connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'"; g = null;
        //    sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (setup_subcategory.category_id = 8)";
        //    connection = new SqlConnection(connetionString);
        //    try
        //    {
        //        connection.Open();
        //        command = new MySqlCommand(sql, sqlcon.con);
        //        adapter.SelectCommand = command;
        //        adapter.Fill(ds);
        //        adapter.Dispose();
        //        command.Dispose();
        //        sqlcon.con.Close();

        //        industry_scale.DataSource = ds.Tables[0];
        //        industry_scale.ValueMember = "subcategory_id";
        //        industry_scale.DisplayMember = "subcategory_unicodename";
        //        foreach (DataRow row in ds.Tables[0].Rows)
        //        {
        //            dataCollection.Add(row[1].ToString());
        //            citizen_off.Items.Add(row[0].ToString() + " Y " + row[1].ToString());
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Can not open connection ! " + ex);
        //    }

        //}
        public void DisplayDataowner()
        {
            ////SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }

           // con.Open();
            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     owner_industry.ownerid, owner_industry.ownerfname AS 'नाम', owner_industry.ownerlname AS 'थर', setup_district_1.distunicodename AS 'ठेगाना', setup_vdc.vdcunicodename AS 'गा.पा.। न.पा.', GetNumberToUnicode(owner_industry.ownervdcward) AS 'वार्ड', owner_industry.ownertole AS 'टोल',    setup_citizenissueoff.citizen_officeunicodename AS 'जारी गर्ने कार्यालय', setup_district.distunicodename AS 'जारी जिल्ला', GetNumberToUnicode(owner_industry.citzdate) AS 'जारी मिति',   GetNumberToUnicode(owner_industry.citznum) AS 'ना.प्र.नं.', setup_subcategory_1.subcategory_unicodename AS 'बाबु।पति', owner_industry.ownergname AS 'बावु।पति', setup_subcategory_2.subcategory_unicodename AS 'बाजे।ससुरा', owner_industry.ownerfgname AS 'बाजे।ससुरा', setup_subcategory.subcategory_unicodename AS 'लिंग', owner_industry.comment AS 'कैफियत', GetNumberToUnicode(owner_industry.contact) As 'सम्पर्क नं', owner_industry.email As 'ईमेल' FROM         owner_industry INNER JOIN setup_district ON owner_industry.cddist = setup_district.distcode INNER JOIN setup_citizenissueoff ON owner_industry.ccio = setup_citizenissueoff.citizen_officeid INNER JOIN setup_vdc ON owner_industry.ownervdccode = setup_vdc.VDC_SID INNER JOIN     setup_district AS setup_district_1 ON owner_industry.ownerdistcode = setup_district_1.distcode INNER JOIN        setup_subcategory ON owner_industry.gender = setup_subcategory.subcategory_id INNER JOIN      setup_subcategory AS setup_subcategory_1 ON owner_industry.ownergrel = setup_subcategory_1.subcategory_id INNER JOIN       setup_subcategory AS setup_subcategory_2 ON owner_industry.ownerfgrel = setup_subcategory_2.subcategory_id WHERE     (owner_industry.industryid =@darta)", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@darta", id.ToString());
            DataTable tb = new DataTable();
            qry.Fill(tb);
            //dataGridView2.DataSource = tb;
            //dataGridView2.Columns[0].Visible = false;

            sqlcon.con.Close();

        }


        //private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    button2.Enabled = true;
        //    button3.Enabled = true;
        //    int i;

        //    i = dataGridView1.CurrentRow.Index;
        //    id = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
        //    DisplayDataowner();
        //}


        //public void firmobj(AutoCompleteStringCollection dataCollection)
        //{

        //    if (sqlcon.con.State == ConnectionState.Closed)
        //    {
        //        sqlcon.con.Open();
        //    }
        //    //string connetionString = null;
        //    //SqlConnection connection;
        //    MySqlCommand command;
        //    MySqlDataAdapter adapter = new MySqlDataAdapter();
        //    DataSet ds = new DataSet();

        //    string sql = null;
        //    //connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;
        //    sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (setup_subcategory.category_id = 10)";
        //    //connection = new SqlConnection(connetionString);
        //    try
        //    {
        //        //connection.Open();
        //        command = new MySqlCommand(sql, sqlcon.con);
        //        adapter.SelectCommand = command;
        //        adapter.Fill(ds);
        //        adapter.Dispose();
        //        command.Dispose();
        //        sqlcon.con.Close();

        //        industry_cat.DataSource = ds.Tables[0];
        //        industry_cat.ValueMember = "subcategory_id";
        //        industry_cat.DisplayMember = "subcategory_unicodename";
        //        foreach (DataRow row in ds.Tables[0].Rows)
        //        {
        //            dataCollection.Add(row[1].ToString());
        //            //citizen_off.Items.Add(row[0].ToString()+" Y "+row[1].ToString());
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Can not open connection ! " + ex);
        //    }

        //}

        private void industryreport_Load(object sender, EventArgs e)
        {

            global.createBorderAround(this, Color.Teal, 2);

            isReady = false; //for comboboxes
            lblTitle.Text = reportType; //Industry Report or Commerce Report

            global.fillCombo(comboMonth, "SELECT monthid, monthunicodename From setup_month", "monthunicodename", "monthid");
            global.fillCombo(comboFY, "SELECT concat('20',FY_ID) as FY_ID, GetNumberToUnicode(format_fiscal_yr(FY)) as FY From setup_fy", "FY", "FY_ID");
            global.fillCombo(comboFYChaumasic, "SELECT concat('20',FY_ID) as FY_ID, GetNumberToUnicode(format_fiscal_yr(FY)) as FY From setup_fy", "FY", "FY_ID");

            //global.fillCombo(comboYear, "SELECT concat('20',FY_ID) as FY_ID, GetNumberToUnicode(concat('20',FY_ID)) as FY From setup_fy", "FY", "FY_ID");

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

                buttonDistrictWiseNum.Enabled = false; //district wise report is only for mantralay and nirdeshanalay
            }
            else
            {
                comboDistrict.SelectedIndex = 0; //सबै जिल्ला
                comboDistrict.Enabled = true;

                buttonDistrictWiseNum.Enabled = true; //district wise report is only for mantralay and nirdeshanalay
            }


            int YearNumber=0; 
              
            for (int iii=0;iii<=comboFY.Items.Count-1;iii++)
            {

                DataRowView FirstYear = comboFY.Items[iii] as DataRowView; //getting the first year
                if (FirstYear != null)
                 //   YearNumber = 2000 + Convert.ToInt32(FirstYear.Row["FY_ID"]);
                YearNumber = Convert.ToInt32(FirstYear.Row["FY_ID"]);

                comboYear.Items.Add(global.convertNumToUnicode(Convert.ToString(YearNumber)));
                
            }

            string curNepDate = global.todaynepslash;
            string MonNm = global.convertUnicodeToNum(curNepDate.Substring(5, 2));

            //For Baishak Jestha And Ashar - add one more year as they are from current fiscal year but Year is +1 from the value of FY 
            if (Convert.ToInt16(MonNm) >= 1 && Convert.ToInt16(MonNm) <= 3)
                YearNumber++;

            comboYear.Items.Add(global.convertNumToUnicode(YearNumber.ToString()));

            getCurMon();
            getCurChaumasic();
            getCurYear();

            darta_date.Text = global.todaynepslash;


            //positioning at the center of parent
            this.Top = 10;
            this.Left = this.Parent.Width / 2 - this.Width / 2;

            isReady = true; //for comboboxes
       }

        private void button4_Click(object sender, EventArgs e)
        {
            try{
            if (!darta_date.MaskFull || !darta_dateTo.MaskFull)
            {
                MessageBox.Show("मिति पुरा राखेर फेरी प्रयास गर्नुहोला ।", "मिति पुरा भएन");
            }
            else
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                ////SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

            //con.Open();

            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     industryreg.industryid, GetNumberToUnicode(industryreg.industryregno) AS 'दर्ता नं', industryreg.industrynepname AS 'उद्योगको नाम', setup_district.distunicodename AS 'जिल्ला',  setup_vdc.vdcunicodename AS 'गा.पा.। न.पा.', GetNumberToUnicode(industryreg.industryward) AS 'वार्ड', industryreg.tole AS 'टोल', industryreg.branch AS 'शाखा', setup_subcategory.subcategory_unicodename AS 'स्तर', setup_subcategory_2.subcategory_unicodename AS 'कानूनी स्वरूप', setup_subcategory_1.subcategory_unicodename AS 'वर्ग', GetNumberToUnicode(industryreg.regdat) AS 'दर्ता मिति', GetNumberToUnicode(industryreg.renewdate) AS 'नविकरण मिति', industryreg.karobar AS 'उद्योगको उद्देश्य', GetNumberToUnicode(industryreg.yearlyturnover) AS 'बार्षिक उत्पादन', GetNumberToUnicode(industryreg.electricpower) AS 'विधुत शक्ती', GetNumberToUnicode(industryreg.statcapital) AS 'स्थिर पुँजी', GetNumberToUnicode(industryreg.varcapital) AS 'चालु पुँजी', GetNumberToUnicode(industryreg.femaleworker) AS 'महिला कामदार', GetNumberToUnicode(industryreg.maleworker) AS 'पुरूष कामदार', GetNumberToUnicode(industryreg.tax) AS 'राजश्व', industryreg.comment AS 'कैफियत', GetNumberToUnicode(industryreg.techworker) AS 'प्राविधिक', GetNumberToUnicode(industryreg.nontechworker)  AS 'अप्राविधिक', industryreg.rawmaterial AS 'कच्चा पदार्थ', GetNumberToUnicode(industryreg.fileno) AS 'फाइल नं', GetNumberToUnicode(industryreg.thelino) AS 'ठेली', GetNumberToUnicode(industryreg.fiscalyear) AS 'आ.व', GetNumberToUnicode(industryreg.adhikrit_capital) AS 'अधिकृत पुँजी', GetNumberToUnicode(industryreg.jari_capital) AS 'जारी पुँजी',                       owner_industry.ownerfname AS 'प्रोपाईटर पहिलो नाम', owner_industry.ownerlname AS 'थर', setup_district_1.distunicodename AS 'जिल्ला', setup_vdc_1.vdcunicodename AS 'म न पा।न पा।गा पा',                       owner_industry.ownervdcward AS 'वार्ड', owner_industry.email AS 'ईमेल', owner_industry.contact AS 'सम्पर्क', owner_industry.citzdate AS 'नागरिकता जारी मिति', owner_industry.citznum AS 'नागरिकता नं' FROM         setup_subcategory INNER JOIN                      industryreg INNER JOIN                      setup_district ON industryreg.industrydist = setup_district.distcode INNER JOIN                      setup_vdc ON industryreg.industryvdc = setup_vdc.VDC_SID ON setup_subcategory.subcategory_id = industryreg.industryscale INNER JOIN                      setup_subcategory AS setup_subcategory_2 ON industryreg.industrytype = setup_subcategory_2.subcategory_id INNER JOIN                      setup_subcategory AS setup_subcategory_1 ON industryreg.industrycat = setup_subcategory_1.subcategory_id INNER JOIN                      owner_industry ON industryreg.industryid = owner_industry.industryid INNER JOIN                      setup_district AS setup_district_1 ON owner_industry.ownerdistcode = setup_district_1.distcode INNER JOIN                      setup_vdc AS setup_vdc_1 ON owner_industry.ownervdccode = setup_vdc_1.VDC_SID where  Getint(industryreg.regdat)>=Getint(@darta_date) AND Getint(industryreg.regdat)<=Getint(@dateto) AND industryreg.industryscale=@scale", sqlcon.con);    
            qry.SelectCommand.Parameters.AddWithValue("@darta_date", darta_date.Text);          
            qry.SelectCommand.Parameters.AddWithValue("@dateto", darta_dateTo.Text);
           // qry.SelectCommand.Parameters.AddWithValue("@scale", industry_scale.SelectedValue.ToString());
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView1.DataSource = tb;
            dataGridView1.Columns[0].Visible = false;
            sqlcon.con.Close();
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try{
            if (!darta_date.MaskFull || !darta_dateTo.MaskFull)
            {
                MessageBox.Show("मिति पुरा राखेर फेरी प्रयास गर्नुहोला ।", "मिति पुरा भएन");
            }
            else
            {
               // //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

              //  con.Open();
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }

                MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     industryreg.industryid, GetNumberToUnicode(industryreg.industryregno) AS 'दर्ता नं', industryreg.industrynepname AS 'उद्योगको नाम', setup_district.distunicodename AS 'जिल्ला',  setup_vdc.vdcunicodename AS 'गा.पा.। न.पा.', GetNumberToUnicode(industryreg.industryward) AS 'वार्ड', industryreg.tole AS 'टोल', industryreg.branch AS 'शाखा', setup_subcategory.subcategory_unicodename AS 'स्तर', setup_subcategory_2.subcategory_unicodename AS 'कानूनी स्वरूप', setup_subcategory_1.subcategory_unicodename AS 'वर्ग', GetNumberToUnicode(industryreg.regdat) AS 'दर्ता मिति', GetNumberToUnicode(industryreg.renewdate) AS 'नविकरण मिति', industryreg.karobar AS 'उद्योगको उद्देश्य', GetNumberToUnicode(industryreg.yearlyturnover) AS 'बार्षिक उत्पादन', GetNumberToUnicode(industryreg.electricpower) AS 'विधुत शक्ती', GetNumberToUnicode(industryreg.statcapital) AS 'स्थिर पुँजी', GetNumberToUnicode(industryreg.varcapital) AS 'चालु पुँजी', GetNumberToUnicode(industryreg.femaleworker) AS 'महिला कामदार', GetNumberToUnicode(industryreg.maleworker) AS 'पुरूष कामदार', GetNumberToUnicode(industryreg.tax) AS 'राजश्व', industryreg.comment AS 'कैफियत', GetNumberToUnicode(industryreg.techworker) AS 'प्राविधिक', GetNumberToUnicode(industryreg.nontechworker)  AS 'अप्राविधिक', industryreg.rawmaterial AS 'कच्चा पदार्थ', GetNumberToUnicode(industryreg.fileno) AS 'फाइल नं', GetNumberToUnicode(industryreg.thelino) AS 'ठेली', GetNumberToUnicode(industryreg.fiscalyear) AS 'आ.व', GetNumberToUnicode(industryreg.adhikrit_capital) AS 'अधिकृत पुँजी', GetNumberToUnicode(industryreg.jari_capital) AS 'जारी पुँजी',                       owner_industry.ownerfname AS 'प्रोपाईटर पहिलो नाम', owner_industry.ownerlname AS 'थर', setup_district_1.distunicodename AS 'जिल्ला', setup_vdc_1.vdcunicodename AS 'म न पा।न पा।गा पा',                       owner_industry.ownervdcward AS 'वार्ड', owner_industry.email AS 'ईमेल', owner_industry.contact AS 'सम्पर्क', owner_industry.citzdate AS 'नागरिकता जारी मिति', owner_industry.citznum AS 'नागरिकता नं' FROM         setup_subcategory INNER JOIN                      industryreg INNER JOIN                      setup_district ON industryreg.industrydist = setup_district.distcode INNER JOIN                      setup_vdc ON industryreg.industryvdc = setup_vdc.VDC_SID ON setup_subcategory.subcategory_id = industryreg.industryscale INNER JOIN                      setup_subcategory AS setup_subcategory_2 ON industryreg.industrytype = setup_subcategory_2.subcategory_id INNER JOIN                      setup_subcategory AS setup_subcategory_1 ON industryreg.industrycat = setup_subcategory_1.subcategory_id INNER JOIN                      owner_industry ON industryreg.industryid = owner_industry.industryid INNER JOIN                      setup_district AS setup_district_1 ON owner_industry.ownerdistcode = setup_district_1.distcode INNER JOIN                      setup_vdc AS setup_vdc_1 ON owner_industry.ownervdccode = setup_vdc_1.VDC_SID where  Getint(industryreg.regdat)>=Getint(@darta_date) AND Getint(industryreg.regdat)<=Getint(@dateto) AND industryreg.industrytype=@scale", sqlcon.con);
                qry.SelectCommand.Parameters.AddWithValue("@darta_date", darta_date.Text);
                qry.SelectCommand.Parameters.AddWithValue("@dateto", darta_dateTo.Text);
                //qry.SelectCommand.Parameters.AddWithValue("@scale", industry_type.SelectedValue.ToString());
                DataTable tb = new DataTable();
                qry.Fill(tb);
                dataGridView1.DataSource = tb;
                dataGridView1.Columns[0].Visible = false;
                sqlcon.con.Close();
            }
              }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (!darta_date.MaskFull || !darta_dateTo.MaskFull)
                {
                    MessageBox.Show("मिति पुरा राखेर फेरी प्रयास गर्नुहोला ।", "मिति पुरा भएन");
                }
                else
                {
                   // //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

                   // con.Open();
                    if (sqlcon.con.State == ConnectionState.Closed)
                    {
                        sqlcon.con.Open();
                    }
                    MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     industryreg.industryid, GetNumberToUnicode(industryreg.industryregno) AS 'दर्ता नं', industryreg.industrynepname AS 'उद्योगको नाम', setup_district.distunicodename AS 'जिल्ला',  setup_vdc.vdcunicodename AS 'गा.पा.। न.पा.', GetNumberToUnicode(industryreg.industryward) AS 'वार्ड', industryreg.tole AS 'टोल', industryreg.branch AS 'शाखा', setup_subcategory.subcategory_unicodename AS 'स्तर', setup_subcategory_2.subcategory_unicodename AS 'कानूनी स्वरूप', setup_subcategory_1.subcategory_unicodename AS 'वर्ग', GetNumberToUnicode(industryreg.regdat) AS 'दर्ता मिति', GetNumberToUnicode(industryreg.renewdate) AS 'नविकरण मिति', industryreg.karobar AS 'उद्योगको उद्देश्य', GetNumberToUnicode(industryreg.yearlyturnover) AS 'बार्षिक उत्पादन', GetNumberToUnicode(industryreg.electricpower) AS 'विधुत शक्ती', GetNumberToUnicode(industryreg.statcapital) AS 'स्थिर पुँजी', GetNumberToUnicode(industryreg.varcapital) AS 'चालु पुँजी', GetNumberToUnicode(industryreg.femaleworker) AS 'महिला कामदार', GetNumberToUnicode(industryreg.maleworker) AS 'पुरूष कामदार', GetNumberToUnicode(industryreg.tax) AS 'राजश्व', industryreg.comment AS 'कैफियत', GetNumberToUnicode(industryreg.techworker) AS 'प्राविधिक', GetNumberToUnicode(industryreg.nontechworker)  AS 'अप्राविधिक', industryreg.rawmaterial AS 'कच्चा पदार्थ', GetNumberToUnicode(industryreg.fileno) AS 'फाइल नं', GetNumberToUnicode(industryreg.thelino) AS 'ठेली', GetNumberToUnicode(industryreg.fiscalyear) AS 'आ.व', GetNumberToUnicode(industryreg.adhikrit_capital) AS 'अधिकृत पुँजी', GetNumberToUnicode(industryreg.jari_capital) AS 'जारी पुँजी',                       owner_industry.ownerfname AS 'प्रोपाईटर पहिलो नाम', owner_industry.ownerlname AS 'थर', setup_district_1.distunicodename AS 'जिल्ला', setup_vdc_1.vdcunicodename AS 'म न पा।न पा।गा पा',                       owner_industry.ownervdcward AS 'वार्ड', owner_industry.email AS 'ईमेल', owner_industry.contact AS 'सम्पर्क', owner_industry.citzdate AS 'नागरिकता जारी मिति', owner_industry.citznum AS 'नागरिकता नं' FROM         setup_subcategory INNER JOIN                      industryreg INNER JOIN                      setup_district ON industryreg.industrydist = setup_district.distcode INNER JOIN                      setup_vdc ON industryreg.industryvdc = setup_vdc.VDC_SID ON setup_subcategory.subcategory_id = industryreg.industryscale INNER JOIN                      setup_subcategory AS setup_subcategory_2 ON industryreg.industrytype = setup_subcategory_2.subcategory_id INNER JOIN                      setup_subcategory AS setup_subcategory_1 ON industryreg.industrycat = setup_subcategory_1.subcategory_id INNER JOIN                      owner_industry ON industryreg.industryid = owner_industry.industryid INNER JOIN                      setup_district AS setup_district_1 ON owner_industry.ownerdistcode = setup_district_1.distcode INNER JOIN                      setup_vdc AS setup_vdc_1 ON owner_industry.ownervdccode = setup_vdc_1.VDC_SID where  Getint(industryreg.regdat)>=Getint(@darta_date) AND Getint(industryreg.regdat)<=Getint(@dateto) AND industryreg.industrycat=@scale", sqlcon.con);
                    qry.SelectCommand.Parameters.AddWithValue("@darta_date", darta_date.Text);
                    qry.SelectCommand.Parameters.AddWithValue("@dateto", darta_dateTo.Text);
                    //qry.SelectCommand.Parameters.AddWithValue("@scale", industry_cat.SelectedValue.ToString());
                    DataTable tb = new DataTable();
                    qry.Fill(tb);
                    dataGridView1.DataSource = tb;
                    dataGridView1.Columns[0].Visible = false;
                    sqlcon.con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                ////SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

                //con.Open();
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }

                MySqlDataAdapter qry = new MySqlDataAdapter("SELECT    N'लघु' As 'विवरण', COUNT(industryscale) AS 'संख्या' FROM industryreg WHERE     (industryscale = 131) AND (Getint(industryreg.regdat)>=Getint(@darta_date) AND Getint(industryreg.regdat)<=Getint(@dateto)) UNION ALL SELECT     N'घरेलु' ,COUNT(industryscale) AS 'संख्या' FROM         industryreg AS industryreg_1 WHERE     (industryscale = 81) AND (Getint(industryreg_1.regdat)>=Getint(@darta_date) AND Getint(industryreg_1.regdat)<=Getint(@dateto)) UNION ALL SELECT     N'साना' ,COUNT(industryscale) AS 'संख्या' FROM         industryreg AS industryreg_2 WHERE     (industryscale = 82) AND (Getint(industryreg_2.regdat)>=Getint(@darta_date) AND Getint(industryreg_2.regdat)<=Getint(@dateto)) UNION ALL SELECT     N'नविकरण' ,COUNT(industryid) AS 'संख्या' FROM         industryrenew WHERE    (Getint(industryrenew.updatenepdate)>=Getint(@darta_date) AND Getint(industryrenew.updatenepdate)<=Getint(@dateto)) UNION ALL SELECT     N'नामसारी' ,COUNT(DISTINCT dartanum) AS 'संख्या' FROM         update_industry  WHERE     (updattitle = 125) AND (Getint(update_industry.updatenepdate)>=Getint(@darta_date) AND Getint(update_industry.updatenepdate)<=Getint(@dateto)) UNION ALL SELECT     N'ठाउँसारी' ,COUNT(DISTINCT dartanum) AS 'संख्या' FROM         update_industry WHERE     (updattitle = 123) AND (Getint(update_industry.updatenepdate)>=Getint(@darta_date) AND Getint(update_industry.updatenepdate)<=Getint(@dateto)) UNION ALL SELECT     N'पुँजीबृध्दि' ,COUNT(DISTINCT dartanum) AS 'संख्या' FROM         update_industry WHERE     (updattitle = 122) AND (Getint(update_industry.updatenepdate)>=Getint(@darta_date) AND Getint(update_industry.updatenepdate)<=Getint(@dateto)) UNION ALL SELECT     N'नाम परिवर्रतन' ,COUNT(DISTINCT dartanum) AS 'संख्या' FROM         update_industry WHERE     (updattitle = 121) AND (Getint(update_industry.updatenepdate)>=Getint(@darta_date) AND Getint(update_industry.updatenepdate)<=Getint(@dateto)) UNION ALL SELECT     N'उद्देश्य थप' ,COUNT(DISTINCT dartanum) AS 'संख्या' FROM         update_industry WHERE     (updattitle = 124) AND (Getint(update_industry.updatenepdate)>=Getint(@darta_date) AND Getint(update_industry.updatenepdate)<=Getint(@dateto)) UNION ALL SELECT     N'लगतकट्टा' ,COUNT(DISTINCT industryid) AS 'संख्या'FROM        industryclose WHERE     (Getint(industryclose.updatenepdate)>=Getint(@darta_date) AND Getint(industryclose.updatenepdate)<=Getint(@dateto)) UNION ALL SELECT     N'पुरूष उद्यमी' ,COUNT(industryid) AS 'संख्या'FROM        owner_industry WHERE gender=11 AND    (Getint(GetUnicodeToNumber(owner_industry.updnepdate))>=Getint(@darta_date) AND Getint(GetUnicodeToNumber(owner_industry.updnepdate))<=Getint(@dateto)) UNION ALL SELECT     N'महिला उद्यमी' ,COUNT(industryid) AS 'संख्या'FROM        owner_industry WHERE gender=10 AND    (Getint(GetUnicodeToNumber(owner_industry.updnepdate))>=Getint(@darta_date) AND Getint(GetUnicodeToNumber(owner_industry.updnepdate))<=Getint(@dateto)) UNION ALL SELECT     N'प्राइभेट' ,COUNT(industryid) AS 'संख्या' FROM         industryreg AS industryreg_5 WHERE     (industrytype = 91) AND (Getint(industryreg_5.regdat)>=Getint(@darta_date) AND Getint(industryreg_5.regdat)<=Getint(@dateto)) UNION ALL SELECT     N'साझेदारी' ,COUNT(industryid) AS 'संख्या' FROM         industryreg AS industryreg_6 WHERE     (industrytype = 92) AND (Getint(industryreg_6.regdat)>=Getint(@darta_date) AND Getint(industryreg_6.regdat)<=Getint(@dateto)) UNION ALL SELECT     N'नयाँ दर्ता जम्मा राजश्व' ,SUM(tax) AS 'संख्या' FROM         industryreg AS industryreg_8 WHERE     (Getint(industryreg_8.regdat)>=Getint(@darta_date) AND Getint(industryreg_8.regdat)<=Getint(@dateto)) UNION ALL SELECT     N'जम्मा चालु पुँजी' ,SUM(varcapital) AS 'संख्या' FROM         industryreg AS industryreg_7 WHERE     (Getint(industryreg_7.regdat)>=Getint(@darta_date) AND Getint(industryreg_7.regdat)<=Getint(@dateto)) UNION ALL SELECT     N'जम्मा स्थिर पुँजी' ,SUM(statcapital) AS 'संख्या' FROM         industryreg AS industryreg_9 WHERE     (Getint(industryreg_9.regdat)>=Getint(@darta_date) AND Getint(industryreg_9.regdat)<=Getint(@dateto))", sqlcon.con);
                qry.SelectCommand.Parameters.AddWithValue("@darta_date", darta_date.Text);
                qry.SelectCommand.Parameters.AddWithValue("@dateto", darta_dateTo.Text);
                DataTable tb = new DataTable();
                qry.Fill(tb);
                dataGridView1.DataSource = tb;
                // dataGridView1.Columns[0].Visible = false;
                sqlcon.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void darta_date_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                string csioid = global.csioid;

                MonthlyReportDisplayViewer ciname = new MonthlyReportDisplayViewer();
                ciname.MdiParent = this.MdiParent;
                ciname.getdata(darta_dateFrom.Text, darta_dateTo.Text, csioid);
                //this.Hide();
                ciname.Show();
            }
            catch (Exception ex){
                MessageBox.Show(ex.ToString());
            }
        
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioExactDate_CheckedChanged(object sender, EventArgs e)
        {
            darta_date.Enabled = radioExactDate.Checked;
        }

        private void radioPeriod_CheckedChanged(object sender, EventArgs e)
        {
            darta_dateFrom.Enabled = radioPeriod.Checked;
            darta_dateTo.Enabled = radioPeriod.Checked;
        }

        private string getReportTitle()
        {
            if (radioExactDate.Checked)
                myRepTitle = "मिति " + darta_date.Text.ToString() + " को ";

            else if (radioPeriod.Checked)
                myRepTitle = "मिति " + darta_dateFrom.Text.ToString() + " देखि " + darta_dateTo.Text.ToString() + " सम्मको ";

            else if (radioYearly.Checked)
                myRepTitle = "आर्थिक वर्ष " + comboFY.Text + " (" + darta_dateFrom.Text.ToString() + " देखि " + darta_dateTo.Text.ToString() + " सम्म) को ";

            else if (radioChaumasic.Checked)
                myRepTitle = "आर्थिक वर्ष " + comboFY.Text + " को " + comboChaumasic.Text + " (" + darta_dateFrom.Text.ToString() + " देखि " + darta_dateTo.Text.ToString() + " सम्म) को ";

            else if (radioMonthly.Checked)
                myRepTitle = "वर्ष " + comboFY.Text + " को " + comboMonth.Text + " महिनाको ";

            return myRepTitle;
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            string dateFROM="", dateTO="";
            if (radioExactDate.Checked)
            {
                if (darta_date.MaskFull)
                {
                    dateFROM = darta_date.Text;
                    dateTO = darta_date.Text;
                }
                else
                {
                    MessageBox.Show("कृपया पुरा मिति राखेर पुन: प्रयास गर्नुहोला !", "मिति", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    darta_date.Focus();
                    darta_date.SelectAll();
                    return;
                }
            }
            else
            {

                if (darta_dateFrom.MaskFull && darta_dateTo.MaskFull)
                {
                    dateFROM = darta_dateFrom.Text;
                    dateTO = darta_dateTo.Text;
                }
                else
                {
                    MessageBox.Show("कृपया पुरा मिति राखेर पुन: प्रयास गर्नुहोला !", "मिति", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (!darta_dateFrom.MaskFull)
                    {
                        darta_dateFrom.Focus();
                        darta_dateFrom.SelectAll();
                        return;
                    }
                    else
                    {
                        darta_dateTo.Focus();
                        darta_dateTo.SelectAll();
                    }
                
                }
            }

            //District ID
            string distID = comboDistrict.SelectedValue.ToString();
            if (distID == "0") distID = ""; 
            //condition in SQL is LIKE '%distid%' -- so that data of every district will be seen in case of all is selected
            
            // creating new WorkBook within Excel application  
            Excel.Application xlApp;
            Excel.Workbook xlwb;
            Excel.Worksheet xlws;
            xlApp = new Excel.Application();
            string template = Application.StartupPath + "\\csio_industry_detail_report_template_new4.xlsx";
            xlwb = xlApp.Workbooks.Add (template);
            xlws = xlwb.Worksheets[1];
            object misValue = System.Reflection.Missing.Value;

            try
            {
                //OFFICE HEADINGS
                string sqsm = "SELECT govtname,ministryname,departmentname,officeunicodename,office_address,provincename, GetNumberToUnicode(phone),GetNumberToUnicode(fax),email FROM setup_office where isCur=1";

                string[] offhead = global.getSingleRowFromTable(sqsm);
                string myHead=offhead[0];
                myHead +="\n " + offhead[1];
                myHead += "\n " + offhead[2];
                myHead += "\n " + offhead[3] + ", " + offhead[4];
                myHead += "\n " + offhead[5];

                //ADDRESS
                string myAddr = "फोन नं:" + offhead[6] + "\n फ्याक्स नं: " + offhead[7] + "\n इमेल: " + offhead[8];


                xlws.Cells[1, 1] = myHead;
                xlws.Cells[1, 20] = myAddr;

                //REPORT HEADER
                //myHead = "मिति ";
                //myHead += darta_dateFrom.Text ;
                //myHead +=" देखि ";
                //myHead += darta_dateTo.Text;
                //myHead += " सम्मको उद्योगसम्बन्धी प्रगति विवरण" ;

                xlws.Cells[2, 1] = getReportTitle() + " उद्योग सम्बन्धी प्रगति प्रतिवेदन ";

                //con.Open();
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }


                MySqlDataAdapter qry = new MySqlDataAdapter("CALL industryReport(@datefrom,@dateto,@distid)", sqlcon.con);
                qry.SelectCommand.Parameters.AddWithValue("@datefrom", dateFROM);
                qry.SelectCommand.Parameters.AddWithValue("@dateto", dateTO);
                qry.SelectCommand.Parameters.AddWithValue("@distid", distID);
                DataTable tb = new DataTable();
                qry.Fill(tb);
                dataGridView1.DataSource = tb;
                // dataGridView1.Columns[0].Visible = false;


                //INDUSTRY REGISTRATION -- FIRST PART OF THE REPORT
                // FILLING THE DATA
                //as per the template - the first data is in 7th row but col is always 1
                //so added 7 in j (j+7, k+1)
                for (int j = 0; j < tb.Rows.Count; j++)
                {
                    for (int k = 0; k < tb.Columns.Count; k++)
                    {
                        xlws.Cells[j + 7 , k + 1] = tb.Rows[j].ItemArray[k].ToString();  
                    }
                }
                //} 


                // OTHER WORKS RELATED TO INDUSTRY (Second part of the report)

                float Rajaswa=0;

/* SELECT GetNumberToUnicode(sum(if(update_industry.updattitle='121',1,0))) As 'NameUpdate',
 * GetNumberToUnicode(sum(if(update_industry.updattitle='122',1,0))) As 'CapitalUpdate',
 * GetNumberToUnicode(sum(if(update_industry.updattitle='123',1,0))) As 'LocationUpdate',
 * GetNumberToUnicode(sum(if(update_industry.updattitle='124',1,0))) As 'ObjectiveUpdate', 
 * GetNumberToUnicode(sum(if(update_industry.updattitle='125',1,0))) As 'NaamsariUpdate',
GetNumberToUnicode(sum(if((update_industry.updattitle='125' or update_industry.updattitle='121'),1,0))) As 'Name_Namsari_Update',
SUM(update_industry.tax) As 'taxupdate' 
 * From update_industry WHERE Getint(GetUnicodeToNumber(update_industry.decisiondate))>=Getint(GetUnicodeToNumber(datefrom)) AND Getint(GetUnicodeToNumber(update_industry. decisiondate))<=Getint(GetUnicodeToNumber(dateto))
 */

                string[] data1 = global.getSingleRowFromProcedure("GetIndustryUpdateReport", "@datefrom,@dateto,@distid", dateFROM + "," + dateTO + "," + distID);

                xlws.Cells[22, 13] = data1[5]; //namsari and name update
                xlws.Cells[22, 14] = data1[2]; //thausari
                xlws.Cells[22, 15] = data1[3]; //uddeshya
                xlws.Cells[22, 18] = data1[1]; //puji briddhi

                if (data1[6] != "")
                Rajaswa = float.Parse(data1[6]);
               
                 /* GetIndustryRenewReport
                 * SELECT GetNumberToUnicode(sum(if(industryrenew.updattitle='127',1,0))) As 'RenewIndustry',
                 * SUM(industryrenew.tax) As 'taxrenew' From industryrenew 
                 * WHERE Getint(GetUnicodeToNumber(industryrenew.decisiondate))>=Getint(GetUnicodeToNumber(datefrom)) AND 
                 * Getint(GetUnicodeToNumber(industryrenew.decisiondate))<=Getint(GetUnicodeToNumber(dateto))
                 */
              

                string[] data2 = global.getSingleRowFromProcedure("GetIndustryRenewReport", "@datefrom,@dateto,@distid", dateFROM + "," + dateTO + "," + distID);

                xlws.Cells[22, 12] = data2[0]; //nabikarn

                if(data2[1]!="")
                Rajaswa += float.Parse(data2[1]);

                /* 
                 GetIndustryCloseReport
                 SELECT GetNumberToUnicode(sum(if(industryclose.updattitle='128',1,0))) As 'CloseIndustry',
                 * SUM(industryclose.tax) As 'CloseTax' From industryclose 
                 * WHERE Getint(GetUnicodeToNumber(industryclose.decisiondate))>=Getint(GetUnicodeToNumber(datefrom)) AND 
                 * Getint(GetUnicodeToNumber(industryclose.decisiondate))<=Getint(GetUnicodeToNumber(dateto))
                 */
                string[] data3 = global.getSingleRowFromProcedure("GetIndustryCloseReport", "@datefrom,@dateto,@distid", dateFROM + "," + dateTO + "," + distID);

                xlws.Cells[22, 16] = data3[0]; //lagat katta

                if (data3[1]!="")
                Rajaswa += float.Parse(data3[1]);

                xlws.Cells[22, 22] = Rajaswa;

                
                //FORMATTING THE EXCEL FILE
                xlws.Columns.AutoFit();

               //SAVING FILE
               //string filePath;
               //filePath = "D:\\csio_report.xlsx";

                //if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                //{
                //    filePath = folderBrowserDialog1.SelectedPath;
                //}
                //xlwb.SaveAs(filePath);



                //SAVING FILE IN USER DEFINED SPACE

                string filePath="";

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
                    xlwb=null;
                    xlApp.Quit();
                    xlApp=null;
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

        private void getCurChaumasic()
        {
            //DateTime curEngDate = DateTime.Today;

            string fys = global.getSingleDataFromTable("SELECT GetNumberToUnicode(format_fiscal_yr(FY)) FROM setup_fy WHERE FY_ID=" + global.fyid);
            comboFYChaumasic.Text = fys;

            string curNepDate = global.todaynepslash;
            string YearNm = global.convertUnicodeToNum(curNepDate.Substring(0, 4));
            string MonNm = global.convertUnicodeToNum(curNepDate.Substring(5, 2));
            int mno=Convert.ToInt16(MonNm);
            if (mno >= 4 && mno <= 7)
            {
                
                comboChaumasic.SelectedIndex = 0;
            }
            else if (mno >= 8 && mno <= 11)
            {
                comboChaumasic.SelectedIndex = 1;
            }
            else
            {
                comboFYChaumasic.SelectedIndex = comboFYChaumasic.SelectedIndex - 1;
                comboChaumasic.SelectedIndex = 2;
            }
        }

        private void getCurMon()
        {
            DateTime curEngDate = DateTime.Today;
            string curNepDate = global.todaynepslash;

            string YearNm = global.convertUnicodeToNum(curNepDate.Substring(0, 4));

            string MonNm = global.convertUnicodeToNum(curNepDate.Substring(5, 2));
            comboYear.Text = global.convertNumToUnicode(YearNm);

            DisplayMonth(YearNm, MonNm);
        }


        private void getPrevMon()
        {
            DateTime curEngDate = DateTime.Today;
            string curNepDate = global.todaynepslash;

            string YearNm = global.convertUnicodeToNum(curNepDate.Substring(0, 4));

            string MonNm = global.convertUnicodeToNum(curNepDate.Substring(5, 2));

            if (Convert.ToInt16(MonNm) == 1) // if it is Baishak go to previous year and 12th month
            {
                YearNm = Convert.ToString(Convert.ToInt16(YearNm) - 1);
                MonNm = "12";
            }
            else
            {
                MonNm = Convert.ToString(Convert.ToInt16(MonNm) - 1);
            }

            DisplayMonth(YearNm, MonNm);
        }

        private void DisplayMonth(string YearNumEng, string MonNumEng)
        {
            if (MonNumEng.Length == 1)
                MonNumEng = "0" + MonNumEng;

            string MonFld = "NDNepM" + MonNumEng;

            //DISPLAY IN DATE RANGE BOX
            //get total days in a month of a year (for last date of month)
            string monLD = global.getSingleDataFromTable("SELECT " + MonFld + " FROM setup_nepcalender WHERE Year=" + YearNumEng);

            darta_dateFrom.Text = global.convertNumToUnicode(YearNumEng) + "/" + global.convertNumToUnicode(MonNumEng) + "/०१";
            darta_dateTo.Text =  global.convertNumToUnicode(YearNumEng) + "/" + global.convertNumToUnicode(MonNumEng) + global.convertNumToUnicode(monLD);

            //also display in the combobox
            if (Convert.ToInt16(MonNumEng) >= 1 && Convert.ToInt16(MonNumEng) <= 3) //For Baishak Jestha And Ashar - it is from previous Fiscal Year
                YearNumEng = Convert.ToString(Convert.ToInt16(YearNumEng) - 1);

            //comboYear.SelectedValue = YearNumEng;
            //comboMonth.SelectedValue = MonNumEng;
        }

        private void getCurYear()
        {
            string fys = global.getSingleDataFromTable("SELECT GetNumberToUnicode(format_fiscal_yr(FY)) FROM setup_fy WHERE FY_ID=" + global.fyid);
            comboFY.Text = fys;
            comboFYChaumasic.Text = fys;
        }

        private void linkThisMon_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            getCurMon();
            radioYearly.Checked = true;
        }

        private void radioPeriodic_CheckedChanged(object sender, EventArgs e)
        {
            comboFY.Enabled = radioYearly.Checked;
            if (comboFY.Enabled && isReady)
                comboFY_SelectedIndexChanged(sender, e);
        }

        private void linkThisYear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            getCurYear();
        }

        private void linkTod_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            darta_date.Text = global.todaynepslash;
        }

        private void comboChaumasic_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isReady) return;
            string date1=global.convertNumToUnicode(comboFYChaumasic.SelectedValue.ToString());
            switch (comboChaumasic.SelectedIndex)
            {

                case 0:
                    darta_dateFrom.Text = date1 + "\\०४\\०१";
                    darta_dateTo.Text = date1 + "\\०७\\" + global.convertNumToUnicode(global.getNepMonthTotDays(comboFY.SelectedValue.ToString(),"07"));
                    break;

                case 1:
                    darta_dateFrom.Text = date1 + "\\०८\\०१";
                    darta_dateTo.Text = date1 + "\\११\\" + global.convertNumToUnicode(global.getNepMonthTotDays(comboFY.SelectedValue.ToString(),"11"));
                    break;

                case 2: //ashar is from another year 
                    darta_dateFrom.Text = date1 + "\\१२\\०१";
                    string date2 = (Convert.ToInt16(comboFYChaumasic.SelectedValue) + 1).ToString();
                    string monthdays2 = global.getNepMonthTotDays(Convert.ToString((Convert.ToInt16(comboFY.SelectedValue) + 1)), "03");
                    darta_dateTo.Text = global.convertNumToUnicode(date2) + "\\०३\\" + global.convertNumToUnicode(monthdays2);
                    break;

                default:
                    getCurYear();
                    break;
            }
        }

        private void linkLastMon_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            getPrevMon();
        }

        private void comboFY_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isReady) return;

            string YearNumEng = Convert.ToString(comboFY.SelectedValue);
            string yearId = YearNumEng;
            if (YearNumEng.Length == 4)
                yearId = (YearNumEng).Substring(2, 2); //removing 20 from 2077 and getting 77 only

            string fys = global.getSingleDataFromTable("SELECT GetNumberToUnicode(format_fiscal_yr(FY)) FROM setup_fy WHERE FY_ID=" + global.fyid);
            // comboYear.Text = fys;


            //Display Fiscal year in the date range box
            darta_dateFrom.Text = global.convertNumToUnicode(YearNumEng) + "/०४/०१"; //Shrawan 1 gatey

            //getting ashad last day of next year i.e yearnum + 1 (e.g. if year is 2077/78 then -- yearnum is 2077, so datefrom is 2077/04/01 but dateto is 2078/03/31)
            YearNumEng=Convert.ToString(Convert.ToInt16(YearNumEng)+1);
            string monLD = global.getSingleDataFromTable("SELECT NDNepM03 FROM setup_nepcalender WHERE Year=" + YearNumEng);
            darta_dateTo.Text = global.convertNumToUnicode(YearNumEng) + "/०३/" + global.convertNumToUnicode(monLD);

            //DISPLAY IN DATE RANGE BOX - By mistake it was done for Baishak to Chaitra
            //get total days in a month of a year (for last date of month)
            //darta_dateFrom.Text = global.convertNumToUnicode(YearNumEng) + "/०१/०१";
            //string monLD = global.getSingleDataFromTable("SELECT NDNepM12 FROM setup_nepcalender WHERE Year=" + YearNumEng);
            //darta_dateTo.Text = global.convertNumToUnicode(YearNumEng) + "/१२/" + global.convertNumToUnicode(monLD);
        }

        private void comboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isReady) return;

            string YearNum, MonthNum;
            YearNum = global.convertUnicodeToNum(comboYear.Text.ToString());
            MonthNum = comboMonth.SelectedValue.ToString();
            DisplayMonth(YearNum,MonthNum);
        }

        private void radioChaumasic_CheckedChanged(object sender, EventArgs e)
        {
            comboFYChaumasic.Enabled = radioChaumasic.Checked;
            comboChaumasic.Enabled = radioChaumasic.Checked;

            if (comboChaumasic.Enabled && isReady)
                comboChaumasic_SelectedIndexChanged(sender, e);
        }

        private void radioMonthly_CheckedChanged(object sender, EventArgs e)
        {
            comboYear.Enabled = radioMonthly.Checked;
            comboMonth.Enabled = radioMonthly.Checked;

            if (comboMonth.Enabled && isReady)
                comboMonth_SelectedIndexChanged(sender, e);
        }

        private void comboFYChaumasic_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isReady)
                comboChaumasic_SelectedIndexChanged(sender, e);
        }

        private void comboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isReady)
                comboMonth_SelectedIndexChanged(sender, e);
        }

        private void comboProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isReady)
            global.fillCombo(comboDistrict, "SELECT distcode, distunicodename FROM setup_district WHERE distzonecd=" + comboProvince.SelectedValue, "distunicodename", "distcode", "सबै जिल्ला");
        }

        private void btnIndustryDetailReport_Click(object sender, EventArgs e)
        {
            industrysearchbydate isss = new industrysearchbydate();
            isss.MdiParent = this.MdiParent;
            isss.getDatesFromOutside(darta_dateFrom.Text.ToString(), darta_dateTo.Text.ToString());
            isss.Show();
        }

        private void buttonDistrictWiseNum_Click(object sender, EventArgs e)
        {
            if (!radioYearly.Checked)
            {
                MessageBox.Show("कृपया कुनै एक आर्थिक वर्ष रोज्नुहोस !", "जिल्लागत उद्योग दर्ता", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                DistrictwiseMonthwiseIndustry(darta_dateFrom.Text.ToString(), darta_dateTo.Text.ToString(), true);
                if (dgvDistWise.Rows.Count > 0)
                {
                    string repTit = getReportTitle() + "जिल्लागत उद्योग दर्ता संख्या";
                    ExportDataGridToExcel(dgvDistWise, "districtwise_industry_count", repTit);
                }
                else
                {
                    MessageBox.Show("दिइएको अवधिमा कुनै पनि जिल्लाको उद्योग दर्ता भेटिएन !", "जिल्लागत उद्योग दर्ता", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void DistrictwiseMonthwiseIndustry(string dateFROM, string dateTO, Boolean showTotal = true)
        {
            if (sqlcon.con.State == ConnectionState.Closed)
                sqlcon.con.Open();

            MySqlDataAdapter qry = new MySqlDataAdapter("CALL getMonthwiseindustryReg(@datefrom,@dateto)", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@datefrom", dateFROM);
            qry.SelectCommand.Parameters.AddWithValue("@dateto", dateTO);
            DataTable tb = new DataTable();

            qry.Fill(tb);

            if (showTotal)
                tb.Columns.Add("जम्मा", typeof(string));

            for (int i = 0; i < tb.Rows.Count; i++)
            {
                int tot = 0, val;
                for (int j = 1; j < tb.Columns.Count; j++)
                {

                    string v = tb.Rows[i].ItemArray[j].ToString();

                    if (string.IsNullOrEmpty(v))
                        val = 0;
                    else
                        val = Convert.ToInt32(v);

                    if (showTotal)
                        tot += val;
                }

                if (showTotal)
                    tb.Rows[i]["जम्मा"] = tot.ToString();
            }

            dgvDistWise.DataSource = tb;
        }


        private void ExportDataGridToExcel(DataGridView dgv, string filename, string heading)
        {
            // creating Excel Application  
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            // creating new WorkBook within Excel application  
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            // creating new Excelsheet in workbook  
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            // see the excel sheet behind the program  
            app.Visible = true;
            // get the reference of first sheet. By default its name is Sheet1.  
            // store its reference to worksheet  
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            // changing the name of active sheet  
            worksheet.Name = "Districtwise_Industry_Reg";
            // storing header part in Excel  

            worksheet.Cells.Font.Name = "Kalimati";

            Excel.Range xlrg;

            xlrg = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 14]];
            xlrg.Merge();
            worksheet.Cells[1, 1] = heading;
            xlrg.Cells.Font.Size = "13";
            xlrg.Cells.Font.Bold = true;

            int fps = 3; //position of first row -- remains constant

            int nps = fps; //new position -- that changes below

            string fyy = comboFY.Text.ToString();

            xlrg = worksheet.Range[worksheet.Cells[nps, 2], worksheet.Cells[nps, 10]];
            xlrg.Merge();
            xlrg.Font.Bold = true;
            worksheet.Cells[nps, 2] = global.convertNumToUnicode((darta_dateFrom.Text.ToString()).Substring(0, 4));


            xlrg = worksheet.Range[worksheet.Cells[nps, 11], worksheet.Cells[nps, 13]];
            xlrg.Merge();
            xlrg.Font.Bold = true;
            worksheet.Cells[nps, 11] = global.convertNumToUnicode((darta_dateTo.Text.ToString()).Substring(0, 4));

            xlrg= worksheet.Range[worksheet.Cells[nps, 1], worksheet.Cells[nps+1, 13]];

            nps++;

            for (int i = 1; i < dgv.Columns.Count + 1; i++)
            {
                worksheet.Cells[nps, i] = dgv.Columns[i - 1].HeaderText;
            }

            xlrg = worksheet.Range[worksheet.Cells[fps, 1], worksheet.Cells[fps+1, 1]];
            xlrg.Merge();

            xlrg = worksheet.Range[worksheet.Cells[fps, dgv.Columns.Count], worksheet.Cells[fps + 1, dgv.Columns.Count]];
            xlrg.Merge();


            //making the header cells bold, center and backcolor
            xlrg = worksheet.Range[worksheet.Cells[fps, 1], worksheet.Cells[nps, dgv.Columns.Count]];
            xlrg.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            xlrg.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            xlrg.Font.Bold = true;
            xlrg.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gainsboro);

            xlrg = worksheet.Range[worksheet.Cells[nps, 1], worksheet.Cells[nps + 1, 13]];

            nps++;
            // storing Each row and column value to excel sheet  
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                for (int j = 0; j < dgv.Columns.Count; j++)
                {
                    worksheet.Cells[i + nps, j + 1] = global.convertNumToUnicode(dgv.Rows[i].Cells[j].Value.ToString());
                }
            }
            //FORMATTING THE EXCEL FILE

            //border
            xlrg = worksheet.Range[worksheet.Cells[fps, 1], worksheet.Cells[fps + 2 + dgv.Rows.Count, dgv.Columns.Count]];
            //xlrg = xlws.Range[xlws.Cells[fps - 1, 1], xlws.Cells[nps + m + j - 2, tb.Columns.Count + otb.Columns.Count - 2]];
            xlrg.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            xlrg.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

            //worksheet.Columns.AutoFit();
            worksheet.Rows.AutoFit();

            //string filePath = "";

            //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //saveFileDialog1.InitialDirectory = @Environment.SpecialFolder.MyDocuments.ToString();
            //saveFileDialog1.Title = "Save text Files";
            //saveFileDialog1.CheckFileExists = false;
            ////saveFileDialog1.CheckPathExists = false;    
            //saveFileDialog1.DefaultExt = ".xlsx";
            //saveFileDialog1.FileName = "Industry_Report";
            //saveFileDialog1.Filter = "Excel Workbook (*.xlsx)|*.xlsx|Excel 97-2003 Workbook (*.xls)|*.xls|CSV (*.csv)|*.csv|All files (*.*)|*.*";
            //saveFileDialog1.FilterIndex = 1;
            //saveFileDialog1.RestoreDirectory = true;
            //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    filePath = saveFileDialog1.FileName;
            //    workbook.SaveAs(filePath);

            //    //workbook.Close(Type.Missing, filePath, Type.Missing);
            //    //workbook = null;
            //    //app.Quit();
            //    saveFileDialog1.Dispose();


            //    //DialogResult dlrg = MessageBox.Show("प्रतिवेदन सफलतापुर्वक MS Excel मा सेभ गरियो । \n\n के तपाईँ प्रतिवेदन अहिले हेर्न चाहनुहुन्छ?", "प्रतिवेदन", MessageBoxButtons.YesNo, MessageBoxIcon.None);

            //    //if (dlrg == DialogResult.Yes)
            //    //{
            //    //    FileInfo fi = new FileInfo(filePath);
            //    //    if (fi.Exists)
            //    //    {
            //    //        System.Diagnostics.Process.Start(@filePath); //open excel file
            //    //    }
            //    //    else
            //    //    {
            //    //        MessageBox.Show("प्रतिवेदन फाइल खोल्न सकिएन ! कृपया सम्बन्धित फोल्डरमा गई आफै खोल्नुहोला ।", "प्रतिवेदन", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    //    }
            //    //}

            //}

            // save the application  
            //workbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            // Exit from the application  
            workbook = null;
            worksheet = null;
           // workbook = null;
        }
    }
}
