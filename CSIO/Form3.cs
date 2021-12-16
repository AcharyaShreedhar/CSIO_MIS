using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;

namespace CSIO
{
    public partial class usersetting : Form
    {
       // SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.passwordname + "' ");

        public usersetting()
        {
            InitializeComponent();
          }
        //Display Data in DataGridView  
        public void DisplayData()
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT userid, username, name, address, phone, status, usertype FROM login WHERE usertype<='" + global.usertype + "' order by userid", sqlcon.con);
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView1.DataSource = tb;
            sqlcon.con.Close();

            dataGridView1.Columns[0].Visible = false;

           
        }  


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void userssetting()
        {
            // MessageBox.Show("Working");
            // //string connetionString = null;
            // //SqlConnection connection;
           MySqlCommand command;
           MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            //connetionString = mysqlcon.conn.ToString();
            sql = "SELECT `usertypeid`, `usertypename` FROM `user_type` WHERE usertypeid<='" + global.usertype + "'";
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


                usertype.DataSource = ds.Tables[0];
                usertype.ValueMember = "usertypeid";
                usertype.DisplayMember = "usertypename";
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
        public void statusedit(string firmobj)
        {

            //string connetionString = null;
            ////SqlConnection connection;
           MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            // connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.passwordname + "'";
            sql = "SELECT     statusid, status FROM user_status  WHERE (status = @firmobj)";
           // //connection = new SqlConnection(connetionString);
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
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
                    status.SelectedValue = row[0].ToString();
                    //citizen_off.Items.Add(row[0].ToString()+" Y "+row[1].ToString());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }
        public void officecategory()
        {
            // MessageBox.Show("Working");
            // //string connetionString = null;
            // //SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            //connetionString = mysqlcon.conn.ToString();
            sql = "SELECT office_catid,  officenamenep FROM setup_officecategory ";
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


                office_cat.DataSource = ds.Tables[0];
                office_cat.ValueMember = "office_catid";
                office_cat.DisplayMember = "officenamenep";

                sqlcon.con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }
        public void officename()
        {
            // MessageBox.Show("Working");
            // //string connetionString = null;
            // //SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            //connetionString = mysqlcon.conn.ToString();
            sql = "SELECT csioid,  csioNepNm FROM setup_csio ";
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


                office_name.DataSource = ds.Tables[0];
                office_name.ValueMember = "csioid";
                office_name.DisplayMember = "csioNepNm";
                
                sqlcon.con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }

        public void usersstatus()
        {
            // MessageBox.Show("Working");
            // //string connetionString = null;
            // //SqlConnection connection;
           MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            //connetionString = mysqlcon.conn.ToString();
            sql = "SELECT `statusid`, `status` FROM `user_status`";
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


                status.DataSource = ds.Tables[0];
                status.ValueMember = "statusid";
                status.DisplayMember = "status";
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
    
        public void usertypeedit(string firmobj)
        {

           // //string connetionString = null;
          //  //SqlConnection connection;
           MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            // connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.passwordname + "'";
            sql = "SELECT     usertypeid, usertypename FROM user_type  WHERE (usertypename = @firmobj)";
          //  //connection = new SqlConnection(connetionString);
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
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
                    usertype.SelectedValue = row[0].ToString();
                    //citizen_off.Items.Add(row[0].ToString()+" Y "+row[1].ToString());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }
        private void usersetting_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cSIODataSet.login' table. You can move, or remove it, as needed.
            //this.loginTableAdapter.Fill(this.cSIODataSet.login);

            DisplayData();
            userssetting();
            usersstatus();
             groupBox1.Enabled = false;
            panel1.Enabled = false;
            save.Enabled = false;
           
            update.Enabled = false;
            //this.reportViewer1.RefreshReport();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
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
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
           
            if (password.Text != repassword.Text)
                {
                    MessageBox.Show("पासवर्ड मिलेन,फेरी प्रयास गर्नुहोस ।");
                    repassword.Focus();
                }
                else
                {
                    string s = usertype.SelectedValue.ToString();
                    string st = status.SelectedValue.ToString();
                  
                    string pass = MD5Hash(password.Text);
                    MessageBox.Show(s.ToString() + st.ToString());
                    //SqlCommand qr = new SqlCommand("insert into login (username,password,name,address,phone,status,usertype) values('" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + st + "','" + s + "' )", con);
                    MySqlCommand qr = new MySqlCommand("INSERT INTO login (username,password,name,address,phone,status,usertype ,office_cat, officeid)VALUES(@user, @passwd,@name,@address,@phone,@status,@usertype,@officecat,@officeid)");
                  


                    qr.CommandType = CommandType.Text;
                      qr.Parameters.AddWithValue("@user", username.Text);
                 qr.Parameters.AddWithValue("@passwd", pass.ToString());
                 qr.Parameters.AddWithValue("@name", name.Text);
                 qr.Parameters.AddWithValue("@address", address.Text);
                 qr.Parameters.AddWithValue("@phone", phone.Text);
                 qr.Parameters.AddWithValue("@status", st.ToString());
                 qr.Parameters.AddWithValue("@usertype", s.ToString());
                 qr.Parameters.AddWithValue("@officecat", office_cat.SelectedValue.ToString());
                 qr.Parameters.AddWithValue("@officeid", office_name.SelectedValue.ToString());
        

                    qr.Connection = sqlcon.con;


                    int k = qr.ExecuteNonQuery();

                    if (k > 0)
                    {
                
                
                
                
               // SqlCommand qr = new SqlCommand("INSERT INTO login ([username],[password],[name],[address],[phone],[status],[usertype])VALUES(@username, @passwd, @name, @address, @phone, @status, @usertype)");

               //     qr.CommandType = CommandType.Text;

               //     qr.Parameters.AddWithValue("@user", textBox2.Text);
               //  qr.Parameters.AddWithValue("@passwd", textBox3.Text);
               //  qr.Parameters.AddWithValue("@name", textBox4.Text);
               //  qr.Parameters.AddWithValue("@address", textBox5.Text);
               //  qr.Parameters.AddWithValue("@phone", textBox6.Text);
               //  qr.Parameters.AddWithValue("@status", st.ToString());
               //qr.Parameters.AddWithValue("@usertype", s.ToString());
               //qr.Connection = con;

               


               //     int n = qr.ExecuteNonQuery();

               //     if (n > 0)
                    
                        MessageBox.Show("नयाँ युजर सफलतापुर्वक इन्ट्री भयो ।");
                        sqlcon.con.Close();
                        dataGridView1.DataSource = null;
                        DisplayData();
                      
                        
                        
                    }

                }
           
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

            DialogResult result1 = MessageBox.Show("Do you want to update?",
          "Update User Details",
          MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result1 == DialogResult.Yes)
            {
                string curdate = DateTime.Today.ToString();
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                if (password.Text != repassword.Text)
                {
                    MessageBox.Show("Password Mismatch, Try Again");
                    password.Focus();
                }
                else
                {
                    //MySqlCommand cmd = new MySqlCommand("UPDATE login SET username= @username,password= @password, full_name= @fullname, address= @address, contactno= @phone,status= @status,usertype= @usertype WHERE userid= '" + userid.Text + "'", mysqlcon.conn);
                    MySqlCommand cmd = new MySqlCommand("UPDATE login SET username= @username,password= @password, full_name= @fullname, address= @address, contactno= @phone,status= @status,usertype= @usertype WHERE userid= @uid", sqlcon.con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@uid", userid.Text);
                    cmd.Parameters.AddWithValue("@username", username.Text);
                    cmd.Parameters.AddWithValue("@password", globalfunction.MD5Hash(password.Text));
                    cmd.Parameters.AddWithValue("@usertype", usertype.SelectedValue.ToString());

                    cmd.Parameters.AddWithValue("@fullname", name.Text);
                    cmd.Parameters.AddWithValue("@address", address.Text);
                    cmd.Parameters.AddWithValue("@phone", phone.Text);
                    cmd.Parameters.AddWithValue("@status", status.SelectedValue.ToString());
                    //cmd.Parameters.AddWithValue("@usertype", usertype.SelectedItem);

                    // cmd.Parameters.AddWithValue("@curdate", curdate.ToString());
                    cmd.Connection = sqlcon.con;


                    //int n = qr.ExecuteNonQuery();
                    int n = 0;
                    n = cmd.ExecuteNonQuery();

                    if (n > 0)
                    {

                        sqlcon.con.Close();
                        dataGridView1.DataSource = null;
                        DisplayData();
                        cleardata();
                        MessageBox.Show("Update Successfully Completed", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                        update.Enabled = false;
                        save.Enabled = true;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

      

        private void loginBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            username.Text = dataGridView1.Rows.ToString();
        }

      
        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
         public void cleardata()
        {
            username.Text = "";
            password.Text = "";
            repassword.Text = "";
            name.Text = "";
            address.Text = "";
            phone.Text = "";

        }
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           update.Enabled = true;
            save.Enabled = false;
            int i;
            i=dataGridView1.CurrentRow.Index;
            userid.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            username.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            name.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            address.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            phone.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();

            statusedit(dataGridView1.Rows[i].Cells[6].Value.ToString());
            //  usertype.SelectedItem = dataGridView1.Rows[i].Cells[5].Value.ToString();
            usertypeedit(dataGridView1.Rows[i].Cells[5].Value.ToString());
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
            panel1.Enabled = true;
            save.Enabled = true;
            update.Enabled = false;
        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        
    }
}
