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
public partial class Hrms_Master_Transportation_Vehicle_Details : System.Web.UI.Page
{
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    private SqlConnection _connection;
    SqlConnection mycon = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    Company company = new Company();
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    SqlDataReader rea;
    Employee employee = new Employee();
    Collection<Employee> VehiclesList;
    string _Value;
    string s_login_role;
    string s_form = "";
    DataSet ds_userrights;
    int check;
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
    protected void grid_Vehicles_Details_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Img_btn_add_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (check == 0)
            {

                employee.Veh_id = "";
                employee.Veh_number = veh_no.Value;
                employee.Veh_type = Veh_type.Value;
                employee.Veh_capacity = Convert.ToInt32(veh_capacity.Value);
                //employee.status = 'Y';

                _Value = employee.Vehicle_Details(employee);
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
            veh_no.Value = "";
            veh_capacity.Value = "";
            Veh_type.Value = "";
            grid_load();
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    public void grid_load()
    {

        VehiclesList = employee.fn_vehicles();
        if (VehiclesList.Count > 0)
        {
            grid_Vehicles_Details.DataSource = VehiclesList;
            grid_Vehicles_Details.DataBind();

        }
        else
        {
            grid_Vehicles_Details.DataSource = VehiclesList;
            grid_Vehicles_Details.DataBind();
        }

    }
    protected void Edit(object sender, GridViewEditEventArgs e)
    {
        try
        {
            employee.Veh_id = ((HtmlInputText)grid_Vehicles_Details.Rows[e.NewEditIndex].FindControl("txt_id")).Value;
            employee.Vehicle = ((HtmlInputText)grid_Vehicles_Details.Rows[e.NewEditIndex].FindControl("txt_type")).Value;
            string[] veh = employee.Vehicle.Split('-');
            employee.Veh_number = veh[0];
            employee.Veh_type = veh[2];
            employee.Veh_capacity = Convert.ToInt32(((HtmlInputText)grid_Vehicles_Details.Rows[e.NewEditIndex].FindControl("txt_capacity")).Value);
            //employee.status = 'Y';
            if (employee.Area_name != "")
            {
                check = 0;
                //check = name_validate(employee.DesignationName);

                if (check == 0)
                {
                    employee.Vehicle_Details(employee);
                    ((ImageButton)grid_Vehicles_Details.Rows[e.NewEditIndex].FindControl("img_update")).Visible = true;
                    ((ImageButton)grid_Vehicles_Details.Rows[e.NewEditIndex].FindControl("img_save")).Visible = false;
                    ((HtmlInputText)grid_Vehicles_Details.Rows[e.NewEditIndex].FindControl("txt_type")).Disabled = true;
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
            Error.Text = "Updated Succesfully";

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

            ((HtmlInputText)grid_Vehicles_Details.Rows[e.RowIndex].FindControl("txt_type")).Disabled = false;
            ((HtmlInputText)grid_Vehicles_Details.Rows[e.RowIndex].FindControl("txt_type")).Focus();
            ((ImageButton)grid_Vehicles_Details.Rows[e.RowIndex].FindControl("img_save")).Visible = true;
            ((ImageButton)grid_Vehicles_Details.Rows[e.RowIndex].FindControl("img_update")).Visible = false;
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";


        }
    }
    protected void grid_Vehicles_Details_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void grid_Vehicles_Details_RowCommand(object sender, GridViewCommandEventArgs e)
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
                HtmlInputText lnkbtn = (HtmlInputText)gvrow.FindControl("txt_type");
                string str = lnkbtn1.Value;
                mycon.Open();
                SqlCommand cmd = new SqlCommand("delete from paym_vehicle_details  where vehicle_id ='" + str + "' and pn_companyId='" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "'", mycon);
                cmd.ExecuteNonQuery();
               Error.Text = "Vehicle Deleted Successfully";
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
}