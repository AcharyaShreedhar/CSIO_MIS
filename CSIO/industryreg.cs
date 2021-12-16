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
    public partial class industryreg : Form
    {
        public industryreg()
        {
            InitializeComponent();
          
        }
        //public int id; //removed as transid is used for this purpose
        public int dartarep = 0;
        public int datecomp=0;
        public string transid;

        string saveMode = "";
        bool isReady;

        //background colors
        Color backFoc = System.Drawing.Color.FromArgb(230, 240, 255); //Color.Cornsilk;
        Color backNorm = Color.White;
        Color backErr= System.Drawing.Color.FromArgb(255, 210, 220);

        public void firmscale(AutoCompleteStringCollection dataCollection)
        {

          //  //string connetionString = null;
           // //SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
          //  //connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;
            sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (setup_subcategory.category_id = 8)";
           // //connection = new SqlConnection(connetionString);
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

                industry_scale.DataSource = ds.Tables[0];
                industry_scale.ValueMember = "subcategory_id";
                industry_scale.DisplayMember = "subcategory_unicodename";
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
        public void firmkarobar(AutoCompleteStringCollection dataCollection)
        {
            try{
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
            
            MySqlCommand cmd = new MySqlCommand("SELECT DISTINCT   karobar FROM industryreg", sqlcon.con);
           
            MySqlDataReader reader = cmd.ExecuteReader();
          //  AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            while (reader.Read())
            {
                dataCollection.Add(reader.GetString(0));
            }
            reader.Close();
            sqlcon.con.Close();
           


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }

        public void firmIndustryNatureList(AutoCompleteStringCollection dataCollection)
        {
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }

                MySqlCommand cmd = new MySqlCommand("SELECT DISTINCT industry_nature FROM industryreg", sqlcon.con);

                MySqlDataReader reader = cmd.ExecuteReader();
                //  AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                while (reader.Read())
                {
                    dataCollection.Add(reader.GetString(0));
                }
                reader.Close();
                sqlcon.con.Close();



            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }


        public void firmscaleedit(string firmscale)
        {

            ////string connetionString = null;
           // //SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
           // //connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;
            sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE ( subcategory_unicodename = @firmscale)";
           
           // //connection = new SqlConnection(connetionString);
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                ////connection.Open();
                command = new MySqlCommand(sql, sqlcon.con);
                
                adapter.SelectCommand = command;
                adapter.SelectCommand.Parameters.AddWithValue("@firmscale", firmscale.ToString());

                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                sqlcon.con.Close();

                //industry_scale.DataSource = ds.Tables[0];
                //industry_scale.ValueMember = "subcategory_id";
                //industry_scale.DisplayMember = "subcategory_nepname";
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    industry_scale.SelectedValue = row[0].ToString();
                    //citizen_off.Items.Add(row[0].ToString()+" Y "+row[1].ToString());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }
        public void firmtypeedit(string firmtype)
        {

            ////string connetionString = null;
            ////SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            ////connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;
            sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE ( subcategory_unicodename = @firmtype)";
            ////connection = new SqlConnection(connetionString);
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
           
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                      adapter.SelectCommand.Parameters.AddWithValue("@firmtype", firmtype.ToString());
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                sqlcon.con.Close();

                //industry_type.DataSource = ds.Tables[0];
                //industry_type.ValueMember = "subcategory_id";
                //industry_type.DisplayMember = "subcategory_nepname";
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    industry_type.SelectedValue=row[0].ToString();
                    //citizen_off.Items.Add(row[0].ToString()+" Y "+row[1].ToString());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }
        public void comparedate()
        {
            datecomp = 0; 
            if (sqlcon.con.State == ConnectionState.Closed)
                    {
                        sqlcon.con.Open();
                    }
            try
            {
                MySqlDataAdapter qrey = new MySqlDataAdapter("SELECT    Getint(GetUnicodeToNumber(N'" + textRegDate.Text + "')) ", sqlcon.con);
                //  qrey.SelectCommand.Parameters.AddWithValue("@id", cust.ToString());


                DataTable tb = new DataTable();
                qrey.Fill(tb);
                int regdate = int.Parse(tb.Rows[0][0].ToString());
                int todate = int.Parse(entry_nepdate.Text);
                // MessageBox.Show("RegDate:" + regdate.ToString() + " Today Date:" + todate.ToString());
                if (regdate > todate)
                {
                    datecomp = 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
                  sqlcon.con.Close();
        }
        public void renewdate()
        {
            if (textRegDate.MaskFull != false)
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


                    years = (textRegDate.Text).Substring(0, 4);
                    months = (textRegDate.Text).Substring(5, 2);
                    days = (textRegDate.Text).Substring(8, 2);


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
                        textRenewDate.Text = tbs.Rows[0][0].ToString();
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
        public void firmtype(AutoCompleteStringCollection dataCollection)
        {

           // //string connetionString = null;
           // //SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            ////connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;
            sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (setup_subcategory.category_id = 9)";
           // //connection = new SqlConnection(connetionString);
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

                industry_type.DataSource = ds.Tables[0];
                industry_type.ValueMember = "subcategory_id";
                industry_type.DisplayMember = "subcategory_unicodename";
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
        public void dafa1change()
        {

            string firmscal = industry_scale.SelectedValue.ToString();
           // MessageBox.Show("IndustryScale:"+firmscal.ToString());
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
           // connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.passwordname + "'";
            sql = "SELECT     Ain_id FROM ain_dafa  WHERE ( subcategory_id = @firmscale AND cur=1)";

          //  //connection = new SqlConnection(connetionString);
            try
            {

                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                command = new MySqlCommand(sql, sqlcon.con);

                adapter.SelectCommand = command;
               adapter.SelectCommand.Parameters.AddWithValue("@firmscale", firmscal);

                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                sqlcon.con.Close();

                //industry_scale.DataSource = ds.Tables[0];
                //industry_scale.ValueMember = "subcategory_id";
                //industry_scale.DisplayMember = "subcategory_nepname";
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    dafa1.SelectedValue = int.Parse(row[0].ToString());
                    //citizen_off.Items.Add(row[0].ToString()+" Y "+row[1].ToString());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }
        public void checkdartarepeat()
        {
            try
            {
                dartarep = 0;
                MySqlCommand command;
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                DataSet ds = new DataSet();

                string sql = null;

                sql = "select * from industryreg where industryregno=GetUnicodeToNumber(@firmname)";



                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                command.CommandType = CommandType.Text;

                command.Parameters.AddWithValue("@firmname", textRegNum.Text.Trim());
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();

                int i = 0;
                i = ds.Tables[0].Rows.Count;
                
                if (i > 0)
                {
                    dartarep = 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }
        public void dafa2change()
        {

          //  //string connetionString = null;
          //  //SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
           // connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.passwordname + "'";
            sql = "SELECT     Ain_id FROM ain_dafa  WHERE ( subcategory_id = @firmscale AND cur=1)";

        //    //connection = new SqlConnection(connetionString);
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                command = new MySqlCommand(sql, sqlcon.con);

                adapter.SelectCommand = command;
              adapter.SelectCommand.Parameters.AddWithValue("@firmscale", firm_obj.SelectedValue.ToString());

                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                sqlcon.con.Close();

                //industry_scale.DataSource = ds.Tables[0];
                //industry_scale.ValueMember = "subcategory_id";
                //industry_scale.DisplayMember = "subcategory_nepname";
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    dafa2.SelectedValue = int.Parse(row[0].ToString());
                    //citizen_off.Items.Add(row[0].ToString()+" Y "+row[1].ToString());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }
        public void dafa1load(AutoCompleteStringCollection dataCollection)
        {

          //  //string connetionString = null;
           // //SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
           // //connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;
            sql = "SELECT     Ain_id, dafaname FROM ain_dafa  WHERE (dafaid = 1)";
           // //connection = new SqlConnection(connetionString);
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

                dafa1.DataSource = ds.Tables[0];
                dafa1.ValueMember = "Ain_id";
                dafa1.DisplayMember = "dafaname";
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
        public void dafa2load(AutoCompleteStringCollection dataCollection)
        {

            ////string connetionString = null;
            ////SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
           // //connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;
            sql = "SELECT     Ain_id, dafaname FROM ain_dafa  WHERE (dafaid = 2)";
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

                dafa2.DataSource = ds.Tables[0];
                dafa2.ValueMember = "Ain_id";
                dafa2.DisplayMember = "dafaname";
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
        public void firmobjedit(string firmobj)
        {

           // //string connetionString = null;
           // //SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
          //  //connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;
            sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (subcategory_unicodename = @firmobj)";
           // //connection = new SqlConnection(connetionString);
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                ////connection.Open();
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

        public void firmobj(AutoCompleteStringCollection dataCollection)
        {

            ////string connetionString = null;
          //  //SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
           // //connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;
            sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (setup_subcategory.category_id = 10)";
           // //connection = new SqlConnection(connetionString);
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
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
           // darta_txt.Text = null;
            // district_combo.SelectedValue=40;
            textWard.Text = null;
            textTole.Text = null;
            textRenewDate.Text = null;
            textRegDate.Text = global.todaynepslash;
            textFirmName.Text = null;
            textObjective.Text = null;
            firm_turnover.Text = null;
            textBranch.Text = null;
            comment.Text = null;
            textFemaleWorker.Text = null;
            textMaleWorker.Text = null;
            tax_txt.Text = null;
            textFuelWater.Text = null;
            textSthirPuji.Text = null;
            textChaluPuji.Text = null;

            textProductionDate.Text = null;
            textRunningDays.Text = null;
            textIndustryNature.Text = null;
            textProduct_Service.Text = null;
            textFuelWater.Text = null;
            textEnvironment.Text = null;
            textPostalAddress.Text = null;
            textContactNo.Text = null;
            textContactPerson.Text = null;
            comboNiyamawali.Text = null;

            //buttons and panels
            buttonSave.Enabled = false;
            buttonCancel.Enabled = false;
            buttonProprieter.Enabled = false;
            buttonFileScan.Enabled = false;
            buttonDelete.Enabled = false;
            buttonNewIndustry.Enabled = true;
            panelContent.Enabled = false;

            //clear frame
            //remove previous controls
            var controlsToRemove = panelProprieter.Controls.OfType<Label>().ToArray();
            foreach (Control lbl in controlsToRemove)
            {
                if (lbl.Name != "lblOwn1")
                {
                    panelProprieter.Controls.Remove(lbl);
                    lbl.Dispose();
                }
                else
                {
                    lbl.Visible = false;
                }
                //Controls.Remove(control);
                //cntrl.Dispose();
            }

        }
        public void transactionid()
        {
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }

                MySqlCommand cmd = new MySqlCommand("select industryregno, fiscalyear, officeid, officedist from setup_industryregid where industryregid=1", sqlcon.con);
                cmd.CommandType = CommandType.Text;
                // cmd.Connection = con;
                MySqlDataReader sdr = cmd.ExecuteReader();
                sdr.Read();
                transid = global.csioid.ToString().Trim() + global.fyid.ToString().Trim() + sdr["industryregno"].ToString().Trim();

                sdr.Close(); //closing data reader
                sqlcon.con.Close();

               // if (sqlcon.con.State == ConnectionState.Closed)
                sqlcon.con.Open();

                MySqlCommand cmo = new MySqlCommand("update setup_industryregid SET industryregno= industryregno+1 WHERE industryregid=1", sqlcon.con);
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


        //Display Data in DataGridView  
        public void DisplayData(string dartanum="")
        {

            //argument dartanum has been added so that this function can be called from other forms as well
            if (dartanum == "")
                dartanum = textRegNum.Text;


            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }

            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT industryreg.industryid, GetNumberToUnicode(industryreg.industryregno) AS 'दर्ता नं', industryreg.industrynepname AS 'उद्योगको नाम', setup_district.distunicodename AS 'जिल्ला',setup_vdc.vdcunicodename As 'गा.पा।न.पा.',GetNumberToUnicode(industryreg.industryward) As 'वार्ड', industryreg.tole AS 'टोल', industryreg.branch AS 'शाखा',   setup_subcategory.subcategory_unicodename AS 'स्तर', setup_subcategory_2.subcategory_unicodename AS 'कानूनी स्वरूप',   setup_subcategory_1.subcategory_unicodename AS 'वर्ग', GetNumberToUnicode(industryreg.regdat) AS 'दर्ता मिति',  GetNumberToUnicode(industryreg.renewdate) AS 'नविकरण मिति', industryreg.karobar AS 'उद्योगको उद्देश्य', GetNumberToUnicode(industryreg.yearlyturnover) AS 'बार्षिक उत्पादन',  GetNumberToUnicode(industryreg.electricpower) AS 'विधुत शक्ती',GetNumberToUnicode(industryreg.statcapital) AS 'स्थिर पुँजी', GetNumberToUnicode(industryreg.varcapital) AS 'चालु पुँजी', GetNumberToUnicode(industryreg.femaleworker) As 'महिला कामदार', GetNumberToUnicode(industryreg.maleworker) As 'पुरूष कामदार', GetNumberToUnicode(industryreg.tax) As 'राजश्व', industryreg.comment AS 'कैफियत', GetNumberToUnicode(industryreg.techworker) As 'प्राविधिक', GetNumberToUnicode(industryreg.nontechworker) As 'प्रशासनिक', industryreg.rawmaterial As 'कच्चा पदार्थ', GetNumberToUnicode(industryreg.fileno) As 'फाइल नं', GetNumberToUnicode(industryreg.thelino) As 'ठेली', GetNumberToUnicode(industryreg.fiscalyear) As 'आ.व' ,GetNumberToUnicode(industryreg.adhikrit_capital) As 'अधिकृत पुँजी', GetNumberToUnicode(industryreg.jari_capital) As 'जारी पुँजी',ain As 'औद्योगिक ब्य.ऐन',dafa1 As 'दफा उद्योग स्तर',dafa2 As 'दफा उद्योग बर्ग', GetNumberToUnicode(kittanum) As 'कि.नं.',GetNumberToUnicode(rawmaterial_price) As 'कच्चा पदार्थ मुल्य', machine As 'मेशिनरी', GetNumberToUnicode(machine_price) As 'मेशिनरी मुल्य',production_startdate as 'संचालन हुने मिति', runningperyear as 'संचालन हुने दिन',shiftno as 'शिफ्ट',industry_nature as 'उद्योगको प्रकृति', product_servicetype as 'उत्पादन हुने वस्तु/सेवा',fuel_water as 'इन्धन/पानी',environment_affect as 'वातावरणीय प्रभाव',postal_address as 'पत्राचारको ठेगाना', contact_num as 'सम्पर्क नं.', contact_person as 'सम्पर्क व्यक्ति', niyamawali as 'नियमावली'  FROM  industryreg INNER JOIN      setup_district ON industryreg.industrydist = setup_district.distcode INNER JOIN       setup_vdc ON industryreg.industryvdc = setup_vdc.VDC_SID INNER JOIN setup_subcategory ON setup_subcategory.subcategory_id = industryreg.industryscale INNER JOIN      setup_subcategory AS setup_subcategory_2 ON industryreg.industrytype = setup_subcategory_2.subcategory_id INNER JOIN       setup_subcategory AS setup_subcategory_1 ON industryreg.industrycat = setup_subcategory_1.subcategory_id where (industryreg.industryregno=@darta) or (GetNumberToUnicode(industryreg.industryregno)=@darta) or industryreg.industryregno=GetUnicodeToNumber(@darta) ", sqlcon.con);

            qry.SelectCommand.Parameters.AddWithValue("@darta", dartanum);
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView1.DataSource = tb;
            //Getting Columns name

           // MySqlConnection conn = new MySqlConnection(connection);
        //    DataTable dt = new DataTable();
        //    MySqlCommand cmd = new MySqlCommand("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'industryreg'", sqlcon.con);
        //    cmd.Parameters.AddWithValue("@t_name", tablename);
        //    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        //    da.Fill(dt);
        ////    return dt;
        //    List<String> lsColumns = new List<string>();

        //    if (tb.Rows.Count > 0)
        //    {
        //        var count = tb.Rows[0].Table.Columns.Count;

        //        for (int i = 0; i < count; i++)
        //        {
        //            lsColumns.Add(Convert.ToString(tb.Rows[0][i]));
        //            MessageBox.Show(Convert.ToString(tb.Rows[0][i]));
        //        }
        //        MessageBox.Show(lsColumns.ToString());
        //    }
            //if(buttonNewIndustry.Enabled && dataGridView1.Rows.Count==0)
            //{
            // panel2.Enabled = false;
            //}

            sqlcon.con.Close();

            //VISIBILITY OF COLUMNS OF GRID
            int ccc;
            for (ccc = 0; ccc <= dataGridView1.Columns.Count - 1;ccc++ )
            {
                if (ccc > 11)
                {
                    dataGridView1.Columns[ccc].Visible = false;
                }
            }

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[7].Visible = false;


            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            /*
            dataGridView1.Columns[1].Width = 50;
            dataGridView1.Columns[2].Width = 300;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[4].Width = 150;
            dataGridView1.Columns[5].Width = 150;
            */
        }

        public void FillDropDownListvdc(AutoCompleteStringCollection dataCollectionvdcs)
        {

            //string connetionStrings = null;
           // //SqlConnection connections;
            MySqlCommand commands;
            MySqlDataAdapter adapters = new MySqlDataAdapter();
            DataSet dss = new DataSet();

            //int distcodevalue = Convert.ToInt16(district_combo.SelectedValue);
            ////connetionStrings = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";


            string sqls = "SELECT VDC_SID, vdcunicodename FROM  setup_vdc where DistCode = '" + district_combo.SelectedValue + "'";
                //if sabik samet checkbox is NOT checked, then display only Current VDCs
                if (chkOldVDC.Checked == false)
                sqls += " AND isCur=1";


            //sqls = "SELECT VDC_SID,vdcunicodename FROM  setup_vdc where DistCode='" + district_combo.SelectedValue + "'";
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                } 
                ////connections.Open();
                commands = new MySqlCommand(sqls, sqlcon.con);
                adapters.SelectCommand = commands;
                adapters.Fill(dss);
                adapters.Dispose();
                commands.Dispose();
                sqlcon.con.Close();

                vdc_combo.DataSource = dss.Tables[0];
                vdc_combo.ValueMember = "VDC_SID";
                vdc_combo.DisplayMember = "vdcunicodename";

                foreach (DataRow row in dss.Tables[0].Rows)
                {
                    dataCollectionvdcs.Add(row[1].ToString());
                }

            }
            catch 
            {
                //MessageBox.Show("Can not open connection ! inside vdc "+ex);
            }

        }
        public void district(string dist)
        {
           
            //string connetionStrings = null;
            ////SqlConnection connections;
            MySqlCommand commands;
            MySqlDataAdapter adapters = new MySqlDataAdapter();
            DataSet dss = new DataSet();

            string sqls = null;
            //int distcodevalue = Convert.ToInt16(district_combo.SelectedValue);
           // //connetionStrings = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";

            //sqls = "SELECT VdcCode,VdcNepnam FROM  setup_vdc where DistCode='" +district_combo.SelectedValue+ "'";
            sqls = " SELECT     distcode, distunicodename FROM         setup_district WHERE     (distunicodename = @district)";
            
           // //connections = new SqlConnection(connetionStrings);

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
                commands = new MySqlCommand(sqls, sqlcon.con);
                adapters.SelectCommand = commands;
                adapters.SelectCommand.Parameters.AddWithValue("@district", dist.ToString());
                adapters.Fill(dss);
                adapters.Dispose();
                commands.Dispose();
                sqlcon.con.Close();


                foreach (DataRow row in dss.Tables[0].Rows)
                {
                    
                   district_combo.SelectedValue= row[0].ToString();
                }
            
        }
        public void vdcedit(string vdc)
        {

           // string connetionStrings = null;
           // //SqlConnection connections;
            MySqlCommand commands;
            MySqlDataAdapter adapters = new MySqlDataAdapter();
            DataSet dss = new DataSet();

            string sqls = null;
            //int distcodevalue = Convert.ToInt16(district_combo.SelectedValue);
            ////connetionStrings = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";

            //sqls = "SELECT VdcCode,VdcNepnam FROM  setup_vdc where DistCode='" +district_combo.SelectedValue+ "'";
            sqls = "SELECT VDC_SID,vdcunicodename FROM  setup_vdc where vdcunicodename=@vdc and DistCode='" + district_combo.SelectedValue + "'";


           // //connections = new SqlConnection(connetionStrings);
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
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
        public void dafa2loading()
        {
            // MessageBox.Show("Working");
            // //string connetionString = null;
            // //SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            //connetionString = mysqlcon.conn.ToString();
            sql = "SELECT Ain_id, dafaname FROM ain_dafa WHERE dafaid = 2 AND ain='" + ain_drop.SelectedValue + "' ";
            //connection = new MySqlConnection(mysqlcon.conn);
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
              //  adapter.SelectCommand.Parameters.AddWithValue("@ain", ain_drop.SelectedValue.ToString());
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();


                dafa2.DataSource = ds.Tables[0];
                dafa2.ValueMember = "Ain_id";
                dafa2.DisplayMember = "dafaname";
                // foreach (DataRow row in ds.Tables[0].Rows)
                // {
                //MessageBox.Show("Working"+usertype.ValueMember.ToString());
                //    usertype.Items.Add(row[1].ToString());

                // data.Add(row[1].ToString());
                //citizen_off.Items.Add(row[0].ToString()+" Y "+row[1].ToString());
                //  }
                sqlcon.con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }
        public void ainloading()
        {
            // MessageBox.Show("Working");
            // //string connetionString = null;
            // //SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            //connetionString = mysqlcon.conn.ToString();
            sql = "SELECT ainid, ainname FROM setup_ain ";
            //connection = new MySqlConnection(mysqlcon.conn);
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


                ain_drop.DataSource = ds.Tables[0];
                ain_drop.ValueMember = "ainid";
                ain_drop.DisplayMember = "ainname";
                // foreach (DataRow row in ds.Tables[0].Rows)
                // {
                //MessageBox.Show("Working"+usertype.ValueMember.ToString());
                //    usertype.Items.Add(row[1].ToString());

                // data.Add(row[1].ToString());
                //citizen_off.Items.Add(row[0].ToString()+" Y "+row[1].ToString());
                //  }
                sqlcon.con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }
        public void dafa1loading()
        {
            // MessageBox.Show("Working");
            // //string connetionString = null;
            // //SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();
          //  MessageBox.Show(ain_drop.SelectedValue.ToString());
            string sql = null;
            //connetionString = mysqlcon.conn.ToString();
            sql = "SELECT Ain_id, dafaname FROM ain_dafa WHERE dafaid=1 AND ain='" + ain_drop.SelectedValue + "'";
            //connection = new MySqlConnection(mysqlcon.conn);
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
               // adapter.SelectCommand.Parameters.AddWithValue("@ain", ain_drop.SelectedValue.ToString());
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();


                dafa1.DataSource = ds.Tables[0];
                dafa1.ValueMember = "Ain_id";
                dafa1.DisplayMember = "dafaname";
                // foreach (DataRow row in ds.Tables[0].Rows)
                // {
                //MessageBox.Show("Working"+usertype.ValueMember.ToString());
                //    usertype.Items.Add(row[1].ToString());

                // data.Add(row[1].ToString());
                //citizen_off.Items.Add(row[0].ToString()+" Y "+row[1].ToString());
                //  }
                sqlcon.con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }


        public void FillTheliLuz()
        {

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            ////connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;

            //sql = "SELECT     theli_id, theli_no FROM         setup_theli INNER JOIN  setup_subcategory ON setup_theli.subcategory_id = setup_subcategory.subcategory_id INNER JOIN    setup_category ON setup_subcategory.category_id = setup_category.category_id WHERE     (setup_theli.subcategory_name = '" + theli_combo.SelectedValue + "')";
            sql = "SELECT     theli_id, GetNumberToUnicode(theli_no) As theli_no FROM setup_theli INNER JOIN  setup_subcategory ON setup_theli.subcategory_id = setup_subcategory.subcategory_id INNER JOIN    setup_category ON setup_subcategory.category_id = setup_category.category_id WHERE     (setup_theli.subcategory_id = 129) order by theli_no desc ";

            //  //connection = new SqlConnection(connetionString);
            try
            {
                // //connection.Open();
                //  MySqlCommand command;
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
            ////string connetionString = null;
            // //SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            // //connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;
            sql = "SELECT  FY_ID, GetNumberToUnicode(format_fiscal_yr(FY)) As fiscal FROM setup_fy order by FY_ID desc";
            ////connection = new SqlConnection(connetionString);
            try
            {
                ////connection.Open();
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                sqlcon.con.Close();

                fy_txt.DataSource = ds.Tables[0];
                fy_txt.ValueMember = "FY_ID";
                fy_txt.DisplayMember = "fiscal";


                //foreach (DataRow row in ds.Tables[0].Rows)
                // {
                //dataCollection.Add(row[1].ToString());
                //citizen_off.Items.Add(row[0].ToString()+" Y "+row[1].ToString());
                //}

                //int myFYID=global.getSingleDataFromTable("SELECT FYID FROM setup_fy WHERE ")

                fy_txt.Text = global.fy;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }

        private void banijyareg_Load(object sender, EventArgs e)
        {


            //if (sqlcon.con.State == ConnectionState.Closed)
            //    sqlcon.con.Open();
            //MySqlDataReader rd = new MySqlDataReader;


            //MessageBox.Show(sqlcon.con.com)

            isReady = false;
            this.KeyPreview = true; //for keydown
            ainloading();
            ain_drop.SelectedValue = 1;
            dafa1loading();
            dafa2loading();

            //district_txt.ReadOnly = true;
            //theli_no.ReadOnly = true;

            FillTheliLuz();

            fiscalyear();

            usr.Text = global.username;
            textRegDate.Text = global.todaynepslash;
            entry_nepdate.Text = global.nepalidate;
            //DisplayData();
            //renewdate();
            buttonProprieter.Enabled = false;
            buttonDelete.Enabled = false;
            buttonSave.Enabled = false;
            buttonFileScan.Enabled = false;
            buttonCancel.Enabled = buttonSave.Enabled;

            //save_btn.Enabled = false;
            //district_combo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //district_combo.AutoCompleteSource = AutoCompleteSource.CustomSource;

            //AutoCompleteStringCollection combData = new AutoCompleteStringCollection();
            //FillDropDownList(combData);
            //district_combo.AutoCompleteCustomSource = combData;
            //district_combo.SelectedValue = global.csiodist;
            // DisplayData();

            //firm_obj.AutoCompleteMode = AutoCompleteMode.Append;
            //firm_obj.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection firmo = new AutoCompleteStringCollection();
            firmobj(firmo);
            firm_obj.AutoCompleteCustomSource = firmo;
            firm_obj.SelectedIndex = 0;


            //industry_scale.AutoCompleteMode = AutoCompleteMode.Append;
            //industry_scale.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection firmc = new AutoCompleteStringCollection();
            firmscale(firmc);
            industry_scale.AutoCompleteCustomSource = firmc;
            industry_scale.SelectedIndex = 0;

            //industry_type.AutoCompleteMode = AutoCompleteMode.Append;
            //industry_type.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection firmt = new AutoCompleteStringCollection();
            firmtype(firmt);
            industry_type.AutoCompleteCustomSource = firmt;
            industry_type.SelectedIndex = 0;
            textAdhikritPuji.Enabled = false;
            textJariPuji.Enabled = false;

            //dafa1.AutoCompleteMode = AutoCompleteMode.Append;
            //dafa1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //AutoCompleteStringCollection d1 = new AutoCompleteStringCollection();
            //dafa1load(d1);
            //dafa1.AutoCompleteCustomSource = d1;

           // vdc_combo.SelectedValue = 4260;
            //dafa2.AutoCompleteMode = AutoCompleteMode.Append;
            //dafa2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //AutoCompleteStringCollection d2 = new AutoCompleteStringCollection();
            //dafa2load(d2);
            //dafa2.AutoCompleteCustomSource = d2;
            textObjective.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textObjective.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection karobar = new AutoCompleteStringCollection();
            firmkarobar(karobar);
            textObjective.AutoCompleteCustomSource = karobar;


            textIndustryNature.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textIndustryNature.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection firmNature = new AutoCompleteStringCollection();
            firmIndustryNatureList(firmNature);
            textIndustryNature.AutoCompleteCustomSource = firmNature;

            //this.Height=this.inn


            //PROVINCE
            global.fillCombo(comboProvince, "SELECT provinceid, provincenep From setup_province", "provincenep", "provinceid");
            isReady = true;
            comboProvince.SelectedValue = global.myProvinceId;
            comboProvince.Enabled = false;

            global.fillCombo (district_combo, "SELECT distcode, distunicodename FROM setup_district WHERE distzonecd=" + comboProvince.SelectedValue, "distunicodename", "distcode");
            district_combo.SelectedValue = global.csiodist;
            district_combo.Enabled = false;

            //NIYAMAWALI
            global.fillCombo(comboNiyamawali, "SELECT dartaniyamawali FROM setup_indniyamawali", "dartaniyamawali", "dartaniyamawali");

            this.WindowState = FormWindowState.Normal;
            //POSITION and SIZE
            this.Left = (this.Parent.Width / 2) - (this.Width / 2) - 5;
            this.Top = 0;

            Rectangle recNew = new Rectangle();
            recNew = this.Parent.ClientRectangle;
            recNew.Height -= 4;
            recNew.Width -= 4;
            //this.Size = recNew.Size;
            this.Height = recNew.Height;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            saveMode = "add";
            //DisplayData(); -- no need to call this

            if (sqlcon.con.State == ConnectionState.Closed)
            sqlcon.con.Open();

            //string curdate = DateTime.Today.ToString();
            MySqlCommand qr = new MySqlCommand("select count(*) from industryreg where GetUnicodeToNumber(industryregno)= @darta_no");
            qr.CommandType = CommandType.Text;
            qr.Parameters.AddWithValue("@darta_no", global.convertUnicodeToNum(textRegNum.Text.Trim()));  
            qr.Connection = sqlcon.con;
            MySqlDataReader reader = qr.ExecuteReader();
            reader.Read();
           int n =Convert.ToInt32(reader[0].ToString());
            //int n = reader.RecordsAffected;
            reader.Close(); //closing data reader

            if (n>0)
            {
                DialogResult result1 = MessageBox.Show( "दर्ता नं "+ global.convertUnicodeToNum(textRegNum.Text.Trim()) +" भएको उद्योग पहिले नै इन्ट्री भइ सकेको छ । \n \n के तपाईँ सो दता नं. मा फेरि अर्को उद्योगको विवरण इन्ट्री गर्न चाहनुहुन्छ?", "उद्योग दर्ता", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (result1 == DialogResult.Yes)
                {
                    cleardata();
                    buttonNewIndustry.Enabled = false;
                    chkOldVDC.Checked = false; //new VDCs Muns Only
                    //buttonUpdate.Enabled = false;
                    //save_btn.Enabled = true;
                    buttonSave.Enabled = true;
                    buttonCancel.Enabled = buttonSave.Enabled;
                    panelContent.Enabled = true;
                    textFirmName.Focus();
                }
                else
                {
                    textRegNum.SelectAll();
                    textRegNum.Focus();
                }
            }
            else
            {
                cleardata();
                buttonNewIndustry.Enabled = false;
                chkOldVDC.Checked = false; //new VDCs Muns Only
                transactionid(); //to create transid requried to open More detail form -- moved from save button to here
                //buttonUpdate.Enabled = false;
                //save_btn.Enabled = true;
                buttonSave.Enabled = true;
                buttonCancel.Enabled = buttonSave.Enabled;
                buttonProprieter.Enabled = true;
                panelContent.Enabled = true;
            }

            sqlcon.con.Close(); 
        }

        private void district_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            vdc_combo.Text = null;
            vdc_combo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            vdc_combo.AutoCompleteSource = AutoCompleteSource.CustomSource;

            AutoCompleteStringCollection combDataa = new AutoCompleteStringCollection();
            FillDropDownListvdc(combDataa);
            vdc_combo.AutoCompleteCustomSource = combDataa;
        }

        private void SaveNewRecord()
        { 
            checkdartarepeat();
            comparedate();
            if (datecomp < 1)
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }

                DialogResult result1 = MessageBox.Show("के तपाईं नयाँ उद्योगको विवरण दर्ता गर्न चाहनुहुन्छ ?",
          "नयाँ उद्योग दर्ता", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result1 == DialogResult.Yes)
                {
                    //string curdate = DateTime.Today.ToString();
                    // SqlDataReader reader = null;
                    if (textAdhikritPuji.Text == "")
                    {
                        textAdhikritPuji.Text = "0";
                    }
                    if (textJariPuji.Text == "")
                    {
                        textJariPuji.Text = "0";
                    }
                    try
                    {
                        MySqlCommand command;
                        MySqlDataAdapter adapter = new MySqlDataAdapter();
                        DataSet ds = new DataSet();

                        string sql = null;

                        sql = "select * from industryreg where industrynepname=@firmname";

                        command = new MySqlCommand(sql, sqlcon.con);
                        adapter.SelectCommand = command;
                        command.CommandType = CommandType.Text;

                        command.Parameters.AddWithValue("@firmname", textFirmName.Text.Trim());
                        adapter.Fill(ds);
                        adapter.Dispose();
                        command.Dispose();

                        int i = 0;
                        i = ds.Tables[0].Rows.Count;
                     

                        if (i > 0)
                        {
                            MessageBox.Show("'"+textFirmName.Text.Trim()+"' नामको उद्योग पहिले नै इन्ट्री भइ सकेकोले कृपया अर्को नाममा इन्ट्री गर्नुहोस ।", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            if (dartarep == 1)
                            {
                                MessageBox.Show("'" + textRegNum.Text.Trim() + "' दर्ता नं भएको उद्योग पहिले नै इन्ट्री भइ सकेकोले कृपया अर्को दर्ता नं इन्ट्री गर्नुहोस ।", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            }
                            else{
                           // transactionid(); moved to add new
                            MySqlCommand qr = new MySqlCommand("INSERT INTO industryreg(industryid,industryregno,regdat,renewdate,industryscale,industrytype,industrycat,industrynepname,branch,industrydist,industryvdc,industryward,tole,karobar,yearlyturnover,electricpower,statcapital,varcapital,comment,usr,updatnepdate,femaleworker,maleworker,tax,techworker,nontechworker,rawmaterial,thelino,fileno,fiscalyear,adhikrit_capital,jari_capital,ain,dafa1,dafa2, kittanum, rawmaterial_price, machine, machine_price, regnepdate, officedist, csioid, production_startdate, runningperyear, shiftno, industry_nature, product_servicetype, fuel_water, environment_affect, postal_address, contact_num, contact_person, niyamawali )  VALUES (@industryid,GetUnicodeToNumber(@darta), GetUnicodeToNumber(@dartadate), GetUnicodeToNumber(@renewdate), @industryscale, @industrytype, @firmobj, @firmname, @branch, @district, @vdc, GetUnicodeToNumber(@ward), @tole ,@karobar, GetUnicodeToNumber(@turnover), GetUnicodeToNumber(@electric), GetUnicodeToNumber(@txtbxthree), GetUnicodeToNumber(@txtbxfour), @comment, @user,  GetUnicodeToNumber(@nepdate), GetUnicodeToNumber(@fw),GetUnicodeToNumber(@mw), GetUnicodeToNumber(@tx), GetUnicodeToNumber(@techworker), GetUnicodeToNumber(@nontechworker), @rawmaterial,GetUnicodeToNumber(@theli), GetUnicodeToNumber(@file), GetUnicodeToNumber(@fy),GetUnicodeToNumber(@adhikritpuji),GetUnicodeToNumber(@jaripuji),@ain,@dafa1,@dafa2,@kitta,GetUnicodeToNumber(@rawprice),@machine,GetUnicodeToNumber(@machineprice),Getint(GetUnicodeToNumber(@regnepdate)),@officedist, @csioid,@production_startdate, @runningperyear, @shiftno, @industry_nature, @product_servicetype, @fuel_water, @environment_affect, @postal_address, @contact_num, @contact_person,@niyamawali)");

                                // production_startdate as 'संचालन हुने मिति', runningperyear as 'संचालन हुने दिन',shiftno as 'शिफ्ट',industry_nature as 'उद्योगको प्रकृति', product_servicetype as 'उत्पादन हुने वस्तु/सेवा',fuel_water as 'इन्धन/पानी',environment_effect as 'वातावरणीय प्रभाव',postal_address as 'पत्राचारको ठेगाना', contact_num as 'सम्पर्क नं.', contact_person as 'सम्पर्क व्यक्ति', niyamawali as 'नियमावली'
                                //@production_startdate, @runningperyear, @shiftno, @industry_nature, @product_servicetype, @fuel_water, @environment_effect, @postal_address, @contact_num, @contact_person
                                //production_startdate, runningperyear, shiftno, industry_nature, product_servicetype, fuel_water, environment_effect, postal_address, contact_num, contact_person


                                qr.CommandType = CommandType.Text;
                            qr.Parameters.AddWithValue("@industryid", transid.ToString());
                            qr.Parameters.AddWithValue("@darta", textRegNum.Text);
                            qr.Parameters.AddWithValue("@dartadate", textRegDate.Text);
                            qr.Parameters.AddWithValue("@regnepdate", textRegDate.Text);
                            qr.Parameters.AddWithValue("@renewdate", textRenewDate.Text);
                            qr.Parameters.AddWithValue("@industryscale", industry_scale.SelectedValue);
                            qr.Parameters.AddWithValue("@industrytype", industry_type.SelectedValue);
                            qr.Parameters.AddWithValue("@firmobj", firm_obj.SelectedValue);
                            qr.Parameters.AddWithValue("@firmname", textFirmName.Text);
                            qr.Parameters.AddWithValue("@branch", textBranch.Text);
                            qr.Parameters.AddWithValue("@district", district_combo.SelectedValue);
                            qr.Parameters.AddWithValue("@vdc", vdc_combo.SelectedValue);
                            qr.Parameters.AddWithValue("@ward", textBox1.Text);
                            qr.Parameters.AddWithValue("@tole", textTole.Text);
                            qr.Parameters.AddWithValue("@karobar", textObjective.Text);
                            qr.Parameters.AddWithValue("@turnover", firm_turnover.Text);

                            qr.Parameters.AddWithValue("@electric", textElectricPower.Text);

                            qr.Parameters.AddWithValue("@txtbxthree", textSthirPuji.Text);
                            qr.Parameters.AddWithValue("@txtbxfour", textChaluPuji.Text);
                            qr.Parameters.AddWithValue("@comment", comment.Text);
                            qr.Parameters.AddWithValue("@fw", textFemaleWorker.Text);
                            qr.Parameters.AddWithValue("@mw", textMaleWorker.Text);
                            qr.Parameters.AddWithValue("@tx", tax_txt.Text);
                            qr.Parameters.AddWithValue("@user", usr.Text);
                            //qr.Parameters.AddWithValue("@currentdate", curdate);
                            qr.Parameters.AddWithValue("@nepdate", entry_nepdate.Text);
                            qr.Parameters.AddWithValue("@techworker", textTechWorker.Text);
                            qr.Parameters.AddWithValue("@nontechworker", textNonTechWorker.Text);
                            qr.Parameters.AddWithValue("@rawmaterial", textRawMaterial.Text);
                            qr.Parameters.AddWithValue("@theli", theli_no.Text);
                            qr.Parameters.AddWithValue("@file", textFileNum.Text);
                            qr.Parameters.AddWithValue("@fy", textWard.Text);
                            qr.Parameters.AddWithValue("@adhikritpuji", textAdhikritPuji.Text);
                            qr.Parameters.AddWithValue("@jaripuji", textJariPuji.Text);
                            qr.Parameters.AddWithValue("@ain", ain_drop.Text);
                            qr.Parameters.AddWithValue("@dafa1", dafa1.Text);
                            qr.Parameters.AddWithValue("@dafa2", dafa2.Text);
                            qr.Parameters.AddWithValue("@kitta", textKitta.Text);
                            qr.Parameters.AddWithValue("@rawprice", text_raw_price.Text);
                            qr.Parameters.AddWithValue("@machine", textMachinery.Text);
                            qr.Parameters.AddWithValue("@machineprice", textMachinePrice.Text);
                            qr.Parameters.AddWithValue("@csioid", global.csioid);
                            qr.Parameters.AddWithValue("@officedist", global.csiodist);


                                //@production_startdate, @runningperyear, @shiftno, @industry_nature, @product_servicetype, @fuel_water, @environment_effect, @postal_address, @contact_num, @contact_person
                                //production_startdate, runningperyear, shiftno, industry_nature, product_servicetype, fuel_water, environment_effect, postal_address, contact_num, contact_person

                                qr.Parameters.AddWithValue("@production_startdate",textProductionDate.Text);
                                qr.Parameters.AddWithValue("@runningperyear",textRunningDays.Text);
                                qr.Parameters.AddWithValue("@shiftno",comboShift.Text );
                                qr.Parameters.AddWithValue("@industry_nature",textIndustryNature.Text);
                                qr.Parameters.AddWithValue("@product_servicetype",textProduct_Service.Text);
                                qr.Parameters.AddWithValue("@fuel_water",textFuelWater.Text);
                                qr.Parameters.AddWithValue("@environment_affect",textEnvironment.Text);
                                qr.Parameters.AddWithValue("@postal_address",textPostalAddress.Text);
                                qr.Parameters.AddWithValue("@contact_num",textContactNo.Text);
                                qr.Parameters.AddWithValue("@contact_person",textContactPerson.Text);
                                qr.Parameters.AddWithValue("@niyamawali",comboNiyamawali.Text);

                                qr.Connection = sqlcon.con;
                            
                                int k = qr.ExecuteNonQuery();

                                if (k > 0)
                                {
                                    global.sync_dblog("industryreg", transid.ToString(), "INSERT", transid.ToString(), "industryid");
                                   
                                    sqlcon.con.Close();
                                    dataGridView1.DataSource = null;
                                    DisplayData();
                                    cleardata();
                                    //save_btn.Enabled = false;
                                    //buttonSave.Enabled = false;
                                    //buttonCancel.Enabled = buttonSave.Enabled;
                                    //buttonNewIndustry.Enabled = true;
                                    MessageBox.Show("नयाँ उद्योगको विवरण सफलतापुर्वक दर्ता गरियो ।", "नयाँ उद्योग दर्ता", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                }

                            
                            }





                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
//=======
//                    if (i > 0)
//                    {
//                        MessageBox.Show("उद्योगको नाम पहिले नै इन्ट्री भइ सकेको ले कृपया अर्को नाम इन्ट्री गर्नुहोस ।", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
//                    }
//                    else
//                    {
//                        transactionid();
//                        MySqlCommand qr = new MySqlCommand("INSERT INTO industryreg(industryid,industryregno,regdat,renewdate,industryscale,industrytype,industrycat,industrynepname,branch,industrydist,industryvdc,industryward,tole,karobar,yearlyturnover,electricpower,statcapital,varcapital,comment,usr,updatnepdate,femaleworker,maleworker,tax,techworker,nontechworker,rawmaterial,thelino,fileno,fiscalyear,adhikrit_capital,jari_capital,ain,dafa1,dafa2, kittanum,rawmaterial_price,machine,machine_price,regnepdate,csioid)  VALUES (@industryid,GetUnicodeToNumber(@darta), GetUnicodeToNumber(@dartadate), GetUnicodeToNumber(@renewdate), @industryscale, @industrytype, @firmobj, @firmname, @branch, @district, @vdc, GetUnicodeToNumber(@ward), @tole ,@karobar, GetUnicodeToNumber(@turnover), GetUnicodeToNumber(@electric), GetUnicodeToNumber(@txtbxthree), GetUnicodeToNumber(@txtbxfour), @comment, @user,  GetUnicodeToNumber(@nepdate), GetUnicodeToNumber(@fw),GetUnicodeToNumber(@mw), GetUnicodeToNumber(@tx), GetUnicodeToNumber(@techworker), GetUnicodeToNumber(@nontechworker), @rawmaterial,GetUnicodeToNumber(@theli), GetUnicodeToNumber(@file), GetUnicodeToNumber(@fy),GetUnicodeToNumber(@adhikritpuji),GetUnicodeToNumber(@jaripuji),@ain,@dafa1,@dafa2,@kitta,GetUnicodeToNumber(@rawprice),@machine,GetUnicodeToNumber(@machineprice),Getint(GetUnicodeToNumber(@regnepdate)),@csioid)");

//                        qr.CommandType = CommandType.Text;
//                        qr.Parameters.AddWithValue("@industryid", transid.ToString());
//                        qr.Parameters.AddWithValue("@darta", darta_txt.Text);
//                        qr.Parameters.AddWithValue("@dartadate", darta_date.Text);
//                        qr.Parameters.AddWithValue("@regnepdate", darta_date.Text);
//                        qr.Parameters.AddWithValue("@renewdate", renew_date.Text);
//                        qr.Parameters.AddWithValue("@industryscale", industry_scale.SelectedValue);
//                        qr.Parameters.AddWithValue("@industrytype", industry_type.SelectedValue);
//                        qr.Parameters.AddWithValue("@firmobj", firm_obj.SelectedValue);
//                        qr.Parameters.AddWithValue("@firmname", firm_name.Text);
//                        qr.Parameters.AddWithValue("@branch", branch_txt.Text);
//                        qr.Parameters.AddWithValue("@district", district_combo.SelectedValue);
//                        qr.Parameters.AddWithValue("@vdc", vdc_combo.SelectedValue);
//                        qr.Parameters.AddWithValue("@ward", ward_txt.Text);
//                        qr.Parameters.AddWithValue("@tole", tole_txt.Text);
//                        qr.Parameters.AddWithValue("@karobar", firm_karobar.Text);
//                        qr.Parameters.AddWithValue("@turnover", firm_turnover.Text);
//                        qr.Parameters.AddWithValue("@electric", textBox2.Text);
//                        qr.Parameters.AddWithValue("@txtbxthree", textBox3.Text);
//                        qr.Parameters.AddWithValue("@txtbxfour", textBox4.Text);
//                        qr.Parameters.AddWithValue("@comment", comment.Text);
//                        qr.Parameters.AddWithValue("@fw", fworker.Text);
//                        qr.Parameters.AddWithValue("@mw", maleworker.Text);
//                        qr.Parameters.AddWithValue("@tx", tax_txt.Text);
//                        qr.Parameters.AddWithValue("@user", usr.Text);
//                        //qr.Parameters.AddWithValue("@currentdate", curdate);
//                        qr.Parameters.AddWithValue("@nepdate", entry_nepdate.Text);
//                        qr.Parameters.AddWithValue("@techworker", tech_worker.Text);
//                        qr.Parameters.AddWithValue("@nontechworker", non_techworker.Text);
//                        qr.Parameters.AddWithValue("@rawmaterial", raw_material.Text);
//                        qr.Parameters.AddWithValue("@theli", theli_no.Text);
//                        qr.Parameters.AddWithValue("@file", district_txt.Text);
//                        qr.Parameters.AddWithValue("@fy", fy_txt.Text);
//                        qr.Parameters.AddWithValue("@adhikritpuji", adhikrit_puji.Text);
//                        qr.Parameters.AddWithValue("@jaripuji", jari_puji.Text);
//                        qr.Parameters.AddWithValue("@ain", ain_drop.Text);
//                        qr.Parameters.AddWithValue("@dafa1", dafa1.Text);
//                        qr.Parameters.AddWithValue("@dafa2", dafa2.Text);
//                        //qr.Parameters.AddWithValue("@kitta", kinam.Text);
//                        //qr.Parameters.AddWithValue("@rawprice", raw_price.Text);
//                        //qr.Parameters.AddWithValue("@dafa2", dafa2.Text);
//                        qr.Parameters.AddWithValue("@kitta", kinam.Text);
//                        qr.Parameters.AddWithValue("@rawprice", raw_price.Text);
//                        qr.Parameters.AddWithValue("@machine", machine.Text);
//                        qr.Parameters.AddWithValue("@machineprice", machine_price.Text);
//                        qr.Parameters.AddWithValue("@csioid", global.csioid);
//                        qr.Connection = sqlcon.con;

//                        int k = qr.ExecuteNonQuery();

//                        if (k > 0)
//                        {
//                            sqlcon.con.Close();
//                            dataGridView1.DataSource = null;
//                            DisplayData();
//                            cleardata();
//                            MessageBox.Show("रेकर्ड इन्ट्री सफल भयो ।", " ", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
//                        }
//>>>>>>> Stashed changes
                    }
                }
            }
            else
            {
                MessageBox.Show("दर्ता मिति मिलेन, पुन सच्याई प्रयास गर्नुहोला ।");
                textRegDate.Focus();

            }
        }
   
        

       
        public void getindustrytype(string firmtype)
        {
            try{

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
        private void DisplayIndustryOwner()
        {
            String industnam = textFirmName.Text;
            String regdat = textRegDate.Text;
            industrynaasari ownrindst = new industrynaasari();
            //ownrindst.MdiParent = this.MdiParent;
            ownrindst.getdataa(textRegNum.Text, regdat.ToString(), transid, industry_type.SelectedValue.ToString(), industnam.ToString());
            //string upid = "132";
            ownrindst.myparent = this;
            ownrindst.Show();
        }

        private void textBox4_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            //base.OnKeyPress(e);
            //// Check if the pressed character was a backspace or numeric.
            //if (e.KeyChar != (char)8 && !char.IsNumber(e.KeyChar))
            //{
            //    e.Handled = true;
            //}
        }

        private void textBox4_TabIndexChanged(object sender, EventArgs e)
        {
            //double a = Convert.ToDouble(textBox4.Text);
            //double b = Convert.ToDouble(textBox4.Text);
            //textBox5.Text = (a + b).ToString();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        //    double a = 0;
        //    double b = 0;
        //    if (textBox3.Text != "")
        //    {
        //        a = Convert.ToDouble(textBox3.Text);
        //    }
        //    if (textBox4.Text != "")
        //    {
        //        b = Convert.ToDouble(textBox4.Text);
        //    }
        //    textBox5.Text = (a + b).ToString();
        }


        public void DisplayIndividualData(string indid = "")
        {
            //this argument and the following code is added in case the form is opened from other forms
            if (indid != "")
            {
                dataGridView1.ClearSelection();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[0].Value.ToString() == indid)
                        dataGridView1.CurrentCell = row.Cells[1];
                }
            }
            saveMode = "edit";

            //buttonUpdate.Enabled = true;
            buttonSave.Enabled = true;
            buttonCancel.Enabled = buttonSave.Enabled;
            panelContent.Enabled = true;
            buttonDelete.Enabled = true;
            buttonFileScan.Enabled = true;
            buttonProprieter.Enabled = true;

            int i;
            // string distcode=null;
            //district_combo.SelectedText=null;
            //vdc_combo.SelectedText = " ";
            //industry_scale.SelectedText = " ";
            //industry_type.SelectedText = " ";
            //firm_obj.SelectedText = " ";
            //textBox3.Text = " ";
            //textBox4.Text = " ";

            chkOldVDC.Checked = true; //display old/sabik VDC mun also
                                      //chkOldVDC.Enabled = false;

            i = dataGridView1.CurrentRow.Index;
            DataGridViewRow rw = dataGridView1.Rows[i];

            transid = dataGridView1.Rows[i].Cells[0].Value.ToString();

            textRegNum.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            textFirmName.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            district(dataGridView1.Rows[i].Cells[3].Value.ToString());
            vdcedit(dataGridView1.Rows[i].Cells[4].Value.ToString());
            if (vdc_combo.Items.Count > 0)
            { 
                string VDCid = vdc_combo.SelectedValue.ToString();
                string isCur = global.getSingleDataFromTable("SELECT isCur from setup_vdc WHERE VDC_SID=" + VDCid);
                if (isCur == "1")
                    chkOldVDC.Checked = false;
                else
                    chkOldVDC.Checked = true;

                vdc_combo.SelectedValue = VDCid;
        }
            //MessageBox.Show(vdc_combo.SelectedValue.ToString() + ' ' + vdc_combo.SelectedText.ToString());

            textWard.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
            textTole.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();
            textBranch.Text = dataGridView1.Rows[i].Cells[7].Value.ToString();

            firmscaleedit(dataGridView1.Rows[i].Cells[8].Value.ToString());
            //industry_scale.SelectedText = dataGridView1.Rows[i].Cells[8].Value.ToString();
            firmtypeedit(dataGridView1.Rows[i].Cells[9].Value.ToString());
            //industry_type.SelectedText = dataGridView1.Rows[i].Cells[9].Value.ToString();
            firmobjedit(dataGridView1.Rows[i].Cells[10].Value.ToString());
            //firm_obj.SelectedText = dataGridView1.Rows[i].Cells[10].Value.ToString();
            textRegDate.Text = dataGridView1.Rows[i].Cells[11].Value.ToString();
            textRenewDate.Text = dataGridView1.Rows[i].Cells[12].Value.ToString();
            textObjective.Text = dataGridView1.Rows[i].Cells[13].Value.ToString();
            firm_turnover.Text = dataGridView1.Rows[i].Cells[14].Value.ToString();
            textFuelWater.Text = dataGridView1.Rows[i].Cells[15].Value.ToString();
           // firm_turnover.Text = dataGridView1.Rows[i].Cells[15].Value.ToString();
            textSthirPuji.Text = dataGridView1.Rows[i].Cells[16].Value.ToString();
            textChaluPuji.Text = dataGridView1.Rows[i].Cells[17].Value.ToString();
            textFemaleWorker.Text = dataGridView1.Rows[i].Cells[18].Value.ToString();
            textMaleWorker.Text = dataGridView1.Rows[i].Cells[19].Value.ToString();
            tax_txt.Text = dataGridView1.Rows[i].Cells[20].Value.ToString();
            comment.Text = dataGridView1.Rows[i].Cells[21].Value.ToString();
            textTechWorker.Text = dataGridView1.Rows[i].Cells[22].Value.ToString();
            textNonTechWorker.Text = dataGridView1.Rows[i].Cells[23].Value.ToString();
            textRawMaterial.Text = dataGridView1.Rows[i].Cells[24].Value.ToString();
            textFileNum.Text = dataGridView1.Rows[i].Cells[25].Value.ToString();
             theli_no.Text = dataGridView1.Rows[i].Cells[26].Value.ToString();
                fy_txt.Text = dataGridView1.Rows[i].Cells[27].Value.ToString();
                textAdhikritPuji.Text = dataGridView1.Rows[i].Cells[28].Value.ToString();
                textJariPuji.Text = dataGridView1.Rows[i].Cells[29].Value.ToString();
                ainedit(dataGridView1.Rows[i].Cells[30].Value.ToString());
             //   ain_drop.SelectedIndex = ain_drop.FindStringExact("२०७६");
              //  ain_drop.SelectedText = "'"+(dataGridView1.Rows[i].Cells[30].Value.ToString().Trim())+"'";
            //MessageBox.Show(dataGridView1.Rows[i].Cells[30].Value.ToString());
                dafa1edit(dataGridView1.Rows[i].Cells[31].Value.ToString());
                dafa2edit(dataGridView1.Rows[i].Cells[32].Value.ToString());
            
                textKitta.Text = dataGridView1.Rows[i].Cells[33].Value.ToString();
                text_raw_price.Text = dataGridView1.Rows[i].Cells[34].Value.ToString();
                textMachinery.Text = dataGridView1.Rows[i].Cells[35].Value.ToString();
                textMachinePrice.Text = dataGridView1.Rows[i].Cells[36].Value.ToString();
                textFileNum.ReadOnly = false;


            // production_startdate as 'संचालन हुने मिति', runningperyear as 'संचालन हुने दिन',shiftno as 'शिफ्ट',industry_nature as 'उद्योगको प्रकृति', product_servicetype as 'उत्पादन हुने वस्तु/सेवा',fuel_water as 'इन्धन/पानी',environment_effect as 'वातावरणीय प्रभाव',postal_address as 'पत्राचारको ठेगाना', , contact_num as 'सम्पर्क नं.', contact_person as 'सम्पर्क व्यक्ति', niyamawali as 'नियमावली'

            textProductionDate.Text = rw.Cells[37].Value.ToString();
            textRunningDays.Text= rw.Cells[38].Value.ToString();
            comboShift.Text= rw.Cells[39].Value.ToString();
            textIndustryNature.Text= rw.Cells[40].Value.ToString();
            textProduct_Service.Text= rw.Cells[41].Value.ToString();
            textFuelWater.Text= rw.Cells[42].Value.ToString();
            textEnvironment.Text= rw.Cells[43].Value.ToString();
            textPostalAddress.Text= rw.Cells[44].Value.ToString();
            textContactNo.Text= rw.Cells[45].Value.ToString();
            textContactPerson.Text= rw.Cells[46].Value.ToString();
            comboNiyamawali.Text= rw.Cells[47].Value.ToString();

            //theli_no.ReadOnly = false;

            //display additional values from tables
            displayAdditionalDetails();

            //Owner Labels in the text box
            displayOwnerLabels();
        }
        public void ainedit(string di)
        {
            ////string connetionString = null;
            ////SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
           // //connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;
            sql = "SELECT ainid, ainname FROM setup_ain WHERE (ainname = @dis)";

            ////connection = new SqlConnection(connetionString);
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                ////connection.Open();
                command = new MySqlCommand(sql, sqlcon.con);

                adapter.SelectCommand = command;
                adapter.SelectCommand.Parameters.AddWithValue("@dis", di.ToString());

                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                sqlcon.con.Close();

                //industry_scale.DataSource = ds.Tables[0];
                //industry_scale.ValueMember = "subcategory_id";
                //industry_scale.DisplayMember = "subcategory_nepname";
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ain_drop.SelectedValue = row[0].ToString();
                    //citizen_off.Items.Add(row[0].ToString()+" Y "+row[1].ToString());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }



        }
        public void dafa1edit(string dist)
        {
            dafa1loading();
            //string connetionStrings = null;
           // //SqlConnection connections;
            MySqlCommand commands;
            MySqlDataAdapter adapters = new MySqlDataAdapter();
            DataSet dss = new DataSet();

            string sqls = null;
            //int distcodevalue = Convert.ToInt16(district_combo.SelectedValue);
           // //connetionStrings = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";

            //sqls = "SELECT VdcCode,VdcNepnam FROM  setup_vdc where DistCode='" +district_combo.SelectedValue+ "'";
            sqls = " SELECT Ain_id, dafaname FROM ain_dafa WHERE dafaid=1 AND (dafaname = @district)";

           // //connections = new SqlConnection(connetionStrings);
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
           // //connections.Open();
            commands = new MySqlCommand(sqls, sqlcon.con);
            adapters.SelectCommand = commands;
            adapters.SelectCommand.Parameters.AddWithValue("@district", dist.ToString());
            adapters.Fill(dss);
            adapters.Dispose();
            commands.Dispose();
            sqlcon.con.Close();


            foreach (DataRow row in dss.Tables[0].Rows)
            {

                dafa1.SelectedValue = row[0].ToString();
            }

        }
        public void dafa2edit(string dist)
        {
            dafa2loading();
            //string connetionStrings = null;
            ////SqlConnection connections;
            MySqlCommand commands;
            MySqlDataAdapter adapters = new MySqlDataAdapter();
            DataSet dss = new DataSet();

            string sqls = null;
            //int distcodevalue = Convert.ToInt16(district_combo.SelectedValue);
            ////connetionStrings = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";

            //sqls = "SELECT VdcCode,VdcNepnam FROM  setup_vdc where DistCode='" +district_combo.SelectedValue+ "'";
            sqls = " SELECT Ain_id, dafaname FROM ain_dafa WHERE dafaid=2 AND (dafaname = @district)";

           // //connections = new SqlConnection(connetionStrings);
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            ////connections.Open();
            commands = new MySqlCommand(sqls, sqlcon.con);
            adapters.SelectCommand = commands;
            adapters.SelectCommand.Parameters.AddWithValue("@district", dist.ToString());
            adapters.Fill(dss);
            adapters.Dispose();
            commands.Dispose();
           sqlcon.con.Close();


            foreach (DataRow row in dss.Tables[0].Rows)
            {

                dafa2.SelectedValue = row[0].ToString();
            }

        }
        private void textBox3_KeyPress_1(object sender, KeyPressEventArgs e)
        
    {
       // firm_capital.Text = e.KeyChar.(Replace('०', '0').Replace('१', '1').Replace('२', '2').Replace('३', '3').Replace('४', '4').Replace('५', '5').Replace('६', '6').Replace('७', '7').Replace('८', '8').Replace('९', '9'));
        //    base.OnKeyPress(e);
        //// Check if the pressed character was a backspace or numeric.
        //if (e.KeyChar != (char)8 && !char.IsNumber(e.KeyChar))
        //{
        //    e.Handled = true;
        //}
    
        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {
            //double a = 0;
            //double b = 0;
            //if (textBox3.Text != "")
            //{
            //a = Convert.ToDouble(textBox3.Text);
            //}
            //if (textBox4.Text != "")
            //{
            //    b = Convert.ToDouble(textBox4.Text);
            //}
            //textBox5.Text = (a + b).ToString();
        }

        private bool SaveIndustryToHistory(string actionType)
        {
            if (sqlcon.con.State == ConnectionState.Closed)
                sqlcon.con.Open();

            MySqlCommand qrform = new MySqlCommand("INSERT INTO industryreg_hist(industryid, industryregno, regdat, renewdate, industryscale, industrytype, industrycat, industrynepname, branch, industrydist, industryvdc, industryward, karobar, yearlyturnover, electricpower, statcapital, varcapital, comment, femaleworker, maleworker, tax, usr, updatdate, updatnepdate, techworker, nontechworker, rawmaterial, fileno, thelino, fiscalyear, tole, adhikrit_capital, jari_capital, ain, dafa1, dafa2, kittanum, rawmaterial_price, machine, machine_price, regnepdate, officedist, csioid, transid, updateuser) SELECT industryid, industryregno, regdat, renewdate, industryscale, industrytype, industrycat, industrynepname, branch, industrydist, industryvdc, industryward, karobar, yearlyturnover, electricpower, statcapital, varcapital, comment, femaleworker, maleworker, tax, usr, updatdate, updatnepdate, techworker, nontechworker, rawmaterial, fileno, thelino, fiscalyear, tole, adhikrit_capital, jari_capital, ain, dafa1, dafa2, kittanum, rawmaterial_price, machine, machine_price, regnepdate, officedist, csioid, @transid ,@user  FROM industryreg   where industryid=@firmid");

            qrform.CommandType = CommandType.Text;
            qrform.Parameters.AddWithValue("@firmid", transid.ToString());
            // qrform.Parameters.AddWithValue("@firmid", id_label.Text);
            qrform.Parameters.AddWithValue("@transid", actionType); //updated or deleted
            qrform.Parameters.AddWithValue("@user", global.username);
            // qrform.Parameters.AddWithValue("@decisionnepdate", global.nepalidate);

            qrform.Connection = sqlcon.con;
            int qrf = qrform.ExecuteNonQuery();

            bool myResult;
            if (qrf > 0)
                myResult = true;
            else
                myResult = false;

            return myResult;
        }

        private void UpdateRecords()
        { 
            try
            {

                DialogResult result1 = MessageBox.Show("के तपाईं उद्योगको विवरण अद्यावधिक गर्न चाहनुहुन्छ?",
           "उद्योगको विवरण अद्यावधिक", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result1 == DialogResult.Yes)
                {

                    if (textAdhikritPuji.Text == "")
                    {
                        textAdhikritPuji.Text = "0";
                    }
                    if (textJariPuji.Text == "")
                    {
                        textJariPuji.Text = "0";
                    }

                    string curdate = DateTime.Today.ToString();

                    if (SaveIndustryToHistory("updated") == true)
                    {

                       if (sqlcon.con.State == ConnectionState.Closed)
                            sqlcon.con.Open();
                        MySqlCommand qr = new MySqlCommand(" UPDATE industryreg  SET industryregno = GetUnicodeToNumber(@darta),regdat = GetUnicodeToNumber(@dartadate),renewdate =  GetUnicodeToNumber(@renewdate),industryscale = @industryscale ,industrytype = @industrytype ,industrycat = @firmobj,industrynepname = @firmname ,branch = @branch,industrydist = @district ,industryvdc = @vdc,industryward = GetUnicodeToNumber(@ward) ,tole = @tole,karobar = @karobar,yearlyturnover = GetUnicodeToNumber(@turnover),electricpower = GetUnicodeToNumber(@electric),statcapital = GetUnicodeToNumber(@txtbxthree) ,varcapital = GetUnicodeToNumber(@txtbxfour) ,comment = @comment ,usr = @user  ,updatdate = getdate(),updatnepdate = GetUnicodeToNumber(@nepdate), femaleworker = GetUnicodeToNumber(@female),maleworker = GetUnicodeToNumber(@male),tax = GetUnicodeToNumber(@taxs),techworker=GetUnicodeToNumber(@techworker),nontechworker=GetUnicodeToNumber(@nontechworker), rawmaterial=@rawmaterial, adhikrit_capital=GetUnicodeToNumber(@adhikritpuji), jari_capital=GetUnicodeToNumber(@jaripuji),fileno =GetUnicodeToNumber(@file),thelino = GetUnicodeToNumber(@theli),ain=@ain,dafa1=@dafa1,dafa2=@dafa2,kittanum =GetUnicodeToNumber(@kitta),rawmaterial_price =GetUnicodeToNumber(@rawprice),machine = @machine,machine_price = GetUnicodeToNumber(@machineprice),  production_startdate=@production_startdate, runningperyear=@runningperyear, shiftno=@shiftno, industry_nature=@industry_nature, product_servicetype=@product_servicetype, fuel_water=@fuel_water, environment_affect=@environment_affect, postal_address=@postal_address, contact_num=@contact_num, contact_person=@contact_person,niyamawali=@niyamawali WHERE industryid=@indid");

                        qr.CommandType = CommandType.Text;

                        qr.Parameters.AddWithValue("@indid", transid.ToString());
                        qr.Parameters.AddWithValue("@darta", textRegNum.Text);
                        qr.Parameters.AddWithValue("@dartadate", textRegDate.Text);
                        qr.Parameters.AddWithValue("@renewdate", textRenewDate.Text);
                        qr.Parameters.AddWithValue("@industryscale", industry_scale.SelectedValue);
                        qr.Parameters.AddWithValue("@industrytype", industry_type.SelectedValue);
                        qr.Parameters.AddWithValue("@firmobj", firm_obj.SelectedValue);
                        qr.Parameters.AddWithValue("@firmname", textFirmName.Text);
                        qr.Parameters.AddWithValue("@branch", textBranch.Text);
                        qr.Parameters.AddWithValue("@district", district_combo.SelectedValue);
                        qr.Parameters.AddWithValue("@vdc", vdc_combo.SelectedValue);
                        qr.Parameters.AddWithValue("@ward", textWard.Text);
                        qr.Parameters.AddWithValue("@tole", textTole.Text);
                        qr.Parameters.AddWithValue("@karobar", textObjective.Text);
                        qr.Parameters.AddWithValue("@turnover", firm_turnover.Text);
                        qr.Parameters.AddWithValue("@electric", textElectricPower.Text);
                        qr.Parameters.AddWithValue("@txtbxthree", textSthirPuji.Text);
                        qr.Parameters.AddWithValue("@txtbxfour", textChaluPuji.Text);
                        qr.Parameters.AddWithValue("@comment", comment.Text);
                        qr.Parameters.AddWithValue("@user", usr.Text);
                        qr.Parameters.AddWithValue("@currentdate", curdate);
                        qr.Parameters.AddWithValue("@nepdate", entry_nepdate.Text);
                        qr.Parameters.AddWithValue("@female", textFemaleWorker.Text);
                        qr.Parameters.AddWithValue("@male", textMaleWorker.Text);
                        qr.Parameters.AddWithValue("@taxs", tax_txt.Text);
                        qr.Parameters.AddWithValue("@techworker", textTechWorker.Text);
                        qr.Parameters.AddWithValue("@nontechworker", textNonTechWorker.Text);
                        qr.Parameters.AddWithValue("@rawmaterial", textRawMaterial.Text);
                        qr.Parameters.AddWithValue("@adhikritpuji", textAdhikritPuji.Text);
                        qr.Parameters.AddWithValue("@jaripuji", textJariPuji.Text);
                        qr.Parameters.AddWithValue("@theli", theli_no.Text);
                        qr.Parameters.AddWithValue("@file", textFileNum.Text);
                        qr.Parameters.AddWithValue("@ain", ain_drop.Text);
                        qr.Parameters.AddWithValue("@dafa1", dafa1.Text);
                        qr.Parameters.AddWithValue("@dafa2", dafa2.Text);
                        qr.Parameters.AddWithValue("@kitta", textKitta.Text);
                        qr.Parameters.AddWithValue("@rawprice", text_raw_price.Text);
                        qr.Parameters.AddWithValue("@machine", textMachinery.Text);
                        qr.Parameters.AddWithValue("@machineprice", textMachinePrice.Text);

                        qr.Parameters.AddWithValue("@production_startdate", textProductionDate.Text);
                        qr.Parameters.AddWithValue("@runningperyear", textRunningDays.Text);
                        qr.Parameters.AddWithValue("@shiftno", comboShift.Text);
                        qr.Parameters.AddWithValue("@industry_nature", textIndustryNature.Text);
                        qr.Parameters.AddWithValue("@product_servicetype", textProduct_Service.Text);
                        qr.Parameters.AddWithValue("@fuel_water", textFuelWater.Text);
                        qr.Parameters.AddWithValue("@environment_affect", textEnvironment.Text);
                        qr.Parameters.AddWithValue("@postal_address", textPostalAddress.Text);
                        qr.Parameters.AddWithValue("@contact_num", textContactNo.Text);
                        qr.Parameters.AddWithValue("@contact_person", textContactPerson.Text);
                        qr.Parameters.AddWithValue("@niyamawali", comboNiyamawali.Text);


                        qr.Connection = sqlcon.con;

                   
                        //int n = qr.ExecuteNonQuery();

                        int n = qr.ExecuteNonQuery();

                    
                        if (n > 0)
                        {
                            sqlcon.con.Close();
                            dataGridView1.DataSource = null;
                            DisplayData();
                            cleardata();
                            MessageBox.Show("उद्योगको विवरण सफलतापुर्वक अद्यावधिक गरियो ।", "उद्योगको विवरण अद्यावधिक", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            //buttonUpdate.Enabled = false;
                            //save_btn.Enabled = false;
                            //buttonSave.Enabled = false;
                            //buttonCancel.Enabled = buttonSave.Enabled;
                            //buttonNewIndustry.Enabled = true;
                            //panelContent.Enabled = false;
                            //buttonProprieter.Enabled = false;

                    }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            cleardata();
        }
        public void valuedelete()
        {
            DialogResult result1 = MessageBox.Show("के तपाईँ उद्योगको विवरण हटाउन (DELETE गर्न) चाहनुहुन्छ ?",
                    "उद्योगको विवरण हटाउन", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result1 == DialogResult.Yes)
            {
                if (SaveIndustryToHistory("deleted") == true) //saving industry data to history
                {

                    if (sqlcon.con.State == ConnectionState.Closed)
                    {
                        sqlcon.con.Open();
                    }
                    int i;
                    i = dataGridView1.CurrentRow.Index;


                    transid = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    MySqlCommand qrform = new MySqlCommand("INSERT INTO industryreg_hist(industryid,industryregno,regdat ,renewdate ,industryscale,industrytype ,industrycat ,industrynepname ,branch  ,industrydist ,industryvdc,industryward ,tole ,karobar,yearlyturnover ,electricpower   ,statcapital,varcapital,comment ,femaleworker ,maleworker ,tax ,usr ,updatdate  ,updatnepdate ,decisionnepdate  ,updateuser,transid) SELECT industryid ,industryregno ,regdat ,renewdate ,industryscale ,industrytype  ,industrycat   ,industrynepname  ,branch ,industrydist ,industryvdc ,industryward  ,tole ,karobar  ,yearlyturnover  ,electricpower  ,statcapital ,varcapital  ,comment ,femaleworker ,maleworker  ,tax  ,usr  ,updatdate  ,updatnepdate,@decisionnepdate,@user,@transid   FROM industryreg where industryid=@firmid");

                    qrform.CommandType = CommandType.Text;
                    qrform.Parameters.AddWithValue("@firmid", label5.Text);
                    // qrform.Parameters.AddWithValue("@firmid", id_label.Text);
                    qrform.Parameters.AddWithValue("@transid", "");
                    qrform.Parameters.AddWithValue("@user", global.username);
                    qrform.Parameters.AddWithValue("@decisionnepdate", global.todaynepslash);

                    qrform.Connection = sqlcon.con;
                    int qrf = qrform.ExecuteNonQuery();

                    if (qrf > 0)
                    {
                        global.sync_dblog("industryreg_hist", label5.Text, "INSERT", label5.Text, "industryid");
                    }
                        MySqlCommand qr = new MySqlCommand(" DELETE FROM industryreg  WHERE industryid=@indid");


                    qr.CommandType = CommandType.Text;

                    qr.Parameters.AddWithValue("@indid", transid.ToString());
                    qr.Connection = sqlcon.con;

                    //int n = qr.ExecuteNonQuery();
                    int n = qr.ExecuteNonQuery();

                    if (n > 0)
                    {
                        global.sync_dblog("industryreg", transid.ToString(), "DELETE", transid.ToString(), "industryid");
                        sqlcon.con.Close();
                        dataGridView1.DataSource = null;
                        DisplayData();
                        cleardata();
                        MessageBox.Show("उद्योगको बिवरण  सफलता पुर्वक हटाइयो ।");
                    }
                }
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            int usert = int.Parse(global.usertype);
            if (usert > 1)
            {
                valuedelete();
            }
               else
            {
                MessageBox.Show("बिवरण हटाउने अधिकार तपाईलाई नभएकोले यो विवरण हटाउने कार्य सफल हुन सकेन । ", "अपर्याप्त अधिकार", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void industry_type_SelectedValueChanged(object sender, EventArgs e)
        {
            string val;
            val = industry_type.SelectedValue.ToString();
            if (val == "93")
            {
                textJariPuji.Enabled = true;
                textAdhikritPuji.Enabled = true;
            }
            else
            {
                textJariPuji.Text = "";
                textAdhikritPuji.Text = "";
                textJariPuji.Enabled = false;
                textAdhikritPuji.Enabled = false;
            }
        }

        private void industry_scale_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dafa1.AutoCompleteMode = AutoCompleteMode.Append;
            //dafa1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //AutoCompleteStringCollection d1 = new AutoCompleteStringCollection();
            //dafa1load(d1);
            //dafa1.AutoCompleteCustomSource = d1;
        }

        private void firm_obj_SelectedIndexChanged(object sender, EventArgs e)
        {
          // dafa2change();
        }

        private void industry_scale_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dafa1change();
        }

        private void firm_obj_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dafa2change();

        }
        private void ain_drop_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dafa1.Text = "";
            dafa2.Text = "";
            dafa1loading();
            dafa2loading();
        }

        private void ain_drop_SelectedIndexChanged(object sender, EventArgs e)
        {
           

        }

        private void darta_txt_Leave(object sender, EventArgs e)
        {
            DisplayData();
            textFirmName_Leave(sender, e);
        }

        private void vdc_combo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ward_txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void dafa2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dafa1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LoadRegSubForm(string disptype)
        {
            industryreg_subforms ins = new industryreg_subforms();
            ins.myIndId = transid;
            ins.disptype = disptype;
            ins.myparent = this;
            ins.ShowDialog();
        }
        private void buttonMoreProduct_Click(object sender, EventArgs e)
        {
            LoadRegSubForm("product");

        }

        private void buttonMoreEnvironment_Click(object sender, EventArgs e)
        {
            LoadRegSubForm("environment");
        }

        private void buttonMoreMachine_Click(object sender, EventArgs e)
        {
            LoadRegSubForm("machinery");
        }

        private void buttonMoreRawMat_Click(object sender, EventArgs e)
        {
            LoadRegSubForm("raw_material");
        }

        private void buttonMoreFuelWater_Click(object sender, EventArgs e)
        {
            LoadRegSubForm("fuel_water");
        }

        private void buttonMoreInvestment_Click(object sender, EventArgs e)
        {
            LoadRegSubForm("investment");
        }

        public void displayAdditionalDetails()
        {
            string str = "";
            //product service
            str = getAdditionalIndividual("industryreg_product_service", "material");

            if (!string.IsNullOrEmpty(str))
            {
                textProduct_Service.Text = str;
                textProduct_Service.ReadOnly = true;
            }
            else
                textProduct_Service.ReadOnly = false;

            str = global.getSingleDataFromTable("SELECT  GetNumberToUnicode(sum(capacity * unit_price)) FROM industryreg_product_service where industryid='" + transid + "'");

            if(!string.IsNullOrEmpty(str))
            {
                firm_turnover.Text = str;
                firm_turnover.ReadOnly = true;
            }
            else
            firm_turnover.ReadOnly = false;

            //investment - STHIR
            str = global.getSingleDataFromTable("SELECT  GetNumberToUnicode(sum(amount)) FROM industryreg_investment_sthir where industryid='" + transid + "'");

            if (!string.IsNullOrEmpty(str))
            {
                textSthirPuji.Text = str;
                textSthirPuji.ReadOnly = true;
            }
            else
            textSthirPuji.ReadOnly = false;

            //investment - CHALU
            str = global.getSingleDataFromTable("SELECT  GetNumberToUnicode(sum(amount)) FROM industryreg_investment_chalu where industryid='" + transid + "'");

            if (!string.IsNullOrEmpty(str))
            {
                textChaluPuji.Text = str;
                textChaluPuji.ReadOnly = true;
            }
            else
                textChaluPuji.ReadOnly = true;

            //machinery

            str = getAdditionalIndividual("industryreg_machinery", "mach_name");

            if (!string.IsNullOrEmpty(str))
            {
                textMachinery.Text = str;
                textMachinery.ReadOnly = true;
            }
            else
                textMachinery.ReadOnly = false;

            str = global.getSingleDataFromTable("SELECT GetNumberToUnicode(sum(amount)) FROM industryreg_machinery where industryid='" + transid + "'");

            if (!string.IsNullOrEmpty(str))
            {
                textMachinePrice.Text = str;
                textMachinePrice.ReadOnly = true;
            }
            else
                textMachinePrice.ReadOnly = false;

            //raw material

            str = getAdditionalIndividual("industryreg_rawmaterial", "rawmat_name");

            if (!string.IsNullOrEmpty(str))
            {
                textRawMaterial.Text = str;
                textRawMaterial.ReadOnly = true;
            }
            else
                textMachinePrice.ReadOnly = false;

            str = global.getSingleDataFromTable("SELECT  GetNumberToUnicode(sum(unit_price)*sum(qty)) FROM industryreg_rawmaterial where industryid='" + transid + "'");

            if (!string.IsNullOrEmpty(str))
            {
                text_raw_price.Text = str;
                text_raw_price.ReadOnly = true;
            }
            else
                text_raw_price.ReadOnly = false;

            //fuel_water

            str = getAdditionalIndividual("industryreg_fuel_water", "concat(type,' (', qty, ' ', unit,')')");

            string elepow = getAdditionalIndividual("industryreg_fuel_water", "sum(qty)","type='विद्युत शक्ति'");
            textElectricPower.Text = elepow;

            //MessageBox.Show(elepow.ToString());

            if (!string.IsNullOrEmpty(str))
            {
                textFuelWater.Text = str;
                textFuelWater.ReadOnly = true;
            }
            else
                textFuelWater.ReadOnly = false;

            //environment

            str = getAdditionalIndividual("industryreg_environment", "component");

            if (!string.IsNullOrEmpty(str))
            {
                textEnvironment.Text = str;
                textEnvironment.ReadOnly = true;
            }
            else
                textEnvironment.ReadOnly = false;
        }

        private string getAdditionalIndividual(string tbl, string fld, string cond="")
        {
            string res="";
            if (cond != "") cond = "AND " + cond;
            string sqq = "SELECT " + fld + " FROM " + tbl + " WHERE industryid='" + transid + "'" + cond;
            string[] vals = global.getSingleFieldArrayFromTable(sqq);
            for (int ii = 0; ii < vals.Length; ii++)
            {
                res += vals[ii].ToString();

                if (ii< vals.Length - 1)
                    res += ", ";
            }
            return res;
        }
       public void displayOwnerLabels()
        {
            //remove previous controls
            var controlsToRemove = panelProprieter.Controls.OfType<Label>().ToArray();
            foreach (Control lbl in controlsToRemove)
            {
                if (lbl.Name != "lblOwn1")
                {
                    panelProprieter.Controls.Remove(lbl);
                    lbl.Dispose();
                }
                else
                {
                    lbl.Visible = false;
                }
                //Controls.Remove(control);
                //cntrl.Dispose();
            }
            //textProprieter.Text = "";
            //textProprieter.ReadOnly=true;;

            string sqq = "SELECT concat(ownerfname,' ' , ownerlname) as oname FROM owner_industry WHERE industryid='" + transid + "'";
            string[] onrs = global.getSingleFieldArrayFromTable(sqq);
            int pos = lblOwn1.Left;
            lblOwn1.Visible = false;
            for (int oo=0;oo<onrs.Length;oo++)
            {
                //textProprieter.Text += onrs[oo].ToString();
                Label lbl = new Label();
                lbl.Name = "lbl" + oo;
                //font and color
                lbl.Font = lblOwn1.Font;
                lbl.BackColor = Color.PaleGreen;

                lbl.AutoSize = true;
                lbl.Text = onrs[oo].ToString();
                lbl.BorderStyle = lblOwn1.BorderStyle;
                lbl.Parent = panelProprieter;

                //position
                lbl.Left = pos;
                pos = pos + lbl.Width + 5;
                lbl.Top = lblOwn1.Top;

                panelProprieter.Controls.Add (lbl);
                lbl.Show();
                lbl.BringToFront();
            }
        }

        private void comboProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isReady)
                global.fillCombo(district_combo, "SELECT distcode, distunicodename FROM setup_district WHERE distzonecd=" + comboProvince.SelectedValue, "distunicodename", "distcode", "सबै जिल्ला");
        }

        private void textPostalAddress_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Space && e.Modifiers==Keys.Control)
            {
                if (!String.IsNullOrEmpty(textTole.Text))
                    textPostalAddress.Text = textTole.Text + ", ";

                if (!String.IsNullOrEmpty(textWard.Text))
                    textPostalAddress.Text += vdc_combo.Text + " - " + textWard.Text + ", " + district_combo.Text;
                else
                    textPostalAddress.Text += vdc_combo.Text + ", " + district_combo.Text;

               // e.Handled = true;
               e.SuppressKeyPress=true;

                labelAddressInfo.Visible = false;
                textContactNo.Focus();
            }
        }

        private void textPostalAddress_Leave(object sender, EventArgs e)
        {
            labelAddressInfo.Visible = false;
            textFirmName_Leave(sender, e);
        }

        private void textPostalAddress_Enter(object sender, EventArgs e)
        {
            labelAddressInfo.Visible = true;
            textFirmName_Enter(sender, e);
        }

        private void btnMoreTheli_Click(object sender, EventArgs e)
        {
            //industry theli = new industry();
            //theli.myparent = this;
            //theli.ShowDialog();

            //get the max theli number
            int lastTheli = Convert.ToInt32(global.getSingleDataFromTable("SELECT MAX(theli_no) FROM setup_theli WHERE subcategory_id=129"));

            lastTheli = lastTheli + 1;

            if (MessageBox.Show("नयाँ ठेली नं. " + global.convertNumToUnicode(lastTheli.ToString()) + " थप्न चाहनुहुन्छ ?", "नयाँ ठेली नं. थप", MessageBoxButtons.YesNo ,MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {

                if (sqlcon.con.State == ConnectionState.Closed)
                    sqlcon.con.Open();

                string theliLuzId = "129"; //for theli

                MySqlCommand cmd = new MySqlCommand("INSERT INTO setup_theli(subcategory_id,theli_no,fy,update_user ,update_nepdate) VALUES (@subcategory_id,GetUnicodeToNumber(@theli_no), @fy,@update_user,@update_nepdate)");
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@subcategory_id", theliLuzId);
                cmd.Parameters.AddWithValue("@theli_no", lastTheli);
                cmd.Parameters.AddWithValue("@fy", global.fyid);
                cmd.Parameters.AddWithValue("@update_user", global.username);
                cmd.Parameters.AddWithValue("@update_nepdate", global.nepalidate);
                cmd.Connection = sqlcon.con;
                int n = cmd.ExecuteNonQuery();
                /////////////////////////////


                if (n > 0)
                {
                    FillTheliLuz();

                    //sqlcon.con.Close();
                    //dataGridView1.DataSource = null;
                    // DisplayData();
                    //textBox1.Text = null;
                    //MessageBox.Show("रेकर्ड इन्ट्री सफल भयो ।", "डाटा इन्ट्री", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void chkOldVDC_CheckedChanged(object sender, EventArgs e)
        {
            if (vdc_combo.Items.Count < 1)
                return;

            string VDCid = vdc_combo.SelectedValue.ToString();

            string sqst = "SELECT VDC_SID, vdcunicodename FROM  setup_vdc where DistCode = '" + district_combo.SelectedValue + "'";
                //if sabik samet checkbox is NOT checked, then display only Current VDCs
                if (chkOldVDC.Checked == false)
                sqst += " AND isCur=1";

                global.fillCombo(vdc_combo,sqst,"vdcunicodename","VDC_SID");

            vdc_combo.SelectedValue = VDCid; //display the same item that was selected before the list got changed
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string fieldsNotCheck = "branch_txt,textContactNo,textContactPerson";
            if (!ValidateFields(fieldsNotCheck))
            {
                MessageBox.Show("कृपया उद्योग सम्बन्धी सम्पूर्ण विवरण प्रविष्टी गर्नुहोला । ", "उद्योग विवरण सेभ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            else
            {
                int ownc = Convert.ToInt32(global.getSingleDataFromTable("select IFNULL(COUNT(ownerid),0) FROM owner_industry WHERE industryid='" + transid + "'"));

                if (ownc == 0)
                {
                    MessageBox.Show("कृपया उद्योग संचालकको विवरण प्रविष्टी गर्नुहोला । ", "उद्योग विवरण सेभ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
           }



            if(saveMode=="add")
            {
                SaveNewRecord();
            }
            else if (saveMode=="edit")
            {
                int usert = int.Parse(global.usertype);
                if(usert > 1){
                    UpdateRecords();
                }else if (usert <= 1 && textRegDate.Text==global.todaynepslash)
                {
                    UpdateRecords();
                }
                else
                {
                    MessageBox.Show("संशोधन गर्ने अधिकार तपाईलाई नभएकोले संशोधन सफल हुन सकेन । ", "अपर्याप्त अधिकार", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidateFields(string checkControls)
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(textRegNum.Text))
            {
                //errorProvider1.SetError(tb, "Please fill the required field");
                textRegNum.BackColor = backErr;
                if (isValid == true) textRegNum.Focus();
                isValid = false;
            }

            if (string.IsNullOrEmpty(textFirmName.Text))
            {
                //errorProvider1.SetError(tb, "Please fill the required field");
                textFirmName.BackColor = backErr;
                isValid = false;
            }

            if (string.IsNullOrEmpty(textObjective.Text))
            {
                //errorProvider1.SetError(tb, "Please fill the required field");
                textObjective.BackColor = backErr;
                isValid = false;
            }

            if (!textRegDate.MaskFull)
            {
                //errorProvider1.SetError(tb, "Please fill the required field");
                textRegDate.BackColor = backErr;
                isValid = false;
            }

            if (string.IsNullOrEmpty(textWard.Text))
            {
                //errorProvider1.SetError(tb, "Please fill the required field");
                textWard.BackColor = backErr;
                isValid = false;
            }

            if (string.IsNullOrEmpty(textSthirPuji.Text))
            {
                //errorProvider1.SetError(tb, "Please fill the required field");
                textSthirPuji.BackColor = backErr;
                isValid = false;
            }

            if (string.IsNullOrEmpty(textChaluPuji.Text))
            {
                //errorProvider1.SetError(tb, "Please fill the required field");
                textChaluPuji.BackColor = backErr;
                isValid = false;
            }

            return isValid;
        }
        



        //THIS IS TO CHECK VALIDATION OF ALL THE FIELDS (within form and within panel / groupbox within the form)
        public bool ValidateAllFields(string exceptionList="")
        {
                //   The types you want to select
                var typeToBeSelected = new List<Type>
            {
                typeof(TextBox)
                , typeof(MaskedTextBox)
                , typeof(Button)
            };

            //    Only one call
            var allControls = global.GetAll(this,typeToBeSelected);
            

            //var controls = new[] { txt_PartyName, txt_ContactPerson, txt_mobile1, txt_add, txt_city, txt_state, txt_PinCode };
            //var controls = this.Controls;

                bool isValid = true;

            foreach (Control ct in allControls)
            {
                
                if (ct is System.Windows.Forms.TextBox)
                {
                    TextBox tb = ct as TextBox;

                    if(!isIgnoreControl(tb.Name,exceptionList))
                        {
                            if (string.IsNullOrEmpty(tb.Text) && tb.Enabled)
                            {
                                //errorProvider1.SetError(tb, "Please fill the required field");
                                tb.BackColor = System.Drawing.Color.FromArgb(255, 210, 220);
                                isValid = false;
                            }
                        }
                        //else
                        //{
                        //    MessageBox.Show(st);
                        //}
                }
            }
            return isValid;
        }

        //to check ignore list for above function
    private bool isIgnoreControl(string conName, string ignoreList)
    {
        bool ign = false;

        //if there are parameters
        string[] myExcep;
        //if (exceptionList != "")
        //{
        myExcep = ignoreList.Split(',');
        //}

        foreach(string st in myExcep)
        {
            if(conName==st)
            {
                ign = true;
                return ign;
            }
        }
        return ign;
    }


    private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                panelContent.Enabled = true;
                DataGridViewRow rr = dataGridView1.SelectedRows[0];
                DisplayIndividualData();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 && panelContent.Enabled==true)
            {
                DataGridViewRow rr = dataGridView1.SelectedRows[0];
                DisplayIndividualData();
            }
        }

        private void buttonFileScan_Click(object sender, EventArgs e)
        {
            industryfileupload ownrindst = new industryfileupload();
            ownrindst.MdiParent = this.MdiParent;
            ownrindst.getdataa(textRegNum.Text.ToString(),transid.ToString());
            //this.Hide();
            ownrindst.Show();
        }

        private void buttonProprieter_Click(object sender, EventArgs e)
        {
            DisplayIndustryOwner();
        }

        private void buttonMoreProprieter_Click(object sender, EventArgs e)
        {
            DisplayIndustryOwner();
        }

        private void darta_date_Leave(object sender, EventArgs e)
        {
            (sender as MaskedTextBox).BackColor = backNorm;

            if (textRegDate.MaskFull)
            {
                renewdate();
            }
        }

        private void textFirmName_Enter(object sender, EventArgs e)
        {
            (sender as TextBox).BackColor = backFoc;
        }

        private void textFirmName_Leave(object sender, EventArgs e)
        {
            (sender as TextBox).BackColor = backNorm;
        }

        private void textRegDate_Enter(object sender, EventArgs e)
        {
            (sender as MaskedTextBox).BackColor = backFoc;
        }

        private void textProductionDate_Leave(object sender, EventArgs e)
        {
            (sender as MaskedTextBox).BackColor = backFoc;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
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

            string dartanum = dataGridView1.Rows[i].Cells[1].Value.ToString();
            //getindustrytype(dataGridView1.Rows[i].Cells[9].Value.ToString());
            //getindustrynature(dataGridView1.Rows[i].Cells[10].Value.ToString());
            //MessageBox.Show(dartanum.ToString());
            industrynaasari ownrindst = new industrynaasari();
            SearchIndustryCertLetter bd = new SearchIndustryCertLetter();
            bd.MdiParent = this.MdiParent;

            bd.getdataa(dartanum);


            bd.Show();
        }
    }
}
