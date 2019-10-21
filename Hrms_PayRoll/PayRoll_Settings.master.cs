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

public partial class PayRoll_PayRoll_Settings : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblmsg.Text = "Hi " + Request.Cookies["Login_UserID"].Value + "!"; //(string)Session["Login_Name"] + "!";
        img_photo.ImageUrl = (string)Session["Login_temp_Photo"]; //(string)Session["Login_temp_Photo"];
    }
}
