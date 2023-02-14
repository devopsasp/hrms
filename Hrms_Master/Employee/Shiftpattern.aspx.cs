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
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    private SqlConnection _connection;
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    SqlDataReader rea;
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();

    Company company = new Company();

    Employee employee = new Employee();

    Collection<Company> BranchsList;
    Collection<Employee> ShiftList;
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
                    case "a":
                    ddl_load1();
                        break;

                    case "h": ddl_load();
                        access();
                       
                        break;

                    case "u": s_form = "4";
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
        myConnection.Open();
        
        myConnection.Close();

    }

    public void access()
    {
        //_connection = con.fn_Connection();
        //_connection.Open();
        //cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and section_view='No'", _connection);
        //SqlDataReader rdrview = cmd.ExecuteReader();
        //if (rdrview.Read())
        //{
        //    string accesserror = "Permission Restricted. Please Contact Administrator.";
        //    MessageBox.Show(accesserror);
        //    //Response.Write("<script>alert('Permission Restricted. Please Contact Administrator.')</script>");
        //    Response.Redirect("~/Company_Home.aspx");
        //}
        //rdrview.Close();
        //cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and section_edit='No'", _connection);
        //SqlDataReader rdredit = cmd.ExecuteReader();
        //if (rdredit.Read())
        //{
        //    for (int b = 0; b < grid_Shift.Rows.Count; b++)
        //    {
        //        ((ImageButton)grid_Shift.Rows[b].FindControl("img_update")).Visible = false;
        //    }
        //    ((System.Web.UI.Control)grid_Shift.HeaderRow.FindControl("lbledit")).Visible = false;
        //}
        //rdredit.Close();
        //cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and  section_delete='No'", _connection);
        //SqlDataReader rdrdel = cmd.ExecuteReader();
        //if (rdrdel.Read())
        //{
        //    // ((ImageButton)grid_Course.Rows[0].FindControl("img_update")).Visible = false;
        //    for (int a = 0; a < grid_Shift.Rows.Count; a++)
        //    {
        //        ((ImageButton)grid_Shift.Rows[a].FindControl("imgdel")).Visible = false;
        //    }
        //    ((System.Web.UI.Control)grid_Shift.HeaderRow.FindControl("lbldel")).Visible = false;
        //}
        //rdrdel.Close();

    }
    public void ddl_load1()
    {
        myConnection.Open();
        SqlCommand cmd_ddl = new SqlCommand();

        if (s_login_role == "a")
        {
            cmd_ddl = new SqlCommand("select v_categoryname, pn_CategoryID from paym_category where pn_companyid='" + employee.CompanyId + "' and branchid='" + employee.BranchId + "' ", myConnection);
        }

        SqlDataAdapter ddl = new SqlDataAdapter(cmd_ddl);
        DataSet ds = new DataSet();
        ddl.Fill(ds, "paym_category");
        ddl_category.DataTextField = "v_categoryname";
        ddl_category.DataValueField = "pn_CategoryID";
        ddl_category.DataSource = ds;
        ddl_category.DataBind();
        ddl_category.Items.Insert(0, "Select");
        ddl_category.Items.Insert(1, "All");
        myConnection.Close();
    }


    public void ddl_load()
    {
        myConnection.Open();
        SqlCommand cmd_ddl = new SqlCommand();
       
        if (s_login_role == "h")
        {
            cmd_ddl = new SqlCommand("select v_categoryname, pn_CategoryID from paym_category where pn_companyid='" + employee.CompanyId + "' and branchid='" + employee.BranchId + "' ", myConnection);
        }

        SqlDataAdapter ddl = new SqlDataAdapter(cmd_ddl);
        DataSet ds = new DataSet();
        ddl.Fill(ds, "paym_category");
        ddl_category.DataTextField = "v_categoryname";
        ddl_category.DataValueField = "pn_CategoryID";
        ddl_category.DataSource = ds;
        ddl_category.DataBind();
        ddl_category.Items.Insert(0, "Select");
        ddl_category.Items.Insert(1, "All");
        myConnection.Close();
    }

    protected void ddl_category_SelectedIndexChanged(object sender, EventArgs e)
    {
        myConnection.Open();
        SqlCommand  ad = new SqlCommand();
        
        if (s_login_role == "h")
        {
            ad = new SqlCommand("select * from paym_attbonus where pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + employee.BranchId + "' and categoryName = '" + ddl_category.SelectedItem.Text + "'", myConnection);
        }
        SqlDataReader rea = ad.ExecuteReader();
        if (rea.Read())
        {
            txtFull.Text = rea["Fullatt"].ToString();
            txtHalf.Value = rea["Halfatt"].ToString();
            txtOne.Value = rea["Oneatt"].ToString();
        }
        else
        {
            txtFull.Text = "0.00";
            txtHalf.Value = "0.00";
            txtOne.Value = "0.00";
        }
        myConnection.Close();
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {   
        SqlCommand cmd = new SqlCommand();
        if (ddl_category.SelectedItem.Text == "All")
        {
            myConnection.Open();
            cmd = new SqlCommand("Delete from paym_attbonus where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID = '" + employee.BranchId + "'", myConnection);
            cmd.ExecuteNonQuery();
            foreach (ListItem li in ddl_category.Items)
            {
                if (li.Text != "Select" && li.Text != "All")
                {
                    cmd = new SqlCommand("insert into paym_attbonus values ('" + employee.CompanyId + "','" + employee.BranchId + "','" + li.Value + "','" + li.Text + "', '" + txtFull.Text + "','" + txtHalf.Value + "','" + txtOne.Value + "')", myConnection);
                    cmd.ExecuteNonQuery();
                }
            }
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Saved Successfully');", true);
            myConnection.Close();
        }
        else
        {
            myConnection.Open();
            cmd = new SqlCommand("Delete from paym_attbonus where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID = '" + employee.BranchId + "' and categoryID = '" + ddl_category.SelectedItem.Value + "'", myConnection);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("insert into paym_attbonus values ('" + employee.CompanyId + "','" + employee.BranchId + "','" + ddl_category.SelectedItem.Value + "','" + ddl_category.SelectedItem.Text + "', '" + txtFull.Text + "','" + txtHalf.Value + "','" + txtOne.Value + "')", myConnection);
            cmd.ExecuteNonQuery();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Saved Successfully');", true);
            myConnection.Close();
        }
        clear();
    }

    public void clear()
    {
        txtFull.Text = "0.00";
        txtHalf.Value = "0.00";
        txtOne.Value = "0.00";
        ddl_category.SelectedIndex = 0;
    }
}

