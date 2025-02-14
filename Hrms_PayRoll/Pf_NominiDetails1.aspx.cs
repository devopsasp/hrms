﻿using System;
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

public partial class Hrms_PayRoll_Pf_NominiDetails : System.Web.UI.Page
{
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    private SqlConnection _connection;
    SqlConnection mycon = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    //private SqlConnection _con;
    //ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlCommand cmd = new SqlCommand();

    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    Candidate c = new Candidate();
    Company company = new Company();
    Collection<Employee> EmployeeList;
    Collection<Employee> EmployeesList;
    Collection<Employee> emp_ID_List;
    Collection<Employee> emp_available;
    Collection<Employee> EmpFirstList;
    Collection<Employee> EmpGeneralList;

    Collection<PayRoll> emp_edu_List;
    Collection<PayRoll> Empty_gridList;
    Collection<PayRoll> EpsList;
    Collection<PayRoll> epflist;
    string str_query;
    int ddl_i;
    int i, yr_it, cur_yr, mon, dat, year, pr_emp;
    string _Value,_value, _data, dt, mn, yr, dob_edit, default_sqldate = "01/01/1900";
    string s_login_role;

    string s_form = "";
    DataSet ds_userrights;

    protected void Page_Load(object sender, EventArgs e)
    {

        
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        lbl_Error.Text = "";
        // Error.Text = "";

        if (!IsPostBack)
        {
            switch (s_login_role)
            {
                case "a": //load();
                    break;

                case "h":
                    // rounds();
                    //gridload();
                    ddl_department_load();
                    //load();
                    // access();
                    break;

                case "u": s_form = "30";
                    ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                    if (ds_userrights.Tables[0].Rows.Count > 0)
                    {
                        //load();
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
    protected void btn_save_Click(object sender, ImageClickEventArgs e)
    {

    }
    public void gridload()
    {
        try
        {
            epflist = pay.Re_EPF(pay);

            if (epflist.Count > 0)
            {
                GridView1.DataSource = epflist;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = epflist;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }
    public void hr()
    {
        try
        {

            ddl_employee.Items.Clear();
            Collection<Employee> EmployeeList = employee.fn_getEmployeeList(employee);

            if (EmployeeList.Count > 0)
            {

                for (ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
                {

                    if (ddl_i == -1)
                    {
                        ListItem e_list = new ListItem();

                        e_list.Text = "Select Employee";
                        e_list.Value = "0";
                        ddl_employee.Items.Add(e_list);
                    }
                    else
                    {

                        ListItem e_list = new ListItem();

                        e_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
                        e_list.Text = EmployeeList[ddl_i].LastName.ToString();
                        ddl_employee.Items.Add(e_list);

                    }

                }
            }
            else
            {
                lbl_Error.Text = "No Employees Available";

            }


        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }


    protected void btn_save_epf_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            pay.EmployeeId = Convert.ToInt32(ddl_employee.SelectedValue);
            pay.Nomineename = txt_nominee.Value;
            pay.Gender = ddl_gender.SelectedItem.Text;
            pay.Dob = Convert.ToDateTime(txt_DOB.Value);
            pay.Pf_share = Convert.ToDecimal(txt_PF_Share.Value);
            pay.Relationship = ddl_relationship_epf.SelectedItem.Text;
            pay.PermanentAddress1 = txt_address1.Value;
           // pay.PermanentAddress2 = txt_address2.Value;
            pay.PermanentState = txt_state.Value;
            pay.PermanentDistrict = txt_district.Value;
            pay.PermanentCity = txt_city.Value;
            pay.PermanentPincode = txt_pincode.Value;

            _value = pay.EPF(pay);
            if (_value != "1")
            {
                lbl_Error.Text = "Added Successfully";

            }
            else
            {
                lbl_Error.Text = "Error";
            }

            gridload();
        }
        catch (Exception ex)
        {

        }
    }
   

    
    protected void chk_address_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_address.Checked == true)
        {
            txtaddress11.Value = txtaddress1.Value;
            //txtaddress22.Value = txtaddress2.Value;
            txtcity2.Value = txtcity1.Value;
            txtstate2.Value = txtstate1.Value;
            txtdistrict2.Value = txtdistrict1.Value;
            txtpincode2.Value = txtpincode1.Value;
        }
        else if (chk_address.Checked == false)
        {
            txtaddress11.Value = "";
           // txtaddress22.Value = "";
            txtcity2.Value = "";
            txtstate2.Value = "";
            txtdistrict2.Value = "";
            txtpincode2.Value = "";
        }
    }
    protected void btn_memberdetails_save_Click(object sender, ImageClickEventArgs e)
    {

    }


    protected void btn_eps_Click(object sender, ImageClickEventArgs e)
    {

    }
    public void ddl_department_load()
    {
        ddl_dept.Items.Clear();
        EmployeeList = employee.fn_getDepartmentList1(employee.BranchId);
        if (EmployeeList.Count > 0)
        {

            for (int ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem list = new ListItem();

                    list.Text = "Select Department";
                    list.Value = "sd";
                    ddl_dept.Items.Add(list);
                }
                else
                {
                    ListItem list = new ListItem();

                    list.Text = EmployeeList[ddl_i].DepartmentName;
                    list.Value = EmployeeList[ddl_i].DepartmentId.ToString();
                    ddl_dept.Items.Add(list);
                }
            }
        }
        else
        {
            lbl_Error.Text = "No Department Available";
        }

    }
    public void ddl_employee_load()
    {
        //employee dropdown
        ddl_employee.Items.Clear();

        if (s_login_role == "a")
        {
            employee.BranchId = (int)ViewState["Appraisal_BranchID"];
        }

        if (s_login_role == "h")
        {
            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        }

        str_query = "Select a.pn_EmployeeID, a.EmployeeCode, a.Employee_First_Name from paym_employee a, paym_employee_profile1 b where a.pn_CompanyID=b.pn_CompanyID and a.pn_BranchID=b.pn_BranchID and a.pn_EmployeeID=b.pn_EmployeeID and b.pn_DepartmentID=" + ddl_dept.SelectedValue + " and b.pn_CompanyID=" + employee.CompanyId + " and a.status='y' and b.pn_BranchID=" + employee.BranchId;

        EmployeeList = employee.fn_getEmplist(str_query);

        if (EmployeeList.Count > 0)
        {
            for (ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();
                    e_list.Text = "Select";
                    e_list.Value = "0";
                    ddl_employee.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();
                    e_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
                    e_list.Text = EmployeeList[ddl_i].LastName.ToString();
                    ddl_employee.Items.Add(e_list);
                }
            }
        }
        else
        {
            lbl_Error.Text = "No Employee";
        }
    }
  
    protected void chk_address_eps_CheckedChanged(object sender, EventArgs e)
    {
       
            txtaddress1_eps.Value = txtaddress1.Value;
            //txtaddredd2_eps.Value = txtaddress2.Value;
            txtcity_eps.Value = txtcity1.Value;
            txt_state_eps.Value = txtstate1.Value;
            txtdistrict_eps.Value = txtdistrict1.Value;
            txt_pincode_eps.Value = txtpincode1.Value;
        
    }
    protected void ddl_dept_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_employee_load();
    }
    protected void ddl_employee_SelectedIndexChanged1(object sender, EventArgs e)
    {
        mycon.Open();
        cmd = new SqlCommand("select a.Account_type,a.employee_first_name,b.presentcity,b.presentstate,b.PermanentStreetName,b.PresentStreetName,b.fathername,b.mothername,a.gender,a.dateofbirth,b.permanentcity,b.permanentstate,b.ph_residence,b.emailid,(b.PresentHouseNo+','+b.PresentAddLine1+','+b.PresentAddLine2) as tempaddress,(b.PermanentHouseNo+','+b.PresentAddLine1+','+b.PermanentAddLine2) as permanentaddress from paym_Employee a,paym_Employee_General b where a.pn_employeeid='" + ddl_employee.SelectedValue + "' and b.pn_employeeid='" + ddl_employee.SelectedValue + "' and a.pn_companyid=b.pn_companyid and a.pn_branchid=b.pn_branchid", mycon);
        SqlDataReader rdr = cmd.ExecuteReader();
        if (rdr.Read())
        {
            txtAccNo.Value = rdr["Account_type"].ToString();
            txtName.Value = rdr["employee_first_name"].ToString();
            txtfathername.Value = rdr["fathername"].ToString();
            txt_mother.Value = rdr["mothername"].ToString();
            ddlgender.SelectedItem.Text = rdr["gender"].ToString();
            txtdob.Value = rdr["dateofbirth"].ToString();
            txtaddress1.Value = rdr["permanentaddress"].ToString();
            txtaddress11.Value = rdr["tempaddress"].ToString();
            txtcity1.Value = rdr["permanentcity"].ToString();
            txtcity2.Value = rdr["presentcity"].ToString();
            txtstate2.Value = rdr["presentstate"].ToString();
            txtstate1.Value = rdr["permanentstate"].ToString();
            txtphoneno.Value = rdr["ph_residence"].ToString();
            txtemail.Value = rdr["emailid"].ToString();
            txtpincode1.Value = rdr["PermanentStreetName"].ToString();
            txtpincode2.Value = rdr["PresentStreetName"].ToString();
        }
        mycon.Close();
        pay.EmployeeId = Convert.ToInt32(ddl_employee.SelectedValue);
        load();
        gridload();
    }

    
    protected void btn_eps_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {

            pay.EmployeeId = Convert.ToInt32(ddl_employee.SelectedValue);
            pay.Nomineename = txtnominee_name.Value;
            pay.Gender = ddl_gender_eps.SelectedItem.Text;
            pay.Dob = Convert.ToDateTime(txtdob_eps.Value);
            pay.Relationship = ddl_relationship.SelectedItem.Text;
            pay.PermanentAddress1 = txtaddress1_eps.Value;
            //pay.PermanentAddress2 = txtaddredd2_eps.Value;
            pay.PermanentState = txt_state_eps.Value;
            pay.PermanentCity = txtcity_eps.Value;
            pay.PermanentDistrict = txtdistrict_eps.Value;
            pay.PermanentPincode = txt_pincode_eps.Value;
            _Value = pay.fn_epsdetails(pay);
            if (_Value != "1")
            {
                lbl_Error.Text = "<font color=Blue>Added Successfully</font>";
                // load();
            }
            else
            {
                lbl_Error.Text = "<font color=Red>Error Occured</font>";
                // load();
            }

            load();

        }
        catch (Exception ex)
        {

        }

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        pay.EmployeeId = Convert.ToInt32(ddl_employee.SelectedValue);
        GridView1.EditIndex = e.NewEditIndex;
        Label gender = (Label)GridView1.Rows[e.NewEditIndex].FindControl("lbl_Gender");
        Label Relationship = (Label)GridView1.Rows[e.NewEditIndex].FindControl("label_Relationship");
        gridload();
        DropDownList gender1 = (DropDownList)GridView1.Rows[e.NewEditIndex].FindControl("ddl_edit_Gender1");
        DropDownList Relationship1 = (DropDownList)GridView1.Rows[e.NewEditIndex].FindControl("ddl_edit_relationship");
        gender1.SelectedItem.Text = gender.Text;
        Relationship1.SelectedItem.Text = Relationship.Text;
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow Gvrow = GridView1.Rows[e.RowIndex];
        // Label8.Text = Gvrow.ToString();
        if (Gvrow != null)
        {
            pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
             pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            pay.EmployeeId = Convert.ToInt32(((Label)Gvrow.FindControl("lblemp")).Text);

            pay.ID = Convert.ToInt32(((Label)Gvrow.FindControl("lblid")).Text);
            pay.Nomineename = ((TextBox)Gvrow.FindControl("txt_nominee1")).Text;
            pay.Gender = ((DropDownList)Gvrow.FindControl("ddl_edit_Gender1")).Text;
            pay.Dob = Convert.ToDateTime(((TextBox)Gvrow.FindControl("txt_DOB1")).Text);
            pay.Pf_share = Convert.ToDecimal(((TextBox)Gvrow.FindControl("txt_Share1")).Text);
            pay.Relationship = ((DropDownList)Gvrow.FindControl("ddl_edit_relationship")).SelectedItem.Text;
            pay.PermanentAddress1 = ((TextBox)Gvrow.FindControl("txt_address11")).Text;
            //pay.PermanentAddress2 = ((TextBox)Gvrow.FindControl("txt_address21")).Text;
            pay.PermanentState = ((TextBox)Gvrow.FindControl("txt_State1")).Text;
            pay.PermanentDistrict = ((TextBox)Gvrow.FindControl("txt_District1")).Text;
            pay.PermanentCity = ((TextBox)Gvrow.FindControl("txt_City1")).Text;
            pay.PermanentPincode = ((TextBox)Gvrow.FindControl("txt_postalcode1")).Text;
            _value = pay.EPF(pay);
            if (_value != "1")
            {
                lbl_Error.Text = "Updated Successfully";
                gridload();
            }
            else
            {
                lbl_Error.Text = "Error while updating";
            }
        }
        GridView1.EditIndex = -1;
        pay.EmployeeId = Convert.ToInt32(ddl_employee.SelectedValue);
        gridload();

    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        pay.EmployeeId = Convert.ToInt32(ddl_employee.SelectedValue);
        //EmployeeList = employee.fn_getDepartmentList1(employee.BranchId);
        gridload();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string ID = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lblemp")).Text;

        DeleteRecord(ID);
        pay.EmployeeId = Convert.ToInt32(ddl_employee.SelectedValue);
        gridload();
    }
    
