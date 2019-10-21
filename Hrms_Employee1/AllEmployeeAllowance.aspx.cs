using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
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
using System.Configuration;
using System.Data;
using ePayHrms.Leave;

public partial class Hrms_Employee_AllEmployeeAllowance : System.Web.UI.Page
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
   
    DataSet ds_userrights;
    string _Value, s_form;
    string s_login_role;
    protected void Page_Load(object sender, EventArgs e)
    {
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        
        if (!IsPostBack)
        {
            switch (s_login_role)
            {
                case "a":

                    //ddl_branch_load();

                    break;

                case "h":

                    BindDepartment();
                    BindAllowance();
                    // load();
                    ddl_branch.Visible = false;

                    break;

                case "u":
                    s_form = "26";

                    ds_userrights = company.check_Userrights(Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value), s_form);

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
    }


    public void BindAllowance()
    {
        Collection<PayRoll> EarningList = pay.fn_Earnings(pay);
        //if (EarningList.Count > 0)
        //{
        //    ddlAllowance.DataSource = EarningList;
        //    ddlAllowance.DataValueField = "EarningsId";
        //    ddlAllowance.DataTextField = "EarningsCode";
        //    ddlAllowance.DataBind();
        //}

        if (EarningList.Count > 0)
        {

            for (int ddl_i = -1; ddl_i < EarningList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();
                    e_list.Text = "Select Allowance";
                    e_list.Value = "0";
                    ddlAllowance.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();
                    e_list.Value = EarningList[ddl_i].EarningsId.ToString();
                    e_list.Text = EarningList[ddl_i].EarningsCode.ToString();
                    ddlAllowance.Items.Add(e_list);
                }
            }
        }
        else
        {
            ListItem e_list = new ListItem();
            e_list.Text = "No Earnings Available";
            e_list.Value = "0";
            ddlAllowance.Items.Add(e_list);
        }
    }
    public void BindDepartment()
    {
        Collection<Employee> departmentList = employee.fn_Department();
        //if (departmentList.Count > 0)
        //{
        //    ddlDepartment.DataSource = departmentList;
        //    ddlDepartment.DataValueField = "DepartmentID";
        //    ddlDepartment.DataTextField = "DepartmentName";
        //    ddlDepartment.DataBind();
        //}

        if (departmentList.Count > 0)
        {

            for (int ddl_i = -1; ddl_i < departmentList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();
                    e_list.Text = "Select Department";
                    e_list.Value = "0";
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
        else
        {
            ListItem e_list = new ListItem();
            e_list.Text = "No Students Available";
            e_list.Value = "0";
            ddlDepartment.Items.Add(e_list);
        }
    }
    public void load()
    {
        try
        {

            SqlDataAdapter ad = new SqlDataAdapter();
            if (s_login_role == "a")
            {
                //pay.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
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
                    if (chk[7].ToString() == "Percentage")
                    {
                        //Radio_calc.SelectedIndex = 1;
                    }
                    else
                    {
                      //  Radio_calc.SelectedIndex = 0;
                    }
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
    protected void txt_empall_TextChanged(object sender, EventArgs e)
    {
        for (int b = 0; b < GridView1.Rows.Count; b++)
        {

            ((TextBox)GridView1.Rows[b].FindControl("txt_contribution")).Text = txt_empall.Text+ ".00";
        }
    }

    protected void txt_contribution_TextChanged(object sender, EventArgs e)
    {
        TextBox thisTextBox = (TextBox)sender;
        GridViewRow currentRow = (GridViewRow)thisTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;
        string s = ((TextBox)GridView1.Rows[rowindex].FindControl("txt_contribution")).Text;
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
                //pay.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
            }

            ad = new SqlDataAdapter("select a.EmployeeCode, a.Employee_First_Name,a.pn_EmployeeID, b.pn_DepartmentId  from paym_employee a, paym_employee_profile1 b where a.pn_EmployeeID=b.pn_EmployeeID  and b.pn_companyid = '" + pay.CompanyId + "' and b.pn_BranchID = '" + pay.BranchId + "' and b.pn_DepartmentId='" + ddlDepartment.SelectedValue + "'  and a.status = 'Y'  order by b.pn_gradeid asc", myConnection);
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

            myConnection.Open();
            
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
        for (int emp_edu = 0; emp_edu < GridView1.Rows.Count; emp_edu++)
        {
            pay.EarningsId = Convert.ToInt32(ddlAllowance.SelectedValue);
            pay.d_date = employee.Convert_ToSqlDate(txtdate.Text);
            pay.fromdate = DateTime.Now;
            pay.todate = DateTime.Now;
            pay.regular = 'Y';
            pay.EmployeeId = Convert.ToInt32(GridView1.DataKeys[emp_edu].Value);
            pay.Amount = Convert.ToDouble(pay.check_null(((TextBox)GridView1.Rows[emp_edu].FindControl("txt_contribution")).Text));
            _Value = pay.Emp_Earnings(pay);
        }
        if (_Value != "1")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Saved Successfully');", true);
            clear();
        }
    }

    public void clear()
    {
        for (int b = 0; b < GridView1.Rows.Count; b++)
        {
            ((TextBox)GridView1.Rows[b].FindControl("txt_contribution")).Text = "";
        }
        ddlDepartment.SelectedIndex = 0;
        ddlAllowance.SelectedIndex = 0;
        //txt_date.Text = "";
        txt_empall.Text = "";
    }
    protected void ddlAllowance_SelectedIndexChanged(object sender, EventArgs e)
    {
        myConnection.Open();
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            SqlDataReader chk;
            SqlCommand rd = new SqlCommand();

            rd = new SqlCommand("select * from paym_Emp_Earnings where pn_CompanyID ='" + pay.CompanyId + "'  and pn_BranchID='" + pay.BranchId + "' and pn_EmployeeID = '" + GridView1.DataKeys[i].Value + "' and pn_EarningsID = '" + ddlAllowance.SelectedValue + "'", myConnection);
            chk = rd.ExecuteReader();
            if (chk.Read())
            {
                decimal txt_vpfvalue = Convert.ToDecimal(chk[6]);
                ((TextBox)GridView1.Rows[i].FindControl("txt_contribution")).Text = txt_vpfvalue.ToString("0.00");
                txtdate.Text = Convert.ToDateTime(chk["d_date"]).ToString("dd/MM/yyyy");
            }
        }
        myConnection.Close();
    }
}

