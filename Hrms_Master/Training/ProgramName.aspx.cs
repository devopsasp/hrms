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

public partial class Hrms_Training_Default : System.Web.UI.Page
{

    private SqlConnection _connection;
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    SqlDataReader rea;

    Company company = new Company();

    Employee employee = new Employee();


    Collection<Company> BranchsList;
    Collection<Employee> prgmnameList;
    Collection<Company> CompanyList;
    int company_Id, branch_Id, valid, temp_valid, check;
    string _Value;
    string s_login_role;
    string s_form = "";
    DataSet ds_userrights;

    protected void Page_Load(object sender, EventArgs e)
    {

        
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        if (!IsPostBack)
        {
            switch (s_login_role)
            {
                case "a": load();
                    break;

                case "h": load1();
                    break;

                case "u": s_form = "28";
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
        prgmnameList = employee.fn_programname(employee);

        if (prgmnameList.Count > 0)
        {
            grid_pgmname.DataSource = prgmnameList;
            grid_pgmname.DataBind();
        }
        else
        {
            Error.Text = "<font color=Red>No Program added</font>";
        }

    }

    public void load1()
    {
        prgmnameList = employee.fn_programname1(employee.BranchId);

        if (prgmnameList.Count > 0)
        {
            grid_pgmname.DataSource = prgmnameList;
            grid_pgmname.DataBind();
        }
        else
        {
            Error.Text = "<font color=Red>No Program added</font>";
        }

    }


    //protected void Button1_Click1(object sender, EventArgs e)
    //{
        
    //}

    protected void Edit(object sender, GridViewEditEventArgs e)
    {
        try
        {

            employee.prgmid = Convert.ToInt32(grid_pgmname.DataKeys[e.NewEditIndex].Value);
            employee.prgmname = ((HtmlInputText)grid_pgmname.Rows[e.NewEditIndex].FindControl("txtgrid")).Value;

            if (employee.prgmname != "")
            {
                check = name_validate(employee.prgmname);
                if (check == 0)
                {
                    _Value = employee.fn_Update_pgmname(employee);
                    if (_Value == "0")
                    {
                        ((ImageButton)grid_pgmname.Rows[e.NewEditIndex].FindControl("img_update")).Visible = true;
                        ((ImageButton)grid_pgmname.Rows[e.NewEditIndex].FindControl("img_save")).Visible = false;
                        ((HtmlInputText)grid_pgmname.Rows[e.NewEditIndex].FindControl("txtgrid")).Disabled = true;
                    }
                    else
                    {
                        lbl_Error.Text = "Error Occured";
                    }
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
            ((HtmlInputText)grid_pgmname.Rows[e.RowIndex].FindControl("txtgrid")).Disabled = false;
            ((ImageButton)grid_pgmname.Rows[e.RowIndex].FindControl("img_save")).Visible = true;
            ((ImageButton)grid_pgmname.Rows[e.RowIndex].FindControl("img_update")).Visible = false;
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    public int name_validate(string m_name)
    {
        prgmnameList = employee.fn_programname(employee);
        if (prgmnameList.Count > 0)
        {
            for (valid = 0; valid < prgmnameList.Count; valid++)
            {
                if (prgmnameList[valid].prgmname == m_name)//|| m_name == ""
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
            employee.prgmid = Convert.ToInt32(hpgmnameID.Value);
            employee.prgmname = pgmname.Value;
            employee.status = 'Y';


            if (employee.prgmname != "")
            {
                check = name_validate(employee.prgmname);
                if (check == 0)
                {
                    _connection = con.fn_Connection();

                    _connection.Open();
                    //cmd = new SqlCommand("select count(*) from hrmm_course", _connection);
                    //int cc = (int)cmd.ExecuteScalar();
                    //cc++;
                    cmd1 = new SqlCommand("select count(*) from paym_instpgmname", _connection);
                    int cc = (int)cmd1.ExecuteScalar();
                    cmd = new SqlCommand("insert into paym_instpgmname values('" + employee.CompanyId + "', '" + employee.prgmname + "','" + employee.status + "', '" + employee.BranchId + "')", _connection);
                    cmd.ExecuteNonQuery();
                    cmd1 = new SqlCommand("select count(*) from paym_instpgmname", _connection);
                    int aa = (int)cmd1.ExecuteScalar();
                    _connection.Close();
                    //_Value = employee.programnameUpdate(employee);

                    if (cc != aa)
                    {
                        Error.Text = "<font color=Blue>Added Successfully</font>";
                        pgmname.Value = "";
                    }
                    else
                    {
                        Error.Text = "<font color=Red>Error Occured</font>";
                    }

                    prgmnameList = employee.fn_programname1(employee.BranchId);

                    if (prgmnameList.Count > 0)
                    {
                        grid_pgmname.DataSource = prgmnameList;
                        grid_pgmname.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";

        }

    }
}
