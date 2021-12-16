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
    public partial class banijyaform : Form
    {
        //string usernam;
        public banijyaform()
        {
           // usernam = username;
          // usrname.Text = username;
            InitializeComponent();
            
        }
        public void senddetail(string user, string date)
        {
            string usernam = user.ToString();
            string nepdate = date.ToString();
            


            //date_txt.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }
        public void theliluj(AutoCompleteStringCollection dataCollection)
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
            sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (setup_subcategory.category_id = 4)";
            //connection = new MySqlConnection(connetionString);
            try
            {
                //connection.Open();
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                sqlcon.con.Close();

                theli_lujsave.DataSource = ds.Tables[0];
                theli_lujsave.ValueMember = "subcategory_id";
                theli_lujsave.DisplayMember = "subcategory_unicodename";
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
        //public void theli(AutoCompleteStringCollection dataCollection)
        //{

        //    //string connetionString = null;
        //    //SqlConnection connection;
        //    MySqlCommand command;
        //    MySqlDataAdapter adapter = new MySqlDataAdapter();
        //    DataSet ds = new DataSet();

        //    string sql = null;
        //    connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.passwordname + "'";
        //    sql = "SELECT     subcategory_name, subcategory_nepname FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (setup_subcategory.category_id = 4)";
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

        //        theli_combo.DataSource = ds.Tables[0];
        //        theli_combo.ValueMember = "subcategory_name";
        //        theli_combo.DisplayMember = "subcategory_nepname";
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


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //owendetailbanijya ow = new owendetailbanijya();
            //ow.MdiParent = this.ParentForm;
            //ow.getdataa(textBox2.Text.ToString(), textBox3.Text, theli_no.Text, fy_combo.SelectedText);
 

         
            //ow.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           banijyaforminfo bd = new banijyaforminfo();
            bd.MdiParent = this.MdiParent;
            
            bd.getdataa(textBox2.Text.ToString(), textBox3.Text, theli_no.Text, fy_combo.Text);
           

            bd.Show();
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
                this.Close();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
      

        public void FillDropDownListvdc()
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
       
        private void banijyaform_Load(object sender, EventArgs e)
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            //string a = "१०००";
            //string b = "२०००";
            //Int32 c = Convert.ToInt32(a) + Convert.ToInt32(b);
            //MessageBox.Show(c.ToString());
            //usrname.Text = global.username;
            // TODO: This line of code loads data into the 'cSIODataSet5.setup_theli' table. You can move, or remove it, as needed.
            //this.setup_theliTableAdapter.Fill(this.cSIODataSet5.setup_theli);
            // TODO: This line of code loads data into the 'cSIODataSet4.setup_subcategory' table. You can move, or remove it, as needed.
            //this.setup_subcategoryTableAdapter.Fill(this.cSIODataSet4.setup_subcategory);
            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.passwordname + "' ");
            //con.Open();
            //theli_combo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //theli_combo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //theli_lujsave.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //theli_lujsave.AutoCompleteSource = AutoCompleteSource.CustomSource;

            AutoCompleteStringCollection combDat = new AutoCompleteStringCollection();
           // theli(combDat);
            AutoCompleteStringCollection combD = new AutoCompleteStringCollection();
            theliluj(combD);
           // theli_combo.AutoCompleteCustomSource = combDat;
            theli_lujsave.AutoCompleteCustomSource = combD;

            theli_lujsave.SelectedValue = 'T';

           // theli_combo.SelectedValue='T';

            FillDropDownListvdc();
            fiscalyear();
         



        }

        private void button2_Click(object sender, EventArgs e)
        {
            capitalupdateindustry capdet = new capitalupdateindustry();
            capdet.MdiParent = this.ParentForm;

            capdet.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.passwordname + "' ");
            //con.Open();
            //theli_no.Text = null;
        
            FillDropDownListvdc();
            

            
            
           
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
           

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            MySqlCommand cmd = new MySqlCommand("INSERT INTO setup_theli(subcategory_id,theli_no,fy ,update_user ,update_nepdate) VALUES ( @darta,GetUnicodeToNumber(@thelino), @fy,@user,@updt)");
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@darta", theli_lujsave.SelectedValue);
            cmd.Parameters.AddWithValue("@thelino", textBox1.Text);
            cmd.Parameters.AddWithValue("@fy", fy_combo.Text);
            cmd.Parameters.AddWithValue("@user", global.username);
            cmd.Parameters.AddWithValue("@updt", global.nepalidate);
                            cmd.Connection = sqlcon.con;
                int n=cmd.ExecuteNonQuery();
                /////////////////////////////


                if (n > 0)
                {

                    sqlcon.con.Close();
                    //dataGridView1.DataSource = null;
                    // DisplayData();
                    textBox1.Text = null;
                    MessageBox.Show("रेकर्ड इन्टी सफल भयो ।", "डाटा इन्ट्री", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }

        }

        private void theli_lujsave_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }   
}
