using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.ObjectModel;
using ePayHrms.Connection;
using ePayHrms.Company;
using ePayHrms.Employee;

/// <summary>
/// Summary description for PayRoll
/// </summary>
public class PayRoll
{
    public PayRoll()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private SqlConnection _Connection;
    ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();
    private decimal _Grauity;
    private decimal _encashment_amt;
    private decimal _deduct_salary_amt;
    private decimal _final_amt;
    private string _joining_date;
    private string _Status;

    public string Status
    {
        get { return _Status; }
        set { _Status = value; }
    }
    public string Joining_date
    {
        get { return _joining_date; }
        set { _joining_date = value; }
    }
    private string _retire_date;

    public string Retire_date
    {
        get { return _retire_date; }
        set { _retire_date = value; }
    }

    private string _refno;

    public string Refno
    {
        get { return _refno; }
        set { _refno = value; }
    }

    private string _last_working_date;

    public string Last_working_date
    {
        get { return _last_working_date; }
        set { _last_working_date = value; }
    }
    public decimal Final_amt
    {
        get { return _final_amt; }
        set { _final_amt = value; }
    }

    public decimal Deduct_salary_amt
    {
        get { return _deduct_salary_amt; }
        set { _deduct_salary_amt = value; }
    }

    public decimal Encashment_amt
    {
        get { return _encashment_amt; }
        set { _encashment_amt = value; }
    }
    public decimal Grauity
    {
        get { return _Grauity; }
        set { _Grauity = value; }
    } 




    private int _CompanyId;
    private int _BranchId;
    private int _EmployeeId;
    private string _EmployeeCode;
    private int _EarningsId;
    private string _EarningsName;
    private string _EarningsCode;
    private int _DeductionId;
    private string _DeductionName;
    private string _DeducationCode;
    private DateTime _Date;
    private TimeSpan _hrs;
    private string period_code;
    private double _Amount;
    private char _Pf;
    private char _Esi;
    private char _OT;
    private char _LOP;
    private char _PT;
    private char _Print;
    private char _regular;
    private char _status;
    private char _eligible;
    private string max_ceiling;
    private double upper_limit;
    private int max_amount;
    public string leave_name;
    private int _temp_int;
    private string _temp_str;

    private double _Pf_Amount;
    private double _Emp_Con;
    private double _Employer_Con;
    private double _Emp_Con_PF;
    private double _Emp_Con_EPF;
    private double _Emp_Con_FPF;
    private double _Vpf;
    private double _charges;
    private char _c_Round;

    private int _Month;
    private int _F_Month;
    private int _T_Month;
    private int _LWF_Limit;
    private int _LWF_Month;
    private double _LWF_Amt;

    private int _Year;
    private DateTime _d_FromDate;
    private DateTime _d_ToDate;
    private DateTime _d_date;
    private DateTime _d_EffDate;
    private double _Calc_Days;
    private double _Paid_Days;
    private double _Present_Days;
    private double _Absent_Days;
    private double _TotLeave_Days;
    private double _WeekOffDays;
    private double _Holidays;
    private string _Week_Holiday1;
    private string _Week_Holiday2;
    private double _OnDuty_days;
    private double _Tour_Days;
    private double _Compoff_Days;
    private string _Att_Bonus;
    private double _Att_BonusAmount;
    private double _OT_HRS;
    private double _OT_Amt;
    private double _T_Amount;
    private int _To_Amount;
    private double _F_Amount;

    private double _Quaterly;
    private double _Annual;
    private int _Count;
    private double _Earn_Arrears;
    private double _Ded_Arrears;

    private double _OT_Days;
    private double _EL_Days;
    private DateTime _from_date;
    private DateTime _to_date;
    private int _bonusfrommonth;
    private int _bonustomonth;
    private int _bonuslimit;
    private int _lastpaymonth;
    private int _ptcount;
    private string _PTmonths;
    private char _PTtype;
    private char _WH_Status;
    private char _Bonus_Type;

    private double _Earn_Amount;
    private double _Ded_Amount;
    private double _Emp_Con_VPF;
    private string _ESI_No;
    private string _PF_No;
    private double _Act_Amount;
    private double _Earn_Act_Amount;
    private double _Earn_Act_Amount_A;
    private double _Ded_Act_Amount;
    private double _float_Amount;
    private double _Net_Amount;
    private double _Act_Basic;
    private double _Earned_Basic;
    private double _Gross_Salary;
    private double _Net_Salary;

    //Loan

    private int _loanid;
    private string _loanname;
    private string _loancode;
    private int _bankid;
    private string _bankname;
    private string _bankcode;
    private string _Branch_Name;
    private string _Account_Type;
    private string _Micr_Code;
    private string _Ifsc_Code;
    private string _Address;
    private string _Others;
    private DateTime _dateapplication;
    private DateTime _effectivedate;
    private int _installmentcount;
    private int _loancloserid;
    private DateTime _applicationdate;
    private int _loanamount;
    private double _balanceamount;
    private double _paidamount;
    private double _closureamount;
    private string _checkno;
    private DateTime _checkdate;
    private string _checkamount;
    private string _Remarks;
    private int _holidayid;
    private DateTime _holidaydate;
    private DateTime _holidaytodate;
    private string _holidayname;
    private char _Common_Flag;
    private char _Month_Calc;
    private string _Month_Type;
    private int _Manual_Days;
    private char _OT_Flag;
    private double _Admin_Charges;
    private double _Eligibility_Amt;
    private char _Round;
    private double _Pro_Tax_Amt;
    private double _half_monthly;
    private string _Str_From_Date;
    private string _Str_To_Date;

    private DateTime _eff_date;
    private int _Overheadingid;
    private string _OverheadingName;

    public string OverheadingName
    {
        get { return _OverheadingName; }
        set { _OverheadingName = value; }
    }
    public int Overheadingid
    {
        get { return _Overheadingid; }
        set { _Overheadingid = value; }
    }

    public DateTime eff_date
    {
        get { return _eff_date; }
        set { _eff_date = value; }
    }

    private DateTime _app_date;

    public DateTime app_date
    {
        get { return _app_date; }
        set { _app_date = value; }
    }

    private string _DurationFrom;

    public string DurationFrom
    {
        get { return _DurationFrom; }
        set { _DurationFrom = value; }
    }

    private string _DurationTo;

    public string DurationTo
    {
        get { return _DurationTo; }
        set { _DurationTo = value; }
    }

    private string _loanprocess;

    public string loanprocess
    {
        get { return _loanprocess; }
        set { _loanprocess = value; }
    }

    private string _loan_mas_id;

    public string loan_mas_id
    {
        get { return _loan_mas_id; }
        set { _loan_mas_id = value; }
    }


    private decimal _loaninterest_amt;

    public decimal loaninterest_amt
    {
        get { return _loaninterest_amt; }
        set { _loaninterest_amt = value; }
    }

    private string _pay_mode;
    public string pay_mode
    {
        get { return _pay_mode; }
        set { _pay_mode = value; }
    }

    private decimal _loan_amt;
    public decimal loan_amt
    {
        get { return _loan_amt; }
        set { _loan_amt = value; }
    }

    private string _loan_calc;

    public string loan_calc
    {
        get { return _loan_calc; }
        set { _loan_calc = value; }
    }

    private double _loaninterest;
    public double loaninterest
    {
        get { return _loaninterest; }
        set { _loaninterest = value; }
    }

    private string _EmployeeName;

    public string EmployeeName
    {
        get { return _EmployeeName; }
        set { _EmployeeName = value; }
    }

    private double _tot_interest_amt;
    public double tot_interest_amt
    {
        get { return _tot_interest_amt; }
        set { _tot_interest_amt = value; }
    }

    private string _loan_process;

    public string loan_process
    {
        get { return _loan_process; }
        set { _loan_process = value; }
    }

    private string _loanappid;

    public string loanappid
    {
        get { return _loanappid; }
        set { _loanappid = value; }
    }

    public DateTime fromdate
    {
        get { return _from_date; }
        set { _from_date = value; }
    }

    public DateTime todate
    {
        get { return _to_date; }
        set { _to_date = value; }
    }

    private TimeSpan _fromtime;

    public TimeSpan fromtime
    {
        get { return _fromtime; }
        set { _fromtime = value; }
    }

    private TimeSpan _totime;

    public TimeSpan totime
    {
        get { return _totime; }
        set { _totime = value; }
    }

    public string periodCode
    {
        get { return period_code; }
        set { period_code = value; }
    }

    public string maxceiling
    {
        get { return max_ceiling; }
        set { max_ceiling = value; }
    }

    public double upperlimit
    {
        get { return upper_limit; }
        set { upper_limit = value; }
    }

    public int maxamount
    {
        get { return max_amount; }
        set { max_amount = value; }
    }

    public int BonusFromMonth
    {
        get { return _bonusfrommonth; }
        set { _bonusfrommonth = value; }
    }

    public int BonusToMonth
    {
        get { return _bonustomonth; }
        set { _bonustomonth = value; }
    }

    public int Bonuslimit
    {
        get { return _bonuslimit; }
        set { _bonuslimit = value; }
    }

    public int lastPaymonth
    {
        get { return _lastpaymonth; }
        set { _lastpaymonth = value; }
    }


    public int PTcount
    {
        get { return _ptcount; }
        set { _ptcount = value; }
    }


    public string PTmonth
    {
        get { return _PTmonths; }
        set { _PTmonths = value; }
    }


    public char PTtype
    {
        get { return _PTtype; }
        set { _PTtype = value; }
    }



    public char WH_Status
    {
        get { return _WH_Status; }
        set { _WH_Status = value; }
    }


    public char BonusType
    {
        get { return _Bonus_Type; }
        set { _Bonus_Type = value; }
    }
        
    public int CompanyId
    {
        get { return _CompanyId; }
        set { _CompanyId = value; }
    }

    public int BranchId
    {
        get { return _BranchId; }
        set { _BranchId = value; }
    }

    public int EmployeeId
    {
        get { return _EmployeeId; }
        set { _EmployeeId = value; }
    }


    public string EmployeeCode
    {
        get { return _EmployeeCode; }
        set { _EmployeeCode = value; }
    }


    public string temp_str
    {
        get { return _temp_str; }
        set { _temp_str = value; }
    }


    public int temp_int
    {
        get { return _temp_int; }
        set { _temp_int = value; }
    }


    public int EarningsId
    {
        get { return _EarningsId; }
        set { _EarningsId = value; }
    }

    public string EarningsName
    {
        get { return _EarningsName; }
        set { _EarningsName = value; }
    }

    public string EarningsCode
    {
        get { return _EarningsCode; }
        set { _EarningsCode = value; }
    }

    public int DeductionId
    {
        get { return _DeductionId; }
        set { _DeductionId = value; }
    }

    public string DeductionName
    {
        get { return _DeductionName; }
        set { _DeductionName = value; }
    }

    public string DeducationCode
    {
        get { return _DeducationCode; }
        set { _DeducationCode = value; }
    }

    public DateTime Date
    {
        get { return _Date; }
        set { _Date = value; }
    }

    public TimeSpan hrs
    {
        get { return _hrs; }
        set { _hrs = value; }
    }

    public DateTime d_FromDate
    {
        get { return _d_FromDate; }
        set { _d_FromDate = value; }
    }


    public DateTime d_ToDate
    {
        get { return _d_ToDate; }
        set { _d_ToDate = value; }
    }

    public DateTime d_EffDate
    {
        get { return _d_EffDate; }
        set { _d_EffDate = value; }
    }

    public string leavename
    {
        get { return _leave_name; }
        set { _leave_name = value; }
    }
    public string leavecode
    {
        get { return _leave_code; }
        set { _leave_code = value; }
    }
    public int  leaveId
    {
        get { return _leave_Id; }
        set { _leave_Id = value; }
    }

    public double Amount
    {
        get { return _Amount; }
        set { _Amount = value; }
    }

    public double Quaterly
    {
        get { return _Quaterly; }
        set { _Quaterly = value; }
    }

    public double Annual
    {
        get { return _Annual; }
        set { _Annual = value; }
    }

    public double F_Amount
    {
        get { return _F_Amount; }
        set { _F_Amount = value; }
    }

    public double T_Amount
    {
        get { return _T_Amount; }
        set { _T_Amount = value; }
    }

    public int To_Amount
    {
        get { return _To_Amount; }
        set { _To_Amount = value; }
    }

    public char Pf
    {
        get { return _Pf; }
        set { _Pf = value; }
    }

    public double VPf
    {
        get { return _Vpf; }
        set { _Vpf = value; }
    }

    public char Esi
    {
        get { return _Esi; }
        set { _Esi = value; }

    }

    public char regular
    {
        get { return _regular; }
        set { _regular = value; }
    }

    public char status
    {
        get { return _status; }
        set { _status = value; }
    }

    public char eligible
    {
        get { return _eligible; }
        set { _eligible = value; }
    }


    public char OT
    {
        get { return _OT; }
        set { _OT = value; }
    }

    public char LOP
    {
        get { return _LOP; }
        set { _LOP = value; }

    }

    public char PT
    {
        get { return _PT; }
        set { _PT = value; }
    }

    public char Print
    {
        get { return _Print; }
        set { _Print = value; }
    }


    public double Pf_Amount
    {
        get { return _Pf_Amount; }
        set { _Pf_Amount = value; }
    }


    public double Net_Amount
    {
        get { return _Net_Amount; }
        set { _Net_Amount = value; }
    }


    public double Earn_Act_Amount
    {
        get { return _Earn_Act_Amount; }
        set { _Earn_Act_Amount = value; }
    }

    public double Earn_Act_Amount_A
    {
        get { return _Earn_Act_Amount_A; }
        set { _Earn_Act_Amount_A = value; }
    }


    public double Ded_Act_Amount
    {
        get { return _Ded_Act_Amount; }
        set { _Ded_Act_Amount = value; }
    }


    public double float_Amount
    {
        get { return _float_Amount; }
        set { _float_Amount = value; }
    }


    public double Act_Amount
    {
        get { return _Act_Amount; }
        set { _Act_Amount = value; }
    }


    public double Earn_Amount
    {
        get { return _Earn_Amount; }
        set { _Earn_Amount = value; }
    }


    public double Act_Basic
    {
        get { return _Act_Basic; }
        set { _Act_Basic = value; }
    }

    public double Earned_Basic
    {
        get { return _Earned_Basic; }
        set { _Earned_Basic = value; }
    }

    public double Gross_Salary
    {
        get { return _Gross_Salary; }
        set { _Gross_Salary = value; }
    }

    public double Net_Salary
    {
        get { return _Net_Salary; }
        set { _Net_Salary = value; }
    }

    public string PF_No
    {
        get { return _PF_No; }
        set { _PF_No = value; }
    }


    public string ESI_No
    {
        get { return _ESI_No; }
        set { _ESI_No = value; }
    }

    public double Ded_Amount
    {
        get { return _Ded_Amount; }
        set { _Ded_Amount = value; }
    }


    public double Emp_Con_PF
    {
        get { return _Emp_Con_PF; }
        set { _Emp_Con_PF = value; }
    }
    public double Emp_Con_EPF
    {
        get { return _Emp_Con_EPF; }
        set { _Emp_Con_EPF = value; }
    }

    public double Emp_Con_VPF
    {
        get { return _Emp_Con_VPF; }
        set { _Emp_Con_VPF = value; }
    }


    public double Emp_Con
    {
        get { return _Emp_Con; }
        set { _Emp_Con = value; }
    }
    public double Employer_Con
    {
        get { return _Employer_Con; }
        set { _Employer_Con = value; }
    }

    public double Emp_Con_FPF
    {
        get { return _Emp_Con_FPF; }
        set { _Emp_Con_FPF = value; }
    }

    public double charges
    {
        get { return _charges; }
        set { _charges = value; }
    }


    public char c_Round
    {
        get { return _c_Round; }
        set { _c_Round = value; }
    }



    public int F_Month
    {
        get { return _F_Month; }
        set { _F_Month = value; }
    }

    public int T_Month
    {
        get { return _T_Month; }
        set { _T_Month = value; }
    }

    public int LWF_Limit
    {
        get { return _LWF_Limit; }
        set { _LWF_Limit = value; }
    }

    public int LWF_Month
    {
        get { return _LWF_Month; }
        set { _LWF_Month = value; }
    }


    public double LWF_Amt
    {
        get { return _LWF_Amt; }
        set { _LWF_Amt = value; }
    }


    public int Month
    {
        get { return _Month; }
        set { _Month = value; }
    }

    public int Year
    {
        get { return _Year; }
        set { _Year = value; }
    }

    public DateTime d_date
    {
        get { return _d_date; }
        set { _d_date = value; }
    }

    public double Calc_Days
    {
        get { return _Calc_Days; }
        set { _Calc_Days = value; }
    }

    public double Paid_Days
    {
        get { return _Paid_Days; }
        set { _Paid_Days = value; }
    }

    public double Present_Days
    {
        get { return _Present_Days; }
        set { _Present_Days = value; }
    }



    public double Absent_Days
    {
        get { return _Absent_Days; }
        set { _Absent_Days = value; }
    }

    public double TotLeave_Days
    {
        get { return _TotLeave_Days; }
        set { _TotLeave_Days = value; }
    }

    public double WeekOffDays
    {
        get { return _WeekOffDays; }
        set { _WeekOffDays = value; }
    }


    public double Holidays
    {
        get { return _Holidays; }
        set { _Holidays = value; }
    }

    public string Week_Holiday1
    {
        get { return _Week_Holiday1; }
        set { _Week_Holiday1 = value; }
    }

    public string Week_Holiday2
    {
        get { return _Week_Holiday2; }
        set { _Week_Holiday2 = value; }
    }

    public double OnDuty_days
    {
        get { return _OnDuty_days; }
        set { _OnDuty_days = value; }
    }

    public double Compoff_Days
    {
        get { return _Compoff_Days; }
        set { _Compoff_Days = value; }
    }

    public double Tour_Days
    {
        get { return _Tour_Days; }
        set { _Tour_Days = value; }
    }

    public string Att_Bonus
    {
        get { return _Att_Bonus; }
        set { _Att_Bonus = value; }
    }

    public double Att_BonusAmount
    {
        get { return _Att_BonusAmount; }
        set { _Att_BonusAmount = value; }
    }

    public double OT_HRS
    {
        get { return _OT_HRS; }
        set { _OT_HRS = value; }
    }

    public double OT_Amt
    {
        get { return _OT_Amt; }
        set { _OT_Amt = value; }
    }

    public int Count
    {
        get { return _Count; }
        set { _Count = value; }
    }

    public double Earn_Arrears
    {
        get { return _Earn_Arrears; }
        set { _Earn_Arrears = value; }
    }


    public double Ded_Arrears
    {
        get { return _Ded_Arrears; }
        set { _Ded_Arrears = value; }
    }

    public double OT_Days
    {
        get { return _OT_Days; }
        set { _OT_Days = value; }
    }

    public double EL_Days
    {
        get { return _EL_Days; }
        set { _EL_Days = value; }
    }


    private char _PT_Flag;

    public char PT_Flag
    {
        get { return _PT_Flag; }
        set { _PT_Flag = value; }
    }
    private char _LWF_Flag;

    public char LWF_Flag
    {
        get { return _LWF_Flag; }
        set { _LWF_Flag = value; }
    }
    private char _Bonus_Flag;

    public char Bonus_Flag
    {
        get { return _Bonus_Flag; }
        set { _Bonus_Flag = value; }
    }


    //Loan


    public int loanid
    {
        get { return _loanid; }
        set { _loanid = value; }
    }
    public string loanname
    {
        get { return _loanname; }
        set { _loanname = value; }
    }

    public string loancode
    {
        get { return _loancode; }
        set { _loancode = value; }
    }

    public int bankid
    {
        get { return _bankid; }
        set { _bankid = value; }
    }


    public string bankname
    {
        get { return _bankname; }
        set { _bankname = value; }
    }
    public string bankcode
    {
        get { return _bankcode; }
        set { _bankcode = value; }
    }

    public string Branch_Name
    {
        get { return _Branch_Name; }
        set { _Branch_Name = value; }
    }

    public string Account_Type
    {
        get { return _Account_Type; }
        set { _Account_Type = value; }
    }

    public string Micr_Code
    {
        get { return _Micr_Code; }
        set { _Micr_Code = value; }
    }

    public string Ifsc_Code
    {
        get { return _Ifsc_Code; }
        set { _Ifsc_Code = value; }
    }

    public string Address
    {
        get { return _Address; }
        set { _Address = value; }
    }

    public string others
    {
        get { return _Others; }
        set { _Others = value; }
    }


    public DateTime dateapplication
    {
        get { return _dateapplication; }
        set { _dateapplication = value; }
    }

    public DateTime effectivedate
    {
        get { return _effectivedate; }
        set { _effectivedate = value; }
    }



    public int installmentcount
    {
        get { return _installmentcount; }
        set { _installmentcount = value; }
    }

    public int loancloserid
    {
        get { return _loancloserid; }
        set { _loancloserid = value; }
    }

    public DateTime applicationdate
    {
        get { return _applicationdate; }
        set { _applicationdate = value; }
    }


    public int loanamount
    {
        get { return _loanamount; }
        set { _loanamount = value; }
    }

    public double balanceamount
    {
        get { return _balanceamount; }
        set { _balanceamount = value; }
    }


    public double paidamount
    {
        get { return _paidamount; }
        set { _paidamount = value; }
    }


    public double closureamount
    {
        get { return _closureamount; }
        set { _closureamount = value; }
    }


    public string checkno
    {
        get { return _checkno; }
        set { _checkno = value; }
    }


    public DateTime checkdate
    {
        get { return _checkdate; }
        set { _checkdate = value; }
    }


    public string checkamount
    {
        get { return _checkamount; }
        set { _checkamount = value; }
    }




    public string Remarks
    {
        get { return _Remarks; }
        set { _Remarks = value; }
    }



    public int holidayid
    {
        get { return _holidayid; }
        set { _holidayid = value; }
    }

    public DateTime holidaydate
    {
        get { return _holidaydate; }
        set { _holidaydate = value; }
    }

    public DateTime holidaytodate
    {
        get { return _holidaytodate; }
        set { _holidaytodate = value; }
    }

    public string holidayname
    {
        get { return _holidayname; }
        set { _holidayname = value; }
    }
    private string _Emp_Name;

    public string Empname
    {
        get { return _Emp_Name; }
        set { _Emp_Name = value; }
    }
    public char commonflag
    {
        get { return _Common_Flag; }
        set { _Common_Flag = value; }
    }



    public char Month_Calc
    {
        get { return _Month_Calc; }
        set { _Month_Calc = value; }
    }

    public string Month_Type
    {
        get { return _Month_Type; }
        set { _Month_Type = value; }
    }

    public int Manual_Days
    {
        get { return _Manual_Days; }
        set { _Manual_Days = value; }
    }

    public char OT_Flag
    {
        get { return _OT_Flag; }
        set { _OT_Flag = value; }
    }



    public double AdminCharges
    {
        get { return _Admin_Charges; }
        set { _Admin_Charges = value; }
    }


    public double EligibilityAmt
    {
        get { return _Eligibility_Amt; }
        set { _Eligibility_Amt = value; }
    }

    public char Round
    {
        get { return _Round; }
        set { _Round = value; }
    }

    public double ProTaxAmt
    {
        get { return _Pro_Tax_Amt; }
        set { _Pro_Tax_Amt = value; }
    }

    public double half_monthly
    {
        get { return _half_monthly; }
        set { _half_monthly = value; }
    }

    private char _payslip;
    public char payslip
    {
        get { return _payslip; }
        set { _payslip = value; }
    
    }
    private int _d_order;
    public int d_order
    {
        get { return _d_order; }
        set { _d_order = value; }
    }
    private string _FirstName;

    public string FirstName
    {
        get { return _FirstName; }
        set { _FirstName = value; }
    }
    private string _LastName;

    public string LastName
    {
        get { return _LastName; }
        set { _LastName = value; }
    }

    private int _ex_EmployeeID;

    public int ex_EmployeeID
    {
        get { return _ex_EmployeeID; }
        set { _ex_EmployeeID = value; }
    }

    private string _CategoryID;
    public string CategoryID
    {
        get { return _CategoryID; }
        set { _CategoryID = value; }
    }

    private int _DepartmentID;

    public int DepartmentID
    {
        get { return _DepartmentID; }
        set { _DepartmentID = value; }
    }

    private string _strdateapplication;

    public string strdateapplication
    {
        get { return _strdateapplication; }
        set { _strdateapplication = value; }
    }

    private string _streffectivedate;

    public string streffectivedate
    {
        get { return _streffectivedate; }
        set { _streffectivedate = value; }
    }

    private string _strapplicationdate;

    public string strapplicationdate
    {
        get { return _strapplicationdate; }
        set { _strapplicationdate = value; }
    }

    private string _strcheckdate;

    public string strcheckdate
    {
        get { return _strcheckdate; }
        set { _strcheckdate = value; }
    }

    private string _strDate;

    public string strDate
    {
        get { return _strDate; }
        set { _strDate = value; }
    }

    public string Str_From_date
    {
        get { return _Str_From_Date; }
        set { _Str_From_Date = value; }
    }

    public string Str_To_date
    {
        get { return _Str_To_Date; }
        set { _Str_To_Date = value; }
    }

    private string _emailid;
    private string _fathername;
    private string _Gender;
    private string _permanentAddress1;
    private string _Nomineename;
    private string _Relationship;
    private int _ID;

    public int ID
    {
        get { return _ID; }
        set { _ID = value; }
    }
    public string Relationship
    {
        get { return _Relationship; }
        set { _Relationship = value; }
    }

    public string Nomineename
    {
        get { return _Nomineename; }
        set { _Nomineename = value; }
    }
    public string PermanentAddress1
    {
        get { return _permanentAddress1; }
        set { _permanentAddress1 = value; }
    }
    private string _PhoneNo;

    public string PhoneNo
    {
        get { return _PhoneNo; }
        set { _PhoneNo = value; }
    }


    private string _permanentAddress2;

