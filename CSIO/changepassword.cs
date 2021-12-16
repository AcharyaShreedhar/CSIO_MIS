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
    public partial class changepassword : Form
    {
        public changepassword()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }

            if (new_password.Text != re_password.Text)
            {
                MessageBox.Show("Password Mismatch, Try Again", "Warning");
                new_password.Focus();
            }
            else
            {
                string oldpass = globalfunction.MD5Hash(old_password.Text);
                string newpass = globalfunction.MD5Hash(new_password.Text);
                string username = global.username;

                MySqlCommand cmd = new MySqlCommand("UPDATE login SET password= @password WHERE username= @username AND password=@oldpassword", sqlcon.con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@oldpassword", oldpass.ToString());
                cmd.Parameters.AddWithValue("@username", username.ToString());
                cmd.Parameters.AddWithValue("@password", newpass.ToString());


                //int n = qr.ExecuteNonQuery();
                int n = 0;
                n = cmd.ExecuteNonQuery();

                if (n > 0)
                {

                    sqlcon.con.Close();
                    old_password.Text = "";
                    new_password.Text = "";
                    re_password.Text = "";
                    MessageBox.Show("Password Successfully Changed", "Password Changed", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);


                }
                else
                {
                    MessageBox.Show("Unable to Change Password, Wrong Password", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void changepassword_Load(object sender, EventArgs e)
        {
            lbl_userid.Text = global.username;
        }
    }
}
