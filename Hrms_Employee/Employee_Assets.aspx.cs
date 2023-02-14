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

public partial class Hrms_Employee_Employee_Assets : System.Web.UI.Page
{
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    Company company = new Company();

    Collection<Employee> EmployeesList;
    Collection<Employee> emp_edu_List;
    Collection<Employee> emp_ID_List;

    int pr_emp, ddl_ex, grd, _lbl;
    string _Value;
    string s_login_role;
    string s_form = "";
    DataSet ds_userrights;


    protected void Page_Load(object sender, EventArgs e)
    {

        
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        if (s_login_role != "e")
        {
            if (Convert.ToInt32(Request.Cookies["Select_Employee"].Value) == 1)
            {
                

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
                                    ddl();
                                    admin();
                                    grd_ddl();
                                    Lbl();
                                    btn_update.Visible = false;
                                }
                                else
                                {
                                    ddl();
                                    btn_save.Visible = false;
                                    btn_skip.Visible = false;
                                    //employee.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
                                    //employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                                    admin();
                                    grd_ddl();
                                    Lbl();
                                }
                                break;

                            case "h": if (pr_emp == 1)
                                {
                                    ddl();
                                    btn_update.Visible = false;
                                }
                                else
                                {
                                    ddl();
                                    btn_save.Visible = false;
                                    btn_skip.Visible = false;
                                    employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                    employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                                    admin();
                                    grd_ddl();
                                    Lbl();
                                }
                                break;

                            case "u": s_form = "34";

                                ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                                if (ds_userrights.Tables[0].Rows.Count > 0)
                                {
                                    if (pr_emp == 1)
                                    {
                                        ddl();
                                        btn_update.Visible = false;
                                    }
                                    else
                                    {
                                        ddl();
                                        btn_save.Visible = false;
                                        btn_skip.Visible = false;
                                        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                        employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                                        admin();
                                        grd_ddl();
                                        Lbl();
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
            Session["emp_menu"] = 2;
            Response.Redirect("Employee_Preview.aspx");

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

                for (int emp_edu = 0; emp_edu <grid_emp_Asset.Rows.Count; emp_edu++)
                {
                    employee.AssetId = Convert.ToInt32(grid_emp_Asset.DataKeys[emp_edu].Value);
                    employee.AssetNo = ((DropDownList)grid_emp_Asset.Rows[emp_edu].FindControl("ddl_assetno")).SelectedItem.Text;

                    //employee.Experience = ((DropDownList)grid_emp_Asset.Rows[emp_edu].FindControl("ddl_Experience")).SelectedItem.Value;
                    //employee.Proficiency = ((DropDownList)grid_emp_Asset.Rows[emp_edu].FindControl("ddl_Proficiency")).SelectedItem.Text;

                    _Value = employee.Employee_Asset(employee);

                }

            }
            else
            {

                Response.Redirect("Employee_Profile.aspx");
            }





            if (_Value != "1")
            {
                Response.Redirect("WorkExperience.aspx");
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

    protected void btn_update_Click(object sender, EventArgs e)
    {
        try
        {

            if (s_login_role == "a")
            {
                //employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                //employee.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);

            }

            if (s_login_role == "h")
            {
                employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

            }


            //emp_ID_List = employee.fn_get_EmployeeID((string)Session["emp_Code"]);

            //if (emp_ID_List.Count > 0)
            //{ 

            //    employee.EmployeeId = Convert.ToInt32(emp_ID_List[0].EmployeeId);

            for (int emp_edu = 0; emp_edu < grid_emp_Asset.Rows.Count; emp_edu++)
            {
                employee.AssetId = Convert.ToInt32(grid_emp_Asset.DataKeys[emp_edu].Value);
                employee.AssetNo =((DropDownList)grid_emp_Asset.Rows[emp_edu].FindControl("ddl_assetno")).SelectedItem.Text;
                //employee.Proficiency = ((DropDownList)grid_emp_Asset.Rows[emp_edu].FindControl("ddl_Proficiency")).SelectedItem.Text;

                _Value = employee.Employee_Asset(employee);

            }


            //}
            //else
            //{

            //    Response.Redirect("Employee_Profile.aspx");
            //}



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

    public void admin()
    {

        try
        {

            //EmployeesList = employee.fn_getEmployees(employee);

            // if (EmployeesList.Count > 0)
            //{

            emp_edu_List = employee.fn_getEmployee_Asset(employee);

            if (emp_edu_List.Count > 0)
            {

                grid_emp_Asset.DataSource = emp_edu_List;
                grid_emp_Asset.DataBind();

            }



            //}

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }


    }

    public void ddl()
    {
        try
        {

          Collection<Employee> AssetList = employee.fn_getAssetList1(employee.BranchId);
            //Collection<Employee> SpecializationList = employee.fn_getSpecializationList();
           list_asset.DataSource = AssetList;
           list_asset.DataValueField = "AssetId";
           list_asset.DataTextField = "AssetName";
           list_asset.DataBind();
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

    protected void btn_Assets_Click(object sender, EventArgs e)
    {

        try
        {

            string list_s = "(";
            int list_i, list_temp = 0;

            for (list_i = 0; list_i <list_asset.Items.Count; list_i++)
            {
                if (list_asset.Items[list_i].Selected)
                {
                    if (list_temp == 0)
                    {
                        list_s = list_s + list_asset.Items[list_i].Value;
                        list_temp++;
                    }
                    else
                    {
                        list_s = list_s + "," + list_asset.Items[list_i].Value;
                    }
                }
            }


            list_s = list_s + ")";

            //r.temp_string = list_s;
            employee.temp_str = list_s;

            Collection<Employee> educationList = employee.fn_Assets(employee);

            grid_emp_Asset.DataSource = educationList;
            grid_emp_Asset.DataBind();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }

    }

    public void grd_ddl()
    {

        for (grd = 0; grd < grid_emp_Asset.Rows.Count; grd++)
        {
            for (ddl_ex = 0; ddl_ex < ((DropDownList)grid_emp_Asset.Rows[grd].FindControl("ddl_assetno")).Items.Count; ddl_ex++)
            {
                if (((DropDownList)grid_emp_Asset.Rows[grd].FindControl("ddl_assetno")).Items[ddl_ex].Text == emp_edu_List[grd].AssetNo)
                {
                    ((DropDownList)grid_emp_Asset.Rows[grd].FindControl("ddl_assetno")).SelectedIndex = ddl_ex;
                }
            }
        }
    }

    public void Lbl()
    {

        for (grd = 0; grd < emp_edu_List.Count; grd++)
        {

            for (_lbl = 0; _lbl < list_asset.Items.Count; _lbl++)
            {
                if (list_asset.Items[_lbl].Value == emp_edu_List[grd].AssetId.ToString())
                {
                    list_asset.Items[_lbl].Selected = true;
                }

            }

        }


    }
}
