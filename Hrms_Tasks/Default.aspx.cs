using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ePayHrms.Company;
using ePayHrms.Employee;
using ePayHrms.Leave;
using System.Data.SqlClient;
using Ionic.Zip;
public partial class Hrms_Tasks_Default : System.Web.UI.Page
{
    private SqlConnection _connection;
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);

    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    SqlDataReader rea;
    SqlDataAdapter ada = new SqlDataAdapter();
    //DataSet ds = new DataSet();
    Company company = new Company();
    Employee employee = new Employee();
    string s_login_role;
    protected void Page_Load(object sender, EventArgs e)
    {
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        if (!IsPostBack)
        {
            //string[] filePaths = Directory.GetFiles(Server.MapPath("~/Uploads/"));
            //List<ListItem> files = new List<ListItem>();
            //foreach (string filePath in filePaths)
            //{
            //    files.Add(new ListItem(Path.GetFileName(filePath), filePath));
            //}
        }
    }
    protected void UploadMultipleFiles(object sender, EventArgs e)
    {
        //foreach (HttpPostedFile postedFile in FileUpload1.PostedFiles)
        //{
        //    string fileName = Path.GetFileName(postedFile.FileName);
        //    postedFile.SaveAs(Server.MapPath("~/Uploads/") + fileName);
        //    string state = "New";
        //    string path = Server.MapPath("~/Uploads/") + fileName;
        //    myConnection.Open();

        //    cmd = new SqlCommand("insert into uploads(pn_CompanyID,pn_BranchID,pn_EmployeeID,filename,state) values('" + employee.CompanyId + "','" + employee.BranchId + "','" + employee.EmployeeId + "','" + path + "','" + state + "')", myConnection);
        //    rea = cmd.ExecuteReader();
        //    myConnection.Close();
        //}
        //lblSuccess.Text = string.Format("{0} files have been uploaded successfully.", FileUpload1.PostedFiles.Count);


         try
        {
            // Get the HttpFileCollection
            HttpFileCollection hfc = Request.Files;
            for (int i = 0; i < hfc.Count; i++)
            {
                HttpPostedFile hpf = hfc[i];              
                if (hpf.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(hpf.FileName);
                    hpf.SaveAs(Server.MapPath("~/Uploads/") +
                      Path.GetFileName(hpf.FileName));
                    string path = Server.MapPath("~/Uploads/") + fileName;
                    string state = "New";
                    myConnection.Open();
                    cmd = new SqlCommand("insert into uploads(pn_CompanyID,pn_BranchID,pn_EmployeeID,filename,state) values('" + employee.CompanyId + "','" + employee.BranchId + "','" + employee.EmployeeId + "','" + path + "','" + state + "')", myConnection);
                    rea = cmd.ExecuteReader();
                    myConnection.Close();
                    lblSuccess.Text = string.Format("files have been uploaded successfully.");
                }              
            }   
        }
        catch (Exception ex)
        {
            // Handle your exception here
        }
 

    }
    protected void DownloadFiles(object sender, EventArgs e)
    {
        myConnection.Open();
        cmd = new SqlCommand("Select count(*) from uploads where state='New' and pn_EmployeeID='" + employee.EmployeeId + "'", myConnection);
        int count =(int) cmd.ExecuteScalar();
        if (count > 0)
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                zip.AddDirectoryByName("Upload");
                //foreach (GridViewRow row in GridView1.Rows)
                //{
                //    if ((row.FindControl("chkSelect") as CheckBox).Checked)
                //    {
                //        string filePath = (row.FindControl("lblFilePath") as Label).Text;
                //        zip.AddFile(filePath, "Files");
                //    }
                //}
                cmd = new SqlCommand("Select * from uploads where state='New' and pn_EmployeeID='" + employee.EmployeeId + "'", myConnection);
                rea = cmd.ExecuteReader();
                while (rea.Read())
                {
                    string filePath = rea["filename"].ToString();
                    zip.AddFile(filePath, "Upload");
                    cmd1 = new SqlCommand("update uploads set state='opened' where filename='" + filePath + "' and pn_EmployeeID='" + employee.EmployeeId + "'", myConnection);
                    cmd1.ExecuteNonQuery();
                }
                rea.Close();
                myConnection.Close();
                Response.Clear();
                Response.BufferOutput = false;
                string zipName = String.Format(employee.EmployeeId + "_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HH"));
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                zip.Save(Response.OutputStream);
                Response.End();
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No files to download!');", true);
        }
        
    }
}