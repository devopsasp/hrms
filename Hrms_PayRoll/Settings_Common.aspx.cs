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
    Collection<PayRoll> ptlist;
    Collection<Company> CompanyList;
    DataSet ds_userrights;

    PayRoll pay = new PayRoll();
    Company company = new Company();

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
            //rdooption_changed();

            CompanyList = company.fn_getCompany();
            if (CompanyList.Count > 0)
            {
                switch (s_login_role)
                {
                    case "a":
                        load();
                        rdooption_changed();
                        break;

                    case "h":
                        load();
                        rdooption_changed();
                        break;

                    case "u":
                        s_form = "47";

                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            load();
                            rdooption_changed();
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
            pay.commonflag = 'Y';
            pay.Month_Calc = Convert.ToChar(rdo_option.SelectedItem.Value);
            pay.temp_str = txt_calcdays.Value;
            pay.Week_Holiday1 = ddl_week_holiday1.SelectedItem.Text;
            pay.Week_Holiday2 = ddl_week_holiday2.SelectedItem.Text;
            _value = pay.pay_Common_settings(pay);
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
        ptlist = pay.fn_Common_Settings(pay);
        
        if (ptlist.Count > 0)
        {
            txt_calcdays.Value = ptlist[0].temp_str;
           
 //Month 
            for (i = 0; i < rdo_option.Items.Count; i++)
            {
                if (rdo_option.Items[i].Value == ptlist[0].Month_Calc.ToString())
                {
                    rdo_option.SelectedIndex = i;
                }
            }

//Holiday 1

            for (i = 0; i < ddl_week_holiday1.Items.Count; i++)
             {
                if (Convert.ToString(ddl_week_holiday1.Items[i].Text) == ptlist[0].Week_Holiday1)
                {
                    ddl_week_holiday1.SelectedIndex = i;
                    break;

                }

            }

//Holiday 2

            for (i = 0;i<ddl_week_holiday2.Items.Count;i++)  
            {
                if (Convert.ToString(ddl_week_holiday2.Items[i].Text) == ptlist[0].Week_Holiday2)
                {
                    ddl_week_holiday2.SelectedIndex = i;

                    break;
                }
           }
       }
    }

    protected void rdo_option_SelectedIndexChanged(object sender, EventArgs e)
    {
        rdooption_changed();
    }

    public void rdooption_changed()
    {
        if (rdo_option.SelectedItem.Value == "D")
        {
            tab_check.Visible = true;
            lbl_calc.Visible = true;
            txt_calcdays.Visible = true;
            lbl_weekholiday1.Visible = false;
            lbl_weekholiday2.Visible = false;
            ddl_week_holiday1.Visible = false;
            ddl_week_holiday2.Visible = false;
        }
        else
        {
            tab_check.Visible = false;
        }

        if (rdo_option.SelectedItem.Value == "M")
        {
            tab_check.Visible = true;
            btn_save.Visible = true;
            lbl_calc.Visible = false;
            txt_calcdays.Visible = false;
            lbl_weekholiday1.Visible = false;
            lbl_weekholiday2.Visible = false;
            ddl_week_holiday1.Visible = false;
            ddl_week_holiday2.Visible = false;
        }
        
        if (rdo_option.SelectedItem.Value == "W")
        {
            tab_check.Visible = true;
            lbl_calc.Visible = false;
            txt_calcdays.Visible = false;
            lbl_weekholiday1.Visible = true;
            lbl_weekholiday2.Visible = true;
            ddl_week_holiday1.Visible = true;
            ddl_week_holiday2.Visible = true;

        }
        
    }
}
