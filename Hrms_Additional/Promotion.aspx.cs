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
using ePayHrms.BE.Recruitment;
using ePayHrms.Connection;
using ePayHrms.Candidate;
using System.IO;
using ePayHrms.Company;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ePayHrms.Leave;
using ePayHrms.Employee;

public partial class Hrms_Additional_Default : System.Web.UI.Page
{
    ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader rea;
    SqlDataAdapter ada = new SqlDataAdapter();
    Company company = new Company();
    Employee employee = new Employee();
    Leave l = new Leave();
    string gname = "";
    Collection<Leave> LeaveList, AppraisalList;
    Collection<Company> CompanyList;
    Collection<Employee> EmployeeList;
    Collection<Company> ddlBranchsList;

    string s_login_role;
    string s_form = "", subquery, qry, _Value;
    DataSet ds_userrights;
    int ddl_i, i, index, total = 0;
    double avg, tot_pts;
    int eid = 0;

    DataSet ds = new DataSet();
    //DropDownList ddlemp_virtual = new DropDownList();
    


    protected void Page_Load(object sender, EventArgs e)
    {
        Session["formulaName"] = "";
        
       
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        l.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
       

        if (!IsPostBack)
        {

            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

               

                switch (s_login_role)
                {
                    case "a":

                        
                        
                        break;

                    case "h":
                        hr();
                        break;

                    case "e": 
                       
                        l.EmployeeID = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
                        
                        break;

                    case "u": //s_form = "5";
                        s_form = "41";
                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                           
                           
                           
                        }
                        else
                        {
                            
                            l.EmployeeID = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);

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

    public void hr()
    {
        initial_load();
    }

    public void initial_load()
    {
        var conStr = ConfigurationManager.ConnectionStrings["connectionstring"];
        string constr = conStr.ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        SqlCommand cmd = new SqlCommand("select distinct grade,gradeid from promotion1 where pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "'", con);
        SqlDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            ListItem list = new ListItem();
            list.Text=rdr[0].ToString();
            list.Value=rdr[1].ToString();
            ddlgrade.Items.Add(list);   
        }
        rdr.Dispose();
        rdr.Close();
        SqlCommand cmd1 = new SqlCommand("select distinct v_departmentname,pn_DepartmentID from paym_department where pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "'", con);
        rdr = cmd1.ExecuteReader();
        while (rdr.Read())
        {
            ListItem list = new ListItem();
            list.Text = rdr[0].ToString();
            list.Value = rdr[1].ToString();
            ddldept.Items.Add(list);
        }
        chk_for_allotmnets();
        chk_for_SelectAll();
    }

    
    public void chk_for_allotmnets()
    {
        ddlemp_virtual.Items.Clear();
        
        chk_emp_alloted.Items.Clear();
        var conStr = ConfigurationManager.ConnectionStrings["connectionstring"];
        string constr = conStr.ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        SqlCommand cmd = new SqlCommand("select empid from annual_increment_allotments_slab where pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "'", con);
        SqlDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            ddlemp_virtual.Items.Add(rdr[0].ToString());
            
            for(int i=0;i<chk_emp.Items.Count;i++)
            {
                
                if (chk_emp.Items[i].Value == rdr[0].ToString())
                {                    
                    ListItem list = new ListItem();
                    list.Text = chk_emp.Items[i].Text;
                    list.Value = chk_emp.Items[i].Value;
                    chk_emp_alloted.Items.Add(list); 
                    chk_emp_unalloted.Items.Remove(list);    
                }
                
            }
        }
    }

    public void populate_lv()
    {
        var conStr = ConfigurationManager.ConnectionStrings["connectionstring"];
        string constr = conStr.ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        con.Open();

        SqlCommand cmd = new SqlCommand("select * from annual_increment_allotments_slab where pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "'", con);
        SqlDataAdapter ada = new SqlDataAdapter(cmd);
        
        ada.Fill(ds);
        ds.AcceptChanges();
        lv_details.DataSource = ds;
        lv_details.DataBind();
    }
    public void chk_for_SelectAll()
    {
        if (chk_emp_unalloted.Items.Count >= 2)
        {
            chk_sel_al.Visible = true;
        }
        else
        {
            chk_sel_al.Visible = false;
        }
        if (chk_emp_alloted.Items.Count >= 2)
        {
            chk_sel_al2.Visible = true;
        }
        else
        {
            chk_sel_al2.Visible = false;
        }
    }


    public void Populate_VieweList()
    {
        var constr = ConfigurationManager.ConnectionStrings["ConnectionString"];
        string Cstr = constr.ConnectionString;
        SqlConnection con = new SqlConnection(Cstr);
        SqlCommand com = new SqlCommand("select * from annual_increment where pn_branchID = '" + employee.BranchId + "' and pn_CompanyID = '" + employee.CompanyId + "'", con);
        SqlDataAdapter ada = new SqlDataAdapter(com);
        DataSet ds = new DataSet();
        ada.Fill(ds, "Annual_increment");
        lv_details.DataSource = ds;
        lv_details.DataBind();
        ds.AcceptChanges();

    }

    protected void lv_details1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int sno = Convert.ToInt32(lv_details.DataKeys[0].Value);
        myConnection.Open();
        cmd = new SqlCommand("Update annual_increment set incr_amount = '" + txtper.Text + "' where Sno = " + sno + "", myConnection);
        cmd.ExecuteNonQuery();
        lblerror.Text = "Increment saved successfully";
        myConnection.Close();
    }

    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        chk_emp_alloted.Items.Clear();
        chk_emp_unalloted.Items.Clear();

        try
        {
            var conStr = ConfigurationManager.ConnectionStrings["connectionstring"];
            string constr = conStr.ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();

            //this query checks all required constraints(employee under particular dept AND grade)
                       
            SqlCommand cmd = new SqlCommand("select a.pn_employeeid,b.employee_full_name from paym_employee_profile1 a,paym_employee b,promotion1 c where a.pn_gradeid='"+ddlgrade.SelectedItem.Value+"' and a.pn_gradeid=c.gradeid and a.pn_departmentid='"+ddldept.SelectedItem.Value+"' and a.pn_departmentid=c.deptid and a.pn_branchid=9 and a.pn_branchid=c.pn_branchid and a.pn_companyid='"+employee.CompanyId+"' and a.pn_companyid=c.pn_companyid and a.pn_employeeid=b.pn_employeeid group by a.pn_employeeid , b.employee_full_name", con);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                ListItem list = new ListItem();
                list.Text = rdr[1].ToString();
                list.Value = rdr[0].ToString();

                chk_emp.Items.Add(list);
                chk_emp_unalloted.Items.Add(list);
            }

            if (!rdr.HasRows)
            {
                lblerror.Text = "No Employee Found in this category";
                chk_emp.Items.Clear();
                txtper.Text = "";
            }
            else
            {
                lblerror.Text = "";
            }
            rdr.Dispose();
            rdr.Close();
            cmd.Dispose();
            //lblerror.Text = chk_emp.Items[1].Text;
        }
        catch (Exception ex)
        {
            lblerror.Text = "Error:" + ex.Message;
        }
        finally
        {
            chk_for_allotmnets();
            chk_for_SelectAll();
        }
    }
    protected void cmd_allot_Click(object sender, EventArgs e)
    {
        var conStr = ConfigurationManager.ConnectionStrings["connectionstring"];
        string constr = conStr.ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        for (int i = 0; i < chk_emp_unalloted.Items.Count; i++)
        {
            if (chk_emp_unalloted.Items[i].Selected == true)
            {
                double bp = 0, x = 0;
                SqlCommand cmd_bp = new SqlCommand("select basic_salary from paym_employee where pn_employeeid='" + chk_emp_unalloted.Items[i].Value + "' and pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "'", con);
                SqlDataReader rdr_bp = cmd_bp.ExecuteReader();
                rdr_bp.Read();
                if (rdr_bp.HasRows)
                {
                    bp = Convert.ToDouble(rdr_bp[0].ToString());
                }
                rdr_bp.Dispose();
                rdr_bp.Close();
                double per = -1;
                SqlCommand cmd_slab = new SqlCommand("select basic,upto_amount,percentage from promotion1 where gradeid='" + ddlgrade.SelectedItem.Value + "' and deptid='" + ddldept.SelectedItem.Value + "'", con);
                SqlDataReader rdr_slab = cmd_slab.ExecuteReader();
                while (rdr_slab.Read())
                {
                    double basic = Convert.ToDouble(rdr_slab[0].ToString());
                    double upto = Convert.ToDouble(rdr_slab[1].ToString());
                    if (bp >= basic && bp <= upto)
                    {
                        per = Convert.ToDouble(rdr_slab[2].ToString());
                    }
                }

                double sal = bp + (bp * (per / 100));

                if (txtdate.Text == "")
                {
                    txtdate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                }
                if (per != -1)
                {
                    SqlCommand cmd = new SqlCommand("insert into annual_increment_allotments_slab (employeename,empid,inc_percentage,cal_amt,basic,pn_branchid,pn_companyid,date) values('" + chk_emp_unalloted.Items[i].Text + "','" + chk_emp_unalloted.Items[i].Value + "','" + per + "','" + sal + "','" + bp + "','" + employee.BranchId + "','" + employee.CompanyId + "','" + txtdate.Text + "')", con);
                    x = cmd.ExecuteNonQuery();
                    if (x == 1)
                    {
                        lblerror.Text = "Allotment made";
                        txtdate.Text = "";
                    }
                    else
                    {
                        lblerror.Text = "";
                        txtdate.Text = "";
                    }
                }
                else
                {
                    lblerror.Text = "The Selected Employee's(basicpay) in Unalloted list are unfit to PromotionSlab at Master";
                }
            }
        }
        chk_for_allotmnets();
        chk_for_SelectAll();
        populate_lv();
    }
    protected void lnk_chk_allot_Click(object sender, EventArgs e)
    {
        chk_for_allotmnets();
    }
    protected void cmd_deallot_Click(object sender, EventArgs e)
    {
        int x = -10;
        var conStr = ConfigurationManager.ConnectionStrings["connectionstring"];
        string constr = conStr.ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        for (int i = 0; i < chk_emp_alloted.Items.Count; i++)
        {
            if (chk_emp_alloted.Items[i].Selected == true)
            {
                SqlCommand cmd = new SqlCommand("delete annual_increment_allotments_slab where empid='" + chk_emp_alloted.Items[i].Value + "' and pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "'", con);
                x = cmd.ExecuteNonQuery();
                ListItem list = new ListItem();
                list.Text = chk_emp_alloted.Items[i].Text;
                list.Value = chk_emp_alloted.Items[i].Value;
                chk_emp_unalloted.Items.Add(list);
            }
        }
        if (x == 1)
        {
            lblerror.Text = "Allotment Deleted";
        }
        else
        {
            lblerror.Text = "";
        }
        chk_for_allotmnets();
        chk_for_SelectAll();
        populate_lv();
    }
   
    protected void ddlgrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddldept.Enabled = true;
        ddldept.SelectedIndex = 0;
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        populate_lv();
        chk_for_SelectAll();
    }
    
    protected void chk_sel_al_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_sel_al.Checked == true)
        {
            for (int i = 0; i < chk_emp_unalloted.Items.Count; i++)
            {
                chk_emp_unalloted.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i < chk_emp_unalloted.Items.Count; i++)
            {
                chk_emp_unalloted.Items[i].Selected = false;
            }
        }
    }
    protected void chk_sel_al2_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_sel_al2.Checked == true)
        {
            for (int i = 0; i < chk_emp_alloted.Items.Count; i++)
            {
                chk_emp_alloted.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i < chk_emp_alloted.Items.Count; i++)
            {
                chk_emp_alloted.Items[i].Selected = false;
            }
        }
    }
}
