using System;
using System.Data;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ePayHrms.Candidate;
using ePayHrms.Company;
using System.Collections.ObjectModel;
using ePayHrms.BE.Recruitment;
using ePayHrms.Employee;

public partial class Hrms_Additional_Default : System.Web.UI.Page
{
    private SqlConnection _connection;
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    Candidate c = new Candidate();
    Company company = new Company();

    Collection<Employee> emp_ID_List, EmployeeList;
    Collection<Employee> emp_Edit;
    Collection<Company> ddlBranchsList, CompanyList;

    int pr_emp,Index,tr, chk_i;
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
        
        lbl_Error.Text = "";

        s_login_role = Request.Cookies["Login_temp_Role"].Value;

        

        c.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);

        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        //pr_emp = Convert.ToInt32(Request.Cookies["preview_emp"].Value);
        //pr_emp = 1;
        btn_save_con.Visible = false;

        if (!IsPostBack)
        {
            row_emp.Visible = false;
            tbl_details.Visible = false;
            row_ddlemp.Visible = false;
            row_grid.Visible = false;
            row_type.Visible = false;
            row_button.Visible = false;
            
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

            prgmtypList = employee.fn_programtypes(employee);
            ddl_PrgmType.DataSource = prgmtypList;
            ddl_PrgmType.DataValueField = "prgmtypId";
            ddl_PrgmType.DataTextField = "prgmtypName";
            ddl_PrgmType.DataBind();

            TrainerName = employee.fn_gettrainerNameList1(employee);
            ddl_TrainerName.DataSource = TrainerName;
            ddl_TrainerName.DataValueField = "trnrID";
            ddl_TrainerName.DataTextField = "trnrName";
            ddl_TrainerName.DataBind();

            //if (Request.Cookies["Profile_Check"].Value == "1")
            //{
            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {
                switch (s_login_role)
                {
                    case "a":
                        btn_update.Visible = false;
                        btn_save_con.Visible = false;
                        btn_skip.Visible = false;
                        ddl_Branch_load();
                        break;

                    case "h":
                        btn_update.Visible = false;
                        btn_save_con.Visible = false;
                        btn_skip.Visible = false;
                        ddl_Branch.Visible = false;
                        row_type.Visible = true;
                        break;

                    case "u": //s_form = "4";
                        s_form = "42";ddl_Branch.Visible = false;
                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            //if (pr_emp == 1)
                            //{
                            //    btn_update.Visible = false;
                            //}
                            //else
                            //{
                            //    // btn_save.Visible = false;
                            //    btn_update.Visible = false;
                            //    btn_save_con.Visible = false;
                            //    btn_skip.Visible = false;
                            //    //employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                            //    //grid_load();
                            //}
                            ddl_Branch.Visible = false;
                            tbl_selection.Visible = true;
                            row_type.Visible = true;
                        }
                        else
                        {
                            Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                            Response.Redirect("~/Company_Home.aspx");
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

            //}bysan
            //else
            //{
            //    Session["Profile_Error"] = "Complete Your Profile To proceed Forther";
            //    Response.Redirect("Employee_Profile.aspx");
            //}
       }
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
            //c.EmployeeID = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);

            //employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);

            c.EmployeeID = Convert.ToInt32(ddl_employeelist.SelectedValue);

            employee.EmployeeId = Convert.ToInt32(ddl_employeelist.SelectedValue);

            employee.TrainingID = (int)grid_Training.DataKeys[e.NewEditIndex].Value;
           
            emp_Edit = employee.fn_Training(employee);

            if (emp_Edit.Count > 0)
            {
                tbl_details.Visible = true;
                btn_update.Visible = true;

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
            lbl_Error.Text = "Error";
        }
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                //employee.EmployeeId = 0;
                employee.BranchId = (int)ViewState["Training_BranchID"];
            }

            if (s_login_role == "h")
            {
                //employee.EmployeeId = 0;
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            }

            hSeqID.Value = "0";
            //if (Convert.ToInt32(Request.Cookies["preview_emp"].Value) == 1)
            //{
            //    emp_ID_List = employee.fn_get_EmployeeID(Request.Cookies["emp_Code"].Value);
            //    if (emp_ID_List.Count > 0)
            //    {
            //        employee.EmployeeId = Convert.ToInt32(emp_ID_List[0].EmployeeId);
            //        employee.TrainingID = Convert.ToInt32(hSeqID.Value);
            //        save();
            //        if (_Value != "1")
            //        {
            //            lbl_Error.Text = "<font color=Blue>Added Successfully</font>";
            //            //grid_load();
            //            //Response.Redirect("PreviewEmployee.aspx");
            //        }
            //        else
            //        {
            //            lbl_Error.Text = "<font color=Red>Error Occured</font>";
            //        }
            //    }
            //}
            //else
            //{
            for (chk_i = 0; chk_i < chk_Master.Items.Count; chk_i++)
            {
                if (chk_Master.Items[chk_i].Selected == true)
                {
                    employee.EmployeeId = Convert.ToInt32(chk_Master.Items[chk_i].Value);
                    employee.TrainingID = Convert.ToInt32(hSeqID.Value);
                    save();
                }
            }
                if (_Value != "1")
                {
                    lbl_Error.Text = "<font color=Blue>Added Successfully</font>";
                    //grid_load();
                    //Response.Redirect("PreviewEmployee.aspx");
                }
                else
                {
                    lbl_Error.Text = "<font color=Red>Error Occured</font>";
                }
            //}
        }
        catch (Exception ex)
        {
           lbl_Error.Text = "Error";
        }
    }

    public void grid_load()
    {
        employee.EmployeeId = Convert.ToInt32(ddl_employeelist.SelectedValue);
        emp_Edit = employee.fn_Training_grid(employee);

        if (emp_Edit.Count > 0)
        {
            grid_Training.DataSource = emp_Edit;
            grid_Training.DataBind();
            grid_Training.Visible = true;
        }
        else
        {
            lbl_Error.Text = "No Data";
            grid_Training.Visible = false;
        }
    }

    public void save()
    {
        //employee.TrainingID = 0;
        employee.DurationFrom = txtDurationFrom.Text;
        employee.DurationTo = txtDurationTo.Text;
        employee.temp_str = txtsummary.Text;
        employee.InstitutionId = Convert.ToInt32(ddl_InstName.SelectedItem.Value);
        employee.prgmtypName = ddl_PrgmType.SelectedItem.Text;
        employee.prgmname = ddl_PrgmName.SelectedItem.Text;
        employee.trnrID = Convert.ToInt32(ddl_TrainerName.SelectedItem.Value);

       _Value=employee.Employee_Training(employee);
        
    }

    protected void btn_Back_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/Company_Home.aspx");
            //Response.Redirect("../Hrms_Company/Employee.aspx");bysan
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    protected void btn_skip_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../Hrms_Employee/Employee_Date.aspx");
        }

        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    protected void btn_save_con_Click(object sender, EventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                employee.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
            }

            if (s_login_role == "h")
            {
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            }

            hSeqID.Value = "0";
            emp_ID_List = employee.fn_get_EmployeeID(Request.Cookies["emp_Code"].Value);

            if (emp_ID_List.Count > 0)
            {
                employee.EmployeeId = Convert.ToInt32(emp_ID_List[0].EmployeeId);
                employee.TrainingID = Convert.ToInt32(hSeqID.Value);

                save();

                if (_Value != "1")
                {
                    lbl_Error.Text = "<font color=Blue>Added Successfully</font>";
                    Response.Redirect("Employee_Date.aspx");
                }
                else
                {
                    lbl_Error.Text = "<font color=Red>Error Occured</font>";
                }
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    protected void btn_update_Click(object sender, EventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                employee.EmployeeId = Convert.ToInt32(ddl_employeelist.SelectedValue);
                employee.BranchId = (int)ViewState["Training_BranchID"];
            }

            if (s_login_role == "h")
            {
                employee.EmployeeId = Convert.ToInt32(ddl_employeelist.SelectedValue);
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            }

            employee.TrainingID =Convert.ToInt32(hSeqID.Value);
            save();

            if (_Value != "1")
            {
                lbl_Error.Text = "<font color=Blue>Updated Successfully</font>";
                btn_update.Visible = false;
                tbl_details.Visible = false;
                //Response.Redirect("PreviewEmployee.aspx");
                grid_load();
            }
            else
            {
                lbl_Error.Text = "<font color=Red>Error Occured</font>";
            }

        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    protected void ddl_typelist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_typelist.SelectedValue != "st")
        {
            if (ddl_typelist.SelectedValue == "add")
            {
                row_emp.Visible = true;
                tbl_details.Visible = true;
                row_ddlemp.Visible = false;
                row_grid.Visible = false;
                btn_save.Visible = true;
                employee_load();
                row_button.Visible = true;
                btn_update.Visible = false;
            }
            else if (ddl_typelist.SelectedValue == "view")
            {
                row_ddlemp.Visible = true;
                row_grid.Visible = true;
                row_emp.Visible = false;
                tbl_details.Visible = false;
                row_button.Visible = false;
                employee_load();
            }
        }
        else
        {
            row_emp.Visible = false;
            tbl_details.Visible = false;
            row_ddlemp.Visible = false;
            row_grid.Visible = false;
            row_button.Visible = false;
        }
    }

    public void ddl_Branch_load()
    {
        int ddl_i;
        //branck dropdown
        ddlBranchsList = company.fn_getBranchs();
        if (ddlBranchsList.Count > 0)
        {
            for (ddl_i = -1; ddl_i < ddlBranchsList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem list = new ListItem();

                    list.Text = "Select Branch";
                    list.Value = "0";
                    ddl_Branch.Items.Add(list);
                }
                else
                {
                    ListItem list = new ListItem();

                    list.Text = ddlBranchsList[ddl_i].CompanyName;
                    list.Value = ddlBranchsList[ddl_i].CompanyId.ToString();
                    ddl_Branch.Items.Add(list);
                }
            }
        }
    }
    protected void ddl_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Branch.SelectedValue != "0")
        {
            ViewState["Training_BranchID"] = Convert.ToInt32(ddl_Branch.SelectedValue);
            tbl_selection.Visible = true;
            row_type.Visible = true;
            ddl_typelist.SelectedValue = "st";
            row_ddlemp.Visible = false;
            row_emp.Visible = false;
            tbl_details.Visible = false;
            grid_Training.Visible = false;
            row_button.Visible = false;
            //tbl_details.Visible = true;
            //row_button.Visible = true;
        }
        else
        {
            tbl_selection.Visible = false;
            tbl_details.Visible = false;
            row_button.Visible = false;
        }
    }

    public void employee_load()
    {
        if (s_login_role == "a")
        {
            employee.BranchId = (int)ViewState["Training_BranchID"];
        }

        if (s_login_role == "h")
        {
            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        }
        EmployeeList = employee.fn_getEmployeeList(employee);

        if (EmployeeList.Count > 0)
        {
            ddl_employeelist.Items.Clear();
            for (int ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem list = new ListItem();
                    list.Text = "Select Employee";
                    list.Value = "0";
                    ddl_employeelist.Items.Add(list);
                }
                else
                {
                    ListItem list = new ListItem();
                    list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
                    list.Text = EmployeeList[ddl_i].LastName.ToString();
                    ddl_employeelist.Items.Add(list);
                    //chk_Master.Items.Add(list);
                }
            }

            chk_Master.DataSource = EmployeeList;
            chk_Master.DataTextField = "LastName";
            chk_Master.DataValueField = "EmployeeId";
            chk_Master.DataBind();

            //ddl_employeelist.DataSource = EmployeeList;
            //ddl_employeelist.DataTextField = "LastName";
            //ddl_employeelist.DataValueField = "EmployeeId";
            //ddl_employeelist.DataBind();
        }
        else
        {
            lbl_Error.Text = "No employees";
            tbl_details.Visible = false;
            row_grid.Visible = false;
            row_ddlemp.Visible = false;
            row_emp.Visible = false;
            btn_save.Visible = false;
        }
    }
    protected void ddl_employeelist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_employeelist.SelectedValue != "0")
        {
            grid_load();
            row_button.Visible = true;
            btn_save.Visible = false;
            btn_update.Visible = false;
            tbl_details.Visible = false;

        }
        else
        {
            tbl_details.Visible = false;
            grid_Training.Visible = false;
            row_button.Visible = false;
            btn_save.Visible = false;
            btn_update.Visible = false;
        }
    }
    protected void ddl_InstName_SelectedIndexChanged(object sender, EventArgs e)
    {
        prgmnameList = employee.fn_programname(employee);
        ddl_PrgmName.DataSource = prgmnameList;
        //ddl_PrgmName.DataValueField = "prgmid";
        ddl_PrgmName.DataTextField = "prgmname";
        ddl_PrgmName.DataBind();
    }
    protected void grid_Training_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            GridViewRow val = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
            int RowIndex = val.RowIndex;
            Label taskid = (Label)grid_Training.Rows[RowIndex].FindControl("lbltraing_id");

            int rating = ((AjaxControlToolkit.Rating)grid_Training.Rows[RowIndex].FindControl("Rating1")).CurrentRating;

            string query = "Update paym_Training_New set rating=" + rating + " where pn_TrainingID=" + taskid.Text + "";

            SqlCommand myCommand = new SqlCommand(query, myConnection);

            myConnection.Open();

            myCommand.ExecuteNonQuery();

            myConnection.Close();

            grid_load();
        }
    }


    
}