    private void DeleteRecord(string ID)
    {

        string sqlStatement = "DELETE FROM PF_EPF WHERE id = @id";
        try
        {
            _connection = con.fn_Connection();
            _connection.Open();
            SqlCommand cmd = new SqlCommand(sqlStatement, _connection);
            cmd.Parameters.AddWithValue("@id", ID);
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
            _connection.Close();
        }
    }
    protected void GridView3_RowEditing(object sender, GridViewEditEventArgs e)
    {
        pay.EmployeeId = Convert.ToInt32(ddl_employee.SelectedValue);
        GridView3.EditIndex = e.NewEditIndex;
        Label gender = (Label)GridView3.Rows[e.NewEditIndex].FindControl("lblgender");
        Label Relationship = (Label)GridView3.Rows[e.NewEditIndex].FindControl("lblrelationship");
        load();
        DropDownList gender1 = (DropDownList)GridView3.Rows[e.NewEditIndex].FindControl("ddl_gender");
        DropDownList Relationship1 = (DropDownList)GridView3.Rows[e.NewEditIndex].FindControl("ddl_relationship1");
        gender1.SelectedItem.Text = gender.Text;
        Relationship1.SelectedItem.Text = Relationship.Text;
    }
    public void load()
    {
        try
        {
            EpsList = pay.epsload(pay);

            if (EpsList.Count > 0)
            {
                GridView3.DataSource = EpsList;
                GridView3.DataBind();
            }
            else
            {
                GridView3.DataSource = EpsList;
                GridView3.DataBind();
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }
    protected void GridView3_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView3.EditIndex = -1;
        pay.EmployeeId = Convert.ToInt32(ddl_employee.SelectedValue);
        load();
    }
    protected void GridView3_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            GridViewRow Gvrow = GridView3.Rows[e.RowIndex];

            if (Gvrow != null)
            {
                pay.EmployeeId = Convert.ToInt32(((Label)Gvrow.FindControl("lblemp")).Text);
                pay.ID = Convert.ToInt32(((Label)Gvrow.FindControl("lblemployeeid")).Text);
                pay.Nomineename = ((HtmlInputText)Gvrow.FindControl("txtnominee")).Value;
                pay.Gender = ((DropDownList)Gvrow.FindControl("ddl_gender")).SelectedItem.Text;
                pay.Dob = Convert.ToDateTime(((HtmlInputText)Gvrow.FindControl("txtdob")).Value);
                pay.Relationship = ((DropDownList)Gvrow.FindControl("ddl_relationship1")).SelectedItem.Text;
                pay.PermanentAddress1 = ((HtmlInputText)Gvrow.FindControl("txtaddr1")).Value;
               // pay.PermanentAddress2 = ((HtmlInputText)Gvrow.FindControl("txtaddr2")).Value;
                pay.PermanentDistrict = ((HtmlInputText)Gvrow.FindControl("txtdistrict1")).Value;
                pay.PermanentCity = ((HtmlInputText)Gvrow.FindControl("txtcity")).Value;
                pay.PermanentState = ((HtmlInputText)Gvrow.FindControl("txtstate111")).Value;
                pay.PermanentPincode = ((HtmlInputText)Gvrow.FindControl("txtpincode_1")).Value;
                _Value = pay.fn_epsdetails(pay);
                if (_Value != "1")
                {
                    lbl_Error.Text = "<font color=Blue>Updated Successfully</font>";
                    load();
                }
                else
                {
                    lbl_Error.Text = "<font color=Red>Error Occured</font>";
                    load();
                }
                GridView3.EditIndex = -1;
                pay.EmployeeId = Convert.ToInt32(ddl_employee.SelectedValue);
                load();
            }

        }
        catch (Exception ex)
        {
        }
    }

    protected void GridView3_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string ID = ((Label)GridView3.Rows[e.RowIndex].Cells[0].FindControl("lblemployeeid")).Text;

        DeleteRecord1(ID);
        pay.EmployeeId = Convert.ToInt32(ddl_employee.SelectedValue);
        load();
        
    }
    private void DeleteRecord1(string ID)
    {

        string sqlStatement = "DELETE FROM PF_EPS WHERE id = @id";
        try
        {
            _connection = con.fn_Connection();
            _connection.Open();
            SqlCommand cmd = new SqlCommand(sqlStatement, _connection);
            cmd.Parameters.AddWithValue("@id", ID);
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
            _connection.Close();
        }
    }
    protected void chkbx1_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void img_btn_memberdetails_Click(object sender, ImageClickEventArgs e)
    {
        memberdetails_header.Visible = true;
        memberdetails.Visible = true;
        memberdetails_save.Visible = true;
        epf_header.Visible = false;
        epf.Visible = false;
        epf_save.Visible = false;
        eps_header.Visible = false;
        eps.Visible = false;
        eps_save.Visible = false;
       
    }

    protected void img_btn_epf_Click(object sender, ImageClickEventArgs e)
    {
        epf_header.Visible = true;
        epf.Visible = true;
        epf_save.Visible = true;
        memberdetails_header.Visible = false;
        memberdetails.Visible = false;
        memberdetails_save.Visible = false;
        eps_header.Visible = false;
        eps.Visible = false;
        eps_save.Visible = false;
    }
    protected void img_btn_eps_Click(object sender, ImageClickEventArgs e)
    {
        eps_header.Visible = true;
        eps.Visible = true;
        eps_save.Visible = true;
        memberdetails_header.Visible = false;
        memberdetails.Visible = false;
        memberdetails_save.Visible = false;
        epf_header.Visible = false;
        epf.Visible = false;
        epf_save.Visible = false;
    }
    protected void chkbx_Address_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void chkbx_Address_CheckedChanged1(object sender, EventArgs e)
    {
       
            txt_address1.Value = txtaddress1.Value;
           // txt_address2.Value = txtaddress2.Value;
            txt_city.Value = txtcity1.Value;
            txt_state.Value = txtstate1.Value;
            txt_district.Value = txtdistrict1.Value;
            txt_pincode.Value = txtpincode1.Value;
       
    }
}

