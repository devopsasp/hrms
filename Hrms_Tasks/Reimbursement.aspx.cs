using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using ePayHrms.Login;
using ePayHrms.BE.Recruitment;
using ePayHrms.Connection;
using ePayHrms.Candidate;
using System.IO;
using ePayHrms.Company;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ePayHrms.Leave;
//using System.Drawing;
//using System.Drawing.Imaging;
using ePayHrms.Employee;

public partial class Hrms_Additional_Default : System.Web.UI.Page
{
   //var sqlCon = ConfigurationManager.ConnectionStrings["connectionstring"];
   //     string constr = sqlCon.ConnectionString;
   //     SqlConnection con = new SqlConnection(constr);
    Company company = new Company();
    Employee employee = new Employee();
    Leave l = new Leave();
    SqlCommand cmd;
    Collection<Leave> LeaveList;
    Collection<Company> CompanyList;
    Collection<Employee> EmployeeList;
    Collection<Company> ddlBranchsList;
   
    string _Code;
    string s_login_role;
    int Employee_ID, reimburse_id, reimburse_id1, counter, hr_reimburse_id; 
    bool avail = false, temp_avail = false, check = true;
    string s_form = "";
    DataSet ds_userrights;

    protected void Page_Load(object sender, EventArgs e)
    {

        Image1.Visible = false;
        Image2.Visible = false;
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        l.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
       
        if (!IsPostBack)
        {
            String name = Request.QueryString["name"];
            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

                
                switch (s_login_role)
                {
                    case "a": 
                        break;

                    case "h":
                        if (Session["reimburse_id"] != null)
                        {
                            hr_reimburse_id = (int)Session["reimburse_id"];
                        }
                        hr();
                        BindGridData();
                        break;

                    case "e":
                        employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
                        emp();
                        BindGridData();
                        break;

                    case "u": //s_form = "5";
                        s_form = "100";
                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                       

                        break;

                    default: Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                        Response.Redirect("Company_Home.aspx");
                        break;
                }
            }
            else
            {
                Response.Cookies["Msg_Session"].Value = "Create Company";
                Response.Redirect("Company_Home.aspx");
            }
        }
    }  
    
    public void hr()
    {
        tbt_emp.Visible = false;
        load_lv();
    }

    public void emp()
    {
        Fileupload1.Visible = false;
            Fileupload2.Visible = false;
            Fileupload3.Visible = false;
            Fileupload4.Visible = false;
            Fileupload5.Visible = false;

            lv_hr.Visible = true;
            var sqlCon = ConfigurationManager.ConnectionStrings["connectionstring"];
            string constr = sqlCon.ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from reimbursement where pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "' and pn_EmployeeID = '" + employee.EmployeeId + "'", con);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            lv_hr.DataSource = ds;
            lv_hr.DataBind();
            con.Close();
        reimburse_notification_load();
    }

    public void load_lv()
    {
        lv_hr.Visible = true;
        var sqlCon = ConfigurationManager.ConnectionStrings["connectionstring"];
        string constr = sqlCon.ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from reimbursement where pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "'", con);
        SqlDataAdapter ada = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        ada.Fill(ds);
        lv_hr.DataSource = ds;
        lv_hr.DataBind();
        con.Close();
    }
    
    protected void ImageButton_send_Click(object sender, EventArgs e)
    {
        string emp_name = "";
        if (txtpurpose.Text == "")
            { txtpurpose.Text = "NULL"; }
        if (txtothers.Text =="")
            { txtothers.Text = "NULL"; }

        if (txtdest.Text == "" || txtdays.Text == "" || ddlmode.SelectedItem.Text == "Select" || txtexp.Text == "" || txtfrom.Text == "" || txtto.Text == "")
        {
            lblerror.Text = "Enter the Mandatory Fields";

        }
        else
        {
            
            var sqlCon = ConfigurationManager.ConnectionStrings["connectionstring"];
            string constr = sqlCon.ConnectionString;
            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("select Employee_First_Name from paym_employee where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID = '" + employee.BranchId + "' and pn_employeeID = '" + employee.EmployeeId + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                emp_name = reader[0].ToString();
            }
             reader.Close();
             con.Close();
             try
             {
                 Byte[] imgByte= null;
                 Byte[] imgByte2 = null;
                 Byte[] imgByte3 = null;
                 Byte[] imgByte4 = null;
                 Byte[] imgByte5 = null;

                 if ((Fileupload1.FileName != ""))
                 {
                     //to allow only jpg gif and png files to be uploaded.
                     FileUpload img = (FileUpload)Fileupload1;
                     if (img.HasFile && img.PostedFile != null)
                     {
                         //To create a PostedFile
                         HttpPostedFile File = Fileupload1.PostedFile;
                         //Create byte Array with file len
                         imgByte = new Byte[File.ContentLength];
                         //force the control to load data in array
                         File.InputStream.Read(imgByte, 0, File.ContentLength);
                     }
                 }

                 if ((Fileupload2.FileName != ""))
                 {
                     //to allow only jpg gif and png files to be uploaded.
                     FileUpload img = (FileUpload)Fileupload2;

                     if (img.HasFile && img.PostedFile != null)
                     {
                         //To create a PostedFile
                         HttpPostedFile File = Fileupload2.PostedFile;
                         //Create byte Array with file len
                         imgByte2 = new Byte[File.ContentLength];
                         //force the control to load data in array
                         File.InputStream.Read(imgByte2, 0, File.ContentLength);
                     }

                 }
                 else
                 {
                     imgByte2 = new Byte[0];
                 }
                 if ((Fileupload3.FileName != ""))
                 {
                     //to allow only jpg gif and png files to be uploaded.
                     FileUpload img = (FileUpload)Fileupload3;

                     if (img.HasFile && img.PostedFile != null)
                     {
                         //To create a PostedFile
                         HttpPostedFile File = Fileupload3.PostedFile;
                         //Create byte Array with file len
                         imgByte3 = new Byte[File.ContentLength];
                         //force the control to load data in array
                         File.InputStream.Read(imgByte3, 0, File.ContentLength);
                     }
                 }
                 else
                 {
                     imgByte3 = new Byte[0];
                 }
                 if ((Fileupload4.FileName != ""))
                 {
                     //to allow only jpg gif and png files to be uploaded.
                     FileUpload img = (FileUpload)Fileupload4;

                     if (img.HasFile && img.PostedFile != null)
                     {
                         //To create a PostedFile
                         HttpPostedFile File = Fileupload4.PostedFile;
                         //Create byte Array with file len
                         imgByte4 = new Byte[File.ContentLength];
                         //force the control to load data in array
                         File.InputStream.Read(imgByte4, 0, File.ContentLength);
                     }
                 }
                 else
                 {
                     imgByte4 = new Byte[0];
                 }
                 if ((Fileupload5.FileName != ""))
                 {
                     //to allow only jpg gif and png files to be uploaded.
                     FileUpload img = (FileUpload)Fileupload5;

                     if (img.HasFile && img.PostedFile != null)
                     {
                         //To create a PostedFile
                         HttpPostedFile File = Fileupload5.PostedFile;
                         //Create byte Array with file len
                         imgByte5 = new Byte[File.ContentLength];
                         //force the control to load data in array
                         File.InputStream.Read(imgByte5, 0, File.ContentLength);
                     }
                 }
                 else
                 {
                     imgByte5 = new Byte[0];
                 }
                 con.Open();
                 SqlCommand cmd2 = new SqlCommand("INSERT INTO paym_employee_bill_photo(pn_companyid,pn_branchid,pn_employeeid,from_date,to_date,billimage1,billimage2,billimage3,billimage4,billimage5) VALUES (@pn_companyid,@pn_branchid,@pn_employeeid,@from_date,@to_date,@billimage1,@billimage2,@billimage3,@billimage4,@billimage5)", con);
                 //cmd.CommandType = CommandType.StoredProcedure;
                 cmd2.Parameters.AddWithValue("@pn_companyid", employee.CompanyId);
                 cmd2.Parameters.AddWithValue("@pn_branchid", employee.BranchId);
                 cmd2.Parameters.AddWithValue("@pn_employeeid", employee.EmployeeId);
                 cmd2.Parameters.AddWithValue("@from_date", employee.Convert_ToSqlDatestring(txtfrom.Text));
                 cmd2.Parameters.AddWithValue("@to_date", employee.Convert_ToSqlDatestring(txtto.Text));
                 cmd2.Parameters.AddWithValue("@billimage1", imgByte);
                 cmd2.Parameters.AddWithValue("@billimage2", imgByte2);
                 cmd2.Parameters.AddWithValue("@billimage3", imgByte3);
                 cmd2.Parameters.AddWithValue("@billimage4", imgByte4);
                 cmd2.Parameters.AddWithValue("@billimage5", imgByte5);


                 int rows = cmd2.ExecuteNonQuery();
                 string string1 = "N";
                 //SqlCommand cmd2 = new SqlCommand("insert into paym_employee_bill_photo(pn_companyid,pn_branchid,pn_employeeid,from_date,to_date,billimage1,billimage2,billimage3,billimage4,billimage5)values('" + employee.CompanyId + "','" + employee.BranchId + "','" + employee.EmployeeId + "','" + employee.Convert_ToSqlDatestring(txtfrom.Text) + "','" + employee.Convert_ToSqlDatestring(txtto.Text) + "',)", con);
                 SqlCommand cmd1 = new SqlCommand("insert into reimbursement(pn_companyid,pn_branchid,pn_employeeid,pn_EmployeeName,from_date,to_date,total_days,mode,destination,expense,purpose,other,status,billimage1,billimage2,billimage3,billimage4,billimage5)VALUES (@pn_companyid,@pn_branchid,@pn_employeeid,@pn_EmployeeName,@from_date,@to_date,@total_days,@mode,@destination,@expense,@purpose,@other,@status,@billimage1,@billimage2,@billimage3,@billimage4,@billimage5)", con);
                 cmd1.Parameters.AddWithValue("@pn_companyid", employee.CompanyId);
                 cmd1.Parameters.AddWithValue("@pn_branchid", employee.BranchId);
                 cmd1.Parameters.AddWithValue("@pn_employeeid", employee.EmployeeId);
                 cmd1.Parameters.AddWithValue("@pn_EmployeeName", emp_name);
                 cmd1.Parameters.AddWithValue("@from_date", employee.Convert_ToSqlDatestring(txtfrom.Text));
                 cmd1.Parameters.AddWithValue("@to_date", employee.Convert_ToSqlDatestring(txtto.Text));
                 cmd1.Parameters.AddWithValue("@total_days", txtdays.Text);
                 cmd1.Parameters.AddWithValue("@mode", ddlmode.SelectedItem.Text);
                 cmd1.Parameters.AddWithValue("@destination", txtdest.Text);
                 cmd1.Parameters.AddWithValue("@expense", txtexp.Text);
                 cmd1.Parameters.AddWithValue("@purpose", txtpurpose.Text);
                 cmd1.Parameters.AddWithValue("@other", txtothers.Text);
                 cmd1.Parameters.AddWithValue("@status", string1);
                 cmd1.Parameters.AddWithValue("@billimage1", imgByte);
                 cmd1.Parameters.AddWithValue("@billimage2", imgByte2);
                 cmd1.Parameters.AddWithValue("@billimage3", imgByte3);
                 cmd1.Parameters.AddWithValue("@billimage4", imgByte4);
                 cmd1.Parameters.AddWithValue("@billimage5", imgByte5);

                 //cmd2.ExecuteNonQuery();

                 cmd1.ExecuteNonQuery();

                 lblerror.Text = "Sent Successfully";
                 con.Close();
                 emp();

             }
             catch (Exception ex)
             {
                 lblerror.Text = "Sending failed";

             }

        
        }
    }
   
    protected void lv_hr_ItemEditing(object sender, ListViewEditEventArgs e)
    {
        var sqlCon = ConfigurationManager.ConnectionStrings["connectionstring"];
        string constr = sqlCon.ConnectionString;        
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        Label lblid = (Label)lv_hr.Items[e.NewEditIndex].FindControl("lblBillId");
        SqlCommand cmd = new SqlCommand("update reimbursement set status='Y' where id='" + lblid.Text + "' and pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID = '" +employee.BranchId+ "'", con);        
        cmd.ExecuteNonQuery();        
        lblerror.Text = "Request Approved";
        load_lv();
    }
    protected void lv_hr_ItemCanceling(object sender, ListViewCancelEventArgs e)
    {
        
    }
    protected void lv_hr_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "Reject")
        {
            var sqlCon = ConfigurationManager.ConnectionStrings["connectionstring"];
            string constr = sqlCon.ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            Label lblid = (Label)e.Item.FindControl("lblid");
            SqlCommand cmd = new SqlCommand("update reimbursement set status='N' where id='" + lblid.Text + "' and pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID = '" + employee.BranchId + "'", con);
            cmd.ExecuteNonQuery();
            lblerror.Text = "Request Rejected";
            load_lv();
        }
        else if (e.CommandName == "View")
        {
            //MemoryStream memoryStream = new MemoryStream();
            Image1.Visible = true;
            Image2.Visible = true;
            var sqlCon = ConfigurationManager.ConnectionStrings["connectionstring"];
            string constr = sqlCon.ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            int RowIndex = int.Parse(e.CommandArgument.ToString());
           // employee.RequisitionCode = lv_approval.DataKeys[RowIndex].Value.ToString();
            string lblempid = lv_hr.DataKeys[RowIndex].Value.ToString();
            
            Label lblid = (Label)e.Item.FindControl("label8");
            Label lblid1 = (Label)e.Item.FindControl("label9");
            int id = Convert.ToInt32(lblempid);

            SqlCommand cmd = new SqlCommand("set dateformat dmy;Select billimage1 from reimbursement where id='" + lblempid + "' and from_date='" + lblid.Text + "'and to_date='" + lblid1.Text + "';set dateformat mdy", con);
           
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {

                Image1.ImageUrl = "~/Handler2.ashx?ImID=" + lblempid;
                Image2.ImageUrl = "~/Handler3.ashx?ImID=" + lblempid;
                //Image3.ImageUrl = "~/Handler3.ashx?ImID=" + lblempid;
                //Image4.ImageUrl = "~/Handler3.ashx?ImID=" + lblempid;
                //Image5.ImageUrl = "~/Handler3.ashx?ImID=" + lblempid;
                //Image2.ImageUrl = "~/Handler3.ashx?ImID=" + lblempid.Text;

  

            }
            dr.Close();
            con.Close();
        }
    }
           

                




                

            
    
    protected void lv_hr_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
      
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataitem = (ListViewDataItem)e.Item;
                int id = (int)DataBinder.Eval(dataitem.DataItem, "ID");
                if (id == hr_reimburse_id)
                {
                    HtmlTableRow cell = (HtmlTableRow)e.Item.FindControl("temp_id");
                    cell.Style.Add("background-color", "#A9A9A9");
                Session["reimburse_id"]=0;
                }
                else
                {
                   HtmlTableRow cell = (HtmlTableRow)e.Item.FindControl("temp_id");
                   cell.Style.Add("background-color", "#FFFFFF");
                }
            }
       
        
      
    }
    protected void txtdest_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtexp_TextChanged(object sender, EventArgs e)
    {

    }
    protected void but_clear_Click(object sender, EventArgs e)
    {
        txtdays.Text = "";
        txtdest.Text = "";
        txtexp.Text = "";
        txtothers.Text = "";
        txtpurpose.Text = "";
        ddlmode.SelectedIndex = 0;

    }
    protected void txtto_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime fdate, tdate;
            fdate = Convert.ToDateTime(txtfrom.Text);
            tdate = Convert.ToDateTime(txtto.Text);
            TimeSpan diff = tdate - fdate;
            txtdays.Text = diff.Days.ToString();
        }
        catch (Exception ex) { }
    }
    protected void txtfrom_TextChanged(object sender, EventArgs e)
    {
        
    }
    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    HttpFileCollection fileCollection = Request.Files;
    //    for (int i = 0; i < fileCollection.Count; i++)
    //    {
    //        HttpPostedFile uploadfile = fileCollection[i];
    //        string fileName = Path.GetFileName(uploadfile.FileName);
    //        if (uploadfile.ContentLength > 0)
    //        {
    //            uploadfile.SaveAs(Server.MapPath("~/hrimages/") + fileName);
    //            lblMessage.Text += fileName;
    //        }
    //    }
    //}
    //protected void txt_nobills_TextChanged(object sender, EventArgs e)
    //{
        
    //}
    protected void Btn_add_Click(object sender, EventArgs e)
    {
        int no = Convert.ToInt32(txt_nobills.Text);
        if (no == 1)
        {
            Fileupload1.Visible = true;
            Fileupload2.Visible = false;
            Fileupload3.Visible = false;
            Fileupload4.Visible = false;
            Fileupload5.Visible = false;
        }
        else if (no == 2)
        {
            Fileupload1.Visible = true;
            Fileupload2.Visible = true;
            Fileupload3.Visible = false;
            Fileupload4.Visible = false;
            Fileupload5.Visible = false;
        }
        else if (no == 3)
        {
            Fileupload1.Visible = true;
            Fileupload2.Visible = true;
            Fileupload3.Visible = true;
            Fileupload4.Visible = false;
            Fileupload5.Visible = false;
        }
        else if (no == 4)
        {
            Fileupload1.Visible = true;
            Fileupload2.Visible = true;
            Fileupload3.Visible = true;
            Fileupload4.Visible = true;
            Fileupload5.Visible = false;
        }
        else if (no == 5)
        {
            Fileupload1.Visible = true;
            Fileupload2.Visible = true;
            Fileupload3.Visible = true;
            Fileupload4.Visible = true;
            Fileupload5.Visible = true;
        }
        else
        {
            Response.Write("only Maximum of 5 bills are allowed");
        }

    }
    protected void lv_hr_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void gvImages_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    public void BindGridData()
    {
        Image1.Visible = false;
        //Image2.Visible = false;

        var sqlCon = ConfigurationManager.ConnectionStrings["connectionstring"];
        string constr = sqlCon.ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        SqlCommand command = new SqlCommand("SELECT pn_Employeeid,billimage1  from paym_employee_bill_photo", con);// new SqlCommand("SELECT pn_Employeeid,billimage1  from [paym_employee_bill_photo]", Con);
        SqlDataAdapter daimages = new SqlDataAdapter(command);
        DataTable dt = new DataTable();
        daimages.Fill(dt);
        //gvImages.DataSource = dt;
        //gvImages.DataBind();
        con.Close();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    public void  reimburse_notification_load()
    { 
        try
        
        {   
        var sqlCon = ConfigurationManager.ConnectionStrings["connectionstring"];
        string constr = sqlCon.ConnectionString;
        SqlConnection con = new SqlConnection(constr);
            con.Open();
            //int notification = 0;
            cmd = new SqlCommand("select count(*) from notification_reimburse ", con);
            int notification = Convert.ToInt32(cmd.ExecuteScalar());
            if (notification == 0)
            {
                cmd = new SqlCommand("select id,pn_employeeID from reimbursement where pn_companyID='" + employee.CompanyId + "' and pn_branchID='" + employee.BranchId + "' and status='N'", con);
                SqlDataReader drd0 = cmd.ExecuteReader();
                while (drd0.Read())
                {

                    reimburse_id = Convert.ToInt32(drd0["id"]);
                    Employee_ID =Convert.ToInt32(drd0["pn_employeeID"]);
                    //drd0.Close();

                    cmd = new SqlCommand("insert into notification_reimburse values ('" + employee.CompanyId + "','" + employee.BranchId + "','" + Employee_ID + "','" + reimburse_id + "')", con);
                    cmd.ExecuteNonQuery();
                }
                drd0.Close();

            }

            else
            {
                SqlCommand cmd5 = new SqlCommand("select id from reimbursement where status!='N'", con);
                SqlDataReader drd3 = cmd5.ExecuteReader();
                while (drd3.Read())
                {
                    int reimburse_id2 = Convert.ToInt32(drd3["id"]);
                    SqlCommand cmd6 = new SqlCommand("delete from notification_reimburse where reimbursement_id='" + reimburse_id2 + "' and pn_employeeID='" + employee.EmployeeId + "'", con);
                    cmd6.ExecuteNonQuery();
                }
                 cmd = new SqlCommand("select id,pn_employeeID from reimbursement where pn_companyID='" + employee.CompanyId + "' and pn_branchID='" + employee.BranchId + "' and status='N'", con);
                SqlDataReader drd = cmd.ExecuteReader();
                while (drd.Read())
                {
                    counter = 0;
                    reimburse_id = Convert.ToInt32(drd["id"]);
                    Employee_ID = Convert.ToInt32(drd["pn_employeeID"]);
                    SqlCommand cmd3 = new SqlCommand("select reimburse_id from notifications", con);
                    SqlDataReader drd1 = cmd3.ExecuteReader();
                    while (drd1.Read())
                    {
                        reimburse_id1 = Convert.ToInt32(drd1["reimburse_id"]);
                        if (reimburse_id == reimburse_id1)
                        {
                            counter = counter + 1;
                        }
                    }
                    drd1.Close();
                    if (counter == 0)
                    {
                        SqlCommand cmd1 = new SqlCommand("insert into notification_reimburse values ('" + employee.CompanyId + "','" + employee.BranchId + "','" + Employee_ID + "','" + reimburse_id + "')", con);
                        cmd1.ExecuteNonQuery();
                    }
                }



                drd.Close();


                drd.Close();
                con.Close();
            }
        }

        catch (Exception ex)
        {
           //lbl_Error.Text = "Error";
        }
    }
}


