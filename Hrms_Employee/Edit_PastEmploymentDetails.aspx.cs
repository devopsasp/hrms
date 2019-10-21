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
public partial class Hrms_Employee_Edit_PastEmploymentDetails : System.Web.UI.Page
{
    Collection<Employee> EmpBankList;
    Company company = new Company();
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    Candidate c = new Candidate();
    Collection<Employee> PastEmploymentList;

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
                    employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
                    employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                    employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                    AcceePastEmploymentDetails();
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

    public void AcceePastEmploymentDetails()
    {
        try
        {
            PastEmploymentList = employee.fn_getPastEmploymentDetails(employee);
            if (PastEmploymentList.Count > 0)
            {
                txtSalaryStructure.Text = PastEmploymentList[0].salary.ToString();
                txtPositionHeld.Text = PastEmploymentList[0].position.ToString();
                txtTrainingAttended.Text = PastEmploymentList[0].training_attended.ToString();
                txtTrainingDuration.Text = PastEmploymentList[0].training_duration.ToString();
                Ref1_PersonName.Text = PastEmploymentList[0].Ref1_Name.ToString();
                Ref1_ContactPhoneNo.Text = PastEmploymentList[0].Ref1_Phno.ToString();
                Ref1_ContactEmail.Text = PastEmploymentList[0].Ref1_Email.ToString();
                Ref1_Relationship.Text = PastEmploymentList[0].Ref1_Relation.ToString();
                Ref2_PersonName.Text = PastEmploymentList[0].Ref2_Name.ToString();
                Ref2_ContactPhoneNo.Text = PastEmploymentList[0].Ref2_Phno.ToString();
                Ref2_ContactEmailID.Text = PastEmploymentList[0].Ref2_Email.ToString();
                Ref2_Relationship.Text = PastEmploymentList[0].Ref2_Relation.ToString();
            }

        }
        catch(Exception ex)
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
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Error Occured');};", true);
        }
    }   

    public void save()
    {
        employee.salary = txtSalaryStructure.Text;
        employee.position = txtPositionHeld.Text;
        employee.training_attended = txtTrainingAttended.Text;
        employee.training_duration = txtTrainingDuration.Text;
        employee.Ref1_Name = Ref1_PersonName.Text;
        employee.Ref1_Phno = Ref1_ContactPhoneNo.Text;
        employee.Ref1_Email = Ref1_ContactEmail.Text;
        employee.Ref1_Relation = Ref1_Relationship.Text;
        employee.Ref2_Name = Ref2_PersonName.Text;
        employee.Ref2_Phno = Ref2_ContactPhoneNo.Text;
        employee.Ref2_Email = Ref2_ContactEmailID.Text;
        employee.Ref2_Relation = Ref2_Relationship.Text;
        _Value = employee.PastEmployment_Details(employee);
    }

}