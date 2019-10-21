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
using ePayHrms.Connection;
using ePayHrms.Company;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ePayHrms.Employee;
using ePayHrms.BE.Recruitment;
using ePayHrms.Login;
using ePayHrms.Connection;
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
    string eid , Dept , Desg , Ename;
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


    public void admin()
    {

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

        if (Dept == "Travel Desk")
        {
            Response.Redirect("../Hrms_Tasks/Tasks_TravelDesk.aspx");
        }

        else
        {

            SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM Travel_Request where pn_BranchId='" + employee.BranchId + "' and pn_EmployeeID='" + employee.EmployeeId + "'", myConnection);

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
        
    }

    private void DeleteRecord(string ID)
    {
        string sqlStatement = "DELETE FROM Travel_Request WHERE pn_TravelID = @pn_TravelID";
        try
        {
            myConnection.Open();
            SqlCommand cmd = new SqlCommand(sqlStatement, myConnection);
            cmd.Parameters.AddWithValue("@pn_TravelID", ID);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Deletion Error:";
            msg += ex.Message;
            throw new Exception(msg);
        }
        finally
        {
            myConnection.Close();
        }
    }

    public void clear()
    {
        txt_country.Value = "";
        txt_city.Value = "";
        txt_Ddate.Value = "";
        txt_Adate.Value = "";
        txt_project.Value = "";
        txt_other.Value = "";
       
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        hr();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string ID = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lbl_sno")).Text;

        DeleteRecord(ID);
        hr();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex; // turn to edit mode
        hr();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow Gvrow = GridView1.Rows[e.RowIndex];
        if (Gvrow != null)
        {
            string sno = ((Label)Gvrow.FindControl("Edit_lbl_sno")).Text;
            string coun = ((TextBox)Gvrow.FindControl("Edit_lbl_country")).Text;
            string city = ((TextBox)Gvrow.FindControl("Edit_lbl_city")).Text;
            string D_date = ((TextBox)Gvrow.FindControl("Edit_lbl_Ddate")).Text;
            string A_date = ((TextBox)Gvrow.FindControl("Edit_lbl_Adate")).Text;
            string pref = ((DropDownList)Gvrow.FindControl("Edit_lbl_pref")).Text;
            string project = ((TextBox)Gvrow.FindControl("Edit_lbl_project")).Text;
            string other = ((TextBox)Gvrow.FindControl("Edit_lbl_other")).Text;
            myConnection.Open();
            cmd = new SqlCommand("update Travel_Request set Country='" + coun + "',City='" + city + "',Departure_Date='" + employee.Convert_ToSqlDate(D_date) + "',Arrival_Date='" + employee.Convert_ToSqlDate(A_date) + "',Seat_Preference='" + pref + "',Project_name='" + project + "',other_info='" + other + "' where pn_TravelID='"+sno+"'", myConnection);
            cmd.ExecuteNonQuery();
            myConnection.Close();
            GridView1.EditIndex = -1; // turn to edit mode
            hr();
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void Img_Submit_Click(object sender, EventArgs e)
    {
        EmpFirstList = employee.fn_get_Emp_first(employee);

        if (EmpFirstList.Count > 0)
        {
            if (EmpFirstList[0].FullName != "")
            {
                Ename = EmpFirstList[0].FullName;
            }
        }

        else
        {
            lbl_Error.Text = "Error";
        }


        EmpProfileList = employee.fn_get_Emp_Profile1(employee);

        if (EmpProfileList.Count > 0)
        {
            if (EmpProfileList[0].DepartmentId != 1)
            {
                Dept = employee.fn_GetDepartmentName(EmpProfileList[0].DepartmentId);
                lbl_dept.Text = "dept";
            }
            else
            {
                lbl_dept.Text = "No dept";
            }

            if (EmpProfileList[0].DesignationId != 1)
            {
                Desg = employee.fn_GetDesignationName(EmpProfileList[0].DesignationId);
                lbl_desg.Text = "desg";
            }
            else
            {
                lbl_desg.Text = "No desg";
            }
        }

        else
        {
            lbl_Error.Text = "Error";
        }
        string txt_othr;
        if (txt_other.Value == "")
        {
            txt_othr = "No other information";
        }
        else
        {
            txt_othr = txt_other.Value;
        }

        try
        {
            myConnection.Open();
            cmd = new SqlCommand("Insert into Travel_Request(pn_CompanyID , pn_BranchID , pn_EmployeeID , Employee_name , Department , Designation , Country , City , Departure_Date , Arrival_Date , Seat_Preference , Project_name , other_info) values ('" + employee.CompanyId + "','" + employee.BranchId + "','" + employee.EmployeeId + "','" + Ename + "','" + Dept + "','" + Desg + "','" + txt_country.Value + "','" + txt_city.Value + "','" + employee.Convert_ToSqlDate(txt_Ddate.Value) + "','" + employee.Convert_ToSqlDate(txt_Adate.Value) + "','" + ddl_pref.SelectedItem.Text + "','" + txt_project.Value + "','" + txt_othr + "')", myConnection);
            cmd.ExecuteNonQuery();
            myConnection.Close();
            clear();
            hr();
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error. Could not submit!";
        }
    }
}
