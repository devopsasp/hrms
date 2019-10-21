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
using System.Data.Sql;
using System.Windows.Forms;


public partial class Hrms_Master_Default : System.Web.UI.Page
{
    private SqlConnection _connection;
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
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
    DropDownList ddlid = new DropDownList();

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
                    hr();
                    access();

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

    public void grid_load()
    {
        // Gridview population
        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM paym_annual_increment where pn_BranchID='" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "' ", myConnection);

        DataSet ds = new DataSet();

        ad.Fill(ds, "paym_annual_increment");


        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView1.DataSource = ds;
            GridView1.DataBind();
            int columnCount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = 1;
            GridView1.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();

        }
    }

    public void load()
    {
        lblerror.Text = "";
        l.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        myConnection.Open();
        SqlCommand cmd = new SqlCommand("select v_departmentname,pn_departmentid from paym_department where pn_branchid='" + l.BranchID + "' and pn_companyid='" + l.CompanyID + "' ",myConnection);
        SqlDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            ListItem list = new ListItem();
            list.Text = rdr[0].ToString();
            list.Value = rdr[1].ToString();
            ddl_dept.Items.Add(list);
        }
        txtdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        rdr.Close();
        myConnection.Close();
        grid_load();
    }

    public void access()
    {
        myConnection.Open();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and section_view='No'", myConnection);
        SqlDataReader rdrview = cmd.ExecuteReader();
        if (rdrview.Read())
        {
            //Response.Write("<script language='javascript'>alert('Permission Restricted. Please Contact Administrator.');window.location='~/Company_Home.aspx';</script>");
        }
        rdrview.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and section_edit='No'", myConnection);
        SqlDataReader rdredit = cmd.ExecuteReader();
        if (rdredit.Read())
        {
            //btn_formulaedit.Visible = false;
        }
        rdredit.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and  section_delete='No'", myConnection);
        SqlDataReader rdrdel = cmd.ExecuteReader();
        if (rdrdel.Read())
        {
            // ((ImageButton)grid_Course.Rows[0].FindControl("img_update")).Visible = false;
        }
        rdrdel.Close();

    }

    public void hr()
    {
        
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


    protected void cmd_ok_Click(object sender, EventArgs e)
    {
        lblerror.Text = "";
        
       myConnection.Open();

        try
        {

            string date = txtdate.Text;
            string[] mydate = date.Split('/');
            string day = mydate[0];
            string month = mydate[1];
            string year = mydate[2];

            //lblerror.Text = year;
            string mydate1 = month + "/" + day + "/" + year;

            if (rdo_dept.Checked == true)
            {

                if (cmd_ok.Text == "Save")
                {

                    if (ddlmode.SelectedItem.Text == "Amount")
                    {
                        SqlCommand cmd = new SqlCommand("insert into paym_annual_increment(grade_dept,pn_companyid,pn_branchid,deptname,inc_mode,inc_value,deptid,date)values('dept','" + l.CompanyID + "','" + l.BranchID + "','" + ddl_dept.SelectedItem.Text + "','" + ddlmode.SelectedItem.Text + "','" + txtamt.Text + "','" + ddl_dept.SelectedValue.ToString() + "','" + mydate1 + "')",myConnection);
                        cmd.ExecuteNonQuery();
                        lblerror.Text = "Increment Amount Saved Successfully";
                    }
                    else if (ddlmode.SelectedItem.Text == "Percentage")
                    {
                        SqlCommand cmd1 = new SqlCommand("insert into paym_annual_increment(grade_dept,pn_companyid,pn_branchid,deptname,inc_mode,inc_value,deptid,date)values('dept','" + l.CompanyID + "','" + l.BranchID + "','" + ddl_dept.SelectedItem.Text + "','" + ddlmode.SelectedItem.Text + "','" + txtper.Text + "','" + ddl_dept.SelectedValue.ToString() + "','" + mydate1 + "')",myConnection);
                        cmd1.ExecuteNonQuery();
                        lblerror.Text = "Increment Percentage Saved Successfully";
                    }
                    pnl.Visible = false;
                    tbl.Visible = false;
                    ddlmode.SelectedIndex = 0;
                    ddl_dept.SelectedIndex = 0;
                    txtper.Text = "";
                    txtamt.Text = "";
                    txtdate.Text = "";
                    tr_rdo_grade.Visible = false;
                }
                else if (cmd_ok.Text == "Modify")
                {

                    if (ddlmode.SelectedItem.Text == "Amount")
                    {
                        txtper.Text = "0";
                        SqlCommand cmd = new SqlCommand("update paym_annual_increment set grade_dept='dept',inc_mode='" + ddlmode.SelectedItem.Text + "',inc_amt='" + txtamt.Text + "',date='" + mydate1 + "',inc_percentage='" + txtper.Text + "' where deptid='" + ddl_dept.SelectedValue.ToString() + "'",myConnection);
                        cmd.ExecuteNonQuery();
                        lblerror.Text = "Increment Amount Modified Successfully";
                    }
                    else if (ddlmode.SelectedItem.Text == "Percentage")
                    {
                        txtamt.Text = "0";
                        SqlCommand cmd1 = new SqlCommand("update paym_annual_increment set grade_dept='dept', inc_mode='" + ddlmode.SelectedItem.Text + "',inc_percentage='" + txtper.Text + "',date='" + mydate1 + "',inc_amt='" + txtamt.Text + "' where deptid='" + ddl_dept.SelectedValue.ToString() + "'",myConnection);
                        cmd1.ExecuteNonQuery();
                        lblerror.Text = "Increment Percentage Modified Successfully";
                    }
                    grid_load();
                    //load();

                }
            }
            else if (rdo_grade.Checked == true)
            {
                //**********************************
                if (cmd_ok.Text == "Save")
                {
                    if (ddlmode.SelectedItem.Text == "Amount")
                    {
                        SqlCommand cmd = new SqlCommand("insert into paym_annual_increment(grade_dept,pn_companyid,pn_branchid,grade_name,inc_mode,inc_amt,grade_id,date)values('grade','" + l.CompanyID + "','" + l.BranchID + "','" + ddlgrade.SelectedItem.Text + "','" + ddlmode.SelectedItem.Text + "','" + txtamt.Text + "','" + ddlgrade.SelectedValue.ToString() + "','" + mydate1 + "')",myConnection);
                        cmd.ExecuteNonQuery();
                        lblerror.Text = "Increment Amount Saved Successfully";
                    }
                    else if (ddlmode.SelectedItem.Text == "Percentage")
                    {
                        SqlCommand cmd1 = new SqlCommand("insert into paym_annual_increment(grade_dept,pn_companyid,pn_branchid,grade_name,inc_mode,inc_percentage,grade_id,date)values('grade','" + l.CompanyID + "','" + l.BranchID + "','" + ddlgrade.SelectedItem.Text + "','" + ddlmode.SelectedItem.Text + "','" + txtper.Text + "','" + ddlgrade.SelectedValue.ToString() + "','" + mydate1 + "')",myConnection);
                        cmd1.ExecuteNonQuery();
                        lblerror.Text = "Increment Percentage Saved Successfully";
                    }
                    pnl.Visible = false;
                    tbl.Visible = false;
                    ddlmode.SelectedIndex = 0;
                    ddl_dept.SelectedIndex = 0;
                    txtper.Text = "";
                    txtamt.Text = "";
                    txtdate.Text = "";
                }
                else if (cmd_ok.Text == "Modify")
                {
                    if (ddlmode.SelectedItem.Text == "Amount")
                    {
                        txtper.Text = "0";
                        SqlCommand cmd = new SqlCommand("update paym_annual_increment set grade_dept='grade',inc_mode='" + ddlmode.SelectedItem.Text + "',inc_amt='" + txtamt.Text + "',date='" + mydate1 + "',inc_percentage='" + txtper.Text + "' where grade_id='" + ddlgrade.SelectedValue.ToString() + "'",myConnection);
                        cmd.ExecuteNonQuery();
                        lblerror.Text = "Increment Amount Modified Successfully";
                    }
                    else if (ddlmode.SelectedItem.Text == "Percentage")
                    {
                        txtamt.Text = "0";
                        SqlCommand cmd1 = new SqlCommand("update paym_annual_increment set grade_dept='grade', inc_mode='" + ddlmode.SelectedItem.Text + "',inc_percentage='" + txtper.Text + "',date='" + mydate1 + "',inc_amt='" + txtamt.Text + "' where grade_id='" + ddlgrade.SelectedValue.ToString() + "'",myConnection);
                        cmd1.ExecuteNonQuery();
                        lblerror.Text = "Increment Percentage Modified Successfully";
                    }

                    load();
                }

                //**********************************
            }
            rdo_dept.Checked = false;
            rdo_grade.Checked = false;
            load();
        }
        catch (Exception excpe)
        {
            lblerror.Text = "Required Field Missing";
        }
    }
   
    protected void ddlmode_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblerror.Text = "";
        tr_rdo_grade.Visible = true;
       
        if (ddlmode.SelectedItem.Text == "Amount")
        {
            txtper.Text = "";
            tr_per.Visible = false;
            tr_amt.Visible = true;
           
        }
        else if (ddlmode.SelectedItem.Text == "Percentage")
        {
            txtamt.Text = "";
            tr_amt.Visible = false;
            tr_per.Visible = true;
            
        }
    }

    protected void ddl_dept_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblerror.Text = "";
        txtamt.Text = "";
        txtper.Text = "";
        txtdate.Text = "";
        DateTime date = new DateTime();

       myConnection.Open();
        //******************************
        //checking for if the previous allotment is made for individuals are not,If so we cant modify those
        SqlCommand cmd = new SqlCommand("select grade_dept,deptid,gradeid from annual_increment_allotments where pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "'",myConnection);
        SqlDataReader rdr_mcheck = cmd.ExecuteReader();
        DropDownList ddl_chk_dept = new DropDownList();
        //DropDownList ddl_chk_grade = new DropDownList();

        while (rdr_mcheck.Read())
        {
            string t = rdr_mcheck[0].ToString();
            if (t == "dept")
            {
                ddl_chk_dept.Items.Add(rdr_mcheck[1].ToString());
            }            

        }
        for (int i = 0; i < ddl_chk_dept.Items.Count; i++)
        {
            if (ddl_chk_dept.Items[i].Text == ddl_dept.SelectedItem.Value)
            {

                //lblerror.Text = "Unable to modify,Allotment already made ";
                cmd_ok.Enabled = false;
               
            }
            else
            {
                cmd_ok.Enabled = true;
                lblerror.Text = "";
                
            }
        
        }
        //******************************


       
        //check for insert or update
       
        SqlCommand cmd1 = new SqlCommand("select date,inc_mode,inc_value from paym_annual_increment where pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "' and deptid='" + ddl_dept.SelectedValue.ToString() + "'",myConnection);
        

        SqlDataReader rdr = cmd1.ExecuteReader();
        if (rdr.Read())
        {
            date = Convert.ToDateTime(rdr[0].ToString());
            string dat = date.ToString();
            string[] mydate = dat.Split(' ');
            
            if (rdr[1].ToString() == "Amount")
            {
                txtamt.Text = rdr[2].ToString();
                tr_amt.Visible = true;
                tr_per.Visible = false;
                txtdate.Text = mydate[0].ToString();
                ddlmode.SelectedIndex = 1;
            }
            else if (rdr[1].ToString() == "Percentage")
            {
                txtper.Text = rdr[2].ToString();
                tr_amt.Visible = false;
                tr_per.Visible = true;
                txtdate.Text = mydate[0].ToString();
                ddlmode.SelectedIndex = 1;
            }
            else
            {
                txtdate.Text = "";
            }
            

            
        }
        if (date.ToString("yyyy") == DateTime.Now.Year.ToString())
        {
            cmd_ok.Text = "Modify";
        }
        else
        {
            cmd_ok.Text = "Save";
        }
        rdr.Close();
       myConnection.Close();
       
       
    }

    protected void rdo_grade_CheckedChanged(object sender, EventArgs e)
    {
        lblerror.Text = "";
        pnl.Visible = true;//making all visible
        tbl.Visible = true;
        int flag = 0;

        tr_grade.Visible = true;
        tr_dept.Visible = true;
        ddlgrade.Items.Clear();
        ddlgrade.Items.Add("Select");

       myConnection.Open();
        SqlCommand cmd = new SqlCommand("select v_gradename,pn_gradeid from paym_grade where branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "'",myConnection);
        SqlDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            ListItem list = new ListItem();
            list.Text = rdr[0].ToString();
            list.Value = rdr[1].ToString();
            ddlgrade.Items.Add(list);
        }
        rdr.Close();
       myConnection.Close();
        //checking tat is there alredy inc alloted against deptwise
       myConnection.Open();
        
        //*****************************
        //checking tat allotments made to individuals or not,if made we can delete this
        SqlCommand cmd_dcheck = new SqlCommand("select grade_dept from annual_increment_allotments where pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "'",myConnection);
        SqlDataReader rdr_dcheck = cmd_dcheck.ExecuteReader();
        while (rdr_dcheck.Read())
        {
            string t = rdr_dcheck[0].ToString();
            if (t == "dept" || t == "grade")
            {
                lblpopup_dept.Text = "You can't change because already departmentwise allotments were made";
                flag = 1;
                pnl_popup.Visible = true;
                popup_container.Visible = true;
                cmd_k.Visible = false;
                cmd_dept_k.Visible = false;
            }

        }

      
        //******************************
        if (flag == 0)
        {
            cmd_k.Visible = true;
            cmd_dept_k.Visible = true;
            SqlCommand cmd1 = new SqlCommand("select grade_dept from paym_annual_increment where pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "'",myConnection);
            SqlDataReader rdr1 = cmd1.ExecuteReader();
            while (rdr1.Read())
            {
                string t = rdr1[0].ToString();
                if (t == "dept")
                {
                    pnl_popup.Visible = true;
                    popup_container.Visible = true;

                }
            }
            rdr1.Close();
           myConnection.Close();
        }

    }
    protected void rdo_dept_CheckedChanged(object sender, EventArgs e)
    {
        lblerror.Text = "";
        pnl.Visible = true;//making all visible
        tbl.Visible = true;

        tr_grade.Visible = false;
        tr_dept.Visible = true;
        int flag = 0;

        //checking tat is there alredy inc alloted against grade_wise
       myConnection.Open();
        //*****************************
        //checking tat allotments made to individuals or not,if made we can delete this
        SqlCommand cmd_dcheck = new SqlCommand("select grade_dept from annual_increment_allotments where pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "'",myConnection);
        SqlDataReader rdr_dcheck = cmd_dcheck.ExecuteReader();
        while (rdr_dcheck.Read())
        {
            string t = rdr_dcheck[0].ToString();
            if (t == "dept" || t == "grade")
            {
                lblpopup.Text = "U cant delete because already gradewise allotment is made";
                flag = 1;
                cmd_k.Visible = false;
                cmd_dept_k.Visible = false;
            }

        }


        //******************************
        if (flag == 0)
        {
            cmd_k.Visible = true;
            cmd_dept_k.Visible = true;
            SqlCommand cmd1 = new SqlCommand("select grade_dept from paym_annual_increment where pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "'",myConnection);
            SqlDataReader rdr1 = cmd1.ExecuteReader();
            while (rdr1.Read())
            {
                string t = rdr1[0].ToString();
                if (t == "grade")
                {
                    pnl_popup1.Visible = true;
                    popup_container.Visible = true;

                }
            }
            rdr1.Close();
           myConnection.Close();
            //checking tat  there alredy inc alloted against gradewise
           myConnection.Open();
            SqlCommand cmd = new SqlCommand("select grade_dept from paym_annual_increment where pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "'",myConnection);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                string t = rdr[0].ToString();
                if (t == "grade")
                {
                    popup_container.Visible = true;/////////
                    pnl_popup1.Visible = true;

                }
            }
            rdr.Close();
           myConnection.Close();
        }
    }

    protected void ddlgrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblerror.Text = "";
        txtamt.Text = "";
        txtper.Text = "";
        txtdate.Text = "";
        DateTime date = new DateTime();
       myConnection.Open();
        //*******************************
        //checking for if the previous allotment is made for individuals are not,If so we cant modify those
        SqlCommand cmd = new SqlCommand("select grade_dept,deptid,gradeid from annual_increment_allotments where pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "'",myConnection);
        SqlDataReader rdr_mcheck = cmd.ExecuteReader();
        
        DropDownList ddl_chk_grade = new DropDownList();

        while (rdr_mcheck.Read())
        {
            string t = rdr_mcheck[0].ToString();
            if (t == "grade")
            {
                ddl_chk_grade.Items.Add(rdr_mcheck[2].ToString());
            }

        }
        for (int i = 0; i <ddlgrade.Items.Count ; i++)
        {
            if (ddl_chk_grade.Items[i].Text == ddlgrade.SelectedItem.Value)
            {
                lblerror.Text = "Unable to Modify";
                cmd_ok.Enabled = false;
            }
            else
            {
                cmd_ok.Enabled = true;
                lblerror.Text = "";
            }

        }

        //*******************************


        //check for insert or update
        SqlCommand cmd1 = new SqlCommand("select date,inc_mode,inc_percentage,inc_amt from paym_annual_increment where pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "' and grade_id='" + ddlgrade.SelectedValue.ToString() + "'",myConnection);
        SqlDataReader rdr = cmd1.ExecuteReader();
        if (rdr.Read())
        {
            date = Convert.ToDateTime(rdr[0].ToString());
            string dat = date.ToString();
            string[] mydate = dat.Split(' ');
            
            if (rdr[1].ToString() == "Amount")
            {
                txtamt.Text = rdr[3].ToString();
                tr_amt.Visible = true;
                tr_per.Visible = false;
                txtdate.Text = mydate[0].ToString();
                ddlmode.SelectedIndex = 1;
            }
            else if (rdr[1].ToString() == "Percentage")
            {
                txtper.Text = rdr[2].ToString();
                tr_per.Visible = true;
                tr_amt.Visible = false;
                txtdate.Text = mydate[0].ToString();
                ddlmode.SelectedIndex = 2;
            }
            else
            {
                txtdate.Text = "";
            }
            
        }
        if (date.ToString("yyyy") == DateTime.Now.Year.ToString())
        {
            cmd_ok.Text = "Modify";
        }
        else
        {
            cmd_ok.Text = "Save";
        }
        rdr.Close();
       myConnection.Close();
    }
    protected void cmd_k_Click(object sender, EventArgs e)
    {
        lblerror.Text = "";
       
       myConnection.Open();
        
        SqlCommand cmd1 = new SqlCommand("select id,grade_dept from paym_annual_increment where pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "'",myConnection);
        SqlDataReader rdr = cmd1.ExecuteReader();
        int id = -1;
        while (rdr.Read())
        {
            string t = rdr[1].ToString();
            if (t == "dept")
            {
                id = Convert.ToInt32(rdr[0]);
            }
            if (id != -1)
            {
                SqlCommand cmd2 = new SqlCommand("delete paym_annual_increment where pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "' and id='" + id + "'",myConnection);
                cmd2.ExecuteNonQuery();
                id = -1;
                lblerror.Text = "Previous Departmentwise allotment deleted";
            }
        }
        rdr.Close();
    
        
        
        pnl_popup.Visible = false;
        popup_container.Visible = false;
    }
    protected void cmd_c_Click(object sender, EventArgs e)
    {
        lblerror.Text = "";
        rdo_dept.Checked = false;
        rdo_grade.Checked = false;
        ddlgrade.SelectedIndex = 0;
        pnl_popup.Visible = false;
        popup_container.Visible = false;
        tr_grade.Visible = false;
        tr_dept.Visible = true;
        tbl.Visible = false;
    }
    protected void cmd_dept_k_Click(object sender, EventArgs e)
    {
        lblerror.Text = "";
        
       myConnection.Open();
       
       
        SqlCommand cmd1 = new SqlCommand("select id,grade_dept from paym_annual_increment where pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "'",myConnection);
        SqlDataReader rdr = cmd1.ExecuteReader();
        int id = -1;
        while (rdr.Read())
        {
            string t = rdr[1].ToString();
            if (t == "grade")
            {
                id = Convert.ToInt32(rdr[0]);
            }
            if (id != -1)
            {
                SqlCommand cmd2 = new SqlCommand("delete paym_annual_increment where pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "' and id='" + id + "'",myConnection);
                cmd2.ExecuteNonQuery();
                lblerror.Text = "Previous Gradewise allotment deleted";
                id = -1;
            }
        }
        rdr.Close();
        
        pnl_popup1.Visible = false;
        popup_container.Visible = false;
    }
    protected void cmd_dept_c_Click(object sender, EventArgs e)
    {
        lblerror.Text = "";
        rdo_grade.Checked = false;
        rdo_dept.Checked = false;
        popup_container.Visible = false;
        pnl_popup1.Visible = false;
        tr_grade.Visible = true;
        tr_dept.Visible = false;
        tbl.Visible = false;
    }
    protected void txtamt_TextChanged(object sender, EventArgs e)
    {

    }
    protected void cmd_dept_c_Click1(object sender, EventArgs e)
    {
        lblerror.Text = "";
        rdo_dept.Checked = false;
        rdo_grade.Checked = false;
        ddlgrade.SelectedIndex = 0;
        pnl_popup.Visible = false;
        popup_container.Visible = false;
        tr_grade.Visible = true;
        
        tr_dept.Visible = false;
        tbl.Visible = false;
        
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ArrayList dep = new ArrayList();
        ArrayList annual = new ArrayList();
        string date = DateTime.Now.ToString("dd/MM/yyyy");
        string[] date1 = date.Split('/');
        string dd = date1[0];
        string mm = date1[1];
        string yy = date1[2];
        string dat = mm + "/" + dd + "/" + yy;
        myConnection.Open();
        cmd = new SqlCommand("select * from paym_department where pn_companyid = '" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
        SqlDataReader rdr_dep = cmd.ExecuteReader();
        while (rdr_dep.Read())
        {
            dep.Add(Convert.ToString(rdr_dep["v_DepartmentName"]));
        }
        rdr_dep.Close();
        cmd = new SqlCommand("delete from paym_annual_increment where pn_companyid = '" + employee.CompanyId + "' and pn_branchid = '" + employee.BranchId + "'", myConnection);
        cmd.ExecuteNonQuery();
       
        for (int b = 0; b < dep.Count; b++)
        {
            string d_id="";
            cmd = new SqlCommand("select * from paym_department where pn_branchid='" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "' and v_departmentname = '" + dep[b].ToString() + "'", myConnection);
            SqlDataReader rdr_did = cmd.ExecuteReader();
            if (rdr_did.Read())
            {
                d_id = Convert.ToString(rdr_did["pn_departmentid"]);
            }
            rdr_did.Close();
            cmd1 = new SqlCommand("insert into paym_annual_increment(pn_companyid,pn_branchid,deptname,inc_mode,inc_percentage,deptid,date) values ('" + employee.CompanyId + "' , '" + employee.BranchId + "' , '" + dep[b].ToString() + "' , 'Percentage', '" + txt_incvalue.Text + "' , '" + d_id + "' ,'" + dat + "')", myConnection);  
            cmd1.ExecuteNonQuery();
            
            lblerror.Text = "Annual increment saved successfully";
            grid_load();
            
        }
        myConnection.Close();
    }
    protected void Link1_Click(object sender, EventArgs e)
    {
        if (Link1.Text == "Click here for overall annual increment")
        {
            overall.Visible = true;
            Link1.Text = "Hide overall annual increment";
        }
        else
        {
            overall.Visible = false;
            Link1.Text = "Click here for overall annual increment";
        }
    }

    protected void ddl_branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (s_login_role == "a")
        {
            l.BranchID = Convert.ToInt32(ddl_branch.SelectedItem.Value);
        }
        load();
    }
}
