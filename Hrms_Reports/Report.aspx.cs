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


public partial class TempTable : System.Web.UI.Page
{

    Company company = new Company();
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    Candidate c = new Candidate();
    Leave l = new Leave();

    Collection<Company> CompanyList;

    Collection<Employee> EmployeeList;
    Collection<Employee> EmpFirstList;
    Collection<Employee> EmpGeneralList;
    Collection<Employee> EmpProfileList;
    Collection<Employee> EmpWorkList;   
    Collection<Employee> chkList;
    Collection<Employee> CheckList;
   
    Collection<Leave> LeaveList;
    Collection<Leave> Leave_AllocList;
   
    
    Collection<PayRoll> EarningsList;
    Collection<PayRoll> NonEarningsList;
    Collection<PayRoll> DeductionList;    

    private ReportDocument myReportDocument = new ReportDocument();

    char s_login_role;

    int temp_i=0, i=0, j, count = 0, temp_count = 0, counting = 0, temp_mon = 0, cur_yr, yr_it;
    int i_sin, i_all, i_emp, earn_count = 0, ded_count=0,cur_count=0, nonearn_count;

    string temp_string = "", temp_earn = "", emp_count = "", sel_value="";
    string str_edu = "", s_Report = "", query = "", emp_code="";  

    bool emp_check=false; 
   

