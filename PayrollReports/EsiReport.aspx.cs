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
using Microsoft.Office.Interop.Excel;
using System.Reflection;

public partial class Hrms_Reports_EsiReport : System.Web.UI.Page
{
    Company company = new Company();
    Employee employee = new Employee();
    PayRoll pay = new PayRoll();
    Candidate c = new Candidate();
    //Leave l = new Leave();
    //Be_Recruitment r = new Be_Recruitment();

    Collection<Company> CompanyList, ddlBranchsList;
    Collection<Employee> EmployeeList, Empcodelist;
    Collection<PayRoll> PayList;

    private static Microsoft.Office.Interop.Excel.Workbook mWorkBook;
    private static Microsoft.Office.Interop.Excel.Sheets mWorkSheets;
    private static Microsoft.Office.Interop.Excel.Worksheet mWSheet1;
    private static Microsoft.Office.Interop.Excel.Application oXL;
    char s_login_role;
    int i = 0, j, temp_count = 0; //count = 0, counting = 0, temp_mon = 0, cur_yr, yr_it;
    string query = "", s_form; //str_edu = "", s_Report = "", emp_code = "";
    string fromdate = "", todate, month, year;
    int i_month, i_year;
    //bool emp_check = false;
    //int i_sin, i_all, i_emp, earn_count = 0, ded_count = 0, cur_count = 0;
    //string temp_string = "", temp_earn = "", emp_count = "", sel_value = "";

