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

    Collection<PayRoll> bonuslist;
    Collection<Company> CompanyList;
    DataSet ds_userrights;

    int i;
    string _value, s_form;
    string s_login_role;

    protected void Page_Load(object sender, EventArgs e)
    {
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
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
            pay.Bonus_Flag = 'Y';
            pay.BonusFromMonth = Convert.ToInt32(ddl_f_month.SelectedItem.Value);
            pay.BonusToMonth = Convert.ToInt32(ddl_t_month.SelectedItem.Value);
            pay.Bonuslimit = Convert.ToInt32(txt_bonuslimit.Value);
            pay.BonusType = Convert.ToChar(rr_type.SelectedItem.Value);

            _value = pay.pay_Bonus_settings(pay);
            if (_value != "1")
            {
                lbl_Error.Text = "<font color=blue>Saved Sucessfully</font>";
                load();
            }
            else
            {
                lbl_Error.Text = "<font color=Red>Error Occured</font>";
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    public void load()
    {

        bonuslist = pay.fn_Bonus_Settings(pay);

        if (bonuslist.Count > 0)
        {
            txt_bonuslimit.Value = Convert.ToString(bonuslist[0].Bonuslimit);

//From Month
            for (i = 0; i < ddl_f_month.Items.Count; i++)
            {

                if (Convert.ToInt32(ddl_f_month.Items[i].Value) == bonuslist[0].BonusFromMonth)
                {
                    ddl_f_month.SelectedIndex = i;
                    break;
                }
            }

//To Month
            for (i = 0; i < ddl_t_month.Items.Count; i++)
            {
                if (Convert.ToInt32(ddl_t_month.Items[i].Value) == bonuslist[0].BonusToMonth)
                {
                    ddl_t_month.SelectedIndex = i;
                    break;
                }
            }

//Bonus Type

            for (i = 0; i < rr_type.Items.Count; i++)
            {
                if (rr_type.Items[i].Value ==Convert.ToString(bonuslist[0].BonusType))
                {
                    rr_type.SelectedIndex = i;
                    break;
                }
            }
        }
    }  
}
