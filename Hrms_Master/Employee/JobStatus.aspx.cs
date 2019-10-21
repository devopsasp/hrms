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

public partial class Hrms_Master_Default5 : System.Web.UI.Page
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
    Collection<Employee> JobStatusList;
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
                    access();
                    ddl_branch.Visible = false;
                    branch_body.Visible = false;
                    //branch_header.Visible = false;
                    grid_Branch.Visible = false;
                    break;

                case "u": s_form = "11";
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
        job_add.Visible = true;
        job_branch.Visible = true;
        Button2.Visible = true;

        JobStatusList = employee.fn_JobStatus1();

        if (JobStatusList.Count > 0)
        {
            grid_JobStatus.DataSource = JobStatusList;
            grid_JobStatus.DataBind();
        }
        else
        {

            JobStatusList = employee.fn_EmptyJobStatusList();

            if (JobStatusList.Count > 0)
            {
                grid_JobStatus.DataSource = JobStatusList;
                grid_JobStatus.DataBind();

                ((ImageButton)grid_JobStatus.Rows[0].FindControl("img_update")).Visible = false;
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
        job_add.Visible = false;
        job_branch.Visible = false;
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
        // MessageBox.Show(employee.BranchId.ToString());
        // MessageBox.Show(employee.CompanyId.ToString());
        _connection = con.fn_Connection();
        _connection.Open();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and section_view='No'", _connection);
        SqlDataReader rdrview = cmd.ExecuteReader();
        if (rdrview.Read())
        {
            string accesserror = "Permission Restricted. Please Contact Administrator.";
            MessageBox.Show(accesserror);
            //Response.Write("<script>alert('Permission Restricted. Please Contact Administrator.')</script>");
            Response.Redirect("~/Company_Home.aspx");
        }
        rdrview.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and section_edit='No'", _connection);
        SqlDataReader rdredit = cmd.ExecuteReader();
        if (rdredit.Read())
        {
            for (int b = 0; b < grid_JobStatus.Rows.Count; b++)
            {
                ((ImageButton)grid_JobStatus.Rows[b].FindControl("img_update")).Visible = false;
            }
            ((System.Web.UI.Control)grid_JobStatus.HeaderRow.FindControl("lbledit")).Visible = false;
        }
        rdredit.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and  section_delete='No'", _connection);
        SqlDataReader rdrdel = cmd.ExecuteReader();
        if (rdrdel.Read())
        {
            // ((ImageButton)grid_Course.Rows[0].FindControl("img_update")).Visible = false;
            for (int a = 0; a < grid_JobStatus.Rows.Count; a++)
            {
                ((ImageButton)grid_JobStatus.Rows[a].FindControl("imgdel")).Visible = false;
            }
            ((System.Web.UI.Control)grid_JobStatus.HeaderRow.FindControl("lbldel")).Visible = false;
        }
        rdrdel.Close();

    }

    public void load1()
    {
        JobStatusList = employee.fn_JobStatus1();
        grid_Branch.Visible = false;
        //Label1.Visible = false;
        Button2.Visible = false;
        if (JobStatusList.Count > 0)
        {
            grid_JobStatus.DataSource = JobStatusList;
            grid_JobStatus.DataBind();
            for (int c = 0; c < JobStatusList.Count; c++)
            {
                grid_JobStatus.Rows[c].FindControl("chkid").Visible = false;
            }
        }
        else
        {

            JobStatusList = employee.fn_EmptyJobStatusList();

            if (JobStatusList.Count > 0)
            {
                grid_JobStatus.DataSource = JobStatusList;
                grid_JobStatus.DataBind();

                ((ImageButton)grid_JobStatus.Rows[0].FindControl("img_update")).Visible = false;
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
            employee.JobStatusId = Convert.ToInt32(grid_JobStatus.DataKeys[e.NewEditIndex].Value);
            employee.JobStatusName = ((HtmlInputText)grid_JobStatus.Rows[e.NewEditIndex].FindControl("txtgrid")).Value;

            if (employee.JobStatusName != "")
            {
                check = 0;
                //check = name_validate(employee.JobStatusName);

                if (check == 0)
                {

                    employee.fn_Update_JobStatus(employee);

                    ((ImageButton)grid_JobStatus.Rows[e.NewEditIndex].FindControl("img_update")).Visible = true;
                    ((ImageButton)grid_JobStatus.Rows[e.NewEditIndex].FindControl("img_save")).Visible = false;
                    ((HtmlInputText)grid_JobStatus.Rows[e.NewEditIndex].FindControl("txtgrid")).Disabled = true;

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

    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {

            for (int j = 0; j < grid_JobStatus.Rows.Count; j++)
            {

                GridViewRow JobStatus_row = grid_JobStatus.Rows[j];


                bool JobStatus_check = ((HtmlInputCheckBox)JobStatus_row.FindControl("Chk_JobStatus")).Checked;

                if (JobStatus_check)
                {
                    for (int i = 0; i < grid_Branch.Rows.Count; i++)
                    {
                        GridViewRow Branch_row = grid_Branch.Rows[i];

                        bool Branch_check = ((HtmlInputCheckBox)Branch_row.FindControl("Chk_Branch")).Checked;
                        if (Branch_check)
                        {
                            //employee.CompanyId = company_Id;
                            employee.JobStatusName = ((HtmlInputText)JobStatus_row.FindControl("txtgrid")).Value;
                            employee.JobStatusId = 0;
                            employee.BranchId = Convert.ToInt32(grid_Branch.DataKeys[Branch_row.RowIndex].Value);

                            _Value = employee.JobStatusUpdate(employee);

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

    //protected void Button1_Click1(object sender, EventArgs e)
    //{
        
    //}
    protected void Update(object sender, GridViewUpdateEventArgs e)
    {
        try
        {

            ((HtmlInputText)grid_JobStatus.Rows[e.RowIndex].FindControl("txtgrid")).Disabled = false;
            ((HtmlInputText)grid_JobStatus.Rows[e.RowIndex].FindControl("txtgrid")).Focus();
            ((ImageButton)grid_JobStatus.Rows[e.RowIndex].FindControl("img_save")).Visible = true;
            ((ImageButton)grid_JobStatus.Rows[e.RowIndex].FindControl("img_update")).Visible = false;

        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    public int name_validate(string m_name)
    {
        JobStatusList = employee.fn_JobStatus1();
        if (JobStatusList.Count > 0)
        {
            for (valid = 0; valid < JobStatusList.Count; valid++)
            {
                if (JobStatusList[valid].JobStatusName == m_name)
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
            check = name_validate(JobStatusName.Value);

            if (check == 0)
            {
                employee.JobStatusId = Convert.ToInt32(hJobStatusID.Value);
                employee.JobStatusName = JobStatusName.Value;
                employee.status = 'Y';
                
                _Value = employee.JobStatusUpdate(employee);
                if (_Value != "1")
                {
                    Error.Text = "<font color=Blue>Added Successfully</font>";
                }
                else
                {
                    Error.Text = "<font color=Red>Error Occured</font>";
                }

                JobStatusList = employee.fn_JobStatus1();

                if (JobStatusList.Count > 0)
                {
                    grid_JobStatus.DataSource = JobStatusList;
                    grid_JobStatus.DataBind();
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
    protected void grid_JobStatus_RowCommand(object sender, GridViewCommandEventArgs e)
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
                SqlCommand cmd = new SqlCommand("delete from paym_jobstatus where v_JobStatusName='" + str + "' and Branchid='" + employee.BranchId + "'", mycon);
                cmd.ExecuteNonQuery();
                if (s_login_role == "a")
                {
                    load();
                }
                else
                {
                    load1();
                }
                lbl_Error.Text = "Job Type Deleted Successfully";
            }
            catch (Exception exc)
            {
                lbl_Error.Text = "Couldnt delete because the job type is assigned to employee";
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

