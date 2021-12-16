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
    public partial class searchindustrydetailbyname : Form
    {
        public searchindustrydetailbyname()
        {
            InitializeComponent();
        }
        public static string idt;
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        public void DisplayDatadartaname()
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }

            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     industryreg.industryid, GetNumberToUnicode(industryreg.industryregno) AS 'दर्ता नं', industryreg.industrynepname AS 'उद्योगको नाम', setup_district.distunicodename AS 'जिल्ला',setup_vdc.vdcunicodename As 'गा.पा.। न.पा.',GetNumberToUnicode(industryreg.industryward) As 'वार्ड', industryreg.tole AS 'टोल', industryreg.branch AS 'शाखा',   setup_subcategory.subcategory_unicodename AS 'स्तर', setup_subcategory_2.subcategory_unicodename AS 'कानूनी स्वरूप',   setup_subcategory_1.subcategory_unicodename AS 'वर्ग', GetNumberToUnicode(industryreg.regdat) AS 'दर्ता मिति',  GetNumberToUnicode(industryreg.renewdate) AS 'नविकरण मिति', industryreg.karobar AS 'उद्योगको उदेश्य', GetNumberToUnicode(industryreg.yearlyturnover) AS 'बार्षिक उत्पादन',  GetNumberToUnicode(industryreg.electricpower) AS 'विधुत शक्ती', GetNumberToUnicode(industryreg.statcapital) AS 'स्थिर पुँजी', GetNumberToUnicode(industryreg.varcapital) AS 'चालु पुँजी', GetNumberToUnicode(industryreg.femaleworker) As 'महिला कामदार', GetNumberToUnicode(industryreg.maleworker) As 'पुरूष कामदार', GetNumberToUnicode(industryreg.tax) As 'राजश्व', industryreg.comment AS 'कैफियत', GetNumberToUnicode(industryreg.techworker) As 'प्राविधिक', GetNumberToUnicode(industryreg.nontechworker) As 'अप्राविधिक', industryreg.rawmaterial As 'कच्चा पदार्थ', GetNumberToUnicode(industryreg.fileno) As 'फाइल नं', GetNumberToUnicode(industryreg.thelino) As 'ठेली', GetNumberToUnicode(industryreg.fiscalyear) As 'आ.व'  FROM         setup_subcategory INNER JOIN          industryreg INNER JOIN      setup_district ON industryreg.industrydist = setup_district.distcode INNER JOIN       setup_vdc ON industryreg.industryvdc = setup_vdc.VDC_SID ON setup_subcategory.subcategory_id = industryreg.industryscale INNER JOIN      setup_subcategory AS setup_subcategory_2 ON industryreg.industrytype = setup_subcategory_2.subcategory_id INNER JOIN       setup_subcategory AS setup_subcategory_1 ON industryreg.industrycat = setup_subcategory_1.subcategory_id where  (GetNumberToUnicode(industryregno)=GetNumberToUnicode(@darta) or industryregno=GetUnicodeToNumber(@darta)) or industrynepname LIKE @firmname", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@darta", darta_txt.Text);
            qry.SelectCommand.Parameters.AddWithValue("@firmname", textBox1.Text + '%');
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView1.DataSource = tb;
            dataGridView1.Columns[0].Visible = false;
            sqlcon.con.Close();
        }

        public void DisplayDatafirm()
        {
            try
            {
                MessageBox.Show(idt.ToString());
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                /////////////////
                MySqlDataAdapter qry = new MySqlDataAdapter("SELECT industryreg.industryid, GetNumberToUnicode(industryreg.industryregno) AS 'दर्ता नं', industryreg.industrynepname AS 'उद्योगको नाम', setup_district.distunicodename AS 'जिल्ला',setup_vdc.vdcunicodename As 'गा.पा।न.पा.',GetNumberToUnicode(industryreg.industryward) As 'वार्ड', industryreg.tole AS 'टोल', industryreg.branch AS 'शाखा',   setup_subcategory.subcategory_unicodename AS 'स्तर', setup_subcategory_2.subcategory_unicodename AS 'कानूनी स्वरूप',   setup_subcategory_1.subcategory_unicodename AS 'वर्ग', GetNumberToUnicode(industryreg.regdat) AS 'दर्ता मिति',  GetNumberToUnicode(industryreg.renewdate) AS 'नविकरण मिति', industryreg.karobar AS 'उद्योगको उद्देश्य', GetNumberToUnicode(industryreg.yearlyturnover) AS 'बार्षिक उत्पादन',  GetNumberToUnicode(industryreg.electricpower) AS 'विधुत शक्ती',GetNumberToUnicode(industryreg.statcapital) AS 'स्थिर पुँजी', GetNumberToUnicode(industryreg.varcapital) AS 'चालु पुँजी', GetNumberToUnicode(industryreg.femaleworker) As 'महिला कामदार', GetNumberToUnicode(industryreg.maleworker) As 'पुरूष कामदार', GetNumberToUnicode(industryreg.tax) As 'राजश्व', industryreg.comment AS 'कैफियत', GetNumberToUnicode(industryreg.techworker) As 'प्राविधिक', GetNumberToUnicode(industryreg.nontechworker) As 'अप्राविधिक', industryreg.rawmaterial As 'कच्चा पदार्थ', GetNumberToUnicode(industryreg.fileno) As 'फाइल नं', GetNumberToUnicode(industryreg.thelino) As 'ठेली', GetNumberToUnicode(industryreg.fiscalyear) As 'आ.व' ,GetNumberToUnicode(industryreg.adhikrit_capital) As 'अधिकृत पुँजी', GetNumberToUnicode(industryreg.jari_capital) As 'जारी पुँजी',ain As 'औद्योगिक ब्य.ऐन',dafa1 As 'दफा उद्योग स्तर',dafa2 As 'दफा उद्योग बर्ग', GetNumberToUnicode(kittanum) As 'कि.नं.',GetNumberToUnicode(rawmaterial_price) As 'कच्चा पदार्थ मुल्य',machine As 'मेशिन औजार', GetNumberToUnicode(machine_price) As 'मेशिन औजार मुल्य' FROM  industryreg INNER JOIN      setup_district ON industryreg.industrydist = setup_district.distcode INNER JOIN       setup_vdc ON industryreg.industryvdc = setup_vdc.VDC_SID INNER JOIN setup_subcategory ON setup_subcategory.subcategory_id = industryreg.industryscale INNER JOIN      setup_subcategory AS setup_subcategory_2 ON industryreg.industrytype = setup_subcategory_2.subcategory_id INNER JOIN       setup_subcategory AS setup_subcategory_1 ON industryreg.industrycat = setup_subcategory_1.subcategory_id where industryreg.industryid=@darta ", sqlcon.con);
                qry.SelectCommand.Parameters.AddWithValue("@darta", idt.ToString());
                DataTable tb = new DataTable();
                                qry.Fill(tb);
                dataGridView1.DataSource = tb;
                dataGridView1.Columns[0].Visible = false;

                sqlcon.con.Close();

                /////////////

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        public void DisplayDatadarta()
        {
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                /////////////////
                MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     industryreg.industryid, GetNumberToUnicode(industryreg.industryregno) AS 'दर्ता नं', industryreg.industrynepname AS 'उद्योगको नाम', setup_district.distunicodename AS 'जिल्ला',setup_vdc.vdcunicodename As 'गा.पा।न.पा.',GetNumberToUnicode(industryreg.industryward) As 'वार्ड', industryreg.tole AS 'टोल', industryreg.branch AS 'शाखा',   setup_subcategory.subcategory_unicodename AS 'स्तर', setup_subcategory_2.subcategory_unicodename AS 'कानूनी स्वरूप',   setup_subcategory_1.subcategory_unicodename AS 'वर्ग', GetNumberToUnicode(industryreg.regdat) AS 'दर्ता मिति',  GetNumberToUnicode(industryreg.renewdate) AS 'नविकरण मिति', industryreg.karobar AS 'उद्योगको उद्देश्य', GetNumberToUnicode(industryreg.yearlyturnover) AS 'बार्षिक उत्पादन',  GetNumberToUnicode(industryreg.electricpower) AS 'विधुत शक्ती',GetNumberToUnicode(industryreg.statcapital) AS 'स्थिर पुँजी', GetNumberToUnicode(industryreg.varcapital) AS 'चालु पुँजी', GetNumberToUnicode(industryreg.femaleworker) As 'महिला कामदार', GetNumberToUnicode(industryreg.maleworker) As 'पुरूष कामदार', GetNumberToUnicode(industryreg.tax) As 'राजश्व', industryreg.comment AS 'कैफियत', GetNumberToUnicode(industryreg.techworker) As 'प्राविधिक', GetNumberToUnicode(industryreg.nontechworker) As 'अप्राविधिक', industryreg.rawmaterial As 'कच्चा पदार्थ', GetNumberToUnicode(industryreg.fileno) As 'फाइल नं', GetNumberToUnicode(industryreg.thelino) As 'ठेली', GetNumberToUnicode(industryreg.fiscalyear) As 'आ.व' ,GetNumberToUnicode(industryreg.adhikrit_capital) As 'अधिकृत पुँजी', GetNumberToUnicode(industryreg.jari_capital) As 'जारी पुँजी',ain As 'औद्योगिक ब्य.ऐन',dafa1 As 'दफा उद्योग स्तर',dafa2 As 'दफा उद्योग बर्ग', GetNumberToUnicode(kittanum) As 'कि.नं.',GetNumberToUnicode(rawmaterial_price) As 'कच्चा पदार्थ मुल्य',machine As 'मेशिन औजार', GetNumberToUnicode(machine_price) As 'मेशिन औजार मुल्य' FROM  industryreg INNER JOIN      setup_district ON industryreg.industrydist = setup_district.distcode INNER JOIN       setup_vdc ON industryreg.industryvdc = setup_vdc.VDC_SID INNER JOIN setup_subcategory ON setup_subcategory.subcategory_id = industryreg.industryscale INNER JOIN      setup_subcategory AS setup_subcategory_2 ON industryreg.industrytype = setup_subcategory_2.subcategory_id INNER JOIN       setup_subcategory AS setup_subcategory_1 ON industryreg.industrycat = setup_subcategory_1.subcategory_id where  (industryreg.industryregno=@darta) or (GetNumberToUnicode(industryreg.industryregno)=@darta) or industryreg.industryregno=GetUnicodeToNumber(@darta) ", sqlcon.con);
                qry.SelectCommand.Parameters.AddWithValue("@darta", darta_txt.Text);
                DataTable tb = new DataTable();
                qry.Fill(tb);
                dataGridView1.DataSource = tb;
                dataGridView1.Columns[0].Visible = false;

                sqlcon.con.Close();
                /////////////
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }
        public void DisplayDataname()
        {
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }

                MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     industryreg.industryid, GetNumberToUnicode(industryreg.industryregno) AS 'दर्ता नं', industryreg.industrynepname AS 'उद्योगको नाम', setup_district.distunicodename AS 'जिल्ला',setup_vdc.vdcunicodename As 'गा.पा।न.पा.',GetNumberToUnicode(industryreg.industryward) As 'वार्ड', industryreg.tole AS 'टोल', industryreg.branch AS 'शाखा',   setup_subcategory.subcategory_unicodename AS 'स्तर', setup_subcategory_2.subcategory_unicodename AS 'कानूनी स्वरूप',   setup_subcategory_1.subcategory_unicodename AS 'वर्ग', GetNumberToUnicode(industryreg.regdat) AS 'दर्ता मिति',  GetNumberToUnicode(industryreg.renewdate) AS 'नविकरण मिति', industryreg.karobar AS 'उद्योगको उद्देश्य', GetNumberToUnicode(industryreg.yearlyturnover) AS 'बार्षिक उत्पादन',  GetNumberToUnicode(industryreg.electricpower) AS 'विधुत शक्ती',GetNumberToUnicode(industryreg.statcapital) AS 'स्थिर पुँजी', GetNumberToUnicode(industryreg.varcapital) AS 'चालु पुँजी', GetNumberToUnicode(industryreg.femaleworker) As 'महिला कामदार', GetNumberToUnicode(industryreg.maleworker) As 'पुरूष कामदार', GetNumberToUnicode(industryreg.tax) As 'राजश्व', industryreg.comment AS 'कैफियत', GetNumberToUnicode(industryreg.techworker) As 'प्राविधिक', GetNumberToUnicode(industryreg.nontechworker) As 'अप्राविधिक', industryreg.rawmaterial As 'कच्चा पदार्थ', GetNumberToUnicode(industryreg.fileno) As 'फाइल नं', GetNumberToUnicode(industryreg.thelino) As 'ठेली', GetNumberToUnicode(industryreg.fiscalyear) As 'आ.व' ,GetNumberToUnicode(industryreg.adhikrit_capital) As 'अधिकृत पुँजी', GetNumberToUnicode(industryreg.jari_capital) As 'जारी पुँजी',ain As 'औद्योगिक ब्य.ऐन',dafa1 As 'दफा उद्योग स्तर',dafa2 As 'दफा उद्योग बर्ग', GetNumberToUnicode(kittanum) As 'कि.नं.',GetNumberToUnicode(rawmaterial_price) As 'कच्चा पदार्थ मुल्य',machine As 'मेशिन औजार', GetNumberToUnicode(machine_price) As 'मेशिन औजार मुल्य' FROM  industryreg INNER JOIN      setup_district ON industryreg.industrydist = setup_district.distcode INNER JOIN       setup_vdc ON industryreg.industryvdc = setup_vdc.VDC_SID INNER JOIN setup_subcategory ON setup_subcategory.subcategory_id = industryreg.industryscale INNER JOIN      setup_subcategory AS setup_subcategory_2 ON industryreg.industrytype = setup_subcategory_2.subcategory_id INNER JOIN       setup_subcategory AS setup_subcategory_1 ON industryreg.industrycat = setup_subcategory_1.subcategory_id where industrynepname LIKE @firmname", sqlcon.con);
                //  qry.SelectCommand.Parameters.AddWithValue("@darta", darta_txt.Text);
                qry.SelectCommand.Parameters.AddWithValue("@firmname", textBox1.Text + '%');
                DataTable tb = new DataTable();
                qry.Fill(tb);
                dataGridView1.DataSource = tb;
                dataGridView1.Columns[0].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            sqlcon.con.Close();
        }
        public void DisplayOwnerData()
        {
            try
            {
                int i;

                i = dataGridView1.CurrentRow.Index;

                string dartanum = dataGridView1.Rows[i].Cells[0].Value.ToString();

                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                MySqlDataAdapter qry = new MySqlDataAdapter("select owner_industry.ownerid, owner_industry.ownerfname AS 'नाम', owner_industry.ownerlname AS 'थर', setup_district_1.distunicodename AS 'ठेगाना', setup_vdc.vdcunicodename AS 'गा.पा.। न.पा.', GetNumberToUnicode(owner_industry.ownervdcward) AS 'वार्ड', owner_industry.ownertole AS 'टोल',    setup_citizenissueoff.citizen_officeunicodename AS 'जारी गर्ने कार्यालय', setup_district.distunicodename AS 'जारी जिल्ला', GetNumberToUnicode(owner_industry.citzdate) AS 'जारी मिति',   GetNumberToUnicode(owner_industry.citznum) AS 'ना.प्र.नं.', setup_subcategory_1.subcategory_unicodename AS 'बाबु।पति', owner_industry.ownergname AS 'बावु।पति', setup_subcategory_2.subcategory_unicodename AS 'बाजे।ससुरा', owner_industry.ownerfgname AS 'बाजे।ससुरा', setup_subcategory.subcategory_unicodename AS 'लिंग', owner_industry.comment AS 'कैफियत', GetNumberToUnicode(owner_industry.contact) As 'सम्पर्क नं', owner_industry.email As 'ईमेल' FROM         owner_industry INNER JOIN setup_district ON owner_industry.cddist = setup_district.distcode INNER JOIN setup_citizenissueoff ON owner_industry.ccio = setup_citizenissueoff.citizen_officeid INNER JOIN setup_vdc ON owner_industry.ownervdccode = setup_vdc.VDC_SID INNER JOIN     setup_district AS setup_district_1 ON owner_industry.ownerdistcode = setup_district_1.distcode INNER JOIN        setup_subcategory ON owner_industry.gender = setup_subcategory.subcategory_id INNER JOIN      setup_subcategory AS setup_subcategory_1 ON owner_industry.ownergrel = setup_subcategory_1.subcategory_id INNER JOIN       setup_subcategory AS setup_subcategory_2 ON owner_industry.ownerfgrel = setup_subcategory_2.subcategory_id  WHERE     (owner_industry.industryid = @darta)", sqlcon.con);
                qry.SelectCommand.Parameters.AddWithValue("@darta", dartanum);
                DataTable tb = new DataTable();
                qry.Fill(tb);
                dataGridView2.DataSource = tb;
                dataGridView2.Columns[0].Visible = false;

                sqlcon.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if ((!string.IsNullOrWhiteSpace(textBox1.Text)) & (!string.IsNullOrWhiteSpace(darta_txt.Text)))
            {
                DisplayDatadartaname();
             
            }
            else if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                DisplayDataname();
            }
            else if (!string.IsNullOrWhiteSpace(darta_txt.Text))
            {
                DisplayDatadarta();
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DisplayOwnerData();
            button7.Enabled = true;
        }
        public void owner()
        {
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     owner_industry.ownerid,owner_industry.industryid, owner.ownerfname AS 'नाम', owner.ownerlname AS 'थर', setup_citizenissueoff.citizen_officeunicodename AS 'नागरिकता जारी गर्ने कार्यालय', setup_district.distunicodename AS 'जिल्ला',                       GetNumberToUnicode(owner.citzdate) AS 'जारी मिति', GetNumberToUnicode(owner.citznum) AS 'ना.प्र.नं.', setup_district_1.distunicodename AS 'जिल्ला', setup_vdc.vdcunicodename AS 'गा.वि.स । न.पा.', owner.ownertole AS 'टोल',                       setup_subcategory_1.subcategory_unicodename AS 'बाबु । पति', owner.ownergname AS 'बावु । पति नाम', setup_subcategory.subcategory_unicodename AS 'बाजे । ससुरा',                       owner.ownerfgname AS 'बाजे नाम', setup_subcategory_2.subcategory_unicodename AS 'लिंग', contact As 'सम्पर्क नं', email As 'ईमेल', owner.comment AS 'कैफियत' FROM         owner INNER JOIN            setup_district ON owner.cddist = setup_district.distcode INNER JOIN          setup_citizenissueoff ON owner.ccio = setup_citizenissueoff.citizen_officeid INNER JOIN            setup_district AS setup_district_1 ON owner.ownerdistcode = setup_district_1.distcode INNER JOIN         setup_vdc ON owner.ownervdccode = setup_vdc.VDC_SID INNER JOIN        setup_subcategory AS setup_subcategory_1 ON owner.ownergrel = setup_subcategory_1.subcategory_id INNER JOIN           setup_subcategory ON owner.ownerfgrel = setup_subcategory.subcategory_id INNER JOIN      setup_subcategory AS setup_subcategory_2 ON owner.gender = setup_subcategory_2.subcategory_id WHERE   (owner.ownerfname LIKE @fname) AND (owner.ownerlname LIKE @lname) AND (cddist = @dist) AND  (GetNumberToUnicode(owner.citzdate) LIKE @cdate) AND (GetNumberToUnicode(owner.citznum) Like @cnumber or (owner.citznum)  LIKE @cnumber)", sqlcon.con);
                qry.SelectCommand.Parameters.AddWithValue("@fname", fname.Text + '%');
                qry.SelectCommand.Parameters.AddWithValue("@lname", lname.Text + '%');
                qry.SelectCommand.Parameters.AddWithValue("@dist", district_combo.SelectedValue);
                qry.SelectCommand.Parameters.AddWithValue("@cdate", darta_date.Text + '%');
                qry.SelectCommand.Parameters.AddWithValue("@cnumber", cnum.Text + '%');
                DataTable tb = new DataTable();
                qry.Fill(tb);
                dataGridView2.DataSource = tb;
                dataGridView2.Columns[0].Visible = false;
                dataGridView2.Columns[1].Visible = false;

                sqlcon.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void ownerdist()
        {
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     owner_industry.ownerid,owner_industry.industryid, owner_industry.ownerfname AS 'नाम', owner_industry.ownerlname AS 'थर', setup_district_1.distunicodename AS 'ठेगाना', setup_vdc.vdcunicodename AS 'गा.पा.। न.पा.', GetNumberToUnicode(owner_industry.ownervdcward) AS 'वार्ड', owner_industry.ownertole AS 'टोल',    setup_citizenissueoff.citizen_officeunicodename AS 'जारी गर्ने कार्यालय', setup_district.distunicodename AS 'जारी जिल्ला', GetNumberToUnicode(owner_industry.citzdate) AS 'जारी मिति',   GetNumberToUnicode(owner_industry.citznum) AS 'ना.प्र.नं.', setup_subcategory_1.subcategory_unicodename AS 'बाबु।पति', owner_industry.ownergname AS 'बावु।पति', setup_subcategory_2.subcategory_unicodename AS 'बाजे।ससुरा', owner_industry.ownerfgname AS 'बाजे।ससुरा', setup_subcategory.subcategory_unicodename AS 'लिंग', owner_industry.comment AS 'कैफियत', GetNumberToUnicode(owner_industry.contact) As 'सम्पर्क नं', owner_industry.email As 'ईमेल' FROM         owner_industry INNER JOIN setup_district ON owner_industry.cddist = setup_district.distcode INNER JOIN setup_citizenissueoff ON owner_industry.ccio = setup_citizenissueoff.citizen_officeid INNER JOIN setup_vdc ON owner_industry.ownervdccode = setup_vdc.VDC_SID INNER JOIN     setup_district AS setup_district_1 ON owner_industry.ownerdistcode = setup_district_1.distcode INNER JOIN        setup_subcategory ON owner_industry.gender = setup_subcategory.subcategory_id INNER JOIN      setup_subcategory AS setup_subcategory_1 ON owner_industry.ownergrel = setup_subcategory_1.subcategory_id INNER JOIN       setup_subcategory AS setup_subcategory_2 ON owner_industry.ownerfgrel = setup_subcategory_2.subcategory_id WHERE   (owner_industry.ownerfname LIKE @fname) AND (owner_industry.ownerlname LIKE @lname) AND (owner_industry.cddist = @dist) AND  (GetNumberToUnicode(owner_industry.citzdate) LIKE @cdate) AND (GetNumberToUnicode(owner_industry.citznum) Like @cnumber or (owner_industry.citznum  LIKE @cnumber))", sqlcon.con);
                qry.SelectCommand.Parameters.AddWithValue("@fname", fname.Text + '%');
                qry.SelectCommand.Parameters.AddWithValue("@lname", lname.Text + '%');
                qry.SelectCommand.Parameters.AddWithValue("@dist", district_combo.SelectedValue);
                //  qry.SelectCommand.Parameters.AddWithValue("@cdate", darta_date.Text + '%');
                qry.SelectCommand.Parameters.AddWithValue("@cnumber", cnum.Text);
                DataTable tb = new DataTable();
                qry.Fill(tb);
                dataGridView2.DataSource = tb;
                dataGridView2.Columns[0].Visible = false;
                sqlcon.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        public void ownerdartadist()
        {
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     owner_industry.ownerid,owner_industry.industryid, owner_industry.ownerfname AS 'नाम', owner_industry.ownerlname AS 'थर', setup_district_1.distunicodename AS 'ठेगाना', setup_vdc.vdcunicodename AS 'गा.पा.। न.पा.', GetNumberToUnicode(owner_industry.ownervdcward) AS 'वार्ड', owner_industry.ownertole AS 'टोल',    setup_citizenissueoff.citizen_officeunicodename AS 'जारी गर्ने कार्यालय', setup_district.distunicodename AS 'जारी जिल्ला', GetNumberToUnicode(owner_industry.citzdate) AS 'जारी मिति',   GetNumberToUnicode(owner_industry.citznum) AS 'ना.प्र.नं.', setup_subcategory_1.subcategory_unicodename AS 'बाबु।पति', owner_industry.ownergname AS 'बावु।पति', setup_subcategory_2.subcategory_unicodename AS 'बाजे।ससुरा', owner_industry.ownerfgname AS 'बाजे।ससुरा', setup_subcategory.subcategory_unicodename AS 'लिंग', owner_industry.comment AS 'कैफियत', GetNumberToUnicode(owner_industry.contact) As 'सम्पर्क नं', owner_industry.email As 'ईमेल' FROM         owner_industry INNER JOIN setup_district ON owner_industry.cddist = setup_district.distcode INNER JOIN setup_citizenissueoff ON owner_industry.ccio = setup_citizenissueoff.citizen_officeid INNER JOIN setup_vdc ON owner_industry.ownervdccode = setup_vdc.VDC_SID INNER JOIN     setup_district AS setup_district_1 ON owner_industry.ownerdistcode = setup_district_1.distcode INNER JOIN        setup_subcategory ON owner_industry.gender = setup_subcategory.subcategory_id INNER JOIN      setup_subcategory AS setup_subcategory_1 ON owner_industry.ownergrel = setup_subcategory_1.subcategory_id INNER JOIN       setup_subcategory AS setup_subcategory_2 ON owner_industry.ownerfgrel = setup_subcategory_2.subcategory_id WHERE  (owner_industry.ownerfname LIKE @fname) AND (owner_industry.ownerlname LIKE @lname)  AND   (GetNumberToUnicode(owner_industry.citznum) Like @cnumber or (owner_industry.citznum)  LIKE @cnumber) ", sqlcon.con);
                qry.SelectCommand.Parameters.AddWithValue("@fname", fname.Text + '%');
                qry.SelectCommand.Parameters.AddWithValue("@lname", lname.Text + '%');
                // qry.SelectCommand.Parameters.AddWithValue("@dist", district_combo.SelectedValue);
                //qry.SelectCommand.Parameters.AddWithValue("@cdate", darta_date.Text + '%');
                qry.SelectCommand.Parameters.AddWithValue("@cnumber", cnum.Text + '%');
                DataTable tb = new DataTable();
                qry.Fill(tb);
                dataGridView2.DataSource = tb;
                dataGridView2.Columns[0].Visible = false;
                dataGridView2.Columns[1].Visible = false;


                sqlcon.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void ownerdarta()
        {
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     owner_industry.ownerid, owner_industry.industryid, owner_industry.ownerfname AS 'नाम', owner_industry.ownerlname AS 'थर', setup_district_1.distunicodename AS 'ठेगाना', setup_vdc.vdcunicodename AS 'गा.पा.। न.पा.', GetNumberToUnicode(owner_industry.ownervdcward) AS 'वार्ड', owner_industry.ownertole AS 'टोल',    setup_citizenissueoff.citizen_officeunicodename AS 'जारी गर्ने कार्यालय', setup_district.distunicodename AS 'जारी जिल्ला', GetNumberToUnicode(owner_industry.citzdate) AS 'जारी मिति',   GetNumberToUnicode(owner_industry.citznum) AS 'ना.प्र.नं.', setup_subcategory_1.subcategory_unicodename AS 'बाबु।पति', owner_industry.ownergname AS 'बावु।पति', setup_subcategory_2.subcategory_unicodename AS 'बाजे।ससुरा', owner_industry.ownerfgname AS 'बाजे।ससुरा', setup_subcategory.subcategory_unicodename AS 'लिंग', owner_industry.comment AS 'कैफियत', GetNumberToUnicode(owner_industry.contact) As 'सम्पर्क नं', owner_industry.email As 'ईमेल' FROM         owner_industry INNER JOIN setup_district ON owner_industry.cddist = setup_district.distcode INNER JOIN setup_citizenissueoff ON owner_industry.ccio = setup_citizenissueoff.citizen_officeid INNER JOIN setup_vdc ON owner_industry.ownervdccode = setup_vdc.VDC_SID INNER JOIN     setup_district AS setup_district_1 ON owner_industry.ownerdistcode = setup_district_1.distcode INNER JOIN        setup_subcategory ON owner_industry.gender = setup_subcategory.subcategory_id INNER JOIN      setup_subcategory AS setup_subcategory_1 ON owner_industry.ownergrel = setup_subcategory_1.subcategory_id INNER JOIN       setup_subcategory AS setup_subcategory_2 ON owner_industry.ownerfgrel = setup_subcategory_2.subcategory_id WHERE   (owner_industry.ownerfname LIKE @fname) AND (owner_industry.ownerlname LIKE @lname)  AND   (GetNumberToUnicode(owner_industry.citznum) Like @cnumber or (owner_industry.citznum)  LIKE @cnumber)", sqlcon.con);
                qry.SelectCommand.Parameters.AddWithValue("@fname", fname.Text + '%');
                qry.SelectCommand.Parameters.AddWithValue("@lname", lname.Text + '%');
                //qry.SelectCommand.Parameters.AddWithValue("@dist", district_combo.SelectedValue);
                qry.SelectCommand.Parameters.AddWithValue("@cdate", darta_date.Text + '%');
                qry.SelectCommand.Parameters.AddWithValue("@cnumber", cnum.Text + '%');
                DataTable tb = new DataTable();
                qry.Fill(tb);
                dataGridView2.DataSource = tb;
                dataGridView2.Columns[0].Visible = false;
                dataGridView2.Columns[1].Visible = false;
                sqlcon.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
                MySqlCommand cmo = new MySqlCommand("SELECT     subcategory_id  FROM setup_subcategory WHERE ( subcategory_unicodename = @firmtype)", sqlcon.con);
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
            int i;

            i = dataGridView1.CurrentRow.Index;
            //string comment_text = comment.Text;

            string dartanum = dataGridView1.Rows[i].Cells[0].Value.ToString();
            getindustrytype(dataGridView1.Rows[i].Cells[8].Value.ToString());
            getindustrynature(dataGridView1.Rows[i].Cells[9].Value.ToString());
            //MessageBox.Show(dartanum.ToString());
            printcertificate bd = new printcertificate();
            bd.MdiParent = this.MdiParent;

            bd.getdataa(dartanum, ind_type.Text, ind_nature.Text);


            bd.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void searchindustrydetailbyname_Load(object sender, EventArgs e)
        {
            button7.Enabled = false;
        }

        private void dataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                button2.Enabled = true;
                button3.Enabled = true;
                button7.Enabled = true;
                int i;

                i = dataGridView2.CurrentRow.Index;
                idt = dataGridView2.Rows[i].Cells[1].Value.ToString();
                MessageBox.Show(idt.ToString());
                DisplayDatafirm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i;

            i = dataGridView1.CurrentRow.Index;

            string dartan = dataGridView1.Rows[i].Cells[0].Value.ToString();
            //MessageBox.Show(dartanum.ToString());
            letterview lt = new letterview();
            lt.MdiParent = this.MdiParent;

            lt.getdata(dartan,"","");
            lt.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int i;

            i = dataGridView1.CurrentRow.Index;
            //string comment_text = comment.Text;

            string industryid = dataGridView1.Rows[i].Cells[0].Value.ToString();
            string dartanum = dataGridView1.Rows[i].Cells[1].Value.ToString();
            getindustrytype(dataGridView1.Rows[i].Cells[8].Value.ToString());
            getindustrynature(dataGridView1.Rows[i].Cells[9].Value.ToString());
            //MessageBox.Show(dartanum.ToString());
          //  industryfileupload bd = new industryfileupload();
          //  bd.MdiParent = this.MdiParent;

           // bd.getdataa(dartanum, ind_type.Text, ind_nature.Text, ind_nature.Text,industryid);


           // bd.Show();
        }
    }
}
