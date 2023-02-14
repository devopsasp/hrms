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
using System.Reflection;

public partial class Hrms_Master_Default : System.Web.UI.Page
{
    ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataTable dt = new DataTable();
    DataTable dtable = new DataTable();

    Company company = new Company();
    Employee employee = new Employee();
    Leave l = new Leave();

    Collection<Leave> LeaveMasterList;
    Collection<Leave> LeaveList;
    Collection<Leave> grid_LeaveList;
    Collection<Company> CompanyList;
    Collection<Employee> EmployeeList;
    Collection<Employee> EmployeeList1;
    Collection<Employee> DepartmentList;
    Collection<Employee> DivisionList;
    Collection<Employee> CategoryList;
    Collection<Company> ddlBranchsList;

    int company_Id, branch_Id, valid, temp_valid = 0, check, ddl_i,i;
    string _Value;
    string s_login_role;
    bool grd_chk = true;
    string s_form = "";
    DataSet ds_userrights;
    

    protected void Page_Load(object sender, EventArgs e)
    {

        //string s = "'dafa'";
        //Response.Write(s);

        

        l.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        lbl_allo_type.Text = "Select";
        DropDownList1.Enabled = true;
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        if (!IsPostBack)
        {
            ////btn_back.Visible = false;
            //btn_save.Visible = false;
            //btn_modify.Visible = false;

            switch (s_login_role)
            {
                case "a": 
                    load();
                    ddl_employee_load();
                    break;

                case "h": load1();
                    master_load();
                    break;

                case "u": s_form = "30";
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
        ddl_Branch_load();
    }

    public void load1()
    {
        ddl_Branch.Visible = false;
    }

    public void load2()
    {
        ddl_Branch.Visible = false;
        ddl_department_load1();
    }

    public void load3()
    {
        ddl_Branch.Visible = true;
        ddl_division_load1();
    }

    public void load4()
    {
        ddl_Branch.Visible = false;
        ddl_category_load1();
    }
   
    public void clear()
    {
        //txt_leavename.Value = "";
        //txt_LeaveCode.Value = "";
        //txt_count.Value = "";
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

    public void ddl_employee_load1()
    {
        //employee dropdown
        l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        ddl_Employee.Items.Clear();
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        if (s_login_role == "a")
        {
            employee.BranchId = Convert.ToInt32(ddl_Branch.SelectedItem.Value);
        }
        EmployeeList = employee.fn_getEmployeeList(employee);

        if (EmployeeList.Count > 0)
        {
            for (ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();
                    e_list.Text = "Select";
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
            grid_Leave.Visible = false;
            ////btn_back.Visible = false;
            //btn_save.Visible = false;
            //btn_modify.Visible = false;
            ClientScriptManager manager = Page.ClientScript;
            manager.RegisterStartupScript(this.GetType(), "Call", "show_Error();", true);
        }
    }


    public void ddl_department_load1()
    {
        //employee dropdown
        ddl_Employee.Items.Clear();
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        if (s_login_role == "a")
        {
            DepartmentList = employee.fn_getDepartmentList1(Convert.ToInt32(ddl_Branch.SelectedItem.Value));
        }
        else if (s_login_role == "h")
        {
            DepartmentList = employee.fn_getDepartmentList1(employee.BranchId);
        }

        if (DepartmentList.Count > 0)
        {
            for (ddl_i = -1; ddl_i < DepartmentList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();
                    e_list.Text = "Select";
                    e_list.Value = "0";
                    ddl_Employee.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();
                    e_list.Value = DepartmentList[ddl_i].DepartmentId.ToString();
                    e_list.Text = DepartmentList[ddl_i].DepartmentName.ToString();
                    ddl_Employee.Items.Add(e_list);
                }
            }
        }
        else
        {
            grid_Leave.Visible = false;
            ////btn_back.Visible = false;
            //btn_save.Visible = false;
            //btn_modify.Visible = false;
            ClientScriptManager manager = Page.ClientScript;
            manager.RegisterStartupScript(this.GetType(), "Call", "show_Error();", true);
        }
    }

    public void ddl_division_load1()
    {
        //employee dropdown
        ddl_Employee.Items.Clear();
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        DivisionList = employee.fn_getDivisionList1(employee.BranchId);

        if (DivisionList.Count > 0)
        {
            for (ddl_i = -1; ddl_i < DivisionList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();
                    e_list.Text = "Select";
                    e_list.Value = "0";
                    ddl_Employee.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();
                    e_list.Value = DivisionList[ddl_i].DivisionId.ToString();
                    e_list.Text = DivisionList[ddl_i].DivisionName.ToString();
                    ddl_Employee.Items.Add(e_list);
                }
            }
        }
        else
        {
            grid_Leave.Visible = false;
            ////btn_back.Visible = false;
            //btn_save.Visible = false;
            //btn_modify.Visible = false;
            btn_reset.Visible = false;
            ClientScriptManager manager = Page.ClientScript;
            manager.RegisterStartupScript(this.GetType(), "Call", "show_Error();", true);
        }
    }


    public void ddl_category_load1()
    {
        //employee dropdown
        ddl_Employee.Items.Clear();
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        CategoryList = employee.fn_getCategoryList1(employee.BranchId);

        if (CategoryList.Count > 0)
        {
            for (ddl_i = -1; ddl_i < CategoryList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();
                    e_list.Text = "Select";
                    e_list.Value = "0";
                    ddl_Employee.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();
                    e_list.Value = CategoryList[ddl_i].CategoryId.ToString();
                    e_list.Text = CategoryList[ddl_i].CategoryName.ToString();
                    ddl_Employee.Items.Add(e_list);
                }
            }
        }
        else
        {
            grid_Leave.Visible = false;
           // //btn_back.Visible = false;
            //btn_save.Visible = false;
            //btn_modify.Visible = false;
            btn_reset.Visible = false;
            ClientScriptManager manager = Page.ClientScript;
            manager.RegisterStartupScript(this.GetType(), "Call", "show_Error();", true);
        }
    }

    public void ddl_employee_load()
    {
//employee dropdown
        ddl_Employee.Items.Clear();
        EmployeeList = employee.fn_getEmployeeList(employee);

        if (EmployeeList.Count > 0)
        {
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
            grid_Leave.Visible = false;
            ////btn_back.Visible = false;
            //btn_save.Visible = false;
            //btn_modify.Visible = false;
            btn_reset.Visible = false;
            ClientScriptManager manager = Page.ClientScript;
            manager.RegisterStartupScript(this.GetType(), "Call", "show_Error();", true);
        }
    }

    protected void Update(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            ((TextBox)grid_Leave.Rows[e.RowIndex].FindControl("txtcode")).Enabled = true;
            ((HtmlInputText)grid_Leave.Rows[e.RowIndex].FindControl("txtcount")).Disabled = false;      
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
        }
    }

