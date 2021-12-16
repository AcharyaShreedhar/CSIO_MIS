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
using System.Diagnostics;
using System.Drawing.Printing;
using System.Net.Sockets;
using System.Net;

namespace CSIO
{
    public partial class industryfileupload : Form
    {
        public industryfileupload()
        {
            InitializeComponent();
        }
        
        //string rd;
        //byte[] b1;
        //string v;
        //int m;
        //TcpListener list;

        //Int32 port = 5050;
        //Int32 port1 = 5055;
        struct ftpSetting
        {
            public string Server { get; set; }
            public string Username { get; set; }
            public String Password { get; set; }
            public string Filename { get; set; }
           // https://www.youtube.com/watch?v=5d-AE21Zjog
        }
       
        public static string firmi;
        public static string transid;
        public static string servername = Properties.Settings.Default.servername;
        public static string filelocation = Properties.Settings.Default.reportaddress;
        public static string newfilename;
       public static byte[] bt;
       public void sendtoserver()
       {
           if (sqlcon.con.State == ConnectionState.Closed)
           {
               sqlcon.con.Open();
           }
           //string connetionString = null;
           //SqlConnection connection;
//           AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);

//WindowsIdentity identity = new WindowsIdentity(username, password);

//WindowsImpersonationContext context = identity.Impersonate();
           string sourcePath = file_name.Text;
try
{
    var fi = new FileInfo(file_name.Text);
   
    string extn = fi.Extension;
   // string filename = fi.Name;

    //string filename = global.csiodist + "_" + global.csioid + "_" + DateTime.Now.ToString("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss.fffffffK") + "_"+ fi.Name;
    string filename = firmi.ToString() + "_" + global.csiodist + "_" + global.csioid + "_" + DateTime.Now.ToString("yyyyMMddHHmmss")+ "_" + fi.Name;
    newfilename = filename;
    //string newfile = @"\\" + servername +filelocation+ filename;
    string newfile =  @"\\"+ servername + filelocation + filename;
    MessageBox.Show(newfile.ToString());
   // File.Copy(@"c:\temp\File.txt", @"\\server Name or IP\folder\File.txt", true);
  //  AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
  // WindowsIdentity idnt = new WindowsIdentity(username, negotiation_type);
// WindowsImpersonationContext context = idnt.Impersonate();
    File.Copy(@sourcePath.ToString(),@newfile.ToString(),  true);
   
   // MessageBox.Show("Copy Done");
   // context.Undo();
}

           catch (Exception ex)
           {
               MessageBox.Show("Can not open connection ! " + ex);
           }

       }
       public void getdataa(string darta, string id)
       {
           darta_txt.Text = null;
           darta_txt.Text = darta.ToString();
           //district_txt.Text = null;
           //district_txt.Text = dist.ToString();
           //vdc_txt.Text = null;
           //vdc_txt.Text = vdc.ToString();
           //ward_txt.Text = vdcname.ToString();
           firmi = id.ToString();
          


           //date_txt.Text = DateTime.Today.ToString("dd-MM-yyyy");
       }

