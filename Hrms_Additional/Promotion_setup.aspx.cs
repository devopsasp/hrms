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
using ePayHrms.BE.Recruitment;
using ePayHrms.Connection;
using ePayHrms.Candidate;
using System.IO;
using ePayHrms.Company;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ePayHrms.Leave;
using ePayHrms.Employee;

public partial class Hrms_Additional_Default : System.Web.UI.Page
{
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader rea;
    SqlDataAdapter ada = new SqlDataAdapter();
    Company company = new Company();
    Employee employee = new Employee();
    Leave l = new Leave();
    string gname = "";
    Collection<Leave> LeaveList, AppraisalList;
    Collection<Company> CompanyList;
    Collection<Employee> DepartmentList;
    Collection<Employee> DesignationList;
    Collection<Employee> GradeList;
    Collection<Employee> LevelList;
    Collection<Company> ddlBranchsList;
    Collection<Employee> EmployeeList;
    Collection<Employee> EmpProfileList;
    Collection<Employee> EmpWorkDetails;

    string s_login_role;
    string s_form = "", subquery, qry, _Value;
    DataSet ds_userrights;
    int ddl_i, i, index, total = 0;
    double avg, tot_pts;
    int eid = 0;

    DataSet ds = new DataSet();
    //DropDownList ddlemp_virtual = new DropDownList();
    

