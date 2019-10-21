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
using ePayHrms.Leave;

public partial class Hrms_Master_Default : System.Web.UI.Page
{

    ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();

    Company company = new Company();

    Employee employee = new Employee();

    Leave l = new Leave();

    Collection<Leave> IncrementList;
    Collection<Company> CompanyList;

    int company_Id, branch_Id, valid, temp_valid = 0, check;
    string _Value;
    string s_login_role;
    bool b_check = true;
    string s_form = "";
    DataSet ds_userrights;


    protected void Page_Load(object sender, EventArgs e)
    {
        
        l.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;

        if (!IsPostBack)
        {

            switch (s_login_role)
            {
                case "a": load();
                    break;

                case "h": load();
                    break;

                case "u": s_form = "25";
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
        IncrementList = l.fn_Increment();

        if (IncrementList.Count > 0)
        {
            grid_Increment.DataSource = IncrementList;
            grid_Increment.DataBind();
        }
    }

    protected void Button1_Click1(object sender, EventArgs e)

    {
        try
        {

            //if (data_check() == true)
            //{

                l.IncrementID = 0;
                l.startpoint = Convert.ToInt32(txtFromPoint.Value);
                l.lastpoint = Convert.ToInt32(txtToPoint.Value);
                l.increment = Convert.ToInt32(txtPointsAmount.Value);

                _Value = l.Increment(l);
                if (_Value != "1")
                {
                    Error.Text = "<font color=Blue>Added Successfully</font>";
                    txtFromPoint.Value = "";
                    txtToPoint.Value = "";
                    txtPointsAmount.Value = "";

                }
                else
                {
                    Error.Text = "<font color=Red>Error Occured</font>";
                }
                IncrementList = l.fn_Increment();

                if (IncrementList.Count > 0)
                {
                    grid_Increment.DataSource = IncrementList;
                    grid_Increment.DataBind();
                }


            //}
            //else
            //{
            //    lbl_Error.Text = "Invalid Values";

            //}
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";

        }
    }


    //public bool data_check()
    //{
    //    b_check = true;

    //    if (Convert.ToInt32(txtToPoint.Value) >= Convert.ToInt32(txtFromPoint.Value))
    //    {



    //    }
    //    else
    //    {
    //        b_check = false;

    //    }

    //    return b_check;


    //    for (i = 0; i < grid_appraisal.Rows.Count; i++)
    //    {
    //        if (Convert.ToInt32(((TextBox)grid_appraisal.Rows[i].FindControl("txtpoints")).Text) > Convert.ToInt32(((TextBox)grid_appraisal.Rows[i].FindControl("txttotpts")).Text))
    //        {

    //            b_check = false;

    //            break;
    //        }

    //    }

      


    //}

    protected void Edit(object sender, GridViewEditEventArgs e)
    {
        try
        {

            l.IncrementID = Convert.ToInt32(grid_Increment.DataKeys[e.NewEditIndex].Value);


            if (((HtmlInputText)grid_Increment.Rows[e.NewEditIndex].FindControl("txtgridfrompoint")).Value != "")
            {
                if (((HtmlInputText)grid_Increment.Rows[e.NewEditIndex].FindControl("txtgridtopoint")).Value != "")
                {
                    if (((HtmlInputText)grid_Increment.Rows[e.NewEditIndex].FindControl("txtgridamount")).Value != "")
                    {
                        l.startpoint = Convert.ToInt32(((HtmlInputText)grid_Increment.Rows[e.NewEditIndex].FindControl("txtgridfrompoint")).Value);
                        l.lastpoint = Convert.ToInt32(((HtmlInputText)grid_Increment.Rows[e.NewEditIndex].FindControl("txtgridtopoint")).Value);
                        l.increment = Convert.ToInt32(((HtmlInputText)grid_Increment.Rows[e.NewEditIndex].FindControl("txtgridamount")).Value);

                        _Value = l.fn_Update_increment(l);
                        if (_Value != "1")
                        {
                            ((ImageButton)grid_Increment.Rows[e.NewEditIndex].FindControl("img_update")).Visible = true;
                            ((ImageButton)grid_Increment.Rows[e.NewEditIndex].FindControl("img_save")).Visible = false;
                            ((HtmlInputText)grid_Increment.Rows[e.NewEditIndex].FindControl("txtgridfrompoint")).Disabled = true;
                            ((HtmlInputText)grid_Increment.Rows[e.NewEditIndex].FindControl("txtgridtopoint")).Disabled = true;
                            ((HtmlInputText)grid_Increment.Rows[e.NewEditIndex].FindControl("txtgridamount")).Disabled = true;
                        }
                        else
                        {
                            lbl_Error.Text = "Data not Saved Successfully";
                        }

                    }
                    else
                    {
                        ClientScriptManager manager = Page.ClientScript;
                        manager.RegisterStartupScript(this.GetType(), "Call", "show_Error3();", true);
                    }
                }
                else
                {
                    ClientScriptManager manager = Page.ClientScript;
                    manager.RegisterStartupScript(this.GetType(), "Call", "show_Error2();", true);
                }
            }
            else
            {
                ClientScriptManager manager = Page.ClientScript;
                manager.RegisterStartupScript(this.GetType(), "Call", "show_Error1();", true);
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    protected void Delete(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void Update(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            ((HtmlInputText)grid_Increment.Rows[e.RowIndex].FindControl("txtgridfrompoint")).Disabled = false;
            ((HtmlInputText)grid_Increment.Rows[e.RowIndex].FindControl("txtgridtopoint")).Disabled = false;
            ((HtmlInputText)grid_Increment.Rows[e.RowIndex].FindControl("txtgridamount")).Disabled = false;
            ((ImageButton)grid_Increment.Rows[e.RowIndex].FindControl("img_save")).Visible = true;
            ((ImageButton)grid_Increment.Rows[e.RowIndex].FindControl("img_update")).Visible = false;
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";


        }

    }



}
