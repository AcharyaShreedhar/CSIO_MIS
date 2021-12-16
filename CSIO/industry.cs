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
    public partial class industry : Form
    {


        public industry()
        {
            InitializeComponent();
        }

        public industryreg myparent { get; set; } //this is required to call a method of parent from this child form


        //public void FillDropDownListvdc()
        //{

        //    //string connetionString = null;
        //    //SqlConnection connection;
        //    MySqlCommand command;
        //    MySqlDataAdapter adapter = new MySqlDataAdapter();
        //    DataSet ds = new DataSet();

        //    string sql = null;
        //    connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.passwordname + "'";

        //    sql = "SELECT     theli_id, theli_no FROM         setup_theli INNER JOIN  setup_subcategory ON setup_theli.subcategory_id = setup_subcategory.subcategory_id INNER JOIN    setup_category ON setup_subcategory.category_id = setup_category.category_id WHERE     (setup_theli.subcategory_name = '" + theli_combo.SelectedValue + "')";

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

        //        theli_no.DataSource = ds.Tables[0];
        //        theli_no.ValueMember = "theli_id";
        //        theli_no.DisplayMember = "theli_no";
        //        // foreach (DataRow row in ds.Tables[0].Rows)
        //        // {
        //        //   dataCollectionvdc.Add(row[1].ToString());
        //        // }


        //    }
        //    catch
        //    {
        //        MessageBox.Show("Can not open connection ! ");
        //    }

        //}
        public void theliluj()
        {

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
           // //string connetionString = null;
            ////SqlConnection connection;
           MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
          //  //connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;
            sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (setup_subcategory.category_id = 13)";
          //  //connection = new SqlConnection(connetionString);
            try
            {
                ////connection.Open();
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                sqlcon.con.Close();

                theli_lujsave.DataSource = ds.Tables[0];
                theli_lujsave.ValueMember = "subcategory_id";
                theli_lujsave.DisplayMember = "subcategory_unicodename";

                theli_lujsave.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }
        
       
        private void industry_Load(object sender, EventArgs e)
        {

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            //AutoCompleteStringCollection combDat = new AutoCompleteStringCollection();
            // theli(combDat);
            //AutoCompleteStringCollection combD = new AutoCompleteStringCollection();
            theliluj();
            // theli_combo.AutoCompleteCustomSource = combDat;
            //theli_lujsave.AutoCompleteCustomSource = combD;

            theli_lujsave.SelectedValue = 'T';

            // theli_combo.SelectedValue='T';
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            try
            {
               if(textBox1.Text.Trim()=="" || theli_lujsave.Text=="")
               {
                   MessageBox.Show("TextBox Cannot be blank");
               }
               else
               {
                   if (sqlcon.con.State == ConnectionState.Closed)
                   {
                       sqlcon.con.Open();
                   }
                MySqlCommand cmd = new MySqlCommand("INSERT INTO setup_theli(subcategory_id,theli_no,fy ,update_user ,update_nepdate) VALUES ( @darta,GetUnicodeToNumber(@thelino), @fy,@user,@updt)");
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@darta", theli_lujsave.SelectedValue);
                cmd.Parameters.AddWithValue("@thelino", textBox1.Text);
                cmd.Parameters.AddWithValue("@fy", global.fyid);
                cmd.Parameters.AddWithValue("@user", global.username);
                cmd.Parameters.AddWithValue("@updt", global.nepalidate);
                cmd.Connection = sqlcon.con;
                int n = cmd.ExecuteNonQuery();
                /////////////////////////////


                if (n > 0)
                {

                    sqlcon.con.Close();
                    //dataGridView1.DataSource = null;
                    // DisplayData();
                    textBox1.Text = null;
                    MessageBox.Show("रेकर्ड इन्ट्री सफल भयो ।", "डाटा इन्ट्री", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
               }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void industry_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.Parent.
            myparent.FillTheliLuz(); //updating the parent form
        }
    }
}
