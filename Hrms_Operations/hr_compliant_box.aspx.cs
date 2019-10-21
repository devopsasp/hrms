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

public partial class Hrms_Operations_hr_compliant_box : System.Web.UI.Page
{

    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();

    SqlConnection mycon = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    Company company = new Company();
    Employee employee = new Employee();
    Be_Recruitment recruitment = new Be_Recruitment();
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();

    Collection<Employee> EmployeeList;

    string s_login_role;
    string s_form = "";
    DataSet ds_userrights;
    string _Value1 = "", hr_complaint_id;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        lbl_Error.Text = "";
        hr_complaint_id = (string)Session["complaint_id"];

        if (!IsPostBack)
        {
            switch (s_login_role)
            {
                case "a":
                    //load();
                    //ddl_department_load1();
                    break;

                case "h":
                    load();
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
    public void load()
    {
        try
        {
            EmployeeList = employee.fn_compliant_box1(employee);

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
            employee.Id1 = Convert.ToInt32(((Label)Gvrow.FindControl("lbl_id")).Text);
            employee.Compliant_Subject1 = ((Label)Gvrow.FindControl("lbl_subject")).Text;
            employee.Compliant_Text1 = ((Label)Gvrow.FindControl("lbl_text")).Text;
            employee.Status21 = ((DropDownList)Gvrow.FindControl("ddl_status")).SelectedItem.Text;
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Id1")) == hr_complaint_id)
            {
                e.Row.BackColor = System.Drawing.Color.DarkGray;
                Session["complaint_id"] = null;
            }
        }
    }
}
