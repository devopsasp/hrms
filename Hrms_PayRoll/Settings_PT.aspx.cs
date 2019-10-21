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

public partial class Hrms_PayRoll_Default2 : System.Web.UI.Page
{
    Company company = new Company();
    PayRoll pay = new PayRoll();
    
    Collection<PayRoll> ptlist;
    Collection<Company> CompanyList;
    DataSet ds_userrights;

    int i,j;
    string _Value, str_month = "", s_form;
    string []str_array =new string[12];
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
        pay.PT_Flag = 'Y';

        pay.PTtype = Convert.ToChar(rrl_type.SelectedItem.Value);

        for (i = 0; i < chkl_month.Items.Count; i++)
        {
            if (chkl_month.Items[i].Selected == true)
            {
                str_month = str_month + chkl_month.Items[i].Value;
                //pay.PTmonth = chkl_month.SelectedItem.Value;
            }
        }

        pay.PTmonth = str_month;
                
      _Value= pay.pay_pt_settings(pay);
        if (_Value != "1")
        {
            lbl_Error.Text = "<font color=Blue>Added Successfully</font>";
            load();
        }
        else
        {
            lbl_Error.Text = "<font color=Red>Error Occured</font>";
        }
    }


    public void load()
    {
        ptlist = pay.fn_PT_Settings(pay);
        
        if (ptlist.Count > 0)
        {
 //PT Type
            for (i = 0; i < rrl_type.Items.Count; i++)
            {
                if (Convert.ToChar(rrl_type.Items[i].Value) == ptlist[0].PTtype)
                {
                    rrl_type.SelectedIndex = i;
                    break;
                }
            }
 //Months
            str_month = ptlist[0].PTmonth;

            if(str_month!="")
            {
                j = 0;
                for (i = 0; i < str_month.Length; i=i+2)
                {
                    str_array[j] = str_month.Substring(i, 2);
                    j++;
                }

                for (i = 0; i < chkl_month.Items.Count; i++)
                {
                    for (j = 0; j < str_array.Length; j++)
                    {
                        if (chkl_month.Items[i].Value == str_array[j])
                        {
                            chkl_month.Items[i].Selected = true;
                        }
                    }
                }
            }           

        }
    }
}
