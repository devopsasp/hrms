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

public partial class Hrms_Training_Default : System.Web.UI.Page
{
    Company company = new Company();
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();

    Collection<Company> CompanyList;

    string s_login_role;

    protected void Page_Load(object sender, EventArgs e)
    {
        s_login_role = Request.Cookies["Login_temp_Role"].Value;


        if (!IsPostBack)
        {

            CompanyList = company.fn_getCompany();

            if (s_login_role == "a" || s_login_role == "h" || s_login_role == "u")
            {
                if (CompanyList.Count > 0)
                {
                    
                }
                else
                {
                    Response.Cookies["Msg_Session"].Value=  "Please Create Company";
                    Response.Redirect("~/Company_Home.aspx");
                }
            }
            else
            {
                Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator.";
                Response.Redirect("~/Company_Home.aspx");
            }


        }




    }



}
