using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MySql.Data.MySqlClient;
using System.Globalization; //to display rupees in nepali currency format
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.Office.Core;

namespace CSIO
{
    public partial class backscreen : Form
    {
        public backscreen()
        {
            InitializeComponent();
        }

        // Gets a NumberFormatInfo associated with the Nepalese culture.
        NumberFormatInfo NpFormat = new CultureInfo("ne-NP", false).NumberFormat;

        string conDate; //last transaction date

        private void backscreen_Load(object sender, EventArgs e)
        {
           this.BackColor= Color.FromArgb(224, 224, 224);
            string curYear = global.fy.Substring(0, 4);
            string dateFROM = global.convertUnicodeToNum(curYear) + "/04/01";

            //getting next year (2nd year of FY)
            curYear = (Convert.ToInt32(global.convertUnicodeToNum(curYear)) + 1).ToString();
            //getting number of days in Ashar of the 2nd year of FY
            string monLD = global.getSingleDataFromTable("SELECT NDNepM03 FROM setup_nepcalender WHERE Year=" + curYear);
            string dateTO = global.convertUnicodeToNum(curYear) + "/03/" + monLD;

            //district ID is the current district for Karyalay but ALL (i.e. blank) for Mantralay and Nirdeshanalay
            string provinceName = global.getSingleDataFromTable("SELECT provincenep FROM setup_province WHERE provinceid=" + global.myProvinceId);

            //this is to avoid a trailing प्रदेश in case of प्रदेश न. १ , प्रदेश न. २
            if (!provinceName.Contains("प्रदेश"))
                provinceName += " प्रदेश";

            string distID, distName;
            if (Convert.ToInt32(global.useroffice_category) > 2)
            {
                distID = global.csiodist;
                distName = global.getSingleDataFromTable("SELECT distunicodename FROM setup_district WHERE distcode=" + distID) + " जिल्लाको ";
            }
            else
            {
                distID = "";
                distName = provinceName + "को "; //गण्डकी प्रदेशको
            }

            //Main title for statistics
            labelStatTitle.Text = distName + " उद्योगसम्बन्धी विविध तथ्यांक";


            //string[] data1 = global.getSingleRowFromProcedure("GetIndustryUpdateReport", "@datefrom,@dateto,@distid", dateFROM + "," + dateTO + "," + distID);

            if (sqlcon.con.State == ConnectionState.Closed)
                sqlcon.con.Open();


            getSummaryStat();

            getRenewCrossedInustry();
            getRenewCrossedInustry();

            MySqlDataAdapter qry = new MySqlDataAdapter("CALL getMonthwiseindustryRegTotal(@datefrom,@dateto,@distid)", sqlcon.con);
            qry.SelectCommand.Parameters.AddWithValue("@datefrom", dateFROM);
            qry.SelectCommand.Parameters.AddWithValue("@dateto", dateTO);
            qry.SelectCommand.Parameters.AddWithValue("@distid", distID);
            DataTable tb = new DataTable();

            string[] hh; //Headers 
            string[] vv; //Values

            qry.Fill(tb);
            hh = new string[tb.Columns.Count];
            vv = new string[tb.Columns.Count];

            //vv = tb.Rows[0].ItemArray.Select(x => x.ToString()).ToArray();
           // if (tb.Rows.Count > 0)
           // {
                for (int j = 0; j < tb.Columns.Count; j++)
                {
                    hh[j] = tb.Columns[j].Caption;

                    if(tb.Rows.Count>0)
                        vv[j] = tb.Rows[0].ItemArray[j].ToString();
                }
            //}

            //DistrictwiseMonthwiseIndustry(dateFROM, dateTO,false); -districtwise count in datagrid

            labelTitleIndustryReg.Text = "आ.व." + global.convertNumToUnicode(global.fy) + " को उद्योग दर्ता संख्या";

            DrawBarDiagram(barIndustryReg, labelTitleIndustryReg.Text,hh,vv, ChartColorPalette.None, SeriesChartType.Column,false,false,0);

            //= tb.AsEnumerable().Select(r => r.Field<string>("type")).ToArray();
            // ColV = tb.AsEnumerable().Select(r => r.Field<string>("tot")).ToArray();

            //industry count doughnut charts

            //getting data -- used cast so that sum can be directly placed into string array from columns
            string sqSt = "SELECT ss.subcategory_unicodename as 'type',cast(count(vv.industryid) as varchar(20)) as 'tot' FROM setup_subcategory as ss LEFT OUTER JOIN view_all_industryrecord AS vv ON ss.subcategory_id=vv.industryscale WHERE ss.category_id = '8' GROUP BY ss.subcategory_name ORDER BY ss.subcategory_sn ASC";

            DataTable tb1 = global.getDataTableFromTable(sqSt);

           string[] ColH = tb1.AsEnumerable().Select(r => r.Field<string>("type")).ToArray();
           string[] ColV = tb1.AsEnumerable().Select(r => r.Field<string>("tot")).ToArray();

            tb1 = null;

            DrawPieChart (pieIndStar, labelpieIndStar.Text, ColH, ColV, ChartColorPalette.BrightPastel, SeriesChartType.Doughnut,true,true,40);


            //getting data -- used cast so that sum can be directly placed into string array from columns
            sqSt = "SELECT REPLACE(ss.subcategory_unicodename,'लिमिटेड कम्पनी','लि.') as 'type',cast(count(vv.industryid) as varchar(20)) as 'tot' FROM setup_subcategory as ss LEFT OUTER JOIN view_all_industryrecord AS vv ON ss.subcategory_id=vv.industrytype WHERE ss.category_id = '9' GROUP BY ss.subcategory_name ORDER BY ss.subcategory_sn ASC";

            tb1 = global.getDataTableFromTable(sqSt);

            ColH = tb1.AsEnumerable().Select(r => r.Field<string>("type")).ToArray();
            ColV = tb1.AsEnumerable().Select(r => r.Field<string>("tot")).ToArray();

            tb1 = null;

            DrawPieChart(pieIndKanuni, labelpieIndKanuni.Text, ColH, ColV, ChartColorPalette.BrightPastel, SeriesChartType.Doughnut,true,true,47);

            //Based on Category - वर्ग
            //getting data -- used cast so that sum can be directly placed into string array from columns -- see 'मा आधारित' ह्याज बिन रिप्लेस्ड बाइ '' ब्ल्यांक
            sqSt = "SELECT REPLACE(ss.subcategory_unicodename,'मा आधारित','') as 'type',cast(count(vv.industryid) as varchar(20)) as 'tot' FROM setup_subcategory as ss LEFT OUTER JOIN view_all_industryrecord AS vv ON ss.subcategory_id=vv.industrycat WHERE ss.category_id = '10' GROUP BY ss.subcategory_name ORDER BY ss.subcategory_sn ASC";

            tb1 = global.getDataTableFromTable(sqSt);

            ColH = tb1.AsEnumerable().Select(r => r.Field<string>("type")).ToArray();
            ColV = tb1.AsEnumerable().Select(r => r.Field<string>("tot")).ToArray();

            tb1 = null;

            DrawPieChart(pieIndBarga, labelpieIndBarga.Text, ColH, ColV, ChartColorPalette.BrightPastel, SeriesChartType.Doughnut,true,true,52);


            //Workers and Owners in pie charts
            ColH = new string[] { "महिला", "पुरुष" };
            ColV = global.getSingleRowFromTable("SELECT sum(femaleworker),sum(maleworker) FROM view_all_industryrecord");
             
            
           DrawPieChart(pieWorkerSex, labelPieWorkerSex.Text, ColH, ColV,ChartColorPalette.SeaGreen,SeriesChartType.Pie);

            //Workers and Owners in pie charts
            ColV = global.getSingleRowFromTable("SELECT sum(femaleowner),sum(maleowner) FROM view_all_industryrecord");
            DrawPieChart(pieOwnerSex, labelPieOwnerSex.Text, ColH, ColV, ChartColorPalette.SeaGreen, SeriesChartType.Pie);

            /* SQL STATEMENT TO GET INDUSTRY REGISTRATION BY MONTH
            SELECT mo.monthunicodename,substring(ir.regdat, 6, 2) as monthnum, count(ir.industryid) FROM `view_all_industryrecord` as ir RIGHT JOIN setup_month as mo ON mo.monthid = CAST(substring(ir.regdat, 6, 2) as int)
            GROUP BY mo.monthunicodename ORDER BY mo.month_fy_order
            */

            //TAX STATISTICS
            labelTitleTax.Text = "आ.व." + global.convertNumToUnicode(global.fy) + " को राजश्व";
            ColV = global.getSingleRowFromProcedure("getIndustryTaxReport", "@datefrom,@dateto,@distid", dateFROM + "," + dateTO + "," + distID);
            ColH =new string[] {"उद्योग दर्ता","नविकरण","संशोधन","लगत कट्टा"};

            // DrawBarDiagram(barIndustryReg,)
            //string[] columnNames = tb.Columns.Cast<DataColumn>()
            //.Select(x => x.ColumnName)
            //.ToArray();

            //string[] dataValues = tb.Rows[0].ItemArray.Select(x => x.ToString()).ToArray();

            DrawBarDiagram (barRajaswa, labelTitleTax.Text, ColH, ColV, ChartColorPalette.None, SeriesChartType.Bar,false,false,0);

           // //MALE FEMALE IN SINGLE CHART -- DROPPED THE IDEA
           // // Data arrays
           // string[] seriesArray = { "महिला", "पुरुष" };
           // string[] pointsArray = global.getSingleRowFromTable("SELECT sum(femaleowner) as 'fo', sum(maleowner) as 'mo' FROM view_all_industryrecord");

           // pieWorkerSex.Series.Clear();

           // DrawBarDiagramOwnerWorker(pieWorkerSex, "उद्यमी (उद्योग सञ्चालक)", seriesArray, pointsArray, ChartColorPalette.Pastel, SeriesChartType.Bar);


           //pointsArray = global.getSingleRowFromTable("SELECT sum(femaleworker) as 'fs', sum(maleworker) as 'mw' FROM view_all_industryrecord");

           // DrawBarDiagramOwnerWorker(pieWorkerSex, "रोजगार (उद्योग जनशक्ति)", seriesArray, pointsArray, ChartColorPalette.SeaGreen, SeriesChartType.Bar);

            //POSITION and SIZE OF THE FORM
            this.Left = 0; // (this.Parent.Width / 2) - (this.Width / 2);
            this.Top = 0;

            Rectangle recNew = new Rectangle();
            recNew = this.Parent.ClientRectangle;
            recNew.Height -= 4;
            recNew.Width -= 4;
            //this.Size = recNew.Size;
            this.Height = recNew.Height;
        }

