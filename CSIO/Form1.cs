using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
//using MySql.Data.MySqlClient;

namespace CSIO
{
    public partial class login : Form
    {
        
        public login()
        {
            InitializeComponent();
        }

        bool askBefClose = true;

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
        public void csioadd()
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();
            string sql = "SELECT    csioNepNm,csioid,csioDist,officeaddress from setup_csio where isCur='True'";

            //string sql = "SELECT VdcCode, VdcNepnam FROM setup_vdc where setup_vdc.DistCode='" + comboBox3.ValueMember + "'";



            command = new MySqlCommand(sql, sqlcon.con);
            adapter.SelectCommand = command;
            adapter.Fill(ds);
            adapter.Dispose();
            command.Dispose();


            foreach (DataRow row in ds.Tables[0].Rows)
            {
                String firmid = row[0].ToString();
                String address = row[3].ToString();
                global.csioffice = firmid.ToString()+", "+address.ToString();
                global.csioid = row[1].ToString();
                global.csiodist = row[2].ToString();
                //MessageBox.Show(firmid.ToString());
            }
            
        }
        public void officedisplay()
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            ////////////////////////////////////////
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();
            string sql = "SELECT csioid,  Concat(cast(csioNepNm,', ',officeaddress)) As 'address' FROM setup_csio where IsCur='True' ";

            //string sql = "SELECT VdcCode, VdcNepnam FROM setup_vdc where setup_vdc.DistCode='" + comboBox3.ValueMember + "'";



            command = new MySqlCommand(sql, sqlcon.con);
            adapter.SelectCommand = command;
            adapter.Fill(ds);
            adapter.Dispose();
            command.Dispose();


