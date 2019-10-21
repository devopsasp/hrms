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
using ePayHrms.Leave;
using CrystalDecisions.Web;
using CrystalDecisions.Enterprise;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.Reporting;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

public partial class DB_Refresh : System.Web.UI.Page
{


    Company company = new Company();
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    Candidate c = new Candidate();
    Leave l = new Leave();

  

    Collection<Employee> EmployeeList;
    Collection<Employee> EmpFirstList;
    Collection<Employee> EmpGeneralList;
    Collection<Employee> EmpProfileList;
    Collection<Employee> EmpWorkList;   
   
    Collection<Leave> LeaveList;
 
    Collection<PayRoll> EarningsList;
    Collection<PayRoll> DeductionList;

    string s_login_role;

    int temp_i = 0, i = 0,count=0;  

    string temp_string = "", temp_earn = "",temp_query=""; 
   
    protected void Page_Load(object sender, EventArgs e)
    {
        temp_tables();
    }

    public void temp_tables()
    {
       

        Temp_employee_Drop();
        Temp_employee_Create();
        Temp_employee_Load();

        temp_query = "set dateformat dmy;update temp_Employee set DateofBirth=null where  DateofBirth='1900/01/01'";
        temp_query =temp_query+ " update temp_Employee set JoiningDate=null where  JoiningDate='1900/01/01'";
        temp_query = temp_query + "update temp_Employee set OfferDate=null where  OfferDate='1900/01/01'";
        temp_query = temp_query + " update temp_Employee set ProbationUpto=null where  ProbationUpto='1900/01/01'";
        temp_query = temp_query + " update temp_Employee set ExtendedUpto=null where  ExtendedUpto='1900/01/01'";
        temp_query = temp_query + " update temp_Employee set ConfirmationDate=null where  ConfirmationDate='1900/01/01'";
        temp_query = temp_query + " update temp_Employee set RetirementDate=null where  RetirementDate='1900/01/01'";
        temp_query = temp_query + " update temp_Employee set ContractRenviewDate=null where  ContractRenviewDate='1900/01/01';set dateformat dmy";

        employee.Temp_Employee(temp_query);

        //Temp_employee_Delete();
        //Temp_employee_Load();

        //Temp_earnings_Drop();
        //temp_earnings_create();
        //temp_earnings_load();

        //Temp_deductions_Drop();
        //temp_deductions_create();
        //temp_deductions_load();

        //Temp_leave_Drop();
        //temp_leave_create();

        lbl_refresh.Text = "Database Updated";

        
    }      




    //********************************Temp_tables Operitions*************************************************


    public void Temp_employee_Drop()
    {
        temp_string = "drop table Temp_Employee";

        employee.Temp_Employee(temp_string);

    }

    //public void Temp_employee_Delete()
    //{
    //    temp_string = "Delete from Temp_Employee";

    //    employee.Temp_Employee(temp_string);



    //}

