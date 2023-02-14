using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.ObjectModel;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ePayHrms.Company;
using ePayHrms.Employee;
using System.Data.SqlClient;

public partial class Hrms_Master_Default : System.Web.UI.Page
{
    private SqlConnection _connection;
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    Company company = new Company();

    Employee employee = new Employee();
    
    string s_login_role;
    string s_form = "";
    string[] str_array = new string[12];
    DataSet ds_userrights;
    int i;

   

    protected void Page_Load(object sender, EventArgs e)
    {
        
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;

        if (!IsPostBack)
        {
            switch (s_login_role)
            {
                case "a":

                    load_admin();
                    //access();
                    break;

                case "h":
                   // ddl_branch.Visible = true;
                    load();
                    access();
                    break;

                case "u": s_form = "3";
                    ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                    if (ds_userrights.Tables[0].Rows.Count > 0)
                    {
                        load();
                    }
                    else
                    {
                        Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator.";
                        Response.Redirect("~/Company_Home.aspx");
                    }
                    break;

                default: Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                    Response.Redirect("~/Company_Home.aspx");
                    break;
            }
        }
    }

    public void load()
    {
        //lbl_branch.Visible = false;
        ddl_branch.Visible =false;

        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM shift_details where pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "'", myConnection);

        DataSet ds = new DataSet();

