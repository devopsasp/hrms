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

public partial class Hrms_Tasks_Default : System.Web.UI.Page
{
    private SqlConnection _connection;
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
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
    string eid;
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
        if (!IsPostBack)
        {
            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {

                switch (s_login_role)
                {

                    case "a":
                       // hr();
                        break;

                    case "h":
                        txt_qsetno.Visible = false;
                        ddlqsetload();
                        assign();
                        access();
                       //hr();
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

   
    public void assign()
    {
        myConnection.Open();
        SqlDataAdapter adap = new SqlDataAdapter("SELECT * FROM test_assign where pn_branchID='" + employee.BranchId + "' order by sno ", myConnection);

        DataSet dset = new DataSet();

        adap.Fill(dset, "test_assign");


        if (dset.Tables[0].Rows.Count == 0)
        {
            dset.Tables[0].Rows.Add(dset.Tables[0].NewRow());
            GridView2.DataSource = dset;
            GridView2.DataBind();
            int columnCount = GridView2.Rows[0].Cells.Count;
            GridView2.Rows[0].Cells.Clear();
            GridView2.Rows[0].Cells.Add(new TableCell());
            GridView2.Rows[0].Cells[0].ColumnSpan = columnCount;
            GridView2.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            GridView2.DataSource = dset;
            GridView2.DataBind();
        }
        myConnection.Close();
    }

    public void access()
    {
        _connection = con.fn_Connection();
        _connection.Open();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=4 and section_view='No'", _connection);
        SqlDataReader rdrview = cmd.ExecuteReader();
        if (rdrview.Read())
        {
            Response.Write("<script language='javascript'>alert('Permission Restricted. Please Contact Administrator.');window.location='~/Company_Home.aspx';</script>");
        }
        rdrview.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=4 and section_edit='No'", _connection);
        SqlDataReader rdredit = cmd.ExecuteReader();
        if (rdredit.Read())
        {
            //for (int b = 0; b < GridView1.Rows.Count; b++)
            //{
            //    GridView1.Rows[b].Cells[7].Controls[0].Visible = false;
            //}
        }
        rdredit.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=4 and  section_delete='No'", _connection);
        SqlDataReader rdrdel = cmd.ExecuteReader();
        if (rdrdel.Read())
        {
            
            for (int a = 0; a < GridView1.Rows.Count; a++)
            {
                GridView1.Rows[a].Cells[7].Controls[0].Visible = false;
                GridView2.Rows[a].Cells[6].Controls[0].Visible = false;
            }
            
        }
        rdrdel.Close();

    }

    public void ddlqsetload()
    {
        myConnection.Open();
        cmd = new SqlCommand("select distinct pn_questid from online where pn_BranchID = '" + employee.BranchId + "'", myConnection);
        rea1 = cmd.ExecuteReader();
        while (rea1.Read())
        {
            ddl_qsetcode.Items.Add(Convert.ToString(rea1["pn_questID"]));
        }
        myConnection.Close();
    }

    public void hr()
    {
        //txt_qsetno.Visible=true;
        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM online where pn_questID='"+ddl_qsetcode.SelectedItem.Text+"' and pn_branchID='"+employee.BranchId+"' order by sno ", myConnection);
        DataSet ds = new DataSet();
        ad.Fill(ds, "online");

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
        myConnection.Open();
        cmd = new SqlCommand("select count(*) from online where pn_questID='" + ddl_qsetcode.SelectedItem.ToString() + "' and pn_BranchID = '" + employee.BranchId + "' ", myConnection);
        int cc = (int)cmd.ExecuteScalar();
        cc = cc + 1;
        ((TextBox)GridView1.FooterRow.FindControl("ddl_sno")).Text = cc.ToString();
        myConnection.Close();
    }


    private void DeleteRecord(string ID)
    {

        string sqlStatement = "DELETE FROM online WHERE sno = @sno and pn_questID='" + txt_qsetno.Text + "'";
        try
        {
            myConnection.Open();
            SqlCommand cmd = new SqlCommand(sqlStatement, myConnection);
            cmd.Parameters.AddWithValue("@sno", ID);
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
            string sno = ((TextBox)GridView1.FooterRow.FindControl("ddl_sno")).Text;
            string que = ((TextBox)GridView1.FooterRow.FindControl("txt_que")).Text;
            string opt1 = ((TextBox)GridView1.FooterRow.FindControl("txt_opt1")).Text;
            string opt2 = ((TextBox)GridView1.FooterRow.FindControl("txt_opt2")).Text;
            string opt3 = ((TextBox)GridView1.FooterRow.FindControl("txt_opt3")).Text;
            string opt4 = ((TextBox)GridView1.FooterRow.FindControl("txt_opt4")).Text;
            string ans = ((DropDownList)GridView1.FooterRow.FindControl("ddl_ans")).Text;
            AddNewRecord(sno, que, opt1, opt2, opt3, opt4, ans);
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string ID = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lbl_sno")).Text;
        DeleteRecord(ID);
        hr();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex; // turn to edit mode
        hr();
    }

    private void AddNewRecord(string sno , string que, string opt1, string opt2, string opt3, string opt4, string ans)
    {
        myConnection.Open();
        txt_qsetno.Visible = true;
        //txt_qsetno.Text = ddl_qsetcode.SelectedItem.Text;
        cmd1 = new SqlCommand("select count(*) from online where pn_questID='" + txt_qsetno.Text + "' and pn_BranchID = '"+employee.BranchId+"'", myConnection);
        int cc1 = (int)cmd1.ExecuteScalar();
        if (cc1 >= 50)
        {
            ClientScriptManager manager = Page.ClientScript;
            manager.RegisterStartupScript(this.GetType(), "Call", "show_message('Only maximum of 50 questions allowed per question set');", true);
        }

        else
        {
            //txt_qsetno.Text = ddl_qsetcode.SelectedItem.Text;
            //if (txt_qsetno.Text != "")
            //{
            //    ClientScriptManager manager = Page.ClientScript;
            //    manager.RegisterStartupScript(this.GetType(), "Call", "show_message('Please enter the question set code.');", true);
            //}

            //else
            //{
                string chk;
                if (ddl_qsetcode.SelectedItem.Text == "New")
                {
                    chk = txt_qsetno.Text;
                }
                else
                {
                    chk = ddl_qsetcode.SelectedItem.Text;
                }
                string cans, sopt="";
                string query = @"INSERT INTO online (pn_CompanyID, pn_BranchID, pn_questID, sno, questions,option1,option2,option3,option4,answer) VALUES('" + employee.CompanyId + "','" + employee.BranchId + "','" + chk + "', '" + sno + "' , '" + que + "','" + opt1 + "','" + opt2 + "','" + opt3 + "','" + opt4 + "','" + ans + "')";

                SqlCommand myCommand = new SqlCommand(query, myConnection);

                myCommand.ExecuteNonQuery();

                myConnection.Close();

                //ddlqsetload();
                ddl_qsetcode.SelectedIndex = 0;
            
                int flg = 0;
                int temp1 = ddl_qsetcode.Items.Count;
                txt_qsetno.Visible = true;
                for (int i = 0; i < temp1;i++ )
                {
                    if (ddl_qsetcode.SelectedItem.Text == txt_qsetno.Text)
                    {
                        flg = 1;
                    }
                    ddl_qsetcode.SelectedIndex = i;
                 }
                if (ddl_qsetcode.SelectedItem.Text != txt_qsetno.Text)
                {
                    ddl_qsetcode.Items.Add(txt_qsetno.Text);
                    int temp = ddl_qsetcode.Items.Count;
                    ddl_qsetcode.SelectedIndex = temp - 1;
                }
                else
                { 
                
                }
                txt_qsetno.Visible = false;

                hr();
                assign();
                myConnection.Open();
                SqlCommand com = new SqlCommand("select * from online where questions='" + que + "'", myConnection);
                SqlDataReader read;
                read = com.ExecuteReader();
                if (read.Read())
                {
                    sopt = Convert.ToString(read["answer"]);
                }
                if (sopt == "1")
                {
                    cans = opt1;
                }
                else if (sopt == "2")
                {
                    cans = opt2;
                }
                else if (sopt == "3")
                {
                    cans = opt3;
                }
                else
                {
                    cans = opt4;
                }
                read.Close();
                com = new SqlCommand("update online set answers = '" + cans + "' where questions = '" + que + "'", myConnection);
                com.ExecuteNonQuery();
                
                myConnection.Close();

            //}
        }      

    }
  
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
     {
        GridViewRow Gvrow = GridView1.Rows[e.RowIndex];
        Label8.Text = Gvrow.ToString();
        txt_qsetno.Visible = true;
        txt_qsetno.Text = ddl_qsetcode.SelectedItem.Text;
        if (Gvrow != null)
        {
            string snoedit = ((Label)Gvrow.FindControl("lbl_sno_edit")).Text;
            string queedit = ((TextBox)Gvrow.FindControl("txt_que_edit")).Text;
            string opt1edit = ((TextBox)Gvrow.FindControl("txt_opt1_edit")).Text;
            string opt2edit = ((TextBox)Gvrow.FindControl("txt_opt2_edit")).Text;
            string opt3edit = ((TextBox)Gvrow.FindControl("txt_opt3_edit")).Text;
            string opt4edit = ((TextBox)Gvrow.FindControl("txt_opt4_edit")).Text;
            string ansedit = ((DropDownList)Gvrow.FindControl("ddl_ans_edit")).Text;
            myConnection.Open();
            cmd = new SqlCommand("update online set questions='" + queedit + "',option1='" + opt1edit + "',option2='" + opt2edit + "',option3='" + opt3edit + "',option4='" + opt4edit + "',answer='" + ansedit + "' where Sno='" + snoedit + "' and pn_questID='" + txt_qsetno.Text + "'", myConnection);
            cmd.ExecuteNonQuery();
            ClientScriptManager manager = Page.ClientScript;
            manager.RegisterStartupScript(this.GetType(), "Call", "show_message('Updated Successfully');", true);
            txt_qsetno.Visible = false;
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

    protected void ddl_qsetcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_qsetcode.SelectedItem.Text == "New")
        {
            txt_qsetno.Visible = true;
        }
        else
        {
            txt_qsetno.Text = ddl_qsetcode.SelectedItem.Text;
            hr();
        }
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        hr();
    }
    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddassi = (DropDownList)GridView2.FooterRow.FindControl("DropDownList1");
        ddassi.Items.Clear();
        DropDownList department = (DropDownList)GridView2.FooterRow.FindControl("DropDownList4");
        string dept = department.SelectedItem.Text;
        //Label8.Text = dept;
        myConnection.Open();
        SqlCommand comm = new SqlCommand("select distinct a.Employee_First_Name, b.v_departmentName, c.pn_EmployeeID, c.pn_DepartmentID from paym_employee a inner join paym_employee_profile1 c on a.pn_employeeid = c.pn_employeeid inner join paym_department b on c.pn_DepartmentID = b.pn_DepartmentID where v_DepartmentName = '" + dept + "'", myConnection);
        SqlDataReader reader;
        reader = comm.ExecuteReader();
        while (reader.Read())
        {
            //ddassi.Items.Clear();
            ddassi.Items.Add(reader[0].ToString());
        }
        reader.Close();
        myConnection.Close();
    }

    private void DeleteRecord1(string ID)
    {

        string sqlStatement = "DELETE FROM test_assign WHERE Sno = @Sno";
        try
        {
            myConnection.Open();
            SqlCommand cmd = new SqlCommand(sqlStatement, myConnection);
            cmd.Parameters.AddWithValue("@Sno", ID);
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


    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "add1")
        {
            string qset = ((DropDownList)GridView2.FooterRow.FindControl("ddl_qid")).Text;
            string dept = ((DropDownList)GridView2.FooterRow.FindControl("DropDownList4")).Text;
            string assi = ((DropDownList)GridView2.FooterRow.FindControl("DropDownList1")).Text;
            string total = ((DropDownList)GridView2.FooterRow.FindControl("ddl_total")).Text;
            string remind = ((TextBox)GridView2.FooterRow.FindControl("tdate")).Text;
            string[] datesplit = remind.Split('/', '-');
            string dd = datesplit[0].ToString();
            string mm = datesplit[1].ToString();
            string yy = datesplit[2].ToString();
            string reminder = mm + "/" + dd + "/" + yy;
            string qry = @"select pn_EmployeeID from paym_Employee where Employee_First_Name = '" + assi + "' and pn_BranchID='" + employee.BranchId + "'";
            SqlCommand com = new SqlCommand(qry, myConnection);
            myConnection.Open();
            SqlDataReader readr = com.ExecuteReader();
            if (readr.Read())
            {
                eid = Convert.ToString(readr["pn_EmployeeID"]);
            }
            myConnection.Close();
            AddNewRecord1(qset, eid, dept, assi, total , reminder);
        }
    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string ID = ((Label)GridView2.Rows[e.RowIndex].Cells[0].FindControl("lbl_sno")).Text;

        DeleteRecord1(ID);
        assign();
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView2.EditIndex = e.NewEditIndex; // turn to edit mode
        assign();
    }
    private void AddNewRecord1(string qset, string eid, string dept, string assi, string total , string reminder)
    {

        string query = @"INSERT INTO test_assign (pn_CompanyID, pn_BranchID, pn_QuestID, pn_DepartmentName, pn_EmployeeID, pn_EmployeeName, total_quest , test_date) VALUES('" + employee.CompanyId + "','" + employee.BranchId + "','" + qset + "','" + dept + "','" + eid + "','" + assi + "','" + total + "' , '"+reminder+"')";

        SqlCommand myCommand = new SqlCommand(query, myConnection);
        myConnection.Open();
        myCommand.ExecuteNonQuery();
        myConnection.Close();
        assign();
    }
    protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow Gvrow = GridView2.Rows[e.RowIndex];
        //Label8.Text = Gvrow.ToString();
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
            assign();

        }
    }
    protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        hr();
    }
    protected void deptedit_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GridViewRow Gvrow1 = GridView2.Rows[e.RowIndex];
        DropDownList ddassi1 = (DropDownList)GridView2.FindControl("assiedit");
        ddassi1.Items.Clear();
        DropDownList department1 = (DropDownList)GridView2.FindControl("deptedit");
        string dept1 = department1.SelectedItem.Text;
        //Label8.Text = dept;
        myConnection.Open();
        SqlCommand comm = new SqlCommand("select distinct a.Employee_First_Name , b.v_departmentName, c.pn_EmployeeID , c.pn_DepartmentID from paym_employee a inner join paym_employee_profile1 c on a.pn_employeeid = c.pn_employeeid inner join paym_department b on c.pn_DepartmentID = b.pn_DepartmentID where v_DepartmentName = '" + dept1 + "'", myConnection);
        SqlDataReader reader1;
        reader1 = comm.ExecuteReader();
        while (reader1.Read())
        {
            ddassi1.Items.Clear();
            ddassi1.Items.Add(reader1[0].ToString());
        }
        reader1.Close();
        myConnection.Close();
    }
    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    Label8.Text = "Mmm";
    //}
}
