using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.ObjectModel;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ePayHrms.Company;
using ePayHrms.Employee;

public partial class Bank_Loan_Default : System.Web.UI.Page
{
    Company company = new Company();
    PayRoll Esi = new PayRoll();
    Collection<PayRoll> esilist;
    Collection<Company> CompanyList, ddlBranchsList;

    DataSet ds_userrights;

    string _Value, str_date = "", month = "", year = "", s_form;
    int cur_yr, yr_it, ddl_ex, grd;

    string s_login_role;

    protected void Page_Load(object sender, EventArgs e)
    {
        Esi.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        Esi.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
       


        if (!IsPostBack)
        {
            ddl_year_load(ddl_year);
            if (DateTime.Now.Month.ToString().Length == 1)
            {
                ddl_month.SelectedValue = "0" + DateTime.Now.Month.ToString();
            }
            else
            {
                ddl_month.SelectedValue = DateTime.Now.Month.ToString();
            }
           
            //load();
            //grid_ddl_rdo_load();
            CompanyList = company.fn_getCompany();
            if (CompanyList.Count > 0)
            {

                switch (s_login_role)
                {
                    case "a":

                       
                        ddl_Branch.Visible = true;
                        ddl_Branch_load();
                        //load();
                        //grid_ddl_rdo_load();
                        break;

                    case "h":
                        ddl_Branch.Visible = false;
                        load();
                        //grid_ddl_rdo_load();
                        break;

                    case "u":
                        s_form = "9";

                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            ddl_Branch.Visible = false;
                            load();
                        }
                        else
                        {
                            Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator.";
                            Response.Redirect("~/Company_Home.aspx");
                        }
                        //hr();
                        //session_check();
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

    protected void btn_Add_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                Esi.BranchId = (int)ViewState["Esi_BranchID"];
            }
            Esi.Emp_Con = Convert.ToDouble(txtEmployee_con.Value);
            Esi.Employer_Con = Convert.ToDouble(txtEloyer_con.Value);
            Esi.AdminCharges = Convert.ToDouble(txtAdmin_Charges.Value);
            Esi.EligibilityAmt = Convert.ToInt32(txtEligibility_Amt.Value);

            str_date = "01/" + ddl_month.SelectedItem.Value + "/" + ddl_year.SelectedItem.Value;
            Esi.c_Round = Convert.ToChar(rdo_round.SelectedItem.Value);


            switch (rdo_round.SelectedItem.Value)
            {
                case "0": Esi.c_Round = '0';
                    break;

                case "1": Esi.c_Round = '1';
                    break;

                default: Esi.c_Round = '0';
                    break;

            }
            Esi.d_date = Esi.Convert_ToSqlDate(str_date); //Convert.ToDateTime(str_date);
            _Value = Esi.ESI(Esi);

            if (_Value != "1")
            {
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Added Successfully');", true);
                ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Added Successfully');", true);
                load();
                //grid_ddl_rdo_load();
            }
            else
            {
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
                ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Error Occured');", true);
            }

        }
        catch (Exception ex)
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
            ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }
    public void load()
    {
        if (s_login_role == "a")
        {
            Esi.BranchId = (int)ViewState["Esi_BranchID"];
        }
        esilist = Esi.fn_pay_esi(Esi);
        if (esilist[0].Count == 1)
        {
            ddl_month.SelectedValue = esilist[0].Month.ToString();
            //ddl_year.SelectedItem.Text = esilist[0].Year.ToString();
            txtAdmin_Charges.Value = esilist[0].AdminCharges.ToString("#0.00");
            txtEligibility_Amt.Value = esilist[0].EligibilityAmt.ToString("#0.00");
            txtEmployee_con.Value = esilist[0].Emp_Con.ToString("#0.00");
            txtEloyer_con.Value = esilist[0].Employer_Con.ToString("#0.00");
            if (esilist[0].Round == '0')
            {
                rdo_round.SelectedValue = "0";
            }
            else
            {
                rdo_round.SelectedValue = "1";
            }
        }
        else
        {
            ddl_month.SelectedItem.Text = DateTime.Now.ToString("MMMM");
            ddl_year.SelectedItem.Text = DateTime.Now.ToString("yyyy");
        }
        //ESIlist = Esi.fn_ESI(Esi);
        //grid_esi.DataSource = ESIlist;
        //grid_esi.DataBind();
        //grid_ddl_rdo_load();

    }

   

    public void ddl_year_load(DropDownList ddl)
    {
        try
        {
            cur_yr = DateTime.Now.Year;

            cur_yr = cur_yr + 5;

            for (yr_it = 2000; yr_it <= cur_yr; yr_it++)
            {

                ddl.Items.Add(Convert.ToString(yr_it));

            }
            ddl_year.SelectedValue = DateTime.Now.Year.ToString();
        }
        catch (Exception ex)
        {
            //lbl_error.Text = "";
        }
    }

    //public void grid_ddl_rdo_load()
    //{
    //    if (s_login_role == "a")
    //    {
    //        Esi.BranchId = (int)ViewState["Esi_BranchID"];
    //    }

    //    for (grd = 0; grd < grid_esi.Rows.Count; grd++)
    //    {

    //        ddl_year_load(((DropDownList)grid_esi.Rows[grd].FindControl("grd_year")));


