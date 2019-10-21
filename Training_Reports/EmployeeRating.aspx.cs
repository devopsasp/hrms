using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
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
using System.Data.SqlClient;
using ePayHrms.Leave;

public partial class Training_Reports_EmployeeRating : System.Web.UI.Page
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
    Collection<Employee> prgmnameList;

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
    string from_date, to_date, pgm_name, pgm_trainer_id; int rating;
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
                    prgm();
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
    public void prgm()
    {

        ddl_prgmtype.Items.Clear();
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        if (s_login_role == "a")
        {
            //prgmnameList = employee.fn_programname(Convert.ToInt32(ddl_Branch.SelectedItem.Value));
        }
        else if (s_login_role == "h")
        {
            prgmnameList = employee.fn_programname1(employee);
        }

        if (prgmnameList.Count > 0)
        {
            for (ddl_i = -1; ddl_i < prgmnameList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();
                    e_list.Text = "Select";
                    //e_list.Value = "0";
                    ddl_prgmtype.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();
                    //e_list.Value = prgmnameList[ddl_i].DepartmentId.ToString();
                    e_list.Text = prgmnameList[ddl_i].prgmname.ToString();
                    ddl_prgmtype.Items.Add(e_list);
                }
            }
        }
        else
        {

            ClientScriptManager manager = Page.ClientScript;
            manager.RegisterStartupScript(this.GetType(), "Call", "show_Error();", true);
        }
    }
    protected void ddl_Inst_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btn_emp_report_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            mycon.Open();

            cmd = new SqlCommand("delete from Temp_ratingTrainer", mycon);
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("select distinct(a.rating) as Rating,a.pn_employeeid,a.Trainer_name,b.Employee_First_Name from Training_Emp_Feedback a,paym_employee b where a.pn_employeeid=b.pn_employeeid and pgm_name='" + ddl_prgmtype.SelectedItem.Text + "' ", mycon);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmd = new SqlCommand("Insert into Temp_ratingTrainer values('" + dr[0].ToString() + "','" + dr[1].ToString() + "','" + dr[2].ToString() + "','" + dr[3].ToString() + "','" + ddl_prgmtype.SelectedItem.Text + "')", mycon);
                int c = cmd.ExecuteNonQuery();
            }
            dr.Close();
            Session["ReportName"] = "~/crystalreports/EmployRating.rpt";
            Session["preview_page"] = "~/EmployeeRating.aspx";
            Response.Redirect("~/PayrollReports/Report_view.aspx", false);
            mycon.Close();
        }
        catch (Exception ex) { }
    }
}
