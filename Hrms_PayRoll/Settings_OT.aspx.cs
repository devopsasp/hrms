using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.ObjectModel;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ePayHrms.Company;
using ePayHrms.Employee;
public partial class PayRoll_Default : System.Web.UI.Page
{    
    PayRoll pay = new PayRoll();
    Company company = new Company();

    Collection<Company> CompanyList;
    Collection<PayRoll> OTList;
    
    DataSet ds_userrights;

    string s_form, _value;
    string s_login_role;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;

        

        if (!IsPostBack)
        {
            //load();
            CompanyList = company.fn_getCompany();
            if (CompanyList.Count > 0)
            {
                switch (s_login_role)
                {
                    case "a":
                        load();
                        break;

                    case "h":
                        load();
                        break;

                    case "u":
                        s_form = "47";

                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            load();
                        }
                        else
                        {
                            Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator.";
                            Response.Redirect("~/Company_Home.aspx");
                        }
                        break;

                    default:
                        Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                        Response.Redirect("~/Company_Home.aspx");
                        break;
                }
            }
            else
            {
                Response.Cookies["Msg_Session"].Value = "Create Company";
                Response.Redirect("~/Company_Home.aspx");
            }
        }
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            pay.OT_Flag = 'Y';
            pay.OT_Days = Convert.ToDouble(txt_otdays.Value);
            pay.OT_HRS = Convert.ToDouble(txt_othours.Value);
            _value = pay.pay_ot_settings(pay);
            if (_value != "1")
            {
                lbl_Error.Text = "<font color=blue>Saved sucessfully</font>";
            }
            else
            {
                lbl_Error.Text = "<font color=red>Error occured</font>";
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    public void load()
    {
        OTList = pay.fn_OT_Settings(pay);

        if (OTList.Count > 0)
        {
            txt_otdays.Value = Convert.ToString(OTList[0].OT_Days);
            txt_othours.Value = Convert.ToString(OTList[0].OT_HRS);

        }

    }
}
