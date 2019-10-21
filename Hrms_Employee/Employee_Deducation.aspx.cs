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
                                    Load_grid();
                                    //Empty_grid();
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



                            case "u": s_form = "39";

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
            Session["emp_menu"] = 6;
            Response.Redirect("Employee_Preview.aspx");

        }

        //}
        //catch (Exception ex)
        //{
        //    Response.Cookies["Msg_Session"].Value=  "Error Occurred";

        //    Response.Redirect("Company_Home.aspx");


        //}


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

                for (int emp_edu = 0; emp_edu < grd_Deducation.Rows.Count; emp_edu++)
                {
                    pay.DeductionId = Convert.ToInt32(grd_Deducation.DataKeys[emp_edu].Value);
                    pay.Amount = Convert.ToDouble(pay.check_null(((TextBox)grd_Deducation.Rows[emp_edu].FindControl("txt_Ded_Amt")).Text));
                    // pay.Date = Convert.ToDateTime(((TextBox)grd_Deducation.Rows[emp_edu].FindControl("txt_Earn_Amt")).Text);
                    pay.d_date = employee.Convert_ToSqlDate(txt.Text);
                    pay.fromdate = DateTime.Now;
                    pay.todate = DateTime.Now;
                   // pay.periodCode = "Nov11";
                    if (((CheckBox)grd_Deducation.Rows[emp_edu].FindControl("grd_chk")).Checked == true)
                    {
                        pay.regular = 'Y';
                    }
                    else
                    {
                        pay.regular = 'N';
                    }
                    _Value = pay.Emp_Deduction(pay);
                }

            }
            else
            {

                Response.Redirect("Employee_Profile.aspx");
            }


            if (_Value != "1")
            {

                Response.Redirect("Employee_Photo.aspx");
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
                pay.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                pay.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
            }

            if (s_login_role == "h")
            {
                pay.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                 pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            }
            

            for (int emp_edu = 0; emp_edu < grd_Deducation.Rows.Count; emp_edu++)
            {
                pay.DeductionId = Convert.ToInt32(grd_Deducation.DataKeys[emp_edu].Value);
                if (pay.check_null(((TextBox)grd_Deducation.Rows[emp_edu].FindControl("txt_Ded_Amt")).Text) == "Pre-defined")
                {
                    pay.Amount = 0;
                }
                else
                {
                    pay.Amount = Convert.ToDouble(pay.check_null(((TextBox)grd_Deducation.Rows[emp_edu].FindControl("txt_Ded_Amt")).Text));
                }

                pay.d_date = employee.Convert_ToSqlDate(txt.Text);
                pay.fromdate = DateTime.Now;
                pay.todate = DateTime.Now;
                
                if (((CheckBox)grd_Deducation.Rows[emp_edu].FindControl("grd_chk")).Checked == true)
                {
                    pay.regular = 'Y';
                }
                else
                {
                    pay.regular = 'N';
                }
                _Value = pay.Emp_Deduction(pay);
            }
            //}
            //else
            //{

            //    Response.Redirect("Employee_Profile.aspx");
            //}



            if (_Value != "1")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Updated Successfully');};", true);
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

    public void Load_grid()
    {

        try
        {

            //EmployeesList = employee.fn_getEmployees(employee);

            //if (EmployeesList.Count > 0)
            //{

            emp_edu_List = pay.fn_Emp_DeductionAll(pay);

            if (emp_edu_List.Count > 0)
            {

                grd_Deducation.DataSource = emp_edu_List;
                grd_Deducation.DataBind();

                grid();
            }
            else
            {
                emp_edu_List = pay.fn_Deduction(pay);

                if (emp_edu_List.Count > 0)
                {
                    grd_Deducation.DataSource = emp_edu_List;
                    grd_Deducation.DataBind();
                }

            }


            //}

        }
        catch (Exception ex)
        {
          ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Error Occured');};", true);
        }


    }

    public void Empty_grid()
    {
        try
        {

            //EmployeesList = employee.fn_getEmployees(employee);

            //if (EmployeesList.Count > 0)
            //{

            Empty_gridList = pay.fn_GetDeduction(pay);

            if (Empty_gridList.Count > 0)
            {

                grd_Deducation.DataSource = Empty_gridList;
                grd_Deducation.DataBind();

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Create Deduction');", true);
            }


            //}

        }
        catch (Exception ex)
        {
          ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Error Occured');};", true);
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
          ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Error Occured');};", true);
        }

    }

    public void grid()
    {
        for (int grk = 0; grk < grd_Deducation.Rows.Count; grk++)
        {
            //grid Check Box

            if (emp_edu_List[grk].eligible == 'Y')
            {
                ((CheckBox)grd_Deducation.Rows[grk].FindControl("grd_chk")).Checked = true;
            }
            else
            {
                ((CheckBox)grd_Deducation.Rows[grk].FindControl("grd_chk")).Checked = false;
            }
            if (emp_edu_List[grk].regular == 'Y')
            {
                ((TextBox)grd_Deducation.Rows[grk].FindControl("txt_Ded_Amt")).Text = "Pre-defined";
                ((TextBox)grd_Deducation.Rows[grk].FindControl("txt_Ded_Amt")).Enabled = false;
                ((TextBox)grd_Deducation.Rows[grk].FindControl("txt_Ded_Amt")).Style.Add("background-color", "lightgrey");
            }
            else
            {
                ((TextBox)grd_Deducation.Rows[grk].FindControl("txt_Ded_Amt")).Enabled = true;
            }

        }

    }


    protected void grd_Deducation_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void txt_Ded_Amt_TextChanged(object sender, EventArgs e)
    {
        TextBox thisTextBox = (TextBox)sender;
        GridViewRow currentRow = (GridViewRow)thisTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;
        string s = ((TextBox)grd_Deducation.Rows[rowindex].FindControl("txt_Ded_Amt")).Text;
        double a = double.Parse(s);

        ((TextBox)grd_Deducation.Rows[rowindex].FindControl("txt_Ded_Amt")).Text = a.ToString("F2");  

    }
}