            foreach (DataRow row in ds.Tables[0].Rows)
            {
                String ff = row[1].ToString();
                //  String fs = row[1].ToString();
                //  global.csioffice = ff.ToString()+"/"+fs.ToString();
              //  global.csioffice = ff.ToString();
                officename.Text = ff.ToString();
                global.office = ff.ToString();
                //MessageBox.Show(firmid.ToString());
            }
            sqlcon.con.Close();
        }
        public void fy()
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            ////////////////////////////////////////
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();
            string sql = "SELECT GetNumberToUnicode(format_fiscal_yr(FY)),FY_ID from setup_fy where IsCur=True ";

            //string sql = "SELECT VdcCode, VdcNepnam FROM setup_vdc where setup_vdc.DistCode='" + comboBox3.ValueMember + "'";

            command = new MySqlCommand(sql, sqlcon.con);
            adapter.SelectCommand = command;
            adapter.Fill(ds);
            adapter.Dispose();
            command.Dispose();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                String ff = row[0].ToString();
             //  String fs = row[1].ToString();
             //  global.csioffice = ff.ToString()+"/"+fs.ToString();
               global.csioffice = ff.ToString();
               fiscal_txt.Text = ff.ToString();
               global.fyid = row[1].ToString();
                global.fy = ff.ToString();
                //MessageBox.Show(firmid.ToString());
            }
            sqlcon.con.Close();
        }
        public void getunicodedate()
        {

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            } ////////////////////////
            DateTime curdat = new DateTime();
            curdat = DateTime.Now;


            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = "SELECT GetNumberToUnicode(GetNepaliDate(getdate())),Getint(GetNepaliDate(getdate()))";

            //string sql = "SELECT VdcCode, VdcNepnam FROM setup_vdc where setup_vdc.DistCode='" + comboBox3.ValueMember + "'";

            try
            {
               
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                sqlcon.con.Close();
                // int i = 0;
            
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                     global.todaynepslash= (row[0].ToString());
                     global.nepalidate = (row[1].ToString());
                    
                        }
                        
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }


            ////////////////////////

            sqlcon.con.Close();

        }
        public void expcheck()
        {
            if (int.Parse(Properties.Settings.Default.updatdate.ToString())< int.Parse(global.nepalidate.ToString()))
            {
                Properties.Settings.Default.updatdate=int.Parse(global.nepalidate.ToString());
                Properties.Settings.Default.noofdays = Properties.Settings.Default.noofdays - 1;
               // MessageBox.Show(Properties.Settings.Default.updatdate.ToString());
                Properties.Settings.Default.Save();
            }

        }
        public void checkbackdate()
        {
          DateTime curdate=  DateTime.Now;
          DateTime storedate = DateTime.Parse(Properties.Settings.Default.newdatetime.ToString());
         int diff1 = (curdate-storedate).Days;
        // MessageBox.Show(diff1.ToString());
          if (diff1 < 0)
          {
              MessageBox.Show("Check Your System Date","System Date Error");
             // MessageBox.Show(diff1.ToString());
              button1.Enabled = false;
          }
          else
          {
              Properties.Settings.Default.newdatetime = curdate;
         
              Properties.Settings.Default.Save();
          }
            
      
        //  MessageBox.Show(Properties.Settings.Default.newdatetime.ToString());
            //if (int.Parse(Properties.Settings.Default.updatdate.ToString()) < int.Parse(global.nepalidate.ToString()))
            //{
            //    Properties.Settings.Default.updatdate = int.Parse(global.nepalidate.ToString());
            //    Properties.Settings.Default.noofdays = Properties.Settings.Default.noofdays - 1;
            //    // MessageBox.Show(Properties.Settings.Default.updatdate.ToString());
            //    Properties.Settings.Default.Save();
            //}

        }
        public void adtobs()
        {

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            ///////////////////////
            DateTime curdat = new DateTime();
            curdat = DateTime.Now;


            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();
           // MySqlconection conn = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

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
                            global.nepalidate = Nepaliyear.ToString()  + nepalimonth.ToString("00")  + nepaliday.ToString("00");

                            date_txt.Text = Nepaliyear.ToString() + "/" + nepalimonth.ToString("00") + "/" + nepaliday.ToString("00");
                            global.todaynepslash = date_txt.Text;
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

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                askBefClose = true; //ask before closing this form
                labelSarkar.Text = null;
                labelMantralay.Text = null;
                labelPradesh.Text = null; ;

                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }

                string[] titles = global.getSingleRowFromTable("SELECT govtname,ministryname,provincename FROM setup_office WHERE isCur=1");
                labelSarkar.Text = titles[0].ToString();
                labelMantralay.Text = titles[1].ToString();
                labelPradesh.Text = titles[2].ToString();

            }
            catch (Exception ex)
            {
                DialogResult dlg = MessageBox.Show("डाटाबेस / नेटवर्क कनेक्सनमा समस्या छ । के तपाईँ डाटाबेसको सेटिङ परिवतन गन चाहनुहुन्छ?", "डाटावेस/ नेटवर्क समस्या", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dlg == DialogResult.Yes)
                {
                    configurationset conf = new configurationset();
                    conf.ShowDialog();
                }
                else
                    Application.Exit();
                
                 //this.Hide();
            }
	try
	{
       // con.Open();
        //MessageBox.Show("Database connected");
       // adtobs();
        getunicodedate();
        date_txt.Text = global.todaynepslash;
        fy();
        csioadd();
        officename.Text = global.csioffice;
        expiry_lbl.Visible = false;
        //string expirydate="20741230";
        string nepdat=global.nepalidate;
//For Expiry Checking
        DateTime startTime = DateTime.Now;
        DateTime expiry = DateTime.Parse(Properties.Settings.Default.exdate.ToString());
        TimeSpan span = expiry.Subtract(startTime);
     //   MessageBox.Show("Expiry Date: " + expiry.ToString() + " Today Date: " + startTime.ToString() + " Subtract Days:" + span.ToString());
        int days = span.Days;
        //   DateTime newDate2 = startTime.AddDays(30);
       // MessageBox.Show("Expiry Remaining Days:" + span.Days);
        if (days < 0)
        {
            MessageBox.Show("Your Applicaion Has Been Expired. Please Contact you Application Provider. Contact: 9864167600 . Email: dreamer_pkr@yahoo.com", "Application Expired");
            expiry_lbl.Visible = true;
            button1.Enabled = false;
            Properties.Settings.Default.exint =  1;
            // MessageBox.Show(Properties.Settings.Default.updatdate.ToString());
            Properties.Settings.Default.Save();
        }
        else if (days <= 5 && days > 0)
        {
            MessageBox.Show("Software is going to expire after " + days.ToString() + " days . Please contact to your application provider. Contact: 9846167600 , Email: dreamer_pkr@yahoo.com", "Application Expiry Notification", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning);
            expiry_lbl.Visible = true;
            expiry_lbl.Text = "Software is going to expire after " + days.ToString() + " days . Contact: 9846167600 ";
        }

   // Expiry Checking End


        //string yr = nepdat.Substring(0, 4);
        //MessageBox.Show(yr.ToString());
        //string mnth = nepdat.Substring(4, 2);
        //string dy = nepdat.Substring(6, 2);
        //MessageBox.Show(yr.ToString() + "--" + mnth.ToString() + "--" + dy.ToString());
        //double expday=int.Parse(expirydate.Substring(0, 4))*365+int.Parse(expirydate.Substring(4, 2))*30+int.Parse(expirydate.Substring(6, 2));
        //double crday=int.Parse(nepdat.Substring(0, 4))*365+int.Parse(nepdat.Substring(4, 2))*30+int.Parse(nepdat.Substring(6, 2));
        //double remain=expday-crday;
      // MessageBox.Show("Remaining"+remain.ToString());
        //int rday=(int.Parse(expirydate.Substring(0, 4))-int.Parse(yr.ToString())*365+(int.Parse(expirydate.Substring(4, 2))-int.Parse(mnth.ToString())*30+
       // MessageBox.Show(Properties.Settings.Default.noofdays.ToString());
        //Properties.Settings.Default.noofdays = 210;
        //Properties.Settings.Default.exdate = 20750330;
        //Properties.Settings.Default.Save();
        //if (int.Parse(Properties.Settings.Default.noofdays.ToString()) <= 0 || int.Parse(Properties.Settings.Default.exdate.ToString()) < int.Parse(global.nepalidate) )
        ////|| Properties.Settings.Default.exdate > Properties.Settings.Default.updatdate)
        //{
        //    button1.Enabled = false;
        //    expiry expfo = new expiry();
        //    this.Hide();
        //    expfo.Show();
        //   //MessageBox.Show("नविकरण गर्नुहोस");
        //} 
        //else if(int.Parse(Properties.Settings.Default.noofdays.ToString()) <= 30)
        //{
        //    expiry expfo = new expiry();
           
        //    this.Hide();
        //    expfo.Show();
        //}
	    
	}

	catch(Exception ex)
	{
       MessageBox.Show(ex.ToString());
	    
        
               //  configurationset conf = new configurationset();
                // this.Hide();
                  //  conf.Show();
     
	}
    

               
           // con.Open();

               // MessageBox.Show("connection state=" + con.State);
           
               // if (con.State != ConnectionState.Open)
               
               // {
                //    MessageBox.Show("Database Connection Error");
                 //   this.Hide();
                 //   configurationset conf = new configurationset();
                 //   conf.Show();
                    
                    
            
               // }
           // }




    sqlcon.con.Close();
           
        }
           
        

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
           ////////////////////////////////////////
             string pass = MD5Hash(textBox2.Text);
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;

            sql = "select userid, status, usertype, office_cat,officeid from login where status='1' and username=@user and password=@passwd ";

           // textBox1.Text = pass.ToString();
           // MessageBox.Show(pass.ToString());
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                command.CommandType = CommandType.Text;

                command.Parameters.AddWithValue("@user", textBox1.Text.Trim());
                command.Parameters.AddWithValue("@passwd", pass.ToString());
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();

                int i = 0;
                    i= ds.Tables[0].Rows.Count;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                global.userid = row[0].ToString();
                global.status=row[1].ToString();
                global.usertype = row[2].ToString();
                global.useroffice_category = row[3].ToString();
                global.user_officeid = row[4].ToString();
                //MessageBox.Show("Status:" + global.status.ToString() + "UserType:" + global.usertype.ToString());
            }
                   // MessageBox.Show(global.status.ToString());
                        //MessageBox.Show("Total Row" + i.ToString());


                //string curdate = DateTime.Today.ToString();
                //SqlCommand qrs = new SqlCommand("select count(*) from industryreg where industrynepname=@firmname");

                //MessageBox.Show(qrs.ToString());
                //    qrs.CommandType = CommandType.Text;

                //    qrs.Parameters.AddWithValue("@firmname", firm_name.Text.Trim());
                //    qrs.Connection = con;


                //    int n = qrs.ExecuteNonQuery();
                //   // reader = qrs.ExecuteReader();
                //   // MessageBox.Show(reader.HasRows.ToString());
                //DataTable dt = new DataTable();



                if (i > 0)
                {

                    global.csioid = Properties.Settings.Default.officeid;
                /////////////////////////////////////////////

                //SqlCommand qr = new SqlCommand("select * from login where status='Active' and username=@user and password=@passwd ");
                ////SqlDataAdapter qry =new SqlDataAdapter("select * from login where status='Active' and username='"+textBox1.Text+"' and password='"+pass.ToString()+"' ", con);

                //qr.CommandType = CommandType.Text;

                //qr.Parameters.AddWithValue("@user", textBox1.Text.Trim());
                //qr.Parameters.AddWithValue("@passwd", pass.ToString());
                //qr.Connection = con;
                //int n = qr.ExecuteNonQuery();
                //MessageBox.Show(n.ToString());
                //if (n>0)
                //{


                //   expcheck();

                Form fc = Application.OpenForms["home"];

                if (fc != null)
                {
                    askBefClose = false; //whether to ask or not before closing this form
                    fc.Focus();
                    this.Close();
                }
                else
                {
                    this.Hide();

                    home hm = new home();
                    hm.getdata(textBox1.Text.ToString());
                    global.username = textBox1.Text;
                    hm.Show();

                    backscreen ift = new backscreen();
                    ift.MdiParent = hm;
                    ift.Show();
                }
            }
            else
            {
                MessageBox.Show("युजरको नाम वा पासवर्ड गलत भयो, फेरी प्रयास गर्नुहोस ।");
               
                textBox2.Text = "";
            }
            sqlcon.con.Close();
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Application.Exit();
           this.Close();
           
            //askBefClose = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            configurationset conf = new configurationset();
            conf.Show();
            this.Hide();
        }

        //to be called from home -- to lock system
        public void LockApp()
        {
            textBox1.Text = global.username;
            textBox1.ReadOnly = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (askBefClose == true && textBox1.ReadOnly)
            {
                if (MessageBox.Show(" यो सूचना प्रणाली सफ्टवेयरलाई प्रयोगकर्ता " + textBox1.Text + " द्वारा लक गरिएको छ । \n " +
                    "यसलाई बन्द गदा यसमा र्खुलिरहेका फर्महरु बन्द हुन्छन र सेभ नभएका डाटा हराउन सक्छन् । \n \n के तपाईँ सफ्टवेयर बन्द गर्न चाहनुहुन्छ ?", "बन्द गर्ने", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    this.FormClosing -= login_FormClosing; //this is required to prevent calling of form closing twice
                    Application.Exit();
                }
                else
                    e.Cancel = true;
            }
        }
    }
}
