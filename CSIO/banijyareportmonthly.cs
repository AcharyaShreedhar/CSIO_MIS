using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;

namespace CSIO
{
    public partial class banijyareportmonthly : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        //FOR DRAGGING THE FORM ON DRAG OF TOP PANEL AS WELL AS THE FORM ITSELF

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }

            base.WndProc(ref m);
        }

        private void PanelTitle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        public banijyareportmonthly()
        {
            InitializeComponent();
        }
        int id;
        bool isReady = false;
        string myRepTitle = "";

        public string reportDomain;
        public string reportType;

        private void banijyareportmonthly_Load(object sender, EventArgs e)
        {

            global.createBorderAround(this, Color.Teal, 2);

            isReady = false; //for comboboxes
            lblTitle.Text = reportType; //Industry Report or Commerce Report

            global.fillCombo(comboMonth, "SELECT monthid, monthunicodename From setup_month", "monthunicodename", "monthid");
            global.fillCombo(comboFY, "SELECT FY_ID, GetNumberToUnicode(format_fiscal_yr(FY)) as FY From setup_fy", "FY", "FY_ID");
            global.fillCombo(comboFYChaumasic, "SELECT FY_ID, GetNumberToUnicode(format_fiscal_yr(FY)) as FY From setup_fy", "FY", "FY_ID");

            //global.fillCombo(comboYear, "SELECT concat('20',FY_ID) as FY_ID, GetNumberToUnicode(concat('20',FY_ID)) as FY From setup_fy", "FY", "FY_ID");

            //province
            global.fillCombo(comboProvince, "SELECT provinceid, provincenep From setup_province", "provincenep", "provinceid");

            comboProvince.SelectedValue = global.myProvinceId;
            comboProvince.Enabled = false;

            //district
            global.fillCombo(comboDistrict, "SELECT distcode, distunicodename FROM setup_district WHERE distzonecd=" + comboProvince.SelectedValue, "distunicodename", "distcode", "सबै जिल्ला");

            int YearNumber = 0;

            for (int iii = 0; iii <= comboFY.Items.Count - 1; iii++)
            {

                DataRowView FirstYear = comboFY.Items[iii] as DataRowView; //getting the first year
                if (FirstYear != null)
                    YearNumber = Convert.ToInt32(FirstYear.Row["FY_ID"]);

                comboYear.Items.Add(global.convertNumToUnicode(Convert.ToString(YearNumber)));

            }

            string curNepDate = global.todaynepslash;
            string MonNm = global.convertUnicodeToNum(curNepDate.Substring(5, 2));

            //For Baishak Jestha And Ashar - add one more year as they are from current fiscal year but Year is +1 from the value of FY 
            if (Convert.ToInt16(MonNm) >= 1 && Convert.ToInt16(MonNm) <= 3)
                YearNumber++;

            comboYear.Items.Add(global.convertNumToUnicode(YearNumber.ToString()));

            getCurMon();
            getCurChaumasic();
            getCurYear();

            darta_date.Text = global.todaynepslash;


            //positioning at the center of parent
            this.Top = 10;
            this.Left = this.Parent.Width / 2 - this.Width / 2;

            isReady = true; //for comboboxes
        }
        private void getCurChaumasic()
        {
            //DateTime curEngDate = DateTime.Today;

            string fys = global.getSingleDataFromTable("SELECT GetNumberToUnicode(format_fiscal_yr(FY)) FROM setup_fy WHERE FY_ID=" + global.fyid);
            comboFYChaumasic.Text = fys;

            string curNepDate = global.todaynepslash;
            string YearNm = global.convertUnicodeToNum(curNepDate.Substring(0, 4));
            string MonNm = global.convertUnicodeToNum(curNepDate.Substring(5, 2));
            int mno = Convert.ToInt16(MonNm);
            if (mno >= 4 && mno <= 7)
            {

                comboChaumasic.SelectedIndex = 0;
            }
            else if (mno >= 8 && mno <= 11)
            {
                comboChaumasic.SelectedIndex = 1;
            }
            else
            {
                comboFYChaumasic.SelectedIndex = comboFYChaumasic.SelectedIndex - 1;
                comboChaumasic.SelectedIndex = 2;
            }
        }

        private void getCurMon()
        {
            DateTime curEngDate = DateTime.Today;
            string curNepDate = global.todaynepslash;

            string YearNm = global.convertUnicodeToNum(curNepDate.Substring(0, 4));

            string MonNm = global.convertUnicodeToNum(curNepDate.Substring(5, 2));
            comboYear.Text = global.convertNumToUnicode(YearNm);

            DisplayMonth(YearNm, MonNm);
        }

        private void getPrevMon()
        {
            DateTime curEngDate = DateTime.Today;
            string curNepDate = global.todaynepslash;

            string YearNm = global.convertUnicodeToNum(curNepDate.Substring(0, 4));

            string MonNm = global.convertUnicodeToNum(curNepDate.Substring(5, 2));

            if (Convert.ToInt16(MonNm) == 1) // if it is Baishak go to previous year and 12th month
            {
                YearNm = Convert.ToString(Convert.ToInt16(YearNm) - 1);
                MonNm = "12";
            }
            else
            {
                MonNm = Convert.ToString(Convert.ToInt16(MonNm) - 1);
            }

            DisplayMonth(YearNm, MonNm);
        }

        private void DisplayMonth(string YearNumEng, string MonNumEng)
        {
            if (MonNumEng.Length == 1)
                MonNumEng = "0" + MonNumEng;

            string MonFld = "NDNepM" + MonNumEng;

            //DISPLAY IN DATE RANGE BOX
            //get total days in a month of a year (for last date of month)
            string monLD = global.getSingleDataFromTable("SELECT " + MonFld + " FROM setup_nepcalender WHERE Year=" + YearNumEng);

            darta_dateFrom.Text = global.convertNumToUnicode(YearNumEng) + "/" + global.convertNumToUnicode(MonNumEng) + "/०१";
            darta_dateTo.Text = global.convertNumToUnicode(YearNumEng) + "/" + global.convertNumToUnicode(MonNumEng) + global.convertNumToUnicode(monLD);

            //also display in the combobox
            if (Convert.ToInt16(MonNumEng) >= 1 && Convert.ToInt16(MonNumEng) <= 3) //For Baishak Jestha And Ashar - it is from previous Fiscal Year
                YearNumEng = Convert.ToString(Convert.ToInt16(YearNumEng) - 1);

            //comboYear.SelectedValue = YearNumEng;
            //comboMonth.SelectedValue = MonNumEng;
        }

        private void getCurYear()
        {
            string fys = global.getSingleDataFromTable("SELECT GetNumberToUnicode(format_fiscal_yr(FY)) FROM setup_fy WHERE FY_ID=" + global.fyid);
            comboFY.Text = fys;
            comboFYChaumasic.Text = fys;
        }

        private void linkThisMon_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            getCurMon();
            radioYearly.Checked = true;
        }

        private void radioPeriodic_CheckedChanged(object sender, EventArgs e)
        {
            comboFY.Enabled = radioYearly.Checked;
            if (comboFY.Enabled && isReady)
                comboFY_SelectedIndexChanged(sender, e);
        }
        private void comboFY_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isReady) return;

            string YearNumEng = Convert.ToString(comboFY.SelectedValue);
            string yearId = YearNumEng;
            if (YearNumEng.Length == 4)
                yearId = (YearNumEng).Substring(2, 2); //removing 20 from 2077 and getting 77 only

            string fys = global.getSingleDataFromTable("SELECT GetNumberToUnicode(format_fiscal_yr(FY)) FROM setup_fy WHERE FY_ID=" + global.fyid);
            // comboYear.Text = fys;


            //Display Fiscal year in the date range box
            darta_dateFrom.Text = global.convertNumToUnicode(YearNumEng) + "/०४/०१"; //Shrawan 1 gatey

            //getting ashad last day of next year i.e yearnum + 1 (e.g. if year is 2077/78 then -- yearnum is 2077, so datefrom is 2077/04/01 but dateto is 2078/03/31)
            YearNumEng = Convert.ToString(Convert.ToInt16(YearNumEng) + 1);
            string monLD = global.getSingleDataFromTable("SELECT NDNepM03 FROM setup_nepcalender WHERE Year=" + YearNumEng);
            darta_dateTo.Text = global.convertNumToUnicode(YearNumEng) + "/०३/" + global.convertNumToUnicode(monLD);

            //DISPLAY IN DATE RANGE BOX - By mistake it was done for Baishak to Chaitra
            //get total days in a month of a year (for last date of month)
            //darta_dateFrom.Text = global.convertNumToUnicode(YearNumEng) + "/०१/०१";
            //string monLD = global.getSingleDataFromTable("SELECT NDNepM12 FROM setup_nepcalender WHERE Year=" + YearNumEng);
            //darta_dateTo.Text = global.convertNumToUnicode(YearNumEng) + "/१२/" + global.convertNumToUnicode(monLD);
        }
        private void linkThisYear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            getCurYear();
        }

        private void linkTod_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            darta_date.Text = global.todaynepslash;
        }

        private void comboChaumasic_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isReady) return;
            string date1 = global.convertNumToUnicode(comboFYChaumasic.SelectedValue.ToString());
            switch (comboChaumasic.SelectedIndex)
            {

                case 0:
                    darta_dateFrom.Text = date1 + "\\०४\\०१";
                    darta_dateTo.Text = date1 + "\\०७\\" + global.convertNumToUnicode(global.getNepMonthTotDays(comboFY.SelectedValue.ToString(), "07"));
                    break;

                case 1:
                    darta_dateFrom.Text = date1 + "\\०८\\०१";
                    darta_dateTo.Text = date1 + "\\११\\" + global.convertNumToUnicode(global.getNepMonthTotDays(comboFY.SelectedValue.ToString(), "11"));
                    break;

                case 2: //ashar is from another year 
                    darta_dateFrom.Text = date1 + "\\१२\\०१";
                    string date2 = (Convert.ToInt16(comboFYChaumasic.SelectedValue) + 1).ToString();
                    string monthdays2 = global.getNepMonthTotDays(Convert.ToString((Convert.ToInt16(comboFY.SelectedValue) + 1)), "03");
                    darta_dateTo.Text = global.convertNumToUnicode(date2) + "\\०३\\" + global.convertNumToUnicode(monthdays2);
                    break;

                default:
                    getCurYear();
                    break;
            }
        }

        private void linkLastMon_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            getPrevMon();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
