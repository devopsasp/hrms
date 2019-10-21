using System;
using System.Data;
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
using ePayHrms.Leave;
using System.Data.SqlClient;

public partial class Hrms_Master_Default : System.Web.UI.Page
{
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    //private SqlConnection _con;
    //ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlCommand cmd = new SqlCommand();
    Company company = new Company();
    Employee employee = new Employee();
    Leave l = new Leave();

    Collection<Leave> AppraisalList;
    Collection<Company> CompanyList;

    int company_Id, branch_Id, valid, temp_valid = 0, check;
    string _Value;
    string s_login_role;
    string s_form = "";
    DataSet ds_userrights;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        l.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;

        if (!IsPostBack)
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

    public void load()
    {
        grid_load();
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        l.AppraisalID = 0;
        l.AppraisalName = txt_appraisalname.Value;
        l.totalpoint = 10;
        l.status = 'Y';
        _Value = l.Appraisal(l);

        if (_Value != "1")
        {
            lbl_Error.Text = "<font color=Blue>Added Successfully</font>";
            
            myConnection.Open();
            cmd = new SqlCommand("update paym_Appraisal set pn_BranchID = '" + employee.BranchId + "' where v_AppraisalName = '" + txt_appraisalname.Value + "' ", myConnection);
            cmd.ExecuteNonQuery();
            myConnection.Close();
            txt_appraisalname.Value = "";
            //Txt_points.Value = "";
            grid_load();
        }
        else
        {
            lbl_Error.Text = "<font color=Red>Error Occured</font>";
        }
    }

    public void grid_load()
    {
        AppraisalList = l.fn_Appraisal1(employee.BranchId);

        if (AppraisalList.Count > 0)
        {
            grid_appraisal.DataSource = AppraisalList;
            grid_appraisal.DataBind();
        }
        else
        {
            lbl_Error.Text = "No Data in Master";
        }
    }

    protected void Update(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            ((HtmlInputText)grid_appraisal.Rows[e.RowIndex].FindControl("txtgrid")).Disabled = false;
            ((HtmlInputText)grid_appraisal.Rows[e.RowIndex].FindControl("Txtgpoint")).Disabled = false;
            ((ImageButton)grid_appraisal.Rows[e.RowIndex].FindControl("img_save")).Visible = true;
            ((ImageButton)grid_appraisal.Rows[e.RowIndex].FindControl("img_update")).Visible = false;
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    protected void Edit(object sender, GridViewEditEventArgs e)
    {

        try
        {
            l.AppraisalID = Convert.ToInt32(grid_appraisal.DataKeys[e.NewEditIndex].Value);
            l.AppraisalName = ((HtmlInputText)grid_appraisal.Rows[e.NewEditIndex].FindControl("txtgrid")).Value;
            if (((HtmlInputText)grid_appraisal.Rows[e.NewEditIndex].FindControl("Txtgpoint")).Value != "")
            {
                l.totalpoint = Convert.ToInt32(((HtmlInputText)grid_appraisal.Rows[e.NewEditIndex].FindControl("Txtgpoint")).Value);
            }
            else
            {
                ClientScriptManager manager = Page.ClientScript;
                manager.RegisterStartupScript(this.GetType(), "Call", "show_Error1();", true);

            }

            l.status = 'Y';
            check = name_validate(l.AppraisalName);
            if (l.AppraisalName != "")
            {
                if (check == 0)
                {
                    _Value = l.Appraisal(l);

                    if (_Value != "1")
                    {
                        lbl_Error.Text = "<font color=Blue>Updated Successfully</font>";
                        txt_appraisalname.Value = "";
                        //Txt_points.Value = "";
                        grid_load();

                        ((ImageButton)grid_appraisal.Rows[e.NewEditIndex].FindControl("img_update")).Visible = true;
                        ((ImageButton)grid_appraisal.Rows[e.NewEditIndex].FindControl("img_save")).Visible = false;
                        ((HtmlInputText)grid_appraisal.Rows[e.NewEditIndex].FindControl("txtgrid")).Disabled = true;
                        ((HtmlInputText)grid_appraisal.Rows[e.NewEditIndex].FindControl("Txtgpoint")).Disabled = true;
                    }
                    else
                    {
                        lbl_Error.Text = "<font color=Red>Error Occured</font>";
                    }

                }
                else
                {
                    ClientScriptManager manager = Page.ClientScript;
                    manager.RegisterStartupScript(this.GetType(), "Call", "show_message();", true);
                }

            }
            else
            {
                ClientScriptManager manager = Page.ClientScript;
                manager.RegisterStartupScript(this.GetType(), "Call", "show_Error();", true);
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    public int name_validate(string m_name)
    {

        AppraisalList = l.fn_Appraisal();
        
        if (AppraisalList.Count > 0)
        {
            for (valid = 0; valid < AppraisalList.Count; valid++)
            {

                if (AppraisalList[valid].AppraisalName == m_name)
                {
                    temp_valid++;

                }

            }

        }
        return temp_valid;
    }

    protected void delete(object sender, GridViewDeleteEventArgs e)
    {

    }
}
