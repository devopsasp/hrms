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
using System.Data.SqlClient;
using System.Web.Services;

public partial class Hrms_Employee_Default2 : System.Web.UI.Page
{
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    //private SqlConnection _con;
    //ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlCommand cmd = new SqlCommand();

    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    Candidate c = new Candidate();
    Company company = new Company();

    Collection<Employee> EmployeesList;
    Collection<Employee> emp_ID_List;
    Collection<Employee> emp_available;
    Collection<Employee> EmpFirstList;
    Collection<Employee> EmpGeneralList;
    Collection<PayRoll> Edlilist;
    Collection<PayRoll> emp_edu_List;
    Collection<PayRoll> Empty_gridList;

    int i, yr_it, cur_yr, mon, dat, year, pr_emp;
    string _Value, _data, dt, mn, yr, dob_edit, default_sqldate = "01/01/1900", _Value1;
    string s_login_role;

    string s_form = "", session_emp_code;
    DataSet ds_userrights;

    protected void Page_Load(object sender, EventArgs e)
    {

        
        //rolelode();
       
        try
        {
            s_login_role = Request.Cookies["Login_temp_Role"].Value;

            if (s_login_role != "e")

            {
                
                if (Convert.ToInt32(Request.Cookies["Select_Employee"].Value) == 1)
                {
                    employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                    employee.EmployeeId = Convert.ToInt32(Request.Cookies["userid"].Value);
                    employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
                   

                    pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
                     pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

                    c.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
                    c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

                    pr_emp = Convert.ToInt32(Request.Cookies["preview_emp"].Value);

                    //date_yr();

                    EmployeeLoad();
                   
                    if (!IsPostBack)
                    {
                        if (Request.Cookies["Employee_Code_FirstLastName"].Value != "")
                        {

                            //lbl_empcodename.Text = Request.Cookies["Employee_Code_FirstLastName"].Value;
                        }
                        else
                        {
                            //lbl_empcodename.Text = "New Employee";
                        }

                        lbl_Error.Text = Request.Cookies["Profile_Error"].Value;
                       
                        switch (s_login_role)
                        {
                            
                            case "a":
                                if (pr_emp == 1)
                                {
                                    btn_update.Visible = false;
                                }
                                else
                                {
                                    btn_save.Visible = false;
                                    row_pwd.Visible = false;
                                    row_cpwd.Visible = false;
                                    btn_avail.Visible = false;
                                    //txtEmployeeCode.Disabled = true;//For Aban purpose only

                                    employee.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
                                    employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);

                                    pay.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
                                    pay.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);

                                    c.BranchID = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
                                    c.EmployeeID = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);

                                    admin();
                                }
                                break;

                            case "h":
                                if (pr_emp == 1)
                                {
                                    //personal_tbl.Visible = true;
                                    //perstbl.Visible = true;
                                    //contact_tbl.Visible = false;
                                    //contbl.Visible = false;
                                    //reftbl.Visible = false;
                                    //reference_tbl.Visible = false;
                                    //tab_empcodename.Visible = false;
                                    //reg_tbl.Visible = false;
                                    //tbl_bank.Visible = false;
                                    //tbl_bankhead.Visible = false;

                                    Edlilist = pay.fn_pay_EDLI(pay);
                                    if (Edlilist[0].Count == 1)
                                    {
                                        if (Edlilist[0].eligible == '0')
                                        {
                                            rdo_tds.Enabled = true;
                                            rdo_tds.SelectedIndex = 1;
                                        }
                                        else
                                        {
                                            rdo_tds.Enabled = false;
                                            rdo_tds.SelectedIndex = 0;
                                        }
                                    }
                                    btn_update.Visible = false;
                                }
                                else
                                {
                                    row_pwd.Visible = false;
                                    row_cpwd.Visible = false;

                                    btn_save.Visible = false;
                                    btn_avail.Visible = false;
                                    //txtEmployeeCode.Disabled = true;

                                    employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                    employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);

                                     pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                    pay.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);

                                    c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                    c.EmployeeID = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);

                                    admin();

                                    // //personal_tbl.Visible = true;
                                    //perstbl.Visible = true;
                                    //contact_tbl.Visible = true;
                                    //contbl.Visible = true;
                                    //reftbl.Visible = true;
                                    //reference_tbl.Visible = true;
                                    //tab_empcodename.Visible = true;
                                    //reg_tbl.Visible = true;
                                }
                                break;


