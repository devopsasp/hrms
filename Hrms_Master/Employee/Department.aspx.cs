using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.ObjectModel;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ePayHrms.Company;
using ePayHrms.Employee;
using System.Data.SqlClient;
using System.Windows.Forms;

public partial class Hrms_Master_Default : System.Web.UI.Page
{
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    private SqlConnection _connection;
    SqlConnection mycon = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    Company company = new Company();
    Employee employee = new Employee();
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    SqlDataReader rea;

    Collection<Company> BranchsList;
    Collection<Employee> DepartmentList;
    Collection<Company> CompanyList;

    int company_Id, branch_Id, valid, temp_valid = 0, check;
    string _Value;
    string s_login_role;

    string s_form = "";
    DataSet ds_userrights;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        lbl_Error.Text = "";
        Error.Text = "";

        if (!IsPostBack)
        {
            switch (s_login_role)
            {
                case "a":
                    load_admin();
                    
                    break;

                case "h":
                    load1();
                    branch_body.Visible = false;
                    ddl_branch.Visible = false;
                    //branch_header.Visible = false;
                    grid_Branch.Visible = false;
                    access();
                    break;

                case "u": s_form = "5";
                    ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                    if (ds_userrights.Tables[0].Rows.Count > 0)
                    {
                        load();
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

    public void load()
    {
        branch_body.Visible = true;
        dept_body.Visible = true;
        dept_add.Visible = true;
        Button2.Visible = true;

        DepartmentList = employee.fn_Department(employee.BranchId);

        if (DepartmentList.Count > 0)
        {
            grid_Department.DataSource = DepartmentList;
            grid_Department.DataBind();
        }
        else
        {
            DepartmentList = employee.fn_EmptyDepartmentList();

            if (DepartmentList.Count > 0)
            {
                grid_Department.DataSource = DepartmentList;
                grid_Department.DataBind();

                ((ImageButton)grid_Department.Rows[0].FindControl("img_update")).Visible = false;
            }
        }
        BranchsList = company.fn_getBranchs();

        if (BranchsList.Count > 0)    //first branch is company
        {
            grid_Branch.DataSource = BranchsList;
            grid_Branch.DataBind();
        }
    }

    public void load_admin()
    {
        branch_body.Visible = false;
        dept_body.Visible = false;
        dept_add.Visible = false;
        Button2.Visible = false;
        _connection = con.fn_Connection();
        _connection.Open();
        SqlDataAdapter ad = new SqlDataAdapter("select * from paym_branch", _connection);        
        DataSet ds = new DataSet();
        ad.Fill(ds);
        ddl_branch.DataTextField = "branchname";
        ddl_branch.DataValueField = "pn_branchid";
        ddl_branch.DataSource = ds;
        ddl_branch.DataBind();
        ddl_branch.Items.Insert(0, "Select Branch");
        _connection.Close();
    }

    public void access()
    {
        _connection = con.fn_Connection();
        _connection.Open();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and section_view='No'", _connection);
        SqlDataReader rdrview = cmd.ExecuteReader();
        if (rdrview.Read())
        {
            Response.Write("<script language='javascript'>alert('Permission Restricted. Please Contact Administrator.');window.location='~/Company_Home.aspx';</script>");
        }
        rdrview.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and section_edit='No'", _connection);
        SqlDataReader rdredit = cmd.ExecuteReader();
        if (rdredit.Read())
        {
            for (int b = 0; b < grid_Department.Rows.Count; b++)
            {
                ((ImageButton)grid_Department.Rows[b].FindControl("img_update")).Visible = false;
            }
            ((System.Web.UI.Control)grid_Department.HeaderRow.FindControl("lbledit")).Visible = false;
        }
        rdredit.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and  section_delete='No'", _connection);
        SqlDataReader rdrdel = cmd.ExecuteReader();
        if (rdrdel.Read())
        {
            // ((ImageButton)grid_Course.Rows[0].FindControl("img_update")).Visible = false;
            for (int a = 0; a < grid_Department.Rows.Count; a++)
            {
                ((ImageButton)grid_Department.Rows[a].FindControl("imgdel")).Visible = false;
            }
            ((System.Web.UI.Control)grid_Department.HeaderRow.FindControl("lbldel")).Visible = false;
        }
        rdrdel.Close();

    }

    public void load1()
    {
        Button2.Visible = false;
        grid_Branch.Visible = false;
        DepartmentList = employee.fn_Department1(employee);

        if (DepartmentList.Count > 0)
        {
            grid_Department.DataSource = DepartmentList;
            grid_Department.DataBind();

            for (int c = 0; c < DepartmentList.Count; c++)
            {
                grid_Department.Rows[c].FindControl("chkid").Visible = false;
            }
        }
        else
        {
            DepartmentList = employee.fn_EmptyDepartmentList();

            if (DepartmentList.Count > 0)
            {
                grid_Department.DataSource = DepartmentList;
                grid_Department.DataBind();
                ((ImageButton)grid_Department.Rows[0].FindControl("img_update")).Visible = false;
            }
        }

    }


    protected void Edit(object sender, GridViewEditEventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
            }
            employee.DepartmentId = Convert.ToInt32(grid_Department.DataKeys[e.NewEditIndex].Value);
            employee.DepartmentName = ((HtmlInputText)grid_Department.Rows[e.NewEditIndex].FindControl("txtgrid")).Value;
            //employee.status = 'Y';
            if (employee.DepartmentName != "")
            {
                check = 0;
                //check = name_validate(employee.DepartmentName);

                if (check == 0)
                {
                    employee.fn_Update_Department(employee);

                    ((ImageButton)grid_Department.Rows[e.NewEditIndex].FindControl("img_update")).Visible = true;
                    ((ImageButton)grid_Department.Rows[e.NewEditIndex].FindControl("img_save")).Visible = false;
                    ((HtmlInputText)grid_Department.Rows[e.NewEditIndex].FindControl("txtgrid")).Disabled = true;
                }
                else
                {
                    ClientScriptManager manager = Page.ClientScript;
                    manager.RegisterStartupScript(this.GetType(), "Call", "show_message();", true);
                }
            }
            else
            {
                ClientScriptManager manager = Page.ClientScript;
                manager.RegisterStartupScript(this.GetType(), "Call", "show_Error();", true);
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    protected void Delete(object sender, GridViewDeleteEventArgs e)
    {
        //int Index = (int)AppointmentGrid.DataKeys[e.RowIndex].Value;
    }

    protected void Update(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            ((HtmlInputText)grid_Department.Rows[e.RowIndex].FindControl("txtgrid")).Disabled = false;
            ((HtmlInputText)grid_Department.Rows[e.RowIndex].FindControl("txtgrid")).Focus();
            ((ImageButton)grid_Department.Rows[e.RowIndex].FindControl("img_save")).Visible = true;
            ((ImageButton)grid_Department.Rows[e.RowIndex].FindControl("img_update")).Visible = false;
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    
    public int name_validate(string m_name)
    {
        DepartmentList = employee.fn_Department1(employee);

        if (DepartmentList.Count > 0)
        {
            for (valid = 0; valid < DepartmentList.Count; valid++)
            {
                if (DepartmentList[valid].DepartmentName == m_name)
                {
                    temp_valid++;
                }
            }
        }
        return temp_valid;
    }

    protected void Button1_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
            }
            check = name_validate(DepartmentName.Value);

            if (check == 0)
            {
                employee.DepartmentId = Convert.ToInt32(hDepartmentID.Value);
                employee.DepartmentName = DepartmentName.Value;
                employee.status = 'Y';

                _Value = employee.DepartmentUpdate(employee);
                if (_Value != "1")
                {
                    Error.Text = "<font color=Blue>Added Successfully</font>";
                }
                else
                {
                    Error.Text = "<font color=Red>Error Occured</font>";
                }

                DepartmentList = employee.fn_Department1(employee);

                if (DepartmentList.Count > 0)
                {
                    grid_Department.DataSource = DepartmentList;
                    grid_Department.DataBind();
                }
            }
            else
            {
                ClientScriptManager manager = Page.ClientScript;
                manager.RegisterStartupScript(this.GetType(), "Call", "show_message();", true);
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }



    protected void Button2_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            for (int j = 0; j < grid_Department.Rows.Count; j++)
            {
                GridViewRow Department_row = grid_Department.Rows[j];
                bool Department_check = ((HtmlInputCheckBox)Department_row.FindControl("Chk_Department")).Checked;

                if (Department_check)
                {
                    for (int i = 0; i < grid_Branch.Rows.Count; i++)
                    {
                        GridViewRow Branch_row = grid_Branch.Rows[i];
                        bool Branch_check = ((HtmlInputCheckBox)Branch_row.FindControl("Chk_Branch")).Checked;
                        
                        if (Branch_check)
                        {
                            //employee.CompanyId = company_Id;
                            employee.DepartmentName = ((HtmlInputText)Department_row.FindControl("txtgrid")).Value;
                            employee.DepartmentId = 0;
                            employee.BranchId = Convert.ToInt32(grid_Branch.DataKeys[Branch_row.RowIndex].Value);
                            _Value = employee.DepartmentUpdate(employee);

                            if (_Value != "1")
                            {
                                Error.Text = "<font color=Blue>Added Successfully</font>";
                            }
                            else
                            { 
                                Error.Text = "<font color=Red>Error Occured</font>";
                            }
                            //((CheckBox)Employee_row.FindControl("Employee_select")).BackColor = System.Drawing.Color.Blue;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }
    protected void grid_Department_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }
    protected void grid_Department_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            try
            {
                //finding row index
                if (s_login_role == "a")
                {
                    employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
                }
                GridViewRow gvrow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);//catching the row in which thhe link button is clicked.
                HtmlInputText lnkbtn = (HtmlInputText)gvrow.FindControl("txtgrid");

                mycon.Open();
                SqlCommand cmd = new SqlCommand("delete from paym_department where v_DepartmentName='" + lnkbtn.Value + "' and pn_branchid='" + employee.BranchId + "'", mycon);
                cmd.ExecuteNonQuery();
                if (s_login_role == "a")
                {
                    load();
                }
                else
                {
                    load1();
                }
                lbl_Error.Text = "Department Deleted Successfully";
            }
            catch (Exception exc)
            {
                lbl_Error.Text = "Couldn't delete because the department is assigned to employee";
                lbl_Error.Visible = true;
            }
        }
        mycon.Close();
    }
    protected void ddl_branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (s_login_role == "a")
        {
            employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
        }
        load();
    }
}