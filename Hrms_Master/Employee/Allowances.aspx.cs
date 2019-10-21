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


public partial class Hrms_Master_Default3 : System.Web.UI.Page
{
    
    private SqlConnection _connection;
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    SqlDataReader rea;

    Company company = new Company();

    Employee employee = new Employee();


    Collection<Company> BranchsList;
    Collection<Employee> GradeList;
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

        if (!IsPostBack)
        {
            switch (s_login_role)
            {
                case "a": load();
                    break;

                case "h": load1();
                   
                    
                    break;

                case "u": s_form = "12";
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
        var conString = ConfigurationManager.ConnectionStrings["connectionstring"];
        string constr = conString.ConnectionString;
        SqlConnection m_con = new SqlConnection(constr);
        m_con.Open();
        SqlCommand cmd = new SqlCommand("select * from allowances", m_con);
        SqlDataAdapter ada = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        ada.Fill(ds);
        lv_a_details.DataSource = ds;
        lv_a_details.DataBind();
        ada.Dispose();
        m_con.Close();
        

    }


    public void load1()
    {
             
        var conString = ConfigurationManager.ConnectionStrings["connectionstring"];
        string constr = conString.ConnectionString;
        SqlConnection m_con = new SqlConnection(constr);
        
        m_con.Open();
            SqlCommand cmd = new SqlCommand("select * from allowances", m_con);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            lv_a_details.DataSource = ds;
            
            lv_a_details.DataBind();
            ada.Dispose();
        m_con.Close();
        
        

    }


    protected void lv_a_details_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        
    }
    protected void lv_a_details_ItemInserting(object sender, ListViewInsertEventArgs e)
    {
        var conString = ConfigurationManager.ConnectionStrings["connectionstring"];
        string constr = conString.ConnectionString;

        SqlConnection m_con = new SqlConnection(constr);

        TextBox txgrade =(TextBox) e.Item.FindControl("txtgrade");
        TextBox txallowance = (TextBox)e.Item.FindControl("txtmaxallowance");
        //Label1.Text = txgrade.Text;
        m_con.Open();
        SqlCommand cmd = new SqlCommand("insert into allowances(grade,allowances,pn_companyid,pn_branchid) values('" + txgrade.Text + "','" + txallowance.Text + "','"+employee.CompanyId+"','"+employee.BranchId+"')", m_con);
        cmd.ExecuteNonQuery();
        m_con.Close();
        load1();
    }

    protected void lv_a_details_ItemEditing(object sender, ListViewEditEventArgs e)
    {
        lv_a_details.EditIndex = e.NewEditIndex;
    }

    protected void pager1_perrender(object sender, EventArgs e)
    {
        load1();
    }
    protected void lv_a_details_updating(object sender, ListViewUpdateEventArgs e)
    {
        var conString = ConfigurationManager.ConnectionStrings["connectionstring"];
        string constr = conString.ConnectionString;
        SqlConnection m_con = new SqlConnection(constr);
        m_con.Open();
        //id is at hidden row in edit item index
       
        TextBox txtgrade = (TextBox)lv_a_details.Items[e.ItemIndex].FindControl("txtegrade");
        TextBox txtallo = (TextBox)lv_a_details.Items[e.ItemIndex].FindControl("txteallowance");
        SqlCommand cmd = new SqlCommand("update allowances set grade='" + txtgrade.Text + "',allowances='" + txtallo.Text + "',pn_companyid='" + employee.CompanyId + "',pn_branchid='" + employee.BranchId + "' where id='" + ((Label)lv_a_details.Items[e.ItemIndex].FindControl("lblid")).Text + "'", m_con);
        cmd.ExecuteNonQuery();
        m_con.Close();
        lv_a_details.EditIndex = -1;
        load1();
    }
    protected void lv_a_deatis_cancelling(object sender, ListViewCancelEventArgs e)
    {
        lv_a_details.EditIndex = -1;
        load1();
    }
    protected void lv_a_details_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        var conString = ConfigurationManager.ConnectionStrings["connectionstring"];
        string constr = conString.ConnectionString;
        SqlConnection m_con = new SqlConnection(constr);
        m_con.Open();
        
        Label txttemp=(Label)lv_a_details.Items[e.ItemIndex].FindControl("lblid");
        //lblerror.Text = txttemp.Text;
        SqlCommand cmd = new SqlCommand("delete allowances where id='" + txttemp.Text + "'", m_con);
        cmd.ExecuteNonQuery();
        m_con.Close();

    }
}

