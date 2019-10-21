using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;


public partial class Hrms_PayRoll_Loanpost : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connectionstring"]);
    PayRoll pay = new PayRoll();
    string loan_process = "";
        
    
    protected void Page_Load(object sender, EventArgs e)
    {
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        pay.BranchId  = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        if (!IsPostBack)
        {            
            ddl_empid_load();
            ListBox1.Visible = false;
            ddl_mon_topost.Items.Insert(0, "Select");
            ddl_mon_posted.Items.Insert(0, "Select");
        }
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        con.Open();

        SqlDataAdapter ad_list = new SqlDataAdapter("Select loan_reqno from loan_post where pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' ", con);
        DataSet ds = new DataSet();
        ad_list.Fill(ds);
        ListBox1.DataSource = ds;
        ListBox1.DataBind();

        SqlCommand cmd_check = new SqlCommand("select * from loan_post where loan_reqno='" + txt_loan_req.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' ", con);
        SqlDataReader rd_check = cmd_check.ExecuteReader();

        if (rd_check.Read())
        {
            SqlCommand cmd_pro = new SqlCommand("select loan_process from loanentry where loan_appid='" + ddl_loan.SelectedItem.Text + "' and pn_employeeid='" + ddl_emp.SelectedItem.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' ", con);
            SqlDataReader rd_pro = cmd_pro.ExecuteReader();
            if (rd_pro.Read())
            {
                loan_process = Convert.ToString(rd_pro["loan_process"]);
            }
            if (loan_process == "By Flat Rate")
            {
                SqlCommand cmd1 = new SqlCommand("select instal_amt from payoutput_loan where d_date ='" + ddl_mon_topost.SelectedItem.Value + "' and loan_appid='" + ddl_loan.SelectedItem.Text + "' and pn_employeeid='" + ddl_emp.SelectedItem.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' ", con);
                SqlDataReader rd = cmd1.ExecuteReader();
                double ins_amt1 = 0;
                if (rd.Read())
                {
                    ins_amt1 = Convert.ToDouble(rd["instal_amt"]);
                }
                SqlCommand cmd2 = new SqlCommand("select instal_amt from payoutput_loan where d_date='" + ddl_mon_posted.SelectedItem.Value + "' and loan_appid ='" + ddl_loan.SelectedItem.Text + "' and pn_employeeid='" + ddl_emp.SelectedItem.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' ", con);
                SqlDataReader rd1 = cmd2.ExecuteReader();
                double ins_amt2 = 0;
                if (rd1.Read())
                {
                    ins_amt2 = Convert.ToDouble(rd1["instal_amt"]);
                }
                double sum = ins_amt1 + ins_amt2;

                SqlCommand cmd_upd = new SqlCommand("update payoutput_loan set instal_amt ='" + sum + "'  where d_date='" + ddl_mon_posted.SelectedItem.Value + "' and loan_appid='" + ddl_loan.SelectedItem.Text + "' and pn_employeeid='" + ddl_emp.SelectedItem.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "'  ", con);
                cmd_upd.ExecuteNonQuery();

                string posted_month = (ddl_mon_posted.SelectedItem.Value).Remove(10);
                SqlCommand cmd_flat_upd = new SqlCommand("update payoutput_loan set loan_status= '" + posted_month + "' where d_date='" + ddl_mon_topost.SelectedItem.Value + "' and pn_employeeid='" + ddl_emp.SelectedItem.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' ", con);
                cmd_flat_upd.ExecuteNonQuery();

                txt_post_amt.Value = Convert.ToString(ins_amt1);

            }
            else if (loan_process == "By Diminishing Rate")
            {
                SqlCommand cmd1 = new SqlCommand("select instal_amt from paym_loan_diminishing where eff_date ='" + ddl_mon_topost.SelectedItem.Value + "' and loan_appid='" + ddl_loan.SelectedItem.Text + "' and pn_employeeid='" + ddl_emp.SelectedItem.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' ", con);
                SqlDataReader rd = cmd1.ExecuteReader();
                double ins_amt1 = 0;
                if (rd.Read())
                {
                    ins_amt1 = Convert.ToDouble(rd["instal_amt"]);
                }
                SqlCommand cmd2 = new SqlCommand("select instal_amt from paym_loan_diminishing where eff_date='" + ddl_mon_posted.SelectedItem.Value + "' and loan_appid ='" + ddl_loan.SelectedItem.Text + "' and pn_employeeid='" + ddl_emp.SelectedItem.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' ", con);
                SqlDataReader rd1 = cmd2.ExecuteReader();
                double ins_amt2 = 0;
                if (rd1.Read())
                {
                    ins_amt2 = Convert.ToDouble(rd1["instal_amt"]);
                }
                double sum = ins_amt1 + ins_amt2;

                SqlCommand cmd_upd = new SqlCommand("update paym_loan_diminishing set instal_amt ='" + sum + "'  where eff_date='" + ddl_mon_posted.SelectedItem.Value + "' and loan_appid='" + ddl_loan.SelectedItem.Text + "' and pn_employeeid='" + ddl_emp.SelectedItem.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "'  ", con);
                cmd_upd.ExecuteNonQuery();

                string posted_month = (ddl_mon_posted.SelectedItem.Value).Remove(10);
                SqlCommand cmd_dim_upd = new SqlCommand("update paym_loan_diminishing set loan_status= '" + posted_month + "' where eff_date='" + ddl_mon_topost.SelectedItem.Value + "' and pn_employeeid='" + ddl_emp.SelectedItem.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' ", con);
                cmd_dim_upd.ExecuteNonQuery();

                txt_post_amt.Value = Convert.ToString(ins_amt1);

            }
        }

        else
        {
            SqlCommand cmd_insert = new SqlCommand("insert into loan_post(loan_reqno,req_date,employeeid,employeename,loan_appid,loan_type,loan_name,loan_amount,month_to_posted,month_posted_on,rem_month,postedamt,balance_amt,approve_by,pn_companyid,pn_branchid) values('" + txt_loan_req.Text + "','" + txt_req_date.Text + "','" + ddl_emp.SelectedItem.Text + "','" + txt_empnam.Value + "','" + ddl_loan.SelectedItem.Text + "','" + txt_loan_type.Value + "','" + txt_loan_name.Value + "','" + txt_loan_amt.Value + "','" + ddl_mon_topost.SelectedItem.Value + "','" + ddl_mon_posted.SelectedItem.Value + "','" + txt_rem_mon.Value + "','" + txt_post_amt.Value + "','" + txt_bal_amt.Value + "','" + txt_app_by.Value + "','" + pay.CompanyId + "','" + pay.BranchId + "' )", con);
            cmd_insert.ExecuteNonQuery();
            lbl_error.Text = "Record Inserted";
        }
        
        con.Close();
    }

    public void ddl_empid_load()
    {
        con.Open();
        SqlCommand cmd_ddl = new SqlCommand("select pn_employeeid from paym_employee where pn_companyid=" + pay.CompanyId + " and pn_branchid=" + pay.BranchId + " ", con);
        SqlDataAdapter ad_ddl = new SqlDataAdapter(cmd_ddl);
        DataSet ds = new DataSet();
        ad_ddl.Fill(ds, "paym_employee");
        ddl_emp.DataSource = ds;
        ddl_emp.DataTextField = "pn_employeeid";
        ddl_emp.DataValueField = "pn_employeeid";
        ddl_emp.DataBind();              
        con.Close();
        ddl_emp.Items.Insert(0, "Select");
        ddl_loan.Items.Insert(0, "Select"); 
    }

    protected void ddl_emp_SelectedIndexChanged(object sender, EventArgs e)
    {
        con.Open();
        SqlCommand cmd_nam   = new SqlCommand("select employee_first_name from paym_employee where pn_companyid=" + pay.CompanyId + " and pn_branchid=" + pay.BranchId + " and pn_employeeid='" + ddl_emp.SelectedItem.Value + "' ", con);
        SqlDataReader rd_nam = cmd_nam.ExecuteReader();
        string emp_nam = "";
        if (rd_nam.Read())
        {
            emp_nam = rd_nam[0].ToString();
        }
        txt_empnam.Value = emp_nam;
        
        con.Close();
        
        ddl_loan_load();

    }

    public void ddl_loan_load()
    {
        con.Open();
        SqlCommand cmd_loan    = new SqlCommand("select loan_appid from loanentry where pn_companyid= " + pay.CompanyId + " and pn_branchid=" + pay.BranchId + " and pn_employeeid='" + ddl_emp.SelectedItem.Value + "' ", con);
        SqlDataAdapter ad_loan = new SqlDataAdapter(cmd_loan);

        DataSet ds = new DataSet();
        ad_loan.Fill(ds);
        ddl_loan.DataSource = ds;
        ddl_loan.DataTextField = "loan_appid";
        ddl_loan.DataValueField = "loan_appid";
        ddl_loan.DataBind();
        ddl_loan.Items.Insert(0, "Select"); 
               
        con.Close();
    }
    protected void ddl_loan_SelectedIndexChanged(object sender, EventArgs e)
    {
        loan_details();
        mon_post();
    }
    public void mon_post()
    {
        con.Open();

        if (loan_process == "By Flat Rate")
        {
            SqlDataAdapter cmd_ad = new SqlDataAdapter("select datename(month,d_date) + ''+ datename(year,d_date) as d_date1,d_date from payoutput_loan where loan_appid='" + ddl_loan.SelectedItem.Value + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' and loan_status!='paid' and loan_status='pending'  ", con);

            DataSet ds = new DataSet();
            cmd_ad.Fill(ds);
            ddl_mon_topost.DataSource = ds;
            ddl_mon_topost.DataTextField = "d_date1";
            ddl_mon_topost.DataValueField = "d_date";            
            ddl_mon_topost.DataBind();
            ddl_mon_topost.Items.Insert(0, "Select");

            ddl_mon_posted.DataSource = ds;
            ddl_mon_posted.DataTextField = "d_date1";
            ddl_mon_posted.DataValueField = "d_date";
            ddl_mon_posted.DataBind();
            ddl_mon_posted.Items.Insert(0, "Select");
        }

        else if (loan_process == "By Diminishing Rate")
        {
            SqlDataAdapter cmd_ad = new SqlDataAdapter("select datename(month,eff_date) + ''+ datename(year,eff_date) as eff_date1,eff_date from paym_loan_diminishing where loan_appid='" + ddl_loan.SelectedItem.Value + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' and loan_status!='paid' and loan_status='pending' ", con);

            DataSet ds = new DataSet();
            cmd_ad.Fill(ds);

            ddl_mon_topost.DataTextField = "eff_date1";
            ddl_mon_topost.DataValueField = "eff_date";
            ddl_mon_topost.DataSource = ds;
            ddl_mon_topost.DataBind();
            ddl_mon_topost.Items.Insert(0, "Select");

            ddl_mon_posted.DataTextField = "eff_date1";
            ddl_mon_posted.DataValueField = "eff_date";
            ddl_mon_posted.DataSource = ds;
            ddl_mon_posted.DataBind();
            ddl_mon_posted.Items.Insert(0, "Select");
        }     
               
        con.Close();
    }
    public void loan_details()
    {
        con.Open();
        SqlCommand cmd_loan_details = new SqlCommand("select instalmentcount,loan_amt,loan_name,loan_process,balance_amt from loanentry where pn_companyid= " + pay.CompanyId + " and pn_branchid=" + pay.BranchId + " and pn_employeeid='" + ddl_emp.SelectedItem.Value + "' and loan_appid='" + ddl_loan.SelectedItem.Value + "' ", con);
        SqlDataReader rd_loan = cmd_loan_details.ExecuteReader();
        
        string loan_amt = "";
        string loan_name = "";
        
        int total_instal = 0;
        int instal_count = 0;
        double main_bal = 0;

        if (rd_loan.Read())
        {           
            loan_amt     = rd_loan["loan_amt"].ToString();
            loan_name    = rd_loan["loan_name"].ToString();
            loan_process = rd_loan["loan_process"].ToString();
            total_instal = Convert.ToInt32(rd_loan["instalmentcount"]);
            main_bal     = Convert.ToDouble(rd_loan["balance_amt"]);
        }

        if (loan_process == "By Flat Rate")
        {
            SqlCommand cmd_flat_process = new SqlCommand("select installement_count from payoutput_loan where loan_appid='" + ddl_loan.SelectedItem.Value + "' and loan_status='paid' ", con);
            SqlDataReader rd_flat_process = cmd_flat_process.ExecuteReader();            

            if (rd_flat_process.Read())
            {
                if (rd_flat_process["installement_count"].ToString() != "")
                {
                    instal_count = Convert.ToInt32(rd_flat_process["installement_count"]);
                }
            }
            txt_rem_mon.Value = Convert.ToString(total_instal - instal_count);                       
        }

        else if (loan_process == "By Diminishing Rate")
        {
            SqlCommand cmd_dim_process = new SqlCommand("select months from paym_loan_diminishing where loan_appid='" + ddl_loan.SelectedItem.Value + "' and loan_status='paid' ", con);
            SqlDataReader rd_dim_process = cmd_dim_process.ExecuteReader();

            if (rd_dim_process.Read())
            {
                instal_count = Convert.ToInt32(rd_dim_process["installement_count"]);
            }
            txt_rem_mon.Value = Convert.ToString(total_instal - instal_count);            
        }
        
        txt_loan_amt.Value = loan_amt;
        txt_loan_name.Value = loan_name;
        txt_loan_type.Value = loan_process;
        txt_bal_amt.Value =Convert.ToString(main_bal);
        con.Close();
    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        txt_app_by.Value    = "";
        txt_bal_amt.Value   = "";
        txt_empnam.Value    = "";
        
        txt_loan_amt.Value  = "";
        txt_loan_name.Value = "";
        txt_loan_req.Text  = "";
        txt_loan_type.Value = "";
        txt_post_amt.Value  = "";
        txt_rem_mon.Value   = "";
        txt_req_date.Text = "";
    }
    protected void ddl_mon_topost_SelectedIndexChanged(object sender, EventArgs e)
    {
        con.Open();
        double posted_amt = 0;
        if(txt_loan_type.Value=="By Flat Rate")
        {
            SqlCommand cmd_post_flat = new SqlCommand("select instal_amt from payoutput_loan where d_date='" + ddl_mon_topost.SelectedItem.Value + "' and loan_appid='" + ddl_loan.SelectedItem.Text + "' and pn_employeeid='" + ddl_emp.SelectedItem.Text + "' ", con);
            SqlDataReader rd_post_flat = cmd_post_flat.ExecuteReader();
            
            if (rd_post_flat.Read())
            {
                posted_amt = Convert.ToDouble(rd_post_flat["instal_amt"]);
            }
            txt_post_amt.Value =Convert.ToString(posted_amt);
        }
        else if(txt_loan_type.Value=="By Diminishing Rate")
        {
            SqlCommand cmd_post_dim = new SqlCommand("select instal_amt from paym_loan_diminishing where eff_date='" + ddl_mon_topost.SelectedItem.Value + "' and loan_appid='" + ddl_loan.SelectedItem.Text + "' and pn_employeeid='" + ddl_emp.SelectedItem.Text + "' ", con);
            SqlDataReader rd_post_dim = cmd_post_dim.ExecuteReader();

            if (rd_post_dim.Read())
            {
                posted_amt = Convert.ToDouble(rd_post_dim["instal_amt"]);
            }
            txt_post_amt.Value =Convert.ToString(posted_amt);
        }
        
        con.Close();
    }
    protected void btn_undo_Click(object sender, EventArgs e)
    {
        con.Open();
        if(txt_loan_type.Value=="By Flat Rate")
        {
            SqlCommand cmd_mon_tpost = new SqlCommand("select instal_amt from payoutput_loan where d_date='" + ddl_mon_topost.SelectedItem.Value + "' and pn_employeeid='" + ddl_emp.SelectedItem.Text + "' and loan_appid='" + ddl_loan.SelectedItem.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "'", con);
            SqlDataReader rd_topost = cmd_mon_tpost.ExecuteReader();
            double instal_amount = 0;
            if (rd_topost.Read())
            {
                instal_amount = Convert.ToDouble(rd_topost["instal_amt"]);
            }

            SqlCommand cmd_mon_posted = new SqlCommand("select instal_amt from payoutput_loan where d_date='" + ddl_mon_posted.SelectedItem.Value + "' and pn_employeeid='" + ddl_emp.SelectedItem.Text + "' and loan_appid='" + ddl_loan.SelectedItem.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "'", con);
            SqlDataReader rd_posted = cmd_mon_posted.ExecuteReader();
            double instal_amount1 = 0;
            if (rd_posted.Read())
            {
                instal_amount1 = Convert.ToDouble(rd_posted["instal_amt"]);
            }

            double tot = instal_amount1 - instal_amount;           

            SqlCommand cmd_flat_post = new SqlCommand("update payoutput_loan set loan_status='Pending' where d_date='" + ddl_mon_topost.SelectedItem.Value + "' and pn_employeeid='" + ddl_emp.SelectedItem.Text + "' and loan_appid='" + ddl_loan.SelectedItem.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "'", con);
            cmd_flat_post.ExecuteNonQuery();
            SqlCommand cmd_flat_post1 = new SqlCommand("update payoutput_loan set instal_amt='" + tot + "' where d_date='" + ddl_mon_posted.SelectedItem.Value + "' and pn_employeeid='" + ddl_emp.SelectedItem.Text + "' and loan_appid='" + ddl_loan.SelectedItem.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "'", con);
            cmd_flat_post1.ExecuteNonQuery();
        }
        else if(txt_loan_type.Value=="By Diminishing Rate")
        {
            SqlCommand cmd_mon_tpost = new SqlCommand("select instal_amt from paym_loan_diminishing where eff_date='" + ddl_mon_topost.SelectedItem.Value + "' and pn_employeeid='" + ddl_emp.SelectedItem.Text + "' and loan_appid='" + ddl_loan.SelectedItem.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "'", con);
            SqlDataReader rd_topost = cmd_mon_tpost.ExecuteReader();
            double instal_amount = 0;
            if (rd_topost.Read())
            {
                instal_amount = Convert.ToDouble(rd_topost["instal_amt"]);
            }

            SqlCommand cmd_mon_posted = new SqlCommand("select instal_amt from paym_loan_diminishing where eff_date='" + ddl_mon_posted.SelectedItem.Value + "' and pn_employeeid='" + ddl_emp.SelectedItem.Text + "' and loan_appid='" + ddl_loan.SelectedItem.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "'", con);
            SqlDataReader rd_posted = cmd_mon_posted.ExecuteReader();
            double instal_amount1 = 0;
            if (rd_posted.Read())
            {
                instal_amount1 = Convert.ToDouble(rd_posted["instal_amt"]);
            }

            double tot = instal_amount1 - instal_amount;

            SqlCommand cmd_flat_post = new SqlCommand("update paym_loan_diminishing set loan_status='Pending' where eff_date='" + ddl_mon_topost.SelectedItem.Value + "' and pn_employeeid='" + ddl_emp.SelectedItem.Text + "' and loan_appid='" + ddl_loan.SelectedItem.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "'", con);
            cmd_flat_post.ExecuteNonQuery();
            SqlCommand cmd_flat_post1 = new SqlCommand("update paym_loan_diminishing set instal_amt='" + tot + "' where eff_date='" + ddl_mon_posted.SelectedItem.Value + "' and pn_employeeid='" + ddl_emp.SelectedItem.Text + "' and loan_appid='" + ddl_loan.SelectedItem.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "'", con);
            cmd_flat_post1.ExecuteNonQuery();
        }
        con.Close();
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        con.Open();
        ListBox1.Visible = true;
        SqlDataAdapter ad_list = new SqlDataAdapter("Select loan_reqno from loan_post where pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' ", con);
        DataSet ds = new DataSet();
        ad_list.Fill(ds);
        ListBox1.DataSource = ds;
        ListBox1.DataTextField = "loan_reqno";
        ListBox1.DataBind();
        con.Close();
    }
    protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_loan_req.Text = ListBox1.SelectedItem.Text;
        ListBox1.Visible = false;
        con.Open();
        SqlCommand cmd_select = new SqlCommand("select req_date,employeeid,employeename,loan_appid,loan_type,loan_name,loan_amount,rem_month,postedamt,balance_amt,approve_by,datename(month,month_to_posted) + ''+ datename(year,month_to_posted) as topost,datename(month,month_posted_on) + ''+ datename(year,month_posted_on) as postedon,month_to_posted,month_posted_on  from loan_post where loan_reqno='" + txt_loan_req.Text + "' and pn_companyid='" + pay.CompanyId + "' and pn_branchid='" + pay.BranchId + "' ", con);
        SqlDataReader rd = cmd_select.ExecuteReader();

        if (rd.Read())
        {
            txt_req_date.Text = Convert.ToString(rd["req_date"]);
            ddl_emp.SelectedItem.Text = Convert.ToString(rd["employeeid"]);
            txt_empnam.Value = Convert.ToString(rd["employeename"]);
            ddl_loan.SelectedItem.Text = Convert.ToString(rd["loan_appid"]);
            txt_loan_type.Value = Convert.ToString(rd["loan_type"]);
            txt_loan_name.Value = Convert.ToString(rd["loan_name"]);
            txt_loan_amt.Value = Convert.ToString(rd["loan_amount"]);
            txt_rem_mon.Value = Convert.ToString(rd["rem_month"]);
            ddl_mon_topost.SelectedItem.Text = Convert.ToString(rd["topost"]);
            ddl_mon_posted.SelectedItem.Text = Convert.ToString(rd["postedon"]);
            txt_post_amt.Value = Convert.ToString(rd["postedamt"]);
            txt_bal_amt.Value = Convert.ToString(rd["balance_amt"]);
            txt_app_by.Value = Convert.ToString(rd["approve_by"]);
            ddl_mon_topost.SelectedItem.Value = Convert.ToString(rd["month_to_posted"]);
            ddl_mon_posted.SelectedItem.Value = Convert.ToString(rd["month_posted_on"]);

        }

        con.Close();
    }
    protected void txt_loan_req_TextChanged(object sender, EventArgs e)
    {
        
    }
}