        public void citizenofficedetail(AutoCompleteStringCollection dataCollection)
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            //string connetionString = null;
            //SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            //connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "'";g = null;
            sql = "SELECT    filetypeid, filetypename FROM setup_filetype";
            //connection = new SqlConnection(connetionString);
            try
            {
                //connection.Open();
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                sqlcon.con.Close();

                citizen_off.DataSource = ds.Tables[0];
                citizen_off.ValueMember = "filetypeid";
                citizen_off.DisplayMember = "filetypename";
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    dataCollection.Add(row[1].ToString());
                    //citizen_off.Items.Add(row[0].ToString()+" Y "+row[1].ToString());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }
        private void industryfileupload_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(DateTime.Now.ToString("yyyyMMddHHmmss"));
            panel_pdf.Visible = false;
            panel_img.Visible = true;
            // axAcroPDF1.LoadFile("C:\\Users\\ANIL\\Downloads\\Social_Model_question.pdf");
            citizen_off.AutoCompleteMode = AutoCompleteMode.Append;
            citizen_off.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection ctzenoff = new AutoCompleteStringCollection();
            citizenofficedetail(ctzenoff);
            citizen_off.AutoCompleteCustomSource = ctzenoff;
            citizen_off.SelectedIndex = 0;
            DisplayData();
            button3.Enabled = false;
            button2.Enabled = false;

            labelIndName.Visible = false;
            labelIndAddress.Visible = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
           try{
               button3.Enabled = true;
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Choose File(*.jpg; *.png; *.gif; *.pdf)|*.jpg; *.png; *.gif;*.pdf";
            if (opf.ShowDialog() == DialogResult.OK)
            {
                
                file_name.Text = opf.FileName;
                var fi = new FileInfo(file_name.Text);
                string extn = fi.Extension;
                if (extn.ToString() == ".pdf")
                {
                    panel_pdf.Visible = true;
                    panel_img.Visible = false;
                   // MessageBox.Show("Extention Inside Pdf="+extn.ToString());
                   axAcroPDF2.src=(opf.FileName);
                    //ProcessStartInfo startInfo = new ProcessStartInfo(opf.FileName);
                    //Process.Start(startInfo);
                    //AddedwebBrowserControl.Navigate(@"c:\IntroductiontoSqlServer.pdf");
                }
                else
                {
                  //  MessageBox.Show("Extention Inside Image=" + extn.ToString());
                    panel_pdf.Visible = false;
                    panel_img.Visible = true;
                    pictureBox1.Image = Image.FromFile(opf.FileName);

                }
            }
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.ToString());
           }
            
            //using (OpenFileDialog ofd = new OpenFileDialog() { ValidateNames = true, Multiselect = false, Filter = "PDF|*.pdf" })
            //{
            //    if (ofd.ShowDialog() == DialogResult.OK)
            //    {
            //        bt = null;
            //        FileStream fs = File.OpenRead(ofd.FileName);
            //        bt = new byte[fs.Length];
            //        fs.Read(bt, 0, (int)fs.Length);
            //     //   axAcroPDF1.src = ofd.FileName;
            //        file_name.Text = ofd.FileName;
            //        fs.Close();

            //    }
            //}


            //OpenFileDialog openFileDialog1 = new OpenFileDialog
            //{
            //    InitialDirectory = @"D:\",
            //    Title = "फाईल खोज्नुहोस",

            //    CheckFileExists = true,
            //    CheckPathExists = true,

            //    DefaultExt = "pdf",
            //    Filter = "File Types (*.pdf)|*.pdf",
            //    FilterIndex = 2,
            //    RestoreDirectory = true,

            //    ReadOnlyChecked = true,
            //    ShowReadOnly = true
            //};

            //if (ofd.ShowDialog() == DialogResult.OK)
            //{
            //    file_name.Text = openFileDialog1.FileName;
            //}  
        }
        public void DisplayData()
        {
          try{
          //  firmi = "1";
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

            // con.Open();
            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT industryfileid, industryid, GetNumberToUnicode(industryregno) As 'उद्योग दर्ता नं', distcode, officeid, filetypename As 'फाइलको प्रकार', filename As 'फाइलको नाम',fileext As 'फाइल एक्स्टेन्सन', user As 'युजर', updatenep, updateengdate FROM industry_file INNER JOIN setup_filetype ON filetypeid=filetype WHERE     (industryid =@darta)", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@darta", firmi.ToString());
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView1.DataSource = tb;

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;

            sqlcon.con.Close();
          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.ToString());
          }
        }
        public void transactionid()
        {
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }

                MySqlCommand cmd = new MySqlCommand("select industryregno, fiscalyear, officeid, officedist from setup_industryregid where industryregid=5", sqlcon.con);
                cmd.CommandType = CommandType.Text;
                // cmd.Connection = con;
                MySqlDataReader sdr = cmd.ExecuteReader();
                sdr.Read();
                transid = global.csioid.ToString().Trim()  + sdr["industryregno"].ToString().Trim();

                sdr.Close(); //closing data reader
                sqlcon.con.Close();

                // if (sqlcon.con.State == ConnectionState.Closed)
                sqlcon.con.Open();

                MySqlCommand cmo = new MySqlCommand("update setup_industryregid SET industryregno= industryregno+1 WHERE industryregid=5", sqlcon.con);
                cmo.CommandType = CommandType.Text;
                //  cmo.Parameters.AddWithValue("@firmtype", firmtype.ToString());
                // int tid = Convert.ToInt32(cmo.ExecuteScalar());
                // cmo.Connection = sqlcon.con;

                //int n = qr.ExecuteNonQuery();

                int n = cmo.ExecuteNonQuery();
                // ind_type.Text = tid.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sqlcon.con.Close();
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                transactionid();
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
               
                string oldPath = file_name.Text;
                sendtoserver();
              //  string fileSavePath = @"D:\Software_files"+file_name.ToString();
