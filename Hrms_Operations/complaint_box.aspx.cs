using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using ePayHrms.Login;
using ePayHrms.BE.Recruitment;
using ePayHrms.Connection;
using ePayHrms.Candidate;
using ePayHrms.Company;
using ePayHrms.Employee;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public partial class Hrms_Operations_complaint_box : System.Web.UI.Page
{
    private SqlConnection _Connection;
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    private SqlConnection _connection;
    SqlConnection mycon = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    Company company = new Company();
    Employee employee = new Employee();
    Be_Recruitment recruitment = new Be_Recruitment();
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();

    Collection<Employee> DepartmentList;
    Collection<Be_Recruitment> DepartmentList1;
    Collection<Employee> EmployeeList;
    Collection<Be_Recruitment> RecruitmentList;

    //int company_Id, branch_Id, valid, temp_valid = 0, check;
    string _Value;
    string query = "";
    string s_login_role;
    //int ddl_i = -1;
    string s_form = "";
    DataSet ds_userrights;
    string _Value1 = "";
    int ddl_i, complaint_id, complaint_id1, counter, Employee_ID;
    string hr_complaint_id;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        employee.EmployeeCode = Request.Cookies["Login_temp_EmployeeID"].Value;
        employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
       // employee.QuerBy = Request.Cookies["Login_UserID"].Value;
        lbl_Error.Text = "";
        //Error.Text = "";
        if (!IsPostBack)
        {
            switch (s_login_role)
            {
                case "a":
                    //load();
                    //ddl_department_load1();
                    break;

                case "h":
                    //load();
                    //sortload();
                    //ddl_department_load1();
                    break;
                case "e":
                    load();
                    //sortload();
                    //ddl_department_load1();
                    break;
                case "u": s_form = "83";
                    ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                    if (ds_userrights.Tables[0].Rows.Count > 0)
                    {
                        /// load();
                        //ddl_department_load1();
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
    protected void btn_Report_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            
            employee.Compliant_Subject1 = txt_subject.Text;
            employee.Compliant_Text1 = txt_compliant.Text;
            employee.Status21 = "Waiting";
            _Value = employee.Compliant_Box(employee);
            if (_Value != "1")
            {
                lbl_Error.Text = "<font color=Blue>Added Successfully</font>";
                load();
            }
            else
            {
                lbl_Error.Text = "<font color=Red>Error Occured</font>";
                load();
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void load()
    {
        try
        {
            EmployeeList = employee.fn_compliant_box(employee);

            if (EmployeeList.Count > 0)
            {
                GridView1.DataSource = EmployeeList;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = EmployeeList;
                GridView1.DataBind();
            }
            complaint_notification_load();
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;

        load();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        load();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow Gvrow = GridView1.Rows[e.RowIndex];
        if (Gvrow != null)
        {
            string str = ((Label)Gvrow.FindControl("lbl_empid")).Text;
            string[] str1 = str.Split('-');
            employee.EmployeeCode = str1[0].ToString();
          
            employee.Id1=Convert.ToInt32(((Label)Gvrow.FindControl("lbl_id")).Text);
            employee.Compliant_Subject1 = ((TextBox)Gvrow.FindControl("txt_subject")).Text;
            employee.Compliant_Text1 = ((TextBox)Gvrow.FindControl("txt_text")).Text;
            employee.Status21 = ((Label)Gvrow.FindControl("lbl_Status")).Text;
            _Value1 = employee.Compliant_Box(employee);
            GridView1.EditIndex = -1;
            if (_Value1 != "1")
            {
                lbl_Error.Text = "<font color=Blue>Updated Successfully</font>";
                load();
            }
            else
            {
                lbl_Error.Text = "<font color=Red>Error Occured</font>";
                load();
            }
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string ID = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lbl_id")).Text;
        DeleteRecord(ID);
        load();
       
    }

    private void DeleteRecord(string ID)
    {

        string sqlStatement = "DELETE FROM compliant_box WHERE id = @id";
        try
        {
            _connection = con.fn_Connection();
            _connection.Open();
            SqlCommand cmd = new SqlCommand(sqlStatement, _connection);
            cmd.Parameters.AddWithValue("@id", ID);
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
            _connection.Close();
        }
    }
    public void complaint_notification_load()
    {
        try
        {
            var sqlCon = ConfigurationManager.ConnectionStrings["connectionstring"];
            string constr = sqlCon.ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            //int notification = 0;
            cmd = new SqlCommand("select count(*) from notifications_complaints ", con);
            int notification = Convert.ToInt32(cmd.ExecuteScalar());
            if (notification == 0)
            {
                cmd = new SqlCommand("select id,pn_employeeID from Compliant_Box where pn_companyID='" + employee.CompanyId + "' and pn_branchID='" + employee.BranchId + "' and status='Waiting'", con);
                SqlDataReader drd0 = cmd.ExecuteReader();
                while (drd0.Read())
                {

                    complaint_id = Convert.ToInt32(drd0["id"]);
                    Employee_ID = Convert.ToInt32(drd0["pn_employeeID"]);
                    //drd0.Close();

                    cmd = new SqlCommand("insert into notifications_complaints values ('" + employee.CompanyId + "','" + employee.BranchId + "','" + Employee_ID + "','" + complaint_id + "')", con);
                    cmd.ExecuteNonQuery();
                }
                drd0.Close();

            }

            else
            {
                SqlCommand cmd5 = new SqlCommand("select id from Compliant_Box where status!='Waiting'", con);
                SqlDataReader drd3 = cmd5.ExecuteReader();
                while (drd3.Read())
                {
                    int complaint_id2 = Convert.ToInt32(drd3["id"]);
                    SqlCommand cmd6 = new SqlCommand("delete from notifications_complaints where complaint_id='" + complaint_id2 + "' and pn_employeeID='" + employee.EmployeeId + "'", con);
                    cmd6.ExecuteNonQuery();
                }
                cmd = new SqlCommand("select id,pn_employeeID from Compliant_Box where pn_companyID='" + employee.CompanyId + "' and pn_branchID='" + employee.BranchId + "' and status='Waiting'", con);
                SqlDataReader drd = cmd.ExecuteReader();
                while (drd.Read())
                {
                    counter = 0;
                    complaint_id = Convert.ToInt32(drd["id"]);
                    Employee_ID = Convert.ToInt32(drd["pn_employeeID"]);
                    SqlCommand cmd3 = new SqlCommand("select complaint_id from notifications_complaints", con);
                    SqlDataReader drd1 = cmd3.ExecuteReader();
                    while (drd1.Read())
                    {
                        complaint_id1 = Convert.ToInt32(drd1["complaint_id"]);
                        if (complaint_id == complaint_id1)
                        {
                            counter = counter + 1;
                        }
                    }
                    drd1.Close();
                    if (counter == 0)
                    {
                        SqlCommand cmd1 = new SqlCommand("insert into notifications_complaints values ('" + employee.CompanyId + "','" + employee.BranchId + "','" + Employee_ID + "','" + complaint_id + "')", con);
                        cmd1.ExecuteNonQuery();
                    }
                }

                drd.Close();
                con.Close();
            }
        }

        catch (Exception ex)
        {
            //lbl_Error.Text = "Error";
        }
    }



}
