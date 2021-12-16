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
    public partial class SearchIndustryCertLetter : Form
    {
        public SearchIndustryCertLetter()
        {
            InitializeComponent();
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
        public void DisplayData()
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
           
          //  //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");
            try
            {
                /////////////////
                MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     industryreg.industryid, GetNumberToUnicode(industryreg.industryregno) AS 'दर्ता नं', industryreg.industrynepname AS 'उद्योगको नाम', setup_district.distunicodename AS 'जिल्ला',setup_vdc.vdcunicodename As 'गा.पा.। न.पा.',GetNumberToUnicode(industryreg.industryward) As 'वार्ड', industryreg.tole AS 'टोल', industryreg.branch AS 'शाखा',   setup_subcategory.subcategory_unicodename AS 'स्तर', setup_subcategory_2.subcategory_unicodename AS 'कानूनी स्वरूप',   setup_subcategory_1.subcategory_unicodename AS 'वर्ग', GetNumberToUnicode(industryreg.regdat) AS 'दर्ता मिति',  GetNumberToUnicode(industryreg.renewdate) AS 'नविकरण मिति', industryreg.karobar AS 'उद्योगको उदेश्य', GetNumberToUnicode(industryreg.yearlyturnover) AS 'बार्षिक उत्पादन',  GetNumberToUnicode(industryreg.electricpower) AS 'विधुत शक्ती', GetNumberToUnicode(industryreg.statcapital) AS 'स्थिर पुँजी', GetNumberToUnicode(industryreg.varcapital) AS 'चालु पुँजी', GetNumberToUnicode(industryreg.femaleworker) As 'महिला कामदार', GetNumberToUnicode(industryreg.maleworker) As 'पुरूष कामदार', GetNumberToUnicode(industryreg.tax) As 'राजश्व', industryreg.comment AS 'कैफियत', GetNumberToUnicode(industryreg.techworker) As 'प्राविधिक', GetNumberToUnicode(industryreg.nontechworker) As 'अप्राविधिक', industryreg.rawmaterial As 'कच्चा पदार्थ', GetNumberToUnicode(industryreg.fileno) As 'फाइल नं', GetNumberToUnicode(industryreg.thelino) As 'ठेली', GetNumberToUnicode(industryreg.fiscalyear) As 'आ.व'  FROM  industryreg  INNER JOIN setup_subcategory AS setup_subcategory_2 ON industryreg.industrytype = setup_subcategory_2.subcategory_id INNER JOIN       setup_subcategory AS setup_subcategory_1 ON industryreg.industrycat = setup_subcategory_1.subcategory_id INNER JOIN       setup_vdc ON industryreg.industryvdc = setup_vdc.VDC_SID INNER JOIN                setup_district ON industryreg.industrydist = setup_district.distcode INNER JOIN setup_subcategory   ON setup_subcategory.subcategory_id = industryreg.industryscale  where (industryreg.industryregno=@darta) or (GetNumberToUnicode(industryreg.industryregno)=GetNumberToUnicode(@darta)) ", sqlcon.con);
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

        public void DisplayOwnerData(){
             int i;

            i = dataGridView1.CurrentRow.Index;

            string dartanum = dataGridView1.Rows[i].Cells[0].Value.ToString();
        
            ////SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
           
           // con.Open();
            MySqlDataAdapter qry = new MySqlDataAdapter("select owner_industry.ownerid, owner_industry.ownerfname AS 'नाम', owner_industry.ownerlname AS 'थर', setup_district_1.distunicodename AS 'ठेगाना', setup_vdc.vdcunicodename AS 'गा.पा.। न.पा.', GetNumberToUnicode(owner_industry.ownervdcward) AS 'वार्ड', owner_industry.ownertole AS 'टोल',    setup_citizenissueoff.citizen_officeunicodename AS 'जारी गर्ने कार्यालय', setup_district.distunicodename AS 'जारी जिल्ला', GetNumberToUnicode(owner_industry.citzdate) AS 'जारी मिति',   GetNumberToUnicode(owner_industry.citznum) AS 'ना.प्र.नं.', setup_subcategory_1.subcategory_unicodename AS 'बाबु।पति', owner_industry.ownergname AS 'बावु।पति', setup_subcategory_2.subcategory_unicodename AS 'बाजे।ससुरा', owner_industry.ownerfgname AS 'बाजे।ससुरा', setup_subcategory.subcategory_unicodename AS 'लिंग', owner_industry.comment AS 'कैफियत', GetNumberToUnicode(owner_industry.contact) As 'सम्पर्क नं', owner_industry.email As 'ईमेल' FROM         owner_industry INNER JOIN setup_district ON owner_industry.cddist = setup_district.distcode INNER JOIN setup_citizenissueoff ON owner_industry.ccio = setup_citizenissueoff.citizen_officeid INNER JOIN setup_vdc ON owner_industry.ownervdccode = setup_vdc.VDC_SID INNER JOIN     setup_district AS setup_district_1 ON owner_industry.ownerdistcode = setup_district_1.distcode INNER JOIN        setup_subcategory ON owner_industry.gender = setup_subcategory.subcategory_id INNER JOIN      setup_subcategory AS setup_subcategory_1 ON owner_industry.ownergrel = setup_subcategory_1.subcategory_id INNER JOIN       setup_subcategory AS setup_subcategory_2 ON owner_industry.ownerfgrel = setup_subcategory_2.subcategory_id  WHERE     (owner_industry.industryid = @darta)", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@darta", dartanum);
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView2.DataSource = tb;
            dataGridView2.Columns[0].Visible = false;

            sqlcon.con.Close();

        }


       private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DisplayData();
        }
        public void getdataa(string dartanum)
        {
            darta_txt.Text = dartanum.ToString();
            DisplayData();
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

            bd.getdataa(dartanum, ind_type.Text,ind_nature.Text);


            bd.Show();
        }

        private void searchindustry_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            //POSITION and SIZE
            this.Left = (this.Parent.Width / 2) - (this.Width / 2) - 5;
            this.Top = 0;

            //border
            global.createBorderAround(this, Color.Teal, 2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int i;
                if (dataGridView1.Rows.Count > 0 && dataGridView1.Rows != null && (dataGridView1.SelectedRows.Count > 0))
                {

                    i = dataGridView1.CurrentRow.Index;

                    string dartan = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    //MessageBox.Show(dartanum.ToString());
                    letterview lt = new letterview();
                    lt.MdiParent = this.MdiParent;

                    lt.getdata(dartan, bodartha_txt.Text, bodartha_txt1.Text);
                    lt.Show();
                }
                else
                {
                    MessageBox.Show("कृपया रेकर्ड छान्नुहोला ।");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DisplayOwnerData();

        }

        private void add_bodartha_Click(object sender, EventArgs e)
        {
            bodartha_txt1.Text = "";
            bodartha_txt1.Visible = true;
            remove_bodartha.Visible = true;
            add_bodartha.Visible = false;
        }

        private void remove_bodartha_Click(object sender, EventArgs e)
        {
            bodartha_txt1.Text = "";
            bodartha_txt1.Visible = false;
            remove_bodartha.Visible = false;
            add_bodartha.Visible = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
