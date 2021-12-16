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
    public partial class setup_calendar : Form
    {
        public setup_calendar()
        {
            InitializeComponent();
        }

        private void new_btn_Click(object sender, EventArgs e)
        {
            // new_btn.Enabled = true;
            save_btn.Enabled = true;
            button4.Enabled = false;
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
               sqlcon.con.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("INSERT INTO `setup_nepcalender`(`Year`, `StDtEng`, `EndDTEng`, `NDNepM01`, `NDNepM02`, `NDNepM03`, `NDNepM04`, `NDNepM05`, `NDNepM06`, `NDNepM07`, `NDNepM08`, `NDNepM09`, `NDNepM10`, `NDNepM11`, `NDNepM12`) VALUES (@year,@start,@end,@month1,@month2,@month3,@month4,@month5,@month6,@month7,@month8,@month9,@month10,@month11,@month12)");
                DateTime now = DateTime.Now;

                cmd.CommandType = CommandType.Text;
                //  ,,,,,,,,,,,, )");
                cmd.Parameters.AddWithValue("@year", textBox13.Text);
                cmd.Parameters.AddWithValue("@start", startdate.Text);
                cmd.Parameters.AddWithValue("@end", enddate.Text);
                cmd.Parameters.AddWithValue("@month1", textBox1.Text);
                cmd.Parameters.AddWithValue("@month2", textBox2.Text);
                cmd.Parameters.AddWithValue("@month3", textBox3.Text);
                cmd.Parameters.AddWithValue("@month4", textBox4.Text);
                cmd.Parameters.AddWithValue("@month5", textBox5.Text);
                cmd.Parameters.AddWithValue("@month6", textBox6.Text);
                cmd.Parameters.AddWithValue("@month7", textBox7.Text);
                cmd.Parameters.AddWithValue("@month8", textBox8.Text);
                cmd.Parameters.AddWithValue("@month9", textBox9.Text);
                cmd.Parameters.AddWithValue("@month10", textBox10.Text);
                cmd.Parameters.AddWithValue("@month11", textBox11.Text);
                cmd.Parameters.AddWithValue("@month12", textBox12.Text);

                cmd.Connection = sqlcon.con;
                int n = cmd.ExecuteNonQuery();

                if (n > 0)
                {
                    global.sync_dblog("setup_nepcalender", textBox13.Text, "INSERT", textBox13.Text, "Year");
                    MessageBox.Show("New Record Inserted Successfully.");
                    sqlcon.con.Close();
                    //dataGridView1.DataSource = null;
                    DisplayData();
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
        public void DisplayData()
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            // MySqlDataAdapter qrey = new MySqlDataAdapter("SELECT customerdetail.customerid, customerdetail.cfname AS 'First Name', customerdetail.clname AS 'Last Name', customerdetail.address AS 'Address', customerdetail.city AS 'City',customerdetail.state AS 'State', customerdetail.zipcode As 'Zip Code', customerdetail.contact As 'Contact', customerdetail.year As 'Year', setup_make.makename AS 'Make', model.model_name AS 'Model', customerdetail.vin AS 'VIN', setup_group.groupname AS 'Group', customerdetail.entrydate AS 'Entry Date', customerdetail.updtdate AS 'Update Date' FROM `customerdetail` INNER JOIN `model` ON model.model_id=customerdetail.model INNER JOIN setup_group ON setup_group.group_id=customerdetail.group INNER JOIN setup_make ON setup_make.make_id=customerdetail.make ", mysqlcon.conn);
            MySqlDataAdapter qrey = new MySqlDataAdapter("SELECT `Year` AS 'Year(B.S.)', DATE_FORMAT(`StDtEng`, '%Y/%m/%d %H:%i') As 'Year Starts From', DATE_FORMAT(`EndDTEng`,'%Y/%m/%d %H:%i') As 'Year End At', `NDNepM01` As 'Baishak', `NDNepM02` As 'Jestha', `NDNepM03` As 'Ashad', `NDNepM04` As 'Shrawan', `NDNepM05` As 'Bhadra', `NDNepM06` As 'Ashwin', `NDNepM07` As 'Kartik', `NDNepM08` As 'Mangsir', `NDNepM09` As 'Poush', `NDNepM10` As 'Magh', `NDNepM11` As 'Falgun', `NDNepM12` As 'Chaitra' FROM `setup_nepcalender` ORDER BY Year DESC ", sqlcon.con);
            // qrey.SelectCommand.Parameters.AddWithValue("@cname", "%" + customername.Text + "%");
            // qrey.SelectCommand.Parameters.AddWithValue("@textnum", customername.Text);
            DataTable tb = new DataTable();
            qrey.Fill(tb);
            dataGridView1.DataSource = tb;
            // dataGridView1.Columns[0].Visible = false;
            //  dataGridView1.Columns[1].Visible = false;

            sqlcon.con.Close();

        }
        private void setup_calendar_Load(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            button4.Enabled = true;
            save_btn.Enabled = false;

            int i;
            // DateTime strt;

            i = dataGridView1.CurrentRow.Index;
            textBox13.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            yearvalue.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            // cid.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            // DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            startdate.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            //  strt = dataGridView1.Rows[i].Cells[1].Value.ToString();
            enddate.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            textBox1.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            textBox2.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            textBox3.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
            textBox4.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();
            textBox5.Text = dataGridView1.Rows[i].Cells[7].Value.ToString();
            textBox6.Text = dataGridView1.Rows[i].Cells[8].Value.ToString();
            textBox7.Text = dataGridView1.Rows[i].Cells[9].Value.ToString();
            textBox8.Text = dataGridView1.Rows[i].Cells[10].Value.ToString();
            textBox9.Text = dataGridView1.Rows[i].Cells[11].Value.ToString();
            textBox10.Text = dataGridView1.Rows[i].Cells[12].Value.ToString();
            textBox11.Text = dataGridView1.Rows[i].Cells[13].Value.ToString();
            textBox12.Text = dataGridView1.Rows[i].Cells[14].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("Update `setup_nepcalender` SET `Year`=@year, `StDtEng`=@start, `EndDTEng`=@end, `NDNepM01`=@month1, `NDNepM02`=@month2, `NDNepM03`=@month3, `NDNepM04`=@month4, `NDNepM05`=@month5, `NDNepM06`=@month6, `NDNepM07`=@month7, `NDNepM08`=@month8, `NDNepM09`=@month9, `NDNepM10`=@month10, `NDNepM11`=@month11, `NDNepM12`=@month12 ");
                DateTime now = DateTime.Now;

                cmd.CommandType = CommandType.Text;
                //  ,,,,,,,,,,,, )");
                cmd.Parameters.AddWithValue("@year", textBox13.Text);
                cmd.Parameters.AddWithValue("@start", startdate.Text);
                cmd.Parameters.AddWithValue("@end", enddate.Text);
                cmd.Parameters.AddWithValue("@month1", textBox1.Text);
                cmd.Parameters.AddWithValue("@month2", textBox2.Text);
                cmd.Parameters.AddWithValue("@month3", textBox3.Text);
                cmd.Parameters.AddWithValue("@month4", textBox4.Text);
                cmd.Parameters.AddWithValue("@month5", textBox5.Text);
                cmd.Parameters.AddWithValue("@month6", textBox6.Text);
                cmd.Parameters.AddWithValue("@month7", textBox7.Text);
                cmd.Parameters.AddWithValue("@month8", textBox8.Text);
                cmd.Parameters.AddWithValue("@month9", textBox9.Text);
                cmd.Parameters.AddWithValue("@month10", textBox10.Text);
                cmd.Parameters.AddWithValue("@month11", textBox11.Text);
                cmd.Parameters.AddWithValue("@month12", textBox12.Text);

                cmd.Connection = sqlcon.con;
                int n = cmd.ExecuteNonQuery();

                if (n > 0)
                {
                    global.sync_dblog("setup_nepcalender", textBox13.Text, "UPDATE", textBox13.Text, "Year");
                    MessageBox.Show("बिवरण संशोधन भयो ।");
                    sqlcon.con.Close();
                    //dataGridView1.DataSource = null;
                    DisplayData();
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
    }
}
