using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using ePayHrms.Login;
using ePayHrms.Connection;
using ePayHrms.Candidate;
using System.IO;
using ePayHrms.Company;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ePayHrms.BE.Recruitment;
using ePayHrms.Employee;
using ePayHrms.Leave;
public partial class Hrms_Operations_Transport_Allocation : System.Web.UI.Page
{
    private SqlConnection _Connection;
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    private SqlConnection _connection;
    SqlConnection mycon = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    Company company = new Company();
    Employee employee = new Employee();
    Collection<Company> CompanyList;
    Collection<Employee> vehicleList1, ddl_DestinationList, ddl_driversList, RouteList, ddl_DestinationList1;
    //Collection<Company> ddlBranchsList;
    string s_login_role;
    int ddl_i, cs_k = 0, i;
    string s_form = "", _Value, _Value1;
    DataSet ds_userrights;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            s_login_role = Request.Cookies["Login_temp_Role"].Value;
            employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            CompanyList = company.fn_getCompany();
            load();
            grid_load();
            grid_load1();
            Grid_incharge.Visible = false;
            if (CompanyList.Count > 0)
            {

                if (s_login_role == "a" || s_login_role == "h")
                {
                    //invisible();
                    //Branch_Load(ddl_frombranch);
                    
                }
                else
                {
                    Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                    Response.Redirect("~/Company_Home.aspx");
                }

            }
            else
            {
                Response.Cookies["Msg_Session"].Value = "Create Company";
                Response.Redirect("Company_Home.aspx");
            }
        }

    }
    public void vehicle_load(DropDownList ddl)
    {
        try
        {
            ddl_vehicle.Items.Clear();

            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {

                vehicleList1 = employee.fn_Vehicles1(employee);
                if (vehicleList1.Count > 0)
                {


                    for (ddl_i = -1; ddl_i <vehicleList1.Count; ddl_i++)
                    {

                        if (ddl_i == -1)
                        {
                            ListItem list = new ListItem();

                            list.Text = "Select Vehicle";
                            list.Value = "sv";
                            ddl.Items.Add(list);
                        }
                        else
                        {

                            ListItem list = new ListItem();

                            list.Text = vehicleList1[ddl_i].Vehicle.ToString();
                            list.Value = vehicleList1[ddl_i].Veh_id.ToString();
                            ddl.Items.Add(list);

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
    public void places_load(DropDownList ddl)
    {
        try
        {
            ddl_Destination.Items.Clear();
            CompanyList = company.fn_getCompany();
            if (CompanyList.Count > 0)
            {
                ddl_DestinationList = employee.fn_get_destination(employee);
            }
            if (ddl_DestinationList.Count > 0)
            {
                for (ddl_i = -1; ddl_i < ddl_DestinationList.Count; ddl_i++)
                {
                    if (ddl_i == -1)
                    {
                        ListItem list = new ListItem();
                        list.Text = "select Destination";
                        list.Value = "sd";
                        ddl.Items.Add(list);
                    }
                    else
                    {
                        ListItem list = new ListItem();
                        list.Text = ddl_DestinationList[ddl_i].Area_name;
                        list.Value = ddl_DestinationList[ddl_i].Area_id.ToString();
                        ddl.Items.Add(list);
                    }
                }

            }
        }
        catch (Exception ex)
        {

        }
    }
    public void drivers_load(DropDownList ddl)
    {
        ddl_Driver.Items.Clear();
        CompanyList = company.fn_getCompany();
        if (CompanyList.Count > 0)
        {
            ddl_driversList = employee.fn_get_drivers(employee);
        }
        if (ddl_driversList.Count > 0)
        {
            for (ddl_i = -1; ddl_i < ddl_driversList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem list = new ListItem();
                    list.Text = "Select Drivers";
                    list.Value = "SD";
                    ddl.Items.Add(list);
                }
                else
                {
                    ListItem list = new ListItem();
                    list.Text = ddl_driversList[ddl_i].Driver_name;
                    list.Value = ddl_driversList[ddl_i].EmployeeId.ToString();
                    ddl.Items.Add(list);
                }
            }
        }
    }
    protected void btn_transfer_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            mycon.Open();
            employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            employee.Veh_id = ddl_vehicle.SelectedItem.Value;
            employee.Vehicle = ddl_vehicle.SelectedItem.Text;
            employee.Area_name = ddl_Destination.SelectedItem.Text;
            employee.EmployeeId =Convert.ToInt32(ddl_Driver.SelectedItem.Value);
            employee.Routes = txt_route.Value;
              _Value = employee.Route_details(employee);
                if (_Value != "1")
                {
                    Error.Text = "<font color=Blue>Added Successfully</font>";
                }
                else
                {
                    Error.Text = "<font color=Red>Error Occured</font>";
                }

          //  Boarding_area.Value = "";
            grid_load();
                mycon.Close();
                clear();
                load();

        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }
    public void clear()
    {
        ddl_vehicle.SelectedItem.Value = "sv";
        ddl_Driver.SelectedItem.Value = "SD";
        ddl_Destination.SelectedItem.Value = "sd";
        txt_route.Value = "";
    }
    public void load()
    {
        ddl_Destination.Items.Clear();
        ddl_Driver.Items.Clear();
        ddl_vehicle.Items.Clear();
        places_load(ddl_Destination);
        drivers_load(ddl_Driver);
        vehicle_load(ddl_vehicle);
    }
    public void grid_load()
    {
        try
        {
            RouteList = employee.fn_route(employee);

            if (RouteList.Count > 0)
            {
             Grid_bus_allocation.DataSource = RouteList;
                Grid_bus_allocation.DataBind();
            }
            else
            {
                Grid_bus_allocation.DataSource = RouteList;
                Grid_bus_allocation.DataBind();
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }
    public void grid_load1()
    {
        try
        {
            RouteList = employee.fn_route1(employee);

            if (RouteList.Count > 0)
            {
                Grid_incharge.DataSource = RouteList;
                Grid_incharge.DataBind();
            }
            else
            {
                Grid_incharge.DataSource = RouteList;
                Grid_incharge.DataBind();
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }
    protected void Grid_bus_allocation_RowEditing(object sender, GridViewEditEventArgs e)
    {
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        Grid_bus_allocation.EditIndex = e.NewEditIndex;
        employee.Vehicle = ((Label)Grid_bus_allocation.Rows[e.NewEditIndex].FindControl("lbl_veh_no")).Text;
        employee.Area_name = ((Label)Grid_bus_allocation.Rows[e.NewEditIndex].FindControl("lbl_destination")).Text;
        employee.Driver_name = ((Label)Grid_bus_allocation.Rows[e.NewEditIndex].FindControl("lbl_driver")).Text;
        
        grid_load();
        DropDownList ddl_edit = (DropDownList)Grid_bus_allocation.Rows[e.NewEditIndex].FindControl("ddl_edit_destination");
        ddl_edit.Items.Clear();
        CompanyList = company.fn_getCompany();
        if (CompanyList.Count > 0)
        {
            ddl_DestinationList = employee.fn_get_destination(employee);
        }
        if (ddl_DestinationList.Count > 0)
        {
            for (ddl_i = -1; ddl_i < ddl_DestinationList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem list = new ListItem();
                    list.Text = "select Destination";
                    list.Value = "sd";
                    ddl_edit.Items.Add(list);
                }
                else
                {
                    ListItem list = new ListItem();
                    list.Value = ddl_DestinationList[ddl_i].Area_id.ToString();
                    list.Text = ddl_DestinationList[ddl_i].Area_name ;
                    ddl_edit.Items.Add(list);
                }
            }

        }
        DropDownList ddl_edit1 = (DropDownList)Grid_bus_allocation.Rows[e.NewEditIndex].FindControl("ddl_edit_drivers");
        ddl_edit1.Items.Clear();
        CompanyList = company.fn_getCompany();
        if (CompanyList.Count > 0)
        {
            ddl_driversList = employee.fn_get_drivers(employee);
        }
        if (ddl_driversList.Count > 0)
        {
            for (ddl_i = -1; ddl_i < ddl_driversList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem list = new ListItem();
                    list.Text = "Select Drivers";
                    list.Value = "SD";
                    ddl_edit1.Items.Add(list);
                }
                else
                {
                    ListItem list = new ListItem();
                    list.Text = ddl_driversList[ddl_i].Driver_name;
                    list.Value = ddl_driversList[ddl_i].EmployeeId.ToString();
                    ddl_edit1.Items.Add(list);
                }
            }
        }
        DropDownList ddl_edit2 = (DropDownList)Grid_bus_allocation.Rows[e.NewEditIndex].FindControl("ddl_edit_veh_no");
        ddl_edit2.Items.Clear();
        CompanyList = company.fn_getCompany();
        if (CompanyList.Count > 0)
        {
            vehicleList1 = employee.fn_Vehicles1(employee);
        }
            if (vehicleList1.Count > 0)
            {


                for (ddl_i = -1; ddl_i < vehicleList1.Count; ddl_i++)
                {

                    if (ddl_i == -1)
                    {
                        ListItem list = new ListItem();

                        list.Text = "Select Vehicle";
                        list.Value = "sv";
                        ddl_edit2.Items.Add(list);
                    }
                    else
                    {

                        ListItem list = new ListItem();

                        list.Text = vehicleList1[ddl_i].Vehicle.ToString();
                        list.Value = vehicleList1[ddl_i].Veh_id.ToString();
                        ddl_edit2.Items.Add(list);

                    }

                }

            }
        ////GridViewRow Gvrow = Grid_bus_allocation.Rows[e.NewEditIndex];
        //DropDownList ddl_edit_destination = (DropDownList)Grid_bus_allocation.Rows[e.NewEditIndex].FindControl("ddl_edit_destination");
        //edit_ddl_area_load(ddl_edit_destination);
            ddl_edit.SelectedItem.Text = employee.Area_name;
            ddl_edit1.SelectedItem.Text = employee.Driver_name;
            ddl_edit2.SelectedItem.Text = employee.Vehicle;
    }
    protected void Grid_bus_allocation_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        Grid_bus_allocation.EditIndex = -1;
        grid_load();
    }
//    public void edit_ddl_area_load(DropDownList ddl)
//    {
//        try
//        {
//            CompanyList = company.fn_getCompany();
//            if (CompanyList.Count > 0)
//            {
//                ddl_DestinationList1 = employee.fn_get_destination(employee);
//            }
//            if (ddl_DestinationList1.Count > 0)
//            {
//                for (ddl_i = -1; ddl_i < ddl_DestinationList1.Count; ddl_i++)
//                {
//                    if (ddl_i == -1)
//                    {
//                        ListItem list = new ListItem();
//                        list.Text = "select Destination";
//                        list.Value = "sd";
//                        ddl.Items.Add(list);
//                    }
//                    else
//                    {
//                        ListItem list = new ListItem();
//                        list.Text = ddl_DestinationList1[ddl_i].Area_name;
//                        list.Value = ddl_DestinationList1[ddl_i].Area_id.ToString();
//                        ddl.Items.Add(list);
//                    }
//                }

//            }
//        }
//        catch (Exception ex)
//        {

//        }
//    }
    protected void Grid_bus_allocation_RowUpdating1(object sender, GridViewUpdateEventArgs e)
    {

        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        GridViewRow Gvrow = Grid_bus_allocation.Rows[e.RowIndex];
        if (Gvrow != null)
        {
            employee.Route_ID = Convert.ToInt32(((Label)Gvrow.FindControl("lbl_route_id")).Text);
            employee.Veh_id = ((Label)Gvrow.FindControl("lbl_veh_id")).Text;
            employee.Vehicle = ((DropDownList)Gvrow.FindControl("ddl_edit_veh_no")).SelectedItem.Text;
            employee.Area_name = ((DropDownList)Gvrow.FindControl("ddl_edit_destination")).SelectedItem.Text;
            employee.EmployeeId =Convert.ToInt32(((DropDownList)Gvrow.FindControl("ddl_edit_drivers")).SelectedItem.Value);
            employee.Routes = ((TextBox)Gvrow.FindControl("txt_route")).Text;
            //string str = ((Label)Gvrow.FindControl("")).Text;
            //string[] str1 = str.Split('-');
            //employee.EmployeeId = str1[0].ToString();
            //  employee.Status21 = ((DropDownList)Gvrow.FindControl("ddl_status")).SelectedItem.Text;
            _Value = employee.Route_details(employee);
                if (_Value != "1")
                {
                    Error.Text = "<font color=Blue>Added Successfully</font>";
                }
                else
                {
                    Error.Text = "<font color=Red>Error Occured</font>";
                }

          //  Boarding_area.Value = "";
                Grid_bus_allocation.EditIndex = -1;
            grid_load();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
        }
        catch (Exception ex) { }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            Grid_incharge.EditIndex = e.NewEditIndex;

            string lbl = ((Label)Grid_incharge.Rows[e.NewEditIndex].FindControl("lbl_veh_id")).Text;
            grid_load1();
            DropDownList drp = (DropDownList)Grid_incharge.Rows[e.NewEditIndex].FindControl("DropDownList1");

            drp.Items.Clear();
          
           ddl_DestinationList1 = employee.fn_getemployee(employee);
            
            if (ddl_DestinationList1.Count > 0)
            {
                for (ddl_i = -1; ddl_i < ddl_DestinationList1.Count; ddl_i++)
                {
                    if (ddl_i == -1)
                    {
                        ListItem list = new ListItem();
                        list.Text = "select Employee";
                        list.Value = "sd";
                        drp.Items.Add(list);
                    }
                    else
                    {
                        ListItem list = new ListItem();
                        list.Value = ddl_DestinationList1[ddl_i].EmployeeId.ToString();
                        list.Text = ddl_DestinationList1[ddl_i].LastName.ToString();
                        drp.Items.Add(list);
                    }
                }

            }
        }
        catch (Exception ex) { }
    }
    protected void lnk_incharge_Click(object sender, EventArgs e)
    {
        Grid_incharge.Visible = true;
    }

    protected void Grid_incharge_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        mycon.Open();
        GridViewRow Gvrow=Grid_incharge.Rows[e.RowIndex];
        DropDownList inchargeList=(DropDownList)Gvrow.FindControl("DropDownList1");
        Label veh_id=(Label)Gvrow.FindControl("lbl_veh_id");
        int Vehicle_id=Convert.ToInt32(veh_id.Text);
        string Incharge=inchargeList.SelectedItem.Text;
        SqlCommand cmd = new SqlCommand("update paym_bus_Details set incharge='" + Incharge + "' where vehicle_id='" + Vehicle_id + "'", mycon);
        cmd.ExecuteNonQuery();
        mycon.Close();
        Grid_incharge.EditIndex = -1;
        grid_load1();
    
        Grid_incharge.Visible = true;
    }
}