    public string PermanentAddress2
    {
        get { return _permanentAddress2; }
        set { _permanentAddress2 = value; }
    }


    private string _permanentCity;

    public string PermanentCity
    {
        get { return _permanentCity; }
        set { _permanentCity = value; }
    }


    private string _permanentState;

    public string PermanentState
    {
        get { return _permanentState; }
        set { _permanentState = value; }
    }


    private string _permanentDistrict;

    public string PermanentDistrict
    {
        get { return _permanentDistrict; }
        set { _permanentDistrict = value; }
    }


    private string _permanentPincode;

    public string PermanentPincode
    {
        get { return _permanentPincode; }
        set { _permanentPincode = value; }
    }
    private string _tempAddress1;

    public string TempAddress1
    {
        get { return _tempAddress1; }
        set { _tempAddress1 = value; }
    }

    private string _tempAddress2;

    public string TempAddress2
    {
        get { return _tempAddress2; }
        set { _tempAddress2 = value; }
    }

    private decimal _Pf_share;

    public decimal Pf_share
    {
        get { return _Pf_share; }
        set { _Pf_share = value; }
    }


    private string _tempCity;

    public string TempCity
    {
        get { return _tempCity; }
        set { _tempCity = value; }
    }


    private string _tempState;

    public string TempState
    {
        get { return _tempState; }
        set { _tempState = value; }
    }


    private string _tempDistrict;

    public string TempDistrict
    {
        get { return _tempDistrict; }
        set { _tempDistrict = value; }
    }


    private string _tempPincode;

    public string TempPincode
    {
        get { return _tempPincode; }
        set { _tempPincode = value; }
    }
    public string Gender
    {
        get { return _Gender; }
        set { _Gender = value; }
    }
    private string _AccNo;

    public string AccNo
    {
        get { return _AccNo; }
        set { _AccNo = value; }
    }
    public string Fathername
    {
        get { return _fathername; }
        set { _fathername = value; }
    }
    private string _mothername;

    public string Mothername
    {
        get { return _mothername; }
        set { _mothername = value; }
    }
    private DateTime _Dob;

    public DateTime Dob
    {
        get { return _Dob; }
        set { _Dob = value; }
    }
    public string Emailid
    {
        get { return _emailid; }
        set { _emailid = value; }
    }

    public string _leave_name { get; private set; }
    public int _leave_Id { get; private set; }
    public string _leave_code { get; private set; }


    //Loan procedures functions

    //************************Procedures Savings*******************************************





