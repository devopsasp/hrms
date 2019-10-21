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
public partial class Hrms_PayRoll_Default : System.Web.UI.Page
{
    PayRoll pay = new PayRoll();
    Company company = new Company();
    
    Collection<PayRoll> lwflist;
    Collection<Company> CompanyList;
    DataSet ds_userrights;

    int i;
    string _value, s_form;
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
            pay.LWF_Flag = 'Y';
            pay.LWF_Limit = Convert.ToInt32(txt_lwflimit.Value);
            pay.LWF_Amt = Convert.ToDouble(txt_lwf_amount.Value);
            pay.LWF_Month = Convert.ToInt32(ddl_lwf_month.SelectedItem.Value);
            _value = pay.pay_LWF_settings(pay);
            if (_value != "1")
            {
                lbl_Error.Text = "<font color=blue>Saved sucessfully</font>";
                load();
            }
            else
            {
                lbl_Error.Text = "<font color=blue>Error occured</font>";
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }        
    }

    public void load()
    {
        lwflist = pay.fn_LWF_Settings(pay);
        
        if (lwflist.Count > 0)
        {
            txt_lwflimit.Value = Convert.ToString(lwflist[0].LWF_Limit);
            txt_lwf_amount.Value = Convert.ToString(lwflist[0].LWF_Amt);


            for (i=0;i<ddl_lwf_month.Items.Count;i++)
            {

                if (Convert.ToInt32(ddl_lwf_month.Items[i].Value) == lwflist[0].LWF_Month)
                {
                    ddl_lwf_month.SelectedIndex = i;                   

                    break;

                }


            }
        }
    }




}
