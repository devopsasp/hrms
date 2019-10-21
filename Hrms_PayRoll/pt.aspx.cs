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
using System.Data.SqlClient;

public partial class Bank_Loan_Default : System.Web.UI.Page
{   
    Company company = new Company();
    PayRoll pay = new PayRoll();
    Employee employee = new Employee();
    Collection<Company> CompanyList, ddlBranchsList;
    Collection<PayRoll> ptlist;
    Collection<Employee> Branchlist;
    DataSet ds_userrights;

    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);

    string _Value, s_form;
    string s_login_role;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        

        if (!IsPostBack)
        {
            CompanyList = company.fn_getCompany();
            if (CompanyList.Count > 0)
            {

                switch (s_login_role)
                {
                    case "a":
                        ddl_Branch.Visible = true;
                        ddl_Branch_load();
                        break;

                    case "h":
                        ddl_Branch.Visible = false;
                        load();
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

    public void load()
    {
        if (s_login_role == "a")
        {
            pay.BranchId = (int)ViewState["PT_BranchID"];
        }
        ptlist = pay.fn_pay_pt(pay);
        if (ptlist.Count > 0)
        {
            grid_pt.DataSource = ptlist;
            grid_pt.DataBind();
        }
        Branchlist = employee.fn_getPlace(employee);
        if (Branchlist.Count > 0)
        {
            lbl_branch.Text = "* for the State of " + Branchlist[0].State;
        }
    }

    protected void edit(object sender, GridViewEditEventArgs e)
    {
        double mtax = 0.0;
        if (s_login_role == "a")
        {
            pay.BranchId = (int)ViewState["PT_BranchID"];
        }
        pay.PTcount = Convert.ToInt32(grid_pt.DataKeys[e.NewEditIndex].Value);
        pay.F_Amount = Convert.ToDouble(((TextBox)grid_pt.Rows[e.NewEditIndex].FindControl("grd_fromamount")).Text);
        pay.T_Amount = Convert.ToDouble(((TextBox)grid_pt.Rows[e.NewEditIndex].FindControl("grd_toamount")).Text);
        pay.ProTaxAmt = Convert.ToDouble(((TextBox)grid_pt.Rows[e.NewEditIndex].FindControl("grd_taxamount")).Text);
        mtax = pay.ProTaxAmt / 6;
        pay.half_monthly = Math.Round(mtax,2);
        pay.Quaterly = pay.ProTaxAmt / 2;
        pay.Annual = pay.ProTaxAmt * 2;
        //pay.Date = Convert.ToDateTime(((TextBox)grid_pt.Rows[e.NewEditIndex].FindControl("grd_date")).Text);
        _Value = pay.pay_pt(pay);
        
        ((ImageButton)grid_pt.Rows[e.NewEditIndex].FindControl("img_update")).Visible = true;
        ((ImageButton)grid_pt.Rows[e.NewEditIndex].FindControl("img_save")).Visible = false;
        ((TextBox)grid_pt.Rows[e.NewEditIndex].FindControl("grd_fromamount")).Enabled = false;
        ((TextBox)grid_pt.Rows[e.NewEditIndex].FindControl("grd_toamount")).Enabled = false;
        ((TextBox)grid_pt.Rows[e.NewEditIndex].FindControl("grd_taxamount")).Enabled = false;
        if (_Value != "1")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Updated Successfully');", true);
            load();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error occured');", true);
        }

    }

    protected void update(object sender, GridViewUpdateEventArgs e)
    {
        ((TextBox)grid_pt.Rows[e.RowIndex].FindControl("grd_fromamount")).Enabled = true;
        ((TextBox)grid_pt.Rows[e.RowIndex].FindControl("grd_toamount")).Enabled = true;
        ((TextBox)grid_pt.Rows[e.RowIndex].FindControl("grd_taxamount")).Enabled = true;
        ((ImageButton)grid_pt.Rows[e.RowIndex].FindControl("img_save")).Visible = true;
        ((ImageButton)grid_pt.Rows[e.RowIndex].FindControl("img_update")).Visible = false;
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
            ViewState["PT_BranchID"] = Convert.ToInt32(ddl_Branch.SelectedValue);
            load();
            grid_pt.Visible = true;
        }
        else
        {
            grid_pt.Visible = false;
        }
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        double mtax = 0.0;
        if (txt_fromamt.Value != "" && txt_toamt.Value != "" && txt_date.Text != "" && txt_ptaxamount.Value != "")
        {
            con.Open();
            if (s_login_role == "a")
            {
                pay.BranchId = (int)ViewState["PT_BranchID"];
            }
            pay.PTcount = 0;
            pay.Date = pay.Convert_ToSqlDate(txt_date.Text); //Convert.ToDateTime(txt_date.Value);
            pay.F_Amount = Convert.ToDouble(txt_fromamt.Value);
            pay.T_Amount = Convert.ToDouble(txt_toamt.Value);
            pay.ProTaxAmt = Convert.ToDouble(txt_ptaxamount.Value);
            mtax = pay.ProTaxAmt / 6;
            pay.Quaterly = pay.ProTaxAmt / 2;
            pay.Annual = pay.ProTaxAmt * 2;
            pay.half_monthly = Math.Round(mtax, 2);

            ptlist = pay.fn_In_PT(pay);
            if (ptlist.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('The entered values are within the below slab');", true);
                return;
            }
            _Value = pay.pay_pt(pay);


            if (_Value != "1")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Added Successfully');", true);
                txt_date.Text = "";
                txt_fromamt.Value = "";
                txt_toamt.Value = "";
                txt_ptaxamount.Value = "";

                load();

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter all the details');", true);
        }
        con.Close();
    }
    protected void btn_reset_Click(object sender, EventArgs e)
    {
        txt_date.Text = "";
        txt_fromamt.Value = "";
        txt_toamt.Value = "";
        txt_ptaxamount.Value = "";
    }
}
