using System;
using System.Collections;
using System.Configuration;
using System.Data;
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

public partial class Hrms_Additional_RequestTraining : System.Web.UI.Page
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
    Collection<Employee> InstitutionName;
    Collection<Employee> prgmnameList;
    Collection<Employee> prgmtypList;
    Collection<Employee> TrainerName;
    string s_login_role;
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
    int chk_i = 0;
    string _Value;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Session["Repordid"] = "";
        Session["fdate"] = "";
        Session["tdate"] = "";

        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);       
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
      

        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        //lbl_error.Text = "";

        if (!IsPostBack)
        {

            CompanyList = company.fn_getCompany();
            ListItem li = new ListItem();
            if (CompanyList.Count > 0)
            {    switch (s_login_role)
                {
                    case "a":
                        //admin();
                        //session_check();
                        tr1.Visible = false;
                        tr2.Visible = false;
                        tr3.Visible = false;
                        tr4.Visible = true;
                        ddl_Branch.Visible = false;
                        grid1();
                        //ddl_Branch_load();
                        break;

                    case "h":
                        tr1.Visible = false;
                        tr2.Visible = false;
                        tr3.Visible = false;
                        tr4.Visible = true;
                        ddl_Branch.Visible = false;
                        grid1();
                        //session_check();
                        break;

                    case "u": s_form = "79";
                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            ddl_Branch.Visible = false;
                            li = new ListItem();
                            li.Text = Request.Cookies["EmpCodeName"].Value;
                            li.Value = Request.Cookies["Login_temp_EmployeeID"].Value;
                            li.Selected = true;
                            tr1.Visible = true;
                            tr2.Visible = true;
                            tr3.Visible = true;
                            tr4.Visible = false;
                            grid();
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
                        tr1.Visible = true;
                        tr2.Visible = true;
                        tr3.Visible = true;
                        tr4.Visible = false;
                        grid();
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

    public void grid()
    {
        try
        {
            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            query = "Select * from Paym_training_request where pn_BranchId='" + employee.BranchId + "'";
            EmployeeList = employee.fn_getTraininglist(query);

            if (EmployeeList.Count > 0)
            {
                GridView1.DataSource = EmployeeList;
                GridView1.DataBind();
            }

        }
        catch (Exception ex) { }
    }
    public void grid1()
    {
        try
        {
            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            query = "Select * from Paym_training_request where pn_BranchId='" + employee.BranchId + "'";
            EmployeeList = employee.fn_getTraininglist(query);

            if (EmployeeList.Count > 0)
            {
                GridView2.DataSource = EmployeeList;
                GridView2.DataBind();
            }

        }
        catch (Exception ex) { }
    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        try
        {
            employee.TrainingID = 0;
            employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            employee.prgmname = txtpgmname.Text;
            employee.prgmtypName = ddl_PgmType.SelectedItem.Text;
            employee.temp_str = txtSummary.Text;
            employee.IDno = "Waiting";
            employee.Reason = "";
            _Value = employee.Employee_Training_Req(employee);
            if (_Value != "1")
            {
                lbl_Error.Text = "<font color=Blue>Added Successfully</font>";
                txtpgmname.Text = "";
                ddl_PgmType.SelectedIndex = -1;
                txtSummary.Text = "";
                grid();
            }
            else
            {
                lbl_Error.Text = "<font color=Red>Error Occured</font>";
                grid();
            }
        }
        catch (Exception ex) { }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView1.EditIndex = e.NewEditIndex;
            employee.prgmtypName = ((Label)GridView1.Rows[e.NewEditIndex].FindControl("lblPgmType")).Text;
            employee.IDno = ((Label)GridView1.Rows[e.NewEditIndex].FindControl("lblStatus")).Text;
            grid();
            DropDownList drp = (DropDownList)GridView1.Rows[e.NewEditIndex].FindControl("ddl_PgmType");
            //DropDownList drp1 = (DropDownList)GridView1.Rows[e.NewEditIndex].FindControl("ddl_Status");
            drp.SelectedItem.Text = employee.prgmtypName;
            //drp1.SelectedItem.Text = employee.IDno;
        }
        catch (Exception ex) { }
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        grid();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            GridViewRow row = GridView1.Rows[e.RowIndex];
            employee.prgmname = ((TextBox)row.FindControl("txtpgmname")).Text;
            employee.prgmtypName = ((DropDownList)row.FindControl("ddl_PgmType")).SelectedItem.Text;
            employee.temp_str = ((TextBox)row.FindControl("txtsum")).Text;
            employee.TrainingID = Convert.ToInt32(((Label)row.FindControl("lblid")).Text);
            employee.IDno = ((Label)row.FindControl("lblstatus")).Text;
            employee.Reason = ((Label)row.FindControl("lblreason")).Text;

            _Value = employee.Employee_Training_Req(employee);
            if (_Value != "1")
            {
                lbl_Error.Text = "<font color=Blue>Update Successfully</font>";
                txtpgmname.Text = "";
                ddl_PgmType.SelectedIndex = -1;
                txtSummary.Text = "";
                GridView1.EditIndex = -1;
                grid();
            }
            else
            {
                lbl_Error.Text = "<font color=Red>Error Occured</font>";
                grid();
            }
        }
        catch (Exception ex) { }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        try
        {
            string ID = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lblid")).Text;

            DeleteRecord(ID);

            grid();
        }
        catch (Exception ex) { }

    }
    private void DeleteRecord(string ID)
    {
        lbl_Error.Text = "";
        string sqlStatement = "DELETE FROM paym_training_Request WHERE id= @id";
        try
        {
            myConnection.Open();
            SqlCommand cmd = new SqlCommand(sqlStatement, myConnection);
            cmd.Parameters.AddWithValue("@id", ID);
            cmd.CommandType = CommandType.Text;
            int c= cmd.ExecuteNonQuery();
            if (c > 0)
            {
                lbl_Error.Text = "<font color=Blue>Deleted Successfully</font>";
            }
            else
            {
                lbl_Error.Text = "<font color=Red>errrrrorrrrrr</font>";
            }
            myConnection.Close();
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "errrrrorrrrrr";

        }
    }

    protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            GridViewRow row = GridView2.Rows[e.RowIndex];
            employee.prgmname = ((Label)row.FindControl("lblPgmName")).Text;
            employee.prgmtypName = ((Label)row.FindControl("lblPgmType")).Text;
            employee.temp_str = ((Label)row.FindControl("lblsum")).Text;
            employee.TrainingID = Convert.ToInt32(((Label)row.FindControl("lblid")).Text);
            employee.IDno = ((DropDownList)row.FindControl("ddl_Status")).SelectedItem.Text;
            employee.Reason = ((TextBox)row.FindControl("txtReason")).Text;

            _Value = employee.Employee_Training_Req(employee);
            if (_Value != "1")
            {
                lbl_Error.Text = "<font color=Blue>Update Successfully</font>";
               
                GridView2.EditIndex = -1;
                grid1();
            }
            else
            {
                lbl_Error.Text = "<font color=Red>Error Occured</font>";
                grid1();
            }
        }
        catch (Exception ex) { }
    }
    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView2.EditIndex = e.NewEditIndex;
            employee.IDno = ((Label)GridView2.Rows[e.NewEditIndex].FindControl("lblStatus")).Text;
            grid1();
            DropDownList drp = (DropDownList)GridView2.Rows[e.NewEditIndex].FindControl("ddl_Status");
            drp.SelectedItem.Text = employee.IDno;
        }
        catch (Exception ex) { }
    }
    protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView2.EditIndex = -1;
        grid1();
    }
}
