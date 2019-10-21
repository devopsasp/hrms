using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
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
using ePayHrms.Leave;

public partial class Hrms_PayRoll_Final_settlement_new : System.Web.UI.Page
{
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    private SqlConnection _connection;
    SqlConnection mycon = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    //private SqlConnection _con;
    //ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlCommand cmd = new SqlCommand();
    Company company = new Company();
    Employee employee = new Employee();
    Leave l = new Leave();
    PayRoll pay = new PayRoll();

    Collection<Leave> LeaveList;
    Collection<Leave> LeaveGridList;
    Collection<Leave> LeaveMasterList;
    Collection<PayRoll> finalsettlementlist;
    Collection<Company> CompanyList;
    Collection<Employee> EmployeeList;
    Collection<Company> ddlBranchsList;

    Be_Recruitment r = new Be_Recruitment();

    Candidate c = new Candidate();


    Collection<Employee> EmployeesList;
    Collection<Employee> emp_ID_List;
    Collection<Employee> emp_available;
    Collection<Employee> EmpFirstList;
    Collection<Employee> EmpGeneralList;

    Collection<PayRoll> emp_edu_List;
    Collection<PayRoll> Empty_gridList;
    Collection<PayRoll> EpsList;
    Collection<PayRoll> epflist;
    string str_query, ann_leave = "", date;
    int ddl_i, max = 0, month, day;
    int i, yr_it, cur_yr, mon, dat, pr_emp;
    string _Value, _value, _data, dt, mn, yr, dob_edit, default_sqldate = "01/01/1900";
    double basic = 0.0; double net_Pay = 0.0,cal_amt, actual_basic,earn_basic, final_amt = 0.0;
    string s_login_role;
    int cal_days,paid_days;
    double grauity, basicpay, da, service, extra_salary, deduct_salary;
    string s_form = "";
    DataSet ds_userrights;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        l.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        lbl_Error.Text = "";
        // Error.Text = "";

