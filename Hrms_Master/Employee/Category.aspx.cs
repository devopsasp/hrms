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

public partial class Hrms_Master_Default4 : System.Web.UI.Page
{
    private SqlConnection _connection;
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlConnection mycon = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    SqlDataReader rea;
    Company company = new Company();
    Employee employee = new Employee();

    Collection<Company> BranchsList;
    Collection<Employee> CategoryList;
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
                case "a": load_admin();
                    break;

                case "h": load1();
                    //branch_header.Visible = false;
                    ddl_branch.Visible = false;
                    grid_Branch.Visible = false;
                    branch_body.Visible = false;
                    access();
                    break;

                case "u": s_form = "10";
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

        if (IsPostBack)
        {
            try
            {
                if (ToolBarCode.Value == "1")
                {
                    employee.CategoryId = Convert.ToInt32(hCategoryID.Value);
                    employee.CategoryName = CategoryName.Value;

                    _Value = employee.CategoryUpdate(employee);
                    if (_Value != "1")
                    {
                        Error.Text = "<font color=Blue>Added Successfully</font>";
                    }
                    else
                    {
                        Error.Text = "<font color=Red>Error Occured</font>";
                    }

                    CategoryList = employee.fn_Category();
                    if (CategoryList.Count > 0)
                    {
                        grid_Category.DataSource = CategoryList;
                        grid_Category.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lbl_Error.Text = "Error";
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
            employee.CategoryId = Convert.ToInt32(grid_Category.DataKeys[e.NewEditIndex].Value);
            employee.CategoryName = ((HtmlInputText)grid_Category.Rows[e.NewEditIndex].FindControl("txtgrid")).Value;

            if (employee.CategoryName != "")
            {
                check = 0;
                //check = name_validate(employee.CategoryName);
                if (check == 0)
                {
                    employee.fn_Update_Category(employee);

                    ((ImageButton)grid_Category.Rows[e.NewEditIndex].FindControl("img_update")).Visible = true;
                    ((ImageButton)grid_Category.Rows[e.NewEditIndex].FindControl("img_save")).Visible = false;
                    ((HtmlInputText)grid_Category.Rows[e.NewEditIndex].FindControl("txtgrid")).Disabled = true;
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

    public void load()
    {
        cat_add.Visible = true;
        cat_branch.Visible = true;
        Button2.Visible = true;

        CategoryList = employee.fn_Category1();
        if (CategoryList.Count > 0)
        {
            grid_Category.DataSource = CategoryList;
            grid_Category.DataBind();
        }
        else
        {
            CategoryList = employee.fn_EmptyCategoryList();
            if (CategoryList.Count > 0)
            {
                grid_Category.DataSource = CategoryList;
                grid_Category.DataBind();
                ((ImageButton)grid_Category.Rows[0].FindControl("img_update")).Visible = false;
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
        cat_add.Visible = false;
        cat_branch.Visible = false;
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
            for (int b = 0; b < grid_Category.Rows.Count; b++)
            {
                ((ImageButton)grid_Category.Rows[b].FindControl("img_update")).Visible = false;
            }
            ((System.Web.UI.Control)grid_Category.HeaderRow.FindControl("lbledit")).Visible = false;
        }
        rdredit.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and  section_delete='No'", _connection);
        SqlDataReader rdrdel = cmd.ExecuteReader();
        if (rdrdel.Read())
        {
            // ((ImageButton)grid_Course.Rows[0].FindControl("img_update")).Visible = false;
            for (int a = 0; a < grid_Category.Rows.Count; a++)
            {
                ((ImageButton)grid_Category.Rows[a].FindControl("imgdel")).Visible = false;
            }
            ((System.Web.UI.Control)grid_Category.HeaderRow.FindControl("lbldel")).Visible = false;
        }
        rdrdel.Close();

    }

    public void load1()
    {
        Button2.Visible = false;
        grid_Branch.Visible = false;
        CategoryList = employee.fn_Category1();
        if (CategoryList.Count > 0)
        {
            grid_Category.DataSource = CategoryList;
            grid_Category.DataBind();
            
            for (int c = 0; c < CategoryList.Count; c++)
            {
                grid_Category.Rows[c].FindControl("chkid").Visible = false;
            }
        }
        else
        {
            CategoryList = employee.fn_EmptyCategoryList();
            if (CategoryList.Count > 0)
            {
                grid_Category.DataSource = CategoryList;
                grid_Category.DataBind();
                ((ImageButton)grid_Category.Rows[0].FindControl("img_update")).Visible = false;
            }
        }
    }


    protected void Delete(object sender, GridViewDeleteEventArgs e)
    {
        //int Index = (int)AppointmentGrid.DataKeys[e.RowIndex].Value;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            for (int j = 0; j < grid_Category.Rows.Count; j++)
            {
                GridViewRow Category_row = grid_Category.Rows[j];
                bool Category_check = ((HtmlInputCheckBox)Category_row.FindControl("Chk_Category")).Checked;
                
                if (Category_check)
                {
                    for (int i = 0; i < grid_Branch.Rows.Count; i++)
                    {
                        GridViewRow Branch_row = grid_Branch.Rows[i];
                        bool Branch_check = ((HtmlInputCheckBox)Branch_row.FindControl("Chk_Branch")).Checked;
                
                        if (Branch_check)
                        {
                            // employee.CompanyId = company_Id;
                            employee.CategoryName = ((HtmlInputText)Category_row.FindControl("txtgrid")).Value;
                            employee.CategoryId = 0;
                            employee.BranchId = Convert.ToInt32(grid_Branch.DataKeys[Branch_row.RowIndex].Value);
                            _Value = employee.CategoryUpdate(employee);
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


    protected void Update(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            ((HtmlInputText)grid_Category.Rows[e.RowIndex].FindControl("txtgrid")).Disabled = false;
            ((HtmlInputText)grid_Category.Rows[e.RowIndex].FindControl("txtgrid")).Focus();
            ((ImageButton)grid_Category.Rows[e.RowIndex].FindControl("img_save")).Visible = true;
            ((ImageButton)grid_Category.Rows[e.RowIndex].FindControl("img_update")).Visible = false;
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    public int name_validate(string m_name)
    {
        CategoryList = employee.fn_Category1();
        
        if (CategoryList.Count > 0)
        {
            for (valid = 0; valid < CategoryList.Count; valid++)
            {
                if (CategoryList[valid].CategoryName == m_name)
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
            check = name_validate(CategoryName.Value);
            if (check == 0)
            {
                employee.CategoryId = Convert.ToInt32(hCategoryID.Value);
                employee.CategoryName = CategoryName.Value;
                employee.status = 'y';

                _Value = employee.CategoryUpdate(employee);
                if (_Value != "1")
                {
                    Error.Text = "<font color=Blue>Added Successfully</font>";
                }
                else
                {
                    Error.Text = "<font color=Red>Error Occured</font>";
                }

                CategoryList = employee.fn_Category1();
                if (CategoryList.Count > 0)
                {
                    grid_Category.DataSource = CategoryList;
                    grid_Category.DataBind();
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


    protected void grid_Category_RowCommand(object sender, GridViewCommandEventArgs e)
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
                string str = lnkbtn.Value;

                mycon.Open();
                SqlCommand cmd = new SqlCommand("delete from paym_category where v_CategoryName='" + str + "' and Branchid='" + employee.BranchId + "'", mycon);
                cmd.ExecuteNonQuery();
                if (s_login_role == "a")
                {
                    load();
                }
                else
                {
                    load1();
                }
                lbl_Error.Text = "Category Deleted Successfully";
            }
            catch (Exception exc)
            {
                lbl_Error.Text = "Couldnt delete because the category is assigned to employee";
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
