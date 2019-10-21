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

public partial class Search : System.Web.UI.Page
{
    Candidate cd = new Candidate();
    Company c = new Company();
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();


    string ws_query = "", cs_Where_Master_Query = "", cs_temp_master = "", finalQuery = "", s_Report="";
    int i, ks = 0,cs_k = 0;
    string s_login_role;


    protected void Page_Load(object sender, EventArgs e)
    {
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        lblmsg.Text = "";
        
        lblmsg.Text = "Hi " + Request.Cookies["Login_UserID"].Value + "!";
        ClientScriptManager manager = Page.ClientScript;
        manager.RegisterStartupScript(this.GetType(), "Call", "clear_all();", true);
        
        //txtminvalue.Visible = false;
        //txtmaxvalue.Visible = false;

        Session["Query_Session"] = "";
        
        if (!IsPostBack)
        {
            chk_ddl_load();

        }


     }   

   

//*********************Select Query*******************************



//*********************Where Query (Masters)*******************************

 public string whereQuery_Master()
{ 
   

cs_temp_master = selected_Masters(Chk_Branch);                 
        
        if (cs_temp_master != "")
        {
            if (cs_k == 0)
            {
                cs_Where_Master_Query = "BranchName in(" + cs_temp_master + ")";
                cs_k++;
                cs_temp_master = "";
            }
            else
            {
                cs_Where_Master_Query = cs_Where_Master_Query + " and BranchName in(" + cs_temp_master + ")";
                cs_temp_master = "";
            }

        }


 cs_temp_master = selected_Masters(chk_division);

        if (cs_temp_master != "")
        {
            if (cs_k == 0)
            {
                cs_Where_Master_Query = "DivisionName in(" + cs_temp_master + ")";
                cs_k++;
                cs_temp_master = "";
            }
            else
            {
                cs_Where_Master_Query = cs_Where_Master_Query + " and DivisionName in(" + cs_temp_master + ")";
                cs_temp_master = "";
            }

        }



        cs_temp_master = selected_Masters(Chk_Department);

        if (cs_temp_master != "")
        {
            if (cs_k == 0)
            {
                cs_Where_Master_Query = "DepartmentName in(" + cs_temp_master + ")";
                cs_k++;
                cs_temp_master = "";
            }
            else
            {
                cs_Where_Master_Query = cs_Where_Master_Query + " and DepartmentName in(" + cs_temp_master + ")";
                cs_temp_master = "";
            }

        }




        cs_temp_master = selected_Masters(Chk_Designation);

        if (cs_temp_master != "")
        {
            if (cs_k == 0)
            {
                cs_Where_Master_Query = "DesignationName in(" + cs_temp_master + ")";
                cs_k++;
                cs_temp_master = "";
            }
            else
            {
                cs_Where_Master_Query = cs_Where_Master_Query + " and DesignationName in(" + cs_temp_master + ")";
                cs_temp_master = "";
            }

        }




        cs_temp_master = selected_Masters(chk_Category);

        if (cs_temp_master != "")
        {
            if (cs_k == 0)
            {
                cs_Where_Master_Query = "CategoryName in(" + cs_temp_master + ")";
                cs_k++;
                cs_temp_master = "";
            }
            else
            {
                cs_Where_Master_Query = cs_Where_Master_Query + " and CategoryName in(" + cs_temp_master + ")";
                cs_temp_master = "";
            }

        }


        cs_temp_master = selected_Masters(chk_Grade);

        if (cs_temp_master != "")
        {
            if (cs_k == 0)
            {
                cs_Where_Master_Query = "GradeName in(" + cs_temp_master + ")";
                cs_k++;
                cs_temp_master = "";
            }
            else
            {
                cs_Where_Master_Query = cs_Where_Master_Query + " and GradeName in(" + cs_temp_master + ")";
                cs_temp_master = "";
            }

        }


        cs_temp_master = selected_Masters(chk_Shift);

        if (cs_temp_master != "")
        {
            if (cs_k == 0)
            {
                cs_Where_Master_Query = "ShiftName in(" + cs_temp_master + ")";
                cs_k++;
                cs_temp_master = "";
            }
            else
            {
                cs_Where_Master_Query = cs_Where_Master_Query + " and ShiftName in(" + cs_temp_master + ")";
                cs_temp_master = "";
            }

        }



        cs_temp_master = selected_Masters(chk_Jobstatus);

        if (cs_temp_master != "")
        {
            if (cs_k == 0)
            {
                cs_Where_Master_Query = "JobStatusName in(" + cs_temp_master + ")";
                cs_k++;
                cs_temp_master = "";
            }
            else
            {
                cs_Where_Master_Query = cs_Where_Master_Query + " and JobStatusName in(" + cs_temp_master + ")";
                cs_temp_master = "";
            }

        }


        cs_temp_master = selected_Masters(chk_Level);

        if (cs_temp_master != "")
        {
            if (cs_k == 0)
            {
                cs_Where_Master_Query = "LevelName in(" + cs_temp_master + ")";
                cs_k++;
                cs_temp_master = "";
            }
            else
            {
                cs_Where_Master_Query = cs_Where_Master_Query + " and LevelName in(" + cs_temp_master + ")";
                cs_temp_master = "";
            }

        }



        cs_temp_master = selected_Masters(chk_projectsite);

        if (cs_temp_master != "")
        {
            if (cs_k == 0)
            {
                cs_Where_Master_Query = "projectsiteName in(" + cs_temp_master + ")";
                cs_k++;
                cs_temp_master = "";
            }
            else
            {
                cs_Where_Master_Query = cs_Where_Master_Query + " and projectsiteName in(" + cs_temp_master + ")";
                cs_temp_master = "";
            }

        }


        //cs_Where_Master_Query = cs_Where_Master_Query+" ";

return cs_Where_Master_Query;




}

