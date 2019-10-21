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
public partial class Training_Reports_TrainingExpense : System.Web.UI.Page
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
                    //gridload();
                    
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
    public void ddl_Institution_load()
    {
        ddl_Inst.Items.Clear();
        EmployeeList = employee.fn_getInstitution(employee.BranchId);
        if (EmployeeList.Count > 0)
        {

            for (int ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem list = new ListItem();

                    list.Text = "Select Institution";
                    list.Value = "sd";
                    ddl_Inst.Items.Add(list);
                }
                else
                {
                    ListItem list = new ListItem();

                    list.Text = EmployeeList[ddl_i].DepartmentName;
                    list.Value = EmployeeList[ddl_i].DepartmentId.ToString();
                    ddl_Inst.Items.Add(list);
                }
            }
        }
        else
        {
            lbl_Error.Text = "No Department Available";
        }

    }
    public void ddl_Trainer_load()
    {
        ddl_trainer.Items.Clear();
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        employee.InstitutionId = Convert.ToInt32(ddl_Inst.SelectedValue);
        EmployeeList = employee.fn_gettrainer(employee);
        if (EmployeeList.Count > 0)
        {

            for (int ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem list = new ListItem();

                    list.Text = "Select Trainer";
                    list.Value = "sd";
                    ddl_trainer.Items.Add(list);
                }
                else
                {
                    ListItem list = new ListItem();

                    list.Text = EmployeeList[ddl_i].DepartmentName;
                    list.Value = EmployeeList[ddl_i].DepartmentId.ToString();
                    ddl_trainer.Items.Add(list);
                }
            }
        }
        else
        {
            lbl_Error.Text = "No Department Available";
        }

    }

    public void prgm()
    {

        ddlPgm.Items.Clear();
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        if (s_login_role == "a")
        {
            //prgmnameList = employee.fn_programname(Convert.ToInt32(ddl_Branch.SelectedItem.Value));
        }
        else if (s_login_role == "h")
        {
            EmployeeList = employee.fn_programname1(employee);
        }

        if (EmployeeList.Count > 0)
        {
            for (ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();
                    e_list.Text = "Select";
                    //e_list.Value = "0";
                    ddlPgm.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();
                    //e_list.Value = prgmnameList[ddl_i].DepartmentId.ToString();
                    e_list.Text = EmployeeList[ddl_i].prgmname.ToString();
                    ddlPgm.Items.Add(e_list);
                }
            }
        }
        else
        {

            ClientScriptManager manager = Page.ClientScript;
            manager.RegisterStartupScript(this.GetType(), "Call", "show_Error();", true);
        }
    }
    protected void btn_emp_report_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            mycon.Open();
            if (ddl_select.SelectedItem.Text == "Institution")
            {

                cmd = new SqlCommand("delete from temp_training_expense", mycon);
                int c = cmd.ExecuteNonQuery();
                cmd = new SqlCommand("set dateformat dmy;select distinct a.Fname,a.Specification,a.ptype,b.ProgramName,b.TrainingCost from trainer_profile1 a,paym_training_new b where a.trainer_id=b.TrainerID and b.instid='" + ddl_Inst.SelectedValue + "' and ((v_durationfrom between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "') or ( v_durationto between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "') or (v_durationfrom>='" + txtFromDate.Text + "' and v_durationto<='" + txtToDate.Text + "') or ( v_durationfrom<='" + txtFromDate.Text + "' and v_durationto>='" + txtToDate.Text + "'));set dateformat mdy", mycon);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cmd = new SqlCommand("set dateformat dmy;insert into temp_training_expense values('" + dr[0].ToString() + "','" + ddl_Inst.SelectedItem.Text + "','" + dr[1].ToString() + "','" + dr[2].ToString() + "','" + dr[3].ToString() + "','" + dr[4].ToString() + "','" + txtFromDate.Text + "','" + txtToDate.Text + "')set dateformat mdy;", mycon);
                    c = cmd.ExecuteNonQuery();
                }
                dr.Close();
                Session["ReportName"] = "~/crystalreports/InstExpense.rpt";
                Session["preview_page"] = "~/Training_Reports/TrainingExpense.aspx";
                Response.Redirect("~/PayrollReports/Report_view.aspx", false);
            }
            else if (ddl_select.SelectedItem.Text == "Trainer")
            {

                cmd = new SqlCommand("delete from temp_training_expense", mycon);
                int c = cmd.ExecuteNonQuery();
                cmd = new SqlCommand("set dateformat dmy;select distinct a.Fname,a.Specification,a.ptype,b.ProgramName,b.TrainingCost from trainer_profile1 a,paym_training_new b where a.trainer_id=b.TrainerId and a.trainer_id='"+ddl_trainer.SelectedValue+"' and b.instid='" + ddl_Inst.SelectedValue + "' and ((v_durationfrom between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "') or ( v_durationto between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "') or (v_durationfrom>='" + txtFromDate.Text + "' and v_durationto<='" + txtToDate.Text + "') or ( v_durationfrom<='" + txtFromDate.Text + "' and v_durationto>='" + txtToDate.Text + "'));set dateformat mdy", mycon);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cmd = new SqlCommand("set dateformat dmy;insert into temp_training_expense values('" + ddl_trainer.SelectedItem.Text + "','" + ddl_Inst.SelectedItem.Text + "','" + dr[1].ToString() + "','" + dr[2].ToString() + "','" + dr[3].ToString() + "','" + dr[4].ToString() + "','" + txtFromDate.Text + "','" + txtToDate.Text + "')set dateformat mdy;", mycon);
                    c = cmd.ExecuteNonQuery();
                }
                dr.Close();
                Session["ReportName"] = "~/crystalreports/TrainerExpense.rpt";
                Session["preview_page"] = "~/Training_Reports/TrainingExpense.aspx";
                Response.Redirect("~/PayrollReports/Report_view.aspx", false);
            }

            else if (ddl_select.SelectedItem.Text == "Program Name")
            {

                cmd = new SqlCommand("delete from temp_training_expense", mycon);
                int c = cmd.ExecuteNonQuery();

                cmd = new SqlCommand("set dateformat dmy;select distinct a.Fname,c.ins_name,a.Specification,a.ptype,b.TrainingCost,b.ProgramNAme from trainer_profile1 a,paym_training_new b,institution_profile c where a.trainer_id=b.TrainerId and b.instid=c.id and b.ProgramName='" + ddlPgm.SelectedItem.Text + "' and ((v_durationfrom between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "') or ( v_durationto between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "') or (v_durationfrom>='" + txtFromDate.Text + "' and v_durationto<='" + txtToDate.Text + "') or ( v_durationfrom<='" + txtFromDate.Text + "' and v_durationto>='" + txtToDate.Text + "'));set dateformat mdy", mycon);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cmd = new SqlCommand("set dateformat dmy;insert into temp_training_expense values('" + dr[0].ToString() + "','" + dr[1].ToString() + "','" + dr[2].ToString() + "','" + dr[3].ToString() + "','" + ddlPgm.SelectedItem.Text + "','" + dr[4].ToString() + "','" + txtFromDate.Text + "','" + txtToDate.Text + "')set dateformat mdy;", mycon);
                    c = cmd.ExecuteNonQuery();
                }
                dr.Close();
                Session["ReportName"] = "~/crystalreports/trainingexpense.rpt";
                Session["preview_page"] = "~/Training_Reports/TrainingExpense.aspx";
                Response.Redirect("~/PayrollReports/Report_view.aspx", false);
            }
           
            mycon.Close();

        }
        catch (Exception ex) { }
    }
    protected void ddl_select_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddl_select.SelectedItem.Text == "Institution")
            {
                Tr1.Visible = true;
                Tr2.Visible = false;
                Tr3.Visible = false;
                Tr4.Visible = true;
                Tr5.Visible = true;
                ddl_Institution_load();
            }
            else if (ddl_select.SelectedItem.Text == "Trainer")
            {
                Tr1.Visible = true;
                Tr2.Visible = true;
                Tr3.Visible = false;
                Tr4.Visible = true;
                Tr5.Visible = true;
                ddl_Institution_load();
            }
            else if (ddl_select.SelectedItem.Text == "Program Name")
            {
                Tr1.Visible = false;
                Tr2.Visible = false;
                Tr3.Visible = true;
                Tr4.Visible = true;
                Tr5.Visible = true;
                prgm();
            }
            else if (ddl_select.SelectedItem.Text == "Select")
            {
                Tr1.Visible = false;
                Tr2.Visible = false;
                Tr3.Visible = false;
                Tr4.Visible = false;
                Tr5.Visible = false;
            }
        }
        catch (Exception ex) { }
    }
    protected void ddl_dept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddl_Trainer_load();
        }
        catch(Exception ex)
        {}
    }
}
