using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Threading.Tasks; //for asynchronous calls

namespace CSIO
{
    public partial class home : Form
    {
        string nepdate;
        string usernameg;
        // string stat;

        frmNotification frmNoti;
      
        public home()
        {
            InitializeComponent();
           
        }
        string Nepaliyear;
        int nepalimonth;
        int nepaliday;

        bool isExpand = false;

        public void adtobs( )
        {

        //    //SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");
        //    ////////////////////////
        //    DateTime curdat = new DateTime();
        //    curdat = DateTime.Now;


        //    MySqlCommand command;
        //    MySqlDataAdapter adapter = new MySqlDataAdapter();
        //    DataSet ds = new DataSet();
        //    if (sqlcon.con.State == ConnectionState.Closed)
        //    {
        //        sqlcon.con.Open();
        //    }
        //   // SqlConnection conn = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.password + "' ");

        //    string sql = "SELECT     Year, StDtEng, EndDTEng, NDNepM01, NDNepM02, NDNepM03, NDNepM04, NDNepM05, NDNepM06, NDNepM07, NDNepM08, NDNepM09, NDNepM10, NDNepM11, NDNepM12 FROM setup_nepcalender WHERE     ({ fn NOW() } BETWEEN StDtEng AND EndDTEng)";

        //    //string sql = "SELECT VdcCode, VdcNepnam FROM setup_vdc where setup_vdc.DistCode='" + comboBox3.ValueMember + "'";

        //    try
        //    {
                
        //        command = new MySqlCommand(sql, sqlcon.con);
        //        adapter.SelectCommand = command;
        //        adapter.Fill(ds);
        //        adapter.Dispose();
        //        command.Dispose();
        //        sqlcon.con.Close();
        //        // int i = 0;
        //        int k = 1;
        //       // string Nepaliyear;
        //       // int nepalimonth;
        //        //int nepaliday;
        //        foreach (DataRow row in ds.Tables[0].Rows)
        //        {
        //            Nepaliyear = (row[0].ToString());
        //            // label3.Text = Nepaliyear;
        //            DateTime stddate = new DateTime();
        //            stddate = Convert.ToDateTime(row[1].ToString());
        //            // MessageBox.Show(stddate.ToString());
        //            TimeSpan dt = curdat - stddate;
        //            //int daysdiff = diffResult.Days;
        //            //MessageBox.Show(dt.ToString());
        //            int d = dt.Days;
        //            // MessageBox.Show(d.ToString());
        //            // MessageBox.Show(d.ToString());
        //            for (int j = 3; j <= 14; j++)
        //            {

        //                // MessageBox.Show(d.ToString());
        //                int l = j + 1;
        //                //MessageBox.Show(row[l].ToString());
        //                if (d <= Convert.ToInt16(row[l].ToString()))
        //                {
        //                    nepalimonth = k;
        //                    // MessageBox.Show(nepalimonth.ToString());
        //                    nepaliday = d + 1;

        //                    date_txt.Text = Nepaliyear.ToString() + "/" + nepalimonth.ToString("00") + "/" + nepaliday.ToString("00");

        //                    nepdate = date_txt.Text;
        //                    break;
        //                }
        //                d = d - Convert.ToInt16(row[j].ToString());
        //                k = k + 1;


        //            }
        //        }



        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());

        //  }


            ////////////////////////


           // sqlcon.con.Close();

        }
        

        private void ShowNewForm(object sender, EventArgs e)
        {
            adduser usrset = new adduser();
            usrset.MdiParent = this;
           
            usrset.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            vdcedit usrset = new vdcedit();
            usrset.MdiParent = this;

            usrset.Show();
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            banijyaform bft = new banijyaform();
          
            bft.MdiParent = this;
           // bft.senddetail(usrn.ToString(), date_txt.ToString());
            bft.Show();

        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }
        public void getdata(string a)
        {
            usrn.Text = null;
            usrn.Text = a.ToString();
            //date_txt.Text = DateTime.Today.ToString("dd-MM-yyyy");
            usernameg = usrn.Text;
        }