    public string fn_memberdetails(PayRoll e)
    {
        try
        {
            _Connection = Con.fn_Connection();

            SqlCommand _Cmd = new SqlCommand("sp_pf_Member_details", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[22];
            _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPParamter[0].Value = e.CompanyId;
            _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
            _ISPParamter[1].Value = e.EmployeeId;
            _ISPParamter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
            _ISPParamter[2].Value = e.d_date;
            _ISPParamter[3] = new SqlParameter("@emp_name", SqlDbType.VarChar);
            _ISPParamter[3].Value = e.d_FromDate;
            _ISPParamter[4] = new SqlParameter("@Father_Name", SqlDbType.VarChar);
            _ISPParamter[4].Value = e.Fathername;
            _ISPParamter[5] = new SqlParameter("@Dob", SqlDbType.DateTime);
            _ISPParamter[5].Value = e.Dob;
            _ISPParamter[6] = new SqlParameter("@Gender", SqlDbType.VarChar);
            _ISPParamter[6].Value = e.Gender;
            _ISPParamter[7] = new SqlParameter("@Account_No", SqlDbType.VarChar);
            _ISPParamter[7].Value = e.AccNo;
            _ISPParamter[8] = new SqlParameter("@Emailid", SqlDbType.VarChar);
            _ISPParamter[8].Value = e.Emailid;
            _ISPParamter[9] = new SqlParameter("@Phone_no", SqlDbType.VarChar);
            _ISPParamter[9].Value = e.PhoneNo;
            _ISPParamter[10] = new SqlParameter("@permanent_address1", SqlDbType.VarChar);
            _ISPParamter[10].Value = e.PermanentAddress1;
            _ISPParamter[11] = new SqlParameter("@permanent_address2", SqlDbType.VarChar);
            _ISPParamter[11].Value = e.PermanentAddress2;
            _ISPParamter[12] = new SqlParameter("@permanent_city", SqlDbType.VarChar);
            _ISPParamter[12].Value = e.PermanentCity;
            _ISPParamter[13] = new SqlParameter("@permanent_state", SqlDbType.VarChar);
            _ISPParamter[13].Value = e.PermanentState;
            _ISPParamter[14] = new SqlParameter("@permanent_district", SqlDbType.VarChar);
            _ISPParamter[14].Value = e.PermanentDistrict;
            _ISPParamter[15] = new SqlParameter("@permanent_pincode", SqlDbType.VarChar);
            _ISPParamter[15].Value = e.PermanentPincode;
            _ISPParamter[16] = new SqlParameter("@temp_address1 ", SqlDbType.VarChar);
            _ISPParamter[16].Value = e.TempAddress1;
            _ISPParamter[17] = new SqlParameter("@temp_address2 ", SqlDbType.VarChar);
            _ISPParamter[17].Value = e.TempAddress2;
            _ISPParamter[18] = new SqlParameter("@temp_city ", SqlDbType.VarChar);
            _ISPParamter[18].Value = e.TempCity;
            _ISPParamter[19] = new SqlParameter("@temp_state", SqlDbType.VarChar);
            _ISPParamter[19].Value = e.TempState;
            _ISPParamter[20] = new SqlParameter("@temp_district", SqlDbType.VarChar);
            _ISPParamter[20].Value = e.TempDistrict;
            _ISPParamter[21] = new SqlParameter("@temp_pincode ", SqlDbType.VarChar);
            _ISPParamter[21].Value = e.TempPincode;


            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
            return "0";
        }
        catch (SqlException Ex)
        {
            return "1";
        }
    }



    public string finalsettlement(PayRoll pay)
    {
        try
        {
            _Connection = Con.fn_Connection();
            SqlCommand _Cmd = new SqlCommand("sp_final_settlement", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[14];
            _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPParamter[0].Value = pay.CompanyId;
            _ISPParamter[1] = new SqlParameter("@pn_branchid", SqlDbType.Int);
            _ISPParamter[1].Value = pay.BranchId;
            _ISPParamter[2] = new SqlParameter("@pn_employeeid", SqlDbType.Int);
            _ISPParamter[2].Value = pay.EmployeeId;
            _ISPParamter[3] = new SqlParameter("@Joining_date", SqlDbType.VarChar);
            _ISPParamter[3].Value = pay.Joining_date;
            //_ISPParamter[4] = new SqlParameter("@retirement_date", SqlDbType.VarChar);
            //_ISPParamter[4].Value = pay.Retire_date;
            _ISPParamter[4] = new SqlParameter("@last_working_date", SqlDbType.VarChar);
            _ISPParamter[4].Value = pay.Last_working_date;
            _ISPParamter[5] = new SqlParameter("@serviceyear", SqlDbType.Int);
            _ISPParamter[5].Value = pay.Year;
            _ISPParamter[6] = new SqlParameter("@grauity_amount", SqlDbType.Decimal);
            _ISPParamter[6].Value = pay.Grauity;
            _ISPParamter[7] = new SqlParameter("@pf_amount", SqlDbType.Decimal);
            _ISPParamter[7].Value = pay.Pf_share;
            _ISPParamter[8] = new SqlParameter("@encashment_amount", SqlDbType.Decimal);
            _ISPParamter[8].Value = pay.Encashment_amt;
            _ISPParamter[9] = new SqlParameter("@loan_amount", SqlDbType.Decimal);
            _ISPParamter[9].Value = pay.loan_amt;
            _ISPParamter[10] = new SqlParameter("@deduct_salary_amount", SqlDbType.Decimal);
            _ISPParamter[10].Value = pay.Deduct_salary_amt;
            _ISPParamter[11] = new SqlParameter("@final_amount", SqlDbType.Decimal);
            _ISPParamter[11].Value = pay.Final_amt;
            _ISPParamter[12] = new SqlParameter("@status", SqlDbType.VarChar);
            _ISPParamter[12].Value = pay.Status;
            _ISPParamter[13] = new SqlParameter("@ReferenceNo", SqlDbType.VarChar);
            _ISPParamter[13].Value = pay.Refno;

            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
            return "0";
        }
        catch (SqlException Ex)
        {
            return "1";
        }
    }




    public Collection<PayRoll> fn_final_settlement(PayRoll r)
    {
        Collection<PayRoll> finalsettlementlist = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString = "select a.*,c.Employee_First_Name, c.pn_employeeID from payroll_final_settlement a,Paym_employee c where a.Pn_EmployeeId=c.Pn_EmployeeID  and a.pn_branchID='" + r.BranchId + "' and a.Pn_CompanyID='" + r.CompanyId + "'";
        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_settlement = _SSDesignation.ExecuteReader();
        while (dr_settlement.Read())
        {
            PayRoll pay = new PayRoll();
            pay.EmployeeName = (int)dr_settlement["pn_employeeID"] + "-" + (string)dr_settlement["Employee_First_Name"];
            pay.Joining_date = Convert.IsDBNull(dr_settlement["joining_date"]) ? "" : (string)dr_settlement["joining_date"];
            // pay.Retire_date = Convert.IsDBNull(dr_settlement["retirement_date"]) ? "" : (string)dr_settlement["retirement_date"];
            pay.Last_working_date = Convert.IsDBNull(dr_settlement["last_working_date"]) ? "" : (string)dr_settlement["last_working_date"];
            pay.Year = (int)dr_settlement["serviceyear"];
            pay.Grauity = Convert.ToDecimal(dr_settlement["grauity_amount"]);
            pay.Pf_share = Convert.ToDecimal(dr_settlement["pf_amount"]);
            pay.Encashment_amt = Convert.ToDecimal(dr_settlement["encashment_amount"]);
            pay.loan_amt = Convert.ToDecimal(dr_settlement["loan_amount"]);
            pay.Deduct_salary_amt = Convert.ToDecimal(dr_settlement["deduct_salary_amount"]);
            pay.Final_amt = Convert.ToDecimal(dr_settlement["final_amount"]);
            pay.Status = Convert.IsDBNull(dr_settlement["status"]) ? "" : (string)dr_settlement["status"];

            finalsettlementlist.Add(pay);
        }
        _Connection.Close();
        return finalsettlementlist;
    }


    public string fn_memberdetails1(PayRoll e)
    {
        try
        {
            _Connection = Con.fn_Connection();
            SqlCommand _Cmd = new SqlCommand("sp_pf_Member_details", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[22];
            _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPParamter[0].Value = e.CompanyId;
            _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
            _ISPParamter[1].Value = e.BranchId;
            _ISPParamter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
            _ISPParamter[2].Value = e.EmployeeId;
            _ISPParamter[3] = new SqlParameter("@emp_name", SqlDbType.VarChar);
            _ISPParamter[3].Value = e.EmployeeName;
            _ISPParamter[4] = new SqlParameter("@Father_Name", SqlDbType.VarChar);
            _ISPParamter[4].Value = e.Fathername;
            _ISPParamter[5] = new SqlParameter("@Dob", SqlDbType.DateTime);
            _ISPParamter[5].Value = e.Dob;
            _ISPParamter[6] = new SqlParameter("@Gender", SqlDbType.VarChar);
            _ISPParamter[6].Value = e.Gender;
            _ISPParamter[7] = new SqlParameter("@Account_No", SqlDbType.VarChar);
            _ISPParamter[7].Value = e.AccNo;
            _ISPParamter[8] = new SqlParameter("@Emailid", SqlDbType.VarChar);
            _ISPParamter[8].Value = e.Emailid;
            _ISPParamter[9] = new SqlParameter("@Phone_no", SqlDbType.VarChar);
            _ISPParamter[9].Value = e.PhoneNo;
            _ISPParamter[10] = new SqlParameter("@permanent_address1", SqlDbType.VarChar);
            _ISPParamter[10].Value = e.PermanentAddress1;
            _ISPParamter[11] = new SqlParameter("@permanent_address2", SqlDbType.VarChar);
            _ISPParamter[11].Value = e.PermanentAddress2;
            _ISPParamter[12] = new SqlParameter("@permanent_city", SqlDbType.VarChar);
            _ISPParamter[12].Value = e.PermanentCity;
            _ISPParamter[13] = new SqlParameter("@permanent_state", SqlDbType.VarChar);
            _ISPParamter[13].Value = e.PermanentState;
            _ISPParamter[14] = new SqlParameter("@permanent_district", SqlDbType.VarChar);
            _ISPParamter[14].Value = e.PermanentDistrict;
            _ISPParamter[15] = new SqlParameter("@permanent_pincode", SqlDbType.VarChar);
            _ISPParamter[15].Value = e.PermanentPincode;
            _ISPParamter[16] = new SqlParameter("@temp_address1 ", SqlDbType.VarChar);
            _ISPParamter[16].Value = e.PermanentAddress1;
            _ISPParamter[17] = new SqlParameter("@temp_address2 ", SqlDbType.VarChar);
            _ISPParamter[17].Value = e.PermanentAddress2;
            _ISPParamter[18] = new SqlParameter("@temp_city ", SqlDbType.VarChar);
            _ISPParamter[18].Value = e.PermanentCity;
            _ISPParamter[19] = new SqlParameter("@temp_state", SqlDbType.VarChar);
            _ISPParamter[19].Value = e.PermanentState;
            _ISPParamter[20] = new SqlParameter("@temp_district", SqlDbType.VarChar);
            _ISPParamter[20].Value = e.PermanentDistrict;
            _ISPParamter[21] = new SqlParameter("@temp_pincode ", SqlDbType.VarChar);
            _ISPParamter[21].Value = e.PermanentPincode;

            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
            return "0";
        }
        catch (SqlException Ex)
        {
            return "1";
        }
    }


    public string fn_epsdetails(PayRoll e)
    {
        try
        {
            _Connection = Con.fn_Connection();

            SqlCommand _Cmd = new SqlCommand("sp_PF_EPS", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[13];
            _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPParamter[0].Value = e.CompanyId;
            _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
            _ISPParamter[1].Value = e.BranchId;
            _ISPParamter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
            _ISPParamter[2].Value = e.EmployeeId;
            _ISPParamter[3] = new SqlParameter("@FamilyMember_Name", SqlDbType.VarChar);
            _ISPParamter[3].Value = e.Nomineename;
            _ISPParamter[4] = new SqlParameter("@Gender", SqlDbType.VarChar);
            _ISPParamter[4].Value = e.Gender;
            _ISPParamter[5] = new SqlParameter("@Relationship", SqlDbType.VarChar);
            _ISPParamter[5].Value = e.Relationship;
            _ISPParamter[6] = new SqlParameter("@Dob", SqlDbType.DateTime);
            _ISPParamter[6].Value = e.Dob;
            _ISPParamter[7] = new SqlParameter("@address1", SqlDbType.VarChar);
            _ISPParamter[7].Value = e.PermanentAddress1;
            _ISPParamter[8] = new SqlParameter("@State", SqlDbType.VarChar);
            _ISPParamter[8].Value = e.PermanentState;
            _ISPParamter[9] = new SqlParameter("@district", SqlDbType.VarChar);
            _ISPParamter[9].Value = e.PermanentDistrict;
            _ISPParamter[10] = new SqlParameter("@city", SqlDbType.VarChar);
            _ISPParamter[10].Value = e.PermanentCity;
            _ISPParamter[11] = new SqlParameter("@pin_no", SqlDbType.VarChar);
            _ISPParamter[11].Value = e.PermanentPincode;

            _ISPParamter[12] = new SqlParameter("@Id", SqlDbType.Int);
            _ISPParamter[12].Value = e.ID;






            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
            return "0";
        }
        catch (SqlException Ex)
        {
            return "1";
        }
    }




    public Collection<PayRoll> Re_EPF(PayRoll F)
    {
        Collection<PayRoll> EPF_List = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _Sqlcmd = "select * from PF_EPF where pn_employeeid='" + F.EmployeeId + "'";
        SqlCommand cmd = new SqlCommand(_Sqlcmd, _Connection);
        _Connection.Open();
        SqlDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            PayRoll EPF = new PayRoll();
            EPF.ID = (int)rdr["id"];
            EPF.EmployeeId = (int)rdr["pn_employeeID"];
            EPF.Nomineename = Convert.IsDBNull(rdr["Nominee_Name"]) ? "" : (string)rdr["Nominee_Name"];
            EPF.Gender = Convert.IsDBNull(rdr["Gender"]) ? "" : (string)rdr["Gender"];
            EPF.Dob = (DateTime)(rdr["DOB"]);
            EPF.Pf_share = Convert.ToDecimal(rdr["PF_Share"]);
            EPF.Relationship = Convert.IsDBNull(rdr["Relationship"]) ? "" : (string)rdr["Relationship"];
            EPF.PermanentAddress1 = Convert.IsDBNull(rdr["address1"]) ? "" : (string)rdr["address1"];
            // EPF.PermanentAddress2 = Convert.IsDBNull(rdr["address2"]) ? "" : (string)rdr["address2"];
            EPF.PermanentState = Convert.IsDBNull(rdr["State"]) ? "" : (string)rdr["State"];
            EPF.PermanentDistrict = Convert.IsDBNull(rdr["District"]) ? "" : (string)rdr["District"];
            EPF.PermanentCity = Convert.IsDBNull(rdr["city"]) ? "" : (string)rdr["city"];
            EPF.PermanentPincode = rdr["pin_no"].ToString();
            EPF_List.Add(EPF);

        }
        _Connection.Close();
        return EPF_List;
    }





    public string EPF(PayRoll EPF)
    {
        try
        {
            _Connection = Con.fn_Connection();
            SqlCommand _EPF = new SqlCommand("sp_PF_EPF", _Connection);
            _EPF.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _EPFparam = new SqlParameter[14];
            _EPFparam[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _EPFparam[0].Value = EPF.CompanyId;
            _EPFparam[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
            _EPFparam[1].Value = EPF.BranchId;
            _EPFparam[2] = new SqlParameter("@pn_employeeID", SqlDbType.Int);
            _EPFparam[2].Value = EPF.EmployeeId;
            _EPFparam[3] = new SqlParameter("@Nominee_Name", SqlDbType.VarChar);
            _EPFparam[3].Value = EPF.Nomineename;
            _EPFparam[4] = new SqlParameter("@Gender", SqlDbType.VarChar);
            _EPFparam[4].Value = EPF.Gender;
            _EPFparam[5] = new SqlParameter("@DOB", SqlDbType.DateTime);
            _EPFparam[5].Value = EPF.Dob;
            _EPFparam[6] = new SqlParameter("@PF_Share", SqlDbType.Decimal);
            _EPFparam[6].Value = EPF.Pf_share;
            _EPFparam[7] = new SqlParameter("@Relationship", SqlDbType.VarChar);
            _EPFparam[7].Value = EPF.Relationship;
            _EPFparam[8] = new SqlParameter("@address1", SqlDbType.VarChar);
            _EPFparam[8].Value = EPF.PermanentAddress1;
            _EPFparam[9] = new SqlParameter("@State", SqlDbType.VarChar);
            _EPFparam[9].Value = EPF.PermanentState;
            _EPFparam[10] = new SqlParameter("@District", SqlDbType.VarChar);
            _EPFparam[10].Value = EPF.PermanentDistrict;
            _EPFparam[11] = new SqlParameter("@city", SqlDbType.VarChar);
            _EPFparam[11].Value = EPF.PermanentCity;
            _EPFparam[12] = new SqlParameter("@pin_no", SqlDbType.Int);
            _EPFparam[12].Value = EPF.PermanentPincode;
            _EPFparam[13] = new SqlParameter("@Id", SqlDbType.VarChar);
            _EPFparam[13].Value = EPF.ID;

            for (int i = 0; i < _EPFparam.Length; i++)
            {

                _EPF.Parameters.Add(_EPFparam[i]);
            }
            _Connection.Open();
            _EPF.ExecuteNonQuery();
            _Connection.Close();

            return "0";
        }
        catch (Exception ex)
        {
            return "1";
        }
    }





    public Collection<PayRoll> epsload(PayRoll r)
    {
        Collection<PayRoll> EpsList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString = "select * from PF_EPS where pn_employeeid='" + r.EmployeeId + "'";
        SqlCommand cmd = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_recruit = cmd.ExecuteReader();
        while (dr_recruit.Read())
        {
            PayRoll pay = new PayRoll();
            pay.EmployeeId = (int)dr_recruit["pn_employeeid"];
            pay.ID = (int)dr_recruit["id"];
            pay.Nomineename = Convert.IsDBNull(dr_recruit["familymember_name"]) ? "" : (string)dr_recruit["familymember_name"]; //(string)dr_recruit["familymember_name"] + "-" + (string)dr_recruit["familymember_name"];
            pay.Gender = Convert.IsDBNull(dr_recruit["gender"]) ? "" : (string)dr_recruit["gender"];
            pay.Relationship = Convert.IsDBNull(dr_recruit["relationship"]) ? "" : (string)dr_recruit["relationship"];
            pay.Dob = (DateTime)dr_recruit["Dob"];// Convert.IsDBNull(dr_recruit["Dob"]) ? "" : (string)dr_recruit["Dob"];
            pay.PermanentAddress1 = Convert.IsDBNull(dr_recruit["address1"]) ? "" : (string)dr_recruit["address1"];
            // pay.PermanentAddress2 = Convert.IsDBNull(dr_recruit["address2"]) ? "" : (string)dr_recruit["address2"];
            pay.PermanentState = Convert.IsDBNull(dr_recruit["state"]) ? "" : (string)dr_recruit["state"];
            pay.PermanentDistrict = Convert.IsDBNull(dr_recruit["district"]) ? "" : (string)dr_recruit["district"];
            pay.PermanentCity = Convert.IsDBNull(dr_recruit["city"]) ? "" : (string)dr_recruit["city"];
            pay.PermanentPincode = Convert.IsDBNull(dr_recruit["pin_no"]) ? "" : (string)dr_recruit["pin_no"];
            EpsList.Add(pay);
        }
        _Connection.Close();
        return EpsList;
    }

    public void Earn_Deduct_Insert(PayRoll e)
    {
        string[] a = new string[10];
        string[] d = new string[10];
        int l = 0;
        _Connection = Con.fn_Connection();
        _Connection.Open();
        SqlCommand _Cmd = new SqlCommand("Select v_EarningsCode from paym_earnings where pn_BranchID='" + e.BranchId + "' and pn_CompanyID = '" + e.CompanyId + "' order by d_order asc", _Connection);
        SqlDataReader _rd_ed = _Cmd.ExecuteReader();
        DataTable dt_earn = new DataTable();
        dt_earn.Load(_rd_ed);

        if (dt_earn.Rows.Count > 0)
        {
            for (l = 0; l < 10; l++)
            {
                try
                {
                    a[l] = dt_earn.Rows[l][0].ToString();
                }
                catch
                {
                    a[l] = "";
                }
            }
        }
        else
        {
            for (l = 0; l < 10; l++)
            {
                a[l] = "";
            }
        }

        _Cmd = new SqlCommand("Select v_DeductionCode from paym_deduction where pn_BranchID='" + e.BranchId + "' and pn_CompanyID = '" + e.CompanyId + "' order by d_order asc", _Connection);
        _rd_ed = _Cmd.ExecuteReader();
        DataTable dt_ded = new DataTable();
        dt_ded.Load(_rd_ed);
        if (dt_ded.Rows.Count > 0)
        {
            for (l = 0; l < 10; l++)
            {
                try
                {
                    d[l] = dt_ded.Rows[l][0].ToString();
                }
                catch
                {
                    d[l] = "";
                }
            }
        }
        else
        {
            for (l = 0; l < 10; l++)
            {
                d[l] = "";
            }
        }

        _Cmd = new SqlCommand("insert into earn_deduct(pn_CompanyID,pn_BranchID,pn_EmployeeID,Allowance1,Allowance2,Allowance3,Allowance4,Allowance5,Allowance6,Allowance7,Allowance8,Allowance9,Allowance10,Deduction1,Deduction2,Deduction3,Deduction4,Deduction5,Deduction6,Deduction7,Deduction8,Deduction9,Deduction10,d_date,d_From_Date,d_To_Date) values('" + e.CompanyId + "','" + e.BranchId + "','" + e.EmployeeId + "','" + a[0] + "','" + a[1] + "','" + a[2] + "','" + a[3] + "','" + a[4] + "','" + a[5] + "','" + a[6] + "','" + a[7] + "','" + a[8] + "','" + a[9] + "','" + d[0] + "','" + d[1] + "','" + d[2] + "','" + d[3] + "','" + d[4] + "','" + d[5] + "','" + d[6] + "','" + d[7] + "','" + d[8] + "','" + d[9] + "','" + e.strDate + "','" + e.Str_From_date + "','" + e.Str_To_date + "')", _Connection);
        _Cmd.ExecuteNonQuery();
        _Connection.Close();
    }

    public void PayProcess(PayRoll e)
    {
        _Connection = Con.fn_Connection();
        _Connection.Open();
        SqlCommand _Cmd = new SqlCommand("insert into PayProcess(pn_CompanyID,pn_BranchID,d_date,d_From_Date,d_To_Date,Salary_Period,ProcessDate) values('" + e.CompanyId + "','" + e.BranchId + "','" + e.strDate + "','" + e.Str_From_date + "','" + e.Str_To_date + "','"+e.period_code+"','"+DateTime.Now.ToString("MM/dd/yyyy")+"')", _Connection);
        _Cmd.ExecuteNonQuery();
        _Connection.Close();
    }

    public void Earnings_update_register(PayRoll e)
    {
        try
        {
            _Connection = Con.fn_Connection();
            _Connection.Open();
            int c = 1;
            SqlCommand _Cmd = new SqlCommand("Select d_order from paym_earnings where pn_CompanyID='" + e.CompanyId + "' and pn_branchID='" + e.BranchId + "' and v_EarningsCode = '" + e.EarningsCode + "'", _Connection);
            SqlDataReader _Rea = _Cmd.ExecuteReader();
            if (_Rea.Read())
            {
                c = Convert.ToInt32(_Rea[0]);
            }
            _Cmd = new SqlCommand("update earn_deduct set value" + c + " ='" + e.Earn_Amount + "' where pn_CompanyID='" + e.CompanyId + "' and pn_BranchID = '" + e.BranchId + "' and pn_EmployeeID='" + e.EmployeeId + "' and d_date='" + e.strDate + "'", _Connection);
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
        }
        catch(Exception ex)
        {

        }
    }

    public void Deduction_update_register(PayRoll e)
    {
        try
        {
            _Connection = Con.fn_Connection();
            _Connection.Open();
            int c = 1;
            SqlCommand _Cmd = new SqlCommand("Select d_order from paym_deduction where pn_CompanyID='" + e.CompanyId + "' and pn_branchID='" + e.BranchId + "' and v_DeductionCode = '" + e.DeducationCode + "'", _Connection);
            SqlDataReader _Rea = _Cmd.ExecuteReader();
            if (_Rea.Read())
            {
                c = Convert.ToInt32(_Rea[0]);
            }
            _Cmd = new SqlCommand("update earn_deduct set valueA" + c + " ='" + e.Ded_Amount + "' where pn_CompanyID='" + e.CompanyId + "' and pn_BranchID = '" + e.BranchId + "' and pn_EmployeeID='" + e.EmployeeId + "' and d_date='" + e.strDate + "'", _Connection);
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
        }
        catch(Exception ex)
        {

        }
    }

    public string EarningsUpdate(PayRoll pay)
    {
        try
        {
            _Connection = Con.fn_Connection();
            SqlCommand _Cmd = new SqlCommand("sp_paym_Earnings", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[14];
            _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPParamter[0].Value = pay.CompanyId;
            _ISPParamter[1] = new SqlParameter("@pn_EarningsId", SqlDbType.Int);
            _ISPParamter[1].Value = pay.EarningsId;
            _ISPParamter[2] = new SqlParameter("@v_EarningsCode", SqlDbType.VarChar);
            _ISPParamter[2].Value = pay.EarningsCode;
            _ISPParamter[3] = new SqlParameter("@v_EarningsName", SqlDbType.VarChar);
            _ISPParamter[3].Value = pay.EarningsName;
            _ISPParamter[4] = new SqlParameter("@c_Regular", SqlDbType.Char);
            _ISPParamter[4].Value = pay.regular;
            _ISPParamter[5] = new SqlParameter("@c_PF", SqlDbType.Char);
            _ISPParamter[5].Value = pay.Pf;
            _ISPParamter[6] = new SqlParameter("@c_ESI", SqlDbType.Char);
            _ISPParamter[6].Value = pay.Esi;
            _ISPParamter[7] = new SqlParameter("@c_OT", SqlDbType.Char);
            _ISPParamter[7].Value = pay.OT;
            _ISPParamter[8] = new SqlParameter("@c_LOP", SqlDbType.Char);
            _ISPParamter[8].Value = pay.LOP;
            _ISPParamter[9] = new SqlParameter("@c_PT", SqlDbType.Char);
            _ISPParamter[9].Value = pay.PT;
            _ISPParamter[10] = new SqlParameter("@c_Print", SqlDbType.Char);
            _ISPParamter[10].Value = pay.Print;
            _ISPParamter[11] = new SqlParameter("@payslip", SqlDbType.Char);
            _ISPParamter[11].Value = pay.payslip;
            _ISPParamter[12] = new SqlParameter("@status", SqlDbType.VarChar);
            _ISPParamter[12].Value = pay.status;
            _ISPParamter[13] = new SqlParameter("@d_order", SqlDbType.Char);
            _ISPParamter[13].Value = pay.d_order;

            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
            return "0";
        }
        catch (SqlException Ex)
        {
            return "1";
        }
    }

    public string DeductionUpdate(PayRoll pay)
    {
        try
        {
            _Connection = Con.fn_Connection();
            SqlCommand _Cmd = new SqlCommand("sp_paym_Deduction", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[8];
            _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPParamter[0].Value = pay.CompanyId;
            _ISPParamter[1] = new SqlParameter("@pn_DeductionID", SqlDbType.Int);
            _ISPParamter[1].Value = pay.DeductionId;
            _ISPParamter[2] = new SqlParameter("@v_DeductionCode", SqlDbType.VarChar);
            _ISPParamter[2].Value = pay.DeducationCode;
            _ISPParamter[3] = new SqlParameter("@v_DeductionName", SqlDbType.VarChar);
            _ISPParamter[3].Value = pay.DeductionName;
            _ISPParamter[4] = new SqlParameter("@c_Regular", SqlDbType.Char);
            _ISPParamter[4].Value = pay.regular;
            _ISPParamter[5] = new SqlParameter("@d_order", SqlDbType.Int);
            _ISPParamter[5].Value = pay.d_order;
            _ISPParamter[6] = new SqlParameter("@status", SqlDbType.VarChar);
            _ISPParamter[6].Value = pay.status;
            _ISPParamter[7] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
            _ISPParamter[7].Value = pay.BranchId;
            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
            return "0";
        }
        catch (SqlException Ex)
        {
            return "1";
        }
    }

    public string Earnings(PayRoll e)
    {
        try
        {
            _Connection = Con.fn_Connection();

            SqlCommand _Cmd = new SqlCommand("sp_Earnings", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[3];
            _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPParamter[0].Value = e.CompanyId;
            _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
            _ISPParamter[1].Value = e.BranchId;
            _ISPParamter[2] = new SqlParameter("@fn_EarningsID", SqlDbType.Int);
            _ISPParamter[2].Value = e.EarningsId;

            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
            return "0";
        }
        catch (SqlException Ex)
        {
            return "1";
        }
    }

    public string Deduction(PayRoll e)
    {
        try
        {
            _Connection = Con.fn_Connection();

            SqlCommand _Cmd = new SqlCommand("sp_Deduction", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[3];
            _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPParamter[0].Value = e.CompanyId;
            _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
            _ISPParamter[1].Value = e.BranchId;
            _ISPParamter[2] = new SqlParameter("@fn_DeductionID", SqlDbType.Int);
            _ISPParamter[2].Value = e.DeductionId;

            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
            return "0";
        }
        catch (SqlException Ex)
        {
            return "1";
        }
    }

    public string Emp_Earnings(PayRoll e)
    {
        try
        {
            _Connection = Con.fn_Connection();

            SqlCommand _Cmd = new SqlCommand("sp_paym_Emp_Earnings1", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[10];
            _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPParamter[0].Value = e.CompanyId;
            _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
            _ISPParamter[1].Value = e.BranchId;
            _ISPParamter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
            _ISPParamter[2].Value = e.EmployeeId;
            _ISPParamter[3] = new SqlParameter("@pn_EarningsID", SqlDbType.Int);
            _ISPParamter[3].Value = e.EarningsId;
            _ISPParamter[4] = new SqlParameter("@n_Amount", SqlDbType.Int);
            _ISPParamter[4].Value = e.Amount;
            _ISPParamter[5] = new SqlParameter("@c_eligible", SqlDbType.Char);
            _ISPParamter[5].Value = e.regular;
            _ISPParamter[6] = new SqlParameter("@d_Date", SqlDbType.DateTime);
            _ISPParamter[6].Value = e.d_date;
            _ISPParamter[7] = new SqlParameter("@from_date", SqlDbType.DateTime);
            _ISPParamter[7].Value = e.fromdate;
            _ISPParamter[8] = new SqlParameter("@to_date", SqlDbType.DateTime);
            _ISPParamter[8].Value = e.todate;
            _ISPParamter[9] = new SqlParameter("@Flag", SqlDbType.Char);
            _ISPParamter[9].Value = 'N';
            

            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
            return "0";
        }
        catch (SqlException Ex)
        {
            return "1";
        }
    }

    public string Emp_Deduction(PayRoll e)
    {
        try
        {
            _Connection = Con.fn_Connection();
            SqlCommand _Cmd = new SqlCommand("sp_paym_Emp_Deduction1", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[9];
            _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPParamter[0].Value = e.CompanyId;
            _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
            _ISPParamter[1].Value = e.BranchId;
            _ISPParamter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
            _ISPParamter[2].Value = e.EmployeeId;
            _ISPParamter[3] = new SqlParameter("@pn_DeductionID", SqlDbType.Int);
            _ISPParamter[3].Value = e.DeductionId;
            _ISPParamter[4] = new SqlParameter("@n_Amount", SqlDbType.Float);
            _ISPParamter[4].Value = e.Amount;
            _ISPParamter[5] = new SqlParameter("@c_eligible", SqlDbType.Char);
            _ISPParamter[5].Value = e.regular;
            _ISPParamter[6] = new SqlParameter("@d_Date", SqlDbType.DateTime);
            _ISPParamter[6].Value = e.d_date;
            _ISPParamter[7] = new SqlParameter("@from_date", SqlDbType.DateTime);
            _ISPParamter[7].Value = e.fromdate;
            _ISPParamter[8] = new SqlParameter("@to_date", SqlDbType.DateTime);
            _ISPParamter[8].Value = e.todate;

            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
            return "0";
        }
        catch (SqlException Ex)
        {
            return "1";
        }
    }

    public string Emp_Basic(PayRoll e)
    {
        try
        {
            _Connection = Con.fn_Connection();
            SqlCommand _Cmd = new SqlCommand("sp_paym_Emp_Basic", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[4];
            _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPParamter[0].Value = e.CompanyId;
            _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
            _ISPParamter[1].Value = e.BranchId;
            _ISPParamter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
            _ISPParamter[2].Value = e.EmployeeId;
            _ISPParamter[3] = new SqlParameter("@n_Amount", SqlDbType.Float);
            _ISPParamter[3].Value = e.Amount;

            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
            return "0";
        }
        catch (SqlException Ex)
        {
            return "1";
        }
    }


    public void PayOutput_Earnings(PayRoll e)
    {
        try
        {
            _Connection = Con.fn_Connection();

            SqlCommand _Cmd = new SqlCommand("sp_PayOutput_Earnings", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[12];
            _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPParamter[0].Value = e.CompanyId;
            _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
            _ISPParamter[1].Value = e.BranchId;
            _ISPParamter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
            _ISPParamter[2].Value = e.EmployeeId;
            _ISPParamter[3] = new SqlParameter("@pn_EarningsID", SqlDbType.Int);
            _ISPParamter[3].Value = e.EarningsId;
            _ISPParamter[4] = new SqlParameter("@pn_DepartmentName", SqlDbType.VarChar);
            _ISPParamter[4].Value = e.Empname;
            _ISPParamter[5] = new SqlParameter("@d_Date", SqlDbType.DateTime);
            _ISPParamter[5].Value = e.d_date;
            _ISPParamter[6] = new SqlParameter("@d_From_Date", SqlDbType.DateTime);
            _ISPParamter[6].Value = e.d_FromDate;
            _ISPParamter[7] = new SqlParameter("@d_To_Date", SqlDbType.DateTime);
            _ISPParamter[7].Value = e.d_ToDate;
            _ISPParamter[8] = new SqlParameter("@Flag", SqlDbType.Char);
            _ISPParamter[8].Value = e.status;
            _ISPParamter[9] = new SqlParameter("@Mode", SqlDbType.Char);
            _ISPParamter[9].Value = e.pay_mode;
            _ISPParamter[10] = new SqlParameter("@Act_Amount", SqlDbType.Float);
            _ISPParamter[10].Value = e.Act_Amount;
            _ISPParamter[11] = new SqlParameter("@Amount", SqlDbType.Float);
            _ISPParamter[11].Value = e.Earn_Amount;
            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
        }
        catch (Exception ex)
        {

        }

    }

    public void PayOutput_Earnings_Mode(PayRoll e)
    {
        try
        {
            _Connection = Con.fn_Connection();

            SqlCommand _Cmd = new SqlCommand("sp_PayOutput_Earnings_Mode", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[12];
            _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPParamter[0].Value = e.CompanyId;
            _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
            _ISPParamter[1].Value = e.BranchId;
            _ISPParamter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
            _ISPParamter[2].Value = e.EmployeeId;
            _ISPParamter[3] = new SqlParameter("@pn_EarningsID", SqlDbType.Int);
            _ISPParamter[3].Value = e.EarningsId;
            _ISPParamter[4] = new SqlParameter("@pn_DepartmentName", SqlDbType.VarChar);
            _ISPParamter[4].Value = e.Empname;
            _ISPParamter[5] = new SqlParameter("@d_Date", SqlDbType.DateTime);
            _ISPParamter[5].Value = e.d_date;
            _ISPParamter[6] = new SqlParameter("@d_From_Date", SqlDbType.DateTime);
            _ISPParamter[6].Value = e.d_FromDate;
            _ISPParamter[7] = new SqlParameter("@d_To_Date", SqlDbType.DateTime);
            _ISPParamter[7].Value = e.d_ToDate;
            _ISPParamter[8] = new SqlParameter("@Flag", SqlDbType.Char);
            _ISPParamter[8].Value = e.status;
            _ISPParamter[9] = new SqlParameter("@Mode", SqlDbType.Char);
            _ISPParamter[9].Value = e.pay_mode;
            _ISPParamter[10] = new SqlParameter("@Act_Amount", SqlDbType.Float);
            _ISPParamter[10].Value = e.Act_Amount;
            _ISPParamter[11] = new SqlParameter("@Amount", SqlDbType.Float);
            _ISPParamter[11].Value = e.Earn_Amount;
            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
        }
        catch (Exception ex)
        {

        }

    }

    public void PayOutput_Deductions(PayRoll e)
    {
        try
        {
            _Connection = Con.fn_Connection();
            SqlCommand _Cmd = new SqlCommand("sp_PayOutput_Deductions", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[12];
            _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPParamter[0].Value = e.CompanyId;
            _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
            _ISPParamter[1].Value = e.BranchId;
            _ISPParamter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
            _ISPParamter[2].Value = e.EmployeeId;
            _ISPParamter[3] = new SqlParameter("@pn_DeductionID", SqlDbType.Int);
            _ISPParamter[3].Value = e.DeductionId;
            _ISPParamter[4] = new SqlParameter("@pn_DepartmentName", SqlDbType.VarChar);
            _ISPParamter[4].Value = e.Empname;
            _ISPParamter[5] = new SqlParameter("@d_Date", SqlDbType.DateTime);
            _ISPParamter[5].Value = e.d_date;
            _ISPParamter[6] = new SqlParameter("@d_From_Date", SqlDbType.DateTime);
            _ISPParamter[6].Value = e.d_FromDate;
            _ISPParamter[7] = new SqlParameter("@d_To_Date", SqlDbType.DateTime);
            _ISPParamter[7].Value = e.d_ToDate;
            _ISPParamter[8] = new SqlParameter("@Flag", SqlDbType.Char);
            _ISPParamter[8].Value = e.status;
            _ISPParamter[9] = new SqlParameter("@Mode", SqlDbType.Char);
            _ISPParamter[9].Value = e.pay_mode;
            _ISPParamter[10] = new SqlParameter("@Act_Amount", SqlDbType.Float);
            _ISPParamter[10].Value = e.Act_Amount;
            _ISPParamter[11] = new SqlParameter("@Amount", SqlDbType.Float);
            _ISPParamter[11].Value = e.Ded_Amount;

            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
        }
        catch (Exception ex)
        {

        }

    }

    public void PayOutput_ESI(PayRoll e)
    {
        try
        {
            _Connection = Con.fn_Connection();

            SqlCommand _Cmd = new SqlCommand("sp_PayOutput_ESI", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[14];
            _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPParamter[0].Value = e.CompanyId;
            _ISPParamter[1] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
            _ISPParamter[1].Value = e.EmployeeId;
            _ISPParamter[2] = new SqlParameter("@v_ESIno", SqlDbType.VarChar);
            _ISPParamter[2].Value = e.ESI_No;
            _ISPParamter[3] = new SqlParameter("@d_Date", SqlDbType.DateTime);
            _ISPParamter[3].Value = e.d_date;
            _ISPParamter[4] = new SqlParameter("@d_From_Date", SqlDbType.DateTime);
            _ISPParamter[4].Value = e.d_FromDate;
            _ISPParamter[5] = new SqlParameter("@d_To_Date", SqlDbType.DateTime);
            _ISPParamter[5].Value = e.d_ToDate;
            _ISPParamter[6] = new SqlParameter("@NetPay", SqlDbType.Int);
            _ISPParamter[6].Value = e.Net_Amount;
            _ISPParamter[7] = new SqlParameter("@ESI_EMP", SqlDbType.Float);
            _ISPParamter[7].Value = e.Emp_Con;
            _ISPParamter[8] = new SqlParameter("@ESI_EPR", SqlDbType.Float);
            _ISPParamter[8].Value = e.Employer_Con;
            _ISPParamter[9] = new SqlParameter("@Paid_Days", SqlDbType.Float);
            _ISPParamter[9].Value = e.Paid_Days;
            _ISPParamter[10] = new SqlParameter("@Absent_Days", SqlDbType.Float);
            _ISPParamter[10].Value = e.Absent_Days;
            _ISPParamter[11] = new SqlParameter("@WeekOffDays", SqlDbType.Float);
            _ISPParamter[11].Value = e.WeekOffDays;
            _ISPParamter[12] = new SqlParameter("@Period_Code", SqlDbType.VarChar);
            _ISPParamter[12].Value = e.period_code;
            _ISPParamter[13] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
            _ISPParamter[13].Value = e.BranchId;

            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
        }
        catch (Exception ex)
        {
        }

    }

    public void PayOutput_PF(PayRoll e)
    {
        try
        {
            _Connection = Con.fn_Connection();

            SqlCommand _Cmd = new SqlCommand("sp_PayOutput_PF", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[17];
            _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPParamter[0].Value = e.CompanyId;
            _ISPParamter[1] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
            _ISPParamter[1].Value = e.EmployeeId;
            _ISPParamter[2] = new SqlParameter("@v_PFno", SqlDbType.VarChar);
            _ISPParamter[2].Value = e.PF_No;
            _ISPParamter[3] = new SqlParameter("@d_Date", SqlDbType.DateTime);
            _ISPParamter[3].Value = e.d_date;
            _ISPParamter[4] = new SqlParameter("@d_From_Date", SqlDbType.DateTime);
            _ISPParamter[4].Value = e.d_FromDate;
            _ISPParamter[5] = new SqlParameter("@d_To_Date", SqlDbType.DateTime);
            _ISPParamter[5].Value = e.d_ToDate;
            _ISPParamter[6] = new SqlParameter("@NetPay", SqlDbType.Int);
            _ISPParamter[6].Value = e.Gross_Salary;
            _ISPParamter[7] = new SqlParameter("@PF", SqlDbType.Float);
            _ISPParamter[7].Value = e.Emp_Con_PF;
            _ISPParamter[8] = new SqlParameter("@Tot_pf", SqlDbType.Float);
            _ISPParamter[8].Value = e.Pf_Amount;
            _ISPParamter[9] = new SqlParameter("@EPF", SqlDbType.Float);
            _ISPParamter[9].Value = e.Emp_Con_EPF;
            _ISPParamter[10] = new SqlParameter("@FPF", SqlDbType.Float);
            _ISPParamter[10].Value = e.Emp_Con_FPF;
            _ISPParamter[11] = new SqlParameter("@VPF", SqlDbType.Float);
            _ISPParamter[11].Value = e.Emp_Con_VPF;
            _ISPParamter[12] = new SqlParameter("@Paid_Days", SqlDbType.Float);
            _ISPParamter[12].Value = e.Paid_Days;
            _ISPParamter[13] = new SqlParameter("@Absent_Days", SqlDbType.Float);
            _ISPParamter[13].Value = e.Absent_Days;
            _ISPParamter[14] = new SqlParameter("@WeekOffDays", SqlDbType.Float);
            _ISPParamter[14].Value = e.WeekOffDays;
            _ISPParamter[15] = new SqlParameter("@Period_Code", SqlDbType.VarChar);
            _ISPParamter[15].Value = e.period_code;
            _ISPParamter[16] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
            _ISPParamter[16].Value = e.BranchId;

            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
        }
        catch (Exception ex)
        {

        }

    }

    public void PayOutput_Loan(PayRoll e)
    {
        try
        {
            _Connection = Con.fn_Connection();

            SqlCommand _Cmd = new SqlCommand("sp_PayOutput_Loan", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[7];
            _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPParamter[0].Value = e.CompanyId;
            _ISPParamter[1] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
            _ISPParamter[1].Value = e.EmployeeId;
            _ISPParamter[2] = new SqlParameter("@pn_LoanID", SqlDbType.Int);
            _ISPParamter[2].Value = e.loanid;
            _ISPParamter[3] = new SqlParameter("@d_Date", SqlDbType.DateTime);
            _ISPParamter[3].Value = e.d_date;
            _ISPParamter[4] = new SqlParameter("@d_From_Date", SqlDbType.DateTime);
            _ISPParamter[4].Value = e.d_FromDate;
            _ISPParamter[5] = new SqlParameter("@d_To_Date", SqlDbType.DateTime);
            _ISPParamter[5].Value = e.d_ToDate;
            _ISPParamter[6] = new SqlParameter("@Amount", SqlDbType.Float);
            _ISPParamter[6].Value = e.float_Amount;



            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
        }
        catch
        {

        }

    }

    public void PayOutput_NetPay(PayRoll e)
    {
        try
        {
            _Connection = Con.fn_Connection();

            SqlCommand _Cmd = new SqlCommand("sp_PayOutput_NetPay", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[17];
            _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPParamter[0].Value = e.CompanyId;
            _ISPParamter[1] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
            _ISPParamter[1].Value = e.EmployeeId;
            _ISPParamter[2] = new SqlParameter("@d_Date", SqlDbType.DateTime);
            _ISPParamter[2].Value = e.d_date;
            _ISPParamter[3] = new SqlParameter("@d_From_Date", SqlDbType.DateTime);
            _ISPParamter[3].Value = e.d_FromDate;
            _ISPParamter[4] = new SqlParameter("@d_To_Date", SqlDbType.DateTime);
            _ISPParamter[4].Value = e.d_ToDate;
            _ISPParamter[5] = new SqlParameter("@Earn_Act_Amount", SqlDbType.Float);
            _ISPParamter[5].Value = e.Earn_Act_Amount;
            _ISPParamter[6] = new SqlParameter("@Earn_Amount", SqlDbType.Float);
            _ISPParamter[6].Value = e.Earn_Amount;
            _ISPParamter[7] = new SqlParameter("@Ded_Act_Amount", SqlDbType.Float);
            _ISPParamter[7].Value = e.Ded_Act_Amount;
            _ISPParamter[8] = new SqlParameter("@Ded_Amount", SqlDbType.Float);
            _ISPParamter[8].Value = e.Ded_Amount;
            _ISPParamter[9] = new SqlParameter("@NetPay", SqlDbType.Int);
            _ISPParamter[9].Value = e.Net_Amount;
            _ISPParamter[10] = new SqlParameter("@Act_Basic", SqlDbType.Float);
            _ISPParamter[10].Value = e.Act_Basic;
            _ISPParamter[11] = new SqlParameter("@Earned_Basic", SqlDbType.Float);
            _ISPParamter[11].Value = e.Earned_Basic;
            _ISPParamter[12] = new SqlParameter("@Gross_salary", SqlDbType.Float);
            _ISPParamter[12].Value = e.Gross_Salary;
            _ISPParamter[13] = new SqlParameter("@Net_salary", SqlDbType.Float);
            _ISPParamter[13].Value = e.Net_Salary;
            _ISPParamter[14] = new SqlParameter("@OT_Amt", SqlDbType.Float);
            _ISPParamter[14].Value = e.OT_Amt;
            _ISPParamter[15] = new SqlParameter("@Period_code", SqlDbType.VarChar);
            _ISPParamter[15].Value = e.period_code;
            _ISPParamter[16] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
            _ISPParamter[16].Value = e.BranchId;

            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
        }
        catch(Exception ex)
        {

        }

    }

    public void PayOutput_Actuals(PayRoll e)
    {
        try
        {
            _Connection = Con.fn_Connection();

            SqlCommand _Cmd = new SqlCommand("sp_PayOutput_Actuals", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[10];
            _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPParamter[0].Value = e.CompanyId;
            _ISPParamter[1] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
            _ISPParamter[1].Value = e.EmployeeId;
            _ISPParamter[2] = new SqlParameter("@d_Date", SqlDbType.DateTime);
            _ISPParamter[2].Value = e.d_date;
            _ISPParamter[3] = new SqlParameter("@d_From_Date", SqlDbType.DateTime);
            _ISPParamter[3].Value = e.d_FromDate;
            _ISPParamter[4] = new SqlParameter("@d_To_Date", SqlDbType.DateTime);
            _ISPParamter[4].Value = e.d_ToDate;
            _ISPParamter[5] = new SqlParameter("@Earn_Act_Amount", SqlDbType.Float);
            _ISPParamter[5].Value = e._Earn_Act_Amount_A;
            _ISPParamter[6] = new SqlParameter("@Ded_Act_Amount", SqlDbType.Float);
            _ISPParamter[6].Value = e.Ded_Act_Amount;
            _ISPParamter[7] = new SqlParameter("@Act_Basic", SqlDbType.Float);
            _ISPParamter[7].Value = e.Act_Basic;
            _ISPParamter[8] = new SqlParameter("@Period_code", SqlDbType.VarChar);
            _ISPParamter[8].Value = e.period_code;
            _ISPParamter[9] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
            _ISPParamter[9].Value = e.BranchId;

            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
        }
        catch (Exception ex)
        {

        }

    }

    public void PayOutput_Deleteion(PayRoll e)
    {
        try
        {
            _Connection = Con.fn_Connection();

            SqlCommand _Cmd = new SqlCommand("sp_PayOutput_Deletion", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[2];
            _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPParamter[0].Value = e.CompanyId;
            _ISPParamter[1] = new SqlParameter("@d_Date", SqlDbType.DateTime);
            _ISPParamter[1].Value = e.d_date;

            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
        }
        catch
        {

        }
    }

    public void fn_Update_Earnings(PayRoll e)
    {


        _Connection = Con.fn_Connection();

        _Connection.Open();

        SqlCommand _RSC_can = new SqlCommand("update paym_Earnings set v_EarningsName ='" + e.EarningsName + "',v_EarningsCode ='" + e.EarningsCode + "',c_Regular ='" + e.regular + "',d_order='" + e.d_order + "', c_PF ='" + e.Pf + "',c_ESI ='" + e.Esi + "',c_LOP='" + e.LOP + "',c_OT='" + e.OT + "',c_PT='" + e.PT + "'  where pn_EarningsID=" + e.EarningsId + " and pn_CompanyID =" + e.CompanyId + "", _Connection);

        _RSC_can.ExecuteNonQuery();

        _Connection.Close();

    }

    public void fn_Update_Deduction(PayRoll e)
    {


        _Connection = Con.fn_Connection();

        _Connection.Open();

        SqlCommand _RSC_can = new SqlCommand("update paym_Deduction set v_DeductionCode ='" + e.DeducationCode + "',v_DeductionName ='" + e.DeductionName + "',c_Regular ='" + e.regular + "', d_order='" + e.d_order + "' where pn_DeductionID=" + e.DeductionId + " and pn_CompanyID =" + e.CompanyId + "", _Connection);

        _RSC_can.ExecuteNonQuery();

        _Connection.Close();


    }

    public void Update_Paybill(PayRoll e)
    {
        try
        {
            _Connection = Con.fn_Connection();
            SqlCommand _Cmd = new SqlCommand("Proc_UpdatePayBill", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[4];
            _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPParamter[0].Value = e.CompanyId;
            _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
            _ISPParamter[1].Value = e.BranchId;
            _ISPParamter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
            _ISPParamter[2].Value = e.EmployeeId;
            _ISPParamter[3] = new SqlParameter("@d_Date", SqlDbType.DateTime);
            _ISPParamter[3].Value = e.d_date;
            
            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
        }
        catch (Exception ex)
        {

        }
    }

    public string holiday(PayRoll h)
    {
        try
        {
            _Connection = Con.fn_Connection();
            SqlCommand cmd = new SqlCommand("sp_Holiday", _Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] ispparam = new SqlParameter[4];
            ispparam[0] = new SqlParameter("@pn_HolidayID", SqlDbType.Int);
            ispparam[0].Value = h.holidayid;
            ispparam[1] = new SqlParameter("@v_Holidayname", SqlDbType.VarChar);
            ispparam[1].Value = h.holidayname;
            ispparam[2] = new SqlParameter("@d_Holidaydate", SqlDbType.DateTime);
            ispparam[2].Value = h.holidaydate;
            ispparam[3] = new SqlParameter("@status", SqlDbType.Char);
            ispparam[3].Value = h.status;
            for (int i = 0; i < ispparam.Length; i++)
            {
                cmd.Parameters.Add(ispparam[i]);
            }
            _Connection.Open();
            cmd.ExecuteNonQuery();
            _Connection.Close();

            return "0";
        }
        catch (SqlException Ex)
        {
            return "1";
        }
    }

    public string Loan_update(PayRoll l)
    {
        try
        {
            _Connection = Con.fn_Connection();
            SqlCommand cmd = new SqlCommand("sp_paym_Loan", _Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] ispparam = new SqlParameter[6];
            ispparam[0] = new SqlParameter("@pn_Companyid", SqlDbType.Int);
            ispparam[0].Value = l.CompanyId;
            ispparam[1] = new SqlParameter("@pn_LoanID", SqlDbType.Int);
            ispparam[1].Value = l.loanid;
            ispparam[2] = new SqlParameter("@v_LoanName", SqlDbType.VarChar);
            ispparam[2].Value = l.loanname;
            ispparam[3] = new SqlParameter("@v_LoanCode", SqlDbType.VarChar);
            ispparam[3].Value = l.loancode;
            ispparam[4] = new SqlParameter("@status", SqlDbType.Char);
            ispparam[4].Value = l.status;
            ispparam[5] = new SqlParameter("@pn_Branchid", SqlDbType.Int);
            ispparam[5].Value = l.BranchId;
            for (int i = 0; i < ispparam.Length; i++)
            {
                cmd.Parameters.Add(ispparam[i]);
            }
            _Connection.Open();
            cmd.ExecuteNonQuery();
            _Connection.Close();
            return "0";
        }
        catch (SqlException Ex)
        {
            return "1";
        }
    }

    public string bankupdate(PayRoll l)
    {
        try
        {
            _Connection = Con.fn_Connection();
            SqlCommand cmd = new SqlCommand("sp_paym_Bank", _Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] ispparam = new SqlParameter[12];
            ispparam[0] = new SqlParameter("@pn_BankID", SqlDbType.Int);
            ispparam[0].Value = l.bankid;
            ispparam[1] = new SqlParameter("@v_BankName", SqlDbType.VarChar);
            ispparam[1].Value = l.bankname;
            ispparam[2] = new SqlParameter("@v_BankCode", SqlDbType.VarChar);
            ispparam[2].Value = l.bankcode;
            ispparam[3] = new SqlParameter("@status", SqlDbType.Char);
            ispparam[3].Value = l.status;
            ispparam[4] = new SqlParameter("@Branch_Name", SqlDbType.VarChar);
            ispparam[4].Value = l.Branch_Name; ;
            ispparam[5] = new SqlParameter("@Account_Type", SqlDbType.VarChar);
            ispparam[5].Value = l.Account_Type;
            ispparam[6] = new SqlParameter("@Micr_Code", SqlDbType.VarChar);
            ispparam[6].Value = l.Micr_Code;
            ispparam[7] = new SqlParameter("@Ifsc_Code", SqlDbType.VarChar);
            ispparam[7].Value = l.Ifsc_Code;
            ispparam[8] = new SqlParameter("@Address", SqlDbType.VarChar);
            ispparam[8].Value = l.Address;
            ispparam[9] = new SqlParameter("@others", SqlDbType.VarChar);
            ispparam[9].Value = l.others;
            ispparam[10] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
            ispparam[10].Value = l.BranchId;
            ispparam[11] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            ispparam[11].Value = l.CompanyId;
            for (int i = 0; i < ispparam.Length; i++)
            {
                cmd.Parameters.Add(ispparam[i]);
            }
            _Connection.Open();
            cmd.ExecuteNonQuery();
            _Connection.Close();
            return "0";
        }
        catch (SqlException Ex)
        {
            return "1";
        }
    }

    public string Loancancel(PayRoll a)
    {
        try
        {
            _Connection = Con.fn_Connection();
            SqlCommand cmd = new SqlCommand("sp_Loan_Cancel", _Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] ispparm = new SqlParameter[5];
            ispparm[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            ispparm[0].Value = a.CompanyId;
            ispparm[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
            ispparm[1].Value = a.BranchId;
            ispparm[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
            ispparm[2].Value = a.EmployeeId;
            ispparm[3] = new SqlParameter("@fn_LoanID", SqlDbType.Int);
            ispparm[3].Value = a.loanid;
            ispparm[4] = new SqlParameter("@d_date", SqlDbType.DateTime);
            ispparm[4].Value = a.Date;
            for (int i = 0; i < ispparm.Length; i++)
            {
                cmd.Parameters.Add(ispparm[i]);
            }
            _Connection.Open();
            cmd.ExecuteNonQuery();
            _Connection.Close();
            return "0";
        }
        catch (SqlException Ex)
        {
            return "1";
        }
    }

    public string updateclosere(PayRoll l)
    {
        try
        {

            _Connection = Con.fn_Connection();
            SqlCommand cmd = new SqlCommand("sp_loancloser", _Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] ispparam = new SqlParameter[19];
            ispparam[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            ispparam[0].Value = l.CompanyId;
            ispparam[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
            ispparam[1].Value = l.BranchId;
            ispparam[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
            ispparam[2].Value = l.EmployeeId;
            ispparam[3] = new SqlParameter("@loan_appid", SqlDbType.VarChar);
            ispparam[3].Value = l.loan_mas_id;
            ispparam[4] = new SqlParameter("@d_date", SqlDbType.DateTime);
            ispparam[4].Value = l.applicationdate;
            ispparam[5] = new SqlParameter("@n_loanamount", SqlDbType.Int);
            ispparam[5].Value = l.loan_amt;
            ispparam[6] = new SqlParameter("@n_balanceamount", SqlDbType.Float);
            ispparam[6].Value = l.balanceamount;
            ispparam[7] = new SqlParameter("@n_paidamount", SqlDbType.Float);
            ispparam[7].Value = l.paidamount;
            ispparam[8] = new SqlParameter("@n_closureamount", SqlDbType.Float);
            ispparam[8].Value = l.closureamount;
            ispparam[9] = new SqlParameter("@n_checkno", SqlDbType.VarChar);
            ispparam[9].Value = l.checkno;
            ispparam[10] = new SqlParameter("@d_checkdate", SqlDbType.DateTime);
            ispparam[10].Value = l.checkdate;
            ispparam[11] = new SqlParameter("@n_checkamount", SqlDbType.VarChar);
            ispparam[11].Value = l.checkamount;
            ispparam[12] = new SqlParameter("@v_bankname", SqlDbType.VarChar);
            ispparam[12].Value = l.bankname;
            ispparam[13] = new SqlParameter("@v_Remarks", SqlDbType.VarChar);
            ispparam[13].Value = l.Remarks;
            ispparam[14] = new SqlParameter("@c_status", SqlDbType.VarChar);
            ispparam[14].Value = l.status;
            ispparam[15] = new SqlParameter("@loan_interest", SqlDbType.Decimal);
            ispparam[15].Value = l.loaninterest_amt;
            ispparam[16] = new SqlParameter("@loan_process", SqlDbType.Char);
            ispparam[16].Value = l.loanprocess;
            ispparam[17] = new SqlParameter("@loan_name", SqlDbType.Char);
            ispparam[17].Value = l.loanname;
            ispparam[18] = new SqlParameter("@payment_mode", SqlDbType.Char);
            ispparam[18].Value = l.pay_mode;

            for (int i = 0; i < ispparam.Length; i++)
            {
                cmd.Parameters.Add(ispparam[i]);
            }
            _Connection.Open();
            cmd.ExecuteNonQuery();
            _Connection.Close();

            return "0";
        }
        catch (SqlException Ex)
        {
            return "1";
        }

    }

    public string updateallot(PayRoll l)
    {
        try
        {

            _Connection = Con.fn_Connection();
            SqlCommand cmd = new SqlCommand("sp_loanentry", _Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] ispparam = new SqlParameter[11];
            ispparam[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            ispparam[0].Value = l.CompanyId;
            ispparam[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
            ispparam[1].Value = l.BranchId;
            ispparam[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
            ispparam[2].Value = l.EmployeeId;
            ispparam[3] = new SqlParameter("@fn_LoanID", SqlDbType.Int);
            ispparam[3].Value = l.loanid;
            ispparam[4] = new SqlParameter("@d_appdate", SqlDbType.DateTime);
            ispparam[4].Value = l.dateapplication;
            ispparam[5] = new SqlParameter("@d_effdate", SqlDbType.DateTime);
            ispparam[5].Value = l.effectivedate;
            ispparam[6] = new SqlParameter("@Loan_Amt", SqlDbType.Int);
            ispparam[6].Value = l.Amount;
            ispparam[7] = new SqlParameter("@InstalmentAmt", SqlDbType.Float);
            ispparam[7].Value = l.F_Amount;
            ispparam[8] = new SqlParameter("@Instalmentcount", SqlDbType.Int);
            ispparam[8].Value = l.installmentcount;
            ispparam[9] = new SqlParameter("@Balance_Amt", SqlDbType.Float);
            ispparam[9].Value = l.balanceamount;
            ispparam[10] = new SqlParameter("@c_status", SqlDbType.Char);
            ispparam[10].Value = l.status;
            for (int i = 0; i < ispparam.Length; i++)
            {
                cmd.Parameters.Add(ispparam[i]);
            }
            _Connection.Open();
            cmd.ExecuteNonQuery();
            _Connection.Close();


            return "0";
        }
        catch (SqlException Ex)
        {
            return "1";
        }


    }

    public string payinputupdate(PayRoll p)
    {
        try
        {
            _Connection = Con.fn_Connection();
            SqlCommand cmd = new SqlCommand("sp_payinput", _Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] isparam = new SqlParameter[20];
            isparam[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            isparam[0].Value = p.CompanyId;
            isparam[1] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
            isparam[1].Value = p.EmployeeId;
            isparam[2] = new SqlParameter("@d_Date", SqlDbType.DateTime);
            isparam[2].Value = p.d_date;
            isparam[3] = new SqlParameter("@d_From_Date", SqlDbType.DateTime);
            isparam[3].Value = p.d_FromDate;
            isparam[4] = new SqlParameter("@d_To_Date", SqlDbType.DateTime);
            isparam[4].Value = p.d_ToDate;
            isparam[5] = new SqlParameter("@Calc_Days", SqlDbType.Float);
            isparam[5].Value = p.Calc_Days;
            isparam[6] = new SqlParameter("@Paid_Days", SqlDbType.Float);
            isparam[6].Value = p.Paid_Days;
            isparam[7] = new SqlParameter("@Present_Days", SqlDbType.Float);
            isparam[7].Value = p.Present_Days;
            isparam[8] = new SqlParameter("@Absent_Days", SqlDbType.Float);
            isparam[8].Value = p.Absent_Days;
            isparam[9] = new SqlParameter("@TotLeave_Days", SqlDbType.Float);
            isparam[9].Value = p.TotLeave_Days;
            isparam[10] = new SqlParameter("@WeekOffDays", SqlDbType.Float);
            isparam[10].Value = p.WeekOffDays;
            isparam[11] = new SqlParameter("@Holidays", SqlDbType.Float);
            isparam[11].Value = p.Holidays;
            isparam[12] = new SqlParameter("@OnDuty_days", SqlDbType.Float);
            isparam[12].Value = p.OnDuty_days;
            isparam[13] = new SqlParameter("@Compoff_Days", SqlDbType.Float);
            isparam[13].Value = p.Compoff_Days;
            isparam[14] = new SqlParameter("@Tour_Days", SqlDbType.Float);
            isparam[14].Value = p.Tour_Days;
            isparam[15] = new SqlParameter("@Att_Bonus", SqlDbType.Char);
            isparam[15].Value = p.Att_Bonus;
            isparam[16] = new SqlParameter("@Att_BonusAmount", SqlDbType.Float);
            isparam[16].Value = p.Att_BonusAmount;
            isparam[17] = new SqlParameter("@OT_HRS", SqlDbType.Float);
            isparam[17].Value = p.OT_HRS;
            isparam[18] = new SqlParameter("@Earn_Arrears", SqlDbType.Float);
            isparam[18].Value = p.Earn_Arrears;
            isparam[19] = new SqlParameter("@Ded_Arrears", SqlDbType.Float);
            isparam[19].Value = p.Ded_Arrears;
            
            for (int i = 0; i < isparam.Length; i++)
            {
                cmd.Parameters.Add(isparam[i]);
            }
            _Connection.Open();
            cmd.ExecuteNonQuery();
            _Connection.Close();
            return "0";



        }
        catch(SqlException ex) 
        {
            return "1";
            
        }

    }

    public string payinput_PT(PayRoll p)
    {
        try
        {
            _Connection = Con.fn_Connection();
            SqlCommand cmd = new SqlCommand("update payinput set PT_Gross = '" + p.Act_Amount + "' where pn_branchID = '" + p.BranchId + "' and pn_employeeID = '" + p.EmployeeId + "' and d_date = '" + p.d_date + "'", _Connection);
           
            _Connection.Open();
            cmd.ExecuteNonQuery();
            _Connection.Close();
            return "0";
        }
        catch (SqlException ex)
        {
            return "1";

        }

    } 



    //***********************Collections*************************************
    public Collection<PayRoll> fn_payinput()
    {
        Collection<PayRoll> payinputlist = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string substring = "select * from PayInput";
        SqlCommand cmd = new SqlCommand(substring, _Connection);
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            PayRoll p = new PayRoll();
            p.CompanyId = (int)dr["pn_CompanyID"];
            p.EmployeeId = (int)dr["pn_EmployeeID"];
            p.d_date = (DateTime)dr["d_Date"];
            p.d_FromDate = (DateTime)dr["d_From_Date"];
            p.d_ToDate = (DateTime)dr["d_To_Date"];
            p.Calc_Days = (double)dr["Calc_Days"];
            p.Paid_Days = (double)dr["Paid_Days"];
            p.Present_Days = (double)dr["Present_Days"];
            p.Absent_Days = (double)dr["Absent_Days"];
            p.TotLeave_Days = (double)dr["TotLeave_Days"];
            p.WeekOffDays = (double)dr["WeekOffDays"];
            p.Holidays = (double)dr["Holidays"];
            p.OnDuty_days = (double)dr["OnDuty_days"];
            p.Compoff_Days = (double)dr["Compoff_Days"];
            p.Tour_Days = (double)dr["Tour_Days"];
            p.Att_Bonus = (string)dr["Att_Bonus"];
            p.Att_BonusAmount = (double)dr["Att_BonusAmount"];
            p.OT_HRS = (double)dr["OT_HRS"];
            p.Earn_Arrears = (double)dr["Earn_Arrears"];
            p.Ded_Arrears = (double)dr["Ded_Arrears"];
            payinputlist.Add(p);
        }
        _Connection.Close();
        return payinputlist;
    }


    public Collection<PayRoll> fn_payinput_check(PayRoll pay)
    {
        Collection<PayRoll> payinputlist = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        _Connection.Open();
        string substring = "select * from PayInput where pn_CompanyID = '" + pay.CompanyId + "' and pn_BranchID = '" + pay.BranchId + "' and pn_EmployeeID = '" + pay.EmployeeId + "' and d_date ='" + pay.strDate + "'";
        SqlCommand cmd = new SqlCommand(substring, _Connection);
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            PayRoll p = new PayRoll();
            p.CompanyId = (int)dr["pn_CompanyID"];
            p.EmployeeId = (int)dr["pn_EmployeeID"];
            p.d_date = (DateTime)dr["d_Date"];
            p.d_FromDate = (DateTime)dr["d_From_Date"];
            p.d_ToDate = (DateTime)dr["d_To_Date"];
            p.Calc_Days = (double)dr["Calc_Days"];
            p.Paid_Days = (double)dr["Paid_Days"];
            p.Present_Days = (double)dr["Present_Days"];
            p.Absent_Days = (double)dr["Absent_Days"];
            p.TotLeave_Days = (double)dr["TotLeave_Days"];
            p.WeekOffDays = (double)dr["WeekOffDays"];
            p.Holidays = (double)dr["Holidays"];
            p.OnDuty_days = (double)dr["OnDuty_days"];
            p.Compoff_Days = (double)dr["Compoff_Days"];
            p.Tour_Days = (double)dr["Tour_Days"];
            p.Att_Bonus = (string)dr["Att_Bonus"];
            p.Att_BonusAmount = (double)dr["Att_BonusAmount"];
            p.Earn_Arrears = (double)dr["Earn_Arrears"];
            p.Ded_Arrears = (double)dr["Ded_Arrears"];
            p.Act_Basic = (double)dr["Act_Basic"];
            p.Earned_Basic = (double)dr["Earn_Basic"];
            p.OT_Amt = (double)dr["ot_Amt"];
            p.Date = (DateTime)dr["OT_Hrs"];
            p.others = dr["OT_Hrs"].ToString();
            payinputlist.Add(p);
        }
        _Connection.Close();
        return payinputlist;
    }


    public Collection<PayRoll> fn_Get_Basic(PayRoll pay)
    {
        Collection<PayRoll> payinputlist = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        _Connection.Open();
        //string substring = "SELECT top 1 * FROM salary_structure where  pn_companyid = '" + pay.CompanyId + "' and  pn_BranchID = '" + pay.BranchId + "' and pn_EmployeeID = '" + pay.EmployeeId + "' and effective_date <= '" + pay.strDate + "' order by effective_date desc ";
        string substring = "SELECT top 1 * FROM paym_employee where  pn_companyid = '" + pay.CompanyId + "' and  pn_BranchID = '" + pay.BranchId + "' and pn_EmployeeID = '" + pay.EmployeeId + "'";
        SqlCommand cmd = new SqlCommand(substring, _Connection);
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            PayRoll p = new PayRoll();
            p.CompanyId = (int)dr["pn_CompanyID"];
            p.EmployeeId = (int)dr["pn_EmployeeID"];
            p.Act_Basic = (double)dr["basic_salary"];
            payinputlist.Add(p);
        }
        _Connection.Close();
        return payinputlist;
    }
   
    //$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
    public Collection<PayRoll> fn_loanentry(string str_query)
    {
        Collection<PayRoll> loanlist = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        SqlCommand cmd = new SqlCommand(str_query, _Connection);
       _Connection.Open();
       SqlDataReader dr = cmd.ExecuteReader();
       while (dr.Read())
       {
           PayRoll l = new PayRoll();
           l.EmployeeId = (int)dr["pn_EmployeeID"];
           l.EmployeeName = (string)dr["emp_name"];
           l.loanappid = (string)dr["loan_appid"];
           l.loanid = (int)dr["fn_LoanID"];
           l.loanprocess = (string)dr["loan_process"];
           l.loaninterest = Convert.ToDouble(dr["interest"]);
           l.loanname = (string)dr["loan_name"];
           l.strdateapplication = Convert_ToIISDate1(dr["san_date"].ToString());// (DateTime)dr["d_appdate"];
           l.streffectivedate = Convert_ToIISDate1(dr["d_effdate"].ToString());//(DateTime)dr["d_effdate"];
           l.app_date = Convert.ToDateTime(dr["san_date"]);
           l.eff_date = Convert.ToDateTime(dr["d_effdate"]);
           l.Amount = Convert.ToDouble(dr["Loan_Amt"]);
           l.F_Amount = Convert.ToDouble(dr["InstalmentAmt"]);
           l.installmentcount = (int)dr["Instalmentcount"];
           l.balanceamount = Convert.ToDouble(dr["Balance_Amt"]);
           l.loan_process = (string)dr["loan_process"];
           l.tot_interest_amt = Convert.ToDouble(dr["tot_interest_amt"]);
           l.loan_calc = (string)dr["loan_calculation"];


           l.status = Convert.ToChar(dr["c_status"]);

           loanlist.Add(l);
       }
       _Connection.Close();
        return loanlist;
    }   

    public Collection<PayRoll> fn_loanPrecloser(string s_query)
    {
        Collection<PayRoll> loanlist = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();

        SqlCommand cmd = new SqlCommand(s_query, _Connection);
        _Connection.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            PayRoll l = new PayRoll();

            l.CompanyId = (int)dr["pn_companyid"];
            l.loanid = (int)dr["fn_LoanID"];
            l.EmployeeId = (int)dr["pn_EmployeeID"];
            l.Empname = Convert.IsDBNull(dr["employee_first_Name"]) ? "" : (string)dr["employee_first_Name"];        
            l.loancode = Convert.IsDBNull(dr["v_loancode"]) ? "" : (string)dr["v_loancode"];
            l.strdateapplication = Convert_ToIISDate(dr["d_date"].ToString()); //(DateTime)dr["d_date"];
            l.loanamount = (int)dr["n_loanamount"];
            l.balanceamount = (double)dr["n_balanceamount"];
            l.paidamount = (double)dr["n_paidamount"];
            l.closureamount = (double)dr["n_closureamount"];

            l.checkno = Convert.IsDBNull(dr["n_checkno"]) ? "" : (string)dr["n_checkno"];
            l.strcheckdate = Convert_ToIISDate(dr["d_checkdate"].ToString()); //(DateTime)dr["d_checkdate"];
            l.checkamount = Convert.ToString(dr["n_checkamount"]);
            l.bankname = Convert.IsDBNull(dr["v_bankname"]) ? "" : (string)dr["v_bankname"];  
       
            loanlist.Add(l);
        }
        _Connection.Close();
        return loanlist;
    }

    public Collection<PayRoll> fn_loanPrecloser1(string s_query)
    {
        Collection<PayRoll> loanlist = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();

        SqlCommand cmd = new SqlCommand(s_query, _Connection);
        _Connection.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            PayRoll l = new PayRoll();

            l.CompanyId = (int)dr["pn_companyid"];
            l.BranchId = (int)dr["pn_branchid"];
            l.loan_mas_id = Convert.ToString(dr["loan_appid"]);
            l.EmployeeId = (int)dr["pn_EmployeeID"];
            l.Empname = Convert.IsDBNull(dr["employee_first_Name"]) ? "" : (string)dr["employee_first_Name"];
            l.loancode = Convert.IsDBNull(dr["loan_appid"]) ? "" : (string)dr["loan_appid"];
            l.strdateapplication = Convert_ToIISDate(dr["d_effdate"].ToString()); //(DateTime)dr["d_date"];
            l.loan_amt = (decimal)dr["n_loanamount"];
            l.balanceamount = Convert.ToDouble(dr["balance_amt"]);
            l.paidamount = (double)dr["n_paidamount"];
            l.closureamount = (double)dr["n_closureamount"];

            l.checkno = Convert.IsDBNull(dr["n_checkno"]) ? "" : (string)dr["n_checkno"];
            l.strcheckdate = Convert_ToIISDate(dr["d_checkdate"].ToString()); //(DateTime)dr["d_checkdate"];
            l.checkamount = Convert.ToString(dr["n_checkamount"]);
            l.bankname = Convert.IsDBNull(dr["v_bankname"]) ? "" : (string)dr["v_bankname"];
            l.pay_mode = Convert.ToString(dr["payment_mode"]);

            loanlist.Add(l);
        }
        _Connection.Close();
        return loanlist;
    }

    public Collection<PayRoll> fn_loan(PayRoll p)                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              
    {
        Collection<PayRoll> loanlist = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        SqlCommand cmd = new SqlCommand("Select * from paym_Loan where status='Y' and pn_branchid='" + p.BranchId + "'  ", _Connection);
        _Connection.Open();
        SqlDataReader dr = cmd.ExecuteReader();

        while (dr.Read())
        {
            PayRoll l = new PayRoll();
            l.loanid = (int)dr["pn_LoanID"];
            l.loanname = Convert.IsDBNull(dr["v_LoanName"]) ? "" : (string)dr["v_LoanName"];
            l.loancode = Convert.IsDBNull(dr["v_LoanCode"]) ? "" : (string)dr["v_LoanCode"];
            loanlist.Add(l);

        }
        _Connection.Close();
        return loanlist;
    }

    public Collection<PayRoll> fn_LoanCancel(PayRoll pr)
    {
        Collection<PayRoll> loanlist = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string substring = "select * from Loan_Cancel where pn_CompanyID=" + pr.CompanyId + " and pn_BranchID=" + pr.BranchId + " and d_date='" + pr.Date + "'";
        SqlCommand cmd = new SqlCommand(substring, _Connection);
        _Connection.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            PayRoll b = new PayRoll();

            b.EmployeeId = (int)dr["pn_EmployeeID"];
            b.loanid = (int)dr["fn_LoanID"];
            b.strDate = Convert_ToIISDate(dr["d_date"].ToString()); // (DateTime)dr["d_date"];

            loanlist.Add(b);
        }
        _Connection.Close();
        return loanlist;
    }

    

    public Collection<PayRoll> fn_Loan_Cancel()
    {
        Collection<PayRoll> loanlist = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        SqlCommand cmd = new SqlCommand("select * from Loan_Cancel", _Connection);
        _Connection.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            PayRoll b = new PayRoll();

            b.EmployeeId = (int)dr["pn_EmployeeID"];
            b.loanid = (int)dr["fn_LoanID"];
            b.Date = (DateTime)dr["d_date"];

            loanlist.Add(b);
        }
        _Connection.Close();
        return loanlist;
    }

   
    public Collection<PayRoll> fn_bank()
    {
        Collection<PayRoll> banklist = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        SqlCommand cmd = new SqlCommand("Select * from paym_bank where status='Y'", _Connection);
        _Connection.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            
            PayRoll b = new PayRoll();
            b.bankid = (int)dr["pn_BankID"];
            b.bankname = Convert.IsDBNull(dr["v_BankName"]) ? "" : (string)dr["v_BankName"];
            b.bankcode = Convert.IsDBNull(dr["v_BankCode"]) ? "" : (string)dr["v_BankCode"];
            banklist.Add(b);

        }
        _Connection.Close();
        return banklist;
        
    }

    public Collection<PayRoll> fn_Holiday()
    {
        Collection<PayRoll> Holidaylist = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        SqlCommand cmd = new SqlCommand("select * from Holiday where status='Y'", _Connection);
        _Connection.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            PayRoll d = new PayRoll();
            d.holidayid = (int)dr["pn_HolidayID"];
            d.holidayname = Convert.IsDBNull(dr["v_Holidayname"]) ? "" : (string)dr["v_Holidayname"];
            d.holidaydate = (DateTime)dr["d_Holidaydate"];
            d.holidaytodate = (DateTime)dr["d_Holidaytodate"];
            
            Holidaylist.Add(d);
        }
        _Connection.Close();
        return Holidaylist;
    }

    public Collection<PayRoll> fn_Holiday1(int eid)
    {
        Collection<PayRoll> Holidaylist = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        SqlCommand cmd = new SqlCommand("select * from paym_Holiday where pn_BranchID='" + eid + "'", _Connection);
        _Connection.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            PayRoll d = new PayRoll();
            d.holidayname = dr["pn_HolidayCode"].ToString();
            d.holidaydate = (DateTime)dr["from_date"];
            d.holidaytodate = (DateTime)dr["to_date"];

            Holidaylist.Add(d);
        }
        _Connection.Close();
        return Holidaylist;
    }


    public Collection<PayRoll> fn_Emp_NonEarnings(PayRoll e)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();

        SqlCommand _SSDesignation = new SqlCommand(e.temp_str, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();
            employee.EmployeeId = (int)dr_Designation["pn_EmployeeID"];
            employee.EarningsId = (int)dr_Designation["pn_EarningsID"];
            employee.EarningsCode = Convert.IsDBNull(dr_Designation["v_EarningsCode"]) ? "" : (string)dr_Designation["v_EarningsCode"];
            employee.EmployeeCode = Convert.IsDBNull(dr_Designation["EmployeeCode"]) ? "" : (string)dr_Designation["EmployeeCode"];
            employee.Amount = (double)dr_Designation["Amount"];
            //employee.regular = Convert.ToChar(dr_Designation["c_eligible"]);
            employee.d_date = (DateTime)dr_Designation["d_Date"];
            //employee.strDate = Convert_ToIISDate(dr_Designation["d_Date"].ToString());

            DesignationList.Add(employee);
        }
        _Connection.Close();
        return DesignationList;
    }


    public Collection<PayRoll> fn_Emp_NonDeduction(PayRoll e)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();

        SqlCommand _SSDesignation = new SqlCommand(e.temp_str, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();
            employee.EmployeeId = (int)dr_Designation["pn_EmployeeID"];
            employee.DeductionId = (int)dr_Designation["pn_DeductionID"];
            employee.DeducationCode = Convert.IsDBNull(dr_Designation["v_DeductionCode"]) ? "" : (string)dr_Designation["v_DeductionCode"];
            employee.EmployeeCode = Convert.IsDBNull(dr_Designation["EmployeeCode"]) ? "" : (string)dr_Designation["EmployeeCode"];
            employee.Amount = (int)dr_Designation["n_Amount"];
            //employee.regular = Convert.ToChar(dr_Designation["c_eligible"]);
            employee.d_date = (DateTime)dr_Designation["d_Date"];

            DesignationList.Add(employee);
        }
        _Connection.Close();
        return DesignationList;
    }


    public Collection<PayRoll> fn_Earnings1(int eid)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString = "select * from paym_Earnings where   status='Y' and pn_BranchID='" + eid + "' order by d_order asc";
        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();
            employee.EarningsId = (int)dr_Designation["pn_EarningsID"];
            employee.EarningsCode = Convert.IsDBNull(dr_Designation["v_EarningsCode"]) ? "" : (string)dr_Designation["v_EarningsCode"];
            employee.EarningsName = Convert.IsDBNull(dr_Designation["v_EarningsName"]) ? "" : (string)dr_Designation["v_EarningsName"];
            employee.regular = Convert.ToChar(dr_Designation["c_Regular"]);
            employee.Pf = Convert.ToChar(dr_Designation["c_PF"]);
            employee.Esi = Convert.ToChar(dr_Designation["c_ESI"]);
            employee.OT = Convert.ToChar(dr_Designation["c_OT"]);
            employee.LOP = Convert.ToChar(dr_Designation["c_LOP"]);
            employee.PT = Convert.ToChar(dr_Designation["c_PT"]);
            employee.Print = Convert.ToChar(dr_Designation["c_Print"]);
            employee.payslip = Convert.ToChar(dr_Designation["payslip"]);
            employee.d_order = Convert.ToChar(dr_Designation["d_order"]);
            employee.Amount = 0;
            DesignationList.Add(employee);
        }
        _Connection.Close();
        return DesignationList;
    }


    public Collection<PayRoll> fn_Earnings(PayRoll p)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString = "select * from paym_Earnings where pn_CompanyID = '" + p.CompanyId + "' and pn_BranchID = '" + p.BranchId + "' and c_Regular='Y' and status='Y'  order by d_order asc";
        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();
            employee.EarningsId = (int)dr_Designation["pn_EarningsID"];
            employee.EarningsCode = Convert.IsDBNull(dr_Designation["v_EarningsCode"]) ? "" : (string)dr_Designation["v_EarningsCode"];
            employee.EarningsName = Convert.IsDBNull(dr_Designation["v_EarningsName"]) ? "" : (string)dr_Designation["v_EarningsName"];
            employee.regular = Convert.ToChar(dr_Designation["c_Regular"]);
            employee.Pf = Convert.ToChar(dr_Designation["c_PF"]);
            employee.Esi = Convert.ToChar(dr_Designation["c_ESI"]);
            employee.OT = Convert.ToChar(dr_Designation["c_OT"]);
            employee.LOP = Convert.ToChar(dr_Designation["c_LOP"]);
            employee.PT = Convert.ToChar(dr_Designation["c_PT"]);
            employee.Print = Convert.ToChar(dr_Designation["c_Print"]);
            employee.payslip = Convert.ToChar(dr_Designation["payslip"]);
            employee.d_order = Convert.ToChar(dr_Designation["d_order"]);
            employee.Amount = 0;

            DesignationList.Add(employee);
        }
        _Connection.Close();
        return DesignationList;
    }


    public Collection<PayRoll> fn_Deduction1(int bid)
    {
        Collection<PayRoll> DeductionList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString = "select * from paym_Deduction where status='Y' and pn_BranchID='" + bid + "' order by d_order asc";
        SqlCommand _SSDeduction = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Deduction = _SSDeduction.ExecuteReader();
        while (dr_Deduction.Read())
        {
            PayRoll employee = new PayRoll();
            employee.DeductionId = (int)dr_Deduction["pn_DeductionId"];
            employee.DeducationCode = Convert.IsDBNull(dr_Deduction["v_DeductionCode"]) ? "" : (string)dr_Deduction["v_DeductionCode"];
            employee.DeductionName = Convert.IsDBNull(dr_Deduction["v_DeductionName"]) ? "" : (string)dr_Deduction["v_DeductionName"];
            employee.d_order = (int)dr_Deduction["d_order"];
            employee.regular = Convert.ToChar(dr_Deduction["c_Regular"]);
            employee.Amount = 0;

            DeductionList.Add(employee);
        }
        _Connection.Close();
        return DeductionList;
    }


    public Collection<PayRoll> fn_GetDeduction(PayRoll p)
    {
        Collection<PayRoll> DeductionList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString = "select * from paym_Deduction where status='Y' and pn_BranchID='" + p.BranchId + "' order by d_order asc";
        SqlCommand _SSDeduction = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Deduction = _SSDeduction.ExecuteReader();
        while (dr_Deduction.Read())
        {
            PayRoll employee = new PayRoll();
            employee.DeductionId = (int)dr_Deduction["pn_DeductionId"];
            employee.DeducationCode = Convert.IsDBNull(dr_Deduction["v_DeductionCode"]) ? "" : (string)dr_Deduction["v_DeductionCode"];
            employee.DeductionName = Convert.IsDBNull(dr_Deduction["v_DeductionName"]) ? "" : (string)dr_Deduction["v_DeductionName"];
            employee.d_order = (int)dr_Deduction["d_order"];
            employee.regular = Convert.ToChar(dr_Deduction["c_Regular"]);
            employee.Amount = 0;

            DeductionList.Add(employee);
        }
        _Connection.Close();
        return DeductionList;
    }

    public Collection<PayRoll> fn_Deduction(PayRoll p)
    {
        Collection<PayRoll> DeductionList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString = "select * from paym_Deduction where pn_CompanyID = '" + p.CompanyId + "' and pn_BranchID = '" + p.BranchId + "' and c_Regular='Y' and status='Y' order by d_order asc";
        SqlCommand _SSDeduction = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Deduction = _SSDeduction.ExecuteReader();
        while (dr_Deduction.Read())
        {
            PayRoll employee = new PayRoll();
            employee.DeductionId = (int)dr_Deduction["pn_DeductionId"];
            employee.DeducationCode = Convert.IsDBNull(dr_Deduction["v_DeductionCode"]) ? "" : (string)dr_Deduction["v_DeductionCode"];
            employee.DeductionName = Convert.IsDBNull(dr_Deduction["v_DeductionName"]) ? "" : (string)dr_Deduction["v_DeductionName"];
            employee.regular = Convert.ToChar(dr_Deduction["c_Regular"]);
            employee.Amount = 0;

            DeductionList.Add(employee);
        }
        _Connection.Close();
        return DeductionList;
    }

    public Collection<PayRoll> fn_Emp_Earnings(PayRoll e)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString;
        
        //_SqlString = "select a.v_EarningsCode,a.v_EarningsName,a.pn_EarningsID as  paym_Earnings,b.n_amount,b.c_eligible,b.pn_EarningsID as  paym_Emp_Earnings from ";
        //_SqlString = _SqlString + "(select v_EarningsCode,v_EarningsName,pn_EarningsID from  paym_Earnings where pn_EarningsID in(select pn_EarningsID from paym_Emp_Earnings where pn_CompanyId=" + e.CompanyId + " and pn_BranchID=" + e.BranchId + " and pn_EmployeeID=" + e.EmployeeId + ") ) a,";
        //_SqlString = _SqlString + "(select n_amount,c_eligible,pn_EarningsID from paym_Emp_Earnings where pn_CompanyId=" + e.CompanyId + " and pn_BranchID=" + e.BranchId + " and pn_EmployeeID=" + e.EmployeeId + ") b where a.pn_EarningsID=b.pn_EarningsID";
        
        _SqlString = "select a.v_EarningsCode,a.v_EarningsName,b.pn_EarningsID,b.n_amount,b.d_Date,b.c_eligible from paym_Emp_Earnings b,paym_Earnings a";
        _SqlString = _SqlString + " where b.pn_CompanyId=" + e.CompanyId + " and b.pn_BranchID=" + e.BranchId + " and b.pn_EmployeeID=" + e.EmployeeId + " and  a.pn_EarningsID=b.pn_EarningsID and b.c_eligible='Y' and b.flag = 'N'";
        
        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();
            employee.EarningsId = (int)dr_Designation["pn_EarningsID"];
            employee.EarningsCode = Convert.IsDBNull(dr_Designation["v_EarningsCode"]) ? "" : (string)dr_Designation["v_EarningsCode"];
            employee.EarningsName = Convert.IsDBNull(dr_Designation["v_EarningsName"]) ? "" : (string)dr_Designation["v_EarningsName"];
            employee.Amount = (int)dr_Designation["n_amount"];
            employee.regular = Convert.ToChar(dr_Designation["c_eligible"]);
            DesignationList.Add(employee);
        }
        _Connection.Close();
        return DesignationList;
    }

    public Collection<PayRoll> fn_Emp_EarningsAll(PayRoll e)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString;
        //_SqlString = "select a.v_EarningsCode,a.v_EarningsName,a.pn_EarningsID as  paym_Earnings,b.n_amount,b.c_eligible,b.pn_EarningsID as  paym_Emp_Earnings from ";
        //_SqlString = _SqlString + "(select v_EarningsCode,v_EarningsName,pn_EarningsID from  paym_Earnings where pn_EarningsID in(select pn_EarningsID from paym_Emp_Earnings where pn_CompanyId=" + e.CompanyId + " and pn_BranchID=" + e.BranchId + " and pn_EmployeeID=" + e.EmployeeId + ") ) a,";
        //_SqlString = _SqlString + "(select n_amount,c_eligible,pn_EarningsID from paym_Emp_Earnings where pn_CompanyId=" + e.CompanyId + " and pn_BranchID=" + e.BranchId + " and pn_EmployeeID=" + e.EmployeeId + ") b where a.pn_EarningsID=b.pn_EarningsID";

        _SqlString = "select a.v_EarningsCode,a.v_EarningsName,a.pn_EarningsID,b.n_amount,b.c_Eligible from paym_Emp_Earnings b right join (select * from paym_Earnings where pn_BranchID='" + e.BranchId + "' ) a on b.pn_EmployeeID='" + e.EmployeeId + "' and b.pn_BranchID='" + e.BranchId + "' and b.pn_CompanyID=a.pn_CompanyID and b.pn_EarningsID=a.pn_EarningsID and b.flag = 'N'";

        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();
            employee.EarningsId = (int)dr_Designation["pn_EarningsID"];
            employee.EarningsCode = Convert.IsDBNull(dr_Designation["v_EarningsCode"]) ? "" : (string)dr_Designation["v_EarningsCode"];
            employee.EarningsName = Convert.IsDBNull(dr_Designation["v_EarningsName"]) ? "" : (string)dr_Designation["v_EarningsName"];
            employee.Amount = Convert.IsDBNull(dr_Designation["n_amount"]) ? 0 : (int)dr_Designation["n_amount"];
            employee.regular = Convert.IsDBNull(dr_Designation["c_eligible"]) ? ' ' : Convert.ToChar(dr_Designation["c_eligible"]);
            DesignationList.Add(employee);
        }
        _Connection.Close();
        return DesignationList;
    }

    public Collection<PayRoll> fn_Emp_Deduction(PayRoll e)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString;  

        _SqlString = "select a.v_DeductionCode,a.v_DeductionName,b.pn_DeductionID,b.n_amount,b.d_Date,b.c_eligible from paym_Emp_Deduction b,paym_Deduction a";
        _SqlString = _SqlString + " where b.pn_CompanyId=" + e.CompanyId + " and b.pn_BranchID=" + e.BranchId + " and b.pn_EmployeeID=" + e.EmployeeId + " and  a.pn_DeductionID=b.pn_DeductionID and b.c_eligible='Y'";    
        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();
            employee.DeductionId = (int)dr_Designation["pn_DeductionID"];
            employee.DeducationCode = Convert.IsDBNull(dr_Designation["v_DeductionCode"]) ? "" : (string)dr_Designation["v_DeductionCode"];
            employee.DeductionName = Convert.IsDBNull(dr_Designation["v_DeductionName"]) ? "" : (string)dr_Designation["v_DeductionName"];
            employee.Amount = (double)dr_Designation["n_Amount"];
            employee.regular = Convert.ToChar(dr_Designation["c_eligible"]);
            DesignationList.Add(employee);
        }
        _Connection.Close();
        return DesignationList;
    }


    public Collection<PayRoll> fn_Emp_DeductionAll(PayRoll e)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString;

        //_SqlString = "select v_DeductionCode,v_DeductionName from paym_Deduction where DeductionID=" + e.DeductionId + " and status='Y'";
        //_SqlString = "select a.v_DeductionCode,a.v_DeductionName,a.pn_DeductionID as paym_Deduction,b.n_amount,b.c_eligible,b.pn_DeductionID as  paym_Emp_Deduction from ";
        //_SqlString = _SqlString + "(select v_DeductionCode,v_DeductionName,pn_DeductionID from paym_Deduction where pn_DeductionID in(select pn_DeductionID from paym_Emp_Deduction where pn_CompanyId=" + e.CompanyId + " and pn_BranchID=" + e.BranchId + " and pn_EmployeeID=" + e.EmployeeId + ") ) a,";
        //_SqlString = _SqlString + "(select n_amount,c_eligible,pn_DeductionID from paym_Emp_Deduction where pn_CompanyId=" + e.CompanyId + " and pn_BranchID=" + e.BranchId + " and pn_EmployeeID=" + e.EmployeeId + ") b where a.pn_DeductionID=b.pn_DeductionID";

        _SqlString = "select a.v_DeductionCode,a.v_DeductionName,a.pn_DeductionID,a.c_regular,b.n_amount,b.c_Eligible from paym_Emp_Deduction b right join (select * from paym_Deduction where pn_BranchID='" + e.BranchId + "') a on b.pn_EmployeeID='" + e.EmployeeId + "' and b.pn_BranchID='" + e.BranchId + "' and b.pn_CompanyID=a.pn_CompanyID and b.pn_DeductionID=a.pn_DeductionID";

        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();
            employee.DeductionId = (int)dr_Designation["pn_DeductionID"];
            employee.DeducationCode = Convert.IsDBNull(dr_Designation["v_DeductionCode"]) ? "" : (string)dr_Designation["v_DeductionCode"];
            employee.DeductionName = Convert.IsDBNull(dr_Designation["v_DeductionName"]) ? "" : (string)dr_Designation["v_DeductionName"];
            employee.Amount = Convert.IsDBNull(dr_Designation["n_Amount"]) ? 0 : (double)dr_Designation["n_Amount"];
            employee.eligible = Convert.IsDBNull(dr_Designation["c_eligible"]) ? ' ' : Convert.ToChar(dr_Designation["c_eligible"]);
            employee.regular = Convert.IsDBNull(dr_Designation["c_regular"]) ? ' ' : Convert.ToChar(dr_Designation["c_regular"]);
            DesignationList.Add(employee);
        }
        _Connection.Close();
        return DesignationList;
    }


    public Collection<PayRoll> fn_NonEarnings()
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString = "select * from paym_Earnings where c_Regular='N' and status='Y'";
        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();
            employee.EarningsId = (int)dr_Designation["pn_EarningsID"];
            employee.EarningsCode = Convert.IsDBNull(dr_Designation["v_EarningsCode"]) ? "" : (string)dr_Designation["v_EarningsCode"];
            employee.EarningsName = Convert.IsDBNull(dr_Designation["v_EarningsName"]) ? "" : (string)dr_Designation["v_EarningsName"];
            employee.regular = Convert.ToChar(dr_Designation["c_Regular"]);
            employee.Pf = Convert.ToChar(dr_Designation["c_PF"]);
            employee.Esi = Convert.ToChar(dr_Designation["c_ESI"]);
            employee.Amount = 0;

            DesignationList.Add(employee);
        }
        _Connection.Close();
        return DesignationList;
    }

    public Collection<PayRoll> fn_NonEarnings1(int bid)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString = "select * from paym_Earnings where pn_branchid = '"+bid+"' and c_Regular='N' and status='Y'";
        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();
            employee.EarningsId = (int)dr_Designation["pn_EarningsID"];
            employee.EarningsCode = Convert.IsDBNull(dr_Designation["v_EarningsCode"]) ? "" : (string)dr_Designation["v_EarningsCode"];
            employee.EarningsName = Convert.IsDBNull(dr_Designation["v_EarningsName"]) ? "" : (string)dr_Designation["v_EarningsName"];
            employee.regular = Convert.ToChar(dr_Designation["c_Regular"]);
            employee.Pf = Convert.ToChar(dr_Designation["c_PF"]);
            employee.Esi = Convert.ToChar(dr_Designation["c_ESI"]);
            employee.Amount = 0;

            DesignationList.Add(employee);
        }
        _Connection.Close();
        return DesignationList;
    }

    public Collection<PayRoll> fn_NonDeduction()
    {
        Collection<PayRoll> DeductionList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString = "select * from paym_Deduction where c_Regular='N' and status='Y'";
        SqlCommand _SSDeduction = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Deduction = _SSDeduction.ExecuteReader();
        while (dr_Deduction.Read())
        {
            PayRoll employee = new PayRoll();
            employee.DeductionId = (int)dr_Deduction["pn_DeductionId"];
            employee.DeducationCode = Convert.IsDBNull(dr_Deduction["v_DeductionCode"]) ? "" : (string)dr_Deduction["v_DeductionCode"];
            employee.DeductionName = Convert.IsDBNull(dr_Deduction["v_DeductionName"]) ? "" : (string)dr_Deduction["v_DeductionName"];
            employee.regular = Convert.ToChar(dr_Deduction["c_Regular"]);
            employee.Amount = 0;

            DeductionList.Add(employee);
        }
        _Connection.Close();
        return DeductionList;
    }


    public Collection<PayRoll> fn_In_PayInput(PayRoll p)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString = "select * from PayInput where pn_EmployeeID=" + p.EmployeeId + " and d_date='" + p.strDate + "'";
        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll pr = new PayRoll();
            pr.CompanyId = (int)dr_Designation["pn_companyid"];
            pr.EmployeeId = (int)dr_Designation["pn_EmployeeID"];
            pr.d_date = (DateTime)dr_Designation["d_Date"];
            pr.d_FromDate = (DateTime)dr_Designation["d_From_Date"];
            pr.d_ToDate = (DateTime)dr_Designation["d_To_Date"];
            pr.Calc_Days = (double)dr_Designation["Calc_Days"];
            pr.Paid_Days = (double)dr_Designation["Paid_Days"];
            pr.Present_Days = (double)dr_Designation["Present_Days"];
            pr.Absent_Days = (double)dr_Designation["Absent_Days"];
            pr.TotLeave_Days = (double)dr_Designation["TotLeave_Days"];
            pr.WeekOffDays = (double)dr_Designation["WeekOffDays"];
            pr.Holidays = (double)dr_Designation["Holidays"];
            pr.OnDuty_days = (double)dr_Designation["OnDuty_days"];
            pr.Compoff_Days = (double)dr_Designation["Compoff_Days"];
            pr.Tour_Days = (double)dr_Designation["Tour_Days"];
            pr.Att_Bonus = Convert.ToString(dr_Designation["Att_Bonus"]);
            pr.Att_BonusAmount = (double)dr_Designation["Att_BonusAmount"];
            pr.OT_HRS = (double)dr_Designation["ot_value"];
            pr.Earn_Arrears = (double)dr_Designation["Earn_Arrears"];
            pr.Ded_Arrears = (double)dr_Designation["Ded_Arrears"];

            DesignationList.Add(pr);

        }
        _Connection.Close();
        return DesignationList;
    }


    public Collection<PayRoll> fn_In_Earnings(PayRoll pr)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString;

        _SqlString = "select ee.pn_EarningsID,ee.n_Amount,ee.c_eligible,me.v_EarningsCode,me.c_PF,me.c_ESI,me.c_OT,me.c_LOP,me.c_PT";
        _SqlString = _SqlString + " from paym_Emp_Earnings ee,paym_Earnings me where ee.pn_CompanyID=" + pr.CompanyId + " and ee.pn_EmployeeID=" + pr.EmployeeId + "";
        _SqlString = _SqlString + " and me.pn_EarningsID=ee.pn_EarningsID and me.c_regular='Y' and ee.c_eligible='Y' and ee.flag= 'N' order by me.d_order asc";

        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();
            employee.EarningsId = (int)dr_Designation["pn_EarningsID"];
            employee.EarningsCode = dr_Designation["v_EarningsCode"].ToString();
            employee.Amount = (int)dr_Designation["n_amount"];
            employee.eligible = Convert.ToChar(dr_Designation["c_eligible"]);
            employee.Pf = Convert.ToChar(dr_Designation["c_PF"]);
            employee.Esi = Convert.ToChar(dr_Designation["c_ESI"]);
            employee.OT = Convert.ToChar(dr_Designation["c_OT"]);
            employee.LOP = Convert.ToChar(dr_Designation["c_LOP"]);
            employee.PT = Convert.ToChar(dr_Designation["c_PT"]);
            DesignationList.Add(employee);
        }
        _Connection.Close();
        return DesignationList;
    }

