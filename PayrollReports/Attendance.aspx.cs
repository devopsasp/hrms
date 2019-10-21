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
using ePayHrms.Connection;
using ePayHrms.Candidate;
using System.IO;
using ePayHrms.Company;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ePayHrms.BE.Recruitment;
using ePayHrms.Employee;
using ePayHrms.Student;
using ePayHrms.Leave;
using CrystalDecisions.Web;
using CrystalDecisions.Enterprise;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.Reporting;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using ePayHrms.User_authentication;


public partial class PayrollReports_Default : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
    Company company = new Company();
    Employee employee = new Employee();
    PayRoll pay = new PayRoll();
    Student student = new Student();
    Collection<Company> CompanyList, ddlBranchsList;

    Collection<Employee> EmployeeList;
    Collection<Student> StudentList;
    Collection<Student> TimeList;
    Collection<Employee> EmployeeTimeList;
    Collection<PayRoll> PayList;
    Collection<User__Rights> UserRightsList;
    User__Rights user = new User__Rights();
    char s_login_role;

    int i = 0, j, count=0;
    string d_date, query, s_form;
    DataSet ds_userrights;

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
        Response.AddHeader("pragma", "no-cache");
        Response.AddHeader("Cache-Control", "no-cache");
        Response.CacheControl = "no-cache";
        Response.Expires = -1;
        Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
        Response.Cache.SetNoStore();
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //Response.Cache.SetExpires(DateTime.Now);
        Session["Msg_session"] = "";
        Session["Period"] = "";
        Session["Repordid"] = "";
        Session["Query_Session"] = "start";
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        student.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        student.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        s_login_role = Convert.ToChar(Request.Cookies["Login_temp_Role"].Value);
        lbl_error.Text = "";

        if (!IsPostBack)
        {
            CompanyList = company.fn_getCompany();
            if (CompanyList.Count > 0)
            {
                switch (s_login_role)
                {
                    case 'a':
                        tbl_attreport.Visible = false;
                        ddl_Branch.Visible = true;
                       // hr();
                        ddl_Branch_load();
                        break;

                    case 'h':
                        ddl_Branch.Visible = false;
                        tbl_attreport.Visible = true;
                        hr();
                        //chkEmployee_load();
                        //chk_Empcode.Visible = false;
                        //chkall.Visible = false;
                        session_check();
                        break;

                    case 'u':
                        s_form = "54";

                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            user.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                            user.EmployeeID = (int)Session["Login_temp_EmployeeID"];
                            ddl_Branch.Visible = false;
                            tbl_attreport.Visible = true;
                            //chkEmployee_load();
                            session_check();
                        }
                        else
                        {
                            Session["Msg_session"] = "Permission Restricted. Please Contact Administrator.";
                            Response.Redirect("~/Company_Home.aspx");
                        }
                        break;

                    case 'e':
                        ddl_Branch.Visible = false;
                        ListItem li = new ListItem();
                        li.Text = Session["Login_temp_EmpCodeName"].ToString();
                        li.Value = Session["Login_temp_EmployeeID"].ToString();
                        li.Selected = true;
                        chk_Empcode.Items.Clear();
                        chk_Empcode.Items.Add(li);
                        chk_Empcode.Enabled = false;
                        //lbl_selectemp.Visible = false;
                        //chkall.Disabled = true;
                        ddl_Departmentlist.Enabled = false;
                        break;

                    default:
                        Session["Msg_session"] = "Permission Restricted. Please Contact Administrator";
                        Response.Redirect("~/Company_Home.aspx");
                        break;
                }
            }
            else
            {
                Session["Msg_session"] = "Create Company";
                Response.Redirect("~/Company_Home.aspx");
            }
        }
    }


    public void session_check()
    {
        switch (Convert.ToString(Session["Query_Session"]))
        {
            case "start":
                lbl_error.Text = "Welcome To Report Section!";
                Session["Query_Session"] = "start";
                break;
            case "nil":
                lbl_error.Text = "No Result Found";
                Session["Query_Session"] = "start";
                break;
            case "back":
                lbl_error.Text = "";
                Session["Query_Session"] = "start";
                break;
            default:
                final_query_execute();
                break;
        }
    }

    public void hr()
    {
        try
        {
            EmployeeList = employee.fn_getEmployeeList(employee);

            if (EmployeeList.Count > 0)
            {
                chk_Empcode.DataSource = EmployeeList;
                chk_Empcode.DataTextField = "LastName";
                chk_Empcode.DataValueField = "EmployeeId";
                chk_Empcode.DataBind();
            }
            else
            {
                lbl_error.Text = "No employees";
                chk_Empcode.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            lbl_error.Text = "Error";
        }
    }

    public void final_query_execute()
    {
        employee.temp_str = (string)Session["Query_Session"];
        EmployeeList = employee.Temp_Selected_EmployeeList(employee);
        if (EmployeeList.Count > 0)
        {
            lbl_error.Text = EmployeeList.Count + " Employees Selected!";
            Session["Query_Session"] = "start";
        }
        else
        {
            lbl_error.Text = "No Employees has been selected";
            Session["Query_Session"] = "start";
        }
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

    public void chkEmployee_load()
    {
        if (s_login_role == 'a')
        {
            pay.BranchId = (int)ViewState["Att_BranchID"];
            employee.BranchId = (int)ViewState["Att_BranchID"];
        }

        EmployeeList = employee.fn_getEmployeeList(employee);
        if (EmployeeList.Count > 0)
        {
            chk_Empcode.DataSource = EmployeeList;
            chk_Empcode.DataValueField = "EmployeeId";
            chk_Empcode.DataTextField = "LastName";
            chk_Empcode.DataBind();
        }
        else
        {
            chk_Empcode.Items.Clear();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Employees Available');", true);
        }
    }

    protected void ddl_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Branch.SelectedValue != "0")
        {
            ViewState["Att_BranchID"] = Convert.ToInt32(ddl_Branch.SelectedValue);
            tbl_attreport.Visible = true;
            session_check();
            chkEmployee_load();
        }
        else
        {
            tbl_attreport.Visible = false;
        }
    }

    protected void btn_Report_Click(object sender, EventArgs e)
    {
        string intime = "", outtime = "", Latein = "", Lateout = "",   earlyout = "", status = "", stat = "", date = "", dept = "", leav = "";
        int grad = 0;
        double prsdays = 0.0, leavedays = 0.0, odDays = 0.0, holidays = 0.0, weekoff = 0.0, Absdays = 0.0, paidDays = 0.0;
        if (s_login_role == 'a')
        {
            pay.BranchId = (int)ViewState["Att_BranchID"];
            employee.BranchId = (int)ViewState["Att_BranchID"];
        }
        Session["preview_page"] = "~/PayrollReports/Attendance.aspx";
        student.d_Date = employee.Convert_ToSqlDatestring(txt_date.Text);
        employee.d_Date = employee.Convert_ToSqlDatestring(txt_date.Text);
        query = "delete from tempsattendance;delete from temp_muster";
        employee.fn_reportbyid(query);
        
        for (int ddl_i = 0; ddl_i < chk_Empcode.Items.Count; ddl_i++)
        {
            if (chk_Empcode.Items[ddl_i].Selected == true)
            {
                if (ddl_category.SelectedItem.Text == "Student")
                {
                    if (ddl_report.SelectedItem.Text == "Consolidate")
                    {
                        student.DurationFrom = employee.Convert_ToSqlDatestring(txt_date.Text);
                        student.DurationTo = employee.Convert_ToSqlDatestring(txt_tdate.Text);
                        student.RegisterNo = chk_Empcode.Items[ddl_i].Value;
                        student.FirstName = chk_Empcode.Items[ddl_i].Text;
                        TimeList = student.fn_StudentConsolidate(student);
                        if (TimeList.Count > 0)
                        {
                            for (int i = 0; i < TimeList.Count; i++)
                            {
                                dept = ddl_Departmentlist.SelectedItem.Text;
                                date = TimeList[i].Date.ToString();
                                intime = TimeList[i].Intimestr.ToString();
                                outtime = TimeList[i].Outtimestr.ToString();
                                Latein = TimeList[i].Lateinstr.ToString();
                                Lateout = TimeList[i].Lateoutstr.ToString();
                                status = TimeList[i].Flag;
                                leav = TimeList[i].status21;
                                query = "set dateformat dmy;insert into tempsattendance values(" + student.CompanyId + ", '" + student.BranchId + "', '" + TimeList[0].RegisterNo + "', '" + TimeList[0].FirstName + "','" + date + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "','');set dateformat mdy";
                                employee.fn_reportbyid(query);
                                count++;
                            }
                        }
                    }

                    else if (ddl_report.SelectedItem.Text == "Present")
                    {
                        student.RegisterNo = chk_Empcode.Items[ddl_i].Value;
                        student.FirstName = chk_Empcode.Items[ddl_i].Text;
                        TimeList = student.fn_StudentTimeCard(student);
                        if (TimeList.Count > 0)
                        {
                            dept = ddl_Departmentlist.SelectedItem.Text;
                            intime = TimeList[0].Intimestr.ToString();
                            outtime = TimeList[0].Outtimestr.ToString();
                            Latein = TimeList[0].Lateinstr.ToString();
                            Lateout = TimeList[0].Lateoutstr.ToString();
                            status = TimeList[0].Flag;
                            if (status == "XX" || status == "AX" || status == "XA")
                            {
                                query = "insert into tempsattendance values(" + student.CompanyId + ", '" + student.BranchId + "', '" + student.RegisterNo + "', '" + TimeList[0].FirstName + "','" + student.d_Date + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "','','')";
                                employee.fn_reportbyid(query);
                                count++;
                            }
                            
                        }
                    }
                    else if (ddl_report.SelectedItem.Text == "Absent")
                    {
                        student.RegisterNo = chk_Empcode.Items[ddl_i].Value;
                        student.FirstName = chk_Empcode.Items[ddl_i].Text;
                        TimeList = student.fn_StudentTimeCard(student);
                        if (TimeList.Count > 0)
                        {
                            dept = ddl_Departmentlist.SelectedItem.Text;
                            intime = TimeList[0].Intimestr.ToString();
                            outtime = TimeList[0].Outtimestr.ToString();
                            Latein = TimeList[0].Lateinstr.ToString();
                            Lateout = TimeList[0].Lateoutstr.ToString();
                            status = TimeList[0].Flag;
                            if (status == "AA" || status == "AX" || status == "XA")
                            {
                                query = "insert into tempsattendance values(" + student.CompanyId + ", '" + student.BranchId + "', '" + student.RegisterNo + "', '" + TimeList[0].FirstName + "','" + student.d_Date + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "','','')";
                                employee.fn_reportbyid(query);
                                count++;
                            }

                        }
                    }

                    else
                    {
                        student.RegisterNo = chk_Empcode.Items[ddl_i].Value;
                        student.FirstName = chk_Empcode.Items[ddl_i].Text;
                        TimeList = student.fn_StudentTimeCard(student);
                        if (TimeList.Count > 0)
                        {
                            dept = ddl_Departmentlist.SelectedItem.Text;
                            intime = TimeList[0].Intimestr.ToString();
                            outtime = TimeList[0].Outtimestr.ToString();
                            Latein = TimeList[0].Lateinstr.ToString();
                            Lateout = TimeList[0].Lateoutstr.ToString();
                            status = TimeList[0].Flag;
                            query = "insert into tempsattendance values(" + student.CompanyId + ", '" + student.BranchId + "', '" + student.RegisterNo + "', '" + TimeList[0].FirstName + "','" + student.d_Date + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "','','')";
                            employee.fn_reportbyid(query);
                            count++;
                        }
                    }
                }
                else
                {
                    if (ddl_report.SelectedItem.Text == "Daily Attendance")
                    {
                        employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[ddl_i].Value);
                        employee.FirstName = chk_Empcode.Items[ddl_i].Text;
                        EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                        if (EmployeeTimeList.Count > 0)
                        {
                            dept = ddl_Departmentlist.SelectedItem.Text;
                            intime = EmployeeTimeList[0].Intime.ToString("HH:mm");
                            outtime = EmployeeTimeList[0].Outtime.ToString("HH:mm");
                            Latein = EmployeeTimeList[0].Latein.ToString("HH:mm");
                            Lateout = EmployeeTimeList[0].Lateout.ToString("HH:mm");
                            earlyout = EmployeeTimeList[0].Earlyout.ToString("HH:mm");
                            status = EmployeeTimeList[0].Flag;
                            leav = EmployeeTimeList[0].Status21;
                            grad = EmployeeTimeList[0].GradeId;
                            query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,status,Department,Leave_name,earlyout,pn_gradeID) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[0].EmployeeCode + "', '" + EmployeeTimeList[0].FirstName + "','" + employee.d_Date + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "','" + earlyout + "'," + grad + ")";
                            employee.fn_reportbyid(query);
                            count++;
                        }
                    }

                    if (ddl_report.SelectedItem.Text == "Muster Roll")
                    {
                        status = ""; stat = "";
                        prsdays = 0.0; leavedays = 0.0; odDays = 0.0; holidays = 0.0; weekoff = 0.0; Absdays = 0.0; paidDays = 0.0;
                        employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[ddl_i].Value);
                        employee.FirstName = chk_Empcode.Items[ddl_i].Text;
                        employee.DurationFrom = txt_date.Text;
                        employee.DurationTo = txt_tdate.Text;
                        EmployeeTimeList = employee.fn_EmployeeMuster(employee);
                        int cc = 31 - EmployeeTimeList.Count;
                        if (EmployeeTimeList.Count > 0)
                        {
                            //dept = ddl_Departmentlist.SelectedItem.Text;
                            for (int i = 0; i < EmployeeTimeList.Count; i++)
                            {
                                stat = EmployeeTimeList[i].Flag;
                                status += "'" + EmployeeTimeList[i].Flag + "'";
                                if (i != EmployeeTimeList.Count - 1)
                                {
                                    status = status + ",";
                                }
                                if (stat == "XX" || stat == "PX" || stat == "XP")
                                {
                                    prsdays += 1;
                                }
                                else if (stat == "AA")
                                {
                                    Absdays += 1;
                                }
                                else if (stat == "LL")
                                {
                                    leavedays += 1;
                                }
                                else if (stat == "DD")
                                {
                                    odDays += 1;
                                }
                                else if (stat == "XA" || stat == "AX")
                                {
                                    prsdays += 0.5;
                                    Absdays += 0.5;
                                }
                                else if (stat == "XL" || stat == "LX")
                                {
                                    prsdays += 0.5;
                                    leavedays += 0.5;
                                }
                                else if (stat == "LA" || stat == "LX")
                                {
                                    leavedays += 0.5;
                                    Absdays += 0.5;
                                }
                                else if (stat == "XD" || stat == "DX")
                                {
                                    prsdays += 0.5;
                                    odDays += 0.5;
                                }
                                else if (stat == "DA" || stat == "AD")
                                {
                                    odDays += 0.5;
                                    Absdays += 0.5;
                                }
                                else if (stat == "WW")
                                {
                                    weekoff += 1;
                                }
                                else if (stat == "HH")
                                {
                                    holidays += 1;
                                }

                            }

                            if (cc != 0)
                            {
                                for (int j = EmployeeTimeList.Count + 1; j <= 31; j++)
                                {
                                    status = status + ",'-'";
                                }
                            }

                            paidDays = prsdays + leavedays + weekoff + holidays + odDays;
                            //status = EmployeeTimeList[0].Flag;
                            leav = EmployeeTimeList[0].Status21;
                            query = "set dateformat dmy;insert into Temp_Muster values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[0].EmployeeCode + "', '" + EmployeeTimeList[0].FirstName + "','" + employee.DurationFrom + "','" + employee.DurationTo + "'," + status + ",'" + prsdays + "','" + leavedays + "','" + odDays + "','" + holidays + "','" + weekoff + "','" + Absdays + "','" + paidDays + "', " + EmployeeTimeList[0].GradeId + "); set dateformat mdy";
                            //if (EmployeeTimeList.Count == 30)
                            //{
                            //    query = "set dateformat dmy;insert into Temp_Muster values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[0].EmployeeCode + "', '" + EmployeeTimeList[0].FirstName + "','" + employee.DurationFrom + "','" + employee.DurationTo + "'," + status + ",'','" + prsdays + "','" + leavedays + "','" + odDays + "','" + holidays + "','" + weekoff + "','" + Absdays + "','" + paidDays + "', " + EmployeeTimeList[0].GradeId + "); set dateformat mdy";
                            //}
                            //else if (EmployeeTimeList.Count == 29)
                            //{
                            //    query = "set dateformat dmy;insert into Temp_Muster values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[0].EmployeeCode + "', '" + EmployeeTimeList[0].FirstName + "','" + employee.DurationFrom + "','" + employee.DurationTo + "'," + status + ",'','','','" + prsdays + "','" + leavedays + "','" + odDays + "','" + holidays + "','" + weekoff + "','" + Absdays + "','" + paidDays + "', " + EmployeeTimeList[0].GradeId + "); set dateformat mdy";
                            //}
                            //else if (EmployeeTimeList.Count == 28)
                            //{
                            //    query = "set dateformat dmy;insert into Temp_Muster values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[0].EmployeeCode + "', '" + EmployeeTimeList[0].FirstName + "','" + employee.DurationFrom + "','" + employee.DurationTo + "'," + status + ",'','','','','" + prsdays + "','" + leavedays + "','" + odDays + "','" + holidays + "','" + weekoff + "','" + Absdays + "','" + paidDays + "', " + EmployeeTimeList[0].GradeId + "); set dateformat mdy";
                            //}
                            employee.fn_reportbyid(query);
                            count++;
                        }
                    }
                    else if (ddl_report.SelectedItem.Text == "Present")
                    {
                        employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[ddl_i].Value);
                        employee.FirstName = chk_Empcode.Items[ddl_i].Text;
                        EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                        if (EmployeeTimeList.Count > 0)
                        {
                            dept = ddl_Departmentlist.SelectedItem.Text;
                            intime = EmployeeTimeList[0].Intime.ToString("HH:mm");
                            outtime = EmployeeTimeList[0].Outtime.ToString("HH:mm");
                            Latein = EmployeeTimeList[0].Latein.ToString("HH:mm");
                            Lateout = EmployeeTimeList[0].Lateout.ToString("HH:mm");
                            earlyout = EmployeeTimeList[0].Earlyout.ToString("HH:mm");
                            status = EmployeeTimeList[0].Flag;
                            leav = EmployeeTimeList[0].Status21;
                            grad = EmployeeTimeList[0].GradeId;
                            if (status == "XX" || status == "AX" || status == "XA" || status == "LX" || status == "XL" || status == "XD" || status == "DX")
                            {
                                query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,status,Department,Leave_name,earlyout,pn_gradeID) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[0].EmployeeCode + "', '" + EmployeeTimeList[0].FirstName + "','" + employee.d_Date + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "','" + earlyout + "'," + grad + ")";
                                employee.fn_reportbyid(query);
                                count++;
                            }
                            
                        }
                    }

                    else if (ddl_report.SelectedItem.Text == "Absent")
                    {
                        employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[ddl_i].Value);
                        employee.FirstName = chk_Empcode.Items[ddl_i].Text;
                        EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                        if (EmployeeTimeList.Count > 0)
                        {
                            dept = ddl_Departmentlist.SelectedItem.Text;
                            intime = EmployeeTimeList[0].Intime.ToString("HH:mm");
                            outtime = EmployeeTimeList[0].Outtime.ToString("HH:mm");
                            Latein = EmployeeTimeList[0].Latein.ToString("HH:mm");
                            Lateout = EmployeeTimeList[0].Lateout.ToString("HH:mm");
                            earlyout = EmployeeTimeList[0].Earlyout.ToString("HH:mm");
                            status = EmployeeTimeList[0].Flag;
                            leav = EmployeeTimeList[0].Status21;
                            grad = EmployeeTimeList[0].GradeId;
                            if (status == "AA" || status == "AX" || status == "XA" || status == "LA" || status == "AL" || status == "AD" || status == "DA")
                            {
                                query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,status,Department,Leave_name,earlyout,pn_gradeID) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[0].EmployeeCode + "', '" + EmployeeTimeList[0].FirstName + "','" + employee.d_Date + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "','" + earlyout + "'," + grad + ")";
                                employee.fn_reportbyid(query);
                                count++;
                            }
                            
                        }
                    }

                    else if (ddl_report.SelectedItem.Text == "Missing Staff")
                    {
                        employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[ddl_i].Value);
                        employee.FirstName = chk_Empcode.Items[ddl_i].Text;
                        EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                        if (EmployeeTimeList.Count > 0)
                        {
                            dept = ddl_Departmentlist.SelectedItem.Text;
                            intime = EmployeeTimeList[0].Intime.ToString("HH:mm");
                            outtime = EmployeeTimeList[0].Outtime.ToString("HH:mm");
                            Latein = EmployeeTimeList[0].Latein.ToString("HH:mm");
                            Lateout = EmployeeTimeList[0].Lateout.ToString("HH:mm");
                            earlyout = EmployeeTimeList[0].Earlyout.ToString("HH:mm");
                            status = EmployeeTimeList[0].Flag;
                            leav = EmployeeTimeList[0].Status21;
                            grad = EmployeeTimeList[0].GradeId;
                            if (status == "AA" || status == "LL" || status == "DD" || status == "LA" || status == "AL" || status == "AD" || status == "DA")
                            {
                                query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,status,Department,Leave_name,earlyout,pn_gradeID) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[0].EmployeeCode + "', '" + EmployeeTimeList[0].FirstName + "','" + employee.d_Date + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "','" + earlyout + "'," + grad + ")";
                                employee.fn_reportbyid(query);
                                count++;
                            }

                        }
                    }

                    else if (ddl_report.SelectedItem.Text == "Late in")
                    {
                        employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[ddl_i].Value);
                        employee.FirstName = chk_Empcode.Items[ddl_i].Text;
                        EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                        if (EmployeeTimeList.Count > 0)
                        {
                            dept = ddl_Departmentlist.SelectedItem.Text;
                            intime = EmployeeTimeList[0].Intime.ToString("HH:mm");
                            outtime = EmployeeTimeList[0].Outtime.ToString("HH:mm");
                            Latein = EmployeeTimeList[0].Latein.ToString("HH:mm");
                            Lateout = EmployeeTimeList[0].Lateout.ToString("HH:mm");
                            earlyout = EmployeeTimeList[0].Earlyout.ToString("HH:mm");
                            status = EmployeeTimeList[0].Flag;
                            leav = EmployeeTimeList[0].Status21;
                            grad = EmployeeTimeList[0].GradeId;
                            if (Latein != "00:00")
                            {
                                query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,status,Department,Leave_name,earlyout,pn_gradeID) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[0].EmployeeCode + "', '" + EmployeeTimeList[0].FirstName + "','" + employee.d_Date + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "','" + earlyout + "'," + grad + ")";
                                employee.fn_reportbyid(query);
                                count++;
                            }
                            
                        }
                    }

                    else if (ddl_report.SelectedItem.Text == "Leave")
                    {
                        employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[ddl_i].Value);
                        employee.FirstName = chk_Empcode.Items[ddl_i].Text;
                        EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                        if (EmployeeTimeList.Count > 0)
                        {
                            dept = ddl_Departmentlist.SelectedItem.Text;
                            intime = EmployeeTimeList[0].Intime.ToString("HH:mm");
                            outtime = EmployeeTimeList[0].Outtime.ToString("HH:mm");
                            Latein = EmployeeTimeList[0].Latein.ToString("HH:mm");
                            Lateout = EmployeeTimeList[0].Lateout.ToString("HH:mm");
                            earlyout = EmployeeTimeList[0].Earlyout.ToString("HH:mm");
                            status = EmployeeTimeList[0].Flag;
                            leav = EmployeeTimeList[0].Status21;
                            grad = EmployeeTimeList[0].GradeId;
                            if (status == "LL" || status == "XL" || status == "LX" || status == "AL" || status == "LA" || status == "DL" || status == "LD")
                            {
                                query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,status,Department,Leave_name,earlyout,pn_gradeID) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[0].EmployeeCode + "', '" + EmployeeTimeList[0].FirstName + "','" + employee.d_Date + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "','" + earlyout + "'," + grad + ")";
                                employee.fn_reportbyid(query);
                                count++;
                            }
                            
                        }
                    }
                    else if (ddl_report.SelectedItem.Text == "On Duty")
                    {
                        employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[ddl_i].Value);
                        employee.FirstName = chk_Empcode.Items[ddl_i].Text;
                        EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                        if (EmployeeTimeList.Count > 0)
                        {
                            dept = ddl_Departmentlist.SelectedItem.Text;
                            intime = EmployeeTimeList[0].Intime.ToString("HH:mm");
                            outtime = EmployeeTimeList[0].Outtime.ToString("HH:mm");
                            Latein = EmployeeTimeList[0].Latein.ToString("HH:mm");
                            Lateout = EmployeeTimeList[0].Lateout.ToString("HH:mm");
                            earlyout = EmployeeTimeList[0].Earlyout.ToString("HH:mm");
                            status = EmployeeTimeList[0].Flag;
                            leav = EmployeeTimeList[0].Status21;
                            grad = EmployeeTimeList[0].GradeId;
                            if (status == "DD" || status == "XD" || status == "DX" || status == "AD" || status == "DA" || status == "DL" || status == "LD")
                            {
                                query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,status,Department,Leave_name,earlyout,pn_gradeID) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[0].EmployeeCode + "', '" + EmployeeTimeList[0].FirstName + "','" + employee.d_Date + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "','" + earlyout + "'," + grad + ")";
                                employee.fn_reportbyid(query);
                                count++;
                            }

                        }
                    }
                    else if (ddl_report.SelectedItem.Text == "Morning Attendance")
                    {
                        employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[ddl_i].Value);
                        employee.FirstName = chk_Empcode.Items[ddl_i].Text;
                        EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                        if (EmployeeTimeList.Count > 0)
                        {
                            dept = ddl_Departmentlist.SelectedItem.Text;
                            intime = EmployeeTimeList[0].Intime.ToString("HH:mm");
                            outtime = EmployeeTimeList[0].Outtime.ToString("HH:mm");
                            Latein = EmployeeTimeList[0].Latein.ToString("HH:mm");
                            Lateout = EmployeeTimeList[0].Lateout.ToString("HH:mm");
                            status = EmployeeTimeList[0].Flag;
                            leav = EmployeeTimeList[0].Status21;
                            grad = EmployeeTimeList[0].GradeId;
                            query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,status,Department,Leave_name,earlyout,pn_gradeID) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[0].EmployeeCode + "', '" + EmployeeTimeList[0].FirstName + "','" + employee.d_Date + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','',''," + grad + ")";
                            employee.fn_reportbyid(query);
                            count++;
                        }
                    }
                    else if (ddl_report.SelectedItem.Text == "Consolidate")
                    {
                        employee.DurationFrom = employee.Convert_ToSqlDatestring(txt_date.Text);
                        employee.DurationTo = employee.Convert_ToSqlDatestring(txt_tdate.Text);
                        employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[ddl_i].Value);
                        employee.FirstName = chk_Empcode.Items[ddl_i].Text;
                        EmployeeTimeList = employee.fn_EmployeeConsolidate(employee);
                        if (EmployeeTimeList.Count > 0)
                        {
                            for (int i = 0; i < EmployeeTimeList.Count; i++)
                            {
                                dept = ddl_Departmentlist.SelectedItem.Text;
                                date = EmployeeTimeList[i].Date.ToString();
                                intime = EmployeeTimeList[i].Intime.ToString("HH:mm");
                                outtime = EmployeeTimeList[i].Outtime.ToString("HH:mm");
                                Latein = EmployeeTimeList[i].Latein.ToString("HH:mm");
                                Lateout = EmployeeTimeList[i].Lateout.ToString("HH:mm");
                                status = EmployeeTimeList[i].Flag;
                                leav = EmployeeTimeList[i].Status21;
                                grad = EmployeeTimeList[0].GradeId;
                                query = "set dateformat dmy;insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,status,Department,Leave_name,earlyout,pn_gradeID) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[0].EmployeeCode + "', '" + EmployeeTimeList[0].FirstName + "','" + date + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "',''," + grad + ");set dateformat mdy";
                                employee.fn_reportbyid(query);
                                count++;
                            }
                        }
                    }
                }
            }
        }

        if (count != 0)
        {
            if (ddl_category.SelectedItem.Text == "Student")
            {
                if (ddl_report.SelectedItem.Text == "Daily Attendance" || ddl_report.SelectedItem.Text == "Present" || ddl_report.SelectedItem.Text == "Absent")
                {
                    Session["ReportName"] = "~/crystalreports/AttendanceDaily.rpt";
                }
            }
            else
            {
                if (ddl_report.SelectedItem.Text == "Daily Attendance" || ddl_report.SelectedItem.Text == "Present" || ddl_report.SelectedItem.Text == "Absent")
                {
                    Session["ReportName"] = "~/crystalreports/AttendanceDailyEmp.rpt";
                }
                else if (ddl_report.SelectedItem.Text == "Morning Attendance")
                {
                    Session["ReportName"] = "~/crystalreports/Mattendance.rpt";
                }
                else if (ddl_report.SelectedItem.Text == "Late in")
                {
                    Session["ReportName"] = "~/crystalreports/Latein.rpt";
                }
                else if (ddl_report.SelectedItem.Text == "Leave")
                {
                    Session["ReportName"] = "~/crystalreports/Leave.rpt";
                }
                else if (ddl_report.SelectedItem.Text == "Missing Staff")
                {
                    Session["ReportName"] = "~/crystalreports/Missing.rpt";
                }
                else if (ddl_report.SelectedItem.Text == "Muster Roll")
                {
                    Session["ReportName"] = "~/crystalreports/Musteroll.rpt";
                }
                else if (ddl_report.SelectedItem.Text == "On Duty")
                {
                    Session["ReportName"] = "~/crystalreports/Onduty.rpt";
                }
                else if (ddl_report.SelectedItem.Text == "Consolidate")
                {
                    if (ddl_category.SelectedItem.Text == "Staff")
                    {
                        Session["ReportName"] = "~/crystalreports/Consolidate.rpt";
                    }
                    else
                    {
                        Session["ReportName"] = "~/crystalreports/ConsolidateStudent.rpt";
                        //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Consolidate report applicable only for staffs');", true);
                        //return;
                    }
                }
            }

            Response.Redirect("Report_view.aspx");
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No data found for the selected criteria');", true);
        }
    }

    protected void ddl_CurrentYearlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        chk_Empcode.Items.Clear();
        StudentCheckList();
    }

    public void StudentCheckList()
    {
        student.ClassName = ddl_Courselist.SelectedItem.Text;
        student.DepartmentName = ddl_Departmentlist.SelectedItem.Text;
        student.Cyear = ddl_CurrentYearlist.SelectedItem.Text;

        StudentList = student.fn_getStudentList_Department(student);
        if (StudentList.Count > 0)
        {
            chk_Empcode.DataSource = StudentList;
            chk_Empcode.DataTextField = "FirstName";
            chk_Empcode.DataValueField = "RegisterNo";
            chk_Empcode.DataBind();
        }
        else
        {
            chk_Empcode.Items.Clear();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No data found for the selected criteria');", true);
        }
    }

    public void EmployeeCheckList()
    {
        if (ddl_Departmentlist.SelectedItem.Text == "All")
        {
            EmployeeList = employee.fn_getEmployeeList(employee);
            if (EmployeeList.Count > 0)
            {
                chk_Empcode.DataSource = EmployeeList;
                chk_Empcode.DataTextField = "LastName";
                chk_Empcode.DataValueField = "EmployeeId";
                chk_Empcode.DataBind();
            }
        }
        else
        {
            employee.DepartmentId = Convert.ToInt32(ddl_Departmentlist.SelectedItem.Value);
            EmployeeList = employee.fn_getEmployeeDepartment(employee);
            if (EmployeeList.Count > 0)
            {
                chk_Empcode.DataSource = EmployeeList;
                chk_Empcode.DataTextField = "LastName";
                chk_Empcode.DataValueField = "EmployeeId";
                chk_Empcode.DataBind();
            }
        }
    }

    protected void ddl_category_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_category.SelectedItem.Text == "Staff")
        {
            if (s_login_role != 'u')
            {
                EmployeeList = employee.fn_getDepartmentList1(employee.BranchId);
                if (EmployeeList.Count > 0)
                {
                    ddl_Departmentlist.DataSource = EmployeeList;
                    ddl_Departmentlist.DataTextField = "DepartmentName";
                    ddl_Departmentlist.DataValueField = "DepartmentId";
                    ddl_Departmentlist.DataBind();
                    ddl_Departmentlist.Items.Add("All");
                }
                ddl_Departmentlist.SelectedIndex = ddl_Departmentlist.Items.Count - 1;
                ddl_Courselist.Enabled = false;
                ddl_CurrentYearlist.Enabled = false;
            }
        }
        else if(ddl_category.SelectedItem.Text == "Student")
        {
            ddl_Courselist.Enabled = true;
            ddl_CurrentYearlist.Enabled = true;
            StudentList = student.fn_department(employee.BranchId);
            if (StudentList.Count > 0)
            {
                ddl_Departmentlist.DataSource = StudentList;
                ddl_Departmentlist.DataTextField = "DepartmentName";
                ddl_Departmentlist.DataValueField = "DepartmentId";
                ddl_Departmentlist.DataBind();
            }
            if (s_login_role == 'u')
            {
                user.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                user.EmployeeID = (int)Session["Login_temp_EmployeeID"];
                UserRightsList = user.fn_emp_user_creation(user);
                if (UserRightsList[0].DepartmentName == "All (First Year Only)")
                {
                    //ddl_CurrentYearlist.Enabled = false;
                    ddl_CurrentYearlist.Items.Remove("2");
                    ddl_CurrentYearlist.Items.Remove("3");
                    ddl_CurrentYearlist.Items.Remove("4");
                    return;
                }

                ddl_Departmentlist.SelectedItem.Text = UserRightsList[0].DepartmentName;
                ddl_Departmentlist.Enabled = false;
                ddl_Courselist.Enabled = false;
            }
        }
    }

    protected void ddl_Departmentlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_category.SelectedItem.Text == "Staff")
        {
            EmployeeCheckList();
        }
    }
    protected void ddl_report_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_report.SelectedItem.Text == "Consolidate" || ddl_report.SelectedItem.Text == "Muster Roll")
        {
            chkall.Disabled = false;
            chkall.Checked = false;
            foreach (ListItem li in chk_Empcode.Items)
            {
                li.Selected = false;
                li.Enabled = true;
            }
            txt_tdate.Enabled = true;
        }
        else
        {
            chkall.Disabled = true;
            chkall.Checked = true;
            foreach (ListItem li in chk_Empcode.Items)
            {
                li.Selected = true;
                li.Enabled = false;
            }
            txt_tdate.Enabled = false;
        }
    }
}
