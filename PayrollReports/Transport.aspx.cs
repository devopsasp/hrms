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
public partial class PayrollReports_Transport : System.Web.UI.Page
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
            //grid_load();
           // grid_load1();
           // Grid_incharge.Visible = false;
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
            ddl_vehicles.Items.Clear();

            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {

                vehicleList1 = employee.fn_get_Vehicles(employee);
                if (vehicleList1.Count > 0)
                {


                    for (ddl_i = -1; ddl_i < vehicleList1.Count; ddl_i++)
                    {

                        if (ddl_i == -1)
                        {
                            ListItem list = new ListItem();

                            list.Text = "Select Vehicle Number";
                            list.Value = "sv";
                            ddl.Items.Add(list);
                        }
                        else
                        {

                            ListItem list = new ListItem();
                            list.Text = vehicleList1[ddl_i].Veh_id.ToString();
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
    public void load()
    {
        ddl_vehicles.Visible = false;
        vehicle_load(ddl_vehicles);
    }
    protected void ddl_options_SelectedIndexChanged(object sender, EventArgs e)
    {
        string option;
       option= ddl_options.SelectedItem.Text;
       if (option == "Report of Employees")
       {
           ddl_vehicles.Visible = true;
       }
       else
       {
           ddl_vehicles.Visible = false;
       }
    }
    protected void btn_transfer_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ddl_options.SelectedItem.Text == "Report of Vehicles")
            {
                employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                mycon.Open();
                SqlCommand cmd = new SqlCommand("Delete from temp_vehicles_report", mycon);
                cmd.ExecuteNonQuery();
                SqlCommand cmd1 = new SqlCommand("select a.*,b.employee_First_name from paym_bus_Details a,paym_employee  b where a.pn_companyID='" + employee.CompanyId + "' and a.pn_branchID='" + employee.BranchId + "' and b.pn_employeeID=a.driver_id", mycon);
                SqlDataReader drd = cmd1.ExecuteReader();
                while (drd.Read())
                {
                    employee.Veh_id = drd["vehicle_id"].ToString();
                    employee.Veh_number = drd["vehicle_number"].ToString();
                    employee.Area_name = drd["Destination"].ToString();
                    employee.FirstName = drd["employee_First_name"].ToString();
                    employee.Incharge_name = drd["Incharge"].ToString();
                    SqlCommand cmd2 = new SqlCommand("Insert Into temp_vehicles_report values('" + employee.CompanyId + "','" + employee.BranchId + "','" + employee.Veh_id + "','" + employee.Veh_number + "','" + employee.FirstName + "','" + employee.Area_name + "','" + employee.Incharge_name + "')", mycon);
                    cmd2.ExecuteNonQuery();
                }

                Session["ReportName"] = "~/crystalreports/TransportDetails.rpt";
                Session["preview_page"] = "~/Transport.aspx";
                Response.Redirect("~/PayrollReports/Report_view.aspx", false);
            }
            else if (ddl_options.SelectedItem.Text == "Report of Employees")
            {
                employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                mycon.Open();
                employee.Veh_id = ddl_vehicles.SelectedItem.Text;
                SqlCommand cmd = new SqlCommand("delete from temp_bus_employees", mycon);
                cmd.ExecuteNonQuery();
                SqlCommand cmd1=new SqlCommand("select a.*,b.employee_first_name,c.Incharge from paym_bus_members a,paym_employee b,paym_bus_Details c where a.pn_companyID='"+employee.CompanyId+"' and a.pn_branchID='"+employee.BranchId+"' and a.pn_employee_ID=b.pn_employeeid and c.vehicle_id='"+employee.Veh_id+"'",mycon);
                SqlDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    employee.Veh_id = dr["bus_number"].ToString();
                    employee.Veh_number = dr["vehicle_no"].ToString();
                    employee.Area_name = dr["boarding_area"].ToString();
                    employee.Boarding_point = dr["boarding_point"].ToString();
                    employee.FirstName = dr["employee_first_name"].ToString();
                    employee.Driver_id = dr["Driver_id"].ToString();
                    employee.Incharge_name = dr["Incharge"].ToString();
                    SqlCommand cmd2 = new SqlCommand("select employee_first_name from paym_employee where pn_employeeID='" + employee.Driver_id + "'", mycon);
                    SqlDataReader drd = cmd2.ExecuteReader();
                    if (drd.Read())
                    {
                        employee.Driver_name = drd["employee_first_name"].ToString();
                    }
                    drd.Close();
                    SqlCommand cmd3 = new SqlCommand("Insert into temp_bus_employees values('" + employee.CompanyId + "','" + employee.BranchId + "','" + employee.Veh_id + "','" + employee.Veh_number + "','" + employee.Driver_name + "','" + employee.Area_name + "','" + employee.FirstName + "','"+employee.Boarding_point+"','" + employee.Incharge_name + "')", mycon);
                    cmd3.ExecuteNonQuery();
                }
                Session["ReportName"] = "~/crystalreports/transport_employees.rpt";
                Session["preview_page"] = "~/Transport.aspx";
                Response.Redirect("~/PayrollReports/Report_view.aspx", false);
                
            }
        }
        catch (Exception ex)
        {
        }


    }
}