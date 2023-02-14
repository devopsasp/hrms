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
using System.Drawing;

public partial class Hrms_Company_Default : System.Web.UI.Page
{

    ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader rea;
    DataSet ds;
    
    Company company = new Company();
    Employee employee = new Employee();
    Leave l = new Leave();
    PayRoll pay = new PayRoll();

    Collection<Leave> LeaveList;
    Collection<Leave> LeaveMasterList;
   
    Collection<Company> CompanyList;
    Collection<Employee> EmployeeList;
    Collection<Company> ddlBranchsList;
    

    string _Code;
    string s_login_role;
    int i, j, cur_yr, ddl_i,temp_check = 0;
    bool avail = false,leave_check=false;
    string s_form = "", str_date = "", _Value="",query="";
    double temp_count = 0;
    DataSet ds_userrights,ds_leavecount;


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

            if (CompanyList.Count > 0)
            {

                ddl_year_load();

                
                switch (s_login_role)
                {

                    case "a": row_emp.Visible = false;
                              row_month.Visible = false;
                              row_year.Visible = false;
                              ddl_Branch_load();
                        ddl_employee_load();
                        lreq.Visible = false;
                        break;

                    case "h": ddl_Branch.Visible = false;
                              ddl_Department_load();
                              ddl_employee_load();
                        lreq.Visible = false;
                        break;

                    case "e": ddl_Branch.Visible = false;
                              row_emp.Visible = false;
                              lreq.Visible = false;
                        ddl_department.Enabled = false;
                              break;

                    case "u": s_form = "42";
                           
                             ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                             if (ds_userrights.Tables[0].Rows.Count > 0)
                             {
                                 ddl_Branch.Visible = false;
                                 ddl_employee_load();
                             }
                             else
                             {
                                 ddl_Branch.Visible = false;
                                 row_emp.Visible = false;
                                 Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator.";
                                 Response.Redirect("../Hrms_Attendance/Attendance_Home.aspx");
                             }

                              break;

                    default: Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                        Response.Redirect("~/Company_Home.aspx");
                        break;
                }
            }
            else
            {
                Response.Cookies["Msg_Session"].Value = "Create Company";
                Response.Redirect("~/Company_Home.aspx");
            }
            }    
    }


    public void hr_edit()
    {
        SqlDataAdapter adap = new SqlDataAdapter("select * from leave_apply where pn_BranchID='" + employee.BranchId + "'", myConnection);
        DataSet ds1 = new DataSet();
        adap.Fill(ds1, "leave_apply");
        //if (ds1.Tables[0].Rows.Count == 0)
        //{
        //    ds1.Tables[0].Rows.Add(ds1.Tables[0].NewRow());
        //    GridView2.DataSource = ds1;
        //    GridView2.DataBind();
        //    int columnCount = GridView2.Rows[0].Cells.Count;
        //    GridView2.Rows[0].Cells.Clear();
        //    GridView2.Rows[0].Cells.Add(new TableCell());
        //    GridView2.Rows[0].Cells[0].ColumnSpan = columnCount;
        //    GridView2.Rows[0].Cells[0].Text = "No Records Found..";
        //}
        //else
        //{
            GridView2.DataSource = ds1;
            GridView2.DataBind();
        //}

        myConnection.Close();
    }


    public void ddl_Branch_load()
    {
        //branck dropdown

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
  
    protected void btn_Back_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("~/Company_Home.aspx");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured.');", true);
        }
    }

    protected void ddl_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddl_Branch.SelectedValue != "0")
            {
                ViewState["Leave_BranchID"] = Convert.ToInt32(ddl_Branch.SelectedItem.Value);

                row_emp.Visible = true;
                row_month.Visible = true;
                row_year.Visible = true;
                grid_leave.Visible = false;              
                ddl_employee_load();
                ddl_Department_load();

            }

            else
            {
                row_emp.Visible = false;
                row_month.Visible = false;
                row_year.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured.');", true);
        }
    }

    public void ddl_employee_load()
    {
        //employee dropdown

        ddl_Employee.Items.Clear();

        //row_emp.Visible = true;
        //if (s_login_role == "a")
        //{
        //    employee.BranchId = (int)ViewState["Leave_BranchID"];
        //}   

        if (s_login_role == "h")
        {
            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        }

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


    protected void btn_view_Click(object sender, EventArgs e)
    {
        try
        {
            get_value();
            
            grid_load();

            if (leave_check == true)
            {
                Leaveallocation();
                month_details();
            }
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured.');", true);
        }
    }

    protected void btn_year_Click(object sender, EventArgs e)
    {
        try
        {
            get_value();

            grid_load();

            if (leave_check == true)
            {
                Leaveallocation();
                year_details();
                Leave_History();
            }
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured.');", true);
        }
    }

    public void Leave_History()
    {
        SqlDataAdapter adap = new SqlDataAdapter("select * from leave_apply where pn_BranchID='" + employee.BranchId + "' and  pn_EmployeeID = '" + l.EmployeeID + "' and DATEPART(YEAR, from_date) = '" + ddl_Year.Text + "'", myConnection);
        DataSet ds1 = new DataSet();
        adap.Fill(ds1, "leave_apply");
        Grid_Details.DataSource = ds1;
        Grid_Details.DataBind();
    }

    public void get_value()
    {
        switch (s_login_role)
        {
            case "a": l.BranchID = Convert.ToInt32(ddl_Branch.SelectedItem.Value);

                l.EmployeeID = Convert.ToInt32(ddl_Employee.SelectedItem.Value);

                l.year = Convert.ToInt32(ddl_Year.SelectedItem.Value);

                l.month = Convert.ToInt32(ddl_Month.SelectedItem.Value);

                break;

            case "h": l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

                l.EmployeeID = Convert.ToInt32(ddl_Employee.SelectedItem.Value);

                l.year = Convert.ToInt32(ddl_Year.SelectedItem.Value);

                l.month = Convert.ToInt32(ddl_Month.SelectedItem.Value);

                break;

            case "e": l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

                l.EmployeeID = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);

                l.year = Convert.ToInt32(ddl_Year.SelectedItem.Value);

                l.month = Convert.ToInt32(ddl_Month.SelectedItem.Value);

                break;

            default: Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                Response.Redirect("Company_Home.aspx");
                break;
        }
    }    

    public void grid_load()
    {
        
   //column(leave code)

        LeaveMasterList = l.fn_paym_leave1(employee.BranchId);

        if (LeaveMasterList.Count > 0)
        {
            grid_leave.Visible = true;
            grid_leave.DataSource = LeaveMasterList;
            grid_leave.DataBind();

            leave_check = true;
        }
        else
        {
      
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Data found in LeaveMaster.');", true);
            leave_check = false;
        }
    }

    public void Leaveallocation()
    {
 //column(Total Days)

        LeaveList = l.fn_emp_leaveAllocation(l);

        if (LeaveList.Count > 0)
        {
            //leave_check = true;

            for (i = 0; i < LeaveList.Count; i++)
            {
                if (Convert.ToInt32(grid_leave.DataKeys[i].Value) == LeaveList[i].leaveID)
                {
                    ((Label)grid_leave.Rows[i].FindControl("grdallowed")).Text = LeaveList[i].Count1.ToString();
                }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "Leave Not Allotted for this Employee.');", true);
        }
    }

    public void year_details()
    {
 //column(availed Date(year))      
        for (i = 0; i < grid_leave.Rows.Count; i++)
        {
            l.leaveID = Convert.ToInt32(grid_leave.DataKeys[i].Value);
            l.year = Convert.ToInt32(ddl_Year.SelectedItem.Value);
            LeaveList = l.fn_leave_PerYear(l);

            if (LeaveList.Count > 0)
            {
                for (j = 0; j < LeaveList.Count; j++)
                {
                    if (temp_check == 0)
                    {
                        str_date = "( " + LeaveList[j].str_fromdate + " - " + LeaveList[j].str_todate + " )" + System.Environment.NewLine;
                        temp_count = LeaveList[j].Cur_Leave;
                        temp_check++;
                    }
                    else
                    {
                        str_date = str_date + "( " + LeaveList[j].str_fromdate + " - " + LeaveList[j].str_todate + " )" + System.Environment.NewLine;
                        temp_count = temp_count + LeaveList[j].Cur_Leave;

                    }
                }

               ((TextBox)grid_leave.Rows[i].FindControl("grddate")).Text = str_date;
               ((Label)grid_leave.Rows[i].FindControl("grdcount")).Text = temp_count.ToString();                                                     

                    temp_count = 0;
                    temp_check = 0;
                    str_date = "";
            }
        }       
    }

    public void month_details()
    {
       
 //column(availed Days(Month))

        for (i = 0; i < grid_leave.Rows.Count; i++)
        {
            l.leaveID = Convert.ToInt32(grid_leave.DataKeys[i].Value);

            LeaveList = l.fn_leave_PerMonth(l);

            if (LeaveList.Count > 0)
            {
                for (j = 0; j < LeaveList.Count; j++)
                {
                    if (temp_check == 0)
                    {
                        str_date = "( " + LeaveList[j].str_fromdate + " - " + LeaveList[j].str_todate + " )";
                        temp_count = LeaveList[j].Cur_Leave;
                        temp_check++;
                    }
                    else
                    {
                        str_date = str_date + " ( " + LeaveList[j].str_fromdate + " - " + LeaveList[j].str_todate + " )";
                        temp_count = temp_count + LeaveList[j].Cur_Leave;
                    }
                }
                ((TextBox)grid_leave.Rows[i].FindControl("grddate")).Text = str_date;
                ((Label)grid_leave.Rows[i].FindControl("grdcount")).Text = temp_count.ToString();

                temp_count = 0;
                temp_check = 0;
                str_date = "";
            }
        }
    }

    public void clear()
    {
        //txt_leavename.Value = "";
        //txt_LeaveCode.Value = "";
        //txt_count.Value = "";
    }

    public void ddl_year_load()
    {
        try
        {
            i = 0;
            cur_yr = DateTime.Now.Year;
            cur_yr = cur_yr + 5;

            for (int yr_it = 1990; yr_it <= cur_yr; yr_it++)
            {
                ddl_Year.Items.Add(Convert.ToString(yr_it));
                i++;
            }

            i = i - 6;
            //current year is selected index
            ddl_Year.SelectedIndex = i;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured.');", true);
        }
    }

    protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView2.EditIndex = -1;
        hr_edit();
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string sno = ((Label)GridView2.Rows[e.RowIndex].Cells[0].FindControl("Labtask1")).Text;
        DeleteRecord(sno);
        hr_edit();
    }
    private void DeleteRecord(string ID)
    {
        string sqlStatement = "DELETE FROM leave_apply WHERE Sno = @Sno";
        try
        {
            myConnection.Open();
            SqlCommand cmd = new SqlCommand(sqlStatement, myConnection);
            cmd.Parameters.AddWithValue("@Sno", ID);
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
    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView2.EditIndex = e.NewEditIndex; // turn to edit mode
        hr_edit();
    }
    protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow Gvrow1 = GridView2.Rows[e.RowIndex];
        
        if (Gvrow1 != null)
        {
            string appr, perm, reminder, flag;
            string subedit1 = ((Label)Gvrow1.FindControl("Labelsubedit1")).Text;
            DropDownList ddl1 = ((DropDownList)GridView2.Rows[e.RowIndex].FindControl("DropDownList2"));
            string com = ((TextBox)Gvrow1.FindControl("txtcom")).Text;
            string remind = ((TextBox)Gvrow1.FindControl("txt_remind")).Text;
            if (remind == "")
            {
                reminder = "";
            }
            else
            {
                string[] datesplit = remind.Split('/', '-');
                string dd = datesplit[0].ToString();
                string mm = datesplit[1].ToString();
                string yy = datesplit[2].ToString();
                reminder = mm + "/" + dd + "/" + yy;
            }
            if (ddl1.Text == "Yes")
            {
                flag = "Y";
            }
            else
            {
                flag = "N";
            }
            myConnection.Open();
            cmd = new SqlCommand("update leave_apply set Approve='" + ddl1.Text + "', Reminder='" + reminder + "', Comments='" + com + "', flag = '" + flag + "' where Sno='" + subedit1 + "'", myConnection);
            cmd.ExecuteNonQuery();
            myConnection.Close();

            GridView2.EditIndex = -1; // turn to edit mode
            hr_edit();
            

            ClientScriptManager manager = Page.ClientScript;

            manager.RegisterStartupScript(this.GetType(), "Call", "show_message1('Leave Details Updated Sucessfully');", true);


        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.DataItem != null)
            {
                if (((string)DataBinder.Eval(e.Row.DataItem, "Approve")) == "Pending")
                {
                    e.Row.BackColor = Color.FromName("#FFFFFF");
                }
            }
        }
        if (e.Row.DataItem != null)
        {
            try
            {
                Label rmind = ((Label)e.Row.FindControl("Label13"));
                if (rmind.Text == "01/01/1900")
                {
                    rmind.Text = "";
                }
            }
            catch (Exception ex)
            {

            }
        }

    }
    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Employee_load();
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

                    es_list.Text = "Select Department";
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
}
