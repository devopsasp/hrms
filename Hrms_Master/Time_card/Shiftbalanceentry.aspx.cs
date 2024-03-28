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
    Collection<Employee> EmployeeList;

    string[] _Value, _Val;
    string s_login_role;
    string s_form = "";
    DataSet ds_userrights;
    DataTable _DayEventsTable;

    public string mnt { get; private set; }
    public int year { get; private set; }
    public int month { get; private set; }

    protected void Page_Load(object sender, EventArgs e)
    {

        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        //ddl_patterncode.Enabled = false;
        if (!IsPostBack)
        {

            switch (s_login_role)
            {
                case "a":
                    load_admin();
                    ddl_Department_load();
                    break;

                case "h":
                    ddl_Department_load();
                    load();
                    access();
                    ddl_branch.Visible = false;
                    break;

                case "u":
                    s_form = "5";
                    ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                    if (ds_userrights.Tables[0].Rows.Count > 0)
                    {
                        load();
                    }
                    else
                    {
                        Response.Cookies["Msg_Session"].Value = "Permission Restricted. Please Contact Administrator.";
                        Response.Redirect("~/Company_Home.aspx");
                    }
                    break;

                default:
                    Response.Cookies["Msg_Session"].Value = "Permission Restricted. Please Contact Administrator";
                    Response.Redirect("~/Company_Home.aspx");
                    break;

            }
        }
    }

    public void ddl_Department_load()
    {
        EmployeeList = employee.fn_getDepartmentList1(employee.BranchId);
        if (EmployeeList.Count > 0)
        {

            for (int ddl_i = -2; ddl_i < EmployeeList.Count; ddl_i++)
            {

                if (ddl_i == -2)
                {
                    ListItem es_list = new ListItem();

                    es_list.Text = "Select Department";
                    es_list.Value = "0";
                    ddl_department.Items.Add(es_list);
                }
                else if(ddl_i==-1)
                {

                    ListItem es_list = new ListItem();

                    es_list.Text = "All";
                    es_list.Value = "1";
                    ddl_department.Items.Add(es_list);
                }
                else
                {
                    ListItem es_list = new ListItem();

                    es_list.Value = EmployeeList[ddl_i].DepartmentId.ToString();
                    es_list.Text = EmployeeList[ddl_i].DepartmentName.ToString();
                    ddl_department.Items.Add(es_list);
                }
            }
        }
    }


    public void ddl_Pattern_load()
    {
        ddl_patterncode.Items.Clear();
        EmployeeList = employee.fn_getPatternList(employee.BranchId);
        if (EmployeeList.Count > 0)
        {

            for (int ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {

                if (ddl_i == -1)
                {
                    ListItem es_list = new ListItem();

                    es_list.Text = "Select";
                    es_list.Value = "0";
                    ddl_patterncode.Items.Add(es_list);
                }
                else
                {
                    ListItem es_list = new ListItem();

                    es_list.Value = EmployeeList[ddl_i].ShiftName.ToString();
                    es_list.Text = EmployeeList[ddl_i].ShiftName.ToString();
                    ddl_patterncode.Items.Add(es_list);
                }
            }
        }
    }

    public void ddl_shift_load()
    {
        ddl_shift.Items.Clear();
        EmployeeList = employee.fn_getshiftList(employee.BranchId);
        if (EmployeeList.Count > 0)
        {

            for (int ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {

                if (ddl_i == -1)
                {
                    ListItem es_list = new ListItem();

                    es_list.Text = "Select";
                    es_list.Value = "0";
                    ddl_shift.Items.Add(es_list);
                }
                else
                {
                    ListItem es_list = new ListItem();

                    es_list.Value = EmployeeList[ddl_i].ShiftCode.ToString();
                    es_list.Text = EmployeeList[ddl_i].ShiftCode.ToString();
                    ddl_shift.Items.Add(es_list);
                }
            }
        }
    }

    public void load()
    {
        myConnection.Open();
        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM shift_pattern where pn_companyID='" + employee.CompanyId + "' and pn_BranchID = '" + employee.BranchId + "'", myConnection);
        DataSet ds = new DataSet();
        ad.Fill(ds, "shift_pattern");
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

        TextBox txt_bran = (TextBox)GridView1.FooterRow.FindControl("txt_branch");
        txt_bran.Text = employee.BranchId.ToString();
        txt_bran.Enabled = false;

        cmd = new SqlCommand("select * from shift_details ", myConnection);
        rea = cmd.ExecuteReader();
        while (rea.Read())
        {
            DropDownList shift_code1 = (DropDownList)GridView1.FooterRow.FindControl("ddl_shiftcode1");
            DropDownList shift_code2 = (DropDownList)GridView1.FooterRow.FindControl("ddl_shiftcode2");
            DropDownList shift_code3 = (DropDownList)GridView1.FooterRow.FindControl("ddl_shiftcode3");
            DropDownList shift_code4 = (DropDownList)GridView1.FooterRow.FindControl("ddl_shiftcode4");
            DropDownList shift_code5 = (DropDownList)GridView1.FooterRow.FindControl("ddl_shiftcode5");
            DropDownList shift_code6 = (DropDownList)GridView1.FooterRow.FindControl("ddl_shiftcode6");
            DropDownList shift_code7 = (DropDownList)GridView1.FooterRow.FindControl("ddl_shiftcode7");
            DropDownList shift_code8 = (DropDownList)GridView1.FooterRow.FindControl("ddl_shiftcode8");
            shift_code1.Items.Add(rea["shift_code"].ToString());
            shift_code2.Items.Add(rea["shift_code"].ToString());
            shift_code3.Items.Add(rea["shift_code"].ToString());
            shift_code4.Items.Add(rea["shift_code"].ToString());
            shift_code5.Items.Add(rea["shift_code"].ToString());
            shift_code6.Items.Add(rea["shift_code"].ToString());
            shift_code7.Items.Add(rea["shift_code"].ToString());
            shift_code8.Items.Add(rea["shift_code"].ToString());
        }
        rea.Close();
        myConnection.Close();


        //myConnection.Open();
        //cmd = new SqlCommand("Select * from shift_pattern where pn_companyid = '" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
        //rea = cmd.ExecuteReader();
        //while (rea.Read())
        //{
        //    ddl_patterncode.Items.Add(Convert.ToString(rea[2]));
        //}
        //rea.Close();
        //myConnection.Close();

        ddl_Pattern_load();
        ddl_shift_load();
        ddl_employee_load();
        //lbl_Error.Text = "chk";

    }

    public void ddl_employee_load()
    {
        try
        {
            if (s_login_role == "a")
            {
                employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
            }

            Collection<Employee> EmployeeList = employee.fn_getEmployeeList(employee);
            if (EmployeeList.Count > 0)
            {
                ddl_empcode.DataSource = EmployeeList;
                ddl_empcode.DataValueField = "EmployeeCode";
                ddl_empcode.DataTextField = "LastName";
                ddl_empcode.DataBind();
                ddl_empcode.Items.Insert(0, "Select Employee");
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Employees Available');", true);
            }
        }
        catch (Exception ex)
        {

        }
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

    public void load1()
    {
        myConnection.Open();
        //cmd = new SqlCommand("select * from shift_pattern where pn_BranchID='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "'", myConnection);
        //rea = cmd.ExecuteReader();
        //if (rea.Read())
        //{
        //    string d1 = rea["days1"].ToString();
        //    string d2 = rea["days2"].ToString();
        //}

        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM shift_pattern where pn_BranchID = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "'", myConnection);

        DataSet ds = new DataSet();

        ad.Fill(ds, "shift_pattern");

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


        cmd = new SqlCommand("select * from shift_details where pn_BranchId='" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "'", myConnection);
        rea = cmd.ExecuteReader();
        while (rea.Read())
        {
            DropDownList shift_code1 = (DropDownList)GridView1.FooterRow.FindControl("ddl_shiftcode1");
            DropDownList shift_code2 = (DropDownList)GridView1.FooterRow.FindControl("ddl_shiftcode2");
            DropDownList shift_code3 = (DropDownList)GridView1.FooterRow.FindControl("ddl_shiftcode3");
            DropDownList shift_code4 = (DropDownList)GridView1.FooterRow.FindControl("ddl_shiftcode4");
            DropDownList shift_code5 = (DropDownList)GridView1.FooterRow.FindControl("ddl_shiftcode5");
            DropDownList shift_code6 = (DropDownList)GridView1.FooterRow.FindControl("ddl_shiftcode6");
            DropDownList shift_code7 = (DropDownList)GridView1.FooterRow.FindControl("ddl_shiftcode7");
            DropDownList shift_code8 = (DropDownList)GridView1.FooterRow.FindControl("ddl_shiftcode8");

            TextBox txt_bran = (TextBox)GridView1.FooterRow.FindControl("txt_branch");

            txt_bran.Text = employee.BranchId.ToString();
            txt_bran.Enabled = false;
            shift_code1.Items.Add(rea["shift_code"].ToString());
            shift_code2.Items.Add(rea["shift_code"].ToString());
            shift_code3.Items.Add(rea["shift_code"].ToString());
            shift_code4.Items.Add(rea["shift_code"].ToString());
            shift_code5.Items.Add(rea["shift_code"].ToString());
            shift_code6.Items.Add(rea["shift_code"].ToString());
            shift_code7.Items.Add(rea["shift_code"].ToString());
            shift_code8.Items.Add(rea["shift_code"].ToString());
        }
        rea.Close();
        myConnection.Close();
        ((TextBox)GridView1.FooterRow.FindControl("txt_pattern")).Focus();
    }


    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedItem.Text == "All")
        {
            foreach (ListItem li in CheckBoxList1.Items)
            {
                li.Selected = true;
            }
        }
        else
        {
            foreach (ListItem li in CheckBoxList1.Items)
            {
                li.Selected = false;
            }
        }
    }

    protected void ddl_empcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txt_monyear.Text.Length >= 7)
        {
            string sample = "01/";
            string correct = sample + txt_monyear.Text;
            DateTime d = Convert.ToDateTime(correct);
            Calendar1.TodaysDate = d;

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter Month/Year in Shift Selection');", true);
            // ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('');", true);
        }
    }


    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        _DayEventsTable = new DataTable();
        _DayEventsTable.Columns.Add("Date");
        _DayEventsTable.Columns.Add("Title");
        //myConnection.Close();
        myConnection.Open();
        cmd = new SqlCommand("Select * from shift_month where pn_companyid = '" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "' and pn_Employeecode='" + ddl_empcode.SelectedValue + "' and monthyear='" + txt_monyear.Text + "' ", myConnection);
        rea = cmd.ExecuteReader();
        while (rea.Read())
        {
            _DayEventsTable.Rows.Add(Convert.ToString(rea[5]), Convert.ToString(rea[6]));
        }

        foreach (DataRow Row in _DayEventsTable.Rows)
        {
            string Date = Row["Date"].ToString();
            string Title = Row["Title"].ToString();
            if (Date == e.Day.Date.ToString())
            {
                e.Cell.Controls.Add(new LiteralControl("</br>" + Title));
            }
        }
        myConnection.Close();
    }

    protected void txt_monyear_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Calendar1.TodaysDate = Convert.ToDateTime(txt_monyear.Text);
            myConnection.Open();
            cmd = new SqlCommand("Select pattern_code, slot, balance_days from shift_balance where pn_companyid = '" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "' and monthyear='" + txt_monyear.Text + "' ", myConnection);
            rea = cmd.ExecuteReader();
            if (rea.Read())
            {
                ddl_patterncode.SelectedValue = rea[0].ToString();
                txt_slot.Text = rea[1].ToString();
                txt_balance.Text = rea[2].ToString();
            }
            else
            {
                ddl_patterncode.SelectedIndex = 0;
                txt_balance.Text = "";
            }
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Check the Month/Year you have entered');", true);
            //ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Check the Month/Year you have entered');", true);
        }
        finally
        {
            myConnection.Close();
        }
    }

    protected void ddl_branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (s_login_role == "a")
        {
            employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
        }
        SqlDataSource1.SelectCommand = "select employee_first_name,employeecode from paym_employee where pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + employee.BranchId + "' ";

        load();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //// loop all data rows
            //foreach (DataControlFieldCell cell in e.Row.Cells)
            //{
            //    //TextBox tbox = e.Row.FindControl("txt_pattern") as TextBox;
            //    //tbox.Attributes.Add("onKeyDown", "javascript:if(window.event.keycode == 13)return false;}");
            //    // check all cells in one row
            //    foreach (Control control in cell.Controls)
            //    {
            //        // Must use LinkButton here instead of ImageButton
            //        // if you are having Links (not images) as the command button.
            //        ImageButton button = control as ImageButton;
            //        if (button != null && button.CommandName == "Delete")
            //            // Add delete confirmation
            //            button.OnClientClick = "if (!confirm('Are you sure " +
            //                   "you want to delete this record?')) return;";
            //    }
            //}

            for (int i = 0; i < GridView1.Columns.Count; i++)
            {
                if (i == 0)
                {
                    string txt1 = ((Label)e.Row.FindControl("lbl_branch")).Text;
                    string str = txt1;
                    if (str == "0")
                    {
                        e.Row.Cells[i].Text = "";
                    }
                }
                if (i == 1)
                {
                    string txt1 = ((Label)e.Row.FindControl("lbl_pattern")).Text;
                    string str = txt1;
                    if (str == "0")
                    {
                        e.Row.Cells[i].Text = "";
                    }
                }
                if (i == 2)
                {
                    string txt1 = ((Label)e.Row.FindControl("lbl_shiftcode1")).Text;
                    string str = txt1;
                    if (str == "0")
                    {
                        e.Row.Cells[i].Text = "";
                    }
                }
                if (i == 3)
                {
                    string txt1 = ((Label)e.Row.FindControl("lbl_days1")).Text;
                    string str = txt1;
                    if (str == "0")
                    {
                        e.Row.Cells[i].Text = "";
                    }
                }
                if (i == 4)
                {
                    string txt1 = ((Label)e.Row.FindControl("lbl_shiftcode2")).Text;
                    string str = txt1;
                    if (str == "0")
                    {
                        e.Row.Cells[i].Text = "";
                    }
                }
                if (i == 5)
                {
                    string txt1 = ((Label)e.Row.FindControl("lbl_days2")).Text;
                    string str = txt1;
                    if (str == "0")
                    {
                        e.Row.Cells[i].Text = "";
                    }
                }
                if (i == 6)
                {
                    string txt1 = ((Label)e.Row.FindControl("lbl_shiftcode3")).Text;
                    string str = txt1;
                    if (str == "0")
                    {
                        e.Row.Cells[i].Text = "";
                    }
                }
                if (i == 7)
                {
                    string txt1 = ((Label)e.Row.FindControl("lbl_days3")).Text;
                    string str = txt1;
                    if (str == "0")
                    {
                        e.Row.Cells[i].Text = "";
                    }
                }
                if (i == 8)
                {
                    string txt1 = ((Label)e.Row.FindControl("lbl_shiftcode4")).Text;
                    string str = txt1;
                    if (str == "0")
                    {
                        e.Row.Cells[i].Text = "";
                    }
                }
                if (i == 9)
                {
                    string txt1 = ((Label)e.Row.FindControl("lbl_days4")).Text;
                    string str = txt1;
                    if (str == "0")
                    {
                        e.Row.Cells[i].Text = "";
                    }
                }

                if (i == 10)
                {
                    string txt1 = ((Label)e.Row.FindControl("lbl_shiftcode5")).Text;
                    string str = txt1;
                    if (str == "0")
                    {
                        e.Row.Cells[i].Text = "";
                    }
                }

                if (i == 11)
                {
                    string txt1 = ((Label)e.Row.FindControl("lbl_days5")).Text;
                    string str = txt1;
                    if (str == "0")
                    {
                        e.Row.Cells[i].Text = "";
                    }
                }

                if (i == 12)
                {
                    string txt1 = ((Label)e.Row.FindControl("lbl_shiftcode6")).Text;
                    string str = txt1;
                    if (str == "0")
                    {
                        e.Row.Cells[i].Text = "";
                    }
                }

                if (i == 13)
                {
                    string txt1 = ((Label)e.Row.FindControl("lbl_days6")).Text;
                    string str = txt1;
                    if (str == "0")
                    {
                        e.Row.Cells[i].Text = "";
                    }
                }

                if (i == 14)
                {
                    string txt1 = ((Label)e.Row.FindControl("lbl_shiftcode7")).Text;
                    string str = txt1;
                    if (str == "0")
                    {
                        e.Row.Cells[i].Text = "";
                    }
                }

                if (i == 15)
                {
                    string txt1 = ((Label)e.Row.FindControl("lbl_days7")).Text;
                    string str = txt1;
                    if (str == "0")
                    {
                        e.Row.Cells[i].Text = "";
                    }
                }

                if (i == 16)
                {
                    string txt1 = ((Label)e.Row.FindControl("lbl_shiftcode8")).Text;
                    string str = txt1;
                    if (str == "0")
                    {
                        e.Row.Cells[i].Text = "";
                    }
                }
                    {
                    string txt1 = ((Label)e.Row.FindControl("lbl_days8")).Text;
                    string str = txt1;
                    if (str == "0")
                    {
                        e.Row.Cells[i].Text = "";
                    }
                }
            }


        }
    }
    //public void delete_tables()
    //{
    //    //_Value = CheckBoxList1.Items[em].Text.Split('-');
    //    employee.EmployeeCode = _Value[1].ToString().Trim();
    //    // myConnection.Open();
    //    cmd = new SqlCommand("Delete from shift_balance where pn_EmployeeCode= '" + CheckBoxList1.Items[em].Value + "' and monthyear='" + txt_monyear.Text + "' and pn_branchID = '" + employee.BranchId + "';Delete from shift_month where pn_EmployeeCode= '" + CheckBoxList1.Items[em].Value + "' and monthyear='" + txt_monyear.Text + "' and pn_branchID = '" + employee.BranchId + "'", myConnection);
    //    cmd.ExecuteNonQuery();
    //    // myConnection.Close();
    //}


    protected void txt_balance_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(txt_balance.Text) > 7)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Balance days must be less than 7');", true);
            //ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Balance days must be less than 7');", true);
        }
    }
    //protected void btn_bulksave_Click(object sender, EventArgs e)
    //{
    //    if (txt_monyear.Text == "")
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter month & year')", true);
    //    }
    //    else
    //    {
    //        if (CheckBoxList1.SelectedItem == null)
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Select Employees');", true);
    //        }
    //        else
    //        {
    //            myConnection.Open();
    //            for (int em = 0; em < CheckBoxList1.Items.Count; em++)
    //            {
    //                _Value = CheckBoxList1.Items[em].Text.Split('-');
    //                employee.EmployeeCode = _Value[1].ToString().Trim();
    //                if (CheckBoxList1.Items[em].Selected == true)
    //                {
    //                    cmd = new SqlCommand("select pattern_code from shift_balance pn_employeecode=" + employee.EmployeeCode);
    //                    cmd.ExecuteNonQuery();
    //                }
    //            }

    //        }
    //    }
    //}
    protected void  btn_save_Click(object sender, EventArgs e)
    {
        if (txt_monyear.Text == "" || ddl_patterncode.Text == "Select" || txt_balance.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter all details.');", true);
        }
        else if (Convert.ToInt32(txt_balance.Text) > 7)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Balance days must be less than 7');", true);
            //ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Balance days must be less than 7');", true);
        }
        else
        {
            if (CheckBoxList1.SelectedItem == null)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Select Employees');", true);
            }
            else
            {
                string scode1 = "", scode2 = "", scode3 = "", scode4 = "", scode5 = "", scode6 = "", scode7 = "", scode8 = "", dat = "";
                string day1 = "", day2 = "", day3 = "", day4 = "", day5 = "", day6 = "", day7 = "", day8 = "";
                int days1 = 0, days2 = 0, days3 = 0, days4 = 0, days5 = 0, days6 = 0, days7 = 0, days8 = 0,days9=0,days10=0;
                string strdate = txt_monyear.Text;
                string[] datespt = strdate.Split('/');
                int bday = Convert.ToInt32(txt_balance.Text);
                int month = Convert.ToInt32(datespt[0]);

                int year = Convert.ToInt32(datespt[1]);
                //delete_tables();
                myConnection.Open();
                for (int em = 0; em < CheckBoxList1.Items.Count; em++)
                {
                    string datee;
                    if (month == 12)
                    {
                        month = 12;
                        //    dat = Convert.ToString(month + "/" + d + "/" + year);
                        //    DateTime ch = Convert.ToDateTime(year + "/" + month);

                        datee = Convert.ToString(month + "/" + year);
                    }
                    else if (month == 11)
                    {
                        month = 11;
                        //    dat = Convert.ToString(month + "/" + d + "/" + year);
                        //    DateTime ch = Convert.ToDateTime(year + "/" + month);

                        datee = Convert.ToString(month + "/" + year);
                    }
                    else if (month == 10)
                    {
                        month = 10;
                        //    dat = Convert.ToString(month + "/" + d + "/" + year);
                        //    DateTime ch = Convert.ToDateTime(year + "/" + month);

                        datee = Convert.ToString(month + "/" + year);
                    }
                    else
                    {
                        //    dat = Convert.ToString("0" + month + "/" + year);
                        //    DateTime ch = Convert.ToDateTime(year + "/" + "0" + month);

                        datee = Convert.ToString("0" + month + "/" + year);
                    }
                    dat = Convert.ToString(txt_monyear.Text);
                    _Value = CheckBoxList1.Items[em].Text.Split('-');
                    // employee.EmployeeCode = _Value[1].ToString().Trim();
                    //string datee = Convert.ToString(month + "/" + year);

                    employee.EmployeeCode = _Value[0].ToString();
                    employee.FirstName = _Value[1].ToString();
                    //employee.Employee_First = _Val[1].ToString();
                    string ecode = employee.EmployeeCode;
                    mnt = Convert.ToString(datee);
                    //delete_previous(mnt, ecode);
                    //cmd = new SqlCommand("Delete from shift_balance where pn_EmployeeCode= '" + employee.EmployeeCode + "' and monthyear='" + txt_monyear.Text + "' and pn_branchID = '" + employee.BranchId + "';Delete from shift_month where pn_EmployeeCode= '" + CheckBoxList1.Items[em].Value + "' and monthyear='" + txt_monyear.Text + "' and pn_branchID = '" + employee.BranchId + "'", myConnection);
                    //cmd.ExecuteNonQuery();

                    if (CheckBoxList1.Items[em].Selected == true)
                    {
                        delete_previous(mnt, ecode);
                        try
                        {
                            cmd = new SqlCommand("Delete from shift_balance where pn_EmployeeCode= '" + employee.EmployeeCode + "' and monthyear='" + txt_monyear.Text + "' and pn_branchID = '" + employee.BranchId + "';Delete from shift_month where pn_EmployeeCode= '" + CheckBoxList1.Items[em].Value + "' and monthyear='" + txt_monyear.Text + "' and pn_branchID = '" + employee.BranchId + "'", myConnection);
                            cmd.ExecuteNonQuery();
                            cmd = new SqlCommand("insert into shift_balance (pn_companyid,pn_branchid,pn_employeecode,pn_employeename,monthyear,pattern_code,slot,balance_days) values ('" + employee.CompanyId + "' , '" + employee.BranchId + "' ,  '" + employee.EmployeeCode + "','" + employee.FirstName + "','" + txt_monyear.Text + "','" + ddl_patterncode.Text + "' , '" + txt_slot.Text + "' , '" + txt_balance.Text + "')", myConnection);
                            cmd.ExecuteNonQuery();
                            //  ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('success');", true);
                        }
                        catch (Exception ex)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('delete  shift or inset shift balance');", true);
                        }
                    }
                }

                //DateTime sdate = Convert.ToDateTime("01/"+txt_monyear.Teit);
                //string scode1 = "", scode2 = "", scode3 = "", scode4 = "", scode5 = "", scode6 = "", scode7 = "", scode8 = "", dat = "";
                //string day1 = "", day2 = "", day3 = "", day4 = "", day5 = "", day6 = "", day7 = "", day8 = "";
                //int days1 = 0, days2 = 0, days3 = 0, days4 = 0, days5 = 0, days6 = 0, days7 = 0, days8 = 0;
                //string strdate = txt_monyear.Text;
                //string[] datespt = strdate.Split('/');
                //int bday = Convert.ToInt32(txt_balance.Text);
                //int month = Convert.ToInt32(datespt[0]);
                //int year = Convert.ToInt32(datespt[1]);

                // Checking the first day of the monthhsd
                if (s_login_role == "a")
                {
                    //cmd = new SqlCommand("Select week_off1 , week_off2 from attendance_ceiling where pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + ddl_branch.Text + "'", myConnection);
                }
                if (s_login_role == "h")
                {
                    cmd = new SqlCommand("Select week_off1 , week_off2 from attendance_ceiling where pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
                }
                SqlDataReader rdw = cmd.ExecuteReader();

                string wk1 = "", wk2 = "";
                if (rdw.Read())
                {
                    wk1 = Convert.ToString(rdw[0]);
                    wk2 = Convert.ToString(rdw[1]);
                }

                string f1 = "01/" + strdate;
                string f2 = "02/" + strdate;
                //string f1 = datespt[0] + "/01/" + datespt[1];
                DateTime sun = Convert.ToDateTime(f1);
                DateTime sat = Convert.ToDateTime(f2);
                string dow = sun.DayOfWeek.ToString();
                string dow2 = sat.DayOfWeek.ToString();
                //if (wk1 != wk2 && (dow == wk1 || dow == wk2))
                if (dow == wk1)
                {
                    bday = bday + 1;
                }
                if (dow == wk2)
                {
                    bday = bday + 1;
                }

                // End

                int enddate = System.DateTime.DaysInMonth(year, month);
                cmd1 = new SqlCommand("Select * from shift_pattern where pn_companyid = '" + employee.CompanyId + "' and pn_branchid= '" + employee.BranchId + "' and pattern_code = '" + ddl_patterncode.Text + "'", myConnection);
                SqlDataReader rd_sp = cmd1.ExecuteReader();
                if (rd_sp.Read())
                {
                    scode1 = Convert.ToString(rd_sp["shift_code1"]);
                    day1 = Convert.ToString(rd_sp["days1"]);
                    scode2 = Convert.ToString(rd_sp["shift_code2"]);
                    day2 = Convert.ToString(rd_sp["days2"]);
                    scode3 = Convert.ToString(rd_sp["shift_code3"]);
                    day3 = Convert.ToString(rd_sp["days3"]);
                    scode4 = Convert.ToString(rd_sp["shift_code4"]);
                    day4 = Convert.ToString(rd_sp["days4"]);
                    scode5 = Convert.ToString(rd_sp["shift_code5"]);
                    day5 = Convert.ToString(rd_sp["days5"]);
                    scode6 = Convert.ToString(rd_sp["shift_code6"]);
                    day6 = Convert.ToString(rd_sp["days6"]);
                    scode7 = Convert.ToString(rd_sp["shift_code7"]);
                    day7 = Convert.ToString(rd_sp["days7"]);
                    scode8 = Convert.ToString(rd_sp["shift_code8"]);
                    day8 = Convert.ToString(rd_sp["days8"]);
                }
                //rd_sp.Close();
                int d, f, g, h, i, j, k, l, m, n, o, p, r, s, t,z;

                for (int ec = 0; ec < CheckBoxList1.Items.Count; ec++)
                {
                    if (CheckBoxList1.Items[ec].Selected == true)
                    {
                        _Val = CheckBoxList1.Items[ec].Text.Split('-');
                        employee.EmployeeCode = _Val[0].ToString();
                        string ecode = employee.EmployeeCode;
                        employee.FirstName = _Val[1].ToString();
                        //employee.Employee_First = _Val[1].ToString();
                        //string ecode = employee.EmployeeCode;
                        //mnt = Convert.ToString(datee);
                        //delete_previous(mnt, ecode);
                        for (d = 1; d <= enddate; d++)
                        {
                            dat = Convert.ToString(month + "/" + d + "/" + year);
                            DateTime ch = Convert.ToDateTime(year + "/" + month + "/" + d);
                            string datee = Convert.ToString(month + "/" + year);
                            if (wk1 != wk2 && (dow == wk1 || dow == wk2) && ch == sun)
                            {
                                scode1 = "W";
                            }
                            else
                            {
                                scode1 = Convert.ToString(rd_sp["shift_code1"]);
                            }
                            try
                            {
                                
                                cmd = new SqlCommand("insert into shift_month values('" + employee.CompanyId + "' , '" + employee.BranchId + "'  , '" + employee.EmployeeCode + "' ,'" + employee.FirstName + "', '" + txt_monyear.Text + "' , '" + dat + "' , '" + scode1 + "')", myConnection);
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(ex);", true);
                            }

                        }
                    }
                }
                
                           
               
                     for (int ec = 0; ec < CheckBoxList1.Items.Count; ec++)
                      {
                        if (CheckBoxList1.Items[ec].Selected == true)

                        {
                          for (d = 1; d <= enddate; d++)
                          {
                            dat = Convert.ToString(month + "/" + d + "/" + year);
                            DateTime ch = Convert.ToDateTime(year + "/" + month + "/" + d);
                            string datee = Convert.ToString(month + "/" + year);
                            
                            dat = Convert.ToString(month + "/" + d + "/" + year);
                            _Val = CheckBoxList1.Items[ec].Text.Split('-');
                            employee.EmployeeCode = _Val[0].ToString();
                            employee.FirstName = _Val[1].ToString();
                            int first;
                            if ((dow == wk1 || dow == wk2) && (dow2 == wk1 || dow2 == wk2))
                            {
                                first = bday;
                            }
                            else if ((dow == wk1 || dow == wk2) && dow2 != wk1 && dow2 != wk2)
                            {
                                first = bday + 6;
                            }
                            //Modification starts
                           
                            first = bday + 1;
                              
                                days2 = Convert.ToInt32(day2);

                                for (f = first; f < first + days2; f++)
                                {
                                    dat = Convert.ToString(month + "/" + f + "/" + year);
                                    cmd = new SqlCommand("update shift_month set Shift_Code='" + scode2 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                    cmd.ExecuteNonQuery();
                                }
                            if (scode3 != "")
                            {
                                int second = f;
                                days3 = Convert.ToInt32(day3);
                                for (g = second; g < second + days3; g++)
                                {
                                    dat = Convert.ToString(month + "/" + g + "/" + year);
                                    cmd = new SqlCommand("update shift_month set Shift_Code='" + scode3 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                    cmd.ExecuteNonQuery();
                                }
                                int third = g;
                                days4 = Convert.ToInt32(day4);
                                for (i = third; i < third + days4; i++)
                                {
                                    dat = Convert.ToString(month + "/" + i + "/" + year);
                                    cmd = new SqlCommand("update shift_month set Shift_Code='" + scode4 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                    cmd.ExecuteNonQuery();
                                }

                                if (scode5 != "")
                                {
                                    int fourth = i;
                                    days5 = Convert.ToInt32(day5);
                                    for (j = fourth; j < fourth + days5; j++)
                                    {
                                        dat = Convert.ToString(month + "/" + j + "/" + year);
                                        cmd = new SqlCommand("update shift_month set Shift_Code='" + scode5 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                        cmd.ExecuteNonQuery();
                                    }
                                    int fifth = j;
                                    days6 = Convert.ToInt32(day6);
                                    for (k = fifth; k < fifth + days6; k++)
                                    {
                                        dat = Convert.ToString(month + "/" + k + "/" + year);
                                        cmd = new SqlCommand("update shift_month set Shift_Code='" + scode6 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                        cmd.ExecuteNonQuery();
                                    }
                                    if (scode7 != "")
                                    {
                                        int svn = k;
                                        days7 = Convert.ToInt32(day7);
                                        for (r = svn; r < svn + days7; r++)
                                        {
                                            dat = Convert.ToString(month + "/" + r + "/" + year);
                                            cmd = new SqlCommand("update shift_month set Shift_Code='" + scode7 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                            cmd.ExecuteNonQuery();
                                        }
                                        int egt = r;
                                        days8 = Convert.ToInt32(day8);
                                        for (s = egt; s < egt + days8; s++)
                                        {
                                            dat = Convert.ToString(month + "/" + s + "/" + year);
                                            cmd = new SqlCommand("update shift_month set Shift_Code='" + scode8 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                            cmd.ExecuteNonQuery();
                                        }
                                        if (s <= enddate)
                                        {
                                            int ecc = s;

                                            days9 = Convert.ToInt32(day7);
                                            for (z = ecc; z < ecc + days9; z++)
                                            {

                                                if (z <= enddate)
                                                {
                                                    dat = Convert.ToString(month + "/" + z + "/" + year);
                                                    cmd = new SqlCommand("update shift_month set Shift_Code='" + scode3 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                                    cmd.ExecuteNonQuery();
                                                }

                                            }
                                            if (z <= enddate)
                                            {
                                                int etg = z;
                                                days10 = Convert.ToInt32(day8);
                                                for (o = etg; o < etg + days8; o++)
                                                {
                                                    dat = Convert.ToString(month + "/" + o + "/" + year);
                                                    cmd = new SqlCommand("update shift_month set Shift_Code='" + scode2 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                                    cmd.ExecuteNonQuery();
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        int svn1 = k;
                                        days1 = Convert.ToInt32(day1);
                                        for (r = svn1; r < svn1 + days1; r++)
                                        {
                                            dat = Convert.ToString(month + "/" + r + "/" + year);
                                            cmd = new SqlCommand("update shift_month set Shift_Code='" + scode1 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                            cmd.ExecuteNonQuery();
                                        }
                                        int egt1 = r;
                                        days2 = Convert.ToInt32(day2);
                                        for (s = egt1; s < egt1 + days2; s++)
                                        {
                                            dat = Convert.ToString(month + "/" + s + "/" + year);
                                            cmd = new SqlCommand("update shift_month set Shift_Code='" + scode2 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                            cmd.ExecuteNonQuery();
                                        }
                                    }
                                }
                                else
                                {
                                    int fourth1 = i;
                                    days1 = Convert.ToInt32(day1);
                                    for (j = fourth1; j < fourth1 + days1; j++)
                                    {
                                        dat = Convert.ToString(month + "/" + j + "/" + year);
                                        cmd = new SqlCommand("update shift_month set Shift_Code='" + scode1 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                        cmd.ExecuteNonQuery();
                                    }
                                    int fifth1 = j;
                                    days2 = Convert.ToInt32(day2);
                                    for (k = fifth1; k < fifth1 + days2; k++)
                                    {
                                        dat = Convert.ToString(month + "/" + k + "/" + year);
                                        cmd = new SqlCommand("update shift_month set Shift_Code='" + scode2 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                        cmd.ExecuteNonQuery();
                                    }

                                    int sixth1 = k;
                                    days3 = Convert.ToInt32(day3);
                                    for (l = sixth1; l < sixth1 + days3; l++)
                                    {
                                        dat = Convert.ToString(month + "/" + l + "/" + year);
                                        cmd = new SqlCommand("update shift_month set Shift_Code='" + scode3 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                        cmd.ExecuteNonQuery();
                                    }
                                    int seventh1 = l;
                                    days4 = Convert.ToInt32(day4);
                                    for (m = seventh1; m < seventh1 + days4; m++)
                                    {
                                        dat = Convert.ToString(month + "/" + m + "/" + year);
                                        cmd = new SqlCommand("update shift_month set Shift_Code='" + scode4 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                        cmd.ExecuteNonQuery();
                                    }

                                    int eighth1 = m;
                                    days1 = Convert.ToInt32(day1);
                                    for (n = eighth1; n < eighth1 + days1; n++)
                                    {
                                        if (n <= enddate)
                                        {
                                            dat = Convert.ToString(month + "/" + n + "/" + year);
                                            cmd = new SqlCommand("update shift_month set Shift_Code='" + scode1 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                            cmd.ExecuteNonQuery();
                                        }
                                    }
                                    int ninth1 = n;
                                    days2 = Convert.ToInt32(day2);
                                    for (o = seventh1; o < seventh1 + days2; o++)
                                    {
                                        if (n <= enddate)
                                        {
                                            dat = Convert.ToString(month + "/" + o + "/" + year);
                                            cmd = new SqlCommand("update shift_month set Shift_Code='" + scode2 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                            cmd.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                int second1 = f;
                                days1 = Convert.ToInt32(day1);
                                for (g = second1; g < second1 + days1; g++)
                                {
                                    dat = Convert.ToString(month + "/" + g + "/" + year);
                                    cmd = new SqlCommand("update shift_month set Shift_Code='" + scode1 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                    cmd.ExecuteNonQuery();
                                }
                                int third1 = g;
                                days1 = Convert.ToInt32(day1);
                                days2 = Convert.ToInt32(day2);
                                for (h = third1; h <= enddate; h += days1 + 1)
                                {
                                    dat = Convert.ToString(month + "/" + h + "/" + year);
                                    cmd = new SqlCommand("update shift_month set Shift_Code='" + scode2 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                    cmd.ExecuteNonQuery();
                                    if (days2 == 2)
                                    {
                                        int x = h + 1;
                                        if (x <= enddate)
                                        {
                                            dat = Convert.ToString(month + "/" + x + "/" + year);
                                            cmd = new SqlCommand("update shift_month set Shift_Code='" + scode2 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                            cmd.ExecuteNonQuery();
                                            h++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    
                    //Modification ends
                    rd_sp.Close();

                }

                
                cmd = new SqlCommand("create table temp_shift(pn_CompanyID int , pn_BranchID int , pn_EmployeeCode varchar(50) , pn_EmployeeName varchar(50) , monthyear varchar(8) , date datetime , shift_Code varchar(5))", myConnection);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("INSERT INTO temp_shift SELECT DISTINCT * FROM shift_month where pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("drop table shift_month", myConnection);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("EXEC sp_rename 'temp_shift', 'shift_month'", myConnection);
                cmd.ExecuteNonQuery();

                myConnection.Close();
                //Response.Write("<script language='javascript'>alert('Shift balance saved successfully')</script>");
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Saved Successfully');", true);
            }
        }
    }
   

    protected void btn_reset_Click(object sender, EventArgs e)
    {
        txt_monyear.Text = "";
        txt_balance.Text = "";
        txt_slot.SelectedIndex = 0;
        ddl_patterncode.SelectedIndex = 0;
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {
        try
        {
            myConnection.Open();
            for (int ec = 0; ec < CheckBoxList1.Items.Count; ec++)
            {
                if (CheckBoxList1.Items[ec].Selected == true)
                {
                    _Val = CheckBoxList1.Items[ec].Text.Split('-');
                    employee.EmployeeCode = _Val[0].ToString();
                    string ecode = employee.EmployeeCode;
                    employee.FirstName = _Val[1].ToString();
                    cmd = new SqlCommand("set dateformat dmy;update shift_month set shift_code = '" + ddl_shift.SelectedItem.Text + "' where date = '" + txt_sdate.Text + "' and pn_branchID = '" + employee.BranchId + "' and pn_employeecode='" + employee.EmployeeCode + "';set dateformat mdy;", myConnection);
                    cmd.ExecuteNonQuery();
                }
            }
            //if (chk_student.Checked)
            //{
            //    cmd = new SqlCommand("set dateformat dmy;insert into StudentShiftChange values('" + employee.CompanyId + "','" + employee.BranchId + "','" + txt_sdate.Text + "','ST');set dateformat mdy;", myConnection);
            //    cmd.ExecuteNonQuery();
            //}
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Shift Updated Successfully');", true);

        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
        finally
        {
            myConnection.Close();
        }

    }
    protected void btn_rset_Click(object sender, EventArgs e)
    {
        txt_sdate.Text = "";
        ddl_shift.SelectedIndex = 0;
    }
    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Employee_load();
    }
    //public void ddl_load_save()
    //{
    //    if (ddl_save.SelectedItem.Text == "save")
    //    {
    //        ddl_patterncode.Enabled = true;
    //    }
    //    if (ddl_save.SelectedItem.Text == "bulk Save")
    //    {
    //        ddl_patterncode.Enabled = false;
    //    }
    //}
    //protected void ddl_Save_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ddl_load_save();
    //}
    public void ddl_Employee_load()
    {
        if (ddl_department.SelectedItem.Text == "All")
        {
            
            EmployeeList = employee.fn_getEmployeeDepartment1(employee);
           
            if (EmployeeList.Count > 0)
            { CheckBoxList1.Items.Clear();
                for (int ddl_i = 0; ddl_i < EmployeeList.Count; ddl_i++)
                {
                    ListItem es_list = new ListItem();
                    es_list.Value = EmployeeList[ddl_i].EmployeeCode.ToString();
                    es_list.Text = EmployeeList[ddl_i].LastName.ToString();
                    CheckBoxList1.Items.Add(es_list);
                }
            }
        }
        else
        {
            CheckBoxList1.Items.Clear();
            employee.DepartmentId = Convert.ToInt32(ddl_department.SelectedValue);
            EmployeeList = employee.fn_getEmployeeDepartment(employee);
            if (EmployeeList.Count > 0)
            {
                for (int ddl_i = 0; ddl_i < EmployeeList.Count; ddl_i++)
                {
                    ListItem es_list = new ListItem();
                    es_list.Value = EmployeeList[ddl_i].EmployeeCode.ToString();
                    es_list.Text = EmployeeList[ddl_i].LastName.ToString();
                    CheckBoxList1.Items.Add(es_list);
                }
            }
        }
    }
    protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int t = 0, f = 0;
        foreach (ListItem li in CheckBoxList1.Items)
        {
            if (li.Selected == true)
            {
                t += 1;
            }
            else
            {
                f += 1;
            }
        }
        if (f > 0)
        {
            RadioButtonList1.SelectedIndex = 1;
        }
        else
        {
            RadioButtonList1.SelectedIndex = 0;
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (s_login_role == "a")
        {

            GridView1.EditIndex = e.NewEditIndex; // turn to edit mode
            load();
            access();
        }
        else if (s_login_role == "h")
        {
            GridView1.EditIndex = e.NewEditIndex; // turn to edit mode
            load();
            access();
            //Edit();
            //AddNewRecord(scode, stime, btimeo, btimei, etime, sindicator);
        }
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        if (s_login_role == "a")
        {
            load();
        }
        else
        {
            load1();
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "add")
        {
            string branch = ((TextBox)GridView1.FooterRow.FindControl("txt_branch")).Text;
            string patcode = ((TextBox)GridView1.FooterRow.FindControl("txt_pattern")).Text;
            string scode1 = ((DropDownList)GridView1.FooterRow.FindControl("ddl_shiftcode1")).Text;
            string days1 = ((TextBox)GridView1.FooterRow.FindControl("txt_days1")).Text;
            string scode2 = ((DropDownList)GridView1.FooterRow.FindControl("ddl_shiftcode2")).Text;
            string days2 = ((TextBox)GridView1.FooterRow.FindControl("txt_days2")).Text;
            string scode3 = ((DropDownList)GridView1.FooterRow.FindControl("ddl_shiftcode3")).Text;
            string days3 = ((TextBox)GridView1.FooterRow.FindControl("txt_days3")).Text;
            string scode4 = ((DropDownList)GridView1.FooterRow.FindControl("ddl_shiftcode4")).Text;
            string days4 = ((TextBox)GridView1.FooterRow.FindControl("txt_days4")).Text;
            string scode5 = ((DropDownList)GridView1.FooterRow.FindControl("ddl_shiftcode5")).Text;
            string days5 = ((TextBox)GridView1.FooterRow.FindControl("txt_days5")).Text;
            string scode6 = ((DropDownList)GridView1.FooterRow.FindControl("ddl_shiftcode6")).Text;
            string days6 = ((TextBox)GridView1.FooterRow.FindControl("txt_days6")).Text;
            string scode7 = ((DropDownList)GridView1.FooterRow.FindControl("ddl_shiftcode7")).Text;
            string days7 = ((TextBox)GridView1.FooterRow.FindControl("txt_days7")).Text;
            string scode8 = ((DropDownList)GridView1.FooterRow.FindControl("ddl_shiftcode8")).Text;
            string days8 = ((TextBox)GridView1.FooterRow.FindControl("txt_days8")).Text;

            if (patcode == "" || scode1 == "" || days1 == "" || scode2 == "" || days2 == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter all the mandatory fields');", true);
                //ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Enter all the mandatory fields');", true);
                return;
            }
            else
            {
                AddNewRecord(branch, patcode, scode1, days1, scode2, days2, scode3, days3, scode4, days4, scode5, days5, scode6, days6, scode7, days7, scode8, days8);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Shift Details Saved Successfully');", true);
            }

        }
    }

    private void AddNewRecord(string branch, string patcode, string scode1, string days1, string scode2, string days2, string scode3, string days3, string scode4, string days4, string scode5, string days5, string scode6, string days6, string scode7, string days7, string scode8, string days8)
    {
        try
        {
            myConnection.Open();
            string query = @"INSERT INTO shift_pattern (pn_CompanyID, pn_BranchID, pattern_code, shift_code1,days1,shift_code2,days2,shift_code3,days3,shift_code4,days4,shift_code5,days5,shift_code6,days6,shift_code7,days7) VALUES('" + employee.CompanyId + "','" + branch + "','" + patcode + "','" + scode1 + "','" + days1 + "','" + scode2 + "','" + days2 + "','" + scode3 + "','" + days3 + "','" + scode4 + "','" + days4 + "','" + scode5 + "','" + days5 + "','" + scode6 + "','" + days6 + "','" + scode7 + "','" + days7 + "')";
            SqlCommand myCommand = new SqlCommand(query, myConnection);
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            if (s_login_role == "a")
            {
                load();
                ddl_Pattern_load();
            }
            else
            {
                load1();
                ddl_Pattern_load();
            }
        }
        catch (Exception ex)
        {
            myConnection.Close();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Pattern Code already exists');", true);

            //ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Pattern Code already exists');", true);
            //Response.Redirect("Login.aspx");
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string ID = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lbl_pattern")).Text;
        DeleteRecord(ID);
        if (s_login_role == "a")
        {
            load();
        }
        else
        {
            load1();
        }
    }

    private void DeleteRecord(string ID)
    {
        string sqlStatement = "DELETE FROM shift_pattern WHERE pattern_code = @pattern_code";
        try
        {
            myConnection.Open();
            SqlCommand cmd = new SqlCommand(sqlStatement, myConnection);
            cmd.Parameters.AddWithValue("@pattern_code", ID);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            if (ex.Number == 547)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Unable to Delete. transaction Exists.');", true);

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);

            }
        }
        finally
        {
            myConnection.Close();
        }
    }

    //protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    string ID = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lbl_pattern")).Text;
    //    DeleteRecord(ID);
    //    if (s_login_role == "a")
    //    {
    //        load();
    //    }
    //    else
    //    {
    //        load1();
    //    }
    //}

    //private void DeleteRecord(string ID)
    //{

    //    string Statement = " SELECT * FROM shift_pattern where pattern_code='" + ID + "'"; /*or shift_code2 = '" + ID + "' or shift_code3 = '" + ID + "' or shift_code4 = '" + ID + "' or shift_code5 = '" + ID + "' or shift_code6 = '" + ID + "' or shift_code7 = '" + ID + "'";*/
    //    myConnection.Open();
    //    SqlCommand cmd1 = new SqlCommand(Statement, myConnection);
    //    cmd1.CommandType = CommandType.Text;
    //    SqlDataReader rdrdel = cmd1.ExecuteReader();
    //    myConnection.Close();
    //    try
    //    {
    //        if (rdrdel.Read())
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Deletion Error: This record has a relationship with pattern code!');", true);
    //        }
    //        else
    //        {
    //            string sqlStatement = "DELETE FROM shift_pattern WHERE pattern_code = @pattern_code and pn_branchid = '" + ddl_branch.SelectedItem.Value + "'";/* "DELETE FROM shift_details WHERE shift_code = @shift_code and ";*/
    //        myConnection.Open();
    //        SqlCommand cmd = new SqlCommand(sqlStatement, myConnection);
    //        cmd.Parameters.AddWithValue("@pattern_code", ID);
    //        cmd.CommandType = CommandType.Text;
    //        cmd.ExecuteNonQuery();
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Delete Successfully');", true);
    //        }
    //    }
    //    catch (System.Data.SqlClient.SqlException ex)
    //    {
    //        if (ex.Number == 547)
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Unable to Delete. transaction Exists.');", true);

    //        }
    //        else
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);

    //        }
    //    }
    //    finally
    //    {
    //        myConnection.Close();
    //    }
    //}
    public void delete_tables()
    {
        myConnection.Open();
        cmd = new SqlCommand("Delete from shift_balance where monthyear='" + txt_monyear.Text + "' and pn_branchID = '" + employee.BranchId + "';Delete from shift_month where monthyear='" + txt_monyear.Text + "' and pn_branchID = '" + employee.BranchId + "'", myConnection);
        cmd.ExecuteNonQuery();
        myConnection.Close();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow Gvrow = GridView1.Rows[e.RowIndex];
        if (Gvrow != null)
        {
            string epatcode = ((TextBox)Gvrow.FindControl("txt_editpattern")).Text;
            string escode1 = ((DropDownList)Gvrow.FindControl("ddl_editshiftcode1")).Text;
            string edays1 = ((TextBox)Gvrow.FindControl("lbl_editdays1")).Text;
            string escode2 = ((DropDownList)Gvrow.FindControl("ddl_editshiftcode2")).Text;
            string edays2 = ((TextBox)Gvrow.FindControl("lbl_editdays2")).Text;
            string escode3 = ((DropDownList)Gvrow.FindControl("ddl_editshiftcode3")).Text;
            string edays3 = ((TextBox)Gvrow.FindControl("lbl_editdays3")).Text;
            myConnection.Open();
            cmd = new SqlCommand("update shift_pattern set pattern_code='" + epatcode + "', shift_code1='" + escode1 + "', days1='" + edays1 + "', shift_code2='" + escode2 + "', days2= '" + edays2 + "' , shift_code3='" + escode3 + "' where pattern_code='" + epatcode + "'", myConnection);
            cmd.ExecuteNonQuery();
            myConnection.Close();
            GridView1.EditIndex = -1; // turn to edit mode
            if (s_login_role == "a")
            {
                load();
            }
            else
            {
                load1();
            }
        }
    }
    protected void btn_bulksave_Click1(object sender, EventArgs e)
    {
        string monthyr = "", stdate = "", edate = "", strdate = "", wk1 = "", wk2 = "", mnyr = "", pattern = "", pmntyr = "";
        string scode1 = "", nday1 = "", scode2 = "", nday2 = "", scode3 = "", nday3 = "", scode4 = "", nday4 = "", scode5 = "", nday5 = "", scode6 = "", nday6 = "", scode7 = "", nday7 = "", scode8 = "", nday8 = "", balcode = "", scode = "";
        int ndayi1 = 0;
        string day1 = "", day2 = "", day3 = "", day4 = "", day5 = "", day6 = "", day7 = "", day8 = "";
        int count=0, days1 = 0, days2 = 0, days3 = 0, days4 = 0, days5 = 0, days6 = 0, days7 = 0, days8 = 0, days9 = 0, days10 = 0;

        int bday = Convert.ToInt32(txt_balance.Text);
        if (txt_monyear.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter month & year')", true);
        }
        else
        {
            if (CheckBoxList1.SelectedItem == null)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Select Employees');", true);
            }
            else
            {
                myConnection.Open();
                string wkk1 = "", wkk2 = "";
                for (int em = 0; em < CheckBoxList1.Items.Count; em++)
                {
                    _Value = CheckBoxList1.Items[em].Text.Split('-');
                    employee.EmployeeCode = _Value[0].ToString().Trim();
                    employee.FirstName = _Value[1].ToString().Trim();
                    if (CheckBoxList1.Items[em].Selected == true)
                    {

                        strdate = txt_monyear.Text;
                        string[] datespt = strdate.Split('/');
                        int month = Convert.ToInt32(datespt[0]);
                        int year = Convert.ToInt32(datespt[1]);
                        int pmnt = 0;
                        if (month == 01)
                        {
                            month = 12;
                            year = year - 1;
                            pmnt = month;
                        }
                        else
                        {
                             pmnt = month - 1;
                        }
                        if (pmnt < 10)
                        {
                            pmntyr = "0" + pmnt + "/" + year;
                        }
                        else
                        {
                            pmntyr = pmnt + "/" + year;
                        }
                        cmd = new SqlCommand("Delete from shift_balance where monthyear='" + txt_monyear.Text + "' and pn_employeecode='" + employee.EmployeeCode + "' and pn_branchID = '" + employee.BranchId + "'", myConnection);
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCommand("Delete from shift_month where monthyear='" + txt_monyear.Text + "' and pn_employeecode='" + employee.EmployeeCode + "' and pn_branchID = '" + employee.BranchId + "';Delete from shift_month where monthyear='" + txt_monyear.Text + "' and pn_branchID = '" + employee.BranchId + "'", myConnection);
                        cmd.ExecuteNonQuery();
                    }
                }
                for (int em = 0; em < CheckBoxList1.Items.Count; em++)
                {
                    _Value = CheckBoxList1.Items[em].Text.Split('-');
                    employee.EmployeeCode = _Value[0].ToString().Trim();
                    employee.FirstName = _Value[1].ToString().Trim();

                    if (CheckBoxList1.Items[em].Selected == true)
                    {

                        strdate = txt_monyear.Text;
                        string[] datespt = strdate.Split('/');
                        int month = Convert.ToInt32(datespt[0]);
                        int year = Convert.ToInt32(datespt[1]);
                        int pmnt = 0;
                        if (month == 01)
                        {
                            month = 12;
                            pmnt = month;
                            year = year - 1;
                        }
                        else
                        {
                            pmnt = month - 1;
                        }
                        int days_count = DateTime.DaysInMonth(year, pmnt);
                        string sdate = datespt[1] + "/" + pmnt + "/" + days_count.ToString();
                        string sdatee = days_count + "/" + pmnt + "/" + datespt[1].ToString();
                        DateTime sdate1 = Convert.ToDateTime(sdate);
                        DateTime sdate2 = Convert.ToDateTime(year.ToString() + "/" + (month - 1).ToString() + "/01");
                        //   System.TimeSpan day_count = sdate1.Subtract(sdate2);
                        // days_count = Convert.ToInt32((sdate1 - sdate2).TotalDays);
                        
                        cmd = new SqlCommand("select * from shift_month where pn_employeecode='" + employee.EmployeeCode + "' and date='" + sdate1.ToString("yyyy/MM/dd") + "' ", myConnection);
                        SqlDataReader rd_sp = cmd.ExecuteReader();
                        if (rd_sp.Read())
                        {
                            pattern = Convert.ToString(rd_sp["Shift_code"]);
                        }
                        if (pattern == "W")
                        {
                            DateTime newdate = sdate1.AddDays(-1);
                            cmd = new SqlCommand("select * from shift_month where pn_employeecode='" + employee.EmployeeCode + "' and date='" + newdate.ToString("yyyy/MM/dd") + "' ", myConnection);
                            SqlDataReader rdd = cmd.ExecuteReader();
                            if (rdd.Read())
                            {
                                pattern = Convert.ToString(rdd["Shift_code"]);
                                cmd1 = new SqlCommand("Select * from shift_pattern where pn_companyid = '" + employee.CompanyId + "' and pn_branchid= '" + employee.BranchId + "' and pattern_code = '" + pattern + "'", myConnection);
                                SqlDataReader sp = cmd1.ExecuteReader();
                                if (sp.Read())
                                {
                                    scode1 = Convert.ToString(sp["shift_code1"]);
                                    day1 = Convert.ToString(sp["days1"]);
                                    scode2 = Convert.ToString(sp["shift_code2"]);
                                    day2 = Convert.ToString(sp["days2"]);
                                    scode3 = Convert.ToString(sp["shift_code3"]);
                                    day3 = Convert.ToString(sp["days3"]);
                                    scode4 = Convert.ToString(sp["shift_code4"]);
                                    day4 = Convert.ToString(sp["days4"]);
                                    scode5 = Convert.ToString(sp["shift_code5"]);
                                    day5 = Convert.ToString(sp["days5"]);
                                    scode6 = Convert.ToString(sp["shift_code6"]);
                                    day6 = Convert.ToString(sp["days6"]);
                                    scode7 = Convert.ToString(sp["shift_code7"]);
                                    day7 = Convert.ToString(sp["days7"]);
                                    scode8 = Convert.ToString(sp["shift_code8"]);
                                    day8 = Convert.ToString(sp["days8"]);
                                }

                                pattern = scode3;

                            }
                        }
                        cmd = new SqlCommand("insert into  shift_balance values('" + employee.CompanyId + "','" + employee.BranchId + "','" + employee.EmployeeCode + "','" + employee.FirstName + "','" + txt_monyear.Text + "','" + pattern + "','" + txt_slot.Text + "','" + txt_balance.Text + "')", myConnection);
                        cmd.ExecuteNonQuery();

                        //catch(Exception ex)
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(ex)", true);
                        //}

                    }
                }
                for (int em = 0; em < CheckBoxList1.Items.Count; em++)
                {
                    string dat = "";
                    int d;
                    _Value = CheckBoxList1.Items[em].Text.Split('-');
                    employee.EmployeeCode = _Value[0].ToString().Trim();
                    employee.FirstName = _Value[1].ToString().Trim();
                    if (CheckBoxList1.Items[em].Selected == true)
                    {

                        strdate = txt_monyear.Text;
                        string[] datespt = strdate.Split('/');
                        int month = Convert.ToInt32(datespt[0]);
                        int year = Convert.ToInt32(datespt[1]);
                        int days_count = DateTime.DaysInMonth(year, month);
                        string sdate = datespt[1] + "/" + month + "/" + days_count.ToString();
                        string sdatee = days_count + "/" + month + "/" + datespt[1].ToString();
                        DateTime sdate1 = Convert.ToDateTime(sdate);
                        DateTime sdate2 = Convert.ToDateTime(year.ToString() + "/" + (month - 1).ToString() + "/01");
                        string patternn = "";
                        //   System.TimeSpan day_count = sdate1.Subtract(sdate2);
                        cmd = new SqlCommand("select * from shift_balance where pn_employeecode='" + employee.EmployeeCode + "' and monthyear='" + txt_monyear.Text + "'", myConnection);
                        SqlDataReader rd = cmd.ExecuteReader();
                        if (rd.Read())
                        {
                            patternn = Convert.ToString(rd["Pattern_code"]);
                        }
                        
                        // string patrn = patternn.ToString();
                        if (s_login_role == "a")
                        {
                            //cmd = new SqlCommand("Select week_off1 , week_off2 from attendance_ceiling where pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + ddl_branch.Text + "'", myConnection);
                        }
                        if (s_login_role == "h")
                        {
                            cmd = new SqlCommand("Select week_off1 , week_off2 from attendance_ceiling where pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
                        }
                        SqlDataReader rdw = cmd.ExecuteReader();


                        if (rdw.Read())
                        {
                            wkk1 = Convert.ToString(rdw[0]);
                            wkk2 = Convert.ToString(rdw[1]);
                        }

                        string f1 = "01/" + strdate;
                        string f2 = "02/" + strdate;
                        //string f1 = datespt[0] + "/01/" + datespt[1];
                        DateTime sun = Convert.ToDateTime(f1);
                        DateTime sat = Convert.ToDateTime(f2);
                        string dow = sun.DayOfWeek.ToString();
                        string dow2 = sat.DayOfWeek.ToString();
                        //if (wk1 != wk2 && (dow == wk1 || dow == wk2))
                        if (dow == wkk1)
                        {
                            bday = bday + 1;
                        }
                        if (dow == wkk2)
                        {
                            bday = bday + 1;
                        }

                        // End
                        int f, g, h, i, j, k, l, m, n, o, p, r, s, t, z;
                        int enddate = System.DateTime.DaysInMonth(year, month);
                        cmd1 = new SqlCommand("Select * from shift_pattern where pn_companyid = '" + employee.CompanyId + "' and pn_branchid= '" + employee.BranchId + "' and pattern_code = '" + patternn + "'", myConnection);
                        SqlDataReader sp = cmd1.ExecuteReader();
                        if (sp.Read())
                        {
                            scode1 = Convert.ToString(sp["shift_code1"]);
                            day1 = Convert.ToString(sp["days1"]);
                            scode2 = Convert.ToString(sp["shift_code2"]);
                            day2 = Convert.ToString(sp["days2"]);
                            scode3 = Convert.ToString(sp["shift_code3"]);
                            day3 = Convert.ToString(sp["days3"]);
                            scode4 = Convert.ToString(sp["shift_code4"]);
                            day4 = Convert.ToString(sp["days4"]);
                            scode5 = Convert.ToString(sp["shift_code5"]);
                            day5 = Convert.ToString(sp["days5"]);
                            scode6 = Convert.ToString(sp["shift_code6"]);
                            day6 = Convert.ToString(sp["days6"]);
                            scode7 = Convert.ToString(sp["shift_code7"]);
                            day7 = Convert.ToString(sp["days7"]);
                            scode8 = Convert.ToString(sp["shift_code8"]);
                            day8 = Convert.ToString(sp["days8"]);
                        }
                        //else
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Shift allocated for this employees');", true);

                        //}
                        for (d = 1; d <= enddate; d++)
                        {
                            dat = Convert.ToString(month + "/" + d + "/" + year);
                            DateTime ch = Convert.ToDateTime(year + "/" + month + "/" + d);
                            string datee = Convert.ToString(month + "/" + year);
                            if (wkk1 != wkk2 && (dow == wkk1 || dow == wkk2) && ch == sun)
                            {
                                scode1 = "W";
                            }
                            else
                            {
                                scode1 = Convert.ToString(sp["shift_code1"]);
                            }
                            try
                            {

                                cmd = new SqlCommand("insert into shift_month values('" + employee.CompanyId + "' , '" + employee.BranchId + "'  , '" + employee.EmployeeCode + "' ,'" + employee.FirstName + "', '" + txt_monyear.Text + "' , '" + dat + "' , '" + scode1 + "')", myConnection);
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(ex);", true);
                            }
                        }
                
                        for (d = 1; d <= enddate; d++)
                        {
                            dat = Convert.ToString(month + "/" + d + "/" + year);
                            DateTime ch = Convert.ToDateTime(year + "/" + month + "/" + d);
                            string datee = Convert.ToString(month + "/" + year);

                            dat = Convert.ToString(month + "/" + d + "/" + year);
                            _Val = CheckBoxList1.Items[em].Text.Split('-');
                            employee.EmployeeCode = _Val[0].ToString();
                            employee.FirstName = _Val[1].ToString();
                            int first;
                            if ((dow == wk1 || dow == wk2) && (dow2 == wk1 || dow2 == wk2))
                            {
                                first = bday;
                            }
                            else if ((dow == wk1 || dow == wk2) && dow2 != wk1 && dow2 != wk2)
                            {
                                first = bday + 6;
                            }
                            //Modification starts

                            first = bday + 1;

                            days2 = Convert.ToInt32(day2);

                            for (f = first; f < first + days2; f++)
                            {
                                dat = Convert.ToString(month + "/" + f + "/" + year);
                                cmd = new SqlCommand("update shift_month set Shift_Code='" + scode2 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                cmd.ExecuteNonQuery();
                            }
                            if (scode3 != "")
                            {
                                int second = f;
                                days3 = Convert.ToInt32(day3);
                                for (g = second; g < second + days3; g++)
                                {
                                    dat = Convert.ToString(month + "/" + g + "/" + year);
                                    cmd = new SqlCommand("update shift_month set Shift_Code='" + scode3 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                    cmd.ExecuteNonQuery();
                                }
                                int third = g;
                                days4 = Convert.ToInt32(day4);
                                for (i = third; i < third + days4; i++)
                                {
                                    dat = Convert.ToString(month + "/" + i + "/" + year);
                                    cmd = new SqlCommand("update shift_month set Shift_Code='" + scode4 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                    cmd.ExecuteNonQuery();
                                }

                                if (scode5 != "")
                                {
                                    int fourth = i;
                                    days5 = Convert.ToInt32(day5);
                                    for (j = fourth; j < fourth + days5; j++)
                                    {
                                        dat = Convert.ToString(month + "/" + j + "/" + year);
                                        cmd = new SqlCommand("update shift_month set Shift_Code='" + scode5 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                        cmd.ExecuteNonQuery();
                                    }
                                    int fifth = j;
                                    days6 = Convert.ToInt32(day6);
                                    for (k = fifth; k < fifth + days6; k++)
                                    {
                                        dat = Convert.ToString(month + "/" + k + "/" + year);
                                        cmd = new SqlCommand("update shift_month set Shift_Code='" + scode6 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                        cmd.ExecuteNonQuery();
                                    }
                                    if (scode7 != "")
                                    {
                                        int svn = k;
                                        days7 = Convert.ToInt32(day7);
                                        for (r = svn; r < svn + days7; r++)
                                        {
                                            dat = Convert.ToString(month + "/" + r + "/" + year);
                                            cmd = new SqlCommand("update shift_month set Shift_Code='" + scode7 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                            cmd.ExecuteNonQuery();
                                        }
                                        int egt = r;
                                        days8 = Convert.ToInt32(day8);
                                        for (s = egt; s < egt + days8; s++)
                                        {
                                            dat = Convert.ToString(month + "/" + s + "/" + year);
                                            cmd = new SqlCommand("update shift_month set Shift_Code='" + scode8 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                            cmd.ExecuteNonQuery();
                                        }
                                        if (s <= enddate)
                                        {
                                            int ecc = s;

                                            days9 = Convert.ToInt32(day7);
                                            for (z = ecc; z < ecc + days9; z++)
                                            {

                                                if (z <= enddate)
                                                {
                                                    dat = Convert.ToString(month + "/" + z + "/" + year);
                                                    cmd = new SqlCommand("update shift_month set Shift_Code='" + scode3 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                                    cmd.ExecuteNonQuery();
                                                }

                                            }
                                            if (z <= enddate)
                                            {
                                                int etg = z;
                                                days10 = Convert.ToInt32(day8);
                                                for (o = etg; o < etg + days8; o++)
                                                {
                                                    dat = Convert.ToString(month + "/" + o + "/" + year);
                                                    cmd = new SqlCommand("update shift_month set Shift_Code='" + scode2 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                                    cmd.ExecuteNonQuery();
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        int svn1 = k;
                                        days1 = Convert.ToInt32(day1);
                                        for (r = svn1; r < svn1 + days1; r++)
                                        {
                                            dat = Convert.ToString(month + "/" + r + "/" + year);
                                            cmd = new SqlCommand("update shift_month set Shift_Code='" + scode1 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                            cmd.ExecuteNonQuery();
                                        }
                                        int egt1 = r;
                                        days2 = Convert.ToInt32(day2);
                                        for (s = egt1; s < egt1 + days2; s++)
                                        {
                                            dat = Convert.ToString(month + "/" + s + "/" + year);
                                            cmd = new SqlCommand("update shift_month set Shift_Code='" + scode2 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                            cmd.ExecuteNonQuery();
                                        }
                                    }
                                }
                                else
                                {
                                    int fourth1 = i;
                                    days1 = Convert.ToInt32(day1);
                                    for (j = fourth1; j < fourth1 + days1; j++)
                                    {
                                        dat = Convert.ToString(month + "/" + j + "/" + year);
                                        cmd = new SqlCommand("update shift_month set Shift_Code='" + scode1 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                        cmd.ExecuteNonQuery();
                                    }
                                    int fifth1 = j;
                                    days2 = Convert.ToInt32(day2);
                                    for (k = fifth1; k < fifth1 + days2; k++)
                                    {
                                        dat = Convert.ToString(month + "/" + k + "/" + year);
                                        cmd = new SqlCommand("update shift_month set Shift_Code='" + scode2 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                        cmd.ExecuteNonQuery();
                                    }

                                    int sixth1 = k;
                                    days3 = Convert.ToInt32(day3);
                                    for (l = sixth1; l < sixth1 + days3; l++)
                                    {
                                        dat = Convert.ToString(month + "/" + l + "/" + year);
                                        cmd = new SqlCommand("update shift_month set Shift_Code='" + scode3 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                        cmd.ExecuteNonQuery();
                                    }
                                    int seventh1 = l;
                                    days4 = Convert.ToInt32(day4);
                                    for (m = seventh1; m < seventh1 + days4; m++)
                                    {
                                        dat = Convert.ToString(month + "/" + m + "/" + year);
                                        cmd = new SqlCommand("update shift_month set Shift_Code='" + scode4 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                        cmd.ExecuteNonQuery();
                                    }

                                    int eighth1 = m;
                                    days1 = Convert.ToInt32(day1);
                                    for (n = eighth1; n < eighth1 + days1; n++)
                                    {
                                        if (n <= enddate)
                                        {
                                            dat = Convert.ToString(month + "/" + n + "/" + year);
                                            cmd = new SqlCommand("update shift_month set Shift_Code='" + scode1 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                            cmd.ExecuteNonQuery();
                                        }
                                    }
                                    int ninth1 = n;
                                    days2 = Convert.ToInt32(day2);
                                    for (o = seventh1; o < seventh1 + days2; o++)
                                    {
                                        if (n <= enddate)
                                        {
                                            dat = Convert.ToString(month + "/" + o + "/" + year);
                                            cmd = new SqlCommand("update shift_month set Shift_Code='" + scode2 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                            cmd.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                int second1 = f;
                                days1 = Convert.ToInt32(day1);
                                for (g = second1; g < second1 + days1; g++)
                                {
                                    dat = Convert.ToString(month + "/" + g + "/" + year);
                                    cmd = new SqlCommand("update shift_month set Shift_Code='" + scode1 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                    cmd.ExecuteNonQuery();
                                }
                                int third1 = g;
                                days1 = Convert.ToInt32(day1);
                                days2 = Convert.ToInt32(day2);
                                for (h = third1; h <= enddate; h += days1 + 1)
                                {
                                    dat = Convert.ToString(month + "/" + h + "/" + year);
                                    cmd = new SqlCommand("update shift_month set Shift_Code='" + scode2 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                    cmd.ExecuteNonQuery();
                                    if (days2 == 2)
                                    {
                                        int x = h + 1;
                                        if (x <= enddate)
                                        {
                                            dat = Convert.ToString(month + "/" + x + "/" + year);
                                            cmd = new SqlCommand("update shift_month set Shift_Code='" + scode2 + "' where date='" + dat + "' and pn_EmployeeCode = '" + employee.EmployeeCode + "'", myConnection);
                                            cmd.ExecuteNonQuery();
                                            h++;
                                        }
                                    }
                                }
                            }
                        }
                        sp.Close();
                    }
                    count++;
                    


                    //Modification ends


                }
                if (count >= 1)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Shift updated Successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No shift allocated for this employess!');", true);

                }
            }
        }

                }
            
        
    
            //protected void btn_bulksave_Click1(object sender, EventArgs e)
            //{
            //    string monthyr = "", stdate = "", edate = "", strdate = "", wk1 = "", wk2 = "", mnyr = "", pattern = "", pmntyr = "";
            //    string scode1 = "", nday1 = "", scode2 = "", nday2 = "", scode3 = "", nday3 = "", scode4 = "", nday4 = "", scode5 = "", nday5 = "", scode6 = "", nday6 = "", scode7 = "", nday7 = "", scode8 = "", nday8 = "", balcode = "", scode = "";
            //    int ndayi1 = 0;
            //    if (txt_monyear.Text == "")
            //    {
            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter month & year')", true);
            //    }
            //    else
            //    {
            //        if (CheckBoxList1.SelectedItem == null)
            //        {
            //            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Select Employees');", true);
            //        }
            //        else
            //        {
            //            myConnection.Open();
            //            for (int em = 0; em < CheckBoxList1.Items.Count; em++)
            //            {
            //                _Value = CheckBoxList1.Items[em].Text.Split('-');
            //                employee.EmployeeCode = _Value[0].ToString().Trim();
            //                employee.FirstName = _Value[1].ToString().Trim();


            //                if (CheckBoxList1.Items[em].Selected == true)
            //                {

            //                    strdate = txt_monyear.Text;
            //                    string[] datespt = strdate.Split('/');
            //                    int month = Convert.ToInt32(datespt[0]);
            //                    int year = Convert.ToInt32(datespt[1]);
            //                    int pmnt = month - 1;
            //                    if (pmnt < 10)
            //                    {
            //                        pmntyr = "0" + pmnt + "/" + year;
            //                    }
            //                    else
            //                    {
            //                        pmntyr = pmnt + "/" + year;
            //                    }
            //                    cmd = new SqlCommand("Delete from shift_balance where monthyear='" + txt_monyear.Text + "' and pn_employeecode='" + employee.EmployeeCode + "' and pn_branchID = '" + employee.BranchId + "'", myConnection);
            //                    cmd.ExecuteNonQuery();
            //                    cmd = new SqlCommand("Delete from shift_month where monthyear='" + txt_monyear.Text + "' and pn_employeecode='" + employee.EmployeeCode + "' and pn_branchID = '" + employee.BranchId + "';Delete from shift_month where monthyear='" + txt_monyear.Text + "' and pn_branchID = '" + employee.BranchId + "'", myConnection);
            //                    cmd.ExecuteNonQuery();
            //                }
            //            }

            //            for (int em = 0; em < CheckBoxList1.Items.Count; em++)
            //            {
            //                _Value = CheckBoxList1.Items[em].Text.Split('-');
            //                employee.EmployeeCode = _Value[0].ToString().Trim();
            //                if (CheckBoxList1.Items[em].Selected == true)
            //                {

            //                    strdate = txt_monyear.Text;
            //                    string[] datespt = strdate.Split('/');
            //                    int month = Convert.ToInt32(datespt[0]);
            //                    int year = Convert.ToInt32(datespt[1]);
            //                    int pmnt = month - 1;
            //                    if (pmnt < 10)
            //                    {
            //                        pmntyr = "0" + pmnt + "/" + year;
            //                    }
            //                    else
            //                    {
            //                        pmntyr = pmnt + "/" + year;
            //                    }


            //                    cmd = new SqlCommand("select pattern_code from shift_balance where pn_employeecode='" + employee.EmployeeCode + "' and monthyear='" + pmntyr + "'", myConnection);
            //                    SqlDataReader rd_sp = cmd.ExecuteReader();
            //                    if (rd_sp.Read())
            //                    {
            //                        pattern = Convert.ToString(rd_sp["pattern_code"]);
            //                        string prev = pmntyr;
            //                        string moth = Convert.ToString(txt_monyear.Text);
            //                        // int yr = Convert.ToInt32(dt.Year);
            //                        string ecode = employee.EmployeeCode;
            //                       // delete_previous(moth, ecode);
            //                        int days_count = DateTime.DaysInMonth(year, month);
            //                        string sdate = datespt[1] + "/" + datespt[0] + "/" + days_count.ToString();
            //                        DateTime sdate1 = Convert.ToDateTime(sdate);
            //                        DateTime sdate2 = Convert.ToDateTime(year.ToString() + "/" + (month - 1).ToString() + "/01");
            //                        //   System.TimeSpan day_count = sdate1.Subtract(sdate2);
            //                        days_count = Convert.ToInt32((sdate1 - sdate2).TotalDays);
            //                        int dayno = (int)sdate2.DayOfWeek;
            //                        //int bal =Convert.ToInt32(txt_balance.Text);
            //                        int bal = 7 - dayno;
            //                        //int balnce = bal;
            //                        //  string pattern = ddl_patterncode.Text;
            //                        string slot = txt_slot.Text;
            //                        int temp = 0, dy = 0, nw = 0;
            //                        //nw = days_count / 7;
            //                        cmd1 = new SqlCommand("select * from shift_pattern where pattern_code='" + pattern + "'", myConnection);
            //                        SqlDataReader rd = cmd1.ExecuteReader();
            //                        if (rd.Read())
            //                        {
            //                            scode1 = Convert.ToString(rd["shift_code1"]);
            //                            scode = Convert.ToString(rd["shift_code1"]);
            //                            nday1 = Convert.ToString(rd["days1"]);
            //                            // ndayi1 = Convert.ToInt32(nday1);
            //                            scode2 = Convert.ToString(rd["shift_code2"]);
            //                            nday2 = Convert.ToString(rd["days2"]);
            //                            scode3 = Convert.ToString(rd["shift_code3"]);
            //                            nday3 = Convert.ToString(rd["days3"]);
            //                            scode4 = Convert.ToString(rd["shift_code4"]);
            //                            nday4 = Convert.ToString(rd["days4"]);
            //                            scode5 = Convert.ToString(rd["shift_code5"]);
            //                            nday5 = Convert.ToString(rd["days5"]);
            //                            scode6 = Convert.ToString(rd["shift_code6"]);
            //                            nday6 = Convert.ToString(rd["days6"]);
            //                            scode7 = Convert.ToString(rd["shift_code7"]);
            //                            nday7 = Convert.ToString(rd["days7"]);
            //                            scode8 = Convert.ToString(rd["shift_code8"]);
            //                            nday8 = Convert.ToString(rd["days8"]);
            //                        }
            //                        cmd1 = new SqlCommand("select * from paym_Employee where EmployeeCode='" + employee.EmployeeCode + "'", myConnection);
            //                        SqlDataReader read = cmd1.ExecuteReader();
            //                        if (read.Read())
            //                        {
            //                            employee.CompanyId = Convert.ToInt32(read["pn_CompanyID"]);
            //                            employee.BranchId = Convert.ToInt32(read["pn_BranchID"]);
            //                            employee.FirstName = Convert.ToString(read["Employee_First_Name"]);
            //                        }
            //                        cmd = new SqlCommand("select week_off1,week_off2 from attendance_ceiling where pn_CompanyID='" + employee.CompanyId + "'", myConnection);
            //                        SqlDataReader rd1 = cmd.ExecuteReader();
            //                        if (rd1.Read())
            //                        {
            //                            wk1 = Convert.ToString(rd1["week_off1"]);
            //                            wk2 = Convert.ToString(rd1["week_off2"]);
            //                        }
            //                        if (bal > 0)
            //                        {
            //                            nw = 1;
            //                        }
            //                        for (dy = 0; dy <= days_count; dy++)
            //                        {
            //                            DateTime dt = sdate2;

            //                            dt = dt.AddDays(dy);

            //                            //  string[] datspt = sdate.Split('/');
            //                            int mnth = Convert.ToInt32(dt.Month);
            //                            int yr = Convert.ToInt32(dt.Year);
            //                            int day = Convert.ToInt32(dt.Day);
            //                            string dat = Convert.ToString(month + "/" + day + "/" + yr);
            //                            //string dat1 = Convert.ToString(year + "/" + month + "/01");
            //                            string dat1 = Convert.ToString(day + "/" + month + "/" + yr);
            //                            DateTime dt1 = Convert.ToDateTime(dat1);
            //                            if (mnth < 10)
            //                            {
            //                                mnyr = "0" + month + "/" + yr;
            //                            }
            //                            else
            //                            {
            //                                mnyr = month + "/" + yr;
            //                            }
            //                            //if (dy != 1)
            //                            //{
            //                            //    dt = dt.AddDays(dy);
            //                            //}
            //                            string dayname = Convert.ToString(dt1.DayOfWeek);

            //                            //int rem = bal;
            //                            if ((bal > 0) && ((dayname != wk1) && (dayname != wk2)))
            //                            {
            //                                cmd = new SqlCommand("insert into shift_month values('" + employee.CompanyId + "' , '" + employee.BranchId + "' , '" + employee.EmployeeCode + "' , '" + employee.FirstName + "' , '" + mnyr + "' , '" + dat + "' , '" + scode1 + "')", myConnection);
            //                                cmd.ExecuteNonQuery();
            //                                bal = bal - 1;
            //                            }
            //                            else
            //                            {
            //                                if ((dayname == wk1) || (dayname == wk2))
            //                                {
            //                                    cmd = new SqlCommand("insert into shift_month values('" + employee.CompanyId + "' , '" + employee.BranchId + "' , '" + employee.EmployeeCode + "' , '" + employee.FirstName + "' , '" + mnyr + "' , '" + dat + "' , '" + scode2 + "')", myConnection);
            //                                    cmd.ExecuteNonQuery();
            //                                    bal = 0;
            //                                    temp = 1;
            //                                    nw = nw + 1;
            //                                    balcode = scode1;

            //                                }
            //                                else if ((nw == 6 || nw == 2 || nw == 10) && temp >= 1)
            //                                {
            //                                    //scode1 = scode;
            //                                    if (nday3 == "")
            //                                    {
            //                                        //ndayi1 = 0;
            //                                        ndayi1 = Convert.ToInt32(nday1);
            //                                    }
            //                                    else
            //                                    {
            //                                        ndayi1 = Convert.ToInt32(nday3);
            //                                    }
            //                                    if (ndayi1 > 0)
            //                                    {
            //                                        if (scode3 != "")
            //                                        {
            //                                            temp = temp + 1;
            //                                        }
            //                                        else
            //                                        {
            //                                            scode3 = scode1;
            //                                            temp = temp + 1;

            //                                        }

            //                                        cmd = new SqlCommand("insert into shift_month values('" + employee.CompanyId + "' , '" + employee.BranchId + "' , '" + employee.EmployeeCode + "' , '" + employee.FirstName + "' , '" + mnyr + "' , '" + dat + "' , '" + scode2 + "')", myConnection);
            //                                        cmd.ExecuteNonQuery();
            //                                        ndayi1 = ndayi1 - 1;
            //                                    }
            //                                }
            //                                else if ((nw == 7 || nw == 3 || nw == 11) && temp >= 1)
            //                                {

            //                                    if (nday5 == "")
            //                                    {
            //                                        //ndayi1 = 0;
            //                                        ndayi1 = Convert.ToInt32(nday1);
            //                                    }
            //                                    else
            //                                    {
            //                                         ndayi1 = Convert.ToInt32(nday5);
            //                                    }
            //                                    if (ndayi1 == 0)
            //                                    {
            //                                        ndayi1 = Convert.ToInt32(nday1);
            //                                    }
            //                                    if (ndayi1 > 0)
            //                                    {
            //                                        if (scode5 != "")
            //                                        {

            //                                            temp = temp + 1;
            //                                        }
            //                                        else
            //                                        {
            //                                            scode5 = scode1;
            //                                            temp = temp + 1;

            //                                        }

            //                                        cmd = new SqlCommand("insert into shift_month values('" + employee.CompanyId + "' , '" + employee.BranchId + "' , '" + employee.EmployeeCode + "' , '" + employee.FirstName + "' , '" + mnyr + "' , '" + dat + "' , '" + scode5 + "')", myConnection);
            //                                        cmd.ExecuteNonQuery();
            //                                        ndayi1 = ndayi1 - 1;
            //                                    }
            //                                }
            //                                else if ((nw == 8 || nw == 4) && temp >= 1)
            //                                {

            //                                    if (nday7 == "")
            //                                    {
            //                                        ndayi1 = 0;
            //                                    }
            //                                    else
            //                                    {
            //                                        ndayi1 = Convert.ToInt32(nday7);
            //                                    }
            //                                    if (ndayi1 == 0)
            //                                    {
            //                                        ndayi1 = Convert.ToInt32(nday1);
            //                                    }
            //                                    if (ndayi1 > 0)
            //                                    {
            //                                        if (scode7 != "")
            //                                        {
            //                                            temp = temp + 1; 
            //                                             cmd = new SqlCommand("insert into shift_month values('" + employee.CompanyId + "' , '" + employee.BranchId + "' , '" + employee.EmployeeCode + "' , '" + employee.FirstName + "' , '" + mnyr + "' , '" + dat + "' , '" + scode2 + "')", myConnection);
            //                                            cmd.ExecuteNonQuery();

            //                                        }
            //                                        else if (scode3 != "")
            //                                        {
            //                                            scode7 = scode3;
            //                                            cmd = new SqlCommand("insert into shift_month values('" + employee.CompanyId + "' , '" + employee.BranchId + "' , '" + employee.EmployeeCode + "' , '" + employee.FirstName + "' , '" + mnyr + "' , '" + dat + "' , '" + scode2 + "')", myConnection);
            //                                            cmd.ExecuteNonQuery();
            //                                            temp = temp + 1;
            //                                        }
            //                                        else
            //                                        {
            //                                            scode7 = scode1;
            //                                            temp = temp + 1;
            //                                          }
            //                                        cmd = new SqlCommand("insert into shift_month values('" + employee.CompanyId + "' , '" + employee.BranchId + "' , '" + employee.EmployeeCode + "' , '" + employee.FirstName + "' , '" + mnyr + "' , '" + dat + "' , '" + scode7 + "')", myConnection);
            //                                        cmd.ExecuteNonQuery();
            //                                        ndayi1 = ndayi1 - 1;
            //                                    }
            //                                }
            //                                else
            //                                {
            //                                    if ((nw == 1) || (nw == 9))
            //                                    {
            //                                        if (ndayi1 == 0)
            //                                        {
            //                                            ndayi1 = Convert.ToInt32(nday1);
            //                                            cmd = new SqlCommand("insert into shift_month values('" + employee.CompanyId + "' , '" + employee.BranchId + "' , '" + employee.EmployeeCode + "' , '" + employee.FirstName + "' , '" + mnyr + "' , '" + dat + "' , '" + scode2 + "')", myConnection);
            //                                            cmd.ExecuteNonQuery();
            //                                        }
            //                                        //if (scode1 == balcode)
            //                                        //{
            //                                        //    scode1 = scode3;
            //                                        //}
            //                                        if (ndayi1 > 0)
            //                                        {
            //                                            cmd = new SqlCommand("insert into shift_month values('" + employee.CompanyId + "' , '" + employee.BranchId + "' , '" + employee.EmployeeCode + "' , '" + employee.FirstName + "' , '" + mnyr + "' , '" + dat + "' , '" + scode1 + "')", myConnection);
            //                                            cmd.ExecuteNonQuery();
            //                                            ndayi1 = ndayi1 - 1;
            //                                        }
            //                                    }
            //                                    if (nw == 5)
            //                                    {
            //                                        if (ndayi1 == 0)
            //                                        {
            //                                            ndayi1 = Convert.ToInt32(nday1);
            //                                            cmd = new SqlCommand("insert into shift_month values('" + employee.CompanyId + "' , '" + employee.BranchId + "' , '" + employee.EmployeeCode + "' , '" + employee.FirstName + "' , '" + mnyr + "' , '" + dat + "' , '" + scode2 + "')", myConnection);
            //                                            cmd.ExecuteNonQuery();
            //                                        }

            //                                        if (ndayi1 > 0)
            //                                        {
            //                                            cmd = new SqlCommand("insert into shift_month values('" + employee.CompanyId + "' , '" + employee.BranchId + "' , '" + employee.EmployeeCode + "' , '" + employee.FirstName + "' , '" + mnyr + "' , '" + dat + "' , '" + scode2 + "')", myConnection);
            //                                            cmd.ExecuteNonQuery();
            //                                            ndayi1 = ndayi1 - 1;
            //                                        }
            //                                    }
            //                                }//else
            //                            } //else for bal>0

            //                        }//shift month schedule end 

            //                        cmd = new SqlCommand("insert into shift_balance values('" + employee.CompanyId + "','" + employee.BranchId + "','" + employee.EmployeeCode + "','" + employee.FirstName + "','" + mnyr + "','" + pattern + "','" + txt_slot.Text + "','" + bal + "')", myConnection);
            //                        cmd.ExecuteNonQuery();

            //                    }
            //                    else
            //                    {
            //                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Sorry!No Shift allotment for previous month');", true);
            //                    }
            //                }//checking whether the checkbox selected or not
            //            }//employee's loop
            //            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Shift updated Successfully');", true);
            //        }

            //    }
            //    myConnection.Close();
            //}
            //protected void btn_bulksave_Click1(object sender, EventArgs e)
            //{
            //    string day1 = "", day2 = "", day3 = "", day4 = "", day5 = "", day6 = "", day7 = "", day8 = "";
            //    int days1 = 0, days2 = 0, days3 = 0, days4 = 0, days5 = 0, days6 = 0, days7 = 0, days8 = 0, days9 = 0, days10 = 0;

            //    string monthyr = "", stdate = "", edate = "", strdate = "", wk1 = "", wk2 = "", mnyr = "", pattern = "", pmntyr = "";
            //    string scode1 = "", nday1 = "", scode2 = "", nday2 = "", scode3 = "", nday3 = "", scode4 = "", nday4 = "", scode5 = "", nday5 = "", scode6 = "", nday6 = "", scode7 = "", nday7 = "", scode8 = "", nday8 = "", balcode = "", scode = "";
            //    int ndayi1 = 0;
            //    if (txt_monyear.Text == "")
            //    {
            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter month & year')", true);
            //    }
            //    else
            //    {
            //        if (CheckBoxList1.SelectedItem == null)
            //        {
            //            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Select Employees');", true);
            //        }
            //        else
            //        {
            //            myConnection.Open();
            //            for (int em = 0; em < CheckBoxList1.Items.Count; em++)
            //            {
            //                _Value = CheckBoxList1.Items[em].Text.Split('-');
            //                employee.EmployeeCode = _Value[0].ToString().Trim();
            //                if (CheckBoxList1.Items[em].Selected == true)
            //                {

            //                    strdate = txt_monyear.Text;
            //                    string[] datespt = strdate.Split('/');
            //                    int month = Convert.ToInt32(datespt[0]);
            //                    int year = Convert.ToInt32(datespt[1]);
            //                    int pmnt = month - 1;
            //                    if (pmnt < 10)
            //                    {
            //                        pmntyr = "0" + pmnt + "/" + year;
            //                    }
            //                    else
            //                    {
            //                        pmntyr = pmnt + "/" + year;
            //                    }
            //                    cmd = new SqlCommand("Delete from shift_month where pn_employeecode='" + employee.EmployeeCode + "' and monthyear='" + txt_monyear.Text + "' and pn_companyId = '" + employee.CompanyId + "' and pn_BranchID = '" + employee.BranchId + "'", myConnection);
            //                    cmd.ExecuteNonQuery();
            //                    cmd = new SqlCommand("select pattern_code from shift_balance where pn_employeecode='" + employee.EmployeeCode + "' and monthyear='" + pmntyr + "'", myConnection);
            //                    SqlDataReader rd_sp = cmd.ExecuteReader();
            //                    if (rd_sp.Read())
            //                    {
            //                        pattern = Convert.ToString(rd_sp["pattern_code"]);
            //                        string prev = pmntyr;
            //                        string code = employee.EmployeeCode;
            //                        delete_previous(prev, code);
            //                        int days_count = DateTime.DaysInMonth(year, month);
            //                        string sdate = datespt[1] + "/" + datespt[0] + "/" + days_count.ToString();
            //                        DateTime sdate1 = Convert.ToDateTime(sdate);
            //                        DateTime sdate2 = Convert.ToDateTime(year.ToString() + "/" + (month - 1).ToString() + "/01");
            //                        //   System.TimeSpan day_count = sdate1.Subtract(sdate2);
            //                        days_count = Convert.ToInt32((sdate1 - sdate2).TotalDays);
            //                        int dayno = (int)sdate2.DayOfWeek;
            //                        int bal = 7 - dayno;
            //                        int balnce = bal;
            //                        //  string pattern = ddl_patterncode.Text;
            //                        string slot = txt_slot.Text;
            //                        int temp = 0, dy = 0, nw = 0;
            //                        //nw = days_count / 7;

            //                        //if (wk1 != wk2 && (dow == wk1 || dow == wk2))

            //                        cmd1 = new SqlCommand("select * from shift_pattern where pattern_code='" + pattern + "'", myConnection);
            //                        SqlDataReader rd = cmd1.ExecuteReader();
            //                        if (rd.Read())
            //                        {
            //                            scode1 = Convert.ToString(rd["shift_code1"]);
            //                            scode = Convert.ToString(rd["shift_code1"]);
            //                            nday1 = Convert.ToString(rd["days1"]);
            //                            // ndayi1 = Convert.ToInt32(nday1);
            //                            scode2 = Convert.ToString(rd["shift_code2"]);
            //                            nday2 = Convert.ToString(rd["days2"]);
            //                            scode3 = Convert.ToString(rd["shift_code3"]);
            //                            nday3 = Convert.ToString(rd["days3"]);
            //                            scode4 = Convert.ToString(rd["shift_code4"]);
            //                            nday4 = Convert.ToString(rd["days4"]);
            //                            scode5 = Convert.ToString(rd["shift_code5"]);
            //                            nday5 = Convert.ToString(rd["days5"]);
            //                            scode6 = Convert.ToString(rd["shift_code6"]);
            //                            nday6 = Convert.ToString(rd["days6"]);
            //                            scode7 = Convert.ToString(rd["shift_code7"]);
            //                            nday7 = Convert.ToString(rd["days7"]);
            //                            scode8 = Convert.ToString(rd["shift_code8"]);
            //                            nday8 = Convert.ToString(rd["days8"]);
            //                        }
            //                        cmd1 = new SqlCommand("select * from paym_Employee where EmployeeCode='" + employee.EmployeeCode + "'", myConnection);
            //                        SqlDataReader read = cmd1.ExecuteReader();
            //                        if (read.Read())
            //                        {
            //                            employee.CompanyId = Convert.ToInt32(read["pn_CompanyID"]);
            //                            employee.BranchId = Convert.ToInt32(read["pn_BranchID"]);
            //                            employee.FirstName = Convert.ToString(read["Employee_First_Name"]);
            //                        }
            //                        cmd = new SqlCommand("select week_off1,week_off2 from attendance_ceiling where pn_CompanyID='" + employee.CompanyId + "'", myConnection);
            //                        SqlDataReader rd1 = cmd.ExecuteReader();
            //                        if (rd1.Read())
            //                        {
            //                            wk1 = Convert.ToString(rd1["week_off1"]);
            //                            wk2 = Convert.ToString(rd1["week_off2"]);
            //                        }
            //                        if (bal > 0)
            //                        {
            //                            nw = 1;
            //                        }
            //                        for (dy = 0; dy <= days_count; dy++)
            //                        {
            //                            DateTime dt = sdate2;

            //                            dt = dt.AddDays(dy);

            //                            //  string[] datspt = sdate.Split('/');
            //                            int mnth = Convert.ToInt32(dt.Month);
            //                            int yr = Convert.ToInt32(dt.Year);
            //                            int day = Convert.ToInt32(dt.Day);
            //                            string dat = Convert.ToString(month + "/" + day + "/" + year);
            //                            //string dat1 = Convert.ToString(year + "/" + month + "/01");
            //                            string dat1 = Convert.ToString(day + "/" + month + "/" + year);
            //                            DateTime dt1 = Convert.ToDateTime(dat1);
            //                            if (mnth < 10)
            //                            {
            //                                mnyr = "0" + month + "/" + year;
            //                            }
            //                            else
            //                            {
            //                                mnyr = month + "/" + year;
            //                            }
            //                            //if (dy != 1)
            //                            //{
            //                            //    dt = dt.AddDays(dy);
            //                            //}
            //                            string dayname = Convert.ToString(dt1.DayOfWeek);

            //                            //int rem = bal;
            //                            if ((bal > 0) && ((dayname != wk1) && (dayname != wk2)))
            //                            {
            //                                cmd = new SqlCommand("insert into shift_month values('" + employee.CompanyId + "' , '" + employee.BranchId + "' , '" + employee.EmployeeCode + "' , '" + employee.FirstName + "' , '" + mnyr + "' , '" + dat + "' , '" + scode1 + "')", myConnection);
            //                                cmd.ExecuteNonQuery();
            //                                bal = bal - 1;
            //                            }
            //                            else
            //                            {
            //                                if ((dayname == wk1) || (dayname == wk2))
            //                                {
            //                                    cmd = new SqlCommand("insert into shift_month values('" + employee.CompanyId + "' , '" + employee.BranchId + "' , '" + employee.EmployeeCode + "' , '" + employee.FirstName + "' , '" + mnyr + "' , '" + dat + "' , '" + scode2 + "')", myConnection);
            //                                    cmd.ExecuteNonQuery();
            //                                    bal = 0;
            //                                    temp = 1;
            //                                    nw = nw + 1;
            //                                    balcode = scode1;

            //                                }
            //                                else if ((nw == 6 || nw == 2 || nw == 10) && temp >= 1)
            //                                {
            //                                    //scode1 = scode;
            //                                    if (nday3 == "")
            //                                    {
            //                                        //ndayi1 = 0;
            //                                        ndayi1 = Convert.ToInt32(nday1);
            //                                    }
            //                                    else
            //                                    {
            //                                        ndayi1 = Convert.ToInt32(nday3);
            //                                    }
            //                                    if (ndayi1 > 0)
            //                                    {
            //                                        if (scode3 != "")
            //                                        {
            //                                            temp = temp + 1;
            //                                        }
            //                                        else
            //                                        {
            //                                            scode3 = scode1;
            //                                            temp = temp + 1;

            //                                        }

            //                                        cmd = new SqlCommand("insert into shift_month values('" + employee.CompanyId + "' , '" + employee.BranchId + "' , '" + employee.EmployeeCode + "' , '" + employee.FirstName + "' , '" + mnyr + "' , '" + dat + "' , '" + scode3 + "')", myConnection);
            //                                        cmd.ExecuteNonQuery();
            //                                        ndayi1 = ndayi1 - 1;
            //                                    }
            //                                }
            //                                else if ((nw == 7 || nw == 3 || nw == 11) && temp >= 1)
            //                                {

            //                                    if (nday5 == "")
            //                                    {
            //                                        //ndayi1 = 0;
            //                                        ndayi1 = Convert.ToInt32(nday1);
            //                                    }
            //                                    else
            //                                    {
            //                                        ndayi1 = Convert.ToInt32(nday5);
            //                                    }
            //                                    if (ndayi1 == 0)
            //                                    {
            //                                        ndayi1 = Convert.ToInt32(nday1);
            //                                    }
            //                                    if (ndayi1 > 0)
            //                                    {
            //                                        if (scode5 != "")
            //                                        {

            //                                            temp = temp + 1;
            //                                        }
            //                                        else
            //                                        {
            //                                            scode5 = scode1;
            //                                            temp = temp + 1;

            //                                        }

            //                                        cmd = new SqlCommand("insert into shift_month values('" + employee.CompanyId + "' , '" + employee.BranchId + "' , '" + employee.EmployeeCode + "' , '" + employee.FirstName + "' , '" + mnyr + "' , '" + dat + "' , '" + scode5 + "')", myConnection);
            //                                        cmd.ExecuteNonQuery();
            //                                        ndayi1 = ndayi1 - 1;
            //                                    }
            //                                }
            //                                else if ((nw == 8 || nw == 4) && temp >= 1)
            //                                {

            //                                    if (nday7 == "")
            //                                    {
            //                                        ndayi1 = 0;
            //                                    }
            //                                    else
            //                                    {
            //                                        ndayi1 = Convert.ToInt32(nday7);
            //                                    }
            //                                    if (ndayi1 == 0)
            //                                    {
            //                                        ndayi1 = Convert.ToInt32(nday1);
            //                                    }
            //                                    if (ndayi1 > 0)
            //                                    {
            //                                        if (scode7 != "")
            //                                        {
            //                                            temp = temp + 1;
            //                                        }
            //                                        else if (scode3 != "")
            //                                        {
            //                                            scode7 = scode3;
            //                                            temp = temp + 1;
            //                                        }
            //                                        else
            //                                        {
            //                                            scode7 = scode1;
            //                                            temp = temp + 1;
            //                                        }
            //                                        cmd = new SqlCommand("insert into shift_month values('" + employee.CompanyId + "' , '" + employee.BranchId + "' , '" + employee.EmployeeCode + "' , '" + employee.FirstName + "' , '" + mnyr + "' , '" + dat + "' , '" + scode7 + "')", myConnection);
            //                                        cmd.ExecuteNonQuery();
            //                                        ndayi1 = ndayi1 - 1;
            //                                    }
            //                                }
            //                                else
            //                                {
            //                                    if ((nw == 1) || (nw == 9))
            //                                    {
            //                                        if (ndayi1 == 0)
            //                                        {
            //                                            ndayi1 = Convert.ToInt32(nday1);
            //                                        }
            //                                        //if (scode1 == balcode)
            //                                        //{
            //                                        //    scode1 = scode3;
            //                                        //}
            //                                        if (ndayi1 > 0)
            //                                        {
            //                                            cmd = new SqlCommand("insert into shift_month values('" + employee.CompanyId + "' , '" + employee.BranchId + "' , '" + employee.EmployeeCode + "' , '" + employee.FirstName + "' , '" + mnyr + "' , '" + dat + "' , '" + scode1 + "')", myConnection);
            //                                            cmd.ExecuteNonQuery();
            //                                            ndayi1 = ndayi1 - 1;
            //                                        }
            //                                    }
            //                                    if (nw == 5)
            //                                    {
            //                                        if (ndayi1 == 0)
            //                                        {
            //                                            ndayi1 = Convert.ToInt32(nday1);
            //                                        }

            //                                        if (ndayi1 > 0)
            //                                        {
            //                                            cmd = new SqlCommand("insert into shift_month values('" + employee.CompanyId + "' , '" + employee.BranchId + "' , '" + employee.EmployeeCode + "' , '" + employee.FirstName + "' , '" + mnyr + "' , '" + dat + "' , '" + scode1 + "')", myConnection);
            //                                            cmd.ExecuteNonQuery();
            //                                            ndayi1 = ndayi1 - 1;
            //                                        }
            //                                    }
            //                                }//else
            //                            } //else for bal>0

            //                        }//shift month schedule end 

            //                        cmd = new SqlCommand("insert into shift_balance values('" + employee.CompanyId + "','" + employee.BranchId + "','" + employee.EmployeeCode + "','" + employee.FirstName + "','" + mnyr + "','" + pattern + "','" + txt_slot.Text + "','" + balnce + "')", myConnection);
            //                        cmd.ExecuteNonQuery();


            //                    }
            //                    else
            //                    {
            //                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Sorry!No Shift allotment for previous month');", true);
            //                    }
            //                }//checking whether the checkbox selected or not
            //            }//employee's loop
            //            myConnection.Close();
            //        }
            //    }
            //}
      
    public void delete_previous(string mnt, string ecode)
    {
        try
        {
           
        //    myConnection.Open();
            cmd = new SqlCommand("Delete from shift_month where monthyear='" + mnt + "' and pn_EmployeeCode = '" + ecode + "'", myConnection);
            cmd.ExecuteNonQuery();
            // myConnection.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('error delete shift month');", true);
        }
    }


    protected void Monthchange(object sender, MonthChangedEventArgs e)
    {
        int mnt = e.NewDate.Month;
        int yr = e.NewDate.Year;
        string time = "";
        employee.EmployeeCode = ddl_empcode.SelectedValue;
        if (employee.EmployeeCode != "Select Employee")
        {
            if (e.NewDate.Month > e.PreviousDate.Month)
            {

                if (mnt < 10)
                {
                    time = "0" + mnt + "/" + yr;
                }
                else
                {
                    time = mnt + "/" + yr;
                }
                txt_monyear.Text = time;
            }

            else if (e.NewDate.Month < e.PreviousDate.Month)
            {

                if (mnt < 10)
                {
                    time = "0" + mnt + "/" + yr;
                }
                else
                {
                    time = mnt + "/" + yr;
                }
                txt_monyear.Text = time;
            }
            else
            {
                //Message.Text = "You moved backwards one month.";
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('You moved backwards one month.');", true);
                mnt = Calendar1.VisibleDate.Month;
                yr = Calendar1.VisibleDate.Year;
            }
            _DayEventsTable = new DataTable();
            _DayEventsTable.Columns.Add("Date");
            _DayEventsTable.Columns.Add("Title");
            //myConnection.Close();
            myConnection.Open();

            cmd = new SqlCommand("Select * from shift_month where pn_companyid = '" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "' and pn_Employeecode='" + ddl_empcode.SelectedValue + "' and monthyear='" + txt_monyear.Text + "' ", myConnection);
            rea = cmd.ExecuteReader();
            while (rea.Read())
            {
                _DayEventsTable.Rows.Add(Convert.ToString(rea[5]), Convert.ToString(rea[6]));
            }

            foreach (DataRow Row in _DayEventsTable.Rows)
            {
                string Date = Row["Date"].ToString();
                string Title = Row["Title"].ToString();
                //if (Date == e.NewDate.Date.ToString())
                //{
                //    e.Cell.Controls.Add(new LiteralControl("</br>" + Title));
                //}
            }
        }
        else
        {
            //  ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('select first');", true);
        }

        myConnection.Close();
    }
}

//string datee;

//if (month == 12)
//{
//    month = 12;




//}
//else if (month == 11)
//{
//    month = 11;

//     chh = Convert.ToDateTime(year + "/" + month + "/" + d);


//}
//else if (month == 10)
//{
//    month = 10;

//     chh = Convert.ToDateTime(year + "/" + month + "/" + d);


//}
//else
//{
//    dat = Convert.ToString("0" + month + "/" + year);
//           // chh = Convert.ToDateTime(year + "/" + "0" + month + "/" + "0" + d);
//            chh = Convert.ToDateTime(year + "/" + month+"/"  +d);

//    datee = Convert.ToString("0" + month + "/" + year);
//}
//        mnt = Convert.ToString(datee);