        if (!IsPostBack)
        {
            switch (s_login_role)
            {
                case "a": //load();
                    break;

                case "h":
                    // rounds();
                    //gridload();
                    ddl_year_load(ddl_year);
                 
                    load();
                    //load();
                    // access();
                    break;

                case "u": s_form = "52";
                    ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                    if (ds_userrights.Tables[0].Rows.Count > 0)
                    {
                        //load();
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

    public void ddl_department_load()
    {
        ddl_dept.Items.Clear();
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
                    ddl_dept.Items.Add(list);
                }
                else
                {
                    ListItem list = new ListItem();

                    list.Text = EmployeeList[ddl_i].DepartmentName;
                    list.Value = EmployeeList[ddl_i].DepartmentId.ToString();
                    ddl_dept.Items.Add(list);
                }
            }
        }
        else
        {
            lbl_Error.Text = "No Department Available";
        }

    }
    public void ddl_employee_load()
    {
        mycon.Open();
        ddl_employee.Items.Clear();

        if (s_login_role == "a")
        {
            employee.BranchId = (int)ViewState["Appraisal_BranchID"];
        }

        if (s_login_role == "h")
        {
            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        }
       
            //str_query = "Select a.pn_EmployeeID, a.EmployeeCode, a.Employee_First_Name from paym_employee a, paym_employee_profile1 b,paym_Employee_WorkDetails c where a.pn_CompanyID=b.pn_CompanyID and c.pn_employeeid=a.pn_employeeid and c.pn_companyid=b.pn_companyid and c.pn_branchid=b.pn_branchid and a.pn_BranchID=b.pn_BranchID and a.pn_EmployeeID=b.pn_EmployeeID and b.pn_DepartmentID=" + ddl_dept.SelectedValue + " and b.pn_CompanyID=" + employee.CompanyId + " and a.status='y' and datepart(YY,c.offerdate)=" + ddl_year.SelectedItem.Text + " and b.pn_BranchID=" + employee.BranchId;
            str_query = "Select a.pn_EmployeeID, a.EmployeeCode, a.Employee_First_Name from paym_employee a, paym_employee_profile1 b,paym_Employee_WorkDetails c where a.pn_CompanyID=b.pn_CompanyID and c.pn_employeeid=a.pn_employeeid and c.pn_companyid=b.pn_companyid and c.pn_branchid=b.pn_branchid and a.pn_BranchID=b.pn_BranchID and a.pn_EmployeeID=b.pn_EmployeeID and b.pn_DepartmentID=" + ddl_dept.SelectedValue + " and b.pn_CompanyID=" + employee.CompanyId + " and a.status='y' and b.pn_BranchID=" + employee.BranchId;

        EmployeeList = employee.fn_getEmplist(str_query);

        if (EmployeeList.Count > 0)
        {
            for (ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();
                    e_list.Text = "Select";
                    e_list.Value = "0";
                    ddl_employee.Items.Add(e_list);

                }
                else
                {
                    ListItem e_list = new ListItem();
                    e_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
                    e_list.Text = EmployeeList[ddl_i].LastName.ToString();
                    ddl_employee.Items.Add(e_list);
                    

                }
            }
            cmd = new SqlCommand("select a.*,c.Employee_First_Name, c.pn_employeeID from payroll_final_settlement a,Paym_employee c where a.Pn_EmployeeId=c.Pn_EmployeeID  and a.pn_branchID='" + pay.BranchId + "' and a.Pn_CompanyID='" + pay.CompanyId + "'", mycon);
            SqlDataReader dr1 = cmd.ExecuteReader();
            while (dr1.Read())
            {
                string empid = (int)dr1["pn_employeeID"] + "-" + (string)dr1["Employee_First_Name"];
                //string empid=dr1["pn_employeeid"].ToString();
                 for (int j=0; j < ddl_employee.Items.Count; j++)
            {
                if (ddl_employee.Items[j].Text==empid)
                {
                    ddl_employee.Items.RemoveAt(j);
                }
            }
            }
        }
           