    protected void Page_Load(object sender, EventArgs e)
    {
        lb_Check.Visible = false;
        Session["formulaName"] = "";
        
       
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        l.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
       

        if (!IsPostBack)
        {

            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                ddl_branch.Visible = false;
                switch (s_login_role)
                {
                    case "a":
                        ddl_branch.Visible = true;
                        ddl_branch_load();
                        
                        tbl_PF.Visible = false;
                        break;

                    case "h":
                        
                        ddl_deptload();
                        hr();
                        break;

                    case "e": 
                        l.EmployeeID = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
                        break;

                    case "u": //s_form = "5";
                        s_form = "41";
                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
     
                        }
                        else
                        {    
                            l.EmployeeID = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
                        }

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

    public void hr()
    {
        
    }

    public void ddl_branch_load()
    {
        myConnection.Open();
        SqlDataAdapter ad = new SqlDataAdapter("select branchname,pn_branchid from paym_branch", myConnection);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        ddl_branch.DataTextField = "branchname";
        ddl_branch.DataValueField = "pn_branchid";
        ddl_branch.DataSource = ds;
        ddl_branch.DataBind();
        ddl_branch.Items.Insert(0, "Select branch");
        
        myConnection.Close();
    }

    public void ddl_deptload()
    {
        emp_details.Visible = false;
        promo_details.Visible = false;
        DepartmentList = employee.fn_Department(employee.BranchId);
        if (DepartmentList.Count > 0)
        {
            for (ddl_i = -1; ddl_i < DepartmentList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();
                    e_list.Text = "Select";
                    e_list.Value = "sd";
                    ddl_dept.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();
                    e_list.Value = DepartmentList[ddl_i].DepartmentId.ToString();
                    e_list.Text = DepartmentList[ddl_i].DepartmentName.ToString();
                    ddl_dept.Items.Add(e_list);
                }
            }
        }
        else
        {
            lbl_error.Text = "No Department Available";
        }
    }

    protected void ddl_dept_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_dept.SelectedValue != "sd")
        {
            ddl_employee_load();
        }
        else
        {
           
        }
    }

    public void ddl_employee_load()
    {
        ddl_employee.Items.Clear();
        if (s_login_role == "a")
        {
            employee.BranchId =Convert.ToInt32(ddl_branch.SelectedItem.Value);
        }
        if (s_login_role == "h")
        {
            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        }

        qry = "Select distinct a.pn_EmployeeID, a.EmployeeCode, a.Employee_First_Name from paym_employee a, paym_employee_profile1 b where a.pn_CompanyID=b.pn_CompanyID and a.pn_BranchID=b.pn_BranchID and a.pn_EmployeeID=b.pn_EmployeeID and b.pn_DepartmentID=" + ddl_dept.SelectedValue + " and b.pn_CompanyID=" + employee.CompanyId + " and a.status='y' and b.pn_BranchID=" + employee.BranchId;

        EmployeeList = employee.fn_getEmplist(qry);

        if (EmployeeList.Count > 0)
        {
            for (ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();
                    e_list.Text = "Select Employee";
                    e_list.Value = "0";
                    ddl_employee.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();
                    e_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
                    e_list.Text = EmployeeList[ddl_i].LastName.ToString();
                    ddl_employee.Items.Add(e_list);
                }
            }
        }

        else
        {
            lbl_error.Text = "No Employee";
        }
    }
    protected void ddl_employee_SelectedIndexChanged(object sender, EventArgs e)
    {
        emp_details.Visible = true;
        promo_details.Visible = true;
        employee.EmployeeId = Convert.ToInt32(ddl_employee.SelectedItem.Value);
        EmpProfileList = employee.fn_get_Emp_Profile1(employee);
        EmployeeList = employee.fn_getEmployeeList1(employee);
        EmpWorkDetails = employee.fn_get_Emp_WorkDetails(employee);

        if (EmpProfileList.Count > 0)
        {
            if (EmpProfileList[0].LevelId != 1)
            {
                lbl_Level2.Text = employee.fn_GetLevelName(EmpProfileList[0].LevelId);
            }

            if (EmpProfileList[0].DesignationId != 1)
            {
                lbl_designation2.Text = employee.fn_GetDesignationName(EmpProfileList[0].DesignationId);
            }

            if (EmpProfileList[0].GradeId != 1)
            {
                lbl_grade2.Text = employee.fn_GetGradeName(EmpProfileList[0].GradeId);
            }

            if (EmployeeList[0].basic != "0")
            {
                lbl_basic2.Text = EmployeeList[0].basic_salary.ToString();
            }

            if (EmpWorkDetails[0].d_join != Convert.ToDateTime("1900/01/01"))
            {
                lbl_join2.Text = EmpWorkDetails[0].d_join.ToString("dd/MM/yyyy");
            }            
        }
        Designation_load();
        Grade_load();
        Level_load();        
    }

    public void Designation_load()
    {
        DesignationList = employee.fn_Designation1();
        if (DesignationList.Count > 0)
        {
            for (ddl_i = -1; ddl_i < DesignationList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();
                    e_list.Text = "Select";
                    e_list.Value = "sd";
                    ddl_desig.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();
                    e_list.Value = DesignationList[ddl_i].DesignationId.ToString();
                    e_list.Text = DesignationList[ddl_i].DesignationName.ToString();
                    ddl_desig.Items.Add(e_list);
                }
            }
        }
        else
        {
            lbl_error.Text = "No Designations Available";
        }
    }

    public void Grade_load()
    {
        GradeList = employee.fn_Grade1();
        if (GradeList.Count > 0)
        {
            for (ddl_i = -1; ddl_i < GradeList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();
                    e_list.Text = "Select";
                    e_list.Value = "sd";
                    ddl_grade.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();
                    e_list.Value = GradeList[ddl_i].GradeId.ToString();
                    e_list.Text = GradeList[ddl_i].GradeName.ToString();
                    ddl_grade.Items.Add(e_list);
                }
            }
        }
        else
        {
            lbl_error.Text = "No Grades Available";
        }
    }

    public void Level_load()
    {
        LevelList = employee.fn_Level1();
        if (LevelList.Count > 0)
        {
            for (ddl_i = -1; ddl_i < LevelList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();
                    e_list.Text = "Select";
                    e_list.Value = "sd";
                    ddl_level.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();
                    e_list.Value = LevelList[ddl_i].LevelId.ToString();
                    e_list.Text = LevelList[ddl_i].LevelName.ToString();
                    ddl_level.Items.Add(e_list);
                }
            }
        }
        else
        {
            lbl_error.Text = "No Levels Available";
        }
    }
    
    protected void ddl_branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbl_PF.Visible = true;
        employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
        ddl_deptload();
    }

    protected void lb_Check_Click(object sender, EventArgs e)
    {
        EmpProfileList = employee.Promotion_check(employee);
        if (EmpProfileList.Count > 0)
        {
            //grid_Course.DataSource = CourseList;
            //grid_Course.DataBind();
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        employee.EmployeeId = Convert.ToInt32(ddl_employee.SelectedItem.Value);
        EmpProfileList = employee.fn_get_Emp_Profile1(employee);
        if (EmpProfileList.Count > 0)
        {
            employee.DivisionId = EmpProfileList[0].DivisionId;
            employee.DepartmentId = EmpProfileList[0].DepartmentId;
            employee.ShiftId = EmpProfileList[0].ShiftId;
            employee.CategoryId = EmpProfileList[0].CategoryId;
            employee.ProjectsiteId = EmpProfileList[0].ProjectsiteId;
            employee.JobStatusId = EmpProfileList[0].JobStatusId;
            employee.DesignationId = employee.ddl_check(ddl_desig);
            employee.GradeId = employee.ddl_check(ddl_grade);
            employee.LevelId = employee.ddl_check(ddl_level);
            employee.Date = employee.Convert_ToSqlDate(txt_edate.Text);
            employee.Reporting = EmpProfileList[0].Reporting;
            employee.temp_str = EmpProfileList[0].temp_str;
            _Value = employee.Employee_profile1(employee);

            if (_Value != "1")
            {
                lbl_error.Text = "<font color=Blue>Added Successfully</font>";
                double sal = Convert.ToDouble(lbl_basic2.Text) * (Convert.ToDouble(txt_basic.Text) / 100);
                sal = sal + Convert.ToDouble(lbl_basic2.Text);
                employee.salary = sal.ToString();
                employee.d_Date = txt_edate.Text;
                _Value = employee.salary_Appraisal(employee);
                if (_Value != "1")
                {
                    lbl_error.Text = "<font color=Blue>Added Successfully</font>";
                    employee.inc_value = Convert.ToDouble(txt_basic.Text);
                    _Value = employee.Promotion(employee);
                    if (_Value != "1")
                    {
                        lbl_error.Text = "<font color=Blue>Added Successfully</font>";
                    }
                    else
                    {
                        lbl_error.Text = "Error Occured";
                    }
                }
                else
                {
                    lbl_error.Text = "Error occured in updating employee's basic salary";
                }
            }
            else
            {
                lbl_error.Text = "Error Occured";
            }
        }
        else
        {
            lbl_error.Text = "No Record Found";
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ddl_desig.SelectedIndex = -1;
        ddl_grade.SelectedIndex = -1;
        ddl_level.SelectedIndex = -1;
        txt_basic.Text = "";
        txt_edate.Text = "";
    }
}
