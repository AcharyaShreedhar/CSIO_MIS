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
    public partial class updatebanijyarecord : Form
    {
        public updatebanijyarecord()
        {
            InitializeComponent();
        }
        public void DisplayData()
        {
            try
            {
                //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }

                MySqlDataAdapter qry = new MySqlDataAdapter("SELECT   update_banijya.firmid, GetNumberToUnicode(update_banijya.firmreg) AS 'र.नं.',setup_subcategory.subcategory_unicodename AS 'सम्शोधन शिर्षक',update_banijya.oldrec AS 'साविक विवरण', update_banijya.updatnew AS 'सम्शोधन पछि',                        update_banijya.updatenepdate AS 'निर्णय मिति'FROM         update_banijya INNER JOIN                      setup_subcategory ON update_banijya.updattitle = setup_subcategory.subcategory_id where update_banijya.updatid=@darta  ", sqlcon.con);
                qry.SelectCommand.Parameters.AddWithValue("@darta", updateid.Text);

                DataTable tb = new DataTable();
                qry.Fill(tb);
                foreach (DataRow row in tb.Rows)
                {
                    darta_txt.Text = (row[1].ToString());
                    sabik.Text = (row[3].ToString());
                    updated.Text = (row[4].ToString());
                    topic.Text = (row[2].ToString());

                    //citizen_off.Items.Add(row[0].ToString()+" Y "+row[1].ToString());
                }
                //dataGridView1.DataSource = tb;
                //dataGridView1.Columns[0].Visible = false;
                //dataGridView1.Columns[3].Visible = false;
                //dataGridView1.Columns[7].Visible = false;
                sqlcon.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void getdataa(string updateids, string darta)
        {
            try
            {

                updateid.Text = null;
                updateid.Text = updateids.ToString();
                // darta_txt.Text = null;
                // darta_txt.Text = darta.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


            //date_txt.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }
         public void getdata(string updateids, string darta)
        {
            try
            {

                updateid.Text = null;
                updateid.Text = updateids.ToString();
                // darta_txt.Text = null;
                // darta_txt.Text = darta.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


            //date_txt.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }
        private void updatebanijyarecord_Load(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
             if (reason.Text == null || reason.Text == "")
            {
                MessageBox.Show("संशोधन कारण खुलाउनुहोस ।", "संशोधन कारण", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                reason.Focus();
            }
            else{
            DialogResult result1 = MessageBox.Show("विवरण संशोधन गर्ने हो?",
                 "विवरण संशोधन",
                 MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result1 == DialogResult.Yes)
            {
                // string curdate = DateTime.Today.ToString();
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }

                MySqlCommand cmds = new MySqlCommand("INSERT INTO update_banijya_hist(updatid, firmid, firmreg, decisiondate, dartanum, updattitle, updatnew, oldrec, transid, updatedate, updatenepdate, updateuser, tax, comment, updatedreason, updateduser) SELECT updatid, firmid, firmreg, decisiondate, dartanum, updattitle, updatnew, oldrec, transid, updatedate, updatenepdate, updateuser, tax, comment,@updatereason,@updateuser FROM update_banijya WHERE updatid= @uid", sqlcon.con);
                cmds.CommandType = CommandType.Text;

                cmds.Parameters.AddWithValue("@uid", updateid.Text);
                cmds.Parameters.AddWithValue("@updateuser", global.username);
                cmds.Parameters.AddWithValue("@updatereason", reason.Text);

                //cmd.Parameters.AddWithValue("@usertype", usertype.SelectedItem);

                // cmd.Parameters.AddWithValue("@curdate", curdate.ToString());
                cmds.Connection = sqlcon.con;


                //int n = qr.ExecuteNonQuery();
                int p = 0;
                p = cmds.ExecuteNonQuery();

                if (p > 0)
                {
                    //MySqlCommand cmd = new MySqlCommand("UPDATE login SET username= @username,password= @password, full_name= @fullname, address= @address, contactno= @phone,status= @status,usertype= @usertype WHERE userid= '" + userid.Text + "'", mysqlcon.conn);
                    MySqlCommand cmd = new MySqlCommand("UPDATE  update_banijya SET oldrec= @oldrec,updatnew= @updatrec WHERE updatid= @uid", sqlcon.con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@uid", updateid.Text);
                    cmd.Parameters.AddWithValue("@oldrec", sabik.Text);
                    cmd.Parameters.AddWithValue("@updatrec", updated.Text);

                    //cmd.Parameters.AddWithValue("@usertype", usertype.SelectedItem);

                    // cmd.Parameters.AddWithValue("@curdate", curdate.ToString());
                    cmd.Connection = sqlcon.con;


                    //int n = qr.ExecuteNonQuery();
                    int n = 0;
                    n = cmd.ExecuteNonQuery();

                    if (n > 0)
                    {
                        MessageBox.Show("संशोधन सफलतापुर्वक सम्पन्न भयो ।", "संशोधन", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        sqlcon.con.Close();
                        // dataGridView1.DataSource = null;
                        banijyasomsodhansearch existing = Application.OpenForms.OfType<banijyasomsodhansearch>().FirstOrDefault();
                        if (existing != null)
                        {
                            existing.Show();
                            //  shareplan ciname = new shareplan();
                            existing.DisplayData();
                        }


                        this.Close();

                        // cleardata();

                    }
                }
            }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
