using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
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
using ePayHrms.Leave;
using CrystalDecisions.Web;
using CrystalDecisions.Enterprise;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.Reporting;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using System.Configuration;

public partial class Hrms_Additional_ViewTraining : System.Web.UI.Page
{
    ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand cmd1 = new SqlCommand();
    Company company = new Company();
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    Candidate c = new Candidate();
    Leave l = new Leave();

    Collection<Company> CompanyList, ddlBranchsList;
    Collection<Employee> DepartmentList;
    Collection<Employee> EmployeeList;
    Collection<PayRoll> PayList;
    Collection<Employee> InstitutionName;
    Collection<Employee> prgmnameList;
    Collection<Employee> prgmtypList;
    Collection<Employee> TrainerName;
    string s_login_role;
    int presentday = 1;
    int i = 0, j, temp_count = 0;
    int ddl_i = 0;
    string query = "";
    DateTime from_date, to_date;
    int empid, count;
    int daycount;
    string empname, date1, date2, _Month, monthyear;
    string[] sd, ed;
    string s_form = "";
    int from_month, to_month, _Year;
    double act_basic, earned_basic, _Amount, Tot_amt;
    DateTime fromdate, todate;
    DataSet ds_userrights;
    int chk_i = 0;
    string _Value;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Session["Repordid"] = "";
        Session["fdate"] = "";
        Session["tdate"] = "";

        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        c.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        //lbl_error.Text = "";


