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


public partial class Hrms_Reports_Report : System.Web.UI.MasterPage
{
    char s_login_role;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblmsg.Text = (string)Session["Login_Name"];
        img_photo.ImageUrl = (string)Session["Login_temp_Photo"];
        s_login_role = Convert.ToChar(Request.Cookies["Login_temp_Role"].Value);

        if (s_login_role == 'a')
        {

            lblmsg.Text = (string)Session["Login_Name"];
            img_photo.ImageUrl = (string)Session["Login_temp_Photo"];

        }

        else if (s_login_role == 'h')
        {
            this.SiteMapDataSource1.Provider = SiteMap.Providers["Menu3Sitemap"];
            lblmsg.Text = (string)Session["Login_Name"];
            img_photo.ImageUrl = (string)Session["Login_temp_Photo"];

        }


        else if (s_login_role == 'e')
        {
            // menu_del.Visible = false;
            this.SiteMapDataSource1.Provider = SiteMap.Providers["Menu2Sitemap"];
            lblmsg.Text = (string)Session["Login_Name"];
            img_photo.ImageUrl = (string)Session["Login_temp_Photo"];
        }
        else if (s_login_role == 'u')
        {
            //menu_del.Visible = false;
            lblmsg.Text = (string)Session["Login_Name"];
            img_photo.ImageUrl = (string)Session["Login_temp_Photo"];
        }
        else
        {
            Response.Redirect("Login.aspx");
        }

    }

    
}
