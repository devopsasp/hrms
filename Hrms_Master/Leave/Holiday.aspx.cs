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
using ePayHrms.Leave;
using System.Data.SqlClient;

public partial class Hrms_Master_Default : System.Web.UI.Page
{
    private SqlConnection _connection;
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    Company company = new Company();
    Leave l = new Leave();
    Employee employee = new Employee();
    PayRoll pay = new PayRoll();
    Collection<Leave> LeaveList;
    Collection<Company> CompanyList;
    Collection<PayRoll> HolidayList;
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    //private SqlConnection _con;
    //ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlCommand cmd = new SqlCommand();

    int company_Id, branch_Id, valid, temp_valid = 0, check, chkdate;
    string _Value;
    string s_login_role;
    string s_form = "";
    DataSet ds_userrights;

    protected void Page_Load(object sender, EventArgs e)
    {
        LeaveList = l.Get_year(l);
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        l.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        if (!IsPostBack)
        {
            switch (s_login_role)
            {
                case "a": load();
                    break;

                case "h":
                    LeaveList = l.Get_year(l);
                    load1();
                    access();
                    
                    DropDownList1.Visible = false;
                    break;

                case "u": s_form = "29";
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
        _connection = con.fn_Connection();
        _connection.Open();

        SqlDataAdapter ad = new SqlDataAdapter("select * from paym_branch", _connection);
        DataSet ds = new DataSet();
        ad.Fill(ds, "paym_branch");
        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "branchname";
        DropDownList1.DataValueField = "pn_branchid";
        DropDownList1.DataBind();      

    }

    public void access()
    {
        _connection = con.fn_Connection();
        _connection.Open();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and section_view='No'", _connection);
        SqlDataReader rdrview = cmd.ExecuteReader();
        if (rdrview.Read())
        {
            Response.Write("<script language='javascript'>alert('Permission Restricted. Please Contact Administrator.');window.location='~/Company_Home.aspx';</script>");
        }
        rdrview.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and section_edit='No'", _connection);
        SqlDataReader rdredit = cmd.ExecuteReader();
        if (rdredit.Read())
        {
            //Response.Write("<script language='javascript'>alert('Permission Restricted. Please Contact Administrator.');window.location='~/Company_Home.aspx';</script>");
            //GridView gv1 = FindControl("GridView1") as GridView;
            //gv1.AutoGenerateEditButton = false;
           // this.GridView1.AutoGenerateEditButton = false;
            for (int b = 0; b < GridView1.Rows.Count; b++)
            {
                GridView1.Rows[b].Cells[5].Controls[0].Visible = false;
            }
        }
        rdredit.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and  section_delete='No'", _connection);
        SqlDataReader rdrdel = cmd.ExecuteReader();
        if (rdrdel.Read())
        {
            // ((ImageButton)grid_Course.Rows[0].FindControl("img_update")).Visible = false;
            for (int a = 0; a < GridView1.Rows.Count; a++)
            {
                GridView1.Rows[a].Cells[5].Controls[2].Visible = false;
            }
            //((System.Web.UI.Control)grid_Course.HeaderRow.FindControl("lbldel")).Visible = false;
        }
        rdrdel.Close();

    }

    public void load1()
    {
        LeaveList = l.Get_year(l);
        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM paym_holiday where pn_BranchID = '" + employee.BranchId + "' and Fyear = '" + LeaveList[0].year + "' order by from_date asc", myConnection);

        DataSet ds = new DataSet();

        ad.Fill(ds, "paym_holiday");


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
    }

    //protected void btn_add_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //           l.leaveID = 0;
    //            save();
    //            //lbl_Error.Text = "<font color=Blue>Added Successfully</font>";
    //            LeaveList = l.fn_paym_leave1(employee.BranchId);

    //            if (LeaveList.Count > 0)
    //            {
    //                load1();
    //            }
    //    }
    //    catch (Exception ex)
    //    {
    //        lbl_Error.Text = "Error";
    //    }
    //}

    //public void save()                                                                                                                                                                                                                                                                                                  
    //{
    //    l.leaveName = txt_leavename.Value;
    //    l.leaveCode = txt_LeaveCode.Value;
    //    l.Count = Convert.ToInt32(txt_count.Value);
    //    l.status = 'Y';
    //   _Value= l.Leave_First(l);
    //    if (_Value != "1")
    //    {
    //        lbl_Error.Text = "<font color=Blue>Added Successfully</font>";
    //        myConnection.Open();
    //        cmd = new SqlCommand("update paym_holiday set pn_BranchID = '" + employee.BranchId + "' , increment_leave = '"+ddl_leaveincr.SelectedItem.Text+"' , annual_leave = '"+ddl_annualleave.SelectedItem.Text+"' where v_LeaveName = '" + txt_leavename.Value + "'", myConnection);
    //        cmd.ExecuteNonQuery();
    //        myConnection.Close();
    //        load1();
    //        clear();
    //    }
    //    else
    //    {
    //        lbl_Error.Text = "<font color=Red>Error Occured</font>";
    //    }
    //}

    //public void clear()
    //{
    //    txt_leavename.Value = "";
    //    txt_LeaveCode.Value = "";
    //    txt_count.Value = "";
    //}

    protected void Update(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            //((TextBox)grid_Leave.Rows[e.RowIndex].FindControl("txtname")).Enabled = true;
            //((TextBox)grid_Leave.Rows[e.RowIndex].FindControl("txtcode")).Enabled = true;
            //((HtmlInputText)grid_Leave.Rows[e.RowIndex].FindControl("txtcount")).Disabled = false;
            //((ImageButton)grid_Leave.Rows[e.RowIndex].FindControl("img_save")).Visible = true;
            //((ImageButton)grid_Leave.Rows[e.RowIndex].FindControl("img_update")).Visible = false;
        }
        catch (Exception ex)
        {
            //lbl_Error.Text = "Error";
        }
    }

    protected void Edit(object sender, GridViewEditEventArgs e)
    {
 
    }

    
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        if (s_login_role == "h")
        {
            load1();
        }
        if (s_login_role == "a")
        {
            load_admin();
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "add")
        {
            string hcode = ((TextBox)GridView1.FooterRow.FindControl("txt_hcode")).Text;
            string hname = ((TextBox)GridView1.FooterRow.FindControl("txt_hname")).Text;
            string hfdate = ((TextBox)GridView1.FooterRow.FindControl("txt_fdate")).Text;
            string htdate = ((TextBox)GridView1.FooterRow.FindControl("txt_tdate")).Text;
            string days = ((Label)GridView1.FooterRow.FindControl("txt_days")).Text;
            string fdate = employee.Convert_ToSqlDatestring(hfdate);
            string tdate = employee.Convert_ToSqlDatestring(htdate);
            AddNewRecord(hcode, hname, fdate, tdate, days);
        }
    }

