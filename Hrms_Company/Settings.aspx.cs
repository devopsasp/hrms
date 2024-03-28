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
using ePayHrms.Employee;
using ePayHrms.Leave;


public partial class Hrms_Company_Default : System.Web.UI.Page
{

    Be_Recruitment r = new Be_Recruitment();

    private SqlConnection _Connection;

    ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();

    Company company = new Company();
    Employee employee = new Employee();

    Leave l = new Leave();
    Collection<Company> passwordList;
    Collection<Company> CompanyList;
    string l_userid,l_password, O_pwd, N_pwd;
    string s_form = "";
    string s_login_role;
    string status;
    DataSet ds_userrights;

    protected void Page_Load(object sender, EventArgs e)
    {

        
        l.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        switch (s_login_role)
        {
            case "a":
               

            case "h":
               
                break;

            case "e":

                break;

            case "M":

                break;

            case "u": s_form = "12";
                ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                if (ds_userrights.Tables[0].Rows.Count > 0)
                {
                    
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


    protected void btnsettings_Click(object sender, ImageClickEventArgs e)
    {
        

    }

    protected void back_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            Response.Redirect("Company_Home.aspx");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }

    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            
            status = Request.Cookies["Login_temp_Role"].Value;
            l_userid = Request.Cookies["Login_UserID"].Value;
            l_password = Request.Cookies["Login_Password"].Value;
            O_pwd = txtoldpwd.Text;
            N_pwd = txtNewpwd.Text;

            //passwordList = company.fn_getPassword(_userid,_Opwd); 

            if (O_pwd != N_pwd)
            {
                company.fn_get_Update_Pwd(Convert.ToChar(status), l_userid, O_pwd, N_pwd);

                Request.Cookies["Login_Password"].Value = N_pwd;

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Password Changed Successfully');", true);

                txtConfirmpwd.Value = "";
                txtNewpwd.Text = "";
                txtoldpwd.Text = "";
                //ClientScriptManager manager2 = Page.ClientScript;

                //manager2.RegisterStartupScript(this.GetType(), "Call", "clearAll();", true);
            
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Current Password and New password should not be the same');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }       
    }
    protected void img_clear_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void txtoldpwd_TextChanged(object sender, EventArgs e)
    {
       
        l_userid = Request.Cookies["Login_UserID"].Value;
       // l_password = (string)Session["Login_Password"];
        passwordList = company.fn_getPassword(l_userid);
        if (passwordList[0].Password != txtoldpwd.Text)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Incorrect Password');", true);
            txtoldpwd.Text = "";
            txtoldpwd.Focus();
        }
        else
        {
            txtNewpwd.Focus();
        }
    }
}
