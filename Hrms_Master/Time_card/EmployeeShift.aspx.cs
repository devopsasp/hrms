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
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    SqlDataReader rea;
    Company company = new Company();
    Employee employee = new Employee();
    Collection<Employee> EmployeeList;

    string[] _Value , _Val;
    string s_login_role;
    string s_form = "";
    DataSet ds_userrights;
    DataTable _DayEventsTable;


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            s_login_role = Request.Cookies["Login_temp_Role"].Value;

            if (s_login_role == "e")
                this.Page.MasterPageFile = "~/EHRMS.master";
            else
                this.Page.MasterPageFile = "~/HRMS.master";

        }
        catch (Exception ex)
        {

        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);

        if (!IsPostBack)
        {

            switch (s_login_role)
            {
                case "a": load_admin();
                    break;

                case "e":
                case "r":
                    var myDate = DateTime.Now; 
                    var startOfMonth = new DateTime(myDate.Year, myDate.Month, 1);
                    Calendar1.TodaysDate = Convert.ToDateTime(startOfMonth);
                    employee.d_Date = DateTime.Now.ToString("MM") + "/" + myDate.Year;
                    access();
                    ddl_branch.Visible = false;
                    break;

                case "u": s_form = "5";
                    ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                    if (ds_userrights.Tables[0].Rows.Count > 0)
                    {

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



    public void load_admin()
    {
        myConnection.Open();
        SqlDataAdapter ad = new SqlDataAdapter("select * from paym_branch", myConnection);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        ddl_branch.DataTextField = "branchname";
        ddl_branch.DataValueField = "pn_branchid";
        ddl_branch.DataSource = ds;
        ddl_branch.DataBind();
        ddl_branch.Items.Insert(0, "select Branch");
        myConnection.Close();
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
            Response.Write("<script language='javascript'>alert('Permission Restricted. Please Contact Administrator.');window.location='~/Company_Home.aspx';</script>");
        }
        rdrview.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and section_edit='No'", _connection);
        SqlDataReader rdredit = cmd.ExecuteReader();
        if (rdredit.Read())
        {

        }
        rdredit.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and  section_delete='No'", _connection);
        SqlDataReader rdrdel = cmd.ExecuteReader();
        if (rdrdel.Read())
        {

        }
        rdrdel.Close();
    }

    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        //employee.EmployeeId = 
        _DayEventsTable = new DataTable();
        _DayEventsTable.Columns.Add("Date");
        _DayEventsTable.Columns.Add("Title");
        //string[] s = ddl_empcode.SelectedItem.Text.Split('-');

        myConnection.Open();
        cmd = new SqlCommand("Select a.*, b.pn_employeeID from shift_month a, paym_employee b where a.pn_companyID = b.pn_CompanyID and a.pn_BranchID = b.pn_BranchID and a.pn_EmployeeCode = b.EmployeeCode and a.pn_companyid = '" + employee.CompanyId + "' and a.pn_branchid = '" + employee.BranchId + "' and b.pn_employeeID='" + employee.EmployeeId + "' and monthyear='" + employee.d_Date + "' ", myConnection);
        rea = cmd.ExecuteReader();
        while (rea.Read())
        {
            _DayEventsTable.Rows.Add(Convert.ToString(rea[5]), Convert.ToString(rea[6]));
        } 

        foreach (DataRow Row in _DayEventsTable.Rows)
        {
            string Date = Row["Date"].ToString();
            string Title = Row["Title"].ToString();

            if (Date == e.Day.Date.ToString())
            {
                e.Cell.Controls.Add(new LiteralControl("</br>" + Title));
                if (Title == "W")
                {
                    e.Cell.BackColor = System.Drawing.Color.GreenYellow;
                }
                else
                {
                    e.Cell.BackColor = System.Drawing.Color.Aqua;
                }
            }
        }
        myConnection.Close();
    }


    protected void txt_monyear_TextChanged(object sender, EventArgs e)
    {
        try
        {
            employee.d_Date = txt_monyear.Text;
            Calendar1.TodaysDate = Convert.ToDateTime(txt_monyear.Text);
            //myConnection.Open();
            //cmd = new SqlCommand("Select pattern_code, slot, balance_days from shift_balance where pn_companyid = '" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "' and monthyear='" + txt_monyear.Text + "' ", myConnection);
            //rea = cmd.ExecuteReader();
            //if (rea.Read())
            //{
            //    ddl_patterncode.SelectedItem.Text = rea[0].ToString();
            //    txt_slot.Text = rea[1].ToString();
            //    txt_balance.Text = rea[2].ToString();
            //}
        }
        catch
        {
            //lbl_Error.Text = "Check the Month/Year you have entered";
        }
        finally
        {
           // myConnection.Close();
        }
    }

    protected void ddl_branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (s_login_role == "a")
        {
            employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
        }
        SqlDataSource1.SelectCommand = "select employee_first_name,employeecode from paym_employee where pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + employee.BranchId + "' ";
        
        //load();
    }


    public void delete_tables()
    {
        myConnection.Open();
        cmd = new SqlCommand("Delete from shift_balance where monthyear='" + txt_monyear.Text + "' and pn_branchID = '" + employee.BranchId + "';Delete from shift_month where monthyear='" + txt_monyear.Text + "' and pn_branchID = '" + employee.BranchId + "'", myConnection);
        cmd.ExecuteNonQuery();
        myConnection.Close();
    }

   
    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (txt_monyear.Text.Length >= 7)
        {
            employee.d_Date = txt_monyear.Text;
            string sample = "01/";
            string correct = sample + txt_monyear.Text;
            DateTime d = Convert.ToDateTime(correct);
            Calendar1.TodaysDate = d;
        }
    }


}
