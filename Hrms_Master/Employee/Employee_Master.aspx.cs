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
using System.Drawing;
using System.Data.SqlClient;

public partial class Hrms_Master_Default : System.Web.UI.Page
{
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    Company company = new Company();
    Employee employee = new Employee();
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    SqlDataAdapter ad = new SqlDataAdapter();
    PayRoll pay = new PayRoll();
    DataSet ds = new DataSet();
    SqlDataReader rea;

    Collection<Employee> DepartmentList;

    public string Assetname="";
    public string OverHeadingName="";
    public string JobStatusName="";
    public string CategoryName="";
    public string GradeName="";
    public string LevelName="";
    public string DivisionName="";
    public string DesignationName="";
    public string DepartmentName="";
    int  valid, temp_valid = 0, check;
    string _Value;
    string s_login_role;

    string s_form = "";
    DataSet ds_userrights;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        
        if (!IsPostBack)
        {
            switch (s_login_role)
            {
                case "a":
                    load_admin();
                    ddl_branch.Visible = true;
                    break;
                case "h":
                    load();
                    ddl_branch.Visible = false;
                    access();
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
        load_ast();
        load_cat();
        load_dept();
        load_desg();
        load_div();
        load_grd();
        load_jt();
        load_lvl();
        load_oc();
    }
    
    public void load_admin()
    {
        myConnection.Open();
        SqlDataAdapter ad = new SqlDataAdapter("select * from paym_branch", myConnection);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        ddl_branch.DataTextField = "branchname";
        ddl_branch.DataValueField = "pn_branchid";
        ddl_branch.DataSource = ds;
        ddl_branch.DataBind();
        ddl_branch.Items.Insert(0, "select Branch");
        myConnection.Close();
    }
    public void load_dept()
    {
        string Qry = "SELECT * FROM paym_department where pn_branchID='" + employee.BranchId + "' and pn_companyID = '" + employee.CompanyId + "' order by v_DepartmentName asc";
        myConnection.Open();
        ad = new SqlDataAdapter(Qry, myConnection);
        ds = new DataSet();
        ad.Fill(ds, "Paym_Department");

        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            Gv_Dept.DataSource = ds;
            Gv_Dept.DataBind();
            int columnCount = Gv_Dept.Rows[0].Cells.Count;
            Gv_Dept.Rows[0].Cells.Clear();
            Gv_Dept.Rows[0].Cells.Add(new TableCell());
            Gv_Dept.Rows[0].Cells[0].ColumnSpan = columnCount;
            Gv_Dept.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            Gv_Dept.DataSource = ds;
            Gv_Dept.DataBind();

        }
        myConnection.Close();
    }
    public void load_desg()
    {
        ad = new SqlDataAdapter("SELECT * FROM paym_designation where branchID='" + employee.BranchId + "' and pn_companyID = '" + employee.CompanyId + "' order by v_DesignationName asc", myConnection);
        ds = new DataSet();
        ad.Fill(ds, "paym_designation");

        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            Gv_Desg.DataSource = ds;
            Gv_Desg.DataBind();
            int columnCount = Gv_Desg.Rows[0].Cells.Count;
            Gv_Desg.Rows[0].Cells.Clear();
            Gv_Desg.Rows[0].Cells.Add(new TableCell());
            Gv_Desg.Rows[0].Cells[0].ColumnSpan = columnCount;
            Gv_Desg.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            Gv_Desg.DataSource = ds;
            Gv_Desg.DataBind();
        }
    }
    public void load_div()
    {
        ad = new SqlDataAdapter("SELECT * FROM paym_division where branchID='" + employee.BranchId + "' and pn_companyID = '" + employee.CompanyId + "' order by v_DivisionName asc", myConnection);
        ds = new DataSet();
        ad.Fill(ds, "paym_division");

        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            Gv_Div.DataSource = ds;
            Gv_Div.DataBind();
            int columnCount = Gv_Div.Rows[0].Cells.Count;
            Gv_Div.Rows[0].Cells.Clear();
            Gv_Div.Rows[0].Cells.Add(new TableCell());
            Gv_Div.Rows[0].Cells[0].ColumnSpan = columnCount;
            Gv_Div.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            Gv_Div.DataSource = ds;
            Gv_Div.DataBind();
        }
    }
    public void load_lvl()
    {
        ad = new SqlDataAdapter("SELECT * FROM paym_level where branchID='" + employee.BranchId + "' and pn_companyID = '" + employee.CompanyId + "' order by v_LevelName asc", myConnection);
        ds = new DataSet();
        ad.Fill(ds, "paym_level");

        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            Gv_Lvl.DataSource = ds;
            Gv_Lvl.DataBind();
            int columnCount = Gv_Lvl.Rows[0].Cells.Count;
            Gv_Lvl.Rows[0].Cells.Clear();
            Gv_Lvl.Rows[0].Cells.Add(new TableCell());
            Gv_Lvl.Rows[0].Cells[0].ColumnSpan = columnCount;
            Gv_Lvl.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            Gv_Lvl.DataSource = ds;
            Gv_Lvl.DataBind();
        }
    }


    public void load_cat()
    {
        ad = new SqlDataAdapter("SELECT * FROM paym_category where branchID='" + employee.BranchId + "' and pn_companyID = '" + employee.CompanyId + "' order by v_CategoryName asc", myConnection);
        ds = new DataSet();
        ad.Fill(ds, "paym_category");

        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            Gv_Cat.DataSource = ds;
            Gv_Cat.DataBind();
            int columnCount = Gv_Cat.Rows[0].Cells.Count;
            Gv_Cat.Rows[0].Cells.Clear();
            Gv_Cat.Rows[0].Cells.Add(new TableCell());
            Gv_Cat.Rows[0].Cells[0].ColumnSpan = columnCount;
            Gv_Cat.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            Gv_Cat.DataSource = ds;
            Gv_Cat.DataBind();
        }
    }
    public void load_grd()
    {
        ad = new SqlDataAdapter("SELECT * FROM paym_grade where branchID='" + employee.BranchId + "' and pn_companyID = '" + employee.CompanyId + "' order by pn_gradeid asc", myConnection);
        ds = new DataSet();
        ad.Fill(ds, "paym_grade");
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            Gv_Grd.DataSource = ds;
            Gv_Grd.DataBind();
            int columnCount = Gv_Grd.Rows[0].Cells.Count;
            Gv_Grd.Rows[0].Cells.Clear();
            Gv_Grd.Rows[0].Cells.Add(new TableCell());
            Gv_Grd.Rows[0].Cells[0].ColumnSpan = columnCount;
            Gv_Grd.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            Gv_Grd.DataSource = ds;
            Gv_Grd.DataBind();
        }
    }
    public void load_jt()
    {
        ad = new SqlDataAdapter("SELECT * FROM paym_jobstatus where branchID='" + employee.BranchId + "' and pn_companyID = '" + employee.CompanyId + "' order by v_JobStatusName asc", myConnection);
        ds = new DataSet();
        ad.Fill(ds, "paym_jobstatus");

        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            Gv_Jt.DataSource = ds;
            Gv_Jt.DataBind();
            int columnCount = Gv_Jt.Rows[0].Cells.Count;
            Gv_Jt.Rows[0].Cells.Clear();
            Gv_Jt.Rows[0].Cells.Add(new TableCell());
            Gv_Jt.Rows[0].Cells[0].ColumnSpan = columnCount;
            Gv_Jt.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            Gv_Jt.DataSource = ds;
            Gv_Jt.DataBind();
        }
    }
    public void load_oc()
    {
        ad = new SqlDataAdapter("SELECT * FROM paym_overheadingcost where branchID='" + employee.BranchId + "' and pn_companyID = '" + employee.CompanyId + "' order by OverHeadingName asc", myConnection);
        ds = new DataSet();
        ad.Fill(ds, "paym_overheadingcost");

        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            Gv_Oc.DataSource = ds;
            Gv_Oc.DataBind();
            int columnCount = Gv_Oc.Rows[0].Cells.Count;
            Gv_Oc.Rows[0].Cells.Clear();
            Gv_Oc.Rows[0].Cells.Add(new TableCell());
            Gv_Oc.Rows[0].Cells[0].ColumnSpan = columnCount;
            Gv_Oc.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            Gv_Oc.DataSource = ds;
            Gv_Oc.DataBind();
        }
    }
    public void load_ast()
    {
        ad = new SqlDataAdapter("SELECT * FROM assets where branchID='" + employee.BranchId + "' and pn_companyID = '" + employee.CompanyId + "' order by Asset_Name asc", myConnection);
        ds = new DataSet();
        ad.Fill(ds, "assets");

        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            Gv_Ast.DataSource = ds;
            Gv_Ast.DataBind();
            int columnCount = Gv_Ast.Rows[0].Cells.Count;
            Gv_Ast.Rows[0].Cells.Clear();
            Gv_Ast.Rows[0].Cells.Add(new TableCell());
            Gv_Ast.Rows[0].Cells[0].ColumnSpan = columnCount;
            Gv_Ast.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            Gv_Ast.DataSource = ds;
            Gv_Ast.DataBind();
        }
    }

    public void access()
    {
       
    }

    public void load1()
    {

    }


    
    public int name_validate(string m_name)
    {
        DepartmentList = employee.fn_Department1(employee);

        if (DepartmentList.Count > 0)
        {
            for (valid = 0; valid < DepartmentList.Count; valid++)
            {
                if (DepartmentList[valid].DepartmentName == m_name)
                {
                    temp_valid++;
                }
            }
        }
        return temp_valid;
    }
    protected void ddl_branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (s_login_role == "a")
        {
            employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
        }
        load();
    }

    protected void Gv_Dept_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(Gv_Dept, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to select this row";
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.backgroundColor='LightSteelBlue'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#F7F6F3';this.style.textDecoration='none'");
        }
    }

    protected void btn_add_Click(object sender, EventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
            }

            employee.DepartmentId = 0;
            employee.DepartmentName = txt_dept.Text;
            employee.status = 'Y';
            myConnection.Open();
            SqlCommand cmd = new SqlCommand("select * from paym_Department where v_DepartmentName='" + txt_dept.Text + "'", myConnection);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                DepartmentName = dr["v_DepartmentName"].ToString();
            }
            myConnection.Close();
            if (txt_dept.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter Department Name!');", true);

            }
            else if (txt_dept.Text.ToUpper() == DepartmentName.ToUpper())
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Department Name Already Available!');", true);
            }
            else
            {
                _Value = employee.DepartmentUpdate(employee);
                if (_Value != "1")
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Department Name Added Successfully!');", true);
                txt_dept.Text = "";
            }
            load(); 
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }
    protected void Btn_del_Click(object sender, EventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
               
                employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
            }
            myConnection.Open();
            SqlCommand cmd = new SqlCommand("delete from paym_department where pn_departmentID='" + ViewState["HiddenID"].ToString() + "' and pn_branchid='" + employee.BranchId + "'", myConnection);
            cmd.ExecuteNonQuery();
            myConnection.Close();
            txt_dept.Text = "";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Department Deleted Successfully!');", true);
            load();
        }

        catch (SqlException exc)
        {
            if (exc.Number == 547)
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Unable to Delete. Transaction Exists!');", true);
            else
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
        }
    }
    protected void Gv_Desg_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(Gv_Desg, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to select this row";
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.backgroundColor='LightSteelBlue'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#F7F6F3';this.style.textDecoration='none'");
        }
    }

    protected void btn_adddesg_Click(object sender, EventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
            }
            employee.DesignationId = 0;
            employee.DesignationName = txt_desg.Text;
            employee.status = 'Y';
            myConnection.Open();
            SqlCommand cmd = new SqlCommand("select * from paym_Designation where v_DesignationName='" + txt_desg.Text + "'", myConnection);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                DesignationName = dr["v_DesignationName"].ToString();
            }
            myConnection.Close();
            if (txt_desg.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter Designation Name!');", true);

            }
            else if (txt_desg.Text.ToUpper() == DesignationName.ToUpper())
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Designation Name Already Available!');", true);
            }
            else
            {
                _Value = employee.DesignationUpdate(employee);
                if (_Value != "1")
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Designation Name Added Successfully!');", true);
                txt_desg.Text = "";
            }
            load();           
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
        }
    }
    protected void Btn_deldesg_Click(object sender, EventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
            }
            myConnection.Open();
            SqlCommand cmd = new SqlCommand("delete from paym_designation where pn_designationID='" + ViewState["HiddenID"].ToString() + "' and branchid='" + employee.BranchId + "'", myConnection);
            cmd.ExecuteNonQuery();
            txt_desg.Text = "";
            myConnection.Close();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Designation Deleted Successfully!');", true);
            load();
        }

        catch (Exception exc)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
        }
    }

    protected void Gv_Div_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(Gv_Div, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to select this row";
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.backgroundColor='LightSteelBlue'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#F7F6F3';this.style.textDecoration='none'");
        }
    }
    protected void Gv_Div_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in Gv_Div.Rows)
        {
            if (row.RowIndex == Gv_Div.SelectedIndex)
            {
                txt_div.Text = (Gv_Div.SelectedRow.FindControl("lbl_Dvname") as Label).Text;
                ViewState["HiddenID"] = Convert.ToInt32((Gv_Div.SelectedRow.FindControl("lbl_Dvid") as Label).Text);
                row.BackColor = ColorTranslator.FromHtml("LightSteelBlue");
                row.ToolTip = string.Empty;
            }
            else
            {
                row.BackColor = ColorTranslator.FromHtml("#F7F6F3");
            }
        }
    }
    protected void btn_adddiv_Click(object sender, EventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
            }
            employee.DivisionId = 0;
            employee.DivisionName = txt_div.Text;
            employee.status = 'Y';
            myConnection.Open();
            SqlCommand cmd = new SqlCommand("select * from paym_Division where v_DivisionName='" + txt_div.Text + "'", myConnection);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                DivisionName = dr["v_DivisionName"].ToString();
            }
            myConnection.Close();
            if (txt_div.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter Division Name!');", true);

            }
            else if (txt_div.Text.ToUpper() == DivisionName.ToUpper())
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Division Name Already Available!');", true);
            }
            else
            {
                _Value = employee.DivisionUpdate(employee);
                if (_Value != "1")
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Division Name Added Successfully!');", true);
                txt_div.Text = "";
            }
            load();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
        }
    }
    protected void Btn_deldiv_Click(object sender, EventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
            }
            if (txt_div.Text =="")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Value to Delete!');", true);
            }
            else
            {
                myConnection.Open();
                SqlCommand cmd = new SqlCommand("delete from paym_division where pn_divisionID='" + ViewState["HiddenID"].ToString() + "' and branchid='" + employee.BranchId + "'", myConnection);
                cmd.ExecuteNonQuery();
                txt_div.Text = "";
                myConnection.Close();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Division Deleted Successfully!');", true);
                load();
            }
        }
        catch (Exception exc)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
        }
    }
    protected void Gv_Lvl_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(Gv_Lvl, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to select this row";
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.backgroundColor='LightSteelBlue'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#F7F6F3';this.style.textDecoration='none'");
        }
    }
    protected void Gv_Lvl_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in Gv_Lvl.Rows)
        {
            if (row.RowIndex == Gv_Lvl.SelectedIndex)
            {
                txt_lvl.Text = (Gv_Lvl.SelectedRow.FindControl("lbl_Lname") as Label).Text;
                ViewState["HiddenID"] = Convert.ToInt32((Gv_Lvl.SelectedRow.FindControl("lbl_Lid") as Label).Text);
                row.BackColor = ColorTranslator.FromHtml("LightSteelBlue");
                row.ToolTip = string.Empty;
            }
            else
            {
                row.BackColor = ColorTranslator.FromHtml("#F7F6F3");
            }
        }
    }
    protected void btn_addlvl_Click(object sender, EventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
            }
            employee.LevelId = 0;
            employee.LevelName = txt_lvl.Text;
            employee.status = 'Y';
            myConnection.Open();
            SqlCommand cmd = new SqlCommand("select * from paym_Level where v_LevelName='" + txt_lvl.Text + "'", myConnection);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                LevelName = dr["v_LevelName"].ToString();
            }
            myConnection.Close();            
            if (txt_lvl.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter Level Name!');", true);

            }
            else if (txt_lvl.Text.ToUpper() == LevelName.ToUpper())
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Level Name Already Available!');", true);
            }
            else
            {
                _Value = employee.LevelUpdate(employee);
                if (_Value != "1")
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Level Added Successfully!');", true);
                txt_lvl.Text = "";
            }
            load();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
        }
    }
    protected void Btn_dellvl_Click(object sender, EventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
            }
            myConnection.Open();
            SqlCommand cmd = new SqlCommand("delete from paym_level where pn_levelID='" + ViewState["HiddenID"].ToString() + "' and branchid='" + employee.BranchId + "'", myConnection);
            cmd.ExecuteNonQuery();
            txt_lvl.Text = "";
            myConnection.Close();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Level Deleted Successfully!');", true);
            load();
        }

        catch (Exception exc)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
        }
    }
    protected void Gv_Grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(Gv_Grd, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to select this row";
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.backgroundColor='LightSteelBlue'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#F7F6F3';this.style.textDecoration='none'");
        }
    }
    protected void Gv_Grd_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in Gv_Grd.Rows)
        {
            if (row.RowIndex == Gv_Grd.SelectedIndex)
            {
                txt_grd.Text = (Gv_Grd.SelectedRow.FindControl("lbl_Gname") as Label).Text;
                ViewState["HiddenID"] = Convert.ToInt32((Gv_Grd.SelectedRow.FindControl("lbl_Gid") as Label).Text);
                row.BackColor = ColorTranslator.FromHtml("LightSteelBlue");
                row.ToolTip = string.Empty;
            }
            else
            {
                row.BackColor = ColorTranslator.FromHtml("#F7F6F3");
            }
        }
    }
    protected void btn_addgrd_Click(object sender, EventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
            }

            employee.GradeId = 0;
            employee.GradeName = txt_grd.Text;
            employee.status = 'Y';
            myConnection.Open();
            SqlCommand cmd = new SqlCommand("select * from paym_Grade where v_GradeName='" + txt_grd.Text + "'", myConnection);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                GradeName = dr["v_GradeName"].ToString();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('');", true);
            }
            myConnection.Close();
            if (txt_grd.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter Grade Name!');", true);

            }
            else if (txt_grd.Text.ToUpper() == GradeName.ToUpper())
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Grade Name Already Available!');", true);
            }
            else
            {
                _Value = employee.GradeUpdate(employee);
                if (_Value != "1")
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Grade Name Added Successfully!');", true);
                txt_grd.Text = "";
            }
            load();             
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
        }
    }
    protected void Btn_delgrd_Click(object sender, EventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
            }
            myConnection.Open();
            SqlCommand cmd = new SqlCommand("delete from paym_grade where pn_gradeID='" + ViewState["HiddenID"].ToString() + "' and branchid='" + employee.BranchId + "'", myConnection);
            cmd.ExecuteNonQuery();
            txt_grd.Text = "";
            myConnection.Close();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Grade Deleted Successfully!');", true);
            load();
        }

        catch (Exception exc)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
        }
    }
    protected void Gv_Cat_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(Gv_Cat, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to select this row";
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.backgroundColor='LightSteelBlue'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#F7F6F3';this.style.textDecoration='none'");
        }
    }
    protected void Gv_Cat_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in Gv_Cat.Rows)
        {
            if (row.RowIndex == Gv_Cat.SelectedIndex)
            {
                txt_cat.Text = (Gv_Cat.SelectedRow.FindControl("lbl_Cname") as Label).Text;
                ViewState["HiddenID"] = Convert.ToInt32((Gv_Cat.SelectedRow.FindControl("lbl_Cid") as Label).Text);
                row.BackColor = ColorTranslator.FromHtml("LightSteelBlue");
                row.ToolTip = string.Empty;
            }
            else
            {
                row.BackColor = ColorTranslator.FromHtml("#F7F6F3");
            }
        }
    }
    protected void btn_addcat_Click(object sender, EventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
            }
            employee.CategoryId = 0;
            employee.CategoryName = txt_cat.Text;
            employee.status = 'Y';
            myConnection.Open();
            SqlCommand cmd = new SqlCommand("select * from paym_Category where v_CategoryName='" + txt_cat.Text + "'", myConnection);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                CategoryName = dr["v_CategoryName"].ToString();
            }
            myConnection.Close();
            if (txt_cat.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter Category Name!');", true);

            }
            else if (txt_cat.Text.ToUpper() == CategoryName.ToUpper())
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Category Name Already Available!');", true);
            }
            else
            {
                _Value = employee.CategoryUpdate(employee);
                if (_Value != "1")
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Category Added Successfully!');", true);
                txt_cat.Text = "";
            }
            load();  
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
        }
    }
    protected void Btn_delcat_Click(object sender, EventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
            }
            myConnection.Open();
            SqlCommand cmd = new SqlCommand("delete from paym_category where pn_categoryID='" + ViewState["HiddenID"].ToString() + "' and branchid='" + employee.BranchId + "'", myConnection);
            cmd.ExecuteNonQuery();            
            txt_cat.Text = "";
            myConnection.Close();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Category Deleted Successfully!');", true);
            load();
        }

        catch (Exception exc)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
        }
    }
    protected void Gv_Jt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(Gv_Jt, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to select this row";
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.backgroundColor='LightSteelBlue'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#F7F6F3';this.style.textDecoration='none'");
        }
    }
    protected void Gv_Jt_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in Gv_Jt.Rows)
        {
            if (row.RowIndex == Gv_Jt.SelectedIndex)
            {
                txt_jt.Text = (Gv_Jt.SelectedRow.FindControl("lbl_Jname") as Label).Text;
                ViewState["HiddenID"] = Convert.ToInt32((Gv_Jt.SelectedRow.FindControl("lbl_Jid") as Label).Text);
                row.BackColor = ColorTranslator.FromHtml("LightSteelBlue");
                row.ToolTip = string.Empty;
            }
            else
            {
                row.BackColor = ColorTranslator.FromHtml("#F7F6F3");
            }
        }
    }
    protected void btn_addjt_Click(object sender, EventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
            }
            employee.JobStatusId = 0;
            employee.JobStatusName = txt_jt.Text;
            employee.status = 'Y';
            myConnection.Open();
            SqlCommand cmd = new SqlCommand("select * from paym_JobStatus where v_JobStatusName='" + txt_jt.Text + "'", myConnection);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                JobStatusName = dr["v_JobStatusName"].ToString();
            }
            myConnection.Close();

            if (txt_jt.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter JobStatus Name!');", true);

            }
            else if (txt_jt.Text.ToUpper() == JobStatusName.ToUpper())
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('JobStatus Name Already Available!');", true);
            }
            else
            {
                _Value = employee.JobStatusUpdate(employee);
                if (_Value != "1")
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Job Type Added Successfully!');", true);
                txt_jt.Text = "";
            }
            load();           
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
        }
    }
    protected void Btn_deljt_Click(object sender, EventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
            }
            myConnection.Open();
            SqlCommand cmd = new SqlCommand("delete from paym_jobstatus where pn_jobstatusID='" + ViewState["HiddenID"].ToString() + "' and branchid='" + employee.BranchId + "'", myConnection);
            cmd.ExecuteNonQuery();
            myConnection.Close();
            txt_jt.Text = "";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Job Type Deleted Successfully!');", true);
            load();
        }

        catch (Exception exc)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
        }
    }
    protected void Gv_Oc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(Gv_Oc, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to select this row";
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.backgroundColor='LightSteelBlue'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#F7F6F3';this.style.textDecoration='none'");
        }
    }
    protected void Gv_Oc_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in Gv_Oc.Rows)
        {
            if (row.RowIndex == Gv_Oc.SelectedIndex)
            {
                txt_oc.Text = (Gv_Oc.SelectedRow.FindControl("lbl_Oname") as Label).Text;
                ViewState["HiddenID"] = Convert.ToInt32((Gv_Oc.SelectedRow.FindControl("lbl_Oid") as Label).Text);
                row.BackColor = ColorTranslator.FromHtml("LightSteelBlue");
                row.ToolTip = string.Empty;
            }
            else
            {
                row.BackColor = ColorTranslator.FromHtml("#F7F6F3");
            }
        }
    }
    protected void btn_addoc_Click(object sender, EventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
            }
            // check = employee.OverheadingValidate(txt_oc.Text.ToLower());
            employee.OverHeadingCostId = 0;
            employee.OverHeadingCostName = txt_oc.Text;
            employee.status = 'Y';
            myConnection.Open();
            SqlCommand cmd = new SqlCommand("select * from paym_overheadingcost where OverHeadingName='" + txt_oc.Text + "'", myConnection);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                OverHeadingName = dr["OverHeadingName"].ToString();
            }
            myConnection.Close();

            if (txt_oc.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter Overheading Name!');", true);

            }
            else if (txt_oc.Text.ToUpper() == OverHeadingName.ToUpper())
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Overheading Name Already Available!');", true);
            }
            else
            {
                _Value = employee.OverHeadingCostUpdate(employee);
                if (_Value != "1")
             
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Overheading Cost Added Successfully!');", true);
                txt_oc.Text = "";
            }
            load();
            }

             catch (Exception ex)
               {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
                }
            }            
    
    protected void Btn_deloc_Click(object sender, EventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
            }
            myConnection.Open();
            SqlCommand cmd = new SqlCommand("delete from paym_OverHeadingCost where overHeadingID='" + ViewState["HiddenID"].ToString() + "' and branchid='" + employee.BranchId + "'", myConnection);
            cmd.ExecuteNonQuery();
            myConnection.Close();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Ove Heading Cost Deleted Successfully!');", true);
            txt_oc.Text = "";
            load();
        }
        catch (Exception exc)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
        }
    }
    protected void Gv_Ast_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(Gv_Ast, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to select this row";
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.backgroundColor='LightSteelBlue'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#F7F6F3';this.style.textDecoration='none'");
        }
    }
    protected void Gv_Ast_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in Gv_Ast.Rows)
        {
            if (row.RowIndex == Gv_Ast.SelectedIndex)
            {
                txt_ast.Text = (Gv_Ast.SelectedRow.FindControl("lbl_Aname") as Label).Text;
                ViewState["HiddenID"] = Convert.ToInt32((Gv_Ast.SelectedRow.FindControl("lbl_Aid") as Label).Text);
                row.BackColor = ColorTranslator.FromHtml("LightSteelBlue");
                row.ToolTip = string.Empty;
            }
            else
            {
                row.BackColor = ColorTranslator.FromHtml("#F7F6F3");
            }
        }
    }

    protected void btn_addast_Click(object sender, EventArgs e)
    {
        employee.AssetId = 0;
        employee.AssetName = txt_ast.Text.ToLower();
        myConnection.Open();
            SqlCommand cmd =new SqlCommand("select * from Assets where Asset_name='" +txt_ast.Text + "'",myConnection);
            SqlDataReader dr=cmd.ExecuteReader();
             while (dr.Read())
            {
                 Assetname=dr["Asset_name"].ToString();           
            }
        myConnection.Close();

        if (txt_ast.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter Asset!');", true);           

        }
       else if(txt_ast.Text.ToUpper()==Assetname.ToUpper())
        { 
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Assets Name Already Available!');", true);           
        }
        else
        {
           myConnection.Open();
            string query = "insert into Assets(pn_CompanyID,BranchID,Asset_name)values('" + employee.CompanyId + "','" + employee.BranchId + "','" + employee.AssetName + "')";
            cmd = new SqlCommand(query, myConnection);
            cmd.ExecuteNonQuery();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Asset Added Successfully!');", true);
            txt_ast.Text = "";
            myConnection.Close();
         }
        load();
        //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Asset Name Already Exist!');", true);
    }
           
    
    protected void Btn_delast_Click(object sender, EventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
            }
            myConnection.Open();
            SqlCommand cmd = new SqlCommand("delete from assets where pn_AssetID='" + ViewState["HiddenID"].ToString() + "' and branchid='" + employee.BranchId + "'", myConnection);
            cmd.ExecuteNonQuery();
            txt_ast.Text = "";
            myConnection.Close();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Asset Deleted Successfully!');", true);
            load();
        }

        catch (Exception exc)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
        }
    }
    protected void Gv_Desg_SelectedIndexChanged1(object sender, EventArgs e)
    {
        foreach (GridViewRow row in Gv_Desg.Rows)
        {
            if (row.RowIndex == Gv_Desg.SelectedIndex)
            {
                txt_desg.Text = (Gv_Desg.SelectedRow.FindControl("lbl_Dsname") as Label).Text;
                ViewState["HiddenID"] = Convert.ToInt32((Gv_Desg.SelectedRow.FindControl("lbl_Dsid") as Label).Text);
                row.BackColor = ColorTranslator.FromHtml("LightSteelBlue");
                row.ToolTip = string.Empty;
            }
            else
            {
                row.BackColor = ColorTranslator.FromHtml("#F7F6F3");
            }
        }
    }

    protected void Dept_update(object sender, EventArgs e)
    {
        foreach (GridViewRow row in Gv_Dept.Rows)
        {
            if (row.RowIndex == Gv_Dept.SelectedIndex)
            {
                txt_dept.Text = (Gv_Dept.SelectedRow.FindControl("lbl_Dname") as Label).Text;
                ViewState["HiddenID"] = Convert.ToInt32((Gv_Dept.SelectedRow.FindControl("lbl_Did") as Label).Text);
                row.BackColor = ColorTranslator.FromHtml("LightSteelBlue");
                row.ToolTip = string.Empty;
            }
            else
            {
                row.BackColor = ColorTranslator.FromHtml("#F7F6F3");
            }
        }
    }
    protected void desg_update(object sender, EventArgs e)
    {
        foreach (GridViewRow row in Gv_Desg.Rows)
        {
            if (row.RowIndex == Gv_Desg.SelectedIndex)
            {
                txt_desg.Text = (Gv_Desg.SelectedRow.FindControl("lbl_Dsname") as Label).Text;
                ViewState["HiddenID"] = Convert.ToInt32((Gv_Desg.SelectedRow.FindControl("lbl_Dsid") as Label).Text);
                row.BackColor = ColorTranslator.FromHtml("LightSteelBlue");
                row.ToolTip = string.Empty;
            }
            else
            {
                row.BackColor = ColorTranslator.FromHtml("#F7F6F3");
            }
        }
    }
}