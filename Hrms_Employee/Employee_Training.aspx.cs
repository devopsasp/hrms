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

public partial class Hrms_Employee_Default : System.Web.UI.Page
{    
    
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    Candidate c = new Candidate();
    Company company = new Company();
   
    Collection<Employee> emp_ID_List;
    Collection<Employee> emp_Edit;     

    int pr_emp,Index,tr;
    string _Value;
    string s_login_role;
    string s_form = "";
    DataSet ds_userrights;

    Collection<Employee> InstitutionName;
    Collection<Employee> prgmnameList;
    Collection<Employee> prgmtypList;
    Collection<Employee> TrainerName;


    protected void Page_Load(object sender, EventArgs e)
    {
        s_login_role = Request.Cookies["Login_temp_Role"].Value;


         
        //if (s_login_role != "e")
        //{

        //    if (Convert.ToInt32(Request.Cookies["Select_Employee"].Value) == 1)
        //        {

               
        

        c.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);

        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        pr_emp = Convert.ToInt32(Request.Cookies["preview_emp"].Value);
        //pr_emp = 1;

        if (!IsPostBack)
        {

            InstitutionName = employee.fn_getInstList(employee);
            ddl_InstName.DataSource = InstitutionName;
            ddl_InstName.DataValueField = "InstitutionId";
            ddl_InstName.DataTextField = "InstitutionName";
            ddl_InstName.DataBind();

            prgmnameList = employee.fn_programname(employee);
            ddl_PrgmName.DataSource = prgmnameList;
            ddl_PrgmName.DataValueField = "prgmid";
            ddl_PrgmName.DataTextField = "prgmname";
            ddl_PrgmName.DataBind();

            prgmtypList = employee.fn_programtype();
            ddl_PrgmType.DataSource = prgmtypList;
            ddl_PrgmType.DataValueField = "prgmtypId";
            ddl_PrgmType.DataTextField = "prgmtypName";
            ddl_PrgmType.DataBind();

            TrainerName = employee.fn_gettrainerNameList();
            ddl_TrainerName.DataSource = TrainerName;
            ddl_TrainerName.DataValueField = "trnrID";
            ddl_TrainerName.DataTextField = "trnrName";
            ddl_TrainerName.DataBind();

                      

            //if (Request.Cookies["Profile_Check"].Value == "1")
            //{

                switch (s_login_role)
                {

                    case "a": 
                        if (pr_emp == 1)
                        {
                            btn_update.Visible = false;

                        }
                        else
                        {
                           // btn_save.Visible = false;
                            btn_update.Visible = false;
                            btn_skip.Visible = false;
                            //employee.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
                           

                        employee.EmployeeId=Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                        grid_load();
                                                 
                       }
                        break;

                    case "h": if (pr_emp == 1)
                        {
                            btn_update.Visible = false;

                        }
                        else
                        {
                           // btn_save.Visible = false;

                            btn_update.Visible = false;

                            btn_skip.Visible = false;

                            employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                            grid_load();

                           

                        }
                        break;



                    case "u": s_form = "4";

                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            if (pr_emp == 1)
                            {
                              btn_update.Visible = false;
  
                            }
                            else
                            {
                                // btn_save.Visible = false;

                                btn_update.Visible = false;
                                btn_skip.Visible = false;

                                employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                                grid_load();
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
            //else
            //{
            //    Session["Profile_Error"] = "Complete Your Profile To proceed Forther";

            //    Response.Redirect("Employee_Profile.aspx");


            //}

    //    }

    //}
    //else
    //{
    //    Session["ErrorMsg"] = "Employee should be selected";
    //    Response.Redirect("../Hrms_Company/Employee.aspx");       

    //}

    //}
    //else
    //{
    //    Session["emp_menu"] = 7;
    //    Response.Redirect("Employee_Preview.aspx");

    //}




    }

    protected void RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            c.EmployeeID = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);

            employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
            employee.TrainingID = (int)grid_Training.DataKeys[e.NewEditIndex].Value;
           
            emp_Edit = employee.fn_Training(employee);

            if (emp_Edit.Count > 0)
            {
                btn_update.Visible = true;
                btn_save.Visible = false;

                hSeqID.Value = emp_Edit[0].TrainingID.ToString();

                txtDurationFrom.Text = emp_Edit[0].DurationFrom;
               txtDurationTo.Text = emp_Edit[0].DurationTo;
               txtsummary.Text = emp_Edit[0].temp_str;

                for (tr = 0; tr < ddl_InstName.Items.Count; tr++)
                {
                    if (ddl_InstName.Items[tr].Text == emp_Edit[0].InstitutionName)
                    {
                        ddl_InstName.SelectedIndex = tr;

                    }
                }

                for (tr = 0; tr < ddl_PrgmType.Items.Count; tr++)
                {
                    if (ddl_PrgmType.Items[tr].Text == emp_Edit[0].prgmtypName)
                    {
                        ddl_PrgmType.SelectedIndex = tr;

                    }
                }

                for (tr = 0; tr < ddl_PrgmName.Items.Count; tr++)
                {
                    if (ddl_PrgmName.Items[tr].Text == emp_Edit[0].prgmname)
                    {
                        ddl_PrgmName.SelectedIndex = tr;

                    }
                }

                for (tr = 0; tr < ddl_TrainerName.Items.Count; tr++)
                {
                    if (ddl_TrainerName.Items[tr].Text == emp_Edit[0].trnrName)
                    {
                        ddl_TrainerName.SelectedIndex = tr;

                    }
                }


            }


            //hSeqID.Value = "0";

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
                //employee.EmployeeId = 0;
                employee.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);

            }

            if (s_login_role == "h")
            {
                //employee.EmployeeId = 0;
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

            }

            hSeqID.Value = "0";


            if (Convert.ToInt32(Request.Cookies["preview_emp"].Value) == 1)
            {

                emp_ID_List = employee.fn_get_EmployeeID((string)Session["emp_Code"]);

                if (emp_ID_List.Count > 0)
                {

                    employee.EmployeeId = Convert.ToInt32(emp_ID_List[0].EmployeeId);

                    employee.TrainingID = Convert.ToInt32(hSeqID.Value);

                    save();

                    if (_Value != "1")
                    {
                        grid_load();

                        //Response.Redirect("PreviewEmployee.aspx");
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
                    }

                }

            }
            else
            {

          
                employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);

                employee.TrainingID = Convert.ToInt32(hSeqID.Value);

                save();

                if (_Value != "1")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Added Successfully');", true);
                    grid_load();

                    //Response.Redirect("PreviewEmployee.aspx");
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
                }


            }




        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);

        }



    }

    public void grid_load()
    {

        emp_Edit = employee.fn_Training_grid(employee);

        grid_Training.DataSource = emp_Edit;
        grid_Training.DataBind();


    }

    public void save()
    {
        //employee.TrainingID = 0;
        employee.DurationFrom = txtDurationFrom.Text;
        employee.DurationTo = txtDurationTo.Text;
        employee.temp_str = txtsummary.Text;
        employee.InstitutionId = Convert.ToInt32(ddl_InstName.SelectedItem.Value);
        employee.prgmtypId = Convert.ToInt32(ddl_PrgmType.SelectedItem.Value);
        employee.prgmid = Convert.ToInt32(ddl_PrgmName.SelectedItem.Value);
        employee.trnrID = Convert.ToInt32(ddl_TrainerName.SelectedItem.Value);

       _Value=employee.Employee_Training(employee);
       btn_save.Visible = true;
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

    protected void btn_skip_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Redirect("Employee_Date.aspx");

        }

        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

    protected void btn_save_con_Click(object sender, ImageClickEventArgs e)
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


            hSeqID.Value = "0";

            emp_ID_List = employee.fn_get_EmployeeID((string)Session["emp_Code"]);

            if (emp_ID_List.Count > 0)
            {

                employee.EmployeeId = Convert.ToInt32(emp_ID_List[0].EmployeeId);

                employee.TrainingID = Convert.ToInt32(hSeqID.Value);

                save();

                if (_Value != "1")
                {
                    Response.Redirect("Employee_Date.aspx");
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
                }



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
                employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                employee.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);

            }

            if (s_login_role == "h")
            {
                employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

            }

            employee.TrainingID =Convert.ToInt32(hSeqID.Value);

            save();



            if (_Value != "1")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Updated Successfully');", true);

                btn_update.Visible = false;

                //Response.Redirect("PreviewEmployee.aspx");
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
}




