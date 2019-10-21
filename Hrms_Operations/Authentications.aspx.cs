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
using ePayHrms.Leave;
using ePayHrms.User_authentication;

public partial class Hrms_Company_Default : System.Web.UI.Page
{

    Company company = new Company();
    Employee employee = new Employee();
    User__Rights user = new User__Rights();

    DataSet ds = new DataSet();

    Collection<Company> CompanyList;
    Collection<User__Rights> UserRightsList;

    string s_form = "";

    string s_login_role;
    int i = 0, j = 0;
    string emp_code = "", str = "";

    string[] arr_ahk = new string[60];


    protected void Page_Load(object sender, EventArgs e)
    {

        

        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        user.companyid = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        user.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        s_login_role = Request.Cookies["Login_temp_Role"].Value;


        if (!IsPostBack)
        {
            CompanyList = company.fn_getCompany();



            if (CompanyList.Count > 0)
            {

                if (s_login_role != "a" && s_login_role != "h")
                {
                    module2.Visible = false;
                    s_form = "47";
                    Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                    Response.Redirect("~/Company_Home.aspx");
                    //Response.Redirect("~/Company_Home.aspx");
                }

                else
                {
                    ddl_rdo_load();
                }
            }
            else
            {
                Response.Cookies["Msg_Session"].Value=  "Please Create Company";
                Response.Redirect("~/Company_Home.aspx");
            }
        }
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        j = 0;
        //All selected check box items are filled into single array...

        //Checkboxlist Home

        for (i = 0; i < chk_home.Items.Count; i++)
        {
            if (chk_home.Items[i].Selected == true)
            {
                arr_ahk[j] = chk_home.Items[i].Value;
                chk_home.Items[i].Selected = false;
                j++;

            }
        }

        //Checkboxlist Master

        for (i = 0; i < chk_master.Items.Count; i++)
        {
            if (chk_master.Items[i].Selected == true)
            {
                arr_ahk[j] = chk_master.Items[i].Value;
                chk_master.Items[i].Selected = false;
                j++;
            }
        }
        //Checkboxlist Employee

        for (i = 0; i < chk_employee.Items.Count; i++)
        {
            if (chk_employee.Items[i].Selected == true)
            {
                arr_ahk[j] = chk_employee.Items[i].Value;
                chk_employee.Items[i].Selected = false;
                j++;

            }

        }
        //Checkboxlist Task


        for (i = 0; i < chk_task.Items.Count; i++)
        {
            if (chk_task.Items[i].Selected == true)
            {
                arr_ahk[j] = chk_task.Items[i].Value;
                chk_task.Items[i].Selected = false;
                j++;

            }

        }
        //Checkboxlist Actions


        for (i = 0; i < chk_Action.Items.Count; i++)
        {
            if (chk_Action.Items[i].Selected == true)
            {
                arr_ahk[j] = chk_Action.Items[i].Value;
                chk_Action.Items[i].Selected = false;
                j++;

            }

        }
        //Checkboxlist Performance


        for (i = 0; i < chk_performance.Items.Count; i++)
        {
            if (chk_performance.Items[i].Selected == true)
            {
                arr_ahk[j] = chk_performance.Items[i].Value;
                chk_performance.Items[i].Selected = false;
                j++;

            }

        }
        for (i = 0; i < chk_attendance.Items.Count; i++)
        {
            if (chk_attendance.Items[i].Selected == true)
            {
                arr_ahk[j] = chk_attendance.Items[i].Value;
                chk_attendance.Items[i].Selected = false;
                j++;

            }

        }

        for (i = 0; i < chk_payroll.Items.Count; i++)
        {
            if (chk_payroll.Items[i].Selected == true)
            {
                arr_ahk[j] = chk_payroll.Items[i].Value;
                chk_payroll.Items[i].Selected = false;
                j++;

            }

        }
        for (i = 0; i < chk_reports.Items.Count; i++)
        {
            if (chk_reports.Items[i].Selected == true)
            {
                arr_ahk[j] = chk_reports.Items[i].Value;
                chk_reports.Items[i].Selected = false;
                j++;

            }

        }


        save();

    }

