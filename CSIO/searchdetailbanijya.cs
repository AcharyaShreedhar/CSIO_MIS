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
    public partial class searchdetailbanijya : Form
    {
        public searchdetailbanijya()
        {
            InitializeComponent();
        }
        public int id;
       public int idt;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
        public void DisplayDatafirm()
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            } 
            ////SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

         //   con.Open();
           // MessageBox.Show(id.ToString());

            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     banijyaform.firmid, GetNumberToUnicode(banijyaform.firmregno) AS 'र.नं', banijyaform.firmnepname AS 'फर्मको नाम', setup_district.distunicodename AS 'जिल्ला',           setup_vdc.vdcunicodename AS 'गा.पा.। न.पा.', GetNumberToUnicode(banijyaform.firmward) AS 'वार्ड', banijyaform.tole AS 'टोल',setup_subcategory.subcategory_unicodename AS 'प्रकार',  GetNumberToUnicode(banijyaform.regdat) AS 'दर्ता मिति',  GetNumberToUnicode(banijyaform.renewdate) AS 'नविकरण', setup_subcategory_1.subcategory_unicodename AS 'उद्देश्य', banijyaform.karobar AS 'कारोवार बस्तुहरु', GetNumberToUnicode(banijyaform.revenue) AS 'पुँजी',  GetNumberToUnicode(banijyaform.tax) AS 'राजश्व',banijyaform.branch AS 'शाखा',  setup_subcategory_2.subcategory_unicodename AS 'कारोवार', GetNumberToUnicode(banijyaform.thelino) AS 'ठेली न‌', GetNumberToUnicode(banijyaform.fileno) AS 'फाइल न‌', GetNumberToUnicode(banijyaform.fiscalyear) AS 'आ.व', banijyaform.comment AS 'कैफियत' FROM         banijyaform INNER JOIN    setup_subcategory ON banijyaform.firmtype = setup_subcategory.subcategory_id INNER JOIN     setup_district ON banijyaform.firmdist = setup_district.distcode INNER JOIN    setup_vdc ON banijyaform.firmvdc = setup_vdc.VDC_SID INNER JOIN  setup_subcategory AS setup_subcategory_1 ON banijyaform.firmscope = setup_subcategory_1.subcategory_id  INNER JOIN   setup_subcategory AS setup_subcategory_2 ON banijyaform.karobartype = setup_subcategory_2.subcategory_id where banijyaform.firmid=@darta", sqlcon.con);
            //SqlCommand qr = new SqlCommand("SELECT     banijyaform.firmid, banijyaform.firmregno AS 'र.नं', banijyaform.firmnepname AS 'फर्मको नाम', setup_district.distnepname AS 'जिल्ला',           setup_vdc.Vdcunicodename AS 'गा.पा.। न.पा.', banijyaform.firmward AS 'वार्ड', banijyaform.tole AS 'टोल',setup_subcategory.subcategory_unicodename AS 'प्रकार',  banijyaform.regdat AS 'दर्ता मिति',  banijyaform.renewdate AS 'नविकरण', setup_subcategory_1.subcategory_unicodename AS 'उद्देश्य', banijyaform.karobar AS 'कारोवार', banijyaform.revenue AS 'पुँजी',  banijyaform.tax AS 'राजश्व',banijyaform.branch AS 'शाखा', banijyaform.comment AS 'कैफियत' FROM         banijyaform INNER JOIN    setup_subcategory ON banijyaform.firmtype = setup_subcategory.subcategory_id INNER JOIN     setup_district ON banijyaform.firmdist = setup_district.distcode INNER JOIN    setup_vdc ON banijyaform.firmvdc = setup_vdc.VDC_SID INNER JOIN  setup_subcategory AS setup_subcategory_1 ON banijyaform.firmscope = setup_subcategory_1.subcategory_id where firmregno=@firmreg");
            qry.SelectCommand.Parameters.AddWithValue("@darta", idt.ToString());
          
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = tb;
            dataGridView1.Columns[0].Visible = false;
            sqlcon.con.Close();
            //////////////////////////////
            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.passwordname + "' ");

            //con.Open();

            //MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     banijyaform.firmid,GetNumberToUnicode(banijyaform.firmregno) AS 'र.नं', banijyaform.firmnepname AS 'फर्मको नाम', setup_district.distunicodename AS 'जिल्ला',           setup_vdc.vdcunicodename AS 'गा.पा.। न.पा.', GetNumberToUnicode(banijyaform.firmward) AS 'वार्ड', banijyaform.tole AS 'टोल',setup_subcategory.subcategory_unicodename AS 'प्रकार',  GetNumberToUnicode(banijyaform.regdat) AS 'दर्ता मिति',  GetNumberToUnicode(banijyaform.renewdate) AS 'नविकरण', setup_subcategory_1.subcategory_unicodename AS 'उद्देश्य', banijyaform.karobar AS 'कारोवार', GetNumberToUnicode(banijyaform.revenue) AS 'पुँजी',  GetNumberToUnicode(banijyaform.tax) AS 'राजश्व',banijyaform.branch AS 'शाखा', banijyaform.comment AS 'कैफियत',  FROM         banijyaform INNER JOIN    setup_subcategory ON banijyaform.firmtype = setup_subcategory.subcategory_id INNER JOIN     setup_district ON banijyaform.firmdist = setup_district.distcode INNER JOIN    setup_vdc ON banijyaform.firmvdc = setup_vdc.VDC_SID INNER JOIN  setup_subcategory AS setup_subcategory_1 ON banijyaform.firmscope = setup_subcategory_1.subcategory_id where  firmid= @darta ", con);
            //qry.SelectCommand.Parameters.AddWithValue("@darta", ido.ToString());
            //DataTable tb = new DataTable();
            //qry.Fill(tb);
            //dataGridView1.DataSource = tb;
            //dataGridView1.Columns[0].Visible = false;
            //sqlcon.con.Close();
        }
        public void DisplayData()
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            } 
            // //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

           // con.Open();

            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT  banijyaform.firmid,GetNumberToUnicode(banijyaform.firmregno) AS 'र.नं', banijyaform.firmnepname AS 'फर्मको नाम', setup_district.distunicodename AS 'जिल्ला', setup_vdc.vdcunicodename AS 'गा.पा.। न.पा.', GetNumberToUnicode(banijyaform.firmward) AS 'वार्ड', banijyaform.tole AS 'टोल',setup_subcategory.subcategory_unicodename AS 'प्रकार',  GetNumberToUnicode(banijyaform.regdat) AS 'दर्ता मिति',  GetNumberToUnicode(banijyaform.renewdate) AS 'नविकरण', setup_subcategory_1.subcategory_unicodename AS 'उद्देश्य', banijyaform.karobar AS 'कारोवार', GetNumberToUnicode(banijyaform.revenue) AS 'पुँजी',  GetNumberToUnicode(banijyaform.tax) AS 'राजश्व',banijyaform.branch AS 'शाखा', banijyaform.comment AS 'कैफियत'  FROM         banijyaform INNER JOIN    setup_subcategory ON banijyaform.firmtype = setup_subcategory.subcategory_id INNER JOIN     setup_district ON banijyaform.firmdist = setup_district.distcode INNER JOIN    setup_vdc ON banijyaform.firmvdc = setup_vdc.VDC_SID INNER JOIN  setup_subcategory AS setup_subcategory_1 ON banijyaform.firmscope = setup_subcategory_1.subcategory_id WHERE firmnepname LIKE @firmn", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@firmn", textBox1.Text+'%');
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView1.DataSource = tb;
            dataGridView1.Columns[0].Visible = false;
            sqlcon.con.Close();
        }
        public void DisplayDatadartaname()
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            } 

            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     banijyaform.firmid,GetNumberToUnicode(banijyaform.firmregno) AS 'र.नं', banijyaform.firmnepname AS 'फर्मको नाम', setup_district.distunicodename AS 'जिल्ला',           setup_vdc.vdcunicodename AS 'गा.पा.। न.पा.', GetNumberToUnicode(banijyaform.firmward) AS 'वार्ड', banijyaform.tole AS 'टोल',setup_subcategory.subcategory_unicodename AS 'प्रकार',  GetNumberToUnicode(banijyaform.regdat) AS 'दर्ता मिति',  GetNumberToUnicode(banijyaform.renewdate) AS 'नविकरण', setup_subcategory_1.subcategory_unicodename AS 'उद्देश्य', banijyaform.karobar AS 'कारोवार', GetNumberToUnicode(banijyaform.revenue) AS 'पुँजी',  GetNumberToUnicode(banijyaform.tax) AS 'राजश्व',banijyaform.branch AS 'शाखा', banijyaform.comment AS 'कैफियत'  FROM         banijyaform INNER JOIN    setup_subcategory ON banijyaform.firmtype = setup_subcategory.subcategory_id INNER JOIN     setup_district ON banijyaform.firmdist = setup_district.distcode INNER JOIN    setup_vdc ON banijyaform.firmvdc = setup_vdc.VDC_SID INNER JOIN  setup_subcategory AS setup_subcategory_1 ON banijyaform.firmscope = setup_subcategory_1.subcategory_id where  (firmregno=GetNumberToUnicode(@darta) or firmregno=GetUnicodeToNumber(@darta)) or firmnepname LIKE @firmname", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@darta", darta_txt.Text);
            qry.SelectCommand.Parameters.AddWithValue("@firmname", textBox1.Text+'%');
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView1.DataSource = tb;
            dataGridView1.Columns[0].Visible = false;
            sqlcon.con.Close();
        }

        public void DisplayDatadarta()
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            } 


            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     banijyaform.firmid, GetNumberToUnicode(banijyaform.firmregno) AS 'र.नं', banijyaform.firmnepname AS 'फर्मको नाम', setup_district.distunicodename AS 'जिल्ला',           setup_vdc.vdcunicodename AS 'गा.पा.। न.पा.', GetNumberToUnicode(banijyaform.firmward) AS 'वार्ड', banijyaform.tole AS 'टोल',setup_subcategory.subcategory_unicodename AS 'प्रकार',  GetNumberToUnicode(banijyaform.regdat) AS 'दर्ता मिति',  GetNumberToUnicode(banijyaform.renewdate) AS 'नविकरण', setup_subcategory_1.subcategory_unicodename AS 'उद्देश्य', banijyaform.karobar AS 'कारोवार बस्तुहरु', GetNumberToUnicode(banijyaform.revenue) AS 'पुँजी',  GetNumberToUnicode(banijyaform.tax) AS 'राजश्व',banijyaform.branch AS 'शाखा',  setup_subcategory_2.subcategory_unicodename AS 'कारोवार', GetNumberToUnicode(banijyaform.thelino) AS 'ठेली न‌', GetNumberToUnicode(banijyaform.fileno) AS 'फाइल न‌', GetNumberToUnicode(banijyaform.fiscalyear) AS 'आ.व', banijyaform.comment AS 'कैफियत' FROM         banijyaform INNER JOIN    setup_subcategory ON banijyaform.firmtype = setup_subcategory.subcategory_id INNER JOIN     setup_district ON banijyaform.firmdist = setup_district.distcode INNER JOIN    setup_vdc ON banijyaform.firmvdc = setup_vdc.VDC_SID INNER JOIN  setup_subcategory AS setup_subcategory_1 ON banijyaform.firmscope = setup_subcategory_1.subcategory_id  INNER JOIN   setup_subcategory AS setup_subcategory_2 ON banijyaform.karobartype = setup_subcategory_2.subcategory_id where (firmregno=GetUnicodeToNumber(@darta) or firmregno=GetNumberToUnicode(@darta))", sqlcon.con);
            //SqlCommand qr = new SqlCommand("SELECT     banijyaform.firmid, banijyaform.firmregno AS 'र.नं', banijyaform.firmnepname AS 'फर्मको नाम', setup_district.distnepname AS 'जिल्ला',           setup_vdc.Vdcunicodename AS 'गा.पा.। न.पा.', banijyaform.firmward AS 'वार्ड', banijyaform.tole AS 'टोल',setup_subcategory.subcategory_unicodename AS 'प्रकार',  banijyaform.regdat AS 'दर्ता मिति',  banijyaform.renewdate AS 'नविकरण', setup_subcategory_1.subcategory_unicodename AS 'उद्देश्य', banijyaform.karobar AS 'कारोवार', banijyaform.revenue AS 'पुँजी',  banijyaform.tax AS 'राजश्व',banijyaform.branch AS 'शाखा', banijyaform.comment AS 'कैफियत' FROM         banijyaform INNER JOIN    setup_subcategory ON banijyaform.firmtype = setup_subcategory.subcategory_id INNER JOIN     setup_district ON banijyaform.firmdist = setup_district.distcode INNER JOIN    setup_vdc ON banijyaform.firmvdc = setup_vdc.VDC_SID INNER JOIN  setup_subcategory AS setup_subcategory_1 ON banijyaform.firmscope = setup_subcategory_1.subcategory_id where firmregno=@firmreg");
            qry.SelectCommand.Parameters.AddWithValue("@darta", darta_txt.Text+'%');
             DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView1.DataSource = tb;
            dataGridView1.Columns[0].Visible = false;
            sqlcon.con.Close();
        }

        public void DisplayDataowner()
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            } 

            //con.Open();
            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT    owner.firmid, owner.ownerfname AS 'नाम', owner.ownerlname AS 'थर', setup_citizenissueoff.citizen_officeunicodename AS 'नागरिकता जारी गर्ने कार्यालय', setup_district.distunicodename AS 'जिल्ला',                       GetNumberToUnicode(owner.citzdate) AS 'जारी मिति', GetNumberToUnicode(owner.citznum) AS 'ना.प्र.नं.', setup_district_1.distunicodename AS 'जिल्ला', setup_vdc.vdcunicodename AS 'गा.वि.स । न.पा.', owner.ownertole AS 'टोल',                       setup_subcategory_1.subcategory_unicodename AS 'बाबु । पति', owner.ownergname AS 'बावु । पति नाम', setup_subcategory.subcategory_unicodename AS 'बाजे । ससुरा',                       owner.ownerfgname AS 'बाजे नाम', setup_subcategory_2.subcategory_unicodename AS 'लिंग', contact As 'सम्पर्क नं', email As 'ईमेल', owner.comment AS 'कैफियत' FROM         owner INNER JOIN            setup_district ON owner.cddist = setup_district.distcode INNER JOIN          setup_citizenissueoff ON owner.ccio = setup_citizenissueoff.citizen_officeid INNER JOIN            setup_district AS setup_district_1 ON owner.ownerdistcode = setup_district_1.distcode INNER JOIN         setup_vdc ON owner.ownervdccode = setup_vdc.VDC_SID INNER JOIN        setup_subcategory AS setup_subcategory_1 ON owner.ownergrel = setup_subcategory_1.subcategory_id INNER JOIN           setup_subcategory ON owner.ownerfgrel = setup_subcategory.subcategory_id INNER JOIN      setup_subcategory AS setup_subcategory_2 ON owner.gender = setup_subcategory_2.subcategory_id WHERE     (owner.firmid = @darta)", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@darta", id.ToString());
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView2.DataSource = tb;
            dataGridView2.Columns[0].Visible = false;

            sqlcon.con.Close();

        }
        public void owner()
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            } 
            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

           // con.Open();
            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     owner.firmid, owner.ownerfname AS 'नाम', owner.ownerlname AS 'थर', setup_citizenissueoff.citizen_officeunicodename AS 'नागरिकता जारी गर्ने कार्यालय', setup_district.distunicodename AS 'जिल्ला',                       GetNumberToUnicode(owner.citzdate) AS 'जारी मिति', GetNumberToUnicode(owner.citznum) AS 'ना.प्र.नं.', setup_district_1.distunicodename AS 'जिल्ला', setup_vdc.vdcunicodename AS 'गा.वि.स । न.पा.', owner.ownertole AS 'टोल',                       setup_subcategory_1.subcategory_unicodename AS 'बाबु । पति', owner.ownergname AS 'बावु । पति नाम', setup_subcategory.subcategory_unicodename AS 'बाजे । ससुरा',                       owner.ownerfgname AS 'बाजे नाम', setup_subcategory_2.subcategory_unicodename AS 'लिंग', contact As 'सम्पर्क नं', email As 'ईमेल', owner.comment AS 'कैफियत' FROM         owner INNER JOIN            setup_district ON owner.cddist = setup_district.distcode INNER JOIN          setup_citizenissueoff ON owner.ccio = setup_citizenissueoff.citizen_officeid INNER JOIN            setup_district AS setup_district_1 ON owner.ownerdistcode = setup_district_1.distcode INNER JOIN         setup_vdc ON owner.ownervdccode = setup_vdc.VDC_SID INNER JOIN        setup_subcategory AS setup_subcategory_1 ON owner.ownergrel = setup_subcategory_1.subcategory_id INNER JOIN           setup_subcategory ON owner.ownerfgrel = setup_subcategory.subcategory_id INNER JOIN      setup_subcategory AS setup_subcategory_2 ON owner.gender = setup_subcategory_2.subcategory_id WHERE   (owner.ownerfname LIKE @fname) AND (owner.ownerlname LIKE @lname) AND (cddist = @dist) AND  (GetNumberToUnicode(owner.citzdate) LIKE @cdate) AND (GetNumberToUnicode(owner.citznum) Like @cnumber or (owner.citznum)  LIKE @cnumber)", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@fname", fname.Text+'%');
            qry.SelectCommand.Parameters.AddWithValue("@lname", lname.Text + '%');
            qry.SelectCommand.Parameters.AddWithValue("@dist", district_combo.SelectedValue);
            qry.SelectCommand.Parameters.AddWithValue("@cdate", darta_date.Text + '%');
            qry.SelectCommand.Parameters.AddWithValue("@cnumber", cnum.Text + '%');
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView2.DataSource = tb;
            dataGridView2.Columns[0].Visible = false;

            sqlcon.con.Close();

        }
        public void ownerdist()
        {
            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            } 
            //con.Open();
            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT    owner.firmid, owner.ownerfname AS 'नाम', owner.ownerlname AS 'थर', setup_citizenissueoff.citizen_officeunicodename AS 'नागरिकता जारी गर्ने कार्यालय', setup_district.distunicodename AS 'जिल्ला',                       GetNumberToUnicode(owner.citzdate) AS 'जारी मिति', GetNumberToUnicode(owner.citznum) AS 'ना.प्र.नं.', setup_district_1.distunicodename AS 'जिल्ला', setup_vdc.vdcunicodename AS 'गा.वि.स । न.पा.', owner.ownertole AS 'टोल',                       setup_subcategory_1.subcategory_unicodename AS 'बाबु । पति', owner.ownergname AS 'बावु । पति नाम', setup_subcategory.subcategory_unicodename AS 'बाजे । ससुरा',                       owner.ownerfgname AS 'बाजे नाम', setup_subcategory_2.subcategory_unicodename AS 'लिंग', contact As 'सम्पर्क नं', email As 'ईमेल', owner.comment AS 'कैफियत' FROM         owner INNER JOIN            setup_district ON owner.cddist = setup_district.distcode INNER JOIN          setup_citizenissueoff ON owner.ccio = setup_citizenissueoff.citizen_officeid INNER JOIN            setup_district AS setup_district_1 ON owner.ownerdistcode = setup_district_1.distcode INNER JOIN         setup_vdc ON owner.ownervdccode = setup_vdc.VDC_SID INNER JOIN        setup_subcategory AS setup_subcategory_1 ON owner.ownergrel = setup_subcategory_1.subcategory_id INNER JOIN           setup_subcategory ON owner.ownerfgrel = setup_subcategory.subcategory_id INNER JOIN      setup_subcategory AS setup_subcategory_2 ON owner.gender = setup_subcategory_2.subcategory_id WHERE   (owner.ownerfname LIKE @fname) AND (owner.ownerlname LIKE @lname) AND (cddist = @dist) AND  (GetNumberToUnicode(owner.citzdate) LIKE @cdate) AND (GetNumberToUnicode(owner.citznum) Like @cnumber or (owner.citznum  LIKE @cnumber))", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@fname", fname.Text + '%');
            qry.SelectCommand.Parameters.AddWithValue("@lname", lname.Text + '%');
            qry.SelectCommand.Parameters.AddWithValue("@dist", district_combo.SelectedValue);
            //  qry.SelectCommand.Parameters.AddWithValue("@cdate", darta_date.Text + '%');
            qry.SelectCommand.Parameters.AddWithValue("@cnumber", cnum.Text );
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView2.DataSource = tb;
            dataGridView2.Columns[0].Visible = false;
            sqlcon.con.Close();

        }
        public void ownerdartadist()
        {
            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            } 
            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT    owner.firmid, owner.ownerfname AS 'नाम', owner.ownerlname AS 'थर', setup_citizenissueoff.citizen_officeunicodename AS 'नागरिकता जारी गर्ने कार्यालय', setup_district.distunicodename AS 'जिल्ला',                       GetNumberToUnicode(owner.citzdate) AS 'जारी मिति', GetNumberToUnicode(owner.citznum) AS 'ना.प्र.नं.', setup_district_1.distunicodename AS 'जिल्ला', setup_vdc.vdcunicodename AS 'गा.वि.स । न.पा.', owner.ownertole AS 'टोल',                       setup_subcategory_1.subcategory_unicodename AS 'बाबु । पति', owner.ownergname AS 'बावु । पति नाम', setup_subcategory.subcategory_unicodename AS 'बाजे । ससुरा',                       owner.ownerfgname AS 'बाजे नाम', setup_subcategory_2.subcategory_unicodename AS 'लिंग', contact As 'सम्पर्क नं', email As 'ईमेल', owner.comment AS 'कैफियत' FROM         owner INNER JOIN            setup_district ON owner.cddist = setup_district.distcode INNER JOIN          setup_citizenissueoff ON owner.ccio = setup_citizenissueoff.citizen_officeid INNER JOIN            setup_district AS setup_district_1 ON owner.ownerdistcode = setup_district_1.distcode INNER JOIN         setup_vdc ON owner.ownervdccode = setup_vdc.VDC_SID INNER JOIN        setup_subcategory AS setup_subcategory_1 ON owner.ownergrel = setup_subcategory_1.subcategory_id INNER JOIN           setup_subcategory ON owner.ownerfgrel = setup_subcategory.subcategory_id INNER JOIN      setup_subcategory AS setup_subcategory_2 ON owner.gender = setup_subcategory_2.subcategory_id WHERE   (owner.ownerfname LIKE @fname) AND (owner.ownerlname LIKE @lname)  AND   (GetNumberToUnicode(owner.citznum) Like @cnumber or (owner.citznum)  LIKE @cnumber) ", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@fname", fname.Text + '%');
            qry.SelectCommand.Parameters.AddWithValue("@lname", lname.Text + '%');
            // qry.SelectCommand.Parameters.AddWithValue("@dist", district_combo.SelectedValue);
            //qry.SelectCommand.Parameters.AddWithValue("@cdate", darta_date.Text + '%');
            qry.SelectCommand.Parameters.AddWithValue("@cnumber", cnum.Text + '%');
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView2.DataSource = tb;
            dataGridView2.Columns[0].Visible = false;

            sqlcon.con.Close();

        }
        public void ownerdarta()
        {
            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            } 
           // con.Open();
            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT    owner.firmid, owner.ownerfname AS 'नाम', owner.ownerlname AS 'थर', setup_citizenissueoff.citizen_officeunicodename AS 'नागरिकता जारी गर्ने कार्यालय', setup_district.distunicodename AS 'जिल्ला',                       GetNumberToUnicode(owner.citzdate) AS 'जारी मिति', GetNumberToUnicode(owner.citznum) AS 'ना.प्र.नं.', setup_district_1.distunicodename AS 'जिल्ला', setup_vdc.vdcunicodename AS 'गा.वि.स । न.पा.', owner.ownertole AS 'टोल',                       setup_subcategory_1.subcategory_unicodename AS 'बाबु । पति', owner.ownergname AS 'बावु । पति नाम', setup_subcategory.subcategory_unicodename AS 'बाजे । ससुरा',                       owner.ownerfgname AS 'बाजे नाम', setup_subcategory_2.subcategory_unicodename AS 'लिंग', contact As 'सम्पर्क नं', email As 'ईमेल', owner.comment AS 'कैफियत' FROM         owner INNER JOIN            setup_district ON owner.cddist = setup_district.distcode INNER JOIN          setup_citizenissueoff ON owner.ccio = setup_citizenissueoff.citizen_officeid INNER JOIN            setup_district AS setup_district_1 ON owner.ownerdistcode = setup_district_1.distcode INNER JOIN         setup_vdc ON owner.ownervdccode = setup_vdc.VDC_SID INNER JOIN        setup_subcategory AS setup_subcategory_1 ON owner.ownergrel = setup_subcategory_1.subcategory_id INNER JOIN           setup_subcategory ON owner.ownerfgrel = setup_subcategory.subcategory_id INNER JOIN      setup_subcategory AS setup_subcategory_2 ON owner.gender = setup_subcategory_2.subcategory_id WHERE   (owner.ownerfname LIKE @fname) AND (owner.ownerlname LIKE @lname)  AND   (GetNumberToUnicode(owner.citznum) Like @cnumber or (owner.citznum)  LIKE @cnumber)", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@fname", fname.Text + '%');
            qry.SelectCommand.Parameters.AddWithValue("@lname", lname.Text + '%');
           //qry.SelectCommand.Parameters.AddWithValue("@dist", district_combo.SelectedValue);
           qry.SelectCommand.Parameters.AddWithValue("@cdate", darta_date.Text + '%');
            qry.SelectCommand.Parameters.AddWithValue("@cnumber", cnum.Text + '%');
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView2.DataSource = tb;
            dataGridView2.Columns[0].Visible = false;
            sqlcon.con.Close();

        }
        private void button1_Click(object sender, EventArgs e)
        {
           // !string.IsNullOrWhiteSpace(textbox.text)
            if ((!string.IsNullOrWhiteSpace(textBox1.Text)) & (!string.IsNullOrWhiteSpace(darta_txt.Text)))
            {
                DisplayDatadartaname();
            }
             else if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                DisplayData();
            }
            else if (!string.IsNullOrWhiteSpace(darta_txt.Text))
            {
               DisplayDatadarta();
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            button2.Enabled = true;
            button3.Enabled = true;
            int i;

            i = dataGridView1.CurrentRow.Index;
            id = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
            DisplayDataowner();
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
        private void searchdetailbanijya_Load(object sender, EventArgs e)
        {
            
            button2.Enabled = false;
            button3.Enabled = false;
            district_combo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            district_combo.AutoCompleteSource = AutoCompleteSource.CustomSource;

            AutoCompleteStringCollection combData = new AutoCompleteStringCollection();
            FillDropDownList(combData);
            district_combo.AutoCompleteCustomSource = combData;
            district_combo.SelectedValue = -1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int i;

                i = dataGridView1.CurrentRow.Index;

                string dartanum = dataGridView1.Rows[i].Cells[0].Value.ToString();
                //MessageBox.Show(dartanum.ToString());
                reportviewer bd = new reportviewer();
                bd.MdiParent = this.MdiParent;
                
               // bd.getdataa(dartanum);


                bd.Show();
            }
            catch(Exception fes){
                MessageBox.Show(fes.ToString());
            }
                
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i;

            i = dataGridView1.CurrentRow.Index;

            string dartan = dataGridView1.Rows[i].Cells[0].Value.ToString();
            string firmtype = dataGridView1.Rows[i].Cells[14].Value.ToString();
            //MessageBox.Show(dartanum.ToString());
            banijyaletterview lett = new banijyaletterview();
            lett.MdiParent = this.MdiParent;

            lett.getdata(dartan,firmtype);
            lett.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if ((darta_date.MaskFull) & (!string.IsNullOrWhiteSpace(district_combo.Text)))
            {
                owner();
            }
            else if ((!darta_date.MaskFull) & (string.IsNullOrWhiteSpace(district_combo.Text)))
            {
                ownerdartadist();
            }
            else if (!string.IsNullOrWhiteSpace(district_combo.Text))
            {
                ownerdist();
            }
            else if (darta_date.MaskFull)
            {
                ownerdarta();
            }
           
        }

        private void dataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            button2.Enabled = true;
            button3.Enabled = true;
            int i;

            i = dataGridView2.CurrentRow.Index;
            idt = int.Parse(dataGridView2.Rows[i].Cells[0].Value.ToString());
            DisplayDatafirm();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
