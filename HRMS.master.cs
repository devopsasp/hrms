using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

public partial class HRMSMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserName.Text = Request.Cookies["Login_Name"].Value;
        HtmlMeta meta = new HtmlMeta();
        meta.HttpEquiv = "Refresh";
        meta.Content = "2000;url=/hrms/Login.aspx";
        this.Page.Header.Controls.Add(meta);

        Response.Cookies["Cpage"].Value = HttpContext.Current.Request.Url.PathAndQuery;
    }

    
}
