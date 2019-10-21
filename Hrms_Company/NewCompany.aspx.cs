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
    ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();

    Company company = new Company();
    Employee employee = new Employee();

    Collection<Company> CompanyList;
    Collection<Company> CompanyCodeList;
    Collection<Company> BranchList;
    Collection<Company> BranchIdList;

    string _Code, _Value, _BCode;
    string s_login_role;

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            

            if (!IsPostBack)
            {
                CompanyList = company.fn_getCompany();

                if (CompanyList.Count > 0)
                {

                    txtCompanyCode.Value = CompanyList[0].CompanyCode;
                    txtCompanyName.Value = CompanyList[0].CompanyName;
                    txtAddressLine1.Value = CompanyList[0].AddressLine1;
                    txtAddressLine2.Value = CompanyList[0].AddressLine2;
                    txtCity.Value = CompanyList[0].City;
                    txtZipCode.Value = CompanyList[0].ZipCode;
                    txtCountry.Value = CompanyList[0].CountryName;
                    txtState.Value = CompanyList[0].StateName;
                    txtPhoneNo.Value = CompanyList[0].PhoneNo;
                    txtFaxNo.Value = CompanyList[0].FaxNo;
                    txtEmailId.Value = CompanyList[0].EmailId;
                    txtAlternateEmailId.Value = CompanyList[0].Alternate_EmailId;
                    txtPFno.Value = CompanyList[0].PFCode;
                    txtESIno.Value = CompanyList[0].ESICode;
                    txtstartdate.Value = CompanyList[0].Start_date.ToShortDateString();
                    txtenddate.Value = CompanyList[0].End_date.ToShortDateString();

                    BranchList = company.fn_getBranchCompany(1);

                    if (BranchList.Count > 0)
                    {
                        txtHeadCode.Value = BranchList[0].CompanyCode;
                        txtHeadName.Value = BranchList[0].CompanyName;
                    }
                    txtCompanyCode.Disabled = true;
                    txtHeadCode.Disabled = true;
                    btncompany.Visible = false;
                    Img2.Visible = true;

                }
                else
                {
                    Img2.Visible = false;

                }
            }
        }
        catch (Exception ex)
        {
            Response.Cookies["Msg_Session"].Value=  "Error Occurred";

            Response.Redirect("Company_Home.aspx");


        }

            if (IsPostBack)
            {
                try
                {
                    if (ToolBarCode.Value == "1")
                    {
                        _Code = txtCompanyCode.Value;
                        _BCode = txtHeadCode.Value;

                        CompanyCodeList = company.fn_getCompanycode(_Code);

                        if (CompanyCodeList.Count <= 0)
                        {
  //Create Company
                            company.CompanyId = Convert.ToInt32(hCompanyId.Value);
                            company.CompanyCode = txtCompanyCode.Value;
                            company.CompanyName = txtCompanyName.Value;
                            company.AddressLine1 = txtAddressLine1.Value;
                            company.AddressLine2 = txtAddressLine2.Value;
                            company.City = txtCity.Value;
                            company.ZipCode = txtZipCode.Value;
                            company.CountryName = txtCountry.Value;
                            company.StateName = txtState.Value;
                            company.PhoneNo = txtPhoneNo.Value;
                            company.FaxNo = txtFaxNo.Value;
                            company.EmailId = txtEmailId.Value;
                            company.Alternate_EmailId = txtAlternateEmailId.Value;
                            company.temp1 = txtHeadCode.Value;
                            company.temp2 = txtHeadName.Value;
                            company.PFCode = txtPFno.Value;
                            company.ESICode = txtESIno.Value;
                            company.Start_date = Convert.ToDateTime(txtstartdate.Value);
                            company.End_date = Convert.ToDateTime(txtenddate.Value);
                            company.role = 'a';
                            company.status = 'Y';

                            company.UserId = Request.Cookies["Login_UserID"].Value;
                            company.Password = (string)Session["Login_Password"];

                            company.Company_Create(company);

//************************************Auto create masters**************************************

  //Create First Masters   
                            CompanyCodeList = company.fn_getCompanycode(_Code);
          
                            if (CompanyCodeList.Count > 0)
                            {
                                employee.CompanyId = CompanyCodeList[0].CompanyId;
                                Session["Login_temp_CompanyID"] = CompanyCodeList[0].CompanyId;

                                employee.DepartmentId = 0;
                                employee.DesignationId = 0;
                                employee.DivisionId = 0;
                                employee.GradeId = 0;
                                employee.JobStatusId = 0;
                                employee.LevelId = 0;
                                employee.ShiftId = 0;
                                employee.CategoryId = 0;                               
                                employee.ProjectsiteId = 0;

                                employee.DepartmentName = "Not Allotted";
                                employee.DesignationName = "Not Allotted";
                                employee.DivisionName = "Not Allotted";
                                employee.GradeName = "Not Allotted";
                                employee.JobStatusName = "Not Allotted";
                                employee.LevelName = "Not Allotted";

                                employee.ShiftName = "Not Allotted";
                                employee.ShiftFrom = "";
                                employee.ShiftTo = "";

                                employee.CategoryName = "Not Allotted";
                                employee.ProjectsiteName = "Not Allotted";

                                _Value = employee.DepartmentUpdate(employee);
                                _Value = employee.DesignationUpdate(employee);
                                _Value = employee.DivisionUpdate(employee);
                                _Value = employee.GradeUpdate(employee);
                                _Value = employee.JobStatusUpdate(employee);
                                _Value = employee.LevelUpdate(employee);
                                _Value = employee.ShiftUpdate(employee);
                                _Value = employee.CategoryUpdate(employee);
                                _Value = employee.projectsiteUpdate(employee);
                            }
 //Asssign First Masters   
                            BranchIdList = company.fn_getBranchcode(_BCode);
                            if (BranchIdList.Count > 0)
                            {
                                //employee.CompanyId = 
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

//**************************************************************************************************************

                            //ClientScriptManager manager = Page.ClientScript;
                            //manager.RegisterStartupScript(this.GetType(), "Call", "show_message('Company created Sucessfully');", true);

                            lbl_Error.Text = "Company created Sucessfully";

                            btncompany.Visible = false;
                            Img2.Visible = true;
                            //Response.Redirect("NewCompany.aspx");

                        }
                        else
                        {
                            //error message

                            ClientScriptManager manager = Page.ClientScript;
                            manager.RegisterStartupScript(this.GetType(), "Call", "show_message('Company Code already exists');", true);
                        }
                    }

                    if (ToolBarCode.Value == "2")
                    {

                        //Update Company

                        company.CompanyId = Convert.ToInt32(hCompanyId.Value);
                        company.CompanyCode = txtCompanyCode.Value;
                        company.CompanyName = txtCompanyName.Value;
                        company.AddressLine1 = txtAddressLine1.Value;
                        company.AddressLine2 = txtAddressLine2.Value;
                        company.City = txtCity.Value;
                        company.ZipCode = txtZipCode.Value;
                        company.CountryName = txtCountry.Value;
                        company.StateName = txtState.Value;
                        company.PhoneNo = txtPhoneNo.Value;
                        company.FaxNo = txtFaxNo.Value;
                        company.EmailId = txtEmailId.Value;
                        company.Alternate_EmailId = txtAlternateEmailId.Value;
                        company.temp1 = txtHeadCode.Value;
                        company.temp2 = txtHeadName.Value;
                        company.role = 'a';
                        company.status = 'Y';

                        company.UserId = Request.Cookies["Login_UserID"].Value;
                        company.Password = (string)Session["Login_Password"];

                        //company.UserId = txtUserId.Value;
                        //company.Password = txtPassword.Text;
                        company.Company_Create(company);

                        //ClientScriptManager manager = Page.ClientScript;
                        //manager.RegisterStartupScript(this.GetType(), "Call", "show_message('Company created Sucessfully');", true);
                        //Response.Redirect("NewCompany.aspx");

                        lbl_Error.Text = "Company Updated Sucessfully";
                    }
                    ToolBarCode.Value = "0";
                }
                catch (Exception ex)
                {
                    lbl_Error.Text = "Error";
                }
            }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        ////check company code
        //_Code = CompanyCode.Value;
        //Collection<Company> CompanyList = company.fn_getCompanycode(_Code);
        //if (CompanyList.Count <= 0)
        //{
        //    //save
        //    company.CompanyCode = CompanyCode.Value;
        //    company.CompanyName = CompanyName.Value;
        //    company.AddressLine1 = AddressLine1.Value;
        //    company.AddressLine2 = AddressLine2.Value;
        //    company.City = City.Value;
        //    company.ZipCode = ZipCode.Value;
        //    company.CountryName = txtCountry.Value;
        //    company.StateName = txtState.Value;
        //    company.PhoneNo = PhoneNo.Value;
        //    company.FaxNo = FaxNo.Value;
        //    company.EmailId = EmailId.Value;
        //    company.Alternate_EmailId = AlternateEmailId.Value;
        //    //company.UserId = UserId.Value;
        //    //company.Password = Password.Text;
        //    company.Update(company);
        //    ClientScriptManager manager = Page.ClientScript;
        //    manager.RegisterStartupScript(this.GetType(), "Call", "show_message('Company Sucessfully Saved');", true);
        //}
        //else
        //{
        //    //error message
        //    ClientScriptManager manager = Page.ClientScript;
        //    manager.RegisterStartupScript(this.GetType(), "Call", "show_message('Company Code Already exist');", true);
        //}
    }

    protected void Back_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //Response.Redirect("../Login.aspx");
            Response.Redirect("PreviewCompany.aspx");
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }
}