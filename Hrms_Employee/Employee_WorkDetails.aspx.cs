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


public partial class Hrms_Employee_Default4 : System.Web.UI.Page
{

    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    Company company = new Company();
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    //private SqlConnection _con;
    //ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlCommand cmd = new SqlCommand();
    SqlDataReader rea;

    Collection<Employee> EmployeesList;   
    Collection<Employee> emp_ID_List;

    Collection<Employee> EmpProfileList;
    Collection<Employee> EmpWorkList;

    int i, pr_emp;
    string _Value;
    string s_login_role;
    string s_form = "";
    DataSet ds_userrights;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            s_login_role = Request.Cookies["Login_temp_Role"].Value;

            if (s_login_role != "e")
            {

                if (Convert.ToInt32(Request.Cookies["Select_Employee"].Value) == 1)
                {
                    

                    employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
                    employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                    r.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
                    r.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                    pr_emp = Convert.ToInt32(Request.Cookies["preview_emp"].Value);

                    if (!IsPostBack)
                    {


                        if (Request.Cookies["Employee_Code_FirstLastName"].Value != "")
                        {

                            lbl_empcodename.Text = Request.Cookies["Employee_Code_FirstLastName"].Value;
                        }
                        else
                        {
                            lbl_empcodename.Text = "New Employee";

                        }

                        if (Request.Cookies["Profile_Check"].Value == "1")
                        {

                            switch (s_login_role)
                            {

                                case "a": if (pr_emp == 1)
                                    {
                                        employee.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
                                        r.BranchID = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
                                        btn_update.Visible = false;
                                        //row_curexp.Visible = false;
                                        ddl();

                                    }
                                    else
                                    {
                                        btn_save.Visible = false;
                                        btn_skip.Visible = false;
                                        employee.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
                                        r.BranchID = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
                                        employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                                        ddl();
                                        admin();
                                    }
                                    break;

                                case "h": if (pr_emp == 1)
                                    {
                                        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                        r.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                        btn_update.Visible = false;
                                        ddl();
                                    }
                                    else
                                    {
                                        btn_save.Visible = false;
                                        btn_skip.Visible = false;
                                        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                        r.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                        employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                                        ddl();
                                        admin();
                                    }
                                    break;


                                case "u": s_form = "36";

                                    ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                                    if (ds_userrights.Tables[0].Rows.Count > 0)
                                    {
                                        if (pr_emp == 1)
                                        {
                                            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                            r.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                            btn_update.Visible = false;
                                            //row_curexp.Visible = false;
                                            ddl();
                                        }
                                        else
                                        {
                                            btn_save.Visible = false;
                                            btn_skip.Visible = false;
                                            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                            r.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                            employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                                            ddl();
                                            admin();
                                        }
                                    }
                                    else
                                    {

                                        Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                                        Response.Redirect("Employee_Preview.aspx");

                                    }

                                    break;


                                default: Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                                    Response.Redirect("~/Company_Home.aspx");
                                    break;
                            }

                        }
                        else
                        {

                            Session["Profile_Error"] = "Complete Your Profile To proceed Forther";
                            Response.Redirect("Employee_Profile.aspx");
                        }

                    }

                }
                else
                {
                    Session["ErrorMsg"] = "Employee should be selected";
                    Response.Redirect("../Hrms_Company/Employee.aspx");

                }

            }
            else
            {
                Session["emp_menu"] = 4;
                Response.Redirect("Employee_Preview.aspx");

            }

        }
        catch (Exception ex)
        {
            Response.Cookies["Msg_Session"].Value=  "Error Occurred";

            Response.Redirect("~/Company_Home.aspx");
        }



    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                //employee.EmployeeId = 0;
                employee.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
            }

            if (s_login_role == "h")
            {
                //employee.EmployeeId = 0;
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            }

            emp_ID_List = employee.fn_get_EmployeeID((string)Session["emp_Code"]);

            if (emp_ID_List.Count > 0)
            {
                employee.EmployeeId = Convert.ToInt32(emp_ID_List[0].EmployeeId);
                employee.DivisionId = employee.ddl_check(ddl_Division);
                employee.DepartmentId = employee.ddl_check(ddl_Department);
                employee.DesignationId = employee.ddl_check(ddl_Designation);
                employee.GradeId = employee.ddl_check(ddl_Grade);
                employee.LevelId = employee.ddl_check(ddl_Level);
                employee.ShiftId = employee.ddl_check(ddl_Shift);
                employee.CategoryId = employee.ddl_check(ddl_Category);
                employee.JobStatusId = employee.ddl_check(ddl_JobStatus);
                employee.ProjectsiteId = employee.ddl_check(ddl_Project);
                employee.ReportID = employee.ddl_check(ddl_report);
                employee.temp_str = txt_reason.Text;
                employee.temp_str = "";
                employee.Date = employee.Convert_ToSqlDate(txt_date.Text);
                _Value = employee.Employee_profile1(employee);
            }
            else
            {
                Response.Redirect("Employee_Profile.aspx");
            }
           if (_Value != "1")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Data Added Successfully');", true);
                Response.Redirect("Employee_Date.aspx");
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

    protected void btn_Back_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Redirect("Employee_Preview.aspx");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }

    }

    protected void btn_update_Click(object sender, EventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                employee.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);              
             }
            if (s_login_role == "h")
            {
             employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
             employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
            }
            employee.DivisionId = employee.ddl_check(ddl_Division);           
            
            employee.DepartmentId = employee.ddl_check(ddl_Department);
            
            employee.DesignationId = employee.ddl_check(ddl_Designation);
            
            employee.GradeId = employee.ddl_check(ddl_Grade);
            
            employee.LevelId = employee.ddl_check(ddl_Level);
            
            employee.ShiftId = employee.ddl_check(ddl_Shift);
            
            employee.CategoryId = employee.ddl_check(ddl_Category);
            
            employee.JobStatusId = employee.ddl_check(ddl_JobStatus);            
            employee.ProjectsiteId = employee.ddl_check(ddl_Project);
            employee.ReportID = employee.ddl_check(ddl_report);
            employee.temp_str = txt_reason.Text;
            employee.Date = employee.Convert_ToSqlDate(txt_date.Text);      

            _Value = employee.Employee_profile1(employee);    
           
            if (_Value != "1")
            {              
                
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Updated Successfully');", true);
               Response.Redirect("Employee_Preview.aspx");
            }
            else
            {
                int chk = Convert.ToInt32(ddl_Department.SelectedItem.Value);
                //lbl_Error.Text = Convert.ToString(chk);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }

    }   

    public void admin()
    {
        try
        {
            EmpProfileList = employee.fn_get_Emp_Profile(employee);

           
            if (EmpProfileList.Count > 0)
            {
                date_retrive();
                ddl_retrive_Profile();
              
            }           
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

    public void date_retrive()
    {
        try
        {
            if (EmpProfileList[0].Date.ToShortDateString() != "01/01/1900")
            {
                txt_date.Text = EmpProfileList[0].Date.ToString("dd/MM/yyyy");
            }   
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }             
    }

    public void ddl_retrive_Profile()
    {

        try
        {

            ddl_Division.SelectedValue = EmpProfileList[0].DivisionId.ToString();

            ddl_Department.SelectedValue = EmpProfileList[0].DepartmentId.ToString();

            ddl_Designation.SelectedValue = EmpProfileList[0].DesignationId.ToString();

            ddl_Grade.SelectedValue = EmpProfileList[0].GradeId.ToString();

            ddl_Shift.SelectedValue = EmpProfileList[0].ShiftId.ToString();

            ddl_Category.SelectedValue = EmpProfileList[0].CategoryId.ToString();

            ddl_JobStatus.SelectedValue = EmpProfileList[0].JobStatusId.ToString();

            ddl_Project.SelectedValue = EmpProfileList[0].ProjectsiteId.ToString();

            ddl_Level.SelectedValue = EmpProfileList[0].LevelId.ToString();

            ddl_report.SelectedValue = EmpProfileList[0].ReportID.ToString();


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }


    }

    public void ddl()
    {


        try
        {
            Collection<Be_Recruitment> divisionList = r.fn_Division1(r);
            Collection<Be_Recruitment> departmentList = r.fn_Department1(r);
            Collection<Be_Recruitment> designationList = r.fn_Designation1(r);
            Collection<Be_Recruitment> gradeList = r.fn_Grade1(r);
            Collection<Be_Recruitment> jobStatusList = r.fn_JobStatus1(r);
            Collection<Be_Recruitment> shiftList = r.fn_Shift1(r);
            Collection<Be_Recruitment> employeeCategoryList = r.fn_EmployeeCategory1(r);
            Collection<Be_Recruitment> LevelList = r.fn_Level1(r);
            Collection<Be_Recruitment> ProjectList = r.fn_Project1(r);

    //ReportDepartment

            if (designationList.Count > 0)
            {
                for (int ddl_i = -1; ddl_i < designationList.Count; ddl_i++)
                {
                    if (ddl_i == -1)
                    {
                        ListItem es_list = new ListItem();

                        es_list.Text = "Select Reporting Designation";
                        es_list.Value = "0";
                        ddl_report.Items.Add(es_list);
                    }
                    else
                    {
                        ListItem es_list = new ListItem();

                        es_list.Value = designationList[ddl_i].DesignationID.ToString();
                        es_list.Text = designationList[ddl_i].DesignationName.ToString();
                        ddl_report.Items.Add(es_list);
                    }
                }
                //ddl_report.DataSource = departmentList;
                //ddl_report.DataValueField = "DepartmentId";
                //ddl_report.DataTextField = "DepartmentName";
                //ddl_report.DataBind();
            }
            else
            {
                ddl_report.Text = "No Record Found";
            }
   //Division

            if (divisionList.Count > 0)
            {
                for (int ddl_i = -1; ddl_i < divisionList.Count; ddl_i++)
                {
                    if (ddl_i == -1)
                    {
                        ListItem es_list = new ListItem();
                        es_list.Text = "Select Division";
                        es_list.Value = "0";
                        ddl_Division.Items.Add(es_list);
                    }
                    else
                    {
                        ListItem es_list = new ListItem();
                        es_list.Value = divisionList[ddl_i].DivisionID.ToString();
                        es_list.Text = divisionList[ddl_i].DivisionName.ToString();
                        ddl_Division.Items.Add(es_list);
                    }
                }
            }
            else
            {
                ddl_Division.Items.Add("No Record Found");
                ddl_Division.Enabled = false;
            }

    //Department

            if (departmentList.Count > 0)
            {
                for (int ddl_i = -1; ddl_i < departmentList.Count; ddl_i++)
                {

                    if (ddl_i == -1)
                    {
                        ListItem es_list = new ListItem();
                        es_list.Text = "Select Department";
                        es_list.Value = "0";
                        ddl_Department.Items.Add(es_list);
                    }
                    else
                    {
                        ListItem es_list = new ListItem();
                        es_list.Value = departmentList[ddl_i].DepartmentID.ToString();
                        es_list.Text = departmentList[ddl_i].DepartmentName.ToString();
                        ddl_Department.Items.Add(es_list);
                    }
                }

            }
            else
            {
                ddl_Department.Items.Add("No Record Found");
                ddl_Department.Enabled = false;
            }

 //Designation

            if (designationList.Count > 0)
            {
                for (int ddl_i = -1; ddl_i < designationList.Count; ddl_i++)
                {

                    if (ddl_i == -1)
                    {
                        ListItem es_list = new ListItem();

                        es_list.Text = "Select Designation";
                        es_list.Value = "0";
                        ddl_Designation.Items.Add(es_list);
                    }
                    else
                    {
                        ListItem es_list = new ListItem();

                        es_list.Value = designationList[ddl_i].DesignationID.ToString();
                        es_list.Text = designationList[ddl_i].DesignationName.ToString();
                        ddl_Designation.Items.Add(es_list);
                    }
                }
                //ddl_Designation.DataSource = designationList;
                //ddl_Designation.DataValueField = "DesignationId";
                //ddl_Designation.DataTextField = "DesignationName";
                //ddl_Designation.DataBind();

            }
            else
            {
                ddl_Designation.Items.Add("No Record Found");
                ddl_Designation.Enabled = false;
            }
//Grade

            if (gradeList.Count > 0)
            {
                for (int ddl_i = -1; ddl_i < gradeList.Count; ddl_i++)
                {

                    if (ddl_i == -1)
                    {
                        ListItem es_list = new ListItem();

                        es_list.Text = "Select Grade";
                        es_list.Value = "0";
                        ddl_Grade.Items.Add(es_list);
                    }
                    else
                    {
                        ListItem es_list = new ListItem();

                        es_list.Value = gradeList[ddl_i].GradeID.ToString();
                        es_list.Text = gradeList[ddl_i].GradeName.ToString();
                        ddl_Grade.Items.Add(es_list);
                    }
                }
                //ddl_Grade.DataSource = gradeList;
                //ddl_Grade.DataValueField = "GradeId";
                //ddl_Grade.DataTextField = "GradeName";
                //ddl_Grade.DataBind();

            }
            else
            {
                ddl_Grade.Items.Add("No Record Found");
                ddl_Grade.Enabled = false;
            }

//JobStatus


            if (jobStatusList.Count > 0)
            {
                for (int ddl_i = -1; ddl_i < jobStatusList.Count; ddl_i++)
                {

                    if (ddl_i == -1)
                    {
                        ListItem es_list = new ListItem();

                        es_list.Text = "Select JobStatus";
                        es_list.Value = "0";
                        ddl_JobStatus.Items.Add(es_list);
                    }
                    else
                    {
                        ListItem es_list = new ListItem();

                        es_list.Value = jobStatusList[ddl_i].JobStatusID.ToString();
                        es_list.Text = jobStatusList[ddl_i].JobStatusName.ToString();
                        ddl_JobStatus.Items.Add(es_list);
                    }
                }
                //ddl_JobStatus.DataSource = jobStatusList;
                //ddl_JobStatus.DataValueField = "JobStatusId";
                //ddl_JobStatus.DataTextField = "JobStatusName";
                //ddl_JobStatus.DataBind();

            }
            else
            {
                ddl_JobStatus.Items.Add("No Record Found");
                ddl_JobStatus.Enabled = false;
            }

 //Shift
            if (shiftList.Count > 0)
            {
                for (int ddl_i = -1; ddl_i < shiftList.Count; ddl_i++)
                {
                    if (ddl_i == -1)
                    {
                        ListItem es_list = new ListItem();
                        es_list.Text = "Select Shift";
                        es_list.Value = "0";
                        ddl_Shift.Items.Add(es_list);
                    }
                    else
                    {
                        ListItem es_list = new ListItem();
                        es_list.Value = shiftList[ddl_i].ShiftTypeID.ToString();
                        es_list.Text = shiftList[ddl_i].ShiftTypeName.ToString();
                        ddl_Shift.Items.Add(es_list);
                    }
                }
                //ddl_Shift.DataSource = shiftList;
                //ddl_Shift.DataValueField = "ShiftTypeID";
                //ddl_Shift.DataTextField = "ShiftTypeName";
                //ddl_Shift.DataBind();
            }
            else
            {
                ddl_Shift.Items.Add("No Record Found");
                ddl_Shift.Enabled = false;
            }

 //Category

            if (employeeCategoryList.Count > 0)
            {
                for (int ddl_i = -1; ddl_i < employeeCategoryList.Count; ddl_i++)
                {
                    if (ddl_i == -1)
                    {
                        ListItem es_list = new ListItem();
                        es_list.Text = "Select Category";
                        es_list.Value = "0";
                        ddl_Category.Items.Add(es_list);
                    }
                    else
                    {
                        ListItem es_list = new ListItem();
                        es_list.Value = employeeCategoryList[ddl_i].CategoryCode.ToString();
                        es_list.Text = employeeCategoryList[ddl_i].CategoryName.ToString();
                        ddl_Category.Items.Add(es_list);
                    }
                }
                //ddl_Category.DataSource = employeeCategoryList;
                //ddl_Category.DataValueField = "CategoryCode";
                //ddl_Category.DataTextField = "CategoryName";
                //ddl_Category.DataBind();
            }
            else
            {
                ddl_Category.Items.Add("No Record Found");
                ddl_Category.Enabled = false;
            }

 //Level
            if (LevelList.Count > 0)
            {
                for (int ddl_i = -1; ddl_i < LevelList.Count; ddl_i++)
                {
                    if (ddl_i == -1)
                    {
                        ListItem es_list = new ListItem();
                        es_list.Text = "Select Level";
                        es_list.Value = "0";
                        ddl_Level.Items.Add(es_list);
                    }
                    else
                    {
                        ListItem es_list = new ListItem();
                        es_list.Value = LevelList[ddl_i].temp_int.ToString();
                        es_list.Text = LevelList[ddl_i].temp_string.ToString();
                        ddl_Level.Items.Add(es_list);
                    }
                }
                //ddl_Level.DataSource = LevelList;
                //ddl_Level.DataValueField = "temp_int";
                //ddl_Level.DataTextField = "temp_string";
                //ddl_Level.DataBind();

            }
            else
            {
                ddl_Level.Items.Add("No Record Found");
                ddl_Level.Enabled = false;
            }

//Project Site

            if (ProjectList.Count > 0)
            {
                for (int ddl_i = -1; ddl_i < ProjectList.Count; ddl_i++)
                {
                    if (ddl_i == -1)
                    {
                        ListItem es_list = new ListItem();
                        es_list.Text = "Select ProjectSite";
                        es_list.Value = "se";
                        ddl_Project.Items.Add(es_list);
                    }
                    else
                    {
                        ListItem es_list = new ListItem();
                        es_list.Value = ProjectList[ddl_i].ProjectID.ToString();
                        es_list.Text = ProjectList[ddl_i].ProjectName.ToString();
                        ddl_Project.Items.Add(es_list);
                    }
                }
                //ddl_Project.DataSource = ProjectList;
                //ddl_Project.DataValueField = "ProjectID";
                //ddl_Project.DataTextField = "ProjectName";
                //ddl_Project.DataBind();
            }
            else
            {
                ddl_Project.Items.Add("No Record Found");
                ddl_Project.Enabled = false;
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

   protected void btn_skip_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Employee_Date.aspx");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }



}
