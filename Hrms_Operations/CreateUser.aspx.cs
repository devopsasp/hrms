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
using ePayHrms.User_authentication;

public partial class Hrms_Operations_Default : System.Web.UI.Page
{

    Company company = new Company();
    Employee employee = new Employee();   
    User__Rights user= new User__Rights();
    Collection<User__Rights> UserRightList;
    Collection<Employee> EmployeesList;
    Collection<Company> CompanyList;
    Collection<User__Rights> UserList;  
    string s_login_role;
    int ddl_i;

    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //admin();
        //empcodeddlload();
        

        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        user.companyid = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        user.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        s_login_role = Request.Cookies["Login_temp_Role"].Value;

        if (!IsPostBack)
        {
            Response.Cookies["Select_Employee"].Value = "0";

            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {
                Session["ErrorMsg"] = "";

                switch (s_login_role)
                {
                    case "a": row_emp.Visible = false;
                        row_showdet_btn.Visible = false;
                        row_user.Visible = false;
                        admin();
                        break;

                    case "h":
                        ddl_Department_load();
                        hr();
                        CheckedList();
                        grid_load();
                        break;


                    default:
                        Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                        Response.Redirect("~/Company_Home.aspx");
                        break;
                }

            }
            else
            {
                Response.Cookies["Msg_Session"].Value = "Create Company";
                Response.Redirect("~/Company_Home.aspx");
            }

        }
        
        }
    
    public void admin()
    {
        try
        {
           
            Collection<Company> ddlBranchsList;


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


            //ddlBranchsList = company.fn_getBranchs();

            //ddl_Branch.DataSource = ddlBranchsList;
            //ddl_Branch.DataTextField = "CompanyName";
            //ddl_Branch.DataValueField = "CompanyId";
            //ddl_Branch.DataBind();




        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }

    }

    public void CheckedList()
    {
        UserRightList = user.fn_user_creation(user.BranchID);
        if (UserRightList.Count > 0)
        {
            for (int i = 0; i < UserRightList.Count; i++)
            {
                ItemsToCheck(UserRightList[i].EmployeeID.ToString());
            }
        }
    }

    public void ItemsToCheck(string Item)
    {
        for(int i=0; i<ddl_Employee.Items.Count; i++)
        {
            if (ddl_Employee.Items[i].Value == Item)
            {
                ddl_Employee.Items[i].Selected = true;
                //ddl_Employee.SelectedValue = Item;
                break;
            }
        }
    }


    public void grid_load()
    {
        int cc = 0;
        myConnection.Open();
        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM user_creation where pn_BranchID = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "'", myConnection);

        DataSet ds = new DataSet();

        ad.Fill(ds, "user_creation");


        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView1.DataSource = ds;
            GridView1.DataBind();
            int columnCount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columnCount;
            GridView1.Rows[0].Cells[0].Text = "No Records Found..";

        }
        else
        {
            cc = ds.Tables[0].Rows.Count;
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        if (cc != 0)
        {
            UserRightList = user.fn_Student_Department(employee.BranchId);

            if (UserRightList.Count > 0)
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    ((DropDownList)GridView1.Rows[i].FindControl("ddl_department")).DataSource = UserRightList;
                    ((DropDownList)GridView1.Rows[i].FindControl("ddl_department")).DataValueField = "DepartmentID";
                    ((DropDownList)GridView1.Rows[i].FindControl("ddl_department")).DataTextField = "DepartmentName";
                    ((DropDownList)GridView1.Rows[i].FindControl("ddl_department")).DataBind();
                    string ads = ds.Tables[0].Rows[i]["Department"].ToString();
                    ((DropDownList)GridView1.Rows[i].FindControl("ddl_department")).SelectedItem.Text = ds.Tables[0].Rows[i]["Department"].ToString();
                }
            }
        }

        myConnection.Close();

    }


    public void hr()
    {
        try
        {
            row_branch.Visible = false;
            ddl_Employee.Visible = false;
            row_showdet_btn.Visible = false;

            Collection<Employee> EmployeeList = employee.fn_getEmployeeList(employee);

            if (EmployeeList.Count > 0)
            {
                ddl_Employee.Visible = true;
                row_showdet_btn.Visible = true;

                for (ddl_i = 0; ddl_i < EmployeeList.Count; ddl_i++)
                {

                    if (ddl_i == -1)
                    {
                        ListItem e_list = new ListItem();

                        e_list.Text = "Select Employee";
                        e_list.Value = "0";
                        ddl_Employee.Items.Add(e_list);
                    }
                    else
                    {

                        ListItem e_list = new ListItem();

                        e_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
                        e_list.Text = EmployeeList[ddl_i].LastName.ToString();
                        ddl_Employee.Items.Add(e_list);

                    }

                }
            }
            else
            {
                ddl_Employee.Visible = false;
            }


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }


    protected void ddl_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           
            ddl_Employee.Items.Clear();

            ViewState["preview_BranchID"] = Convert.ToInt32(ddl_Branch.SelectedItem.Value);

            employee.BranchId = Convert.ToInt32(ddl_Branch.SelectedItem.Value);

            Collection<Employee> EmployeeList = employee.fn_getEmployeeList(employee);
            //Collection<Employee> EmployeeList = employee.fn_getAllEmployees();

            if (EmployeeList.Count > 0)
            {
                row_user.Visible = true;
                row_emp.Visible = true;
                row_showdet_btn.Visible = true;


                for (ddl_i = 0; ddl_i < EmployeeList.Count; ddl_i++)
                {

                    if (ddl_i == -1)
                    {
                        ListItem es_list = new ListItem();

                        es_list.Text = "Select Employee";
                        es_list.Value = "0";
                        ddl_Employee.Items.Add(es_list);
                    }
                    else
                    {
                        ListItem es_list = new ListItem();

                        es_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
                        es_list.Text = EmployeeList[ddl_i].LastName.ToString();
                        ddl_Employee.Items.Add(es_list);
                    }

                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Employees found');", true);

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

    public void save()
    {
        user.d_Date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        user.status = 'Y';
        user.Role = Convert.ToChar(s_login_role);
        user.user_Creation(user);
    }

    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Employee_load();
        CheckedList();
    }

    public void ddl_Employee_load()
    {
        ddl_Employee.Items.Clear();
        employee.DepartmentId = Convert.ToInt32(ddl_department.SelectedValue);
        EmployeesList = employee.fn_getEmployeeDepartment(employee);
        if (EmployeesList.Count > 0)
        {
            for (int ddl_i = 0; ddl_i < EmployeesList.Count; ddl_i++)
            {
                ListItem es_list = new ListItem();
                es_list.Value = EmployeesList[ddl_i].EmployeeId.ToString();
                es_list.Text = EmployeesList[ddl_i].LastName.ToString();
                ddl_Employee.Items.Add(es_list);
            }
        }
    }

    public void ddl_Department_load()
    {
        EmployeesList = employee.fn_getDepartmentList1(employee.BranchId);
        if (EmployeesList.Count > 0)
        {

            for (int ddl_i = -1; ddl_i < EmployeesList.Count; ddl_i++)
            {

                if (ddl_i == -1)
                {
                    ListItem es_list = new ListItem();

                    es_list.Text = "Select Department";
                    es_list.Value = "0";
                    ddl_department.Items.Add(es_list);
                }
                else
                {
                    ListItem es_list = new ListItem();

                    es_list.Value = EmployeesList[ddl_i].DepartmentId.ToString();
                    es_list.Text = EmployeesList[ddl_i].DepartmentName.ToString();
                    ddl_department.Items.Add(es_list);
                }
            }
        }
    }
    protected void ddl_Employee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            string result = Request.Form["__EVENTTARGET"];

            string[] checkedBox = result.Split('$'); ;

            int index = int.Parse(checkedBox[checkedBox.Length - 1]);

            user.EmployeeID = Convert.ToInt32(ddl_Employee.Items[index].Value);

            string[] code = ddl_Employee.Items[index].Text.Split('-');

            user.EmpCode = code[0].Trim();

            user.username = ddl_Employee.Items[index].Text;

            if (ddl_Employee.Items[index].Selected)
            {
                UserList = user.fn_emp_user_creation(user);
                if (UserList.Count <= 0)
                {
                    if (s_login_role == "a")
                    {
                        user.BranchID = (int)ViewState["preview_BranchID"];
                    }

                    if (s_login_role == "h")
                    {
                        user.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                    }
                    save();
                    grid_load();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('User Rights already aligned for this employee.');", true);
                }
            }

            else
            {
                myConnection.Open();
                SqlCommand cmd = new SqlCommand("delete from user_authentications where pn_employeeid = '" + user.EmployeeID + "'", myConnection);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("delete from user_creation where pn_employeeid = '" + user.EmployeeID + "'", myConnection);
                cmd.ExecuteNonQuery();
                myConnection.Close();
                grid_load();
            }

            //user.EmployeeID = Convert.ToInt32(ddl_Employee.SelectedItem.Value);
            //if (ddl_user.SelectedItem.Text == "Revoke")
            //{
            //    var conString = ConfigurationManager.ConnectionStrings["connectionstring"];
            //    string constr = conString.ConnectionString;

            //    SqlConnection con = new SqlConnection(constr);
            //    con.Open();
            //    SqlCommand cmd = new SqlCommand("delete from user_authentications where pn_employeeid = '" + user.EmployeeID + "'", con);
            //    cmd.ExecuteNonQuery();
            //    cmd = new SqlCommand("delete from user_creation where pn_employeeid = '" + user.EmployeeID + "'", con);
            //    cmd.ExecuteNonQuery();
            //    con.Close();
            //    Response.Write("<script>alert('Permission Revoked')</script>");
            //}
            //else
            //{
            //    UserList = user.fn_emp_user_creation(user);

            //    if (UserList.Count <= 0)
            //    {
            //        if (s_login_role == "a")
            //        {
            //            user.BranchID = (int)ViewState["preview_BranchID"];
            //        }

            //        if (s_login_role == "h")
            //        {
            //            user.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            //        }
            //        save();
            //        lbl_Error.Text = "Successfully Saved";
            //    }
            //    else
            //    {
            //        lbl_Error.Text = "Employee already exist in the user list";
            //    }
            //}
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        myConnection.Open();
        SqlCommand cmd = new SqlCommand();
        foreach (GridViewRow gvr in GridView1.Rows)
        {
            cmd = new SqlCommand("update user_creation set department =  '" + ((DropDownList)gvr.FindControl("ddl_department")).SelectedItem.Text + "' where username = '" + ((Label)gvr.FindControl("lbl_empname")).Text + "'and pn_companyID = '"+employee.CompanyId+"' and pn_Branchid = '"+employee.BranchId+"'", myConnection);
            cmd.ExecuteNonQuery();
        }
        myConnection.Close();
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Saved Successfully');", true);
    }
}
