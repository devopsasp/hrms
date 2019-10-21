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
    string formula = "";
    string s_login_role;
    string s_form = "", subquery, qry, _Value;
    DataSet ds_userrights;
    int ddl_i, i, index, total = 0;
    double avg, tot_pts;
    int eid = 0;
    //getting the value from ascx page
    
    protected void Page_Load(object sender, EventArgs e)
    {
        TextBox1.Visible=false;
        Session["formulaName"] = "";
        
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        l.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        //txttot_pts.Value = "";
        //txttot_amt_mei.Value = "";

        //Session["noofstars"] = "2";

        if (!IsPostBack)
        {

            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

                btn_save.Visible = false;
                TextBox1.Visible = false;
                btn_update.Visible = false;
               
                but_cal.Visible = false;

                switch (s_login_role)
                {
                    case "a": 
                        ddl_Branch_load();
                        break;

                    case "h": //ddl_Branch.Visible = false;
                        allotment.Visible = false;
                        ddl_department_load();
                        break;

                    case "e": //ddl_Branch.Visible = false;
                        
                        btn_save.Visible = false;
                        txt_date.Enabled = false;
                        l.EmployeeID = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
                        //Emp_grid_load();
                        //emp_grid_hide();
                        break;

                    case "u": //s_form = "5";
                        s_form = "41";
                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            //ddl_Branch.Visible = false;
                            
                            ddl_department_load();
                        }
                        else
                        {
                            //ddl_Branch.Visible = false;
                            
                            btn_save.Visible = false;
                            txt_date.Enabled = false;
                            l.EmployeeID = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);

                        }

                        break;

                    default: Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
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

    public void ddl_Branch_load()
    {
        //branck dropdown
        //ddlBranchsList = company.fn_getBranchs();
        //if (ddlBranchsList.Count > 0)
        //{
        //    for (ddl_i = -1; ddl_i < ddlBranchsList.Count; ddl_i++)
        //    {
        //        if (ddl_i == -1)
        //        {
        //            ListItem list = new ListItem();

        //            list.Text = "Select Branch";
        //            list.Value = "0";
        //            ddl_Branch.Items.Add(list);
        //        }
        //        else
        //        {
        //            ListItem list = new ListItem();

        //            list.Text = ddlBranchsList[ddl_i].CompanyName;
        //            list.Value = ddlBranchsList[ddl_i].CompanyId.ToString();
        //            ddl_Branch.Items.Add(list);
        //        }
        //    }
        //}
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



        qry = "Select a.pn_EmployeeID, a.EmployeeCode, a.Employee_First_Name from paym_employee a, paym_employee_profile1 b where a.pn_CompanyID=b.pn_CompanyID and a.pn_BranchID=b.pn_BranchID and a.pn_EmployeeID=b.pn_EmployeeID and b.pn_DepartmentID=" + ddl_department.SelectedValue + " and b.pn_CompanyID=" + employee.CompanyId + " and a.status='y' and b.pn_BranchID=" + employee.BranchId;

        EmployeeList = employee.fn_getEmplist(qry);

        if (EmployeeList.Count > 0)
        {
            
            for (ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();
                    e_list.Text = "Select Employee";
                    e_list.Value = "0";
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
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Employee Found');", true);
           
        }
    }

    public void grid_load()
    {
        qry = "";
        qry = "select  a.pn_appraisalid, a.v_AppraisalQues, ";
        qry += "(Select b.ratings from paym_Emp_Appraisalnew as b where b.pn_appraisalid=a.pn_appraisalid  and b.pn_EmployeeID=" + ddl_Employee.SelectedValue + ") as Ratings";
        qry += " from Appraisal_type a where  a.pn_appraisalid in (select pn_appraisalid from paym_Emp_Appraisalnew where pn_employeeid=" + ddl_Employee.SelectedValue + " and Appraisal_type=" + rdo_Appraisallist.SelectedValue + ")";
        qry += " or a.pn_appraisalid not in (select pn_appraisalid from paym_Emp_Appraisalnew where pn_employeeid=" + ddl_Employee.SelectedValue + " and Appraisal_type=" + rdo_Appraisallist.SelectedValue + ")";
        qry += " and a.pn_departmentid in (" + ddl_department.SelectedValue + ",0) and a.Appraisal_type=" + rdo_Appraisallist.SelectedValue + " and a.Flag='Y' and a.pn_branchid='" + employee.BranchId + "'";
        AppraisalList = l.fn_paym_emp_Appraisal_count(qry);

        if (AppraisalList.Count > 0)
        {
            
            grid_appraisal.DataSource = AppraisalList;
            grid_appraisal.DataBind();


            btn_save.Visible = true;
            btn_update.Visible = false;
           
            grid_appraisal.Visible = true;
            //global_var ob = new global_var();
            foreach (GridViewRow row in grid_appraisal.Rows)
            {
                index = row.RowIndex;
                for (i = 0; i < AppraisalList.Count; i++)
                {
                    if (grid_appraisal.DataKeys[index].Value.ToString() == AppraisalList[i].AppraisalID.ToString())
                    {
                        ((TextBox)grid_appraisal.Rows[index].FindControl("txtslidetarget")).Text = AppraisalList[i].Point.ToString();
                        tot_pts += Convert.ToDouble(AppraisalList[i].Point);
                        if (Convert.ToDouble(AppraisalList[i].Point) == 0.0)
                        {
                            btn_save.Visible = true;
                            btn_update.Visible = false;
                        }
                        else
                        {
                            btn_save.Visible = false;
                            btn_update.Visible = true;
                        }
                    }
                }
            }
            txttot_pts.Value = tot_pts.ToString();
            //total_amount();
        }
        else
        {
           
        }

    }
    public void grid_load1()
    {
        var conStr = ConfigurationManager.ConnectionStrings["connectionstring"];
        string constr = conStr.ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from task_schedule where pn_employeeid='" + ddl_Employee.SelectedItem.Value + "'", con);
        SqlDataAdapter ad = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        ad.Fill(ds, "task_schedule");
        gvd_task.DataSource = ds;
        int i = ds.Tables[0].Rows.Count;
        gvd_task.DataBind();
        con.Close();
        //gvd_task.FooterRow.Visible = false;
        con.Open();
        string rate = "";
        int rating = 0;
        double eff;
        cmd = new SqlCommand("select rating from task_schedule  where pn_employeeid='" + ddl_Employee.SelectedItem.Value + "'", con);
        SqlDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            rate = rdr["rating"].ToString();
            if (rate != "")
            {
                rating += Convert.ToInt32(rate);

                int j = i * 10;
                eff = (((double)rating / (double)j) * 100);
                string eff1 = Convert.ToInt32(eff) + "%";
                gvd_task.Rows[0].Cells[9].RowSpan = i;
                gvd_task.Rows[0].Cells[9].Text = eff1.ToString();
                for (int ij = 1; ij < i; ij++)
                {
                    gvd_task.Rows[ij].Cells[9].Visible = false;
                }
               // gvd_task.Rows[i].Cells[9].Visible = false;
            }
        }
        

        //         else
        //      {


        //     gvd_task.DataBind();
        //}

    }
   
    protected void btn_Back_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/Company_Home.aspx");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

    protected void ddl_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (ddl_Branch.SelectedValue != "0")
            //{
            //    ViewState["Appraisal_BranchID"] = Convert.ToInt32(ddl_Branch.SelectedItem.Value);
            //    ddl_department_load();
            //    tbl_details.Visible = true;
            //    Tr2.Visible = false;
            //    row_emp.Visible = false;
            //    Tr4.Visible = false;
            //    tbl_grd.Visible = false;
            //    ddl_department.SelectedValue = "sd";
            //}
            //else
            //{
            //    tbl_details.Visible = false;
            //    tbl_grd.Visible = false;
            //}
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

    public void ddl_department_load()
    {
        //here

        if (s_login_role == "a")
        {
            //employee.BranchId = Convert.ToInt32(ddl_Branch.SelectedItem.Value);
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
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Department Available');", true);
        }

    }

    protected void ddl_Employee_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvd_task.Visible = false;
        txt_amt.Text = "";
        txttot_pts.Value = "";
        grid_appraisal.Visible = false;
        txttot_pts.Visible = false;
        txt_amt.Visible = false;
        lbl_mode.Visible = false;
        TextBox1.Visible = false;
        LinkButton1.Visible = false;
        
        but_cal.Visible = true;
        var conStr = ConfigurationManager.ConnectionStrings["connectionstring"];
        string constr = conStr.ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        con.Open();
         
        SqlCommand cmd = new SqlCommand("select d_date from paym_Emp_Appraisalnew where pn_branchid='" + employee.BranchId + "'and pn_companyid='" + employee.CompanyId + "' and pn_employeeid='" + ddl_Employee.SelectedValue.ToString() + "'", con);
        SqlDataReader rdr = cmd.ExecuteReader();
        if (rdr.Read())
        {
            DateTime dt = Convert.ToDateTime(rdr[0]);
            txt_date.Text = dt.ToShortDateString();
        }
        con.Close();
        //******************
        if (ddl_Employee.SelectedValue != "0")
        {
            ViewState["Appraisal_EmployeeID"] = Convert.ToInt32(ddl_Employee.SelectedItem.Value);

            if (s_login_role == "a")
            {
                l.BranchID = (int)ViewState["Appraisal_BranchID"];
            }

            if (s_login_role == "h")
            {
                l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            }

            l.EmployeeID = (int)ViewState["Appraisal_EmployeeID"];

            grid_load();
            grid_load1();
           
            gvd_task.Visible = true;
            
        }
        else
        {
            
            txttot_pts.Value = "";
            //txttot_amt_mei.Value = "";
        }

    }
    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_amt.Text = "";
        gvd_task.Visible = false;
        TextBox1.Visible = false;
        LinkButton1.Visible = false;
        
        if (ddl_department.SelectedValue != "sd")
        {
            ddl_employee_load();
            
        }
        else
        {
            
        }
    }

    

    protected void btn_save_Click(object sender, EventArgs e)
    {
        
        double bp_str = 0.0;
        string grade = "";
        int gradeid = 0;
        for (i = 0; i < grid_appraisal.Rows.Count; i++)
        {
            if (s_login_role == "a")
            {
                l.BranchID = (int)ViewState["Appraisal_BranchID"];
            }

            if (s_login_role == "h")
            {
                l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            }

            l.EmployeeID = Convert.ToInt32(ddl_Employee.SelectedValue);
            //lbl_Error.Text =Convert.ToString( l.EmployeeID);//*********del mei
            l.AppraisalID = Convert.ToInt32(grid_appraisal.DataKeys[i].Value);
            l.Count1 = Convert.ToDouble((((TextBox)grid_appraisal.Rows[i].FindControl("txtslidebound")).Text));
            l.Departmentid = Convert.ToInt32(ddl_department.SelectedValue);
           // l.str_fromdate = DateTime.Now.ToString("dd/MM/yyyy"); //DateTime.Now.Date.f; //Convert.ToDateTime(txt_date.Value);
            // Label2.Text = DateTime.Now.ToString("dd/MM/yyyy");
            string date = txt_date.Text;
            string[] date1 = date.Split('/');
            string dd = date1[0];
            string mm = date1[1];
            string yy = date1[2];
            string dat = mm + "/" + dd + "/" + yy;
            l.AppraisalName = rdo_Appraisallist.SelectedValue;
            //l.Emp_Appraisal(l);
            qry = "insert into paym_Emp_Appraisalnew (pn_CompanyID, pn_BranchID, pn_DepartmentID, pn_EmployeeID, pn_AppraisalID, Appraisal_type, ratings, d_Date, status) values (" + l.CompanyID + "," + l.BranchID + "," + l.Departmentid + "," + l.EmployeeID + "," + l.AppraisalID + "," + l.AppraisalName + "," + l.Count1 + ",'"+dat+"','Y')";
            _Value = employee.fn_procappraisal(qry);
            if (_Value == "0")
            {
                if (i == 0)
                {

                    myConnection.Open();
                    SqlCommand bp = new SqlCommand("select basic_salary from paym_employee where pn_employeeid='" + l.EmployeeID + "'", myConnection);
                    SqlDataReader bprdr = bp.ExecuteReader();
                    if (bprdr.Read())
                    {
                        bp_str = Convert.ToInt32(bprdr[0]);//getting bascic pay

                    }
                    bprdr.Close();
                    //getting gradeid from emp id
                    SqlCommand mygradeid = new SqlCommand("select pn_gradeid from paym_employee_profile1 where pn_employeeid='" + l.EmployeeID + "'", myConnection);
                    SqlDataReader gidrdr = mygradeid.ExecuteReader();
                    if (gidrdr.Read())
                    {
                        gradeid = Convert.ToInt32(gidrdr[0]);
                    }
                    gidrdr.Close();
                    //getting grade from grade id
                    SqlCommand mygrade = new SqlCommand("select v_gradename from paym_grade where pn_gradeid='" + gradeid + "'", myConnection);
                    SqlDataReader grdr = mygrade.ExecuteReader();
                    if (grdr.Read())
                    {
                        grade = grdr[0].ToString();
                    }
                    grdr.Close();

                    //inserting values   

                    // double totamt = Convert.ToDouble(txttot_amt.Value);

                    string stramt = Convert.ToString(txt_amt.Text);

                    SqlCommand mysqlcmd1 = new SqlCommand("insert into emp_apprisal_yearwise values('" + employee.CompanyId + "','" + employee.BranchId + "','" + l.EmployeeID + "','" + grade + "','" + bp_str + "','"+dat+"','" + stramt + "')", myConnection);//*********

                    mysqlcmd1.ExecuteNonQuery();
                    myConnection.Close();
                }
                allotment.Visible = true;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Saved Successfully');", true);

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
            }
        }
    }

    protected void rdo_Appraisallist_SelectedIndexChanged(object sender, EventArgs e)
    {
        grid_load();
    }
    protected void rdo_appraisalrating_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grid_appraisal.Rows)
        {
            i = row.RowIndex;
            //assigning txtbox to ddl
            Control ct_rc = (Control)grid_appraisal.Rows[i].FindControl("RatingControl1");


            Control ct = ct_rc.FindControl("txtstarts");
            TextBox dt = (TextBox)ct;
            //TextBox1.Text = dt.Text;
            //((DropDownList)grid_appraisal.Rows[i].FindControl("rdo_appraisalrating")).SelectedValue = dt.Text;

            //i = row.RowIndex;
            total = total + Convert.ToInt32((((DropDownList)grid_appraisal.Rows[i].FindControl("rdo_appraisalrating")).SelectedValue));
        }
        txttot_pts.Value = total.ToString();
        double average = Convert.ToDouble((total / (grid_appraisal.Rows.Count)));
        average = Math.Round(average, 2);
        //txttot_amt_mei.Value = Convert.ToString(average);
    }
    protected void btn_update_Click(object sender, EventArgs e)//******************************
    {
        double bp_str = 0.0;
        string grade = "";
        int gradeid = 0;
        for (i = 0; i < grid_appraisal.Rows.Count; i++)
        {
            if (s_login_role == "a")
            {
                l.BranchID = (int)ViewState["Appraisal_BranchID"];
            }

            if (s_login_role == "h")
            {
                l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            }

            l.EmployeeID = Convert.ToInt32(ddl_Employee.SelectedValue);

            l.AppraisalID = Convert.ToInt32(grid_appraisal.DataKeys[i].Value);
            l.Count1 = Convert.ToDouble(((TextBox)grid_appraisal.Rows[i].FindControl("txtslidebound")).Text);//Convert.ToInt32(((DropDownList)grid_appraisal.Rows[i].FindControl("rdo_appraisalrating")).SelectedValue);
            l.Departmentid = Convert.ToInt32(ddl_department.SelectedValue);
            l.str_fromdate = "01/01/2010"; //DateTime.Now.Date.f; //Convert.ToDateTime(txt_date.Value);
            l.AppraisalName = rdo_Appraisallist.SelectedValue;
            //l.Emp_Appraisal(l);
            string update = txt_date.Text;
            string[] update1 = update.Split('/');
            string dd1 = update1[0];
            string mm1 = update1[1];
            string yy1 = update1[2];
            string updat = mm1 + "/" + dd1 + "/" + yy1;
            qry = "update paym_Emp_Appraisalnew set ratings = " + l.Count1 + ",d_date='"+updat+"' where pn_appraisalID = '" + l.AppraisalID + "' and pn_EmployeeID = '" + l.EmployeeID + "' and pn_BranchID='" + l.BranchID + "' ";
            //lbl_Error.Text=l.EmployeeID.ToString();
            _Value = employee.fn_procappraisal(qry);
            if (_Value == "0")
            {
                if (i == 0)
                {
                    //getting basic pay from empid
                    myConnection.Open();
                    SqlCommand bp = new SqlCommand("select basic_salary from paym_employee where pn_employeeid='" + l.EmployeeID + "'", myConnection);
                    SqlDataReader bprdr = bp.ExecuteReader();
                    if (bprdr.Read())
                    {
                        bp_str = Convert.ToInt32(bprdr[0]);//getting bascic pay

                    }
                    bprdr.Close();
                    //getting gradeid from emp id
                    SqlCommand mygradeid = new SqlCommand("select pn_gradeid from paym_employee_profile1 where pn_employeeid='" + l.EmployeeID + "'", myConnection);
                    SqlDataReader gidrdr = mygradeid.ExecuteReader();
                    if (gidrdr.Read())
                    {
                        gradeid = Convert.ToInt32(gidrdr[0]);
                    }
                    gidrdr.Close();
                    //getting grade from grade id
                    SqlCommand mygrade = new SqlCommand("select v_gradename from paym_grade where pn_gradeid='" + gradeid + "'", myConnection);
                    SqlDataReader grdr = mygrade.ExecuteReader();
                    if (grdr.Read())
                    {
                        grade = grdr[0].ToString();
                    }
                    grdr.Close();

                    //inserting values   
                    //finding whether to update or insert
                    SqlCommand cmdfind = new SqlCommand("select pn_empid from emp_apprisal_yearwise where pn_empid='" + l.EmployeeID + "'", myConnection);
                    SqlDataReader rdr = cmdfind.ExecuteReader();

                    int temp = 0;
                    if (rdr.Read())
                    {
                        temp = Convert.ToInt32(rdr[0]);

                    }
                    rdr.Close();
                    if (temp == l.EmployeeID)
                    {
                        string stramt = Convert.ToString(txt_amt.Text);
                        SqlCommand mysqlcmd1 = new SqlCommand("update emp_apprisal_yearwise set date='" + updat + "',inc_amt='" + stramt + "' where pn_empid='" + l.EmployeeID + "'", myConnection);//*********
                        mysqlcmd1.ExecuteNonQuery();
                    }

                    else
                    {
                        string stramt = Convert.ToString(txt_amt.Text);
                        SqlCommand mysqlcmd2 = new SqlCommand("insert into emp_apprisal_yearwise values('" + employee.CompanyId + "','" + employee.BranchId + "','" + l.EmployeeID + "','" + grade + "','" + bp_str + "','" + updat + "','" + stramt + "')", myConnection);//*********
                        mysqlcmd2.ExecuteNonQuery();
                    }

                    myConnection.Close();
                }
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Updated Successfully');", true);


            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
            }
        }
    }

    protected void but_cal_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grid_appraisal.Rows)
        {
            i = row.RowIndex;
            tot_pts = tot_pts + Convert.ToDouble((((TextBox)grid_appraisal.Rows[i].FindControl("txtslidebound")).Text));
        }
        txttot_pts.Value = tot_pts.ToString();
        allotment.Visible = true;
        total_amount();
    }

    public void total_amount()
    {
        lbl_mode.Visible = true;
        LinkButton1.Visible = true;
        //TextBox1.Visible = true;
        txt_amt.Visible = true;
        txttot_pts.Visible = true;
        if (txttot_pts.Value != "0")
        {
            string emp = ddl_Employee.SelectedItem.Text;
            string[] emp_split = emp.Split('-');
            string code = emp_split[0];
            string wise = "", imode = "";
            int start = 0;
            int last = 0;
            double iValue = 0.0;
            int gid = 0;
            string gradename = "";
            string gradename2 = "";
            int deptid = 0;
            string incrementtype = "";

            myConnection.Open();

            cmd = new SqlCommand("select pn_GradeID,pn_DepartmentID from paym_employee_profile1 where pn_EmployeeID ='" + ddl_Employee.SelectedItem.Value + "' and pn_BranchID = '" + employee.BranchId + "' and pn_CompanyID = '" + employee.CompanyId + "'", myConnection);
            rea = cmd.ExecuteReader();
            if (rea.Read())
            {
                gid = Convert.ToInt32(rea["pn_GradeID"]);
                deptid = Convert.ToInt32(rea["pn_DepartmentID"]);
            }
            rea.Close();
            cmd = new SqlCommand("select v_gradename from paym_grade where pn_gradeid=" + gid + "", myConnection);
            rea = cmd.ExecuteReader();
            if (rea.Read())
            {
                gradename = rea["v_gradename"].ToString();
            }
            rea.Close();
            cmd = new SqlCommand("select grade_dept,inc_mode,inc_value from paym_annual_increment where (deptid='" + deptid + "' or grade_id='" + gid + "') and pn_BranchID = '" + employee.BranchId + "' and pn_CompanyID = '" + employee.CompanyId + "'", myConnection);
            rea = cmd.ExecuteReader();
            if (rea.Read())
            {
                wise = rea[0].ToString();
                imode = rea[1].ToString();
            }
            rea.Close();
            double amt = Convert.ToDouble(txttot_pts.Value);
            cmd = new SqlCommand("SELECT increment,formula_name,increment_type from app_increment where  pn_companyid='" + employee.CompanyId + "' and department='" + ddl_department.SelectedItem.Text + "' and start_point<='" + amt + "' and last_point>='" + amt + "' and grade='" + gradename + "'", myConnection);
            rea = cmd.ExecuteReader();

            if (rea.Read())
            {
                //double amt=Convert.ToDouble(rea[0]);
                txt_amt.Text = rea[0].ToString();
                formula = rea[1].ToString();
                TextBox1.Text = formula;
                incrementtype = rea[2].ToString();
            }
            rea.Close();

            Session["points"] = TextBox1.Text;

            if (imode == "Percentage")
            {
                lbl_mode.Text = "Max. Alloted Percent";
            }

            Session["dept"] = ddl_department.SelectedItem.Text;

            Session["formulaName"] = Convert.ToString(gid + "-" + deptid + "-" + ddl_Employee.SelectedItem.Value + "-" + incrementtype + "-" + txt_amt.Text);
        } 
    }

    protected void txt_date_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txt_amt_TextChanged(object sender, EventArgs e)
    {

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
       Session["points"] = TextBox1.Text;
       Response.Redirect("../Hrms_Master/Appraisal/App_Increment_New.aspx");
    }
}