        public void DrawBarDiagram(Chart chartControl, string ChartTitle, string[] seriesArray, string[] pointsArray, ChartColorPalette Chartcolor = ChartColorPalette.None, SeriesChartType chartType=SeriesChartType.Bar,bool ShowLegend=false,bool isIgnoreifZero=true,int labelAngle=0)
        {
            chartControl.Series.Clear();

            // Data arrays
            //string[] seriesArray = { "Cat", "Dog", "Bird", "Monkey" };
            //int[] pointsArray = { 2, 1, 7, 5 };

            //seriesArray = new string[] { "Cat", "Dog", "Bird", "Monkey" };

            // Set title but make visible -- need while saving as image
            chartControl.Titles.Add(ChartTitle);
            chartControl.Titles[0].Font = new System.Drawing.Font("Noto Sans", 11);
            chartControl.Titles[0].Visible = false;


            Series series = chartControl.Series.Add("series1");
            series.ChartType = chartType;
            series.IsValueShownAsLabel = true;
            series.IsVisibleInLegend = ShowLegend;
            series.SetCustomProperty("BarLabelStyle", "Right");
            series.SetCustomProperty("PixelPointWidth", "25");
            series.Font = new Font("Kalimati", 10, FontStyle.Regular);

            // Set Color and palette
            if (Chartcolor == ChartColorPalette.None)
            { //gradient
                series.Color = Color.DodgerBlue;
            }
            else
            {
                chartControl.Palette = Chartcolor;
            }

            series.BackGradientStyle = GradientStyle.LeftRight;
            series.BackSecondaryColor = Color.LightSkyBlue;

            //sorting
            series.Sort(PointSortOrder.Ascending, "X");


            //Hiding the grid
            chartControl.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            chartControl.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;

            //HIDE AXIS LABEL Y
            chartControl.ChartAreas[0].AxisY.LabelStyle.Enabled = false;


            //font
            chartControl.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Noto Sans", 11);
            chartControl.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Kalimati", 10);


            //chartControl.ChartAreas[0].AxisX.IsReversed = true;

            //reverse - i.e from to to bottom
            //chartControl.ChartAreas[0].AxisY.IsReversed = true;

            //label angle
            chartControl.ChartAreas[0].AxisX.LabelStyle.Angle = labelAngle;

            //so that every label is seen (every other was skipped when this was not added)
            chartControl.ChartAreas[0].AxisX.Interval = 1;

            chartControl.ChartAreas[0].Position.X = 0;
            chartControl.ChartAreas[0].Position.Width = 100;
            chartControl.ChartAreas[0].Position.Y = 5;
            chartControl.ChartAreas[0].Position.Height = 95;

            // Add series.
            for (int i = 0; i < seriesArray.Length; i++)
            {

                //MessageBox.Show(global.convertUnicodeToNum(pointsArray[i]));
                int myVal;
                if (string.IsNullOrEmpty(pointsArray[i]))
                    myVal = 0;
                else
                    myVal = Convert.ToInt32(global.convertUnicodeToNum(pointsArray[i]));

                
                if (!isIgnoreifZero || myVal > 0)
                    series.Points.AddXY(seriesArray[i], myVal);
            }
        }

        public void DrawBarDiagram2Level(Chart chartControl, string ChartTitle, string[] seriesArray, string[] pointsLabelArray, string[] pointsArray, ChartColorPalette Chartcolor = ChartColorPalette.None, SeriesChartType chartType = SeriesChartType.Bar, bool ShowLegend = false, bool isIgnoreifZero = true, int labelAngle = 0)
        {
            //chartControl.Series.Clear();

            // Data arrays
            //string[] seriesArray = { "Cat", "Dog", "Bird", "Monkey" };
            //int[] pointsArray = { 2, 1, 7, 5 };

            //seriesArray = new string[] { "Cat", "Dog", "Bird", "Monkey" };

            // Set title
            //chartControl.Titles.Add(ChartTitle);

            
            //Hiding the grid
            chartControl.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            chartControl.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;

            //font
            chartControl.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Kalimati", 10);
            chartControl.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Kalimati", 10);

            //chartControl.ChartAreas[0].AxisX.IsReversed = true;

            //reverse - i.e from to to bottom
            //chartControl.ChartAreas[0].AxisY.IsReversed = true;

            //label angle
            chartControl.ChartAreas[0].AxisX.LabelStyle.Angle = labelAngle;

            //so that every label is seen (every other was skipped when this was not added)
            chartControl.ChartAreas[0].AxisX.Interval = 1;

            chartControl.ChartAreas[0].Position.X = 0;
            chartControl.ChartAreas[0].Position.Width = 100;
            chartControl.ChartAreas[0].Position.Y = 0;
            chartControl.ChartAreas[0].Position.Height = 100;

            // Add series.
            for (int i = 0; i < seriesArray.Length; i++)
            {

                Series series = chartControl.Series.Add(seriesArray[i].ToString());
                series.ChartType = chartType;
                series.IsValueShownAsLabel = true;
                series.IsVisibleInLegend = ShowLegend;
                series.SetCustomProperty("BarLabelStyle", "Right");
                //series.SetCustomProperty("PixelPointWidth", "20");
                series.Font = new Font("Kalimati", 10, FontStyle.Regular);
                //chartControl.Series[i]["PixelPointWidth"] = "1";

                // Set Color and palette
                if (Chartcolor == ChartColorPalette.None)
                { //gradient
                    series.BackGradientStyle = GradientStyle.LeftRight;
                    series.BackSecondaryColor = Color.LightSkyBlue;
                    series.Color = Color.DodgerBlue;
                }
                else
                {
                    chartControl.Palette = Chartcolor;
                }

                //sorting
                series.Sort(PointSortOrder.Ascending, "X");

                for (int j = 0; j < pointsArray.Length; j++)
                {
                    //MessageBox.Show(global.convertUnicodeToNum(pointsArray[i]));
                    int myVal;
                    if (string.IsNullOrEmpty(pointsArray[j]))
                        myVal = 0;
                    else
                        myVal = Convert.ToInt32(global.convertUnicodeToNum(pointsArray[j]));

                    series.Points.AddXY(pointsLabelArray[j], myVal);
                }

            }
        }


