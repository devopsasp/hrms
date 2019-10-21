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
    private SqlConnection _connection;
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlConnection mycon = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    SqlDataReader rea;

    Company company = new Company();

    Employee employee = new Employee();


    Collection<Company> BranchsList;
    Collection<Employee> DivisionList;
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
                    //branch_header.Visible = false;
                    branch_body.Visible = false;
                    grid_Branch.Visible = false;
                    break;

                case "u": s_form = "6";
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
        div_add.Visible = true;
        div_branch.Visible = true;
        Button2.Visible = true;

        CompanyList = company.fn_getCompany();

        if (s_login_role == "a" && CompanyList.Count > 0)
        {

            DivisionList = employee.fn_Division1();

            if (DivisionList.Count > 0)
            {
                grid_Division.DataSource = DivisionList;
                grid_Division.DataBind();
            }
            else
            {
                DivisionList = employee.fn_EmptyDivisionList();

                if (DivisionList.Count > 0)
                {
                    grid_Division.DataSource = DivisionList;
                    grid_Division.DataBind();

                    ((ImageButton)grid_Division.Rows[0].FindControl("img_update")).Visible = false;
                }


            }
            BranchsList = company.fn_getBranchs();

            if (BranchsList.Count > 0)    //first branch is company
            {
                grid_Branch.DataSource = BranchsList;
                grid_Branch.DataBind();

            }
        }
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
            for (int b = 0; b < grid_Division.Rows.Count; b++)
            {
                ((ImageButton)grid_Division.Rows[b].FindControl("img_update")).Visible = false;
            }
            ((System.Web.UI.Control)grid_Division.HeaderRow.FindControl("lbledit")).Visible = false;
        }
        rdredit.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and  section_delete='No'", _connection);
        SqlDataReader rdrdel = cmd.ExecuteReader();
        if (rdrdel.Read())
        {
            // ((ImageButton)grid_Course.Rows[0].FindControl("img_update")).Visible = false;
            for (int a = 0; a < grid_Division.Rows.Count; a++)
            {
                ((ImageButton)grid_Division.Rows[a].FindControl("imgdel")).Visible = false;
            }
            ((System.Web.UI.Control)grid_Division.HeaderRow.FindControl("lbldel")).Visible = false;
        }
        rdrdel.Close();

    }

    public void load1()
    {
        CompanyList = company.fn_getCompany();
        grid_Branch.Visible = false;
        //Label1.Visible = false;
        Button2.Visible = false;
        if (s_login_role == "h" && CompanyList.Count > 0)
        {

            DivisionList = employee.fn_Division1();

            if (DivisionList.Count > 0)
            {
                grid_Division.DataSource = DivisionList;
                grid_Division.DataBind();
                for (int c = 0; c < DivisionList.Count; c++)
                {
                    grid_Division.Rows[c].FindControl("chkid").Visible = false;
                }
            }
            else
            {
                DivisionList = employee.fn_EmptyDivisionList();

                if (DivisionList.Count > 0)
                {
                    grid_Division.DataSource = DivisionList;
                    grid_Division.DataBind();

                    ((ImageButton)grid_Division.Rows[0].FindControl("img_update")).Visible = false;
                }


            }
            
        }
    }

    public void load_admin()
    {
        div_add.Visible = false;
        div_branch.Visible = false;
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


    protected void Edit(object sender, GridViewEditEventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
            }
            employee.DivisionId = Convert.ToInt32(grid_Division.DataKeys[e.NewEditIndex].Value);
            employee.DivisionName = ((HtmlInputText)grid_Division.Rows[e.NewEditIndex].FindControl("txtgrid")).Value;
            //employee.status = 'Y';

            if (employee.DivisionName != "")
            {
                check = 0;
               // check = name_validate(employee.DivisionName);

                if (check == 0)
                {
                    employee.fn_Update_Division(employee);

                    ((ImageButton)grid_Division.Rows[e.NewEditIndex].FindControl("img_update")).Visible = true;
                    ((ImageButton)grid_Division.Rows[e.NewEditIndex].FindControl("img_save")).Visible = false;
                    ((HtmlInputText)grid_Division.Rows[e.NewEditIndex].FindControl("txtgrid")).Disabled = true;


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
            for (int j = 0; j < grid_Division.Rows.Count; j++)
            {
                GridViewRow Division_row = grid_Division.Rows[j];
                bool Division_check = ((HtmlInputCheckBox)Division_row.FindControl("Chk_Division")).Checked;

                if (Division_check)
                {
                    for (int i = 0; i < grid_Branch.Rows.Count; i++)
                    {
                        GridViewRow Branch_row = grid_Branch.Rows[i];

                        bool Branch_check = ((HtmlInputCheckBox)Branch_row.FindControl("Chk_Branch")).Checked;


                        if (Branch_check)
                        {
                            //employee.CompanyId = company_Id;
                            employee.DivisionName = ((HtmlInputText)Division_row.FindControl("txtgrid")).Value;
                            employee.DivisionId = 0;
                            employee.BranchId = Convert.ToInt32(grid_Branch.DataKeys[Branch_row.RowIndex].Value);

                            _Value = employee.DivisionUpdate(employee);

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

            ((HtmlInputText)grid_Division.Rows[e.RowIndex].FindControl("txtgrid")).Disabled = false;
            ((HtmlInputText)grid_Division.Rows[e.RowIndex].FindControl("txtgrid")).Focus();
            ((ImageButton)grid_Division.Rows[e.RowIndex].FindControl("img_save")).Visible = true;
            ((ImageButton)grid_Division.Rows[e.RowIndex].FindControl("img_update")).Visible = false;
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    public int name_validate(string m_name)
    {
        DivisionList = employee.fn_Division1();
        if (DivisionList.Count > 0)
        {
            for (valid = 0; valid < DivisionList.Count; valid++)
            {
                if (DivisionList[valid].DivisionName == m_name)
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
            check = name_validate(DivisionName.Value);

            if (check == 0)
            {

                employee.DivisionId = Convert.ToInt32(hDivisionID.Value);
                employee.DivisionName = DivisionName.Value;
                employee.status = 'Y';

                _Value = employee.DivisionUpdate(employee);
                if (_Value != "1")
                {
                    Error.Text = "<font color=Blue>Added Successfully</font>";
                }
                else
                {
                    Error.Text = "<font color=Red>Error Occured</font>";
                }

                DivisionList = employee.fn_Division1();

                if (DivisionList.Count > 0)
                {
                    grid_Division.DataSource = DivisionList;
                    grid_Division.DataBind();
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
    protected void grid_Division_RowCommand(object sender, GridViewCommandEventArgs e)
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
                SqlCommand cmd = new SqlCommand("delete from paym_division where v_DivisionName='" + str + "' and Branchid='" + employee.BranchId + "'", mycon);
                cmd.ExecuteNonQuery();
                if (s_login_role == "a")
                {
                    load();
                }
                else
                {
                    load1();
                }
                lbl_Error.Text = "Division Deleted Successfully";
            }
            catch (Exception exc)
            {
                lbl_Error.Text = "Couldnt delete because the division is assigned to employee";
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

