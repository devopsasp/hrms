using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.ObjectModel;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ePayHrms.Company;
using ePayHrms.Employee;
using ePayHrms.Leave;
using System.Data.SqlClient;

public partial class Hrms_Master_Default : System.Web.UI.Page
{
    private SqlConnection _connection;
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader rea;
    SqlDataAdapter ada = new SqlDataAdapter();

    Company company = new Company();

    Employee employee = new Employee();

    Leave l = new Leave();

    Collection<Leave> IncrementList;
    Collection<Company> CompanyList;

    int company_Id, branch_Id, valid, temp_valid = 0, check;
    string _Value;
    string s_login_role;
    bool b_check = true;
    string s_form = "" , fname;
    DataSet ds_userrights;


    protected void Page_Load(object sender, EventArgs e)
    {
        fname = (string)Session["formulaBonus"];
        
        l.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        lbl_increment1.Visible = false;
        txtpointamount1.Visible = false;
        //optional_type.Visible = false;
        //rdo_percentage.Visible = false;
        if (!IsPostBack)
        {
            switch (s_login_role)
            {
                case "a":
                    load_admin();
                    break;

                case "h":
                    hr();
                    access();                    
                    break;

                case "u": s_form = "11";
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

                default: Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                    Response.Redirect("~/Company_Home.aspx");
                    break;

            }


        }
    }

    public void access()
    {
        _connection = con.fn_Connection();
        _connection.Open();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and section_view='No'", _connection);
        SqlDataReader rdrview = cmd.ExecuteReader();
        if (rdrview.Read())
        {
            Response.Write("<script language='javascript'>alert('Permission Restricted. Please Contact Administrator.');window.location='~/Company_Home.aspx';</script>");
        }
        rdrview.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and section_edit='No'", _connection);
        SqlDataReader rdredit = cmd.ExecuteReader();
        if (rdredit.Read())
        {
            btn_Edit.Visible = false;
        }
        rdredit.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and  section_delete='No'", _connection);
        SqlDataReader rdrdel = cmd.ExecuteReader();
        if (rdrdel.Read())
        {
            // ((ImageButton)grid_Course.Rows[0].FindControl("img_update")).Visible = false;
        }
        rdrdel.Close();
    }

    public void load_admin()
    {
        myConnection.Open();
        SqlDataAdapter ad = new SqlDataAdapter("select * from paym_branch", myConnection);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        ddl_branch.DataTextField = "branchname";
        ddl_branch.DataValueField = "pn_branchid";
        ddl_branch.DataSource = ds;
        ddl_branch.DataBind();
        ddl_branch.Items.Insert(0, "Select Branch");
        myConnection.Close();
    }

    public void hr()
    {
        myConnection.Open();
        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM bonus_increment where pn_BranchID='" + employee.BranchId + "'", myConnection);

        DataSet ds = new DataSet();
        ad.Fill(ds, "bonus_increment");

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
        btn_cancel.Visible = false;
        btn_Edit.Text = "Edit";
        btn_Add.Visible = true;
        myConnection.Close();
        ddl_branch.Visible = false;
    }


    public void clear()
    {
        txtPointsAmount.Value = "";
        txtpointamount1.Value = "";
        txt_formulaName.Value = "";
        //ddl_grade.SelectedItem = ddl_grade.Items[0];
        ddl_Inctype.Text = "Amount";
        //optional_type.Visible = false;
    }
    
    protected void Edit(object sender, GridViewEditEventArgs e)
    {
        try
        {
            
        }
        catch (Exception ex)
        {
            
        }
    }

    protected void Delete(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void Update(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            
        }
        catch (Exception ex)
        {          

        }

    }


