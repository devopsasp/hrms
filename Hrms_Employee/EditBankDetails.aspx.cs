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

public partial class Hrms_Employee_EditBankDetails : System.Web.UI.Page
{
    Collection<Employee> EmpBankList;
    Company company = new Company();
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    Candidate c = new Candidate();

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


        if (!IsPostBack)
        {

            switch (s_login_role)
            {
                case "a":

                    break;

                case "h":
                    AccessBankDetails();
                    break;

                case "e":

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

    public void AccessBankDetails()
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
                Bank();
            }

            else if (s_login_role == "h")
            {
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                 pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                Bank();
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
                Bank();
            }


        }
        catch (Exception ex)
        {
          
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Error Occured');};", true);
        }


    }
    public void Bank()
    {
        try
        {
            EmpBankList = employee.fn_get_Emp_Bank(employee);
            if (EmpBankList.Count > 0)
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
            if(EmpBankList[0].Bank_Code.ToString()!="")
            {
                txt_bankcode.Value = EmpBankList[0].Bank_Code.ToString();
            }
            if (EmpBankList[0].Bank_Name.ToString() != "")
            {
                txt_bankname.Value = EmpBankList[0].Bank_Name.ToString();
            }
            if (EmpBankList[0].Branch_Name.ToString() != "")
            {
                txt_branchname.Value = EmpBankList[0].Branch_Name.ToString();
            }
            if (EmpBankList[0].Account_Type.ToString() != "")
            {
                txt_actype.Value = EmpBankList[0].Account_Type.ToString();
            }
            if (EmpBankList[0].MICR_Code.ToString() != "")
            {
                txt_micrcode.Value = EmpBankList[0].MICR_Code.ToString();
            }
            if (EmpBankList[0].IFSC_Code.ToString() != "")
            {
                txt_ifsccode.Value = EmpBankList[0].IFSC_Code.ToString();
            }
            if (EmpBankList[0].Address.ToString() != "")
            {
                txt_address.Text = EmpBankList[0].Address.ToString();
            }
            if (EmpBankList[0].Other_Info.ToString() != "")
            {
                txt_otherinfo.Text = EmpBankList[0].Other_Info.ToString();
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

    protected void btnUpdate_Click(object sender, EventArgs e)
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
        employee.Bank_Code = txt_bankcode.Value;
        employee.Bank_Name = txt_bankname.Value;
        employee.Branch_Name = txt_branchname.Value;
        employee.Account_Type = txt_actype.Value;
        employee.MICR_Code = txt_micrcode.Value;
        employee.IFSC_Code = txt_ifsccode.Value;
        employee.Address = txt_address.Text;
        employee.Other_Info = txt_otherinfo.Text;
        _Value = employee.Employee_Bank(employee);
    }
}