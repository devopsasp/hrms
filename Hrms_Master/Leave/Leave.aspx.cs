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
    private SqlConnection _connection;
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    Company company = new Company();
    Leave l = new Leave();
    Employee employee = new Employee();
    Collection<Leave> LeaveList;
    Collection<Company> CompanyList;
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand cmd = new SqlCommand();

    int company_Id, branch_Id, valid, temp_valid = 0, check;
    string _Value;
    string s_login_role;
    string s_form = "";
    DataSet ds_userrights;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        l.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        if (!IsPostBack)
        {
            switch (s_login_role)
            {
                case "a": load_admin();
                    tb_leave.Visible = false;
                  //  tb_leave1.Visible = false;
                    break;

                case "h": 
                    load1();
                    access();
                    ddl_branch.Visible = false;
                    break;

                case "u": s_form = "28";
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
        //lbl_days.Visible = false;
        txt_maxdays.Enabled = false;

        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM paym_leave", myConnection);

        DataSet ds = new DataSet();

        ad.Fill(ds, "paym_leave");


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

    public void access()
    {
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
            //Response.Write("<script language='javascript'>alert('Permission Restricted. Please Contact Administrator.');window.location='~/Company_Home.aspx';</script>");
            //GridView gv1 = FindControl("GridView1") as GridView;
            //gv1.AutoGenerateEditButton = false;
           // this.GridView1.AutoGenerateEditButton = false;
            for (int b = 0; b < GridView1.Rows.Count; b++)
            {
                GridView1.Rows[b].Cells[5].Controls[0].Visible = false;
            }
        }
        rdredit.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and  section_delete='No'", _connection);
        SqlDataReader rdrdel = cmd.ExecuteReader();
        if (rdrdel.Read())
        {
            // ((ImageButton)grid_Course.Rows[0].FindControl("img_update")).Visible = false;
            for (int a = 0; a < GridView1.Rows.Count; a++)
            {
                GridView1.Rows[a].Cells[5].Controls[2].Visible = false;
            }
            //((System.Web.UI.Control)grid_Course.HeaderRow.FindControl("lbldel")).Visible = false;
        }
        rdrdel.Close();

    }

    public void load1()
    {
        //lbl_days.Visible = false;
        txt_maxdays.Enabled = false;

        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM paym_leave where pn_BranchID = '" + employee.BranchId + "'", myConnection);

        DataSet ds = new DataSet();

        ad.Fill(ds, "paym_leave");


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

    public int name_validate1(string d_name, string d_code)
    {
        LeaveList = l.fn_paym_leave1(employee.BranchId);

        if (LeaveList.Count > 0)
        {
            for (valid = 0; valid < LeaveList.Count; valid++)
            {
                if (LeaveList[valid].leaveName.ToLower() == d_name.ToLower() || LeaveList[valid].leaveCode.ToLower() == d_code.ToLower())
                {
                    temp_valid++;
                }
            }
        }
        return temp_valid;
    }

    protected void btn_add_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddl_annualleave.SelectedItem.Text != "Select")
            {
                l.leaveID = 0;
                check = name_validate1(txt_leavename.Value, txt_LeaveCode.Value);
                if (check == 0)
                {
                    save();
                    //LeaveList = l.fn_paym_leave1(employee.BranchId);

                    //if (LeaveList.Count > 0)
                    //{
                    //    load1();
                    //}
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Leave Code / Leave Name Already Exist!');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Choose Leave Options!');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
        }
    }

    public void save()
    {
        l.leaveName = txt_leavename.Value;
        l.leaveCode = txt_LeaveCode.Value;
        l.Count = Convert.ToInt32(txt_count.Value);
        l.status = 'Y';
        l.AnnualLeave = ddl_annualleave.SelectedItem.Text;
        if (txt_maxdays.Text == "")
        {
            l.MaxDays = 0;
        }
        else
        {
            l.MaxDays = Convert.ToInt32(txt_maxdays.Text);
        }
        l.Point = ddl_gender.SelectedItem.Text;
        l.Flag = "Leave";
        if (Chk_onduty.Checked == true)
        {
            l.Flag = "On Duty";
        }
        _Value = l.Leave_First(l);
        if (_Value != "1")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Saved Successfully!');", true);
            myConnection.Open();
            cmd = new SqlCommand("update paym_leave set annual_leave = '" + ddl_annualleave.SelectedItem.Text + "', max_days = '" + txt_maxdays.Text + "', EL = '" + l.Point + "', Type = '" + l.Flag + "' where v_LeaveName = '" + txt_leavename.Value + "' and pn_BranchID = '" + employee.BranchId + "' ", myConnection);
            cmd.ExecuteNonQuery();
            myConnection.Close();
            load1();
            clear();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
        }
    }

    public void clear()
    {
        txt_leavename.Value = "";
        txt_LeaveCode.Value = "";
        txt_count.Value = "";
        ddl_annualleave.SelectedIndex = 0;
        txt_maxdays.Text = "";
        ddl_gender.SelectedIndex = 0;
        Chk_onduty.Checked = false;
        //lbl_days.Visible = false;
        txt_maxdays.Enabled = false;
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
        ddl_branch.Items.Insert(0, "Select Branch");
        myConnection.Close();
    }

    protected void Update(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
           
        }
        catch (Exception ex)
        {
            //lbl_Error.Text = "Error";
        }
    }

    protected void Edit(object sender, GridViewEditEventArgs e)
    {

    }

    protected void btn_add_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            l.leaveID = 0;
            l.leaveName = txt_leavename.Value;
            LeaveList = l.fn_paym_leave(l);

            if (LeaveList.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Leave Name already exist!');", true);
            }
            else
            {
                save();
                load1();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
        }
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        load1();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

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
                        button.OnClientClick = "if (!confirm('Are you sure " +
                               "you want to delete this record?')) return;";
                }
            }
        }
    }
    
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string ID = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lbl_leavecode")).Text;
        DeleteRecord(ID);
        load1();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex; // turn to edit mode
        load1();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow Gvrow = GridView1.Rows[e.RowIndex];
        //Label8.Text = Gvrow.ToString();
        if (Gvrow != null)
        {
            string leavecode = ((Label)Gvrow.FindControl("lbl_leavecode_edit")).Text;
            string leavename = ((TextBox)Gvrow.FindControl("txt_leavename_edit")).Text;
            string count = ((TextBox)Gvrow.FindControl("txt_count_edit")).Text;
            string max = ((TextBox)Gvrow.FindControl("txt_maxdays_edit")).Text;
            string EL = ((DropDownList)Gvrow.FindControl("txt_EL_edit")).Text;
            string type = ((DropDownList)Gvrow.FindControl("txt_Type")).SelectedItem.Text;
            string annualleave = ((DropDownList)Gvrow.FindControl("ddl_annualleave")).Text;

            myConnection.Open();
            cmd = new SqlCommand("update paym_leave set v_leaveName='" + leavename + "', pn_Count ='" + count + "', max_days ='" + max + "', annual_leave ='" + annualleave + "', EL = '" + EL + "', type = '" + type + "' where pn_leaveCode='" + leavecode + "' and pn_BranchID = '" + l.BranchID + "'", myConnection);
            cmd.ExecuteNonQuery();
            myConnection.Close();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Updated Successfully!');", true);
            GridView1.EditIndex = -1; // turn to edit mode
            load1();
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void DeleteRecord(string ID)
    {

        string sqlStatement = "DELETE FROM paym_leave WHERE pn_leaveCode=@pn_leaveCode";
        try
        {
            myConnection.Open();
            SqlCommand cmd = new SqlCommand(sqlStatement, myConnection);
            cmd.Parameters.AddWithValue("@pn_leaveCode", ID);
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
    protected void ddl_annualleave_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_annualleave.SelectedItem.Text == "Carry Forward" || ddl_annualleave.SelectedItem.Text == "Encashment" || ddl_annualleave.SelectedItem.Text == "Carry Forward & Encashment")
        {
            txt_maxdays.Enabled = true;
        }
        
        else
        {
            txt_maxdays.Enabled = false;
        }
    }
    protected void ddl_branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (s_login_role == "a")
        {
            employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
        }
        load1();
        tb_leave1.Visible = true;
        tb_leave.Visible = true;
    }
    protected void Chk_EL_CheckedChanged(object sender, EventArgs e)
    {
        txt_maxdays.Enabled = true;
    }
    protected void txt_maxdays_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(txt_maxdays.Text) > Convert.ToInt32(txt_count.Value))
        {
            txt_maxdays.Text = "";
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Choose Leave Options!');", true);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Max days should not be greater than annual leave days');", true);
        }
    }
}
