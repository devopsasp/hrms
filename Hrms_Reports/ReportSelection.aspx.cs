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


public partial class Hrms_Master_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Company company = new Company();
        Employee employee = new Employee();
        Be_Recruitment r = new Be_Recruitment();
        PayRoll pay = new PayRoll();
        Candidate c = new Candidate();

        //Collection<Employee> AllowanceList;
        Collection<Company> CompanyList;

        string s_login_role;
        int ddl_i, grk;
        string _path;
        string s_form = "";
        DataSet ds_userrights;
        
        
        

        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        c.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        s_login_role = Request.Cookies["Login_temp_Role"].Value;

        

        if (!IsPostBack)
        {
            //lblmsg.Text = (string)Session["Login_Name"];
            

            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {

                
                Session["ErrorMsg"] = "";
               

                switch (s_login_role)
                {

                    case "a":
                       

                        break;

                    case "h":
                       
                       
                        break;

                    case "e": Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                        
                        Response.Redirect("~/Company_Home.aspx");
                        break;

                    //case "u":
                    //    hr();
                    //    break;

                    case "u": s_form = "6";

                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            

                        }
                        else
                        {
                            Response.Redirect("~/Company_Home.aspx");

                        }


                        break;


                    default:
                        Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                        Response.Redirect("~/Company_Home.aspx");
                        break;

                }



            }
            else
            {
                Response.Cookies["Msg_Session"].Value = "Create Company";
                Response.Redirect("~/Company_Home.aspx");
            }



        }



    }



    protected void ddl_reportselection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_reportselection.SelectedItem.Value != "0")
        {

            switch (ddl_reportselection.SelectedItem.Value)
            {
                case "1": Session["ses_report"] = "1";
                    break;

                case "2": Session["ses_report"] = "2";
                    break;

                default: 
                     break;

            }


            Response.Redirect("Report.aspx");

        }
        else
        {
            //lblmsg.Text = "Select Report";

        }


    }



}
