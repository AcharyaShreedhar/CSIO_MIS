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
    public partial class reportdisplay : Form
    {
        public reportdisplay()
        {
            InitializeComponent();
        }
        //Display Data in DataGridView  
        public void DisplayData()
        {
            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }

            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT firmregno AS 'र.नं'',firmnepname As 'फर्मको नाम',firmscope As 'उद्देश्य',karobar AS 'कारोवार',renewdate As 'नविकरण मिति', setup_district.distnepname As 'जिल्ला', setup_vdc.VdcNepnam As 'गाउपालिका।न.पा' FROM         setup_vdc INNER JOIN     banijyaform ON setup_vdc.VDC_SID = banijyaform.firmvdc INNER JOIN    setup_district ON banijyaform.firmdist = setup_district.distcode where firmregno='" + darta_txt.Text + "' ", sqlcon.con);
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView1.DataSource = tb;
            sqlcon.con.Close();

        }  

        private void reportdisplay_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DisplayData();

        }

      
    }
}
