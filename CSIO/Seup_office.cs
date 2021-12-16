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
    public partial class Seup_office : Form
    {
        public Seup_office()
        {
            InitializeComponent();
        }
        bool isReady = false;
        string tablename = "setup_office";
        private void saveOffice()
        {
            try
            {
                DialogResult result1 = MessageBox.Show("कार्यालयको बिवरण संशोधन गर्ने हो ?",
           "कार्यालयको विवरण संशोधन गर्न",
           MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result1 == DialogResult.Yes)
                {
                    if (sqlcon.con.State == ConnectionState.Closed)
                    {
                        sqlcon.con.Open();
                    }
                    //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");
                    string curdate = DateTime.Today.ToString();
                    // con.Open();

                    MySqlCommand qr = new MySqlCommand(" UPDATE "+tablename.ToString()+" SET `provinceid`=@province,`officeDist`=@dist,`officeNm`=@officenameeng,`office_address`=@officeaddress,`officeunicodename`=@officeNepName,`phone`=@phone,`fax`=@fax,`email`=@email,`departmentname`=@department,`ministryname`=@ministry,`provincename`=@provincename WHERE officeid=@officeid");


                    qr.CommandType = CommandType.Text;

                    qr.Parameters.AddWithValue("@officeid", 1);
                    // qr.Parameters.AddWithValue("@darta", darta_txt.Text);
                    qr.Parameters.AddWithValue("@province", province.SelectedValue);
                    qr.Parameters.AddWithValue("@dist", district_combo.SelectedValue);
                    qr.Parameters.AddWithValue("@officenameeng", office_engname.Text);
                    qr.Parameters.AddWithValue("@officeaddress", district_combo.Text);
                    qr.Parameters.AddWithValue("@officeNepName", office_name.Text);
                    qr.Parameters.AddWithValue("@phone", Phone.Text);
                    qr.Parameters.AddWithValue("@fax", Fax.Text);
                    qr.Parameters.AddWithValue("@email", email.Text);
                    qr.Parameters.AddWithValue("@department", department.Text);
                    qr.Parameters.AddWithValue("@ministry", ministry.Text);
                    qr.Parameters.AddWithValue("@provincename", province.Text);
                    
                    qr.Connection = sqlcon.con;


                    //int n = qr.ExecuteNonQuery();



                    //int n = qr.ExecuteNonQuery();
                    int n = qr.ExecuteNonQuery();

                    if (n > 0)
                    {

                        sqlcon.con.Close();
                        
                        MessageBox.Show("संशोधन सफल भयो ।", "संशोधन", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                     

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void province_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isReady)
                global.fillCombo(district_combo, "SELECT distcode, distunicodename FROM setup_district WHERE distzonecd=" + province.SelectedValue, "distunicodename", "distcode");
        }
        private void Seup_office_Load(object sender, EventArgs e)
        {
            isReady = false;
            
global.fillCombo(province, "SELECT provinceid, provincenep From setup_province", "provincenep", "provinceid");
            province.SelectedValue = global.myProvinceId;
           // if (isReady)
                global.fillCombo(district_combo, "SELECT distcode, distunicodename FROM setup_district WHERE distzonecd=" + province.SelectedValue, "distunicodename", "distcode");
            DisplayData();
            setupfaat();

        }
        public void setupfaat()
        {
            // MessageBox.Show("Working");
            // //string connetionString = null;
            // //SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            //connetionString = mysqlcon.conn.ToString();


            sql = "SELECT faat_id,  faat_name FROM setup_faat where faat_id<=2 ";

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


                faat.DataSource = ds.Tables[0];
                faat.ValueMember = "faat_id";
                faat.DisplayMember = "faat_name";


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

            string stmt = "SELECT `officeid`, `provinceid`, `officeDist`, `officeNm`, `office_address`, `officeunicodename`, `isCur`, `phone`, `fax`, `email`, `departmentname`, `ministryname`, `govtname`, `provincename` FROM "+tablename.ToString()+"  WHERE  officeid =1";

            MySqlDataAdapter qry = new MySqlDataAdapter(stmt, sqlcon.con);
            //qry.SelectCommand.Parameters.AddWithValue("@darta", indID);

            DataTable tb = new DataTable();

            qry.Fill(tb);
            dataGridView1.DataSource = tb;
            foreach (DataRow row in tb.Rows)
            {
                ministry.Text = row["ministryname"].ToString();
                department.Text = row["departmentname"].ToString();
                office_engname.Text = row["officeNm"].ToString();
                office_name.Text = row["officeunicodename"].ToString();
                Phone.Text = row["phone"].ToString();
                Fax.Text = row["fax"].ToString();
                email.Text = row["email"].ToString();
              district_combo.SelectedValue= row["officeDist"].ToString();
            }
          

            sqlcon.con.Close();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            saveOffice();
        }

        private void faat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(faat.SelectedValue.ToString()=="2")
            {
                tablename = "setup_officebanijya";
            }
            else
            {
                tablename = "setup_office";
            }

            DisplayData();
        }
    }
}