    private void AddNewRecord(string hcode, string hname, string fdate, string tdate, string days)
    {
        try
        {
            if (hcode != "" && hname != "" && fdate != "1900/01/01" && tdate != "1900/01/01")
            {
                check = name_validate1(hcode, fdate, tdate);
                if (check == 0)
                {
                    chkdate = l.fn_CheckHoliday(fdate, tdate);
                    if (chkdate == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter date within the financial year');", true);
                        return;
                    }
                    LeaveList = l.Get_year(l);
                    if (s_login_role == "a")
                    {
                        myConnection.Open();
                        string query = @"INSERT INTO paym_holiday (pn_CompanyID, pn_BranchID, pn_Holidaycode, pn_Holidayname,Fyear,From_date, To_date, days) VALUES('" + employee.CompanyId + "','" + DropDownList1.SelectedItem.Value + "','" + hcode + "','" + hname + "'," + LeaveList[0].year + ",'" + fdate + "','" + tdate + "','" + days + "')";
                        SqlCommand myCommand = new SqlCommand(query, myConnection);
                        myCommand.ExecuteNonQuery();
                        myConnection.Close();
                        load_admin();
                        access();
                    }
                    else if (s_login_role == "h")
                    {
                        string query = @"INSERT INTO paym_holiday (pn_CompanyID, pn_BranchID, pn_Holidaycode, pn_Holidayname,Fyear,From_date, To_date, days) VALUES('" + employee.CompanyId + "','" + employee.BranchId + "','" + hcode + "','" + hname + "'," + LeaveList[0].year + ",'" + fdate + "','" + tdate + "','" + days + "')";
                        SqlCommand myCommand = new SqlCommand(query, myConnection);
                        myConnection.Open();
                        myCommand.ExecuteNonQuery();
                        myConnection.Close();
                        load1();
                        access();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Holiday code already exist');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter all details');", true);
            }
            
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Entered date is invalid');", true);
        }

    }

    public int name_validate1(string e_name, string fdate, string tdate)
    {
        HolidayList = pay.fn_Holiday1(employee.BranchId);
        if (HolidayList.Count > 0)
        {
            for (valid = 0; valid < HolidayList.Count; valid++)
            {
                if (HolidayList[valid].holidayname.ToLower() == e_name.ToLower() && HolidayList[valid].holidaydate.ToString("dd/MM/yyyy") == fdate && HolidayList[valid].holidaytodate.ToString("dd/MM/yyyy") == tdate)
                {
                    temp_valid++;
                }
            }
        }
        return temp_valid;
    }

    public int date_validate(string e_name, string fdate, string tdate)
    {
        HolidayList = pay.fn_Holiday1(employee.BranchId);
        if (HolidayList.Count > 0)
        {
            for (valid = 0; valid < HolidayList.Count; valid++)
            {
                if (HolidayList[valid].holidayname.ToLower() == e_name.ToLower() && HolidayList[valid].holidaydate.ToString("dd/MM/yyyy") == fdate && HolidayList[valid].holidaytodate.ToString("dd/MM/yyyy") == tdate)
                {
                    temp_valid++;
                }
            }
        }
        return temp_valid;
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // loop all data rows
            foreach (DataControlFieldCell cell in e.Row.Cells)
            {
                // check all cells in one row
                foreach (Control control in cell.Controls)
                {
                    // Must use LinkButton here instead of ImageButton
                    // if you are having Links (not images) as the command button.
                    ImageButton button = control as ImageButton;
                    if (button != null && button.CommandName == "Delete")
                        // Add delete confirmation
                        button.OnClientClick = "if (!confirm('Are you sure " +
                               "you want to delete this record?')) return;";
                }
            }
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string ID = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lbl_holidaycode")).Text;

        DeleteRecord(ID);
        if (s_login_role == "h")
        {
            load1();
        }
        if (s_login_role == "a")
        {
            load_admin();
        }
        
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex; // turn to edit mode
        if (s_login_role == "h")
        {
            load1();
        }
        if (s_login_role == "a")
        {
            load_admin();
        }   
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow Gvrow = GridView1.Rows[e.RowIndex];

        if (Gvrow != null)
        {
            string holidaycode = ((Label)Gvrow.FindControl("lbl_holidaycode_edit")).Text;
            string holidayname = ((TextBox)Gvrow.FindControl("txt_holidayname_edit")).Text;
            string from_date = ((TextBox)Gvrow.FindControl("txt_from_date_edit")).Text;
            string to_date = ((TextBox)Gvrow.FindControl("txt_to_date_edit")).Text;
            string days = ((TextBox)Gvrow.FindControl("txt_days1")).Text;
            string f_date = employee.Convert_ToSqlDatestring(from_date);
            string t_date = employee.Convert_ToSqlDatestring(to_date);
           
            myConnection.Open();
            cmd = new SqlCommand("update paym_holiday set pn_holidayname='" + holidayname + "', from_date ='" + f_date + "', to_date ='" + t_date + "', days='" + days + "' where pn_holidaycode='" + holidaycode + "'", myConnection);
            cmd.ExecuteNonQuery();
            myConnection.Close();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Updated Successfully');", true);
            GridView1.EditIndex = -1;

            if (s_login_role == "h")
            {
                load1();
            }

            if (s_login_role == "a")
            {
                load_admin();
            }
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void DeleteRecord(string ID)
    {

        string sqlStatement = "DELETE FROM paym_holiday WHERE pn_holidaycode=@pn_holidaycode";
        try
        {
            myConnection.Open();
            SqlCommand cmd = new SqlCommand(sqlStatement, myConnection);
            cmd.Parameters.AddWithValue("@pn_holidaycode", ID);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Deletion Error:";
            msg += ex.Message;
            throw new Exception(msg);

        }
        finally
        {
            myConnection.Close();
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        load_admin();
    }

    public void load_admin()
    {
        myConnection.Open();
        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM paym_holiday where pn_branchid = '" + DropDownList1.SelectedItem.Value + "' ", myConnection);
        DataSet ds = new DataSet();
        ad.Fill(ds, "paym_holiday");

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
    }

    protected void Txt_fdate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int diff = 1;
            ((TextBox)GridView1.FooterRow.FindControl("Txt_tdate")).Text = ((TextBox)GridView1.FooterRow.FindControl("Txt_fdate")).Text;
            DateTime fr_date = Convert.ToDateTime(((TextBox)GridView1.FooterRow.FindControl("Txt_fdate")).Text);
            DateTime to_date = Convert.ToDateTime(((TextBox)GridView1.FooterRow.FindControl("Txt_tdate")).Text);
            diff = diff + (to_date - fr_date).Days;
            ((Label)GridView1.FooterRow.FindControl("txt_days")).Text = diff.ToString();
            //((TextBox)GridView1.FooterRow.FindControl("Txt_tdate")).Focus();
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Entered date is invalid');", true);
        }
    }
    protected void Txt_tdate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int diff = 1;
            DateTime fr_date = Convert.ToDateTime(((TextBox)GridView1.FooterRow.FindControl("Txt_fdate")).Text);
            DateTime to_date = Convert.ToDateTime(((TextBox)GridView1.FooterRow.FindControl("Txt_tdate")).Text);
            diff = diff + (to_date - fr_date).Days;
            ((Label)GridView1.FooterRow.FindControl("txt_days")).Text = diff.ToString();
            //((TextBox)GridView1.FooterRow.FindControl("txt_days")).Focus();
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Entered date is invalid');", true);
        }
    }
}