        private void DrawPieChart(Chart chartControl, string ChartTitle, string[] heads, string[] values, ChartColorPalette ChartColor = ChartColorPalette.None, SeriesChartType chartType=SeriesChartType.Pie,bool ShowLegend=false, bool isIgnoreifZero = true,int legendSize=50)
        {
            //reset your chart series and legends
           chartControl.Series.Clear();
           chartControl.Legends.Clear();

            // Set palette
            chartControl.Palette = ChartColor;


            chartControl.Titles.Add(ChartTitle);
            chartControl.Titles[0].Font = new System.Drawing.Font("Noto Sans", 11);
            chartControl.Titles[0].Visible = false;


            //Add a new chart-series
            Series series= chartControl.Series.Add("series1");

            //angle
            series["PieStartAngle"] = "270";

            //if doughnut then place the label outside
            if(chartType==SeriesChartType.Doughnut)
                series["PieLabelStyle"] = "Inside";

            //set the chart-type
           series.ChartType = chartType;

            //SETTING LEGEND
            if (ShowLegend == true)
            {
                //Add a new Legend(if needed) and do some formating
                chartControl.Legends.Add("MyLegend");
                chartControl.Legends[0].LegendStyle = LegendStyle.Table;
                chartControl.Legends[0].Docking = Docking.Right;
                chartControl.Legends[0].Alignment = StringAlignment.Center;
                //chartControl.Legends[0].Title = ChartTitle;
                chartControl.Legends[0].BorderColor = Color.Black;
                chartControl.Legends[0].Font = new Font("Noto Sans", 10, FontStyle.Regular);
                //chartControl.Legends[0].CellColumns[0].Margins = new Margins(5, 5,5,5);
                //IF LEGEND IS SHOWN THEN Don't show the label on the diagram
                //series["PieLabelStyle"] = "Disabled";
                chartControl.Legends[0].MaximumAutoSize = legendSize;
                
                
            }
            else //if the legend is not shown then set 100 percent width and height of chart
            {
                chartControl.ChartAreas[0].Position.X = 1;
                chartControl.ChartAreas[0].Position.Width = 99;
                chartControl.ChartAreas[0].Position.Y = 1;
                chartControl.ChartAreas[0].Position.Height = 99;
                
            }

            // Set Color and palette
            if (ChartColor == ChartColorPalette.None)
            { //gradient
                series.Color = Color.DodgerBlue;
            }
            else
            {
                chartControl.Palette = ChartColor;
            }

            series.BackGradientStyle = GradientStyle.LeftRight;
            series.BackSecondaryColor = Color.LightSkyBlue;


            //GETTING TOTAL -- so that percent can be got ownself
            //int mytot = values.Sum(x => int.Parse(x)); //try to get total -- but generates error if value is not numeric
            int mytot = 0, myval = 0;
            for (int i = 0; i < values.Length; i++)
            {
                myval = (string.IsNullOrEmpty(values[i])) ? 0 : Convert.ToInt32(global.convertUnicodeToNum(values[i].ToString()));

                mytot += myval;
            }

            int pcc = 0; //count the number of points added

            for (int i = 0; i < heads.Length; i++)
            {
                int val1;
                if (string.IsNullOrEmpty(values[i]))
                    val1 = 0;
                else
                    val1 = Convert.ToInt32(global.convertUnicodeToNum(values[i].ToString()));


                //add the series and set other task if ignoring if zero is false OR value is more than zero
                if(!isIgnoreifZero || val1>0)
                {
                    series.Points.AddXY(heads[i], val1);

                    if (ShowLegend == false)
                    {
                        if (chartType == SeriesChartType.Doughnut)
                            series.Label = "#VALX\n#VALY (#PERCENT{P0})"; //display label, value and percent (with 0 digit after decimal {P0} rounds up 
                        else
                            series.Label = "#VALX\n#VALY\n(#PERCENT{P0})";

                        series.Font = new Font("Kalimati", 9, FontStyle.Regular);
                    }
                    else
                    {
                        string PC = global.convertNumToUnicode(Math.Round((double)val1 / (mytot / 100.0), 1).ToString()) + "%";

                            series.Points[pcc].Label = PC;
                            //MANUALLY ADDING LABEL AND PERCENTAGE -- just for NOTO FONT
                            series.Points[pcc].LegendText = heads[i] + " (" + global.convertNumToUnicode(val1.ToString()) + ")";
                        series.Font = new Font("Noto Sans", 10, FontStyle.Regular);
                    }

                    pcc++; //increase the count of points added to series

                }
              
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var height = 40;
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                height += dr.Height;
            }

            dataGridView1.Height = height;
        }

        private void getSummaryStat()
        {
            
            string sqst = "SELECT MAX(regdat) FROM industryreg";
            conDate = global.getSingleDataFromTable(sqst); //declared on top
            if (string.IsNullOrEmpty(conDate))
                conDate = global.todaynepslash;

            sqst = ""; 
            labelTitleSummary.Text = "पछिल्लो कारोबार मिति (" + global.convertNumToUnicode(conDate).ToString() + ") को \n उद्योग प्रशासन सम्बन्धी कार्यको सारांश";

            //COALESCE function is used to convert Null to empty string;
            sqst = "SELECT IFNULL(count(industryid),0), IFNULL(sum(tax),0) FROM industryreg WHERE regdat=@datecon UNION ALL " +
                " SELECT IFNULL(count(industryid),0), IFNULL(sum(tax),0) FROM industryrenew WHERE decisiondate = @datecon UNION ALL " +
                " SELECT IFNULL(count(distinct industryid),0), IFNULL(sum(tax),0) FROM update_industry WHERE decisiondate = @datecon UNION ALL " +
                " SELECT IFNULL(count(industryid),0), IFNULL(sum(tax),0) FROM industryclose WHERE decisiondate = @datecon";

            DataTable dt = global.getDataTableFromTable(sqst, "@datecon", conDate);

            //dataGridView1.DataSource = dt;

            NpFormat.NumberDecimalDigits = 0; //Globalization formatting -- Only comma separated, NO decimal points in number of industries

            int vv = Convert.ToInt32(dt.Rows[1].ItemArray[0].ToString());

            labelSummary.Text = "● जम्मा उद्योग दर्ता: " + 
                global.convertNumToUnicode(Convert.ToInt64(dt.Rows[0].ItemArray[0].ToString()).ToString("N", NpFormat));

            labelSummary.Text += "\n● जम्मा उद्योग नविकरण: " +
                global.convertNumToUnicode(Convert.ToInt64(dt.Rows[1].ItemArray[0].ToString()).ToString("N", NpFormat));

            labelSummary.Text += "\n● जम्मा उद्योग ‍संशोधन: " + 
                global.convertNumToUnicode(Convert.ToInt64(dt.Rows[2].ItemArray[0].ToString()).ToString("N", NpFormat));

            labelSummary.Text += "\n● जम्मा उद्योग लगतकट्टा: " +
                global.convertNumToUnicode(Convert.ToInt32(dt.Rows[3].ItemArray[0]).ToString("N", NpFormat));



            NpFormat.NumberDecimalDigits = 2; //comma separated, 2 decimal points in rajaswa -- रू  is added by "C" 

            decimal rajaswa, totRajaswa = 0;

            labelSummaryTaxNum.Text = "";

            for (int ii=0;ii<dt.Rows.Count;ii++)
            {
                rajaswa = Convert.ToDecimal(dt.Rows[ii].ItemArray[1]);

                labelSummaryTaxNum.Text += global.convertNumToUnicode(rajaswa.ToString("C", NpFormat)) + "\n";
                totRajaswa += rajaswa;
            }

            labelTotalTaxNum.Text += global.convertNumToUnicode(totRajaswa.ToString("C", NpFormat));

            //labelSummaryTax.Text = " राजश्व: " + global.convertNumToUnicode(Convert.ToInt32(dt.Rows[0].ItemArray[1]).ToString("C", NpFormat));
            //labelSummaryTax.Text += "\n राजश्व: " + global.convertNumToUnicode(Convert.ToInt32(dt.Rows[1].ItemArray[1]).ToString("C", NpFormat));
            //labelSummaryTax.Text += "\n राजश्व: " + global.convertNumToUnicode(Convert.ToInt32(dt.Rows[2].ItemArray[1]).ToString("C", NpFormat));
            //labelSummaryTax.Text += "\n राजश्व: " + global.convertNumToUnicode(Convert.ToInt32(dt.Rows[3].ItemArray[1]).ToString("C", NpFormat));
            //select count(industryid) as 'tot',sum(tax) as 'tax' FROM industryreg WHERE regdat = (select max(ir.regdat) FROM industryreg as ir)//
        }

        private void getRenewCrossedInustry()
        {
            string indc="";
            int totindc=0;

            /*
            //Get count of industries who haven't renewed at all and have crossed their renew date
            string stmt = "SELECT COUNT(industryid) FROM industryreg WHERE regdat<getnepalidate(DATE_ADD(CURRENT_DATE,INTERVAL -5 YEAR)) " +
                "AND industrytype<>93 AND industryid NOT in(SELECT industryid FROM industryrenew)";

            indc = global.getSingleDataFromTable(stmt);
            if (string.IsNullOrEmpty(indc))
                indc = "0";
            totindc = Convert.ToInt32(indc);

            //get count of industries whose latest renewal date before 2 years (i.e. renew date is crossed)
            stmt = "SELECT COUNT(industryid) from industryrenew where (industryid,decisiondate) IN (SELECT industryid, MAX(decisiondate) FROM industryrenew GROUP BY industryid) AND decisiondate< getnepalidate(DATE_ADD(CURRENT_DATE,INTERVAL -2 YEAR))";

            indc = global.getSingleDataFromTable(stmt);
            if (string.IsNullOrEmpty(indc))
                indc = "0";
            totindc += Convert.ToInt32(indc);
            */
            string stmt = "SELECT COUNT(industryid) FROM industryreg WHERE renewdate < getnepalidate(CURRENT_DATE)";
            indc = global.getSingleDataFromTable(stmt);
            if (string.IsNullOrEmpty(indc))
                indc = "0";
            totindc = Convert.ToInt32(indc);

            LabelIndustryRenewCrossed.Text = global.convertNumToUnicode(totindc.ToString());
        }

