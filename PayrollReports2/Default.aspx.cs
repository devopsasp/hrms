using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using ePayHrms.BE.Recruitment;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ePayHrms.BE.Recruitment;
using ePayHrms.Company;
using ePayHrms.Employee;
using CrystalDecisions.Web;
using CrystalDecisions.Enterprise;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.Reporting;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

public partial class _Default : System.Web.UI.Page
{
    ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataTable dt = new DataTable();
    DataTable dtable = new DataTable();

    Company company = new Company();
    Employee employee = new Employee();

    Collection<Company> CompanyList;
    Collection<Employee> EmployeeList;
    Collection<Employee> DepartmentList;
    Collection<Employee> DesignationList;

    int company_Id, branch_Id, valid, temp_valid = 0, check, ddl_i, i;
    string _Value;
    char s_login_role;
    bool grd_chk = true;
    string s_form = "";
    DataSet ds_userrights;
    string query = "";
    protected void Page_Load(object sender, EventArgs e)
    {
 
        Session["Msg_session"] = "";
        Session["Repordid"] = "";
        company.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        company.BranchCompanyId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        ddl_dept.Enabled = true;
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
    
    
        s_login_role = Convert.ToChar(Request.Cookies["Login_temp_Role"].Value);
        lbl_Error.Text = "";
        if (!IsPostBack)
        {
            switch (s_login_role)
            {

                case 'h':
                    ddl_Branch.Visible = false;
                    ddl_department_load1();
                    break;

                case 'u': s_form = "22";
                    ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                    if (ds_userrights.Tables[0].Rows.Count > 0)
                    {
                       // load();
                    }
                    else
                    {
                        Session["Msg_session"] = "Permission Restricted. Please Contact Administrator.";
                        Response.Redirect("~/Company_Home.aspx");
                    }
                    break;

                default: Session["Msg_session"] = "Permission Restricted. Please Contact Administrator";
                    Response.Redirect("~/Company_Home.aspx");
                    break;
            }
        }
    }
  
    public void ddl_department_load1()
    {
        ddl_dept.Items.Clear();
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        if (s_login_role == 'a')
        {
            DepartmentList = employee.fn_getDepartmentList1(Convert.ToInt32(ddl_Branch.SelectedItem.Value));
        }
        else if (s_login_role == 'h')
        {
            DepartmentList = employee.fn_getDepartmentList1(employee.BranchId);
        }

        if (DepartmentList.Count > 0)
        {
            for (ddl_i = -1; ddl_i < DepartmentList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();
                    e_list.Text = "Select";
                    e_list.Value = "0";
                    ddl_dept.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();
                    e_list.Value = DepartmentList[ddl_i].DepartmentId.ToString();
                    e_list.Text = DepartmentList[ddl_i].DepartmentName.ToString();
                    ddl_dept.Items.Add(e_list);
                }
            }
        }
        else
        {
           
            ClientScriptManager manager = Page.ClientScript;
            manager.RegisterStartupScript(this.GetType(), "Call", "show_Error();", true);
        }
    }
    public void hr()
    {
        try
        {
            ddl_Employee.Items.Clear();
            employee.DepartmentId = Convert.ToInt32(ddl_dept.SelectedItem.Value);
            ViewState["pn_DepartmentID"] = Convert.ToInt32(ddl_dept.SelectedItem.Value);
            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
          
            string str1 = ddl_dept.SelectedItem.Text;
            employee.DepartmentName = str1;

            string qry = "Select a.pn_EmployeeID,a.EmployeeCode,a.Employee_First_Name from paym_employee a, paym_employee_profile1 b where a.pn_CompanyID=b.pn_CompanyID and a.pn_BranchID=b.pn_BranchID and a.pn_EmployeeID=b.pn_EmployeeID and b.pn_DepartmentID=" + ddl_dept.SelectedValue + " and b.pn_CompanyID=" + employee.CompanyId + " and a.status='y' and b.pn_BranchID=" + employee.BranchId+"";

            EmployeeList = employee.fn_getEmplist(qry);
            if (EmployeeList.Count > 0)
            {
                ddl_Employee.Visible = true;
                ddl_Employee.Items.Clear();
                for (ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
                {

                    if (ddl_i == -1)
                    {
                        ListItem e_list = new ListItem();
                        e_list.Text = "Select Employee";
                        e_list.Value = "0";
                        ddl_Employee.Items.Add(e_list);
                        ddl_Employee.Visible = true;
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
                lbl_Error.Text = "No Employees Available";
                ListItem e_list = new ListItem();
                e_list.Text = "Select Employee";
                e_list.Value = "0";
                ddl_Employee.Items.Add(e_list);
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    protected void ddl_dept_SelectedIndexChanged(object sender, EventArgs e)
    {
        hr();
    }
    protected void btn_save_Click(object sender, ImageClickEventArgs e)
    {
        query = "delete from temp_employeeid";
        employee.fn_reportbyid(query);

        query = "insert id values('" + employee.CompanyId + "','" + employee.BranchId + "','" + ddl_Employee.SelectedValue + "','" + DateTime.Now.ToString("MM/dd/yyyy") + "')";
        employee.fn_reportbyid(query);

        Session["preview_page"] = "~/Default.aspx";
        Session["ReportName"] = "~/crystalreports/task_Shedule.rpt";
        Response.Redirect("Report_view.aspx");
    }

    protected void ddl_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_Employee_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
}

