using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ePayHrms.Company;
using ePayHrms.Employee;
using System.Data.SqlClient;

public partial class Hrms_Master_Common_Specialization : System.Web.UI.Page
{
    private SqlConnection _connection;
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlConnection mycon = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    Company company = new Company();
    SqlDataReader rea;
    Employee employee = new Employee();


    Collection<Company> BranchsList;
    Collection<Employee> SpecializationList;
    Collection<Company> CompanyList;
    int company_Id, branch_Id, valid, temp_valid = 0, check=0;
    string _Value;
    string s_login_role;
    string s_form = "";
    DataSet ds_userrights;
    static string cname = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        if (!IsPostBack)
        {
            switch (s_login_role)
            {
                case "a":
                   // load_admin();
                    break;
                case "h":
                   // ddl_branch.Visible = false;
                    load1();
                   // access();
                    break;

                case "u": s_form = "32";
                    ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                    if (ds_userrights.Tables[0].Rows.Count > 0)
                    {
                        load1();
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
    public void load1()
    {
        SpecializationList = employee.fn_Specialization(employee.BranchId);
        if (SpecializationList.Count > 0)
        {            
            Grid_Specialization.DataSource = SpecializationList;
            Grid_Specialization.DataBind();
        }
        else
        {
            SpecializationList = employee.fn_EmptySpecialization(employee);

            if (SpecializationList.Count > 0)
            {
                Grid_Specialization.DataSource = SpecializationList;
                Grid_Specialization.DataBind();
                ((ImageButton)Grid_Specialization.Rows[0].FindControl("img_update")).Visible = false;
            }
        }
    }
    protected void btnAddSpecialization_Click(object sender, EventArgs e)
    {
        try
        {
            check = name_validate(SpecializationName.Value);
            if (check == 0)
            {
                employee.specializationID = Convert.ToInt32(hSpecializationID.Value);
                employee.specializationName = SpecializationName.Value;
                employee.status = 'Y';
                _connection = con.fn_Connection();
                _connection.Open();
                cmd1 = new SqlCommand("select count(*) from hrmm_Specialization", _connection);
                int cc = (int)cmd1.ExecuteScalar();
                cmd = new SqlCommand("insert into hrmm_Specialization(pn_CompanyID,pn_BranchID,v_SpecializationName,status) values('" + employee.CompanyId + "','" + employee.BranchId + "','" + employee.specializationName + "','" + employee.status + "')", _connection);
                cmd.ExecuteNonQuery();
                cmd1 = new SqlCommand("select count(*) from hrmm_Specialization", _connection);
                int aa = (int)cmd1.ExecuteScalar();
                _connection.Close();
                if (cc != aa)
                {
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Added Successfully');", true);
                    ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Added Successfully');", true);              
                    SpecializationName.Value = "";
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Error Occured');", true);
                }
                SpecializationList = employee.fn_Specialization(employee.BranchId);
                if (SpecializationList.Count > 0)
                {
                    Grid_Specialization.DataSource = SpecializationList;
                    Grid_Specialization.DataBind();
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
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
            ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

    protected void Grid_Specialization_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            try
            {
                GridViewRow grow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);//getting the index
                HtmlInputText txt = (HtmlInputText)grow.FindControl("txtgrid");
                string str = txt.Value;

                mycon.Open();
                SqlCommand cmd = new SqlCommand("delete from hrmm_Specialization where v_SpecializationName='" + str + "' and pn_BranchID='" + employee.BranchId + "'", mycon);
                cmd.ExecuteNonQuery();
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Specialization Deleted Successfully');", true);
                ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Deleted Successfully');", true);
                load1();
            }
            catch (Exception exc)
            {
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Specialization already assigned to employees');", true);
                ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Specialization already assigned to employees');", true);
            }
        }

    }
    protected void Grid_Specialization_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void Update(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            HtmlInputText textgrid = ((HtmlInputText)Grid_Specialization.Rows[e.RowIndex].FindControl("txtgrid"));
            ((LinkButton)Grid_Specialization.Rows[e.RowIndex].FindControl("img_save")).Visible = true;
            ((LinkButton)Grid_Specialization.Rows[e.RowIndex].FindControl("img_update")).Visible = false;
            textgrid.Disabled = false;
            textgrid.Attributes.Add("style", "font-color:blue");
            textgrid.Attributes.Add("style", "width:500px");
            cname = textgrid.Value;
        }
        catch (Exception ex)
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
            ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }

    }
    protected void Edit(object sender, GridViewEditEventArgs e)
    {
        try
        {
            employee.specializationID = Convert.ToInt32(Grid_Specialization.DataKeys[e.NewEditIndex].Value);
            employee.specializationName = ((HtmlInputText)Grid_Specialization.Rows[e.NewEditIndex].FindControl("txtgrid")).Value;

            if (employee.specializationName != "")
            {
                if (employee.specializationName == cname)
                {
                    ((LinkButton)Grid_Specialization.Rows[e.NewEditIndex].FindControl("img_update")).Visible = true;
                    ((LinkButton)Grid_Specialization.Rows[e.NewEditIndex].FindControl("img_save")).Visible = false;
                    ((HtmlInputText)Grid_Specialization.Rows[e.NewEditIndex].FindControl("txtgrid")).Disabled = true;
                    ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Updated Successfully');", true);
                    cname = "";
                    return;
                }
                check = name_validate(employee.specializationName);
               // check = 0;

                if (check == 0)
                {
                    employee.fn_Update_Specialization(employee);
                    ((LinkButton)Grid_Specialization.Rows[e.NewEditIndex].FindControl("img_update")).Visible = true;
                    ((LinkButton)Grid_Specialization.Rows[e.NewEditIndex].FindControl("img_save")).Visible = false;
                    ((HtmlInputText)Grid_Specialization.Rows[e.NewEditIndex].FindControl("txtgrid")).Disabled = true;
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Specialization Updated Successfully');", true);
                    ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Updated Successfully');", true);
                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
                    ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Specialization Name already exist');", true);
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
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
            ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }

    }
    public int name_validate(string m_name)
    {
        SpecializationList = employee.fn_Specialization(employee.BranchId);

        if (SpecializationList.Count > 0)
        {
            for (valid = 0; valid < SpecializationList.Count; valid++)
            {

                if (SpecializationList[valid].specializationName.ToUpper().Trim() == m_name.ToUpper().Trim())
                {
                    temp_valid++;
                }
            }
        }
        return temp_valid;
    }
    protected void Grid_Specialization_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {

    }
}