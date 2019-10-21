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
using System.Data.SqlClient;
using ePayHrms.Login;
using ePayHrms.BE.Recruitment;
using ePayHrms.Connection;
using ePayHrms.Candidate;
using System.IO;
using ePayHrms.Company;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ePayHrms.Leave;
using ePayHrms.Employee;

public partial class Hrms_Additional_Default : System.Web.UI.Page
{
    ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand cmd1 = new SqlCommand();
    SqlDataReader rea;
    SqlDataAdapter ada = new SqlDataAdapter();
    Company company = new Company();
    Employee employee = new Employee();
    Leave l = new Leave();

    Collection<Leave> LeaveList;
    Collection<Company> CompanyList;
    Collection<Employee> EmployeeList;
    Collection<Company> ddlBranchsList;
    Collection<Employee> EmpProfileList;

    GridView gv = new GridView();
    string _Code, qry, eid = "";
    string s_login_role;
    int i, j, cur_yr, yr_it, ddl_i;
    bool avail = false, temp_avail = false, check = true;
    string s_form = "", fd = "", td = "";
    DataSet ds_userrights;

    #region Variables
    string gvUniqueID = String.Empty;
    int gvNewPageIndex = 0;
    string strQRY = "";
    string gvSortExpr = String.Empty;
    private string gvSortDir
    {

        get { return ViewState["SortDirection"] as string ?? "ASC"; }

        set { ViewState["SortDirection"] = value; }

    }
    #endregion


    private SqlDataSource ChildDataSource(string strSlabId, string strSort)
    {
        SqlDataSource dsTemp = new SqlDataSource();
        dsTemp.ConnectionString = ConfigurationManager.AppSettings["Connectionstring"];
        strQRY = " Set dateformat dmy; SELECT [paym_emp_earnings].[pn_EmployeeID],[paym_emp_earnings].[pn_earningsID]," +
                                "[paym_earnings].[v_earningsname],[paym_emp_earnings].[n_Amount] FROM [paym_emp_earnings], [paym_earnings]" +
                                " WHERE [paym_emp_earnings].[pn_EmployeeID] = '" + strSlabId + "' and [paym_emp_earnings].[pn_EarningsID]=[paym_earnings].[pn_EarningsID] and [paym_earnings].[c_Regular] = 'Y' " +
                                "UNION ALL " +
                                "SELECT '" + strSlabId + "','','','' FROM [paym_emp_earnings] WHERE [paym_emp_earnings].[pn_EmployeeID] = '" + strSlabId + "'" +
                                "HAVING COUNT(*)=0 " + strSort + "Set dateformat mdy";

        dsTemp.SelectCommand = strQRY;
        return dsTemp;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        Session["formulaBonus"] = "";
        l.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;

        if (!IsPostBack)
        {

            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                ddl_year_load();
                switch (s_login_role)
                {
                    case "a": //row_emp.Visible = false;
                        ddl_Branch_load();
                        break;

                    case "h": //ddl_Branch.Visible = false;
                        ddl_department_load();

                        break;

                    case "e": //ddl_Branch.Visible = false;
                        //row_emp.Visible = false;
                        l.EmployeeID = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);

                        break;

                    case "u": //s_form = "5";
                        s_form = "41";
                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            //ddl_Branch.Visible = false;
                            ddl_employee_load();
                        }
                        else
                        {
                            //ddl_Branch.Visible = false;
                            //row_emp.Visible = false;
                            l.EmployeeID = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);

                        }

                        break;

