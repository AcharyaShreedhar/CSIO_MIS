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
    public partial class frmNotification : Form
    {
        public frmNotification()
        {
            InitializeComponent();
        }

        int MinLen = 74, maxLen = 575;
        Color smBack = Color.FromArgb(224, 224, 224); //Color.Honeydew
        Color bgBack = Color.LightSkyBlue;// Color.FromArgb(200,255,200);

        //values for buttons of notification
        string btnTxtSync = "सिन्क्रोनाइज गर्नुहोस्";
        string btnTxtUpd = "अपडेट गर्नुहोस्";

        private void frmNotification_Load(object sender, EventArgs e)
        {
            this.Width = MinLen;
            panelNotiDetail.Visible = false;
            labelTitle.Visible = false;
            panelSyncDist.Visible = false;

            this.BackColor = smBack;
            panelNoti.BackColor = this.BackColor;

            if (Convert.ToInt32(global.useroffice_category) >= 3)
            {
                LnkSyncDist.Enabled = false; //district wise detail is only for mantralay and nirdeshanalay
            }
            else
            {
                LnkSyncDist.Enabled = true; 
            }

            UpdateNotification();
            
        }

        
        private void UpdateNotification()
        {
            //this.Width = maxLen;
            //this.BackColor = bgBack;
            //panelNoti.BackColor = this.BackColor;

            int TotalNoti = 0;

            dgvNoti.Columns[0].Width = 330;

            int synCountLoc = global.CountNumToBeSync("local");
            int synCountSer = global.CountNumToBeSync("server");

            dgvNoti.Rows.Clear();

            if (synCountLoc > 0)
            {

                dgvNoti.Rows.Add();
                dgvNoti.Rows[dgvNoti.Rows.Count - 1].Cells[0].Value = "● " + global.convertNumToUnicode(synCountLoc.ToString()) + " रेकर्ड सर्भरमा सिन्क्रोनाइज हुन बाँकी छन् ।";
                dgvNoti.Rows[dgvNoti.Rows.Count - 1].Cells[1].Value = btnTxtSync;

                //DataGridViewButtonCell b = new DataGridViewButtonCell();
                //int rowIndex = MainTable.Rows.Add(b);
                //MainTable.Rows[rowIndex].Cells[0].Value = "name";

                //labelNotiDetail.Text = global.convertNumToUnicode(synCountLoc.ToString()) + " रेकर्ड सर्भरमा सिन्क्रोनाइज हुन बाँकी छन् ।";
                TotalNoti++;
            }

            if (synCountSer > 0)
            {
                dgvNoti.Rows.Add();
                dgvNoti.Rows[dgvNoti.Rows.Count - 1].Cells[0].Value = "● " + global.convertNumToUnicode(synCountSer.ToString()) + " नयाँ सेटिङ् अपडेट गर्न बाँकी छन् ।";
                dgvNoti.Rows[dgvNoti.Rows.Count - 1].Cells[1].Value = btnTxtUpd;
                TotalNoti++;
            }

            labelNotiCount.Text = global.convertNumToUnicode(TotalNoti.ToString());
            dgvNoti.ClearSelection();


            var height = 50;
            foreach (DataGridViewRow dr in dgvNoti.Rows)
            {
                height += dr.Height;
            }

            dgvNoti.Height = height;

            LnkSyncDist.Top = dgvNoti.Top + dgvNoti.Height + 20;

        }

        private void labelNotiClose_Click(object sender, EventArgs e)
        {
            panelNotiDetail.Visible = true;
            panelNoti_Click(sender, e);
        }

        private void panelNoti_Click(object sender, EventArgs e)
        {
            ShowHideNotificationDetail();
        }

        public void ShowHideNotificationDetail()
        {
            if (panelNotiDetail.Visible)
            {
                panelNotiDetail.Visible = false;
                panelSyncDist.Visible = false;
                labelTitle.Visible = false;
                this.Width = MinLen;
                this.BackColor = smBack;
                panelNoti.BackColor = this.BackColor;
                this.SendToBack();
            }
            else
            {
                panelNotiDetail.Visible = true;
                //panelSyncDist.Visible = true;
                labelTitle.Visible = true;
                this.Width = maxLen;
                this.BackColor = bgBack;
                panelNoti.BackColor = this.BackColor;
                this.BringToFront();
            }
        }

        private void dgvNoti_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                //TODO - Button Clicked - Execute Code Here

                if(senderGrid.CurrentCell.Value.ToString()==btnTxtSync)
                {
                    //SYNCHRONIZATION
                    global.sync_operation();
                    UpdateNotification();
                }
                else if(senderGrid.CurrentCell.Value.ToString()==btnTxtUpd)
                {
                    //MessageBox.Show("Update");
                    global.sync_updateFromServer();
                    UpdateNotification();
                }

            }
        }

        private void frmNotification_Leave(object sender, EventArgs e)
        {
            panelNotiDetail.Visible = true;
            panelNoti_Click(sender, e);
        }

        private void labelNotiRefresh_Click(object sender, EventArgs e)
        {
            UpdateNotification();
        }

        private void LnkSyncDist_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DisplaySyncDist();
            panelSyncDist.Visible = true;
            panelSyncDist.BringToFront();
        }

        private void btnCloseSyncDist_Click(object sender, EventArgs e)
        {
            panelSyncDist.Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dgvSyncDist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            global.fullsync_operation();
        }

        private void dgvNoti_Leave(object sender, EventArgs e)
        {
            dgvNoti.ClearSelection();
        }

        private void DisplaySyncDist()
        {
            try
            {
                if (sqlcononline.cononline.State == ConnectionState.Closed)
                    sqlcononline.cononline.Open();

                string sqstt= "SELECT scs.csioid, dist.distunicodename as 'जिल्ला', scs.csioNepNm as 'कार्यालय', getNumberToUnicode(CONCAT(getNepaliDate(DATE(CONVERT_Tz(scom.push_date,@@session.time_zone, '+05:45'))),' ',TIME(CONVERT_Tz(scom.push_date,@@session.time_zone, '+05:45')))) as 'पछिल्लो सिन्क्रोनाइज मिति',getNumberToUnicode(CONCAT(getNepaliDate(DATE(CONVERT_Tz(scom.pull_date,@@session.time_zone, '+05:45'))),' ',TIME(CONVERT_Tz(scom.pull_date,@@session.time_zone, '+05:45'))) ) as 'पछिल्लो अपडेट मिति' FROM sync_completed as scom INNER JOIN setup_csio as scs on scs.csioid=scom.csioid" +
                    " INNER JOIN setup_district as dist ON scs.csioDist = dist.distcode";
                MySqlDataAdapter da = new MySqlDataAdapter(sqstt,sqlcononline.cononline);


                DataTable tb = new DataTable();

                da.Fill(tb);
                dgvSyncDist.DataSource = null;
                dgvSyncDist.DataSource = tb;
                dgvSyncDist.Columns[0].Visible = false;
                dgvSyncDist.Columns[1].Width = Convert.ToInt32((11 / 100.0) * dgvSyncDist.Width);
                dgvSyncDist.Columns[2].Width = Convert.ToInt32((32/100.0) * dgvSyncDist.Width);
                dgvSyncDist.Columns[3].Width = Convert.ToInt32((28/100.0) * dgvSyncDist.Width);
                dgvSyncDist.Columns[4].Width = Convert.ToInt32((28/100.0) * dgvSyncDist.Width);

                //making columns not sortable
                foreach (DataGridViewColumn column in dgvSyncDist.Columns) 
                { column.SortMode = DataGridViewColumnSortMode.NotSortable; }

                dgvSyncDist.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
               
            }

        }
    }
}
