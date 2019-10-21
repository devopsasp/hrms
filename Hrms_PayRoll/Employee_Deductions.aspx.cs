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

public partial class Bank_Loan_Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand com = new SqlCommand();
    Employee employee = new Employee();
    Company company = new Company();
    PayRoll pay = new PayRoll();

    Collection<Employee> EmployeesList;
    Collection<PayRoll> PFList;
    Collection<PayRoll> ESIList;
    Collection<PayRoll> OTList;
    Collection<PayRoll> PTList;
    Collection<Employee> EmployeeList;
    Collection<PayRoll> Ded_List;
    Collection<PayRoll> emp_ded_List;
    Collection<Company> CompanyList, ddlBranchsList;
    Collection<PayRoll> CheckingList;
    Collection<PayRoll> PT_Settings;
    Collection<PayRoll> payinput;
    Collection<PayRoll> AttendanceList;
    Collection<PayRoll> OT_Settings;
    Collection<PayRoll> RegEarningsList;
    Collection<PayRoll> NonRegEarningsList;
    Collection<PayRoll> RegDeductionList;
    Collection<PayRoll> LWF_Settings;
    Collection<PayRoll> LoanEntryList;
    Collection<PayRoll> LoanCancelList;
    Collection<PayRoll> LoanPreCloserList;
    Collection<PayRoll> PayOutput_LoanList;
    Collection<PayRoll> RegEarningsCheck;
    Collection<PayRoll> RegEarningsChange;

    DataSet ds_PT;
    DataSet ds_emp = new DataSet();
    DataSet ds_userrights;

    int i_amt = 0, i_temp = 0, NetPay = 0, i_emp, i_regearn, i_nonregearn, i_regded, i_nonregded, i_Precloser, i_loan, i_loan_ex, i_length, emp_count = 0;

    double abs = 0, prs = 0, hday = 0, ldays = 0, rc = 0, wkoff = 0, total_wdays = 0, paid = 0, odd = 0, cod = 0, tod = 0, att_amt = 0, basic = 0, ern_basic = 0, ot_calc = 0, ot_amt = 0, e_arr = 0, d_arr = 0;
    double d_temp = 0, A_calcdays = 0, A_paiddays = 0, A_Absentdays = 0, A_WeekOffdays = 0, E_amt = 0, E_amt_A = 0, E_actamt = 0, E_actamt_A = 0, N_E_amt = 0, N_E_actamt = 0, D_amt = 0, D_actamt = 0;
    double N_D_amt = 0, N_D_actamt = 0, f_ot = 0, f_pt = 0, f_PF = 0, f_ESI = 0, cal_amt = 0, PFAmt = 0, EPFAmt = 0;
    double Emp_ESIAmt = 0, Er_ESIAmt = 0, Emp_BasAmt = 0, Er_BasAmt = 0, OT_Amt = 0, PT_Amt = 0, IT_Amt = 0, Loan_amt = 0, Loan_Actamt = 0;
    double FPFAmt = 0, Balance_Amt = 0, Net_Earn = 0, Net_Ded = 0, Net_Act_Earn = 0, Net_Act_Earn_A = 0, Net_Act_Ded = 0;

    string s_login_role;
    string str_temp = "", str_query = "", _path = "", str_Month = "", str_Year = "", s_query = "", Query = "", s_form, _value = "";
    int ddl_i;
    bool bool_Month = false;

    string emp_cod;
    int c_rd = 0;
    int txt_ceiling = 0;
    decimal txt_EPF = 0, txt_pf = 0;
    char chk_ceiling = ' ';
    char chk_allwance = ' ';
    double pf, epf, fpf, tot_pf, t_epf, t_pf, gross, bas_sal, upper_limit, pro_F;
    double total_pf = 0;

    protected void Page_Load(object sender, EventArgs e)
    {


        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);

        pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        s_login_role = Request.Cookies["Login_temp_Role"].Value;



        if (!IsPostBack)
        {
            period_load();

            CompanyList = company.fn_getCompany();
            if (CompanyList.Count > 0)
            {

                switch (s_login_role)
                {
                    case "a":
                        tbl_deductions.Visible = false;
                        ddl_Branch.Visible = true;
                        ddl_Branch_load();
                        break;

                    case "h":

                        ddl_Branch.Visible = false;
                        tbl_deductions.Visible = true;
                        ddl_department_load();
                        break;

                    case "u":
                        s_form = "45";

                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            ddl_Branch.Visible = false;
                            tbl_deductions.Visible = true;
                        }
                        else
                        {
                            Response.Cookies["Msg_Session"].Value = "Permission Restricted. Please Contact Administrator.";
                            Response.Redirect("~/Company_Home.aspx");
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

    public void load()
    {
        pay.d_date = employee.Convert_ToSqlDate(date_change(txt_fromdate.Text));
        pay.temp_str = "Set Dateformat dmy;select ed.pn_EmployeeID,ed.pn_deductionID,ed.Amount,ed.d_Date,e.EmployeeCode,d.v_deductionCode from payoutput_deductions ed,paym_Employee e,paym_deduction d where ed.d_Date='" + pay.d_date + "' and e.pn_EmployeeID=ed.pn_EmployeeID and ed.pn_EmployeeID = '" + ddl_Employee.SelectedValue + "' and d.pn_deductionID=ed.pn_deductionID and ed.pn_CompanyID='" + employee.CompanyId + "' and ed.pn_BranchID='" + employee.BranchId + "';Set Dateformat mdy";
        SqlDataAdapter ad = new SqlDataAdapter(pay.temp_str, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);

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
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
            tr_chk.Visible = true;
        }

        ddl_allowance_load();


    }


    public void ddl_allowance_load()
    {
        con.Open();
        com = new SqlCommand("SELECT * FROM paym_deduction WHERE pn_BranchID='" + employee.BranchId + "'", con);
        SqlDataReader re = com.ExecuteReader();
        while (re.Read())
        {
            ListItem es_list = new ListItem();
   
            DropDownList ddl_allowance = (DropDownList)GridView1.FooterRow.FindControl("ddl_allowance");

            es_list.Value = re["pn_deductionID"].ToString();
            es_list.Text = re["v_deductionCode"].ToString();

            ddl_allowance.Items.Add(es_list);
        }
        re.Close();
        con.Close();
    }

    public void load1()
    {
        pay.d_date = employee.Convert_ToSqlDate(date_change(txt_fromdate.Text));
        pay.temp_str = "set dateformat dmy;select distinct ed.pn_DeductionID,ed.Amount,ed.d_Date,d.v_DeductionCode from payoutput_Deductions ed,paym_Employee e,paym_deduction d where ed.d_Date = '" + pay.d_date + "' and ed.flag = 'S' and ed.Mode = 'A' and ed.pn_DepartmentName = '" + ddl_department.SelectedItem.Text + "' and e.pn_EmployeeID=ed.pn_EmployeeID and d.pn_DeductionID=ed.pn_DeductionID and ed.pn_CompanyID='" + employee.CompanyId + "' and ed.pn_BranchID='" + employee.BranchId + "';set dateformat mdy";
        SqlDataAdapter ad = new SqlDataAdapter(pay.temp_str, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
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
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
            tr_chk.Visible = true;
        }
        ddl_allowance_load();
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
                    list.Value = "sb";
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
        if (ddl_Branch.SelectedValue != "sb")
        {
            ViewState["NonEarn_BranchID"] = Convert.ToInt32(ddl_Branch.SelectedValue);
            tbl_deductions.Visible = true;
        }
        else
        {
            tbl_deductions.Visible = false;
        }
    }

    public void period_load()
    {
        con.Open();
        com = new SqlCommand("Select top 10 * from salary_period where pn_branchid = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "' order by from_date desc", con);
        SqlDataReader re = com.ExecuteReader();
        while (re.Read())
        {
            ddl_periodcode.Items.Add(re["period_code"].ToString());
        }
        re.Close();
        con.Close();
    }

    protected void ddl_periodcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        con.Open();
        com = new SqlCommand("Select * from salary_period where pn_branchid = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "' and period_code='" + ddl_periodcode.SelectedItem.Text + "'", con);
        SqlDataReader re = com.ExecuteReader();
        if (re.Read())
        {
            txt_fromdate.Text = Convert.ToDateTime(re["from_date"]).ToString("dd/MM/yyyy");
            txt_todate.Text = Convert.ToDateTime(re["to_date"]).ToString("dd/MM/yyyy");
        }
        re.Close();
        con.Close();
    }

    public void ddl_department_load()
    {


        if (s_login_role == "a")
        {
            employee.BranchId = Convert.ToInt32(ddl_Branch.SelectedItem.Value);
        }
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
                    ddl_department.Items.Add(list);
                }
                else
                {
                    ListItem list = new ListItem();

                    list.Text = EmployeeList[ddl_i].DepartmentName;
                    list.Value = EmployeeList[ddl_i].DepartmentId.ToString();
                    ddl_department.Items.Add(list);
                }
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('No Department Available');", true);
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Department Available');", true);
        }

    }

    protected void chk_empname_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        tr_chk.Visible = true;
        if (ddl_department.SelectedValue != "sd")
        {
            ddl_employee_load();
        }
    }

    public void ddl_employee_load()
    {
        //employee dropdown
        ddl_Employee.Items.Clear();

        if (s_login_role == "a")
        {
            employee.BranchId = (int)ViewState["Appraisal_BranchID"];
        }

        if (s_login_role == "h")
        {
            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        }

        str_query = "Select a.pn_EmployeeID, a.EmployeeCode, a.Employee_First_Name from paym_employee a, paym_employee_profile1 b where a.pn_CompanyID=b.pn_CompanyID and a.pn_BranchID=b.pn_BranchID and a.pn_EmployeeID=b.pn_EmployeeID and b.pn_DepartmentID=" + ddl_department.SelectedValue + " and b.pn_CompanyID=" + employee.CompanyId + " and a.status='y' and b.pn_BranchID=" + employee.BranchId;

        EmployeeList = employee.fn_getEmplist(str_query);

        if (EmployeeList.Count > 0)
        {
            for (ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();
                    e_list.Text = "All";
                    e_list.Value = "All";
                    ddl_Employee.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();
                    e_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
                    e_list.Text = EmployeeList[ddl_i].LastName.ToString();
                    ddl_Employee.Items.Add(e_list);
                }
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('No Employees Available');", true);
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Employees Found');", true);
        }
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        load();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "add")
        {
            string amt = ((TextBox)GridView1.FooterRow.FindControl("txt_amount")).Text;
            string alln = ((DropDownList)GridView1.FooterRow.FindControl("ddl_allowance")).Text;
            string allc = ((DropDownList)GridView1.FooterRow.FindControl("ddl_allowance")).SelectedValue;
            con.Close();

            AddNewRecord(alln, allc, amt);
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string ID = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lbl_all")).Text;
        string amt = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("Label12")).Text;

        DeleteRecord(ID, amt);
        load();
    }

    private void DeleteRecord(string ID, string amt)
    {

        string sqlStatement = "Set Dateformat dmy;DELETE FROM payoutput_deductions WHERE pn_EmployeeiD= @pn_EmployeeID and pn_deductionID = @pn_deductionID and Amount = @Amount and d_Date = @d_Date and pn_CompanyID = @Pn_CompanyID and pn_BranchID = @pn_BranchID;Set Dateformat mdy; ";
        try
        {
            pay.d_date = employee.Convert_ToSqlDate(date_change(txt_fromdate.Text));
            con.Open();
            SqlCommand cmd = new SqlCommand(sqlStatement, con);
            cmd.Parameters.AddWithValue("@pn_EmployeeID", ddl_Employee.SelectedValue);
            cmd.Parameters.AddWithValue("@pn_deductionID", ID);
            cmd.Parameters.AddWithValue("@Amount", amt);
            cmd.Parameters.AddWithValue("@d_Date", pay.d_date);
            cmd.Parameters.AddWithValue("@pn_CompanyID", pay.CompanyId);
            cmd.Parameters.AddWithValue("@pn_BranchID", pay.BranchId);
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
            con.Close();
        }
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex; // turn to edit mode
        load();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow Gvrow = GridView1.Rows[e.RowIndex];
        if (Gvrow != null)
        {
            pay.d_date = employee.Convert_ToSqlDate(date_change(txt_fromdate.Text));
            string deductionname = ((Label)Gvrow.FindControl("ddl_eallowance")).Text;
            string deductionCode = ((Label)Gvrow.FindControl("lbl_alledit")).Text;
            string amt = ((TextBox)Gvrow.FindControl("txt_eamount")).Text;

            con.Open();
            com = new SqlCommand("Set Dateformat dmy;update payoutput_deductions set Amount='" + amt + "' where d_date='" + pay.d_date + "' and pn_deductionID='" + deductionCode + "' and pn_EmployeeID = '" + ddl_Employee.SelectedValue + "';Set Dateformat mdy; ", con);
            com.ExecuteNonQuery();
            con.Close();
            GridView1.EditIndex = -1; // turn to edit mode
            load();
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void AddNewRecord(string alln, string allc, string amt)
    {
        pay.d_FromDate = employee.Convert_ToSqlDate(txt_fromdate.Text);
        pay.d_ToDate = employee.Convert_ToSqlDate(txt_todate.Text);
        pay.d_date = employee.Convert_ToSqlDate(date_change(txt_fromdate.Text));
        pay.DeductionId = Convert.ToInt32(allc);
        pay.Empname = ddl_department.SelectedItem.Text;
        pay.status = 'S';
        if (ddl_Employee.SelectedItem.Text == "All")
        {
            pay.pay_mode = "A";
        }
        else
        {
            pay.pay_mode = "I";
        }
        pay.Ded_Amount = Convert.ToDouble(amt);
        pay.Act_Amount = 0.0;
        if (ddl_Employee.SelectedItem.Text == "All")
        {
            for (i_emp = 1; i_emp < ddl_Employee.Items.Count; i_emp++)
            {
                pay.EmployeeId = Convert.ToInt32(ddl_Employee.Items[i_emp].Value);
                employee.EmployeeId = Convert.ToInt32(ddl_Employee.Items[i_emp].Value);
                pay.PayOutput_Deductions(pay);
            }
            load1();
        }
        else
        {
            string query = @"Set Dateformat dmy;INSERT INTO payoutput_deductions (pn_CompanyID, pn_BranchID, pn_EmployeeID,pn_deductionID,d_Date,d_From_Date,d_To_Date,Flag,Amount) VALUES('" + employee.CompanyId + "','" + employee.BranchId + "','" + ddl_Employee.SelectedValue + "','" + allc + "','" + pay.d_date + "','" + pay.d_FromDate + "','" + pay.d_ToDate + "','S','" + amt + "');Set Dateformat mdy;";

            SqlCommand myCommand = new SqlCommand(query, con);

            con.Open();

            myCommand.ExecuteNonQuery();

            con.Close();

            load();
        }
    }

    protected void Btn_calc_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddl_periodcode.SelectedItem.Text != "Select" && ddl_department.SelectedItem.Text != "Select Department" && ddl_Employee.SelectedItem.Text != "")
            {
                pay.d_FromDate = employee.Convert_ToSqlDate(txt_fromdate.Text);
                pay.d_ToDate = employee.Convert_ToSqlDate(txt_todate.Text);
                pay.d_date = employee.Convert_ToSqlDate(date_change(txt_fromdate.Text));
                pay.strDate = employee.Convert_ToSqlDatestring(date_change(txt_todate.Text));
                pay.Empname = ddl_department.SelectedItem.Text;
                CheckingList = pay.fn_Out_PayOutput_Netpay(pay);
                if (CheckingList.Count <= 0)
                {
                    emp_ded_List = pay.fn_Out_PayOutput_Deductios(pay);
                    if (emp_ded_List.Count <= 0)
                    {
                        Initial_Processing();
                        if (emp_count != 0)
                        {
                            if (ddl_Employee.SelectedItem.Text == "All")
                            {
                                load1();
                            }
                            else
                            {
                                load();
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Attendance Found');", true);
                        }
                    }
                    else
                    {
                        if (ddl_Employee.SelectedItem.Text != "All")
                        {
                            pay.EmployeeId = Convert.ToInt32(ddl_Employee.SelectedValue);
                            emp_ded_List = pay.fn_Out_PayOutput_Deduction_Mode(pay);
                            if (emp_ded_List.Count <= 0)
                            {
                                Initial_Processing();
                                load();
                            }
                            else
                            {
                                load();
                            }
                        }
                        else
                        {
                            load1();
                        }
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Processing done for this month already');", true);
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Processing done for this month already');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Error! All fields are mandatory.');", true);
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error! All fields are mandatory.');", true);
            }
        }

        catch
        {
            ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Error Occured!');", true);
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
        }
    }


    public void Initial_Processing()
    {
        PT_Settings = pay.fn_In_PTax_Settings(pay);
        if (PT_Settings.Count > 0)
        {
            if (ddl_Employee.SelectedItem.Text == "All")
            {
                //for (i_emp = 1; i_emp < ddl_Employee.Items.Count; i_emp++)
                //{
                //    abs = 0; prs = 0; hday = 0; ldays = 0; rc = 0; wkoff = 0; total_wdays = 0; paid = 0; odd = 0; cod = 0; tod = 0; att_amt = 0; basic = 0; ern_basic = 0; ot_calc = 0; ot_amt = 0; e_arr = 0; d_arr = 0;
                //    N_D_amt = 0; N_D_actamt = 0; f_ot = 0; f_pt = 0; f_PF = 0; f_ESI = 0; cal_amt = 0; PFAmt = 0; EPFAmt = 0;
                //    Emp_ESIAmt = 0; Er_ESIAmt = 0; Emp_BasAmt = 0; Er_BasAmt = 0; OT_Amt = 0; PT_Amt = 0; IT_Amt = 0; Loan_amt = 0; Loan_Actamt = 0;
                //    FPFAmt = 0; Balance_Amt = 0; Net_Earn = 0; Net_Ded = 0; Net_Act_Earn = 0; Net_Act_Earn_A = 0; Net_Act_Ded = 0;

                //    pay.EmployeeId = Convert.ToInt32(ddl_Employee.Items[i_emp].Value);
                //    employee.EmployeeId = Convert.ToInt32(ddl_Employee.Items[i_emp].Value);
                //    payinput = pay.fn_payinput_check(pay);

                //    if (payinput.Count <= 0)
                //    {
                //        Month_Attendance();
                //    }

                //    AttendanceList = pay.fn_In_PayInput(pay);
                //    if (AttendanceList.Count > 0)
                //    {
                //        A_calcdays = AttendanceList[0].Calc_Days;
                //        A_Absentdays = AttendanceList[0].Absent_Days;
                //        A_WeekOffdays = AttendanceList[0].WeekOffDays;

                //        A_paiddays = AttendanceList[0].Paid_Days + AttendanceList[0].Earn_Arrears;
                //        A_paiddays = A_paiddays - AttendanceList[0].Ded_Arrears;

                //        Processing();
                emp_count++;
                //    }
                //    else
                //    {
                //        lbl_Error.Text = "No attendance found for one or more employees";
                //    }
                //}
            }
            else
            {
                abs = 0; prs = 0; hday = 0; ldays = 0; rc = 0; wkoff = 0; total_wdays = 0; paid = 0; odd = 0; cod = 0; tod = 0; att_amt = 0; basic = 0; ern_basic = 0; ot_calc = 0; ot_amt = 0; e_arr = 0; d_arr = 0;
                N_D_amt = 0; N_D_actamt = 0; f_ot = 0; f_pt = 0; f_PF = 0; f_ESI = 0; cal_amt = 0; PFAmt = 0; EPFAmt = 0;
                Emp_ESIAmt = 0; Er_ESIAmt = 0; Emp_BasAmt = 0; Er_BasAmt = 0; OT_Amt = 0; PT_Amt = 0; IT_Amt = 0; Loan_amt = 0; Loan_Actamt = 0;
                FPFAmt = 0; Balance_Amt = 0; Net_Earn = 0; Net_Ded = 0; Net_Act_Earn = 0; Net_Act_Earn_A = 0; Net_Act_Ded = 0;

                pay.EmployeeId = Convert.ToInt32(ddl_Employee.SelectedValue);
                employee.EmployeeId = Convert.ToInt32(ddl_Employee.SelectedValue);
                payinput = pay.fn_payinput_check(pay);

                if (payinput.Count <= 0)
                {
                    Month_Attendance();
                }

                AttendanceList = pay.fn_In_PayInput(pay);
                if (AttendanceList.Count > 0)
                {
                    A_calcdays = AttendanceList[0].Calc_Days;
                    A_Absentdays = AttendanceList[0].Absent_Days;
                    A_WeekOffdays = AttendanceList[0].WeekOffDays;
                    A_paiddays = AttendanceList[0].Paid_Days;
                    Processing();
                    emp_count++;

                }
                else
                {
                    ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('No Attendance found for one or more employees');", true);
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Attendance found for one or more employees');", true);
                }
            }
        }

        else
        {
            ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Enter Professional Tax Settings');", true);
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter Professional Tax Settings');", true);
        }
    }

    public void Processing()
    {
        Reg_Earnings();
        Non_Reg_Earnings();
        Reg_deduction();
    }

    public void Month_Attendance()
    {
        try
        {
            con.Open();
            string stats = "", att_bonus = "";
            TimeSpan tot_ot = new TimeSpan(0, 0, 0);

            //DateTime othr;
            pay.strDate = employee.Convert_ToSqlDatestring(date_change(txt_fromdate.Text));
            pay.d_date = employee.Convert_ToSqlDate(date_change(txt_todate.Text));
            string fdate = txt_fromdate.Text;
            string tdate = txt_todate.Text;
            string pdate = "";
            string[] fd = fdate.Split('/');
            string[] td = tdate.Split('/');
            string[] pd = pdate.Split('/');
            string fr_date = fd[1] + "/" + fd[0] + "/" + fd[2];
            string to_date = td[1] + "/" + td[0] + "/" + td[2];
            string p_date = pd[1] + "/" + pd[0] + "/" + pd[2];
            pay.Str_From_date = fr_date;
            pay.Str_To_date = to_date;
            tot_ot = TimeSpan.Parse("00:00:00");
            abs = 0; prs = 0; hday = 0; ldays = 0; rc = 0; wkoff = 0; paid = 0; OT_Amt = 0; basic = 0; ern_basic = 0;

            com = new SqlCommand("SELECT * FROM time_card where dates between '" + fr_date + "' and '" + to_date + "' and pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' and pn_EmployeeID = '" + pay.EmployeeId + "' order by dates ", con);

            SqlDataReader rd_calc = com.ExecuteReader();
            if (rd_calc.HasRows)
            {
                while (rd_calc.Read())
                {
                    rc++;
                    DateTime othr = Convert.ToDateTime(rd_calc["ot_hrs"]);
                    string otim = othr.ToString("HH:mm");
                    tot_ot += TimeSpan.Parse(otim);

                    stats = Convert.ToString(rd_calc["status"]);
                    if (stats == "XX" || stats == "LL")
                    {
                        prs += 1;
                    }
                    else if (stats == "XA" || stats == "AX" || stats == "LA" || stats == "AL")
                    {
                        prs += 0.5;
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
            }
            else
            {
                return;
            }
            //  OnDuty_days, Compoff_Days, Tour_Days, Att_Bonus, Att_BonusAmount, 
            rd_calc.Close();

            string ty = "";
            com = new SqlCommand("SELECT * FROM attendance_ceiling where pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' ", con);
            SqlDataReader rd_ce = com.ExecuteReader();
            if (rd_ce.Read())
            {
                ty = Convert.ToString(rd_ce["month_type"]);
            }
            if (ty == "Week-off ex")
            {
                paid = prs + ldays + hday;
            }
            else
            {
                paid = prs + ldays + wkoff + hday;
            }
            double ovt = 0, otround = 0;
            string str_ot = Convert.ToString(tot_ot);
            string[] ot_spl = str_ot.Split(':');
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
            ovt = Convert.ToDouble(ot_spl[0]) + otround;

            com = new SqlCommand("SELECT top 1 * FROM salary_structure where  pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' and pn_EmployeeID = '" + pay.EmployeeId + "' and effective_date <= '" + fr_date + "' order by effective_date desc ", con);
            SqlDataReader rd_bas = com.ExecuteReader();
            if (rd_bas.Read())
            {
                basic = Convert.ToDouble(rd_bas["salary"]);
            }

            //com = new SqlCommand("update paym_employee set basic_salary = '" + basic + "' where pn_EmployeeID='" + pay.EmployeeId + "' and pn_BranchID = '" + employee.BranchId + "' and pn_CompanyID='" + employee.CompanyId + "'", con);
            //com.ExecuteNonQuery();

            com = new SqlCommand("SELECT OT_calc FROM paym_employee where  pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' and pn_EmployeeID = '" + pay.EmployeeId + "' ", con);
            SqlDataReader rd_otc = com.ExecuteReader();
            if (rd_otc.Read())
            {
                ot_calc = Convert.ToDouble(rd_otc[0]);
            }
            ern_basic = (basic / total_wdays) * paid;

            if (ovt > 0)
            {
                OT_Settings = pay.fn_In_OT_Settings1(pay);

                if (OT_Settings.Count > 0)
                {
                    OT_Amt = ((basic / OT_Settings[0].OT_Days) / OT_Settings[0].OT_HRS) * ovt * ot_calc;
                }
            }

            pay.Act_Basic = basic;
            pay.Earned_Basic = ern_basic;
            com = new SqlCommand("Insert into payinput(pn_Companyid, pn_BranchID, pn_EmployeeID, d_Date, d_from_Date, d_To_Date, Calc_Days, Paid_Days, Present_Days, Absent_Days, TotLeave_Days, WeekOffDays, Holidays, OnDuty_days, Compoff_Days, Tour_Days, Att_Bonus, Att_BonusAmount, OT_HRS, Earn_Arrears, Ded_Arrears, ot_value, ot_Amt, Act_Basic, Earn_Basic, Mode, Flag) values ('" + employee.CompanyId + "','" + employee.BranchId + "','" + pay.EmployeeId + "','" + pay.strDate + "','" + fr_date + "','" + to_date + "','" + rc + "','" + paid + "','" + prs + "','" + abs + "','" + ldays + "','" + wkoff + "','" + hday + "','" + odd + "','" + cod + "','" + tod + "','" + att_bonus + "','" + att_amt + "','" + tot_ot + "','" + e_arr + "','" + d_arr + "','" + ovt + "','" + OT_Amt + "','" + basic + "','" + ern_basic + "','R','Y')", con);
            com.ExecuteNonQuery();
            con.Close();

        }
        catch (Exception ex)
        {
            //lbl_error.Text = "Already the Processing are done for this month!";
        }
    }

    public void Reg_Earnings()
    {
        RegEarningsCheck = pay.fn_Check_Earnings(pay);

        if (RegEarningsCheck.Count <= 0)
        {
            RegEarningsList = pay.fn_In_Earnings_Slab(pay);

            if (RegEarningsList.Count > 0)
            {

                for (i_regearn = 0; i_regearn < RegEarningsList.Count; i_regearn++)
                {
                    pay.Count = i_regearn;
                    E_actamt = RegEarningsList[i_regearn].Amount;

                    if (RegEarningsList[i_regearn].OT == 'Y')
                    {
                        f_ot = f_ot + E_actamt;
                    }


                    if (RegEarningsList[i_regearn].LOP == 'Y')
                    {
                        E_amt = (E_actamt / A_calcdays) * A_paiddays;
                    }

                    else
                    {
                        E_amt = E_actamt;
                    }

                    if (RegEarningsList[i_regearn].PT == 'Y')
                    {
                        if (PT_Settings[0].PTtype == 'a')
                        {
                            f_pt = f_pt + E_actamt;
                        }
                        else
                        {
                            f_pt = f_pt + E_amt;
                        }
                    }

                    if (RegEarningsList[i_regearn].Pf == 'Y')
                    {
                        f_PF = f_PF + E_amt;
                    }

                    if (RegEarningsList[i_regearn].Esi == 'Y')
                    {
                        f_ESI = f_ESI + E_amt;
                    }

                    pay.EarningsId = RegEarningsList[i_regearn].EarningsId;
                    pay.EarningsCode = RegEarningsList[i_regearn].EarningsCode;



                    pay.status = 'E';
                    pay.Act_Amount = E_actamt;
                    pay.Earn_Amount = round_up(E_amt);

                    Net_Act_Earn = Net_Act_Earn + E_actamt;
                    Net_Earn = Net_Earn + E_amt;

                }
            }

            else
            {
                RegEarningsList = pay.fn_In_Earnings(pay);

                if (RegEarningsList.Count > 0)
                {

                    for (i_regearn = 0; i_regearn < RegEarningsList.Count; i_regearn++)
                    {
                        pay.Count = i_regearn;
                        E_actamt = RegEarningsList[i_regearn].Amount;

                        if (RegEarningsList[i_regearn].OT == 'Y')
                        {
                            f_ot = f_ot + E_actamt;
                        }

                        if (RegEarningsList[i_regearn].LOP == 'Y')
                        {
                            E_amt = (E_actamt / A_calcdays) * A_paiddays;
                        }

                        else
                        {
                            E_amt = E_actamt;

                        }

                        if (RegEarningsList[i_regearn].PT == 'Y')
                        {
                            if (PT_Settings[0].PTtype == 'a')
                            {
                                f_pt = f_pt + E_actamt;
                            }
                            else
                            {
                                f_pt = f_pt + E_amt;
                            }

                        }

                        if (RegEarningsList[i_regearn].Pf == 'Y')
                        {
                            f_PF = f_PF + E_amt;
                        }

                        if (RegEarningsList[i_regearn].Esi == 'Y')
                        {
                            f_ESI = f_ESI + E_amt;
                        }

                        pay.EarningsId = RegEarningsList[i_regearn].EarningsId;
                        pay.EarningsCode = RegEarningsList[i_regearn].EarningsCode;


                        pay.status = 'E';
                        pay.Act_Amount = E_actamt;
                        pay.Earn_Amount = round_up(E_amt);

                        Net_Act_Earn = Net_Act_Earn + E_actamt;
                        Net_Earn = Net_Earn + E_amt;

                    }
                }
            }

        }
        else
        {
            RegEarningsCheck = pay.fn_Check_Earnings_Change(pay);
            E_amt = 0; E_actamt = 0;
            for (i_regearn = 0; i_regearn < RegEarningsCheck.Count; i_regearn++)
            {
                E_actamt += RegEarningsCheck[i_regearn].Amount;
                pay.Count = i_regearn;
                E_amt = RegEarningsCheck[i_regearn].Amount;
                if (RegEarningsCheck[i_regearn].OT == 'Y')
                {
                    f_ot = f_ot + E_actamt;
                }

                if (RegEarningsCheck[i_regearn].PT == 'Y')
                {
                    if (PT_Settings[0].PTtype == 'a')
                    {
                        f_pt = f_pt + E_actamt;
                    }
                    else
                    {
                        f_pt = f_pt + E_amt;
                    }

                }

                if (RegEarningsCheck[i_regearn].Pf == 'Y')
                {
                    f_PF = f_PF + E_amt;
                }

                if (RegEarningsCheck[i_regearn].Esi == 'Y')
                {
                    f_ESI = f_ESI + E_amt;
                }


            }
            pay.Earn_Amount = round_up(E_actamt);
            Net_Earn = round_up(E_actamt);
        }
    }



    public void Non_Reg_Earnings()
    {

        NonRegEarningsList = pay.fn_Check_Non_Earnings(pay);

        if (NonRegEarningsList.Count > 0)
        {

            for (i_nonregearn = 0; i_nonregearn < NonRegEarningsList.Count; i_nonregearn++)
            {
                N_E_actamt = NonRegEarningsList[i_nonregearn].Amount;

                if (NonRegEarningsList[i_nonregearn].OT == 'Y')
                {
                    f_ot = f_ot + N_E_actamt;
                }

                N_E_amt = N_E_actamt;

                if (NonRegEarningsList[i_nonregearn].PT == 'Y')
                {
                    f_pt = f_pt + N_E_amt;
                }

                if (NonRegEarningsList[i_nonregearn].Pf == 'Y')
                {
                    f_PF = f_PF + N_E_amt;
                }

                if (NonRegEarningsList[i_nonregearn].Esi == 'Y')
                {
                    f_ESI = f_ESI + N_E_amt;
                }

                //s_query = "insert into PayOutput_Earnings values(" + pay.CompanyId + "," + pay.EmployeeId + "," + NonRegEarningsList[i_nonregearn].EarningsId + ",'01/01/2008','01/01/2008','E'," + E_actamt + "," + E_amt + ")";

                pay.EarningsId = NonRegEarningsList[i_nonregearn].EarningsId;

                Net_Act_Earn = Net_Act_Earn + N_E_actamt;
                Net_Earn = Net_Earn + N_E_amt;

            }
        }
    }

    public void Reg_deduction()
    {

        RegDeductionList = pay.fn_In_Deduction(pay);

        if (RegDeductionList.Count > 0)
        {

            for (i_regded = 0; i_regded < RegDeductionList.Count; i_regded++)
            {

                switch (RegDeductionList[i_regded].DeducationCode)
                {

                    case "PF":
                        PF_Calculation();
                        break;

                    case "VPF":
                        VPF_Calculation();
                        break;

                    case "ESI":
                        ESI_Calculation();
                        break;

                    case "PT":
                        PT_Calculation();
                        break;

                    case "IT":
                        IT_Calculation();
                        break;

                    case "LWF": //LWF_Settings = pay.fn_In_LWF_Settings(pay);
                    //if (LWF_Settings.Count > 0)
                    //{
                    //    LWF_Calculation();
                    //}
                    //break;


                    default:
                        Normal_Calculation();
                        break;
                }
            }
        }

    }

    public string date_change(string str_date)
    {
        str_date = "01" + str_date.Substring(2, 8);
        return str_date;
    }

    public DateTime date_split(string s_date)
    {
        string _d, _m, _y;
        DateTime dt;
        if (s_date != "")
        {
            string[] da = s_date.Split('/');
            _d = da[0];
            _m = da[1];
            _y = da[2];
            s_date = _m + "/" + _d + "/" + _y;
            dt = Convert.ToDateTime(s_date);
        }
        else
        {
            dt = DateTime.Now;
        }
        return dt;
    }

    public void PF_Calculation()
    {
        if (RegDeductionList[i_regded].eligible == 'Y')
        {
            PFList = pay.fn_In_PF(pay);
            if (PFList.Count > 0)
            {
                if (f_PF <= PFList[0].Amount)
                {
                    cal_amt = f_PF;
                }
                else
                {
                    cal_amt = PFList[0].Amount;
                }

                PFAmt = cal_amt * (PFList[0].Emp_Con_PF / 100);
                EPFAmt = cal_amt * (PFList[0].Emp_Con_EPF / 100);
                FPFAmt = PFAmt - EPFAmt;

                EmployeesList = employee.fn_get_Emp_first(employee);
                if (EmployeesList[0].PFno == "")
                {
                    pay.PF_No = "0";
                }
                else
                {
                    pay.PF_No = EmployeesList[0].PFno;
                }
                PF_Calc();
                if (ddl_Employee.SelectedItem.Text == "All")
                {
                    pay.pay_mode = "A";
                }
                else
                {
                    pay.pay_mode = "I";
                }
                pay.DeductionId = RegDeductionList[i_regded].DeductionId;
                pay.DeducationCode = RegDeductionList[i_regded].DeducationCode;
                pay.Act_Amount = f_PF;
                pay.Ded_Amount = epf + fpf;
                pay.status = 'S';

                pay.PayOutput_Deductions(pay);
                pay.Deduction_update_register(pay);
                Net_Act_Ded = Net_Act_Ded + f_PF;
                Net_Ded = Net_Ded + tot_pf;
            }
        }
    }

    public void VPF_Calculation()
    {
        con.Open();
        double volpf = VPF();
        con.Close();
        pay.DeductionId = RegDeductionList[i_regded].DeductionId;
        pay.DeducationCode = RegDeductionList[i_regded].DeducationCode;
        pay.Act_Amount = volpf;
        pay.Ded_Amount = volpf;
        pay.status = 'S';
        if (ddl_Employee.SelectedItem.Text == "All")
        {
            pay.pay_mode = "A";
        }
        else
        {
            pay.pay_mode = "I";
        }
        pay.PayOutput_Deductions(pay);
        pay.Deduction_update_register(pay);
    }

    public void PF_Calc()
    {
        SqlDataReader rd1, rd2;
        con.Open();
        SqlCommand cmd_pf = new SqlCommand();
        cmd_pf = new SqlCommand("select * from paym_pf where pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + employee.BranchId + "'", con);
        rd2 = cmd_pf.ExecuteReader();
        if (rd2.Read())
        {
            txt_pf = Convert.ToDecimal(rd2["Emp_Con_PF"]);
            txt_EPF = Convert.ToDecimal(rd2["Emp_Con_EPF"]);
            txt_ceiling = Convert.ToInt32(rd2["max_amount"]);
            chk_ceiling = Convert.ToChar(rd2["check_ceiling"]);
            chk_allwance = Convert.ToChar(rd2["check_allowance"]);
            c_rd = Convert.ToInt32(rd2["c_Round"]);
        }
        SqlCommand cmd = new SqlCommand();
        cmd = new SqlCommand("select Earn_basic,pn_EmployeeID from payinput where pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + employee.BranchId + "' and pn_EmployeeID = '" + pay.EmployeeId + "' and d_date='" + pay.strDate + "' ", con);
        rd1 = cmd.ExecuteReader();
        if (rd1.Read())
        {
            emp_cod = rd1[1].ToString();
            bas_sal = Convert.ToInt32(rd1[0]);
            gross = bas_sal + Net_Earn;
            upper_limit = Convert.ToInt32(txt_ceiling);
            t_pf = Convert.ToDouble(txt_pf);
            t_epf = Convert.ToDouble(txt_EPF);
            fpf = 0;
            if (upper_limit > bas_sal)
            {
                if (chk_allwance == 'Y')
                {
                    bas_sal = gross;
                    if (gross < upper_limit)
                    {
                        PF_Sub_Calculation1();
                    }
                    else
                    {
                        PF_Sub_Calculation2();
                    }
                }
                else
                {
                    PF_Sub_Calculation1();
                }
                //cmd_ins.ExecuteNonQuery();

            }

            else if (upper_limit < bas_sal)
            {
                PF_Sub_Calculation2();
            }

        }
        con.Close();
    }

    public void PF_Sub_Calculation1()
    {
        pf = (((bas_sal) * (t_pf)) / 100);
        pf = pf + PFAmt;
        epf = (((bas_sal) * (t_epf)) / 100);
        epf = epf + EPFAmt;
        fpf = pf - epf;
        //fpf = fpf + FPFAmt;
        //SqlCommand cmd_ins = new SqlCommand("insert into pf_employee(pn_companyid,pn_branchid,emp_code,emp_name,month,year,period,Epf,Fpf) values('" + employee.CompanyId + "','" + employee.BranchId + "','" + emp_cod + "','" + emp_name + "','" + ddl_month.SelectedItem.Text + "','" + ddl_year.SelectedItem.Text + "','" + ddl_periodcode.SelectedItem.Text + "','" + epf + "','" + fpf + "') ", con);
        //cmd_ins.ExecuteNonQuery();

        double vpf = 0;
        vpf = VPF();
        tot_pf = epf + fpf;

        if (c_rd == 0)
        {
            total_pf = function_rd_nearest(tot_pf);
        }
        else
        {
            total_pf = function_rd_next(tot_pf);
        }

        pay.Net_Amount = round_up(f_PF);
        pay.Emp_Con_PF = total_pf;
        pay.Pf_Amount = epf + fpf;
        pay.Emp_Con_EPF = epf;
        pay.Emp_Con_FPF = fpf;
        pay.Emp_Con_VPF = vpf;
        pay.Paid_Days = A_paiddays;
        pay.Absent_Days = A_Absentdays;
        pay.WeekOffDays = A_WeekOffdays;
        pay.periodCode = ddl_periodcode.SelectedItem.Text;

        //SqlCommand cmd_ins = new SqlCommand();

        pay.PayOutput_PF(pay);

    }

    public void PF_Sub_Calculation2()
    {
        if (chk_ceiling == 'N')
        {

            pf = (((bas_sal) * (t_pf)) / 100);
            pf = pf + PFAmt;
            epf = (((upper_limit) * (t_epf)) / 100);
            //epf = epf + EPFAmt;
            fpf = pf - epf;
            //fpf = fpf + FPFAmt;

            double vpf = 0;
            vpf = VPF();
            tot_pf = epf + fpf;

            if (c_rd == 0)
            {
                total_pf = function_rd_nearest(tot_pf);
            }
            else
            {
                total_pf = function_rd_next(tot_pf);
            }

            SqlCommand cmd_ins = new SqlCommand();

            pay.Net_Amount = round_up(f_PF);
            pay.Emp_Con_PF = total_pf;
            pay.Pf_Amount = epf + fpf;
            pay.Emp_Con_EPF = epf;
            pay.Emp_Con_FPF = fpf;
            pay.Emp_Con_VPF = vpf;
            pay.Paid_Days = A_paiddays;
            pay.Absent_Days = A_Absentdays;
            pay.WeekOffDays = A_WeekOffdays;
            pay.periodCode = ddl_periodcode.SelectedItem.Text;

            pay.PayOutput_PF(pay);

        }
        else if (chk_ceiling == 'Y')
        {

            pf = (((upper_limit) * (t_pf)) / 100);
            //pf = pf + PFAmt;
            epf = (((upper_limit) * (t_epf)) / 100);
            //epf = epf + EPFAmt;
            fpf = pf - epf;
            //fpf = fpf + FPFAmt;

            double vpf = 0;
            vpf = VPF();
            tot_pf = epf + fpf;

            if (c_rd == 0)
            {
                total_pf = function_rd_nearest(tot_pf);
            }
            else
            {
                total_pf = function_rd_next(tot_pf);
            }

            pay.Net_Amount = round_up(f_PF);
            pay.Emp_Con_PF = total_pf;
            pay.Pf_Amount = epf + fpf;
            pay.Emp_Con_EPF = epf;
            pay.Emp_Con_FPF = fpf;
            pay.Emp_Con_VPF = vpf;
            pay.Paid_Days = A_paiddays;
            pay.Absent_Days = A_Absentdays;
            pay.WeekOffDays = A_WeekOffdays;
            pay.periodCode = ddl_periodcode.SelectedItem.Text;

            pay.PayOutput_PF(pay);


        }
    }

    public double VPF()
    {
        string contribution_type = "";
        double monthly_contribution = 0;
        string type = "";
        SqlDataReader rd_se;
        SqlCommand cmd_se = new SqlCommand();
        cmd_se = new SqlCommand("select monthlycontribution,contribution_type,salaryfrom from paym_vpf where pn_EmployeeID='" + emp_cod + "' and pn_companyid='" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "' ", con);
        rd_se = cmd_se.ExecuteReader();
        if (rd_se.Read())
        {
            monthly_contribution = Convert.ToDouble(rd_se[0]);
            contribution_type = rd_se[1].ToString();
            type = rd_se[2].ToString();
        }
        SqlDataReader rd_esal;
        SqlCommand cmd_esal = new SqlCommand();
        double earn_bas = 0;
        double vpf_sub = 0;

        if (type == "Basic Salary")
        {
            earn_bas = bas_sal;
        }
        else
        {
            earn_bas = gross;
        }

        if (contribution_type == "Percentage")
        {
            vpf_sub = (earn_bas * monthly_contribution) / 100;
        }

        else
        {
            vpf_sub = monthly_contribution;
        }

        return vpf_sub;
    }


    public void ESI_Calculation()
    {
        double bs = 0.0;
        if (payinput.Count > 0)
        {
            pay.Act_Basic = payinput[0].Act_Basic;
            pay.Earned_Basic = (pay.Act_Basic / total_wdays) * A_paiddays;
            bs = (pay.Act_Basic / total_wdays) * A_paiddays;
        }
        if (RegDeductionList[i_regded].eligible == 'Y')
        {
            ESIList = pay.fn_In_ESI(pay);

            if (ESIList.Count > 0)
            {
                //f_ESI = f_ESI + OT_Amt;
                if (bs <= ESIList[0].Amount)
                {
                    Emp_ESIAmt = f_ESI * (ESIList[0].Emp_Con / 100);
                    Er_ESIAmt = f_ESI * (ESIList[0].Employer_Con / 100);
                    Emp_BasAmt = bs * (ESIList[0].Emp_Con / 100);
                    Er_BasAmt = bs * (ESIList[0].Employer_Con / 100);
                    Emp_ESIAmt = Emp_ESIAmt + Emp_BasAmt;
                    Er_ESIAmt = Er_ESIAmt + Er_BasAmt;

                    EmployeesList = employee.fn_get_Emp_first(employee);
                    if (EmployeesList[0].ESIno == "")
                    {
                        pay.ESI_No = "0";
                    }
                    else
                    {
                        pay.ESI_No = EmployeesList[0].ESIno;
                    }

                    pay.Net_Amount = round_up(f_ESI);
                    pay.Emp_Con = Emp_ESIAmt;
                    pay.Employer_Con = Er_ESIAmt;
                    pay.Paid_Days = A_paiddays;
                    pay.Absent_Days = A_Absentdays;
                    pay.WeekOffDays = A_WeekOffdays;

                    pay.PayOutput_ESI(pay);

                    if (ddl_Employee.SelectedItem.Text == "All")
                    {
                        pay.pay_mode = "A";
                    }
                    else
                    {
                        pay.pay_mode = "I";
                    }
                    pay.DeductionId = RegDeductionList[i_regded].DeductionId;
                    pay.DeducationCode = RegDeductionList[i_regded].DeducationCode;
                    pay.Act_Amount = f_ESI;
                    pay.Ded_Amount = Emp_ESIAmt;
                    pay.status = 'S';
                    pay.PayOutput_Deductions(pay);
                    pay.Deduction_update_register(pay);
                    Net_Act_Ded = Net_Act_Ded + f_ESI;
                    Net_Ded = Net_Ded + Emp_ESIAmt;

                }
            }
        }
    }

    public void PT_Calculation()
    {

        if (RegDeductionList[i_regded].eligible == 'Y')
        {
            pay.F_Amount = f_pt;

            if (PT_Settings[0].PTmonth == "12")
            {
                PT_Sub_Calculation3();
            }
            else
            {
                if (PT_Month_Calculation() == true)
                {
                    PT_Sub_Calculation1();
                    bool_Month = false;
                }
            }
        }

    }

    public void IT_Calculation()
    {
        //RegEarningsList = pay.fn_In_Earnings(pay);
        //{
        //    for (i_regded = 0; i_regded < RegEarningsList.Count; i_regded++)
        //    {
        //        if (RegEarningsList[i_regded].OT == 'Y')
        //        {
        //            f_ot = f_ot + E_actamt;
        //        }
        //        if (RegEarningsList[i_regded].LOP == 'Y')
        //        {
        //            E_actamt = ((A_calcdays / A_paiddays) * 100);
        //        }
        //        else
        //            E_actamt = E_actamt + E_amt;

        //    }
        //}

    }

    public void LWF_Calculation()
    {

        str_Month = Convert.ToString(pay.d_ToDate).Substring(3, 2);

        if (Convert.ToInt32(str_Month) == LWF_Settings[0].LWF_Month)
        {

            i_length = Convert.ToInt32(str_Month) - 12;

            FromMonth_Calculation();

            Query = "select sum(Earn_Act_Amount) from PayOutput_Netpay where";
            Query = Query + " pn_EmployeeID=" + pay.EmployeeId + " and d_date between '" + str_temp + "' and '" + pay.d_date + "'";

            ds_PT = pay.fn_Output(Query);

            if (ds_PT.Tables[0].Rows.Count > 0)
            {
                i_temp = Convert.ToInt32(ds_PT.Tables[0].Rows[0][0]);

                if (LWF_Settings[0].LWF_Limit <= i_temp)
                {
                    LWF_Sub_Calculation();
                }

            }
        }
    }

    //*************************************************************************************************

    public void PT_Sub_Calculation1()
    {

        str_Month = Convert.ToString(pay.d_ToDate).Substring(3, 2);
        str_Year = Convert.ToString(pay.d_ToDate).Substring(6, 4);

        //12 is months ,2 is length of single month in database(01,02...)

        i_length = 12 / (PT_Settings[0].PTmonth.Length / 2);

        i_temp = i_length;

        i_length = Convert.ToInt32(str_Month) - i_length;


        FromMonth_Calculation();

        PT_Sub_Calculation3();

    }

    public void PT_Sub_Calculation2()
    {

        if (PT_Settings[0].PTtype == 'a')
        {

            Query = "select sum(Act_Amount) from PayOutput_Earnings poe,paym_Earnings pe where";
            Query = Query + " poe.pn_EmployeeID=" + pay.EmployeeId + " and poe.d_date between '" + str_temp + "' and '" + pay.d_date + "'";
            Query = Query + " and pe.c_Pt='Y' and poe.pn_EarningsID=pe.pn_EarningsID";
        }
        else
        {
            Query = "select sum(Amount) from PayOutput_Earnings poe,paym_Earnings pe where";
            Query = Query + " poe.pn_EmployeeID=" + pay.EmployeeId + " and poe.d_date between '" + str_temp + "' and '" + pay.d_date + "'";
            Query = Query + " and pe.c_Pt='Y' and poe.pn_EarningsID=pe.pn_EarningsID";
        }

        ds_PT = pay.fn_Output(Query);

        if (ds_PT.Tables[0].Rows.Count > 0)
        {
            int i = Convert.IsDBNull(ds_PT.Tables[0].Rows[0][0]) ? 0 : Convert.ToInt32(ds_PT.Tables[0].Rows[0][0]);
            if (i == 0)
            {
                pay.F_Amount = (Convert.ToInt32("0") / i_temp) * i_temp;
            }
            else
            {
                pay.F_Amount = (Convert.ToInt32(ds_PT.Tables[0].Rows[0][0]) / i_temp) * i_temp;
            }
            PT_Sub_Calculation3();
        }
    }

    public void PT_Sub_Calculation3()
    {
        PTList = pay.fn_In_PT(pay);

        if (PTList.Count > 0)
        {
            //s_query = "insert into PayOutput_Deductions values(" + pay.CompanyId + "," + pay.EmployeeId + "," + RegDeductionList[i_regded].DeductionId + ",'01/01/2008','01/01/2008','D'," + f_pt + "," + PTList[0].T_Amount + ")";

            pay.DeductionId = RegDeductionList[i_regded].DeductionId;
            pay.DeducationCode = RegDeductionList[i_regded].DeducationCode;

            pay.Act_Amount = f_pt;
            pay.Ded_Amount = PTList[0].T_Amount;
            pay.status = 'S';
            if (ddl_Employee.SelectedItem.Text == "All")
            {
                pay.pay_mode = "A";
            }
            else
            {
                pay.pay_mode = "I";
            }
            pay.PayOutput_Deductions(pay);
            pay.Deduction_update_register(pay);
            Net_Act_Ded = Net_Act_Ded + f_pt;
            Net_Ded = Net_Ded + PTList[0].T_Amount;
        }

    }

    public bool PT_Month_Calculation()
    {
        str_Month = Convert.ToString(pay.d_ToDate).Substring(3, 2);

        i_length = PT_Settings[0].PTmonth.Length;
        str_temp = PT_Settings[0].PTmonth;

        for (i_temp = 0; i_temp <= i_length - 1; i_temp = i_temp + 2)
        {

            if (str_Month == str_temp.Substring(i_temp, 2))
            {
                bool_Month = true;
            }
        }

        return bool_Month;

    }

    public void FromMonth_Calculation()
    {

        if (i_length >= 0)
        {
            i_length = i_length + 1;

            if (i_length <= 9)
            {
                str_temp = "01/0" + i_length + "/" + str_Year;
            }
            else
            {
                str_temp = "01/" + i_length + "/" + str_Year;

            }

        }
        else
        {
            str_temp = Convert.ToString(i_length);

            str_temp = str_temp.Substring(1, str_temp.Length - 1);

            i_length = Convert.ToInt32(str_temp);

            i_temp = Convert.ToInt32(str_Year) - 1;

            if (i_length <= 9)
            {
                str_temp = "01/0" + i_length + "/" + i_temp;
            }
            else
            {
                str_temp = "01/" + i_length + "/" + i_temp;
            }
        }
    }

    public void LWF_Sub_Calculation()
    {
        //s_query = "insert into PayOutput_Deductions values(" + pay.CompanyId + "," + pay.EmployeeId + "," + RegDeductionList[i_regded].DeductionId + ",'01/01/2008','01/01/2008','D'," + i_temp + "," + LWF_Settings[0].LWF_Amt + ")";

        pay.DeductionId = RegDeductionList[i_regded].DeductionId;
        pay.Act_Amount = i_temp;
        pay.Ded_Amount = LWF_Settings[0].LWF_Amt;
        pay.status = 'S';

        pay.PayOutput_Deductions(pay);

        Net_Act_Ded = Net_Act_Ded + LWF_Settings[0].LWF_Amt;
        Net_Ded = Net_Ded + LWF_Settings[0].LWF_Amt;
    }

    public void Loan_Sub_Calulation()
    {
        pay.DeductionId = LoanEntryList[i_loan].loanid;
        pay.Act_Amount = Loan_Actamt;
        pay.status = 'L';

        if (Balance_Amt <= Loan_amt)
        {
            //s_query = "insert into PayOutput_Deductions values(" + pay.CompanyId + "," + pay.EmployeeId + "," + LoanEntryList[i_loan].loanid + ",'01/01/2008','01/01/2008','L'," + Loan_Actamt + "," + Balance_Amt + ")";

            pay.Ded_Amount = Balance_Amt;

            pay.PayOutput_Deductions(pay);

            //s_query = "insert into PayOutput_Loan values(" + pay.CompanyId + "," + pay.EmployeeId + "," + LoanEntryList[i_loan].loanid + ",'01/01/2008','01/01/2008','" + pay.d_date + "'," + Balance_Amt + ")";

            pay.float_Amount = Balance_Amt;

            pay.PayOutput_Loan(pay);

            s_query = "update LoanEntry set c_status='N' where pn_companyid=" + pay.CompanyId + " and pn_EmployeeID=" + pay.EmployeeId + " and fn_LoanID=" + LoanEntryList[i_loan].loanid + " and d_effdate='" + LoanEntryList[i_loan].d_EffDate + "'";

            pay.fn_In_Out(s_query);

            Net_Act_Ded = Net_Act_Ded + Loan_Actamt;
            Net_Ded = Net_Ded + Balance_Amt;

        }
        else
        {

            //s_query = "insert into PayOutput_Deductions values(" + pay.CompanyId + "," + pay.EmployeeId + ",'01/01/2008','01/01/2008'," + LoanEntryList[i_loan].loanid + ",'L'," + Loan_Actamt + "," + Loan_amt + ")";

            pay.Ded_Amount = Loan_amt;

            pay.PayOutput_Deductions(pay);

            // s_query = "insert into PayOutput_Loan values(" + pay.CompanyId + "," + pay.EmployeeId + "," + LoanEntryList[i_loan].loanid + ",'01/01/2008','01/01/2008','" + pay.d_date + "'," + Loan_amt + ")";

            pay.float_Amount = Loan_amt;

            pay.PayOutput_Loan(pay);

            Balance_Amt = Balance_Amt - Loan_amt;

            s_query = "update LoanEntry set Balance_Amt=" + Balance_Amt + " where pn_companyid=" + pay.CompanyId + " and pn_EmployeeID=" + pay.EmployeeId + " and fn_LoanID=" + LoanEntryList[i_loan].loanid + " and d_effdate='" + LoanEntryList[i_loan].d_EffDate + "'";

            pay.fn_In_Out(s_query);


            Net_Act_Ded = Net_Act_Ded + Loan_Actamt;
            Net_Ded = Net_Ded + Loan_amt;

        }
    }

    public void Normal_Calculation()
    {
        D_actamt = RegDeductionList[i_regded].Amount;

        D_amt = D_actamt;

        //s_query = "insert into PayOutput_Deductions values(" + pay.CompanyId + "," + pay.EmployeeId + "," + RegDeductionList[i_regded].DeductionId + ",'01/01/2008','01/01/2008','D'," + D_actamt + "," + D_amt + ")";

        pay.DeductionId = RegDeductionList[i_regded].DeductionId;
        pay.Act_Amount = D_actamt;
        pay.Ded_Amount = D_amt;
        pay.status = 'S';
        if (ddl_Employee.SelectedItem.Text == "All")
        {
            pay.pay_mode = "A";
        }
        else
        {
            pay.pay_mode = "I";
        }
        pay.PayOutput_Deductions(pay);

        Net_Act_Ded = Net_Act_Ded + D_actamt;
        Net_Ded = Net_Ded + D_amt;

    }
    public int round_up(double d_amt)
    {
        i_amt = Convert.ToInt32(d_amt);

        return i_amt;

    }

    public double function_rd_nearest(double rup)
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

    public double function_rd_next(double rup)
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
}
