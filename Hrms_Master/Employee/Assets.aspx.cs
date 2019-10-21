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

public partial class Hrms_Master_Employee_Assets : System.Web.UI.Page
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
    Collection<Employee> AssetList;
    Collection<Company> CompanyList;
    int company_Id, branch_Id, valid, temp_valid = 0, check = 0;
    string _Value;
    string s_login_role;
    string s_form = "";
    DataSet ds_userrights;
    protected void Page_Load(object sender, EventArgs e)
    {

        
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        Error.Text = "";


        if (!IsPostBack)
        {

            switch (s_login_role)
            {
                case "a":
                    //load_admin();
                    break;

                case "h":
                    ddl_branch.Visible = false;
                    load1();
                    //access();
                    break;

                case "u": s_form = "3";
                    ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                    if (ds_userrights.Tables[0].Rows.Count > 0)
                    {
                        load1();
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
    public void load1()
    {
        AssetList = employee.fn_Asset(employee.BranchId);

        if (AssetList.Count > 0)
        {
            grid_assets.DataSource = AssetList;
            grid_assets.DataBind();
        }
        else
        {
            AssetList = employee.fn_EmptycourseList(employee);

            if (AssetList.Count > 0)
            {
                grid_assets.DataSource = AssetList;
                grid_assets.DataBind();

                ((ImageButton)grid_assets.Rows[0].FindControl("img_update")).Visible = true;
                ((ImageButton)grid_assets.Rows[0].FindControl("imgdel")).Visible = true;
            }

        }

    }
    protected void Edit(object sender, GridViewEditEventArgs e)
    {
        try
        {

            employee.AssetId = Convert.ToInt32(grid_assets.DataKeys[e.NewEditIndex].Value);
            employee.AssetName = ((HtmlInputText)grid_assets.Rows[e.NewEditIndex].FindControl("txtgrid")).Value;

            if (employee.AssetName != "")
            {
                check = 0;

                if (check == 0)
                {

                    _Value = employee.fn_Update_Asset(employee);
                    if (_Value == "0")
                    {
                        ((ImageButton)grid_assets.Rows[e.NewEditIndex].FindControl("img_update")).Visible = true;
                        ((ImageButton)grid_assets.Rows[e.NewEditIndex].FindControl("img_save")).Visible = false;
                        ((HtmlInputText)grid_assets.Rows[e.NewEditIndex].FindControl("txtgrid")).Disabled = true;
                        Error.Text = "<font color=Blue>Updated Successfully</font>";
                    }
                    else
                    {
                        Error.Text = "<font color=Red>Error Occured</font>";
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
            HtmlInputText textgrid = ((HtmlInputText)grid_assets.Rows[e.RowIndex].FindControl("txtgrid"));
            ((ImageButton)grid_assets.Rows[e.RowIndex].FindControl("img_save")).Visible = true;
            ((ImageButton)grid_assets.Rows[e.RowIndex].FindControl("img_update")).Visible = false;
            textgrid.Disabled = false;
            textgrid.Attributes.Add("style", "font-color:blue");
            textgrid.Attributes.Add("style", "width:500px");//textbox width

        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }

    }

    protected void btn_save_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            employee.AssetId = Convert.ToInt32(hAssetID.Value);
            employee.AssetName = txt_assets.Text;
            _connection = con.fn_Connection();
            _connection.Open();
            cmd1 = new SqlCommand("select count(*) from Assets", _connection);
            int cc = (int)cmd1.ExecuteScalar();
            cmd = new SqlCommand("insert into Assets values('" + employee.CompanyId + "', '" + employee.BranchId + "', '" + employee.AssetName + "')", _connection);
            cmd.ExecuteNonQuery();
            cmd1 = new SqlCommand("select count(*) from Assets", _connection);
            int aa = (int)cmd1.ExecuteScalar();
            _connection.Close();
            if (cc != aa)
            {
                Error.Text = "<font color=Blue>Added Successfully</font>";
                txt_assets.Text = "";
            }
            else
            {
                Error.Text = "<font color=Red>Error Occured</font>";
            }

            AssetList = employee.fn_Asset(employee.BranchId);

            if (AssetList.Count > 0)
            {
                grid_assets.DataSource = AssetList;
                grid_assets.DataBind();
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }
    protected void grid_assets_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            try
            {
                GridViewRow gvrow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                int rowindex = gvrow.RowIndex;
                HtmlInputText lnkbtn = (HtmlInputText)gvrow.FindControl("txtgrid");
                string str = lnkbtn.Value;
                mycon.Open();
                SqlCommand cmd = new SqlCommand("delete from Assets where Asset_name='" + str + "' and branchid='" + employee.BranchId + "'", mycon);
                cmd.ExecuteNonQuery();
                load1();
                lbl_Error.Text = "";


            }
            catch (Exception exc)
            {

            }
        }
        mycon.Close();
    }
    protected void grid_assets_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}
