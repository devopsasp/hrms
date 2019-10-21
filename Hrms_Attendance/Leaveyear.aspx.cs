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
    Collection<Leave> LeaveGridList;
    Collection<Leave> LeaveMasterList;
   
    Collection<Company> CompanyList;
    Collection<Employee> EmployeeList;
    Collection<Company> ddlBranchsList;

    string dt; 
    string _Code;
    string s_login_role, ec, cf;
    int i, j, max = 0, cur_yr, ddl_i, temp_check = 0, s = 0, n = 0;
    bool avail = false,leave_check=false;
    string s_form = "", str_date = "", _Value="",query="";
    double temp_count = 0;
    DataSet ds_userrights,ds_leavecount;
    string sdate = "", edate = "", ann_leave = "";
    string[] sd, ed;
    //DateTime dtime = DateTime.Now.ToString("dd/MM/yyyy", null);

    protected void Page_Load(object sender, EventArgs e)
    {
        
        lbl_Error.Text = "";

        
        lbl_Error.Text = "";

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

                        ddl_Branch_load();
                        break;

                    case "h": ddl_Branch.Visible = false;
                        load();
                        break;

                    case "e": ddl_Branch.Visible = false;
                        break;

                    case "u": s_form = "43";

                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            ddl_Branch.Visible = false;
                        }
                        else
                        {
                            ddl_Branch.Visible = false;
                            Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator.";
                            Response.Redirect("../Hrms_Attendance/Attendance_Home.aspx");
                        }

                        break;

                    default: Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
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

    public void load()
    {
        try
        {
            myConnection.Open();
            cmd = new SqlCommand("select start_date , end_date from paym_branch where pn_companyid = '" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
            rea = cmd.ExecuteReader();
            if (rea.Read())
            {
                txt_fromDate.Text = Convert.ToDateTime(rea[0]).ToString("dd/MM/yyyy");
                txt_ToDate.Text = Convert.ToDateTime(rea[1]).ToString("dd/MM/yyyy");
            }
            myConnection.Close();
            leave_list();
        }

        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }


    public void leave_list()
    {
        myConnection.Open();
        cmd = new SqlCommand("select v_leavename from paym_leave where pn_companyid = '" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "' and annual_leave = 'Carry Forward'", myConnection);
        rea = cmd.ExecuteReader();
        while (rea.Read())
        {
            lb_Leave.Items.Add(rea[0].ToString());
        }
        myConnection.Close();
    }

    public void ddl_Branch_load()
    {
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
  

    protected void ddl_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddl_Branch.SelectedValue != "0")
            {
                employee.BranchId = Convert.ToInt32(ddl_Branch.SelectedItem.Value);
                l.BranchID = Convert.ToInt32(ddl_Branch.SelectedItem.Value);
                load();
            }

            else
            {

                lbl_Error.Text = "Select Branch";
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }




    public void clear()
    {
        //txt_leavename.Value = "";
        //txt_LeaveCode.Value = "";
        //txt_count.Value = "";
    }


    public void Get_year()
    {
        try
        {
            myConnection.Open();
            cmd = new SqlCommand("Select * from paym_branch where pn_companyid = '" + employee.CompanyId + "' and pn_branchId = '" + employee.BranchId + "'", myConnection);
            rea = cmd.ExecuteReader();
            if (rea.Read())
            {
                sdate = Convert.ToDateTime(rea["start_date"]).ToString("dd/MM/yyyy");
                edate = Convert.ToDateTime(rea["end_date"]).ToString("dd/MM/yyyy");
            }
            sd = sdate.Split('/');
            s = Convert.ToInt32(sd[2]);
            sdate = sd[1] + "/" + sd[0] + "/" + s.ToString();
            ed = edate.Split('/');
            n = Convert.ToInt32(ed[2]);
            edate = ed[1] + "/" + ed[0] + "/" + n.ToString();
            
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error. Please check the branch details.";
        }              
        finally
        {
            myConnection.Close();
        }
    }

    public void Branch_update()
    {
        try
        {
            Get_year();
            myConnection.Open();
            sdate = sd[1] + "/" + sd[0] + "/" + (s + 1).ToString();
            edate = ed[1] + "/" + ed[0] + "/" + (n + 1).ToString();
            cmd = new SqlCommand("update paym_branch set start_date='" + sdate + "' , end_date = '" + edate + "' where pn_companyid = '" + employee.CompanyId + "' and pn_branchId = '" + employee.BranchId + "'", myConnection);
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            lbl_Error.Text = " Error Occured";
        }
        finally
        {
            myConnection.Close();
        }
    }

    public void YearEnd()
    {
        try
        {
            //Get_year();
            myConnection.Open();
            cmd = new SqlCommand("insert into yearend values ('" + employee.CompanyId + "', '" + employee.BranchId + "', '" + txt_fromDate.Text + "','" + txt_ToDate.Text + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "')", myConnection);
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            lbl_Error.Text = " Error Occured";
        }
        finally
        {
            myConnection.Close();
        }
    }

    public void Leave_settlement()
    {
        try
        {
            Get_year();
            l.To_year = n;
            myConnection.Open();
            cmd = new SqlCommand("Select pn_employeeid from paym_employee where pn_CompanyID = '" + l.CompanyID + "' and pn_BranchID = '" + l.BranchID + "'", myConnection);
            SqlDataReader red = cmd.ExecuteReader();
            while (red.Read())
            {
                l.EmployeeID = Convert.ToInt32(red[0]);

                cmd = new SqlCommand("Select * from paym_leave where pn_CompanyID = '" + l.CompanyID + "' and pn_BranchID = '" + l.BranchID + "'", myConnection);
                SqlDataReader rede = cmd.ExecuteReader();
                while (rede.Read())
                {
                    l.leaveID = Convert.ToInt32(rede["pn_LeaveID"]);
                    l.leaveCode = rede["pn_leavecode"].ToString();
                    l.LeaveBY = rede["v_LeaveName"].ToString();
                    max = Convert.ToInt32(rede["max_days"]);
                    ann_leave = rede["annual_leave"].ToString();
                    //int s += l.LeaveBY + ",";

                    if (ann_leave == "Encashment")
                    {
                        ec = "Y";
                        cf = "N";
                    }
                    else if (ann_leave == "Carry Forward")
                    {
                        ec = "N";
                        cf = "Y";
                    }
                    else
                    {
                        ec = "";
                        cf = "";
                    }
                    LeaveList = l.Check_leaveAllocation(l);
                    double taken_days = 0.0, allow_days = 0.0, bal_days = 0.0;
                    if (LeaveList.Count > 0)
                    {
                        allow_days = LeaveList[0].Count;
                        LeaveGridList = l.fn_leave_PerYear(l);
                        if (LeaveGridList.Count > 0)
                        {
                            for (i = 0; i < LeaveGridList.Count; i++)
                            {
                                taken_days = taken_days + LeaveGridList[i].Cur_Leave;
                            }
                            bal_days = allow_days - taken_days;
                        }

                        cmd = new SqlCommand("insert into leave_settlement values('" + l.CompanyID + "','" + l.BranchID + "','" + l.EmployeeID + "','" + l.leaveID + "','" + l.leaveCode + "','" + allow_days + "','" + taken_days + "','" + bal_days + "','" + ec + "','" + cf + "','" + max + "','N','" + n + "')", myConnection);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        lbl_Error.Text = "Error Occured";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error Occured";
        }
        finally
        {
            myConnection.Close();
        }

    }


    public void leave_allocation_update()
    {
        try
        {
            Get_year();
            l.To_year = n;
            s = n + 1;
            double actDays = 0.0, taken_days = 0.0, bal_days = 0.0, allow_days = 0.0, tot_days = 0.0;
            myConnection.Open();
            cmd = new SqlCommand("Select count(*) from paym_leave where pn_CompanyID = '" + l.CompanyID + "' and pn_BranchID = '" + l.BranchID + "'", myConnection);
            int scount = Convert.ToInt32(cmd.ExecuteScalar());
            cmd = new SqlCommand("Select pn_employeeid from paym_employee where pn_CompanyID = '" + l.CompanyID + "' and pn_BranchID = '" + l.BranchID + "'", myConnection);
            SqlDataReader red = cmd.ExecuteReader();
            while (red.Read())
            {
                l.EmployeeID = Convert.ToInt32(red[0]);
                cmd = new SqlCommand("Select * from paym_leave where pn_CompanyID = '" + l.CompanyID + "' and pn_BranchID = '" + l.BranchID + "'", myConnection);
                SqlDataReader rede = cmd.ExecuteReader();
                while (rede.Read())
                {
                    max = 0;
                    l.leaveID = Convert.ToInt32(rede["pn_LeaveID"]);
                    l.leaveCode = rede["pn_leavecode"].ToString();
                    l.LeaveBY = rede["v_LeaveName"].ToString();
                    max = Convert.ToInt32(rede["max_days"]);
                    ann_leave = rede["annual_leave"].ToString();
                    if (ann_leave != "")
                    {
                        LeaveList = l.Get_leaveAllocation(l);
                        if (LeaveList.Count > 0)
                        {
                            actDays = LeaveList[0].Count1;
                            allow_days = LeaveList[0].Count2;
                        }
                        LeaveGridList = l.fn_leave_PerYear(l);
                        if (LeaveGridList.Count > 0)
                        {
                            for (i = 0; i < LeaveGridList.Count; i++)
                            {
                                taken_days = taken_days + LeaveGridList[i].Cur_Leave;
                            }
                            bal_days = allow_days - taken_days;
                        }
                        if (max == 0)
                        {
                            tot_days = actDays;
                            bal_days = 0.0;
                        }
                        else if (bal_days < max)
                        {
                            tot_days = (actDays + bal_days);
                        }
                        else if (bal_days > max)
                        {
                            tot_days = (actDays + max);
                            bal_days = max;
                        }
                        else if (bal_days == max)
                        {
                            tot_days = (actDays + max);
                        }

                        if (ann_leave == "Encashment")
                        {
                            Encashment(actDays, allow_days, taken_days, bal_days, max);
                        }
                        else if (ann_leave == "Carry Forward")
                        {
                            CarryForward(actDays, allow_days, taken_days, bal_days, tot_days, max);
                        }
                        else
                        {
                            Allocation(actDays, allow_days, taken_days, bal_days, tot_days, max);
                        }
                    }
                }                               
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error Occured";
        }
        finally
        {
            myConnection.Close();
        }
    }

    public void Allocation(double actdays, double allowdays, double takendays, double baldays, double totdays, double max)
    {
        try
        {
            Get_year();
            l.To_year = n;
            s = n + 1;
            myConnection.Open();
            string date1 = DateTime.Now.ToString("MM/dd/yyyy", null);
            cmd = new SqlCommand("insert into paym_leaveAllocation1 values('" + l.CompanyID + "','" + l.BranchID + "','" + l.leaveID + "','" + l.EmployeeID + "','" + actdays + "','" + actdays + "','Individual','" + s + "')", myConnection);
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error Occured";
        }
        finally
        {
            myConnection.Close();
        }
    }


    public void CarryForward(double actdays, double allowdays, double takendays, double baldays, double totdays, double max)
    {
        try
        {
            Get_year();
            l.To_year = n;
            s = n + 1;
            myConnection.Open();
            string date1 = DateTime.Now.ToString("MM/dd/yyyy", null);
            cmd = new SqlCommand("insert into paym_CarryForward values('" + l.CompanyID + "','" + l.BranchID + "','" + l.EmployeeID + "','" + l.leaveID + "','" + allowdays + "','" + takendays + "','" + max + "','" + baldays + "','" + date1 + "','" + l.To_year + "')", myConnection);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("insert into paym_leaveAllocation1 values('" + l.CompanyID + "','" + l.BranchID + "','" + l.leaveID + "','" + l.EmployeeID + "','" + actdays + "','" + totdays + "','Individual','" + s + "')", myConnection);
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error Occured";
        }
        finally
        {
            myConnection.Close();
        }
    }


    public void Encashment(double actdays, double allowdays, double takendays, double baldays, double max)
    {
        try
        {
            Get_year();
            l.To_year = n;
            s = n + 1;
            myConnection.Open();
            double basic = 0.0; double netAmt = 0.0;
            cmd = new SqlCommand("select basic_salary from paym_employee where pn_employeeid='" + l.EmployeeID + "' and pn_CompanyID = '" + l.CompanyID + "' and pn_BranchID = '" + l.BranchID + "'", myConnection);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                basic = (double)dr["basic_salary"];
            }
            basic = (basic * 12) / 280;
            netAmt = max * basic;
            string date1 = DateTime.Now.ToString("MM/dd/yyyy", null);
            cmd = new SqlCommand("insert into paym_EncashmentDetails values('" + l.CompanyID + "','" + l.BranchID + "','" + l.EmployeeID + "','" + l.leaveID + "','" + allowdays + "','" + takendays + "','" + max + "','" + baldays + "','" + basic + "','" + netAmt + "','" + date1 + "','" + l.To_year + "')", myConnection);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("insert into paym_leaveAllocation1 values('" + l.CompanyID + "','" + l.BranchID + "','" + l.leaveID + "','" + l.EmployeeID + "','" + actdays + "','" + actdays + "','Individual','" + s + "')", myConnection);
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error Occured";
        }
        finally
        {
            myConnection.Close();
        }
    }


    public void Encashment_Details()
    {
        try
        {
            
            string encash = "0";
            Get_year();
            l.To_year = n;
            s = n + 1;
            myConnection.Open();
            cmd = new SqlCommand("Select count(*) from paym_leave where pn_CompanyID = '" + l.CompanyID + "' and pn_BranchID = '" + l.BranchID + "'", myConnection);
            int scount = Convert.ToInt32(cmd.ExecuteScalar());
            cmd = new SqlCommand("Select pn_employeeid from paym_employee where pn_CompanyID = '" + l.CompanyID + "' and pn_BranchID = '" + l.BranchID + "' and Salary_Type='Month'", myConnection);
            SqlDataReader red = cmd.ExecuteReader();
            while (red.Read())
            {
                l.EmployeeID = Convert.ToInt32(red[0]);

                cmd = new SqlCommand("Select * from paym_leave where pn_CompanyID = '" + l.CompanyID + "' and pn_BranchID = '" + l.BranchID + "' and annual_leave='Encashment'", myConnection);
                SqlDataReader rede = cmd.ExecuteReader();
                while (rede.Read())
                {
                    l.leaveID = Convert.ToInt32(rede["pn_LeaveID"]);
                    l.leaveCode = rede["pn_leavecode"].ToString();
                    l.LeaveBY = rede["v_LeaveName"].ToString();
                    max = 0;
                    max = Convert.ToInt32(rede["max_days"]);
                    //max = 5;
                    ann_leave = rede["annual_leave"].ToString();
                    double taken_days = 0.0, allow_days = 0.0, bal_days = 0.0, tot_days = 0.0;
                    if (ann_leave == "Encashment")
                    {
                        LeaveList = l.Check_leaveAllocation1(l);
                        if (LeaveList.Count > 0)
                        {
                            allow_days = LeaveList[0].Count;
                        }
                        else
                        {
                            lbl_Error.Text = "Error Occured";
                        }
                        LeaveGridList = l.fn_leave_PerYear1(l);
                        if (LeaveGridList.Count > 0)
                        {
                            for (i = 0; i < LeaveGridList.Count; i++)
                            {
                                taken_days = taken_days + LeaveGridList[i].Cur_Leave;
                            }
                            bal_days = allow_days - taken_days;
                        }
                    }
                    string dtt;
                    cmd = new SqlCommand("select max(d_date) as ddate from PayOutput_NetPay where pn_employeeid='" + l.EmployeeID + "' and pn_CompanyID = '" + l.CompanyID + "' and pn_BranchID = '" + l.BranchID + "'", myConnection);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        dt = Convert.ToDateTime(dr["ddate"]).ToString("yyyy/MM/dd");
                    }
                    
                    double basic = 0.0; double net_Pay = 0.0;
                    cmd = new SqlCommand("select act_basic from PayOutput_NetPay where pn_employeeid='" + l.EmployeeID + "' and d_Date='" + dt + "' and pn_CompanyID = '" + l.CompanyID + "' and pn_BranchID = '" + l.BranchID + "'", myConnection);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        basic = (double)dr["act_basic"];
                    }
                    cmd = new SqlCommand("select pn_DepartmentId from paym_employee_profile1 where pn_employeeid='" + l.EmployeeID + "' and pn_CompanyID = '" + l.CompanyID + "' and pn_BranchID = '" + l.BranchID + "'", myConnection);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        employee.DepartmentId = (int)dr["pn_DepartmentId"];
                    }
                    net_Pay = (basic * 12) / 280;
                    
                    if (bal_days < max)
                    {
                        encash = (net_Pay * bal_days).ToString("0.00");
                    }
                    else if (bal_days > max)
                    {
                        encash = (net_Pay * max).ToString("0.00");
                    }
                    else if (bal_days == max)
                    {
                        encash = (net_Pay * max).ToString("0.00");
                    }
                    else if (max == 0)
                    {
                        encash = "0";
                    }
                    string date1 = DateTime.Now.ToString("MM/dd/yyyy", null);
                    cmd = new SqlCommand("insert into paym_EncashmentDetails values('" + l.CompanyID + "','" + l.BranchID + "','" + l.EmployeeID + "','" + employee.DepartmentId + "','" + l.leaveID + "','" + allow_days + "','" + taken_days + "','" + max + "','" + bal_days + "','" + net_Pay + "','" + encash + "','" + date1 + "','" + l.To_year + "')", myConnection);
                    int c = cmd.ExecuteNonQuery();             
                }
                //cmd = new SqlCommand("select Count(*) from LeaveAllocation_Master where pn_companyID='" + l.CompanyID + "' and pn_BranchID='" + l.BranchID + "' and pn_EmployeeID='" + l.EmployeeID + "' and yearend='" + s + "'", myConnection);
                //int count1 = Convert.ToInt32(cmd.ExecuteScalar());
                //if (count1 == 0)
                //{

                //    cmd = new SqlCommand("insert into leaveAllocation_Master values('" + l.CompanyID + "','" + l.BranchID + "','" + l.EmployeeID + "','" + s + "'," + count + ")", myConnection);
                //    int c = cmd.ExecuteNonQuery();
                //}

                //cmd = new SqlCommand("update paym_leaveallocation1 set yearend = '" + s + "' where pn_CompanyID = '" + l.CompanyID + "' and pn_BranchID = '" + l.BranchID + "' and yearend = '" + n + "'", myConnection);
                //cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error Occured";
        }
        finally
        {
            myConnection.Close();
        }
    }

    protected void Btn_process_Click(object sender, EventArgs e)
    {
        //Leave_settlement();
        leave_allocation_update();
        //Encashment_Details();
        Branch_update();

        lbl_Error.Text = "Leave Year End process is done successfully";
    }
    protected void Img_Clear_Click(object sender, EventArgs e)
    {

    }
}
