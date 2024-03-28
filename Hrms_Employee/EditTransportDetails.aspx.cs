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

public partial class Hrms_Employee_EditTransportDetails : System.Web.UI.Page
{
    Company company = new Company();
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    Candidate c = new Candidate();
    string _Value1;

    Collection<Employee> EmpTransportList;
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
                    AccessTransportDetails();
                    break;

                case "h":
                    AccessTransportDetails();
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
    
    public void AccessTransportDetails()
    {
        try
        {

            employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
            pay.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
            c.EmployeeID = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);

            if (s_login_role == "a")
            {
                //employee.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
                //pay.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
                TransportDetails();
            }

            else if (s_login_role == "h")
            {
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                 pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                TransportDetails();
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
                TransportDetails();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Error Occured');};", true);
        }
    }
    public void TransportDetails()
    {
        try
        {
            EmpTransportList = employee.fn_get_bus(employee);
            if (EmpTransportList.Count > 0)
            {
                Txt_area.Value = EmpTransportList[0].Area_name.ToString();
                txt_vehicle.Value = EmpTransportList[0].Veh_id.ToString();
                Txt_veh_number.Value = EmpTransportList[0].Veh_number.ToString();
                Txt_point.Value = EmpTransportList[0].Boarding_point.ToString();
                Txt_driver.Value = EmpTransportList[0].Driver_name.ToString();
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
                //employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                //employee.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
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
        employee.Area_name = Txt_area.Value;
        employee.Veh_id = txt_vehicle.Value;
        employee.Veh_number = Txt_veh_number.Value;        
        employee.Boarding_point = Txt_point.Value;
        employee.Driver_name = Txt_driver.Value;
        _Value1 = employee.Transport_details(employee);
    }    
}