                            case "u":
                                s_form = "32";

                                ds_userrights = company.check_Userrights(Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value), s_form);

                                if (ds_userrights.Tables[0].Rows.Count > 0)
                                {
                                    if (pr_emp == 1)
                                    {
                                        btn_update.Visible = false;
                                    }
                                    else
                                    {
                                        row_pwd.Visible = false;
                                        row_cpwd.Visible = false;

                                        btn_save.Visible = false;
                                        btn_avail.Visible = false;
                                        //txtEmployeeCode.Disabled = true;

                                        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                        employee.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);

                                         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                        pay.EmployeeId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);

                                        c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                        c.EmployeeID = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);

                                        admin();
                                    }
                                }
                                else
                                {
                                    Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                                    Response.Redirect("Employee_Preview.aspx");
                                }
                                break;
                            default:
                                Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                                Response.Redirect("~/Company_Home.aspx");
                                break;
                        }
                    }
                }
                else
                {
                    Session["ErrorMsg"] = "Employee should be selected";
                    Response.Redirect("../Hrms_Employee/Employee_Preview.aspx");
                }
            }
            else
            {
                Session["emp_menu"] = 0;
                Response.Redirect("Employee_Preview.aspx");
            }
        }
        catch (Exception ex)
        {
          
        }
    }

    //public class Role
    //{
    //    public int RowNo { get; set; }
    //    public int RoleId { get; set; }
    //    public string RoleName { get; set; }
    //    public int UserId { get; set; }

    //    // Constructor
    //    public Role()
    //    {
    //        this.RowNo = 0;
    //        this.RoleId = -1;
    //        this.RoleName = string.Empty;

    //        this.UserId = -1;
    //    }
    //}

    public void transport_save()
    {
        myConnection.Open();
        SqlCommand cmd = new SqlCommand("select pn_employeeID from paym_Employee where employeecode='" + employee.EmployeeCode + "'", myConnection);
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            employee.EmployeeId = Convert.ToInt32(dr["pn_employeeID"]);
        }
        employee.Area_name = Txt_area.Value;
        employee.Veh_id = txt_vehicle.Text;
        employee.Veh_number = Txt_veh_number.Value;       
        employee.Boarding_point = Txt_point.Value;
        employee.Driver_id = Txt_driver.Value;
        _Value1 = employee.Transport_details(employee);
        myConnection.Close();
    }
    public void admin()
    {
        try
        {
            //Session.LCID = 1053;

            EmpFirstList = employee.fn_get_Emp_first(employee);

            if (EmpFirstList.Count > 0)
            {
                txtEmployeeCode.Value = EmpFirstList[0].EmployeeCode;

                ViewState["vs_empcode"] = EmpFirstList[0].EmployeeCode;
                ViewState["password"] = EmpFirstList[0].password;

                //txtepwd.Type = "text";
                txtfullname.Value = EmpFirstList[0].FullName;
                txtFirstName.Value = EmpFirstList[0].FirstName;
                txtMiddleName.Value = EmpFirstList[0].MiddleName;
                txtLastName.Value = EmpFirstList[0].LastName;
                txtepwd.Value = EmpFirstList[0].password;
                txtecpwd.Value = EmpFirstList[0].c_password;

                txt_dob.Text = employee.Convert_ToIISDate(EmpFirstList[0].d_birth.ToShortDateString());

                txt_Readerid.Value = Convert.ToString(EmpFirstList[0].ReaderId);
                //rdo_btn.SelectedItem.Value = EmpFirstList[0].OT_Eligible;
                txt_pfno.Value = EmpFirstList[0].PFno;
                txt_esino.Value = EmpFirstList[0].ESIno;
                //ddl_ot.SelectedItem.Value = EmpFirstList[0].OT_calc;
                //txt_dob.Value = EmpFirstList[0].d_birth.ToString();

                ddl_retrive();
            }

            EmpGeneralList = employee.fn_get_Emp_general(employee);

            if (EmpGeneralList.Count > 0)
            {

                txtEmailId.Value = EmpGeneralList[0].EmailId;
                txtAEmailId.Value = EmpGeneralList[0].A_EmailId;
                //txtBloodGroup.Value = EmpGeneralList[0].BloodGroup;
                txtReligion.Value = EmpGeneralList[0].Religion;
                txtNationality.Value = EmpGeneralList[0].Nationality;
                //txtPhoneNo.Value = EmployeesList[0].CellNo;

                txtPresentHouseNo.Value = EmpGeneralList[0].HouseNo;
                txtPresentStreetName.Value = EmpGeneralList[0].StreetName;
                txtPresentAddressLine1.Value = EmpGeneralList[0].AddressLine1;
                txtPresentAddressLine2.Value = EmpGeneralList[0].AddressLine2;
                txtPresentCity.Value = EmpGeneralList[0].City;
                txtPresentState.Value = EmpGeneralList[0].State;

                txtPermanentHouseNo.Value = EmpGeneralList[0].p_HouseNo;
                txtPermanentStreetName.Value = EmpGeneralList[0].p_StreetName;
                txtPermanentAddressLine1.Value = EmpGeneralList[0].P_AddressLine1;
                txtPermanentAddressLine2.Value = EmpGeneralList[0].P_AddressLine2;
                txtPermanentCity.Value = EmpGeneralList[0].P_City;
                txtPermanentState.Value = EmpGeneralList[0].P_State;

                txtOfficeNo.Value = EmpGeneralList[0].ph_Office;
                txtRecidenceNo.Value = EmpGeneralList[0].ph_Residence;
                txtCellNo.Value = EmpGeneralList[0].CellNo;
                txtFaxNo.Value = EmpGeneralList[0].Fax;
                txtemgname.Value = EmpGeneralList[0].emgname;
                txtemgno.Value = EmpGeneralList[0].emgno;

                txt_father.Value = EmpGeneralList[0].FatherName;
                txt_mother.Value = EmpGeneralList[0].MotherName;
                txt_child.Value = EmpGeneralList[0].Children;
                txt_Spouse.Value = EmpGeneralList[0].SpouseName;

                txt_ref1_name.Value = EmpGeneralList[0].Ref1_Name;
                txt_ref1_phno.Value = EmpGeneralList[0].Ref1_Phno;
                txt_ref1_email.Value = EmpGeneralList[0].Ref1_Email;
                txt_ref1_relation.Value = EmpGeneralList[0].Ref1_Relation;

                txt_ref2_name.Value = EmpGeneralList[0].Ref2_Name;
                txt_ref2_phno.Value = EmpGeneralList[0].Ref2_Phno;
                txt_ref2_email.Value = EmpGeneralList[0].Ref2_Email;
                txt_ref2_relation.Value = EmpGeneralList[0].Ref2_Relation;

                general_ddl_retrive();

            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = ex.Message.ToString();
        }
    }

    public void ddl_retrive()
    {
        try
        {
            yr = EmpFirstList[0].Gender;
            for (year = 0; year < ddl_gender.Items.Count; year++)
            {
                if (ddl_gender.Items[year].Text == yr)
                {
                    ddl_gender.SelectedIndex = year;
                }
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = ex.Message.ToString();
        }
    }   
    public void save()
    {
        employee.EmployeeCode = txtEmployeeCode.Value;
        Response.Cookies["emp_Code"].Value = txtEmployeeCode.Value;
        //employee.FullName = txtfullname.Value;
        employee.FirstName = txtFirstName.Value;
        //employee.Role = Convert.ToInt32(Rolelode.SelectedItem.Value);
        employee.MiddleName = txtMiddleName.Value;
        employee.LastName = txtLastName.Value;
        employee.d_birth = employee.Convert_ToSqlDate(txt_dob.Text);
        employee.password = txtepwd.Value;
        employee.Gender = ddl_gender.SelectedItem.Text;
        employee.status = 'Y';
        employee.FullName = txtfullname.Value;
        employee.ReaderId = Convert.ToInt32(txt_Readerid.Value);
        employee.OT_Eligible = Convert.ToChar(rdo_btn.SelectedItem.Value);
        employee.PFno = txt_pfno.Value;
        employee.ESIno = txt_esino.Value;
        employee.OT_calc = Convert.ToDouble(ddl_ot.SelectedItem.Value);
        employee.basic = txt_basicsal.Value;
        employee.CardNo = txt_Readerid.Value;
        employee.IDtype = ddl_idtype.SelectedItem.Text;
        employee.IDOthers = txt_otherid.Value;
        employee.IDno = txt_idno.Value;
        employee.EmailId = txtEmailId.Value;
        employee.A_EmailId = txtAEmailId.Value;
        employee.BloodGroup = ddl_blood.SelectedItem.Text;
        employee.Religion = txtReligion.Value;
        employee.Nationality = txtNationality.Value;
        employee.ph_Office = txtOfficeNo.Value;
        employee.ph_Residence = txtRecidenceNo.Value;
        employee.CellNo = txtCellNo.Value;
        employee.Fax = txtFaxNo.Value;
        employee.emgname = txtemgname.Value;
        employee.emgno = txtemgno.Value;
        employee.HouseNo = txtPresentHouseNo.Value;
        employee.StreetName = txtPresentStreetName.Value;
        employee.AddressLine1 = txtPresentAddressLine1.Value;
        employee.AddressLine2 = txtPresentAddressLine2.Value;
        employee.City = txtPresentCity.Value;
        employee.State = txtPresentState.Value;
        employee.p_HouseNo = txtPermanentHouseNo.Value;
        employee.p_StreetName = txtPermanentStreetName.Value;
        employee.P_AddressLine1 = txtPermanentAddressLine1.Value;
        employee.P_AddressLine2 = txtPermanentAddressLine2.Value;
        employee.P_City = txtPermanentCity.Value;
        employee.P_State = txtPermanentState.Value;
        employee.Salutation = Convert.ToChar(rdo_salutation.SelectedItem.Value);
        employee.MaritalStatus = Convert.ToChar(ddl_marital.SelectedItem.Value);
        employee.FatherName = txt_father.Value;
        employee.MotherName = txt_mother.Value;
        employee.Children = txt_child.Value;
        employee.SpouseName = txt_Spouse.Value;
        employee.Ref1_Name = txt_ref1_name.Value;
        employee.Ref1_Phno = txt_ref1_phno.Value;
        employee.Ref1_Email = txt_ref1_email.Value;
        employee.Ref1_Relation = txt_ref1_relation.Value;
        employee.Ref2_Name = txt_ref2_name.Value;
        employee.Ref2_Phno = txt_ref2_phno.Value;
        employee.Ref2_Email = txt_ref2_email.Value;
        employee.Ref2_Relation = txt_ref2_relation.Value;
        employee.training_attended = txt_training_attend.Value;
        employee.training_duration = txt_training_duration.Value;
        employee.position = txt_position.Value;
        employee.salary = txt_basicsal.Value;
        employee.CTC = txt_basicsal.Value;
        employee.Bank_Code = txt_bankcode.Value;
        employee.Bank_Name = txt_bankname.Value;
        employee.Branch_Name = txt_branchname.Value;
        employee.Account_Type = txt_actype.Value;
        employee.MICR_Code = txt_micrcode.Value;
        employee.IFSC_Code = txt_ifsccode.Value;
        employee.Bank_Addr = txt_address.Text;
        employee.Other_Info = txt_otherinfo.Text;
        employee.Reporting = txt_rep.SelectedValue;
        employee.Pan_no = txt_panno.Value;
        employee.Salary_Type = rdo_saltype.SelectedItem.Text;
        employee.TDS = Convert.ToChar(rdo_tds.SelectedItem.Text);
    }

    public void general_ddl_retrive()
    {
        //Blood Group
        yr = EmpGeneralList[0].BloodGroup;

        for (year = 0; year < ddl_blood.Items.Count; year++)
        {
            if (ddl_blood.Items[year].Text == yr)
            {
                ddl_blood.SelectedIndex = year;
            }
        }
        //Salutation
        yr = Convert.ToString(EmpGeneralList[0].Salutation);
        for (year = 0; year < rdo_salutation.Items.Count; year++)
        {
            if (rdo_salutation.Items[year].Value == yr)
            {
                rdo_salutation.SelectedIndex = year;
            }
        }
        for (year = 0; year < rdo_salutation.Items.Count; year++)
        {
            if (rdo_salutation.Items[year].Value == "Mr")
            {
                rdo_salutation.SelectedIndex = 1;
            }
        }

        //Marital status

        yr = Convert.ToString(EmpGeneralList[0].MaritalStatus);

        for (year = 0; year < ddl_marital.Items.Count; year++)
        {
            if (ddl_marital.Items[year].Value == yr)
            {
                ddl_marital.SelectedIndex = year;

            }
        }
    }
    protected void rdo_btn_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdo_btn.SelectedItem.Value == "0")
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

    public void EmployeeLoad()
    {
        var EmpFirstList = employee.fn_getReportingList(employee);

        if (EmpFirstList.Count > 0 && txt_rep.Items.Count < 1)
        {

            for (int ddl_i = -1; ddl_i < EmpFirstList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();

                    e_list.Text = "----Select----";
                    e_list.Value = "0"; 
                    txt_rep.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();

                    e_list.Value = EmpFirstList[ddl_i].EmployeeId.ToString();
                    e_list.Text = EmpFirstList[ddl_i].LastName.ToString();
                    txt_rep.Items.Add(e_list);
                }
            }
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        //tab_empcodename.Visible = true;
        //reg_tbl.Visible = true;
        //personal_tbl.Visible = true;
        //perstbl.Visible = true;
        //contact_tbl.Visible = true;
        //contbl.Visible = true;
        //reference_tbl.Visible = true;
        //reftbl.Visible = true;
        //tbl_bank.Visible = true;
        //////tbl_bankhead.Visible = true;
        btn_Back.Focus();
        ////tab_empcodename.Visible = true;
        ////reg_tbl.Visible = true;
        //btn_opt.Visible = true;
        ////personal_tbl.Visible = false;
        ////perstbl.Visible = false;
        ////contact_tbl.Visible = false;
        ////contbl.Visible = false;
        ////reftbl.Visible = false;
        ////reference_tbl.Visible = false;
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        ////personal_tbl.Visible = true;
        //perstbl.Visible = true;
        //contact_tbl.Visible = false;
        //contbl.Visible = false;
        //reftbl.Visible = false;
        //reference_tbl.Visible = false;
        //tab_empcodename.Visible = false;
        //reg_tbl.Visible = false;
        //tbl_bank.Visible = false;
        //////tbl_bankhead.Visible = false;
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        //contact_tbl.Visible = true;
       // //contbl.Visible = true;
        //reftbl.Visible = false;
       // //reference_tbl.Visible = false;
        //tab_empcodename.Visible = false;
        //reg_tbl.Visible = false;
        //personal_tbl.Visible = false;
        //perstbl.Visible = false;
      //  //tbl_bank.Visible = false;
        //////tbl_bankhead.Visible = false;
    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        //reference_tbl.Visible = true;
        //reftbl.Visible = true;
        //personal_tbl.Visible = false;
        //perstbl.Visible = false;
        //contact_tbl.Visible = false;
        //contbl.Visible = false;
        //tab_empcodename.Visible = false;
        //reg_tbl.Visible = false;
        //tbl_bank.Visible = false;
        //////tbl_bankhead.Visible = false;
    }
    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        //reference_tbl.Visible = false;
        //reftbl.Visible = false;
        //personal_tbl.Visible = false;
        //perstbl.Visible = false;
        //contact_tbl.Visible = false;
        //contbl.Visible = false;
        //tab_empcodename.Visible = false;
        //reg_tbl.Visible = false;
        //tbl_bank.Visible = true;
        //////tbl_bankhead.Visible = true;
    }

    //protected void ddl_vehicle_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    myConnection.Open();
    //    int driver_id = Convert.ToInt32(txt_vehicle.Text);
    //    SqlCommand cmd = new SqlCommand("select a.destination a.vehicle_number,a.driver_id,b.employee_first_name from paym_bus_Details a,paym_Employee b where a.pn_companyID='" + employee.CompanyId + "' and a.vehicle_id='" + driver_id + "' and a.driver_id=b.pn_employeeID", myConnection);
    //    SqlDataReader rdr = cmd.ExecuteReader();
    //    while (rdr.Read())
    //    {
    //        Txt_area.Value = rdr["destination"].ToString();
    //        Txt_driver_id.Value = rdr["driver_id"].ToString();
    //        Txt_driver.Value = rdr["employee_first_name"].ToString();
    //        Txt_veh_number.Value = rdr["vehicle_number"].ToString();
    //    }
    //    myConnection.Close();
    //    rdr.Close();
    //}


    protected void btn_avail_Click1(object sender, EventArgs e)
    {
        try
        {
            if (txtEmployeeCode.Value.Trim() != "")
            {
                emp_available = employee.fn_get_EmployeeID(txtEmployeeCode.Value);
                if (emp_available.Count == 0)
                {
                    emp_available = employee.fn_get_TempID(txtEmployeeCode.Value);

                    if (emp_available.Count == 0)
                    {
                        ClientScriptManager manager = Page.ClientScript;
                        manager.RegisterStartupScript(this.GetType(), "Call", "show_message('Employee Code is Available');", true);
                    }
                    else
                    {
                        ClientScriptManager manager = Page.ClientScript;
                        manager.RegisterStartupScript(this.GetType(), "Call", "show_message('Employee Code Already Exist');", true);
                    }
                }
                else
                {
                    ClientScriptManager manager = Page.ClientScript;
                    manager.RegisterStartupScript(this.GetType(), "Call", "show_message('Employee Code Already Exist');", true);
                }
            }
            else
            {
                ClientScriptManager manager = Page.ClientScript;
                manager.RegisterStartupScript(this.GetType(), "Call", "show_message('Enter Employee Code');", true);
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = ex.Message.ToString();
        }
    }
    protected void btn_Back_Click(object sender, EventArgs e)
    {
        try
        {
            if (pr_emp == 1)
            {
                //Response.Redirect("../Hrms_Company/Employee.aspx");

                Response.Redirect("../Hrms_Company/Employee.aspx");
            }
            else
            {
                Response.Redirect("Employee_Preview.aspx");

            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = ex.Message.ToString();
        }
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {
        try
        {
            if ((string)ViewState["vs_empcode"] == txtEmployeeCode.Value)
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
                }

                employee.password = (string)ViewState["password"];

                save();

                _Value = employee.Employee_First(employee);

                _Value = employee.Employee_General(employee);


                if (_Value != "1")
                {
                    lbl_Error.Text = "<font color=Blue>Added Successfully</font>";

                    Session["Profile_Error"] = "";

                    Response.Cookies["Employee_Code_FirstLastName"].Value = txtEmployeeCode.Value + " - " + txtFirstName.Value + " " + txtLastName.Value;

                    Response.Redirect("Employee_Preview.aspx");
                }

                else
                {
                    lbl_Error.Text = "<font color=Red>Error Occured</font>";
                }

            }
            else
            {
                emp_available = employee.fn_get_EmployeeID(txtEmployeeCode.Value);

                if (emp_available.Count == 0)
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
                    }

                    employee.password = (string)ViewState["password"];

                    save();

                    _Value = employee.Employee_First(employee);

                    _Value = employee.Employee_General(employee);

                    if (_Value != "1")
                    {
                        lbl_Error.Text = "<font color=Blue>Added Successfully</font>";

                        Session["Profile_Error"] = "";

                        Response.Cookies["Employee_Code_FirstLastName"].Value= txtEmployeeCode.Value + " - " + txtFirstName.Value + " " + txtLastName.Value;


                        Response.Redirect("../Hrms_Employee/Employee_Preview.aspx");
                    }
                    else
                    {
                        lbl_Error.Text = "<font color=Red>Error Occured</font>";
                    }


                }
                else
                {
                    ClientScriptManager manager = Page.ClientScript;
                    manager.RegisterStartupScript(this.GetType(), "Call", "show_message('Employee Code Already Exist');", true);
                }

            }

        }
        catch (Exception ex)
        {
            lbl_Error.Text = ex.Message.ToString();
        }
    }


    protected void btn_save_Click(object sender, EventArgs e)
    {
        
            try
            {
                emp_available = employee.fn_get_EmployeeID(txtEmployeeCode.Value);

                if (emp_available.Count == 0)
                {

                    if (s_login_role == "a")
                    {
                        employee.EmployeeId = 0;

                        employee.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
                        pay.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
                        c.BranchID = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
                    }

                    if (s_login_role == "h")
                    {
                        employee.EmployeeId = 0;

                        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                        pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                        c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

                    }

                    employee.password = txtepwd.Value;

                    save();
                    _Value = employee.Employee_First(employee);
                    transport_save();
                    _Value1 = employee.Transport_details(employee);

                    emp_ID_List = employee.fn_get_EmployeeID(txtEmployeeCode.Value);

                    if (emp_ID_List.Count > 0)
                    {
                        employee.EmployeeId = Convert.ToInt32(emp_ID_List[0].EmployeeId);
                        pay.EmployeeId = Convert.ToInt32(emp_ID_List[0].EmployeeId);
                        c.EmployeeID = Convert.ToInt32(emp_ID_List[0].EmployeeId);
                        employee.EmployeeCode = txtEmployeeCode.Value;
                        Response.Cookies["preview_EmployeeID"].Value = emp_ID_List[0].EmployeeId.ToString();

                        _Value = employee.Employee_General(employee);
                        _data = employee.Employee_salary(employee);

                    }


                    if (_Value != "1" && _data != "1")
                    {   
                    //lbl_Error.Text = "<font color=Blue>Added Successfully</font>";
                    ClientScriptManager manager = Page.ClientScript;
                    manager.RegisterStartupScript(this.GetType(), "Call", "show_message('Employee Added Successfully');", true);

                    Response.Cookies["Profile_Check"].Value = "1";
                        Session["Profile_Error"] = "";

                        Response.Cookies["Employee_Code_FirstLastName"].Value = txtEmployeeCode.Value + " - " + txtFirstName.Value + " " + txtLastName.Value;

                    //myConnection.Open();
                    //cmd = new SqlCommand("update paym_employee set basic_salary = '" + txt_basicsal.Value + "' , Bank_code = '" + txt_bankcode.Value + "' , Bank_Name = '" + txt_bankname.Value + "' , Branch_Name = '" + txt_branchname.Value + "' , Account_type = '" + txt_actype.Value + "' , MICR_code = '" + txt_micrcode.Value + "' , IFSC_code = '" + txt_ifsccode.Value + "' , Address = '" + txt_address.Text + "' , Other_info = '" + txt_otherinfo.Text + "', reporting_person = '" + txt_rep.Value + "' , Pan_no = '"+txt_panno.Value+"' , salary_type = '"+rdo_saltype.SelectedItem.Text+"' , TDS_Applicable = '"+rdo_tds.SelectedItem.Text+"'  where EmployeeCode = '" + txtEmployeeCode.Value + "'  ", myConnection);
                    //cmd.ExecuteNonQuery();
                    //myConnection.Close();

                    Response.Redirect("Employee_Education.aspx", false);
                    }
                    else
                    {
                        //lbl_Error.Text = "<font color=Red></font>";
                    ClientScriptManager manager = Page.ClientScript;
                    manager.RegisterStartupScript(this.GetType(), "Call", "show_message('Error Occured');", true);
                }

                }
                else
                {
                    ClientScriptManager manager = Page.ClientScript;
                    manager.RegisterStartupScript(this.GetType(), "Call", "show_message('Employee Code Already Exist');", true);
                }
            }

            catch (Exception ex)
            {
                lbl_Error.Text = ex.Message.ToString();
           }
    }

    protected void btn_edit_Click(object sender, EventArgs e)
    {
        myConnection.Open();
        session_emp_code = (string)Session["Session_emp_code"];
        SqlCommand cmd = new SqlCommand("select pn_employeeID from paym_Employee where employeecode='" + session_emp_code + "'", myConnection);
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            employee.EmployeeId = Convert.ToInt32(dr["pn_employeeID"]);
        }
        employee.Area_name = Txt_area.Value;
        employee.Veh_id = txt_vehicle.Text;
        employee.Veh_number = Txt_veh_number.Value;
        employee.Driver_id = Txt_driver_id.Value;
        employee.Boarding_point = Txt_point.Value;
        myConnection.Close();
        _Value1 = employee.Transport_details(employee);
        session_emp_code = null;
        Session["Session_emp_code"] = null;
        Response.Redirect("../Hrms_Employee/Employee_Preview.aspx");
    }

    private SqlConnection _Connection;
    ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();

    [WebMethod]
    public Collection<PayRoll> bank_auto(string bank)
    {
        Collection<PayRoll> banklist = new Collection<PayRoll>();
        _Connection = Con.fn_Connection();
        SqlCommand cmd = new SqlCommand("sp_bank_auto", _Connection);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@bank", bank);
        _Connection.Open();
        SqlDataReader dr = null;
        dr = cmd.ExecuteReader();
        _Connection.Close();
        return banklist;

    }

    //public void rolelode()
    //{
    //    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);

    //    SqlCommand cmd = new SqlCommand("spr_role_retrieve", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    con.Open();
    //    using (SqlDataReader sdr = cmd.ExecuteReader())
    //    {
    //        DataTable dt = new DataTable();
    //        dt.Load(sdr);

    //        if (Rolelode.Items.Count<=1 && dt.Rows.Count > 0 )
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

}

    

