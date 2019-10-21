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
using System.Windows.Forms;


public partial class Hrms_Master_Default : System.Web.UI.Page
{
    private SqlConnection _connection;
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    SqlDataReader rea;
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlConnection mycon = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    Company company = new Company();

    Employee employee = new Employee();

    Collection<Company> BranchsList;
    Collection<Employee> ShiftList;
    Collection<Company> CompanyList;

    int company_Id, branch_Id, valid, temp_valid = 0, check;
    string _Value;
    string s_login_role;
    string s_form = "";
    DataSet ds_userrights;

    protected void Page_Load(object sender, EventArgs e)
    {
            
            employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
            s_login_role = Request.Cookies["Login_temp_Role"].Value;
            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

            lbl_Error.Text = "";
            Error.Text = "";

            if (!IsPostBack)
            {
                switch (s_login_role)
                {
                    case "a": load();
                        break;

                    case "h": load1();
                        access();
                        break;

                    case "u": s_form = "15";
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

        ShiftList = employee.fn_Shift();

        if (ShiftList.Count > 0)
        {
            grid_Shift.DataSource = ShiftList;
            grid_Shift.DataBind();
        }
        else
        {
            ShiftList = employee.fn_EmptyShiftList();

            if (ShiftList.Count > 0)
            {
                grid_Shift.DataSource = ShiftList;
                grid_Shift.DataBind();

                ((ImageButton)grid_Shift.Rows[0].FindControl("img_update")).Visible = false;
            }

        }


        BranchsList = company.fn_getBranchs();

        if (BranchsList.Count > 0)    //first branch is company
        {
            grid_Branch.DataSource = BranchsList;
            grid_Branch.DataBind();

        }

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
            string accesserror = "Permission Restricted. Please Contact Administrator.";
            MessageBox.Show(accesserror);
            //Response.Write("<script>alert('Permission Restricted. Please Contact Administrator.')</script>");
            Response.Redirect("~/Company_Home.aspx");
        }
        rdrview.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and section_edit='No'", _connection);
        SqlDataReader rdredit = cmd.ExecuteReader();
        if (rdredit.Read())
        {
            for (int b = 0; b < grid_Shift.Rows.Count; b++)
            {
                ((ImageButton)grid_Shift.Rows[b].FindControl("img_update")).Visible = false;
            }
            ((System.Web.UI.Control)grid_Shift.HeaderRow.FindControl("lbledit")).Visible = false;
        }
        rdredit.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and  section_delete='No'", _connection);
        SqlDataReader rdrdel = cmd.ExecuteReader();
        if (rdrdel.Read())
        {
            // ((ImageButton)grid_Course.Rows[0].FindControl("img_update")).Visible = false;
            for (int a = 0; a < grid_Shift.Rows.Count; a++)
            {
                ((ImageButton)grid_Shift.Rows[a].FindControl("imgdel")).Visible = false;
            }
            ((System.Web.UI.Control)grid_Shift.HeaderRow.FindControl("lbldel")).Visible = false;
        }
        rdrdel.Close();

    }

    public void load1()
    {
        grid_Branch.Visible = false;
        Button2.Visible = false;
        ShiftList = employee.fn_Shift1(employee.BranchId);

        if (ShiftList.Count > 0)
        {
            grid_Shift.DataSource = ShiftList;
            grid_Shift.DataBind();
            for (int c = 0; c < ShiftList.Count; c++)
            {
                grid_Shift.Rows[c].FindControl("chkid").Visible = false;
            }
        }
        else
        {
            ShiftList = employee.fn_EmptyShiftList();

            if (ShiftList.Count > 0)
            {
                grid_Shift.DataSource = ShiftList;
                grid_Shift.DataBind();

                ((ImageButton)grid_Shift.Rows[0].FindControl("img_update")).Visible = false;
            }

        }


    }


    protected void Edit(object sender, GridViewEditEventArgs e)
    {
        try
        {
            employee.ShiftId = Convert.ToInt32(grid_Shift.DataKeys[e.NewEditIndex].Value);
            employee.ShiftName = ((HtmlInputText)grid_Shift.Rows[e.NewEditIndex].FindControl("txtgrid")).Value;
            employee.ShiftFrom = ((System.Web.UI.WebControls.TextBox)grid_Shift.Rows[e.NewEditIndex].FindControl("txtgrid1")).Text;
            employee.ShiftTo = ((System.Web.UI.WebControls.TextBox)grid_Shift.Rows[e.NewEditIndex].FindControl("txtgrid2")).Text;

            if (employee.ShiftName != "")
            {

                check = 0;
                //check = name_validate(employee.ShiftName);

                if (check == 0)
                {


                    employee.fn_Update_Shift(employee);

                    ((ImageButton)grid_Shift.Rows[e.NewEditIndex].FindControl("img_update")).Visible = true;
                    ((ImageButton)grid_Shift.Rows[e.NewEditIndex].FindControl("img_save")).Visible = false;
                    ((HtmlInputText)grid_Shift.Rows[e.NewEditIndex].FindControl("txtgrid")).Disabled = true;
                    ((System.Web.UI.WebControls.TextBox)grid_Shift.Rows[e.NewEditIndex].FindControl("txtgrid1")).Enabled = false;
                    ((System.Web.UI.WebControls.TextBox)grid_Shift.Rows[e.NewEditIndex].FindControl("txtgrid2")).Enabled = false;


                }
                else
                {
                    ClientScriptManager manager = Page.ClientScript;
                    manager.RegisterStartupScript(this.GetType(), "Call", "show_message();", true);
                }
            }
            else
            {

                ClientScriptManager manager = Page.ClientScript;
                manager.RegisterStartupScript(this.GetType(), "Call", "show_Error();", true);

            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";


        }

    }

    protected void Delete(object sender, GridViewDeleteEventArgs e)
    {
        //int Index = (int)AppointmentGrid.DataKeys[e.RowIndex].Value;
    }

    //protected void Button1_Click(object sender, EventArgs e)
    //{

    //}

    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {

            //DepartmentGrid
            //grid_Branch

            for (int j = 0; j < grid_Shift.Rows.Count; j++)
            {

                GridViewRow Shift_row = grid_Shift.Rows[j];


                bool Shift_check = ((HtmlInputCheckBox)Shift_row.FindControl("Chk_Shift")).Checked;

                if (Shift_check)
                {


                    for (int i = 0; i < grid_Branch.Rows.Count; i++)
                    {


                        GridViewRow Branch_row = grid_Branch.Rows[i];

                        bool Branch_check = ((HtmlInputCheckBox)Branch_row.FindControl("Chk_Branch")).Checked;


                        if (Branch_check)
                        {


                            //employee.CompanyId = company_Id;
                            employee.ShiftId = Convert.ToInt32(grid_Shift.DataKeys[Shift_row.RowIndex].Value);
                            employee.BranchId = Convert.ToInt32(grid_Branch.DataKeys[Branch_row.RowIndex].Value);



                            _Value = employee.Shift(employee);

                            if (_Value != "1")
                            {
                                Error.Text = "<font color=Blue>Added Successfully</font>";
                            }
                            else
                            {
                                Error.Text = "<font color=Red>Error Occured</font>";
                            }

                        }

                    }

                }

            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";


        }


    }





    protected void Update(object sender, GridViewUpdateEventArgs e)
    {
        try
        {

        ((HtmlInputText)grid_Shift.Rows[e.RowIndex].FindControl("txtgrid")).Disabled = false;
        ((System.Web.UI.WebControls.TextBox)grid_Shift.Rows[e.RowIndex].FindControl("txtgrid1")).Enabled = true;
            ((System.Web.UI.WebControls.TextBox)grid_Shift.Rows[e.RowIndex].FindControl("txtgrid2")).Enabled = true;

            ((ImageButton)grid_Shift.Rows[e.RowIndex].FindControl("img_save")).Visible = true;
            ((ImageButton)grid_Shift.Rows[e.RowIndex].FindControl("img_update")).Visible = false;

        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    public int name_validate(string m_name)
    {


        ShiftList = employee.fn_Shift1(employee.BranchId);

        if (ShiftList.Count > 0)
        {
            for (valid = 0; valid < ShiftList.Count; valid++)
            {

                if (ShiftList[valid].ShiftName == m_name)
                {
                    temp_valid++;

                }

            }

        }
        return temp_valid;
    }




    protected void Button1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            check = name_validate(ShiftName.Value);

            if (check == 0)
            {

                employee.ShiftId = Convert.ToInt32(hShiftID.Value);
                employee.ShiftName = ShiftName.Value;
                employee.ShiftFrom = ShiftFrom.Value;
                employee.ShiftTo = ShiftTo.Value;
                employee.status = 'Y';
                _connection = con.fn_Connection();

                _connection.Open();
                cmd = new SqlCommand("select count(*) from hrmm_course", _connection);
                int cc = (int)cmd.ExecuteScalar();
                cc++;
                //cmd1 = new SqlCommand("select count(*) from paym_shift", _connection);
                //int cc = (int)cmd1.ExecuteScalar();
                //cmd = new SqlCommand("insert into paym_shift values('" + employee.CompanyId + "', '" + employee.ShiftName + "', '"+employee.ShiftFrom+"','"+employee.ShiftTo+"','" + employee.status + "', '" + employee.BranchId + "')", _connection);
                //cmd.ExecuteNonQuery();
                //cmd1 = new SqlCommand("select count(*) from paym_shift", _connection);
                //int aa = (int)cmd1.ExecuteScalar();
                
                _connection.Close();


                _Value = employee.ShiftUpdate(employee);
                if (_Value != "1")
                {
                    Error.Text = "<font color=Blue>Added Successfully</font>";
                    _connection.Open();
                    cmd1 = new SqlCommand("update paym_shift set v_ShiftCategory = '"+DropDownList1.SelectedItem.Text+"' , BranchID = '" + employee.BranchId + "' where v_ShiftName='" + ShiftName.Value + "' and BranchID is null ", _connection);
                    cmd1.ExecuteNonQuery();
                    _connection.Close();
                    ShiftName.Value = "";
                    ShiftFrom.Value = "";
                    ShiftTo.Value = "";
                }
                else
                {
                    Error.Text = "<font color=Red>Error Occured</font>";

                }

                ShiftList = employee.fn_Shift1(employee.BranchId);

                if (ShiftList.Count > 0)
                {
                    grid_Shift.DataSource = ShiftList;
                    grid_Shift.DataBind();
                }


            }
            else
            {

                ClientScriptManager manager = Page.ClientScript;
                manager.RegisterStartupScript(this.GetType(), "Call", "show_message();", true);


            }

        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";


        }
    }
    protected void grid_Shift_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            try
            {
                //finding row index
                GridViewRow gvrow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);//catching the row in which thhe link button is clicked.
                HtmlInputText lnkbtn = (HtmlInputText)gvrow.FindControl("txtgrid");
                string str = lnkbtn.Value;

                mycon.Open();
                SqlCommand cmd = new SqlCommand("delete from paym_shift where v_ShiftName='" + str + "' and Branchid='" + employee.BranchId + "'", mycon);
                cmd.ExecuteNonQuery();
                load1();
                lbl_Error.Text = "Department Deleted Successfully";
            }
            catch (Exception exc)
            {
                lbl_Error.Text = "Couldnt delete because the shift is assigned to employee";
                lbl_Error.Visible = true;
            }
        }
        mycon.Close();
    }
}

