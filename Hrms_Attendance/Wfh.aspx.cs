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
using System.Net.Mail;

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


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            s_login_role = Request.Cookies["Login_temp_Role"].Value;

            if (s_login_role == "e")
                this.Page.MasterPageFile = "~/EHRMS.master";
            else
                this.Page.MasterPageFile = "~/HRMS.master";

        }
        catch (Exception ex)
        {

        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        r.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        r.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        if (s_login_role == "e" || s_login_role == "r")
        {
            employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
        }

        if (!IsPostBack)
        {
            if (s_login_role == "a")
            {

            }

            else if (s_login_role == "h")
            {

                //hr_load(); //hr onduty Load
                load(); //HR paym_Permission Load
                // onduty_load();
                BindDepartment();

            }

            else if (s_login_role == "r")
            {
                employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
                //hr_load(); //hr onduty Load
                bindempDetails();
                load_Reporting(); //HR paym_Permission Load
                // onduty_load();
                BindDepartment();
                load_request();

            }

            else if (s_login_role == "e")
            {
                employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
                load_request();
                //employee_load(); //Emp onduty Load
                //hr_load(); // HR onduty Load
                empload(); //Emp paym_permission
                bindempDetails(); //ddl Emp Permission
                bindDepDetails(); //ddl Dept Permission
                // BindDepartment();
            }
        }        
    }

    public void empload()
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from Paym_wfh where pn_CompanyID='" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and EmployeeID='" +employee.EmployeeId + "'",con);
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
        SqlCommand cmd = new SqlCommand("select EmployeeCode,Employee_First_Name from paym_employee where pn_CompanyID='" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and pn_EmployeeID='" + employee.EmployeeId + "'", con);
        SqlDataReader da = cmd.ExecuteReader();
        if (da.Read())
        {
            lblEmpCode.Text = da[0].ToString();
            lblEmpName.Text = da[1].ToString();
        }
        con.Close();
    }

    public void bindDepDetails()
    {
        //con.Open();
        //SqlCommand cmd = new SqlCommand("select a.v_DepartmentName,a.pn_DepartmentID from paym_department a, paym_employee_profile1 b  where a.pn_CompanyID=b.pn_CompanyID and a.pn_DepartmentID=b.pn_DepartmentID and b.pn_BranchID='" + employee.BranchId + "'and b.pn_CompanyID='" + employee.CompanyId + "' and b.pn_EmployeeID='" + employee.EmployeeId + "'", con);
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //da.Fill(ds);
        //((DropDownList)GridViewPermission.FooterRow.FindControl("ddl_Department")).DataSource = ds;
        //((DropDownList)GridViewPermission.FooterRow.FindControl("ddl_Department")).DataValueField = "pn_DepartmentID";
        //((DropDownList)GridViewPermission.FooterRow.FindControl("ddl_Department")).DataTextField = "v_DepartmentName";
        //((DropDownList)GridViewPermission.FooterRow.FindControl("ddl_Department")).DataBind();
        //con.Close();
        //((DropDownList)GridViewPermission.FooterRow.FindControl("ddl_Department")).Enabled = false;
    }

    public void load()
    {
        con.Open();
        var myDate = DateTime.Now;
        var startOfMonth = new DateTime(myDate.Year, myDate.Month, 1);
        var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
        SqlDataAdapter da = new SqlDataAdapter("set dateformat dmy;select * from Paym_wfh where pn_CompanyID='" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and fromdate >='" + startOfMonth + "' and fromdate <= '" + endOfMonth + "' order by fromdate asc;set dateformat mdy;", con);
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

    public void load_Reporting()
    {
        con.Open();
        var myDate = DateTime.Now;
        var startOfMonth = new DateTime(myDate.Year, myDate.Month, 1);
        var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
        SqlDataAdapter da = new SqlDataAdapter("set dateformat dmy;select * from Paym_wfh where pn_CompanyID='" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and (reportingID = " + employee.EmployeeId + " or EmployeeID = '" + employee.EmployeeId + "') and Status = 'NA' order by fromdate asc;set dateformat mdy;", con);
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

    public void load_request()
    {
        con.Open();
        var myDate = DateTime.Now;
        var startOfMonth = new DateTime(myDate.Year, myDate.Month, 1);
        var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
        SqlDataAdapter da = new SqlDataAdapter("set dateformat dmy;select * from Paym_wfh where pn_CompanyID='" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and EmployeeID = '" + employee.EmployeeId + "' order by fromdate asc;set dateformat mdy;", con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            grid_WFH.DataSource = ds;
            grid_WFH.DataBind();
            int columnCount = grid_WFH.Rows[0].Cells.Count;
            grid_WFH.Rows[0].Cells.Clear();
            grid_WFH.Rows[0].Cells.Add(new TableCell());
            grid_WFH.Rows[0].Cells[0].ColumnSpan = columnCount;
            grid_WFH.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            grid_WFH.DataSource = ds;
            grid_WFH.DataBind();
        }
        grid_WFH.ShowFooter = false;
        con.Close();
    }

    public void access()
    {


    }

    public void load_dates()
    {
        //con.Open();
        //SqlDataAdapter da = new SqlDataAdapter("set dateformat dmy;select * from Paym_Permission where CompanyID='" + employee.CompanyId + "' and BranchID='" + employee.BranchId + "' and date >='" + Txt_fdate.Text + "' and date <= '" + Txt_tdate.Text + "' order by date asc;set dateformat mdy;", con);
        //DataSet ds = new DataSet();
        //da.Fill(ds);
        //if (ds.Tables[0].Rows.Count == 0)
        //{
        //    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
        //    GridViewPermission.DataSource = ds;
        //    GridViewPermission.DataBind();
        //    int columnCount = GridViewPermission.Rows[0].Cells.Count;
        //    GridViewPermission.Rows[0].Cells.Clear();
        //    GridViewPermission.Rows[0].Cells.Add(new TableCell());
        //    GridViewPermission.Rows[0].Cells[0].ColumnSpan = columnCount;
        //    GridViewPermission.Rows[0].Cells[0].Text = "No Records Found..";
        //}
        //else
        //{
        //    GridViewPermission.DataSource = ds;
        //    GridViewPermission.DataBind();
        //}
        //GridViewPermission.ShowFooter = true;
        //con.Close();
    }

    public void BindDepartment()
    {
        //Collection<Employee> departmentList = employee.fn_Department();
        //if (departmentList.Count > 0)
        //{
        //    ((DropDownList)GridViewPermission.FooterRow.FindControl("ddl_Department")).DataSource = departmentList;
        //    ((DropDownList)GridViewPermission.FooterRow.FindControl("ddl_Department")).DataValueField = "DepartmentID";
        //    ((DropDownList)GridViewPermission.FooterRow.FindControl("ddl_Department")).DataTextField = "DepartmentName";
        //    ((DropDownList)GridViewPermission.FooterRow.FindControl("ddl_Department")).DataBind();   
        //}
    }   

    public void hr()
    {
        //try
        //{
        //    Collection<Employee> EmployeeList = employee.fn_getEmployeeList(employee);
        //    if (EmployeeList.Count > 0)
        //    {                //row_showdet_btn.Visible = true;
        //        for (ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
        //        {
        //            if (ddl_i == -1)
        //            {
        //                ListItem e_list = new ListItem();
        //                e_list.Text = "Select Employee";
        //                e_list.Value = "0";
        //                ((DropDownList)GridViewPermission.FooterRow.FindControl("ddlEmployee_Code")).Items.Add(e_list);
        //              }
        //            else
        //            {
        //                ListItem e_list = new ListItem();
        //                e_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
        //                e_list.Text = EmployeeList[ddl_i].LastName.ToString();
        //                ((DropDownList)GridViewPermission.FooterRow.FindControl("ddlEmployee_Code")).Items.Add(e_list);                
        //            }
        //        }
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No employees available');", true);          }
        //}
        //catch (Exception ex)
        //{
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error');", true);
        //}
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
        EmployeeList = employee.fn_getEmployeeList1(employee);
        con.Open();
        SqlCommand cmd_ins = new SqlCommand("Set dateformat dmy;insert into Paym_Wfh(pn_companyid,pn_branchid,EmployeeID,EmployeeCode,EmployeeName,FromDate,ToDate,Reason,Status,ReportingID) values('" + employee.CompanyId + "','" + employee.BranchId + "' ," + employee.EmployeeId + ",'" + lblEmpCode.Text + "','" + lblEmpName.Text + "','" + txt_fromdate.Text + "','" + txt_todate.Text + "','" + txtreason.Text + "','NA','" + EmployeeList[0].ReportID + "');Set dateformat mdy;", con);
        cmd_ins.ExecuteNonQuery();
        
        SendMail();
        con.Close();
        if (s_login_role == "e")
        {
            empload();
        }
        if (s_login_role == "r")
        {
            load_Reporting();
        }
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Saved Successfully');", true);
    }

    public void SendMail()
    {
       
        string Email = "", Cemail = "", Epass="", Emailsub ="", EmailText="";
        SqlCommand cmd = new SqlCommand("select Reporting_email from paym_employee where pn_companyID = '" + employee.CompanyId + "' and pn_BranchID = '" + employee.BranchId + "' and pn_employeeID = '" + employee.EmployeeId + "'", con);
        SqlDataReader rea = cmd.ExecuteReader();
        if (rea.Read())
        {
            //Email = ddl_mailto.SelectedValue;
            Email = "epayventures@gmail.com";
            Cemail = rea[0].ToString();


            //Epass = rea["EmailPass"].ToString();
            Emailsub = "Work From Home Request " + DateTime.Now.ToString("dd/MM/yyyy");

            try
            {
                using (MailMessage mailmessage = new MailMessage(Email, Cemail))
                {
                    

                    //FileUpload1.PostedFile.SaveAs(Server.MapPath(fn));
                    mailmessage.Subject = Emailsub;
                   
                    mailmessage.Body = "A new work from home request received. Please log in to your portal for more informantion.";
                    //System.Net.Mail.Attachment attach = new Attachment(Server.MapPath(fn));

                   
                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                    smtpClient.Credentials = new System.Net.NetworkCredential()
                    {
                        UserName = Email,
                        Password = "epayventures!@#"
                    };
                    smtpClient.EnableSsl = true;
                    smtpClient.Timeout = Int32.MaxValue;
                    smtpClient.Send(mailmessage);
                   
                    
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }
        
    }


    protected void GridViewPermission_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (s_login_role == "r")
        {
            employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
            GridViewPermission.EditIndex = e.NewEditIndex; // turn to edit mode
            load_Reporting();

            var lbl = this.GridViewPermission.Rows[e.NewEditIndex].Controls[0].FindControl("lbl_EmpID_edit") as Label;
            if (lbl.Text == Convert.ToString(employee.EmployeeId))
            {
                var ddlstat = this.GridViewPermission.Rows[e.NewEditIndex].Controls[0].FindControl("ddl_Status_edit") as DropDownList;
                ddlstat.Enabled = false;
            }
            //BindDepartment();
        }

    }

    protected void GridViewPermission_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        if (s_login_role == "r")
        {
            GridViewPermission.EditIndex = -1;
            load_Reporting();
        }
    }

    protected void GridViewPermission_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        con.Open();
        GridViewRow Gvrow = GridViewPermission.Rows[e.RowIndex];
        if (Gvrow != null && s_login_role == "a")
        {
            string PermissionID = ((Label)Gvrow.FindControl("lbl_PermissionID_edit")).Text;
            string Status = ((DropDownList)Gvrow.FindControl("ddl_Status_edit")).Text;
            SqlCommand cmd = new SqlCommand("update Paym_wfh set  status='" + Status + "' where WfhID='" + PermissionID + "' and pn_CompanyID= '" + employee.CompanyId + "' and  pn_BranchID= '" + employee.BranchId + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            GridViewPermission.EditIndex = -1; // turn to edit mode
            load();
            //BindDepartment();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(' Data Updated');", true);
        }
        else if (Gvrow != null && s_login_role == "r")
        {
            string PermissionID = ((Label)Gvrow.FindControl("lbl_PermissionID_edit")).Text;
            string Status = ((DropDownList)Gvrow.FindControl("ddl_Status_edit")).Text;
            SqlCommand cmd = new SqlCommand("update Paym_wfh set  status='" + Status + "' where WfhID='" + PermissionID + "' and pn_CompanyID= '" + employee.CompanyId + "' and  pn_BranchID= '" + employee.BranchId + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            GridViewPermission.EditIndex = -1; // turn to edit mode
            load_Reporting();
            //BindDepartment();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Saved Successfully');", true);
        }
    }

    protected void GridViewPermission_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (s_login_role == "r")
        {
            string ID = ((Label)GridViewPermission.Rows[e.RowIndex].Cells[1].FindControl("lbl_PermissionID")).Text;

            DeleteRecord(ID);
            load_Reporting();
           // BindDepartment();
        }
    }

    private void DeleteRecord(string ID)
    {
        string sqlStatement = "DELETE FROM Paym_Wfh WHERE WfhID = @PermissionID and BranchID = '" + employee.BranchId + "'";
        con.Open();
        SqlCommand cmd = new SqlCommand(sqlStatement, con);
        cmd.Parameters.AddWithValue("@PermissionID", ID);
        cmd.CommandType = CommandType.Text;
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Deleted Successfully');", true);
    }

    protected void GridViewPermission_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (s_login_role == "h" || s_login_role == "e")
            {
                ImageButton DelButton = (ImageButton)e.Row.Cells[8].Controls[0];
                DelButton.Visible = false;
                //ImageButton EditButton = (ImageButton)e.Row.Cells[7].Controls[1];
                //EditButton.Visible = false;
            }
        }
    }

}
