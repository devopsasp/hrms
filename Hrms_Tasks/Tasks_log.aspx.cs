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
using ePayHrms.Login;
using ePayHrms.Connection;
using ePayHrms.Candidate;
using System.IO;
using ePayHrms.Company;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ePayHrms.BE.Recruitment;
using ePayHrms.Employee;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class Hrms_Tasks_Default : System.Web.UI.Page
{
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    //private SqlConnection _con;
    //ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    SqlDataReader rea;
    SqlDataReader rea1;
    SqlDataAdapter ada = new SqlDataAdapter();
    SqlDataAdapter ada1 = new SqlDataAdapter();
    Company company = new Company();
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    Candidate c = new Candidate();

    Collection<Candidate> WorkHistoryList;

    //Collection<Employee> AllowanceList;
    Collection<Company> CompanyList;


    string s_login_role;
    int ddl_i, grk;
    string _path, _Value;
    string s_form = "";
    DataSet ds_userrights;


    protected void Page_Load(object sender, EventArgs e)
    {
        

        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        c.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        Label8.Text = (string)Session["Login_Name"] + "!";
        //grd_view1.Visible = false;
        if (!IsPostBack)
        {
            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {

                switch (s_login_role)
                {

                    case "a":
                        admin();
                        break;

                    case "h":
                        hr();
                        break;

                    case "e": Response.Redirect("../Hrms_Employee/Employee_Preview.aspx");
                        break;

                    case "u":
                        s_form = "46";

                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            hr();

                        }
                        else
                        {
                            Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator.";
                            Response.Redirect("~/Company_Home.aspx");
                        }
                        break;
                    default:
                        Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                        Response.Redirect("../Hrms_Master/Common/Common_Home.aspx");
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
        //    try
        //    {
        //        Collection<Company> ddlBranchsList;

        //        ddlBranchsList = company.fn_getBranchs();

        //        if (ddlBranchsList.Count > 0)
        //        {

        //            for (ddl_i = -1; ddl_i < ddlBranchsList.Count; ddl_i++)
        //            {

        //                if (ddl_i == -1)
        //                {
        //                    ListItem list = new ListItem();

        //                    list.Text = "Select Branch";
        //                    list.Value = "0";
        //                    ddl_Branch.Items.Add(list);
        //                }
        //                else
        //                {

        //                    ListItem list = new ListItem();

        //                    list.Text = ddlBranchsList[ddl_i].CompanyName;
        //                    list.Value = ddlBranchsList[ddl_i].CompanyId.ToString();
        //                    ddl_Branch.Items.Add(list);

        //                }

        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        lbl_Error.Text = "Error";
        //    }

    }

    public void hr()
    {
        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM task_schedule", myConnection);

        DataSet ds = new DataSet();

        ad.Fill(ds, "task_schedule");


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
            GridView1.DataSource = ds;
            GridView1.DataBind();

        }
        

    }


    public void hr1()
    {


        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM task_schedule where pn_BranchId='" + employee.BranchId + "'", myConnection);

        DataSet ds = new DataSet();

        ad.Fill(ds, "task_schedule");


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
            GridView1.DataSource = ds;
            GridView1.DataBind();

        }
        GridView1.FooterRow.Visible = false;
        
    }



    private void DeleteRecord(string ID)
    {

        string sqlStatement = "DELETE FROM task_schedule WHERE TSubject = @TSubject";
        try
        {
            myConnection.Open();
            SqlCommand cmd = new SqlCommand(sqlStatement, myConnection);
            cmd.Parameters.AddWithValue("@TSubject", ID);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Deletion Error:";
            msg += ex.Message;
            throw new Exception(msg);

        }
        finally
        {
            myConnection.Close();
        }
    }

    
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "add")
        {

            string sub = ((TextBox)GridView1.FooterRow.FindControl("TextBox1")).Text;
            string des = ((TextBox)GridView1.FooterRow.FindControl("TextBox2")).Text;
            string doa = ((TextBox)GridView1.FooterRow.FindControl("TextBox3")).Text;
            string dept = ((DropDownList)GridView1.FooterRow.FindControl("DropDownList4")).Text;
            string assi = ((DropDownList)GridView1.FooterRow.FindControl("DropDownList1")).Text;
            string pri = ((DropDownList)GridView1.FooterRow.FindControl("DropDownList2")).Text;
            string stat = ((DropDownList)GridView1.FooterRow.FindControl("DropDownList3")).Text;
            string doc = ((TextBox)GridView1.FooterRow.FindControl("TextBox7")).Text;

            AddNewRecord(sub, des, doa, dept, assi, pri, stat, doc);

        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string ID = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("Label1")).Text;

        DeleteRecord(ID);
        hr();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex; // turn to edit mode
        hr1();
    }

    private void AddNewRecord(string sub, string des, string doa, string dept, string assi, string pri, string stat, string doc)
    {

        string query = @"INSERT INTO task_schedule (pn_CompanyID, pn_BranchID, TSubject,TDescription,DOA,Dept,Assigned,Priority,Status,DOC) VALUES('" + employee.CompanyId + "','" + employee.BranchId + "','" + sub + "','" + des + "','" + doa + "','" + dept + "','" + assi + "','" + pri + "','" + stat + "','" + doc + "')";

        SqlCommand myCommand = new SqlCommand(query, myConnection);

        //myCommand.Parameters.AddWithValue("@TSubject", sub);
        //myCommand.Parameters.AddWithValue("@des", des);
        //myCommand.Parameters.AddWithValue("@doa", doa);
        //myCommand.Parameters.AddWithValue("@assi", assi);
        //myCommand.Parameters.AddWithValue("@pri", pri);
        //myCommand.Parameters.AddWithValue("@stat", stat);
        //myCommand.Parameters.AddWithValue("@doc", doc);

        myConnection.Open();

        myCommand.ExecuteNonQuery();

        myConnection.Close();

        hr();

    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow Gvrow = GridView1.Rows[e.RowIndex];
        Label8.Text = Gvrow.ToString();
        if (Gvrow != null)
        {
            string subedit = ((TextBox)Gvrow.FindControl("Tsubedit")).Text;
            string desedit = ((TextBox)Gvrow.FindControl("Tdesedit")).Text;
            string doa = ((TextBox)Gvrow.FindControl("DOAedit")).Text;
            string dept = ((DropDownList)Gvrow.FindControl("deptedit")).Text;
            string asi = ((DropDownList)Gvrow.FindControl("assiedit")).Text;
            string pri = ((DropDownList)Gvrow.FindControl("prioredit")).Text;
            string stat = ((DropDownList)Gvrow.FindControl("statedit")).Text;
            string doc = ((TextBox)Gvrow.FindControl("DOCedit")).Text;
            myConnection.Open();
            cmd = new SqlCommand("update task_schedule set TSubject='" + subedit + "',TDescription='" + desedit + "',DOA='" + doa + "',dept='" + dept + "',Assigned='" + asi + "',Priority='" + pri + "',Status='" + stat + "',DOC='" + doc + "' where TSubject='" + subedit + "'", myConnection);
            cmd.ExecuteNonQuery();
            myConnection.Close();
            GridView1.EditIndex = -1; // turn to edit mode
            hr();

        }
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        hr();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("task_report.aspx");
    }
}
