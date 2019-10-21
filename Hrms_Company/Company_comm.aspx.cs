using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using ePayHrms.Login;
using ePayHrms.BE.Recruitment;
using ePayHrms.Connection;
using ePayHrms.Candidate;
using System.IO;
using ePayHrms.Company;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ePayHrms.Employee;



public partial class Hrms_Company_Default : System.Web.UI.Page
{
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    //private SqlConnection _con;
    //ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    SqlDataReader rea;
    SqlDataReader rea1;
    SqlDataAdapter ada = new SqlDataAdapter();
    SqlDataAdapter ada1 = new SqlDataAdapter();
    private SqlConnection _Connection;
    ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();
    Collection<Company> CompanyList;
    Company company = new Company();
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    Candidate c = new Candidate();
    Collection<Employee> EmpFirstList;
    DropDownList ddl = new DropDownList();
    string[] rdate;
    string msg, Ename;
    string s_login_role;
    int ddl_i, grk, dept_id, emp_id;
    string _path, _Value;
    string s_form = "";
    DataSet ds_userrights;
    string str = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        

        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        c.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        s_login_role = Request.Cookies["Login_temp_Role"].Value;

        //mei
        SqlCommand com_new;
        if (!Page.IsPostBack)
        {
            myConnection.Open();
            com_new = new SqlCommand("select [v_departmentName] from paym_department where pn_branchid='" + c.BranchID + "'", myConnection);
            SqlDataReader rdr_new = com_new.ExecuteReader();

            while (rdr_new.Read())
            {
                ddl_dept.Items.Add(rdr_new[0].ToString());
            }
            myConnection.Close();
            rdr_new.Close();
        }


        if (s_login_role == "a")
        {

            lblmsg.Text = (string)Session["Login_Name"];
            img_photo.ImageUrl = (string)Session["Login_temp_Photo"];

        }

        else if (s_login_role == "h")
        {
            this.SiteMapDataSource1.Provider = SiteMap.Providers["Menu3Sitemap"];
            lblmsg.Text = (string)Session["Login_Name"];
            img_photo.ImageUrl = (string)Session["Login_temp_Photo"];

        }


        else if (s_login_role == "e")
        {
            // menu_del.Visible = false;
            this.SiteMapDataSource1.Provider = SiteMap.Providers["Menu2Sitemap"];
            lblmsg.Text = (string)Session["Login_Name"];
            img_photo.ImageUrl = (string)Session["Login_temp_Photo"];
        }
        else if (s_login_role == "u")
        {
            //menu_del.Visible = false;
            lblmsg.Text = (string)Session["Login_Name"];
            img_photo.ImageUrl = (string)Session["Login_temp_Photo"];
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        CompanyList = company.fn_getCompany();

        if (CompanyList.Count > 0)
        {
            s_login_role = Request.Cookies["Login_temp_Role"].Value;

            if (s_login_role == "a")
            {
                msg = (string)Session["Msg_session"];
                hr();
                
            }

            else if (s_login_role == "h")
            {
                msg = (string)Session["Msg_session"];
                hr();
                
            }

            else if (s_login_role == "e")
            {
                msg = (string)Session["Msg_session"];
                employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
                hr();
                
            }

            else
            {
                msg = (string)Session["Msg_session"];
                employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
                hr();
                
                // Label4.Text = s_login_role.ToString(); 
                //Response.Redirect("Login.aspx");
            }
        }
        else
        {
            //lbl_Error.Text = "Create Company";
        }


        if (!IsPostBack)
        {
            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {

                switch (s_login_role)
                {

                    case "a":
                        hr();

                        break;

                    case "h":
                        hr();
                        //**********************************
                        //Reimbrusement 
                        reimbursement();

                        break;

                    case "e":
                        emp();
                        outbox();
                        break;

                    case "u":
                        //s_form = "46";

                        //ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        //if (ds_userrights.Tables[0].Rows.Count > 0)
                        //{
                        //    employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
                            hr();

                        //}
                        //else
                        //{
                        //    Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator.";
                        //    Response.Redirect("~/Company_Home.aspx");
                        //}
                        break;
                    default:
                        Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                        Response.Redirect("../Hrms_Master/Common/Common_Home.aspx");
                        break;
                }
            }
            else
            {
                Response.Cookies["Msg_Session"].Value = "Create Company";
                Response.Redirect("~/Company_Home.aspx");
            }

        }




    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    public void hr()
    {

        myConnection.Open();

        SqlCommand command1 = new SqlCommand("select Employee_First_Name from paym_employee where pn_BranchId='" + employee.BranchId + "' and pn_companyID = '" + employee.CompanyId + "' and pn_EmployeeID='" + employee.EmployeeId + "'", myConnection);
        SqlDataReader re1 = command1.ExecuteReader();
        if (re1.Read())
        {
            Ename = Convert.ToString(re1["Employee_First_Name"]);
        }
        myConnection.Close();
        myConnection.Open();
        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM message where pn_branchID='" + employee.BranchId + "' and Receiver ='" + Ename + "'  order by id desc", myConnection);
        DataSet ds = new DataSet();
        ad.Fill(ds, "message");
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView1.DataSource = ds;
            GridView1.DataBind();
            int columnCount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columnCount;
            GridView1.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
            //lblmsg.Text = employee.BranchId.ToString();
        }

        myConnection.Close();
        //*******************
        myConnection.Open();
        SqlCommand comm = new SqlCommand("Select * from leave_apply where pn_branchID='" + employee.BranchId + "' ", myConnection);
        SqlDataReader read1 = comm.ExecuteReader();
        DropDownList3.Items.Clear();
        while (read1.Read())
        {
            DropDownList3.Items.Add(Convert.ToString(read1["reminder"]));
        }

        read1.Close();

        for (int i = 0; i < DropDownList3.Items.Count; i++)
        {
            if (DropDownList3.Items[i].Text == DateTime.Now.ToString("dd/MM/yyyy") + " 12:00:00 AM")
            {
                ModalPopupExtender1.Show();
            }
        }
        myConnection.Close();
        

    }
    public void reimbursement()
    {
        int req_count=0;
        myConnection.Open();
        SqlCommand cmd = new SqlCommand("select * from reimbursement where status='n' and pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "'", myConnection);
        SqlDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            req_count++;
            pnl_reimbursement.Visible = true;
        }
        //txthdr_reimbursement.Text = "You have " + req_count + " message form Ur Employee";
        ltrlhdr_reimbursement.Text = "<div style='background-color:#DBCDCC;font-family:Calibri;'><marquee><a href=../Hrms_Additional/Reimbursement.aspx> You have " + req_count + " message from your Employee</a></marquee></div>";
       
        myConnection.Close();
    }

