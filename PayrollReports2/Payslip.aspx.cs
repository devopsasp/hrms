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
using ePayHrms.Leave;
using CrystalDecisions.Web;
using CrystalDecisions.Enterprise;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.Reporting;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

public partial class PayrollReports_Default : System.Web.UI.Page
{
    Company company = new Company();
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    Candidate c = new Candidate();
    Leave l = new Leave();

    Collection<Company> CompanyList, ddlBranchsList;
    Collection<Employee> EmpProfileList;
    Collection<Employee> EmployeeList;
    char s_login_role;

    int i = 0, j, temp_count = 0, temp_i;

    string query = "", s_form, temp_string;
    DataSet ds_userrights;

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
        Response.AddHeader("pragma", "no-cache");
        Response.AddHeader("Cache-Control", "no-cache");
        Response.CacheControl = "no-cache";
        Response.Expires = -1;
        Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
        Response.Cache.SetNoStore();
        Session["Msg_session"] = "";
        Session["Period"] = "";
        Session["Repordid"] = "";
        Session["ReportSelection"] = "";
        Session["Query_Session"] = "start";
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        c.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);

        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        s_login_role = Convert.ToChar(Request.Cookies["Login_temp_Role"].Value);

  

        if (!IsPostBack)
        {
            date_load();

            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {

                switch (s_login_role)
                {

                    case 'a':
                        ddl_Branch.Visible = true;
                        ddl_Branch_load();
                        break;

                    case 'h':
                        ddl_Branch.Visible = false;
                        ddl_Department_load();
                        ddl_Category_load();
                        hr();
                        session_check();
                        break;

                    case 'u':
                        s_form = "57";

                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            ddl_Branch.Visible = false;

                            hr();
                            session_check();
                        }
                        else
                        {
                            Session["Msg_session"] = "Permission Restricted. Please Contact Administrator.";
                            Response.Redirect("~/Company_Home.aspx");
                        }
                        break;

                    case 'e':
                        ddl_Branch.Visible = false;
                        ListItem li = new ListItem();
                        li.Text = Request.Cookies["Login_temp_EmpCodeName"].Value;
                        li.Value = Request.Cookies["Login_temp_EmployeeID"].Value;
                        li.Selected = true;
                        chk_Empcode.Items.Clear();
                        chk_Empcode.Items.Add(li);
                        chk_Empcode.Enabled = false;
                        chkall.Disabled = true;
                        DropDownList1.Enabled = false;
                        break;
                    default:
                        Session["Msg_session"] = "Permission Restricted. Please Contact Administrator";
                        Response.Redirect("~/Company_Home.aspx");
                        break;
                }
            }
            else
            {

                Session["Msg_session"] = "Create Company";
                Response.Redirect("~/Company_Home.aspx");
            }
        }
    }
    public void admin()
    {

        EmployeeList = employee.fn_getEmployeeList(employee);

        if (EmployeeList.Count > 0)
        {
            chk_Empcode.DataSource = EmployeeList;
            chk_Empcode.DataTextField = "LastName";
            chk_Empcode.DataValueField = "EmployeeId";
            chk_Empcode.DataBind();
        }
        else
        {
          
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('No employees');};", true);
            chk_Empcode.Items.Clear();
        }


        //employee.temp_str = "select * from Temp_Employee";

        //EmpFirstList = employee.Temp_Selected_EmployeeList(employee);

        //if (EmpFirstList.Count == 0)
        //{
        //    temp_tables();

        //}  
    }

    public void ddl_Department_load()
    {
        EmployeeList = employee.fn_getDepartmentList1(employee.BranchId);
        if (EmployeeList.Count > 0)
        {

            for (int ddl_i = -2; ddl_i < EmployeeList.Count; ddl_i++)
            {

                if (ddl_i == -2)
                {
                    ListItem es_list = new ListItem();

                    es_list.Text = "Select Department";
                    es_list.Value = "0";
                    ddl_department.Items.Add(es_list);
                }
                else if (ddl_i == -1)
                {
                    ListItem es_list = new ListItem();

                    es_list.Text = "All";
                    es_list.Value = "0";
                    ddl_department.Items.Add(es_list);
                }
                else
                {
                    ListItem es_list = new ListItem();
                    es_list.Value = EmployeeList[ddl_i].DepartmentId.ToString();
                    es_list.Text = EmployeeList[ddl_i].DepartmentName.ToString();
                    ddl_department.Items.Add(es_list);
                }
            }
        }
    }

    public void ddl_Category_load()
    {
        EmployeeList = employee.fn_getCategoryList1(employee.BranchId);
        if (EmployeeList.Count > 0)
        {

            for (int ddl_i = -2; ddl_i < EmployeeList.Count; ddl_i++)
            {

                if (ddl_i == -2)
                {
                    ListItem es_list = new ListItem();

                    es_list.Text = "Select Category";
                    es_list.Value = "0";
                    ddl_category.Items.Add(es_list);
                }
                else if (ddl_i == -1)
                {
                    ListItem es_list = new ListItem();

                    es_list.Text = "All";
                    es_list.Value = "0";
                    ddl_category.Items.Add(es_list);
                }
                else
                {
                    ListItem es_list = new ListItem();
                    es_list.Value = EmployeeList[ddl_i].CategoryId.ToString();
                    es_list.Text = EmployeeList[ddl_i].CategoryName.ToString();
                    ddl_category.Items.Add(es_list);
                }
            }
        }
    }

    public void hr()
    {
        try
        {
            EmployeeList = employee.fn_getEmployeeList(employee);

            if (EmployeeList.Count > 0)
            {
                chk_Empcode.DataSource = EmployeeList;
                chk_Empcode.DataTextField = "LastName";
                chk_Empcode.DataValueField = "EmployeeId";
                chk_Empcode.DataBind();
            }
            else
            {
                
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('No employees');};", true);
                chk_Empcode.Items.Clear();
            }
        }
        catch (Exception ex)
        {
          
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Error Occured.');};", true);
        }
    }


    public void session_check()
    {

        switch (Convert.ToString(Session["Query_Session"]))
        {
            case "start":

                Session["Query_Session"] = "start";
                break;

            case "nil":
             
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('No Result Found');};", true);
                Session["Query_Session"] = "start";
                break;

            case "back":
             
                Session["Query_Session"] = "start";
                break;

            default:
                final_query_execute();
                break;

        }


    }

    public void final_query_execute()
    {
        employee.temp_str = (string)Session["Query_Session"];
        EmployeeList = employee.Temp_Selected_EmployeeList(employee);
        if (EmployeeList.Count > 0)
        {
            for (i = 0; i < chk_Empcode.Items.Count; i++)
            {
                for (j = 0; j < EmployeeList.Count; j++)
                {
                    if (Convert.ToInt32(chk_Empcode.Items[i].Value) == EmployeeList[j].EmployeeId)
                    {
                        chk_Empcode.Items[i].Selected = true;
                    }
                }
            }
          
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('"+EmployeeList.Count+" Employees Selected!');};", true);
            Session["Query_Session"] = "start";
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('No Employees has been selected');};", true);
            Session["Query_Session"] = "start";
        }
    }

    public void checked_All()
    {
        for (i = 0; i < chk_Empcode.Items.Count; i++)
        {
            chk_Empcode.Items[i].Selected = true;
        }
    }

    protected void btn_Report_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdate, todate, month, year;
            string fdate = "", tdate = "";
            int i_month, i_year;
            string start_date, end_date;
            month = ddl_monthlist.SelectedValue;
            year = ddl_yearlist.SelectedValue;
            i_month = Convert.ToInt32(month);
            i_year = Convert.ToInt32(year);

            if (s_login_role == 'a')
            {
                pay.BranchId = (int)ViewState["Pay_BranchID"];
                employee.BranchId = (int)ViewState["Pay_BranchID"];
                c.BranchID = (int)ViewState["Pay_BranchID"];
            }

            if (i_month > 0 && i_month <= 9)
            {
                fromdate = "01/" + month + "/" + year;
                todate = Convert.ToString(DateTime.DaysInMonth(i_year, i_month)) + "/" + month + "/" + year;
            }
            else
            {
                fromdate = "01/" + month + "/" + year;
                todate = Convert.ToString(DateTime.DaysInMonth(i_year, i_month)) + "/" + month + "/" + year;
            }
            start_date = employee.Convert_ToSqlDatestring(fromdate);
            end_date = employee.Convert_ToSqlDatestring(todate);
            employee.d_Date = start_date;
            //delete and drop tables
            
            
            table_delete();


            for (i = 0; i < chk_Empcode.Items.Count; i++)
            {
                if (chk_Empcode.Items[i].Selected == true)
                {
                    query = "insert into temp_employeeid values(" + employee.CompanyId + "," + employee.BranchId + "," + chk_Empcode.Items[i].Value + ", '" + start_date + "')";
                    employee.fn_reportbyid(query);
                    temp_count++;
                }
            }
            query = "select a.*, b.max_amount into  temp_Payinput from payinput a, paym_pf b where a.d_date='" + start_date + "' and a.pn_branchID = b.pn_BranchID and a.pn_branchID = '" + employee.BranchId + "'";
            //query = "select * into  temp_Payinput from payinput where d_date='" + start_date + "'";
            employee.fn_reportbyid(query);

            query = "select a.*,b.v_earningsname into temp_earnings from payoutput_earnings a,paym_earnings b where a.pn_earningsid=b.pn_earningsid and a.d_date='" + start_date + "'";
            employee.fn_reportbyid(query);

            query = "select a.*,b.v_deductionname into temp_deductions from payoutput_deductions a,paym_deduction b where a.pn_deductionid=b.pn_deductionid and a.d_date='" + start_date + "'";
            employee.fn_reportbyid(query);

            query = "select a.* , b.EPF, b.FPF into temp_netpay  from payoutput_netpay a, payoutput_pf b where a.d_date=b.d_date and a.pn_BranchID = b.pn_branchid and a.pn_branchid = '" + employee.BranchId + "' and a.d_date='" + start_date + "' and a.pn_EmployeeID=b.pn_EmployeeID";
            //query = "select * into temp_netpay  from payoutput_netpay where pn_branchid = '" + employee.BranchId + "' and d_date='" + start_date + "'";
            employee.fn_reportbyid(query);

            //EmpProfileList = employee.Temp_Emp_Profile_update(employee);
            EmpProfileList = employee.Temp_Emp_Profile(employee);

            for (i = 0; i < chk_Empcode.Items.Count; i++)
            {
                if (chk_Empcode.Items[i].Selected == true)
                {
                    pay.d_date = Convert.ToDateTime(start_date);
                    pay.EmployeeId = Convert.ToInt32(chk_Empcode.Items[i].Value);
                    pay.Update_Paybill(pay);
                }
            }
            
            if (DropDownList1.SelectedValue == "5")
            {
                query = "Delete from tempregister";
                employee.fn_reportbyid(query);
                for (i = 0; i < chk_Empcode.Items.Count; i++)
                {
                    
                    if (chk_Empcode.Items[i].Selected == true)
                    {
                        fdate = "01/" + ddl_monthlist.SelectedItem.Value + "/" + ddl_yearlist.Text;
                        tdate = "01/" + ddl_monthlist1.SelectedItem.Value + "/" + ddl_yearlist1.Text;
                        pay.DurationFrom = employee.Convert_ToSqlDatestring(fdate);
                        pay.DurationTo = employee.Convert_ToSqlDatestring(tdate);
                        pay.EmployeeId = Convert.ToInt32(chk_Empcode.Items[i].Value);
                        pay.fn_Salary_Register(pay);
                        temp_count++;
                    }
                }
            }
           
            //EmpProfileList = employee.Temp_Emp_Profile_update(employee);
            EmpProfileList = employee.Temp_Emp_Profile(employee);
            temp_string = "";
            if (EmpProfileList.Count > 0)
            {
                for (temp_i = 0; temp_i < EmpProfileList.Count; temp_i++)
                {
                    temp_string = "set dateformat dmy;update Temp_Employee set DivisionName='" + EmpProfileList[temp_i].DivisionName + "',DepartmentName='" + EmpProfileList[temp_i].DepartmentName + "',DesignationName='" + EmpProfileList[temp_i].DesignationName + "',CategoryName='" + EmpProfileList[temp_i].CategoryName + "',GradeName='" + EmpProfileList[temp_i].GradeName + "',ShiftName='" + EmpProfileList[temp_i].ShiftName + "',JobStatusName='" + EmpProfileList[temp_i].JobStatusName + "',LevelName='" + EmpProfileList[temp_i].LevelName + "',projectsiteName='" + EmpProfileList[temp_i].ProjectsiteName + "',d_Date='" + EmpProfileList[temp_i].Date.ToShortDateString() + "',v_Reason='" + EmpProfileList[temp_i].temp_str + "'";

                    temp_string = temp_string + " where pn_EmployeeID=" + EmpProfileList[temp_i].EmployeeId + ";set dateformat mdy;";

                    employee.Temp_Employee(temp_string);

                    temp_string = "";
                }
            }
            
            if (temp_count > 0)
            {
                if (DropDownList1.SelectedItem.Text == "PaySlip")
                {
                    Session["preview_page"] = "~/PayrollReports/Payslip.aspx";
                    Session["ReportName"] = "~/crystalreports/Payslip.rpt";
                    Response.Redirect("Report_view.aspx");
                }
                else if (DropDownList1.SelectedItem.Text == "Pay Register")
                {
                    Session["preview_page"] = "~/PayrollReports/Payslip.aspx";
                    Session["ReportName"] = "~/crystalreports/PayRegister.rpt";
                    Response.Redirect("Report_view.aspx");
                }
                else if (DropDownList1.SelectedItem.Text == "Pay Bill")
                {
                    Session["preview_page"] = "~/PayrollReports/Payslip.aspx";
                    Session["ReportName"] = "~/crystalreports/PayBill.rpt";
                    Response.Redirect("Report_view.aspx");
                }
                else if (DropDownList1.SelectedItem.Text == "Acquittance")
                {
                    Session["preview_page"] = "~/PayrollReports/Payslip.aspx";
                    Session["ReportName"] = "~/crystalreports/Acquittance.rpt";
                    Response.Redirect("Report_view.aspx");
                }
                else if (DropDownList1.SelectedItem.Text == "Bank Report")
                {
                    Session["preview_page"] = "~/PayrollReports/Payslip.aspx";
                    Session["ReportName"] = "~/crystalreports/Bank.rpt";
                    Response.Redirect("Report_view.aspx");
                }
                else
                {
                    Session["preview_page"] = "~/PayrollReports/Payslip.aspx";
                    Session["ReportName"] = "~/crystalreports/SalaryRegisterYTD.rpt";
                    Session["ReportSelection"] = "Salary Register for the Period from " + Convert.ToDateTime(pay.DurationFrom).ToString("MMMMMMMMMMM yyyy") + " to " + Convert.ToDateTime(pay.DurationTo).ToString("MMMMMMMMMMM yyyy");
                    Response.Redirect("Report_view.aspx");
                }
            }
            else
            {
                
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('No Employee Selected');};", true);
            }
        }
        catch (Exception ex)
        {
           
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('"+ ex.Message + "');};", true);
        }

    }

    public void table_delete()
    {
        query = "delete from temp_employeeid";
        employee.fn_reportbyid(query);

        query = "drop table temp_Payinput";
        employee.fn_reportbyid(query);

        query = "drop table temp_earnings";
        employee.fn_reportbyid(query);

        query = "drop table temp_deductions";
        employee.fn_reportbyid(query);

        query = "drop table temp_netpay";
        employee.fn_reportbyid(query);

    }

    public string changedate(string tmp_dt)
    {
        string date = tmp_dt.Substring(3, 7);
        date = "01/" + date;
        return date;
    }

    public void date_load()
    {
        DateTime tnow = DateTime.Now;
        ArrayList AlYear = new ArrayList();
        int i = 0, yr_it, cur_yr;
        cur_yr = DateTime.Now.Year;
        //cur_yr = cur_yr + 5;

        for (yr_it = cur_yr - 5; yr_it <= cur_yr; yr_it++)
        {
            ddl_yearlist.Items.Add(Convert.ToString(yr_it));
            ddl_yearlist1.Items.Add(Convert.ToString(yr_it));
            i++;
        }
        i = i - 6;
        //current year is selected index
        //ddl_yearlist.SelectedIndex = i;
        //ddl_yearlist1.SelectedIndex = i;
        ddl_yearlist.SelectedValue = tnow.Year.ToString();
        ddl_monthlist.SelectedValue = tnow.Month.ToString();
        ddl_yearlist1.SelectedValue = tnow.Year.ToString();
        ddl_monthlist1.SelectedValue = tnow.Month.ToString();
    }
    protected void ddl_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Branch.SelectedValue !="0")
        {
            ViewState["Pay_BranchID"] = Convert.ToInt32(ddl_Branch.SelectedValue);
            pay.BranchId = Convert.ToInt32(ddl_Branch.SelectedValue);
            employee.BranchId = Convert.ToInt32(ddl_Branch.SelectedValue);
            c.BranchID = Convert.ToInt32(ddl_Branch.SelectedValue);
            admin();
            session_check();
        }
        else
        {

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

    public void ddl_Employee_load()
    {
        chk_Empcode.Items.Clear();

        if (ddl_department.SelectedItem.Text == "All")
        {
            EmployeeList = employee.fn_getEmployeeList(employee);
            if (EmployeeList.Count > 0)
            {
                for (int ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
                {
                    if (ddl_i == -1)
                    {
                        
                    }
                    else
                    {
                        ListItem es_list = new ListItem();
                        es_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
                        es_list.Text = EmployeeList[ddl_i].LastName.ToString();
                        chk_Empcode.Items.Add(es_list);
                    }
                }
            }
        }
        else
        {
            employee.DepartmentId = Convert.ToInt32(ddl_department.SelectedValue);
            EmployeeList = employee.fn_getEmployeeDepartment(employee);
            if (EmployeeList.Count > 0)
            {
                for (int ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
                {
                    if (ddl_i == -1)
                    {

                    }
                    else
                    {
                        ListItem es_list = new ListItem();
                        es_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
                        es_list.Text = EmployeeList[ddl_i].LastName.ToString();
                        chk_Empcode.Items.Add(es_list);
                    }
                }
            }
        }
    }

    public void ddl_Employee_Category()
    {
        chk_Empcode.Items.Clear();

        if (ddl_department.SelectedIndex != 0)
        {
            if (ddl_department.SelectedItem.Text != "All")
            {
                employee.DepartmentId = Convert.ToInt32(ddl_department.SelectedValue);
                employee.CategoryId = Convert.ToInt32(ddl_category.SelectedValue);
                EmployeeList = employee.fn_getEmployeeCategory(employee);
                if (EmployeeList.Count > 0)
                {
                    for (int ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
                    {
                        if (ddl_i == -1)
                        {

                        }
                        else
                        {
                            ListItem es_list = new ListItem();
                            es_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
                            es_list.Text = EmployeeList[ddl_i].LastName.ToString();
                            chk_Empcode.Items.Add(es_list);
                        }
                    }
                }
            }
            else
            {
                if (ddl_category.SelectedItem.Text == "All")
                {
                    EmployeeList = employee.fn_getEmployeeList(employee);
                    if (EmployeeList.Count > 0)
                    {
                        for (int ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
                        {
                            if (ddl_i == -1)
                            {

                            }
                            else
                            {
                                ListItem es_list = new ListItem();
                                es_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
                                es_list.Text = EmployeeList[ddl_i].LastName.ToString();
                                chk_Empcode.Items.Add(es_list);
                            }
                        }
                    }
                }
                else
                {
                    employee.DepartmentId = Convert.ToInt32(ddl_department.SelectedValue);
                    employee.CategoryId = Convert.ToInt32(ddl_category.SelectedValue);
                    EmployeeList = employee.fn_getEmployeeCategory(employee);
                    if (EmployeeList.Count > 0)
                    {
                        for (int ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
                        {
                            if (ddl_i == -1)
                            {

                            }
                            else
                            {
                                ListItem es_list = new ListItem();
                                es_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
                                es_list.Text = EmployeeList[ddl_i].LastName.ToString();
                                chk_Empcode.Items.Add(es_list);
                            }
                        }
                    }
                }
            }
        }
        else
        {
         
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Select Department');};", true);
        }
    }

    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Employee_load();
    }

    protected void ddl_category_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Employee_Category();
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedValue == "5")
        {
            lbl_month.Visible = true;
            lbl_year.Visible = true;
            ddl_monthlist1.Visible = true;
            ddl_yearlist1.Visible = true;
        }  
        else
        {
            lbl_month.Visible = false;
            lbl_year.Visible = false;
            ddl_monthlist1.Visible = false;
            ddl_yearlist1.Visible = false;
        }
    }

    protected void chk_Empcode_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
