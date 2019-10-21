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

    Collection<Company> CompanyList, ddlBranchsList;
    Collection<Employee> EmployeeList;
    string s_login_role;
    string s_form = "", _Value;
    DataSet ds_userrights;
    string doco;

    protected void Page_Load(object sender, EventArgs e)
    {
        

        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
       
        lbl_Error.Text = "";

        if (!IsPostBack)
        {
            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {

                switch (s_login_role)
                {

                    case "a":
                        grd_view.Visible = false;
                        ddl_Branch_load();
                        break;

                    case "e":
                        hr();
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
                        Response.Redirect("~/Company_Home.aspx");
                        break;
                }
            }
            else
            {
                Response.Cookies["Msg_Session"].Value = "Create Company";
                Response.Redirect("~/Company_Home.aspx");
            }

        }
    }

    public void hr()
    {
        ddl_Branch.Visible = false;
       Buttonswitch.Text = "Switch to Finished Tasks";
        grd_view.Visible = true;
        grd_view1.Visible = false;
      
        Employee_load();
    }

    public void ddl_Branch_load()
    {
        int ddl_i;
        //branck dropdown
        ddlBranchsList = company.fn_getBranchs();

        if (ddlBranchsList.Count > 0)
        {
            for (ddl_i = -1; ddl_i < ddlBranchsList.Count; ddl_i++)
            {

                if (ddl_i == -1)
                {
                    ListItem list = new ListItem();

                    list.Text = "Select Branch";
                    list.Value = "0";
                    ddl_Branch.Items.Add(list);
                }
                else
                {
                    ListItem list = new ListItem();

                    list.Text = ddlBranchsList[ddl_i].CompanyName;
                    list.Value = ddlBranchsList[ddl_i].CompanyId.ToString();
                    ddl_Branch.Items.Add(list);
                }
            }
        }
    }

    public void Employee_load()
    {
       
        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM task_schedule where Status!='Closed' and pn_employeeID='" + employee.EmployeeId + "' and pn_BranchID = '" + employee.BranchId + "' and pn_Companyid='" + employee.CompanyId + "' ", myConnection);

        DataSet ds = new DataSet();

        ad.Fill(ds, "task_schedule");


        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            grd_view.DataSource = ds;
            grd_view.DataBind();
            int columnCount = grd_view.Rows[0].Cells.Count;
            grd_view.Rows[0].Cells.Clear();
            grd_view.Rows[0].Cells.Add(new TableCell());
            grd_view.Rows[0].Cells[0].ColumnSpan = columnCount;
            grd_view.Rows[0].Cells[0].Text = "No Tasks Found..";
            //GridView1.DataSource = ds;
            //GridView1.DataBind();
            //int columnCount = GridView1.Rows[0].Cells.Count;
            //GridView1.Rows[0].Cells.Clear();
            //GridView1.Rows[0].Cells.Add(new TableCell());
            //GridView1.Rows[0].Cells[0].ColumnSpan = columnCount;
            //GridView1.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {

            grd_view.DataSource = ds;
            grd_view.DataBind();
            //GridView1.DataSource = ds;
            //GridView1.DataBind();

        }

        //if (s_login_role == "a")
        //{
        //    employee.BranchId = (int)ViewState["Task_BranchID"];
        //}
        //EmployeeList = employee.get_Tasksheduledetails(employee);
        //if (EmployeeList.Count > 0)
        //{
        //    grd_view.DataSource = EmployeeList;
        //    grd_view.DataBind();
        //    grd_ddl_load();
        //}
        //else
        //{
        //    grd_view.Visible = false;
        //    lbl_Error.Text = "No Data";
        //}
    }

    public void Completed_tasks()
    {
        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM task_schedule where Status='Closed' and pn_employeeID='" + employee.EmployeeId + "' and pn_BranchID = '" + employee.BranchId + "' and pn_Companyid='" + employee.CompanyId + "' ", myConnection);

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
    }
    protected void ddl_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Branch.SelectedValue != "0")
        {
            ViewState["Task_BranchID"] = Convert.ToInt32(ddl_Branch.SelectedValue);
            grd_view.Visible = true;
            Employee_load();
        }
        else
        {
           grd_view.Visible = false;
        }
    }

    public void grd_ddl_load()
    {
        for (int i = 0; i < grd_view.Rows.Count; i++)
        {
            for (int j = 0; j < EmployeeList.Count; j++)
            {
                string str = ((TextBox)grd_view.Rows[i].FindControl("txt_Empcode")).Text;
                if (Convert.ToString(((TextBox)grd_view.Rows[i].FindControl("txt_Empcode")).Text) == EmployeeList[j].LastName.ToString())
                {
                    ((DropDownList)grd_view.Rows[i].FindControl("ddl_priority")).SelectedValue = EmployeeList[j].Priority.ToString();
                    ((DropDownList)grd_view.Rows[i].FindControl("ddl_status")).SelectedValue = EmployeeList[j].status.ToString();
                }
            }
        }
    }
    protected void grd_view_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd_view.EditIndex = e.NewEditIndex;
        hr();
        grd_view.Rows[e.NewEditIndex].FindControl("statedit").Focus();
        string refr = ((Label)grd_view.Rows[e.NewEditIndex].FindControl("lblStatedit")).Text.ToString();
        DropDownList drpstat = (DropDownList)grd_view.Rows[e.NewEditIndex].FindControl("statedit");
        drpstat.Items.Add("On-process");
        drpstat.Items.Add("Completed");
        //drpstat.SelectedItem.Text = refr;
       // TextBox reedit = (TextBox)grd_view.Rows[e.NewEditIndex].FindControl("remedit");
     
    }
    protected void grd_view_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow Gvrow = grd_view.Rows[e.RowIndex];
        //Label8.Text = Gvrow.ToString();
        if (Gvrow != null)
        {
            string sdate = "1990/01/01";
            string tid = ((Label)Gvrow.FindControl("lbltaskid")).Text;
            string subedit = ((Label)Gvrow.FindControl("Tsubedit")).Text;
            string Remedit = ((TextBox)Gvrow.FindControl("Remedit")).Text;
            string stat = ((DropDownList)Gvrow.FindControl("statedit")).Text;
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
               // cmd1 = new SqlCommand("insert into task_history (task_id,status,RDOC,remarks,comment) values ('" + tid + "' ,'" + stat + "','" + Remedit + "','" + rdoc + "','" + cmnt + "')", myConnection);
              
            }
            else
            {
                string rdoc = DateTime.Now.ToString();
               string cmnt = "-";
               cmd = new SqlCommand("set dateformat dmy;update task_schedule set TaskTitle='" + subedit + "',Status='" + stat + "',Remarks='" + Remedit + "',RDOC='" + rdoc + "' where TaskTitle='" + subedit + "'", myConnection);
               cmd1 = new SqlCommand("set dateformat dmy;insert into task_history (task_id,status,RDOC,remarks,comment) values ('" + tid + "' ,'" + stat + "','" + rdoc + "','" + Remedit + "','" + cmnt + "');set dateformat mdy", myConnection);
                cmd1.ExecuteNonQuery();
            }
           // myConnection.Open();
            //cmd = new SqlCommand("update task_schedule set Status='"+stat+"', Remarks='" + Remedit + "', Submitted_date='"+sdate+"' where TSubject='" + subedit + "' and pn_EmployeeID='"+employee.EmployeeId+"'", myConnection);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Task Updated Successfully!');", true);
            cmd.ExecuteNonQuery();
            myConnection.Close();
            grd_view.EditIndex = -1; // turn to edit mode
            hr();
        }
            
    }
    protected void grd_view_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd_view.EditIndex = -1;
        hr();
    }
    protected void grd_view_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex == grd_view.EditIndex)
        {
          
            //if (s_login_role == "e")
            //{
            //   // ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('works!');", true);
            //    DropDownList drpstat = (DropDownList)e.Row.FindControl("statedit");
            //    drpstat.Items.Add("Select");
            //    drpstat.Items.Add("On-process");
            //    drpstat.Items.Add("Completed");

            //}
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
       
    }
    protected void Buttoncomplete_Click(object sender, EventArgs e)
    {
        
        
    }
    
    protected void Buttonswitch_Click(object sender, EventArgs e)
    {
        if (Buttonswitch.Text == "Switch to Finished Tasks")
        {
            Completed_tasks();
            Buttonswitch.Text = "Switch to Assigned Tasks";
            grd_view.Visible = false;
            grd_view1.Visible = true;
           
        }
        else
        {
            hr();
        }
    }
}
