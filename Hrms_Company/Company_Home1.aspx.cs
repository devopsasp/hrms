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
using ePayHrms.Employee;



public partial class Hrms_Company_Default : System.Web.UI.Page
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
    private SqlConnection _Connection;
    ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();
    Collection<Company> CompanyList;
    Company company = new Company();
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    Candidate c = new Candidate();
    Collection<Employee> EmpFirstList;
    DropDownList ddl = new DropDownList();
    string[] rdate;
    string msg, Ename;
    string s_login_role;
    int ddl_i, grk, dept_id, emp_id;
    string _path, _Value;
    string s_form = "";
    DataSet ds_userrights;
    string str = "";
    string lbl;
    string code;
    string id;
    protected void Page_Load(object sender, EventArgs e)
    {
        
       
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        c.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        s_login_role = Request.Cookies["Login_temp_Role"].Value;

        //mei
        SqlCommand com_new;
        if (!Page.IsPostBack)
        {
            myConnection.Open();
            com_new = new SqlCommand("select [v_departmentName] from paym_department where pn_branchid='" + c.BranchID + "'", myConnection);
            SqlDataReader rdr_new = com_new.ExecuteReader();

            while (rdr_new.Read())
            {
                //ddl_dept.Items.Add(rdr_new[0].ToString());
            }
            myConnection.Close();
            rdr_new.Close();
        }


        if (s_login_role == "a")
        {

            //lblmsg.Text = (string)Session["Login_Name"];
            //img_photo.ImageUrl = (string)Session["Login_temp_Photo"];

        }

        else if (s_login_role == "h")
        {
            this.SiteMapDataSource1.Provider = SiteMap.Providers["Menu3Sitemap"];
            //lblmsg.Text = (string)Session["Login_Name"];
            //img_photo.ImageUrl = (string)Session["Login_temp_Photo"];
            hr();

        }


        else if (s_login_role == "e")
        {
            this.SiteMapDataSource1.Provider = SiteMap.Providers["Menu2Sitemap"];
            employee_login();
            //lblmsg.Text = (string)Session["Login_Name"];
            //img_photo.ImageUrl = (string)Session["Login_temp_Photo"];
        }
        else if (s_login_role == "u")
        {
            //lblmsg.Text = (string)Session["Login_Name"];
            //img_photo.ImageUrl = (string)Session["Login_temp_Photo"];
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        CompanyList = company.fn_getCompany();

        if (CompanyList.Count > 0)
        {
            s_login_role = Request.Cookies["Login_temp_Role"].Value;

            if (s_login_role == "a")
            {
                msg = (string)Session["Msg_session"];
                hr();
                
            }

            else if (s_login_role == "h")
            {
                msg = (string)Session["Msg_session"];
                hr();
                
            }

            else if (s_login_role == "e")
            {
                msg = (string)Session["Msg_session"];
                employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
                //hr();
                employee_login();
                
            }

            else
            {
                msg = (string)Session["Msg_session"];
                employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
                hr();
                
            }
        }
        else
        {
            //lbl_Error.Text = "Create Company";
        }


        if (!IsPostBack)
        {
            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {

                switch (s_login_role)
                {

                    case "a":
                        hr();

                        break;

                    case "h":
                        hr();
                        //**********************************
                        //Reimbrusement 
                        reimbursement();

                        break;

                    case "e":
                        emp();
                        employee_login();

                        break;

                    case "u":

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

        }




    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    public void hr()
    {
        main_panel.Visible = true;
        sub_panel.Visible = false;
        reimbursement_load();
        leave_load();
        Requisitions_load();
        complaints_load();
        annoucements_load();
        
    }
    public void reimbursement()
    {
        int req_count = 0;
        myConnection.Open();
        SqlCommand cmd = new SqlCommand("select * from reimbursement where status='n' and pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "'", myConnection);
        SqlDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            req_count++;
        }
        //lbl_no1.Text = "<div style='font-family:Calibri;'><marquee><a href=../Hrms_Additional/Reimbursement.aspx> You have " + req_count + " reimbursement request from your employee/s</a></marquee></div>";

        myConnection.Close();
    }

    public void emp()
    {
        employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);

        myConnection.Open();
        SqlCommand comm = new SqlCommand("Select * from task_schedule where pn_EmployeeID = '" + employee.EmployeeId + "' and Status='New' and pn_branchID='" + employee.BranchId + "' ", myConnection);
        SqlDataReader read1 = comm.ExecuteReader();
        DropDownList3.Items.Clear();
        while (read1.Read())
        {
           // lbl_no3.Text = "<div style='background-color:#DBCDCC;font-family:Calibri;'><marquee><a href=../Hrms_Additional/Reimbursement.aspx> A task has been assigned for you</a></marquee></div>";
        }

        read1.Close();
        myConnection.Close();
       
    }

    public void admin()
    {

    }
    public void leave_load()
    {
      
        int count;
       
        myConnection.Open();
        SqlCommand cmd1 = new SqlCommand("select count(*) from leave_apply where pn_branchID='" + employee.BranchId + "'and approve=''", myConnection);
        count = Convert.ToInt32(cmd1.ExecuteScalar());
        lbl_total_leave.Text = "total : " + count.ToString();
       Lbl_leave.Text = "" + count + " leave requests pending"; 
            SqlCommand comm = new SqlCommand("Select pn_employeeid,(emp_name+'-'+CONVERT(CHAR(10), from_date, 120)) as leave from leave_apply where pn_branchID='" + employee.BranchId + "'and approve=''", myConnection);
            SqlDataAdapter da = new SqlDataAdapter(comm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            Grid_leave.DataSource = ds;
            Grid_leave.DataBind();
        myConnection.Close();
    }
    public void Requisitions_load()
    {
        int count;
        myConnection.Open();

        SqlCommand cmd1 = new SqlCommand("select count(*) from Recruit_jobrequistion where pn_branchID='" + employee.BranchId + "'and status='Waiting For Approval'", myConnection);
        count = Convert.ToInt32(cmd1.ExecuteScalar());
        Lbl_requisition.Text =  + count + " Job Requisitons Approvals  pending";
        SqlCommand comm = new SqlCommand("Select pn_employeeid,(Job_title+'-'+CONVERT(CHAR(10), Required_date, 120)) as Requisition from Recruit_jobrequistion where pn_branchID='" + employee.BranchId + "'and status='Waiting For Approval'", myConnection);
            Lbl_total_requisitions.Text = "total: " + count.ToString();
            SqlDataAdapter da1 = new SqlDataAdapter(comm);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            Grid_requisition.DataSource = ds1;
            Grid_requisition.DataBind();
            myConnection.Close();

    }
    public void reimbursement_load()
    {
        int count;
        myConnection.Open();
        SqlCommand cmd1 = new SqlCommand("select count(*) from reimbursement where pn_branchID='" + employee.BranchId + "'and status='N'", myConnection);
        count = Convert.ToInt32(cmd1.ExecuteScalar());
        Lbl_reimbursement.Text =  + count + " Reimbursements pending";
        SqlCommand comm = new SqlCommand("Select (pn_employeename +' - '+ expense) as reimbursement  from reimbursement where pn_branchID='" + employee.BranchId + "'and status='N'", myConnection);
           Lbl_totatl_reimbursement.Text = "total: " + count.ToString();
            SqlDataAdapter da2 = new SqlDataAdapter(comm);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);
            Grid_reimbursement.DataSource = ds2;
            Grid_reimbursement.DataBind();
        myConnection.Close();
    }

    protected void Grd_Leave_SelectedIndexChanged(object sender, EventArgs e)
    {
      
    }
    
    protected void Grid_leave_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
            e.Row.CssClass = "row";
    }


    protected void Grid_leave_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
   
    protected void Grid_leave_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        

            Response.Redirect("../Hrms_Attendance/LeaveDetails.aspx");
        
    }
    protected void Grid_requisition_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
            e.Row.CssClass = "row";
    }
    protected void Grid_requisition_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Response.Redirect("../Hrms_Recruitment/requisition_approval.aspx");
    }
    protected void Grid_requisition_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void Grid_reimbursement_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
            e.Row.CssClass = "row";
    }
    protected void Grid_reimbursement_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Response.Redirect("../Hrms_Additional/Reimbursement.aspx");
    }
    protected void Grid_reimbursement_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    public void complaints_load()
    {
        int count;
        myConnection.Open();
        SqlCommand cmd1 = new SqlCommand("select count(*) from Compliant_Box where pn_branchID='" + employee.BranchId + "'and status='waiting'", myConnection);
        count = Convert.ToInt32(cmd1.ExecuteScalar());
        Lbl_complaints.Text = + count + " Complaints pending";
        SqlCommand comm = new SqlCommand("select a.Employee_first_name +' - '+ b.compliant_subject as complaints from paym_employee a,Compliant_Box b where a.pn_employeeid=b.pn_employeeid and b.status='Waiting' and b.pn_branchID='" + employee.BranchId + "'", myConnection);
        Lbl_total_complaints.Text = "total: " + count.ToString();
        SqlDataAdapter da3 = new SqlDataAdapter(comm);
        DataSet ds3 = new DataSet();
        da3.Fill(ds3);
        Grid_complaints.DataSource = ds3;
        Grid_complaints.DataBind();
        myConnection.Close();
    }
    protected void Grid_complaints_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
            e.Row.CssClass = "row";
    }
    protected void Grid_complaints_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Response.Redirect("../Hrms_Operations/hr_compliant_box.aspx");
    }
    protected void Grid_complaints_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void Img_btn_publish_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            myConnection.Open();
            employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            employee.Announcement_id1 = 0;
            string date3 = DateTime.Now.ToString("dd/MM/yyyy");
            employee.Date = Convert.ToDateTime(date3);
            employee.Subject1 = txt_subject.Text;
            employee.Announcement1 = Txt_details.Text;
            _Value = employee._Announcements(employee);

            if (_Value != "1")
            {
                lbl_Error.Text = "<font color=Blue>Added Successfully</font>";
                
            }
            else
            {
                lbl_Error.Text = "<font color=Red>Error Occured</font>";
                
            }
            myConnection.Close();
            annoucements_load();
            clear();
            

        }
        catch (Exception ex)
        {
            
        }
    }
    public void annoucements_load()
    {
        Img_btn_publish.Visible = true;
        Grid_announcements.Visible = true;
        Img_btn_update.Visible = false;
        //img_btn_cancel.Visible = false;
        string Today_date1 = DateTime.Now.ToString("dd/MM/yyyy");
        DateTime Today_Date = Convert.ToDateTime(Today_date1);
        myConnection.Open();
        SqlCommand comm = new SqlCommand("set dateformat dmy;select (CONVERT(CHAR(10),announcementid,120)+'-'+subject) as Announcements,announcementid  from announcements where date='" + Today_Date + "' and pn_branchID='" + employee.BranchId + "';set dateformat mdy", myConnection);
        SqlDataAdapter da4 = new SqlDataAdapter(comm);
        DataSet ds4 = new DataSet();
        da4.Fill(ds4);
       Grid_announcements.DataSource = ds4;
       Grid_announcements.DataBind();
        myConnection.Close();
       
        //txt_subject.Text = "";
        //Txt_details.Text = "";
        //txt_id.Text = "";
    }
    protected void Grid_announcements_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
            e.Row.CssClass = "row";
    }
    protected void Grid_announcements_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "cmd")
        {
            lbl_Error.Text = "";
            int rowindex = int.Parse(e.CommandArgument.ToString());
            code = ((Label)Grid_announcements.Rows[rowindex].FindControl("lblid1")).Text;
            string[] id1 = code.Split('-');
            id = id1[0];
            myConnection.Open();
            SqlCommand comm = new SqlCommand("select subject,Details from announcements where announcementid='" + id + "' and pn_BranchID='" + employee.BranchId + "'", myConnection);
            SqlDataReader rdr = comm.ExecuteReader();
            while (rdr.Read())
            {
                txt_subject.Text = rdr["subject"].ToString();
                Txt_details.Text = rdr["Details"].ToString();
                txt_id.Text = id;
            }
            rdr.Close();
            myConnection.Close();
            Img_btn_publish.Visible = false;
            Grid_announcements.Visible = false;
            Img_btn_update.Visible = true;
            img_btn_cancel.Visible = true;
           
        }
    }
    protected void Img_btn_update_Click(object sender, ImageClickEventArgs e)
    {
       try
       {
           //Label id = ((Label)Grid_announcements.FindControl("lblid"));
           myConnection.Open();
           SqlCommand cmd=new SqlCommand("update announcements set subject='"+txt_subject.Text+"',details='"+Txt_details.Text+"' where announcementid='"+txt_id.Text+"'",myConnection);
           cmd.ExecuteNonQuery();
           myConnection.Close();
           lbl_Error.Text = "<font color=Blue>Updated Successfully</font>";
           annoucements_load();
           clear();
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "<font color=Red>Error Occured While updating</font>";
        }
    }
    protected void img_btn_cancel_Click(object sender, ImageClickEventArgs e)
    {
        lbl_Error.Text = "";
        annoucements_load();
        clear();
    }
    public void clear()
    {
        txt_subject.Text = "";
        txt_id.Text = "";
        Txt_details.Text = "";
    }
    public void employee_login()
    {
        main_panel.Visible = false;
        sub_panel.Visible = true;
        lbl_employee_announcement.Visible=false;
        employeee_announcements_load();
      
        
    }
    public void employeee_announcements_load()
    {
        try
        {
            string Today_date1 = DateTime.Now.ToString("dd/MM/yyyy");
            DateTime Today_Date = Convert.ToDateTime(Today_date1);
            myConnection.Open();
            SqlCommand comm = new SqlCommand("set dateformat dmy;select (CONVERT(CHAR(10),announcementid,120)+'-'+subject) as Announcements1,announcementid  from announcements where date='" + Today_Date + "' and pn_branchID='" + employee.BranchId + "';set dateformat mdy", myConnection);
            SqlDataAdapter da5 = new SqlDataAdapter(comm);
            DataSet ds5 = new DataSet();
            da5.Fill(ds5);
            Grid_annoucment1.DataSource = ds5;
            Grid_annoucment1.DataBind();
            myConnection.Close();
        }
        catch (Exception ex)
        {

        }
    }
    protected void Grid_annoucment1_RowCreated(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
            e.Row.CssClass = "row";
    }
    protected void Grid_annoucment1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "cmd")
        {
            Grid_annoucment1.Visible = false;
            int rowindex = int.Parse(e.CommandArgument.ToString());
            string code1 = ((Label)Grid_announcements.Rows[rowindex].FindControl("lblid")).Text;
            string[] id1 = code1.Split('-');
          string   id3 = id1[0];
            myConnection.Open();
            SqlCommand comm = new SqlCommand("select subject,Details from announcements where announcementid='" + id3 + "' and pn_BranchID='" + employee.BranchId + "'", myConnection);
            SqlDataReader rdr = comm.ExecuteReader();
            while (rdr.Read())
            {
                //txt_subject.Text = rdr["subject"].ToString();
                lbl_employee_announcement.Text = rdr["Details"].ToString();

            }
            rdr.Close();
            myConnection.Close();
        }
    }
}
