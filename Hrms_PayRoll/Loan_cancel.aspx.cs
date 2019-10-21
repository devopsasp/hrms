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
    Employee employee = new Employee();
    PayRoll pay = new PayRoll();

    Collection<PayRoll> banklist;
    Collection<PayRoll> loanlist;
    Collection<Employee> EmployeeList;  
    Collection<Company> CompanyList, ddlBranchsList;

    DataSet ds_userrights;
    string _Value, c_date = "", str_date = "", month = "", year = "", s_form;
    string s_login_role;
    int cur_yr, yr_it, ddl_ex,grd;

    protected void Page_Load(object sender, EventArgs e)
    {
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);

        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        

        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        lbl_Error.Text = "";

        if (!IsPostBack)
        {
            //grid_load();
            ddl_year_load(ddl_year);
            if (DateTime.Now.Month.ToString().Length == 1)
            {
                ddl_month.SelectedValue = "0" + Convert.ToString(DateTime.Now.Month);
            }
            else
            {
                ddl_month.SelectedValue = Convert.ToString(DateTime.Now.Month);
            }
            //ddl_load(ddl_Loancode,ddl_empcode);

            CompanyList = company.fn_getCompany();
            if (CompanyList.Count > 0)
            {
            switch (s_login_role)
            {
                case "a":
                    tab_ddl.Visible = false;
                    ddl_Branch.Visible = true;
                    ddl_Branch_load();
                    break;
                
                case "h":
                    tab_ddl.Visible = true;
                    ddl_Branch.Visible = false;
                    ddl_load(ddl_Loancode, ddl_empcode);
                    break;
                
                case "u":
                    s_form = "49";
                    ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);
                    if (ds_userrights.Tables[0].Rows.Count > 0)
                    {
                        ddl_Branch.Visible = false;
                        load();
                    }
                    else
                    {
                        Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator.";
                        //Response.Redirect("MasterHome.aspx");
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

    public void ddl_load(DropDownList arg_loan,DropDownList arg_emp)
    {
        if (s_login_role == "a")
        {
            employee.BranchId = (int)ViewState["Loancancel_BranchID"];
            pay.BranchId = (int)ViewState["Loancancel_BranchID"];
        }

        loanlist = pay.fn_loan(pay);
        if (loanlist.Count > 0)
        {
            arg_loan.DataSource = loanlist;
            arg_loan.DataTextField = "loanname";
            arg_loan.DataValueField = "loanid";
            arg_loan.DataBind();
        }
        else
        {
            lbl_Error.Text = "No Loans Available";
        }

        EmployeeList = employee.fn_getEmployeeList(employee); //employee.fn_getAllEmployees();
        if (EmployeeList.Count > 0)
        {
            //arg_emp.DataSource = EmployeeList;
            //arg_emp.DataTextField = "LastName";
            //arg_emp.DataValueField = "EmployeeId";
            //arg_emp.DataBind();
            ddl_empcode.Enabled = true;
            for (int ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem list = new ListItem();
                    list.Text = "Select Employee";
                    list.Value = "0";
                    arg_emp.Items.Add(list);
                }
                else
                {
                    ListItem list = new ListItem();
                    list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
                    list.Text = EmployeeList[ddl_i].LastName.ToString();
                    arg_emp.Items.Add(list);
                }
            }
        }
        else
        {
            lbl_Error.Text = "No employees Available";
            ddl_empcode.Enabled = false;
            ddl_empcode.Items.Clear();
        }
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                employee.BranchId = (int)ViewState["Loancancel_BranchID"];
                pay.BranchId = (int)ViewState["Loancancel_BranchID"];
            }

        pay.loanid = Convert.ToInt32(ddl_Loancode.SelectedItem.Value);
        pay.EmployeeId = Convert.ToInt32(ddl_empcode.SelectedItem.Value);
        pay.Date = pay.Convert_ToSqlDate(create_date(ddl_month, ddl_year)); //Convert.ToDateTime(create_date(ddl_month, ddl_year));

        _Value=pay.Loancancel(pay);
        if (_Value != "1")
        {
            lbl_Error.Text = "<font color=Blue>Added Successfully</font>";
            //load();
            grid_load();
            grid_ddl_load();
        }
        else
        {
            lbl_Error.Text = "<font color=Red>Error Occured</font>";
        }
    }
    catch (Exception ex)
    {
        //lbl_Error.Text = "Error";
    }
    }

    protected void edit(object sender, GridViewEditEventArgs e)
    {
        if (s_login_role == "a")
        {
            employee.BranchId = (int)ViewState["Loancancel_BranchID"];
            pay.BranchId = (int)ViewState["Loancancel_BranchID"];
        }
        pay.loanid = Convert.ToInt32(((DropDownList)grid_loan.Rows[e.NewEditIndex].FindControl("grd_Loan")).SelectedItem.Value);
        pay.EmployeeId = Convert.ToInt32(((DropDownList)grid_loan.Rows[e.NewEditIndex].FindControl("grd_Employee")).SelectedItem.Value);
        pay.Date = pay.Convert_ToSqlDate(create_date(((DropDownList)grid_loan.Rows[e.NewEditIndex].FindControl("grd_Month")), ((DropDownList)grid_loan.Rows[e.NewEditIndex].FindControl("grd_Year"))));
        //pay.Date = Convert.ToDateTime(create_date(((DropDownList)grid_loan.Rows[e.NewEditIndex].FindControl("grd_Month")), ((DropDownList)grid_loan.Rows[e.NewEditIndex].FindControl("grd_Year"))));
        _Value=pay.Loancancel(pay);
        //l.swupdate(l);

        if (_Value != "1")
        {
            lbl_Error.Text = "<font color=Blue>Updated Successfully</font>";
            grid_load();
            grid_ddl_load();
        }
        else
        {
            lbl_Error.Text = "<font color=Red>Error Occured</font>";
        }

        ((DropDownList)grid_loan.Rows[e.NewEditIndex].FindControl("grd_Loan")).Enabled = false;
        ((DropDownList)grid_loan.Rows[e.NewEditIndex].FindControl("grd_Employee")).Enabled = false;
        ((DropDownList)grid_loan.Rows[e.NewEditIndex].FindControl("grd_Year")).Enabled = false;
        ((DropDownList)grid_loan.Rows[e.NewEditIndex].FindControl("grd_Month")).Enabled = false;

        ((LinkButton)grid_loan.Rows[e.NewEditIndex].FindControl("img_update")).Visible = true;
        ((LinkButton)grid_loan.Rows[e.NewEditIndex].FindControl("img_save")).Visible = false;
    }

    protected void update(object sender, GridViewUpdateEventArgs e)
    {
        ((DropDownList)grid_loan.Rows[e.RowIndex].FindControl("grd_Loan")).Enabled = true;
        ((DropDownList)grid_loan.Rows[e.RowIndex].FindControl("grd_Employee")).Enabled = true;
        ((DropDownList)grid_loan.Rows[e.RowIndex].FindControl("grd_Year")).Enabled = true;
        ((DropDownList)grid_loan.Rows[e.RowIndex].FindControl("grd_Month")).Enabled = true;

        ((LinkButton)grid_loan.Rows[e.RowIndex].FindControl("img_save")).Visible = true;
        ((LinkButton)grid_loan.Rows[e.RowIndex].FindControl("img_update")).Visible = false;
    }

    protected void btn_details_Click(object sender, EventArgs e)
    {
        if (s_login_role == "a")
        {
            employee.BranchId = (int)ViewState["Loancancel_BranchID"];
            pay.BranchId = (int)ViewState["Loancancel_BranchID"];
        }
        pay.Date = pay.Convert_ToSqlDate(create_date(ddl_month, ddl_year)); //Convert.ToDateTime(create_date(ddl_month, ddl_year));
        grid_load();
    }
    public void load()
    {
        banklist = pay.fn_Loan_Cancel();
        if (banklist.Count > 0)
        {
            grid_loan.DataSource = banklist;
            grid_loan.DataBind();
            grid_ddl_load();
        }
    }

    public void grid_load()
    {
        banklist = pay.fn_LoanCancel(pay);
        if (banklist.Count > 0)
        {
            grid_loan.DataSource = banklist;
            grid_loan.DataBind();
            grid_ddl_load();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Record Found');", true);
        }

    }

    public void ddl_year_load(DropDownList arg_year)
    {
        try
        {
            cur_yr = DateTime.Now.Year;
            cur_yr = cur_yr + 5;
            for (yr_it = 1990; yr_it <= cur_yr; yr_it++)
            {
                arg_year.Items.Add(Convert.ToString(yr_it));
            }
            arg_year.SelectedValue = Convert.ToString(DateTime.Now.Year);
            //ddl_month.SelectedValue = Convert.ToString(DateTime.Now.Month);
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    public string create_date(DropDownList arg_month,DropDownList arg_year)
    {
        c_date = "01/" + arg_month.SelectedItem.Value + "/" + arg_year.SelectedItem.Value;
        return c_date;
    }

    public void grid_ddl_load()
    {
        for (grd = 0; grd < grid_loan.Rows.Count; grd++)
        {
            ddl_load(((DropDownList)grid_loan.Rows[grd].FindControl("grd_Loan")), ((DropDownList)grid_loan.Rows[grd].FindControl("grd_Employee")));
            ddl_year_load(((DropDownList)grid_loan.Rows[grd].FindControl("grd_Year")));

            str_date = banklist[grd].strDate.ToString();
            month = str_date.Substring(3, 2);
            year = str_date.Substring(6, 4);

  //Loan           
            for (ddl_ex = 0; ddl_ex < ((DropDownList)grid_loan.Rows[grd].FindControl("grd_Loan")).Items.Count; ddl_ex++)
            {
                if (((DropDownList)grid_loan.Rows[grd].FindControl("grd_Loan")).Items[ddl_ex].Value == banklist[grd].loanid.ToString())
                {
                    ((DropDownList)grid_loan.Rows[grd].FindControl("grd_Loan")).SelectedIndex = ddl_ex;
                }
            }

 //Employee
            for (ddl_ex = 0; ddl_ex < ((DropDownList)grid_loan.Rows[grd].FindControl("grd_Employee")).Items.Count; ddl_ex++)
            {
                if (((DropDownList)grid_loan.Rows[grd].FindControl("grd_Employee")).Items[ddl_ex].Value == banklist[grd].EmployeeId.ToString())
                {
                    ((DropDownList)grid_loan.Rows[grd].FindControl("grd_Employee")).SelectedIndex = ddl_ex;
                }
            }

  //Year         
            for (ddl_ex = 0; ddl_ex < ((DropDownList)grid_loan.Rows[grd].FindControl("grd_Year")).Items.Count; ddl_ex++)
            {
                if (((DropDownList)grid_loan.Rows[grd].FindControl("grd_Year")).Items[ddl_ex].Value == year)
                {
                    ((DropDownList)grid_loan.Rows[grd].FindControl("grd_Year")).SelectedIndex = ddl_ex;
                }
            }

   //Month
            for (ddl_ex = 0; ddl_ex < ((DropDownList)grid_loan.Rows[grd].FindControl("grd_Month")).Items.Count; ddl_ex++)
            {
                if (((DropDownList)grid_loan.Rows[grd].FindControl("grd_Month")).Items[ddl_ex].Value == month)
                {
                    ((DropDownList)grid_loan.Rows[grd].FindControl("grd_Month")).SelectedIndex = ddl_ex;
                }
            }
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
            ViewState["Loancancel_BranchID"] = Convert.ToInt32(ddl_Branch.SelectedValue);
            tab_ddl.Visible = true;
            ddl_load(ddl_Loancode, ddl_empcode);
        }
        else
        {
            tab_ddl.Visible = false;
        }
    }
}
