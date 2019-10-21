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

public partial class Bank_Loan_Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand com = new SqlCommand();
    Employee employee = new Employee();
    Company company = new Company();
    PayRoll pay = new PayRoll();


    //Candidate c = new Candidate();

    Collection<Employee> EmployeeList;   
    Collection<PayRoll> Earn_List;
    Collection<PayRoll> emp_Earn_List;
    Collection<Company> CompanyList, ddlBranchsList;

    DataSet ds_emp = new DataSet();
    DataSet ds_userrights;

    string str_query = "", str_date = "", _value = "", c_date = "", s_form;
    int i, temp = 0, cur_yr, yr_it;
    string s_login_role;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        //pay.CompanyId = 1;
        //pay.BranchId = 1;
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);   
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        //c.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);

         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        //c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        lbl_Error.Text = "";
        tr_chk.Visible = false;
     

        if (!IsPostBack)
        {          
            ddl_year_load();
            period_load();
            //ddl_load();
            CompanyList = company.fn_getCompany();
            if (CompanyList.Count > 0)
            {
                //ddl_year_load();
                switch (s_login_role)
                {
                    case "a":
                        tbl_earnings.Visible = false;
                        ddl_Branch.Visible = true;
                        ddl_Branch_load();
                        break;

                    case "h":
                        
                        ddl_Branch.Visible = false;
                        tbl_earnings.Visible = true;
                        ddl_department_load();
                        //ddl_load();
                        //session_check();
                        break;

                    case "u":
                        s_form = "38";

                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            ddl_Branch.Visible = false;
                            tbl_earnings.Visible = true;
                            ddl_load();
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
    
    protected void edit(object sender, GridViewEditEventArgs e)
    {
        if (s_login_role == "a")
        {
            pay.BranchId = (int)ViewState["NonEarn_BranchID"];
            employee.BranchId = (int)ViewState["NonEarn_BranchID"];
        }

        pay.EmployeeId = Convert.ToInt32(((Label)grd_earnings.Rows[e.NewEditIndex].FindControl("grdempid")).Text);
        pay.EarningsId = Convert.ToInt32(grd_earnings.DataKeys[e.NewEditIndex].Value);
        pay.d_date = Convert.ToDateTime(((Label)grd_earnings.Rows[e.NewEditIndex].FindControl("grddate")).Text);
        //pay.d_date = pay.Convert_ToSqlDate(((Label)grd_earnings.Rows[e.NewEditIndex].FindControl("grddate")).Text);
        pay.Amount = Convert.ToInt32(((HtmlInputText)grd_earnings.Rows[e.NewEditIndex].FindControl("grdamount")).Value);
        pay.regular = 'Y';

        ((ImageButton)grd_earnings.Rows[e.NewEditIndex].FindControl("img_update")).Visible = true;
        ((ImageButton)grd_earnings.Rows[e.NewEditIndex].FindControl("img_save")).Visible = false;
        ((HtmlInputText)grd_earnings.Rows[e.NewEditIndex].FindControl("grdamount")).Disabled = true;

        _value=pay.Emp_Earnings(pay);
        
        if (_value != "1")
        {
            lbl_Error.Text = "<font color=blue>Updated successfully</font>";
        }
        else
        {
            lbl_Error.Text = "<font color=blue>Error occured</font>";
        }
    }

    protected void update(object sender, GridViewUpdateEventArgs e)
    {
        ((HtmlInputText)grd_earnings.Rows[e.RowIndex].FindControl("grdamount")).Disabled = false;
        ((ImageButton)grd_earnings.Rows[e.RowIndex].FindControl("img_save")).Visible = true;
        ((ImageButton)grd_earnings.Rows[e.RowIndex].FindControl("img_update")).Visible = false;
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (s_login_role == "a")
        {
            pay.BranchId = (int)ViewState["NonEarn_BranchID"];
            employee.BranchId = (int)ViewState["NonEarn_BranchID"];
        }
        for (int j = 0; j < chk_allowance.Items.Count; j++)
        {
            //GridViewRow Department_row = grid_sections.Rows[j];
            //bool Department_check = ((HtmlInputCheckBox)Department_row.FindControl("Chk_Department")).Checked;
            if(chk_allowance.Items[j].Selected == true)
            {
                for (i = 0; i < chk_empname.Items.Count; i++)
                {
                    if (chk_empname.Items[i].Selected == true)
                    {
                        //pay.regularearningid = 0;
                        pay.EmployeeId = Convert.ToInt32(chk_empname.Items[i].Value);
                        pay.EarningsId = Convert.ToInt32(chk_allowance.Items[j].Value);
                        pay.d_date = Convert.ToDateTime(create_date()); //pay.Convert_ToSqlDate(create_date());
                        pay.Amount = Convert.ToInt32(Txtamount.Value);
                        pay.regular = 'Y';
                        string Fdate = txt_fromdate.Text;
                        string[] datesplit = Fdate.Split('/', '-');
                        string dd = datesplit[0].ToString();
                        string mm = datesplit[1].ToString();
                        string yy = datesplit[2].ToString();
                        //string fd = mm + "/" + dd + "/" + yy;
                        string Tdate = txt_todate.Text;
                        string[] datesplit1 = Tdate.Split('/', '-');
                        string dd1 = datesplit1[0].ToString();
                        string mm1 = datesplit1[1].ToString();
                        string yy1 = datesplit1[2].ToString();
                        //string td = mm1 + "/" + dd1 + "/" + yy1;
                        pay.strapplicationdate = mm + "/" + dd + "/" + yy;
                        pay.strdateapplication = mm1 + "/" + dd1 + "/" + yy1;
                        pay.periodCode = ddl_periodcode.SelectedItem.Text;

                        _value = pay.Emp_Earnings(pay);
                        if (_value != "1")
                        {
                            lbl_Error.Text = "<font color=blue>Added sucessfully</font>";
                        }
                        else
                        {
                            lbl_Error.Text = "<font color=blue>Error occured</font>";
                        }
                    }
                }
            }
        }
    }


    public string create_date()
    {
        c_date = ddl_year.SelectedItem.Value + "/" + ddl_month.SelectedItem.Value + "/01";
        return c_date;
    }

    protected void btn_details_Click(object sender, EventArgs e)
    {
        if (s_login_role == "a")
        {
            pay.BranchId = (int)ViewState["NonEarn_BranchID"];
            employee.BranchId = (int)ViewState["NonEarn_BranchID"];
        }
        //temp = 0;
        //for (i = 0; i < chk_empname.Items.Count; i++)
        //{
        //    if (chk_empname.Items[i].Selected == true)
        //    {
        //        if (temp == 0)
        //        {
        //            str_empcount = "(" + i + "";
        //            temp++;
        //        }
        //        else
        //        {
        //            str_empcount = str_empcount + "," + i + "";
        //        }
        //    }
        //}
        //str_empcount = str_empcount + ")";
        //if (temp == 0)
        //{
            str_query = "select ed.pn_EmployeeID,ed.pn_EarningsID,ed.n_Amount,ed.d_Date,e.EmployeeCode,d.v_EarningsCode";
            str_query = str_query + " from paym_Emp_Earnings ed,paym_Employee e,paym_Earnings d where";
            str_query = str_query + " ed.d_Date='" +create_date()+ "' and d.c_Regular='N' and e.pn_EmployeeID=ed.pn_EmployeeID and d.pn_EarningsID=ed.pn_EarningsID";
            str_query = str_query + " and ed.pn_CompanyID=" + pay.CompanyId + " and ed.pn_BranchID=" + pay.BranchId;
        //    pay.temp_str = str_query;
        //}
        //else
        //{
        //    str_query = "select ed.pn_EmployeeID,ed.pn_EarningsID,ed.n_Amount,ed.d_Date,e.EmployeeCode,d.v_EarningsCode";
        //    str_query = str_query + " from paym_Emp_Earnings ed,paym_Employee e,paym_Earnings d where ed.d_Date='" + pay.Date + "'";
        //    str_query = str_query + " and ed.pn_EmployeeID in" + str_empcount + " and e.pn_EmployeeID=ed.pn_EmployeeID and d.pn_EarningsID=ed.pn_EarningsID";

            pay.temp_str = str_query;

        //}
        emp_Earn_List = pay.fn_Emp_NonEarnings(pay);

        if (emp_Earn_List.Count > 0)
        {
            grd_earnings.Visible = true;
            grd_earnings.DataSource = emp_Earn_List;
            grd_earnings.DataBind();
            lbl_Error.Text = "";
        }

        else
        {
            lbl_Error.Text = "No Record Found..";
        }
    }

    public void ddl_load()
    {
        if (s_login_role == "a")
        {
            pay.BranchId = (int)ViewState["NonEarn_BranchID"];
            employee.BranchId = (int)ViewState["NonEarn_BranchID"];
        }
        if (s_login_role == "h")
        {
            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
             pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        }
        str_query = "Select a.pn_EmployeeID, a.EmployeeCode, a.Employee_First_Name from paym_employee a, paym_employee_profile1 b where a.pn_CompanyID=b.pn_CompanyID and a.pn_BranchID=b.pn_BranchID and a.pn_EmployeeID=b.pn_EmployeeID and b.pn_DepartmentID=" + ddl_department.SelectedValue + " and b.pn_CompanyID=" + employee.CompanyId + " and a.status='y' and b.pn_BranchID=" + employee.BranchId;

        EmployeeList = employee.fn_getEmplist(str_query);
        //EmployeeList = employee.fn_getAllEmployees();

        if (EmployeeList.Count > 0)
        {
            chk_empname.Enabled = true;
            //tbl_earnings.Visible = true;
            chk_empname.DataSource = EmployeeList;
            chk_empname.DataTextField = "LastName";
            chk_empname.DataValueField = "EmployeeId";
            chk_empname.DataBind();
        }
        else
        {
            lbl_Error.Text = "No employees";
            //tbl_earnings.Visible = false;
            grd_earnings.Visible = false;
            chk_empname.Items.Clear();
            chk_empname.Enabled = false;
        }

        Earn_List = pay.fn_NonEarnings1(employee.BranchId);

        if (Earn_List.Count > 0)
        {
            //ddl_Earn.DataSource = Earn_List;
            //ddl_Earn.DataTextField = "EarningsCode";
            //ddl_Earn.DataValueField = "EarningsId";
            //ddl_Earn.DataBind();
            chk_allowance.DataSource = Earn_List;
            chk_allowance.DataTextField = "EarningsCode";
            chk_allowance.DataValueField = "EarningsId";
            chk_allowance.DataBind();
        }
        else
        {
            lbl_Error.Text = "No Earnings";
        }
    }

    public void ddl_year_load()
    {
        try
        {
            cur_yr = DateTime.Now.Year;
            cur_yr = cur_yr + 5;

            for (yr_it = 1990; yr_it <= cur_yr; yr_it++)
            {
                ddl_year.Items.Add(Convert.ToString(yr_it));
            }
            ddl_year.SelectedValue = Convert.ToString(DateTime.Now.Year);
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
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
            ViewState["NonEarn_BranchID"] = Convert.ToInt32(ddl_Branch.SelectedValue);
            tbl_earnings.Visible = true;
            ddl_load();
        }
        else
        {
            tbl_earnings.Visible = false;
        }
    }
    protected void Chkall_CheckedChanged(object sender, EventArgs e)
    {
        if (Chkall.Checked == true)
        {
            foreach (ListItem li in chk_empname.Items)
            {
                li.Selected = true;
            }
        }
        else
        {
            foreach (ListItem li in chk_empname.Items)
            {
                li.Selected = false;
            }
        }
    }
    protected void Chkall1_CheckedChanged(object sender, EventArgs e)
    {
        if (Chkall1.Checked == true)
        {
            foreach (ListItem li in chk_allowance.Items)
            {
                li.Selected = true;
            }
        }
        else
        {
            foreach (ListItem li in chk_allowance.Items)
            {
                li.Selected = false; 
            }
        }
    }
    public void period_load()
    {
        con.Open();
        com = new SqlCommand("Select * from salary_period where pn_branchid = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "' order by from_date asc", con);
        SqlDataReader re = com.ExecuteReader();
        while (re.Read())
        {
            ddl_periodcode.Items.Add(re["period_code"].ToString());
        }
        re.Close();
        con.Close();
    }
    protected void ddl_periodcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        con.Open();
        com=new SqlCommand("Select * from salary_period where pn_branchid = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "' and period_code='"+ddl_periodcode.SelectedItem.Text+"' order by from_date asc", con);
        SqlDataReader re=com.ExecuteReader();
        if (re.Read())
        {
            ddl_month.SelectedItem.Text = re["p_month"].ToString();
            ddl_year.SelectedItem.Text = re["p_year"].ToString();
            txt_fromdate.Text = Convert.ToDateTime(re["from_date"]).ToString("dd/MM/yyyy");
            txt_todate.Text = Convert.ToDateTime(re["to_date"]).ToString("dd/MM/yyyy");
        }
        re.Close();
        con.Close();
            


    }
    public void ddl_department_load()
    {


        if (s_login_role == "a")
        {
            employee.BranchId = Convert.ToInt32(ddl_Branch.SelectedItem.Value);
        }
        EmployeeList = employee.fn_getDepartmentList1(employee.BranchId);
        if (EmployeeList.Count > 0)
        {

            for (int ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem list = new ListItem();

                    list.Text = "Select Department";
                    list.Value = "sd";
                    ddl_department.Items.Add(list);
                }
                else
                {
                    ListItem list = new ListItem();

                    list.Text = EmployeeList[ddl_i].DepartmentName;
                    list.Value = EmployeeList[ddl_i].DepartmentId.ToString();
                    ddl_department.Items.Add(list);
                }
            }
        }
        else
        {
            lbl_Error.Text = "No Department Available";
        }

    }
    protected void chk_empname_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        tr_chk.Visible = true;
        if (ddl_department.SelectedValue != "sd")
        {
            ddl_load();
        }
    }
}
