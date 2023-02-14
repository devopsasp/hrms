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

public partial class PayrollReports_ProjectDetail : System.Web.UI.Page
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
    char s_login_role;
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
    double act_basic, earned_basic, _Amount, Tot_amt, overheading;
    DateTime fromdate, todate;
    DataSet ds_userrights;
    int monthcount = 1;
    int presentdays = 1;
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

        if (!IsPostBack)
        {
            CompanyList = company.fn_getCompany();
            ListItem li = new ListItem();
            if (CompanyList.Count > 0)
            {
                switch (s_login_role)
                {
                    case 'a':
                        tbl_pfreport.Visible = false;
                        ddl_Branch.Visible = true;
                        break;

                    case 'h':
                        ddl_Branch.Visible = false;
                        tbl_pfreport.Visible = true;
                        ddl_department_load1();

                        break;

                    case 'u': s_form = "80";
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
                            ddl_department_load1();
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

    public void chkEmployee_load()
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

    public void hr()
    {
        myConnection.Open();
        SqlDataAdapter ad1 = new SqlDataAdapter("SELECT * FROM temp_allowance", myConnection);

        DataSet ds = new DataSet();

        ad1.Fill(ds, "temp_allowance");


        if (ds.Tables["temp_allowance"].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView2.DataSource = ds;
            GridView2.DataBind();
            int columnCount = GridView2.Rows[0].Cells.Count;
            GridView2.Rows[0].Cells.Clear();
            GridView2.Rows[0].Cells.Add(new TableCell());
            GridView2.Rows[0].Cells[0].ColumnSpan = columnCount;
            GridView2.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            GridView2.DataSource = ds;
            GridView2.DataBind();
        }
        myConnection.Close();
    }

    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkall.Checked = false;
        
        if (ddl_type.SelectedItem.Text == "Employee Vs Bench")
        {
            
            div_chk_Empcode.Visible = true;
            chkEmployee_load();
            query = "truncate table temp_allowance";
            employee.fn_reportbyid(query);
            hr();
            allowance1();
            div_chk_Empcode.Visible = true;
            div_chkempcode.Visible = true;
            chkall.Visible = true;
            lbl_selectemp.Visible = true;

        }
        else if (ddl_type.SelectedItem.Text == "Bench Vs OverHeading")
        {
            div_chk_Empcode.Visible = false;
            ddlEmployee_load();
            
        }
        

    }

    public void allowance1()
    {
        EmployeeList = employee.fn_getEarningsList_Regular(employee);


        if (EmployeeList.Count > 0)
        {
            for (ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();
                    e_list.Text = "Select Allowance";
                    e_list.Value = "0";
                    DropDownList earn = (DropDownList)GridView2.FooterRow.FindControl("txtallowance");
                    earn.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();
                    e_list.Value = EmployeeList[ddl_i].EarningsId.ToString();
                    e_list.Text = EmployeeList[ddl_i].EarningsName.ToString();
                    DropDownList earn = (DropDownList)GridView2.FooterRow.FindControl("txtallowance");
                    earn.Items.Add(e_list);
                }
            }
        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Add")
            {
                string allowance = ((DropDownList)GridView2.FooterRow.FindControl("txtallowance")).SelectedItem.Text;
                string value = ((TextBox)GridView2.FooterRow.FindControl("txtvalue")).Text;

                string strSQL = "";
                myConnection.Open();
                strSQL = "INSERT INTO temp_allowance (allowance,amt) VALUES ('" + allowance + "','" + value + "')";
                cmd1 = new SqlCommand(strSQL, myConnection);
                int c = cmd1.ExecuteNonQuery();
                myConnection.Close();
                hr();
                allowance1();
            }
        }
        catch (Exception ex)
        {

        }

    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strID = ((TextBox)GridView2.Rows[e.RowIndex].FindControl("txtid")).Text;
        string strSQL = "";

        try
        {
            strSQL = "DELETE from temp_allowance WHERE ID = " + strID + "";
            SqlDataSource dsTemp = new SqlDataSource();
            dsTemp.ConnectionString = ConfigurationManager.AppSettings["Connectionstring"];
            dsTemp.DeleteCommand = strSQL;
            dsTemp.Delete();
            hr();
            allowance1();
        }
        
        catch (Exception ex)
        {

        }
    }
    protected void btn_Report_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ddl_type.SelectedItem.Text == "Employee Vs Bench")
            {
                query = "delete from temp_emp_projects";
                employee.fn_reportbyid(query);
                myConnection.Open();
                for (i = 0; i < chk_Empcode.Items.Count; i++)
                {
                    if (chk_Empcode.Items[i].Selected == true)
                    {
                        date1 = txtFromDate.Text;
                        date2 = txtToDate.Text;

                        sd = date1.Split('/');
                        from_month = Convert.ToInt32(sd[1]);

                        ed = date2.Split('/');
                        to_month = Convert.ToInt32(ed[1]);

                        act_basic = 0; earned_basic = 0; Tot_amt = 0;
                        _Amount = 0;
                        for (int j = from_month; j <= to_month; j++)
                        {

                            cmd1 = new SqlCommand("select (Employee_First_Name) as Emp_name,basic_salary from paym_employee where pn_employeeid='" + chk_Empcode.Items[i].Value + "' and pn_companyid='" + employee.CompanyId + "' and pn_Branchid='" + employee.BranchId + "'", myConnection);
                            SqlDataReader dreader = cmd1.ExecuteReader();
                            if (dreader.Read())
                            {
                                empname = dreader["Emp_name"].ToString();
                                act_basic += Convert.ToDouble(dreader["basic_salary"].ToString());
                            }

                            cmd1 = new SqlCommand("Select isnull(sum(amt),0) as Amount from temp_allowance", myConnection);
                            SqlDataReader dr = cmd1.ExecuteReader();
                            if (dr.Read())
                            {
                                earned_basic += Convert.ToDouble(dr["Amount"].ToString());
                            }
                            if (chkoverheading.Checked == false)
                            {

                                Tot_amt = act_basic + earned_basic;



                            }
                            else if (chkoverheading.Checked == true)
                            {
                                cmd1 = new SqlCommand("Select isnull(sum(Amount),0) as Amount from paym_emp_OverHeading where pn_employeeid='" + chk_Empcode.Items[i].Value + "' and pn_companyid='" + employee.CompanyId + "' and pn_Branchid='" + employee.BranchId + "'", myConnection);
                                SqlDataReader rdr = cmd1.ExecuteReader();
                                if (rdr.Read())
                                {
                                    overheading += Convert.ToDouble(rdr["Amount"].ToString());
                                }
                                Tot_amt = act_basic + earned_basic + overheading;


                            }

                            fromdate = fromdate.AddMonths(+1);
                        }
                        query = "set dateformat dmy;insert into temp_emp_projects values('" + chk_Empcode.Items[i].Value + "', '" + empname + "','" + txtFromDate.Text + "','" + txtToDate.Text + "','"+presentdays+"','" + act_basic + "','" + overheading + "','" + Tot_amt + "');set dateformat mdy";
                        employee.fn_reportbyid(query);
                        temp_count++;

                    }


                }

                if (temp_count > 0)
                {
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
                    Session["Repordid"] = "Bench";
                    Session["fdate"] = txtFromDate.Text;
                    Session["tdate"] = txtToDate.Text;
                    if (chkoverheading.Checked == true)
                    {
                        Session["ReportName"] = "~/crystalreports/BenchReport.rpt";
                    }
                    else if (chkoverheading.Checked == false)
                    {
                        Session["ReportName"] = "~/crystalreports/BenchReport1.rpt";
                    }
                    Session["preview_page"] = "~/ProjectDetail.aspx";
                    Response.Redirect("Report_view.aspx", false);
                }
                else
                {
                    lbl_error.Text = "No Employee Selected";
                }
                myConnection.Close();
            }
            else if (ddl_type.SelectedItem.Text == "Bench Vs OverHeading")
            {
                query = "delete from temp_emp_projects";
                employee.fn_reportbyid(query);
                myConnection.Open();
                act_basic = 0; earned_basic = 0; Tot_amt = 0;

                act_basic = Convert.ToDouble(txtwork.Text);

                date1 = txtFromDate.Text;
                date2 = txtToDate.Text;

                sd = date1.Split('/');
                from_month = Convert.ToInt32(sd[1]);

                ed = date2.Split('/');
                to_month = Convert.ToInt32(ed[1]);
                cmd1 = new SqlCommand("select (Employee_First_Name) as Emp_name from paym_employee where pn_employeeid='" + ddl_Employee.SelectedItem.Value + "' and pn_companyid='" + employee.CompanyId + "' and pn_Branchid='" + employee.BranchId + "'", myConnection);
                SqlDataReader dreader = cmd1.ExecuteReader();
                if (dreader.Read())
                {
                    empname = dreader["Emp_name"].ToString();
                }
                for (int j = from_month; j <= to_month; j++)
                {
                    cmd1 = new SqlCommand("Select isnull(sum(Amount),0) as Amount from paym_emp_OverHeading where pn_employeeid='" + ddl_Employee.SelectedItem.Value + "' and pn_companyid='" + employee.CompanyId + "' and pn_Branchid='" + employee.BranchId + "'", myConnection);
                    SqlDataReader rdr = cmd1.ExecuteReader();
                    if (rdr.Read())
                    {
                        overheading += Convert.ToDouble(rdr["Amount"].ToString());
                    }
                    _Amount += act_basic;
                    Tot_amt = overheading + _Amount;
                    fromdate = fromdate.AddMonths(+1);
                }
                query = "set dateformat dmy;insert into temp_emp_projects values('" + ddl_Employee.SelectedItem.Value + "', '" + empname + "','" + txtFromDate.Text + "','" + txtToDate.Text + "','"+presentdays+"','" + _Amount + "','" + overheading + "','" + Tot_amt + "');set dateformat mdy";
                employee.fn_reportbyid(query);


                //sd = txtFromDate.Text.Split('/');
                //ed = txtToDate.Text.Split('/');
                //int mon = Convert.ToInt32(sd[0]);
                //todate = Convert.ToDateTime(txtFromDate.Text);
                //_Month = todate.ToString("MMMMMMMMMMMMMMMMM");
                //_Year = Convert.ToInt32(sd[2]);
                //txtFromDate.Text = _Month + " " + (mon + " " + _Year).ToString();

                //mon = Convert.ToInt32(ed[0]);
                //todate = Convert.ToDateTime(txtToDate.Text);
                //_Month = todate.ToString("MMMMMMMMMMMMMMMMM");
                //_Year = Convert.ToInt32(ed[2]);
                //txtToDate.Text = _Month + " " + (mon + " " + _Year).ToString();
                //Session["Repordid"] = "Bench";
                //Session["fdate"] = txtFromDate.Text;
                //Session["tdate"] = txtToDate.Text;
                Session["ReportName"] = "~/crystalreports/BenchVsOverheading.rpt";

                Session["preview_page"] = "~/ProjectDetail.aspx";
                Response.Redirect("Report_view.aspx", false);

                myConnection.Close();
            }
        }
        catch (Exception ex)
        {
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
    protected void ddl_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_department.Items.Clear();
        chk_Empcode.Items.Clear();
        lbl_selectemp.Visible = false;
        chkall.Visible = false;
        div_chkempcode.Visible = false;
        chkall.Checked = false;
        chkoverheading.Checked = false;
        ddl_Employee.Items.Clear();
        txtwork.Text = "";
        txtFromDate.Text = "";
        txtToDate.Text = "";
        lbl_error.Text = "Welcome to Report Section";
        if (ddl_type.SelectedItem.Text == "Employee Vs Bench")
        {
            ddl_department_load1();
            
            Tr2.Visible = false;
            Tr3.Visible = false;
            GridView2.Visible = true;
            chkoverheading.Visible = true;
        }
        else if (ddl_type.SelectedItem.Text == "Bench Vs OverHeading")
        {
            ddl_department_load1();
            div_chk_Empcode.Visible = false;
            Tr2.Visible = true;
            Tr3.Visible = true;
            GridView2.Visible = false;
            chkoverheading.Visible = false;
            chk_Empcode.Items.Clear();
            lbl_selectemp.Visible = false;
            chkall.Visible = false;
            div_chkempcode.Visible = false;
            chkall.Checked = false;
        }
    }
}