    public void emp()
    {
        employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
        myConnection.Open();
        SqlCommand command1 = new SqlCommand("select Employee_First_Name from paym_employee where pn_BranchId='" + employee.BranchId + "' and pn_companyID = '" + employee.CompanyId + "' and pn_EmployeeID='" + employee.EmployeeId + "'", myConnection);
        SqlDataReader re1 = command1.ExecuteReader();
        if (re1.Read())
        {
            Ename = Convert.ToString(re1["Employee_First_Name"]);
        }
        myConnection.Close();
        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM message where pn_branchID='" + employee.BranchId + "' and Receiver ='" + Ename + "' and status='y'  ", myConnection);
        DataSet ds = new DataSet();
        ad.Fill(ds, "message");
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView1.DataSource = ds;
            GridView1.DataBind();
            int columnCount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columnCount;
            GridView1.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        myConnection.Open();
        cmd1 = new SqlCommand("select Employee_Full_Name from paym_Employee where pn_BranchID = '" + employee.BranchId + "'", myConnection);
        SqlDataReader dr_dept = cmd1.ExecuteReader();
        while (dr_dept.Read())
        {
            ddl_ename.Items.Add(Convert.ToString(dr_dept["Employee_Full_Name"]));
        }
        myConnection.Close();
        myConnection.Open();
        SqlCommand comm = new SqlCommand("Select * from task_schedule where pn_EmployeeID = '" + employee.EmployeeId + "' and Status='New' and pn_branchID='" + employee.BranchId + "' ", myConnection);
        SqlDataReader read1 = comm.ExecuteReader();
        DropDownList3.Items.Clear();
        while (read1.Read())
        {
            ModalPopupExtender2.Show();
        }

        read1.Close();
        myConnection.Close();
       
    }


