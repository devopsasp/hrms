using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
using System.Data;
using System.Configuration;

public partial class PayrollReports_Bench_Cost_Analysis : System.Web.UI.Page
{
    ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand cmd1 = new SqlCommand();
    Company company = new Company();
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    Candidate c = new Candidate();
    Leave l = new Leave();

    Collection<Company> CompanyList, ddlBranchsList;
    Collection<Employee> DepartmentList;
    Collection<Employee> EmployeeList;
    Collection<PayRoll> PayList;
    char s_login_role;
    int presentday = 1;
    int i = 0, j, temp_count = 0;
    int ddl_i = 0;
    string query = "";
    DateTime from_date, to_date;
    int empid, count;
    int daycount;
    string empname, date1, date2, _Month, monthyear;
    string[] sd, ed;
    string s_form = "";
    int from_month, to_month, _Year;
    double act_basic, earned_basic, _Amount, Tot_amt;
    DateTime fromdate, todate;
    DataSet ds_userrights;
    int monthcount = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Msg_session"] = "";
        Session["Repordid"] = "";
        Session["fdate"] = "";
        Session["tdate"] = "";

        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        c.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);

        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        s_login_role = Convert.ToChar(Request.Cookies["Login_temp_Role"].Value);
        //lbl_error.Text = "";

        if (!IsPostBack)
        {
           // date_load();

            CompanyList = company.fn_getCompany();
            ListItem li = new ListItem();
            if (CompanyList.Count > 0)
            {
                switch (s_login_role)
                {
                    case 'a':
                        //admin();
                        //session_check();
                        tbl_pfreport.Visible = false;
                        ddl_Branch.Visible = true;
                        //ddl_Branch_load();
                        break;

                    case 'h':
                        //hr();
                        //session_check();
                        ddl_Branch.Visible = false;
                        tbl_pfreport.Visible = true;
                       
                        //session_check();
                        break;

                    case 'u': s_form = "79";
                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            ddl_Branch.Visible = false;
                            li = new ListItem();
                            li.Text = Session["Login_temp_EmpCodeName"].ToString();
                            li.Value = Session["Login_temp_EmployeeID"].ToString();
                            li.Selected = true;
                            chk_Empcode.Items.Add(li);
                            chk_Empcode.Enabled = false;
                            lbl_selectemp.Visible = false;
                            chkall.Visible = false;
                        }
                        else
                        {
                            Session["Msg_session"] = "Permission Restricted. Please Contact Administrator.";
                            Response.Redirect("~/Company_Home.aspx");
                        }
                        break;

                    case 'e':
                        ddl_Branch.Visible = false;
                        li = new ListItem();
                        li.Text = Session["Login_temp_EmpCodeName"].ToString();
                        li.Value = Session["Login_temp_EmployeeID"].ToString();
                        li.Selected = true;
                        chk_Empcode.Items.Add(li);
                        chk_Empcode.Enabled = false;
                        lbl_selectemp.Visible = false;
                        chkall.Visible = false;
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

    public void session_check()
    {

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

            lbl_error.Text = EmployeeList.Count + " Employees Selected!";
            Session["Query_Session"] = "start";

        }
        else
        {
            lbl_error.Text = "No Employees has been selected";
            Session["Query_Session"] = "start";
        }
    }
    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (ddl_type.SelectedItem.Text == "Graphical Representation Report")
        {
            ddlEmployee_load();
            Tr3.Visible = true;
        }
        else if (ddl_type.SelectedItem.Text == "Cost Analaysis Report")
        {
            chkEmployee_load();
            Tr1.Visible = true;
            lbl_selectemp.Visible = true;
            chkall.Visible = true;
            div_chkempcode.Visible = true;
            Tr3.Visible = false;
        }
        
    }
    public void ddl_department_load1()
    {
        ddl_department.Items.Clear();
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        if (s_login_role == 'a')
        {
            DepartmentList = employee.fn_getDepartmentList1(Convert.ToInt32(ddl_Branch.SelectedItem.Value));
        }
        else if (s_login_role == 'h')
        {
            DepartmentList = employee.fn_getDepartmentList1(employee.BranchId);
        }

        if (DepartmentList.Count > 0)
        {
            for (ddl_i = -1; ddl_i < DepartmentList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();
                    e_list.Text = "Select";
                    e_list.Value = "0";
                    ddl_department.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();
                    e_list.Value = DepartmentList[ddl_i].DepartmentId.ToString();
                    e_list.Text = DepartmentList[ddl_i].DepartmentName.ToString();
                    ddl_department.Items.Add(e_list);
                }
            }
        }
        else
        {

            ClientScriptManager manager = Page.ClientScript;
            manager.RegisterStartupScript(this.GetType(), "Call", "show_Error();", true);
        }
    }
    public void ddlEmployee_load()
    {
        employee.DepartmentId = Convert.ToInt32(ddl_department.SelectedItem.Value);
        ViewState["pn_DepartmentId"] = Convert.ToInt32(ddl_department.SelectedItem.Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        string str1 = ddl_department.SelectedItem.Text;
        employee.DivisionName = str1;
        string qry = "Select a.pn_EmployeeID,a.EmployeeCode,a.Employee_First_Name from paym_employee a, paym_employee_profile1 b where a.pn_CompanyID=b.pn_CompanyID and a.pn_BranchID=b.pn_BranchID and a.pn_EmployeeID=b.pn_EmployeeID and b.pn_DepartmentId=" + ddl_department.SelectedValue + " and b.pn_CompanyID=" + employee.CompanyId + " and b.pn_BranchID=" + employee.BranchId + " and status='Y' order by EmployeeCode ";

        EmployeeList = employee.fn_getEmplist(qry);
        if (EmployeeList.Count > 0)
        {
            for (ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();
                    e_list.Text = "Select";
                    e_list.Value = "0";
                    ddl_employee.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();
                    e_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
                    e_list.Text = EmployeeList[ddl_i].LastName.ToString();
                    ddl_employee.Items.Add(e_list);
                }
            }
        }
        else
        {
            ddl_employee.Items.Add("Select");
        }
    }
    public void chkEmployee_load()
    {
        employee.DepartmentId = Convert.ToInt32(ddl_department.SelectedItem.Value);
        ViewState["pn_DepartmentId"] = Convert.ToInt32(ddl_department.SelectedItem.Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        string str1 = ddl_department.SelectedItem.Text;
        employee.DivisionName = str1;
        string qry = "Select a.pn_EmployeeID,a.EmployeeCode,a.Employee_First_Name from paym_employee a, paym_employee_profile1 b where a.pn_CompanyID=b.pn_CompanyID and a.pn_BranchID=b.pn_BranchID and a.pn_EmployeeID=b.pn_EmployeeID and b.pn_DepartmentId=" + ddl_department.SelectedValue + " and b.pn_CompanyID=" + employee.CompanyId + " and b.pn_BranchID=" + employee.BranchId+" and status='Y' order by EmployeeCode ";

        EmployeeList = employee.fn_getEmplist(qry);
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
    protected void btn_Report_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ddl_type.SelectedItem.Text == "Cost Analaysis Report")
            {

                query = "delete from temp_emp_projects";
                employee.fn_reportbyid(query);
                myConnection.Open();
                for (i = 0; i < chk_Empcode.Items.Count; i++)
                {
                    if (chk_Empcode.Items[i].Selected == true)
                    {


                        //from_date = Convert.ToDateTime(txtFromDate.Text);
                        //to_date = Convert.ToDateTime(txtToDate.Text);

                        cmd1 = new SqlCommand("set dateformat dmy;select pn_employeename,from_date,to_date,isnull(datediff(day,from_date,to_date),0) as monthcount from paym_emp_projects where pn_employeeid='" + chk_Empcode.Items[i].Value + "' and p_name='Bench' and ((from_date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "') or ( to_date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "') or (from_date>='" + txtFromDate.Text + "' and to_date<='" + txtToDate.Text + "') or ( from_date<='" + txtFromDate.Text + "' and to_date>='" + txtToDate.Text + "'));set dateformat mdy", myConnection);
                        SqlDataReader dr = cmd1.ExecuteReader();
                        while (dr.Read())
                        {

                            empname = dr["pn_employeename"].ToString();
                            fromdate = Convert.ToDateTime(dr["from_date"]);
                            date1 = fromdate.ToString("dd/MM/yyyy");
                            todate = Convert.ToDateTime(dr["to_date"]);
                            date2 = todate.ToString("dd/MM/yyyy");
                            monthcount = 1;
                            monthcount += Convert.ToInt32(dr["monthcount"].ToString());
                            _Amount = 0;
                            cmd1 = new SqlCommand("set dateformat dmy;select isnull(sum(present_days),0) as Presentdays from payinput where pn_employeeid='" + chk_Empcode.Items[i].Value + "' and ((d_from_date between '" + date1 + "' and '" + date2 + "') or ( d_to_date between '" + date1 + "' and '" + date2 + "') or (d_from_date>='" + date1 + "' and d_to_date<='" + date2 + "') or ( d_from_date<='" + date1 + "' and d_to_date>='" + date2 + "'));set dateformat mdy", myConnection);
                            SqlDataReader datreader = cmd1.ExecuteReader();
                            if (datreader.Read())
                            {
                                presentday = Convert.ToInt32(datreader["Presentdays"]);
                            }
                            cmd1 = new SqlCommand("Select isnull(Sum(Amount),0) as Amount from paym_emp_OverHeading where Pn_employeeid='" + chk_Empcode.Items[i].Value + "'", myConnection);
                            SqlDataReader drr = cmd1.ExecuteReader();
                            if (drr.Read())
                            {
                                _Amount = Convert.ToInt32(drr["Amount"].ToString());
                            }
                            Tot_amt = (double)(((_Amount * 12) / 280) * presentday);

                            sd = date1.Split('/');
                            from_month = Convert.ToInt32(sd[1]);

                            ed = date2.Split('/');
                            to_month = Convert.ToInt32(ed[1]);


                            act_basic = 0; earned_basic = 0;
                            for (int j = from_month; j <= to_month; j++)
                            {

                                _Month = fromdate.ToString("MMMMMMMMMM");
                                _Year = Convert.ToInt32(fromdate.Year);
                                monthyear = _Month + " " + _Year;
                                cmd1 = new SqlCommand("Select isnull(sum(Earn_Act_Amount+Act_Basic),0) as basic from PayOutput_Actuals where Period_Code='" + monthyear + "' and pn_Employeeid='" + chk_Empcode.Items[i].Value + "'", myConnection);
                                SqlDataReader rdr = cmd1.ExecuteReader();
                                if (rdr.Read())
                                {
                                    act_basic += (double)rdr["basic"];

                                }
                                cmd1 = new SqlCommand("Select isnull((Gross_salary),0) as Earned from payoutput_netpay where Period_Code='" + monthyear + "' and pn_Employeeid='" + chk_Empcode.Items[i].Value + "'", myConnection);
                                SqlDataReader sqlrdr = cmd1.ExecuteReader();
                                if (sqlrdr.Read())
                                {

                                    earned_basic += (double)sqlrdr["Earned"];
                                }


                                fromdate = fromdate.AddMonths(+1);
                            }


                            query = "set dateformat dmy;insert into temp_emp_projects values('" + chk_Empcode.Items[i].Value + "', '" + empname + "','" + date1 + "','" + date2 + "','" + presentday + "','" + act_basic + "','" + earned_basic + "','" + Tot_amt + "');set dateformat mdy";
                            employee.fn_reportbyid(query);
                            temp_count++;
                        }
                    }
                }

                if (temp_count > 0)
                {
                    sd = txtFromDate.Text.Split('/');
                    ed = txtToDate.Text.Split('/');
                    int mon = Convert.ToInt32(sd[0]);
                    to_date=DateTime.ParseExact(txtFromDate.Text,"dd/MM/yyyy",null);
                    //todate = Convert.ToDateTime(txtFromDate.Text);
                    _Month = todate.ToString("MMMMMMMMMMMMMMMMM");
                    _Year = Convert.ToInt32(sd[2]);
                    txtFromDate.Text = _Month + " " + (mon + " " + _Year).ToString();

                    mon = Convert.ToInt32(ed[0]);
                    to_date = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null);
                    //todate = Convert.ToDateTime(txtToDate.Text);
                    _Month = todate.ToString("MMMMMMMMMMMMMMMMM");
                    _Year = Convert.ToInt32(ed[2]);
                    txtToDate.Text = _Month + " " + (mon + " " + _Year).ToString();
                    Session["Repordid"] = "Bench";
                    Session["fdate"] = txtFromDate.Text;
                    Session["tdate"] = txtToDate.Text;
                    if (chkoverheading.Checked == true)
                    {
                        Session["ReportName"] = "~/crystalreports/costanalysisreport1.rpt";
                    }
                    else if (chkoverheading.Checked == false)
                    {
                        Session["ReportName"] = "~/crystalreports/CostAnalysisReport.rpt";
                    }
                    Session["preview_page"] = "~/Bench_Cost_Analysis.aspx";
                    Response.Redirect("Report_view.aspx", false);
                }
                else
                {
                    lbl_error.Text = "No Employee Selected";
                }
                myConnection.Close();

            }
            else if (ddl_type.SelectedItem.Text == "Graphical Representation Report")
            {

                query = "delete from temp_emp_projects";
                employee.fn_reportbyid(query);
                myConnection.Open();



                //from_date = Convert.ToDateTime(txtFromDate.Text);
                //to_date = Convert.ToDateTime(txtToDate.Text);

                cmd1 = new SqlCommand("set dateformat dmy;select pn_employeename,from_date,to_date,isnull(datediff(day,from_date,to_date),0) as monthcount from paym_emp_projects where pn_employeeid='" + ddl_employee.SelectedItem.Value + "' and p_name='Bench' and ((from_date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "') or ( to_date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "') or (from_date>='" + txtFromDate.Text + "' and to_date<='" + txtToDate.Text + "') or ( from_date<='" + txtFromDate.Text + "' and to_date>='" + txtToDate.Text + "'));set dateformat mdy", myConnection);
                SqlDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {

                    empname = dr["pn_employeename"].ToString();
                    fromdate = Convert.ToDateTime(dr["from_date"]);
                    date1 = fromdate.ToString("dd/MM/yyyy");
                    todate = Convert.ToDateTime(dr["to_date"]);
                    date2 = todate.ToString("dd/MM/yyyy");
                    monthcount = 1;
                    monthcount += Convert.ToInt32(dr["monthcount"].ToString());
                    _Amount = 0;
                    cmd1 = new SqlCommand("set dateformat dmy;select isnull(sum(present_days),0) as Presentdays from payinput where pn_employeeid='" + empid + "' and ((d_from_date between '" + fromdate + "' and '" + todate + "') or ( d_to_date between '" + fromdate + "' and '" + todate + "') or (d_from_date>='" + fromdate + "' and d_to_date<='" + todate + "') or ( d_from_date<='" + fromdate + "' and d_to_date>='" + todate + "'));set dateformat mdy", myConnection);
                    SqlDataReader datreader1 = cmd1.ExecuteReader();
                    if (datreader1.Read())
                    {
                        presentday = Convert.ToInt32(datreader1["Presentdays"]);
                    }

                    cmd1 = new SqlCommand("Select isnull(Sum(Amount),0) as Amount from paym_emp_OverHeading where Pn_employeeid='" + empid + "'", myConnection);
                    SqlDataReader drr = cmd1.ExecuteReader();
                    if (drr.Read())
                    {
                        _Amount = Convert.ToInt32(drr["Amount"].ToString());
                    }
                    Tot_amt = (double)(((_Amount * 12) / 280) * presentday);
                    sd = date1.Split('/');
                    from_month = Convert.ToInt32(sd[1]);

                    ed = date2.Split('/');
                    to_month = Convert.ToInt32(ed[1]);


                    act_basic = 0; earned_basic = 0;
                    for (int j = from_month; j <= to_month; j++)
                    {

                        _Month = fromdate.ToString("MMMMMMMMMM");
                        _Year = Convert.ToInt32(fromdate.Year);
                        monthyear = _Month + " " + _Year;
                        cmd1 = new SqlCommand("Select isnull(sum(Earn_Act_Amount+Act_Basic),0) as basic from PayOutput_Actuals where Period_Code='" + monthyear + "' and pn_Employeeid='" + ddl_employee.SelectedItem.Value + "'", myConnection);
                        SqlDataReader rdr = cmd1.ExecuteReader();
                        if (rdr.Read())
                        {
                            act_basic += (double)rdr["basic"];

                        }
                        cmd1 = new SqlCommand("Select isnull((Gross_salary),0) as Earned from payoutput_netpay where Period_Code='" + monthyear + "' and pn_Employeeid='" + ddl_employee.SelectedItem.Value + "'", myConnection);
                        SqlDataReader sqlrdr = cmd1.ExecuteReader();
                        if (sqlrdr.Read())
                        {

                            earned_basic += (double)sqlrdr["Earned"];
                        }


                        fromdate = fromdate.AddMonths(+1);
                    }

                }
                query = "set dateformat dmy;insert into temp_emp_projects values('" + ddl_employee.SelectedItem.Value + "', '" + empname + "','" + date1 + "','" + date2 + "','" + presentday + "','" + act_basic + "','" + earned_basic + "','" + Tot_amt + "');set dateformat mdy";
                employee.fn_reportbyid(query);

                sd = txtFromDate.Text.Split('/');
                ed = txtToDate.Text.Split('/');
                int mon = Convert.ToInt32(sd[0]);
                //todate = Convert.ToDateTime(txtFromDate.Text);
                todate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null);
                _Month = todate.ToString("MMMMMMMMMMMMMMMMM");
                _Year = Convert.ToInt32(sd[2]);
                txtFromDate.Text = _Month + " " + (mon + " " + _Year).ToString();

                mon = Convert.ToInt32(ed[0]);