        if (!IsPostBack)
        {
            // date_load();

            CompanyList = company.fn_getCompany();
            ListItem li = new ListItem();
            if (CompanyList.Count > 0)
            {
                switch (s_login_role)
                {
                    case "a":
                        //admin();
                        //session_check();

                        ddl_Branch.Visible = true;
                        //ddl_Branch_load();
                        break;

                    case "h":
                        tr1.Visible = true;
                        tr2.Visible = true;
                        ddl_Branch.Visible = false;
                        prgm();

                        //session_check();
                        break;

                    case "u": s_form = "79";
                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            ddl_Branch.Visible = false;
                            li = new ListItem();
                            li.Text = Request.Cookies["EmpCodeName"].Value;
                            li.Value = Request.Cookies["Login_temp_EmployeeID"].Value;
                            li.Selected = true;
                        }
                        
                        else
                        
                        {
                            Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator.";
                            Response.Redirect("~/Company_Home.aspx");
                        }
                        break;

                    case "e":
                        tr2.Visible = true;
                        employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
                        prgm1();
                        ddl_Branch.Visible = false;
                        li = new ListItem();
                        li.Text = Request.Cookies["EmpCodeName"].Value;
                        li.Value = Request.Cookies["Login_temp_EmployeeID"].Value;
                        li.Selected = true;

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
   

    public void prgm()
    {

        ddl_prgmtype.Items.Clear();
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        if (s_login_role == "a")
        {
            //prgmnameList = employee.fn_programname(Convert.ToInt32(ddl_Branch.SelectedItem.Value));
        }
        else if (s_login_role == "h")
        {
            prgmnameList = employee.fn_programname1(employee);
        }

        if (prgmnameList.Count > 0)
        {
            for (ddl_i = -1; ddl_i < prgmnameList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();
                    e_list.Text = "Select";
                    //e_list.Value = "0";
                    ddl_prgmtype.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();
                    //e_list.Value = prgmnameList[ddl_i].DepartmentId.ToString();
                    e_list.Text = prgmnameList[ddl_i].prgmname.ToString();
                    ddl_prgmtype.Items.Add(e_list);
                }
            }
        }
        else
        {

            ClientScriptManager manager = Page.ClientScript;
            manager.RegisterStartupScript(this.GetType(), "Call", "show_Error();", true);
        }
    }
    public void prgm1()
    {

        ddl_prgmtype.Items.Clear();
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        if (s_login_role == "a")
        {
            //prgmnameList = employee.fn_programname(Convert.ToInt32(ddl_Branch.SelectedItem.Value));
        }
        else if (s_login_role == "e")
        {
            prgmnameList = employee.fn_programname2(employee);
        }

        if (prgmnameList.Count > 0)
        {
            for (ddl_i = -1; ddl_i < prgmnameList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();
                    e_list.Text = "Select";
                    //e_list.Value = "0";
                    ddl_prgmtype.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();
                    //e_list.Value = prgmnameList[ddl_i].DepartmentId.ToString();
                    e_list.Text = prgmnameList[ddl_i].prgmname.ToString();
                    ddl_prgmtype.Items.Add(e_list);
                }
            }
        }
        else
        {

            ClientScriptManager manager = Page.ClientScript;
            manager.RegisterStartupScript(this.GetType(), "Call", "show_Error();", true);
        }
    }
    public void emp()
    {

        ddl_employee.Items.Clear();
        employee.prgmname = ddl_prgmtype.SelectedItem.Text;
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        if (s_login_role == "a")
        {
           // prgmnameList = employee.fn_programname1(Convert.ToInt32(ddl_Branch.SelectedItem.Value));
        }
        else if (s_login_role == "h")
        {
            prgmnameList = employee.fn_emp(employee);
        }

        if (prgmnameList.Count > 0)
        {
            for (ddl_i = -1; ddl_i < prgmnameList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();
                    e_list.Text = "Select";
                    e_list.Value = "0";
                    ddl_employee.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();
                    e_list.Value = prgmnameList[ddl_i].prgmid.ToString();
                    e_list.Text = prgmnameList[ddl_i].prgmname.ToString();
                    ddl_employee.Items.Add(e_list);
                }
            }
        }
        else
        {

            ClientScriptManager manager = Page.ClientScript;
            manager.RegisterStartupScript(this.GetType(), "Call", "show_Error();", true);
        }
    }



    public void grid_load()
    {
        employee.prgmname = ddl_prgmtype.SelectedItem.Text;
        EmployeeList = employee.fn_Training_grid1(employee);

        if (EmployeeList.Count > 0)
        {
            grid_Training.DataSource = EmployeeList;
            grid_Training.DataBind();
            grid_Training.Visible = true;
        }
        else
        {
            lbl_Error.Text = "No Data";
            grid_Training.Visible = false;
        }
    }

    public void grid()
    {
        myConnection.Open();
        employee.prgmname = ddl_prgmtype.SelectedItem.Text;
        //string _Sqlstring = "select (a.employeecode+'-'+a.Employee_First_Name) as EmployeeName,b.*,c.Ins_Name,d.fname from paym_employee a,paym_training_new b,institution_profile c,trainer_profile1 d where b.fn_trninstID=c.id and a.pn_employeeid=b.pn_employeeid and b.fn_pgmtrnrNameID=d.trainer_id and fn_pgrmName='" + employee.prgmname + "'";
        cmd1 = new SqlCommand("select a.pn_employeeid as EmployeeId,(a.employeecode+'-'+a.Employee_First_Name) as FirstName,b.TrainingID as TrainingID,c.Ins_Name as InstitutionName,d.fname as trnrName,b.Rating from paym_employee a,paym_training_new b,institution_profile c,trainer_profile1 d where b.instID=c.id and a.pn_employeeid=b.pn_employeeid and b.trainerID=d.trainer_id and b.ProgramName='" + employee.prgmname + "'", myConnection);
        SqlDataAdapter da = new SqlDataAdapter(cmd1);
        DataSet ds = new DataSet();
        da.Fill(ds);
        grid_Training.DataSource = ds;
        grid_Training.DataBind();
        int i = ds.Tables[0].Rows.Count;
        grid_Training.Rows[0].Cells[1].RowSpan = i;
        grid_Training.Rows[0].Cells[2].RowSpan = i;
        grid_Training.Rows[0].Cells[3].RowSpan = i;

        for (int ij = 1; ij < i; ij++)
        {
            grid_Training.Rows[ij].Cells[1].Visible = false;
            grid_Training.Rows[ij].Cells[2].Visible = false;
            grid_Training.Rows[ij].Cells[3].Visible = false;
        }
        myConnection.Close();

    }
    public void grid_emp()
    {
        myConnection.Open();
        employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
        employee.prgmname = ddl_prgmtype.SelectedItem.Text;
        //string _Sqlstring = "select (a.employeecode+'-'+a.Employee_First_Name) as EmployeeName,b.*,c.Ins_Name,d.fname from paym_employee a,paym_training_new b,institution_profile c,trainer_profile1 d where b.fn_trninstID=c.id and a.pn_employeeid=b.pn_employeeid and b.fn_pgmtrnrNameID=d.trainer_id and fn_pgrmName='" + employee.prgmname + "'";
        // cmd1 = new SqlCommand("select a.pn_employeeid as EmployeeId,(a.employeecode+'-'+a.Employee_First_Name) as FirstName,b.pn_TrainingID as TrainingID,c.Ins_Name as InstitutionName,d.fname as trnrName,b.Rating from paym_employee a,paym_training_new b,institution_profile c,trainer_profile1 d where b.fn_trninstID=c.id and a.pn_employeeid=b.pn_employeeid and b.fn_pgmtrnrNameID=d.trainer_id and fn_pgrmName='" + employee.prgmname + "'", myConnection);
        cmd1 = new SqlCommand("select '0' as Id,c.pn_employeeid as Pn_employeeid, a.v_feedback_ques as FeedbackQues,'' as Feedback_ans,b.fname as trnrName,'' as Rating from training_feedback a,trainer_profile1 b,paym_training_new c where c.pn_employeeid='" + employee.EmployeeId + "' and c.ProgramName='" + ddl_prgmtype.SelectedItem.Text + "' and c.TrainerId=b.trainer_id", myConnection);
        SqlDataAdapter da = new SqlDataAdapter(cmd1);
        DataSet ds = new DataSet();
        da.Fill(ds);
        grid_emp_feedback.DataSource = ds;
        grid_emp_feedback.DataBind();
        int i = ds.Tables[0].Rows.Count;
        grid_emp_feedback.Rows[0].Cells[2].RowSpan = i;
        grid_emp_feedback.Rows[0].Cells[5].RowSpan = i;

        for (int ij = 1; ij < i; ij++)
        {
            grid_emp_feedback.Rows[ij].Cells[2].Visible = false;
            grid_emp_feedback.Rows[ij].Cells[5].Visible = false;
        }
        myConnection.Close();

    }
    public void grid_emp1()
    {
        myConnection.Open();
        employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
        employee.prgmname = ddl_prgmtype.SelectedItem.Text;
        //string _Sqlstring = "select (a.employeecode+'-'+a.Employee_First_Name) as EmployeeName,b.*,c.Ins_Name,d.fname from paym_employee a,paym_training_new b,institution_profile c,trainer_profile1 d where b.fn_trninstID=c.id and a.pn_employeeid=b.pn_employeeid and b.fn_pgmtrnrNameID=d.trainer_id and fn_pgrmName='" + employee.prgmname + "'";
        // cmd1 = new SqlCommand("select a.pn_employeeid as EmployeeId,(a.employeecode+'-'+a.Employee_First_Name) as FirstName,b.pn_TrainingID as TrainingID,c.Ins_Name as InstitutionName,d.fname as trnrName,b.Rating from paym_employee a,paym_training_new b,institution_profile c,trainer_profile1 d where b.fn_trninstID=c.id and a.pn_employeeid=b.pn_employeeid and b.fn_pgmtrnrNameID=d.trainer_id and fn_pgrmName='" + employee.prgmname + "'", myConnection);
        cmd1 = new SqlCommand("select Feedback_id as Id,Pn_employeeid,Feedback_ques as FeedbackQues,Feedback_ans,Trainer_Name as trnrName,Rating from Training_Emp_Feedback where pn_employeeid='" + employee.EmployeeId + "' and pn_branchid='" + employee.BranchId + "'", myConnection);
        SqlDataAdapter da = new SqlDataAdapter(cmd1);
        DataSet ds = new DataSet();
        da.Fill(ds);
        int i = ds.Tables[0].Rows.Count;
        if (i > 0)
        {
            grid_emp_feedback.DataSource = ds;
            grid_emp_feedback.DataBind();

            grid_emp_feedback.Rows[0].Cells[2].RowSpan = i;
            grid_emp_feedback.Rows[0].Cells[5].RowSpan = i;

            for (int ij = 1; ij < i; ij++)
            {
                grid_emp_feedback.Rows[ij].Cells[2].Visible = false;
                grid_emp_feedback.Rows[ij].Cells[5].Visible = false;
            }
        }
        myConnection.Close();

    }


    protected void ddl_prgmtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (s_login_role == "h")
            {
                if (ddl_select.SelectedItem.Text == "HR Feedback")
                {
                    tr2.Visible = true;
                    tr3.Visible = false;
                    grid_Training.Visible = true;
                    grid();
                }
                else if (ddl_select.SelectedItem.Text == "Employee Feedback")
                {
                    tr2.Visible = true;
                    tr3.Visible = true;
                    grid_Training.Visible = false;
                    emp();
                }
                    
            }
            else if (s_login_role == "e")
            {
                employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
                tr1.Visible = false;
                tr3.Visible = false;
                    myConnection.Open();
                    cmd1 = new SqlCommand("select count(*) from training_emp_feedback where pgm_name='" + ddl_prgmtype.SelectedItem.Text + "'", myConnection);
                    int count = Convert.ToInt32(cmd1.ExecuteScalar());
                    myConnection.Close();
                    if (count == 0)
                    {
                        grid_emp();
                    }
                    else
                    {
                        grid_emp1();
                        ((Button)grid_emp_feedback.FooterRow.FindControl("btn_submit")).Visible = false;

                        for (int j = 0; j < grid_emp_feedback.Rows.Count; j++)
                        {
                            ((TextBox)grid_emp_feedback.Rows[j].FindControl("txt_answer")).Enabled = false;
                            ((AjaxControlToolkit.Rating)grid_emp_feedback.Rows[j].FindControl("Rating_emp")).Enabled = false;


                        }

                    }
                }
            }
                        
        
        catch (Exception ex) { }
    }

    protected void grid_Training_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grid_Training.EditIndex = e.NewEditIndex;
        grid();
    }
    protected void grid_Training_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            myConnection.Open();
            int rating = ((AjaxControlToolkit.Rating)grid_Training.Rows[e.RowIndex].FindControl("Rating2")).CurrentRating;
            cmd1 = new SqlCommand("update paym_training_new set Rating='" + rating + "'", myConnection);
            cmd1.ExecuteNonQuery();
            grid_Training.EditIndex = -1;
            myConnection.Close();
        }
        catch (Exception ex) { }
    }

    protected void grid_Training_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grid_Training.EditIndex = -1;
        grid();
    }
    protected void grid_Training_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "btnadd")
        {
            myConnection.Open();
            GridViewRow val = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
            int RowIndex = val.RowIndex;
            Label taskid = (Label)grid_Training.Rows[RowIndex].FindControl("lblCode");
            Label empid = (Label)grid_Training.Rows[RowIndex].FindControl("lblempCode");

            int rating = ((AjaxControlToolkit.Rating)grid_Training.Rows[RowIndex].FindControl("Rating1")).CurrentRating;

            cmd1 = new SqlCommand("update paym_training_new set Rating='" + rating + "' where pn_employeeid='"+ empid.Text +"' and Trainingid='" + taskid.Text + "'", myConnection);
            int c = cmd1.ExecuteNonQuery();
            if (c > 0)
            {
                lbl_Error.Text = "Updated Successfully";
            }
            myConnection.Close();
        }
    }
    protected void grid_emp_feedback_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "submit")
            {
                lbl_Error.Text = "";
                employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
                int rating1 = ((AjaxControlToolkit.Rating)grid_emp_feedback.Rows[0].FindControl("Rating_emp")).CurrentRating;
                string trainer_name = ((Label)grid_emp_feedback.Rows[0].FindControl("lbl_trnrName1")).Text;
                myConnection.Open();
                for (int i = 0; i < grid_emp_feedback.Rows.Count; i++)
                {
                    string ques = ((Label)grid_emp_feedback.Rows[i].FindControl("lbl_que")).Text;
                    string ans = ((TextBox)grid_emp_feedback.Rows[i].FindControl("txt_answer")).Text;
                    cmd1 = new SqlCommand("insert into training_emp_feedback values('" + employee.CompanyId + "','" + employee.BranchId + "','" + employee.EmployeeId + "','" + ddl_prgmtype.SelectedItem.Text + "','" + ques + "','" + ans + "','" + trainer_name + "','" + rating1 + "')", myConnection);
                    cmd1.ExecuteNonQuery();
                }
                myConnection.Close();
                lbl_Error.Text = "Submitted Sucessfully";
                grid_emp1();
                ((Button)grid_emp_feedback.FooterRow.FindControl("btn_submit")).Visible = false;
                for (int j = 0; j < grid_emp_feedback.Rows.Count; j++)
                {
                    ((TextBox)grid_emp_feedback.Rows[j].FindControl("txt_answer")).Enabled = false;
                    // ((AjaxControlToolkit.Rating)grid_emp_feedback.Rows[j].FindControl("Rating_emp")).Enabled = false;


                }

            }

        }
        catch (Exception ex)
        {
        }

    }
    protected void grid_emp_feedback_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grid_emp_feedback_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            lbl_Error.Text = "";
            grid_emp_feedback.EditIndex = e.NewEditIndex;
            grid_emp1();
            ((TextBox)grid_emp_feedback.Rows[e.NewEditIndex].FindControl("txt_answer1")).Enabled = true;
            ((AjaxControlToolkit.Rating)grid_emp_feedback.Rows[e.NewEditIndex].FindControl("Rating_emp_edit")).Enabled = true;
            ((Button)grid_emp_feedback.FooterRow.FindControl("btn_submit")).Visible = false;
        }
        catch (Exception ex) { }
    }
    protected void grid_emp_feedback_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        lbl_Error.Text = "";
        grid_emp_feedback.EditIndex = -1;
        grid_emp1();
        ((Button)grid_emp_feedback.FooterRow.FindControl("btn_submit")).Visible = false;

    }
    protected void grid_emp_feedback_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            lbl_Error.Text = "";
            myConnection.Open();
            int empid = Convert.ToInt32(((Label)grid_emp_feedback.Rows[e.RowIndex].FindControl("lbl_empid")).Text);
            int rating1 = ((AjaxControlToolkit.Rating)grid_emp_feedback.Rows[0].FindControl("Rating_emp")).CurrentRating;
            string answer = ((TextBox)grid_emp_feedback.Rows[e.RowIndex].FindControl("txt_answer1")).Text;
            int id = Convert.ToInt32(((Label)grid_emp_feedback.Rows[e.RowIndex].FindControl("lbl_id")).Text);
            cmd1 = new SqlCommand("update training_emp_feedback set Feedback_ans='" + answer + "' where pn_employeeid='" + empid + "' and Feedback_id='" + id + "'", myConnection);
            int c= cmd1.ExecuteNonQuery();
            cmd1 = new SqlCommand("update training_emp_feedback set rating='" + rating1 + "' where pn_employeeid='" + empid + "'", myConnection);
            c= cmd1.ExecuteNonQuery();
            myConnection.Close();
            lbl_Error.Text = "Updated Sucessfully";
            grid_emp_feedback.EditIndex = -1;
            grid_emp1();
            ((Button)grid_emp_feedback.FooterRow.FindControl("btn_submit")).Visible = false;
        }
        catch (Exception ex)
        {
        }
    }
    protected void ddl_select_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_select.SelectedItem.Text == "HR Feedback")
        {
            tr2.Visible = true;
            tr3.Visible = false;
            grid_Training.Visible = true;
        }
        else if (ddl_select.SelectedItem.Text == "Employee Feedback")
        {
            tr2.Visible = true;
            tr3.Visible = true;
            grid_Training.Visible = false;
            emp();
        }
    }
    protected void ddl_employee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            myConnection.Open();
            GridView1.Visible = true;
            employee.prgmname = ddl_prgmtype.SelectedItem.Text;
            //string _Sqlstring = "select (a.employeecode+'-'+a.Employee_First_Name) as EmployeeName,b.*,c.Ins_Name,d.fname from paym_employee a,paym_training_new b,institution_profile c,trainer_profile1 d where b.fn_trninstID=c.id and a.pn_employeeid=b.pn_employeeid and b.fn_pgmtrnrNameID=d.trainer_id and fn_pgrmName='" + employee.prgmname + "'";
            cmd1 = new SqlCommand("select * from Training_Emp_Feedback where pn_employeeid='" + ddl_employee.SelectedItem.Value + "' and pgm_Name='" + employee.prgmname + "'", myConnection);
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataSet ds = new DataSet();
            da.Fill(ds);
            int i = ds.Tables[0].Rows.Count;
            if (i > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Rows[0].Cells[2].RowSpan = i;

                for (int ij = 1; ij < i; ij++)
                {
                    GridView1.Rows[ij].Cells[2].Visible = false;
                } 
            }
            
            myConnection.Close();
        }
        catch (Exception ex) { }
    }
}