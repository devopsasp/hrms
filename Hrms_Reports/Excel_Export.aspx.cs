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


public partial class Hrms_Reports_Excel_Export : System.Web.UI.Page
{
   
    Employee employee = new Employee();
   
    Collection<Employee> CheckList;
    Collection<Employee> emp_edu_List;
    DataSet gridList;

    string s_login_role;

    int temp_i, i, j, count = 0, temp_count = 0;
    string temp_string = "", temp_earn = "", temp_ses = "";
  
    string query="";
    int temp=0;
 

    protected void Page_Load(object sender, EventArgs e)
    {

        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        lblmsg.Text = "";
        
        lblmsg.Text = "Hi " + Request.Cookies["Login_UserID"].Value + "!";
        lbl_error.Text = "";
        btn_report.Visible = false;

        if (!IsPostBack)
        {
            query = (string)Session["Final_Query"];

            gridList = employee.Temp_Employee_retrive(query);

            if (gridList.Tables.Count > 0)
            {
                grd_execl.DataSource = gridList;
                grd_execl.DataBind();
                btn_report.Visible = true;

            }
        }
    }      

    private void PrepareGridViewForExport(Control gv)
    {
        TextBox T = new TextBox();
        Literal l = new Literal();
        string name = String.Empty;
        for (int i = 0; i < gv.Controls.Count; i++)
        {
            if (gv.Controls[i].GetType() == typeof(TextBox))
            {
                l.Text = (gv.Controls[i] as TextBox).Text;
                gv.Controls.Remove(gv.Controls[i]);
                gv.Controls.AddAt(i, l);
            }
            if (gv.Controls[i].HasControls())
            {
                PrepareGridViewForExport(gv.Controls[i]);
            }
        }
    }               

    protected void btn_report_Click(object sender, ImageClickEventArgs e)
    {
        if (grd_execl.Rows.Count > 0)
        {
            
            PrepareGridViewForExport(grd_execl);
            string attachment = "attachment; filename=Contacts.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            HtmlForm frm = new HtmlForm();
            grd_execl.Parent.Controls.Add(frm);
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(grd_execl);
            frm.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();


        }
        //else
        //{
        //    lbl_error.Text = "First Click Gridview Button";

        //}


    }
    
    protected void btn_back_Click(object sender, ImageClickEventArgs e)
    {
       Response.Cookies["Query_Session"].Value = "back";

        Response.Redirect("Report.aspx");
    }



}



