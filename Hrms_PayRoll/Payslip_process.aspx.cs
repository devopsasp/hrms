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

public partial class PayRoll_Default : System.Web.UI.Page
{
    //ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand com = new SqlCommand();

    Company company = new Company();
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    Candidate c = new Candidate();

    Collection<Candidate> WorkHistoryList;
    //Collection<Employee> AllowanceList;
    Collection<Company> CompanyList;
    Collection<Employee> EmployeesList;
    Collection<Employee> EmpProfileList;
    Collection<PayRoll> AttendanceList;
    Collection<PayRoll> RegEarningsList;
    Collection<PayRoll> RegEarningsCheck;
    Collection<PayRoll> RegEarningsChange;
    Collection<PayRoll> RegDeductionList;
    Collection<PayRoll> RegDeductionsCheck;
    Collection<PayRoll> NonRegDeductionsCheck;
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

    int i_amt = 0, i_temp = 0,  i_emp, i_regearn, i_nonregearn, i_regded, i_nonregded, i_Precloser, i_loan, i_loan_ex, i_length, emp_count = 0;

    double d_temp = 0, A_calcdays = 0, NetPay = 0, A_paiddays = 0, A_Absentdays = 0, A_WeekOffdays = 0, E_amt = 0, E_amt_A = 0, E_actamt = 0, E_actamt_A = 0, N_E_amt = 0, N_E_actamt = 0, D_amt = 0, D_actamt = 0;
    double N_D_amt = 0, N_D_actamt = 0, f_ot = 0, f_pt = 0, f_PF = 0, f_ESI = 0, cal_amt = 0, PFAmt = 0, EPFAmt = 0;
    double Emp_ESIAmt = 0, Er_ESIAmt = 0,Emp_BasAmt = 0, Er_BasAmt = 0, OT_Amt = 0, PT_Amt = 0, IT_Amt = 0, Loan_amt = 0, Loan_Actamt = 0;
    double FPFAmt = 0, Balance_Amt = 0, Net_Earn = 0, Net_Ded = 0, Net_Act_Earn = 0, Net_Act_Earn_A = 0, Net_Act_Ded = 0, total_wdays = 0;

    string s_login_role;
    string str_temp = "", s_query = "", _path = "", str_Month = "", str_Year = "", Query = "", s_form;
    int cur_yr, yr_it;
    bool bool_Month = false;

    string Ot_hrs= "";
    int c_rd = 0;
    int txt_ceiling = 0;
    decimal txt_EPF = 0, txt_pf = 0;
    char chk_ceiling = ' ';
    char chk_allwance = ' ';
    double pf, epf, fpf, tot_pf, t_epf, t_pf, gross, bas_sal, upper_limit, pro_F; 
    double total_pf = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        //employee.CompanyId = 1;
        //employee.BranchId = 1;

        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        // employee.EmployeeId = 1;

        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        // pay.EmployeeId = 1;

        employee.ESIno = (string)ViewState["Emp_Esino"];

        
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        pay.Empname = "";
        pay.pay_mode = "";

