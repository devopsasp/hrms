using System;
using System.Collections.Generic;
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
using System.Data;

public partial class Hrms_Employee_Employee_OverHeadingCost : System.Web.UI.Page
{
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    Company company = new Company();

    Collection<Employee> emp_ID_List;
    Collection<Employee> EmployeesList;

    Collection<PayRoll> emp_edu_List;
    Collection<PayRoll> Empty_gridList;

    int pr_emp;
    string _Value;
    string s_login_role;
    string s_form = "";
    DataSet ds_userrights;
    protected void Page_Load(object sender, EventArgs e)
    {
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        txt.Text = DateTime.Now.ToShortDateString();

        if (s_login_role != "e")
        {


            if (Convert.ToInt32(Request.Cookies["Select_Employee"].Value) == 1)
            {
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
                pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
                 pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                pr_emp = Convert.ToInt32(Request.Cookies["preview_emp"].Value);

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

                    if (Request.Cookies["Profile_Check"].Value == "1")
                    {

                        switch (s_login_role)
                        {

                            case "a": if (pr_emp == 1)
                                {
                                    Empty_grid();
                                    btn_update.Visible = false;
                                }
                                else
                                {
                                    btn_skip.Visible = false;
                                    btn_save.Visible = false;
                                    pay.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
                                    pay.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                                    Load_grid();
                                }
                                break;

                            case "h": if (pr_emp == 1)
                                {
                                    Empty_grid();
                                    employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                    btn_update.Visible = false;
                                }
                                else
                                {
                                    btn_skip.Visible = false;
                                    btn_save.Visible = false;
                                     pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                    pay.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                                    Load_grid();
                                }
                                break;


                            case "u": s_form = "38";

                                ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                                if (ds_userrights.Tables[0].Rows.Count > 0)
                                {
                                    if (pr_emp == 1)
                                    {
                                        Empty_grid();
                                        btn_update.Visible = false;
                                    }
                                    else
                                    {
                                        btn_skip.Visible = false;
                                        btn_save.Visible = false;
                                         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                        pay.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                                        Load_grid();
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
            Session["emp_menu"] = 10;
            Response.Redirect("Employee_Preview.aspx");

        }
    }
    public void Empty_grid()
    {
        try
        {
            Empty_gridList = pay.fn_Overheading1(employee.BranchId);

            if (Empty_gridList.Count > 0)
            {

                grid_OverHeading.DataSource = Empty_gridList;
                grid_OverHeading.DataBind();

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Create Over Headings in master');", true);

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }


    }

   
    public void Load_grid()
    {

        try
        {

            emp_edu_List = pay.fn_Emp_OverHeading(pay);

            if (emp_edu_List.Count > 0)
            {

                grid_OverHeading.DataSource = emp_edu_List;
                grid_OverHeading.DataBind();

               // grid();
            }
            else
            {
                emp_edu_List = pay.fn_Overheading1(pay.BranchId);

                if (emp_edu_List.Count > 0)
                {
                    grid_OverHeading.DataSource = emp_edu_List;
                    grid_OverHeading.DataBind();

                }

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

    protected void grid_Earnings_SelectedIndexChanged(object sender, EventArgs e)
    {

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
    protected void btn_skip_Click1(object sender, EventArgs e)
    {
        try
        {

            Response.Redirect("Employee_Photo.aspx");

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

            if (s_login_role == "a")
            {
                //pay.EmployeeId = 0;
                pay.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);

            }

            if (s_login_role == "h")
            {
                //pay.EmployeeId = 0;
                 pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);


            }


            emp_ID_List = employee.fn_get_EmployeeID((string)Session["emp_Code"]);

            if (emp_ID_List.Count > 0)
            {

                pay.EmployeeId = Convert.ToInt32(emp_ID_List[0].EmployeeId);

                for (int emp_edu = 0; emp_edu < grid_OverHeading.Rows.Count; emp_edu++)
                {
                    pay.Overheadingid = Convert.ToInt32(pay.check_null(((Label)grid_OverHeading.Rows[emp_edu].FindControl("lblid")).Text));
                    pay.Amount = Convert.ToInt32(pay.check_null(((HtmlInputText)grid_OverHeading.Rows[emp_edu].FindControl("txt_Earn_Amt")).Value));
                    //pay.Date =Convert.ToDateTime(((TextBox)grid_Earnings.Rows[emp_edu].FindControl("txt_Earn_Amt")).Text);
                    pay.d_date = employee.Convert_ToSqlDate(txt.Text);
                    pay.regular = 'Y' ;

                    _Value = pay.Emp_OverHeading(pay);

                }

            }
            else
            {

                Response.Redirect("Employee_Photo.aspx");
            }





            if (_Value != "1")
            {
                Response.Redirect("Employee_Photo.aspx");
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
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

            if (s_login_role == "a")
            {
                pay.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                pay.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);

            }

            if (s_login_role == "h")
            {
                pay.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                 pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

            }
                

            for (int emp_edu = 0; emp_edu < grid_OverHeading.Rows.Count; emp_edu++)
            {

                pay.Overheadingid = Convert.ToInt32(pay.check_null(((Label)grid_OverHeading.Rows[emp_edu].FindControl("lblid")).Text));
                pay.Amount = Convert.ToInt32(pay.check_null(((HtmlInputText)grid_OverHeading.Rows[emp_edu].FindControl("txt_Earn_Amt")).Value));
                //pay.Date =Convert.ToDateTime(((TextBox)grid_Earnings.Rows[emp_edu].FindControl("txt_Earn_Amt")).Text);
                pay.d_date = employee.Convert_ToSqlDate(txt.Text);
                pay.regular = 'Y';

                _Value = pay.Emp_OverHeading(pay);

            }


            if (_Value != "1")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Updated Successfully');", true);
          
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
            }
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

            Response.Redirect("Employee_Photo.aspx");

        }

        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }
}