    DataSet ds_userrights;

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Msg_session"] = "";
        Session["Period"] = "";
        Session["Repordid"] = "";

        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);        
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        c.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);

        pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        s_login_role = Convert.ToChar(Request.Cookies["Login_temp_Role"].Value);

        //lblmsg.Text = "";
        lbl_error.Text = "";

        //ddl_Branch.Visible = false;
        //row_type.Visible = false;
        //row_fromdate.Visible = false;
        //row_todate.Visible = false;
        //System.Globalization.DateTimeFormatInfo d = new System.Globalization.DateTimeFormatInfo();
        //string mm = d.MonthNames[01/01/2009];
        //Label1.Text = mm;

        if (!IsPostBack)
        {
            date_load();
            
            ddl_tomonthlist.Visible = false;
            ddl_toyearlist.Visible = false;
            lbl_todate.Visible = false;
            lbl_fromdate.Visible = false;
            
            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {
                //ddl_year_load();

                switch (s_login_role)
                {
                    case 'a': 
                        //admin();        
                        //tbl_esireport.Visible = false;

                        //tbl_details.Visible = false;
                        btn_Report.Visible = false;

                        ddl_Branch.Visible = true;
                        ddl_Branch_load();
                        //session_check();
                        break;

                    case 'h':
                        //hr();
                        ddl_Branch.Visible = false;
                        ddl_Department_load();
                        //tbl_esireport.Visible = true;
                        chkEmployee_load();
                        //pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                        //employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                        //c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                        session_check();
                        break;

                    case 'u':
                        s_form = "56";

                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            ddl_Branch.Visible = false;
                            //tbl_esireport.Visible = true;
                            chkEmployee_load();
                        }
                        else
                        {
                            //ddl_Branch.Visible = false;
                            //tbl_esireport.Visible = false;
                            Session["Msg_session"] = "Permission Restricted. Please Contact Administrator.";
                            Response.Redirect("~/Company_Home.aspx");
                        }

                        //hr();
                        //session_check();
                        break;

                    case 'e':
                        ddl_Branch.Visible = false;
                        ListItem li = new ListItem();
                        li.Text = Session["Login_temp_EmpCodeName"].ToString();
                        li.Value = Session["Login_temp_EmployeeID"].ToString();
                        li.Selected = true;
                        chk_Empcode.Items.Clear();
                        chk_Empcode.Items.Add(li);
                        chk_Empcode.Enabled = false;
                        //lbl_selectemp.Visible = false;
                        chkall.Disabled = true;
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
        //manually
        //if (Convert.ToString(Session["ses_report"]) == "1")
        //{
        //    EmployeeList = employee.fn_getAllEmployees();
        //}
        //else
        //{
        //    EmployeeList = employee.fn_getOldEmployees();

        //}
        //19-05-09
        //EmployeeList = employee.fn_getAllEmployees();
        //if (EmployeeList.Count > 0)
        //{
        //    chk_Empcode.DataSource = EmployeeList;
        //    chk_Empcode.DataTextField = "LastName";
        //    chk_Empcode.DataValueField = "EmployeeId";
        //    chk_Empcode.DataBind();
        //}
        //else
        //{
        //    lbl_error.Text = "No employees";

        //}
        //employee.temp_str = "select * from Temp_Employee";

        //EmpFirstList = employee.Temp_Selected_EmployeeList(employee);

        //if (EmpFirstList.Count == 0)
        //{
        //    temp_tables();

        //}  
    }

    public void hr()
    {
        try
        {
            if (Convert.ToString(Session["ses_report"]) == "1")
            {
                EmployeeList = employee.fn_getEmployeeList(employee);
            }
            else
            {
                EmployeeList = employee.fn_getOldEmployeeList(employee);

            }
            //19-05-09
            //if (EmployeeList.Count > 0)
            //{
            //    chk_Empcode.DataSource = EmployeeList;
            //    chk_Empcode.DataTextField = "LastName";
            //    chk_Empcode.DataValueField = "EmployeeId";
            //    chk_Empcode.DataBind();
            //}
            //else
            //{
            //    lbl_error.Text = "No employees";

            //}
        }
        catch (Exception ex)
        {
            lbl_error.Text = "Error";
        }
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

    public void session_check()
    {
        string s = Convert.ToString(Session["Query_Session"]);
        switch (Convert.ToString(Session["Query_Session"]))
        {
            case "start":
                lbl_error.Text = "Welcome To Report Section!";
                Session["Query_Session"] = "start";
                break;

            case "nil":
                lbl_error.Text = "No Result Found";
                Session["Query_Session"] = "start";
                break;

            case "back":
                lbl_error.Text = "";
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
            //19-05-09
            //for (i = 0; i < chk_Empcode.Items.Count; i++)
            //{
            //    for (j = 0; j < EmployeeList.Count; j++)
            //    {
            //        if (Convert.ToInt32(chk_Empcode.Items[i].Value) == EmployeeList[j].EmployeeId)
            //        {
            //            chk_Empcode.Items[i].Selected = true;
            //        }

            //    }
            //}
            lbl_error.Text = EmployeeList.Count + " Employees Selected!";
            Session["Query_Session"] = "start";
        }
        else
        {
            lbl_error.Text = "No Employees has been selected";
            Session["Query_Session"] = "start";
        }
    }

    protected void ddl_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Branch.SelectedValue != "0")
        {
            ViewState["ESI_BranchID"] = Convert.ToInt32(ddl_Branch.SelectedValue);
            //pay.BranchId = Convert.ToInt32(ddl_Branch.SelectedValue);
            //employee.BranchId = Convert.ToInt32(ddl_Branch.SelectedValue);
            //c.BranchID = Convert.ToInt32(ddl_Branch.SelectedValue);

            //tbl_esireport.Visible = true;
            //tbl_details.Visible = true;
            btn_Report.Visible = true;

            session_check();
            chkEmployee_load();
        }
        else
        {
            //tbl_esireport.Visible = false;
            //tbl_details.Visible = false;
            btn_Report.Visible = false;
        }

    }

    protected void btn_Report_Click(object sender, ImageClickEventArgs e)
    {

        month = ddl_monthlist.SelectedValue;
        year = ddl_yearlist.SelectedValue;
        i_month = Convert.ToInt32(month);
        i_year = Convert.ToInt32(year);

        if (s_login_role == 'a')
        {
            pay.BranchId = (int)ViewState["ESI_BranchID"];
            employee.BranchId = (int)ViewState["ESI_BranchID"];
            c.BranchID = (int)ViewState["ESI_BranchID"];
        }

        if (ddl_type.SelectedValue == "1")
        {
            fromdate = "01/" + month + "/" + year;
            todate = Convert.ToString(DateTime.DaysInMonth(i_year, i_month)) + "/" + month + "/" + year;
        }
        else if (ddl_type.SelectedValue == "2" || ddl_type.SelectedValue == "3" || ddl_type.SelectedValue == "4")
        {
            fromdate = Convert.ToDateTime("01/" + month + "/" + year).ToString();
            todate = Convert.ToDateTime("01/" + ddl_tomonthlist.SelectedValue + "/" + ddl_toyearlist.SelectedValue).ToString();
            //fromdate = "01/" + month + "/" + year;
            //todate = "01/" + ddl_tomonthlist.SelectedValue + "/" + ddl_toyearlist.SelectedValue;
        }

        query = "delete from temp_employeeid";
        employee.fn_reportbyid(query);

        Session["preview_page"] = "~/PayrollReports/EsiReport.aspx";
        switch (Convert.ToInt32(ddl_type.SelectedValue))
        {
            case 1:
                for (i = 0; i < chk_Empcode.Items.Count; i++)
                {
                    if (chk_Empcode.Items[i].Selected == true)
                    {
                        query = "set dateformat dmy;insert into temp_employeeid select pn_CompanyID, pn_BranchID, pn_EmployeeId, '" + fromdate + "' as d_date ";
                        query += "from paym_employee where pn_CompanyID=" + employee.CompanyId + " and pn_BranchID=" + employee.BranchId + " and pn_EmployeeId="+chk_Empcode.Items[i].Value+";set dateformat mdy";
                        employee.fn_reportbyid(query);
                    }
                }

                Session["ReportName"] = "~/crystalreports/Esi.rpt";
                Response.Redirect("Report_view.aspx");
                break;
            
            case 2:
                fn_esiprocess();
                Session["ReportName"] = "~/crystalreports/EsiForm6.rpt";
                Response.Redirect("Report_view.aspx");
                break;
            
            case 3:
                fn_esiprocess();
                Session["ReportName"] = "~/crystalreports/EsiForm7.rpt";
                Response.Redirect("Report_view.aspx");
                break;
           
            case 4:
                //query = "set dateformat dmy;insert into temp_employeeid select pn_CompanyID, pn_BranchID, pn_EmployeeId, '" + fromdate + "' as d_date ";
                //query += "from paym_employee where pn_CompanyID=" + employee.CompanyId + "and pn_BranchID=" + employee.BranchId + ";set dateformat mdy";
                //employee.fn_reportbyid(query);

                fn_esiprocess();
                Session["ReportName"] = "~/crystalreports/EsiChallan.rpt";
                Response.Redirect("Report_view.aspx");
                break;
            //}
        }
        //else
        //{
        //    lbl_error.Text = "No Employee Selected";
        //}
    }

    public void date_load()
    {

        DateTime tnow = DateTime.Now;
        ArrayList AlYear = new ArrayList();
        int i;
        for (i = tnow.Year - 5; i <= tnow.Year; i++)
            AlYear.Add(i);

        if (!this.IsPostBack)
        {
            ddl_yearlist.DataSource = AlYear;
            ddl_yearlist.DataBind();
            ddl_yearlist.SelectedValue = tnow.Year.ToString();
            ddl_monthlist.SelectedValue = tnow.Month.ToString();

            ddl_toyearlist.DataSource = AlYear;
            ddl_toyearlist.DataBind();
            ddl_toyearlist.SelectedValue = tnow.Year.ToString();
            ddl_tomonthlist.SelectedValue = tnow.Month.ToString();
        }

    }
    protected void ddl_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_type.SelectedValue == "1" || ddl_type.SelectedValue == "5")
        {
            ddl_tomonthlist.Visible = false;
            ddl_toyearlist.Visible = false;
            lbl_todate.Visible = false;
            lbl_fromdate.Visible = false;
            lbl_month.Visible = true;
        }
        else if (ddl_type.SelectedValue == "2" || ddl_type.SelectedValue == "3" || ddl_type.SelectedValue == "4")
        {
            ddl_tomonthlist.Visible = true;
            ddl_toyearlist.Visible = true;
            lbl_todate.Visible = true;
            lbl_fromdate.Visible = true;
            lbl_month.Visible = false;
        }
    }

    public void fn_esiprocess()
    {
        try
        {
            DateTime dt;
            Collection<Employee> date_list;
            ArrayList datearray = new ArrayList();
            int j = 0, temp_month, d_month, k;

            //string mm;
            //ReportDocument reportdoc = new ReportDocument();

            query = "delete from form7";
            employee.fn_reportbyid(query);

            query = "set dateformat dmy;SELECT DATEDIFF(month, '" + fromdate + "', '" + todate + "')+1 as differ;set dateformat mdy";
            date_list = employee.fn_getdatediff(query);
            temp_month = i_month;

            if (date_list[0].Count == 6)
            {
                for (j = 1; j <= date_list[0].Count; j++)
                {
                    if (temp_month >= 1 && temp_month <= 9)
                    {
                        //For IIS
                        dt = Convert.ToDateTime(year + "/" + "0" + Convert.ToString(temp_month) + "/" + "01");
                        datearray.Add(dt);
                        temp_month++;
                    }
                    else
                    {
                        //mm = "Month" + Convert.ToString(j);
                        //reportdoc.DataDefinition.FormulaFields[mm].Text =  temp_month.ToString("MMMM");
                        //For Local
                        //dt = Convert.ToDateTime("01/" + Convert.ToString(temp_month) + "/" + year);
                        //For IIS
                        dt = Convert.ToDateTime(year + "/" + Convert.ToString(temp_month) + "/" + "01");
                        datearray.Add(dt);
                        temp_month++;
                    }
                }
            }

            for (k = 0; k < chk_Empcode.Items.Count; k++)
            {
                if (chk_Empcode.Items[k].Selected == true)
                {
                    query = "set dateformat dmy;select e.pn_CompanyID, e.pn_BranchID, e.pn_EmployeeID, e.EmployeeCode, e.Esino, es.NetPay, es.ESI_EMP, es.ESI_EPR, p.Paid_Days, es.d_Date from paym_Employee e left join payoutput_esi es on e.pn_CompanyID=es.pn_CompanyID and es.d_date between'" + fromdate + "' and '" + todate + "' and e.pn_EmployeeID=es.pn_EmployeeID left join payinput p on e.pn_CompanyID=p.pn_CompanyID and e.pn_EmployeeID=p.pn_EmployeeID and es.d_date=p.d_date";
                    query += " where e.pn_CompanyID=" + employee.CompanyId + " and e.pn_BranchID=" + employee.BranchId + " and e.pn_EmployeeID=" + chk_Empcode.Items[k].Value + ";set dateformat mdy";
                    EmployeeList = employee.fn_getesidetails(query);


                    if (EmployeeList.Count > 0)
                    {
                        for (i = 0; i < EmployeeList.Count; i++)
                        {
                            //query = "insert into form7 (pn_CompanyID, pn_BranchID, pn_EmployeeID, EmployeeCode, Esino, netpay, ESI_EMP, ESI_EPR) values (" + EmployeeList[i].CompanyId + ","+ EmployeeList[i].BranchId + ","+EmployeeList[i].EmployeeId + ","+EmployeeList[i].EmployeeCode + ","+EmployeeList[i].ESIno + ","+EmployeeList[i].Netpay + ","+EmployeeList[i].ESI_EMP + ","+EmployeeList[i].ESI_EMPR + ")";
                            query = "select EmployeeCode from form7 where pn_CompanyID=" + EmployeeList[i].CompanyId + "and pn_BranchID=" + EmployeeList[i].BranchId + "and EmployeeCode='" + EmployeeList[i].EmployeeCode + "'";
                            Empcodelist = employee.fn_getempcount(query);

                            if (Empcodelist.Count == 0)
                            {
                                query = "insert into form7 (pn_CompanyID, pn_BranchID, pn_EmployeeID, EmployeeCode, Esino) values (" + EmployeeList[i].CompanyId + "," + EmployeeList[i].BranchId + ",'" + EmployeeList[i].EmployeeId + "','" + EmployeeList[i].EmployeeCode + "','" + EmployeeList[i].ESIno + "')";
                                employee.fn_reportbyid(query);
                            }

                            if (EmployeeList[i].d_Date.ToString() == datearray[0].ToString())
                            {
                                query = "update form7 set D1=" + EmployeeList[i].Paiddays + ", W1=" + EmployeeList[i].Netpay + ", Esi1=" + EmployeeList[i].ESI_EMP + ",Empr1=" + EmployeeList[i].ESI_EMPR + " where pn_EmployeeID='" + EmployeeList[i].EmployeeId + "' and pn_CompanyID=" + EmployeeList[i].CompanyId + " and pn_BranchID=" + EmployeeList[i].BranchId + "";
                                employee.fn_reportbyid(query);
                            }
                            else if (EmployeeList[i].d_Date.ToString() == datearray[1].ToString())
                            {
                                query = "update form7 set D2=" + EmployeeList[i].Paiddays + ", W2=" + EmployeeList[i].Netpay + ", Esi2=" + EmployeeList[i].ESI_EMP + ",Empr2=" + EmployeeList[i].ESI_EMPR + " where pn_EmployeeID='" + EmployeeList[i].EmployeeId + "' and pn_CompanyID=" + EmployeeList[i].CompanyId + " and pn_BranchID=" + EmployeeList[i].BranchId + "";
                                employee.fn_reportbyid(query);
                            }
                            else if (EmployeeList[i].d_Date.ToString() == datearray[2].ToString())
                            {
                                query = "update form7 set D3=" + EmployeeList[i].Paiddays + ", W3=" + EmployeeList[i].Netpay + ", Esi3=" + EmployeeList[i].ESI_EMP + ",Empr3=" + EmployeeList[i].ESI_EMPR + " where pn_EmployeeID='" + EmployeeList[i].EmployeeId + "' and pn_CompanyID=" + EmployeeList[i].CompanyId + " and pn_BranchID=" + EmployeeList[i].BranchId + "";
                                employee.fn_reportbyid(query);
                            }
                            else if (EmployeeList[i].d_Date.ToString() == datearray[3].ToString())
                            {
                                query = "update form7 set D4=" + EmployeeList[i].Paiddays + ", W4=" + EmployeeList[i].Netpay + ", Esi4=" + EmployeeList[i].ESI_EMP + ",Empr4=" + EmployeeList[i].ESI_EMPR + " where pn_EmployeeID='" + EmployeeList[i].EmployeeId + "' and pn_CompanyID=" + EmployeeList[i].CompanyId + " and pn_BranchID=" + EmployeeList[i].BranchId + "";
                                employee.fn_reportbyid(query);
                            }
                            else if (EmployeeList[i].d_Date.ToString() == datearray[4].ToString())
                            {
                                query = "update form7 set D5=" + EmployeeList[i].Paiddays + ", W5=" + EmployeeList[i].Netpay + ", Esi5=" + EmployeeList[i].ESI_EMP + ",Empr5=" + EmployeeList[i].ESI_EMPR + " where pn_EmployeeID='" + EmployeeList[i].EmployeeId + "' and pn_CompanyID=" + EmployeeList[i].CompanyId + " and pn_BranchID=" + EmployeeList[i].BranchId + "";
                                employee.fn_reportbyid(query);
                            }
                            else if (EmployeeList[i].d_Date.ToString() == datearray[5].ToString())
                            {
                                query = "update form7 set D6=" + EmployeeList[i].Paiddays + ", W6=" + EmployeeList[i].Netpay + ", Esi6=" + EmployeeList[i].ESI_EMP + ",Empr6=" + EmployeeList[i].ESI_EMPR + " where pn_EmployeeID='" + EmployeeList[i].EmployeeId + "' and pn_CompanyID=" + EmployeeList[i].CompanyId + " and pn_BranchID=" + EmployeeList[i].BranchId + "";
                                employee.fn_reportbyid(query);
                            }
                        }
                    }
                }
            }
            d_month = i_month;
            if (date_list[0].Count == 6)
            {
                for (j = 1; j <= date_list[0].Count; j++)
                {
                    if (d_month >= 1 && d_month <= 9)
                    {
                        query = "update form7 set M" + j + "='" + d_month + "'";
                        employee.fn_reportbyid(query);
                        d_month++;
                    }
                    else
                    {
                        query = "update form7 set M" + j + "='" + d_month + "'";
                        employee.fn_reportbyid(query);
                        d_month++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lbl_error.Text = "Select 6 months period";
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

    public void chkEmployee_load()
    {
        if (s_login_role == 'a')
        {
            pay.BranchId = (int)ViewState["ESI_BranchID"];
            employee.BranchId = (int)ViewState["ESI_BranchID"];
            c.BranchID = (int)ViewState["ESI_BranchID"];
        }

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
            chk_Empcode.Items.Clear();
        }
    }
    protected void btn_Report_Click(object sender, EventArgs e)
    {

        month = ddl_monthlist.SelectedValue;
        year = ddl_yearlist.SelectedValue;
        i_month = Convert.ToInt32(month);
        i_year = Convert.ToInt32(year);

        if (s_login_role == 'a')
        {
            pay.BranchId = (int)ViewState["ESI_BranchID"];
            employee.BranchId = (int)ViewState["ESI_BranchID"];
            c.BranchID = (int)ViewState["ESI_BranchID"];
        }

        if (ddl_type.SelectedValue == "1" || ddl_type.SelectedValue == "5")
        {
            fromdate = "01/" + month + "/" + year;
            todate = Convert.ToString(DateTime.DaysInMonth(i_year, i_month)) + "/" + month + "/" + year;
        }

        else if (ddl_type.SelectedValue == "2" || ddl_type.SelectedValue == "3" || ddl_type.SelectedValue == "4")
        {
            fromdate = Convert.ToDateTime("01/" + month + "/" + year).ToString();
            todate = Convert.ToDateTime("01/" + ddl_tomonthlist.SelectedValue + "/" + ddl_toyearlist.SelectedValue).ToString();
            //fromdate = "01/" + month + "/" + year;
            //todate = "01/" + ddl_tomonthlist.SelectedValue + "/" + ddl_toyearlist.SelectedValue;
        }

        query = "delete from temp_employeeid";
        employee.fn_reportbyid(query);
        Session["preview_page"] = "~/PayrollReports/EsiReport.aspx";
        switch (Convert.ToInt32(ddl_type.SelectedValue))
        {
            case 1:
                for (i = 0; i < chk_Empcode.Items.Count; i++)
                {
                    if (chk_Empcode.Items[i].Selected == true)
                    {
                        query = "set dateformat dmy;insert into temp_employeeid select pn_CompanyID, pn_BranchID, pn_EmployeeId, '" + fromdate + "' as d_date ";
                        query += "from paym_employee where pn_CompanyID=" + employee.CompanyId + " and pn_BranchID=" + employee.BranchId + " and pn_EmployeeId=" + chk_Empcode.Items[i].Value + ";set dateformat mdy";
                        employee.fn_reportbyid(query);
                    }
                }

                Session["ReportName"] = "~/crystalreports/Esi.rpt";
                Response.Redirect("Report_view.aspx");
                break;

            case 2:
                fn_esiprocess();
                Session["ReportName"] = "~/crystalreports/EsiForm6.rpt";
                Response.Redirect("Report_view.aspx");
                break;

            case 3:
                fn_esiprocess();
                Session["ReportName"] = "~/crystalreports/EsiForm7.rpt";
                Response.Redirect("Report_view.aspx");
                break;

            case 4:

                fn_esiprocess();
                Session["ReportName"] = "~/crystalreports/EsiChallan.rpt";
                Response.Redirect("Report_view.aspx");
                break;

            case 5:
                DataSet ds = new DataSet();
                string path = Server.MapPath("~//Files//Esi1.xls");
                string Source = Server.MapPath("~//Files//Esi.xls");
                string directory = path.Substring(0, path.LastIndexOf("\\"));// GetDirectory(Path);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                FileInfo fl = new FileInfo(path);
                if (fl.Exists)
                {
                    File.Delete(path);
                }
                File.Copy(Source, path);

                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;
                oXL.Interactive = false;
                oXL.DisplayAlerts = false;
                mWorkBook = oXL.Workbooks.Open(path, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                //Get all the sheets in the workbook
                mWorkSheets = mWorkBook.Worksheets;
                //Get the allready exists sheet
                mWSheet1 = (Microsoft.Office.Interop.Excel.Worksheet)mWorkSheets.get_Item("Sheet1");
                Microsoft.Office.Interop.Excel.Range range = mWSheet1.UsedRange;
                int colCount = range.Columns.Count;
                int rowCount = range.Rows.Count;


                for (i = 0; i < chk_Empcode.Items.Count; i++)
                { 
                    string esino = "";
                    if (chk_Empcode.Items[i].Selected == true)
                    {
                        rowCount += 1;
                        query = "set dateformat dmy;select a.ESIno,a.Employee_First_Name, b.Paid_Days, b.NetPay from paym_employee a, PayOutput_ESI b where a.pn_EmployeeID = b.pn_EmployeeID and a.pn_BranchID = b.pn_BranchID and a.pn_EmployeeID = " + chk_Empcode.Items[i].Value + " and b.d_Date = '" + fromdate + "' and a.pn_branchid = " + employee.BranchId + ";set dateformat mdy";
                        PayList = pay.fn_getESI(query);
                        if (PayList[0].ESI_No == "")
                            esino = "0";
                        else
                            esino = PayList[0].ESI_No;
                        mWSheet1.Cells[rowCount, 1] = esino;
                        mWSheet1.Cells[rowCount, 2] = PayList[0].FirstName;
                        mWSheet1.Cells[rowCount, 3] = PayList[0].Paid_Days.ToString();
                        mWSheet1.Cells[rowCount, 4] = PayList[0].Gross_Salary.ToString();

                    }
                }

                mWorkBook.SaveAs(path, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive,
                Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value);
                mWorkBook.Close(Missing.Value, Missing.Value, Missing.Value);
                mWSheet1 = null;
                mWorkBook = null;
                oXL.Quit();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                //FileInfo file = new FileInfo(path);
                //if (file.Exists)
                //{
                //    Response.ContentType = "application/vnd.ms-word";
                //    Response.AddHeader("Content-Disposition", "inline; filename=\"" + file.Name + "\"");
                //    Response.AddHeader("Content-Length", file.Length.ToString());
                //    Response.TransmitFile(file.FullName);

                //}

                Response.Redirect("~//DownloadECR.ashx?rt=Esi1.xls");
                break;
        }
        
    }
    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Employee_load();
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
}
