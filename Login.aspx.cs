using System;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using ePayHrms.BE.Recruitment;
using ePayHrms.Candidate;
using ePayHrms.Company;
using ePayHrms.Employee;
using System.Collections.Generic;
using System.Web.Services;
using System.Linq;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI;
//using System.Web.SessionState;

public partial class flash : System.Web.UI.Page
{
    Be_Recruitment r = new Be_Recruitment();
    private SqlConnection _Connection;
    Company company = new Company();
    Employee emp = new Employee();
    Candidate c = new Candidate();

    int emp_ID_photo, emp_id;
    string user_id, password, emp_img, _path = "", database_uid, database_pwd, t_database_uid, t_database_pwd, remember;
    DataSet ds_login_Temp, ds_login_Employee, ds_login_reg, ds_login_cand, ds_login_userrights;

    //HttpSessionState st = new HttpSessionState();

    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoStore();
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetValidUntilExpires(true);

        if (!IsPostBack)
        {
            if (Request.Cookies["user_id"] != null && Request.Cookies["password"] != null)
            {
                emp_userid.Value= Request.Cookies["user_id"].Value;
                //emp_password.Attributes["value"] = Request.Cookies["password"].Value;
                emp_password.Attributes.Add("value", Request.Cookies["password"].Value);
            }
        }

