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
    Collection<PayRoll> PFList;
    Collection<Company> CompanyList, ddlBranchsList;

    PayRoll pay = new PayRoll();
    Company company = new Company();

    DataSet ds_userrights;

    string _Value, str_date = "", month = "", year = "", s_form;
    int cur_yr, yr_it, ddl_ex, grd;

    string s_login_role;
  
    protected void Page_Load(object sender, EventArgs e)
    {
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;

        
        lbl_error.Text = "";

        if(!IsPostBack)
        {
            ddl_month.SelectedValue = DateTime.Now.Month.ToString();
            ddl_year.SelectedValue = DateTime.Now.Year.ToString();
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
                        ddl_year_load();
                        //load();
                        //grid_ddl_rdo_load();
                        break;

                    case "h":
                        ddl_year_load();
                        ddl_Branch.Visible = false;
                        load();
                        
                        break;

                    case "u":
                        s_form = "7";

                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            ddl_year_load();
                            ddl_Branch.Visible = false;
                            load();
                        }
                        else
                        {
                            Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator.";
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

    public void load()
    {
        if (chk_ceiling.Checked == false)
        {
            txt_ceiling.Disabled = true;
            txt_amount.Disabled = true;
        }
        else
        {
            txt_ceiling.Disabled = false;
            txt_amount.Disabled = false;
        }

        if (s_login_role == "a")
        {
            pay.BranchId = (int)ViewState["PF_BranchID"];
        }
        PFList = pay.fn_pay_pf(pay);
        if (PFList[0].Count == 1)
        {
            ddl_month.SelectedValue = PFList[0].PTmonth;
            ddl_year.SelectedValue = PFList[0].Year.ToString();
            txt_pf.Value = PFList[0].Emp_Con_PF.ToString("#0.00");
            txt_EPF.Value = PFList[0].Emp_Con_EPF.ToString("#0.00");
            txt_FPF.Value = PFList[0].Emp_Con_FPF.ToString("#0.00");
            txt_admincharge.Value = PFList[0].AdminCharges.ToString("#0.00");
            if (PFList[0].maxceiling.ToString() == "Y")
            {
                chk_ceiling.Checked = true;
                txt_ceiling.Disabled = false;
                txt_amount.Disabled = false;
            }
            else
            {
                chk_ceiling.Checked = false;
            }
            if (PFList[0].regular == 'Y')
            {
                chk_allowance.Checked = true;
            }
            else
            {
                chk_allowance.Checked = false;
            }
            txt_ceiling.Value = PFList[0].maxamount.ToString();
            txt_amount.Value = PFList[0].EligibilityAmt.ToString();
            if (PFList[0].c_Round == '0')
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
        
    }

    public void ddl_year_load()
    {
        try
        {
            cur_yr = DateTime.Now.Year;
            cur_yr = cur_yr + 5;
            for (yr_it = 2005; yr_it <= cur_yr; yr_it++)
            {
                ddl_year.Items.Add(Convert.ToString(yr_it));
            }
        }
        catch (Exception ex)
        {
            lbl_error.Text = "Error";
        }
    }

    
    
    public void ddl_Branch_load()
    {
        int ddl_i;
        //branch dropdown
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
            ViewState["PF_BranchID"] = Convert.ToInt32(ddl_Branch.SelectedValue);
            load();
        }
        else
        {

        }
    }

    protected void chk_ceiling_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_ceiling.Checked == false)
        {
            txt_ceiling.Disabled = true;
            txt_amount.Disabled = true;
        }
        else
        {
            txt_ceiling.Disabled = false;
            txt_amount.Disabled = false;
        }
    }

    protected void Img_Save_Click(object sender, ImageClickEventArgs e)
    {
        
    }
    protected void Img__Reset_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (s_login_role == "a")
        {
            pay.BranchId = (int)ViewState["PF_BranchID"];
        }

        pay.Emp_Con_PF = Convert.ToDouble(txt_pf.Value);
        pay.Emp_Con_EPF = Convert.ToDouble(txt_EPF.Value);
        pay.Emp_Con_FPF = Convert.ToDouble(txt_FPF.Value);
        pay.AdminCharges = Convert.ToDouble(txt_admincharge.Value);
        pay.upperlimit = Convert.ToDouble(txt_ceiling.Value);
        pay.c_Round = Convert.ToChar(rdo_round.SelectedItem.Value);
        pay.EligibilityAmt = Convert.ToDouble(txt_amount.Value);
        pay.PTmonth = ddl_month.SelectedItem.Text;
        pay.Year = Convert.ToInt32(ddl_year.SelectedItem.Text);

        if (chk_ceiling.Checked == true)
        {
            pay.maxceiling = "Y";
        }
        else
        {
            pay.maxceiling = "N";
        }
        if (chk_allowance.Checked == true)
        {
            pay.regular = 'Y';
        }
        else
        {
            pay.regular = 'N';
        }

        switch (rdo_round.SelectedItem.Value)
        {
            case "0": pay.c_Round = '0';
                break;

            case "1": pay.c_Round = '1';
                break;

            default: pay.c_Round = '0';
                break;
        }

        str_date = "01/" + ddl_month.SelectedItem.Value + "/" + ddl_year.SelectedItem.Value;
        pay.Date = pay.Convert_ToSqlDate(str_date); //Convert.ToDateTime(str_date);
        _Value = pay.pay_pf(pay);
        if (_Value != "1")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Saved Successfully');", true);
            load();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }
    protected void btn_reset_Click(object sender, EventArgs e)
    {
        txt_admincharge.Value = "";
        txt_amount.Value = "";
        txt_EPF.Value = "";
        txt_FPF.Value = "";
        txt_pf.Value = "";
        txt_ceiling.Value = "";
        chk_ceiling.Checked = false;
        chk_allowance.Checked = false;
        ddl_month.SelectedValue = DateTime.Now.ToString("MMMMMMMMMM");
        ddl_year.SelectedValue = DateTime.Now.Year.ToString();
    }
}
