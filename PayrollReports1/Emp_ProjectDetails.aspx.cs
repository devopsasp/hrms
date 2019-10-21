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

public partial class PayrollReports_Emp_ProjectDetails : System.Web.UI.Page
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
    SqlDataAdapter da = new SqlDataAdapter();
    Collection<Company> CompanyList, ddlBranchsList;
    Collection<Employee> DepartmentList;
    Collection<Employee> EmployeeList;
    Collection<PayRoll> PayList;
    string s_login_role;
    int i = 0, j, temp_count = 0;
    int ddl_i = 0;
    string query = "";
    DateTime from_date, to_date;
    int empid, count;
    int daycount;
    string empname, date1, date2, _Month, monthyear, p_name;
    string[] sd, ed;
    string s_form = "";
    int from_month, to_month, _Year;
    double act_basic, earned_basic, _Amount, Tot_amt, overheading;
    DateTime fromdate, todate;
    DataSet ds_userrights;
    int monthcount = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        Session["Repordid"] = "";
        Session["fdate"] = "";
        Session["tdate"] = "";

        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        c.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);

        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        s_login_role = Request.Cookies["Login_temp_Role"].Value;

        if (!IsPostBack)
        {
            CompanyList = company.fn_getCompany();
            ListItem li = new ListItem();
            if (CompanyList.Count > 0)
            {
                switch (s_login_role)
                {
                    case "a":
                        tbl_pfreport.Visible = false;
                        ddl_Branch.Visible = true;
                        break;

                    case "h":
                        ddl_Branch.Visible = false;
                        tbl_pfreport.Visible = true;
                        ddl_department_load1();

                        break;

                    case "u": s_form = "81";
                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            ddl_Branch.Visible = false;
                            li = new ListItem();
                            li.Text = Request.Cookies["EmpCodeName"].Value;
                            li.Value = Request.Cookies["Login_temp_EmployeeID"].Value;
                            li.Selected = true;
                            chk_Empcode.Items.Add(li);
                            chk_Empcode.Enabled = false;
                            lbl_selectemp.Visible = false;
                            chkall.Visible = false;
                            ddl_department_load1();
                        }
                        else
                        {
                            Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator.";
                            Response.Redirect("~/Company_Home.aspx");
                        }
                        break;

                    case "e":
                        ddl_Branch.Visible = false;
                        li = new ListItem();
                        li.Text = Request.Cookies["EmpCodeName"].Value;
                        li.Value = Request.Cookies["Login_temp_EmployeeID"].Value;
                        li.Selected = true;
                        chk_Empcode.Items.Add(li);
                        chk_Empcode.Enabled = false;
                        lbl_selectemp.Visible = false;
                        chkall.Visible = false;
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

    public void ddl_department_load1()
    {
        ddl_department.Items.Clear();
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        if (s_login_role == "a")
        {
            DepartmentList = employee.fn_getDepartmentList1(Convert.ToInt32(ddl_Branch.SelectedItem.Value));
        }
        else if (s_login_role == "h")
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
                    ddl_Employee.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();
                    e_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
                    e_list.Text = EmployeeList[ddl_i].LastName.ToString();
                    ddl_Employee.Items.Add(e_list);
                }
            }
        }
        else
        {
            ddl_Employee.Items.Add("Select");
        }
    }
    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlEmployee_load();
    }
    protected void btn_Report_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            query = "delete from temp_empproject";
            employee.fn_reportbyid(query);
            myConnection.Open();

           // date1 = txtFromDate.Text;
           // date2 = txtToDate.Text;

            
            act_basic = 0; earned_basic = 0; Tot_amt = 0;
            _Amount = 0;


            cmd1 = new SqlCommand("set dateformat dmy;select Pn_EmployeeName,from_date,to_date,P_name from paym_emp_projects where pn_employeeid='" + ddl_Employee.SelectedItem.Value + "' and pn_companyid='" + employee.CompanyId + "' and pn_Branchid='" + employee.BranchId + "' and ((from_date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "') or ( to_date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "') or (from_date>='" + txtFromDate.Text + "' and to_date<='" + txtToDate.Text + "') or ( from_date<='" + txtFromDate.Text + "' and to_date>='" + txtToDate.Text + "'));set dateformat mdy", myConnection);
            SqlDataReader dreader = cmd1.ExecuteReader();
            while (dreader.Read())
            {
                empname = dreader["Pn_EmployeeName"].ToString();
                fromdate = Convert.ToDateTime(dreader["from_date"]);
                date1 = fromdate.ToString("dd/MM/yyyy");
                todate = Convert.ToDateTime(dreader["to_date"]);
                date2 = todate.ToString("dd/MM/yyyy");
                p_name = dreader["P_Name"].ToString();

                sd = date1.Split('/');
                from_month = Convert.ToInt32(sd[1]);

                ed = date2.Split('/');
                to_month = Convert.ToInt32(ed[1]);
                earned_basic = 0; Tot_amt = 0;
                for (int j = from_month; j <= to_month; j++)
                {

                    _Month = fromdate.ToString("MMMMMMMMMM");
                    _Year = Convert.ToInt32(fromdate.Year);
                    monthyear = _Month + " " + _Year;

                    cmd1 = new SqlCommand("Select isnull((Gross_salary),0) as Earned from payoutput_netpay where Period_Code='" + monthyear + "' and pn_Employeeid='" + ddl_Employee.SelectedItem.Value + "'", myConnection);
                    SqlDataReader sqlrdr = cmd1.ExecuteReader();
                    if (sqlrdr.Read())
                    {
                        earned_basic += (double)sqlrdr["Earned"];
                    }
                    if (chkoverheading.Checked == false)
                    {
                        Tot_amt = earned_basic;
                    }
                    else if (chkoverheading.Checked == true)
                    {
                        cmd1 = new SqlCommand("Select isnull(sum(Amount),0) as Amount from paym_emp_OverHeading where pn_employeeid='" + ddl_Employee.SelectedItem.Value + "' and pn_companyid='" + employee.CompanyId + "' and pn_Branchid='" + employee.BranchId + "'", myConnection);
                        SqlDataReader rdr = cmd1.ExecuteReader();
                        if (rdr.Read())
                        {
                            overheading += Convert.ToDouble(rdr["Amount"].ToString());
                        }
                        Tot_amt = earned_basic + overheading;
                    }

                    fromdate = fromdate.AddMonths(+1);
                }
                query = "set dateformat dmy;insert into temp_empproject values('" + ddl_Employee.SelectedItem.Value + "', '" + empname + "','" + date1 + "','" + date2 + "','" + p_name + "','" + earned_basic + "','" + overheading + "','" + Tot_amt + "');set dateformat mdy";
                employee.fn_reportbyid(query);
            }

            sd = txtFromDate.Text.Split('/');
            ed = txtToDate.Text.Split('/');
            int mon = Convert.ToInt32(sd[0]);
            todate = Convert.ToDateTime(txtFromDate.Text);
            _Month = todate.ToString("MMMMMMMMMMMMMMMMM");
            _Year = Convert.ToInt32(sd[2]);
            txtFromDate.Text = _Month + " " + (mon + " " + _Year).ToString();

            mon = Convert.ToInt32(ed[0]);
            todate = Convert.ToDateTime(txtToDate.Text);
            _Month = todate.ToString("MMMMMMMMMMMMMMMMM");
            _Year = Convert.ToInt32(ed[2]);
            txtToDate.Text = _Month + " " + (mon + " " + _Year).ToString();
            
            if (chkoverheading.Checked == true)
            {
                Session["ReportName"] = "~/crystalreports/empproject1.rpt";
            }
            else if (chkoverheading.Checked == false)
            {
                Session["ReportName"] = "~/crystalreports/emp_project.rpt";
            }
            Session["preview_page"] = "~/ProjectDetail.aspx";
            Response.Redirect("Report_view.aspx", false);

            myConnection.Close();
        }
        catch (Exception ex)
        {
        }

    }
}