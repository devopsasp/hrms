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

public partial class Hrms_Company_Default : System.Web.UI.Page
{

    Company company = new Company();
    Employee employee = new Employee();
    Leave l = new Leave();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    Leave leave = new Leave();
    DataSet ds_check_Employee = new DataSet();
    DataSet ds_count = new DataSet();
    Collection<Company> CompanyList;
    Collection<Company> ddlBranchsList;
    Collection<Employee> EmployeeList;
    Collection<Employee> CheckList;
    Collection<Employee> Rec_CheckList;
    Collection<PayRoll> Pay_CheckList;
    Collection<Leave> Leave_CheckList;

    ListItem mas_list;
    string s_login_role, ses_check;
   
    int ddl_i, _id;
    string del = "", del2 = "", user_id, password, sep = "", str_del = "", str_del2 = "";
    string s_form = "",Item="";
    DataSet ds_userrights;

    protected void Page_Load(object sender, EventArgs e)
    {

        s_login_role = Request.Cookies["Login_temp_Role"].Value;

        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        r.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        l.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        lbl_Error.Text = "";
        if (!IsPostBack)
        {
            ddl_load();
        }

    }

    public void ddl_load()
    {
        ses_check = Request.Cookies["ses_Deletion"].Value;
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        tab_deletion.Visible = false;

        switch (ses_check)
        {
            case "e":
                tr2.Visible = true;
                lbl_first.Text = "Select Branch";
                lbl_second.Text = "Select Employee";
                if (s_login_role == "h")
                {

                    tr1.Visible = false;
                    tr2.Visible = true;
                    lbl_first.Visible = false;
                    ddl_first.Visible = false;
                    ddl_second.Items.Clear();
                    employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                    dept();
                }
               else if (s_login_role == "a")
                {
                    tr1.Visible = true;
                    Branch_Load(ddl_branch);
                    
                    lbl_first.Visible = false;
                    ddl_first.Visible = false;
                    ddl_second.Items.Clear();

                }
                else
                {
                    Branch_Load(ddl_first);
                    tab_Entry.Visible = false;
                }
                break;

            case "M":
                lbl_first.Text = "Select Master's Heading";
                lbl_second.Text = "Select Master";
                tr1.Visible = true;
                Master_Load(ddl_first);
                tab_Entry.Visible = false;
                tr.Visible = false;
                tr1.Visible = false;
                tr2.Visible = false;
                break;

            case "B":
                lbl_second.Text = "Select Branch";
                row_first.Visible = false;
             
                Branch_Load(ddl_second);
                tr.Visible = false;
                break;

        }

    }

    public void dept()
    {
        
        EmployeeList = employee.fn_Department(employee.BranchId);
        if (EmployeeList.Count > 0)
        {
            tab_Entry.Visible = true;
            ddl_dept.Items.Clear();
            for (ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {

                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();

                    e_list.Text = "Select Department";
                    e_list.Value = "0";

                    ddl_dept.Items.Add(e_list);


                }
                else
                {

                    ListItem e_list = new ListItem();

                    e_list.Value = EmployeeList[ddl_i].DepartmentId.ToString();
                    e_list.Text = EmployeeList[ddl_i].DepartmentName.ToString();
                    ddl_dept.Items.Add(e_list);

                }

            }


        }
    }

    protected void ddl_first_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ddl_item_value"] = ddl_first.SelectedItem.Value;
        ses_check = Request.Cookies["ses_Deletion"].Value;