    //        //Month
    //        str_date = Convert.ToString(esilist[grd].strDate);

    //        month = str_date.Substring(3, 2);
    //        year = str_date.Substring(6, 4);

    //        for (ddl_ex = 0; ddl_ex < ((DropDownList)grid_esi.Rows[grd].FindControl("grd_month")).Items.Count; ddl_ex++)
    //        {
    //            if (((DropDownList)grid_esi.Rows[grd].FindControl("grd_month")).Items[ddl_ex].Value == month)
    //            {
    //                ((DropDownList)grid_esi.Rows[grd].FindControl("grd_month")).SelectedIndex = ddl_ex;
    //            }

    //        }

    //        //Year

    //        for (ddl_ex = 0; ddl_ex < ((DropDownList)grid_esi.Rows[grd].FindControl("grd_year")).Items.Count; ddl_ex++)
    //        {
    //            if (((DropDownList)grid_esi.Rows[grd].FindControl("grd_year")).Items[ddl_ex].Value == year)
    //            {
    //                ((DropDownList)grid_esi.Rows[grd].FindControl("grd_year")).SelectedIndex = ddl_ex;
    //            }

    //        }

    //        //Round
    //        //        for (ddl_ex = 0; ddl_ex < ((RadioButtonList)grid_esi.Rows[grd].FindControl("grd_round")).Items.Count; ddl_ex++)
    //        //        {
    //        //            if (Convert.ToChar(((RadioButtonList)grid_esi.Rows[grd].FindControl("grd_round")).Items[ddl_ex].Value) == ESIlist[grd].Round)
    //        //            {
    //        //                ((RadioButtonList)grid_esi.Rows[grd].FindControl("grd_round")).SelectedIndex = ddl_ex;
    //        //            }
    //        //        }
    //        //    }
    //        //}

    //        if (esilist[grd].c_Round == 'Y')
    //        {
    //            ((RadioButtonList)grid_esi.Rows[grd].FindControl("grd_round")).SelectedIndex = 0;
    //        }
    //        if (esilist[grd].c_Round == 'N')
    //        {
    //            ((RadioButtonList)grid_esi.Rows[grd].FindControl("grd_round")).SelectedIndex = 1;
    //        }
    //    }
    //}

    public void ddl_Branch_load()
    {
        int ddl_i;
        //branck dropdown
        ddlBranchsList = company.fn_getBranchs();

        if (ddlBranchsList.Count > 0)
        {
            for (ddl_i = -1; ddl_i < ddlBranchsList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem list = new ListItem();
                    list.Text = "Select Branch";
                    list.Value = "0";
                    ddl_Branch.Items.Add(list);
                }
                else
                {
                    ListItem list = new ListItem();
                    list.Text = ddlBranchsList[ddl_i].CompanyName;
                    list.Value = ddlBranchsList[ddl_i].CompanyId.ToString();
                    ddl_Branch.Items.Add(list);
                }
            }
        }
    }

    protected void ddl_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Branch.SelectedValue != "0")
        {
            ViewState["Esi_BranchID"] = Convert.ToInt32(ddl_Branch.SelectedValue);
            load();
            //grid_ddl_rdo_load();
            //grid_esi.Visible = true;
        }
        else
        {
            //grid_esi.Visible = false;
        }
    }
 

    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                Esi.BranchId = (int)ViewState["Esi_BranchID"];
            }
            if (txtAdmin_Charges.Value != "" && txtEligibility_Amt.Value != "" && txtEmployee_con.Value != "" && txtEloyer_con.Value != "")
            {
                Esi.Emp_Con = Convert.ToDouble(txtEmployee_con.Value);
                Esi.Employer_Con = Convert.ToDouble(txtEloyer_con.Value);
                Esi.AdminCharges = Convert.ToDouble(txtAdmin_Charges.Value);
                Esi.EligibilityAmt = Convert.ToDouble(txtEligibility_Amt.Value);

                str_date = "01/" + ddl_month.SelectedItem.Value + "/" + ddl_year.SelectedItem.Value;
                Esi.c_Round = Convert.ToChar(rdo_round.SelectedItem.Value);

                switch (rdo_round.SelectedItem.Value)
                {
                    case "0": Esi.c_Round = '0';
                        break;

                    case "1": Esi.c_Round = '1';
                        break;

                    default: Esi.c_Round = '0';
                        break;

                }
                Esi.d_date = Esi.Convert_ToSqlDate(str_date); //Convert.ToDateTime(str_date);
                _Value = Esi.ESI(Esi);

                if (_Value != "1")
                {
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Saved Succesfully');", true);
                    ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Saved Successfully');", true);
                    load();
                    //grid_ddl_rdo_load();
                }
                else
                {
                   // ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
                    ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Error Occured');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Enter all fields');", true);
            }

        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

    protected void btn_reset_Click(object sender, EventArgs e)
    {
        ddl_month.SelectedValue = DateTime.Now.ToString("MMMM");
        ddl_year.SelectedItem.Text = DateTime.Now.Year.ToString();
        txtAdmin_Charges.Value = "";
        txtEligibility_Amt.Value = "";
        txtEloyer_con.Value = "";
        txtEmployee_con.Value = "";
    }
}

    

