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
using ePayHrms.Leave;

public partial class Hrms_Operations_Default : System.Web.UI.Page
{

    Company company = new Company();
    Employee employee = new Employee();
    Collection<Company> CompanyList;

    DataSet ds_check_Employee = new DataSet();
    DataSet ds_userrights;

    string user_id, password, s_form = "", login = "", pwd = "";
    string s_login_role;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        
        s_login_role = Request.Cookies["Login_temp_Role"].Value;

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
                        btn_Employee.Visible = false;
                        btn_Masters.Visible = false;
                        btn_branch.Visible = false;
                        btn.Visible = false;
                        break;

                    case "h":
                        
                        btn_Employee.Visible = false;
                        btn_Masters.Visible = false;
                        btn_branch.Visible = false;
                        btn.Visible = false;
                        break;

                    case "u": 
                        s_form = "7";
                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);
                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            btn_Employee.Visible = false;
                            btn_Masters.Visible = false;
                            btn_branch.Visible = false;
                            btn.Visible = false;
                        }
                        else
                        {
                            Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                            Response.Redirect("~/Company_Home.aspx");
                        }
                        break;

                    default:
                        Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                        Response.Redirect("~/Company_Home.aspx");
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

    protected void btn_yes_Click(object sender, EventArgs e)
    {
        try
        {
            user_id = txt_empcode.Text;
            password = txt_emppwd.Text;

            switch (s_login_role)
            {
                case "a":
                     login = Request.Cookies["Login_UserID"].Value;
                     pwd = (string)Session["Login_pwd"];
                    if (login == user_id && pwd == password)
                    {
                        btn_Employee.Visible = true;
                        btn_Masters.Visible = true;
                        btn_branch.Visible = true;
                        btn.Visible = true;
                        tab_emp.Visible = false;
                        Session["del_empcode"] = password;

                        lbl_Error.Text = "Choose Any Option";
                    }
                    else
                    {
                        lbl_Error.Text = "Employee Code and Password does not match";
                    }
                    break;

                case "h":
                    login = Request.Cookies["Login_UserID"].Value;
                    pwd = (string)Session["Login_pwd"];
                    if (login == user_id && pwd == password)
                    {
                        btn_Employee.Visible = true;
                        btn_Masters.Visible = true;
                        //btn_branch.Visible = true;
                        btn.Visible = true;
                        tab_emp.Visible = false;
                        Session["del_empcode"] = password;

                        lbl_Error.Text = "Choose Any Option";
                    }
                    else
                    {
                        lbl_Error.Text = "Employee Code and Password does not match";
                    }
                   
                   
                    break;
                   
                default:
                    lbl_Error.Text = "Employee Code and Password does not match";
                    break;
                    
            }
            if (s_login_role == "u")
            {
                s_form = "7";
                ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);
                if (ds_userrights.Tables[0].Rows.Count > 0)
                {

                    ds_check_Employee = company.fn_get_Login_Employee(user_id, password);
                    if (ds_check_Employee.Tables[0].Rows.Count > 0)
                    {
                        btn_Employee.Visible = true;
                        btn_Masters.Visible = true;
                        //btn_branch.Visible = true;
                        btn.Visible = false;
                        tab_emp.Visible = false;

                        Session["del_empcode"] = password;

                        lbl_Error.Text = "Choose Any Option";
                    }
                    else
                    {
                        lbl_Error.Text = "Employee Code and Password does not match";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    protected void btn_Employee_Click(object sender, EventArgs e)
    {
        Session["ses_Deletion"] = 'E';
        Response.Redirect("Deletions.aspx");
    }

    protected void btn_Masters_Click(object sender, EventArgs e)
    {
        Session["ses_Deletion"] = 'M';
        Response.Redirect("Deletions.aspx");
    }

    protected void btn_branch_Click(object sender, EventArgs e)
    {
        Session["ses_Deletion"] = 'B';
        Response.Redirect("Deletions.aspx");
    }
    protected void btn_Click(object sender, EventArgs e)
    {

    }
}
