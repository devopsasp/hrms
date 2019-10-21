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
using System.Data.SqlClient;

public partial class Hrms_Master_Default : System.Web.UI.Page
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
    Collection<Employee> skillList;
    Collection<Company> CompanyList;
    int company_Id, branch_Id, valid, temp_valid = 0, check;
    string _Value;
    string s_login_role;
    string s_form = "";
    DataSet ds_userrights;
    static string sname = "";

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
                    load_admin();                    
                    break;

                case "h":
                    ddl_branch.Visible = false;
                    load1();
                    access();
                    break;

                case "u": s_form = "33";
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

    public void access()
    {
        // MessageBox.Show(employee.BranchId.ToString());
        // MessageBox.Show(employee.CompanyId.ToString());
        _connection = con.fn_Connection();
        _connection.Open();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and section_view='No'", _connection);
        SqlDataReader rdrview = cmd.ExecuteReader();
        if (rdrview.Read())
        {
            Response.Write("<script language='javascript'>alert('Permission Restricted. Please Contact Administrator.');window.location='~/Company_Home.aspx';</script>");
        }
        rdrview.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and section_edit='No'", _connection);
        SqlDataReader rdredit = cmd.ExecuteReader();
        if (rdredit.Read())
        {
            for (int b = 0; b < grid_skill.Rows.Count; b++)
            {
                ((ImageButton)grid_skill.Rows[b].FindControl("img_update")).Visible = false;
            }
            ((Label)grid_skill.HeaderRow.FindControl("lbledit")).Visible = false;
        }
        rdredit.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and  section_delete='No'", _connection);
        SqlDataReader rdrdel = cmd.ExecuteReader();
        if (rdrdel.Read())
        {
            // ((ImageButton)grid_Course.Rows[0].FindControl("img_update")).Visible = false;
            for (int a = 0; a < grid_skill.Rows.Count; a++)
            {
                ((ImageButton)grid_skill.Rows[a].FindControl("imgdel")).Visible = false;
            }
            ((System.Web.UI.Control)grid_skill.HeaderRow.FindControl("lbldel")).Visible = false;
        }
        rdrdel.Close();

    }

    public void load_admin()
    {
        mycon.Open();
        SqlDataAdapter ad = new SqlDataAdapter("select * from paym_branch", mycon);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        ddl_branch.DataTextField = "branchname";
        ddl_branch.DataValueField = "pn_branchid";
        ddl_branch.DataSource = ds;
        ddl_branch.DataBind();
        ddl_branch.Items.Insert(0, "Select Branch");
        mycon.Close();
    }

    public void load1()
    {
        skillList = employee.fn_skill1(employee);

        if (skillList.Count > 0)
        {
            grid_skill.DataSource = skillList;
            grid_skill.DataBind();
        }
        else
        {
            skillList = employee.fn_EmptyskillList(employee);

            if (skillList.Count > 0)
            {
                grid_skill.DataSource = skillList;
                grid_skill.DataBind();

                ((ImageButton)grid_skill.Rows[0].FindControl("img_update")).Visible = false;
            }
        }
    }

    protected void Edit(object sender, GridViewEditEventArgs e)
    {
        try
        {
            employee.SkillId = Convert.ToInt32(grid_skill.DataKeys[e.NewEditIndex].Value);
            employee.SkillName = ((HtmlInputText)grid_skill.Rows[e.NewEditIndex].FindControl("txtgrid")).Value;

            if (employee.SkillName != "")
            {
                if (employee.SkillName == sname)
                {
                    ((LinkButton)grid_skill.Rows[e.NewEditIndex].FindControl("img_update")).Visible = true;
                    ((LinkButton)grid_skill.Rows[e.NewEditIndex].FindControl("img_save")).Visible = false;
                    ((HtmlInputText)grid_skill.Rows[e.NewEditIndex].FindControl("txtgrid")).Disabled = true;
                    ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Updated Successfully');", true);
                    sname = "";
                    return;
                }
                check = 0;
                check = name_validate(employee.SkillName);
                if (check == 0)
                {
                    employee.fn_Update_skill(employee);

                    ((LinkButton)grid_skill.Rows[e.NewEditIndex].FindControl("img_update")).Visible = true;
                    ((LinkButton)grid_skill.Rows[e.NewEditIndex].FindControl("img_save")).Visible = false;
                    ((HtmlInputText)grid_skill.Rows[e.NewEditIndex].FindControl("txtgrid")).Disabled = true;
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Skill Updated Successfully');", true);           
                    ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Skill Updated Successfully');", true);
                    load1();
                    
                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);           
                    ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Skill Name already exists.');", true);
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

    protected void Update(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            sname = "";
            HtmlInputText textgrid = ((HtmlInputText)grid_skill.Rows[e.RowIndex].FindControl("txtgrid"));
            ((LinkButton)grid_skill.Rows[e.RowIndex].FindControl("img_save")).Visible = true;
            ((LinkButton)grid_skill.Rows[e.RowIndex].FindControl("img_update")).Visible = false;
            textgrid.Disabled = false;
            textgrid.Attributes.Add("style", "font-color:blue");
            textgrid.Attributes.Add("style", "width:500px");
            sname = textgrid.Value;
        }
        catch (Exception ex)
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
            ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }

    }


    public int name_validate(string m_name)
    {

        skillList = employee.fn_skill1(employee);

        if (skillList.Count > 0)
        {
            for (valid = 0; valid < skillList.Count; valid++)
            {

                if (skillList[valid].SkillName.ToLower() == m_name.ToLower())
                {
                    temp_valid++;
                }

            }

        }
        return temp_valid;
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        try
        {
            if (skillName.Value != "")
            {
                check = name_validate(skillName.Value);
                if (check == 0)
                {
                    employee.SkillId = Convert.ToInt32(hskillID.Value);
                    employee.SkillName = skillName.Value;
                    employee.status = 'Y';
                    _connection = con.fn_Connection();
                    //_connection.Open();
                    //cmd = new SqlCommand("insert into hrmm_skillsmaster values('" + employee.CompanyId + "', '" + employee.SkillName + "','" + employee.status + "', '" + employee.BranchId + "')", _connection);
                    //cmd.ExecuteNonQuery();
                    //_connection.Close();

                    _Value = employee.skillUpdate(employee);
                    if (_Value == "0")
                    {
                        //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Added Successfully');", true);
                        ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Added Successfully');", true);
                        skillName.Value = "";
                    }
                    else
                    {
                        //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
                        ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Error Occured');", true);
                    }

                    skillList = employee.fn_skill1(employee);

                    if (skillList.Count > 0)
                    {
                        grid_skill.DataSource = skillList;
                        grid_skill.DataBind();
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Skill Name already exists');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Enter Skill Name');", true);
            }
        }
        catch (Exception ex)
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
            ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }
    protected void grid_skill_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            try
            {
                GridViewRow grow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);//getting the index
                HtmlInputText txt = (HtmlInputText)grow.FindControl("txtgrid");
                string str = txt.Value;
                mycon.Open();
                SqlCommand cmd = new SqlCommand("delete from hrmm_SkillsMaster where v_SkillName='" + str + "' and branchid='" + employee.BranchId + "'", mycon);
                cmd.ExecuteNonQuery();
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Skill Deleted Successfully');", true);           
                ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Skill Deleted Successfully');", true);
                load1();

            }
            catch (Exception exc)
            {
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Skill already assigned to employees');", true);
                ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Skill already assigned to employees');", true);
            }
        }
    }
    protected void grid_skill_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void ddl_branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (s_login_role == "a")
        {
            employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
        }
        load1();
    }
}

