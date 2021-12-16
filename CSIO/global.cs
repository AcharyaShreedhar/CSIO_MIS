using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
namespace CSIO
{

    public class global
    {
        public static string userid;
        public static string username;
        public static string nepalidate;
        public static string todaynepslash;
        public static string csioffice;
        public static string fy;
        public static string office;
        public static string status;
        public static string csioid;
        public static string csiodist;
        public static string usertype;
        public static string fyid;
        public static string configid;
        public static int myProvinceId = 4; //GANDAKI BY DEFAULT
        public static int myDistrictId;
        public static string useroffice_category;
        public static string user_officeid;
        //public static string convertNumToUnicode(string num)
        //{
        //    string cvr;
        //    cvr = num.Replace('०', '0').Replace('१', '1').Replace('२', '2').Replace('३', '3').Replace('४', '4').Replace('५', '5').Replace('६', '6').Replace('७', '7').Replace('८', '8').Replace('९', '9');
        //    return cvr;
        //}

        public static string convertUnicodeToNum(string num)
        {
            string cvr;
            cvr = num.Replace('०', '0').Replace('१', '1').Replace('२', '2').Replace('३', '3').Replace('४', '4').Replace('५', '5').Replace('६', '6').Replace('७', '7').Replace('८', '8').Replace('९', '9');
            return cvr;
        }

        public static string convertNumToUnicode(string num)
        {
            string cvr;
            cvr = num.Replace('0', '०').Replace('1', '१').Replace('2', '२').Replace('3', '३').Replace('4', '४').Replace('5', '५').Replace('6', '६').Replace('7', '७').Replace('8', '८').Replace('9', '९');
            return cvr;
        }

        //Changed separate param1, param2 to param strings which are splitted - comma separated
        public static string[] getSingleRowFromProcedure(string procname, string myparams, string myvalues, string conType = "local")
        {
            string[] data = { };

            string[] myparsArr = myparams.Split(',');
            string[] myvalsArr = myvalues.Split(',');

            //new addition of conType -- so that it can called for SERVER as well
            MySqlConnection myCon;
            if (conType == "remote")
                myCon = sqlcononline.cononline;
            else
                myCon = sqlcon.con;

            if (myCon.State == ConnectionState.Closed)
                myCon.Open();

            //comma separated parameters added here
            MySqlDataAdapter da = new MySqlDataAdapter("CALL " + procname + " (" + myparams + ")", myCon);

            //adding separate parameters and their values
            for(int ii=0;ii<myparsArr.Length;ii++)
            {
                da.SelectCommand.Parameters.AddWithValue(myparsArr[ii].ToString(), myvalsArr[ii].ToString());
            }
            
            //da.SelectCommand.Parameters.AddWithValue(param2, value2);

            DataTable tb = new DataTable();
            da.Fill(tb);
            if(tb.Rows.Count>0)
                data = tb.Rows[0].ItemArray.Select(x => x.ToString()).ToArray(); ;

            return data;
            //SELECT officeid , officeDist, officeNm, office_address, officeunicodename,  GetNumberToUnicode(phone) As 'phone', GetNumberToUnicode(fax) As 'fax', email,departmentname,ministryname,govtname,provincename FROM setup_office WHERE isCur=isCurs
        }

        public static string[] getSingleRowFromTable(string sqlSt, string conType = "local")
        {
            string[] data= { };

            //new addition of conType -- so that it can called for SERVER as well
            MySqlConnection myCon;
            if (conType == "remote")
                myCon = sqlcononline.cononline;
            else
                myCon = sqlcon.con;

            if (myCon.State == ConnectionState.Closed)
                myCon.Open();

            MySqlDataAdapter da = new MySqlDataAdapter(sqlSt, myCon);

            DataTable tb = new DataTable();
            da.Fill(tb);
            if (tb.Rows.Count > 0)
                data = tb.Rows[0].ItemArray.Select(x => x.ToString()).ToArray(); ;

            return data;
            //SELECT officeid , officeDist, officeNm, office_address, officeunicodename,  GetNumberToUnicode(phone) As 'phone', GetNumberToUnicode(fax) As 'fax', email,departmentname,ministryname,govtname,provincename FROM setup_office WHERE isCur=isCurs
        }

        public static string[] getSingleFieldArrayFromTable(string sqlSt,string conType="local")
        {
            string[] data;

            //new addition of conType -- so that it can called for SERVER as well
            MySqlConnection myCon;
            if (conType == "remote")
                myCon = sqlcononline.cononline;
            else
                myCon = sqlcon.con;

            if (myCon.State == ConnectionState.Closed)
                myCon.Open();

            MySqlDataAdapter da = new MySqlDataAdapter(sqlSt, myCon);
            DataTable tb = new DataTable();
            da.Fill(tb);
            data = new string[tb.Rows.Count];

            for (int ii=0;ii<tb.Rows.Count;ii++)
            {
                data[ii] = tb.Rows[ii].ItemArray[0].ToString();
            }

            return data;
            //SELECT officeid , officeDist, officeNm, office_address, officeunicodename,  GetNumberToUnicode(phone) As 'phone', GetNumberToUnicode(fax) As 'fax', email,departmentname,ministryname,govtname,provincename FROM setup_office WHERE isCur=isCurs
        }

