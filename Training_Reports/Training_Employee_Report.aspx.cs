using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
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
using System.Data.SqlClient;
using ePayHrms.Leave;


public partial class Training_Reports_Training_Employee_Report : System.Web.UI.Page
{

    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    private SqlConnection _connection;
    SqlConnection mycon = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    //private SqlConnection _con;
    //ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlCommand cmd = new SqlCommand();
    Company company = new Company();
    Employee employee = new Employee();
    Leave l = new Leave();
    PayRoll pay = new PayRoll();

    Collection<Leave> LeaveList;
    Collection<Leave> LeaveGridList;
    Collection<Leave> LeaveMasterList;

    Collection<Company> CompanyList;
    Collection<Employee> EmployeeList;
    Collection<Company> ddlBranchsList;

    Be_Recruitment r = new Be_Recruitment();

    Candidate c = new Candidate();


    Collection<Employee> EmployeesList;
    Collection<Employee> emp_ID_List;
    Collection<Employee> emp_available;
    Collection<Employee> EmpFirstList;
    Collection<Employee> EmpGeneralList;

    Collection<PayRoll> emp_edu_List;
    Collection<PayRoll> Empty_gridList;
    Collection<PayRoll> EpsList;
    Collection<PayRoll> epflist;
    string str_query, ann_leave = "", date;
    int ddl_i, max = 0, month, day;
    int i, yr_it, cur_yr, mon, dat, pr_emp;
    string _Value, _value, _data, dt, mn, yr, dob_edit, default_sqldate = "01/01/1900";
    double basic = 0.0; double net_Pay = 0.0, earn_basic, final_amt = 0.0;
    string s_login_role;
    int presentDays;
    double grauity, basicpay, da, service, extra_salary, deduct_salary;
    string s_form = "";
    DataSet ds_userrights;
    string from_date, to_date, pgm_name, pgm_trainer_id;int rating;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        l.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        lbl_Error.Text = "";
        // Error.Text = "";

        if (!IsPostBack)
        {
            switch (s_login_role)
            {
                case "a": //load();
                    break;

                case "h":
                    // rounds();
                    //gridload();
                    ddl_department_load();
                    //load();
                    // access();
                    break;

                case "u": s_form = "30";
                    ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                    if (ds_userrights.Tables[0].Rows.Count > 0)
                    {
                        //load();
                    }
                    else
                    {
                        Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator.";
                        Response.Redirect("~/Company_Home.aspx");
                    }
                    break;

                default: Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                    Response.Redirect("~/Company_Home.aspx");
                    break;
            }
        }
    }
    protected void ddl_dept_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_employee_load();
    }
    public void ddl_department_load()
    {
        ddl_dept.Items.Clear();
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
                    ddl_dept.Items.Add(list);
                }
                else
                {
                    ListItem list = new ListItem();

                    list.Text = EmployeeList[ddl_i].DepartmentName;
                    list.Value = EmployeeList[ddl_i].DepartmentId.ToString();
                    ddl_dept.Items.Add(list);
                }
            }
        }
        else
        {
            lbl_Error.Text = "No Department Available";
        }

    }
    public void ddl_employee_load()
    {
        //employee dropdown
        ddl_employee.Items.Clear();

        if (s_login_role == "a")
        {
            employee.BranchId = (int)ViewState["Appraisal_BranchID"];
        }

        if (s_login_role == "h")
        {
            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        }

        str_query = "Select a.pn_EmployeeID, a.EmployeeCode, a.Employee_First_Name from paym_employee a, paym_employee_profile1 b where a.pn_CompanyID=b.pn_CompanyID and a.pn_BranchID=b.pn_BranchID and a.pn_EmployeeID=b.pn_EmployeeID and b.pn_DepartmentID=" + ddl_dept.SelectedValue + " and b.pn_CompanyID=" + employee.CompanyId + " and a.status='y' and b.pn_BranchID=" + employee.BranchId;

        EmployeeList = employee.fn_getEmplist(str_query);

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
            lbl_Error.Text = "No Employee";
        }
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btn_emp_report_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            mycon.Open();
            cmd = new SqlCommand("delete from temp_emp_training", mycon);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("set dateformat dmy;select v_durationfrom,v_durationto,ProgramName,TrainerID,rating from paym_training_new where pn_employeeid=" + ddl_employee.SelectedValue + " and ((v_durationfrom between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "') or ( v_durationto between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "') or (v_durationfrom>='" + txtFromDate.Text + "' and v_durationto<='" + txtToDate.Text + "') or ( v_durationfrom<='" + txtFromDate.Text + "' and v_durationto>='" + txtToDate.Text + "'));set dateformat mdy", mycon);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                from_date = rdr["v_durationfrom"].ToString();
                to_date = rdr["v_durationto"].ToString();
                pgm_name = rdr["fn_pgrmname"].ToString();
               // pgm_trainer_id = Convert.ToInt32(rdr["fn_pgmtrnrNameID"]);
                rating = Convert.ToInt32(rdr["rating"]);
                cmd = new SqlCommand("select b.fname as trainername from paym_training_new a,trainer_profile1 b where a.TrainerId=b.trainer_id and a.pn_employeeid=" + ddl_employee.SelectedValue + " and a.ProgramName='" + pgm_name + "'", mycon);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    pgm_trainer_id = dr["trainername"].ToString();
                }
                cmd = new SqlCommand("set dateformat dmy;insert into temp_emp_training values('" + ddl_employee.SelectedItem.Text + "','" + from_date + "','" + to_date + "','" + pgm_name + "','" + pgm_trainer_id + "','" + rating + "','" + txtFromDate.Text + "','" + txtToDate.Text + "');set dateformat mdy;", mycon);
                cmd.ExecuteNonQuery();
            }
            Session["ReportName"] = "~/crystalreports/Training_Emp_Performance.rpt";
            Session["preview_page"] = "~/Training_Employee_Report.aspx";
            Response.Redirect("~/PayrollReports/Report_view.aspx", false);
            mycon.Close();
        }
        catch (Exception ex)
        {
        }
    }
}
