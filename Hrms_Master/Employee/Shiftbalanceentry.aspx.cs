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

    string[] _Value , _Val;
    string s_login_role;
    string s_form = "";
    DataSet ds_userrights;
    DataTable _DayEventsTable;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        
        if (!IsPostBack)
        {

            switch (s_login_role)
            {
                case "a": load_admin();
                   
                    break;

                case "h":

                    load();
                    access();
                    ddl_branch.Visible = false;
                    break;

                case "u": s_form = "16";
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
    }


    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        _DayEventsTable = new DataTable();
        _DayEventsTable.Columns.Add("Date");
        _DayEventsTable.Columns.Add("Title");

        myConnection.Open();
        cmd = new SqlCommand("Select * from shift_month where pn_companyid = '" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "' and pn_Employeename='"+ddl_empcode.SelectedItem.Text+"' and monthyear='"+txt_monyear.Text+"' ", myConnection);
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
                ddl_patterncode.SelectedItem.Text = rea[0].ToString();
                txt_slot.Text = rea[1].ToString();
                txt_balance.Text = rea[2].ToString();
            }
        }
        catch
        {
            lbl_Error.Text = "Check the Month/Year you have entered";
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
            // loop all data rows
            foreach (DataControlFieldCell cell in e.Row.Cells)
            {
                //TextBox tbox = e.Row.FindControl("txt_pattern") as TextBox;
                //tbox.Attributes.Add("onKeyDown", "javascript:if(window.event.keycode == 13)return false;}");
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

                if (i == 17)
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

    public void delete_tables()
    {
        myConnection.Open();
        cmd = new SqlCommand("Delete from shift_balance where monthyear='" + txt_monyear.Text + "' and pn_branchID = '" + employee.BranchId + "';Delete from shift_month where monthyear='" + txt_monyear.Text + "' and pn_branchID = '" + employee.BranchId + "'", myConnection);
        cmd.ExecuteNonQuery();
        myConnection.Close();
    }

    protected void txt_balance_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(txt_balance.Text) > 7)
        {
            lbl_Error.Text = " Balance days must be less than 7";
        }
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (txt_monyear.Text == "" || ddl_patterncode.Text == "Select" || txt_balance.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter all details.');", true);
        }
        else if (Convert.ToInt32(txt_balance.Text) > 7)
        {
            lbl_Error.Text = " Balance days must be less than 7";

        }
        else
        {
            if (CheckBoxList1.SelectedItem == null)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Select Employees');", true);
            }
            else
            {
                delete_tables();
                myConnection.Open();
                for (int em = 0; em < CheckBoxList1.Items.Count; em++)
                {
                    _Value = CheckBoxList1.Items[em].Text.Split('-');
                    employee.EmployeeCode = _Value[1].ToString();
                    if (CheckBoxList1.Items[em].Selected == true)
                    {
                        cmd = new SqlCommand("insert into shift_balance (pn_companyid,pn_branchid,pn_employeecode,pn_employeename,monthyear,pattern_code,slot,balance_days) values ('" + employee.CompanyId + "' , '" + employee.BranchId + "' , '" + CheckBoxList1.Items[em].Value + "' , '" + employee.EmployeeCode + "','" + txt_monyear.Text + "','" + ddl_patterncode.Text + "' , '" + txt_slot.Text + "' , '" + txt_balance.Text + "')", myConnection);
                        cmd.ExecuteNonQuery();
                    }
                }
                //DateTime sdate = Convert.ToDateTime("01/"+txt_monyear.Text);
                string scode1 = "", scode2 = "", scode3 = "", scode4 = "", scode5 = "", scode6 = "", scode7 = "", scode8 = "", dat = "";
                string day1 = "", day2 = "", day3 = "", day4 = "", day5 = "", day6 = "", day7 = "", day8 = "";
                int days1 = 0, days2 = 0, days3 = 0, days4 = 0, days5 = 0, days6 = 0, days7 = 0, days8 = 0;
                string strdate = txt_monyear.Text;
                string[] datespt = strdate.Split('/');
                int bday = Convert.ToInt32(txt_balance.Text);
                int month = Convert.ToInt32(datespt[0]);
                int year = Convert.ToInt32(datespt[1]);

                // Checking the first day of the month
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
                    bday = bday+1;
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
                int d, f, g, h, i, j, k, l, m, n, o, p, r, s, t;
                for (d = 1; d <= enddate; d++)
                {
                    dat = Convert.ToString(month + "/" + d + "/" + year);
                    DateTime ch = Convert.ToDateTime(year + "/" + month + "/" + d);
                    for (int ec = 0; ec < CheckBoxList1.Items.Count; ec++)
                    {
                        if (CheckBoxList1.Items[ec].Selected == true)
                        {
                            _Val = CheckBoxList1.Items[ec].Text.Split('-');
                            employee.EmployeeCode = _Val[1].ToString();
                            if (wk1 != wk2 && (dow == wk1 || dow == wk2) && ch == sun)
                            {
                                scode1 = "WW";
                            }
                            else
                            {
                                scode1 = Convert.ToString(rd_sp["shift_code1"]);
                            }
                            cmd = new SqlCommand("insert into shift_month values('" + employee.CompanyId + "' , '" + employee.BranchId + "' , '" + CheckBoxList1.Items[ec].Value + "' , '" + employee.EmployeeCode + "' , '" + txt_monyear.Text + "' , '" + dat + "' , '" + scode1 + "')", myConnection);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                int first = bday + 1;
                days2 = Convert.ToInt32(day2);
                for (f = first; f < first + days2; f++)
                {
                    dat = Convert.ToString(month + "/" + f + "/" + year);
                    cmd = new SqlCommand("update shift_month set Shift_Code='" + scode2 + "' where date='" + dat + "'", myConnection);
                    cmd.ExecuteNonQuery();
                }

                if (scode3 != "")
                {
                    int second = f;
                    days3 = Convert.ToInt32(day3);
                    for (g = second; g < second + days3; g++)
                    {
                        dat = Convert.ToString(month + "/" + g + "/" + year);
                        cmd = new SqlCommand("update shift_month set Shift_Code='" + scode3 + "' where date='" + dat + "'", myConnection);
                        cmd.ExecuteNonQuery();
                    }
                    int third = g;
                    days4 = Convert.ToInt32(day4);
                    for (i = third; i < third + days4; i++)
                    {
                        dat = Convert.ToString(month + "/" + i + "/" + year);
                        cmd = new SqlCommand("update shift_month set Shift_Code='" + scode4 + "' where date='" + dat + "'", myConnection);
                        cmd.ExecuteNonQuery();
                    }

                    if (scode5 != "")
                    {
                        int fourth = i;
                        days5 = Convert.ToInt32(day5);
                        for (j = fourth; j < fourth + days5; j++)
                        {
                            dat = Convert.ToString(month + "/" + j + "/" + year);
                            cmd = new SqlCommand("update shift_month set Shift_Code='" + scode5 + "' where date='" + dat + "'", myConnection);
                            cmd.ExecuteNonQuery();
                        }
                        int fifth = j;
                        days6 = Convert.ToInt32(day6);
                        for (k = fifth; k < fifth + days6; k++)
                        {
                            dat = Convert.ToString(month + "/" + k + "/" + year);
                            cmd = new SqlCommand("update shift_month set Shift_Code='" + scode6 + "' where date='" + dat + "'", myConnection);
                            cmd.ExecuteNonQuery();
                        }
                        if (scode7 != "")
                        {
                            int svn = k;
                            days7 = Convert.ToInt32(day7);
                            for (r = svn; r < svn + days7; r++)
                            {
                                dat = Convert.ToString(month + "/" + r + "/" + year);
                                cmd = new SqlCommand("update shift_month set Shift_Code='" + scode7 + "' where date='" + dat + "'", myConnection);
                                cmd.ExecuteNonQuery();
                            }
                            int egt = r;
                            days8 = Convert.ToInt32(day8);
                            for (s = egt; s < egt + days8; s++)
                            {
                                dat = Convert.ToString(month + "/" + s + "/" + year);
                                cmd = new SqlCommand("update shift_month set Shift_Code='" + scode8 + "' where date='" + dat + "'", myConnection);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            int svn1 = k;
                            days1 = Convert.ToInt32(day1);
                            for (r = svn1; r < svn1 + days1; r++)
                            {
                                dat = Convert.ToString(month + "/" + r + "/" + year);
                                cmd = new SqlCommand("update shift_month set Shift_Code='" + scode1 + "' where date='" + dat + "'", myConnection);
                                cmd.ExecuteNonQuery();
                            }
                            int egt1 = r;
                            days2 = Convert.ToInt32(day2);
                            for (s = egt1; s < egt1 + days2; s++)
                            {
                                dat = Convert.ToString(month + "/" + s + "/" + year);
                                cmd = new SqlCommand("update shift_month set Shift_Code='" + scode2 + "' where date='" + dat + "'", myConnection);
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
                            cmd = new SqlCommand("update shift_month set Shift_Code='" + scode1 + "' where date='" + dat + "'", myConnection);
                            cmd.ExecuteNonQuery();
                        }
                        int fifth1 = j;
                        days2 = Convert.ToInt32(day2);
                        for (k = fifth1; k < fifth1 + days2; k++)
                        {
                            dat = Convert.ToString(month + "/" + k + "/" + year);
                            cmd = new SqlCommand("update shift_month set Shift_Code='" + scode2 + "' where date='" + dat + "'", myConnection);
                            cmd.ExecuteNonQuery();
                        }

                        int sixth1 = k;
                        days3 = Convert.ToInt32(day3);
                        for (l = sixth1; l < sixth1 + days3; l++)
                        {
                            dat = Convert.ToString(month + "/" + l + "/" + year);
                            cmd = new SqlCommand("update shift_month set Shift_Code='" + scode3 + "' where date='" + dat + "'", myConnection);
                            cmd.ExecuteNonQuery();
                        }
                        int seventh1 = l;
                        days4 = Convert.ToInt32(day4);
                        for (m = seventh1; m < seventh1 + days4; m++)
                        {
                            dat = Convert.ToString(month + "/" + m + "/" + year);
                            cmd = new SqlCommand("update shift_month set Shift_Code='" + scode4 + "' where date='" + dat + "'", myConnection);
                            cmd.ExecuteNonQuery();
                        }

                        int eighth1 = m;
                        days1 = Convert.ToInt32(day1);
                        for (n = eighth1; n < eighth1 + days1; n++)
                        {
                            if (n <= enddate)
                            {
                                dat = Convert.ToString(month + "/" + n + "/" + year);
                                cmd = new SqlCommand("update shift_month set Shift_Code='" + scode1 + "' where date='" + dat + "'", myConnection);
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
                                cmd = new SqlCommand("update shift_month set Shift_Code='" + scode2 + "' where date='" + dat + "'", myConnection);
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
                        cmd = new SqlCommand("update shift_month set Shift_Code='" + scode1 + "' where date='" + dat + "'", myConnection);
                        cmd.ExecuteNonQuery();
                    }
                    int third1 = g;
                    days1 = Convert.ToInt32(day1);
                    days2 = Convert.ToInt32(day2);
                    for (h = third1; h <= enddate; h += days1 + 1)
                    {
                        dat = Convert.ToString(month + "/" + h + "/" + year);
                        cmd = new SqlCommand("update shift_month set Shift_Code='" + scode2 + "' where date='" + dat + "'", myConnection);
                        cmd.ExecuteNonQuery();
                        if (days2 == 2)
                        {
                            int x = h + 1;
                            if (x <= enddate)
                            {
                                dat = Convert.ToString(month + "/" + x + "/" + year);
                                cmd = new SqlCommand("update shift_month set Shift_Code='" + scode2 + "' where date='" + dat + "'", myConnection);
                                cmd.ExecuteNonQuery();
                                h++;
                            }
                        }
                    }
                }
                rd_sp.Close();
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
        txt_slot.Text = "";
        ddl_patterncode.SelectedIndex = 0;
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {
        myConnection.Open();
        cmd = new SqlCommand("set dateformat dmy;update shift_month set shift_code = '" + ddl_shift.SelectedItem.Text + "' where date = '" + txt_sdate.Text + "' and pn_branchID = '" + employee.BranchId + "';set dateformat mdy;", myConnection);
        cmd.ExecuteNonQuery();
        myConnection.Close();
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Shift Updated Successfully');", true);
    }
    protected void btn_rset_Click(object sender, EventArgs e)
    {
        txt_sdate.Text = "";
        ddl_shift.SelectedIndex = 0;
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
                lbl_Error.Text = "Enter all the mandatory fields";
                return;
            }
            AddNewRecord(branch, patcode, scode1, days1, scode2, days2, scode3, days3, scode4, days4, scode5, days5, scode6, days6, scode7, days7, scode8, days8);
        }
    }

    private void AddNewRecord(string branch, string patcode, string scode1, string days1, string scode2, string days2, string scode3, string days3, string scode4, string days4, string scode5, string days5, string scode6, string days6, string scode7, string days7, string scode8, string days8)
    {
        try
        {
            string query = @"INSERT INTO shift_pattern (pn_CompanyID, pn_BranchID, pattern_code, shift_code1,days1,shift_code2,days2,shift_code3,days3,shift_code4,days4,shift_code5,days5,shift_code6,days6,shift_code7,days7,shift_code8,days8) VALUES('" + employee.CompanyId + "','" + branch + "','" + patcode + "','" + scode1 + "','" + days1 + "','" + scode2 + "','" + days2 + "','" + scode3 + "','" + days3 + "','" + scode4 + "','" + days4 + "','" + scode5 + "','" + days5 + "','" + scode6 + "','" + days6 + "','" + scode7 + "','" + days7 + "','" + scode8 + "','" + days8 + "')";

            SqlCommand myCommand = new SqlCommand(query, myConnection);

            myConnection.Open();

            myCommand.ExecuteNonQuery();

            myConnection.Close();

            if (s_login_role == "a")
            {
                load();
            }
            else
            {
                load1();
            }

        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Pattern Code Already Exists";
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
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow Gvrow = GridView1.Rows[e.RowIndex];
        //Label8.Text = Gvrow.ToString();
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
            string msg = "Deletion Error:";
            msg += ex.Message;
            throw new Exception(msg);

        }
        finally
        {
            myConnection.Close();
        }
    }
}
