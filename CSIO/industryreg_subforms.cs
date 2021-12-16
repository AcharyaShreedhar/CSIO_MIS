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
    public partial class industryreg_subforms : Form
    {
        public industryreg_subforms()
        {
            InitializeComponent();
        }
        public string transid;
        public industryreg myparent { get; set; } //this is required to call a method of parent from this child form
        public capitalupdateindustry myparent2 { get; set; } //to call Capital Update for Puji Briddhi;
        public changeelectriccapacity myparent3 { get; set; } //to call Update of Machinery and indhan pani;
        public string myparentName = "industryreg"; //for checking which form is calling it

        public string myIndId = "";
        public string disptype = "";

        //int myId;
        //string mySaveMode; //save mode 'add' or 'edit'

        Color enaColo = Color.FromArgb(192, 255, 192);
        Color disColor = Color.LightGray;

        public void FillDataGrid(DataGridView dgv, DataTable tb)
        {
            //ADDING COLUMNS AND ROWS MANUALLY
            int ii = 0, jj;

            for (ii = 0; ii < tb.Rows.Count; ii++)
            {
                dgv.Rows.Add();
                for (jj = 0; jj < tb.Columns.Count; jj++)
                {
                    dgv.Rows[ii].Cells[jj].Value = tb.Rows[ii].ItemArray[jj].ToString();
                }
            }
        }

        public void FormatDataGridProduct()
        {
            DataGridView dgv = new DataGridView();
            string sqst = "";
            MySqlDataAdapter qry;
            DataTable tb = new DataTable();

            //this.Text = "उद्योगबाट उत्पादन हुने वस्तु वा सेवाको विवरण"; 
            dgv = dgvProduct;
            sqst = "SELECT id, industryid, material as 'वस्तु वा सेवाको विवरण',  GetNumberToUnicode(capacity) as 'वार्षिक क्षमता', unit as 'एकाइ', GetNumberToUnicode(unit_price) As 'बिक्री मूल्य दर (रू)',  GetNumberToUnicode(capacity * unit_price) as 'कुल वार्षिक बिक्री मुल्य (रू)' From  industryreg_product_service where industryid='" + myIndId + "'";
            labelTotProduct.Text = global.getSingleDataFromTable("SELECT  GetNumberToUnicode(sum(capacity * unit_price)) FROM industryreg_product_service where industryid='" + myIndId + "'");

            if (sqlcon.con.State == ConnectionState.Closed)
                sqlcon.con.Open();

            qry = new MySqlDataAdapter(sqst, sqlcon.con);

            qry.Fill(tb);

            dgv.Rows.Clear();
            dgv.Columns.Clear();

            dgv.Columns.Add("col1", tb.Columns[0].Caption); //id
            dgv.Columns.Add("col2", tb.Columns[1].Caption); //industryid
            dgv.Columns.Add("col3", tb.Columns[2].Caption); //material
            dgv.Columns.Add("col4", tb.Columns[3].Caption); //capacity
            //unit is combobox
            DataGridViewComboBoxColumn dgcbUnit = new DataGridViewComboBoxColumn();
            //global.fillCombo(dgcbUnit, "select id,unitname FROM setup_units", "unitname", "unitname", "-");
            //combobox is filled with array rather than dispmember and valuemember
            dgcbUnit.Items.AddRange(global.getSingleFieldArrayFromTable("select unitname FROM setup_units"));
            //dgcbUnit.Items.AddRange("दर्जन", "प्याकेट", "थान", "मिटर", "से.मि.");
            dgv.Columns.Add(dgcbUnit);
            dgv.Columns[4].HeaderText = tb.Columns[4].Caption; //header for unit

            dgv.Columns.Add("col6", tb.Columns[5].Caption); //unit_price
            dgv.Columns.Add("col7", tb.Columns[6].Caption); //tot

            //filling data grid
            FillDataGrid(dgv, tb);

            dgv.Columns[0].Visible = false;
            dgv.Columns[1].Visible = false;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.ReadOnly = true;
            sqlcon.con.Close();
        }

        private void FormatDataGridInvestment()
        {
            DataGridView dgv = new DataGridView();
            string sqst = "";
            MySqlDataAdapter qry;
            DataTable tb = new DataTable();
            labelTitle.Text = "उद्योगको परियोजना लागत तथा लगानी श्रोत सम्बन्धी विवरण";
            
            string inv_type = "स्थिर पूँजी";

            dgv = dgvSthirPuji;
            sqst = "SELECT id, industryid, category as 'स्थिर पूँजीको विवरण', detail as 'थप विवरण', GetNumberToUnicode(amount) as 'रकम (रू.)' FROM industryreg_investment_sthir where industryid='" + myIndId + "'";

            labelTotSthir.Text = global.getSingleDataFromTable("SELECT  GetNumberToUnicode(sum(amount)) FROM industryreg_investment_sthir where industryid='" + myIndId + "'");

            //total self and loan - it is same for every row
            textSelfSthir.Text = global.getSingleDataFromTable("SELECT GetNumberToUnicode(self_tot) FROM industryreg_investment_sthir where industryid='" + myIndId + "' LIMIT 0,1");
           
            textLoanSthir.Text = global.getSingleDataFromTable("SELECT GetNumberToUnicode(loan_tot) FROM industryreg_investment_sthir where industryid='" + myIndId + "' LIMIT 0,1");

            if (string.IsNullOrEmpty(labelTotSthir.Text))
                labelTotSthir.Text = "0";

            if (string.IsNullOrEmpty(labelTotChalu.Text))
                labelTotChalu.Text = "0";

            if (sqlcon.con.State == ConnectionState.Closed)
                sqlcon.con.Open();

            qry = new MySqlDataAdapter(sqst, sqlcon.con);

            tb.Clear();
            dgv.Rows.Clear();
            dgv.Columns.Clear();

            qry.Fill(tb);


            dgv.Columns.Add("col1", tb.Columns[0].Caption); //id
            dgv.Columns.Add("col2", tb.Columns[1].Caption); //industryid

            //category is a combobox
            DataGridViewComboBoxColumn dgcbSthir = new DataGridViewComboBoxColumn();
            //global.fillCombo(dgcbSthir, "select id,capital_detail FROM setup_capitalamount WHERE capital_type='" + inv_type + "'", "capital_detail", "capital_detail", "-");
            dgcbSthir.Items.AddRange(global.getSingleFieldArrayFromTable("select capital_detail FROM setup_capitalamount WHERE capital_type='" + inv_type + "'"));
            dgv.Columns.Add(dgcbSthir);
            dgv.Columns[2].HeaderText = tb.Columns[2].Caption; //header for category

            dgv.Columns.Add("col4", tb.Columns[3].Caption); //detail
            dgv.Columns.Add("col4", tb.Columns[4].Caption); //amount

            //filling data grid
            FillDataGrid(dgv, tb);



            dgv.Columns[0].Visible = false;
            dgv.Columns[1].Visible = false;
            
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.ReadOnly = true;
            sqlcon.con.Close();

           inv_type = "चालू पूँजी";

            dgv = dgvChaluPuji;
            sqst = "SELECT id, industryid, category as 'चालू पूँजीको विवरण', detail as 'थप विवरण', GetNumberToUnicode(amount) as 'रकम (रू.)' FROM industryreg_investment_chalu where industryid='" + myIndId + "'";

            labelTotChalu.Text = global.getSingleDataFromTable("SELECT  GetNumberToUnicode(sum(amount)) FROM industryreg_investment_chalu where industryid='" + myIndId + "'");

            //total self and loan - it is same for every row
            textSelfChalu.Text = global.getSingleDataFromTable("SELECT GetNumberToUnicode(self_tot) FROM industryreg_investment_chalu where industryid='" + myIndId + "' LIMIT 0,1");

            textLoanChalu.Text = global.getSingleDataFromTable("SELECT GetNumberToUnicode(loan_tot) FROM industryreg_investment_chalu where industryid='" + myIndId + "' LIMIT 0,1");

            if (string.IsNullOrEmpty(labelTotChalu.Text))
                labelTotChalu.Text = "0";

            labelTotInvestment.Text = (Convert.ToDecimal(global.convertUnicodeToNum(labelTotChalu.Text)) + Convert.ToDecimal(global.convertUnicodeToNum(labelTotSthir.Text))).ToString();

            if (sqlcon.con.State == ConnectionState.Closed)
                sqlcon.con.Open();

            qry = new MySqlDataAdapter(sqst, sqlcon.con);

            //tb.Clear();
            tb.Rows.Clear();
            tb.Columns.Clear();

            dgv.Rows.Clear();
            dgv.Columns.Clear();

            qry.Fill(tb);

            dgv.Columns.Add("col1", tb.Columns[0].Caption); //id
            dgv.Columns.Add("col2", tb.Columns[1].Caption); //industryid

            //category is a combobox
            DataGridViewComboBoxColumn dgcbChalu = new DataGridViewComboBoxColumn();
            //global.fillCombo(dgcbChalu, "select id,capital_detail FROM setup_capitalamount WHERE capital_type='" + inv_type + "'", "capital_detail", "capital_detail", "-");
            dgcbChalu.Items.AddRange(global.getSingleFieldArrayFromTable("select capital_detail FROM setup_capitalamount WHERE capital_type='" + inv_type + "'"));

            dgv.Columns.Add(dgcbChalu);
            dgv.Columns[2].HeaderText = tb.Columns[2].Caption; //header for category

            dgv.Columns.Add("col4", tb.Columns[3].Caption); //detail
            dgv.Columns.Add("col4", tb.Columns[4].Caption); //amount

            //filling data grid
            FillDataGrid(dgv, tb);

            dgv.Columns[0].Visible = false;
            dgv.Columns[1].Visible = false;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.ReadOnly = true;
            sqlcon.con.Close();

            //dgv = dgvChaluPuji;
            //sqst = "SELECT id, industryid, category as 'चालू पुँजीको विवरण', detail as 'थप विवरण', GetNumberToUnicode(amount) as 'रकम (रू.)' FROM industryreg_investment where industryid='" + myIndId + "' and inv_type='चालू पूँजी'";

            //labelTotSthir.Text = global.getSingleDataFromTable("SELECT  GetNumberToUnicode(sum(amount)) FROM industryreg_investment where industryid='" + myIndId + "' and inv_type='चालू पूँजी'");

            //if (string.IsNullOrEmpty(labelTotSthir.Text))
            //    labelTotSthir.Text = "0";

            //if (string.IsNullOrEmpty(labelTotChalu.Text))
            //    labelTotChalu.Text = "0";
            //totInv = Convert.ToDecimal(labelTotSthir.Text) + Convert.ToDecimal(labelTotChalu.Text);
            //labelTotInvestment.Text = totInv.ToString();

        }

        private void FormatDataGridMachinery()
        {
            string sqst = "";
            MySqlDataAdapter qry;
            DataTable tb = new DataTable();
            DataGridView dgv = dgvMachinery;

            sqst = "SELECT id, industryid, mach_name as 'मेशिनको नाम',  detail as 'मेशिनको विवरण', GetNumberToUnicode(qty) as 'आवश्यक संख्या', GetNumberToUnicode(amount) As 'मूल्य (रू)' From  industryreg_machinery where industryid='" + myIndId + "'";
            labelTotRawMat.Text = global.getSingleDataFromTable("SELECT  GetNumberToUnicode(sum(amount)) FROM industryreg_machinery where industryid='" + myIndId + "'");

            if (sqlcon.con.State == ConnectionState.Closed)
                sqlcon.con.Open();

            qry = new MySqlDataAdapter(sqst, sqlcon.con);

            qry.Fill(tb);

            dgv.Rows.Clear();
            dgv.Columns.Clear();

            dgv.Columns.Add("col1", tb.Columns[0].Caption); //id
            dgv.Columns.Add("col2", tb.Columns[1].Caption); //industryid
            dgv.Columns.Add("col3", tb.Columns[2].Caption); //machine name
            dgv.Columns.Add("col4", tb.Columns[3].Caption); //machine detail
            dgv.Columns.Add("col5", tb.Columns[4].Caption); //qty
            dgv.Columns.Add("col6", tb.Columns[5].Caption); //amount

            //filling data grid
            FillDataGrid(dgv, tb);

            dgv.Columns[0].Visible = false;
            dgv.Columns[1].Visible = false;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.ReadOnly = true;
            sqlcon.con.Close();

        }

        private void FormatDataGridRawMaterial()
        {
            string sqst = "";
            MySqlDataAdapter qry;
            DataTable tb = new DataTable();
            DataGridView dgv = dgvRawMat;

            sqst = "SELECT id, industryid, rawmat_name as 'कच्चा पदार्थ, सहायक कच्चा पदार्थ, प्याकिङ सामग्रीको नाम',  unit as 'एकाइ', GetNumberToUnicode(unit_price) as 'दर',GetNumberToUnicode(qty) as 'वाषिक आवश्यक परिमाण', GetNumberToUnicode(unit_price*qty) As 'मूल्य रकम (रू)' From  industryreg_rawmaterial where industryid='" + myIndId + "'";
            labelTotRawMat.Text = global.getSingleDataFromTable("SELECT  GetNumberToUnicode(sum(unit_price)*sum(qty)) FROM industryreg_rawmaterial where industryid='" + myIndId + "'");

            if (sqlcon.con.State == ConnectionState.Closed)
                sqlcon.con.Open();

            qry = new MySqlDataAdapter(sqst, sqlcon.con);

            qry.Fill(tb);

            dgv.Rows.Clear();
            dgv.Columns.Clear();

            dgv.Columns.Add("col1", tb.Columns[0].Caption); //id
            dgv.Columns.Add("col2", tb.Columns[1].Caption); //industryid
            dgv.Columns.Add("col3", tb.Columns[2].Caption); //rawmat
            //unit is combobox
            DataGridViewComboBoxColumn dgcbUnit = new DataGridViewComboBoxColumn();
            //combobox is filled with array rather than dispmember and valuemember
            dgcbUnit.Items.AddRange(global.getSingleFieldArrayFromTable("select unitname FROM setup_units"));
            dgv.Columns.Add(dgcbUnit);
            dgv.Columns[3].HeaderText = tb.Columns[3].Caption; //header for unit

            dgv.Columns.Add("col5", tb.Columns[4].Caption); //unit_price
            dgv.Columns.Add("col6", tb.Columns[5].Caption); //qty
            dgv.Columns.Add("col7", tb.Columns[6].Caption); //amount

            //filling data grid
            FillDataGrid(dgv, tb);

            dgv.Columns[0].Visible = false;
            dgv.Columns[1].Visible = false;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.ReadOnly = true;
            sqlcon.con.Close();
        }

        private void FormatDataGridFuelWater()
        {
            string sqst = "";
            MySqlDataAdapter qry;
            DataTable tb = new DataTable();
            DataGridView dgv = dgvFuelWater;

            sqst = "SELECT id, industryid, type as 'किसिम', detail as 'विवरण', qty as 'वार्षिक परिमाण / क्षमता',  unit as 'एकाइ', source as 'श्रोत' From  industryreg_fuel_water where industryid='" + myIndId + "'";

            if (sqlcon.con.State == ConnectionState.Closed)
                sqlcon.con.Open();

            qry = new MySqlDataAdapter(sqst, sqlcon.con);

            qry.Fill(tb);

            dgv.Rows.Clear();
            dgv.Columns.Clear();

            dgv.Columns.Add("col1", tb.Columns[0].Caption); //id
            dgv.Columns.Add("col2", tb.Columns[1].Caption); //industryid

            //type is combobox
            DataGridViewComboBoxColumn dgcbType = new DataGridViewComboBoxColumn();
            //combobox is filled with array rather than dispmember and valuemember
            dgcbType.Items.AddRange(global.getSingleFieldArrayFromTable("select fuelwater_type FROM setup_fuel_water"));
            dgv.Columns.Add(dgcbType);
            dgv.Columns[2].HeaderText = tb.Columns[2].Caption; //header for type

            dgv.Columns.Add("col4", tb.Columns[3].Caption); //detail
            dgv.Columns.Add("col5", tb.Columns[4].Caption); //qty
            //unit is combobox
            DataGridViewComboBoxColumn dgcbUnit = new DataGridViewComboBoxColumn();
            //combobox is filled with array rather than dispmember and valuemember
            dgcbUnit.Items.AddRange(global.getSingleFieldArrayFromTable("select unitname FROM setup_units"));
            dgv.Columns.Add(dgcbUnit);
            dgv.Columns[5].HeaderText = tb.Columns[5].Caption; //header for unit

            dgv.Columns.Add("col6", tb.Columns[6].Caption); //source

            //filling data grid
            FillDataGrid(dgv, tb);

            dgv.Columns[0].Visible = false;
            dgv.Columns[1].Visible = false;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.ReadOnly = true;
            sqlcon.con.Close();
        }

        private void FormatDataGridEnvironment()
        {
            string sqst = "";
            MySqlDataAdapter qry;
            DataTable tb = new DataTable();
            DataGridView dgv = dgvEnvironment;
            sqst = "SELECT id, industryid, component as 'वातावरणीय अवयव', distance_detail as 'दुरी / विवरण', minimizing_idea as 'प्रतिकुल असर न्युनिकरणका उपायहरू' From  industryreg_environment where industryid='" + myIndId + "'";

            if (sqlcon.con.State == ConnectionState.Closed)
                sqlcon.con.Open();

            qry = new MySqlDataAdapter(sqst, sqlcon.con);

            qry.Fill(tb);

            dgv.Rows.Clear();
            dgv.Columns.Clear();

            dgv.Columns.Add("col1", tb.Columns[0].Caption); //id
            dgv.Columns.Add("col2", tb.Columns[1].Caption); //industryid
            //Component is combobox
            DataGridViewComboBoxColumn dgcbCompo = new DataGridViewComboBoxColumn();
            //global.fillCombo(dgcbUnit, "select id,affected_component FROM setup_environment_effect", "affected_component", "affected_component", "-");
            //combobox is filled with array rather than dispmember and valuemember
            dgcbCompo.Items.AddRange(global.getSingleFieldArrayFromTable("select affected_component FROM setup_environment_effect"));
            dgv.Columns.Add(dgcbCompo);
            dgv.Columns[2].HeaderText = tb.Columns[2].Caption; //header for component
            dgv.Columns.Add("col4", tb.Columns[3].Caption); //detail
            dgv.Columns.Add("col5", tb.Columns[4].Caption); //minimizing_area

            //filling data grid
            FillDataGrid(dgv, tb);

            dgv.Columns[0].Visible = false;
            dgv.Columns[1].Visible = false;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.ReadOnly = true;
            sqlcon.con.Close();
        }

        private void industryclose_Load(object sender, EventArgs e)
        {
            switch (disptype)
            {
                case "product":
                    TabControl1.SelectedTab = tabPage1;
                    break;

                case "investment":
                    TabControl1.SelectedTab = tabPage2;
                    break;

                case "machinery":
                    TabControl1.SelectedTab = tabPage3;
                    break;

                case "raw_material":
                    TabControl1.SelectedTab = tabPage4;
                    break;

                case "fuel_water":
                    TabControl1.SelectedTab = tabPage5;
                    break;

                case "environment":
                    TabControl1.SelectedTab = tabPage6;
                    break;
            }


            dgvSthirPuji.DefaultCellStyle = dgvChaluPuji.DefaultCellStyle = dgvEnvironment.DefaultCellStyle = dgvRawMat.DefaultCellStyle = dgvFuelWater.DefaultCellStyle = dgvMachinery.DefaultCellStyle = dgvProduct.DefaultCellStyle;

            dgvSthirPuji.AlternatingRowsDefaultCellStyle = dgvChaluPuji.AlternatingRowsDefaultCellStyle = dgvEnvironment.AlternatingRowsDefaultCellStyle = dgvRawMat.AlternatingRowsDefaultCellStyle = dgvFuelWater.AlternatingRowsDefaultCellStyle = dgvMachinery.AlternatingRowsDefaultCellStyle = dgvProduct.AlternatingRowsDefaultCellStyle;
            //dgvChaluPuji.AlternatingRowsDefaultCellStyle.Font.Name = "Kalimati";

            //this one is default son needs to be called 
            if (TabControl1.SelectedTab == tabPage1)
                FormatDataGridProduct();

            LockAll();
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (TabControl1.SelectedIndex)
            {
                case 0:
                    labelTitle.Text = "उद्योगबाट उत्पादन हुने वस्तु वा सेवाको विवरण";
                    if(dgvProduct.ReadOnly)
                        FormatDataGridProduct();

                    break;

                case 1:
                    labelTitle.Text = "उद्योगको परियोजना लागत तथा लगानी श्रोत सम्बन्धी विवरण";
                    if(dgvSthirPuji.ReadOnly && dgvChaluPuji.ReadOnly)
                        FormatDataGridInvestment();
                    break;

                case 2:
                    labelTitle.Text = "उद्योगमा प्रयोग हुने मेशिनको विवरण";
                    if (dgvMachinery.ReadOnly)
                        FormatDataGridMachinery();
                    break;

                case 3:
                    labelTitle.Text = "उद्योगलाई आवश्यक पर्ने कच्चा पदार्थको विवरण";
                    if (dgvRawMat.ReadOnly)
                        FormatDataGridRawMaterial();
                    break;

                case 4:
                    labelTitle.Text = "उद्योगमा प्रयोग हुने इन्धन तथा पानीको विवरण";
                    if(dgvFuelWater.ReadOnly)
                        FormatDataGridFuelWater();
                    break;

                case 5:
                    labelTitle.Text = "उद्योग सञ्चालनबाट प्रभावित हुन सक्ने वातावरणीय पक्षहरू सम्बन्धी विवरण";
                    if(dgvEnvironment.ReadOnly)
                        FormatDataGridEnvironment();
                    break;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string[] flds; //field names
            int[] fldp; //field positions
            bool[] fldin; //field types - numeric or other if is it needed to conver to number from unicode 
            bool[] ischf; //whether or not to validate the field
            string[] fldt; //type of the field -- string (str), numeric (num) or combobox (cbo)

            string tblname = "";
            DataGridView dgv;
            DataGridViewCell dgc;
           

            //product

            if (!dgvProduct.ReadOnly)
            {
                flds = new string[] { "material", "capacity", "unit", "unit_price" };
                fldp = new[] { 2, 3, 4, 5 };
                fldin = new[] { false, true, false, true };

                ischf = new bool[] { true, true, true, true };
                fldt = new string[] { "str", "num", "cbo", "num" };

                tblname = "industryreg_product_service";
                dgv = dgvProduct;
                dgc = getCellWithInvalidData(dgv, fldp, ischf, fldt);

                //Check if any cell has invalid data -- if null SAVE THE DATA otherwise generate report
                if (dgc == null) 
                    AddUpdateDataFromDataGrid(tblname, dgv, flds, fldp, fldt);
                else
                {
                    MessageBox.Show("कृपया सही डाटा प्रविष्ट गर्नुहोस्", "डाटा प्रविष्टी", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    dgv.CurrentCell = dgc;
                    dgv.BeginEdit(true);
                    return;
                }
            }

            if (!dgvSthirPuji.ReadOnly)
            {
                //STHIR PUJI
                flds = new string[] { "category", "detail", "amount" };
                fldp = new[] { 2, 3, 4 };
                fldin = new[] { false, false, true };

                ischf = new bool[] { true, false, true };
                fldt = new string[] { "cbo", "str", "num" };

                tblname = "industryreg_investment_sthir";
                dgv = dgvSthirPuji;
                dgc = getCellWithInvalidData(dgv, fldp, ischf, fldt);

                //Check if any cell has invalid data -- if null SAVE THE DATA otherwise generate report
                if (dgc == null)
                {
                    AddUpdateDataFromDataGrid(tblname, dgv, flds, fldp, fldt, false);

                    //ALSO UPDATE THE Swa Lagani and Hrin Lagani

                    if (sqlcon.con.State == ConnectionState.Closed)
                        sqlcon.con.Open();

                    MySqlCommand cmd = sqlcon.con.CreateCommand();
                    cmd.Connection = sqlcon.con;

                    MySqlCommand qr = new MySqlCommand("UPDATE " + tblname + " SET self_tot=@self_tot, loan_tot=@loan_tot WHERE industryid=@industryid");
                    qr.CommandType = CommandType.Text;
                    qr.Parameters.AddWithValue("@industryid", myIndId);
                    qr.Parameters.AddWithValue("@self_tot", global.convertUnicodeToNum(textSelfSthir.Text));
                    qr.Parameters.AddWithValue("@loan_tot", global.convertUnicodeToNum(textLoanSthir.Text));
                    if (sqlcon.con.State == ConnectionState.Closed) sqlcon.con.Open();
                    qr.Connection = sqlcon.con;
                    qr.ExecuteNonQuery();
                    qr = null;
                    cmd = null;
                    //qr.Dispose();
                    //cmd.Dispose();

                }
                else
                {
                    MessageBox.Show("कृपया सही डाटा प्रविष्ट गर्नुहोस्", "डाटा प्रविष्टी", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dgv.CurrentCell = dgc;
                    dgv.BeginEdit(true);
                    return;
                }
            }

            if (!dgvChaluPuji.ReadOnly)
            { 
                //AddUpdateDataFromDataGrid("industryreg_investment_sthir", dgvSthirPuji, flds, fldp, fldin, false);

                //CHALU PUJI
                flds = new string[] { "category", "detail", "amount" };
                fldp = new[] { 2, 3, 4 };
                fldin = new[] { false, false, true };

                ischf = new bool[] { true, false, true };
                fldt = new string[] { "cbo", "str", "num" };

                tblname = "industryreg_investment_chalu";
                dgv = dgvChaluPuji;
                dgc = getCellWithInvalidData(dgv, fldp, ischf, fldt);

                //Check if any cell has invalid data -- if null SAVE THE DATA otherwise generate report
                if (dgc == null)
                {
                    AddUpdateDataFromDataGrid(tblname, dgv, flds, fldp, fldt);

                    //ALSO UPDATE THE Swa Lagani and Hrin Lagani

                    if (sqlcon.con.State == ConnectionState.Closed)
                        sqlcon.con.Open();

                    MySqlCommand cmd = sqlcon.con.CreateCommand();
                    cmd.Connection = sqlcon.con;

                    MySqlCommand qr = new MySqlCommand("UPDATE " + tblname + " SET self_tot=@self_tot, loan_tot=@loan_tot WHERE industryid=@industryid");
                    qr.CommandType = CommandType.Text;
                    qr.Parameters.AddWithValue("@industryid", myIndId);
                    qr.Parameters.AddWithValue("@self_tot", global.convertUnicodeToNum(textSelfChalu.Text));
                    qr.Parameters.AddWithValue("@loan_tot", global.convertUnicodeToNum(textLoanChalu.Text));
                    if (sqlcon.con.State == ConnectionState.Closed) sqlcon.con.Open();
                    qr.Connection = sqlcon.con;
                    qr.ExecuteNonQuery();
                    qr = null;
                    cmd = null;
                    //qr.Dispose();
                    //cmd.Dispose();
                }
                else
                {
                    MessageBox.Show("कृपया सही डाटा प्रविष्ट गर्नुहोस्", "डाटा प्रविष्टी", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dgv.CurrentCell = dgc;
                    dgv.BeginEdit(true);
                    return;
                }

            }

            if (!dgvMachinery.ReadOnly)
            {
                flds = new string[] { "mach_name", "detail", "qty", "amount" };
                fldp = new[] { 2, 3, 4, 5 };
                fldin = new[] { false, false, true, true };
                //add and update from same function

                ischf = new bool[] { true, false, true, true };
                fldt = new string[] { "str", "str", "num", "num" };

                tblname = "industryreg_machinery";
                dgv = dgvMachinery;
                dgc = getCellWithInvalidData(dgv, fldp, ischf, fldt);

                //Check if any cell has invalid data -- if null SAVE THE DATA otherwise generate report
                if (dgc == null)
                    AddUpdateDataFromDataGrid(tblname, dgv, flds, fldp, fldt);
                else
                {
                    MessageBox.Show("कृपया सही डाटा प्रविष्ट गर्नुहोस्", "डाटा प्रविष्टी", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dgv.CurrentCell = dgc;
                    dgv.BeginEdit(true);
                    return;
                }
            }

            if (!dgvRawMat.ReadOnly)
            {
                flds = new string[] { "rawmat_name", "unit", "unit_price", "qty" };
                fldp = new[] { 2, 3, 4, 5 };
                fldin = new[] { false, false, true, true };

                ischf = new bool[] { true, true, true, true };
                fldt = new string[] { "str", "cbo", "num", "num" };

                tblname = "industryreg_rawmaterial";
                dgv = dgvRawMat;
                dgc = getCellWithInvalidData(dgv, fldp, ischf, fldt);

                //Check if any cell has invalid data -- if null SAVE THE DATA otherwise generate report
                if (dgc == null)
                    AddUpdateDataFromDataGrid(tblname, dgv, flds, fldp, fldt);
                else
                {
                    MessageBox.Show("कृपया सही डाटा प्रविष्ट गर्नुहोस्", "डाटा प्रविष्टी", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dgv.CurrentCell = dgc;
                    dgv.BeginEdit(true);
                    return;
                }

            }

            if (!dgvFuelWater.ReadOnly)
            {
                flds = new string[] { "type", "detail", "qty", "unit", "source" };
                fldp = new[] { 2, 3, 4, 5, 6 };
                fldin = new[] { false, false, true, false, false };

                ischf = new bool[] { true, false, true, true,false };
                fldt = new string[] { "cbo","str", "num", "cbo", "str" };

                tblname = "industryreg_fuel_water";
                dgv = dgvFuelWater;
                dgc = getCellWithInvalidData(dgv, fldp, ischf, fldt);

                //Check if any cell has invalid data -- if null SAVE THE DATA otherwise generate report
                if (dgc == null)
                    AddUpdateDataFromDataGrid(tblname, dgv, flds, fldp, fldt);
                else
                {
                    MessageBox.Show("कृपया सही डाटा प्रविष्ट गर्नुहोस्", "डाटा प्रविष्टी", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dgv.CurrentCell = dgc;
                    dgv.BeginEdit(true);
                    return;
                }
            }

            if (!dgvEnvironment.ReadOnly)
            {
                flds = new string[] { "component", "distance_detail", "minimizing_idea" };
                fldp = new[] { 2, 3, 4, 5 };
                fldin = new[] { false, false, false };

                ischf = new bool[] { true, true, true };
                fldt = new string[] { "cbo", "str", "str" };

                tblname = "industryreg_environment";
                dgv = dgvEnvironment;
                dgc = getCellWithInvalidData(dgv, fldp, ischf, fldt);

                //Check if any cell has invalid data -- if null SAVE THE DATA otherwise generate report
                if (dgc == null)
                    AddUpdateDataFromDataGrid(tblname, dgv, flds, fldp, fldt);
                else
                {
                    MessageBox.Show("कृपया सही डाटा प्रविष्ट गर्नुहोस्", "डाटा प्रविष्टी", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dgv.CurrentCell = dgc;
                    dgv.BeginEdit(true);
                    return;
                }
            }

        }

        private DataGridViewCell getCellWithInvalidData(DataGridView dgv, int[] fldPos, bool[] isChkFld, string[] fldTypes,bool isIgnoreWholeBlank=true )
        {
            DataGridViewCell errCell;

            for (int ii = 0; ii < dgv.Rows.Count; ii++)
            {
                //IGNORE IF WHOLE ROW IS BLANK
                if(isIgnoreWholeBlank==true)
                {
                    if (global.isWholeRowEmpty(dgv.Rows[ii]))
                        continue;
                }    

                for (int ff = 0; ff < fldPos.Length; ff++)
                {
                    if (isChkFld[ff] == true)
                    {
                        errCell = dgv.Rows[ii].Cells[fldPos[ff]];

                        if (errCell.Value == null || string.IsNullOrEmpty(errCell.Value.ToString()))
                        {
                            return errCell;
                        }
                        else if (fldTypes[ff] == "num") //NUMERIC
                        {
                            bool isNumeric = decimal.TryParse(global.convertUnicodeToNum(errCell.Value.ToString()), out decimal num);
                            if (!isNumeric)
                            {
                                return errCell;
                            }
                        }
                        else if (fldTypes[ff] == "cbo") //COMBO BOX
                        {
                            DataGridViewComboBoxCell dgcb = (DataGridViewComboBoxCell) errCell;
                            //gcb.DisplayMember = "unitname";
                            //dgcb.ValueMember = "unitname";
                            //if (!global.checkValueInCombo(dgcb, errCell.Value.ToString()))
                            //{
                               /// return errCell;
                            //}

                            if (!dgcb.Items.Contains(errCell.Value.ToString()))
                            {
                              //dgcb.Value;
                              MessageBox.Show("didn't find it");
                              return errCell;
                            }

                            //MessageBox.Show("कृपया सही डाटा प्रविष्ट गर्नुहोस्", "");
                            //dgv.CurrentCell = dgv.Rows[ii].Cells[fieldpos[ff]];
                            //dgv.BeginEdit(true);
                            //return;

                        }


                    }
                }
            }
            return null;
        }

        private void AddUpdateDataFromDataGridNewTry(string tblname, DataGridView dgv, string[] fields, int[] fieldpos, bool[] fieldisNum, bool showMsg = true)
        {
            string SqStmt, ValStmt;
            if (sqlcon.con.State == ConnectionState.Closed)
                sqlcon.con.Open();

            MySqlCommand cmd = sqlcon.con.CreateCommand();
            //MySqlTransaction trans;
            //trans = sqlcon.con.BeginTransaction();

            cmd.Connection = sqlcon.con;
            //cmd.Transaction = trans;

            try
            {
                //DELETE WHOLE RECORD FROM TABLE

                    MySqlCommand qr = new MySqlCommand("DELETE FROM " + tblname + " WHERE industryid=@ind");
                    qr.CommandType = CommandType.Text;
                    qr.Parameters.AddWithValue("@indid", myIndId);
                    if (sqlcon.con.State == ConnectionState.Closed) sqlcon.con.Open();
                    qr.Connection = sqlcon.con;
                    qr.ExecuteNonQuery();


                //NOW INSERT ALL RECORDS



                for (int ii = 0; ii < dgv.Rows.Count; ii++)
                {
                    if (global.isWholeRowEmpty(dgv.Rows[ii]) == true)
                        continue;

                    if (dgv.Rows[ii].Cells[0].Value != null)
                    {
                        //IF id field is not empty / null then update the existing records
                        //CREATE UPDATE STATEMENT

                        SqStmt = "";

                        for (int ff = 0; ff < fields.Length; ff++)
                        {
                            SqStmt = SqStmt + fields[ff] + " = @" + fields[ff];
                            if (ff < fields.Length - 1)
                                SqStmt = SqStmt + ", ";
                        }

                        //Prepare update statement
                        cmd.CommandText = "UPDATE " + tblname + " SET " + SqStmt + " WHERE id=@id AND industryid=@industryid";
                    }
                    else
                    {
                        //IF ID FIELD IS EMPTY THEN INSERT INTO THE TABLE

                        SqStmt = "industryid, ";
                        ValStmt = "@industryid, ";

                        //CREATE INSERT fields and Values
                        for (int ff = 0; ff < fields.Length; ff++)
                        {
                            SqStmt += fields[ff];
                            ValStmt += "@" + fields[ff];

                            if (ff < fields.Length - 1)
                            {
                                SqStmt += ", ";
                                ValStmt += ", ";
                            }
                        }
                        //PREPARE THE STATEMENT 
                        cmd.CommandText = "INSERT INTO " + tblname + " (" + SqStmt + ") VALUES (" + ValStmt + ")";
                    }

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@id", dgv.Rows[ii].Cells[0].Value);
                    cmd.Parameters.AddWithValue("@industryid", myIndId);

                    //CREATE PARAMETERS AND THEIR VALUES
                    for (int ff = 0; ff < fields.Length; ff++)
                    {
                        if (dgv.Rows[ii].Cells[fieldpos[ff]].Value != null)
                        {
                            string fldval = dgv.Rows[ii].Cells[fieldpos[ff]].Value.ToString();
                            if (fieldisNum[ff] == true)
                            {
                                //numeric value -- need to check whether the entered text is a number
                                //and also need to convert unicode to number before saving

                                bool isNumeric = decimal.TryParse(global.convertUnicodeToNum(fldval), out decimal num);

                                if (isNumeric)
                                {
                                    cmd.Parameters.AddWithValue("@" + fields[ff], global.convertUnicodeToNum(fldval));
                                }

                                else
                                {
                                    MessageBox.Show("कृपया सही डाटा प्रविष्ट गर्नुहोस्", "");
                                    dgv.CurrentCell = dgv.Rows[ii].Cells[fieldpos[ff]];
                                    dgv.BeginEdit(true);
                                    return;
                                }
                            }
                            else //not numeric - check if it is empty -- if not insert
                            {
                                if (!string.IsNullOrEmpty(fldval))
                                {

                                    cmd.Parameters.AddWithValue("@" + fields[ff], fldval);
                                }
                                else
                                {
                                    MessageBox.Show("कृपया सही डाटा प्रविष्ट गर्नुहोस्", "");
                                    dgv.CurrentCell = dgv.Rows[ii].Cells[fieldpos[ff]];
                                    dgv.BeginEdit(true);
                                    return;
                                }

                            }
                        }
                        else
                        {
                            MessageBox.Show("कृपया सही डाटा प्रविष्ट गर्नुहोस्", "");
                            dgv.CurrentCell = dgv.Rows[ii].Cells[fieldpos[ff]];
                            dgv.BeginEdit(true);
                            return;
                        }
                    }
                    cmd.ExecuteNonQuery();
                    //MessageBox.Show(cmd.CommandText.ToString());


                }

                if (showMsg == true) //it comes false if another save work is also to be checked e.g. investment chalu and sthir
                {
                    MessageBox.Show("सफलतापुर्वक सेभ गरियो ।", "सेभ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LockAll();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \n " + ex.Message);
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

                MySqlCommand cmd = new MySqlCommand("select industryregno, fiscalyear, officeid, officedist from setup_industryregid where industryregid=6", sqlcon.con);
                cmd.CommandType = CommandType.Text;
                // cmd.Connection = con;
                MySqlDataReader sdr = cmd.ExecuteReader();
                sdr.Read();
                transid = global.csioid.ToString().Trim() + global.fyid.ToString().Trim() + sdr["industryregno"].ToString().Trim();

                sdr.Close(); //closing data reader
                sqlcon.con.Close();

                // if (sqlcon.con.State == ConnectionState.Closed)
                sqlcon.con.Open();

                MySqlCommand cmo = new MySqlCommand("update setup_industryregid SET industryregno= industryregno+1 WHERE industryregid=6", sqlcon.con);
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
        private void AddUpdateDataFromDataGrid(string tblname, DataGridView dgv, string[] fields, int[] fieldpos, string[] fieldtypes,bool showMsg=true)
        {
            string SqStmt, ValStmt;
            if (sqlcon.con.State == ConnectionState.Closed)
                sqlcon.con.Open();

            MySqlCommand cmd = sqlcon.con.CreateCommand();
            //MySqlTransaction trans;
            //trans = sqlcon.con.BeginTransaction();

            cmd.Connection = sqlcon.con;
            //cmd.Transaction = trans;

            try
            {
                for (int ii = 0; ii < dgv.Rows.Count; ii++)
                {
                    //IGNORE IF WHOLE ROW IS BLANK
                    if (global.isWholeRowEmpty(dgv.Rows[ii]))
                            continue;

                    if (dgv.Rows[ii].Cells[0].Value!=null)
                    {
                        //IF id field is not empty / null then update the existing records
                        //CREATE UPDATE STATEMENT
                        SqStmt = "";

                        for (int ff = 0; ff < fields.Length; ff++)
                        {
                            SqStmt = SqStmt + fields[ff] + " = @" + fields[ff];
                            if (ff < fields.Length - 1)
                                SqStmt = SqStmt + ", ";
                        }

                        //Prepare update statement
                        cmd.CommandText = "UPDATE " + tblname + " SET " + SqStmt + " WHERE id=@id AND industryid=@industryid";
                        global.sync_dblog(tblname.ToString(), myIndId.ToString(), "UPDATE", dgv.Rows[ii].Cells[0].Value.ToString(), "id");

                    }
                    else
                    {
                        //IF ID FIELD IS EMPTY THEN INSERT INTO THE TABLE

                        SqStmt = "industryid,id, ";
                        ValStmt = "@industryid,@tid,";
                        
                        //CREATE INSERT fields and Values
                        for (int ff = 0; ff < fields.Length; ff++)
                        {
                            SqStmt += fields[ff];
                            ValStmt += "@" + fields[ff];

                            if (ff < fields.Length - 1)
                            {
                                SqStmt += ", ";
                                ValStmt += ", ";
                            }
                        }
                        transactionid();
                        //PREPARE THE STATEMENT 
                        cmd.CommandText = "INSERT INTO " + tblname + " (" + SqStmt + ") VALUES (" + ValStmt + ")";
                        global.sync_dblog(tblname.ToString(), myIndId.ToString(), "INSERT", transid.ToString(), "id");
                    }
                    
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@id", dgv.Rows[ii].Cells[0].Value);
                    cmd.Parameters.AddWithValue("@industryid", myIndId);
                    cmd.Parameters.AddWithValue("@tid", transid);

                    //CREATE PARAMETERS AND THEIR VALUES
                    for (int ff = 0; ff < fields.Length; ff++)
                    {
                        string fldval;

                        if (dgv.Rows[ii].Cells[fieldpos[ff]].Value == null)
                            fldval = "";
                        else
                            fldval = dgv.Rows[ii].Cells[fieldpos[ff]].Value.ToString();

                        //check the type of field - num or not (str, cbo)
                        if (fieldtypes[ff] == "num")
                        {
                            //numeric value -- need to check whether the entered text is a number
                            //and also need to convert unicode to number before saving
                            fldval = global.convertUnicodeToNum(fldval);

                            //check if it is number, if not convert it to zero
                            if (!decimal.TryParse(fldval, out decimal num))
                                fldval = "0";

                            cmd.Parameters.AddWithValue("@" + fields[ff], fldval);
                        }
                        else //not numeric - 
                        {
                            cmd.Parameters.AddWithValue("@" + fields[ff], fldval);
                        }
                    }
                    cmd.ExecuteNonQuery();
                    //MessageBox.Show(cmd.CommandText.ToString());
                }

                if (showMsg == true) //it comes false if another save work is also to be checked e.g. investment chalu and sthir
                {
                    MessageBox.Show("सफलतापुर्वक सेभ गरियो ।", "सेभ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LockAll();
                }
            }
            catch (Exception ex)
           {
            MessageBox.Show("Error \n " + ex.Message);
           }

        }

        private void LockAll()
        {
            if (TabControl1.SelectedTab == tabPage1)
            {
                dgvProduct.ReadOnly = true;
            }
            else if (TabControl1.SelectedTab == tabPage2)
            {
                dgvChaluPuji.ReadOnly = true;
                dgvSthirPuji.ReadOnly = true;

                textLoanChalu.ReadOnly = true;
                textLoanSthir.ReadOnly = true;
                textSelfChalu.ReadOnly = true;
                textSelfSthir.ReadOnly = true;
            }
            else if (TabControl1.SelectedTab == tabPage3)
            {
                dgvMachinery.ReadOnly = true;
            }
            else if (TabControl1.SelectedTab == tabPage4)
            {
                dgvRawMat.ReadOnly = true;
            }
            else if (TabControl1.SelectedTab == tabPage5)
            {
                dgvFuelWater.ReadOnly = true;
            }
            else if (TabControl1.SelectedTab == tabPage4)
            {
                dgvEnvironment.ReadOnly = true;
            }

            btnAddEdit.Enabled = true;
            btnAddEdit.BackColor = enaColo;
            btnDelete.Enabled = true;
            btnDelete.BackColor = enaColo;
            btnSave.Enabled = false;
            btnSave.BackColor = disColor;
            btnCancel.Enabled = false;
            btnCancel.BackColor = disColor;
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(dgvProduct.SelectedRows[0].Cells[0].Value.ToString());
            //myId = Convert.ToInt32(dgvProduct.SelectedRows[0].Cells[0].Value);
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            if (TabControl1.SelectedTab == tabPage1)
            {
                dgvProduct.AllowUserToAddRows = true;
                //dgvProduct.Rows.Add();
                dgvProduct.ReadOnly = false;
                dgvProduct.Columns[6].ReadOnly = true;
                dgvProduct.Focus();
            }
            else if (TabControl1.SelectedTab == tabPage2)
            {
                dgvSthirPuji.AllowUserToAddRows = true;
                dgvChaluPuji.AllowUserToAddRows = true;

                dgvChaluPuji.ReadOnly = false;
                dgvSthirPuji.ReadOnly = false;

                textLoanChalu.ReadOnly = false;
                textLoanSthir.ReadOnly = false;
                textSelfChalu.ReadOnly = false;
                textSelfSthir.ReadOnly = false;
                dgvSthirPuji.Focus();

            }
            else if (TabControl1.SelectedTab == tabPage3)
            {
                dgvMachinery.AllowUserToAddRows = true;
                dgvMachinery.ReadOnly = false;
                dgvMachinery.Focus();
            }
            else if (TabControl1.SelectedTab == tabPage4)
            {
                dgvRawMat.AllowUserToAddRows = true;
                dgvRawMat.ReadOnly = false;
                dgvRawMat.Columns[6].ReadOnly = true;
                dgvRawMat.Focus();
            }
            else if (TabControl1.SelectedTab == tabPage5)
            {
                dgvFuelWater.AllowUserToAddRows = true;
                dgvFuelWater.ReadOnly = false;
                dgvFuelWater.Focus();
            }
            else if (TabControl1.SelectedTab == tabPage4)
            {
                dgvEnvironment.AllowUserToAddRows = true;
                dgvEnvironment.ReadOnly = false;
                dgvEnvironment.Focus();
            }

            btnSave.Enabled = true;
            btnSave.BackColor = enaColo;
            btnCancel.Enabled = true;
            btnCancel.BackColor = enaColo;

            btnAddEdit.Enabled = false;
            btnAddEdit.BackColor = disColor;
            btnDelete.Enabled = false;
            btnDelete.BackColor = disColor;
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (TabControl1.SelectedTab == tabPage1)
            {
                dgvProduct.ReadOnly = false;
            }
            else if (TabControl1.SelectedTab == tabPage2)
            {
                dgvChaluPuji.ReadOnly = false;
                dgvSthirPuji.ReadOnly = false;
            }
            else if (TabControl1.SelectedTab == tabPage3)
            {
                dgvMachinery.ReadOnly = false;
            }
            else if (TabControl1.SelectedTab == tabPage4)
            {
                dgvRawMat.ReadOnly = false;
            }
            else if (TabControl1.SelectedTab == tabPage5)
            {
                dgvFuelWater.ReadOnly = false;
            }
            else if (TabControl1.SelectedTab == tabPage4)
            {
                dgvEnvironment.ReadOnly = false;
            }
            //mySaveMode = "edit";
            btnSave.Enabled = true;
            btnSave.BackColor = enaColo;
            btnAddEdit.Enabled = false;
            btnAddEdit.BackColor = disColor;
            btnDelete.Enabled = false;
            btnDelete.BackColor = disColor;
        }

        private decimal DoAmountCalculation(DataGridViewRow rr,int fld1, int fld2)
        {
            decimal resval;
            if (rr.Cells[3].Value != null && rr.Cells[5].Value != null)
            {
                bool isNum;
                decimal qty = 0, rate = 0, num;

                isNum = decimal.TryParse(global.convertUnicodeToNum(rr.Cells[fld1].Value.ToString()), out num);
                if (isNum)
                    qty = Convert.ToDecimal(global.convertUnicodeToNum(rr.Cells[fld1].Value.ToString()));

                isNum = decimal.TryParse(global.convertUnicodeToNum(rr.Cells[fld2].Value.ToString()), out num);
                if (isNum)
                    rate = Convert.ToDecimal(global.convertUnicodeToNum(rr.Cells[fld2].Value.ToString()));
                resval = qty * rate;
            }

            else
            {
                resval = 0;
            }

            return resval;
        }

        private string DoTotalCalculation(DataGridView dgv,int fld)
        {
            DataGridViewRow rr;
            //calculating total
            decimal tot = 0,num=0;
            for (int ii = 0; ii < dgv.Rows.Count; ii++)
            {
                rr = dgv.Rows[ii];
                if (rr.Cells[fld].Value != null && decimal.TryParse(global.convertUnicodeToNum(rr.Cells[fld].Value.ToString()), out num))
                    tot += Convert.ToDecimal(global.convertUnicodeToNum(rr.Cells[fld].Value.ToString()));
            }
            //labelTotProduct.Text = tot.ToString();
            return tot.ToString();
        }

        private void dgvProduct_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProduct.ReadOnly == true)
                return;

            int cc = e.ColumnIndex;
            DataGridViewRow rr = dgvProduct.Rows[e.RowIndex];

            if (cc == 3 || cc == 5)
            {
                if (rr.Cells[3].Value != null && rr.Cells[5].Value != null)
                {
                        bool isNum;
                    decimal qty = 0, rate = 0, num;

                        isNum = decimal.TryParse(global.convertUnicodeToNum(rr.Cells[3].Value.ToString()), out num);
                        if(isNum)
                            qty = Convert.ToDecimal(global.convertUnicodeToNum(rr.Cells[3].Value.ToString()));

                        isNum = decimal.TryParse(global.convertUnicodeToNum(rr.Cells[5].Value.ToString()), out num);
                        if(isNum)
                        rate = Convert.ToDecimal(global.convertUnicodeToNum(rr.Cells[5].Value.ToString()));
                        
                        rr.Cells[6].Value = qty * rate;
                }
                    
                else
                {
                    rr.Cells[6].Value = "०";
                }

                //calculating total
                decimal tot=0;
                for(int ii=0;ii<dgvProduct.Rows.Count;ii++)
                {
                    rr = dgvProduct.Rows[ii];
                    if(rr.Cells[6].Value !=null)
                        tot += Convert.ToDecimal(global.convertUnicodeToNum(rr.Cells[6].Value.ToString()));
                }
                labelTotProduct.Text = tot.ToString();
            }
        }

        private void dgvSthirPuji_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSthirPuji.ReadOnly == true)
                return;
            int cc = e.ColumnIndex;
            DataGridViewRow rr = dgvSthirPuji.Rows[e.RowIndex];
            decimal tot = 0, totsthirchalu = 0;
            for (int ii = 0; ii < dgvSthirPuji.Rows.Count; ii++)
            {
                rr = dgvSthirPuji.Rows[ii];
                if (rr.Cells[4].Value != null)
                {

                    decimal qty = 0, num;
                    bool isNum = decimal.TryParse(global.convertUnicodeToNum(rr.Cells[4].Value.ToString()), out num);

                    if (isNum)
                        qty = Convert.ToDecimal(global.convertUnicodeToNum(rr.Cells[4].Value.ToString()));

                    tot += qty;

                    labelTotSthir.Text = tot.ToString();
                    //add it with chalu
                    totsthirchalu = tot + Convert.ToDecimal(global.convertUnicodeToNum(labelTotChalu.Text));
                    labelTotInvestment.Text = totsthirchalu.ToString();
                }

            }
        }

        private void dgvChaluPuji_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvChaluPuji.ReadOnly == true)
                return;
            int cc = e.ColumnIndex;
            DataGridViewRow rr = dgvChaluPuji.Rows[e.RowIndex];
            decimal tot = 0, totsthirchalu = 0;
            for (int ii = 0; ii < dgvChaluPuji.Rows.Count; ii++)
            {
                rr = dgvChaluPuji.Rows[ii];
                if (rr.Cells[4].Value != null)
                {

                    decimal qty = 0, num;
                    bool isNum = decimal.TryParse(global.convertUnicodeToNum(rr.Cells[4].Value.ToString()), out num);

                    if (isNum)
                        qty = Convert.ToDecimal(global.convertUnicodeToNum(rr.Cells[4].Value.ToString()));

                    tot += qty;

                    labelTotChalu.Text = tot.ToString();

                    //add it with sthir
                    totsthirchalu = tot + Convert.ToDecimal(global.convertUnicodeToNum(labelTotSthir.Text));
                    labelTotInvestment.Text = totsthirchalu.ToString();
                }

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataGridView dgv = new DataGridView();
            string tbl="";
            int fldTotCalc = -1; //field having calculated value;
            Label totLabel = new Label();

            if (TabControl1.SelectedTab == tabPage1)
            {
                //dgvProduct.ReadOnly = false;
                dgv = dgvProduct;
                tbl = "industryreg_product_service";
                fldTotCalc = 6;
                totLabel = labelTotProduct;
            }
            else if (TabControl1.SelectedTab == tabPage2)
            {
                if (dgvSthirPuji.Rows.Count > 0 && dgvSthirPuji.SelectedRows.Count > 0)
                {
                    dgv = dgvSthirPuji;
                    tbl = "industryreg_investment_sthir";
                    fldTotCalc = 4;
                    totLabel = labelTotSthir;
                }
                else
                {
                    dgv = dgvChaluPuji;
                    tbl = "industryreg_investment_chalu";
                    fldTotCalc = 4;
                    totLabel = labelTotChalu;
                }
            }
            else if (TabControl1.SelectedTab == tabPage3)
            {
                dgv = dgvMachinery;
                tbl = "industryreg_machinery";
                fldTotCalc = 5;
                totLabel = labelTotMachinery;
            }
            else if (TabControl1.SelectedTab == tabPage4)
            {
                dgv = dgvRawMat;
                tbl = "industryreg_rawmaterial";
                fldTotCalc = 6;
                totLabel = labelTotRawMat;
            }
            else if (TabControl1.SelectedTab == tabPage5)
            {
                dgv = dgvFuelWater;
                tbl = "industryreg_fuel_water";
            }
            else if (TabControl1.SelectedTab == tabPage4)
            {
                dgv = dgvEnvironment;
                tbl = "industryreg_environment";
            }

            string ttl = TabControl1.SelectedTab.Text.ToString();

            if (dgv.Rows.Count>0 && dgv.SelectedRows.Count>0)
            {
                //MessageBox.Show(dgv.SelectedRows[0].Index.ToString());
                //function for delete

                if(global.isWholeRowEmpty(dgv.SelectedRows[0]))//empty row, only remove from datagridview
                {
                    dgv.Rows.Remove(dgv.SelectedRows[0]); 
                }
                //non empty row -- ask and remove
                else if (MessageBox.Show("रोजिएको " + ttl + "को विवरणलाई हटाउने हो ?", "विवरण हटाउन", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                 {
                    if (dgv.SelectedRows[0].Cells[0].Value != null)
                    {
                        //row comes from database -- not added new -- so remove from db also

                        int iid = Convert.ToInt32(dgv.SelectedRows[0].Cells[0].Value.ToString());

                        MySqlCommand qr = new MySqlCommand("DELETE FROM " + tbl + " WHERE id=@id AND industryid=@industryid");

                        qr.CommandType = CommandType.Text;
                        qr.Parameters.AddWithValue("@id", iid);
                        qr.Parameters.AddWithValue("@industryid", myIndId);

                        if (sqlcon.con.State == ConnectionState.Closed) sqlcon.con.Open();

                        qr.Connection = sqlcon.con;

                        qr.ExecuteNonQuery();

                        //if (n > 0)
                        //{
                            sqlcon.con.Close();
                           // MessageBox.Show("डाटा  सफलता पुर्वक हटाइयो ।");
                     }
                        
                        //remove from datagridview - whether it is from database or added by user
                        dgv.Rows.Remove(dgv.SelectedRows[0]);

                    //recalculate if needed
                    if (fldTotCalc!=-1)
                    {
                        totLabel.Text= DoTotalCalculation(dgv, fldTotCalc);
                    }

                }
            }
            else
            {
                MessageBox.Show("दिइएको तालिकामा हटाउनुपर्ने " + ttl + "को विवरणलाई रोज्नुहोस् ।","विवरण हटाउन", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
           
            //mySaveMode = "edit";
            //btnSave.Enabled = true;
            //btnAddNew.Enabled = false;
            //btnEdit.Enabled = false;
            //btnDelete.Enabled = false;
        }

        private void dgvSthirPuji_Enter(object sender, EventArgs e)
        {
            dgvChaluPuji.ClearSelection();
        }

        private void dgvChaluPuji_Enter(object sender, EventArgs e)
        {
            dgvSthirPuji.ClearSelection();
        }

        private void dgvRawMat_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = dgvRawMat;
            if (dgv.ReadOnly == true)
                return;

            int cc = e.ColumnIndex;
            DataGridViewRow rr = dgv.Rows[e.RowIndex];

            if (cc == 4 || cc == 5)
            {
                rr.Cells[6].Value = DoAmountCalculation(rr, 4, 5).ToString();
            }
            labelTotRawMat.Text = DoTotalCalculation(dgv, 6).ToString();
        }

        private void industryreg_subforms_FormClosing(object sender, FormClosingEventArgs e)
        {
           if (btnSave.Enabled)
            {
                if (MessageBox.Show("इन्ट्री गरिएका सबै डाटा सेभ भएका छैनन् । \n के तपाईँ सेभ नगरिकन यो फर्म बन्द गर्न चाहनुहुन्छ ?","फर्म बन्द ?",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button2)==DialogResult.No)
                {
                    e.Cancel=true;
                }
            }
        }

        private void industryreg_subforms_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (myparentName == "industryreg")
                myparent.displayAdditionalDetails(); //updating the parent form
            else if (myparentName == "capital_update")
                myparent2.getInvestmentFromTable(); //updating parent form - capital update
            else if (myparentName == "machinery")
                myparent3.getMachineryFromTable();
            else if (myparentName == "fuel_water")
                myparent3.getElectricFromTable();
        }

        private void dgvMachinery_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = dgvMachinery;
            if (dgv.ReadOnly == true)
                return;

            int cc = e.ColumnIndex;
            //DataGridViewRow rr = dgv.Rows[e.RowIndex];

            if (cc == 5)
            {
                //rr.Cells[6].Value = DoAmountCalculation(rr, 4, 5).ToString();
                labelTotMachinery.Text = DoTotalCalculation(dgv, 5).ToString();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            LockAll();
        }

        private string CalculateSelfLoan(string inp, string tot)
        {
            string res;
            decimal tt = 0, ii = 0, num;

            if (tot == null || string.IsNullOrEmpty(tot))
                tt = 0;

            if (inp == null || string.IsNullOrEmpty(inp))
                ii = 0;

            if (decimal.TryParse(global.convertUnicodeToNum(tot), out num))
                tt= Convert.ToDecimal(global.convertUnicodeToNum(tot));

            if (decimal.TryParse(global.convertUnicodeToNum(inp), out num))
                ii = Convert.ToDecimal(global.convertUnicodeToNum(inp));

            res=(tt - ii).ToString();
            return res;
        }


        private void textSelfSthir_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textSelfSthir.Text)) textSelfSthir.Text = "0";

            textLoanSthir.Text =CalculateSelfLoan(textSelfSthir.Text, labelTotSthir.Text);
            
        }

        private void textLoanSthir_Leave(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textLoanSthir.Text)) textLoanSthir.Text = "0";

            textSelfSthir.Text= CalculateSelfLoan(textLoanSthir.Text, labelTotSthir.Text);
        }

        private void textSelfChalu_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textSelfChalu.Text)) textSelfChalu.Text = "0";
            textLoanChalu.Text=CalculateSelfLoan(textSelfChalu.Text, labelTotChalu.Text);
        }

        private void textLoanChalu_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textLoanChalu.Text)) textLoanChalu.Text = "0";
            textSelfChalu.Text = CalculateSelfLoan(textLoanChalu.Text, labelTotChalu.Text);
        }

        private void labelTotSthir_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textLoanSthir.Text))
                textLoanSthir.Text = "0";

            textSelfSthir.Text = CalculateSelfLoan(textLoanSthir.Text, labelTotSthir.Text);

        }

        private void labelTotChalu_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textLoanChalu.Text))
                textLoanChalu.Text = "0";

            textSelfChalu.Text = CalculateSelfLoan(textLoanChalu.Text, labelTotChalu.Text);
        }
    }
}
