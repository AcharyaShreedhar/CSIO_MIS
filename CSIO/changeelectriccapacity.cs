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
    public partial class changeelectriccapacity : Form
    {
        public changeelectriccapacity()
        {
            InitializeComponent();
        }
        public string transid;
        private void label3_Click(object sender, EventArgs e)
        {

        }
        public void getid(int indusid, string trans,string ddate)
        {
            label5.Text = null;
            label5.Text = indusid.ToString();
            transid= indusid.ToString();
            darta_no.Text = null;
            darta_no.Text = trans.ToString();
            d_date.Text = ddate.ToString();

            //date_txt.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }
        public void DisplayData()
        {
            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }

            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     industryreg.industryid, GetNumberToUnicode(industryreg.industryregno) AS 'दर्ता नं.', industryreg.industrynepname AS 'उद्योगको नाम', industryreg.machine As 'मेशिनरी औजार',GetNumberToUnicode(industryreg.electricpower) As 'विद्युत क्षमता(अ.स./कि.वा.)' From  industryreg where industryreg.industryid=@darta ", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@darta", label5.Text);
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView1.DataSource = tb;
            dataGridView1.Columns[0].Visible = false;
            sqlcon.con.Close();

        }
        private void changeelectriccapacity_Load(object sender, EventArgs e)
        {
           // d_date.Text = global.todaynepslash;
            user_txt.Text = global.username;
            DisplayData();
            groupBox1.Enabled = false;
            button4.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;

            int i;
            i = dataGridView1.CurrentRow.Index;
            textMachinery.Text = (dataGridView1.Rows[i].Cells[3].Value.ToString());
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
                string statvalue = dataGridView1.Rows[i].Cells[4].Value.ToString();


                //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                ///////////////////////////////////////////////////////////

                //SqlCommand qr = new SqlCommand("INSERT INTO update_industry (industryid,industryreg ,dartanum,updattitle,updatnew,oldrec,[updatedate],updatenepdate,updateuser) VALUES (@industryid, @industryregno, @dartanum, @updatetitle, @updatenew, @oldvalue, @updatedate, @updatenepdate, @updateuser)");
                MySqlCommand qr = new MySqlCommand("INSERT INTO update_industry (industryid,industryreg ,dartanum,updattitle,updatnew,oldrec,updatenepdate,updateuser,decisiondate,tax,comment) VALUES (@industryid, GetUnicodeToNumber(@industryregno), @dartanum, @updatetitle, concat(GetNumberToUnicode(@updatenewpower),@updatenew), @oldvalue, GetUnicodeToNumber(@updatenepdate), @updateuser, GetUnicodeToNumber(@ddate), GetUnicodeToNumber(@tax), @comment)");

                qr.CommandType = CommandType.Text;
                qr.Parameters.AddWithValue("@industryid", industryid.ToString());
                qr.Parameters.AddWithValue("@industryregno", regno.ToString());
                qr.Parameters.AddWithValue("@dartanum", darta_no.Text);
                qr.Parameters.AddWithValue("@updatetitle", updateid.Text);
                qr.Parameters.AddWithValue("@tax", textBox2.Text);
                //qr.Parameters.AddWithValue("@updatenew", textBox1.Text);
                //qr.Parameters.AddWithValue("@oldvalue", statvalue.ToString());

                qr.Parameters.AddWithValue("@updatenewpower", textElectricPower.Text );
                qr.Parameters.AddWithValue("@updatenew", " (अ.स./कि.वा.)");
                qr.Parameters.AddWithValue("@oldvalue", statvalue.ToString() + " (अ.स./कि.वा.)");

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
                    MySqlCommand qrs = new MySqlCommand("UPDATE industryreg  SET machine = @txtbxthree,electricpower = GetUnicodeToNumber(@txtbxfour)   WHERE industryid=@indid");


                    qrs.CommandType = CommandType.Text;


                    qrs.Parameters.AddWithValue("@txtbxthree", textMachinery.Text);
                     qrs.Parameters.AddWithValue("@txtbxfour", textElectricPower.Text);

                    //  qrs.Parameters.AddWithValue("@comment", textBox4.Text);
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
                        // cleardata();
                        MessageBox.Show("संशोधन सफल भयो ।", "संशोधन", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        //button9.Enabled = false;
                        //button12.Enabled = false;
                        //panel2.Enabled = false;
                        groupBox1.Enabled = false;
                        button4.Enabled = false;

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            button4.Enabled = true;
        }
        private void buttonMoreIndhanPani_Click(object sender, EventArgs e)
        {
            LoadRegSubForm("fuel_water");
        }


        private void buttonMoreMachinery_Click(object sender, EventArgs e)
        {
            LoadRegSubForm("machinery");
        }

        private void LoadRegSubForm (string disptype)
        {
            industryreg_subforms ins = new industryreg_subforms();
            ins.myIndId = label5.Text; //transid;
            ins.disptype = disptype;
            ins.myparent3 = this;
            ins.myparentName = disptype;
            ins.ShowDialog();
        }

        //raw material

        public void getMachineryFromTable()
        {
            string str;
            //machinery

            str = getAdditionalIndividual("industryreg_machinery", "mach_name");

            if (!string.IsNullOrEmpty(str))
            {
                textMachinery.Text = str;
                textMachinery.ReadOnly = true;
            }
            else
                textMachinery.ReadOnly = false;

            str = global.getSingleDataFromTable("SELECT GetNumberToUnicode(sum(amount)) FROM industryreg_machinery where industryid='" + label5.Text + "'");

            //if (!string.IsNullOrEmpty(str))
            //{
            //    textMachinePrice.Text = str;
            //    textMachinePrice.ReadOnly = true;
            //}
            //else
            //    textMachinePrice.ReadOnly = false;
        }

        public void getElectricFromTable()
        {
            string str;
            str = global.getSingleDataFromTable("SELECT  GetNumberToUnicode(sum(unit_price)*sum(qty)) FROM industryreg_rawmaterial where industryid='" + label5.Text + "'");

            //if (!string.IsNullOrEmpty(str))
            //{
            //    text_raw_price.Text = str;
            //    text_raw_price.ReadOnly = true;
            //}
            //else
            //    text_raw_price.ReadOnly = false;

            //fuel_water

            str = getAdditionalIndividual("industryreg_fuel_water", "concat(type,' (', qty, ' ', unit,')')");

            string elepow = getAdditionalIndividual("industryreg_fuel_water", "sum(qty)", "type='विद्युत शक्ति'");
            textElectricPower.Text = elepow;

            //MessageBox.Show(elepow.ToString());

            if (!string.IsNullOrEmpty(str))
            {
                textFuelWater.Text = str;
                textFuelWater.ReadOnly = true;
            }
            else
                textFuelWater.ReadOnly = false;
        }


private string getAdditionalIndividual(string tbl, string fld, string cond = "")
        {
            string res = "";
            if (cond != "") cond = "AND " + cond;
            string sqq = "SELECT " + fld + " FROM " + tbl + " WHERE industryid='" + label5.Text + "'" + cond;
            string[] vals = global.getSingleFieldArrayFromTable(sqq);
            for (int ii = 0; ii < vals.Length; ii++)
            {
                res += vals[ii].ToString();

                if (ii < vals.Length - 1)
                    res += ", ";
            }
            return res;
        }
    }
}
