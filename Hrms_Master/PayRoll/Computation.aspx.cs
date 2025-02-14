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
using System.Data.SqlClient;

public partial class Hrms_Master_Default : System.Web.UI.Page
{
    private SqlConnection _connection;
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    SqlDataReader rea;
    Company company = new Company();

    Employee employee = new Employee();
    PayRoll pay = new PayRoll();
    Collection<PayRoll> EarningsList;
    Collection<Employee> EmpFirstList;
    Collection<Company> BranchsList;
    Collection<Company> CompanyList;
    int company_Id, branch_Id, valid, temp_valid = 0, check = 0;
    string _Value;
    string s_login_role;
    string s_form = "";
    DataSet ds_userrights;

   

    protected void Page_Load(object sender, EventArgs e)
    {
        
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        //if (s_login_role == "a")
        //{
        //    employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
        //}
        if (!IsPostBack)
        {

            switch (s_login_role)
            {
                case "a":
                    load_admin();
                    //ddl_category.Visible = false;
                    ddl_branch.Visible =true;
                    
                    //RadioButtonList1.Visible = false;
                    break;

                case "h":
                    ddl_branch.Visible = false;
                  
                    load_page();
                    access();
                    ddl_load();   
                    break;

                case "u": s_form = "6";
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
       // category();
        SqlDataAdapter ad = new SqlDataAdapter();
        string category1 = ((DropDownList)GridView1.HeaderRow.FindControl("ddl_category")).SelectedItem.Text;
        if (category1 != "All Category")
        {
            if (s_login_role == "a")
            {
                ad = new SqlDataAdapter("SELECT * FROM otslab where pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + ddl_branch.SelectedItem.Value + "' and pn_category = '" + category1 + "' order by slab_id asc ", myConnection);
            }

            if (s_login_role == "h")
            {
                ad = new SqlDataAdapter("SELECT * FROM otslab where pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' order by slab_id asc ", myConnection);
            }
        }
        else
        {
            if (s_login_role == "a")
            {
                ad = new SqlDataAdapter("SELECT * FROM otslab where pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + ddl_branch.SelectedItem.Value + "'  order by slab_id asc ", myConnection);
            }

            if (s_login_role == "h")
            {
                ad = new SqlDataAdapter("SELECT * FROM otslab where pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' order by slab_id asc ", myConnection);
            }
        }

        DataSet ds = new DataSet();

        ad.Fill(ds, "otslab");

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

    public void load1()
    {
        SqlDataAdapter ad = new SqlDataAdapter();
        if (s_login_role == "a")
        {
            ad = new SqlDataAdapter("SELECT * FROM otslab where pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + ddl_branch.SelectedItem.Value + "'  order by slab_id asc ", myConnection);
        }

        if (s_login_role == "h")
        {
            ad = new SqlDataAdapter("SELECT * FROM otslab where pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "' order by slab_id asc ", myConnection);
        }


        DataSet ds = new DataSet();

        ad.Fill(ds, "otslab");

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



    public void load_page()
    {
        try
        {
            string g = "";
            myConnection.Open();
            SqlDataAdapter ad = new SqlDataAdapter();
            SqlCommand com = new SqlCommand("SELECT type FROM paym_computation where pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "'", myConnection);
            SqlDataReader read = com.ExecuteReader();
            if (read.Read())
            {
                g = read[0].ToString();
            }
            if (g == "Gross Salary")
            {
                RadioButtonList1.SelectedIndex = 1;
                btn_refresh.Visible = true;
            }
            else
            {
                btn_refresh.Visible = false;
                GridView1.Visible = false;
            }
            if (s_login_role == "a")
            {
                ad = new SqlDataAdapter("SELECT * FROM paym_computation where pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + ddl_branch.SelectedItem.Value + "'", myConnection);
            }
            if (s_login_role == "h")
            {
                ad = new SqlDataAdapter("SELECT * FROM paym_computation where pn_companyid = '" + employee.CompanyId + "' and  pn_BranchID = '" + employee.BranchId + "'", myConnection);
            }

            DataSet ds = new DataSet();

            ad.Fill(ds, "paym_computation");


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

        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }

        finally
        {
            myConnection.Close();
        }
    }


    public void load_admin()
    {
        myConnection.Open();

        SqlDataAdapter ad = new SqlDataAdapter("select * from paym_branch", myConnection);
        DataSet ds = new DataSet();
        ad.Fill(ds, "paym_branch");
        ddl_branch.DataSource = ds;
        ddl_branch.DataTextField = "branchname";
        ddl_branch.DataValueField = "pn_branchid";
        ddl_branch.DataBind();

        ddl_branch.Items.Insert(0, "Select Branch");
        myConnection.Close();

        
    }

        
    public void access()
    {
        // MessageBox.Show(employee.BranchId.ToString());
        // MessageBox.Show(employee.CompanyId.ToString());
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

        }
        rdredit.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and  section_delete='No'", _connection);
        SqlDataReader rdrdel = cmd.ExecuteReader();
        if (rdrdel.Read())
        {

        }
        rdrdel.Close();

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


    protected void Update(object sender, GridViewUpdateEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            
        }

    }

    private void DeleteRecord(string ID)
    {
        
        try
        {
            if (s_login_role == "a")
            {
               
                string sqlStatement = "DELETE FROM paym_computation WHERE id = @id and pn_branchid = '" + ddl_branch.SelectedItem.Value + "'";
                myConnection.Open();
                SqlCommand cmd = new SqlCommand(sqlStatement, myConnection);
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            else
            {
                string sqlStatement = "DELETE FROM paym_computation WHERE id = @id and pn_branchid = '" + employee.BranchId + "'";
                myConnection.Open();
                SqlCommand cmd = new SqlCommand(sqlStatement, myConnection);
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }

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

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        load1();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "add")
        {
            string value = ((TextBox)GridView1.FooterRow.FindControl("txt_value")).Text;
            string ecode = ((DropDownList)GridView1.FooterRow.FindControl("ddl_ecode")).SelectedItem.Text;
            if (value == "" || ecode == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter all the Fields');", true);
                return;
            }
            AddNewRecord(ecode, value);
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // loop all data rows
            foreach (DataControlFieldCell cell in e.Row.Cells)
            {
                // check all cells in one row
                foreach (Control control in cell.Controls)
                {
                    // Must use LinkButton here instead of ImageButton
                    // if you are having Links (not images) as the command button.
                    ImageButton button = control as ImageButton;
                    if (button != null && button.CommandName == "Delete")
                        // Add delete confirmation
                        button.OnClientClick = "if (!confirm('Are you sure " +
                               "you want to delete this record?')) return;";
                }
            }
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (s_login_role == "a")
        {
            
            string ID = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lbl_id")).Text;

            DeleteRecord(ID);
            load1();
            ddl_load();
            access();
        }
        else if(s_login_role == "h")
        {
            string ID = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lbl_id")).Text;
            string name = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lbl_ecode")).Text;
            if (name == "Basic")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Cannot delete the mandatory field.');", true);
                return;
            }
            DeleteRecord(ID);
            load_page();
            ddl_load();
        }

    }

    private void AddNewRecord(string ecode, string value)
    {
        try
        {
            string query = @"INSERT INTO paym_computation VALUES('" + employee.CompanyId + "','" + employee.BranchId + "','" + RadioButtonList1.SelectedItem.Text + "','" + ecode + "','" + value + "')";
            SqlCommand myCommand = new SqlCommand(query, myConnection);
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            load_page();
            ddl_load();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Record Already Exist!');", true);
        }

    }
        


    protected void ddl_branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        load_page();
        ddl_load();
        //((DropDownList)GridView1.HeaderRow.FindControl("ddl_category")).Visible = true;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    
    protected void ddl_category_SelectedIndexChanged1(object sender, EventArgs e)
    {
        load();
        ddl_load();
    }

    public void ddl_load()
    {
        
            myConnection.Open();
            SqlCommand cmd_ddl = new SqlCommand();
            if (s_login_role == "a")
            {
                cmd_ddl = new SqlCommand("select v_EarningsCode,v_EarningsName from paym_earnings where pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + ddl_branch.SelectedItem.Value + "' ", myConnection);
            }
            if (s_login_role == "h")
            {
                cmd_ddl = new SqlCommand("select v_EarningsCode,v_EarningsName from paym_earnings where pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + employee.BranchId + "' ", myConnection);
            }


            SqlDataAdapter ddl = new SqlDataAdapter(cmd_ddl);
            DataSet ds = new DataSet();
            ddl.Fill(ds, "paym_earnings");
            TextBox de = ((TextBox)GridView1.FooterRow.FindControl("txt_value"));
            ((DropDownList)GridView1.FooterRow.FindControl("ddl_ecode")).DataTextField = "v_EarningsName";
            ((DropDownList)GridView1.FooterRow.FindControl("ddl_ecode")).DataValueField = "v_EarningsCode";
            ((DropDownList)GridView1.FooterRow.FindControl("ddl_ecode")).DataSource = ds;
            ((DropDownList)GridView1.FooterRow.FindControl("ddl_ecode")).DataBind();
            ((DropDownList)GridView1.FooterRow.FindControl("ddl_ecode")).Items.Insert(0, "Select");
            ((DropDownList)GridView1.FooterRow.FindControl("ddl_ecode")).Items.Insert(1, "Basic");
            myConnection.Close();
        
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedIndex == 1)
        {
            GridView1.Visible = true;
            btn_refresh.Visible = true;
        }
        else
        {
            GridView1.Visible = false;
            btn_refresh.Visible = false;
        }
    }

    public double function_rd_nearest(double rup)
    {
        if (rup > 0)
        {
            double value;
            value = Math.Round(rup,MidpointRounding.AwayFromZero);
            return value;
        }

        else
        {
            return 0;
        }
    }

    protected void btn_refresh_Click(object sender, EventArgs e)
    {
        try
        {
            myConnection.Open();
            string EarnCode = "", Value = "";
            SqlCommand cmd = new SqlCommand("Select * from paym_computation where pn_companyID = '" + employee.CompanyId + "' and pn_branchID = '" + employee.BranchId + "'", myConnection);
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                EarnCode = read[3].ToString();
                Value = read[4].ToString();
                EarningsList = pay.fn_Earnings(pay);
                if (EarningsList.Count > 0)
                {
                    for (int j = 0; j < EarningsList.Count; j++)
                    {
                        pay.EarningsCode = EarningsList[j].EarningsCode;
                        pay.EarningsId = EarningsList[j].EarningsId;
                        if (EarnCode == pay.EarningsCode)
                        {
                            EmpFirstList = employee.fn_getAllEmployees();
                            if (EmpFirstList.Count > 0)
                            {
                                pay.d_date = DateTime.Now;
                                pay.fromdate = DateTime.Now;
                                pay.todate = DateTime.Now;
                                pay.regular = 'Y';
                                for (int i = 0; i < EmpFirstList.Count; i++)
                                {
                                    
                                    pay.EmployeeId = EmpFirstList[i].EmployeeId;
                                    pay.EmployeeCode = EmpFirstList[i].EmployeeCode;
                                    employee.CTC_salary = EmpFirstList[i].CTC_salary;
                                    pay.Amount = function_rd_nearest((employee.CTC_salary / 100) * Convert.ToDouble(Value));
                                    _Value = pay.Emp_Earnings(pay);
                                }
                            }
                        }
                        else if (EarnCode == "Basic")
                        {
                            EmpFirstList = employee.fn_getAllEmployees();
                            if (EmpFirstList.Count > 0)
                            {
                                pay.d_date = DateTime.Now;
                                pay.fromdate = DateTime.Now;
                                pay.todate = DateTime.Now;
                                pay.regular = 'Y';
                                for (int i = 0; i < EmpFirstList.Count; i++)
                                {
                                    pay.EmployeeId = EmpFirstList[i].EmployeeId;
                                    pay.EmployeeCode = EmpFirstList[i].EmployeeCode;
                                    employee.CTC_salary = EmpFirstList[i].CTC_salary;
                                    pay.Amount = function_rd_nearest((employee.CTC_salary / 100) * Convert.ToDouble(Value));
                                    SqlCommand com = new SqlCommand("update paym_employee set basic_salary = '" + pay.Amount + "' where pn_EmployeeID='" + pay.EmployeeId + "' and pn_BranchID = '" + employee.BranchId + "' and pn_CompanyID='" + employee.CompanyId + "'", myConnection);
                                    com.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
            }
            
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + ex.Message + "');", true);
        }
        finally
        {
            myConnection.Close();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Saved Successfully');", true);
        }
    }

}


    

            
            
            
            
            
            
            