       // rememberme.Checked = true;


    }

    protected void btn_Employee_Click1(object sender, EventArgs e)
    {
        try
        {


            user_id = emp_userid.Value;
            password = emp_password.Value;

            Response.Cookies["Login_UserID"].Value = user_id;

            //ds_login_Temp = company.fn_get_Login_Temp("asd", password);



            //if (ds_login_Temp.Tables[0].Rows.Count > 0)
            //{
            //    t_database_pwd = ds_login_Temp.Tables[0].Rows[0][3].ToString();

            //    if (password == t_database_pwd)
            //    {

            //        Response.Cookies["Login_temp_CompanyID"].Value = (ds_login_Temp.Tables[0].Rows[0][0].ToString());
            //        Response.Cookies["Login_temp_BranchID"].Value = (ds_login_Temp.Tables[0].Rows[0][1].ToString());
            //        Response.Cookies["Roleid"].Value = (ds_login_Temp.Tables[0].Rows[0][4].ToString());
            //        Response.Cookies["Login_temp_Role"].Value = (ds_login_Temp.Tables[0].Rows[0][4].ToString());
            //        Response.Cookies["Login_temp_Status"].Value = (ds_login_Temp.Tables[0].Rows[0][5].ToString());
            //        Response.Cookies["Login_pwd"].Value = t_database_pwd;
            //        if (Response.Cookies["Login_temp_Role"].Value == "256")
            //        {
            //            Response.Cookies["Login_temp_Photo"].Value = "~/Photo/" + "Admin.GIF";
            //            //Session["Login_temp_Photo"] = Server.MapPath("~/Photo/") + "Admin.GIF";
            //            Response.Cookies["Login_Name"].Value = "Hi Administrator!";
            //        }
            //        else
            //        {
            //            //Session["Login_temp_Photo"] = Server.MapPath("~/Photo/") + "HR.JPG";
            //            Response.Cookies["Login_temp_Photo"].Value = "~/Photo/" + "HR.JPG";
            //            Response.Cookies["Login_Name"].Value = "Hi HR " + Session["Login_Userid"] + "!";
            //        }
            //        Response.Redirect("Company_Home.aspx", false);
            //    }
            //    else
            //    {
            //        lbl.Text = "Case Sensitive Problem";
            //    }
            //}
            //else
            //{
            //Employee Table Checking

            ds_login_Employee = company.fn_get_Login_Employee(user_id, password);

            if (ds_login_Employee.Tables[0].Rows.Count > 0)
            {


                //if (rememberme.Checked == true)
                //{
                //    //HttpCookie http = new HttpCookie(user_id, password);
                //    //http.Expires=DateTime.Now.AddDays(1);
                //    //  http.Expires= DateTime.Now.AddDays(30);
                //    Response.Cookies["user_id"].Expires = DateTime.Now.AddDays(30);
                //    Response.Cookies["password"].Expires = DateTime.Now.AddDays(30);
                //    //Response.Cookies.Add(http);

                //}
                //else
                //{
                //    Response.Cookies["user_id"].Expires = DateTime.Now.AddDays(-1);
                //    Response.Cookies["password"].Expires = DateTime.Now.AddDays(-1);

                //}



                //if (rememberme.Checked == true)
                //if (rememberme.Checked)
                //{
                Response.Cookies["Login_Password"].Value =database_pwd = ds_login_Employee.Tables[0].Rows[0][9].ToString();
                //Response.Cookies["user_id"].Expires = DateTime.Now.AddDays(30);
                //Response.Cookies["password"].Expires = DateTime.Now.AddDays(30);

                if (password == database_pwd)
                {
                    Response.Cookies["Roleid"].Value = (ds_login_Employee.Tables[0].Rows[0][0].ToString());
                    Response.Cookies["userid"].Value = (ds_login_Employee.Tables[0].Rows[0][3].ToString());

                    Response.Cookies["Login_temp_CompanyID"].Value = (ds_login_Employee.Tables[0].Rows[0][1].ToString());
                    Response.Cookies["Login_temp_BranchID"].Value = (ds_login_Employee.Tables[0].Rows[0][2].ToString());
                    Response.Cookies["Login_temp_EmployeeID"].Value = (ds_login_Employee.Tables[0].Rows[0][3].ToString());
                    Response.Cookies["preview_EmployeeID"].Value = ds_login_Employee.Tables[0].Rows[0][3].ToString();
                    Response.Cookies["Login_Name"].Value = "Hi " + ds_login_Employee.Tables[0].Rows[0][5].ToString() + "!";
                    Response.Cookies["Login_temp_EmpCodeName"].Value = ds_login_Employee.Tables[0].Rows[0][4].ToString() + "-" + ds_login_Employee.Tables[0].Rows[0][4].ToString();

                    emp_ID_photo = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
                    emp_img = emp.fn_GetEmployeePhoto(emp_ID_photo);


                    if (rememberme.Checked == true)
                    {
                        HttpCookie http = new HttpCookie(user_id, password);
                        http.Expires=DateTime.Now.AddDays(1);
                        //  http.Expires= DateTime.Now.AddDays(30);
                        //Response.Cookies["user_id"].Expires = DateTime.Now.AddDays(30);
                        //Response.Cookies["password"].Expires = DateTime.Now.AddDays(30);
                        Response.Cookies.Add(http);
                    }
                    else
                    {
                        Response.Cookies["user_id"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["password"].Expires = DateTime.Now.AddDays(-1);

                    }
                    Response.Cookies["user_id"].Value = "emp_userid";

                    if (emp_img != null)
                    {
                        //Session["Login_temp_Photo"] = Server.MapPath("~/Photo/") + emp_img;
                        Response.Cookies["Login_temp_Photo"].Value = "~/Photo/" + emp_img;
                    }
                    else
                    {
                        //Session["Login_temp_Photo"] = Server.MapPath("~/Photo/") + "Default.JPG";
                        Response.Cookies["Login_temp_Photo"].Value = "~/Photo/" + "Default.JPG";

                    }

                    if (Request.Cookies["Roleid"].Value == "256")
                    {
                        Response.Cookies["Login_temp_Role"].Value = "a";
                        Response.Cookies["Login_temp_Photo"].Value = "~/Photo/" + "Admin.GIF";
                        //Session["Login_temp_Photo"] = Server.MapPath("~/Photo/") + "Admin.GIF";
                        Response.Cookies["Login_Name"].Value = "Hi Administrator!";
                    }
                    else if (Request.Cookies["Roleid"].Value == "251")
                    {
                        Response.Cookies["Login_temp_Role"].Value = "h";
                        //Session["Login_temp_Photo"] = Server.MapPath("~/Photo/") + "HR.JPG";
                        Response.Cookies["Login_temp_Photo"].Value = "~/Photo/" + "HR.JPG";
                        Response.Cookies["Login_Name"].Value = "Hi HR!";
                    }
                    else
                    {
                        Response.Cookies["Login_temp_Role"].Value = "e";
                    }
                    //ds_login_userrights = company.fn_get_Login_UserAuthentication(Convert.ToInt32(Response.Cookies["Login_temp_EmployeeID"].Value));

                    //if (ds_login_userrights.Tables[0].Rows.Count > 0)
                    //{
                    //    Response.Cookies["Login_temp_Role"].Value = "u";
                    //    //Response.Redirect("Company_Home.aspx");
                    //}
                    //else
                    //{
                    //    Response.Cookies["Login_temp_Role"].Value = "h";
                    //}


                   // Response.Cookies["Cpage"].Value = Request.ServerVariables["HTTP_REFERER"];
                   // if (Request.Cookies["Cpage"].Value == "http://localhost:50964/Login.aspx" || Request.Cookies["Cpage"].Value == "http://192.168.1.10/HRMS/Login.aspx")
                //    {
                        Response.Redirect("Company_Home.aspx", false);
                   // }
                 //   else
                   // {
                     //   Response.Redirect(Request.Cookies["Cpage"].Value, false);
                    //}
                }

                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Case Sensitive Problem');};", true);
          
                }
            }
            //    else
            //    {
            //        Response.Cookies["user_id"].Expires = DateTime.Now.AddDays(-1);
            //        Response.Cookies["password"].Expires = DateTime.Now.AddDays(-1);
            //    }

            //}
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Invalid Username or Password');};", true);

            }

            //}
            //JobRequisitional Login

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('" + ex.Message + "');};", true);

        }
    }


}