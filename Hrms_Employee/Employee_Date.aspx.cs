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
public partial class Hrms_Employee_Default : System.Web.UI.Page
{
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    Company company = new Company();
    Collection<Employee> EmployeesList;   
    Collection<Employee> emp_ID_List;
    Collection<Employee> EmpProfileList;
    Collection<Employee> EmpWorkList;
    int i, pr_emp;
    string _Value, default_sqldate = "01/01/1900";
    string s_login_role;
    string s_form = "";
    DataSet ds_userrights;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            s_login_role = Request.Cookies["Login_temp_Role"].Value;
            if (s_login_role != "e")
            {
                if (Convert.ToInt32(Request.Cookies["Select_Employee"].Value) == 1)
                {
                    
                    //r.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
                    //r.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                    employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
                    employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
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
                                        //employee.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
                                        //r.BranchID = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
                                        btn_update.Visible = false;
                                        //row_curexp.Visible = false;
                                        //ddl();
                                    }
                                    else
                                    {
                                        btn_save.Visible = false;
                                        btn_skip.Visible = false;
                                        //employee.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
                                        //r.BranchID = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
                                        employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                                        //ddl();
                                        admin();
                                    }
                                    break;

                                case "h": if (pr_emp == 1)
                                    {
                                        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                        r.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                        btn_update.Visible = false;
                                        //row_curexp.Visible = false;
                                        //ddl();
                                    }
                                    else
                                    {
                                        btn_save.Visible = false;
                                        btn_skip.Visible = false;
                                        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                        r.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                        employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                                        //ddl();
                                        admin();
                                    }
                                    break;

                                case "u": s_form = "37";

                                    ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);
                                    if (ds_userrights.Tables[0].Rows.Count > 0)
                                    {
                                        if (pr_emp == 1)
                                        {
                                            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                            r.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                            btn_update.Visible = false;
                                            //row_curexp.Visible = false;
                                            //ddl();
                                        }
                                        else
                                        {
                                            btn_save.Visible = false;
                                            btn_skip.Visible = false;
                                            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                            r.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                            employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                                            // ddl();
                                            admin();
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
                Session["emp_menu"] = 9;
                Response.Redirect("Employee_Preview.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Cookies["Msg_Session"].Value=  "Error Occurred";
            Response.Redirect("~/Company_Home.aspx");
        }
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                //employee.EmployeeId = 0;
                employee.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
            }
            if (s_login_role == "h")
            {
                //employee.EmployeeId = 0;
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            }
            emp_ID_List = employee.fn_get_EmployeeID((string)Session["emp_Code"]);

            if (emp_ID_List.Count > 0)
            {
            employee.EmployeeId = Convert.ToInt32(emp_ID_List[0].EmployeeId);
            save();
            }
            else
            {
                Response.Redirect("Employee_Profile.aspx");
            }
            if (_Value != "1")
            {
                Response.Redirect("Employee_Earnings.aspx");
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Error Occured');};", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Error Occured');};", true);
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
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Error Occured');};", true);
        }
    }

    protected void btn_update_Click(object sender, EventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                //employee.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
            }

            if (s_login_role == "h")
            {
                employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            }
            save();        
            if (_Value != "1")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Updated Successfully');};", true);
               // Response.Redirect("Employee_Preview.aspx");
                //Response.Redirect("PreviewEmployee.aspx");
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Error Occured');};", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Error Occured');};", true);
        }
    }   

    public void admin()
    {
        try
        {
            EmpWorkList = employee.fn_get_Emp_WorkDetails(employee);
            if (EmpWorkList.Count > 0)
            {
                ddl_retrive_Work();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Error Occured');};", true);
        }
    }

    public void ddl_retrive_Work()
    {
       try
        {
            if (EmpWorkList[0].d_probotion.ToShortDateString() != "01/01/2000")
            {
                txtprobation.Text = EmpWorkList[0].d_probotion.ToString("dd/MM/yyyy");
            }

            if (EmpWorkList[0].d_extended.ToShortDateString() != "01/01/2000")
            {
                txtextended.Text = EmpWorkList[0].d_extended.ToString("dd/MM/yyyy");
            }

            if (EmpWorkList[0].d_confirmation.ToShortDateString() != "01/01/2000")
            {
                txtconfirm.Text = EmpWorkList[0].d_confirmation.ToString("dd/MM/yyyy");
            }

            if (EmpWorkList[0].d_retirement.ToShortDateString() != "01/01/2000")
            {
                txtretire.Text = EmpWorkList[0].d_retirement.ToString("dd/MM/yyyy");
            }

            if (EmpWorkList[0].d_renue.ToShortDateString() != "01/01/2000")
            {
                txtrenew.Text = EmpWorkList[0].d_renue.ToString("dd/MM/yyyy");
            }

            if (EmpWorkList[0].d_join.ToShortDateString() != "01/01/2000")
            {
                txtjoin.Text = EmpWorkList[0].d_join.ToString("dd/MM/yyyy");
            }

            if (EmpWorkList[0].d_Offer.ToShortDateString() != "01/01/2000")
            {
                txtoffer.Text = EmpWorkList[0].d_Offer.ToString("dd/MM/yyyy");
            }

            //if (EmpWorkList[0].d_join != "")
            //{
            //    txt_curexp.Value = "";
            //}
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

    public void save()
    {
        employee.d_probotion = employee.Convert_ToSqlDate(txtprobation.Text);
        employee.d_extended = employee.Convert_ToSqlDate(txtextended.Text);
        employee.d_confirmation = employee.Convert_ToSqlDate(txtconfirm.Text);
        employee.d_retirement = employee.Convert_ToSqlDate(txtretire.Text);
        employee.d_renue = employee.Convert_ToSqlDate(txtrenew.Text);
        employee.d_join = employee.Convert_ToSqlDate(txtjoin.Text);
        employee.d_Offer = employee.Convert_ToSqlDate(txtoffer.Text);
        employee.Reason = txt_reason.Text;

        _Value = employee.Employee_WorkDetails(employee);
    }
    

   protected void btn_skip_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Employee_Earnings.aspx");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Error Occured');};", true);
        }
    }
}

