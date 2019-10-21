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
using ePayHrms.Candidate;
using ePayHrms.Employee;
using CrystalDecisions.Web;
using CrystalDecisions.Enterprise;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.Reporting;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

public partial class PayrollReports_EncashmentReport : System.Web.UI.Page
{
    ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataTable dt = new DataTable();
    DataTable dtable = new DataTable();

    Company company = new Company();
    Employee employee = new Employee();
    PayRoll pay = new PayRoll();
    Candidate c = new Candidate();
    Collection<Company> CompanyList;
    Collection<Employee> EmployeeList;
    Collection<Employee> DepartmentList;
    int temp_count = 0;
    int company_Id, branch_Id, valid, temp_valid = 0, check, ddl_i, i;
    string _Value;
    char s_login_role;
    bool grd_chk = true;
    string s_form = "";
    DataSet ds_userrights;
    string query = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string s = "'dafa'";
        Response.Write(s);

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
            date_load();
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
    public void date_load()
    {
        DateTime tnow = DateTime.Now;
        ArrayList AlYear = new ArrayList();
        int i;
        for (i = 2002; i <= 2022; i++)
            AlYear.Add(i);

        if (!this.IsPostBack)
        {
            ddl_Year.DataSource = AlYear;
            ddl_Year.DataBind();
            ddl_Year.SelectedValue = tnow.Year.ToString();
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
    protected void ddl_dept_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkEmployee_load();
    }

    public void chkEmployee_load()
    {
        employee.DepartmentId = Convert.ToInt32(ddl_dept.SelectedItem.Value);
        ViewState["pn_DepartmentId"] = Convert.ToInt32(ddl_dept.SelectedItem.Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        string str1 = ddl_dept.SelectedItem.Text;
        employee.DivisionName = str1;
        string qry = "Select a.pn_EmployeeID,a.EmployeeCode,a.Employee_First_Name from paym_employee a, paym_employee_profile1 b where a.pn_CompanyID=b.pn_CompanyID and a.pn_BranchID=b.pn_BranchID and a.pn_EmployeeID=b.pn_EmployeeID and b.pn_DepartmentId=" + ddl_dept.SelectedValue + " and b.pn_CompanyID=" + employee.CompanyId + " and a.salary_Type='Month' and a.status='y' and b.pn_BranchID=" + employee.BranchId;

        EmployeeList = employee.fn_getEmplist(qry);
        if (EmployeeList.Count > 0)
        {
            chk_Empcode.DataSource = EmployeeList;
            chk_Empcode.DataValueField = "EmployeeId";
            chk_Empcode.DataTextField = "LastName";
            chk_Empcode.DataBind();
        }
        else
        {
            chk_Empcode.Items.Clear();
        }
    }

    protected void ddl_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btn_save_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            query = "delete from Temp_Encashment";
            employee.fn_reportbyid(query);

            for (i = 0; i < chk_Empcode.Items.Count; i++)
            {
                if (chk_Empcode.Items[i].Selected == true)
                {
                    query = "insert into Temp_Encashment values(" + employee.CompanyId + "," + employee.BranchId + "," + chk_Empcode.Items[i].Value + ", '" + ddl_Year.SelectedValue + "')";
                    employee.fn_reportbyid(query);
                    temp_count++;
                }
            }

            Session["preview_page"] = "~/Default.aspx";
            Session["ReportName"] = "~/crystalreports/Encashment_Details.rpt";
            Response.Redirect("Report_view.aspx");
        }
        catch (Exception ex) { }

    }
}
