using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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

public partial class Hrms_Employee_EditFamilyDetails : System.Web.UI.Page
{
    Company company = new Company();
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    Candidate c = new Candidate();


    Collection<Employee> EmpFamilyList;
    string s_login_role;
    string _Value;

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
            Response.Cookies["preview_EmployeeID"].Value = employee.EmployeeId.ToString();
        }
        if (s_login_role == "M")
        {
            employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
            pay.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
            c.EmployeeID = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
            Response.Cookies["preview_EmployeeID"].Value = employee.EmployeeId.ToString();
        }


        if (!IsPostBack)
        {
            
            switch (s_login_role)
            {
                case "a":
                    
                    break;

                case "h":
                    AccessFamilyDetails();
                    break;

                case "e":
                   
                    break;

                case "M":

                    break;
                case "u":
                    
                    break;


                default:
                    Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                    Response.Redirect("~/Company_Home.aspx");
                    break;

            }

        }
    }
    

    public void AccessFamilyDetails()
    {
        try
        {
            
            employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
            pay.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
            c.EmployeeID = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);

            if (s_login_role == "a")
            {
                employee.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
                pay.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
                family();
            }

            else if (s_login_role == "h")
            {
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                 pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                family();
            }

            else if (s_login_role == "u")
            {
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                 pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
               
            }
            else
            {
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                 pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                family();
            }
           

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Error Occured');};", true);
        }

       
    }

    public void family()
    {
        try
        {
            EmpFamilyList = employee.fn_get_Emp_general(employee);
            if (EmpFamilyList.Count > 0)
            {
                ddl_retrive_Work();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Error Occured');};", true);
        }
    }

    public void ddl_retrive_Work()
    {
        try
        {
            if (EmpFamilyList[0].FatherName.ToString() != "")
            {
                Text1.Value = EmpFamilyList[0].FatherName.ToString();
                
            }

            if (EmpFamilyList[0].MotherName.ToString() != "")
            {
                txt_mother.Value = EmpFamilyList[0].MotherName.ToString();
            }
            if (EmpFamilyList[0].SpouseName.ToString() != "")
            {
                txt_Spouse.Value = EmpFamilyList[0].SpouseName.ToString();
            }
            if (EmpFamilyList[0].Children.ToString() != "")
            {
                txt_child.Value = EmpFamilyList[0].Children.ToString();
            }
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Error Occured');};", true);
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
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Error Occured');};", true);
        }
    }
    protected void btnUpdate_Familydetails_Click(object sender, EventArgs e)
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
                save();
            }
           

            if (_Value != "1")
            {
                
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Updated Successfully');};", true);

            }
            //else
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
            //}
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Error Occured');};", true);
        }
    }


    public void save()
    {
        employee.FatherName = Text1.Value;
        employee.MotherName = txt_mother.Value;
        employee.SpouseName = txt_Spouse.Value;
        employee.Children = txt_child.Value;
        _Value = employee.Employee_Family(employee);      
    }
    
}