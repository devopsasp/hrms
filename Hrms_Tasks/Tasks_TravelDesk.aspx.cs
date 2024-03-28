using System;
using System.Data;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ePayHrms.Company;
using System.Collections.ObjectModel;
using ePayHrms.Employee;
using ePayHrms.BE.Recruitment;
using ePayHrms.Candidate;

public partial class Hrms_Tasks_Default : System.Web.UI.Page
{
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    //private SqlConnection _con;
    //ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    SqlDataReader rea;
    SqlDataReader rea1;
    SqlDataAdapter ada = new SqlDataAdapter();
    SqlDataAdapter ada1 = new SqlDataAdapter();
    Company company = new Company();
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    Candidate c = new Candidate();
    Collection<Employee> EmpFirstList;
    Collection<Employee> EmpProfileList;
    Collection<Candidate> WorkHistoryList;
    string eid, Dept, Desg, Ename;
    //Collection<Employee> AllowanceList;
    Collection<Company> CompanyList;


    string s_login_role;
    int ddl_i, grk;
    string _path, _Value;
    string s_form = "";
    DataSet ds_userrights;

    ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();

    string msg;

    protected void Page_Load(object sender, EventArgs e)
    {
        

        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);

        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        c.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        // Label8.Text = (string)Session["Login_Name"] + "!";
        //grd_view1.Visible = false;
        if (!IsPostBack)
        {
            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {

                switch (s_login_role)
                {

                    case "a":

                        break;

                    case "h":

                        break;

                    case "e": //Response.Redirect("../Hrms_Employee/Employee_Preview.aspx");
                        hr();
                        break;

                    case "u":
                        s_form = "46";

                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            hr();

                        }
                        else
                        {
                            Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator.";
                            Response.Redirect("~/Company_Home.aspx");
                        }
                        break;
                    default:
                        Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                        Response.Redirect("../Hrms_Master/Common/Common_Home.aspx");
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
        EmpProfileList = employee.fn_get_Emp_Profile1(employee);

        if (EmpProfileList.Count > 0)
        {
            if (EmpProfileList[0].DepartmentId != 1)
            {
                Dept = employee.fn_GetDepartmentName(EmpProfileList[0].DepartmentId);
            }
        }

        else
        {
            lbl_Error.Text = "Error";
        }

        if (Dept == "Travel Desk")
        {
            SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM Travel_Request where pn_BranchId='" + employee.BranchId + "'", myConnection);

            DataSet ds = new DataSet();

            ad.Fill(ds, "Travel_Request");


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
        }

        else
        {
            Response.Redirect("~/Company_Home.aspx");
        }

    }

}