    protected void Page_Load(object sender, EventArgs e)
    {
       
        Session["Msg_session"] = "";

        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        c.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        s_login_role = Convert.ToChar(Request.Cookies["Login_temp_Role"].Value);


    
    
       lbl_error.Text = "";
      
        
        if (!IsPostBack)
        {

           
            //chk_month.Visible = false;
            
            row_strngth.Visible = false;
            row_leave_year.Visible = false;
            row_leave_month.Visible = false;
            
            div_chk_Master.Visible = false;
           // chk_all_master.Visible = false;

            CompanyList = company.fn_getCompany();
           
                if (CompanyList.Count > 0)
                {
                    ddl_year_load();

                    switch (s_login_role)
                    {

                        case 'a': admin();
                                  session_check();
                                  break;
                              case 'h':
                                  hr();
                                  session_check();
                                  break;

                              case 'u': hr();
                                  session_check();
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

    public void admin()
    {
        if (Convert.ToString(Session["ses_report"]) == "1") 
        {
            EmployeeList = employee.fn_getAllEmployees();
        }
        else
        {
            EmployeeList = employee.fn_getOldEmployees();
        }

        if (EmployeeList.Count > 0)
        {
            chk_Empcode.DataSource = EmployeeList;
            chk_Empcode.DataTextField = "LastName";
            chk_Empcode.DataValueField = "EmployeeId";
            chk_Empcode.DataBind();
        }
        else
        {
            lbl_error.Text = "No employees";

        }


        //employee.temp_str = "select * from Temp_Employee";

        //EmpFirstList = employee.Temp_Selected_EmployeeList(employee);

        //if (EmpFirstList.Count == 0)
        //{
        //    temp_tables();

        //}  


    }

    public void hr()
    {
        try
        {
           

            if (Convert.ToString(Session["ses_report"]) == "1")
            {
                EmployeeList = employee.fn_getEmployeeList(employee);
            }
            else
            {
                EmployeeList = employee.fn_getOldEmployeeList(employee);

            }


                if (EmployeeList.Count > 0)
                {
                    chk_Empcode.DataSource = EmployeeList;
                    chk_Empcode.DataTextField = "LastName";
                    chk_Empcode.DataValueField = "EmployeeId";
                    chk_Empcode.DataBind();
                }
                else
                {
                    lbl_error.Text = "No employees";

                }

          
           

        }
        catch (Exception ex)
        {
            lbl_error.Text = "Error";
        }
    }
    
    
    public void session_check()
    {

        switch (Convert.ToString(Session["Query_Session"]))
        {

            case "start":
                lbl_error.Text = "Welcome To Report Section!";
                Session["Query_Session"] = "start";
                break;

            case "nil":
                lbl_error.Text = "No Result Found";
                Session["Query_Session"] = "start";
                break;

            case "back":
                lbl_error.Text = "";
                Session["Query_Session"] = "start";
                break;

            default:
                final_query_execute();
                break;

        }


    }

    public void final_query_execute()
    {

        employee.temp_str = (string)Session["Query_Session"];

        EmployeeList = employee.Temp_Selected_EmployeeList(employee);

        if (EmployeeList.Count > 0)
        {

            for (i = 0; i < chk_Empcode.Items.Count; i++)
            {
                for (j = 0; j < EmployeeList.Count; j++)
                {
                    if (Convert.ToInt32(chk_Empcode.Items[i].Value) == EmployeeList[j].EmployeeId)
                    {
                        chk_Empcode.Items[i].Selected = true;
                    }

                }
            }

            lbl_error.Text = EmployeeList.Count + " Employees Selected!";
            Session["Query_Session"] = "start";

        }
        else
        {
            lbl_error.Text = "No Employees has been selected";
            Session["Query_Session"] = "start";
        }



    }


    public void temp_tables()
    {
        col_refresh.Visible = false;

        Temp_employee_Drop();
        Temp_employee_Create();
        Temp_employee_Load();

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

        col_refresh.Visible = true;
    }      

    public void checked_All()
    {
        for (i = 0; i < chk_Empcode.Items.Count; i++)
        {
            chk_Empcode.Items[i].Selected = true;           
        }

    }

    protected void btn_Query_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Search.aspx");
    }   


    public bool emp_selection()
    {
       
            for (i = 0; i < chk_Empcode.Items.Count; i++)
            {
                if (chk_Empcode.Items[i].Selected == true)
                {
                    emp_check = true;

                    break;

                }
            }

            return emp_check;
    }
   
    protected void ddl_r_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            row_leave_year.Visible = false;
            row_leave_month.Visible = false;
            row_strngth.Visible = false;
            div_chk_Master.Visible = false;
            //chk_all_master.Visible = false;

            chk_all_master.Checked = false;
            //chkall.Checked = false;


            chk_Master.Items.Clear();

            ViewState["ddl_rep_value"] = Convert.ToInt32(ddl_r_Type.SelectedItem.Value);  

            if (Convert.ToInt32(ddl_r_Type.SelectedItem.Value) != 10)
            {
               
                if (emp_selection() == true)
                {
                    div_chk_Master.Visible = true;
                    //chk_all_master.Visible = true;
                    //Session["ddl_rep"] = Convert.ToInt32(ddl_r_Type.SelectedItem.Value);                                   
                   
                    switch (Convert.ToInt32(ddl_r_Type.SelectedItem.Value))
                    {
                        case 0:
                            div_chk_Master.Visible = false;
                            //chk_all_master.Visible = false;
                            lbl_error.Text = "Select a Report";
                            break;

                        case 1:
                            s_Report = "sp_columns temp_employee";
                            chkList = employee.Temp_checkList(s_Report);

                            if (chkList.Count > 0)
                            {
                                for (temp_count = 2; temp_count < chkList.Count; temp_count++)
                                {
                                    if (temp_count != 6 && temp_count != 8 && temp_count != 20)
                                    {
                                        ListItem list = new ListItem();

                                        list.Text = chkList[temp_count].EmployeeCode;
                                        list.Value = chkList[temp_count].EmployeeCode;
                                        chk_Master.Items.Add(list);
                                    }

                                }

                            }

                            break;

                        case 2:
                            //chk_Master.Items.Add("EmployeeName");
                            chk_Master.Items.Add("CourseName");
                            chk_Master.Items.Add("SpecializationName");
                            chk_Master.Items.Add("InstitutionName");
                            chk_Master.Items.Add("Percentage");
                            chk_Master.Items.Add("CompletedYear");
                            chk_Master.Items.Add("Mode");
                            chk_Master.Items.Add("Information");

                            break;

                        case 3:
                            //chk_Master.Items.Add("EmployeeName");
                            chk_Master.Items.Add("SkillName");
                            chk_Master.Items.Add("Experience");
                            chk_Master.Items.Add("Proficiency");

                            break;

                        case 4:
                            //chk_Master.Items.Add("EmployeeName");
                            chk_Master.Items.Add("FromMonth");
                            chk_Master.Items.Add("FromYear");
                            chk_Master.Items.Add("ToMonth");
                            chk_Master.Items.Add("ToYear");
                            chk_Master.Items.Add("CompanyName");
                            chk_Master.Items.Add("CompanyLocation");
                            chk_Master.Items.Add("DesignationCode");
                            chk_Master.Items.Add("Salary");
                            chk_Master.Items.Add("Role");
                            chk_Master.Items.Add("Responsibility");

                            break;

                        case 5:
                            //chk_Master.Items.Add("EmployeeName");
                            chk_Master.Items.Add("InstitutionName");
                            chk_Master.Items.Add("Programtype");
                            chk_Master.Items.Add("TrainerName");
                            chk_Master.Items.Add("ProgramName");
                            chk_Master.Items.Add("DurationFrom");
                            chk_Master.Items.Add("DurationTo");

                            break;

                        case 6:
                            row_leave_year.Visible = true;
                            row_leave_month.Visible = true;

                            s_Report = "sp_columns temp_leave";
                            chkList = employee.Temp_checkList(s_Report);

                            if (chkList.Count > 0)
                            {
                                //chk_Master.Items.Add("EmployeeName");

                                for (temp_count = 0; temp_count < chkList.Count; temp_count++)
                                {
                                    if (temp_count > 2)
                                    {
                                        ListItem list = new ListItem();

                                        list.Text = chkList[temp_count].EmployeeCode;
                                        list.Value = chkList[temp_count].EmployeeCode;
                                        chk_Master.Items.Add(list);
                                    }

                                }

                            }

                            break;

                        case 7: //lbl_error.Text = "Appraisal";

                            chk_Master.Items.Add("AppraisalName");
                            chk_Master.Items.Add("Points");
                            break;

                        case 8:
                            s_Report = "sp_columns temp_employee";
                            chkList = employee.Temp_checkList(s_Report);

                             EarningsList = pay.fn_Earnings(pay);
                             earn_count = EarningsList.Count;

                             //DeductionList = pay.fn_Deduction();
                             //ded_count = DeductionList.Count;

                             //from 65 bysan
                             cur_count = 69 + earn_count;

                             if (earn_count > 0)
                             {

                                 if (chkList.Count > 0)
                                 {
                                     //chk_Master.Items.Add("EmployeeName");
                                     //from 65 bysan
                                     for (temp_count = 68; temp_count < cur_count; temp_count++)
                                     {
                                        
                                             ListItem list = new ListItem();
                                             list.Text = chkList[temp_count].EmployeeCode;
                                             list.Value = chkList[temp_count].EmployeeCode;
                                             chk_Master.Items.Add(list);
                                        

                                     }

                                 }
                             }
                             else
                             {

                                 lbl_error.Text = "No Earnings";


                             }

                            break;

                        case 9:
                            s_Report = "sp_columns temp_employee";
                            chkList = employee.Temp_checkList(s_Report);

                            EarningsList = pay.fn_Earnings(pay);
                            earn_count = EarningsList.Count;
                            //bysan
                            NonEarningsList = pay.fn_NonEarnings();
                            nonearn_count = NonEarningsList.Count;
                            //
                            DeductionList = pay.fn_Deduction(pay);
                            ded_count = DeductionList.Count;
                            //set 69 value for avoid the column in temp_employee
                            cur_count = 68 + earn_count + nonearn_count + ded_count;

                            if (ded_count > 0)
                            {


                            if (chkList.Count > 0)
                            {
                                //chk_Master.Items.Add("EmployeeName");
                                //from 65 to 68 less than 69 bcoz the chklist count from 0.. bysan
                                for (temp_count = 68 + earn_count + nonearn_count; temp_count < cur_count; temp_count++)
                                {
                                    if (temp_count > 0)
                                    {
                                        ListItem list = new ListItem();
                                        list.Text = chkList[temp_count].EmployeeCode;
                                        list.Value = chkList[temp_count].EmployeeCode;
                                        chk_Master.Items.Add(list);
                                    }

                                }

                            }

                        }
                        else
                        {

                            lbl_error.Text = "No Deduction";


                        }


                            break;

                        //case 10: row_strngth.Visible = true;
                        //    break;




                        default: lbl_error.Text = "";
                            break;

                    }


                   // ddl_r_Type.SelectedIndex = 0;

                }
                else
                {
                    lbl_error.Text = "Select atleast one Employee";

                    ddl_r_Type.SelectedIndex = 0;

                }
            }
            else
            {
                //div_chk_Master.Visible = false;
                row_strngth.Visible = true;

                ddl_r_Type.SelectedIndex = 0;

            }



        }
        catch (Exception ex)
        {
            //lblmsg.Text = "Report not generated";
        }
    }

    protected void ddl_masters_SelectedIndexChanged(object sender, EventArgs e)
    {
        div_chk_Master.Visible = true;
        //chk_all_master.Visible = true;

        ViewState["ddlvalue"] = Convert.ToInt32(ddl_masters.SelectedItem.Value);

        switch (Convert.ToInt32(ddl_masters.SelectedItem.Value))
        {

            case 1: CheckList = employee.fn_Department(employee.BranchId);
                chklist_load("DepartmentId", "DepartmentName");
                break;

            case 2: CheckList = employee.fn_Division();
                chklist_load("DivisionId", "DivisionName");
                break;

            case 3: CheckList = employee.fn_projectsite();
                chklist_load("ProjectsiteId", "ProjectsiteName");
                break;

            case 4: CheckList = employee.fn_Designation();
                chklist_load("DesignationId", "DesignationName");
                break;

            case 5: CheckList = employee.fn_Grade();
                chklist_load("GradeId", "GradeName");
                break;

            case 6: CheckList = employee.fn_Category();
                chklist_load("CategoryId", "CategoryName");
                break;

            case 7: CheckList = employee.fn_JobStatus();
                chklist_load("JobStatusId", "JobStatusName");
                break;

            case 8: CheckList = employee.fn_Shift();
                chklist_load("ShiftId", "ShiftName");
                break;

            case 9: CheckList = employee.fn_Level();
                chklist_load("LevelId", "LevelName");
                break;

            default: lbl_error.Text = "No Items Selected";
                break;


        }


    }

    protected void btn_Report_Click(object sender, ImageClickEventArgs e)
    {
        //Temp_emp_Delete();
        //Temp_emp_Load();

        try
        {

            Employee_count();

            switch (Convert.ToInt32(ViewState["ddl_rep_value"]))
            {
                case 0: lbl_error.Text = "Select a Report";
                    break;

                case 1: f_general();

                    break;

                case 2: f_Qualification();

                    break;

                case 3: f_skills();

                    break;

                case 4: f_work_experience();

                    break;

                case 5: f_training();

                    break;
                case 6: Temp_leave_delete();
                    Temp_leave_load();
                    f_leave();

                    break;

                case 7: //lbl_error.Text = "Appraisal";
                    f_appraisal();

                    break;

                case 8:
                    f_earnings();
                    break;

                case 9:
                    f_deductions();
                    break;

                case 10:
                    f_strength();
                    break;


                default: lbl_error.Text = "";
                    break;

            }

        }
        catch (Exception ex)
        {

            lbl_error.Text = "Report not created";


        }
    }

    protected void chk_all_master_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_all_master.Checked == true)
        {

            for (i = 0; i < chk_Master.Items.Count; i++)
            {
                chk_Master.Items[i].Selected = true;
            }
        }
        else
        {
            for (i = 0; i < chk_Master.Items.Count; i++)
            {
                chk_Master.Items[i].Selected = false;
            }

        }
    }

    protected void chkall_CheckedChanged(object sender, EventArgs e)
    {
        if (chkall.Checked == true)
        {

            for (i = 0; i < chk_Empcode.Items.Count; i++)
            {
                chk_Empcode.Items[i].Selected = true;
            }
        }
        else
        {
            for (i = 0; i < chk_Empcode.Items.Count; i++)
            {
                chk_Empcode.Items[i].Selected = false;
            }

        }

    }

    public void f_general()
    {

        temp_count = 0;
        query = "";

        for (i = 0; i < chk_Master.Items.Count; i++)
        {
            if (chk_Master.Items[i].Selected == true)
            {
                if (temp_count == 0)
                {
                   
                    if (chk_Master.Items[i].Value == "DateofBirth" || chk_Master.Items[i].Value == "JoiningDate" || chk_Master.Items[i].Value == "OfferDate" || chk_Master.Items[i].Value == "ProbationUpto" || chk_Master.Items[i].Value == "ExtendedUpto" || chk_Master.Items[i].Value == "ConfirmationDate" || chk_Master.Items[i].Value == "RetirementDate" || chk_Master.Items[i].Value == "ContractRenviewDate")
                    {

                        query = "convert(varchar(15)," + chk_Master.Items[i].Value + ",103) as " + chk_Master.Items[i].Value + ""; 
                       
                    }
                    else
                    {
                        query = chk_Master.Items[i].Value;

                    }

                    temp_count++;
                }
                else
                {
                    if (chk_Master.Items[i].Value == "DateofBirth" || chk_Master.Items[i].Value == "JoiningDate" || chk_Master.Items[i].Value == "OfferDate" || chk_Master.Items[i].Value == "ProbationUpto" || chk_Master.Items[i].Value == "ExtendedUpto" || chk_Master.Items[i].Value == "ConfirmationDate" || chk_Master.Items[i].Value == "RetirementDate" || chk_Master.Items[i].Value == "ContractRenviewDate")
                    {
                        query = query + "," + "convert(varchar(15)," + chk_Master.Items[i].Value + ",103) as " + chk_Master.Items[i].Value + "";

                    }
                    else
                    {
                        query = query + "," + chk_Master.Items[i].Value;
                    }

                   

                }

            }

        }

        if (temp_count > 0)
        {

            if (emp_count != "")
            {
                query = "select EmployeeCode,Employee_Full_Name," + query + " from temp_employee where pn_EmployeeID in(" + emp_count + ")";

                Session["Final_Query"] = query;

                Response.Redirect("Excel_Export.aspx");
            }
            else
            {
                lbl_error.Text = "No Employee selected";

            }

        }
        else
        {
            lbl_error.Text = "Select Checkboxlist items";

        }


    }

    public void f_Qualification()
    {

        temp_count = 0;
        query = "";

        for (i = 0; i < chk_Master.Items.Count; i++)
        {
            if (chk_Master.Items[i].Selected == true)
            {
                if (temp_count == 0)
                {
                    query =switch_Qualification(chk_Master.Items[i].Value);

                    temp_count++;
                }
                else
                {
                    query = query + "," + switch_Qualification(chk_Master.Items[i].Value);

                }

            }

        }

        if (temp_count > 0)
        {

            if (emp_count != "")
            {

                query = "select emp.EmployeeCode,emp.Employee_Full_Name," + query + " from paym_Employee_Education ed,hrmm_Course c,hrmm_Specialization s,paym_Employee emp ";
                query = query + "where ed.pn_EmployeeID in(" + emp_count + ") and c.pn_CourseID = ed.pn_CourseID and ";
                query = query + "s.pn_SpecializationId=ed.pn_SpecializationID and emp.pn_EmployeeID=ed.pn_EmployeeID";

                Session["Final_Query"] = query;

                Response.Redirect("Excel_Export.aspx");
            }
            else
            {
                lbl_error.Text = "No Employee selected";

            }

        }
        else
        {
            lbl_error.Text = "Select Checkboxlist items";

        }


      

    }

    public void f_skills()
    {

        temp_count = 0;
        query = "";

        for (i = 0; i < chk_Master.Items.Count; i++)
        {
            if (chk_Master.Items[i].Selected == true)
            {
                if (temp_count == 0)
                {
                    query =switch_skills(chk_Master.Items[i].Value);

                    temp_count++;
                }
                else
                {
                    query = query + "," + switch_skills(chk_Master.Items[i].Value);

                }

            }

        }

        if (temp_count > 0)
        {

            if (emp_count != "")
            {

                query = "select emp.EmployeeCode,emp.Employee_Full_Name," + query + " from paym_Employee_Skills ed,hrmm_SkillsMaster c,paym_Employee emp ";
                query = query + "where ed.pn_EmployeeID in("+emp_count+") and c.pn_SkillID=ed.pn_SkillID and emp.pn_EmployeeID=ed.pn_EmployeeID";

                Session["Final_Query"] = query;

                Response.Redirect("Excel_Export.aspx");
            }
            else
            {
                lbl_error.Text = "No Employee selected";

            }

        }
        else
        {
            lbl_error.Text = "Select Checkboxlist items";

        }


    }

    public void f_work_experience()
    {
        
        temp_count = 0;
        query = "";

        for (i = 0; i < chk_Master.Items.Count; i++)
        {
            if (chk_Master.Items[i].Selected == true)
            {
                if (temp_count == 0)
                {
                    query = switch_work_experience(chk_Master.Items[i].Value);
                    temp_count++;
                }
                else
                {
                    query = query + "," + switch_work_experience(chk_Master.Items[i].Value);
                }

            }

        }

        if (temp_count > 0)
        {
            if (emp_count != "")
            {
                query = "select emp.EmployeeCode,emp.Employee_Full_Name," + query + " from paym_Employee_WorkHistory ew,paym_Employee emp where ew.pn_EmployeeID in(" + emp_count + ") and emp.pn_EmployeeID=ew.pn_EmployeeID";

                Session["Final_Query"] = query;

                Response.Redirect("Excel_Export.aspx");
            }
            else
            {
                lbl_error.Text = "No Employee selected";

            }

        }
        else
        {
            lbl_error.Text = "Select Checkboxlist items";

        }




    }

    public void f_training()
    {

        temp_count = 0;
        query = "";

        for (i = 0; i < chk_Master.Items.Count; i++)
        {
            if (chk_Master.Items[i].Selected == true)
            {
                if (temp_count == 0)
                {
                    query = switch_Training(chk_Master.Items[i].Value);

                    temp_count++;
                }
                else
                {
                    query = query + "," + switch_Training(chk_Master.Items[i].Value);

                }

            }

        }

        if (temp_count > 0)
        {

            if (emp_count != "")
            {

                query = "select emp.EmployeeCode,emp.Employee_Full_Name," + query + "";
                query =query+" from paym_training t,paym_trninst ti,paym_instpgmtype tpt,paym_instpgmtrnr tt,paym_instpgmname tpn,paym_Employee emp";
                query =query+" where t.pn_EmployeeID in("+emp_count+") and ti.pn_trninstID=t.fn_trninstID and tpt.pn_pgrmtypeID=t.fn_pgrmtypeID";
                query =query+" and tt.pn_pgmtrnrNameID=t.fn_pgmtrnrNameID and tpn.pn_pgrmNameID=t.fn_pgrmNameID and emp.pn_EmployeeID=t.pn_EmployeeID";


                Session["Final_Query"] = query;

                Response.Redirect("Excel_Export.aspx");
            }
            else
            {
                lbl_error.Text = "No Employee selected";

            }

        }
        else
        {
            lbl_error.Text = "Select Checkboxlist items";

        }




    }

    public void f_appraisal()
    {
        temp_count = 0;
        query = "";

        for (i = 0; i < chk_Master.Items.Count; i++)
        {
            if (chk_Master.Items[i].Selected == true)
            {
                if (temp_count == 0)
                {
                    query = switch_Appraisal (chk_Master.Items[i].Value);

                    temp_count++;
                }
                else
                {
                    query = query + "," + switch_Appraisal(chk_Master.Items[i].Value);
                }
            }

        }
        if (temp_count > 0)
        {

            if (emp_count != "")
            {
                query = "select emp.EmployeeCode,emp.Employee_Full_Name," + query;
                query += " from paym_employee emp, paym_emp_appraisal emp_app, paym_appraisal app where ";
                query += "emp_app.pn_EmployeeID in(" + emp_count + ") and emp_app.pn_EmployeeID=emp.pn_EmployeeID and app.pn_AppraisalID=emp_app.pn_AppraisalID";
                
                Session["Final_Query"] = query;

                Response.Redirect("Excel_Export.aspx");
            }
            else
            {
                lbl_error.Text = "No Employee selected";

            }

        }
        else
        {
            lbl_error.Text = "Select Checkboxlist items";

        }

    }

    public string switch_Appraisal(string str_app)
    {
        //ret_exp = "ew.v_CompanyName as CompanyName";

        string ret_app;

        switch (str_app)
        {
            case "AppraisalName":
                ret_app = " app.v_AppraisalName as AppraisalName";
                break;
           
            case "Points":
                ret_app = "emp_app.n_points as Points";
                break;
            
            default:
                ret_app = "points as Points";
                break;
        }
        return ret_app;

    }

    public void f_earnings()
    {

        temp_count = 0;
        query = "";

        for (i = 0; i < chk_Master.Items.Count; i++)
        {
            if (chk_Master.Items[i].Selected == true)
            {
                if (temp_count == 0)
                {
                    query =chk_Master.Items[i].Value;

                    temp_count++;
                }
                else
                {
                    query = query + "," + chk_Master.Items[i].Value;

                }

            }

        }

        if (temp_count > 0)
        {

            if (emp_count != "")
            {

                //query = "select emp.EmployeeCode," + query + " from temp_employee earn,paym_Employee emp ";
                //query = query + "where earn.pn_EmployeeID in(" + emp_count + ") and emp.pn_EmployeeID=earn.pn_EmployeeID";


                query = "select EmployeeCode,Employee_First_Name as EmployeeName," + query + " from temp_employee";
                query = query + " where pn_EmployeeID in(" + emp_count + ")";
               

                Session["Final_Query"] = query;

                Response.Redirect("Excel_Export.aspx");
            }
            else
            {
                lbl_error.Text = "No Employee selected";

            }

        }
        else
        {
            lbl_error.Text = "Select Checkboxlist items";

        }




    }

    public void f_deductions()
    {

        temp_count = 0;
        query = "";

        for (i = 0; i < chk_Master.Items.Count; i++)
        {
            if (chk_Master.Items[i].Selected == true)
            {
                if (temp_count == 0)
                {
                    query =chk_Master.Items[i].Value;

                    temp_count++;
                }
                else
                {
                    query = query + "," + chk_Master.Items[i].Value;

                }

            }

        }

        if (temp_count > 0)
        {

            if (emp_count != "")
            {
                query = "select EmployeeCode,Employee_Full_Name," + query + " from temp_employee";
                query = query + " where pn_EmployeeID in(" + emp_count + ")";
                 
                Session["Final_Query"] = query;

                Response.Redirect("Excel_Export.aspx");
            }
            else
            {
                lbl_error.Text = "No Employee selected";

            }

        }
        else
        {
            lbl_error.Text = "Select Checkboxlist items";

        }
    }

    public void f_leave()
    {

        temp_count = 0;
        query = "";

        for (i = 0; i < chk_Master.Items.Count; i++)
        {
            if (chk_Master.Items[i].Selected == true)
            {
                if (temp_count == 0)
                {
                    query = switch_leave(chk_Master.Items[i].Value);

                    temp_count++;
                }
                else
                {
                    query = query + "," + switch_leave(chk_Master.Items[i].Value);

                }
//                select d.Employee_Full_Name as EmployeeFullName, a.v_leaveName as LeaveName,  
//a.pn_Count as TotalDays, b.n_Count as AllocatedDays, c.Leave_Count as LeaveDays
//from paym_Leave a, paym_leaveallocation b, paym_Employee_leave c, paym_Employee d
//where d.pn_EmployeeID in (9,3,4) and b.pn_EmployeeID=d.pn_EmployeeID and c.pn_EmployeeID=d.pn_EmployeeID
//and b.pn_leaveID in (3) and a.pn_leaveID=b.pn_leaveID and c.pn_leaveID=b.pn_leaveID

            }

        }

        if (temp_count > 0)
        {

            if (emp_count != "")
            {
                query = "select emp.EmployeeCode,emp.Employee_Full_Name,le.n_year as Year,le.n_month as Month," + query + " from temp_leave le,paym_Employee emp ";
                query = query + "where emp.pn_EmployeeID=le.pn_EmployeeID";

                Session["Final_Query"] = query;

                Response.Redirect("Excel_Export.aspx");
            }
            else
            {
                lbl_error.Text = "No Employee selected";

            }

        }
        else
        {
            lbl_error.Text = "Select Checkboxlist items";

        }




    }

    public void f_strength()
    {
       
        temp_count = 0;

        for (i = 0; i < chk_Master.Items.Count; i++)
        {
            if (chk_Master.Items[i].Selected == true)
            {
                if (temp_count == 0)
                {
                    query = "'" + chk_Master.Items[i].Text + "'";

                    temp_count++;
                }
                else
                {
                    query = query + "," + "'" + chk_Master.Items[i].Text + "'";

                }

            }

        }



        if (temp_count > 0)
        {           


            query = "select " + ddl_masters.SelectedItem.Text + "Name,count(employeecode)as Total_Employees from temp_employee where " + ddl_masters.SelectedItem.Text + "Name in (" + query + ") group by " + ddl_masters.SelectedItem.Text + "Name" + " with rollup";

            
            Session["Final_Query"] = query;

            Response.Redirect("Excel_Export.aspx");


        }
        else
        {
            lbl_error.Text = "Select an Option";

        }    



    }
   

    public string switch_Qualification(string str_qual)
    {
        string ret_qual;

        switch (str_qual)
        {

            //case "EmployeeName": ret_qual = "emp.Employee_First_Name as EmployeeName";

            //    break;

            case "CourseName": ret_qual = "c.v_CourseName as CourseName";

                break;
            case "SpecializationName": ret_qual = "s.v_SpecializationName as SpecializationName";

                break;
            case "InstitutionName": ret_qual = "ed.InstitutionName";

                break;
         
            case "Percentage": ret_qual = "ed.Percentage";

                break;
            case "CompletedYear": ret_qual = "ed.CompletedYear";

                break;
            case "Mode": ret_qual = "ed.Mode";

                break;

            case "Information": ret_qual = "ed.Information";

                break;

            default: ret_qual = "";
                break;

        }


        return ret_qual;

    }

    public string switch_skills(string str_skills)
    {      
        string ret_skills;

        switch (str_skills)
        {

            //case "EmployeeName": ret_skills = "emp.Employee_First_Name as EmployeeName";

            //    break;

            case "SkillName": ret_skills = "c.v_SkillName as SkillName";

                break;
            case "Experience": ret_skills = "ed.v_Experience as Experience";

                break;
            case "Proficiency": ret_skills = "ed.v_Proficiency as Proficiency";

                break;


            default: ret_skills = "";
                break;

        }


        return ret_skills;

    }

    public string switch_work_experience(string str_exp)
    {
       
        string ret_exp;

        switch (str_exp)
        {

            //case "EmployeeName": ret_exp = "emp.Employee_First_Name as EmployeeName";

            //    break;

            case "FromMonth": ret_exp = "ew.n_FromDateMonth as FromMonth";

                break;
            case "FromYear": ret_exp = "ew.n_FromDateYear as FromYear";

                break;

            case "ToMonth": ret_exp = "ew.n_ToDateMonth as ToMonth";

                break;
            case "ToYear": ret_exp = "ew.n_ToDateYear as ToYear";

                break;


            case "CompanyName": ret_exp = "ew.v_CompanyName as CompanyName";

                break;

            case "CompanyLocation": ret_exp = "ew.v_CompanyLocation as CompanyLocation";

                break;
            case "DesignationCode": ret_exp = "ew.v_DesignationCode as DesignationCode";

                break;
            case "Salary": ret_exp = "ew.n_Salary as Salary";

                break;


            case "Role": ret_exp = "ew.v_Role as Role";

                break;

            case "Responsibility": ret_exp = "ew.v_Responsibility as Responsibility";

                break;





            default: ret_exp = "";
                break;

        }


        return ret_exp;

    }

    public string switch_Training(string str_train)
    {
        
        string ret_train;

        switch (str_train)
        {

            //case "EmployeeName": ret_train = "emp.Employee_First_Name as EmployeeName";

            //    break;

            case "InstitutionName": ret_train = "ti.v_trninstName as InstitutionName";

                break;
            case "Programtype": ret_train = "tpt.v_pgrmtypeName as Programtype";

                break;
            case "TrainerName": ret_train = "tt.v_pgmtrnrName as TrainerName";

                break;

            case "ProgramName": ret_train = "tpn.v_pgrmName as ProgramName";

                break;
            case "DurationFrom": ret_train = "t.v_DurationFrom as DurationFrom";

                break;

            case "DurationTo": ret_train = "t.v_DurationTo as DurationTo";

                break;

            default: ret_train = "";
                break;

        }


        return ret_train;


    }

    public string switch_earnings(string str_earn)
    {

        string ret_earn;

        switch (str_earn)
        {
            case "EmployeeName": ret_earn = "emp.Employee_First_Name as EmployeeName";

                break;

            default: ret_earn = "earn." + str_earn;
                break;

        }


        return ret_earn;

    }

    public string switch_deductions(string str_ded)
    {

        string ret_ded;

        switch (str_ded)
        {
            case "EmployeeName": ret_ded = "emp.Employee_First_Name as EmployeeName";

                break;

            default: ret_ded = "ded." + str_ded;
                break;

        }


        return ret_ded;

    }

    public string switch_leave(string str_leave)
    {

        string ret_leave;

        switch (str_leave)
        {
            case "EmployeeName": ret_leave = "emp.Employee_First_Name as EmployeeName";

                break;

            default: ret_leave = "le." + str_leave;
                break;

        }


        return ret_leave;

    }



    public void temp_1()
    {
        //Qualification 

        //str_edu = "select emp.EmployeeCode,emp.Employee_First_Name as EmployeeName,c.v_CourseName as CourseName,s.v_SpecializationName as SpecializationName,ed.InstitutionName,ed.Percentage,ed.CompletedYear,ed.Mode,ed.Information from paym_Employee_Education ed,hrmm_Course c,hrmm_Specialization s,paym_Employee emp ";
        //str_edu = str_edu + "where ed.pn_EmployeeID in(337,338) and c.pn_CourseID = ed.pn_CourseID and ";
        //str_edu = str_edu + "s.pn_SpecializationId=ed.pn_SpecializationID and emp.pn_EmployeeID=ed.pn_EmployeeID";

        //Skills

        //str_edu = "select emp.EmployeeCode,emp.Employee_First_Name as EmployeeName,c.v_SkillName as SkillName,ed.v_Experience as Experience,ed.v_Proficiency as Proficiency from paym_Employee_Skills ed,hrmm_SkillsMaster c,paym_Employee emp ";
        //str_edu = str_edu + "where ed.pn_EmployeeID in(337,338) and c.pn_SkillID=ed.pn_SkillID and emp.pn_EmployeeID=ed.pn_EmployeeID";


        //work experience

        //str_edu = "select emp.EmployeeCode,emp.Employee_First_Name as EmployeeName,ew.n_FromDateMonth as FromMonth,ew.n_FromDateYear as FromYear,ew.n_ToDateMonth as ToMonth,";
        //str_edu += "ew.n_ToDateYear as ToYear,ew.v_CompanyName as CompanyName,ew.v_CompanyLocation as CompanyLocation,ew.v_DesignationCode as DesignationCode,";
        //str_edu += "ew.n_Salary as Salary,ew.v_Role as Role,ew.v_Responsibility as Responsibility from paym_Employee_WorkHistory ew,paym_Employee emp where ew.pn_EmployeeID in(337,338) and emp.pn_EmployeeID=ew.pn_EmployeeID";

        //training

        //str_edu = "select emp.EmployeeCode,emp.Employee_First_Name as EmployeeName,ti.v_trninstName as InstitutionName,tpt.v_pgrmtypeName as Programtype,tt.v_pgmtrnrName as TrainerName,tpn.v_pgrmName as ProgramName,t.v_DurationFrom as DurationFrom,t.v_DurationTo as DurationTo";
        //str_edu += " from paym_training t,paym_trninst ti,paym_instpgmtype tpt,paym_instpgmtrnr tt,paym_instpgmname tpn,paym_Employee emp";
        //str_edu += " where t.pn_EmployeeID in(337,338) and ti.pn_trninstID=t.fn_trninstID and tpt.pn_pgrmtypeID=t.fn_pgrmtypeID";
        //str_edu += " and tt.pn_pgmtrnrNameID=t.fn_pgmtrnrNameID and tpn.pn_pgrmNameID=t.fn_pgrmNameID and emp.pn_EmployeeID=t.pn_EmployeeID";



        //ds_report = employee.Temp_Employee_retrive(str_edu);

        //grd_execl.DataSource = ds_report;
        //grd_execl.DataBind();  


    }

    public void Employee_count()
    {

        emp_count = "";             
        count = 0;

        for (i = 0; i < chk_Empcode.Items.Count; i++)
        {
            if (chk_Empcode.Items[i].Selected == true)
            {             
               
                if (count == 0)
                {
                    emp_count = chk_Empcode.Items[i].Value;
                    count++;
                }
                else
                {
                    emp_count = emp_count + "," + chk_Empcode.Items[i].Value;
                }


            }

        }


    }
  
    public void leave_master_Alloc()
    {

        Leave_AllocList = l.fn_emp_leaveAllocation(l);

        if (Leave_AllocList.Count > 0)
        {

            for (i = 0; i < Leave_AllocList.Count; i++)
            {
                if (counting == 0)
                {

                    emp_code = l.fn_paym_leave_code(Leave_AllocList[i].leaveID);

                    //n_month=" + l.month + " and n_year=" + l.year + "
                    str_edu = "insert into temp_leave(pn_employeeid,n_year,n_month," + emp_code + ",Availd_" + emp_code + ") values(" + l.EmployeeID + "," + l.year + ",'" + l.str_month + "'," + Leave_AllocList[i].Count + ",0)";

                    employee.Temp_Employee(str_edu);

                    counting++;


                }
                else
                {
                    emp_code = l.fn_paym_leave_code(Leave_AllocList[i].leaveID);

                    str_edu = "update temp_leave set " + emp_code + "=" + Leave_AllocList[i].Count + ",Availd_" + emp_code + "=0 where pn_employeeid=" + l.EmployeeID + " and n_year=" + l.year + " and n_month='" + l.str_month + "'";

                    employee.Temp_Employee(str_edu);
                }

            }

        }
        else
        {
            Leave_AllocList = l.fn_paym_leave(l);

            if (Leave_AllocList.Count > 0)
            {
                for (i = 0; i < Leave_AllocList.Count; i++)
                {
                    if (counting == 0)
                    {
                        str_edu = "insert into temp_leave(pn_employeeid,n_year,n_month," + Leave_AllocList[i].leaveCode + ",Availd_" + Leave_AllocList[i].leaveCode + ") values(" + l.EmployeeID + "," + l.year + ",'" + l.str_month + "'," + Leave_AllocList[i].Count + ",0)";

                        employee.Temp_Employee(str_edu);

                        counting++;
                    }
                    else
                    {
                        str_edu = "update temp_leave set " + Leave_AllocList[i].leaveCode + "=" + Leave_AllocList[i].Count + ",Availd_" + Leave_AllocList[i].leaveCode + "=0 where pn_employeeid=" + l.EmployeeID + " and n_year=" + l.year + " and n_month='" + l.str_month + "'";

                        employee.Temp_Employee(str_edu);
                    }

                }

            }

        }



    }

    public void ddl_year_load()
    {
        try
        {         

            cur_yr = DateTime.Now.Year;

            cur_yr = cur_yr + 5;

            for (yr_it = 1990; yr_it <= cur_yr; yr_it++)
            {

                ddl_year.Items.Add(Convert.ToString(yr_it));

            }

        }
        catch (Exception ex)
        {
            lbl_error.Text = "Error";

            
        }



    }    


    public void chklist_load(string str_id, string str_name)
    {
        chk_Master.Items.Clear();
        //if (CheckList.Count > 1)bysan
        if (CheckList.Count >= 1)
        {
            chk_Master.DataSource = CheckList;
            chk_Master.DataValueField = str_id;
            chk_Master.DataTextField = str_name;
            chk_Master.DataBind();
        }
        else
        {
            lbl_error.Text = "No Data";
            div_chk_Master.Visible = false;

        }

    }


    public string temp_value(int tem)
    {

        switch (tem)
        {

            case 1:
                sel_value = "pn_DepartmentId";
                break;

            case 2: sel_value = "pn_DivisionId";
                break;

            case 3: sel_value = "pn_projectsiteID";
                break;

            case 4: sel_value = "pn_DesignationId";
                break;
            case 5: sel_value = "pn_GradeId";
                break;

            case 6: sel_value = "pn_CategoryId";
                break;

            case 7: sel_value = "pn_JobStatusID";
                break;

            case 8: sel_value = "pn_ShiftId";
                break;

            case 9: sel_value = "pn_LevelID";
                break;

            default: lbl_error.Text = "No Items Selected";
                break;


        }

        return sel_value;




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

        temp_string = temp_string + "pn_EmployeeID  int not null,EmployeeCode varchar(50),Employee_First_Name varchar(50),Employee_Middle_Name varchar(50),Employee_Last_Name varchar(50),DateofBirth datetime,Password varchar(20),Gender varchar(20),";
        temp_string = temp_string + "CompanyName varchar(50),BranchName varchar(50),DivisionName varchar(50),DepartmentName varchar(50),DesignationName varchar(50),CategoryName varchar(50),GradeName varchar(50),ShiftName varchar(50),JobStatusName varchar(50),LevelName varchar(50),projectsiteName varchar(50),d_Date datetime,v_Reason varchar(500),";
        temp_string = temp_string + "JoiningDate datetime,OfferDate datetime,ProbationUpto datetime,ExtendedUpto datetime,ConfirmationDate datetime,RetirementDate datetime,ContractRenviewDate datetime,";
        temp_string = temp_string + "EmailId varchar(50),AlternateEmailId varchar(50),BloodGroup varchar(20),Religion varchar(50),Nationality varchar(50),PresentHouseNo varchar(20),PresentStreetName varchar(50),PresentAddLine1 varchar(100),PresentAddLine2 varchar(100),PresentCity varchar(50),PresentState varchar(50),PermanentHouseNo varchar(50),";
        temp_string = temp_string + "PermanentStreetName varchar(50),PermanentAddLine1 varchar(100),PermanentAddLine2 varchar(100),PermanentCity varchar(50),PermanentState varchar(50),ph_Office varchar(20),ph_Residence varchar(20),CellNo varchar(20),Fax varchar(20),emgName varchar(30),emgPhone varchar(30),Salutation  varchar(20),M_Status varchar(20),";
        temp_string = temp_string + "FatherName varchar(30),MotherName varchar(30),Children varchar(3),SpouseName varchar(30),Ref1_Name varchar(30),Ref1_Phno varchar(20),Ref1_Email varchar(50),Ref1_Relation varchar(50),Ref2_Name varchar(30),Ref2_Phno varchar(30),Ref2_Email varchar(50),Ref2_Relation varchar(50) )";
        
        
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
                temp_string = "set dateformat dmy;insert into Temp_Employee(CompanyName,BranchName,pn_EmployeeID,EmployeeCode,Employee_First_Name,Employee_Middle_Name,Employee_Last_Name,Gender,DateofBirth,Password) values(";
                temp_string = temp_string + "'" + EmpFirstList[temp_i].CourseName + "','" + EmpFirstList[temp_i].SkillName + "'," + EmpFirstList[temp_i].EmployeeId + ",'" + EmpFirstList[temp_i].EmployeeCode + "',";
                temp_string = temp_string + "'" + EmpFirstList[temp_i].FirstName + "','" + EmpFirstList[temp_i].MiddleName + "','" + EmpFirstList[temp_i].LastName + "','" + Convert_gender(EmpFirstList[temp_i].Gender) + "','" + Convert_ToSqlDateformat(EmpFirstList[temp_i].d_birth.ToShortDateString()) + "','" + EmpFirstList[temp_i].password + "');set dateformat mdy";
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

                temp_string = "set dateformat dmy;update Temp_Employee set DivisionName='" + EmpProfileList[temp_i].DivisionName + "',DepartmentName='" + EmpProfileList[temp_i].DepartmentName + "',DesignationName='" + EmpProfileList[temp_i].DesignationName + "',CategoryName='" + EmpProfileList[temp_i].CategoryName + "',GradeName='" + EmpProfileList[temp_i].GradeName + "',ShiftName='" + EmpProfileList[temp_i].ShiftName + "',JobStatusName='" + EmpProfileList[temp_i].JobStatusName + "',LevelName='" + EmpProfileList[temp_i].LevelName + "',projectsiteName='" + EmpProfileList[temp_i].ProjectsiteName + "',d_Date='" + Convert_ToSqlDateformat(EmpProfileList[temp_i].Date.ToShortDateString()) + "',v_Reason='" + EmpProfileList[temp_i].temp_str + "';set dateformat mdy";

                temp_string = temp_string + " where pn_EmployeeID=" + EmpProfileList[temp_i].EmployeeId + "";

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

                temp_string = "set dateformat dmy;update Temp_Employee set JoiningDate='" + Convert_ToSqlDateformat(EmpWorkList[temp_i].d_join.ToShortDateString()) + "',OfferDate='" + Convert_ToSqlDateformat(EmpWorkList[temp_i].d_Offer.ToShortDateString()) + "',ProbationUpto='" + Convert_ToSqlDateformat(EmpWorkList[temp_i].d_probotion.ToShortDateString()) + "',ExtendedUpto='" + Convert_ToSqlDateformat(EmpWorkList[temp_i].d_extended.ToShortDateString()) + "',ConfirmationDate='" + Convert_ToSqlDateformat(EmpWorkList[temp_i].d_confirmation.ToShortDateString()) + "',RetirementDate='" + Convert_ToSqlDateformat(EmpWorkList[temp_i].d_retirement.ToShortDateString()) + "',ContractRenviewDate='" + Convert_ToSqlDateformat(EmpWorkList[temp_i].d_renue.ToShortDateString()) + "';set dateformat dmy";

                temp_string = temp_string + " where pn_EmployeeID=" + EmpWorkList[temp_i].EmployeeId + "";

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


  //*******************************Secondary load**********************************************************

    public void Temp_leave_delete()
    {
        temp_string = "Delete from temp_leave";

        employee.Temp_Employee(temp_string);

    }

    public void Temp_leave_load()
    {


        for (i_emp = 0; i_emp < chk_Empcode.Items.Count; i_emp++)
        {
            if (chk_Empcode.Items[i_emp].Selected == true)
            {
                for (temp_i = 0; temp_i < chk_month.Items.Count; temp_i++)
                {

                    if (chk_month.Items[temp_i].Selected == true)
                    {                       

                             counting = 0;

                            l.EmployeeID = Convert.ToInt32(chk_Empcode.Items[i_emp].Value);
                            l.year = Convert.ToInt32(ddl_year.SelectedItem.Value);
                            l.month = Convert.ToInt32(chk_month.Items[temp_i].Value);
                            l.str_month = chk_month.Items[temp_i].Text;


                            leave_master_Alloc();

                            //l.year = 2008;
                            //l.month = temp_i;

                            LeaveList = l.fn_leave_PerMonth(l);

                            if (LeaveList.Count > 0)
                            {

                                for (i_all = 0; i_all < LeaveList.Count; i_all++)
                                {

                                    str_edu = "update temp_leave set Availd_" + l.fn_paym_leave_code(LeaveList[i_all].leaveID) + "=" + LeaveList[i_all].Count + " where  pn_employeeid=" + l.EmployeeID + " and n_year=" + l.year + " and n_month='" + l.str_month + "'";
                                    employee.Temp_Employee(str_edu);


                                }

                            }                        
                       


                    }


                }



            }


        }


    }


//*******************************Old**********************************************************


    public void Temp_emp_Delete()
    {
        temp_string = "Delete from Temp_emp";

        employee.Temp_Employee(temp_string);


    }

    public void Temp_emp_Load()
    {

        temp_string = "";

        string temp_d = "raja";
        count = 0;

        for (i = 0; i < chk_Empcode.Items.Count; i++)
        {
            if (chk_Empcode.Items[i].Selected == true)
            {
                temp_string = "insert into Temp_emp(pn_employeeid,pn_employeecode) values(" + chk_Empcode.Items[i].Value + ",'" + temp_d + "')";

                employee.Temp_Employee(temp_string);

                temp_string = "";


                if (count == 0)
                {
                    emp_count = chk_Empcode.Items[i].Value;
                    count++;
                }
                else
                {

                    emp_count = emp_count + "," + chk_Empcode.Items[i].Value;
                }


            }

        }


        //ViewState["sel_temp_emp"] = emp_count;



    }

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

    protected void chkall_months_CheckedChanged(object sender, EventArgs e)
    {
        if (chkall_months.Checked == true)
        {

            for (i = 0; i < chk_month.Items.Count; i++)
            {
                chk_month.Items[i].Selected = true;
            }
        }
        else
        {
            for (i = 0; i < chk_month.Items.Count; i++)
            {
                chk_month.Items[i].Selected = false;
            }

        }
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

                _d = cur_date.Substring(0, 2);
                _m = cur_date.Substring(3, 2);
                _y = cur_date.Substring(6, 4);

                sql_date = _y + "/" + _m + "/" + _d;
            }
            else
            {
                str_ary = cur_date.Split(splitter);


                _d = check_single(str_ary[0]);
                _m = check_single(str_ary[1]);
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
        string r_gender="";

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

    public string Check_NullDate(string p_date)
    {
       
        if (p_date == "01/01/1900")
        {
            p_date = "";
        }

        return p_date;

    }





}
