using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ePayHrms.Company;
using ePayHrms.Employee;
using System.Data.SqlClient;
using System.Globalization;


public partial class Hrms_Employee_Default : System.Web.UI.Page
{
    private SqlConnection _connection;
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlConnection sqlcon = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    Company company = new Company();
    public static string status;
    Employee employee = new Employee();
    Collection<Employee> EmployeeList;
    Collection<Employee> EmpProfileList;
    string s_login_role;
    string s_form = "", shift = "";
    string ec = "", en = "", cur_day = "", BranCode = "";
    static int deptid = 0;

    DataSet ds_userrights;

    public class Shift
    {
        #region Properties

        public TimeSpan intime { get; set; }

        public TimeSpan starttime { get; set; }

        public TimeSpan breakout { get; set; }

        public TimeSpan break_intime { get; set; }

        public TimeSpan break_outtime { get; set; }

        public TimeSpan breakin { get; set; }

        public TimeSpan halflimit { get; set; }

        public TimeSpan Otlimit { get; set; }

        public TimeSpan earlyout { get; set; }

        public TimeSpan outtime { get; set; }

        public TimeSpan othours { get; set; }

        public string stat { get; set; }

        public string leavecode { get; set; }

        public string ShiftCode { get; set; }

        #endregion Properties

        #region .ctors
        public Shift()
        {
        }
        #endregion Properties
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;


        if (!IsPostBack)
        {

            switch (s_login_role)
            {
                case "a":
                    populate_ddlbranch();
                    ddl_Department_load();
                    access();
                    break;

                case "h":
                    ddl_branch.Visible = false;
                    ddl_Department_load();
                    access();
                    break;

                case "u":
                    s_form = "37";
                    ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                    if (ds_userrights.Tables[0].Rows.Count > 0)
                    {
                        ddl_Department_load();
                        ddl_Employee_Department();
                    }
                    else
                    {
                        Response.Cookies["Msg_Session"].Value = "Permission Restricted. Please Contact Administrator.";
                        Response.Redirect("~/Company_Home.aspx");
                    }
                    break;

                default:
                    Response.Cookies["Msg_Session"].Value = "Permission Restricted. Please Contact Administrator";
                    Response.Redirect("~/Company_Home.aspx");
                    break;

            }
        }
    }

    public void ddl_Employee_Department()
    {
        employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
        EmpProfileList = employee.fn_get_Emp_Profile1(employee);
        if (EmpProfileList.Count > 0)
        {
            ddl_department.SelectedValue = EmpProfileList[0].DepartmentId.ToString();
            deptid = (int)EmpProfileList[0].DepartmentId;
            ddl_department.Enabled = false;
            ddl_Employee_load();
            ddl_ename.Enabled = true;
            ddl_type.SelectedIndex = 1;
        }
    }

    //public void ddl_Employee_load()
    //{
    //    ddl_ename.Items.Clear();
    //    employee.DepartmentId = Convert.ToInt32(ddl_department.SelectedValue);
    //    EmployeeList = employee.fn_getEmployeeDepartment(employee);
    //    if (EmployeeList.Count > 0)
    //    {
    //        for (int ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
    //        {
    //            if (ddl_i == -1)
    //            {
    //                ListItem es_list = new ListItem();
    //                es_list.Text = "Select Employee";
    //                es_list.Value = "0";
    //                ddl_ename.Items.Add(es_list);
    //            }
    //            else
    //            {
    //                ListItem es_list = new ListItem();
    //                es_list.Value = EmployeeList[ddl_i].EmployeeCode.ToString();
    //                es_list.Text = EmployeeList[ddl_i].LastName.ToString();
    //                ddl_ename.Items.Add(es_list);
    //            }
    //        }
    //    }
    //}

    //public void ddl_Department_load()
    //{
    //    EmployeeList = employee.fn_getDepartmentList1(employee.BranchId);
    //    if (EmployeeList.Count > 0)
    //    {

    //        for (int ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
    //        {

    //            if (ddl_i == -1)
    //            {
    //                ListItem es_list = new ListItem();

    //                es_list.Text = "Select Department";
    //                es_list.Value = "0";
    //                ddl_department.Items.Add(es_list);
    //            }
    //            else
    //            {
    //                ListItem es_list = new ListItem();

