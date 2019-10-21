using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
public partial class Hrms_Tasks_task_report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ReportDocument reportdocument = new ReportDocument();

        reportdocument.Load(Server.MapPath("~/CrystalReport.rpt"));
        reportdocument.SetDatabaseLogon("", "", "SQL-SERVER", "Hesperus_Hrms");

        CrystalReportViewer1.ReportSource = reportdocument;
        CrystalReportViewer1.RefreshReport();
    }
    protected void CrystalReportViewer1_Init(object sender, EventArgs e)
    {

    }
}