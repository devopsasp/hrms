using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ePayHrms.Company;
using ePayHrms.Employee;

public partial class Hrms_PayRoll_Default : System.Web.UI.Page
{
    Employee employee = new Employee();
    PayRoll pay = new PayRoll();
    Company company = new Company();
    static int dayCount, count;
    Collection<Employee> emplist;
    Collection<PayRoll> paylist;
    Collection<Company> CompanyList, ddlBranchsList;
    Collection<Employee> EmployeeList;
    Collection<PayRoll> OT_Settings;
    
    protected string clientid = "";
    int year, month, days;
    string ch, s_login_role;
    string calcdays, str_value, y, m, d, d1, dow, _Value, s_form;
    
    ArrayList AlDay = new ArrayList();
    DateTime dt;
    DataSet ds_userrights;

    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand com = new SqlCommand();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);

        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

       
        Label5.Visible = false;
        grid_input.Visible = true;
        s_login_role = Request.Cookies["Login_temp_Role"].Value;

        if (!IsPostBack)
        {
            date_load();
            grd_mcalc();
            //row_category.Visible = false;
            //div_grd.Visible = false;
            
            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {
                //ddl_year_load();
                switch (s_login_role)
                {
                    case "a":
                        tbl_details.Visible = false;
                        ddl_Branch_load();
                        break;

                    case "h":
                        tbl_details.Visible = true;
                        ddl_Branch.Visible = false;
                        break;

                    case "u": 
                        s_form = "39";

                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            tbl_details.Visible = true;
                            ddl_Branch.Visible = false;
                            break;
                        }
                        else
                        {
                            Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator.";
                            Response.Redirect("~/Company_Home.aspx");
                        }
                        //hr();
                        //session_check();
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

    public void admin()
    {
        //manually
        //if (Convert.ToString(Session["ses_report"]) == "1")
        //{
        //    EmployeeList = employee.fn_getAllEmployees();
        //}
        //else
        //{
        //    EmployeeList = employee.fn_getOldEmployees();

        //}
        //19-05-2009
        //EmployeeList = employee.fn_getAllEmployees();
        //if (EmployeeList.Count > 0)
        //{
        //    chk_Empcode.DataSource = EmployeeList;
        //    chk_Empcode.DataTextField = "LastName";
        //    sachk_Empcode.DataValueField = "EmployeeId";
        //    chk_Empcode.DataBind();
        //}
        //else
        //{
        //    lbl_error.Text = "No employees";

        //}


        //employee.temp_str = "select * from Temp_Employee";

        //EmpFirstList = employee.Temp_Selected_EmployeeList(employee);

        //if (EmpFirstList.Count == 0)
        //{
        //    temp_tables();

        //}  
    }
    
    public void hr()
    {
        try
        {
            if (Convert.ToString(Session["ses_report"]) == "1")
            {
                EmployeeList = employee.fn_getEmployeeList(employee);
            }
            else
            {
                EmployeeList = employee.fn_getOldEmployeeList(employee);
            }
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Error Occured.');};", true);
        }
    }


    public void session_check()
    {
        switch (Request.Cookies["Query_Session"].Value)
        {
            case "start":
              
                Response.Cookies["Query_Session"].Value= "start";
                break;

            case "nil":
               
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('No Result Found.');};", true);
                Response.Cookies["Query_Session"].Value= "start";
                break;

            case "back":
              
                Response.Cookies["Query_Session"].Value= "start";
                break;

            default:
                final_query_execute();
                break;
        }
    }

    public void final_query_execute()
    {
        employee.temp_str = Request.Cookies["Query_Session"].Value;
        EmployeeList = employee.Temp_Selected_EmployeeList(employee);

        if (EmployeeList.Count > 0)
        {
            //19-05-2009
            //for (i = 0; i < chk_Empcode.Items.Count; i++)
            //{
            //    for (j = 0; j < EmployeeList.Count; j++)
            //    {
            //        if (Convert.ToInt32(chk_Empcode.Items[i].Value) == EmployeeList[j].EmployeeId)
            //        {
            //            chk_Empcode.Items[i].Selected = true;
            //        }
            //    }
            //}
            
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('"+EmployeeList.Count+"' Employees Selected!);};", true);
            Response.Cookies["Query_Session"].Value= "start";
        }
        else
        {
          
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('No Employees has been selected.');};", true);
            Response.Cookies["Query_Session"].Value= "start";
        }
    }
    
    protected void edit(object sender, GridViewEditEventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                pay.BranchId = (int)ViewState["Payinput_BranchID"];
            }
            double basic = 0, ern_basic = 0, ot_calc = 0, ot_amt = 0, ovt = 0, otround = 0;
            TimeSpan tot_ot = new TimeSpan(0, 0, 0);
            pay.EmployeeId = Convert.ToInt32(((HtmlInputHidden)grid_input.Rows[e.NewEditIndex].FindControl("grdhd_txt_empid")).Value);
            pay.EmployeeCode = Convert.ToString(((Label)grid_input.Rows[e.NewEditIndex].FindControl("grd_txt_employeecode")).Text);
            year = Int32.Parse(ddl_Year.SelectedValue);
            month = Int32.Parse(ddl_Month.SelectedValue);
            days = Convert.ToInt32(DateTime.DaysInMonth(year, month));
            y = Convert.ToString(year);
            m = Convert.ToString(month);
            d = Convert.ToString(days);
            d1 = "01";
            pay.d_date = Convert.ToDateTime(y + "/" + m + "/" + d1);
            pay.d_FromDate = Convert.ToDateTime(y + "/" + m + "/" + d1);
            pay.d_ToDate = Convert.ToDateTime(y + "/" + m + "/" + d);
            pay.strDate = y + "/" + m + "/" + d;
            pay.Calc_Days = dayCount;
            pay.Paid_Days = Convert.ToDouble(((TextBox)grid_input.Rows[e.NewEditIndex].FindControl("grd_txt_paiddays")).Text);
            pay.Present_Days = Convert.ToDouble(((TextBox)grid_input.Rows[e.NewEditIndex].FindControl("grd_txt_presdays")).Text);
            pay.Absent_Days = Convert.ToDouble(((TextBox)grid_input.Rows[e.NewEditIndex].FindControl("grd_txt_absdays")).Text);
            pay.TotLeave_Days = Convert.ToDouble(((TextBox)grid_input.Rows[e.NewEditIndex].FindControl("txt_leavedays")).Text);
            pay.WeekOffDays = count;
            pay.Holidays = Convert.ToDouble(((TextBox)grid_input.Rows[e.NewEditIndex].FindControl("text_holiday")).Text);
            pay.OnDuty_days = Convert.ToDouble(((TextBox)grid_input.Rows[e.NewEditIndex].FindControl("txt_dutyday")).Text);
            pay.Compoff_Days = Convert.ToDouble(((TextBox)grid_input.Rows[e.NewEditIndex].FindControl("txt_compday")).Text);
            pay.Tour_Days = Convert.ToDouble(((TextBox)grid_input.Rows[e.NewEditIndex].FindControl("txt_tourday")).Text);
            //pay.Att_Bonus = Convert.ToString(((TextBox)grid_input.Rows[e.NewEditIndex].FindControl("text_attbonus")).Text);//'m';
            pay.Att_Bonus = "0";
            pay.Att_BonusAmount = 0;//Convert.ToDouble(((HtmlInputText)grid_input.Rows[e.NewEditIndex].FindControl("text_attbonusamount")).Value);
            pay.temp_str = Convert.ToString(((TextBox)grid_input.Rows[e.NewEditIndex].FindControl("text_othours")).Text);
            pay.Earn_Arrears = 0;//Convert.ToDouble(((HtmlInputText)grid_input.Rows[e.NewEditIndex].FindControl("txt_earnarrear")).Value);
            pay.Ded_Arrears = 0;//Convert.ToDouble(((HtmlInputText)grid_input.Rows[e.NewEditIndex].FindControl("txt_dedarrear")).Value);
            if (pay.temp_str == "0")
            {
                pay.temp_str = "00:00";
            }
            tot_ot = TimeSpan.Parse(pay.temp_str);
            string str_ot = "1900-01-01 " + pay.temp_str + ":00.000";
            string[] ot_spl = pay.temp_str.Split(':');
            if (pay.temp_str != "0")
            {
                if (ot_spl[1] == "15")
                {
                    otround = 0.25;
                }
                else if (ot_spl[1] == "30")
                {
                    otround = 0.5;
                }
                else if (ot_spl[1] == "45")
                {
                    otround = 0.75;
                }
                else
                {
                    otround = 0;
                }
            }
            ovt = Convert.ToDouble(ot_spl[0]) + otround;
            con.Open();
            com = new SqlCommand("SELECT top 1 * FROM salary_structure where  pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' and pn_EmployeeID = '" + pay.EmployeeId + "' and effective_date <= '" + pay.strDate + "' order by effective_date desc ", con);
            SqlDataReader rd_bas = com.ExecuteReader();
            if (rd_bas.Read())
            {
                basic = Convert.ToDouble(rd_bas["salary"]);
            }

            com = new SqlCommand("update paym_employee set basic_salary = '" + basic + "' where pn_EmployeeID='" + pay.EmployeeId + "' and pn_BranchID = '" + employee.BranchId + "' and pn_CompanyID='" + employee.CompanyId + "'", con);
            com.ExecuteNonQuery();

            com = new SqlCommand("SELECT OT_calc FROM paym_employee where  pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' and pn_EmployeeID = '" + pay.EmployeeId + "' ", con);
            SqlDataReader rd_otc = com.ExecuteReader();
            if (rd_otc.Read())
            {
                ot_calc = Convert.ToDouble(rd_otc[0]);
            }
            ern_basic = (basic / pay.Calc_Days) * pay.Paid_Days;

            if (ovt > 0)
            {
                OT_Settings = pay.fn_In_OT_Settings1(pay);

                if (OT_Settings.Count > 0)
                {
                    ot_amt = ((basic / OT_Settings[0].OT_Days) / OT_Settings[0].OT_HRS) * ovt * ot_calc;
                }
            }

            pay.Act_Basic = basic;
            pay.Earned_Basic = ern_basic;
            pay.Date = Convert.ToDateTime(str_ot);
            pay.OT_Days = ovt;
            pay.OT_Amt = ot_amt;
            con.Close();
            _Value = pay.payinput_insert(pay);
            if (_Value != "1")
            {
               // Label7.Text = "Saved";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
               // ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Added Successfully.');};", true);
            }
            else
            {
              //  Label7.Text = "Error Occured";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Insert Failed')", true);
               // ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Error Occured.');};", true);
            }
            if (ddl_category.SelectedValue == "0")
            {
                get_load_alldetails();
            }
            else
            {
                grid_load_id();
            }
            //((LinkButton)grid_input.Rows[e.NewEditIndex].FindControl("img_edit")).Visible = true;
            ((LinkButton)grid_input.Rows[e.NewEditIndex].FindControl("img_save")).Visible = true;
            exam();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Enter OT Hours in 'HH:MM' Format.')", true);

            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Enter OT Hours in 'HH:MM' Format.');};", true);
        }
    }

    public void grid_load_id()
    {
       
        if (s_login_role == "a")
        {
            //employee.BranchId = (int)ViewState["Payinput_BranchID"];
            pay.BranchId = (int)ViewState["Payinput_BranchID"];
        }
     
        pay.DepartmentID = Convert.ToInt32(ddl_department.SelectedValue);
        //paylist = pay.fn_getemployeeid(pay);
        year = Int32.Parse(ddl_Year.SelectedValue);
        month = Int32.Parse(ddl_Month.SelectedValue);
        y = Convert.ToString(year);
        m = Convert.ToString(month);
        d = "01";
        pay.d_date = Convert.ToDateTime(m + "/" + d + "/" + y);

        if (ddl_category.SelectedValue == "0")
        {
        }
        else if (ddl_category.SelectedValue == "1")
        {
            pay.CategoryID = "pn_EmployeeID";
        }
        else if (ddl_category.SelectedValue == "2")
        {
            pay.CategoryID = "pn_DepartmentId";
        }
        else if (ddl_category.SelectedValue == "3")
        {
            pay.CategoryID = "pn_DesingnationId";
        }
        else if (ddl_category.SelectedValue == "4")
        {
            pay.CategoryID = "pn_CategoryId";
        }
        
        paylist = pay.fn_Employeedetails_deptid(pay);
        if (paylist.Count > 0)
        {
            grid_input.DataSource = paylist;
            grid_input.DataBind();
        }
        else
        {
           
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('No Data.');};", true);
        }
        for (int i = 0; i < grid_input.Rows.Count; i++)
        {
            for (int j = 0; j < paylist.Count; j++)
            {
                if (Convert.ToInt32(((HtmlInputHidden)grid_input.Rows[i].FindControl("grdhd_txt_empid")).Value) == paylist[j].ex_EmployeeID)
                {
                    ((LinkButton)grid_input.Rows[i].FindControl("img_save")).BackColor = System.Drawing.Color.LightSeaGreen;
                }
            }
        }
       // days_calculation(paylist);
        //days_check();
        exam();
    }

    protected void update(object sender, GridViewUpdateEventArgs e)
    {
        ((HtmlInputText)grid_input.Rows[e.RowIndex].FindControl("grd_txt_employeecode")).Disabled = true;
        //((DropDownList)grid_input.Rows[e.RowIndex].FindControl("grid_ddl_employee")).Enabled = false;
        //((HtmlInputText)grid_input.Rows[e.RowIndex].FindControl("grid_txt_date")).Disabled = true;
        //((HtmlInputText)grid_input.Rows[e.RowIndex].FindControl("grid_txt_fromdate")).Disabled = true;
        //((HtmlInputText)grid_input.Rows[e.RowIndex].FindControl("grid_txt_todate")).Disabled = true;
        ((TextBox)grid_input.Rows[e.RowIndex].FindControl("grd_txt_calcdays")).Enabled = false;
        ((TextBox)grid_input.Rows[e.RowIndex].FindControl("grd_txt_paiddays")).Enabled = false;
        ((TextBox)grid_input.Rows[e.RowIndex].FindControl("grd_txt_presdays")).Enabled = false;
        ((TextBox)grid_input.Rows[e.RowIndex].FindControl("grd_txt_absdays")).Enabled = false;
        ((TextBox)grid_input.Rows[e.RowIndex].FindControl("txt_leavedays")).Enabled = false;
        ((TextBox)grid_input.Rows[e.RowIndex].FindControl("txt_leavedays")).Enabled = false;
        ((TextBox)grid_input.Rows[e.RowIndex].FindControl("text_holiday")).Enabled = false;
        ((TextBox)grid_input.Rows[e.RowIndex].FindControl("txt_dutyday")).Enabled = false;
        ((TextBox)grid_input.Rows[e.RowIndex].FindControl("txt_compday")).Enabled = false;
        ((TextBox)grid_input.Rows[e.RowIndex].FindControl("txt_tourday")).Enabled = false;
        ((TextBox)grid_input.Rows[e.RowIndex].FindControl("text_attbonus")).Enabled = false;
        //((HtmlInputText)grid_input.Rows[e.RowIndex].FindControl("text_attbonusamount")).Disabled = true;
        ((TextBox)grid_input.Rows[e.RowIndex].FindControl("text_othours")).Enabled = false;
        ((TextBox)grid_input.Rows[e.RowIndex].FindControl("grdhd_txt_empid")).Enabled = false;
        //((HtmlInputText)grid_input.Rows[e.RowIndex].FindControl("txt_earnarrear")).Disabled = true;
        //((HtmlInputText)grid_input.Rows[e.RowIndex].FindControl("txt_dedarrear")).Disabled = true;
        ((LinkButton)grid_input.Rows[e.RowIndex].FindControl("img_edit")).Visible = false;
        ((LinkButton)grid_input.Rows[e.RowIndex].FindControl("img_save")).Visible = true;

        string str = e.RowIndex.ToString();
        ((TextBox)grid_input.Rows[e.RowIndex].FindControl("grd_txt_presdays")).Attributes.Add("onBlur", "add_paidday(" + str + ");");
        ((TextBox)grid_input.Rows[e.RowIndex].FindControl("txt_leavedays")).Attributes.Add("onBlur", "add_paidday(" + str + ");");
        ((TextBox)grid_input.Rows[e.RowIndex].FindControl("txt_leavedays")).Attributes.Add("onBlur", "add_paidday(" + str + ");");
        ((TextBox)grid_input.Rows[e.RowIndex].FindControl("txt_dutyday")).Attributes.Add("onBlur", "add_paidday(" + str + ");");
        ((TextBox)grid_input.Rows[e.RowIndex].FindControl("txt_tourday")).Attributes.Add("onBlur", "add_paidday(" + str + ");");
        ((TextBox)grid_input.Rows[e.RowIndex].FindControl("txt_compday")).Attributes.Add("onBlur", "add_paidday(" + str + ");");
    }

    public void date_load()
    {
       int cur_yr = DateTime.Now.Year;
        DateTime tnow = DateTime.Now;
        ArrayList AlYear = new ArrayList();
        int i;
        for (i = cur_yr-1; i <= cur_yr; i++)
            AlYear.Add(i);
        ArrayList AlMonth = new ArrayList();
        for (i = 1; i <= 12; i++)
        {
            AlMonth.Add(i);
        }
        if (!this.IsPostBack)
        {
            ddl_Year.DataSource = AlYear;
            ddl_Year.DataBind();
            ddl_Year.SelectedValue = tnow.Year.ToString();
            ddl_Month.SelectedValue = tnow.Month.ToString();
            //days_check();
        }
    }

    public void grd_mcalc()
    {
        paylist = pay.calc_days(pay);
        
        if (paylist.Count > 0)
        {
            lblcalcmethod.Text = paylist[0].Status;

            if (paylist[0].Status == "Month Days")
            {
                pay.Month_Calc = 'M';
            }
            else if (paylist[0].Status == "Week-off ex")
            {
                pay.Month_Calc = 'W';
            }
            else if (paylist[0].Status == "Manual Days")
            {
                pay.Month_Calc = 'D';
            }
            hdtxt_monthcalc.Value = pay.Month_Calc.ToString();
        }
    }

    public void days_check()
    {
        int datenum;
        pay.CompanyId = 1;
        paylist = pay.calc_days(pay);
        year = Int32.Parse(ddl_Year.SelectedValue);
        month = Int32.Parse(ddl_Month.SelectedValue);
        y = Convert.ToString(year);
        m = Convert.ToString(month);
        datenum = Convert.ToInt32(DateTime.DaysInMonth(year, month));
        int holi_ct = holiday_count();

        for (int grd = 0; grd <= grid_input.Rows.Count - 1; grd++)
        {
            TextBox calc_name = ((TextBox)grid_input.Rows[grd].FindControl("grd_txt_calcdays"));
            TextBox paid_name = ((TextBox)grid_input.Rows[grd].FindControl("grd_txt_paiddays"));
            TextBox abs_name = ((TextBox)grid_input.Rows[grd].FindControl("grd_txt_absdays"));
            TextBox week_val = ((TextBox)grid_input.Rows[grd].FindControl("text_weekdays"));
            TextBox holiday_val = ((TextBox)grid_input.Rows[grd].FindControl("text_holiday"));

            holiday_val.Text = Convert.ToString(holi_ct);
            int pres_name = Convert.ToInt32(((TextBox)grid_input.Rows[grd].FindControl("grd_txt_presdays")).Text);
            int leave_name = Convert.ToInt32(((TextBox)grid_input.Rows[grd].FindControl("txt_leavedays")).Text);
            int week_name = Convert.ToInt32(((TextBox)grid_input.Rows[grd].FindControl("text_weekdays")).Text);
            int duty_name = Convert.ToInt32(((TextBox)grid_input.Rows[grd].FindControl("txt_dutyday")).Text);
            int tour_name = Convert.ToInt32(((TextBox)grid_input.Rows[grd].FindControl("txt_tourday")).Text);
            int comp_name = Convert.ToInt32(((TextBox)grid_input.Rows[grd].FindControl("txt_compday")).Text);

            int count = 0;
            
            if (pay.Month_Calc == 'D')
            {
                calc_name.Text = Convert.ToString(pay.Calc_Days);
            }
            
            else if (pay.Month_Calc == 'M')
            {
                calc_name.Text = Convert.ToString(datenum);

                for (int dcount = 1; dcount <= datenum; dcount++)
                {
                    d = Convert.ToString(dcount);
                    dt = DateTime.Parse(y + "/" + m + "/" + d);
                    dow = Convert.ToString(dt.DayOfWeek);
                    if (dow == "Sunday")
                    {
                        count = count + 1;
                    }
                }
                week_val.Text = Convert.ToString(count);
            }
            
            else if (pay.Month_Calc == 'W')
            {                               
                for (int dcount = 1; dcount <= datenum; dcount++)
                {
                    d = Convert.ToString(dcount);
                    dt = DateTime.Parse(y + "/" + m + "/" + d);
                    dow = Convert.ToString(dt.DayOfWeek);
                    
                    if (pay.Week_Holiday1 != pay.Week_Holiday2)
                    {
                        if (dow == pay.Week_Holiday1)
                        {
                            count = count + 1;
                        }
                        if (dow == pay.Week_Holiday2)
                        {
                            count = count + 1;
                        }
                    }

                    else if (pay.Week_Holiday1 == pay.Week_Holiday2)
                    {
                        if (pay.Week_Holiday1 == "None" && pay.Week_Holiday2 == "None")
                        {
                            count = 0;
                        }
                        else if (dow == pay.Week_Holiday1 && dow == pay.Week_Holiday2)
                        {
                            count = count + 1;
                        }
                    }
                }
                //count = weekofday_calc();
                week_val.Text = Convert.ToString(count);
                calc_name.Text = Convert.ToString(datenum - count);
                //paid_name.Value = Convert.ToString(pres_name + leave_name + duty_name + tour_name + comp_name);
                //abs_name.Value = Convert.ToString(Convert.ToInt32(calc_name.Value) - Convert.ToInt32(paid_name.Value));
                // }
            }
        }
    }

    public void get_load_alldetails()
    {
        ddl_Month.Enabled = false;
        ddl_Year.Enabled = false;
        ddl_category.Enabled = false;
        ddl_department.Enabled = false;
        if (s_login_role == "a")
        {
            pay.BranchId = (int)ViewState["Payinput_BranchID"];
        }
        year = Int32.Parse(ddl_Year.SelectedValue);
        month = Int32.Parse(ddl_Month.SelectedValue);
        y = Convert.ToString(year);
        m = Convert.ToString(month);
        d = "01";
        //pay.d_date = Convert.ToDateTime(m + "/" + d + "/" + y);
        //pay.d_date = Convert.ToDateTime(m + "/" + d + "/" + y);
        pay.d_date = Convert.ToDateTime(m + "/" + d + "/" + y);
        paylist = pay.fn_getAll_Employeedetails(pay);
        if (paylist.Count > 0)
        {
            grid_input.DataSource = paylist;
            grid_input.DataBind();


            for (int i = 0; i < grid_input.Rows.Count; i++)
            {
                if (paylist[i].OT == '0')
                {
                    ((TextBox)grid_input.Rows[i].FindControl("text_othours")).Enabled = false;
                }

                for (int j = 0; j < paylist.Count; j++)
                {
                    if (Convert.ToInt32(((HtmlInputHidden)grid_input.Rows[i].FindControl("grdhd_txt_empid")).Value) == paylist[j].ex_EmployeeID)
                    {
                        ((LinkButton)grid_input.Rows[i].FindControl("img_save")).BackColor = System.Drawing.Color.LightSeaGreen;
                    }
                }
            }
            days_calculation(paylist);
        }
         //days_check();
   }

    protected void btn_refresh_Click(object sender, EventArgs e)
    {
        get_load_alldetails();
        exam();
        ddl_category.SelectedValue = "0";
        //txt_department.Visible = false;
        //ddl_department.Visible = false;
        row_category.Visible = true;
        div_grd.Visible = true;
    }

    protected void btn_saveall_Click(object sender, EventArgs e)
    {
        //if (grid_input.Rows.Count > 0)
        //{
        //    for (int i = 0; i <= grid_input.Rows.Count-1; i++)
        //    {
        //foreach (GridViewRow row in grid_input.Rows)
        //{
        //    int i = row.RowIndex;
        //    pay.CompanyId = 1;
        //    pay.EmployeeId = Convert.ToInt32(((HtmlInputHidden)grid_input.Rows[i].FindControl("grdhd_txt_empid")).Value);
        //    pay.EmployeeCode = Convert.ToString(((HtmlInputText)grid_input.Rows[i].FindControl("grd_txt_employeecode")).Value);
        //    year = Int32.Parse(ddl_Year.SelectedValue);
        //    month = Int32.Parse(ddl_Month.SelectedValue);
        //    days = Convert.ToInt32(DateTime.DaysInMonth(year, month));
        //    y = Convert.ToString(year);
        //    m = Convert.ToString(month);
        //    d = Convert.ToString(days);
        //    d1 = "01";
        //    pay.d_date = Convert.ToDateTime(y + "/" + m + "/" + d1);
        //    pay.d_FromDate = Convert.ToDateTime(y + "/" + m + "/" + d1);
        //    pay.d_ToDate = Convert.ToDateTime(y + "/" + m + "/" + d);
        //    pay.EmployeeId = Convert.ToInt32(((DropDownList)grid_input.Rows[e.NewEditIndex].FindControl("grid_ddl_employee")).SelectedItem.Value);
        //    pay.d_date = Convert.ToDateTime(((HtmlInputText)grid_input.Rows[e.NewEditIndex].FindControl("grid_txt_date")).Value);
        //    pay.d_FromDate = Convert.ToDateTime(((HtmlInputText)grid_input.Rows[e.NewEditIndex].FindControl("grid_txt_fromdate")).Value);
        //    pay.d_ToDate = Convert.ToDateTime(((HtmlInputText)grid_input.Rows[e.NewEditIndex].FindControl("grid_txt_todate")).Value);
        //    pay.Calc_Days = Convert.ToDouble(ddl_Day.Items.Count);
        //    pay.Calc_Days = Convert.ToDouble(((HtmlInputText)grid_input.Rows[i].FindControl("grd_txt_calcdays")).Value);
        //    pay.Paid_Days = Convert.ToDouble(((HtmlInputText)grid_input.Rows[i].FindControl("grd_txt_paiddays")).Value);
        //    pay.Present_Days = Convert.ToDouble(((HtmlInputText)grid_input.Rows[i].FindControl("grd_txt_presdays")).Value);
        //    pay.Absent_Days = Convert.ToDouble(((HtmlInputText)grid_input.Rows[i].FindControl("grd_txt_absdays")).Value);
        //    pay.TotLeave_Days = Convert.ToDouble(((HtmlInputText)grid_input.Rows[i].FindControl("txt_leavedays")).Value);
        //    pay.WeekOffDays = Convert.ToDouble(((HtmlInputText)grid_input.Rows[i].FindControl("text_weekdays")).Value);
        //    pay.Holidays = Convert.ToDouble(((HtmlInputText)grid_input.Rows[i].FindControl("text_holiday")).Value);
        //    pay.OnDuty_days = Convert.ToDouble(((HtmlInputText)grid_input.Rows[i].FindControl("txt_dutyday")).Value);
        //    pay.Compoff_Days = Convert.ToDouble(((HtmlInputText)grid_input.Rows[i].FindControl("txt_compday")).Value);
        //    pay.Tour_Days = Convert.ToDouble(((HtmlInputText)grid_input.Rows[i].FindControl("txt_tourday")).Value);
        //    pay.Att_Bonus = Convert.ToString(((HtmlInputText)grid_input.Rows[i].FindControl("text_attbonus")).Value);//'m';
        //    pay.Att_BonusAmount = 0;//Convert.ToDouble(((HtmlInputText)grid_input.Rows[e.NewEditIndex].FindControl("text_attbonusamount")).Value);
        //    pay.OT_HRS = Convert.ToDouble(((HtmlInputText)grid_input.Rows[i].FindControl("text_othours")).Value);

        //    pay.Earn_Arrears = 0;//Convert.ToDouble(((HtmlInputText)grid_input.Rows[e.NewEditIndex].FindControl("txt_earnarrear")).Value);
        //    pay.Ded_Arrears = 0;//Convert.ToDouble(((HtmlInputText)grid_input.Rows[e.NewEditIndex].FindControl("txt_dedarrear")).Value);
        //    pay.payinput_insert(pay);
        //    det_load();


        //    ((LinkButton)grid_input.Rows[i].FindControl("img_edit")).Visible = true;
        //    ((LinkButton)grid_input.Rows[i].FindControl("img_save")).Visible = false;
        //    }
        //    get_load_alldetails();
        //}
        
    }
    public int holiday_count()
    {
        int datenum, hcount = 0;
        paylist = pay.fn_Holiday();
        //string holiday_count = Convert.ToString(paylist.Count);
        year = Int32.Parse(ddl_Year.SelectedValue);
        month = Int32.Parse(ddl_Month.SelectedValue);
        y = Convert.ToString(year);
        m = Convert.ToString(month);
        datenum = Convert.ToInt32(DateTime.DaysInMonth(year, month));
        for (int dcount = 1; dcount <= datenum; dcount++)
        {
            for (int i = 0; i < paylist.Count; i++)
            {
                d = Convert.ToString(dcount);
                dt = DateTime.Parse(y + "/" + m + "/" + d);
                if (dt == paylist[i].holidaydate)
                {
                    hcount = hcount + 1;
                }
            }
        }
        return hcount;
    }

    public void exam()
    {
        foreach (GridViewRow row in grid_input.Rows)
        {
            
            int i = row.RowIndex;
            string str = row.RowIndex.ToString();
            ((TextBox)grid_input.Rows[i].FindControl("grd_txt_presdays")).Attributes.Add("onBlur", "add_paidday(" + str + ");");
            ((TextBox)grid_input.Rows[i].FindControl("txt_leavedays")).Attributes.Add("onBlur", "add_paidday(" + str + ");");
            ((TextBox)grid_input.Rows[i].FindControl("txt_leavedays")).Attributes.Add("onBlur", "add_paidday(" + str + ");");
            ((TextBox)grid_input.Rows[i].FindControl("txt_dutyday")).Attributes.Add("onBlur", "add_paidday(" + str + ");");
            ((TextBox)grid_input.Rows[i].FindControl("txt_tourday")).Attributes.Add("onBlur", "add_paidday(" + str + ");");
            ((TextBox)grid_input.Rows[i].FindControl("txt_compday")).Attributes.Add("onBlur", "add_paidday(" + str + ");");
        }
    }

    protected void gridinput_indexchanging(object sender, GridViewPageEventArgs e)
    {
        grid_input.PageIndex = e.NewPageIndex;
        get_load_alldetails();
        exam();
    }

    public void days_calculation(Collection<PayRoll> ex)
    {
        int datenum;
        paylist = pay.calc_days(pay);
        y = Convert.ToString(year);
        m = Convert.ToString(month);
        datenum = Convert.ToInt32(DateTime.DaysInMonth(year, month));
        int holi_ct = holiday_count();
        pay.Month_Calc = Convert.ToChar(hdtxt_monthcalc.Value);

        count = 0;
        if (pay.Month_Calc == 'D')
        {
            lblcalcmethod.Text = "Calculated Days = " + Convert.ToString(pay.Calc_Days);
            //paid_name.Value = Convert.ToString(pres_name + leave_name + week_name + duty_name + tour_name + comp_name);
            //abs_name.Value = Convert.ToString(Convert.ToInt32(calc_name.Value) - Convert.ToInt32(paid_name.Value));
        }
        else if (pay.Month_Calc == 'M')
        {
            //year = Int32.Parse(ddl_Year.SelectedValue);
            //month = Int32.Parse(ddl_Month.SelectedValue);
            //y = Convert.ToString(year);
            //m = Convert.ToString(month);
            // datenum = Convert.ToInt32(DateTime.DaysInMonth(year, month));
            dayCount = datenum;
            lblcalcmethod.Text = "Calculated Days = " + Convert.ToString(datenum);
            //paid_name.Value = Convert.ToString(pres_name + leave_name + week_name + duty_name + tour_name + comp_name);
            //abs_name.Value = Convert.ToString(Convert.ToInt32(calc_name.Value) - Convert.ToInt32(paid_name.Value));
            for (int dcount = 1; dcount <= datenum; dcount++)
            {
                d = Convert.ToString(dcount);
                dt = DateTime.Parse(y + "/" + m + "/" + d);
                dow = Convert.ToString(dt.DayOfWeek);
                if (dow == "Sunday")
                {
                    count = count + 1;
                }
            }
            lblcalcmethod.Text += " , Week Off Days = " + Convert.ToString(count);
            for (int grd = 0; grd < grid_input.Rows.Count; grd++)
            {
                if (((LinkButton)grid_input.Rows[grd].FindControl("img_save")).BackColor != System.Drawing.Color.LightSeaGreen)
                {
                    TextBox calc_name = ((TextBox)grid_input.Rows[grd].FindControl("grd_txt_calcdays"));
                    TextBox paid_name = ((TextBox)grid_input.Rows[grd].FindControl("grd_txt_paiddays"));
                    TextBox prs_name = ((TextBox)grid_input.Rows[grd].FindControl("grd_txt_presdays"));
                    TextBox abs_name = ((TextBox)grid_input.Rows[grd].FindControl("grd_txt_absdays"));
                    TextBox holiday_val = ((TextBox)grid_input.Rows[grd].FindControl("text_holiday"));

                    holiday_val.Text = Convert.ToString(holi_ct);
                    int pres_name = Convert.ToInt32(((TextBox)grid_input.Rows[grd].FindControl("grd_txt_presdays")).Text);
                    int leave_name = Convert.ToInt32(((TextBox)grid_input.Rows[grd].FindControl("txt_leavedays")).Text);
                    int duty_name = Convert.ToInt32(((TextBox)grid_input.Rows[grd].FindControl("txt_dutyday")).Text);
                    int tour_name = Convert.ToInt32(((TextBox)grid_input.Rows[grd].FindControl("txt_tourday")).Text);
                    int comp_name = Convert.ToInt32(((TextBox)grid_input.Rows[grd].FindControl("txt_compday")).Text);

                    calc_name.Text = Convert.ToString(datenum);
                    paid_name.Text = Convert.ToString(datenum); //Convert.ToString(pres_name + leave_name + duty_name + tour_name + comp_name);
                    prs_name.Text = Convert.ToString(datenum);
                }
            }
        }
        else if (pay.Month_Calc == 'W')
        {
            //year = Int32.Parse(ddl_Year.SelectedValue);
            //month = Int32.Parse(ddl_Month.SelectedValue);
            //datenum = Convert.ToInt32(DateTime.DaysInMonth(year, month));
            for (int dcount = 1; dcount <= datenum; dcount++)
            {
                d = Convert.ToString(dcount);
                dt = DateTime.Parse(y + "/" + m + "/" + d);
                dow = Convert.ToString(dt.DayOfWeek);
                if (pay.Week_Holiday1 != pay.Week_Holiday2)
                {
                    if (dow == pay.Week_Holiday1)
                    {
                        count = count + 1;
                    }
                    if (dow == pay.Week_Holiday2)
                    {
                        count = count + 1;
                    }
                }
                else if (pay.Week_Holiday1 == pay.Week_Holiday2)
                {
                    if (pay.Week_Holiday1 == "None" && pay.Week_Holiday2 == "None")
                    {
                        count = 0;
                    }
                    else if (dow == pay.Week_Holiday1 && dow == pay.Week_Holiday2)
                    {
                        count = count + 1;
                    }
                }
            }
            dayCount = datenum - count;
            lblcalcmethod.Text = "Calculated Days = " + Convert.ToString(datenum - count);
            lblcalcmethod.Text += " , Week Off Days = " + Convert.ToString(count);


            for (int grd = 0; grd < grid_input.Rows.Count; grd++)
            {
                if (((LinkButton)grid_input.Rows[grd].FindControl("img_save")).BackColor != System.Drawing.Color.LightSeaGreen)
                {

                    TextBox calc_name = ((TextBox)grid_input.Rows[grd].FindControl("grd_txt_calcdays"));
                    TextBox paid_name = ((TextBox)grid_input.Rows[grd].FindControl("grd_txt_paiddays"));
                    TextBox prs_name = ((TextBox)grid_input.Rows[grd].FindControl("grd_txt_presdays"));
                    TextBox abs_name = ((TextBox)grid_input.Rows[grd].FindControl("grd_txt_absdays"));

                    TextBox holiday_val = ((TextBox)grid_input.Rows[grd].FindControl("text_holiday"));

                    holiday_val.Text = Convert.ToString(holi_ct);

                    int pres_name = Convert.ToInt32(((TextBox)grid_input.Rows[grd].FindControl("grd_txt_presdays")).Text);
                    int leave_name = Convert.ToInt32(((TextBox)grid_input.Rows[grd].FindControl("txt_leavedays")).Text);

                    int duty_name = Convert.ToInt32(((TextBox)grid_input.Rows[grd].FindControl("txt_dutyday")).Text);
                    int tour_name = Convert.ToInt32(((TextBox)grid_input.Rows[grd].FindControl("txt_tourday")).Text);
                    int comp_name = Convert.ToInt32(((TextBox)grid_input.Rows[grd].FindControl("txt_compday")).Text);

                    calc_name.Text = Convert.ToString(datenum - count);
                    paid_name.Text = Convert.ToString(datenum - count); //Convert.ToString(pres_name + leave_name + duty_name + tour_name + comp_name);
                    prs_name.Text = Convert.ToString(datenum - count);
                }
            }
        }
    }



    protected void ddl_category_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (s_login_role == "a")
        {
            employee.BranchId = (int)ViewState["Payinput_BranchID"];
            //pay.BranchId = (int)ViewState["Payinput_BranchID"];
        }
       
        ddl_Month.Enabled = false;
        ddl_category.Enabled = false;
        ddl_Year.Enabled = false;
        string categoryid;
        categoryid = ddl_category.SelectedItem.Value;
        if (categoryid == "0")
        {
            //txt_department.Visible = false;
           // ddl_department.Visible = false;
        }
        
        else if (categoryid == "1")
        {
            int s = 0;
            txt_department.Text = "Employee";
            txt_department.Visible = true;
            ddl_department.Visible = true;
            //ddl_Employee_load();
            emplist = employee.fn_getEmployeeList(employee);// employee.fn_getAllEmployees();
            ddl_department.DataSource = emplist;
            ddl_department.DataValueField = "EmployeeId";
            ddl_department.DataTextField = "LastName";
            ddl_department.DataBind();
            ddl_department.Items.Insert(0, "Select Employee");
        }

        else if (categoryid == "2")
        {
            
             txt_department.Text = "Department";
            txt_department.Visible = true;
            ddl_department.Visible = true;
            //ddl_Department_load();
            emplist =employee.fn_getDepartmentList1(employee.BranchId); //employee.fn_Department(employee.BranchId);
            ddl_department.DataSource = emplist;
            ddl_department.DataValueField = "DepartmentId";
            ddl_department.DataTextField = "DepartmentName";
            ddl_department.DataBind();
            ddl_department.Items.Insert(0, "Select Department");
        }

        else if (categoryid == "3")
        {
            txt_department.Text = "Designation";
            txt_department.Visible = true;
            ddl_department.Visible = true;
            emplist = employee.fn_Designation();
            ddl_department.DataSource = emplist;
            ddl_department.DataValueField = "DesignationId";
            ddl_department.DataTextField = "DesignationName";
            ddl_department.DataBind();
            ddl_department.Items.Insert(0, "Select Designation");
        }
        
        else if (categoryid == "4")
        {
            txt_department.Text = "Other";
            emplist = employee.fn_Category();
            ddl_department.DataSource = emplist;
            ddl_department.DataValueField = "CategoryId";
            ddl_department.DataTextField = "CategoryName";
            ddl_department.DataBind();
            ddl_department.Items.Insert(0, "Other");
        }
        
    }


    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (s_login_role == "a")
        {
            //employee.BranchId = (int)ViewState["Payinput_BranchID"];
            pay.BranchId = (int)ViewState["Payinput_BranchID"];
        }
        pay.DepartmentID = Convert.ToInt32(ddl_department.SelectedValue);
        //paylist = pay.fn_getemployeeid(pay);
        year = Int32.Parse(ddl_Year.SelectedValue);
        month = Int32.Parse(ddl_Month.SelectedValue);
        y = Convert.ToString(year);
        m = Convert.ToString(month);
        d = "01";
        pay.d_date = Convert.ToDateTime(m + "/" + d + "/" + y);

        if (ddl_category.SelectedValue == "0")
        {

        }
        else if (ddl_category.SelectedValue == "1")
        {
            pay.CategoryID = "pn_EmployeeID";
        }
        else if (ddl_category.SelectedValue == "2")
        {
            pay.CategoryID = "pn_DepartmentId";
        }
        else if (ddl_category.SelectedValue == "3")
        {
            pay.CategoryID = "pn_DesingnationId";
        }
        else if (ddl_category.SelectedValue == "4")
        {
            pay.CategoryID = "pn_CategoryId";
        }

        paylist = pay.fn_Employeedetails_deptid(pay);
        if (paylist.Count > 0)
        {
            grid_input.DataSource = paylist;
            grid_input.DataBind();
        }
        else
        {

            // Label5.Visible = true;
            grid_input.Visible = false;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('NO Records Found')", true);
            // ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('No Data.');};", true);

        }
        for (int i = 0; i < grid_input.Rows.Count; i++)
        {
            if (paylist[i].OT == '0')
            {
                ((TextBox)grid_input.Rows[i].FindControl("text_othours")).Enabled = false;
            }

            for (int j = 0; j < paylist.Count; j++)
            {
                if (Convert.ToInt32(((HtmlInputHidden)grid_input.Rows[i].FindControl("grdhd_txt_empid")).Value) == paylist[j].ex_EmployeeID)
                {
                    ((LinkButton)grid_input.Rows[i].FindControl("img_save")).BackColor = System.Drawing.Color.LightSeaGreen;
                }
            }
        }
        days_calculation(paylist);
        //days_check();
        exam();
    }




    public void ddl_Branch_load()
    {
        int ddl_i;
        //branch dropdown
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
        if (ddl_Branch.SelectedValue != "0")
        {
            ViewState["Payinput_BranchID"] = Convert.ToInt32(ddl_Branch.SelectedValue);
            tbl_details.Visible = true;
        }
        else
        {
            tbl_details.Visible = false;
        }
    }

    protected void btn_refresh_Click1(object sender, EventArgs e)
    {
        get_load_alldetails();
        exam();
        ddl_category.SelectedValue = "0";
        //txt_department.Visible = false;
        //ddl_department.Visible = false;
        row_category.Visible = true;
        div_grd.Visible = true;
    }
    protected void btn_reset1(object sender,EventArgs e)
    {
        ddl_Month.Enabled = true;
        ddl_Year.Enabled = true;
        ddl_category.Enabled = true;
        ddl_department.Enabled = true;
        ddl_Month.ClearSelection();
        ddl_Year.ClearSelection();
        ddl_category.ClearSelection();
        ddl_department.ClearSelection();
        grid_input.Visible = false;
    }


    protected void btn_saveall_Click1(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in grid_input.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    if (s_login_role == "a")
                    {
                        pay.BranchId = (int)ViewState["Payinput_BranchID"];
                    }

                    double basic = 0, ern_basic = 0, ot_calc = 0, ot_amt = 0, ovt = 0, otround = 0;
                    TimeSpan tot_ot = new TimeSpan(0, 0, 0);
                    pay.EmployeeId = Convert.ToInt32(((HtmlInputHidden)row.FindControl("grdhd_txt_empid")).Value);
                    pay.EmployeeCode = Convert.ToString(((Label)row.FindControl("grd_txt_employeecode")).Text);
                    year = Int32.Parse(ddl_Year.SelectedValue);
                    month = Int32.Parse(ddl_Month.SelectedValue);
                    days = Convert.ToInt32(DateTime.DaysInMonth(year, month));
                    y = Convert.ToString(year);
                    m = Convert.ToString(month);
                    d = Convert.ToString(days);
                    d1 = "01";
                    pay.d_date = Convert.ToDateTime(y + "/" + m + "/" + d1);
                    pay.d_FromDate = Convert.ToDateTime(y + "/" + m + "/" + d1);
                    pay.d_ToDate = Convert.ToDateTime(y + "/" + m + "/" + d);
                    pay.strDate = y + "/" + m + "/" + d;
                    pay.Calc_Days = dayCount;
                    pay.Paid_Days = Convert.ToDouble(((TextBox)row.FindControl("grd_txt_paiddays")).Text);
                    pay.Present_Days = Convert.ToDouble(((TextBox)row.FindControl("grd_txt_presdays")).Text);
                    pay.Absent_Days = Convert.ToDouble(((TextBox)row.FindControl("grd_txt_absdays")).Text);
                    pay.TotLeave_Days = Convert.ToDouble(((TextBox)row.FindControl("txt_leavedays")).Text);
                    pay.WeekOffDays = count;
                    pay.Holidays = Convert.ToDouble(((TextBox)row.FindControl("text_holiday")).Text);
                    pay.OnDuty_days = Convert.ToDouble(((TextBox)row.FindControl("txt_dutyday")).Text);
                    pay.Compoff_Days = Convert.ToDouble(((TextBox)row.FindControl("txt_compday")).Text);
                    pay.Tour_Days = Convert.ToDouble(((TextBox)row.FindControl("txt_tourday")).Text);
                    //pay.Att_Bonus = Convert.ToString(((TextBox)row.FindControl("text_attbonus")).Text);//'m';
                    pay.Att_Bonus = "0";
                    pay.Att_BonusAmount = 0;//Convert.ToDouble(((HtmlInputText)row.FindControl("text_attbonusamount")).Value);
                    pay.temp_str = Convert.ToString(((TextBox)row.FindControl("text_othours")).Text);
                    pay.Earn_Arrears = 0;//Convert.ToDouble(((HtmlInputText)row.FindControl("txt_earnarrear")).Value);
                    pay.Ded_Arrears = 0;//Convert.ToDouble(((HtmlInputText)row.FindControl("txt_dedarrear")).Value);
                    if (pay.temp_str == "0")
                    {
                        pay.temp_str = "00:00";
                    }
                    tot_ot = TimeSpan.Parse(pay.temp_str);
                    string str_ot = "1900-01-01 " + pay.temp_str + ":00.000";
                    string[] ot_spl = pay.temp_str.Split(':');
                    if (pay.temp_str != "0")
                    {
                        if (ot_spl[1] == "15")
                        {
                            otround = 0.25;
                        }
                        else if (ot_spl[1] == "30")
                        {
                            otround = 0.5;
                        }
                        else if (ot_spl[1] == "45")
                        {
                            otround = 0.75;
                        }
                        else
                        {
                            otround = 0;
                        }
                    }
                    ovt = Convert.ToDouble(ot_spl[0]) + otround;
                    con.Open();
                    com = new SqlCommand("SELECT top 1 * FROM salary_structure where  pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' and pn_EmployeeID = '" + pay.EmployeeId + "' and effective_date <= '" + pay.strDate + "' order by effective_date desc ", con);
                    SqlDataReader rd_bas = com.ExecuteReader();
                    if (rd_bas.Read())
                    {
                        basic = Convert.ToDouble(rd_bas["salary"]);
                    }

                    com = new SqlCommand("update paym_employee set basic_salary = '" + basic + "' where pn_EmployeeID='" + pay.EmployeeId + "' and pn_BranchID = '" + employee.BranchId + "' and pn_CompanyID='" + employee.CompanyId + "'", con);
                    com.ExecuteNonQuery();

                    com = new SqlCommand("SELECT OT_calc FROM paym_employee where  pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' and pn_EmployeeID = '" + pay.EmployeeId + "' ", con);
                    SqlDataReader rd_otc = com.ExecuteReader();
                    if (rd_otc.Read())
                    {
                        ot_calc = Convert.ToDouble(rd_otc[0]);
                    }
                    ern_basic = (basic / pay.Calc_Days) * pay.Paid_Days;

                    if (ovt > 0)
                    {
                        OT_Settings = pay.fn_In_OT_Settings1(pay);

                        if (OT_Settings.Count > 0)
                        {
                            ot_amt = ((basic / OT_Settings[0].OT_Days) / OT_Settings[0].OT_HRS) * ovt * ot_calc;
                        }
                    }

                    pay.Act_Basic = basic;
                    pay.Earned_Basic = ern_basic;
                    pay.Date = Convert.ToDateTime(str_ot);
                    pay.OT_Days = ovt;
                    pay.OT_Amt = ot_amt;
                    con.Close();
                    _Value = pay.payinput_insert(pay);
                    if (_Value != "1")
                    {

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Insert Failed')", true);
                    }
                    if (ddl_category.SelectedValue == "0")
                    {
                        get_load_alldetails();
                    }
                    else
                    {
                        grid_load_id();
                    }
                    //((LinkButton)row.FindControl("img_save")).Visible = true;
                    exam();
                }
            }
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Enter OT Hours in 'HH:MM' Format.');};", true);
        }
    }

    protected void grd_txt_presdays_TextChanged(object sender, EventArgs e)
    {
        GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
        TextBox txtprs = (TextBox)currentRow.FindControl("grd_txt_presdays");
        TextBox txtpaid = (TextBox)currentRow.FindControl("grd_txt_paiddays");
        TextBox txtabs = (TextBox)currentRow.FindControl("grd_txt_absdays");
        TextBox txtleave = (TextBox)currentRow.FindControl("txt_leavedays");
        TextBox txtholiday = (TextBox)currentRow.FindControl("text_holiday");
        TextBox txtonduty = (TextBox)currentRow.FindControl("txt_dutyday");
        TextBox txtcomp = (TextBox)currentRow.FindControl("txt_compday");
        TextBox txttour = (TextBox)currentRow.FindControl("txt_tourday");
        TextBox txtcalc = (TextBox)currentRow.FindControl("grd_txt_calcdays");

        if (Convert.ToInt32(txtcalc.Text) > Convert.ToInt32(txtprs.Text) && txtprs.Text != "")
        {
            txtabs.Text = (Convert.ToInt32(txtcalc.Text) - Convert.ToInt32(txtprs.Text)).ToString();
            txtpaid.Text = (Convert.ToInt32(txtprs.Text) + Convert.ToInt32(txtleave.Text) + Convert.ToInt32(txtholiday.Text) + Convert.ToInt32(txtonduty.Text) + Convert.ToInt32(txtcomp.Text) + Convert.ToInt32(txttour.Text)).ToString();
        }
        else
        {
            txtprs.Text = "";
            ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Entered Value is invalid.');", true);
            txtprs.Focus();
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Entered Value is invalid.');", true);
        }
    }
    protected void grd_txt_absdays_TextChanged(object sender, EventArgs e)
    {
        GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
        TextBox txtprs = (TextBox)currentRow.FindControl("grd_txt_presdays");
        TextBox txtpaid = (TextBox)currentRow.FindControl("grd_txt_paiddays");
        TextBox txtabs = (TextBox)currentRow.FindControl("grd_txt_absdays");
        TextBox txtleave = (TextBox)currentRow.FindControl("txt_leavedays");
        TextBox txtholiday = (TextBox)currentRow.FindControl("text_holiday");
        TextBox txtonduty = (TextBox)currentRow.FindControl("txt_dutyday");
        TextBox txtcomp = (TextBox)currentRow.FindControl("txt_compday");
        TextBox txttour = (TextBox)currentRow.FindControl("txt_tourday");
        txtprs.Text = (Convert.ToInt32(txtprs.Text) - Convert.ToInt32(txtabs.Text)).ToString();
        txtpaid.Text = (Convert.ToInt32(txtprs.Text) + Convert.ToInt32(txtleave.Text) + Convert.ToInt32(txtholiday.Text) + Convert.ToInt32(txtonduty.Text) + Convert.ToInt32(txtcomp.Text) + Convert.ToInt32(txttour.Text)).ToString();
        txtleave.Focus();
    }
    protected void txt_leavedays_TextChanged(object sender, EventArgs e)
    {
       
        GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
        TextBox txtprs = (TextBox)currentRow.FindControl("grd_txt_presdays");
        TextBox txtpaid = (TextBox)currentRow.FindControl("grd_txt_paiddays");
        TextBox txtabs = (TextBox)currentRow.FindControl("grd_txt_absdays");
        TextBox txtleave = (TextBox)currentRow.FindControl("txt_leavedays");
        TextBox txtholiday = (TextBox)currentRow.FindControl("text_holiday");
        TextBox txtonduty = (TextBox)currentRow.FindControl("txt_dutyday");
        TextBox txtcomp = (TextBox)currentRow.FindControl("txt_compday");
        TextBox txttour = (TextBox)currentRow.FindControl("txt_tourday");
        int cc = dayCount - (Convert.ToInt32(txtprs.Text) + Convert.ToInt32(txtleave.Text) + Convert.ToInt32(txtholiday.Text) + Convert.ToInt32(txtonduty.Text) + Convert.ToInt32(txtcomp.Text) + Convert.ToInt32(txttour.Text));
        if(cc < 0)
        {
            txtleave.Text = "0";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Entered Value is invalid.');", true);
            return;
        }
        
        txtabs.Text = cc.ToString();
        txtpaid.Text = (Convert.ToInt32(txtprs.Text) + Convert.ToInt32(txtleave.Text) + Convert.ToInt32(txtholiday.Text) + Convert.ToInt32(txtonduty.Text) + Convert.ToInt32(txtcomp.Text) + Convert.ToInt32(txttour.Text)).ToString();
        txtholiday.Focus();
    }

    protected void text_holiday_TextChanged(object sender, EventArgs e)
    {
        GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
        TextBox txtprs = (TextBox)currentRow.FindControl("grd_txt_presdays");
        TextBox txtpaid = (TextBox)currentRow.FindControl("grd_txt_paiddays");
        TextBox txtabs = (TextBox)currentRow.FindControl("grd_txt_absdays");
        TextBox txtleave = (TextBox)currentRow.FindControl("txt_leavedays");
        TextBox txtholiday = (TextBox)currentRow.FindControl("text_holiday");
        TextBox txtonduty = (TextBox)currentRow.FindControl("txt_dutyday");
        TextBox txtcomp = (TextBox)currentRow.FindControl("txt_compday");
        TextBox txttour = (TextBox)currentRow.FindControl("txt_tourday");

        int cc = dayCount - (Convert.ToInt32(txtprs.Text) + Convert.ToInt32(txtleave.Text) + Convert.ToInt32(txtholiday.Text) + Convert.ToInt32(txtonduty.Text) + Convert.ToInt32(txtcomp.Text) + Convert.ToInt32(txttour.Text));
        if (cc < 0)
        {
            txtholiday.Text = "0";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Entered Value is invalid.');", true);
            return;
        }
        txtabs.Text = (dayCount - (Convert.ToInt32(txtprs.Text) + Convert.ToInt32(txtleave.Text) + Convert.ToInt32(txtholiday.Text) + Convert.ToInt32(txtonduty.Text) + Convert.ToInt32(txtcomp.Text) + Convert.ToInt32(txttour.Text))).ToString();
        txtpaid.Text = (Convert.ToInt32(txtprs.Text) + Convert.ToInt32(txtleave.Text) + Convert.ToInt32(txtholiday.Text) + Convert.ToInt32(txtonduty.Text) + Convert.ToInt32(txtcomp.Text) + Convert.ToInt32(txttour.Text)).ToString();
        txtonduty.Focus();
    }
    protected void txt_dutyday_TextChanged(object sender, EventArgs e)
    {
        GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
        TextBox txtprs = (TextBox)currentRow.FindControl("grd_txt_presdays");
        TextBox txtpaid = (TextBox)currentRow.FindControl("grd_txt_paiddays");
        TextBox txtabs = (TextBox)currentRow.FindControl("grd_txt_absdays");
        TextBox txtleave = (TextBox)currentRow.FindControl("txt_leavedays");
        TextBox txtholiday = (TextBox)currentRow.FindControl("text_holiday");
        TextBox txtonduty = (TextBox)currentRow.FindControl("txt_dutyday");
        TextBox txtcomp = (TextBox)currentRow.FindControl("txt_compday");
        TextBox txttour = (TextBox)currentRow.FindControl("txt_tourday");
        int cc = dayCount - (Convert.ToInt32(txtprs.Text) + Convert.ToInt32(txtleave.Text) + Convert.ToInt32(txtholiday.Text) + Convert.ToInt32(txtonduty.Text) + Convert.ToInt32(txtcomp.Text) + Convert.ToInt32(txttour.Text));
        if (cc < 0)
        {
            txtonduty.Text = "0";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Entered Value is invalid.');", true);
            return;
        }
        txtabs.Text = (dayCount - (Convert.ToInt32(txtprs.Text) + Convert.ToInt32(txtleave.Text) + Convert.ToInt32(txtholiday.Text) + Convert.ToInt32(txtonduty.Text) + Convert.ToInt32(txtcomp.Text) + Convert.ToInt32(txttour.Text))).ToString();
        txtpaid.Text = (Convert.ToInt32(txtprs.Text) + Convert.ToInt32(txtleave.Text) + Convert.ToInt32(txtholiday.Text) + Convert.ToInt32(txtonduty.Text) + Convert.ToInt32(txtcomp.Text) + Convert.ToInt32(txttour.Text)).ToString();
        txtcomp.Focus();
    }
    protected void txt_tourday_TextChanged(object sender, EventArgs e)
    {
        GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
        TextBox txtprs = (TextBox)currentRow.FindControl("grd_txt_presdays");
        TextBox txtpaid = (TextBox)currentRow.FindControl("grd_txt_paiddays");
        TextBox txtabs = (TextBox)currentRow.FindControl("grd_txt_absdays");
        TextBox txtleave = (TextBox)currentRow.FindControl("txt_leavedays");
        TextBox txtholiday = (TextBox)currentRow.FindControl("text_holiday");
        TextBox txtonduty = (TextBox)currentRow.FindControl("txt_dutyday");
        TextBox txtcomp = (TextBox)currentRow.FindControl("txt_compday");
        TextBox txttour = (TextBox)currentRow.FindControl("txt_tourday");
        TextBox txtattbonus = (TextBox)currentRow.FindControl("text_attbonus");
        int cc = dayCount - (Convert.ToInt32(txtprs.Text) + Convert.ToInt32(txtleave.Text) + Convert.ToInt32(txtholiday.Text) + Convert.ToInt32(txtonduty.Text) + Convert.ToInt32(txtcomp.Text) + Convert.ToInt32(txttour.Text));
        if (cc < 0)
        {
            txttour.Text = "0";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Entered Value is invalid.');", true);
            return;
        }
        txtabs.Text = (dayCount - (Convert.ToInt32(txtprs.Text) + Convert.ToInt32(txtleave.Text) + Convert.ToInt32(txtholiday.Text) + Convert.ToInt32(txtonduty.Text) + Convert.ToInt32(txtcomp.Text) + Convert.ToInt32(txttour.Text))).ToString();
        txtpaid.Text = (Convert.ToInt32(txtprs.Text) + Convert.ToInt32(txtleave.Text) + Convert.ToInt32(txtholiday.Text) + Convert.ToInt32(txtonduty.Text) + Convert.ToInt32(txtcomp.Text) + Convert.ToInt32(txttour.Text)).ToString();
        txtattbonus.Focus();
    }
    protected void txt_compday_TextChanged(object sender, EventArgs e)
    {
        GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
        TextBox txtprs = (TextBox)currentRow.FindControl("grd_txt_presdays");
        TextBox txtpaid = (TextBox)currentRow.FindControl("grd_txt_paiddays");
        TextBox txtabs = (TextBox)currentRow.FindControl("grd_txt_absdays");
        TextBox txtleave = (TextBox)currentRow.FindControl("txt_leavedays");
        TextBox txtholiday = (TextBox)currentRow.FindControl("text_holiday");
        TextBox txtonduty = (TextBox)currentRow.FindControl("txt_dutyday");
        TextBox txtcomp = (TextBox)currentRow.FindControl("txt_compday");
        TextBox txttour = (TextBox)currentRow.FindControl("txt_tourday");
        int cc = dayCount - (Convert.ToInt32(txtprs.Text) + Convert.ToInt32(txtleave.Text) + Convert.ToInt32(txtholiday.Text) + Convert.ToInt32(txtonduty.Text) + Convert.ToInt32(txtcomp.Text) + Convert.ToInt32(txttour.Text));
        if (cc < 0)
        {
            txtcomp.Text = "0";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Entered Value is invalid.');", true);
            return;
        }
        txtabs.Text = (dayCount - (Convert.ToInt32(txtprs.Text) + Convert.ToInt32(txtleave.Text) + Convert.ToInt32(txtholiday.Text) + Convert.ToInt32(txtonduty.Text) + Convert.ToInt32(txtcomp.Text) + Convert.ToInt32(txttour.Text))).ToString();
        txtpaid.Text = (Convert.ToInt32(txtprs.Text) + Convert.ToInt32(txtleave.Text) + Convert.ToInt32(txtholiday.Text) + Convert.ToInt32(txtonduty.Text) + Convert.ToInt32(txtcomp.Text) + Convert.ToInt32(txttour.Text)).ToString();
        txtcomp.Focus();
    }


    protected void ddl_department_PreRender(object sender, EventArgs e)
    {

    }
}