    //                es_list.Value = EmployeeList[ddl_i].DepartmentId.ToString();
    //                es_list.Text = EmployeeList[ddl_i].DepartmentName.ToString();
    //                ddl_department.Items.Add(es_list);
    //            }
    //        }
    //    }
    //}
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
    }
    public void ddl_Employee_load()
    {
        ddl_ename.Items.Clear();

        if (ddl_department.SelectedItem.Text == "All")
        {
            EmployeeList = employee.fn_getEmployeeList(employee);
            if (EmployeeList.Count > 0)
            {
                for (int ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
                {
                    if (ddl_i == -1)
                    {
                        ListItem es_list = new ListItem();
                        es_list.Text = "Select Employee";
                        ddl_ename.Items.Add(es_list);
                    }
                    else
                    {
                        ListItem es_list = new ListItem();
                        es_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
                        es_list.Text = EmployeeList[ddl_i].LastName.ToString();
                        ddl_ename.Items.Add(es_list);
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
                        ListItem es_list = new ListItem();
                        es_list.Text = "Select Employee";
                        ddl_ename.Items.Add(es_list);
                    }
                    else
                    {
                        ListItem es_list = new ListItem();
                        es_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
                        es_list.Text = EmployeeList[ddl_i].LastName.ToString();
                        ddl_ename.Items.Add(es_list);
                    }
                }
            }
        }
    }
    public void populate_ddlbranch()
    {
        myConnection.Open();
        SqlDataAdapter ad = new SqlDataAdapter("select * from paym_branch", myConnection);
        DataSet ds = new DataSet();
        ad.Fill(ds, "paym_branch");
        ddl_branch.DataSource = ds;

        ddl_branch.DataTextField = "branchname";
        ddl_branch.DataValueField = "pn_branchid";
        ddl_branch.DataBind();
        ddl_branch.Items.Insert(0, "Select ");
        //ddl_branch.SelectedItem.Text = "Select";
    }
    public void load()
    {
        string fdate = txt_fromdate.Text;
        string tdate = txt_todate.Text;
        string[] fd = fdate.Split('/');
        string[] td = tdate.Split('/');
        string fr_date = fd[1] + "/" + fd[0] + "/" + fd[2];
        string to_date = td[1] + "/" + td[0] + "/" + td[2];
        SqlDataAdapter ada = new SqlDataAdapter();
        if (s_login_role == "a")
        {
            ada = new SqlDataAdapter("SELECT * FROM time_card where dates between '" + fr_date + "' and '" + to_date + "' and pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + ddl_branch.SelectedItem.Text + "' and emp_name = '" + ddl_ename.SelectedItem.Text + "' order by dates ", myConnection);
        }

        else if (s_login_role == "h")
        {
            ada = new SqlDataAdapter("SELECT * FROM time_card where dates between '" + fr_date + "' and '" + to_date + "' and pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' and emp_name = '" + ddl_ename.SelectedItem.Text + "' order by dates ", myConnection);
        }

        DataSet dset = new DataSet();
        ada.Fill(dset, "temp_timecard");
        if (dset.Tables[0].Rows.Count == 0)
        {
            dset.Tables[0].Rows.Add(dset.Tables[0].NewRow());
            GridView1.DataSource = dset;
            GridView1.DataBind();
            int columnCount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columnCount;
            GridView1.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            GridView1.DataSource = dset;
            GridView1.DataBind();
        }
    }

    public void load1()
    {

        SqlDataAdapter ad = new SqlDataAdapter();
        if (s_login_role == "a")
        {
            ad = new SqlDataAdapter("SELECT * FROM temp_timecard  where pn_companyid = '" + ddl_branch.Text + "' and  pn_BranchID = '" + employee.BranchId + "' ", myConnection);
        }

        if (s_login_role == "h" || s_login_role == "u")
        {
            ad = new SqlDataAdapter("SELECT * FROM temp_timecard  where pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' ", myConnection);
        }
        DataSet ds = new DataSet();

        ad.Fill(ds, "time_card ");

        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView1.DataSource = ds;
            GridView1.DataBind();
            int columnCount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columnCount;
            GridView1.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GridView1.ClientID + "', 400, 950 , 40 ,true); </script>", false);
        }

    }


    public void access()
    {
        // MessageBox.Show(employee.BranchId.ToString());
        // MessageBox.Show(employee.CompanyId.ToString());
        _connection = con.fn_Connection();
        _connection.Open();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=7 and section_view='No'", _connection);
        SqlDataReader rdrview = cmd.ExecuteReader();
        if (rdrview.Read())
        {
            Response.Write("<script language='javascript'>alert('Permission Restricted. Please Contact Administrator.');window.location='~/Company_Home.aspx';</script>");
        }
        rdrview.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=7 and section_edit='No'", _connection);
        SqlDataReader rdredit = cmd.ExecuteReader();
        if (rdredit.Read())
        {

        }
        rdredit.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=7 and  section_delete='No'", _connection);
        SqlDataReader rdrdel = cmd.ExecuteReader();
        if (rdrdel.Read())
        {

        }
        rdrdel.Close();
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        //if (Btn_view.Text == "Save")
        //{
        //    load1();
        //}
        //else
        //{
        //    load();
        //}
        if (status == "Import")
        {
            load1();
        }
        else
        {
            ViewAttendance();
        }
        //Btn_view.Text = "Reset";
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.DataItem != null)
            {
                if (Btn_view.Text == "Save")
                {

                }
                else if (Btn_view.Text == "View")
                {
                    Label lbl_starttime = (Label)e.Row.FindControl("lbl_starttime");
                    if (DataBinder.Eval(e.Row.DataItem, "intime").ToString().Contains("01/01/1900"))
                    {
                        string dt = DataBinder.Eval(e.Row.DataItem, "intime").ToString();
                        string[] spt = dt.Split(' ');
                        dt = spt[1];
                        lbl_starttime.Text = dt;
                    }

                    Label lbl_breaktimei = (Label)e.Row.FindControl("lbl_breaktimei");
                    if (DataBinder.Eval(e.Row.DataItem, "late_in").ToString().Contains("01/01/1900"))
                    {
                        string dt = DataBinder.Eval(e.Row.DataItem, "late_in").ToString();
                        string[] spt = dt.Split(' ');
                        dt = spt[1];
                        lbl_breaktimei.Text = dt;
                    }

                    Label lbl_eout = (Label)e.Row.FindControl("lbl_eout");
                    if (DataBinder.Eval(e.Row.DataItem, "early_out").ToString().Contains("01/01/1900"))
                    {
                        string dt = DataBinder.Eval(e.Row.DataItem, "early_out").ToString();
                        string[] spt = dt.Split(' ');
                        dt = spt[1];
                        lbl_eout.Text = dt;
                    }

                    Label lbl_endtime = (Label)e.Row.FindControl("lbl_endtime");
                    if (DataBinder.Eval(e.Row.DataItem, "outtime").ToString().Contains("01/01/1900"))
                    {
                        string dt = DataBinder.Eval(e.Row.DataItem, "outtime").ToString();
                        string[] spt = dt.Split(' ');
                        dt = spt[1];
                        lbl_endtime.Text = dt;
                    }

                    Label lbl_breaktimeo = (Label)e.Row.FindControl("lbl_breaktimeo");
                    if (DataBinder.Eval(e.Row.DataItem, "Late_out").ToString().Contains("01/01/1900"))
                    {
                        string dt = DataBinder.Eval(e.Row.DataItem, "Late_out").ToString();
                        string[] spt = dt.Split(' ');
                        dt = spt[1];
                        lbl_breaktimeo.Text = dt;
                    }
                    //Label intim = ((Label)e.Row.FindControl("lbl_starttime"));
                    //Label btimi = ((Label)e.Row.FindControl("lbl_breaktimeo"));
                    //Label btimo = ((Label)e.Row.FindControl("lbl_breaktimei"));
                    //Label outtim = ((Label)e.Row.FindControl("lbl_endtime"));
                    //Label othour = ((Label)e.Row.FindControl("lbl_othrs"));
                    //if (intim.Text == "00:00")
                    //{ intim.Text = ""; }
                    //if (btimi.Text == "00:00")
                    //{ btimi.Text = ""; }
                    //if (btimo.Text == "00:00")
                    //{ btimo.Text = ""; }
                    //if (outtim.Text == "00:00")
                    //{ outtim.Text = ""; }
                    //if (othour.Text == "00:00")
                    //{ othour.Text = ""; }
                }

                else
                {

                }

            }


            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    e.Row.Attributes.Add("onclick",
            //    ClientScript.GetPostBackEventReference(GridView1, "Select$" +
            //    e.Row.RowIndex.ToString()));
            //    e.Row.Style["cursor"] = "pointer";
            //}
        }
        catch (Exception ex)
        {
            return;
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        //if (Btn_view.Text == "Save")
        //{
        //    load1();
        //}
        //else
        //{
        //    load();
        //}
        if (status == "Import")
        {
            //load1();
            ViewAttendance();
        }
        else
        {
            ViewAttendance();
        }
        //Btn_view.Text = "View";
        Btn_view.CssClass = "btn btn-info";
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = GridView1.Rows[e.RowIndex];
        if (row != null)
        {
            string emp_code = ((Label)row.FindControl("txt_empcode_edit")).Text;
            string emp_name = ((Label)row.FindControl("txt_empname_edit")).Text;
            string shift_code = ((Label)row.FindControl("ddl_editshiftcode1")).Text;
            string date_edit = ((Label)row.FindControl("txt_date_edit")).Text;
            string day_edit = ((Label)row.FindControl("txt_day_edit")).Text;
            string start_time = ((TextBox)row.FindControl("txt_starttime_edit")).Text;
            string latein = ((TextBox)row.FindControl("txt_breaktimei_edit")).Text;
            string break_timeo = ((TextBox)row.FindControl("txt_breaktimeo_edit")).Text;
            string break_timei = ((TextBox)row.FindControl("txt_eout_edit")).Text;
            string end_time = ((TextBox)row.FindControl("txt_endtime_edit")).Text;
            string othrs = ((TextBox)row.FindControl("txt_othrs_edit")).Text;
            string leave = ((DropDownList)row.FindControl("ddl_leavename")).SelectedItem.Text;
            string status = ((DropDownList)row.FindControl("ddl_status")).SelectedValue;
            if (leave == "Select" || status == "Select")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Update Failed. Check Leave Name and Status');", true);
                return;
            }
            myConnection.Open();
            if (s_login_role == "a")
            {
                SqlCommand updat = new SqlCommand("set dateformat dmy;update time_card set intime = '" + start_time + "', late_in = '" + latein + "',late_out = '" + break_timeo + "',early_out = '" + break_timei + "',outtime ='" + end_time + "',status='" + status + "',leave_code = '" + leave + "' ,data='M' where emp_code = '" + emp_code + "' and emp_name = '" + emp_name + "' and dates = '" + date_edit + "';set dateformat mdy;", myConnection);
                updat.ExecuteNonQuery();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Updated successfully');", true);
            }

            if (s_login_role == "h")
            {
                SqlCommand updat = new SqlCommand("set dateformat dmy;update temp_timecard set intime = '" + start_time + "', late_in = '" + latein + "', late_out = '" + break_timeo + "',early_out = '" + break_timei + "',outtime ='" + end_time + "',status='" + status + "',leave_code = '" + leave + "' where emp_code = '" + emp_code + "' and emp_name = '" + emp_name + "' and dates = '" + date_edit + "';set dateformat mdy;", myConnection);
                updat.ExecuteNonQuery();
            }

            GridView1.EditIndex = -1;
            load1();
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Updated Successfully');", true);
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbl_previous.Text = "Previous Day";
        lbl_current.Text = "Current Day";
        lbl_next.Text = "Next Day";
        GridView1.SelectedRow.BackColor = System.Drawing.Color.Aqua;
        Label lbldate = (Label)GridView1.SelectedRow.FindControl("lbl_date");
        Label lblcode = (Label)GridView1.SelectedRow.FindControl("lbl_empcode");
        DateTime datenow = Convert.ToDateTime(lbldate.Text);
        DateTime d1 = datenow.AddDays(-1);
        DateTime d2 = datenow.AddDays(1);
        string date = lbldate.Text;
        string date1 = d1.ToShortDateString();
        string date2 = d2.ToShortDateString();
        string[] dat = date.Split('/', '-');
        string act_date = dat[1] + "/" + dat[0] + "/" + dat[2];
        string[] dat1 = date1.Split('/', '-');
        string act_date1 = dat1[1] + "/" + dat1[0] + "/" + dat1[2];
        string[] dat2 = date2.Split('/', '-');
        string act_date2 = dat2[1] + "/" + dat2[0] + "/" + dat2[2];
        lb_current.Items.Clear();
        lb_previous.Items.Clear();
        lb_next.Items.Clear();
        myConnection.Open();
        cmd = new SqlCommand("select distinct * from punch_details where emp_code='" + lblcode.Text + "' and  dates = '" + act_date + "' and pn_companyid = '" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
        SqlDataReader rd_ls = cmd.ExecuteReader();
        while (rd_ls.Read())
        {
            lbl_current.Visible = true;
            lb_current.Visible = true;
            //DateTime d = Convert.ToDateTime(rd_ls["times"]);
            lb_current.Items.Add(rd_ls["times"].ToString());
        }
        cmd = new SqlCommand("select * from punch_details where emp_code='" + lblcode.Text + "' and  dates = '" + act_date1 + "' and pn_companyid = '" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
        SqlDataReader rd_lp = cmd.ExecuteReader();
        while (rd_lp.Read())
        {
            lbl_previous.Visible = true;
            lb_previous.Visible = true;
            //DateTime da1 = Convert.ToDateTime(rd_lp["times"]);
            lb_previous.Items.Add(rd_lp["times"].ToString());
        }
        cmd = new SqlCommand("select * from punch_details where emp_code='" + lblcode.Text + "' and  dates = '" + act_date2 + "' and pn_companyid = '" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
        SqlDataReader rd_ln = cmd.ExecuteReader();
        while (rd_ln.Read())
        {
            lbl_next.Visible = true;
            lb_next.Visible = true;
            //DateTime da2 = Convert.ToDateTime(rd_ln["times"]);
            lb_next.Items.Add(rd_ln["times"].ToString());
        }

    }

    public void ClearList()
    {
        lb_previous.Items.Clear();
        lb_current.Items.Clear();
        lb_next.Items.Clear();
    }

    protected void Btn_import_Click(object sender, EventArgs e)
    {
        try
        {
            //GridView1.AutoGenerateEditButton = true;
            //GridView1.AutoGenerateDeleteButton = true;
            status = "Import";
            ClearList();
            Btn_view.Text = "Save";
            Btn_view.CssClass = "btn btn-success";
            DateTime fdate, tdate;
            if (ddl_type.SelectedItem.Text == "Employee")
            {
                myConnection.Open();
                string cmd_text = "IF EXISTS (SELECT * FROM sysobjects WHERE Name = 'temp_timecard')" + "DROP TABLE temp_timecard";
                SqlCommand cmd2 = new SqlCommand(cmd_text, myConnection);
                cmd2.ExecuteNonQuery();
                myConnection.Close();


                fdate = Convert.ToDateTime(txt_fromdate.Text);
                tdate = Convert.ToDateTime(txt_todate.Text);
                TimeSpan diff = tdate - fdate;
                int date_diff = diff.Days;

                SqlCommand comm = new SqlCommand();
                TimeSpan inchk = new TimeSpan(0, 0, 0);
                TimeSpan outchk = new TimeSpan(0, 0, 0);
                TimeSpan out_time = new TimeSpan(0, 0, 0);
                TimeSpan in_limit = new TimeSpan(0, 0, 0);
                TimeSpan breakin_limit = new TimeSpan(0, 0, 0);
                TimeSpan breakout_limit = new TimeSpan(0, 0, 0);
                TimeSpan start_time = new TimeSpan(0, 0, 0);
                TimeSpan breakin_time = new TimeSpan(0, 0, 0);
                TimeSpan breakout_time = new TimeSpan(0, 0, 0);

                DateTime inc, outc, chkin, chkout;
                TimeSpan halfLmt = new TimeSpan(0, 0, 0);


                TimeSpan in_time, daily_in, daily_out, ot_hours, ein, eout;

                string cin = "", cbin = "", cbout = "", cout = "", ecod, stats = "", outdate, dat1, leav = "";
                int otc = 0;
                //txt_fromdate.Text = DateTime.Now.ToShortDateString();
                myConnection.Open();

                string dt1 = Convert.ToString(txt_fromdate.Text);
                string[] date1 = dt1.Split('/');
                string txt_date = date1[1] + "/" + date1[0] + "/" + date1[2];
                //DateTime day = Convert.ToDateTime(txt_fromdate.Text);
                //cur_day = day.DayOfWeek.ToString();

                cmd = new SqlCommand("create table temp_timecard(pn_companyid int, pn_branchid int, BranchCode varchar(10), emp_code varchar(10), emp_name varchar(50),shift_code varchar(10),dates datetime,days varchar(15),intime time , break_out time, break_in time , outtime time, late_in time, late_out time, early_out time, ot_hrs datetime ,leave_code varchar(20), status varchar(2))", myConnection);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("delete from daily_timecard_new", myConnection);
                cmd.ExecuteNonQuery();

                for (int d = 0; d <= date_diff; d++)
                {
                    DateTime day = fdate.AddDays(d);
                    cur_day = day.DayOfWeek.ToString();
                    string dt = Convert.ToString(fdate.AddDays(d));
                    string[] date = dt.Split('/', '-');
                    txt_date = date[1] + "/" + date[0] + "/" + date[2];


                    cin = ""; cbin = ""; cbout = ""; cout = "";
                    TimeSpan otslb = new TimeSpan(0, 0, 0);
                    string lin = "00:00:00", lout = "00:00:00", e_out = "00:00:00";
                    string[] code = ddl_ename.SelectedItem.Text.Split('-');
                    ec = code[0];
                    //ec = ddl_en66ame.SelectedItem.Value;
                    employee.EmployeeId = Convert.ToInt32(ddl_ename.SelectedValue);
                    en = ddl_ename.SelectedItem.Text;
                    string[] name = en.Split('-');
                    en = name[1].Trim();
                    BranCode = "";

                    Collection<Shift> Attend;
                    Attend = Shift_Details(dt);

                    cmd = new SqlCommand("delete from daily_timecard_new", myConnection);
                    cmd.ExecuteNonQuery();
                    if (Attend[0].stat == "Next Day")
                    {
                        TimeSpan tInc = new TimeSpan(1, 0, 0);
                        TimeSpan tDec = new TimeSpan(-1, 0, 0);
                        string dtend = Convert.ToString(fdate.AddDays(1));
                        string[] datend = dtend.Split('/', '-');
                        string txt_date1 = datend[1] + "/" + datend[0] + "/" + datend[2];
                        cmd = new SqlCommand("Insert into daily_timecard_new select distinct a.pn_companyid,a.pn_branchid,a.machine_num,a.card_no,a.emp_code,a.emp_name,a.VerifyMode,a.InOutMode,a.shift_code,a.dates,a.days,(select top 1 aa.times as Intime from punch_details aa where times between '" + Attend[0].intime.Add(tDec) + "' and '" + Attend[0].intime.Add(tInc) + "' and aa.emp_code=a.emp_code and aa.dates='" + txt_date + "' order by aa.times desc ) as intime,'00:00' as break_out,'00:00' as break_in,(select top 1 aa.times as outtime from punch_details aa where times between '" + Attend[0].outtime.Add(tDec) + "' and '" + Attend[0].outtime.Add(tInc) + "' and aa.emp_code=a.emp_code and aa.dates='" + txt_date1 + "' order by aa.times desc) as outtime , a.ot_hrs , a.status from (select * from punch_details) as a where dates = '" + txt_date + "' and emp_code = '" + ec + "'", myConnection);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd = new SqlCommand("Insert into daily_timecard_new select distinct a.pn_companyid,a.pn_branchid,a.machine_num,a.card_no,a.emp_code,a.emp_name,a.VerifyMode,a.InOutMode,a.shift_code,a.dates,a.days,(select top 1 aa.times as Intime from punch_details aa where times < '" + Attend[0].break_intime + "' and aa.emp_code=a.emp_code and aa.dates=a.dates order by aa.times desc ) as intime,(select top 1 aa.times as break_out from punch_details aa where times between '" + Attend[0].break_intime + "' and '" + Attend[0].breakin + "' and aa.emp_code=a.emp_code and aa.dates=a.dates order by aa.times desc) as break_out,(select top 1 aa.times as break_in from punch_details aa where times between '" + Attend[0].breakout + "' and '" + Attend[0].breakin + "' and aa.emp_code=a.emp_code and aa.dates=a.dates order by aa.times desc) as break_in,(select top 1 aa.times as outtime from punch_details aa where times > '" + Attend[0].breakin + "' and aa.emp_code=a.emp_code and aa.dates=a.dates order by aa.times desc) as outtime , a.ot_hrs , a.status from (select * from punch_details) as a where dates = '" + txt_date + "' and emp_code = '" + ec + "'", myConnection);
                        cmd.ExecuteNonQuery();
                    }
                    SqlCommand cmd_dt = new SqlCommand("Select * from daily_timecard_new where pn_branchid = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "' and dates = '" + txt_date + "' and emp_code='" + ec + "'", myConnection);
                    SqlDataReader rd_time = cmd_dt.ExecuteReader();
                    if (rd_time.Read())
                    {
                        cin = Convert.ToString(rd_time["intime"]);
                        cbin = Convert.ToString(rd_time["break_out"]);
                        cbout = Convert.ToString(rd_time["break_in"]);
                        cout = Convert.ToString(rd_time["outtime"]);
                        outdate = Convert.ToString(rd_time["dates"]);
                        string[] datespl = outdate.Split(' ');
                        dat1 = employee.Convert_ToSqlDatestring(datespl[0]);
                        ecod = Convert.ToString(rd_time["emp_code"]);
                        stats = "";
                        if (cin != "")
                        {
                            string stim = rd_time["intime"].ToString();
                            inchk = TimeSpan.Parse(stim);
                            if (inchk > Attend[0].starttime)
                            {
                                lin = Convert.ToString(inchk - Attend[0].starttime);
                            }
                        }
                        //if (Attend[0].stat == "Next Day")
                        //{
                        //    if (cin != "")
                        //    {
                        //        string stim = rd_time["intime"].ToString();
                        //        inchk = TimeSpan.Parse(stim);
                        //        if (inchk > Attend[0].starttime)
                        //        {
                        //            lin = Convert.ToString(inchk - Attend[0].starttime);
                        //        }
                        //    }
                        //}
                        if (cout != "")
                        {

                            string etim = rd_time["outtime"].ToString();
                            outchk = TimeSpan.Parse(etim);
                            if (outchk > Attend[0].outtime)
                            {
                                lout = Convert.ToString(outchk - Attend[0].outtime);
                            }
                            else
                            {
                                e_out = Convert.ToString(Attend[0].outtime - outchk);
                            }
                        }
                        //if (Attend[0].stat == "Next Day")
                        //{
                        //    if (cout != "")
                        //    {

                        //        string etim = rd_time["outtime"].ToString();
                        //        outchk = TimeSpan.Parse(etim);
                        //        if (outchk > Attend[0].outtime)
                        //        {
                        //            lout = Convert.ToString(outchk - Attend[0].outtime);
                        //        }
                        //        else
                        //        {
                        //            e_out = Convert.ToString(Attend[0].outtime - outchk);
                        //        }
                        //    }
                        //}
                        if (cin != "" && cout != "")
                        {
                            stats = "XX";
                            leav = "";
                            chkin = Convert.ToDateTime(cin);
                            cin = chkin.ToString("HH:mm:ss");
                            ein = TimeSpan.Parse(cin);
                            diff = Attend[0].break_intime - ein;
                            if (diff < Attend[0].halflimit)
                            {
                                stats = "AX";
                            }
                            chkout = Convert.ToDateTime(cout);
                            cout = chkout.ToString("HH:mm:ss");
                            eout = TimeSpan.Parse(cout);
                            diff = eout - Attend[0].break_outtime;
                            if (diff < Attend[0].halflimit)
                            {
                                stats = "XA";
                            }
                            string perm = Check_Permission(dt);
                            if (perm != "")
                            {
                                if (perm == "FN")
                                { stats = "PX"; }
                                else
                                { stats = "XP"; }
                            }

                        }
                        else if (((cin == "" && cbout != "") || (cin != "" && cbout == "") || (cin == "" && cbout == "")) && cout != "")
                        {
                            stats = "AX";
                            leav = "Half Day";
                        }
                        else if (((cbin == "" && cout != "") || (cbin != "" && cout == "") || (cbin == "" && cout == "")) && cin != "" && (cbin != "" || cbout != ""))
                        {
                            stats = "XA";
                            leav = "Half Day";
                            cout = cbin;
                        }
                        else
                        {
                            stats = "AA";
                            leav = "Absent";
                        }

                    }
                    else
                    {
                        stats = "AA";
                        leav = "Absent";
                    }

                    SqlCommand cmdlv = new SqlCommand("Select a.pn_Leavecode , a.pn_leavename, a.from_status, a.status, b.type from leave_apply a, paym_leave b where a.pn_branchid = b.pn_branchID and a.pn_leavecode=b.pn_leavecode and a.pn_BranchID = '" + employee.BranchId + "' and a.pn_companyid = '" + employee.CompanyId + "' and a.from_date <= '" + txt_date + "' and a.to_date >= '" + txt_date + "' and a.emp_code = '" + ec + "' and a.flag='y'", myConnection);
                    SqlDataReader rdlv = cmdlv.ExecuteReader();
                    while (rdlv.Read())
                    {
                        string spt1 = stats.Substring(0, 1);
                        string spt2 = stats.Substring(1, 1);
                        string type = rdlv["type"].ToString();
                        if (rdlv["from_status"].ToString() == "FH")
                        {
                            stats = "L" + spt2;
                        }
                        else if (rdlv["from_status"].ToString() == "SH")
                        {
                            stats = spt1 + "L";
                        }
                        else if (rdlv["from_status"].ToString() == "FD")
                        {
                            stats = "LL";
                        }
                        if (type == "On Duty")
                        {
                            if (rdlv["from_status"].ToString() == "FH")
                            {
                                stats = "D" + spt2;
                            }
                            else if (rdlv["from_status"].ToString() == "SH")
                            {
                                stats = spt1 + "D";
                            }
                            else if (rdlv["from_status"].ToString() == "FD")
                            {
                                stats = "DD";
                            }
                        }
                        //stats = Convert.ToString(rdlv[0]);
                        leav = Convert.ToString(rdlv[0]);
                        cmd1 = new SqlCommand("update leave_apply set record = 'T' where from_date <= '" + txt_date + "' and to_date >= '" + txt_date + "' and emp_code = '" + ec + "' and emp_name = '" + en + "' and pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
                        cmd1.ExecuteNonQuery();
                    }


                    //SqlCommand cmdod = new SqlCommand("Select fstatus, tstatus from onduty where pn_branchid = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "' and onduty_dat <= '" + txt_date + "' and todat >= '" + txt_date + "' and empid = '" + employee.EmployeeId + "'", myConnection);
                    //SqlDataReader rdod = cmdod.ExecuteReader();
                    //while (rdod.Read())
                    //{
                    //    string spt1 = stats.Substring(0, 1);
                    //    string spt2 = stats.Substring(1, 1);
                    //    if (rdod["fstatus"].ToString() == "First Half")
                    //    {
                    //        stats = "D" + spt2;
                    //    }
                    //    else if (rdod["fstatus"].ToString() == "Second Half")
                    //    {
                    //        stats = spt1 + "D";
                    //    }
                    //    else if (rdod["fstatus"].ToString() == "Full Day")
                    //    {
                    //        stats = "DD";
                    //    }
                    //    //stats = Convert.ToString(rdlv[0]);
                    //    leav = "On Duty";

                    //}

                    SqlCommand cmdhd = new SqlCommand("Select pn_Holidaycode, pn_Holidayname from paym_holiday where pn_branchid = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "' and from_date <= '" + txt_date + "' and to_date >= '" + txt_date + "'", myConnection);
                    SqlDataReader rdhd = cmdhd.ExecuteReader();
                    if (rdhd.Read())
                    {
                        leav = Convert.ToString(rdhd[1]);
                        stats = "HH";
                    }


                    //SqlCommand cmdod = new SqlCommand("Select * from onduty where pn_branchid = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "' and onduty_dat <= '" + txt_date + "' and todat >= '" + txt_date + "' and empname = '" + en + "' and approval = 'yes' ", myConnection);
                    //SqlDataReader rdod = cmdod.ExecuteReader();
                    //if (rdod.Read())
                    //{
                    //    leav = "On Duty";
                    //    stats = "DD";
                    //}

                    if (Attend[0].ShiftCode == "W")
                    {
                        leav = "Week Off";
                        stats = "WW";
                    }

                    SqlCommand cmdotc = new SqlCommand("Select ot_calc from paym_employee where pn_branchid = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "'  and Employee_First_Name = '" + en + "'", myConnection);
                    SqlDataReader rdotc = cmdotc.ExecuteReader();
                    if (rdotc.Read())
                    {
                        otc = Convert.ToInt32(rdotc[0]);
                    }

                    if (cin != "" && cout != "")
                    {
                        chkin = Convert.ToDateTime(cin);
                        cin = chkin.ToString("HH:mm:ss");
                        ein = TimeSpan.Parse(cin);
                        chkout = Convert.ToDateTime(cout);
                        cout = chkout.ToString("HH:mm:ss");
                        eout = TimeSpan.Parse(cout);
                        int ttl_hr = (Attend[0].outtime - Attend[0].starttime).Hours;
                        int emp_hr = (eout - ein).Hours;

                        // DateTime dtim = Convert.ToDateTime(rd_time["outtime"]);
                        string etime = cout;  //dtim.ToString("HH:mm:ss");
                        daily_out = TimeSpan.Parse(etime);
                        if (otc == 1 && cout != "" && emp_hr >= ttl_hr)
                        {
                            ot_hours = daily_out - Attend[0].outtime;
                            if (ot_hours < System.TimeSpan.Parse("00:00:00"))
                            {
                                ot_hours = System.TimeSpan.Parse("00:00:00");
                            }

                            comm = new SqlCommand("select ot_slab from otslab where ot_from <= '" + ot_hours + "' and ot_to >= '" + ot_hours + "' and pn_branchID = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "'", myConnection);
                            SqlDataReader rdot = comm.ExecuteReader();
                            if (rdot.Read())
                            {
                                DateTime ottime = Convert.ToDateTime(rdot[0]);
                                string ottim = ottime.ToString("HH:mm:ss");
                                otslb = TimeSpan.Parse(ottim);
                            }
                        }
                    }

                    cmd = new SqlCommand("insert into temp_timecard values ('" + employee.CompanyId + "', '" + employee.BranchId + "' , '" + BranCode + "','" + ec + "','" + en + "','" + Attend[0].ShiftCode + "','" + txt_date + "','" + cur_day + "','" + cin + "','" + cbin + "','" + cbout + "','" + cout + "','" + lin + "','" + lout + "','" + e_out + "','" + otslb + "','" + leav + "','" + stats + "')", myConnection);
                    cmd.ExecuteNonQuery();

                    //cmd = new SqlCommand("create table temp_timecard3(pn_companyid int,pn_branchid int,machine_num int, card_no varchar(5) , emp_code varchar(10), emp_name varchar(50),VerifyMode int,InOutMode int, shift_code varchar(5),dates datetime,days varchar(15),intime time , break_out time, break_in time , outtime time, ot_hrs datetime , status varchar(2))", myConnection);
                    //cmd.ExecuteNonQuery();
                    //cmd = new SqlCommand("INSERT INTO temp_timecard3 SELECT DISTINCT * FROM daily_timecard_new where pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
                    //cmd.ExecuteNonQuery();
                    //cmd = new SqlCommand("drop table daily_timecard_new", myConnection);
                    //cmd.ExecuteNonQuery();
                    //cmd = new SqlCommand("EXEC sp_rename 'temp_timecard3', 'daily_timecard_new'", myConnection);
                    //cmd.ExecuteNonQuery();
                }
                myConnection.Close();

            }

            else if (ddl_type.SelectedItem.Text == "All")
            {

                SqlCommand comm = new SqlCommand();
                TimeSpan inchk = new TimeSpan(0, 0, 0);
                TimeSpan outchk = new TimeSpan(0, 0, 0);
                TimeSpan out_time = new TimeSpan(0, 0, 0);
                TimeSpan in_limit = new TimeSpan(0, 0, 0);
                TimeSpan breakin_limit = new TimeSpan(0, 0, 0);
                TimeSpan breakout_limit = new TimeSpan(0, 0, 0);
                TimeSpan start_time = new TimeSpan(0, 0, 0);
                TimeSpan breakin_time = new TimeSpan(0, 0, 0);
                TimeSpan breakout_time = new TimeSpan(0, 0, 0);

                DateTime inc, outc, chkin, chkout;
                TimeSpan halfLmt = new TimeSpan(0, 0, 0);

                TimeSpan diff = new TimeSpan(0, 0, 0);
                TimeSpan in_time, daily_in, daily_out, ot_hours, ein, eout;

                string cin = "", cbin = "", cbout = "", cout = "", ecod, stats = "", outdate, dat1, leav = "";
                int otc = 0;
                //txt_fromdate.Text = DateTime.Now.ToShortDateString();
                myConnection.Open();
                string cmd_text = "IF EXISTS (SELECT * FROM sysobjects WHERE Name = 'temp_timecard')" + "DROP TABLE temp_timecard";
                SqlCommand cmd2 = new SqlCommand(cmd_text, myConnection);
                cmd2.ExecuteNonQuery();
                fdate = Convert.ToDateTime(txt_fromdate.Text);
                tdate = Convert.ToDateTime(DateTime.Now);
                TimeSpan difff = tdate - fdate;
                int date_difff = difff.Days;

                string dt1 = Convert.ToString(txt_fromdate.Text);
                string[] date1 = dt1.Split('/');
                string txt_date = date1[1] + "/" + date1[0] + "/" + date1[2];
                DateTime day = Convert.ToDateTime(txt_fromdate.Text);
                cur_day = day.DayOfWeek.ToString();
                cmd = new SqlCommand("delete from daily_timecard_new", myConnection);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("create table temp_timecard(pn_companyid int, pn_branchid int, BranchCode varchar(10), emp_code varchar(10), emp_name varchar(50),shift_code varchar(10),dates datetime,days varchar(15),intime time , break_out time, break_in time , outtime time, late_in time, late_out time, early_out time, ot_hrs datetime ,leave_code varchar(20), status varchar(2))", myConnection);
                cmd.ExecuteNonQuery();
                if (s_login_role == "a")
                {
                    cmd = new SqlCommand("select a.*, b.branchcode from paym_employee a, paym_branch b where a.pn_BranchID = b.pn_branchID and a.pn_companyid='" + employee.CompanyId + "' and a.pn_branchid = '" + ddl_branch.Text + "' and a.status='Y' order by a.employee_first_name asc", myConnection);
                }
                if (s_login_role == "h")
                {
                    cmd = new SqlCommand("select a.*, b.branchcode from paym_employee a, paym_branch b where a.pn_BranchID = b.pn_branchID and a.pn_companyid='" + employee.CompanyId + "' and a.pn_branchid = '" + employee.BranchId + "' and a.status='Y' order by a.employee_first_name asc", myConnection);
                }
                if (s_login_role == "u")
                {
                    cmd = new SqlCommand("select a.* from paym_employee a, paym_employee_profile1 b where a.pn_BranchID = b.pn_branchID and a.pn_companyid='" + employee.CompanyId + "' and a.pn_branchid = '" + employee.BranchId + "' and a.status='Y' and b.pn_DepartmentID = '" + deptid + "' and a.pn_EmployeeID=b.pn_EmployeeID order by a.employee_first_name asc", myConnection);
                }
                SqlDataReader rd_ecode1 = cmd.ExecuteReader();
                while (rd_ecode1.Read())
                {
                    cin = ""; cbin = ""; cbout = ""; cout = "";
                    TimeSpan otslb = new TimeSpan(0, 0, 0);
                    string lin = "00:00:00", lout = "00:00:00", e_out = "00:00:00";
                    employee.EmployeeId = Convert.ToInt32(rd_ecode1["pn_EmployeeID"]);
                    ec = Convert.ToString(rd_ecode1["EmployeeCode"]);
                    en = Convert.ToString(rd_ecode1["Employee_First_Name"]);
                    if (s_login_role == "u")
                    {
                        BranCode = Convert.ToString(rd_ecode1["BranchCode"]);
                    }

                    Collection<Shift> Attend;
                    Attend = Shift_Details(dt1);

                    if (Attend[0].stat == "Next Day")
                    {
                        TimeSpan tInc = new TimeSpan(1, 0, 0);
                        TimeSpan tDec = new TimeSpan(-1, 0, 0);
                        string dtend = Convert.ToString(fdate.AddDays(1));
                        string[] datend = dtend.Split('/', '-');
                        string txt_date1 = datend[1] + "/" + datend[0] + "/" + datend[2];
                        cmd = new SqlCommand("Insert into daily_timecard_new select distinct a.pn_companyid,a.pn_branchid,a.machine_num,a.card_no,a.emp_code,a.emp_name,a.VerifyMode,a.InOutMode,a.shift_code,a.dates,a.days,(select top 1 aa.times as Intime from punch_details aa where times  between '" + Attend[0].intime.Add(tDec) + "' and '" + Attend[0].intime.Add(tInc) + "' and aa.emp_code=a.emp_code and aa.dates='" + txt_date + "' order by aa.times desc ) as intime,'00:00' as break_out,'00:00' as break_in,(select top 1 aa.times as outtime from punch_details aa where times  between '" + Attend[0].outtime.Add(tDec) + "' and '" + Attend[0].outtime.Add(tInc) + "' and aa.emp_code=a.emp_code and aa.dates='" + txt_date1 + "' order by aa.times desc) as outtime , a.ot_hrs , a.status from (select * from punch_details) as a where dates = '" + txt_date + "' and emp_code = '" + ec + "'", myConnection);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd = new SqlCommand("Insert into daily_timecard_new select distinct a.pn_companyid,a.pn_branchid,a.machine_num,a.card_no,a.emp_code,a.emp_name,a.VerifyMode,a.InOutMode,a.shift_code,a.dates,a.days,(select top 1 aa.times as Intime from punch_details aa where times < '" + Attend[0].break_intime + "' and aa.emp_code=a.emp_code and aa.dates=a.dates order by aa.times desc ) as intime,(select top 1 aa.times as break_out from punch_details aa where times between '" + Attend[0].break_intime + "' and '" + Attend[0].breakin + "' and aa.emp_code=a.emp_code and aa.dates=a.dates order by aa.times desc) as break_out,(select top 1 aa.times as break_in from punch_details aa where times between '" + Attend[0].breakout + "' and '" + Attend[0].breakin + "' and aa.emp_code=a.emp_code and aa.dates=a.dates order by aa.times desc) as break_in,(select top 1 aa.times as outtime from punch_details aa where times > '" + Attend[0].breakin + "' and aa.emp_code=a.emp_code and aa.dates=a.dates order by aa.times desc) as outtime , a.ot_hrs , a.status from (select * from punch_details) as a where dates = '" + txt_date + "' and emp_code = '" + ec + "'", myConnection);
                        cmd.ExecuteNonQuery();
                    }
                    //cmd = new SqlCommand("delete from daily_timecard_new", myConnection);
                    //cmd.ExecuteNonQuery();
                    //cmd = new SqlCommand("Insert into daily_timecard_new select distinct a.pn_companyid,a.pn_branchid,a.machine_num,a.card_no,a.emp_code,a.emp_name,a.VerifyMode,a.InOutMode,a.shift_code,a.dates,a.days,(select top 1 aa.times as Intime from punch_details aa where times < '" + Attend[0].break_intime + "' and aa.emp_code=a.emp_code and aa.dates=a.dates order by aa.times desc ) as intime,(select top 1 aa.times as break_out from punch_details aa where times between '" + Attend[0].break_intime + "' and '" + Attend[0].breakin + "' and aa.emp_code=a.emp_code and aa.dates=a.dates order by aa.times desc) as break_out,(select top 1 aa.times as break_in from punch_details aa where times between '" + Attend[0].breakout + "' and '" + Attend[0].breakin + "' and aa.emp_code=a.emp_code and aa.dates=a.dates order by aa.times desc) as break_in,(select top 1 aa.times as outtime from punch_details aa where times > '" + Attend[0].breakin + "' and aa.emp_code=a.emp_code and aa.dates=a.dates order by aa.times desc) as outtime , a.ot_hrs , a.status from (select * from punch_details) as a where dates = '" + txt_date + "' and emp_code = '" + ec + "'", myConnection);
                    //cmd.ExecuteNonQuery();
                    SqlCommand cmd_dt = new SqlCommand("Select * from daily_timecard_new where pn_branchid = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "' and dates = '" + txt_date + "' and emp_code='" + ec + "'", myConnection);
                    SqlDataReader rd_time = cmd_dt.ExecuteReader();
                    if (rd_time.Read())
                    {
                        cin = Convert.ToString(rd_time["intime"]);
                        cbin = Convert.ToString(rd_time["break_out"]);
                        cbout = Convert.ToString(rd_time["break_in"]);
                        cout = Convert.ToString(rd_time["outtime"]);
                        outdate = Convert.ToString(rd_time["dates"]);
                        string[] datespl = outdate.Split(' ');
                        dat1 = employee.Convert_ToSqlDatestring(datespl[0]);
                        ecod = Convert.ToString(rd_time["emp_code"]);
                        stats = "";
                        if (cin != "")
                        {
                            string stim = rd_time["intime"].ToString();
                            inchk = TimeSpan.Parse(stim);
                            if (inchk > Attend[0].starttime)
                            {
                                lin = Convert.ToString(inchk - Attend[0].starttime);
                            }
                        }
                        if (cout != "")
                        {

                            string etim = rd_time["outtime"].ToString();
                            outchk = TimeSpan.Parse(etim);
                            if (outchk > Attend[0].outtime)
                            {
                                lout = Convert.ToString(outchk - Attend[0].outtime);
                            }
                            else
                            {
                                e_out = Convert.ToString(Attend[0].outtime - outchk);
                            }
                        }
                        if (Attend[0].stat == "Next Day")
                            {
                            if (cin != "")
                            {
                                string stim = rd_time["intime"].ToString();
                                inchk = TimeSpan.Parse(stim);
                                
                                if (inchk > Attend[0].starttime)
                                {
                                    lin = Convert.ToString(inchk - Attend[0].starttime);
                                }
                            }
                            
                        }
                        if (cout != "")
                        {

                            string etim = rd_time["outtime"].ToString();
                            outchk = TimeSpan.Parse(etim);
                            if (outchk > Attend[0].outtime)
                            {
                                lout = Convert.ToString(outchk - Attend[0].outtime);
                            }
                            else
                            {
                                e_out = Convert.ToString(Attend[0].outtime - outchk);
                            }
                        }
                        if (cin != "" && cout != "")
                        {
                            stats = "XX";
                            leav = "";
                            chkin = Convert.ToDateTime(cin);
                            cin = chkin.ToString("HH:mm:ss");
                            ein = TimeSpan.Parse(cin);
                            diff = Attend[0].break_intime - ein;
                            if (diff < Attend[0].halflimit)
                            {
                                stats = "AX";
                            }
                            if (Attend[0].stat == "Next Day")
                            {
                                stats = "XX";
                                leav = "";
                                chkin = Convert.ToDateTime(cin);
                                cin = chkin.ToString("HH:mm:ss");
                                ein = TimeSpan.Parse(cin);
                                diff = Attend[0].break_intime - ein;
                                if (diff > Attend[0].halflimit)
                                {
                                    stats = "AX";
                                }


                            }
                            chkout = Convert.ToDateTime(cout);
                            cout = chkout.ToString("HH:mm:ss");
                            eout = TimeSpan.Parse(cout);
                            diff = eout - Attend[0].break_outtime;
                            if (diff < Attend[0].halflimit)
                            {
                                stats = "XA";
                            }
                            string perm = Check_Permission(dt1);
                            if (perm != "")
                            {
                                if (perm == "FN")
                                { stats = "PX"; }
                                else
                                { stats = "XP"; }
                            }

                        }
                        else if (((cin == "" && cbout != "") || (cin != "" && cbout == "") || (cin == "" && cbout == "")) && cout != "")
                        {
                            stats = "AX";
                            leav = "Half Day";
                        }
                        else if (((cbin == "" && cout != "") || (cbin != "" && cout == "") || (cbin == "" && cout == "")) && cin != "" && (cbin != "" || cbout != ""))
                        {
                            stats = "XA";
                            leav = "Half Day";
                        }
                        else
                        {
                            stats = "AA";
                            leav = "Absent";
                        }


                    }
                    else
                    {
                        stats = "AA";
                        leav = "Absent";
                    }

                    SqlCommand cmdlv = new SqlCommand("Select a.pn_Leavecode , a.pn_leavename, a.from_status, a.status, b.type from leave_apply a, paym_leave b where a.pn_branchid = b.pn_branchID and a.pn_leavecode=b.pn_leavecode and a.pn_BranchID = '" + employee.BranchId + "' and a.pn_companyid = '" + employee.CompanyId + "' and a.from_date <= '" + txt_date + "' and a.to_date >= '" + txt_date + "' and a.emp_code = '" + ec + "' and a.flag='y'", myConnection);
                    SqlDataReader rdlv = cmdlv.ExecuteReader();
                    while (rdlv.Read())
                    {
                        string spt1 = stats.Substring(0, 1);
                        string spt2 = stats.Substring(1, 1);
                        string type = rdlv["type"].ToString();
                        if (rdlv["from_status"].ToString() == "FH")
                        {
                            stats = "L" + spt2;
                        }
                        else if (rdlv["from_status"].ToString() == "SH")
                        {
                            stats = spt1 + "L";
                        }
                        else if (rdlv["from_status"].ToString() == "FD")
                        {
                            stats = "LL";
                        }
                        if (type == "On Duty")
                        {
                            if (rdlv["from_status"].ToString() == "FH")
                            {
                                stats = "D" + spt2;
                            }
                            else if (rdlv["from_status"].ToString() == "SH")
                            {
                                stats = spt1 + "D";
                            }
                            else if (rdlv["from_status"].ToString() == "FD")
                            {
                                stats = "DD";
                            }
                        }
                        //stats = Convert.ToString(rdlv[0]);
                        leav = Convert.ToString(rdlv[1]);
                        cmd1 = new SqlCommand("update leave_apply set record = 'T' where from_date <= '" + txt_date + "' and to_date >= '" + txt_date + "' and emp_code = '" + ec + "' and emp_name = '" + en + "' and pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
                        cmd1.ExecuteNonQuery();
                    }

                    //SqlCommand cmdod = new SqlCommand("Select fstatus, tstatus from onduty where pn_branchid = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "' and onduty_dat <= '" + txt_date + "' and todat >= '" + txt_date + "' and empid = '" + employee.EmployeeId + "'", myConnection);
                    //SqlDataReader rdod = cmdod.ExecuteReader();
                    //while (rdod.Read())
                    //{
                    //    string spt1 = stats.Substring(0, 1);
                    //    string spt2 = stats.Substring(1, 1);
                    //    if (rdod["fstatus"].ToString() == "First Half")
                    //    {
                    //        stats = "D" + spt2;
                    //    }
                    //    else if (rdod["fstatus"].ToString() == "Second Half")
                    //    {
                    //        stats = spt1 + "D";
                    //    }
                    //    else if (rdod["fstatus"].ToString() == "Full Day")
                    //    {
                    //        stats = "DD";
                    //    }
                    //    //stats = Convert.ToString(rdlv[0]);
                    //    leav = "On Duty";

                    //}

                    SqlCommand cmdhd = new SqlCommand("Select pn_Holidaycode, pn_Holidayname from paym_holiday where pn_branchid = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "' and from_date <= '" + txt_date + "' and to_date >= '" + txt_date + "'", myConnection);
                    SqlDataReader rdhd = cmdhd.ExecuteReader();
                    if (rdhd.Read())
                    {
                        leav = Convert.ToString(rdhd[1]);
                        stats = "HH";
                    }


                    if (Attend[0].ShiftCode == "WW")
                    {
                        leav = "Week Off";
                        stats = "WW";
                    }

                    SqlCommand cmdotc = new SqlCommand("Select ot_calc from paym_employee where pn_branchid = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "'  and Employee_First_Name = '" + en + "'", myConnection);
                    SqlDataReader rdotc = cmdotc.ExecuteReader();
                    if (rdotc.Read())
                    {
                        otc = Convert.ToInt32(rdotc[0]);
                    }

                    if (cin != "" && cout != "")
                    {
                        chkin = Convert.ToDateTime(cin);
                        cin = chkin.ToString("HH:mm:ss");
                        ein = TimeSpan.Parse(cin);
                        chkout = Convert.ToDateTime(cout);
                        cout = chkout.ToString("HH:mm:ss");
                        eout = TimeSpan.Parse(cout);
                        int ttl_hr = (Attend[0].outtime - Attend[0].starttime).Hours;
                        int emp_hr = (eout - ein).Hours;

                        // DateTime dtim = Convert.ToDateTime(rd_time["outtime"]);
                        string etime = cout;  //dtim.ToString("HH:mm:ss");
                        daily_out = TimeSpan.Parse(etime);
                        if (otc == 1 && cout != "" && emp_hr >= ttl_hr)
                        {
                            ot_hours = daily_out - Attend[0].outtime;
                            if (ot_hours < System.TimeSpan.Parse("00:00:00"))
                            {
                                ot_hours = System.TimeSpan.Parse("00:00:00");
                            }

                            comm = new SqlCommand("select ot_slab from otslab where ot_from <= '" + ot_hours + "' and ot_to >= '" + ot_hours + "' and pn_branchID = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "'", myConnection);
                            SqlDataReader rdot = comm.ExecuteReader();
                            if (rdot.Read())
                            {
                                DateTime ottime = Convert.ToDateTime(rdot[0]);
                                string ottim = ottime.ToString("HH:mm:ss");
                                otslb = TimeSpan.Parse(ottim);
                            }
                        }
                    }

                    cmd = new SqlCommand("insert into temp_timecard values ('" + employee.CompanyId + "', '" + employee.BranchId + "' , '" + BranCode + "','" + ec + "','" + en + "','" + Attend[0].ShiftCode + "','" + txt_date + "','" + cur_day + "','" + cin + "','" + cbin + "','" + cbout + "','" + cout + "','" + lin + "','" + lout + "','" + e_out + "','" + otslb + "','" + leav + "','" + stats + "')", myConnection);
                    cmd.ExecuteNonQuery();

                    //cmd = new SqlCommand("create table temp_timecard3(pn_companyid int,pn_branchid int,machine_num int, card_no varchar(5) , emp_code varchar(10), emp_name varchar(50),VerifyMode int,InOutMode int, shift_code varchar(5),dates datetime,days varchar(15),intime time , break_out time, break_in time , outtime time, ot_hrs datetime , status varchar(2))", myConnection);
                    //cmd.ExecuteNonQuery();

                    //cmd = new SqlCommand("INSERT INTO temp_timecard3 SELECT DISTINCT * FROM daily_timecard_new where pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
                    //cmd.ExecuteNonQuery();

                    //cmd = new SqlCommand("drop table daily_timecard_new", myConnection);
                    //cmd.ExecuteNonQuery();

                    //cmd = new SqlCommand("EXEC sp_rename 'temp_timecard3', 'daily_timecard_new'", myConnection);
                    //cmd.ExecuteNonQuery();
                }
                myConnection.Close();

            }

            load1();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Data found for the one or more employees!');", true);
        }
    }

    public string Check_Permission(string dt1)
    {
        string pm = "", stat = "";
        string[] date1 = dt1.Split('/');
        string txt_date = date1[1] + "/" + date1[0] + "/" + date1[2];
        SqlCommand com = new SqlCommand("select * from paym_permission where companyID = '" + employee.CompanyId + "' and BranchID = '" + employee.BranchId + "' and EmployeeID = '" + employee.EmployeeId + "' and date = '" + txt_date + "'", myConnection);
        SqlDataReader read = com.ExecuteReader();
        if (read.Read())
        {
            stat = read[7].ToString();
            if (stat == "Y")
            {
                pm = read[6].ToString();
            }
        }
        return pm;
    }

    public Collection<Shift> Shift_Details(string dt1)
    {
        Collection<Shift> Shift = new Collection<Hrms_Employee_Default.Shift>();
        Shift sh = new Shift();
        //string dt1 = Convert.ToString(txt_fromdate.Text);
        string[] date1 = dt1.Split('/');
        string txt_date = date1[1] + "/" + date1[0] + "/" + date1[2];
        TimeSpan inchk = new TimeSpan(0, 0, 0);
        TimeSpan outchk = new TimeSpan(0, 0, 0);
        TimeSpan out_time = new TimeSpan(0, 0, 0);
        TimeSpan in_limit = new TimeSpan(0, 0, 0);
        TimeSpan breakin_limit = new TimeSpan(0, 0, 0);
        TimeSpan breakout_limit = new TimeSpan(0, 0, 0);
        TimeSpan start_time = new TimeSpan(0, 0, 0);
        TimeSpan breakin_time = new TimeSpan(0, 0, 0);
        TimeSpan breakout_time = new TimeSpan(0, 0, 0);
        TimeSpan otslb = new TimeSpan(0, 0, 0);
        TimeSpan diff = new TimeSpan(0, 0, 0);
        TimeSpan halfLmt = new TimeSpan(0, 0, 0);
        TimeSpan in_time, daily_in, daily_out, ot_hours, ein, eout;
        TimeSpan max_in1, max_breakout1, max_breakin1;
        string lin = "00:00:00", lout = "00:00:00", e_out = "00:00:00";
        DateTime inc, outc, chkin, chkout;
        string otime = "", stime = "", bintime = "", bouttime = "", dtime, intime_lmt, bin_lmt, hd_lmt, bout_lmt, time_stat, time_statin, time_statout, in_chk, bin_chk, hd_chk, bout_chk, str0, str1, str2, lvCode = "", lvName = "", hdCode = "", hdName = "";
        string cin, cbin, cbout, cout, ecod, stats, outdate, dat1, leav = "";
        int otc = 0;


        SqlCommand comm = new SqlCommand("select * from attendance_ceiling where pn_branchid='" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "'", myConnection);
        SqlDataReader rd_out = comm.ExecuteReader();
        if (rd_out.Read())
        {
            // Intime limit 
            time_stat = ""; time_statin = "";
            intime_lmt = Convert.ToString(rd_out["intime"]);
            string[] iti = intime_lmt.Split(' ');
            if (iti.Length == 3)
            {
                time_stat = Convert.ToString(iti[2]);
            }
            in_chk = Convert.ToString(iti[1]);
            string[] str_chk = in_chk.Split(':');
            str0 = Convert.ToString(str_chk[0]);
            if (str0 == "12" && time_stat == "AM")
            {
                in_limit = TimeSpan.Parse("00:" + str_chk[1] + ":00");
            }
            else
            {
                in_limit = TimeSpan.Parse(iti[1]);
            }
            //BreakIntime Limit
            bin_lmt = Convert.ToString(rd_out["lunch_ein"]);
            string[] biti = bin_lmt.Split(' ');
            if (biti.Length == 3)
            {
                time_statin = Convert.ToString(biti[2]);
            }
            bin_chk = Convert.ToString(biti[1]);
            string[] str_chkin = bin_chk.Split(':');
            str1 = Convert.ToString(str_chkin[0]);
            if (str1 == "12" && time_statin == "AM")
            {
                breakin_limit = TimeSpan.Parse("00:" + str_chkin[1] + ":00");
            }
            else
            {
                breakin_limit = TimeSpan.Parse(biti[1]);
            }
            // Half Day Limit
            hd_lmt = Convert.ToString(rd_out["halfday"]);
            string[] hd = hd_lmt.Split(' ');
            if (hd.Length == 3)
            {
                time_statin = Convert.ToString(hd[2]);
            }
            hd_chk = Convert.ToString(hd[1]);
            string[] str_chkhd = hd_chk.Split(':');
            str1 = Convert.ToString(str_chkhd[0]);
            if (str1 == "12" && time_statin == "AM")
            {
                halfLmt = TimeSpan.Parse("00:" + str_chkhd[1] + ":00");
            }
            else
            {
                halfLmt = TimeSpan.Parse(hd[1]);
            }

        }
        rd_out.Close();



        cmd = new SqlCommand("Select * from shift_month where date='" + txt_date + "' and pn_branchid ='" + employee.BranchId + "' and pn_Employeecode = '" + ec + "'", myConnection);
        SqlDataReader rd1 = cmd.ExecuteReader();
        if (rd1.Read())
        {
            sh.ShiftCode = Convert.ToString(rd1["Shift_Code"]);
        }
        else
        {
            sh.ShiftCode = "";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Shift Balance not found for employees');", true);
            GridView1.AutoGenerateEditButton = false;
            GridView1.AutoGenerateDeleteButton = false;
        }
        rd1.Close();

        //punch time populate

        comm = new SqlCommand("select * from shift_details where shift_code = '" + sh.ShiftCode + "' and pn_branchid='" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "'", myConnection);
        rd_out = comm.ExecuteReader();
        if (rd_out.Read())
        {
            DateTime st, bit, bot, et;
            shift = Convert.ToString(rd_out["shift_code"]);
            //start time
            st = Convert.ToDateTime(rd_out["start_time"]);
            stime = st.ToString("HH:mm:ss");
            start_time = TimeSpan.Parse(stime);
            //break out time
            bit = Convert.ToDateTime(rd_out["break_time_out"]);
            bintime = bit.ToString("HH:mm:ss");
            breakin_time = TimeSpan.Parse(bintime);
            //break in time
            bot = Convert.ToDateTime(rd_out["break_time_in"]);
            bouttime = bot.ToString("HH:mm:ss");
            breakout_time = TimeSpan.Parse(bouttime);
            //end time
            et = Convert.ToDateTime(rd_out["end_time"]);
            otime = et.ToString("HH:mm:ss");
            out_time = TimeSpan.Parse(otime);
            //Shift Indicator
            sh.stat = rd_out["Shift_indicator"].ToString();
        }
        rd_out.Close();
        //max_in1 = start_time + in_limit;
        //max_breakout1 = breakin_time + breakin_limit;
        //max_breakin1 = breakout_time + breakin_limit;
        sh.intime = start_time + in_limit;
        sh.break_intime = breakin_time;
        sh.break_outtime = breakout_time;
        sh.breakout = breakin_time + breakin_limit;
        sh.breakin = breakout_time + breakin_limit;
        sh.outtime = out_time;
        sh.starttime = start_time;
        sh.halflimit = halfLmt;
        Shift.Add(sh);
        return Shift;
    }

    protected void ddl_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_type.SelectedItem.Text == "Employee")
        {
            ddl_ename.Enabled = false;
            ddl_department.Enabled = true;
            txt_todate.Visible = true;
            lbl_to.Visible = true;
            lbl_from.Text = "Attendance From ";
            ddl_ename.ForeColor = System.Drawing.Color.Black;
            Image1.Visible = true;
            Image2.Visible = true;
            if (s_login_role == "u")
            {
                ddl_department.Enabled = false;
                ddl_ename.Enabled = true;
            }
            //txt_fromdate.Text = "";
        }
        else if (ddl_type.SelectedItem.Text == "All")
        {
            ddl_ename.Enabled = false;
            ddl_department.Enabled = false;
            txt_todate.Visible = false;
            lbl_to.Visible = false;
            lbl_from.Text = "Attendance Date";
            ddl_ename.ForeColor = System.Drawing.Color.LightGray;
            Image2.Visible = false;
            //ddl_ename.SelectedIndex = 0;
        }
    }
    protected void Btn_view_Click(object sender, EventArgs e)
    {
        try
        {
            ClearList();

            if (Btn_view.Text == "Save")
            {
                try
                {
                    string eid = "";
                    myConnection.Open();
                    foreach (GridViewRow gvr in GridView1.Rows)
                    {
                        eid = "";
                        cmd = new SqlCommand("select pn_EmployeeID from paym_employee where EmployeeCode = '" + ((Label)gvr.FindControl("lbl_empcode")).Text + "' and pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID = '" + employee.BranchId + "' and status = 'Y'", myConnection);
                        SqlDataReader rid = cmd.ExecuteReader();
                        if (rid.Read())
                        {
                            eid = Convert.ToString(rid[0]);
                        }

                        string datesp = ((Label)gvr.FindControl("lbl_date")).Text;
                        string[] dsp = datesp.Split('/', '-');
                        string findate = dsp[1] + "/" + dsp[0] + "/" + dsp[2];

                        cmd = new SqlCommand("delete from time_card where emp_code='" + ((Label)gvr.FindControl("lbl_empcode")).Text + "' and dates = '" + findate + "' and pn_companyid = '" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("insert into time_card(pn_companyid,pn_branchid, emp_code, emp_name, shift_code,dates,days,intime, Late_in, Late_out, early_out, outtime, ot_hrs, leave_code, status, data, pn_EmployeeID) values ('" + employee.CompanyId + "','" + employee.BranchId + "','" + ((Label)gvr.FindControl("lbl_empcode")).Text + "','" + ((Label)gvr.FindControl("lbl_empname")).Text + "','" + ((Label)gvr.FindControl("lbl_shiftcode")).Text + "','" + findate + "','" + ((Label)gvr.FindControl("lbl_day")).Text + "','" + ((Label)gvr.FindControl("lbl_starttime")).Text + "','" + ((Label)gvr.FindControl("lbl_breaktimei")).Text + "','" + ((Label)gvr.FindControl("lbl_breaktimeo")).Text + "','" + ((Label)gvr.FindControl("lbl_eout")).Text + "','" + ((Label)gvr.FindControl("lbl_endtime")).Text + "','" + ((Label)gvr.FindControl("lbl_othrs")).Text + "','" + ((Label)gvr.FindControl("lbl_leave")).Text + "','" + ((Label)gvr.FindControl("lbl_status")).Text + "','R','" + eid + "')", myConnection);
                        cmd.ExecuteNonQuery();
                    }


                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Attendance Saved Successfully');", true);
                    Btn_view.Text = "View";
                    Btn_view.CssClass = "btn btn-info";
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Attendance Saved Successfully');", true);
                }
                finally
                {
                    myConnection.Close();
                }

            }

            else if (Btn_view.Text == "View")
            {
                status = "View";
                ViewAttendance();
            }

            else
            {
                txt_fromdate.Text = "";
                txt_todate.Text = "";
                ddl_type.SelectedIndex = 0;
                ddl_ename.Enabled = false;
                Btn_view.Text = "View";
                Btn_view.CssClass = "btn btn-info";
                ddl_ename.ForeColor = System.Drawing.Color.White;
            }
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
        finally
        {
            myConnection.Close();
        }

    }

    public void ViewAttendance()
    {
        try
        {
            myConnection.Open();
            if (ddl_type.SelectedItem.Text == "Employee")
            {
                string stats = "";
                TimeSpan tot_ot = new TimeSpan(0, 0, 0);

                double abs = 0.0, prs = 0.0, hday = 0.0, ldays = 0.0, rc = 0.0, wkoff = 0.0;
                //DateTime othr;
                string[] name = ddl_ename.SelectedItem.Text.Split('-');
                string en = name[0];
                string fdate = txt_fromdate.Text;
                string tdate = txt_todate.Text;
                string[] fd = fdate.Split('/');
                string[] td = tdate.Split('/');
                string fr_date = fd[1] + "/" + fd[0] + "/" + fd[2];
                string to_date = td[1] + "/" + td[0] + "/" + td[2];
                SqlDataAdapter ada = new SqlDataAdapter("SELECT * FROM time_card where dates between '" + fr_date + "' and '" + to_date + "' and pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' and pn_employeeID = '" + ddl_ename.SelectedItem.Value + "' order by dates ", myConnection);

                DataSet dset = new DataSet();

                ada.Fill(dset, "time_card");


                if (dset.Tables[0].Rows.Count == 0)
                {
                    dset.Tables[0].Rows.Add(dset.Tables[0].NewRow());
                    GridView1.DataSource = dset;
                    GridView1.DataBind();
                    int columnCount = GridView1.Rows[0].Cells.Count;
                    GridView1.Rows[0].Cells.Clear();
                    GridView1.Rows[0].Cells.Add(new TableCell());
                    GridView1.Rows[0].Cells[0].ColumnSpan = columnCount;
                    GridView1.Rows[0].Cells[0].Text = "No Records Found..";
                }
                else
                {
                    GridView1.DataSource = dset;
                    GridView1.DataBind();
                }

                if (s_login_role == "a")
                {
                    cmd1 = new SqlCommand("SELECT * FROM time_card where dates between '" + fr_date + "' and '" + to_date + "' and pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + ddl_branch.Text + "' and pn_employeeId = '" + ddl_ename.SelectedItem.Value + "' order by dates ", myConnection);
                }

                if (s_login_role == "h")
                {
                    cmd1 = new SqlCommand("SELECT * FROM time_card where dates between '" + fr_date + "' and '" + to_date + "' and pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' and pn_employeeId = '" + ddl_ename.SelectedItem.Value + "' order by dates ", myConnection);
                }
                SqlDataReader rd_calc = cmd1.ExecuteReader();
                while (rd_calc.Read())
                {
                    rc++;

                    DateTime othr = Convert.ToDateTime(rd_calc["ot_hrs"]);
                    string otim = othr.ToString("HH:mm");
                    tot_ot += TimeSpan.Parse(otim);

                    stats = Convert.ToString(rd_calc["status"]);
                    if (stats == "XX")
                    {
                        prs += 1;
                    }

                    else if (stats == "LL")
                    {
                        ldays += 1;
                    }

                    else if (stats == "XA" || stats == "AX")
                    {
                        prs += 0.5;
                        abs += 0.5;
                    }

                    else if (stats == "XL" || stats == "LX")
                    {
                        prs += 0.5;
                        ldays += 0.5;
                    }

                    else if (stats == "LA" || stats == "AL")
                    {
                        ldays += 0.5;
                        abs += 0.5;
                    }

                    else if (stats == "AA")
                    {
                        abs += 1;
                    }
                    else if (stats == "WW")
                    {
                        wkoff += 1;
                    }

                    else if (stats == "HH")
                    {
                        hday += 1;
                    }
                }
                //string hours = tot_ot.Hours.ToString();
                //string min = tot_ot.Minutes.ToString();
                txt_tdays.Text = rc.ToString();
                txt_pdays.Text = prs.ToString();
                txt_adays.Text = abs.ToString();
                txt_ldays.Text = ldays.ToString();
                txt_hdays.Text = hday.ToString();
                txt_wdays.Text = wkoff.ToString();
                Btn_view.Text = "Save";
                Btn_view.CssClass = "btn btn-success";
            }

            else
            {
                string stats = "";
                TimeSpan tot_ot = new TimeSpan(0, 0, 0);

                int abs = 0, prs = 0, hday = 0, ldays = 0, rc = 0, wkoff = 0;
                //DateTime othr;
                string fdate = txt_fromdate.Text;
                string tdate = txt_todate.Text;
                string[] fd = fdate.Split('/');
                string[] td = tdate.Split('/');
                string fr_date = fd[1] + "/" + fd[0] + "/" + fd[2];
                //string to_date = td[1] + "/" + td[0] + "/" + td[2];
                SqlDataAdapter ada = new SqlDataAdapter("SELECT * FROM time_card where dates = '" + fr_date + "' and pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' order by dates ", myConnection);

                DataSet dset = new DataSet();

                ada.Fill(dset, "temp_timecard");


                if (dset.Tables[0].Rows.Count == 0)
                {
                    dset.Tables[0].Rows.Add(dset.Tables[0].NewRow());
                    GridView1.DataSource = dset;
                    GridView1.DataBind();
                    int columnCount = GridView1.Rows[0].Cells.Count;
                    GridView1.Rows[0].Cells.Clear();
                    GridView1.Rows[0].Cells.Add(new TableCell());
                    GridView1.Rows[0].Cells[0].ColumnSpan = columnCount;
                    GridView1.Rows[0].Cells[0].Text = "No Records Found..";
                }
                else
                {
                    GridView1.DataSource = dset;
                    GridView1.DataBind();
                }

                if (s_login_role == "h")
                {
                    cmd1 = new SqlCommand("SELECT * FROM time_card where dates = '" + fr_date + "' and pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' order by dates ", myConnection);
                }

                if (s_login_role == "a")
                {
                    cmd1 = new SqlCommand("SELECT * FROM time_card where dates = '" + fr_date + "'  and pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + ddl_branch.Text + "' order by dates ", myConnection);
                }
                //cmd1 = new SqlCommand("SELECT * FROM time_card where dates = '" + fr_date + "' and pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' order by dates ", myConnection);
                SqlDataReader rd_calc = cmd1.ExecuteReader();
                while (rd_calc.Read())
                {

                    DateTime othr = Convert.ToDateTime(rd_calc["ot_hrs"]);
                    string otim = othr.ToString("HH:mm");
                    tot_ot += TimeSpan.Parse(otim);

                    stats = Convert.ToString(rd_calc["status"]);
                    if (stats == "XX")
                    {
                        prs += 1;
                    }
                    else if (stats == "AA")
                    {
                        abs += 1;
                    }
                    else if (stats == "WW")
                    {
                        wkoff += 1;
                    }
                    else if (stats != "AA" && stats != "XX" && stats != "WW" && stats != "HH")
                    {
                        ldays += 1;
                    }
                    else if (stats == "HH")
                    {
                        hday += 1;
                    }
                }
                //string hours = tot_ot.Hours.ToString();
                //string min = tot_ot.Minutes.ToString();
                txt_tdays.Text = rc.ToString();
                txt_pdays.Text = prs.ToString();
                txt_adays.Text = abs.ToString();
                txt_ldays.Text = ldays.ToString();
                txt_hdays.Text = hday.ToString();
                txt_wdays.Text = wkoff.ToString();
                Btn_view.Text = "Save";
                Btn_view.CssClass = "btn btn-success";
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            myConnection.Close();
        }
    }

    protected void lb_previous_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    protected void ddl_branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //populate_ddlbranch();
        if (s_login_role == "a")
        {
            employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
        }
        SqlDataSource1.SelectCommand = "select employee_first_name,employeecode from paym_employee where pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + employee.BranchId + "' ";


        // ddl_ename.SelectedItem.Text = "Select";
    }
    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_ename.Enabled = true;
        ddl_Employee_load();
    }
    protected void Btn_update_Click(object sender, EventArgs e)
    {
        myConnection.Open();
        SqlCommand cmd = new SqlCommand();
        if (ddl_time.SelectedItem.Text == "Intime")
        {
            string intime = txt_mtime.Text + ":00";
            cmd = new SqlCommand("update temp_timecard set intime ='" + intime + "'", myConnection);
            cmd.ExecuteNonQuery();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (((Label)GridView1.Rows[i].FindControl("lbl_endtime")).Text != "00:00:00")
                {
                    cmd = new SqlCommand("update temp_timecard set leave_code ='',status='XX'", myConnection);
                    cmd.ExecuteNonQuery();
                    ((Label)GridView1.Rows[i].FindControl("lbl_leave")).Text = "Present";
                    ((Label)GridView1.Rows[i].FindControl("lbl_status")).Text = "XX";
                }
            }
        }
        else if (ddl_time.SelectedItem.Text == "Outtime")
        {
            string outtime = txt_mtime.Text + ":00";
            cmd = new SqlCommand("update temp_timecard set outtime ='" + outtime + "'", myConnection);
            cmd.ExecuteNonQuery();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (((Label)GridView1.Rows[i].FindControl("lbl_starttime")).Text != "00:00:00")
                {
                    cmd = new SqlCommand("update temp_timecard set leave_code ='',status='XX'", myConnection);
                    cmd.ExecuteNonQuery();
                    ((Label)GridView1.Rows[i].FindControl("lbl_leave")).Text = "Present";
                    ((Label)GridView1.Rows[i].FindControl("lbl_status")).Text = "XX";
                }
            }
        }
        myConnection.Close();
        load1();
        ddl_time.SelectedIndex = 0;
        txt_mtime.Text = "";

    }
    protected void txt_todate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToDateTime(txt_fromdate.Text) > Convert.ToDateTime(txt_todate.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Attendance To Date should not be less than From Date');", true);
                txt_todate.Text = "";
            }
        }
        catch
        {

        }
    }
}