        public static string getSingleDataFromTable(string sqlSt,string myparams="", string myvalues="",string conType="local")
        {
            string data="";

            //new addition of conType -- so that it can called for SERVER as well
            MySqlConnection myCon;
            if (conType == "remote")
                myCon = sqlcononline.cononline;
            else
                myCon = sqlcon.con;

            if (myCon.State == ConnectionState.Closed)
                myCon.Open();

            MySqlDataAdapter da = new MySqlDataAdapter(sqlSt,myCon);
            DataTable tb = new DataTable();

            //if there are parameters
            if(myparams!="" && myvalues!="")
            {
                string[] myparsArr = myparams.Split(',');
                string[] myvalsArr = myvalues.Split(',');

                //adding separate parameters and their values
                for (int ii = 0; ii < myparsArr.Length; ii++)
                {
                    da.SelectCommand.Parameters.AddWithValue(myparsArr[ii].ToString(), myvalsArr[ii].ToString());
                }
            }

            da.Fill(tb);
            if (tb.Rows.Count > 0)
            {
                DataRow dr = tb.Rows[0];
                data = dr[0].ToString();
                //tb.Rows[0].ItemArray.Select(x => x.ToString()).ToArray(); ;
            }
            return data;
        }

        public static DataTable getDataTableFromTable(string sqlSt, string myparams = "", string myvalues = "", string conType = "local")
        {
            //new addition of conType -- so that it can called for SERVER as well
            MySqlConnection myCon;
            if (conType == "remote")
                myCon = sqlcononline.cononline;
            else
                myCon = sqlcon.con;

            if (myCon.State == ConnectionState.Closed)
                myCon.Open();

            MySqlDataAdapter da = new MySqlDataAdapter(sqlSt, myCon);

            //if there are parameters
            if (myparams != "" && myvalues != "")
            {
                string[] myparsArr = myparams.Split(',');
                string[] myvalsArr = myvalues.Split(',');

                //adding separate parameters and their values
                for (int ii = 0; ii < myparsArr.Length; ii++)
                {
                    da.SelectCommand.Parameters.AddWithValue(myparsArr[ii].ToString(), myvalsArr[ii].ToString());
                }
            }


            DataTable tb = new DataTable(); //source
            //DataTable tb2 = new DataTable(); //destination after validation

            //for(int rr=0;rr<tb.Rows.Count;rr++)
            //{
            //    for(int cc=0; cc<tb.Columns.Count;cc++)
            //    {

            //    }
            //}

            da.Fill(tb);
            return tb;
        }

        public static string[] getSingleRowFromRemoteTable(string sqlSt, string conType = "remote")
        {
            string[] data = { };

            //new addition of conType -- so that it can called for SERVER as well
            MySqlConnection myCon;
            if (conType == "remote")
                myCon = sqlcononline.cononline;
            else
                myCon = sqlcon.con;

            if (myCon.State == ConnectionState.Closed)
                myCon.Open();

            MySqlDataAdapter da = new MySqlDataAdapter(sqlSt, myCon);

            DataTable tb = new DataTable();
            da.Fill(tb);
            if (tb.Rows.Count > 0)
                data = tb.Rows[0].ItemArray.Select(x => x.ToString()).ToArray(); ;

            return data;
            //SELECT officeid , officeDist, officeNm, office_address, officeunicodename,  GetNumberToUnicode(phone) As 'phone', GetNumberToUnicode(fax) As 'fax', email,departmentname,ministryname,govtname,provincename FROM setup_office WHERE isCur=isCurs
        }

        public static void fillCombo(ComboBox cbo, string sql, string dismem, string valmem, string firstItem="",Boolean showAutoComplete=false)
        {
            MySqlCommand command;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

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
                sqlcon.con.Close();

                if (firstItem != "")
                {
                    DataRow dr = ds.Tables[0].NewRow();
                    dr[dismem] = firstItem;
                    dr[valmem] = 0;
                    ds.Tables[0].Rows.InsertAt(dr, 0);
                }


               

                cbo.DataSource = ds.Tables[0];
                cbo.ValueMember = valmem;
                cbo.DisplayMember = dismem;
                
                //if autocomplete is also to be displayed
                if (showAutoComplete == true)
                {
                    AutoCompleteStringCollection autoComp = new AutoCompleteStringCollection();

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        autoComp.Add(row[1].ToString());
                    }
                    cbo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cbo.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    cbo.AutoCompleteCustomSource = autoComp;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filling " + cbo.Name + " \n" + ex);
            }
        }