    public void save()
    {
        if (arr_ahk.Length > 0)
        {
            try
            {
                for (int h = 0; h < ddl_Employee.Items.Count; h++)
                {
                    if (ddl_Employee.Items[h].Selected)
                    {
                        user.EmployeeID = Convert.ToInt32(ddl_Employee.Items[h].Value);
                        emp_code = employee.fn_GetEmployeeCode(user.EmployeeID);
                        var conString = ConfigurationManager.ConnectionStrings["connectionstring"];
                        string constr = conString.ConnectionString;
                        SqlConnection con = new SqlConnection(constr);
                        con.Open();
                        SqlCommand cmd = new SqlCommand("delete from user_authentications where pn_employeeid='" + user.EmployeeID + "'", con);
                        cmd.ExecuteNonQuery();
                        //arr_ahk.Length

                        for (i = 0; i < j; i++)
                        {
                            user.companyid = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);

                            user.FormID = Convert.ToInt32(arr_ahk[i]);

                            user.F_Date = employee.Convert_ToSqlDate(txtFromdate.Value);
                            user.T_Date = employee.Convert_ToSqlDate(txtTodate.Value);
                            user.d_Date = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                            user.Role = 'a';
                            user.status = 'Y';
                            //user.EmpCode = "45001";
                            user.EmpCode = emp_code;

                            user.user_Authentications(user);


                        }

                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Saved Successfully');", true);

                        
                    }
                }
                un_chkAll();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);

            }

        }
        else
        {

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Atleast one form is selected');", true);

        }

    }

    public void ddl_rdo_load()
    {

        UserRightsList = user.fn_user_creation(employee.BranchId);

        if (UserRightsList.Count > 0)
        {
            ddl_Employee.DataSource = UserRightsList;
            ddl_Employee.DataValueField = "EmployeeID";
            ddl_Employee.DataTextField = "username";
            ddl_Employee.DataBind();
        }

    }

    protected void btn_details_Click(object sender, EventArgs e)
    {

        bool isAnySelected = ddl_Employee.SelectedIndex != -1;
        if (isAnySelected == true)
        {
            user.EmployeeID = Convert.ToInt32(ddl_Employee.SelectedItem.Value);
            UserRightsList = user.fn_emp_user_Authentications(user);

            if (UserRightsList.Count > 0)
            {
                txtFromdate.Value = Convert.ToString(UserRightsList[0].F_Date.ToString("dd/MM/yyyy"));
                txtTodate.Value = Convert.ToString(UserRightsList[0].T_Date.ToString("dd/MM/yyyy"));

                j = 0;

                for (i = 0; i < UserRightsList.Count; i++)
                {
                    arr_ahk[j] = Convert.ToString(UserRightsList[i].FormID);
                    j++;
                }

                //Checkboxlist Home

                for (j = 0; j < arr_ahk.Length; j++)
                {

                    for (i = 0; i < chk_home.Items.Count; i++)
                    {
                        if (arr_ahk[j] == chk_home.Items[i].Value)
                        {
                            chk_home.Items[i].Selected = true;
                        }

                    }
                }


                //Checkboxlist Master


                for (j = 0; j < arr_ahk.Length; j++)
                {

                    for (i = 0; i < chk_master.Items.Count; i++)
                    {
                        if (arr_ahk[j] == chk_master.Items[i].Value)
                        {
                            chk_master.Items[i].Selected = true;
                        }

                    }
                }


                //Checkboxlist Employee

                for (j = 0; j < arr_ahk.Length; j++)
                {

                    for (i = 0; i < chk_employee.Items.Count; i++)
                    {
                        if (arr_ahk[j] == chk_employee.Items[i].Value)
                        {
                            chk_employee.Items[i].Selected = true;
                        }

                    }
                }
                //Checkboxlist Tasks


                for (j = 0; j < arr_ahk.Length; j++)
                {

                    for (i = 0; i < chk_task.Items.Count; i++)
                    {
                        if (arr_ahk[j] == chk_task.Items[i].Value)
                        {
                            chk_task.Items[i].Selected = true;
                        }

                    }
                }
                //Checkboxlist Actions


                for (j = 0; j < arr_ahk.Length; j++)
                {

                    for (i = 0; i < chk_Action.Items.Count; i++)
                    {
                        if (arr_ahk[j] == chk_Action.Items[i].Value)
                        {
                            chk_Action.Items[i].Selected = true;
                        }

                    }
                }
                //Checkboxlist Performance


                for (j = 0; j < arr_ahk.Length; j++)
                {

                    for (i = 0; i < chk_performance.Items.Count; i++)
                    {
                        if (arr_ahk[j] == chk_performance.Items[i].Value)
                        {
                            chk_performance.Items[i].Selected = true;
                        }

                    }
                }

                for (j = 0; j < arr_ahk.Length; j++)
                {

                    for (i = 0; i < chk_attendance.Items.Count; i++)
                    {
                        if (arr_ahk[j] == chk_attendance.Items[i].Value)
                        {
                            chk_attendance.Items[i].Selected = true;
                        }

                    }
                }
                for (j = 0; j < arr_ahk.Length; j++)
                {

                    for (i = 0; i < chk_payroll.Items.Count; i++)
                    {
                        if (arr_ahk[j] == chk_payroll.Items[i].Value)
                        {
                            chk_payroll.Items[i].Selected = true;
                        }

                    }
                }
                for (j = 0; j < arr_ahk.Length; j++)
                {
                    for (i = 0; i < chk_reports.Items.Count; i++)
                    {
                        if (arr_ahk[j] == chk_reports.Items[i].Value)
                        {
                            chk_reports.Items[i].Selected = true;
                        }
                    }
                }
            }
            else
            {
                un_chkAll();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Select Employee');", true);
        }
    }

    public void un_chkAll()
    {
        txtFromdate.Value = "";
        txtTodate.Value = "";

        //Employee List

        for (i = 0; i < ddl_Employee.Items.Count; i++)
        {
            ddl_Employee.Items[i].Selected = false;

        }

        //Checkboxlist Home

        for (i = 0; i < chk_home.Items.Count; i++)
        {
            chk_home.Items[i].Selected = false;

        }

        //Checkboxlist Master


        for (i = 0; i < chk_master.Items.Count; i++)
        {
            chk_master.Items[i].Selected = false;

        }

        //Checkboxlist Employee

        for (i = 0; i < chk_employee.Items.Count; i++)
        {
            chk_employee.Items[i].Selected = false;

        }

        //Checkboxlist Tasks

        for (i = 0; i < chk_task.Items.Count; i++)
        {
            chk_task.Items[i].Selected = false;

        }

        //Checkboxlist Actons

        for (i = 0; i < chk_Action.Items.Count; i++)
        {
            chk_Action.Items[i].Selected = false;

        }

        //Checkboxlist Performance

        for (i = 0; i < chk_performance.Items.Count; i++)
        {
            chk_performance.Items[i].Selected = false;

        }
        for (i = 0; i < chk_attendance.Items.Count; i++)
        {
            chk_attendance.Items[i].Selected = false;

        }
        for (i = 0; i < chk_payroll.Items.Count; i++)
        {
            chk_payroll.Items[i].Selected = false;

        }
        for (i = 0; i < chk_reports.Items.Count; i++)
        {
            chk_reports.Items[i].Selected = false;

        }


    }


    protected void ddl_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
