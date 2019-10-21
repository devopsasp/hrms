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

public partial class Hrms_Master_Transportation_Bus_area : System.Web.UI.Page
{
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    private SqlConnection _connection;
    SqlConnection mycon = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    Company company = new Company();
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    SqlDataReader rea;
    Employee employee = new Employee();
    Collection<Company> BranchsList;
    Collection<Employee> BoardingList;
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
                case "a": 
                    break;

                case "h":
                    //branch_header.Visible = false;
                    grid_load();
                    break;

                case "u": s_form = "8";
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
    protected void Img_btn_add_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (check == 0)
            {

                employee.Area_id = 0;
                employee.Area_name = Boarding_area.Value;
                employee.status = 'Y';

                _Value = employee.Area_Update(employee);
                if (_Value != "1")
                {
                    Error.Text = "<font color=Blue>Added Successfully</font>";
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
            Boarding_area.Value = "";
            grid_load();
           

        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }

    }
    public void grid_load()
    {
       
        BoardingList=employee.fn_boarding();
        if (BoardingList.Count > 0)
        {
            grid_Boarding_point.DataSource = BoardingList;
            grid_Boarding_point.DataBind();

        }
        else
        {
            grid_Boarding_point.DataSource = BoardingList;
            grid_Boarding_point.DataBind();
        }

    }
    protected void grid_Boarding_point_RowCommand(object sender, GridViewCommandEventArgs e)
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
                HtmlInputText lnkbtn1 = (HtmlInputText)gvrow.FindControl("txt_id");
                HtmlInputText lnkbtn = (HtmlInputText)gvrow.FindControl("txt_area");
                string str = lnkbtn1.Value;
                mycon.Open();
                SqlCommand cmd = new SqlCommand("delete from paym_Bus_area  where Area_id ='" + str + "'", mycon);
                cmd.ExecuteNonQuery();
                lbl_Error.Text = "Boarding point Deleted Successfully";
            }
            catch (Exception exc)
            {
                lbl_Error.Text = "Couldnt delete because the designation is assigned to employee";
                lbl_Error.Visible = true;
            }
            grid_load();
        }
        mycon.Close();
    }
    protected void Update(object sender, GridViewUpdateEventArgs e)
    {
        try
        {

            ((HtmlInputText)grid_Boarding_point.Rows[e.RowIndex].FindControl("txt_area")).Disabled = false;
            ((HtmlInputText)grid_Boarding_point.Rows[e.RowIndex].FindControl("txt_area")).Focus();
            ((ImageButton)grid_Boarding_point.Rows[e.RowIndex].FindControl("img_save")).Visible = true;
            ((ImageButton)grid_Boarding_point.Rows[e.RowIndex].FindControl("img_update")).Visible = false;
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";


        }
    }
    protected void Edit(object sender, GridViewEditEventArgs e)
    {
        try
        {
            employee.Area_id =Convert.ToInt32(((HtmlInputText)grid_Boarding_point.Rows[e.NewEditIndex].FindControl("txt_id")).Value);
            employee.Area_name = ((HtmlInputText)grid_Boarding_point.Rows[e.NewEditIndex].FindControl("txt_area")).Value;
            //employee.status = 'Y';
            if (employee.Area_name != "")
            {
                check = 0;
                //check = name_validate(employee.DesignationName);

                if (check == 0)
                {
                    employee.Area_Update(employee);
                    ((ImageButton)grid_Boarding_point.Rows[e.NewEditIndex].FindControl("img_update")).Visible = true;
                    ((ImageButton)grid_Boarding_point.Rows[e.NewEditIndex].FindControl("img_save")).Visible = false;
                    ((HtmlInputText)grid_Boarding_point.Rows[e.NewEditIndex].FindControl("txt_area")).Disabled = true;
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
            grid_load();

        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";


        }


    }
    protected void grid_Boarding_point_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}