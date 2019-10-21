using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ePayHrms.Employee;
using ePayHrms.Candidate;

public partial class Hrms_Additional_Default2 : System.Web.UI.Page
{
    Collection<Employee> emplist;

    sample s = new sample();
    Employee emp = new Employee();
  
   
    protected void Page_Load(object sender, EventArgs e)
    {
        emp.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        //s_login_role = Request.Cookies["Login_temp_Role"].Value;
        emp.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        s.date = Calendar1.SelectedDate;
        //CalendarDay;
       Calendar1.DataBind();
       
        }
        
    protected void  DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
{
    string categoryid;
    categoryid = ddl_all.SelectedItem.Value;
    if (categoryid == "0")
    {
        ddl_2.Visible = false;
    }
    else if (categoryid == "1")
    {
        emplist = emp.fn_Category();
        ddl_2.DataSource = emplist;
        ddl_2.DataValueField = "CategoryId";
        ddl_2.DataTextField = "CategoryName";
        ddl_2.DataBind();
    }
    else if (categoryid == "2")
    {
        emplist = emp.fn_getAllEmployees();

        ddl_2.DataSource = emplist;
        ddl_2.DataValueField = "EmployeeId";
        ddl_2.DataTextField = "LastName";
        ddl_2.DataBind();
    }
    else if (categoryid == "3")
    {
        emplist = emp.fn_Department(emp.BranchId);
        ddl_2.DataSource = emplist;
        ddl_2.DataValueField = "DepartmentId";
        ddl_2.DataTextField = "DepartmentName";
        ddl_2.DataBind();
    }
   
}
    public void employeeload()
    {
        emplist = emp.fn_EmptyDivisionList();
        ddl_2.DataSource = emplist;
        ddl_2.DataValueField = "DivisionId";
        ddl_2.DataTextField = "DivisionName";
        ddl_2.DataBind();

    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        s.date = Calendar1.SelectedDate;
        s.username = "aru";
        Calendar1.DataBind();

           }
}