 public string selected_Masters(CheckBoxList cs_id)
    {
       
     ws_query = "";
     ks = 0;

        for (i = 0; i < cs_id.Items.Count; i++)
        {

            if (cs_id.Items[i].Selected == true)
            {             

                if (ks == 0)
                {
                    ws_query = "'" + cs_id.Items[i].Text + "'";
                    ks = ks + 1;
                }
                else
                {
                    ws_query = ws_query + "," + "'" + cs_id.Items[i].Text + "'";
                }

            }

        }

        return ws_query;

       

    }


//*********************FinalQuery**************************************************
   
    
    protected void btn_query_Click(object sender, ImageClickEventArgs e)
    {
        try
        {


            finalQuery = whereQuery_Master();

            if (finalQuery != "" && hdn_Others.Value != "")
            {
                finalQuery = finalQuery + " and " + hdn_Others.Value;
            }

            if (finalQuery == "" && hdn_Others.Value != "")
            {
                finalQuery = hdn_Others.Value;
            }



            if (finalQuery != "")
            {

                finalQuery = "select pn_EmployeeID from Temp_Employee where " + finalQuery + "";

                Response.Cookies["Query_Session"].Value = finalQuery;

                Response.Redirect("Report.aspx");


            }
            else
            {
                finalQuery = "nil";
                ClientScriptManager manager1 = Page.ClientScript;
                //manager1.RegisterOnSubmitStatement(this.GetType(), "SubmitScript", "Error();");
                //btn_query.Attributes.Add("OnClientClick", "Error();");
                if (!manager1.IsStartupScriptRegistered(this.GetType(), "Alert"))
                    {

                        manager1.RegisterStartupScript(this.GetType(), "Alert", "alert('No Query Selected')", true);

                    }
                //manager1.RegisterClientScriptBlock(this.GetType(), "Alert", "<script /javascript>alert('No Query Selected')</script>");

                //Session["Query_Session"] = finalQuery;
                //Response.Redirect("Report.aspx");

            }

        }
        catch (Exception ex)
        {

            lbl_error.Text = "Error";

        }

    }


    protected void btn_back_Click(object sender, ImageClickEventArgs e)
    {
        Response.Cookies["Query_Session"].Value = "back";

        Response.Redirect("Report.aspx");


    }


