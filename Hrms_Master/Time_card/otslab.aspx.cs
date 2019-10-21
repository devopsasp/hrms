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
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    SqlDataReader rea;
    Company company = new Company();

    Employee employee = new Employee();

    Collection<Company> BranchsList;
    Collection<Employee> CourseList;
    Collection<Company> CompanyList;
    Collection<PayRoll> OtList;

    PayRoll pay = new PayRoll();
    int company_Id, branch_Id, valid, temp_valid = 0, check = 0;
    string _Value;
    string s_login_role;
    string s_form = "";
    DataSet ds_userrights;

   

    protected void Page_Load(object sender, EventArgs e)
    {
        
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        if (s_login_role == "a")
        {
            employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
        }
        if (!Page.IsPostBack)
        {

            switch (s_login_role)
            {
                case "a": 
                    load_admin();
                    //ddl_category.Visible = false;
                    break;

                case "h":
                    ddl_branch.Visible = false;

                    load_page();
                    access();
                    category();
                    ddl_load();   
                    break;

                case "u": s_form = "6";
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
        SqlDataAdapter ad = new SqlDataAdapter();
        string cat = ((DropDownList)GridView1.HeaderRow.FindControl("ddl_category")).SelectedItem.Text;
        if (cat != "All Category" && cat != "Select")
        {
            if (s_login_role == "a")
            {
                ad = new SqlDataAdapter("SELECT * FROM otslab where pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + ddl_branch.SelectedItem.Value + "' order by slab_id asc ", myConnection);
            }

            if (s_login_role == "h")
            {
                ad = new SqlDataAdapter("SELECT * FROM otslab where pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' and pn_category='" + cat + "' order by slab_id asc ", myConnection);
            }
        }
        else
        {
            if (s_login_role == "a")
            {
                ad = new SqlDataAdapter("SELECT * FROM otslab where pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + ddl_branch.SelectedItem.Value + "'  order by slab_id asc ", myConnection);
            }

            if (s_login_role == "h")
            {
                ad = new SqlDataAdapter("SELECT * FROM otslab where pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' order by slab_id asc ", myConnection);
            }
        }

        DataSet ds = new DataSet();

        ad.Fill(ds, "otslab");

        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView1.DataSource = ds;
            GridView1.DataBind();
            int columnCount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columnCount;
            GridView1.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
    }



    public void load_page()
    {
        GridView1.Attributes.Add("bordercolor", "white");

        SqlDataAdapter ad = new SqlDataAdapter();

        if (s_login_role == "a")
        {
            ad = new SqlDataAdapter("SELECT * FROM otslab where pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + ddl_branch.SelectedItem.Value + "'  order by slab_id asc ", myConnection);
        }

        if (s_login_role == "h")
        {
            ad = new SqlDataAdapter("SELECT * FROM otslab where pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "'  order by slab_id asc ", myConnection);
        }

        DataSet ds = new DataSet();

        ad.Fill(ds, "otslab");


        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView1.DataSource = ds;
            GridView1.DataBind();
            int columnCount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columnCount;
            GridView1.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
    }


    public void load_admin()
    {
        myConnection.Open();

        SqlDataAdapter ad = new SqlDataAdapter("select * from paym_branch", myConnection);
        DataSet ds = new DataSet();
        ad.Fill(ds, "paym_branch");
        ddl_branch.DataSource = ds;
        ddl_branch.DataTextField = "branchname";
        ddl_branch.DataValueField = "pn_branchid";
        ddl_branch.DataBind();

        ddl_branch.Items.Insert(0, "Select Branch");
        myConnection.Close();

        
    }

    public void category()
    {

        myConnection.Open();
        SqlDataAdapter ad = new SqlDataAdapter();

        if (s_login_role == "a")
        {
            ad = new SqlDataAdapter("select distinct pn_category from otslab where pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + ddl_branch.SelectedItem.Value + "'", myConnection);
        }

        if (s_login_role == "h")
        {
            ad = new SqlDataAdapter("select distinct pn_category from otslab where pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + employee.BranchId + "'", myConnection);
        }
        
        DataSet ds = new DataSet();
        ad.Fill(ds, "otslab");
        ((DropDownList)GridView1.HeaderRow.FindControl("ddl_category")).DataSource = ds;
        ((DropDownList)GridView1.HeaderRow.FindControl("ddl_category")).DataTextField = "pn_category";
        ((DropDownList)GridView1.HeaderRow.FindControl("ddl_category")).DataValueField = "pn_category";
        ((DropDownList)GridView1.HeaderRow.FindControl("ddl_category")).DataBind();
        ((DropDownList)GridView1.HeaderRow.FindControl("ddl_category")).Items.Insert(0, "Select");
        ((DropDownList)GridView1.HeaderRow.FindControl("ddl_category")).Items.Insert(1, "All Category");
        myConnection.Close();
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

        }
        rdredit.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and  section_delete='No'", _connection);
        SqlDataReader rdrdel = cmd.ExecuteReader();
        if (rdrdel.Read())
        {

        }
        rdrdel.Close();

    }

    


    protected void Edit(object sender, GridViewEditEventArgs e)
    {
        try
        {

        }

        catch (Exception ex)
        {

        }


    }


    protected void Update(object sender, GridViewUpdateEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            
        }

    }

    private void DeleteRecord(string ID)
    {
        try
        {
            string sqlStatement = "DELETE FROM otslab WHERE slab_id = @slab_id and pn_branchid = '" + employee.BranchId + "'";
            if (s_login_role == "a")
            {
                sqlStatement = "DELETE FROM otslab WHERE slab_id = @slab_id and pn_branchid = '" + ddl_branch.SelectedItem.Value + "'";      
            }
            
            myConnection.Open();
            SqlCommand cmd = new SqlCommand(sqlStatement, myConnection);
            cmd.Parameters.AddWithValue("@slab_id", ID);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }

        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Deletion Error:";
            msg += ex.Message;
            throw new Exception(msg);
        }

        finally
        {
            myConnection.Close();
        }
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        load_page();
        category();
        ddl_load();   
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "add")
        {
            string otfrom = ((TextBox)GridView1.FooterRow.FindControl("txt_otfrom")).Text;
            string otto = ((TextBox)GridView1.FooterRow.FindControl("txt_otto")).Text;
            string otslab = ((TextBox)GridView1.FooterRow.FindControl("txt_otslab")).Text;
            string cat = ((DropDownList)GridView1.FooterRow.FindControl("ddl_catid")).Text;
            if (otfrom == "" || otto == "" || otslab == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter all the Fields');", true);
                return;
            }
            AddNewRecord(otfrom, otto, otslab, cat);
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // loop all data rows
            foreach (DataControlFieldCell cell in e.Row.Cells)
            {
                // check all cells in one row
                foreach (Control control in cell.Controls)
                {
                    // Must use LinkButton here instead of ImageButton
                    // if you are having Links (not images) as the command button.
                    ImageButton button = control as ImageButton;
                    if (button != null && button.CommandName == "Delete")
                        // Add delete confirmation
                        button.OnClientClick = "if (!confirm('Are you sure you want to delete this record?')) return false;";
                }
            }
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string ID = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lbl_id")).Text;
        DeleteRecord(ID);
        load_page();
        ddl_load();
        category();
        access();
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (s_login_role == "a")
        {
            
            GridView1.EditIndex = e.NewEditIndex; // turn to edit mode
            load();
            ddl_load();
            access();
        }
        else if(s_login_role == "h")
        {
            GridView1.EditIndex = e.NewEditIndex; // turn to edit mode
            load();
            ddl_load();
            category();
            access();
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void AddNewRecord(string otfrom, string otto, string otslab, string cat)
    {
        try
        {
            pay.fromtime = TimeSpan.Parse(otfrom);
            pay.totime = TimeSpan.Parse(otto);
            pay.CategoryID = cat;
            double s = 0;
            if (otslab == "00:00")
            {
                s = 0;
            }
            else if (otslab == "00:30")
            {
                s = 0.5;
            }
            else if (otslab == "01:00")
            {
                s = 1;
            }
            else if (otslab == "01:30")
            {
                s = 1.5;
            }
            else if (otslab == "02:00")
            {
                s = 2;
            }
            else if (otslab == "02:30")
            {
                s = 2.5;
            }
            else if (otslab == "03:00")
            {
                s = 3;
            }
            else if (otslab == "03:30")
            {
                s = 3.5;
            }
            else if (otslab == "04:00")
            {
                s = 4;
            }
            else if (otslab == "04:30")
            {
                s = 4.5;
            }
            else if (otslab == "05:00")
            {
                s = 5;
            }

            OtList = pay.fn_In_OTSlab(pay);
            if (OtList.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('The entered values are overlapping the other values');", true);
                return;
            }


            if (cat == "All")
            {
                myConnection.Open();
                SqlDataAdapter cmd = new SqlDataAdapter("Select v_Categoryname from paym_category where BranchID = '" + employee.BranchId + "'", myConnection);
                DataTable dt = new DataTable();
                cmd.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string query = @"INSERT INTO otslab (pn_CompanyID, pn_BranchID, pn_category, ot_from, ot_to, ot_slab, ot_hrs) VALUES('" + employee.CompanyId + "','" + employee.BranchId + "','" + dt.Rows[i][0].ToString() + "','" + otfrom + "','" + otto + "','" + otslab + "','" + s + "')";
                    SqlCommand myCommand = new SqlCommand(query, myConnection);
                    myCommand.ExecuteNonQuery();
                }
                myConnection.Close();
            }
            else
            {
                if (otfrom == "" || otto == "" || otslab== "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter all the Fields');", true);
                    return;
                }
                string query = @"INSERT INTO otslab (pn_CompanyID, pn_BranchID, pn_category, ot_from, ot_to, ot_slab, ot_hrs) VALUES('" + employee.CompanyId + "','" + employee.BranchId + "','" + cat + "','" + otfrom + "','" + otto + "','" + otslab + "','" + s + "')";
                SqlCommand myCommand = new SqlCommand(query, myConnection);
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
            load();
            ddl_load();
            category();
            access();
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Record Already Exist!');", true);
        }
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow Gvrow = GridView1.Rows[e.RowIndex];
        //Label8.Text = Gvrow.ToString();
            string otidedit = ((Label)Gvrow.FindControl("txt_id_edit")).Text;
            string otfromedit = ((TextBox)Gvrow.FindControl("txt_otfrom_edit")).Text;
            string ottoedit = ((TextBox)Gvrow.FindControl("txt_otto_edit")).Text;
            string otslabedit = ((TextBox)Gvrow.FindControl("txt_otslab_edit")).Text;
            
            myConnection.Open();
            if (s_login_role == "a")
            {
                cmd = new SqlCommand("update otslab set ot_from='" + otfromedit + "', ot_to='" + ottoedit + "', ot_slab='" + otslabedit + "' where slab_id='" + otidedit + "' and pn_companyid= '" + employee.CompanyId + "' and  pn_branchid= '" + ddl_branch.SelectedItem.Value + "'", myConnection);
                cmd.ExecuteNonQuery();
                myConnection.Close();
                GridView1.EditIndex = -1; // turn to edit mode
               
            }
            else if (s_login_role == "h")
            {
                cmd = new SqlCommand("update otslab set ot_from='" + otfromedit + "', ot_to='" + ottoedit + "', ot_slab='" + otslabedit + "' where slab_id='" + otidedit + "' and pn_companyid= '" + employee.CompanyId + "' and  pn_branchid= '" + employee.BranchId + "'", myConnection);
                cmd.ExecuteNonQuery();
                myConnection.Close();
                GridView1.EditIndex = -1; // turn to edit mode
                
            }
            load_page();
            ddl_load();   
            category();
    }
        


    protected void ddl_branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        load_page();
        //category();
        ddl_load();
        ((DropDownList)GridView1.HeaderRow.FindControl("ddl_category")).Visible = true;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    
    protected void ddl_category_SelectedIndexChanged1(object sender, EventArgs e)
    {
        var catValue = ((DropDownList)GridView1.HeaderRow.FindControl("ddl_category")).SelectedValue;  
        load();
        ddl_load();
        category();
        ((DropDownList)GridView1.HeaderRow.FindControl("ddl_category")).SelectedValue = catValue;

    }

    public void ddl_load()
    {
        myConnection.Open();
        SqlCommand cmd_ddl = new SqlCommand();
        if (s_login_role == "a")
        {
            cmd_ddl = new SqlCommand("select v_categoryname from paym_category where pn_companyid='" + employee.CompanyId + "' and branchid='" + ddl_branch.SelectedItem.Value + "' ", myConnection);
        }
        if (s_login_role == "h")
        {
            cmd_ddl = new SqlCommand("select v_categoryname from paym_category where pn_companyid='" + employee.CompanyId + "' and branchid='" + employee.BranchId + "' ", myConnection);
        }
        
        
        SqlDataAdapter ddl = new SqlDataAdapter(cmd_ddl);
        DataSet ds= new DataSet();
        ddl.Fill(ds,"paym_category");
        ((DropDownList)GridView1.FooterRow.FindControl("ddl_catid")).DataTextField = "v_categoryname";
        ((DropDownList)GridView1.FooterRow.FindControl("ddl_catid")).DataSource = ds;
        ((DropDownList)GridView1.FooterRow.FindControl("ddl_catid")).DataBind();
        ((DropDownList)GridView1.FooterRow.FindControl("ddl_catid")).Items.Insert(0, "All");
        myConnection.Close();
    }
}


    

            
            
            
            
            
            
            