    public void Temp_employee_Create()
    {


        //Temp_employee create

        temp_string = "create table Temp_Employee(";

        temp_string = temp_string + "pn_EmployeeID  int not null,EmployeeCode varchar(50),Employee_First_Name varchar(50),Employee_Middle_Name varchar(50),Employee_Last_Name varchar(50),DateofBirth datetime,Password varchar(20),Gender varchar(20),Employee_Full_Name varchar(70),";
        temp_string = temp_string + "CompanyName varchar(50),BranchName varchar(50),DivisionName varchar(50),DepartmentName varchar(50),DesignationName varchar(50),CategoryName varchar(50),GradeName varchar(50),ShiftName varchar(50),JobStatusName varchar(50),LevelName varchar(50),projectsiteName varchar(50),d_Date datetime,v_Reason varchar(500),";
        temp_string = temp_string + "JoiningDate datetime,OfferDate datetime,ProbationUpto datetime,ExtendedUpto datetime,ConfirmationDate datetime,RetirementDate datetime,ContractRenviewDate datetime,";
        temp_string = temp_string + "EmailId varchar(50),AlternateEmailId varchar(50),BloodGroup varchar(20),Religion varchar(50),Nationality varchar(50),PresentHouseNo varchar(20),PresentStreetName varchar(50),PresentAddLine1 varchar(100),PresentAddLine2 varchar(100),PresentCity varchar(50),PresentState varchar(50),PermanentHouseNo varchar(50),";
        temp_string = temp_string + "PermanentStreetName varchar(50),PermanentAddLine1 varchar(100),PermanentAddLine2 varchar(100),PermanentCity varchar(50),PermanentState varchar(50),ph_Office varchar(20),ph_Residence varchar(20),CellNo varchar(20),Fax varchar(20),emgName varchar(30),emgPhone varchar(30),Salutation  varchar(20),M_Status varchar(20),";
        temp_string = temp_string + "FatherName varchar(30),MotherName varchar(30),Children varchar(3),SpouseName varchar(30),Ref1_Name varchar(30),Ref1_Phno varchar(20),Ref1_Email varchar(50),Ref1_Relation varchar(50),Ref2_Name varchar(30),Ref2_Phno varchar(30),Ref2_Email varchar(50),Ref2_Relation varchar(50), Bank_Name varchar(20), Account_Type varchar(20) )";


        employee.Temp_Employee(temp_string);


        //Add Earnings Column

        EmpFirstList = employee.Temp_Emp_Earnings1(employee);

        if (EmpFirstList.Count > 0)
        {
            for (temp_i = 0; temp_i < EmpFirstList.Count; temp_i++)
            {

                temp_string = "alter table Temp_Employee add " + EmpFirstList[temp_i].EarningsName + " int";

                employee.Temp_Employee(temp_string);

            }

        }


        //Add Deducations Column

        EmpFirstList = employee.Temp_Emp_Deductions1(employee);

        if (EmpFirstList.Count > 0)
        {
            for (temp_i = 0; temp_i < EmpFirstList.Count; temp_i++)
            {

                temp_string = "alter table Temp_Employee add " + EmpFirstList[temp_i].DeducationCode + " int";

                employee.Temp_Employee(temp_string);
            }

        }





    }

