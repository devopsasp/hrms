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
using ePayHrms.Connection;
using ePayHrms.Candidate;
using System.IO;
using ePayHrms.Company;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ePayHrms.BE.Recruitment;
using ePayHrms.Employee;

public partial class Hrms_Employee_PreviousEmploy : System.Web.UI.Page
{
    Company company = new Company();
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    Candidate c = new Candidate();

    //Collection<Employee> AllowanceList;
    Collection<Company> CompanyList;

    string s_login_role;
    int ddl_i, grk;
    string _path;
    string s_form = "";
    DataSet ds_userrights;

    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_Error.Text = "";
        

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
        }

        if (!IsPostBack)
        {
            Response.Cookies["Select_Employee"].Value = "0";

            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {
                lbl_Error.Text = (string)Session["ErrorMsg"];
                Session["ErrorMsg"] = "";

                switch (s_login_role)
                {
                    case "a":
                        admin();
                        break;

                    case "h":
                        hr();
                        break;

                    case "e": Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                        Response.Redirect("Company_Home.aspx");
                        break;

                    //case "u":
                    //    hr();
                    //    break;

                    case "u": s_form = "30";

                        ds_userrights = company.check_Userrights(Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value), s_form);

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
            lbl_Error.Text = "Error";
        }
    }


    public void hr()
    {
        try
        {
            row_branch.Visible = false;
            ddl_Employee.Visible = false;
            //row_showdet_btn.Visible = false;

            Collection<Employee> EmployeeList = employee.fn_getoldEmployeeList(employee);

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
                lbl_Error.Text = "No Employees Available";
                ddl_Employee.Visible = false;
                //row_showdet_btn.Visible = true;
                row_emp.Visible = false;
            }


        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
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
            lbl_Error.Text = "Error";
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
                lbl_Error.Text = "No Employees Available";
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    protected void btn_show_Click(object sender, EventArgs e)
    {
        try
        {
            employee.EmployeeId = Convert.ToInt32(ddl_Employee.SelectedItem.Value);
            pay.EmployeeId = Convert.ToInt32(ddl_Employee.SelectedItem.Value);
            c.EmployeeID = Convert.ToInt32(ddl_Employee.SelectedItem.Value);

            Response.Cookies["preview_EmployeeID"].Value = ddl_Employee.SelectedItem.Value;
            Response.Cookies["Employee_Code_FirstLastName"].Value= ddl_Employee.SelectedItem.Text;
            Session["preview_emp"] = 2;
            Response.Cookies["Profile_Check"].Value = "1";
            Response.Cookies["Select_Employee"].Value = "1";
            Response.Redirect("../Hrms_Employee/Employee_Preview.aspx");

        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Hrms_Company/Employee.aspx");
    }
}
