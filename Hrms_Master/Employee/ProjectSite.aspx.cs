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


    public partial class Hrms_Master_Default : System.Web.UI.Page
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
        Collection<Employee> projectsiteList;
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
                            ddl_branch.Visible = false;
                            access();
                            break;

                        case "u": s_form = "13";
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
        pro_add.Visible = true;
        pro_branch.Visible = true;

        projectsiteList = employee.fn_projectsite1();

        if (projectsiteList.Count > 0)
        {
            grid_projectsite.DataSource = projectsiteList;
            grid_projectsite.DataBind();
        }
        else
        {
            projectsiteList = employee.fn_EmptyprojectsiteList();

            if (projectsiteList.Count > 0)
            {
                grid_projectsite.DataSource = projectsiteList;
                grid_projectsite.DataBind();

                ((ImageButton)grid_projectsite.Rows[0].FindControl("img_update")).Visible = false;
            }

        }

    }

    public void load_admin()
    {
        pro_add.Visible = false;
        pro_branch.Visible = false;
        _connection = con.fn_Connection();
        _connection.Open();
        SqlDataAdapter ad = new SqlDataAdapter("select * from paym_branch", _connection);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        ddl_branch.DataTextField="branchname";
        ddl_branch.DataValueField="pn_branchid";
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
            string accesserror = "Permission Restricted. Please Contact Administrator.";
            //MessageBox.Show(accesserror);
            Response.Redirect("~/Company_Home.aspx");
        }
        rdrview.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and section_edit='No'", _connection);
        SqlDataReader rdredit = cmd.ExecuteReader();
        if (rdredit.Read())
        {
            for (int b = 0; b < grid_projectsite.Rows.Count; b++)
            {
                ((ImageButton)grid_projectsite.Rows[b].FindControl("img_update")).Visible = false;
            }
            ((System.Web.UI.Control)grid_projectsite.HeaderRow.FindControl("lbledit")).Visible = false;
        }
        rdredit.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and  section_delete='No'", _connection);
        SqlDataReader rdrdel = cmd.ExecuteReader();
        if (rdrdel.Read())
        {
            // ((ImageButton)grid_Course.Rows[0].FindControl("img_update")).Visible = false;
            for (int a = 0; a < grid_projectsite.Rows.Count; a++)
            {
                ((ImageButton)grid_projectsite.Rows[a].FindControl("imgdel")).Visible = false;
            }
            ((System.Web.UI.Control)grid_projectsite.HeaderRow.FindControl("lbldel")).Visible = false;
        }
        rdrdel.Close();

    }


    public void load1()
    {
        projectsiteList = employee.fn_projectsite1();

        if (projectsiteList.Count > 0)
        {
            grid_projectsite.DataSource = projectsiteList;
            grid_projectsite.DataBind();
            for (int c = 0; c < projectsiteList.Count; c++)
            {
                grid_projectsite.Rows[c].FindControl("chkid").Visible = false;
            }
        }
        else
        {
            projectsiteList = employee.fn_EmptyprojectsiteList();

            if (projectsiteList.Count > 0)
            {
                grid_projectsite.DataSource = projectsiteList;
                grid_projectsite.DataBind();

                ((ImageButton)grid_projectsite.Rows[0].FindControl("img_update")).Visible = false;
            }

        }

    }


    protected void Delete(object sender, GridViewDeleteEventArgs e)
    {
        //int Index = (int)AppointmentGrid.DataKeys[e.RowIndex].Value;
    }

    protected void Edit(object sender, GridViewEditEventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
            }
            employee.ProjectsiteId = Convert.ToInt32(grid_projectsite.DataKeys[e.NewEditIndex].Value);
            employee.ProjectsiteName = ((HtmlInputText)grid_projectsite.Rows[e.NewEditIndex].FindControl("txtgrid")).Value;

            if (employee.ProjectsiteName != "")
            {
                check = 0;
                
                if (check == 0)
                {

                    employee.fn_Update_projectsite(employee);

                    ((ImageButton)grid_projectsite.Rows[e.NewEditIndex].FindControl("img_update")).Visible = true;
                    ((ImageButton)grid_projectsite.Rows[e.NewEditIndex].FindControl("img_save")).Visible = false;
                    ((HtmlInputText)grid_projectsite.Rows[e.NewEditIndex].FindControl("txtgrid")).Disabled = true;
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

    

    protected void Update(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            ((HtmlInputText)grid_projectsite.Rows[e.RowIndex].FindControl("txtgrid")).Disabled = false;
            ((HtmlInputText)grid_projectsite.Rows[e.RowIndex].FindControl("txtgrid")).Focus();
            ((ImageButton)grid_projectsite.Rows[e.RowIndex].FindControl("img_save")).Visible = true;
            ((ImageButton)grid_projectsite.Rows[e.RowIndex].FindControl("img_update")).Visible = false;
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }

    }

    public int name_validate(string m_name)
    {

        projectsiteList = employee.fn_projectsite1();

        if (projectsiteList.Count > 0)
        {
            for (valid = 0; valid < projectsiteList.Count; valid++)
            {

                if (projectsiteList[valid].ProjectsiteName == m_name)
                {
                    temp_valid++;

                }

            }

        }
        return temp_valid;
    }

    protected void Button1_Click1(object sender, ImageClickEventArgs e)
    {

        if (s_login_role == "a")
        {
            employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
        }
            check = 0;

            if (check == 0)
            {
                employee.ProjectsiteId = Convert.ToInt32(hprojectsiteID.Value);
                employee.ProjectsiteName = txt_siteName.Value;
                //employee.Location = txtprojectsite.Value;
                employee.ProjectName = txt_Address.Value;
                employee.status = 'Y';

                _Value = employee.projectsiteUpdate(employee);

                if (_Value != "1")
                {
                    Error.Text = "<font color=Blue>Added Successfully</font>";
                }
                else
                {
                    Error.Text = "<font color=Red>Error Occured</font>";
                }
                ToolBarCode.Value = "0";

                projectsiteList = employee.fn_projectsite1();

                if (projectsiteList.Count > 0)
                {
                    grid_projectsite.DataSource = projectsiteList;
                    grid_projectsite.DataBind();
                }

                
            }
            else
            {

                ClientScriptManager manager = Page.ClientScript;
                manager.RegisterStartupScript(this.GetType(), "Call", "show_message();", true);


            }
        
        
    }
    protected void grid_projectsite_RowCommand(object sender, GridViewCommandEventArgs e)
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
                SqlCommand cmd = new SqlCommand("delete from paym_projectsite where v_projectsiteName='" + str + "' and branchid='" + employee.BranchId + "'", mycon);
                cmd.ExecuteNonQuery();
                if (s_login_role == "a")
                {
                    load();
                }
                else
                {
                    load1();
                }
                lbl_Error.Text = "Project Site Deleted Successfully";
            }
            catch (Exception exc)
            {
                lbl_Error.Text = "Couldnt delete because the project site is assigned to employee";
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

