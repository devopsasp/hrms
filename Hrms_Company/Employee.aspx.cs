using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ePayHrms.Candidate;
using ePayHrms.Company;
using System.Collections.ObjectModel;
using ePayHrms.BE.Recruitment;
using ePayHrms.Employee;

public partial class Hrms_Company_Default : System.Web.UI.Page
{
    Company company = new Company();
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    Candidate c = new Candidate();

    //Collection<Employee> AllowanceList;
    Collection<Company> CompanyList;
    Collection<Employee> EmployeeList;
   
    string s_login_role;
    int ddl_i, grk;
    string _path;
    string s_form = "";
    DataSet ds_userrights;

    protected void Page_Load(object sender, EventArgs e)
    {

        

        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        c.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        s_login_role = Request.Cookies["Login_temp_Role"].Value;

        if (s_login_role == "e")
        {
            employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
            pay.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
            c.EmployeeID = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);

            Session["emp_menu"] = 0;
            Response.Cookies["Select_Employee"].Value = "1";
            Response.Redirect("../Hrms_Employee/Employee_Preview.aspx");
        }

        if (!IsPostBack)
        {
         Response.Cookies["Select_Employee"].Value = "0";

            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {

                Session["ErrorMsg"] = "";

                switch (s_login_role)
                {
                    case "a":
                        admin();
                        ddl_Department_load();
                       // hr();
                       
                        
                        break;

                    case "h":
                        hr();
                        ddl_Department_load();
                        break;

                    case "e":
                        Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                        Response.Redirect("Company_Home.aspx");
                        break;

                    //case "u":
                    //    hr();
                    //    break;

                    case "u":
                        s_form = "23";

                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            hr();
                        }
                        else
                        {
                            Response.Redirect("../Hrms_Employee/Employee_Preview.aspx");
                        }
                        break;
                    default:
                        Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
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


    public void admin()
    {
        try
        {
            row_emp.Visible = false;
            //row_showdet_btn.Visible = false;

            Collection<Company> ddlBranchsList;


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


            //ddlBranchsList = company.fn_getBranchs();

            //ddl_Branch.DataSource = ddlBranchsList;
            //ddl_Branch.DataTextField = "CompanyName";
            //ddl_Branch.DataValueField = "CompanyId";
            //ddl_Branch.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured.');", true);
        }
    }


    public void hr()
    {
        try
        {
            row_branch.Visible = false;
            ddl_Employee.Visible = false;
            //row_showdet_btn.Visible = false;

            Collection<Employee> EmployeeList = employee.fn_getEmployeeList(employee);

            if (EmployeeList.Count > 0)
            {
                ddl_Employee.Visible = true;
                //row_showdet_btn.Visible = true;

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
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Employees Available');", true);
                ddl_Employee.Visible = false;
                //row_showdet_btn.Visible = true;
                row_emp.Visible = false;
            }


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured.');", true);
        }
    }

    protected void btn_Back_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("Company_Home.aspx");
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
            row_emp.Visible = true;
            ddl_Employee.Visible = false;

            ddl_Employee.Items.Clear();

            Session["preview_BranchID"] = Convert.ToInt32(ddl_Branch.SelectedItem.Value);

            employee.BranchId = Convert.ToInt32(ddl_Branch.SelectedItem.Value);

            Collection<Employee> EmployeeList = employee.fn_getEmployeeList(employee);

            if (EmployeeList.Count > 0)
            {

                ddl_Employee.Visible = true;
                //row_showdet_btn.Visible = true;

                for (ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
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
            else
            {
                ddl_Employee.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('No Employees Available.');};", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured.');", true);
        }
    }

    protected void btn_show_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddl_Employee.SelectedItem.Value != "0")
            {
                employee.EmployeeId = Convert.ToInt32(ddl_Employee.SelectedItem.Value);
                pay.EmployeeId = Convert.ToInt32(ddl_Employee.SelectedItem.Value);
                c.EmployeeID = Convert.ToInt32(ddl_Employee.SelectedItem.Value);
                Response.Cookies["preview_EmployeeID"].Value = ddl_Employee.SelectedItem.Value;
                Response.Cookies["Employee_Code_FirstLastName"].Value= ddl_Employee.SelectedItem.Text;
                Response.Cookies["preview_emp"].Value = "2";
                Response.Cookies["Profile_Check"].Value = "1";
                Response.Cookies["Select_Employee"].Value = "1";
                Response.Redirect("../Hrms_Employee/Employee_Preview.aspx");
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Select the employee from the list.!');", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured.');", true);
        }
    }

    protected void btn_add_employee_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Cookies["Profile_Check"].Value = "0";
            Response.Cookies["preview_emp"].Value = "1";
            //Session["first_profile"] = 0;
            Response.Cookies["Select_Employee"].Value = "1";

            Response.Cookies["Employee_Code_FirstLastName"].Value= "";

            Response.Redirect("../Hrms_Employee/Employee_Profile.aspx");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured.');", true);
        }
    }
    protected void ddl_Employee_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Img_prev_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Hrms_Employee/PreviousEmploy.aspx");
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

            for (int ddl_i = -2; ddl_i < EmployeeList.Count; ddl_i++)
            {

                if (ddl_i == -2)
                {
                    ListItem es_list = new ListItem();

                    es_list.Text = "Select Department";
                    es_list.Value = "0";
                    ddl_department.Items.Add(es_list);
                }
                else if (ddl_i == -1)
                {
                    ListItem es_list = new ListItem();

                    es_list.Text = "All";
                    es_list.Value = "All";
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
        try
        {
            ddl_Employee.Items.Clear();
            if (ddl_department.SelectedItem.Text == "All")
            {
                EmployeeList = employee.fn_getEmployeeList(employee);
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
            else
            {
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
        catch(Exception ex)
        {

        }
    }
}













