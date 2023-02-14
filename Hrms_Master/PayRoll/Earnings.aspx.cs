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
using System.Drawing;

public partial class Hrms_Master_Default7 : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["Connectionstring"].ConnectionString;
    ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    //private SqlConnection _con;
    //ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlCommand cmd = new SqlCommand();

    Company company = new Company();
    Employee employee = new Employee();
    PayRoll pay = new PayRoll();

    Collection<Company> BranchsList;
    Collection<PayRoll> EarningsList;
    Collection<Company> CompanyList;

    int company_Id, branch_Id, ddl_ex, valid, temp_valid = 0, check, check1;
    public static int orderID = 0;

    string _Value;
    string s_login_role;
    string s_form = "";
    DataSet ds_userrights;

    public object rdo_earn { get; private set; }
    public Color BackColor { get; private set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        if (!IsPostBack)
        {
            switch (s_login_role)
            {
                case "a": load1();
                    break;

                case "h":
                    grid_Branch.Visible = false;
                    Button2.Visible = false;
                    load();
                    break;

                case "u": s_form = "18";
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
        EarningsList = pay.fn_Earnings1(employee.BranchId);

        if (EarningsList.Count > 0)
        {
            grid_Earnings.DataSource = EarningsList;
            grid_Earnings.DataBind();
            grd_rdo_chk();
            grd_rdo_chk1();
            for (int c = 0; c < EarningsList.Count; c++)
            {
                grid_Earnings.Rows[c].FindControl("Chk_Earnings").Visible = false;
            }
        }
    }

    protected void checkboxAttendanceStatus_CheckedChanged(object sender, EventArgs e)
    {
     
    }
    public void load1()
    {
        EarningsList = pay.fn_Earnings(pay);

        if (EarningsList.Count > 0)
        {
            
            grid_Earnings.DataSource = EarningsList;
            grid_Earnings.DataBind();
            grd_rdo_chk();
            grd_rdo_chk1();
        }

        BranchsList = company.fn_getBranchs();

        if (BranchsList.Count > 0)    //first branch is company
        {
            grid_Branch.DataSource = BranchsList;
            grid_Branch.DataBind();
            grid_Branch.Visible = false;
        }

    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        try
        {
            check = name_validate(EarningsCode.Value);
            //check = Convert.ToInt32(order_validate(Convert.ToInt32(txt_d_order.Value)));
            check1 = Convert.ToInt32(order_validate(Convert.ToInt32(txt_d_order.Value)));

            if (check == 0)
            {
                if (check1 == 0)
                {
                    check = name_validate1(EarningsName.Value);
                    if (check == 0)
                    {

                        pay.EarningsId = Convert.ToInt32(hEarningsID.Value);
                        pay.EarningsName = EarningsName.Value;
                        pay.EarningsCode = EarningsCode.Value;

                        //if (chk_earnings.Checked)
                        //{
                        //    pay.regular = 'Y';
                        //}
                        //else
                        //{
                        //    pay.regular = 'N';
                        //}

                        pay.regular = Convert.ToChar(ddl_earntype.SelectedItem.Value);



                        switch (Convert.ToInt32(rdo_Earnings.SelectedItem.Value))
                        {
                            case 0: pay.Pf = 'Y';
                                pay.Esi = 'N';

                                break;

                            case 1: pay.Pf = 'N';
                                pay.Esi = 'Y';
                                break;

                            case 2: pay.Pf = 'Y';
                                pay.Esi = 'Y';
                                break;

                            case 3: pay.Pf = 'N';
                                pay.Esi = 'N';
                                break;

                        }
                        pay.OT = 'N';
                        pay.LOP = 'N';
                        pay.PT = 'N';
                        pay.Print = 'N';

                        pay.payslip = 'N';

                        for (int i = 0; i < check_earn.Items.Count - 1; i++)
                        {
                            if (check_earn.Items[i].Selected == true)
                            {
                                if (i == 0)
                                {
                                    pay.OT = 'Y';

                                }

                                if (i == 1)
                                {
                                    pay.LOP = 'Y';

                                }
                                if (i == 2)
                                {
                                    pay.PT = 'Y';

                                }
                                if (i == 3)
                                {
                                    pay.Print = 'Y';
                                }
                                if (i == 4)
                                {
                                    pay.payslip = 'Y';
                                }
                            }
                        }
                        pay.status = 'Y';
                        pay.d_order = Convert.ToInt32(txt_d_order.Value);
                        _Value = pay.EarningsUpdate(pay);

                        if (_Value != "1")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Saved Successfully!');", true);
                            myConnection.Open();
                            cmd = new SqlCommand("update paym_earnings set pn_BranchID = '" + employee.BranchId + "' where v_EarningsName = '" + EarningsName.Value + "'", myConnection);
                            cmd.ExecuteNonQuery();
                            myConnection.Close();
                            EarningsName.Value = "";
                            EarningsCode.Value = "";
                            //chk_earnings.Checked = false;
                            //rdo_Earnings.se
                            ddl_earntype.SelectedIndex = 0;
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
                        }

                        EarningsList = pay.fn_Earnings(pay);

                        if (EarningsList.Count > 0)
                        {
                            grid_Earnings.DataSource = EarningsList;
                            grid_Earnings.DataBind();

                            grd_rdo_chk();
                            grd_rdo_chk1();
                        }
                    }
                    else
                    {
                        ClientScriptManager manager = Page.ClientScript;
                        manager.RegisterStartupScript(this.GetType(), "Call", "show_message2();", true);
                    }
                }
                else
                {
                    ClientScriptManager manager = Page.ClientScript;
                    manager.RegisterStartupScript(this.GetType(), "Call", "show_message1();", true);
                }
            }
            else
            {
                ClientScriptManager manager1 = Page.ClientScript;
                manager1.RegisterStartupScript(this.GetType(), "Call", "show_message();", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {

        try
        {
            //DepartmentGrid
            //grid_Branch

            for (int j = 0; j < grid_Earnings.Rows.Count; j++)
            {

                GridViewRow Earnings_row = grid_Earnings.Rows[j];


                bool Earnings_check = ((HtmlInputCheckBox)Earnings_row.FindControl("Chk_Earnings")).Checked;

                if (Earnings_check)
                {


                    for (int i = 0; i < grid_Branch.Rows.Count; i++)
                    {


                        GridViewRow Branch_row = grid_Branch.Rows[i];

                        bool Branch_check = ((HtmlInputCheckBox)Branch_row.FindControl("Chk_Branch")).Checked;


                        if (Branch_check)
                        {
                            //employee.CompanyId = company_Id;
                            pay.EarningsId = Convert.ToInt32(grid_Earnings.DataKeys[Earnings_row.RowIndex].Value);
                            pay.BranchId = Convert.ToInt32(grid_Branch.DataKeys[Branch_row.RowIndex].Value);

                            _Value = pay.Earnings(pay);

                            if (_Value != "1")
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Saved Successfully!');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
        }
    }

    protected void Edit(object sender, GridViewEditEventArgs e)
    {
        try
        {
            pay.EarningsId = Convert.ToInt32(grid_Earnings.DataKeys[e.NewEditIndex].Value);
            pay.EarningsName = ((HtmlInputText)grid_Earnings.Rows[e.NewEditIndex].FindControl("grd_EName")).Value;
            pay.EarningsCode = ((TextBox)grid_Earnings.Rows[e.NewEditIndex].FindControl("grd_ECode")).Text;
            pay.regular = Convert.ToChar(((DropDownList)grid_Earnings.Rows[e.NewEditIndex].FindControl("grd_ddl")).SelectedItem.Value);
            pay.d_order = Convert.ToInt32(((HtmlInputText)grid_Earnings.Rows[e.NewEditIndex].FindControl("txt_order")).Value);
            if(orderID != pay.d_order)
            check1 = Convert.ToInt32(order_validate(pay.d_order));
            if (check1 != 0 || pay.d_order == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Order Number already assigned / invalid');", true);
                return;
            }
            switch (Convert.ToInt32(((RadioButtonList)grid_Earnings.Rows[e.NewEditIndex].FindControl("grd_Rdo")).SelectedItem.Value))
            {
                case 0: pay.Pf = 'Y';
                    pay.Esi = 'N';
                    break;

                case 1: pay.Pf = 'N';
                    pay.Esi = 'Y';
                    break;

                case 2: pay.Pf = 'Y';
                    pay.Esi = 'Y';
                    break;

                case 3: pay.Pf = 'N';
                    pay.Esi = 'N';
                    break;

            }

            pay.LOP = 'N';
            pay.OT = 'N';
            pay.PT = 'N';
            CheckBoxList chk_eligible = ((CheckBoxList)grid_Earnings.Rows[e.NewEditIndex].FindControl("rdo_earn"));
            foreach (ListItem li in chk_eligible.Items)
            {
                if (li.Selected)
                {
                    if (li.Value == "0")
                    {
                        pay.LOP = 'Y';
                    }
                    if (li.Value == "1")
                    {
                        pay.OT = 'Y';
                    }
                    if (li.Value == "2")
                    {
                        pay.PT = 'Y';
                    }
                }
            }

            // pay.status = 'Y';

            if (pay.EarningsName != "")
            {
                //bysan to delete commanline in the below line
                check = 0;  

                if (check == 0)
                {
                    pay.fn_Update_Earnings(pay);
                    ((ImageButton)grid_Earnings.Rows[e.NewEditIndex].FindControl("img_update")).Visible = true;
                    ((LinkButton)grid_Earnings.Rows[e.NewEditIndex].FindControl("img_save")).Visible = false;
                    ((ImageButton)grid_Earnings.Rows[e.NewEditIndex].FindControl("imgdel")).Visible = true;
                    //((TextBox)grid_Earnings.Rows[e.NewEditIndex].FindControl("grd_ECode")).Enabled = false;
                    ((HtmlInputText)grid_Earnings.Rows[e.NewEditIndex].FindControl("grd_EName")).Disabled = true;
                    //((CheckBox)grid_Earnings.Rows[e.NewEditIndex].FindControl("grd_Reg")).Enabled = false;
                    ((DropDownList)grid_Earnings.Rows[e.NewEditIndex].FindControl("grd_ddl")).Enabled = false;
                    ((RadioButtonList)grid_Earnings.Rows[e.NewEditIndex].FindControl("grd_Rdo")).Enabled = false;
                    ((CheckBoxList)grid_Earnings.Rows[e.NewEditIndex].FindControl("rdo_earn")).Enabled = false;
                    ((HtmlInputText)grid_Earnings.Rows[e.NewEditIndex].FindControl("txt_order")).Disabled = true;
                }

                else
                {

                    ClientScriptManager manager = Page.ClientScript;
                    manager.RegisterStartupScript(this.GetType(), "Call", "show_message2();", true);
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
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
        }
    }

    protected void Update(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            ((HtmlInputText)grid_Earnings.Rows[e.RowIndex].FindControl("grd_EName")).Disabled = false;
            ((DropDownList)grid_Earnings.Rows[e.RowIndex].FindControl("grd_ddl")).Enabled = true;
            ((RadioButtonList)grid_Earnings.Rows[e.RowIndex].FindControl("grd_Rdo")).Enabled = true;
            ((CheckBoxList)grid_Earnings.Rows[e.RowIndex].FindControl("rdo_earn")).Enabled = true;
            ((LinkButton)grid_Earnings.Rows[e.RowIndex].FindControl("img_save")).Visible = true;
            ((ImageButton)grid_Earnings.Rows[e.RowIndex].FindControl("img_update")).Visible = false;
            ((ImageButton)grid_Earnings.Rows[e.RowIndex].FindControl("imgdel")).Visible = false;
            ((HtmlInputText)grid_Earnings.Rows[e.RowIndex].FindControl("txt_order")).Disabled = false;
            orderID = Convert.ToInt32(((HtmlInputText)grid_Earnings.Rows[e.RowIndex].FindControl("txt_order")).Value);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
        }
    }

    public void grd_rdo_chk()
    {

        for (int grk = 0; grk < grid_Earnings.Rows.Count; grk++)
        {
            for (ddl_ex = 0; ddl_ex < ((DropDownList)grid_Earnings.Rows[grk].FindControl("grd_ddl")).Items.Count; ddl_ex++)
            {
                if (((DropDownList)grid_Earnings.Rows[grk].FindControl("grd_ddl")).Items[ddl_ex].Value == EarningsList[grk].regular.ToString())
                {
                    ((DropDownList)grid_Earnings.Rows[grk].FindControl("grd_ddl")).SelectedIndex = ddl_ex;
                }

            }

            if (EarningsList[grk].Pf == 'Y')
            {
                ((RadioButtonList)grid_Earnings.Rows[grk].FindControl("grd_Rdo")).SelectedIndex = 0;
            }
            if (EarningsList[grk].Esi == 'Y')
            {
                ((RadioButtonList)grid_Earnings.Rows[grk].FindControl("grd_Rdo")).SelectedIndex = 1;
            }

            if (EarningsList[grk].Pf == 'Y' && EarningsList[grk].Esi == 'Y')
            {
                ((RadioButtonList)grid_Earnings.Rows[grk].FindControl("grd_Rdo")).SelectedIndex = 2;
            }

            if (EarningsList[grk].Pf == 'N' && EarningsList[grk].Esi == 'N')
            {
                ((RadioButtonList)grid_Earnings.Rows[grk].FindControl("grd_Rdo")).SelectedIndex = 3;
            }
            //if (EarningsList[grk].LOP == 'Y')
            //{
            //    ((CheckBoxList)grid_Earnings.Rows[grk].FindControl("rdo_earn")).SelectedIndex = 0;
            //}
            //if (EarningsList[grk].OT == 'Y')
            //{
            //    ((CheckBoxList)grid_Earnings.Rows[grk].FindControl("rdo_earn")).SelectedIndex = 1;
            //}
            //if (EarningsList[grk].PT == 'Y')
            //{
            //    ((CheckBoxList)grid_Earnings.Rows[grk].FindControl("rdo_earn")).SelectedIndex = 2;
            //}

        }


    }
    public void grd_rdo_chk1()
    {

        for (int grk = 0; grk < grid_Earnings.Rows.Count; grk++)
        {

            for (ddl_ex = 0; ddl_ex < ((DropDownList)grid_Earnings.Rows[grk].FindControl("grd_ddl")).Items.Count; ddl_ex++)
            {
                if (((DropDownList)grid_Earnings.Rows[grk].FindControl("grd_ddl")).Items[ddl_ex].Value == EarningsList[grk].regular.ToString())
                {
                    ((DropDownList)grid_Earnings.Rows[grk].FindControl("grd_ddl")).SelectedIndex = ddl_ex;
                }

            }



            //((DropDownList)grid_Earnings.Rows[grk].FindControl("grd_ddl")).SelectedIndex =Convert.ToInt32(EarningsList[grk].regular);

            //RadioButton List
            CheckBoxList chk_eligible = ((CheckBoxList)grid_Earnings.Rows[grk].FindControl("rdo_earn"));

            if (EarningsList[grk].LOP == 'Y')
            {
                ((CheckBoxList)grid_Earnings.Rows[grk].FindControl("rdo_earn")).Items[0].Selected = true;
            }
            if (EarningsList[grk].PT == 'Y')
            {
                ((CheckBoxList)grid_Earnings.Rows[grk].FindControl("rdo_earn")).Items[2].Selected = true;
            }

            if (EarningsList[grk].OT == 'Y')
            {
                ((CheckBoxList)grid_Earnings.Rows[grk].FindControl("rdo_earn")).Items[1].Selected = true;
            }

        }
    }
    protected void Delete(object sender, GridViewDeleteEventArgs e)    
    {
        pay.EarningsId = Convert.ToInt32(grid_Earnings.DataKeys[e.RowIndex].Value);
        pay.EarningsCode = ((TextBox)grid_Earnings.Rows[e.RowIndex].FindControl("grd_ECode")).Text;
        pay.EarningsName = ((HtmlInputText)grid_Earnings.Rows[e.RowIndex].FindControl("grd_EName")).Value;
        using (SqlConnection con = new SqlConnection(str))
        {
            try
            {
                con.Open();
                string query = "delete from paym_Earnings where pn_CompanyID='" + pay.CompanyId + "' and pn_BranchID='" + pay.BranchId + "' and pn_EarningsID='" + pay.EarningsId + "' and v_EarningsName='" + pay.EarningsName + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Record Deleted Succesfully');", true);
                load1();
            }
            catch
            {
                //load1();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Cannot Delete. Transaction exists.');", true);
               
            }
        }

    }
    //protected void row_bound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //    }
    //}
    public int name_validate(string m_name)
    {
        EarningsList = pay.fn_Earnings1(employee.BranchId);

        if (EarningsList.Count > 0)
        {
            for (valid = 0; valid < EarningsList.Count; valid++)
            {

                if (EarningsList[valid].EarningsCode.ToLower() == m_name.ToLower())
                {
                    temp_valid++;

                }

            }

        }
        return temp_valid;
    }

    public int name_validate1(string e_name)
    {

        EarningsList = pay.fn_Earnings1(employee.BranchId);

        if (EarningsList.Count > 0)
        {
            for (valid = 0; valid < EarningsList.Count; valid++)
            {

                if (EarningsList[valid].EarningsName.ToLower() == e_name.ToLower())
                {
                    temp_valid++;

                }

            }

        }
        return temp_valid;
    }


    public int order_validate(int id)
    {
        EarningsList = pay.fn_Earnings1(employee.BranchId);
        if (EarningsList.Count > 0)
        {
            for (valid = 0; valid < EarningsList.Count; valid++)
            {
                if (EarningsList[valid].d_order == id)
                {
                    temp_valid++;
                }
            }
        }
        return temp_valid;
    }


    protected void Button2_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //DepartmentGrid
            //grid_Branch

            for (int j = 0; j < grid_Earnings.Rows.Count; j++)
            {

                GridViewRow Earnings_row = grid_Earnings.Rows[j];


                bool Earnings_check = ((HtmlInputCheckBox)Earnings_row.FindControl("Chk_Earnings")).Checked;

                if (Earnings_check)
                {


                    for (int i = 0; i < grid_Branch.Rows.Count; i++)
                    {


                        GridViewRow Branch_row = grid_Branch.Rows[i];

                        bool Branch_check = ((HtmlInputCheckBox)Branch_row.FindControl("Chk_Branch")).Checked;


                        if (Branch_check)
                        {
                            //employee.CompanyId = company_Id;
                            pay.EarningsId = Convert.ToInt32(grid_Earnings.DataKeys[Earnings_row.RowIndex].Value);
                            pay.BranchId = Convert.ToInt32(grid_Branch.DataKeys[Branch_row.RowIndex].Value);

                            _Value = pay.Earnings(pay);

                            if (_Value != "1")
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Assigned Successfully!');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
        }
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (EarningsCode.Value == "" || EarningsName.Value == "" || txt_d_order.Value == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter all the Fields');", true);
            return;
        }
        if (txt_d_order.Value == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Invalid Order Number');", true);
            return;
        }
        try
        {
            check = name_validate(EarningsCode.Value);
            //check = Convert.ToInt32(order_validate(Convert.ToInt32(txt_d_order.Value)));
            check1 = Convert.ToInt32(order_validate(Convert.ToInt32(txt_d_order.Value)));

            if (check == 0)
            {
                if (check1 == 0)
                {
                    check = name_validate1(EarningsName.Value);
                    if (check == 0)
                    {
                        pay.EarningsId = Convert.ToInt32(hEarningsID.Value);
                        pay.EarningsName = EarningsName.Value;
                        pay.EarningsCode = EarningsCode.Value;

                        //if (chk_earnings.Checked)
                        //{
                        //    pay.regular = 'Y';
                        //}
                        //else
                        //{
                        //    pay.regular = 'N';
                        //}

                        pay.regular = Convert.ToChar(ddl_earntype.SelectedItem.Value);



                        switch (Convert.ToInt32(rdo_Earnings.SelectedItem.Value))
                        {
                            case 0: pay.Pf = 'Y';
                                pay.Esi = 'N';

                                break;

                            case 1: pay.Pf = 'N';
                                pay.Esi = 'Y';
                                break;

                            case 2: pay.Pf = 'Y';
                                pay.Esi = 'Y';
                                break;

                            case 3: pay.Pf = 'N';
                                pay.Esi = 'N';
                                break;

                        }
                        pay.OT = 'N';
                        pay.LOP = 'N';
                        pay.PT = 'N';
                        pay.Print = 'N';

                        pay.payslip = 'N';

                        for (int i = 0; i < check_earn.Items.Count - 1; i++)
                        {
                            if (check_earn.Items[i].Selected == true)
                            {
                                if (i == 0)
                                {
                                    pay.OT = 'Y';

                                }

                                if (i == 1)
                                {
                                    pay.LOP = 'Y';

                                }
                                if (i == 2)
                                {
                                    pay.PT = 'Y';

                                }
                                if (i == 3)
                                {
                                    pay.Print = 'Y';
                                }
                                if (i == 4)
                                {
                                    pay.payslip = 'Y';
                                }
                            }
                        }
                        pay.status = 'Y';
                        pay.d_order = Convert.ToInt32(txt_d_order.Value);
                        _Value = pay.EarningsUpdate(pay);

                        if (_Value != "1")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Saved Successfully!');", true);
                            myConnection.Open();
                            cmd = new SqlCommand("update paym_earnings set pn_BranchID = '" + employee.BranchId + "' where v_EarningsName = '" + EarningsName.Value + "' and pn_branchID is null", myConnection);
                            cmd.ExecuteNonQuery();
                            myConnection.Close();
                            EarningsName.Value = "";
                            EarningsCode.Value = "";
                            txt_d_order.Value = "";
                            //chk_earnings.Checked = false;
                            //rdo_Earnings.se
                            ddl_earntype.SelectedIndex = 0;
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
                        }
                        EarningsList = pay.fn_Earnings1(employee.BranchId);

                        if (EarningsList.Count > 0)
                        {
                            grid_Earnings.DataSource = EarningsList;
                            grid_Earnings.DataBind();

                            grd_rdo_chk();
                            grd_rdo_chk1();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Earnings Name Already Exist!');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Order Value Already Exist!');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Earnings Code Already Exist!');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
        }
    }
}

