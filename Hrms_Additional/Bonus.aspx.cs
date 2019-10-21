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
    ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader rea;
    SqlDataAdapter ada = new SqlDataAdapter();
    Company company = new Company();
    Employee employee = new Employee();
    Leave l = new Leave();

    Collection<Leave> LeaveList;
    Collection<Company> CompanyList;
    Collection<Employee> EmployeeList;
    Collection<Company> ddlBranchsList;
    Collection<Employee> EmpProfileList;

    string _Code;
    string s_login_role;
    int i, j, cur_yr, ddl_i, temp_count = 0, balance = 0, temp_check = 0, temp_check2 = 0, app_amt = 0, app_pnt = 0 ,app_tot=0,emp_code;
    bool avail = false, temp_avail = false, check = true;
    string s_form = "";
    DataSet ds_userrights;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        lbl_Error.Text = "";
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        Session["formulaBonus"] = "";
        l.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;

        if (!IsPostBack)
        {

            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

                btn_save.Visible = false;
                btn_update.Visible = false;
                
                Tr2.Visible = false;
                switch (s_login_role)
                {
                    case "a": row_emp.Visible = false;
                        ddl_Branch_load();
                        break;

                    case "h": ddl_Branch.Visible = false;
                        ddl_department_load();
                        
                        break;

                    case "e": ddl_Branch.Visible = false;
                        row_emp.Visible = false;
                        btn_save.Visible = false;
                        txt_date.Disabled = true;
                        l.EmployeeID = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
                        Emp_grid_load();
                       
                        break;

                    case "u": //s_form = "5";
                        s_form = "41";
                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            ddl_Branch.Visible = false;
                            ddl_employee_load();
                        }
                        else
                        {
                            ddl_Branch.Visible = false;
                            row_emp.Visible = false;
                            btn_save.Visible = false;
                            txt_date.Disabled = true;
                            l.EmployeeID = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
                            Emp_grid_load();
                            
                        }

                        break;

                    default: Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                        Response.Redirect("Company_Home.aspx");
                        break;
                }
            }
            else
            {
                Response.Cookies["Msg_Session"].Value = "Create Company";
                Response.Redirect("Company_Home.aspx");
            }
        }
    }

    public void Emp_grid_load()
    {
        
        myConnection.Open();
        int gid = 0;
        string gname = "";
        cmd = new SqlCommand("select pn_gradeID from paym_employee_profile1 where pn_EmployeeID='" + l.EmployeeID + "'", myConnection);
        rea = cmd.ExecuteReader();
        while (rea.Read())
        {
            gid = Convert.ToInt32(rea["pn_GradeID"]);
        }
        rea.Close();
        cmd = new SqlCommand("select v_GradeName from paym_grade where pn_gradeID=" + gid + "", myConnection);
        rea = cmd.ExecuteReader();
        if (rea.Read())
        {
            gname = Convert.ToString(rea["v_GradeName"]);
        }
        rea.Close();
        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM bonus_increment where pn_BranchID='" + employee.BranchId + "' and grade = '"+gname+"'", myConnection);
        DataSet ds = new DataSet();
        ad.Fill(ds, "bonus_increment");
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView1.DataSource = ds;
            GridView1.DataBind();
            int columnCount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columnCount;
            GridView1.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        btn_Back.Visible = true;
        btn_save.Visible = false;
        btn_update.Visible = false;
        Tr2.Visible = false;
    }

   

    public void grid_load()
    {

        
    }

    

    public void apprisal_Amount()
    {
       
    }

    public void ddl_Branch_load()
    {

        //branch dropdown

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

    public void save()
    {

        
    }

    
    

    protected void btn_Back_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/Company_Home.aspx");
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    protected void ddl_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddl_Branch.SelectedValue != "0")
            {
                ViewState["Appraisal_BranchID"] = Convert.ToInt32(ddl_Branch.SelectedItem.Value);
                ddl_employee_load();
                tbl_details.Visible = true;
                Tr2.Visible = false;
               
            }
            else
            {
                tbl_details.Visible = false;
                tbl_grd.Visible = false;
            }
        }

        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    public void ddl_employee_load()
    {
        //employee dropdown
        ddl_Employee.Items.Clear();

        
        if (s_login_role == "a")
        {
            employee.BranchId = (int)ViewState["Appraisal_BranchID"];
        }

        if (s_login_role == "h")
        {
            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        }

        EmployeeList = employee.fn_getEmployeeList(employee);

        if (EmployeeList.Count > 0)
        {
            row_emp.Visible = true;
            tbl_details.Visible = true;
            for (ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();

                    e_list.Text = "Select Employee";
                    e_list.Value = "0";
                    ddl_Employee.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();

                    e_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
                    e_list.Text = EmployeeList[ddl_i].LastName.ToString();
                    ddl_Employee.Items.Add(e_list);
                }
            }
        }
        else
        {
            lbl_Error.Text = "No Employee";
            tbl_details.Visible = false;
            tbl_grd.Visible = false;
            row_emp.Visible = false;
        }
    }


    public void ddl_department_load()
    {
        
        Emp_details.Visible = false;

        if (s_login_role == "a")
        {
            employee.BranchId = Convert.ToInt32(ddl_Branch.SelectedItem.Value);
        }
        EmployeeList = employee.fn_getDepartmentList1(employee.BranchId);
        if (EmployeeList.Count > 0)
        {
            
            for (int ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem list = new ListItem();

                    list.Text = "Select Department";
                    list.Value = "sd";
                    ddl_department.Items.Add(list);
                }
                else
                {
                    ListItem list = new ListItem();

                    list.Text = EmployeeList[ddl_i].DepartmentName;
                    list.Value = EmployeeList[ddl_i].DepartmentId.ToString();
                    ddl_department.Items.Add(list);
                }
            }
        }
        else
        {
            lbl_Error.Text = "No Department Available";
        }

    }

    protected void ddl_Employee_SelectedIndexChanged(object sender, EventArgs e)
    {
       
            
            if (ddl_Employee.SelectedValue != "0")
            {
                
                ViewState["Appraisal_EmployeeID"] = Convert.ToInt32(ddl_Employee.SelectedItem.Value);

                //l.BranchID = (int)ViewState["Appraisal_BranchID"];
                if (s_login_role == "a")
                {
                    l.BranchID = (int)ViewState["Bouns_BranchID"];
                }

                if (s_login_role == "h")
                {
                    l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                }

                l.EmployeeID = (int)ViewState["Appraisal_EmployeeID"];
                Emp_grid_load();
                
                tbl_grd.Visible = true;
            }
            else
            {
                tbl_grd.Visible = false;
                Tr2.Visible = false;
            }
      
    }
    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_department.SelectedValue != "sd")
        {
            Emp_details.Visible = true;
            ddl_employee_load();
        }
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {

    }
    protected void btn_update_Click(object sender, EventArgs e)
    {

    }
}


