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
using System.Drawing;

public partial class Hrms_Company_Default : System.Web.UI.Page
{

    ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader rea;
    DataSet ds;

    Company company = new Company();
    Employee employee = new Employee();
    Leave l = new Leave();
    PayRoll pay = new PayRoll();

    Collection<Leave> LeaveList;
    Collection<Leave> LeaveMasterList;

    Collection<Company> CompanyList;
    Collection<Employee> EmployeeList;
    Collection<Company> ddlBranchsList;


    string _Code;
    string s_login_role;
    int i, j, cur_yr, ddl_i, temp_check = 0;
    bool avail = false, leave_check = false;
    string s_form = "", str_date = "", _Value = "", query = "";
    double temp_count = 0;
    DataSet ds_userrights, ds_leavecount;
    Collection<Employee> EmployeeTimeList;
    protected void Page_Load(object sender, EventArgs e)
    {
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        l.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        s_login_role = Request.Cookies["Login_temp_Role"].Value;

       
        if (!IsPostBack)
        {
            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {
                switch (s_login_role)
                {

                    case "a":
                        hr_edit();
                        break;

                    case "h":
                        hr_edit();
                        

                        break;
                    case "M":
                        hr_edit();
                        break;

                    case "e":

                        break;

                    case "u":
                        s_form = "42";

                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {

                        }
                        else
                        {

                            Response.Cookies["Msg_Session"].Value = "Permission Restricted. Please Contact Administrator.";
                            Response.Redirect("../Hrms_Attendance/Attendance_Home.aspx");
                        }

                        break;

                    default:
                        Response.Cookies["Msg_Session"].Value = "Permission Restricted. Please Contact Administrator";
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


    string empcode = "",empid="", emp_name = "", leavename = "", leavecode = "", from_date = "", To_date = "", from_status = "", to_status = "", submitted_date = "", approve = "", yearend = "";
    int designationId, leaveId,dayss;

    public int e { get; private set; }

    public void hr_edit()
    {
       
        leave_process(e);
        if (s_login_role =="h")
        {
            SqlDataAdapter adap = new SqlDataAdapter("SELECT * FROM leaveapprove_hr WHERE  pn_CompanyID='" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "'", myConnection);
            //SqlDataAdapter adap = new SqlDataAdapter("select * from leave_apply where pn_BranchID='" + employee.BranchId + "' and ", myConnection);
            DataSet ds1 = new DataSet();
            adap.Fill(ds1, "leave_apply");
            //if (ds1.Tables[0].Rows.Count == 0)
            //{
            //    ds1.Tables[0].Rows.Add(ds1.Tables[0].NewRow());
            //    GridView2.DataSource = ds1;
            //    GridView2.DataBind();
            //    int columnCount = GridView2.Rows[0].Cells.Count;
            //    GridView2.Rows[0].Cells.Clear();
            //    GridView2.Rows[0].Cells.Add(new TableCell());
            //    GridView2.Rows[0].Cells[0].ColumnSpan = columnCount;
            //    GridView2.Rows[0].Cells[0].Text = "No Records Found..";
            //}
            //else
            //{
            GridView1.DataSource = ds1;
            GridView1.DataBind();
            //}



        }
        else
        {
            SqlDataAdapter adap = new SqlDataAdapter("SELECT * FROM leaveapprove_manager WHERE pn_CompanyID='" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "'", myConnection);
            //    //SqlDataAdapter adap = new SqlDataAdapter("select * from leave_apply where pn_BranchID='" + employee.BranchId + "' and ", myConnection);
            DataSet ds1 = new DataSet();
            adap.Fill(ds1, "leave_apply");
            //    //if (ds1.Tables[0].Rows.Count == 0)
            //    //{
            //    //    ds1.Tables[0].Rows.Add(ds1.Tables[0].NewRow());
            //    //    GridView2.DataSource = ds1;
            //    //    GridView2.DataBind();
            //    //    int columnCount = GridView2.Rows[0].Cells.Count;
            //    //    GridView2.Rows[0].Cells.Clear();
            //    //    GridView2.Rows[0].Cells.Add(new TableCell());
            //    //    GridView2.Rows[0].Cells[0].ColumnSpan = columnCount;
            //    //    GridView2.Rows[0].Cells[0].Text = "No Records Found..";
            //    //}
            //    //else
            //    //{
            GridView1.DataSource = ds1;
            GridView1.DataBind();
            //    //}

            //myConnection.Close();
        }
        myConnection.Close();
    }
    public void leave_process(int e)
    {
        query = "delete from leaveapprove_manager;delete from  leaveapprove_hr";
        employee.fn_reportbyid(query);
        EmployeeTimeList = employee.leave_process(employee.BranchId);
        int j = 0, k = 0;
        if (EmployeeTimeList.Count > 0)
        {
            for (int i = 0; i < EmployeeTimeList.Count; i++)
            {
                //int sno = i;
                
                empid = EmployeeTimeList[i].EmployeeId.ToString();
                empcode = EmployeeTimeList[i].EmployeeCode.ToString();
                emp_name = EmployeeTimeList[i].FirstName.ToString();
                leavename = EmployeeTimeList[i].leave_name.ToString();
                leaveId = Convert.ToInt32(EmployeeTimeList[i].leave_id);
                leavecode = EmployeeTimeList[i].leave_Code.ToString();
                from_date = EmployeeTimeList[i].from_date.ToString("yyyy/MM/dd");
                To_date = EmployeeTimeList[i].to_date.ToString("yyyy/MM/dd");
                submitted_date = EmployeeTimeList[i].Submit_date.ToString();
                from_status = EmployeeTimeList[i].from_status.ToString();
                to_status = EmployeeTimeList[i].to_status.ToString();
                approve = EmployeeTimeList[i].approve.ToString();
                yearend = EmployeeTimeList[i].yearend.ToString();
                designationId = Convert.ToInt32(EmployeeTimeList[i].DesignationId);
                dayss = Convert.ToInt32(EmployeeTimeList[i].day);
                //DateTime da = Convert.ToDateTime(submitted_date.ToString());
                string[] arrDate = submitted_date.Split('/');
                //now use array to get specific date object
                string day = arrDate[0].ToString();
                string month = arrDate[1].ToString();
                string year = arrDate[2].ToString();
                DateTime cur_date =DateTime.Now;
                string cu_date = cur_date.ToString();
                string[] curr_date = cu_date.Split('/');
                string cday = arrDate[0].ToString();
                string cmonth = arrDate[1].ToString();
                string cyear = arrDate[2].ToString();
                int dday = Convert.ToInt32(day);
                int cuday = Convert.ToInt32(cday);
                int da =cuday-dday ;
                //int days = Convert.ToInt32(da);
               
                if (da >= 2)
                {
                    if (designationId == 257)
                    {
                        j++;
                        query = "insert into leaveapprove_manager values('" + j + "','" + employee.CompanyId + "','" + employee.BranchId + "','" + empid + "','" + empcode + "','" + emp_name + "','" + leaveId + "','" + leavecode + "','" + leavename + "','" + from_date + "','" + To_date + "','" + submitted_date + "','" + from_status + "','" + to_status + "','" + approve + "','" + yearend + "','" + dayss + "','" + designationId + "')";
                        //query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,whours,status,Department,Leave_name,earlyout,pn_gradeID,To_Date,shift_code,Work_hrs) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[i].EmployeeCode + "', '" + EmployeeTimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + whours + "','" + status + "','" + dept + "','" + leav + "','" + earlyout + "'," + grad + ",'" + enddate + "','" + shift_code + "','" + work_hrs + "')";
                        employee.fn_reportbyid(query);
                    }
                }
                else
                {
                    k++;
                    query = "insert into leaveapprove_hr values('" + k + "','" + employee.CompanyId + "','" + employee.BranchId + "','" + empid + "','" + empcode + "','" + emp_name + "','" + leaveId + "','" + leavecode + "','" + leavename + "','" + from_date + "','" + To_date + "','" + submitted_date + "','" + from_status + "','" + to_status + "','" + approve + "','" + yearend + "','" + dayss + "','" + designationId + "')";
                    //query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,whours,status,Department,Leave_name,earlyout,pn_gradeID,To_Date,shift_code,Work_hrs) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[i].EmployeeCode + "', '" + EmployeeTimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + whours + "','" + status + "','" + dept + "','" + leav + "','" + earlyout + "'," + grad + ",'" + enddate + "','" + shift_code + "','" + work_hrs + "')";
                    employee.fn_reportbyid(query);
                }
                //}
            }
                
            }
        }
    

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        hr_edit();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string sno = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("Labtask1")).Text;
        DeleteRecord(sno);
        hr_edit();
    }
    private void DeleteRecord(string ID)
    {
        string sqlStatement = "DELETE FROM leave_apply WHERE Sno = @Sno";
        try
        {
            myConnection.Open();
            SqlCommand cmd = new SqlCommand(sqlStatement, myConnection);
            cmd.Parameters.AddWithValue("@Sno", ID);
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
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex; // turn to edit mode
        hr_edit();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        GridViewRow Gvrow1 = GridView1.Rows[e.RowIndex];
        if (Gvrow1 != null)
        {
            string appr, perm, reminder, flag;

            string sno = ((Label)Gvrow1.FindControl("Labelsubedit1")).Text;
            string empcode = ((Label)Gvrow1.FindControl("Labelsubedit")).Text;
            string empname = ((Label)Gvrow1.FindControl("Labelassi")).Text;
            string leaveID = ((Label)Gvrow1.FindControl("Labeledit")).Text;
            string from_date = ((Label)Gvrow1.FindControl("Labelleavedate")).Text;
            string To_date = ((Label)Gvrow1.FindControl("Labeltodate")).Text;
            string Sum_date = ((Label)Gvrow1.FindControl("Labelsubm")).Text;
            //string prior = ((Label)Gvrow1.FindControl("lbl_prioredit")).Text;
           // string reason = ((Label)Gvrow1.FindControl("Labelreason")).Text;
            string approve = ((DropDownList)Gvrow1.FindControl("DropDownList1")).Text;
            myConnection.Open();
            cmd = new SqlCommand("update leave_apply set Approve='" + approve + "' ,Flag='Y' where Sno='" + sno + "'", myConnection);
            cmd.ExecuteNonQuery();
            myConnection.Close();

            GridView1.EditIndex = -1; // turn to edit mode
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Leave Status Updated')", true);
            hr_edit();


            //ClientScriptManager manager = Page.ClientScript;

            //manager.RegisterStartupScript(this.GetType(), "Call", "show_message1('Leave Details Updated Sucessfully');", true);


        }
    }
    
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.DataItem != null)
            {
                if (((string)DataBinder.Eval(e.Row.DataItem, "Approve")) == "Pending")
                {
                    // e.Row.BackColor = Color.FromName("#FFFFFF");
                }
            }
        }
        //if (e.Row.DataItem != null)
        //{
        //    try
        //    {
        //        Label rmind = ((Label)e.Row.FindControl("Label13"));
        //        if (rmind.Text == "01/01/1900")
        //        {
        //            rmind.Text = "";
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
    }

}