        public static void sync_dblog(string tablename, string industryid, string actiondb,string transid,string conditionalfield)
        {
           

            try
            {
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }// con.Open();


                string curdate = DateTime.Now.ToString();

                MySqlCommand cmd = new MySqlCommand("INSERT INTO sync_dblog( operation, tablename, industryid, updateduser,  updatednepdate, sync_status,transactionid,conditional_field) VALUES(@operation,@tablename,@industryid,@user,@nepdate,@status,@transid,@condfield)");

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@operation", actiondb.ToString());
                cmd.Parameters.AddWithValue("@tablename",tablename.ToString());
                cmd.Parameters.AddWithValue("@industryid", industryid.ToString());
                cmd.Parameters.AddWithValue("@transid", transid.ToString());
                // cmd.Parameters.Add(new SqlParameter("@curdate", curdate));
                cmd.Parameters.Add(new MySqlParameter("@nepdate", global.nepalidate));
                cmd.Parameters.Add(new MySqlParameter("@user", global.username));
                cmd.Parameters.AddWithValue("@condfield", conditionalfield.ToString());

                cmd.Parameters.AddWithValue("@status", 0);
                cmd.Connection = sqlcon.con;

                int n = cmd.ExecuteNonQuery();

                if (n > 0)
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting Sync_log "  + ex);
            }
        }
        public static void fullsync_operation()
        {
            try
            {
                int no_op = 0;
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }// con.Open();
                if (sqlcononline.cononline.State == ConnectionState.Closed)
                {
                    sqlcononline.cononline.Open();
                }// con.Open();
                MySqlDataAdapter qry = new MySqlDataAdapter("SELECT sync_id, tablename, sync_fieldname FROM sync_dbname", sqlcon.con);
                // qry.SelectCommand.Parameters.AddWithValue("@darta", label5.Text);
                DataTable tb = new DataTable();
                qry.Fill(tb);
                //dataGridView1.DataSource = tb;
                //newmember = "प्रोपाइटर विवरण:";
                string sync_id;
                string operation;
                string tablename;
                string industryid;
                string transactionid;
                string conditional_field;
                foreach (DataRow row in tb.Rows)
                {
                    sync_id = row["sync_id"].ToString();
                    //operation = row["operation"].ToString();
                    tablename = row["tablename"].ToString();
                 //   MessageBox.Show(tablename.ToString());
                    // industryid = row["industryid"].ToString();
                    //  transactionid = row["transactionid"].ToString();
                    conditional_field = row["sync_fieldname"].ToString();
                  //  MessageBox.Show(conditional_field.ToString());
                    // Getting ID From Table
                    MySqlDataAdapter qrys = new MySqlDataAdapter("SELECT " + conditional_field.ToString() + " FROM " + tablename.ToString() , sqlcon.con);
                   // MessageBox.Show(qrys.ToString());
                    // qry.SelectCommand.Parameters.AddWithValue("@darta", label5.Text);
                    DataTable tbs = new DataTable();
                    qrys.Fill(tbs);
                    //dataGridView1.DataSource = tb;
                    //newmember = "प्रोपाइटर विवरण:";
                    //string columnconditionvalue;
                    //string operation;
                    //string tablename;
                    //string industryid;
                    //string transactionid;
                    //string conditional_field;
                    string[] colNames, colVals;
                    int numcol;
                    colNames = getSingleFieldArrayFromTable("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + tablename + "'");
                    foreach (DataRow rows in tbs.Rows)
                    {
                        transactionid = rows[conditional_field.ToString()].ToString();
                        //string[] columncheck;
                        //columncheck = getSingleRowFromTable("SELECT * FROM " + tablename + " WHERE " + conditional_field + " = '" + transactionid + "'");

                        string sqST = "";
                        string sqVal = "";
                        //  MessageBox.Show(colVals[0].ToString());

                        MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM " + tablename + " WHERE " + conditional_field + " = '" + transactionid + "'", sqlcononline.cononline);

                        DataTable tbst = new DataTable();
                        da.Fill(tbst);
                        if (tbst.Rows.Count > 0)
                          //  if (colVals.Length > 0 )
                        {
                            colVals = getSingleRowFromRemoteTable("SELECT * FROM " + tablename + " WHERE " + conditional_field + " = '" + transactionid + "'");
                            //string sqST = "";
                            //string sqVal = "";
                          //  MessageBox.Show("Inside Update");
                            //Update Operation if record Exist in remote Server
                            sqST = "UPDATE " + tablename + " SET ";
                            //sqVal = "VALUES (";

                            for (int ii = 0; ii < colNames.Length; ii++)
                            {
                                
                                sqST += colNames[ii] + " = '" + colVals[ii] + "'";

                                if (ii < colNames.Length - 1)
                                {
                                    sqST += ", ";
                                }
                            }

                            sqST += " WHERE " + conditional_field + " = '" + transactionid + "'";
                        }
                        else
                        {
                           // MessageBox.Show("Table Name="+tablename.ToString() + ": ID=" + transactionid.ToString());
                            colVals = getSingleRowFromTable("SELECT * FROM " + tablename + " WHERE " + conditional_field + " = '" + transactionid + "'");
                           
                           // MessageBox.Show("Inside Insert");
                            //Insert Record
                            sqST = "INSERT INTO " + tablename + "(";
                            sqVal = "VALUES (";

                            for (int ii = 0; ii < colNames.Length; ii++)
                            {
                                sqST += colNames[ii];
                                sqVal += "'" + colVals[ii] + "'";

                                if (ii < colNames.Length - 1)
                                {
                                    sqST += ", ";
                                    sqVal += ", ";
                                }
                            }

                            sqST += ")";
                            sqVal += ")";

                            sqST += sqVal;
                        }
                        MySqlCommand qrs = new MySqlCommand(sqST, sqlcononline.cononline);

                        
                        //int n = qr.ExecuteNonQuery();
                        int n = qrs.ExecuteNonQuery();

                        if (n > 0)
                        {
                            no_op++;
                            //MessageBox.Show(no_op.ToString());
                        }


                    }
                }
            
                    // End Value Extracting

                    
                  

                    //if there is no record of given condition, skip current execution and continue with next iteration
                   
                   
                        MySqlCommand cmd = new MySqlCommand("UPDATE sync_dblog SET sync_status=true ", sqlcon.con);
                        int m = cmd.ExecuteNonQuery();

                        if (m > 0)
                        {
                            
                           // MessageBox.Show(no_op.ToString());
                        }


                //UPDATING PUSH DATE IN SYNC_COMPLETE TABLE

                MySqlCommand cmdd;
                int totC = 0;

                if (sqlcononline.cononline.State == ConnectionState.Open)
                    sqlcononline.cononline.Close();

                sqlcononline.cononline.Open();

                MySqlDataAdapter daON = new MySqlDataAdapter("SELECT count(*) FROM sync_completed WHERE csioid='" + global.csioid + "'", sqlcononline.cononline);

                DataTable tbON = new DataTable();

                daON.Fill(tbON);
                totC = tbON.Rows.Count;

                if (totC == 0)
                {
                    //insert
                    cmdd = new MySqlCommand("INSERT INTO sync_completed(csioid,push_date) VALUES ('" + global.csioid + "',current_timestamp)", sqlcononline.cononline);
                    cmdd.ExecuteNonQuery();
                }
                else
                {
                    //update
                    cmdd = new MySqlCommand("UPDATE sync_completed SET push_date = current_timestamp  WHERE csioid='" + global.csioid + "'", sqlcononline.cononline);
                    cmdd.ExecuteNonQuery();
                }

                // if (n > 0)
                // {
                MessageBox.Show(no_op.ToString() + " रेकर्ड सर्भरमा सफलतापुर्वक सिन्क्रोनाइज भयो ।");
                // }
            }
            catch (Exception ex)
            {
                MessageBox.Show("सर्भरमा रेकर्ड सिन्क्रोनाइज हुन सकेन । \n\n" + ex);
            }

        }


        public static void sync_operation()
        {
            try
            {
                int no_op=0;
                if (sqlcon.con.State == ConnectionState.Closed)
                {
                    sqlcon.con.Open();
                }// con.Open();
                if (sqlcononline.cononline.State == ConnectionState.Closed)
                {
                    sqlcononline.cononline.Open();
                }// con.Open();
                MySqlDataAdapter qry = new MySqlDataAdapter("SELECT     `sync_id`, `operation`, `tablename`, `industryid`, `updateduser`, `updateddate`, `updatednepdate`, `sync_status`, `transactionid`, `conditional_field` FROM `sync_dblog` WHERE     (sync_status = false)", sqlcon.con);
               // qry.SelectCommand.Parameters.AddWithValue("@darta", label5.Text);
                DataTable tb = new DataTable();
                qry.Fill(tb);
                //dataGridView1.DataSource = tb;
                //newmember = "प्रोपाइटर विवरण:";
                string sync_id;
                string operation;
                string tablename;
                string industryid;
                string transactionid;
                string conditional_field;
                foreach (DataRow row in tb.Rows)
                {
                    sync_id = row["sync_id"].ToString();
                    operation = row["operation"].ToString();
                    tablename = row["tablename"].ToString();
                    industryid = row["industryid"].ToString();
                    transactionid = row["transactionid"].ToString();
                    conditional_field = row["conditional_field"].ToString();

                    string[] colNames, colVals;
                    colNames = getSingleFieldArrayFromTable("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + tablename + "'");
                    colVals = getSingleRowFromTable("SELECT * FROM " + tablename + " WHERE " + conditional_field + " = '" + transactionid + "'");

                    //if there is no record of given condition, skip current execution and continue with next iteration
                    if (colVals.Length == 0)
                        continue;

                    string sqST = "";
                    string sqVal = "";
                    
                    if (operation=="INSERT")
                    {
                        sqST = "INSERT INTO " + tablename + "(";
                        sqVal = "VALUES (";

                        for (int ii = 0; ii < colNames.Length; ii++)
                        {
                            MessageBox.Show("Field Name:" + colNames[ii] + ", values: " + colVals[ii]);
                            sqST += colNames[ii];
                            sqVal += "'" + colVals[ii] + "'";

                            if (ii < colNames.Length - 1)
                            {
                                sqST += ", ";
                                sqVal += ", ";
                            }
                        }

                        sqST += ")";
                        sqVal += ")";

                        sqST += sqVal;
                        //MessageBox.Show(sqST);

                    }

                    else if (operation == "UPDATE")
                    {

                        sqST = "UPDATE " + tablename + " SET ";
                        //sqVal = "VALUES (";

                        for (int ii = 0; ii < colNames.Length; ii++)
                        {
                            sqST += colNames[ii] + " = '" + colVals[ii] + "'";

                            if (ii < colNames.Length - 1)
                            {
                                sqST += ", ";
                            }
                        }

                        sqST += " WHERE " + conditional_field + " = '" + transactionid + "'";

                        //MessageBox.Show(sqST);


                    }
                    else if (operation == "DELETE")
                    {
                        sqST = "DELETE FROM " + tablename + " WHERE " + conditional_field  + " = '" + transactionid + "'";
                    }
                    MySqlCommand qrs = new MySqlCommand(sqST,sqlcononline.cononline);


                    //int n = qr.ExecuteNonQuery();
                    int n = qrs.ExecuteNonQuery();

                    if (n > 0)
                    {
                        MySqlCommand cmd = new MySqlCommand("UPDATE sync_dblog SET sync_status=true WHERE sync_id='"+ sync_id+"'", sqlcon.con);
                        int m = cmd.ExecuteNonQuery();

                        if (m > 0)
                        {
                            no_op++;
                            //MessageBox.Show(no_op.ToString());
                        }
                    }
                }


                //UPDATING PUSH DATE IN SYNC_COMPLETE TABLE

                MySqlCommand cmdd;
                int totC=0;

                if (sqlcononline.cononline.State == ConnectionState.Open)
                    sqlcononline.cononline.Close();

                sqlcononline.cononline.Open();

                MySqlDataAdapter daON = new MySqlDataAdapter("SELECT count(*) FROM sync_completed WHERE csioid='" + global.csioid + "'", sqlcononline.cononline);

                DataTable tbON = new DataTable();

                daON.Fill(tbON);
                totC = tbON.Rows.Count;

                if (totC==0)
                {
                    //insert
                    cmdd = new MySqlCommand("INSERT INTO sync_completed(csioid,push_date) VALUES ('" + global.csioid + "',current_timestamp)",sqlcononline.cononline);
                    cmdd.ExecuteNonQuery();
                }
                else
                {
                    //update
                    cmdd = new MySqlCommand("UPDATE sync_completed SET push_date = current_timestamp  WHERE csioid='" + global.csioid + "'",sqlcononline.cononline);
                    cmdd.ExecuteNonQuery();
                }

                // if (n > 0)
                // {
                        MessageBox.Show(no_op.ToString() + " रेकर्ड सर्भरमा सफलतापुर्वक सिन्क्रोनाइज भयो ।");
                // }
            }
            catch (Exception ex)
            {
                MessageBox.Show("सर्भरमा रेकर्ड सिन्क्रोनाइज हुन सकेन । \n\n" + ex);
            }
            
        }


        public static void sync_updateFromServer()
        {
            try
            {
                int no_op = 0;

                if (sqlcon.con.State == ConnectionState.Closed)
                    sqlcon.con.Open();

                if (sqlcononline.cononline.State == ConnectionState.Closed)
                    sqlcononline.cononline.Open();

                MySqlDataAdapter qry = new MySqlDataAdapter("SELECT `sync_id`, `operation`, `tablename`, `industryid`, `updateduser`, `updateddate`, `updatednepdate`, `sync_status`, `transactionid`, `conditional_field` FROM `sync_dblog` WHERE     (sync_status = false)", sqlcononline.cononline);

                DataTable tb = new DataTable();
                qry.Fill(tb);

                string sync_id;
                string operation;
                string tablename;
                string industryid;
                string transactionid;
                string conditional_field;
                foreach (DataRow row in tb.Rows)
                {
                    sync_id = row["sync_id"].ToString();
                    operation = row["operation"].ToString();
                    tablename = row["tablename"].ToString();
                    industryid = row["industryid"].ToString();
                    transactionid = row["transactionid"].ToString();
                    conditional_field = row["conditional_field"].ToString();

                    string[] colNames, colVals;
                    colNames = getSingleFieldArrayFromTable("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + tablename + "'","remote");
                    colVals = getSingleRowFromTable("SELECT * FROM " + tablename + " WHERE " + conditional_field + " = '" + transactionid + "'","remote");

                    //if there is no record of given condition, skip current execution and continue with next iteration
                    if (colVals.Length == 0)
                        continue;

                    string sqST = "";
                    string sqVal = "";

                    if (operation == "INSERT")
                    {
                        sqST = "INSERT INTO " + tablename + "(";
                        sqVal = "VALUES (";

                        for (int ii = 0; ii < colNames.Length; ii++)
                        {
                            sqST += colNames[ii];
                            sqVal += "'" + colVals[ii] + "'";

                            if (ii < colNames.Length - 1)
                            {
                                sqST += ", ";
                                sqVal += ", ";
                            }
                        }

                        sqST += ")";
                        sqVal += ")";

                        sqST += sqVal;
                        //MessageBox.Show(sqST);

                    }

                    else if (operation == "UPDATE")
                    {

                        sqST = "UPDATE " + tablename + " SET ";
                        //sqVal = "VALUES (";

                        for (int ii = 0; ii < colNames.Length; ii++)
                        {
                            sqST += colNames[ii] + " = '" + colVals[ii] + "'";

                            if (ii < colNames.Length - 1)
                            {
                                sqST += ", ";
                            }
                        }

                        sqST += " WHERE " + conditional_field + " = '" + transactionid + "'";

                        //MessageBox.Show(sqST);


                    }
                    else if (operation == "DELETE")
                    {
                        sqST = "DELETE FROM " + tablename + " WHERE " + conditional_field + " = '" + transactionid + "'";
                    }
                    MySqlCommand qrs = new MySqlCommand(sqST, sqlcon.con);


                    //int n = qr.ExecuteNonQuery();
                    int n = qrs.ExecuteNonQuery();

                    if (n > 0)
                        no_op++;
                }


                //UPDATING PUSH DATE IN SYNC_COMPLETE TABLE

                MySqlCommand cmdd;
                int totC = 0;

                if (sqlcononline.cononline.State == ConnectionState.Open)
                    sqlcononline.cononline.Close();

                sqlcononline.cononline.Open();

                MySqlDataAdapter daON = new MySqlDataAdapter("SELECT count(*) FROM sync_completed WHERE csioid='" + global.csioid + "'", sqlcononline.cononline);

                DataTable tbON = new DataTable();

                daON.Fill(tbON);
                totC = tbON.Rows.Count;

                if (totC == 0)
                {
                    //insert
                    cmdd = new MySqlCommand("INSERT INTO sync_completed(csioid,pull_date) VALUES ('" + global.csioid + "',current_timestamp)", sqlcononline.cononline);
                    cmdd.ExecuteNonQuery();
                }
                else
                {
                    //update
                    cmdd = new MySqlCommand("UPDATE sync_completed SET pull_date = current_timestamp  WHERE csioid='" + global.csioid + "'", sqlcononline.cononline);
                    cmdd.ExecuteNonQuery();
                }


                // if (n > 0)
                // {
                MessageBox.Show(no_op.ToString() + " सेटिङ सर्भरबाट सफलतापुर्वक अपडेट भयो ।");
                // }
            }
            catch (Exception ex)
            {
                MessageBox.Show("सर्भरबाट सेटिङ अपडेट हुन सकेन । \n\n" + ex);
            }

        }



        public static int CountNumToBeSync(string fromServer)
        {
            try
            {
                int totSync = 0;
                string val = "";

                if (fromServer == "local")
                {
                    val = global.getSingleDataFromTable("SELECT count(sync_id) FROM sync_dblog WHERE sync_status=0");
                    if (string.IsNullOrEmpty(val))
                        val = "0";

                    totSync = Convert.ToInt32(val);
                }

                else //FROM SERVER

                {
                    if (sqlcononline.cononline.State == ConnectionState.Open)
                        sqlcononline.cononline.Close();

                    sqlcononline.cononline.Open();

                    MySqlDataAdapter daON = new MySqlDataAdapter("SELECT count(sync_id) FROM sync_dblog WHERE sync_status=0 && updateddate > (select sc.pull_date FROM sync_completed as sc WHERE sc.csioid='" + csioid +"')", sqlcononline.cononline);
                    DataTable tbON = new DataTable();

                    daON.Fill(tbON);
                    if (tbON.Rows.Count > 0)
                    {
                        DataRow drON = tbON.Rows[0];
                        val = drON[0].ToString();
                    }
                }

                if (string.IsNullOrEmpty(val))
                    val = "0";

                totSync = Convert.ToInt32(val);

                return totSync;
            }
            catch
            {
                return 0;
            }
            finally
            {

            }
        }

        //This is same function as above with different first argument as datagridviewcombocolumn
        //public static void fillCombo(DataGridViewComboBoxColumn cbo, string sql, string dismem, string valmem, string firstItem = "", Boolean showAutoComplete = false, AutoCompleteStringCollection dataCollection = null)
        //{
        //    MySqlCommand command;
        //    MySqlDataAdapter adapter = new MySqlDataAdapter();
        //    DataSet ds = new DataSet();

        //    try
        //    {
        //        if (sqlcon.con.State == ConnectionState.Closed)
        //        {
        //            sqlcon.con.Open();
        //        }
        //        command = new MySqlCommand(sql, sqlcon.con);
        //        adapter.SelectCommand = command;
        //        adapter.Fill(ds);
        //        adapter.Dispose();
        //        command.Dispose();
        //        sqlcon.con.Close();

        //        if (firstItem != "")
        //        {
        //            DataRow dr = ds.Tables[0].NewRow();
        //            dr[dismem] = firstItem;
        //            dr[valmem] = 0;
        //            ds.Tables[0].Rows.InsertAt(dr, 0);
        //        }




        //        cbo.DataSource = ds.Tables[0];
        //        cbo.ValueMember = valmem;
        //        cbo.DisplayMember = dismem;

        //        //if autocomplete is also to be displayed
        //        if (showAutoComplete == true)
        //        {
        //            foreach (DataRow row in ds.Tables[0].Rows)
        //            {
        //                dataCollection.Add(row[1].ToString());
        //            }
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error filling " + cbo.Name + " \n" + ex);
        //    }
        //}



        //function to return total days in a nepali month
        public static string getNepMonthTotDays(string nepYear,string nepMonthNum)
        {
            string monTot="";
            string MonFld = "NDNepM" + nepMonthNum;
            monTot = global.getSingleDataFromTable("SELECT " + MonFld + " FROM setup_nepcalender WHERE Year=" + nepYear);
            return monTot;
        }
        public static string getuserpermission(string userid, string menuname,string menuid)
        {
            string permission = "";
          //  string MonFld = "NDNepM" + nepMonthNum;
            permission = global.getSingleDataFromTable("SELECT `user_permissionid`, `userid`, `menuid`, `access_permission`, `assignby_user`, `assigned_in`, `assignnep_date` FROM `user_permission_assign`  WHERE userid=" + userid+ "AND menuid="+menuid);
            
            return permission;
        }
        public string dateAddNepali(string nepDate)
        {
            string resultDate;

            if (nepDate != "")
            {
                string year;
                string month;
                string day;
                string years;
                string months;
                string days;
                int ryear = 0;
                int rmonth = 0;
                int rday = 0;
                try
                {
                    years = (nepDate).Substring(0, 4);
                    months = (nepDate).Substring(5, 2);
                    days = (nepDate).Substring(8, 2);


                    //  SqlConnection con = new SqlConnection("Data Source='" + Properties.Settings.Default.servername + "';Initial Catalog='" + Properties.Settings.Default.databasename + "';User ID='" + Properties.Settings.Default.username + "';Password='" + Properties.Settings.Default.passwordname + "' ");
                    if (sqlcon.con.State == ConnectionState.Closed)
                    {
                        sqlcon.con.Open();
                    }

                    MySqlDataAdapter qrey = new MySqlDataAdapter("SELECT    GetUnicodeToNumber(N'" + years.ToString() + "'),  GetUnicodeToNumber(N'" + months.ToString() + "'), GetUnicodeToNumber(N'" + days.ToString() + "') ", sqlcon.con);
                    //  qrey.SelectCommand.Parameters.AddWithValue("@id", cust.ToString());


                    DataTable tb = new DataTable();
                    qrey.Fill(tb);
                    year = tb.Rows[0][0].ToString();
                    month = tb.Rows[0][1].ToString();
                    day = tb.Rows[0][2].ToString();
                    //  con.Open();
                    ////////////////////////////////////////
                    //  MySqlCommand command;
                    //MySqlDataAdapter adapter = new MySqlDataAdapter();
                    //DataSet ds = new DataSet();
                    //string sql = "SELECT    GetUnicodeToNumber(N'" + years.ToString() + "'),  GetUnicodeToNumber(N'" + months.ToString() + "'), GetUnicodeToNumber(N'" + days.ToString() + "') ";

                    ////string sql = "SELECT VdcCode, VdcNepnam FROM setup_vdc where setup_vdc.DistCode='" + comboBox3.ValueMember + "'";



                    //command = new SqlCommand(sql, sqlcon.con);
                    //adapter.SelectCommand = command;
                    //adapter.Fill(ds);
                    //adapter.Dispose();
                    //command.Dispose();


                    //foreach (DataRow row in ds.Tables[0].Rows)
                    //{
                    //    year = row[0].ToString();
                    //    month = row[1].ToString();
                    //    day = row[2].ToString();

                    //     MessageBox.Show(year.ToString() + " ," + month.ToString() + " ," + day.ToString());

                    if (day.ToString() == "01")
                    {
                        rday = 30;
                        if (month == "01")
                        {
                            rmonth = 12;
                            ryear = int.Parse(year) + 4;

                        }

                        else
                        {
                            ryear = int.Parse(year) + 5;
                            rmonth = (int.Parse(month)) - 1;
                        }
                    }
                    else
                    {
                        rday = (int.Parse(day)) - 1;
                        rmonth = int.Parse(month);
                        ryear = int.Parse(year) + 5;
                    }

                    //   MessageBox.Show("Year:" + ryear.ToString("0000") + "Month:" + rmonth.ToString("00") + "Day:" + rday.ToString("00"));
                    string newdate = ryear.ToString("0000") + rmonth.ToString("00") + rday.ToString("00");
                    //  sql = "SELECT    GetNumberToUnicode('" + newdate.ToString() + "') ";

                    //string sql = "SELECT VdcCode, VdcNepnam FROM setup_vdc where setup_vdc.DistCode='" + comboBox3.ValueMember + "'";

                    MySqlDataAdapter qreys = new MySqlDataAdapter("SELECT    GetNumberToUnicode('" + newdate.ToString() + "') ", sqlcon.con);
                    //  qrey.SelectCommand.Parameters.AddWithValue("@id", cust.ToString());


                    DataTable tbs = new DataTable();
                    qreys.Fill(tbs);
                    resultDate = tbs.Rows[0][0].ToString();
                    sqlcon.con.Close();
                    //command = new SqlCommand(sql, sqlcon.con);
                    //adapter.SelectCommand = command;
                    //adapter.Fill(ds);
                    //adapter.Dispose();
                    //command.Dispose();


                    //foreach (DataRow rowss in ds.Tables[0].Rows)
                    //{
                    //  renew_date.Text = rowss[0].ToString();
                    // MessageBox.Show("Inside loop");

                    //month = row[1].ToString();
                    //day = row[2].ToString();

                    //  }  //renew_date.Text = ryear.ToString("0000") + rmonth.ToString("00") + rday.ToString("00");
                    // }

                    return resultDate;
                }
                catch (Exception ex)
                {
                    // MessageBox.Show("Before Exception");
                   
                    MessageBox.Show(ex.ToString());
                    return "";

                }
            }
            else
            {
                MessageBox.Show("Please enter complete date", "Date Error");
                return "";
            }
        }


        public static bool isWholeRowEmpty(DataGridViewRow dgr)
        {
            bool isemp = false;
            for (int ii = 0; ii < dgr.Cells.Count; ii++)
            {
                if (dgr.Cells[ii].Value != null)
                {
                    if (!string.IsNullOrEmpty(dgr.Cells[ii].Value.ToString()))
                    {
                        //cell is not empty - return with false
                        isemp = false;
                        return isemp;
                    }
                }
            }
            isemp = true;
            return isemp;
        }

        public static bool checkValueInCombo(DataGridViewComboBoxCell dgc, string val)
        {
            bool chkres = false;
            for(int ii=0;ii< dgc.Items.Count; ii++)
            {
                //MessageBox.Show(dgc.Items[ii].ToString() + " " + val);
                if (dgc.Items[ii].ToString()==val)
                {
                    chkres = true;
                    return chkres;
                }

            }
            return chkres;
        }

        public static void createBorderAround(Control ctrl,Color clr,int wdh)
        {
            //BORDER 
            Label lblleft = new Label();
            Label lblright = new Label();
            Label lbltop = new Label();
            Label lblbottom = new Label();

            lblleft.BackColor = lblright.BackColor = lblbottom.BackColor = clr;
            lblleft.Width = lblright.Width = wdh;
            lblleft.Top = lblright.Top = 0;
            lblleft.Height = lblright.Height = ctrl.Height;
            lblleft.Left = 0;
            lblright.Left = ctrl.Width - lblright.Width;

            lbltop.Height = lblbottom.Height = wdh;
            lbltop.Left = lblbottom.Left = 0;
            lbltop.Width = lblbottom.Width = ctrl.Width;
            lbltop.Top = 0;
            lblbottom.Top = ctrl.Height - lblbottom.Height;

            lblleft.AutoSize = lblright.AutoSize = lblbottom.AutoSize = false;

            ctrl.Controls.Add(lblleft);
            ctrl.Controls.Add(lblright);
            ctrl.Controls.Add(lbltop);
            ctrl.Controls.Add(lblbottom);

            lblleft.Show();
            lblright.Show();
            lbltop.Show();
            lblbottom.Show();

            lblleft.SendToBack();
            lblright.SendToBack();
            lbltop.SendToBack();
            lblbottom.SendToBack();
        }



        public static bool isNumeric (string val)
        {
            bool nume = decimal.TryParse(global.convertUnicodeToNum(val), out decimal num);
            return nume;
        }





        public static string TruncateAtWord(string text, int maxCharacters, string trailingStringIfTextCut = "&hellip;")
        {
            if (text == null || (text = text.Trim()).Length <= maxCharacters)
                return text;

            int trailLength = trailingStringIfTextCut.StartsWith("&") ? 1
                                                                      : trailingStringIfTextCut.Length;
            maxCharacters = maxCharacters - trailLength >= 0 ? maxCharacters - trailLength
                                                             : 0;
            int pos = text.LastIndexOf(" ", maxCharacters);
            if (pos >= 0)
                return text.Substring(0, pos) + trailingStringIfTextCut;

            return string.Empty;
        }


        public static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        //get all controls within a control (e.g. textboxes inside a form -- even the textboxes within panels in a form
        public static IEnumerable<Control> GetAll(Control control, IEnumerable<Type> filteringTypes)
        {
            var ctrls = control.Controls.Cast<Control>();

            return ctrls.SelectMany(ctrl => GetAll(ctrl, filteringTypes))
                        .Concat(ctrls)
                        .Where(ctl => filteringTypes.Any(t => ctl.GetType() == t));
        }
    }
   
}
