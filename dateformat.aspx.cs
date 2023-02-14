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
using CrystalDecisions.Web;
using CrystalDecisions.Enterprise;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.Reporting;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Windows.Forms;

public partial class dateformat : System.Web.UI.Page
{
    Employee employee = new Employee();
    Collection<Employee> emplist = new Collection<Employee>();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string query;
        DateTime dt = sqldate(TextBox1.Text);
        TextBox2.Text = dt.ToString();
        employee.Date = dt;
        //TextBox3.Text = Convert_ToIISDate(dt.ToString());
        //query = "insert into temp_date (date) values('"+ dt+"')";
        employee.date_insert(employee);

        query = "select * from temp_date";
        emplist = employee.fn_getdt(query);
        TextBox3.Text = Convert_ToIISDate(emplist[0].dat.ToString());
    }

    public DateTime sqldate(string cur_date)
    {
        string _d, _m, _y;
        DateTime sql_date;
        if (cur_date != "")
        {
            string[] da = cur_date.Split('/');
            _d = da[0];
            _m = da[1];
            _y = da[2];

            sql_date = Convert.ToDateTime(_y + "/" + _m + "/" + _d);
        }
        else
        {
            sql_date = Convert.ToDateTime("1900/01/01");

        }

        return sql_date;

    
    }

    public string Convert_ToIISDate(string cur_date)
    {

        string _d, _m, _y, sql_date = "";

        char[] splitter ={ '/' };
        string[] str_ary = new string[4];

        if (cur_date.Length == 10)
        {
            _m = cur_date.Substring(0, 2);
            _d = cur_date.Substring(3, 2);
            _y = cur_date.Substring(6, 4);

            sql_date = _d + "/" + _m + "/" + _y;
        }
        else
        {
            str_ary = cur_date.Split(splitter);


            _m = str_ary[0];
            _d = str_ary[1];
            _y = str_ary[2];

            sql_date = _d + "/" + _m + "/" + _y;

        }

        return sql_date;

    }
}
