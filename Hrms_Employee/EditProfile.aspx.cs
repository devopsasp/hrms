using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
using System.Web.Services;
using System.Configuration;
using System.Data;

public partial class Hrms_Employee_EditProfile : System.Web.UI.Page
{
    Company company = new Company();
    Employee employee = new Employee();    
    PayRoll pay = new PayRoll();
    Collection<Employee> EmpFirstList;
    Candidate c = new Candidate();
    Collection<Employee> ProfileList;
    Collection<Employee> emp_available;
    string s_login_role;
    string _Value;
    protected void Page_Load(object sender, EventArgs e)
    {
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        c.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        s_login_role = Request.Cookies["Login_temp_Role"].Value;

        if (s_login_role == "e")
        {
            employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
            pay.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
            c.EmployeeID = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
            Response.Cookies["preview_EmployeeID"].Value = employee.EmployeeId.ToString();
        }
        if (s_login_role == "M")
        {
            employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
            pay.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
            c.EmployeeID = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
            Response.Cookies["preview_EmployeeID"].Value = employee.EmployeeId.ToString();
        }

        if (!IsPostBack)
        {
            //role();
            switch (s_login_role)
            {
                case "a":

                    break;

                case "h":
                   
                    dllEmployeeLoad();
                    AccessEmployeeProfile();
                    break;

                case "e":
                    dllEmployeeLoad();
                    AccessEmployeeProfile();
                    //Rolelode.Enabled = false;
                    ddl_rep.Enabled = false;
                    txt_rep.Disabled = true;
                    break;
                case "M":
                    dllEmployeeLoad();
                    AccessEmployeeProfile();
                    //Rolelode.Enabled = false;
                    ddl_rep.Enabled = false;
                    txt_rep.Disabled = true;
                    break;


                case "u":

                    break;


                default:
                    Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                    Response.Redirect("~/Company_Home.aspx");
                    break;
            }        }


    }
    //public void role()
    //{
    //    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);

    //    SqlCommand cmd = new SqlCommand("spr_role_retrieve", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    con.Open();
    //    using (SqlDataReader sdr = cmd.ExecuteReader())
    //    {
    //        DataTable dt = new DataTable();
    //        dt.Load(sdr);

    //        if (dt != null && dt.Rows.Count > 0)
    //        {
    //            foreach (DataRow row in dt.Rows)
    //            {
    //                ListItem e_list = new ListItem();

    //                e_list.Value = row["role_id"].ToString();
    //                e_list.Text = Convert.ToString(row["role_name"]);
    //                Rolelode.Items.Add(e_list);
    //            }
    //        }
    //    }

    //}


    public class Role
    {
        public int RowNo { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int UserId { get; set; }

        // Constructor
        public Role()
        {
            this.RowNo = 0;
            this.RoleId = -1;
            this.RoleName = string.Empty;

            this.UserId = -1;
        }
    }

    public void dllEmployeeLoad()
    {

        EmpFirstList = employee.fn_getReportingList(employee);

        if (EmpFirstList.Count > 0)
        {

            for (int ddl_i = -1; ddl_i < EmpFirstList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();

                    e_list.Text = "Select Employee";
                    e_list.Value = "0";
                    ddl_rep.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();

                    e_list.Value = EmpFirstList[ddl_i].DesignationId.ToString();
                    e_list.Text = EmpFirstList[ddl_i].DesignationName.ToString();
                    ddl_rep.Items.Add(e_list);
                }
            }
        }
    }
    public void AccessEmployeeProfile()
    {
        try
        {
            employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
            pay.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
            c.EmployeeID = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);

            if (s_login_role == "a")
            {
                employee.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
                pay.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
                EmployeeProfile();
            }

            else if (s_login_role == "h")
            {
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                 pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                EmployeeProfile();
            }

            else if (s_login_role == "u")
            {
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                 pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

            }
            else
            {
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                 pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                EmployeeProfile();
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Error Occured.');};", true);
        }
    }

