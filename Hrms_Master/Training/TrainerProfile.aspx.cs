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

public partial class Hrms_Training_Default : System.Web.UI.Page
{
    private SqlConnection _connection;
    ePayHrms.Connection.Connection con1 = new ePayHrms.Connection.Connection();
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]); 
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    SqlDataReader rea;
    Company company = new Company();

    Employee employee = new Employee();


    Collection<Company> BranchsList;
    Collection<Employee> TrainerList;
    Collection<Employee> TrainerName;
    Collection<Company> CompanyList;

    int company_Id, branch_Id;
    string _Value;
    string s_login_role;
    string s_form = "";
    DataSet ds_userrights;

    protected void Page_Load(object sender, EventArgs e)
    {

        
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        

        if (!IsPostBack)
        {
            switch (s_login_role)
            {
                case "a": load();
                    break;

                case "h": load1();
                    break;

                case "u": s_form = "29";
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
       
    }

    public void load1()
    {

        con.Open();
        SqlCommand cmd = new SqlCommand("select * from INSTITUTION_PROFILE where id='" + Request.QueryString["id"] + "'", con);

        //displaying institute profile in listview

        SqlDataAdapter ada = new SqlDataAdapter(cmd);
       
        
        //displaying institute profile in detailsView
        SqlDataAdapter ada1 = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        ada1.Fill(ds);
        ada1.Dispose();
        ds.Clear();
        //displaying trainer  profile
        SqlCommand cmd1 = new SqlCommand("select * from trainer_profile1 where id='" + Request.QueryString["id"] + "'", con);
        ada = new SqlDataAdapter(cmd1);
        ada.Fill(ds);
        lv_trainers.DataSource = ds.Tables[0];
        lv_trainers.DataBind();
        con.Close();
    }

    protected void lv_trainers_ItemEditing(object sender, ListViewEditEventArgs e)
    {
        con.Open();
        lv_trainers.EditIndex = e.NewEditIndex;
        SqlCommand cmd5 = new SqlCommand("select * from trainer_profile1 where id='" + Request.QueryString["id"] + "'", con);
        SqlDataAdapter ada = new SqlDataAdapter(cmd5);
        DataSet ds = new DataSet();
        ada.Fill(ds);

        lv_trainers.DataSource = ds;
        lv_trainers.DataBind();
        con.Close();

    }
    protected void lv_trainers_ItemCanceling(object sender, ListViewCancelEventArgs e)
    {
        lv_trainers.EditIndex = -1;
        load1();
    }
    protected void lv_trainers_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {
        string fname, spec, worktype, ptype;
        int exp, rating;

        Label lblTid = (Label)lv_trainers.Items[e.ItemIndex].FindControl("lblTid");//hidden id

        TextBox txt=(lv_trainers.Items[e.ItemIndex].FindControl("txtname")) as TextBox;
            fname=txt.Text;
            txt = (lv_trainers.Items[e.ItemIndex].FindControl("txtspe")) as TextBox;
            spec=txt.Text;
        txt = (lv_trainers.Items[e.ItemIndex].FindControl("txtwt")) as TextBox;
            worktype = txt.Text;

        txt = (lv_trainers.Items[e.ItemIndex].FindControl("txtptype")) as TextBox;
            ptype = txt.Text;

        rating = ((AjaxControlToolkit.Rating)lv_trainers.Items[e.ItemIndex].FindControl("Rating2")).CurrentRating;


        txt = (lv_trainers.Items[e.ItemIndex].FindControl("txtexp")) as TextBox;
            exp = Convert.ToInt32(txt.Text);
        //*******************
        con.Open();
        SqlCommand cmdup = new SqlCommand("update trainer_profile1 set fname='"+fname+"',experience='"+exp+"',specification='"+spec+"',worktype='"+worktype+"',rating='"+rating+"',ptype='"+ptype+"' where id='" + Request.QueryString["id"] + "' and trainer_id='"+lblTid.Text+"'", con);
        cmdup.ExecuteNonQuery();
        lv_trainers.EditIndex = -1;
        con.Close();
        load1();


    }













    
}
