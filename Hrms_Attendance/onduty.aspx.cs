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
using ePayHrms.Company;
using ePayHrms.Employee;
using ePayHrms.BE.Recruitment;
using System.Drawing;

public partial class Hrms_Company_Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    string s_login_role;
    Double diff = 0;
    DataTable dt;
    int grd;
    int ddl_i;
    string _Value;
    string ST;
    DataSet ds = new DataSet();
    static int code;
    Collection<Employee> EmployeeList;

    protected void Page_Load(object sender, EventArgs e)
    {
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        r.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        r.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);   
        
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        if (s_login_role == "e")
        {
            employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
        }

        if (!IsPostBack)
        {
            if (s_login_role == "a")
            {
                admin_load();
            }

            else if (s_login_role == "h")
            {
                ddl_branch.Visible = false;
                ddl_Designation_load();
                //hr_load(); //hr onduty Load
                load(); //HR paym_Permission Load
               // onduty_load();
                //BindDepartment();
                ddl_Department_load();
                ((DropDownList)GridViewPermission.FooterRow.FindControl("ddl_Status")).SelectedIndex = 1;
                ((DropDownList)GridViewPermission.FooterRow.FindControl("ddl_Status")).Enabled = false;
            }

            else if (s_login_role == "e")
            {
                employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
                ddl_branch.Visible = false;
                //employee_load(); //Emp onduty Load
                //hr_load(); // HR onduty Load
                empload(); //Emp paym_permission
                bindempDetails(); //ddl Emp Permission
                bindDepDetails(); //ddl Dept Permission
               // BindDepartment();
            }

         }           
    }

    public void ddl_Department_load()
    {
        EmployeeList = employee.fn_getDepartmentList1(employee.BranchId);
        if (EmployeeList.Count > 0)
        {

            for (int ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {

                if (ddl_i == -1)
                {
                    ListItem es_list = new ListItem();

                    es_list.Text = "Select Department";
                    es_list.Value = "0";
                    ddl_department.Items.Add(es_list);
                    ((DropDownList)GridViewPermission.FooterRow.FindControl("ddl_Department")).Items.Add(es_list);
                }
                else
                {
                    ListItem es_list = new ListItem();

                    es_list.Value = EmployeeList[ddl_i].DepartmentId.ToString();
                    es_list.Text = EmployeeList[ddl_i].DepartmentName.ToString();
                    ddl_department.Items.Add(es_list);
                    ((DropDownList)GridViewPermission.FooterRow.FindControl("ddl_Department")).Items.Add(es_list);
                }
            }
        }
    }

    public void ddl_Designation_load()
    {
        EmployeeList = employee.fn_getDesignation(employee);
        if (EmployeeList.Count > 0)
        {
            for (int ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem es_list = new ListItem();

                    es_list.Text = "Select";
                    es_list.Value = "0";
                    ddl_approve.Items.Add(es_list);
                }
                else
                {
                    ListItem es_list = new ListItem();

                    es_list.Value = EmployeeList[ddl_i].DesignationId.ToString();
                    es_list.Text = EmployeeList[ddl_i].DesignationName.ToString();
                    ddl_approve.Items.Add(es_list);
                }
            }
        }
    }

    public void ddl_Employee_load()
    {
        ddl_ename.Items.Clear();
        employee.DepartmentId = Convert.ToInt32(ddl_department.SelectedValue);
        EmployeeList = employee.fn_getEmployeeDepartment(employee);
        if (EmployeeList.Count > 0)
        {
            for (int ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem es_list = new ListItem();
                    es_list.Text = "Select Employee";
                    es_list.Value = "0";
                    ddl_ename.Items.Add(es_list);
                }
                else
                {
                    ListItem es_list = new ListItem();
                    es_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
                    es_list.Text = EmployeeList[ddl_i].LastName.ToString();
                    ddl_ename.Items.Add(es_list);
                }
            }
        }
    }

    public void empload()
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from Paym_Permission where CompanyID='" + employee.CompanyId + "' and BranchID='" + employee.BranchId + "' and EmployeeID='" +employee.EmployeeId + "'",con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridViewPermission.DataSource = ds;
            GridViewPermission.DataBind();
            int columnCount = GridViewPermission.Rows[0].Cells.Count;
            GridViewPermission.Rows[0].Cells.Clear();
            GridViewPermission.Rows[0].Cells.Add(new TableCell());
            GridViewPermission.Rows[0].Cells[0].ColumnSpan = columnCount;
            GridViewPermission.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            GridViewPermission.DataSource = ds;
            GridViewPermission.DataBind();
        }
        
        con.Close();
    }

    public void bindempDetails()
    {
        
        con.Open();
        SqlCommand cmd = new SqlCommand("select pn_EmployeeID,Employee_First_Name from paym_employee where pn_CompanyID='" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and pn_EmployeeID='" + employee.EmployeeId + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        ((DropDownList)GridViewPermission.FooterRow.FindControl("ddlEmployee_Code")).DataSource = ds;
        ((DropDownList)GridViewPermission.FooterRow.FindControl("ddlEmployee_Code")).DataValueField = "pn_EmployeeID";
        ((DropDownList)GridViewPermission.FooterRow.FindControl("ddlEmployee_Code")).DataTextField = "Employee_First_Name";
        ((DropDownList)GridViewPermission.FooterRow.FindControl("ddlEmployee_Code")).DataBind();       
        con.Close();
        ((DropDownList)GridViewPermission.FooterRow.FindControl("ddlEmployee_Code")).Enabled = false;
        
    }
    public void bindDepDetails()
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("select a.v_DepartmentName,a.pn_DepartmentID from paym_department a, paym_employee_profile1 b  where a.pn_CompanyID=b.pn_CompanyID and a.pn_DepartmentID=b.pn_DepartmentID and b.pn_BranchID='" + employee.BranchId + "'and b.pn_CompanyID='" + employee.CompanyId + "' and b.pn_EmployeeID='" + employee.EmployeeId + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        ((DropDownList)GridViewPermission.FooterRow.FindControl("ddl_Department")).DataSource = ds;
        ((DropDownList)GridViewPermission.FooterRow.FindControl("ddl_Department")).DataValueField = "pn_DepartmentID";
        ((DropDownList)GridViewPermission.FooterRow.FindControl("ddl_Department")).DataTextField = "v_DepartmentName";
        ((DropDownList)GridViewPermission.FooterRow.FindControl("ddl_Department")).DataBind();
        con.Close();
        ((DropDownList)GridViewPermission.FooterRow.FindControl("ddl_Department")).Enabled = false;
    }

    public void load()
    {
        con.Open();
        var myDate = DateTime.Now;
        var startOfMonth = new DateTime(myDate.Year, myDate.Month, 1);
        var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
        string select;
        if (s_login_role == "e")
        {
            select = "set dateformat dmy;select * from Paym_Permission where CompanyID='" + employee.CompanyId + "' and BranchID='" + employee.BranchId + "' and EmployeeID='"+employee.EmployeeId+"' and date >='" + startOfMonth + "' and date <= '" + endOfMonth + "' order by date asc;set dateformat mdy;";
        }
        else
        {
            select = "set dateformat dmy;select * from Paym_Permission where CompanyID='" + employee.CompanyId + "' and BranchID='" + employee.BranchId + "' and date >='" + startOfMonth + "' and date <= '" + endOfMonth + "' order by date asc;set dateformat mdy;";
        }
        SqlDataAdapter da = new SqlDataAdapter(select, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridViewPermission.DataSource = ds;
            GridViewPermission.DataBind();
            int columnCount = GridViewPermission.Rows[0].Cells.Count;
            GridViewPermission.Rows[0].Cells.Clear();
            GridViewPermission.Rows[0].Cells.Add(new TableCell());
            GridViewPermission.Rows[0].Cells[0].ColumnSpan = columnCount;
            GridViewPermission.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            GridViewPermission.DataSource = ds;
            GridViewPermission.DataBind();
        }
        GridViewPermission.ShowFooter = false;
        con.Close();
    }

    public void load_dates()
    {
        con.Open();
        string select;
        if (s_login_role == "e")
        {
            select = "set dateformat dmy;select * from Paym_Permission where CompanyID='" + employee.CompanyId + "' and BranchID='" + employee.BranchId + "' and EmployeeID='"+ employee.EmployeeId + "' and date >='" + Txt_fdate.Text + "' and date <= '" + Txt_tdate.Text + "' order by date asc;set dateformat mdy;";
        }
        else
        {
            select = "set dateformat dmy;select * from Paym_Permission where CompanyID='" + employee.CompanyId + "' and BranchID='" + employee.BranchId + "' and date >='" + Txt_fdate.Text + "' and date <= '" + Txt_tdate.Text + "' order by date asc;set dateformat mdy;";
          }
        SqlDataAdapter da = new SqlDataAdapter(select, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridViewPermission.DataSource = ds;
            GridViewPermission.DataBind();
            int columnCount = GridViewPermission.Rows[0].Cells.Count;
            GridViewPermission.Rows[0].Cells.Clear();
            GridViewPermission.Rows[0].Cells.Add(new TableCell());
            GridViewPermission.Rows[0].Cells[0].ColumnSpan = columnCount;
            GridViewPermission.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            GridViewPermission.DataSource = ds;
            GridViewPermission.DataBind();
        }
        GridViewPermission.ShowFooter = true;
        con.Close();
    }

    //public void BindDepartment()
    //{
    //    Collection<Employee> departmentList = employee.fn_Department();
    //    if (departmentList.Count > 0)
    //    {
    //        ((DropDownList)GridViewPermission.FooterRow.FindControl("ddl_Department")).DataSource = departmentList;
    //        ((DropDownList)GridViewPermission.FooterRow.FindControl("ddl_Department")).DataValueField = "DepartmentID";
    //        ((DropDownList)GridViewPermission.FooterRow.FindControl("ddl_Department")).DataTextField = "DepartmentName";
    //        ((DropDownList)GridViewPermission.FooterRow.FindControl("ddl_Department")).DataBind();   
    //    }
    //}   

    public void hr()
    {
        try
        {
            Collection<Employee> EmployeeList = employee.fn_getEmployeeList(employee);
            if (EmployeeList.Count > 0)
            {                //row_showdet_btn.Visible = true;
                for (ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
                {
                    if (ddl_i == -1)
                    {
                        ListItem e_list = new ListItem();
                        e_list.Text = "Select Employee";
                        e_list.Value = "0";
                        ((DropDownList)GridViewPermission.FooterRow.FindControl("ddlEmployee_Code")).Items.Add(e_list);
                      }
                    else
                    {
                        ListItem e_list = new ListItem();
                        e_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
                        e_list.Text = EmployeeList[ddl_i].LastName.ToString();
                        ((DropDownList)GridViewPermission.FooterRow.FindControl("ddlEmployee_Code")).Items.Add(e_list);                
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No employees available');", true);          }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error');", true);
        }
    }    

    public void employee_load()
    {     

        SqlCommand cmd_grid = new SqlCommand("select * from onduty", con);
        SqlDataAdapter ad = new SqlDataAdapter(cmd_grid);
        DataSet ds = new DataSet();
        ad.Fill(ds);

        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView_ondutyentry.DataSource = ds;
            GridView_ondutyentry.DataBind();
            int columnCount = GridView_ondutyentry.Rows[0].Cells.Count;
            GridView_ondutyentry.Rows[0].Cells.Clear();
            GridView_ondutyentry.Rows[0].Cells.Add(new TableCell());
            GridView_ondutyentry.Rows[0].Cells[0].ColumnSpan = columnCount;
            GridView_ondutyentry.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            GridView_ondutyentry.DataSource = ds;
            GridView_ondutyentry.DataBind();
        }               
    }

    public void onduty_load()
    {

        SqlCommand cmd_grid = new SqlCommand("select * from onduty where pn_CompanyID = '" + employee.CompanyId + "' and pn_Branchid= '" + employee.BranchId + "' ", con);
        SqlDataAdapter ad = new SqlDataAdapter(cmd_grid);
        DataSet ds = new DataSet();
        ad.Fill(ds);

        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView_ondutyentry.DataSource = ds;
            GridView_ondutyentry.DataBind();
            int columnCount = GridView_ondutyentry.Rows[0].Cells.Count;
            GridView_ondutyentry.Rows[0].Cells.Clear();
            GridView_ondutyentry.Rows[0].Cells.Add(new TableCell());
            GridView_ondutyentry.Rows[0].Cells[0].ColumnSpan = columnCount;
            GridView_ondutyentry.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            GridView_ondutyentry.DataSource = ds;
            GridView_ondutyentry.DataBind();
        }
    }

    public void hr_load()
    {
        onduty_table();
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView_hr.DataSource = ds;
            GridView_hr.DataBind();
            int columnCount = GridView_hr.Rows[0].Cells.Count;
            GridView_hr.Rows[0].Cells.Clear();
            GridView_hr.Rows[0].Cells.Add(new TableCell());
            GridView_hr.Rows[0].Cells[0].ColumnSpan = columnCount;
            GridView_hr.Rows[0].Cells[0].Text = "No Records Found..";
        }

        else
        {
            GridView_hr.DataSource = ds;
            GridView_hr.DataBind();
        }
    }

    public void onduty_table()
    {
        SqlCommand cmd_grid_hr = new SqlCommand("select * from onduty where pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + employee.BranchId + "'  ", con);
        SqlDataAdapter ad = new SqlDataAdapter(cmd_grid_hr);
        ds = new DataSet();
        ad.Fill(ds);      
        dt = ds.Tables[0];
    }

    public void admin_load()
    {
        
        SqlDataAdapter ad = new SqlDataAdapter("select branchname,pn_branchid from paym_branch", con);
        DataSet ds = new DataSet();
        ad.Fill(ds, "paym_branch");
        ddl_branch.DataSource = ds;
        ddl_branch.DataTextField = "branchname";
        ddl_branch.DataValueField = "pn_branchid";
        ddl_branch.DataBind();

        ddl_branch.Items.Insert(0, "Select Branch");            
        
    }

    public void clear()
    {
         txt_ondutydat.Text = "";
         ddl_onduty_fstat.SelectedIndex = 0;
         txt_todate.Text = "";
         ddl_to_tstat.SelectedIndex = 0;
         txt_tot_days.Text = "";
         txt_sdate.Text = "";
         txt_reason.Text = "";
         ddl_priority.SelectedIndex = 0;
    }


    protected void ddl_onduty_fstat_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_onduty_fstat.SelectedItem.Text == "First Half")
        {
             diff = 0.5;
            txt_todate.Enabled = false;
            ddl_to_tstat.SelectedItem.Text = ddl_onduty_fstat.SelectedItem.Text;
            ddl_to_tstat.Enabled = false;
        }
        else
        {
            txt_todate.Enabled = true;
            ddl_to_tstat.Enabled = true;
        }

        txt_tot_days.Text = diff.ToString();
        txt_tot_days.Focus();
    }


    protected void ddl_to_tstat_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime fr_date = Convert.ToDateTime(txt_ondutydat.Text);
        DateTime to_date = Convert.ToDateTime(txt_todate.Text);
        diff = (to_date - fr_date).Days;

        if (ddl_onduty_fstat.SelectedItem.Text == "Full Day" && ddl_to_tstat.SelectedItem.Text == "Full Day")
        {
            diff = diff + 1;
        }
        else if (ddl_onduty_fstat.SelectedItem.Text != "Full Day" && (ddl_to_tstat.SelectedItem.Text == "Full Day"))
        {
            diff = diff + 0.5;
        }
        else if (ddl_onduty_fstat.SelectedItem.Text == "Full Day" && ddl_to_tstat.SelectedItem.Text != "Full Day")
        {
            diff = diff + 0.5;
        }

        txt_tot_days.Text = diff.ToString();
        txt_tot_days.Focus();
    }
    protected void GridView_ondutyentry_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        con.Open();
        string id = ((Label)GridView_ondutyentry.Rows[e.RowIndex].Cells[0].FindControl("lbl_sno")).Text;
        SqlCommand cmd_del = new SqlCommand("delete from onduty where ref_no ='" + id + "' and pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + employee.BranchId + "' ", con);
        cmd_del.ExecuteNonQuery();
        con.Close();
        employee_load();       
    }
    protected void GridView_ondutyentry_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView_ondutyentry.EditIndex = e.NewEditIndex;

        if (s_login_role == "a")
        {

        }
        else if (s_login_role == "h")
        {
            employee_load();
        }
        else if (s_login_role == "e")
        {
            employee_load();
        }
    }
    protected void GridView_ondutyentry_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView_ondutyentry.EditIndex = -1;
        
        if (s_login_role == "e" || s_login_role == "h")
        {
            employee_load();
        }
    }
    protected void GridView_ondutyentry_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string E_sno, E_fstatus, E_tstatus, E_fdat, E_tdat, E_subon, E_reason, E_priority, E_approval;
        string Ed_fdat, Ed_tdat, Ed_sdat;

        decimal E_tot_day;

        GridViewRow gvrow = GridView_ondutyentry.Rows[e.RowIndex];

        E_sno = ((Label)gvrow.FindControl("E_lbl_sno")).Text;
        E_fdat = ((TextBox)gvrow.FindControl("E_txt_ondutydat")).Text;
        E_fstatus = ((DropDownList)gvrow.FindControl("E_ddl_onduty_fstat")).SelectedItem.Text;
        E_tdat = ((TextBox)gvrow.FindControl("E_txt_todate")).Text;
        E_tstatus = ((DropDownList)gvrow.FindControl("E_ddl_to_tstat")).SelectedItem.Text;
        E_tot_day = Convert.ToDecimal(((TextBox)gvrow.FindControl("E_txt_tot_days")).Text);
        E_subon = ((TextBox)gvrow.FindControl("E_txt_sdate")).Text;
        E_reason = ((TextBox)gvrow.FindControl("E_txt_reason")).Text;
        E_priority = ((DropDownList)gvrow.FindControl("E_ddl_priority")).SelectedItem.Text;
        E_approval = ((Label)gvrow.FindControl("E_lbl_approval")).Text;

        Ed_fdat = employee.Convert_ToSqlDatestring(E_fdat);
        Ed_tdat = employee.Convert_ToSqlDatestring(E_tdat);
        Ed_sdat = employee.Convert_ToSqlDatestring(E_subon);
        con.Open();
        SqlCommand cmd_up = new SqlCommand("update onduty set onduty_dat='" + Ed_fdat + "',fstatus='" +E_fstatus + "',todat='" + Ed_tdat + "',tstatus='" + E_tstatus + "',tot_days='" + E_tot_day + "',sub_dat='" + Ed_sdat + "',reason='" + E_reason + "',priority='" + E_priority + "',approval='" + E_approval + "' where ref_no='" + E_sno + "' and pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + employee.BranchId + "' ", con);
        cmd_up.ExecuteNonQuery();
        con.Close();
        GridView_ondutyentry.EditIndex = -1;

        if (s_login_role == "e" || s_login_role=="h")
        {
            employee_load();
        }
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Record Updated Successfully');", true);
        
    }
    protected void E_txt_ondutydat_TextChanged(object sender, EventArgs e)
    {
        ((TextBox)GridView_ondutyentry.Rows[0].Cells[3].FindControl("E_txt_todate")).Text = ((TextBox)GridView_ondutyentry.Rows[0].Cells[1].FindControl("E_txt_ondutydat")).Text;
    }

    protected void txt_ondutydat_TextChanged(object sender, EventArgs e)
    {
        txt_todate.Text = txt_ondutydat.Text;
    }
    protected void E_ddl_onduty_fstat_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList drp = (DropDownList)sender;
        GridViewRow r1 = (GridViewRow)drp.NamingContainer;
        
        
        if (((DropDownList)GridView_ondutyentry.Rows[r1.RowIndex].Cells[2].FindControl("E_ddl_onduty_fstat")).SelectedItem.Text == "First Half")
        {
            diff = 0.5;
            ((TextBox)GridView_ondutyentry.Rows[r1.RowIndex].Cells[3].FindControl("E_txt_todate")).Enabled = false;
            ((DropDownList)GridView_ondutyentry.Rows[r1.RowIndex].Cells[4].FindControl("E_ddl_to_tstat")).SelectedItem.Text = ((DropDownList)GridView_ondutyentry.Rows[r1.RowIndex].Cells[2].FindControl("E_ddl_onduty_fstat")).SelectedItem.Text;
            ((DropDownList)GridView_ondutyentry.Rows[r1.RowIndex].Cells[4].FindControl("E_ddl_to_tstat")).Enabled = false;
        }
        else
        {
            ((TextBox)GridView_ondutyentry.Rows[r1.RowIndex].Cells[3].FindControl("E_txt_todate")).Enabled = true;
            ((DropDownList)GridView_ondutyentry.Rows[r1.RowIndex].Cells[4].FindControl("E_ddl_to_tstat")).Enabled = true;
        }

        ((TextBox)GridView_ondutyentry.Rows[r1.RowIndex].Cells[5].FindControl("E_txt_tot_days")).Text = diff.ToString();
        ((TextBox)GridView_ondutyentry.Rows[r1.RowIndex].Cells[5].FindControl("E_txt_tot_days")).Focus();
    }
    protected void E_ddl_to_tstat_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList drp = (DropDownList)sender;
        GridViewRow r1 = (GridViewRow)drp.NamingContainer;        

        DateTime fr_date = Convert.ToDateTime(((TextBox)GridView_ondutyentry.Rows[r1.RowIndex].Cells[2].FindControl("E_txt_ondutydat")).Text);
        DateTime to_date = Convert.ToDateTime(((TextBox)GridView_ondutyentry.Rows[r1.RowIndex].Cells[3].FindControl("E_txt_todate")).Text);
        diff = (to_date - fr_date).Days;

        if (((DropDownList)GridView_ondutyentry.Rows[r1.RowIndex].Cells[2].FindControl("E_ddl_onduty_fstat")).SelectedItem.Text == "Full Day" && ((DropDownList)GridView_ondutyentry.Rows[r1.RowIndex].Cells[4].FindControl("E_ddl_to_tstat")).SelectedItem.Text == "Full Day")
        {
            diff = diff + 1;
        }
        else if (((DropDownList)GridView_ondutyentry.Rows[r1.RowIndex].Cells[2].FindControl("E_ddl_onduty_fstat")).SelectedItem.Text != "Full Day" && ((DropDownList)GridView_ondutyentry.Rows[r1.RowIndex].Cells[4].FindControl("E_ddl_to_tstat")).SelectedItem.Text == "Full Day")
        {
            diff = diff + 0.5;
        }
        else if (((DropDownList)GridView_ondutyentry.Rows[r1.RowIndex].Cells[2].FindControl("E_ddl_onduty_fstat")).SelectedItem.Text == "Full Day" && ((DropDownList)GridView_ondutyentry.Rows[r1.RowIndex].Cells[4].FindControl("E_ddl_to_tstat")).SelectedItem.Text != "Full Day")
        {
            diff = diff + 0.5;
        }

        ((TextBox)GridView_ondutyentry.Rows[r1.RowIndex].Cells[5].FindControl("E_txt_tot_days")).Text = diff.ToString();
        ((TextBox)GridView_ondutyentry.Rows[r1.RowIndex].Cells[5].FindControl("E_txt_tot_days")).Focus();

    }
    protected void ddl_branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        admin_oper();
    }
    public void admin_oper()
    {
        SqlCommand cmd_grid_hr = new SqlCommand("select * from onduty where pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + ddl_branch.SelectedItem.Value + "'  ", con);
        SqlDataAdapter ad1 = new SqlDataAdapter(cmd_grid_hr);
        DataSet ds1 = new DataSet();
        ad1.Fill(ds1);

        if (ds1.Tables[0].Rows.Count == 0)
        {
            ds1.Tables[0].Rows.Add(ds1.Tables[0].NewRow());
            GridView_hr.DataSource = ds1;
            GridView_hr.DataBind();
            int columnCount = GridView_hr.Rows[0].Cells.Count;
            GridView_hr.Rows[0].Cells.Clear();
            GridView_hr.Rows[0].Cells.Add(new TableCell());
            GridView_hr.Rows[0].Cells[0].ColumnSpan = columnCount;
            GridView_hr.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            GridView_hr.DataSource = ds1;
            GridView_hr.DataBind();
        }        
    }
    protected void GridView_hr_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("detail"))
        {
            onduty_table();
            int index = Convert.ToInt32(e.CommandArgument);
            code = Convert.ToInt32(GridView_hr.DataKeys[index].Value);
            IEnumerable<DataRow> query = from i in dt.AsEnumerable()
                                         where i.Field<int>("sno").Equals(code)
                                         select i;
            DataTable detailTable = query.CopyToDataTable<DataRow>();
            DetailsView1.DataSource = detailTable;
            DetailsView1.DataBind();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#dialog').dialog({modal: true});");
            sb.Append(@"</script>");
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ModalScript", sb.ToString(), false);
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowModal", "<script type='text/javascript'>ShowModal();</script>", false);
            //ClientScriptManager manager = Page.ClientScript;
            //manager.RegisterStartupScript(this.GetType(), "Call", "ShowModal();", true);
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "KeyToIdentifythisScript", "ShowModal();", true);
        }

    }
    protected void DetailsView1_ModeChanging(object sender, DetailsViewModeEventArgs e)
    {
        DetailsView1.ChangeMode(e.NewMode);
        onduty_table();        
        IEnumerable<DataRow> query = from i in dt.AsEnumerable()
                                     where i.Field<int>("sno").Equals(code)
                                     select i;
        DataTable detailTable = query.CopyToDataTable<DataRow>();
        DetailsView1.DataSource = detailTable;
        DetailsView1.DataBind();
    }
    protected void DetailsView1_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
    {
        con.Open();
        string sno = ((Label)((DetailsView)sender).FindControl("lbl_Esno")).Text;
        string Fdate = ((TextBox)((DetailsView)sender).FindControl("txt_EFdate")).Text;
        string Tdate = ((TextBox)((DetailsView)sender).FindControl("txt_ETdate")).Text;
        string Totdays = ((TextBox)((DetailsView)sender).FindControl("txt_ETotdays")).Text;
        string Eapp = ((DropDownList)((DetailsView)sender).FindControl("ddl_EApp")).Text;
        string msg1 = ((TextBox)((DetailsView)sender).FindControl("lbl_Emsg1")).Text;
        string msg2 = ((TextBox)((DetailsView)sender).FindControl("lbl_Emsg2")).Text;
        SqlCommand cmd_up = new SqlCommand("set dateformat dmy;update onduty set onduty_dat='" + Fdate + "',todat='" + Tdate + "',tot_days='" + Totdays + "',approval='" + Eapp + "' where sno='" + sno + "' and pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + employee.BranchId + "';set dateformat mdy; ", con);
        cmd_up.ExecuteNonQuery();
        con.Close();
        DetailsView1.ChangeMode(DetailsViewMode.ReadOnly);
        onduty_table();
        IEnumerable<DataRow> query = from i in dt.AsEnumerable()
                                     where i.Field<int>("sno").Equals(code)
                                     select i;
        DataTable detailTable = query.CopyToDataTable<DataRow>();
        DetailsView1.DataSource = detailTable;
        DetailsView1.DataBind();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#currentdetail').modal('show');");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", sb.ToString(), false);
        hr_load();
    }
    protected void txt_ETdate_TextChanged(object sender, EventArgs e)
    {
        DateTime fr_date = Convert.ToDateTime(((TextBox)((DetailsView)sender).FindControl("txt_EFdate")).Text);
        DateTime to_date = Convert.ToDateTime(((TextBox)((DetailsView)sender).FindControl("txt_ETdate")).Text);
        diff = (to_date - fr_date).Days;
        ((TextBox)((DetailsView)sender).FindControl("txt_ETotdays")).Text = diff.ToString();
    }
    protected void GridViewPermission_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (s_login_role == "h")
        {
            if (e.CommandName == "add")
            {
                try
                {
                    string EmployeeID = ((DropDownList)GridViewPermission.FooterRow.FindControl("ddlEmployee_Code")).SelectedValue;
                    string EmployeeName = ((DropDownList)GridViewPermission.FooterRow.FindControl("ddlEmployee_Code")).SelectedItem.Text;
                    string date = employee.Convert_ToSqlDatestring(((TextBox)GridViewPermission.FooterRow.FindControl("txtDate")).Text);
                    string Session = ((DropDownList)GridViewPermission.FooterRow.FindControl("ddlSessio")).Text;
                    string Status = ((DropDownList)GridViewPermission.FooterRow.FindControl("ddl_Status")).Text;
                    if (date == "1900/01/01")
                    {

                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Select Permission date');", true);
                        goto Exx;
                        //return;
                    }
                    int Count = employee.Check_Count(EmployeeID, date);
                    if (Count < 2)
                    {
                        AddNewRecord(EmployeeID, EmployeeName, date, Session, Status);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Cannot Save. Maximum Permission limit reached for the month.!');", true);
                    }
                    Exx:
                    load();
                    GridViewPermission.FooterRow.Visible = true;
                    ddl_Department_load();
                    //BindDepartment();
                }
                catch
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter all details');", true);
                    //load();
                    //GridViewPermission.FooterRow.Visible = true;
                    //ddl_Department_load();
                    //return;
                }
            }
        }
        if (s_login_role == "a")
        {
            if (e.CommandName == "add")
            {
                string EmployeeID = ((DropDownList)GridViewPermission.FooterRow.FindControl("ddlEmployee_Code")).SelectedValue;
                string EmployeeName = ((DropDownList)GridViewPermission.FooterRow.FindControl("ddlEmployee_Code")).SelectedItem.Text;
                string date = employee.Convert_ToSqlDatestring(((TextBox)GridViewPermission.FooterRow.FindControl("txtDate")).Text);
                string Session = ((DropDownList)GridViewPermission.FooterRow.FindControl("ddlSessio")).Text;
                string Status = ((DropDownList)GridViewPermission.FooterRow.FindControl("ddl_Status")).Text;
                AddNewRecord(EmployeeID, EmployeeName, date, Session, Status);
            }

        }
        if (s_login_role == "e")
        {
            if (e.CommandName == "add")
            {
                string EmployeeID = ((DropDownList)GridViewPermission.FooterRow.FindControl("ddlEmployee_Code")).SelectedValue;
                string EmployeeName = ((DropDownList)GridViewPermission.FooterRow.FindControl("ddlEmployee_Code")).SelectedItem.Text;
                string date = employee.Convert_ToSqlDatestring(((TextBox)GridViewPermission.FooterRow.FindControl("txtDate")).Text);
                string Session = ((DropDownList)GridViewPermission.FooterRow.FindControl("ddlSessio")).Text;
                string Status = ((DropDownList)GridViewPermission.FooterRow.FindControl("ddl_Status")).Text;
                int Count = employee.Check_Count(EmployeeID, date);
                if (Count < 2)
                {
                    AddNewRecord(EmployeeID, EmployeeName, date, Session, Status);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Cannot Save. Maximum Permission limit reached for the month.!');", true);
                }
                empload();
                bindempDetails();
                bindDepDetails();
            }         
            
        }
    }
    private void AddNewRecord(string EmployeeID, string EmployeeName, string date, string Session, string Status)
    {
        try
        {
            con.Open();
            string query = "insert into Paym_Permission(CompanyID,BranchID,EmployeeID,EmployeeName,Date,Session,Status)values('" + employee.CompanyId + "','" + employee.BranchId + "','" + EmployeeID + "','" + EmployeeName + "','" + date + "','" + Session + "','" + Status + "')";
            SqlCommand myCommand = new SqlCommand(query, con);
            myCommand.ExecuteNonQuery();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Saved Successfully!');", true);
        }
        catch (Exception ex)
        {
            string s = ex.Message;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Record Already Exists');", true);
        }

        finally
        {
            con.Close();
        }
    }
    protected void GridViewPermission_RowEditing(object sender, GridViewEditEventArgs e)
    {        
        if (s_login_role == "h")
        {
            GridViewPermission.EditIndex = e.NewEditIndex; // turn to edit mode
            load_dates();           
            //BindDepartment();
            ddl_Department_load();
        }
        if (s_login_role == "e")
        {
            GridViewPermission.EditIndex = e.NewEditIndex; // turn to edit mode
            empload();
            bindempDetails();
            bindDepDetails();


        }
    }
    protected void GridViewPermission_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        con.Open();
        GridViewRow Gvrow = GridViewPermission.Rows[e.RowIndex];
        if (Gvrow != null && s_login_role == "a")
        {
            string PermissionID = ((Label)Gvrow.FindControl("lbl_PermissionID_edit")).Text;
            string date = employee.Convert_ToSqlDatestring(((TextBox)Gvrow.FindControl("txtDate_edit")).Text);
            string Session = ((DropDownList)Gvrow.FindControl("ddlSessio_edit")).Text;
            string Status = ((DropDownList)Gvrow.FindControl("ddl_Status_edit")).Text;
            SqlCommand cmd = new SqlCommand("update Paym_Permission set  Date='" + date + "', Session='" + Session + "', Status='" + Status + "'  where PermissionID='" + PermissionID + "' and CompanyID= '" + employee.CompanyId + "' and  BranchID= '" + employee.BranchId + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            GridViewPermission.EditIndex = -1; // turn to edit mode
            load_dates();
            //BindDepartment();
            ddl_Department_load();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(' Data Updated');", true);
        }
        else if (Gvrow != null && s_login_role == "h")
        {
            string PermissionID = ((Label)Gvrow.FindControl("lbl_PermissionID_edit")).Text;
            string date = employee.Convert_ToSqlDatestring(((TextBox)Gvrow.FindControl("txtDate_edit")).Text);
            string Session = ((DropDownList)Gvrow.FindControl("ddlSessio_edit")).Text;
            string Status = ((DropDownList)Gvrow.FindControl("ddl_Status_edit")).Text;
            SqlCommand cmd = new SqlCommand("update Paym_Permission set  Date='" + date + "', Session='" + Session + "', Status='" + Status + "'  where PermissionID='" + PermissionID + "' and CompanyID= '" + employee.CompanyId + "' and  BranchID= '" + employee.BranchId + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            GridViewPermission.EditIndex = -1; // turn to edit mode
            load_dates();
            //BindDepartment();
            ddl_Department_load();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(' Data Updated');", true);
        }
        else 
        {
            if (Gvrow != null && s_login_role == "e")
            {
                string PermissionID = ((Label)Gvrow.FindControl("lbl_PermissionID_edit")).Text;
                string date = employee.Convert_ToSqlDatestring(((TextBox)Gvrow.FindControl("txtDate_edit")).Text);
                string Session = ((DropDownList)Gvrow.FindControl("ddlSessio_edit")).Text;
                string Status = ((DropDownList)Gvrow.FindControl("ddl_Status_edit")).Text;
                SqlCommand cmd = new SqlCommand("update Paym_Permission set  Date='" + date + "', Session='" + Session + "', Status='" + Status + "'  where PermissionID='" + PermissionID + "' and CompanyID= '" + employee.CompanyId + "' and  BranchID= '" + employee.BranchId + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();
                GridViewPermission.EditIndex = -1; // turn to edit mode
                empload();
                bindempDetails();
                bindDepDetails();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(' Data Updated');", true);
            }
        }
    }
    private void DeleteRecord(string ID)
    {
        string sqlStatement = "DELETE FROM Paym_Permission WHERE PermissionID = @PermissionID and BranchID = '" + employee.BranchId + "'";
        con.Open();
        SqlCommand cmd = new SqlCommand(sqlStatement,con);
        cmd.Parameters.AddWithValue("@PermissionID", ID);
        cmd.CommandType = CommandType.Text;
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(' Record Deleted');", true);     
    }
    protected void GridViewPermission_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (s_login_role == "h")
        {
            string ID = ((Label)GridViewPermission.Rows[e.RowIndex].Cells[1].FindControl("lbl_PermissionID")).Text;

            DeleteRecord(ID);
            load_dates();           
            //BindDepartment();
            ddl_Department_load();     
        }
        if (s_login_role == "e")
        {
            string ID = ((Label)GridViewPermission.Rows[e.RowIndex].Cells[1].FindControl("lbl_PermissionID")).Text;
            DeleteRecord(ID);
            empload();
            bindempDetails();
            bindDepDetails();
        }
    }
    protected void GridViewPermission_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
         if (s_login_role == "h")
          {
            GridViewPermission.EditIndex = -1;
            load_dates();         
            //BindDepartment();
            ddl_Department_load();
         }
         if (s_login_role == "e")
         {
             GridViewPermission.EditIndex = -1;
             empload();
             bindempDetails();
             bindDepDetails();
         }
    }
    protected void ddl_Department_SelectedIndexChanged(object sender, EventArgs e)
    {
        con.Open();
        string ddl_Department = ((DropDownList)GridViewPermission.FooterRow.FindControl("ddl_Department")).Text;
        SqlDataAdapter ad;
        ad = new SqlDataAdapter("select a.EmployeeCode,a.pn_EmployeeID,a.Employee_First_Name, b.pn_DepartmentId  from paym_employee a, paym_employee_profile1 b where a.pn_EmployeeID=b.pn_EmployeeID  and b.pn_companyid = '" + pay.CompanyId + "' and b.pn_BranchID = '" + pay.BranchId + "' and b.pn_DepartmentId='" + ddl_Department + "' and a.status = 'Y' order by a.employee_first_name asc", con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        ((DropDownList)GridViewPermission.FooterRow.FindControl("ddlEmployee_Code")).DataSource = ds;
        ((DropDownList)GridViewPermission.FooterRow.FindControl("ddlEmployee_Code")).DataValueField = "pn_EmployeeID";
        ((DropDownList)GridViewPermission.FooterRow.FindControl("ddlEmployee_Code")).DataTextField = "Employee_First_Name";
        ((DropDownList)GridViewPermission.FooterRow.FindControl("ddlEmployee_Code")).DataBind();
        GridViewPermission.FooterRow.Visible=true;
        con.Close();
    }

    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Employee_load();
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        string fstatus, tstatus, fdat, tdat, subon, reason, priority, approval, empname = "";
        string c_fdat, c_tdat, c_sdat, Refno = "OD-0";
        int cc = 1;
        decimal tot_day;
        con.Open();
        SqlCommand com = new SqlCommand("Select top 1 ref_no from onduty where pn_companyid = '" + employee.CompanyId + "' and pn_branchID = '" + employee.BranchId + "' order by ref_no desc", con);
        SqlDataReader read = com.ExecuteReader();
        if (read.Read())
        {
            Refno = read[0].ToString();
        }
        string[] splt = Refno.Split('-');
        cc = Convert.ToInt32(splt[1]);
        cc = cc + 1;
        Refno = "OD-" + cc.ToString();
        fdat = txt_ondutydat.Text;
        fstatus = ddl_onduty_fstat.SelectedItem.Text;
        tdat = txt_todate.Text;
        tstatus = ddl_to_tstat.SelectedItem.Text;
        tot_day = Convert.ToDecimal(txt_tot_days.Text);
        subon = txt_sdate.Text;
        reason = txt_reason.Text;
        priority = ddl_priority.SelectedItem.Text;
        approval = ddl_approve.SelectedItem.Text;
        employee.EmployeeId = Convert.ToInt32(ddl_ename.SelectedValue);
        if (approval == "Select")
        {
            approval = "";
        }
        if (fstatus == "Select" || tstatus == "Select" || priority == "Select")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter all data');", true);
            return;
        }
        c_fdat = employee.Convert_ToSqlDatestring(fdat);
        c_tdat = employee.Convert_ToSqlDatestring(tdat);
        c_sdat = employee.Convert_ToSqlDatestring(subon);
        SqlCommand cmd = new SqlCommand("select employee_first_name from paym_employee where pn_employeeid='" + employee.EmployeeId + "' ", con);
        SqlDataReader rd1;
        rd1 = cmd.ExecuteReader();
        if (rd1.Read())
        {
            empname = rd1[0].ToString();
        }
        rd1.Close();

        SqlCommand cmd_ins = new SqlCommand("insert into onduty(Ref_no,pn_companyid,pn_branchid,empid,empname,onduty_dat,fstatus,todat,tstatus,tot_days,sub_dat,reason,priority,approval) values('" + Refno + "','" + employee.CompanyId + "','" + employee.BranchId + "' ,'" + employee.EmployeeId + "','" + empname + "','" + c_fdat + "','" + fstatus + "','" + c_tdat + "','" + tstatus + "','" + tot_day + "','" + c_sdat + "','" + reason + "','" + priority + "','" + approval + "')", con);
        cmd_ins.ExecuteNonQuery();
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Saved Successfully');", true);

        clear();
        con.Close();
        employee_load();
    }
    protected void btn_Details_Click(object sender, EventArgs e)
    {
        load_dates();
    }
    protected void btn_month_Click(object sender, EventArgs e)
    {
        load();
    }
    protected void GridViewPermission_RowDataBound(object sender, GridViewRowEventArgs e)
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
}
