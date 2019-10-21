using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

public partial class Hrms_TimeAndAttenence_InOutMode : System.Web.UI.Page
{

    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings("connectionstring"));
    protected void Page_Load(object sender, EventArgs e)
    {
        populate_lv();
    }
    public void populate_lv()
    {
        var conStr = ConfigurationManager.ConnectionStrings["connectionstring"];
        string constr = conStr.ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        SqlCommand cmd = new SqlCommand("select * from shiftdetails", con);
        SqlDataAdapter ada = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        ada.Fill(ds);
        lv_shiftDetails.DataSource = ds;
        lv_shiftDetails.DataBind();
    }
    protected void cmd_apply_Click(object sender, EventArgs e)
    {
        //var connString = ConfigurationManager.ConnectionStrings("connectionstring");
        var conStr = ConfigurationManager.ConnectionStrings["connectionstring"];
        string constr = conStr.ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        
        con.Open();

        string txtintime =Convert.ToString(( txt_intime_hr.Text + ":" + txt_intime_min.Text + ":" + txt_intime_sec.Text));
        string txt_early_inlimit = (txt_early_inlimit_hr.Text + ":" + txt_early_inlimit_min.Text + ":" + txt_early_inlimit_sec.Text);
        string txt_shift_lateIn_EarlyOut_limit = (txt_shift_lateIn_EarlyOut_limit_hr.Text + ":" + txt_shift_lateIn_EarlyOut_limit_min.Text + ":" + txt_shift_lateIn_EarlyOut_limit_sec.Text);
        
        string txt_lunch_earlyin_lateOut_limit = (txt_lunch_earlyin_lateOut_limit_hr.Text + ":" + txt_lunch_earlyin_lateOut_limit_min.Text + ":" + txt_lunch_earlyin_lateOut_limit_sec.Text);
        string txt_halfday_workHrs_limit = (txt_halfday_workHrs_limit_hr.Text + ":" + txt_halfday_workHrs_limit_min.Text + ":" + txt_halfday_workHrs_limit_sec.Text);
        
        string txt_OT_limit=(txt_OT_limit_hr.Text+":"+txt_OT_limit_min.Text+":"+txt_OT_limit_sec.Text);
        string txt_permission_limit=(txt_permission_limit_hr.Text+":"+txt_permission_limit_min.Text+":"+txt_permission_limit_sec.Text);

        SqlCommand cmd = new SqlCommand("insert into InOutMode(intime,earlyintime,shift_lateIn_EarlyOut,lunch_EarlyIn_lateOut,halfdayworkhrs,ot,permission,monthlyleavedays) values('" + txt_intime_hr.Text + "','" + txt_early_inlimit + "','" + txt_shift_lateIn_EarlyOut_limit + "','" + txt_lunch_earlyin_lateOut_limit + "','" + txt_halfday_workHrs_limit + "','" + txt_OT_limit + "','" + txt_permission_limit + "','" + txt_monthly_leaveDays_limit.Text + "')", con);
        cmd.ExecuteNonQuery();
        
        con.Close();
    }





    protected void lv_shiftDetails_ItemInserting(object sender, ListViewInsertEventArgs e)
    {
        Response.Write("<script>alert('hello')</script>");
        var conStr = ConfigurationManager.ConnectionStrings["connectionstring"];
        string constr = conStr.ConnectionString;
        SqlConnection con = new SqlConnection(constr);

        //txt_monthly_leaveDays_limit.Text = e.ToString();
        //TextBox txtsc=e.Item.FindControl
        //SqlCommand cmd = new SqlCommand("insert into shiftdetails(shiftcode,starttime,breaktime,endtime,shiftindicator) values()", con);
        //SqlDataAdapter ada = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //ada.Fill(ds);
        //lv_shiftDetails.DataSource = ds;
        //lv_shiftDetails.DataBind();
    }
    protected void lv_shiftDetails_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            txt_monthly_leaveDays_limit.Text = "mei";
        }
    }
}
