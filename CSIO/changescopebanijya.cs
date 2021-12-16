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
    public partial class changescopebanijya : Form
    {
        public changescopebanijya()
        {
            InitializeComponent();
        }
        public void firmkarobar(AutoCompleteStringCollection data)
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
            sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (setup_subcategory.category_id = 7)";
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
        public void firmkarobaredit(string firmobj)
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
            sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (subcategory_unicodename = @firmobj)";
            //connection = new SqlConnection(connetionString);
            try
            {
                //connection.Open();
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
        public void firmobj(string firmobj)
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
            sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (subcategory_unicodename = @firmobj)";
            //connection = new SqlConnection(connetionString);
            try
            {
                //connection.Open();
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
                    firm_obj.SelectedValue = row[0].ToString();
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
            //string connetionString = null;
            //SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            //connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;
            sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (setup_subcategory.category_id = 5)";
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
       
        private void changescopebanijya_Load(object sender, EventArgs e)
        {
            d_date.Text = global.todaynepslash;
            user_txt.Text = global.username;
            DisplayData();
            groupBox1.Enabled = false;
            button4.Enabled = false;

            karobar.AutoCompleteMode = AutoCompleteMode.Append;
            karobar.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection firmk = new AutoCompleteStringCollection();
            firmkarobar(firmk);
            karobar.AutoCompleteCustomSource = firmk;
            karobar.SelectedIndex = 0;
            firm_obj.AutoCompleteMode = AutoCompleteMode.Append;
            firm_obj.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection firmo = new AutoCompleteStringCollection();
            firmobj(firmo);
            firm_obj.AutoCompleteCustomSource = firmo;
            firm_obj.SelectedIndex = 0;
        }
        public void cleardata()
        {
            textBox1.Text = null;
            textBox2.Text = null;
            textBox4.Text = null;
        }
        public void DisplayData()
        {
            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            //con.Open();

            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     banijyaform.firmid, banijyaform.firmregno AS 'र.नं', banijyaform.firmnepname AS 'फर्मको नाम', banijyaform.karobar AS 'कारोवार बस्तुहरु', setup_subcategory.subcategory_unicodename As 'कारोवार', setup_subcategory_1.subcategory_unicodename AS 'उद्देश्य' FROM         banijyaform INNER JOIN  setup_subcategory ON banijyaform.karobartype = setup_subcategory.subcategory_id INNER JOIN   setup_subcategory AS setup_subcategory_1 ON banijyaform.firmscope = setup_subcategory_1.subcategory_id where banijyaform.firmid=@darta ", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@darta", label5.Text);
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView1.DataSource = tb;
            dataGridView1.Columns[0].Visible = false;
            sqlcon.con.Close();

        }

        public void getid(int indusid, string trans,string decisiondate)
        {
            label5.Text = null;
            label5.Text = indusid.ToString();
            darta_no.Text = trans.ToString();
            d_date.Text = decisiondate.ToString();
           // updateid.Text = null;



            //date_txt.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int i;
                i = dataGridView1.CurrentRow.Index;
                int industryid = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                string regno = dataGridView1.Rows[i].Cells[1].Value.ToString();
                string statvalue = dataGridView1.Rows[i].Cells[3].Value.ToString();
                string karobartype = dataGridView1.Rows[i].Cells[4].Value.ToString();
                string scope = dataGridView1.Rows[i].Cells[5].Value.ToString();

                //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                ///////////////////////////////////////////////////////////






                //MySqlCommand command;
                //MySqlDataAdapter adapter = new MySqlDataAdapter();
                //DataSet ds = new DataSet();

                //string sql = null;

                //sql = "select * from industry_prev where industryid=@firmname";



                //    command = new SqlCommand(sql, con);
                //    adapter.SelectCommand = command;
                //    command.CommandType = CommandType.Text;

                //    command.Parameters.AddWithValue("@firmname", label5.Text);
                //    adapter.Fill(ds);
                //    adapter.Dispose();
                //    command.Dispose();

                //    int i = 0;
                //        i= ds.Tables[0].Rows.Count;
                //    //MessageBox.Show("Total Row" + i.ToString());


                //    //string curdate = DateTime.Today.ToString();
                //    //SqlCommand qrs = new SqlCommand("select count(*) from industryreg where industrynepname=@firmname");

                //    //MessageBox.Show(qrs.ToString());
                //    //    qrs.CommandType = CommandType.Text;

                //    //    qrs.Parameters.AddWithValue("@firmname", firm_name.Text.Trim());
                //    //    qrs.Connection = con;


                //    //    int n = qrs.ExecuteNonQuery();
                //    //   // reader = qrs.ExecuteReader();
                //    //   // MessageBox.Show(reader.HasRows.ToString());
                //    //DataTable dt = new DataTable();



                //        if (i > 0)
                //        {



                //            MessageBox.Show("उद्योगको नाम पहिले नै इन्टी भइ सकेको ले कृपया अर्को नाम इन्टी गर्नुहोस ।", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);


                //        }
                //        else
                //        {
                //        }



                /////////////////////////////////////////

                // string curdate = DateTime.Today.ToString();

                //SqlCommand qr = new SqlCommand("INSERT INTO update_industry (industryid,industryreg ,dartanum,updattitle,updatnew,oldrec,[updatedate],updatenepdate,updateuser) VALUES (@industryid, @industryregno, @dartanum, @updatetitle, @updatenew, @oldvalue, @updatedate, @updatenepdate, @updateuser)");
                MySqlCommand qr = new MySqlCommand("INSERT INTO update_banijya (firmid,firmreg ,dartanum,updattitle,updatnew,oldrec,updatenepdate,updateuser,decisiondate,tax,comment) VALUES (@industryid, GetUnicodeToNumber(@industryregno), GetUnicodeToNumber(@dartanum), @updatetitle, @updatenew, @oldvalue, @updatenepdate, @updateuser, GetUnicodeToNumber(@ddate), GetUnicodeToNumber(@tax), @comment)");

                qr.CommandType = CommandType.Text;
                qr.Parameters.AddWithValue("@industryid", industryid.ToString());
                qr.Parameters.AddWithValue("@industryregno", regno.ToString());
                qr.Parameters.AddWithValue("@dartanum", darta_no.Text);
                qr.Parameters.AddWithValue("@updatetitle", updateid.Text);
                qr.Parameters.AddWithValue("@tax", textBox2.Text);
                //qr.Parameters.AddWithValue("@updatenew", textBox1.Text);
                //qr.Parameters.AddWithValue("@oldvalue", statvalue.ToString());

                qr.Parameters.AddWithValue("@updatenew", textBox1.Text + " को " + firm_obj.Text + " गर्ने गरी " + karobar.Text + " खरिद विक्री ।");
                qr.Parameters.AddWithValue("@oldvalue", statvalue.ToString() + " को " + scope.ToString() + " गर्ने गरी " + karobartype.ToString() + " खरिद विक्री ।");

                //qr.Parameters.AddWithValue("@transid", updateid.Text);
                //// qr.Parameters.AddWithValue("@updatedate", curdate.ToString());
                qr.Parameters.AddWithValue("@updatenepdate", global.nepalidate);
                qr.Parameters.AddWithValue("@updateuser", user_txt.Text);
                qr.Parameters.AddWithValue("@ddate", d_date.Text);
                qr.Parameters.AddWithValue("@comment", textBox4.Text);


                qr.Connection = sqlcon.con;


                int k = qr.ExecuteNonQuery();

                if (k > 0)
                {

                    //sqlcon.con.Close();
                    // dataGridView1.DataSource = null;
                    // DisplayData();
                    // cleardata();
                    MySqlCommand qrs = new MySqlCommand("UPDATE banijyaform  SET karobar = @txtbxthree, karobartype=@karobartype, firmscope=@obj  WHERE firmid=@indid");


                    qrs.CommandType = CommandType.Text;


                    qrs.Parameters.AddWithValue("@txtbxthree", textBox1.Text);
                    // qrs.Parameters.AddWithValue("@txtbxfour", textBox2.Text);

                    // qrs.Parameters.AddWithValue("@comment", textBox4.Text);
                    qrs.Parameters.AddWithValue("@indid", industryid.ToString());
                    qrs.Parameters.AddWithValue("@karobartype", karobar.SelectedValue);
                    qrs.Parameters.AddWithValue("@obj", firm_obj.SelectedValue);
                    //qr.Parameters.AddWithValue("@nepdate", entry_nepdate.Text);
                    //qr.Parameters.AddWithValue("@female", fworker.Text);
                    //qr.Parameters.AddWithValue("@male", maleworker.Text);
                    //qr.Parameters.AddWithValue("@taxs", tax_txt.Text);
                    qrs.Connection = sqlcon.con;


                    //int n = qr.ExecuteNonQuery();
                    int n = qrs.ExecuteNonQuery();

                    if (n > 0)
                    {

                        sqlcon.con.Close();
                        dataGridView1.DataSource = null;
                        DisplayData();
                        cleardata();
                        MessageBox.Show("संशोधन सफल भयो ।", "संशोधन", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        //button9.Enabled = false;
                        //button12.Enabled = false;
                        //panel2.Enabled = false;


                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cleardata();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int i;
            
            i = dataGridView1.CurrentRow.Index;
            button4.Enabled = true;
            textBox1.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            firmkarobaredit(dataGridView1.Rows[i].Cells[4].Value.ToString());
            firmobj(dataGridView1.Rows[i].Cells[5].Value.ToString());
        }

        private void d_date_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
