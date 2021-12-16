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
    public partial class configurationset : Form
    {
        public configurationset()
        {
            InitializeComponent();
            
       
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }
        public void updateoffice()
        {
            
                string curdate = DateTime.Today.ToString();
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
               
                


                     MySqlCommand cmds = new MySqlCommand("UPDATE setup_csio SET isCur=@isCur", sqlcon.con);
                    cmds.CommandType = CommandType.Text;

                    cmds.Parameters.AddWithValue("@isCur", "False");
                   
                    cmds.Connection = sqlcon.con;


                    //int n = qr.ExecuteNonQuery();
                    int m = 0;
                    m = cmds.ExecuteNonQuery();

                    if (m > 0)
                    {

                    //MySqlCommand cmd = new MySqlCommand("UPDATE login SET username= @username,password= @password, full_name= @fullname, address= @address, contactno= @phone,status= @status,usertype= @usertype WHERE userid= '" + userid.Text + "'", mysqlcon.conn);
                    MySqlCommand cmd = new MySqlCommand("UPDATE setup_csio SET isCur=@isCur WHERE csioid=@csioid", sqlcon.con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@isCur", "True");
                    cmd.Parameters.AddWithValue("@csioid", Properties.Settings.Default.officeid);
                    
                    cmd.Connection = sqlcon.con;


                    //int n = qr.ExecuteNonQuery();
                    int n = 0;
                    n = cmd.ExecuteNonQuery();

                    if (n > 0)
                    {

                        sqlcon.con.Close();
                        
                        MessageBox.Show("Update Successfully Completed", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                     }
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
            sql = "SELECT csioid,  Concat(csioNepNm,'- ',officeaddress) As 'office' FROM setup_csio ";
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


                office.DataSource = ds.Tables[0];
                office.ValueMember = "csioid";
                office.DisplayMember = "office";

                sqlcon.con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }
        //public void FillDropDownList(AutoCompleteStringCollection dataCollection)
        
        //    {
        //    MySqlCommand command;
        //    MySqlDataAdapter adapter = new MySqlDataAdapter();
        //    DataSet ds = new DataSet();

        //    string sql = null;
        //    // //connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;
        //    sql = "SELECT distcode, distunicodename FROM setup_district ORDER BY distunicodename";
        //    ////connection = new SqlConnection(connetionString);
        //    try
        //    {
        //        if (sqlcon.con.State == ConnectionState.Closed)
        //        {
        //            sqlcon.con.Open();
        //        }
        //        command = new MySqlCommand(sql, sqlcon.con);
        //        adapter.SelectCommand = command;
        //        adapter.Fill(ds);
        //        adapter.Dispose();
        //        command.Dispose();
        //        sqlcon.con.Close();

        //        comboBox1.DataSource = ds.Tables[0];
        //        comboBox1.ValueMember = "distcode";
        //        comboBox1.DisplayMember = "distunicodename";
        //        foreach (DataRow row in ds.Tables[0].Rows)
        //        {
        //            dataCollection.Add(row[1].ToString());
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Can not open connection inside district ! " + ex);
        //    }

        //}
        private void Form2_Load(object sender, EventArgs e)
        {
            if (global.configid == "1")
            {
                office.Visible = true;
                officename();
                office.SelectedValue = Properties.Settings.Default.officeid;
                //AutoCompleteStringCollection combData = new AutoCompleteStringCollection();
                //FillDropDownList(combData);
                //comboBox1.AutoCompleteCustomSource = combData;

                //comboBox1.SelectedValue = 40;
            }
            else
            {
                office.Visible = false;


            }

            servertxt.Text = Properties.Settings.Default.servername;
            databasetxt.Text = Properties.Settings.Default.databasename;
            dbusertxt.Text = Properties.Settings.Default.username;
            dbpasswordtxt.Text = Properties.Settings.Default.password;
            rpt_address.Text = Properties.Settings.Default.reportaddress;
            
            
           
            
                        





        }

        private void server_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void save_Click(object sender, EventArgs e)
        {
              DialogResult result1 = MessageBox.Show("Do you want to update?",
          "Update System Configuration ",
          MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
              if (result1 == DialogResult.Yes)
              {
                  Properties.Settings.Default.servername = servertxt.Text;
                  Properties.Settings.Default.databasename = databasetxt.Text;
                  Properties.Settings.Default.username = dbusertxt.Text;
                  Properties.Settings.Default.password = dbpasswordtxt.Text;
                  Properties.Settings.Default.reportaddress = rpt_address.Text;

                if (global.configid == "1")
                {
                    Properties.Settings.Default.officeid = office.SelectedValue.ToString();

                    updateoffice();
                }
                Properties.Settings.Default.Save();
            }
        }
    }
}
