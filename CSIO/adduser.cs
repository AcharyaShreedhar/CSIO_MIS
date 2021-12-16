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
    public partial class adduser : Form
    {
        public adduser()
        {
            InitializeComponent();
        }

        string savemode; //add for adding new and update for updating existing user
        public string myIndId = "";
        public void DisplayData()
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     login.userid, login.username AS 'युजरको नाम', login.name AS 'पुरा नाम', login.address AS 'ठेगाना', login.phone AS 'सम्पर्क नं', user_status.status AS 'युजरको अवस्था',                       user_type.usertypename AS 'युजर प्रकार',pisno As 'संकेत नं',setup_officecategory.officenamenep As 'कार्यालयको प्रकार', officeid,setup_faat.faat_name As 'कार्यक्षेत्र फाँट' FROM         login INNER JOIN               user_status ON login.status = user_status.statusid INNER JOIN      user_type ON login.usertype = user_type.usertypeid INNER JOIN setup_officecategory ON setup_officecategory.office_catid=login.office_cat INNER JOIN setup_faat ON setup_faat.faat_id=login.faat WHERE     (login.usertype <= '" + global.usertype + "')", sqlcon.con);
            DataTable tb = new DataTable();
            qry.Fill(tb);
            dataGridView1.DataSource = tb;
            sqlcon.con.Close();

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[9].Visible = false;
        }
        public void DisplayMenu(String office_category)
        {
          //  MessageBox.Show(office_category.ToString());
            try
            {
                string columnname = "ministry";
            if(office_category=="1")
            {
                columnname = "ministry";
            }
            else if (office_category == "2")
            {
                columnname = "nirdesanalaya";
            }
            else
            {
                columnname = "gharelu";
            }
               // MessageBox.Show(columnname.ToString());
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                MySqlDataAdapter qry = new MySqlDataAdapter("SELECT `submenu_id`, submenu_name AS 'मेनु', `main_menuid` FROM `user_submenu` WHERE "+columnname.ToString()+"=1", sqlcon.con);
                DataTable tb = new DataTable();
                qry.Fill(tb);

                if (tb.Rows.Count > 0)
                {
                    menugrid.DataSource = tb;
                    DataGridViewCheckBoxColumn datacheckbox = new DataGridViewCheckBoxColumn();
                    datacheckbox.HeaderText‍ = "अधिकार";
                    datacheckbox.Name = "menupermission";
                    datacheckbox.Width = 30;
                    menugrid.Columns.Insert(3, datacheckbox);

                    //menugrid.Cells["menupermission"].Value = true;
                    //datacheckbox.ValueType = true;

                    //foreach (DataGridViewRow row in menugrid.Rows)
                    //{
                    //    ((DataGridViewCheckBoxCell)row.Cells["menupermission"]).Value = datacheckbox.checked;
                    //    row.Cells["menupermission"].Value = true;
                    //    //  bool isSelected = Convert.ToBoolean(row.Cells["menupermission"].Value);
                    //    //if (isSelected)
                    //    //{
                    //    //    //message += Environment.NewLine;
                    //    //    //message += row.Cells["Name"].Value.ToString();
                    //    //}
                    //}


                    menugrid.Columns[0].Visible = false;
                    menugrid.Columns[2].Visible = false;

                }
                sqlcon.con.Close();
              
               


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void DisplayAction()
        {
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                MySqlDataAdapter qry = new MySqlDataAdapter("SELECT user_operationid, operationname As 'कार्यावाही' FROM `user_permission_operation` ", sqlcon.con);
                DataTable tb = new DataTable();
                qry.Fill(tb);

                if (tb.Rows.Count > 0)
                {
                    actiongrid.DataSource = tb;
                    DataGridViewCheckBoxColumn datacheckbox = new DataGridViewCheckBoxColumn();
                    datacheckbox.HeaderText‍ = "अधिकार";
                    datacheckbox.Name = "menuaction";
                    datacheckbox.Width = 30;
                    actiongrid.Columns.Insert(2, datacheckbox);

                    //datacheckbox.ValueType = true;

                    foreach (DataGridViewRow Row in menugrid.Rows)
                    {
                        //(DataGridViewCheckBoxCell)Row.Cells["menupermission"].Value = checked;
                    }




                }
                sqlcon.con.Close();

                actiongrid.Columns[0].Visible = false;
               // menugrid.Columns[2].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //public void usersetting(AutoCompleteStringCollection data)
        public void cleardata()
        {
            username.Text = "";
            password.Text = "";
            repassword.Text = "";
            name.Text = "";
            address.Text = "";
            phone.Text = "";
           pisno.Text = "";

        }
        public void userssetting()
        {
            // MessageBox.Show("Working");
            // //string connetionString = null;
            // //SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            //connetionString = mysqlcon.conn.ToString();
            sql = "SELECT usertypeid, usertypename FROM user_type WHERE usertypeid<='" + global.usertype + "'";
            //connection = new MySqlConnection(mysqlcon.conn);
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();


                usertype.DataSource = ds.Tables[0];
                usertype.ValueMember = "usertypeid";
                usertype.DisplayMember = "usertypename";
                // foreach (DataRow row in ds.Tables[0].Rows)
                // {
                //MessageBox.Show("Working"+usertype.ValueMember.ToString());
                //    usertype.Items.Add(row[1].ToString());

                // data.Add(row[1].ToString());
                //citizen_off.Items.Add(row[0].ToString()+" Y "+row[1].ToString());
                //  }
                sqlcon.con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }
        public void usertypeedit(string firmobj)
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
            // connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.passwordname + "'";
            sql = "SELECT     usertypeid, usertypename FROM user_type  WHERE (usertypename = @firmobj)";
           // connection = new MySqlConnection(connetionString);
            try
            {

                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                adapter.SelectCommand.Parameters.AddWithValue("@firmobj", firmobj.ToString());
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                sqlcon.con.Close();

                //firm_obj.DataSource = ds.Tables[0];
                //firm_obj.ValueMember = "subcategory_id";
                //firm_obj.DisplayMember = "subcategory_nepname";
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    usertype.SelectedValue = row[0].ToString();
                    //citizen_off.Items.Add(row[0].ToString()+" Y "+row[1].ToString());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }
        public void statusedit(string firmobj)
        {
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            //string connetionString = null;
            ////SqlConnection connection;
            MySqlCommand command;
           MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            // connetionString = "Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.passwordname + "'";
            sql = "SELECT     statusid, status FROM user_status  WHERE (status = @firmobj)";
           // connection = new MySqlConnection(connetionString);
            try
            {

                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                adapter.SelectCommand.Parameters.AddWithValue("@firmobj", firmobj.ToString());
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                sqlcon.con.Close();

                //firm_obj.DataSource = ds.Tables[0];
                //firm_obj.ValueMember = "subcategory_id";
                //firm_obj.DisplayMember = "subcategory_nepname";
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    status.SelectedValue = row[0].ToString();
                    //citizen_off.Items.Add(row[0].ToString()+" Y "+row[1].ToString());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }
        public void usersstatus()
        {
            // MessageBox.Show("Working");
            // //string connetionString = null;
            // //SqlConnection connection;
            if (sqlcon.con.State == ConnectionState.Closed)
            {
                sqlcon.con.Open();
            }
            MySqlCommand command;
           MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            //connetionString = mysqlcon.conn.ToString();
            sql = "SELECT statusid, status FROM user_status";
            //connection = new MySqlConnection(mysqlcon.conn);
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();


                status.DataSource = ds.Tables[0];
                status.ValueMember = "statusid";
                status.DisplayMember = "status";
                // foreach (DataRow row in ds.Tables[0].Rows)
                // {
                //MessageBox.Show("Working"+usertype.ValueMember.ToString());
                //    usertype.Items.Add(row[1].ToString());

                // data.Add(row[1].ToString());
                //citizen_off.Items.Add(row[0].ToString()+" Y "+row[1].ToString());
                //  }
                sqlcon.con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }
        public void officecategory()
        {
            // MessageBox.Show("Working");
            // //string connetionString = null;
            // //SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            //connetionString = mysqlcon.conn.ToString();
            sql = "SELECT office_catid,  officenamenep FROM setup_officecategory WHERE office_catid>='"+global.useroffice_category+"' ";
            //connection = new MySqlConnection(mysqlcon.conn);
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();


                office_cat.DataSource = ds.Tables[0];
                office_cat.ValueMember = "office_catid";
                office_cat.DisplayMember = "officenamenep";

                sqlcon.con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }
        public void officename()
        {
            // MessageBox.Show("Working");
            // //string connetionString = null;
            // //SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            //connetionString = mysqlcon.conn.ToString();
           
          
                sql = "SELECT csioid,  concat(csioNepNm,' ',officeaddress) As 'csioNepNm' FROM setup_csio WHERE office_cat=@officecat AND provinceid=@province";
           
            //connection = new MySqlConnection(mysqlcon.conn);
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
                adapter.SelectCommand.Parameters.AddWithValue("@officecat", office_cat.SelectedValue.ToString());
                adapter.SelectCommand.Parameters.AddWithValue("@province", '4');
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();


                office_name.DataSource = ds.Tables[0];
                office_name.ValueMember = "csioid";
                office_name.DisplayMember = "csioNepNm";
                if (global.useroffice_category == "4")
                {
                    office_name.SelectedValue = global.user_officeid;
                    office_name.Enabled = false;
                }

                sqlcon.con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }
        public void setupfaat()
        {
            // MessageBox.Show("Working");
            // //string connetionString = null;
            // //SqlConnection connection;
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            string sql = null;
            //connetionString = mysqlcon.conn.ToString();


            sql = "SELECT faat_id,  faat_name FROM setup_faat ";

            //connection = new MySqlConnection(mysqlcon.conn);
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                command = new MySqlCommand(sql, sqlcon.con);
                adapter.SelectCommand = command;
               
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();


                faat.DataSource = ds.Tables[0];
                faat.ValueMember = "faat_id";
                faat.DisplayMember = "faat_name";
                

                sqlcon.con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }

        }


        private void adduser_Load(object sender, EventArgs e)
        {
            DisplayData();
            //usertypeauto();

            userssetting();
            usersstatus();
            officecategory();
           // officename();
            groupPersonal.Enabled = false;
            groupLogin.Enabled = false;
            save.Enabled = false;
         officename();
            setupfaat();

            cleardata();
            // update.Enabled = false;
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupPersonal.Enabled = true;
            groupLogin.Enabled = true;
            save.Enabled = true;
            //update.Enabled = false;
            savemode = "add";
            office_cat.Enabled = true;
            
            username.ReadOnly = false;
           // MessageBox.Show(global.user_officeid);
            if (global.useroffice_category == "3")
            {
                office_name.SelectedValue = global.user_officeid.ToString();

                office_name.Enabled = false;
            }
            else
            {
                office_name.Enabled = true;
            }
           // menugrid.DataSource = null;
            actiongrid.DataSource = null;
            menugrid.DataSource = null;
            menugrid.Columns.Clear();
            menugrid.Rows.Clear();
            DisplayMenu(office_cat.SelectedValue.ToString());
            DisplayAction();
        }
        private DataGridViewCell getCellWithInvalidData(DataGridView dgv, int[] fldPos, bool[] isChkFld, string[] fldTypes, bool isIgnoreWholeBlank = true)
        {
            DataGridViewCell errCell;

            for (int ii = 0; ii < dgv.Rows.Count; ii++)
            {
                //IGNORE IF WHOLE ROW IS BLANK
                if (isIgnoreWholeBlank == true)
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
                            DataGridViewComboBoxCell dgcb = (DataGridViewComboBoxCell)errCell;
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
       
        public void addpermission()
        {
           
            try
            {
                String uid = global.getSingleDataFromTable("SELECT userid FROM login WHERE username='" + username.Text + "'");
              //  global.sync_dblog("user_permission_assign", uid.ToString(), "INSERT", uid.ToString(), "userid");
                //   MessageBox.Show("Inside"+uid.ToString());
                int inserted = 0;
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                int n = 0;
                foreach (DataGridViewRow row in menugrid.Rows)
                {
                    n = n + 1;
                   // bool isSelected = Convert.ToBoolean(row.Cells["menupermission"].Value);
                  //  if (isSelected)
                  //  {
                        

                       
                       // MessageBox.Show(uid.ToString());
                        using (MySqlCommand cmd = new MySqlCommand("INSERT INTO user_permission_assign(user_permissionid, `userid`, `menuid`, `access_permission`, `assignby_user`, `assignnep_date`,office_id) VALUES(@userperid,@userid,@menuid, @permission,@assignby,@assigndate,@officeid)", sqlcon.con))
                        {
                        cmd.Parameters.AddWithValue("@userperid", uid.ToString()+n.ToString("000"));
                        cmd.Parameters.AddWithValue("@userid", uid);
                            cmd.Parameters.AddWithValue("@menuid", row.Cells["submenu_id"].Value);
                            cmd.Parameters.AddWithValue("@permission", row.Cells["menupermission"].Value);
                            cmd.Parameters.AddWithValue("@assignby", global.username);
                            cmd.Parameters.AddWithValue("@assigndate", global.nepalidate);
                        cmd.Parameters.AddWithValue("@officeid", global.csiodist);
                        // sqlcon.con.Open();
                        cmd.ExecuteNonQuery();
                            
                        }

                        inserted++;
                    }
             //   }

                if (inserted > 0)
                {
                  //  global.sync_dblog("user_permission_assign", uid.ToString(), "INSERT", uid.ToString(), "userid");
                    MessageBox.Show(string.Format("{0} मेनु अधिकार थपियो ।", inserted), "मेनु अधिकार");
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            sqlcon.con.Close();
        }
        public void addActionpermission()
        {
            try
            {
                int inserted = 0;
                foreach (DataGridViewRow row in actiongrid.Rows)
                {
                    bool isSelected = Convert.ToBoolean(row.Cells["menuaction"].Value);
                    //if (isSelected)
                    //{
                        if (sqlcon.con.State == ConnectionState.Closed)
                        {
                            sqlcon.con.Open();
                        }

                       String uid = global.getSingleDataFromTable("SELECT userid FROM login WHERE username='" + username.Text.ToString() + "'");
                       // MessageBox.Show(uid.ToString());

                        using (MySqlCommand cmd = new MySqlCommand("INSERT INTO user_permission_actionassign( user_permissionid,`userid`, `menuid`, `access_permission`, `assignby_user`, `assignnep_date`,office_id) VALUES(@permissionid,@userid,@menuid, @permission,@assignby,@assigndate,@officeid)", sqlcon.con))
                        {
                        cmd.Parameters.AddWithValue("@permissionid", uid.ToString() + inserted.ToString("000"));
                        cmd.Parameters.AddWithValue("@userid", uid);
                            cmd.Parameters.AddWithValue("@menuid", row.Cells["user_operationid"].Value);
                            cmd.Parameters.AddWithValue("@permission", row.Cells["menuaction"].Value);
                            cmd.Parameters.AddWithValue("@assignby", global.username);
                            cmd.Parameters.AddWithValue("@assigndate", global.nepalidate);
                        cmd.Parameters.AddWithValue("@officeid", global.csiodist);
                        // sqlcon.con.Open();
                        cmd.ExecuteNonQuery();
                            sqlcon.con.Close();
                        }

                        inserted++;
                    }
              //  }

                if (inserted > 0)
                {
                    MessageBox.Show(string.Format("{0} कार्यावाहीअधिकार प्रदान गरियो ।", inserted), "कार्यावाही अधिकार");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void save_Click(object sender, EventArgs e)
        {
            if (savemode == "add")
            {
                addNewUser();
                addpermission();
                addActionpermission();
                cleardata();
            }
            else //savemode == "update"
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

                if (!menugrid.ReadOnly)
                {
                    flds = new string[] { "access_permission" };
                    fldp = new[] { 4 };
                  //  fldin = new[] { false, true, false, true };

                    ischf = new bool[] { false };
                    fldt = new string[] {"bool" };

                    tblname = "user_permission_assign";
                    dgv = menugrid;
                    AddUpdateDataFromDataGrid(tblname, dgv, flds, fldp, fldt);
                    
                }
                if (!actiongrid.ReadOnly)
                {
                    flds = new string[] { "access_permission" };
                    fldp = new[] { 4 };
                    //  fldin = new[] { false, true, false, true };

                    ischf = new bool[] { false };
                    fldt = new string[] { "bool" };

                    tblname = "user_permission_actionassign";
                    dgv = actiongrid;
                    AddUpdateDataFromDataGrid(tblname, dgv, flds, fldp, fldt);
                   
                }
                updateUser();
            }
                 
        }

        public void addNewUser()
        {
            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                if (username.Text == "")
                {
                    MessageBox.Show("युजर आईडी भर्नुहोला ।", "युजरआईडी");
                    username.Focus();
                }
                else if (name.Text == "")
                {
                    MessageBox.Show("युजरको नाम खाली भयो, कृपया नाम फेरी प्रविष्ट गरी सेभ गर्नुहोला ।", "युजर नाममा समस्या");
                    name.Focus();

                }
                else if (password.Text != repassword.Text)
                {
                    MessageBox.Show("पासवर्ड मिलेन, फेरी प्रयास गर्नुहोला", "पासवर्डमा मिलेन");
                    password.Focus();
                }
                else
                {
                    int transid = Convert.ToInt32(global.getSingleDataFromTable("select IFNULL(COUNT(industryregid),0) FROM setup_industryregid WHERE industryregid='7'"));
                    string usertransid;
                    if (transid == 0)
                    {
                        MySqlCommand qrs = new MySqlCommand("INSERT INTO setup_industryregid(industryregid,industryregno,fiscalyear,officeid,officedist)VALUES(@regid, @regno,@fy,@officeid,@officedist)");

                        qrs.CommandType = CommandType.Text;
                        qrs.Parameters.AddWithValue("@regid", "7");
                        qrs.Parameters.AddWithValue("@regno", "1");
                        qrs.Parameters.AddWithValue("@fy", global.fyid);
                        qrs.Parameters.AddWithValue("@officeid", global.csioid);
                        qrs.Parameters.AddWithValue("@officedist", global.csiodist);



                        qrs.Connection = sqlcon.con;

                        int ss;
                        ss = qrs.ExecuteNonQuery();


                    }

                    String uids = global.getSingleDataFromTable("SELECT industryregno FROM setup_industryregid WHERE industryregid='7'");
                    usertransid = global.csioid + global.fyid + uids;

                    MessageBox.Show(usertransid.ToString());


                    string st = status.SelectedValue.ToString();
                    string s = usertype.SelectedValue.ToString();
                    // string st = usertype.SelectedItem.ToString();

                    string pass = globalfunction.MD5Hash(password.Text);
                    //MessageBox.Show(s.ToString() + st.ToString());
                    //SqlCommand qr = new SqlCommand("insert into login (username,password,name,address,phone,status,usertype) values('" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + st + "','" + s + "' )", con);
                    MySqlCommand qr = new MySqlCommand("INSERT INTO login(userid,username,password,name,address,phone,status,usertype,udate,office_cat, officeid,pisno,faat)VALUES(@userid,@user, @passwd,@name,@address,@phone,@status,@usertype,@update,@officecat,@officeid,@pisno,@faat)");

                    qr.CommandType = CommandType.Text;
                    qr.Parameters.AddWithValue("@userid", usertransid.ToString());
                    qr.Parameters.AddWithValue("@user", username.Text);
                    qr.Parameters.AddWithValue("@passwd", pass.ToString());
                    qr.Parameters.AddWithValue("@name", name.Text);
                    qr.Parameters.AddWithValue("@address", address.Text);
                    qr.Parameters.AddWithValue("@phone", phone.Text);
                    qr.Parameters.AddWithValue("@status", st.ToString());
                    qr.Parameters.AddWithValue("@usertype", s.ToString());
                    qr.Parameters.AddWithValue("@update", global.nepalidate.ToString());
                    qr.Parameters.AddWithValue("@officecat", office_cat.SelectedValue.ToString());
                    qr.Parameters.AddWithValue("@officeid", office_name.SelectedValue.ToString());
                    qr.Parameters.AddWithValue("@pisno", pisno.Text);
                    qr.Parameters.AddWithValue("@faat", faat.SelectedValue.ToString());


                    qr.Connection = sqlcon.con;


                    int k = qr.ExecuteNonQuery();

                    if (k > 0)
                    {

                        String uid = global.getSingleDataFromTable("SELECT userid FROM login WHERE username='" + username.Text + "'");
                        global.sync_dblog("login", uid.ToString(), "INSERT", uid.ToString(), "userid");

                        MySqlCommand cmo = new MySqlCommand("update setup_industryregid SET industryregno= industryregno+1 WHERE industryregid=7", sqlcon.con);
                        cmo.CommandType = CommandType.Text;
                        //  cmo.Parameters.AddWithValue("@firmtype", firmtype.ToString());
                        // int tid = Convert.ToInt32(cmo.ExecuteScalar());
                        // cmo.Connection = sqlcon.con;

                        //int n = qr.ExecuteNonQuery();

                        int n = cmo.ExecuteNonQuery();

                        MessageBox.Show("नयाँ प्रयोगकर्ता थप भयो  ।");
                        sqlcon.con.Close();
                        dataGridView1.DataSource = null;
                        DisplayData();


                    }
                
                
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void updateUser()
        {
            DialogResult result1 = MessageBox.Show("प्रयोगकर्ताको बिवरण संशोधन गर्ने हो?",
                 "प्रयोगकर्ताको बिवरण संशोधन",
                 MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result1 == DialogResult.Yes)
            {
                string curdate = DateTime.Today.ToString();
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                if (password.Text != repassword.Text)
                {
                    MessageBox.Show("Password Mismatch, Try Again");
                    password.Focus();
                }
                else
                {
                    //MySqlCommand cmd = new MySqlCommand("UPDATE login SET username= @username,password= @password, full_name= @fullname, address= @address, contactno= @phone,status= @status,usertype= @usertype WHERE userid= '" + userid.Text + "'", mysqlcon.conn);
                    MySqlCommand cmd = new MySqlCommand("UPDATE login SET username= @username,password= @password, name= @fullname, address= @address, phone= GetUnicodeToNumber(@phone),status= @status,usertype= @usertype,office_cat=@officecat, officeid=@officeid,pisno=@pisno,faat=@faat WHERE username= @username AND officeid=@officeid", sqlcon.con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@uid", userid.Text);
                    cmd.Parameters.AddWithValue("@username", username.Text);
                    cmd.Parameters.AddWithValue("@password", globalfunction.MD5Hash(password.Text));
                    cmd.Parameters.AddWithValue("@usertype", usertype.SelectedValue.ToString());

                    cmd.Parameters.AddWithValue("@fullname", name.Text);
                    cmd.Parameters.AddWithValue("@address", address.Text);
                    cmd.Parameters.AddWithValue("@phone", phone.Text);
                    cmd.Parameters.AddWithValue("@status", status.SelectedValue.ToString());
                    //cmd.Parameters.AddWithValue("@usertype", usertype.SelectedItem);
                    cmd.Parameters.AddWithValue("@officecat", office_cat.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@officeid", office_name.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@pisno", pisno.Text);
                    cmd.Parameters.AddWithValue("@faat", faat.SelectedValue.ToString());
                    // cmd.Parameters.AddWithValue("@curdate", curdate.ToString());
                    cmd.Connection = sqlcon.con;


                    //int n = qr.ExecuteNonQuery();
                    int n = 0;
                    n = cmd.ExecuteNonQuery();

                    if (n > 0)
                    {
                        global.sync_dblog("login", userid.Text, "UPDATE", userid.Text, "userid");

                        sqlcon.con.Close();
                        dataGridView1.DataSource = null;
                        DisplayData();
                        cleardata();
                        MessageBox.Show("संशोधन सफल भयो ।", "संशोधन", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                        save.Enabled = false;
                        //save.Enabled = true;
                    }
                }
            }

        }
        private void AddUpdateDataFromDataGrid(string tblname, DataGridView dgv, string[] fields, int[] fieldpos, string[] fieldtypes, bool showMsg = true)
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
                        cmd.CommandText = "UPDATE " + tblname + " SET " + SqStmt + " WHERE user_permissionid=@id AND office_id=@officeid";
                        global.sync_dblog(tblname, dgv.Rows[ii].Cells[0].Value.ToString(), "UPDATE", dgv.Rows[ii].Cells[0].Value.ToString(), "user_permissionid");
                    }
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@id", dgv.Rows[ii].Cells[0].Value);
                    cmd.Parameters.AddWithValue("@officeid", global.csioid);

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
                        else if (fieldtypes[ff]=="bool") //boolean
                        {
                            if (fldval == "True")
                                fldval = "1";
                            else
                                fldval = "0";

                            cmd.Parameters.AddWithValue("@" + fields[ff], fldval);
                        }    
                        else    //not numeric - 
                        {
                            cmd.Parameters.AddWithValue("@" + fields[ff], fldval);
                        }
                    }
                    cmd.ExecuteNonQuery();
                    
                    //MessageBox.Show(cmd.CommandText.ToString());
                }

                if (showMsg == true) //it comes false if another save work is also to be checked e.g. investment chalu and sthir
                {
                  //  MessageBox.Show("सफलतापुर्वक सेभ गरियो ।", "सेभ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   // LockAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \n " + ex.Message);
            }

        }
    
           
                    private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            groupPersonal.Enabled = true;
            groupLogin.Enabled = true;
            save.Enabled = false;

            //update.Enabled = true;
            savemode = "update";
          //  update.Enabled = true;
            save.Enabled = false;
            int i;
            i = dataGridView1.CurrentRow.Index;
            menugrid.DataSource = null;
            menugrid.Columns.Clear();
            menugrid.Rows.Clear();
            userid.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            username.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            username.ReadOnly = true;
            name.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            address.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            phone.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();

            statusedit(dataGridView1.Rows[i].Cells[5].Value.ToString());
            //  usertype.SelectedItem = dataGridView1.Rows[i].Cells[5].Value.ToString();
            usertypeedit(dataGridView1.Rows[i].Cells[6].Value.ToString());
            pisno.Text = dataGridView1.Rows[i].Cells[7].Value.ToString();
            office_cat.Text = dataGridView1.Rows[i].Cells[8].Value.ToString();
            office_name.SelectedValue = dataGridView1.Rows[i].Cells[9].Value.ToString();
            faat.Text = dataGridView1.Rows[i].Cells[10].Value.ToString();
            office_cat.Enabled = false;
            office_name.Enabled = false;
            save.Enabled = true;
            DisplayMenu(office_cat.SelectedValue.ToString());
            DisplayEditMenu(userid.Text);
            DisplayEditActionMenu(userid.Text);
        }
        public void DisplayEditMenu(String office_category)
        {
            //  MessageBox.Show(office_category.ToString());
            try
            {
                //string columnname = "ministry";
                //if (office_category == "1")
                //{
                //    columnname = "ministry";
                //}
                //else if (office_category == "2")
                //{
                //    columnname = "nirdesanalaya";
                //}
                //else
                //{
                //    columnname = "gharelu";
                //}
                // MessageBox.Show(columnname.ToString());
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                menugrid.DataSource = null;
                menugrid.Columns.Clear();
                menugrid.Rows.Clear();
                MySqlDataAdapter qry = new MySqlDataAdapter("SELECT user_permissionid,userid,menuid, user_submenu.submenu_name As 'मेनु', `access_permission` As 'अधिकार' FROM user_permission_assign INNER JOIN user_submenu ON submenu_id=menuid WHERE userid ="+ office_category, sqlcon.con);
                DataTable tb = new DataTable();
                qry.Fill(tb);
                menugrid.DataSource = null;
                menugrid.DataSource = tb;
                menugrid.Columns[0].Visible = false;
                menugrid.Columns[1].Visible = false;
                menugrid.Columns[2].Visible = false;
                //  String data;
                //foreach (DataGridViewRow item in menugrid.Rows)
                //{
                //  //  MessageBox.Show(item.Cells[0].Value.ToString());
                //    foreach (DataRow row in tb.Rows)
                //{

                //        if (item.Cells[0].Value != null && item.Cells[0].Value.ToString() == row["menuid"].ToString())
                //        {
                //            item.Cells[3].Value = row["access_permission"];
                //            //((DataGridViewCheckBoxCell)item.Cells["menupermission"]).Value = checkmenu.Checked;
                //        }
                //        else
                //        {
                //            item.Cells[3].Value = false;
                //        }

                //    }

                menugrid.RefreshEdit();
               // }
                   
                //if (tb.Rows.Count > 0)
                //{
                //    menugrid.DataSource = tb;
                //    DataGridViewCheckBoxColumn datacheckbox = new DataGridViewCheckBoxColumn();
                //    datacheckbox.HeaderText‍ = "अधिकार";
                //    datacheckbox.Name = "menupermission";
                //    datacheckbox.Width = 30;
                //    menugrid.Columns.Insert(3, datacheckbox);

                //    //menugrid.Cells["menupermission"].Value = true;
                //    //datacheckbox.ValueType = true;

                //    //foreach (DataGridViewRow row in menugrid.Rows)
                //    //{
                //    //    ((DataGridViewCheckBoxCell)row.Cells["menupermission"]).Value = datacheckbox.checked;
                //    //    row.Cells["menupermission"].Value = true;
                //    //    //  bool isSelected = Convert.ToBoolean(row.Cells["menupermission"].Value);
                //    //    //if (isSelected)
                //    //    //{
                //    //    //    //message += Environment.NewLine;
                //    //    //    //message += row.Cells["Name"].Value.ToString();
                //    //    //}
                //    //}


                //    menugrid.Columns[0].Visible = false;
                //    menugrid.Columns[2].Visible = false;

                //}
                sqlcon.con.Close();




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void DisplayEditActionMenu(String office_category)
        {
            //  MessageBox.Show(office_category.ToString());
            try
            {
                //string columnname = "ministry";
                //if (office_category == "1")
                //{
                //    columnname = "ministry";
                //}
                //else if (office_category == "2")
                //{
                //    columnname = "nirdesanalaya";
                //}
                //else
                //{
                //    columnname = "gharelu";
                //}
                // MessageBox.Show(columnname.ToString());
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }
                actiongrid.DataSource = null;
                actiongrid.Columns.Clear();
                actiongrid.Rows.Clear();
                MySqlDataAdapter qry = new MySqlDataAdapter("SELECT user_permissionid,userid,menuid, user_permission_operation.operationname As 'मेनु', `access_permission` As 'अधिकार' FROM user_permission_actionassign INNER JOIN user_permission_operation ON user_operationid=menuid WHERE userid =" + office_category, sqlcon.con);
                DataTable tb = new DataTable();
                qry.Fill(tb);
                actiongrid.DataSource = null;
                actiongrid.DataSource = tb;
                actiongrid.Columns[0].Visible = false;
                actiongrid.Columns[1].Visible = false;
                actiongrid.Columns[2].Visible = false;


                actiongrid.RefreshEdit();
              
                sqlcon.con.Close();




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void checkboxclicked(CheckBox checkmenu)
        {
          
           
            foreach (DataGridViewRow Row in menugrid.Rows)
                ((DataGridViewCheckBoxCell)Row.Cells["menupermission"]).Value= checkmenu.Checked;
            menugrid.RefreshEdit();
        }
        private void office_cat_SelectedIndexChanged(object sender, EventArgs e)
        {
            officename();
            menugrid.DataSource = null;
            menugrid.Columns.Clear();
            menugrid.Rows.Clear();
            // actiongrid.DataSource = null;
            DisplayMenu(office_cat.SelectedValue.ToString());
        }

        private void checkmenu_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkmenu_Click(object sender, EventArgs e)
        {
            checkboxclicked(checkmenu);
        }

        private void checkAction_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in actiongrid.Rows)
                ((DataGridViewCheckBoxCell)Row.Cells["menuaction"]).Value = checkAction.Checked;
            menugrid.RefreshEdit();
        }
    }
}
