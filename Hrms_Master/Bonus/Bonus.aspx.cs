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

    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand cmd = new SqlCommand();
    Company company = new Company();
    Employee employee = new Employee();
    Leave l = new Leave();

    Collection<Leave> AppraisalList;
    Collection<Company> CompanyList;

    int company_Id, branch_Id, valid, temp_valid = 0, check;
    string _Value;
    string s_login_role;
    string s_form = "";
    DataSet ds_userrights;

    protected void Page_Load(object sender, EventArgs e)
    {
        

        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        l.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;

        if (!IsPostBack)
        {

            switch (s_login_role)
            {
                case "a": load_admin();
                    tb_bonus.Visible = false;
                    annual_tbl.Visible = false;
                    break;

                case "h": load();
                    access();
                    break;

                case "u": s_form = "24";
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
        //display date in textbox
        if (!Page.IsPostBack)
            setdate();
    }
    public void setdate()
    {
        //display date in textbox

        myConnection.Open();
        SqlCommand cmd = new SqlCommand("select date,pn_increment from paym_annualincrement where pn_companyid='" + l.CompanyID + "' and pn_branchid='" + l.BranchID + "'", myConnection);
        SqlDataReader rdr = cmd.ExecuteReader();
        if (rdr.Read())
        {
            DateTime str = Convert.ToDateTime(rdr[0]);

            txt_inc_date.Value = str.ToShortDateString();
            Txt_annual_incr.Value = rdr[1].ToString() + "%";
            butadd_inc.Text = "Modify";
            lbldate.Visible = true;
            txt_inc_date.Visible = true;

        }
        myConnection.Close();
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
        myConnection.Open();
        for (int x = 0;  x< grid_appraisal.Rows.Count; x++)
        {
            ((HtmlInputCheckBox)grid_appraisal.Rows[x].FindControl("Chk_Grade")).Visible = false;
        }
        
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and section_view='No'",myConnection);
        SqlDataReader rdrview = cmd.ExecuteReader();
        
       
                
        if (rdrview.Read())
        {
            Response.Write("<script language='javascript'>alert('Permission Restricted. Please Contact Administrator.');window.location='~/Company_Home.aspx';</script>");
           
        }
        rdrview.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and section_edit='No'",myConnection);
        SqlDataReader rdredit = cmd.ExecuteReader();
        if (rdredit.Read())
        {
            for (int b = 0; b < grid_appraisal.Rows.Count; b++)
            {
                ((ImageButton)grid_appraisal.Rows[b].FindControl("img_update")).Visible = false;
            }
            ((Label)grid_appraisal.HeaderRow.FindControl("lbledit")).Visible = false;
        }
        rdredit.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and  section_delete='No'",myConnection);
        SqlDataReader rdrdel = cmd.ExecuteReader();
        if (rdrdel.Read())
        {
            
            // ((ImageButton)grid_Course.Rows[0].FindControl("img_update")).Visible = false;
            for (int a = 0; a < grid_appraisal.Rows.Count; a++)
            {
                
               
                ((ImageButton)grid_appraisal.Rows[a].FindControl("imgdel")).Visible = false;
                
                
            }
            ((Label)grid_appraisal.HeaderRow.FindControl("lbldel")).Visible = false;
        }
        rdrdel.Close();
        myConnection.Close();

    }

    public void load()
    {
        grid_load();
        annual_tbl.Visible = false;
        Button1.Visible = false;
        points.Visible = false;
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        l.AppraisalID = 0;
        l.AppraisalName = txt_appraisalname.Value;
        l.totalpoint = Convert.ToInt32(Txt_points.Value);
        l.status = 'Y';
        _Value = l.Bonus(l);

        if (_Value != "1")
        {
            lbl_Error.Text = "<font color=Blue>Added Successfully</font>";

            myConnection.Open();
            cmd = new SqlCommand("update paym_bonus set pn_BranchID = '" + employee.BranchId + "' where v_BonusName = '" + txt_appraisalname.Value + "'", myConnection);
            cmd.ExecuteNonQuery();
            myConnection.Close();
            txt_appraisalname.Value = "";
            Txt_points.Value = "";
            grid_load();
        }
        else
        {
            lbl_Error.Text = "<font color=Red>Error Occured</font>";
        }
    }

    public void grid_load()
    {
        AppraisalList = l.fn_Bonus1(employee.BranchId);

        if (AppraisalList.Count > 0)
        {
            grid_appraisal.DataSource = AppraisalList;
            grid_appraisal.DataBind();
        }
        else
        {
            lbl_Error.Text = "No Data in Master";
        }
    }

    protected void Update(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            ((HtmlInputText)grid_appraisal.Rows[e.RowIndex].FindControl("txtgrid")).Disabled = false;
            ((HtmlInputText)grid_appraisal.Rows[e.RowIndex].FindControl("Txtgpoint")).Disabled = false;
            ((ImageButton)grid_appraisal.Rows[e.RowIndex].FindControl("img_save")).Visible = true;
            ((ImageButton)grid_appraisal.Rows[e.RowIndex].FindControl("img_update")).Visible = false;
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }

    protected void Edit(object sender, GridViewEditEventArgs e)
    {

        try
        {
            
            l.Bonus_id = Convert.ToInt32(grid_appraisal.DataKeys[e.NewEditIndex].Value);
            l.BonusName = ((HtmlInputText)grid_appraisal.Rows[e.NewEditIndex].FindControl("txtgrid")).Value;
            if (((HtmlInputText)grid_appraisal.Rows[e.NewEditIndex].FindControl("Txtgpoint")).Value != "")
            {
                l.totalpoint = Convert.ToInt32(((HtmlInputText)grid_appraisal.Rows[e.NewEditIndex].FindControl("Txtgpoint")).Value);
            }
            else
            {
                ClientScriptManager manager = Page.ClientScript;
                manager.RegisterStartupScript(this.GetType(), "Call", "show_Error1();", true);

            }

            l.status = 'Y';
            check = name_validate(l.AppraisalName);
            if (l.AppraisalName != "")
            {
                if (check == 0)
                {
                    _Value = l.Bonus(l);

                    if (_Value != "1")
                    {
                        lbl_Error.Text = "<font color=Blue>Updated Successfully</font>";
                        txt_appraisalname.Value = "";
                        Txt_points.Value = "";
                        grid_load();

                        ((ImageButton)grid_appraisal.Rows[e.NewEditIndex].FindControl("img_update")).Visible = true;
                        ((ImageButton)grid_appraisal.Rows[e.NewEditIndex].FindControl("img_save")).Visible = false;
                        ((HtmlInputText)grid_appraisal.Rows[e.NewEditIndex].FindControl("txtgrid")).Disabled = true;
                        ((HtmlInputText)grid_appraisal.Rows[e.NewEditIndex].FindControl("Txtgpoint")).Disabled = true;
                    }
                    else
                    {
                        lbl_Error.Text = "<font color=Red>Error Occured</font>";
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

    public int name_validate(string m_name)
    {

        AppraisalList = l.fn_Appraisal();

        if (AppraisalList.Count > 0)
        {
            for (valid = 0; valid < AppraisalList.Count; valid++)
            {

                if (AppraisalList[valid].AppraisalName == m_name)
                {
                    temp_valid++;

                }

            }

        }
        return temp_valid;
    }

    protected void delete(object sender, GridViewDeleteEventArgs e)
    {
        //var conStr = ConfigurationManager.ConnectionStrings["connectionstring"];//connectionstring
        //string constr = conStr.ConnectionString;
        //SqlConnection con = new SqlConnection(constr);
        //con.Open();
        //SqlCommand cmd = new SqlCommand("delete paym_bonus where v_BonusName='" + txt_appraisalname.Value + "'", con);
        //cmd.ExecuteNonQuery();
        //lbl_Error.Text = txt_appraisalname.Value + " deleteds";
        //grid_load();
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        l.AppraisalID = 0;
        l.AppraisalName = txt_appraisalname.Value;
        l.totalpoint = 10;
        l.status = 'Y';
        _Value = l.Bonus(l);
        if (txt_appraisalname.Value != "")
        {
            if (_Value != "1")
            {
                lbl_Error.Text = "<font color=Blue>Added Successfully</font>";

                myConnection.Open();
                cmd = new SqlCommand("update paym_bonus set pn_BranchID = '" + employee.BranchId + "' where v_BonusName = '" + txt_appraisalname.Value + "' and pn_BranchID is null", myConnection);
                cmd.ExecuteNonQuery();
                myConnection.Close();
                txt_appraisalname.Value = "";
                Txt_points.Value = "";
                grid_load();
            }
            else
            {
                lbl_Error.Text = "<font color=Red>Error Occured</font>";
            }
        }
        else 
        {
            lbl_Error.Text = "<font color=Red>Please Enter the Bonasname</font>";
        }
    }
    protected void butadd_inc_Click(object sender, EventArgs e)
    {
        myConnection.Open();

        if (butadd_inc.Text == "Add")
        {
            string today = DateTime.Today.ToString();
            SqlCommand cmd = new SqlCommand("insert into paym_annualincrement(pn_companyid,pn_branchid,pn_increment,date) values('" + l.CompanyID + "','" + l.BranchID + "','" + Txt_annual_incr.Value + "','" + today + "')", myConnection);
            cmd.ExecuteNonQuery();
            lbl_Error.Text = "Increment Saved Successfully";
            setdate();
        }
        else if (butadd_inc.Text == "Modify")
        {
            //SqlCommand cmd = new SqlCommand("insert into paym_annualincrement(pn_companyid,pn_branchid,pn_increment,date) values('" + l.CompanyID + "','" + l.BranchID + "','" + Txt_annual_incr.Value + "','" + today + "')", con);
            string today = DateTime.Today.ToString();
            SqlCommand cmd = new SqlCommand("update paym_annualincrement set pn_increment='" + Txt_annual_incr.Value + "',date='" + today + "' where pn_branchid='" + l.BranchID + "' and pn_companyid='" + l.CompanyID + "' and pn_branchid='" + l.BranchID + "'", myConnection);
            cmd.ExecuteNonQuery();
            lbl_Error.Text = "Increment Upadted Successfully";

            setdate();

        }

        myConnection.Close();
    }
    protected void grid_appraisal_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grid_appraisal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            try
            {
                //finding row index
                GridViewRow gvrow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);//catching the row in which thhe link button is clicked.
                HtmlInputText lnkbtn = (HtmlInputText)gvrow.FindControl("txtgrid");
                string str = lnkbtn.Value;

                myConnection.Open();
                SqlCommand cmd = new SqlCommand("delete from paym_bonus where v_BonusName='" + str + "' and pn_branchid='" + employee.BranchId + "'", myConnection);
                cmd.ExecuteNonQuery();
                load();
                lbl_Error.Text = "Bonus Deleted Successfully";
            }
            catch (Exception exc)
            {
                lbl_Error.Text = "Couldnt delete because the bonus is assigned to employee";
                lbl_Error.Visible = true;
            }
        }
        myConnection.Close();
    }
    protected void ddl_branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (s_login_role == "a")
        {
            employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
        }
        load();
        tb_bonus.Visible = true;
        annual_tbl.Visible = true;
    }
}
