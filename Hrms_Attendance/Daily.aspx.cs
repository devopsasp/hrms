using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using ePayHrms.Login;
using ePayHrms.BE.Recruitment;
using ePayHrms.Connection;
using ePayHrms.Candidate;
using System.IO;
using ePayHrms.Company;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ePayHrms.Leave;
using ePayHrms.Employee;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;


public partial class Hrms_Attendance_Default : System.Web.UI.Page
{
    ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand com = new SqlCommand();
    SqlDataReader rea;
    DataSet ds;
    Company company = new Company();
    Employee employee = new Employee();
    Leave l = new Leave();
    PayRoll pay = new PayRoll();
    
    Collection<Leave> LeaveList;
    Collection<Leave> LeaveGridList;
    Collection<Leave> LeaveMasterList;

    Collection<Company> CompanyList;
    Collection<Employee> EmployeeList;
    Collection<Company> ddlBranchsList;

    string query , empcode , empname , value;
    string[] split,yspl;
    string comid, year="";
    string s_login_role;
    int i, cur_yr, yr_it, ddl_i, temp_check2 = 0, empid;
    string s_form = "", _Value = "", str_query = "";
    DataSet ds_userrights, ds_LeaveAlloc;
    double tot_leave = 0, diff = 0;
    DateTime fd, td , now=DateTime.Now;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        l.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        s_login_role = Request.Cookies["Login_temp_Role"].Value;

