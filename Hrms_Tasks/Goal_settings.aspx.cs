using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ePayHrms.Company;
using ePayHrms.Employee;
using ePayHrms.Leave;
using System.Data.SqlClient;
public partial class Hrms_Tasks_Goal_settings : System.Web.UI.Page
{
    private SqlConnection _connection;
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);

    SqlCommand cmd = new SqlCommand();
    SqlDataReader rea;
    SqlDataAdapter ada = new SqlDataAdapter();
    //DataSet ds = new DataSet();
    Company company = new Company();
    Employee employee = new Employee();
    protected void Page_Load(object sender, EventArgs e)
    {
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
       
            loadgoal();
        //GridView1.Visible = true;
        
    }
    public void loadgoal()
    {
        SqlDataAdapter adap = new SqlDataAdapter("select * from Goal_Master where pn_BranchID='" + employee.BranchId + "'", myConnection);
        DataSet ds = new DataSet();
        adap.Fill(ds, "Goal_Master");

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
    protected void Button1_Click(object sender, EventArgs e)
    {
        myConnection.Open();
        string gid = txtgoalid.ToString();
        string gname = txtgoalname.ToString();
        string gdesc = txtdescription.ToString();
        try
        {
            if (ddl_goaltype.Text == "--Select--")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Please select goal type!');", true);
            }
            else if (gid == "" || gname == "" || gdesc == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Please enter all details!');", true);
            }
            else
            {
                cmd = new SqlCommand("insert into Goal_Master(pn_CompanyID,pn_BranchID,Goal_id,Goal_name,Goal_type,Goal_description) values('" + employee.CompanyId + "','" + employee.BranchId + "','" + txtgoalid.Value + "','" + txtgoalname.Value + "','" + ddl_goaltype.Text + "','" + txtdescription.Value + "')", myConnection);
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Goal Added Successfully!');", true);
                clear();
                
            }
            myConnection.Close();
           loadgoal();
        }
        catch (Exception exc)
        {
        }
    }
    public void clear()
    {
        txtgoalid.Value = "";
        txtgoalname.Value = "";
        txtdescription.Value = "";
        ddl_goaltype.Text = "--Select--";

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        loadgoal();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow Gvrow = GridView1.Rows[e.RowIndex];
        if (Gvrow != null)
        {
            string gid = ((Label)Gvrow.FindControl("gidedit")).Text;
            string gname= ((Label)Gvrow.FindControl("gnameedit")).Text;
            string gtype = ((DropDownList)Gvrow.FindControl("gytpeedit")).Text;
            string gdesc = ((TextBox)Gvrow.FindControl("gdescedit")).Text;

            myConnection.Open();
            cmd = new SqlCommand("update goal_master set goal_name='" + gname + "',goal_type='" + gtype + "',goal_description='" + gdesc + "' where goal_id='" + gid + "'", myConnection);
            cmd.ExecuteNonQuery();
            myConnection.Close();
            GridView1.EditIndex = -1; // turn to edit mode
            loadgoal();

        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string ID = ((Label)GridView1.Rows[e.RowIndex].FindControl("Lblgid")).Text;

        DeleteRecord(ID);
        loadgoal();
    }
    private void DeleteRecord(string ID)
    {

        string sqlStatement = "DELETE FROM goal_master WHERE goal_id = @goalid";
        try
        {
            myConnection.Open();
            SqlCommand cmd = new SqlCommand(sqlStatement, myConnection);
            cmd.Parameters.AddWithValue("@goalid", ID);
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

}