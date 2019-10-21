using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using ePayHrms.Login;
using ePayHrms.Connection;
using ePayHrms.Candidate;
using System.IO;
using ePayHrms.Company;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ePayHrms.BE.Recruitment;
using ePayHrms.Employee;
using ePayHrms.Leave;
using CrystalDecisions.Web;
using CrystalDecisions.Enterprise;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.Reporting;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using System.Configuration;


public partial class PayrollReports_PF_NomineeDetails_Rpt : System.Web.UI.Page
{
    ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand cmd1 = new SqlCommand();
    Company company = new Company();
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    Candidate c = new Candidate();
    Leave l = new Leave();

    Collection<Company> CompanyList, ddlBranchsList;
    Collection<Employee> DepartmentList;
    Collection<Employee> EmployeeList;
    Collection<PayRoll> PayList;
    string s_login_role;
    DataSet ds_userrights;
    string s_form = "";
    int i = 0, j, temp_count = 0;
    int ddl_i = 0;
    string EmpName, Emp_Father, Emp_Dob, Emp_Gender, Emp_MaritalStatus, Acc_No, Emp_Per_Address, Emp_Temp_Address, Emp_DoJ, Nomi_Name, Nomi_Address, Nomi_Dob, Nomi_Relation, Pf_Share, F_MemberName, F_Mem_Address, F_Mem_Dob, F_Mem_Relation;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        Session["Repordid"] = "";
        Session["fdate"] = "";
        Session["tdate"] = "";

        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);

        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        //lbl_error.Text = "";

        if (!IsPostBack)
        {
            // date_load();

            CompanyList = company.fn_getCompany();
            ListItem li = new ListItem();
            if (CompanyList.Count > 0)
            {
                switch (s_login_role)
                {
                    case "a":
                        //admin();
                        //session_check();
                        
                        ddl_Branch.Visible = true;
                        //ddl_Branch_load();
                        break;

                    case "h":
                        //hr();
                        //session_check();
                        ddl_Branch.Visible = false;
                        
                        ddl_department_load1();
                        //session_check();
                        break;

                    case "u": s_form = "79";
                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            ddl_Branch.Visible = false;

                        }
                        else
                        {
                            Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator.";
                            Response.Redirect("~/Company_Home.aspx");
                        }
                        break;

                    case "e":
                        ddl_Branch.Visible = false;

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
    
    public void ddl_department_load1()
    {
        try
        {
            ddl_dept.Items.Clear();
            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            if (s_login_role == "a")
            {
                DepartmentList = employee.fn_getDepartmentList1(Convert.ToInt32(ddl_Branch.SelectedItem.Value));
            }
            else if (s_login_role == "h")
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
        catch (Exception ex) { }
    }
    public void ddlEmployee_load()
    {
        try
        {
            employee.DepartmentId = Convert.ToInt32(ddl_dept.SelectedItem.Value);
            ViewState["pn_DepartmentId"] = Convert.ToInt32(ddl_dept.SelectedItem.Value);
            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

            string str1 = ddl_dept.SelectedItem.Text;
            employee.DivisionName = str1;
            string qry = "Select a.pn_EmployeeID,a.EmployeeCode,a.Employee_First_Name from paym_employee a, paym_employee_profile1 b where a.pn_CompanyID=b.pn_CompanyID and a.pn_BranchID=b.pn_BranchID and a.pn_EmployeeID=b.pn_EmployeeID and b.pn_DepartmentId=" + ddl_dept.SelectedValue + " and b.pn_CompanyID=" + employee.CompanyId + " and b.pn_BranchID=" + employee.BranchId + " and status='Y' order by EmployeeCode ";

            EmployeeList = employee.fn_getEmplist(qry);
            if (EmployeeList.Count > 0)
            {
                for (ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
                {
                    if (ddl_i == -1)
                    {
                        ListItem e_list = new ListItem();
                        e_list.Text = "Select";
                        e_list.Value = "0";
                        ddl_Employee.Items.Add(e_list);

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
                ddl_Employee.Items.Add("Select");
            }
        }
        catch (Exception ex) { }
    }
    protected void btn_save_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            myConnection.Open();
            cmd1 = new SqlCommand("delete from PF_tempNominee", myConnection);
            cmd1.ExecuteNonQuery();

            cmd1 = new SqlCommand("select a.employee_first_name,a.gender,a.dateofbirth,a.Account_Type,d.fathername,(d.PresentHouseNo+','+d.PresentStreetName+','+d.PresentAddLine1+','+d.PresentCity) as temp_address,(d.PermanentHouseNo+','+d.PermanentStreetName+','+d.PermanentAddLine1+','+d.PermanentCity) as Per_Address,c.Nominee_Name,c.DOB as Dateofbirth,c.RelationShip as RShip,(c.address1+','+c.District+','+C.City+'-'+c.pin_no) as Addres,c.PF_Share ,b.FamilyMember_name,b.DOB,b.RelationShip,(b.address1+','+b.District+','+b.City+'-'+b.pin_no) as Address from Paym_employee a,PF_EPS b,PF_EPF c,paym_Employee_General d where a.Pn_employeeid=d.pn_employeeid and a.pn_employeeid='" + ddl_Employee.SelectedValue + "'", myConnection);
            SqlDataReader dr = cmd1.ExecuteReader();
            if (dr.Read())
            {
                EmpName = dr["employee_first_name"].ToString();
                Emp_Father = dr["fathername"].ToString();
                Emp_Dob = dr["dateofbirth"].ToString();
                Emp_Gender = dr["Gender"].ToString();
                Acc_No = dr["Account_Type"].ToString();
                Emp_Per_Address = dr["Per_Address"].ToString();
                Emp_Temp_Address = dr["temp_address"].ToString();
                Nomi_Name = dr["Nominee_Name"].ToString();
                Nomi_Address = dr["Addres"].ToString();
                Nomi_Dob = dr["Dateofbirth"].ToString();
                Nomi_Relation = dr["RelationShip"].ToString();
                Pf_Share = dr["PF_Share"].ToString();
                F_MemberName = dr["FamilyMember_name"].ToString();
                F_Mem_Address = dr["Address"].ToString();
                F_Mem_Dob = dr["DOB"].ToString();
                F_Mem_Relation = dr["RelationShip"].ToString();
            }
            dr.Close();

            cmd1 = new SqlCommand("set dateformat dmy;Insert Into PF_tempNominee values('" + ddl_Employee.SelectedValue + "','" + EmpName + "','" + Emp_Father + "', '" + Emp_Dob + "','" + Emp_Gender + "','" + Emp_MaritalStatus + "','" + Acc_No + "','" + Emp_Per_Address + "','" + Emp_Temp_Address + "','" + Emp_DoJ + "','" + Nomi_Name + "','" + Nomi_Address + "','" + Nomi_Dob + "','" + Nomi_Relation + "','" + Pf_Share + "','" + F_MemberName + "','" + F_Mem_Address + "','" + F_Mem_Dob + "','" + F_Mem_Relation + "');set Dateformat mdy", myConnection);
            int c = cmd1.ExecuteNonQuery();

            Session["ReportName"] = "~/crystalreports/PF_Nominee_Details.rpt";

            Session["preview_page"] = "~/PF_NomineeDetails_Rpt.aspx";
            Response.Redirect("Report_view.aspx", false);

            myConnection.Close();
        }
        catch (Exception ex) { }
    }

    protected void ddl_dept_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlEmployee_load();
    }

}
