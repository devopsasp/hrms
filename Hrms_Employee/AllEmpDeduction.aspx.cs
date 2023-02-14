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
using ePayHrms.BE.Recruitment;
using System.Data.SqlClient;
using ePayHrms.Leave;

public partial class Bank_Loan_Default : System.Web.UI.Page
{
    //private SqlConnection _connection;
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();     
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    //private SqlConnection _con;
    //ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlCommand cmd = new SqlCommand();
    Company company = new Company();
    Employee employee = new Employee();
    PayRoll pay = new PayRoll();
    Be_Recruitment recruitment = new Be_Recruitment();
    Collection<Company> CompanyList, ddlBranchsList;
    Collection<PayRoll> ptlist;
    DataSet ds_userrights;
    string _Value, s_form;
    string s_login_role;
    
    protected void Page_Load(object sender, EventArgs e)
    {    
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);        
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        
        //lbl_Error.Text = "";

        if (!IsPostBack)
        {
            CompanyList = company.fn_getCompany();
            if (CompanyList.Count > 0)
            {
            
            switch (s_login_role)
            {
                case "a":
                    
                    ddl_branch_load();
                        BindDepartment();
                        BindDeduction();
                        break;

                case "h":
                     txt_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
                BindDepartment();
                BindDeduction();
                    ddl_branch.Visible = false;
                    break;

                case "u":
                    s_form = "27";

                    ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                    if (ds_userrights.Tables[0].Rows.Count > 0)
                    {
                   // load();                      
                    }
                    else
                    {
                        Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator.";
                        Response.Redirect("~/Company_Home.aspx");
                    }
                    //hr();
                    //session_check();
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

    public void BindDepartment()
    {
        Collection<Employee> departmentList = employee.fn_Department();
        
        if (departmentList.Count > 0)
        {
            for (int ddl_i = -2; ddl_i < departmentList.Count; ddl_i++)
            {

                if (ddl_i == -2)
                {
                    ListItem e_list = new ListItem();

                    e_list.Text = "Select Department";
                    e_list.Value = "se";
                    ddlDepartment.Items.Add(e_list);
                }

                else if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();

                    e_list.Text = "All";
                    e_list.Value = "All";
                    ddlDepartment.Items.Add(e_list);
                }
                else
                {

                    ListItem e_list = new ListItem();
                    e_list.Value = departmentList[ddl_i].DepartmentId.ToString();
                    e_list.Text = departmentList[ddl_i].DepartmentName.ToString();
                    ddlDepartment.Items.Add(e_list);
                }
            }
        }
    }

    public void BindDeduction()
    {
        Collection<PayRoll> DeductionList = pay.fn_GetDeduction(pay);
        if (DeductionList.Count > 0)
        {
            for (int ddl_i = -1; ddl_i < DeductionList.Count; ddl_i++)
            {

                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();

                    e_list.Text = "Select Deduction";
                    e_list.Value = "se";
                    ddl_deductioncode.Items.Add(e_list);
                }
                else
                {

                    ListItem e_list = new ListItem();
                    e_list.Value = DeductionList[ddl_i].DeductionId.ToString();
                    e_list.Text = DeductionList[ddl_i].DeducationCode.ToString();
                    ddl_deductioncode.Items.Add(e_list);
                }
            }
        }
    }

    public void ddl_branch_load()
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
    }

   

    //public void Bind()
    //{
    //    try
    //    {
    //        SqlDataAdapter ad = new SqlDataAdapter();
    //        if (s_login_role == "a")
    //        {
    //            pay.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
    //        }
    //        SqlCommand cmd = new SqlCommand("select a.EmployeeCode, a.Employee_First_Name  from paym_employee a, paym_employee_profile1 b where a.pn_EmployeeID=b.pn_EmployeeID  and a.pn_companyid = '" + pay.CompanyId + "' and a.pn_BranchID = '" + pay.BranchId + "' and pn_DepartmentId='" + ddlDepartment.SelectedItem+"'", myConnection);

