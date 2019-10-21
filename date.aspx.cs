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
using ePayHrms.BE.Recruitment;
using ePayHrms.Connection;
using ePayHrms.Candidate;
using System.IO;
using ePayHrms.Company;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ePayHrms.Leave;
using ePayHrms.Employee;

public partial class date : System.Web.UI.Page
{
    ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();

    Employee employee = new Employee();
    Leave l = new Leave();
    
    Collection<Employee> EmployeeList;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string query;
        DateTime dt = Convert.ToDateTime(TextBox1.Text);
        query = "set dateformat dmy;insert into dd values('" + dt + "');set dateformat mdy";
        employee.fn_reportbyid(query);

        query = "select * from dd";
        EmployeeList = employee.fn_getdt(query);
        TextBox2.Text = EmployeeList[0].dat.ToString();

    }
    
}
