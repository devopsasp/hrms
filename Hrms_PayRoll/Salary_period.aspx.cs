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
using ePayHrms.Leave;
using System.Data.SqlClient;

public partial class Bank_Loan_Default : System.Web.UI.Page
{
    //ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand com = new SqlCommand();
    
    Company company = new Company();
    Employee employee = new Employee();
    Leave l = new Leave();

    PayRoll Esi = new PayRoll();
    Collection<PayRoll> ESIlist;
    Collection<Company> CompanyList, ddlBranchsList;
    string month_type = "";

    DataSet ds_userrights;

    string _Value, str_date = "", month = "", year = "", s_form;
    int cur_yr, yr_it, ddl_ex, grd;

    string s_login_role;
    string man_days = "";    

    protected void Page_Load(object sender, EventArgs e)
    {
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value); 
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;

        

        // code to hide the from date and to date for manualdays
                              
        if (!IsPostBack)
        {
            ddl_year_load(ddl_select_year);
            ddl_select_month.SelectedValue = DateTime.Now.Month.ToString();
            ddl_select_year.SelectedValue = DateTime.Now.Year.ToString();
            CompanyList = company.fn_getCompany();
            if (CompanyList.Count > 0)
            {
                switch (s_login_role)
                {
                    case "a":                        
                        ddl_branch_load();
                        break;

                    case "h":
                        ddl_Branch.Visible = false;
                        
                        //load();
                        break;

                    case "u":
                        s_form = "43";

                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            ddl_Branch.Visible = false;
                            //load();
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
    
    public void fun_mon_type()
    {
        if (s_login_role == "a")
        {
            employee.BranchId = Convert.ToInt32(ddl_Branch.SelectedItem.Value);
        }
        //con.Open();
        SqlCommand cmd_att = new SqlCommand("select month_type from attendance_ceiling where pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "' ", con);
        SqlDataReader rd_att = cmd_att.ExecuteReader();
        if (rd_att.Read())
        {
            month_type = rd_att[0].ToString();
        }
        //con.Close();
    }

    public void ddl_branch_load()
    {
        con.Open();
        SqlDataAdapter ad = new SqlDataAdapter("select * from paym_branch ", con);
        DataSet ds = new DataSet();
        ad.Fill(ds, "paym_branch");
        ddl_Branch.DataSource = ds;
        ddl_Branch.DataTextField = "branchname";
        ddl_Branch.DataValueField = "pn_branchid";
        ddl_Branch.DataBind();

        ddl_Branch.Items.Insert(0, "Select Branch");
        con.Close();
    }

    public void process()
    {

        SqlCommand cmd_at = new SqlCommand();
        if (s_login_role == "h")
        {
            cmd_at = new SqlCommand("select month_type from attendance_ceiling where pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "'", con);
        }
        if (s_login_role == "a")
        {
            cmd_at = new SqlCommand("select month_type from attendance_ceiling where pn_branchid='" + ddl_Branch.SelectedItem.Value + "' and pn_companyid='" + employee.CompanyId + "'", con);
        }
        SqlDataReader rd_at = cmd_at.ExecuteReader();

        if (rd_at.Read())
        {
            month_type = rd_at[0].ToString();
        }
        rd_at.Close();


        if (month_type == "Manual Days")
        {
            SqlCommand cmd_man = new SqlCommand();
            if (s_login_role == "a")
            {
                cmd_man = new SqlCommand("select manual_days from attendance_ceiling where month_type='" + month_type + "' and pn_branchid='" + ddl_Branch.SelectedItem.Value + "' and pn_companyid='" + employee.CompanyId + "' ", con);
            }
            if (s_login_role == "h")
            {
                cmd_man = new SqlCommand("select manual_days from attendance_ceiling where month_type='" + month_type + "' and  pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "' ", con);
            }
            SqlDataReader rd_man = cmd_man.ExecuteReader();
            if (rd_man.Read())
            {
                man_days = rd_man[0].ToString();
            }
            Txt_wdays.Text = man_days;
        }

    }

    public void ddl_year_load(DropDownList ddl)
    {
        try
        {
            cur_yr = DateTime.Now.Year;

            //cur_yr = cur_yr + 5;

            for (yr_it = cur_yr - 4; yr_it <= cur_yr; yr_it++)
            {
                ddl.Items.Add(Convert.ToString(yr_it));
            }

        }

        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }
    

                
    public void clear()
    {
        Txt_spcode.Text = "";
        Txt_fdate.Text = "";
        Txt_tdate.Text = "";
        Txt_wdays.Text = "";
        txt_paydate.Text = "";
        Chk_ot.SelectedIndex = 0;
        ddl_select_month.SelectedValue = DateTime.Now.ToString("MMMMMMMMMM");
        ddl_select_year.SelectedValue = DateTime.Now.Year.ToString();
        Img_Save.Text = "Save";
    }


    protected void Txt_wdays_TextChanged(object sender, EventArgs e)
    {
           
    }
    
    protected void Txt_tdate_TextChanged(object sender, EventArgs e)
    {
        CalcDays();
    }

    public void CalcDays()
    {
        con.Open();
        fun_mon_type();
        if (month_type == "Month Days" || month_type == "Week-off ex")
        {
            string check = "IF EXISTS(Select * from sysobjects where name='temp_salary') " + "DROP TABLE temp_salary";
            SqlCommand chk_cmd = new SqlCommand(check, con);
            chk_cmd.ExecuteNonQuery();

            SqlCommand cre_cmd = new SqlCommand("create table temp_salary(pn_companyid int,pn_branchid int,day_week char(10),date_month datetime,holiday varchar(15),weekoff char(10))", con);
            cre_cmd.ExecuteNonQuery();

            DateTime tdate, fdate;
            SqlDataReader rd1, rd2, rd3, dr;
            string curday = "";

            fdate = Convert.ToDateTime(employee.Convert_ToSqlDatestring(Txt_fdate.Text));
            tdate = Convert.ToDateTime(employee.Convert_ToSqlDatestring(Txt_tdate.Text));

            TimeSpan diff = tdate - fdate;
            int dat_diff = diff.Days;
            if (dat_diff > 0)
            {
                for (int d = 0; d <= dat_diff; d++)
                {
                    DateTime day = fdate.AddDays(d);
                    curday = day.DayOfWeek.ToString();
                    string dt = fdate.AddDays(d).ToString("dd/MM/yyyy");
                    string[] date = dt.Split('/', '-');
                    string final_date = date[2] + "/" + date[1] + "/" + date[0];
                    SqlCommand cmd = new SqlCommand();

                    if (s_login_role == "a")
                    {
                        cmd = new SqlCommand("insert into temp_salary(pn_companyid,pn_branchid,day_week,date_month) values('" + employee.CompanyId + "','" + ddl_Branch.SelectedItem.Value + "','" + curday + "','" + final_date + "')", con);
                    }

                    if (s_login_role == "h")
                    {
                        cmd = new SqlCommand("insert into temp_salary(pn_companyid,pn_branchid,day_week,date_month) values('" + employee.CompanyId + "','" + employee.BranchId + "','" + curday + "','" + final_date + "')", con);
                    }

                    cmd.ExecuteNonQuery();



                    if (month_type == "Week-off ex")
                    {

                        SqlCommand cmd2 = new SqlCommand();

                        if (s_login_role == "a")
                        {
                            cmd2 = new SqlCommand("select week_off1 from attendance_ceiling where week_off1 = '" + curday + "' and pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + ddl_Branch.SelectedItem.Value + "' ", con);
                        }

                        if (s_login_role == "h")
                        {
                            cmd2 = new SqlCommand("select week_off1 from attendance_ceiling where week_off1 = '" + curday + "' and pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + employee.BranchId + "' ", con);
                        }

                        rd2 = cmd2.ExecuteReader();

                        if (rd2.Read())
                        {
                            SqlCommand u_cmd1 = new SqlCommand();
                            if (s_login_role == "a")
                            {
                                u_cmd1 = new SqlCommand("update temp_salary set weekoff ='w' where day_week = '" + curday + "' and pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + ddl_Branch.SelectedItem.Value + "' ", con);
                            }

                            if (s_login_role == "h")
                            {
                                u_cmd1 = new SqlCommand("update temp_salary set weekoff ='w' where day_week = '" + curday + "' and pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + employee.BranchId + "' ", con);
                            }

                            u_cmd1.ExecuteNonQuery();
                        }

                        rd2.Close();

                        SqlCommand cmd3 = new SqlCommand();
                        if (s_login_role == "a")
                        {
                            cmd3 = new SqlCommand("select week_off2 from attendance_ceiling where week_off2 = '" + curday + "' and pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + ddl_Branch.SelectedItem.Value + "' ", con);
                        }

                        if (s_login_role == "h")
                        {
                            cmd3 = new SqlCommand("select week_off2 from attendance_ceiling where week_off2 = '" + curday + "' and pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + employee.BranchId + "' ", con);
                        }

                        rd3 = cmd3.ExecuteReader();

                        if (rd3.Read())
                        {
                            SqlCommand u_cmd2 = new SqlCommand();
                            if (s_login_role == "a")
                            {
                                u_cmd2 = new SqlCommand("update temp_salary set weekoff ='w' where day_week = '" + curday + "' and pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + ddl_Branch.SelectedItem.Value + "'", con);
                            }

                            if (s_login_role == "h")
                            {
                                u_cmd2 = new SqlCommand("update temp_salary set weekoff ='w' where day_week = '" + curday + "' and pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + employee.BranchId + "' ", con);
                            }

                            u_cmd2.ExecuteNonQuery();
                        }
                        rd3.Close();

                        SqlCommand dis_cmd = new SqlCommand();

                        if (s_login_role == "a")
                        {
                            dis_cmd = new SqlCommand("select count(*) as total from temp_salary where weekoff is null and pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + ddl_Branch.SelectedItem.Value + "' ", con);
                        }

                        if (s_login_role == "h")
                        {
                            dis_cmd = new SqlCommand("select count(*) as total from temp_salary where weekoff is null and pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + employee.BranchId + "' ", con);
                        }

                        int work = (int)dis_cmd.ExecuteScalar();

                        Txt_wdays.Text = Convert.ToString(work);

                    }


                    else if (month_type == "Month Days")
                    {
                        SqlCommand dis_cmd = new SqlCommand();

                        if (s_login_role == "a")
                        {
                            dis_cmd = new SqlCommand("select count(*) as total from temp_salary where weekoff is null  and pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + ddl_Branch.SelectedItem.Value + "' ", con);
                        }

                        if (s_login_role == "h")
                        {
                            dis_cmd = new SqlCommand("select count(*) as total from temp_salary where weekoff is null  and pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + employee.BranchId + "' ", con);
                        }

                        int work = (int)dis_cmd.ExecuteScalar();

                        Txt_wdays.Text = Convert.ToString(work);
                    }


                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Salary To Date should be greater than Salary From Date');", true);
            }
        }
        //else if (month_type == "Manual Days")
        //{
        //    Txt_wdays.Text = man_days;
        //}

        con.Close();
    }

    protected void ddl_select_month_SelectedIndexChanged(object sender, EventArgs e)
    {
        con.Open();
        Txt_wdays.Text = "";
        SqlDataReader ret;
        SqlCommand cmd = new SqlCommand();
        if (s_login_role == "a")
        {
            cmd = new SqlCommand("select * from salary_period where p_year = '" + ddl_select_year.SelectedItem.Text + "' and p_month = '" + ddl_select_month.SelectedItem.Text + "' and selection='" + RadioButtonList1.SelectedItem.Text + "' and pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + ddl_Branch.SelectedItem.Value + "' ", con);
        }

        if (s_login_role == "h")
        {
            cmd = new SqlCommand("select * from salary_period where p_year = '" + ddl_select_year.SelectedItem.Text + "' and p_month = '" + ddl_select_month.SelectedItem.Text + "' and selection='" + RadioButtonList1.SelectedItem.Text + "' and pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + employee.BranchId + "' ", con);
        }
        ret = cmd.ExecuteReader();
        
        if (ret.Read())
        {
            string p_code = ret[2].ToString();
            string sel_code = ret[3].ToString();
            string year = ret[4].ToString();
            string month = ret[5].ToString();

            string tot_dat = ret[8].ToString();
            DateTime pay_dat = Convert.ToDateTime(ret[9]);
            string ot = ret[10].ToString();

            Txt_spcode.Text = p_code;

            Txt_wdays.Text = tot_dat;
            txt_paydate.Text = pay_dat.ToShortDateString();
            Txt_spcode.ReadOnly = true;
            Chk_ot.SelectedValue = ot;
           

            if (month_type != "Manual Days")
            {
                DateTime from_dat = Convert.ToDateTime(ret[6]);
                DateTime to_dat = Convert.ToDateTime(ret[7]);
                Txt_fdate.Text = from_dat.ToShortDateString();
                Txt_tdate.Text = to_dat.ToShortDateString();
            }

            if (ret["selection"].ToString() == "Month")
            {
                RadioButtonList1.SelectedIndex = 0;
            }

             if (ret["selection"].ToString() == "Week")
            {
                RadioButtonList1.SelectedIndex = 1;
            }

             if (ret["selection"].ToString() == "Day")
            {
                RadioButtonList1.SelectedIndex = 2;
            }

             if (month_type == "Manual Days" && ret["selection"].ToString() == "Week")
             {
                 RadioButtonList1.SelectedIndex = 1;

                 DateTime from_dat = Convert.ToDateTime(ret[6]); //from date             
                 DateTime to_dat = Convert.ToDateTime(ret[7]); //to date
                 Txt_fdate.Text = from_dat.ToShortDateString();
                 Txt_tdate.Text = to_dat.ToShortDateString();
             }

                 //
             fun_mon_type();

             if (month_type == "Manual Days" && RadioButtonList1.SelectedItem.Text != "Week")
             {
                 //int last_date =Convert.ToInt32(DateTime.DaysInMonth(Convert.ToInt32(ddl_select_year.SelectedItem.Value), Convert.ToInt32(ddl_select_month.SelectedItem.Value)));
                 DateTime initialdate = Convert.ToDateTime(ddl_select_month.SelectedItem.Value + "/01/" + ddl_select_year.SelectedItem.Text);

                 DateTime newdate = initialdate.AddMonths(1);
                 int day = (newdate.AddDays(-1)).Day;

                 Txt_fdate.Text = "01/" + ddl_select_month.SelectedItem.Value + "/" + ddl_select_year.SelectedItem.Value;
                 Txt_tdate.Text = day + "/" + ddl_select_month.SelectedItem.Value + "/" + ddl_select_year.SelectedItem.Value;
                 Txt_wdays.Text = man_days;
                 Txt_wdays.Enabled = false;
                 Txt_fdate.Enabled = false;
                 Txt_tdate.Enabled = false;
             }

            //
             process();
             Img_Save.Text = "Modify";
            //Btn_save.Text = "Modify";
            
        }

        else
        {
            con.Close();
            var myDate = Convert.ToDateTime("01/" + ddl_select_month.SelectedItem.Value + "/" + ddl_select_year.SelectedItem.Value);
            var startOfMonth = new DateTime(myDate.Year, myDate.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
            //DateTime initialdate = Convert.ToDateTime(ddl_select_month.SelectedItem.Value + "/01/" + ddl_select_year.SelectedItem.Text);
            //this.CompareValidator1.ValueToCompare = endOfMonth.ToShortDateString();
            //DateTime newdate = initialdate.AddMonths(1);
            //int day = (newdate.AddDays(-1)).Day;
            //Txt_fdate.Text = "01/" + ddl_select_month.SelectedItem.Value + "/" + ddl_select_year.SelectedItem.Value;
            //Txt_tdate.Text = day + "/" + ddl_select_month.SelectedItem.Value + "/" + ddl_select_year.SelectedItem.Value;
            Txt_fdate.Text = myDate.ToShortDateString();
            Txt_tdate.Text = endOfMonth.ToShortDateString();
            txt_paydate.Text = "";
            Txt_spcode.Text= "";
            Txt_spcode.ReadOnly = false;
            Txt_spcode.Text = ddl_select_month.SelectedItem.Text + "-" + ddl_select_year.SelectedItem.Text;
            txt_paydate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            CalcDays();
            //fun_mon_type();
            //if (month_type != "Manual Days")
            //{
            //    Txt_fdate.Text = "";
            //    Txt_tdate.Text = "";
            //}

            Img_Save.Text = "Save";
            //Btn_save.Text = "Save";

        }

        
              
       
    }

    //        Holiday Code
    //SqlCommand cmd1 = new SqlCommand();

    //if (s_login_role == "a")
    //{
    //    cmd1 = new SqlCommand("select pn_Holidayname from paym_holiday where pn_branchid = '" + ddl_Branch.SelectedItem.Value + "' and pn_companyid = '" + employee.CompanyId + "' and from_date <= '" + final_date + "' and to_date >= '" + final_date + "'", con);
    //}

    //if (s_login_role == "h")
    //{
    //    cmd1 = new SqlCommand("select pn_Holidayname from paym_holiday where pn_branchid = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "' and from_date <= '" + final_date + "' and to_date >= '" + final_date + "'", con);
    //}
    //rd1 = cmd1.ExecuteReader();

    //if (rd1.Read())
    //{
    //    SqlCommand u_cmd = new SqlCommand();
    //    if (s_login_role == "a")
    //    {
    //        u_cmd = new SqlCommand("update temp_salary set holiday ='H' where date_month = '" + final_date + "' and pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + ddl_Branch.SelectedItem.Value + "' ", con);
    //    }

    //    if (s_login_role == "h")
    //    {
    //        u_cmd = new SqlCommand("update temp_salary set holiday ='H' where date_month = '" + final_date + "' and pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + employee.BranchId + "' ", con);
    //    }

    //    u_cmd.ExecuteNonQuery();
    //}

    //rd1.Close();
    //// 

    protected void ddl_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        con.Open();      
        process();
        con.Close();

    }
    protected void Txt_fdate_TextChanged(object sender, EventArgs e)
    {

    }

    protected void Img_Save_Click(object sender, EventArgs e)
    {
        try
        {
            con.Open();
            string ot = Chk_ot.SelectedValue;
            string f_date = employee.Convert_ToSqlDatestring(Txt_fdate.Text);
            string t_date = employee.Convert_ToSqlDatestring(Txt_tdate.Text);
            string p_date = employee.Convert_ToSqlDatestring(txt_paydate.Text);

            if (Img_Save.Text == "Save")
            {
                SqlCommand cmd_at = new SqlCommand();
                if (s_login_role == "h")
                {
                    cmd_at = new SqlCommand("select month_type from attendance_ceiling where pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "'", con);
                }

                if (s_login_role == "a")
                {
                    cmd_at = new SqlCommand("select month_type from attendance_ceiling where pn_branchid='" + ddl_Branch.SelectedItem.Value + "' and pn_companyid='" + employee.CompanyId + "'", con);
                }
                
                SqlDataReader cmd_rd = cmd_at.ExecuteReader();
                if (cmd_rd.Read())
                {
                    month_type = cmd_rd[0].ToString();
                }


                if (month_type == "Month Days" || month_type == "Week-off ex")
                {

                    if (s_login_role == "a")
                    {
                        com = new SqlCommand("insert into salary_period(pn_CompanyID, pn_BranchID, period_code, selection, p_year, p_month, from_date, to_date, total_days, pay_date, ot_include) values ('" + employee.CompanyId + "','" + ddl_Branch.SelectedItem.Value + "','" + Txt_spcode.Text + "','" + RadioButtonList1.SelectedItem.Text + "','" + ddl_select_year.SelectedItem.Text + "','" + ddl_select_month.SelectedItem.Text + "','" + f_date + "','" + t_date + "','" + Txt_wdays.Text + "','" + p_date + "','" + ot + "')", con);
                    }

                    if (s_login_role == "h")
                    {
                        com = new SqlCommand("insert into salary_period(pn_CompanyID, pn_BranchID, period_code, selection, p_year, p_month, from_date, to_date, total_days, pay_date, ot_include) values ('" + employee.CompanyId + "','" + employee.BranchId + "','" + Txt_spcode.Text + "','" + RadioButtonList1.SelectedItem.Text + "','" + ddl_select_year.SelectedItem.Text + "','" + ddl_select_month.SelectedItem.Text + "','" + f_date + "','" + t_date + "','" + Txt_wdays.Text + "','" + p_date + "','" + ot + "')", con);
                    }

                    com.ExecuteNonQuery();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Saved Successfully');", true);
                }


                else if (month_type == "Manual Days")
                {
                    if (s_login_role == "a")
                    {
                        com = new SqlCommand("insert into salary_period(pn_CompanyID, pn_BranchID, period_code, selection, p_year, p_month, from_date, to_date, total_days, pay_date, ot_include) values ('" + employee.CompanyId + "','" + ddl_Branch.SelectedItem.Value + "','" + Txt_spcode.Text + "','" + RadioButtonList1.SelectedItem.Text + "','" + ddl_select_year.SelectedItem.Text + "','" + ddl_select_month.SelectedItem.Text + "','" + f_date + "','" + t_date + "','" + Txt_wdays.Text + "','" + p_date + "','" + ot + "')", con);
                    }

                    if (s_login_role == "h")
                    {
                        com = new SqlCommand("insert into salary_period(pn_CompanyID, pn_BranchID, period_code, selection, p_year, p_month,from_date, to_date, total_days, pay_date, ot_include) values ('" + employee.CompanyId + "','" + employee.BranchId + "','" + Txt_spcode.Text + "','" + RadioButtonList1.SelectedItem.Text + "','" + ddl_select_year.SelectedItem.Text + "','" + ddl_select_month.SelectedItem.Text + "','" + f_date + "','" + t_date + "','" + Txt_wdays.Text + "','" + p_date + "','" + ot + "')", con);
                    }
                    com.ExecuteNonQuery();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Saved Successfully');", true);
                }

            }


            if (Img_Save.Text == "Modify")
            {

                SqlCommand up_cmd = new SqlCommand();
                if (s_login_role == "a")
                {
                    up_cmd = new SqlCommand("update salary_period set selection= '" + RadioButtonList1.SelectedItem.Text + "',from_date  = '" + f_date + "',to_date = '" + t_date + "',pay_date = '" + p_date + "',total_days = '" + Txt_wdays.Text + "',ot_include = '" + ot + "' where period_code ='" + Txt_spcode.Text + "' and pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + ddl_Branch.SelectedItem.Value + "' ", con);
                }

                if (s_login_role == "h")
                {
                    up_cmd = new SqlCommand("update salary_period set selection= '" + RadioButtonList1.SelectedItem.Text + "',from_date  = '" + f_date + "',to_date = '" + t_date + "',pay_date = '" + p_date + "',total_days = '" + Txt_wdays.Text + "',ot_include = '" + ot + "' where period_code ='" + Txt_spcode.Text + "' and pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + employee.BranchId + "' ", con);
                }
                up_cmd.ExecuteNonQuery();

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Updated Successfully');", true);
            }

        }

        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }

        finally
        {
            clear();
            con.Close();
        }
    }
    protected void Btn_clear_Click(object sender, EventArgs e)
    {
        clear();
    }

    protected void Img_Delete_Click(object sender, EventArgs e)
    {
        try
        {
            if (Txt_spcode.Text != "")
            {
                con.Open();
                com = new SqlCommand("Delete from salary_period where period_code='" + Txt_spcode.Text + "' and pn_companyId = '" + employee.CompanyId + "' and pn_BranchID = '" + employee.BranchId + "'", con);
                com.ExecuteNonQuery();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Deleted Successfully');", true);
                con.Close();
                clear();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter Period Code');", true);
            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }
    protected void Img_Clear_Click(object sender,EventArgs e)
    {
        clear();
    }
}

   