        if (!IsPostBack)
        {
            
            CompanyList = company.fn_getCompany();
            if (CompanyList.Count > 0)
            {
                //ddl_year_load();
                switch (s_login_role)
                {
                    case "a":
                        ddl_branch_load(); 
                        break;

                    case "h":
                        ddl_Department_load();
                        ddl_branch.Visible = false;
                        load();
                        ddl_employee_load();
                        break;

                    case "u":
                        s_form = "50";

                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                           
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

    public void ddl_Employee_load()
    {
        chk_Empcode.Items.Clear();
        employee.DepartmentId = Convert.ToInt32(ddl_department.SelectedValue);
        EmployeesList = employee.fn_getEmployeeDepartment(employee);
        if (EmployeesList.Count > 0)
        {
            for (int ddl_i = -1; ddl_i < EmployeesList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    
                }
                else
                {
                    ListItem es_list = new ListItem();
                    es_list.Value = EmployeesList[ddl_i].EmployeeId.ToString();
                    es_list.Text = EmployeesList[ddl_i].LastName.ToString();
                    chk_Empcode.Items.Add(es_list);
                }
            }
        }
    }

    public void ddl_Department_load()
    {
        EmployeesList = employee.fn_getDepartmentList1(employee.BranchId);
        if (EmployeesList.Count > 0)
        {

            for (int ddl_i = -1; ddl_i < EmployeesList.Count; ddl_i++)
            {

                if (ddl_i == -1)
                {
                    ListItem es_list = new ListItem();

                    es_list.Text = "Select Department";
                    es_list.Value = "0";
                    ddl_department.Items.Add(es_list);
                }
                else
                {
                    ListItem es_list = new ListItem();

                    es_list.Value = EmployeesList[ddl_i].DepartmentId.ToString();
                    es_list.Text = EmployeesList[ddl_i].DepartmentName.ToString();
                    ddl_department.Items.Add(es_list);
                }
            }
        }
    }

    public void ddl_year_load(DropDownList ddl)
    {
        try
        {
            cur_yr = DateTime.Now.Year;

            cur_yr = cur_yr + 5;

            for (yr_it = 1990; yr_it <= cur_yr; yr_it++)
            {
                ddl.Items.Add(Convert.ToString(yr_it));
            }

        }

        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

    public void ddl_branch_load()
    {
        try
        {
            con.Open();

            SqlDataAdapter ad = new SqlDataAdapter("select * from paym_branch", con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "paym_branch");
            ddl_branch.DataSource = ds;
            ddl_branch.DataTextField = "branchname";
            ddl_branch.DataValueField = "pn_branchid";
            ddl_branch.DataBind();

            ddl_branch.Items.Insert(0, "Select Branch");
        }
        catch (Exception ex)
        {

        }

        finally
        {
            con.Close();
        }
    }

    public void ddl_employee_load()
    {
        try
        {
            if (s_login_role == "a")
            {
                employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
                pay.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
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
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Employees Available');", true);
            }
        }
        catch (Exception ex)
        {

        }
    }

    public void load()
    {
        try
        {
            con.Open();
            Emp.Visible = false;
            ddl_year_load(ddl_year);
            ddl_month.SelectedValue = DateTime.Now.Month.ToString();
            ddl_year.SelectedValue = DateTime.Now.Year.ToString();
            if (s_login_role == "a")
            {
                pay.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
                employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
            }
            
            com = new SqlCommand("Select top 10 * from salary_period where pn_branchid = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "' order by from_date desc", con);
            SqlDataReader re = com.ExecuteReader();
            while (re.Read())
            {
                ddl_periodcode.Items.Add(re["period_code"].ToString());
            }
            re.Close();
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }

        finally
        {
            con.Close();
        }
    }
    
    public void clear()
    {
        Txt_fdate.Text = "";
        Txt_tdate.Text = "";
        txt_paydate.Text = "";
        Txt_totdays.Text = "";
        ddl_month.SelectedValue = DateTime.Now.Month.ToString();
        ddl_year.SelectedValue = DateTime.Now.Year.ToString();
        ddl_periodcode.SelectedIndex = 0;
        Btn_process.Enabled = true;
        Btn_undo.Enabled = true;
    }

    protected void Btn_process_Click(object sender, EventArgs e)
    {
        int ccount = 0;
        pay.d_FromDate = employee.Convert_ToSqlDate(Txt_fdate.Text);
        pay.d_ToDate = employee.Convert_ToSqlDate(Txt_tdate.Text);
        pay.d_date = employee.Convert_ToSqlDate(date_change(Txt_tdate.Text));
        pay.strDate = employee.Convert_ToSqlDatestring(date_change(Txt_tdate.Text));
        string fdate = Txt_fdate.Text;
        string tdate = Txt_tdate.Text;
        string pdate = txt_paydate.Text;
        string[] fd = fdate.Split('/');
        string[] td = tdate.Split('/');
        string[] pd = pdate.Split('/');
        string fr_date = fd[1] + "/" + fd[0] + "/" + fd[2];
        string to_date = td[1] + "/" + td[0] + "/" + td[2];
        string p_date = pd[1] + "/" + pd[0] + "/" + pd[2];
        pay.Str_From_date = fr_date;
        pay.Str_To_date = to_date;
        if (ddl_department.SelectedItem.Text == "Select Department")
        {
            CheckingList = pay.fn_Out_PayOutput_Netpay(pay);
            ccount = CheckingList.Count;
        }
        else
        {
            for (int i = 0; i < chk_Empcode.Items.Count; i++)
            {
                if (chk_Empcode.Items[i].Selected == true)
                {
                    pay.EmployeeId = Convert.ToInt32(chk_Empcode.Items[i].Value);
                    CheckingList = pay.fn_Out_PayOutput_Netpay_Employee(pay);
                    if (CheckingList.Count > 0)
                    {
                        ccount += 1;
                    }
                }
            }
        }
        if (ccount <= 0)
        {
            try
            {
                con.Open();
                Initial_Processing();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(Error Occured);", true);
            }
            finally
            {
                con.Close();
            }
            if (emp_count != 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Process Done Successfully!');", true);
                clear();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Attendance Found!');", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Process Done for this month already!');", true);
        }
    }


    public void Initial_Processing()
    {
        PT_Settings = pay.fn_In_PTax_Settings(pay);
        if (PT_Settings.Count > 0)
        {
            for (i_emp = 0; i_emp < chk_Empcode.Items.Count; i_emp++)
            {
                N_D_amt = 0; N_D_actamt = 0; f_ot = 0; f_pt = 0; f_PF = 0; f_ESI = 0; cal_amt = 0; PFAmt = 0; EPFAmt = 0;
                Emp_ESIAmt = 0; Er_ESIAmt = 0; Emp_BasAmt = 0; Er_BasAmt = 0; OT_Amt = 0; PT_Amt = 0; IT_Amt = 0; Loan_amt = 0; Loan_Actamt = 0;
                FPFAmt = 0; Balance_Amt = 0; Net_Earn = 0; Net_Ded = 0; Net_Act_Earn = 0; Net_Act_Earn_A = 0; Net_Act_Ded = 0; E_actamt_A = 0;
                if (chk_Empcode.Items[i_emp].Selected == true)
                {
                    pay.periodCode = ddl_periodcode.SelectedItem.Text;
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

                        A_paiddays = AttendanceList[0].Paid_Days + AttendanceList[0].Earn_Arrears;
                        A_paiddays = A_paiddays - AttendanceList[0].Ded_Arrears;

                        Processing();
                        emp_count++;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No attendance found for employees');", true);
                    }
                }
            }
            pay.PayProcess(pay);
        }

        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter Professional Tax Settings');", true);
        }
    }

    public void Processing()
    {
        pay.Earn_Deduct_Insert(pay);
        Reg_Earnings_Actuals();
        Reg_Earnings();
        Non_Reg_Earnings();
        Reg_Deductions();
        Non_Reg_Deductions();
        //OT_Calculation();
        Net_Calculation();
    }

    public void Month_Attendance()
    {
        try
        {

            string stats = "", att_bonus = "", sothr = "";
            TimeSpan tot_ot = new TimeSpan(0, 0, 0);
            double abs = 0, prs = 0, hday = 0, ldays = 0, rc = 0, wkoff = 0, paid = 0, odd = 0, cod = 0, tod = 0, att_amt = 0, basic = 0, ern_basic = 0, ot_calc = 0, ot_amt = 0, e_arr = 0, d_arr = 0;
            //DateTime othr;
            pay.strDate = employee.Convert_ToSqlDatestring(date_change(Txt_tdate.Text));
            pay.d_date = employee.Convert_ToSqlDate(date_change(Txt_tdate.Text));
            string fdate = Txt_fdate.Text;
            string tdate = Txt_tdate.Text;
            string pdate = txt_paydate.Text;
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
                    if (stats == "XX" )
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
               sothr = Math.Truncate(tot_ot.TotalHours).ToString("00") + ":" + tot_ot.Minutes.ToString("00");
            }
            else
            {
                return;
            }
            //  OnDuty_days, Compoff_Days, Tour_Days, Att_Bonus, Att_BonusAmount, 
            rd_calc.Close();
            total_wdays = Convert.ToDouble(Txt_totdays.Text);

            com = new SqlCommand("SELECT * FROM attendance_ceiling where pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' ", con);
            SqlDataReader rd_ce = com.ExecuteReader();
            if (rd_ce.Read())
            {
                pay.Month_Type = Convert.ToString(rd_ce["month_type"]);
                pay.Manual_Days = Convert.ToInt32(rd_ce["manual_days"]);
                
            }
            if (pay.Month_Type == "Week-off ex")
            {
                A_paiddays = prs + ldays + hday;
                if (prs == 0.0)
                {
                    A_paiddays = 0.0;
                }
            }
            else
            {
                A_paiddays = prs + ldays + wkoff + hday;
                if (prs == 0.0)
                {
                    A_paiddays = 0.0;
                }
            }
            double ovt = 0, otround = 0;
            string str_ot = Convert.ToString(tot_ot);
            string[] ot_spl = sothr.Split(':');
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
            pay.OT_HRS = Convert.ToDouble(ot_spl[0]) + otround;

            payinput = pay.fn_Get_Basic(pay);
            if (payinput.Count > 0)
            {
                pay.Act_Basic = payinput[0].Act_Basic;
                if (pay.Month_Type == "Week-off ex")
                {
                    pay.Earned_Basic = (pay.Act_Basic / total_wdays) * A_paiddays;
                }
                else if (pay.Month_Type == "Month Days")
                {
                    pay.Earned_Basic = (pay.Act_Basic / A_calcdays) * A_paiddays;
                }
                else
                {
                    pay.Earned_Basic = (pay.Act_Basic / pay.Manual_Days) * A_paiddays;
                }
            }

            com = new SqlCommand("SELECT OT_calc FROM paym_employee where  pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' and pn_EmployeeID = '" + pay.EmployeeId + "' ", con);
            SqlDataReader rd_otc = com.ExecuteReader();
            if (rd_otc.Read())
            {
                ot_calc = Convert.ToDouble(rd_otc[0]);
            }


            if (pay.OT_HRS > 0)
            {
                OT_Settings = pay.fn_In_OT_Settings1(pay);

                if (OT_Settings.Count > 0)
                {
                    pay.OT_Amt = ((pay.Act_Basic / OT_Settings[0].OT_Days) / OT_Settings[0].OT_HRS) * pay.OT_HRS * ot_calc;
                }
            }

            com = new SqlCommand("Insert into payinput(pn_Companyid, pn_BranchID, pn_EmployeeID, d_Date, d_from_Date, d_To_Date, Calc_Days, Paid_Days, Present_Days, Absent_Days, TotLeave_Days, WeekOffDays, Holidays, OnDuty_days, Compoff_Days, Tour_Days, Att_Bonus, Att_BonusAmount, OT_HRS, Earn_Arrears, Ded_Arrears, ot_value, ot_Amt, Act_Basic, Earn_Basic, Mode, Flag) values ('" + employee.CompanyId + "','" + employee.BranchId + "','" + pay.EmployeeId + "','" + pay.strDate + "','" + fr_date + "','" + to_date + "','" + rc + "','" + A_paiddays + "','" + prs + "','" + abs + "','" + ldays + "','" + wkoff + "','" + hday + "','" + odd + "','" + cod + "','" + tod + "','" + att_bonus + "','" + att_amt + "','00:00','" + e_arr + "','" + d_arr + "','" + pay.OT_HRS + "','" + pay.OT_Amt + "','" + pay.Act_Basic + "','" + pay.Earned_Basic + "','R','Y')", con);
            com.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            //lbl_error.Text = "Already the Processing are done for this month!";
        }
        finally
        {

        }
    }

    //********************Earnings*******************************************************************

    public void Reg_Earnings_Actuals()
    {

        RegEarningsList = pay.fn_In_Earnings(pay);

        if (RegEarningsList.Count > 0)
        {

            for (i_regearn = 0; i_regearn < RegEarningsList.Count; i_regearn++)
            {
                pay.Count = i_regearn;
                E_actamt_A = RegEarningsList[i_regearn].Amount;

                pay.d_date = employee.Convert_ToSqlDate(date_change(Txt_tdate.Text));
                pay.d_FromDate = employee.Convert_ToSqlDate(Txt_fdate.Text);
                pay.d_ToDate = employee.Convert_ToSqlDate(Txt_tdate.Text);

                pay.status = 'E';
                pay.Act_Amount = E_actamt_A;

                Net_Act_Earn_A = Net_Act_Earn_A + E_actamt_A;
            }
        }

    }


    public void Reg_Earnings()
    {
        RegEarningsCheck = pay.fn_Check_Earnings(pay);

        if (RegEarningsCheck.Count <= 0)
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

                    if (RegEarningsList[i_regearn].LOP == 'Y')
                    {
                        E_amt = (E_actamt / A_calcdays) * A_paiddays;
                    }

                    else
                    {
                        E_amt = E_actamt;
                    }

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

                    pay.d_date = employee.Convert_ToSqlDate(date_change(Txt_tdate.Text));
                    pay.d_FromDate = employee.Convert_ToSqlDate(Txt_fdate.Text);
                    pay.d_ToDate = employee.Convert_ToSqlDate(Txt_tdate.Text);

                    pay.status = 'E';
                    pay.Act_Amount = E_actamt;
                    //pay.Earn_Amount = round_up(E_amt);
                    pay.Earn_Amount = E_amt;

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
                        pay.EarningsId = RegEarningsList[i_regearn].EarningsId;
                        pay.Count = i_regearn;
                        E_actamt = RegEarningsList[i_regearn].Amount;

                        RegEarningsCheck = pay.fn_Check_Earnings_ID(pay);
                        if (RegEarningsCheck.Count > 0)
                        {
                            continue;
                            //E_actamt = RegEarningsCheck[0].Amount;
                        }

                        if (RegEarningsList[i_regearn].OT == 'Y')
                        {
                            f_ot = f_ot + E_actamt;
                        }


                        if (RegEarningsList[i_regearn].LOP == 'Y')
                        {
                            E_amt = (E_actamt / A_calcdays) * A_paiddays;

                        }
                        else
                        {
                            E_amt = E_actamt;

                        }

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

                        pay.d_date = employee.Convert_ToSqlDate(date_change(Txt_tdate.Text));
                        pay.d_FromDate = employee.Convert_ToSqlDate(Txt_fdate.Text);
                        pay.d_ToDate = employee.Convert_ToSqlDate(Txt_tdate.Text);

                        pay.status = 'E';
                        pay.Act_Amount = E_actamt;
                        //pay.Earn_Amount = round_up(E_amt);
                        pay.Earn_Amount = E_amt;

                        pay.PayOutput_Earnings(pay);
                        pay.Earnings_update_register(pay);
                        Net_Act_Earn = Net_Act_Earn + E_actamt;
                        Net_Earn = Net_Earn + E_amt;

                    }
                }
            }
            
        }
        else
        {
            RegEarningsCheck = pay.fn_Check_Earnings_Change(pay);
            E_amt = 0; E_actamt = 0;
            for (i_regearn = 0; i_regearn < RegEarningsCheck.Count; i_regearn++)
            {
                pay.EarningsId = RegEarningsCheck[i_regearn].EarningsId;
                E_actamt_A = RegEarningsCheck[i_regearn].Amount;
                pay.Count = i_regearn;
                E_amt = RegEarningsCheck[i_regearn].Amount;

                RegEarningsChange = pay.fn_Check_Earnings_ID(pay);
                if (RegEarningsChange.Count > 0)
                {
                    continue;
                }

                if (RegEarningsCheck[i_regearn].OT == 'Y')
                {
                    f_ot = f_ot + E_amt;
                }

                if (RegEarningsCheck[i_regearn].LOP == 'Y')
                {
                    E_amt = (E_amt / A_calcdays) * A_paiddays;
                }

                if (RegEarningsCheck[i_regearn].PT == 'Y')
                {
                    if (PT_Settings[0].PTtype == 'a')
                    {
                        f_pt = f_pt + E_amt;
                    }
                    else
                    {
                        f_pt = f_pt + E_amt;
                    }
                }

                if (RegEarningsCheck[i_regearn].Pf == 'Y')
                {
                    f_PF = f_PF + E_amt;
                }

                if (RegEarningsCheck[i_regearn].Esi == 'Y')
                {
                    f_ESI = f_ESI + E_amt;
                }

                E_actamt += E_amt;
                pay.EarningsId = RegEarningsCheck[i_regearn].EarningsId;

                pay.d_date = employee.Convert_ToSqlDate(date_change(Txt_tdate.Text));
                pay.d_FromDate = employee.Convert_ToSqlDate(Txt_fdate.Text);
                pay.d_ToDate = employee.Convert_ToSqlDate(Txt_tdate.Text);

                pay.status = 'E';
                pay.Act_Amount = E_actamt_A;
                //pay.Earn_Amount = round_up(E_amt);
                pay.Earn_Amount = E_amt;

                pay.PayOutput_Earnings(pay);
            }
            pay.Earn_Amount = round_up(E_actamt);
            Net_Earn = function_rd_next(E_actamt);
        }

    }



    public void Non_Reg_Earnings()
    {
        N_E_amt = 0;
        NonRegEarningsList = pay.fn_Check_Non_Earnings(pay);

        if (NonRegEarningsList.Count > 0)
        {

            for (i_nonregearn = 0; i_nonregearn < NonRegEarningsList.Count; i_nonregearn++)
            {
                N_E_actamt = NonRegEarningsList[i_nonregearn].Amount;

                if (NonRegEarningsList[i_nonregearn].OT == 'Y')
                {
                    f_ot = f_ot + N_E_actamt;
                }

                if (NonRegEarningsList[i_nonregearn].LOP == 'Y')
                {
                    N_E_amt = (N_E_actamt / A_calcdays) * A_paiddays;

                }
                else
                {
                    N_E_amt = N_E_actamt;

                }
                //N_E_amt = N_E_actamt;

                if (NonRegEarningsList[i_nonregearn].PT == 'Y')
                {
                    f_pt = f_pt + N_E_amt;
                }

                if (NonRegEarningsList[i_nonregearn].Pf == 'Y')
                {
                    f_PF = f_PF + N_E_amt;
                }

                if (NonRegEarningsList[i_nonregearn].Esi == 'Y')
                {
                    f_ESI = f_ESI + N_E_amt;
                }
                pay.EarningsId = NonRegEarningsList[i_nonregearn].EarningsId;

                pay.d_date = employee.Convert_ToSqlDate(date_change(Txt_tdate.Text));
                pay.d_FromDate = employee.Convert_ToSqlDate(Txt_fdate.Text);
                pay.d_ToDate = employee.Convert_ToSqlDate(Txt_tdate.Text);

                pay.status = 'E';
                pay.Act_Amount = N_E_actamt;
                //pay.Earn_Amount = round_up(N_E_amt);
                pay.Earn_Amount = N_E_amt;

                pay.PayOutput_Earnings(pay);

                Net_Act_Earn = Net_Act_Earn + N_E_actamt;
                Net_Earn = Net_Earn + N_E_amt;

            }
        }
    }


    //********************Deductions****************************************************************

    public void Reg_Deductions()
    {
        RegDeductionsCheck = pay.fn_Check_Deductions(pay);

        if (RegDeductionsCheck.Count <= 0)
        {

            RegDeductionList = pay.fn_In_Deduction(pay);

            if (RegDeductionList.Count > 0)
            {

                for (i_regded = 0; i_regded < RegDeductionList.Count; i_regded++)
                {

                    switch (RegDeductionList[i_regded].DeducationCode)
                    {

                        case "PF": PF_Calculation();
                            break;

                        case "VPF": VPF_Calculation();
                            break;

                        case "ESI": ESI_Calculation();
                            break;

                        case "PT": PT_Calculation();
                            break;

                        case "IT": //IT_Calculation();
                            break;

                        case "LWF": //LWF_Settings = pay.fn_In_LWF_Settings(pay);
                        //if (LWF_Settings.Count > 0)
                        //{
                        //    LWF_Calculation();
                        //}
                        break;


                        default: Normal_Calculation();
                            break;
                    }
                }
            }
        }
        else
        {
            //Net_Ded = pay.Payout_Deduction_Sum(pay);
        }
    }

    public void Non_Reg_Deductions()
    {

        NonRegDeductionsCheck = pay.fn_Check_Non_Deduction(pay);

        if (NonRegDeductionsCheck.Count <= 0)
        {
            NonRegDeductionList = pay.fn_In_Non_Deduction(pay);

            if (NonRegDeductionList.Count > 0)
             {

                 for (i_nonregded = 0; i_nonregded < NonRegDeductionList.Count; i_nonregded++)
                 {
                     N_D_actamt = NonRegDeductionList[i_nonregded].Amount;

                     N_D_amt = N_D_actamt;
                     pay.DeducationCode = NonRegDeductionList[i_nonregded].DeducationCode;
                     pay.DeductionId = NonRegDeductionList[i_nonregded].DeductionId;
                     pay.Act_Amount = N_D_actamt;
                     pay.Ded_Amount = N_D_amt;
                     if (A_paiddays == 0.0)
                     {
                         pay.Ded_Amount = 0.0;
                     }
                     pay.status = 'D';
                     pay.PayOutput_Deductions(pay);
                     pay.Deduction_update_register(pay);
                     Net_Act_Ded = Net_Act_Ded + N_D_actamt;
                     Net_Ded = Net_Ded + N_D_amt;

                 }
             }
        }

        //NonRegDeductionList = pay.fn_In_Non_Deduction(pay);

        //if (NonRegDeductionList.Count > 0)
        //{

        //    for (i_nonregded = 0; i_nonregded < NonRegDeductionList.Count; i_nonregded++)
        //    {

        //        N_D_actamt = NonRegDeductionList[i_nonregded].Amount;

        //        N_D_amt = N_D_actamt;

        //        //s_query = "insert into PayOutput_Deductions values(" + pay.CompanyId + "," + pay.EmployeeId + "," + NonRegDeductionList[i_nonregded].DeductionId + ",'01/01/2008','01/01/2008','D'," + N_D_actamt + "," + N_D_amt + ")";

        //        pay.DeductionId = NonRegDeductionList[i_nonregded].DeductionId;
        //        pay.Act_Amount = N_D_actamt;
        //        pay.Ded_Amount = N_D_amt;
        //        pay.status = 'D';

        //        pay.PayOutput_Deductions(pay);


        //        Net_Act_Ded = Net_Act_Ded + N_D_actamt;
        //        Net_Ded = Net_Ded + N_D_amt;
        //    }
        //}
    }

    public void Loan_Calculation()
    {

        //PayOutput_Loan Table checking

        PayOutput_LoanList = pay.fn_In_PayOutput_Loan(pay);

        if (PayOutput_LoanList.Count <= 0)
        {

            //LoanEntry Table checking

            LoanEntryList = pay.fn_In_LoanEntry(pay);

            if (LoanEntryList.Count > 0)
            {

                for (i_loan = 0; i_loan < LoanEntryList.Count; i_loan++)
                {
                    pay.loanid = LoanEntryList[i_loan].loanid;

                    Loan_Actamt = LoanEntryList[i_loan].Amount;
                    Loan_amt = LoanEntryList[i_loan].F_Amount;

                    //Loan_Actamt = LoanEntryList[i_loan].F_Amount;
                    //Loan_amt = LoanEntryList[i_loan].T_Amount;
                    Balance_Amt = LoanEntryList[i_loan].balanceamount;

                    //LoanCancel Table checking

                    LoanCancelList = pay.fn_In_LoanCancel(pay);

                    if (LoanCancelList.Count >= 0)
                    {

                        //LoanPreCloser Table checking

                        LoanPreCloserList = pay.fn_In_LoanPreCloser(pay);

                        if (LoanPreCloserList.Count > 0)
                        {
                            for (i_Precloser = 0; i_Precloser < LoanPreCloserList.Count; i_Precloser++)
                            {

                                Balance_Amt = Balance_Amt - LoanPreCloserList[i_Precloser].balanceamount;

                                Loan_Sub_Calulation();

                                s_query = "update Loan_PreCloser set c_status='N' where pn_CompanyID=" + pay.CompanyId + " and pn_EmployeeID=" + pay.EmployeeId + " and fn_LoanID=" + pay.loanid + " and d_date='" + LoanPreCloserList[i_Precloser].d_EffDate + "'";

                                pay.fn_In_Out(s_query);

                            }

                        }
                        else
                        {
                            Loan_Sub_Calulation();

                        }

                    }

                }

            }

        }
        else
        {

            for (i_loan_ex = 0; i_loan_ex < PayOutput_LoanList.Count; i_loan_ex++)
            {

                Net_Act_Ded = Net_Act_Ded + PayOutput_LoanList[i_loan_ex].F_Amount;
                Net_Ded = Net_Ded + PayOutput_LoanList[i_loan_ex].T_Amount;

            }
        }
    }


    //*************************************************************************************************

    public void PF_Calculation()
    {

        if (RegDeductionList[i_regded].eligible == 'Y')
        {
            PFList = pay.fn_In_PF(pay);

            if (PFList.Count > 0)
            {

                //if (f_PF <= PFList[0].Amount)
                //{
                    cal_amt = f_PF;
                //}
                //else
                //{
                //    cal_amt = PFList[0].Amount;
                //}

                PFAmt = cal_amt * (PFList[0].Emp_Con_PF / 100);

                EPFAmt = cal_amt * (PFList[0].Emp_Con_EPF / 100);

                FPFAmt = PFAmt - EPFAmt;

                EmployeesList = employee.fn_get_Emp_first(employee);
                if (EmployeesList[0].PFno == "")
                {
                    pay.PF_No = "0";
                }
                else
                {
                    pay.PF_No = EmployeesList[0].PFno;
                }
                //pay.PF_No = "007";
                PF_Calc();
                //pay.Net_Amount = round_up(f_PF);
                //pay.Emp_Con_PF = PFAmt;
                //pay.Emp_Con_EPF = EPFAmt;
                //pay.Emp_Con_FPF = FPFAmt;
                //pay.Emp_Con_VPF = 0;
                //pay.Paid_Days = A_paiddays;
                //pay.Absent_Days = A_Absentdays;
                //pay.WeekOffDays = A_WeekOffdays;
               
                //pay.PayOutput_PF(pay);

                //s_query = "insert into PayOutput_Deductions values(" + pay.CompanyId + "," + pay.EmployeeId + "," + RegDeductionList[i_regded].DeductionId + ",'01/01/2008','01/01/2008','D'," + f_PF + "," + PFAmt + ")";

                pay.DeductionId = RegDeductionList[i_regded].DeductionId;
                pay.DeducationCode = RegDeductionList[i_regded].DeducationCode;
                pay.Act_Amount = f_PF;
                pay.Ded_Amount = epf + fpf;
                pay.status = 'D';

                pay.PayOutput_Deductions(pay);
                pay.Deduction_update_register(pay);
                Net_Act_Ded = Net_Act_Ded + f_PF;
                Net_Ded = Net_Ded + tot_pf;

            }
        }
    }

    public void VPF_Calculation()
    {
        if (RegDeductionList[i_regded].eligible == 'Y')
        {
            
            double volpf = VPF();
            
            pay.DeductionId = RegDeductionList[i_regded].DeductionId;
            pay.DeducationCode = RegDeductionList[i_regded].DeducationCode;
            if (Net_Earn > volpf)
            {
                pay.Act_Amount = volpf;
                pay.Ded_Amount = volpf;
            }
            else
            {
                pay.Act_Amount = 0.0;
                pay.Ded_Amount = 0.0;
            }
            pay.status = 'D';
            Net_Ded = Net_Ded + pay.Ded_Amount;
            pay.PayOutput_Deductions(pay);
            pay.Deduction_update_register(pay);
        }
    }

    public void PF_Calc()
    {
        try
        {
            SqlDataReader rd1, rd2;
            SqlCommand cmd_pf = new SqlCommand();
            cmd_pf = new SqlCommand("select * from paym_pf where pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + employee.BranchId + "'", con);
            rd2 = cmd_pf.ExecuteReader();
            if (rd2.Read())
            {
                txt_pf = Convert.ToDecimal(rd2["Emp_Con_PF"]);
                txt_EPF = Convert.ToDecimal(rd2["Emp_Con_EPF"]);
                txt_ceiling = Convert.ToInt32(rd2["max_amount"]);
                chk_ceiling = Convert.ToChar(rd2["check_ceiling"]);
                chk_allwance = Convert.ToChar(rd2["check_allowance"]);
                c_rd = Convert.ToInt32(rd2["c_Round"]);
            }
            
            if (payinput.Count > 0)
            {

                bas_sal = pay.Earned_Basic;
                gross = bas_sal + Net_Earn;
                upper_limit = Convert.ToInt32(txt_ceiling);
                t_pf = Convert.ToDouble(txt_pf);
                t_epf = Convert.ToDouble(txt_EPF);
                pay.Gross_Salary = gross;

                fpf = 0;
                if (upper_limit > gross)
                {
                    if (chk_allwance == 'Y')
                    {
                        bas_sal = gross;
                        if (gross < upper_limit)
                        {
                            PF_Sub_Calculation1();
                        }
                        else
                        {
                            PF_Sub_Calculation2();
                        }
                    }
                    else
                    {
                        PF_Sub_Calculation1();
                    }

                    //cmd_ins.ExecuteNonQuery();
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('PF Generated');", true);

                }

                else
                {
                    PF_Sub_Calculation2();
                }

            }
        }
        catch
        {

        }
        finally
        {

        }
    }

    public void PF_Sub_Calculation1()
    {
        pf = round_up((((bas_sal) * (t_pf)) / 100));
        pf = round_up((((bas_sal) * (t_pf)) / 100));
        pf = pf + PFAmt;
        epf = (((bas_sal) * (t_epf)) / 100);
        epf = round_up(epf + EPFAmt);
        fpf = pf - epf;
        //fpf = fpf + FPFAmt;
        //SqlCommand cmd_ins = new SqlCommand("insert into pf_employee(pn_companyid,pn_branchid,emp_code,emp_name,month,year,period,Epf,Fpf) values('" + employee.CompanyId + "','" + employee.BranchId + "','" + emp_cod + "','" + emp_name + "','" + ddl_month.SelectedItem.Text + "','" + ddl_year.SelectedItem.Text + "','" + ddl_periodcode.SelectedItem.Text + "','" + epf + "','" + fpf + "') ", con);
        //cmd_ins.ExecuteNonQuery();

        double vpf = 0;
        vpf = VPF();
        tot_pf = epf + fpf;
        
        if (c_rd == 0)
        {
            total_pf = function_rd_nearest(tot_pf);
        }
        else
        {
            total_pf = function_rd_next(tot_pf);
        }

        pay.Net_Amount = round_up(f_PF);
        pay.Emp_Con_PF = total_pf;
        pay.Pf_Amount = epf + fpf;
        pay.Emp_Con_EPF = epf;
        pay.Emp_Con_FPF = fpf;
        pay.Emp_Con_VPF = vpf;
        pay.Paid_Days = A_paiddays;
        pay.Absent_Days = A_Absentdays;
        pay.WeekOffDays = A_WeekOffdays;
        pay.periodCode = ddl_periodcode.SelectedItem.Text;

        //SqlCommand cmd_ins = new SqlCommand();


        pay.PayOutput_PF(pay);

    }

    public void PF_Sub_Calculation2()
    {
        if (chk_ceiling == 'N')
        {

            pf = round_up((((bas_sal) * (t_pf)) / 100));
            pf = pf + PFAmt;
            epf = round_up((((upper_limit) * (t_epf)) / 100));
            //epf = epf + EPFAmt;
            fpf = pf - epf;
            //fpf = fpf + FPFAmt;

            double vpf = 0;
            vpf = VPF();
            tot_pf = epf + fpf;

            if (c_rd == 0)
            {
                total_pf = function_rd_nearest(tot_pf);
            }
            else
            {
                total_pf = function_rd_next(tot_pf);
            }

            SqlCommand cmd_ins = new SqlCommand();

            pay.Net_Amount = round_up(f_PF);
            pay.Emp_Con_PF = total_pf;
            pay.Pf_Amount = epf + fpf;
            pay.Emp_Con_EPF = epf;
            pay.Emp_Con_FPF = fpf;
            pay.Emp_Con_VPF = vpf;
            pay.Paid_Days = A_paiddays;
            pay.Absent_Days = A_Absentdays;
            pay.WeekOffDays = A_WeekOffdays;
            pay.periodCode = ddl_periodcode.SelectedItem.Text;

            pay.PayOutput_PF(pay);

            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('PF Generated');", true);

        }
        else if (chk_ceiling == 'Y')
        {
            pf = round_up((((upper_limit) * (t_pf)) / 100));
            epf = round_up((((upper_limit) * (t_epf)) / 100));
            fpf = pf - epf;

            double vpf = 0;
            vpf = VPF();
            tot_pf = epf + fpf;

            if (c_rd == 0)
            {
                total_pf = function_rd_nearest(tot_pf);
            }
            else
            {
                total_pf = function_rd_next(tot_pf);
            }

            pay.Net_Amount = round_up(f_PF);
            pay.Emp_Con_PF = total_pf;
            pay.Pf_Amount = epf + fpf;
            pay.Emp_Con_EPF = epf;
            pay.Emp_Con_FPF = fpf;
            pay.Emp_Con_VPF = vpf;
            pay.Paid_Days = A_paiddays;
            pay.Absent_Days = A_Absentdays;
            pay.WeekOffDays = A_WeekOffdays;
            pay.periodCode = ddl_periodcode.SelectedItem.Text;

            pay.PayOutput_PF(pay);

            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('PF Generated');", true);

        }
    }

    public double VPF()
    {
        string contribution_type = "";
        double monthly_contribution = 0;
        string type = "";
        SqlDataReader rd_se;
        SqlCommand cmd_se = new SqlCommand();
        cmd_se = new SqlCommand("select monthlycontribution,contribution_type,salaryfrom from paym_vpf where pn_EmployeeID='" + employee.EmployeeId + "' and pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "' ", con);
        rd_se = cmd_se.ExecuteReader();
        if (rd_se.Read())
        {
            monthly_contribution = Convert.ToDouble(rd_se[0]);
            contribution_type = rd_se[1].ToString();
            type = rd_se[2].ToString();
        }

        SqlDataReader rd_esal;
        SqlCommand cmd_esal = new SqlCommand();
        double earn_bas = 0;
        double vpf_sub = 0;

        if (type == "Basic Salary")
        {
            earn_bas = bas_sal;
        }
        else
        {
            earn_bas = gross;
        }

        if (contribution_type == "Percentage")
        {
            vpf_sub = (earn_bas * monthly_contribution) / 100;
        }

        else
        {
            vpf_sub = monthly_contribution;
        }

        return vpf_sub;

    }


    public void ESI_Calculation()
    {
        if (RegDeductionList[i_regded].eligible == 'Y')
        {
            double bs = 0.0;
            payinput = pay.fn_Get_Basic(pay);
            total_wdays = Convert.ToDouble(Txt_totdays.Text);

            if (payinput.Count > 0)
            {
                //pay.Act_Basic = payinput[0].Act_Basic;
                //pay.Earned_Basic = (pay.Act_Basic / total_wdays) * A_paiddays;
                //bs = (pay.Act_Basic / total_wdays) * A_paiddays;
                bs = pay.Earned_Basic;
            }

            if (RegDeductionList[i_regded].eligible == 'Y')
            {
                ESIList = pay.fn_In_ESI(pay);

                if (ESIList.Count > 0)
                {
                    //f_ESI = f_ESI + OT_Amt;
                    if (bs <= ESIList[0].Amount)
                    {
                        Emp_ESIAmt = f_ESI * (ESIList[0].Emp_Con / 100);
                        Er_ESIAmt = f_ESI * (ESIList[0].Employer_Con / 100);
                        Emp_BasAmt = bs * (ESIList[0].Emp_Con / 100);
                        Er_BasAmt = bs * (ESIList[0].Employer_Con / 100);
                        Emp_ESIAmt = Emp_ESIAmt + Emp_BasAmt;
                        Er_ESIAmt = Er_ESIAmt + Er_BasAmt;

                        EmployeesList = employee.fn_get_Emp_first(employee);
                        if (EmployeesList[0].ESIno == "")
                        {
                            pay.ESI_No = "0";
                        }
                        else
                        {
                            pay.ESI_No = EmployeesList[0].ESIno;
                        }

                        //pay.ESI_No = "007";
                        pay.Net_Amount = round_up(pay.Gross_Salary);
                        pay.Emp_Con = round_up(Emp_ESIAmt);
                        pay.Employer_Con = round_up(Er_ESIAmt);
                        pay.Paid_Days = A_paiddays;
                        pay.Absent_Days = A_Absentdays;
                        pay.WeekOffDays = A_WeekOffdays;
                        pay.PayOutput_ESI(pay);

                        //s_query = "insert into PayOutput_Deductions values(" + pay.CompanyId + "," + pay.EmployeeId + "," + RegDeductionList[i_regded].DeductionId + ",'01/01/2008','01/01/2008','D'," + f_ESI + "," + Emp_ESIAmt + ")";

                        pay.DeductionId = RegDeductionList[i_regded].DeductionId;
                        pay.DeducationCode = RegDeductionList[i_regded].DeducationCode;
                        pay.Act_Amount = f_ESI;
                        pay.Ded_Amount = Emp_ESIAmt;
                        pay.status = 'D';
                        pay.PayOutput_Deductions(pay);
                        pay.Deduction_update_register(pay);
                        Net_Act_Ded = Net_Act_Ded + f_ESI;
                        Net_Ded = Net_Ded + Emp_ESIAmt;
                    }
                }
            }
        }
    }

    public void PT_Calculation()
    {
        if (RegDeductionList[i_regded].eligible == 'Y')
        {
            double bs = 0.0;

            SqlCommand cmd = new SqlCommand();

            cmd = new SqlCommand("select Earn_basic from payinput where pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + employee.BranchId + "' and pn_EmployeeID = '" + employee.EmployeeId + "' and d_date = '" + pay.strDate + "' ", con);

            SqlDataReader rds = cmd.ExecuteReader();
            if (rds.Read())
            {
                bs = Convert.ToDouble(rds[0]);
            }

            pay.F_Amount = f_pt + bs;

            if (PT_Settings[0].PTmonth == "12")
            {
                PT_Sub_Calculation3();
            }
            else
            {
                if (PT_Month_Calculation() == true)
                {
                    PT_Sub_Calculation1();
                    bool_Month = false;
                }
                else
                {
                    PT_Sub_Calculation2();
                }
            }
        }
    }

    public void IT_Calculation()
    {
        RegEarningsList = pay.fn_In_Earnings(pay);
        {
            for (i_regded = 0; i_regded < RegEarningsList.Count; i_regded++)
            {
                if (RegEarningsList[i_regded].OT == 'Y')
                {
                    f_ot = f_ot + E_actamt;
                }
                if (RegEarningsList[i_regded].LOP == 'Y')
                {
                    E_actamt = ((A_calcdays / A_paiddays) * 100);
                }
                else
                {
                    E_actamt = E_actamt + E_amt;
                }
            }
        }
    }

    public void LWF_Calculation()
    {
        str_Month = Convert.ToString(pay.d_ToDate).Substring(3, 2);

        if (Convert.ToInt32(str_Month) == LWF_Settings[0].LWF_Month)
        {
            i_length = Convert.ToInt32(str_Month) - 12;

            FromMonth_Calculation();

            Query = "select sum(Earn_Act_Amount) from PayOutput_Netpay where";
            Query = Query + " pn_EmployeeID=" + pay.EmployeeId + " and d_date between '" + str_temp + "' and '" + pay.d_date + "'";

            ds_PT = pay.fn_Output(Query);

            if (ds_PT.Tables[0].Rows.Count > 0)
            {
                i_temp = Convert.ToInt32(ds_PT.Tables[0].Rows[0][0]);
                if (LWF_Settings[0].LWF_Limit <= i_temp)
                {
                    LWF_Sub_Calculation();
                }
            }
        }
    }

    //*************************************************************************************************

    public void PT_Sub_Calculation1()
    {

        str_Month = Convert.ToString(pay.d_ToDate).Substring(3, 2);
        str_Year = Convert.ToString(pay.d_ToDate).Substring(6, 4);

        //12 is months ,2 is length of single month in database(01,02...)

        i_length = 12 / (PT_Settings[0].PTmonth.Length / 2);

        i_temp = i_length;

        i_length = Convert.ToInt32(str_Month) - i_length;

        FromMonth_Calculation();

        PT_Sub_Calculation3();

    }

    //public void PT_Sub_Calculation2()
    //{
    //    if (PT_Settings[0].PTtype == 'a')
    //    {
    //        Query = "select sum(Act_Amount) from PayOutput_Earnings poe,paym_Earnings pe where";
    //        Query = Query + " poe.pn_EmployeeID=" + pay.EmployeeId + " and poe.d_date between '" + str_temp + "' and '" + pay.d_date + "'";
    //        Query = Query + " and pe.c_Pt='Y' and poe.pn_EarningsID=pe.pn_EarningsID";
    //    }
    //    else
    //    {
    //        Query = "select sum(Amount) from PayOutput_Earnings poe,paym_Earnings pe where";
    //        Query = Query + " poe.pn_EmployeeID=" + pay.EmployeeId + " and poe.d_date between '" + str_temp + "' and '" + pay.d_date + "'";
    //        Query = Query + " and pe.c_Pt='Y' and poe.pn_EarningsID=pe.pn_EarningsID";
    //    }

    //    ds_PT = pay.fn_Output(Query);

    //    if (ds_PT.Tables[0].Rows.Count > 0)
    //    {
    //        int i = Convert.IsDBNull(ds_PT.Tables[0].Rows[0][0]) ? 0 : Convert.ToInt32(ds_PT.Tables[0].Rows[0][0]);
    //        if (i == 0)
    //        {
    //            pay.F_Amount = (Convert.ToInt32("0") / i_temp) * i_temp;
    //        }
    //        else
    //        {
    //            pay.F_Amount = (Convert.ToInt32(ds_PT.Tables[0].Rows[0][0]) / i_temp) * i_temp;
    //        }
    //        PT_Sub_Calculation3();
    //    }
    //}

    public void PT_Sub_Calculation2()
    {
        PTList = pay.fn_In_PT(pay);

        if (PTList.Count > 0)
        {
            //s_query = "insert into PayOutput_Deductions values(" + pay.CompanyId + "," + pay.EmployeeId + "," + RegDeductionList[i_regded].DeductionId + ",'01/01/2008','01/01/2008','D'," + f_pt + "," + PTList[0].T_Amount + ")";

            pay.DeductionId = RegDeductionList[i_regded].DeductionId;
            pay.DeducationCode = RegDeductionList[i_regded].DeducationCode;
            pay.Act_Amount = pay.F_Amount;
            pay.payinput_PT(pay);
        }
    }

    public void PT_Sub_Calculation3()
    {
        double PTG = 0.0;

        SqlCommand cmd = new SqlCommand();
        cmd = new SqlCommand("SELECT sum(PT_Gross) as gross_salary FROM payinput WHERE d_date >= DATEADD(MONTH, -6, GETDATE()) and pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + employee.BranchId + "' and pn_EmployeeID = '" + employee.EmployeeId + "' ", con);

        SqlDataReader rds = cmd.ExecuteReader();
        if (rds.Read())
        {
            PTG = Convert.IsDBNull(rds[0]) ? 0.0 : Convert.ToDouble(rds[0]);
        }

        pay.F_Amount = PTG;
        PTList = pay.fn_In_PT(pay);

        if (PTList.Count > 0)
        {
            //s_query = "insert into PayOutput_Deductions values(" + pay.CompanyId + "," + pay.EmployeeId + "," + RegDeductionList[i_regded].DeductionId + ",'01/01/2008','01/01/2008','D'," + f_pt + "," + PTList[0].T_Amount + ")";
            
            pay.DeductionId = RegDeductionList[i_regded].DeductionId;
            pay.DeducationCode = RegDeductionList[i_regded].DeducationCode;
            
            pay.Act_Amount = f_pt;
            pay.Ded_Amount = PTList[0].T_Amount;
            pay.status = 'D';

            pay.PayOutput_Deductions(pay);
            pay.Deduction_update_register(pay);
            Net_Act_Ded = Net_Act_Ded + f_pt;
            Net_Ded = Net_Ded + PTList[0].T_Amount;
        }

    }

    public bool PT_Month_Calculation()
    {
        str_Month = Convert.ToString(pay.d_ToDate).Substring(3, 2);

        i_length = PT_Settings[0].PTmonth.Length;
        str_temp = PT_Settings[0].PTmonth;

        for (i_temp = 0; i_temp <= i_length - 1; i_temp = i_temp + 2)
        {

            if (str_Month == str_temp.Substring(i_temp, 2))
            {
                bool_Month = true;
            }
        }

        return bool_Month;

    }

    public void FromMonth_Calculation()
    {

        if (i_length >= 0)
        {
            i_length = i_length + 1;

            if (i_length <= 9)
            {
                str_temp = "01/0" + i_length + "/" + str_Year;
            }
            else
            {
                str_temp = "01/" + i_length + "/" + str_Year;
            }
        }
        else
        {
            str_temp = Convert.ToString(i_length);

            str_temp = str_temp.Substring(1, str_temp.Length - 1);

            i_length = Convert.ToInt32(str_temp);

            i_temp = Convert.ToInt32(str_Year) - 1;

            if (i_length <= 9)
            {
                str_temp = "01/0" + i_length + "/" + i_temp;
            }
            else
            {
                str_temp = "01/" + i_length + "/" + i_temp;
            }
        }
    }

    public void LWF_Sub_Calculation()
    {
        //s_query = "insert into PayOutput_Deductions values(" + pay.CompanyId + "," + pay.EmployeeId + "," + RegDeductionList[i_regded].DeductionId + ",'01/01/2008','01/01/2008','D'," + i_temp + "," + LWF_Settings[0].LWF_Amt + ")";

        pay.DeductionId = RegDeductionList[i_regded].DeductionId;
        pay.Act_Amount = i_temp;
        pay.Ded_Amount = LWF_Settings[0].LWF_Amt;
        pay.status = 'D';

        pay.PayOutput_Deductions(pay);

        Net_Act_Ded = Net_Act_Ded + LWF_Settings[0].LWF_Amt;
        Net_Ded = Net_Ded + LWF_Settings[0].LWF_Amt;

    }

    public void Loan_Sub_Calulation()
    {
        pay.DeductionId = LoanEntryList[i_loan].loanid;
        pay.Act_Amount = Loan_Actamt;
        pay.status = 'L';

        if (Balance_Amt <= Loan_amt)
        {
            //s_query = "insert into PayOutput_Deductions values(" + pay.CompanyId + "," + pay.EmployeeId + "," + LoanEntryList[i_loan].loanid + ",'01/01/2008','01/01/2008','L'," + Loan_Actamt + "," + Balance_Amt + ")";

            pay.Ded_Amount = Balance_Amt;

            pay.PayOutput_Deductions(pay);

            //s_query = "insert into PayOutput_Loan values(" + pay.CompanyId + "," + pay.EmployeeId + "," + LoanEntryList[i_loan].loanid + ",'01/01/2008','01/01/2008','" + pay.d_date + "'," + Balance_Amt + ")";

            pay.float_Amount = Balance_Amt;

            pay.PayOutput_Loan(pay);

            s_query = "update LoanEntry set c_status='N' where pn_companyid=" + pay.CompanyId + " and pn_EmployeeID=" + pay.EmployeeId + " and fn_LoanID=" + LoanEntryList[i_loan].loanid + " and d_effdate='" + LoanEntryList[i_loan].d_EffDate + "'";

            pay.fn_In_Out(s_query);

            Net_Act_Ded = Net_Act_Ded + Loan_Actamt;
            Net_Ded = Net_Ded + Balance_Amt;
        }
        else
        {

            //s_query = "insert into PayOutput_Deductions values(" + pay.CompanyId + "," + pay.EmployeeId + ",'01/01/2008','01/01/2008'," + LoanEntryList[i_loan].loanid + ",'L'," + Loan_Actamt + "," + Loan_amt + ")";

            pay.Ded_Amount = Loan_amt;

            pay.PayOutput_Deductions(pay);

            // s_query = "insert into PayOutput_Loan values(" + pay.CompanyId + "," + pay.EmployeeId + "," + LoanEntryList[i_loan].loanid + ",'01/01/2008','01/01/2008','" + pay.d_date + "'," + Loan_amt + ")";

            pay.float_Amount = Loan_amt;

            pay.PayOutput_Loan(pay);

            Balance_Amt = Balance_Amt - Loan_amt;

            s_query = "update LoanEntry set Balance_Amt=" + Balance_Amt + " where pn_companyid=" + pay.CompanyId + " and pn_EmployeeID=" + pay.EmployeeId + " and fn_LoanID=" + LoanEntryList[i_loan].loanid + " and d_effdate='" + LoanEntryList[i_loan].d_EffDate + "'";

            pay.fn_In_Out(s_query);


            Net_Act_Ded = Net_Act_Ded + Loan_Actamt;
            Net_Ded = Net_Ded + Loan_amt;

        }
    }

    public void Normal_Calculation()
    {
        D_actamt = RegDeductionList[i_regded].Amount;

        D_amt = D_actamt;

        //s_query = "insert into PayOutput_Deductions values(" + pay.CompanyId + "," + pay.EmployeeId + "," + RegDeductionList[i_regded].DeductionId + ",'01/01/2008','01/01/2008','D'," + D_actamt + "," + D_amt + ")";

        pay.DeductionId = RegDeductionList[i_regded].DeductionId;
        pay.Act_Amount = D_actamt;
        pay.Ded_Amount = D_amt;
        pay.status = 'D';

        pay.PayOutput_Deductions(pay);

        Net_Act_Ded = Net_Act_Ded + D_actamt;
        Net_Ded = Net_Ded + D_amt;

    }

    public void OT_Calculation()
    {
        try
        {
            payinput = pay.fn_payinput_check(pay);
            if (payinput.Count > 0)
            {
                pay.Act_Basic = payinput[0].Act_Basic;
                pay.Earned_Basic = payinput[0].Earned_Basic;
                Ot_hrs = payinput[0].others;
                if (Ot_hrs == "01/01/1900 12:00:00 AM" || Ot_hrs == "01/01/1900 00:00:00")
                {
                    Ot_hrs = "1900/01/01 00:00:00";
                }
            }

            EmpProfileList = employee.fn_get_Emp_Profile1(employee);
            if (EmpProfileList[0].CategoryId != 1)
            {
                employee.CategoryName = employee.fn_GetCategoryName(EmpProfileList[0].CategoryId);
            }

 
            SqlCommand comm = new SqlCommand();
            TimeSpan otslb = new TimeSpan(0, 0, 0);
            comm = new SqlCommand("select ot_slab from otslab where ot_from < '" + Ot_hrs + "' and ot_to > '" + Ot_hrs + "' and pn_Category = '" + employee.CategoryName + "' and pn_branchID = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "'", con);
            SqlDataReader rdot = comm.ExecuteReader();
            if (rdot.Read())
            {
                DateTime ottime = Convert.ToDateTime(rdot[0]);
                pay.temp_str = ottime.ToString("HH:mm:ss");
                otslb = TimeSpan.Parse(pay.temp_str);
            }
            else
            {
                pay.temp_str = "0";
                otslb = new TimeSpan(0, 0, 0);
            }

            double ot_calc = 0, ot_amt = 0, ovt = 0, otround = 0;
            TimeSpan tot_ot = new TimeSpan(0, 0, 0);

            if (pay.temp_str == "0")
            {
                pay.temp_str = "00:00";
            }
            tot_ot = TimeSpan.Parse(pay.temp_str);
            string str_ot = "1900-01-01 " + pay.temp_str + ":00.000";
            string[] ot_spl = pay.temp_str.Split(':');
            if (pay.temp_str != "0")
            {
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
            }
            ovt = Convert.ToDouble(ot_spl[0]) + otround;


            com = new SqlCommand("SELECT OT_calc FROM paym_employee where  pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' and pn_EmployeeID = '" + pay.EmployeeId + "' ", con);
            SqlDataReader rd_otc = com.ExecuteReader();
            if (rd_otc.Read())
            {
                ot_calc = Convert.ToDouble(rd_otc[0]);
            }


            if (ovt > 0)
            {
                OT_Settings = pay.fn_In_OT_Settings1(pay);

                if (OT_Settings.Count > 0)
                {
                    ot_amt = (((pay.Act_Basic + f_ot) / OT_Settings[0].OT_Days) / OT_Settings[0].OT_HRS) * ovt * ot_calc;
                }
            }
            pay.OT_Days = ovt;
            pay.OT_Amt = ot_amt;
            OT_Amt = ot_amt;

        }
        catch (Exception ex)
        {

        }
    }

    public void Net_Calculation()
    {
        NetPay = Convert.ToInt32(Net_Earn) - Convert.ToInt32(Net_Ded);
        
        total_wdays = Convert.ToDouble(Txt_totdays.Text);
        
        pay.Earn_Act_Amount = Net_Act_Earn;
        pay.Earn_Act_Amount_A = Net_Act_Earn_A;
        pay.Earn_Amount = Net_Earn;
        //pay.OT_Amt = OT_Amt;
        pay.Ded_Act_Amount = Net_Act_Ded;
        pay.Ded_Amount = Net_Ded;
        pay.Net_Amount = NetPay;
        //pay.Gross_Salary = function_rd_next(pay.Earned_Basic + pay.Earn_Amount + OT_Amt);
        //pay.Net_Salary = function_rd_nearest(pay.Gross_Salary - pay.Ded_Amount);
        pay.Gross_Salary = pay.Earned_Basic + pay.Earn_Amount + pay.OT_Amt;
        pay.Net_Salary = function_rd_next(pay.Gross_Salary - pay.Ded_Amount);
        pay.PayOutput_NetPay(pay);
        pay.PayOutput_Actuals(pay);
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
    
    public void Normal_Status()
    {
        for (i_emp = 0; i_emp < chk_Empcode.Items.Count; i_emp++)
        {
            chk_Empcode.Items[i_emp].Selected = false;
        }
        Txt_fdate.Text = "";
        Txt_tdate.Text = "";
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

    public double function_rd_nearest(double rup)
    {
        if (rup > 0)
        {
            double value;
            value = Math.Round(rup);
            return value;
        }

        else
        {
            return 0;
        }
    }


    public double function_rd_next(double rup)
    {
        if (rup > 0)
        {
            double value;
            rup = rup + Convert.ToDouble(0.50);
            value = Math.Round(rup);
            return value;
        }

        else
        {
            return 0;
        }
    } 

    protected void Btn_undo_Click(object sender, EventArgs e)
    {
        pay.d_date = employee.Convert_ToSqlDate(date_change(Txt_tdate.Text));

        for (i_emp = 0; i_emp < chk_Empcode.Items.Count; i_emp++)
        {
            if (chk_Empcode.Items[i_emp].Selected == true)
            {
                pay.EmployeeId = Convert.ToInt32(chk_Empcode.Items[i_emp].Value);

                s_query = "Set Dateformat dmy;delete from PayOutput_Earnings where pn_CompanyID=" + pay.CompanyId + " and pn_BranchID='"+pay.BranchId+"' and d_Date='" + pay.d_date + "' and pn_EmployeeID=" + pay.EmployeeId + ";";
                s_query += "delete from PayOutput_Deductions where pn_CompanyID=" + pay.CompanyId + " and pn_BranchID='" + pay.BranchId + "' and d_Date='" + pay.d_date + "' and pn_EmployeeID=" + pay.EmployeeId + ";";
                s_query += "delete from PayOutput_ESI where pn_CompanyID=" + pay.CompanyId + " and pn_BranchID='" + pay.BranchId + "' and d_Date='" + pay.d_date + "' and pn_EmployeeID=" + pay.EmployeeId + ";";
                s_query += "delete from PayOutput_PF where pn_CompanyID=" + pay.CompanyId + " and pn_BranchID='" + pay.BranchId + "' and d_Date='" + pay.d_date + "' and pn_EmployeeID=" + pay.EmployeeId + ";";
                s_query += "delete from PayOutput_NetPay where pn_CompanyID=" + pay.CompanyId + " and pn_BranchID='" + pay.BranchId + "' and d_Date='" + pay.d_date + "' and pn_EmployeeID=" + pay.EmployeeId + ";";
                s_query += "delete from PayOutput_Actuals where pn_CompanyID=" + pay.CompanyId + " and pn_BranchID='" + pay.BranchId + "' and d_Date='" + pay.d_date + "' and pn_EmployeeID=" + pay.EmployeeId + ";";
                s_query += "delete from earn_deduct where pn_CompanyID=" + pay.CompanyId + " and pn_BranchID='" + pay.BranchId + "' and d_Date='" + pay.d_date + "' and pn_EmployeeID=" + pay.EmployeeId + ";";
                s_query += "delete from payinput where pn_CompanyID=" + pay.CompanyId + " and pn_BranchID='" + pay.BranchId + "' and d_Date='" + pay.d_date + "' and pn_EmployeeID=" + pay.EmployeeId + ";";
                //s_query += "delete from Payinput where pn_CompanyID=" + pay.CompanyId + " and pn_BranchID='" + pay.BranchId + "' and d_Date='" + pay.d_date + "' and pn_EmployeeID=" + pay.EmployeeId + ";";
                s_query += "delete from PayProcess where pn_CompanyID=" + pay.CompanyId + " and pn_BranchID='" + pay.BranchId + "' and d_Date='" + pay.d_date + "';set dateformat mdy;";
                employee.fn_reportbyid(s_query);
            }
        }

        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Roll Back Successful!');", true);
        clear();
    }

    protected void ddl_periodcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            con.Open();
            com = new SqlCommand("Select a.*, b.salary_period from salary_period a left outer join PayProcess b on a.Period_Code = b.salary_Period where a.pn_branchid = '" + employee.BranchId + "' and a.pn_companyid = '" + employee.CompanyId + "' and a.period_code = '" + ddl_periodcode.SelectedItem.Text + "'", con);
            SqlDataReader re1 = com.ExecuteReader();
            if (re1.Read())
            {
                if (re1["selection"].ToString() == "Month")
                {
                    RadioButtonList1.SelectedIndex = 0;
                }
                if (re1["selection"].ToString() == "Week")
                {
                    RadioButtonList1.SelectedIndex = 1;
                }
                if (re1["selection"].ToString() == "Day")
                {
                    RadioButtonList1.SelectedIndex = 2;
                }
                ddl_year.SelectedItem.Text = re1["p_year"].ToString();
                ddl_month.SelectedItem.Text = re1["p_month"].ToString();
                Txt_fdate.Text = Convert.ToDateTime(re1["from_date"]).ToString("dd/MM/yyyy");
                Txt_tdate.Text = Convert.ToDateTime(re1["to_date"]).ToString("dd/MM/yyyy");
                Txt_totdays.Text = Convert.ToString(re1["total_days"]);
                txt_paydate.Text = Convert.ToDateTime(re1["pay_date"]).ToString("dd/MM/yyyy");
                string paychk = re1["Salary_Period"].ToString();
                if (paychk == "")
                {
                    Btn_process.Enabled = true;
                    Btn_undo.Enabled = false;
                }
                else
                {
                    Btn_process.Enabled = false;
                    Btn_undo.Enabled = true;
                }

            }
            re1.Close();
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }

        finally
        {
            con.Close();
        }
    }
    protected void Btn_Show_Click(object sender, EventArgs e)
    {
        Emp.Visible = true ;
    }
    protected void ddl_branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_employee_load();
        load();
    }
    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Employee_load();
        Btn_process.Enabled = true;
        Btn_undo.Enabled = true;
    }
}

   