    public void Temp_employee_Load()
    {


        //Employee Profile

        EmpFirstList = employee.Temp_Emp_first(employee);

        temp_string = "";

        if (EmpFirstList.Count > 0)
        {

            for (temp_i = 0; temp_i < EmpFirstList.Count; temp_i++)
            {
                temp_string = "set dateformat dmy;insert into Temp_Employee(CompanyName,BranchName,pn_EmployeeID,EmployeeCode,Employee_First_Name,Employee_Middle_Name,Employee_Last_Name,Gender,DateofBirth,Password,bank_name,account_type) values(";
                temp_string = temp_string + "'" + EmpFirstList[temp_i].CourseName + "','" + EmpFirstList[temp_i].SkillName + "'," + EmpFirstList[temp_i].EmployeeId + ",'" + EmpFirstList[temp_i].EmployeeCode + "',";
                temp_string = temp_string + "'" + EmpFirstList[temp_i].FirstName + "','" + EmpFirstList[temp_i].MiddleName + "','" + EmpFirstList[temp_i].LastName + "','" + Convert_gender(EmpFirstList[temp_i].Gender) + "','" + EmpFirstList[temp_i].d_birth.ToShortDateString() + "','" + EmpFirstList[temp_i].password + "','"+EmpFirstList[temp_i].Bank_Name+"','"+EmpFirstList[temp_i].Account_Type+"');set dateformat mdy";

                employee.Temp_Employee(temp_string);

                temp_string = "";
            }

        }


        //Employee General

        EmpGeneralList = employee.Temp_Emp_general(employee);

        temp_string = "";

        if (EmpGeneralList.Count > 0)
        {
            for (temp_i = 0; temp_i < EmpGeneralList.Count; temp_i++)
            {
                temp_string = "update Temp_Employee set EmailId='" + EmpGeneralList[temp_i].EmailId + "',AlternateEmailId='" + EmpGeneralList[temp_i].A_EmailId + "',BloodGroup='" + EmpGeneralList[temp_i].BloodGroup + "',Religion='" + EmpGeneralList[temp_i].Religion + "',Nationality='" + EmpGeneralList[temp_i].Nationality + "',PresentHouseNo='" + EmpGeneralList[temp_i].HouseNo + "',PresentStreetName='" + EmpGeneralList[temp_i].StreetName + "',PresentAddLine1='" + EmpGeneralList[temp_i].AddressLine1 + "',PresentAddLine2='" + EmpGeneralList[temp_i].AddressLine2 + "',";
                temp_string = temp_string + "PresentCity='" + EmpGeneralList[temp_i].City + "',PresentState='" + EmpGeneralList[temp_i].State + "',PermanentHouseNo='" + EmpGeneralList[temp_i].p_HouseNo + "',PermanentStreetName='" + EmpGeneralList[temp_i].p_StreetName + "',PermanentAddLine1='" + EmpGeneralList[temp_i].P_AddressLine1 + "',PermanentAddLine2='" + EmpGeneralList[temp_i].P_AddressLine2 + "',PermanentCity='" + EmpGeneralList[temp_i].P_City + "',PermanentState='" + EmpGeneralList[temp_i].P_State + "',";
                temp_string = temp_string + "ph_Office='" + EmpGeneralList[temp_i].ph_Office + "',ph_Residence='" + EmpGeneralList[temp_i].ph_Residence + "',CellNo='" + EmpGeneralList[temp_i].CellNo + "',Fax='" + EmpGeneralList[temp_i].Fax + "',emgName='" + EmpGeneralList[temp_i].emgname + "',emgPhone='" + EmpGeneralList[temp_i].emgno + "',Salutation='" + Convert_Salutation(EmpGeneralList[temp_i].Salutation) + "',M_Status='" + Convert_MaritalStatus(EmpGeneralList[temp_i].MaritalStatus) + "',FatherName='" + EmpGeneralList[temp_i].FatherName + "',MotherName='" + EmpGeneralList[temp_i].MotherName + "',Children='" + EmpGeneralList[temp_i].Children + "',SpouseName='" + EmpGeneralList[temp_i].SpouseName + "',";
                temp_string = temp_string + "Ref1_Name='" + EmpGeneralList[temp_i].Ref1_Name + "',Ref1_Phno='" + EmpGeneralList[temp_i].Ref1_Phno + "',Ref1_Email='" + EmpGeneralList[temp_i].Ref1_Email + "',Ref1_Relation='" + EmpGeneralList[temp_i].Ref1_Relation + "',Ref2_Name='" + EmpGeneralList[temp_i].Ref2_Name + "',Ref2_Phno='" + EmpGeneralList[temp_i].Ref2_Phno + "',Ref2_Email='" + EmpGeneralList[temp_i].Ref2_Email + "',Ref2_Relation='" + EmpGeneralList[temp_i].Ref2_Relation + "'";
                temp_string = temp_string + " where pn_EmployeeID=" + EmpGeneralList[temp_i].EmployeeId + "";

                employee.Temp_Employee(temp_string);

                temp_string = "";
            }

        }


        //Employee WorkDetails 1(div,.....)

        EmpProfileList = employee.Temp_Emp_Profile(employee);

        temp_string = "";
        if (EmpProfileList.Count > 0)
        {
            for (temp_i = 0; temp_i < EmpProfileList.Count; temp_i++)
            {

                temp_string = "set dateformat dmy;update Temp_Employee set DivisionName='" + EmpProfileList[temp_i].DivisionName + "',DepartmentName='" + EmpProfileList[temp_i].DepartmentName + "',DesignationName='" + EmpProfileList[temp_i].DesignationName + "',CategoryName='" + EmpProfileList[temp_i].CategoryName + "',GradeName='" + EmpProfileList[temp_i].GradeName + "',ShiftName='" + EmpProfileList[temp_i].ShiftName + "',JobStatusName='" + EmpProfileList[temp_i].JobStatusName + "',LevelName='" + EmpProfileList[temp_i].LevelName + "',projectsiteName='" + EmpProfileList[temp_i].ProjectsiteName + "',d_Date='" + EmpProfileList[temp_i].Date.ToShortDateString() + "',v_Reason='" + EmpProfileList[temp_i].temp_str + "'";

                temp_string = temp_string + " where pn_EmployeeID=" + EmpProfileList[temp_i].EmployeeId + ";set dateformat mdy";

                employee.Temp_Employee(temp_string);

                temp_string = "";
            }
        }


        //Employee WorkDetails 2

        EmpWorkList = employee.Temp_Emp_WorkDetails(employee);
        temp_string = "";

        if (EmpWorkList.Count > 0)
        {
            for (temp_i = 0; temp_i < EmpWorkList.Count; temp_i++)
            {

                temp_string = "set dateformat dmy;update Temp_Employee set JoiningDate='" +EmpWorkList[temp_i].d_join.ToShortDateString() + "',OfferDate='" + EmpWorkList[temp_i].d_Offer.ToShortDateString() + "',ProbationUpto='" + EmpWorkList[temp_i].d_probotion.ToShortDateString()+ "',ExtendedUpto='" +EmpWorkList[temp_i].d_extended.ToShortDateString() + "',ConfirmationDate='" + EmpWorkList[temp_i].d_confirmation.ToShortDateString() + "',RetirementDate='" + EmpWorkList[temp_i].d_retirement.ToShortDateString() + "',ContractRenviewDate='" + EmpWorkList[temp_i].d_renue.ToShortDateString()+ "'";

                temp_string = temp_string + " where pn_EmployeeID=" + EmpWorkList[temp_i].EmployeeId + ";set dateformat dmy";

                employee.Temp_Employee(temp_string);

                temp_string = "";
            }

        }


        //Employee Earnings


        EarningsList = pay.Temp_Earnings2();

        if (EarningsList.Count > 0)
        {

            for (temp_i = 0; temp_i < EarningsList.Count; temp_i++)
            {

                temp_string = "update Temp_Employee set " + EarningsList[temp_i].EarningsCode + "=" + EarningsList[temp_i].Amount + " where pn_employeeid=" + EarningsList[temp_i].EmployeeId + "";

                employee.Temp_Employee(temp_string);


            }


        }


        //Employee Deductions


        DeductionList = pay.Temp_Deductions2();


        if (DeductionList.Count > 0)
        {

            for (temp_i = 0; temp_i < DeductionList.Count; temp_i++)
            {

                temp_string = "update Temp_Employee set " + DeductionList[temp_i].DeducationCode + "=" + DeductionList[temp_i].Amount + " where pn_employeeid=" + DeductionList[temp_i].EmployeeId + "";

                employee.Temp_Employee(temp_string);


            }


        }





    }


