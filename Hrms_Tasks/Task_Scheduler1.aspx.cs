using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ePayHrms.Login;
using ePayHrms.Connection;
using ePayHrms.Candidate;
using System.IO;
using ePayHrms.Company;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ePayHrms.BE.Recruitment;
using ePayHrms.Employee;
using System.Data.SqlClient;

public partial class Hrms_Tasks_Default : System.Web.UI.Page
{
    private SqlConnection _connection;
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    //private SqlConnection _con;
    //ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    SqlDataReader rea;
    SqlDataReader rea1;
    SqlDataAdapter ada = new SqlDataAdapter();
    SqlDataAdapter ada1 = new SqlDataAdapter();
    Company company = new Company();
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    Candidate c = new Candidate();

    Collection<Candidate> WorkHistoryList;
    string eid, doco;
    //Collection<Employee> AllowanceList;
    Collection<Company> CompanyList;
    Collection<Employee> EmployeeList;

    string s_login_role;
    int ddl_i, grk;
    string _path, _Value;
    static string filterval, fdata, crnt;
    string s_form = "";
    DataSet ds_userrights;


    protected void Page_Load(object sender, EventArgs e)
    {
        

        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
       // employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        c.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        // Label8.Text = (string)Session["Login_Name"] + "!";
        // GridView1.Visible = false;
        // rdobtn1.Checked = true;

        if (!IsPostBack)
        {
            CompanyList = company.fn_getCompany();

            filterdata.Enabled = false;
            // btnapply.Enabled = false;

            if (CompanyList.Count > 0)
            {
                switch (s_login_role)
                {
                    case "a":
                        admin();
                        break;

                    case "h":
                        //Button2.Visible = false;
                        //Button3.Visible = false;
                        //Button4.Visible = false;
                        //btn_ptasks.Visible = false;
                        hr();
                        access();
                        break;

                    case "r":
                        employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
                        reporting();
                        break;

                    case "e": Response.Redirect("../Hrms_Tasks/Tasks_List.aspx");
                        break;

                    case "u":
                        s_form = "46";

                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            hr();
                        }
                        else
                        {
                            Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator.";
                            Response.Redirect("~/Company_Home.aspx");
                        }
                        break;
                    default:
                        Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                        Response.Redirect("../Hrms_Master/Common/Common_Home.aspx");
                        break;
                }
            }
            else
            {
                Response.Cookies["Msg_Session"].Value = "Create Company";
                Response.Redirect("~/Company_Home.aspx");
            }
            //  rdobtn1.Checked = true;
            Button5.Visible = false;
            ddlfilter_populate();
           //sortdata();
        }

    }

    public void admin()
    {
        GridView1.Attributes.Add("bordercolor", "darkgray");

        SqlDataAdapter ad1 = new SqlDataAdapter("SELECT * FROM task_schedule where pn_BranchID ='" + employee.BranchId + "'and pn_CompanyID='" + employee.CompanyId + "'", myConnection);

        DataSet ds = new DataSet();

        ad1.Fill(ds, "task_schedule");

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
        GridView1.FooterRow.Visible = false;
    }

    public void hr()
    {
        GridView1.Attributes.Add("bordercolor", "Darkgray");
        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM task_schedule where Status!='Closed' and pn_BranchID = '" + employee.BranchId + "'", myConnection);
        DataSet ds = new DataSet();
        ad.Fill(ds, "task_schedule");
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

    public void reporting()
    {
        employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
        GridView1.Attributes.Add("bordercolor", "Darkgray");
       

        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM task_schedule where Status!='Closed' and pn_BranchID = '" + employee.BranchId + "' and assignedby = '" + employee.EmployeeId + "'", myConnection);

        DataSet ds = new DataSet();

        ad.Fill(ds, "task_schedule");

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
        ViewState["dirState"] = ds.Tables[0];
        ViewState["sortdr"] = "Asc";
        DropDownList ddassi = (DropDownList)GridView1.FooterRow.FindControl("DropDownList1");
        ddassi.Items.Clear();
        employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
        EmployeeList = employee.fn_getEmployeeReporting(employee);
        if (EmployeeList.Count > 0)
        {
            for (int c = -1; c < EmployeeList.Count; c++)
            {
                if (c == -1)
                {
                    ListItem emp_li = new ListItem();
                    emp_li.Text = "Select Employee";
                    emp_li.Value = "0";
                    ddassi.Items.Add(emp_li);
                }
                else
                {
                    ListItem emp_li = new ListItem();
                    emp_li.Text = EmployeeList[c].LastName;
                    emp_li.Value = EmployeeList[c].EmployeeId.ToString();
                    ddassi.Items.Add(emp_li);
                }
            }
        }


    }

    public void access()
    {
        _connection = con.fn_Connection();
        _connection.Open();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=4 and section_view='No'", _connection);
        SqlDataReader rdrview = cmd.ExecuteReader();
        if (rdrview.Read())
        {
            Response.Write("<script language='javascript'>alert('Permission Restricted. Please Contact Administrator.');window.location='~/Company_Home.aspx';</script>");
        }
        rdrview.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=4 and section_edit='No'", _connection);
        SqlDataReader rdredit = cmd.ExecuteReader();
        if (rdredit.Read())
        {

            for (int b = 0; b < GridView1.Rows.Count; b++)
            {
                GridView1.Rows[b].Cells[9].Controls[0].Visible = false;
            }
        }
        rdredit.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=4 and  section_delete='No'", _connection);
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

    public void hr1()
    {

        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM task_schedule where pn_BranchId='" + employee.BranchId + "'", myConnection);

        DataSet ds = new DataSet();

        ad.Fill(ds, "task_schedule");


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
        GridView1.FooterRow.Visible = false;


    }


    private void DeleteRecord(string ID)
    {

        string sqlStatement = "DELETE FROM task_schedule WHERE TaskTitle = @TSubject";
        try
        {
            myConnection.Open();
            SqlCommand cmd = new SqlCommand(sqlStatement, myConnection);
            cmd.Parameters.AddWithValue("@TSubject", ID);
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


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "add")
            {
                string refr = ((TextBox)GridView1.FooterRow.FindControl("TextBoxref")).Text;
                string sub = ((TextBox)GridView1.FooterRow.FindControl("TextBox1")).Text;
                string des = ((TextBox)GridView1.FooterRow.FindControl("TextBox2")).Text;
                string doa = ((TextBox)GridView1.FooterRow.FindControl("TextBox3")).Text;
                string assi = ((DropDownList)GridView1.FooterRow.FindControl("DropDownList1")).SelectedItem.ToString();

                string pri = ((DropDownList)GridView1.FooterRow.FindControl("DropDownList2")).Text;
                string lvl = ((DropDownList)GridView1.FooterRow.FindControl("DropDownListLevel")).Text;
                string stat = ((DropDownList)GridView1.FooterRow.FindControl("DropDownList3")).Text;
                string due = ((TextBox)GridView1.FooterRow.FindControl("TextBoxDue")).Text;

                // string doc = ((TextBox)GridView1.FooterRow.FindControl("TextBox7")).Text;
                eid = ((DropDownList)GridView1.FooterRow.FindControl("DropDownList1")).SelectedValue;
                if (refr != "" || sub != "" || doa != "" || assi != "Select" || pri != "Select" || stat != "Select" || due != "")
                {
                    AddNewRecord(refr, sub, des, doa, eid, assi, pri, lvl, stat, due);
                }

                else
                {
                    lbl_error.Text = "Enter all the details";
                }
            }
        }
        catch (Exception ex)
        {

            lbl_error.Text = "Enter all the details";
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        string ID = ((Label)GridView1.Rows[e.RowIndex].FindControl("Label1")).Text;

        DeleteRecord(ID);
        hr();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //  GridView1.EditIndex = e.NewEditIndex;
        //  e.Row.RowIndex = GridView1.EditIndex;
        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex == GridView1.EditIndex)
        {

            foreach (DataControlFieldCell cell in e.Row.Cells)
            {
                foreach (Control control in cell.Controls)
                {
                    LinkButton button = control as LinkButton;
                    if (button != null && button.CommandName == "Delete")
                        button.OnClientClick = "return confirm('Are you sure " +
                               "you want to delete this record?');";
                }
            }

        }
    }
    public void populate(string tid)
    {
        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM task_history where task_id='" + tid + "'", myConnection);

        DataSet ds = new DataSet();

        ad.Fill(ds, "task_history");


        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridViewHistry.DataSource = ds;
            GridViewHistry.DataBind();
            int columnCount = GridViewHistry.Rows[0].Cells.Count;
            GridViewHistry.Rows[0].Cells.Clear();
            GridViewHistry.Rows[0].Cells.Add(new TableCell());
            GridViewHistry.Rows[0].Cells[0].ColumnSpan = columnCount;
            GridViewHistry.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            GridViewHistry.DataSource = ds;
            GridViewHistry.DataBind();

        }
        GridViewHistry.FooterRow.Visible = false;


    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

        string id;
        id = ((Label)GridView1.SelectedRow.FindControl("lbltaskid")).Text;
        populate(id);
        lablname.Text = ((Label)GridView1.SelectedRow.FindControl("Label12")).Text;
        modalhistory.Show();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

        GridView1.EditIndex = e.NewEditIndex; // turn to edit mode  
        hr();
        DropDownList drpstat = (DropDownList)GridView1.Rows[e.NewEditIndex].FindControl("statedit");
        drpstat.Focus();
        //GridView1.Rows[e.NewEditIndex].FindControl("statedit").Focus();
        string refr = ((Label)GridView1.Rows[e.NewEditIndex].FindControl("lblStatedit")).Text.ToString();
        //string qry = "Select * from task_schedule where Reference_No='" + refr + "'";
        //SqlCommand com = new SqlCommand(qry, myConnection);
        //myConnection.Open();
        //SqlDataReader readr = com.ExecuteReader();
        //if (readr.Read())
        //{
        //    crnt = Convert.ToString(readr["Status"]);
        //}
        ////refr = crnt;
        //myConnection.Close();

        if (s_login_role == "h" || s_login_role == "r")
        {

            // DropDownList drpstat = (DropDownList)e.Row.FindControl("statedit");
            drpstat.Items.Add(refr);
            if (refr == "On-process" || refr == "Completed")
            {
                drpstat.Items.Add("Re-open");
                drpstat.Items.Add("Hold");
                drpstat.Items.Add("Closed");
            }
            else
            {
                drpstat.Items.Add("Hold");
               // drpstat.Items.Add("Re-open");
            }
            //drpstat.Items.Add(crnt);
            drpstat.SelectedValue = refr;

        }
    }

    private void AddNewRecord(string refr, string sub, string des, string doa, string eid, string assi, string pri, string lvl, string stat, string due)
    {
        employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
        string query = @"set dateformat dmy;INSERT INTO task_schedule (pn_CompanyID, pn_BranchID, pn_EmployeeID,Reference_No, TaskTitle,TDescription,DOA,Assigned,assignedby,Priority,TaskLevel,Status,duedate) VALUES('" + employee.CompanyId + "','" + employee.BranchId + "','" + eid + "','" + refr + "','" + sub + "','" + des + "','" + doa + "','" + assi + "','" + employee.EmployeeId + "','" + pri + "','" + lvl + "','" + stat + "','" + due + "');set dateformat mdy";
        SqlCommand myCommand = new SqlCommand(query, myConnection);
        //myCommand.Parameters.AddWithValue("@TSubject", sub);
        //myCommand.Parameters.AddWithValue("@des", des);
        //myCommand.Parameters.AddWithValue("@doa", doa);
        //myCommand.Parameters.AddWithValue("@assi", assi);
        //myCommand.Parameters.AddWithValue("@pri", pri);
        //myCommand.Parameters.AddWithValue("@stat", stat);
        //myCommand.Parameters.AddWithValue("@doc", doc);

        myConnection.Open();

        myCommand.ExecuteNonQuery();
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('New Task created!');", true);

        myConnection.Close();

        hr();

    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow Gvrow = GridView1.Rows[e.RowIndex];
        if (Gvrow != null)
        {
            string tid = ((Label)Gvrow.FindControl("lbltaskid")).Text;
            string refr = ((Label)Gvrow.FindControl("refedit")).Text;
            string subedit = ((Label)Gvrow.FindControl("Tsubedit")).Text;
            string desedit = ((Label)Gvrow.FindControl("Tdesedit")).Text;
            string doa = ((Label)Gvrow.FindControl("DOAedit")).Text;
            string dept = ((Label)Gvrow.FindControl("deptedit")).Text;
            string asi = ((Label)Gvrow.FindControl("assiedit")).Text;
            string pri = ((Label)Gvrow.FindControl("prioredit")).Text;
            string stat = ((DropDownList)Gvrow.FindControl("statedit")).Text;
            string lvl = ((Label)Gvrow.FindControl("leveledit")).Text;
            //  string due = ((TextBox)Gvrow.FindControl("DueEdit")).Text;
            //  string doc = ((TextBox)Gvrow.FindControl("DOCedit")).Text;
            string rdoc = DateTime.Now.ToString();
            string cmnt = ((TextBox)Gvrow.FindControl("Cmntedit")).Text;
            DateTime d1 = DateTime.Parse(rdoc).Date;
            string temp = d1.ToString("dd/MM/yyyy");
            string qry = @"select DOC from task_schedule where TaskID='" + tid + "'";
            SqlCommand com = new SqlCommand(qry, myConnection);
            myConnection.Open();
            SqlDataReader readr = com.ExecuteReader();
            if (readr.Read())
            {
                doco = Convert.ToString(readr["DOC"]);
            }
            if (stat == "Completed" && doco == "")
            {
                DateTime dt = DateTime.Now;
                doco = dt.Date.ToString();
                cmd = new SqlCommand("set dateformat dmy;update task_schedule set TaskTitle='" + subedit + "',TDescription='" + desedit + "',DOA='" + doa + "',Department='" + dept + "',Assigned='" + asi + "',Priority='" + pri + "',TaskLevel='" + lvl + "',Status='" + stat + "',DOC='" + doco + "',Comments='" + cmnt + "' where TaskTitle='" + subedit + "';set dateformat mdy", myConnection);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Task Updated Successfully!');", true);
            }
            else if (stat == "Closed")
            {
                DateTime dt = DateTime.Now;
                string fdoc = dt.Date.ToString();
                cmd = new SqlCommand("set dateformat dmy;update task_schedule set TaskTitle='" + subedit + "',TDescription='" + desedit + "',DOA='" + doa + "',Department='" + dept + "',Assigned='" + asi + "',Priority='" + pri + "',TaskLevel='" + lvl + "',Status='" + stat + "',FDOC='" + fdoc + "',Comments='" + cmnt + "' where TaskTitle='" + subedit + "';set dateformat mdy", myConnection);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Task Closed');", true);
            }
            else
            {
                cmd = new SqlCommand("set dateformat dmy;update task_schedule set TaskTitle='" + subedit + "',TDescription='" + desedit + "',DOA='" + doa + "',Department='" + dept + "',Assigned='" + asi + "',Priority='" + pri + "',TaskLevel='" + lvl + "',Status='" + stat + "',RDOC='" + rdoc + "',Comments='" + cmnt + "' where TaskTitle='" + subedit + "';set dateformat mdy", myConnection);
                cmd1 = new SqlCommand("set dateformat dmy;insert into task_history (task_id,Reference_No,status,RDOC,comment) values ('" + tid + "','" + refr + "' ,'" + stat + "','" + rdoc + "','" + cmnt + "');set dateformat mdy", myConnection);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Task Updated Successfully!');", true);
                // Page.ClientScript.RegisterStartupScript(this.GetType(),"toastr_message", "toastr.error('There was an error', 'Error')", true);
                cmd1.ExecuteNonQuery();
            }

            cmd.ExecuteNonQuery();
            myConnection.Close();
            GridView1.EditIndex = -1; // turn to edit mode
            hr();
        }
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        hr();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Button2.Text == "View Notifications")
        {
            hr_edit();
            Button2.Text = "Hide Notifications";
            GridView3.Visible = false;
            GridView2.Visible = true;
        }
        else //if(Button2.Text=="Hide Notifications")
        {
            //GridView2.Visible = false;
            //Button2.Text = "View Notifications";
            Response.Redirect("Task_Scheduler1.aspx");
        }

    }

    public void hr_edit()
    {
        SqlDataAdapter adap = new SqlDataAdapter("select * from task_schedule where pn_BranchID='" + employee.BranchId + "' and Remarks != '' and Comments is null", myConnection);
        DataSet ds1 = new DataSet();
        adap.Fill(ds1, "task_schedule");

        if (ds1.Tables[0].Rows.Count == 0)
        {
            ds1.Tables[0].Rows.Add(ds1.Tables[0].NewRow());
            GridView2.DataSource = ds1;
            GridView2.DataBind();
            int columnCount = GridView2.Rows[0].Cells.Count;
            GridView2.Rows[0].Cells.Clear();
            GridView2.Rows[0].Cells.Add(new TableCell());
            GridView2.Rows[0].Cells[0].ColumnSpan = columnCount;
            GridView2.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            GridView2.DataSource = ds1;
            GridView2.DataBind();

        }

        myConnection.Close();
    }

    public void hr_edit1()
    {
        SqlDataAdapter ad3 = new SqlDataAdapter("select * from task_schedule where pn_BranchID='" + employee.BranchId + "' and Status='Closed'", myConnection);

        DataSet ds3 = new DataSet();

        ad3.Fill(ds3, "task_schedule");


        if (ds3.Tables[0].Rows.Count > 0)
        {
            GridView3.DataSource = ds3;
            GridView3.DataBind();
        }
        else
        {
            GridView3.DataSource = ds3;
            GridView3.DataBind();
            int columnCount = GridView3.Rows[0].Cells.Count;
            GridView3.Rows[0].Cells.Clear();
            GridView3.Rows[0].Cells.Add(new TableCell());
            GridView3.Rows[0].Cells[0].ColumnSpan = columnCount;
            GridView3.Rows[0].Cells[0].Text = "No Records Found..";
        }

    }


    protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView2.EditIndex = -1;
        hr_edit();
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //string appr;
        //CheckBox appr_yes = ((CheckBox)GridView2.FindControl("Check_yes"));
        //CheckBox appr_no = ((CheckBox)GridView2.FindControl("Check_no"));
        //string cmt = ((TextBox)GridView2.FindControl("TSubedit")).Text;
        //string sub1 = ((Label)GridView2.FindControl("Label1")).Text;
        //if (appr_yes.Checked == true)
        //{
        //    appr = "Granted";
        //}
        //else if (appr_no.Checked == true)
        //{
        //    appr = "Denied";
        //}

        //if (e.CommandName == "Update")
        //{
        //    SqlDataAdapter adap1 = new SqlDataAdapter("update task_schedule set approve='Granted' , Comments='"+cmt+"' where TSubject='"+sub1+"'", myConnection);
        //    DataSet ds2 = new DataSet();
        //    adap1.Fill(ds2, "task_schedule");
        //    GridView2.DataSource = ds2;
        //    GridView2.DataBind();
        //    myConnection.Close();
        //}
    }

    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView2.EditIndex = e.NewEditIndex; // turn to edit mode
        hr_edit();
    }
    protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow Gvrow1 = GridView2.Rows[e.RowIndex];

        if (Gvrow1 != null)
        {
            string appr, perm;
            string subedit1 = ((Label)Gvrow1.FindControl("Labelsubedit")).Text;
            CheckBox appr_yes = ((CheckBox)GridView2.Rows[e.RowIndex].FindControl("Check_yes1"));
            appr = appr_yes.Checked.ToString();
            if (appr == "True")
            {
                perm = "Granted";
            }
            else
            {
                perm = "Denied";
            }

            string com = ((TextBox)Gvrow1.FindControl("txtcom")).Text;
            string rdoc = ((TextBox)Gvrow1.FindControl("LRDOCedit")).Text;

            myConnection.Open();
            cmd = new SqlCommand("set dateformat dmy;update task_schedule set Approve='" + appr + "', RDOC='" + rdoc + "', Comments='" + com + "', Permission='" + perm + "' where TSubject='" + subedit1 + "';set dateformat mdy", myConnection);
            cmd.ExecuteNonQuery();
            myConnection.Close();
            GridView2.EditIndex = -1; // turn to edit mode
            hr_edit();
        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("Tasks_log.aspx");
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        hr_edit1();
        GridView3.Visible = true;
        GridView2.Visible = false;
    }

    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddassi = (DropDownList)GridView1.FooterRow.FindControl("DropDownList1");
        ddassi.Items.Clear();
        DropDownList department = (DropDownList)GridView1.FooterRow.FindControl("DropDownList4");
        string dept = department.SelectedItem.Text;
        //Label8.Text = dept;
        myConnection.Open();
        SqlCommand comm = new SqlCommand("select distinct a.Employee_First_Name , b.v_departmentName, c.pn_EmployeeID , c.pn_DepartmentID from paym_employee a inner join paym_employee_profile1 c on a.pn_employeeid = c.pn_employeeid inner join paym_department b on c.pn_DepartmentID = b.pn_DepartmentID where v_DepartmentName = '" + dept + "'", myConnection);
        //select distinct a.Employee_First_Name , b.v_departmentName, c.pn_EmployeeID , c.pn_DepartmentID from paym_employee a inner join paym_employee_profile1 c on a.pn_employeeid = c.pn_employeeid inner join paym_department b on c.pn_DepartmentID = b.pn_DepartmentID where v_DepartmentName = '"+dept+"'
        SqlDataReader reader;
        reader = comm.ExecuteReader();
        while (reader.Read())
        {
            //ddassi.Items.Clear();
            ddassi.Items.Add(reader[0].ToString());
        }
        reader.Close();
        myConnection.Close();
    }
    protected void GridView3_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //GridView3.EditIndex = e.NewEditIndex;
        //hr_edit1();
    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "btnadd")
        {
            GridViewRow val = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
            int RowIndex = val.RowIndex;
            Label taskid = (Label)GridView3.Rows[RowIndex].FindControl("lbltaskid");

            int rating = ((AjaxControlToolkit.Rating)GridView3.Rows[RowIndex].FindControl("Rating1")).CurrentRating;

            string query = "Update task_schedule set rating=" + rating + " where Taskid=" + taskid.Text + "";

            SqlCommand myCommand = new SqlCommand(query, myConnection);

            myConnection.Open();

            myCommand.ExecuteNonQuery();

            myConnection.Close();
        }
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        if (Button5.Text == "Completed Tasks")
        {
            Button5.Text = "Close";
            task_complete();
            GridView3.Visible = true;
        }
        else if (Button5.Text == "Close")
        {
            GridView3.Visible = false;
            Button5.Text = "Completed Tasks";
        }
    }
    protected void histry_Click(object sender, ImageClickEventArgs e)
    {
        //  GridView2.Visible = true;
    }
    protected void Rating1_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
    {
        int value;
        value = Convert.ToInt32(e.Value);

    }
    protected void TextBox3_TextChanged(object sender, EventArgs e)
    {
        string samp;
        DateTime today, dump;

        samp = ((TextBox)GridView1.FooterRow.FindControl("Textbox3")).Text;
        dump = DateTime.Parse(samp);
        today = DateTime.Now.Date;
        if (dump < today)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Date you selected is less than today.select again!');", true);
            ((TextBox)GridView1.FooterRow.FindControl("Textbox3")).Text = "";
            ((TextBox)GridView1.FooterRow.FindControl("Textbox3")).Focus();
        }


    }
    protected void TextBoxdue_TextChanged(object sender, EventArgs e)
    {
        string samps, dumps;
        DateTime samp, dump;

        samps = ((TextBox)GridView1.FooterRow.FindControl("Textbox3")).Text;
        dumps = ((TextBox)GridView1.FooterRow.FindControl("TextBoxdue")).Text;
        dump = DateTime.Parse(dumps);
        samp = DateTime.Parse(samps);
        if (dump < samp)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Date you selected is less than today.select again!');", true);
            ((TextBox)GridView1.FooterRow.FindControl("TextBoxdue")).Text = "";
            ((TextBox)GridView1.FooterRow.FindControl("TextBoxdue")).Focus();
        }
        ((Button)GridView1.FooterRow.FindControl("Button1")).Focus();
    }

    protected void rdobtn1_CheckedChanged(object sender, EventArgs e)
    {
        //if (rdobtn1.Checked)
        //{

        //}

    }
    protected void grd_view_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd_view1.EditIndex = e.NewEditIndex;
        task();
        DropDownList drpstat = (DropDownList)grd_view1.Rows[e.NewEditIndex].FindControl("DropDownStat");
        drpstat.Focus();
        drpstat.Items.Add("On-process");
        drpstat.Items.Add("Completed");
    }
    protected void grd_view_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow Gvrow = grd_view1.Rows[e.RowIndex];
        //Label8.Text = Gvrow.ToString();
        if (Gvrow != null)
        {
            string sdate = "1990/01/01";
            string tid = ((Label)Gvrow.FindControl("lbltaskid")).Text;
            string refr = ((Label)Gvrow.FindControl("Lblref")).Text;
            string subedit = ((Label)Gvrow.FindControl("Tsubedit")).Text;
            string Remedit = ((TextBox)Gvrow.FindControl("remedit")).Text;
            string stat = ((DropDownList)Gvrow.FindControl("DropDownStat")).Text;
            //if (stat == "Completed")
            //{
            //    sdate = DateTime.Now.ToString("MM/dd/yyyy");
            //}
            string qry = @"select DOC from task_schedule where TaskID='" + tid + "'";
            SqlCommand com = new SqlCommand(qry, myConnection);
            myConnection.Open();
            SqlDataReader readr = com.ExecuteReader();
            if (readr.Read())
            {
                doco = Convert.ToString(readr["DOC"]);
            }
            if (stat == "Completed" && doco == "")
            {
                doco = DateTime.Now.ToString();
                cmd = new SqlCommand("set dateformat dmy;update task_schedule set TaskTitle='" + subedit + "',Status='" + stat + "',Remarks='" + Remedit + "',DOC='" + doco + "' where TaskTitle='" + subedit + "';set dateformat mdy", myConnection);
                cmd1 = new SqlCommand("set dateformat dmy;insert into task_history (task_id,status,remarks) values ('" + tid + "' ,'" + stat + "','" + Remedit + "');set dateformat mdy", myConnection);

            }
            else
            {
                string rdoc = DateTime.Now.ToString();
                string cmnt = "-";
                cmd = new SqlCommand("set dateformat dmy;update task_schedule set TaskTitle='" + subedit + "',Status='" + stat + "',Remarks='" + Remedit + "',RDOC='" + rdoc + "' where TaskTitle='" + subedit + "';set dateformat mdy", myConnection);
                cmd1 = new SqlCommand("set dateformat dmy;insert into task_history (task_id,Reference_No,status,RDOC,remarks,comment) values ('" + tid + "','" + refr + "' ,'" + stat + "','" + rdoc + "','" + Remedit + "','" + cmnt + "');set dateformat mdy", myConnection);
                cmd1.ExecuteNonQuery();
            }
            // myConnection.Open();
            //cmd = new SqlCommand("update task_schedule set Status='"+stat+"', Remarks='" + Remedit + "', Submitted_date='"+sdate+"' where TSubject='" + subedit + "' and pn_EmployeeID='"+employee.EmployeeId+"'", myConnection);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Task Updated Successfully!');", true);
            cmd.ExecuteNonQuery();
            myConnection.Close();
            grd_view1.EditIndex = -1; // turn to edit mode
            task();
        }
    }
    protected void grd_view_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd_view1.EditIndex = -1;
        task();
    }
    protected void grd_view_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex == grd_view1.EditIndex)
        {

            if (s_login_role == "e")
            {
                DropDownList drpstat = (DropDownList)e.Row.FindControl("statedit");
                drpstat.Items.Add("Select");
                drpstat.Items.Add("On-process");
                drpstat.Items.Add("Completed");

            }
        }
    }
    protected void rdobtn2_CheckedChanged(object sender, EventArgs e)
    {
        //if (rdobtn2.Checked)
        //{

        //}

    }
    public void task()
    {

        grd_view1.Attributes.Add("bordercolor", "Darkgray");

        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM task_schedule where Assigned='SUNDAR RAJ.C' and Status!='Closed'", myConnection);

        DataSet ds = new DataSet();

        ad.Fill(ds, "task_schedule");

        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            grd_view1.DataSource = ds;
            grd_view1.DataBind();
            int columnCount = grd_view1.Rows[0].Cells.Count;
            grd_view1.Rows[0].Cells.Clear();
            grd_view1.Rows[0].Cells.Add(new TableCell());
            grd_view1.Rows[0].Cells[0].ColumnSpan = columnCount;
            grd_view1.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            grd_view1.DataSource = ds;
            grd_view1.DataBind();
        }

        myConnection.Open();

        cmd1 = new SqlCommand("select * from paym_department where pn_BranchId='" + employee.BranchId + "'", myConnection);
        rea1 = cmd1.ExecuteReader();
        while (rea1.Read())
        {
            DropDownList dept = (DropDownList)GridView1.FooterRow.FindControl("DropDownList4");
            dept.Items.Add(rea1["v_DepartmentName"].ToString());
        }
        rea1.Close();
        //  DropDownList drpstatus = (DropDownList)GridView1.
        // drpstatus.Items[0].Value = "new";
        myConnection.Close();
    }
    public void task_complete()
    {

        GridView3.Attributes.Add("bordercolor", "Darkgray");

        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM task_schedule where Assigned='SUNDAR RAJ.C' and Status='Closed'", myConnection);

        DataSet ds = new DataSet();

        ad.Fill(ds, "task_schedule");

        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView3.DataSource = ds;
            GridView3.DataBind();
            int columnCount = GridView3.Rows[0].Cells.Count;
            GridView3.Rows[0].Cells.Clear();
            GridView3.Rows[0].Cells.Add(new TableCell());
            GridView3.Rows[0].Cells[0].ColumnSpan = columnCount;
            GridView3.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            GridView3.DataSource = ds;
            GridView3.DataBind();
        }
    }
    protected void ddlfilter_populate()
    {
        filterid.Items.Add("Select");
        filterid.Items.Add("TaskTitle");
        filterid.Items.Add("Assigned");
        filterid.Items.Add("DOA");
        filterid.Items.Add("TaskLevel");
        filterid.Items.Add("Priority");
        filterid.Items.Add("Status");
        filterid.Items.Add("Duedate");
        filterid.Items.Add("Refresh");
    }
    protected void filterid_SelectedIndexChanged(object sender, EventArgs e)
    {

        
        filterval = filterid.SelectedItem.Value;
        if (filterval == "Select" || filterval=="Refresh")
        {
            filterdata.SelectedItem.Text = "Select";
            filterdata.Enabled = false;
            filterid.SelectedItem.Text = "Select";
            //btnapply.Enabled = false;
            reporting();
            filterid.Items.Clear();
           ddlfilter_populate();
            
        }
       
        else
        {
            filterdata.Items.Clear();
            filterdata.Items.Insert(0, new ListItem("Select", "0"));
            string query = "select distinct " + filterval + " from task_schedule";
            BindDropDownList(filterdata, query, filterval, "Select");
            filterdata.Enabled = true;

            // btnapply.Enabled = true;
        }
    }
    private void BindDropDownList(DropDownList ddl, string query, string text, string defaultText)
    {
        string conString = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
        SqlCommand cmd = new SqlCommand(query);
        using (SqlConnection con = new SqlConnection(conString))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                cmd.Connection = con;
                con.Open();
                ddl.DataSource = cmd.ExecuteReader();
                ddl.DataTextField = text;
                // ddl.DataValueField = value;
                ddl.DataBind();
                con.Close();
            }
        }
        ddl.Items.Insert(0, new ListItem(defaultText, "0"));
    }

    protected void intasks_Click(object sender, ImageClickEventArgs e)
    {
        task();
        div1.Visible = false;
        GridView1.Visible = false;

        lblfilter.Visible = false;
        filterid.Visible = false;
        filterdata.Visible = false;
        // btnapply.Visible = false;
        Button2.Visible = false;
        Button3.Visible = false;
        Button4.Visible = false;
        grd_view1.Visible = true;
        Button5.Visible = true;
       
    }
    protected void outtasks_Click(object sender, ImageClickEventArgs e)
    {
        div1.Visible = true;
        grd_view1.Visible = false;
        GridView1.Visible = true;
        lblfilter.Visible = true;
        filterid.Visible = true;
        filterdata.Visible = true;
        // btnapply.Visible = true;
        Button5.Visible = false;
        Button2.Visible = true;
        Button3.Visible = true;
        Button4.Visible = true;
    }
    protected void filterdata_SelectedIndexChanged(object sender, EventArgs e)
    {
        fdata = filterdata.SelectedItem.Value;
        string query = "SELECT * FROM task_schedule where Status!='Closed' and " + filterval + " = '" + fdata + "'";
        SqlDataAdapter ad = new SqlDataAdapter(query, myConnection);

        DataSet ds = new DataSet();

        ad.Fill(ds, "task_schedule");

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
    public void sortdata()
    {

        //  SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True;User Instance=True");
        SqlCommand cmd = new SqlCommand("select TaskID,Reference_No,TaskTitle,TDescription,DOA,Assigned,Priority,TaskLevel,Status,duedate,Comments,FDOC from task_schedule where Status!='Closed'", myConnection);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();
        ViewState["dirState"] = dt;
        ViewState["sortdr"] = "Asc";
       // myConnection.Open();

       // cmd1 = new SqlCommand("select * from paym_department where pn_BranchId='" + employee.BranchId + "'", myConnection);
       // rea1 = cmd1.ExecuteReader();
       // while (rea1.Read())
       // {
       //     DropDownList dept = (DropDownList)GridView1.FooterRow.FindControl("DropDownList4");
       //     dept.Items.Add(rea1["v_DepartmentName"].ToString());
       // }
       // rea1.Close();
       // DropDownList drpstatus = (DropDownList)GridView1.
       //drpstatus.Items[0].Value = "new";
       // myConnection.Close();

    }



    protected void GridView1_Sorting1(object sender, GridViewSortEventArgs e)
    {
        //sortdata();
        DataTable dtrslt = (DataTable)ViewState["dirState"];
        if (dtrslt.Rows.Count > 0)
        {
            if (Convert.ToString(ViewState["sortdr"]) == "Asc")
            {
                dtrslt.DefaultView.Sort = e.SortExpression + " Desc";
                ViewState["sortdr"] = "Desc";
            }
            else
            {
                dtrslt.DefaultView.Sort = e.SortExpression + " Asc";
                ViewState["sortdr"] = "Asc";
            }
            GridView1.DataSource = dtrslt;
            GridView1.DataBind();
            myConnection.Open();

            cmd1 = new SqlCommand("select * from paym_department where pn_BranchId='" + employee.BranchId + "'", myConnection);
            rea1 = cmd1.ExecuteReader();
            while (rea1.Read())
            {
                DropDownList dept = (DropDownList)GridView1.FooterRow.FindControl("DropDownList4");
                dept.Items.Add(rea1["v_DepartmentName"].ToString());
            }
            rea1.Close();
   //         DropDownList drpstatus = (DropDownList)GridView1.drpstatus.Items[0].Value = "new";
            myConnection.Close();

        }
    }
    //protected void refresh_Click(object sender, ImageClickEventArgs e)
    //{
    //    reporting();
    //    filterid.SelectedItem.Text = "Select";
    //    filterdata.Enabled = false;
    //}
}
