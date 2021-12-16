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
    public partial class changeindustryaddress : Form
    {
        public changeindustryaddress()
        {
            InitializeComponent();
        }

        private void changeindustryaddress_Load(object sender, EventArgs e)
        {
            
            button4.Enabled = false;
            button1.Enabled = false;
            groupBox1.Enabled = false;
           // d_date.Text = global.todaynepslash;
            user_txt.Text = global.username;
            
         
            DisplayData();
         

            district_combo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            district_combo.AutoCompleteSource = AutoCompleteSource.CustomSource;

            AutoCompleteStringCollection combData = new AutoCompleteStringCollection();
            FillDropDownList(combData);
            district_combo.AutoCompleteCustomSource = combData;

            district_combo.SelectedValue = 40;
        }
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
            sql = "SELECT distcode, distunicodename FROM setup_district ORDER BY distunicodename";
           // connection = new MySqlConnection(connetionString);
            try
            {
              //  //connection.Open();
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

        public void FillDropDownListvdc(AutoCompleteStringCollection dataCollectionvdcs)
        {

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            MySqlCommand commands;
            MySqlDataAdapter adapters = new MySqlDataAdapter();
            DataSet dss = new DataSet();

            string sqls = null;
            //int distcodevalue = Convert.ToInt16(district_combo.SelectedValue);
            ////connetionStrings = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";

            //sqls = "SELECT VdcCode,VdcNepnam FROM  setup_vdc where DistCode='" +district_combo.SelectedValue+ "'";
            sqls = "SELECT VDC_SID,Vdcunicodename FROM  setup_vdc where DistCode='" + district_combo.SelectedValue + "'";


         //   //connections = new SqlConnection(connetionStrings);
            try
            {
                ////connections.Open();
                commands = new MySqlCommand(sqls, sqlcon.con);
                adapters.SelectCommand = commands;
                adapters.Fill(dss);
                adapters.Dispose();
                commands.Dispose();
                sqlcon.con.Close();

                vdc_combo.DataSource = dss.Tables[0];
                vdc_combo.ValueMember = "VDC_SID";
                vdc_combo.DisplayMember = "Vdcunicodename";
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
        public void getid(int indusid, string trans,string ddate)
        {
            label5.Text = null;
            label5.Text = indusid.ToString();
            darta_no.Text = null;
            darta_no.Text = trans.ToString();
            d_date.Text = ddate.ToString();

            //date_txt.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }
        //public void DisplayData()
        //{
        //    SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.passwordname + "' ");

        //    con.Open();

        //    MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     industryreg.industryid, industryreg.industryregno AS 'दर्ता नं.', industryreg.industrynepname AS 'उद्योगको नाम' From  industryreg where industryreg.industryid=@darta ", con);
        //    qry.SelectCommand.Parameters.AddWithValue("@darta", label5.Text);
        //    DataTable tb = new DataTable();
        //    qry.Fill(tb);
        //    dataGridView1.DataSource = tb;
        //    dataGridView1.Columns[0].Visible = false;
        //    sqlcon.con.Close();

        //}

        private void changeindustryname_Load(object sender, EventArgs e)
        {
            d_date.Text = global.todaynepslash;
            user_txt.Text = global.username;
            DisplayData();
            groupBox1.Enabled = false;
            button4.Enabled = false;
        }

      
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i;
            i = dataGridView1.CurrentRow.Index;
            int industryid = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
            string regno = dataGridView1.Rows[i].Cells[1].Value.ToString();
            string district = dataGridView1.Rows[i].Cells[3].Value.ToString();
            string vdc = dataGridView1.Rows[i].Cells[4].Value.ToString();
            string ward = dataGridView1.Rows[i].Cells[5].Value.ToString();
            string tole = dataGridView1.Rows[i].Cells[6].Value.ToString();
            string kitta = dataGridView1.Rows[i].Cells[7].Value.ToString();
            try
            {

                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }

                // string curdate = DateTime.Today.ToString();

                //SqlCommand qr = new SqlCommand("INSERT INTO update_industry (industryid,industryreg ,dartanum,updattitle,updatnew,oldrec,[updatedate],updatenepdate,updateuser) VALUES (@industryid, @industryregno, @dartanum, @updatetitle, @updatenew, @oldvalue, @updatedate, @updatenepdate, @updateuser)");
                MySqlCommand qr = new MySqlCommand("INSERT INTO update_industry (industryid,industryreg ,dartanum,updattitle,updatnew,oldrec,updatenepdate,updateuser,decisiondate,tax,comment) VALUES (@industryid, GetUnicodeToNumber(@industryregno), @dartanum, @updatetitle, concat(@updatenewvdc,' ',GetNumberToUnicode(@updatenewward),' ',@updatenewdist,GetNumberToUnicode(@updatenewkitta)), concat(@oldvdc,' ',GetNumberToUnicode(@oldward),' ',@oldtoledist,GetNumberToUnicode(@oldkittanum)), @updatenepdate, @updateuser, @ddate, @tax, @comment)");

                qr.CommandType = CommandType.Text;
                qr.Parameters.AddWithValue("@industryid", industryid.ToString());
                qr.Parameters.AddWithValue("@industryregno", regno.ToString());
                qr.Parameters.AddWithValue("@dartanum", darta_no.Text);
                qr.Parameters.AddWithValue("@updatetitle", updateid.Text);
                qr.Parameters.AddWithValue("@tax", textBox2.Text);
                //qr.Parameters.AddWithValue("@updatenew", textBox1.Text);
                //qr.Parameters.AddWithValue("@oldvalue", statvalue.ToString());
                // MessageBox.Show(vdc_combo.Text);
                //qr.Parameters.AddWithValue("@updatenew", vdc_combo.Text + " " + textBox3.Text + " " + tole_txt.Text + ", " + district_combo.Text);
                qr.Parameters.AddWithValue("@updatenewvdc", vdc_combo.Text);
                qr.Parameters.AddWithValue("@updatenewward",  textBox3.Text);
                qr.Parameters.AddWithValue("@updatenewdist", tole_txt.Text + ", " + district_combo.Text+" कित्ता नं.:");
                qr.Parameters.AddWithValue("@updatenewkitta", kittanum.Text);

                qr.Parameters.AddWithValue("@oldvdc", vdc.ToString());
                qr.Parameters.AddWithValue("@oldward", ward.ToString());
                qr.Parameters.AddWithValue("@oldtoledist", tole.ToString() + ", " + district.ToString() + " कित्ता नं.:");
                qr.Parameters.AddWithValue("@oldkittanum", kitta.ToString());
              //  qr.Parameters.AddWithValue("@oldvalue", vdc.ToString() + " " + ward.ToString() + " " + tole.ToString() + ", " + district.ToString());

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
                    //  cleardata();
                    MySqlCommand qrs = new MySqlCommand("UPDATE industryreg  SET industrydist = @txtbxthree ,industryvdc = @txtbxfour ,industryward = GetUnicodeToNumber(@ward), tole = @tole, kittanum = @kittanum  WHERE industryid=@indid");


                    qrs.CommandType = CommandType.Text;


                    qrs.Parameters.AddWithValue("@txtbxthree", district_combo.SelectedValue);
                    qrs.Parameters.AddWithValue("@txtbxfour", vdc_combo.SelectedValue);
                    qrs.Parameters.AddWithValue("@ward", textBox3.Text);
                    qrs.Parameters.AddWithValue("@tole", tole_txt.Text);
                    qrs.Parameters.AddWithValue("@kittanum", kittanum.Text);
                    qrs.Parameters.AddWithValue("@indid", industryid.ToString());
                    //qr.Parameters.AddWithValue("@currentdate", curdate);
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
                        groupBox1.Enabled = false;
                        button4.Enabled = false;
                        // button4.Enabled = false;
                        button1.Enabled = false;
                        // cleardata();
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

        private void tole_txt_TextChanged(object sender, EventArgs e)
        {

        }

        public void district(string dist)
        {

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            MySqlCommand commands;
            MySqlDataAdapter adapters = new MySqlDataAdapter();
            DataSet dss = new DataSet();

            string sqls = null;
            //int distcodevalue = Convert.ToInt16(district_combo.SelectedValue);
          //  //connetionStrings = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";

            //sqls = "SELECT VdcCode,VdcNepnam FROM  setup_vdc where DistCode='" +district_combo.SelectedValue+ "'";
            sqls = " SELECT     distcode, distunicodename FROM         setup_district WHERE     (distunicodename = '" + dist.ToString() + "')";

           // //connections = new SqlConnection(connetionStrings);

           // //connections.Open();
            commands = new MySqlCommand(sqls, sqlcon.con);
            adapters.SelectCommand = commands;
            adapters.Fill(dss);
            adapters.Dispose();
            commands.Dispose();
            sqlcon.con.Close();


            foreach (DataRow row in dss.Tables[0].Rows)
            {

                district_combo.SelectedValue = row[0].ToString();
            }

        }
        public void DisplayData()
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }

            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     industryreg.industryid, GetNumberToUnicode(industryreg.industryregno) AS 'दर्ता नं', industryreg.industrynepname AS 'उद्योगको नाम', setup_district.distunicodename AS 'जिल्ला',setup_vdc.vdcunicodename As 'गा.पा.। न.पा.',GetNumberToUnicode(industryreg.industryward) As 'वार्ड', industryreg.tole AS 'टोल', GetNumberToUnicode(industryreg.kittanum) AS 'कित्ता नं' FROM          setup_district INNER JOIN    industryreg ON setup_district.distcode = industryreg.industrydist INNER JOIN    setup_vdc ON industryreg.industryvdc = setup_vdc.VDC_SID  where industryreg.industryid=@darta ", sqlcon.con);
            //  MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     industryreg.industryid, industryreg.industryregno AS 'दर्ता नं.', industryreg.industrynepname AS 'उद्योगको नाम' From  industryreg where industryreg.industryid=@darta ", con);
              qry.SelectCommand.Parameters.AddWithValue("@darta", label5.Text);
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView1.DataSource = tb;
            dataGridView1.Columns[0].Visible = false;

            sqlcon.con.Close();

        }
        public void vdcedit(string vdc)
        {

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            MySqlCommand commands;
            MySqlDataAdapter adapters = new MySqlDataAdapter();
            DataSet dss = new DataSet();

            string sqls = null;
            //int distcodevalue = Convert.ToInt16(district_combo.SelectedValue);
           // //connetionStrings = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";

            //sqls = "SELECT VdcCode,VdcNepnam FROM  setup_vdc where DistCode='" +district_combo.SelectedValue+ "'";
            sqls = "SELECT VDC_SID,vdcunicodename FROM  setup_vdc where vdcunicodename='" + vdc.ToString() + "'";


           // //connections = new SqlConnection(connetionStrings);
            try
            {
               //My //connections.Open();
                commands = new MySqlCommand(sqls, sqlcon.con);
                adapters.SelectCommand = commands;
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

        private void district_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            vdc_combo.Text = null;
            vdc_combo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            vdc_combo.AutoCompleteSource = AutoCompleteSource.CustomSource;

            AutoCompleteStringCollection combDataa = new AutoCompleteStringCollection();
            FillDropDownListvdc(combDataa);
            vdc_combo.AutoCompleteCustomSource = combDataa;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            button4.Enabled = true;
            int i;
         
            i = dataGridView1.CurrentRow.Index;
            int industryid = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
            label5.Text = industryid.ToString();
     
            //district(dataGridView1.Rows[i].Cells[3].Value.ToString());
          
            //vdcedit(dataGridView1.Rows[i].Cells[4].Value.ToString());
          
            
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
            //button4.Enabled = true;
            button1.Enabled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //base.OnKeyPress(e);
            //// Check if the pressed character was a backspace or numeric.
            //if (e.KeyChar != (char)8 && !char.IsNumber(e.KeyChar))
            //{
            //    e.Handled = true;
            //}

        }

    }
}
