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
using System.Data.SqlClient;

public partial class Bank_Loan_Default : System.Web.UI.Page
{
    Company company = new Company();
    Employee employee = new Employee();
    PayRoll pay = new PayRoll();

    Collection<PayRoll> loanlist, loanlist1;
    Collection<Employee> EmployeeList, EmployeeList1;
    Collection<Company> CompanyList, ddlBranchsList;

    DataSet ds_userrights;
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connectionstring"]);

    string _Value, query = "", s_form;
    string s_login_role;
    int grd, ddl_ex;

    protected void Page_Load(object sender, EventArgs e)
    {
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        pay.CompanyId      = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);

        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        pay.BranchId      = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        

        s_login_role = Request.Cookies["Login_temp_Role"].Value;


        if (!IsPostBack)
        {
            //ddl_load(ddl_Loancode,ddl_empcode);
            ListBox1.Visible = false;
            CompanyList = company.fn_getCompany();
            if (CompanyList.Count > 0)
            {
                switch (s_login_role)
                {
                    case "a":
                        ddl_Branch.Visible = true;
                        ddl_Branch_load();
                        ddl_Loancode.Items.Insert(0, "Select");
                        break;

                    case "h":
                        ddl_Branch.Visible = false;
                        ddl_Department_load();
                        ddl_load(ddl_Loancode, ddl_empcode);
                        ddl_Loancode.Items.Insert(0, "Select");
                        break;

                    case "u":
                        s_form = "46";

                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);
                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            //load();
                            ddl_Branch.Visible = false;
                            ddl_load(ddl_Loancode, ddl_empcode);
                        }
                        else
                        {
                            Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator.";
                            //Response.Redirect("MasterHome.aspx");
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



    protected void edit(object sender, GridViewEditEventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                employee.BranchId = (int)ViewState["Loanentry_BranchID"];
                pay.BranchId = (int)ViewState["Loanentry_BranchID"];
            }
            pay.loanid     = Convert.ToInt32(((DropDownList)grid_loan.Rows[e.NewEditIndex].FindControl("grd_Loan")).SelectedItem.Value);
            pay.EmployeeId = Convert.ToInt32(((DropDownList)grid_loan.Rows[e.NewEditIndex].FindControl("grd_Employee")).SelectedItem.Value);

            pay.dateapplication = pay.Convert_ToSqlDate(((TextBox)grid_loan.Rows[e.NewEditIndex].FindControl("Txtdateapplication")).Text);//Convert.ToDateTime(((TextBox)grid_loan.Rows[e.NewEditIndex].FindControl("Txtdateapplication")).Text);
            pay.effectivedate   = pay.Convert_ToSqlDate(((TextBox)grid_loan.Rows[e.NewEditIndex].FindControl("Txteffdate")).Text); //Convert.ToDateTime(((TextBox)grid_loan.Rows[e.NewEditIndex].FindControl("Txteffdate")).Text);

            pay.Amount = Convert.ToInt32(((HtmlInputText)grid_loan.Rows[e.NewEditIndex].FindControl("Txtamount")).Value);
            pay.F_Amount = Convert.ToInt32(((HtmlInputText)grid_loan.Rows[e.NewEditIndex].FindControl("TxtInsAmount")).Value);
            pay.installmentcount = Convert.ToInt32(((HtmlInputText)grid_loan.Rows[e.NewEditIndex].FindControl("Txtcount")).Value);
            pay.balanceamount = Convert.ToDouble(Convert.ToInt32(((HtmlInputText)grid_loan.Rows[e.NewEditIndex].FindControl("Txtamount")).Value) - Convert.ToInt32(((HtmlInputText)grid_loan.Rows[e.NewEditIndex].FindControl("TxtInsAmount")).Value));
            pay.status = 'Y';
            _Value = pay.updateallot(pay);

            if (_Value != "1")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Updated successfully');", true);
                //grid_load();
                //grid_ddl_load();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
                //grid_load();
            }

            ((DropDownList)grid_loan.Rows[e.NewEditIndex].FindControl("grd_Loan")).Enabled = false;
            ((DropDownList)grid_loan.Rows[e.NewEditIndex].FindControl("grd_Employee")).Enabled = false;

            ((TextBox)grid_loan.Rows[e.NewEditIndex].FindControl("Txtdateapplication")).Enabled = false;
            ((TextBox)grid_loan.Rows[e.NewEditIndex].FindControl("Txteffdate")).Enabled = false;

            ((HtmlInputText)grid_loan.Rows[e.NewEditIndex].FindControl("Txtamount")).Disabled = true;
            ((HtmlInputText)grid_loan.Rows[e.NewEditIndex].FindControl("TxtInsAmount")).Disabled = true;
            ((HtmlInputText)grid_loan.Rows[e.NewEditIndex].FindControl("Txtcount")).Disabled = true;

            ((Image)grid_loan.Rows[e.NewEditIndex].FindControl("img_update")).Visible = true;
            ((Image)grid_loan.Rows[e.NewEditIndex].FindControl("img_save")).Visible = false;
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }

    }

    protected void update(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            ((DropDownList)grid_loan.Rows[e.RowIndex].FindControl("grd_Loan")).Enabled = false;
            ((DropDownList)grid_loan.Rows[e.RowIndex].FindControl("grd_Employee")).Enabled = false;

            ((TextBox)grid_loan.Rows[e.RowIndex].FindControl("Txtdateapplication")).Enabled = false;
            ((TextBox)grid_loan.Rows[e.RowIndex].FindControl("Txteffdate")).Enabled = false;

            ((HtmlInputText)grid_loan.Rows[e.RowIndex].FindControl("Txtamount")).Disabled = false;
            ((HtmlInputText)grid_loan.Rows[e.RowIndex].FindControl("TxtInsAmount")).Disabled = false;
            ((HtmlInputText)grid_loan.Rows[e.RowIndex].FindControl("Txtcount")).Disabled = false;
            ((Image)grid_loan.Rows[e.RowIndex].FindControl("img_save")).Visible = true;
            ((Image)grid_loan.Rows[e.RowIndex].FindControl("img_update")).Visible = false;
        }

        catch
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }

        //((ImageButton)grid_loan.Rows[e.RowIndex].FindControl("img_update")).Visible = false;
    }



    public void grid_load()
    {
        try
        {
            loanlist = pay.fn_loanentry(query);
            if (loanlist.Count > 0)
            {
                grid_loan.DataSource = loanlist;
                grid_loan.DataBind();
                grid_ddl_load();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Data');", true);
            }
        }

        catch
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

    public void ddl_load(DropDownList arg_loan, DropDownList arg_emp)
    {
        try
        {
            if (s_login_role == "a")
            {
                employee.BranchId = (int)ViewState["Loanentry_BranchID"];
                pay.BranchId      = (int)ViewState["Loanentry_BranchID"];
            }

            loanlist1 = pay.fn_loan(pay);


            if (loanlist1.Count > 0)
            {
                arg_loan.DataSource     = loanlist1;
                arg_loan.DataTextField  = "loanname";
                arg_loan.DataValueField = "loanid";
                arg_loan.DataBind();
            }

            EmployeeList1 = employee.fn_getEmployeeList(employee);

            if (EmployeeList1.Count > 0)
            {
                ddl_empcode.Enabled = true;

                for (int ddl_i = -1; ddl_i < EmployeeList1.Count; ddl_i++)
                {
                    if (ddl_i == -1)
                    {
                        ListItem list = new ListItem();
                        list.Text = "Select Employee";
                        list.Value = "0";
                        arg_emp.Items.Add(list);
                    }
                    else
                    {
                        ListItem list = new ListItem();
                        list.Value = EmployeeList1[ddl_i].EmployeeId.ToString();
                        list.Text = EmployeeList1[ddl_i].FullName.ToString();
                        arg_emp.Items.Add(list);
                    }
                }
            }
            else
            {
                arg_emp.Items.Clear();
                //ListItem list = new ListItem();
                //list.Text = "";
                //list.Value = "";
                //arg_emp.Items.Add(list);

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Employees Available');", true);
                ddl_empcode.Enabled = false;
            }
        }

        catch
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

    protected void btn_LoanDetails_Click(object sender, EventArgs e)
    {
        try
        {
            con.Open();
            string loanprocess, loancalculation;
            int mon = 0, bal_rem_mon = 0;
            if (s_login_role == "a")
            {
                employee.BranchId = (int)ViewState["Loanentry_BranchID"];
                pay.BranchId = (int)ViewState["Loanentry_BranchID"];
            }
            query = "select * from LoanEntry where loan_appid='" + Txt_loan_id.Text + "' and c_status='Y'";
            query = query + " and pn_CompanyID=" + pay.CompanyId + " and pn_BranchID=" + pay.BranchId + " and loan_status!='loan closed'";


            loanlist = pay.fn_loanentry(query);

            if (loanlist.Count > 0)
            {
                ddl_Loancode.SelectedItem.Text = Convert.ToString(loanlist[0].loanname);
                ddl_empcode.SelectedValue = Convert.ToString(loanlist[0].EmployeeId);
                Txt_empnam.Text = Convert.ToString(loanlist[0].Empname);
                Txt_loan_sdate.Text = Convert.ToString(loanlist[0].strdateapplication);
                Txt_loan_effdate.Text = Convert.ToString(loanlist[0].streffectivedate);
                Txt_loan_amt.Text = Convert.ToString(loanlist[0].Amount.ToString("#,0.00"));
                Txt_interest.Text = Convert.ToString(loanlist[0].loaninterest.ToString("#,0.00"));
                Txt_no_dedu.Text = Convert.ToString(loanlist[0].installmentcount);
                Txt_cur_bal.Text = Convert.ToString(loanlist[0].balanceamount.ToString("#,0.00"));
                Txt_empnam.Text = Convert.ToString(loanlist[0].EmployeeName);
                Txt_dedu_mon.Text = Convert.ToString(loanlist[0].F_Amount.ToString("#,0.00"));
                Txt_interest_amt.Text = Convert.ToString(loanlist[0].tot_interest_amt.ToString("#,0.00"));
                loanprocess = Convert.ToString(loanlist[0].loan_process);
                loancalculation = Convert.ToString(loanlist[0].loan_calc);
                if (loanprocess == "By Flat Rate")
                {
                    rd_loan_process.SelectedIndex = 0;
                    SqlCommand cmd_sel_mon = new SqlCommand("select max(installement_count) from payoutput_loan where loan_appid='" + loanlist[0].loanappid + "' ", con);
                    SqlDataReader rd_mon = cmd_sel_mon.ExecuteReader();

                    if (rd_mon.Read())
                    {
                        mon = Convert.ToInt32(rd_mon[0]);
                    }

                    int tot_count = Convert.ToInt32(loanlist[0].installmentcount);

                    bal_rem_mon = tot_count - mon;

                    Txt_rem_count.Text = Convert.ToString(bal_rem_mon);
                }

                else if (loanprocess == "By Diminishing Rate")
                {
                    rd_loan_process.SelectedIndex = 1;
                    SqlCommand cmd_sel_mon = new SqlCommand("select max(installement_count) from paym_loan_diminishing where loan_appid='" + loanlist[0].loanappid + "' ", con);
                    SqlDataReader rd_mon = cmd_sel_mon.ExecuteReader();

                    if (rd_mon.Read())
                    {
                        mon = Convert.ToInt32(rd_mon[0]);
                    }

                    int tot_count = Convert.ToInt32(loanlist[0].installmentcount);

                    bal_rem_mon = tot_count - mon;
                    Txt_rem_count.Text = Convert.ToString(bal_rem_mon);
                    rd_calc.Enabled = false;

                }

                if (loancalculation == "Monthly Based")
                {
                    rd_calc.SelectedIndex = 0;
                }
                else if (loancalculation == "Amount Based")
                {
                    rd_calc.SelectedIndex = 1;
                }
            }
        }

        catch
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

    protected void btn_EmpDetails_Click(object sender, EventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                employee.BranchId = (int)ViewState["Loanentry_BranchID"];
                pay.BranchId = (int)ViewState["Loanentry_BranchID"];
            }
            if (ddl_empcode.SelectedValue != "0" && ddl_empcode.SelectedValue != "")
            {
                //ddl_empcode.Enabled = true;
                query = "select * from LoanEntry where pn_EmployeeID=" + ddl_empcode.SelectedItem.Value + " and loan_appid='" + Txt_loan_id.Text + "'and c_status='Y'";
                query = query + " and pn_CompanyID=" + pay.CompanyId + " and pn_BranchID=" + pay.BranchId;
                grid_load();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Select Employee');", true);
            }
        }

        catch
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }


    public void grid_ddl_load()
    {
        try
        {
            for (grd = 0; grd < grid_loan.Rows.Count; grd++)
            {
                ddl_load(((DropDownList)grid_loan.Rows[grd].FindControl("grd_Loan")), ((DropDownList)grid_loan.Rows[grd].FindControl("grd_Employee")));

                //Loan           

                for (ddl_ex = 0; ddl_ex < ((DropDownList)grid_loan.Rows[grd].FindControl("grd_Loan")).Items.Count; ddl_ex++)
                {
                    if (((DropDownList)grid_loan.Rows[grd].FindControl("grd_Loan")).Items[ddl_ex].Value == loanlist[grd].loanid.ToString())
                    {
                        ((DropDownList)grid_loan.Rows[grd].FindControl("grd_Loan")).SelectedIndex = ddl_ex;
                    }
                }

                //Employee

                for (ddl_ex = 0; ddl_ex < ((DropDownList)grid_loan.Rows[grd].FindControl("grd_Employee")).Items.Count; ddl_ex++)
                {
                    if (((DropDownList)grid_loan.Rows[grd].FindControl("grd_Employee")).Items[ddl_ex].Value == loanlist[grd].EmployeeId.ToString())
                    {
                        ((DropDownList)grid_loan.Rows[grd].FindControl("grd_Employee")).SelectedIndex = ddl_ex;
                    }
                }

            }
        }

        catch 
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

    public void ddl_Branch_load()
    {
        try
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

        catch 
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

    protected void ddl_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddl_Branch.SelectedValue != "0")
            {
                ViewState["Loanentry_BranchID"] = Convert.ToInt32(ddl_Branch.SelectedValue);
                ddl_load(ddl_Loancode, ddl_empcode);
            }
        }

        catch 
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    protected void Txt_no_dedu_TextChanged(object sender, EventArgs e)
    {
        try
        {

            if (rd_loan_process.SelectedItem.Text == "By Flat Rate")
            {
                if (rd_calc.SelectedItem.Text == "Monthly Based")
                {
                    double amt = Convert.ToDouble(Txt_loan_amt.Text);
                    double interest = Convert.ToDouble(Txt_interest.Text);
                    double due_mon = Convert.ToDouble(Txt_no_dedu.Text);
                    double tot = (((amt * interest) / 100) / 12);
                    double tot_int = round_off(tot * due_mon);
                    double mon_int = round_off(tot + (amt / due_mon));
                    Txt_dedu_mon.Text = Convert.ToString(dec(Convert.ToDecimal(mon_int)));

                    Txt_interest_amt.Text = Convert.ToString(dec(Convert.ToDecimal(tot_int)));
                    double amt_interest = Convert.ToDouble(Txt_interest_amt.Text);
                    //Txt_cur_bal.Text = Convert.ToString(amt + amt_interest);
                    decimal cur_bal = Convert.ToDecimal(due_mon * mon_int);
                    Txt_cur_bal.Text = Convert.ToString(dec(cur_bal));

                }

            }

            else if (rd_loan_process.SelectedItem.Text == "By Diminishing Rate")
            {
                double amt = Convert.ToDouble(Txt_loan_amt.Text);
                double interest = Convert.ToDouble(Txt_interest.Text);
                int due_mon = Convert.ToInt32(Txt_no_dedu.Text);
                double tot = (((amt * interest) / 100) / 12);
                double tot_int = round_off(tot * due_mon);
                int mon_int = round_off(tot + (amt / due_mon));
                Txt_dedu_mon.Text = Convert.ToString(dec(Convert.ToDecimal(mon_int)));

                double ins_amt1 = 0;
                for (int j = due_mon; j >= 1; j--)
                {
                    ins_amt1 = (amt / j);
                    double intst = ((amt * double.Parse(Txt_interest.Text)) / 100) / 12;
                    amt = amt - ins_amt1;
                    con.Open();
                    SqlCommand cmd_dim = new SqlCommand("insert into paym_dim_intcal(pn_companyid,pn_branchid,pn_employeeid,loan_appid,installement_count,balance_amt,instal_amt,interest_amt,inst_count) values('" + employee.CompanyId + "','" + employee.BranchId + "','" + ddl_empcode.SelectedItem.Value + "','" + Txt_loan_id.Text + "','" + Txt_no_dedu.Text + "','" + Convert.ToDecimal(Txt_loan_amt.Text) + "','" + Convert.ToDecimal(ins_amt1) + "','" + Convert.ToDecimal(intst) + "','" + j + "')", con);
                    cmd_dim.ExecuteNonQuery();
                    con.Close();
                }
                con.Open();
                SqlCommand cmd_int = new SqlCommand("select sum(interest_amt) from paym_dim_intcal where loan_appid='" + Txt_loan_id.Text + "' ", con);
                SqlDataReader rd_int = cmd_int.ExecuteReader();
                decimal cur_bal = 0;
                if (rd_int.Read())
                {
                    cur_bal = Convert.ToDecimal(rd_int[0]);
                }
                rd_int.Close();
                con.Close();
                Txt_interest_amt.Text = Convert.ToString(dec(cur_bal));
                decimal cur_bal1 = decimal.Parse(Txt_loan_amt.Text) + cur_bal;
                Txt_cur_bal.Text = Convert.ToString(dec(cur_bal1));

            }
        }

        catch 
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }


    public double round_next(double value)
    {
        try
        {
            double d2 = Math.Round(value + 0.5);
            return d2;
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
            return 0;
        }
    }


    protected void Txt_dedu_mon_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Txt_dedu_mon.Text = Convert.ToString(dec(Convert.ToDecimal(Txt_dedu_mon.Text)));
            if (rd_loan_process.SelectedItem.Text == "By Flat Rate")
            {
                if (rd_calc.SelectedItem.Text == "Amount Based")
                {
                    double amt = Convert.ToDouble(Txt_loan_amt.Text);
                    double interest = Convert.ToDouble(Txt_interest.Text);
                    double tot = (((amt * interest) / 100) / 12);
                    double pay_mon = Convert.ToDouble(Txt_dedu_mon.Text);
                    double pay_period = round_next(amt / pay_mon);
                    decimal interest_amt = Convert.ToDecimal(tot * pay_period);
                    Txt_interest_amt.Text = Convert.ToString(dec(interest_amt));

                    decimal cur_bal = Convert.ToDecimal(amt + double.Parse(Txt_interest_amt.Text));
                    Txt_cur_bal.Text = Convert.ToString(dec(cur_bal));
                    Txt_no_dedu.Text = Convert.ToString(round_off((amt + double.Parse(Txt_interest_amt.Text)) / pay_mon));

                }
            }
        }

        catch 
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

    public int round_off(double amt)
    {
        try
        {
            int value = Convert.ToInt32(amt);
            return value;
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
            return 0;
        }
    }

    public decimal dec(decimal num)
    {
        try
        {
            num = Convert.ToDecimal(num.ToString("#,0.00"));
            return num;
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
            return 0;
        }
    }

    protected void ddl_empcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        string[] nam = ddl_empcode.SelectedItem.Text.Split('-');
        Txt_empnam.Text = nam[1];
    }

    protected void Btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddl_Loancode.SelectedIndex != 0)
            {
                con.Open();
                string[] nam = ddl_empcode.SelectedItem.Text.Split('-');
                string[] sd_split = Txt_loan_sdate.Text.Split('/');
                string sdate = sd_split[1] + "/" + sd_split[0] + "/" + sd_split[2];
                string[] ed_split = Txt_loan_effdate.Text.Split('/');
                string edate = ed_split[1] + "/" + ed_split[0] + "/" + ed_split[2];
                SqlCommand cm_chk = new SqlCommand("select status from paym_employee where pn_employeeid ='" + ddl_empcode.SelectedValue + "'and pn_branchid='" + pay.BranchId + "' and pn_companyid='" + pay.CompanyId + "'", con);
                SqlDataReader rd_stat = cm_chk.ExecuteReader();
                string e_stat = "";
                DateTime dat = DateTime.Now;
                double sub_bal = 0;
                double ins_amt = 0;
                int i = 0;
                SqlCommand cmd = new SqlCommand("select count(*) from loanentry where pn_branchid='" + pay.BranchId + "' and pn_companyid='" + pay.CompanyId + "'", con);
                int count = (int)cmd.ExecuteScalar();
                count = count + 1;

                string autoid = sd_split[2] + sd_split[1] + sd_split[0] + ed_split[2] + ed_split[1] + ed_split[0] + count.ToString("#####");
                int due = Convert.ToInt32(Txt_no_dedu.Text);
                if (rd_stat.Read())
                {
                    e_stat = rd_stat[0].ToString();
                }
                rd_stat.Close();
                if (e_stat == "Y")
                {
                    SqlCommand cmd_lsel = new SqlCommand("select pn_loanid from paym_loan where v_loanname='" + ddl_Loancode.SelectedItem.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' ", con);
                    SqlDataReader rd_lsel = cmd_lsel.ExecuteReader();
                    string loan_id = "";

                    if (rd_lsel.Read())
                    {
                        loan_id = rd_lsel[0].ToString();
                    }
                    rd_lsel.Close();

                    if (rd_loan_process.SelectedItem.Text == "By Flat Rate")
                    {
                        if (rd_calc.SelectedItem.Text == "Monthly Based")
                        {
                            SqlCommand cm_ins = new SqlCommand("insert into loanentry(pn_companyid,pn_branchid,pn_employeeid,Loan_autoID,fn_loanid,san_date,d_effdate,loan_amt,instalmentamt,instalmentcount,balance_amt,c_status,loan_name,loan_process,loan_calculation,comments,loan_appid,interest,tot_interest_amt,emp_name,loan_status) values('" + pay.CompanyId + "','" + pay.BranchId + "','" + ddl_empcode.SelectedItem.Value + "','" + autoid + "','" + loan_id + "','" + sdate + "','" + edate + "','" + Txt_loan_amt.Text + "','" + Txt_dedu_mon.Text + "','" + Txt_no_dedu.Text + "','" + Txt_cur_bal.Text + "','" + e_stat + "','" + ddl_Loancode.SelectedItem.Text + "','" + rd_loan_process.SelectedItem.Text + "','" + rd_calc.SelectedItem.Text + "','" + Txt_comments.Text + "','" + Txt_loan_id.Text + "','" + Txt_interest.Text + "','" + Txt_interest_amt.Text + "','" + Txt_empnam.Text + "','Pending' )", con);
                            cm_ins.ExecuteNonQuery();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Saved Successfully');", true);
                        }
                        else if (rd_calc.SelectedItem.Text == "Amount Based")
                        {
                            SqlCommand cm_ins1 = new SqlCommand("insert into loanentry(pn_companyid,pn_branchid,pn_employeeid,Loan_autoID,fn_loanid,san_date,d_effdate,loan_amt,instalmentamt,instalmentcount,balance_amt,c_status,loan_name,loan_process,loan_calculation,comments,loan_appid,interest,tot_interest_amt,emp_name,loan_status) values('" + pay.CompanyId + "','" + pay.BranchId + "','" + ddl_empcode.SelectedItem.Value + "','" + autoid + "','" + loan_id + "','" + sdate + "','" + edate + "','" + Txt_loan_amt.Text + "','" + Txt_dedu_mon.Text + "','" + Txt_no_dedu.Text + "','" + Txt_cur_bal.Text + "','" + e_stat + "','" + ddl_Loancode.SelectedItem.Text + "','" + rd_loan_process.SelectedItem.Text + "','" + rd_calc.SelectedItem.Text + "','" + Txt_comments.Text + "','" + Txt_loan_id.Text + "','" + Txt_interest.Text + "','" + Txt_interest_amt.Text + "','" + Txt_empnam.Text + "','Pending' )", con);
                            cm_ins1.ExecuteNonQuery();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Saved Successfully');", true);
                        }

                        SqlCommand cmd_sel = new SqlCommand("select d_effdate,balance_amt,instalmentamt from loanentry where loan_appid= '" + Txt_loan_id.Text + "' ", con);
                        SqlDataReader rd_sel = cmd_sel.ExecuteReader();

                        if (rd_sel.Read())
                        {
                            string stt = rd_sel[0].ToString();
                            dat = Convert.ToDateTime(rd_sel[0]);
                            sub_bal = Convert.ToDouble(rd_sel[1]);
                            ins_amt = Convert.ToDouble(rd_sel[2]);
                        }

                        for (i = 1; i <= due; i++)
                        {
                            if (rd_calc.SelectedItem.Text == "Monthly Based")
                            {
                                sub_bal = sub_bal - double.Parse(Txt_dedu_mon.Text);
                                SqlCommand cmd_in = new SqlCommand("insert into payoutput_loan(pn_companyid,pn_employeeid,pn_loanid,d_date,Amount,count_installement,pn_branchid,installement_count,loan_appid,instal_amt,balance_amt,loan_status) values('" + employee.CompanyId + "','" + ddl_empcode.SelectedItem.Value + "','" + ddl_Loancode.SelectedItem.Value + "','" + dat + "','" + Txt_loan_amt.Text + "','" + Txt_no_dedu.Text + "','" + employee.BranchId + "','" + i + "','" + Txt_loan_id.Text + "','" + Txt_dedu_mon.Text + "','" + sub_bal + "','Pending')", con);
                                cmd_in.ExecuteNonQuery();
                                dat = dat.AddMonths(1);
                            }
                            else if (rd_calc.SelectedItem.Text == "Amount Based")
                            {
                                if (sub_bal < double.Parse(Txt_dedu_mon.Text))
                                {
                                    double sample = sub_bal;
                                    sub_bal = sub_bal - sample;

                                    SqlCommand cmd_in = new SqlCommand("insert into payoutput_loan(pn_companyid,pn_employeeid,pn_loanid,d_date,Amount,count_installement,pn_branchid,installement_count,loan_appid,instal_amt,balance_amt,loan_status) values('" + employee.CompanyId + "','" + ddl_empcode.SelectedItem.Value + "','" + ddl_Loancode.SelectedItem.Value + "','" + dat + "','" + Txt_loan_amt.Text + "','" + Txt_no_dedu.Text + "','" + employee.BranchId + "','" + i + "','" + Txt_loan_id.Text + "','" + sample + "','" + sub_bal + "','Pending')", con);
                                    cmd_in.ExecuteNonQuery();
                                    dat = dat.AddMonths(1);
                                }
                                else
                                {
                                    sub_bal = sub_bal - double.Parse(Txt_dedu_mon.Text);

                                    SqlCommand cmd_in = new SqlCommand("insert into payoutput_loan(pn_companyid,pn_employeeid,pn_loanid,d_date,Amount,count_installement,pn_branchid,installement_count,loan_appid,instal_amt,balance_amt,loan_status) values('" + employee.CompanyId + "','" + ddl_empcode.SelectedItem.Value + "','" + ddl_Loancode.SelectedItem.Value + "','" + dat + "','" + Txt_loan_amt.Text + "','" + Txt_no_dedu.Text + "','" + employee.BranchId + "','" + i + "','" + Txt_loan_id.Text + "','" + Txt_dedu_mon.Text + "','" + sub_bal + "','Pending')", con);
                                    cmd_in.ExecuteNonQuery();
                                    dat = dat.AddMonths(1);
                                }
                            }
                        }

                        rd_sel.Close();
                    }

                    else if (rd_loan_process.SelectedItem.Text == "By Diminishing Rate")
                    {
                        int j = 1;

                        //SqlCommand cmd_dim_del = new SqlCommand("delete from paym_dim_intcal where pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' ", con);
                        //cmd_dim_del.ExecuteNonQuery();

                        SqlCommand cm_ins2 = new SqlCommand("insert into loanentry(pn_companyid,pn_branchid,pn_employeeid,fn_loanid,san_date,d_effdate,loan_amt,instalmentamt,instalmentcount,balance_amt,c_status,loan_name,loan_process,loan_calculation,comments,loan_appid,interest,tot_interest_amt,emp_name,loan_status) values('" + pay.CompanyId + "','" + pay.BranchId + "','" + ddl_empcode.SelectedItem.Value + "','" + loan_id + "','" + sdate + "','" + edate + "','" + Txt_loan_amt.Text + "','" + Txt_dedu_mon.Text + "','" + Txt_no_dedu.Text + "','" + Txt_cur_bal.Text + "','" + e_stat + "','" + ddl_Loancode.SelectedItem.Text + "','" + rd_loan_process.SelectedItem.Text + "','Monthly Diminishing','" + Txt_comments.Text + "','" + Txt_loan_id.Text + "','" + Txt_interest.Text + "','" + Txt_interest_amt.Text + "','" + Txt_empnam.Text + "','Pending' )", con);
                        cm_ins2.ExecuteNonQuery();

                        SqlCommand cmd_sel = new SqlCommand("select d_effdate,balance_amt,instalmentamt from loanentry where loan_appid= '" + Txt_loan_id.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' ", con);
                        SqlDataReader rd_sel = cmd_sel.ExecuteReader();

                        if (rd_sel.Read())
                        {
                            dat = Convert.ToDateTime(rd_sel[0]);
                            sub_bal = Convert.ToDouble(rd_sel[1]);
                            //ins_amt = Convert.ToDouble(rd_sel[2]);
                        }
                        rd_sel.Close();


                        for (i = due; i >= 1; i--)
                        {
                            SqlCommand sel_ins = new SqlCommand("select instal_amt,interest_amt from paym_dim_intcal where loan_appid='" + Txt_loan_id.Text + "' and inst_count='" + i + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' ", con);
                            SqlDataReader rd_ins = sel_ins.ExecuteReader();
                            double interest = 0;
                            if (rd_ins.Read())
                            {
                                ins_amt = Convert.ToDouble(rd_ins[0]);
                                interest = Convert.ToDouble(rd_ins[1]);
                            }
                            //double interest = ((sub_bal * double.Parse(Txt_interest.Text)) / 100) / 12;
                            ins_amt = ins_amt + interest;
                            if (sub_bal > ins_amt)
                            {
                                sub_bal = sub_bal - ins_amt;
                            }
                            else
                            {
                                ins_amt = sub_bal;
                                sub_bal = sub_bal - ins_amt;
                            }

                            SqlCommand cmd_dim = new SqlCommand("insert into paym_loan_diminishing(pn_companyid,pn_branchid,pn_employeeid,fn_loanid,loan_appid,loan_amount,balance_amt,installement_count,eff_date,instal_amt,months,loan_status) values('" + pay.CompanyId + "','" + pay.BranchId + "','" + ddl_empcode.SelectedItem.Value + "','" + ddl_Loancode.SelectedItem.Value + "','" + Txt_loan_id.Text + "','" + Convert.ToDecimal(Txt_loan_amt.Text) + "','" + Convert.ToDecimal(sub_bal) + "','" + Txt_no_dedu.Text + "','" + dat + "','" + Convert.ToDecimal(ins_amt) + "','" + j + "','Pending')", con);
                            cmd_dim.ExecuteNonQuery();

                            SqlCommand cmd_upd_dim = new SqlCommand("update loanentry set instalmentamt='" + ins_amt + "' where loan_appid='" + Txt_loan_id.Text + "' ", con);
                            cmd_upd_dim.ExecuteNonQuery();
                            dat = dat.AddMonths(1);
                            j++;

                        }
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Saved Successfully');", true);

                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Check Employee State');", true);
                }
                con.Close();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Specify the Loan Name');", true);
            }
        }

        catch(SqlException ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }

    }


    public double function_rd_nearest(double rup)
    {
        try
        {
            if (rup > 0)
            {
                double value;
                value = Math.Round(rup);
                return value;
            }

            else
            {
                return 0;
            }
       }
        catch 
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
            return 0;
        }
    }


    public double function_rd_next(double rup)
    {
      try
       {
            if (rup > 0)
            {
                double value;
                rup = rup + Convert.ToDouble(0.50);
                value = Math.Round(rup);
                return value;
            }

            else
            {
                return 0;
            }
        }

        catch 
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
            return 0;
        }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
    }


    protected void Btn_clear_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void rd_loan_process_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rd_loan_process.SelectedItem.Text == "By Diminishing Rate")
        {
            rd_calc.Enabled = false;

        }
        else
        {
            rd_calc.Enabled = true;
        }
    }
    protected void grid_loan_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Txt_loan_id.Text = ListBox1.SelectedItem.Text;
        ListBox1.Visible = false;
    }
    protected void Txt_loan_id_TextChanged(object sender, EventArgs e)
    {

    }
    protected void Txt_loan_amt_TextChanged(object sender, EventArgs e)
    {
        Txt_loan_amt.Text = Convert.ToString(dec(Convert.ToDecimal(Txt_loan_amt.Text)));
    }
    protected void Txt_interest_TextChanged(object sender, EventArgs e)
    {
        Txt_interest.Text = Convert.ToString(dec(Convert.ToDecimal(Txt_interest.Text)));
    }
    protected void Txt_interest_amt_TextChanged(object sender, EventArgs e)
    {
        Txt_interest_amt.Text = Convert.ToString(dec(Convert.ToDecimal(Txt_interest_amt.Text)));
    }
    protected void Txt_loan_effdate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime sanction_date = Convert.ToDateTime(Txt_loan_sdate.Text);
            DateTime effective_date = Convert.ToDateTime(Txt_loan_effdate.Text);

            if (sanction_date > effective_date)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Effective date should be gearter than the sanction date');", true);
                Txt_loan_effdate.Text = "";
            }
        }

        catch 
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

    protected void Btn_delete_Click(object sender, EventArgs e)
    {
        try
        {
            con.Open();

            if (rd_loan_process.SelectedItem.Text == "By Flat Rate")
            {
                SqlCommand cmd_ret_flat = new SqlCommand("select * from payoutput_loan where loan_appid = '" + Txt_loan_id.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' and pn_employeeid='" + ddl_empcode.SelectedItem.Value + "' and loan_status!='paid' ", con);
                SqlDataReader rd_loan = cmd_ret_flat.ExecuteReader();

                if (rd_loan.Read())
                {
                    SqlCommand cmd_del_loan = new SqlCommand("delete from loanentry where loan_appid='" + Txt_loan_id.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' and pn_employeeid='" + ddl_empcode.SelectedItem.Value + "' ", con);
                    cmd_del_loan.ExecuteNonQuery();
                    SqlCommand cmd_del_flat = new SqlCommand("delete from payoutput_loan where loan_appid='" + Txt_loan_id.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' and pn_employeeid='" + ddl_empcode.SelectedItem.Value + "' ", con);
                    cmd_del_flat.ExecuteNonQuery();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Record Deleted');", true);
                }
                clear();
            }

            else if (rd_loan_process.SelectedItem.Text == "By Diminishing Rate")
            {
                SqlCommand cmd_ret_dim = new SqlCommand("select * from paym_loan_diminishing where loan_appid='" + Txt_loan_id.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' and pn_employeeid='" + ddl_empcode.SelectedItem.Value + "' and loan_status!='paid'", con);
                SqlDataReader rd_loan = cmd_ret_dim.ExecuteReader();

                if (rd_loan.Read())
                {
                    SqlCommand cmd_del_loan = new SqlCommand("delete from loanentry where loan_appid='" + Txt_loan_id.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' and pn_employeeid='" + ddl_empcode.SelectedItem.Value + "' ", con);
                    cmd_del_loan.ExecuteNonQuery();
                    SqlCommand cmd_del_dim = new SqlCommand("delete from paym_loan_diminishing where loan_appid='" + Txt_loan_id.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' and pn_employeeid='" + ddl_empcode.SelectedItem.Value + "' ", con);
                    cmd_del_dim.ExecuteNonQuery();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Record Deleted');", true);
                }
                clear();
            }

            con.Close();
        }

        catch 
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }

    }

    public void clear()
    {
        try
        {
            rd_loan_process.ClearSelection();
            rd_calc.ClearSelection();
            ddl_empcode.SelectedIndex = -1;
            ddl_Loancode.SelectedIndex = -1;
            Txt_loan_id.Text = "";
            Txt_loan_amt.Text = "";
            Txt_empnam.Text = "";
            Txt_interest.Text = "";
            Txt_interest_amt.Text = "";
            Txt_loan_effdate.Text = "";
            Txt_loan_sdate.Text = "";
            Txt_no_dedu.Text = "";
            Txt_cur_bal.Text = "";
            Txt_dedu_mon.Text = "";
            Txt_comments.Text = "";
            Txt_rem_count.Text = "";
        }

        catch
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }


    //protected void Btn_update_Click(object sender, EventArgs e)
    //{
    //    con.Open();

    //    if (rd_loan_process.SelectedItem.Text == "By Flat Rate")
    //    {
    //        SqlCommand cmd_ret_flat = new SqlCommand("select * from payoutput_loan where loan_appid='" + Txt_loan_id.Text + "' where pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' and pn_employeeid='" + ddl_empcode.SelectedItem.Value + "' and loan_status!='paid' ", con);
    //        SqlDataReader rd_loan = cmd_ret_flat.ExecuteReader();

    //        if (rd_loan.Read())
    //        {
    //            SqlCommand cmd_upd_flat = new SqlCommand("update loanentry set loan_process = '" + rd_loan_process.Text + "',loan_calculation='" + rd_calc.Text + "',pn_employeeid='" + ddl_empcode.SelectedItem.Value + "',loan_name='" + ddl_Loancode.SelectedItem.Text + "',emp_name='" + Txt_empnam.Text + "',san_date='" + Txt_loan_sdate.Text + "',d_effdate='" + Txt_loan_effdate.Text + "',loan_amt='" + Txt_loan_amt.Text + "',interest='" + Txt_interest.Text + "',tot_interest_amt='" + Txt_interest_amt.Text + "',instalmentcount='" + Txt_no_dedu.Text + "',balance_amt='" + Txt_cur_bal.Text + "',instalmentamt='" + Txt_dedu_mon.Text + "',comments='" + Txt_comments.Text + "' where loan_appid='" + Txt_loan_id.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' ", con);
    //            cmd_upd_flat.ExecuteNonQuery();
    //            lbl_Error.Text = "Record updated";
    //        }

    //    }
    //    else if (rd_loan_process.SelectedItem.Text == "By Diminishing Rate")
    //    {
    //        SqlCommand cmd_ret_dim = new SqlCommand("select * from paym_loan_diminishing where loan_appid='" + Txt_loan_id.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' and pn_employeeid='" + ddl_empcode.SelectedItem.Value + "' and loan_status!='paid'", con);
    //        SqlDataReader rd_loan = cmd_ret_dim.ExecuteReader();

    //        if (rd_loan.Read())
    //        {
    //            SqlCommand cmd_dim_del = new SqlCommand("delete from paym_dim_intcal where loan_appid='" + Txt_loan_id.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' ", con);
    //            cmd_dim_del.ExecuteNonQuery();
    //            SqlCommand cmd_upd_flat = new SqlCommand("update loanentry set loan_process = '" + rd_loan_process.Text + "',loan_calculation='" + rd_calc.Text + "',pn_employeeid='" + ddl_empcode.SelectedItem.Value + "',loan_name='" + ddl_Loancode.SelectedItem.Text + "',emp_name='" + Txt_empnam.Text + "',san_date='" + Txt_loan_sdate.Text + "',d_effdate='" + Txt_loan_effdate.Text + "',loan_amt='" + Txt_loan_amt.Text + "',interest='" + Txt_interest.Text + "',tot_interest_amt='" + Txt_interest_amt.Text + "',instalmentcount='" + Txt_no_dedu.Text + "',balance_amt='" + Txt_cur_bal.Text + "',instalmentamt='" + Txt_dedu_mon.Text + "',comments='" + Txt_comments.Text + "' where loan_appid='" + Txt_loan_id.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' ", con);
    //            cmd_upd_flat.ExecuteNonQuery();
    //            lbl_Error.Text = "Record updated";
    //        }
    //        con.Close();
    //    }
    //}

    protected void Btn_cancel_Click(object sender, EventArgs e)
    {
        try
        {

            if (rd_loan_process.SelectedItem.Text == "By Flat Rate")
            {
                SqlCommand cmd_ret_flat = new SqlCommand("select * from payoutput_loan where loan_appid = '" + Txt_loan_id.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' and pn_employeeid='" + ddl_empcode.SelectedItem.Value + "' and loan_status!='paid' ", con);
                SqlDataReader rd_loan = cmd_ret_flat.ExecuteReader();

                if (rd_loan.Read())
                {
                    SqlCommand cmd_del_loan = new SqlCommand("delete from loanentry where loan_appid='" + Txt_loan_id.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' and pn_employeeid='" + ddl_empcode.SelectedItem.Value + "' ", con);
                    cmd_del_loan.ExecuteNonQuery();
                    SqlCommand cmd_del_flat = new SqlCommand("delete from payoutput_loan where loan_appid='" + Txt_loan_id.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' and pn_employeeid='" + ddl_empcode.SelectedItem.Value + "' ", con);
                    cmd_del_flat.ExecuteNonQuery();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Record Deleted');", true);
                }
                clear();
            }
            else if (rd_loan_process.SelectedItem.Text == "By Diminishing Rate")
            {
                SqlCommand cmd_ret_dim = new SqlCommand("select * from paym_loan_diminishing where loan_appid='" + Txt_loan_id.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' and pn_employeeid='" + ddl_empcode.SelectedItem.Value + "' and loan_status!='paid'", con);
                SqlDataReader rd_loan = cmd_ret_dim.ExecuteReader();

                if (rd_loan.Read())
                {
                    SqlCommand cmd_del_loan = new SqlCommand("delete from loanentry where loan_appid='" + Txt_loan_id.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' and pn_employeeid='" + ddl_empcode.SelectedItem.Value + "' ", con);
                    cmd_del_loan.ExecuteNonQuery();
                    SqlCommand cmd_del_dim = new SqlCommand("delete from paym_loan_diminishing where loan_appid='" + Txt_loan_id.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' and pn_employeeid='" + ddl_empcode.SelectedItem.Value + "' ", con);
                    cmd_del_dim.ExecuteNonQuery();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Record Deleted');", true);
                }
                clear();
            }
        }
        catch 
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

    protected void Img_btn_Click(object sender, EventArgs e)
    {
        try
        {
            con.Open();
            ListBox1.Visible = true;
            SqlDataAdapter cmd_sel = new SqlDataAdapter("select loan_appid  from loanentry where pn_branchid='" + pay.BranchId + "' and pn_companyid='" + pay.CompanyId + "' and pn_employeeid='" + ddl_empcode.SelectedItem.Value + "' and loan_status!='loan closed' ", con);
            DataSet ds = new DataSet();
            cmd_sel.Fill(ds, "loanentry");
            ListBox1.DataSource = ds;
            ListBox1.DataTextField = "loan_appid";
            ListBox1.DataBind();
            //ListBox1.DisplayMember = "loan_appid";
            con.Close();
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Select Employee');", true);
        }
    }

    protected void Txt_cur_bal_TextChanged(object sender, EventArgs e)
    {
        Txt_cur_bal.Text = Convert.ToString(dec(Convert.ToDecimal(Txt_cur_bal.Text)));
    }


    public void ddl_Department_load()
    {
        EmployeeList = employee.fn_getDepartmentList1(employee.BranchId);
        if (EmployeeList.Count > 0)
        {

            for (int ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {

                if (ddl_i == -1)
                {
                    ListItem es_list = new ListItem();

                    es_list.Text = "Select Department";
                    es_list.Value = "0";
                    ddl_department.Items.Add(es_list);
                }
                else
                {
                    ListItem es_list = new ListItem();

                    es_list.Value = EmployeeList[ddl_i].DepartmentId.ToString();
                    es_list.Text = EmployeeList[ddl_i].DepartmentName.ToString();
                    ddl_department.Items.Add(es_list);
                }
            }
        }
    }
    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Employee_load();
    }
    public void ddl_Employee_load()
    {
        ddl_empcode.Items.Clear();
        employee.DepartmentId = Convert.ToInt32(ddl_department.SelectedValue);
        EmployeeList = employee.fn_getEmployeeDepartment(employee);
        if (EmployeeList.Count > 0)
        {
            for (int ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem es_list = new ListItem();
                    es_list.Text = "Select Employee";
                    es_list.Value = "0";
                    ddl_empcode.Items.Add(es_list);
                }
                else
                {
                    ListItem es_list = new ListItem();
                    es_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
                    es_list.Text = EmployeeList[ddl_i].LastName.ToString();
                    ddl_empcode.Items.Add(es_list);
                }
            }
        }
    }
}