    protected void ddl_Inctype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Inctype.SelectedItem.Text == "Percentage")
        {
            lbl_increment.Text = "Percentage";
            //rdo_percentage.Visible = true;
        }
        else if (ddl_Inctype.SelectedItem.Text == "Amount")
        {
            lbl_increment.Text = "Amount";
        }
        else if (ddl_Inctype.SelectedItem.Text == "Average")
        {
            lbl_increment.Text = "Percentage";
            //rdo_percentage.Visible = true;
            lbl_increment1.Visible = true;
            txtpointamount1.Visible = true;
            //optional_type.Visible = true;
        }
        else if (ddl_Inctype.SelectedItem.Text == "Whichever is higher")
        {
            lbl_increment.Text = "Percentage";
            //rdo_percentage.Visible = true;
            lbl_increment1.Visible = true;
            txtpointamount1.Visible = true;
            //optional_type.Visible = true;
        }
        else
        {
            lbl_increment.Text = "Percentage";
            //rdo_percentage.Visible = true;
            lbl_increment1.Visible = true;
            txtpointamount1.Visible = true;
            //optional_type.Visible = true;
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {      
        if (e.Row.RowState != DataControlRowState.Edit) // check for RowState
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //check for RowType
            {
                string BonusID = ((Label)e.Row.FindControl("lbl_id")).Text;
                //string id = e.Row.Cells[0].Text; // Get the id to be deleted
                //cast the ShowDeleteButton link to linkbutton
                //LinkButton lb = (LinkButton)e.Row.Cells[5].Controls[2];
                ImageButton lb = (ImageButton)e.Row.Cells[5].Controls[2];
                if (lb != null)
                {
                    //attach the JavaScript function with the ID as the paramter
                    lb.Attributes.Add("onclick", "return ConfirmOnDelete();");
                }
            }
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string ID = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lbl_id")).Text;

        DeleteRecord(ID);
        hr();
    }

    private void DeleteRecord(string ID)
    {

        string sqlStatement = "DELETE FROM Bonus_increment WHERE pn_BonusID = @pn_BonusID";
        try
        {
            myConnection.Open();
            SqlCommand cmd = new SqlCommand(sqlStatement, myConnection);
            cmd.Parameters.AddWithValue("@pn_BonusID", ID);
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
    protected void ddl_branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (s_login_role == "a")
        {
            employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
        }
        hr();
        
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        try
        {
            string rdo = "NULL";
            string b_amt = txtPointsAmount.Value + ".00";
            myConnection.Open();
            cmd = new SqlCommand("insert into bonus_increment(pn_CompanyID,pn_BranchID,grade,Bonus_type,Bonus,formula_name,percent_increment) values('" + employee.CompanyId + "','" + employee.BranchId + "','" + ddl_grade.Text + "','" + ddl_Inctype.Text + "','" + b_amt + "','" + txt_formulaName.Value + "','" + rdo + "')", myConnection);
            cmd.ExecuteNonQuery();
            myConnection.Close();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Saved Successfully!');", true);
            clear();
            hr();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
        }
    }
    protected void btn_Edit_Click(object sender, EventArgs e)
    {
        myConnection.Open();
        if (btn_Edit.Text == "Edit")
        {

            if (txt_formulaName.Value == "")
            {
                Label7.Text = "Please enter the Bonus code and press EDIT..";
            }
            else
            {
                try
                {
                    Label7.Text = "";
                    cmd = new SqlCommand("select * from bonus_increment where pn_BranchID='" + employee.BranchId + "' and pn_companyID='" + employee.CompanyId + "' and Formula_name='" + txt_formulaName.Value + "'", myConnection);
                    SqlDataReader dr_app = cmd.ExecuteReader();
                    if (dr_app.Read())
                    {
                        ddl_grade.SelectedItem.Text = Convert.ToString(dr_app["Grade"]);
                        //ddl_Inctype.SelectedItem.Text = Convert.ToString(dr_app["Increment_Type"]);
                        txtPointsAmount.Value = Convert.ToString(dr_app["Bonus"]);
                        txt_formulaName.Value = Convert.ToString(dr_app["Formula_name"]);
                        btn_Edit.Text = "Update";
                        btn_Add.Visible = false;
                        btn_cancel.Visible = true;

                    }
                    else
                    {
                        Label7.Text = "Bonus Code not found";
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
                }
            }

        }
        else
        {
            try
            {
                cmd = new SqlCommand("update bonus_increment set Grade = '" + ddl_grade.SelectedItem.Text + "' , Bonus_Type = '" + ddl_Inctype.SelectedItem.Text + "' , Bonus = '" + txtPointsAmount.Value + "' where Formula_name='" + txt_formulaName.Value + "' and pn_companyID = '" + employee.CompanyId + "' and pn_BranchID = '" + employee.BranchId + "'", myConnection);
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Updated Successfully!');", true);
                myConnection.Close();
                hr();
                clear();
            }

            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
            }
        }

        myConnection.Close();
    }
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        hr();
        clear();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        hr();
        //bindGrade();
        //bindDepDetails();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string BonusID = ((Label)GridView1.Rows[e.RowIndex].FindControl("lbl_id")).Text;
        string Grade = ((DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddlGrade")).SelectedItem.Text;
        string BonusType = ((DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddlBonusType")).SelectedItem.Text;
        string BonusValue = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtBonus")).Text;
        string BonusCode = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtBonusCode")).Text;
        myConnection.Open();
        SqlCommand cmd = new SqlCommand("update Bonus_increment set Grade='" + Grade + "',Bonus_type='" + BonusType + "',Bonus='" + BonusValue + "',formula_name='" + BonusCode + "' where pn_CompanyID='" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "'and pn_BonusID='" + BonusID+"'",myConnection);
        cmd.ExecuteNonQuery();
        myConnection.Close();
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(' Record Updated');", true);
        GridView1.EditIndex = -1;
        hr();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex; // turn to edit mode
        hr();
        myConnection.Open();
        SqlCommand cmd = new SqlCommand("select pn_BonusID,Grade from Bonus_increment", myConnection);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        ((DropDownList)GridView1.Rows[e.NewEditIndex].FindControl("ddlGrade")).DataSource = ds;
        ((DropDownList)GridView1.Rows[e.NewEditIndex].FindControl("ddlGrade")).DataValueField = "pn_BonusID";
        ((DropDownList)GridView1.Rows[e.NewEditIndex].FindControl("ddlGrade")).DataTextField = "Grade";
        ((DropDownList)GridView1.Rows[e.NewEditIndex].FindControl("ddlGrade")).DataBind();
        myConnection.Close();

        myConnection.Open();
        SqlCommand cmdtype = new SqlCommand("select pn_BonusID,Bonus_type from Bonus_increment",myConnection);
        SqlDataAdapter daa = new SqlDataAdapter(cmdtype);
        DataSet dss = new DataSet();
        daa.Fill(dss);
        ((DropDownList)GridView1.Rows[e.NewEditIndex].FindControl("ddlBonusType")).DataSource = dss;
        ((DropDownList)GridView1.Rows[e.NewEditIndex].FindControl("ddlBonusType")).DataValueField = "pn_BonusID";
        ((DropDownList)GridView1.Rows[e.NewEditIndex].FindControl("ddlBonusType")).DataTextField = "Bonus_type";
        ((DropDownList)GridView1.Rows[e.NewEditIndex].FindControl("ddlBonusType")).DataBind();
        myConnection.Close();       
    }       
}
