using System;
using System.Data;
using System.Configuration;
using System.Web.UI;
using ePayHrms.Company;
using System.Collections.ObjectModel;
using ePayHrms.Employee;
using ePayHrms.Student;
using ePayHrms.User_authentication;
using System.Web.UI.WebControls;

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
    string s_login_role;

    int i = 0, j, count = 0;
    string d_date, e_date, query, s_form;
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

        Session["Period"] = "";
        Session["Repordid"] = "";
        //TextBox1.Enabled = false;
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        student.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        student.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        lbl_error.Text = "";

        if (!IsPostBack)
        {
            CompanyList = company.fn_getCompany();
            if (CompanyList.Count > 0)
            {
                switch (s_login_role)
                {
                    case "a":
                        tbl_attreport.Visible = false;
                        ddl_Branch.Visible = true;

                        // hr();
                        ddl_Branch_load();
                        ddl_Department_load();
                        ddl_pattern_load();
                        break;

                    case "h":
                        ddl_Branch.Visible = false;
                        tbl_attreport.Visible = true;
                        //   hr();
                        ddl_Department_load();
                        ddl_pattern_load();
                        //chkEmployee_load();
                        //chk_Empcode.Visible = false;
                        //chkall.Visible = false;
                        //session_check();
                        break;

                    case "u":
                        s_form = "54";

                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            user.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                            user.EmployeeID = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
                            ddl_Branch.Visible = false;
                            tbl_attreport.Visible = true;
                            //chkEmployee_load();
                            //session_check();
                        }
                        else
                        {
                            Response.Cookies["Msg_Session"].Value = "Permission Restricted. Please Contact Administrator.";
                            Response.Redirect("~/Company_Home.aspx");
                        }
                        break;

                    case "e":
                        ddl_Branch.Visible = false;
                        ListItem li = new ListItem();
                        li.Text = Request.Cookies["EmpCodeName"].Value;
                        li.Value = Request.Cookies["Login_temp_EmployeeID"].Value;
                        li.Selected = true;
                        //chk_Empcode.Items.Clear();
                        //chk_Empcode.Items.Add(li);
                        //chk_Empcode.Enabled = false;
                        //lbl_selectemp.Visible = false;
                        //chkall.Disabled = true;
                        ddl_Departmentlist.Enabled = false;
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
    public void ddl_Department_load()
    {
        EmployeeList = employee.fn_getDepartmentList1(employee.BranchId);
        if (EmployeeList.Count > 0)
        {

            for (int ddl_i = -2; ddl_i < EmployeeList.Count; ddl_i++)
            {

                if (ddl_i == -2)
                {
                    ListItem es_list = new ListItem();

                    es_list.Text = "Select Department";
                    es_list.Value = "0";
                    ddl_Departmentlist.Items.Add(es_list);
                }
                else if (ddl_i == -1)
                {
                    ListItem es_list = new ListItem();

                    es_list.Text = "All";
                    es_list.Value = "1";
                    ddl_Departmentlist.Items.Add(es_list);
                }
                else
                {
                    ListItem es_list = new ListItem();
                    es_list.Value = EmployeeList[ddl_i].DepartmentId.ToString();
                    es_list.Text = EmployeeList[ddl_i].DepartmentName.ToString();
                    ddl_Departmentlist.Items.Add(es_list);
                }
            }
        }

    }
    public void ddl_pattern_load()
    {
        EmployeeList = employee.fn_getPatternList(employee.BranchId);
        if (EmployeeList.Count > 0)
        {

            for (int ddl_i = -2; ddl_i < EmployeeList.Count; ddl_i++)
            {

                if (ddl_i == -2)
                {
                    ListItem es_list = new ListItem();

                    es_list.Text = "Select Pattern";
                    es_list.Value = "0";
                    ddl_ShiftList.Items.Add(es_list);
                }
                else if (ddl_i == -1)
                {
                    ListItem es_list = new ListItem();

                    es_list.Text = "All";
                    es_list.Value = "1";
                    ddl_ShiftList.Items.Add(es_list);
                }
                else
                {
                    ListItem es_list = new ListItem();
                    es_list.Text = EmployeeList[ddl_i].ShiftName.ToString();
                    ddl_ShiftList.Items.Add(es_list);
                }
            }
        }

    }



    //public void session_check()
    //{
    //    switch (Request.Cookies["Query_Session"].Value)
    //    {
    //        case "start":
    //            lbl_error.Text = "Welcome To Report Section!";
    //            Response.Cookies["Query_Session"].Value= "start";
    //            break;
    //        case "nil":
    //            lbl_error.Text = "No Result Found";
    //            Response.Cookies["Query_Session"].Value= "start";
    //            break;
    //        case "back":
    //            lbl_error.Text = "";
    //            Response.Cookies["Query_Session"].Value= "start";
    //            break;
    //        default:
    //            final_query_execute();
    //            break;
    //    }
    //}

    //public void admin()
    //{

    //    EmployeeList = employee.fn_getEmployeeList(employee);

    //    if (EmployeeList.Count > 0)
    //    {
    //        chk_Empcode.DataSource = EmployeeList;
    //        chk_Empcode.DataTextField = "LastName";
    //        chk_Empcode.DataValueField = "EmployeeId";
    //        chk_Empcode.DataBind();
    //    }
    //    else
    //    {
    //        lbl_error.Text = "No employees";
    //        chk_Empcode.Items.Clear();
    //    }
    //    //employee.temp_str = "select * from Temp_Employee";
    //    //EmpFirstList = employee.Temp_Selected_EmployeeList(employee);
    //    //if (EmpFirstList.Count == 0)
    //    //{
    //    //    temp_tables();
    //    //}  
    //}


    //public void hr()
    //{

    //    try
    //    {
    //        EmployeeList = employee.fn_getEmployeeList(employee);
    //        if (EmployeeList.Count > 0)
    //        {

    //            chk_Empcode.DataSource = EmployeeList;
    //            chk_Empcode.DataValueField = "EmployeeId";
    //            chk_Empcode.DataTextField = "LastName";
    //            chk_Empcode.DataBind();

    //        }
    //        else
    //        {
    //            lbl_error.Text = "No employees";
    //            chk_Empcode.Items.Clear();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lbl_error.Text = "Error";
    //    }
    //}



    public void final_query_execute()
    {
        employee.temp_str = Request.Cookies["Query_Session"].Value;
        EmployeeList = employee.Temp_Selected_EmployeeList(employee);
        if (EmployeeList.Count > 0)
        {
            lbl_error.Text = EmployeeList.Count + " Employees Selected!";
            Response.Cookies["Query_Session"].Value = "start";
        }
        else
        {
            lbl_error.Text = "No Employees has been selected";
            Response.Cookies["Query_Session"].Value = "start";
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
    public void ddl_Employee_load()
    {
        chk_Empcode.Items.Clear();

        if (ddl_Departmentlist.SelectedItem.Text == "All")
        {
            EmployeeList = employee.fn_getEmployeeList(employee);
            if (EmployeeList.Count > 0)
            {
                for (int ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
                {
                    if (ddl_i == -1)
                    {
                        // Label2.Visible = true;
                    }
                    else
                    {
                        ListItem es_list = new ListItem();
                        es_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
                        es_list.Text = EmployeeList[ddl_i].LastName.ToString();
                        chk_Empcode.Items.Add(es_list);
                    }
                }
            }
        }
        else
        {
            employee.DepartmentId = Convert.ToInt32(ddl_Departmentlist.SelectedValue);
            EmployeeList = employee.fn_getEmployeeDepartment(employee);
            if (EmployeeList.Count > 0)
            {
                for (int ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
                {
                    if (ddl_i == -1)
                    {
                        //  Label2.Visible = true;
                    }
                    else
                    {
                        ListItem es_list = new ListItem();
                        es_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
                        es_list.Text = EmployeeList[ddl_i].LastName.ToString();
                        chk_Empcode.Items.Add(es_list);
                    }
                }
            }
        }
    }
    protected void ddl_patternlist_selectedIndexChanged(object sender, EventArgs e)
    {

        ddl_pattern_load();
    }
    protected void ddl_Departmentlist_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddl_Employee_load();
    }
    public void chkEmployee_load()
    {
        if (s_login_role == "a")
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
            //session_check();
            //chkEmployee_load();
        }
        else
        {
            tbl_attreport.Visible = false;
        }
    }

    protected void btn_Report_Click(object sender, EventArgs e)
    {
        string intime = "", work_hrs = "", data = "", outtime = "", Latein = "", ToDate = "", shift_code = "", dates = "", enddate = "", Lateout = "", Ot_hrs = "", whours = "", earlyout = "", status = "", stat = "", date = "", dept = "", leav = "";
        int grad = 0;
        DateTime temp , inti, outi;
        double prsdays = 0.0, leavedays = 0.0, odDays = 0.0, holidays = 0.0, weekoff = 0.0, Absdays = 0.0, paidDays = 0.0;
        if (s_login_role == "a")
        {
            pay.BranchId = (int)ViewState["Att_BranchID"];
            employee.BranchId = (int)ViewState["Att_BranchID"];
        }
        Session["preview_page"] = "~/PayrollReports/Attendance.aspx";
        student.d_Date = employee.Convert_ToSqlDatestring(txt_date.Text);
        employee.d_Date = employee.Convert_ToSqlDatestring(txt_date.Text);
        student.e_date = employee.Convert_ToSqlDatestring(txt_tdate.Text);
        employee.e_date = employee.Convert_ToSqlDatestring(txt_tdate.Text);

        query = "delete from tempsattendance;delete from temp_muster;delete from tempshiftdetails";
        employee.fn_reportbyid(query);

        for (int ddl_i = 0; ddl_i < chk_Empcode.Items.Count; ddl_i++)
        {
            if (chk_Empcode.Items[ddl_i].Selected == true)
            {
                //if (ddl_category.SelectedItem.Text == "Student")
                //{
                //    if (ddl_report.SelectedItem.Text == "Consolidate")
                //    {
                //        student.DurationFrom = employee.Convert_ToSqlDatestring(txt_date.Text);
                //        student.DurationTo = employee.Convert_ToSqlDatestring(txt_tdate.Text);
                //        student.RegisterNo = chk_Empcode.Items[ddl_i].Value;
                //        student.FirstName = chk_Empcode.Items[ddl_i].Text;
                //        TimeList = student.fn_StudentConsolidate(student);
                //        if (TimeList.Count > 0)
                //        {
                //            for (int i = 0; i < TimeList.Count; i++)
                //            {
                //                dept = employee.fn_DepartmentName();
                //                date = TimeList[i].Date.ToString();
                //                intime = TimeList[i].Intimestr.ToString();
                //                outtime = TimeList[i].Outtimestr.ToString();
                //                Latein = TimeList[i].Lateinstr.ToString();
                //                Lateout = TimeList[i].Lateoutstr.ToString();
                //                status = TimeList[i].Flag;
                //                leav = TimeList[i].status21;
                //                dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                //                temp = Convert.ToDateTime(txt_tdate.Text);
                //                enddate = temp.ToString("yyyy/MM/dd");
                //                query = "set dateformat dmy;insert into tempsattendance values(" + student.CompanyId + ", '" + student.BranchId + "', '" + TimeList[i].RegisterNo + "', '" + TimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "','','','" + enddate + "');set dateformat mdy";
                //                employee.fn_reportbyid(query);
                //                count++;
                //            }
                //        }
                //    }

                //    else if (ddl_report.SelectedItem.Text == "Present")
                //    {
                //        student.RegisterNo = chk_Empcode.Items[ddl_i].Value;
                //        student.FirstName = chk_Empcode.Items[ddl_i].Text;
                //        TimeList = student.fn_StudentTimeCard(student);
                //        if (TimeList.Count > 0)
                //        {
                //            for (int i = 0; i < EmployeeTimeList.Count; i++)
                //            {
                //                dept = employee.fn_DepartmentName();
                //                intime = TimeList[i].Intimestr.ToString();
                //                outtime = TimeList[i].Outtimestr.ToString();
                //                Latein = TimeList[i].Lateinstr.ToString();
                //                Lateout = TimeList[i].Lateoutstr.ToString();
                //                status = TimeList[i].Flag;
                //                dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                //                temp = Convert.ToDateTime(txt_tdate.Text);
                //                enddate = temp.ToString("yyyy/MM/dd");
                //                if (status == "XX" || status == "AX" || status == "XA")
                //                {
                //                    query = "insert into tempsattendance values(" + student.CompanyId + ", '" + student.BranchId + "', '" + student.RegisterNo + "', '" + TimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "','','','" + enddate + "')";
                //                    employee.fn_reportbyid(query);
                //                    count++;
                //                }

                //            }
                //        }
                //    }
                //    else if (ddl_report.SelectedItem.Text == "Absent")
                //    {


                //        student.RegisterNo = chk_Empcode.Items[ddl_i].Value;
                //        student.FirstName = chk_Empcode.Items[ddl_i].Text;
                //        TimeList = student.fn_StudentTimeCard(student);
                //        if (TimeList.Count > 0)
                //        {
                //            for (int i = 0; i < EmployeeTimeList.Count; i++)
                //            {
                //                dept = employee.fn_DepartmentName();
                //                intime = TimeList[i].Intimestr.ToString();
                //                outtime = TimeList[i].Outtimestr.ToString();
                //                Latein = TimeList[i].Lateinstr.ToString();
                //                Lateout = TimeList[i].Lateoutstr.ToString();
                //                status = TimeList[i].Flag;
                //                dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                //                temp = Convert.ToDateTime(txt_tdate.Text);
                //                enddate = temp.ToString("yyyy/MM/dd");
                //                if (status == "AA" || status == "AX" || status == "XA")
                //                {
                //                    query = "insert into tempsattendance values(" + student.CompanyId + ", '" + student.BranchId + "', '" + student.RegisterNo + "', '" + TimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "','','','" + enddate + "')";
                //                    employee.fn_reportbyid(query);
                //                    count++;
                //                }
                //            }
                //        }
                //    }

                //    else
                //    {
                //        student.RegisterNo = chk_Empcode.Items[ddl_i].Value;
                //        student.FirstName = chk_Empcode.Items[ddl_i].Text;
                //        TimeList = student.fn_StudentTimeCard(student);
                //        for (int i = 0; i < TimeList.Count; i++)
                //        {
                //            if (TimeList.Count > 0)
                //            {
                //                dept = employee.fn_DepartmentName();
                //                intime = TimeList[i].Intimestr.ToString();
                //                outtime = TimeList[i].Outtimestr.ToString();
                //                Latein = TimeList[i].Lateinstr.ToString();
                //                Lateout = TimeList[i].Lateoutstr.ToString();
                //                status = TimeList[i].Flag;
                //                dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                //                temp = Convert.ToDateTime(txt_tdate.Text);
                //                enddate = temp.ToString("yyyy/MM/dd");
                //                query = "insert into tempsattendance values(" + student.CompanyId + ", '" + student.BranchId + "', '" + student.RegisterNo + "', '" + TimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "','','','" + enddate + "')";
                //                employee.fn_reportbyid(query);
                //                count++;
                //            }
                //        }
                //    }
                //}
                //else
                //{
                if (ddl_report.SelectedItem.Text == "Daily Attendance")
                {
                    employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[ddl_i].Value);
                    employee.FirstName = chk_Empcode.Items[ddl_i].Text;

                    if (ddl_ShiftList.SelectedItem.Text == "All")
                    {
                        EmployeeList = employee.fn_getPatternList(employee.BranchId);
                        if (EmployeeList.Count > 0)
                        {

                            for (int j = 0; j < EmployeeList.Count; j++)
                            {
                                employee.shiftcode = EmployeeList[j].ShiftName.ToString();
                                EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                                if (EmployeeTimeList.Count > 0)
                                {
                                    for (int i = 0; i < EmployeeTimeList.Count; i++)
                                    {
                                        dept = employee.fn_DepartmentName();
                                        intime = EmployeeTimeList[i].Intime.ToString("HH:mm");
                                        outtime = EmployeeTimeList[i].Outtime.ToString("HH:mm");
                                        Latein = EmployeeTimeList[i].Latein.ToString("HH:mm");
                                        Lateout = EmployeeTimeList[i].Lateout.ToString("HH:mm");
                                        earlyout = EmployeeTimeList[i].Earlyout.ToString("HH:mm");
                                        status = EmployeeTimeList[i].Flag;
                                        leav = EmployeeTimeList[i].Status21;
                                        grad = EmployeeTimeList[i].GradeId;
                                        shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                        dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                                        temp = Convert.ToDateTime(txt_tdate.Text);
                                        enddate = temp.ToString("yyyy/MM/dd");
                                        if (intime != "00:00" && outtime != "00:00")
                                        {
                                            inti = Convert.ToDateTime(intime);
                                            outi = Convert.ToDateTime(outtime);
                                            work_hrs = Convert.ToString(outi - inti);
                                        }
                                        else if (intime != "00:00")
                                        {
                                            work_hrs = "Morning punch is missing";
                                        }
                                        else
                                        {
                                            work_hrs = "evening punch is missing";
                                        }
                                        query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,whours,status,Department,Leave_name,earlyout,pn_gradeID,To_Date,shift_code,Work_hrs) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[i].EmployeeCode + "', '" + EmployeeTimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + whours + "','" + status + "','" + dept + "','" + leav + "','" + earlyout + "'," + grad + ",'" + enddate + "','" + shift_code + "','" + work_hrs + "')";
                                        employee.fn_reportbyid(query);
                                        count++;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[ddl_i].Value);
                        employee.FirstName = chk_Empcode.Items[ddl_i].Text;
                        employee.shiftcode = ddl_ShiftList.SelectedItem.Text;
                        EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                        if (EmployeeTimeList.Count > 0)
                        {
                            for (int i = 0; i < EmployeeTimeList.Count; i++)
                            {
                                dept = employee.fn_DepartmentName();
                                intime = EmployeeTimeList[i].Intime.ToString("HH:mm");
                                outtime = EmployeeTimeList[i].Outtime.ToString("HH:mm");
                                Latein = EmployeeTimeList[i].Latein.ToString("HH:mm");
                                Lateout = EmployeeTimeList[i].Lateout.ToString("HH:mm");
                                earlyout = EmployeeTimeList[i].Earlyout.ToString("HH:mm");
                                status = EmployeeTimeList[i].Flag;
                                leav = EmployeeTimeList[i].Status21;
                                grad = EmployeeTimeList[i].GradeId;
                                shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                                temp = Convert.ToDateTime(txt_tdate.Text);
                                enddate = temp.ToString("yyyy/MM/dd");
                                if (intime != "00:00" && outtime != "00:00")
                                {
                                    inti = Convert.ToDateTime(intime);
                                    outi = Convert.ToDateTime(outtime);
                                    work_hrs = Convert.ToString(outi - inti);
                                }
                                else if (intime != "00:00")
                                {
                                    work_hrs = "Morning punch is missing";
                                }
                                else
                                {
                                    work_hrs = "evening punch is missing";
                                }
                                if (intime != "00:00" && outtime != "00:00")
                                {
                                    inti = Convert.ToDateTime(intime);
                                    outi = Convert.ToDateTime(outtime);
                                    work_hrs = Convert.ToString(outi - inti);
                                }
                                else if (intime != "00:00")
                                {
                                    work_hrs = "Morning punch is missing";
                                }
                                else
                                {
                                    work_hrs = "evening punch is missing";
                                }
                                query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,whours,status,Department,Leave_name,earlyout,pn_gradeID,To_Date,shift_code,Work_hrs) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[i].EmployeeCode + "', '" + EmployeeTimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + whours + "','" + status + "','" + dept + "','" + leav + "','" + earlyout + "'," + grad + ",'" + enddate + "','" + shift_code + "','" + work_hrs + "')";
                                employee.fn_reportbyid(query);
                                count++;
                            }
                        }
                    }
                }


                if (ddl_report.SelectedItem.Text == "Muster Roll")
                {
                    status = ""; stat = "";
                    prsdays = 0.0; leavedays = 0.0; odDays = 0.0; holidays = 0.0; weekoff = 0.0; Absdays = 0.0; paidDays = 0.0;
                    employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[ddl_i].Value);
                    employee.FirstName = chk_Empcode.Items[ddl_i].Text;
                    temp = Convert.ToDateTime(txt_date.Text);
                    dates = temp.ToString("MM/dd/yyyy");
                    employee.DurationFrom = txt_date.Text;
                    temp = Convert.ToDateTime(txt_tdate.Text);
                    enddate = temp.ToString("yyyy/MM/dd");
                    employee.DurationTo = temp.ToString("yyyy/MM/dd");
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
                        query = "insert into Temp_Muster values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[0].EmployeeCode + "', '" + EmployeeTimeList[0].FirstName + "','" + dates + "','" + enddate + "'," + status + ",'" + prsdays + "','" + leavedays + "','" + odDays + "','" + holidays + "','" + weekoff + "','" + Absdays + "','" + paidDays + "', " + EmployeeTimeList[0].GradeId + ") ";
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
                    if (ddl_ShiftList.SelectedItem.Text == "All")
                    {
                        EmployeeList = employee.fn_getPatternList(employee.BranchId);
                        if (EmployeeList.Count > 0)
                        {
                            for (int j = 0; j < EmployeeList.Count; j++)
                            {
                                employee.shiftcode = EmployeeList[j].ShiftName.ToString();
                                EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                                if (EmployeeTimeList.Count > 0)
                                {
                                    for (int i = 0; i < EmployeeTimeList.Count; i++)
                                    {
                                        dept = employee.fn_DepartmentName();
                                        intime = EmployeeTimeList[i].Intime.ToString("HH:mm");
                                        outtime = EmployeeTimeList[i].Outtime.ToString("HH:mm");
                                        Latein = EmployeeTimeList[i].Latein.ToString("HH:mm");
                                        Lateout = EmployeeTimeList[i].Lateout.ToString("HH:mm");
                                        earlyout = EmployeeTimeList[i].Earlyout.ToString("HH:mm");
                                        status = EmployeeTimeList[i].Flag;
                                        leav = EmployeeTimeList[i].Status21;
                                        grad = EmployeeTimeList[i].GradeId;
                                        temp = Convert.ToDateTime(txt_tdate.Text);
                                        enddate = temp.ToString("yyyy/MM/dd");
                                        shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                        dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                                        if (status == "XX" || status == "AX" || status == "XA" || status == "LX" || status == "XL" || status == "XD" || status == "DX")
                                        {
                                            query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,status,Department,Leave_name,earlyout,pn_gradeID,To_Date,shift_code) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[i].EmployeeCode + "', '" + EmployeeTimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "','" + earlyout + "'," + grad + ",'" + enddate + "','" + shift_code + "')";
                                            employee.fn_reportbyid(query);
                                            count++;
                                        }
                                    }

                                }
                            }
                        }
                    }
                    else
                    {
                        employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[ddl_i].Value);
                        employee.FirstName = chk_Empcode.Items[ddl_i].Text;
                        employee.shiftcode = ddl_ShiftList.SelectedItem.Text;
                        if (EmployeeTimeList.Count > 0)
                        {
                            for (int i = 0; i < EmployeeTimeList.Count; i++)
                            {
                                dept = employee.fn_DepartmentName();
                                intime = EmployeeTimeList[i].Intime.ToString("HH:mm");
                                outtime = EmployeeTimeList[i].Outtime.ToString("HH:mm");
                                Latein = EmployeeTimeList[i].Latein.ToString("HH:mm");
                                Lateout = EmployeeTimeList[i].Lateout.ToString("HH:mm");
                                earlyout = EmployeeTimeList[i].Earlyout.ToString("HH:mm");
                                status = EmployeeTimeList[i].Flag;
                                leav = EmployeeTimeList[i].Status21;
                                grad = EmployeeTimeList[i].GradeId;
                                temp = Convert.ToDateTime(txt_tdate.Text);
                                enddate = temp.ToString("yyyy/MM/dd");
                                shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                                if (status == "XX" || status == "AX" || status == "XA" || status == "LX" || status == "XL" || status == "XD" || status == "DX")
                                {
                                    query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,status,Department,Leave_name,earlyout,pn_gradeID,To_Date,shift_code) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[i].EmployeeCode + "', '" + EmployeeTimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "','" + earlyout + "'," + grad + ",'" + enddate + "','" + shift_code + "')";
                                    employee.fn_reportbyid(query);
                                    count++;
                                }
                            }

                        }
                    }
                }


                else if (ddl_report.SelectedItem.Text == "Absent")
                {
                    employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[ddl_i].Value);
                    employee.FirstName = chk_Empcode.Items[ddl_i].Text;
                    if (ddl_ShiftList.SelectedItem.Text == "All")
                    {
                        EmployeeList = employee.fn_getPatternList(employee.BranchId);
                        if (EmployeeList.Count > 0)
                        {

                            for (int j = 0; j < EmployeeList.Count; j++)
                            {
                                employee.shiftcode = EmployeeList[j].ShiftName.ToString();
                                EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                                if (EmployeeTimeList.Count > 0)
                                {
                                    for (int i = 0; i < EmployeeTimeList.Count; i++)
                                    {
                                        dept = employee.fn_DepartmentName();
                                        intime = EmployeeTimeList[i].Intime.ToString("HH:mm");
                                        outtime = EmployeeTimeList[i].Outtime.ToString("HH:mm");
                                        Latein = EmployeeTimeList[i].Latein.ToString("HH:mm");
                                        Lateout = EmployeeTimeList[i].Lateout.ToString("HH:mm");
                                        earlyout = EmployeeTimeList[i].Earlyout.ToString("HH:mm");
                                        status = EmployeeTimeList[i].Flag;
                                        leav = EmployeeTimeList[i].Status21;
                                        grad = EmployeeTimeList[i].GradeId;
                                        shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                        dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                                        temp = Convert.ToDateTime(txt_tdate.Text);
                                        enddate = temp.ToString("yyyy/MM/dd");
                                        if (status == "AA" || status == "AX" || status == "XA" || status == "LA" || status == "AL" || status == "AD" || status == "DA")
                                        {
                                            query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,status,Department,Leave_name,earlyout,pn_gradeID,To_Date,shift_code) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[i].EmployeeCode + "', '" + EmployeeTimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "','" + earlyout + "'," + grad + ",'" + enddate + "','" + shift_code + "')";
                                            employee.fn_reportbyid(query);
                                            count++;
                                        }
                                    }

                                }
                            }
                        }
                    }
                    else
                    {
                        employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[ddl_i].Value);
                        employee.FirstName = chk_Empcode.Items[ddl_i].Text;
                        employee.shiftcode = ddl_ShiftList.SelectedItem.Text;
                        EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                        if (EmployeeTimeList.Count > 0)
                        {
                            for (int i = 0; i < EmployeeTimeList.Count; i++)
                            {
                                dept = employee.fn_DepartmentName();
                                intime = EmployeeTimeList[i].Intime.ToString("HH:mm");
                                outtime = EmployeeTimeList[i].Outtime.ToString("HH:mm");
                                Latein = EmployeeTimeList[i].Latein.ToString("HH:mm");
                                Lateout = EmployeeTimeList[i].Lateout.ToString("HH:mm");
                                earlyout = EmployeeTimeList[i].Earlyout.ToString("HH:mm");
                                status = EmployeeTimeList[i].Flag;
                                leav = EmployeeTimeList[i].Status21;
                                grad = EmployeeTimeList[i].GradeId;
                                shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                                temp = Convert.ToDateTime(txt_tdate.Text);
                                enddate = temp.ToString("yyyy/MM/dd");
                                if (status == "AA" || status == "AX" || status == "XA" || status == "LA" || status == "AL" || status == "AD" || status == "DA")
                                {
                                    query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,status,Department,Leave_name,earlyout,pn_gradeID,To_Date,shift_code) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[i].EmployeeCode + "', '" + EmployeeTimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "','" + earlyout + "'," + grad + ",'" + enddate + "','" + shift_code + "')";
                                    employee.fn_reportbyid(query);
                                    count++;
                                }
                            }

                        }
                    }
                }

                else if (ddl_report.SelectedItem.Text == "Missing Staff")
                {
                    employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[ddl_i].Value);
                    employee.FirstName = chk_Empcode.Items[ddl_i].Text;

                    if (ddl_ShiftList.SelectedItem.Text == "All")
                    {
                        EmployeeList = employee.fn_getPatternList(employee.BranchId);
                        if (EmployeeList.Count > 0)
                        {

                            for (int j = 0; j < EmployeeList.Count; j++)
                            {
                                employee.shiftcode = EmployeeList[j].ShiftName.ToString();
                                EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                                if (EmployeeTimeList.Count > 0)
                                {
                                    for (int i = 0; i < EmployeeTimeList.Count; i++)
                                    {
                                        dept = employee.fn_DepartmentName();
                                        intime = EmployeeTimeList[i].Intime.ToString("HH:mm");
                                        outtime = EmployeeTimeList[i].Outtime.ToString("HH:mm");
                                        Latein = EmployeeTimeList[i].Latein.ToString("HH:mm");
                                        Lateout = EmployeeTimeList[i].Lateout.ToString("HH:mm");
                                        earlyout = EmployeeTimeList[i].Earlyout.ToString("HH:mm");
                                        status = EmployeeTimeList[i].Flag;
                                        leav = EmployeeTimeList[i].Status21;
                                        grad = EmployeeTimeList[i].GradeId;
                                        shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                        dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                                        temp = Convert.ToDateTime(txt_tdate.Text);
                                        enddate = temp.ToString("yyyy/MM/dd");
                                        //if (status == "AA" || status == "LL" || status == "DD" || status == "LA" || status == "AL" || status == "AD" || status == "DA")
                                        if (status == "AA")
                                        {
                                            query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,status,Department,Leave_name,earlyout,pn_gradeID,To_Date,shift_code) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[i].EmployeeCode + "', '" + EmployeeTimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "','" + earlyout + "'," + grad + ",'" + enddate + "','" + shift_code + "')";
                                            employee.fn_reportbyid(query);
                                            count++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[ddl_i].Value);
                        employee.FirstName = chk_Empcode.Items[ddl_i].Text;
                        employee.shiftcode = ddl_ShiftList.SelectedItem.Text;
                        EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                        if (EmployeeTimeList.Count > 0)
                        {
                            for (int i = 0; i < EmployeeTimeList.Count; i++)
                            {
                                dept = employee.fn_DepartmentName();
                                intime = EmployeeTimeList[i].Intime.ToString("HH:mm");
                                outtime = EmployeeTimeList[i].Outtime.ToString("HH:mm");
                                Latein = EmployeeTimeList[i].Latein.ToString("HH:mm");
                                Lateout = EmployeeTimeList[i].Lateout.ToString("HH:mm");
                                earlyout = EmployeeTimeList[i].Earlyout.ToString("HH:mm");
                                status = EmployeeTimeList[i].Flag;
                                leav = EmployeeTimeList[i].Status21;
                                grad = EmployeeTimeList[i].GradeId;
                                shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                                temp = Convert.ToDateTime(txt_tdate.Text);
                                enddate = temp.ToString("yyyy/MM/dd");
                                //if (status == "AA" || status == "LL" || status == "DD" || status == "LA" || status == "AL" || status == "AD" || status == "DA")
                                if (status == "AA")
                                {
                                    query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,status,Department,Leave_name,earlyout,pn_gradeID,To_Date,shift_code) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[i].EmployeeCode + "', '" + EmployeeTimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "','" + earlyout + "'," + grad + ",'" + enddate + "','" + shift_code + "')";
                                    employee.fn_reportbyid(query);
                                    count++;
                                }
                            }
                        }
                    }
                }

                else if (ddl_report.SelectedItem.Text == "Late in")
                {
                    employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[ddl_i].Value);
                    employee.FirstName = chk_Empcode.Items[ddl_i].Text;
                    employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[ddl_i].Value);
                    employee.FirstName = chk_Empcode.Items[ddl_i].Text;

                    if (ddl_ShiftList.SelectedItem.Text == "All")
                    {
                        EmployeeList = employee.fn_getPatternList(employee.BranchId);
                        if (EmployeeList.Count > 0)
                        {

                            for (int j = 0; j < EmployeeList.Count; j++)
                            {
                                employee.shiftcode = EmployeeList[j].ShiftName.ToString();
                                EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                                if (EmployeeTimeList.Count > 0)
                                {
                                    for (int i = 0; i < EmployeeTimeList.Count; i++)
                                    {
                                        dept = employee.fn_DepartmentName();
                                        intime = EmployeeTimeList[i].Intime.ToString("HH:mm");
                                        outtime = EmployeeTimeList[i].Outtime.ToString("HH:mm");
                                        Latein = EmployeeTimeList[i].Latein.ToString("HH:mm");
                                        Lateout = EmployeeTimeList[i].Lateout.ToString("HH:mm");
                                        earlyout = EmployeeTimeList[i].Earlyout.ToString("HH:mm");
                                        status = EmployeeTimeList[i].Flag;
                                        leav = EmployeeTimeList[i].Status21;
                                        grad = EmployeeTimeList[i].GradeId;
                                        shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                        dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                                        temp = Convert.ToDateTime(txt_tdate.Text);
                                        enddate = temp.ToString("yyyy/MM/dd");
                                        //enddate = txt_tdate.Text;
                                        if (Latein != "00:00")
                                        {
                                            query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,status,Department,Leave_name,earlyout,pn_gradeID,To_Date,shift_code) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[i].EmployeeCode + "', '" + EmployeeTimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "','" + earlyout + "'," + grad + ",'" + enddate + "','" + shift_code + "')";
                                            employee.fn_reportbyid(query);
                                            count++;
                                        }

                                    }

                                }
                            }
                        }
                    }
                    else
                    {
                        employee.shiftcode = ddl_ShiftList.SelectedItem.Text;
                        EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                        if (EmployeeTimeList.Count > 0)
                        {
                            for (int i = 0; i < EmployeeTimeList.Count; i++)
                            {
                                dept = employee.fn_DepartmentName();
                                intime = EmployeeTimeList[i].Intime.ToString("HH:mm");
                                outtime = EmployeeTimeList[i].Outtime.ToString("HH:mm");
                                Latein = EmployeeTimeList[i].Latein.ToString("HH:mm");
                                Lateout = EmployeeTimeList[i].Lateout.ToString("HH:mm");
                                earlyout = EmployeeTimeList[i].Earlyout.ToString("HH:mm");
                                status = EmployeeTimeList[i].Flag;
                                leav = EmployeeTimeList[i].Status21;
                                grad = EmployeeTimeList[i].GradeId;
                                shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                                temp = Convert.ToDateTime(txt_tdate.Text);
                                enddate = temp.ToString("yyyy/MM/dd");
                                //enddate = txt_tdate.Text;
                                if (Latein != "00:00")
                                {
                                    query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,status,Department,Leave_name,earlyout,pn_gradeID,To_Date,shift_code) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[i].EmployeeCode + "', '" + EmployeeTimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "','" + earlyout + "'," + grad + ",'" + enddate + "','" + shift_code + "')";
                                    employee.fn_reportbyid(query);
                                    count++;
                                }

                            }

                        }
                    }
                }

                else if (ddl_report.SelectedItem.Text == "Leave")
                {
                    employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[ddl_i].Value);
                    employee.FirstName = chk_Empcode.Items[ddl_i].Text;
                    if (ddl_ShiftList.SelectedItem.Text == "All")
                    {
                        EmployeeList = employee.fn_getPatternList(employee.BranchId);
                        if (EmployeeList.Count > 0)
                        {

                            for (int j = 0; j < EmployeeList.Count; j++)
                            {
                                employee.shiftcode = EmployeeList[j].ShiftName.ToString();
                                EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                                if (EmployeeTimeList.Count > 0)
                                {
                                    for (int i = 0; i < EmployeeTimeList.Count; i++)
                                    {
                                        dept = employee.fn_DepartmentName();
                                        intime = EmployeeTimeList[i].Intime.ToString("HH:mm");
                                        outtime = EmployeeTimeList[i].Outtime.ToString("HH:mm");
                                        Latein = EmployeeTimeList[i].Latein.ToString("HH:mm");
                                        Lateout = EmployeeTimeList[i].Lateout.ToString("HH:mm");
                                        earlyout = EmployeeTimeList[i].Earlyout.ToString("HH:mm");
                                        status = EmployeeTimeList[i].Flag;
                                        leav = EmployeeTimeList[i].Status21;
                                        grad = EmployeeTimeList[i].GradeId;
                                        shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                        dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                                        temp = Convert.ToDateTime(txt_tdate.Text);
                                        enddate = temp.ToString("yyyy/MM/dd");
                                        if (status == "LL" || status == "XL" || status == "LX" || status == "AL" || status == "LA" || status == "DL" || status == "LD")
                                        {
                                            query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,status,Department,Leave_name,earlyout,pn_gradeID,To_Date,shift_code) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[i].EmployeeCode + "', '" + EmployeeTimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "','" + earlyout + "'," + grad + ",'" + enddate + "','" + shift_code + "')";
                                            employee.fn_reportbyid(query);
                                            count++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        employee.shiftcode = ddl_ShiftList.SelectedItem.Text;
                        EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                        if (EmployeeTimeList.Count > 0)
                        {
                            for (int i = 0; i < EmployeeTimeList.Count; i++)
                            {
                                dept = employee.fn_DepartmentName();
                                intime = EmployeeTimeList[i].Intime.ToString("HH:mm");
                                outtime = EmployeeTimeList[i].Outtime.ToString("HH:mm");
                                Latein = EmployeeTimeList[i].Latein.ToString("HH:mm");
                                Lateout = EmployeeTimeList[i].Lateout.ToString("HH:mm");
                                earlyout = EmployeeTimeList[i].Earlyout.ToString("HH:mm");
                                status = EmployeeTimeList[i].Flag;
                                leav = EmployeeTimeList[i].Status21;
                                grad = EmployeeTimeList[i].GradeId;
                                shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                                temp = Convert.ToDateTime(txt_tdate.Text);
                                enddate = temp.ToString("yyyy/MM/dd");
                                if (status == "LL" || status == "XL" || status == "LX" || status == "AL" || status == "LA" || status == "DL" || status == "LD")
                                {
                                    query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,status,Department,Leave_name,earlyout,pn_gradeID,To_Date,shift_code) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[i].EmployeeCode + "', '" + EmployeeTimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "','" + earlyout + "'," + grad + ",'" + enddate + "','" + shift_code + "')";
                                    employee.fn_reportbyid(query);
                                    count++;
                                }
                            }
                        }
                    }
                }
                else if (ddl_report.SelectedItem.Text == "On Duty")
                {
                    employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[ddl_i].Value);
                    employee.FirstName = chk_Empcode.Items[ddl_i].Text;
                    if (ddl_ShiftList.SelectedItem.Text == "All")
                    {
                        EmployeeList = employee.fn_getPatternList(employee.BranchId);
                        if (EmployeeList.Count > 0)
                        {

                            for (int j = 0; j < EmployeeList.Count; j++)
                            {
                                employee.shiftcode = EmployeeList[j].ShiftName.ToString();
                                EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                                if (EmployeeTimeList.Count > 0)
                                {
                                    for (int i = 0; i < EmployeeTimeList.Count; i++)
                                    {
                                        dept = employee.fn_DepartmentName();
                                        intime = EmployeeTimeList[i].Intime.ToString("HH:mm");
                                        outtime = EmployeeTimeList[i].Outtime.ToString("HH:mm");
                                        Latein = EmployeeTimeList[i].Latein.ToString("HH:mm");
                                        Lateout = EmployeeTimeList[i].Lateout.ToString("HH:mm");
                                        earlyout = EmployeeTimeList[i].Earlyout.ToString("HH:mm");
                                        status = EmployeeTimeList[i].Flag;
                                        leav = EmployeeTimeList[i].Status21;
                                        shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                        grad = EmployeeTimeList[i].GradeId;
                                        dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                                        temp = Convert.ToDateTime(txt_tdate.Text);
                                        enddate = temp.ToString("yyyy/MM/dd");
                                        if (status == "DD" || status == "XD" || status == "DX" || status == "AD" || status == "DA" || status == "DL" || status == "LD")
                                        {
                                            query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,status,Department,Leave_name,earlyout,pn_gradeID,To_Date,shift_code) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[i].EmployeeCode + "', '" + EmployeeTimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "','" + earlyout + "'," + grad + ",'" + enddate + "','" + shift_code + "')";
                                            employee.fn_reportbyid(query);
                                            count++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {


                        employee.shiftcode = ddl_ShiftList.SelectedItem.Text;
                        EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                        if (EmployeeTimeList.Count > 0)
                        {
                            for (int i = 0; i < EmployeeTimeList.Count; i++)
                            {
                                dept = employee.fn_DepartmentName();
                                intime = EmployeeTimeList[i].Intime.ToString("HH:mm");
                                outtime = EmployeeTimeList[i].Outtime.ToString("HH:mm");
                                Latein = EmployeeTimeList[i].Latein.ToString("HH:mm");
                                Lateout = EmployeeTimeList[i].Lateout.ToString("HH:mm");
                                earlyout = EmployeeTimeList[i].Earlyout.ToString("HH:mm");
                                status = EmployeeTimeList[i].Flag;
                                leav = EmployeeTimeList[i].Status21;
                                shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                grad = EmployeeTimeList[i].GradeId;
                                dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                                temp = Convert.ToDateTime(txt_tdate.Text);
                                enddate = temp.ToString("yyyy/MM/dd");
                                if (status == "DD" || status == "XD" || status == "DX" || status == "AD" || status == "DA" || status == "DL" || status == "LD")
                                {
                                    query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,status,Department,Leave_name,earlyout,pn_gradeID,To_Date,shift_code) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[i].EmployeeCode + "', '" + EmployeeTimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "','" + earlyout + "'," + grad + ",'" + enddate + "','" + shift_code + "')";
                                    employee.fn_reportbyid(query);
                                    count++;
                                }
                            }
                        }
                    }
                }
                else if (ddl_report.SelectedItem.Text == "Morning Attendance")
                {
                    employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[ddl_i].Value);
                    employee.FirstName = chk_Empcode.Items[ddl_i].Text;
                    if (ddl_ShiftList.SelectedItem.Text == "All")
                    {
                        EmployeeList = employee.fn_getPatternList(employee.BranchId);
                        if (EmployeeList.Count > 0)
                        {

                            for (int j = 0; j < EmployeeList.Count; j++)
                            {
                                employee.shiftcode = EmployeeList[j].ShiftName.ToString();
                                EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                                if (EmployeeTimeList.Count > 0)
                                {
                                    for (int i = 0; i < EmployeeTimeList.Count; i++)
                                    {
                                        dept = employee.fn_DepartmentName();
                                        intime = EmployeeTimeList[i].Intime.ToString("HH:mm");
                                        outtime = EmployeeTimeList[i].Outtime.ToString("HH:mm");
                                        Latein = EmployeeTimeList[i].Latein.ToString("HH:mm");
                                        Lateout = EmployeeTimeList[i].Lateout.ToString("HH:mm");
                                        status = EmployeeTimeList[i].Flag;
                                        leav = EmployeeTimeList[i].Status21;
                                        grad = EmployeeTimeList[i].GradeId;
                                        shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                        dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                                        temp = Convert.ToDateTime(txt_tdate.Text);
                                        enddate = temp.ToString("yyyy/MM/dd");
                                        query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,status,Department,Leave_name,earlyout,pn_gradeID,To_Date,shift_code) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[i].EmployeeCode + "', '" + EmployeeTimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','',''," + grad + ",'" + enddate + "','" + shift_code + "')";
                                        employee.fn_reportbyid(query);
                                        count++;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        employee.shiftcode = ddl_ShiftList.SelectedItem.Text;
                        EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                        if (EmployeeTimeList.Count > 0)
                        {
                            for (int i = 0; i < EmployeeTimeList.Count; i++)
                            {
                                dept = employee.fn_DepartmentName();
                                intime = EmployeeTimeList[i].Intime.ToString("HH:mm");
                                outtime = EmployeeTimeList[i].Outtime.ToString("HH:mm");
                                Latein = EmployeeTimeList[i].Latein.ToString("HH:mm");
                                Lateout = EmployeeTimeList[i].Lateout.ToString("HH:mm");
                                status = EmployeeTimeList[i].Flag;
                                leav = EmployeeTimeList[i].Status21;
                                grad = EmployeeTimeList[i].GradeId;
                                shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                                temp = Convert.ToDateTime(txt_tdate.Text);
                                enddate = temp.ToString("yyyy/MM/dd");
                                query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,status,Department,Leave_name,earlyout,pn_gradeID,To_Date,shift_code) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[i].EmployeeCode + "', '" + EmployeeTimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','',''," + grad + ",'" + enddate + "','" + shift_code + "')";
                                employee.fn_reportbyid(query);
                                count++;
                            }
                        }
                    }
                }
                else if (ddl_report.SelectedItem.Text == "Consolidate")
                {
                    employee.DurationFrom = employee.Convert_ToSqlDatestring(txt_date.Text);
                    employee.DurationTo = employee.Convert_ToSqlDatestring(txt_tdate.Text);
                    employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[ddl_i].Value);
                    employee.FirstName = chk_Empcode.Items[ddl_i].Text;


                    if (ddl_ShiftList.SelectedItem.Text == "All")
                    {
                        EmployeeList = employee.fn_getPatternList(employee.BranchId);
                        if (EmployeeList.Count > 0)
                        {

                            for (int j = 0; j < EmployeeList.Count; j++)
                            {
                                employee.shiftcode = EmployeeList[j].ShiftName.ToString();
                                EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                                if (EmployeeTimeList.Count > 0)
                                {
                                    for (int i = 0; i < EmployeeTimeList.Count; i++)
                                    {
                                        dept = employee.fn_DepartmentName();
                                        date = EmployeeTimeList[i].Date.ToString();
                                        intime = EmployeeTimeList[i].Intime.ToString("HH:mm");
                                        outtime = EmployeeTimeList[i].Outtime.ToString("HH:mm");
                                        Latein = EmployeeTimeList[i].Latein.ToString("HH:mm");
                                        Lateout = EmployeeTimeList[i].Lateout.ToString("HH:mm");
                                        status = EmployeeTimeList[i].Flag;
                                        leav = EmployeeTimeList[i].Status21;
                                        grad = EmployeeTimeList[i].GradeId;
                                        shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                        dates = EmployeeTimeList[i].Dates.ToString("dd/MM/yyyy");
                                        //dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                                        temp = Convert.ToDateTime(txt_tdate.Text);
                                        enddate = temp.ToString("yyyy/MM/dd");
                                        query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,status,Department,Leave_name,earlyout,pn_gradeID,To_Date,shift_code) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[i].EmployeeCode + "', '" + EmployeeTimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "',''," + grad + ",'" + enddate + "','" + shift_code + "')";
                                        employee.fn_reportbyid(query);
                                        count++;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        employee.shiftcode = ddl_ShiftList.SelectedItem.Text;
                        EmployeeTimeList = employee.fn_EmployeeConsolidate(employee);
                        if (EmployeeTimeList.Count > 0)
                        {
                            for (int i = 0; i < EmployeeTimeList.Count; i++)
                            {
                                dept = employee.fn_DepartmentName();
                                date = EmployeeTimeList[i].Date.ToString();
                                intime = EmployeeTimeList[i].Intime.ToString("HH:mm");
                                outtime = EmployeeTimeList[i].Outtime.ToString("HH:mm");
                                Latein = EmployeeTimeList[i].Latein.ToString("HH:mm");
                                Lateout = EmployeeTimeList[i].Lateout.ToString("HH:mm");
                                status = EmployeeTimeList[i].Flag;
                                leav = EmployeeTimeList[i].Status21;
                                grad = EmployeeTimeList[i].GradeId;
                                shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                dates = EmployeeTimeList[i].Dates.ToString("dd/MM/yyyy");
                                //dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                                temp = Convert.ToDateTime(txt_tdate.Text);
                                enddate = temp.ToString("yyyy/MM/dd");
                                query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,status,Department,Leave_name,earlyout,pn_gradeID,To_Date,shift_code) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[i].EmployeeCode + "', '" + EmployeeTimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "',''," + grad + ",'" + enddate + "','" + shift_code + "')";
                                employee.fn_reportbyid(query);
                                count++;
                            }
                        }
                    }
                }

                else if (ddl_report.SelectedItem.Text == "OT report")
                {
                    employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[ddl_i].Value);
                    employee.FirstName = chk_Empcode.Items[ddl_i].Text;
                    if (ddl_ShiftList.SelectedItem.Text == "All")
                    {
                        EmployeeList = employee.fn_getPatternList(employee.BranchId);
                        if (EmployeeList.Count > 0)
                        {

                            for (int j = 0; j < EmployeeList.Count; j++)
                            {
                                employee.shiftcode = EmployeeList[j].ShiftName.ToString();
                                EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                                if (EmployeeTimeList.Count > 0)
                                {
                                    for (int i = 0; i < EmployeeTimeList.Count; i++)
                                    {
                                        dept = employee.fn_DepartmentName();
                                        intime = EmployeeTimeList[i].Intime.ToString("HH:mm");
                                        outtime = EmployeeTimeList[i].Outtime.ToString("HH:mm");
                                        Latein = EmployeeTimeList[i].Latein.ToString("HH:mm");
                                        Lateout = EmployeeTimeList[i].Lateout.ToString("HH:mm");
                                        whours = EmployeeTimeList[i].whours.ToString("HH:mm");
                                        status = EmployeeTimeList[i].Flag;
                                        leav = EmployeeTimeList[i].Status21;
                                        grad = EmployeeTimeList[i].GradeId;
                                        shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                        dates = EmployeeTimeList[i].dates.ToString("dd/MM/yyyy");
                                        temp = Convert.ToDateTime(txt_tdate.Text);
                                        enddate = temp.ToString("yyyy/MM/dd");

                                        if (whours != "00:00")
                                        {
                                            query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,whours,status,Department,Leave_name,earlyout,pn_gradeID,To_Date,shift_code) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[i].EmployeeCode + "', '" + EmployeeTimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + whours + "','" + status + "','" + dept + "','',''," + grad + ",'" + enddate + "','" + shift_code + "')";
                                            employee.fn_reportbyid(query);
                                            count++;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    else
                    {
                        employee.shiftcode = ddl_ShiftList.SelectedItem.Text;
                        EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                        if (EmployeeTimeList.Count > 0)
                        {
                            for (int i = 0; i < EmployeeTimeList.Count; i++)
                            {
                                dept = employee.fn_DepartmentName();
                                intime = EmployeeTimeList[i].Intime.ToString("HH:mm");
                                outtime = EmployeeTimeList[i].Outtime.ToString("HH:mm");
                                Latein = EmployeeTimeList[i].Latein.ToString("HH:mm");
                                Lateout = EmployeeTimeList[i].Lateout.ToString("HH:mm");
                                whours = EmployeeTimeList[i].whours.ToString("HH:mm");
                                status = EmployeeTimeList[i].Flag;
                                leav = EmployeeTimeList[i].Status21;
                                grad = EmployeeTimeList[i].GradeId;
                                shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                dates = EmployeeTimeList[i].dates.ToString("dd/MM/yyyy");
                                temp = Convert.ToDateTime(txt_tdate.Text);
                                enddate = temp.ToString("yyyy/MM/dd");

                                if (whours != "00:00")
                                {
                                    query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,whours,status,Department,Leave_name,earlyout,pn_gradeID,To_Date,shift_code) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[i].EmployeeCode + "', '" + EmployeeTimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + whours + "','" + status + "','" + dept + "','',''," + grad + ",'" + enddate + "','" + shift_code + "')";
                                    employee.fn_reportbyid(query);
                                    count++;
                                }
                            }
                        }
                    }
                }
                else if (ddl_report.SelectedItem.Text == "Employee Shift details by date wise")
                {
                    employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[ddl_i].Value);
                    employee.FirstName = chk_Empcode.Items[ddl_i].Text;
                    if (ddl_ShiftList.SelectedItem.Text == "All")
                    {
                        EmployeeList = employee.fn_getPatternList(employee.BranchId);
                        if (EmployeeList.Count > 0)
                        {

                            for (int j = 0; j < EmployeeList.Count; j++)
                            {
                                employee.shiftcode = EmployeeList[j].ShiftName.ToString();
                                EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                                if (EmployeeTimeList.Count > 0)
                                {
                                    for (int i = 0; i < EmployeeTimeList.Count; i++)
                                    {
                                        dept = employee.fn_DepartmentName();
                                        // dept = ddl_Departmentlist.SelectedItem.Text;
                                        shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                        dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                                        intime = EmployeeTimeList[i].Intime.ToString("HH:mm");
                                        outtime = EmployeeTimeList[i].Outtime.ToString("HH:mm");
                                        Latein = EmployeeTimeList[i].Latein.ToString("HH:mm");
                                        Lateout = EmployeeTimeList[i].Lateout.ToString("HH:mm");
                                        whours = EmployeeTimeList[i].whours.ToString("HH:mm");
                                        status = EmployeeTimeList[i].Flag;
                                        leav = EmployeeTimeList[i].Status21;
                                        grad = EmployeeTimeList[i].GradeId;
                                        shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                        temp = Convert.ToDateTime(txt_tdate.Text);
                                        enddate = temp.ToString("yyyy/MM/dd");
                                        //employee.fn_reportbyid(query);
                                        query = "insert into tempshiftdetails(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,shift_code,Department,To_Date) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[i].EmployeeCode + "', '" + EmployeeTimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + shift_code + "','" + dept + "','" + enddate + "')";
                                        employee.fn_reportbyid(query);
                                        count++;

                                    }
                                }
                            }
                        }
                    }
                    else
                    {


                        employee.shiftcode = ddl_ShiftList.SelectedItem.Text;
                        EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                        if (EmployeeTimeList.Count > 0)
                        {
                            for (int i = 0; i < EmployeeTimeList.Count; i++)
                            {
                                dept = employee.fn_DepartmentName();
                                // dept = ddl_Departmentlist.SelectedItem.Text;
                                shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                                intime = EmployeeTimeList[i].Intime.ToString("HH:mm");
                                outtime = EmployeeTimeList[i].Outtime.ToString("HH:mm");
                                Latein = EmployeeTimeList[i].Latein.ToString("HH:mm");
                                Lateout = EmployeeTimeList[i].Lateout.ToString("HH:mm");
                                whours = EmployeeTimeList[i].whours.ToString("HH:mm");
                                status = EmployeeTimeList[i].Flag;
                                leav = EmployeeTimeList[i].Status21;
                                grad = EmployeeTimeList[i].GradeId;
                                shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                temp = Convert.ToDateTime(txt_tdate.Text);
                                enddate = temp.ToString("yyyy/MM/dd");
                                //employee.fn_reportbyid(query);
                                query = "insert into tempshiftdetails(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,shift_code,Department,To_Date) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[i].EmployeeCode + "', '" + EmployeeTimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + shift_code + "','" + dept + "','" + enddate + "')";
                                employee.fn_reportbyid(query);
                                count++;

                            }
                        }
                    }
                }
                else if (ddl_report.SelectedItem.Text == "Frequently Late")
                {
                    employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[ddl_i].Value);
                    employee.FirstName = chk_Empcode.Items[ddl_i].Text;
                    if (ddl_ShiftList.SelectedItem.Text == "All")
                    {
                        EmployeeList = employee.fn_getPatternList(employee.BranchId);
                        if (EmployeeList.Count > 0)
                        {

                            for (int j = 0; j < EmployeeList.Count; j++)
                            {
                                employee.shiftcode = EmployeeList[j].ShiftName.ToString();
                                EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                                if (EmployeeTimeList.Count > 0)
                                {
                                    for (int i = 0; i < EmployeeTimeList.Count; i++)
                                    {
                                        dept = employee.fn_DepartmentName();
                                        intime = EmployeeTimeList[i].Intime.ToString("HH:mm");
                                        outtime = EmployeeTimeList[i].Outtime.ToString("HH:mm");
                                        Latein = EmployeeTimeList[i].Latein.ToString("HH:mm");
                                        Lateout = EmployeeTimeList[i].Lateout.ToString("HH:mm");
                                        earlyout = EmployeeTimeList[i].Earlyout.ToString("HH:mm");
                                        status = EmployeeTimeList[i].Flag;
                                        leav = EmployeeTimeList[i].Status21;
                                        shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                        grad = EmployeeTimeList[i].GradeId;
                                        dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                                        temp = Convert.ToDateTime(txt_tdate.Text);
                                        enddate = temp.ToString("yyyy/MM/dd");
                                        if (Latein != "00:00")
                                        {
                                            query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,status,Department,Leave_name,earlyout,pn_gradeID,To_Date,shift_code) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[i].EmployeeCode + "', '" + EmployeeTimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "','" + earlyout + "'," + grad + ",'" + enddate + "','" + shift_code + "')";
                                            employee.fn_reportbyid(query);
                                            count++;
                                        }

                                    }
                                    if (count >= 1)
                                    {
                                        //query = "select count(*) from tempsattendance";
                                        //int c = employee.fn_RowCount(query);
                                        //for (int i = 0; i < c; i++)
                                        //{
                                        query = "select count(*) from tempsattendance where registerno='" + EmployeeTimeList[i].EmployeeCode + "'";
                                        int s = employee.fn_RowCount(query);
                                        int Get_value = Convert.ToInt32(TextBox1.Text);
                                        if (Get_value >= s)
                                        {
                                            query = "delete from tempsattendance where registerno='" + EmployeeTimeList[i].EmployeeCode + "' ";
                                            employee.fn_reportbyid(query);
                                        }
                                        //}
                                        query = "select count(*) from tempsattendance";
                                        int Final_count = employee.fn_RowCount(query);
                                        if (Final_count == 0)
                                        {
                                            count = 0;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        employee.shiftcode = ddl_ShiftList.SelectedItem.Text;
                        EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                        if (EmployeeTimeList.Count > 0)
                        {
                            for (int i = 0; i < EmployeeTimeList.Count; i++)
                            {
                                dept = employee.fn_DepartmentName();
                                intime = EmployeeTimeList[i].Intime.ToString("HH:mm");
                                outtime = EmployeeTimeList[i].Outtime.ToString("HH:mm");
                                Latein = EmployeeTimeList[i].Latein.ToString("HH:mm");
                                Lateout = EmployeeTimeList[i].Lateout.ToString("HH:mm");
                                earlyout = EmployeeTimeList[i].Earlyout.ToString("HH:mm");
                                status = EmployeeTimeList[i].Flag;
                                leav = EmployeeTimeList[i].Status21;
                                shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                grad = EmployeeTimeList[i].GradeId;
                                dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                                temp = Convert.ToDateTime(txt_tdate.Text);
                                enddate = temp.ToString("yyyy/MM/dd");
                                if (Latein != "00:00")
                                {
                                    query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,status,Department,Leave_name,earlyout,pn_gradeID,To_Date,shift_code) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[i].EmployeeCode + "', '" + EmployeeTimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "','" + earlyout + "'," + grad + ",'" + enddate + "','" + shift_code + "')";
                                    employee.fn_reportbyid(query);
                                    count++;
                                }

                            }
                            if (count >= 1)
                            {
                                //query = "select count(*) from tempsattendance";
                                //int c = employee.fn_RowCount(query);
                                //for (int i = 0; i < c; i++)
                                //{
                                query = "select count(*) from tempsattendance where registerno='" + EmployeeTimeList[i].EmployeeCode + "'";
                                int s = employee.fn_RowCount(query);
                                int Get_value = Convert.ToInt32(TextBox1.Text);
                                if (Get_value >= s)
                                {
                                    query = "delete from tempsattendance where registerno='" + EmployeeTimeList[i].EmployeeCode + "' ";
                                    employee.fn_reportbyid(query);
                                }
                                //}
                                query = "select count(*) from tempsattendance";
                                int Final_count = employee.fn_RowCount(query);
                                if (Final_count == 0)
                                {
                                    count = 0;
                                }
                            }
                        }

                    }
                }
                else if (ddl_report.SelectedItem.Text == "Frequent Early leaving")
                {
                    employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[ddl_i].Value);
                    employee.FirstName = chk_Empcode.Items[ddl_i].Text;
                    if (ddl_ShiftList.SelectedItem.Text == "All")
                    {
                        EmployeeList = employee.fn_getPatternList(employee.BranchId);
                        if (EmployeeList.Count > 0)
                        {

                            for (int j = 0; j < EmployeeList.Count; j++)
                            {
                                employee.shiftcode = EmployeeList[j].ShiftName.ToString();
                                EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                                if (EmployeeTimeList.Count > 0)
                                {
                                    for (int i = 0; i < EmployeeTimeList.Count; i++)
                                    {
                                        dept = employee.fn_DepartmentName();
                                        intime = EmployeeTimeList[i].Intime.ToString("HH:mm");
                                        outtime = EmployeeTimeList[i].Outtime.ToString("HH:mm");
                                        Latein = EmployeeTimeList[i].Latein.ToString("HH:mm");
                                        Lateout = EmployeeTimeList[i].Lateout.ToString("HH:mm");
                                        earlyout = EmployeeTimeList[i].Earlyout.ToString("HH:mm");
                                        status = EmployeeTimeList[i].Flag;
                                        leav = EmployeeTimeList[i].Status21;
                                        grad = EmployeeTimeList[i].GradeId;
                                        shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                        dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                                        temp = Convert.ToDateTime(txt_tdate.Text);
                                        enddate = temp.ToString("yyyy/MM/dd");
                                        if (earlyout != "00:00")
                                        {
                                            query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,status,Department,Leave_name,earlyout,pn_gradeID,To_Date,shift_code) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[i].EmployeeCode + "', '" + EmployeeTimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "','" + earlyout + "'," + grad + ",'" + enddate + "','" + shift_code + "')";
                                            employee.fn_reportbyid(query);
                                            count++;
                                        }

                                    }
                                    if (count >= 1)
                                    {
                                        //query = "select count(*) from tempsattendance";
                                        //int c = employee.fn_RowCount(query);
                                        //for (int i = 0; i < c; i++)
                                        //{
                                        query = "select count(*) from tempsattendance where registerno='" + EmployeeTimeList[i].EmployeeCode + "'";
                                        int s = employee.fn_RowCount(query);
                                        int Get_value = Convert.ToInt32(TextBox1.Text);
                                        if (Get_value >= s)
                                        {
                                            query = "delete from tempsattendance where registerno='" + EmployeeTimeList[i].EmployeeCode + "' ";
                                            employee.fn_reportbyid(query);
                                        }
                                        //}
                                        query = "select count(*) from tempsattendance";
                                        int Final_count = employee.fn_RowCount(query);
                                        if (Final_count == 0)
                                        {
                                            count = 0;
                                        }

                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        employee.shiftcode = ddl_ShiftList.SelectedItem.Text;
                        EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                        if (EmployeeTimeList.Count > 0)
                        {
                            for (int i = 0; i < EmployeeTimeList.Count; i++)
                            {
                                dept = employee.fn_DepartmentName();
                                intime = EmployeeTimeList[i].Intime.ToString("HH:mm");
                                outtime = EmployeeTimeList[i].Outtime.ToString("HH:mm");
                                Latein = EmployeeTimeList[i].Latein.ToString("HH:mm");
                                Lateout = EmployeeTimeList[i].Lateout.ToString("HH:mm");
                                earlyout = EmployeeTimeList[i].Earlyout.ToString("HH:mm");
                                status = EmployeeTimeList[i].Flag;
                                leav = EmployeeTimeList[i].Status21;
                                grad = EmployeeTimeList[i].GradeId;
                                shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                                temp = Convert.ToDateTime(txt_tdate.Text);
                                enddate = temp.ToString("yyyy/MM/dd");
                                if (earlyout != "00:00")
                                {
                                    query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,status,Department,Leave_name,earlyout,pn_gradeID,To_Date,shift_code) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[i].EmployeeCode + "', '" + EmployeeTimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "','" + earlyout + "'," + grad + ",'" + enddate + "','" + shift_code + "')";
                                    employee.fn_reportbyid(query);
                                    count++;
                                }

                            }
                            if (count >= 1)
                            {
                                //query = "select count(*) from tempsattendance";
                                //int c = employee.fn_RowCount(query);
                                //for (int i = 0; i < c; i++)
                                //{
                                query = "select count(*) from tempsattendance where registerno='" + EmployeeTimeList[i].EmployeeCode + "'";
                                int s = employee.fn_RowCount(query);
                                int Get_value = Convert.ToInt32(TextBox1.Text);
                                if (Get_value >= s)
                                {
                                    query = "delete from tempsattendance where registerno='" + EmployeeTimeList[i].EmployeeCode + "' ";
                                    employee.fn_reportbyid(query);
                                }
                                //}
                                query = "select count(*) from tempsattendance";
                                int Final_count = employee.fn_RowCount(query);
                                if (Final_count == 0)
                                {
                                    count = 0;
                                }

                            }
                        }
                    }
                }


                else if (ddl_report.SelectedItem.Text == "Frequent Absentees")
                {
                    employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[ddl_i].Value);
                    employee.FirstName = chk_Empcode.Items[ddl_i].Text;
                    if (ddl_ShiftList.SelectedItem.Text == "All")
                    {
                        EmployeeList = employee.fn_getPatternList(employee.BranchId);
                        if (EmployeeList.Count > 0)
                        {

                            for (int j = 0; j < EmployeeList.Count; j++)
                            {
                                employee.shiftcode = EmployeeList[j].ShiftName.ToString();
                                EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                                if (EmployeeTimeList.Count > 0)
                                {
                                    for (int i = 0; i < EmployeeTimeList.Count; i++)
                                    {
                                        dept = employee.fn_DepartmentName();
                                        intime = EmployeeTimeList[i].Intime.ToString("HH:mm");
                                        outtime = EmployeeTimeList[i].Outtime.ToString("HH:mm");
                                        Latein = EmployeeTimeList[i].Latein.ToString("HH:mm");
                                        Lateout = EmployeeTimeList[i].Lateout.ToString("HH:mm");
                                        earlyout = EmployeeTimeList[i].Earlyout.ToString("HH:mm");
                                        status = EmployeeTimeList[i].Flag;
                                        leav = EmployeeTimeList[i].Status21;
                                        shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                        grad = EmployeeTimeList[i].GradeId;
                                        dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                                        temp = Convert.ToDateTime(txt_tdate.Text);
                                        enddate = temp.ToString("yyyy/MM/dd");
                                        if (status == "AA" || status == "AX" || status == "XA" || status == "LA" || status == "AL" || status == "AD" || status == "DA")
                                        {
                                            query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,status,Department,Leave_name,earlyout,pn_gradeID,To_Date,shift_code) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[i].EmployeeCode + "', '" + EmployeeTimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "','" + earlyout + "'," + grad + ",'" + enddate + "','" + shift_code + "')";
                                            employee.fn_reportbyid(query);
                                            count++;
                                        }
                                    }
                                    if (count >= 1)
                                    {
                                        //query = "select count(*) from tempsattendance";
                                        //int c = employee.fn_RowCount(query);
                                        //for (int i = 0; i < c; i++)
                                        //{
                                        query = "select count(*) from tempsattendance where registerno='" + EmployeeTimeList[i].EmployeeCode + "'";
                                        int s = employee.fn_RowCount(query);
                                        int Get_value = Convert.ToInt32(TextBox1.Text);
                                        if (Get_value >= s)
                                        {
                                            query = "delete from tempsattendance where registerno='" + EmployeeTimeList[i].EmployeeCode + "' ";
                                            employee.fn_reportbyid(query);
                                        }
                                        //}
                                        query = "select count(*) from tempsattendance";
                                        int Final_count = employee.fn_RowCount(query);
                                        if (Final_count == 0)
                                        {
                                            count = 0;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        employee.shiftcode = ddl_ShiftList.SelectedItem.Text;
                        EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                        if (EmployeeTimeList.Count > 0)
                        {
                            for (int i = 0; i < EmployeeTimeList.Count; i++)
                            {
                                dept = employee.fn_DepartmentName();
                                intime = EmployeeTimeList[i].Intime.ToString("HH:mm");
                                outtime = EmployeeTimeList[i].Outtime.ToString("HH:mm");
                                Latein = EmployeeTimeList[i].Latein.ToString("HH:mm");
                                Lateout = EmployeeTimeList[i].Lateout.ToString("HH:mm");
                                earlyout = EmployeeTimeList[i].Earlyout.ToString("HH:mm");
                                status = EmployeeTimeList[i].Flag;
                                leav = EmployeeTimeList[i].Status21;
                                shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                grad = EmployeeTimeList[i].GradeId;
                                dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                                temp = Convert.ToDateTime(txt_tdate.Text);
                                enddate = temp.ToString("yyyy/MM/dd");
                                if (status == "AA" || status == "AX" || status == "XA" || status == "LA" || status == "AL" || status == "AD" || status == "DA")
                                {
                                    query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,status,Department,Leave_name,earlyout,pn_gradeID,To_Date,shift_code) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[i].EmployeeCode + "', '" + EmployeeTimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + status + "','" + dept + "','" + leav + "','" + earlyout + "'," + grad + ",'" + enddate + "','" + shift_code + "')";
                                    employee.fn_reportbyid(query);
                                    count++;
                                }
                            }
                            if (count >= 1)
                            {
                                //query = "select count(*) from tempsattendance";
                                //int c = employee.fn_RowCount(query);
                                //for (int i = 0; i < c; i++)
                                //{
                                query = "select count(*) from tempsattendance where registerno='" + EmployeeTimeList[i].EmployeeCode + "'";
                                int s = employee.fn_RowCount(query);
                                int Get_value = Convert.ToInt32(TextBox1.Text);
                                if (Get_value >= s)
                                {
                                    query = "delete from tempsattendance where registerno='" + EmployeeTimeList[i].EmployeeCode + "' ";
                                    employee.fn_reportbyid(query);
                                }
                                //}
                                query = "select count(*) from tempsattendance";
                                int Final_count = employee.fn_RowCount(query);
                                if (Final_count == 0)
                                {
                                    count = 0;
                                }
                            }
                        }
                    }
                }

                else if (ddl_report.SelectedItem.Text == "Compoff details")
                {
                    employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[ddl_i].Value);
                    employee.FirstName = chk_Empcode.Items[ddl_i].Text;

                    if (ddl_ShiftList.SelectedItem.Text == "All")
                    {
                        EmployeeList = employee.fn_getPatternList(employee.BranchId);
                        if (EmployeeList.Count > 0)
                        {

                            for (int j = 0; j < EmployeeList.Count; j++)
                            {
                                employee.shiftcode = EmployeeList[j].ShiftName.ToString();
                                EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                                if (EmployeeTimeList.Count > 0)
                                {
                                    for (int i = 0; i < EmployeeTimeList.Count; i++)
                                    {
                                        dept = employee.fn_DepartmentName();
                                        intime = EmployeeTimeList[i].Intime.ToString("HH:mm");
                                        outtime = EmployeeTimeList[i].Outtime.ToString("HH:mm");
                                        Latein = EmployeeTimeList[i].Latein.ToString("HH:mm");
                                        Lateout = EmployeeTimeList[i].Lateout.ToString("HH:mm");
                                        whours = EmployeeTimeList[i].whours.ToString("HH:mm");
                                        status = EmployeeTimeList[i].Flag;
                                        leav = EmployeeTimeList[i].Status21;
                                        shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                        grad = EmployeeTimeList[i].GradeId;
                                        dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                                        //dates = EmployeeTimeList[i].dates.ToString("dd/MM/yyyy");
                                        temp = Convert.ToDateTime(txt_tdate.Text);
                                        enddate = temp.ToString("yyyy/MM/dd");
                                        data = EmployeeTimeList[i].data;
                                        if (status == "CCL")
                                        {
                                            query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,whours,status,Department,Leave_name,earlyout,pn_gradeID,To_Date,shift_code) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[0].EmployeeCode + "', '" + EmployeeTimeList[0].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + whours + "','" + status + "','" + dept + "','',''," + grad + ",'" + enddate + "','" + shift_code + "')";
                                            employee.fn_reportbyid(query);
                                            count++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        employee.shiftcode = ddl_ShiftList.SelectedItem.Text;
                        EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                        if (EmployeeTimeList.Count > 0)
                        {
                            for (int i = 0; i < EmployeeTimeList.Count; i++)
                            {
                                dept = employee.fn_DepartmentName();
                                intime = EmployeeTimeList[i].Intime.ToString("HH:mm");
                                outtime = EmployeeTimeList[i].Outtime.ToString("HH:mm");
                                Latein = EmployeeTimeList[i].Latein.ToString("HH:mm");
                                Lateout = EmployeeTimeList[i].Lateout.ToString("HH:mm");
                                whours = EmployeeTimeList[i].whours.ToString("HH:mm");
                                status = EmployeeTimeList[i].Flag;
                                leav = EmployeeTimeList[i].Status21;
                                shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                grad = EmployeeTimeList[i].GradeId;
                                dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                                //dates = EmployeeTimeList[i].dates.ToString("dd/MM/yyyy");
                                temp = Convert.ToDateTime(txt_tdate.Text);
                                enddate = temp.ToString("yyyy/MM/dd");
                                data = EmployeeTimeList[i].data;
                                if (status == "CCL")
                                {
                                    query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,whours,status,Department,Leave_name,earlyout,pn_gradeID,To_Date,shift_code) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[0].EmployeeCode + "', '" + EmployeeTimeList[0].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + whours + "','" + status + "','" + dept + "','',''," + grad + ",'" + enddate + "','" + shift_code + "')";
                                    employee.fn_reportbyid(query);
                                    count++;
                                }
                            }
                        }
                    }
                }
                else if (ddl_report.SelectedItem.Text == "Manual Punch entry details")
                {
                    employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[ddl_i].Value);
                    employee.FirstName = chk_Empcode.Items[ddl_i].Text;
                    if (ddl_ShiftList.SelectedItem.Text == "All")
                    {
                        EmployeeList = employee.fn_getPatternList(employee.BranchId);
                        if (EmployeeList.Count > 0)
                        {

                            for (int j = 0; j < EmployeeList.Count; j++)
                            {
                                employee.shiftcode = EmployeeList[j].ShiftName.ToString();
                                EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                                if (EmployeeTimeList.Count > 0)
                                {
                                    for (int i = 0; i < EmployeeTimeList.Count; i++)
                                    {
                                        dept = employee.fn_DepartmentName();
                                        intime = EmployeeTimeList[i].Intime.ToString("HH:mm");
                                        outtime = EmployeeTimeList[i].Outtime.ToString("HH:mm");
                                        Latein = EmployeeTimeList[i].Latein.ToString("HH:mm");
                                        Lateout = EmployeeTimeList[i].Lateout.ToString("HH:mm");
                                        whours = EmployeeTimeList[i].whours.ToString("HH:mm");
                                        status = EmployeeTimeList[i].Flag;
                                        leav = EmployeeTimeList[i].Status21;
                                        grad = EmployeeTimeList[i].GradeId;
                                        shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                        dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                                        data = EmployeeTimeList[i].data;
                                        temp = Convert.ToDateTime(txt_tdate.Text);
                                        enddate = temp.ToString("yyyy/MM/dd");
                                        if (data == "M")
                                        {
                                            query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,whours,status,Department,Leave_name,earlyout,pn_gradeID,To_Date,shift_code) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[i].EmployeeCode + "', '" + EmployeeTimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + whours + "','" + status + "','" + dept + "','',''," + grad + ",'" + enddate + "','" + shift_code + "')";
                                            employee.fn_reportbyid(query);
                                            count++;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    else
                    {
                        employee.shiftcode = ddl_ShiftList.SelectedItem.Text;
                        EmployeeTimeList = employee.fn_EmployeeTimeCard(employee);
                        if (EmployeeTimeList.Count > 0)
                        {
                            for (int i = 0; i < EmployeeTimeList.Count; i++)
                            {
                                dept = employee.fn_DepartmentName();
                                intime = EmployeeTimeList[i].Intime.ToString("HH:mm");
                                outtime = EmployeeTimeList[i].Outtime.ToString("HH:mm");
                                Latein = EmployeeTimeList[i].Latein.ToString("HH:mm");
                                Lateout = EmployeeTimeList[i].Lateout.ToString("HH:mm");
                                whours = EmployeeTimeList[i].whours.ToString("HH:mm");
                                status = EmployeeTimeList[i].Flag;
                                leav = EmployeeTimeList[i].Status21;
                                grad = EmployeeTimeList[i].GradeId;
                                shift_code = EmployeeTimeList[i].shiftcode.ToString();
                                dates = EmployeeTimeList[i].dates.ToString("yyyy/MM/dd");
                                data = EmployeeTimeList[i].data;
                                temp = Convert.ToDateTime(txt_tdate.Text);
                                enddate = temp.ToString("yyyy/MM/dd");
                                if (data == "M")
                                {
                                    query = "insert into tempsattendance(pn_companyid,pn_branchid,RegisterNo,StudentName,dates,intime,latein,outtime,lateout,whours,status,Department,Leave_name,earlyout,pn_gradeID,To_Date,shift_code) values(" + employee.CompanyId + ", '" + employee.BranchId + "', '" + EmployeeTimeList[i].EmployeeCode + "', '" + EmployeeTimeList[i].FirstName + "','" + dates + "','" + intime + "','" + Latein + "','" + outtime + "','" + Lateout + "','" + whours + "','" + status + "','" + dept + "','',''," + grad + ",'" + enddate + "','" + shift_code + "')";
                                    employee.fn_reportbyid(query);
                                    count++;
                                }
                            }
                        }
                    }
                }
            }
        }
            
      

            if (count != 0)
            {
                //if (ddl_category.SelectedItem.Text == "Student")
                //{
                //    if (ddl_report.SelectedItem.Text == "Daily Attendance" || ddl_report.SelectedItem.Text == "Present" || ddl_report.SelectedItem.Text == "Absent")
                //    {
                //        Session["ReportName"] = "~/crystalreports/AttendanceDaily.rpt";
                //    }
                //}
                //else
                //{
                if (ddl_Departmentlist.SelectedItem.Text == "All")
                {
                    if (ddl_report.SelectedItem.Text == "Daily Attendance")
                    //if (ddl_report.SelectedItem.Text == "Daily Attendance" || ddl_report.SelectedItem.Text == "Present" || ddl_report.SelectedItem.Text == "Absent")
                    {
                        Session["ReportName"] = "~/crystalreports/AttendanceDailyEmp.rpt";
                    Response.Redirect("Report_view.aspx");
                }
                }
                else
                {
                    if (ddl_report.SelectedItem.Text == "Daily Attendance")
                    //if (ddl_report.SelectedItem.Text == "Daily Attendance" || ddl_report.SelectedItem.Text == "Present" || ddl_report.SelectedItem.Text == "Absent")
                    {
                        Session["ReportName"] = "~/crystalreports/AttendanceDailyEmpDep.rpt";
                    }
                }
                if (ddl_report.SelectedItem.Text == "Present")
                {
                    Session["ReportName"] = "~/crystalreports/Present.rpt";
                }
                else if (ddl_report.SelectedItem.Text == "Absent")
                {
                    Session["ReportName"] = "~/crystalreports/Absent.rpt";
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
                    Session["ReportName"] = "~/crystalreports/leavereport.rpt";
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
                    //if (ddl_category.SelectedItem.Text == "Staff")
                    //{
                    Session["ReportName"] = "~/crystalreports/Consolidate.rpt";
                    //}
                    //else
                    //{
                    //    Session["ReportName"] = "~/crystalreports/ConsolidateStudent.rpt";
                    //    //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Consolidate report applicable only for staffs');", true);
                    //    //return;
                    //}
                }
                else if (ddl_report.SelectedItem.Text == "OT report")
                {
                    Session["ReportName"] = "~/crystalreports/OTReport.rpt";
                }
                else if (ddl_report.SelectedItem.Text == "Employee Shift details by date wise")
                {
                    Session["ReportName"] = "~/crystalreports/employeeshiftdetails.rpt";
                }
                else if (ddl_report.SelectedItem.Text == "Compoff details")
                {
                    Session["ReportName"] = "~/crystalreports/Compoff.rpt";
                }
                else if (ddl_report.SelectedItem.Text == "Manual Punch entry details")
                {
                    Session["ReportName"] = "~/crystalreports/ManualPunch.rpt";
                }
                else if (ddl_report.SelectedItem.Text == "Frequently Late")
                {
                    Session["ReportName"] = "~/crystalreports/Frequent_Late.rpt";
                }
                else if (ddl_report.SelectedItem.Text == "Frequent Absentees")
                {
                    Session["ReportName"] = "~/crystalreports/Frequent_Absentees.rpt";
                }
                else if (ddl_report.SelectedItem.Text == "Frequent Early leaving")
                {
                    Session["ReportName"] = "~/crystalreports/Frequent_Early_Leave.rpt";
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
            //    student.ClassName = ddl_Courselist.SelectedItem.Text;
            //    student.DepartmentName = ddl_Departmentlist.SelectedItem.Text;
            //    student.Cyear = ddl_CurrentYearlist.SelectedItem.Text;

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

        //protected void ddl_category_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddl_category.SelectedItem.Text == "Staff")
        //    {
        //        if (s_login_role == "u")
        //        {
        //            EmployeeList = employee.fn_getDepartmentList1(employee.BranchId);
        //            if (EmployeeList.Count > 0)
        //            {
        //                ddl_Departmentlist.DataSource = EmployeeList;
        //                ddl_Departmentlist.DataTextField = "DepartmentName";
        //                ddl_Departmentlist.DataValueField = "DepartmentId";
        //                ddl_Departmentlist.DataBind();
        //                ddl_Departmentlist.Items.Add("All");
        //            }
        //            ddl_Departmentlist.SelectedIndex = ddl_Departmentlist.Items.Count - 1;
        //            //ddl_Courselist.Enabled = false;
        //            //ddl_CurrentYearlist.Enabled = false;
        //        }
        //    }
        //    else if (ddl_category.SelectedItem.Text == "Student")
        //    {
        //        //ddl_Courselist.Enabled = true;
        //        //ddl_CurrentYearlist.Enabled = true;
        //        StudentList = student.fn_department(employee.BranchId);
        //        if (StudentList.Count > 0)
        //        {
        //            ddl_Departmentlist.DataSource = StudentList;
        //            ddl_Departmentlist.DataTextField = "DepartmentName";
        //            ddl_Departmentlist.DataValueField = "DepartmentId";
        //            ddl_Departmentlist.DataBind();
        //        }
        //        if (s_login_role == "u")
        //        {
        //            user.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        //            user.EmployeeID = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
        //            UserRightsList = user.fn_emp_user_creation(user);
        //            if (UserRightsList[0].DepartmentName == "All (First Year Only)")
        //            {
        //                //ddl_CurrentYearlist.Enabled = false;
        //                //ddl_CurrentYearlist.Items.Remove("2");
        //                //ddl_CurrentYearlist.Items.Remove("3");
        //                //ddl_CurrentYearlist.Items.Remove("4");
        //                //return;
        //            }

        //            ddl_Departmentlist.SelectedItem.Text = UserRightsList[0].DepartmentName;
        //            ddl_Departmentlist.Enabled = false;
        //            //ddl_Courselist.Enabled = false;
        //        }
        //    }
        //}


        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedItem.Text == "All")
            {
                foreach (ListItem li in chk_Empcode.Items)
                {
                    li.Selected = true;
                }
                txt_tdate.Enabled = true;
            }

            else
            {
                foreach (ListItem li in chk_Empcode.Items)
                {
                    li.Selected = false;
                }
                txt_tdate.Enabled = false;
            }
        }
        protected void ddl_report_SelectedIndexChanged(object sender, EventArgs e)
        {
            //    if (ddl_report.SelectedItem.Text == "Consolidate" || ddl_report.SelectedItem.Text == "Muster Roll")
            //    {

            //    //    //chkall.Disabled = true;
            //    //    //chkall.Checked =false;
            //    //    foreach (ListItem li in chk_Empcode.Items)
            //    //    {
            //    //        li.Selected = false;
            //    //        li.Enabled = true;
            //    //    }
            //    //    txt_tdate.Enabled = true;
            //    //}
            //    //else
            //    //{
            //    //    //chkall.Disabled = true;
            //    //    //chkall.Checked = false;
            //    //    foreach (ListItem li in chk_Empcode.Items)
            //    //    {
            //    //        li.Selected = false;
            //    //        li.Enabled = true;
            //    //    }
            //    //    txt_tdate.Enabled = false;
            //    //}
            //}
            //if (ddl_report.SelectedValue == "Frequently Late")
            //{
            //    TextBox1.Enabled = true;
            //}
            //if (ddl_report.SelectedValue == "//Frequent Early leaving")
            //{
            //    TextBox1.Enabled = true;
            //}
            //if (ddl_report.SelectedValue == "Frequent Absentees")
            //{
            //    TextBox1.Enabled = true;
            //}

        }
    
}