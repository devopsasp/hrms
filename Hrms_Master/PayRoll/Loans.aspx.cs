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



public partial class Bank_Loan_Default : System.Web.UI.Page
{
    ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();
    string str = ConfigurationManager.ConnectionStrings["Connectionstring"].ConnectionString;
    Company company = new Company();
    Employee employee = new Employee();
    PayRoll pay = new PayRoll();

    Collection<PayRoll> loanlist;
    
    DataSet ds_userrights;

    int company_Id, branch_Id, valid, temp_valid = 0, check;
    string _Value, s_form = "";
    string s_login_role;
    static string cname = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;

        if (!IsPostBack)
        {

            switch (s_login_role)
            {
                case "a": grid_load();
                    break;

                case "h": grid_load();
                    break;

                case "u": 
                    s_form = "34";
                    ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                    if (ds_userrights.Tables[0].Rows.Count > 0)
                    {
                        grid_load();

                    }
                    else
                    {
                        Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator.";
                        Response.Redirect("~/Company_Home.aspx");
                    }

                    break;
                default: 
                    Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                    Response.Redirect("~/Company_Home.aspx");
                    break;
            }

            //grid_load();
        }      
    }

    //protected void btn_save_Click(object sender, EventArgs e)
    //{
    //    try
    //    {

    //        check = name_validate(txt_code.Value);

    //         if (check == 0)
    //         {

    //    pay.loanid = 0;       
    //    pay.loanname = txt_name.Value;
    //    pay.loancode = txt_code.Value;
    //    pay.status = 'Y';
    //    pay.Loan_update(pay);

    //    grid_load();
    //    lbl_Error.Text = "<font color=Blue>Added Successfully</font>";
    //    txt_code.Value = "";
    //    txt_name.Value = "";

    //           }
    //         else
    //         {
    //             ClientScriptManager manager = Page.ClientScript;
    //             manager.RegisterStartupScript(this.GetType(), "Call", "show_message();", true);             

    //         }



    //    }
    //    catch (Exception ex)
    //    {
    //        lbl_Error.Text = "<font color=Red>Error Occured</font>";


    //    }


    //}

    public void grid_load()
    {

        loanlist = pay.fn_loan(pay);

        if (loanlist.Count > 0)
        {

            GV.DataSource = loanlist;
            GV.DataBind();

        }
        else
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Data');", true);
        }

    }


    protected void Edit(object sender, GridViewEditEventArgs e)
    {

        try
        {

            pay.loanid = Convert.ToInt32(GV.DataKeys[e.NewEditIndex].Value);
            pay.loanname = ((HtmlInputText)GV.Rows[e.NewEditIndex].FindControl("grd_txtname")).Value;
            pay.loancode = ((HtmlInputText)GV.Rows[e.NewEditIndex].FindControl("grd_txtcode")).Value;
            pay.status = 'Y';

            if (pay.loanname != "" && pay.loancode != "")
            {
                if (pay.loanname == cname)
                {
                    ((LinkButton)GV.Rows[e.NewEditIndex].FindControl("img_update")).Visible = true;
                    ((LinkButton)GV.Rows[e.NewEditIndex].FindControl("img_save")).Visible = false;
                    ((HtmlInputText)GV.Rows[e.NewEditIndex].FindControl("grd_txtcode")).Disabled = true;
                    ((HtmlInputText)GV.Rows[e.NewEditIndex].FindControl("grd_txtname")).Disabled = true;
                    ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Updated Successfully');", true);
                    cname = "";
                    return;
                }
                // check = 0;
                check = name_validate(pay.loanname);
                if (check == 0)
                {


                    pay.Loan_update(pay);
                    //pay.bankupdate(pay);
                    ((LinkButton)GV.Rows[e.NewEditIndex].FindControl("img_update")).Visible = true;
                    ((LinkButton)GV.Rows[e.NewEditIndex].FindControl("img_save")).Visible = false;
                    ((HtmlInputText)GV.Rows[e.NewEditIndex].FindControl("grd_txtname")).Disabled = true;
                    ((HtmlInputText)GV.Rows[e.NewEditIndex].FindControl("grd_txtcode")).Disabled = true;


                    grid_load();
                    
                    txt_code.Value = "";
                    txt_name.Value = "";
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Updated successfully');", true);
                    ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Updated Successfully');", true);
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
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
            ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }

    }

    protected void Update(object sender, GridViewUpdateEventArgs e)
    {
        ((HtmlInputText)GV.Rows[e.RowIndex].FindControl("grd_txtcode")).Disabled = false;
        ((HtmlInputText)GV.Rows[e.RowIndex].FindControl("grd_txtname")).Disabled = false;
        ((LinkButton)GV.Rows[e.RowIndex].FindControl("img_save")).Visible = true;
        ((LinkButton)GV.Rows[e.RowIndex].FindControl("img_update")).Visible = false;
        cname = ((HtmlInputText)GV.Rows[e.RowIndex].FindControl("grd_txtname")).Value;
    }

    protected void Delete(object sender, GridViewDeleteEventArgs e)
    {
        pay.loanid = Convert.ToInt32(GV.DataKeys[e.RowIndex].Value);
        
        using (SqlConnection con = new SqlConnection(str))
        {
            try
            {
                con.Open();
                string query = "delete from paym_loan where pn_CompanyID='" + pay.CompanyId + "' and pn_BranchID='" + pay.BranchId + "' and pn_LoanID='" + pay.loanid + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Record Deleted Succesfully');", true);
                ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Deleted Successfully');", true);
                grid_load();

            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Cannot Delete. Transaction Exists');", true);
            }
            finally
            {
                con.Close();
            }
        }
    }


    public int name_validate(string m_name)
    {
        loanlist = pay.fn_loan(pay);
        if (loanlist.Count > 0)
        {
            for (valid = 0; valid < loanlist.Count; valid++)
            {
                if (loanlist[valid].loancode.ToUpper() == m_name.ToUpper())
                {
                    temp_valid++;
                }
            }
        }
        return temp_valid;
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            check = name_validate(txt_code.Value);

            if (check == 0)
            {
                pay.loanid = 0;
                pay.loanname = txt_name.Value;
                pay.loancode = txt_code.Value;
                pay.status = 'Y';
                _Value = pay.Loan_update(pay);
                if (_Value == "0")
                {
                    grid_load();
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Saved Successfully');", true);
                    ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Saved Successfully');", true);
                    txt_code.Value = "";
                    txt_name.Value = "";
                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
                    ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Error Occured');", true);
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
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
            ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Error Occured');", true);
        }
    }
}