    protected void Edit(object sender, GridViewEditEventArgs e)
    {
        try
        {
            l.leaveID = Convert.ToInt32(grid_Leave.DataKeys[e.NewEditIndex].Value);
            l.leaveCode = ((TextBox)grid_Leave.Rows[e.NewEditIndex].FindControl("txtcode")).Text;
            l.Count = Convert.ToInt32(((HtmlInputText)grid_Leave.Rows[e.NewEditIndex].FindControl("txtcount")).Value);
            l.Leave_First(l);

            ((TextBox)grid_Leave.Rows[e.NewEditIndex].FindControl("txtcode")).Enabled = false;
            ((HtmlInputText)grid_Leave.Rows[e.NewEditIndex].FindControl("txtcount")).Disabled = true;      
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
        }
    }

    protected void ddl_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddl_Branch.SelectedItem.Value != "0")
            {
                l.BranchID = Convert.ToInt32(ddl_Branch.SelectedItem.Value);
                employee.BranchId = Convert.ToInt32(ddl_Branch.SelectedItem.Value);
                ViewState["Leave_BranchID"] = Convert.ToInt32(ddl_Branch.SelectedItem.Value);
                //ddl_employee_load();
                master_load();
            }
            else
            {
                grid_Leave.Visible = false;
                ////btn_back.Visible = false;
                //btn_save.Visible = false;
                //btn_modify.Visible = false;
                btn_reset.Visible = false;
                
            }
         }

        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
        }
    }
    //public void ddl_employee_load()
    //{
    //    //employee dropdown
    //    ddl_Employee.Items.Clear();



    //    if (s_login_role == "a")
    //    {
    //        employee.BranchId = (int)ViewState["Appraisal_BranchID"];
    //    }

    //    if (s_login_role == "h")
    //    {
    //        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
    //    }



    //    qry = "Select a.pn_EmployeeID, a.EmployeeCode, a.Employee_First_Name from paym_employee a, paym_employee_profile1 b where a.pn_CompanyID=b.pn_CompanyID and a.pn_BranchID=b.pn_BranchID and a.pn_EmployeeID=b.pn_EmployeeID and b.pn_DepartmentID=" + ddl_department.SelectedValue + " and b.pn_CompanyID=" + employee.CompanyId + " and a.status='y' and b.pn_BranchID=" + employee.BranchId;

    //    EmployeeList = employee.fn_getEmplist(qry);

    //    if (EmployeeList.Count > 0)
    //    {
    //        row_emp.Visible = true;
    //        tbl_details.Visible = true;
    //        for (ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
    //        {

    //            if (ddl_i == -1)
    //            {
    //                ListItem e_list = new ListItem();
    //                e_list.Text = "Select Employee";
    //                e_list.Value = "0";
    //                ddl_Employee.Items.Add(e_list);
    //            }
    //            else
    //            {
    //                ListItem e_list = new ListItem();
    //                e_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
    //                e_list.Text = EmployeeList[ddl_i].LastName.ToString();
    //                ddl_Employee.Items.Add(e_list);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        lbl_Error.Text = "No Employee";
    //        tbl_details.Visible = true;
    //        tbl_grd.Visible = false;
    //        row_emp.Visible = false;
    //    }
    //}

    protected void ddl_Employee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddl_Employee.SelectedItem.Value != "0")
            {
                if (DropDownList1.SelectedItem.Text == "Individuals")
                {
                    btn_save.Enabled = true;
                    if (s_login_role == "h")
                    {
                        string gender = "";
                        l.EmployeeID = Convert.ToInt32(ddl_Employee.SelectedItem.Value);
                        ViewState["Leave_EmpID"] = Convert.ToInt32(ddl_Employee.SelectedItem.Value);
                        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                        l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                        gender = l.fn_getGender(l);
                        string str;

                        str = DropDownList1.SelectedItem.Text;

                        l.LeaveBY = str;

                        LeaveMasterList = l.fn_paym_leave1(employee.BranchId);

                        if (LeaveMasterList.Count > 0)
                        {

                            grid_Leave.DataSource = LeaveMasterList;
                            grid_Leave.DataBind();
                        }

                        LeaveList = l.fn_emp_leaveAllocation(l);
                        grid_Leave.Visible = true;
                        if (LeaveList.Count > 0)
                        {

                            for (i = 0; i < LeaveList.Count; i++)
                            {

                                if (Convert.ToInt32(grid_Leave.DataKeys[i].Value) == LeaveList[i].leaveID)
                                {
                                    ((HtmlInputText)grid_Leave.Rows[i].FindControl("txtcount")).Value = LeaveList[i].Count1.ToString();
                                    if (LeaveMasterList[i].LeaveBY == gender || LeaveMasterList[i].LeaveBY == "Both")
                                    {
                                        ((HtmlInputText)grid_Leave.Rows[i].FindControl("txtcount")).Disabled = false;
                                    }
                                    else
                                    {
                                        ((HtmlInputText)grid_Leave.Rows[i].FindControl("txtcount")).Disabled = true;
                                    }
                                }
                            }


                            //btn_save.Visible = false;
                            //btn_modify.Visible = true;
                            
                            ////btn_back.Visible = true;
                        }
                        else
                        {
                            for (i = 0; i < LeaveMasterList.Count; i++)
                            {

                                if (Convert.ToInt32(grid_Leave.DataKeys[i].Value) == LeaveMasterList[i].leaveID)
                                {
                                    ((HtmlInputText)grid_Leave.Rows[i].FindControl("txtcount")).Value = "0";
                                    if (LeaveMasterList[i].LeaveBY == gender || LeaveMasterList[i].LeaveBY == "Both")
                                    {
                                        ((HtmlInputText)grid_Leave.Rows[i].FindControl("txtcount")).Disabled = false;
                                    }
                                    else
                                    {
                                        ((HtmlInputText)grid_Leave.Rows[i].FindControl("txtcount")).Disabled = true;
                                    }
                                }

                            }

                            //for (i = 0; i < grid_Leave.Rows.Count; i++)
                            //{
                            //    ((HtmlInputText)grid_Leave.Rows[i].FindControl("txtcount")).Value = "0";
                            //}
                            //btn_save.Visible = true;
                            //btn_modify.Visible = false;
                            grid_Leave.Visible = true;
                           // //btn_back.Visible = true;
                        }

                    }

                }

                else if (DropDownList1.SelectedItem.Text == "Department")
                {

                    //display alloted leaves
                    string str = ddl_Employee.SelectedItem.Text;
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select n_count from paym_leaveallocation1 where leaveby='" + str + "'", con);
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();


                    //****


                    if (s_login_role == "a")
                    {
                        l.EmployeeID = Convert.ToInt32(ddl_Employee.SelectedItem.Value);
                        ViewState["Leave_DeptID"] = Convert.ToInt32(ddl_Employee.SelectedItem.Value);

                        //column(leave code)

                        LeaveList = l.fn_paym_leave(l);
                        if (LeaveList.Count > 0)
                        {
                            grid_Leave.DataSource = LeaveList;
                            grid_Leave.DataBind();
                        }

                        //column(Total Days)
                        LeaveList = l.fn_emp_leaveAllocation(l);//n_count
                        if (LeaveList.Count > 0)
                        {
                            for (i = 0; i < LeaveList.Count; i++)
                            {
                                if (Convert.ToInt32(grid_Leave.DataKeys[i].Value) == LeaveList[i].leaveID)
                                {
                                    ((HtmlInputText)grid_Leave.Rows[i].FindControl("txtcount")).Value = LeaveList[i].Count1.ToString();
                                }
                            }

                            //btn_save.Visible = false;
                            //btn_modify.Visible = true;
                            grid_Leave.Visible = true;
                           // //btn_back.Visible = true;
                        }
                        else
                        {
                            //btn_save.Visible = true;
                            //btn_modify.Visible = false;
                            grid_Leave.Visible = true;
                           // //btn_back.Visible = true;
                        }

                    }

                    if (s_login_role == "h")
                    {

                        l.EmployeeID = Convert.ToInt32(ddl_Employee.SelectedItem.Value);
                        ViewState["Leave_DeptID"] = Convert.ToInt32(ddl_Employee.SelectedItem.Value);
                        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                        l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

                        string str1 = ddl_Employee.SelectedItem.Text;
                        l.LeaveBY = str1;

                        LeaveList = l.fn_paym_leave1(employee.BranchId);
                        if (LeaveList.Count > 0)
                        {
                            grid_Leave.DataSource = LeaveList;
                            grid_Leave.DataBind();
                        }

                        //column(Total Days)
                        LeaveList = l.fn_emp_leaveAllocation(l);
                        if (LeaveList.Count > 0)
                        {
                            for (i = 0; i < LeaveList.Count; i++)
                            {
                                if (Convert.ToInt32(grid_Leave.DataKeys[i].Value) == LeaveList[i].leaveID)
                                {
                                    ((HtmlInputText)grid_Leave.Rows[i].FindControl("txtcount")).Value = LeaveList[i].Count1.ToString();
                                }
                            }
                            //btn_save.Visible = false;
                            //btn_modify.Visible = true;
                            grid_Leave.Visible = true;
                            ////btn_back.Visible = true;
                        }
                        else
                        {
                            //btn_save.Visible = true;
                            //btn_modify.Visible = false;
                            grid_Leave.Visible = true;
                            ////btn_back.Visible = true;
                        }
                    }
                }

                else if (DropDownList1.SelectedItem.Text == "Division")
                {
                    if (s_login_role == "a")
                    {
                        l.EmployeeID = Convert.ToInt32(ddl_Employee.SelectedItem.Value);
                        ViewState["Leave_DivID"] = Convert.ToInt32(ddl_Employee.SelectedItem.Value);

                        //column(leave code)

                        LeaveList = l.fn_paym_leave(l);
                        if (LeaveList.Count > 0)
                        {
                            grid_Leave.DataSource = LeaveList;
                            grid_Leave.DataBind();
                        }
                        //column(Total Days)
                        LeaveList = l.fn_emp_leaveAllocation(l);
                        if (LeaveList.Count > 0)
                        {
                            for (i = 0; i < LeaveList.Count; i++)
                            {
                                if (Convert.ToInt32(grid_Leave.DataKeys[i].Value) == LeaveList[i].leaveID)
                                {
                                    ((HtmlInputText)grid_Leave.Rows[i].FindControl("txtcount")).Value = LeaveList[i].Count1.ToString();
                                }
                            }
                            //btn_save.Visible = false;
                            //btn_modify.Visible = true;
                            grid_Leave.Visible = true;
                            ////btn_back.Visible = true;
                        }
                        else
                        {
                            //btn_save.Visible = true;
                            //btn_modify.Visible = false;
                            grid_Leave.Visible = true;
                            ////btn_back.Visible = true;
                        }
                    }

                    if (s_login_role == "h")
                    {
                        l.EmployeeID = Convert.ToInt32(ddl_Employee.SelectedItem.Value);
                        ViewState["Leave_DivID"] = Convert.ToInt32(ddl_Employee.SelectedItem.Value);
                        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

                        l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

                        string str1 = ddl_Employee.SelectedItem.Text;
                        l.LeaveBY = str1;

                        //column(leave code)
                        LeaveList = l.fn_paym_leave1(employee.BranchId);
                        if (LeaveList.Count > 0)
                        {
                            grid_Leave.DataSource = LeaveList;
                            grid_Leave.DataBind();
                        }

                        //column(Total Days)
                        LeaveList = l.fn_emp_leaveAllocation(l);

                        if (LeaveList.Count > 0)
                        {
                            for (i = 0; i < LeaveList.Count; i++)
                            {
                                if (Convert.ToInt32(grid_Leave.DataKeys[i].Value) == LeaveList[i].leaveID)
                                {
                                    ((HtmlInputText)grid_Leave.Rows[i].FindControl("txtcount")).Value = LeaveList[i].Count1.ToString();
                                }
                            }

                            //btn_save.Visible = false;
                            //btn_modify.Visible = true;
                            grid_Leave.Visible = true;
                            ////btn_back.Visible = true;
                        }
                        else
                        {
                            //btn_save.Visible = true;
                            //btn_modify.Visible = false;
                            grid_Leave.Visible = true;
                            ////btn_back.Visible = true;
                        }

                    }

                }

                else if (DropDownList1.SelectedItem.Text == "Category")
                {
                    if (s_login_role == "a")
                    {
                        l.EmployeeID = Convert.ToInt32(ddl_Employee.SelectedItem.Value);
                        ViewState["Leave_CatID"] = Convert.ToInt32(ddl_Employee.SelectedItem.Value);

                        //column(leave code)

                        LeaveList = l.fn_paym_leave(l);
                        if (LeaveList.Count > 0)
                        {
                            grid_Leave.DataSource = LeaveList;
                            grid_Leave.DataBind();
                        }

                        //column(Total Days)
                        LeaveList = l.fn_emp_leaveAllocation(l);
                        if (LeaveList.Count > 0)
                        {
                            for (i = 0; i < LeaveList.Count; i++)
                            {
                                if (Convert.ToInt32(grid_Leave.DataKeys[i].Value) == LeaveList[i].leaveID)
                                {
                                    ((HtmlInputText)grid_Leave.Rows[i].FindControl("txtcount")).Value = LeaveList[i].Count1.ToString();
                                }
                            }

                            //btn_save.Visible = false;
                            //btn_modify.Visible = true;
                            grid_Leave.Visible = true;
                            //btn_back.Visible = true;
                        }
                        else
                        {
                            //btn_save.Visible = true;
                            //btn_modify.Visible = false;
                            grid_Leave.Visible = true;
                            //btn_back.Visible = true;
                        }

                    }

                    if (s_login_role == "h")
                    {
                        l.EmployeeID = Convert.ToInt32(ddl_Employee.SelectedItem.Value);
                        ViewState["Leave_CatID"] = Convert.ToInt32(ddl_Employee.SelectedItem.Value);
                        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                        l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

                        string str1 = ddl_Employee.SelectedItem.Text;
                        l.LeaveBY = str1;


                        //column(leave code)

                        LeaveList = l.fn_paym_leave1(employee.BranchId);
                        if (LeaveList.Count > 0)
                        {
                            grid_Leave.DataSource = LeaveList;
                            grid_Leave.DataBind();
                        }

                        //column(Total Days)
                        LeaveList = l.fn_emp_leaveAllocation(l);
                        if (LeaveList.Count > 0)
                        {
                            for (i = 0; i < LeaveList.Count; i++)
                            {
                                if (Convert.ToInt32(grid_Leave.DataKeys[i].Value) == LeaveList[i].leaveID)
                                {
                                    ((HtmlInputText)grid_Leave.Rows[i].FindControl("txtcount")).Value = LeaveList[i].Count1.ToString();
                                }
                            }

                            //btn_save.Visible = false;
                            //btn_modify.Visible = true;
                            grid_Leave.Visible = true;
                            //btn_back.Visible = true;
                        }
                        else
                        {
                            //btn_save.Visible = true;
                            //btn_modify.Visible = false;
                            grid_Leave.Visible = true;
                            //btn_back.Visible = true;
                        }

                    }

                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Employee Selected!');", true);
                grid_Leave.Visible = false;
                //btn_save.Visible = false;
                //btn_modify.Visible = false;
                //btn_back.Visible = false;
            }
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
        }
    }



    public void master_load()
    {
        btn_save.Enabled = false;
        con.Open();
        SqlCommand comm = new SqlCommand("Select * from leaveallocation_master where pn_companyID = '" + employee.CompanyId + "' and pn_branchID = '" + employee.BranchId + "'", con);
        SqlDataReader reader = comm.ExecuteReader();
        if (reader.Read())
        {
            DropDownList1.SelectedItem.Text = reader["Category"].ToString();
            //DropDownList1.Enabled = false;
            Category_change();
        }
        con.Close();
    }


    public void Get_year()
    {
        try
        {
            string sdate = "", edate = "";
            con.Open();
            cmd = new SqlCommand("Select * from paym_branch where pn_companyid = '" + employee.CompanyId + "' and pn_branchId = '" + employee.BranchId + "'", con);
            SqlDataReader rea = cmd.ExecuteReader();
            if (rea.Read())
            {
                sdate = Convert.ToDateTime(rea["start_date"]).ToString("dd/MM/yyyy");
                edate = Convert.ToDateTime(rea["end_date"]).ToString("dd/MM/yyyy");
            }
            string[] sd = sdate.Split('/');
            l.year = Convert.ToInt32(sd[2]);
            string[] ed = edate.Split('/');
            l.To_year = Convert.ToInt32(ed[2]);
        }
        catch (Exception ex)
        {
            
        }
        finally
        {
            con.Close();
        }
    }

    public void master_allocation()
    {
        if (DropDownList1.SelectedItem.Text == "Individuals")
        {
            l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            string year = "0";
            //l.EmployeeID = (int)ViewState["Leave_EmpID"];
            l.EmployeeID = Convert.ToInt32(ddl_Employee.SelectedValue);
            employee.EmployeeId = Convert.ToInt32(ddl_Employee.SelectedValue);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from paym_branch where pn_companyID = '" + l.CompanyID + "' and pn_BranchID = '" + l.BranchID + "'", con);
            SqlDataReader rder = cmd.ExecuteReader();
            if (rder.Read())
            {
                year = Convert.ToDateTime(rder["End_Date"]).ToString("dd/MM/yyyy");
                string[] yspl = year.Split('/');
                year = yspl[2];
            }
            l.year = Convert.ToInt32(year);
            con.Close();
            for (int emp_edu = 0; emp_edu < grid_Leave.Rows.Count; emp_edu++)
            {
                l.Flag = DropDownList1.SelectedItem.Text;
                l.leaveID = Convert.ToInt32(grid_Leave.DataKeys[emp_edu].Value);
                l.Count1 = Convert.ToDouble(((HtmlInputText)grid_Leave.Rows[emp_edu].FindControl("txtcount")).Value);
                _Value = l.Leave_Allocation(l);
            }
            if (_Value == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Saved Successfully!');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
            }
            
        }

        else if (DropDownList1.SelectedItem.Text == "All")
        {
            l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            string year = "0";
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from paym_branch where pn_companyID = '" + l.CompanyID + "' and pn_BranchID = '" + l.BranchID + "'", con);
            SqlDataReader rder = cmd.ExecuteReader();
            if (rder.Read())
            {
                year = Convert.ToDateTime(rder["End_Date"]).ToString("dd/MM/yyyy");
                string[] yspl = year.Split('/');
                year = yspl[2];
            }
            l.year = Convert.ToInt32(year);
            con.Close();
            EmployeeList = employee.fn_getEmployeeList(employee);

            if (EmployeeList.Count > 0)
            {
                for (ddl_i = 0; ddl_i < EmployeeList.Count; ddl_i++)
                {
                    employee.EmployeeId = EmployeeList[ddl_i].EmployeeId;
                    l.EmployeeID = EmployeeList[ddl_i].EmployeeId;
                    for (int emp_edu = 0; emp_edu < grid_Leave.Rows.Count; emp_edu++)
                    {
                        l.Flag = DropDownList1.SelectedItem.Text;
                        l.leaveID = Convert.ToInt32(grid_Leave.DataKeys[emp_edu].Value);
                        l.Count1 = Convert.ToInt32(((HtmlInputText)grid_Leave.Rows[emp_edu].FindControl("txtcount")).Value);
                        l.Point = l.fn_paym_leaveCode(l);
                        if (l.Point != "Both")
                        {
                            EmployeeList1 = employee.fn_getEmployeeList1(employee);
                            if (EmployeeList1[0].Gender != l.Point)
                            {
                                l.Count1 = 0;
                                l.Flag = "individual";
                            }
                        }
                        _Value = l.Leave_Allocation(l);
                    }
                }
                if (_Value == "0")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Saved Successfully!');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Employee Found!');", true);
            }

            
        }

        else if (DropDownList1.SelectedItem.Text == "Department")
        {
            l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

            l.Departmentid = (int)ViewState["Leave_DeptID"];

            SqlCommand cmd8 = new SqlCommand();
            SqlDataReader rdr1;
            for (int emp_edu = 0; emp_edu < grid_Leave.Rows.Count; emp_edu++)
            {
                l.leaveID = Convert.ToInt32(grid_Leave.DataKeys[emp_edu].Value);
                l.Count = Convert.ToInt32(((HtmlInputText)grid_Leave.Rows[emp_edu].FindControl("txtcount")).Value);
                int did = 0;
                con.Open();
                string year = "0";
                //l.EmployeeID = (int)ViewState["Leave_EmpID"];
                SqlCommand cmd = new SqlCommand("select * from paym_branch where pn_companyID = '" + l.CompanyID + "' and pn_BranchID = '" + l.BranchID + "'", con);
                SqlDataReader rder = cmd.ExecuteReader();
                if (rder.Read())
                {
                    year = Convert.ToDateTime(rder["End_Date"]).ToString("dd/MM/yyyy");
                    string[] yspl = year.Split('/');
                    year = yspl[2];
                }
                string str;
                str = ddl_Employee.SelectedItem.Text;
                //SqlCommand com = new SqlCommand("select a.pn_EmployeeID,a.pn_departmentid,b.v_DepartmentName from paym_employee_profile1 a,paym_department b where a.pn_departmentid=b.pn_departmentid and b.v_DepartmentName='" + str + "'", con);
                SqlCommand com = new SqlCommand("select a.pn_employeeid, a.pn_categoryid,b.v_departmentname from paym_employee_profile1 a,paym_department b where a.pn_departmentid=b.pn_departmentid and a.pn_branchID = b.pn_branchid and b.v_DepartmentName='" + str + "'", con);
                rdr1 = com.ExecuteReader();

                while (rdr1.Read())
                {
                    did = Convert.ToInt32(rdr1[0]);
                    //orginally it was paym_leaveAllocation ::mei
                    cmd8 = new SqlCommand("insert into paym_leaveAllocation1 (pn_companyid,pn_branchid,pn_leaveid,pn_employeeid,n_count,leaveby,yearend) values ('" + l.CompanyID + "','" + l.BranchID + "','" + l.leaveID + "','" + did + "','" + l.Count + "' , '" + str + "','" + year + "')", con);
                    cmd8.ExecuteNonQuery();

                }

                rdr1.Close();
                con.Close();
                //_Value = l.Leave_Allocation(l);
            }
        }

        else if (DropDownList1.SelectedItem.Text == "Division")
        {
            l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

            l.Divisionid = (int)ViewState["Leave_DivID"];

            for (int emp_edu = 0; emp_edu < grid_Leave.Rows.Count; emp_edu++)
            {
                l.leaveID = Convert.ToInt32(grid_Leave.DataKeys[emp_edu].Value);
                l.Count = Convert.ToInt32(((HtmlInputText)grid_Leave.Rows[emp_edu].FindControl("txtcount")).Value);

                //leave allocation table insertion mei
                con.Open();
                string str;
                string year = "0";
                //l.EmployeeID = (int)ViewState["Leave_EmpID"];
                SqlCommand cmd = new SqlCommand("select * from paym_branch where pn_companyID = '" + l.CompanyID + "' and pn_BranchID = '" + l.BranchID + "'", con);
                SqlDataReader rder = cmd.ExecuteReader();
                if (rder.Read())
                {
                    year = Convert.ToDateTime(rder["End_Date"]).ToString("dd/MM/yyyy");
                    string[] yspl = year.Split('/');
                    year = yspl[2];
                }
                str = ddl_Employee.SelectedItem.Text;


                int temp = 0;

                //SqlCommand com = new SqlCommand("select * from paym_division where v_divisionname='" + str + "' ", con);//to find pn_division id
                //SqlCommand com = new SqlCommand("select a.pn_DivisionID,a.pn_employeeid,b.v_DivisionName from  paym_employee_profile1 a,paym_division b where a.pn_divisionid=b.pn_divisionid and v_divisionname='"+str+"'", con);
                SqlCommand com = new SqlCommand("select a.pn_employeeid, a.pn_divisionid,b.v_divisionname from paym_employee_profile1 a,paym_division b where a.pn_divisionid=b.pn_divisionid and a.pn_branchID = b.branchid and b.v_divisionname='" + str + "'", con);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    temp = Convert.ToInt32(rdr[0]);
                    SqlCommand cmd10 = new SqlCommand("insert into paym_leaveAllocation1 (pn_companyid,pn_branchid,pn_leaveid,pn_employeeid,n_count,leaveby,yearend) values ('" + l.CompanyID + "','" + l.BranchID + "','" + l.leaveID + "','" + temp + "','" + l.Count + "','" + str + "','" + year + "')", con);
                    cmd10.ExecuteNonQuery();
                    //
                }
                //lblmei.Text = temp.ToString();//correct mei
                rdr.Close();
                con.Close();
                //_Value = l.Leave_Allocation(l);
            }


        }

        else if (DropDownList1.SelectedItem.Text == "Category")
        {
            l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            //l.BranchID = (int)ViewState["Leave_BranchID"];
            l.Categoryid = (int)ViewState["Leave_CatID"];



            for (int emp_edu = 0; emp_edu < grid_Leave.Rows.Count; emp_edu++)
            {
                l.leaveID = Convert.ToInt32(grid_Leave.DataKeys[emp_edu].Value);
                l.Count = Convert.ToInt32(((HtmlInputText)grid_Leave.Rows[emp_edu].FindControl("txtcount")).Value);
                string str;
                int tempid = 0;
                str = ddl_Employee.SelectedItem.Text;
                con.Open();
                string year = "0";
                //l.EmployeeID = (int)ViewState["Leave_EmpID"];
                SqlCommand cmd = new SqlCommand("select * from paym_branch where pn_companyID = '" + l.CompanyID + "' and pn_BranchID = '" + l.BranchID + "'", con);
                SqlDataReader rder = cmd.ExecuteReader();
                if (rder.Read())
                {
                    year = Convert.ToDateTime(rder["End_Date"]).ToString("dd/MM/yyyy");
                    string[] yspl = year.Split('/');
                    year = yspl[2];
                }
                //SqlCommand com = new SqlCommand("select a.pn_employeeid, a.pn_divisionid,b.v_divisionname from paym_employee_profile1 a,paym_division b where a.pn_divisionid=b.pn_divisionid and a.pn_branchID = b.branchid and b.v_divisionname='" + str + "'", con);
                SqlCommand com = new SqlCommand("select a.pn_employeeid, a.pn_categoryid,b.v_categoryname from paym_employee_profile1 a,paym_category b where a.pn_categoryid=b.pn_categoryid and a.pn_branchID = b.branchid and b.v_categoryname='" + str + "'", con);
                com.ExecuteNonQuery();
                SqlDataReader rdr = com.ExecuteReader();

                while (rdr.Read())
                {
                    tempid = Convert.ToInt32(rdr[0]);
                    SqlCommand cmd10 = new SqlCommand("insert into paym_leaveAllocation1 (pn_companyid,pn_branchid,pn_leaveid,pn_employeeid,n_count,leaveby,yearend) values ('" + l.CompanyID + "','" + l.BranchID + "','" + l.leaveID + "','" + tempid + "','" + l.Count + "','" + str + "','" + year + "')", con);
                    cmd10.ExecuteNonQuery();
                }
                rdr.Close();
                con.Close();


            }
        }
    }

    public void save()
    {
        if (s_login_role == "a")
        {
            l.BranchID = Convert.ToInt32(ddl_Branch.SelectedItem.Value);
        }
        else
        {
            l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        }
        string yend = "0";
        master_allocation();
        con.Open();
        SqlCommand com1 = new SqlCommand("select * from paym_branch where pn_companyID = '" + l.CompanyID + "' and pn_BranchID = '" + l.BranchID + "'", con);
        SqlDataReader rer = com1.ExecuteReader();
        if (rer.Read())
        {
            yend = Convert.ToDateTime(rer["End_Date"]).ToString("dd/MM/yyyy");
            string[] yspl1 = yend.Split('/');
            yend = yspl1[2];
        }
        con.Close();
        if (DropDownList1.SelectedItem.Text == "Individuals")
        {
            l.EmployeeID = (int)ViewState["Leave_EmpID"];
            SqlCommand cmd8 = new SqlCommand();
            SqlDataReader rdr1;
            for (int emp_edu = 0; emp_edu < grid_Leave.Rows.Count; emp_edu++)
            {
                l.leaveID = Convert.ToInt32(grid_Leave.DataKeys[emp_edu].Value);
                l.Count = Convert.ToInt32(((HtmlInputText)grid_Leave.Rows[emp_edu].FindControl("txtcount")).Value);
                int did = 0;
                con.Open();

                string str,str1,str2;
                
                str = ddl_Employee.SelectedItem.Text;

                string[] strcode = str.Split('-');

                str1 = strcode[0];

                SqlCommand com = new SqlCommand("select pn_Employeeid from paym_employee where employeecode='"+str1+"'", con);
                rdr1 = com.ExecuteReader();
                
                while (rdr1.Read())
                {
                    did = Convert.ToInt32(rdr1[0]);
                    //orginally it was paym_leaveAllocation ::mei
                    cmd8 = new SqlCommand("select * from paym_leaveAllocation1 where pn_companyID = '" + l.CompanyID + "' and pn_BranchID = '" + l.BranchID + "' and pn_LeaveID = '" + l.leaveID + "' and leaveby = '" + str + "' and yearend = '" + yend + "'", con);
                    SqlDataReader rde = cmd8.ExecuteReader();
                    if (rde.HasRows)
                    {
                        cmd8 = new SqlCommand("update paym_leaveAllocation1 set n_count = '" + l.Count + "' where pn_companyID = '" + l.CompanyID + "' and pn_BranchID = '" + l.BranchID + "' and pn_LeaveID = '" + l.leaveID + "' and leaveby = '" + str + "' and yearend = '" + yend + "'", con);
                        cmd8.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd8 = new SqlCommand("insert into paym_leaveAllocation1 (pn_companyid,pn_branchid,pn_leaveid,pn_employeeid,n_count,leaveby,yearend) values ('" + l.CompanyID + "','" + l.BranchID + "','" + l.leaveID + "','" + did + "','" + l.Count + "' , '" + str + "','" + yend + "')", con);
                        cmd8.ExecuteNonQuery();
                    }
                }

                rdr1.Close();
                con.Close();
                //_Value = l.Leave_Allocation(l);
            }
        }

        else if (DropDownList1.SelectedItem.Text == "Department")
        {

            l.Departmentid = (int)ViewState["Leave_DeptID"];
            
            SqlCommand cmd8 = new SqlCommand();
            SqlDataReader rdr1;
            for (int emp_edu = 0; emp_edu < grid_Leave.Rows.Count; emp_edu++)
            {
                l.leaveID = Convert.ToInt32(grid_Leave.DataKeys[emp_edu].Value);
                l.Count = Convert.ToInt32(((HtmlInputText)grid_Leave.Rows[emp_edu].FindControl("txtcount")).Value);
                int did = 0;
                con.Open();
                
                string str;
                str = ddl_Employee.SelectedItem.Text;
                //SqlCommand com = new SqlCommand("select a.pn_EmployeeID,a.pn_departmentid,b.v_DepartmentName from paym_employee_profile1 a,paym_department b where a.pn_departmentid=b.pn_departmentid and b.v_DepartmentName='" + str + "'", con);
                SqlCommand com = new SqlCommand("select a.pn_employeeid, a.pn_categoryid,b.v_departmentname from paym_employee_profile1 a,paym_department b where a.pn_departmentid=b.pn_departmentid and a.pn_branchID = b.pn_branchid and b.v_DepartmentName='" + str + "'", con);
                rdr1 = com.ExecuteReader();

                while (rdr1.Read())
                {
                    did = Convert.ToInt32(rdr1[0]);
                    //orginally it was paym_leaveAllocation ::mei
                    cmd8 = new SqlCommand("select * from paym_leaveAllocation1 where pn_companyID = '" + l.CompanyID + "' and pn_BranchID = '" + l.BranchID + "' and pn_LeaveID = '" + l.leaveID + "' and pn_EmployeeID = '" + did + "' and leaveby = '" + str + "' and yearend = '" + yend + "'", con);
                    SqlDataReader rde = cmd8.ExecuteReader();
                   
          
            
                    if (rde.HasRows)
                    {
                          cmd = new SqlCommand("select count(*) from paym_leave",con);
                          int count = Convert.ToInt32(cmd.ExecuteScalar());

                        for (int i = 2; i < count + 2; i++)
                        {
                                TextBox ddl= new TextBox();
                                  ddl.ID = "txt_leave"+i;
                            string txt_id= ddl.ID;
                        cmd8 = new SqlCommand("update paym_leaveAllocation1 set n_count = '" + l.Count + "' where pn_companyID = '" + l.CompanyID + "' and pn_BranchID = '" + l.BranchID + "' and pn_LeaveID = '" + l.leaveID + "' and leaveby = '" + str + "' and yearend = '" + yend + "','"+txt_id+"' ", con);
                        cmd8.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        cmd8 = new SqlCommand("insert into paym_leaveAllocation1 (pn_companyid,pn_branchid,pn_leaveid,pn_employeeid,n_count,leaveby,yearend) values ('" + l.CompanyID + "','" + l.BranchID + "','" + l.leaveID + "','" + did + "','" + l.Count + "' , '" + str + "','" + yend + "')", con);
                        cmd8.ExecuteNonQuery();
                    }
                }
                
                rdr1.Close();
                con.Close();
                //_Value = l.Leave_Allocation(l);
            }
        }

        else if (DropDownList1.SelectedItem.Text == "Division")
        {
            l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            
            l.Divisionid = (int)ViewState["Leave_DivID"];

            //select a.pn_DivisionID,a.pn_employeeid,b.v_DivisionName from  paym_employee_profile1 a,paym_division b where a.pn_divisionid=b.pn_divisionid//might b wrong
            //select b.pn_employeeid,a.pn_divisionid from paym_division a,paym_employee_profile1 b where a.pn_divisionId=39

            for (int emp_edu = 0; emp_edu < grid_Leave.Rows.Count; emp_edu++)
            {
                l.leaveID = Convert.ToInt32(grid_Leave.DataKeys[emp_edu].Value);
                l.Count = Convert.ToInt32(((HtmlInputText)grid_Leave.Rows[emp_edu].FindControl("txtcount")).Value);

                //leave allocation table insertion mei
                con.Open();
                string str;

                str = ddl_Employee.SelectedItem.Text;

                int temp = 0;

                //SqlCommand com = new SqlCommand("select * from paym_division where v_divisionname='" + str + "' ", con);//to find pn_division id
                //SqlCommand com = new SqlCommand("select a.pn_DivisionID,a.pn_employeeid,b.v_DivisionName from  paym_employee_profile1 a,paym_division b where a.pn_divisionid=b.pn_divisionid and v_divisionname='"+str+"'", con);
                SqlCommand com = new SqlCommand("select a.pn_employeeid, a.pn_divisionid,b.v_divisionname from paym_employee_profile1 a,paym_division b where a.pn_divisionid=b.pn_divisionid and a.pn_branchID = b.branchid and b.v_divisionname='" + str + "'", con);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    temp = Convert.ToInt32(rdr[0]);
                    SqlCommand cmd10 = new SqlCommand("insert into paym_leaveAllocation1 (pn_companyid,pn_branchid,pn_leaveid,pn_employeeid,n_count,leaveby,yearend) values ('" + l.CompanyID + "','" + l.BranchID + "','" + l.leaveID + "','" + temp + "','" + l.Count + "','" + str + "','" + yend + "')", con);
                    cmd10.ExecuteNonQuery();
                    //
                }
                //lblmei.Text = temp.ToString();//correct mei
                rdr.Close();
                con.Close();
                //_Value = l.Leave_Allocation(l);
            }
            
            
        }

        else if (DropDownList1.SelectedItem.Text == "Category")
        {
            l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            //l.BranchID = (int)ViewState["Leave_BranchID"];
            l.Categoryid = (int)ViewState["Leave_CatID"];
            for (int emp_edu = 0; emp_edu < grid_Leave.Rows.Count; emp_edu++)
            {
                l.leaveID = Convert.ToInt32(grid_Leave.DataKeys[emp_edu].Value);
                l.Count = Convert.ToInt32(((HtmlInputText)grid_Leave.Rows[emp_edu].FindControl("txtcount")).Value);
                string str;
                int tempid = 0;
                str = ddl_Employee.SelectedItem.Text;
                con.Open();
                //SqlCommand com = new SqlCommand("select a.pn_employeeid, a.pn_divisionid,b.v_divisionname from paym_employee_profile1 a,paym_division b where a.pn_divisionid=b.pn_divisionid and a.pn_branchID = b.branchid and b.v_divisionname='" + str + "'", con);
                SqlCommand com = new SqlCommand("select a.pn_employeeid, a.pn_categoryid,b.v_categoryname from paym_employee_profile1 a,paym_category b where a.pn_categoryid=b.pn_categoryid and a.pn_branchID = b.branchid and b.v_categoryname='" + str + "'", con);
                com.ExecuteNonQuery();
                SqlDataReader rdr = com.ExecuteReader();

                while (rdr.Read())
                {
                    tempid = Convert.ToInt32(rdr[0]);
                    SqlCommand cmd10 = new SqlCommand("insert into paym_leaveAllocation1 (pn_companyid,pn_branchid,pn_leaveid,pn_employeeid,n_count,leaveby,yearend) values ('" + l.CompanyID + "','" + l.BranchID + "','" + l.leaveID + "','" + tempid + "','" + l.Count + "','" + str + "','" + yend + "')", con);
                    cmd10.ExecuteNonQuery();
                }
                rdr.Close();
                con.Close();               
              
            }
        }
    }

    public bool grid_check()
    {
        try
        {
            grd_chk = true;
            for (i = 0; i < grid_Leave.Rows.Count; i++)
            {
                if (Convert.ToInt32(((HtmlInputText)grid_Leave.Rows[i].FindControl("txtcount")).Value) > Convert.ToInt32(((Label)grid_Leave.Rows[i].FindControl("txttotal")).Text))
                {
                    grd_chk = false;
                    break;
                }
            }
            return grd_chk;
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter no of days!');", true);
            return false;
        }
    }


    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Category_change();
    }

    public void Normal_state()
    {
        for (i = 0; i < grid_Leave.Rows.Count; i++)
        {
            ((HtmlInputText)grid_Leave.Rows[i].FindControl("txtcount")).Value = "";
        }
        DropDownList1.SelectedIndex = 0;
        ddl_department.Items.Clear();
        ddl_Employee.Items.Clear();
        btn_save.Enabled = false;
        grid_Leave.Visible = false;
    }

    public void Category_change()
    {
        if (DropDownList1.SelectedItem.Text == "Individuals")
        {
            ddl_department.Items.Clear();
            ddl_Employee.Items.Clear();
            grid_Leave.Visible = false;
            ddl_Department_load();
            ddl_Employee.Enabled = true;
            btn_save.Enabled = false;
            //EmployeeList = employee.fn_getEmployeeList(employee);

            //if (EmployeeList.Count > 0)
            //{
            //    for (ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            //    {
            //        if (ddl_i == -1)
            //        {
            //            ListItem e_list = new ListItem();
            //            e_list.Text = "Select";
            //            e_list.Value = "0";
            //            ddl_Employee.Items.Add(e_list);
            //        }
            //        else
            //        {
            //            ListItem e_list = new ListItem();
            //            e_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
            //            e_list.Text = EmployeeList[ddl_i].LastName.ToString();
            //            ddl_Employee.Items.Add(e_list);
            //        }
            //    }
            //}
            //else
            //{
            //    grid_Leave.Visible = false;
            //    ////btn_back.Visible = false;
            //    //btn_save.Visible = false;
            //    //btn_modify.Visible = false;
            //    ClientScriptManager manager = Page.ClientScript;
            //    manager.RegisterStartupScript(this.GetType(), "Call", "show_Error();", true);
            //}
        }

        else if (DropDownList1.SelectedItem.Text == "Department")
        {
            //lbl_allo_type.Text = "Select Department";
            ddl_Department_load();
            grid_Leave.Visible = false;
            btn_save.Enabled = false;
        }

        else if (DropDownList1.SelectedItem.Text == "Division")
        {
            lbl_allo_type.Text = "Select Division";
            load3();
        }

        else if (DropDownList1.SelectedItem.Text == "Category")
        {
            lbl_allo_type.Text = "Select Category";
            load4();
        }

        else if (DropDownList1.SelectedItem.Text == "All")
        {
            ddl_department.Items.Clear();
            ddl_Employee.Items.Clear();
            ddl_Department_load();
            lbl_allo_type.Text = "Select Employee";
            ListItem e_list = new ListItem();
            e_list.Text = "All";
            e_list.Value = "All";
            ddl_Employee.Items.Add(e_list);
            ddl_Employee.Enabled = false;
            btn_save.Enabled = true;

            if (s_login_role == "h")
            {
                //l.EmployeeID = Convert.ToInt32(ddl_Employee.SelectedItem.Value);
                //ViewState["Leave_EmpID"] = Convert.ToInt32(ddl_Employee.SelectedItem.Value);
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

                l.LeaveBY = DropDownList1.SelectedItem.Text;

                LeaveList = l.fn_paym_leave1(employee.BranchId);

                if (LeaveList.Count > 0)
                {

                    grid_Leave.DataSource = LeaveList;
                    grid_Leave.DataBind();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No leave Found!');", true);
                }

                LeaveList = l.fn_emp_leaveAllocation1(l);

                if (LeaveList.Count > 0)
                {
                    for (i = 0; i < LeaveList.Count; i++)
                    {
                        if (Convert.ToInt32(grid_Leave.DataKeys[i].Value) == LeaveList[i].leaveID)
                        {
                            ((HtmlInputText)grid_Leave.Rows[i].FindControl("txtcount")).Value = LeaveList[i].Count1.ToString();
                        }
                    }
                    //btn_save.Visible = false;
                    //btn_modify.Visible = true;
                    grid_Leave.Visible = true;
                    ////btn_back.Visible = true;
                }
                else
                {
                    //btn_save.Visible = true;
                    //btn_modify.Visible = false;
                    grid_Leave.Visible = false;
                   // //btn_back.Visible = true;
                }

            }
        }
    }

    protected void grid_Leave_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            con.Open();
            cmd = new SqlCommand("select count(*) from paym_leave",con);
            int count = Convert.ToInt32(cmd.ExecuteScalar());

            for (int i = 2; i < count + 2; i++)
            {
                TextBox ddl= new TextBox();
                ddl.ID = "txt_leave"+i;
                e.Row.Cells[i].Controls.Add(ddl);
                e.Row.Cells[i].Width = 150;            
            }
            
            con.Close();
        } 
    }
    protected void btn_reset_Click(object sender, EventArgs e)
    {
        //con.Open();
        //SqlCommand cm = new SqlCommand("Delete from leaveallocation_master where pn_CompanyID = '" + employee.CompanyId + "' and pn_branchID = '" + l.BranchID + "'", con);
        //cm.ExecuteNonQuery();
        //cm = new SqlCommand("Delete from paym_leaveallocation1 where pn_CompanyID = '" + employee.CompanyId + "' and pn_branchID = '" + l.BranchID + "'", con);
        //cm.ExecuteNonQuery();
        //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Allocations has been reseted!');", true);
        //con.Close();
        Normal_state();
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            master_allocation();
            if (_Value != "1")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Added Successfully!');", true);
                clear();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
        }
    }
    protected void btn_modify_Click(object sender, EventArgs e)
    {
        try
        {
            if (grid_check() == true)
            {
                save();
                if (_Value != "1")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Updated Successfully!');", true);
                    clear();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No of days exceeded!');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
        }
    }
    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedItem.Text == "Department")
        {
            
            ddl_Employee.Enabled = false;
            grid_Leave.Visible = true;

            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

            l.LeaveBY = DropDownList1.SelectedItem.Text;

            LeaveList = l.fn_paym_leave1(employee.BranchId);

            if (LeaveList.Count > 0)
            {

                grid_Leave.DataSource = LeaveList;
                grid_Leave.DataBind();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No leave Found!');", true);
            }

            LeaveList = l.fn_emp_leaveAllocation1(l);

            if (LeaveList.Count > 0)
            {
                for (i = 0; i < LeaveList.Count; i++)
                {
                    if (Convert.ToInt32(grid_Leave.DataKeys[i].Value) == LeaveList[i].leaveID)
                    {
                        ((HtmlInputText)grid_Leave.Rows[i].FindControl("txtcount")).Value = LeaveList[i].Count1.ToString();
                    }
                }

                grid_Leave.Visible = true;

            }
            else
            {

                grid_Leave.Visible = false;
            }

        
    }
        else
        {
            ddl_Employee_load();
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
}