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

public partial class Hrms_Employee_EmployeeMaster : System.Web.UI.MasterPage
{
    string s_login_role;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        s_login_role = Request.Cookies["Login_temp_Role"].Value;

        if (s_login_role == "a")
        {

            //lblmsg.Text = (string)Session["Login_Name"];
            //img_photo.ImageUrl = (string)Session["Login_temp_Photo"];

        }

        else if (s_login_role == "h")
        {
            this.SiteMapDataSource1.Provider = SiteMap.Providers["Menu3Sitemap"];
            //lblmsg.Text = (string)Session["Login_Name"];
            //img_photo.ImageUrl = (string)Session["Login_temp_Photo"];

        }


        else if (s_login_role == "e")
        {
            // menu_del.Visible = false;
            this.SiteMapDataSource1.Provider = SiteMap.Providers["Menu2Sitemap"];
            //lblmsg.Text = (string)Session["Login_Name"];
            //img_photo.ImageUrl = (string)Session["Login_temp_Photo"];
        }
        else if (s_login_role == "u")
        {
            //menu_del.Visible = false;
            //lblmsg.Text = (string)Session["Login_Name"];
           // img_photo.ImageUrl = (string)Session["Login_temp_Photo"];
        }
        else
        {
            Response.Redirect("Login.aspx");
        }


    }    



}



//protected void lbtn_Deduction_Click(object sender, EventArgs e)
//    {
        
//        if (Convert.ToInt32(Request.Cookies["preview_emp"].Value) == 2)
//        {
//            Response.Redirect("Employee_Deducation.aspx");

//        }
//        else
//        {
//            //Response.Redirect("Employee_Profile.aspx");

//            lbl_error_msg.Text = "Please fill in the profile details and continue";

//        }


//    }