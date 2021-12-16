using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace CSIO
{
    public partial class backupdatabase : Form
    {
        public backupdatabase()
        {
            InitializeComponent();
        }
        string file = "D:\\backup.sql";//This is path to save the backup db file..


        string constring = "server='" + Properties.Settings.Default.servername + "';PORT='" + Properties.Settings.Default.port + "';user='" + Properties.Settings.Default.username + "';pwd='" + Properties.Settings.Default.password + "' ;database='" + Properties.Settings.Default.databasename + "'; Convert Zero Datetime=True;";//
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void browse_btn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                BackupTextBox.Text = fbd.SelectedPath;
                backup_btn.Enabled = true;
            }
        }

        private void backup_btn_Click(object sender, EventArgs e)
        {
            if (BackupTextBox.Text.Length > 0)
            {
                string str1 = BackupTextBox.Text.Substring(BackupTextBox.Text.Length - 1);
                if (str1 != "\\")
                {
                    BackupTextBox.Text = BackupTextBox.Text + "\\";
                }
                if (File.Exists(BackupTextBox.Text + filename.Text))
                {
                    MessageBox.Show("The file already exists. Please enter unique filename", "File Name Error");
                    filename.Focus();
                }
                else
                {

                    progressBar1.Visible = true;
                    using (MySqlConnection conn = new MySqlConnection(constring))
                    {
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            using (MySqlBackup mb = new MySqlBackup(cmd))
                            {
                                cmd.Connection = conn;
                                conn.Open();
                                // mb.ExportToFile(file);
                                if (filename.Text.Length > 4)
                                {
                                    string str = filename.Text.Substring(filename.Text.Length - 4);
                                    if (str != ".sql")
                                    {
                                        filename.Text = filename.Text + ".sql";
                                    }
                                    this.timer1.Start();
                                    mb.ExportToFile(BackupTextBox.Text + filename.Text);//This line will export the file to given path.

                                }
                                else if (filename.Text == "" || filename.Text.Length == 0)
                                {
                                    MessageBox.Show("Check File Name, File Name cannot be blank.", "File Name Error");
                                    filename.Focus();
                                }
                                else
                                {
                                    filename.Text = filename.Text + ".sql";
                                    this.timer1.Start();
                                    mb.ExportToFile(BackupTextBox.Text + filename.Text);//This line will export the file to given path.

                                }

                                conn.Close();
                                MessageBox.Show("Backup Completed...!!!");
                            }
                        }
                    }
                    //mysqlcon.conn.Open();
                    //String database = mysqlcon.conn.Database.ToString();
                    //try
                    //{
                    //    if (BackupTextBox.Text == string.Empty)
                    //    {
                    //        //  s.Speak("please enter the valid backup file location");
                    //        MessageBox.Show("please enter the backup file location");
                    //    }
                    //    else
                    //    {

                    //        string q = "BACKUP DATABASE [" + database + "] TO DISK='" + BackupTextBox.Text + "\\" + "Database" + "-" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + ".bak'";

                    //        MySqlCommand scmd = new MySqlCommand(q, mysqlcon.conn);
                    //        scmd.ExecuteNonQuery();
                    //        // s.Speak("Backup taken successfully");
                    //        MessageBox.Show("Backup taken successfully", "Backup successs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        backup_btn.Enabled = false;

                    //    }



                    //{

                    //    using (MySqlCommand cmd = new MySqlCommand())
                    //    {
                    //        using (MySqlBackup mb = new MySqlBackup(cmd))
                    //        {
                    //            if (mysqlcon.conn.State == ConnectionState.Closed)
                    //            {
                    //                mysqlcon.conn.Open();
                    //            }
                    //            mb.ExportToFile(file);//This line will export the file to given path.
                    //            mysqlcon.conn.Close();
                    //            MessageBox.Show("Backup Completed...!!!");
                    //        }
                    //    }
                    //}
                }
            }
            else
            {
                MessageBox.Show("Check File Location", "Location Error");
                BackupTextBox.Focus();
            }
        }

        private void backupdatabase_Load(object sender, EventArgs e)
        {

        }
    }
}
