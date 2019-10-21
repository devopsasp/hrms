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
    DataSet ds = new DataSet();
    Company company = new Company();

    Employee employee = new Employee();

    Leave l = new Leave();

    Collection<Leave> IncrementList;
    Collection<Company> CompanyList;
    string formula;
    int company_Id, branch_Id, valid, temp_valid = 0, check;
    string _Value, fname;
    string s_login_role;
    bool b_check = true;
    string s_form = "";
    DataSet ds_userrights;
    int start = 0;
    int last = 0;
    string grade = "";
    string pts = "";
    string deptname = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        fname = (string)Session["formulaName"];
        
        l.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        lbl_increment1.Visible = false;
        txtpointamount1.Visible = false;


        //rdo_percentage.Visible = false;
        if (!IsPostBack)
        {

            switch (s_login_role)
            {
                case "a":
                    load_admin();
                    //tb_app_inc.Visible = false;
                    break;

                case "h":
                    //ddl_branch.Visible = false;
                    hr();
                    access();

                    break;

                case "u":
                    s_form = "25";
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
         //btn_formulaedit.Visible = false;
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

    public void hr()
    {
        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM app_increment where pn_BranchID='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "' order by formula_name ", myConnection);

        ds = new DataSet();

        ad.Fill(ds, "app_increment");


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
            //appraisal();

        }
        //btn_cancel.Visible = false;
        //btn_formulaedit.Text = "Edit";
        Button1.Visible = true;
        lbl_increment.Visible = true;
        txtPointsAmount.Visible = true;

    }
    public void appraisal()
    {
        _connection = con.fn_Connection();
        _connection.Open();
        int pts1 = 0;
        int _start = 0;
        string spts = "0";
        if (Session["dept"] != null)
        {
            deptname = Session["dept"].ToString();
        }
        cmd = new SqlCommand("SELECT start_point,last_point,grade from app_increment where pn_companyid='" + employee.CompanyId + "' and department='" + deptname + "'", _connection);
        rea = cmd.ExecuteReader();
        while (rea.Read())
        {

            start = Convert.ToInt32(rea["start_point"]);
            last = Convert.ToInt32(rea["last_point"]);
            grade = rea["grade"].ToString();

            if (Session["points"] != null)
            {
                pts = Session["points"].ToString();
                pts1 = Convert.ToInt32(pts);
            }
            if (start <= pts1 && last >= pts1)
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    //spts = ((Label)GridView1.Rows[i].Cells[3].FindControl("start_point")).Text;
                    spts = GridView1.Rows[i].Cells[3].Text;

                    //if (spts == start)
                    //{
                    //    GridView1.Rows[3].BackColor = System.Drawing.Color.MistyRose;
                    //}
                }


                //{
                //_start=Convert.ToInt32(
                ////_start = Convert.ToInt32(((Label)GridView1.Rows[i].Cells[3].FindControl("start_point")).Text);

                //if (_start == start)
                //{
                //e.Row.BackColor = System.Drawing.Color.MistyRose;

                //}
                //}

            }

            //if (Convert.ToDouble(pts) > start && Convert.ToDouble(pts) < last && grade==e.Row.Cells[2].Text)
            //{

            //    e.Row.BackColor = System.Drawing.Color.MistyRose;
            //}



        }


        _connection.Close();
    }

    public void load_admin()
    {
        myConnection.Open();
        SqlDataAdapter ad = new SqlDataAdapter("select * from paym_branch", myConnection);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        //ddl_branch.DataTextField = "branchname";
        //ddl_branch.DataValueField = "pn_branchid";
        //ddl_branch.DataSource = ds;
        //ddl_branch.DataBind();
        //ddl_branch.Items.Insert(0, "Select Branch");
        myConnection.Close();
    }



    protected void Button1_Click1(object sender, EventArgs e)
    {

        int v = 0;
        if ((ddl_Inctype.SelectedItem.Text == "Amount" && txtPointsAmount.Value == "") || (ddl_Inctype.SelectedItem.Text == "Percentage" && txtpointamount1.Value == "") || (ddl_Inctype.SelectedItem.Text == "Whichever is higher" && (txtPointsAmount.Value == "" || txtpointamount1.Value == "")))
        {
            if (ddl_Inctype.SelectedItem.Text == "Amount")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter the amount');", true);
                txtPointsAmount.Visible = true;
                lbl_increment.Visible = true;
            }
            if (ddl_Inctype.SelectedItem.Text == "Whichever is higher")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter Amount and Percentage!');", true);
                txtPointsAmount.Visible = true;
                lbl_increment.Visible = true;

                txtpointamount1.Visible = true;
                lbl_increment1.Visible = true;
            }
            else if (ddl_Inctype.SelectedItem.Text == "Percentage")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter Percentage!');", true);
                lbl_increment1.Visible = true;
                txtpointamount1.Visible = true;
            }
            else
            {

            }

        }
        else
        {
            if (ddl_Inctype.SelectedItem.Text == "Percentage")
            {
                myConnection.Open();
                cmd = new SqlCommand("insert into app_increment(pn_CompanyID,pn_BranchID,grade,department,start_point,last_point,increment_type,increment,formula_name,percentage) values('" + employee.CompanyId + "','" + employee.BranchId + "','" + ddl_grade.Text + "','" + ddl_dept.Text + "','" + txtFromPoint.Value + "','" + txtToPoint.Value + "','" + ddl_Inctype.SelectedItem.Text + "','" + v + "','" + txt_formulaName.Value + "','" + txtpointamount1.Value + "')", myConnection);
                cmd.ExecuteNonQuery();
                myConnection.Close();
                clear();
                hr();
            }
            else if (ddl_Inctype.SelectedItem.Text == "Amount")
            {
                myConnection.Open();
                cmd = new SqlCommand("insert into app_increment(pn_CompanyID,pn_BranchID,grade,department,start_point,last_point,increment_type,increment,formula_name,percentage) values('" + employee.CompanyId + "','" + employee.BranchId + "','" + ddl_grade.Text + "','" + ddl_dept.Text + "','" + txtFromPoint.Value + "','" + txtToPoint.Value + "','" + ddl_Inctype.SelectedItem.Text + "','" + txtPointsAmount.Value + "','" + txt_formulaName.Value + "','" + v + "')", myConnection);
                cmd.ExecuteNonQuery();
                myConnection.Close();
                clear();
                hr();
            }

            else
            {
                myConnection.Open();
                cmd = new SqlCommand("insert into app_increment(pn_CompanyID,pn_BranchID,grade,department,start_point,last_point,increment_type,increment,formula_name,percentage) values('" + employee.CompanyId + "','" + employee.BranchId + "','" + ddl_grade.Text + "','" + ddl_dept.Text + "','" + txtFromPoint.Value + "','" + txtToPoint.Value + "','" + ddl_Inctype.SelectedItem.Text + "','" + txtPointsAmount.Value + "','" + txt_formulaName.Value + "','" + txtpointamount1.Value + "')", myConnection);
                cmd.ExecuteNonQuery();
                myConnection.Close();
                clear();
                hr();
            }
        }

    }

    public void clear()
    {
        txtFromPoint.Value = "";
        txtToPoint.Value = "";
        txtPointsAmount.Value = "";
        txtpointamount1.Value = "";
        txt_formulaName.Value = "";
        //ddl_Inctype.Text = "Amount";
        ddl_Inctype.SelectedIndex = -1;
        ddl_grade.SelectedIndex = -1;
        ddl_dept.SelectedIndex = -1;
    }



    protected void ddl_Inctype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Inctype.SelectedItem.Text == "Percentage")
        {
            lbl_increment1.Text = "Percentage";

            lbl_increment.Visible = false;
            txtPointsAmount.Visible = false;


            lbl_increment1.Visible = true;
            txtpointamount1.Visible = true;
        }
        else if (ddl_Inctype.SelectedItem.Text == "Amount")
        {
            lbl_increment.Text = "Amount";
            //mei
            lbl_increment.Visible = true;
            txtPointsAmount.Visible = true;

        }
        else if (ddl_Inctype.SelectedItem.Text == "Average")
        {
            lbl_increment.Text = "Amount";

            lbl_increment1.Visible = true;
            txtpointamount1.Visible = true;
            lbl_increment.Visible = true;
            txtPointsAmount.Visible = true;

        }
        else if (ddl_Inctype.SelectedItem.Text == "Whichever is higher")
        {

            lbl_increment.Visible = true;
            txtPointsAmount.Visible = true;
            lbl_increment1.Visible = true;
            txtpointamount1.Visible = true;
            lbl_increment1.Text = "Percentage";



            lbl_increment.Text = "Amount";

            lbl_increment1.Visible = true;
            txtpointamount1.Visible = true;

        }
        else
        {
            lbl_increment.Text = "Amount";

            lbl_increment1.Visible = true;
            txtpointamount1.Visible = true;

            lbl_increment.Visible = true;
            txtPointsAmount.Visible = true;

        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (Session["points"] != null)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "formula_name")) == (string)Session["points"])
                {
                    e.Row.BackColor = System.Drawing.Color.MistyRose;
                    Session["points"] = null;
                }

            }
        }
        else
        {
            //img_btn1.Visible = false;
        }

    }
    //// protected void btn_formulaedit_Click(object sender, EventArgs e)
    ////{
    ////    myConnection.Open();
    ////    if (btn_formulaedit.Text == "Edit")
    ////    {

    ////        if (txt_formulaName.Value == "")
    ////        {
    ////            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Please enter the formula code and press EDIT.');", true);
    ////        }
    ////        else
    ////        {
    ////            try
    ////            {
    ////                cmd = new SqlCommand("select * from app_increment where pn_BranchID='" + employee.BranchId + "' and Formula_name='" + txt_formulaName.Value + "'", myConnection);
    ////                SqlDataReader dr_app = cmd.ExecuteReader();
    ////               if (dr_app.Read())
    ////                {
    ////                    txtFromPoint.Value = Convert.ToString(dr_app["start_point"]);
    ////                    txtToPoint.Value = Convert.ToString(dr_app["last_point"]);
    ////                    ddl_grade.SelectedItem.Text = Convert.ToString(dr_app["Grade"]);
    ////                    ddl_dept.SelectedItem.Text = Convert.ToString(dr_app["department"]);
    ////                    //ddl_Inctype.SelectedItem.Text = Convert.ToString(dr_app["Increment_Type"]);
    ////                    txtPointsAmount.Value = Convert.ToString(dr_app["increment"]);
    ////                    txt_formulaName.Value = Convert.ToString(dr_app["Formula_name"]);
    ////                    txtpointamount1.Value = Convert.ToString(dr_app["Percentage"]);
    ////                    btn_formulaedit.Text = "Update";
    ////                    Button1.Visible = false;
    ////                    btn_cancel.Visible = true;
    ////                    ddl_Inctype.SelectedValue = Convert.ToString(dr_app["increment_type"]);
    ////                    if (ddl_Inctype.SelectedValue == "Percentage")
    ////                    {
    ////                        lbl_increment1.Visible = true;
    ////                        txtpointamount1.Visible = true;
    ////                        lbl_increment.Visible = false;
    ////                        txtPointsAmount.Visible = false;
    ////                    }
    ////                    else
    ////                    {
    ////                        lbl_increment1.Visible = false;
    ////                        txtpointamount1.Visible = false;
    ////                        lbl_increment.Visible = true;
    ////                        txtPointsAmount.Visible = true;
    ////                    }
    ////                }
    ////                else
    ////                {
    ////                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Formula Code not found');", true);
    ////                }

    ////            }

    ////            catch (Exception ex)
    ////            {
    ////                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);

    ////            }
    ////        }

    ////    }
    ////    else
    ////    {
    ////        try
    ////        {
    ////            cmd = new SqlCommand("update app_increment set start_point='" + txtFromPoint.Value + "' , last_point = '" + txtToPoint.Value + "' , Grade = '" + ddl_grade.SelectedItem.Text + "' , department = '" + ddl_dept.SelectedItem.Text + "' , Increment_Type = '" + ddl_Inctype.SelectedItem.Text + "' , increment = '" + txtPointsAmount.Value + "', percentage = '" + txtpointamount1.Value + "' where Formula_name='" + txt_formulaName.Value + "' and  pn_BranchID='" + employee.BranchId + "'", myConnection);
    ////            cmd.ExecuteNonQuery();
    ////            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Updated Successfully');", true);
    ////            hr();
    ////            clear();
    ////        }

    ////        catch (Exception ex)
    ////        {
    ////            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);

    ////        }
    ////    }

    ////    myConnection.Close();
    ////}
    ////protected void btn_cancel_Click(object sender, EventArgs e)
    ////{
    ////    hr();
    ////    clear();
    ////}

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void DeleteRecord(string ID)
    {

        string sqlStatement = "DELETE FROM app_increment WHERE pn_incrementID = @pn_incrementID";
        try
        {
            myConnection.Open();
            SqlCommand cmd = new SqlCommand(sqlStatement, myConnection);
            cmd.Parameters.AddWithValue("@pn_IncrementID", ID);
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
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string ID = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lbl_id")).Text;

        DeleteRecord(ID);
        hr();
    }
    protected void ddl_branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (s_login_role == "a")
        {
            //employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
        }
        hr();
        //tb_app_inc.Visible = true;
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void btn_back_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Hrms_Additional/Appraisalnew.aspx");
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;

        hr();

    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        myConnection.Open();
        
        string frompoint = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_startpoint")).Text;
        string topoint = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_lastpoint")).Text;
        string dept = ((DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddl_deptEdit")).SelectedItem.Text;
        string grade = ((DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddl_gradeEdit")).SelectedItem.Text;
        string inctype=((DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddl_InctypeEdit")).SelectedItem.Text;
        string inc = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_increment")).Text;
        string percent = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_percentage")).Text;
        string formula=((TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_formula")).Text;
        cmd = new SqlCommand("update app_increment set start_point='" + frompoint + "' , last_point = '" + topoint + "' , Grade = '" + grade + "' , department = '" + dept + "' , Increment_Type = '" + inctype + "' , increment = '" + inc + "', percentage = '" + percent + "' where Formula_name='" + formula + "' and  pn_BranchID='" + employee.BranchId + "'", myConnection);
        cmd.ExecuteNonQuery();
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Updated Successfully');", true);
        GridView1.EditIndex = -1;
        hr();
        myConnection.Close();
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
    }
}