        if (!IsPostBack)
        {
            CompanyList = company.fn_getCompany();
            ddl_year_load();

            if (CompanyList.Count > 0)
            {
                switch (s_login_role)
                {
                    case "a":
                        ddl_Branch_load();
                        ddl_employee_load();
                        ddl_Department_load();
                        Get_Year();
                        break;

                    case "h":
                        ddl_Branch.Visible = false;
                        ddl_employee_load();
                        ddl_Department_load();
                        ddl_leave_load();                        
                        Get_Year();
                        break;

                    case "e":
                        ddl_Branch.Visible = false;
                        ddl_employee_load();
                        ddl_leave_load();                        
                        emp();
                        Get_Year();
                        ddl_department.Enabled = false;
                        break;

                    case "u": s_form = "41";
                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);
                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            ddl_Branch.Visible = false;
                            ddl_employee_load();
                            ddl_department.Enabled = false;
                        }
                        else
                        {
                            ddl_Branch.Visible = false;
                            Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator.";
                            Response.Redirect("../Hrms_Attendance/Attendance_Home.aspx");
                        }
                        break;

                    default: Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                        Response.Redirect("Company_Home.aspx");
                        break;
                }
            }
            else
            {
                Response.Cookies["Msg_Session"].Value = "Create Company";
                Response.Redirect("Company_Home.aspx");
            }
        }
    }

    public void LeaveLoad()
    {
        LeaveList = l.fn_paym_leave1(employee.BranchId);
        if (LeaveList.Count > 0)
        {
            for (int i = 0; i <LeaveList.Count; i++)
            {
                ListItem lvlist=new ListItem();
                    lvlist.Text= LeaveList[i].leaveCode;
                    lvlist.Value = LeaveList[i].leaveID.ToString();                
                ((DropDownList)GridView1.FooterRow.FindControl("ddl_LeaveCode")).Items.Add(lvlist);
            }
        }
      }

    public void ddl_Department_load()
    {
        EmployeeList = employee.fn_getDepartmentList1(employee.BranchId);
        if (EmployeeList.Count > 0)
        {

            for (int ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {

                if (ddl_i == -1)
                {
                    ListItem es_list = new ListItem();

                    es_list.Text = "All";
                    es_list.Value = "0";
                    ddl_department.Items.Add(es_list);
                }
                else
                {
                    ListItem es_list = new ListItem();

                    es_list.Value = EmployeeList[ddl_i].DepartmentId.ToString();
                    es_list.Text = EmployeeList[ddl_i].DepartmentName.ToString();
                    ddl_department.Items.Add(es_list);
                }
            }
        }
    }

    public void Get_Year()
    {
        try
        {
            myConnection.Open();
            com = new SqlCommand("select * from paym_branch where pn_companyID = '" + l.CompanyID + "' and pn_BranchID = '" + l.BranchID + "'", myConnection);
            SqlDataReader rder = com.ExecuteReader();
            if (rder.Read())
            {
                year = Convert.ToDateTime(rder["End_Date"]).ToString("dd/MM/yyyy");
                yspl = year.Split('/');
                year = yspl[2];
            }
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured.');", true);
         
        }
        finally
        {
            myConnection.Close();
        }
    }

    public void emp()
    {
        
        employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);

        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM leave_apply where pn_employeeID='"+employee.EmployeeId+"' and yearend = '" + year + "' and record is null", myConnection);

        DataSet ds1 = new DataSet(); 

        ad.Fill(ds1, "leave_apply");

        if (ds1.Tables[0].Rows.Count == 0)
        {
            ds1.Tables[0].Rows.Add(ds1.Tables[0].NewRow());
            GridView1.DataSource = ds1;
            GridView1.DataBind();
            int columnCount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = 1;
            GridView1.Rows[0].Cells[0].Text = "";
        }
        else
        {
            GridView1.DataSource = ds1;
            GridView1.DataBind();
        }

    }

    public void hr()
    {
        value = ddl_Employee.SelectedItem.ToString();
        split = value.Split('-');
        empname = split[1].Trim();
        empcode = split[0].Trim();
        year = ddl_year.SelectedItem.Text;
        string lc = ddl_leave.SelectedItem.Text;
       // string lc = ((DropDownList)GridView1.FooterRow.FindControl("ddl_LeaveCode")).Text;
        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM leave_apply where emp_code='" + empcode + "' and pn_leavecode='" + lc + "' and DATEPART(YEAR, from_date) = '" + year + "'", myConnection);

        DataSet ds1 = new DataSet();

        ad.Fill(ds1, "leave_apply");
        

        if (ds1.Tables[0].Rows.Count == 0)
        {
            ds1.Tables[0].Rows.Add(ds1.Tables[0].NewRow());
            GridView1.DataSource = ds1;
            GridView1.DataBind();
            int columnCount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columnCount;
            GridView1.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            GridView1.DataSource = ds1;
            GridView1.DataBind();
        }
    }
    protected void btn_Back_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void ddl_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddl_Branch.SelectedValue != "0")
            {
                ViewState["Leave_BranchID"] = Convert.ToInt32(ddl_Branch.SelectedItem.Value);
                
                ddl_employee_load();
                ddl_leave_load();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured.');", true);
        }
    }

    public void ddl_Branch_load()
    {

        ddlBranchsList = company.fn_getBranchs();
        if (ddlBranchsList.Count > 0)
        {
            for (ddl_i = -1; ddl_i < ddlBranchsList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem list = new ListItem();

                    list.Text = "Select Branch";
                    list.Value = "0";
                    ddl_Branch.Items.Add(list);
                }
                else
                {
                    ListItem list = new ListItem();

                    list.Text = ddlBranchsList[ddl_i].CompanyName;
                    list.Value = ddlBranchsList[ddl_i].CompanyId.ToString();
                    ddl_Branch.Items.Add(list);
                }
            }
        }
    }

    public void ddl_employee_load()
    {
        ddl_Employee.Items.Clear();

        if (s_login_role == "a")
        {
            //employee.BranchId = (int)ViewState["Leave_BranchID"];
            l.BranchID = Convert.ToInt32(ddl_Branch.SelectedItem.Value);
            EmployeeList = employee.fn_getEmployeeList(employee);
            if (EmployeeList.Count > 0)
            {
                ddl_Employee.Enabled = true;
                for (ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
                {
                    if (ddl_i == -1)
                    {
                        ListItem e_list = new ListItem();

                        e_list.Text = "Select Employee";
                        e_list.Value = "0";
                        ddl_Employee.Items.Add(e_list);
                    }
                    else
                    {
                        ListItem e_list = new ListItem();

                        e_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
                        e_list.Text = EmployeeList[ddl_i].LastName.ToString();
                        ddl_Employee.Items.Add(e_list);
                    }
                }
            }
            else
            {
                ddl_Employee.Enabled = false;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Employees Available.');", true);
             
            }
        }

        if (s_login_role == "h")
        {
            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            EmployeeList = employee.fn_getEmployeeList(employee);
            if (EmployeeList.Count > 0)
            {
                ddl_Employee.Enabled = true;
                for (ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
                {
                    if (ddl_i == -1)
                    {
                        ListItem e_list = new ListItem();

                        e_list.Text = "Select Employee";
                        e_list.Value = "0";
                        ddl_Employee.Items.Add(e_list);
                    }
                    else
                    {
                        ListItem e_list = new ListItem();

                        e_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
                        e_list.Text = EmployeeList[ddl_i].LastName.ToString();
                        ddl_Employee.Items.Add(e_list);
                    }
                }
            }
            else
            {
                ddl_Employee.Enabled = false;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Employees Available.');", true);
                
            }
        }


        if (s_login_role == "e")
        {
            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
            EmployeeList = employee.fn_getEmployeeList1(employee);
            if (EmployeeList.Count > 0)
            {
                ListItem e_list = new ListItem();

                e_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
                e_list.Text = EmployeeList[ddl_i].LastName.ToString();
                ddl_Employee.Items.Add(e_list);
               
            }
            else
            {
                ddl_Employee.Enabled = false;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Employees Available.');", true);
            }
        }
        
    }

    public void ddl_Employee_load()
    {
        ddl_Employee.Items.Clear();
        employee.DepartmentId = Convert.ToInt32(ddl_department.SelectedValue);
        EmployeeList = employee.fn_getEmployeeDepartment(employee);
        if (EmployeeList.Count > 0)
        {
            for (int ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem es_list = new ListItem();
                    es_list.Text = "Select Employee";
                    es_list.Value = "0";
                    ddl_Employee.Items.Add(es_list);
                }
                else
                {
                    ListItem es_list = new ListItem();
                    es_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
                    es_list.Text = EmployeeList[ddl_i].LastName.ToString();
                    ddl_Employee.Items.Add(es_list);
                }
            }
        }
    }

    public void ddl_leave_load()
    {
        LeaveList = l.fn_paym_leave1(employee.BranchId);
        if (LeaveList.Count > 0)
        {
            ddl_leave.DataSource = LeaveList;
            ddl_leave.DataTextField = "leaveCode";
            ddl_leave.DataValueField = "leaveid";
            ddl_leave.DataBind();
        }
    }

    public void Aban_Company_Use()
    {

        //after 1 year completed,then leave will be Allot automatically

        string query = "";
        query = "select * from paym_Employee_WorkDetails where pn_EmployeeID=" + ddl_Employee.SelectedItem.Value + " and datediff(yy,JoiningDate,getdate()) >=1";
        ds_LeaveAlloc = pay.fn_Output(query);

        if (ds_LeaveAlloc.Tables[0].Rows.Count > 0)
        {

            LeaveMasterList = l.fn_paym_leave(l);
            if (LeaveMasterList.Count > 0)
            {
                for (i = 0; i < LeaveMasterList.Count; i++)
                {
                    //Insert into LeaveAllocation
                    l.leaveID = LeaveMasterList[i].leaveID;
                    l.Count = LeaveMasterList[i].Count;

                    _Value = l.Leave_Allocation(l);
                    if (LeaveMasterList[i].leaveID == temp_check2)
                    {
                        ddl_Branch.Enabled = false;
                        ddl_Employee.Enabled = false;
                        ddl_leave.Enabled = false;
                        ddl_year.Enabled = false;

                        txt_Allowed.Text = LeaveMasterList[i].Count.ToString();
                        txt_Balance.Text = LeaveMasterList[i].Count.ToString();
                    }
                }
            }
        }
        else
        {
            normal_state();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Leave Not Allotted for this Employee.');", true);
        
        }
    }

    public DateTime Convert_ToSqlDateformat(string cur_date)
    {

        string _d, _m, _y;
        DateTime sql_date;
        if (cur_date != "")
        {
            string[] da = cur_date.Split('/');
            _d = da[0];
            _m = da[1];
            _y = da[2];

            sql_date = Convert.ToDateTime(_y + "/" + _m + "/" + _d);
        }
        else
        {
            sql_date = Convert.ToDateTime("1900/01/01");
        }
        return sql_date;
    }


    protected void delete(object sender, GridViewDeleteEventArgs e)
    {
        l.EmployeeID = Convert.ToInt32(ddl_Employee.SelectedItem.Value);
        l.leaveID = Convert.ToInt32(ddl_leave.SelectedItem.Value);
        fd = Convert_ToSqlDateformat(((TextBox)grid_leave.Rows[e.RowIndex].FindControl("grdfromdate")).Text);
        td = Convert_ToSqlDateformat(((TextBox)grid_leave.Rows[e.RowIndex].FindControl("grdtodate")).Text);

        str_query = "delete from paym_Employee_leave where pn_EmployeeID=" + l.EmployeeID + " and pn_leaveID=" + l.leaveID + " and From_Date='" + fd.ToShortDateString() + "' and To_Date='" + td.ToShortDateString() + "'";
        employee.fn_temp_table(str_query);
        l.year = Convert.ToInt32(ddl_year.SelectedItem.Text);
        load();
    }

    public void ddl_year_load()
    {
        try
        {
            i = 0;
            cur_yr = DateTime.Now.Year;
            //cur_yr = cur_yr + 1;

            for (yr_it = cur_yr - 5; yr_it <= cur_yr + 1; yr_it++)
            {
                ddl_year.Items.Add(Convert.ToString(yr_it));
                i++;
            }
            i = i - 1;

            ddl_year.SelectedIndex = i - 1;
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured.');", true);
        }
    }

    public void load()
    {
        Get_Year();
        l.To_year = Convert.ToInt32(year);
        LeaveList = l.Check_leaveAllocation(l);
        txt_Taken.Text = "0";
        txt_Balance.Text = "0";
        txt_Allowed.Text = "0";
        if (LeaveList.Count > 0)
        {
            ddl_Branch.Enabled = false;
            ddl_Employee.Enabled = true;
            ddl_leave.Enabled = true;
            ddl_year.Enabled = true;

            txt_Allowed.Text = LeaveList[0].Count1.ToString();

            LeaveGridList = l.fn_leave_PerYear(l);
            if (LeaveGridList.Count > 0)
            {
                grid_leave.Visible = true;
                grid_leave.DataSource = LeaveGridList;
                grid_leave.DataBind();
                
                for (i = 0; i < LeaveGridList.Count; i++)
                {
                    tot_leave = tot_leave + LeaveGridList[i].Cur_Leave;
                }
                txt_Taken.Text = Convert.ToString(tot_leave);
                txt_Balance.Text = Convert.ToString(Convert.ToDouble(txt_Allowed.Text) - Convert.ToDouble(txt_Taken.Text));
            }
            else
            {
                grid_leave.Visible = false;
                txt_Taken.Text = "0";
                txt_Balance.Text = txt_Allowed.Text;
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Leave Not Allotted for this Employee.');", true);
          
            Aban_Company_Use();
        }
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        normal_state();
    }
    public void normal_state()
    {
        ddl_Branch.Enabled = true;
        ddl_Employee.Enabled = true;
        ddl_leave.Enabled = true;
        ddl_year.Enabled = true;
        
        //Switch to grid in null position
        l.EmployeeID = 0;
        LeaveList = l.Employee_leaveAllocation(l);

        grid_leave.DataSource = LeaveList;
        grid_leave.DataBind();
    }    
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "add")
            {
                empid = Convert.ToInt32(ddl_Employee.SelectedItem.Value);
                comid = employee.CompanyId.ToString();
                string leavename = "";
                string branid = employee.BranchId.ToString();
                value = ddl_Employee.SelectedItem.ToString();
                split = value.Split('-');
                empname = split[1].Trim();
                empcode = split[0].Trim();
                string fromdate = ((TextBox)GridView1.FooterRow.FindControl("txt_fromdate2")).Text;
                string status1 = ((DropDownList)GridView1.FooterRow.FindControl("DropDownList21")).SelectedItem.Value;
                string prior = "";
                string todate = ((TextBox)GridView1.FooterRow.FindControl("txt_todate2")).Text;
                string subm = DateTime.Now.ToString("MM/dd/yyyy");
                string rea = ((TextBox)GridView1.FooterRow.FindControl("txt_rea")).Text;
                decimal day = Convert.ToDecimal(((TextBox)GridView1.FooterRow.FindControl("txt_days")).Text);
                string status = ((DropDownList)GridView1.FooterRow.FindControl("DropDownList2")).SelectedItem.Value;
                //string leavecode = ddl_leave.SelectedItem.Text;
                string leavecode = ((DropDownList)GridView1.FooterRow.FindControl("ddl_LeaveCode")).SelectedItem.Text;
                string leaveid = ddl_leave.SelectedItem.Value;

                AddNewRecord(comid, branid, empid, leaveid, empcode, empname, fromdate, todate, status, status1, day, subm, rea, prior, leavename, leavecode);
            }
        }
        catch
        {

        }

        finally
        {
            myConnection.Close();
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string ID = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("Label13")).Text;
        DeleteRecord(ID);
        if (s_login_role == "h")
        {
            hr();
        }
        else if (s_login_role == "e")
        {
            emp();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex; // turn to edit mode
        if (s_login_role == "h")
        {
            hr();
        }
        else if (s_login_role == "e")
        {
            emp();
        }
    }

    private void AddNewRecord(string comid, string branid, Int32 empid, string leaveid, string empcode, string empname, string fromdate, string todate, string status, string status1, Decimal day, string subm, string rea, string prior, string leavename, string leavecode)
    {
        
        string approve="Yes";
        string appr = "Pending";
        Get_Year();
        string[] datesplit1 = fromdate.Split('/', '-');
        string dd1 = datesplit1[0].ToString();
        string mm1 = datesplit1[1].ToString();
        string yy1 = datesplit1[2].ToString();
        string fr_date = mm1 + "/" + dd1 + "/" + yy1;
        string[] datesplit2 = todate.Split('/', '-');
        string dd2 = datesplit2[0].ToString();
        string mm2 = datesplit2[1].ToString();
        string yy2 = datesplit2[2].ToString();
        string to_date = mm2 + "/" + dd2 + "/" + yy2;
        string[] datesplit3 = subm.Split('/', '-');
        string dd3 = datesplit3[0].ToString();
        string mm3 = datesplit3[1].ToString();
        string yy3 = datesplit3[2].ToString();
        string sb_date = mm3 + "/" + dd3 + "/" + yy3;
        
        SqlCommand myCommand = new SqlCommand();
        try
        {
            myConnection.Open();
            if (s_login_role == "e")
            {
                query = @"INSERT INTO leave_apply (pn_CompanyID,pn_BranchID,pn_EmployeeID,pn_LeaveID,pn_Leavecode,Emp_Code,Emp_Name,from_date,to_date,status,Submitted_date,Reason,Approve,from_status,days,priority,pn_LeaveName,yearend) VALUES('" + comid + "','" + branid + "','" + empid + "','" + leaveid + "','" + leavecode + "','" + empcode + "','" + empname + "','" + fr_date + "','" + to_date + "','" + status + "','" + sb_date + "','" + rea + "','" + appr + "','" + status1 + "','" + day + "','" + prior + "','" + leavename + "','" + year + "')";

                myCommand = new SqlCommand(query, myConnection);

                myCommand.ExecuteNonQuery();

                emp();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Leave Submitted Successfully.');", true);

               
            }
            else if (s_login_role == "h")
            {
                query = @"INSERT INTO leave_apply (pn_CompanyID,pn_BranchID,pn_EmployeeID,pn_LeaveID,pn_Leavecode,Emp_Code,Emp_Name,from_date,to_date,status,Submitted_date,Reason,Approve,from_status,days,priority,pn_LeaveName,flag,yearend) VALUES('" + comid + "','" + branid + "','" + empid + "','" + leaveid + "','" + leavecode + "','" + empcode + "','" + empname + "','" + fr_date + "','" + to_date + "','" + status + "','" + subm + "','" + rea + "','" + approve + "','" + status1 + "','" + day + "','" + prior + "','" + leavename + "','Y','" + year + "')";

                myCommand = new SqlCommand(query, myConnection);

                myCommand.ExecuteNonQuery();

                hr();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Leave Submitted Successfully.');", true);

            
            }
        }
        catch (Exception e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured.');", true);
        }
        finally
        {
            myConnection.Close();
        }

    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow Gvrow = GridView1.Rows[e.RowIndex];
        if (Gvrow != null)
        {
            Get_Year();
            string snoedit = ((Label)Gvrow.FindControl("txt_sno")).Text;
            string fromedit = ((TextBox)Gvrow.FindControl("txt_fromdate1")).Text;
            string toedit = ((TextBox)Gvrow.FindControl("txt_todate1")).Text;
            string submedit = DateTime.Now.ToString("dd/MM/yyyy");
            string tstatedit = ((DropDownList)Gvrow.FindControl("ddl_stat1")).Text;
            string statedit = ((DropDownList)Gvrow.FindControl("ddl_stat")).Text;
            string reaedit = ((TextBox)Gvrow.FindControl("txt_rea1")).Text;
            string LeaveCode = ((Label)Gvrow.FindControl("ddl_LeaveCodeedit")).Text;
            string days = ((TextBox)Gvrow.FindControl("txt_days1")).Text;
            string E_fromedit = employee.Convert_ToSqlDatestring(fromedit);
            string E_toedit = employee.Convert_ToSqlDatestring(toedit);
            string E_submedit = employee.Convert_ToSqlDatestring(submedit);
            myConnection.Open();
            com = new SqlCommand("update leave_apply set from_date='" + E_fromedit + "', to_date ='" + E_toedit + "', submitted_date='" + E_submedit + "', Status ='" + statedit + "',from_status= '" + tstatedit + "', pn_leavecode='" + LeaveCode + "',days='" + days + "',Reason ='" + reaedit + "' where Sno='" + snoedit + "' and yearend = '" + year + "'", myConnection);
            com.ExecuteNonQuery();
            myConnection.Close();
            GridView1.EditIndex = -1; // turn to edit mode
            if (s_login_role == "h")
            {
                hr();
            }
            else if (s_login_role == "e")
            {
                emp();
            }

        }
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        if (s_login_role == "h")
        {
            hr();
        }
        else if (s_login_role == "e")
        {
            emp();
        }
    }

    private void DeleteRecord(string ID)
    {
        string d = "Deleted";
        string sqlStatement = "delete from leave_apply WHERE Sno = @Sno";
        try
        {
            myConnection.Open();
            SqlCommand cmd = new SqlCommand(sqlStatement, myConnection);
            cmd.Parameters.AddWithValue("@Sno", ID);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Deleted Successfully.');", true);
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
          
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Deletion Error:"+ ex + "');", true);

        }
        finally
        {
            myConnection.Close();
        }
    }

    protected void txt_fromdate_TextChanged(object sender, EventArgs e)
    {
        ((TextBox)GridView1.FooterRow.FindControl("txt_todate2")).Text = ((TextBox)GridView1.FooterRow.FindControl("txt_fromdate2")).Text;
    }

    protected void rdo_Leavetype_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime fr_date = Convert.ToDateTime(((TextBox)GridView1.FooterRow.FindControl("txt_fromdate2")).Text);
        DateTime to_date = Convert.ToDateTime(((TextBox)GridView1.FooterRow.FindControl("txt_todate2")).Text);
        diff = (to_date - fr_date).Days;
     
        if (((DropDownList)GridView1.FooterRow.FindControl("DropDownList21")).Text == "FD" && ((DropDownList)GridView1.FooterRow.FindControl("DropDownList2")).Text == "FD")
        {
            diff = diff + 1;
        }
        else if (((DropDownList)GridView1.FooterRow.FindControl("DropDownList21")).Text != "FD" && ((DropDownList)GridView1.FooterRow.FindControl("DropDownList2")).Text == "FD")
        {
            diff = diff + 0.5;
        }
        else if (((DropDownList)GridView1.FooterRow.FindControl("DropDownList21")).Text == "FD" && ((DropDownList)GridView1.FooterRow.FindControl("DropDownList2")).Text != "FD")
        {
            diff = diff + 0.5;
        }
        else if (((DropDownList)GridView1.FooterRow.FindControl("DropDownList21")).Text == "FH" && ((DropDownList)GridView1.FooterRow.FindControl("DropDownList2")).Text == "FH")
        {
            diff = diff + 0.5;
        }
        
        ((TextBox)GridView1.FooterRow.FindControl("txt_days")).Text = diff.ToString();
        ((TextBox)GridView1.FooterRow.FindControl("txt_days")).Focus();
    }

    protected void DropDownList21_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (((DropDownList)GridView1.FooterRow.FindControl("DropDownList21")).Text == "First Half")
        {
            diff = 0.5;
            ((TextBox)GridView1.FooterRow.FindControl("txt_todate2")).Enabled = false;
            ((DropDownList)GridView1.FooterRow.FindControl("DropDownList2")).Enabled = false;
        }
        else
        {
            ((TextBox)GridView1.FooterRow.FindControl("txt_todate2")).Enabled = true;
            ((DropDownList)GridView1.FooterRow.FindControl("DropDownList2")).Enabled = true;
        }
        
        ((TextBox)GridView1.FooterRow.FindControl("txt_days")).Text = diff.ToString();
        ((TextBox)GridView1.FooterRow.FindControl("txt_days")).Focus();
        ((DropDownList)GridView1.FooterRow.FindControl("DropDownList2")).Focus();
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    
    protected void ddl_stat1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList dd1 = (DropDownList)sender;
        GridViewRow row = (GridViewRow)dd1.NamingContainer;
        int idx = row.RowIndex;

        if (((DropDownList)GridView1.Rows[idx].Cells[2].FindControl("ddl_stat1")).Text == "First Half")
            
        {
            diff = 0.5;
            ((TextBox)GridView1.Rows[idx].Cells[3].FindControl("txt_todate1")).Enabled = false;
            ((DropDownList)GridView1.Rows[idx].Cells[4].FindControl("ddl_stat")).SelectedItem.Text = ((DropDownList)GridView1.Rows[idx].Cells[2].FindControl("ddl_stat1")).SelectedItem.Text;
            ((DropDownList)GridView1.Rows[idx].Cells[4].FindControl("ddl_stat")).Enabled = false;
        }
        else
        {
            ((TextBox)GridView1.Rows[idx].Cells[3].FindControl("txt_todate1")).Enabled = true;
            ((DropDownList)GridView1.Rows[idx].Cells[4].FindControl("ddl_stat")).Enabled = true;
        }

        ((TextBox)GridView1.Rows[idx].Cells[5].FindControl("txt_days1")).Text = diff.ToString();
        ((TextBox)GridView1.Rows[idx].Cells[5].FindControl("txt_days1")).Focus();
    }


    protected void ddl_stat_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList dd1 = (DropDownList)sender;
        GridViewRow row =  (GridViewRow)dd1.NamingContainer;
        int idx = row.RowIndex;

        DateTime fr_date = Convert.ToDateTime(((TextBox)GridView1.Rows[idx].Cells[1].FindControl("txt_fromdate1")).Text);
        DateTime to_date = Convert.ToDateTime(((TextBox)GridView1.Rows[idx].Cells[3].FindControl("txt_todate1")).Text);
        diff = (to_date - fr_date).Days;

        if (((DropDownList)GridView1.Rows[idx].Cells[2].FindControl("ddl_stat1")).Text == "Full Day" && ((DropDownList)GridView1.Rows[idx].Cells[4].FindControl("ddl_stat")).Text == "Full Day")
        {
            diff = diff + 1;
        }
        else if (((DropDownList)GridView1.Rows[idx].Cells[2].FindControl("ddl_stat1")).Text != "Full Day" && ((DropDownList)GridView1.Rows[idx].Cells[4].FindControl("ddl_stat")).Text == "Full Day")
        {
            diff = diff + 0.5;
        }
        else if (((DropDownList)GridView1.Rows[idx].Cells[2].FindControl("ddl_stat1")).Text == "Full Day" && ((DropDownList)GridView1.Rows[idx].Cells[4].FindControl("ddl_stat")).Text != "Full Day")
        {
            diff = diff + 0.5;
        }

        ((TextBox)GridView1.Rows[idx].Cells[5].FindControl("txt_days1")).Text = diff.ToString();
        ((TextBox)GridView1.Rows[idx].Cells[5].FindControl("txt_days1")).Focus();
        ((DropDownList)GridView1.FooterRow.FindControl("ddl_stat")).Focus();
    }

    protected void txt_fromdate1_TextChanged(object sender, EventArgs e)
    {
        ((TextBox)GridView1.Rows[0].Cells[3].FindControl("txt_todate1")).Text = ((TextBox)GridView1.Rows[0].Cells[1].FindControl("txt_fromdate1")).Text;       
    }
    protected void btn_Details_Click(object sender, EventArgs e)
    {
        try
        {
            leave_entry.Visible = true;
            leave_history.Visible = true;
            l.EmployeeID = Convert.ToInt32(ddl_Employee.SelectedItem.Value);
            l.leaveID = Convert.ToInt32(ddl_leave.SelectedItem.Value);
            temp_check2 = Convert.ToInt32(ddl_leave.SelectedItem.Value);
            l.year = Convert.ToInt32(ddl_year.SelectedItem.Text);
            load();
            if ((s_login_role == "h" || s_login_role == "e") && Convert.ToDouble(txt_Balance.Text) >= 0)
            {
                hr();
                LeaveLoad();
                
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured.');", true);
        }
        

    }
    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Employee_load();
    }
}
