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
using System.Data.SqlClient;


public partial class Hrms_Master_Default : System.Web.UI.Page
{
    private SqlConnection _connection;
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlCommand cmd1 = new SqlCommand();
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand cmd = new SqlCommand();
    SqlDataReader rea;
    SqlDataAdapter ada = new SqlDataAdapter();

    Company company = new Company();

    Employee employee = new Employee();

    Leave l = new Leave();

    Collection<Leave> IncrementList;
    Collection<Company> CompanyList;

    int company_Id, branch_Id, valid, temp_valid = 0, check;
    string _Value, fname;
    string s_login_role;
    bool b_check = true;
    string s_form = "";
    DataSet ds_userrights;
    string str_id;//used at deleting in detailsview

    protected void Page_Load(object sender, EventArgs e)
    {
        fname = (string)Session["formulaName"];
        
        l.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        //rdo_percentage.Visible = false;
        if (!IsPostBack)
        {

            switch (s_login_role)
            {
                case "a":
                    hr();
                    load_admin();
                    break;

                case "h":
                    ddl_branch.Visible = false;
                    load();
                    access();
                    hr();

                    break;

                case "u": s_form = "25";
                    ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                    if (ds_userrights.Tables[0].Rows.Count > 0)
                    {
                        hr();
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
        populate_listview_promotion();
    }

    public void load_admin()
    {
        myConnection.Open();
        SqlDataAdapter ad = new SqlDataAdapter("select * from paym_branch", myConnection);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        ddl_branch.DataTextField = "branchname";
        ddl_branch.DataValueField = "pn_branchid";
        ddl_branch.DataSource = ds;
        ddl_branch.DataBind();
        ddl_branch.Items.Insert(0, "Select Branch");
        myConnection.Close();
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
            Response.Write("<script language='javascript'>alert('Permission Restricted. Please Contact Administrator.');window.location='~/Company_Home.aspx';</script>");
        }
        rdrview.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and section_edit='No'", _connection);
        SqlDataReader rdredit = cmd.ExecuteReader();
        if (rdredit.Read())
        {
            for (int b = 0; b < lv_promotion.Items.Count; b++)
            {
                ((LinkButton)lv_promotion.Items[b].FindControl("cmd_edit")).Visible = false;
                //((ImageButton)grid_Course.Rows[b].FindControl("img_update")).Visible = false;
            }
            //((System.Web.UI.Control)grid_Course.HeaderRow.FindControl("lbledit")).Visible = false;
        }
        rdredit.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and  section_delete='No'", _connection);
        SqlDataReader rdrdel = cmd.ExecuteReader(); 
        if (rdrdel.Read())
        {
            // ((ImageButton)grid_Course.Rows[0].FindControl("img_update")).Visible = false;
            for (int a = 0; a < lv_promotion.Items.Count; a++)
            {
                ((LinkButton)lv_promotion.Items[a].FindControl("cmd_del")).Visible = false;
            }
            //((System.Web.UI.Control)grid_Course.HeaderRow.FindControl("lbldel")).Visible = false;
        }
        rdrdel.Close();

    }

    public void populate_listview_promotion()
    {
        //var conStr = ConfigurationManager.ConnectionStrings["Connectionstring"];
        //string constr = conStr.ConnectionString;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
        con.Open();

        SqlCommand cmd = new SqlCommand("select * from promotion1 where pn_companyid='" + employee.CompanyId + "' and pn_branchid='" + employee.BranchId + "' order by grade ", con);
        SqlDataAdapter ada = new SqlDataAdapter(cmd);
        
        DataSet ds = new DataSet();
        ada.Fill(ds);
        lv_promotion.DataSource = ds;
        lv_promotion.DataBind();
        

    }
    


    public void hr()
    {
        
    }


    protected void lv_promotion_ItemEditing(object sender, ListViewEditEventArgs e)
    {
        lv_promotion.EditIndex = e.NewEditIndex;
        populate_listview_promotion();
        
        
    }
    protected void lv_promotion_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {
        l.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        //var conStr = ConfigurationManager.ConnectionStrings["connectionstring"];
        //string constr = conStr.ConnectionString;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
        con.Open();
        //string grade,dept;
        Label id = (Label)lv_promotion.EditItem.FindControl("lbl_hiddenid");
        DropDownList grade = (DropDownList)lv_promotion.Items[e.ItemIndex].FindControl("ddldept");
        DropDownList dept=(DropDownList)lv_promotion.EditItem.FindControl("ddlgrade");
        TextBox txtbasic=(TextBox)lv_promotion.EditItem.FindControl("txtbasic");
        TextBox txtupto_amt=(TextBox)lv_promotion.EditItem.FindControl("txtupto_amt");
        TextBox txtper=(TextBox)lv_promotion.EditItem.FindControl("txtpercentage");
        //SqlCommand cmd = new SqlCommand("update promotion1 set pn_departmentname='" +grade.SelectedItem.Text+ "'",con);
        SqlCommand cmd=new SqlCommand("update promotion1 set pn_companyid='"+l.CompanyID+"',pn_branchid='"+l.BranchID+"',pn_departmentname='"+dept.SelectedItem.Text+"',grade='"+grade.SelectedItem.Text+"',basic='"+txtbasic.Text+"',upto_amount='"+txtupto_amt.Text+"',percentage='"+txtper.Text+"' where id='"+id.Text+"'",con);
        cmd.ExecuteNonQuery();
        lv_promotion.EditIndex = -1;
        load();
        lblerror.Text = "Updated";
    }
    protected void lv_promotion_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        

    }
    protected void lv_promotion_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            var conStr = ConfigurationManager.ConnectionStrings["connectionstring"];
            string constr = conStr.ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            Label id = (Label)e.Item.FindControl("lbl_ihiddenid");
            //lblerror.Text = id.Text;
            SqlCommand cmd = new SqlCommand("delete promotion1 where id='" + id.Text + "'", con);
            cmd.ExecuteNonQuery();
            Response.Write("<script>alert('Record Deleted')</script>");
            load();
        }
    }
    protected void lv_promotion_ItemInserting(object sender, ListViewInsertEventArgs e)
    {
        try
        {
            int flg = 0;
            l.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
            l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            var conStr = ConfigurationManager.ConnectionStrings["connectionstring"];
            string constr = conStr.ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            TextBox txtbasic = (TextBox)e.Item.FindControl("txtibasic");
            TextBox txtper = (TextBox)e.Item.FindControl("txtipercentage");
            TextBox txtupto_amt = (TextBox)e.Item.FindControl("txtiupto_amt");
            DropDownList ddlgrade = (DropDownList)e.Item.FindControl("ddligrade");
            DropDownList ddldept = (DropDownList)e.Item.FindControl("ddlidept");
            //Checking tat already a grade name exist?
            SqlCommand cmd_chk = new SqlCommand("select grade,upto_amount from promotion1 where pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "' and grade='"+ddlgrade.SelectedItem.Text+"' and pn_departmentname='"+ddldept.SelectedItem.Text+"'", con);

            SqlDataReader rdr_chk = cmd_chk.ExecuteReader();

            while (rdr_chk.Read())
            {

                
                double uptoamt = Convert.ToDouble(rdr_chk[1].ToString());

                if (uptoamt >= Convert.ToDouble(txtbasic.Text))
                {
                    flg = 2;

                }
                

            }
            if (Convert.ToDouble(txtbasic.Text) >=Convert.ToDouble(txtupto_amt.Text))
            {
                flg = 3;
                Response.Write("<script>alert('Basic must not less than or equal UpTo amount')</script>");
            }

            //****************************************


            if (flg == 0)
            {
                SqlCommand cmd = new SqlCommand("insert into promotion1(pn_companyid,pn_branchid,pn_departmentname,grade,basic,upto_amount,percentage) values('" + l.CompanyID + "','" + l.BranchID + "','" + ddldept.SelectedItem.Text + "','" + ddlgrade.SelectedItem.Text + "','" + txtbasic.Text + "','" + txtupto_amt.Text + "','" + txtper.Text + "')", con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("update promotion1 set gradeid=( select distinct pn_gradeid from paym_grade where branchid='"+employee.BranchId+"' and pn_companyid='"+employee.CompanyId+"' and v_gradename='"+ddlgrade.SelectedItem.Text+"') ,deptid=(select distinct pn_departmentid from paym_department where pn_branchid='"+employee.BranchId+"' and pn_companyid='"+employee.CompanyId+"' and v_departmentname='"+ddldept.SelectedItem.Text+"') where pn_departmentname='"+ddldept.SelectedItem.Text+"' and grade='"+ddlgrade.SelectedItem.Text+"'", con);
                cmd.ExecuteNonQuery();
                Response.Write("<script>alert('Record Saved')</script>");
                load();
            }
           
            else if (flg == 2)
            {
                
                Response.Write("<script>alert('Basic must not less than or equal previous upto amount')</script>");
            }
            
        }
        catch (Exception exce)
        {
            lblerror.Text = "Please Fill all the details";
        }
    }
    protected void lv_promotion_ItemCanceling(object sender, ListViewCancelEventArgs e)
    {
        lv_promotion.EditIndex = -1;
        load();
    }
    protected void lv_promotion_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (s_login_role == "a")
        {
            employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
        }
        load();
    }
}
