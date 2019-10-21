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
    PayRoll Edli = new PayRoll();
    Collection<PayRoll> Edlilist;
    Collection<Company> CompanyList, ddlBranchsList;

    DataSet ds_userrights;

    string _Value, str_date = "", month = "", year = "", s_form;
    int cur_yr, yr_it, ddl_ex, grd;

    string s_login_role;

    protected void Page_Load(object sender, EventArgs e)
    {
        Edli.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        Edli.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;

        

        if (!IsPostBack)
        {
            ddl_year_load(ddl_year);
            //if (DateTime.Now.Month.ToString().Length == 1)
            //{
            //    ddl_month.SelectedValue = "0" + DateTime.Now.Month.ToString();
            //}
            //else
            //{
            //    ddl_month.SelectedValue = DateTime.Now.Month.ToString();
            //}
            //ddl_year.SelectedValue = DateTime.Now.Year.ToString();
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
                Edli.BranchId = (int)ViewState["Edli_BranchID"];
            }
            Edli.Employer_Con = Convert.ToDouble(txtEloyer_con.Value);
            Edli.AdminCharges = Convert.ToDouble(txtAdmin_Charges.Value);
            Edli.EligibilityAmt = Convert.ToInt32(txtEligibility_Amt.Value);

            str_date = "01/" + ddl_month.SelectedItem.Value + "/" + ddl_year.SelectedItem.Value;
            Edli.c_Round = Convert.ToChar(rdo_round.SelectedItem.Value);


            switch (rdo_round.SelectedItem.Value)
            {
                case "0": Edli.c_Round = '0';
                    break;

                case "1": Edli.c_Round = '1';
                    break;

                default: Edli.c_Round = '0';
                    break;

            }
            Edli.d_date = Edli.Convert_ToSqlDate(str_date); //Convert.ToDateTime(str_date);
            _Value = Edli.EDLI(Edli);

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
            Edli.BranchId = (int)ViewState["Edli_BranchID"];
        }
        Edlilist = Edli.fn_pay_EDLI(Edli);
        if (Edlilist[0].Count == 1)
        {
            if (Edlilist[0].eligible == '0')
            {
                rdo_ins.SelectedValue = "0";
                ddl_month.Enabled = true;
                ddl_year.Enabled = true;
                txtAdmin_Charges.Disabled = false;
                txtEligibility_Amt.Disabled = false;
                txtEloyer_con.Disabled = false;
                rdo_round.Enabled = true;
            }
            else
            {
                rdo_ins.SelectedValue = "1";
                ddl_month.Enabled = false;
                ddl_year.Enabled = false;
                txtAdmin_Charges.Disabled = true;
                txtEligibility_Amt.Disabled = true;
                txtEloyer_con.Disabled = true;
                rdo_round.Enabled = false;
            }
            ddl_month.SelectedValue = Edlilist[0].Month.ToString();
            ddl_year.SelectedValue = Edlilist[0].Year.ToString();
            txtAdmin_Charges.Value = Edlilist[0].AdminCharges.ToString("#0.00");
            txtEligibility_Amt.Value = Edlilist[0].EligibilityAmt.ToString("#0.00");
            txtEloyer_con.Value = Edlilist[0].Employer_Con.ToString("#0.00");
            if (Edlilist[0].Round == '0')
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
            //ddl_month.SelectedItem.Text = DateTime.Now.ToString("MMMM");
            //ddl_year.SelectedItem.Text = DateTime.Now.ToString("yyyy");
        }
        

    }

   

    public void ddl_year_load(DropDownList ddl)
    {
        try
        {
            cur_yr = DateTime.Now.Year;

            //cur_yr = cur_yr ;
            for (yr_it = 2000; yr_it <= cur_yr; yr_it++)
            {
                ddl.Items.Add(new ListItem(Convert.ToString(yr_it), Convert.ToString(yr_it)));
            }
        }
        catch (Exception ex)
        {
            //lbl_error.Text = "";
        }
    }

   
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
            ViewState["Edli_BranchID"] = Convert.ToInt32(ddl_Branch.SelectedValue);
            load();

        }
        else
        {

        }
    }
 

    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                Edli.BranchId = (int)ViewState["Edli_BranchID"];
            }
            if (txtAdmin_Charges.Value != "" && txtEligibility_Amt.Value != ""  && txtEloyer_con.Value != "")
            {
                Edli.Employer_Con = Convert.ToDouble(txtEloyer_con.Value);
                Edli.AdminCharges = Convert.ToDouble(txtAdmin_Charges.Value);
                Edli.EligibilityAmt = Convert.ToDouble(txtEligibility_Amt.Value);

                str_date = "01/" + ddl_month.SelectedItem.Value + "/" + ddl_year.SelectedItem.Value;
                Edli.c_Round = Convert.ToChar(rdo_round.SelectedItem.Value);

                switch (rdo_round.SelectedItem.Value)
                {
                    case "0": Edli.c_Round = '0';
                        break;

                    case "1": Edli.c_Round = '1';
                        break;

                    default: Edli.c_Round = '0';
                        break;
                }
                switch (rdo_ins.SelectedItem.Value)
                {
                    case "0": Edli.eligible = '0';
                        break;

                    case "1": Edli.eligible = '1';
                        break;

                    default: Edli.eligible = '0';
                        break;

                }
                Edli.d_date = Edli.Convert_ToSqlDate(str_date); //Convert.ToDateTime(str_date);
                _Value = Edli.EDLI(Edli);

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
        clear();
    }

    public void clear()
    {
        ddl_month.SelectedValue = DateTime.Now.Month.ToString();
        ddl_year.SelectedItem.Text = DateTime.Now.Year.ToString();
        txtAdmin_Charges.Value = "0.00";
        txtEligibility_Amt.Value = "0.00";
        txtEloyer_con.Value = "0.00";
    }
    protected void rdo_ins_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdo_ins.SelectedItem.Text == "Yes")
        {
            ddl_month.Enabled = false;
            ddl_year.Enabled = false;
            txtAdmin_Charges.Disabled = true;
            txtEligibility_Amt.Disabled = true;
            txtEloyer_con.Disabled = true;
            rdo_round.Enabled = false;
            clear();
        }
        else
        {
            ddl_month.Enabled = true;
            ddl_year.Enabled = true;
            txtAdmin_Charges.Disabled = false;
            txtEligibility_Amt.Disabled = false;
            txtEloyer_con.Disabled = false;
            rdo_round.Enabled = true;
        }
    }
}

    