    public void outbox()
{
        employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
        myConnection.Open();
        SqlCommand command1 = new SqlCommand("select Employee_First_Name from paym_employee where pn_BranchId='" + employee.BranchId + "' and pn_companyID = '" + employee.CompanyId + "' and pn_EmployeeID='" + employee.EmployeeId + "'", myConnection);
        SqlDataReader re1 = command1.ExecuteReader();
        if (re1.Read())
        {
            Ename = Convert.ToString(re1["Employee_First_Name"]);
        }
        re1.Close();
        
        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM message where pn_branchID = '" + employee.BranchId + "' and Sender = '" + Ename + "' and status = 'y'  order by id ", myConnection);
        DataSet ds = new DataSet();
        ad.Fill(ds, "message");
        
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView2.DataSource = ds;
            GridView2.DataBind();
            int columnCount = GridView2.Rows[0].Cells.Count;
            GridView2.Rows[0].Cells.Clear();
            GridView2.Rows[0].Cells.Add(new TableCell());
            GridView2.Rows[0].Cells[0].ColumnSpan = columnCount;
            GridView2.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            GridView2.DataSource = ds;
            GridView2.DataBind();
        }
        
        
        myConnection.Close();
        
}






    public void admin()
    {

    }




    //protected void Timer1_Tick(object sender, EventArgs e)
    //{

    //    //Datagrid refresh time
    //    myConnection.Open();

    //    cmd=new SqlCommand("select * from paym_employee where pn_employeeid='"+employee.EmployeeId+"'",myConnection);
    //    SqlDataReader rdr=cmd.ExecuteReader();
    //    if(rdr.Read())
    //    {
    //        str=rdr[4].ToString();
    //    }

    //    myConnection.Close();
    //    rdr.Close();
    //    SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM message where receiver='"+str+"' order by id desc", myConnection);

    //    DataSet ds = new DataSet();

    //    ad.Fill(ds, "message");
    //    GridView1.DataBind();


    //}
    protected void Button3_Click(object sender, EventArgs e)
    {
        string gvIDs = "";
        bool chkBox = false;
        //'Navigate through each row in the GridView for checkbox items
        foreach (GridViewRow gv in GridView1.Rows)
        {
            CheckBox deleteChkBxItem = (CheckBox)gv.FindControl("Check_yes");
            if (deleteChkBxItem.Checked)
            {
                chkBox = true;
                // Concatenate GridView items with comma for SQL Delete
                gvIDs += ((Label)gv.FindControl("lblhide")).Text.ToString() + ",";
            }
        }

        SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
        if (chkBox)
        {
            // Execute SQL Query only if checkboxes are checked to avoid any error with initial null string
            try
            {
                string deleteSQL = "update message set status='yes' WHERE id IN (" +
                  gvIDs.Substring(0, gvIDs.LastIndexOf(",")) + ")";
                SqlCommand cmd = new SqlCommand(deleteSQL, cn);
                cn.Open();
                cmd.ExecuteNonQuery();
                GridView1.DataBind();
            }
            catch (SqlException err)
            {
                Response.Write(err.Message.ToString());
            }
            finally
            {
                cn.Close();
            }

        }

    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        myConnection.Open();
        SqlCommand command = new SqlCommand("select Employee_Full_Name from paym_employee where pn_BranchId='" + employee.BranchId + "' and pn_companyID = '" + employee.CompanyId + "' and pn_EmployeeID='" + employee.EmployeeId + "'", myConnection);
        SqlDataReader re = command.ExecuteReader();
        if (re.Read())
        {
            Ename = Convert.ToString(re["Employee_Full_Name"]);
        }
        myConnection.Close();

        EmpFirstList = employee.fn_get_Emp_first(employee);

        if (EmpFirstList.Count > 0)
        {
            if (EmpFirstList[0].FirstName != "")
            {
                Ename = EmpFirstList[0].FirstName;
            }
        }

        string today = DateTime.Now.ToString("dd/MM/yyyy");
        myConnection.Open();
        cmd = new SqlCommand("insert into message(sender , receiver , message , date , pn_BranchID , pn_CompanyID , status) values ('" + Ename + "' , '" + ddl_ename.SelectedItem.Value + "' , '" + txt_msg.Value + "' , '" + today + "' , '" + employee.BranchId + "' , '" + employee.CompanyId + "' , 'y')", myConnection);
        cmd.ExecuteNonQuery();
        myConnection.Close();
    }


    protected void ddl_dept_SelectedIndexChanged1(object sender, EventArgs e)
    {
        ddl_ename.Items.Clear();
        myConnection.Open();

        {
            string str = ddl_dept.SelectedItem.Text;
            cmd = new SqlCommand("select distinct a.Employee_First_Name , b.v_departmentName, c.pn_EmployeeID , c.pn_DepartmentID from paym_employee a inner join paym_employee_profile1 c on a.pn_employeeid = c.pn_employeeid inner join paym_department b on c.pn_DepartmentID = b.pn_DepartmentID where v_DepartmentName = '" + str + "'", myConnection);

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {

                ddl_ename.Items.Add(rdr[0].ToString());
            }
            rdr.Close();
        }
        myConnection.Close();

    }


    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //lblmsg.Text = "Delettting";
        string ID = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lblid")).Text;
        DeleteRecord(ID);
        emp();   
    }
    
    private void DeleteRecord(string ID)
    {
        // MessageBox.Show(ID); checking 
         myConnection.Open();
        string sqlStatement = "update message set status = 'n' WHERE ID = @ID and pn_branchid = '" + employee.BranchId + "' and pn_companyid = '" + employee.CompanyId + "'";
            
            SqlCommand cmd = new SqlCommand(sqlStatement, myConnection);
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            myConnection.Close();
        
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            //string ID = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lblid")).Text;
            //DeleteRecord(ID);
            //emp();
        }
    }
    protected void but_Approve_Click(object sender, EventArgs e)
    {
        pnl_reimbursement.Visible = false;
    }
    protected void cmd_view_Click(object sender, EventArgs e)
    {

        myConnection.Open();
        
        SqlCommand cmd = new SqlCommand("select * from reimbusement where status='n' and pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "'", myConnection);
        SqlDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            Response.Redirect("..\\Hrms_Additional\\Reimbursement.aspx?reqid='"+rdr[0].ToString()+"'");
        }
        myConnection.Close();
        
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