    //public string WorkDetails_query(string t_str)
    //{
    //    temp_string = "update Temp_Employee set JoiningDate='" + Check_NullDate(EmpWorkList[temp_i].d_join.ToShortDateString()) + "',OfferDate='" + Check_NullDate(EmpWorkList[temp_i].d_Offer.ToShortDateString()) + "',ProbationUpto='" + Check_NullDate(EmpWorkList[temp_i].d_probotion.ToShortDateString()) + "',ExtendedUpto='" + Check_NullDate(EmpWorkList[temp_i].d_extended.ToShortDateString()) + "',ConfirmationDate='" + Check_NullDate(EmpWorkList[temp_i].d_confirmation.ToShortDateString()) + "',RetirementDate='" + Check_NullDate(EmpWorkList[temp_i].d_retirement.ToShortDateString()) + "',ContractRenviewDate='" + Check_NullDate(EmpWorkList[temp_i].d_renue.ToShortDateString()) + "'";

    //    temp_string = temp_string + " where pn_EmployeeID=" + EmpWorkList[temp_i].EmployeeId + "";

    //    employee.Temp_Employee(temp_string);


    //    count++;
    //    switch (count)
    //    {
    //        case 1: t_str = "";
    //        case 1:

    //        default :


    //    }





    //}



    public void Temp_earnings_Drop()
    {
        temp_string = "drop table temp_earnings";

        employee.Temp_Employee(temp_string);

    }

    public void temp_earnings_create()
    {


        //temp_earnings create

        temp_string = "create table temp_earnings(pn_EmployeeID int)";

        employee.Temp_Employee(temp_string);



        //temp_earnings columns alter


        EmpFirstList = employee.Temp_Emp_Earnings1(employee);

        if (EmpFirstList.Count > 0)
        {
            for (temp_i = 0; temp_i < EmpFirstList.Count; temp_i++)
            {

                temp_string = "alter table temp_earnings add " + EmpFirstList[temp_i].EarningsName + " int";

                employee.Temp_Employee(temp_string);

            }

        }



    }

