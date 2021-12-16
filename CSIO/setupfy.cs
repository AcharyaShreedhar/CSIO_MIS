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
    public partial class setupfy : Form
    {
        public setupfy()
        {
            InitializeComponent();
        }
        public void Displayfy()
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            ////////////////////////////////////////
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();
            string sql = "SELECT    GetNumberToUnicode(format_fiscal_yr(FY)),FY_ID from setup_fy where IsCur='True' ";

            //string sql = "SELECT VdcCode, VdcNepnam FROM setup_vdc where setup_vdc.DistCode='" + comboBox3.ValueMember + "'";



            command = new MySqlCommand(sql, sqlcon.con);
            adapter.SelectCommand = command;
            adapter.Fill(ds);
            adapter.Dispose();
            command.Dispose();


            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT  FY_ID,  GetNumberToUnicode(format_fiscal_yr(FY)) As 'आर्थिक वर्ष',IsCur As 'चालु आ.व.', FY_StrtDate As 'शुरु मिति(ई.सं.)', FY_EndDate As 'अन्तिम मिति(ई.सं.)' from setup_fy", sqlcon.con);
            
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView1.DataSource = tb;
            dataGridView1.Columns[0].Visible = false;
           // dataGridView1.Columns[14].Visible = false;
            sqlcon.con.Close();
        }
        private void setupfy_Load(object sender, EventArgs e)
        {
            Displayfy();
            save_btn.Enabled = false;
            groupBox1.Enabled = false;
            update_btn.Enabled = false;
        }

        private void new_btn_Click(object sender, EventArgs e)
        {
            save_btn.Enabled = true;
            groupBox1.Enabled = true;
            update_btn.Enabled = false;
          
        }
        public void getfalse() {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            try
            {
                //  MySqlCommand cmd = new MySqlCommand("INSERT INTO setup_fy(FY_ID, FY, IsCur, FY_StrtDate, FY_EndDate) VALUES (max(FY_ID)+1,Getint(GetUnicodeToNumber(@fy)),GetUnicodeToNumber(@start),GetUnicodeToNumber(@end),@iscur)");
                MySqlCommand cmd = new MySqlCommand("Update setup_fy SET IsCur=False");
                DateTime now = DateTime.Now;

                cmd.CommandType = CommandType.Text;
                //  ,,,,,,,,,,,, )");
             

                cmd.Connection = sqlcon.con;
                int n = cmd.ExecuteNonQuery();

                if (n > 0)
                {
                   



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        
    }
        private void save_btn_Click(object sender, EventArgs e)
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            try
            {
                if(iscur.Checked==true)
                {
                    getfalse();
                }
              //  MySqlCommand cmd = new MySqlCommand("INSERT INTO setup_fy(FY_ID, FY, IsCur, FY_StrtDate, FY_EndDate) VALUES (max(FY_ID)+1,Getint(GetUnicodeToNumber(@fy)),GetUnicodeToNumber(@start),GetUnicodeToNumber(@end),@iscur)");
                MySqlCommand cmd = new MySqlCommand("INSERT INTO setup_fy( FY, FY_StrtDate, FY_EndDate,IsCur) VALUES (Getint(GetUnicodeToNumber(@fy)),GetUnicodeToNumber(@start),GetUnicodeToNumber(@end),@iscur)");
                DateTime now = DateTime.Now;

                cmd.CommandType = CommandType.Text;
                //  ,,,,,,,,,,,, )");
                cmd.Parameters.AddWithValue("@fy", fy_txt.Text);
                cmd.Parameters.AddWithValue("@start", DateTime.Parse(startdate.Text));
                cmd.Parameters.AddWithValue("@end", DateTime.Parse(enddate.Text));
              cmd.Parameters.AddWithValue("@iscur", iscur.Checked);
               
                cmd.Connection = sqlcon.con;
                int n = cmd.ExecuteNonQuery();

                if (n > 0)
                {
                    global.sync_dblog("setup_fy", fy_txt.Text, "INSERT", fy_txt.Text, "FY");
                    MessageBox.Show("नयाँ आर्थिक वर्ष सफलता पूर्वक सेभ गरियो ।", "नयाँ आर्थिक वर्ष थप");
                    sqlcon.con.Close();
                    //dataGridView1.DataSource = null;
                    Displayfy();
                    // cleardata();
                    // groupBox1.Enabled = false;
                    // update_btn.Enabled = false;

                    new_btn.Enabled = true;
                    save_btn.Enabled = false;




                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void update_btn_Click(object sender, EventArgs e)
        {

        }
    }
}
