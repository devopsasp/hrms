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

public partial class Bank_Loan_Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand com = new SqlCommand();

    Company company = new Company();
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    Candidate c = new Candidate();
    Collection<Candidate> WorkHistoryList;
    //Collection<Employee> AllowanceList;
    Collection<Company> CompanyList, ddlBranchsList;
    Collection<Employee> EmployeesList;
    Collection<Employee> EmployeeList;
    Collection<PayRoll> emp_Earn_List;
    Collection<PayRoll> AttendanceList;
    Collection<PayRoll> RegEarningsList;
    Collection<PayRoll> RegDeductionList;
    Collection<PayRoll> NonRegEarningsList;
    Collection<PayRoll> NonRegDeductionList;
    Collection<PayRoll> PFList;
    Collection<PayRoll> ESIList;
    Collection<PayRoll> PTList;
    Collection<PayRoll> ITList;
    Collection<PayRoll> LoanEntryList;
    Collection<PayRoll> LoanCancelList;
    Collection<PayRoll> LoanPreCloserList;
    Collection<PayRoll> PayOutput_LoanList;
    Collection<PayRoll> payinput;
    Collection<PayRoll> PT_Settings;
    Collection<PayRoll> OT_Settings;
    Collection<PayRoll> LWF_Settings;
    Collection<PayRoll> Bonus_Settings;
    Collection<PayRoll> CheckingList;
    DataSet ds_PT;
    DataSet ds_userrights;

    int i_amt = 0, i_temp = 0, NetPay = 0, i_emp, i_regearn, i_nonregearn, i_regded, i_nonregded, i_Precloser, i_loan, i_loan_ex, i_length, emp_count = 0;

    double abs = 0, prs = 0, hday = 0, ldays = 0, rc = 0, wkoff = 0, total_wdays = 0, paid = 0, odd = 0, cod = 0, tod = 0, att_amt = 0, basic = 0, ern_basic = 0, ot_calc = 0, ot_amt = 0, e_arr = 0, d_arr = 0;
    double d_temp = 0, A_calcdays = 0, A_paiddays = 0, A_Absentdays = 0, A_WeekOffdays = 0, E_amt = 0, E_amt_A = 0, E_actamt = 0, E_actamt_A = 0, N_E_amt = 0, N_E_actamt = 0, D_amt = 0, D_actamt = 0;
    double N_D_amt = 0, N_D_actamt = 0, f_ot = 0, f_pt = 0, f_PF = 0, f_ESI = 0, cal_amt = 0, PFAmt = 0, EPFAmt = 0;
    double Emp_ESIAmt = 0, Er_ESIAmt = 0, Emp_BasAmt = 0, Er_BasAmt = 0, OT_Amt = 0, PT_Amt = 0, IT_Amt = 0, Loan_amt = 0, Loan_Actamt = 0;
    double FPFAmt = 0, Balance_Amt = 0, Net_Earn = 0, Net_Ded = 0, Net_Act_Earn = 0, Net_Act_Earn_A = 0, Net_Act_Ded = 0;

    string s_login_role;
    string str_temp = "", str_query = "", _path = "", str_Month = "", str_Year = "", Query = "", s_form, _value="";
    int ddl_i;


    protected void Page_Load(object sender, EventArgs e)
    {
        
        //pay.CompanyId = 1;
        //pay.BranchId = 1;
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);   
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        //c.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);

         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);       

        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        lbl_Error.Text = "";
        pay.Empname = "";
        pay.pay_mode = "";

        if (!IsPostBack)
        {          
            period_load();
            //ddl_load();
            CompanyList = company.fn_getCompany();
            if (CompanyList.Count > 0)
            {
                //ddl_year_load();
                switch (s_login_role)
                {
                    case "a":
                        Row_Emplist.Visible = false;
                        ddl_Branch.Visible = true;
                        ddl_Branch_load();
                        break;

                    case "h":
                        Row_Emplist.Visible = false;
                        ddl_Branch.Visible = false;
                        ddl_department_load();                      
                        break;

                    case "u":
                        s_form = "44";

                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            ddl_Branch.Visible = false;
                        }
                        else
                        {
                            Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator.";
                            Response.Redirect("~/Company_Home.aspx");
                        }
                        //hr();
                        //session_check();
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

    public void load()
    {
        //lbl_Error.Text = DateTime.Now.ToString();
        pay.d_date = employee.Convert_ToSqlDate(date_change(txt_fromdate.Text));
        pay.temp_str = "Set Dateformat dmy;select ed.pn_EmployeeID,ed.pn_EarningsID,ed.Amount,ed.d_Date,e.EmployeeCode,d.v_EarningsCode from payoutput_earnings ed,paym_Employee e,paym_Earnings d where ed.d_Date='" + pay.d_date + "' and e.pn_EmployeeID=ed.pn_EmployeeID and ed.pn_EmployeeID = '" + ddl_Employee.SelectedValue + "' and d.pn_EarningsID=ed.pn_EarningsID and ed.pn_CompanyID='" + employee.CompanyId + "' and ed.pn_BranchID='" + employee.BranchId + "';Set Dateformat mdy;";
        SqlDataAdapter ad = new SqlDataAdapter(pay.temp_str, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        //emp_Earn_List = pay.fn_Emp_NonEarnings(pay);
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
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }
    }

    public void ddl_employee_Chklist()
    {
        try
        {
            Row_Emplist.Visible = false;
            if (s_login_role == "a")
            {
                employee.BranchId = Convert.ToInt32(ddl_Branch.SelectedItem.Value);
                pay.BranchId = Convert.ToInt32(ddl_Branch.SelectedItem.Value);
            }

            Collection<Employee> EmployeeList = employee.fn_getEmployeeList(employee);
            if (EmployeeList.Count > 0)
            {
                chk_Empcode.Visible = true;
                chk_Empcode.DataSource = EmployeeList;
                chk_Empcode.DataValueField = "EmployeeId";
                chk_Empcode.DataTextField = "FirstName";
                chk_Empcode.DataBind();
                for (int em = 0; em < chk_Empcode.Items.Count; em++)
                {
                    chk_Empcode.Items[em].Selected = true;
                }
            }
            else
            {
                chk_Empcode.Visible = false;
                lbl_Error.Text = "No Employees Available";
            }
        }
        catch (Exception ex)
        {

        }
    }

    public void load1()
    {
        pay.d_date = employee.Convert_ToSqlDate(date_change(txt_fromdate.Text));
        pay.temp_str = "set dateformat dmy;select distinct ed.pn_EarningsID,ed.Amount,ed.d_Date,d.v_EarningsCode from payoutput_earnings ed,paym_Employee e,paym_Earnings d where ed.d_Date = '" + pay.d_date + "' and ed.flag = 'S' and ed.Mode = 'A' and ed.pn_DepartmentName = '" + ddl_department.SelectedItem.Text + "' and e.pn_EmployeeID=ed.pn_EmployeeID and d.pn_EarningsID=ed.pn_EarningsID and ed.pn_CompanyID='" + employee.CompanyId + "' and ed.pn_BranchID='" + employee.BranchId + "';set dateformat mdy";
        SqlDataAdapter ad = new SqlDataAdapter(pay.temp_str, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        //emp_Earn_List = pay.fn_Emp_NonEarnings(pay);
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
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
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

    protected void ddl_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Branch.SelectedValue != "0")
        {
            ViewState["NonEarn_BranchID"] = Convert.ToInt32(ddl_Branch.SelectedValue);
        }
    }

    public void period_load()
    {
        con.Open();
        com = new SqlCommand("Select top 10 * from salary_period where pn_branchid = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "' order by from_date desc", con);
        SqlDataReader re = com.ExecuteReader();
        while (re.Read())
        {
            ddl_periodcode.Items.Add(re["period_code"].ToString());
        }
        re.Close();
        con.Close();
    }

    protected void ddl_periodcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_periodcode.SelectedItem.Text != "Select")
        {
            con.Open();
            com = new SqlCommand("Select * from salary_period where pn_branchid = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "' and period_code='" + ddl_periodcode.SelectedItem.Text + "'", con);
            SqlDataReader re = com.ExecuteReader();
            if (re.Read())
            {
                txt_fromdate.Text = Convert.ToDateTime(re["from_date"]).ToString("dd/MM/yyyy");
                txt_todate.Text = Convert.ToDateTime(re["to_date"]).ToString("dd/MM/yyyy");
            }
            re.Close();
            con.Close();
            txt_fromdate.Enabled = false;
            txt_todate.Enabled = false;
            ddl_Employee.Enabled = true;
            ddl_department.Enabled = true;
        }
        else
        {
            txt_fromdate.Enabled = false;
            txt_todate.Enabled = false;
            ddl_Employee.Enabled = false;
            ddl_department.Enabled = false;
            txt_fromdate.Text = "";
            txt_todate.Text = "";
            ddl_department.SelectedIndex = 0;
            ddl_Employee.SelectedIndex = 0;
        }
    }
    
    public void ddl_department_load()
    {


        if (s_login_role == "a")
        {
            employee.BranchId = Convert.ToInt32(ddl_Branch.SelectedItem.Value);
        }
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

    protected void chk_empname_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_department.SelectedValue != "sd")
        {
            ddl_employee_load();
        }
    }

    public void ddl_employee_load()
    {
        //employee dropdown
        ddl_Employee.Items.Clear();

        if (s_login_role == "a")
        {
            employee.BranchId = (int)ViewState["Appraisal_BranchID"];
        }

        if (s_login_role == "h")
        {
            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        }

        str_query = "Select a.pn_EmployeeID, a.EmployeeCode, a.Employee_First_Name from paym_employee a, paym_employee_profile1 b where a.pn_CompanyID=b.pn_CompanyID and a.pn_BranchID=b.pn_BranchID and a.pn_EmployeeID=b.pn_EmployeeID and b.pn_DepartmentID=" + ddl_department.SelectedValue + " and b.pn_CompanyID=" + employee.CompanyId + " and a.status='y' and b.pn_BranchID=" + employee.BranchId;

        EmployeeList = employee.fn_getEmplist(str_query);

        if (EmployeeList.Count > 0)
        {
            for (ddl_i = -2; ddl_i < EmployeeList.Count; ddl_i++)
            {
                if (ddl_i == -2)
                {
                    ListItem e_list = new ListItem();
                    e_list.Text = "All";
                    e_list.Value = "All";
                    ddl_Employee.Items.Add(e_list);
                }
                else if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();
                    e_list.Text = "Show All Employees";
                    e_list.Value = "Show All Employees";
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
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Employee');", true);
        }
        ddl_employee_Chklist();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        load();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "add")
        {
            string amt = ((TextBox)GridView1.FooterRow.FindControl("txt_amount")).Text;
            string alln = ((DropDownList)GridView1.FooterRow.FindControl("ddl_allowance")).Text;
            string allc = ((DropDownList)GridView1.FooterRow.FindControl("ddl_allowance")).SelectedValue;
            con.Close();
            
            AddNewRecord(alln,allc,amt);
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string ID = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lbl_all")).Text;
        string amt = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("Label12")).Text;

        DeleteRecord(ID,amt);
        load();
    }

    private void DeleteRecord(string ID, string amt)
    {

        string sqlStatement = "Set Dateformat dmy;DELETE FROM payoutput_earnings WHERE pn_EmployeeiD= @pn_EmployeeID and pn_EarningsID = @pn_EarningsID and Amount = @Amount and d_Date = @d_Date and pn_CompanyID = @Pn_CompanyID and pn_BranchID = @pn_BranchID;Set Dateformat mdy; ";
        try
        {
            pay.d_date = employee.Convert_ToSqlDate(date_change(txt_fromdate.Text));
            con.Open();
            SqlCommand cmd = new SqlCommand(sqlStatement, con);
            cmd.Parameters.AddWithValue("@pn_EmployeeID", ddl_Employee.SelectedValue);
            cmd.Parameters.AddWithValue("@pn_EarningsID", ID);
            cmd.Parameters.AddWithValue("@Amount", amt);
            cmd.Parameters.AddWithValue("@d_Date", pay.d_date);
            cmd.Parameters.AddWithValue("@pn_CompanyID", pay.CompanyId);
            cmd.Parameters.AddWithValue("@pn_BranchID", pay.BranchId);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Deletion Error:";
            msg += ex.Message;
            throw new Exception(msg);

        }
        finally
        {
            con.Close();
        }
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex; // turn to edit mode
        if (ddl_Employee.SelectedItem.Text == "All")
        {
            load1();
        }
        else
        {
            load();
        }
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow Gvrow = GridView1.Rows[e.RowIndex];
        if (Gvrow != null)
        {
            pay.d_date = employee.Convert_ToSqlDate(date_change(txt_fromdate.Text));
            string Earningsname = ((Label)Gvrow.FindControl("ddl_eallowance")).Text;
            string EarningsCode = ((Label)Gvrow.FindControl("lbl_alledit")).Text;
            string amt = ((TextBox)Gvrow.FindControl("txt_eamount")).Text;

            
            if (ddl_Employee.SelectedItem.Text == "All")
            {
                con.Open();
                com = new SqlCommand("Set Dateformat dmy;update payoutput_earnings set Amount='" + amt + "' where d_date='" + pay.d_date + "' and pn_earningsID='" + EarningsCode + "';Set Dateformat mdy; ", con);
                com.ExecuteNonQuery();
                con.Close();
                GridView1.EditIndex = -1; // turn to edit mode
                load1();
            }
            else
            {
                con.Open();
                com = new SqlCommand("Set Dateformat dmy;update payoutput_earnings set Amount='" + amt + "' where d_date='" + pay.d_date + "' and pn_earningsID='" + EarningsCode + "' and pn_EmployeeID = '" + ddl_Employee.SelectedValue + "';Set Dateformat mdy; ", con);
                com.ExecuteNonQuery();
                con.Close();
                GridView1.EditIndex = -1; // turn to edit mode
                load();
            }
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void AddNewRecord(string alln, string allc, string amt)
    {
        pay.d_FromDate = employee.Convert_ToSqlDate(txt_fromdate.Text);
        pay.d_ToDate = employee.Convert_ToSqlDate(txt_todate.Text);
        pay.d_date = employee.Convert_ToSqlDate(date_change(txt_fromdate.Text));
        pay.EarningsId = Convert.ToInt32(allc);
        pay.Empname = ddl_department.SelectedItem.Text;
        pay.status = 'S';
        if (ddl_Employee.SelectedItem.Text == "All")
        {
            pay.pay_mode = "A";
        }
        else
        {
            pay.pay_mode = "I";
        }
        pay.Earn_Amount = Convert.ToDouble(amt);
        pay.Act_Amount = 0.0;
        if (ddl_Employee.SelectedItem.Text == "All")
        {
            for (i_emp = 1; i_emp < ddl_Employee.Items.Count; i_emp++)
            {
                pay.EmployeeId = Convert.ToInt32(ddl_Employee.Items[i_emp].Value);
                employee.EmployeeId = Convert.ToInt32(ddl_Employee.Items[i_emp].Value);
                pay.PayOutput_Earnings(pay);
            }
            load1();
        }
        else
        {
            string query = @"Set Dateformat dmy;INSERT INTO payoutput_earnings (pn_CompanyID, pn_BranchID, pn_EmployeeID,pn_EarningsID,d_Date,d_From_Date,d_To_Date,Mode,Flag,Amount) VALUES('" + employee.CompanyId + "','" + employee.BranchId + "','" + ddl_Employee.SelectedValue + "','" + allc + "','" + pay.d_date + "','" + pay.d_FromDate + "','" + pay.d_ToDate + "','I','S','" + amt + "');Set Dateformat mdy;";

            SqlCommand myCommand = new SqlCommand(query, con);

            con.Open();

            myCommand.ExecuteNonQuery();

            con.Close();

            load();
        }
    }


    public void Initial_Processing()
    {
        PT_Settings = pay.fn_In_PTax_Settings(pay);
        if (PT_Settings.Count > 0)
        {
            for (i_emp = 0; i_emp < chk_Empcode.Items.Count; i_emp++)
            {
                abs = 0; prs = 0; hday = 0; ldays = 0; rc = 0; wkoff = 0; total_wdays = 0; paid = 0; odd = 0; cod = 0; tod = 0; att_amt = 0; basic = 0; ern_basic = 0; ot_calc = 0; ot_amt = 0; e_arr = 0; d_arr = 0;
                N_D_amt = 0; N_D_actamt = 0; f_ot = 0; f_pt = 0; f_PF = 0; f_ESI = 0; cal_amt = 0; PFAmt = 0; EPFAmt = 0;
                Emp_ESIAmt = 0; Er_ESIAmt = 0; Emp_BasAmt = 0; Er_BasAmt = 0; OT_Amt = 0; PT_Amt = 0; IT_Amt = 0; Loan_amt = 0; Loan_Actamt = 0;
                FPFAmt = 0; Balance_Amt = 0; Net_Earn = 0; Net_Ded = 0; Net_Act_Earn = 0; Net_Act_Earn_A = 0; Net_Act_Ded = 0;
                if (chk_Empcode.Items[i_emp].Selected == true)
                {
                    pay.EmployeeId = Convert.ToInt32(chk_Empcode.Items[i_emp].Value);
                    employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[i_emp].Value);
                    payinput = pay.fn_payinput_check(pay);

                    if (payinput.Count <= 0)
                    {
                        Month_Attendance();
                    }
                    AttendanceList = pay.fn_In_PayInput(pay);
                    if (AttendanceList.Count > 0)
                    {
                        A_calcdays = AttendanceList[0].Calc_Days;
                        A_Absentdays = AttendanceList[0].Absent_Days;
                        A_WeekOffdays = AttendanceList[0].WeekOffDays;
                        A_paiddays = AttendanceList[0].Paid_Days;
                        Processing();
                        emp_count++;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Attendance details not found for the selected month');", true);
                    }
                }
            }

        }
        else
        {
            lbl_Error.Text = "Enter the Professional Tax settings";
        }
    }

    public void Processing()
    {
        Reg_Earnings();
    }


    public void Month_Attendance()
    {
        try
        {
            con.Open();
            string stats = "", att_bonus = "";
            TimeSpan tot_ot = new TimeSpan(0, 0, 0);

            //DateTime othr;
            pay.strDate = employee.Convert_ToSqlDatestring(date_change(txt_fromdate.Text));
            pay.d_date = employee.Convert_ToSqlDate(date_change(txt_todate.Text));
            string fdate = txt_fromdate.Text;
            string tdate = txt_todate.Text;
            string pdate = "";
            string[] fd = fdate.Split('/');
            string[] td = tdate.Split('/');
            string[] pd = pdate.Split('/');
            string fr_date = fd[1] + "/" + fd[0] + "/" + fd[2];
            string to_date = td[1] + "/" + td[0] + "/" + td[2];
            string p_date = pd[1] + "/" + pd[0] + "/" + pd[2];
            pay.Str_From_date = fr_date;
            pay.Str_To_date = to_date;
            tot_ot = TimeSpan.Parse("00:00:00");
            abs = 0; prs = 0; hday = 0; ldays = 0; rc = 0; wkoff = 0; paid = 0; OT_Amt = 0; basic = 0; ern_basic = 0;

            com = new SqlCommand("SELECT * FROM time_card where dates between '" + fr_date + "' and '" + to_date + "' and pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' and pn_EmployeeID = '" + pay.EmployeeId + "' order by dates ", con);

            SqlDataReader rd_calc = com.ExecuteReader();
            if (rd_calc.HasRows)
            {
                while (rd_calc.Read())
                {
                    rc++;
                    DateTime othr = Convert.ToDateTime(rd_calc["ot_hrs"]);
                    string otim = othr.ToString("HH:mm");
                    tot_ot += TimeSpan.Parse(otim);

                    stats = Convert.ToString(rd_calc["status"]);
                    if (stats == "XX" || stats == "LL")
                    {
                        prs += 1;
                    }
                    else if (stats == "XA" || stats == "AX" || stats == "LA" || stats == "AL")
                    {
                        prs += 0.5;
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
            }
            else
            {
                return;
            }
            //  OnDuty_days, Compoff_Days, Tour_Days, Att_Bonus, Att_BonusAmount, 
            rd_calc.Close();

            string ty = "";
            com = new SqlCommand("SELECT * FROM attendance_ceiling where pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' ", con);
            SqlDataReader rd_ce = com.ExecuteReader();
            if (rd_ce.Read())
            {
                ty = Convert.ToString(rd_ce["month_type"]);
            }
            if (ty == "Week-off ex")
            {
                paid = prs + ldays + hday;
            }
            else
            {
                paid = prs + ldays + wkoff + hday;
            }
            double ovt = 0, otround = 0;
            string str_ot = Convert.ToString(tot_ot);
            string[] ot_spl = str_ot.Split(':');
            if (ot_spl[1] == "15")
            {
                otround = 0.25;
            }
            else if (ot_spl[1] == "30")
            {
                otround = 0.5;
            }
            else if (ot_spl[1] == "45")
            {                                                                                                                                                                                  
                otround = 0.75;
            }
            else
            {
                otround = 0;
            }
            ovt = Convert.ToDouble(ot_spl[0]) + otround;

            com = new SqlCommand("SELECT top 1 * FROM salary_structure where  pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' and pn_EmployeeID = '" + pay.EmployeeId + "' and effective_date <= '" + fr_date + "' order by effective_date desc ", con);
            SqlDataReader rd_bas = com.ExecuteReader();
            if (rd_bas.Read())
            {
                basic = Convert.ToDouble(rd_bas["salary"]);
            }

            //com = new SqlCommand("update paym_employee set basic_salary = '" + basic + "' where pn_EmployeeID='" + pay.EmployeeId + "' and pn_BranchID = '" + employee.BranchId + "' and pn_CompanyID='" + employee.CompanyId + "'", con);
            //com.ExecuteNonQuery();

            com = new SqlCommand("SELECT OT_calc FROM paym_employee where  pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' and pn_EmployeeID = '" + pay.EmployeeId + "' ", con);
            SqlDataReader rd_otc = com.ExecuteReader();
            if (rd_otc.Read())
            {
                ot_calc = Convert.ToDouble(rd_otc[0]);
            }
            ern_basic = (basic / total_wdays) * paid;
            

            if (ovt > 0)
            {
                OT_Settings = pay.fn_In_OT_Settings1(pay);

                if (OT_Settings.Count > 0)
                {
                    OT_Amt = ((basic / OT_Settings[0].OT_Days) / OT_Settings[0].OT_HRS) * ovt * ot_calc;
                }
            }

            pay.Act_Basic = basic;
            pay.Earned_Basic = ern_basic;
            com = new SqlCommand("Insert into payinput(pn_Companyid, pn_BranchID, pn_EmployeeID, d_Date, d_from_Date, d_To_Date, Calc_Days, Paid_Days, Present_Days, Absent_Days, TotLeave_Days, WeekOffDays, Holidays, OnDuty_days, Compoff_Days, Tour_Days, Att_Bonus, Att_BonusAmount, OT_HRS, Earn_Arrears, Ded_Arrears, ot_value, ot_Amt, Act_Basic, Earn_Basic, Mode, Flag) values ('" + employee.CompanyId + "','" + employee.BranchId + "','" + pay.EmployeeId + "','" + pay.strDate + "','" + fr_date + "','" + to_date + "','" + rc + "','" + paid + "','" + prs + "','" + abs + "','" + ldays + "','" + wkoff + "','" + hday + "','" + odd + "','" + cod + "','" + tod + "','" + att_bonus + "','" + att_amt + "','" + tot_ot + "','" + e_arr + "','" + d_arr + "','" + ovt + "','" + OT_Amt + "','" + basic + "','" + ern_basic + "','R','Y')", con);
            com.ExecuteNonQuery();
            con.Close();
        }

        catch (Exception ex)
        {
            //lbl_error.Text = "Already the Processing are done for this month!";
        }
    }

    public int round_up(double d_amt)
    {
        if (Convert.ToString(d_amt) != "NaN")
        {
            i_amt = Convert.ToInt32(d_amt);
            return i_amt;
        }
        else
        {
            return 0;
        }

    }

    public void Reg_Earnings()
    {

        RegEarningsList = pay.fn_In_Earnings_Slab(pay);

        if (RegEarningsList.Count > 0)
        {

            for (i_regearn = 0; i_regearn < RegEarningsList.Count; i_regearn++)
            {
                pay.Count = i_regearn;
                E_actamt = RegEarningsList[i_regearn].Amount;

                if (RegEarningsList[i_regearn].OT == 'Y')
                {
                    f_ot = f_ot + E_actamt;
                }


                //if (RegEarningsList[i_regearn].LOP == 'Y')
                //{
                //    E_amt = (E_actamt / A_calcdays) * A_paiddays;

                //}
                //else
                //{
                    E_amt = E_actamt;

                //}

                if (RegEarningsList[i_regearn].PT == 'Y')
                {
                    if (PT_Settings[0].PTtype == 'a')
                    {
                        f_pt = f_pt + E_actamt;
                    }
                    else
                    {
                        f_pt = f_pt + E_amt;
                    }

                }

                if (RegEarningsList[i_regearn].Pf == 'Y')
                {
                    f_PF = f_PF + E_amt;
                }

                if (RegEarningsList[i_regearn].Esi == 'Y')
                {
                    f_ESI = f_ESI + E_amt;
                }

                pay.EarningsId = RegEarningsList[i_regearn].EarningsId;
                pay.EarningsCode = RegEarningsList[i_regearn].EarningsCode;

                pay.d_date = employee.Convert_ToSqlDate(date_change(txt_todate.Text));
                pay.d_FromDate = employee.Convert_ToSqlDate(txt_todate.Text);
                pay.d_ToDate = employee.Convert_ToSqlDate(txt_todate.Text);
                if (ddl_Employee.SelectedItem.Text == "All")
                {
                    pay.pay_mode = "A";
                }
                else
                {
                    pay.pay_mode = "I";
                }
                pay.status = 'S';
                pay.Act_Amount = E_actamt;
                pay.Earn_Amount = round_up(E_amt);

                pay.PayOutput_Earnings(pay);
                pay.Earnings_update_register(pay);
                Net_Act_Earn = Net_Act_Earn + E_actamt;
                Net_Earn = Net_Earn + E_amt;

            }
        }

        else
        {
            RegEarningsList = pay.fn_In_Earnings(pay);

            if (RegEarningsList.Count > 0)
            {
                for (i_regearn = 0; i_regearn < RegEarningsList.Count; i_regearn++)
                {
                    pay.Count = i_regearn;
                    E_actamt = RegEarningsList[i_regearn].Amount;

                    if (RegEarningsList[i_regearn].OT == 'Y')
                    {
                        f_ot = f_ot + E_actamt;
                    }

                    E_amt = E_actamt;

                    if (RegEarningsList[i_regearn].PT == 'Y')
                    {
                        if (PT_Settings[0].PTtype == 'a')
                        {
                            f_pt = f_pt + E_actamt;
                        }
                        else
                        {
                            f_pt = f_pt + E_amt;
                        }
                    }

                    if (RegEarningsList[i_regearn].Pf == 'Y')
                    {
                        f_PF = f_PF + E_amt;
                    }

                    if (RegEarningsList[i_regearn].Esi == 'Y')
                    {
                        f_ESI = f_ESI + E_amt;
                    }

                    pay.EarningsId = RegEarningsList[i_regearn].EarningsId;
                    pay.EarningsCode = RegEarningsList[i_regearn].EarningsCode;

                    pay.d_date = employee.Convert_ToSqlDate(date_change(txt_todate.Text));
                    pay.d_FromDate = employee.Convert_ToSqlDate(txt_fromdate.Text);
                    pay.d_ToDate = employee.Convert_ToSqlDate(txt_todate.Text);
                    if (ddl_Employee.SelectedItem.Text == "All")
                    {
                        pay.pay_mode = "A";
                    }
                    else
                    {
                        pay.pay_mode = "I";
                    }
                    pay.status = 'S';
                    pay.Act_Amount = E_actamt;
                    pay.Earn_Amount = round_up(E_amt);

                    pay.PayOutput_Earnings_Mode(pay);
                    pay.Earnings_update_register(pay);
                    Net_Act_Earn = Net_Act_Earn + E_actamt;
                    Net_Earn = Net_Earn + E_amt;

                }
            }
        }
    }

    public string date_change(string str_date)
    {
        str_date = "01" + str_date.Substring(2, 8);
        return str_date;
    }

    public DateTime date_split(string s_date)
    {
        string _d, _m, _y;
        DateTime dt;
        if (s_date != "")
        {
            string[] da = s_date.Split('/');
            _d = da[0];
            _m = da[1];
            _y = da[2];

            s_date = _m + "/" + _d + "/" + _y;
            dt = Convert.ToDateTime(s_date);
        }
        else
        {
            dt = DateTime.Now;
        }
        return dt;
    }


    protected void Btn_calc_Click1(object sender, EventArgs e)
    {
        if (ddl_periodcode.SelectedItem.Text != "Select")
        {
            pay.d_FromDate = employee.Convert_ToSqlDate(txt_fromdate.Text);
            pay.d_ToDate = employee.Convert_ToSqlDate(txt_todate.Text);
            pay.d_date = employee.Convert_ToSqlDate(date_change(txt_fromdate.Text));
            pay.strDate = employee.Convert_ToSqlDatestring(date_change(txt_todate.Text));
            CheckingList = pay.fn_Out_PayOutput_Netpay(pay);
            pay.Empname = ddl_department.SelectedItem.Text;
            if (CheckingList.Count <= 0)
            {
                emp_Earn_List = pay.fn_Out_PayOutput_Earnings(pay);
                if (emp_Earn_List.Count <= 0)
                {
                    Initial_Processing();
                    if (emp_count != 0)
                    {
                        if (ddl_Employee.SelectedItem.Text == "All")
                        {
                            load1();
                        }
                        else
                        {
                            load();
                        }
                    }
                    else
                    {
                        lbl_Error.Text = "No Attendance Records Found";
                    }
                }
                else
                {

                    if (ddl_Employee.SelectedItem.Text != "All" && ddl_Employee.SelectedItem.Text != "Show All Employees")
                    {
                        pay.EmployeeId = Convert.ToInt32(ddl_Employee.SelectedValue);
                        emp_Earn_List = pay.fn_Out_PayOutput_Earnings_Mode(pay);
                        if (emp_Earn_List.Count <= 0)
                        {
                            Initial_Processing();
                            load();
                        }
                        else
                        {
                            load();
                        }
                    }
                    else
                    {
                        load1();
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Processing done for this month already');", true);
            }
        }
        else
        {
            // ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Select Period Code');", true);
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void ddl_Employee_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Employee.SelectedItem.Text == "Show All Employees")
        {
            Row_Emplist.Visible = true;
        }
        else
        {            
            Row_Emplist.Visible=false;
        }
    }
}
