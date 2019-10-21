using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.ObjectModel;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ePayHrms.Company;
using ePayHrms.Employee;
using ePayHrms.User_authentication;
using System.Data.SqlClient;
using System.Globalization;
using ePayHrms.Student;
using System.Text;
using System.Net;
using System.Web;
using System.IO;


public partial class Hrms_Employee_Default : System.Web.UI.Page
{
    private SqlConnection _connection;
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    public zkemkeeper.CZKEMClass CtrlBioComm = new zkemkeeper.CZKEMClass();
    public zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlConnection sqlcon = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    Company company = new Company();
    Student student = new Student();
    Collection<Student> MachineList;
    Collection<Student> CourseList;
    Collection<User__Rights> UserRightsList;
    User__Rights user = new User__Rights();
    private int iMachineNumber = 1;
    bool bIsConnected = false;
    Employee employee = new Employee();
    public string str2;
    public string flag = "0";
    string s_login_role;
    string s_form = "", shift = "";
    string ec = "", en = "", cur_day = "", BranCode = "";
    DataSet ds_userrights;

    public class Shift
    {
        #region Properties

        public TimeSpan intime { get; set; }

        public TimeSpan starttime { get; set; }

        public TimeSpan breakout { get; set; }

        public TimeSpan break_intime { get; set; }

        public TimeSpan breakin { get; set; }

        public TimeSpan halflimit { get; set; }

        public TimeSpan earlyout { get; set; }

        public TimeSpan outtime { get; set; }

        public TimeSpan othours { get; set; }

        public string stat { get; set; }

        public string leavecode { get; set; }

        public string ShiftCode { get; set; }

        #endregion Properties

        #region .ctors
        public Shift()
        {
        }
        #endregion Properties
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        student.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        student.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;

