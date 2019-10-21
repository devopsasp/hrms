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
using ePayHrms.Login;
using ePayHrms.BE.Recruitment;
using ePayHrms.Connection;
using ePayHrms.Candidate;
using System.IO;
using ePayHrms.Company;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ePayHrms.Employee;

public partial class Hrms_Company_Default : System.Web.UI.Page
{


    Be_Recruitment r = new Be_Recruitment();
    Company company = new Company();
    Employee employee = new Employee();

    Collection<Company> CompanyList;   
    Collection<Company> BranchList;
  
    string s_login_role;
    string s_form = "";
    DataSet ds_userrights;

  protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            

            employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            s_login_role = Request.Cookies["Login_temp_Role"].Value;
            //s_login_role = 'a';

            if (!IsPostBack)
            {


                switch (s_login_role)
                {

                    case "a": btn_update.Visible = true;
                        admin();
                        break;

                    case "h": btn_update.Visible = false;
                        hr();
                        break;

                    case "e": btn_update.Visible = false;
                        hr();
                        break;

                    case "u": 
                        s_form = "1";

                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            admin();
                            btn_update.Visible = true;
                        }
                        else
                        {
                            btn_update.Visible = false;
                            hr();

                        }
                       
                        break;

                    default: Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                        Response.Redirect("Company_Home.aspx");
                        break;

                }


            }

        }
        catch (Exception ex)
        {
            //Response.Cookies["Msg_Session"].Value=  "Error Occurred";

            //Response.Cookies["Msg_Session"].Value=  ex.Message.ToString();
            Response.Redirect("Company_Home.aspx");


        }

   
    
    }

  public void hr()
    {

        try
        {

            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {

                tab_Preview.Rows[1].Cells[1].InnerText = CompanyList[0].CompanyCode;
                tab_Preview.Rows[1].Cells[3].InnerText = CompanyList[0].CompanyName;

                BranchList = company.fn_getBranchCompany(employee.CompanyId);

                if (BranchList.Count > 0)
                {
                    tab_Preview.Rows[2].Cells[1].InnerText = BranchList[0].CompanyCode;
                    tab_Preview.Rows[2].Cells[3].InnerText = BranchList[0].CompanyName;

                }

                tab_Preview.Rows[3].Cells[1].InnerText = CompanyList[0].AddressLine1;
                tab_Preview.Rows[3].Cells[3].InnerText = CompanyList[0].AddressLine2;

                tab_Preview.Rows[4].Cells[1].InnerText = CompanyList[0].City;
                tab_Preview.Rows[4].Cells[3].InnerText = CompanyList[0].ZipCode;
                tab_Preview.Rows[5].Cells[1].InnerText = CompanyList[0].CountryName;
                tab_Preview.Rows[5].Cells[3].InnerText = CompanyList[0].StateName;

                tab_Preview.Rows[6].Cells[1].InnerText = CompanyList[0].PhoneNo;
                tab_Preview.Rows[6].Cells[3].InnerText = CompanyList[0].FaxNo;
                tab_Preview.Rows[7].Cells[1].InnerText = CompanyList[0].EmailId;
                tab_Preview.Rows[7].Cells[3].InnerText = CompanyList[0].Alternate_EmailId;


            }
            else
            {

                Response.Redirect("Company_Home.aspx");

            }

        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";


        }


    }

  public void admin()
    {

        try
        {

            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {

                tab_Preview.Rows[1].Cells[2].InnerText = CompanyList[0].CompanyCode;
                tab_Preview.Rows[1].Cells[5].InnerText = CompanyList[0].CompanyName;

                BranchList = company.fn_getBranchCompany(employee.CompanyId);

                if (BranchList.Count > 0)
                {
                    tab_Preview.Rows[2].Cells[2].InnerText = BranchList[0].CompanyCode;
                    tab_Preview.Rows[2].Cells[5].InnerText = BranchList[0].CompanyName;                    
                }

                tab_Preview.Rows[3].Cells[2].InnerText = BranchList[0].AddressLine1;
                tab_Preview.Rows[3].Cells[5].InnerText = BranchList[0].AddressLine2;

                tab_Preview.Rows[4].Cells[2].InnerText = BranchList[0].City;
                tab_Preview.Rows[4].Cells[5].InnerText = BranchList[0].ZipCode;
                tab_Preview.Rows[5].Cells[2].InnerText = BranchList[0].CountryName;
                tab_Preview.Rows[5].Cells[5].InnerText = BranchList[0].StateName;

                tab_Preview.Rows[6].Cells[2].InnerText = BranchList[0].PhoneNo;
                tab_Preview.Rows[6].Cells[5].InnerText = BranchList[0].FaxNo;
                tab_Preview.Rows[7].Cells[2].InnerText = BranchList[0].EmailId;
                tab_Preview.Rows[7].Cells[5].InnerText = BranchList[0].Alternate_EmailId;
                tab_Preview.Rows[8].Cells[2].InnerText = BranchList[0].PFCode;
                tab_Preview.Rows[8].Cells[5].InnerText = BranchList[0].ESICode;
                tab_Preview.Rows[9].Cells[2].InnerText = BranchList[0].Start_date.ToShortDateString();
                tab_Preview.Rows[9].Cells[5].InnerText = BranchList[0].End_date.ToShortDateString();



            }
            else
            {

                Response.Redirect("NewCompany.aspx");
                //Response.Redirect("../Hrms_Master/PF.aspx");
            }

        }
        catch (Exception ex)
        {
            //lbl_Error.Text = "Error";
            Response.Cookies["Msg_Session"].Value=  ex.Message.ToString();

        }



    }

  protected void btn_update_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("NewCompany.aspx");

        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";

        }



    }

  protected void btn_Back_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            Response.Redirect("Company_Home.aspx");
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";


        }

    }




}


