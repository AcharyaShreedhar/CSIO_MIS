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
    public partial class capitalupdateindustry : Form
    {
        public capitalupdateindustry()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            //try
            //{
            //    double a = 0;
            //    double b = 0;
            //    if (textBox1.Text != "")
            //    {
            //        a = Convert.ToDouble(textBox1.Text);
            //    }
            //    if (textBox2.Text != "")
            //    {
            //        b = Convert.ToDouble(textBox2.Text);
            //    }
            //    textBox3.Text = (a + b).ToString();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}

        

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
        public void DisplayData()
        {
            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }

            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     industryreg.industryid, GetNumberToUnicode(industryreg.industryregno) AS 'दर्ता नं.', GetNumberToUnicode(GetNumberFormat(industryreg.statcapital)) AS 'स्थिर पुँजी', GetNumberToUnicode(GetNumberFormat(industryreg.varcapital)) AS 'चालु पुँजी',      GetNumberToUnicode(GetNumberFormat(industryreg.statcapital + industryreg.varcapital)) AS 'कुल पुँजी',GetNumberToUnicode(industryreg.yearlyturnover) As 'बार्षिक उत्पादन क्षमता(रु)'  From  industryreg where industryreg.industryid=@darta ", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@darta", label5.Text);
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView1.DataSource = tb;
            dataGridView1.Columns[0].Visible = false;
            sqlcon.con.Close();

        }
        private void capitaldetailbanijya_Load(object sender, EventArgs e)
        {
            //d_date.Text = global.todaynepslash;
           user_txt.Text = global.username;
           DisplayData();
           groupBox1.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chalu.Text = null;
            sthir.Text = null;
            comment.Text = null;
            kul.Text = null;
            tax.Text = null;
        }
        private void textBox1_TabIndexChanged(object sender, EventArgs e)
        {
            try
            {
                double a = Convert.ToDouble(chalu.Text);
                double b = Convert.ToDouble(sthir.Text);
                kul.Text = (a + b).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBox2_TabIndexChanged(object sender, EventArgs e)
        {
            try{
            double a = Convert.ToDouble(sthir.Text);
            double b = Convert.ToDouble(chalu.Text);
            kul.Text = (a + b).ToString();
              }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        

        //private void textBox2_TextChanged(object sender, EventArgs e)
        //{
        //    double a = 0;
        //    double b = 0;
        //    if (textBox1.Text != "")
        //    {
        //        a = Convert.ToDouble(textBox1.Text);
        //    }
        //    if (textBox2.Text != "")
        //    {
        //        b = Convert.ToDouble(textBox2.Text);
        //    }
        //    textBox3.Text = (a + b).ToString();
        //}
       
      

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int i;
                i = dataGridView1.CurrentRow.Index;
                int industryid = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                string regno = dataGridView1.Rows[i].Cells[1].Value.ToString();
                string statvalue = dataGridView1.Rows[i].Cells[2].Value.ToString();
                string varvalue = dataGridView1.Rows[i].Cells[3].Value.ToString();
                string totalvalue = dataGridView1.Rows[i].Cells[4].Value.ToString();
                string yearly = dataGridView1.Rows[i].Cells[5].Value.ToString();

                //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }

                string curdate = DateTime.Today.ToString();

                //SqlCommand qr = new SqlCommand("INSERT INTO update_industry (industryid,industryreg ,dartanum,updattitle,updatnew,oldrec,[updatedate],updatenepdate,updateuser) VALUES (@industryid, @industryregno, @dartanum, @updatetitle, @updatenew, @oldvalue, @updatedate, @updatenepdate, @updateuser)");
                MySqlCommand qr = new MySqlCommand("INSERT INTO update_industry (industryid,industryreg ,dartanum,updattitle,updatnew,oldrec,updatedate,updatenepdate,updateuser,decisiondate,tax,comment) VALUES (@industryid, @industryregno, @dartanum, @updatetitle,concat(@updatenewchalupujilabel,GetNumberToUnicode(@updatenewchalupuji),@updatenewsthirlabel,GetNumberToUnicode(@updatenewsthirpuji),@updatenewkullabel,GetNumberToUnicode(@updatenewkulpuji)), @oldvalue, @updatedate, @updatenepdate, @updateuser, @ddate, @tax, @comment)");

                qr.CommandType = CommandType.Text;
                qr.Parameters.AddWithValue("@industryid", industryid.ToString());
                qr.Parameters.AddWithValue("@industryregno", regno.ToString());
                qr.Parameters.AddWithValue("@dartanum", darta_no.Text);
                qr.Parameters.AddWithValue("@updatetitle", updateid.Text);
               
                qr.Parameters.AddWithValue("@updatenewchalupujilabel", chalupuji.Text);
                qr.Parameters.AddWithValue("@updatenewchalupuji",chalu.Text+' ');
                qr.Parameters.AddWithValue("@updatenewsthirlabel",  sthirpuji.Text); 
                qr.Parameters.AddWithValue("@updatenewsthirpuji", sthir.Text+' ');
                qr.Parameters.AddWithValue("@updatenewkullabel", kulpuji.Text);
                qr.Parameters.AddWithValue("@updatenewkulpuji",kul.Text+' ');
                //qr.Parameters.AddWithValue("@updatenew", (chalupuji.Text + chalu.Text + ", " + sthirpuji.Text + sthir.Text + ", " + kulpuji.Text + kul.Text));
                



                qr.Parameters.AddWithValue("@oldvalue", (chalupuji.Text + varvalue.ToString() + " " + sthirpuji.Text + statvalue.ToString() + " " + kulpuji.Text + totalvalue.ToString()));

                //qr.Parameters.AddWithValue("@transid", updateid.Text);
                qr.Parameters.AddWithValue("@updatedate", curdate.ToString());
                qr.Parameters.AddWithValue("@updatenepdate", global.nepalidate);
                qr.Parameters.AddWithValue("@updateuser", user_txt.Text);
                qr.Parameters.AddWithValue("@ddate", d_date.Text);
                qr.Parameters.AddWithValue("@tax", tax.Text);
                qr.Parameters.AddWithValue("@comment", comment.Text);


                qr.Connection = sqlcon.con;


                int k = qr.ExecuteNonQuery();

                if (k > 0)
                {
                    MySqlCommand qrt = new MySqlCommand("INSERT INTO update_industry (industryid,industryreg ,dartanum,updattitle,updatnew,oldrec,updatedate,updatenepdate,updateuser,decisiondate,comment) VALUES (@industryid, @industryregno, @dartanum, @updatetitle, GetNumberToUnicode(@updatenew), @oldvalue, @updatedate, @updatenepdate, @updateuser, @ddate, @comment)");

                    qrt.CommandType = CommandType.Text;
                    qrt.Parameters.AddWithValue("@industryid", industryid.ToString());
                    qrt.Parameters.AddWithValue("@industryregno", regno.ToString());
                    qrt.Parameters.AddWithValue("@dartanum", darta_no.Text);
                    qrt.Parameters.AddWithValue("@updatetitle", turnover.Text);

                    qrt.Parameters.AddWithValue("@updatenew", (turnover_txt.Text));
                    qrt.Parameters.AddWithValue("@oldvalue", (yearly.ToString()));

                    //qr.Parameters.AddWithValue("@transid", updateid.Text);
                    qrt.Parameters.AddWithValue("@updatedate", curdate.ToString());
                    qrt.Parameters.AddWithValue("@updatenepdate", global.nepalidate);
                    qrt.Parameters.AddWithValue("@updateuser", user_txt.Text);
                    qrt.Parameters.AddWithValue("@ddate", d_date.Text);
                   
                    qrt.Parameters.AddWithValue("@comment", comment.Text);


                    qrt.Connection = sqlcon.con;


                    int u = qrt.ExecuteNonQuery();

                    //sqlcon.con.Close();
                    // dataGridView1.DataSource = null;
                    // DisplayData();
                    // cleardata();
                    MySqlCommand qrs = new MySqlCommand("UPDATE industryreg  SET statcapital = GetUnicodeToNumber(@txtbxthree) ,varcapital = GetUnicodeToNumber(@txtbxfour),yearlyturnover=GetUnicodeToNumber(@turnover)   WHERE industryid=@indid");


                    qrs.CommandType = CommandType.Text;


                    qrs.Parameters.AddWithValue("@txtbxthree", sthir.Text);
                    qrs.Parameters.AddWithValue("@txtbxfour", chalu.Text);
                    qrs.Parameters.AddWithValue("@turnover", turnover_txt.Text);
                    ////qrs.Parameters.AddWithValue("@comment", textBox4.Text);
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


                    }

                }

                // MessageBox.Show("रेकर्ड इन्टी सफल भयो ।", " ", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            }
    
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public void GetSum()
        {
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }

                MySqlCommand cmd = new MySqlCommand("select GetNumberToUnicode(GetNumberFormat(GetUnicodeToNumber(@sthir))) As 'Stat',GetNumberToUnicode(GetNumberFormat(GetUnicodeToNumber(@chalu))) As 'Var', GetNumberToUnicode(GetNumberFormat(GetUnicodeToNumber(@sthir)+GetUnicodeToNumber(@chalu))) As 'Total'", sqlcon.con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@sthir", sthir.Text);
                cmd.Parameters.AddWithValue("@chalu", chalu.Text);
                // cmd.Connection = con;
                MySqlDataReader sdr = cmd.ExecuteReader();

                sdr.Read();
                kul.Text = sdr["Total"].ToString().Trim();
                chalu.Text = sdr["Var"].ToString().Trim();
                sthir.Text = sdr["Stat"].ToString().Trim();


                sqlcon.con.Close();

                //IF AVAILABLE -- GET IT FROM DATABASE
                getInvestmentFromTable();
               
                // ind_type.Text = tid.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            //GetSum();
           //try{
           // double a = 0;
           // double b = 0;
           // if (textBox1.Text != "")
           // {
           //     a = Convert.ToDouble(textBox1.Text);
           // }
           // if (textBox2.Text != "")
           // {
           //     b = Convert.ToDouble(textBox2.Text);
           // }
           // textBox3.Text = (a + b).ToString();
           //   }
           // catch (Exception ex)
           // {
           //     MessageBox.Show(ex.ToString());
           // }
        
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            //base.OnKeyPress(e);
            //// Check if the pressed character was a backspace or numeric.
            //if (e.KeyChar != (char)8 && !char.IsNumber(e.KeyChar))
            //{
            //    e.Handled = true;
            //}

        

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
//GetSum();
            //base.OnKeyPress(e);
            //// Check if the pressed character was a backspace or numeric.
            //if (e.KeyChar != (char)8 && !char.IsNumber(e.KeyChar))
            //{
            //    e.Handled = true;
            //}

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            // Check if the pressed character was a backspace or numeric.
            if (e.KeyChar != (char)8 && !char.IsNumber(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void sthir_Leave(object sender, EventArgs e)
        {
            GetSum();
        }

        private void sthir_TabIndexChanged(object sender, EventArgs e)
        {
          //  GetSum();
        }

        private void chalu_TabIndexChanged(object sender, EventArgs e)
        {
           // GetSum();
        }

        private void chalu_Leave(object sender, EventArgs e)
        {
            GetSum();
        }

        private void buttonMoreInvestment_Click(object sender, EventArgs e)
        {
            LoadRegSubForm("investment");
        }

        private void LoadRegSubForm(string disptype)
        {
            industryreg_subforms ins = new industryreg_subforms();
            ins.myIndId = label5.Text; //transid;
            ins.disptype = disptype;
            ins.myparent2 = this;
            ins.myparentName = "capital_update";
            ins.ShowDialog();
        }

        public void getInvestmentFromTable()
        {
            string transid = label5.Text.ToString();
            string str;
            //investment - STHIR
            str = global.getSingleDataFromTable("SELECT  GetNumberToUnicode(sum(amount)) FROM industryreg_investment_sthir where industryid='" + transid + "'");

            if (!string.IsNullOrEmpty(str))
            {
                sthir.Text = str;
                sthir.ReadOnly = true;
            }
            else
                sthir.ReadOnly = false;

            //investment - CHALU
            str = global.getSingleDataFromTable("SELECT  GetNumberToUnicode(sum(amount)) FROM industryreg_investment_chalu where industryid='" + transid + "'");

            if (!string.IsNullOrEmpty(str))
            {
                chalu.Text = str;
                chalu.ReadOnly = true;
            }
            else
                chalu.ReadOnly = true;
        }
    }
}
