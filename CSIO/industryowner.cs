using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CSIO
{
    public partial class industryowner : Form
    {
        public industryowner()
        {
            InitializeComponent();
        }
        public void getdataa(string darta, string dist, string vdc, string vdcname)
        {
            darta_txt.Text = null;
            darta_txt.Text = darta.ToString();
            district_txt.Text = null;
            district_txt.Text = dist.ToString();
            vdc_txt.Text = null;
            vdc_txt.Text = vdc.ToString();
            ward_txt.Text = vdcname.ToString();


            //date_txt.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }
        private void industryowner_Load(object sender, EventArgs e)
        {

        }
    }
}
