using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
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
public partial class Hrms_Master_Appraisal_Default : System.Web.UI.Page
{
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
        

        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        l.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);

        s_login_role = Request.Cookies["Login_temp_Role"].Value;



        if (!IsPostBack)
        {

            switch (s_login_role)
            {
                case "a": load();
                    break;

                case "h": load();
                    break;

                case "u": s_form = "Appraisal";
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        //l.CompanyID = 1;
        l.AppraisalmasterID = 0;
        l.AppraisalmasterName = txt_appraisalmastername.Value;
        l.AppraisalmasterCode = Txt_appmastercode.Value;
        l.status = 'y';
        _Value= l.Appraisalmaster(l);
        if (_Value != "1")
        {
            lbl_Error.Text = "<font color=Blue>Added Successfully</font>";
            txt_appraisalmastername.Value = "";
            Txt_appmastercode.Value = "";

            grid_load();
        }
        else
        {
            lbl_Error.Text = "<font color=Red>Error Occured</font>";
        }

    }
    protected void edit(object sender, GridViewEditEventArgs e)
    {
        try
        {
        l.AppraisalmasterID = Convert.ToInt32(grid_appraisal.DataKeys[e.NewEditIndex].Value);
        //l.CompanyID = 1;
        l.AppraisalmasterName = ((TextBox)grid_appraisal.Rows[e.NewEditIndex].FindControl("txtgridname")).Text;
        l.AppraisalmasterCode = ((TextBox)grid_appraisal.Rows[e.NewEditIndex].FindControl("Txtgridcode")).Text;
        

            check = name_validate(l.AppraisalmasterName);

            if (check == 0)
            {
                _Value = l.Appraisalmaster(l);

                if (_Value != "1")
                {
                    lbl_Error.Text = "<font color=Blue>Updated Successfully</font>";
                    txt_appraisalmastername.Value = "";
                    Txt_appmastercode.Value = "";
                    grid_load(); 



        ((ImageButton)grid_appraisal.Rows[e.NewEditIndex].FindControl("img_update")).Enabled = true;
        ((ImageButton)grid_appraisal.Rows[e.NewEditIndex].FindControl("img_save")).Enabled = false;
        ((TextBox)grid_appraisal.Rows[e.NewEditIndex].FindControl("txtgridname")).Enabled = false;
        ((TextBox)grid_appraisal.Rows[e.NewEditIndex].FindControl("Txtgridcode")).Enabled = false;


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

    
    protected void update(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
        ((TextBox)grid_appraisal.Rows[e.RowIndex].FindControl("txtgridname")).Enabled = true;
        ((TextBox)grid_appraisal.Rows[e.RowIndex].FindControl("Txtgridcode")).Enabled = true;
        ((ImageButton)grid_appraisal.Rows[e.RowIndex].FindControl("img_save")).Visible = true;
        ((ImageButton)grid_appraisal.Rows[e.RowIndex].FindControl("img_update")).Visible = false;
        }
            catch (Exception ex)
        {
            lbl_Error.Text = "Error";


        }



    }
    public void grid_load()
    {
        AppraisalList = l.fn_Appraisalmaster();
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
}
