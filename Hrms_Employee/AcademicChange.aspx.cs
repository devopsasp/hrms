using System;
using System.Collections.Generic;
using System.Configuration;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ePayHrms.Company;
using ePayHrms.Employee;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using ePayHrms.Student;
using System.Text;
using System.Net;
using System.IO;

public partial class Hrms_Employee_AcademicChange : System.Web.UI.Page
{
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    Company company = new Company();
    Student student = new Student();
    Employee employee = new Employee();
    Collection<Student> StudentList;    
    string _Value;
    string s_login_role;
    string s_form = "";
    DataSet ds_userrights;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        student.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        student.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;

        if (!IsPostBack)
        {            
            switch (s_login_role)
            {
                case "a":
                   
                    break;

                case "h":
                    studentlist.Visible = false;
                  
                    break;

                case "e":
                   
                    break;

                case "u": s_form = "22";
                    ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                    if (ds_userrights.Tables[0].Rows.Count > 0)
                    {
                       // load1();
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
    protected void ddlCurrentyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        
       // chk_StudentCode.Items.Clear();
        StudentCheckList();
        ddlCourse.Enabled = false;
        ddlDepartment.Enabled = false;
        ddlSection.Enabled = false;

    }
    public void StudentCheckList()
    {
        student.ClassName = ddlCourse.SelectedItem.Text;
        student.DepartmentName = ddlDepartment.SelectedItem.Text;
        student.Section = ddlSection.SelectedItem.Text;
        student.Cyear = ddlCurrentyear.SelectedItem.Text;        
        if (student.Cyear == "Alumni")
        {
            student.Cyear = "5";
        }
        StudentList = student.fn_getStudentList_Currentyear(student);
        if (StudentList.Count > 0)
        {
            chk_StudentCode.DataSource = StudentList;
            chk_StudentCode.DataTextField = "FirstName";
            chk_StudentCode.DataValueField = "RegisterNo";
            chk_StudentCode.DataBind();
            chkall.Visible = true;
            btnUpdate.Visible = true;
            btnSelectChange.Visible = true;
            studentlist.Visible = true;
            btnUpdate.Visible = true;
        }
        else
        {
            chk_StudentCode.Items.Clear();            
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Students found');", true);
            btnSelectChange.Visible = false;
            chkall.Visible = false;
            btnUpdate.Visible = false;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (ddlChangeAcademic.SelectedItem.Text == "Select")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('First Choose the  Academic Change year');", true);
        }
        else if (chkall.Checked==false)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('First select the student');", true);
        }         
        else
        {
            myConnection.Open();
            string Course = ddlCourse.SelectedItem.Text;
            string Department = ddlDepartment.SelectedItem.Text;
            string Section = ddlSection.SelectedItem.Text;
            string CurrentYear = ddlCurrentyear.SelectedItem.Text;
            string ChangeAcademic = ddlChangeAcademic.SelectedItem.Text;
            if (CurrentYear == "Alumni")
            {
                CurrentYear = "5";
            }
            if (ChangeAcademic == "Alumni")
            {
                ChangeAcademic = "5";
            }

            if (Convert.ToInt32(ChangeAcademic) <= Convert.ToInt32(CurrentYear))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Changed year must be above the Current year');", true);
            }
            else
            {
                for (int ddl_i = 0; ddl_i < chk_StudentCode.Items.Count; ddl_i++)
                {
                    if (chk_StudentCode.Items[ddl_i].Selected == true)
                    {
                        student.RegisterNo = chk_StudentCode.Items[ddl_i].Value;
                        student.FirstName = chk_StudentCode.Items[ddl_i].Text;
                        SqlCommand cmd = new SqlCommand("update paym_Student set CurrentYear='" + ChangeAcademic + "' where pn_CompanyID='" + student.CompanyId + "' and pn_BranchID='" + student.BranchId + "' and RegisterNo='" + student.RegisterNo + "'and ClassName='" + Course + "' and Department='" + Department + "' and Section='" + Section + "' and CurrentYear='" + CurrentYear + "'", myConnection);
                        cmd.ExecuteNonQuery();
                    }
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Updated Successfully');", true);
                }
            }            
            myConnection.Close();
            chk_StudentCode.Items.Clear();
            chkall.Checked = false;
        }
    }
    protected void btnSelectChange_Click(object sender, EventArgs e)
    {
        ddlCourse.Enabled =true;
        ddlDepartment.Enabled =true;
        ddlSection.Enabled =true;
        chk_StudentCode.Items.Clear();
    }

    protected void chk_StudentCode_CheckedChanged(object sender, EventArgs e)
    {
        chkall.Checked = false;
    }
}