//File.Copy(oldPath, fileSavePath, true); 

                //MemoryStream ms = new MemoryStream();
                //pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                //byte[] img = ms.ToArray();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO industry_file(industryfileid,industryid, industryregno, distcode, officeid, filetype,filename,fileext, user, updatenep)VALUES(@fileid,@firmid,GetUnicodeToNumber(@darta),@distcode,@officeid,@filetype,@filename,@fileext,@username,@nepdate)");

               
              //  firmi = "1";
                var fi = new FileInfo(file_name.Text);
                string extn = fi.Extension;
                string filename = fi.Name;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@fileid", transid);
                cmd.Parameters.AddWithValue("@firmid", firmi);
                cmd.Parameters.AddWithValue("@darta", darta_txt.Text);
                cmd.Parameters.AddWithValue("@distcode", global.csiodist);
                cmd.Parameters.AddWithValue("@officeid", global.csioid);
              //  cmd.Parameters.Add(new MySqlParameter("@citizenoff", citizen_off.SelectedValue));
                cmd.Parameters.Add(new MySqlParameter("@filetype", citizen_off.SelectedValue));
               // cmd.Parameters.Add(new MySqlParameter("@file", img));
                cmd.Parameters.Add(new MySqlParameter("@filename", newfilename));
                cmd.Parameters.Add(new MySqlParameter("@fileext", extn));
                // cmd.Parameters.Add(new SqlParameter("@curdate", curdate));
                cmd.Parameters.Add(new MySqlParameter("@nepdate", global.nepalidate));
                cmd.Parameters.Add(new MySqlParameter("@username", global.username));
                // cmd.Parameters.Add(new MySqlParameter("@nepdate", global.nepalidate));
                // cmd.Parameters.Add(new MySqlParameter("@username", global.username));
                cmd.Connection = sqlcon.con;
                int n = cmd.ExecuteNonQuery();
                if (n > 0)
                {
                    global.sync_dblog("industry_file", firmi.ToString(), "INSERT", transid, "industryfileid");
                    MessageBox.Show("फाईल सफलता पुर्वक सेभ भयो ।");
                    sqlcon.con.Close();
                    dataGridView1.DataSource = null;
                     DisplayData();
                    // cleardata();
                    button2.Enabled = false;
                    button5.Enabled = false;
                    // panel2.Enabled = false;
                    button1.Enabled = true;
                    button3.Enabled = true;
                    pictureBox1.Image = null;
                    axAcroPDF2.src = null;
                    sqlcon.con.Close();



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }




        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                button3.Enabled = true;
                pictureBox1.Image = null;
                axAcroPDF2.src = null;

                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                button5.Enabled = true;
                int i;
                DataTable dt = new DataTable();
                i = dataGridView1.CurrentRow.Index;
                string industryfileids;
                industryfileids = dataGridView1.Rows[i].Cells[0].Value.ToString();
                fileid.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                //MemoryStream ms = new MemoryStream();
                //FileStream fs;
                // byte[] bindata;
                MySqlCommand cmd = new MySqlCommand("SELECT filename,fileext FROM industry_file WHERE industryfileid='" + industryfileids.ToString() + "'", sqlcon.con);
                // cmd.Parameters.Add("@name", industryfileids);
                //cmd.SelectCommand.Parameters.AddWithValue("@name", industryfileids);
                //  bindata = (byte[])(cmd.ExecuteScalar());
                DataTable table = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(table);
                file_name.Text = table.Rows[0][0].ToString();
                //byte[] img = (byte[])table.Rows[0][0];
                //ms.Write(img, 0, img.Length);
               // pictureBox1.Image = new Bitmap(ms);
              //  string filelocation="\\Document\\";
              //  pictureBox1.Image = Image.FromFile(@"\\'"+servername+"'\\Document\\"+file_name.Text);
                var fi = new FileInfo(file_name.Text);
                string extn = fi.Extension;
                if (extn == ".pdf")
                {
                    panel_pdf.Visible = true;
                    panel_img.Visible = false;
                    axAcroPDF2.src=(@"\\" + servername + filelocation + file_name.Text);
                }
                else
                {
                    panel_pdf.Visible = false;
                    panel_img.Visible = true;
                    pictureBox1.Image = Image.FromFile(@"\\" + servername + filelocation + file_name.Text);
                }

              //  pictureBox1.Image = Image.FromFile(@"\\"+servername +filelocation+ file_name.Text);
                
                //  fs = new FileStream(name, FileMode.Create, FileAccess.Write);
                // ms.WriteTo(fs);



                //  MySqlDataAdapter da=new MySqlDataAdapter();
                //  // string f = comboBox2.Text;
                // string qu = "SELECT file FROM industry_file WHERE industryfileid=@name";

                //  MySqlDataAdapter com = new MySqlDataAdapter(qu, sqlcon.con);
                // com.SelectCommand.Parameters.AddWithValue("@name", industryfileids);
                //// da = new MySqlDataAdapter(sqlcon.con);
                //// dt = new DataTable();
                // com.Fill(dt);

                //  /////////////////////////////////////////
                //  DataSet ds = new DataSet();
                // // string filePath = @"D:\file\here.pdf";

                ////  using (MySqlDataAdapter da = new MySqlDataAdapter("SELECT file FROM industry_file WHERE industryfileid='"+industryfileids.ToString()+"'", sqlcon.con))
                // // {

                //     // da.Parameters.Add(new MySqlParameter("@name", industryfileids));
                //  byte[] bytes = (byte[])dt.Rows[0]["file"];
                //          //da.Fill(ds);
                //         // byte[] bytes = (byte[])ds.Tables[0].Rows[0][0];
                //          MemoryStream memStream = new MemoryStream(bytes);
                //       //   File.WriteAllBytes(filePath, bytes);

                //          string path = Path.GetTempPath();
                //          string file = Guid.NewGuid().ToString("N") + ".pdf";
                //          string filepath = Path.Combine(path, file);
                //          File.WriteAllBytes(filepath, bytes);
                //          Process p = new Process();
                //          p.StartInfo.FileName = filepath;
                //          p.Start();
                //          ProcessStartInfo startInfo = new ProcessStartInfo();
                //          startInfo.FileName = "AcroRd32.exe";
                //          startInfo.Arguments = filepath;
                //          Process.Start(startInfo);

                //  }
                //  

                //   axAcroPDF1.LoadFile(filePath);

                ///////////////////////////////////////

                //MySqlDataReader reader = com.ExecuteReader();

                //while (reader.Read())
                //{
                //    var files = (byte[])reader["file"];
                //    this.axAcroPDF1.LoadFile(files.ToString());
                //   // this.axAcroPDF1.LoadFile(files.ToString());
                //   // this.axAcroPDF1.LoadFile(file);
                //    //else use a method like 'File.WriteAllBytes(file);'
                //}
                //        }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            //Print image
            Bitmap bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(bm, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            e.Graphics.DrawImage(bm, 0, 0);
            bm.Dispose();
        }
        private void button3_Click(object sender, EventArgs e)
        {
           
            if (pictureBox1.Image != null)
            {
                PrintDialog pd = new PrintDialog();
                PrintDocument doc = new PrintDocument();
                doc.PrintPage += Doc_PrintPage;
                pd.Document = doc;
                if (pd.ShowDialog() == DialogResult.OK)
                    doc.Print();
                
            }
            else if(axAcroPDF2.src !=null)
            {
                axAcroPDF2.Print();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }


                MySqlCommand qrh = new MySqlCommand("INSERT INTO industry_file_hist(industryfileid, industryid, industryregno, distcode, officeid, filetype, filename, fileext, user, updatenep, updateengdate, user_hist) SELECT industryfileid, industryid, industryregno, distcode, officeid, filetype, filename, fileext, user, updatenep, updateengdate,@upduser FROM  industry_file WHERE industryfileid=@ownerid");




                qrh.CommandType = CommandType.Text;
                qrh.Parameters.AddWithValue("@ownerid", fileid);

                qrh.Parameters.AddWithValue("@upduser", global.username);

                qrh.Connection = sqlcon.con;
                int nh = qrh.ExecuteNonQuery();


                if (nh > 0)
                {
                    MySqlCommand qre = new MySqlCommand("DELETE FROM industry_file WHERE industryfileid=@ownerid");




                    qre.CommandType = CommandType.Text;
                    qre.Parameters.AddWithValue("@ownerid", fileid);
                    qre.Connection = sqlcon.con;
                    int nht = qre.ExecuteNonQuery();



                    if (nht > 0)
                    {


                        sqlcon.con.Close();
                        dataGridView1.DataSource = null;
                        //DisplayData();
                        //cleardata();
                        MessageBox.Show("बिवरण सफलतापूर्वक हटाइयो ।", "बिवरण हटाइयो", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        //button9.Enabled = false;
                        //button12.Enabled = false;
                        //panel2.Enabled = false;



                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //To where your opendialog box get starting location. My initial directory location is desktop.
            openFileDialog1.InitialDirectory = "C://Desktop";
            //Your opendialog box title name.
            openFileDialog1.Title = "Select file to be upload.";
            //which type file format you want to upload in database. just add them.
            openFileDialog1.Filter = "Select Valid Document(*.pdf; *.doc; *.jpg; *.xlsx; *.html)|*.pdf; *.docx; *.jpg; *.xlsx; *.html";
            //FilterIndex property represents the index of the filter currently selected in the file dialog box.
            openFileDialog1.FilterIndex = 1;
            try
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog1.CheckFileExists)
                    {
                        string path = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                        file_name.Text = path;
                    }
                }
                else
                {
                    MessageBox.Show("Please Upload document.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            sendtoserver();
            //try
            //{
            //    if (sqlcon.con.State == ConnectionState.Closed)
            //    {
            //        sqlcon.con.Open();
            //    }
            //  //  string filename = file_name.Text;
            //    var fi = new FileInfo(file_name.Text);
            //    string extn = fi.Extension;
            //    string filename = fi.Name;
            //    if (filename == null)
            //    {
            //        MessageBox.Show("Please select a valid document.");
            //    }
            //    else
            //    {
            //        //we already define our connection globaly. We are just calling the object of connection.
            //       // con.Open();
            //        MySqlCommand cmd = new MySqlCommand("INSERT INTO industry_file(industryid, industryregno, distcode, officeid, filetype, filename,fileext, user, updatenep)VALUES(@firmid,GetUnicodeToNumber(@darta),@distcode,@officeid,@filetype,@filename,@fileext,@username,@nepdate)");


            //        firmi = "1";

            //        cmd.CommandType = CommandType.Text;
            //        cmd.Parameters.AddWithValue("@firmid", firmi);
            //        cmd.Parameters.AddWithValue("@darta", darta_txt.Text);
            //        cmd.Parameters.AddWithValue("@distcode", 40);
            //        cmd.Parameters.AddWithValue("@officeid", 1);
            //        cmd.Parameters.Add(new MySqlParameter("@citizenoff", citizen_off.SelectedValue));
            //        cmd.Parameters.Add(new MySqlParameter("@filetype", citizen_off.SelectedValue));
            //       //cmd.Parameters.Add(new MySqlParameter("@file", img));
            //        cmd.Parameters.Add(new MySqlParameter("@filename", filename));
            //        cmd.Parameters.Add(new MySqlParameter("@fileext", extn));
            //        // cmd.Parameters.Add(new SqlParameter("@curdate", curdate));
            //        cmd.Parameters.Add(new MySqlParameter("@nepdate", ""));
            //        cmd.Parameters.Add(new MySqlParameter("@username", ""));
            //        // cmd.Parameters.Add(new MySqlParameter("@nepdate", global.nepalidate));
            //        // cmd.Parameters.Add(new MySqlParameter("@username", global.username));
            //        cmd.Connection = sqlcon.con;

            //        //qr.Parameters.AddWithValue("@firmid", firmi);
            //       // string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
            //        //string path = "http://127.0.0.1/Document/";
            //        //System.IO.File.Copy(file_name.Text, path + filename);

            //        //var client = new WebClient();
            //        //var uri = new Uri("http://www.yoursite.com/UploadMethod/");
            //        //try
            //        //{
            //        //    client.Headers.Add("fileName", System.IO.Path.GetFileName(fileName));
            //        //    var data = System.IO.File.ReadAllBytes(fileName);
            //        //    client.UploadDataAsync(uri, data);
            //        //}
            //        //catch (Exception ex)
            //        //{
            //        //    MessageBox.Show(ex.Message);
            //        //}

            //        int n = cmd.ExecuteNonQuery();

            //        if (n > 0)
            //        {
            //            MessageBox.Show("फाईल सफलता पुर्वक सेभ भयो ।");
            //            sqlcon.con.Close();
            //            dataGridView1.DataSource = null;
            //            DisplayData();
            //            // cleardata();
            //            button2.Enabled = false;
            //            button5.Enabled = false;
            //            // panel2.Enabled = false;
            //            button1.Enabled = true;
            //            sqlcon.con.Close();



            //        }

            //       // cmd.ExecuteNonQuery();
            //       // sqlcon.con.Close();

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            axAcroPDF2.src = null;
            button2.Enabled = true;
        }

        private void darta_txt_Leave(object sender, EventArgs e)
        {
            string[] udh = global.getSingleRowFromTable("SELECT ir.industrynepname, concat(ir.tole, ', ', replace(replace(replace(replace(setup_vdc.vdcunicodename, 'पालिका', 'पा.'), 'नगर', 'न.'), 'गाउँ', 'गा.'), 'महा', 'म.'), '-', GetNumberToUnicode(ir.industryward), ', ', setup_district.distunicodename) as 'उद्योग रहेको स्थान' FROM industryreg as ir " +
                    "INNER JOIN setup_district ON ir.industrydist = setup_district.distcode " +
                    "INNER JOIN setup_vdc ON ir.industryvdc = setup_vdc.VDC_SID ");
            if (!string.IsNullOrEmpty(udh[0].ToString()))
            {
                labelIndName.Text = "उद्योगको नाम: " + udh[0].ToString();
                labelIndName.Visible = true;
            }
            else
                labelIndName.Visible = false;


            if (!string.IsNullOrEmpty(udh[1].ToString()))
            {
                labelIndAddress.Text = "उद्योगको ठेगाना: " + udh[1].ToString();
                labelIndAddress.Visible = true;
            }
            else
                labelIndAddress.Visible = false;

        }
    }
}