        public void changefont()
        {

            int ex = Properties.Settings.Default.exint;
        //    MessageBox.Show(ex.ToString());
            if (ex >= 1)
            {
                MessageBox.Show("Your Software has been expired, Please contact to your software developer. Contact on 9846167600", "Software Expired");
                this.Close();
            }

        }
        public void checkbackdate()
        {
            DateTime curdate = DateTime.Now;
            DateTime storedate = DateTime.Parse(Properties.Settings.Default.newdatetime.ToString());
            int diff1 = (curdate - storedate).Days;
            // MessageBox.Show(diff1.ToString());
            if (diff1 < 0)
            {
                MessageBox.Show("Check Your System Date", "System Date Error");
                undoToolStripMenuItem.Visible = false;
                Properties.Settings.Default.password = "123456789";
                //  button1.Enabled = false;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.newdatetime = curdate;

                Properties.Settings.Default.Save();
            }
        }
        public void buttonenable()
        {
            // Menu Permission Checking for every menu in Home for user
            String uid = global.getSingleDataFromTable("SELECT access_permission FROM user_permission_assign WHERE menuid='1' AND userid='" + global.userid.ToString() + "'");
            if (uid == "" || uid == null)
            {
                uid = "false";
            }
            buttonUdhyogNew.Enabled = Boolean.Parse(uid);
            //END Button Visible
            String indsearch = global.getSingleDataFromTable("SELECT access_permission FROM user_permission_assign WHERE menuid='2' AND userid='" + global.userid.ToString() + "'");
            if (indsearch == "" || indsearch == null)
            {
                indsearch = "false";
            }
            buttonUdhyogSearch.Enabled = Boolean.Parse(indsearch);
            //END Button Visible
            String indrenew = global.getSingleDataFromTable("SELECT access_permission FROM user_permission_assign WHERE menuid='3' AND userid='" + global.userid.ToString() + "'");
            if (indrenew == "" || indrenew == null)
            {
                indrenew = "false";
            }
            buttonUdhyogRenew.Enabled = Boolean.Parse(indrenew);
            //END Button Visible
            String indclose = global.getSingleDataFromTable("SELECT access_permission FROM user_permission_assign WHERE menuid='4' AND userid='" + global.userid.ToString() + "'");
            if (indclose == "" || indclose == null)
            {
                indclose = "false";
            }
            buttonUdhyogClose.Enabled = Boolean.Parse(indclose);
            //END Button Visible
            String indedit = global.getSingleDataFromTable("SELECT access_permission FROM user_permission_assign WHERE menuid='5' AND userid='" + global.userid.ToString() + "'");
            if (indedit == "" || indedit == null)
            {
                indedit = "false";
            }
            buttonUdhyogEdit.Enabled = Boolean.Parse(indedit);
            //END Button Visible
            String indcert = global.getSingleDataFromTable("SELECT access_permission FROM user_permission_assign WHERE menuid='6' AND userid='" + global.userid.ToString() + "'");
            if (indcert == "" || indcert == null)
            {
                indcert = "false";
            }
            buttonUdhyogCert.Enabled = Boolean.Parse(indcert);
            //END Button Visible
            String indreport = global.getSingleDataFromTable("SELECT access_permission FROM user_permission_assign WHERE menuid='7' AND userid='" + global.userid.ToString() + "'");
            if (indreport == "" || indreport == null)
            {
                indreport = "false";
            }
            buttonUdhyogReport.Enabled = Boolean.Parse(indreport);
            //END Button Visible
            String indcloseletter = global.getSingleDataFromTable("SELECT access_permission FROM user_permission_assign WHERE menuid='8' AND userid='" + global.userid.ToString() + "'");
            if (indcloseletter == "" || indcloseletter == null)
            {
                indcloseletter = "false";
            }
           // buttonUdhyogCloseLetter.Enabled = Boolean.Parse(indcloseletter);
            //END Button Visible

            //Banijya Menu Enabled
            String banijyanew = global.getSingleDataFromTable("SELECT access_permission FROM user_permission_assign WHERE menuid='9' AND userid='" + global.userid.ToString() + "'");
            if (banijyanew == "" || banijyanew == null)
            {
                banijyanew = "false";
            }
            buttonBanijyaNewFirm.Enabled = Boolean.Parse(banijyanew);
            //END Button Enable
            String banijyaregletter = global.getSingleDataFromTable("SELECT access_permission FROM user_permission_assign WHERE menuid='10' AND userid='" + global.userid.ToString() + "'");
            if (banijyaregletter == "" || banijyaregletter == null)
            {
                banijyaregletter = "false";
            }
            buttonBinijyaFirmRegLetter.Enabled = Boolean.Parse(banijyaregletter);
            //END Button Enable
            String banijyaFirmUpdate = global.getSingleDataFromTable("SELECT access_permission FROM user_permission_assign WHERE menuid='11' AND userid='" + global.userid.ToString() + "'");
            if (banijyaFirmUpdate == "" || banijyaFirmUpdate == null)
            {
                banijyaFirmUpdate = "false";
            }
            buttonBanijyaFirmUpdate.Enabled = Boolean.Parse(banijyaFirmUpdate);
            //END Button Enable
            String banijyarenew = global.getSingleDataFromTable("SELECT access_permission FROM user_permission_assign WHERE menuid='12' AND userid='" + global.userid.ToString() + "'");
            if (banijyarenew == "" || banijyarenew == null)
            {
                banijyarenew = "false";
            }
            buttonBanijyaFirmRenew.Enabled = Boolean.Parse(banijyarenew);
            //END Button Enable
            String banijyaclose = global.getSingleDataFromTable("SELECT access_permission FROM user_permission_assign WHERE menuid='13' AND userid='" + global.userid.ToString() + "'");
            if (banijyaclose == "" || banijyaclose == null)
            {
                banijyaclose = "false";
            }
            buttonBanijyaFirmClose.Enabled = Boolean.Parse(banijyaclose);
            //END Button Enable
            String banijyasearchname = global.getSingleDataFromTable("SELECT access_permission FROM user_permission_assign WHERE menuid='14' AND userid='" + global.userid.ToString() + "'");
            if (banijyasearchname == "" || banijyasearchname == null)
            {
                banijyasearchname = "false";
            }
            buttonBanijyaFirmSearchName.Enabled = Boolean.Parse(banijyasearchname);
            //END Button Enable
            String banijyasearchdetail = global.getSingleDataFromTable("SELECT access_permission FROM user_permission_assign WHERE menuid='15' AND userid='" + global.userid.ToString() + "'");
            if (banijyasearchdetail == "" || banijyasearchdetail == null)
            {
                banijyasearchdetail = "false";
            }
            buttonBanijyaFirmSearch.Enabled = Boolean.Parse(banijyasearchdetail);
            //END Button Enable
            String banijyafirmupdatelet = global.getSingleDataFromTable("SELECT access_permission FROM user_permission_assign WHERE menuid='16' AND userid='" + global.userid.ToString() + "'");
            if (banijyafirmupdatelet == "" || banijyafirmupdatelet == null)
            {
                banijyafirmupdatelet = "false";
            }
            buttonBanijyaFirmUpdateLetter.Enabled = Boolean.Parse(banijyafirmupdatelet);
            //END Button Enable
            String banijyafirmcloselet = global.getSingleDataFromTable("SELECT access_permission FROM user_permission_assign WHERE menuid='17' AND userid='" + global.userid.ToString() + "'");
            if (banijyafirmcloselet == "" || banijyafirmcloselet == null)
            {
                banijyafirmcloselet = "false";
            }
            buttonBanijyaFirmCloseLetter.Enabled = Boolean.Parse(banijyafirmcloselet);
            //END Button Enable

            //Report Permission
            String indperiodicreport = global.getSingleDataFromTable("SELECT access_permission FROM user_permission_assign WHERE menuid='18' AND userid='" + global.userid.ToString() + "'");
            if (indperiodicreport == "" || indperiodicreport == null)
            {
                indperiodicreport = "false";
            }
            buttonReportUdhyog.Enabled = Boolean.Parse(indperiodicreport);
            //END Button Enable
            //Report Banijya Permission
            String banijyaperiodicreport = global.getSingleDataFromTable("SELECT access_permission FROM user_permission_assign WHERE menuid='19' AND userid='" + global.userid.ToString() + "'");
            if (banijyaperiodicreport == "" || banijyaperiodicreport == null)
            {
                banijyaperiodicreport = "false";
            }
            buttonReportBanijya.Enabled = Boolean.Parse(banijyaperiodicreport);

            //END Button Enable

            //Setting Permission
            String setupuser = global.getSingleDataFromTable("SELECT access_permission FROM user_permission_assign WHERE menuid='20' AND userid='" + global.userid.ToString() + "'");
            if (setupuser == "" || setupuser == null)
            {
                setupuser = "false";
            }
            buttonSetupUser.Enabled = Boolean.Parse(setupuser);

            //END Button Enable
            String setupoffice = global.getSingleDataFromTable("SELECT access_permission FROM user_permission_assign WHERE menuid='21' AND userid='" + global.userid.ToString() + "'");
            if (setupoffice == "" || setupoffice == null)
            {
                setupoffice = "false";
            }
            buttonSetupOffice.Enabled = Boolean.Parse(setupoffice);

            //END Button Enable
            String setupmun = global.getSingleDataFromTable("SELECT access_permission FROM user_permission_assign WHERE menuid='22' AND userid='" + global.userid.ToString() + "'");
            if (setupmun == "" || setupmun == null)
            {
                setupmun = "false";
            }
            buttonSetMuni.Enabled = Boolean.Parse(setupmun);
            //END Button Enable
            String setupfy = global.getSingleDataFromTable("SELECT access_permission FROM user_permission_assign WHERE menuid='23' AND userid='" + global.userid.ToString() + "'");
            if (setupfy == "" || setupfy == null)
            {
                setupfy = "false";
            }
            buttonSetFY.Enabled = Boolean.Parse(setupfy);
            //END Button Enable
            String setupAin = global.getSingleDataFromTable("SELECT access_permission FROM user_permission_assign WHERE menuid='24' AND userid='" + global.userid.ToString() + "'");
            if (setupAin == "" || setupAin == null)
            {
                setupAin = "false";
            }
            buttonSetAin.Enabled = Boolean.Parse(setupfy);
            //END Button Enable
            String setuppass = global.getSingleDataFromTable("SELECT access_permission FROM user_permission_assign WHERE menuid='25' AND userid='" + global.userid.ToString() + "'");
            if (setuppass == "" || setuppass == null)
            {
                setuppass = "false";
            }
            buttonChangePass.Enabled = Boolean.Parse(setuppass);
            //END Button Enable
            String setupdb = global.getSingleDataFromTable("SELECT access_permission FROM user_permission_assign WHERE menuid='26' AND userid='" + global.userid.ToString() + "'");
            if (setupdb == "" || setupdb == null)
            {
                setupdb = "false";
            }
            buttonSetDB.Enabled = Boolean.Parse(setupdb);
            //END Button Enable
            String setupdbbak = global.getSingleDataFromTable("SELECT access_permission FROM user_permission_assign WHERE menuid='27' AND userid='" + global.userid.ToString() + "'");
            if (setupdbbak == "" || setupdbbak == null)
            {
                setupdbbak = "false";
            }
            buttonSetDBBack.Enabled = Boolean.Parse(setupdbbak);
            //END Button Enable
            String setupcalendarset = global.getSingleDataFromTable("SELECT access_permission FROM user_permission_assign WHERE menuid='28' AND userid='" + global.userid.ToString() + "'");
            if (setupcalendarset == "" || setupcalendarset == null)
            {
                setupcalendarset = "false";
            }
            //setupcalendar.Enabled = Boolean.Parse(setupcalendarset);
            //END Button Enable
            String setuptest = global.getSingleDataFromTable("SELECT access_permission FROM user_permission_assign WHERE menuid='29' AND userid='" + global.userid.ToString() + "'");
            if (setuptest == "" || setuptest == null)
            {
                setuptest = "false";
            }
            //buttontest.Enabled = Boolean.Parse(setuptest);
            //Faat wise Menu
            String faat = global.getSingleDataFromTable("SELECT faat FROM login WHERE userid='" + global.userid.ToString() + "'");
            if (faat == "1")
            {
                buttonBanijya.Enabled = false;
                buttonReportBanijya.Enabled = false;
            }
            else if (faat == "2")
            {
                buttonUdhyog.Enabled = false;
                buttonReportUdhyog.Enabled = false;
            }
        }

