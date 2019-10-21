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
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ePayHrms.Employee;
using ePayHrms.Connection;
using ePayHrms.Company;
using ePayHrms.Leave;

public partial class Hrms_Master_Appraisal_Default : System.Web.UI.Page
{
    ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();

    Company company = new Company();
    Employee employee = new Employee();
    Leave l = new Leave();

    Collection<Leave> AppraisalList;
    Collection<Company> CompanyList;
    Collection<Employee> EmployeeList;

    string s_login_role;
    string s_form = "", subquery, qry, _Value;
    DataSet ds_userrights, app_list;
    int ddl_i, i, index;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        lbl_Error.Text = "";
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        l.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);

        s_login_role = Request.Cookies["Login_temp_Role"].Value;

        if (!IsPostBack)
        {
            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {
                switch (s_login_role)
                {
                    case "a": load();
                        break;

                    case "h": load();
                        break;

                    case "u": s_form = "24";
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

                    default: Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                        Response.Redirect("~/Company_Home.aspx");
                        break;
                }

            }
        }
    }

    public void load()
    {
        qry = "select * from Appraisal_Band where pn_CompanyID=" + l.CompanyID;
            AppraisalList = l.fn_Appbonus(qry);
            if (AppraisalList.Count == 0)
            {
                for (i = 1; i <= 3; i++)
                {
                    qry = "insert into Appraisal_Band(pn_CompanyID, Band_type, Band_Name, From_value, To_value, Bonus_Points) values (" + l.CompanyID + ", 1,'B" + i + "', 0.0, 0.0," + i + ")";
                    employee.fn_reportbyid(qry);
                }

                for (i = 1; i <= 3; i++)
                {
                    qry = "insert into Appraisal_Band(pn_CompanyID, Band_type, Band_Name, From_value, To_value, Bonus_Points) values (" + l.CompanyID + ", 2,'S" + i + "', 0.0, 0.0," + i + ")";
                    employee.fn_reportbyid(qry);
                }
            }
    }
    protected void ddl_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        grid_load();
    }

    public void grid_load()
    {
        qry = "select * from Appraisal_Band where pn_CompanyID=" + l.CompanyID + " and Band_type=" + ddl_type.SelectedValue;
        AppraisalList = l.fn_Appbonus(qry);
        if (AppraisalList.Count > 0)
        {
            grid_appraisal.Visible = true;
            grid_appraisal.DataSource = AppraisalList;
            grid_appraisal.DataBind();
        }
        else
        {
            grid_appraisal.Visible = false;
        }
    }
    
    protected void Update(object sender, GridViewUpdateEventArgs e)
    {
        string f_value, t_value;
        try
        {
            f_value = ((HtmlInputText)grid_appraisal.Rows[e.RowIndex].FindControl("txt_fromvalue")).Value;
            t_value = ((HtmlInputText)grid_appraisal.Rows[e.RowIndex].FindControl("txt_tovalue")).Value;

            if (ddl_type.SelectedValue != "0")
            {
                if (f_value != "" && t_value != "")
                {
                    qry = "update Appraisal_Band set From_value=" + f_value + ", To_value=" + t_value + " where band_id=" + grid_appraisal.DataKeys[e.RowIndex].Value;
                    _Value = employee.fn_procappraisal(qry);
                    if (_Value == "0")
                    {
                        lbl_Error.Text = "<font color=blue>Saved Successfully</font>";
                        grid_load();
                    }
                    else
                    {
                        lbl_Error.Text = "Saved not Successfully";
                    }
                }
                else
                {
                    lbl_Error.Text = "Enter Both Values";
                }
            }
            else
            {
                grid_appraisal.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }
}