        if (!IsPostBack)
        {
            Timer1.Enabled = false;
            switch (s_login_role)
            {
                case "a":
                    populate_ddlbranch();                 
                    break;

                case "h":
                    ddl_branch.Visible = false;
                    ddl_course_load();
                    access();
                    break;

                case "e":
                    Response.Redirect("~/Company_Home.aspx");
                    break;

                case "u": s_form = "38";
                    ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                    if (ds_userrights.Tables[0].Rows.Count > 0)
                    {
                        user.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                        user.EmployeeID = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
                        ddl_branch.Visible = false;
                        ddl_course_load();
                        ddl_course.Enabled = false;
                        ddl_course.SelectedItem.Text = "";
                        ddl_type.SelectedIndex = 1;
                        ddl_department.Enabled = false;
                        ddl_type.Enabled = false;
                        UserRightsList = user.fn_emp_user_creation(user);
                        ddl_department.SelectedValue = UserRightsList[0].DepartmentName;
                        access();
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

    public void populate_ddlbranch()
    {
        myConnection.Open();
        SqlDataAdapter ad = new SqlDataAdapter("select * from paym_branch", myConnection);
        DataSet ds = new DataSet();
        ad.Fill(ds, "paym_branch");
        ddl_branch.DataSource = ds;
        ddl_branch.DataTextField = "branchname";
        ddl_branch.DataValueField = "pn_branchid";
        ddl_branch.DataBind();
        ddl_branch.SelectedItem.Text = "Select";
    }
    public void ddl_course_load()
    {
        CourseList = student.fn_course(student.BranchId);
        if (CourseList.Count > 0)
        {
            for (int ddl_i = -2; ddl_i < CourseList.Count; ddl_i++)
            {
                if (ddl_i == -2)
                {
                    ListItem e_list = new ListItem();

                    e_list.Text = "Select";
                    e_list.Value = "0";
                    ddl_course.Items.Add(e_list);
                }
                else if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();

                    e_list.Text = "All";
                    e_list.Value = "All";
                    ddl_course.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();

                    e_list.Value = CourseList[ddl_i].CourseId.ToString();
                    e_list.Text = CourseList[ddl_i].CourseName.ToString();
                    ddl_course.Items.Add(e_list);
                }
            }
        }
        else
        {
            lbl_Error.Text = "No Data Exist";
        }
    }
    public void load()
    {
        string fdate = txt_fromdate.Text;
        string tdate = txt_todate.Text;
        string[] fd = fdate.Split('/');
        string[] td = tdate.Split('/');
        string fr_date = fd[1] + "/" + fd[0] + "/" + fd[2];
        string to_date = td[1] + "/" + td[0] + "/" + td[2];
        SqlDataAdapter ada = new SqlDataAdapter();
        if (s_login_role == "a")
        {
            ada = new SqlDataAdapter("SELECT * FROM time_card_student where dates between '" + fr_date + "' and '" + to_date + "' and pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + ddl_branch.SelectedItem.Text + "' and emp_name = '" + ddl_ename.SelectedItem.Text + "' order by dates ", myConnection);
        }

        else if (s_login_role == "h")
        {
            ada = new SqlDataAdapter("SELECT * FROM time_card_student where dates between '" + fr_date + "' and '" + to_date + "' and pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' and emp_name = '" + ddl_ename.SelectedItem.Text + "' order by dates ", myConnection);
        }

        DataSet dset = new DataSet();
        ada.Fill(dset, "temp_studentcard");
        if (dset.Tables[0].Rows.Count == 0)
        {
            dset.Tables[0].Rows.Add(dset.Tables[0].NewRow());
            GridView1.DataSource = dset;
            GridView1.DataBind();
            int columnCount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columnCount;
            GridView1.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            GridView1.DataSource = dset;
            GridView1.DataBind();
        }
    }

    public void load1()
    {
        
        SqlDataAdapter ad = new SqlDataAdapter();
        if (s_login_role == "a")
        {
             ad = new SqlDataAdapter("SELECT * FROM temp_studentcard where pn_companyid = '" + ddl_branch.Text + "' and  pn_BranchID = '" + employee.BranchId + "' ", myConnection);
        }

        if (s_login_role == "h" || s_login_role == "u")
        {
            ad = new SqlDataAdapter("SELECT * FROM temp_studentcard where pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' ", myConnection);
        }
        DataSet ds = new DataSet();

        ad.Fill(ds, "temp_studentcard");


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

    }

    public void loadhour()
    {
        SqlDataAdapter ad = new SqlDataAdapter();
        if (s_login_role == "a")
        {
            ad = new SqlDataAdapter("SELECT * FROM temp_hourlycard where pn_companyid = '" + ddl_branch.Text + "' and  pn_BranchID = '" + employee.BranchId + "' ", myConnection);
        }

        if (s_login_role == "h")
        {
            ad = new SqlDataAdapter("SELECT * FROM temp_hourlycard where pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' ", myConnection);
        }
        DataSet ds = new DataSet();

        ad.Fill(ds, "temp_hourlycard");

        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridHour.DataSource = ds;
            GridHour.DataBind();
            int columnCount = GridHour.Rows[0].Cells.Count;
            GridHour.Rows[0].Cells.Clear();
            GridHour.Rows[0].Cells.Add(new TableCell());
            GridHour.Rows[0].Cells[0].ColumnSpan = columnCount;
            GridHour.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            GridHour.DataSource = ds;
            GridHour.DataBind();
        }

    }


    public void access()
    {
        // MessageBox.Show(employee.BranchId.ToString());
        // MessageBox.Show(employee.CompanyId.ToString());
        _connection = con.fn_Connection();
        _connection.Open();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=7 and section_view='No'", _connection);
        SqlDataReader rdrview = cmd.ExecuteReader();
        if (rdrview.Read())
        {
            Response.Write("<script language='javascript'>alert('Permission Restricted. Please Contact Administrator.');window.location='~/Company_Home.aspx';</script>");
        }
        rdrview.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=7 and section_edit='No'", _connection);
        SqlDataReader rdredit = cmd.ExecuteReader();
        if (rdredit.Read())
        {
          
        }
        rdredit.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=7 and  section_delete='No'", _connection);
        SqlDataReader rdrdel = cmd.ExecuteReader();
        if (rdrdel.Read())
        {

        }
        rdrdel.Close();
    }    
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        load();
        Btn_view.Text = "Reset";
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.DataItem != null)
            {
                if (Btn_view.Text == "Save" || flag == "1")
                {
                    Label ecode = ((Label)e.Row.FindControl("lbl_empcode"));
                    Label ename = ((Label)e.Row.FindControl("lbl_empname"));
                    Label date = ((Label)e.Row.FindControl("lbl_date"));
                    string dd = date.Text;
                    string[] dat = dd.Split('/');
                    string dates = dat[1] + "/" + dat[0] + "/" + dat[2];
                    //myConnection.Open();
                    TimeSpan out_time = new TimeSpan(0, 0, 0);
                    TimeSpan in_limit = new TimeSpan(0, 0, 0);
                    TimeSpan breakin_limit = new TimeSpan(0, 0, 0);
                    TimeSpan breakout_limit = new TimeSpan(0, 0, 0);
                    TimeSpan start_time = new TimeSpan(0, 0, 0);
                    TimeSpan breakin_time = new TimeSpan(0, 0, 0);
                    TimeSpan breakout_time = new TimeSpan(0, 0, 0);
                    TimeSpan otslb = new TimeSpan(0, 0, 0);
                    TimeSpan diff = new TimeSpan(0, 0, 0);
                    TimeSpan halfLmt = new TimeSpan(0, 0, 0);
                    TimeSpan in_time, daily_in, daily_out, ot_hours, ein, eout;
                    string otime = "", stime = "", bintime = "", bouttime = "", dtime, intime_lmt, bin_lmt, hd_lmt, bout_lmt, time_stat, time_statin, time_statout, in_chk, bin_chk, hd_chk, bout_chk, str0, str1, str2, lvCode = "", lvName = "", hdCode = "", hdName = "";
                    string cin, cbin, cbout, cout, ecod, stats, outdate, dat1, leav = "";
                    int otc = 0;
                    DateTime chkin, chkout;
                    DateTime curdate = Convert.ToDateTime(dd);

                    if (ddl_type.SelectedItem.Text == "Hourly" && flag == "0")
                    {
                        myConnection.Open();

                        if (curdate.DayOfWeek.ToString() == "Sunday" || curdate.DayOfWeek.ToString() == "Saturday")
                        {
                            cmd1 = new SqlCommand("update temp_studentcard set shift_code='WW' where dates = '" + dates + "' and emp_code = '" + ecode.Text + "' and emp_name = '" + ename.Text + "'", myConnection);
                            cmd1.ExecuteNonQuery();
                        }
                        else
                        {
                            cmd1 = new SqlCommand("update temp_studentcard set shift_code='ST' where dates = '" + dates + "' and emp_code = '" + ecode.Text + "' and emp_name = '" + ename.Text + "'", myConnection);
                            cmd1.ExecuteNonQuery();
                        }


                        //punch time populate

                        SqlCommand comm = new SqlCommand("select * from shift_details where shift_indicator = 'Student' and pn_branchid='" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "'", myConnection);
                        SqlDataReader rd_out = comm.ExecuteReader();
                        if (rd_out.Read())
                        {
                            DateTime st, bit, bot, et;
                            shift = Convert.ToString(rd_out["shift_code"]);
                            //start time
                            st = Convert.ToDateTime(rd_out["start_time"]);
                            stime = st.ToString("HH:mm:ss");
                            start_time = TimeSpan.Parse(stime);
                            //break out time
                            bit = Convert.ToDateTime(rd_out["break_time_out"]);
                            bintime = bit.ToString("HH:mm:ss");
                            breakin_time = TimeSpan.Parse(bintime);
                            //break in time
                            bot = Convert.ToDateTime(rd_out["break_time_in"]);
                            bouttime = bot.ToString("HH:mm:ss");
                            breakout_time = TimeSpan.Parse(bouttime);
                            //end time
                            et = Convert.ToDateTime(rd_out["end_time"]);
                            otime = et.ToString("HH:mm:ss");
                            out_time = TimeSpan.Parse(otime);
                        }
                        rd_out.Close();
                        TimeSpan max_in = start_time + in_limit;
                        TimeSpan max_breakout = breakin_time + breakin_limit;
                        TimeSpan max_breakin = breakout_time + breakin_limit;
                        //cmd = new SqlCommand("Insert into daily_timecard_student select distinct a.pn_companyid,a.pn_branchid,a.machine_num,a.card_no,a.emp_code,a.emp_name,a.VerifyMode,a.InOutMode,a.shift_code,a.dates,a.days,(select top 1 aa.times as Intime from punch_details aa where times < '" + breakin_time + "' and aa.emp_code=a.emp_code and aa.dates=a.dates order by aa.times desc ) as intime,(select top 1 aa.times as break_out from punch_details aa where times between '" + breakin_time + "' and '" + max_breakout + "' and aa.emp_code=a.emp_code and aa.dates=a.dates order by aa.times desc) as break_out,(select top 1 aa.times as break_in from punch_details aa where times between '" + breakout_time + "' and '" + max_breakin + "' and aa.emp_code=a.emp_code and aa.dates=a.dates order by aa.times desc) as break_in,(select top 1 aa.times as outtime from punch_details aa where times > '" + out_time + "' and aa.emp_code=a.emp_code and aa.dates=a.dates order by aa.times desc) as outtime , a.ot_hrs , a.status from (select * from punch_details) as a where dates = '" + dates + "' and emp_code = '" + ecode.Text + "'", myConnection);
                        //cmd.ExecuteNonQuery();
                        cmd = new SqlCommand("Insert into daily_timecard_student select distinct a.pn_companyid,a.pn_branchid,a.machine_num,a.card_no,a.card_no,a.emp_name,a.VerifyMode,a.InOutMode,a.shift_code,a.dates,a.days,(select top 1 aa.times as Intime from punch_details aa where times < '" + breakin_time + "' and aa.card_no=a.card_no and aa.dates=a.dates order by aa.times desc ) as intime,(select top 1 aa.times as break_out from punch_details aa where times between '" + breakin_time + "' and '" + max_breakout + "' and aa.card_no=a.card_no and aa.dates=a.dates order by aa.times desc) as break_out,(select top 1 aa.times as break_in from punch_details aa where times between '" + breakout_time + "' and '" + max_breakin + "' and aa.card_no=a.card_no and aa.dates=a.dates order by aa.times desc) as break_in,(select top 1 aa.times as outtime from punch_details aa where times > '" + out_time + "' and aa.card_no=a.card_no and aa.dates=a.dates order by aa.times desc) as outtime , a.ot_hrs , a.status from (select * from punch_details) as a where dates = '" + dates + "' and card_no = '" + ecode.Text + "'", myConnection);
                        cmd.ExecuteNonQuery();
                        SqlCommand cmd_dt = new SqlCommand("Select * from daily_timecard_student where pn_branchid = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "' and dates = '" + dates + "' and emp_code = '" + ecode.Text + "'", myConnection);
                        SqlDataReader rd_time = cmd_dt.ExecuteReader();
                        if (rd_time.Read())
                        {
                            cin = Convert.ToString(rd_time["intime"]);
                            cbin = Convert.ToString(rd_time["break_out"]);
                            cbout = Convert.ToString(rd_time["break_in"]);
                            cout = Convert.ToString(rd_time["outtime"]);
                            outdate = Convert.ToString(rd_time["dates"]);
                            string[] datespl = outdate.Split(' ');
                            dat1 = employee.Convert_ToSqlDatestring(datespl[0]);
                            ecod = Convert.ToString(rd_time["emp_code"]);
                            stats = "";
                            if (cin != "" && cout != "")
                            {
                                stats = "XX";
                                chkin = Convert.ToDateTime(cin);
                                cin = chkin.ToString("HH:mm:ss");
                                ein = TimeSpan.Parse(cin);
                                diff = breakin_time - ein;
                                if (diff < halfLmt)
                                {
                                    stats = "AX";
                                }

                            }
                            else if (((cin == "" && cbout != "") || (cin != "" && cbout == "") || (cin == "" && cbout == "")) && cbin != "" && cout != "")
                            {
                                stats = "AX";
                                leav = "Half Day";
                            }
                            else if (((cbin == "" && cout != "") || (cbin != "" && cout == "") || (cbin == "" && cout == "")) && cin != "" && cbout != "")
                            {
                                stats = "XA";
                                leav = "Half Day";
                            }
                            else
                            {
                                stats = "AA";
                                leav = "Absent";
                            }
                            cmd1 = new SqlCommand("update temp_studentcard set intime='" + cin + "' , break_out= '" + cbin + "' , break_in = '" + cbout + "' , outtime = '" + cout + "' where dates = '" + dates + "' and emp_code = '" + ecode.Text + "' and emp_name = '" + ename.Text + "' and pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
                            cmd1.ExecuteNonQuery();
                            SqlCommand cmdotc = new SqlCommand("Select OT_eligible from paym_employee where pn_branchid = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "'  and Employee_First_Name = '" + ename.Text + "'", myConnection);
                            SqlDataReader rdotc = cmdotc.ExecuteReader();
                            if (rdotc.Read())
                            {
                                otc = Convert.ToInt32(rdotc[0]);
                            }

                            if (cin != "" && cout != "")
                            {
                                chkin = Convert.ToDateTime(cin);
                                cin = chkin.ToString("HH:mm:ss");
                                ein = TimeSpan.Parse(cin);
                                chkout = Convert.ToDateTime(cout);
                                cout = chkout.ToString("HH:mm:ss");
                                eout = TimeSpan.Parse(cout);
                                int ttl_hr = (out_time - start_time).Hours;
                                int emp_hr = (eout - ein).Hours;

                                DateTime dtim = Convert.ToDateTime(rd_time["outtime"]);
                                string etime = dtim.ToString("HH:mm:ss");
                                daily_out = TimeSpan.Parse(etime);
                                if (otc == 1 && cout != "" && emp_hr >= ttl_hr)
                                {
                                    ot_hours = daily_out - out_time;
                                    if (ot_hours < System.TimeSpan.Parse("00:00:00"))
                                    {
                                        ot_hours = System.TimeSpan.Parse("00:00:00");
                                    }
                                    comm = new SqlCommand("select ot_slab from otslab where ot_from <= '" + ot_hours + "' and ot_to >= '" + ot_hours + "' and pn_branchID = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "'", myConnection);
                                    SqlDataReader rdot = comm.ExecuteReader();
                                    if (rdot.Read())
                                    {
                                        DateTime ottime = Convert.ToDateTime(rdot[0]);
                                        string ottim = ottime.ToString("HH:mm:ss");
                                        otslb = TimeSpan.Parse(ottim);
                                    }
                                    comm = new SqlCommand("update daily_timecard_student set ot_hrs='" + otslb + "' , status = '" + stats + "'  where outtime = '" + dtim + "' and dates = '" + dat1 + "' and emp_code = '" + ecod + "' and pn_branchID = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "'", myConnection);
                                    comm.ExecuteNonQuery();
                                    comm = new SqlCommand("update temp_studentcard set ot_hrs='" + otslb + "' , status = '" + stats + "'  where outtime = '" + dtim + "' and dates = '" + dat1 + "' and emp_code = '" + ecod + "' and pn_branchID = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "'", myConnection);
                                    comm.ExecuteNonQuery();
                                }
                                else
                                {
                                    comm = new SqlCommand("update daily_timecard_student set status = '" + stats + "'  where outtime = '" + dtim + "' and dates = '" + dat1 + "' and emp_code = '" + ecod + "' and pn_branchID = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "'", myConnection);
                                    comm.ExecuteNonQuery();
                                    comm = new SqlCommand("update temp_studentcard set status = '" + stats + "'  where outtime = '" + dtim + "' and dates = '" + dat1 + "' and emp_code = '" + ecod + "' and pn_branchID = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "'", myConnection);
                                    comm.ExecuteNonQuery();
                                }

                            }
                            else
                            {
                                comm = new SqlCommand("update temp_studentcard set status = '" + stats + "'  where dates = '" + dat1 + "' and emp_code = '" + ecod + "' and pn_branchID = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "'", myConnection);
                                comm.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            stats = "AA";
                            comm = new SqlCommand("update temp_studentcard set status = '" + stats + "'  where dates = '" + dates + "' and emp_code = '" + ecode.Text + "' and pn_branchID = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "'", myConnection);
                            comm.ExecuteNonQuery();
                        }
                        rd_time.Close();
                        Label sft = ((Label)e.Row.FindControl("lbl_shiftcode"));
                        if (sft.Text == "WW")
                        {
                            cmd1 = new SqlCommand("update temp_studentcard set leave_code = 'Week Off' , status = 'WW' where dates = '" + dates + "' and emp_code = '" + ecode.Text + "' and emp_name = '" + ename.Text + "' and pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
                            cmd1.ExecuteNonQuery();
                        }
                        Label stus = ((Label)e.Row.FindControl("lbl_status"));
                        if (stus.Text == "AA" && sft.Text != "WW")
                        {
                            cmd1 = new SqlCommand("update temp_studentcard set leave_code = 'Absent'  where dates = '" + dates + "' and emp_code = '" + ecode.Text + "' and emp_name = '" + ename.Text + "' and pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
                            cmd1.ExecuteNonQuery();
                        }
                        if (stats == "AA")
                        {
                            e.Row.BackColor = System.Drawing.Color.LightPink;
                            SqlCommand cmdlv = new SqlCommand("Select pn_Leavecode , pn_leavename from leave_apply where pn_branchid = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "' and from_date <= '" + dates + "' and to_date >= '" + dates + "' and emp_name = '" + ename.Text + "' and flag='y'", myConnection);
                            SqlDataReader rdlv = cmdlv.ExecuteReader();
                            while (rdlv.Read())
                            {
                                lvCode = Convert.ToString(rdlv[0]);
                                lvName = Convert.ToString(rdlv[1]);
                                cmd1 = new SqlCommand("update temp_studentcard set leave_code = '" + lvName + "' , status = '" + lvCode + "' where dates = '" + dates + "' and emp_code = '" + ecode.Text + "' and emp_name = '" + ename.Text + "' and pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
                                cmd1.ExecuteNonQuery();
                                cmd1 = new SqlCommand("update leave_apply set record = 'T' where from_date <= '" + dates + "' and to_date >= '" + dates + "' and emp_code = '" + ecode.Text + "' and emp_name = '" + ename.Text + "' and pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
                                cmd1.ExecuteNonQuery();
                            }
                        }
                        SqlCommand cmdhd = new SqlCommand("Select pn_Holidaycode , pn_Holidayname from paym_holiday where pn_branchid = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "' and from_date <= '" + dates + "' and to_date >= '" + dates + "'", myConnection);
                        SqlDataReader rdhd = cmdhd.ExecuteReader();
                        if (rdhd.Read())
                        {
                            hdCode = Convert.ToString(rdhd[0]);
                            hdName = Convert.ToString(rdhd[1]);
                            cmd1 = new SqlCommand("update temp_studentcard set leave_code = 'Holiday' , status = 'HH' where dates = '" + dates + "' and emp_code = '" + ecode.Text + "' and emp_name = '" + ename.Text + "' and pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
                            cmd1.ExecuteNonQuery();
                        }
                        SqlCommand cmdod = new SqlCommand("Select * from onduty where pn_branchid = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "' and onduty_dat <= '" + dates + "' and todat >= '" + dates + "' and empname = '" + ename.Text + "' and approval = 'yes' ", myConnection);
                        SqlDataReader rdod = cmdod.ExecuteReader();
                        if (rdod.Read())
                        {
                            cmd1 = new SqlCommand("update temp_studentcard set leave_code = 'On Duty' , status = 'DD' where dates = '" + dates + "' and emp_code = '" + ecode.Text + "' and emp_name = '" + ename.Text + "' and pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
                            cmd1.ExecuteNonQuery();
                        }


                        cmd = new SqlCommand("create table temp_studentcard3(pn_companyid int,pn_branchid int,machine_num int, card_no varchar(5) , emp_code varchar(10), emp_name varchar(50),VerifyMode int,InOutMode int, shift_code varchar(5),dates datetime,days varchar(15),intime time , break_out time, break_in time , outtime time , ot_hrs datetime , status varchar(2))", myConnection);
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCommand("INSERT INTO temp_studentcard3 SELECT DISTINCT * FROM daily_timecard_student where pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCommand("drop table daily_timecard_student", myConnection);
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCommand("EXEC sp_rename 'temp_studentcard3', 'daily_timecard_student'", myConnection);
                        cmd.ExecuteNonQuery();
                        myConnection.Close();
                    }
                }

                //    else if (ddl_type.SelectedItem.Text == "Daily" || flag == "1")
                //    {
                //        TimeSpan max_in1, max_breakout1, max_breakin1;
                //        TimeSpan inchk = new TimeSpan(0, 0, 0);
                //        TimeSpan outchk = new TimeSpan(0, 0, 0);
                //        DateTime inc, outc;
                //        string lin = "00:00:00", lout = "00:00:00";
                //        myConnection.Open();                        

                //        //if (curdate.DayOfWeek.ToString() == "Sunday" || curdate.DayOfWeek.ToString() == "Saturday")
                //        //{
                //        //    cmd1 = new SqlCommand("update temp_studentcard set shift_code='WW' where dates = '" + dates + "' and RegisterNo = '" + ecode.Text + "' and StudentName = '" + ename.Text + "'", myConnection);
                //        //    cmd1.ExecuteNonQuery();  
                //        //}
                //        //else
                //        //{
                //        //    cmd1 = new SqlCommand("update temp_studentcard set shift_code='ST' where dates = '" + dates + "' and RegisterNo = '" + ecode.Text + "' and StudentName = '" + ename.Text + "'", myConnection);
                //        //    cmd1.ExecuteNonQuery();
                //        //}


                //        if (curdate.DayOfWeek.ToString() == "Sunday")
                //        {
                //            cmd1 = new SqlCommand("update temp_studentcard set shift_code='WW' where dates = '" + dates + "' and RegisterNo = '" + ecode.Text + "' and StudentName = '" + ename.Text + "'", myConnection);
                //            cmd1.ExecuteNonQuery();                            
                //        }
                //        else if (curdate.DayOfWeek.ToString() == "Saturday")
                //        {

                //            SqlCommand cmds = new SqlCommand("select * from punch_details where pn_branchid = '" + employee.BranchId + "' and dates='" + dates + "' and  pn_companyid = '" + employee.CompanyId + "'", myConnection);
                //           SqlDataReader dr=cmds.ExecuteReader();
                //            if (dr.Read())
                //            {
                //                cmd1 = new SqlCommand("update temp_studentcard set shift_code='ST' where dates = '" + dates + "' and RegisterNo = '" + ecode.Text + "' and StudentName = '" + ename.Text + "'", myConnection);
                //                cmd1.ExecuteNonQuery();
                //            }                                                 
                //            else
                //            {
                //             cmd1 = new SqlCommand("update temp_studentcard set shift_code='WW' where dates = '" + dates + "' and RegisterNo = '" + ecode.Text + "' and StudentName = '" + ename.Text + "'", myConnection);
                //             cmd1.ExecuteNonQuery();   
                //            }                         

                //        }
                //        else
                //        {
                //            cmd1 = new SqlCommand("update temp_studentcard set shift_code='ST' where dates = '" + dates + "' and RegisterNo = '" + ecode.Text + "' and StudentName = '" + ename.Text + "'", myConnection);
                //            cmd1.ExecuteNonQuery();

                //        }


                //        //punch time populate

                //        SqlCommand comm = new SqlCommand("select * from shift_details where shift_indicator = 'Student' and pn_branchid='" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "'", myConnection);
                //        SqlDataReader rd_out = comm.ExecuteReader();

                //        if (rd_out.Read())
                //        {
                //            DateTime st, bit, bot, et;
                //            shift = Convert.ToString(rd_out["shift_code"]);
                //            //start time
                //            st = Convert.ToDateTime(rd_out["start_time"]);
                //            stime = st.ToString("HH:mm:ss");
                //            start_time = TimeSpan.Parse(stime);
                //            //break out time
                //            bit = Convert.ToDateTime(rd_out["break_time_out"]);
                //            bintime = bit.ToString("HH:mm:ss");
                //            breakin_time = TimeSpan.Parse(bintime);
                //            //break in time
                //            bot = Convert.ToDateTime(rd_out["break_time_in"]);
                //            bouttime = bot.ToString("HH:mm:ss");
                //            breakout_time = TimeSpan.Parse(bouttime);
                //            //end time
                //            et = Convert.ToDateTime(rd_out["end_time"]);
                //            otime = et.ToString("HH:mm:ss");
                //            out_time = TimeSpan.Parse(otime);
                //            //otime = Convert.ToString(rd_out["end_time"]);
                //            //string[] oti = otime.Split(' ');
                //            //out_time = TimeSpan.Parse(oti[1]);
                //        }
                //        rd_out.Close();
                //        max_in1 = start_time;
                //        max_breakout1 = breakin_time;
                //        max_breakin1 = breakout_time;
                //        cmd = new SqlCommand("delete from daily_timecard_student", myConnection);
                //        cmd.ExecuteNonQuery();
                //        cmd = new SqlCommand("Insert into daily_timecard_student select distinct a.pn_companyid,a.pn_branchid,a.machine_num,a.card_no,a.emp_code,a.emp_name,a.VerifyMode,a.InOutMode,a.shift_code,a.dates,a.days,(select top 1 aa.times as Intime from punch_details aa where times < '" + breakin_time + "' and aa.emp_code=a.emp_code and aa.dates=a.dates order by aa.times asc ) as intime,(select top 1 aa.times as break_out from punch_details aa where times between '" + breakin_time + "' and '" + max_breakout1 + "' and aa.emp_code=a.emp_code and aa.dates=a.dates order by aa.times asc) as break_out,(select top 1 aa.times as break_in from punch_details aa where times between '" + breakout_time + "' and '" + max_breakin1 + "' and aa.emp_code=a.emp_code and aa.dates=a.dates order by aa.times asc) as break_in,(select top 1 aa.times as outtime from punch_details aa where times > '" + out_time + "' and aa.emp_code=a.emp_code and aa.dates=a.dates order by aa.times desc) as outtime , a.ot_hrs , a.status from (select * from punch_details) as a where dates = '" + dates + "' and emp_code = '" + ecode.Text + "'", myConnection);
                //        cmd.ExecuteNonQuery();
                //        SqlCommand cmd_dt = new SqlCommand("Select * from daily_timecard_student where pn_branchid = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "' and dates = '" + dates + "' and RegisterNo='" + ecode.Text + "'", myConnection);
                //        SqlDataReader rd_time = cmd_dt.ExecuteReader();
                //        if (rd_time.Read())
                //        {
                //            cin = Convert.ToString(rd_time["intime"]);
                //            cbin = Convert.ToString(rd_time["break_out"]);
                //            cbout = Convert.ToString(rd_time["break_in"]);
                //            cout = Convert.ToString(rd_time["outtime"]);
                //            outdate = Convert.ToString(rd_time["dates"]);
                //            string[] datespl = outdate.Split(' ');
                //            dat1 = employee.Convert_ToSqlDatestring(datespl[0]);
                //            ecod = Convert.ToString(rd_time["RegisterNo"]);
                //            stats = "";
                //            if (cin != "")
                //            {
                //                //inc = Convert.ToDateTime(rd_time["intime"]);
                //                //string stim = inc.ToString("HH:mm:ss");
                //                inchk = TimeSpan.Parse(rd_time["intime"].ToString());
                //                if (inchk > start_time)
                //                {
                //                    lin = Convert.ToString(inchk - start_time);
                //                }
                //            }
                //            if (cout != "")
                //            {
                //                //outc = Convert.ToDateTime(rd_time["outtime"]);
                //                //string etim = outc.ToString("HH:mm:ss");
                //                outchk = TimeSpan.Parse(rd_time["outtime"].ToString());
                //                if (outchk > out_time)
                //                {
                //                    lout = Convert.ToString(outchk - out_time);
                //                }
                //            }
                //            if (cin != "" && cout != "")
                //            {
                //                stats = "XX";
                //                chkin = Convert.ToDateTime(cin);
                //                cin = chkin.ToString("HH:mm:ss");
                //                ein = TimeSpan.Parse(cin);
                //                diff = breakin_time - ein;
                //                if (diff < halfLmt)
                //                {
                //                    stats = "AX";
                //                }

                //            }
                //            else if (((cin == "" && cbout != "") || (cin != "" && cbout == "") || (cin == "" && cbout == "")) && cbin != "" && cout != "")
                //            {
                //                stats = "AX";
                //                leav = "Half Day";
                //            }
                //            else if (((cbin == "" && cout != "") || (cbin != "" && cout == "") || (cbin == "" && cout == "")) && cin != "" && cbout != "")
                //            {
                //                stats = "XA";
                //                leav = "Half Day";
                //            }
                //            else
                //            {
                //                stats = "AA";
                //                leav = "Absent";
                //                e.Row.BackColor = System.Drawing.Color.LightPink;
                //            }
                //            cmd1 = new SqlCommand("update temp_studentcard set intime='" + cin + "' , break_out= '" + cbin + "' , break_in = '" + cbout + "' , outtime = '" + cout + "', late_in = '" + lin + "', late_out = '" + lout + "', status = '" + stats + "' where dates = '" + dates + "' and RegisterNo = '" + ecode.Text + "' and StudentName = '" + ename.Text + "' and pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
                //            cmd1.ExecuteNonQuery();
                //        }
                //        else
                //        {
                //            stats = "AA";
                //            comm = new SqlCommand("update temp_studentcard set status = '" + stats + "'  where dates = '" + dates + "' and RegisterNo = '" + ecode.Text + "' and pn_branchID = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "'", myConnection);
                //            comm.ExecuteNonQuery();
                //        }
                //        rd_time.Close();

                //        SqlCommand cmdhd = new SqlCommand("Select pn_Holidaycode , pn_Holidayname from paym_holiday where pn_branchid = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "' and from_date <= '" + dates + "' and to_date >= '" + dates + "'", myConnection);
                //        SqlDataReader rdhd = cmdhd.ExecuteReader();
                //        if (rdhd.Read())
                //        {
                //            hdCode = Convert.ToString(rdhd[0]);
                //            hdName = Convert.ToString(rdhd[1]);
                //            cmd1 = new SqlCommand("update temp_studentcard set leave_code = 'Holiday' , status = 'HH' where dates = '" + dates + "' and RegisterNo = '" + ecode.Text + "' and StudentName = '" + ename.Text + "' and pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
                //            cmd1.ExecuteNonQuery();
                //        }

                //        Label sft = ((Label)e.Row.FindControl("lbl_shiftcode"));
                //        if (sft.Text == "WW")
                //        {
                //            cmd1 = new SqlCommand("update temp_studentcard set leave_code = 'Week Off' , status = 'WW' where dates = '" + dates + "' and RegisterNo = '" + ecode.Text + "' and StudentName = '" + ename.Text + "' and pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
                //            cmd1.ExecuteNonQuery();
                //            stats = sft.Text;
                //        }
                //        Label stus = ((Label)e.Row.FindControl("lbl_status"));
                //        if (stats == "AA" && stats != "WW")
                //        {
                //            cmd1 = new SqlCommand("update temp_studentcard set leave_code = 'Absent'  where dates = '" + dates + "' and RegisterNo = '" + ecode.Text + "' and StudentName = '" + ename.Text + "' and pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
                //            cmd1.ExecuteNonQuery();
                //        }

                //        //cmd = new SqlCommand("create table temp_studentcard3(pn_companyid int,pn_branchid int,machine_num int, card_no varchar(15) , RegisterNo varchar(15), StudentName varchar(50),VerifyMode int,InOutMode int, shift_code varchar(5),dates datetime,days varchar(15),intime time , break_out time, break_in time , outtime time , late_in time, Late_out time, ot_hrs datetime , status varchar(2))", myConnection);
                //        //cmd.ExecuteNonQuery();
                //        //cmd = new SqlCommand("INSERT INTO temp_studentcard3 SELECT DISTINCT * FROM daily_timecard_student where pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
                //        //cmd.ExecuteNonQuery();
                //        //cmd = new SqlCommand("drop table daily_timecard_student", myConnection);
                //        //cmd.ExecuteNonQuery();
                //        //cmd = new SqlCommand("EXEC sp_rename 'temp_studentcard3', 'daily_timecard_student'", myConnection);
                //        //cmd.ExecuteNonQuery();
                //        myConnection.Close();
                //    }

                //}

                else if (Btn_view.Text == "View" && flag == "0")
                {
                    Label intim = ((Label)e.Row.FindControl("lbl_starttime"));
                    Label btimi = ((Label)e.Row.FindControl("lbl_breaktimeo"));
                    Label btimo = ((Label)e.Row.FindControl("lbl_breaktimei"));
                    Label outtim = ((Label)e.Row.FindControl("lbl_endtime"));
                    Label othour = ((Label)e.Row.FindControl("lbl_othrs"));
                    if (intim.Text == "00:00")
                    { intim.Text = ""; }
                    if (btimi.Text == "00:00")
                    { btimi.Text = ""; }
                    if (btimo.Text == "00:00")
                    { btimo.Text = ""; }
                    if (outtim.Text == "00:00")
                    { outtim.Text = ""; }
                    if (othour.Text == "00:00")
                    { othour.Text = ""; }
                }

                else
                {
                }
            }
        }
        catch (Exception ex)
        {
            myConnection.Close();
            return;
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        load1();
        Btn_view.Text = "View";
        Btn_view.CssClass = "btn btn-info";
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = GridView1.Rows[e.RowIndex];
        if (row != null)
        {
            string emp_code = ((Label)row.FindControl("txt_empcode_edit")).Text;
            string emp_name = ((Label)row.FindControl("txt_empname_edit")).Text;
            string shift_code = ((Label)row.FindControl("ddl_editshiftcode1")).Text;
            string date_edit = ((Label)row.FindControl("txt_date_edit")).Text;
            string day_edit = ((Label)row.FindControl("txt_day_edit")).Text;
            string start_time = ((TextBox)row.FindControl("txt_starttime_edit")).Text;
            string break_timeo = ((TextBox)row.FindControl("txt_breaktimeo_edit")).Text;
            string break_timei = ((TextBox)row.FindControl("txt_breaktimei_edit")).Text;
            string end_time = ((TextBox)row.FindControl("txt_endtime_edit")).Text;
            string othrs = ((TextBox)row.FindControl("txt_othrs_edit")).Text;
            string leave = ((DropDownList)row.FindControl("ddl_leavename")).SelectedItem.Text;
            string status = ((DropDownList)row.FindControl("ddl_status")).SelectedItem.Text;
            myConnection.Open(); 
            if (s_login_role == "a")
            {
                SqlCommand updat = new SqlCommand("update time_card_student set intime = '" + start_time + "',break_out = '" + break_timeo + "',break_in = '" + break_timei + "',outtime ='" + end_time + "',ot_hrs = '" + othrs + "',status='" + status + "',leave_code = '" + leave + "', data = 'M' ", myConnection);
                updat.ExecuteNonQuery();
                lbl_Error.Text = "updated successfully";
            }

            if (s_login_role == "h")
            {
                SqlCommand updat = new SqlCommand("update time_card_student set intime = '" + start_time + "',break_out = '" + break_timeo + "',break_in = '" + break_timei + "',outtime ='" + end_time + "',ot_hrs = '" + othrs + "',status='" + status + "',leave_code = '" + leave + "', data = 'M' ", myConnection);
                updat.ExecuteNonQuery();
                lbl_Error.Text = "updated successfully";
            }

            GridView1.EditIndex = -1;
        }
    }
 
    protected void Btn_import_Click(object sender, EventArgs e)
    {
        try
        {
            
            DateTime fdate, tdate;
            if (ddl_type.SelectedItem.Text == "Hourly")
            {
                myConnection.Open();
                string cmd_text = "IF EXISTS (SELECT * FROM sysobjects WHERE Name = 'temp_studentcard')" + "DROP TABLE temp_studentcard";
                SqlCommand cmd2 = new SqlCommand(cmd_text, myConnection);
                cmd2.ExecuteNonQuery();
                myConnection.Close();

                myConnection.Open();
                fdate = Convert.ToDateTime(txt_fromdate.Text);
                tdate = Convert.ToDateTime(txt_todate.Text);
                TimeSpan diff = tdate - fdate;
                int date_diff = diff.Days;

                cmd = new SqlCommand("create table temp_studentcard(pn_companyid int, pn_branchid int, RegisterNo varchar(20), StudentName varchar(50),shift_code varchar(10),dates datetime,days varchar(15),intime time , break_out time, break_in time, outtime time, late_in time, late_out time, ot_hrs datetime,leave_code varchar(20), status varchar(2)), SMSStat varchar(5)", myConnection);
                cmd.ExecuteNonQuery();

                for (int d = 0; d <= date_diff; d++)
                {
                    DateTime day = fdate.AddDays(d);
                    cur_day = day.DayOfWeek.ToString();
                    string dt = Convert.ToString(fdate.AddDays(d));
                    string[] date = dt.Split('/', '-');
                    string fin_date = date[1] + "/" + date[0] + "/" + date[2];
                    cmd = new SqlCommand("insert into temp_studentcard(pn_companyid,pn_branchid,RegisterNo,StudentName,shift_code,dates,days,status) values ('" + employee.CompanyId + "', '" + employee.BranchId + "' ,'" + ddl_ename.SelectedItem.Text + "','" + ddl_ename.SelectedItem.Value + "','','" + fin_date + "','" + cur_day + "','')", myConnection);
                    cmd.ExecuteNonQuery();
                }
                myConnection.Close();
            }

            else if (ddl_type.SelectedItem.Text == "Daily")
            {
                if (txt_fromdate.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter Attendance Date');", true);
                    return;
                }
                SqlCommand comm = new SqlCommand();
                TimeSpan inchk = new TimeSpan(0, 0, 0);
                TimeSpan outchk = new TimeSpan(0, 0, 0);
                TimeSpan out_time = new TimeSpan(0, 0, 0);
                TimeSpan in_limit = new TimeSpan(0, 0, 0);
                TimeSpan breakin_limit = new TimeSpan(0, 0, 0);
                TimeSpan breakout_limit = new TimeSpan(0, 0, 0);
                TimeSpan start_time = new TimeSpan(0, 0, 0);
                TimeSpan breakin_time = new TimeSpan(0, 0, 0);
                TimeSpan breakout_time = new TimeSpan(0, 0, 0);

                DateTime inc, outc, chkin, chkout;
                TimeSpan halfLmt = new TimeSpan(0, 0, 0);

                TimeSpan diff = new TimeSpan(0, 0, 0);
                TimeSpan in_time, daily_in, daily_out, ot_hours, ein, eout;
                string otime = "", stime = "", bintime = "", bouttime = "", dtime, intime_lmt, bin_lmt, hd_lmt, bout_lmt, time_stat, time_statin, time_statout, in_chk, bin_chk, hd_chk, bout_chk, str0, str1, str2, lvCode = "", lvName = "", hdCode = "", hdName = "";
                string cin = "", cbin = "", cbout = "", cout = "", ecod, stats = "", outdate, dat1, leav = "";
                int otc = 0;
                myConnection.Open();
                string cmd_text = "IF EXISTS (SELECT * FROM sysobjects WHERE Name = 'temp_studentcard')" + "DROP TABLE temp_studentcard";
                SqlCommand cmd2 = new SqlCommand(cmd_text, myConnection);
                cmd2.ExecuteNonQuery();
                
                string dt1 = Convert.ToString(txt_fromdate.Text);
                string[] date1 = dt1.Split('/');
                string txt_date = date1[1] + "/" + date1[0] + "/" + date1[2];
                DateTime day = Convert.ToDateTime(txt_fromdate.Text);
                cur_day = day.DayOfWeek.ToString();

                cmd = new SqlCommand("create table temp_studentcard(pn_companyid int, pn_branchid int, RegisterNo varchar(20), StudentName varchar(50),shift_code varchar(10),dates datetime,days varchar(15),intime time , break_out time, break_in time , outtime time, late_in time, late_out time , ot_hrs datetime ,leave_code varchar(20), status varchar(2))", myConnection);
                cmd.ExecuteNonQuery();

                string qry = "select * from paym_student where pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "' and status = 'Y'";
                if (ddl_course.SelectedItem.Text != "All")
                {
                    qry = "select * from paym_student where pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "' and Department = '" + ddl_department.SelectedItem.Text + "' and Section = '" + ddl_Section.SelectedItem.Text + "' and currentyear = '" + ddl_currentyear.SelectedItem.Text + "' and status = 'Y'";
                }

                cmd = new SqlCommand(qry, myConnection);



                SqlDataReader rd_ecode1 = cmd.ExecuteReader();
                while (rd_ecode1.Read())
                {
                    cin = ""; cbin = ""; cbout = ""; cout = "";
                    TimeSpan otslb = new TimeSpan(0, 0, 0);
                    string lin = "00:00:00", lout = "00:00:00", e_out = "00:00:00"; 
                    ec = Convert.ToString(rd_ecode1["RegisterNo"]);
                    en = Convert.ToString(rd_ecode1["StudentName"]);

                    Collection<Shift> Attend;
                    Attend = Shift_Details();

                    cmd = new SqlCommand("delete from daily_timecard_student", myConnection);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("Insert into daily_timecard_student select distinct a.pn_companyid,a.pn_branchid,a.machine_num,a.card_no,a.emp_code,a.emp_name,a.VerifyMode,a.InOutMode,a.shift_code,a.dates,a.days,(select top 1 aa.times as Intime from punch_details aa where times < '" + Attend[0].break_intime + "' and aa.emp_code=a.emp_code and aa.dates=a.dates order by aa.times desc ) as intime,(select top 1 aa.times as break_out from punch_details aa where times between '" + Attend[0].break_intime + "' and '" + Attend[0].breakout + "' and aa.emp_code=a.emp_code and aa.dates=a.dates order by aa.times desc) as break_out,(select top 1 aa.times as break_in from punch_details aa where times between '" + Attend[0].breakout + "' and '" + Attend[0].breakin + "' and aa.emp_code=a.emp_code and aa.dates=a.dates order by aa.times desc) as break_in,(select top 1 aa.times as outtime from punch_details aa where times > '" + Attend[0].breakin + "' and aa.emp_code=a.emp_code and aa.dates=a.dates order by aa.times desc) as outtime , a.ot_hrs , a.status from (select * from punch_details) as a where dates = '" + txt_date + "' and emp_code = '" + ec + "'", myConnection);
                    cmd.ExecuteNonQuery();
                    SqlCommand cmd_dt = new SqlCommand("Select * from daily_timecard_student where pn_branchid = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "' and dates = '" + txt_date + "' and RegisterNo='" + ec + "'", myConnection);
                    SqlDataReader rd_time = cmd_dt.ExecuteReader();
                    if (rd_time.Read())
                    {
                        cin = Convert.ToString(rd_time["intime"]);
                        cbin = Convert.ToString(rd_time["break_out"]);
                        cbout = Convert.ToString(rd_time["break_in"]);
                        cout = Convert.ToString(rd_time["outtime"]);
                        outdate = Convert.ToString(rd_time["dates"]);
                        string[] datespl = outdate.Split(' ');
                        dat1 = employee.Convert_ToSqlDatestring(datespl[0]);
                        ecod = Convert.ToString(rd_time["RegisterNo"]);
                        stats = "";
                        if (cin != "")
                        {
                            string stim = rd_time["intime"].ToString();
                            inchk = TimeSpan.Parse(stim);
                            if (inchk > Attend[0].starttime)
                            {
                                lin = Convert.ToString(inchk - Attend[0].starttime);
                            }
                        }
                        if (cout != "")
                        {

                            string etim = rd_time["outtime"].ToString();
                            outchk = TimeSpan.Parse(etim);
                            if (outchk > Attend[0].outtime)
                            {
                                lout = Convert.ToString(outchk - Attend[0].outtime);
                            }
                            else
                            {
                                e_out = Convert.ToString(Attend[0].outtime - outchk);
                            }
                        }
                        if (cin != "" && cout != "")
                        {
                            stats = "XX";
                            leav = "";
                            chkin = Convert.ToDateTime(cin);
                            cin = chkin.ToString("HH:mm:ss");
                            ein = TimeSpan.Parse(cin);
                            diff = Attend[0].break_intime - ein;
                            if (diff < Attend[0].halflimit)
                            {
                                stats = "AX";
                            }

                        }
                        else if (((cin == "" && cbout != "") || (cin != "" && cbout == "") || (cin == "" && cbout == "")) && cbin != "" && cout != "")
                        {
                            stats = "AX";
                            leav = "Half Day";
                        }
                        else if (((cbin == "" && cout != "") || (cbin != "" && cout == "") || (cbin == "" && cout == "")) && cin != "" && cbout != "")
                        {
                            stats = "XA";
                            leav = "Half Day";
                        }
                        else
                        {
                            stats = "AA";
                            leav = "Absent";
                        }


                    }
                    else
                    {
                        stats = "AA";
                        leav = "Absent";
                    }

                    SqlCommand cmdlv = new SqlCommand("Select pn_Leavecode , pn_leavename from leave_apply where pn_branchid = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "' and from_date <= '" + txt_date + "' and to_date >= '" + txt_date + "' and emp_name = '" + en + "' and flag='y'", myConnection);
                    SqlDataReader rdlv = cmdlv.ExecuteReader();
                    while (rdlv.Read())
                    {
                        stats = Convert.ToString(rdlv[0]);
                        leav = Convert.ToString(rdlv[1]);
                        cmd1 = new SqlCommand("update leave_apply set record = 'T' where from_date <= '" + txt_date + "' and to_date >= '" + txt_date + "' and emp_code = '" + ec + "' and emp_name = '" + en + "' and pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
                        cmd1.ExecuteNonQuery();
                    }

                    SqlCommand cmdhd = new SqlCommand("Select pn_Holidaycode, pn_Holidayname from paym_holiday where pn_branchid = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "' and from_date <= '" + txt_date + "' and to_date >= '" + txt_date + "'", myConnection);
                    SqlDataReader rdhd = cmdhd.ExecuteReader();
                    if (rdhd.Read())
                    {
                        leav = Convert.ToString(rdhd[0]);
                        stats = "HH";
                    }

                    SqlCommand cmdod = new SqlCommand("Select * from onduty where pn_branchid = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "' and onduty_dat <= '" + txt_date + "' and todat >= '" + txt_date + "' and empname = '" + en + "' and approval = 'yes' ", myConnection);
                    SqlDataReader rdod = cmdod.ExecuteReader();
                    if (rdod.Read())
                    {
                        leav = "On Duty";
                        stats = "DD";
                    }

                    if (Attend[0].ShiftCode == "SW")
                    {
                        leav = "Week Off";
                        stats = "WW";
                    }

                    cmd = new SqlCommand("insert into temp_studentcard values ('" + employee.CompanyId + "', '" + employee.BranchId + "' ,'" + ec + "','" + en + "','" + Attend[0].ShiftCode + "','" + txt_date + "','" + cur_day + "','" + cin + "','" + cbin + "','" + cbout + "','" + cout + "','" + lin + "','" + lout + "','','" + leav + "','" + stats + "')", myConnection);
                    cmd.ExecuteNonQuery();

                }
                myConnection.Close();
                load1();
            }

            lbl_Error.Text = "";
            Btn_view.Text = "Save";
            Btn_view.CssClass = "btn btn-success";
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Data found for the selected criteria');", true);
        }
    }


    public Collection<Shift> Shift_Details()
    {
        Collection<Shift> Shift = new Collection<Hrms_Employee_Default.Shift>();
        Shift sh = new Shift();
        string dt1 = Convert.ToString(txt_fromdate.Text);
        DateTime curdate = Convert.ToDateTime(txt_fromdate.Text);
        string[] date1 = dt1.Split('/');
        string txt_date = date1[1] + "/" + date1[0] + "/" + date1[2];
        TimeSpan inchk = new TimeSpan(0, 0, 0);
        TimeSpan outchk = new TimeSpan(0, 0, 0);
        TimeSpan out_time = new TimeSpan(0, 0, 0);
        TimeSpan in_limit = new TimeSpan(0, 0, 0);
        TimeSpan breakin_limit = new TimeSpan(0, 0, 0);
        TimeSpan breakout_limit = new TimeSpan(0, 0, 0);
        TimeSpan start_time = new TimeSpan(0, 0, 0);
        TimeSpan breakin_time = new TimeSpan(0, 0, 0);
        TimeSpan breakout_time = new TimeSpan(0, 0, 0);
        TimeSpan otslb = new TimeSpan(0, 0, 0);
        TimeSpan diff = new TimeSpan(0, 0, 0);
        TimeSpan halfLmt = new TimeSpan(0, 0, 0);
        TimeSpan in_time, daily_in, daily_out, ot_hours, ein, eout;
        TimeSpan max_in1, max_breakout1, max_breakin1;
        string lin = "00:00:00", lout = "00:00:00", e_out = "00:00:00";
        DateTime inc, outc, chkin, chkout;
        string otime = "", stime = "", bintime = "", bouttime = "", dtime, intime_lmt, bin_lmt, hd_lmt, bout_lmt, time_stat, time_statin, time_statout, in_chk, bin_chk, hd_chk, bout_chk, str0, str1, str2, lvCode = "", lvName = "", hdCode = "", hdName = "";
        string cin, cbin, cbout, cout, ecod, stats, outdate, dat1, leav = "";
        int otc = 0;


        SqlCommand comm = new SqlCommand("select * from attendance_ceiling where pn_branchid='" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "'", myConnection);
        SqlDataReader rd_out = comm.ExecuteReader();
        if (rd_out.Read())
        {
            // Intime limit 
            time_stat = ""; time_statin = "";
            intime_lmt = Convert.ToString(rd_out["intime"]);
            string[] iti = intime_lmt.Split(' ');
            if (iti.Length == 3)
            {
                time_stat = Convert.ToString(iti[2]);
            }
            in_chk = Convert.ToString(iti[1]);
            string[] str_chk = in_chk.Split(':');
            str0 = Convert.ToString(str_chk[0]);
            if (str0 == "12" && time_stat == "AM")
            {
                in_limit = TimeSpan.Parse("00:" + str_chk[1] + ":00");
            }
            else
            {
                in_limit = TimeSpan.Parse(iti[1]);
            }
            //BreakIntime Limit
            bin_lmt = Convert.ToString(rd_out["lunch_ein"]);
            string[] biti = bin_lmt.Split(' ');
            if (biti.Length == 3)
            {
                time_statin = Convert.ToString(biti[2]);
            }
            bin_chk = Convert.ToString(biti[1]);
            string[] str_chkin = bin_chk.Split(':');
            str1 = Convert.ToString(str_chkin[0]);
            if (str1 == "12" && time_statin == "AM")
            {
                breakin_limit = TimeSpan.Parse("00:" + str_chkin[1] + ":00");
            }
            else
            {
                breakin_limit = TimeSpan.Parse(biti[1]);
            }
            // Half Day Limit
            hd_lmt = Convert.ToString(rd_out["halfday"]);
            string[] hd = hd_lmt.Split(' ');
            if (hd.Length == 3)
            {
                time_statin = Convert.ToString(hd[2]);
            }
            hd_chk = Convert.ToString(hd[1]);
            string[] str_chkhd = hd_chk.Split(':');
            str1 = Convert.ToString(str_chkhd[0]);
            if (str1 == "12" && time_statin == "AM")
            {
                halfLmt = TimeSpan.Parse("00:" + str_chkhd[1] + ":00");
            }
            else
            {
                halfLmt = TimeSpan.Parse(hd[1]);
            }
        }
        rd_out.Close();



        if (curdate.DayOfWeek.ToString() == "Sunday")
        {
            sh.ShiftCode = "WW";
        }
        else if (curdate.DayOfWeek.ToString() == "Saturday")
        {

            SqlCommand cmds = new SqlCommand("select * from studentshiftchange where pn_branchid = '" + employee.BranchId + "' and date='" + txt_date + "' and  pn_companyid = '" + employee.CompanyId + "'", myConnection);
            SqlDataReader dr = cmds.ExecuteReader();
            if (dr.Read())
            {
                sh.ShiftCode = "ST";
            }
            else
            {
                sh.ShiftCode = "WW";
            }
        }
        else
        {
            sh.ShiftCode = "ST";

        }

        //punch time populate

        comm = new SqlCommand("select * from shift_details where shift_code = '" + sh.ShiftCode + "' and pn_branchid='" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "'", myConnection);
        rd_out = comm.ExecuteReader();
        if (rd_out.Read())
        {
            DateTime st, bit, bot, et;
            shift = Convert.ToString(rd_out["shift_code"]);
            //start time
            st = Convert.ToDateTime(rd_out["start_time"]);
            stime = st.ToString("HH:mm:ss");
            start_time = TimeSpan.Parse(stime);
            //break out time
            bit = Convert.ToDateTime(rd_out["break_time_out"]);
            bintime = bit.ToString("HH:mm:ss");
            breakin_time = TimeSpan.Parse(bintime);
            //break in time
            bot = Convert.ToDateTime(rd_out["break_time_in"]);
            bouttime = bot.ToString("HH:mm:ss");
            breakout_time = TimeSpan.Parse(bouttime);
            //end time
            et = Convert.ToDateTime(rd_out["end_time"]);
            otime = et.ToString("HH:mm:ss");
            out_time = TimeSpan.Parse(otime);
        }
        rd_out.Close();
        //max_in1 = start_time + in_limit;
        //max_breakout1 = breakin_time + breakin_limit;
        //max_breakin1 = breakout_time + breakin_limit;
        sh.intime = start_time + in_limit;
        sh.break_intime = breakin_time;
        sh.breakout = breakin_time + breakin_limit;
        sh.breakin = breakout_time + breakin_limit;
        sh.outtime = out_time;
        sh.starttime = start_time;
        sh.halflimit = halfLmt;
        Shift.Add(sh);
        return Shift;
    }

    protected void ddl_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_type.SelectedItem.Text == "Hourly")
        {
            ddl_ename.Enabled = false;
            txt_todate.Visible = false;
            lbl_to.Visible = false;
            lbl_from.Text = "Attendance Date";
            Btn_Generate.Visible = true;
            Btn_import.Visible = false;
            ddl_classname.Enabled = true;
            ddl_hour.Enabled = true;
            //txt_fromdate.Text = "";
        }
        else if(ddl_type.SelectedItem.Text == "Daily")
        {
            ddl_ename.Enabled = false;
            txt_todate.Visible = false;
            lbl_to.Visible = false;
            lbl_from.Text = "Attendance Date";
            Btn_Generate.Visible = false;
            Btn_import.Visible = true;
            ddl_classname.Enabled = false;
            ddl_hour.Enabled = false;
            //txt_fromdate.Text = DateTime.Now.ToShortDateString();
        }
        else if (ddl_type.SelectedItem.Text == "View")
        {
            ddl_ename.Enabled = true;
            txt_todate.Visible = true;
            Image2.Visible = true;
            lbl_to.Visible = true;
            lbl_from.Text = "Attendance From ";
            Btn_Generate.Visible = true;
            Btn_import.Visible = false;
            //txt_fromdate.Text = "";
        }

    }

    protected void Btn_view_Click(object sender, EventArgs e)
    {
        try
        {
            lbl_Error.Text = "";
            myConnection.Open();
            if (Btn_view.Text == "Save")
            {
                foreach (GridViewRow gvr in GridView1.Rows)
                {
                    string datesp = ((Label)gvr.FindControl("lbl_date")).Text;
                    string[] dsp = datesp.Split('/', '-');
                    string findate = dsp[1] + "/" + dsp[0] + "/" + dsp[2];

                    cmd = new SqlCommand("delete from time_card_student where registerno='" + ((Label)gvr.FindControl("lbl_empcode")).Text + "' and dates = '" + findate + "' and pn_companyid = '" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("insert into time_card_student(pn_companyid,pn_branchid, RegisterNo, StudentName, shift_code,dates,days,intime, Late_out, Late_in, outtime, ot_hrs, leave_code, status, data) values ('" + employee.CompanyId + "','" + employee.BranchId + "','" + ((Label)gvr.FindControl("lbl_empcode")).Text + "','" + ((Label)gvr.FindControl("lbl_empname")).Text + "','" + ((Label)gvr.FindControl("lbl_shiftcode")).Text + "','" + findate + "','" + ((Label)gvr.FindControl("lbl_day")).Text + "','" + ((Label)gvr.FindControl("lbl_starttime")).Text + "','" + ((Label)gvr.FindControl("lbl_breaktimeo")).Text + "','" + ((Label)gvr.FindControl("lbl_breaktimei")).Text + "','" + ((Label)gvr.FindControl("lbl_endtime")).Text + "','00:00','" + ((Label)gvr.FindControl("lbl_leave")).Text + "','" + ((Label)gvr.FindControl("lbl_status")).Text + "','R')", myConnection);
                    cmd.ExecuteNonQuery();
                }
                cmd = new SqlCommand("create table temp_card_student(pn_companyid int,pn_branchid int, RegisterNo varchar(15), StudentName varchar(50), shift_code varchar(5),dates datetime,days varchar(15),intime time , break_out time, break_in time , outtime time, Late_in time, Late_out time, ot_hrs time , status varchar(15), leave_code varchar(20), data varchar(15))", myConnection);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("INSERT INTO temp_card_student SELECT DISTINCT * FROM time_card_student where pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("drop table time_card_student", myConnection);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("EXEC sp_rename 'temp_card_student', 'time_card_student'", myConnection);
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Attendance Saved Successfully');", true);
                Btn_view.Text = "View";
                Btn_view.CssClass = "btn btn-info";
                Btn_Sms.Visible = true;
            }

            else if (Btn_view.Text == "View")
            {
                if (s_login_role == "u")
                {
                    if (ddl_type.SelectedItem.Text == "Daily")
                    {
                        string stats = "";
                        TimeSpan tot_ot = new TimeSpan(0, 0, 0);
                        int abs = 0, prs = 0, hday = 0, ldays = 0, rc = 0, wkoff = 0;
                        //DateTime othr;
                        string fdate = txt_fromdate.Text;
                        string tdate = txt_todate.Text;
                        string[] fd = fdate.Split('/');
                        string[] td = tdate.Split('/');
                        string fr_date = fd[1] + "/" + fd[0] + "/" + fd[2];
                        //string to_date = td[1] + "/" + td[0] + "/" + td[2];
                        string to_date = fr_date;
                        string qry = "SELECT * FROM time_card_student where dates between '" + fr_date + "' and '" + to_date + "' and pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' order by dates ";

                        if (ddl_course.SelectedItem.Text == "All")
                        {
                            qry = "select * from time_card_student where pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "' and dates = '" + fr_date + "'";
                        }

                        SqlDataAdapter ada = new SqlDataAdapter(qry, myConnection);
                        DataSet dset = new DataSet();
                        ada.Fill(dset, "time_card_student");

                        if (dset.Tables[0].Rows.Count == 0)
                        {
                            dset.Tables[0].Rows.Add(dset.Tables[0].NewRow());
                            GridView1.DataSource = dset;
                            GridView1.DataBind();
                            int columnCount = GridView1.Rows[0].Cells.Count;
                            GridView1.Rows[0].Cells.Clear();
                            GridView1.Rows[0].Cells.Add(new TableCell());
                            GridView1.Rows[0].Cells[0].ColumnSpan = columnCount;
                            GridView1.Rows[0].Cells[0].Text = "No Records Found..";
                        }
                        else
                        {
                            GridView1.DataSource = dset;
                            GridView1.DataBind();
                        }

                        if (s_login_role == "a")
                        {
                            cmd1 = new SqlCommand("SELECT * FROM time_card_student where dates between '" + fr_date + "' and '" + to_date + "' and pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + ddl_branch.Text + "' and emp_name = '" + ddl_ename.SelectedItem.Text + "' order by dates ", myConnection);
                        }

                        else if (s_login_role == "h")
                        {
                            cmd1 = new SqlCommand("SELECT * FROM time_card_student where dates between '" + fr_date + "' and '" + to_date + "' and pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' and dates = '" + fr_date + "' order by dates ", myConnection);
                        }

                        else if (s_login_role == "u")
                        {
                            cmd1 = new SqlCommand("SELECT * FROM time_card_student where dates between '" + fr_date + "' and '" + to_date + "' and pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' and dates = '" + fr_date + "' order by dates ", myConnection);
                            return;
                        }
                        SqlDataReader rd_calc = cmd1.ExecuteReader();
                        while (rd_calc.Read())
                        {
                            rc++;
                            DateTime othr = Convert.ToDateTime(rd_calc["ot_hrs"]);
                            string otim = othr.ToString("HH:mm");
                            tot_ot += TimeSpan.Parse(otim);

                            stats = Convert.ToString(rd_calc["status"]);
                            if (stats == "XX")
                            {
                                prs += 1;
                            }
                            else if (stats == "AA")
                            {
                                abs += 1;
                            }
                            else if (stats == "WW")
                            {
                                wkoff += 1;
                            }
                            else if (stats != "AA" && stats != "XX" && stats != "WW" && stats != "HH")
                            {
                                ldays += 1;
                            }
                            else if (stats == "HH")
                            {
                                hday += 1;
                            }
                        }
                        //string hours = tot_ot.Hours.ToString();
                        //string min = tot_ot.Minutes.ToString();
                        txt_tdays.Text = rc.ToString();
                        txt_pdays.Text = prs.ToString();
                        txt_adays.Text = abs.ToString();
                        //txt_ot.Text = tot_ot.ToString();
                        Btn_view.Text = "Reset";
                    }

                    else
                    {
                        string stats = "";
                        TimeSpan tot_ot = new TimeSpan(0, 0, 0);
                        int abs = 0, prs = 0, hday = 0, ldays = 0, rc = 0, wkoff = 0;
                        //DateTime othr;
                        string fdate = txt_fromdate.Text;
                        string tdate = txt_todate.Text;
                        string[] fd = fdate.Split('/');
                        string[] td = tdate.Split('/');
                        string fr_date = fd[1] + "/" + fd[0] + "/" + fd[2];
                        //string to_date = td[1] + "/" + td[0] + "/" + td[2];
                        SqlDataAdapter ada = new SqlDataAdapter("SELECT * FROM time_card_student where dates = '" + fr_date + "' and pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' order by dates ", myConnection);

                        DataSet dset = new DataSet();

                        ada.Fill(dset, "temp_studentcard");


                        if (dset.Tables[0].Rows.Count == 0)
                        {
                            dset.Tables[0].Rows.Add(dset.Tables[0].NewRow());
                            GridView1.DataSource = dset;
                            GridView1.DataBind();
                            int columnCount = GridView1.Rows[0].Cells.Count;
                            GridView1.Rows[0].Cells.Clear();
                            GridView1.Rows[0].Cells.Add(new TableCell());
                            GridView1.Rows[0].Cells[0].ColumnSpan = columnCount;
                            GridView1.Rows[0].Cells[0].Text = "No Records Found..";
                        }
                        else
                        {
                            GridView1.DataSource = dset;
                            GridView1.DataBind();
                        }

                        if (s_login_role == "h")
                        {
                            cmd1 = new SqlCommand("SELECT * FROM time_card_student where dates = '" + fr_date + "' and pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' order by dates ", myConnection);
                        }

                        if (s_login_role == "a")
                        {
                            cmd1 = new SqlCommand("SELECT * FROM time_card_student where dates = '" + fr_date + "'  and pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + ddl_branch.Text + "' order by dates ", myConnection);
                        }
                        //cmd1 = new SqlCommand("SELECT * FROM time_card_student where dates = '" + fr_date + "' and pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' order by dates ", myConnection);
                        SqlDataReader rd_calc = cmd1.ExecuteReader();
                        while (rd_calc.Read())
                        {
                            DateTime othr = Convert.ToDateTime(rd_calc["ot_hrs"]);
                            string otim = othr.ToString("HH:mm");
                            tot_ot += TimeSpan.Parse(otim);

                            stats = Convert.ToString(rd_calc["status"]);
                            if (stats == "XX")
                            {
                                prs += 1;
                            }
                            else if (stats == "AA")
                            {
                                abs += 1;
                            }
                            else if (stats == "WW")
                            {
                                wkoff += 1;
                            }
                            else if (stats != "AA" && stats != "XX" && stats != "WW" && stats != "HH")
                            {
                                ldays += 1;
                            }
                            else if (stats == "HH")
                            {
                                hday += 1;
                            }
                        }
                        //string hours = tot_ot.Hours.ToString();
                        //string min = tot_ot.Minutes.ToString();
                        txt_tdays.Text = rc.ToString();
                        txt_pdays.Text = prs.ToString();
                        txt_adays.Text = abs.ToString();
                        //txt_ot.Text = tot_ot.ToString();
                        Btn_view.Text = "Reset";
                    }
                }

            }

            else
            {
                txt_fromdate.Text = "";
                txt_todate.Text = "";
                ddl_type.SelectedIndex = 0;
                ddl_ename.Enabled = false;
                Btn_view.Text = "View";
                Btn_view.CssClass = "btn btn-info";
                ddl_ename.ForeColor = System.Drawing.Color.White;
            }
        }

        catch (Exception ex)
        {
            if (ex.Message == "Index was outside the bounds of the array.")
            {
                lbl_Error.Text = "Error Occured. Check the Date Entered";
            }
            else
            {
                lbl_Error.Text = "Error Occured";
            }
        }

        finally
        {

            myConnection.Close();
        }

    }
    protected void lb_previous_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_ename.SelectedItem.Text = "Select";
    }
    protected void ddl_currentyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        student.CourseName = ddl_course.SelectedItem.Text;
        student.DepartmentName = ddl_department.SelectedItem.Text;
        student.Section = ddl_Section.SelectedItem.Text;
        student.Cyear = ddl_currentyear.SelectedItem.Text;
        ddl_classname.Items.Clear();
        ddl_ename.Items.Clear();
        myConnection.Open();
        SqlCommand cmd = new SqlCommand("Select ClassName from student_class where pn_BranchID = '" + employee.BranchId + "' and  DepartmentName = '" + ddl_department.SelectedItem.Text + "' and section = '" + ddl_Section.SelectedItem.Text + "' and year = '" + ddl_currentyear.SelectedItem.Text + "'", myConnection);
        SqlDataReader rea = cmd.ExecuteReader();
        while (rea.Read())
        {
            ddl_classname.Items.Add(rea[0].ToString());
        }
        myConnection.Close();
        Collection<Student> StudentList = student.fn_getStudentTimeCard(student);
        if (StudentList.Count > 0)
        {

            for (int ddl_i = -1; ddl_i < StudentList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();
                    e_list.Text = "Select";
                    e_list.Value = "0";
                    ddl_ename.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();
                    e_list.Value = StudentList[ddl_i].FirstName;
                    e_list.Text = StudentList[ddl_i].RegisterNo;
                    ddl_ename.Items.Add(e_list);
                }
            }
        }
        else
        {
            ListItem e_list = new ListItem();
            e_list.Text = "No Students Available";
            e_list.Value = "0";
            ddl_ename.Items.Add(e_list);
        }
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        try
        {
            string tm = DateTime.Now.ToString("HH:mm");
            if (tm == "16:38")
            {
                Timer1.Enabled = false;
                Timer1.Dispose();
                MachineList = student.fn_GetMachine(student.BranchId);

                if (MachineList.Count > 0)
                {
                    for (int i = 0; i < MachineList.Count; i++)
                    {
                        str2 = MachineList[i].IPAddr;
                        bIsConnected = CtrlBioComm.Connect_Net(str2, 4370);
                        if (bIsConnected == true)
                        {
                            flag = "1";
                            Collect_Data();
                            Disconnect();
                        }
                    }
                    Download_Data();
                    SendSMS();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No reader Found');", true);
                }

            }
        }
        catch
        {
            
        }
    }

    public void SendSMS()
    {
        string strUserID = "avcce@avccengg.net";
        string strPassword = "Engineering";
        string strMobileNo = null;
        string strRegNo = null;
        string strTextMsg = null;
        string strSMSScheduleDate = null;
        string strGatewayResponse = null;
        string dt1 = DateTime.Now.ToShortDateString();
        string[] date1 = dt1.Split('/');
        string txt_date = date1[1] + "/" + date1[0] + "/" + date1[2];
        myConnection.Open();
        SqlCommand cmd = new SqlCommand("select a.*, b.ParentsContact from time_card_student a , paym_student b where a.RegisterNo = b.RegisterNo and a.dates='" + txt_date + "' and SMSStat != 'Y' and a.pn_branchID = '" + employee.BranchId + "'", myConnection);
        SqlDataReader read = cmd.ExecuteReader();
        while (read.Read())
        {
            strRegNo = read["RegisterNo"].ToString();
            strMobileNo = read["ParentsContact"].ToString();
            if (read["intime"].ToString() != "1900-01-01 00:00:00.000")
            {
                strTextMsg = "Present";
            }
            else
            {
                strTextMsg = "Absent";
            }
            strSMSScheduleDate = "";
            strGatewayResponse = SendSMSUsingBS(strUserID, strPassword, strMobileNo, strTextMsg, strSMSScheduleDate);
            SqlCommand com = new SqlCommand("Update time_card_student set SMSStat = 'Y' where dates = '" + txt_date + "' and RegisterNo = '" + strRegNo + "' and pn_BranchID = '" + employee.BranchId + "'", myConnection);
            com.ExecuteNonQuery();
        }
        read.Close();
        myConnection.Close();
    }

    protected string SendSMSUsingBS(string strUser, string strPassword, string strRecip, string strMsgText, string strSMSScheduleDate = "")
    {
        string strUrl = null;
        strUrl = "http://www.businesssms.co.in/SMS.aspx/";

        strUrl += "?ID=" + HttpUtility.UrlEncode(strUser) + "&Pwd=" + HttpUtility.UrlEncode(strPassword) + "&PhNo=" + HttpUtility.UrlEncode(strRecip) + "&Text=" + HttpUtility.UrlEncode(strMsgText);
        if (!string.IsNullOrEmpty(strSMSScheduleDate))
        {
            strUrl += "&ScheduleAt=" + HttpUtility.UrlEncode(strSMSScheduleDate);
        }
        Uri objURI = new Uri(strUrl);
        WebRequest objWebRequest = WebRequest.Create(objURI);
        WebResponse objWebResponse = objWebRequest.GetResponse();
        Stream objStream = objWebResponse.GetResponseStream();
        StreamReader objStreamReader = new StreamReader(objStream);
        string strHTML = objStreamReader.ReadToEnd();

        return strHTML;
    }

    public void Download_Data()
    {
        try
        {
            myConnection.Open();
            string cmd_text = "IF EXISTS (SELECT * FROM sysobjects WHERE Name = 'temp_studentcard')" + "DROP TABLE temp_studentcard";
            SqlCommand cmd2 = new SqlCommand(cmd_text, myConnection);
            cmd2.ExecuteNonQuery();
            myConnection.Close();

            myConnection.Open();
            string dt1 = DateTime.Now.ToShortDateString();
            string[] date1 = dt1.Split('/');
            string txt_date = date1[1] + "/" + date1[0] + "/" + date1[2];
            DateTime day = Convert.ToDateTime(dt1);
            cur_day = day.DayOfWeek.ToString();

            cmd = new SqlCommand("create table temp_studentcard(pn_companyid int, pn_branchid int, RegisterNo varchar(20), StudentName varchar(50),shift_code varchar(10),dates datetime,days varchar(15),intime time , break_out time, break_in time , outtime time , ot_hrs datetime ,leave_code varchar(20), status varchar(2), SMSStat varchar(5))", myConnection);
            cmd.ExecuteNonQuery();

            if (s_login_role == "h")
            {
                cmd = new SqlCommand("select * from paym_student where pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "' and status = 'Y'", myConnection);
            }
            SqlDataReader rd_ecode1 = cmd.ExecuteReader();
            while (rd_ecode1.Read())
            {
                ec = Convert.ToString(rd_ecode1["RegisterNo"]);
                en = Convert.ToString(rd_ecode1["StudentName"]);
                cmd = new SqlCommand("insert into temp_studentcard(pn_companyid,pn_branchid,RegisterNo,StudentName,shift_code,dates,days,status) values ('" + employee.CompanyId + "', '" + employee.BranchId + "' , '" + ec + "','" + en + "','','" + txt_date + "','" + cur_day + "','AA')", myConnection);
                cmd.ExecuteNonQuery();
            }
            myConnection.Close();

            load1();
            load1();
            load1();
            if (flag == "1")
            {
                myConnection.Open();
                cmd = new SqlCommand("INSERT INTO time_card_student SELECT DISTINCT * FROM temp_studentcard where pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("create table temp_card_student(pn_companyid int,pn_branchid int, RegisterNo varchar(15), StudentName varchar(50), shift_code varchar(5),dates datetime,days varchar(15),intime time , break_out time, break_in time , outtime time , ot_hrs time, leave_code varchar(20), status varchar(5))", myConnection);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("INSERT INTO temp_card_student SELECT DISTINCT * FROM time_card_student where pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("drop table time_card_student", myConnection);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("EXEC sp_rename 'temp_card_student', 'time_card_student'", myConnection);
                cmd.ExecuteNonQuery();
                myConnection.Close();
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "No Data found for the selected criteria";
        }
    }

    public void Disconnect()
    {
        //bool beep_chk = true;
        //beep_chk = CtrlBioComm.Beep(500);
        CtrlBioComm.Disconnect();
        CtrlBioComm.EnableDevice(1, true);
    }

    public void Collect_Data()
    {
        var conStr = ConfigurationManager.ConnectionStrings["connectionstring"];
        string constr = conStr.ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        con.Open();

        if (flag == "1")
        {
            bIsConnected = CtrlBioComm.Connect_Net(str2, 4370);

            bool ret = CtrlBioComm.ReadAllGLogData(1);

            if (ret)
            {
                int dwMachineNum = 1;

                string dwEnrollNum;

                int dwVerifyMode = 0;
                int dwInOutMode = 0;
                int dwyear = 0;
                string year = "";
                string month = "";
                string day = "";
                int dwmonth = 0;
                int dwday = 0;
                int dwhour = 0;
                int dwmin = 0;
                int dwsec = 0;
                int dwworkcode = 0;
                int i = 0;

                SqlCommand cmd2 = new SqlCommand("create table current_details(machine_num int,enroll_num int,VerifyMode int,InOutMode int,year int,month int,day int,hour int,min int,sec int,pn_branchid int,pn_companyid int)", con);
                cmd2.ExecuteNonQuery();

                while (CtrlBioComm.SSR_GetGeneralLogData(dwMachineNum, out dwEnrollNum, out dwVerifyMode, out dwInOutMode, out dwyear, out dwmonth, out dwday, out dwhour, out dwmin, out dwsec, ref dwworkcode))
                {
                    string sc = "", en = "", ec = "";
                    //Cursor.Current = Cursors.WaitCursor;
                    DateTime dateValue = new DateTime(dwyear, dwmonth, dwday);
                    string my = dwmonth.ToString() + "/" + dwyear.ToString();
                    if (dwmonth.ToString().Length == 1)
                    {
                        my = "0" + dwmonth.ToString() + "/" + dwyear.ToString();
                    }
                    string preday = dateValue.DayOfWeek.ToString();
                    cmd2 = new SqlCommand("select a.pattern_code, a.pn_employeecode , b.employee_first_name , b.card_no from shift_balance a inner join paym_employee b on a.pn_employeecode = b.employeecode and b.card_no='" + dwEnrollNum + "' and a.monthyear ='" + my + "' ", con);
                    SqlDataReader reader1 = cmd2.ExecuteReader();
                    if (reader1.Read())
                    {
                        sc = Convert.ToString(reader1[0]);
                        en = Convert.ToString(reader1[2]);
                        ec = Convert.ToString(reader1[1]);
                    }
                    else
                    {
                        cmd2 = new SqlCommand("select RegisterNo, StudentName , ReaderID from paym_student where ReaderID='" + dwEnrollNum + "'", con);
                        reader1 = cmd2.ExecuteReader();
                        if (reader1.Read())
                        {
                            sc = "";
                            en = Convert.ToString(reader1[1]);
                            ec = Convert.ToString(reader1[0]);
                        }
                    }

                    reader1.Close();

                    StringBuilder _data = new StringBuilder();
                    _data.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", dwMachineNum, dwEnrollNum, dwVerifyMode, dwInOutMode, dwyear, dwmonth, dwday, dwhour, dwmin, dwsec, dwworkcode);
                    year = dwyear.ToString();
                    month = dwmonth.ToString();
                    day = dwday.ToString();
                    SqlCommand cmd1 = new SqlCommand("insert into punch_details (pn_companyid,pn_branchid,machine_num,card_no,emp_code, emp_name,VerifyMode,InOutMode, shift_code,dates,days,times) values('" + employee.CompanyId + "','" + employee.BranchId + "','" + dwMachineNum + "','" + dwEnrollNum + "', '" + ec + "' , '" + en + "' , '" + dwVerifyMode + "','" + dwInOutMode + "', '" + sc + "' ,'" + (year + '/' + month + '/' + day) + "', '" + preday + "' ,'" + dwhour + ':' + dwmin + ':' + dwsec + "')", con);
                    cmd1.ExecuteNonQuery();
                    cmd2 = new SqlCommand("insert into current_details (pn_branchid,pn_companyid,Machine_num,Enroll_Num,verifymode,inoutmode,year,month,day,hour,min,sec) values('" + employee.BranchId + "','" + employee.CompanyId + "','" + dwMachineNum + "','" + dwEnrollNum + "','" + dwVerifyMode + "','" + dwInOutMode + "','" + dwyear + "','" + dwmonth + "','" + dwday + "','" + dwhour + "','" + dwmin + "','" + dwsec + "')", con);
                    cmd2.ExecuteNonQuery();
                    i++;
                }

                cmd2 = new SqlCommand("drop table current_details", con);
                cmd2.ExecuteNonQuery();

                cmd = new SqlCommand("create table temp_punch_details(pn_companyid int,pn_branchid int,machine_num int, card_no varchar(15) , emp_code varchar(15), emp_name varchar(50),VerifyMode int,InOutMode int, shift_code varchar(5),dates datetime,days varchar(15),times time , ot_hrs time , status varchar(2))", con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("INSERT INTO temp_punch_details SELECT DISTINCT * FROM punch_details", con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("drop table punch_details", con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("EXEC sp_rename 'temp_punch_details', 'punch_details'", con);
                cmd.ExecuteNonQuery();
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Connection Problem. Check Reader Settings');", true);
        }
    }

    protected void Btn_Generate_Click(object sender, EventArgs e)
    {
        try
        {
            myConnection.Open();
            string cmd_text = "IF EXISTS (SELECT * FROM sysobjects WHERE Name = 'temp_hourlycard')" + "DROP TABLE temp_hourlycard";
            SqlCommand cmd2 = new SqlCommand(cmd_text, myConnection);
            cmd2.ExecuteNonQuery();

            string dt1 = Convert.ToString(txt_fromdate.Text);
            string[] date1 = dt1.Split('/');
            string txt_date = date1[1] + "/" + date1[0] + "/" + date1[2];
            DateTime day = Convert.ToDateTime(txt_fromdate.Text);
            cur_day = day.DayOfWeek.ToString();

            cmd = new SqlCommand("create table temp_hourlycard(pn_companyid int, pn_branchid int, RegisterNo varchar(20), StudentName varchar(50),shift_code varchar(10),dates datetime,days varchar(15),status varchar(2))", myConnection);
            cmd.ExecuteNonQuery();

            if (s_login_role == "h")
            {
                cmd = new SqlCommand("select * from paym_student where pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "' and status = 'Y'", myConnection);
            }
            SqlDataReader rd_ecode1 = cmd.ExecuteReader();
            while (rd_ecode1.Read())
            {
                ec = Convert.ToString(rd_ecode1["RegisterNo"]);
                en = Convert.ToString(rd_ecode1["StudentName"]);
                cmd = new SqlCommand("insert into temp_hourlycard(pn_companyid,pn_branchid,RegisterNo,StudentName,shift_code,dates,days,status) values ('" + employee.CompanyId + "', '" + employee.BranchId + "' , '" + ec + "','" + en + "','ST','" + txt_date + "','" + cur_day + "','')", myConnection);
                cmd.ExecuteNonQuery();
            }
            myConnection.Close();
            loadhour();
            Btn_view.Text = "Save";
            Btn_view.CssClass = "btn btn-success";
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('All feilds are mandatory');", true);
        }
    }
    protected void btn_saveall_Click(object sender, EventArgs e)
    {
        myConnection.Open();
        foreach (GridViewRow gvr in GridHour.Rows)
        {
            string datesp = ((Label)gvr.FindControl("lbl_date0")).Text;
            string[] dsp = datesp.Split('/', '-');
            string findate = dsp[1] + "/" + dsp[0] + "/" + dsp[2];
            cmd = new SqlCommand("insert into time_hour_student(pn_companyid,pn_branchid, RegisterNo, StudentName, shift_code,dates,days,status) values ('" + employee.CompanyId + "','" + employee.BranchId + "','" + ((Label)gvr.FindControl("lbl_empcode0")).Text + "','" + ((Label)gvr.FindControl("lbl_empname0")).Text + "','" + ((Label)gvr.FindControl("lbl_shiftcode0")).Text + "','" + findate + "','" + ((Label)gvr.FindControl("lbl_day0")).Text + "','" + ((DropDownList)gvr.FindControl("lbl_status0")).Text + "')", myConnection);
            cmd.ExecuteNonQuery();
        }
        cmd = new SqlCommand("create table temp_hour_student(pn_companyid int,pn_branchid int, RegisterNo varchar(15), StudentName varchar(50), shift_code varchar(5),dates datetime,days varchar(15),status varchar(5))", myConnection);
        cmd.ExecuteNonQuery();
        cmd = new SqlCommand("INSERT INTO temp_hour_student SELECT DISTINCT * FROM time_hour_student where pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
        cmd.ExecuteNonQuery();
        cmd = new SqlCommand("drop table time_hour_student", myConnection);
        cmd.ExecuteNonQuery();
        cmd = new SqlCommand("EXEC sp_rename 'temp_hour_student', 'time_hour_student'", myConnection);
        cmd.ExecuteNonQuery();
        myConnection.Close();
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Record Saved Successfully');", true);
    }

    protected void Btn_Sms_Click(object sender, EventArgs e)
    {
        string strUserID = "avcce@avccengg.net";
        string strPassword = "Engineering";
        string strMobileNo = null;
        string strRegNo = null;
        string strName = null;
        string strTextMsg = null;
        string strSMSScheduleDate = null;
        string strGatewayResponse = null;
        string dt1 = txt_fromdate.Text;
        string[] date1 = dt1.Split('/');
        string txt_date = date1[1] + "/" + date1[0] + "/" + date1[2];
        myConnection.Open();
        SqlCommand cmd = new SqlCommand("select a.*, b.ParentsContact from time_card_student a , paym_student b where b.Department = '" + ddl_department.SelectedItem.Text + "' and b.Section = '" + ddl_Section.SelectedItem.Text + "' and b.currentyear = '" + ddl_currentyear.SelectedItem.Text + "' and a.RegisterNo = b.RegisterNo and a.dates='" + txt_date + "' and a.Data != 'Y' and a.pn_branchID = '" + employee.BranchId + "' order by a.RegisterNo asc", myConnection);
        SqlDataReader read = cmd.ExecuteReader();
        while (read.Read())
        {
            strName = read["StudentName"].ToString();
            strRegNo = read["RegisterNo"].ToString();
            strMobileNo = "91" + read["ParentsContact"].ToString();
            string strin = read["intime"].ToString();
            //string[] insplt = strin.Split(' ');
            //strin = insplt[1];
            if (strin == "00:00:00.000" || strin == "12:00:00.000" || strin == "00:00:00" || strin == "12:00:00")
            {

                strTextMsg = "Your Ward " + strName + " is Absent Today.";
                strSMSScheduleDate = "";
                strGatewayResponse = SendSMSUsingBS(strUserID, strPassword, strMobileNo, strTextMsg, strSMSScheduleDate);
                SqlCommand com = new SqlCommand("Update time_card_student set Data = 'Y' where dates = '" + txt_date + "' and RegisterNo = '" + strRegNo + "' and pn_BranchID = '" + employee.BranchId + "'", myConnection);
                com.ExecuteNonQuery();
            }
            //else
            //{
            //    strTextMsg = "Present";
            //}
            
        }
        read.Close();
        myConnection.Close();
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('SMS Sent Successfully');", true);
    }
    protected void ddl_course_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_course.SelectedItem.Text == "All")
        {
            ddl_department.Enabled = false;
            ddl_Section.Enabled = false;
            ddl_currentyear.Enabled = false;
        }
        else
        {
            ddl_department.Enabled = true;
            ddl_Section.Enabled = true;
            ddl_currentyear.Enabled = true;
        }
    }
}
