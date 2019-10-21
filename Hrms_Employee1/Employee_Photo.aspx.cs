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


public partial class Hrms_Employee_Default5 : System.Web.UI.Page
{

    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    Company company = new Company();

    Collection<Employee> EmployeesList;   
    Collection<Employee> emp_ID_List;
    Collection<Employee> EmpPhotoList;

    int Photo_res_count,Photo_res_count1, pr_emp;
    string _Value, Photo_res, Photo_name,_path="",appPath="",physicalPath="";
    string _Value1, Photo_res1, Photo_name1, _path1 = "", appPath1 = "", physicalPath1 = "";
    string s_login_role;
    string s_form = "";
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
            pr_emp = Convert.ToInt32(Request.Cookies["Profile_Check"].Value);
           


            if (!IsPostBack)
            {


                if (Request.Cookies["Employee_Code_FirstLastName"].Value != "")
                {

                    lbl_empcodename.Text = Request.Cookies["Employee_Code_FirstLastName"].Value;
                }
                else
                {
                    lbl_empcodename.Text = "New Employee";

                }

                if (Request.Cookies["Profile_Check"].Value == "1")
                {


                switch (s_login_role)
                {

                    case "a": if (pr_emp == 1)
                        {
                            btn_update.Visible = false;
                            img_emp_photo.ImageUrl = Server.MapPath("~/Photo/") + "Default.JPG";
                            img_emp_photo1.ImageUrl = Server.MapPath("~/Photo/") + "Default.JPG";
                        }
                        else
                        {

                            btn_save.Visible = false;                           
                            employee.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
                            employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                            admin();
                        }
                        break;

                    case "h": if (pr_emp == 1)
                        {
                            
                            img_emp_photo.ImageUrl = Server.MapPath("~/Photo/") + "Default.JPG";
                            img_emp_photo1.ImageUrl = Server.MapPath("~/Photo/") + "Default.JPG";
                          btn_update.Visible = false;
                          }
                        else
                        {
                                        btn_save.Visible = false;
                                                        
                            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                            employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                            admin();
                        }
                        break;


                    case "u": s_form = "40";

                        ds_userrights = company.check_Userrights(Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value), s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            if (pr_emp == 1)
                            {
                                btn_update.Visible = false;
                                img_emp_photo.ImageUrl = Server.MapPath("~/Photo/") + "Default.JPG";
                            }
                            else
                            {
                                btn_save.Visible = false;
                                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                                admin();
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
            else
            {

                Session["Profile_Error"] = "Complete Your Profile To proceed Forther";

                Response.Redirect("Employee_Profile.aspx");

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
            Session["emp_menu"] = 8;
            Response.Redirect("Employee_Preview.aspx");

        }

        }
        catch (Exception ex)
        {
            Response.Cookies["Msg_Session"].Value=  "Error Occurred";
            Response.Redirect("~/Company_Home.aspx");
        }
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
           // Response.Cache.SetExpires(System.DateTime.Now);

            if (s_login_role == "a")
            {
                //employee.EmployeeId = 0;
                employee.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
            }
            if (s_login_role == "h")
            {
                //employee.EmployeeId = 0;
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            }
            if (UploadImage1.FileName != "")
            {
                Photo_res1 = UploadImage1.FileName.ToString();
                Photo_res_count1 = Photo_res1.Length - 3;
                Photo_name1 = Request.Cookies["emp_Code"].Value + "family." + Photo_res1.Substring(Photo_res_count1, 3);

                UploadImage1.SaveAs(Server.MapPath("~/Photo/") + Photo_name1);
                employee.img_path = Photo_name1;
            }
            if (UploadImage.FileName != "")
            {
                Photo_res = UploadImage.FileName.ToString();
                Photo_res_count = Photo_res.Length - 3;
                Photo_name = Request.Cookies["emp_Code"].Value + "." + Photo_res.Substring(Photo_res_count, 3);
                UploadImage.SaveAs(Server.MapPath("~/Photo/") + Photo_name);                    
                employee.img_path = Photo_name;                                             
            }
            else
            {
                employee.img_path = "Default.JPG";
            }

            emp_ID_List = employee.fn_get_EmployeeID(Request.Cookies["emp_Code"].Value);

            if (emp_ID_List.Count > 0)
            {
                employee.EmployeeId = Convert.ToInt32(emp_ID_List[0].EmployeeId);
                _Value = employee.Employee_Photo(employee);
            }
            else
            {
                Response.Redirect("Employee_Profile.aspx");
            }

            if (_Value != "1")
            {
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Added Successfully');", true);
                Response.Redirect("Employee_Preview.aspx");
                //img_emp_photo.ImageUrl = Server.MapPath("~/Photo/") + Photo_name;
                //img_emp_photo.AlternateText = "Employee Code -" + employee.EmployeeCode;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

    protected void btn_Back_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Employee_Preview.aspx");

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

    protected void btn_update_Click(object sender, EventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                employee.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
            }
            if (s_login_role == "h")
            {
                employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            }
            Photo_res = UploadImage.FileName.ToString();
            if (UploadImage.FileName != "")
            {
                Photo_res = UploadImage.FileName.ToString();
                Photo_res_count = Photo_res.Length - 3;                                       
                Photo_name = employee.fn_GetEmployeeCode(employee.EmployeeId) + "." + Photo_res.Substring(Photo_res_count, 3);
                UploadImage.SaveAs(Server.MapPath("~/Photo/") + Photo_name);
                employee.img_path = Photo_name;               
            }
            else
            {
                employee.img_path = "Default.JPG";
            }          
             _Value = employee.Employee_Photo(employee);
            if (_Value != "1")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Added Successfully');", true);
                img_emp_photo.ImageUrl = Server.MapPath("~/Photo/") + Photo_name;
                Response.Redirect("Employee_Preview.aspx");
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    } 
    
    public void admin()
    {
        try
        {
            EmpPhotoList = employee.fn_get_Emp_photo(employee);

            if (EmpPhotoList.Count > 0)
            {
                img_emp_photo.ImageUrl = "~/Photo/" + EmpPhotoList[0].img_path;             
            }
            else
            {
                img_emp_photo.ImageUrl = "~/Photo/" + "Default.JPG";
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }  
}
