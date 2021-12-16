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
    public partial class vdcedit : Form
    {
        public vdcedit()
        {
            InitializeComponent();
        }
        int maxvdc;
        int maxvdcid;
        public void FillDropDownList(AutoCompleteStringCollection dataCollection)
        {

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
           // //connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;
            sql = "SELECT distcode, distunicodename FROM setup_district ORDER BY distnepname";
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

                district_combo.DataSource = ds.Tables[0];
                district_combo.ValueMember = "distcode";
                district_combo.DisplayMember = "distunicodename";
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    dataCollection.Add(row[1].ToString());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection inside district ! " + ex);
            }

        }
        public void DisplayData()
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }

           // SqlDataAdapter qrey = new SqlDataAdapter("SELECT     VDC_SID, DistCode, VdcNepnam FROM         setup_vdc WHERE     (DistCode = '" + district_combo.SelectedValue + "' )", con);
            MySqlDataAdapter qrey = new MySqlDataAdapter("SELECT     VDC_SID, DistCode,VdcCode As 'कोड न', VdcNepnam AS 'गा वि स / न पा', Vdcname As 'अ‌ग्रेजी नाम',Vdcunicodename As 'यूनिकोडमा नाम' FROM         setup_vdc where DistCode='"+district_combo.SelectedValue+"'", sqlcon.con);
           
            DataTable tb = new DataTable();
            qrey.Fill(tb);
            dataGridView1.DataSource = tb;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
          //  dataGridView1.Columns[3].Visible = false;

            sqlcon.con.Close();

        }
        public void maxv() {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
           // button3.Enabled = true;
            groupBox1.Enabled = true;
            button4.Enabled = true;

            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();
            //string sql = "SELECT    MAX(VDC_SID)+1 FROM         setup_vdc";
            string sql = "SELECT    MAX(VdcCode)+1 FROM         setup_vdc where DistCode='"+district_combo.SelectedValue+"'";


            //string sql = "SELECT VdcCode, VdcNepnam FROM setup_vdc where setup_vdc.DistCode='" + comboBox3.ValueMember + "'";



            command = new MySqlCommand(sql, sqlcon.con);
            adapter.SelectCommand = command;
            adapter.Fill(ds);
            adapter.Dispose();
            command.Dispose();


            foreach (DataRow row in ds.Tables[0].Rows)
            {
                maxvdcid = int.Parse(row[0].ToString());

                //MessageBox.Show(firmid.ToString());
            }
            //SqlDataAdapter qrey = new SqlDataAdapter("SELECT    MAX(VdcCode)+1 FROM         setup_vdc where DistCode='" + district_combo.SelectedValue + "'", con);
            //MessageBox.Show(qrey.ToString());
            //vdcid.Text =  maxvdc.ToString();
        }
        private void vdcedit_Load(object sender, EventArgs e)
        {
            button4.Enabled = false;
            button3.Enabled = false;
            groupBox1.Enabled = false;

            district_combo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            district_combo.AutoCompleteSource = AutoCompleteSource.CustomSource;

            AutoCompleteStringCollection combData = new AutoCompleteStringCollection();
            FillDropDownList(combData);
            district_combo.AutoCompleteCustomSource = combData;

           // district_combo.SelectedValue = 40;
        }

        private void district_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(district_combo.SelectedValue.ToString());
            //DisplayData();
        }

        private void district_combo_ValueMemberChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
           // button3.Enabled = true;
            groupBox1.Enabled = true;
            button4.Enabled = true;

            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();
            string sql = "SELECT    MAX(VDC_SID)+1 FROM         setup_vdc";
            //string sqlvdc = "SELECT    MAX(VdcCode)+1 FROM         setup_vdc where DistCode='"+district_combo.SelectedValue+"'";


            //string sql = "SELECT VdcCode, VdcNepnam FROM setup_vdc where setup_vdc.DistCode='" + comboBox3.ValueMember + "'";



            command = new MySqlCommand(sql, sqlcon.con);
            adapter.SelectCommand = command;
            adapter.Fill(ds);
            adapter.Dispose();
            command.Dispose();


            foreach (DataRow row in ds.Tables[0].Rows)
            {
                maxvdc = int.Parse(row[0].ToString());

                //MessageBox.Show(firmid.ToString());
            }
            //SqlDataAdapter qrey = new SqlDataAdapter("SELECT    MAX(VdcCode)+1 FROM         setup_vdc where DistCode='" + district_combo.SelectedValue + "'", con);
            //MessageBox.Show(qrey.ToString());
            vdcid.Text =  maxvdc.ToString();


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void cleardata(){
            vdcid.Text = null;




            vdc_eng.Text = null;
            vdc_nep.Text = null;
            unicode_txt.Text = null;
        }
        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            button4.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = true;
            groupBox1.Enabled = true;

            int i;
            // string distcode=null;
            //district_combo.SelectedText=null;
            //vdc_combo.SelectedText = " ";
            //industry_scale.SelectedText = " ";
            //industry_type.SelectedText = " ";
            //firm_obj.SelectedText = " ";
            //textBox3.Text = " ";
            //textBox4.Text = " ";
            i = dataGridView1.CurrentRow.Index;
            vdcid.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();




            vdc_eng.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            vdc_nep.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
           unicode_txt.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
          //  district(dataGridView1.Rows[i].Cells[3].Value.ToString());


        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show("विवरण संशोधन गर्ने हो ?",     " विवरण संशोधन गर्न",
      MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result1 == DialogResult.Yes)
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }

              
                //DataTable tb = new DataTable();
                //qrey.Fill(tb);
                //dataGridView1.DataSource = tb;
                //dataGridView1.Columns[0].Visible = false;
                //SqlCommand qr = new SqlCommand(" UPDATE industryreg  SET 'industryregno' = @darta,[regdat] = @dartadate,[renewdate] =  @renewdate,[industryscale] = @industryscale ,[industrytype] = @industrytype ,[industrycat] = @firmobj,[industrynepname] = @firmname ,[branch] = @branch,industrydist = @district ,industryvdc = @vdc,industryward = @ward ,tole = @tole,[karobar] = @karobar,[yearlyturnover] = @turnover,[electricpower] = @electric,[statcapital] = @txtbxthree ,[varcapital] = @txtbxfour ,comment = @comment ,[usr] = @user  ,[updatdate] = @currentdate,[updatnepdate] = @nepdate, [femaleworker] = @female,[maleworker] = @male,tax = @taxs WHERE industryid=@indid");

                MySqlCommand cmd = new MySqlCommand(" UPDATE setup_vdc   SET vdcname = @darta,vdcNepnam = @firmtype, vdcunicodename=@unicode  WHERE VDC_SID=@indid");

                //  INSERT INTO banijyaform('firmregno',[firmtype],[firmdist] ,[firmvdc] ,[firmward],tole,[regdat],[renewdate],[firmscope],[firmnepname],[karobar],[revenue],[branch],comment,[usr],[updatdate],[updatnepdate]) VALUES 
                //                        (@darta, @firmtype, @firmdistrict, @firmvdc, @ward, @tole, @dartadate, @renewdate,                                                                                                                                                                  @firmobj, @firmname, @firmkarobar, @firmcapital, @firmbranch, @comment, @user, @curdate, @entrydate)
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@indid", vdcid.Text);
                cmd.Parameters.AddWithValue("@darta", vdc_eng.Text);
                cmd.Parameters.AddWithValue("@firmtype", vdc_nep.Text);
                cmd.Parameters.AddWithValue("@unicode", unicode_txt.Text);
                cmd.Connection = sqlcon.con;


                //int n = qr.ExecuteNonQuery();
                int n = cmd.ExecuteNonQuery();

                if (n > 0)
                {
                    global.sync_dblog("setup_vdc", vdcid.Text, "UPDATE", vdcid.Text, "VDC_SID");
                    sqlcon.con.Close();
                    dataGridView1.DataSource = null;
                    DisplayData();
                    cleardata();
                    groupBox1.Enabled = false;
                    button2.Enabled = true;
                    MessageBox.Show("संशोधन सफल भयो ।", "संशोधन", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            maxv();

            try
            {
                MySqlCommand cmd = new MySqlCommand("INSERT INTO setup_vdc(VDC_SID,DistCode ,VdcCode ,Vdcname ,VdcNepnam,vdcunicodename) VALUES (@indid,@distcode, @vdccode, @darta, @firmtype, @unicode )");

                // SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[setup_vdc]([VDC_SID],[DistCode],[VdcCode],[Vdcname],[VdcNepnam],[vdcunicodename]) VALUES (@indid, @distcode, @vdccode, @darta, @firmtype, @unicode )");
                //(@indid, @distcode, @vdccode, @darta, @firmtype, @unicode
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@indid", vdcid.Text);
                cmd.Parameters.AddWithValue("@distcode", district_combo.SelectedValue);
                cmd.Parameters.AddWithValue("@vdccode", maxvdcid);
                cmd.Parameters.AddWithValue("@darta", vdc_eng.Text);
                cmd.Parameters.AddWithValue("@firmtype", vdc_nep.Text);
                cmd.Parameters.AddWithValue("@unicode", unicode_txt.Text);
                cmd.Connection = sqlcon.con;
                int n = cmd.ExecuteNonQuery();

                if (n > 0)
                {
                    String uid = global.getSingleDataFromTable("SELECT VDC_SID FROM setup_vdc WHERE vdcunicodename='" + unicode_txt.Text + "' AND DistCode='" + district_combo.SelectedValue + "'");
                    global.sync_dblog("setup_vdc", uid.ToString(), "INSERT", uid.ToString(), "VDC_SID");
                    MessageBox.Show("नयाँ गा‍विस विवरण सेभ भयो ।");
                    sqlcon.con.Close();
                    dataGridView1.DataSource = null;
                    DisplayData();
                    cleardata();
                    groupBox1.Enabled = false;
                    button3.Enabled = false;
               
                    button2.Enabled = true;
                    button4.Enabled = false;




                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            // //connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;
            sql = "SELECT distcode, distunicodename FROM setup_district ";
            ////connection = new SqlConnection(connetionString);
            try
            {
                ////connection.Open();
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
              //  sqlcon.con.Close();

               
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (sqlcon.con.State == ConnectionState.Closed)
                    {
                        sqlcon.con.Open();
                    }
                    string distcode = "0";
                    string distname = "";
                    distcode=(row[0].ToString());
                    distname = (row[1].ToString());
                    MySqlCommand cmd = new MySqlCommand(" UPDATE setup_vdc   SET DistCode = @darta  WHERE Vdcname=@indid");

                    //  INSERT INTO banijyaform('firmregno',[firmtype],[firmdist] ,[firmvdc] ,[firmward],tole,[regdat],[renewdate],[firmscope],[firmnepname],[karobar],[revenue],[branch],comment,[usr],[updatdate],[updatnepdate]) VALUES 
                    //                        (@darta, @firmtype, @firmdistrict, @firmvdc, @ward, @tole, @dartadate, @renewdate,                                                                                                                                                                  @firmobj, @firmname, @firmkarobar, @firmcapital, @firmbranch, @comment, @user, @curdate, @entrydate)
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@indid", distname.ToString());
                    cmd.Parameters.AddWithValue("@darta", distcode.ToString());
                 
                    cmd.Connection = sqlcon.con;


                    //int n = qr.ExecuteNonQuery();
                    int n = cmd.ExecuteNonQuery();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection inside district ! " + ex);
            }


        }
        }
    }

