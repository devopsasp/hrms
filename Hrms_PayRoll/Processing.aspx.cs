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
    Company company = new Company();
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    Candidate c = new Candidate();

    Collection<Candidate> WorkHistoryList;
    //Collection<Employee> AllowanceList;
    Collection<Company> CompanyList, ddlBranchsList;
    Collection<Employee> EmployeesList;

    Collection<PayRoll> AttendanceList;
    Collection<PayRoll> RegEarningsList;
    Collection<PayRoll> RegEarningsCheck;
    Collection<PayRoll> RegDeductionList;
    Collection<PayRoll> RegDeductionsCheck;
    Collection<PayRoll> NonRegEarningsList;
    Collection<PayRoll> NonRegDeductionList;
    Collection<PayRoll> PFList;
    Collection<PayRoll> ESIList;
    Collection<PayRoll> OTList;
    Collection<PayRoll> PTList;
    Collection<PayRoll> ITList;
    Collection<PayRoll> LoanEntryList;
    Collection<PayRoll> LoanCancelList;
    Collection<PayRoll> LoanPreCloserList;
    Collection<PayRoll> PayOutput_LoanList;

    Collection<PayRoll> Common_Settings;
    Collection<PayRoll> PT_Settings;
    Collection<PayRoll> OT_Settings;
    Collection<PayRoll> LWF_Settings;
    Collection<PayRoll> Bonus_Settings;
    Collection<PayRoll> CheckingList;
    DataSet ds_PT;
    DataSet ds_userrights;
  
    int  i_amt=0,i_temp=0,NetPay = 0,i_emp, i_regearn, i_nonregearn, i_regded, i_nonregded, i_Precloser, i_loan, i_loan_ex,i_length, emp_count=0;

    double d_temp=0,A_calcdays = 0, A_paiddays = 0, A_Absentdays = 0, A_WeekOffdays = 0, E_amt = 0, E_actamt = 0, N_E_amt = 0, N_E_actamt = 0, D_amt = 0, D_actamt = 0;
    double N_D_amt=0, N_D_actamt=0,f_ot = 0, f_pt = 0, f_PF = 0, f_ESI = 0,cal_amt=0 ,PFAmt = 0, EPFAmt = 0;
    double Emp_ESIAmt = 0, Er_ESIAmt = 0, OT_Amt = 0, PT_Amt = 0, IT_Amt = 0,Loan_amt=0,Loan_Actamt=0;
    double FPFAmt = 0,Balance_Amt = 0, Net_Earn = 0, Net_Ded = 0, Net_Act_Earn = 0, Net_Act_Ded = 0;

    string s_login_role;
    string str_temp = "", s_query = "", _path = "", str_Month = "", str_Year = "", Query = "", s_form;
    string pf_value;

    bool bool_Month=false;

    protected void Page_Load(object sender, EventArgs e)
    {

        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        employee.ESIno =(string)ViewState["Emp_Esino"];

        
       
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        lbl_Error.Text = "";

        if (!IsPostBack)
        {
            CompanyList = company.fn_getCompany();
            if (CompanyList.Count > 0)
            {
                //ddl_year_load();
                switch (s_login_role)
                {
                    case "a":
                        tab_ddl.Visible = false;
                        ddl_Branch_load();
                        break;

                    case "h":
                        tab_ddl.Visible = true;
                        ddl_Branch.Visible = false;
                        ddl_employee_load();
                        break;

                    case "u":
                        s_form = "45";

                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            tab_ddl.Visible = true;
                            ddl_Branch.Visible = false;
                            ddl_employee_load();
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

    public void ddl_employee_load()
    {
        if (s_login_role == "a")
        {
            employee.BranchId = (int)ViewState["Processing_BranchID"];
            pay.BranchId = (int)ViewState["Processing_BranchID"];
        }

        Collection<Employee> EmployeeList = employee.fn_getEmployeeList(employee);
        if (EmployeeList.Count > 0)
        {
            chk_Empcode.Visible = true;
            chk_Empcode.DataSource = EmployeeList;
            chk_Empcode.DataValueField = "EmployeeId";
            chk_Empcode.DataTextField = "FirstName";
            chk_Empcode.DataBind();
        }
        else
        {
            chk_Empcode.Visible = false;
            lbl_Error.Text = "No Employees Available";
        }
    }

    protected void btn_show_Click(object sender, ImageClickEventArgs e)
    {
        //pay.CompanyId = 1;

        //pay.d_FromDate = Convert.ToDateTime(txtFromDate.Value);
        //pay.d_ToDate = Convert.ToDateTime(txtToDate.Value);
        //pay.d_date = Convert.ToDateTime(date_change(txtToDate.Value));
        //For IIS
        pay.d_FromDate = employee.Convert_ToSqlDate(txtFromDate.Value);
        pay.d_ToDate = employee.Convert_ToSqlDate(txtToDate.Value);
        pay.d_date = employee.Convert_ToSqlDate(date_change(txtToDate.Value));
       
        //pay.d_date = Convert.ToDateTime(txtToDate.Text);

        

        CheckingList = pay.fn_Out_PayOutput_Netpay(pay);
        if (CheckingList.Count <= 0)
        {
            Initial_Processing();
            Normal_Status();
            if (emp_count != 0)
            {
                lbl_Error.Text = "Processing are Finished Sucessfully!";
            }
            else
            {
                lbl_Error.Text = "No Selected Employees";
            }
        }
        else
        {
            btn_show.Visible = false;
            btn_update.Visible = true;
            btn_delete.Visible = true;

            lbl_Error.Text = "Already,this Month Processing are Finished!";
        }
    }

    public void Initial_Processing()
    {
        PT_Settings = pay.fn_In_PTax_Settings(pay);
        if (PT_Settings.Count > 0)
        {
            for (i_emp = 0; i_emp < chk_Empcode.Items.Count; i_emp++)
            {
                if (chk_Empcode.Items[i_emp].Selected == true)
                {
                    pay.EmployeeId = Convert.ToInt32(chk_Empcode.Items[i_emp].Value);
                    employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[i_emp].Value);

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
        Non_Reg_Earnings();
        OT_Calculation();
        Reg_Deductions();
        Non_Reg_Deductions();
        Loan_Calculation();
        Net_Calculation();
    }


//********************Earnings*******************************************************************

    public void Reg_Earnings()
    {
        RegEarningsCheck = pay.fn_Check_Earnings(pay);

        if (RegEarningsCheck.Count > 0)
        {
            RegEarningsList = pay.fn_In_Earnings(pay);

            if (RegEarningsList.Count > 0)
            {
                for (i_regearn = 0; i_regearn < RegEarningsList.Count; i_regearn++)
                {
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

                    //s_query = "insert into PayOutput_Earnings values(" + pay.CompanyId + "," + pay.EmployeeId + "," + RegEarningsList[i_regearn].EarningsId + ",'01/01/2008','01/01/2008','E'," + E_actamt + "," + E_amt + ")";
                    //pay.CompanyId = 1;
                    //pay.EmployeeId =Convert.ToInt32(chk_Empcode.SelectedItem.Value);

                    pay.EarningsId = RegEarningsList[i_regearn].EarningsId;

                    //pay.d_date = Convert.ToDateTime(date_change(txtToDate.Value));
                    //pay.d_FromDate = Convert.ToDateTime(txtFromDate.Value);
                    //pay.d_ToDate = Convert.ToDateTime(txtToDate.Value);

                    pay.d_date = employee.Convert_ToSqlDate(date_change(txtToDate.Value));
                    pay.d_FromDate = employee.Convert_ToSqlDate(txtFromDate.Value);
                    pay.d_ToDate = employee.Convert_ToSqlDate(txtToDate.Value);

                    pay.status = 'E';
                    pay.Act_Amount = E_actamt;
                    pay.Earn_Amount = E_amt;

                    pay.PayOutput_Earnings(pay);

                    Net_Act_Earn = Net_Act_Earn + E_actamt;
                    Net_Earn = Net_Earn + E_amt;

                }
            }
        }
    }

    public void Non_Reg_Earnings()
    {

        NonRegEarningsList = pay.fn_In_Non_Earnings(pay);

        if (NonRegEarningsList.Count > 0)
        {

            for (i_nonregearn = 0; i_nonregearn < NonRegEarningsList.Count; i_nonregearn++)
            {
                N_E_actamt = NonRegEarningsList[i_nonregearn].Amount;

                if (NonRegEarningsList[i_nonregearn].OT == 'Y')
                {
                    f_ot = f_ot + N_E_actamt;
                }

                N_E_amt = N_E_actamt;

                if (NonRegEarningsList[i_nonregearn].PT == 'Y')
                {
                    f_pt = f_pt + N_E_amt;
                }

                if (RegEarningsList[i_nonregearn].Pf == 'Y')
                {
                    f_PF = f_PF + N_E_amt;
                }

                if (RegEarningsList[i_nonregearn].Esi == 'Y')
                {
                    f_ESI = f_ESI + N_E_amt;
                }

                //s_query = "insert into PayOutput_Earnings values(" + pay.CompanyId + "," + pay.EmployeeId + "," + NonRegEarningsList[i_nonregearn].EarningsId + ",'01/01/2008','01/01/2008','E'," + E_actamt + "," + E_amt + ")";

                pay.EarningsId = NonRegEarningsList[i_nonregearn].EarningsId;

                //pay.d_date = Convert.ToDateTime(date_change(txtToDate.Value));
                //pay.d_FromDate = Convert.ToDateTime(txtFromDate.Value);
                //pay.d_ToDate = Convert.ToDateTime(txtToDate.Value);

                pay.d_date = employee.Convert_ToSqlDate(date_change(txtToDate.Value));
                pay.d_FromDate = employee.Convert_ToSqlDate(txtFromDate.Value);
                pay.d_ToDate = employee.Convert_ToSqlDate(txtToDate.Value);

                pay.Act_Amount = N_E_actamt;
                pay.Earn_Amount = N_E_amt;
                
                pay.status = 'E';

                pay.PayOutput_Earnings(pay);

                Net_Act_Earn = Net_Act_Earn + N_E_actamt;
                Net_Earn = Net_Earn + N_E_amt;

            }
        }
    }

    public void OT_Calculation()
    {
        //if(AttendanceList[0].OT_HRS < 1)
        if (AttendanceList[0].OT_HRS >0)
        {
            OTList = pay.fn_In_OT(pay);
            OT_Settings = pay.fn_In_OT_Settings1(pay);

            if (OTList.Count > 0 && OT_Settings.Count > 0)
            {
                   OT_Amt = ((f_ot / OT_Settings[0].OT_Days) / OT_Settings[0].OT_HRS) * AttendanceList[0].OT_HRS;
                    
                //s_query = "insert into PayOutput_Earnings values(" + pay.CompanyId + "," + pay.EmployeeId + "," + OTList[0].EarningsId + ",'01/01/2008','01/01/2008','E'," + f_ot + "," + OT_Amt + ")";

                    pay.EarningsId = OTList[0].EarningsId;
                    pay.Act_Amount = 0.0;//f_ot;bysan
                    pay.Earn_Amount = OT_Amt;
                    f_ESI = f_ESI + OT_Amt;
                    pay.status = 'E';

                    pay.PayOutput_Earnings(pay);
            }
        }
    }

//********************Deductions****************************************************************

    public void Reg_Deductions()
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

                    case "ESI":ESI_Calculation();
                        break;

                    case "PT": PT_Calculation();
                        break;

                    case "IT": IT_Calculation();
                        break;

                    case "LWF": LWF_Settings = pay.fn_In_LWF_Settings(pay);
                                if (LWF_Settings.Count > 0)
                                {
                                    LWF_Calculation();
                                }
                        break;
                    

                    default: Normal_Calculation();
                        break;
                } 
            }
        }
    }

    public void Non_Reg_Deductions()
    {

        NonRegDeductionList = pay.fn_In_Non_Deduction(pay);

        if (NonRegDeductionList.Count > 0)
        {

            for (i_nonregded = 0; i_nonregded < NonRegDeductionList.Count; i_nonregded++)
            {

                N_D_actamt = NonRegDeductionList[i_nonregded].Amount;

                N_D_amt = N_D_actamt;

                //s_query = "insert into PayOutput_Deductions values(" + pay.CompanyId + "," + pay.EmployeeId + "," + NonRegDeductionList[i_nonregded].DeductionId + ",'01/01/2008','01/01/2008','D'," + N_D_actamt + "," + N_D_amt + ")";

                pay.DeductionId = NonRegDeductionList[i_nonregded].DeductionId;
                pay.Act_Amount = N_D_actamt;
                pay.Ded_Amount= N_D_amt;
                pay.status = 'D';

                pay.PayOutput_Deductions(pay);


                Net_Act_Ded = Net_Act_Ded + N_D_actamt;
                Net_Ded = Net_Ded + N_D_amt;
            }
        }
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

                if (f_PF <= PFList[0].Amount)
                {
                    cal_amt = f_PF;
                }
                else
                {
                    cal_amt = PFList[0].Amount;
                }

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
                pay.Net_Amount = round_up(f_PF);
                pay.Emp_Con_PF = PFAmt;
                pay.Emp_Con_EPF = EPFAmt;
                pay.Emp_Con_FPF = FPFAmt;
                pay.Emp_Con_VPF = 0;
                pay.Paid_Days = A_paiddays;
                pay.Absent_Days = A_Absentdays;
                pay.WeekOffDays = A_WeekOffdays;


                pay.PayOutput_PF(pay);

                //s_query = "insert into PayOutput_Deductions values(" + pay.CompanyId + "," + pay.EmployeeId + "," + RegDeductionList[i_regded].DeductionId + ",'01/01/2008','01/01/2008','D'," + f_PF + "," + PFAmt + ")";

                pay.DeductionId = RegDeductionList[i_regded].DeductionId;
                pay.Act_Amount = f_PF;
                pay.Ded_Amount = PFAmt;
                pay.status = 'D';

                pay.PayOutput_Deductions(pay);

                Net_Act_Ded = Net_Act_Ded + f_PF;
                Net_Ded = Net_Ded + PFAmt;

            }
        }
    }

    public void ESI_Calculation()
    {

        if (RegDeductionList[i_regded].eligible == 'Y')
        {
            ESIList = pay.fn_In_ESI(pay);

            if (ESIList.Count > 0)
            {
                //f_ESI = f_ESI + OT_Amt;
                if (f_ESI<= ESIList[0].Amount)
                {
                    Emp_ESIAmt = f_ESI  * (ESIList[0].Emp_Con / 100);
                    Er_ESIAmt = f_ESI * (ESIList[0].Employer_Con / 100);

                    //s_query = "insert into PayOutput_ESI values(" + pay.CompanyId + "," + pay.EmployeeId + ",'043','01/01/2008','01/01/2008'," + f_ESI + "," + Emp_ESIAmt + "," + Er_ESIAmt + "," + A_paiddays + "," + A_Absentdays + "," + A_WeekOffdays + ")";
                    //pay.ESI_No = (int)ViewState["Esino"];

                    //bysan
                    //employee.CompanyId = 1;
                    //employee.BranchId = 1;
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
                    pay.Net_Amount = round_up(f_ESI);
                    pay.Emp_Con = Emp_ESIAmt;
                    pay.Employer_Con = Er_ESIAmt;                   
                    pay.Paid_Days = A_paiddays;
                    pay.Absent_Days = A_Absentdays;
                    pay.WeekOffDays = A_WeekOffdays;

                    pay.PayOutput_ESI(pay);

                    //s_query = "insert into PayOutput_Deductions values(" + pay.CompanyId + "," + pay.EmployeeId + "," + RegDeductionList[i_regded].DeductionId + ",'01/01/2008','01/01/2008','D'," + f_ESI + "," + Emp_ESIAmt + ")";

                    pay.DeductionId = RegDeductionList[i_regded].DeductionId;
                    pay.Act_Amount = f_ESI;
                    pay.Ded_Amount = Emp_ESIAmt;
                    pay.status = 'D';
                    pay.PayOutput_Deductions(pay);

                    Net_Act_Ded = Net_Act_Ded + f_ESI;
                    Net_Ded = Net_Ded + Emp_ESIAmt;

                }
            }
        }
    }

    public void PT_Calculation()
    {

        if (RegDeductionList[i_regded].eligible == 'Y')
        {
            pay.F_Amount = f_pt;

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
                    E_actamt = E_actamt + E_amt;
                
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
                i_temp=Convert.ToInt32(ds_PT.Tables[0].Rows[0][0]);

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

        i_temp=i_length;

        i_length = Convert.ToInt32(str_Month) - i_length;


        FromMonth_Calculation();

        PT_Sub_Calculation2();

    }

    public void PT_Sub_Calculation2()
    {

        if (PT_Settings[0].PTtype == 'a')
        {

            Query = "select sum(Act_Amount) from PayOutput_Earnings poe,paym_Earnings pe where";
            Query = Query + " poe.pn_EmployeeID=" + pay.EmployeeId + " and poe.d_date between '" + str_temp + "' and '" + pay.d_date + "'";
            Query = Query + " and pe.c_Pt='Y' and poe.pn_EarningsID=pe.pn_EarningsID";

        }
        else
        {
            Query = "select sum(Amount) from PayOutput_Earnings poe,paym_Earnings pe where";
            Query = Query + " poe.pn_EmployeeID=" + pay.EmployeeId + " and poe.d_date between '" + str_temp + "' and '" + pay.d_date + "'";
            Query = Query + " and pe.c_Pt='Y' and poe.pn_EarningsID=pe.pn_EarningsID";

        }

        ds_PT = pay.fn_Output(Query);

        if (ds_PT.Tables[0].Rows.Count > 0)
        {
            int i=Convert.IsDBNull(ds_PT.Tables[0].Rows[0][0])?0: Convert.ToInt32( ds_PT.Tables[0].Rows[0][0]);            
            if ( i==0)
            {
                pay.F_Amount = (Convert.ToInt32("0") / i_temp) * i_temp;
            }
            else
            {
                pay.F_Amount = (Convert.ToInt32(ds_PT.Tables[0].Rows[0][0]) / i_temp) * i_temp;
            }
            PT_Sub_Calculation3();

        }
    }

    public void PT_Sub_Calculation3()
    {
        PTList = pay.fn_In_PT(pay);

        if (PTList.Count > 0)
        {
            
            //s_query = "insert into PayOutput_Deductions values(" + pay.CompanyId + "," + pay.EmployeeId + "," + RegDeductionList[i_regded].DeductionId + ",'01/01/2008','01/01/2008','D'," + f_pt + "," + PTList[0].T_Amount + ")";

            pay.DeductionId = RegDeductionList[i_regded].DeductionId;
            pay.Act_Amount = f_pt;
            pay.Ded_Amount = PTList[0].T_Amount;
            pay.status = 'D';

            pay.PayOutput_Deductions(pay);

            Net_Act_Ded = Net_Act_Ded + f_pt;
            Net_Ded = Net_Ded + PTList[0].T_Amount;
        }

    }      
    
    public bool PT_Month_Calculation()
    {
        str_Month =Convert.ToString(pay.d_ToDate).Substring(3, 2);

        i_length = PT_Settings[0].PTmonth.Length;
        str_temp = PT_Settings[0].PTmonth;

        for (i_temp = 0; i_temp <= i_length-1; i_temp = i_temp+2)
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

    public void Net_Calculation()
    {
        //double Netpay;
        //Netpay = Net_Earn - Net_Ded;
        
        NetPay = Convert.ToInt32(Net_Earn) - Convert.ToInt32(Net_Ded);
        
        //s_query = "insert into PayOutput_NetPay values(" + pay.CompanyId + "," + pay.EmployeeId + ",'01/01/2008','01/01/2008'," + Net_Act_Earn + "," + Net_Earn + "," + Net_Act_Ded + "," + Net_Ded + "," + NetPay + ")";

        pay.Earn_Act_Amount = Net_Act_Earn;
        pay.Earn_Amount = Net_Earn;
        pay.Ded_Act_Amount = Net_Act_Ded;
        pay.Ded_Amount = Net_Ded;
        pay.Net_Amount = NetPay;

        pay.PayOutput_NetPay(pay);

    }

    public int round_up(double d_amt) 
    {
        i_amt = Convert.ToInt32(d_amt);

        return i_amt;

    }

    public string date_change(string str_date)
    {
       str_date = "01" + str_date.Substring(2, 8);

        return str_date;

    }

    protected void btn_update_Click(object sender, ImageClickEventArgs e)
    {
        
        //pay.PayOutput_Deleteion(pay);
        pay.d_date = employee.Convert_ToSqlDate(date_change(txtToDate.Value));

        for (i_emp = 0; i_emp < chk_Empcode.Items.Count; i_emp++)
        {
            if (chk_Empcode.Items[i_emp].Selected == true)
            {
                pay.EmployeeId = Convert.ToInt32(chk_Empcode.Items[i_emp].Value);
                //employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[i_emp].Value);

                s_query = "delete from PayOutput_Earnings where pn_CompanyID=" + pay.CompanyId + " and d_Date='" + pay.d_date + "' and pn_EmployeeID=" + pay.EmployeeId + ";";
                s_query += "delete from PayOutput_Deductions where pn_CompanyID=" + pay.CompanyId + " and d_Date='" + pay.d_date + "' and pn_EmployeeID=" + pay.EmployeeId + ";";
                s_query += "delete from PayOutput_ESI where pn_CompanyID=" + pay.CompanyId + " and d_Date='" + pay.d_date + "' and pn_EmployeeID=" + pay.EmployeeId + ";";
                s_query += "delete from PayOutput_PF where pn_CompanyID=" + pay.CompanyId + " and d_Date='" + pay.d_date + "' and pn_EmployeeID=" + pay.EmployeeId + ";";
                s_query += "delete from PayOutput_NetPay where pn_CompanyID=" + pay.CompanyId + " and d_Date='" + pay.d_date + "' and pn_EmployeeID=" + pay.EmployeeId + ";";
                employee.fn_reportbyid(s_query);
            }
        }
        Initial_Processing();
        if (emp_count != 0)
        {
            lbl_Error.Text = "Processing are Updated Sucessfully!";
            btn_show.Visible = true;
            btn_update.Visible = false;
            btn_delete.Visible = false;
        }
        else
        {
            lbl_Error.Text = "No Selected Employees";
        }
        Normal_Status();
    }

    protected void btn_delete_Click(object sender, ImageClickEventArgs e)
    {
        //pay.PayOutput_Deleteion(pay);
        pay.d_date = employee.Convert_ToSqlDate(date_change(txtToDate.Value));

        for (i_emp = 0; i_emp < chk_Empcode.Items.Count; i_emp++)
        {
            if (chk_Empcode.Items[i_emp].Selected == true)
            {
                pay.EmployeeId = Convert.ToInt32(chk_Empcode.Items[i_emp].Value);
                //employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[i_emp].Value);

                s_query = "delete from PayOutput_Earnings where pn_CompanyID=" + pay.CompanyId + " and d_Date='" + pay.d_date + "' and pn_EmployeeID=" + pay.EmployeeId + ";";
                s_query += "delete from PayOutput_Deductions where pn_CompanyID=" + pay.CompanyId + " and d_Date='" + pay.d_date + "' and pn_EmployeeID=" + pay.EmployeeId + ";";
                s_query += "delete from PayOutput_ESI where pn_CompanyID=" + pay.CompanyId + " and d_Date='" + pay.d_date + "' and pn_EmployeeID=" + pay.EmployeeId + ";";
                s_query += "delete from PayOutput_PF where pn_CompanyID=" + pay.CompanyId + " and d_Date='" + pay.d_date + "' and pn_EmployeeID=" + pay.EmployeeId + ";";
                s_query += "delete from PayOutput_NetPay where pn_CompanyID=" + pay.CompanyId + " and d_Date='" + pay.d_date + "' and pn_EmployeeID=" + pay.EmployeeId + ";";
                employee.fn_reportbyid(s_query);
            }
        }
        btn_show.Visible = true;
        btn_update.Visible = false;
        btn_delete.Visible = false;
    }

   public void Normal_Status()
    {
        
        for (i_emp = 0; i_emp < chk_Empcode.Items.Count; i_emp++)
        {
            chk_Empcode.Items[i_emp].Selected = false;
            
        }

        txtFromDate.Value = "";
        txtToDate.Value = "";
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
            ViewState["Processing_BranchID"] = Convert.ToInt32(ddl_Branch.SelectedValue);
            tab_ddl.Visible = true;
            ddl_employee_load();
        }
        else
        {
            tab_ddl.Visible = false;
        }
    }


   
}