        private void home_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;


            Controls.OfType<MdiClient>().FirstOrDefault().BackColor = Color.FromArgb(224,224,224);// Color.Honeydew;
            customizeDesign();

            frmNoti = new frmNotification();
            frmNoti.MdiParent = this;
            frmNoti.Dock = DockStyle.Right;
            frmNoti.Show();

            global.configid = "1";
           // changefont();
            officename.Text = global.csioffice;
            date_txt.Text = global.todaynepslash;
          // stat = global.status;
          // MessageBox.Show(stat.ToString());
           // fileMenu.Enabled = false;
            if(string.Equals(global.status.Trim(),"Operator"))
            {
                fileMenu.Enabled = false;
            }


            if (int.Parse(global.usertype) >3)
            {

                खजToolStripMenuItem.Visible = true;
                viewMenu.Visible = true;
                वणजयफरमकववरणToolStripMenuItem.Visible = true;
                //toolsMenu.Visible = true;
                menuSamshodhanUdhyog.Visible = true;
                उदयगपरतवदनToolStripMenuItem.Visible = true;
            }
            else 
            {

                खजToolStripMenuItem.Visible = true;
                viewMenu.Visible = true;
                वणजयफरमकववरणToolStripMenuItem.Visible = true;
               // toolsMenu.Visible = true;
                menuSamshodhanUdhyog.Visible = true;
                उदयगपरतवदनToolStripMenuItem.Visible = true;
            }

