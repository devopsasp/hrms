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
using CrystalDecisions.Enterprise;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.Reporting;
using CrystalDecisions.ReportSource;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using System.Data.SqlClient; 

public partial class Hrms_Reports_Report_view : System.Web.UI.Page
{
    private ReportDocument reportdoc = new ReportDocument();
    string report;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        lblmsg.Text = "Hi " + (string)Session["Login_UserID"] + "!";
        lbl_error.Text = "";

        string usrname = "", pwd = "", server = "", database = "", rid = "";
        DataSet ds = new DataSet();
        ds.ReadXml(Server.MapPath("..\\Database_Log.xml"));
        if (ds.Tables.Count > 0)
        {
            server = ds.Tables[0].Rows[0].ItemArray[0].ToString();
            database = ds.Tables[0].Rows[0].ItemArray[1].ToString();
            usrname = ds.Tables[0].Rows[0].ItemArray[2].ToString();
            pwd = ds.Tables[0].Rows[0].ItemArray[3].ToString();
        }

        report = Session["ReportName"].ToString();

        try
        {
            reportdoc.Load(Server.MapPath(report));

            reportdoc.DataSourceConnections[0].SetConnection(server, database, usrname, pwd);
            if (rid == "3a" || rid == "6a")
            {
                TextObject p = (TextObject)reportdoc.ReportDefinition.Sections[2].ReportObjects["Per"];
                p.Text = Session["Period"].ToString();
            }

            else if (rid == "Bench")
            {
                TextObject f = (TextObject)reportdoc.ReportDefinition.Sections[2].ReportObjects["fdate"];
                f.Text = Session["fdate"].ToString();
                TextObject t = (TextObject)reportdoc.ReportDefinition.Sections[2].ReportObjects["tdate"];
                t.Text = Session["tdate"].ToString();
            }
            CrystalReportViewer1.ReportSource = reportdoc;
            CrystalReportViewer1.DataBind();
            CrystalReportViewer1.RefreshReport();
            
        }
        catch (Exception ex)
        {

        }
    }
}