//                todate = Convert.ToDateTime(txtToDate.Text);
                todate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null);
                _Month = todate.ToString("MMMMMMMMMMMMMMMMM");
                _Year = Convert.ToInt32(ed[2]);
                txtToDate.Text = _Month + " " + (mon + " " + _Year).ToString();
                //Session["Repordid"] = "Bench";
                Session["fdate"] = txtFromDate.Text;
                Session["tdate"] = txtToDate.Text;
                if (chkoverheading.Checked == true)
                {
                    Session["ReportName"] = "~/crystalreports/ProfitChart.rpt";
                }
                else if (chkoverheading.Checked == false)
                {
                    Session["ReportName"] = "~/crystalreports/ProfitChart1.rpt";
                }
                Session["preview_page"] = "~/Bench_Cost_Analysis.aspx";
                Response.Redirect("Report_view.aspx", false);

                myConnection.Close();

            }
            else if (ddl_type.SelectedItem.Text == "Bench Report")
            {
                query = "delete from temp_Emp_Projects";
                employee.fn_reportbyid(query);

                myConnection.Open();

                //from_date = Convert.ToDateTime(txtFromDate.Text);
                //to_date = Convert.ToDateTime(txtToDate.Text);

                if (chk_successive.Checked == true)
                {
                    cmd1 = new SqlCommand("set dateformat dmy;select distinct(pn_employeeid) from paym_emp_projects where p_name='Bench' and departmentid='" + ddl_department.SelectedItem.Value + "'  and ((from_date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "') or ( to_date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "') or (from_date>='" + txtFromDate.Text + "' and to_date<='" + txtToDate.Text + "') or ( from_date<='" + txtFromDate.Text + "' and to_date>='" + txtToDate.Text + "')) group by(pn_employeeid) having COUNT(pn_employeeid) >0;set dateformat mdy", myConnection);
                    SqlDataReader dr = cmd1.ExecuteReader();
                    while (dr.Read())
                    {
                        empid = Convert.ToInt32(dr["pn_employeeid"].ToString());
                        cmd1 = new SqlCommand("set dateformat dmy;select isnull(datediff(day,from_date,to_date),0) as daycount,pn_employeename,from_date,to_date,isnull(datediff(day,from_date,to_date),0) as monthcount from paym_emp_projects where pn_employeeid='" + empid + "' and p_name='Bench'  and ((from_date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "') or ( to_date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "') or (from_date>='" + txtFromDate.Text + "' and to_date<='" + txtToDate.Text + "') or ( from_date<='" + txtFromDate.Text + "' and to_date>='" + txtToDate.Text + "'));set dateformat mdy", myConnection);
                        //cmd1 = new SqlCommand("set dateformat dmy;select isnull((sum(datediff(day,from_date,to_date))),0) as daycount from paym_emp_projects where pn_employeeid='" + empid + "' and p_name='Bench' and ((from_date<='" + from_date + "' and to_date<='" + to_date + "') or ( from_date>='" + from_date + "' and to_date>='" + to_date + "'));set dateformat mdy", myConnection);
                        SqlDataReader rdr = cmd1.ExecuteReader();
                        while (rdr.Read())
                        {
                            count = Convert.ToInt32(rdr["daycount"].ToString());
                            daycount = Convert.ToInt32(txt_days.Text);
                            empname = rdr["pn_employeename"].ToString();
                            fromdate = Convert.ToDateTime(rdr["from_date"]);
                            date1 = fromdate.ToString("dd/MM/yyyy");
                            todate = Convert.ToDateTime(rdr["to_date"]);
                            date2 = todate.ToString("dd/MM/yyyy");
                            monthcount = 0;
                            monthcount = Convert.ToInt32(rdr["monthcount"].ToString());
                            daycount = Convert.ToInt32(txt_days.Text);
                            _Amount = 0;
                            cmd1 = new SqlCommand("set dateformat dmy;select isnull(sum(present_days),0) as Presentdays from payinput where pn_employeeid='" + empid + "' and ((d_from_date between '" + fromdate + "' and '" + todate + "') or ( d_to_date between '" + fromdate + "' and '" + todate + "') or (d_from_date>='" + fromdate + "' and d_to_date<='" + todate + "') or ( d_from_date<='" + fromdate + "' and d_to_date>='" + todate + "'));set dateformat mdy", myConnection);
                            SqlDataReader datreader1 = cmd1.ExecuteReader();
                            if (datreader1.Read())
                            {
                                presentday = Convert.ToInt32(datreader1["Presentdays"]);
                            }

                            cmd1 = new SqlCommand("Select isnull(Sum(Amount),0) as Amount from paym_emp_OverHeading where Pn_employeeid='" + empid + "'", myConnection);
                            SqlDataReader drr = cmd1.ExecuteReader();
                            if (drr.Read())
                            {
                                _Amount = Convert.ToInt32(drr["Amount"].ToString());
                            }
                            Tot_amt = (double)(((_Amount * 12) / 280) * presentday);
                            if (count > daycount)
                            {
                                sd = date1.Split('/');
                                from_month = Convert.ToInt32(sd[1]);

                                ed = date2.Split('/');
                                to_month = Convert.ToInt32(ed[1]);


                                act_basic = 0; earned_basic = 0;
                                for (int i = from_month; i <= to_month; i++)
                                {

                                    _Month = fromdate.ToString("MMMMMMMMMM");
                                    _Year = Convert.ToInt32(fromdate.Year);
                                    monthyear = _Month + " " + _Year;
                                    cmd1 = new SqlCommand("Select (act_basic+earn_act_amount) as basic,(Earn_Amount + earned_basic) as Earned from payoutput_netpay where Period_Code='" + monthyear + "' and pn_Employeeid='" + empid + "'", myConnection);
                                    SqlDataReader sqlrdr = cmd1.ExecuteReader();
                                    if (sqlrdr.Read())
                                    {
                                        act_basic += (double)sqlrdr["basic"];
                                        earned_basic += (double)sqlrdr["Earned"];
                                    }

                                    fromdate = fromdate.AddMonths(+1);
                                }

                                query = "set dateformat dmy;insert into temp_Emp_Projects values('" + empid + "','" + empname + "','" + date1 + "','" + date2 + "','" + presentday + "','" + act_basic + "','" + earned_basic + "','" + Tot_amt + "');set dateformat mdy";
                                employee.fn_reportbyid(query);
                            }
                        }
                        rdr.Close();
                    }
                    dr.Close();
                }
                else if (chk_successive.Checked == false)
                {
                    cmd1 = new SqlCommand("select distinct(pn_employeeid) from paym_emp_projects where p_name='Bench' and departmentid='" + ddl_department.SelectedItem.Value + "'", myConnection);
                    SqlDataReader dr = cmd1.ExecuteReader();
                    while (dr.Read())
                    {
                        empid = Convert.ToInt32(dr["pn_employeeid"].ToString());
                        //cmd1 = new SqlCommand("set dateformat dmy;select(sum(datediff(day,from_date,to_date))) as daycount from paym_emp_projects where pn_employeeid='" + empid + "' and p_name='Bench' and ((from_date<='" + from_date + "' and to_date<='" + to_date + "') or ( from_date>='" + from_date + "' and to_date>='" + to_date + "'));set dateformat mdy", myConnection);
                        cmd1 = new SqlCommand("set dateformat dmy;select isnull((sum(datediff(day,from_date,to_date))),0) as daycount from paym_emp_projects where pn_employeeid='" + empid + "' and p_name='Bench' and ((from_date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "') or ( to_date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "') or (from_date>='" + txtFromDate.Text + "' and to_date<='" + txtToDate.Text + "') or ( from_date<='" + txtFromDate.Text + "' and to_date>='" + txtToDate.Text + "'));set dateformat mdy", myConnection);
                        SqlDataReader rdr = cmd1.ExecuteReader();
                        while (rdr.Read())
                        {
                            count = Convert.ToInt32(rdr["daycount"].ToString());

                            daycount = Convert.ToInt32(txt_days.Text);
                           
                            if (count > daycount)
                            {
                                cmd1 = new SqlCommand("set dateformat dmy;select pn_employeeid,pn_employeename,from_date,to_date from paym_emp_projects where pn_employeeid='" + empid + "' and p_name='Bench' and ((from_date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "') or ( to_date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "') or (from_date>='" + txtFromDate.Text + "' and to_date<='" + txtToDate.Text + "') or ( from_date<='" + txtFromDate.Text + "' and to_date>='" + txtToDate.Text + "'));set dateformat mdy", myConnection);
                                //cmd1 = new SqlCommand("set dateformat dmy;select pn_employeeid,pn_employeename,from_date,to_date from paym_emp_projects where pn_employeeid='" + empid + "' and p_name='Bench'and ((from_date<='" + from_date + "' and to_date<='" + to_date + "') or ( from_date>='" + from_date + "' and to_date>='" + to_date + "'));set dateformat mdy", myConnection);
                                SqlDataReader drearder = cmd1.ExecuteReader();
                                while (drearder.Read())
                                {
                                    empid = Convert.ToInt32(drearder["pn_employeeid"].ToString());
                                    empname = drearder["pn_employeename"].ToString();
                                    fromdate = Convert.ToDateTime(drearder["from_date"]);
                                    date1 = fromdate.ToString("dd/MM/yyyy");
                                    todate = Convert.ToDateTime(drearder["to_date"]);
                                    date2 = todate.ToString("dd/MM/yyyy");
                                    presentday = 0;
                                    cmd1 = new SqlCommand("set dateformat dmy;select isnull(sum(present_days),0) as Presentdays from payinput where pn_employeeid='" + empid + "' and ((d_from_date between '" + date1 + "' and '" + date2 + "') or ( d_to_date between '" + date1 + "' and '" + date2 + "') or (d_from_date>='" + date1 + "' and d_to_date<='" + date2 + "') or ( d_from_date<='" + date1 + "' and d_to_date>='" + date2 + "'));set dateformat mdy", myConnection);
                                    SqlDataReader datreader2 = cmd1.ExecuteReader();
                                    if (datreader2.Read())
                                    {
                                        presentday = Convert.ToInt32(datreader2["Presentdays"]);
                                    }
                                    _Amount = 0;
                                    cmd1 = new SqlCommand("Select isnull(Sum(Amount),0) as Amount from paym_emp_OverHeading where Pn_employeeid='" + empid + "'", myConnection);
                                    SqlDataReader drr = cmd1.ExecuteReader();
                                    if (drr.Read())
                                    {
                                        _Amount = Convert.ToInt32(drr["Amount"].ToString());
                                    }
                                    Tot_amt = (double)(((_Amount * 12) / 280) * presentday);
                                    sd = date1.Split('/');
                                    from_month = Convert.ToInt32(sd[1]);

                                    ed = date2.Split('/');
                                    to_month = Convert.ToInt32(ed[1]);

                                    act_basic = 0; earned_basic = 0;
                                    for (int i = from_month; i <= to_month; i++)
                                    {

                                        _Month = fromdate.ToString("MMMMMMMMMM");
                                        _Year = Convert.ToInt32(fromdate.Year);
                                        monthyear = _Month + " " + _Year;
                                        cmd1 = new SqlCommand("Select (act_basic+earn_act_amount) as basic,(Earn_Amount + earned_basic) as Earned from payoutput_netpay where Period_Code='" + monthyear + "' and pn_Employeeid='" + empid + "'", myConnection);
                                        SqlDataReader sqlrdr = cmd1.ExecuteReader();
                                        if (sqlrdr.Read())
                                        {
                                            act_basic += (double)sqlrdr["basic"];
                                            earned_basic += (double)sqlrdr["Earned"];
                                        }

                                        fromdate = fromdate.AddMonths(+1);
                                    }

                                    query = "set dateformat dmy;insert into temp_Emp_Projects values('" + empid + "','" + empname + "','" + date1 + "','" + date2 + "','" + presentday + "','" + act_basic + "','" + earned_basic + "','" + Tot_amt + "');set dateformat mdy";
                                    employee.fn_reportbyid(query);
                                }
                                drearder.Close();
                            }
                        }
                        rdr.Close();

                    }
                    dr.Close();
                }
                sd = txtFromDate.Text.Split('/');
                ed = txtToDate.Text.Split('/');
                int mon1 = Convert.ToInt32(sd[0]);

                to_date = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null);

                //todate = Convert.ToDateTime(txtFromDate.Text);
                _Month = to_date.ToString("MMMMMMMMMMMMMMMMM");
                _Year = Convert.ToInt32(sd[2]);
                txtFromDate.Text = _Month + " " + (mon1 + " " + _Year).ToString();

                mon1 = Convert.ToInt32(ed[0]);
                to_date = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null);
                //todate = Convert.ToDateTime(txtToDate.Text);
                _Month = to_date.ToString("MMMMMMMMMMMMMMMMM");
                _Year = Convert.ToInt32(ed[2]);
                txtToDate.Text = _Month + " " + (mon1 + " " + _Year).ToString();

                Session["Repordid"] = "Bench";
                Session["fdate"] = txtFromDate.Text;
                Session["tdate"] = txtToDate.Text;
                Session["preview_page"] = "~/ProjectReport.aspx";
                if (chkoverheading.Checked == true)
                {
                    Session["ReportName"] = "~/crystalreports/ProjectRpt1.rpt";
                }
                else if (chkoverheading.Checked == false)
                {
                    Session["ReportName"] = "~/crystalreports/ProjectRpt.rpt";
                }
                Response.Redirect("Report_view.aspx", false);
            }

        }
        catch (Exception ex)
        {
            lbl_error.Text = ex.ToString();
        }

    }
    protected void ddl_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_department.Items.Clear();
        chk_Empcode.Items.Clear();
        lbl_selectemp.Visible = false;
        chkall.Visible = false;
        div_chkempcode.Visible = false;
        chkall.Checked = false;
        chkoverheading.Checked = false;
        chk_successive.Checked = false;
        ddl_employee.Items.Clear();
        txt_days.Text = "";
        txtFromDate.Text = "";
        txtToDate.Text = "";
        lbl_error.Text = "Welcome to Report Section";
        if (ddl_type.SelectedItem.Text == "Cost Analaysis Report")
        {
            ddl_department_load1();
            div_chk_Empcode.Visible = true;
            Tr2.Visible = false;
            Tr3.Visible = false;
        }
        else if (ddl_type.SelectedItem.Text == "Bench Report")
        {

            ddl_department_load1();
            div_chk_Empcode.Visible = false;
            Tr2.Visible = true;
            Tr3.Visible = false;
        }
        else if (ddl_type.SelectedItem.Text == "Graphical Representation Report")
        {
            ddl_department_load1();
            div_chk_Empcode.Visible = true;
            Tr2.Visible = false;
            ddl_employee.Visible = true;
            lbl_Emp.Visible = true;
            Tr3.Visible = false;
        }

    }
    protected void ddl_employee_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}