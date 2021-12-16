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
    public partial class expiry : Form
    {
        public expiry()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void expiry_Load(object sender, EventArgs e)
        {
            nodays.Text = Properties.Settings.Default.noofdays.ToString();
        }
    }
}