        private void buttonExcelIndSummary_Click(object sender, EventArgs e)
        {
            //PrepareDataGridForExportIndSummary(); called from exportdetailtoexcelindsummary function 4 times/
            ExportDetailtoExcelIndSummary();
        }

        private void PrepareDataGridForExportIndSummary(string mytype)
        {
            //common for all
            string SqStmtMain = "SELECT ir.industryid, GetNumberToUnicode(ROW_NUMBER() OVER(ORDER BY industryid)) AS 'क्र.स.',  ir.industrynepname AS 'उद्योगको नाम', GetNumberToUnicode(ir.industryregno) AS 'दर्ता नं', GetNumberToUnicode(ir.regdat) AS 'दर्ता मिति'," +
            "concat(ir.tole, ', ', replace(replace(replace(replace(setup_vdc.vdcunicodename, 'पालिका', 'पा.'), 'नगर', 'न.'), 'गाउँ', 'गा.'), 'महा', 'म.'), '-', GetNumberToUnicode(ir.industryward), ', ', setup_district.distunicodename) as 'उद्योग रहेको स्थान', " +
            " ir.branch AS 'शाखा', ssc1.subcategory_unicodename AS 'स्तर', ssc2.subcategory_unicodename AS 'कानूनी रुप', ssc3.subcategory_unicodename AS 'वर्ग', ir.karobar AS 'उद्देश्य', GetNumberToUnicode(ir.statcapital) AS 'स्थिर पुँजी', GetNumberToUnicode(ir.varcapital) AS 'चालु पुँजी',";

            string SqStmtJoin = "INNER JOIN setup_district ON ir.industrydist = setup_district.distcode " +
                                "INNER JOIN setup_vdc ON ir.industryvdc = setup_vdc.VDC_SID " +
                                "INNER JOIN setup_subcategory AS ssc1 ON ir.industryscale = ssc1.subcategory_id " +
                                "INNER JOIN setup_subcategory AS ssc2 ON ir.industrytype = ssc2.subcategory_id " +
                                "INNER JOIN setup_subcategory AS ssc3 ON ir.industrycat = ssc3.subcategory_id ";

            string SqStmt = "";

            switch (mytype)
            {
                case "darta":
                    
                    SqStmt=SqStmtMain + " ir.comment as 'कैफियत', ir.tax as 'राजश्व' FROM industryreg as ir " + SqStmtJoin + 
                                        " WHERE ir.regdat='" + conDate + "'";
                    break;

                case "nabikaran":

                    SqStmt=SqStmtMain + " iwn.comment as 'कैफियत', iwn.tax as 'राजश्व' FROM industryreg as ir " +
                                        " INNER JOIN industryrenew AS iwn ON ir.industryid=iwn.industryid " + SqStmtJoin +
                                        " WHERE iwn.decisiondate='" + conDate + "'";
                    break;

                case "samshodhan":

                    SqStmt = SqStmtMain + " isu.comment as 'कैफियत', isu.tax as 'राजश्व' FROM industryreg as ir " +
                                        " INNER JOIN update_industry AS isu ON ir.industryid=isu.industryid " + SqStmtJoin +
                                        " WHERE isu.decisiondate='" + conDate + "'";

                    break;
                case "lagatkatta":

                    SqStmt = SqStmtMain + " icl.comment as 'कैफियत', icl.tax as 'राजश्व' FROM industryreg as ir " +
                                        " INNER JOIN industryclose AS icl ON ir.industryid=icl.industryid " + SqStmtJoin +
                                        " WHERE icl.decisiondate='" + conDate + "'";

                    break;
            }
           
            if (sqlcon.con.State == ConnectionState.Closed)
                sqlcon.con.Open();

            MySqlDataAdapter sqDA = new MySqlDataAdapter();
            MySqlCommand sqCmd = new MySqlCommand();

            sqCmd.CommandText = SqStmt;

            sqCmd.Connection = sqlcon.con;
            sqDA.SelectCommand = sqCmd;

            DataTable tb = new DataTable();
            sqDA.Fill(tb);
            dataGridView1.DataSource = tb;
            sqlcon.con.Close();
        }

        private void ExportDetailtoExcelIndSummary()
        {
            // creating new WorkBook within Excel application  
            Excel.Application xlApp;
            Excel.Workbook xlwb;
            Excel.Worksheet xlws;
            Excel.Range xlrg;
            xlApp = new Excel.Application();
            //string template = Application.StartupPath + "\\csio_industry_detail_report_template_new4.xlsx";
            //xlwb = xlApp.Workbooks.Add(template);
            xlwb = xlApp.Workbooks.Add();
            xlws = xlwb.Worksheets[1];
            object misValue = System.Reflection.Missing.Value;

            try
            {
                //OFFICE HEADINGS
                string sqsm = "SELECT govtname,ministryname,departmentname,officeunicodename,office_address,provincename, GetNumberToUnicode(phone),GetNumberToUnicode(fax),email FROM setup_office where isCur=1";

                string[] offhead = global.getSingleRowFromTable(sqsm);

                string myHead = offhead[0];
                myHead += "\n " + offhead[1];
                myHead += "\n " + offhead[2];
                myHead += "\n " + offhead[3] + ", " + offhead[4];
                myHead += "\n " + offhead[5];

                //SARKAR HEADING
                int hps = 1; //header position
                xlrg = xlws.Range[xlws.Cells[hps, 1], xlws.Cells[hps, 10]];
                xlrg.Merge();
                xlrg.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xlrg.Cells.Font.Size = "12";
                xlrg.Cells.Font.Bold = true;
                xlws.Cells[hps, 1] = offhead[0];

                //MANTRALAY HEADING
                hps++;
                xlrg = xlws.Range[xlws.Cells[hps, 1], xlws.Cells[hps, 10]];
                xlrg.Merge();
                xlrg.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xlrg.Font.Size = "13";
                xlrg.Font.Bold = true;
                xlws.Cells[hps, 1] = offhead[1];

                //Nirdeshanalay HEADING
                if (Convert.ToInt32(global.useroffice_category) > 1)
                {
                    hps++;
                    xlrg = xlws.Range[xlws.Cells[hps, 1], xlws.Cells[hps, 10]];
                    xlrg.Merge();
                    xlrg.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlrg.Font.Size = "13";
                    xlrg.Font.Bold = true;
                    xlws.Cells[hps, 1] = offhead[2];
                }

                //Karyalay HEADING
                if (Convert.ToInt32(global.useroffice_category) > 2)
                {
                    hps++;
                    xlrg = xlws.Range[xlws.Cells[hps, 1], xlws.Cells[hps, 10]];
                    xlrg.Merge();
                    xlrg.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlrg.Font.Size = "14";
                    xlrg.Font.Bold = true;
                    xlws.Cells[hps, 1] = offhead[3] + ", " + offhead[4];
                }

                hps++;
                xlrg = xlws.Range[xlws.Cells[hps, 1], xlws.Cells[hps, 10]];
                xlrg.Merge();
                xlrg.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xlrg.Font.Size = "12";
                xlrg.Font.Bold = true;
                xlws.Cells[hps, 1] = offhead[5];

                //ADDRESS HEADINGS AT THE RIGHT SIDE
                //Address - Phone Number
                xlrg = xlws.Range[xlws.Cells[1, 11], xlws.Cells[1, 13]];
                xlrg.Merge();
                xlws.Cells[1, 11] = "फोन नं:" + offhead[6];

                //Address - FAX
                xlrg = xlws.Range[xlws.Cells[2, 11], xlws.Cells[2, 13]];
                xlrg.Merge();
                xlws.Cells[2, 11] = "फ्याक्स नं: " + offhead[7];

                //Address - Email
                xlrg = xlws.Range[xlws.Cells[3, 11], xlws.Cells[3, 13]];
                xlrg.Merge();
                xlws.Cells[3, 11] = "इमेल: " + offhead[8];

                hps++;

                string logogov = Application.StartupPath + "\\logogov.png";
                xlws.Shapes.AddPicture(@logogov, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 0, 0, 85, 58);

               
                //Title and filename
                string mytitle = "मिति " + global.convertNumToUnicode(conDate).ToString() + " मा उद्योग प्रशासन कार्य (दर्ता, नविकरण, संशोधन, लगतकट्टा) भएका उद्योगको विवरण";
               string myfilename = "industry_detail_" + conDate.Replace("/","_");

                xlrg = xlws.Range[xlws.Cells[hps, 1], xlws.Cells[hps, 10]];
                xlrg.Merge();
                xlws.Cells[hps, 1] = mytitle;
                xlrg.Cells.Font.Size = "13";
                xlrg.Cells.Font.Bold = true;

                //con.Open();
                if (sqlcon.con.State == ConnectionState.Closed)
                    sqlcon.con.Open();

                //datagrid view is filled with required data
                PrepareDataGridForExportIndSummary("darta");
                DataGridView tb = dataGridView1;

                DataTable otb = new DataTable();

                //INDUSTRY REGISTRtTION -- FIRST PART OF THE REPORT
                // FILLING THE DATA
                //as per the template - the first data is in 7th row but col is always 1
                //so added 7 in j (j+7, k+1)

                int fps, nps; //first position of data (starting from header) and new position for each row
                fps = nps = hps + 2; //this is for the new position of row (in excel)
                int j = 0, k = 0, m = 0, n = 0;

                //COLUMN HEADINGS of INDUSTRY
                for (k = 1; k < tb.Columns.Count; k++)
                {
                    xlws.Cells[nps, k] = tb.Columns[k].HeaderText;
                }

                k--; //n gets increased by 1 after for loop

                //merged heading above the column headers
                xlrg = xlws.Range[xlws.Cells[nps - 1, 1], xlws.Cells[nps - 1, k]];
                xlrg.Merge();
                xlws.Cells[nps - 1, 1] = "उद्योगको विवरण";

                //COLUMN HEADINGS OF OWNER 
                otb = DisplayDataOwner("");
                for (n = 1; n < otb.Columns.Count; n++)
                {
                    xlws.Cells[nps, k + n] = otb.Columns[n].Caption;
                }
                n--; //n gets increased by 1 after for loop

                //merged heading above the column headers
                xlrg = xlws.Range[xlws.Cells[nps - 1, k + 1], xlws.Cells[nps - 1, k + n]];
                xlrg.Merge();
                xlws.Cells[nps - 1, k + 1] = "साझेदार / प्रोपराइटर विवरण";

                //making the header cells bold, center and backcolor
                xlrg = xlws.Range[xlws.Cells[nps - 1, 1], xlws.Cells[nps, k + n]];
                xlrg.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xlrg.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                xlrg.Font.Bold = true;
                xlrg.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);

                nps++; //new position for row is just below the header cells

                mytitle = "मिति " + global.convertNumToUnicode(conDate).ToString() + " मा दर्ता भएका उद्योगको विवरण";
                xlrg = xlws.Range[xlws.Cells[nps, 1], xlws.Cells[nps, 10]];
                xlrg.Merge();
                xlws.Cells[nps, 1] = mytitle;
                xlrg.Cells.Font.Size = "13";
                xlrg.Cells.Font.Bold = true;
                xlrg.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.AliceBlue);

