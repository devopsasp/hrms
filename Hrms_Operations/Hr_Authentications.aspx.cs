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
using System.Windows.Forms;

public partial class Hrms_Company_Default : System.Web.UI.Page
{

    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    private SqlConnection _connection;
    Company company = new Company();
    Employee employee = new Employee();
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    SqlDataReader rea;

    Collection<Company> BranchsList;
    Collection<Employee> sectionList;
    Collection<Company> CompanyList;

    int company_Id, branch_Id, valid, temp_valid = 0, check;
    string _Value;
    string s_login_role;

    string s_form = "";
    DataSet ds_userrights;

    protected void Page_Load(object sender, EventArgs e)
    {

        
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        lbl_Error.Text = "";
        Error.Text = "";

        if (!IsPostBack)
        {
            switch (s_login_role)
            {
                case "a": load();
                    break;

                case "h": load1();
                    branch_body.Visible = false;
                    //branch_header.Visible = false;
                    grid_Branch.Visible = false;
                    break;

                case "u": s_form = "8";
                    ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                    if (ds_userrights.Tables[0].Rows.Count > 0)
                    {
                        load();
                    }
                    else
                    {
                        Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator.";
                        Response.Redirect("~/Company_Home.aspx");
                    }
                    break;

                default: Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                    Response.Redirect("~/Company_Home.aspx");
                    break;
            }
        }
    }

    public void load()
    {
        sectionList = employee.fn_section();

        if (sectionList.Count > 0)
        {
            grid_sections.DataSource = sectionList;
            grid_sections.DataBind();
        }
        else
        {
            sectionList = employee.fn_EmptyDepartmentList();

            if (sectionList.Count > 0)
            {
                 grid_sections.DataSource = sectionList;
                 grid_sections.DataBind();

                ((ImageButton)grid_sections.Rows[0].FindControl("img_update")).Visible = false;
            }
        }
        BranchsList = company.fn_getBranchs();

        if (BranchsList.Count > 0)    //first branch is company
        {
            grid_Branch.DataSource = BranchsList;
            grid_Branch.DataBind();
        }
    }


    public void load1()
    {
        //Button2.Visible = false;
        ////Label1.Visible = false;
        //grid_Branch.Visible = false;
        //DepartmentList = employee.fn_Department1(employee);

        //if (DepartmentList.Count > 0)
        //{
        //    //lbl_Error.Text = DepartmentList.Count.ToString();
        //    grid_Department.DataSource = DepartmentList;
        //    grid_Department.DataBind();

        //    for (int c = 0; c < DepartmentList.Count; c++)
        //    {
        //        grid_Department.Rows[c].FindControl("chkid").Visible = false;
        //        //((HtmlInputText)grid_Department.Rows[c].FindControl("txtgrid")).Attributes.Add("style", "width:350px");
        //        //((HtmlInputText)grid_Department.Rows[c].FindControl("txtgrid")).Attributes.Add("style", "font-family: Calibri");
        //        //((HtmlInputText)grid_Department.Rows[c].FindControl("txtgrid")).Attributes.Add("style", "color: #C18685");
        //    }
        //}
        //else
        //{
        //    DepartmentList = employee.fn_EmptyDepartmentList();

        //    if (DepartmentList.Count > 0)
        //    {
        //        grid_Department.DataSource = DepartmentList;
        //        grid_Department.DataBind();

        //        ((ImageButton)grid_Department.Rows[0].FindControl("img_update")).Visible = false;
        //    }
        //}

    }


