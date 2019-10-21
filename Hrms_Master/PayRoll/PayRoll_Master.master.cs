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

public partial class Hrms_Master_PayRoll_PayRoll_Master : System.Web.UI.MasterPage
{
    string s_login_role;
    protected void Page_Load(object sender, EventArgs e)
    {
        s_login_role = Request.Cookies["Login_temp_Role"].Value;

        if (s_login_role == "a")
        {

            lblmsg.Text = (string)Session["Login_Name"];
            img_photo.ImageUrl = (string)Session["Login_temp_Photo"];

        }

        else if (s_login_role == "h")
        {
            this.SiteMapDataSource1.Provider = SiteMap.Providers["Menu3Sitemap"];
            lblmsg.Text = (string)Session["Login_Name"];
            img_photo.ImageUrl = (string)Session["Login_temp_Photo"];

        }


        else if (s_login_role == "e")
        {
            // menu_del.Visible = false;
            this.SiteMapDataSource1.Provider = SiteMap.Providers["Menu2Sitemap"];
            lblmsg.Text = (string)Session["Login_Name"];
            img_photo.ImageUrl = (string)Session["Login_temp_Photo"];
        }
        else if (s_login_role == "u")
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