                nps++;

                FillWorkSheetIndSummary(xlws, nps); //darta

                //NABIKARAN
                nps = nps + dataGridView1.Rows.Count+1;

                mytitle = "मिति " + global.convertNumToUnicode(conDate).ToString() + " मा नविकरण भएका उद्योगको विवरण";
                xlrg = xlws.Range[xlws.Cells[nps, 1], xlws.Cells[nps, 10]];
                xlrg.Merge();
                xlws.Cells[nps, 1] = mytitle;
                xlrg.Cells.Font.Size = "13";
                xlrg.Cells.Font.Bold = true;
                xlrg.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.AliceBlue);

                nps++;

                PrepareDataGridForExportIndSummary("nabikaran");
                FillWorkSheetIndSummary(xlws, nps);


                //SAMSHODHAN
                nps = nps + dataGridView1.Rows.Count+1;

                mytitle = "मिति " + global.convertNumToUnicode(conDate).ToString() + " मा संशोधन भएका उद्योगको विवरण";
                xlrg = xlws.Range[xlws.Cells[nps, 1], xlws.Cells[nps, 10]];
                xlrg.Merge();
                xlws.Cells[nps, 1] = mytitle;
                xlrg.Cells.Font.Size = "13";
                xlrg.Cells.Font.Bold = true;
                xlrg.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.AliceBlue);

                nps++;

                PrepareDataGridForExportIndSummary("samshodhan");
                FillWorkSheetIndSummary(xlws, nps);


                //LAGAT KATTA
                nps = nps + dataGridView1.Rows.Count+1;

                mytitle = "मिति " + global.convertNumToUnicode(conDate).ToString() + " मा लगत कट्टा भएका उद्योगको विवरण";
                xlrg = xlws.Range[xlws.Cells[nps, 1], xlws.Cells[nps, 10]];
                xlrg.Merge();
                xlws.Cells[nps, 1] = mytitle;
                xlrg.Cells.Font.Size = "13";
                xlrg.Cells.Font.Bold = true;
                xlrg.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.AliceBlue);

                nps++;

                PrepareDataGridForExportIndSummary("lagatkatta");
                FillWorkSheetIndSummary(xlws, nps);


                m = nps + dataGridView1.Rows.Count;

                //FORMATTING THE EXCEL FILE

                //border
                xlrg = xlws.Range[xlws.Cells[fps - 1, 1], xlws.Cells[m, tb.Columns.Count + otb.Columns.Count - 2]];
                xlrg.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                xlrg.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;


                //autofit
                xlws.Cells.Font.Name = "Kalimati";
                xlws.Columns.AutoFit();
                xlws.Rows.AutoFit();

                xlrg = xlws.Range[xlws.Cells[1, 1], xlws.Cells[hps - 1, 1]];
                xlrg.Cells.Font.Name = "Noto Sans";

                xlrg = xlws.Range[xlws.Cells[1, 11], xlws.Cells[3, 11]];
                xlrg.Cells.Font.Name = "Noto Sans";

                //SAVING FILE
                //string filePath;
                //filePath = "D:\\csido_report.xlsx";

                //if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                //{
                //    filePath = folderBrowserDialog1.SelectedPath;
                //}
                //xlwb.SaveAs(filePath);

                //SAVING FILE IN USER DEFINED SPACE

                string filePath = "";

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.InitialDirectory = @Environment.SpecialFolder.MyDocuments.ToString();
                saveFileDialog1.Title = "Save text Files";
                saveFileDialog1.CheckFileExists = false;
                //saveFileDialog1.CheckPathExists = false;    
                saveFileDialog1.DefaultExt = ".xlsx";
                saveFileDialog1.FileName = myfilename;
                saveFileDialog1.Filter = "Excel Workbook (*.xlsx)|*.xlsx|Excel 97-2003 Workbook (*.xls)|*.xls|CSV (*.csv)|*.csv|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveFileDialog1.FileName;
                    xlwb.SaveAs(filePath);
                    saveFileDialog1.Dispose();
                    xlwb.Close(misValue, filePath, misValue);
                    xlwb = null;
                    xlApp.Quit();
                    xlApp = null;

                    DialogResult dlrg = MessageBox.Show("प्रतिवेदन सफलतापुर्वक MS Excel मा सेभ गरियो । \n\n के तपाईँ प्रतिवेदन अहिले हेर्न चाहनुहुन्छ?", "प्रतिवेदन", MessageBoxButtons.YesNo, MessageBoxIcon.None);

                    if (dlrg == DialogResult.Yes)
                    {
                        FileInfo fi = new FileInfo(filePath);
                        if (fi.Exists)
                        {
                            System.Diagnostics.Process.Start(@filePath); //open excel file
                        }
                        else
                        {
                            MessageBox.Show("प्रतिवेदन फाइल खोल्न सकिएन ! कृपया सम्बन्धित फोल्डरमा गई आफै खोल्नुहोला ।", "प्रतिवेदन", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

                sqlcon.con.Close();

                //// Cleanup
                GC.Collect();
                GC.WaitForPendingFinalizers();


                // xlApp.Workbooks.Open(filePath);
                //xlApp.Visible = true;
                //xl

                ////Marshal.FinalReleaseComObject(xlRng);
                //Marshal.FinalReleaseComObject(xlws);

                //if (xlwb!=null)
                //{
                //xlwb.Close(misValue,template,misValue);
                //Marshal.FinalReleaseComObject(xlwb);
                //}

                //xlApp.Workbooks.Close();
                //xlApp.Quit();
                //Marshal.FinalReleaseComObject(xlApp);
            }
        }


        private void FillWorkSheetIndSummary(Microsoft.Office.Interop.Excel.Worksheet xlws,int RowPosition)
        {
            int j = 0, k = 0, m=0, n = 0;
            int nps = RowPosition;

            DataGridView tb = dataGridView1;
            DataTable otb = new DataTable();

            Microsoft.Office.Interop.Excel.Range xlrg;
            for (j = 0; j < tb.Rows.Count; j++)
            {
                otb = DisplayDataOwner(tb.Rows[j].Cells[0].Value.ToString());

                for (k = 1; k < tb.Columns.Count; k++)
                {
                    //xlws.Cells[j + 7, k + 1] = tb.Rows[j].ItemArray[k].ToString();
                    xlws.Cells[nps + j, k] = tb.Rows[j].Cells[k].Value.ToString();

                    if (otb.Rows.Count > 1)
                    {
                        xlrg = xlws.Range[xlws.Cells[nps + j, k], xlws.Cells[nps + j + otb.Rows.Count - 1, k]];
                        xlrg.Merge();
                    }
                }

                for (m = 0; m < otb.Rows.Count; m++)
                {
                    for (n = 1; n < otb.Columns.Count; n++)
                    {
                        xlws.Cells[nps + m + j, n + tb.Columns.Count - 1] = otb.Rows[m].ItemArray[n].ToString();
                        //MessageBox.Show(otb.Rows[m].ItemArray[n].ToString());
                    }
                }
                if (otb.Rows.Count > 1)
                {
                    nps += otb.Rows.Count - 1;
                }
            }
        }

        private void buttonExcelRenewCrossed_Click(object sender, EventArgs e)
        {
            PrepareDataGridForExportRenewCrossed();
            ExportDetailtoExcelRenewCrossed();
        }

        private void PrepareDataGridForExportRenewCrossed()
        {
            // CODE FOR THE EXCEL EXPORT
            string SqStmt = "SELECT ir.industryid, GetNumberToUnicode(ROW_NUMBER() OVER (ORDER BY industryid)) AS 'क्र.स.', ir.industrynepname AS 'उद्योगको नाम', GetNumberToUnicode(ir.industryregno) AS 'दर्ता नं', replace(GetNumberToUnicode(ir.regdat),'/','।') AS 'दर्ता मिति', " +
                " replace(GetNumberToUnicode(ir.renewdate),'/','।') as 'नविकरण गर्नुपर्ने मिति', " +
            "concat(ir.tole, ', ', replace(replace(replace(replace(setup_vdc.vdcunicodename, 'पालिका', 'पा.'), 'नगर', 'न.'), 'गाउँ', 'गा.'), 'महा', 'म.'), '-', GetNumberToUnicode(ir.industryward), ', ', setup_district.distunicodename) as 'उद्योग रहेको स्थान', " +
            " ir.branch AS 'शाखा', ssc1.subcategory_unicodename AS 'स्तर', ssc2.subcategory_unicodename AS 'कानूनी रुप', ssc3.subcategory_unicodename AS 'वर्ग', ir.karobar AS 'उद्देश्य', GetNumberToUnicode(ir.statcapital) AS 'स्थिर पुँजी', GetNumberToUnicode(ir.varcapital) AS 'चालु पुँजी' " +
            "FROM industryreg as ir " +
            "INNER JOIN setup_district ON ir.industrydist = setup_district.distcode " +
            "INNER JOIN setup_vdc ON ir.industryvdc = setup_vdc.VDC_SID " +
            "INNER JOIN setup_subcategory AS ssc1 ON ir.industryscale = ssc1.subcategory_id " +
            "INNER JOIN setup_subcategory AS ssc2 ON ir.industrytype = ssc2.subcategory_id " +
            "INNER JOIN setup_subcategory AS ssc3 ON ir.industrycat = ssc3.subcategory_id ";

            
            /*
            //getting CONDITION USING IN FROM DatagridView1's first columns
            string SqCond = " WHERE ir.regdat<getnepalidate(DATE_ADD(CURRENT_DATE,INTERVAL -5 YEAR)) " +
                "AND ir.industrytype<>93 AND ir.industryid NOT in(SELECT industryid FROM industryrenew)";

            String FullSqStmt = SqStmt + " " + SqCond;

            SqStmt = "SELECT ir.industryid, GetNumberToUnicode(ROW_NUMBER() OVER (ORDER BY industryid)) AS 'क्र.स.', ir.industrynepname AS 'उद्योगको नाम', GetNumberToUnicode(ir.industryregno) AS 'दर्ता नं', replace(GetNumberToUnicode(ir.regdat),'/','।') AS 'दर्ता मिति'," +
                   " replace(GetNumberToUnicode(ir.renewdate),'/','।') as 'नविकरण गर्नुपर्ने मिति', " +
            "concat(ir.tole, ', ', replace(replace(replace(replace(setup_vdc.vdcunicodename, 'पालिका', 'पा.'), 'नगर', 'न.'), 'गाउँ', 'गा.'), 'महा', 'म.'), '-', GetNumberToUnicode(ir.industryward), ', ', setup_district.distunicodename) as 'उद्योग रहेको स्थान', " +
            " ir.branch AS 'शाखा', ssc1.subcategory_unicodename AS 'स्तर', ssc2.subcategory_unicodename AS 'कानूनी रुप', ssc3.subcategory_unicodename AS 'वर्ग', ir.karobar AS 'उद्देश्य', GetNumberToUnicode(ir.statcapital) AS 'स्थिर पुँजी', GetNumberToUnicode(ir.varcapital) AS 'चालु पुँजी' " +
            "FROM industryreg as ir " +
            "INNER JOIN industryrenew as iw ON ir.industryid=iw.industryid " +
            "INNER JOIN setup_district ON ir.industrydist = setup_district.distcode " +
            "INNER JOIN setup_vdc ON ir.industryvdc = setup_vdc.VDC_SID " +
            "INNER JOIN setup_subcategory AS ssc1 ON ir.industryscale = ssc1.subcategory_id " +
            "INNER JOIN setup_subcategory AS ssc2 ON ir.industrytype = ssc2.subcategory_id " +
            "INNER JOIN setup_subcategory AS ssc3 ON ir.industrycat = ssc3.subcategory_id ";

            SqCond = " WHERE(iw.industryid, iw.decisiondate) IN(SELECT industryid, MAX(decisiondate) FROM industryrenew GROUP BY industryid) AND iw.decisiondate<getnepalidate(DATE_ADD(CURRENT_DATE, INTERVAL -2 YEAR))";

            //get count of industries whose latest renewal date before 2 years (i.e. renew date is crossed)
            FullSqStmt += "UNION " + SqStmt + " " + SqCond;

            */


            //SIMPLER CODE
            string FullSqStmt = SqStmt + "WHERE ir.renewdate < getnepalidate(CURRENT_DATE) ";

            if (sqlcon.con.State == ConnectionState.Closed)
                sqlcon.con.Open();

            MySqlDataAdapter sqDA = new MySqlDataAdapter();
            MySqlCommand sqCmd = new MySqlCommand();

            sqCmd.CommandText = FullSqStmt;

            sqCmd.Connection = sqlcon.con;
            sqDA.SelectCommand = sqCmd;

            DataTable tb = new DataTable();
            sqDA.Fill(tb);
            //MessageBox.Show(tb.Rows.Count.ToString());
            dataGridView1.DataSource = tb;
            dataGridView1.Columns[0].Visible = false;
            //dataGridView1.AutoResizeColumns();
            //dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            sqlcon.con.Close();
        }

        private void ExportDetailtoExcelRenewCrossed()
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("उद्योगको विवरण भेटाउन सकिएन ।", "उद्योग विवरण Excel मा पठाउन", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // creating new WorkBook within Excel application  
            Excel.Application xlApp;
            Excel.Workbook xlwb;
            Excel.Worksheet xlws;
            Excel.Range xlrg;
            xlApp = new Excel.Application();
            //string template = Application.StartupPath + "\\csio_industry_detail_report_template_new4.xlsx";
            //xlwb = xlApp.Workbooks.Add(template);
            xlwb = xlApp.Workbooks.Add();
            xlws = xlwb.Worksheets[1];
            object misValue = System.Reflection.Missing.Value;


            try
            {
                //OFFICE HEADINGS
                string sqsm = "SELECT govtname,ministryname,departmentname,officeunicodename,office_address,provincename, GetNumberToUnicode(phone),GetNumberToUnicode(fax),email FROM setup_office where isCur=1";

                string[] offhead = global.getSingleRowFromTable(sqsm);

                string myHead = offhead[0];
                myHead += "\n " + offhead[1];
                myHead += "\n " + offhead[2];
                myHead += "\n " + offhead[3] + ", " + offhead[4];
                myHead += "\n " + offhead[5];

                //SARKAR HEADING
                int hps = 1; //header position
                xlrg = xlws.Range[xlws.Cells[hps, 1], xlws.Cells[hps, 10]];
                xlrg.Merge();
                xlrg.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xlrg.Cells.Font.Size = "12";
                xlrg.Cells.Font.Bold = true;
                xlws.Cells[hps, 1] = offhead[0];

                //MANTRALAY HEADING
                hps++;
                xlrg = xlws.Range[xlws.Cells[hps, 1], xlws.Cells[hps, 10]];
                xlrg.Merge();
                xlrg.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xlrg.Font.Size = "13";
                xlrg.Font.Bold = true;
                xlws.Cells[hps, 1] = offhead[1];

                //Nirdeshanalay HEADING
                if (Convert.ToInt32(global.useroffice_category) > 1)
                {
                    hps++;
                    xlrg = xlws.Range[xlws.Cells[hps, 1], xlws.Cells[hps, 10]];
                    xlrg.Merge();
                    xlrg.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlrg.Font.Size = "13";
                    xlrg.Font.Bold = true;
                    xlws.Cells[hps, 1] = offhead[2];
                }

                //Karyalay HEADING
                if (Convert.ToInt32(global.useroffice_category) > 2)
                {
                    hps++;
                    xlrg = xlws.Range[xlws.Cells[hps, 1], xlws.Cells[hps, 10]];
                    xlrg.Merge();
                    xlrg.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlrg.Font.Size = "14";
                    xlrg.Font.Bold = true;
                    xlws.Cells[hps, 1] = offhead[3] + ", " + offhead[4];
                }

                hps++;
                xlrg = xlws.Range[xlws.Cells[hps, 1], xlws.Cells[hps, 10]];
                xlrg.Merge();
                xlrg.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xlrg.Font.Size = "12";
                xlrg.Font.Bold = true;
                xlws.Cells[hps, 1] = offhead[5];

                //ADDRESS HEADINGS AT THE RIGHT SIDE
                //Address - Phone Number
                xlrg = xlws.Range[xlws.Cells[1, 11], xlws.Cells[1, 13]];
                xlrg.Merge();
                xlws.Cells[1, 11] = "फोन नं:" + offhead[6];

                //Address - FAX
                xlrg = xlws.Range[xlws.Cells[2, 11], xlws.Cells[2, 13]];
                xlrg.Merge();
                xlws.Cells[2, 11] = "फ्याक्स नं: " + offhead[7];

                //Address - Email
                xlrg = xlws.Range[xlws.Cells[3, 11], xlws.Cells[3, 13]];
                xlrg.Merge();
                xlws.Cells[3, 11] = "इमेल: " + offhead[8];

                hps++;

                string logogov = Application.StartupPath + "\\logogov.png";
                xlws.Shapes.AddPicture(@logogov, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 0, 0, 85, 58);

                string mytitle="", myfilename="";

                //FILLING DATAGRID -- with columns of industry but none of owner

                mytitle = "नविकरण मिति गुज्रिएका उद्योगहरूको विवरण";
                myfilename = "not_renewed_industry";

                xlrg = xlws.Range[xlws.Cells[hps, 1], xlws.Cells[hps, 10]];
                xlrg.Merge();
                xlws.Cells[hps, 1] = mytitle;
                xlrg.Cells.Font.Size = "13";

               

                //con.Open();
                if (sqlcon.con.State == ConnectionState.Closed)
                    sqlcon.con.Open();

                //datagrid view is filled with required data

                DataGridView tb = dataGridView1;
                DataTable otb = new DataTable();

                //INDUSTRY REGISTRtTION -- FIRST PART OF THE REPORT
                // FILLING THE DATA
                //as per the template - the first data is in 7th row but col is always 1
                //so added 7 in j (j+7, k+1)

                int fps, nps; //first position of data (starting from header) and new position for each row
                fps = nps = hps + 2; //this is for the new position of row (in excel)
                int j = 0, k = 0, m = 0, n = 0;

                //COLUMN HEADINGS of INDUSTRY
                for (k = 1; k < tb.Columns.Count; k++)
                {
                    xlws.Cells[nps, k] = tb.Columns[k].HeaderText;
                }

                k--; //n gets increased by 1 after for loop

                //merged heading above the column headers
                xlrg = xlws.Range[xlws.Cells[nps - 1, 1], xlws.Cells[nps - 1, k]];
                xlrg.Merge();
                xlws.Cells[nps - 1, 1] = "उद्योगको विवरण";

                //COLUMN HEADINGS OF OWNER 
                otb = DisplayDataOwner("");
                for (n = 1; n < otb.Columns.Count; n++)
                {
                    xlws.Cells[nps, k + n] = otb.Columns[n].Caption;
                }
                n--; //n gets increased by 1 after for loop

                //merged heading above the column headers
                xlrg = xlws.Range[xlws.Cells[nps - 1, k + 1], xlws.Cells[nps - 1, k + n]];
                xlrg.Merge();
                xlws.Cells[nps - 1, k + 1] = "साझेदार / प्रोपराइटर विवरण";

                //making the header cells bold, center and backcolor
                xlrg = xlws.Range[xlws.Cells[nps - 1, 1], xlws.Cells[nps, k + n]];
                xlrg.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xlrg.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                xlrg.Font.Bold = true;
                xlrg.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);

                nps++; //new position for row is just below the header cells
                for (j = 0; j < tb.Rows.Count; j++)
                {
                    otb = DisplayDataOwner(tb.Rows[j].Cells[0].Value.ToString());

                    for (k = 1; k < tb.Columns.Count; k++)
                    {
                        //xlws.Cells[j + 7, k + 1] = tb.Rows[j].ItemArray[k].ToString();
                        xlws.Cells[nps + j, k] = tb.Rows[j].Cells[k].Value.ToString();

                        if (otb.Rows.Count > 1)
                        {
                            xlrg = xlws.Range[xlws.Cells[nps + j, k], xlws.Cells[nps + j + otb.Rows.Count - 1, k]];
                            xlrg.Merge();
                        }
                    }

                    for (m = 0; m < otb.Rows.Count; m++)
                    {
                        for (n = 1; n < otb.Columns.Count; n++)
                        {
                            xlws.Cells[nps + m + j, n + tb.Columns.Count - 1] = otb.Rows[m].ItemArray[n].ToString();
                            //MessageBox.Show(otb.Rows[m].ItemArray[n].ToString());
                        }
                    }
                    if (otb.Rows.Count > 1)
                    {
                        nps += otb.Rows.Count - 1;
                    }
                }

                //FORMATTING THE EXCEL FILE

                //border
                xlrg = xlws.Range[xlws.Cells[fps - 1, 1], xlws.Cells[nps + m + j - 2, tb.Columns.Count + otb.Columns.Count - 2]];
                xlrg.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                xlrg.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                //autofit
                xlws.Cells.Font.Name = "Kalimati";
                xlws.Columns.AutoFit();
                xlws.Rows.AutoFit();

                xlrg = xlws.Range[xlws.Cells[1, 1], xlws.Cells[hps - 1, 1]];
                xlrg.Cells.Font.Name = "Noto Sans";

                xlrg = xlws.Range[xlws.Cells[1, 11], xlws.Cells[3, 11]];
                xlrg.Cells.Font.Name = "Noto Sans";

                //SAVING FILE
                //string filePath;
                //filePath = "D:\\csido_report.xlsx";

                //if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                //{
                //    filePath = folderBrowserDialog1.SelectedPath;
                //}
                //xlwb.SaveAs(filePath);



                //SAVING FILE IN USER DEFINED SPACE

                string filePath = "";

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.InitialDirectory = @Environment.SpecialFolder.MyDocuments.ToString();
                saveFileDialog1.Title = "Save text Files";
                saveFileDialog1.CheckFileExists = false;
                //saveFileDialog1.CheckPathExists = false;    
                saveFileDialog1.DefaultExt = ".xlsx";
                saveFileDialog1.FileName = myfilename;
                saveFileDialog1.Filter = "Excel Workbook (*.xlsx)|*.xlsx|Excel 97-2003 Workbook (*.xls)|*.xls|CSV (*.csv)|*.csv|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveFileDialog1.FileName;
                    xlwb.SaveAs(filePath);
                    saveFileDialog1.Dispose();
                    xlwb.Close(misValue, filePath, misValue);
                    xlwb = null;
                    xlApp.Quit();
                    xlApp = null;

                    DialogResult dlrg = MessageBox.Show("प्रतिवेदन सफलतापुर्वक MS Excel मा सेभ गरियो । \n\n के तपाईँ प्रतिवेदन अहिले हेर्न चाहनुहुन्छ?", "प्रतिवेदन", MessageBoxButtons.YesNo, MessageBoxIcon.None);

                    if (dlrg == DialogResult.Yes)
                    {
                        FileInfo fi = new FileInfo(filePath);
                        if (fi.Exists)
                        {
                            System.Diagnostics.Process.Start(@filePath); //open excel file
                        }
                        else
                        {
                            MessageBox.Show("प्रतिवेदन फाइल खोल्न सकिएन ! कृपया सम्बन्धित फोल्डरमा गई आफै खोल्नुहोला ।", "प्रतिवेदन", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

                sqlcon.con.Close();

                //// Cleanup
                GC.Collect();
                GC.WaitForPendingFinalizers();


                // xlApp.Workbooks.Open(filePath);
                //xlApp.Visible = true;
                //xl

                ////Marshal.FinalReleaseComObject(xlRng);
                //Marshal.FinalReleaseComObject(xlws);

                //if (xlwb!=null)
                //{
                //xlwb.Close(misValue,template,misValue);
                //Marshal.FinalReleaseComObject(xlwb);
                //}

                //xlApp.Workbooks.Close();
                //xlApp.Quit();
                //Marshal.FinalReleaseComObject(xlApp);

            }
        }


        public DataTable DisplayDataOwner(string indId)
        {
            DataTable tb = new DataTable();

            if (sqlcon.con.State == ConnectionState.Closed)
                sqlcon.con.Open();

            string mySqst = "SELECT owner_industry.ownerid, concat(owner_industry.ownerfname, ' ', owner_industry.ownerlname) AS 'नाम थर', concat(replace(replace(replace(replace(setup_vdc.vdcunicodename, 'पालिका', 'पा.'), 'नगर', 'न.'), 'गाउँ', 'गा.'), 'महा', 'म.'), '-', GetNumberToUnicode(owner_industry.ownervdcward), ', ', setup_district_1.distunicodename) as 'ठेगाना', setup_subcategory.subcategory_unicodename AS 'लिङ्ग',  GetNumberToUnicode(owner_industry.contact) As 'सम्पर्क नं', email As 'इमेल',  GetNumberToUnicode(owner_industry.citznum) AS 'नागरिकता.प्र.नं.',  concat(setup_citizenissueoff.citizen_officeunicodename, ' - ', setup_district.distunicodename) AS 'जारी गर्ने कार्यालय-जिल्ला',  GetNumberToUnicode(owner_industry.citzdate) AS 'जारी मिति', " +
                "concat(owner_industry.ownergname, ' (',setup_subcategory_1.subcategory_unicodename,')') AS 'बाबु/पतिको नाम'," +
                "concat(owner_industry.ownerfgname, ' (', setup_subcategory_2.subcategory_unicodename,')') AS 'बाजे/ससुराको नाम' ," +
                "GetNumberToUnicode(inv_share) as 'लगानी प्रतिशत'" +
                "FROM owner_industry INNER JOIN setup_district ON owner_industry.cddist = setup_district.distcode " +
                "INNER JOIN setup_citizenissueoff ON owner_industry.ccio = setup_citizenissueoff.citizen_officeid " +
                "INNER JOIN setup_vdc ON owner_industry.ownervdccode = setup_vdc.VDC_SID " +
                "INNER JOIN setup_district AS setup_district_1 ON owner_industry.ownerdistcode = setup_district_1.distcode " +
                "INNER JOIN setup_subcategory ON owner_industry.gender = setup_subcategory.subcategory_id " +
                "INNER JOIN setup_subcategory AS setup_subcategory_1 ON owner_industry.ownergrel = setup_subcategory_1.subcategory_id " +
                "INNER JOIN setup_subcategory AS setup_subcategory_2 ON owner_industry.ownerfgrel = setup_subcategory_2.subcategory_id " +
                "WHERE(owner_industry.industryid = @darta)";

            MySqlDataAdapter qry = new MySqlDataAdapter(mySqst, sqlcon.con);

            qry.SelectCommand.Parameters.AddWithValue("@darta", global.convertUnicodeToNum(indId));
            qry.Fill(tb);

            sqlcon.con.Close();

            return tb;
        }

        private void buttonExcelAllStat_Click(object sender, EventArgs e)
        {
            // Prepare a dummy string, thos would appear in the dialog
            string dummyFileName = "chart";

            SaveFileDialog sf = new SaveFileDialog();
            // Feed the dummy name to the save dialog
            sf.FileName = dummyFileName;
            sf.Title= "Select Folder to Export Chart Images";
            sf.Filter = "Directory | directory";

            if (sf.ShowDialog() == DialogResult.OK)
            {
                // Now here's our save folder
                string savePath = Path.GetDirectoryName(sf.FileName);

                //CREATING RANDOM Number from date -- to append in filename
                string cdt = DateTime.Now.ToString();
                string rnd = DateTime.Parse(cdt).Year.ToString();
                rnd += DateTime.Parse(cdt).Month.ToString();
                rnd += DateTime.Parse(cdt).Day.ToString();
                rnd += DateTime.Parse(cdt).Hour.ToString();
                rnd += DateTime.Parse(cdt).Minute.ToString();
                rnd += DateTime.Parse(cdt).Second.ToString();

                //creating a directory with the filname given by user (e.g. chart)
                savePath += "\\" + Path.GetFileName(sf.FileName); //gets file name and appends to path
                System.IO.Directory.CreateDirectory(savePath); //creates the folder with file name

                //created a function to double the size of the chart and save it

                SaveBiggerChart(barIndustryReg,savePath , "\\industry_registration_bar" + rnd + ".png");
                SaveBiggerChart(barRajaswa,savePath , "\\tax(rajaswa)_bar" + rnd + ".png");
                SaveBiggerChart(pieOwnerSex,savePath , "\\Industry_owners_pie" + rnd + ".png");
                SaveBiggerChart(pieWorkerSex,savePath, "\\Industry_workers_pie" + rnd + ".png");

                SaveBiggerChart(pieIndBarga,savePath , "\\Industry_by_type(category)_pie" + rnd + ".png");
                SaveBiggerChart(pieIndStar,savePath , "\\Industry_by_level(size)_pie" + rnd + ".png");
                SaveBiggerChart(pieIndKanuni,savePath , "\\Industry_by_legal_form_pie" + rnd + ".png");

                MessageBox.Show("सबै चार्टहरूलाई सफलतापुर्वक इमेज फाइलका रूपमा सेभ गरियो ।", "चार्ट सेभ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //barIndustryReg.SaveImage("C:\\mycode\\mychart.png", ChartImageFormat.Png);

        }

        private void SaveBiggerChart(Chart ch, string savePath, string filename,int times = 2)
        {
            int fsInc = times * 3; //font size to be increased;

            System.IO.MemoryStream myStream = new System.IO.MemoryStream();
            Chart chart2 = new Chart();
            ch.Serializer.Save(myStream);
            chart2.Serializer.Load(myStream);
            chart2.Visible = false;
            chart2.Width = ch.Width * times;
            chart2.Height = ch.Height * times;
            

            //title
            if(chart2.Titles.Count>0)
            {
                chart2.Titles[0].Visible = true;
                chart2.Titles[0].Font = new Font(chart2.Titles[0].Font.Name, chart2.Titles[0].Font.Size + fsInc,FontStyle.Bold);
                chart2.Titles[0].BorderWidth = 1;
                chart2.Titles[0].DockedToChartArea = chart2.ChartAreas[0].Name;
                chart2.Titles[0].Position.Y = 5;
                chart2.Titles[0].Position.X = 50;
                chart2.Titles[0].Alignment = ContentAlignment.MiddleCenter;
            }

            //LEGEND
            if (chart2.Legends.Count > 0)
            {
                chart2.Legends[0].Font = new Font(chart2.Legends[0].Font.Name, chart2.Legends[0].Font.Size + fsInc);
                chart2.Legends[0].Title = ""; //title is already added

                //DOCKING OF LEGENDS
                if (chart2.Series.Count > 0)
                {
                    if (chart2.Series[0].ChartType == SeriesChartType.Bar)
                        chart2.Legends[0].Docking = Docking.Top;
                    else if (chart2.Series[0].ChartType == SeriesChartType.Pie || chart2.Series[0].ChartType == SeriesChartType.Doughnut)
                        chart2.Legends[0].Docking = Docking.Right;

                    //chart2.Legends[0].DockedToChartArea = chart2.ChartAreas[0].Name;

                } 
            }

            //SERIES
            if (chart2.Series.Count > 0)
            {
                chart2.Series[0].Font = new Font(chart2.Series[0].Font.Name, chart2.Series[0].Font.Size + fsInc);
                chart2.Series[0].SetCustomProperty("PixelPointWidth", (Convert.ToInt32(chart2.Series[0].GetCustomProperty("PixelPointWidth")) +fsInc).ToString());
            }

            //CHART AREA FONT
            if (chart2.ChartAreas.Count > 0)
            {

                chart2.ChartAreas[0].AxisX.LabelStyle.Font = new Font(chart2.ChartAreas[0].AxisX.LabelStyle.Font.Name, chart2.ChartAreas[0].AxisX.LabelStyle.Font.Size +fsInc);
                chart2.ChartAreas[0].AxisY.LabelStyle.Font = new Font(chart2.ChartAreas[0].AxisY.LabelStyle.Font.Name, chart2.ChartAreas[0].AxisY.LabelStyle.Font.Size +fsInc);

                //title is added, so moving chartarea a little lower
                //chart2.ChartAreas[0].Position.Y = 10;
                //chart2.ChartAreas[0].Position.Height = 90;
            }


            chart2.SaveImage(savePath + filename, ChartImageFormat.Png);
            chart2.Dispose();
        }
    }
}
