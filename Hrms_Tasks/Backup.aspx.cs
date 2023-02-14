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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ePayHrms.Login;
using ePayHrms.Connection;
using System.IO;
using ePayHrms.Company;
using ePayHrms.Employee;

public partial class Hrms_Tasks_Default : System.Web.UI.Page
{
    SqlConnection con, con1;
    SqlCommand cmd;
    SqlDataAdapter da;

    Company company = new Company();
    Employee employee = new Employee();

    Collection<Company> CompanyList;

    string s_login_role;
    string s_form = "";
    DataSet ds_userrights;

    protected void Page_Load(object sender, EventArgs e)
    {
        

        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        s_login_role = Request.Cookies["Login_temp_Role"].Value;

        if (!IsPostBack)
        {
            txt_name.Enabled = false;

            CompanyList = company.fn_getCompany();
            if (CompanyList.Count > 0)
            {
                switch (s_login_role)
                {
                    case "a":
                        
                        break;

                    //case "h":
                    //    hr();
                    //    break;

                    //case "e": Response.Redirect("../Hrms_Employee/Employee_Preview.aspx");
                    //    break;

                    //case "u":
                    //    s_form = "46";

                    //    ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                    //    if (ds_userrights.Tables[0].Rows.Count > 0)
                    //    {
                    //        hr();

                    //    }
                    //    else
                    //    {
                    //        Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator.";
                    //        Response.Redirect("~/Company_Home.aspx");
                    //    }
                    //    break;
                    default:
                        Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                        Response.Redirect("~/Company_Home.aspx");
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

    protected void btn_backup_Click(object sender, EventArgs e)
    {
        string qry;
        string dt = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString();
        //con = new SqlConnection("server=aru;user id=admin;password=;database=Hrms_haspl");
        con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.AppSettings["Connectionstring"];
        if (rdo_button2.Checked == true)
        {
            qry = "backup database Hrms_haspl to disk='D:\\Hrms_" + dt + ".bak'";
        }
        else
        {
            qry = "backup database Hrms_haspl to disk='D:\\" + txt_name.Text + ".bak'";
        }
        con.Open();
        cmd = new SqlCommand(qry, con);
        cmd.ExecuteNonQuery();
        con.Close();
        ClientScriptManager manager = Page.ClientScript;
        if (rdo_button1.Checked == false)
        {
            qry = "restore database Hrms_haspl to disk='D:\\Hrms_" + dt + ".bak'";
        }
        else
        {
            qry = "restore database Hrms_haspl to disk='D:\\" + txt_name.Text + ".bak'";
        }
        manager.RegisterStartupScript(this.GetType(), "Call", "show_message('Backup successful');", true);
        //con.Dispose();
        //int i = con.ConnectionTimeout;
    }

    protected void btn_restore_Click(object sender, EventArgs e)
    {
        string file = "";
        if (FileUpload1.PostedFile.FileName.EndsWith(".bak"))
        {
            file = FileUpload1.PostedFile.FileName;
        }
        //con1 = new SqlConnection("server=aru;user id=admin;password=;database=master");
        con1 = new SqlConnection();
        con1.ConnectionString = ConfigurationManager.AppSettings["Master_Connection"];
        
        string qry = "select spid from master..sysprocesses where program_name='.Net SqlClient Data Provider' and status='sleeping'";
        con1.Open();
        da = new SqlDataAdapter(qry, con1);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con1.Close();
        int flag = 0;

        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                qry = "kill " + ds.Tables[0].Rows[i].ItemArray[0].ToString();
                con1.Open();
                cmd = new SqlCommand(qry, con1);
                try
                {
                    cmd.ExecuteNonQuery();
                    flag++;
                }
                catch (Exception ex)
                {
                    flag = 0;
                }
                con1.Close();
            }

            if (flag == ds.Tables[0].Rows.Count)
            {
                qry = "RESTORE DATABASE Hrms_haspl FROM disk ='" + file + "'";
                con1.Open();
                cmd = new SqlCommand(qry, con1);
                cmd.ExecuteNonQuery();
                con1.Close();
                connect();
            }
        }
        else
        {
            qry = "RESTORE DATABASE Hrms_haspl FROM disk ='" + file + "'";
            con1.Open();
            cmd = new SqlCommand(qry, con1);
            cmd.ExecuteNonQuery();
            con1.Close();
            connect();
        }
    }

    public void connect()
    {
        con = new SqlConnection("server=aru;user id=admin;password=;database=Hrms_haspl");
        con.Open();

        Response.Write("<script>alert(\"Restore completed succesfully..\");</script>");
        Response.Write("<script>window.close()</script>");
    }

    protected void rdo_button1_CheckedChanged(object sender, EventArgs e)
    {
        txt_name.Enabled = true;
        rdo_button2.Checked = false;
    }

    protected void rdo_button2_CheckedChanged(object sender, EventArgs e)
    {
        txt_name.Enabled = false;
        rdo_button1.Checked = false;
    }
}