    public Collection<PayRoll> fn_Check_Earnings(PayRoll pr)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString;

        _SqlString = "select * from payoutput_earnings where pn_companyid = '" + pr.CompanyId + "' and pn_branchid = '" + pr.BranchId + "' and pn_EmployeeID = '" + pr.EmployeeId + "' and d_Date = '" + pr.strDate + "' and mode !='a'";

        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();
            employee.EarningsId = (int)dr_Designation["pn_EarningsID"];
            employee.EmployeeId = (int)dr_Designation["pn_EmployeeID"];
            employee.Amount = (double)dr_Designation["Amount"];
            DesignationList.Add(employee);
        }
        _Connection.Close();
        return DesignationList;
    }

    public Collection<PayRoll> fn_Check_Earnings_ID(PayRoll pr)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString;
        _SqlString = "select * from payoutput_earnings where  pn_companyid = '" + pr.CompanyId + "' and pn_branchid = '" + pr.BranchId + "' and pn_EarningsID = '" + pr.EarningsId + "' and pn_EmployeeID = '" + pr.EmployeeId + "' and d_Date = '" + pr.strDate + "' and mode ='a'";
        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();
            employee.EarningsId = (int)dr_Designation["pn_EarningsID"];
            employee.EmployeeId = (int)dr_Designation["pn_EmployeeID"];
            employee.Amount = (double)dr_Designation["Amount"];
            DesignationList.Add(employee);
        }
        return DesignationList;
    }

    public Collection<PayRoll> fn_Check_Non_Earnings(PayRoll pr)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString;

        _SqlString = "select ee.pn_EarningsID,ee.Amount,me.v_EarningsCode,me.c_PF,me.c_ESI,me.c_OT,me.c_LOP,me.c_PT from payoutput_Earnings ee,paym_Earnings me where ee.pn_CompanyID=1 and me.pn_EarningsID=ee.pn_EarningsID and ee.flag= 'S' and ee.mode != 'I' and ee.d_date='" + pr.strDate + "' and ee.pn_Employeeid = " + pr.EmployeeId + " order by me.d_order asc;";
        

        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();
            employee.EarningsId = (int)dr_Designation["pn_EarningsID"];
            employee.EarningsCode = dr_Designation["v_EarningsCode"].ToString();
            employee.Amount = (double)dr_Designation["amount"];
            employee.Pf = Convert.ToChar(dr_Designation["c_PF"]);
            employee.Esi = Convert.ToChar(dr_Designation["c_ESI"]);
            employee.OT = Convert.ToChar(dr_Designation["c_OT"]);
            employee.LOP = Convert.ToChar(dr_Designation["c_LOP"]);
            employee.PT = Convert.ToChar(dr_Designation["c_PT"]);


            DesignationList.Add(employee);

        }
        _Connection.Close();
        return DesignationList;
    }

    public Collection<PayRoll> fn_Check_Earnings_Change(PayRoll pr)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString;

        _SqlString = "select ee.pn_EarningsID,ee.Amount,me.v_EarningsCode,me.c_PF,me.c_ESI,me.c_OT,me.c_LOP,me.c_PT from payoutput_Earnings ee,paym_Earnings me where ee.pn_CompanyID='" + pr.CompanyId + "' and ee.pn_BranchID = '" + pr.BranchId + "' and me.pn_EarningsID=ee.pn_EarningsID and ee.d_date='" + pr.strDate + "' and ee.pn_Employeeid = " + pr.EmployeeId + " order by me.d_order asc;";
        //_SqlString = "select * from payoutput_earnings where  pn_companyid = '" + pr.CompanyId + "' and pn_branchid = '" + pr.BranchId + "' and pn_EmployeeID = '" + pr.EmployeeId + "' and d_Date = '" + pr.strDate + "'";

        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();
            //employee.EarningsId = (int)dr_Designation["pn_EarningsID"];
            //employee.EmployeeId = (int)dr_Designation["pn_EmployeeID"];
            //employee.Amount = (double)dr_Designation["Amount"];
            employee.EarningsId = (int)dr_Designation["pn_EarningsID"];
            employee.EarningsCode = dr_Designation["v_EarningsCode"].ToString();
            employee.Amount = (double)dr_Designation["amount"];
            employee.Pf = Convert.ToChar(dr_Designation["c_PF"]);
            employee.Esi = Convert.ToChar(dr_Designation["c_ESI"]);
            employee.OT = Convert.ToChar(dr_Designation["c_OT"]);
            employee.LOP = Convert.ToChar(dr_Designation["c_LOP"]);
            employee.PT = Convert.ToChar(dr_Designation["c_PT"]);

            DesignationList.Add(employee);

        }
        _Connection.Close();
        return DesignationList;
    }

    public Collection<PayRoll> fn_In_Earnings_Slab(PayRoll pr)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString;

        _SqlString = "set dateformat dmy;select ee.pn_EarningsID,ee.n_Amount,ee.c_eligible,me.v_EarningsCode,me.c_PF,me.c_ESI,me.c_OT,me.c_LOP,me.c_PT";
        _SqlString = _SqlString + " from paym_Emp_Earnings ee,paym_Earnings me where ee.pn_CompanyID=" + pr.CompanyId + " and ee.pn_EmployeeID=" + pr.EmployeeId + "";
        _SqlString = _SqlString + " and me.pn_EarningsID=ee.pn_EarningsID and me.c_regular='Y' and ee.c_eligible='Y' and ee.flag= 'Y' and ee.from_date<='" + pr.d_date + "' and ee.to_date>='" + pr.d_date + "' order by me.d_order asc; set dateformat mdy;";

        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();
            employee.EarningsId = (int)dr_Designation["pn_EarningsID"];
            employee.EarningsCode = dr_Designation["v_EarningsCode"].ToString();
            employee.Amount = (int)dr_Designation["n_amount"];
            employee.eligible = Convert.ToChar(dr_Designation["c_eligible"]);
            employee.Pf = Convert.ToChar(dr_Designation["c_PF"]);
            employee.Esi = Convert.ToChar(dr_Designation["c_ESI"]);
            employee.OT = Convert.ToChar(dr_Designation["c_OT"]);
            employee.LOP = Convert.ToChar(dr_Designation["c_LOP"]);
            employee.PT = Convert.ToChar(dr_Designation["c_PT"]);

            DesignationList.Add(employee);

        }
        _Connection.Close();
        return DesignationList;
    }

    public Collection<PayRoll> fn_In_Non_Earnings(PayRoll npr)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString;
        //_SqlString = "select top 2 ee.pn_EarningsID,ee.n_Amount,ee.c_eligible,me.c_PF,me.c_ESI,me.c_OT,me.c_LOP,me.c_PT";
        //_SqlString = _SqlString + " from paym_Emp_Earnings ee,paym_Earnings me where ee.pn_CompanyID=" + npr.CompanyId + " and ee.pn_EmployeeID=" + npr.EmployeeId + "";
        //_SqlString = _SqlString + " and ee.d_Date<='" + npr.d_date + "' and me.pn_EarningsID=ee.pn_EarningsID and me.c_regular='N' order by ee.d_Date desc";

        _SqlString = "set dateformat dmy;select ee.pn_EarningsID,ee.n_Amount,ee.c_eligible,me.c_PF,me.c_ESI,me.c_OT,me.c_LOP,me.c_PT";
        _SqlString = _SqlString + " from paym_Emp_Earnings ee,paym_Earnings me where ee.pn_CompanyID=" + npr.CompanyId + " and ee.pn_EmployeeID=" + npr.EmployeeId + "";
        _SqlString = _SqlString + " and me.pn_EarningsID=ee.pn_EarningsID and me.c_regular='N' and ee.c_Eligible = 'N' and ee.c_eligible='Y' and ee.flag='Y'  and ee.from_date<='" + npr.d_date + "' and ee.to_date>='" + npr.d_date + "' order by me.d_order asc; set dateformat mdy;";
        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();
            employee.EarningsId = (int)dr_Designation["pn_EarningsID"];
            employee.Amount = (int)dr_Designation["n_amount"];
            employee.eligible = Convert.ToChar(dr_Designation["c_eligible"]);
            employee.Pf = Convert.ToChar(dr_Designation["c_PF"]);
            employee.Esi = Convert.ToChar(dr_Designation["c_ESI"]);
            employee.OT = Convert.ToChar(dr_Designation["c_OT"]);
            employee.LOP = Convert.ToChar(dr_Designation["c_LOP"]);
            employee.PT = Convert.ToChar(dr_Designation["c_PT"]);
            DesignationList.Add(employee);
        }
        _Connection.Close();
        return DesignationList;
    }


    public Collection<PayRoll> fn_In_Deduction(PayRoll e)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString;

        _SqlString = "Set dateformat dmy;select ed.pn_DeductionID,ed.n_Amount,ed.c_eligible,md.v_DeductionCode,md.c_Print from paym_Emp_Deduction ed,";
        _SqlString = _SqlString + " paym_Deduction md where ed.pn_CompanyID=" + e.CompanyId + " and ed.pn_EmployeeID=" + e.EmployeeId + " ";
        _SqlString = _SqlString + " and md.pn_DeductionID=ed.pn_DeductionID and md.c_regular='Y' order by md.d_order asc;Set dateformat mdy;";

        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();
            employee.DeductionId = (int)dr_Designation["pn_DeductionID"];
            employee.Amount = Convert.ToDouble(dr_Designation["n_Amount"]);
            employee.DeducationCode = (string)dr_Designation["v_DeductionCode"];
            employee.eligible = Convert.ToChar(dr_Designation["c_eligible"]);

            DesignationList.Add(employee);
        }
        _Connection.Close();
        return DesignationList;
    }

    public Collection<PayRoll> fn_Check_Deductions(PayRoll pr)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString;

        _SqlString = "select * from payoutput_deductions where  pn_companyid = '" + pr.CompanyId + "' and pn_branchid = '" + pr.BranchId + "' and pn_EmployeeID = '" + pr.EmployeeId + "' and d_Date = '" + pr.strDate + "'  and mode !='a'";

        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();
            employee.DeductionId = (int)dr_Designation["pn_DeductionID"];
            employee.EmployeeId = (int)dr_Designation["pn_EmployeeID"];
            employee.Amount = (double)dr_Designation["Amount"];

            DesignationList.Add(employee);

        }
        _Connection.Close();
        return DesignationList;
    }

    public Collection<PayRoll> fn_Check_Non_Deduction(PayRoll pr)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString;

        _SqlString = "select ee.pn_DeductionID,ee.Amount,me.v_DeductionCode from payoutput_Deductions ee,paym_Deduction me where ee.pn_CompanyID=1 and me.pn_DeductionID=ee.pn_DeductionID and ee.flag= 'S' and ee.d_date='" + pr.strDate + "' and ee.pn_Employeeid = " + pr.EmployeeId + " order by me.d_order asc;";

        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();
            employee.DeductionId = (int)dr_Designation["pn_DeductionID"];
            employee.DeducationCode = dr_Designation["v_DeductionCode"].ToString();
            employee.Amount = (double)dr_Designation["Amount"];
            DesignationList.Add(employee);
        }
        _Connection.Close();
        return DesignationList;
    }

    public Collection<PayRoll> fn_In_Non_Deduction(PayRoll e)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString;

        _SqlString = "Set dateformat dmy;select ed.pn_DeductionID,ed.n_Amount,md.v_DeductionCode,ed.c_eligible,md.c_Print from paym_Emp_Deduction ed,";
        _SqlString = _SqlString + " paym_Deduction md where ed.pn_CompanyID=" + e.CompanyId + " and ed.pn_EmployeeID=" + e.EmployeeId + "";
        _SqlString = _SqlString + " and md.pn_DeductionID=ed.pn_DeductionID and md.c_regular='N' and ed.c_eligible = 'Y' order by md.d_order asc;Set dateformat mdy;";

        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();
            employee.DeductionId = (int)dr_Designation["pn_DeductionID"];
            employee.DeducationCode = (string)dr_Designation["v_DeductionCode"];
            employee.Amount = (double)dr_Designation["n_Amount"];
            employee.eligible = Convert.ToChar(dr_Designation["c_eligible"]);

            DesignationList.Add(employee);
        }
        _Connection.Close();
        return DesignationList;
    }

    public Collection<PayRoll> fn_In_PF(PayRoll pro)
    {
        Collection<PayRoll> DeductionList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        //string _SqlString = "select top 1 * from paym_PF where pn_CompanyID=" + pro.CompanyId + " and d_date<='" + pro.d_ToDate + "' order by d_date desc";
        string _SqlString = "select top 1 * from paym_PF where pn_CompanyID=" + pro.CompanyId + " and pn_BranchID=" + pro.BranchId + "  order by d_date desc";
        SqlCommand _SSDeduction = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Deduction = _SSDeduction.ExecuteReader();
        while (dr_Deduction.Read())
        {
            PayRoll pr = new PayRoll();
            pr.CompanyId = (int)dr_Deduction["pn_CompanyID"];
            pr.Emp_Con_PF = (double)dr_Deduction["Emp_Con_PF"];
            pr.Emp_Con_EPF = (double)dr_Deduction["Emp_Con_EPF"];
            pr.Emp_Con_FPF = (double)dr_Deduction["Emp_Con_FPF"];
            pr.charges = (double)dr_Deduction["Admin_Charges"];
            pr.Amount = (double)dr_Deduction["Eligibility_Amt"];
            pr.c_Round = Convert.ToChar(dr_Deduction["c_Round"]);
            pr.d_date = Convert.ToDateTime(dr_Deduction["d_date"]);

            DeductionList.Add(pr);
        }
        _Connection.Close();
        return DeductionList;
    }


    public Collection<PayRoll> fn_getECR(string qry)
    {
        Collection<PayRoll> emp_ecr = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        SqlCommand cmd = new SqlCommand(qry, _Connection);
        _Connection.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            PayRoll pay = new PayRoll();
            pay.PF_No = Convert.ToString(dr["PFno"]);
            pay.FirstName = Convert.ToString(dr["Employee_First_Name"]);
            pay.Gross_Salary = Convert.IsDBNull(dr["NetPay"]) ? 0 : (int)dr["NetPay"];
            pay.Emp_Con_PF = Convert.IsDBNull(dr["PF"]) ? 0 : (double)dr["PF"];
            pay.Emp_Con_EPF = Convert.IsDBNull(dr["EPF"]) ? 0 : (double)dr["EPF"];
            pay.Emp_Con_FPF = Convert.IsDBNull(dr["FPF"]) ? 0 : (double)dr["FPF"];
            emp_ecr.Add(pay);

        }
        return emp_ecr;
    }

    public Collection<PayRoll> fn_getESI(string qry)
    {
        Collection<PayRoll> emp_ecr = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        SqlCommand cmd = new SqlCommand(qry, _Connection);
        _Connection.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            PayRoll pay = new PayRoll();
            pay.ESI_No = Convert.ToString(dr["ESIno"]);
            pay.FirstName = Convert.ToString(dr["Employee_First_Name"]);
            pay.Gross_Salary = Convert.IsDBNull(dr["NetPay"]) ? 0 : (int)dr["NetPay"];
            pay.Paid_Days = Convert.IsDBNull(dr["Paid_Days"]) ? 0 : (double)dr["Paid_Days"];
            emp_ecr.Add(pay);

        }
        else
        {
            PayRoll pay = new PayRoll();
            pay.ESI_No = "0";
            pay.FirstName = "";
            pay.Gross_Salary = 0;
            pay.Paid_Days = 0;
            emp_ecr.Add(pay);
        }
        return emp_ecr;
    }

    public Collection<PayRoll> fn_In_ESI(PayRoll e)
    {
        Collection<PayRoll> DeductionList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        //string _SqlString = "select top 1 * from paym_ESI where pn_CompanyID=" + e.CompanyId + " and d_date<='" + e.d_ToDate + "' order by d_date desc";
        string _SqlString = "select top 1 * from paym_ESI where pn_CompanyID=" + e.CompanyId + " and pn_BranchID=" + e.BranchId + " order by d_date desc";
        SqlCommand _SSDeduction = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Deduction = _SSDeduction.ExecuteReader();
        while (dr_Deduction.Read())
        {
            PayRoll pr = new PayRoll();
            pr.CompanyId = (int)dr_Deduction["pn_CompanyID"];
            pr.Emp_Con = (double)dr_Deduction["Employee_Con"];
            pr.Employer_Con = (double)dr_Deduction["Employer_Con"];
            //pr.charges = (double)dr_Deduction["Admin_Charges"];
            pr.Amount = (double)dr_Deduction["Eligibility_Amt"];
            pr.c_Round = Convert.ToChar(dr_Deduction["c_Round"]);
            pr.d_date = Convert.ToDateTime(dr_Deduction["d_date"]);

            DeductionList.Add(pr);
        }
        _Connection.Close();
        return DeductionList;
    }

    public Collection<PayRoll> fn_In_OT(PayRoll e)
    {
        Collection<PayRoll> DeductionList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString = "select * from paym_Earnings where pn_CompanyID=" + e.CompanyId + " and pn_BranchID=" + e.BranchId + " and v_EarningsCode='OT'";
        SqlCommand _SSDeduction = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Deduction = _SSDeduction.ExecuteReader();
        while (dr_Deduction.Read())
        {
            PayRoll pr = new PayRoll();
            pr.EarningsId = (int)dr_Deduction["pn_EarningsID"];
            DeductionList.Add(pr);
        }
        _Connection.Close();
        return DeductionList;
    }


    public Collection<PayRoll> fn_In_OTSlab(PayRoll e)
    {
        Collection<PayRoll> DeductionList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        //string _SqlString = "select Pro_Tax_Amt from paym_PT where pn_CompanyID=" + e.CompanyId + " and " + e.F_Amount + " between From_Amt and To_Amt";
        string _SqlString = "select * from otslab where pn_CompanyID=" + e.CompanyId + " and pn_BranchID=" + e.BranchId + " and pn_category = '" + e.CategoryID + "' and '" + e.fromtime + "' between ot_from and ot_to";
        SqlCommand _SSDeduction = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Deduction = _SSDeduction.ExecuteReader();
        while (dr_Deduction.Read())
        {
            PayRoll pr = new PayRoll();
            pr.ID = (int)dr_Deduction["slab_id"];
            DeductionList.Add(pr);
        }
        return DeductionList;
    }

    public Collection<PayRoll> fn_In_PT(PayRoll e)
    {
        Collection<PayRoll> DeductionList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        //string _SqlString = "select Pro_Tax_Amt from paym_PT where pn_CompanyID=" + e.CompanyId + " and " + e.F_Amount + " between From_Amt and To_Amt";
        string _SqlString = "select Pro_Tax_Amt from paym_PT where pn_CompanyID=" + e.CompanyId + " and pn_BranchID=" + e.BranchId + " and " + e.F_Amount + " between From_Amt and To_Amt";
        SqlCommand _SSDeduction = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Deduction = _SSDeduction.ExecuteReader();
        while (dr_Deduction.Read())
        {
            PayRoll pr = new PayRoll();
            //pr.CompanyId = (int)dr_Deduction["pn_CompanyID"];
            //pr. = (double)dr_Deduction["From_Amt"];
            //pr.T_Amount = (double)dr_Deduction["To_Amt"];
            pr.T_Amount = (double)dr_Deduction["Pro_Tax_Amt"];

            DeductionList.Add(pr);
        }
        _Connection.Close();
        return DeductionList;
    }


    public Collection<PayRoll> fn_In_LoanEntry(PayRoll e)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString;

        _SqlString = "select *,d_effdate as effectivedate from LoanEntry where pn_CompanyID=" + e.CompanyId + " and pn_EmployeeID=" + e.EmployeeId + " and d_effdate<='" + e.d_ToDate + "' and c_status='Y'";

        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();
            employee.loanid = (int)dr_Designation["fn_LoanID"];
            //employee.F_Amount = (double)dr_Designation["Loan_Amt"];
            //employee.T_Amount = (double)dr_Designation["InstalmentAmt"];
            employee.Amount = (int)dr_Designation["Loan_Amt"];
            employee.F_Amount = (double)dr_Designation["InstalmentAmt"];
            employee.installmentcount = (int)dr_Designation["Instalmentcount"];
            employee.balanceamount = (double)dr_Designation["Balance_Amt"];
            employee.d_EffDate = (DateTime)dr_Designation["d_effdate"];

            DesignationList.Add(employee);
        }
        _Connection.Close();
        return DesignationList;
    }

    public Collection<PayRoll> fn_In_LoanCancel(PayRoll e)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString;

        _SqlString = "select * from Loan_Cancel where pn_CompanyID=" + e.CompanyId + " and pn_EmployeeID=" + e.EmployeeId + " and fn_LoanID=" + e.loanid + " and d_date<='" + e.d_date + "'";

        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();
            employee.loanid = (int)dr_Designation["fn_LoanID"];
            DesignationList.Add(employee);
        }
        _Connection.Close();
        return DesignationList;
    }

    public Collection<PayRoll> fn_In_LoanPreCloser(PayRoll e)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString;

        _SqlString = "select * from Loan_PreCloser where pn_CompanyID=" + e.CompanyId + " and pn_EmployeeID=" + e.EmployeeId + "";
        _SqlString = _SqlString + " and fn_LoanID=" + e.loanid + " and d_date<='" + e.d_ToDate + "' and c_status='Y'";

        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();
            employee.balanceamount = (double)dr_Designation["n_balanceamount"];
            employee.loanid = (int)dr_Designation["fn_LoanID"];
            employee.d_EffDate = (DateTime)dr_Designation["d_date"];
            DesignationList.Add(employee);
        }
        _Connection.Close();
        return DesignationList;
    }

    public Collection<PayRoll> fn_In_PayOutput_Loan(PayRoll e)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString;

        _SqlString = "select * from PayOutput_Loan where pn_CompanyID=" + e.CompanyId + " and pn_EmployeeID=" + e.EmployeeId + " and d_To_Date='" + e.d_date + "'";

        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();

            employee.T_Amount = (double)dr_Designation["Amount"];
            DesignationList.Add(employee);
        }
        _Connection.Close();
        return DesignationList;
    }



    public Collection<PayRoll> fn_In_Common_Settings(PayRoll pro)
    {
        Collection<PayRoll> DeductionList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString = "select * from PayOutput_Settings where pn_CompanyID=" + pro.CompanyId + " and Common_Flag='Y'";
        SqlCommand _SSDeduction = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Deduction = _SSDeduction.ExecuteReader();
        while (dr_Deduction.Read())
        {
            PayRoll pr = new PayRoll();

            pr.status = Convert.ToChar(dr_Deduction["Month_Calc"]);
            pr.Calc_Days = (double)dr_Deduction["Calc_Days"];
            pr.Week_Holiday1 = Convert.IsDBNull(dr_Deduction["Week_Holiday1"]) ? "" : (string)dr_Deduction["Week_Holiday1"];
            pr.Week_Holiday2 = Convert.IsDBNull(dr_Deduction["Week_Holiday2"]) ? "" : (string)dr_Deduction["Week_Holiday1"];

            DeductionList.Add(pr);
        }
        _Connection.Close();
        return DeductionList;
    }

    public Collection<PayRoll> fn_In_OT_Settings(PayRoll pro)
    {
        Collection<PayRoll> DeductionList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString = "select * from Payminput_Settings where pn_CompanyID=" + pro.CompanyId + " and OT_Flag='Y'";
        SqlCommand _SSDeduction = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Deduction = _SSDeduction.ExecuteReader();
        while (dr_Deduction.Read())
        {
            PayRoll pr = new PayRoll();

            pr.OT_Days = (double)dr_Deduction["OT_Days"];
            pr.OT_HRS = (double)dr_Deduction["OT_Hours"];

            DeductionList.Add(pr);
        }
        _Connection.Close();
        return DeductionList;
    }


    public Collection<PayRoll> fn_In_OT_Settings1(PayRoll pro)
    {
        Collection<PayRoll> DeductionList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString = "select * from attendance_ceiling where pn_CompanyID=" + pro.CompanyId + " and pn_BranchID = '" + pro.BranchId + "'";
        SqlCommand _SSDeduction = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Deduction = _SSDeduction.ExecuteReader();
        while (dr_Deduction.Read())
        {
            PayRoll pr = new PayRoll();

            pr.OT_Days = (double)dr_Deduction["OT_Days"];
            pr.OT_HRS = (double)dr_Deduction["OT_Hrs"];

            DeductionList.Add(pr);
        }
        _Connection.Close();
        return DeductionList;
    }

    public Collection<PayRoll> fn_In_PT_Settings(PayRoll pro)
    {
        Collection<PayRoll> DeductionList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString = "select * from payminput_settings where pn_CompanyID=" + pro.CompanyId + " and PT_Flag='Y'";
        SqlCommand _SSDeduction = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Deduction = _SSDeduction.ExecuteReader();
        while (dr_Deduction.Read())
        {
            PayRoll pr = new PayRoll();

            pr.PT = Convert.ToChar(dr_Deduction["PT_Type"]);
            pr.PTmonth = (string)dr_Deduction["PT_Months"];
            //pr.temp_str = (string)dr_Deduction["PT_Months"];

            DeductionList.Add(pr);
        }
        _Connection.Close();
        return DeductionList;
    }

    public Collection<PayRoll> fn_In_PTax_Settings(PayRoll pro)
    {
        Collection<PayRoll> DeductionList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString = "select * from attendance_ceiling where pn_CompanyID=" + pro.CompanyId + " and pn_BranchID='" + pro.BranchId + "'";
        SqlCommand _SSDeduction = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Deduction = _SSDeduction.ExecuteReader();
        while (dr_Deduction.Read())
        {
            PayRoll pr = new PayRoll();

            //pr.PT = Convert.ToChar(dr_Deduction["PT_Type"]);
            pr.PTmonth = (string)dr_Deduction["ptax_month"];
            //pr.temp_str = (string)dr_Deduction["PT_Months"];

            DeductionList.Add(pr);
        }
        _Connection.Close();
        return DeductionList;
    }

    public Collection<PayRoll> fn_In_LWF_Settings(PayRoll pro)
    {
        Collection<PayRoll> DeductionList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString = "select * from payminput_settings where pn_CompanyID=" + pro.CompanyId + " and LWF_Flag='Y'";
        SqlCommand _SSDeduction = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Deduction = _SSDeduction.ExecuteReader();
        while (dr_Deduction.Read())
        {
            PayRoll pr = new PayRoll();

            pr.LWF_Limit = (int)dr_Deduction["LWF_Limit"];
            pr.LWF_Amt = (float)dr_Deduction["LWF_Amt"];
            pr.LWF_Month = (int)dr_Deduction["LWF_Month"];


            DeductionList.Add(pr);
        }
        _Connection.Close();
        return DeductionList;
    }


   public Collection<PayRoll> fn_In_Bonus_Settings(PayRoll pro)
    {
        Collection<PayRoll> DeductionList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString = "select * from PayOutput_Settings where pn_CompanyID=" + pro.CompanyId + " and Bonus_Flag='Y'";
        SqlCommand _SSDeduction = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Deduction = _SSDeduction.ExecuteReader();
        while (dr_Deduction.Read())
        {
            PayRoll pr = new PayRoll();

            pr.F_Month = (int)dr_Deduction["Bonus_From_Month"];
            pr.T_Month = (int)dr_Deduction["Bonus_To_Month"];
            pr.status = Convert.ToChar(dr_Deduction["Bonus_Type"]);
            pr.Amount = (int)dr_Deduction["Bonus_Limit"];

            DeductionList.Add(pr);
        }
        _Connection.Close();
        return DeductionList;
    }


    public Collection<PayRoll> fn_Out_PayOutput_Netpay(PayRoll e)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString;

        _SqlString = "select * from PayOutput_Netpay where pn_CompanyID=" + e.CompanyId + " and pn_BranchID= '" + e.BranchId + "' and d_Date='" + e.strDate + "'";

        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();
            employee.T_Amount = (double)dr_Designation["Net_salary"];
            DesignationList.Add(employee);
        }
        _Connection.Close();
        return DesignationList;
    }

    public Collection<PayRoll> fn_Out_PayOutput_Netpay_Employee(PayRoll e)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString;

        _SqlString = "select * from PayOutput_Netpay where pn_CompanyID=" + e.CompanyId + " and pn_BranchID= '" + e.BranchId + "' and d_Date='" + e.strDate + "' and pn_EmployeeID = '" + e.EmployeeId + "'";

        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();
            employee.T_Amount = (double)dr_Designation["Net_salary"];
            DesignationList.Add(employee);
        }
        _Connection.Close();
        return DesignationList;
    }


    public void fn_Salary_Register(PayRoll e)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString;

        _SqlString = "insert into TempRegister select * from paym_paybill where pn_CompanyID=" + e.CompanyId + " and pn_BranchID= '" + e.BranchId + "' and d_Date >='" + e.DurationFrom + "' and d_Date <= '" + e.DurationTo + "' and pn_EmployeeID = '" + e.EmployeeId + "' order by d_Date asc";

        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        _SSDesignation.ExecuteNonQuery();
        _Connection.Close();
    }

    public Collection<PayRoll> fn_Out_PayOutput_Earnings(PayRoll e)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString;

        _SqlString = "select * from PayOutput_Earnings where pn_CompanyID=" + e.CompanyId + " and pn_BranchID= '" + e.BranchId + "' and d_Date='" + e.strDate + "'";

        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();
            employee.T_Amount = (double)dr_Designation["Amount"];
            DesignationList.Add(employee);
        }
        _Connection.Close();
        return DesignationList;
    }

    public Collection<PayRoll> fn_Out_PayOutput_Earnings_Mode(PayRoll e)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString;

        _SqlString = "select * from PayOutput_Earnings where pn_CompanyID=" + e.CompanyId + " and pn_BranchID= '" + e.BranchId + "' and d_Date='" + e.strDate + "' and Mode != 'a' and pn_EmployeeID = '" + e.EmployeeId + "'";

        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();
            employee.T_Amount = (double)dr_Designation["Amount"];
            DesignationList.Add(employee);
        }
        _Connection.Close();
        return DesignationList;
    }

    public Collection<PayRoll> fn_Out_PayOutput_Deduction_Mode(PayRoll e)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString;

        _SqlString = "select * from PayOutput_Deductions where pn_CompanyID=" + e.CompanyId + " and pn_BranchID= '" + e.BranchId + "' and d_Date='" + e.strDate + "' and Mode != 'a' and pn_EmployeeID = '" + e.EmployeeId + "'";

        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();
            employee.T_Amount = (double)dr_Designation["Amount"];
            DesignationList.Add(employee);
        }
        _Connection.Close();
        return DesignationList;
    }

    public Collection<PayRoll> fn_Out_PayOutput_Deductios(PayRoll e)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString;

        _SqlString = "select * from PayOutput_Deductions where pn_CompanyID=" + e.CompanyId + " and pn_BranchID= '" + e.BranchId + "' and d_Date='" + e.strDate + "'";

        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();
            employee.T_Amount = (double)dr_Designation["Amount"];
            DesignationList.Add(employee);
        }
        _Connection.Close();
        return DesignationList;
    }

    public Collection<PayRoll> Temp_Earnings(string str)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();

        SqlCommand _SSDesignation = new SqlCommand(str, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();
            employee.CompanyId = (int)dr_Designation["pn_CompanyID"];
            employee.BranchId = (int)dr_Designation["pn_BranchID"];
            employee.EmployeeId = (int)dr_Designation["pn_EmployeeID"];
            employee.EarningsId = (int)dr_Designation["pn_EarningsID"];
            employee.Amount = (int)dr_Designation["n_Amount"];
            employee.d_date = Convert.ToDateTime(dr_Designation["d_Date"]);
            employee.regular = Convert.ToChar(dr_Designation["c_eligible"]);

            DesignationList.Add(employee);
        }
        _Connection.Close();
        return DesignationList;
    }


    public Collection<PayRoll> Temp_Earnings2()
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();

        SqlCommand _SSDesignation = new SqlCommand("select a.pn_employeeid,b.v_earningscode,a.n_amount from paym_emp_earnings as a,paym_earnings as b where a.pn_earningsid=b.pn_earningsid", _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();

            employee.EmployeeId = (int)dr_Designation["pn_employeeid"];
            employee.EarningsCode = (string)dr_Designation["v_earningscode"];
            employee.Amount = (int)dr_Designation["n_amount"];

            DesignationList.Add(employee);
        }
        _Connection.Close();
        return DesignationList;
    }


    public Collection<PayRoll> Temp_Deductions2()
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();

        SqlCommand _SSDesignation = new SqlCommand("select a.pn_EmployeeID,b.v_DeductionCode,a.n_Amount from paym_Emp_Deduction as a,paym_Deduction as b where a.pn_DeductionID=b.pn_DeductionID", _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();

            employee.EmployeeId = (int)dr_Designation["pn_EmployeeID"];
            employee.DeducationCode = (string)dr_Designation["v_DeductionCode"];
            employee.Amount = Convert.ToDouble(dr_Designation["n_Amount"]);

            DesignationList.Add(employee);
        }
        _Connection.Close();
        return DesignationList;
    }




    //*********************Functions*********************************************

    public DataSet fn_Output(string Query)
    {

        _Connection = Con.fn_Connection();

        _Connection.Open();

        SqlDataAdapter _Ad_sample = new SqlDataAdapter(Query, _Connection);

        DataSet _Ds_sample = new DataSet();

        _Ad_sample.Fill(_Ds_sample);

        _Connection.Close();

        return _Ds_sample;

    }

    public string check_null(string txtvalue)
    {
        string str;
        str = txtvalue;

        if (str == "")
        {
            str = "0";

        }
        else if (str == "Pre-defined")
        {
            str = "0";
        }

        return str;

    }



    public void fn_In_Out(string str_query)
    {


        _Connection = Con.fn_Connection();

        _Connection.Open();

        SqlCommand _RSC_can = new SqlCommand(str_query, _Connection);

        _RSC_can.ExecuteNonQuery();

        _Connection.Close();


    }

    //.................paym_pf...........

    public string pay_pf(PayRoll e)
    {
        try
        {
            _Connection = Con.fn_Connection();
            SqlCommand _Cmd = new SqlCommand("sp_pf", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[14];
            _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPParamter[0].Value = e.CompanyId;
            _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
            _ISPParamter[1].Value = e.BranchId;
            _ISPParamter[2] = new SqlParameter("@Emp_Con_PF", SqlDbType.Float);
            _ISPParamter[2].Value = e.Emp_Con_PF;
            _ISPParamter[3] = new SqlParameter("@Emp_Con_EPF", SqlDbType.Float);
            _ISPParamter[3].Value = e.Emp_Con_EPF;
            _ISPParamter[4] = new SqlParameter("@Emp_Con_FPF", SqlDbType.Float);
            _ISPParamter[4].Value = e.Emp_Con_FPF;
            _ISPParamter[5] = new SqlParameter("@Admin_Charges", SqlDbType.Float);
            _ISPParamter[5].Value = e.AdminCharges;
            _ISPParamter[6] = new SqlParameter("@check_ceiling", SqlDbType.VarChar);
            _ISPParamter[6].Value = e.max_ceiling;
            _ISPParamter[7] = new SqlParameter("@max_amount", SqlDbType.Float);
            _ISPParamter[7].Value = e.upper_limit;
            _ISPParamter[8] = new SqlParameter("@Eligibility_Amt", SqlDbType.Float);
            _ISPParamter[8].Value = e.EligibilityAmt;
            _ISPParamter[9] = new SqlParameter("@c_Round", SqlDbType.Char);
            _ISPParamter[9].Value = e.c_Round;
            _ISPParamter[10] = new SqlParameter("@d_date", SqlDbType.DateTime);
            _ISPParamter[10].Value = e.Date;
            _ISPParamter[11] = new SqlParameter("@month", SqlDbType.VarChar);
            _ISPParamter[11].Value = e.PTmonth;
            _ISPParamter[12] = new SqlParameter("@year", SqlDbType.Int);
            _ISPParamter[12].Value = e.Year;
            _ISPParamter[13] = new SqlParameter("@check_allowance", SqlDbType.Char);
            _ISPParamter[13].Value = e.regular;

            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
            return "0";
        }
        catch (SqlException Ex)
        {
            return "1";
        }
    }

    //........................paym_pt....................

    public string pay_pt(PayRoll e)
    {
        try
        {
            _Connection = Con.fn_Connection();
            SqlCommand _Cmd = new SqlCommand("sp_paym_PT", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[10];
            _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPParamter[0].Value = e.CompanyId;
            _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
            _ISPParamter[1].Value = e.BranchId;
            _ISPParamter[2] = new SqlParameter("@PT_ID", SqlDbType.Int);
            _ISPParamter[2].Value = e.PTcount;     
            _ISPParamter[3] = new SqlParameter("@From_Amt", SqlDbType.Float);
            _ISPParamter[3].Value = e.F_Amount;
            _ISPParamter[4] = new SqlParameter("@To_Amt", SqlDbType.Float);
            _ISPParamter[4].Value = e.T_Amount;
            _ISPParamter[5] = new SqlParameter("@Pro_Tax_Amt", SqlDbType.Float);
            _ISPParamter[5].Value = e.ProTaxAmt;
            _ISPParamter[6] = new SqlParameter("@half_monthly", SqlDbType.Float);
            _ISPParamter[6].Value = e.half_monthly;
            _ISPParamter[7] = new SqlParameter("@Quaterly", SqlDbType.Float);
            _ISPParamter[7].Value = e.Quaterly;
            _ISPParamter[8] = new SqlParameter("@Annual", SqlDbType.Float);
            _ISPParamter[8].Value = e.Annual;
            _ISPParamter[9] = new SqlParameter("@d_date", SqlDbType.DateTime);
            _ISPParamter[9].Value = DateTime.Now;

            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();
            
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
            return "0";
        }
        catch (SqlException Ex)
        {
            return "1";
        }
    }

    public string pay_Increment(PayRoll e)
    {
        try
        {
            _Connection = Con.fn_Connection();
            SqlCommand _Cmd = new SqlCommand("sp_paym_Increment", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[7];
            _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPParamter[0].Value = e.CompanyId;
            _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
            _ISPParamter[1].Value = e.BranchId;
            _ISPParamter[2] = new SqlParameter("@Increment_ID", SqlDbType.Int);
            _ISPParamter[2].Value = e.PTcount;
            _ISPParamter[3] = new SqlParameter("@From_Date", SqlDbType.VarChar);
            _ISPParamter[3].Value = e.DurationFrom;
            _ISPParamter[4] = new SqlParameter("@To_Date", SqlDbType.VarChar);
            _ISPParamter[4].Value = e.DurationTo;
            _ISPParamter[5] = new SqlParameter("@Inc_name", SqlDbType.VarChar);
            _ISPParamter[5].Value = e.temp_str;
            _ISPParamter[6] = new SqlParameter("@d_date", SqlDbType.VarChar);
            _ISPParamter[6].Value = e.strDate;

            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();

            _Cmd.ExecuteNonQuery();
            _Connection.Close();
            return "0";
        }
        catch (SqlException Ex)
        {
            return "1";
        }
    }

    public string Emp_OverHeading(PayRoll e)
    {
        try
        {
            _Connection = Con.fn_Connection();

            SqlCommand _Cmd = new SqlCommand("sp_paym_Emp_OverHeading", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[6];
            _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPParamter[0].Value = e.CompanyId;
            _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
            _ISPParamter[1].Value = e.BranchId;
            _ISPParamter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
            _ISPParamter[2].Value = e.EmployeeId;
            _ISPParamter[3] = new SqlParameter("@OverHeadingID", SqlDbType.Int);
            _ISPParamter[3].Value = e.Overheadingid;
            _ISPParamter[4] = new SqlParameter("@Amount", SqlDbType.Int);
            _ISPParamter[4].Value = e.Amount;
            _ISPParamter[5] = new SqlParameter("@Date", SqlDbType.DateTime);
            _ISPParamter[5].Value = e.d_date;


            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
            return "0";
        }
        catch (SqlException Ex)
        {
            return "1";
        }
    }



    public Collection<PayRoll> fn_Overheading1(int eid)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString = "select * from paym_OverHeadingCost where status='Y' and BranchID='" + eid + "'";
        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();
            employee.Overheadingid = (int)dr_Designation["overHeadingID"];
            employee.OverheadingName = Convert.IsDBNull(dr_Designation["OverHeadingName"]) ? "" : (string)dr_Designation["OverHeadingName"];
            employee.Amount = 0;

            DesignationList.Add(employee);
        }
        _Connection.Close();
        return DesignationList;
    }


    public Collection<PayRoll> fn_Emp_OverHeading(PayRoll e)
    {
        Collection<PayRoll> DesignationList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString;

        //_SqlString = "select a.v_EarningsCode,a.v_EarningsName,a.pn_EarningsID as  paym_Earnings,b.n_amount,b.c_eligible,b.pn_EarningsID as  paym_Emp_Earnings from ";
        //_SqlString = _SqlString + "(select v_EarningsCode,v_EarningsName,pn_EarningsID from  paym_Earnings where pn_EarningsID in(select pn_EarningsID from paym_Emp_Earnings where pn_CompanyId=" + e.CompanyId + " and pn_BranchID=" + e.BranchId + " and pn_EmployeeID=" + e.EmployeeId + ") ) a,";
        //_SqlString = _SqlString + "(select n_amount,c_eligible,pn_EarningsID from paym_Emp_Earnings where pn_CompanyId=" + e.CompanyId + " and pn_BranchID=" + e.BranchId + " and pn_EmployeeID=" + e.EmployeeId + ") b where a.pn_EarningsID=b.pn_EarningsID";

        _SqlString = "select a.OverHeadingId,a.OverHeadingName,b.amount from Paym_Emp_OverHeading b right join (select * from Paym_OverHeadingCost where BranchID='" + e.BranchId + "') a on b.pn_EmployeeID='" + e.EmployeeId + "' and b.pn_BranchID='" + e.BranchId + "' and b.pn_CompanyID=a.pn_CompanyID and b.OverHeadingId=a.OverHeadingId";

        SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
        while (dr_Designation.Read())
        {
            PayRoll employee = new PayRoll();
            employee.Overheadingid = (int)dr_Designation["OverHeadingId"];
            employee.OverheadingName = Convert.IsDBNull(dr_Designation["OverHeadingName"]) ? "" : (string)dr_Designation["OverHeadingName"];
            employee.Amount = Convert.IsDBNull(dr_Designation["amount"]) ? 0 : (int)dr_Designation["amount"];
            // employee.regular = Convert.IsDBNull(dr_Designation["c_eligible"]) ? ' ' : Convert.ToChar(dr_Designation["c_eligible"]);
            DesignationList.Add(employee);
        }
        _Connection.Close();
        return DesignationList;
    }

    public Collection<PayRoll> fn_pay_pf(PayRoll p)
    {
        Collection<PayRoll> pflist = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        SqlCommand cmd = new SqlCommand("select * from paym_PF where pn_CompanyID=" + p.CompanyId + " and pn_BranchID=" + p.BranchId + "", _Connection);
        _Connection.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        PayRoll pay = new PayRoll();
        if (dr.Read())
        {   
            pay.CompanyId = (int)dr["pn_CompanyID"];
            pay.Emp_Con_PF = (double)dr["Emp_Con_PF"];
            pay.Emp_Con_EPF = (double)dr["Emp_Con_EPF"];
            pay.Emp_Con_FPF = (double)dr["Emp_Con_FPF"];
            pay.AdminCharges = (double)dr["Admin_Charges"];
            pay.EligibilityAmt = (double)dr["Eligibility_Amt"];
            pay.c_Round = Convert.ToChar(dr["c_Round"]);
            pay.strDate = Convert.ToDateTime(dr["d_date"]).ToString("dd/MM/yyyy");
            pay.maxceiling = (string)dr["check_ceiling"];
            pay.maxamount = (int)dr["max_amount"];
            pay.regular = Convert.ToChar(dr["check_allowance"]);
            pay.PTmonth = (string)dr["month"];
            pay.Year = (int)dr["year"];
            pay.Count = 1;
            pflist.Add(pay);
        }
        else
        {
            pay.Count = 0;
            pflist.Add(pay);
        }
        return pflist;
    }

    public Collection<PayRoll> fn_pay_esi(PayRoll p)
    {
        Collection<PayRoll> esilist = new Collection<PayRoll>();
        PayRoll l = new PayRoll();
        _Connection = Con.fn_Connection();
        SqlCommand cmd = new SqlCommand("select * from paym_ESI where pn_CompanyID=" + p.CompanyId + " and pn_BranchID=" + p.BranchId + "", _Connection);
        _Connection.Open();
        SqlDataReader dReader = cmd.ExecuteReader();
        if (dReader.Read())
        {
            l.CompanyId = (int)dReader["pn_companyid"];
            l.Emp_Con = (double)dReader["Employee_Con"];
            l.Employer_Con = (double)dReader["Employer_Con"];
            l.AdminCharges = (int)dReader["Admin_Charges"];
            l.EligibilityAmt = (double)dReader["Eligibility_Amt"];
            l.Round = Convert.ToChar(dReader["c_Round"]);
            //l.strDate = (dReader["d_date"].ToString());
            DateTime str_date = Convert.ToDateTime(dReader["d_date"].ToString());
            l.Month = str_date.Month;
            l.Year = str_date.Year;
            l.Count = 1;
            esilist.Add(l);
        }
        else
        {
            l.Count = 0;
            esilist.Add(l);
        }
        _Connection.Close();
        return esilist;
    }

    

    public Collection<PayRoll> fn_pay_EDLI(PayRoll p)
    {
        Collection<PayRoll> edlilist = new Collection<PayRoll>();
        PayRoll l = new PayRoll();
        _Connection = Con.fn_Connection();
        SqlCommand cmd = new SqlCommand("select * from paym_EDLI where pn_CompanyID=" + p.CompanyId + " and pn_BranchID=" + p.BranchId + "", _Connection);
        _Connection.Open();
        SqlDataReader dReader = cmd.ExecuteReader();
        if (dReader.Read())
        {
            l.CompanyId = (int)dReader["pn_companyid"];
            l.Employer_Con = (double)dReader["Employer_Con"];
            l.AdminCharges = (int)dReader["Admin_Charges"];
            l.eligible = Convert.ToChar(dReader["eligibility"]);
            l.EligibilityAmt = (double)dReader["Eligibility_Amt"];
            l.Round = Convert.ToChar(dReader["c_Round"]);
            //l.strDate = (dReader["d_date"].ToString());
            DateTime str_date = Convert.ToDateTime(dReader["d_date"].ToString());
            l.Month = str_date.Month;
            l.Year = str_date.Year;
            l.Count = 1;
            edlilist.Add(l);
        }
        else
        {
            l.Count = 0;
            edlilist.Add(l);
        }
        _Connection.Close();
        return edlilist;
    }

    public Collection<PayRoll> fn_pay_pt(PayRoll p)
    {
        Collection<PayRoll> ptlist = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        SqlCommand cmd = new SqlCommand("select * from paym_PT where pn_CompanyID=" +p.CompanyId+" and pn_BranchID="+p.BranchId+"", _Connection);
        _Connection.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            PayRoll pay = new PayRoll();
            pay.CompanyId = (int)dr["pn_CompanyID"];
            pay.PTcount = (int)dr["PT_ID"];    
            pay.F_Amount = (double)dr["From_Amt"];
            pay.T_Amount = (double)dr["To_Amt"];
            pay.ProTaxAmt = (double)dr["Pro_Tax_Amt"];
            pay.half_monthly = (double)dr["half_monthly"];
            pay.Quaterly = (double)dr["Quaterly"];
            pay.Annual = (double)dr["Annual"];
            pay.strDate = Convert.ToDateTime(dr["d_date"]).ToString("dd/MM/yyyy"); //(DateTime)dr["d_date"];
            ptlist.Add(pay);
        }
        _Connection.Close();
        return ptlist;
    }

    public Collection<PayRoll> fn_Annual_Increment(PayRoll p)
    {
        Collection<PayRoll> ptlist = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        SqlCommand cmd = new SqlCommand("select * from paym_increment where pn_CompanyID=" + p.CompanyId + " and pn_BranchID=" + p.BranchId + "", _Connection);
        _Connection.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            PayRoll pay = new PayRoll();
            pay.PTcount = (int)dr["Increment_ID"];
            pay.DurationFrom = Convert.ToString(dr["From_date"]);
            pay.DurationTo = Convert.ToString(dr["To_Date"]);
            pay.strDate = Convert.ToString(dr["d_Date"]);
            pay.temp_str = Convert.ToString(dr["Inc_name"]);
            ptlist.Add(pay);
        }
        _Connection.Close();
        return ptlist;
    }



     public Collection<PayRoll> fn_ESI(PayRoll p)
    {
        Collection<PayRoll> ESIlist = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        SqlCommand cmd = new SqlCommand("Select * from paym_ESI where pn_CompanyID=" + p.CompanyId + " and pn_BranchID=" + p.BranchId + "", _Connection);
        _Connection.Open();
        SqlDataReader dr = cmd.ExecuteReader();

        while (dr.Read())
        {
            PayRoll l = new PayRoll();
            l.CompanyId = (int)dr["pn_companyid"];
            l.Emp_Con = (double)dr["Employee_Con"];
            l.Employer_Con = (double)dr["Employer_Con"];
            l.AdminCharges = (int)dr["Admin_Charges"];
            l.EligibilityAmt = (double)dr["Eligibility_Amt"];
            l.Round =Convert.ToChar(dr["c_Round"]);
            l.strDate = Convert.ToDateTime(dr["d_date"]).ToString("dd/MM/yyyy"); //l.d_date=(DateTime)dr["d_date"];
            ESIlist.Add(l);
        }
        _Connection.Close();
        return ESIlist;
    }

    public string ESI(PayRoll e)
    {
        try
        {
            _Connection = Con.fn_Connection();
            SqlCommand _Cmd = new SqlCommand("sp_paym_ESI", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[8];
            _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPParamter[0].Value = e.CompanyId;
            _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
            _ISPParamter[1].Value = e.BranchId;
            _ISPParamter[2] = new SqlParameter("@Employee_Con", SqlDbType.Float);
            _ISPParamter[2].Value = e.Emp_Con;
            _ISPParamter[3] = new SqlParameter("@Employer_Con", SqlDbType.Float);
            _ISPParamter[3].Value = e.Employer_Con;
            _ISPParamter[4] = new SqlParameter("@Admin_Charges", SqlDbType.Float);
            _ISPParamter[4].Value = e.AdminCharges;
            _ISPParamter[5] = new SqlParameter("@Eligibility_Amt", SqlDbType.Float);
            _ISPParamter[5].Value = e.EligibilityAmt;
            _ISPParamter[6] = new SqlParameter("@c_Round", SqlDbType.Char);
            _ISPParamter[6].Value = e.c_Round; 
            _ISPParamter[7] = new SqlParameter("@d_Date", SqlDbType.DateTime);
            _ISPParamter[7].Value = e.d_date;

            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
            return "0";
        }
        catch (SqlException Ex)
        {
            return "1";
        }
    }


    public string EDLI(PayRoll e)
    {
        try
        {
            _Connection = Con.fn_Connection();
            SqlCommand _Cmd = new SqlCommand("sp_paym_EDLI", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[8];
            _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPParamter[0].Value = e.CompanyId;
            _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
            _ISPParamter[1].Value = e.BranchId;
            _ISPParamter[2] = new SqlParameter("@Eligibility", SqlDbType.Char);
            _ISPParamter[2].Value =e.eligible;
            _ISPParamter[3] = new SqlParameter("@Employer_Con", SqlDbType.Float);
            _ISPParamter[3].Value = e.Employer_Con;
            _ISPParamter[4] = new SqlParameter("@Admin_Charges", SqlDbType.Float);
            _ISPParamter[4].Value = e.AdminCharges;
            _ISPParamter[5] = new SqlParameter("@Eligibility_Amt", SqlDbType.Float);
            _ISPParamter[5].Value = e.EligibilityAmt;
            _ISPParamter[6] = new SqlParameter("@c_Round", SqlDbType.Char);
            _ISPParamter[6].Value = e.c_Round;
            _ISPParamter[7] = new SqlParameter("@d_Date", SqlDbType.DateTime);
            _ISPParamter[7].Value = e.d_date;

            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
            return "0";
        }
        catch (SqlException Ex)
        {
            return "1";
        }
    }

    public string pay_Common_settings(PayRoll e)
    {
        try
        {
            _Connection = Con.fn_Connection();


            SqlCommand _Cmd = new SqlCommand("sp_settings_Common", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[6];
            _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPParamter[0].Value = e.CompanyId;
            _ISPParamter[1] = new SqlParameter("@Common_Flag", SqlDbType.Char);
            _ISPParamter[1].Value = e.commonflag;
            _ISPParamter[2] = new SqlParameter("@Month_Calc", SqlDbType.Char);
            _ISPParamter[2].Value = e.Month_Calc;
            _ISPParamter[3] = new SqlParameter("@Calc_Days", SqlDbType.VarChar);
            _ISPParamter[3].Value = e.temp_str;
            _ISPParamter[4] = new SqlParameter("@Week_Holiday1", SqlDbType.VarChar);
            _ISPParamter[4].Value = e.Week_Holiday1;
            _ISPParamter[5] = new SqlParameter("@Week_Holiday2", SqlDbType.VarChar);
            _ISPParamter[5].Value = e.Week_Holiday2;

            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
            return "0";
        }
        catch (SqlException Ex)
        {
            return "1";
        }
    }

    public string pay_ot_settings(PayRoll e)
    {
        try
        {
            _Connection = Con.fn_Connection();


            SqlCommand _Cmd = new SqlCommand("sp_Settings_OT", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[4];
            _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPParamter[0].Value = e.CompanyId;
            _ISPParamter[1] = new SqlParameter("@OT_Flag", SqlDbType.Char);
            _ISPParamter[1].Value = e.OT_Flag;
            _ISPParamter[2] = new SqlParameter("@OT_Days", SqlDbType.Char);
            _ISPParamter[2].Value = e.OT_Days;
            _ISPParamter[3] = new SqlParameter("@OT_Hours", SqlDbType.Char);
            _ISPParamter[3].Value = e.OT_HRS;
            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
            return "0";
        }
        catch (SqlException Ex)
        {
            return "1";
        }
    }

    public string pay_pt_settings(PayRoll e)
    {
        try
        {
            _Connection = Con.fn_Connection();


            SqlCommand _Cmd = new SqlCommand("sp_Settings_PT", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[4];
            _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPParamter[0].Value = e.CompanyId;
            _ISPParamter[1] = new SqlParameter("@PT_Flag", SqlDbType.Char);
            _ISPParamter[1].Value = e.PT_Flag;
            _ISPParamter[2] = new SqlParameter("@PT_Type", SqlDbType.Char);
            _ISPParamter[2].Value = e.PTtype;
            _ISPParamter[3] = new SqlParameter("@PT_Months", SqlDbType.VarChar);
            _ISPParamter[3].Value = e.PTmonth;
            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
            return "0";
        }
        catch (SqlException Ex)
        {
            return "1";
        }
    }
   
    public string pay_LWF_settings(PayRoll e)
    {
        try
        {
            _Connection = Con.fn_Connection();


            SqlCommand _Cmd = new SqlCommand("sp_Settings_LWF", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[5];
            _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPParamter[0].Value = e.CompanyId;
            _ISPParamter[1] = new SqlParameter("@LWF_Flag", SqlDbType.Char);
            _ISPParamter[1].Value = e.LWF_Flag;
            _ISPParamter[2] = new SqlParameter("@LWF_Limit", SqlDbType.Int);
            _ISPParamter[2].Value = e.LWF_Limit;
            _ISPParamter[3] = new SqlParameter("@LWF_Amt", SqlDbType.Float);
            _ISPParamter[3].Value = e.LWF_Amt;
            _ISPParamter[4] = new SqlParameter("@LWF_Month", SqlDbType.Int);
            _ISPParamter[4].Value = e.LWF_Month;
            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
            return "0";
        }
        catch (SqlException Ex)
        {
            return "1";
        }
    }

    public string pay_Bonus_settings(PayRoll e)
    {
        try
        {
            _Connection = Con.fn_Connection();


            SqlCommand _Cmd = new SqlCommand("sp_settings_Bonus", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[6];
            _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPParamter[0].Value = e.CompanyId;
            _ISPParamter[1] = new SqlParameter("@Bonus_Flag", SqlDbType.Char);
            _ISPParamter[1].Value = e.Bonus_Flag;
            _ISPParamter[2] = new SqlParameter("@Bonus_From_Month", SqlDbType.Int);
            _ISPParamter[2].Value = e.BonusFromMonth;
            _ISPParamter[3] = new SqlParameter("@Bonus_To_Month", SqlDbType.Int);
            _ISPParamter[3].Value = e.BonusToMonth;
            _ISPParamter[4] = new SqlParameter("@Bonus_Type", SqlDbType.Char);
            _ISPParamter[4].Value = e.BonusType;
            _ISPParamter[5] = new SqlParameter("@Bonus_Limit", SqlDbType.Int);
            _ISPParamter[5].Value = e.Bonuslimit;

            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
            return "0";
        }
        catch (SqlException Ex)
        {
            return "1";
        }
    }

    public Collection<PayRoll> fn_Common_Settings(PayRoll pro)
    {
        Collection<PayRoll> DeductionList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString = "select * from payminput_settings where pn_CompanyID=" + pro.CompanyId + " and Common_Flag='Y'";
        SqlCommand _SSDeduction = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Deduction = _SSDeduction.ExecuteReader();
        while (dr_Deduction.Read())
        {
            PayRoll pr = new PayRoll();

            pr.Month_Calc = Convert.ToChar(dr_Deduction["Month_Calc"]);

            pr.temp_str = Convert.IsDBNull(dr_Deduction["Calc_Days"]) ? "" : Convert.ToString((double)dr_Deduction["Calc_Days"]);
            pr.Week_Holiday1 = Convert.IsDBNull(dr_Deduction["Week_Holiday1"]) ? "" : (string)dr_Deduction["Week_Holiday1"];
            pr.Week_Holiday2 = Convert.IsDBNull(dr_Deduction["Week_Holiday2"]) ? "" : (string)dr_Deduction["Week_Holiday2"];
            
            DeductionList.Add(pr);
        }
        _Connection.Close();
        return DeductionList;
    }

    public Collection<PayRoll> fn_OT_Settings(PayRoll pro)
    {
        Collection<PayRoll> DeductionList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString = "select * from payminput_settings where pn_CompanyID=" + pro.CompanyId + " and OT_Flag='Y'";
        SqlCommand _SSDeduction = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Deduction = _SSDeduction.ExecuteReader();
        while (dr_Deduction.Read())
        {
            PayRoll pr = new PayRoll();
            
            pr.OT_Days = (double)dr_Deduction["OT_Days"];
            pr.OT_HRS = (double)dr_Deduction["OT_Hours"];
           
            DeductionList.Add(pr);
        }
        _Connection.Close();
        return DeductionList;
    }

    public Collection<PayRoll> fn_PT_Settings(PayRoll pro)
    {
        Collection<PayRoll> DeductionList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString = "select * from payminput_settings where pn_CompanyID=" + pro.CompanyId + " and PT_Flag='Y'";
        SqlCommand _SSDeduction = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Deduction = _SSDeduction.ExecuteReader();
        while (dr_Deduction.Read())
        {
            PayRoll pr = new PayRoll();

            pr.PTtype=Convert.ToChar(dr_Deduction["PT_Type"]);                        
            pr.PTmonth = Convert.IsDBNull(dr_Deduction["PT_Months"]) ? "" : (string)dr_Deduction["PT_Months"];
                      
            DeductionList.Add(pr);
        }
        _Connection.Close();
        return DeductionList;
    }

    public Collection<PayRoll> fn_LWF_Settings(PayRoll pro)
    {
        Collection<PayRoll> DeductionList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString = "select * from payminput_settings where pn_CompanyID=" + pro.CompanyId + " and LWF_Flag='Y'";
        SqlCommand _SSDeduction = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Deduction = _SSDeduction.ExecuteReader();
        while (dr_Deduction.Read())
        {
            PayRoll pr = new PayRoll();
                       
            pr.LWF_Limit = (int)dr_Deduction["LWF_Limit"];
            pr.LWF_Amt = (double)dr_Deduction["LWF_Amt"];
            pr.LWF_Month = (int)dr_Deduction["LWF_Month"];
           
            DeductionList.Add(pr);
        }
        _Connection.Close();
        return DeductionList;
    }

    public Collection<PayRoll> fn_Bonus_Settings(PayRoll pro)
    {
        Collection<PayRoll> DeductionList = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string _SqlString = "select * from payminput_settings where pn_CompanyID=" + pro.CompanyId + " and Bonus_Flag='Y'";
        SqlCommand _SSDeduction = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Deduction = _SSDeduction.ExecuteReader();
        while (dr_Deduction.Read())
        {
            PayRoll pr = new PayRoll();
            pr.BonusFromMonth = (int)dr_Deduction["Bonus_From_Month"];
            pr.BonusToMonth = (int)dr_Deduction["Bonus_To_Month"];
            pr.BonusType = Convert.ToChar(dr_Deduction["Bonus_Type"]);
            pr.Bonuslimit = (int)dr_Deduction["Bonus_Limit"];
            DeductionList.Add(pr);
        }
        _Connection.Close();
        return DeductionList;
    }

    public void update(PayRoll p)
    {
        _Connection = Con.fn_Connection();
        _Connection.Open();
        SqlCommand cmd = new SqlCommand("update paym_loan set v_loanname='" + p.loanname + "',v_loancode='" + p.loancode + "' where pn_loanid=" + p.CompanyId + "", _Connection);
        cmd.ExecuteNonQuery();
        _Connection.Close();

    }

    public Collection<PayRoll> fn_getAll_Employeedetails(PayRoll p)
    {
        Collection<PayRoll> payalllist = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string sql;
        sql = "";
        sql = "select b.pn_EmployeeID, b.EmployeeCode, b.Employee_First_Name, b.OT_Eligible, a.pn_EmployeeID as payinput_empid, a.Calc_Days, a.Paid_Days, a.Present_Days, a.Absent_Days, a.TotLeave_Days, a.WeekOffDays, a.Holidays, a.OnDuty_days, a.Compoff_Days, a.Tour_Days, a.Att_Bonus, a.OT_hrs from paym_employee b left join  payinput as a on a.pn_employeeid=b.pn_employeeid and a.d_Date='" + p.d_date + "'";
        sql = sql + " where b.pn_CompanyID=" + p.CompanyId + " and b.pn_BranchID=" + p.BranchId + " and b.status = 'Y'  order by b.employeecode asc";
        SqlCommand _Course = new SqlCommand(sql, _Connection);
        _Connection.Open();
        SqlDataReader dr = _Course.ExecuteReader();
        while (dr.Read())
        {
            PayRoll py = new PayRoll();
            py.EmployeeId = (int)dr["pn_EmployeeID"];
            py.EmployeeCode = Convert.IsDBNull(dr["EmployeeCode"]) ? "" : (string)dr["EmployeeCode"];
            py.FirstName = Convert.IsDBNull(dr["Employee_First_Name"]) ? "" : (string)dr["Employee_First_Name"];
            py.ex_EmployeeID = Convert.IsDBNull(dr["payinput_empid"]) ? Convert.ToInt32("0") : (int)dr["payinput_empid"];
            py.Calc_Days = Convert.IsDBNull(dr["Calc_Days"]) ? Convert.ToDouble("0") : (double)dr["Calc_Days"];
            py.Paid_Days = Convert.IsDBNull(dr["Paid_Days"]) ? Convert.ToDouble("0") : (double)dr["Paid_Days"];
            py.Present_Days = Convert.IsDBNull(dr["Present_Days"]) ? Convert.ToDouble("0") : (double)dr["Present_Days"];
            py.Absent_Days = Convert.IsDBNull(dr["Absent_Days"]) ? Convert.ToDouble("0") : (double)dr["Absent_Days"];
            py.TotLeave_Days = Convert.IsDBNull(dr["TotLeave_Days"]) ? Convert.ToDouble("0") : (double)dr["TotLeave_Days"];
            py.WeekOffDays = Convert.IsDBNull(dr["WeekOffDays"]) ? Convert.ToDouble("0") : (double)dr["WeekOffDays"];
            py.Holidays = Convert.IsDBNull(dr["Holidays"]) ? Convert.ToDouble("0") : (double)dr["Holidays"];
            py.OnDuty_days = Convert.IsDBNull(dr["OnDuty_days"]) ? Convert.ToDouble("0") : (double)dr["OnDuty_days"];
            py.Compoff_Days = Convert.IsDBNull(dr["Compoff_Days"]) ? Convert.ToDouble("0") : (double)dr["Compoff_Days"];
            py.Tour_Days = Convert.IsDBNull(dr["Tour_Days"]) ? Convert.ToDouble("0") : (double)dr["Tour_Days"];
            py.Att_Bonus = Convert.IsDBNull(dr["Att_Bonus"]) ? "" : (string)dr["Att_Bonus"];
            py.Date = Convert.IsDBNull(dr["ot_hrs"]) ? Convert.ToDateTime("1900-01-01") : (DateTime)dr["ot_hrs"];
            py.OT = Convert.IsDBNull(dr["OT_Eligible"]) ? Convert.ToChar("") : Convert.ToChar(dr["OT_Eligible"]);
            py.LastName = (string)dr["EmployeeCode"] + " - " + (string)dr["Employee_First_Name"];
            payalllist.Add(py);
        }
        _Connection.Close();
        return payalllist;
    }

    public Collection<PayRoll> fn_Employeedetails_deptid(PayRoll p)
    {
        Collection<PayRoll> payalllist = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string sql;
        sql = "";
        sql = "select b.pn_EmployeeID, b.EmployeeCode, b.Employee_First_Name, b.OT_Eligible, a.pn_EmployeeID as payinput_empid, a.Calc_Days, a.Paid_Days, a.Present_Days, a.Absent_Days, a.TotLeave_Days, a.WeekOffDays, a.Holidays, a.OnDuty_days, a.Compoff_Days, a.Tour_Days, a.Att_Bonus, a.OT_HRS from paym_employee b left join  payinput as a on a.pn_employeeid=b.pn_employeeid and a.d_Date='" + p.d_date + "' where b.pn_EmployeeID in (select pn_EmployeeID from  paym_employee_profile1 where " + p.CategoryID + " = " + p.DepartmentID + " and b.pn_CompanyID=" + p.CompanyId + " and b.pn_BranchID=" + p.BranchId + ")";
        SqlCommand _Course = new SqlCommand(sql, _Connection);
        _Connection.Open();
        SqlDataReader dr = _Course.ExecuteReader();
        while (dr.Read())
        {
            PayRoll py = new PayRoll();
            py.EmployeeId = (int)dr["pn_EmployeeID"];
            py.EmployeeCode = Convert.IsDBNull(dr["EmployeeCode"]) ? "" : (string)dr["EmployeeCode"];
            py.FirstName = Convert.IsDBNull(dr["Employee_First_Name"]) ? "" : (string)dr["Employee_First_Name"];

            py.ex_EmployeeID = Convert.IsDBNull(dr["payinput_empid"]) ? Convert.ToInt32("0") : (int)dr["payinput_empid"];
            py.Calc_Days = Convert.IsDBNull(dr["Calc_Days"]) ? Convert.ToDouble("0") : (double)dr["Calc_Days"];
            py.Paid_Days = Convert.IsDBNull(dr["Paid_Days"]) ? Convert.ToDouble("0") : (double)dr["Paid_Days"];
            py.Present_Days = Convert.IsDBNull(dr["Present_Days"]) ? Convert.ToDouble("0") : (double)dr["Present_Days"];
            py.Absent_Days = Convert.IsDBNull(dr["Absent_Days"]) ? Convert.ToDouble("0") : (double)dr["Absent_Days"];
            py.TotLeave_Days = Convert.IsDBNull(dr["TotLeave_Days"]) ? Convert.ToDouble("0") : (double)dr["TotLeave_Days"];
            py.WeekOffDays = Convert.IsDBNull(dr["WeekOffDays"]) ? Convert.ToDouble("0") : (double)dr["WeekOffDays"];
            py.Holidays = Convert.IsDBNull(dr["Holidays"]) ? Convert.ToDouble("0") : (double)dr["Holidays"];
            py.OnDuty_days = Convert.IsDBNull(dr["OnDuty_days"]) ? Convert.ToDouble("0") : (double)dr["OnDuty_days"];
            py.Compoff_Days = Convert.IsDBNull(dr["Compoff_Days"]) ? Convert.ToDouble("0") : (double)dr["Compoff_Days"];
            py.Tour_Days = Convert.IsDBNull(dr["Tour_Days"]) ? Convert.ToDouble("0") : (double)dr["Tour_Days"];
            py.Att_Bonus = Convert.IsDBNull(dr["Att_Bonus"]) ? "" : (string)dr["Att_Bonus"];
            py.OT = Convert.IsDBNull(dr["OT_Eligible"]) ? Convert.ToChar("") : Convert.ToChar(dr["OT_Eligible"]);
            //py.OT_HRS = Convert.IsDBNull(dr["OT_HRS"]) ? Convert.ToDouble("0") : (double)dr["OT_HRS"];

            py.LastName = (string)dr["EmployeeCode"] + " - " + (string)dr["Employee_First_Name"];
            payalllist.Add(py);
        }
        _Connection.Close();
        return payalllist;
    }

    public double Payout_Deduction_Sum(PayRoll p)
    {
        double sum = 0.0;
        _Connection = Con.fn_Connection();
        _Connection.Open();
        SqlCommand cmd = new SqlCommand("Set dateformat dmy;Select Amount from PayOutput_Deductions where pn_companyid='" + p.CompanyId + "' and pn_BranchID = '" + p.BranchId + "' and pn_Employeeid='" + p.EmployeeId + "' and d_date = '" + p.d_date + "';set dateformat mdy;", _Connection);
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            sum += Convert.ToDouble(dr[0]);
        }
        _Connection.Close();
        return sum;
    }

    public double Payout_Earnings_Sum(PayRoll p)
    {
        double sum = 0.0;
        _Connection = Con.fn_Connection();
        _Connection.Open();
        SqlCommand cmd = new SqlCommand("Set dateformat dmy;Select Amount from PayOutput_Earnings where pn_companyid='" + p.CompanyId + "' and pn_BranchID = '" + p.BranchId + "' and pn_Employeeid='" + p.EmployeeId + "' and d_date = '" + p.d_date + "';set dateformat mdy;", _Connection);
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            sum += Convert.ToDouble(dr[0]);
        }
        _Connection.Close();
        return sum;
    }


    public string payinput_insert(PayRoll p)
    {
        try
        {
            _Connection = Con.fn_Connection();
            SqlCommand cmd = new SqlCommand("sp_payinput", _Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] isparam = new SqlParameter[27];
            isparam[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            isparam[0].Value = p.CompanyId;
            isparam[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
            isparam[1].Value = p.BranchId;
            isparam[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
            isparam[2].Value = p.EmployeeId;
            isparam[3] = new SqlParameter("@d_Date", SqlDbType.DateTime);
            isparam[3].Value = p.d_date;
            isparam[4] = new SqlParameter("d_From_Date", SqlDbType.DateTime);
            isparam[4].Value = p.d_FromDate;
            isparam[5] = new SqlParameter("d_To_Date", SqlDbType.DateTime);
            isparam[5].Value = p.d_ToDate;
            isparam[6] = new SqlParameter("@Calc_Days", SqlDbType.Float);
            isparam[6].Value = p.Calc_Days;
            isparam[7] = new SqlParameter("@Paid_Days", SqlDbType.Float);
            isparam[7].Value = p.Paid_Days;
            isparam[8] = new SqlParameter("@Present_Days", SqlDbType.Float);
            isparam[8].Value = p.Present_Days;
            isparam[9] = new SqlParameter("@Absent_Days", SqlDbType.Float);
            isparam[9].Value = p.Absent_Days;
            isparam[10] = new SqlParameter("@TotLeave_Days", SqlDbType.Float);
            isparam[10].Value = p.TotLeave_Days;
            isparam[11] = new SqlParameter("@WeekOffDays", SqlDbType.Float);
            isparam[11].Value = p.WeekOffDays;
            isparam[12] = new SqlParameter("@Holidays", SqlDbType.Float);
            isparam[12].Value = p.Holidays;
            isparam[13] = new SqlParameter("@OnDuty_days", SqlDbType.Float);
            isparam[13].Value = p.OnDuty_days;
            isparam[14] = new SqlParameter("@Compoff_Days", SqlDbType.Float);
            isparam[14].Value = p.Compoff_Days;
            isparam[15] = new SqlParameter("@Tour_Days", SqlDbType.Float);
            isparam[15].Value = p.Tour_Days;
            isparam[16] = new SqlParameter("@Att_Bonus", SqlDbType.Char);
            isparam[16].Value = p.Att_Bonus;
            isparam[17] = new SqlParameter("@Att_BonusAmount", SqlDbType.Float);
            isparam[17].Value = p.Att_BonusAmount;
            isparam[18] = new SqlParameter("@OT_HRS", SqlDbType.DateTime);
            isparam[18].Value = p.Date;
            isparam[19] = new SqlParameter("@Earn_Arrears", SqlDbType.Float);
            isparam[19].Value = p.Earn_Arrears;
            isparam[20] = new SqlParameter("@Ded_Arrears", SqlDbType.Float);
            isparam[20].Value = p.Ded_Arrears;
            isparam[21] = new SqlParameter("@ot_value", SqlDbType.Float);
            isparam[21].Value = p.OT_Days;
            isparam[22] = new SqlParameter("@ot_Amt", SqlDbType.Float);
            isparam[22].Value = p.OT_Amt;
            isparam[23] = new SqlParameter("@Act_Basic", SqlDbType.Float);
            isparam[23].Value = p.Act_Basic;
            isparam[24] = new SqlParameter("@Earn_Basic", SqlDbType.Float);
            isparam[24].Value = p.Earned_Basic;
            isparam[25] = new SqlParameter("@Mode", SqlDbType.Char);
            isparam[25].Value = 'C';
            isparam[26] = new SqlParameter("@Flag", SqlDbType.Char);
            isparam[26].Value = 'N';

            for (int i = 0; i < isparam.Length; i++)
            {
                cmd.Parameters.Add(isparam[i]);
            }
            _Connection.Open();
            cmd.ExecuteNonQuery();
            _Connection.Close();
            return "0";

        }
        catch (SqlException ex)
        {
            return "1";
        }
    }

    public Collection<PayRoll> calc_days(PayRoll p)
    {
        Collection<PayRoll> pay = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        string substring = "select Month_type, Week_off1, Week_off2 from attendance_ceiling where pn_CompanyID=" + p.CompanyId + "";
        SqlCommand cmd = new SqlCommand(substring, _Connection);
        _Connection.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            p.Status = Convert.IsDBNull(dr["Month_type"]) ? "" : (string)dr["Month_type"];
            p.Week_Holiday1 = Convert.IsDBNull(dr["Week_off1"]) ? "" : (string)dr["Week_off1"];
            p.Week_Holiday2 = Convert.IsDBNull(dr["Week_off2"]) ? "" : (string)dr["Week_off2"];
            pay.Add(p);
        }
        _Connection.Close();
        return pay;
    }

    public DateTime Convert_ToSqlDate(string cur_date)
    {
        string _d, _m, _y;
        DateTime sql_date;

        if (cur_date != "")
        {
            string[] da = cur_date.Split('/');
            _d = da[0];
            _m = da[1];
            _y = da[2];

            sql_date = Convert.ToDateTime(_y + "/" + _m + "/" + _d);
        }
        else
        {
            sql_date = Convert.ToDateTime("1900/01/01");

        }

        return sql_date;
    }


    public string Convert_ToIISDate(string cur_date)
    {

        string _d, _m, _y, sql_date = "";

        char[] splitter ={ '/' };
        string[] str_ary = new string[4];

        if (cur_date.Length == 10)
        {
            _m = cur_date.Substring(0, 2);
            _d = cur_date.Substring(3, 2);
            _y = cur_date.Substring(6, 4);

            sql_date = _d + "/" + _m + "/" + _y;
        }
        else
        {
            str_ary = cur_date.Split(splitter);


            _m = check_single(str_ary[0]);
            _d = check_single(str_ary[1]);
            _y = str_ary[2];

            sql_date = _d + "/" + _m + "/" + _y;

        }

        return sql_date;

    }

    public string Convert_ToIISDate1(string cur_date)
    {

        string _d, _m, _y, sql_date = "";

        char[] splitter = { '/', ' ' };
        string[] str_ary = new string[4];


        str_ary = cur_date.Split(splitter);


        _m = check_single(str_ary[0]);
        _d = check_single(str_ary[1]);
        _y = str_ary[2];

        sql_date = _d + "/" + _m + "/" + _y;

        return sql_date;

    }


    public string check_single(string str_single)
    {
        if (str_single.Length == 1)
        {
            str_single = "0" + str_single;
        }
        return str_single;
    }

    public void Earn_Deduct_Insert()
    {
        throw new NotImplementedException();
    }

    public void Earnings_update_register()
    {
        throw new NotImplementedException();
    }








   








}

//delete from temp where pn_employeeid in('','')