        switch (ses_check)
        {
            case "e": Employee_Load();
                tr1.Visible = false;
                tr2.Visible = true;
                break;

            case "M":
                s_login_role = Request.Cookies["Login_temp_Role"].Value;
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                if (s_login_role == "h")
                {
                    Master_values_Load();
                    tr.Visible = false;
                    tr1.Visible = false;
                    tr2.Visible = false;
                }
                else if (s_login_role == "a")
                {
                    //tr.Visible = true;
                    tr1.Visible = false;
                    tr2.Visible = false;
                    //ddl_branch.Items.Clear();
                    ddl_second.Items.Clear();
                    //Branch_Load(ddl_branch);
                    Master_values_Load();
                    //tr1.Visible = true;
                }
                
               
                break;

        }


    }

    protected void btn_delete_Click(object sender, EventArgs e)
    {

        try
        {
            if (ddl_second.SelectedItem.Value != "0")
            {

                ses_check = Request.Cookies["ses_Deletion"].Value;

                switch (ses_check)
                {

                    case "e": ViewState["vs_Item"] = "Employee";
                        lbl_delete.Text = "Continuing Deletion will delete Employee " + ddl_second.SelectedItem.Text + ". Are you Sure?";

                        break;

                    case "M": ViewState["vs_Item"] = "Master";
                        del = Master_Count() + Convert.ToInt32(ddl_second.SelectedItem.Value);
                        ds_count = company.Delete_Count(del);


                        if (ds_count.Tables[0].Rows.Count > 0)
                        {
                            lbl_delete.Text = "Continuing Deletion will affect " + ds_count.Tables[0].Rows[0][0].ToString() + " Employee's. Are you Sure?";

                        }
                        else
                        {
                            lbl_delete.Text = "Are you Sure to Delete?";

                        }

                        break;

                    case "B": ViewState["vs_Item"] = "Branch";
                        del = "select count(*) from paym_employee_profile where pn_BranchID=" + Convert.ToInt32(ddl_second.SelectedItem.Value);
                        ds_count = company.Delete_Count(del);

                        if (ds_count.Tables[0].Rows.Count > 0)
                        {
                            lbl_delete.Text = "Continuing Deletion will affect " + ds_count.Tables[0].Rows[0][0].ToString() + " Employee's. Are you Sure?";

                        }
                        else
                        {
                            lbl_delete.Text = "Are you Sure to Delete?";
                        }

                        break;

                    default: lbl_Error.Text = "Select Any Option";
                        break;
                }


                tab_deletion.Visible = true;
                row_first.Visible = false;
                tab_Entry.Visible = false;

            }
            else
            {
                lbl_Error.Text = "Select Option";

            }

        }
        catch (Exception ex)
        {

            lbl_Error.Text = "Error";

        }

    }

    protected void btn_yes_Click(object sender, ImageClickEventArgs e)
    {
       
        try
        {
            del = "";
            _id = Convert.ToInt32(ddl_second.SelectedItem.Value);
            ses_check = Request.Cookies["ses_Deletion"].Value;
            tab_deletion.Visible = false;
            employee.Date = employee.Convert_ToSqlDate(txt_eff_date.Value);

            switch (ses_check)
            {               

                case "e": del = "update paym_Employee set status='N' where pn_EmployeeID=" + _id + "";
                    del2 = "update paym_Employee_WorkDetails set RetirementDate='" + employee.Date + "' where pn_EmployeeID=" + _id + "";
                    company.Delete_All(del);
                    company.Delete_All(del2);

                    break;

                case "M": Master_Update();
                    if (str_del != "" && str_del2 != "")
                    {
                        del = str_del + _id;
                        del2 = str_del2 + _id;
                        company.Delete_All(del);
                        company.Delete_All(del2);
                    }
                    else
                    {
                        lbl_Error.Text = "Error";
                    }
                    break;

                case "B": del = "update paym_Branch set status='N' where pn_BranchID=" + _id + "";
                    del2 = "update paym_Employee set status='N' where n_BranchID=" + _id + "";
                    company.Delete_All(del);
                    company.Delete_All(del2);
                    break;

                default: lbl_Error.Text = "Select Any Option";
                    break;
            }

            if (del != "")
            {
                fn_delete();
               
            }
            else
            {
                lbl_Error.Text = "Select Any Option";
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }



    }

    protected void btn_no_Click(object sender, ImageClickEventArgs e)
    {
        
        ses_check = Request.Cookies["ses_Deletion"].Value;

        tab_deletion.Visible = false;

        switch (ses_check)
        {
            case "e":
                row_first.Visible = true;
                tab_Entry.Visible = true;
                break;

            case "M":
                row_first.Visible = true;
                tab_Entry.Visible = true;
                break;

            case "B": 
                row_first.Visible = false;
                tab_Entry.Visible = true;
                break;

        }
     

        lbl_delete.Text = "";



    }



    

    public void Branch_Load(DropDownList ddl)
    {
        try
        {

            //ddl_first.Items.Clear();

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
                        ddl.Items.Add(list);
                    }
                    else
                    {

                        ListItem list = new ListItem();

                        list.Text = ddlBranchsList[ddl_i].CompanyName;
                        list.Value = ddlBranchsList[ddl_i].CompanyId.ToString();
                        ddl.Items.Add(list);

                    }

                }

            }


        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";


        }


    }

    public void Employee_Load()
    {
        try
        {
            ddl_second.Items.Clear();

            employee.BranchId = Convert.ToInt32(ddl_first.SelectedItem.Value);

            EmployeeList = employee.fn_getEmployeeList(employee);

            if (EmployeeList.Count > 0)
            {
                tab_Entry.Visible = true;
                
                for (ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
                {

                    if (ddl_i == -1)
                    {
                        ListItem e_list = new ListItem();

                        e_list.Text = "Select Employee";
                        e_list.Value = "0";
                        ddl_second.Items.Add(e_list);


                    }
                    else
                    {

                        ListItem e_list = new ListItem();

                        e_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
                        e_list.Text = EmployeeList[ddl_i].LastName.ToString();
                        ddl_second.Items.Add(e_list);

                    }

                }


            }
            else
            {


            }


        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";


        }


    }

    public void Master_Load(DropDownList ddl)
    {
        ddl.Items.Clear();

        mas_list = new ListItem("Select Master", "0");
        ddl.Items.Add(mas_list);
        mas_list = new ListItem("Department", "1");
        ddl.Items.Add(mas_list);
        mas_list = new ListItem("Division", "2");
        ddl.Items.Add(mas_list);
        mas_list = new ListItem("Level", "3");
        ddl.Items.Add(mas_list);
        mas_list = new ListItem("Designation", "4");
        ddl.Items.Add(mas_list);
        mas_list = new ListItem("Grade", "5");
        ddl.Items.Add(mas_list);
        mas_list = new ListItem("Category", "6");
        ddl.Items.Add(mas_list);
        mas_list = new ListItem("JobStatus", "7");
        ddl.Items.Add(mas_list);
        mas_list = new ListItem("Shift", "8");
        ddl.Items.Add(mas_list);
        mas_list = new ListItem("Earnings", "9");
        ddl.Items.Add(mas_list);
        mas_list = new ListItem("Deduction", "10");
        ddl.Items.Add(mas_list);
        mas_list = new ListItem("Skills", "11");
        ddl.Items.Add(mas_list);
        mas_list = new ListItem("Course", "12");
        ddl.Items.Add(mas_list);
        mas_list = new ListItem("Projectsite", "13");
        ddl.Items.Add(mas_list);
        // mas_list = new ListItem("Leave", "14");
        //ddl.Items.Add(mas_list);
        // mas_list = new ListItem("Training Institution", "17");
        //ddl.Items.Add(mas_list);
        // mas_list = new ListItem("Program Type", "18");
        //ddl.Items.Add(mas_list);
        // mas_list = new ListItem("Program Details", "19");
        //ddl.Items.Add(mas_list);
        // mas_list = new ListItem("Trainer Details", "20");
        //ddl.Items.Add(mas_list);


    }

    public void Master_values_Load()
    {
        ddl_second.Items.Clear();
        switch (Convert.ToInt32(ddl_first.SelectedItem.Value))
        {

            case 1: Rec_CheckList = employee.fn_Department(employee.BranchId);
                Recruit_chklist_load("DepartmentID", "DepartmentName");
                break;

            case 2: Rec_CheckList = employee.fn_Division();
                Recruit_chklist_load("DivisionId", "DivisionName");
                break;

            case 3: Rec_CheckList = employee.fn_Level();
                Recruit_chklist_load("LevelId", "LevelName");
                break;

            case 4: Rec_CheckList = employee.fn_Designation();
                Recruit_chklist_load("DesignationId", "DesignationName");
                break;

            case 5: Rec_CheckList = employee.fn_Grade();
                Recruit_chklist_load("GradeId", "GradeName");
                break;

            case 6: Rec_CheckList = employee.fn_Category();
                Recruit_chklist_load("CategoryId", "CategoryName");
                break;

            case 7: Rec_CheckList = employee.fn_JobStatus();
                Recruit_chklist_load("JobStatusId", "JobStatusName");
                break;

            case 8: Rec_CheckList = employee.fn_Shift();
                Recruit_chklist_load("ShiftId", "ShiftName");
                break;

            case 9: Pay_CheckList = pay.fn_Earnings(pay);
                Pay_chklist_load("EarningsId", "EarningsName");
                break;

            case 10: Pay_CheckList = pay.fn_Deduction(pay);
                Pay_chklist_load("DeductionId", "DeductionName");
                break;

            case 11: CheckList = employee.fn_skill();
                chklist_load("SkillId", "SkillName");
                break;

            case 12: CheckList = employee.fn_course();
                chklist_load("CourseId", "CourseName");
                break;


            case 13: Rec_CheckList = employee.fn_projectsite();
                Recruit_chklist_load("ProjectsiteId", "ProjectsiteName");
                break;


            case 14: Leave_CheckList = leave.fn_paym_leave(l);
                Leave_chklist_load("leaveID", "leaveName");
                break;


            case 15: CheckList = employee.fn_getInstList(employee);
                chklist_load("InstitutionId", "InstitutionName");
                break;



            case 16: CheckList = employee.fn_programtype();
                chklist_load("prgmtypId", "prgmtypName");
                break;


            case 17: CheckList = employee.fn_programname(employee);
                chklist_load("prgmid", "prgmname");
                break;


            case 18: CheckList = employee.fn_gettrainerNameList();
                chklist_load("trnrID", "trnrName");
                break;


            default: lbl_Error.Text = "No Items Selected";
                break;


        }


    }

   

    public void Master_Update()
    {        
        str_del = "";
        str_del2 = "";

        switch (Convert.ToInt32(ViewState["ddl_item_value"]))
        {

            case 1: str_del = "update paym_Department set status='N' where pn_DepartmentID=";
                str_del2 = "update paym_employee_profile set pn_DepartmentID=1 where pn_DepartmentID=";
                break;

            case 2: str_del = "update paym_Division set status='N' where pn_DivisionID=";
                str_del2 = "update paym_employee_profile set pn_DivisionID=1 where pn_DivisionID=";
                break;

            case 3: str_del = "update paym_Level set status='N' where pn_LevelID=";
                str_del2 = "update paym_employee_profile set pn_LevelID=1 where pn_LevelID=";
                break;

            case 4: str_del = "update paym_Designation set status='N' where pn_DesignationID=";
                str_del2 = "update paym_employee_profile set pn_DesignationID=1 where pn_DesignationID=";
                break;

            case 5: str_del = "update paym_Grade set status='N' where pn_GradeID=";
                str_del2 = "update paym_employee_profile set pn_GradeID=1 where pn_GradeID=";
                break;

            case 6: str_del = "update paym_Category set status='N' where pn_CategoryID=";
                str_del2 = "update paym_employee_profile set pn_CategoryID=1 where pn_CategoryID=";
                break;

            case 7: str_del = "update paym_JobStatus set status='N' where pn_JobStatusID=";
                str_del2 = "update paym_employee_profile set pn_JobStatusID=1 where pn_JobStatusID=";
                break;

            case 8: str_del = "update paym_Shift set status='N' where pn_ShiftID=";
                str_del2 = "update paym_employee_profile set pn_ShiftID=1 where pn_ShiftID=";
                break;

            case 9: str_del = "update paym_Earnings set status='N' where pn_EarningsID=";
                str_del2 = "update paym_employee_profile set pn_EarningsID=1 where pn_EarningsID=";
                break;

            case 10: str_del = "update paym_Deduction set status='N' where pn_DeductionID=";
                str_del2 = "update paym_employee_profile set pn_DeductionID=1 where pn_DeductionID=";
                break;

            case 11: str_del = "update hrmm_SkillsMaster set status='N' where pn_SkillID=";
                str_del2 = "update paym_employee_profile set pn_SkillID=1 where pn_SkillID=";
                break;

            case 12: str_del = "update hrmm_Course set status='N' where pn_CourseID=";
                str_del2 = "update paym_employee_profile set pn_CourseID=1 where pn_CourseID=";
                break;

            case 13: str_del = "update paym_projectsite set status='N' where pn_projectsiteID=";
                str_del2 = "update paym_employee_profile set pn_projectsiteID=1 where pn_projectsiteID=";
                break;

            //case 14: str_del = "update paym_leave set status='N' where pn_leaveID=";
            //    str_del2 = "update paym_employee_leave set pn_leaveID=1 where pn_leaveID=";
            //    break;

            //case 15: str_del = "update paym_trninst set status='N' where pn_trninstID=";
            //    str_del2 = "update paym_training set fn_trninstID=1 where fn_trninstID=";
            //    break;

            //case 16: str_del = "update paym_instpgmtype set status='N' where pn_pgrmtypeID=";
            //    str_del2 = "update paym_training set fn_pgrmtypeID=1 where fn_pgrmtypeID=";
            //    break;

            //case 17: str_del = "update paym_instpgmname set status='N' where pn_pgrmNameID=";
            //    str_del2 = "update paym_training set fn_pgrmNameID=1 where fn_pgrmNameID=";
            //    break;

            //case 18: str_del = "update paym_instpgmtrnr set status='N' where pn_pgmtrnrNameID=";
            //    str_del2 = "update paym_training set fn_pgmtrnrNameID=1 where fn_pgmtrnrNameID=";
            //    break;

            default: lbl_Error.Text = "";
                break;

        }
        //return str_del + "~" + str_del2;
    }

    public void fn_delete()
    {

        employee.EmployeeCode =Convert.ToString(Session["del_empcode"]);
        employee.Role = Convert.ToChar(s_login_role);
        employee.Item=Convert.ToString(ViewState["vs_Item"]);
        employee.ItemCode=ddl_second.SelectedItem.Text;
        employee.Reason = ddl_reason.SelectedItem.Text;
        employee.temp_str = txt_summary.Value;
        employee.Date= employee.Convert_ToSqlDate(txt_eff_date.Value);
        employee.CurrentDate=Convert.ToDateTime(DateTime.Now.ToShortDateString());
        employee.status='D';

        employee.Deletion(employee);

        lbl_Error.Text = "Sucessfully Deleted";

        Normal_State();

    }

    public string Master_Count()
    {
        str_del = "";

        switch (Convert.ToInt32(ViewState["ddl_item_value"]))
        {

            case 1: str_del = "select count(*) from paym_employee_profile where pn_DepartmentID=";

                break;

            case 2: str_del = "select count(*) from paym_employee_profile where pn_DivisionID=";

                break;

            case 3: str_del = "select count(*) from paym_employee_profile where pn_LevelID=";

                break;

            case 4: str_del = "select count(*) from paym_employee_profile where pn_DesingnationID=";

                break;

            case 5: str_del = "select count(*) from paym_employee_profile where pn_GradeID=";

                break;

            case 6: str_del = "select count(*) from paym_employee_profile where pn_CategoryID=";

                break;

            case 7: str_del = "select count(*) from paym_employee_profile where pn_JobStatusID=";

                break;

            case 8: str_del = "select count(*) from paym_employee_profile where pn_ShiftID=";

                break;

            case 9: str_del = "select count(*) from paym_Emp_Earnings where pn_EarningsID=";

                break;

            case 10: str_del = "select count(*) from paym_Emp_Deduction where pn_DeductionID=";

                break;

            case 11: str_del = "select count(*) from paym_Employee_Skills where pn_SkillID=";

                break;

            case 12: str_del = "select count(*) from paym_Employee_Education where pn_CourseID=";

                break;

            case 13: str_del = "select count(*) from paym_employee_profile where pn_projectsiteID=";

                break;

            //case 14: str_del = "update paym_leave set status='N' where pn_leaveID=";
            //    str_del2 = "update paym_employee_leave set pn_leaveID=1 where pn_leaveID=";
            //    break;

            //case 15: str_del = "update paym_trninst set status='N' where pn_trninstID=";
            //    str_del2 = "update paym_training set fn_trninstID=1 where fn_trninstID=";
            //    break;

            //case 16: str_del = "update paym_instpgmtype set status='N' where pn_pgrmtypeID=";
            //    str_del2 = "update paym_training set fn_pgrmtypeID=1 where fn_pgrmtypeID=";
            //    break;

            //case 17: str_del = "update paym_instpgmname set status='N' where pn_pgrmNameID=";
            //    str_del2 = "update paym_training set fn_pgrmNameID=1 where fn_pgrmNameID=";
            //    break;

            //case 18: str_del = "update paym_instpgmtrnr set status='N' where pn_pgmtrnrNameID=";
            //    str_del2 = "update paym_training set fn_pgmtrnrNameID=1 where fn_pgmtrnrNameID=";
            //    break;

            default: lbl_Error.Text = "";
                break;

        }


        return str_del;
    }


    public void Recruit_chklist_load(string str_id, string str_name)
    {

        ddl_second.Items.Clear();

        if (Rec_CheckList.Count > 0)
        {
            tab_Entry.Visible = true;

            ddl_second.DataSource = Rec_CheckList;
            ddl_second.DataValueField = str_id;
            ddl_second.DataTextField = str_name;
            ddl_second.DataBind();
        }
        else
        {
            lbl_Error.Text = "No Data";

        }


    }

    public void Pay_chklist_load(string str_id, string str_name)
    {

        ddl_second.Items.Clear();

        if (Pay_CheckList.Count > 0)
        {
            tab_Entry.Visible = true;

            ddl_second.DataSource = Pay_CheckList;
            ddl_second.DataValueField = str_id;
            ddl_second.DataTextField = str_name;
            ddl_second.DataBind();
        }
        else
        {
            lbl_Error.Text = "No Data";           

        }

    }

    public void Leave_chklist_load(string str_id, string str_name)
    {

        ddl_second.Items.Clear();

        if (Leave_CheckList.Count > 0)
        {
            tab_Entry.Visible = true;

            ddl_second.DataSource = Leave_CheckList;
            ddl_second.DataValueField = str_id;
            ddl_second.DataTextField = str_name;
            ddl_second.DataBind();
        }
        else
        {
            lbl_Error.Text = "No Data";
           
        }

    }

    public void chklist_load(string str_id, string str_name)
    {
        ddl_second.Items.Clear();

        if (CheckList.Count > 0)
        {
            tab_Entry.Visible = true;

            ddl_second.DataSource = CheckList;
            ddl_second.DataValueField = str_id;
            ddl_second.DataTextField = str_name;
            ddl_second.DataBind();
        }
        else
        {
            lbl_Error.Text = "No Data";          


        }

    }


    public void Normal_State()
    {
        
        ses_check = Request.Cookies["ses_Deletion"].Value;

        tab_deletion.Visible = false;

        switch (ses_check)
        {
            case "e":
                tab_Entry.Visible = false;
                break;

            case "M":
                tab_Entry.Visible = false;
                break;

            case "B": row_first.Visible = false;
                break;

        }

    }


    protected void btn_back_Click(object sender, EventArgs e)
    {
        Response.Redirect("DeletionEntry.aspx");
    }

    public void emp()
    {
        string qry = "Select a.pn_EmployeeID,a.EmployeeCode,a.Employee_First_Name from paym_employee a, paym_employee_profile1 b where a.pn_CompanyID=b.pn_CompanyID and a.pn_BranchID=b.pn_BranchID and a.pn_EmployeeID=b.pn_EmployeeID and b.pn_DepartmentId=" + ddl_dept.SelectedValue + " and b.pn_CompanyID=" + employee.CompanyId + " and b.pn_BranchID=" + employee.BranchId + " and status='Y' order by EmployeeCode ";

        EmployeeList = employee.fn_getEmplist(qry);
        if (EmployeeList.Count > 0)
        {
            tab_Entry.Visible = true;

            for (ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {

                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();

                    e_list.Text = "Select Employee";
                    e_list.Value = "0";
                    ddl_second.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();

                    e_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
                    e_list.Text = EmployeeList[ddl_i].LastName.ToString();
                    ddl_second.Items.Add(e_list);
                }

            }

        }
    }
    protected void ddl_dept_SelectedIndexChanged(object sender, EventArgs e)
    {
        ses_check = Request.Cookies["ses_Deletion"].Value;
        tab_deletion.Visible = false;

        switch (ses_check)
        {
            case "e":
                lbl_first.Text = "Select Branch";
                lbl_second.Text = "Select Employee";
                if (s_login_role == "h")
                {
                    lbl_first.Visible = false;
                    ddl_first.Visible = false;
                    ddl_second.Items.Clear();
                    employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                    emp();
                }
                else if (s_login_role == "a")
                {
                    employee.BranchId = Convert.ToInt32(ddl_branch.SelectedValue);
                    emp();
                }
                else
                {
                    Branch_Load(ddl_first);
                    tab_Entry.Visible = false;
                }
                break;

            case "M":
                lbl_first.Text = "Select Master's Heading";
                lbl_second.Text = "Select Master";
                Master_Load(ddl_first);
                tab_Entry.Visible = false;
                tr.Visible = false;
                tr1.Visible = false;
                tr2.Visible = false;
                break;

            case "B":
                lbl_second.Text = "Select Branch";
                row_first.Visible = false;
                Branch_Load(ddl_second);
                tr.Visible = false;
                break;

        }


        
    }
    protected void ddl_branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        employee.BranchId = Convert.ToInt32(ddl_branch.SelectedValue);
        if (s_login_role == "h")

        {
            
            ddl_dept.Items.Clear();
            dept();
        }
        else if(s_login_role=="a")
        {
            Master_values_Load();
        }
    }
}
