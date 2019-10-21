using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ePayHrms.Login;
using ePayHrms.BE.Recruitment;
using ePayHrms.Connection;
using ePayHrms.Candidate;
using System.IO;
using ePayHrms.Company;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ePayHrms.Leave;
using ePayHrms.Employee;
using CrystalDecisions.Web;
using CrystalDecisions.Enterprise;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.Reporting;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;

public partial class PayrollReports_ProjectReport : System.Web.UI.Page
{
    ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand cmd1 = new SqlCommand();
    SqlDataReader rea;
    SqlDataAdapter ada = new SqlDataAdapter();
    DataTable dtable = new DataTable();
    Company company = new Company();
    Employee employee = new Employee();
    Leave l = new Leave();

    Collection<Leave> LeaveList;
    Collection<Company> CompanyList;
    Collection<Employee> EmployeeList;
    Collection<Company> ddlBranchsList;
    Collection<Employee> EmpProfileList;
    char s_login_role;
    int empid, count, s, n;
    string query;
    string[] sd, ed;
    string empname;
    DateTime fromdate, todate;
    DateTime from_date, to_date;
    int daycount;
    string date1, date2;
    int from_month, to_month;
    int _Year;
    string _Month, monthyear;
    double act_basic = 0, earned_basic = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Msg_session"] = "";
        Session["Repordid"] = "";
        Session["fdate"] = "";
        Session["tdate"] = "";
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        Session["formulaBonus"] = "";
        l.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        s_login_role = Convert.ToChar(Request.Cookies["Login_temp_Role"].Value);

