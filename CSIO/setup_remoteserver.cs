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
    public partial class setup_remoteserver : Form
    {
        public setup_remoteserver()
        {
            InitializeComponent();
        }

        private void save_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show("Do you want to update?",
         "Update System Configuration ",
         MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result1 == DialogResult.Yes)
            {
                Properties.Settings.Default.onlineservername = servertxt.Text;
                Properties.Settings.Default.onlinedatabasename = databasetxt.Text;
                Properties.Settings.Default.onlineusername = dbusertxt.Text;
                Properties.Settings.Default.onlinepassword = dbpasswordtxt.Text;
                             
                Properties.Settings.Default.Save();
            }
        }

        private void setup_remoteserver_Load(object sender, EventArgs e)
        {
            servertxt.Text = Properties.Settings.Default.onlineservername;
            databasetxt.Text = Properties.Settings.Default.onlinedatabasename;
            dbusertxt.Text = Properties.Settings.Default.onlineusername;
            dbpasswordtxt.Text = Properties.Settings.Default.onlinepassword;
            
        }
    }
}
