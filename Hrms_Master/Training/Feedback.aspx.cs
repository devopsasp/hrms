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

public partial class Hrms_Master_Training_Feedback : System.Web.UI.Page
{
    private SqlConnection _connection;
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlConnection mycon = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    SqlDataReader rea;
    Company company = new Company();

    Employee employee = new Employee();

    Collection<Company> BranchsList;
    Collection<Employee> FeedbackList;
    Collection<Company> CompanyList;
    int company_Id, branch_Id, valid, temp_valid = 0, check = 0;
    string _Value;
    string s_login_role;
    string s_form = "";
    DataSet ds_userrights;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        //Error.Text = "";


        if (!IsPostBack)
        {

            switch (s_login_role)
            {
                case "a":
                    //load_admin();
                    break;

                case "h":
                    ddl_branch.Visible = false;
                    load1();
                    //access();
                    break;

                case "u": s_form = "3";
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
    public void load1()
    {
        FeedbackList = employee.fn_feedback(employee.BranchId);

        if (FeedbackList.Count > 0)
        {
           grid_feedback.DataSource = FeedbackList;
           grid_feedback.DataBind();
        }
        else
        {
            FeedbackList = employee.fn_EmptycourseList(employee);

            if (FeedbackList.Count > 0)
            {
                grid_feedback.DataSource = FeedbackList;
                grid_feedback.DataBind();
                ((ImageButton)grid_feedback.Rows[0].FindControl("img_update")).Visible = true;
                ((ImageButton)grid_feedback.Rows[0].FindControl("imgdel")).Visible = true;
            }

        }

    }
    protected void Edit(object sender, GridViewEditEventArgs e)
    {
        try
        {

            employee.FeedbackID = Convert.ToInt32(grid_feedback.DataKeys[e.NewEditIndex].Value);
            employee.FeedbackQues = ((HtmlInputText)grid_feedback.Rows[e.NewEditIndex].FindControl("txtgrid")).Value;

            if (employee.FeedbackQues != "")
            {
                check = 0;

                if (check == 0)
                {

                    _Value = employee.fn_Update_feedback(employee);
                    if (_Value == "0")
                    {
                        ((ImageButton)grid_feedback.Rows[e.NewEditIndex].FindControl("img_update")).Visible = true;
                        ((ImageButton)grid_feedback.Rows[e.NewEditIndex].FindControl("img_save")).Visible = false;
                        ((HtmlInputText)grid_feedback.Rows[e.NewEditIndex].FindControl("txtgrid")).Disabled = true;
                        //Error.Text = "<font color=Blue>Updated Successfully</font>";
                    }
                    else
                    {
                       // Error.Text = "<font color=Red>Error Occured</font>";
                    }

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
    protected void Update(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            HtmlInputText textgrid = ((HtmlInputText)grid_feedback.Rows[e.RowIndex].FindControl("txtgrid"));
            ((ImageButton)grid_feedback.Rows[e.RowIndex].FindControl("img_save")).Visible = true;
            ((ImageButton)grid_feedback.Rows[e.RowIndex].FindControl("img_update")).Visible = false;
            textgrid.Disabled = false;
            textgrid.Attributes.Add("style", "font-color:blue");
            textgrid.Attributes.Add("style", "width:500px");//textbox width

        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }

    }


    protected void grid_feedback_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            try
            {
                GridViewRow gvrow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                int rowindex = gvrow.RowIndex;
                HtmlInputText lnkbtn = (HtmlInputText)gvrow.FindControl("txtgrid");
                string str = lnkbtn.Value;
                mycon.Open();
                SqlCommand cmd = new SqlCommand("delete from training_feedback where v_feedback_ques='" + str + "' and branchid='" + employee.BranchId + "'", mycon);
                cmd.ExecuteNonQuery();
                load1();
                lbl_Error.Text = "";


            }
            catch (Exception exc)
            {
                
            }
        }
        mycon.Close();     
    }
    protected void grid_feedback_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
protected void btn_save_Click1(object sender, EventArgs e)
{
    try
        {
            employee.FeedbackID = Convert.ToInt32(hFeedbackID.Value);
            employee.FeedbackQues = txt_ques.Text;
            _connection = con.fn_Connection();
            _connection.Open();
            cmd1 = new SqlCommand("select count(*) from training_feedback", _connection);
            int cc = (int)cmd1.ExecuteScalar();
            cmd = new SqlCommand("insert into training_feedback values('" + employee.CompanyId + "', '" + employee.BranchId + "', '" + employee.FeedbackQues + "')", _connection);
            cmd.ExecuteNonQuery();
            cmd1 = new SqlCommand("select count(*) from training_feedback", _connection);
            int aa = (int)cmd1.ExecuteScalar();
            _connection.Close();
            if (cc != aa)
            {
                //Error.Text = "<font color=Blue>Added Successfully</font>";
                txt_ques.Text = "";
            }
            else
            {
               // Error.Text = "<font color=Red>Error Occured</font>";
            }

            FeedbackList = employee.fn_feedback(employee.BranchId);

            if (FeedbackList.Count > 0)
            {
                grid_feedback.DataSource = FeedbackList;
                grid_feedback.DataBind();
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }
}