        if (!IsPostBack)
        {

            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

                switch (s_login_role)
                {


                    case 'h':
                        ddl_department_load();

                        break;

                    case 'e':
                        l.EmployeeID = (int)Session["Login_temp_EmployeeID"];

                        break;

                }
            }
        }
    }

    public void ddl_department_load()
    {

        
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
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void btn_Report_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            query = "delete from temp_Emp_Projects";
            employee.fn_reportbyid(query);

            myConnection.Open();

            from_date = Convert.ToDateTime(txt_from_date.Text);
            to_date = Convert.ToDateTime(txt_to_date.Text);

            if (chk_successive.Checked == true)
            {
                cmd1 = new SqlCommand("set dateformat dmy;select distinct(pn_employeeid) from paym_emp_projects where p_name='Bench' and departmentid='" + ddl_department.SelectedItem.Value + "' and ((from_date between '" + from_date + "' and '" + to_date + "') or ( to_date between '" + from_date + "' and '" + to_date + "') or (from_date>='" + from_date + "' and to_date<='" + to_date + "') or ( from_date<='" + from_date + "' and to_date>='" + to_date + "')) group by(pn_employeeid) having COUNT(pn_employeeid) <= 1;set dateformat mdy", myConnection);
                SqlDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    empid = Convert.ToInt32(dr["pn_employeeid"].ToString());
                    cmd1 = new SqlCommand("set dateformat dmy;select isnull((sum(datediff(day,from_date,to_date))),0) as daycount from paym_emp_projects where pn_employeeid='" + empid + "' and p_name='Bench' and ((from_date between '" + from_date + "' and '" + to_date + "') or ( to_date between '" + from_date + "' and '" + to_date + "') or (from_date>='" + from_date + "' and to_date<='" + to_date + "') or ( from_date<='" + from_date + "' and to_date>='" + to_date + "'));set dateformat mdy", myConnection);
                    //cmd1 = new SqlCommand("set dateformat dmy;select isnull((sum(datediff(day,from_date,to_date))),0) as daycount from paym_emp_projects where pn_employeeid='" + empid + "' and p_name='Bench' and ((from_date<='" + from_date + "' and to_date<='" + to_date + "') or ( from_date>='" + from_date + "' and to_date>='" + to_date + "'));set dateformat mdy", myConnection);
                    SqlDataReader rdr = cmd1.ExecuteReader();
                    while (rdr.Read())
                    {
                        count = Convert.ToInt32(rdr["daycount"].ToString());
                        daycount = Convert.ToInt32(txt_days.Text);
                        if (count > daycount)
                        {
                            cmd1 = new SqlCommand("set dateformat dmy;select pn_employeename,from_date,to_date from paym_emp_projects where pn_employeeid='" + empid + "' and p_name='Bench' and ((from_date between '" + from_date + "' and '" + to_date + "') or ( to_date between '" + from_date + "' and '" + to_date + "') or (from_date>='" + from_date + "' and to_date<='" + to_date + "') or ( from_date<='" + from_date + "' and to_date>='" + to_date + "'));set dateformat mdy", myConnection);
                            //cmd1 = new SqlCommand("set dateformat dmy;select pn_employeename,from_date,to_date from paym_emp_projects where pn_employeeid='" + empid + "' and p_name='Bench' and ((from_date<='" + from_date + "' and to_date<='" + to_date + "') or ( from_date>='" + from_date + "' and to_date>='" + to_date + "'));set dateformat mdy", myConnection);
                            SqlDataReader drearder = cmd1.ExecuteReader();
                            while (drearder.Read())
                            {
                                empname = drearder["pn_employeename"].ToString();
                                fromdate = Convert.ToDateTime(drearder["from_date"]);
                                date1 = fromdate.ToString("dd/MM/yyyy");
                                todate = Convert.ToDateTime(drearder["to_date"]);
                                date2 = todate.ToString("dd/MM/yyyy");

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

                                query = "set dateformat dmy;insert into temp_Emp_Projects values('" + empid + "','" + empname + "','" + date1 + "','" + date2 + "','" + act_basic + "','" + earned_basic + "');set dateformat mdy";
                                employee.fn_reportbyid(query);
                            }
                            drearder.Close();
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
                    cmd1 = new SqlCommand("set dateformat dmy;select isnull((sum(datediff(day,from_date,to_date))),0) as daycount from paym_emp_projects where pn_employeeid='" + empid + "' and p_name='Bench' and ((from_date between '" + from_date + "' and '" + to_date + "') or ( to_date between '" + from_date + "' and '" + to_date + "') or (from_date>='" + from_date + "' and to_date<='" + to_date + "') or ( from_date<='" + from_date + "' and to_date>='" + to_date + "'));set dateformat mdy", myConnection);
                    SqlDataReader rdr = cmd1.ExecuteReader();
                    while (rdr.Read())
                    {
                        count = Convert.ToInt32(rdr["daycount"].ToString());
                        daycount = Convert.ToInt32(txt_days.Text);
                        if (count > daycount)
                        {
                            cmd1 = new SqlCommand("set dateformat dmy;select pn_employeeid,pn_employeename,from_date,to_date from paym_emp_projects where pn_employeeid='" + empid + "' and p_name='Bench'and ((from_date between '" + from_date + "' and '" + to_date + "') or ( to_date between '" + from_date + "' and '" + to_date + "') or (from_date>='" + from_date + "' and to_date<='" + to_date + "') or ( from_date<='" + from_date + "' and to_date>='" + to_date + "'));set dateformat mdy", myConnection);
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

                                query = "set dateformat dmy;insert into temp_Emp_Projects values('" + empid + "','" + empname + "','" + date1 + "','" + date2 + "','" + act_basic + "','" + earned_basic + "');set dateformat mdy";
                                employee.fn_reportbyid(query);
                            }
                            drearder.Close();
                        }
                    }
                    rdr.Close();

                }
                dr.Close();
            }
           

            Session["Repordid"] = "Bench";
            Session["fdate"] = txt_from_date.Text;
            Session["tdate"] = txt_to_date.Text;
            Session["preview_page"] = "~/ProjectReport.aspx";
            Session["ReportName"] = "~/crystalreports/ProjectRpt.rpt";
            Response.Redirect("Report_view.aspx",false);
            
        }
        catch(Exception ex)
        {
        }
    }
}