    public void temp_earnings_load()
    {


        //First Employees load


        EmployeeList = employee.fn_getAllEmployees();

        if (EmployeeList.Count > 0)
        {

            for (temp_i = 0; temp_i < EmployeeList.Count; temp_i++)
            {

                temp_string = "insert into temp_earnings(pn_employeeid) values(" + EmployeeList[temp_i].EmployeeId + ")";

                employee.Temp_Employee(temp_string);


            }


        }


        EarningsList = pay.Temp_Earnings2();


        if (EarningsList.Count > 0)
        {

            for (temp_i = 0; temp_i < EarningsList.Count; temp_i++)
            {

                temp_string = "update temp_earnings set " + EarningsList[temp_i].EarningsCode + "=" + EarningsList[temp_i].Amount + " where pn_employeeid=" + EarningsList[temp_i].EmployeeId + "";

                employee.Temp_Employee(temp_string);


            }


        }




    }


    public void Temp_deductions_Drop()
    {
        temp_string = "drop table temp_deductions";

        employee.Temp_Employee(temp_string);

    }

    public void temp_deductions_create()
    {

        //temp_deductions create

        temp_string = "create table temp_deductions(pn_EmployeeID int)";

        employee.Temp_Employee(temp_string);


        //temp_deductions columns alter


        EmpFirstList = employee.Temp_Emp_Deductions1(employee);

        if (EmpFirstList.Count > 0)
        {
            for (temp_i = 0; temp_i < EmpFirstList.Count; temp_i++)
            {

                temp_string = "alter table temp_deductions add " + EmpFirstList[temp_i].DeducationCode + " int";

                employee.Temp_Employee(temp_string);
            }

        }



    }

    public void temp_deductions_load()
    {


        //First Employees load


        EmployeeList = employee.fn_getAllEmployees();

        if (EmployeeList.Count > 0)
        {

            for (temp_i = 0; temp_i < EmployeeList.Count; temp_i++)
            {

                temp_string = "insert into temp_deductions(pn_employeeid) values(" + EmployeeList[temp_i].EmployeeId + ")";

                employee.Temp_Employee(temp_string);


            }


        }


        DeductionList = pay.Temp_Deductions2();


        if (DeductionList.Count > 0)
        {

            for (temp_i = 0; temp_i < DeductionList.Count; temp_i++)
            {

                temp_string = "update temp_deductions set " + DeductionList[temp_i].DeducationCode + "=" + DeductionList[temp_i].Amount + " where pn_employeeid=" + DeductionList[temp_i].EmployeeId + "";

                employee.Temp_Employee(temp_string);


            }


        }

    }


    public void Temp_leave_Drop()
    {
        temp_string = "drop table temp_leave";

        employee.Temp_Employee(temp_string);

    }

    public void temp_leave_create()
    {

        //temp_leave create

        temp_string = "create table temp_leave(pn_EmployeeID int,n_year int,n_month varchar(20))";

        employee.Temp_Employee(temp_string);

        //temp_leave columns alter

        LeaveList = l.fn_paym_leave(l);

        if (LeaveList.Count > 0)
        {
            for (temp_i = 0; temp_i < LeaveList.Count; temp_i++)
            {

                temp_string = "alter table temp_leave add " + LeaveList[temp_i].leaveCode + " int,Availd_" + LeaveList[temp_i].leaveCode + " int";

                employee.Temp_Employee(temp_string);
            }

        }



    }