    protected void Edit(object sender, GridViewEditEventArgs e)
    {
        try
        {
            employee.DepartmentId = Convert.ToInt32(grid_sections.DataKeys[e.NewEditIndex].Value);
            employee.DepartmentName = ((HtmlInputText)grid_sections.Rows[e.NewEditIndex].FindControl("txtgrid")).Value;
            //employee.status = 'Y';
            if (employee.DepartmentName != "")
            {
                check = 0;
                //check = name_validate(employee.DepartmentName);

                if (check == 0)
                {
                    employee.fn_Update_Department(employee);

                    ((ImageButton)grid_sections.Rows[e.NewEditIndex].FindControl("img_update")).Visible = true;
                    ((ImageButton)grid_sections.Rows[e.NewEditIndex].FindControl("img_save")).Visible = false;
                    ((HtmlInputText)grid_sections.Rows[e.NewEditIndex].FindControl("txtgrid")).Disabled = true;
                }
                else
                {
                    ClientScriptManager manager = Page.ClientScript;
                    manager.RegisterStartupScript(this.GetType(), "Call", "show_message();", true);
                }
            }
            else
            {
                ClientScriptManager manager = Page.ClientScript;
                manager.RegisterStartupScript(this.GetType(), "Call", "show_Error();", true);
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    protected void Delete(object sender, GridViewDeleteEventArgs e)
    {
        //int Index = (int)AppointmentGrid.DataKeys[e.RowIndex].Value;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        //MessageBox.Show("hi");
        try
        {
            for (int j = 0; j < grid_sections.Rows.Count; j++)
            {
                GridViewRow Department_row = grid_sections.Rows[j];
                bool Department_check = ((HtmlInputCheckBox)Department_row.FindControl("Chk_Department")).Checked;

                if (Department_check)
                {
                    for (int i = 0; i < grid_Branch.Rows.Count; i++)
                    {
                        GridViewRow Branch_row = grid_Branch.Rows[i];
                        bool Branch_check = ((HtmlInputCheckBox)Branch_row.FindControl("Chk_Branch")).Checked;

                        if (Branch_check)
                        {
                            //employee.CompanyId = company_Id;
                            employee.DepartmentId = Convert.ToInt32(grid_sections.DataKeys[Department_row.RowIndex].Value);
                            employee.BranchId = Convert.ToInt32(grid_Branch.DataKeys[Branch_row.RowIndex].Value);
                            _Value = employee.Department(employee);
                            //MessageBox.Show(employee.DepartmentId.ToString());
                            if (_Value != "1")
                            {
                                Error.Text = "<font color=Blue>Added Successfully</font>";
                            }
                            else
                            {
                                Error.Text = "<font color=Red>Error Occured</font>";
                            }
                            //((CheckBox)Employee_row.FindControl("Employee_select")).BackColor = System.Drawing.Color.Blue;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    //protected void Button1_Click1(object sender, EventArgs e)
    //{


    //}

    protected void Update(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            ((HtmlInputText)grid_sections.Rows[e.RowIndex].FindControl("txtgrid")).Disabled = false;
            ((ImageButton)grid_sections.Rows[e.RowIndex].FindControl("img_save")).Visible = true;
            ((ImageButton)grid_sections.Rows[e.RowIndex].FindControl("img_update")).Visible = false;
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    public int name_validate(string m_name)
    {
        sectionList = employee.fn_Department1(employee);

        if (sectionList.Count > 0)
        {
            for (valid = 0; valid < sectionList.Count; valid++)
            {
                if (sectionList[valid].DepartmentName == m_name)
                {
                    temp_valid++;
                }
            }
        }
        return temp_valid;
    }
    //protected void Button1_Click1(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {

    //        check = name_validate(DepartmentName.Value);

    //        if (check == 0)
    //        {

    //            employee.DepartmentId = Convert.ToInt32(hDepartmentID.Value);
    //            employee.DepartmentName = DepartmentName.Value;
    //            employee.status = 'Y';
    //            _connection = con.fn_Connection();

    //            _connection.Open();
    //            cmd = new SqlCommand("select count(*) from hrmm_course", _connection);
    //            int cc = (int)cmd.ExecuteScalar();
    //            cc++;
    //            //cmd1 = new SqlCommand("select count(*) from paym_department", _connection);
    //            //int cc = (int)cmd1.ExecuteScalar();
    //            //cmd = new SqlCommand("insert into paym_department values('" + employee.CompanyId + "', '" + employee.DepartmentName + "','" + employee.status + "', '" + employee.BranchId + "')", _connection);
    //            //cmd.ExecuteNonQuery();
    //            //cmd1 = new SqlCommand("select count(*) from paym_department", _connection);
    //            //int aa = (int)cmd1.ExecuteScalar();

    //            _connection.Close();

    //            _Value = employee.DepartmentUpdate(employee);
    //            if (_Value != "1")
    //            {
    //                Error.Text = "<font color=Blue>Added Successfully</font>";
    //                _connection.Open();
    //                cmd1 = new SqlCommand("update paym_department set pn_BranchID = '" + employee.BranchId + "' where v_DepartmentName='" + employee.DepartmentName + "' and pn_BranchID is null ", _connection);
    //                cmd1.ExecuteNonQuery();
    //                _connection.Close();
    //                DepartmentName.Value = "";
    //            }
    //            else
    //            {
    //                Error.Text = "<font color=Red>Error Occured</font>";
    //            }

    //            DepartmentList = employee.fn_Department1(employee);

    //            if (DepartmentList.Count > 0)
    //            {
    //                grid_Department.DataSource = DepartmentList;
    //                grid_Department.DataBind();
    //            }
    //        }
    //        else
    //        {
    //            ClientScriptManager manager = Page.ClientScript;
    //            manager.RegisterStartupScript(this.GetType(), "Call", "show_message();", true);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lbl_Error.Text = "Error";
    //    }
    //}




    protected void Button2_Click1(object sender, EventArgs e)
    {
        try
        {
            for (int j = 0; j < grid_sections.Rows.Count; j++)
            {
                GridViewRow sections_row = grid_sections.Rows[j];
                bool sections_check = ((HtmlInputCheckBox)sections_row.FindControl("Chk_sections")).Checked;
                bool section_view = ((HtmlInputCheckBox)sections_row.FindControl("Checkbox1")).Checked;
                bool section_edit = ((HtmlInputCheckBox)sections_row.FindControl("Checkbox2")).Checked;
                bool section_delete = ((HtmlInputCheckBox)sections_row.FindControl("Checkbox3")).Checked;
                if (sections_check)
                {
                    if (section_view)
                    {
                        employee.SectionView = "Yes";
                    }
                    else
                    {
                        employee.SectionView = "No";
                    }
                    if (section_edit)
                    {
                        employee.SectionEdit = "Yes";
                    }
                    else
                    {
                        employee.SectionEdit = "No";
                    }
                    if (section_delete)
                    {
                        employee.SectionDelete = "Yes";
                    }
                    else
                    {
                        employee.SectionDelete = "No";
                    }
                    for (int i = 0; i < grid_Branch.Rows.Count; i++)
                    {
                        GridViewRow Branch_row = grid_Branch.Rows[i];
                        bool Branch_check = ((HtmlInputCheckBox)Branch_row.FindControl("Chk_Branch")).Checked;

                        if (Branch_check)
                        {
                            //employee.CompanyId = company_Id;
                            employee.SectionId = Convert.ToInt32(grid_sections.DataKeys[sections_row.RowIndex].Value);
                            employee.BranchId = Convert.ToInt32(grid_Branch.DataKeys[Branch_row.RowIndex].Value);
                            //employee.SectionName = Convert.ToString(grid_sections.Rows.ToString());
                            //MessageBox.Show(employee.SectionId.ToString());
                            //MessageBox.Show(employee.BranchId.ToString());
                            _Value = employee.Section(employee);

                            if (_Value != "1")
                            {
                                Error.Text = "<font color=Blue>Added Successfully</font>";
                            }
                            else
                            {
                                Error.Text = "<font color=Red>Error Occured</font>";
                            }
                            //((CheckBox)Employee_row.FindControl("Employee_select")).BackColor = System.Drawing.Color.Blue;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }
}