    public void chk_ddl_load()
    {
        Collection<Company> BranchList = c.fn_getBranchs();
        Collection<Employee> departmentList = employee.fn_getDepartmentList();
        Collection<Employee> designationList = employee.fn_getDesignationList();
        Collection<Employee> DivisionList = employee.fn_getDivisionList();
        Collection<Employee> CategoryList = employee.fn_getCategoryList();
        Collection<Employee> GradeList = employee.fn_getGradeList();
        Collection<Employee> ShiftList = employee.fn_getShiftList();
        Collection<Employee> JobStatusList = employee.fn_getJobStatusList();
        Collection<Employee> LevelList = employee.fn_getLevelList();
        Collection<Employee> ProjectsiteList = employee.fn_getprojectsiteList();
        s_Report = "sp_columns temp_employee";
        Collection<Employee> CheckList = employee.Temp_checkList(s_Report);

        //Branch   

        Chk_Branch.DataSource = BranchList;
        Chk_Branch.DataTextField = "CompanyName";
        Chk_Branch.DataValueField = "CompanyId";
        Chk_Branch.DataBind();


        //Department

        if (departmentList.Count > 1)
        {
            Chk_Department.DataSource = departmentList;
            Chk_Department.DataValueField = "DepartmentId";
            Chk_Department.DataTextField = "DepartmentName";
            Chk_Department.DataBind();
        }
        else
        {
            Chk_Department.Items.Add("No Department");
            Chk_Department.Enabled = false;

        }

        //Designation

        if (designationList.Count > 1)
        {

            Chk_Designation.DataSource = designationList;
            Chk_Designation.DataValueField = "DesignationId";
            Chk_Designation.DataTextField = "DesignationName";
            Chk_Designation.DataBind();
        }
        else
        {
            Chk_Designation.Items.Add("No Designation");
            Chk_Designation.Enabled = false;

        }

        //Division                    

        if (DivisionList.Count > 1)
        {

            chk_division.DataSource = DivisionList;
            chk_division.DataValueField = "DivisionID";
            chk_division.DataTextField = "DivisionName";
            chk_division.DataBind();
        }
        else
        {
            chk_division.Items.Add("No Division");
            chk_division.Enabled = false;

        }

        //Category

        if (CategoryList.Count > 1)
        {
            chk_Category.DataSource = CategoryList;
            chk_Category.DataValueField = "CategoryId";
            chk_Category.DataTextField = "CategoryName";
            chk_Category.DataBind();
        }
        else
        {
            chk_Category.Items.Add("No Category");
            chk_Category.Enabled = false;
        }

        //Grade

        if (GradeList.Count > 1)
        {

            chk_Grade.DataSource = GradeList;
            chk_Grade.DataValueField = "GradeID";
            chk_Grade.DataTextField = "GradeName";
            chk_Grade.DataBind();
        }
        else
        {
            chk_Grade.Items.Add("No Grade");
            chk_Grade.Enabled = false;

        }

        //ShiftList

        if (ShiftList.Count > 1)
        {

        //    chk_Shift.DataSource = ShiftList;
        //    chk_Shift.DataValueField = "ShiftTypeID";
        //    chk_Shift.DataTextField = "ShiftTypeName";
        //    chk_Shift.DataBind();
        }
        else
        {
            chk_Shift.Items.Add("No Shift");
            chk_Shift.Enabled = false;
        }

        //JobStatusList

        if (JobStatusList.Count > 1)
        {

            chk_Jobstatus.DataSource = JobStatusList;
            chk_Jobstatus.DataValueField = "JobStatusID";
            chk_Jobstatus.DataTextField = "JobStatusName";
            chk_Jobstatus.DataBind();
        }
        else
        {
            chk_Jobstatus.Items.Add("No Jobstatus");
            chk_Jobstatus.Enabled = false;

        }


        //LevelList

        if (LevelList.Count > 1)
        {

            chk_Level.DataSource = LevelList;
            chk_Level.DataValueField = "LevelId";
            chk_Level.DataTextField = "LevelName";
            chk_Level.DataBind();
        }
        else
        {
            chk_Level.Items.Add("No Level");
            chk_Level.Enabled = false;

        }


        //ProjectsiteList

        if (ProjectsiteList.Count > 1)
        {
            chk_projectsite.DataSource = ProjectsiteList;
            chk_projectsite.DataValueField = "ProjectsiteId";
            chk_projectsite.DataTextField = "ProjectsiteName";
            chk_projectsite.DataBind();
        }
        else
        {
            chk_projectsite.Items.Add("No Projectsite");
            chk_projectsite.Enabled = false;

        }       

        if (CheckList.Count > 0)
        {

            for (int ddl_i = 0; ddl_i < CheckList.Count; ddl_i++)
            {

                if (ddl_i == 1 || ddl_i == 2 || ddl_i == 5 || ddl_i == 21 || ddl_i == 26 || ddl_i == 30 || ddl_i == 31 || ddl_i == 32 || ddl_i == 37 || ddl_i == 38 || ddl_i == 45 || ddl_i == 46 || ddl_i == 47 || ddl_i == 48 || ddl_i == 53 || ddl_i == 54 || ddl_i == 55)
                {
                    ListItem es_list = new ListItem();

                    es_list.Value = CheckList[ddl_i].EmployeeId.ToString();
                    es_list.Text = CheckList[ddl_i].EmployeeCode.ToString();
                    ddl_name.Items.Add(es_list);
                }

            }

        }




    }




}
