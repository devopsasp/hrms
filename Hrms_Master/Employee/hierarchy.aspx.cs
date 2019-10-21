using System;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using ePayHrms.Candidate;
using System.Configuration;
using System.Collections;
using ePayHrms.Company;
using System.Collections.ObjectModel;
using ePayHrms.BE.Recruitment;
using ePayHrms.Employee;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class Hrms_Master_Employee_hierarchy : System.Web.UI.Page
{


    Company company = new Company();
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    Candidate c = new Candidate();

    //Collection<Employee> AllowanceList;
    Collection<Company> CompanyList;
    Collection<Employee> EmployeeList;


    string s_login_role;
    int ddl_i, grk;
    string _path;
    string s_form = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        int BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        c.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        s_login_role = Request.Cookies["Login_temp_Role"].Value;

        if (s_login_role == "e")
        {
            employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
            pay.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
            c.EmployeeID = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);

            Session["emp_menu"] = 0;
            Response.Cookies["Select_Employee"].Value = "1";
            Response.Redirect("../Hrms_Employee/Employee_Preview.aspx");
        }
        row_branch.Visible = false;
        row_branch1.Visible = false;
        if (!IsPostBack)
        {
            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {

                Session["ErrorMsg"] = "";

                switch (s_login_role)
                {
                    case "a":
                        admin();
                        row_branch.Visible = true;
                        row_branch1.Visible = true;
                        break;

                    case "h":   
                        ddl_Department_load();
                        break;

                    case "e":
                        Response.Cookies["Msg_Session"].Value = "Permission Restricted. Please Contact Administrator";
                        Response.Redirect("Company_Home.aspx");
                        break;

                    //case "u":
                    //    hr();
                    //    break;

                    case "u":
                        s_form = "23";
                        ddl_Department_load();


                        break;
                    default:
                        Response.Cookies["Msg_Session"].Value = "Permission Restricted. Please Contact Administrator";
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
    [WebMethod]
    public static ArrayList Emp_select(int did,int Branch)
    {
      
          ArrayList objs = new ArrayList();
        string ConnectionString = ConfigurationManager.ConnectionStrings["Connectionstring"].ConnectionString;
        SqlConnection con;
        con = new SqlConnection(ConnectionString);
        SqlCommand cmd = new SqlCommand("paym_hierarchy_select", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@BranchID", Branch);
        cmd.Parameters.AddWithValue("@DepartmentID", did);
        con.Open();
        SqlDataReader dr = null;
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            objs.Add(new
            {
                Id = dr["Emp_id"],
                name = dr["Emp_name"],
                desi = dr["Emp_designation"],
                img = dr["emp_image"],
                parent = dr["emp_parent"],
                phone = dr["phone"],
                email = dr["email"]
            });
        }

        //_ = JsonConvert.SerializeObject(objs);
        return objs;
        con.Close();
    }

    [WebMethod]
    public static string Emp_update(string id, string toParent)
    {

        string ConnectionString = ConfigurationManager.ConnectionStrings["Connectionstring"].ConnectionString;
        SqlConnection con;
        con = new SqlConnection(ConnectionString);
        SqlCommand cmd = new SqlCommand("paym_hierarchy_update", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@parent", toParent);
        con.Open();

        cmd.ExecuteNonQuery();
        con.Close();
        return null;
    }
    public void ddl_Department_load()
    {
        EmployeeList = employee.fn_getDepartmentList1(employee.BranchId);
        if (EmployeeList.Count > 0)
        {

            for (int ddl_i = -2; ddl_i < EmployeeList.Count; ddl_i++)
            {

                if (ddl_i == -2)
                {
                    ListItem es_list = new ListItem();

                    es_list.Text = "Select Department";
                    es_list.Value = "0";
                    ddl_department.Items.Add(es_list);
                }
                else if (ddl_i == -1)
                {
                  
                }
                else
                {
                    ListItem es_list = new ListItem();
                    es_list.Value = EmployeeList[ddl_i].DepartmentId.ToString();
                    es_list.Text = EmployeeList[ddl_i].DepartmentName.ToString();
                    ddl_department.Items.Add(es_list);
                }
            }
        }
    }

    public void admin()
    {
        try
        {
            Collection<Company> ddlBranchsList;


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


            //ddlBranchsList = company.fn_getBranchs();

            //ddl_Branch.DataSource = ddlBranchsList;
            //ddl_Branch.DataTextField = "CompanyName";
            //ddl_Branch.DataValueField = "CompanyId";
            //ddl_Branch.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured.');", true);
        }
    }


    protected void ddl_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    
}