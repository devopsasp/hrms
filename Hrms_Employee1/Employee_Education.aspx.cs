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

public partial class Hrms_Employee_Default3 : System.Web.UI.Page
{

    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    Company company = new Company();

    Collection<Employee> EmployeesList;
    Collection<Employee> emp_edu_List;
    Collection<Employee> emp_ID_List;

    int pr_emp, grd, ddl_ex, _lbl, cur_yr, yr_it;
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
                    pr_emp = Convert.ToInt32(Request.Cookies["Profile_Check"].Value);

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
                                        ddl();
                                        btn_update.Visible = false;
                                    }
                                    else
                                    {
                                        ddl();
                                        btn_save.Visible = false;
                                        btn_skip.Visible = false;
                                        employee.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
                                        employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                                        admin();
                                        grd_ddl();
                                        Lbl();
                                    }
                                    break;

                                case "h": if (pr_emp == 1)
                                    {
                                        ddl();
                                        btn_update.Visible = false;                              
                                       
                                    }
                                    else
                                    {
                                        ddl();                                     
                                        btn_save.Visible = false;
                                        btn_skip.Visible = false;
                                        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                        employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                                        admin();
                                        grd_ddl();
                                        Lbl();
                                    }
                                    break;

                                case "u": s_form = "33";

                                    ds_userrights = company.check_Userrights(Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value), s_form);

                                    if (ds_userrights.Tables[0].Rows.Count > 0)
                                    {
                                        if (pr_emp == 1)
                                        {
                                            ddl();
                                            btn_update.Visible = false;
                                        }
                                        else
                                        {
                                            ddl();
                                            btn_save.Visible = false;
                                            btn_skip.Visible = false;
                                            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                            employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                                            admin();
                                            grd_ddl();
                                            Lbl();
                                        }
                                    }
                                    else
                                    {
                                        Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                                        Response.Redirect("Employee_Preview.aspx", false);
                                    }
                                    break;

                                default: Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                                    Response.Redirect("Company_Home.aspx");
                                    break;
                            }
                        }
                        else
                        {
                            Session["Profile_Error"] = "Complete Your Profile To proceed Further";
                            Response.Redirect("Employee_Profile.aspx", false);
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
                Session["emp_menu"] = 1;
                Response.Redirect("Employee_Preview.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Response.Cookies["Msg_Session"].Value=  "Error Occurred";
            Response.Redirect("Company_Home.aspx");
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

            emp_ID_List = employee.fn_get_EmployeeID(Request.Cookies["emp_Code"].Value);
            if (emp_ID_List.Count > 0)
            {
                employee.EmployeeId = Convert.ToInt32(emp_ID_List[0].EmployeeId);

                for (int emp_edu = 0; emp_edu < grid_emp_education.Rows.Count; emp_edu++)
                {
                    employee.PGCourseID = Convert.ToInt32(grid_emp_education.DataKeys[emp_edu].Value);
                    employee.PGInstutionName = ((HtmlInputText)grid_emp_education.Rows[emp_edu].FindControl("txtInstitution")).Value;
                    employee.PGCompletedYear = ((DropDownList)grid_emp_education.Rows[emp_edu].FindControl("ddl_ComYear")).SelectedItem.Text;
                    employee.PGPercentage = ((TextBox)grid_emp_education.Rows[emp_edu].FindControl("txtPercentage")).Text;
                    employee.specializationID = Convert.ToInt32(((DropDownList)grid_emp_education.Rows[emp_edu].FindControl("ddl_Specialization")).SelectedItem.Value);
                    employee.mode = ((DropDownList)grid_emp_education.Rows[emp_edu].FindControl("ddl_Mode")).SelectedItem.Text;
                    employee.PGCompletedinf = ((DropDownList)grid_emp_education.Rows[emp_edu].FindControl("ddl_inf")).SelectedItem.Text;
                    
                    _Value = employee.Employee_PGEducation(employee);
                }
            }
            else
            {
                Response.Redirect("Employee_Profile.aspx");
            }

            if (_Value != "1")
            {
                Response.Redirect("Employee_Skills.aspx");
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
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
                employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            }
            //emp_ID_List = employee.fn_get_EmployeeID(Request.Cookies["emp_Code"].Value);
            //if (emp_ID_List.Count > 0)
            //{
            //    employee.EmployeeId = Convert.ToInt32(emp_ID_List[0].EmployeeId);

            for (int emp_edu = 0; emp_edu < grid_emp_education.Rows.Count; emp_edu++)
            {
                employee.PGCourseID = Convert.ToInt32(grid_emp_education.DataKeys[emp_edu].Value);
                employee.PGInstutionName = ((HtmlInputText)grid_emp_education.Rows[emp_edu].FindControl("txtInstitution")).Value;
                employee.specializationID = Convert.ToInt32(((DropDownList)grid_emp_education.Rows[emp_edu].FindControl("ddl_Specialization")).SelectedItem.Value);
                employee.PGCompletedYear = ((DropDownList)grid_emp_education.Rows[emp_edu].FindControl("ddl_ComYear")).SelectedItem.Text;
                employee.PGPercentage = ((TextBox)grid_emp_education.Rows[emp_edu].FindControl("txtPercentage")).Text;               
                employee.mode = ((DropDownList)grid_emp_education.Rows[emp_edu].FindControl("ddl_Mode")).SelectedItem.Text;
                employee.PGCompletedinf = ((DropDownList)grid_emp_education.Rows[emp_edu].FindControl("ddl_inf")).SelectedItem.Text;
                _Value = employee.Employee_PGEducation(employee);
            }            
            if (_Value != "1")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Updated Successfully');", true);
                Response.Redirect("Employee_Skills.aspx");
            }
            else
            {
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
            //EmployeesList = employee.fn_getEmployees(employee);
            //if (EmployeesList.Count > 0)
            //{          
            emp_edu_List = employee.fn_getEmployee_EducationList(employee);
            if (emp_edu_List.Count > 0)
            {
                grid_emp_education.DataSource = emp_edu_List;
                grid_emp_education.DataBind();
            }
            //grid_specialaization();
            //}
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
            Collection<Employee> CourseList = employee.fn_getCourseList1(employee.BranchId);
            //Collection<Employee> SpecializationList = employee.fn_getSpecializationList();
            //Education 
            //Course
            //ddl_PGCourse.DataSource = CourseList;
            //ddl_PGCourse.DataValueField = "PGCourseID";
            //ddl_PGCourse.DataTextField = "PGCourseName";
            //ddl_PGCourse.DataBind();

            lblEducation.DataSource = CourseList;
            lblEducation.DataValueField = "PGCourseID";
            lblEducation.DataTextField = "PGCourseName";
            lblEducation.DataBind();
           
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
            Response.Redirect("Employee_Skills.aspx");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

    public void grd_ddl()
    {
        grid_specialaization();
       // year();
        //Grid Dropdown Selected Item  
        for (grd = 0; grd < grid_emp_education.Rows.Count; grd++)
        {
           // Grid Specialization Dropdown
            for (ddl_ex = 0; ddl_ex < ((DropDownList)grid_emp_education.Rows[grd].FindControl("ddl_Specialization")).Items.Count; ddl_ex++)
            {
                if (((DropDownList)grid_emp_education.Rows[grd].FindControl("ddl_Specialization")).Items[ddl_ex].Value == emp_edu_List[grd].specializationID.ToString())
                {
                    ((DropDownList)grid_emp_education.Rows[grd].FindControl("ddl_Specialization")).SelectedIndex = ddl_ex;
                }
            }
            //Grid Year Dropdown
            for (ddl_ex = 0; ddl_ex < ((DropDownList)grid_emp_education.Rows[grd].FindControl("ddl_ComYear")).Items.Count; ddl_ex++)
            {
                if (((DropDownList)grid_emp_education.Rows[grd].FindControl("ddl_ComYear")).Items[ddl_ex].Text == emp_edu_List[grd].PGCompletedYear)
                {
                    ((DropDownList)grid_emp_education.Rows[grd].FindControl("ddl_ComYear")).SelectedIndex = ddl_ex;
                }
            }
            //Grid Proficiency Mode
            for (ddl_ex = 0; ddl_ex < ((DropDownList)grid_emp_education.Rows[grd].FindControl("ddl_Mode")).Items.Count; ddl_ex++)
            {
                if (((DropDownList)grid_emp_education.Rows[grd].FindControl("ddl_Mode")).Items[ddl_ex].Text == emp_edu_List[grd].mode)
                {
                    ((DropDownList)grid_emp_education.Rows[grd].FindControl("ddl_Mode")).SelectedIndex = ddl_ex;
                }
            }
            //Grid completed information
            for (ddl_ex = 0; ddl_ex < ((DropDownList)grid_emp_education.Rows[grd].FindControl("ddl_inf")).Items.Count; ddl_ex++)
            {
                if (((DropDownList)grid_emp_education.Rows[grd].FindControl("ddl_inf")).Items[ddl_ex].Text == emp_edu_List[grd].PGCompletedinf)
                {
                    ((DropDownList)grid_emp_education.Rows[grd].FindControl("ddl_inf")).SelectedIndex = ddl_ex;
                }
            }
        }
    }

    public void Lbl()
    {
        for (grd = 0; grd < emp_edu_List.Count; grd++)
        {
            for (_lbl = 0; _lbl < lblEducation.Items.Count; _lbl++)
            {
                if (lblEducation.Items[_lbl].Value == emp_edu_List[grd].PGCourseID.ToString())
                {
                    lblEducation.Items[_lbl].Selected = true;
                }
            }
        }
    }

    public void grid_specialaization()
    {
        cur_yr = DateTime.Now.Year;
        cur_yr = cur_yr + 5;

        Collection<Employee> SpecializationList = employee.fn_getSpecializationList();
        for (grd = 0; grd < grid_emp_education.Rows.Count; grd++)
        {
            //Grid Specialization Dropdown
            ((DropDownList)grid_emp_education.Rows[grd].FindControl("ddl_Specialization")).DataSource = SpecializationList;
            ((DropDownList)grid_emp_education.Rows[grd].FindControl("ddl_Specialization")).DataValueField = "PGSpecializationId";
            ((DropDownList)grid_emp_education.Rows[grd].FindControl("ddl_Specialization")).DataTextField = "PGSpecialaization";
            ((DropDownList)grid_emp_education.Rows[grd].FindControl("ddl_Specialization")).DataBind();
            //Grid Year dropdown
            year();
        }
    }

    public void year()
    {
        cur_yr = DateTime.Now.Year;
        cur_yr = cur_yr + 5;
        for (yr_it = 1950; yr_it <= cur_yr; yr_it++)
        {
            ((DropDownList)grid_emp_education.Rows[grd].FindControl("ddl_ComYear")).Items.Add(Convert.ToString(yr_it));
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            string list_s = "(";
            int list_i, list_temp = 0;
            for (list_i = 0; list_i < lblEducation.Items.Count; list_i++)
            {
                if (lblEducation.Items[list_i].Selected)
                {
                    if (list_temp == 0)
                    {
                        list_s = list_s + lblEducation.Items[list_i].Value;
                        list_temp++;
                    }
                    else
                    {
                        list_s = list_s + "," + lblEducation.Items[list_i].Value;
                    }
                }
            }
            list_s = list_s + ")";
            r.temp_string = list_s;            
            Collection<Be_Recruitment> educationList = r.fn_Course(r);
            grid_emp_education.DataSource = educationList;
            grid_emp_education.DataBind();
            year();
            //Grid Specialaization
            grid_specialaization();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }
}