                    default: Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                        Response.Redirect("Company_Home.aspx");
                        break;
                }
            }
            else
            {
                Response.Cookies["Msg_Session"].Value = "Create Company";
                Response.Redirect("Company_Home.aspx");
            }
        }
    }

    public void hr()
    {
        myConnection.Open();
        SqlDataAdapter ad1 = new SqlDataAdapter("SELECT * FROM paym_emp_projects where pn_BranchID ='" + employee.BranchId + "'and pn_CompanyID='" + employee.CompanyId + "' and DepartmentID='" + ddl_department.SelectedItem.Value + "'", myConnection);

        DataSet ds = new DataSet();

        ad1.Fill(ds, "paym_emp_projects");


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
        ddl_employee_load();
        ddl_project_load();
        myConnection.Close();
    }

    public void ddl_year_load()
    {
        try
        {
            i = 0;
            cur_yr = DateTime.Now.Year;
            cur_yr = cur_yr + 5;

            for (yr_it = 2010; yr_it <= cur_yr; yr_it++)
            {
                ddl_year.Items.Add(Convert.ToString(yr_it - 1) + "-" + Convert.ToString(yr_it));
                i++;
            }
            i = i - 6;
            ddl_year.SelectedIndex = i;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error loading year');", true);
        }
    }


    public void ddl_Branch_load()
    {

        //branch dropdown

        //ddlBranchsList = company.fn_getBranchs();

        //if (ddlBranchsList.Count > 0)
        //{

        //    for (ddl_i = -1; ddl_i < ddlBranchsList.Count; ddl_i++)
        //    {

        //        if (ddl_i == -1)
        //        {
        //            ListItem list = new ListItem();

        //            list.Text = "Select Branch";
        //            list.Value = "0";
        //            ddl_Branch.Items.Add(list);


        //        }
        //        else
        //        {

        //            ListItem list = new ListItem();

        //            list.Text = ddlBranchsList[ddl_i].CompanyName;
        //            list.Value = ddlBranchsList[ddl_i].CompanyId.ToString();
        //            ddl_Branch.Items.Add(list);

        //        }

        //    }

        //}
    }



    protected void ddl_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (ddl_Branch.SelectedValue != "0")
            //{
            //    ViewState["Appraisal_BranchID"] = Convert.ToInt32(ddl_Branch.SelectedItem.Value);
            //    ddl_employee_load();
            //    tbl_details.Visible = true;
            //    //Tr2.Visible = false;

            //}
            //else
            //{
            //    tbl_details.Visible = false;
            //    tbl_grd.Visible = false;
            //}
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

    public void ddl_employee_load()
    {
        //employee dropdown
        DropDownList ename = (DropDownList)GridView1.FooterRow.FindControl("txtempname");
        ename.Items.Clear();

        if (s_login_role == "a")
        {
            employee.BranchId = (int)ViewState["Appraisal_BranchID"];
        }

        if (s_login_role == "h")
        {
            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        }

        qry = "Select a.pn_EmployeeID, a.EmployeeCode, a.Employee_First_Name from paym_employee a, paym_employee_profile1 b where a.pn_CompanyID=b.pn_CompanyID and a.pn_BranchID=b.pn_BranchID and a.pn_EmployeeID=b.pn_EmployeeID and b.pn_DepartmentID=" + ddl_department.SelectedValue + " and b.pn_CompanyID=" + employee.CompanyId + " and a.status='y' and b.pn_BranchID=" + employee.BranchId;

        EmployeeList = employee.fn_getEmplist(qry);

        if (EmployeeList.Count > 0)
        {
            //row_emp.Visible = true;
            //tbl_details.Visible = true;
            for (ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();
                    e_list.Text = "Select Employee";
                    e_list.Value = "0";
                    DropDownList emp = (DropDownList)GridView1.FooterRow.FindControl("txtempname");
                    emp.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();
                    e_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
                    e_list.Text = EmployeeList[ddl_i].FirstName.ToString();
                    DropDownList emp = (DropDownList)GridView1.FooterRow.FindControl("txtempname");
                    emp.Items.Add(e_list);
                }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Employee Found');", true);
        }
    }


    public void ddl_project_load()
    {
        //employee dropdown
        DropDownList prname = (DropDownList)GridView1.FooterRow.FindControl("txtpname");
        prname.Items.Clear();

        if (s_login_role == "a")
        {
            employee.BranchId = (int)ViewState["Appraisal_BranchID"];
        }

        if (s_login_role == "h")
        {
            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        }
        qry = "select * from paym_projectsite where BranchId='" + employee.BranchId + "' and pn_CompanyID = '" + employee.CompanyId + "'";
        EmployeeList = employee.fn_getprojectlist(qry);

        if (EmployeeList.Count > 0)
        {
            for (ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();
                    e_list.Text = "Select Project";
                    e_list.Value = "0";
                    DropDownList pname = (DropDownList)GridView1.FooterRow.FindControl("txtpname");
                    pname.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();
                    e_list.Value = EmployeeList[ddl_i].ProjectsiteId.ToString();
                    e_list.Text = EmployeeList[ddl_i].ProjectsiteName.ToString();
                    DropDownList pname = (DropDownList)GridView1.FooterRow.FindControl("txtpname");
                    pname.Items.Add(e_list);
                }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Projects Found.');", true);
        }
    }


    public void ddl_department_load()
    {

        if (s_login_role == "a")
        {
            // employee.BranchId = Convert.ToInt32(ddl_Branch.SelectedItem.Value);
        }
        EmployeeList = employee.fn_getDepartmentList1(employee.BranchId);
        if (EmployeeList.Count > 0)
        {

            for (int ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem list = new ListItem();

                    list.Text = "Select Department";
                    list.Value = "sd";
                    ddl_department.Items.Add(list);
                }
                else
                {
                    ListItem list = new ListItem();
                    list.Text = EmployeeList[ddl_i].DepartmentName;
                    list.Value = EmployeeList[ddl_i].DepartmentId.ToString();
                    ddl_department.Items.Add(list);
                }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Department Found.');", true);
        }

    }

    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_department.SelectedValue != "sd")
        {
            hr();
        }
    }

    #region GridView1 Event Handlers
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridViewRow row = e.Row;
        string d1 = "", d2 = "";
        string strSort = string.Empty;

        if (row.DataItem == null)
        {
            return;
        }

        gv = (GridView)row.FindControl("GridView2");

        if (gv.UniqueID == gvUniqueID)
        {
            ClientScript.RegisterStartupScript(GetType(), "Expand", "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + ((DataRowView)e.Row.DataItem)["Pid"].ToString() + "','one');</script>");
        }

        d1 = ((Label)e.Row.FindControl("txtfromdate")).Text;
        d2 = ((Label)e.Row.FindControl("txttodate")).Text;
        string strSlabId = ((DataRowView)e.Row.DataItem)["pid"].ToString();

        strQRY = "Set dateformat dmy; SELECT [paym_emp_earnings].[Pid], [paym_emp_earnings].[ID], [paym_emp_earnings].[pn_EmployeeID],[paym_emp_earnings].[pn_earningsID]," +
                                "[paym_earnings].[v_earningsname],[paym_emp_earnings].[n_Amount] FROM [paym_emp_earnings], [paym_earnings]" +
                                " WHERE [paym_emp_earnings].[pid] = '" + strSlabId + "' and [paym_emp_earnings].[pn_EarningsID]=[paym_earnings].[pn_EarningsID] and [paym_earnings].[c_Regular] = 'Y' and [paym_emp_earnings].[from_date]='" + d1 + "'  and [paym_emp_earnings].[to_date]='" + d2 + "'" +
                                " UNION ALL " +
                                "SELECT '" + strSlabId + "','','','','','' FROM [paym_emp_earnings] WHERE [paym_emp_earnings].[pid] = '" + strSlabId + "'" +
                                "HAVING COUNT(*)=0 " + strSort + "Set dateformat mdy";

        SqlDataAdapter ad1 = new SqlDataAdapter(strQRY, myConnection);

        DataSet ds = new DataSet();

        ad1.Fill(ds, strQRY);


        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gv.DataSource = ds;
            gv.DataBind();
            int columnCount = gv.Rows[0].Cells.Count;
            gv.Rows[0].Cells.Clear();
            gv.Rows[0].Cells.Add(new TableCell());
            gv.Rows[0].Cells[0].ColumnSpan = columnCount;
            gv.Rows[0].Cells[0].Text = "No Records Found......";
        }
        else
        {
            gv.DataSource = ds;
            gv.DataBind();
        }

        //gv.DataSource = ChildDataSource(((DataRowView)e.Row.DataItem)["pn_EmployeeID"].ToString(), strSort);
        //gv.DataBind();

        EmployeeList = employee.fn_getEarningsList_Regular(employee);


        if (EmployeeList.Count > 0)
        {
            for (ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();
                    e_list.Text = "Select Allowance";
                    e_list.Value = "0";
                    DropDownList earn = (DropDownList)gv.FooterRow.FindControl("txtallowance");
                    earn.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();
                    e_list.Value = EmployeeList[ddl_i].EarningsId.ToString();
                    e_list.Text = EmployeeList[ddl_i].EarningsName.ToString();
                    DropDownList earn = (DropDownList)gv.FooterRow.FindControl("txtallowance");
                    earn.Items.Add(e_list);
                }
            }
        }

        LinkButton l = (LinkButton)e.Row.FindControl("linkDeleteCust");
        l.Attributes.Add("onclick", "javascript:return " +
        "confirm('Are you sure you want to delete this Slab " +
        DataBinder.Eval(e.Row.DataItem, "pn_EmployeeID") + "')");

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Add")
        {
            try
            {
                string ename = ((DropDownList)GridView1.FooterRow.FindControl("txtempname")).SelectedItem.Text;
                string eid = ((DropDownList)GridView1.FooterRow.FindControl("txtempname")).SelectedValue;
                string pname = ((DropDownList)GridView1.FooterRow.FindControl("txtpname")).SelectedItem.Text;
                string pid = ((DropDownList)GridView1.FooterRow.FindControl("txtpname")).Text;
                string fdate = ((TextBox)GridView1.FooterRow.FindControl("txtfdate")).Text;
                string tdate = ((TextBox)GridView1.FooterRow.FindControl("txttdate")).Text;
                string strSQL = "";
                if (fdate == "" || tdate == "")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Date field');</script>");
                    return;
                }
                strSQL = "set dateformat dmy; INSERT INTO paym_emp_projects (pn_CompanyID, pn_BranchID, pn_EmployeeID, pn_Employeename, DepartmentID, p_SiteID, p_Name, " +
                         "from_date, to_date, current_year) VALUES ('" + employee.CompanyId + "','" + employee.BranchId + "','" + eid + "','" + ename + "','" + ddl_department.SelectedItem.Value + "','" + pid + "','" + pname + "','" + fdate + "','" + tdate + "','" + ddl_year.SelectedItem.Text + "'); set dateformat mdy;";

                SqlDataSource1.InsertCommand = strSQL;
                SqlDataSource1.Insert();
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert(' Added successfully ');</script>");
                hr();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message.ToString().Replace("'", "") + "');</script>");
            }
        }
    }


    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        string strSlabID = ((Label)GridView1.Rows[e.RowIndex].FindControl("lblSlabID")).Text;
        string strSQL = "";

        try
        {
            strSQL = "DELETE from paym_emp_projects WHERE Pid = '" + strSlabID + "' and pn_CompanyID='" + employee.CompanyId + "' and pn_BranchID = '" + employee.BranchId + "'";
            strSQL = strSQL + "DELETE from paym_emp_earnings WHERE Pid = '" + strSlabID + "' and pn_CompanyID='" + employee.CompanyId + "' and pn_BranchID = '" + employee.BranchId + "'";
            SqlDataSource1.DeleteCommand = strSQL;
            SqlDataSource1.Delete();
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted successfully');</script>");
            hr();
        }
        catch (Exception ex)
        { }
    }

    protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        if (e.Exception != null)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + e.Exception.Message.ToString().Replace("'", "") + "');</script>");
            e.ExceptionHandled = true;
        }
    }
    #endregion


    #region GridView2 Event Handlers

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView gvTemp = (GridView)sender;
        gvUniqueID = gvTemp.UniqueID;
        gvNewPageIndex = e.NewPageIndex;
        GridView1.DataBind();
    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Addvalue")
        {
            try
            {

                //GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                GridView gvTemp = (GridView)sender;
                gvUniqueID = gvTemp.UniqueID;

                string strSlabID = gvTemp.DataKeys[0].Value.ToString();
                string earnid = ((DropDownList)gvTemp.FooterRow.FindControl("txtallowance")).SelectedItem.Value;
                string strename = ((DropDownList)gvTemp.FooterRow.FindControl("txtallowance")).SelectedItem.Text;
                string strvalue = ((TextBox)gvTemp.FooterRow.FindControl("txtvalue0")).Text;

                LinkButton clickedButton = (LinkButton)e.CommandSource;

                GridViewRow childGridViewRow = clickedButton.NamingContainer as GridViewRow;

                GridView childGrid = childGridViewRow.NamingContainer as GridView;

                GridViewRow parentRow = childGrid.NamingContainer as GridViewRow;

                GridViewRow optimisedParentRow = ((LinkButton)e.CommandSource).NamingContainer.NamingContainer.NamingContainer as GridViewRow;


                if (optimisedParentRow != null)
                {
                    strSlabID = ((Label)optimisedParentRow.FindControl("lblslabid")).Text;
                    eid = ((Label)optimisedParentRow.FindControl("lblempid")).Text;
                    fd = ((Label)optimisedParentRow.FindControl("txtfromdate")).Text;
                    td = ((Label)optimisedParentRow.FindControl("txttodate")).Text;
                }

                string strSQL = "";
                strSQL = "set dateformat dmy; INSERT INTO paym_emp_earnings (pn_CompanyID, pn_BranchID, pn_EmployeeID, pn_EarningsID, Pid, n_Amount,  " +
                        "d_date, c_eligible, from_date, to_date, Flag) VALUES ('" + employee.CompanyId + "','" + employee.BranchId + "','" + eid + "','" + earnid + "','" + strSlabID + "','" + strvalue + "','" +
                        DateTime.Now.ToString("dd/MM/yyyy") + "', 'Y', '" + fd + "','" + td + "', 'Y'); set dateformat mdy;";

                SqlDataSource1.InsertCommand = strSQL;
                SqlDataSource1.Insert();
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Added successfully');</script>");
                hr();


            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message.ToString().Replace("'", "") + "');</script>");
            }
        }

        else if (e.CommandName == "Edit")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            GridView gvTemp = (GridView)sender;
            gvUniqueID = gvTemp.UniqueID;

            string strSlabID = gvTemp.DataKeys[0].Value.ToString();
            string earnid = ((Label)row.Cells[0].FindControl("lblearningsID")).Text;
            string strename = ((TextBox)row.Cells[1].FindControl("txtename")).Text;
            string strvalue = ((TextBox)row.Cells[2].FindControl("txtvalue")).Text;

            LinkButton clickedButton = (LinkButton)e.CommandSource;

            GridViewRow childGridViewRow = clickedButton.NamingContainer as GridViewRow;

            GridView childGrid = childGridViewRow.NamingContainer as GridView;

            GridViewRow parentRow = childGrid.NamingContainer as GridViewRow;

            GridViewRow optimisedParentRow = ((LinkButton)e.CommandSource).NamingContainer.NamingContainer.NamingContainer as GridViewRow;


            if (optimisedParentRow != null)
            {
                fd = ((Label)optimisedParentRow.FindControl("txtfromdate")).Text;
                td = ((Label)optimisedParentRow.FindControl("txttodate")).Text;
            }


            string strSQL = "";
            strSQL = "set dateformat dmy; INSERT INTO paym_emp_earnings (pn_CompanyID, pn_BranchID, pn_EmployeeID, pn_EarningsID, n_Amount,  " +
                    "d_date, c_eligible, from_date, to_date, Flag) VALUES ('" + employee.CompanyId + "','" + employee.BranchId + "','" + strSlabID + "','" + earnid + "','" + strvalue + "','" +
                    DateTime.Now.ToString("dd/MM/yyyy") + "', 'Y', '" + fd + "','" + td + "', 'Y'); set dateformat mdy;";

            SqlDataSource1.InsertCommand = strSQL;
            SqlDataSource1.Insert();
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Added successfully');</script>");
            hr();
        }
    }


    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridView gvTemp = (GridView)sender;
        gvUniqueID = gvTemp.UniqueID;
        string strPromotionID = ((Label)gvTemp.Rows[e.RowIndex].FindControl("lblID")).Text;
        string strSQL = "";

        try
        {
            strSQL = "DELETE from paym_emp_earnings WHERE ID = " + strPromotionID + " and pn_CompanyID='" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "'";
            SqlDataSource dsTemp = new SqlDataSource();
            dsTemp.ConnectionString = ConfigurationManager.AppSettings["Connectionstring"];
            dsTemp.DeleteCommand = strSQL;
            dsTemp.Delete();
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted successfully');</script>");
            hr();
        }
        catch (Exception ex)
        {

        }
    }

    protected void GridView2_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        if (e.Exception != null)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + e.Exception.Message.ToString().Replace("'", "") + "');</script>");
            e.ExceptionHandled = true;
        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((DataRowView)e.Row.DataItem)["ID"].ToString() == String.Empty) e.Row.Visible = false;
        }

    }

    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }


    #endregion

    protected void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_department.SelectedIndex = 0;
    }


    protected void txttdate_TextChanged(object sender, EventArgs e)
    {
        DateTime d1, d2;

        string strt = ((TextBox)GridView1.FooterRow.FindControl("txtfdate")).Text;

        string end = ((TextBox)GridView1.FooterRow.FindControl("txttdate")).Text;
        
      //  ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alertbox", "alert('asd')", true);

        d1 = DateTime.Parse(strt);
        d2 = DateTime.Parse(end);
        if (d2 < d1)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alertbox", "alert('please enter end date greater than start date')", true);
            ((TextBox)GridView1.FooterRow.FindControl("txttdate")).Text = string.Empty;
        
        }
    }
}    

