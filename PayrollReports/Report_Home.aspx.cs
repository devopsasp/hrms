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
using ePayHrms.Connection;
using ePayHrms.Company;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public partial class PayrollReports_Default : System.Web.UI.Page
{
    Company company = new Company();
    Collection<Company> CompanyList;
    private SqlConnection _Connection;

    ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();

    string msg;
    char s_login_role;

    protected void Page_Load(object sender, EventArgs e)
    {
        CompanyList = company.fn_getCompany();

        if (CompanyList.Count > 0)
        {
            s_login_role = Convert.ToChar(Request.Cookies["Login_temp_Role"].Value);

            if (s_login_role == 'a' || s_login_role == 'h' || s_login_role == 'e' || s_login_role == 'u')
            {
                msg = (string)Session["Msg_session"];
                lbl_Error.Text = msg;
                Session["Msg_session"] = "";
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
        }
        else
        {
            lbl_Error.Text = "Create Company";
        }
    }
}