    //        ad = new SqlDataAdapter("select a.EmployeeCode, a.Employee_First_Name  from paym_employee a, paym_employee_profile1 b where a.pn_EmployeeID=b.pn_EmployeeID  and a.pn_companyid = '" + pay.CompanyId + "' and a.pn_BranchID = '" + pay.BranchId + "'", myConnection);
    //        DataSet ds = new DataSet();
    //        ad.Fill(ds, "paym_employee");
    //        if (ds.Tables[0].Rows.Count == 0)
    //        {
    //            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
    //            GridView1.DataSource = ds;
    //            GridView1.DataBind();
    //            int columnCount = GridView1.Rows[0].Cells.Count;
    //            GridView1.Rows[0].Cells.Clear();
    //            GridView1.Rows[0].Cells.Add(new TableCell());
    //            GridView1.Rows[0].Cells[0].ColumnSpan = columnCount;
    //            GridView1.Rows[0].Cells[0].Text = "No Records Found..";
    //        }
    //        else
    //        {
    //            ddlDepartment.DataSource = ds;
    //            ddlDepartment.DataTextField = "";
    //            ddlDepartment.DataValueField = "";
    //            ddlDepartment.DataBind();
    //        }

    //        myConnection.Open();
    //        foreach (GridViewRow ld in GridView1.Rows)
    //        {
    //            SqlDataReader chk;
    //            SqlCommand rd = new SqlCommand();
    //            rd = new SqlCommand("select * from paym_vpf where pn_companyid ='" + pay.CompanyId + "'  and pn_branchid='" + pay.BranchId + "' and pn_EmployeeID = '" + ((Label)ld.FindControl("lbl_empcode")).Text + "'", myConnection);
    //            chk = rd.ExecuteReader();

    //            while (chk.Read())
    //            {
    //                if (chk[7].ToString() == "Percentage")
    //                {
    //                    Radio_calc.SelectedIndex = 1;
    //                }
    //                else
    //                {
    //                    Radio_calc.SelectedIndex = 0;
    //                }

    //                decimal txt_vpfvalue = Convert.ToDecimal(chk[4]);
    //                ((TextBox)ld.FindControl("txt_contribution")).Text = txt_vpfvalue.ToString("0.00");
    //                ((DropDownList)ld.FindControl("ddl_sal")).SelectedValue = chk[5].ToString();
    //                ((Button)GridView1.FooterRow.FindControl("Gbtn_Save")).Text = "Modify";

    //            }
    //        }
    //    }
    //    catch
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Employees Found');", true);
    //    }
    //}


    public void load()
    {
        try
        {

            SqlDataAdapter ad = new SqlDataAdapter();
            if (s_login_role == "a")
            {
                pay.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
            }
            ad = new SqlDataAdapter("SELECT * FROM paym_employee where pn_companyid = '" + pay.CompanyId + "' and pn_BranchID = '" + pay.BranchId + "' ", myConnection);

            DataSet ds = new DataSet();

            ad.Fill(ds, "paym_employee");


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

            foreach (GridViewRow ld in GridView1.Rows)
            {
                SqlDataReader chk;
                SqlCommand rd = new SqlCommand();

                rd = new SqlCommand("select * from paym_vpf where pn_companyid ='" + pay.CompanyId + "'  and pn_branchid='" + pay.BranchId + "' and pn_EmployeeID = '" + ((Label)ld.FindControl("lbl_empcode")).Text + "'", myConnection);

                chk = rd.ExecuteReader();

                while (chk.Read())
                {
                    
                    decimal txt_vpfvalue = Convert.ToDecimal(chk[4]);
                    ((TextBox)ld.FindControl("txt_contribution")).Text = txt_vpfvalue.ToString("0.00");
                    ((DropDownList)ld.FindControl("ddl_sal")).SelectedValue = chk[5].ToString();
                    ((Button)GridView1.FooterRow.FindControl("Gbtn_Save")).Text = "Modify";

                }
            }
        }

        catch (Exception ex)
        {
            //lbl_Error.Text = "No Employees Found.";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Employees Found');", true);
        }

    }

    protected void Gbtn_Save_Click(object sender, EventArgs e)
    {
        myConnection.Open();

        for (int i=0; i<GridView1.Rows.Count; i++)
        {
            pay.EmployeeId = Convert.ToInt32(GridView1.DataKeys[i].Value);
            pay.DeductionId = Convert.ToInt32(ddl_deductioncode.SelectedValue);
            pay.Amount = Convert.ToDouble(pay.check_null(((TextBox)GridView1.Rows[i].FindControl("txt_contribution")).Text));
            // pay.Date = Convert.ToDateTime(((TextBox)grd_Deducation.Rows[emp_edu].FindControl("txt_Earn_Amt")).Text);
            pay.d_date = employee.Convert_ToSqlDate(txt_date.Text);
            pay.fromdate = DateTime.Now;
            pay.todate = DateTime.Now;
            // pay.periodCode = "Nov11";

            pay.regular = 'Y';

            _Value = pay.Emp_Deduction(pay);
            
        }
        myConnection.Close();
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Saved Successfully');", true);
        clear();
    }