            if (int.Parse(global.usertype) < 2)
            {
                कनफयगरसनToolStripMenuItem.Visible = false;
                सरभरमतToolStripMenuItem.Visible = false;
            }


            //check local and remote server connection
            IsLocalServerConnected();
            IsRemoteServerConnected();

            //getNotification
            UpdateNotification();

            labelWait.Visible = false;

            this.Cursor = Cursors.Default;
        }


        private void IsLocalServerConnected()
        {
            try
            {
                localON.Visible = false;
                localOFF.Visible = false;
 
                //close the connection
                if (sqlcon.con.State == ConnectionState.Open)
                    sqlcon.con.Close();

                //try to open
                sqlcon.con.Open();

                localON.Visible = true;
                //return true;
            }
            catch (Exception ex)
            {
                //localOFF.ToolTipText = "Local Server: Not Connected \n " + ex.Message;
                localOFF.Visible=true;
            }
        }

        public void IsRemoteServerConnected()
        {
            try
            {
                remoteON.Visible = false;
                remoteOFF.Visible = false;

                //sqlcon.con.OpenAsync();

                if (sqlcononline.cononline.State == ConnectionState.Open)
                    sqlcononline.cononline.Close();

                //trying to open it
                sqlcononline.cononline.Open();

                remoteON.Visible = true;
                //return true;
            }
            catch (Exception ex)
            {
                //remoteOFF.ToolTipText= "Local Server: Not Connected \n " + ex.Message;
                remoteOFF.Visible = true;
            }
            finally
            {
                sqlcononline.cononline.Close();
            }
        }

        private void customizeDesign()
        {
            panelUdhyog.Visible=false ;
            panelBanijya.Visible = false;
            panelSetting.Visible = false;
            panelReport.Visible = false;
            panelLeft.Width = 215;
            panelBtnHamburger.Visible = true;
           // panelButtonRightArrow.Top = panelLeft.Height / 2;
            //panelBtnLeftArrow.Top = panelButtonRightArrow.Top;
            labelToolTip.Visible = false;
            labelToolTipLight.Visible = false;
            timer1.Enabled = false;
        }

        private void hideSubMenu()
        {
            panelUdhyog.Visible = false; //!panelUdhyog.Visible;
            panelBanijya.Visible = false; //!panelBanijya.Visible;
            panelReport.Visible = false;
            panelSetting.Visible = false; //!panelSetting.Visible;
        }

        private void showSubMenu(Panel subMenu)
        {

            Form fc = Application.OpenForms["backscreen"];

            if (fc != null)
            {
                fc.Close();
            }


            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        private void buttonUdhyog_Click(object sender, EventArgs e)
        {
            showSubMenu(panelUdhyog);
        }

        private void buttonBanijya_Click(object sender, EventArgs e)
        {
            showSubMenu(panelBanijya);
        }

        private void buttonSetting_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSetting);
        }

        private void fileMenu_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void viewMenu_Click(object sender, EventArgs e)
        {

        }

        private void statusStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void status_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //checkbackdate();
            industry indus = new industry();

            indus.MdiParent = this;
            //indus.senddetail(usrn.ToString(), date_txt.ToString());
            indus.Show();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void usrn_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel4_Click(object sender, EventArgs e)
        {

        }

        private void aflfHoKmdLkGUgToolStripMenuItem_Click(object sender, EventArgs e)
        {
            searchbanijya bft = new searchbanijya();


            bft.MdiParent = this;
            bft.Show();
        }

        private void toolStripStatusLabel3_Click(object sender, EventArgs e)
        {

        }

        private void pBfuKdf0fKqLkG6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchIndustryCertLetter ift = new SearchIndustryCertLetter();


            ift.MdiParent = this;
            ift.Show();
        }

        private void उदयगकववरणToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // checkbackdate();
            industrynameupdate updt = new industrynameupdate();
            updt.MdiParent = this;
            updt.Show();
        }

        private void हमरवरToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About abt = new About();
            abt.MdiParent=this;
            abt.Show();
        }

        private void वणजयफरमकववरणToolStripMenuItem_Click(object sender, EventArgs e)
        {
            banijyasamsodhan updt = new banijyasamsodhan();
            updt.MdiParent = this;
            updt.Show();
        }

        private void समशधनचठठपरनटवणजयToolStripMenuItem_Click(object sender, EventArgs e)
        {
            banijyasomsodhansearch updtletter = new banijyasomsodhansearch();
            updtletter.MdiParent = this;
            updtletter.Show();

        }

        private void लगतकटटकचठठपरनटवणजयToolStripMenuItem_Click(object sender, EventArgs e)
        {
            firmclosebanijyasearch updtletter = new firmclosebanijyasearch();
            updtletter.MdiParent = this;
            updtletter.Show();
        }

        private void वणजयफरमकनमवटखजनहसToolStripMenuItem_Click(object sender, EventArgs e)
        {
           searchdetailbanijya updtletter = new searchdetailbanijya();
            updtletter.MdiParent = this;
            updtletter.Show();
        }

        private void परमणपतरपरनटToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //checkbackdate();
            SearchIndustryCertLetter ift = new SearchIndustryCertLetter();


            ift.MdiParent = this;
            ift.Show();
        }

       

        private void कनफयगरसनToolStripMenuItem_Click(object sender, EventArgs e)
        {
            configurationset conf = new configurationset();
            conf.MdiParent = this;
            conf.Show();
            //this.Hide();
        }

        private void बहरजनहसToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void लगतकटटचठठपरनटToolStripMenuItem_Click(object sender, EventArgs e)
        {
            industryclosesearch icl = new industryclosesearch();


            icl.MdiParent = this;
            icl.Show();
        }

        private void समToolStripMenuItem_Click(object sender, EventArgs e)
        {
            industrysomsodhansearch iss = new industrysomsodhansearch();
            iss.MdiParent = this;
            iss.Show();
        }

        private void वणजयफरमकदरतमतवटखजToolStripMenuItem_Click(object sender, EventArgs e)
        {
            banijyasearchbydate iss = new banijyasearchbydate();
            iss.MdiParent = this;
            iss.Show();
        }

        private void वयकअपToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //backupdatabase isss = new backupdatabase();
            //isss.MdiParent = this;
            //isss.Show();
        }

        private void उदयगदरतमतअनसरखजToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void नवकरणटठToolStripMenuItem_Click(object sender, EventArgs e)
        {
             expiry ex = new expiry();
            ex.MdiParent = this;
            ex.Show();
        }

        private void ववरणअनसरखजToolStripMenuItem_Click(object sender, EventArgs e)
        {
           searchindustrydetailbyname ex = new searchindustrydetailbyname();
            ex.MdiParent = this;
            ex.Show();
        }

        private void उदयगपरतवदनToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            industryreportmonthly ex = new industryreportmonthly();
            ex.MdiParent = this;
            ex.Show();
        }

        private void सरभरमतToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setup_calendar ex = new setup_calendar();
            ex.MdiParent = this;
            ex.Show();
        }

        private void पसवरडवदलनहसToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changepassword ex = new changepassword();
            ex.MdiParent = this;
            ex.Show();

        }

        private void खजToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void औदयगकवयवसयऐनToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            setup_ain ex = new setup_ain();
            ex.MdiParent = this;
            ex.Show();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void परकषणरपरटToolStripMenuItem_Click(object sender, EventArgs e)
        {
            previewtestreport ex = new previewtestreport();
            ex.MdiParent = this;
            ex.Show();
        }

        private void औदयगकवयवसयऐनToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void editMenu_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void aflfHoKmdLkGUgToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }

        private void buttonBanijya_Click_1(object sender, EventArgs e)
        {
            showSubMenu(panelBanijya);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            showSubMenu(panelSetting);
        }


        private void panelBtnHamburger_MouseClick(object sender, MouseEventArgs e)
        {          
            timer1.Enabled = true;
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isExpand == true)
            {
                panelLeft.Width += 20;
                if (panelLeft.Width >= 215)
                {
                    panelLeft.Width = 215;
                    timer1.Enabled = false;
                    isExpand = false;
                }
            }
            else
            {
                panelLeft.Width -= 20;
                if (panelLeft.Width <= 50)
                {
                    panelLeft.Width = 50;
                    timer1.Enabled = false;
                    isExpand = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            searchbanijya updtletter = new searchbanijya();
            updtletter.MdiParent = this;
            updtletter.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            banijyaforminfo updtletter = new banijyaforminfo();
            updtletter.MdiParent = this;
            updtletter.Show();
        }

        private void buttonUdhyogNew_Click(object sender, EventArgs e)
        {
            industryreg bd = new industryreg();
            bd.MdiParent = this;

            //bd.getdataa(textBox2.Text, textBox3.Text, theli_no.Text, fy_combo.Text);

            bd.Show();
        }

        //private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        //{
           
        //    var hitTest = e.Node.TreeView.HitTest(e.Location);
        //    if (hitTest.Location == TreeViewHitTestLocations.PlusMinus)
        //        return;

        //    treeView1.CollapseAll();

        //    if (e.Node.IsExpanded)
        //        e.Node.Collapse();
        //    else
        //        e.Node.Expand();
        //}

        private void home_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            showSubMenu(panelReport);
        }

        private void buttonReportUdhyog_Click(object sender, EventArgs e)
        {
            industryreportmonthly inr = new industryreportmonthly();
            inr.MdiParent=this;
            inr.reportType="उद्योग सम्बन्धी प्रतिवेदन";
            inr.Show();
        }

        private void buttonReportBanijya_Click(object sender, EventArgs e)
        {
            banijyareportmonthly inr = new banijyareportmonthly();
            inr.MdiParent=this;
            inr.reportType="वाणिज्य सम्बन्धी प्रतिवेदन";
            inr.Show();
        }

        private void buttonUdhyogEdit_Click(object sender, EventArgs e)
        {
            उदयगकववरणToolStripMenuItem_Click(sender, e);
        }

        private void buttonUdhyogSearch_Click(object sender, EventArgs e)
        {
            industrysearchbydate isss = new industrysearchbydate();
            isss.MdiParent = this;
            isss.Show();
        }

        private void buttonUdhyogCert_Click(object sender, EventArgs e)
        {

            Form fc = Application.OpenForms["SearchIndustryCertLetter"];

            if (fc != null)
            {
                fc.Focus();
            }
            else
            {
                SearchIndustryCertLetter ift = new SearchIndustryCertLetter();
                ift.MdiParent = this;
                ift.Show();
            }

        }

        private void buttonUdhyogRenew_Click(object sender, EventArgs e)
        {
            industryrenew ift = new industryrenew();
            ift.MdiParent = this;
            ift.Show();
        }

        private void buttonUdhyogClose_Click(object sender, EventArgs e)
        {
            industryclose ift = new industryclose();
            ift.MdiParent = this;
            ift.Show();
        }

        private void buttonUdhyogReport_Click(object sender, EventArgs e)
        {
            industrysomsodhansearch iss = new industrysomsodhansearch();
            iss.MdiParent = this;
            iss.Show();
        }

        private void buttonUdhyogCloseLetter_Click(object sender, EventArgs e)
        {
            industryclosesearch icl = new industryclosesearch();

            icl.MdiParent = this;
            icl.Show();
        }

        private void buttonFirmSearch_Click(object sender, EventArgs e)
        {
            banijyasearchbydate icl = new banijyasearchbydate();

            icl.MdiParent = this;
            icl.Show();
        }

        private void buttonFirmRenew_Click(object sender, EventArgs e)
        {
            banijyafirmrenew icl = new banijyafirmrenew();

            icl.MdiParent = this;
            icl.Show();

        }

        private void buttonFirmUpdate_Click(object sender, EventArgs e)
        {
            banijyasamsodhan updt = new banijyasamsodhan();
            updt.MdiParent = this;
            updt.Show();
        }

        private void buttonFirmClose_Click(object sender, EventArgs e)
        {
            banijyafirmclose updt = new banijyafirmclose();
            updt.MdiParent = this;
            updt.Show();

        }

        private void buttonFirmUpdateLetter_Click(object sender, EventArgs e)
        {
            banijyasomsodhansearch updt = new banijyasomsodhansearch();
            updt.MdiParent = this;
            updt.Show();
        }

        private void buttonFirmCloseLetter_Click(object sender, EventArgs e)
        {
            banijyafirmclosesearch updt = new banijyafirmclosesearch();
            updt.MdiParent = this;
            updt.Show();

        }

        private void buttonSetAin_Click(object sender, EventArgs e)
        {
            setup_ain updt = new setup_ain();
          //  updt.MdiParent = this;
            updt.ShowDialog();

        }

        private void buttonSetDBBack_Click(object sender, EventArgs e)
        {
            backupdatabase updt = new backupdatabase();
           // updt.MdiParent = this;
            updt.ShowDialog();
        }

        private void buttonSetDB_Click(object sender, EventArgs e)
        {
            configurationset conf = new configurationset();
            conf.MdiParent = this;
            conf.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            setup_calendar conf = new setup_calendar();
            conf.MdiParent = this;
            conf.Show();
        }

        private void buttonChangePass_Click(object sender, EventArgs e)
        {
            changepassword conf = new changepassword();
           // conf.MdiParent = this;
            conf.Show();
        }

        private void buttonSetMuni_Click(object sender, EventArgs e)
        {
            vdcedit conf = new vdcedit();
           // conf.MdiParent = this;
            conf.ShowDialog();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            previewtestreport conf = new previewtestreport();
            conf.MdiParent = this;
            conf.Show();
        }

        private void buttonSetFY_Click(object sender, EventArgs e)
        {
           setupfy conf = new setupfy();
          //  conf.MdiParent = this;
            conf.ShowDialog();
        }

        private void buttonSetupDist_Click(object sender, EventArgs e)
        {
            Seup_office conf = new Seup_office();
          //  conf.MdiParent = this;
            conf.ShowDialog();
        }

        private void buttonFirmTransferLetter_Click(object sender, EventArgs e)
        {
            searchdetailbanijya conf = new searchdetailbanijya();
            conf.MdiParent = this;
            conf.Show();
        }

        private void buttonSetupUser_Click(object sender, EventArgs e)
        {
            adduser conf = new adduser();
           // conf.MdiParent = this;
            conf.ShowDialog();
        }

        // MOUSE HOVER OF MENU BUTTONS -------------------------------------------
        private void DisplayMenuToolTip(string txt)
        {
            labelToolTip.Text = txt;
            labelToolTip.Visible = true;
            // labelToolTip.Top = Control.MousePosition;
            labelToolTip.Top = System.Windows.Forms.Cursor.Position.Y - labelToolTip.Height;
            labelToolTip.Left = panelLeft.Width;
        }
        private void buttonUdhyog_MouseHover(object sender, EventArgs e)
        {
            DisplayMenuToolTip("उद्योग (Industry)");
        }

        private void buttonBanijya_MouseHover(object sender, EventArgs e)
        {
            DisplayMenuToolTip("वाणिज्य (Commerce)");
        }

        private void buttonReport_MouseHover(object sender, EventArgs e)
        {
            DisplayMenuToolTip("प्रतिवेदन (Report)");
        }

        private void buttonSetting_MouseHover(object sender, EventArgs e)
        {
            DisplayMenuToolTip("व्यवस्थापन (Settings)");
        }

        private void buttonUdhyog_MouseLeave(object sender, EventArgs e)
        {
            labelToolTip.Visible = false;
        }

        private void buttonUdhyogNew_MouseHover(object sender, EventArgs e)
        {
            DisplayMenuToolTip("नयाँ उद्योग दर्ता (Add New Industry)");
        }

        private void buttonUdhyogNew_MouseLeave(object sender, EventArgs e)
        {
            labelToolTip.Visible = false;
        }

        private void buttonBanijya_MouseLeave(object sender, EventArgs e)
        {
            labelToolTip.Visible = false;
        }

        private void buttonReport_MouseLeave(object sender, EventArgs e)
        {
            labelToolTip.Visible = false;
        }

        private void buttonSetting_MouseLeave(object sender, EventArgs e)
        {
            labelToolTip.Visible = false;
        }

        private void TSSLabelSignout_Click(object sender, EventArgs e)
        {
           if(MessageBox.Show(" साइन आउट गर्दा खुलेका फर्महरु बन्द हुन्छन र सेभ नभएका डाटा हराउन सक्छन् । \n के तपाईँ साइन आउट गर्न चाहनुहुन्छ ?", "साइन आउट", MessageBoxButtons.YesNo, MessageBoxIcon.Question,MessageBoxDefaultButton.Button1)==DialogResult.Yes)
            {
                Application.Restart();
                Environment.Exit(0);

                //this.Close();
                //login frmlogin = new login();
                //frmlogin.ShowDialog();
            }
        }

        private void TSSLabelLock_Click(object sender, EventArgs e)
        {
            //Form fc = Application.OpenForms["login"];

            //if (fc != null)
            //{
            //    fc.ShowDialog();
            //}
            //else
            //{
                //this.Close();
                login frmlogin = new login();
                frmlogin.LockApp();
                frmlogin.ShowDialog();
            //}
        }

        private void buttonUdhyogSearch_MouseHover(object sender, EventArgs e)
        {
            DisplayMenuToolTip("उद्योग विवरण खोजी (Search Industry Details)");
        }

        private void button12_MouseHover(object sender, EventArgs e)
        {
            DisplayMenuToolTip("उद्योग नविकरण (Industry Renewal)");
        }

        private void buttonUdhyogClose_MouseHover(object sender, EventArgs e)
        {
            DisplayMenuToolTip("उद्योग लगतकट्टा (Industry Closing)");
        }

        private void buttonUdhyogEdit_MouseHover(object sender, EventArgs e)
        {
            DisplayMenuToolTip("उद्योग संशोधन (Industry Update)");
        }

        private void buttonUdhyogCert_MouseHover(object sender, EventArgs e)
        {
            DisplayMenuToolTip("उद्योग दर्ता चिठी / प्रमाणपत्र (Industry Registration Letter / Certificate)");
        }

        private void buttonUdhyogReport_MouseHover(object sender, EventArgs e)
        {
            DisplayMenuToolTip("उद्योग संशोधन चिठी (Industry Update Letter)");
        }

        private void buttonUdhyogCloseLetter_MouseHover(object sender, EventArgs e)
        {
            DisplayMenuToolTip("उद्योग लगतकट्टा चिठी (Industry Closing Letter)");
        }

        private void buttonHome_Click(object sender, EventArgs e)
        {
            Form fc = Application.OpenForms["backscreen"];

            if (fc != null)
            {
                fc.Close();
            }

            backscreen bck = new backscreen();
            bck.MdiParent = this;
            bck.Show();
        }

        private void buttonHome_MouseHover(object sender, EventArgs e)
        {
            DisplayMenuToolTip("गृहपृष्ठ (Home)");
        }

        private void buttonHome_MouseLeave(object sender, EventArgs e)
        {
            labelToolTip.Visible = false;
        }

        //CALLED FROM OTHER TOOLS ALSO
        private void buttonSetupOffice_MouseHover(object sender, EventArgs e)
        {
            DisplayMenuToolTip(toolTip1.GetToolTip(sender as Button));
        }

        private void buttonSetupOffice_MouseLeave(object sender, EventArgs e)
        {
            labelToolTip.Visible = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            setup_remoteserver updtletter = new setup_remoteserver();
            updtletter.MdiParent = this;
            updtletter.Show();
        }

        private void localON_MouseHover(object sender, EventArgs e)
        {
            labelToolTipLight.Text = (sender as ToolStripStatusLabel).ToolTipText;
            labelToolTipLight.Visible = true;
            labelToolTipLight.Top = System.Windows.Forms.Cursor.Position.Y - 50; //- labelToolTip.Height;
            labelToolTipLight.Left = System.Windows.Forms.Cursor.Position.X - (labelToolTipLight.Width / 2);
            ToolStripStatusLabel tsb = sender as ToolStripStatusLabel;

            if (tsb.Name == "localON" || tsb.Name == "remoteON")
                labelToolTipLight.BackColor = Color.FromArgb(200, 255, 200);
            else
                labelToolTipLight.BackColor = Color.FromArgb(255, 200, 200);
        }

        private void localON_MouseLeave(object sender, EventArgs e)
        {
           labelToolTipLight.Visible = false;
        }

        private void remoteOFF_DoubleClick(object sender, EventArgs e)
        {
            IsRemoteServerConnected();
            labelToolTipLight.Visible = false;
        }

        private void localOFF_DoubleClick(object sender, EventArgs e)
        {
            IsLocalServerConnected();
            labelToolTipLight.Visible = false;
        }



        private void UpdateNotification()
        {
            //this.Width = maxLen;
            //this.BackColor = bgBack;
            //panelNoti.BackColor = this.BackColor;

            int TotalNoti = 0;

            //dgvNoti.Columns[0].Width = 330;

            int synCountLoc = global.CountNumToBeSync("local");
            int synCountSer = global.CountNumToBeSync("server");

            //dgvNoti.Rows.Clear();

            if (synCountLoc > 0)
            {
                //dgvNoti.Rows.Add();
                //dgvNoti.Rows[dgvNoti.Rows.Count - 1].Cells[0].Value = "● " + global.convertNumToUnicode(synCountLoc.ToString()) + " रेकर्ड सर्भरमा सिन्क्रोनाइज हुन बाँकी छन् ।";
                //dgvNoti.Rows[dgvNoti.Rows.Count - 1].Cells[1].Value = btnTxtSync;

                //DataGridViewButtonCell b = new DataGridViewButtonCell();
                //int rowIndex = MainTable.Rows.Add(b);
                //MainTable.Rows[rowIndex].Cells[0].Value = "name";

                //labelNotiDetail.Text = global.convertNumToUnicode(synCountLoc.ToString()) + " रेकर्ड सर्भरमा सिन्क्रोनाइज हुन बाँकी छन् ।";
                TotalNoti++;
            }

            if (synCountSer > 0)
            {
                //dgvNoti.Rows.Add();
                //dgvNoti.Rows[dgvNoti.Rows.Count - 1].Cells[0].Value = "● " + global.convertNumToUnicode(synCountSer.ToString()) + " नयाँ सेटिङ् अपडेट गर्न बाँकी छन् ।";
                //dgvNoti.Rows[dgvNoti.Rows.Count - 1].Cells[1].Value = btnTxtUpd;
                TotalNoti++;
            }

            //labelNotiCount.Text = global.convertNumToUnicode(TotalNoti.ToString());
            // dgvNoti.ClearSelection();

            TSSNotificationCount.Text = global.convertNumToUnicode(TotalNoti.ToString());
        }

        private void TSSNotification_Click(object sender, EventArgs e)
        {
            frmNoti.ShowHideNotificationDetail();
            frmNoti.Focus();
        }



        //public static bool isFormAlreadyOpen()
        //{
        //    if (ActiveMdiChild != null)
        //    {

        //        if (ActiveMdiChild.GetType().Name != "ChildForm2")
        //        {
        //            ActiveMdiChild.Close();
        //            ChildForm2 ch2 = new ChildForm2();
        //            ch2.MdiParent = this;
        //            ch2.Show();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Form is already open");
        //        }

        //    }
        //    else
        //    {
        //        ChildForm2 ch2 = new ChildForm2();
        //        ch2.MdiParent = this;
        //        ch2.Show();
        //    }
        //}
    }
}
