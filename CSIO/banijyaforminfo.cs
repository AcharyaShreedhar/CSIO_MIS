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
    public partial class banijyaforminfo : Form
    {
        string wordrevenue;
        //string taxnumber;
        
        public banijyaforminfo()
        {
           //usr.Text = username;
            InitializeComponent();
            }
        public int transtype = 0;
        public string transid;
        public void wordconver(string number)
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            Int64 rev = Convert.ToInt64(number);
            Int64 rem = 100;
            string wd;
            Int64 remain;
            while (rev != 0)
            {
 MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();
          //  MySqlConnection conn = new MySqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");
                remain=rev % rem;
            string sql = "SELECT     nepaliword FROM         number_words WHERE     (numb = "+remain.ToString()+")";
            try
            {
                //conn.Open();
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                sqlcon.con.Close();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    wd = (row[0].ToString());
                    wd = wd.Trim();
                    if (rem == 100)
                    {
                        if (remain <= 0)
                        {
                            wordrevenue =  " रुपैया मात्र ";
                            rev = rev / rem;
                            rem = 1000;
                           
                        }
                        else
                        {
                            wordrevenue = wd + " रुपैया मात्र ";
                            rev = rev / rem;
                            rem = 1000;
                           
                        }
                    }
                    else if (rem <= 1000)
                    {
                        wordrevenue = wd + " सय " + wordrevenue;
                        rev = rev / rem;
                        rem = 100000;
                        
                    }
                    else if (rem <= 100000)
                    {
                        wordrevenue = wd + " हजार " + wordrevenue;
                        rev = rev / rem;
                        rem = 10000000;
                    }
                    else if (rem <= 10000000)
                    {
                        wordrevenue = wd + " लाख " + wordrevenue;
                        rev = rev / rem;
                        rem = 1000000000;
                    }
                    else if (rem <= 1000000000)
                    {
                        wordrevenue‌ = wd + " करोड " + wordrevenue;
                        rev = rev / rem;
                        rem = 100000000000;
                    }
                    else if (rem <= 100000000000)
                    {
                        rev = rev / rem;
                        wordrevenue‌ =  wd + " अरव "+wordrevenue ;
                    }
                }
              

            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
            }
            MessageBox.Show(wordrevenue.ToString());
           // word.Text = wordrevenue.ToString();
        }
        public void adtobs()
        {

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            } 
         //   MySqlConnection con = new MySqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");
            ////////////////////////
            DateTime curdat = new DateTime();
            curdat = DateTime.Now;


            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();
          
            string sql = "SELECT     Year, StDtEng, EndDTEng, NDNepM01, NDNepM02, NDNepM03, NDNepM04, NDNepM05, NDNepM06, NDNepM07, NDNepM08, NDNepM09, NDNepM10, NDNepM11, NDNepM12 FROM setup_nepcalender WHERE     ({ fn NOW() } BETWEEN StDtEng AND EndDTEng)";

            //string sql = "SELECT VdcCode, VdcNepnam FROM setup_vdc where setup_vdc.DistCode='" + comboBox3.ValueMember + "'";

            try
            {
               // conn.Open();
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                sqlcon.con.Close();
                // int i = 0;
                int k = 1;
                string Nepaliyear;
                int nepalimonth;
                int nepaliday;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Nepaliyear = (row[0].ToString());
                    // label3.Text = Nepaliyear;
                    DateTime stddate = new DateTime();
                    stddate = Convert.ToDateTime(row[1].ToString());
                    // MessageBox.Show(stddate.ToString());
                    TimeSpan dt = curdat - stddate;
                    //int daysdiff = diffResult.Days;
                    //MessageBox.Show(dt.ToString());
                    int d = dt.Days;
                    d = d + 1;
                    // MessageBox.Show(d.ToString());
                    // MessageBox.Show(d.ToString());
                    for (int j = 3; j <= 14; j++)
                    {

                        // MessageBox.Show(d.ToString());
                        // int l = j + 1;
                        //MessageBox.Show(row[l].ToString());
                        if (d <= Convert.ToInt16(row[j].ToString()))
                        {
                            nepalimonth = k;
                            // MessageBox.Show(nepalimonth.ToString());
                            nepaliday = d;

                            entry_nepdate.Text = Nepaliyear.ToString() + nepalimonth.ToString("00") + nepaliday.ToString("00");
                            break;
                        }
                        d = d - Convert.ToInt16(row[j].ToString());
                        k = k + 1;


                    }
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }
        public void firmkarobar(AutoCompleteStringCollection data)
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
            sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (setup_subcategory.category_id = 7)";
           // connection = new MySqlConnection(connetionString);
            try
            {
              //  //connection.Open();
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                sqlcon.con.Close();

               karobar.DataSource = ds.Tables[0];
                karobar.ValueMember = "subcategory_id";
                karobar.DisplayMember = "subcategory_unicodename";
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    data.Add(row[1].ToString());
                    //citizen_off.Items.Add(row[0].ToString()+" Y "+row[1].ToString());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }
        public void firmtype(AutoCompleteStringCollection data)
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
            sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (setup_subcategory.category_id = 6)";
          // connection = new MySqlConnection(connetionString);
            try
            {
               // //connection.Open();
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
               sqlcon.con.Close();

                firm_type.DataSource = ds.Tables[0];
                firm_type.ValueMember = "subcategory_id";
                firm_type.DisplayMember = "subcategory_unicodename";
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    data.Add(row[1].ToString());
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
            ////SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
           // //connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;
            sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (setup_subcategory.category_id = 5)";
           // connection = new MySqlConnection(connetionString);
            try
            {
               // //connection.Open();
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
               sqlcon.con.Close();

                firm_obj.DataSource = ds.Tables[0];
                firm_obj.ValueMember = "subcategory_id";
                firm_obj.DisplayMember = "subcategory_unicodename";
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
       
        public void cleardata()
        {
            //darta_txt.Text=null;
           // district_combo.SelectedValue=40;
            textBox1.Text=null;
            tole_txt.Text=null;
            //darta_date.Text=null;
            renew_date.Text=null;
            firm_name.Text=null;
            firm_karobar.Text=null;
            firm_capital.Text=null;
            branch_txt.Text=null;
            comment.Text=null;
            tax_txt.Text = null;
            firmeng_name.Text = null;
        }

        
 //Display Data in DataGridView  
        public void DisplayData()
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }

            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     banijyaform.firmid, GetNumberToUnicode(banijyaform.firmregno) AS 'र.नं', concat(banijyaform.firmnepname,'(',banijyaform.firmengname,')') AS 'फर्मको नाम', setup_district.distunicodename AS 'जिल्ला',           setup_vdc.vdcunicodename AS 'गा.पा.। न.पा.', GetNumberToUnicode(banijyaform.firmward) AS 'वार्ड', banijyaform.tole AS 'टोल',setup_subcategory.subcategory_unicodename AS 'प्रकार',  GetNumberToUnicode(banijyaform.regdat) AS 'दर्ता मिति',  GetNumberToUnicode(banijyaform.renewdate) AS 'नविकरण', setup_subcategory_1.subcategory_unicodename AS 'उद्देश्य', banijyaform.karobar AS 'कारोवार बस्तुहरु', GetNumberToUnicode(banijyaform.revenue) AS 'पुँजी',  GetNumberToUnicode(banijyaform.tax) AS 'राजश्व',banijyaform.branch AS 'शाखा',  setup_subcategory_2.subcategory_unicodename AS 'कारोवार', GetNumberToUnicode(banijyaform.thelino) AS 'ठेली न‌', GetNumberToUnicode(banijyaform.fileno) AS 'फाइल न‌', GetNumberToUnicode(banijyaform.fiscalyear) AS 'आ.व', banijyaform.comment AS 'कैफियत',banijyaform.firmengname,banijyaform.firmnepname AS 'फर्मको नाम',setup_darturtype.taxtypename As 'रसिद प्रकार', billno As 'रसिद नं',banijyaform.firmtype As 'Firm Type' FROM         banijyaform INNER JOIN    setup_subcategory ON banijyaform.firmtype = setup_subcategory.subcategory_id INNER JOIN     setup_district ON banijyaform.firmdist = setup_district.distcode INNER JOIN    setup_vdc ON banijyaform.firmvdc = setup_vdc.VDC_SID INNER JOIN  setup_subcategory AS setup_subcategory_1 ON banijyaform.firmscope = setup_subcategory_1.subcategory_id  INNER JOIN   setup_subcategory AS setup_subcategory_2 ON banijyaform.karobartype = setup_subcategory_2.subcategory_id INNER JOIN setup_darturtype ON setup_darturtype.taxtypeid=banijyaform.billtype where (GetNumberToUnicode(firmregno)=@darta or (firmregno)=@darta)", sqlcon.con);
          //SqlCommand qr = new SqlCommand("SELECT     banijyaform.firmid, banijyaform.firmregno AS 'र.नं', banijyaform.firmnepname AS 'फर्मको नाम', setup_district.distnepname AS 'जिल्ला',           setup_vdc.Vdcunicodename AS 'गा.पा.। न.पा.', banijyaform.firmward AS 'वार्ड', banijyaform.tole AS 'टोल',setup_subcategory.subcategory_unicodename AS 'प्रकार',  banijyaform.regdat AS 'दर्ता मिति',  banijyaform.renewdate AS 'नविकरण', setup_subcategory_1.subcategory_unicodename AS 'उद्देश्य', banijyaform.karobar AS 'कारोवार', banijyaform.revenue AS 'पुँजी',  banijyaform.tax AS 'राजश्व',banijyaform.branch AS 'शाखा', banijyaform.comment AS 'कैफियत' FROM         banijyaform INNER JOIN    setup_subcategory ON banijyaform.firmtype = setup_subcategory.subcategory_id INNER JOIN     setup_district ON banijyaform.firmdist = setup_district.distcode INNER JOIN    setup_vdc ON banijyaform.firmvdc = setup_vdc.VDC_SID INNER JOIN  setup_subcategory AS setup_subcategory_1 ON banijyaform.firmscope = setup_subcategory_1.subcategory_id where firmregno=@firmreg");
           qry.SelectCommand.Parameters.AddWithValue("@darta", darta_txt.Text);
          //  qry.CommandType = CommandType.Text;

           // qr.Parameters.AddWithValue("@firmreg", darta_txt.Text);
          //  qr.Connection = con;
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView1.DataSource = tb;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[20].Visible = false;
            dataGridView1.Columns[21].Visible = false;
            dataGridView1.Columns[24].Visible = false;
            

            sqlcon.con.Close();
           
        }
        //public void gettaxnumber()
        //{
        //    taxnumber = tax_txt.Text.Replace('०', '0');
        //    taxnumber = tax_txt.Text.Replace('१', '1');
        //    taxnumber = tax_txt.Text.Replace('२', '2');
        //    taxnumber = tax_txt.Text.Replace('३', '3');
        //    taxnumber = tax_txt.Text.Replace('४', '4');
        //    taxnumber = tax_txt.Text.Replace('५', '5');
        //    taxnumber = tax_txt.Text.Replace('६', '6');
        //    taxnumber = tax_txt.Text.Replace('७', '7');
        //    taxnumber = tax_txt.Text.Replace('८', '8');
        //    taxnumber = tax_txt.Text.Replace('९', '9');
        //    MessageBox.Show(taxnumber.ToString());

        //}
        //public void getnumber()
        //{
        //    wordrevenue = firm_capital.Text.Replace('०', '0');
        //   wordrevenue= firm_capital.Text.Replace('१', '1');
        //    wordrevenue = firm_capital.Text.Replace('२', '2');
        //    wordrevenue = firm_capital.Text.Replace('३', '3');
        //    wordrevenue = firm_capital.Text.Replace('४', '4');
        //    wordrevenue = firm_capital.Text.Replace('५', '5');
        //    wordrevenue = firm_capital.Text.Replace('६', '6');
        //    wordrevenue = firm_capital.Text.Replace('७', '7');
        //    wordrevenue = firm_capital.Text.Replace('८', '8');
        //    wordrevenue = firm_capital.Text.Replace('९', '9');
        //    MessageBox.Show(wordrevenue.ToString());
            
        //}
        
        public void getdataa(string darta,string dist,string vdc,string vdcnamee)
        {
           
            darta_txt.Text = null;
            darta_txt.Text = darta.ToString();
            //fileno_txt.Text = null;
            //fileno_txt.Text = dist.ToString();
            //theli_txt.Text = null;
            //theli_txt.Text = vdc.ToString();
            //fy_txt.Text = null;
            //fy_txt.Text = vdcnamee.ToString();


            //date_txt.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }
       

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }
        public void dropbilltype(AutoCompleteStringCollection dataCollection)
        {

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            //  //connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;
            sql = "SELECT taxtypeid, taxtypename FROM setup_darturtype";
            //connection = new MySqlConnection(connetionString);
            try
            {
                // //connection.Open();
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                sqlcon.con.Close();

                tax_type.DataSource = ds.Tables[0];
                tax_type.ValueMember = "taxtypeid";
                tax_type.DisplayMember = "taxtypename";
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
          //  //connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;
            sql = "SELECT distcode, distunicodename FROM setup_district ORDER BY distnepname";
            //connection = new MySqlConnection(connetionString);
            try
            {
               // //connection.Open();
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
                MessageBox.Show("Can not open connection inside district ! "+ex);
            }

        }

        public void FillDropDownListvdc(AutoCompleteStringCollection dataCollectionvdcs)
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
           // string connetionStrings = null;
           // MySqlConnection connections;
            MySqlCommand commands;
            MySqlDataAdapter adapters = new MySqlDataAdapter();
            DataSet dss = new DataSet();

            string sqls = null;
            //int distcodevalue = Convert.ToInt16(district_combo.SelectedValue);
          //  //connetionStrings = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";
         
          // MessageBox.Show("Value of DID=" + district_combo.SelectedValue);
           sqls = "SELECT VDC_SID,Vdcunicodename FROM  setup_vdc where DistCode='" +district_combo.SelectedValue+ "'";
           // sqls = "SELECT VdcCode,VdcNepnam FROM  setup_vdc where DistCode=distcodevalue";
  

        //    connections = new MySqlConnection(connetionStrings);
            try
            {
              //  //connections.Open();
                commands = new MySqlCommand(sqls, sqlcon.con);
                adapters.SelectCommand = commands;
                adapters.Fill(dss);
                adapters.Dispose();
                commands.Dispose();
                sqlcon.con.Close();

                vdc_combo.DataSource = dss.Tables[0];
                vdc_combo.ValueMember = "VDC_SID";
                vdc_combo.DisplayMember = "Vdcunicodename";
                foreach (DataRow row in dss.Tables[0].Rows)
                {
                    dataCollectionvdcs.Add(row[1].ToString());
                }


            }
            catch
            {
                //MessageBox.Show("Can not open connection ! inside vdc ");
            }

        }
        public void thelidropdown()
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

            //sql = "SELECT     theli_id, theli_no FROM         setup_theli INNER JOIN  setup_subcategory ON setup_theli.subcategory_id = setup_subcategory.subcategory_id INNER JOIN    setup_category ON setup_subcategory.category_id = setup_category.category_id WHERE     (setup_theli.subcategory_name = '" + theli_combo.SelectedValue + "')";
            sql = "SELECT     theli_id, GetNumberToUnicode(theli_no) As theli_no FROM         setup_theli INNER JOIN  setup_subcategory ON setup_theli.subcategory_id = setup_subcategory.subcategory_id INNER JOIN    setup_category ON setup_subcategory.category_id = setup_category.category_id WHERE (setup_theli.subcategory_id = 41) ORDER BY theli_no DESC";

            // connection = new MySqlConnection(connetionString);
            try
            {
                //connection.Open();
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                sqlcon.con.Close();

                theli_no.DataSource = ds.Tables[0];
                theli_no.ValueMember = "theli_id";
                theli_no.DisplayMember = "theli_no";
                // foreach (DataRow row in ds.Tables[0].Rows)
                // {
                //   dataCollectionvdc.Add(row[1].ToString());
                // }


            }
            catch
            {
                MessageBox.Show("Can not open connection ! ");
            }

        }
        public void fiscalyear()
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
            sql = "SELECT  FY_ID, GetNumberToUnicode(format_fiscal_yr(FY)) As fiscal FROM         setup_fy order by FY_ID desc";
            // connection = new MySqlConnection(connetionString);
            try
            {
                //connection.Open();
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                sqlcon.con.Close();

                fy_combo.DataSource = ds.Tables[0];
                fy_combo.ValueMember = "FY_ID";
                fy_combo.DisplayMember = "fiscal";
                //foreach (DataRow row in ds.Tables[0].Rows)
                // {
                //dataCollection.Add(row[1].ToString());
                //citizen_off.Items.Add(row[0].ToString()+" Y "+row[1].ToString());
                //}


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }
        private void banijyaformdetail_Load(object sender, EventArgs e)
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            owner_btn.Enabled = false;
            //button9.Enabled = false;
            usr.Text = global.username;
            delete_btn.Enabled = false;
            darta_date.Text = global.todaynepslash;
            renewdate();
            entry_nepdate.Text = global.nepalidate;
            // TODO: This line of code loads data into the 'cSIODataSet3.banijyaform' table. You can move, or remove it, as needed.
           // this.banijyaformTableAdapter.Fill(this.cSIODataSet3.banijyaform);
            // TODO: This line of code loads data into the 'cSIODataSet3.banijyaform' table. You can move, or remove it, as needed.
            //this.banijyaformTableAdapter.Fill(this.cSIODataSet3.banijyaform);
      //DisplayData();

            district_combo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            district_combo.AutoCompleteSource = AutoCompleteSource.CustomSource;

            AutoCompleteStringCollection combData = new AutoCompleteStringCollection();
            FillDropDownList(combData);
            district_combo.AutoCompleteCustomSource = combData;

            district_combo.SelectedValue = 40;

            fiscalyear();
            thelidropdown();
             tax_type.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            tax_type.AutoCompleteSource = AutoCompleteSource.CustomSource;

            AutoCompleteStringCollection taxtype = new AutoCompleteStringCollection();
            dropbilltype(taxtype);
            tax_type.AutoCompleteCustomSource = taxtype;

            tax_type.SelectedValue = 1;
            //vdc_combo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //vdc_combo.AutoCompleteSource = AutoCompleteSource.CustomSource;

            //AutoCompleteStringCollection combDataa = new AutoCompleteStringCollection();
            //FillDropDownListvdc(combDataa);
            //vdc_combo.AutoCompleteCustomSource = combDataa;
             DisplayData();

            firm_obj.AutoCompleteMode = AutoCompleteMode.Append;
            firm_obj.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection firmo = new AutoCompleteStringCollection();
            firmobj(firmo);
            firm_obj.AutoCompleteCustomSource = firmo;
            firm_obj.SelectedIndex = 0;

            firm_type.AutoCompleteMode = AutoCompleteMode.Append;
            firm_type.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection firmt = new AutoCompleteStringCollection();
            firmtype(firmt);
            firm_type.AutoCompleteCustomSource = firmt;
            firm_type.SelectedIndex = 0;

            karobar.AutoCompleteMode = AutoCompleteMode.Append;
            karobar.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection firmk = new AutoCompleteStringCollection();
            firmkarobar(firmk);
            karobar.AutoCompleteCustomSource = firmk;
            karobar.SelectedIndex = 0;
           //adtobs();
        }
        


        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       


      
      

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            DisplayData();
        }
        private void validationdarta()
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT * FROM banijyaform where firmregno=@darta", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@darta", darta_txt.Text);
            DataTable tb = new DataTable();
                             
            if (tb.Rows.Count > 0)
            {

                MessageBox.Show("दर्ता नं पहिले नै इन्टी भइ सकेकोले अर्को राख्नुहोस ।");


            

            }
            
            sqlcon.con.Close();

           

        }

        private void button8_Click(object sender, EventArgs e)
        {
            transtype = 1;
            DisplayData();

            cleardata();
            renewdate();
           validationdarta();
                panel1.Enabled = true;
                english_name.Enabled = true;
               //panel3.Enabled = true;
            //   MessageBox.Show("नयाँ दर्ता गर्ने हो ?");
               MessageBox.Show("फर्मको नयाँ दर्ता गर्ने हो ?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

             
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

     
       

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        public void transactionid()
        {
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }

                MySqlCommand cmd = new MySqlCommand("select industryregno, fiscalyear, officeid, officedist from setup_industryregid where industryregid=2", sqlcon.con);
                cmd.CommandType = CommandType.Text;
                // cmd.Connection = con;
                MySqlDataReader sdr = cmd.ExecuteReader();
                sdr.Read();
                transid = global.csioid.ToString().Trim() + global.fyid.ToString().Trim() + sdr["industryregno"].ToString().Trim();

                sdr.Close(); //closing data reader
                sqlcon.con.Close();

                // if (sqlcon.con.State == ConnectionState.Closed)
                sqlcon.con.Open();

                MySqlCommand cmo = new MySqlCommand("update setup_industryregid SET industryregno= industryregno+1 WHERE industryregid=2", sqlcon.con);
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


        private void button12_Click(object sender, EventArgs e)
        {
            if (transtype == 1)

            {
                DialogResult result1 = MessageBox.Show("नयाँ वाणिज्य फर्मको विवरण ईन्ट्री गर्ने हो ?",
    "नयाँ फर्मको विवरण ईन्ट्री",
    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result1 == DialogResult.Yes)
                {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }


                ///////

                MySqlDataAdapter query = new MySqlDataAdapter("select * from banijyaform where firmnepname=@firmname", sqlcon.con);
                query.SelectCommand.Parameters.AddWithValue("@firmname", firm_name.Text.Trim());
                DataTable tb = new System.Data.DataTable();
                query.Fill(tb);
                if (tb.Rows.Count > 0)
                {


                    MessageBox.Show("फर्मको नाम पहिले नै इन्ट्री भइ सकेको हुनाले कृपया अर्को नाम इन्ट्री गर्नुहोस ।", "नाम पहिले नै ईन्ट्री भइ समेको सम्बन्धमा", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);




                }
                ///



                else
                {
                    string curdate = DateTime.Now.ToString();

                        transactionid();

                    ///////////////////////////////
                        MySqlCommand cmd = new MySqlCommand("INSERT INTO banijyaform(firmid,firmregno,firmtype,firmdist ,firmvdc ,firmward,tole,regdat,renewdate,firmscope,firmnepname,karobar,revenue,branch,comment,usr,updatnepdate,tax,karobartype,thelino,fileno,fiscalyear,registrationdate,firmengname,billtype,billno) VALUES (@firmid,GetUnicodeToNumber(@darta), @firmtype, @firmdistrict, @firmvdc, GetUnicodeToNumber(@ward), @tole, GetUnicodeToNumber(@dartadate), GetUnicodeToNumber(@renewdate), @firmobj, @firmname, @firmkarobar,GetUnicodeToNumber(@firmcapital), @firmbranch, @comment, @user, @entrydate, GetUnicodeToNumber(@tax), @karobartype, @theli, @file, @fy,Getint(@rdate),@engname,@billtype,@billno)");
                    cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@firmid", transid.ToString());
                        cmd.Parameters.AddWithValue("@darta", darta_txt.Text);
                    cmd.Parameters.AddWithValue("@firmtype", firm_type.SelectedValue);
                    cmd.Parameters.AddWithValue("@firmdistrict", district_combo.SelectedValue);
                    cmd.Parameters.AddWithValue("@firmvdc", vdc_combo.SelectedValue);
                    cmd.Parameters.AddWithValue("@ward", textBox1.Text);
                    cmd.Parameters.AddWithValue("@tole", tole_txt.Text);
                    cmd.Parameters.AddWithValue("@dartadate", darta_date.Text);
                    cmd.Parameters.AddWithValue("@renewdate", renew_date.Text);
                    cmd.Parameters.AddWithValue("@firmobj", firm_obj.SelectedValue);
                    cmd.Parameters.AddWithValue("@firmname", firm_name.Text);
                    cmd.Parameters.AddWithValue("@firmkarobar", firm_karobar.Text);
                    cmd.Parameters.AddWithValue("@firmcapital", firm_capital.Text);
                    //cmd.Parameters.AddWithValue("@firmcapital", wordrevenue);

                    cmd.Parameters.AddWithValue("@firmbranch", branch_txt.Text);
                    cmd.Parameters.AddWithValue("@comment", comment.Text);
                    cmd.Parameters.AddWithValue("@user", usr.Text);
                    // cmd.Parameters.AddWithValue("@curdate", curdate.ToString());
                    cmd.Parameters.AddWithValue("@entrydate", entry_nepdate.Text);
                    cmd.Parameters.AddWithValue("@tax", tax_txt.Text);
                    cmd.Parameters.AddWithValue("@karobartype", karobar.SelectedValue);
                    cmd.Parameters.AddWithValue("@theli", theli_no.Text);
                    cmd.Parameters.AddWithValue("@file", fileno.Text);
                    cmd.Parameters.AddWithValue("@fy", fy_combo.Text);
                    cmd.Parameters.AddWithValue("@rdate", darta_date.Text);
                    cmd.Parameters.AddWithValue("@engname", firmeng_name.Text);
                    cmd.Parameters.AddWithValue("@billtype", tax_type.SelectedValue);
                    cmd.Parameters.AddWithValue("@billno", bill_no.Text);
                    cmd.Connection = sqlcon.con;
                    int n = cmd.ExecuteNonQuery();
                    /////////////////////////////


                    if (n > 0)
                    {
                            global.sync_dblog("banijyaform", transid.ToString(), "INSERT", transid.ToString(), "firmid");

                            sqlcon.con.Close();
                        dataGridView1.DataSource = null;
                        DisplayData();
                        cleardata();
                        MessageBox.Show("रेकर्ड इन्ट्री सफल भयो ।", "डाटा इन्ट्री", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                        // MessageBox.Show("रेकर्ड इन्टी सफल भयो ।");
                        //button9.Enabled = false;
                        save_btn.Enabled = false;
                        english_name.Enabled = false;


                    }







                }
            }
            }
            else if (transtype==2)
            {
                DialogResult result1 = MessageBox.Show("विवरण संशोधन गर्ने हो ?",
     "फर्मको विवरण संशोधन गर्न",
     MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result1 == DialogResult.Yes)
                {
                    if (sqlcon.con.State == ConnectionState.Closed)
                    {
                        sqlcon.con.Open();
                    }
                    string curdate = DateTime.Today.ToString();



                    MySqlCommand cmd = new MySqlCommand(" UPDATE banijyaform   SET firmregno = GetUnicodeToNumber(@darta),firmtype = @firmtype ,firmdist = @firmdistrict ,firmvdc =  @firmvdc,firmward = GetUnicodeToNumber(@ward),tole = @tole,regdat = GetUnicodeToNumber(@dartadate) ,renewdate = GetUnicodeToNumber(@renewdate)   ,firmscope = @firmobj,firmnepname = @firmname,karobar = @firmkarobar,revenue = GetUnicodeToNumber(@firmcapital) ,tax =GetUnicodeToNumber(@tax)    ,branch = @firmbranch ,comment = @comment,usr = @user,updatdate = @curdate ,updatnepdate = @entrydate,karobartype=@karobartype,firmengname=@engname,billtype=@billtype,billno=@billno, thelino=@theli,fileno=@file,fiscalyear=@fy WHERE firmid=@indid");

                    //  INSERT INTO banijyaform('firmregno',[firmtype],[firmdist] ,[firmvdc] ,[firmward],tole,[regdat],[renewdate],[firmscope],[firmnepname],[karobar],[revenue],[branch],comment,[usr],[updatdate],[updatnepdate]) VALUES 
                    //                        (@darta, @firmtype, @firmdistrict, @firmvdc, @ward, @tole, @dartadate, @renewdate,                                                                                                                                                                  @firmobj, @firmname, @firmkarobar, @firmcapital, @firmbranch, @comment, @user, @curdate, @entrydate)
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@indid", id.Text);
                    cmd.Parameters.AddWithValue("@darta", darta_txt.Text);
                    cmd.Parameters.AddWithValue("@firmtype", firm_type.SelectedValue);
                    cmd.Parameters.AddWithValue("@firmdistrict", district_combo.SelectedValue);
                    cmd.Parameters.AddWithValue("@firmvdc", vdc_combo.SelectedValue);
                    cmd.Parameters.AddWithValue("@ward", textBox1.Text);
                    cmd.Parameters.AddWithValue("@tole", tole_txt.Text);
                    cmd.Parameters.AddWithValue("@dartadate", darta_date.Text);
                    cmd.Parameters.AddWithValue("@renewdate", renew_date.Text);
                    cmd.Parameters.AddWithValue("@firmobj", firm_obj.SelectedValue);
                    cmd.Parameters.AddWithValue("@firmname", firm_name.Text);
                    cmd.Parameters.AddWithValue("@firmkarobar", firm_karobar.Text);
                    cmd.Parameters.AddWithValue("@firmcapital", firm_capital.Text);
                    cmd.Parameters.AddWithValue("@tax", tax_txt.Text);
                    cmd.Parameters.AddWithValue("@firmbranch", branch_txt.Text);
                    cmd.Parameters.AddWithValue("@comment", comment.Text);
                    cmd.Parameters.AddWithValue("@user", usr.Text);
                    cmd.Parameters.AddWithValue("@curdate", curdate.ToString());
                    cmd.Parameters.AddWithValue("@entrydate", entry_nepdate.Text);
                    cmd.Parameters.AddWithValue("@karobartype", karobar.SelectedValue);
                    cmd.Parameters.AddWithValue("@engname", firmeng_name.Text);
                    cmd.Parameters.AddWithValue("@billtype", tax_type.SelectedValue);
                    cmd.Parameters.AddWithValue("@billno", bill_no.Text);
                    cmd.Parameters.AddWithValue("@theli", theli_no.Text);
                    cmd.Parameters.AddWithValue("@file", fileno.Text);
                    cmd.Parameters.AddWithValue("@fy", fy_combo.Text);
                    cmd.Connection = sqlcon.con;


                    //int n = qr.ExecuteNonQuery();
                    int n = cmd.ExecuteNonQuery();

                    if (n > 0)
                    {
                        global.sync_dblog("banijyaform", id.ToString(), "UPDATE", id.ToString(), "firmid");

                        sqlcon.con.Close();
                        dataGridView1.DataSource = null;
                        DisplayData();
                        cleardata();
                        MessageBox.Show("संशोधन सफल भयो ।", "संशोधन", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        //button9.Enabled = false;
                        save_btn.Enabled = false;
                        english_name.Enabled = false;
                        new_btn.Enabled = true;

                    }

                }
            }
                 }

        //private void district_combo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    vdc_combo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //    vdc_combo.AutoCompleteSource = AutoCompleteSource.CustomSource;

        //    AutoCompleteStringCollection combDataa = new AutoCompleteStringCollection();
        //    FillDropDownListvdc(combDataa);
        //    vdc_combo.AutoCompleteCustomSource = combDataa;
        //    // vdc_combo.SelectedIndex = 0;
        //    vdc_combo.SelectedValue = 1;
        //}

        private void darta_txt_TextChanged(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void firm_name_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void district_txt_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("कुनै रेकर्ड छानिएको छैन । कृपया रेकर्ड छान्नुहोस ।", "रेकर्ड छान्नुहोस");
                }
                else
                {


                    int i;

                    i = dataGridView1.CurrentRow.Index;
                    id.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    string firmnames = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    string firmtype = dataGridView1.Rows[i].Cells[24].Value.ToString();
                    owendetailbanijya ow = new owendetailbanijya();
                    ow.MdiParent = this.ParentForm;
                    ow.getdataa(darta_txt.Text, firmnames.ToString(), id.Text, firmtype.ToString());


                    ow.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void usr_TextChanged(object sender, EventArgs e)
        {

        }

        private void firm_obj_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void darta_date_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void district_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //vdc_combo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //vdc_combo.AutoCompleteSource = AutoCompleteSource.CustomSource;

            //AutoCompleteStringCollection combDataa = new AutoCompleteStringCollection();
            //FillDropDownListvdc(combDataa);
            //vdc_combo.AutoCompleteCustomSource = combDataa;
            //// vdc_combo.SelectedIndex = 0;
            //vdc_combo.SelectedValue = 1;


            vdc_combo.Text = null;
            vdc_combo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            vdc_combo.AutoCompleteSource = AutoCompleteSource.CustomSource;

            AutoCompleteStringCollection combDataa = new AutoCompleteStringCollection();
            FillDropDownListvdc(combDataa);
            vdc_combo.AutoCompleteCustomSource = combDataa;
        }

        private void button9_Click(object sender, EventArgs e)
        {
           
        }
        public void district(string dist)
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
           // string connetionStrings = null;
           // MySqlConnection connections;
            MySqlCommand commands;
            MySqlDataAdapter adapters = new MySqlDataAdapter();
            DataSet dss = new DataSet();

            string sqls = null;
            //int distcodevalue = Convert.ToInt16(district_combo.SelectedValue);
           // //connetionStrings = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";

            //sqls = "SELECT VdcCode,VdcNepnam FROM  setup_vdc where DistCode='" +district_combo.SelectedValue+ "'";
            sqls = " SELECT     distcode, distunicodename FROM         setup_district WHERE     (distunicodename = @dist)";
           // sqls = " SELECT     distcode, distunicodename FROM         setup_district WHERE     (distunicodename = @dist)";

           // connections = new MySqlConnection(connetionStrings);

            
            commands = new MySqlCommand(sqls, sqlcon.con);
            adapters.SelectCommand = commands;
            adapters.SelectCommand.Parameters.AddWithValue("@dist", dist.ToString());
           // adapters.SelectCommand.Parameters.AddWithValue("@dist", dist.ToString());
            adapters.Fill(dss);
            adapters.Dispose();
            commands.Dispose();
           sqlcon.con.Close();


            foreach (DataRow row in dss.Tables[0].Rows)
            {

                district_combo.SelectedValue = row[0].ToString();
            }

        }
        public void vdcedit(string vdc)
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }

          //  string connetionStrings = null;
          //  MySqlConnection connections;
            MySqlCommand commands;
            MySqlDataAdapter adapters = new MySqlDataAdapter();
            DataSet dss = new DataSet();

            string sqls = null;
            //int distcodevalue = Convert.ToInt16(district_combo.SelectedValue);
           // //connetionStrings = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";

            //sqls = "SELECT VdcCode,VdcNepnam FROM  setup_vdc where DistCode='" +district_combo.SelectedValue+ "'";
            sqls = "SELECT VDC_SID,vdcunicodename FROM  setup_vdc where vdcunicodename=@vdc";


           // connections = new MySqlConnection(connetionStrings);
            try
            {
                ////connections.Open();
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
                    vdc_combo.SelectedValue = row[0].ToString();
                }
                // vdc_combo.SelectedItem = vdc.ToString();

            }
            catch
            {
                //MessageBox.Show("Can not open connection ! inside vdc "+ex);
            }

        }
        public void firmtypeedit(string firmobj)
        {

            //string connetionString = null;
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
           // //connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;
            sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (subcategory_unicodename = @firmobj)";
           // //connection = new SqlConnection(connetionString);
            try
            {
               // //connection.Open();
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
                    firm_type.SelectedValue = row[0].ToString();
                    //citizen_off.Items.Add(row[0].ToString()+" Y "+row[1].ToString());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }
        public void firmobjedit(string firmobj)
        {

            //string connetionString = null;
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
          //  //connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;
            sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (subcategory_unicodename = @firmobj)";
         //   //connection = new SqlConnection(connetionString);
            try
            {
              //  //connection.Open();
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
                    firm_obj.SelectedValue=row[0].ToString();
                    //citizen_off.Items.Add(row[0].ToString()+" Y "+row[1].ToString());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }

        public void firmkarobaredit(string firmobj)
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }

            //string connetionString = null;
           // //SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
           // //connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;
            sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (subcategory_unicodename = @firmobj)";
          //  //connection = new SqlConnection(connetionString);
            try
            {
               // //connection.Open();
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
                    karobar.SelectedValue = row[0].ToString();
                    //citizen_off.Items.Add(row[0].ToString()+" Y "+row[1].ToString());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }
        public void renewdate()
        {
            if (darta_date.MaskFull != false)
            {
                string year;
                string month;
                string day;
                string years;
                string months;
                string days;
                int ryear = 0;
                int rmonth = 0;
                int rday = 0;
                try
                {


                    years = (darta_date.Text).Substring(0, 4);
                    months = (darta_date.Text).Substring(5, 2);
                    days = (darta_date.Text).Substring(8, 2);


                    //  SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.passwordname + "' ");
                    if (sqlcon.con.State == ConnectionState.Closed)
                    {
                        sqlcon.con.Open();
                    }

                    MySqlDataAdapter qrey = new MySqlDataAdapter("SELECT    GetUnicodeToNumber(N'" + years.ToString() + "'),  GetUnicodeToNumber(N'" + months.ToString() + "'), GetUnicodeToNumber(N'" + days.ToString() + "') ", sqlcon.con);
                    //  qrey.SelectCommand.Parameters.AddWithValue("@id", cust.ToString());


                    DataTable tb = new DataTable();
                    qrey.Fill(tb);
                    year = tb.Rows[0][0].ToString();
                    month = tb.Rows[0][1].ToString();
                    day = tb.Rows[0][2].ToString();
                    //  con.Open();
                    ////////////////////////////////////////
                    //  MySqlCommand command;
                    //MySqlDataAdapter adapter = new MySqlDataAdapter();
                    //DataSet ds = new DataSet();
                    //string sql = "SELECT    GetUnicodeToNumber(N'" + years.ToString() + "'),  GetUnicodeToNumber(N'" + months.ToString() + "'), GetUnicodeToNumber(N'" + days.ToString() + "') ";

                    ////string sql = "SELECT VdcCode, VdcNepnam FROM setup_vdc where setup_vdc.DistCode='" + comboBox3.ValueMember + "'";



                    //command = new SqlCommand(sql, sqlcon.con);
                    //adapter.SelectCommand = command;
                    //adapter.Fill(ds);
                    //adapter.Dispose();
                    //command.Dispose();


                    //foreach (DataRow row in ds.Tables[0].Rows)
                    //{
                    //    year = row[0].ToString();
                    //    month = row[1].ToString();
                    //    day = row[2].ToString();

                    //     MessageBox.Show(year.ToString() + " ," + month.ToString() + " ," + day.ToString());

                    if (day.ToString() == "01")
                    {
                        rday = 30;
                        if (month == "01")
                        {
                            rmonth = 12;
                            ryear = int.Parse(year) + 4;

                        }

                        else
                        {
                            ryear = int.Parse(year) + 5;
                            rmonth = (int.Parse(month)) - 1;
                        }
                    }
                    else
                    {
                        rday = (int.Parse(day)) - 1;
                        rmonth = int.Parse(month);
                        ryear = int.Parse(year) + 5;
                    }

                    //   MessageBox.Show("Year:" + ryear.ToString("0000") + "Month:" + rmonth.ToString("00") + "Day:" + rday.ToString("00"));
                    string newdate = ryear.ToString("0000") + rmonth.ToString("00") + rday.ToString("00");
                    //  sql = "SELECT    GetNumberToUnicode('" + newdate.ToString() + "') ";

                    //string sql = "SELECT VdcCode, VdcNepnam FROM setup_vdc where setup_vdc.DistCode='" + comboBox3.ValueMember + "'";

                    MySqlDataAdapter qreys = new MySqlDataAdapter("SELECT    GetNumberToUnicode('" + newdate.ToString() + "') ", sqlcon.con);
                    //  qrey.SelectCommand.Parameters.AddWithValue("@id", cust.ToString());


                    DataTable tbs = new DataTable();
                    qreys.Fill(tbs);
                    renew_date.Text = tbs.Rows[0][0].ToString();
                    sqlcon.con.Close();
                    //command = new SqlCommand(sql, sqlcon.con);
                    //adapter.SelectCommand = command;
                    //adapter.Fill(ds);
                    //adapter.Dispose();
                    //command.Dispose();


                    //foreach (DataRow rowss in ds.Tables[0].Rows)
                    //{
                    //  renew_date.Text = rowss[0].ToString();
                    // MessageBox.Show("Inside loop");

                    //month = row[1].ToString();
                    //day = row[2].ToString();

                    //  }  //renew_date.Text = ryear.ToString("0000") + rmonth.ToString("00") + rday.ToString("00");
                    // }
                }
                catch (Exception ex)
                {
                    // MessageBox.Show("Before Exception");
                    MessageBox.Show(ex.ToString());

                }
            }
            else
            {
                MessageBox.Show("Please enter complete date", "Date Error");
            }
        }
        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            transtype = 2;
            try
            {
                english_name.Enabled = true;
                //button9.Enabled = true;
                save_btn.Enabled = true;
                english_name.Enabled = true;
                delete_btn.Enabled = true;
                new_btn.Enabled = true;
                int i = 0;

                i = dataGridView1.CurrentRow.Index;
                id.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();




                darta_txt.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
                firm_name.Text = dataGridView1.Rows[i].Cells[21].Value.ToString();
                district(dataGridView1.Rows[i].Cells[3].Value.ToString());

                vdcedit(dataGridView1.Rows[i].Cells[4].Value.ToString());
                textBox1.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
                tole_txt.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();
                firmtypeedit(dataGridView1.Rows[i].Cells[7].Value.ToString());
                darta_date.Text = dataGridView1.Rows[i].Cells[8].Value.ToString();
                renew_date.Text = dataGridView1.Rows[i].Cells[9].Value.ToString();
                firmobjedit(dataGridView1.Rows[i].Cells[10].Value.ToString());
                firm_karobar.Text = dataGridView1.Rows[i].Cells[11].Value.ToString();
                firm_capital.Text = dataGridView1.Rows[i].Cells[12].Value.ToString();
                tax_txt.Text = dataGridView1.Rows[i].Cells[13].Value.ToString();
                branch_txt.Text = dataGridView1.Rows[i].Cells[14].Value.ToString();
                firmkarobaredit(dataGridView1.Rows[i].Cells[15].Value.ToString());
                theli_no.SelectedItem = dataGridView1.Rows[i].Cells[16].Value.ToString();
                fileno.Text = dataGridView1.Rows[i].Cells[17].Value.ToString();
              fy_combo.SelectedItem = dataGridView1.Rows[i].Cells[18].Value.ToString();
                comment.Text = dataGridView1.Rows[i].Cells[19].Value.ToString();
                firmeng_name.Text = dataGridView1.Rows[i].Cells[20].Value.ToString();
                tax_type.SelectedItem = dataGridView1.Rows[i].Cells[22].Value.ToString();
                bill_no.Text = dataGridView1.Rows[i].Cells[23].Value.ToString();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
           
            
        }

        private void button11_Click(object sender, EventArgs e)
        {
            cleardata();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show("विवरण हटाउने हो ?",
       "उद्योगको विवरण हटाउन",
       MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result1 == DialogResult.Yes)
            {

                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                int i;
                i = dataGridView1.CurrentRow.Index;


                int id = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                MySqlCommand qr = new MySqlCommand(" DELETE FROM banijyaform  WHERE firmid=@indid");
               // global.sync_dblog("banijyaform", id.ToString(), "DELETE", id.ToString(), "firmid");

                qr.CommandType = CommandType.Text;

                qr.Parameters.AddWithValue("@indid", id.ToString());
                qr.Connection = sqlcon.con;


                //int n = qr.ExecuteNonQuery();
                int n = qr.ExecuteNonQuery();

                if (n > 0)
                {
                    global.sync_dblog("banijyaform", id.ToString(), "DELETE", id.ToString(), "firmid");
                    sqlcon.con.Close();
                    dataGridView1.DataSource = null;
                    DisplayData();
                    cleardata();
                    MessageBox.Show("डाटा  सफलता पुर्वक हटायो ।");


                }
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void firm_capital_TabIndexChanged(object sender, EventArgs e)
        {
            
            //  wordconver(firm_capital.Text);
        }

        private void firm_capital_TextChanged(object sender, EventArgs e)
        {
           // wordconver(firm_capital.Text);
            //firm_capital.Text = firm_capital.Text.Replace('०', '0').Replace('१', '1').Replace('२', '2').Replace('३', '3').Replace('४', '4').Replace('५', '5').Replace('६', '6').Replace('७', '7').Replace('८', '8').Replace('९', '9');
            //firm_capital.Focus();
            //firm_capital.Select(firm_capital.Text.Length, 0);
           
        }

        private void tax_txt_TextChanged(object sender, EventArgs e)
        {
                  }

        private void tax_txt_TabIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            owner_btn.Enabled = true;
            delete_btn.Enabled = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void darta_date_KeyUp(object sender, KeyEventArgs e)
        {
            if (darta_date.MaskFull)
            {
                renewdate();
            }
        }

       

       

      

      

      
    }
}
