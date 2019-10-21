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
using System.Collections.Generic;
using System.Collections.ObjectModel;


public partial class Hrms_Company_Default2 : System.Web.UI.Page
{

    Be_Recruitment r = new Be_Recruitment();

    private SqlConnection _Connection;

    ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();

    Company company = new Company();

    Collection<Company> ddlBranchsList;
    Collection<Company> BranchList;
    Collection<Company> BanchCodeList;

    Collection<Company> CompanyList;

    Collection<Company> availablityList;

    string _Code;
    string s_login_role;
    int companyid, branchid, ddl_i,admin_new;
    string s_form = "";
    DataSet ds_userrights;




    protected void Page_Load(object sender, EventArgs e)
    {


        try
        {
            

            s_login_role = Request.Cookies["Login_temp_Role"].Value;
            //s_login_role = 'a';

            if (!IsPostBack)
            {
           
            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {


                switch (s_login_role)
                {

                    case "a":
                        tbl_branch.Visible = false;
                        Branch_Selection.Visible = true;
                        //Panel1.Visible = false;
                        //tab_Preview.Visible = false;
                        //btn_update.Visible = false;
                        //ddlBranch.Visible = false;
                        //lbl_branch.Visible = false;
                        admin();
                        break;

                    case "h": tbl_branch.Visible = true;
                        Branch_Selection.Visible = false;
                        hr();
                        break;                   

                    case "e":
                        btn_update.Visible = false;
                        hr();
                        Branch_Selection.Visible = false;
                        break;

                    case "u":
                        s_form = "2";

                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            //tab_Preview.Visible = false;
                            btn_update.Visible = false;
                            ddlBranch.Visible = false;
                            admin();
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
            else
            {
                Response.Cookies["Msg_Session"].Value = "Create Company";
                Response.Redirect("Company_Home.aspx");
            }


            }

        }
        catch (Exception ex)
        {
            Response.Cookies["Msg_Session"].Value=  "Error Occurred";

            Response.Redirect("Company_Home.aspx");


        }
   


    }


    public void admin()
    {

        try
        {

            ddlBranchsList = company.fn_getBranchs();

                if (ddlBranchsList.Count > 1)    //first branch is company
                {

                    ddlBranch.Visible = true;

                    for (ddl_i = 0; ddl_i < ddlBranchsList.Count; ddl_i++)
                    {
                        if (ddl_i == 0)
                        {
                            ListItem list = new ListItem();

                            list.Text = "Select Branch";
                            list.Value = "0";
                            ddlBranch.Items.Add(list);


                        }
                        else
                        {

                            ListItem list = new ListItem();

                            list.Text = ddlBranchsList[ddl_i].CompanyName;
                            list.Value = ddlBranchsList[ddl_i].CompanyId.ToString();
                            ddlBranch.Items.Add(list);

                        }

                    }

                    

                }
                else
                {
                    lbl_Error.Text = "No Branch Created";
                    //lbl_branch.Text = "No Branch Created";
                }
               
                


        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";


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
                
                //Panel1.Visible = true;
                lbl_branchcode.Text = BranchList[0].CompanyCode;
                lbl_branchname.Text = BranchList[0].CompanyName;
                lbl_Address1.Text = BranchList[0].AddressLine1;
                lbl_Address2.Text = BranchList[0].AddressLine2;
                lbl_city.Text = BranchList[0].City;
                lbl_zip.Text = BranchList[0].ZipCode;
                lbl_country.Text = BranchList[0].CountryName;
                lbl_state.Text = BranchList[0].StateName;
                lbl_phone.Text = BranchList[0].PhoneNo;
                lbl_fax.Text = BranchList[0].FaxNo;
                lbl_email.Text = BranchList[0].EmailId;
                lbl_altemail.Text = BranchList[0].Alternate_EmailId;
                lbl_pfcode.Text = BranchList[0].PFCode;
                lbl_esicode.Text = BranchList[0].ESICode;
                lbl_startdate.Text = BranchList[0].Start_date.ToString("dd/MM/yyyy");
                lbl_enddate.Text = BranchList[0].End_date.ToString("dd/MM/yyyy");

            }

            else
            {
                ;

            }

        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";


        }

    }



    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {

            if (ddlBranch.SelectedItem.Value != "0")
            {

                int ddl_id = Convert.ToInt32(ddlBranch.SelectedItem.Value);
                Session["ddl_Branch_ID"] = ddl_id;


                BranchList = company.fn_getBranchCompany(ddl_id);

                if (BranchList.Count > 0)
                {
                    tbl_branch.Visible = true;
                    
                    lbl_branchcode.Text = BranchList[0].CompanyCode;
                    lbl_branchname.Text = BranchList[0].CompanyName;
                    lbl_Address1.Text = BranchList[0].AddressLine1;
                    lbl_Address2.Text = BranchList[0].AddressLine2;
                    lbl_city.Text = BranchList[0].City;
                    lbl_zip.Text = BranchList[0].ZipCode;
                    lbl_country.Text = BranchList[0].CountryName;
                    lbl_state.Text = BranchList[0].StateName;
                    lbl_phone.Text = BranchList[0].PhoneNo;
                    lbl_fax.Text = BranchList[0].FaxNo;
                    lbl_email.Text = BranchList[0].EmailId;
                    lbl_altemail.Text = BranchList[0].Alternate_EmailId;
                    lbl_pfcode.Text = BranchList[0].PFCode;
                    lbl_esicode.Text = BranchList[0].ESICode;
                    lbl_startdate.Text = BranchList[0].Start_date.ToString("dd/MM/yyyy");
                    lbl_enddate.Text = BranchList[0].End_date.ToString("dd/MM/yyyy");
                    //tab_Preview.Visible = true;

                    //tab_Preview.Rows[1].Cells[1].InnerText = BranchList[0].CompanyCode;
                    //tab_Preview.Rows[1].Cells[3].InnerText = BranchList[0].CompanyName;
                    //tab_Preview.Rows[2].Cells[1].InnerText = BranchList[0].AddressLine1;
                    //tab_Preview.Rows[2].Cells[3].InnerText = BranchList[0].AddressLine2;
                    //tab_Preview.Rows[3].Cells[1].InnerText = BranchList[0].City;
                    //tab_Preview.Rows[3].Cells[3].InnerText = BranchList[0].ZipCode;
                    //tab_Preview.Rows[4].Cells[1].InnerText = BranchList[0].CountryName;
                    //tab_Preview.Rows[4].Cells[3].InnerText = BranchList[0].StateName;
                    //tab_Preview.Rows[5].Cells[1].InnerText = BranchList[0].PhoneNo;
                    //tab_Preview.Rows[5].Cells[3].InnerText = BranchList[0].FaxNo;
                    //tab_Preview.Rows[6].Cells[1].InnerText = BranchList[0].EmailId;
                    //tab_Preview.Rows[6].Cells[3].InnerText = BranchList[0].Alternate_EmailId;

                    btn_update.Visible = true;

                }

            }
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




    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            admin_new = 1;
            Session["admin_new"] = admin_new;
            Response.Redirect("NewBranch.aspx");

        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";


        }
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {
        try
        {
            btn_update.Visible = false;
            admin_new = 2;
            Session["admin_new"] = admin_new;
            Response.Redirect("NewBranch.aspx");

        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";


        }

    }
}
