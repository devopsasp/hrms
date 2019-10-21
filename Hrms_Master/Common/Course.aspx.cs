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
    SqlDataReader rea;
    Company company = new Company();
    Employee employee = new Employee();
    Collection<Company> BranchsList;
    Collection<Employee> CourseList;
    Collection<Company> CompanyList;
    int company_Id, branch_Id, valid, temp_valid = 0, check = 0;
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
                    load_admin();
                    break;

                case "h":
                    ddl_branch.Visible = false;
                    load1();
                    access();
                    break;

                case "u": s_form = "31";
                    ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                    if (ds_userrights.Tables[0].Rows.Count > 0)
                    {
                        load1();
                    }
                    else
                    {
                        Response.Cookies["Msg_Session"].Value = "Permission Restricted. Please Contact Administrator.";
                        Response.Redirect("~/Hrms_Company/Company_Home.aspx");
                    }
                    break;

                default: Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                    Response.Redirect("~/Hrms_Company/Company_Home.aspx");
                    break;

            }
        }
    }

    //public void load()
    // {

    //    CourseList = employee.fn_course();

    //    if (CourseList.Count > 0)
    //    {
    //        grid_Course.DataSource = CourseList;
    //        grid_Course.DataBind();
    //    }
    //    else
    //    {
    //        CourseList = employee.fn_EmptycourseList();

    //        if (CourseList.Count > 0)
    //        {
    //            grid_Course.DataSource = CourseList;
    //            grid_Course.DataBind();

    //            ((ImageButton)grid_Course.Rows[0].FindControl("img_update")).Visible = true;
    //        }

    //    }

    //}


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

    public void access()
    {
        _connection = con.fn_Connection();
        _connection.Open();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and section_view='No'", _connection);
        SqlDataReader rdrview = cmd.ExecuteReader();
        if (rdrview.Read())
        {
            Response.Write("<script language='javascript'>alert('Permission Restricted. Please Contact Administrator.');window.location='~/Hrms_Company/Company_Home.aspx';</script>");
        }
        rdrview.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and section_edit='No'", _connection);
        SqlDataReader rdredit = cmd.ExecuteReader();
        if (rdredit.Read())
        {
            for (int b = 0; b < grid_Course.Rows.Count; b++)
            {
                ((ImageButton)grid_Course.Rows[b].FindControl("img_update")).Visible = false;
            }
            ((System.Web.UI.Control)grid_Course.HeaderRow.FindControl("lbledit")).Visible = false;
        }
        rdredit.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and  section_delete='No'", _connection);
        SqlDataReader rdrdel = cmd.ExecuteReader();
        if (rdrdel.Read())
        {
           // ((ImageButton)grid_Course.Rows[0].FindControl("img_update")).Visible = false;
            for (int a = 0; a < grid_Course.Rows.Count; a++)
            {
                ((ImageButton)grid_Course.Rows[a].FindControl("imgdel")).Visible = false;
            }
            ((System.Web.UI.Control)grid_Course.HeaderRow.FindControl("lbldel")).Visible = false;
        }
        rdrdel.Close();
        
    }

    public void load1()
    {
        CourseList = employee.fn_course1(employee.BranchId);

        if (CourseList.Count > 0)
        {
            grid_Course.DataSource = CourseList;
            grid_Course.DataBind();
        }
        else
        {
            CourseList = employee.fn_EmptycourseList(employee);

            if (CourseList.Count > 0)
            {
                grid_Course.DataSource = CourseList;
                grid_Course.DataBind();

                ((ImageButton)grid_Course.Rows[0].FindControl("img_update")).Visible = true;
                ((ImageButton)grid_Course.Rows[0].FindControl("imgdel")).Visible = true;
            }

        }

    }

    protected void Edit(object sender, GridViewEditEventArgs e)
    {
        try
        {

            employee.CourseId = Convert.ToInt32(grid_Course.DataKeys[e.NewEditIndex].Value);
            employee.CourseName = ((HtmlInputText)grid_Course.Rows[e.NewEditIndex].FindControl("txtgrid")).Value;

            if (employee.CourseName != "")
            {
                if (employee.CourseName == cname)
                {
                    ((LinkButton)grid_Course.Rows[e.NewEditIndex].FindControl("img_update")).Visible = true;
                    ((LinkButton)grid_Course.Rows[e.NewEditIndex].FindControl("img_save")).Visible = false;
                    ((HtmlInputText)grid_Course.Rows[e.NewEditIndex].FindControl("txtgrid")).Disabled = true;
                    ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Updated Successfully');", true);
                    cname = "";
                    return;
                }
                check = 0;
                check = name_validate(employee.CourseName);
                if (check == 0)
                {

                    _Value = employee.fn_Update_course(employee);
                    if (_Value == "0")
                    {
                        ((LinkButton)grid_Course.Rows[e.NewEditIndex].FindControl("img_update")).Visible = true;
                        ((LinkButton)grid_Course.Rows[e.NewEditIndex].FindControl("img_save")).Visible = false;
                        ((HtmlInputText)grid_Course.Rows[e.NewEditIndex].FindControl("txtgrid")).Disabled = true;
                       // ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Updated Successfully');", true);
                        ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Updated Successfully');", true);
                    }
                    else
                    {
                       // ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
                        ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Course Name already exist');", true);
                    }

                }
                else
                {

                    //ClientScriptManager manager = Page.ClientScript;
                    //manager.RegisterStartupScript(this.GetType(), "Call", "show_message();", true);
                    ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Course Name Exist');", true);
                }
            }
            else
            {
                //ClientScriptManager manager = Page.ClientScript;
                //manager.RegisterStartupScript(this.GetType(), "Call", "show_Error();", true);
                ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Enter Course Name');", true);
            }
        }

        catch (Exception ex)
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
            ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }


    }

    //protected void Button1_Click1(object sender, EventArgs e)
    //{


    //}

    protected void Update(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            cname = "";
          HtmlInputText textgrid = ((HtmlInputText)grid_Course.Rows[e.RowIndex].FindControl("txtgrid"));
            ((LinkButton)grid_Course.Rows[e.RowIndex].FindControl("img_save")).Visible = true;
            ((LinkButton)grid_Course.Rows[e.RowIndex].FindControl("img_update")).Visible = false;
            textgrid.Disabled = false;
            textgrid.Attributes.Add("style", "font-color:blue");
            textgrid.Attributes.Add("style", "width:500px");//textbox width
            cname = textgrid.Value;
        }
        catch (Exception ex)
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
            ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }

    }

    public int name_validate(string m_name)
    {
        CourseList = employee.fn_course1(employee.BranchId);
        if (CourseList.Count > 0)
        {
            for (valid = 0; valid < CourseList.Count; valid++)
            {
                if (CourseList[valid].CourseName.ToLower().Trim() == m_name.ToLower().Trim())//|| m_name == ""
                {
                    temp_valid++;
                }
            }
        }
        return temp_valid;
    }

    public int name_validate_Update(string m_name)
    {
        CourseList = employee.fn_course1(employee.BranchId);
        if (CourseList.Count > 0)
        {
            for (valid = 0; valid < CourseList.Count; valid++)
            {
                if (CourseList[valid].CourseName.ToLower() == m_name.ToLower())//|| m_name == ""
                {
                    temp_valid++;
                }
            }
        }
        return temp_valid;
    }





    protected void grid_Course_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        try
        {
            if (CourseName.Value != "")
            {
                check = name_validate(CourseName.Value);
                if (check == 0)
                {
                    employee.CourseId = Convert.ToInt32(hCourseID.Value);
                    employee.CourseName = CourseName.Value;
                    employee.status = 'Y';
                    _connection = con.fn_Connection();
                    _connection.Open();
                    //cmd = new SqlCommand("select count(*) from hrmm_course", _connection);
                    //int cc = (int)cmd.ExecuteScalar();
                    //cc++;
                    cmd1 = new SqlCommand("select count(*) from hrmm_course", _connection);
                    int cc = (int)cmd1.ExecuteScalar();
                    cmd = new SqlCommand("insert into hrmm_course values('" + employee.CompanyId + "', '" + employee.CourseName + "','" + employee.status + "', '" + employee.BranchId + "')", _connection);
                    cmd.ExecuteNonQuery();
                    cmd1 = new SqlCommand("select count(*) from hrmm_course", _connection);
                    int aa = (int)cmd1.ExecuteScalar();
                    _connection.Close();

                    // _Value = employee.courseUpdate(employee);
                    if (cc != aa)
                    {
                        //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Added Successfully');", true);
                        ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Added Successfully');", true);
                        CourseName.Value = "";
                    }
                    else
                    {
                        //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
                        ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Error Occured');", true);
                    }
                    CourseList = employee.fn_course1(employee.BranchId);

                    if (CourseList.Count > 0)
                    {
                        grid_Course.DataSource = CourseList;
                        grid_Course.DataBind();
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Course Name already Exist');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Enter Course Name');", true);
            }
        }
        catch (Exception ex)
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
            ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

    protected void grid_Course_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            try
            { 

                //finding row index

                GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);//catching the row in which the link button is clicked.
                int rowindex = gvrow.RowIndex;
                
                HtmlInputText lnkbtn = (HtmlInputText)gvrow.FindControl("txtgrid");
                string str = lnkbtn.Value;
                mycon.Open();
                SqlCommand cmd = new SqlCommand("delete from hrmm_Course where v_CourseName='" + str + "' and branchid='" + employee.BranchId + "'", mycon);
                cmd.ExecuteNonQuery();
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Course deleted successfully');", true);
                ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Course deleted successfully');", true);
                load1();
            }
            catch (Exception exc)
            {
                ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Course already assigned');", true);
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Course already assigned to employees');", true);           
            }
        }
        mycon.Close();
    }

    protected void ddl_branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (s_login_role == "a")
        {
            employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
        }
        load1();
    }
    protected void grid_Course_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}
