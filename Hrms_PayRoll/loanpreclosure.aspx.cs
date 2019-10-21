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
using System.Data.SqlClient;
using ePayHrms.Company;
using ePayHrms.Employee;


public partial class Bank_Loan_Default : System.Web.UI.Page
{
    Company company = new Company();
    Employee employee = new Employee();
    PayRoll pay = new PayRoll(); 
    Collection<PayRoll> loanlist;
    Collection<Employee> emplist;
    Collection<Company> CompanyList, ddlBranchsList;
    DataSet ds_userrights;

    
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connectionstring"]);

    string _Value, str_Query = "", s_form;
    string s_login_role;   

    protected void Page_Load(object sender, EventArgs e)
    {
        
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        pay.CompanyId          = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);

        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        pay.BranchId      = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        s_login_role   = Request.Cookies["Login_temp_Role"].Value;


        if (!IsPostBack)
        {
            Txtappdate.Text = "";
            txtamount.Text = "";
            txtpaidamount.Text = "";
            txtbalamount.Text = "";
            txtcloseamount.Text = "";
            txtchkno.Value = "";
            txtchkamount.Value = "";
            txtchkbankname.Value = "";
            txtremarks.Text = "";
            txtchkdate.Text = "";
            
            //tab_check.Visible = false;
            loan_details.Visible = false;

           CompanyList = company.fn_getCompany();
           if (CompanyList.Count > 0)
           {
               switch (s_login_role)
               {
                   case "a":
                       //tab_details.Visible = false;
                       //tab_check.Visible = false;
                       //tab_button.Visible = false;
                       ddl_Branch.Visible = true;
                       ddl_Branch_load();
                       break;

                   case "h":
                       //tab_details.Visible = true;
                       //tab_check.Visible = false;
                       //tab_button.Visible = true;
                       ddl_Branch.Visible = false;
                       ddl_load();
                       break;

                   case "u": s_form = "47";
                       ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);
                       if (ds_userrights.Tables[0].Rows.Count > 0)
                       {
                           //tab_details.Visible = true;
                           //tab_check.Visible = false;
                           //tab_button.Visible = true;
                           ddl_Branch.Visible = false;
                           ddl_load();
                       }
                       else
                       {
                           Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator.";
                           Response.Redirect("~/Company_Home.aspx");
                       }

                       break;
               }
           }
        }
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        con.Open();
        if (s_login_role == "a")
        {
            employee.BranchId = (int)ViewState["Loancloser_BranchID"];
            pay.BranchId      = (int)ViewState["Loancloser_BranchID"];
        }
        if (Convert.ToDateTime(Txtappdate.Text) > DateTime.Now)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Application date should not exceed the current date');", true);
            return;
        }
        save();
        _Value = pay.updateclosere(pay);
        if (_Value == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Added sucessfully');", true);      
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
        
        decimal val = 0.00m;
        
        DateTime paid_date = pay.Convert_ToSqlDate(Txtappdate.Text);
        if(txt_loan_process.Text == "By Flat Rate")
        {
            SqlCommand cmd_flat_del = new SqlCommand("delete from payoutput_loan where loan_appid='" + ddl_loan.SelectedItem.Text + "' and loan_status='pending' and pn_companyid='" + pay.CompanyId + "' and  pn_branchid='" + pay.BranchId + "' and pn_employeeid='" + ddl_employee.SelectedItem.Value + "'   ", con);
            cmd_flat_del.ExecuteNonQuery();
           
            SqlCommand cmd_upd_flat = new SqlCommand("insert into payoutput_loan(pn_companyid,pn_branchid,pn_employeeid,d_date,amount,loan_appid,instal_amt,balance_amt,loan_status) values('" + pay.CompanyId + "','" + pay.BranchId + "','" + ddl_employee.SelectedItem.Value + "','" + paid_date + "','" + Convert.ToDecimal(txtamount.Text) + "','" + ddl_loan.SelectedItem.Text + "'," + Convert.ToDecimal(txtcloseamount.Text) + ",'" + val + "','paid') ", con);

            cmd_upd_flat.ExecuteNonQuery();

            SqlCommand cmd_upd_flat_lentry = new SqlCommand("update loanentry set balance_amt ='0.00',loan_status='loan closed' where loan_appid ='" + ddl_loan.SelectedItem.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' and pn_employeeid='" + ddl_employee.SelectedItem.Value + "' ", con);
            cmd_upd_flat_lentry.ExecuteNonQuery();
        }
        else if (txt_loan_process.Text == "By Diminishing Rate")
        {
            SqlCommand cmd_dim_del = new SqlCommand("delete from paym_loan_diminishing where loan_appid='" + ddl_loan.SelectedItem.Text + "' and loan_status='pending' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' and pn_employeeid='" + ddl_employee.SelectedItem.Value + "' ", con);
            cmd_dim_del.ExecuteNonQuery();

            SqlCommand cmd_upd_dim = new SqlCommand("insert into paym_loan_diminishing(pn_companyid,pn_employeeid,eff_date,loan_amount,pn_branchid,loan_appid,instal_amt,balance_amt,loan_status) values('" + pay.CompanyId + "','" + ddl_employee.SelectedItem.Value + "','" + paid_date + "','" + Convert.ToDecimal(txtamount.Text) + "','" + pay.BranchId + "','" + ddl_loan.SelectedItem.Text + "','" + Convert.ToDecimal(txtcloseamount.Text) + "','" + val + "','paid') ", con);
            cmd_upd_dim.ExecuteNonQuery();

            SqlCommand cmd_upd_dim_lentry = new SqlCommand("update loanentry set balance_amt ='0.00',loan_status='loan closed' where loan_appid ='" + ddl_loan.SelectedItem.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' and pn_employeeid='" + ddl_employee.SelectedItem.Value + "' ", con);
            cmd_upd_dim_lentry.ExecuteNonQuery();
        }

        grid_load();
        con.Close();
    }



    protected void loan_details_Click(object sender, EventArgs e)
    {
        if (s_login_role == "a")
        {
            employee.BranchId  = (int)ViewState["Loancloser_BranchID"];
            pay.BranchId       = (int)ViewState["Loancloser_BranchID"];
        }       
            grid_load();             
            //tab_check.Visible  = false;
    }

    public void save()
    {
        try
        {
            pay.loan_mas_id = Convert.ToString(ddl_loan.SelectedItem.Value);
            pay.EmployeeId = Convert.ToInt32(ddl_employee.SelectedItem.Value);
            pay.applicationdate = pay.Convert_ToSqlDate(Txtappdate.Text);
            pay.loanprocess = Convert.ToString(txt_loan_process.Text);
            pay.loaninterest_amt = Convert.ToDecimal(txt_int_amt.Text);
            pay.pay_mode = Convert.ToString(rdo_check.SelectedItem.Text);
            pay.loanname = Convert.ToString(txt_lname.Text);
            pay.loan_amt = Convert.ToDecimal(txtamount.Text);
            pay.paidamount = Convert.ToDouble(txtpaidamount.Text);
            pay.balanceamount = Convert.ToDouble(txtbalamount.Text);
            pay.closureamount = Convert.ToDouble(txtcloseamount.Text);
            pay.checkno = txtchkno.Value;
            pay.checkdate = pay.Convert_ToSqlDate(txtchkdate.Text);
            pay.checkamount = txtchkamount.Value;
            pay.bankname = txtchkbankname.Value;
            pay.Remarks = txtremarks.Text;
            pay.status = 'Y';
        }

        catch (Exception ex)
        {

        }
    }
    

    protected void edit(object sender, GridViewEditEventArgs e)
    {

    }


    public void ddl_load()
    {
        if (s_login_role == "a")
        {
            employee.BranchId = (int)ViewState["Loancloser_BranchID"];
            pay.BranchId      = (int)ViewState["Loancloser_BranchID"];
        }

        emplist = employee.fn_getEmployeeList(employee);
        if (emplist.Count > 0)
        {
            ddl_employee.Enabled = true;

            for (int ddl_i = -1; ddl_i < emplist.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem list = new ListItem();
                    list.Text = "Select Employee";
                    list.Value = "0";
                    ddl_employee.Items.Add(list);
                }
                else
                {
                    ListItem list = new ListItem();
                    list.Value = emplist[ddl_i].EmployeeId.ToString();
                    list.Text = emplist[ddl_i].FullName.ToString();
                    ddl_employee.Items.Add(list);
                }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Employees Available');", true);
            ddl_employee.Enabled = false;
            ddl_employee.Items.Clear();
        }
    }
    
    protected void rdo_check_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdo_check.SelectedItem.Value == "Cheque Pre Closure" )
        {
            //tab_check.Visible = true;
        }
        
        else if (rdo_check.SelectedItem.Value == "Cheque Force Closure")
        {
            //tab_check.Visible = true;
        }
        else
        {
            //tab_check.Visible = false;
            txtchkno.Value = "0";
            txtchkdate.Text = "01/01/2001";
            txtchkamount.Value = "0";
            txtchkbankname.Value = "0";
        }
    }

    public void grid_load()
    {
        str_Query = "select a.employee_first_name,b.d_effdate,b.balance_amt,c. * from paym_employee a,loanentry b,loan_precloser c where a.pn_employeeid='" + ddl_employee.SelectedItem.Value + "' and b.loan_appid='" + ddl_loan.SelectedItem.Value + "' and c.loan_appid='" + ddl_loan.SelectedItem.Value + "' ";
        loanlist = pay.fn_loanPrecloser1(str_Query);
        if (loanlist.Count > 0)
        {
            grid_closere.DataSource = loanlist;
            grid_closere.DataBind();
        }
        else
        {
            DataSet ds = new DataSet();
            grid_closere.DataSource = ds;
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            grid_closere.DataSource = ds;
            grid_closere.DataBind();
            int columncount = grid_closere.Rows[0].Cells.Count;
            grid_closere.Rows[0].Cells.Clear();
            grid_closere.Rows[0].Cells.Add(new TableCell());
            grid_closere.Rows[0].Cells[0].ColumnSpan = columncount;
            grid_closere.Rows[0].Cells[0].Text = "No Records Found";
        }

        btn_save.Visible = true;
    }

    public void ddl_Branch_load()
    {
        int ddl_i;
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
            ViewState["Loancloser_BranchID"] = Convert.ToInt32(ddl_Branch.SelectedValue);
            //tab_details.Visible = true;
            //tab_check.Visible   = false;
            //tab_button.Visible  = true;
            ddl_load();
        }
        else
        {
            //tab_details.Visible = false;
            //tab_check.Visible = false;
            //tab_button.Visible = false;
        }
    }
    protected void ddl_employee_SelectedIndexChanged(object sender, EventArgs e)
    {
        con.Open();
        
        SqlDataAdapter ad_sel_loan= new SqlDataAdapter("select loan_appid from loanentry where pn_employeeid='" + ddl_employee.SelectedItem.Value + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' and loan_status!='loan closed'  ", con);
        DataSet ds = new DataSet();
        ad_sel_loan.Fill(ds);
        ddl_loan.DataSource = ds;
       
        ddl_loan.DataTextField  = "loan_appid";
        ddl_loan.DataValueField = "loan_appid";
        ddl_loan.DataBind();
        ddl_loan.Items.Insert(0, "select");
        con.Close();
    }
    protected void ddl_loan_SelectedIndexChanged(object sender, EventArgs e)
    {
        con.Open();

        SqlCommand cmd_sel_loan = new SqlCommand("select * from loanentry where loan_appid='" + ddl_loan.SelectedItem.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' ", con);
        SqlDataReader rd_sel_loan = cmd_sel_loan.ExecuteReader();
        string l_name = "";
        double l_amt = 0;
        double p_amt = 0;
        double b_amt = 0;
        double c_amt = 0;
        double int_amt = 0;
        string l_process = "";

        if (rd_sel_loan.Read())
        {
            l_name = rd_sel_loan["loan_name"].ToString();
            l_amt = Convert.ToDouble(rd_sel_loan["Loan_Amt"]);
            b_amt = Convert.ToDouble(rd_sel_loan["balance_amt"]);
            c_amt = Convert.ToDouble(rd_sel_loan["balance_amt"]);
            l_process = Convert.ToString(rd_sel_loan["loan_process"]);
            int_amt = Convert.ToDouble(rd_sel_loan["tot_interest_amt"]);
        }

        if (l_process == "By Flat Rate")
        {
            SqlCommand cmd_flat = new SqlCommand("select instal_amt from payoutput_loan where loan_appid='" + ddl_loan.SelectedItem.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' and loan_status='paid'", con);
            SqlDataReader rd_flat = cmd_flat.ExecuteReader();
            while (rd_flat.Read())
            {
                p_amt += Convert.ToDouble(rd_flat["instal_amt"]);
            }
            rd_flat.Close();
        }
        else if (l_process == "By Diminishing Rate")
        {
            SqlCommand cmd_dim = new SqlCommand("select instal_amt from paym_loan_diminishing where loan_appid='" + ddl_loan.SelectedItem.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' and loan_status='paid'", con);
            SqlDataReader rd_dim = cmd_dim.ExecuteReader();

            while (rd_dim.Read())
            {
                p_amt += Convert.ToDouble(rd_dim["instal_amt"]);
            }
            rd_dim.Close();
        }

        txt_lname.Text = l_name;
        txtamount.Text = l_amt.ToString("#,0.00");
        txtbalamount.Text = b_amt.ToString("#,0.00");
        txtcloseamount.Text = c_amt.ToString("#,0.00");
        txtpaidamount.Text = p_amt.ToString("#,0.00");
        txt_int_amt.Text = int_amt.ToString("#,0.00");

        txt_loan_process.Text = l_process;
        rd_sel_loan.Close();

        SqlCommand cmd_chk = new SqlCommand("select * from loan_precloser where loan_appid='" + ddl_loan.SelectedItem.Value + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' ", con);
        SqlDataReader rd_chk = cmd_chk.ExecuteReader();
        if (rd_chk.Read())
        {
            loan_details.Visible = true;
        }
        con.Close();
    }



    protected void grid_closere_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grid_closere.EditIndex = e.NewEditIndex;        
        grid_load();        
    }
    protected void grid_closere_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        con.Open();
        string loan_id = ((Label)grid_closere.Rows[e.RowIndex].FindControl("lbl_loan_id")).Text;
        string cheque_no = ((TextBox)grid_closere.Rows[e.RowIndex].FindControl("txt_check_no")).Text;
        string cheque_date = ((TextBox)grid_closere.Rows[e.RowIndex].FindControl("txt_check_date")).Text;
        decimal cheque_amt = Convert.ToDecimal(((TextBox)grid_closere.Rows[e.RowIndex].FindControl("txt_check_amt")).Text);
        string bank_name = ((TextBox)grid_closere.Rows[e.RowIndex].FindControl("txt_bank_nam")).Text;
        string pay_mode = ((DropDownList)grid_closere.Rows[e.RowIndex].FindControl("ddl_pay_mode")).SelectedItem.Text;

        SqlCommand cmd_upd_precloser = new SqlCommand("update loan_precloser set n_checkno='" + cheque_no + "',d_checkdate='" + cheque_date + "',n_checkamount='" + cheque_amt + "',v_bankname='" + bank_name + "',payment_mode='" + pay_mode + "' where pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' and loan_appid='" + loan_id + "' ", con);
        cmd_upd_precloser.ExecuteNonQuery();
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Record Updated');", true);
        grid_closere.EditIndex = -1;
        grid_load();
    }
    protected void ddl_closure_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_closure_type.SelectedItem.Text == "Pre Closure")
        {
            txtcloseamount.Enabled = false;            
        }
        else if (ddl_closure_type.SelectedItem.Text == "Force Closure")
        {
            txtcloseamount.Enabled = true;
        }
    }
    protected void grid_closere_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grid_closere.EditIndex = -1;
        grid_load();
    }
}