    public void clear()
    {
        for (int b = 0; b < GridView1.Rows.Count; b++)
        {
            ((TextBox)GridView1.Rows[b].FindControl("txt_contribution")).Text = "";
        }
        ddlDepartment.SelectedIndex = 0;
        ddl_deductioncode.SelectedIndex = 0;
        //txt_date.Text = "";
        txt_amount.Text = "";
    }

    protected void txt_empall_TextChanged(object sender, EventArgs e)
    {
        for (int b = 0; b < GridView1.Rows.Count; b++)
        {

            ((TextBox)GridView1.Rows[b].FindControl("txt_contribution")).Text = ((TextBox)GridView1.FooterRow.FindControl("txt_empall")).Text + ".00";
        }
    }

    protected void txt_contribution_TextChanged(object sender, EventArgs e)
    {
        TextBox thisTextBox = (TextBox)sender;
            GridViewRow currentRow = (GridViewRow)thisTextBox.Parent.Parent;
            int rowindex = 0;
            rowindex = currentRow.RowIndex;
        string s=((TextBox)GridView1.Rows[rowindex].FindControl("txt_contribution")).Text;
        double a = double.Parse(s);   
        
        ((TextBox)GridView1.Rows[rowindex].FindControl("txt_contribution")).Text = a.ToString("F2");       
       
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            SqlDataAdapter ad = new SqlDataAdapter();
            if (s_login_role == "a")
            {
                pay.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
            }

            ad = new SqlDataAdapter("select a.pn_EmployeeID, a.EmployeeCode, a.Employee_First_Name, b.pn_DepartmentId  from paym_employee a, paym_employee_profile1 b where a.pn_EmployeeID=b.pn_EmployeeID  and b.pn_companyid = '" + pay.CompanyId + "' and b.pn_BranchID = '" + pay.BranchId + "' and b.pn_DepartmentId='" + ddlDepartment.SelectedValue + "' and a.status = 'Y'  order by b.pn_gradeid asc", myConnection);
            if (ddlDepartment.SelectedValue == "All")
            {
                ad = new SqlDataAdapter("select a.pn_EmployeeID, a.EmployeeCode, a.Employee_First_Name, b.pn_DepartmentId  from paym_employee a, paym_employee_profile1 b where a.pn_EmployeeID=b.pn_EmployeeID  and b.pn_companyid = '" + pay.CompanyId + "' and b.pn_BranchID = '" + pay.BranchId + "'  and a.status = 'Y'  order by b.pn_gradeid asc", myConnection);
            }
            DataSet ds = new DataSet();

            ad.Fill(ds);


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
            //lbl_Error.Text = "No Employees Found.";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Employees Found');", true);
        }
    }


    protected void txt_amount_TextChanged(object sender, EventArgs e)
    {
        for (int b = 0; b < GridView1.Rows.Count; b++)
        {
            ((TextBox)GridView1.Rows[b].FindControl("txt_contribution")).Text = txt_amount.Text + ".00";
        }
    }
    protected void ddl_deductioncode_SelectedIndexChanged(object sender, EventArgs e)
    {
        myConnection.Open();
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            SqlDataReader chk;
            SqlCommand rd = new SqlCommand();

            rd = new SqlCommand("select * from paym_emp_deduction where pn_companyid ='" + pay.CompanyId + "'  and pn_branchid='" + pay.BranchId + "' and pn_EmployeeID = '" + GridView1.DataKeys[i].Value + "' and pn_deductionID = '" + ddl_deductioncode.SelectedValue + "'", myConnection);
            chk = rd.ExecuteReader();
            if (chk.Read())
            {
                decimal txt_vpfvalue = Convert.ToDecimal(chk[4]);
                ((TextBox)GridView1.Rows[i].FindControl("txt_contribution")).Text = txt_vpfvalue.ToString("0.00");
            }
        }
        myConnection.Close();
    }
}
