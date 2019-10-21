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
using System.Data.SqlClient;

public partial class Hrms_Employee_Default2 : System.Web.UI.Page
{
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    //private SqlConnection _con;
    //ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlCommand cmd = new SqlCommand();

    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    Candidate c = new Candidate();
    Company company = new Company();

    Collection<Employee> EmployeesList;
    Collection<Employee> emp_ID_List;
    Collection<Employee> emp_available;
    Collection<Employee> EmpFirstList;
    Collection<Employee> EmpGeneralList;

    Collection<PayRoll> emp_edu_List;
    Collection<PayRoll> Empty_gridList;



    int i, yr_it, cur_yr, mon, dat, year, pr_emp;
    string _Value, _data, dt, mn, yr, dob_edit, default_sqldate = "01/01/1900", _Value1;
    string s_login_role;

    string s_form = "", session_emp_code;
    DataSet ds_userrights;


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            s_login_role = Request.Cookies["Login_temp_Role"].Value;

            if (s_login_role != "e")
            {
                if (Convert.ToInt32(Request.Cookies["Select_Employee"].Value) == 1)
                {
                    

                    employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
                    employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

                    pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
                     pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

                    c.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
                    c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

                    pr_emp = Convert.ToInt32(Request.Cookies["preview_emp"].Value);

                    if (!IsPostBack)
                    {
                        switch (s_login_role)
                        {
                            case "a": if (pr_emp == 1)
                                {

                                }
                                else
                                {
                                    employee.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
                                    employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);

                                    pay.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
                                    pay.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);

                                    c.BranchID = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
                                    c.EmployeeID = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);

                                }
                                break;

                            case "h": if (pr_emp == 1)
                                {

                                }
                                else
                                {
                                    employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                    employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);

                                     pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                    pay.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);

                                    c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                    c.EmployeeID = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                                }
                                break;


                            case "u": s_form = "32";

                                ds_userrights = company.check_Userrights(Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value), s_form);

                                if (ds_userrights.Tables[0].Rows.Count > 0)
                                {
                                    if (pr_emp == 1)
                                    {

                                    }
                                    else
                                    {

                                        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                        employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);

                                         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                        pay.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);

                                        c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                        c.EmployeeID = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);

                                    }

                                }
                                else
                                {

                                    Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                                    Response.Redirect("Employee_Preview.aspx");
                                }

                                break;

                            default: Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                                Response.Redirect("~/Company_Home.aspx");
                                break;

                        }

                    }

                }
                else
                {
                    Session["ErrorMsg"] = "Employee should be selected";
                    Response.Redirect("../Hrms_Company/Employee.aspx");
                }

            }
            else
            {
                Session["emp_menu"] = 0;
                Response.Redirect("Employee_Preview.aspx");
            }

        }
        catch (Exception ex)
        {
            Response.Cookies["Msg_Session"].Value=  "Error Occurred";

            Response.Redirect("~/Company_Home.aspx");


        }

    }


    protected void Img_Save_Click(object sender, ImageClickEventArgs e)
    {
        
    }
}

