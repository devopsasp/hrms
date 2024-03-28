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

public partial class Hrms_Employee_Default : System.Web.UI.Page
{
    private SqlConnection _connection;
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    //private SqlConnection _con;
    //ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    SqlDataReader rea;
    Company company = new Company();
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    Candidate c = new Candidate();

    Collection<Candidate> WorkHistoryList;

    //Collection<Employee> AllowanceList;

    Collection<Employee> ShiftList;
    Collection<Employee> emp_edu_List;
    Collection<Employee> emp_skills_List, BusList;

    Collection<PayRoll> emp_Earnings_List;
    Collection<PayRoll> emp_Deduction_List;

    Collection<Employee> EmpFirstList;
    Collection<Employee> EmpGeneralList;
    Collection<Employee> EmpProfileList;
    Collection<Employee> EmpWorkList;
    Collection<Employee> emp_Edit;
    Collection<Employee> EmpPhotoList;

    string s_login_role;
    int grk, i = 1, j = 1, k = 1, l = 1, m = 1, n = 1, o = 1, p = 1, t=1;
    string  empcode_name, emp_code, transport_edit;

    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_Error.Text=(string)Session["Msg_session"];
            
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
            Response.Cookies["Employee_Code_FirstLastName"].Value = "";
        }
        if (s_login_role == "M")
        {
            employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
            pay.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
            c.EmployeeID = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
            Response.Cookies["preview_EmployeeID"].Value = employee.EmployeeId.ToString();
            Response.Cookies["Employee_Code_FirstLastName"].Value = "";
        }

        //label_clear();

        if (!IsPostBack)
        {
            if (Request.Cookies["Employee_Code_FirstLastName"].Value != "")
            {
                lbl_empcodename.Text = Request.Cookies["Employee_Code_FirstLastName"].Value;
            }
            else
            {
                lbl_empcodename.Text = Request.Cookies["Login_Name"].Value;
            }

                switch (s_login_role)
                {
                    case "a":
                    employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                    get_employee_details();
                        //grid_training_load();
                        grid_exp_load();
                        grid_qualfication_load();
                        grid_skill_load();
                        grid_earnings_load();
                        grid_deductions_load();
                        grid_OverHeading_load();
                        grid_Asset_load();
                        grid_transport_load();
                        break;

                    case "h":
                      
                        tab_profile.Visible = true;
                        tab_address.Visible = true;
                        tab_bank.Visible = true;
                        tab_date.Visible = true;
                        tab_Dedu.Visible = true;
                        tab_Overhead.Visible = true;
                        tab_Earn.Visible = true;
                        tab_edu.Visible = true;
                        tab_family.Visible = true;
                        tab_photo.Visible = true;
                        tab_ref.Visible = true;
                        tab_Skills.Visible = true;
                        //tab_Train.Visible = true;
                        tab_workdet.Visible = true;
                        tab_workexp.Visible = true;
                        tab_asset.Visible = true;
                        Table_bus.Visible = true;

                        get_employee_details();
                        //grid_training_load();
                        grid_exp_load();
                        grid_qualfication_load();
                        grid_skill_load();
                        grid_earnings_load();
                        grid_deductions_load();
                        grid_OverHeading_load();
                        grid_Asset_load();
                        access();
                        grid_transport_load();
                        break;

                    case "e": //btn_hide();
                        Emp_access();
                    tab_profile.Visible = true;
                    tab_address.Visible = true;
                    tab_bank.Visible = true;
                    tab_date.Visible = true;
                    tab_Dedu.Visible = true;
                    tab_Overhead.Visible = true;
                    tab_Earn.Visible = true;
                    tab_edu.Visible = true;
                    tab_family.Visible = true;
                    tab_photo.Visible = true;
                    tab_ref.Visible = true;
                    tab_Skills.Visible = true;
                    //tab_Train.Visible = true;
                    tab_workdet.Visible = true;
                    tab_workexp.Visible = true;
                    tab_asset.Visible = true;
                    Table_bus.Visible = true;

                    get_employee_details();
                        //grid_training_load();
                        grid_exp_load();
                        grid_qualfication_load();
                        grid_skill_load();
                        grid_earnings_load();
                        grid_deductions_load();
                        grid_OverHeading_load();
                        grid_Asset_load();
                        access();
                        grid_transport_load();
                        break;
                case "M": //btn_hide();
                    Emp_access();
                    tab_profile.Visible = true;
                    tab_address.Visible = true;
                    tab_bank.Visible = true;
                    tab_date.Visible = true;
                    tab_Dedu.Visible = true;
                    tab_Overhead.Visible = true;
                    tab_Earn.Visible = true;
                    tab_edu.Visible = true;
                    tab_family.Visible = true;
                    tab_photo.Visible = true;
                    tab_ref.Visible = true;
                    tab_Skills.Visible = true;
                    //tab_Train.Visible = true;
                    tab_workdet.Visible = true;
                    tab_workexp.Visible = true;
                    tab_asset.Visible = true;
                    Table_bus.Visible = true;

                    get_employee_details();
                    //grid_training_load();
                    grid_exp_load();
                    grid_qualfication_load();
                    grid_skill_load();
                    grid_earnings_load();
                    grid_deductions_load();
                    grid_OverHeading_load();
                    grid_Asset_load();
                    access();
                    grid_transport_load();
                    break;

                case "u":
                        get_employee_details();
                        break;


                    default:
                        Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                        Response.Redirect("~/Company_Home.aspx");
                        break;

                }
        }
    }

    public void Emp_access()
    {
        LinkButton1.Visible = false;
        LinkButton2.Visible = false;
        LinkButton3.Visible = false;
        LinkButton4.Visible = false;
        lbtn_date.Visible = false;
        lbtn_Deduction.Visible = false;
        lbtn_Earnings.Visible = false;
        lbtn_skills.Visible = false;
        //lbtn_training.Visible = false;
        lbtn_WorkDetails.Visible = false;
        lbtn_workexp.Visible = false;
        //lnk_Asset_edit.Visible = false;
        Lnk_edit_bus.Visible = false;
    }

    public void btn_hide()
    {
        lbtn_WorkDetails.Visible = false;
        lbtn_workexp.Visible = false;
        lbtn_Earnings.Visible = false;
        lbtn_Deduction.Visible = false;
        LinkButton1.Visible = false;
        LinkButton2.Visible = false;
        LinkButton3.Visible = false;
        LinkButton4.Visible = false;
        lbtn_skills.Visible = false;
        lbtn_date.Visible = false;
        //lbtn_training.Visible = false;
        //lblOverHeading.Visible = false;
        //lnk_Asset_edit.Visible = false;
        //tab_head.Visible = false;
        //Table_bus_heading.Visible = false;
        Table_bus.Visible = false;
    }

    public void grid_transport_load()
    {
        try
        {
            BusList = employee.fn_get_bus(employee);

            if (BusList.Count > 0)
            {
                Grid_bus.DataSource = BusList;
                Grid_bus.DataBind();
            }
            else
            {
                Grid_bus.DataSource = BusList;
                Grid_bus.DataBind();
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Cookies["Profile_Check"].Value = "1";
            Response.Cookies["preview_emp"].Value = "2";
            Response.Redirect("../Hrms_Employee/EditProfile.aspx");
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    protected void PastEmploymentDetails_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Cookies["Profile_Check"].Value = "1";
            Response.Cookies["preview_emp"].Value = "2";
            Response.Redirect("../Hrms_Employee/Edit_PastEmploymentDetails.aspx");
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }

    }

    protected void LbtnContactInformation_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Cookies["Profile_Check"].Value = "1";
            Response.Cookies["preview_emp"].Value = "2";
            Response.Redirect("../Hrms_Employee/EditContactInformation.aspx");
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    protected void LbtnBankDetails_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Cookies["Profile_Check"].Value = "1";
            Response.Cookies["preview_emp"].Value = "2";
            Response.Redirect("../Hrms_Employee/EditBankDetails.aspx");
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    protected void lnkbtnFamilyDetails_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Cookies["Profile_Check"].Value = "1";
            Response.Cookies["preview_emp"].Value = "2";
            Response.Redirect("../Hrms_Employee/EditFamilyDetails.aspx");
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    public void access()
    {
        //_connection = con.fn_Connection();
        //_connection.Open();
        //cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=3 and section_view='No'", _connection);
        //SqlDataReader rdrview = cmd.ExecuteReader();
        //if (rdrview.Read())
        //{
        //    Response.Write("<script language='javascript'>alert('Permission Restricted. Please Contact Administrator.');window.location='~/Company_Home.aspx';</script>");
        //}
        //rdrview.Close();
        //cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=3 and section_edit='No'", _connection);
        //SqlDataReader rdredit = cmd.ExecuteReader();
        //if (rdredit.Read())
        //{
        //    LinkButton1.Visible = false;
        //    LinkButton2.Visible = false;
        //    LinkButton3.Visible = false;
            
        //    lbtn_date.Visible = false;
        //    lbtn_Deduction.Visible = false;
        //    lbtn_Earnings.Visible = false;
        //    lbtn_skills.Visible = false;
        //    //lbtn_training.Visible = false;
        //    lbtn_WorkDetails.Visible = false;
        //    lbtn_workexp.Visible = false;
            
        //}
        //rdredit.Close();
        //cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=3 and  section_delete='No'", _connection);
        //SqlDataReader rdrdel = cmd.ExecuteReader();
        //if (rdrdel.Read())
        //{
            
        //}
        //rdrdel.Close();
        //_connection.Close();
    }  

    public void tab_hide()
    {     

    }

    public void user()
    {
   //Employee Profile

        EmpFirstList = employee.fn_get_Emp_first(employee);
        if (EmpFirstList.Count > 0)
        {
            tab_profile.Rows[0].Cells[1].InnerText = EmpFirstList[0].EmployeeCode;
            tab_profile.Rows[0].Cells[3].InnerText = EmpFirstList[0].FirstName;
            tab_profile.Rows[1].Cells[1].InnerText = EmpFirstList[0].LastName;
            tab_profile.Rows[1].Cells[3].InnerText = EmpFirstList[0].MiddleName;
            tab_profile.Rows[2].Cells[1].InnerText = EmpFirstList[0].d_birth.ToShortDateString();
            tab_profile.Rows[2].Cells[3].InnerText = EmpFirstList[0].Gender;
            //tab_profile.Rows[6].Cells[1].InnerText = EmpFirstList[0].ReaderId;
            //tab_profile.Rows[6].Cells[3].InnerText = EmpFirstList[0].OT_Eligible;
            //tab_profile.Rows[7].Cells[1].InnerText = EmpFirstList[0].PFno;
            //tab_profile.Rows[7].Cells[3].InnerText = EmpFirstList[0].OT_calc;
            //tab_profile.Rows[8].Cells[1].InnerText = EmpFirstList[0].ESIno;
        }
    }
    public void emp()
    {
     
   //Employee Profile

       //Bank Deatils

        int ccount,ccount1;
        myConnection.Open();
        cmd = new SqlCommand("select count (*) from paym_employee where pn_CompanyID = '" + employee.CompanyId + "' and pn_branchId = '" + employee.BranchId + "' and pn_EmployeeID = '" + employee.EmployeeId + "' and Bank_Name is not null ", myConnection);
        ccount = (int)cmd.ExecuteScalar();
        if (ccount > 0)
        {
            cmd = new SqlCommand("select * from paym_employee where pn_CompanyID = '" + employee.CompanyId + "' and pn_branchId = '" + employee.BranchId + "' and pn_EmployeeID = '" + employee.EmployeeId + "' ", myConnection);
            rea = cmd.ExecuteReader();
            if (rea.Read())
            {
                    tab_bank.Rows[1].Cells[1].InnerText = Convert.ToString(rea["Bank_code"]);
                    tab_bank.Rows[1].Cells[3].InnerText = Convert.ToString(rea["Bank_Name"]);
                    tab_bank.Rows[2].Cells[1].InnerText = Convert.ToString(rea["Branch_Name"]);
                    tab_bank.Rows[2].Cells[3].InnerText = Convert.ToString(rea["Account_Type"]);
                    tab_bank.Rows[3].Cells[1].InnerText = Convert.ToString(rea["MICR_Code"]);
                    tab_bank.Rows[3].Cells[3].InnerText = Convert.ToString(rea["IFSC_Code"]);
                    tab_bank.Rows[4].Cells[1].InnerText = Convert.ToString(rea["Address"]);
                    tab_bank.Rows[4].Cells[3].InnerText = Convert.ToString(rea["Other_Info"]);
            }
        }
        myConnection.Close();

        myConnection.Open();
        cmd1 = new SqlCommand("select * from paym_employee where pn_CompanyID = '" + employee.CompanyId + "' and pn_branchId = '" + employee.BranchId + "' and pn_EmployeeID = '" + employee.EmployeeId + "' ", myConnection);
        SqlDataReader rea1 = cmd1.ExecuteReader();
        if (rea1.Read())
        {
            tab_profile.Rows[8].Cells[5].InnerText = Convert.ToString(rea1["Reporting_person"]);
            tab_profile.Rows[9].Cells[5].InnerText = Convert.ToString(rea1["basic_salary"]) + ".00";
            tab_profile.Rows[9].Cells[2].InnerText = Convert.ToString(rea1["Pan_no"]);
            string st = "", tds = "";
            st = Convert.ToString(rea1["Salary_Type"]);
            tds = Convert.ToString(rea1["TDS_Applicable"]);
            if (st != "")
            {
                tab_profile.Rows[10].Cells[2].InnerText = tds;
            }
            if (tds != "")
            {
                tab_profile.Rows[10].Cells[5].InnerText = st;
            }
        }
        myConnection.Close();

        EmpFirstList = employee.fn_get_Emp_first(employee);

        if (EmpFirstList.Count > 0)
        {
            if (EmpFirstList[0].EmployeeCode != "")
            {
                tab_profile.Rows[0].Cells[5].InnerText = EmpFirstList[0].EmployeeCode;
            }
            if (EmpFirstList[0].FullName != "")
            {
                tab_profile.Rows[1].Cells[2].InnerText = EmpFirstList[0].FullName;
            }
            if (EmpFirstList[0].FirstName != "")
            {
                tab_profile.Rows[1].Cells[5].InnerText = EmpFirstList[0].FirstName;
            }
            if (EmpFirstList[0].MiddleName != "")
            {
                tab_profile.Rows[2].Cells[2].InnerText = EmpFirstList[0].MiddleName;
            }
            if (EmpFirstList[0].LastName != "")
            {
                tab_profile.Rows[2].Cells[5].InnerText = EmpFirstList[0].LastName;
            }
            if (EmpFirstList[0].d_birth.ToShortDateString() != "01/01/1900")
            {
                tab_profile.Rows[3].Cells[2].InnerText = EmpFirstList[0].d_birth.ToString("dd/MM/yyyy");
            }
            if (EmpFirstList[0].Gender == "Male")
            {
                tab_profile.Rows[3].Cells[5].InnerText = "Male";
            }
            else
            {
                tab_profile.Rows[3].Cells[5].InnerText = "Female";
            }
            if(Convert.ToString(EmpFirstList[0].ReaderId) != "")
            {
                tab_profile.Rows[6].Cells[2].InnerText = Convert.ToString(EmpFirstList[0].ReaderId);
            }
            if (EmpFirstList[0].OT_Eligible == '0')
            {
                tab_profile.Rows[6].Cells[5].InnerText = "N";
            }
            else 
            {
                tab_profile.Rows[6].Cells[5].InnerText = "Y";
            }
            if (EmpFirstList[0].PFno != "")
            {
                tab_profile.Rows[7].Cells[2].InnerText = EmpFirstList[0].PFno;
            }
            if (Convert.ToString(EmpFirstList[0].OT_calc) != "")
            {
                tab_profile.Rows[7].Cells[5].InnerText = Convert.ToString(EmpFirstList[0].OT_calc);
            }
            if (EmpFirstList[0].ESIno != "")
            {
                tab_profile.Rows[8].Cells[2].InnerText = EmpFirstList[0].ESIno;
            }


        }

        EmpGeneralList = employee.fn_get_Emp_general(employee);

        if (EmpGeneralList.Count > 0)
        {
            switch (EmpGeneralList[0].Salutation)
            {
                case '1': tab_profile.Rows[0].Cells[2].InnerText = "Mr.";
                    break;

                case '2': tab_profile.Rows[0].Cells[2].InnerText = "Ms.";
                    break;

                case '3': tab_profile.Rows[0].Cells[2].InnerText = "Mrs.";
                    break;

                case '4': tab_profile.Rows[0].Cells[2].InnerText = "Dr.";
                    break;

                default:tab_profile.Rows[0].Cells[2].InnerText = "";
                    break;
            }

            if (EmpGeneralList[0].BloodGroup != "None")
            {
                tab_profile.Rows[4].Cells[2].InnerText = EmpGeneralList[0].BloodGroup;
            }
            
            if (EmpGeneralList[0].MaritalStatus == '1')
            {
                tab_profile.Rows[4].Cells[5].InnerText = "Single";
            }
            else
            {
                tab_profile.Rows[4].Cells[5].InnerText = "Married";
            }
           
            if (EmpGeneralList[0].Nationality != "")
            {
                tab_profile.Rows[5].Cells[2].InnerText = EmpGeneralList[0].Nationality;
            }
            if (EmpGeneralList[0].Religion != "")
            {
                tab_profile.Rows[5].Cells[5].InnerText = EmpGeneralList[0].Religion;
            }          

            if (EmpGeneralList[0].HouseNo != "")
            {
                tab_address.Rows[1].Cells[2].InnerText = EmpGeneralList[0].HouseNo;
            }
           
            if (EmpGeneralList[0].AddressLine1 != "")
            {
                tab_address.Rows[1].Cells[5].InnerText = EmpGeneralList[0].AddressLine1;
            }
            if (EmpGeneralList[0].AddressLine2 != "")
            {
                tab_address.Rows[2].Cells[2].InnerText = EmpGeneralList[0].AddressLine2;
            }
            if (EmpGeneralList[0].State != "")
            {
                tab_address.Rows[2].Cells[5].InnerText = EmpGeneralList[0].State;
            }
            if (EmpGeneralList[0].City != "")
            {
                tab_address.Rows[3].Cells[2].InnerText = EmpGeneralList[0].City;
            }
            if (EmpGeneralList[0].StreetName != "")
            {
                tab_address.Rows[3].Cells[5].InnerText = EmpGeneralList[0].StreetName;
            }
            if (EmpGeneralList[0].p_HouseNo != "")
            {
                tab_address.Rows[6].Cells[2].InnerText = EmpGeneralList[0].p_HouseNo;
            }            
            if (EmpGeneralList[0].P_AddressLine1 != "")
            {
                tab_address.Rows[6].Cells[5].InnerText = EmpGeneralList[0].P_AddressLine1;
            }
            if (EmpGeneralList[0].P_AddressLine2 != "")
            {
                tab_address.Rows[7].Cells[2].InnerText = EmpGeneralList[0].P_AddressLine2;
            }
            if (EmpGeneralList[0].P_State != "")
            {
                tab_address.Rows[7].Cells[5].InnerText = EmpGeneralList[0].P_State;
            }
            if (EmpGeneralList[0].P_City != "")
            {
                tab_address.Rows[8].Cells[2].InnerText = EmpGeneralList[0].P_City;
            }
           
            if (EmpGeneralList[0].p_StreetName != "")
            {
                tab_address.Rows[8].Cells[5].InnerText = EmpGeneralList[0].p_StreetName;
            }

            if (EmpGeneralList[0].ph_Office!= "")
            {
                tab_address.Rows[10].Cells[2].InnerText = EmpGeneralList[0].ph_Office;
            }
            if (EmpGeneralList[0].ph_Residence != "")
            {
                tab_address.Rows[10].Cells[5].InnerText = EmpGeneralList[0].ph_Residence;
            }
            if (EmpGeneralList[0].CellNo != "")
            {
                tab_address.Rows[11].Cells[2].InnerText = EmpGeneralList[0].CellNo;
            }
            if (EmpGeneralList[0].Fax!= "")
            {
                tab_address.Rows[11].Cells[5].InnerText = EmpGeneralList[0].Fax;
            }

            if (EmpGeneralList[0].EmailId != "")
            {
                tab_address.Rows[13].Cells[2].InnerText = EmpGeneralList[0].EmailId;
            }
            if (EmpGeneralList[0].A_EmailId != "")
            {
                tab_address.Rows[13].Cells[5].InnerText = EmpGeneralList[0].A_EmailId;
            }

            if (EmpGeneralList[0].emgname != "")
            {
                tab_address.Rows[15].Cells[2].InnerText = EmpGeneralList[0].emgname;
            }
            if (EmpGeneralList[0].emgno != "")
            {
                tab_address.Rows[15].Cells[5].InnerText = EmpGeneralList[0].emgno;
            }
            if (EmpGeneralList[0].FatherName != "")
            {
                tab_family.Rows[1].Cells[1].InnerText = EmpGeneralList[0].FatherName;
            }
            if (EmpGeneralList[0].MotherName != "")
            {
                tab_family.Rows[1].Cells[3].InnerText = EmpGeneralList[0].MotherName;
            }
            if (EmpGeneralList[0].SpouseName != "")
            {
                tab_family.Rows[2].Cells[1].InnerText = EmpGeneralList[0].SpouseName;
            }
            if (Convert.ToString(EmpGeneralList[0].Children)!= "")
            {
                tab_family.Rows[2].Cells[3].InnerText = Convert.ToString(EmpGeneralList[0].Children);
            }

            myConnection.Open();
            cmd1 = new SqlCommand("select * from paym_employee_general where pn_CompanyID = '" + employee.CompanyId + "' and pn_branchId = '" + employee.BranchId + "' and pn_EmployeeID = '" + employee.EmployeeId + "' ", myConnection);
            SqlDataReader read1 = cmd1.ExecuteReader();
            if (read1.Read())
            {
                tab_ref.Rows[0].Cells[1].InnerText = Convert.ToString(read1["salary"]);
                tab_ref.Rows[0].Cells[3].InnerText = Convert.ToString(read1["position"]);
                tab_ref.Rows[1].Cells[1].InnerText = Convert.ToString(read1["training_attended"]);
                tab_ref.Rows[1].Cells[3].InnerText = Convert.ToString(read1["training_duration"]);
            }
            myConnection.Close();
            
            if (EmpGeneralList[0].Ref1_Name != "")
            {
                tab_ref.Rows[3].Cells[1].InnerText = EmpGeneralList[0].Ref1_Name;
            }

            if (EmpGeneralList[0].Ref1_Relation != "")
            {
                tab_ref.Rows[3].Cells[3].InnerText = EmpGeneralList[0].Ref1_Relation;
            }
            if (EmpGeneralList[0].Ref1_Phno != "")
            {
                tab_ref.Rows[4].Cells[1].InnerText = EmpGeneralList[0].Ref1_Phno;
            }           
            if (EmpGeneralList[0].Ref2_Email != "")
            {
                tab_ref.Rows[4].Cells[3].InnerText = EmpGeneralList[0].Ref2_Email;
            }
            if (EmpGeneralList[0].Ref1_Name != "")
            {
                tab_ref.Rows[6].Cells[1].InnerText = EmpGeneralList[0].Ref1_Name;
            }

            if (EmpGeneralList[0].Ref2_Relation != "")
            {
                tab_ref.Rows[6].Cells[3].InnerText = EmpGeneralList[0].Ref2_Relation;
            }
            if (EmpGeneralList[0].Ref2_Phno != "")
            {
                tab_ref.Rows[7].Cells[1].InnerText = EmpGeneralList[0].Ref2_Phno;
            }
            if (EmpGeneralList[0].Ref2_Email != "")
            {
                tab_ref.Rows[7].Cells[3].InnerText = EmpGeneralList[0].Ref2_Email;
            }

        }

        //Employee WorkDetails


        EmpProfileList = employee.fn_get_Emp_Profile1(employee);
        //Label1.Text = EmpProfileList[1].ToString();
        if (EmpProfileList.Count > 0)
        {
            if (EmpProfileList[0].DivisionId != 1)
            {
                tab_workdet.Rows[0].Cells[1].InnerText = employee.fn_GetDivisionName(EmpProfileList[0].DivisionId);
                // Label1.Text = employee.fn_GetDivisionName(EmpProfileList[0].DivisionId).ToString();
            }
            else
            {
                //Label1.Text = "error";
            }


            if (EmpProfileList[0].CategoryId != 1)
            {
                tab_workdet.Rows[0].Cells[3].InnerText = employee.fn_GetCategoryName(EmpProfileList[0].CategoryId);
            }

            if (EmpProfileList[0].DepartmentId != 1)
            {
                tab_workdet.Rows[1].Cells[1].InnerText = employee.fn_GetDepartmentName(EmpProfileList[0].DepartmentId);
            }

            if (EmpProfileList[0].LevelId != 1)
            {
                tab_workdet.Rows[1].Cells[3].InnerText = employee.fn_GetLevelName(EmpProfileList[0].LevelId);
            }


            if (EmpProfileList[0].DesignationId != 1)
            {
                tab_workdet.Rows[2].Cells[1].InnerText = employee.fn_GetDesignationName(EmpProfileList[0].DesignationId);
            }

            if (EmpProfileList[0].GradeId != 1)
            {
                tab_workdet.Rows[2].Cells[3].InnerText = employee.fn_GetGradeName(EmpProfileList[0].GradeId);
            }

            if (EmpProfileList[0].ShiftId != 1)
            {

                ShiftList = employee.fn_GetShiftName(EmpProfileList[0].ShiftId);
                if (ShiftList.Count > 0)
                {
                    tab_workdet.Rows[3].Cells[1].InnerText = ShiftList[0].ShiftName;

                }
            }


            if (EmpProfileList[0].JobStatusId != 1)
            {
                tab_workdet.Rows[3].Cells[3].InnerText = employee.fn_GetJobStatusName(EmpProfileList[0].JobStatusId);
            }

            if (EmpProfileList[0].ProjectsiteId != 1)
            {

                tab_workdet.Rows[4].Cells[1].InnerText = employee.fn_ProjectsiteName(EmpProfileList[0].ProjectsiteId);

            }
            //tab_workdet.Rows[4].Cells[3].InnerText = employee.BranchId.ToString();


        }
        
  
        //Employee Date Details


        EmpWorkList = employee.fn_get_Emp_WorkDetails(employee);

        if (EmpWorkList.Count > 0)
        {
            if (EmpWorkList[0].d_join.ToString("dd/MM/yyyy") != "01/01/2000")
            {
                tab_date.Rows[0].Cells[1].InnerText = EmpWorkList[0].d_join.ToString("dd/MM/yyyy");
            }
            if (EmpWorkList[0].d_extended.ToString("dd/MM/yyyy") != "01/01/2000")
            {
                tab_date.Rows[0].Cells[3].InnerText = EmpWorkList[0].d_extended.ToString("dd/MM/yyyy");
            }
            if (EmpWorkList[0].d_confirmation.ToString("dd/MM/yyyy") != "01/01/2000")
            {
                tab_date.Rows[1].Cells[1].InnerText = EmpWorkList[0].d_confirmation.ToString("dd/MM/yyyy");
            }
            if (EmpWorkList[0].d_retirement.ToString("dd/MM/yyyy") != "01/01/2000")
            {
                tab_date.Rows[1].Cells[3].InnerText = EmpWorkList[0].d_retirement.ToString("dd/MM/yyyy");
            }
            if (EmpWorkList[0].d_renue.ToString("dd/MM/yyyy") != "01/01/2000")
            {
                tab_date.Rows[2].Cells[1].InnerText = EmpWorkList[0].d_renue.ToString("dd/MM/yyyy");
            }
            if (EmpWorkList[0].d_probotion.ToString("dd/MM/yyyy") != "01/01/2000")
            {
                tab_date.Rows[2].Cells[3].InnerText = EmpWorkList[0].d_probotion.ToString("dd/MM/yyyy");
            }
            if (EmpWorkList[0].d_Offer.ToString("dd/MM/yyyy") != "01/01/2000")
            {
                tab_date.Rows[3].Cells[1].InnerText = EmpWorkList[0].d_Offer.ToString("dd/MM/yyyy");
            }
            if (EmpWorkList[0].Reason!= "")
            {
                tab_date.Rows[3].Cells[3].InnerText = EmpWorkList[0].Reason;
            }

        }

        //Employee Photo

        EmpPhotoList = employee.fn_get_Emp_photo(employee);


        if (EmpPhotoList.Count > 0)
        {
            img_emp_photo.ImageUrl = "~/Photo/" + EmpPhotoList[0].img_path;
            //img_emp_photo.ImageUrl = Server.MapPath("~/Photo/") + EmpPhotoList[0].img_path;
            //img_emp.Src = "~/Photo/" + EmpPhotoList[0].img_path;
        }
        else
        {
            img_emp_photo.ImageUrl = "~/Photo/" + "Default.JPG";
            //img_emp_photo.ImageUrl = Server.MapPath("~/Photo/") + "Default.JPG";
        }

       // nil();

    }

    public void EmployeeEntry()
    {
        try
        {
            //tab_emp.Visible = true;
            emp();
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }
    
    protected void btn_Back_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Redirect("../Hrms_Company/Employee.aspx");
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    public void get_employee_details()
    {
        try
        {
            //if (Convert.ToInt32(Request.Cookies["Select_Employee"].Value) == 1)
            //{
            employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
            pay.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
            c.EmployeeID = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);

            if (s_login_role == "a")
            {
               // employee.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
                //pay.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
                emp();
            }

            else if (s_login_role == "h")
            {
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                 pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                emp();
            }

            else if (s_login_role == "u")
            {
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                 pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                user();
            }
            else
            {
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                 pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                emp();
            }
            //}

            //else
            //{
            //    Session["ErrorMsg"] = "Employee should be selected";
            //    Response.Redirect("../Hrms_Company/Employee.aspx");
            //}

        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }

    }


    protected void btn_add_employee_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Cookies["Profile_Check"].Value = "0";
            Response.Cookies["preview_emp"].Value = "1";
            //Session["first_profile"] = 0;
            Response.Redirect("../Hrms_Employee/Employee_Profile.aspx");
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }

    }
    

    protected void lbtn_WorkDetails_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Cookies["Profile_Check"].Value = "1";
            Response.Cookies["preview_emp"].Value = "2";
            Response.Redirect("../Hrms_Employee/Employee_WorkDetails.aspx");
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Cookies["Profile_Check"].Value = "1";
            Response.Cookies["preview_emp"].Value = "2";
            Response.Redirect("../Hrms_Employee/Employee_Education.aspx");

        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }

    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Cookies["Profile_Check"].Value = "1";
            Response.Cookies["preview_emp"].Value = "2";
            Response.Redirect("../Hrms_Employee/Employee_Photo.aspx");

        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    protected void lbtn_skills_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Cookies["Profile_Check"].Value = "1";
            Response.Cookies["preview_emp"].Value = "2";
            Response.Redirect("../Hrms_Employee/Employee_Skills.aspx");
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    public void earn_grid()
    {
        for (grk = 0; grk < grid_Earnings.Rows.Count; grk++)
        {
            //grid Check Box

            if (emp_Earnings_List[grk].regular == 'Y')
            {
                ((CheckBox)grid_Earnings.Rows[grk].FindControl("grd_chk_earn")).Checked = true;
            }
            else
            {
                ((CheckBox)grid_Earnings.Rows[grk].FindControl("grd_chk_earn")).Checked = false;
            }
        }
    }

    public void ded_grid()
    {
        for (grk = 0; grk < grd_Deducation.Rows.Count; grk++)
        {
            if (emp_Deduction_List[grk].regular == 'Y')
            {
                ((CheckBox)grd_Deducation.Rows[grk].FindControl("grd_chk_ded")).Checked = true;
            }
            else
            {
                ((CheckBox)grd_Deducation.Rows[grk].FindControl("grd_chk_ded")).Checked = false;
            }
        }
    }

    protected void lbtn_Earnings_Click(object sender, EventArgs e)
    {

        try
        {
            Response.Cookies["Profile_Check"].Value = "1";
            Response.Cookies["preview_emp"].Value = "2";
            Response.Redirect("../Hrms_Employee/Employee_Earnings.aspx");
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }

    }

    protected void lbtn_Deduction_Click(object sender, EventArgs e)
    {

        try
        {
            Response.Cookies["Profile_Check"].Value = "1";
            Response.Cookies["preview_emp"].Value = "2";
            Response.Redirect("../Hrms_Employee/Employee_Deducation.aspx");

        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }

    }

    protected void lbtn_workexp_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Cookies["Profile_Check"].Value = "1";
            Response.Cookies["preview_emp"].Value = "2";
            Response.Redirect("../Hrms_Employee/WorkExperience.aspx");
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    //protected void lbtn_training_Click(object sender, EventArgs e)
    //{

    //    try
    //    {
    //        Response.Cookies["Profile_Check"].Value = "1";
    //        Response.Cookies["preview_emp"].Value = "2";
    //        Response.Redirect("../Hrms_Employee/Employee_Training.aspx");
    //    }
    //    catch (Exception ex)
    //    {
    //        lbl_Error.Text = "Error";
    //    }
    //}

    public void nil()
    {
        int i;

        //Employee Profile

        for (i = 0; i <= 5; i++)
        {
            if (tab_profile.Rows[i].Cells[1].InnerText == "")
            {
                tab_profile.Rows[i].Cells[1].InnerText = "";
            }

            if (tab_profile.Rows[i].Cells[3].InnerText == "")
            {
                tab_profile.Rows[i].Cells[3].InnerText = "";
            }
        }

        for (i = 1; i <= 8; i++)
        {
            if (i != 4 && i != 5)
            {

                if (tab_address.Rows[i].Cells[1].InnerText == "")
                {
                    tab_address.Rows[i].Cells[1].InnerText = "";
                }

                if (tab_address.Rows[i].Cells[3].InnerText == "")
                {
                    tab_address.Rows[i].Cells[3].InnerText = "";
                }
            }

        }

        //Employee WorkDetails


        //for (i = 0; i <= 5; i++)
        //{

        //    if (tab_emp_profile.Rows[i].Cells[1].InnerText == "")
        //        {
        //            tab_emp_profile.Rows[i].Cells[1].InnerText = "NIL";

        //        }


        //        if (tab_emp_profile.Rows[i].Cells[3].InnerText == "")
        //        {
        //            tab_emp_profile.Rows[i].Cells[3].InnerText = "NIL";

        //        }   


        //}

    }

    public void label_clear()
    {

    }


    protected void lbtn_date_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Cookies["Profile_Check"].Value = "1";
            Response.Cookies["preview_emp"].Value = "2";
            Response.Redirect("../Hrms_Employee/Employee_Date.aspx");
        }

        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    //public void grid_training_load()
    //{
    //    emp_Edit = employee.fn_Training_grid(employee);

    //    if (emp_Edit.Count > 0)
    //    {
    //        grid_Training.DataSource = emp_Edit;
    //        grid_Training.DataBind();
    //    }
    //    else
    //    {
    //        //lbl_training.Text = "(NIL)";
    //    }
    //}

    public void grid_exp_load()
    {

        //Employee Work Experience


        WorkHistoryList = c.fn_get_Employee_WorkHistory(c.EmployeeID);

        if (WorkHistoryList.Count > 0)
        {
            grid_WorkHistory.DataSource = WorkHistoryList;
            grid_WorkHistory.DataBind();
        }
        else
        {
            //lbl_workexp.Text = "(NIL)";
        }
    }

    public void grid_qualfication_load()
    {
        //Employee Education

        //emp_edu_List = employee.fn_getEmployee_EducationList(employee);

        emp_edu_List = employee.Priview_Employee_EducationList(employee);

        if (emp_edu_List.Count > 0)
        {
            grid_emp_education.DataSource = emp_edu_List;
            grid_emp_education.DataBind();
        }
        else
        {
            grid_emp_education.Visible = false;
        }
    }
    public void grid_Asset_load()
    {
        emp_Edit = employee.fn_getEmployee_Asset(employee);

        if (emp_Edit.Count > 0)
        {
            grid_aseet.DataSource = emp_Edit;
            grid_aseet.DataBind();
        }
        else
        {
            //lbl_asset.Text = "(NIL)"; 
        }

    }
    public void grid_skill_load()
    {        //Employee Skills

        emp_skills_List = employee.fn_getEmployee_skills(employee);

        if (emp_skills_List.Count > 0)
        {
            grid_emp_Skills.DataSource = emp_skills_List;
            grid_emp_Skills.DataBind();
        }
        else
        {
            //lbl_skills.Text = "(NIL)";
        }
    }

    public void grid_earnings_load()
    {
        //Employee Earnings
        emp_Earnings_List = pay.fn_Emp_Earnings(pay);

        if (emp_Earnings_List.Count > 0)
        {
            grid_Earnings.DataSource = emp_Earnings_List;
            grid_Earnings.DataBind();
            earn_grid();
        }
        else
        {
            emp_Earnings_List = pay.fn_Earnings(pay);
             
            if (emp_Earnings_List.Count > 0)
            {
                grid_Earnings.DataSource = emp_Earnings_List;
                grid_Earnings.DataBind();
            }
            else
            {
                //lbl_earnings.Text = "(NIL)";
            }
        }
    }

    public void grid_deductions_load()
    {
        //Employee Deduction
        emp_Deduction_List = pay.fn_Emp_Deduction(pay);

        if (emp_Deduction_List.Count > 0)
        {

            grd_Deducation.DataSource = emp_Deduction_List;
            grd_Deducation.DataBind();

            ded_grid();
        }
        else
        {
            emp_Deduction_List = pay.fn_Deduction(pay);

            if (emp_Deduction_List.Count > 0)
            {
                grd_Deducation.DataSource = emp_Deduction_List;
                grd_Deducation.DataBind();
            }
            else
            {
                //lbl_deduction.Text = "(NIL)";
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        
    }
    public void grid_OverHeading_load()
    {

        //Employee Deduction
        emp_Deduction_List = pay.fn_Emp_OverHeading(pay);

        if (emp_Deduction_List.Count > 0)
        {

            gdv_overhead.DataSource = emp_Deduction_List;
            gdv_overhead.DataBind();

            // ded_grid();
        }
        else
        {

            emp_Deduction_List = pay.fn_Overheading1(pay.BranchId);
            if (emp_Deduction_List.Count > 0)
            {
                gdv_overhead.DataSource = emp_Deduction_List;
                gdv_overhead.DataBind();
            }
            else
            {
                //lbl_deduction.Text = "(NIL)";
            }
        }
    }
    protected void grd_Deducation_SelectedIndexChanged(object sender, EventArgs e)
    {
    }   
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Cookies["Profile_Check"].Value = "1";
            Response.Cookies["preview_emp"].Value = "2";
            Response.Redirect("../Hrms_Employee/Employee_OverHeadingCost.aspx");

        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }
    protected void gdv_overhead_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grid_Earnings_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    public void emp_id_load()
    {
        myConnection.Open();
        empcode_name = Request.Cookies["Employee_Code_FirstLastName"].Value;
        string[] s1 = empcode_name.Split('-');
        emp_code = s1[0];
        Session["Session_emp_code"] = emp_code;
        SqlCommand cmd = new SqlCommand("select pn_employeeID from paym_employee where employeecode='" + emp_code + "'", myConnection);
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            employee.Temp_emp_id = Convert.ToInt32(dr["pn_employeeID"]);
        }
        dr.Close();
        myConnection.Close();
    }


    protected void Lnk_edit_bus_Click(object sender, EventArgs e)
    {

        transport_edit = "Edit Clicked";
        Session["transport"] = transport_edit;
        Response.Redirect("../Hrms_Employee/EditTransportDetails.aspx");
    }
    protected void gdv_overhead_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerial = (Label)e.Row.FindControl("lblSerial_OH");
            lblSerial.Text = i.ToString();
            i++;
        }
    }

    protected void grid_WorkHistory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerial = (Label)e.Row.FindControl("lblSerial_WH");
            lblSerial.Text = k.ToString();
            k++;
        }
    }

    protected void grid_emp_Skills_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerial = (Label)e.Row.FindControl("lblSerial_SD");
            lblSerial.Text = l.ToString();
            l++;
        }
    }
    protected void grid_Earnings_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerial = (Label)e.Row.FindControl("lblSerial_ED");
            lblSerial.Text = m.ToString();
            m++;
        }
    }
    protected void grd_Deducation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerial = (Label)e.Row.FindControl("lblSerial_DD");
            lblSerial.Text = n.ToString();
            n++;
        }
    }
    protected void grid_Training_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerial = (Label)e.Row.FindControl("lblSerial_TD");
            lblSerial.Text = o.ToString();
            o++;
        }
    }
    protected void grid_aseet_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerial = (Label)e.Row.FindControl("lblSerial_AD");
            lblSerial.Text = p.ToString();
            p++;
        }
    }

    protected void grid_emp_education_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerial = (Label)e.Row.FindControl("lblSerial_Q");
            lblSerial.Text = j.ToString();
            j++;
        }
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Cookies["Profile_Check"].Value = "1";
            Response.Cookies["preview_emp"].Value = "2";
            Response.Redirect("../Hrms_Employee/Employee_Assets.aspx");

        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }
    protected void grid_emp_education_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string Employeeid = ((Label)grid_emp_education.Rows[e.RowIndex].Cells[1].FindControl("lblEmployeeID")).Text;
        employee.EmployeeId = Convert.ToInt32(Employeeid);
        string CourseID = ((Label)grid_emp_education.Rows[e.RowIndex].Cells[2].FindControl("lblPGCourseId")).Text;
        myConnection.Open();
        SqlCommand cmd = new SqlCommand("delete from paym_Employee_Education where pn_EmployeeID='" + Employeeid + "' and pn_CourseID='" + CourseID + "'", myConnection);
        cmd.ExecuteNonQuery();
        grid_qualfication_load();
        //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Record has Deleted');", true);
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Record has Deleted');};", true);
    }

    protected void btn_Remove_Click(object sender, EventArgs e)
    {
        employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
        pay.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
        c.EmployeeID = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);

        myConnection.Open();
        SqlCommand cmd = new SqlCommand("update paym_employee set status='N' where pn_CompanyID = '" + employee.CompanyId + "' and pn_branchId = '" + employee.BranchId + "' and pn_EmployeeID = '" + employee.EmployeeId + "'", myConnection);
        cmd.ExecuteNonQuery();
        myConnection.Close();
    }
}















