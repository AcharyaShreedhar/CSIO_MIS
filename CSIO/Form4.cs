using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
//using MySql.Data.MySqlClient;
namespace CSIO
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        public void adtobs()
        {
            
            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");
            ////////////////////////
            DateTime curdat = new DateTime();
            curdat = DateTime.Now;

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            
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
                    Nepaliyear=(row[0].ToString());
                   // label3.Text = Nepaliyear;
                    DateTime stddate=new DateTime();
                    stddate=Convert.ToDateTime(row[1].ToString());
                   // MessageBox.Show(stddate.ToString());
                    TimeSpan dt = curdat - stddate;
                    //int daysdiff = diffResult.Days;
                    //MessageBox.Show(dt.ToString());
                    int d = dt.Days;
                   // MessageBox.Show(d.ToString());
                   // MessageBox.Show(d.ToString());
                    for (int j = 3; j <= 14; j++)
                    {
                        
                       // MessageBox.Show(d.ToString());
                        int l = j + 1;
                        //MessageBox.Show(row[l].ToString());
                        if (d <= Convert.ToInt16(row[l].ToString()))
                        {
                            nepalimonth = k;
                           // MessageBox.Show(nepalimonth.ToString());
                            nepaliday = d + 1;
                            string ndate = Nepaliyear.ToString() + "Y" + nepalimonth.ToString() + "Y" + nepaliday.ToString();
                            
                            MessageBox.Show(ndate);
                            label2.Text = Nepaliyear.ToString()+ "Y" + nepalimonth.ToString("00") + "Y" + nepaliday.ToString("00");
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
            

            ////////////////////////
            
            
          








            sqlcon.con.Close();

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
            sql = "SELECT distcode, distnepname FROM setup_district ORDER BY distnepname";
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
                comboBox1.DisplayMember = "distnepname";
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
        private void cone() {

            MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=10.10.10.44;uid=csiokask_admin;" +
                "pwd=#Gh@relu*123;database=csiokask_csio;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
                MessageBox.Show("Connection oppen");
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Form4_Load(object sender, EventArgs e)
                {

                    MySqlConnection connn = new MySqlConnection("Server=csiokaski.gov.np;Port=3306;Database=csiokask_csio;Uid=csiokask_admin;Pwd=#Gh@relu*123;");
                    try
                    {
                        connn.Open();
                        MessageBox.Show("ConnectionState ok");
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show(ex.Message);

                    }
            
            adtobs();

            // this.FillDropDownList();
            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            
            AutoCompleteStringCollection combData = new AutoCompleteStringCollection();
            FillDropDownList(combData);
            comboBox1.AutoCompleteCustomSource = combData;
           
            comboBox1.SelectedValue = 40;

           Encoding a=(Encoding.GetEncoding(textBox1.ToString()));
           MessageBox.Show(a.ToString());

            //this.reportViewer1.RefreshReport();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void setupdistrictBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string a=MD5Hash(textBox1.Text);
            textBox2.Text = a.ToString();

           MessageBox.Show(a.ToString());
           //MessageBox.Show(comboBox1.ValueMember.ToString());
           //MessageBox.Show(comboBox1.Text.ToString());
           //MessageBox.Show(comboBox1.Text + " " + comboBox1.SelectedValue);
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
               // this.setup_districtTableAdapter.FillBy(this.cSIODataSet3.setup_district);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void fillByToolStripButton_Click_1(object sender, EventArgs e)
        {
            try
            {
              //  this.setup_districtTableAdapter.FillBy(this.cSIODataSet3.setup_district);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
        private string convert_from_unicode(string str, char c)
        {

            string rtstr = "";
            for (int i = 2; i < str.Length; i += 6)
            {
                string str1 = str.Substring(i, 4);
                c = (char)Int16.Parse(str1, System.Globalization.NumberStyles.HexNumber);
                rtstr += c;
            }
            return rtstr;

        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //textBox4.Text = int.Parse(textBox3.Text);

           //textBox4.Text=textBox3.Text.Replace('१', '1');
            textBox4.Text = textBox3.Text.Replace('०', '0').Replace('१', '1').Replace('२', '2').Replace('३', '3').Replace('४', '4').Replace('५', '5').Replace('६', '6').Replace('७', '7').Replace('८', '8').Replace('९', '9');
            


        }

       
    }
}
