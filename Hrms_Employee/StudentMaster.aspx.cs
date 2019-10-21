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
using ePayHrms.Connection;
using System.IO;
using ePayHrms.Company;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ePayHrms.Student;
using ePayHrms.Employee;
using System.Data.SqlClient;
using System.Data.OleDb;

public partial class Hrms_Employee_Student_Profile : System.Web.UI.Page
{
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand cmd = new SqlCommand();
    Employee employee = new Employee();
    Company company = new Company();
    Student student = new Student();
    Collection<Student> CourseList;
    Collection<Student> DepartmentList;
    Collection<Student> ClassList;

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

                

                student.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
                student.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

                if (!IsPostBack)
                {
                    switch (s_login_role)
                    {
                        case "a":
                            student.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
                            admin();
                            break;

                        case "h":
                            student.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                            hr();
                            break;

                        case "e":
                            student.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                            hr();
                            break;

                        case "u": s_form = "20";
                            ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                            if (ds_userrights.Tables[0].Rows.Count > 0)
                            {
                                hr();
                            }
                            else
                            {
                                Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator.";
                                Response.Redirect("~/Company_Home.aspx");
                            }
                            break;

                        default: Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                            Response.Redirect("~/Company_Home.aspx");
                            break;
                    }
                }
            }
            else
            {
                Session["emp_menu"] = 0;
                Response.Redirect("~/Company_Home.aspx");
            }

        }
        catch (Exception ex)
        {
            Response.Cookies["Msg_Session"].Value=  "Error Occurred";
        }
    }

    public void ddl_year_load(DropDownList ddl)
    {
        try
        {
            int cur_yr = DateTime.Now.Year;
            ddl.Items.Clear();
            for (int yr_it = cur_yr - 5; yr_it <= cur_yr + 1; yr_it++)
            {
                ddl.Items.Add(Convert.ToString(yr_it));
            }
            ddl.SelectedItem.Text = cur_yr.ToString();
        }

        catch (Exception ex)
        {
            //lbl_error.Text = "Error";
        }
    }

    public void hr()
    {
        CourseList = student.fn_course(student.BranchId);

        if (CourseList.Count > 0)
        {
            gridCourse.DataSource = CourseList;
            gridCourse.DataBind();
        }

        DepartmentList = student.fn_department(student.BranchId);

        if (DepartmentList.Count > 0)
        {
            GridDepartment.DataSource = DepartmentList;
            GridDepartment.DataBind();
        }

        ClassList = student.fn_Class(student.BranchId);

        if (ClassList.Count > 0)
        {
            GridClass.DataSource = ClassList;
            GridClass.DataBind();
        }

    }

    public void admin()
    {

    }

    public void load()
    {

    }

    protected void gridCourse_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "addCourse")
            {
                string Cname = ((HtmlInputText)gridCourse.FooterRow.FindControl("txtCourseAdd")).Value;
                if (Cname == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Course Name cannot be empty!');", true);
                    return;
                }
                AddNewCourse(Cname);
            }
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Course Name already exists.');", true);
        }
    }

    private void AddNewCourse(string Cname)
    {
        string query = @"INSERT INTO Student_Course (pn_CompanyID, pn_BranchID, CourseName) VALUES('" + student.CompanyId + "','" + student.BranchId + "','" + Cname + "')";
        SqlCommand myCommand = new SqlCommand(query, myConnection);
        myConnection.Open();
        myCommand.ExecuteNonQuery();
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Course Saved Successfully!');", true);
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Course Saved Successfully');</script>");
        hr();
    }

    protected void EditCourse(object sender, GridViewEditEventArgs e)
    {
        try
        {
            student.CourseId = Convert.ToInt32(gridCourse.DataKeys[e.NewEditIndex].Value);
            student.CourseName = ((HtmlInputText)gridCourse.Rows[e.NewEditIndex].FindControl("txtCourseName")).Value;
            string query = @"Update Student_Course set CourseName = '" + student.CourseName + "' where pn_CourseID = '" + student.CourseId + "' and pn_CompanyID = '" + student.CompanyId + "' and pn_BranchID = '" + student.BranchId + "'";
            SqlCommand myCommand = new SqlCommand(query, myConnection);
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Course Modified Successfully!');", true);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Course Saved Successfully');</script>");
            hr();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

    protected void DeleteCourse(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            student.CourseId = Convert.ToInt32(gridCourse.DataKeys[e.RowIndex].Value);
            student.CourseName = ((HtmlInputText)gridCourse.Rows[e.RowIndex].FindControl("txtCourseName")).Value;
            string i = Check_Course(student.CourseName);
            if (i == "1")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Cannot Delete. This course has already been assigned');", true);
                return;
            }
            string query = @"Delete from Student_Course where pn_CourseID = '" + student.CourseId + "' and pn_CompanyID = '" + student.CompanyId + "' and pn_BranchID = '" + student.BranchId + "'";
            SqlCommand myCommand = new SqlCommand(query, myConnection);
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Course Deleted Successfully!');", true);
            hr();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }
    protected void UpdateCourse(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            student.CourseName = ((HtmlInputText)gridCourse.Rows[e.RowIndex].FindControl("txtCourseName")).Value;
            string i = Check_Course(student.CourseName);
            if (i == "1")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Cannot Modify. This course has already been assigned');", true);
                return;
            }
            ((HtmlInputText)gridCourse.Rows[e.RowIndex].FindControl("txtCourseName")).Disabled = false;
            ((LinkButton)gridCourse.Rows[e.RowIndex].FindControl("img_save")).Visible = true;
            ((LinkButton)gridCourse.Rows[e.RowIndex].FindControl("img_update")).Visible = false;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

    protected void GridDepartment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "addDepartment")
            {
                string Cname = ((HtmlInputText)GridDepartment.FooterRow.FindControl("txtDepartmentAdd")).Value;
                if (Cname == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Department Name cannot be empty!');", true);
                    return;
                }
                AddNewDepartment(Cname);
            }
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Department Name already exists.');", true);
        }
    }

    private void AddNewDepartment(string Cname)
    {
        string query = @"INSERT INTO Student_Department (pn_CompanyID, pn_BranchID, DepartmentName) VALUES('" + student.CompanyId + "','" + student.BranchId + "','" + Cname + "')";
        SqlCommand myCommand = new SqlCommand(query, myConnection);
        myConnection.Open();
        myCommand.ExecuteNonQuery();
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Department Saved Successfully!');", true);
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Course Saved Successfully');</script>");
        hr();
    }

    protected void EditDepartment(object sender, GridViewEditEventArgs e)
    {
        try
        {

            student.DepartmentId = Convert.ToInt32(GridDepartment.DataKeys[e.NewEditIndex].Value);
            student.DepartmentName = ((HtmlInputText)GridDepartment.Rows[e.NewEditIndex].FindControl("txtDepartmentName")).Value;
            string query = @"Update Student_Department set DepartmentName = '" + student.DepartmentName + "' where pn_DepartmentID = '" + student.DepartmentId + "' and pn_CompanyID = '" + student.CompanyId + "' and pn_BranchID = '" + student.BranchId + "'";
            SqlCommand myCommand = new SqlCommand(query, myConnection);
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Department Modified Successfully!');", true);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Course Saved Successfully');</script>");
            hr();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

    protected void DeleteDepartment(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            student.DepartmentId = Convert.ToInt32(GridDepartment.DataKeys[e.RowIndex].Value);
            student.DepartmentName = ((HtmlInputText)GridDepartment.Rows[e.RowIndex].FindControl("txtDepartmentName")).Value;
            string i = Check_Department(student.DepartmentName);
            if (i == "1")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Cannot Modify. This Department has already been assigned');", true);
                return;
            }
            string query = @"Delete from Student_Department where pn_DepartmentID = '" + student.DepartmentId + "' and pn_CompanyID = '" + student.CompanyId + "' and pn_BranchID = '" + student.BranchId + "'";
            SqlCommand myCommand = new SqlCommand(query, myConnection);
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Department Deleted Successfully!');", true);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Course Saved Successfully');</script>");
            hr();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }
    protected void UpdateDepartment(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            student.DepartmentName = ((HtmlInputText)GridDepartment.Rows[e.RowIndex].FindControl("txtDepartmentName")).Value;
            string i = Check_Department(student.DepartmentName);
            if (i == "1")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Cannot Modify. This Department has already been assigned');", true);
                return;
            }
            ((HtmlInputText)GridDepartment.Rows[e.RowIndex].FindControl("txtDepartmentName")).Disabled = false;
            ((LinkButton)GridDepartment.Rows[e.RowIndex].FindControl("img_save")).Visible = true;
            ((LinkButton)GridDepartment.Rows[e.RowIndex].FindControl("img_update")).Visible = false;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

    protected void gridClass_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "addClass")
            {
                string CourseName = ((DropDownList)GridClass.FooterRow.FindControl("ddlCourseAdd")).SelectedItem.Text;
                string DepartmentName = ((DropDownList)GridClass.FooterRow.FindControl("ddlDepartmentAdd")).SelectedItem.Text;
                string SectionName = ((DropDownList)GridClass.FooterRow.FindControl("ddlSectionAdd")).SelectedItem.Text;
                string Year = ((DropDownList)GridClass.FooterRow.FindControl("ddlYearAdd")).SelectedItem.Text;
                string Cname = ((HtmlInputText)GridClass.FooterRow.FindControl("txtClassAdd")).Value;

                if (CourseName == "Select" || DepartmentName == "Select" || SectionName == "Select" || Year == "Select" || Cname == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error! All Feilds are Mandatory');", true);
                    return;
                }
                AddNewClass(CourseName, DepartmentName, SectionName, Year, Cname);
            }
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Course Name already exists.');", true);
        }
    }

    private void AddNewClass(string Coursename,string DepartmentName, string SectionName, string Year, string Cname)
    {
        string query = @"INSERT INTO Student_Class (pn_CompanyID, pn_BranchID, CourseName, DepartmentName, Section, Year, ClassName) VALUES('" + student.CompanyId + "','" + student.BranchId + "','" + Coursename + "','" + DepartmentName + "','" + SectionName + "','" + Year + "','" + Cname + "')";
        SqlCommand myCommand = new SqlCommand(query, myConnection);
        myConnection.Open();
        myCommand.ExecuteNonQuery();
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Class Name Saved Successfully!');", true);
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Course Saved Successfully');</script>");
        hr();
    }

    protected void EditClass(object sender, GridViewEditEventArgs e)
    {
        try
        {

            student.ClassId = Convert.ToInt32(GridClass.DataKeys[e.NewEditIndex].Value);
            student.CourseName = ((DropDownList)GridClass.Rows[e.NewEditIndex].FindControl("ddlCourseName")).SelectedItem.Text;
            student.DepartmentName = ((DropDownList)GridClass.Rows[e.NewEditIndex].FindControl("ddlDepartmentName")).SelectedItem.Text;
            student.DivisionName = ((DropDownList)GridClass.Rows[e.NewEditIndex].FindControl("ddlSection")).SelectedItem.Text;
            student.PGCompletedYear = ((DropDownList)GridClass.Rows[e.NewEditIndex].FindControl("ddlYear")).SelectedItem.Text;
            student.ClassName = ((HtmlInputText)GridClass.Rows[e.NewEditIndex].FindControl("txtClassName")).Value;

            string query = @"Update Student_Class set CourseName='" + student.CourseName + "', DepartmentName = '" + student.DepartmentName + "',Section = '" + student.DivisionName + "', Year='" + student.PGCompletedYear + "', ClassName = '" + student.ClassName + "' where pn_ClassID = '" + student.ClassId + "' and pn_CompanyID = '" + student.CompanyId + "' and pn_BranchID = '" + student.BranchId + "'";
            SqlCommand myCommand = new SqlCommand(query, myConnection);
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Class Name Modified Successfully!');", true);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Course Saved Successfully');</script>");
            hr();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

    protected void DeleteClass(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            student.ClassId = Convert.ToInt32(GridClass.DataKeys[e.RowIndex].Value);
            student.ClassName = ((HtmlInputText)GridClass.Rows[e.RowIndex].FindControl("txtClassName")).Value;
            string query = @"Delete from Student_Class where pn_ClassID = '" + student.ClassId + "' and pn_CompanyID = '" + student.CompanyId + "' and pn_BranchID = '" + student.BranchId + "'";
            SqlCommand myCommand = new SqlCommand(query, myConnection);
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Class Name Deleted Successfully!');", true);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Course Saved Successfully');</script>");
            hr();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }
    protected void UpdateClass(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            ((DropDownList)GridClass.Rows[e.RowIndex].FindControl("ddlCourseName")).Enabled = true;
            ((DropDownList)GridClass.Rows[e.RowIndex].FindControl("ddlDepartmentName")).Enabled = true;
            ((DropDownList)GridClass.Rows[e.RowIndex].FindControl("ddlSection")).Enabled = true;
            ((DropDownList)GridClass.Rows[e.RowIndex].FindControl("ddlYear")).Enabled = true;
            ((HtmlInputText)GridClass.Rows[e.RowIndex].FindControl("txtClassName")).Disabled = false;
            ((LinkButton)GridClass.Rows[e.RowIndex].FindControl("img_save")).Visible = true;
            ((LinkButton)GridClass.Rows[e.RowIndex].FindControl("img_update")).Visible = false;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

    public string Check_Course(string CN)
    {
        myConnection.Open();
        SqlCommand com = new SqlCommand("select * from student_class where CourseName = '" + CN + "' and pn_BranchID = '" + student.BranchId + "'", myConnection);
        SqlDataReader rea = com.ExecuteReader();
        if (rea.Read())
        {
            if (rea.HasRows)
            {
                myConnection.Close();
                return "1";
            }
            else
            {
                myConnection.Close();
                return "0";
            }
        }
        else
        {
            myConnection.Close();
            return "0";
        }
        
    }

    public string Check_Department(string DN)
    {
        myConnection.Open();
        SqlCommand com = new SqlCommand("select * from student_class where DepartmentName = '" + DN + "' and pn_BranchID = '" + student.BranchId + "'", myConnection);
        SqlDataReader rea = com.ExecuteReader();
        if (rea.Read())
        {
            if (rea.HasRows)
            {
                myConnection.Close();
                return "1";
            }
            else
            {
                myConnection.Close();
                return "0";
            }
        }
        else
        {
            myConnection.Close();
            return "0";
        }

    }
}