        ad.Fill(ds, "shift_details");


        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView1.DataSource = ds;
            GridView1.DataBind();
            int columnCount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columnCount;
            GridView1.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            //GridView1.Enabled = true;
            GridView1.DataSource = ds;
            GridView1.DataBind();

        }

        myConnection.Open();
        cmd = new SqlCommand("Select * from attendance_ceiling where pn_companyid = '" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
        SqlDataReader re1 = cmd.ExecuteReader();
        if (re1.Read())
        {
            string intime = re1[2].ToString();
            txt_intimelt.Text = intime.Substring(11,5);           
            string earlyin =re1[3].ToString();
            txt_earlyin.Text =earlyin.Substring(11,5);
            string shiftLin =re1[4].ToString();
            txt_shiftLin.Text =shiftLin.Substring(11,5);
            string lunchEin =re1[5].ToString();
            txt_lunchEin.Text =lunchEin.Substring(11,5);
            string halfdaylt =re1[6].ToString();
            txt_halfdaylt.Text =halfdaylt.Substring(11,5);
            string otlt = re1[7].ToString();
            txt_otlt.Text =otlt.Substring(11,5);
            string perlt =re1[8].ToString();
            txt_perlt.Text =perlt.Substring(11,5);
            txt_leavelt.Text = Convert.ToString(re1[9]);
            
            string mot = Convert.ToString(re1[10]);
            if (mot == "yes")
            {
                chk_morningot.Checked = true;
            }
            else
            {
                chk_morningot.Checked = false;
            }
            string mday = Convert.ToString(re1[11]);
            if (mday == "Week-off ex")
            {
                ddl_weeloff1.SelectedValue = Convert.ToString(re1[12]);
                ddl_weekoff2.SelectedValue = Convert.ToString(re1[13]);
                lbl_manual.Enabled = false;
                txt_manual.Enabled = false;
                btn_weekoff.BackColor = System.Drawing.Color.AliceBlue;
                btn_weekoff.ForeColor = System.Drawing.Color.Black;
                    //"#F7F6F3";
            }
            else if (mday == "Manual Days")
            {
                txt_manual.Text = Convert.ToString(re1[14]);
                btn_manualdays.BackColor = System.Drawing.Color.AliceBlue;
                btn_manualdays.ForeColor = System.Drawing.Color.Black;
            }
            else if (mday == "Month Days")
            {
                ddl_weeloff1.SelectedItem.Text = Convert.ToString(re1[12]);
                ddl_weekoff2.SelectedItem.Text = Convert.ToString(re1[13]);
                btn_monthdays.BackColor = System.Drawing.Color.AliceBlue;
                btn_monthdays.ForeColor = System.Drawing.Color.Black;
                lbl_manual.Enabled = false;
                txt_manual.Enabled = false;
            }
            txt_otdays.Text = Convert.ToString(re1[15]);
            txt_othrs.Text = Convert.ToString(re1[16]);

            string tc = Convert.ToString(re1[17]);
            if (tc == "Daily Time Card")
            {
                rdo_timecard.SelectedValue = "Daily Time Card";
            }
            else
            {
                rdo_timecard.SelectedValue = "Cummulative Time Card";
            }

            string ptax = Convert.ToString(re1[18]);

            if (ptax != "")
            {
                int j = 0;
                for (i = 0; i < ptax.Length; i = i + 2)
                {
                    str_array[j] = ptax.Substring(i, 2);
                    j++;
                }

                for (i = 0; i < chk_months.Items.Count; i++)
                {
                    for (j = 0; j < str_array.Length; j++)
                    {
                        if (chk_months.Items[i].Value == str_array[j])
                        {
                            chk_months.Items[i].Selected = true;
                        }
                    }
                }
            }

            //if (ptax == "All")
            //{
            //    chk_allmonths.Checked = true;
            //    chk_months.Enabled = false;
            //}

            string rname = Convert.ToString(re1[19]);
            ddl_reader.SelectedValue = rname;

        }
        re1.Close();
        myConnection.Close();

        if (txt_intimelt.Text == "")
        {
            btn_save.Text = "Save";
            lbl_manual.Enabled = false;
            txt_manual.Enabled = false;
        }
        else
        {
            btn_save.Text = "Modify";
        }
    }

    public void clear()
    {
        txt_intimelt.Text = "";
        txt_earlyin.Text = "";
        txt_shiftLin.Text = "";
        txt_lunchEin.Text = "";
        txt_halfdaylt.Text = "";
        txt_otlt.Text = "";
        txt_perlt.Text = "";
        txt_leavelt.Text = "";
        txt_manual.Text = "";
        txt_otdays.Text = "";
        txt_othrs.Text = "";
        ddl_reader.SelectedItem.Text = "select";
        ddl_weeloff1.SelectedItem.Text = "select";
        ddl_weekoff2.SelectedItem.Text = "select";
    }

    public void load_admin()
    {
        myConnection.Open();
        SqlDataAdapter ad = new SqlDataAdapter("select * from paym_branch", myConnection);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        ddl_branch.DataTextField = "branchname";
        ddl_branch.DataValueField = "pn_branchid";
        ddl_branch.DataSource = ds;
        ddl_branch.DataBind();
        ddl_branch.Items.Insert(0, "select Branch");
        myConnection.Close();

        //branch_time.Visible = false; // Table
        //myConnection.Open();
        //SqlCommand cmd = new SqlCommand();
        //cmd.Connection = myConnection;
        //cmd.CommandType = CommandType.Text;
        //cmd.CommandText = "select * from paym_branch";

        //SqlDataAdapter adp = new SqlDataAdapter(cmd);

        //DataSet ds1 = new DataSet();
        //adp.Fill(ds1, "paym_branch");

        //ddl_branch.DataSource = ds1;
        //ddl_branch.DataTextField = "branchname";
        //ddl_branch.DataValueField = "pn_branchid";
        //ddl_branch.DataBind();



        //myConnection.Close();

    }
        
    public void access()
    {
        // MessageBox.Show(employee.BranchId.ToString());
        // MessageBox.Show(employee.CompanyId.ToString());
        _connection = con.fn_Connection();
        _connection.Open();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and section_view='No'", _connection);
        SqlDataReader rdrview = cmd.ExecuteReader();
        if (rdrview.Read())
        {
            Response.Write("<script language='javascript'>alert('Permission Restricted. Please Contact Administrator.');window.location='Company_Home.aspx';</script>");
        }
        rdrview.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and section_edit='No'", _connection);
        SqlDataReader rdredit = cmd.ExecuteReader();
        if (rdredit.Read())
        {

        }
        rdredit.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and  section_delete='No'", _connection);
        SqlDataReader rdrdel = cmd.ExecuteReader();
        if (rdrdel.Read())
        {

        }
        rdrdel.Close();

    }

    


    protected void Edit(object sender, GridViewEditEventArgs e)
    {
        try
        {

        }

        catch (Exception ex)
        {

        }


    }


    protected void Update(object sender, GridViewUpdateEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            //lbl_Error.Text = "Error";
        }

    }

    private void DeleteRecord(string ID)
    {

        string Statement = " SELECT * FROM shift_pattern where shift_code1 = '" + ID + "' or shift_code2 = '" + ID + "' or shift_code3 = '" + ID + "' or shift_code4 = '" + ID + "' or shift_code5 = '" + ID + "' or shift_code6 = '" + ID + "' or shift_code7 = '" + ID + "'";
        myConnection.Open();
        SqlCommand cmd1 = new SqlCommand(Statement, myConnection);
        cmd1.CommandType = CommandType.Text;
        SqlDataReader rdrdel = cmd1.ExecuteReader();
        if (rdrdel.Read())
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Deletion Error: This record has a relationship with pattern code!');", true);
        }

        if (s_login_role == "a")
            {
                string sqlStatement = "DELETE FROM shift_details WHERE shift_code = @shift_code and pn_branchid = '" + ddl_branch.SelectedItem.Value + "'";
                SqlCommand cmd = new SqlCommand(sqlStatement, myConnection);
                cmd.Parameters.AddWithValue("@shift_code", ID);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Deleted Successfully!');", true);
            }
            else
            {
                string sqlStatement = "DELETE FROM shift_details WHERE shift_code = @shift_code and pn_branchid = '" + employee.BranchId + "'";
                SqlCommand cmd = new SqlCommand(sqlStatement, myConnection);
                cmd.Parameters.AddWithValue("@shift_code", ID);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Deleted Successfully!');", true);
            }
        
        myConnection.Close();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        if (s_login_role == "a")
        {
            GridView1.EditIndex = -1;
            load();
        }
        else if(s_login_role == "h")
        {
            GridView1.EditIndex = -1;
            load1();
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "add")
        {

            string scode = ((TextBox)GridView1.FooterRow.FindControl("txt_shiftcode")).Text;
            string stime = ((TextBox)GridView1.FooterRow.FindControl("txt_starttime")).Text;
            string btimeo = ((TextBox)GridView1.FooterRow.FindControl("txt_breaktimeo")).Text;
            string btimei = ((TextBox)GridView1.FooterRow.FindControl("txt_breaktimei")).Text;
            string etime = ((TextBox)GridView1.FooterRow.FindControl("txt_endtime")).Text;
            string sindicator = ((DropDownList)GridView1.FooterRow.FindControl("ddl_shiftindicator")).Text;


            AddNewRecord(scode, stime, btimeo, btimei, etime, sindicator);
        }
         
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (s_login_role == "a")
        {
            string ID = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lbl_shiftcode")).Text;
            DeleteRecord(ID);
            load1();
            access();
        }
        else if(s_login_role == "h")
        {
            string ID = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lbl_shiftcode")).Text;
            DeleteRecord(ID);

            load();
            access();
        }

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (s_login_role == "a")
        {
            GridView1.EditIndex = e.NewEditIndex;   // turn to edit mode
            load1();
            access();
        }
        else if(s_login_role == "h")
        {
            GridView1.EditIndex = e.NewEditIndex; // turn to edit mode
            load();
            access();
            //Edit();
            //AddNewRecord(scode, stime, btimeo, btimei, etime, sindicator);
        }

    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {


    }

    private void AddNewRecord(string scode, string stime, string btimeo, string btimei, string etime, string sindicator)
    {
        try
        {
            if (s_login_role == "a")
            {

                if (stime == etime)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Give Start Time and End Time Different!');", true);
                }
                else if (btimeo == btimei)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Give Break Start Time and End Time Different!');", true);
                }
                else if (sindicator != "Select" && stime != "" && btimeo != "" && btimei != "" && etime != "" && scode != "")
                {
                    string query = @"INSERT INTO shift_details (pn_CompanyID, pn_BranchID, shift_code, start_time, break_time_out, break_time_in, end_time, shift_indicator) VALUES('" + employee.CompanyId + "','" + ddl_branch.SelectedItem.Value + "','" + scode + "','" + stime + "','" + btimeo + "','" + btimei + "','" + etime + "','" + sindicator + "')";
                    SqlCommand myCommand = new SqlCommand(query, myConnection);
                    myConnection.Open();
                    myCommand.ExecuteNonQuery();
                    myConnection.Close();
                    load1();
                    access();
                    shift1(scode, stime, etime, sindicator);
                    

                }
                //else if(stime != etime)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Message all fields!');", true);
                //}
                else
               {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter all fields!');", true);
                    //  ScriptManager.registerclientscriptblock(this.page, this.page.gettype(), "alert", "alert('enter all fields!');", true);
                }
            }
            else if (s_login_role == "h")
            {
                if (stime == etime)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Give Start Time and End Time Different!');", true);
                }
                else if(btimeo == btimei)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Give Break Start Time and End Time Different!');", true);
                }
                else if (sindicator != "Select" && stime != "" && btimeo != "" && btimei != "" && etime != "" && scode != "")
                {
                    string query = @"INSERT INTO shift_details (pn_CompanyID, pn_BranchID, shift_code, start_time, break_time_out, break_time_in, end_time, shift_indicator) VALUES('" + employee.CompanyId + "','" + employee.BranchId + "','" + scode + "','" + stime + "','" + btimeo + "','" + btimei + "','" + etime + "','" + sindicator + "')";
                    SqlCommand myCommand = new SqlCommand(query, myConnection);
                    myConnection.Open();
                    myCommand.ExecuteNonQuery();
                    myConnection.Close();
                    load();
                    access();
                    shift(scode,stime,etime,sindicator);
                }
                //else if(stime != etime)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Give Start Time and End Time Different!');", true);
                //}
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter all fields!');", true);
                }
            }
        }
        catch (SqlException ex)
        {
            if (ex.Number.ToString() == "241")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Time Format Error!');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Shift Code Already Exist!');", true);
            }
        }

    }
    public void shift(string scode,string stime, string etime,string sindicator)
    {
        string query = @"insert into paym_Shift(pn_CompanyID,v_ShiftName,v_ShiftFrom,v_ShiftTo,status,BranchID,v_ShiftCategory)VALUES('" + employee.CompanyId + "','" + scode + "','" + stime + "','" + etime + "','Y','" + employee.BranchId + "','" + sindicator + "')";
        //string query = @"INSERT INTO shift_details (pn_CompanyID, pn_BranchID, shift_code, start_time, break_time_out, break_time_in, end_time, shift_indicator) VALUES('" + employee.CompanyId + "','" + ddl_branch.SelectedItem.Value + "','" + scode + "','" + stime + "','" + etime + "','" + sindicator + "')";
        SqlCommand myCommand = new SqlCommand(query, myConnection);
        myConnection.Open();
        myCommand.ExecuteNonQuery();
        myConnection.Close();
    }
    public void shift1(string scode, string stime, string etime, string sindicator)
    {
        string query = @"insert into paym_Shift(pn_CompanyID,v_ShiftName,v_ShiftFrom,v_ShiftTo,status,BranchID,v_ShiftCategory)VALUES('" + employee.CompanyId + "','" + scode + "','" + stime + "','" + etime + "','Y','" + ddl_branch.SelectedValue + "','" + sindicator + "')";
        //string query = @"INSERT INTO shift_details (pn_CompanyID, pn_BranchID, shift_code, start_time, break_time_out, break_time_in, end_time, shift_indicator) VALUES('" + employee.CompanyId + "','" + ddl_branch.SelectedItem.Value + "','" + scode + "','" + stime + "','" + etime + "','" + sindicator + "')";
        SqlCommand myCommand = new SqlCommand(query, myConnection);
        myConnection.Open();
        myCommand.ExecuteNonQuery();
        myConnection.Close();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow Gvrow = GridView1.Rows[e.RowIndex];
        //Label8.Text = Gvrow.ToString();
        if (Gvrow != null && s_login_role == "a")
        
        {
           
            
            string scodeedit = ((Label)Gvrow.FindControl("txt_shiftcode_edit")).Text;
            string stimeedit = ((TextBox)Gvrow.FindControl("txt_starttime_edit")).Text;
            string btimeoedit = ((TextBox)Gvrow.FindControl("txt_breaktimeo_edit")).Text;
            string btimeiedit = ((TextBox)Gvrow.FindControl("txt_breaktimei_edit")).Text;
            string etimeedit = ((TextBox)Gvrow.FindControl("txt_endtime_edit")).Text;
            string sindicatoredit = ((DropDownList)Gvrow.FindControl("ddl_shiftindicator_edit")).Text;

            //myConnection.Open();
            //cmd = new SqlCommand("update shift_details set shift_code='" + scodeedit + "', start_time='" + stimeedit + "', break_time_out='" + btimeoedit + "', break_time_in='" + btimeiedit + "', end_time='" + etimeedit + "', shift_indicator='" + sindicatoredit + "' where shift_code='" + scodeedit + "' and pn_companyid= '" + employee.CompanyId + "' and  pn_branchid= '" + ddl_branch.SelectedItem.Value + "'", myConnection);
            //cmd.ExecuteNonQuery();
            //myConnection.Close();
            //GridView1.EditIndex = -1; // turn off edit mode
            //load1();
            AddEditRecord(scodeedit, stimeedit, btimeoedit, btimeiedit, etimeedit, sindicatoredit);

        }
        else if (Gvrow != null && s_login_role == "h")
        {
            string scodeedit = ((Label)Gvrow.FindControl("txt_shiftcode_edit")).Text;
            string stimeedit = ((TextBox)Gvrow.FindControl("txt_starttime_edit")).Text;
            string btimeoedit = ((TextBox)Gvrow.FindControl("txt_breaktimeo_edit")).Text;
            string btimeiedit = ((TextBox)Gvrow.FindControl("txt_breaktimei_edit")).Text;
            string etimeedit = ((TextBox)Gvrow.FindControl("txt_endtime_edit")).Text;
            string sindicatoredit = ((DropDownList)Gvrow.FindControl("ddl_shiftindicator_edit")).Text;

            //myConnection.Open();
            //cmd = new SqlCommand("update shift_details set shift_code='" + scodeedit + "', start_time='" + stimeedit + "', break_time_out='" + btimeoedit + "', break_time_in='" + btimeiedit + "', end_time='" + etimeedit + "', shift_indicator='" + sindicatoredit + "' where shift_code='" + scodeedit + "' and pn_companyid= '" + employee.CompanyId + "' and  pn_branchid= '" + employee.BranchId + "'", myConnection);
            //cmd.ExecuteNonQuery();
            //myConnection.Close();
            //GridView1.EditIndex = -1; // turn to edit mode
            //load();

            AddEditRecord(scodeedit, stimeedit, btimeoedit, btimeiedit, etimeedit, sindicatoredit);

        }
    }


    private void AddEditRecord(string scodeedit, string stimeedit, string btimeoedit, string btimeiedit, string etimeedit, string sindicatoredit)
    {
        try
        {
            if (s_login_role == "a")
            {
                if (stimeedit == etimeedit)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Give Start Time and End Time Different!');", true);
                }
                else if (btimeoedit == btimeiedit)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Give Break Start Time and End Time Different!');", true);
                }
                else if (sindicatoredit != "Select" && stimeedit != "" && btimeoedit != "" && btimeiedit != "" && etimeedit != "" && scodeedit != "")
                {
                    //string query = @"INSERT INTO shift_details (pn_CompanyID, pn_BranchID, shift_code, start_time, break_time_out, break_time_in, end_time, shift_indicator) VALUES('" + employee.CompanyId + "','" + ddl_branch.SelectedItem.Value + "','" + scode + "','" + stime + "','" + btimeo + "','" + btimei + "','" + etime + "','" + sindicator + "')";
                    //SqlCommand myCommand = new SqlCommand(query, myConnection);
                    //myConnection.Open();
                    //myCommand.ExecuteNonQuery();
                    //myConnection.Close();
                    //load1();
                    //access();

                    myConnection.Open();
                    cmd = new SqlCommand("update shift_details set shift_code='" + scodeedit + "', start_time='" + stimeedit + "', break_time_out='" + btimeoedit + "', break_time_in='" + btimeiedit + "', end_time='" + etimeedit + "', shift_indicator='" + sindicatoredit + "' where shift_code='" + scodeedit + "' and pn_companyid= '" + employee.CompanyId + "' and  pn_branchid= '" + employee.BranchId + "'", myConnection);
                    cmd.ExecuteNonQuery();
                    myConnection.Close();
                    GridView1.EditIndex = -1; // turn to edit mode
                    load();



                }
                //else if(stime != etime)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Message all fields!');", true);
                //}
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter all fields!');", true);
                    //  ScriptManager.registerclientscriptblock(this.page, this.page.gettype(), "alert", "alert('enter all fields!');", true);
                }
            }
            else if (s_login_role == "h")
            {
                if (stimeedit == etimeedit)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Give Start Time and End Time Different!');", true);
                }
                else if (btimeoedit == btimeiedit)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Give Break Start Time and End Time Different!');", true);
                }
                else if (sindicatoredit != "Select" && stimeedit != "" && btimeoedit != "" && btimeiedit != "" && etimeedit != "" && scodeedit != "")
                {
                    //string query = @"INSERT INTO shift_details (pn_CompanyID, pn_BranchID, shift_code, start_time, break_time_out, break_time_in, end_time, shift_indicator) VALUES('" + employee.CompanyId + "','" + employee.BranchId + "','" + scode + "','" + stime + "','" + btimeo + "','" + btimei + "','" + etime + "','" + sindicator + "')";
                    //SqlCommand myCommand = new SqlCommand(query, myConnection);
                    //myConnection.Open();
                    //myCommand.ExecuteNonQuery();
                    //myConnection.Close();
                    //load();
                    //access();

                    myConnection.Open();
                    cmd = new SqlCommand("update shift_details set shift_code='" + scodeedit + "', start_time='" + stimeedit + "', break_time_out='" + btimeoedit + "', break_time_in='" + btimeiedit + "', end_time='" + etimeedit + "', shift_indicator='" + sindicatoredit + "' where shift_code='" + scodeedit + "' and pn_companyid= '" + employee.CompanyId + "' and  pn_branchid= '" + employee.BranchId + "'", myConnection);
                    cmd.ExecuteNonQuery();
                    myConnection.Close();
                    GridView1.EditIndex = -1; // turn to edit mode
                    load();




                }
                //else if(stime != etime)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Give Start Time and End Time Different!');", true);
                //}
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter all fields!');", true);
                }
            }
        }
        catch (SqlException ex)
        {
            if (ex.Number.ToString() == "241")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Time Format Error!');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Shift Code Already Exist!');", true);
            }
        }

    }




    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (s_login_role == "a")
        {
            employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
            if (btn_save.Text == "Modify")
            {
                string m_ot = "No", month_type = "";

                if (chk_morningot.Checked == true)
                {
                    m_ot = "yes";
                }
                if (ddl_weeloff1.Enabled == false && txt_manual.Enabled == true)
                {
                    month_type = "Manual Days";
                }
                else if (ddl_weeloff1.Enabled == false && txt_manual.Enabled == false)
                {
                    month_type = "Month Days";
                }
                else
                {
                    month_type = "Week-off ex";
                }
                string pt = "";
                if (chk_allmonths.Checked == true)
                {
                    pt = "All";
                }
                else
                {
                    for (int p = 0; p < chk_months.Items.Count; p++)
                    {
                        if (chk_months.Items[p].Selected == true)
                        {
                            pt = pt + chk_months.Items[p].Value;
                        }
                    }
                }

                string query = "update attendance_ceiling set intime ='" + txt_intimelt.Text + "',early_intime ='" + txt_earlyin.Text + "',shift_lin ='" + txt_shiftLin.Text + "',lunch_ein ='" + txt_lunchEin.Text + "',halfday ='" + txt_halfdaylt.Text + "',ot_limit ='" + txt_otlt.Text + "',permission_limit ='" + txt_perlt.Text + "',leave_days ='" + txt_leavelt.Text + "', morning_ot = '" + m_ot + "', month_type = '" + month_type + "', week_off1 = '" + ddl_weeloff1.Text + "', week_off2 = '" + ddl_weekoff2.Text + "', manual_days = '" + txt_manual.Text + "',ot_days = '" + txt_otdays.Text + "',ot_hrs = '" + txt_othrs.Text + "',time_card = '" + rdo_timecard.SelectedItem.Text + "',ptax_month='" + pt + "', reader_name = '" + ddl_reader.SelectedItem.Text + "' where pn_branchid ='" + ddl_branch.SelectedItem.Value + "' ";
                SqlCommand cmd = new SqlCommand(query, myConnection);
                myConnection.Open();
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Updated Successfully!');", true);
                myConnection.Close();
            }
            else
            {
                if (txt_intimelt.Text == " " || txt_earlyin.Text == " " || txt_shiftLin.Text == "" || txt_lunchEin.Text == "" || txt_halfdaylt.Text == "" || txt_otlt.Text == "" || txt_perlt.Text == "" || txt_leavelt.Text == "")
                {
                    Response.Write("<script language='javascript'>alert('Please fill all the details in Attendance Information')</script>");
                }
                else
                {
                    if ((ddl_weeloff1.Text == "Select" && ddl_weekoff2.Text == "Select" && txt_manual.Text == "") || txt_otdays.Text == "" || txt_othrs.Text == "")
                    {
                        Response.Write("<script language='javascript'>alert('Please fill all the details in Month Days & OT Calculation')</script>");
                    }
                    else
                    {
                        if (ddl_reader.Text == "Select")
                        {
                            Response.Write("<script language='javascript'>alert('Please select the reader name')</script>");
                        }
                        else
                        {
                            string m_ot = "No", month_type = "";

                            if (chk_morningot.Checked == true)
                            {
                                m_ot = "yes";
                            }
                            if (ddl_weeloff1.Enabled == false && txt_manual.Enabled == true)
                            {
                                month_type = "Manual Days";
                            }
                            else if (ddl_weeloff1.Enabled == false && txt_manual.Enabled == false)
                            {
                                month_type = "Month Days";
                            }
                            else
                            {
                                month_type = "Week-off ex";
                            }
                            string pt = "";
                            if (chk_allmonths.Checked == true)
                            {
                                pt = "All";
                            }
                            else
                            {
                                for (int p = 0; p < chk_months.Items.Count; p++)
                                {
                                    if (chk_months.Items[p].Selected == true)
                                    {
                                        pt = pt + chk_months.Items[p].Value;
                                    }
                                }
                            }
                            myConnection.Open();
                            cmd = new SqlCommand("insert into attendance_ceiling(pn_companyid, pn_branchid, intime, early_intime, shift_lin, lunch_ein, halfday, ot_limit, permission_limit, leave_days, morning_ot, month_type, week_off1, week_off2, manual_days, ot_days, ot_hrs, time_card, ptax_month, reader_name) values ('" + employee.CompanyId + "' , '" + ddl_branch.SelectedItem.Value + "' , '" + txt_intimelt.Text + "' , '" + txt_earlyin.Text + "' , '" + txt_shiftLin.Text + "' , '" + txt_lunchEin.Text + "' , '" + txt_halfdaylt.Text + "' , '" + txt_otlt.Text + "' , '" + txt_perlt.Text + "' , '" + txt_leavelt.Text + "' , '" + m_ot + "' , '" + month_type + "' , '" + ddl_weeloff1.SelectedItem.Text + "' , '" + ddl_weekoff2.SelectedItem.Text + "' , '" + txt_manual.Text + "' , '" + txt_otdays.Text + "' , '" + txt_othrs.Text + "' , '" + rdo_timecard.SelectedItem.Text + "' , '" + pt + "' , '" + ddl_reader.SelectedItem.Text + "')", myConnection);
                            cmd.ExecuteNonQuery();
                            Response.Write("<script language='javascript'>alert('Information Saved Successfully')</script>");
                            myConnection.Close();
                        }
                    }
                }
            }
        }
        else if (s_login_role == "h")
        {
            if (btn_save.Text == "Modify")
            {
                string m_ot = "No", month_type = "";

                if (chk_morningot.Checked == true)
                {
                    m_ot = "yes";
                }
                if (ddl_weeloff1.Enabled == false && txt_manual.Enabled == true)
                {
                    month_type = "Manual Days";
                }
                else if (ddl_weeloff1.Enabled == false && txt_manual.Enabled == false)
                {
                    month_type = "Month Days";
                }
                else
                {
                    month_type = "Week-off ex";
                }
                string pt = "";
                if (chk_allmonths.Checked == true)
                {
                    pt = "12";
                }
                else
                {
                    for (int p = 0; p < chk_months.Items.Count; p++)
                    {
                        if (chk_months.Items[p].Selected == true)
                        {
                            pt = pt + chk_months.Items[p].Value;
                        }
                    }
                }

                string query = " update attendance_ceiling set intime ='" + txt_intimelt.Text + "',early_intime ='" + txt_earlyin.Text + "',shift_lin ='" + txt_shiftLin.Text + "',lunch_ein ='" + txt_lunchEin.Text + "',halfday ='" + txt_halfdaylt.Text + "',ot_limit ='" + txt_otlt.Text + "',permission_limit ='" + txt_perlt.Text + "',leave_days ='" + txt_leavelt.Text + "', morning_ot = '"+m_ot+"', month_type = '" + month_type + "', week_off1 = '" + ddl_weeloff1.Text +"', week_off2 = '" + ddl_weekoff2.Text + "', manual_days = '"+txt_manual.Text+"',ot_days = '"+txt_otdays.Text+"',ot_hrs = '"+txt_othrs.Text+"',time_card = '"+rdo_timecard.SelectedItem.Text+"',ptax_month='"+pt+"', reader_name = '"+ddl_reader.SelectedItem.Text+"' where pn_branchid ='" + employee.BranchId + "' ";
                SqlCommand cmd = new SqlCommand(query, myConnection);
                myConnection.Open();
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Updated Successfully');", true);
                myConnection.Close();
            }
            else
            {
                    if (txt_intimelt.Text == " " || txt_earlyin.Text == " " || txt_shiftLin.Text == "" || txt_lunchEin.Text == "" || txt_halfdaylt.Text == "" || txt_otlt.Text == "" || txt_perlt.Text == "" || txt_leavelt.Text == "")
                    {
                        Response.Write("<script language='javascript'>alert('Please fill all the details in Attendance Information')</script>");
                    }
                    else
                    {
                        if ((ddl_weeloff1.Text == "Select" && ddl_weekoff2.Text == "Select" && txt_manual.Text == "") || txt_otdays.Text == "" || txt_othrs.Text == "")
                        {
                            Response.Write("<script language='javascript'>alert('Please fill all the details in Month Days & OT Calculation')</script>");
                        }
                        else
                        {
                            if (ddl_reader.Text == "Select")
                            {
                                Response.Write("<script language='javascript'>alert('Please select the reader name')</script>");
                            }
                            else
                            {
                                string m_ot = "No", month_type = "";

                                if (chk_morningot.Checked == true)
                                {
                                    m_ot = "yes";
                                }
                                if (ddl_weeloff1.Enabled == false && txt_manual.Enabled == true)
                                {
                                    month_type = "Manual Days";
                                }
                                else if (ddl_weeloff1.Enabled == false && txt_manual.Enabled == false)
                                {
                                    month_type = "Month Days";
                                }
                                else
                                {
                                    month_type = "Week-off ex";
                                }
                                string pt = "";
                                if (chk_allmonths.Checked == true)
                                {
                                    pt = "All";
                                }
                                else
                                {
                                    for (int p = 0; p < chk_months.Items.Count; p++)
                                    {
                                        if (chk_months.Items[p].Selected == true)
                                        {
                                            pt = pt + chk_months.Items[p].Value;
                                        }
                                    }
                                }

                                myConnection.Open();
                                cmd = new SqlCommand("insert into attendance_ceiling(pn_companyid , pn_branchid , intime , early_intime , shift_lin , lunch_ein , halfday , ot_limit , permission_limit , leave_days , morning_ot , month_type , week_off1 , week_off2 , manual_days , ot_days , ot_hrs , time_card , ptax_month , reader_name) values ('" + employee.CompanyId + "' , '" + employee.BranchId + "' , '" + txt_intimelt.Text + "' , '" + txt_earlyin.Text + "' , '" + txt_shiftLin.Text + "' , '" + txt_lunchEin.Text + "' , '" + txt_halfdaylt.Text + "' , '" + txt_otlt.Text + "' , '" + txt_perlt.Text + "' , '" + txt_leavelt.Text + "' , '" + m_ot + "' , '" + month_type + "' , '" + ddl_weeloff1.SelectedItem.Text + "' , '" + ddl_weekoff2.SelectedItem.Text + "' , '" + txt_manual.Text + "' , '" + txt_otdays.Text + "' , '" + txt_othrs.Text + "' , '" + rdo_timecard.SelectedItem.Text + "' , '" + pt + "' , '" + ddl_reader.SelectedItem.Text + "')", myConnection);
                                cmd.ExecuteNonQuery();
                                
                                Response.Write("<script language='javascript'>alert('Information Saved Successfully)</script>");
                                myConnection.Close();
                            }
                        }

                    }     
            }
        }
    }

    protected void btn_manualdays_Click(object sender, EventArgs e)
    {
        lbl_weekoff1.Enabled = false;
        lbl_weekoff2.Enabled = false;
        ddl_weeloff1.Enabled = false;
        ddl_weekoff2.Enabled = false;
        lbl_manual.Enabled = true;
        txt_manual.Enabled = true;
        btn_manualdays.BackColor = System.Drawing.Color.AliceBlue;
        btn_manualdays.ForeColor = System.Drawing.Color.Black;
        btn_monthdays.BackColor = System.Drawing.ColorTranslator.FromHtml("#5dade2");
        btn_monthdays.ForeColor = System.Drawing.Color.White;
        btn_weekoff.BackColor = System.Drawing.ColorTranslator.FromHtml("#5dade2");
        btn_weekoff.ForeColor = System.Drawing.Color.White;
    }

    protected void btn_monthdays_Click(object sender, EventArgs e)
    {
        lbl_weekoff1.Enabled = false;
        lbl_weekoff2.Enabled = false;
        ddl_weeloff1.Enabled = false;
        ddl_weekoff2.Enabled = false;
        lbl_manual.Enabled = false;
        txt_manual.Enabled = false;
        txt_manual.Text = "";
        btn_monthdays.BackColor = System.Drawing.Color.AliceBlue;
        btn_monthdays.ForeColor = System.Drawing.Color.Black;
        btn_weekoff.BackColor = System.Drawing.ColorTranslator.FromHtml("#5dade2");
        btn_weekoff.ForeColor = System.Drawing.Color.White;
        btn_manualdays.BackColor = System.Drawing.ColorTranslator.FromHtml("#5dade2");
        btn_manualdays.ForeColor = System.Drawing.Color.White;
    }

    protected void btn_weekoff_Click(object sender, EventArgs e)
    {
        lbl_weekoff1.Enabled = true;
        lbl_weekoff2.Enabled = true;
        ddl_weeloff1.Enabled = true;
        ddl_weekoff2.Enabled = true;
        lbl_manual.Enabled = false;
        txt_manual.Enabled = false;
        txt_manual.Text = "";
        btn_weekoff.BackColor = System.Drawing.Color.AliceBlue;
        btn_weekoff.ForeColor = System.Drawing.Color.Black;
        btn_monthdays.BackColor = System.Drawing.ColorTranslator.FromHtml("#5dade2");
        btn_monthdays.ForeColor = System.Drawing.Color.White;
        btn_manualdays.BackColor = System.Drawing.ColorTranslator.FromHtml("#5dade2");
        btn_manualdays.ForeColor = System.Drawing.Color.White;
    }

    protected void btn_reset_Click(object sender, EventArgs e)
    {
        txt_intimelt.Text = "";
        txt_earlyin.Text = "";
        txt_shiftLin.Text = "";
        txt_lunchEin.Text = "";
        txt_halfdaylt.Text = "";
        txt_otlt.Text = "";
        txt_perlt.Text = "";
        txt_leavelt.Text = "";
        txt_otdays.Text = "";
        txt_othrs.Text = "";
        txt_manual.Text = "";
        ddl_weekoff2.SelectedValue = "Select";
        ddl_weeloff1.SelectedValue = "Select";
    }
    public void Grid_Load()
    {
        branch_time.Visible = true;

        clear();
        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM shift_details where pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + ddl_branch.Text + "'", myConnection);
        DataSet ds = new DataSet();
        ad.Fill(ds, "shift_details");
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView1.DataSource = ds;
            GridView1.DataBind();
            int columnCount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columnCount;
            GridView1.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        myConnection.Open();
        cmd = new SqlCommand("Select * from attendance_ceiling where pn_companyid = '" + employee.CompanyId + "' and pn_branchid = '" + ddl_branch.SelectedItem.Value + "'", myConnection);
        SqlDataReader re1 = cmd.ExecuteReader();
        if (re1.Read())
        {
            DateTime intime = Convert.ToDateTime(re1[2]);
            txt_intimelt.Text = string.Format("{0:HH:mm}", intime);
            //TimeSpan int1 = intime.TimeOfDay;
            //txt_intimelt.Text = int1.ToString();
            DateTime earlyin = Convert.ToDateTime(re1[3]);
            txt_earlyin.Text = string.Format("{0:HH:mm}", earlyin);
            DateTime shiftLin = Convert.ToDateTime(re1[4]);
            txt_shiftLin.Text = string.Format("{0:HH:mm}", shiftLin);
            DateTime lunchEin = Convert.ToDateTime(re1[5]);
            txt_lunchEin.Text = string.Format("{0:HH:mm}", lunchEin);
            DateTime halfdaylt = Convert.ToDateTime(re1[6]);
            txt_halfdaylt.Text = string.Format("{0:HH:mm}", halfdaylt);
            DateTime otlt = Convert.ToDateTime(re1[7]);
            txt_otlt.Text = string.Format("{0:HH:mm}", otlt);
            DateTime perlt = Convert.ToDateTime(re1[8]);
            txt_perlt.Text = string.Format("{0:HH:mm}", perlt);
            txt_leavelt.Text = Convert.ToString(re1[9]);

            string mot = Convert.ToString(re1[10]);
            if (mot == "Yes")
            {
                chk_morningot.Checked = true;
            }
            else
            {
                chk_morningot.Checked = false;
            }

            string mday = Convert.ToString(re1[11]);
            if (mday == "Week-off ex")
            {
                ddl_weeloff1.SelectedItem.Text = Convert.ToString(re1[12]);
                ddl_weekoff2.SelectedItem.Text = Convert.ToString(re1[13]);
                lbl_manual.Enabled = false;
                txt_manual.Enabled = false;
            }
            else if (mday == "Manual Days")
            {
                txt_manual.Text = Convert.ToString(re1[14]);
            }
            txt_otdays.Text = Convert.ToString(re1[15]);
            txt_othrs.Text = Convert.ToString(re1[16]);

            string tc = Convert.ToString(re1[17]);
            if (tc == "Daily Time Card")
            {
                rdo_timecard.SelectedItem.Text = "Daily Time Card";
            }
            else
            {
                rdo_timecard.SelectedItem.Text = "Cummulative Time Card";
            }

            string ptax = Convert.ToString(re1[18]);
            if (ptax == "All")
            {
                chk_allmonths.Enabled = false;
            }

            string rname = Convert.ToString(re1[19]);
            ddl_reader.SelectedItem.Text = rname;

        }
        re1.Close();
        myConnection.Close();

        if (txt_intimelt.Text == "")
        {
            btn_save.Text = "Save";
            lbl_manual.Enabled = false;
            txt_manual.Enabled = false;
        }
        else
        {
            btn_save.Text = "Modify";
        }


    }
    protected void ddl_branch_SelectedIndexChanged(object sender, EventArgs e)
    {

        
        if (s_login_role == "a")
        {
            employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
        }

        SqlDataSource1.SelectCommand = "select employee_first_name,employeecode from paym_employee where pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + employee.BranchId + "' ";

        Grid_Load();
    }

    public void load1()
    {
        //lbl_branch.Visible = true;
        //ddl_branch.Visible = true;

        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM shift_details where pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + ddl_branch.SelectedItem.Value + "'", myConnection);

        DataSet ds = new DataSet();

        ad.Fill(ds, "shift_details");


        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView1.DataSource = ds;
            GridView1.DataBind();
            int columnCount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columnCount;
            GridView1.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        myConnection.Open();
        cmd = new SqlCommand("Select * from attendance_ceiling where pn_companyid = '" + employee.CompanyId + "' and pn_branchid = '" + ddl_branch.SelectedItem.Value + "'", myConnection);
        SqlDataReader re1 = cmd.ExecuteReader();
        if (re1.Read())
        {
            DateTime intime = Convert.ToDateTime(re1[2]);
            txt_intimelt.Text = string.Format("{0:HH:mm}", intime);
            //TimeSpan int1 = intime.TimeOfDay;
            //txt_intimelt.Text = int1.ToString();
            DateTime earlyin = Convert.ToDateTime(re1[3]);
            txt_earlyin.Text = string.Format("{0:HH:mm}", earlyin);
            DateTime shiftLin = Convert.ToDateTime(re1[4]);
            txt_shiftLin.Text = string.Format("{0:HH:mm}", shiftLin);
            DateTime lunchEin = Convert.ToDateTime(re1[5]);
            txt_lunchEin.Text = string.Format("{0:HH:mm}", lunchEin);
            DateTime halfdaylt = Convert.ToDateTime(re1[6]);
            txt_halfdaylt.Text = string.Format("{0:HH:mm}", halfdaylt);
            DateTime otlt = Convert.ToDateTime(re1[7]);
            txt_otlt.Text = string.Format("{0:HH:mm}", otlt);
            DateTime perlt = Convert.ToDateTime(re1[8]);
            txt_perlt.Text = string.Format("{0:HH:mm}", perlt);
            txt_leavelt.Text = Convert.ToString(re1[9]);

            string mot = Convert.ToString(re1[10]);
            if (mot == "Yes")
            {
                chk_morningot.Checked = true;
            }
            else
            {
                chk_morningot.Checked = false;
            }

            string mday = Convert.ToString(re1[11]);
            if (mday == "Week-off ex")
            {
                ddl_weeloff1.SelectedItem.Text = Convert.ToString(re1[12]);
                ddl_weekoff2.SelectedItem.Text = Convert.ToString(re1[13]);
                lbl_manual.Enabled = false;
                txt_manual.Enabled = false;
            }
            else if (mday == "Manual Days")
            {
                txt_manual.Text = Convert.ToString(re1[14]);
            }
            txt_otdays.Text = Convert.ToString(re1[15]);
            txt_othrs.Text = Convert.ToString(re1[16]);

            string tc = Convert.ToString(re1[17]);
            if (tc == "Daily Time Card")
            {
                rdo_timecard.SelectedItem.Text = "Daily Time Card";
            }
            else
            {
                rdo_timecard.SelectedItem.Text = "Cummulative Time Card";
            }

            string ptax = Convert.ToString(re1[18]);
            if (ptax == "All")
            {
                chk_allmonths.Checked = true;
            }

            string rname = Convert.ToString(re1[19]);
            ddl_reader.SelectedItem.Text = rname;

        }
        re1.Close();
        myConnection.Close();

        if (txt_intimelt.Text == "")
        {
            btn_save.Text = "Save";
            lbl_manual.Enabled = false;
            txt_manual.Enabled = false;
        }
        else
        {
            btn_save.Text = "Modify";
        }
    }
   
    protected void txt_manual_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txt_otdays_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txt_othrs_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_reader_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_weeloff1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_weekoff2_SelectedIndexChanged(object sender, EventArgs e)
    {
      
    }
    protected void chk_allmonths_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_allmonths.Checked == true)
        {
            foreach (ListItem li in chk_months.Items)
            {
                li.Selected = true;
            }
            chk_months.Enabled = false;
            
        }
        else
        {
            foreach (ListItem li in chk_months.Items)
            {
                li.Selected = false;
            }
            chk_months.Enabled = true;
        }
    }
    protected void txt_breaktimei_TextChanged(object sender, EventArgs e)
    {
       
        try
        {
            TimeSpan btimeo = TimeSpan.Parse(((TextBox)GridView1.FooterRow.FindControl("txt_breaktimeo")).Text);
            TimeSpan btimei = TimeSpan.Parse(((TextBox)GridView1.FooterRow.FindControl("txt_breaktimei")).Text);
            if (btimei < btimeo)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Break Intime is Lesser than Break Outtime!');", true);
                
            }
            else
            {
                ((TextBox)GridView1.FooterRow.FindControl("txt_endtime")).Focus();
            }
        }
        catch(FormatException ex)
        {
          //  if (ex.Number.ToString() == "241")
          //{
          //     ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Time Format Error in break time!');", true);
          //      ((TextBox)GridView1.FooterRow.FindControl("txt_endtime")).Text = String.Empty;
          //    ((TextBox)GridView1.FooterRow.FindControl("txt_endtime")).Focus();
          //    // ((TextBox)GridView1.FooterRow.FindControl("this.id")).Fo();
          //  }
        }
    }


   
}


    

            
            
            
            
            
            
            