 //***************************************************************************************************

       


   
    public void Temp_Earnings_Load()
    {

        //Earnings Monthly

        temp_earn = "select * from paym_Emp_Earnings where pn_EarningsID in( select pn_EarningsID from paym_Earnings where c_Regular='0')";

        EarningsList = pay.Temp_Earnings(temp_earn);

        temp_string = "";

        if (EarningsList.Count > 0)
        {

            for (temp_i = 0; temp_i < EarningsList.Count; temp_i++)
            {
                temp_string = "insert into Temp_MEarnings(pn_CompanyID,pn_BranchID,pn_EmployeeID,pn_EarningsID,n_Amount,d_Date,c_eligible) values(";
                temp_string = temp_string + "" + EarningsList[temp_i].CompanyId + "," + EarningsList[temp_i].BranchId + "," + EarningsList[temp_i].EmployeeId + "," + EarningsList[temp_i].EarningsId + ",";
                temp_string = temp_string + "'" + EarningsList[temp_i].Amount + "','" + EarningsList[temp_i].Date + "','" + EarningsList[temp_i].regular + "')";

                employee.Temp_Employee(temp_string);

                temp_string = "";
            }

        }


        //Employee Yearly

        temp_earn = "select * from paym_Emp_Earnings where pn_EarningsID in( select pn_EarningsID from paym_Earnings where c_Regular='1')";

        EarningsList = pay.Temp_Earnings(temp_earn);

        temp_string = "";

        if (EarningsList.Count > 0)
        {

            for (temp_i = 0; temp_i < EarningsList.Count; temp_i++)
            {
                temp_string = "insert into Temp_YEarnings(pn_CompanyID,pn_BranchID,pn_EmployeeID,pn_EarningsID,n_Amount,d_Date,c_eligible) values(";
                temp_string = temp_string + "" + EarningsList[temp_i].CompanyId + "," + EarningsList[temp_i].BranchId + "," + EarningsList[temp_i].EmployeeId + "," + EarningsList[temp_i].EarningsId + ",";
                temp_string = temp_string + "'" + EarningsList[temp_i].Amount + "','" + EarningsList[temp_i].Date + "','" + EarningsList[temp_i].regular + "')";

                employee.Temp_Employee(temp_string);

                temp_string = "";
            }

        }

        //Employee statury

        temp_earn = "select * from paym_Emp_Earnings where pn_EarningsID in( select pn_EarningsID from paym_Earnings where c_Regular='2')";

        EarningsList = pay.Temp_Earnings(temp_earn);

        temp_string = "";

        if (EarningsList.Count > 0)
        {

            for (temp_i = 0; temp_i < EarningsList.Count; temp_i++)
            {
                temp_string = "insert into Temp_SEarnings(pn_CompanyID,pn_BranchID,pn_EmployeeID,pn_EarningsID,n_Amount,d_Date,c_eligible) values(";
                temp_string = temp_string + "" + EarningsList[temp_i].CompanyId + "," + EarningsList[temp_i].BranchId + "," + EarningsList[temp_i].EmployeeId + "," + EarningsList[temp_i].EarningsId + ",";
                temp_string = temp_string + "'" + EarningsList[temp_i].Amount + "','" + EarningsList[temp_i].Date + "','" + EarningsList[temp_i].regular + "')";

                employee.Temp_Employee(temp_string);

                temp_string = "";
            }

        }



    }

    public void Temp_Earnings_Delete()
    {
        temp_string = "Delete from Temp_MEarnings";

        employee.Temp_Employee(temp_string);


        temp_string = "Delete from Temp_YEarnings";

        employee.Temp_Employee(temp_string);


        temp_string = "Delete from Temp_SEarnings";

        employee.Temp_Employee(temp_string);


    }   


    protected void btn_start_Click(object sender, ImageClickEventArgs e)
    {

    }



    //***************************************************************************************************

    public string Convert_ToSqlDateformat(string cur_date)
    {
        string _d, _m, _y, sql_date = "";
        char[] splitter ={ '/' };
        string[] str_ary = new string[4];
      

        if (cur_date != "")
        {
            if (cur_date.Length == 10)
            {

                _m = cur_date.Substring(0, 2);
                _d = cur_date.Substring(3, 2);
                _y = cur_date.Substring(6, 4);

                sql_date = _y + "/" + _m + "/" + _d;
            }
            else
            {
                str_ary = cur_date.Split(splitter);


                _m = check_single(str_ary[0]);
                _d = check_single(str_ary[1]);
                _y = str_ary[2];

                sql_date = _y + "/" + _m + "/" + _d;

            }

        }
        else
        {
            sql_date ="1900/01/01";

        }


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



    public string Convert_gender(string p_gender)
    {
        string r_gender = "";

        if (p_gender == "1")
        {
            r_gender = "Male";
        }
        else
        {
            r_gender = "Female";

        }

        return r_gender;


    }

    public string Convert_Salutation(char p_Salutation)
    {
        string r_Salutation = "";

        switch (p_Salutation)
        {
            case '1': r_Salutation = "Mr.";
                break;

            case '2': r_Salutation = "Ms.";
                break;

            case '3': r_Salutation = "Mrs.";
                break;

            case '4': r_Salutation = "Dr.";
                break;

            default: r_Salutation = "";
                break;
        }


        return r_Salutation;


    }

    public string Convert_MaritalStatus(char p_MaritalStatus)
    {
        string r_MaritalStatus = "";

        if (p_MaritalStatus == '1')
        {
            r_MaritalStatus = "Single";
        }
        else
        {
            r_MaritalStatus = "Married";

        }

        return r_MaritalStatus;


    }

  



}
