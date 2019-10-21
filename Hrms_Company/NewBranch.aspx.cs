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

public partial class Hrms_Company_Default2 : System.Web.UI.Page
{
    Be_Recruitment r = new Be_Recruitment();

    private SqlConnection _Connection;

    ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();

    Company company = new Company();
    Employee employee = new Employee();

    Collection<Company> ddlBranchsList;
    Collection<Company> BranchList;
    Collection<Company> BanchCodeList;
    Collection<Company> BranchIdList;

    Collection<Company> CompanyList;

    Collection<Company> availablityList;

    string _Code, _Value;
    string s_login_role;
    int companyid,branchid,ddl_i,ddl_j,ad_new;
    string s_form = "";
    DataSet ds_userrights;


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            

            s_login_role = Request.Cookies["Login_temp_Role"].Value;
            ad_new = (int)Session["admin_new"];


            if (!IsPostBack)
            {


                switch (s_login_role)
                {

                    case "a": if (ad_new == 1)
                        {

                            btn_save.Visible = true;
                            btn_update.Visible = false;
                        }
                        else
                        {

                            btn_save.Visible = false;
                            btn_update.Visible = true;
                            admin();
                        }
                        break;

                    case "h":

                        btn_save.Visible = false;
                        btn_update.Visible = true;
                        hr();
                        break;

                    case "u": s_form = "2";
                        btn_save.Visible = false;
                        btn_update.Visible = true;

                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            hr();
                        }
                        else
                        {
                            Response.Redirect("Company_Home.aspx");
                        }

                        break;

                    default: Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                        Response.Redirect("Company_Home.aspx");
                        break;
                }

            }



            if (IsPostBack)
            {
                //Save Branch

                if (ToolBarCode.Value == "1")
                {

                    // txtBranchCode.Disabled = false;

                    _Code = txtBranchCode.Value;

                    BanchCodeList = company.fn_getBranchcode(_Code);


                    if (BanchCodeList.Count <= 0)
                    {

                        availablityList = company.fn_get_Availablity(txtUserID.Value);

                        if (availablityList.Count <= 0)
                        {

                            //Create Branch

                            company.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
                            company.BranchCompanyId = Convert.ToInt32(hBranchID.Value);

                            company.CompanyCode = txtBranchCode.Value;
                            company.CompanyName = txtBranchName.Value;
                            company.AddressLine1 = txtAddressLine1.Text;
                            company.AddressLine2 = txtAddressLine2.Text;
                            company.City = txtCity.Value;
                            company.ZipCode = txtZipCode.Value;
                            company.CountryName = txtCountry.Value;
                            company.StateName = txtState.Value;
                            company.PhoneNo = txtPhoneNo.Value;
                            company.FaxNo = txtFaxNo.Value;
                            company.EmailId = txtEmailId.Value;
                            company.Alternate_EmailId = txtAlternateEmailId.Value;
                            company.UserId = txtUserID.Value;
                            company.Password = txtPassword.Value;
                            company.role = 'h';
                            company.status = 'Y';
                            company.PFCode = txtPFCode.Value;
                            company.ESICode = txtESICode.Value;
                            company.Start_date = employee.Convert_ToSqlDate(txtStartDate.Value);
                            company.End_date = employee.Convert_ToSqlDate(txtEndDate.Value);

                            company.Branch_Create(company);



                            //*********************************Auto Assigned Masters**********************************

                            //Assigned First Masters      

                            BranchIdList = company.fn_getBranchcode(txtBranchCode.Value);

                            if (BranchIdList.Count > 0)
                            {

                                employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
                                employee.BranchId = BranchIdList[0].BranchCompanyId;

                                employee.DepartmentId = 1;
                                employee.DesignationId = 1;
                                employee.DivisionId = 1;
                                employee.GradeId = 1;
                                employee.JobStatusId = 1;
                                employee.LevelId = 1;
                                employee.ShiftId = 1;
                                employee.CategoryId = 1;
                                employee.ProjectsiteId = 1;

                                _Value = employee.Category(employee);
                                _Value = employee.Department(employee);
                                _Value = employee.Designation(employee);
                                _Value = employee.Division(employee);
                                _Value = employee.Grade(employee);
                                _Value = employee.JobStatus(employee);
                                _Value = employee.Level(employee);
                                _Value = employee.Shift(employee);
                                _Value = employee.projectsite(employee);


                            }

                            //****************************************************************************************************


                            //ClientScriptManager manager = Page.ClientScript;
                            //manager.RegisterStartupScript(this.GetType(), "Call", "show_message('Branch created Sucessfully');", true);

                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Branch created successfully');", true);


                            ToolBarCode.Value = "0";

                        }
                        else
                        {

                            ClientScriptManager manager = Page.ClientScript;

                            manager.RegisterStartupScript(this.GetType(), "Call", "show_message('UserID already Exist');", true);


                        }

                    }
                    else
                    {

                        //error message

                        ClientScriptManager manager = Page.ClientScript;

                        manager.RegisterStartupScript(this.GetType(), "Call", "show_message('Branch Code already exists');", true);

                    }




                }




                //Edit Branch


                if (ToolBarCode.Value == "2")
                {





                    company.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);

                    if (s_login_role == "a")
                    {

                        company.BranchCompanyId = (int)Session["ddl_Branch_ID"];

                    }
                    if (s_login_role == "h")
                    {
                        company.BranchCompanyId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

                    }

                    company.CompanyCode = txtBranchCode.Value;
                    company.CompanyName = txtBranchName.Value;
                    company.AddressLine1 = txtAddressLine1.Text;
                    company.AddressLine2 = txtAddressLine2.Text;
                    company.City = txtCity.Value;
                    company.ZipCode = txtZipCode.Value;
                    company.CountryName = txtCountry.Value;
                    company.StateName = txtState.Value;
                    company.PhoneNo = txtPhoneNo.Value;
                    company.FaxNo = txtFaxNo.Value;
                    company.EmailId = txtEmailId.Value;
                    company.Alternate_EmailId = txtAlternateEmailId.Value;
                    company.UserId = "";
                    company.Password = "";
                    company.role = 'h';
                    company.status = 'Y';
                    company.PFCode = txtPFCode.Value;
                    company.ESICode = txtESICode.Value;
                    company.Start_date = employee.Convert_ToSqlDate(txtStartDate.Value);
                    company.End_date = employee.Convert_ToSqlDate(txtEndDate.Value);
                    company.Branch_Create(company);

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Branch updated successfully');", true);
                }
                ToolBarCode.Value = "0";

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

            companyid = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
            branchid = (int)Session["ddl_Branch_ID"];

            BranchList = company.fn_getBranchCompany(branchid);

            if (BranchList.Count > 0)
            {


                txtBranchCode.Value = BranchList[0].CompanyCode;
                txtBranchName.Value = BranchList[0].CompanyName;
                txtAddressLine1.Text = BranchList[0].AddressLine1;
                txtAddressLine2.Text= BranchList[0].AddressLine2;
                txtCity.Value = BranchList[0].City;
                txtZipCode.Value = BranchList[0].ZipCode;
                txtCountry.Value = BranchList[0].CountryName;
                txtState.Value = BranchList[0].StateName;
                txtPhoneNo.Value = BranchList[0].PhoneNo;
                txtFaxNo.Value = BranchList[0].FaxNo;
                txtEmailId.Value = BranchList[0].EmailId;
                txtAlternateEmailId.Value = BranchList[0].Alternate_EmailId;
                txtStartDate.Value = BranchList[0].Start_date.ToShortDateString();
                txtEndDate.Value = BranchList[0].End_date.ToShortDateString();
                txtPFCode.Value = BranchList[0].PFCode;
                txtESICode.Value = BranchList[0].ESICode;
                txtBranchCode.Disabled = true;

            }
            else
            {
                Response.Redirect("Company_Home.aspx");

            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);


        }

    }

    public void hr()
    {
        try
        {
            companyid = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
            branchid = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

            BranchList = company.fn_getBranchCompany(branchid);

            if (BranchList.Count > 0)
            {
                txtBranchCode.Value = BranchList[0].CompanyCode;
                txtBranchName.Value = BranchList[0].CompanyName;
                txtAddressLine1.Text = BranchList[0].AddressLine1;
                txtAddressLine2.Text = BranchList[0].AddressLine2;
                txtCity.Value = BranchList[0].City;
                txtZipCode.Value = BranchList[0].ZipCode;
                txtCountry.Value = BranchList[0].CountryName;
                txtState.Value = BranchList[0].StateName;
                txtPhoneNo.Value = BranchList[0].PhoneNo;
                txtFaxNo.Value = BranchList[0].FaxNo;
                txtEmailId.Value = BranchList[0].EmailId;
                txtAlternateEmailId.Value = BranchList[0].Alternate_EmailId;
                txtStartDate.Value = BranchList[0].Start_date.ToShortDateString();
                txtEndDate.Value = BranchList[0].End_date.ToShortDateString();
                txtPFCode.Value = BranchList[0].PFCode;
                txtESICode.Value = BranchList[0].ESICode;
                txtUserID.Value = BranchList[0].UserId;
                txtPassword.Value = BranchList[0].Password;
                txtConfirmpwd.Value = BranchList[0].Password;
                txtBranchCode.Disabled = true;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    } 

    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    protected void btn_update_Click(object sender, EventArgs e)
    {
        company.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);

        if (s_login_role == "a")
        {

            company.BranchCompanyId = (int)Session["ddl_Branch_ID"];

        }
        if (s_login_role == "h")
        {
            company.BranchCompanyId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        }

        company.CompanyCode = txtBranchCode.Value;
        company.CompanyName = txtBranchName.Value;
        company.AddressLine1 = txtAddressLine1.Text;
        company.AddressLine2 = txtAddressLine2.Text;
        company.City = txtCity.Value;
        company.ZipCode = txtZipCode.Value;
        company.CountryName = txtCountry.Value;
        company.StateName = txtState.Value;
        company.PhoneNo = txtPhoneNo.Value;
        company.FaxNo = txtFaxNo.Value;
        company.EmailId = txtEmailId.Value;
        company.Alternate_EmailId = txtAlternateEmailId.Value;
        company.UserId = txtUserID.Value;
        company.Password = txtPassword.Value;
        company.role = 'h';
        company.status = 'Y';
        company.PFno = txtPFCode.Value;
        company.ESIno = txtESICode.Value;
        company.Start_date = employee.Convert_ToSqlDate(txtStartDate.Value);
        company.End_date = employee.Convert_ToSqlDate(txtEndDate.Value);
        company.Branch_Create(company);

        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Branch Updated Successfully');", true);
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        company.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        company.CompanyCode = txtBranchCode.Value;
        company.CompanyName = txtBranchName.Value;
        company.AddressLine1 = txtAddressLine1.Text;
        company.AddressLine2 = txtAddressLine2.Text;
        company.City = txtCity.Value;
        company.ZipCode = txtZipCode.Value;
        company.CountryName = txtCountry.Value;
        company.StateName = txtState.Value;
        company.PhoneNo = txtPhoneNo.Value;
        company.FaxNo = txtFaxNo.Value;
        company.EmailId = txtEmailId.Value;
        company.Alternate_EmailId = txtAlternateEmailId.Value;
        company.UserId = txtUserID.Value;
        company.Password = txtPassword.Value;
        company.role = 'h';
        company.status = 'Y';
        company.PFno = txtPFCode.Value;
        company.ESIno = txtESICode.Value;
        company.Start_date = employee.Convert_ToSqlDate(txtStartDate.Value);
        company.End_date = employee.Convert_ToSqlDate(txtEndDate.Value);
        company.Branch_Create(company);

        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Branch Created Successfully');", true);
        clear();
    }

    protected void btn_reset_Click(object sender, EventArgs e)
    {
        clear();
    }


    public void clear()
    {
        txtBranchCode.Value = "";
        txtBranchName.Value = "";
        txtCity.Value = "";
        txtCountry.Value = "";
        txtEmailId.Value = "";
        txtPhoneNo.Value = "";
        txtFaxNo.Value = "";
        txtZipCode.Value = "";
        txtAlternateEmailId.Value = "";
        txtState.Value = "";
        txtAddressLine1.Text = "";
        txtAddressLine2.Text = "";
        txtPFCode.Value = "";
        txtESICode.Value = "";
        txtStartDate.Value = "";
        txtEndDate.Value = "";
    }

    protected void Button1_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btn_info_Click(object sender, EventArgs e)
    {
        try
        {
            string a_userID;
            a_userID = txtUserID.Value;
            availablityList = company.fn_get_Availablity(a_userID);

            if (availablityList.Count <= 0)
            {
                ClientScriptManager manager = Page.ClientScript;
                manager.RegisterStartupScript(this.GetType(), "Call", "show_message('UserID is Available');", true);
            }
            else
            {
                ClientScriptManager manager = Page.ClientScript;
                manager.RegisterStartupScript(this.GetType(), "Call", "show_message('UserID Already Exist');", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }
}
