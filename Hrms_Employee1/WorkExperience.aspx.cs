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
using ePayHrms.Candidate;
using System.Configuration;

public partial class Hrms_Employee_Default : System.Web.UI.Page
{

    Candidate c = new Candidate();
    Employee employee = new Employee();
    Company company = new Company();

    Collection<Candidate> WorkHistoryList;
    Collection<Candidate> WorkHistoryGridEditing;   
    Collection<Employee> emp_ID_List;
 
    string _Value;
    string s_login_role;
    int pr_emp, grd;
    string s_form = "";
    DataSet ds_userrights;
    string str = ConfigurationManager.ConnectionStrings["Connectionstring"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {

        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        
        if (s_login_role != "e")
        {

            
   if (Convert.ToInt32(Request.Cookies["Select_Employee"].Value) == 1)
                {

        
        c.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        c.BranchID=Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        c.EmployeeID = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
       employee.EmployeeId=Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
        pr_emp = Convert.ToInt32(Request.Cookies["Profile_Check"].Value);


        if (!IsPostBack)
        {

            if (Request.Cookies["Employee_Code_FirstLastName"].Value != "")
            {

                lbl_empcodename.Text = Request.Cookies["Employee_Code_FirstLastName"].Value;
            }
            else
            {
                lbl_empcodename.Text = "New Employee";

            }


            hSeqID.Value = "0";

            if (Request.Cookies["Profile_Check"].Value == "1")
            {

                switch (s_login_role)
                {

                    case "a": if (pr_emp == 1)
                        {                           
                            btn_update.Visible = false;
                        }
                        else
                        {                          
                            //btn_save.Visible = false;

                            btn_update.Visible = false;                                                      
                            btn_skip.Visible = false;

                            //employee.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
                            c.EmployeeID = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);

                            WorkHistoryList = c.fn_get_Employee_WorkHistory(c.EmployeeID);
                            grid_WorkHistory.DataSource = WorkHistoryList;
                            grid_WorkHistory.DataBind();

                           
                        }
                        break;

                    case "h": if (pr_emp == 1)
                        {

                                    btn_update.Visible = false;
                        }
                        else
                        {
                           ////btn_update.Visible = false;
                           btn_save.Visible = false;
                           btn_skip.Visible = false;
                           //employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

                            c.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
                            c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                            c.EmployeeID = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                            //employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                            gridworkhistoryLoad();                        

                           
                        }
                        break;


                    case "u": s_form = "35";

                        ds_userrights = company.check_Userrights(Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value), s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            if (pr_emp == 1)
                            {
                                btn_update.Visible = false;
                            }
                            else
                            {
                                btn_update.Visible = false;
                                btn_skip.Visible = false;

                                //employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                c.EmployeeID = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);

                                WorkHistoryList = c.fn_get_Employee_WorkHistory(c.EmployeeID);
                                grid_WorkHistory.DataSource = WorkHistoryList;
                                grid_WorkHistory.DataBind();


                            }
                        }
                        else
                        {

                            Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                            Response.Redirect("Employee_Preview.aspx");

                        }
                        
                        break;

                    default: Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                        Response.Redirect("~/Company_Home.aspx");
                        break;

                }

            }
            else
            {
                Session["Profile_Error"] = "Complete Your Profile To proceed Forther";

                Response.Redirect("Employee_Profile.aspx");


            }
        }
    }
    else
    {
        Session["ErrorMsg"] = "Employee should be selected";
        Response.Redirect("../Hrms_Company/Employee.aspx");     


    }
    }
    else
    {
        Session["emp_menu"] = 3;
        Response.Redirect("Employee_Preview.aspx");
    }

    }

    //protected void RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    int Index = (int)grid_WorkHistory.DataKeys[e.RowIndex].Value;
    //    c.DeleteHistory(Index);
    //    WorkHistoryList = c.fn_get_Employee_WorkHistory(1);
    //    grid_WorkHistory.DataSource = WorkHistoryList;
    //    grid_WorkHistory.DataBind();
    //}

    public void gridworkhistoryLoad()
    {
        WorkHistoryList = c.fn_get_Employee_WorkHistory(c.EmployeeID);
        grid_WorkHistory.DataSource = WorkHistoryList;
        grid_WorkHistory.DataBind();
    }
 
    protected void RowEditing(object sender, GridViewEditEventArgs e)
    {
        txtCompanyName.Disabled = true;
        try
        {

            if (pr_emp == 1)
            {
                emp_ID_List = employee.fn_get_EmployeeID(Request.Cookies["emp_Code"].Value);

                if (emp_ID_List.Count > 0)
                {

                    c.EmployeeID = Convert.ToInt32(emp_ID_List[0].EmployeeId);
                }

            }
            else
            {
                c.EmployeeID = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
            }
            


            int Index = (int)grid_WorkHistory.DataKeys[e.NewEditIndex].Value;
            WorkHistoryGridEditing = c.fn_Employee_WorkHistoryGridEdit(Index, c.EmployeeID);

            if (WorkHistoryGridEditing.Count > 0)
            {

                btn_update.Visible = true;
                btn_save.Visible = false;

                hSeqID.Value = WorkHistoryGridEditing[0].WorkHistorySeqID.ToString();
                txtCompanyName.Value = WorkHistoryGridEditing[0].CompanyName.ToString();
                txtCompanyLocation.Value = WorkHistoryGridEditing[0].CompanyLocation.ToString();

                for (int pfm = 0; pfm < ddl_PeriodFromMonth.Items.Count; pfm++)
                {
                    if (ddl_PeriodFromMonth.Items[pfm].Text == WorkHistoryGridEditing[0].WorkHistoryFromDateMonth.ToString())
                    {
                        ddl_PeriodFromMonth.SelectedIndex = pfm;

                    }
                }
                for (int ptm = 0; ptm < ddl_PeriodToMonth.Items.Count; ptm++)
                {
                    if (ddl_PeriodToMonth.Items[ptm].Text == WorkHistoryGridEditing[0].WorkHistoryToDateMonth.ToString())
                    {
                        ddl_PeriodToMonth.SelectedIndex = ptm;

                    }
                }

                for (int pfy = 0; pfy < ddl_PeriodFromYear.Items.Count; pfy++)
                {
                    if (ddl_PeriodFromYear.Items[pfy].Value == WorkHistoryGridEditing[0].WorkHistoryFromDateYear.ToString())
                    {
                        ddl_PeriodFromYear.SelectedIndex = pfy;

                    }
                }

                for (int pty = 0; pty < ddl_PeriodToYear.Items.Count; pty++)
                {
                    if (ddl_PeriodToYear.Items[pty].Value == WorkHistoryGridEditing[0].WorkHistoryToDateYear.ToString())
                    {
                        ddl_PeriodToYear.SelectedIndex = pty;

                    }
                }
                txtDesignationCode.Value = WorkHistoryGridEditing[0].DesignationCode.ToString();
                txtSalaryDrawn.Value = WorkHistoryGridEditing[0].Salary.ToString();
                txtRole.Value = WorkHistoryGridEditing[0].Role.ToString();
                txtResponsibility.Value = WorkHistoryGridEditing[0].Responsibility.ToString();
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            if (pr_emp == 1)
            {
                save_process();

            }
            else
            {
                update_process();
            }         
           
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        } 
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {
        try
        {
            c.WorkHistorySeqID = Convert.ToInt32(hSeqID.Value);
            c.WorkHistoryFromDateMonth = ddl_PeriodFromMonth.SelectedItem.Text;
            c.WorkHistoryFromDateYear = Convert.ToInt32(ddl_PeriodFromYear.Text);
            c.WorkHistoryToDateMonth = ddl_PeriodToMonth.SelectedItem.Text;
            c.WorkHistoryToDateYear = Convert.ToInt32(ddl_PeriodToYear.Text);
            c.CompanyName = txtCompanyName.Value;
            c.CompanyLocation = txtCompanyLocation.Value;
            c.DesignationCode = txtDesignationCode.Value;
            c.Salary = txtSalaryDrawn.Value;
            c.Role = txtRole.Value;
            c.Responsibility = txtResponsibility.Value;
            _Value = c.Emp_WorkHistoryUpdate(c);
            if (_Value != "1")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Updated Successfully');", true);
            }
            WorkHistoryList = c.fn_get_Employee_WorkHistory(c.EmployeeID);
            grid_WorkHistory.DataSource = WorkHistoryList;
            grid_WorkHistory.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

    protected void btn_skip_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Redirect("Employee_WorkDetails.aspx");

        }

        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }

    }


    protected void btn_Back_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Redirect("Employee_Preview.aspx");

        }

        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }


    public void save_process()
    {

        if (s_login_role == "a")
        {
            //employee.EmployeeId = 0;
            c.BranchID = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);

        }

        if (s_login_role == "h")
        {
            //employee.EmployeeId = 0;
            c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);


        }


        emp_ID_List = employee.fn_get_EmployeeID(Request.Cookies["emp_Code"].Value);

        if (emp_ID_List.Count > 0)
        {

            c.EmployeeID = Convert.ToInt32(emp_ID_List[0].EmployeeId);
            //hSeqID.Value = "0";

            save();


            if (_Value != "1")
            {
                btn_skip.Visible = true;
                Response.Redirect("Employee_WorkDetails.aspx");
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
            }

            //grd_ddl();

        }
        else
        {
            Response.Redirect("Employee_Profile.aspx");
        }


    }

    public void update_process()
    {

        if (s_login_role == "a")
        {
            c.EmployeeID = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
            c.BranchID = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);

        }

        if (s_login_role == "h")
        {
            c.EmployeeID = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
            c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        }

        save();

        if (_Value != "1")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Updated Successfully');", true);

            // btn_update.Visible = false;

            // Response.Redirect("Employee_Skills.aspx");
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }




    public void save()
    {

        //c.CompanyID = 1;
        //c.BranchID = 2;
        //c.EmployeeID = 1;   
        //WorkHistoryList = c.fn_get_Employee_WorkHistory(c.EmployeeID);
        //if(WorkHistoryList.Count>0)
        
        c.WorkHistorySeqID = Convert.ToInt32(hSeqID.Value);
        c.WorkHistoryFromDateMonth = ddl_PeriodFromMonth.SelectedItem.Text;
        c.WorkHistoryFromDateYear = Convert.ToInt32(ddl_PeriodFromYear.Text);
        c.WorkHistoryToDateMonth = ddl_PeriodToMonth.SelectedItem.Text;
        c.WorkHistoryToDateYear = Convert.ToInt32(ddl_PeriodToYear.Text);
        c.CompanyName = txtCompanyName.Value;
        c.CompanyLocation = txtCompanyLocation.Value;
        c.DesignationCode = txtDesignationCode.Value;
        c.Salary = txtSalaryDrawn.Value;
        c.Role = txtRole.Value;
        c.Responsibility = txtResponsibility.Value;
        _Value = c.Emp_WorkHistoryUpdate(c);

        hSeqID.Value = "0";      

        WorkHistoryList = c.fn_get_Employee_WorkHistory(c.EmployeeID);
        grid_WorkHistory.DataSource = WorkHistoryList;
        grid_WorkHistory.DataBind();

        ClientScriptManager clr = Page.ClientScript;
        clr.RegisterStartupScript(this.GetType(), "call", "fnNew();", true);
        btn_save.Visible = true;
        btn_update.Visible = false;
        btn_skip.Visible = false;
        //cleartext();
    }

    public void cleartext()
    {
        txtCompanyName.Value = "";
        txtCompanyLocation.Value = "";
        txtDesignationCode.Value = "";
        txtSalaryDrawn.Value = "";
        txtRole.Value = "";
        txtResponsibility.Value="";
    }

    public void grd_ddl()
    {

        for (grd = 0; grd < grid_WorkHistory.Rows.Count; grd++)
        {
            ((ImageButton)grid_WorkHistory.Rows[grd].FindControl("Edit")).Visible = false;
         
        }

    }


    protected void btn_save_con_Click(object sender, ImageClickEventArgs e)
    {
       
        try
        {

            if (s_login_role == "a")
            {
                //employee.EmployeeId = 0;
                c.BranchID = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);

            }

            if (s_login_role == "h")
            {
                //employee.EmployeeId = 0;
                c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);


            }


            emp_ID_List = employee.fn_get_EmployeeID(Request.Cookies["emp_Code"].Value);

            if (emp_ID_List.Count > 0)
            {

                c.EmployeeID = Convert.ToInt32(emp_ID_List[0].EmployeeId);

                save();

                
                if (_Value != "1")
                {
                    Response.Redirect("Employee_WorkDetails.aspx");
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
                }

            }
            else
            {

                Response.Redirect("Employee_Profile.aspx");
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }

    }
   
    protected void btn_cancel_Click(object sender, EventArgs e)
    {


        btn_update.Visible = false;
        btn_save.Visible = true;

        hSeqID.Value = "0";


        ClientScriptManager clr = Page.ClientScript;
        clr.RegisterStartupScript(this.GetType(), "call", "fnNew();", true);

    }



    protected void grid_WorkHistory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        
        using (SqlConnection con = new SqlConnection(str))
        {
            con.Open();
            string query = "delete  from paym_Employee_WorkHistory where pn_CompanyID='" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and  pn_EmployeeID='" + employee.EmployeeId + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Record Deleted');", true);
            gridworkhistoryLoad();
        }
    }
}
