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
    string s_login_role;

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
        
        Session["Period"] = "";
        Session["Repordid"] = "";
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        c.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);

        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;

        lbl_error.Text = "";
       // Label1.Visible = false;
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
                        ddl_Department_load();
                      
                        break;

                    case "h":
                        ddl_Branch.Visible = false;
                        ddl_Department_load();
                      
                        break;

                    case "u":
                        s_form = "57";

                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            ddl_Branch.Visible = false;

                            hr();
                            //session_check();
                        }
                        else
                        {
                            Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator.";
                            Response.Redirect("~/Company_Home.aspx");
                        }
                        break;

                    case "e":
                        ddl_Branch.Visible = false;
                        ListItem li = new ListItem();
                        li.Text = Request.Cookies["EmpCodeName"].Value;
                        li.Value = Request.Cookies["Login_temp_EmployeeID"].Value;
                        li.Selected = true;
                        chk_Empcode.Items.Clear();
                        chk_Empcode.Items.Add(li);
                        chk_Empcode.Enabled = false;
                        chkall.Enabled = false;
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
            lbl_error.Text = "No employees";
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
                    es_list.Value = "1";
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
        //if (ddl_department.SelectedIndex == 1)
        //{
        //    ddl_Employee_load();
        //}

        
        //if (ddl_department.SelectedIndex == 0)
        //{
        //    if (s_login_role == "a")
        //    {
        //        admin();
        //    }
        //    if(s_login_role=="h")
        //    {
        //        hr();
        //    }
           
        //}

    }

    

    public void hr()
    {
        
        try
        {
            EmployeeList = employee.fn_getEmployeeList(employee);
            if (EmployeeList.Count > 0)
            {
               
                chk_Empcode.DataSource = EmployeeList;
                chk_Empcode.DataValueField = "EmployeeId";
                chk_Empcode.DataTextField = "LastName";
                chk_Empcode.DataBind();
                
            }
            else
            {
                lbl_error.Text = "No employees";
                chk_Empcode.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            lbl_error.Text = "Error";
        }
    }

    //public void session_check()
    //{

    //    switch (Request.Cookies["Query_Session"].Value)
    //    {
    //        case "start":
    //            lbl_error.Text = "Welcome To Report Section!";
    //            Response.Cookies["Query_Session"].Value= "start";
    //            break;

    //        case "nil":
    //            lbl_error.Text = "No Result Found";
    //            Response.Cookies["Query_Session"].Value= "start";
    //            break;

    //        case "back":
    //            lbl_error.Text = "";
    //            Response.Cookies["Query_Session"].Value= "start";
    //            break;

    //        default:
    //            final_query_execute();
    //            break;

    //    }


    //}

    public void final_query_execute()
    {

        employee.temp_str = Request.Cookies["Query_Session"].Value;

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

            lbl_error.Text = EmployeeList.Count + " Employees Selected!";
            Response.Cookies["Query_Session"].Value= "start";

        }
        else
        {
            lbl_error.Text = "No Employees has been selected";
            Response.Cookies["Query_Session"].Value= "start";
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
            if (s_login_role == "a")
            {
                pay.BranchId = (int)ViewState["Pay_BranchID"];
                employee.BranchId = (int)ViewState["Pay_BranchID"];
                c.BranchID = (int)ViewState["Pay_BranchID"];
            }

            //delete and drop tables
            table_delete();

            for (i = 0; i < chk_Empcode.Items.Count; i++)
            {
                if (chk_Empcode.Items[i].Selected == true)
                {
                    query = "insert into temp_employeeid values(" + employee.CompanyId + "," + employee.BranchId + "," + chk_Empcode.Items[i].Value + ", '" + DateTime.Now.ToString("MM/dd/yyyy") + "')";
                    employee.fn_reportbyid(query);
                    temp_count++;
                }
            }

            Session["preview_page"] = "~/PayrollReports/EmployeeGeneral.aspx";
            Session["ReportName"] = "~/crystalreports/EmpMaster.rpt";
            Response.Redirect("Report_view.aspx");
            
        }
        catch (Exception ex)
        {
            lbl_error.Text = ex.Message;
        }

    }

    public void table_delete()
    {
        query = "delete from temp_employeeid";
        employee.fn_reportbyid(query);
    }

    public string changedate(string tmp_dt)
    {
        string date = tmp_dt.Substring(3, 7);
        date = "01/" + date;
        return date;
    }

    
    protected void ddl_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Branch.SelectedValue != "0")
        {
            ViewState["Pay_BranchID"] = Convert.ToInt32(ddl_Branch.SelectedValue);
            pay.BranchId = Convert.ToInt32(ddl_Branch.SelectedValue);
            employee.BranchId = Convert.ToInt32(ddl_Branch.SelectedValue);
            c.BranchID = Convert.ToInt32(ddl_Branch.SelectedValue);
            admin();
            //session_check();
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
    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Employee_load();
        chkall.Checked = false;
    }

}
