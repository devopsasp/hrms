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
using ePayHrms.Employee;
using System.Collections.Generic;
using System.Collections.ObjectModel;


public partial class Hrms_Company_Default : System.Web.UI.Page
{
    Be_Recruitment r = new Be_Recruitment();
    private SqlConnection _Connection;
    Company company = new Company();
    Employee emp = new Employee(); 

    string user_id, password;
    DataSet  ds_login_Employee;

    Collection<Company> CompanyList;


    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        CompanyList = company.fn_getCompany();

        if (CompanyList.Count <= 0)
        {
            Response.Cookies["Msg_Session"].Value = "Create Company";
            Response.Redirect("Company_Home.aspx");
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
    protected void btn_save_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            user_id = txtuserid.Value;
            password = txtpassword.Value;

            //Employee Table Checking


            ds_login_Employee = company.fn_get_Login_Employee(user_id, password);

            if (ds_login_Employee.Tables[0].Rows.Count > 0)
            {
                Session["rec_empcode"] = user_id;
                Response.Redirect("../Hrms_Recruitment/Recruitment_Home.aspx");

            }
            else
            {
                lblmsg.Text = "Invalid username or password";

            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";


        }



    }



}
