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
using System.Configuration;

public partial class Hrms_Master_Default8 : System.Web.UI.Page
{
    ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();
    string str = ConfigurationManager.ConnectionStrings["Connectionstring"].ConnectionString;
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    //private SqlConnection _con;
    //ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlCommand cmd = new SqlCommand();
    Company company = new Company();
    Employee employee = new Employee();
    PayRoll pay = new PayRoll();

    Collection<Company> BranchsList;
    Collection<PayRoll> DeductionList;
    Collection<Company> CompanyList;

    int company_Id, branch_Id, valid, temp_valid = 0, check, check1;
    public static int orderID = 0;
    string _Value;
    string s_login_role;
    string s_form = "";
    DataSet ds_userrights;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        // pay.EmployeeId =Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        if (!IsPostBack)
        {
            switch (s_login_role)
            {
                case "a": load();
                    break;

                case "h":
                    grid_Branch.Visible = false;
                    assign.Visible = false;
                    load1();
                    break;

                case "u": s_form = "19";
                    ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                    if (ds_userrights.Tables[0].Rows.Count > 0)
                    {
                        load1();
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

        DeductionList = pay.fn_Deduction1(employee.BranchId);

        if (DeductionList.Count > 0)
        {
            grid_Deduction.DataSource = DeductionList;
            grid_Deduction.DataBind();

            grd_rdo_chk();
        }

        BranchsList = company.fn_getBranchs();

        if (BranchsList.Count > 0)    //first branch is company
        {
            grid_Branch.DataSource = BranchsList;
            grid_Branch.DataBind();

        }
    }
    public void load1()
    {
        DeductionList = pay.fn_Deduction1(employee.BranchId);
        if (DeductionList.Count > 0)
        {
            grid_Deduction.DataSource = DeductionList;
            grid_Deduction.DataBind();

            grd_rdo_chk();
            for (int c = 0; c < DeductionList.Count; c++)
            {
                grid_Deduction.Rows[c].FindControl("Chk_Deduction").Visible = false;
            }
        }
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        try
        {
            check = name_validate(txtDeductionCode.Value);

            if (check == 0)
            {
                check = name_validate1(txtDeductionName.Value);

                if (check == 0)
                {
                    //pay.DeductionId = Convert.ToInt32(hDeductionID.Value);
                    pay.DeducationCode = txtDeductionCode.Value;
                    pay.DeductionName = txtDeductionName.Value;

                    if (chk_Deducations.Checked)
                    {
                        pay.regular = 'Y';
                    }
                    else
                    {
                        pay.regular = 'N';
                    }

                    pay.status = 'Y';

                    _Value = pay.DeductionUpdate(pay);



                    if (_Value != "1")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Saved Successfully');};", true);
                        txtDeductionName.Value = "";
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Error Occured');};", true);
                    }
                    DeductionList = pay.fn_Deduction(pay);

                    if (DeductionList.Count > 0)
                    {
                        grid_Deduction.DataSource = DeductionList;
                        grid_Deduction.DataBind();
                        grd_rdo_chk();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Deduction Name already exists.');};", true);
                }
            }
            else
            {

                ClientScriptManager manager = Page.ClientScript;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Deduction Code already exists.');};", true);

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Error Occured.');};", true);
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
    }

    protected void Update(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            ((HtmlInputText)grid_Deduction.Rows[e.RowIndex].FindControl("grd_DName")).Disabled = false;

            //grd_Dcode
            ((CheckBox)grid_Deduction.Rows[e.RowIndex].FindControl("chk_Ded")).Enabled = true;
            ((LinkButton)grid_Deduction.Rows[e.RowIndex].FindControl("img_save")).Visible = true;
            ((LinkButton)grid_Deduction.Rows[e.RowIndex].FindControl("img_update")).Visible = false;
            ((TextBox)grid_Deduction.Rows[e.RowIndex].FindControl("txt_order")).Enabled = true;
            orderID = Convert.ToInt32(((TextBox)grid_Deduction.Rows[e.RowIndex].FindControl("txt_order")).Text);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Error Occured');};", true);
        }
    }

    protected void Edit(object sender, GridViewEditEventArgs e)
    {
        try
        {
            //grd_Dcode,grd_DName,chk_Ded
            pay.DeductionId = Convert.ToInt32(grid_Deduction.DataKeys[e.NewEditIndex].Value);
            pay.DeducationCode = ((TextBox)grid_Deduction.Rows[e.NewEditIndex].FindControl("grd_Dcode")).Text;
            pay.DeductionName = ((HtmlInputText)grid_Deduction.Rows[e.NewEditIndex].FindControl("grd_DName")).Value;
            pay.d_order =Convert.ToInt32(((TextBox)grid_Deduction.Rows[e.NewEditIndex].FindControl("txt_order")).Text);

            //pay.DeductionName = ((TextBox)grid_Deduction.Rows[e.NewEditIndex].FindControl("grd_Dcode")).Text;
            //pay.status = 'Y';
            if (orderID != pay.d_order)
            check1 = Convert.ToInt32(order_validate(pay.d_order));
            if (check1 != 0 || pay.d_order == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Order Number already assigned / invalid.');};", true);
                return;
            }
            if (((CheckBox)grid_Deduction.Rows[e.NewEditIndex].FindControl("chk_Ded")).Checked)
            {
                pay.regular = 'Y';
            }
            else
            {
                pay.regular = 'N';
            }

            if (pay.DeductionName != "")
            {
                check = 0;
                //check = name_validate1(pay.DeductionName);

                if (check == 0)
                {
                    pay.fn_Update_Deduction(pay);

                    ((LinkButton)grid_Deduction.Rows[e.NewEditIndex].FindControl("img_update")).Visible = true;
                    ((LinkButton)grid_Deduction.Rows[e.NewEditIndex].FindControl("img_save")).Visible = false;
                    ((HtmlInputText)grid_Deduction.Rows[e.NewEditIndex].FindControl("grd_DName")).Disabled = true;
                    ((CheckBox)grid_Deduction.Rows[e.NewEditIndex].FindControl("chk_Ded")).Enabled = false;
                    ((TextBox)grid_Deduction.Rows[e.NewEditIndex].FindControl("txt_order")).Enabled = false;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Deduction Name already exists.');};", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Deduction Code already exists.');};", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Error Occured.');};", true);
        }
    }

    public void grd_rdo_chk()
    {

        for (int grk = 0; grk < grid_Deduction.Rows.Count; grk++)
        {
            if (DeductionList[grk].regular == 'Y')
            {
                ((CheckBox)grid_Deduction.Rows[grk].FindControl("chk_Ded")).Checked = true;

            }
            else
            {
                ((CheckBox)grid_Deduction.Rows[grk].FindControl("chk_Ded")).Checked = false;

            }
        }
    }

    public void BindDeduction()
    {

        DeductionList = pay.fn_Deduction(pay);

        if (DeductionList.Count > 0)
        {
            grid_Deduction.DataSource = DeductionList;
            grid_Deduction.DataBind();
        }
    }

    public int name_validate(string m_name)
    {
        DeductionList = pay.fn_Deduction1(employee.BranchId);

        if (DeductionList.Count > 0)
        {
            for (valid = 0; valid < DeductionList.Count; valid++)
            {
                if (DeductionList[valid].DeducationCode.ToLower() == m_name.ToLower())
                {
                    temp_valid++;
                }
            }
        }
        return temp_valid;
    }

    public int name_validate1(string d_name)
    {

        DeductionList = pay.fn_Deduction1(employee.BranchId);

        if (DeductionList.Count > 0)
        {
            for (valid = 0; valid < DeductionList.Count; valid++)
            {

                if (DeductionList[valid].DeductionName.ToLower() == d_name.ToLower())
                {
                    temp_valid++;
                }
            }
        }
        return temp_valid;
    }

    protected void assign_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            for (int j = 0; j < grid_Deduction.Rows.Count; j++)
            {

                GridViewRow Deduction_row = grid_Deduction.Rows[j];


                bool Deduction_check = ((HtmlInputCheckBox)Deduction_row.FindControl("Chk_Deduction")).Checked;

                if (Deduction_check)
                {
                    for (int i = 0; i < grid_Branch.Rows.Count; i++)
                    {
                        GridViewRow Branch_row = grid_Branch.Rows[i];

                        bool Branch_check = ((HtmlInputCheckBox)Branch_row.FindControl("Chk_Branch")).Checked;


                        if (Branch_check)
                        {
                            //pay.CompanyId = company_Id;
                            pay.DeductionId = Convert.ToInt32(grid_Deduction.DataKeys[Deduction_row.RowIndex].Value);
                            pay.BranchId = Convert.ToInt32(grid_Branch.DataKeys[Branch_row.RowIndex].Value);

                            _Value = pay.Deduction(pay);

                            if (_Value != "1")
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Saved Successfully.');};", true);
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Error Occured.);};", true);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Error Occured.');};", true);
        }
    }
    public int order_validate(int id)
    {
        DeductionList = pay.fn_Deduction1(employee.BranchId);
        if (DeductionList.Count > 0)
        {
            for (valid = 0; valid < DeductionList.Count; valid++)
            {
                if (DeductionList[valid].d_order == id)
                {
                    temp_valid++;
                }
            }
        }
        return temp_valid;
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (txtDeductionCode.Value == "" || txtDeductionName.Value == "" || txt_d_order.Value=="")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Enter all the Fields');};", true);
            return;
        }
        try
        {
            check = name_validate(txtDeductionCode.Value);
            check1 = Convert.ToInt32(order_validate(Convert.ToInt32(txt_d_order.Value)));
            if (check == 0)
            {
                if (check1 == 0)
                {
                    check = name_validate1(txtDeductionName.Value);

                    if (check == 0)
                    {
                        //pay.DeductionId = Convert.ToInt32(hDeductionID.Value);
                        pay.DeducationCode = txtDeductionCode.Value;
                        pay.DeductionName = txtDeductionName.Value;
                        pay.d_order = Convert.ToInt32(txt_d_order.Value);
                        if (chk_Deducations.Checked)
                        {
                            pay.regular = 'Y';
                        }
                        else
                        {
                            pay.regular = 'N';
                        }

                        pay.status = 'Y';

                        _Value = pay.DeductionUpdate(pay);

                        if (_Value != "1")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Saved Successfully');};", true);
                            txtDeductionName.Value = "";
                            txtDeductionCode.Value = "";
                            txt_d_order.Value = "";
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Error Occured');};", true);

                        }
                        DeductionList = pay.fn_Deduction1(employee.BranchId);

                        if (DeductionList.Count > 0)
                        {
                            grid_Deduction.DataSource = DeductionList;
                            grid_Deduction.DataBind();
                            grd_rdo_chk();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Deduction Name already exists.');};", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Order Value already exists');};", true);
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Order Value already exists.');", true);
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Order Value already exists.;};", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Deduction Code already exists.');};", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Error Occured');};", true);
        }        
        
    }
    protected void Delete(object sender, GridViewDeleteEventArgs e)
    {
        pay.DeductionId = Convert.ToInt32(grid_Deduction.DataKeys[e.RowIndex].Value);
        pay.DeducationCode = ((TextBox)grid_Deduction.Rows[e.RowIndex].FindControl("grd_Dcode")).Text;
        pay.DeductionName = ((HtmlInputText)grid_Deduction.Rows[e.RowIndex].FindControl("grd_DName")).Value;
        using (SqlConnection con = new SqlConnection(str))
        {
            try
            {
                con.Open();
                string query = "delete from paym_Deduction where pn_CompanyID='" + pay.CompanyId + "' and pn_BranchID='" + pay.BranchId + "' and pn_DeductionID='" + pay.DeductionId + "' and v_DeductionName='" + pay.DeductionName + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Record Deleted Successfully');};", true);
                load1();
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.onload=function(){alert('Deletion Error : Transaction exists.');};", true);
            }
        }
    }
}