        else
        {
            lbl_Error.Text = "No Employee";
        }
         mycon.Close();
    }
    protected void ddl_dept_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_employee_load();
    }
    protected void ddl_employee_SelectedIndexChanged(object sender, EventArgs e)
    {
        double encashmentamt = 0.0;
        try
        {
            string encash = "0";
            mycon.Open();
            string dtt;
            cmd = new SqlCommand("select max(d_date) as ddate from PayOutput_NetPay where pn_employeeid='" + ddl_employee.SelectedValue + "' and pn_CompanyID = '" + l.CompanyID + "' and pn_BranchID = '" + l.BranchID + "'", mycon);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                dt = Convert.ToDateTime(dr["ddate"]).ToString("yyyy/MM/dd");
            }


            cmd = new SqlCommand("select act_basic from PayOutput_NetPay where pn_employeeid='" + ddl_employee.SelectedValue + "' and d_Date='" + dt + "' and pn_CompanyID = '" + l.CompanyID + "' and pn_BranchID = '" + l.BranchID + "'", mycon);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                basic = (double)dr["act_basic"];
                txt_basic_pay.Text = basic.ToString();
            }
            dr.Close();
            cmd = new SqlCommand("select offerdate,joiningdate,RetirementDate from paym_employee_workdetails where pn_employeeid='" + ddl_employee.SelectedValue + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "'", mycon);

            //cmd = new SqlCommand("select max(dates) as lastdate from time_card where pn_employeeid='" + ddl_employee.SelectedValue + "'", mycon);
            SqlDataReader rdr4 = cmd.ExecuteReader();
            if (rdr4.Read())
            {

                // date = rdr4["lastdate"].ToString();
                DateTime last_date = Convert.ToDateTime(rdr4["RetirementDate"]);
                date = last_date.ToString("dd/MM/yyyy");
                DateTime joiningDate = Convert.ToDateTime(rdr4["JoiningDate"]);
                txt_joiningDate.Text = joiningDate.ToString("dd/MM/yyyy");
                txt_date.Text = date;
                if (date == "01/01/1900")
                {
                    txt_date.Text = "";
                }
            }
            rdr4.Close();
            //cmd = new SqlCommand("select joiningdate from paym_employee_workdetails", mycon);
           // cmd = new SqlCommand("select b.joiningDate as JoiningDate,(58-(datediff(YY,a.dateofbirth,b.joiningDate))) as ServiceYear,dateadd(YY,(58-(datediff(YY,a.dateofbirth,b.joiningdate))),b.joiningDate) as RetirementDate from paym_employee a,paym_employee_workdetails b where A.Pn_employeeid=b.Pn_employeeid and a.pn_employeeid='" + ddl_employee.SelectedValue + "'", mycon);
            //SqlDataReader rdr = cmd.ExecuteReader();
           // if (rdr.Read())
            //{
                //DateTime joiningDate = Convert.ToDateTime(rdr["JoiningDate"]);
                //txt_joiningDate.Text = joiningDate.ToString("dd/MM/yyyy");
               // DateTime retirement_date = Convert.ToDateTime(rdr["RetirementDate"]);
                //txt_retirementDate.Text = retirement_date.ToString("dd/MM/yyyy");
                if (rd_final_process.SelectedValue == "")
                {
                    lbl_Error.Text = "Choose any one Settlement Process";
                }
                //else
                //{
                //    cmd = new SqlCommand("select datediff(YY,joiningdate,offerdate) as serviceyear from paym_employee_workdetails a where pn_employeeid='" + ddl_employee.SelectedValue + "'", mycon);
                //        SqlDataReader reader = cmd.ExecuteReader();
                //        if (reader.Read())
                //        {
                //            txt_service.Text = reader["serviceyear"].ToString();
                //        }
                //        reader.Close();
                    
                //    // txt_basic_pay.Text = rdr["BasicPay"].ToString();
                //    // basicpay = Convert.ToDouble(txt_basic_pay.Text);
                //    //da = Convert.ToDouble(txt_da.Text);
                    
                //}
               
            
           // rdr.Close();
            cmd = new SqlCommand("Select Sum(tot_pf+fpf) as pf from payoutput_pf where pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "' and Pn_employeeid='" + ddl_employee.SelectedValue + "'", mycon);
            SqlDataReader rdr1 = cmd.ExecuteReader();
            if (rdr1.Read())
            {
                txt_pf.Text = rdr1["pf"].ToString();
            }
            rdr1.Close();
            cmd = new SqlCommand("select balance_amt from payoutput_loan where pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "' and Pn_employeeid='" + ddl_employee.SelectedValue + "'", mycon);
            SqlDataReader rdr3 = cmd.ExecuteReader();
            if (rdr3.Read())
            {
                txt_loan.Text = rdr3["balance_amt"].ToString();
            }
            else
            {
                txt_loan.Text = "0.0";
            }
            rdr3.Close();
            
           
            string[] year = date.Split('/');
            l.To_year = Convert.ToInt32(year[2]);
            l.EmployeeID = Convert.ToInt32(ddl_employee.SelectedValue);
            month = Convert.ToInt32(year[1]);
            day = Convert.ToInt32(year[0]);
            cmd = new SqlCommand("select calc_days,paid_days,act_basic from payinput where pn_employeeid='" + ddl_employee.SelectedValue + "' and month(d_date)='" + month + "' and year(d_date)='" + l.To_year + "' and pn_branchid='" + l.BranchID + "' and pn_companyid='" + l.CompanyID + "'", mycon);
            SqlDataReader rdr5 = cmd.ExecuteReader();
            if (rdr5.Read())
            {
                cal_days = Convert.ToInt32(rdr5["calc_days"]);
                paid_days = Convert.ToInt32(rdr5["paid_days"]);
                actual_basic = Convert.ToInt32(rdr5["act_basic"]);
                cal_amt = actual_basic / cal_days;
                earn_basic = cal_amt * paid_days;
                extra_salary = earn_basic;
                txt_extra.Text = extra_salary.ToString();
            }
            rdr5.Close();
            cmd = new SqlCommand("Select * from paym_leave where pn_CompanyID = '" + l.CompanyID + "' and pn_BranchID = '" + l.BranchID + "' and annual_leave='Encashment'", mycon);
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
                double taken_days = 0.0, allow_days = 0.0, tot_allow_days = 0.0, bal_days = 0.0, tot_days = 0.0;
                if (ann_leave == "Encashment")
                {
                    LeaveList = l.Check_leaveAllocation1(l);
                    if (LeaveList.Count > 0)
                    {
                        allow_days = LeaveList[0].Count;
                        tot_allow_days = Math.Round((allow_days / 12) * month);


                    }
                    else
                    {
                        //lbl_Error.Text = "Error Occured";
                        txt_encashment.Text = "0.0";
                    }
                    LeaveGridList = l.fn_leave_PerYear1(l);
                    if (LeaveGridList.Count > 0)
                    {
                        for (i = 0; i < LeaveGridList.Count; i++)
                        {
                            taken_days = taken_days + LeaveGridList[i].Cur_Leave;
                        }
                        net_Pay = (basic * 12) / 280;
                        if (taken_days > tot_allow_days)
                        {
                            bal_days = taken_days - tot_allow_days;
                            deduct_salary = bal_days * cal_amt;
                            txt_deduct_amt.Text = deduct_salary.ToString();
                            encash = "0.0";
                            txt_encashment.Text = "0.0";
                        }
                        else if (taken_days < tot_allow_days)
                        {
                            bal_days = tot_allow_days - taken_days;
                            if (taken_days == 0)
                            {
                               if (bal_days < max)
                                {
                                    max = Convert.ToInt32(bal_days);
                                }

                            }
                            else
                            {
                                if (tot_allow_days < max)
                                {
                                    max = Convert.ToInt32(tot_allow_days);
                                }
                            }


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
                                encash = "0.0";
                            }
                            txt_encashment.Text = encash;
                        }
                        else
                        {
                            txt_encashment.Text = "0.0";
                        }

                    }
                    else
                    {
                        txt_encashment.Text = "0.0";
                    }
                }
              

                encashmentamt += Convert.ToDouble(encash);
                txt_encashment.Text = encashmentamt.ToString();
            }


            mycon.Close();
            double grauitytxt, pftxt, encashtxt, loantxt, deducttxt,extra_amount;
            grauitytxt = Convert.ToDouble(txt_grauity.Text);
            pftxt = Convert.ToDouble(txt_pf.Text);
            encashtxt = Convert.ToDouble(txt_encashment.Text);
            loantxt = Convert.ToDouble(txt_loan.Text);
            deducttxt = Convert.ToDouble(txt_deduct_amt.Text);
            extra_amount = Convert.ToDouble(txt_extra.Text);

            if (ckh_deduct.Checked == true)
            {
                txt_deduct_amt.Visible = true;
                final_amt = (grauitytxt + pftxt + encashtxt+extra_amount) - (loantxt + deducttxt);
                txt_final_amt.Text = final_amt.ToString();
            }
            else if (ckh_deduct.Checked == false)
            {
                txt_deduct_amt.Visible = false;
                final_amt = (grauitytxt + pftxt + encashtxt + extra_amount) - (loantxt);
                txt_final_amt.Text = final_amt.ToString();
            }
        }

        catch (Exception ex)
        {
        }
    }

    protected void ckh_deduct_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            double grauitytxt, pftxt, encashtxt, loantxt, deducttxt, extra_amount;
            grauitytxt = Convert.ToDouble(txt_grauity.Text);
            pftxt = Convert.ToDouble(txt_pf.Text);
            encashtxt = Convert.ToDouble(txt_encashment.Text);
            loantxt = Convert.ToDouble(txt_loan.Text);
            deducttxt = Convert.ToDouble(txt_deduct_amt.Text);
            extra_amount = Convert.ToDouble(txt_extra.Text);

            if (ckh_deduct.Checked == true)
            {
                txt_deduct_amt.Visible = true;
                final_amt = (grauitytxt + pftxt + encashtxt + extra_amount) - (loantxt + deducttxt);
                txt_final_amt.Text = final_amt.ToString();
            }
            else if (ckh_deduct.Checked == false)
            {
                txt_deduct_amt.Visible = false;
                final_amt = (grauitytxt + pftxt + encashtxt + extra_amount) - (loantxt);
                txt_final_amt.Text = final_amt.ToString();
            }
        }
        catch (Exception ex)
        {
        }

    }

    protected void rd_final_process_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            mycon.Open();
            if (ddl_employee.SelectedValue == "0")
            {
                lbl_Error.Text = "Select Employee";
            }
            else if (ddl_year.SelectedItem.Text == "Select")
            {
                lbl_Error.Text = "Select Year";
            }
            else
            {
                cmd = new SqlCommand("select max(d_date) as ddate from PayOutput_NetPay where pn_employeeid='" + ddl_employee.SelectedValue + "' and pn_CompanyID = '" + l.CompanyID + "' and pn_BranchID = '" + l.BranchID + "'", mycon);
                SqlDataReader dr1 = cmd.ExecuteReader();
                if (dr1.Read())
                {
                    dt = Convert.ToDateTime(dr1["ddate"]).ToString("yyyy/MM/dd");
                }
                cmd = new SqlCommand("select act_basic from PayOutput_NetPay where pn_employeeid='" + ddl_employee.SelectedValue + "' and d_Date='" + dt + "' and pn_CompanyID = '" + l.CompanyID + "' and pn_BranchID = '" + l.BranchID + "'", mycon);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    basic = (double)dr["act_basic"];
                    txt_basic_pay.Text = basic.ToString();
                }
                dr.Close();
                cmd = new SqlCommand("select offerdate from paym_employee_workdetails where pn_employeeid='" + ddl_employee.SelectedValue + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "'", mycon);

                SqlDataReader rdr4 = cmd.ExecuteReader();
                if (rdr4.Read())
                {

                    DateTime last_date = Convert.ToDateTime(rdr4["offerdate"]);
                    date = last_date.ToString("dd/MM/yyyy");
                    txt_date.Text = date;
                }
                rdr4.Close();
                //if (rd_final_process.SelectedValue == "1" || rd_final_process.SelectedValue == "2")
                //{
                cmd = new SqlCommand("select datediff(YY,joiningdate,offerdate) as serviceyear from paym_employee_workdetails a where pn_employeeid='" + ddl_employee.SelectedValue + "'", mycon);

                //cmd = new SqlCommand("select (58-(datediff(YY,a.dateofbirth,b.joiningDate))) as ServiceYear from paym_employee a,paym_employee_workdetails b where A.Pn_employeeid=b.Pn_employeeid and a.pn_employeeid='" + ddl_employee.SelectedValue + "'", mycon);
                SqlDataReader reader1 = cmd.ExecuteReader();
                if (reader1.Read())
                {
                    txt_service.Text = reader1["ServiceYear"].ToString();
                }
                reader1.Close();
                //}
                //else if (rd_final_process.SelectedValue == "3")
                //{
                //cmd = new SqlCommand("set dateformat dmy;select datediff(YY,joiningdate,'" + date + "') as serviceyear from paym_employee_workdetails a where pn_employeeid='" + ddl_employee.SelectedValue + "';set dateformat mdy", mycon);
                //SqlDataReader reader = cmd.ExecuteReader();
                //if (reader.Read())
                //{
                //    txt_service.Text = reader["serviceyear"].ToString();
                //}
                //reader.Close();
                //}
                // txt_basic_pay.Text = rdr["BasicPay"].ToString();
                // basicpay = Convert.ToDouble(txt_basic_pay.Text);
                // da = Convert.ToDouble(txt_da.Text);
                service = Convert.ToDouble(txt_service.Text);
                grauity = ((basic) * 15 * (service)) / 26;
                txt_grauity.Text = grauity.ToString();
            }
            mycon.Close();
        }
        catch (Exception ex)
        {
        }
    }

    public void load()

    {
        try
        {
            finalsettlementlist = pay.fn_final_settlement(pay);

            if (finalsettlementlist.Count > 0)
            {
                grid_settlement.DataSource = finalsettlementlist;
                grid_settlement.DataBind();
            }
            else
            {
                grid_settlement.DataSource = finalsettlementlist;
                grid_settlement.DataBind();
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    public void ddl_year_load(DropDownList ddl)
    {
        try
        {

            cur_yr = DateTime.Now.Year;

            cur_yr = cur_yr + 1;
            ddl.Items.Clear();
            ddl.Items.Add("Select");
            for (yr_it = cur_yr - 1; yr_it <= cur_yr; yr_it++)
            {
                ddl.Items.Add(Convert.ToString(yr_it));
            }

        }

        catch (Exception ex)
        {
            //lbl_error.Text = "Error";
        }
    }

    protected void grid_settlement_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grid_settlement.EditIndex = e.NewEditIndex;
        load();
    }

    protected void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_department_load();
    }
    protected void grid_settlement_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grid_settlement.EditIndex = -1;
        load();
    }
    protected void grid_settlement_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
          GridViewRow Gvrow = grid_settlement.Rows[e.RowIndex];
          if (Gvrow != null)
          {
              string empid = ((Label)Gvrow.FindControl("lbl_id")).Text;
              string[] id = empid.Split('-');
              pay.EmployeeId = Convert.ToInt32(id[0]);
              pay.Joining_date = ((Label)Gvrow.FindControl("lbl_joining_date")).Text;
              pay.Retire_date = ((Label)Gvrow.FindControl("lbl_retirement_date")).Text;
              pay.Last_working_date = ((Label)Gvrow.FindControl("lbl_last_date")).Text;
              pay.Year = Convert.ToInt32(((Label)Gvrow.FindControl("lbl_years_serviced")).Text);
              pay.Grauity = Convert.ToDecimal(((Label)Gvrow.FindControl("lbl_gruity_amt")).Text);
              pay.Pf_share = Convert.ToDecimal(((Label)Gvrow.FindControl("lbl_pf_amt")).Text);
              pay.Encashment_amt = Convert.ToDecimal(((Label)Gvrow.FindControl("lbl_encash_amt")).Text);
              pay.loan_amt = Convert.ToDecimal(((Label)Gvrow.FindControl("lbl_loab_amt")).Text);
              pay.Deduct_salary_amt = Convert.ToDecimal(((Label)Gvrow.FindControl("lbl_deductsalary_amt")).Text);
              pay.Final_amt = Convert.ToDecimal(((Label)Gvrow.FindControl("lbl_final_amt")).Text);
              pay.Status = ((TextBox)Gvrow.FindControl("txt_status")).Text;
              _Value = pay.finalsettlement(pay);
              if (_Value != "1")
              {
                  lbl_Error.Text = "<font color=Blue>Updated Successfully</font>";
              }
              else
              {
                  lbl_Error.Text = "<font color=Red>Error Occured</font>";
                  load();
              }
              grid_settlement.EditIndex = -1;
              load();
          }
    }
    protected void grid_settlement_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string ID = ((Label)grid_settlement.Rows[e.RowIndex].Cells[0].FindControl("lbl_id")).Text;
        string[] code = ID.Split('-');
        string code1 = code[0].ToString();
        DeleteRecord(code1);
        load();
    }

    private void DeleteRecord(string ID)
    {
        string sqlStatement = "DELETE FROM payroll_final_settlement WHERE pn_employeeid = @pn_employeeid";
        try
        {
            _connection = con.fn_Connection();
            _connection.Open();
            SqlCommand cmd = new SqlCommand(sqlStatement, _connection);
            cmd.Parameters.AddWithValue("@pn_employeeid", ID);
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
            _connection.Close();
        }
    }

    protected void txt_date_TextChanged(object sender, EventArgs e)
    {
        if (txt_date.Text != "")
        {
            mycon.Open();
            cmd = new SqlCommand("set dateformat dmy;update paym_employee_workdetails set retirementdate = '" + txt_date.Text + "' where pn_EmployeeID = '" + ddl_employee.SelectedValue + "' and pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID = '" + employee.BranchId + "';set dateformat mdy;", mycon);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("select datediff(YY,joiningdate,retirementdate) as serviceyear from paym_employee_workdetails a where pn_employeeid='" + ddl_employee.SelectedValue + "'", mycon);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                txt_service.Text = reader["serviceyear"].ToString();
            }
            reader.Close();
            mycon.Close();
            service = Convert.ToDouble(txt_service.Text);
            grauity = ((basic) * 15 * (service)) / 26;
            txt_grauity.Text = grauity.ToString();
        }
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            pay.EmployeeId = Convert.ToInt32(ddl_employee.SelectedValue);
            pay.Joining_date = txt_joiningDate.Text;
            //pay.Retire_date = txt_retirementDate.Text;
            pay.Last_working_date = txt_date.Text;
            pay.Year = Convert.ToInt32(txt_service.Text);
            pay.Grauity = Convert.ToDecimal(txt_grauity.Text);
            pay.Pf_share = Convert.ToDecimal(txt_pf.Text);
            pay.Encashment_amt = Convert.ToDecimal(txt_encashment.Text);
            pay.loan_amt = Convert.ToDecimal(txt_loan.Text);
            pay.Deduct_salary_amt = Convert.ToDecimal(txt_deduct_amt.Text);
            pay.Final_amt = Convert.ToDecimal(txt_final_amt.Text);
            pay.Status = "Settled";
            pay.Refno = txt_refno.Text;

            _Value = pay.finalsettlement(pay);
            if (_Value != "1")
            {
                lbl_Error.Text = "<font color=Blue>Added Successfully</font>";
                load();
            }
            else
            {
                lbl_Error.Text = "<font color=Red>Error Occured</font>";
                load();
            }
            txt_joiningDate.Text = "";
            //txt_retirementDate.Text = "";
            txt_date.Text = "";
            txt_pf.Text = "";
            txt_encashment.Text = "";
            txt_grauity.Text = "";
            txt_loan.Text = "";
            txt_service.Text = "";
            txt_deduct_amt.Text = "";
            txt_final_amt.Text = "";
            txt_basic_pay.Text = "";
            ddl_dept.SelectedValue = "sd";
            ddl_employee.SelectedValue = "0";

            ckh_deduct.Checked = false;
        }
        catch (Exception ex)
        {
        }
    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        txt_joiningDate.Text = "";
        // txt_retirementDate.Text = "";
        txt_date.Text = "";
        txt_pf.Text = "";
        txt_encashment.Text = "";
        txt_grauity.Text = "";
        txt_loan.Text = "";
        txt_service.Text = "";
        txt_deduct_amt.Text = "";
        txt_final_amt.Text = "";
        txt_basic_pay.Text = "";
        ddl_dept.SelectedValue = "sd";
        ddl_employee.SelectedValue = "0";

        ckh_deduct.Checked = false;
    }
}
