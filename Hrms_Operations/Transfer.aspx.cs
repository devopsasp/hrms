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


public partial class Hrms_Company_Default : System.Web.UI.Page
{
    Company company = new Company();
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();

    PayRoll pay = new PayRoll();
    Leave leave = new Leave();

    DataSet ds_check_Employee = new DataSet();
    DataSet ds_count = new DataSet();

    Collection<Company> CompanyList;
    Collection<Company> ddlBranchsList;
    Collection<Employee> EmployeeList;
    Collection<Be_Recruitment> CheckList;
  
  
    string s_login_role;
    int ddl_i, cs_k=0,i;
    string str_query = "", str_update = "", master_up = "", user_id, password;
    string s_form = "";
    DataSet ds_userrights;

    protected void Page_Load(object sender, EventArgs e)
    {
        s_login_role = Request.Cookies["Login_temp_Role"].Value;

        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        r.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);

        lbl_Error.Text = "";
        lbl_confirmation.Text = "Are you Sure you want to Transfer?";

        if (!IsPostBack)
        {

        CompanyList = company.fn_getCompany();
        if (CompanyList.Count > 0)
        {

            if (s_login_role == "a")
            {              
                   invisible();
                   Branch_Load(ddl_frombranch);
               
            }
            //else if (s_login_role == "u")
            //{
            //      s_form = "Employee";

            //        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

            //        if (ds_userrights.Tables[0].Rows.Count > 0)
            //        {
            //            invisible();
            //            Branch_Load(ddl_frombranch);
            //        }
            //        else
            //        {
            //            Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
            //            Response.Redirect("Company_Home.aspx");
            //        }

            //}
            else
            {
                Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                Response.Redirect("~/Company_Home.aspx");
            }

        }
        else
        {
            Response.Cookies["Msg_Session"].Value = "Create Company";
            Response.Redirect("Company_Home.aspx");
        }


    }


    }


    public void Branch_Load(DropDownList ddl)
    {
        try
        {
            ddl.Items.Clear();

            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {

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
                            ddl.Items.Add(list);
                        }
                        else
                        {

                            ListItem list = new ListItem();

                            list.Text = ddlBranchsList[ddl_i].CompanyName;
                            list.Value = ddlBranchsList[ddl_i].CompanyId.ToString();
                            ddl.Items.Add(list);

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

    public void Employee_Load()
    {
        try
        {
            chk_Empcode.Items.Clear();            

            EmployeeList = employee.fn_getEmployeeList(employee);

            if (EmployeeList.Count > 0)
            {
                row_chkemployee.Visible = true;
                chk_Empcode.DataSource = EmployeeList;
                chk_Empcode.DataTextField = "LastName";
                chk_Empcode.DataValueField = "EmployeeId";
                chk_Empcode.DataBind();

                Branch_Load(ddl_tobranch);
                row_tobranch.Visible = true;
            }
            else
            {
                lbl_Error.Text = "No Employees";

            }           

        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";


        }


    }

    public void Master_Employee_Load()
    {
        try
        {

            chk_Empcode.Items.Clear();

            EmployeeList = employee.fn_query_transwer(str_query);

            if (EmployeeList.Count > 0)
            {
                chk_Empcode.DataSource = EmployeeList;
                chk_Empcode.DataTextField = "LastName";
                chk_Empcode.DataValueField = "EmployeeId";
                chk_Empcode.DataBind();


                Branch_Load(ddl_tobranch);
                row_tobranch.Visible = true;

            }
            else
            {
                lbl_Error.Text = "No Employees";

            }


        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";


        }


    }

    public void Master_values_Load()
    {
        employee.BranchId = (int)ViewState["ddl_frombranch_Selected"];
        r.BranchID = (int)ViewState["ddl_frombranch_Selected"];

        switch (Convert.ToInt32(ddl_fromoption.SelectedItem.Value))
        {

            case 1:
                Employee_Load();
                row_fromfilter.Visible = false;
                row_mMydet.Visible = true;
                row_empcode.Visible = true;
                row_emppwd.Visible = true;
                break;

            case 2: CheckList = r.fn_Department(r);
                Fromfilter_load("DepartmentID", "DepartmentName");
                break;

            case 3: CheckList = r.fn_Division(r);
                Fromfilter_load("DivisionID", "DivisionName");
                break;

            case 4: CheckList = r.fn_Level(r);
                Fromfilter_load("temp_int", "temp_string");
                break;

            case 5: CheckList = r.fn_Designation(r);
                Fromfilter_load("DesignationID", "DesignationName");
                break;

            case 6: CheckList = r.fn_Grade(r);
                Fromfilter_load("GradeID", "GradeName");
                break;

            case 7: CheckList = r.fn_EmployeeCategory(r);
                Fromfilter_load("CategoryCode", "CategoryName");
                break;

            case 8: CheckList = r.fn_JobStatus(r);
                Fromfilter_load("JobStatusID", "JobStatusName");
                break;

            case 9: CheckList = r.fn_Shift(r);
                Fromfilter_load("ShiftTypeID", "ShiftTypeName");
                break;

            case 10: CheckList = r.fn_Project(r);
                Fromfilter_load("ProjectID", "ProjectName");
                break;

            default: lbl_Error.Text = "No Items Selected";
                break;
        }


    }

    public void Master_values_Load_To()
    {
        
        employee.BranchId = (int)ViewState["ddl_frombranch_Selected"];
        r.BranchID = (int)ViewState["ddl_frombranch_Selected"];

        switch (Convert.ToInt32(ddl_tooption.SelectedItem.Value))
        {

            case 1:
                Employee_Load();
                break;

            case 2: CheckList = r.fn_Department(r);
                Tofilter_load("DepartmentID", "DepartmentName");
                break;

            case 3: CheckList = r.fn_Division(r);
                Tofilter_load("DivisionID", "DivisionName");
                break;

            case 4: CheckList = r.fn_Level(r);
                Tofilter_load("temp_int", "temp_string");
                break;

            case 5: CheckList = r.fn_Designation(r);
                Tofilter_load("DesignationID", "DesignationName");
                break;

            case 6: CheckList = r.fn_Grade(r);
                Tofilter_load("GradeID", "GradeName");
                break;

            case 7: CheckList = r.fn_EmployeeCategory(r);
                Tofilter_load("CategoryCode", "CategoryName");
                break;

            case 8: CheckList = r.fn_JobStatus(r);
                Tofilter_load("JobStatusID", "JobStatusName");
                break;

            case 9: CheckList = r.fn_Shift(r);
                Tofilter_load("ShiftTypeID", "ShiftTypeName");
                break;

            case 10: CheckList = r.fn_Project(r);
                Tofilter_load("ProjectID", "ProjectName");
                break;

            default: lbl_Error.Text = "No Items Selected";
                break;
        }
    }

    public void Fromfilter_load(string str_id, string str_name)
    {

        ddl_fromfilter.Items.Clear();

        if (CheckList.Count > 0)
        {

            ddl_fromfilter.DataSource = CheckList;
            ddl_fromfilter.DataValueField = str_id;
            ddl_fromfilter.DataTextField = str_name;
            ddl_fromfilter.DataBind();
        }
        else
        {
            lbl_Error.Text = "No Data";
        }

    }

    public void Tofilter_load(string str_id, string str_name)
    {

        ddl_tofilter.Items.Clear();
        btn_transfer.Visible = true;

        if (CheckList.Count > 0)
        {

            ddl_tofilter.DataSource = CheckList;
            ddl_tofilter.DataValueField = str_id;
            ddl_tofilter.DataTextField = str_name;
            ddl_tofilter.DataBind();
        }
        else
        {
            lbl_Error.Text = "No Data";
        }
    }

    protected void ddl_frombranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_frombranch.SelectedValue != "0")
        {
            ViewState["ddl_frombranch_Selected"] = Convert.ToInt32(ddl_frombranch.SelectedItem.Value);
            invisible();
            row_fromoption.Visible = true;
            ddl_fromoption.SelectedValue = "0";
        }
        else
        {
            //row_fromoption.Visible = false;
            invisible();
        }
    }

    protected void ddl_fromoption_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_fromoption.SelectedValue != "0")
        {
            row_fromfilter.Visible = true;
            ViewState["ddl_fromoption_Selected"] = Convert.ToInt32(ddl_fromoption.SelectedItem.Value);
            Master_values_Load();
        }
        else
        {
            row_fromfilter.Visible = false;
            row_chkemployee.Visible = false;
            row_tobranch.Visible = false;
            row_tooption.Visible = false;
            row_tofilter.Visible = false;
            row_reason.Visible = false;
            btn_transfer.Visible = false;
            row_mMydet.Visible = false;
            row_empcode.Visible = false;
            row_emppwd.Visible = false;
            tab_transfering.Visible = false;
        }
    }

    public void visible()
    {
        //row_from.Visible = true;
        //row_fromoption.Visible = true;
        //row_fromfilter.Visible = true;
        row_chkemployee.Visible = true;
        txt_reason.Value = "";
        btn_transfer.Visible = true;
        row_mMydet.Visible = true;
        row_empcode.Visible = true;
        row_emppwd.Visible = true;
        //tab_transfering.Visible = true;
    }

    public void invisible()
    {
        //row_from.Visible = false;
        row_fromoption.Visible = false;
        row_fromfilter.Visible = false;
        row_chkemployee.Visible = false;
        row_tobranch.Visible = false;
        row_tooption.Visible = false;
        row_tofilter.Visible = false;
        row_reason.Visible = false;
        btn_transfer.Visible = false;
        row_mMydet.Visible = false;
        row_empcode.Visible = false;
        row_emppwd.Visible = false;
        tab_transfering.Visible = false;
    }

    protected void ddl_fromfilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        row_chkemployee.Visible = true;
        row_empcode.Visible = true;
        row_emppwd.Visible = true;
        row_mMydet.Visible = true;
        master_query();
        //Branch_Load();
    }

    protected void ddl_tobranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_tobranch.SelectedValue != "")
        {
            row_tooption.Visible = true;
            ddl_tooption.SelectedValue = "0";
        }
        else
        {
            row_tooption.Visible = false;
        }

    }

    protected void ddl_tooption_SelectedIndexChanged(object sender, EventArgs e)
    {
        row_tofilter.Visible = true;
        r.BranchID = (int)ViewState["ddl_frombranch_Selected"];
        Master_values_Load_To();
    }

    protected void ddl_tofilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_reason.Value = "";
        btn_transfer.Visible = true;
        row_mMydet.Visible = true;
        row_empcode.Visible = true;
        row_emppwd.Visible = true;
    }

    public void master_query()
    {

        switch (Convert.ToInt32(ViewState["ddl_fromoption_Selected"]))
        {

            case 2: str_query = "select * from paym_Employee where pn_EmployeeID in(select pn_EmployeeID from paym_Employee_profile where pn_DepartmentId="+ddl_fromfilter.SelectedItem.Value+")";
                Master_Employee_Load();
                break;

            case 3:
                str_query = "select * from paym_Employee where pn_EmployeeID in(select pn_EmployeeID from paym_Employee_profile where pn_DivisionId=" + ddl_fromfilter.SelectedItem.Value + ")";
                Master_Employee_Load();
                break;

            case 4: 
                str_query = "select * from paym_Employee where pn_EmployeeID in(select pn_EmployeeID from paym_Employee_profile where pn_LevelID=" + ddl_fromfilter.SelectedItem.Value + ")";
                Master_Employee_Load();
                break;

            case 5: 
                str_query = "select * from paym_Employee where pn_EmployeeID in(select pn_EmployeeID from paym_Employee_profile where pn_DesingnationId=" + ddl_fromfilter.SelectedItem.Value + ")";
                Master_Employee_Load();
                break;

            case 6: 
                str_query = "select * from paym_Employee where pn_EmployeeID in(select pn_EmployeeID from paym_Employee_profile where pn_GradeId=" + ddl_fromfilter.SelectedItem.Value + ")";
                Master_Employee_Load();
                break;

            case 7: 
                str_query = "select * from paym_Employee where pn_EmployeeID in(select pn_EmployeeID from paym_Employee_profile where pn_CategoryId=" + ddl_fromfilter.SelectedItem.Value + ")";
                Master_Employee_Load();
                break;

            case 8: 
                str_query = "select * from paym_Employee where pn_EmployeeID in(select pn_EmployeeID from paym_Employee_profile where pn_JobStatusID=" + ddl_fromfilter.SelectedItem.Value + ")";
                Master_Employee_Load();
                break;

            case 9:
                str_query = "select * from paym_Employee where pn_EmployeeID in(select pn_EmployeeID from paym_Employee_profile where pn_ShiftId=" + ddl_fromfilter.SelectedItem.Value + ")";
                Master_Employee_Load();
                break;

            case 10: 
                str_query = "select * from paym_Employee where pn_EmployeeID in(select pn_EmployeeID from paym_Employee_profile where pn_projectsiteID=" + ddl_fromfilter.SelectedItem.Value + ")";
                Master_Employee_Load();
                break;

            default: lbl_Error.Text = "No Items Selected";
                break;
    }
    }

    protected void btn_transfer_Click(object sender, EventArgs e)
    {
            user_id = txt_empcode.Value;
            password = txt_emppwd.Text;
            ds_check_Employee = company.fn_get_Login_Employee(user_id, password);

            if (ds_check_Employee.Tables[0].Rows.Count > 0)
            {
                cs_k = 0;
                str_update = "";

                for (i = 0; i < chk_Empcode.Items.Count; i++)
                {

                    if (chk_Empcode.Items[i].Selected == true)
                    {

                        if (cs_k == 0)
                        {
                            str_update = chk_Empcode.Items[i].Value;
                            cs_k++;

                        }
                        else
                        {
                            str_update = str_update + "," + chk_Empcode.Items[i].Value;

                        }

                    }
                }

                if (str_update != "")
                {
                    ViewState["str_up"] = str_update;
                    tab_transfering.Visible = true;
                }
                else
                {
                    lbl_Error.Text = "Select atleast one Employee";
                }
            }
            else
            {

                lbl_Error.Text = "Employee Code and Password does not match";
            }
    }

    public string master_update()
    {
        master_up = "";

        switch (Convert.ToInt32(ddl_tooption.SelectedItem.Value))
        {

            case 2: master_up = "pn_DepartmentId="+ddl_tofilter.SelectedItem.Value+"";
                
                break;

            case 3:
                master_up = "pn_DivisionId =" + ddl_tofilter.SelectedItem.Value + "";
                break;

            case 4:
                master_up = "pn_LevelID =" + ddl_tofilter.SelectedItem.Value + "";
                break;

            case 5:
                master_up = "pn_DesingnationId =" + ddl_tofilter.SelectedItem.Value + "";
                break;

            case 6: 
                master_up = "pn_GradeId =" + ddl_tofilter.SelectedItem.Value + "";
                break;

            case 7:
                master_up = "pn_CategoryId =" + ddl_tofilter.SelectedItem.Value + "";
                break;

            case 8: 
                master_up = "pn_JobStatusID =" + ddl_tofilter.SelectedItem.Value + "";
                break;

            case 9: 
                master_up = "pn_ShiftId =" + ddl_tofilter.SelectedItem.Value + "";
                break;

            case 10:
                master_up = "pn_projectsiteID =" + ddl_tofilter.SelectedItem.Value + "";
                break;

            default: lbl_Error.Text = "";
                break;
        }
        return master_up;
    }

    protected void btn_yes_Click(object sender, ImageClickEventArgs e)
    {
            str_update = "update paym_Employee_profile set " + master_update() + " where pn_EmployeeID in(" + (string)ViewState["str_up"] + ")";
            employee.Temp_Employee(str_update);
            lbl_Error.Text = "Transferred Sussessfully";
    }

    protected void btn_no_Click(object sender, ImageClickEventArgs e)
    {
        tab_transfering.Visible = false;
    }

    protected void chkall_Employee_CheckedChanged(object sender, EventArgs e)
    {
        if (chkall_Employee.Checked == true)
        {

            for (i = 0; i < chk_Empcode.Items.Count; i++)
            {
                chk_Empcode.Items[i].Selected = true;
            }
        }
        else
        {
            for (i = 0; i < chk_Empcode.Items.Count; i++)
            {
                chk_Empcode.Items[i].Selected = false;
            }

        }
    }
}