    protected void rdo_btn_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdo_btn.SelectedItem.Value == "N")
        {
            ddl_ot.Visible = false;
            lbl_text.Visible = false;
        }
        else
        {
            ddl_ot.Visible = true;
            lbl_text.Visible = true;
        }
    }

    public void EmployeeProfile()
    {

        ProfileList = employee.fn_get_Emp_first(employee);
        if (ProfileList.Count > 0)
        {
            if (ProfileList[0].EmployeeCode != "")
            {
                txtEmployeeCode.Value = ProfileList[0].EmployeeCode.ToString();
            }
            //if (ProfileList[0].EmployeeCode != "")
            //{
            //    Rolelode.SelectedValue = ProfileList[0].Role.ToString();
            //}
            if (ProfileList[0].FullName != "")
            {
                txtfullname.Value = ProfileList[0].FullName.ToString();
            }
            if (ProfileList[0].FirstName != "")
            {
                txtFirstName.Value = ProfileList[0].FirstName.ToString();
            }
            if (ProfileList[0].MiddleName != "")
            {
                txtMiddleName.Value = ProfileList[0].MiddleName.ToString();
            }
            if (ProfileList[0].LastName != "")
            {
                txtLastName.Value = ProfileList[0].LastName.ToString();
            }
            if (ProfileList[0].PFno != "")
            {
                txt_pfno.Value = ProfileList[0].PFno.ToString();
            }
            if (ProfileList[0].ESIno != "")
            {
                txt_esino.Value = ProfileList[0].ESIno.ToString();
            }
            if (ProfileList[0].Pan_no != "")
            {
                txt_panno.Value = ProfileList[0].Pan_no.ToString();
            }
            if (Convert.ToString(ProfileList[0].ReaderId) != "")
            {
                txt_Readerid.Value = Convert.ToString(ProfileList[0].ReaderId);
            }
            if (ProfileList[0].ReportID != 0)
            {
                ddl_rep.SelectedValue = ProfileList[0].ReportID.ToString();
            }
            if (ProfileList[0].ReportingEmail != "")
            {
                txt_rep.Value = ProfileList[0].ReportingEmail.ToString();
            }
            if (Convert.ToString(ProfileList[0].basic_salary) != "")
            {
                txt_basicsal.Value = Convert.ToString(ProfileList[0].basic_salary + ".00");
            }
            if (ProfileList[0].d_birth.ToShortDateString() != "01/01/1900")
            {
                txt_dob.Text = ProfileList[0].d_birth.ToString("dd/MM/yyyy");
            }
            if (ProfileList[0].Salary_Type != "")
            {
                rdo_saltype.SelectedValue = ProfileList[0].Salary_Type.ToString();
            }

            rdo_tds.SelectedValue = ProfileList[0].TDS.ToString();

            rdo_btn.SelectedValue = ProfileList[0].OT_Eligible.ToString();

            if (Convert.ToChar(ProfileList[0].OT_Eligible) == 'Y')
            {
                ddl_ot.Visible = true;
            }

            ddl_status.SelectedValue = ProfileList[0].status.ToString();


            ddl_ot.SelectedValue = ProfileList[0].OT_calc.ToString();

            if (ProfileList[0].Gender != "")
            {
                ddl_gender.SelectedValue = ProfileList[0].Gender;
            }
            if (ProfileList[0].Flag == "N")
            {
                chk_rep.Checked = false;
            }
            else
            {
                chk_rep.Checked = true;
            }
        }

        ProfileList = employee.fn_get_Emp_general(employee);
        if (ProfileList.Count > 0)
        {
            if (ProfileList[0].Religion != "")
            {
                txtReligion.Value = ProfileList[0].Religion.ToString();
            }

            if (ProfileList[0].Nationality != "")
            {
                txtNationality.Value = ProfileList[0].Nationality.ToString();
            }

            if (ProfileList[0].BloodGroup != "None")
            {
                ddl_blood.SelectedValue = ProfileList[0].BloodGroup.ToString();
            }

            rdo_salutation.SelectedValue = ProfileList[0].Salutation.ToString();
            ddl_marital.SelectedValue = ProfileList[0].MaritalStatus.ToString();

            if (ProfileList[0].EmailId != "")
            {
                txtEmail.Text = ProfileList[0].EmailId.ToString();
            }
            if (ProfileList[0].IDtype != "Select")
            {
                ddl_idtype.SelectedValue = ProfileList[0].IDtype.ToString();
            }
            if (ProfileList[0].IDno != "")
            {
                txtIDNo.Value = ProfileList[0].IDno.ToString();
            }
        }
    }
    
    protected void btn_Back_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Employee_Preview.aspx");
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Error Occured.');};", true);
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                employee.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
            }

            if (s_login_role == "h")
            {
                employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                save();               
            }            

            if (_Value != "1")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Updated Successfully.');};", true);

            }            
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Error Occured.');};", true);
        }
    }
    public void save()
    {
       // employee.Role = Convert.ToInt32(Rolelode.SelectedItem.Value);
        employee.EmployeeCode = txtEmployeeCode.Value;
        Response.Cookies["emp_Code"].Value = txtEmployeeCode.Value;
        employee.TDS = Convert.ToChar(rdo_tds.SelectedItem.Text);
        employee.OT_Eligible = Convert.ToChar(rdo_btn.SelectedItem.Value);
        employee.OT_calc = Convert.ToDouble(ddl_ot.SelectedItem.Value);
        employee.FullName = txtfullname.Value;
        employee.FirstName = txtFirstName.Value;
        employee.MiddleName = txtMiddleName.Value;
        employee.LastName = txtLastName.Value;
        employee.PFno = txt_pfno.Value;
        employee.ESIno = txt_esino.Value;
        employee.Pan_no = txt_panno.Value;
        employee.ReaderId = Convert.ToInt32(txt_Readerid.Value);
        employee.EmailId = txtEmail.Text;
        
        if (ddl_rep.SelectedItem.Text == "Select Employee")
        {
            employee.Reporting = "";
            employee.ReportID = 0;
        }
        else
        {
            employee.Reporting = ddl_rep.SelectedItem.Text;
            employee.ReportID = Convert.ToInt32(ddl_rep.SelectedItem.Value);
        }
        employee.ReportingEmail = txt_rep.Value;
        employee.basic_salary = Convert.ToDouble(txt_basicsal.Value);
        employee.Salutation =Convert.ToChar(rdo_salutation.SelectedValue);
        employee.BloodGroup = ddl_blood.SelectedItem.Text;
        employee.d_birth = employee.Convert_ToSqlDate(txt_dob.Text);
        employee.Gender = ddl_gender.SelectedItem.Text;
        employee.MaritalStatus = Convert.ToChar(ddl_marital.SelectedItem.Value);
        employee.Religion = txtReligion.Value;
        employee.Nationality = txtNationality.Value;
        employee.IDOthers = "";
        employee.IDtype = ddl_idtype.SelectedItem.Value;
        employee.IDno = txtIDNo.Value;
        employee.Salary_Type = rdo_saltype.SelectedItem.Text;
        employee.status = Convert.ToChar(ddl_status.SelectedValue);
        employee.EmailId = txtEmail.Text;
        employee.Flag = "N";
        if (chk_rep.Checked == true)
        {
            employee.Flag = "Y";
        }
        _Value = employee.Emp_First(employee);
        _Value = employee.Emp_General(employee);
    }

    

    protected void btn_avail_Click(object sender, EventArgs e)
    {
        try
        {
            emp_available = employee.fn_get_EmployeeID(txtEmployeeCode.Value);
            if (emp_available.Count == 0)
            {
                emp_available = employee.fn_get_TempID(txtEmployeeCode.Value);

                if (emp_available.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Employee Code is Available.');};", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Employee Code already exists.');};", true);                   
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Employee Code already exists.');};", true);
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = ex.Message.ToString();
        }
    }
}