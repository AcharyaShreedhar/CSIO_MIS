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
    public partial class setup_ain : Form
    {
        public setup_ain()
        {
            InitializeComponent();
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
        public void dafaloading()
        {
            // MessageBox.Show("Working");
            // //string connetionString = null;
            // //SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            //connetionString = mysqlcon.conn.ToString();
            sql = "SELECT dafaid, dafa FROM setup_dafa ";
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


                dafa_drop.DataSource = ds.Tables[0];
                dafa_drop.ValueMember = "dafaid";
                dafa_drop.DisplayMember = "dafa";
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
        public void scale()
        {
          
            
            // MessageBox.Show("Working");
            // //string connetionString = null;
            // //SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            //connetionString = mysqlcon.conn.ToString();
            sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (setup_subcategory.category_id = 8) ";
            //connection = new MySqlConnection(mysqlcon.conn);
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                command = new MySqlCommand(sql, sqlcon.con);
               // MySqlCommand command;
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();


                scale_typedrop.DataSource = ds.Tables[0];
                scale_typedrop.ValueMember = "subcategory_id";
                scale_typedrop.DisplayMember = "subcategory_unicodename";
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
        public void type()
        {
            // MessageBox.Show("Working");
            // //string connetionString = null;
            // //SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            //connetionString = mysqlcon.conn.ToString();
            sql = "SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (setup_subcategory.category_id = 10)";
            //connection = new MySqlConnection(mysqlcon.conn);
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
               // MySqlCommand command;
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();


                scale_typedrop.DataSource = ds.Tables[0];
                scale_typedrop.ValueMember = "subcategory_id";
                scale_typedrop.DisplayMember = "subcategory_unicodename";
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
        private void setup_ain_Load(object sender, EventArgs e)
        {
            save_btn.Enabled = false;
            update_btn.Enabled = false;
            new_btn.Enabled = true;
            save_btn1.Enabled = false;
            update_btn1.Enabled = false;
            groupBox2.Enabled = false;
            new_btn1.Enabled = true;
            ainloading();
            dafaloading();
            DisplayData();
            
            scale();
            cur_ain.SelectedText = "0";
            DisplayData1();
        }

        private void new_btn_Click(object sender, EventArgs e)
        {
            save_btn.Enabled = true;
            update_btn.Enabled = false;
            groupBox1.Enabled = true;
        }
        public void DisplayData()
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }

            // SqlDataAdapter qrey = new SqlDataAdapter("SELECT     VDC_SID, DistCode, VdcNepnam FROM         setup_vdc WHERE     (DistCode = '" + district_combo.SelectedValue + "' )", con);
            MySqlDataAdapter qrey = new MySqlDataAdapter("SELECT     ainid As 'ऐन कोड', ainname As 'औद्योगिक व्यवसाय ऐन' FROM         setup_ain", sqlcon.con);

            DataTable tb = new DataTable();
            qrey.Fill(tb);
            dataGridView1.DataSource = tb;
          //  dataGridView1.Columns[0].Visible = false;
          //  dataGridView1.Columns[1].Visible = false;

            sqlcon.con.Close();

        }
        public void DisplayData1()
        {
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }

                // SqlDataAdapter qrey = new SqlDataAdapter("SELECT     VDC_SID, DistCode, VdcNepnam FROM         setup_vdc WHERE     (DistCode = '" + district_combo.SelectedValue + "' )", con);
                MySqlDataAdapter qrey = new MySqlDataAdapter("SELECT     ain_dafa.Ain_id, setup_subcategory.subcategory_unicodename As 'स्तर/बर्ग', ain_dafa.dafaid, ain_dafa.dafaname, setup_ain.ainname AS 'औद्योगिक ब्यवसाय ऐन',ain_dafa.subcategory_id, cur FROM         ain_dafa INNER JOIN  setup_ain ON ain_dafa.ain = setup_ain.ainid INNER JOIN setup_subcategory ON ain_dafa.subcategory_id = setup_subcategory.subcategory_id WHERE ain=@aindrop AND dafaid=@dafadrop", sqlcon.con);
                qrey.SelectCommand.Parameters.AddWithValue("@aindrop", ain_drop.SelectedValue.ToString());
                qrey.SelectCommand.Parameters.AddWithValue("@dafadrop", dafa_drop.SelectedValue.ToString());
                DataTable tb = new DataTable();
                qrey.Fill(tb);
                dataGridView2.DataSource = tb;
                //  dataGridView1.Columns[0].Visible = false;
                //  dataGridView1.Columns[5].Visible = false;

                sqlcon.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        public void cleardata()
        {
            ain_txt.Text = "";
        }
        private void save_btn_Click(object sender, EventArgs e)
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
           

            try
            {
                MySqlCommand cmd = new MySqlCommand("INSERT INTO setup_ain(ainname) VALUES (@ain)");

                // SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[setup_vdc]([VDC_SID],[DistCode],[VdcCode],[Vdcname],[VdcNepnam],[vdcunicodename]) VALUES (@indid, @distcode, @vdccode, @darta, @firmtype, @unicode )");
                //(@indid, @distcode, @vdccode, @darta, @firmtype, @unicode
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ain", ain_txt.Text);
                
                cmd.Connection = sqlcon.con;
                int n = cmd.ExecuteNonQuery();

                if (n > 0)
                {
                    String uid = global.getSingleDataFromTable("SELECT ainid FROM setup_ain WHERE ainname='" + ain_txt.Text + "'");
                    global.sync_dblog("setup_ain", uid.ToString(), "INSERT", uid.ToString(), "ainid");
                    MessageBox.Show("नयाँ औद्योगिक व्यवसाय ऐन सेभ भयो ।");
                    sqlcon.con.Close();
                    dataGridView1.DataSource = null;
                    DisplayData();
                    cleardata();
                    ainloading();
                    dafaloading();
                    ainloading();
                    groupBox1.Enabled = false;
                    save_btn.Enabled = false;

                   new_btn.Enabled = true;
                    update_btn.Enabled = false;




                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
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
            ainid.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            ain_txt.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
        }

        private void update_btn_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show("विवरण संशोधन गर्ने हो ?", " विवरण संशोधन गर्न",
     MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result1 == DialogResult.Yes)
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }


                MySqlCommand cmd = new MySqlCommand(" UPDATE setup_ain   SET ainname = @ainname  WHERE ainid=@indid");

                //  INSERT INTO banijyaform('firmregno',[firmtype],[firmdist] ,[firmvdc] ,[firmward],tole,[regdat],[renewdate],[firmscope],[firmnepname],[karobar],[revenue],[branch],comment,[usr],[updatdate],[updatnepdate]) VALUES 
                //                        (@darta, @firmtype, @firmdistrict, @firmvdc, @ward, @tole, @dartadate, @renewdate,                                                                                                                                                                  @firmobj, @firmname, @firmkarobar, @firmcapital, @firmbranch, @comment, @user, @curdate, @entrydate)
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@indid", ainid.Text);
                cmd.Parameters.AddWithValue("@ainname", ain_txt.Text);
                
                cmd.Connection = sqlcon.con;


                //int n = qr.ExecuteNonQuery();
                int n = cmd.ExecuteNonQuery();

                if (n > 0)
                {
                    
                    global.sync_dblog("setup_ain", ainid.Text, "UPDATE", ainid.Text, "ainid");
                    sqlcon.con.Close();
                    dataGridView1.DataSource = null;
                    DisplayData();
                    cleardata();
                    groupBox1.Enabled = false;
                    new_btn.Enabled = true;
                    MessageBox.Show("संशोधन सफल भयो ।", "संशोधन", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void new_btn1_Click(object sender, EventArgs e)
        {
            save_btn1.Enabled = true;
            update_btn1.Enabled = false;
            groupBox2.Enabled = true;
        }

        private void ain_drop_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void save_btn1_Click(object sender, EventArgs e)
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }


            try
            {
                MySqlCommand cmd = new MySqlCommand("INSERT INTO ain_dafa(dafaid,dafaname,ain,subcategory_id,cur) VALUES (@dafaid,@dafaname,@ain,@subcat,@curstat)");

                // SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[setup_vdc]([VDC_SID],[DistCode],[VdcCode],[Vdcname],[VdcNepnam],[vdcunicodename]) VALUES (@indid, @distcode, @vdccode, @darta, @firmtype, @unicode )");
                //(@indid, @distcode, @vdccode, @darta, @firmtype, @unicode
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@dafaid", dafa_drop.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@dafaname", dafa_txt.Text);
                cmd.Parameters.AddWithValue("@ain", ain_drop.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@subcat", scale_typedrop.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@curstat", cur_ain.SelectedText.ToString());
                cmd.Connection = sqlcon.con;
                int n = cmd.ExecuteNonQuery();

                if (n > 0)
                {
                    String uid = global.getSingleDataFromTable("SELECT Ainid FROM ain_dafa WHERE dafaname='" + dafa_txt.Text + "'");
                    global.sync_dblog("ain_dafa", uid.ToString(), "INSERT", uid.ToString(), "Ainid");
                    MessageBox.Show("नयाँ औद्योगिक व्यवसाय ऐनको दफा सेभ भयो ।");
                    sqlcon.con.Close();
                    dataGridView1.DataSource = null;
                    DisplayData1();
                    cleardata();
                    groupBox2.Enabled = false;
                    save_btn1.Enabled = false;

                    new_btn1.Enabled = true;
                    update_btn1.Enabled = false;




                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dafa_drop_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void ain_drop_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        private void dafa_drop_SelectedValueChanged(object sender, EventArgs e)
        {
         
        }

        private void ain_drop_SelectionChangeCommitted(object sender, EventArgs e)
        {
           // DisplayData1();
        }

        private void dafa_drop_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DisplayData1();
            if (dafa_drop.SelectedValue.ToString() == "1")
            {
                scale();
            }
            else
            {
                type();
            }
        }
        public void scaleedit(string dist)
        {

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
           
           // string connetionStrings = null;
           //// MySqlConnection connections;
            MySqlCommand commands;
            MySqlDataAdapter adapters = new MySqlDataAdapter();
            DataSet dss = new DataSet();

            string sqls = null;
            //int distcodevalue = Convert.ToInt16(district_combo.SelectedValue);
         //   //connetionStrings = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";

            //sqls = "SELECT VdcCode,VdcNepnam FROM  setup_vdc where DistCode='" +district_combo.SelectedValue+ "'";
            sqls = " SELECT     subcategory_id, subcategory_unicodename FROM setup_category INNER JOIN setup_subcategory ON setup_category.category_id = setup_subcategory.category_id WHERE (subcategory_unicodename = @district)";

         //   connections = new MySqlConnection(connetionStrings);

         //   //connections.Open();
            commands = new MySqlCommand(sqls, sqlcon.con);
            adapters.SelectCommand = commands;
            adapters.SelectCommand.Parameters.AddWithValue("@district", dist.ToString());
            adapters.Fill(dss);
            adapters.Dispose();
            commands.Dispose();
            sqlcon.con.Close();


            foreach (DataRow row in dss.Tables[0].Rows)
            {

                scale_typedrop.SelectedValue = row[0].ToString();
            }

        }
        public void ainedit(string dist)
        {

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
           
           // string connetionStrings = null;
          //  MySqlConnection connections;
            MySqlCommand commands;
            MySqlDataAdapter adapters = new MySqlDataAdapter();
            DataSet dss = new DataSet();

            string sqls = null;
            //int distcodevalue = Convert.ToInt16(district_combo.SelectedValue);
           // //connetionStrings = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";

            //sqls = "SELECT VdcCode,VdcNepnam FROM  setup_vdc where DistCode='" +district_combo.SelectedValue+ "'";
            sqls = " SELECT ainid, ainname FROM setup_ain WHERE (ainname = @district)";

         //   connections = new MySqlConnection(connetionStrings);

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

                ain_drop.SelectedValue = row[0].ToString();
            }

        }
        private void dataGridView2_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            save_btn1.Enabled = false;
            update_btn1.Enabled = true;
            groupBox2.Enabled = true;
            int i;
            
            i = dataGridView2.CurrentRow.Index;
            dafaid_txt.Text = dataGridView2.Rows[i].Cells[0].Value.ToString();
           ainedit(dataGridView2.Rows[i].Cells[4].Value.ToString());
          // MessageBox.Show(dataGridView2.Rows[i].Cells[3].Value.ToString());
           dafa_drop.SelectedValue = (dataGridView2.Rows[i].Cells[2].Value.ToString());
           cur_ain.SelectedItem = (dataGridView2.Rows[i].Cells[6].Value.ToString());
            scaleedit(dataGridView2.Rows[i].Cells[1].Value.ToString());
            dafa_txt.Text = dataGridView2.Rows[i].Cells[3].Value.ToString();
        }

        private void update_btn1_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show("दफा विवरण संशोधन गर्ने हो ?", " विवरण संशोधन गर्न",
    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result1 == DialogResult.Yes)
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }


                MySqlCommand cmd = new MySqlCommand(" UPDATE ain_dafa   SET dafaid = @dafaid,dafaname=@dafaname,ain=@ain,subcategory_id=@subcat,cur=@curstat WHERE Ain_id=@indid");

                //  INSERT INTO banijyaform('firmregno',[firmtype],[firmdist] ,[firmvdc] ,[firmward],tole,[regdat],[renewdate],[firmscope],[firmnepname],[karobar],[revenue],[branch],comment,[usr],[updatdate],[updatnepdate]) VALUES 
                //                        (@darta, @firmtype, @firmdistrict, @firmvdc, @ward, @tole, @dartadate, @renewdate,                                                                                                                                                                  @firmobj, @firmname, @firmkarobar, @firmcapital, @firmbranch, @comment, @user, @curdate, @entrydate)
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@indid", dafaid_txt.Text);
                cmd.Parameters.AddWithValue("@dafaid", dafa_drop.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@dafaname", dafa_txt.Text);
                cmd.Parameters.AddWithValue("@ain", ain_drop.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@subcat", scale_typedrop.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@curstat", cur_ain.SelectedText.ToString());
                cmd.Connection = sqlcon.con;


                //int n = qr.ExecuteNonQuery();
                int n = cmd.ExecuteNonQuery();

                if (n > 0)
                {
                    global.sync_dblog("ain_dafa", dafaid_txt.Text, "UPDATE", dafaid_txt.Text, "Ainid");
                    sqlcon.con.Close();
                    dataGridView2.DataSource = null;
                    DisplayData1();
                    cleardata();
                    groupBox2.Enabled = false;
                    new_btn1.Enabled = true;
                    MessageBox.Show("संशोधन सफल भयो ।", "संशोधन", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
        }
    }
}
