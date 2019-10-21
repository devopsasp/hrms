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
    string s_form = "", subquery, t, qry, _Value, ename = "", mode = "", fname="";
    DataSet ds_userrights;
    int ddl_i, i, index, cur_yr, yr_it, total = 0, eid = 0, salary = 0;
    double avg, tot_pts, tot_amt=0, basic = 0, allot = 0;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        fname = (string)Session["formulaName"];
        
       
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        l.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
       

        if (!IsPostBack)
        {

            CompanyList = company.fn_getCompany();
            CalendarExtender1.CssClass = "CalendarStyle";
            if (CompanyList.Count > 0)
            {
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

               

                switch (s_login_role)
                {
                    case "a":
                        hr();
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
        lblerror.Text = "";
        populate_ddldept();
        ddl_year_load();
        cmd_chk_allotmnet.Visible = false;
        if (fname != null)
        {
            if (fname != "")
            {
                check_allotment();
            }
        }
    }

    public void check_allotment()
    {
        string[] slp = fname.Split('-');

        ddl_dept.SelectedValue = slp[1];
        ddl_dept.Enabled = false;
        populate_ddlemp_dept();

        if (slp[3] == "Percentage")
        {
            tr_alloted_amt.Visible = false;
            tr_alloted_per.Visible = true;
            txt_alloted_per.Text = slp[4];
        }
        else if (slp[3] == "Amount")
        {
            tr_alloted_per.Visible = false;
            tr_alloted_amt.Visible = true;
            txt_alloted_amt.Text = slp[4];
        }

        tr_ddl_emp.Visible = true;
        chk_emp.Visible = true;
        chk_emp.Enabled = false;
        chk_all.Visible = false;
        tr_mode.Visible = false;
        tr_value.Visible = false;
        chk_emp.SelectedValue = slp[2];
    }

    public void populate_ddldept()
    {
        var conStr = ConfigurationManager.ConnectionStrings["connectionstring"];
        string constr = conStr.ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        SqlCommand cmd = new SqlCommand("select pn_departmentid,v_departmentname from paym_department where pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "'", con);
        SqlDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            ListItem list = new ListItem();
            list.Text = rdr[1].ToString();
            list.Value = rdr[0].ToString();
            ddl_dept.Items.Add(list);
        }
        con.Close();
    }

    public void populate_ddlemp_dept()
    {
        chk_emp.Items.Clear();
        var conStr = ConfigurationManager.ConnectionStrings["connectionstring"];
        string constr = conStr.ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        SqlCommand cmd = new SqlCommand("select a.pn_employeeid,b.employee_full_name from paym_employee_profile1 a,paym_employee b where a.pn_branchid='" + employee.BranchId + "' and a.pn_departmentid='" + ddl_dept.SelectedValue.ToString() + "' and a.pn_employeeid=b.pn_employeeid and a.pn_companyid='" + employee.CompanyId + "'", con);
        SqlDataReader rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
            ListItem list = new ListItem();
            list.Text = rdr[1].ToString();
            list.Value = rdr[0].ToString();
            chk_emp.Items.Add(list);
        }
        
        rdr.Close();
        con.Close();
    }


    public void chk_for_allotmnets()
    {
        var conStr = ConfigurationManager.ConnectionStrings["connectionstring"];
        string constr = conStr.ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        //lblerror.Text = chk_emp.Items.Count.ToString();
        SqlCommand cmd = new SqlCommand("select empid from annual_increment_allotments where pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "' and department='" + ddl_dept.SelectedItem.Text + "' and calendar_year='" + ddl_cyear.SelectedItem.Text + "'", con);
        SqlDataReader rdr = cmd.ExecuteReader();
        if (rdr.HasRows)
        {
            cmd_chk_allotmnet.Visible = true;
            for (int i = 0; i < chk_emp.Items.Count; i++)
            {
                while (rdr.Read())
                {
                    int empid = Convert.ToInt32(rdr[0]);
                    if (empid == Convert.ToInt32(chk_emp.Items[i].Value))
                    {
                        chk_emp.Items[i].Selected = true;
                        chk_emp.Items[i].Enabled = false;
                        chk_emp.Items[i].Attributes.Add("style", "color:red");
                    }
                }
            }
            rdr.Close();
        }
    }

    protected void ddl_dept_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblerror.Text = "";
        tr_ddl_emp.Visible = true;
        populate_ddlemp_dept();
        txt_alloted_amt.Text = "";
        txt_alloted_per.Text = ""; 
        chk_for_allotmnets();
 
    }

    protected void cmd_allot_Click(object sender, EventArgs e)
    {
        try
        {
            lblerror.Text = "";
            int flag = 0;
            var conStr = ConfigurationManager.ConnectionStrings["connectionstring"];
            string constr = conStr.ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string date = txtdate.Text;
            string[] mydate = date.Split('/');
            string day = mydate[0];
            string month = mydate[1];
            string year = mydate[2];
            string date1 = month + "/" + day + "/" + year;

            if (tr_dept.Visible == true)
            {

                for (int i = 0; i < chk_emp.Items.Count; i++)
                {
                    if (chk_emp.Items[i].Selected == true)
                    {
                        employee.EmployeeId = Convert.ToInt32(chk_emp.Items[i].Value);
                        if (tr_alloted_amt.Visible == true)
                        {
                            txt_alloted_per.Text = "0";
                        }

                        else if (tr_alloted_per.Visible == true)
                        {
                            txt_alloted_amt.Text = "0";
                        }

                        if (chk_emp.Items[i].Enabled == true)
                        {
                            if (fname == null || fname == "")
                            {
                                mode = ddl_mode.SelectedItem.Text;
                                if (mode == "Amount")
                                {
                                    total = Convert.ToInt32(txt_value.Text);
                                }
                                else if (mode == "Percentage on basic")
                                {
                                    cmd = new SqlCommand("select basic_salary from paym_employee where pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + employee.BranchId + "' and pn_employeeid = '" + chk_emp.Items[i].Value + "'", myConnection);
                                    salary = (int)cmd.ExecuteScalar();
                                    total = salary * (Convert.ToInt32(txt_value.Text) / 100);
                                }
                                else
                                {
                                    lblerror.Text = "Please select the increment mode";
                                    return;
                                }
                                cmd = new SqlCommand("insert into annual_increment_allotments(empid,deptid,department,allotment_amt,alloted_percent,status,date,grade_dept,pn_branchid,pn_companyid,empname,calendar_year)values('" + chk_emp.Items[i].Value + "','" + ddl_dept.SelectedValue.ToString() + "','" + ddl_dept.SelectedItem.Text + "','" + total + "','','y','" + date1 + "','dept','" + employee.BranchId + "','" + employee.CompanyId + "','" + chk_emp.Items[i].Text + "','" + ddl_cyear.SelectedItem.Text + "')", con);
                                cmd.ExecuteNonQuery();
                                flag = 1;
                                salary_structure();
                            }

                            else
                            {
                                cmd = new SqlCommand("insert into annual_increment_allotments(empid,deptid,department,allotment_amt,alloted_percent,status,date,grade_dept,pn_branchid,pn_companyid,empname,calendar_year)values('" + chk_emp.Items[i].Value + "','" + ddl_dept.SelectedValue.ToString() + "','" + ddl_dept.SelectedItem.Text + "','" + txt_alloted_amt.Text + "','" + txt_alloted_per.Text + "','y','" + date1 + "','dept','" + employee.BranchId + "','" + employee.CompanyId + "','" + chk_emp.Items[i].Text + "','" + ddl_cyear.SelectedItem.Text + "')", con);
                                cmd.ExecuteNonQuery();
                                flag = 1;
                                salary_structure_dept();
                            }
                            
                        }
                        
                    }   
                    
                }
            }
           
            if (flag == 0)
            {
                lblerror.Text = "Please select employees";
            }
            else if (flag == 1)
            {
                lblerror.Text = "Allotment Saved";
                chk_for_allotmnets();
                lv_inc_details.DataBind();
            }
        }

        catch (Exception ex)
        {
            lblerror.Text = ex.Message;
        }
    }

    public void salary_structure()
    {
        tot_amt = salary + total;
        employee.d_Date = txtdate.Text;
        employee.salary = tot_amt.ToString();
        employee.salary_Increment(employee);
    }

    public void salary_structure_dept()
    {
        myConnection.Open();
        cmd = new SqlCommand("select a.pn_employeeid,a.employee_first_name,a.basic_salary,b.alloted_percent,b.allotment_amt,c.increment_type from  paym_employee a,annual_increment_allotments b,app_increment c where a.pn_employeeid=b.empid and b.department=c.department and a.pn_BranchID=b.pn_BranchID and b.pn_BranchID=c.pn_BranchID and b.empid = '" + chk_emp.Items[i].Value + "'", myConnection);
        rea = cmd.ExecuteReader();
        if (rea.Read())
        {
            basic = Convert.ToDouble(rea[2]);
            allot = Convert.ToDouble(rea[3]);
            mode = rea[5].ToString();
        }
        if (mode == "Percentage")
        {
            tot_amt = (basic * (allot/100)) + basic;
        }
        else if (mode == "Amount")
        {
            tot_amt = basic + Convert.ToDouble(rea[4]);
        }
        employee.d_Date = txtdate.Text;
        employee.salary = tot_amt.ToString();
        myConnection.Close();
        employee.salary_Increment(employee);
    }

    public void salary_structure_grade()
    {
        myConnection.Open();
        cmd = new SqlCommand("select a.pn_employeeid,a.employee_first_name,a.basic_salary,b.alloted_percent,b.allotment_amt,c.increment_type from  paym_employee a,annual_increment_allotments b,app_increment c where a.pn_employeeid=b.empid and b.gradename=c.grade and a.pn_BranchID=b.pn_BranchID and b.pn_BranchID=c.pn_BranchID and b.empid = '" + chk_emp.Items[i].Value + "'", myConnection);
        rea = cmd.ExecuteReader();
        if (rea.Read())
        {
            basic = Convert.ToDouble(rea[2]);
            allot = Convert.ToDouble(rea[3]);
            mode = rea[5].ToString();
        }
        if (mode == "Percentage")
        {
            tot_amt = (basic * allot) + basic;
        }
        else if (mode == "Amount")
        {
            tot_amt = basic + Convert.ToDouble(rea[4]);
        }
        employee.d_Date = txtdate.Text;
        employee.salary = tot_amt.ToString();
        myConnection.Close();
        employee.salary_Increment(employee);
    }

    protected void cmd_de_allot_Click(object sender, EventArgs e)
    {
        try
        {
            lblerror.Text = "";
            var conStr = ConfigurationManager.ConnectionStrings["connectionstring"];
            string constr = conStr.ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            for (int i = 0; i < chk_emp.Items.Count; i++)
            {
                if (chk_emp.Items[i].Selected == true)
                {
                    SqlCommand cmd = new SqlCommand("delete annual_increment_allotments where empid='" + chk_emp.Items[i].Value.ToString() + "' and pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "'", con);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("delete salary_structure where pn_Employeeid='" + chk_emp.Items[i].Value.ToString() + "' and Remarks='Annual Increment' and pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "'", con);
                    cmd.ExecuteNonQuery();
                }
                //lblerror.Text = i + " Allotements deleted";
            }
        }
        catch (Exception excep)
        {
            lblerror.Text = "Error";
        }

    }

    protected void cmd_chk_allotmnet_Click(object sender, EventArgs e)
    {
        try
        {
            lblerror.Text = "";
            //chk_for_allotmnets();

            var conStr = ConfigurationManager.ConnectionStrings["connectionstring"];
            string constr = conStr.ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("select a.empname,a.empid,b.basic_salary,a.allotment_amt,b.basic_salary+a.allotment_amt as Revised_Basic, a.date from annual_increment_allotments a,paym_employee b where a.pn_branchid='" + employee.BranchId + "' and a.pn_companyid='" + employee.CompanyId + "' and a.empid=b.pn_employeeid and a.pn_branchID=b.pn_branchID and a.deptid='" + ddl_dept.SelectedItem.Value.ToString() + "' and calendar_year = '" + ddl_cyear.SelectedItem.Text + "'", con);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            lv_inc_details.DataSource = ds;
            lv_inc_details.DataBind();
            lv_inc_details.Visible = true;
        }

        catch (Exception excep)
        {
            lblerror.Text = "Error";
        }
   
    }


    protected void lv_inc_details_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        try
        {
            var conStr = ConfigurationManager.ConnectionStrings["connectionstring"];
            string constr = conStr.ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            Label lblid = (Label)lv_inc_details.Items[e.ItemIndex].FindControl("lblid");
            SqlCommand cmd = new SqlCommand("delete annual_increment_allotments where empid='" + lblid.Text + "' and pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "'", con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("delete salary_structure where pn_Employeeid='" + lblid.Text + "' and Remarks='Annual Increment' and pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "'", con);
            cmd.ExecuteNonQuery();
            Response.Write("<script>alert('Record Deleted')</script>");
            chk_for_allotmnets();
            lv_inc_details.DataBind();
        }
        catch (Exception ex)
        {
            lblerror.Text = "Error Occured";
        }
    }

    protected void chk_emp_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void lv_inc_details_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    public void ddl_year_load()
    {
        try
        {
            i = 0;
            cur_yr = DateTime.Now.Year;
            cur_yr = cur_yr + 5;

            for (yr_it = 2010; yr_it <= cur_yr; yr_it++)
            {
                ddl_cyear.Items.Add(Convert.ToString(yr_it) + "-" + Convert.ToString(yr_it + 1));
                i++;
            }
            i = i - 5;
            ddl_cyear.SelectedIndex = i;
        }
        catch (Exception ex)
        {
            lblerror.Text = "Error in loading year";
        }
    }

    protected void ddl_cyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_dept.SelectedIndex = 0;
        cmd_chk_allotmnet.Visible = false;
        chk_emp.Items.Clear();
        lv_inc_details.Visible = false;
    }
    protected void lv_inc_details_ItemEditing(object sender, ListViewEditEventArgs e)
    {
        lv_inc_details.EditIndex = e.NewEditIndex;
        hr();
    }
}
