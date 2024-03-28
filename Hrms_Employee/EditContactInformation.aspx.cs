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

public partial class Hrms_Employee_EditContactInformation : System.Web.UI.Page
{
    Company company = new Company();
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    Candidate c = new Candidate();


    Collection<Employee> ContactList;
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
                    AccessContactDetails();
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

    public void AccessContactDetails()
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
                Contact();
            }

            else if (s_login_role == "h")
            {
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                 pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                Contact();
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
                Contact();
            }


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }


    }


    public void Contact()
    {
        try
        {
            ContactList = employee.fn_get_Emp_general(employee);
            if (ContactList.Count > 0)
            {

                ddl_retrive_Work();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }

    }

    public void ddl_retrive_Work()
    {
        try
        {
            if (ContactList[0].HouseNo.ToString() != "")
            {
                txtPresentHouseNo.Value = ContactList[0].HouseNo.ToString();
            }
            if (ContactList[0].AddressLine1.ToString() != "")
            {
                txtPresentAddressLine1.Value = ContactList[0].AddressLine1.ToString();
            }
            if (ContactList[0].AddressLine2.ToString() != "")
            {
                txtPresentAddressLine2.Value = ContactList[0].AddressLine2.ToString();
            }
            if (ContactList[0].City.ToString() != "")
            {
                txtPresentCity .Value= ContactList[0].City.ToString();
            }
            if (ContactList[0].State.ToString() != "")
            {
                txtPresentState.Value = ContactList[0].State.ToString();
            }
            if (ContactList[0].StreetName.ToString() != "")
            {
                txtPresentStreetName.Value = ContactList[0].StreetName.ToString();
            }
            if (ContactList[0].p_HouseNo.ToString() != "")
            {
                txtPermanentHouseNo.Value = ContactList[0].p_HouseNo.ToString();
            }
            if (ContactList[0].P_AddressLine1.ToString() != "")
            {
                txtPermanentAddressLine1.Value = ContactList[0].P_AddressLine1.ToString();
            }
            if (ContactList[0].P_AddressLine2.ToString() != "")
            {
                txtPermanentAddressLine2.Value = ContactList[0].P_AddressLine2.ToString();
            }
            if (ContactList[0].P_State.ToString() != "")
            {
                txtPermanentState.Value = ContactList[0].P_State.ToString();
            }
            if (ContactList[0].P_City.ToString() != "")
            {
                txtPermanentCity.Value = ContactList[0].P_City.ToString();
            }
            if (ContactList[0].p_StreetName.ToString() != "")
            {
                txtPermanentStreetName.Value = ContactList[0].p_StreetName.ToString();
            }
            if (ContactList[0].ph_Office.ToString() != "")
            {
                txtOfficeNo.Value = ContactList[0].ph_Office.ToString();
            }
            if (ContactList[0].Alt_Officeno.ToString() != "")
            {
                txtAltOfficeNo.Value = ContactList[0].Alt_Officeno.ToString();
            }
            if (ContactList[0].ph_Residence.ToString() != "")
            {
                txtRecidenceNo.Value = ContactList[0].ph_Residence.ToString();
            }
            if (ContactList[0].Alt_ResidenceNo.ToString() != "")
            {
                txtAltRecidenceNo.Value = ContactList[0].Alt_ResidenceNo.ToString();
            }
            if (ContactList[0].CellNo.ToString() != "")
            {
                txtCellNo.Value = ContactList[0].CellNo.ToString();
            }
            if (ContactList[0].Alt_CellNo.ToString() != "")
            {
                txtAltCellNo.Value = ContactList[0].Alt_CellNo.ToString();
            }
            if (ContactList[0].EmailId.ToString() != "")
            {
                txtEmailId.Value = ContactList[0].EmailId.ToString();
            }
            if (ContactList[0].A_EmailId.ToString() != "")
            {
                txtAEmailId.Value = ContactList[0].A_EmailId.ToString();
            }
            if (ContactList[0].Fax.ToString() != "")
            {
                txtFaxNo.Value = ContactList[0].Fax.ToString();
            }
            if (ContactList[0].emgname.ToString() != "")
            {
                txtemgname.Value = ContactList[0].emgname.ToString();
            }
            if (ContactList[0].emgno.ToString() != "")
            {
                txtemgno.Value = ContactList[0].emgno.ToString();
            }
            if (ContactList[0].empaddress.ToString() != "")
            {
                txtemgaddress.Text = ContactList[0].empaddress.ToString();
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
            
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Error Occured');};", true);
        }
    }


    public void save()
    {
        employee.HouseNo = txtPresentHouseNo.Value;
        employee.AddressLine1 = txtPresentAddressLine1.Value;
        employee.AddressLine2 = txtPresentAddressLine2.Value;
        employee.City = txtPresentCity.Value;
        employee.State = txtPresentState.Value;
        employee.StreetName = txtPresentStreetName.Value;
        employee.p_HouseNo = txtPermanentHouseNo.Value;
        employee.P_AddressLine1 = txtPermanentAddressLine1.Value;
        employee.P_AddressLine2 = txtPermanentAddressLine2.Value;
        employee.P_State = txtPermanentState.Value;
        employee.P_City = txtPermanentCity.Value;
        employee.p_StreetName = txtPermanentStreetName.Value;
        employee.ph_Office = txtOfficeNo.Value;
        employee.Alt_Officeno = txtAltOfficeNo.Value;
        employee.ph_Residence = txtRecidenceNo.Value;
        employee.Alt_ResidenceNo = txtAltRecidenceNo.Value;
        employee.CellNo = txtCellNo.Value;
        employee.Alt_CellNo = txtAltCellNo.Value;
        employee.EmailId = txtEmailId.Value;
        employee.A_EmailId = txtAEmailId.Value;
        employee.Fax = txtFaxNo.Value;
        employee.emgname = txtemgname.Value;
        employee.emgno = txtemgno.Value;
        employee.empaddress = txtemgaddress.Text;
        _Value = employee.Employee_Contact(employee);

    }
}