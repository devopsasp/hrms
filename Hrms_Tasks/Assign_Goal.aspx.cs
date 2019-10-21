using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ePayHrms.Company;
using ePayHrms.Employee;
using ePayHrms.Leave;
using System.Data.SqlClient;
using Ionic.Zip;
public partial class Hrms_Tasks_Assign_Goal : System.Web.UI.Page
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
    Collection<Employee> EmployeeList;
    string gid,id,filePath;
    string s_login_role;
    protected void Page_Load(object sender, EventArgs e)
    {
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        if (!Page.IsPostBack)
        {
            if (s_login_role == "r")
            {
                assign_goal();
                GridView1.FooterRow.Visible = true;
            }
            else if (s_login_role == "e")
            {
                assigned_goal();
                GridView1.FooterRow.Visible = false;
                
            }
            //string[] filePaths = Directory.GetFiles(Server.MapPath("~/Uploads/"));
            //List<ListItem> files = new List<ListItem>();
            //foreach (string filePath in filePaths)
            //{
            //    files.Add(new ListItem(Path.GetFileName(filePath), filePath));
            //}
        }
       
    }
    public void assigned_goal()
    {
        //ImageButton ib = (ImageButton)GridView1.FindControl("download");
        //ib.Visible = false;
        int crnt_id = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
        SqlDataAdapter adap = new SqlDataAdapter("select * from Goal_Assigning where pn_EmployeeID='" + crnt_id + "' and pn_BranchID='" + employee.BranchId + "'", myConnection);
        DataSet ds = new DataSet();
        adap.Fill(ds, "Goal_Assigning");

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
        
        for (int b = 0; b < GridView1.Rows.Count; b++)
        {
            ImageButton ub = (ImageButton)GridView1.Rows[b].Cells[9].FindControl("upload");
            ImageButton db = (ImageButton)GridView1.Rows[b].Cells[9].FindControl("download");
            ub.Visible = true;
            db.Visible = false;
            ImageButton up = (ImageButton)GridView1.Rows[b].Cells[9].FindControl("update");
            ImageButton cncl = (ImageButton)GridView1.Rows[b].Cells[9].FindControl("cancel");
            ImageButton del = (ImageButton)GridView1.Rows[b].Cells[9].FindControl("delete");
            ImageButton edt = (ImageButton)GridView1.Rows[b].Cells[9].FindControl("edit");
            up.Visible = false;
            cncl.Visible = false;
        }
    }
    public void assign_goal()
    {
        SqlDataAdapter adap = new SqlDataAdapter("select * from Goal_Assigning where pn_BranchID='" + employee.BranchId + "'", myConnection);
        DataSet ds = new DataSet();
        adap.Fill(ds, "Goal_Assigning");

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
        for (int b = 0; b < GridView1.Rows.Count; b++)
        {
          ImageButton ub =(ImageButton)GridView1.Rows[b].Cells[9].FindControl("upload");
          ImageButton db = (ImageButton)GridView1.Rows[b].Cells[9].FindControl("download");
          ub.Visible = false;
          db.Visible = true;
          ImageButton up = (ImageButton)GridView1.Rows[b].Cells[9].FindControl("update");
          ImageButton cncl = (ImageButton)GridView1.Rows[b].Cells[9].FindControl("cancel");
          ImageButton del = (ImageButton)GridView1.Rows[b].Cells[9].FindControl("delete");
          ImageButton edt = (ImageButton)GridView1.Rows[b].Cells[9].FindControl("edit");
          up.Visible = false;
          cncl.Visible = false;
        }
         DropDownList ddassi = (DropDownList)GridView1.FooterRow.FindControl("ddename");
        ddassi.Items.Clear();
        employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
        EmployeeList = employee.fn_getEmployeeReporting(employee);
        if (EmployeeList.Count > 0)
        {
            for (int c = -1; c < EmployeeList.Count; c++)
            {
                if (c == -1)
                {
                    ListItem emp_li = new ListItem();
                    emp_li.Text = "Select Employee";
                    emp_li.Value = "0";
                    ddassi.Items.Add(emp_li);
                }
                else
                {
                    ListItem emp_li = new ListItem();
                    emp_li.Text = EmployeeList[c].LastName;
                    emp_li.Value = EmployeeList[c].EmployeeId.ToString();
                    ddassi.Items.Add(emp_li);
                }
            }
        }

    }
    protected void ddgtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        myConnection.Open();
        string gtype = ((DropDownList)GridView1.FooterRow.FindControl("ddgtype")).Text;
        cmd = new SqlCommand("select * from Goal_Master where Goal_type='" + gtype + "' and pn_BranchId='" + employee.BranchId + "'", myConnection);
        rea = cmd.ExecuteReader();
        DropDownList dept = (DropDownList)GridView1.FooterRow.FindControl("ddgname");
        dept.Items.Clear();
        dept.Items.Add("Select");
        while (rea.Read())
        {
            
            dept.Items.Add(rea["Goal_name"].ToString());
        }
        rea.Close();

        myConnection.Close();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument);

            //GridViewRow row = GridView1.Rows[index];
            if (e.CommandName == "add")
            {
                string refr = ((TextBox)GridView1.FooterRow.FindControl("txtrefno")).Text;
                string ename = ((DropDownList)GridView1.FooterRow.FindControl("ddename")).SelectedItem.ToString();    
                string gtype = ((DropDownList)GridView1.FooterRow.FindControl("ddgtype")).Text;
                string gname = ((DropDownList)GridView1.FooterRow.FindControl("ddgname")).Text;
                string sdate = ((TextBox)GridView1.FooterRow.FindControl("txtsdate")).Text;
                string status = ((DropDownList)GridView1.FooterRow.FindControl("ddstatus")).Text;
                string cdate = ((TextBox)GridView1.FooterRow.FindControl("txtcdate")).Text;
                string cmnt = ((TextBox)GridView1.FooterRow.FindControl("txtcmnt")).Text;

                // string doc = ((TextBox)GridView1.FooterRow.FindControl("TextBox7")).Text;
                string eid = ((DropDownList)GridView1.FooterRow.FindControl("ddename")).SelectedValue;
                DateTime d1 = DateTime.Parse(sdate).Date;
                sdate = d1.ToString();
                string qry = @"select Goal_id from Goal_Master where Goal_name='" + gname + "' and Goal_type='"+ gtype+"'";
                SqlCommand com = new SqlCommand(qry, myConnection);
                myConnection.Open();
                SqlDataReader readr = com.ExecuteReader();
                if (readr.Read())
                {
                    gid = Convert.ToString(readr["Goal_id"]);
                }
                myConnection.Close();
                if (refr != "" || ename!= "" || gtype!= "Select" || gname != "Select"  || sdate != "" || status!="Select")
                {
                    AddNewRecord(refr,eid, ename,gtype,gid,gname,sdate,status,cdate,cmnt);
                }

                else
                {
                    lbl_error.Text = "Enter all the details";
                }
            }
            if (e.CommandName == "Uploaddoc")
            {
                

                modalhistory.Show();
            }
            if (e.CommandName == "Delete")
            {
                string ID = ((Label)GridView1.Rows[index].FindControl("Lblrefno")).Text;

                DeleteRecord(ID);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Assigned goal Deleted Successfully!');", true);
                assign_goal();
            }
            if (e.CommandName == "Edit")
            {
               // GridView1.EditIndex = ;// turn to edit mode  
               
                
            }
        }
        catch (Exception ex)
        {

              lbl_error.Text = "Enter all the details";
        }
    }
    private void AddNewRecord(string refr, string eid, string ename, string gtype,string gid, string gname, string sdate, string status, string cdate, string comments)
    {
        employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
        string query = @"set dateformat dmy;INSERT INTO Goal_Assigning (pn_CompanyID, pn_BranchID, pn_EmployeeID,Refer_id, Assigned_to,Assigned_by,Goal_type,Goal_id,Goal_name,start_date,status,completion_date,comments) VALUES('" + employee.CompanyId + "','" + employee.BranchId + "','" + eid + "','" + refr + "','" + ename + "','" + employee.EmployeeId + "','" + gtype + "','" + gid + "','" + gname + "','" + sdate + "','" + status + "','" + cdate + "','" + comments + "');set dateformat mdy";
        SqlCommand myCommand = new SqlCommand(query, myConnection);
        
        myConnection.Open();

        myCommand.ExecuteNonQuery();
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Goal Assigned successfully!');", true);

        myConnection.Close();

        assign_goal();

    }
    protected void txtsdate_TextChanged(object sender, EventArgs e)
    {
        string samp;
        DateTime today, dump;

        samp = ((TextBox)GridView1.FooterRow.FindControl("txtsdate")).Text;
        dump = DateTime.Parse(samp);
        today = DateTime.Now.Date;
        if (dump < today)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Date you selected is less than today.select again!');", true);
            ((TextBox)GridView1.FooterRow.FindControl("txtsdate")).Text = "";
            ((TextBox)GridView1.FooterRow.FindControl("txtsdate")).Focus();
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex; // turn to edit mode  
        //edit_Click();
        //DropDownList drpstat = (DropDownList)GridView1.Rows[e.NewEditIndex].FindControl("statedit");
        //drpstat.Focus();
        //drpstat.Items.Clear();
        //string refr = ((Label)GridView1.Rows[e.NewEditIndex].FindControl("lblstatedit")).Text.ToString();
        //drpstat.Items.Add(refr);
        //if (s_login_role == "h" || s_login_role == "r")
        //{

        //    // DropDownList drpstat = (DropDownList)e.Row.FindControl("statedit");
        //    assign_goal();
        //    if (refr == "In-Progress" || refr == "Completed")
        //    {
        //        drpstat.Items.Add("Re-open");
        //        drpstat.Items.Add("Hold");
        //        drpstat.Items.Add("Accepted");
        //    }
        //    else if (refr == "Hold")
        //    {
        //        drpstat.Items.Add("Re-open");
        //    }
        //    else
        //    {
        //        drpstat.Items.Add("Hold");
        //        // drpstat.Items.Add("Re-open");
        //    }
          
        //}
        //if (s_login_role == "e")
        //{
        //    assigned_goal();
        //    drpstat.Items.Add("In-Progress");
        //    drpstat.Items.Add("Completed");
        //}
        //drpstat.SelectedValue = refr;
        if (s_login_role == "r")
        {
            assign_goal();
            GridView1.FooterRow.Visible = true;
        }
        else if (s_login_role == "e")
        {
            assigned_goal();
            GridView1.FooterRow.Visible = false;

        }

    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        if (s_login_role == "h" || s_login_role == "r")
        {
            assign_goal();
        }
        if (s_login_role == "e")
        {
            assigned_goal();
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //string ID = ((Label)GridView1.Rows[e.RowIndex].FindControl("Lblrefno")).Text;
        //DeleteRecord(ID);
        //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Assigned goal Deleted Successfully!');", true);
        //assign_goal();
    }
    private void DeleteRecord(string ID)
    {

        string sqlStatement = "DELETE FROM Goal_Assigning WHERE Refer_id = @Refer";
        try
        {
            myConnection.Open();
            SqlCommand cmd = new SqlCommand(sqlStatement, myConnection);
            cmd.Parameters.AddWithValue("@Refer", ID);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Deletion Error:";
            msg += ex.Message;
            throw new Exception(msg);

        }
        finally
        {
            myConnection.Close();
        }
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow Gvrow = GridView1.Rows[e.RowIndex];
        if (Gvrow != null)
        {
            string refrEdit = ((Label)Gvrow.FindControl("refnoedit")).Text;
            string statEdit = ((DropDownList)Gvrow.FindControl("statedit")).Text;
            string cmntEdit = ((TextBox)Gvrow.FindControl("cmntedit")).Text;
            myConnection.Open();
            cmd = new SqlCommand("update Goal_Assigning set status='" + statEdit + "', comments='" + cmntEdit + "' where Refer_id='" + refrEdit + "'", myConnection);
            cmd.ExecuteNonQuery();
            myConnection.Close();
            GridView1.EditIndex = -1;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Assigned Goal updated Successfully!');", true);
            assign_goal();
        }
    }
    protected void upload_Click(object sender, ImageClickEventArgs e)
    {
        //ImageButton b = (ImageButton)sender;
        //GridViewRow row = (GridViewRow)b.NamingContainer;
        //if (row != null)
        //{
        //    ModalPopupExtender modal = (ModalPopUpExtender)row.FindControl("modalhistory");
        //    if (modal != null)
        //    {
        //        modal.Show();
        //    }
        //}
       // modalhistory.Show();
    }
    protected void edit_Click(object sender, ImageClickEventArgs e)
    {
         //GridView1.EditIndex = e.NewEditIndex;
        for (int b = 0; b < GridView1.Rows.Count; b++)
        {
            
            ImageButton up = (ImageButton)GridView1.Rows[b].Cells[9].FindControl("update");
            ImageButton cncl = (ImageButton)GridView1.Rows[b].Cells[9].FindControl("cancel");
            ImageButton del = (ImageButton)GridView1.Rows[b].Cells[9].FindControl("delete");
            ImageButton edt = (ImageButton)GridView1.Rows[b].Cells[9].FindControl("edit");
            up.Visible = true;
            cncl.Visible = true;
            del.Visible = false;
            edt.Visible = false;
        }
    }
    protected void update_Click(object sender, ImageClickEventArgs e)
    {
        for (int b = 0; b < GridView1.Rows.Count; b++)
        {

            ImageButton up = (ImageButton)GridView1.Rows[b].Cells[9].FindControl("update");
            ImageButton cncl = (ImageButton)GridView1.Rows[b].Cells[9].FindControl("cancel");
            ImageButton del = (ImageButton)GridView1.Rows[b].Cells[9].FindControl("delete");
            ImageButton edt = (ImageButton)GridView1.Rows[b].Cells[9].FindControl("edit");
            up.Visible = true;
            cncl.Visible = true;
            del.Visible = false;
            edt.Visible = false;
        }
    }
    protected void DownloadFiles(object sender, EventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int index = gvRow.RowIndex;
        string Assigned = ((Label)gvRow.FindControl("Lblename")).Text;
   
        myConnection.Open();
        cmd = new SqlCommand("Select * from goal_Assigning where Assigned_to='"+Assigned+"'", myConnection);
        rea = cmd.ExecuteReader();
        while (rea.Read())
        {
            id = rea["pn_EmployeeID"].ToString();
           
        }
        cmd = new SqlCommand("Select count(*) from uploads where state='New' and pn_EmployeeID='" + id + "'", myConnection);
        int count = (int)cmd.ExecuteScalar();
        if (count > 0)
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                zip.AddDirectoryByName("File_s");
                //foreach (GridViewRow row in GridView1.Rows)
                //{
                //    if ((row.FindControl("chkSelect") as CheckBox).Checked)
                //    {
                //        string filePath = (row.FindControl("lblFilePath") as Label).Text;
                //        zip.AddFile(filePath, "Files");
                //    }
                //}
                cmd = new SqlCommand("Select * from uploads where state='New' and pn_EmployeeID='" + id + "'", myConnection);
                rea = cmd.ExecuteReader();
                while (rea.Read())
                {
                    filePath = rea["filename"].ToString();
                    zip.AddFile(filePath, "File_s");
                    cmd1 = new SqlCommand("update uploads set state='opened' where filename='" + filePath + "' and pn_EmployeeID='" + id + "'", myConnection);
                    cmd1.ExecuteNonQuery();
                }
                rea.Close();
                myConnection.Close();


                //Response.Clear();
                //Response.BufferOutput = false;
                string zipName = Environment.GetEnvironmentVariable("USERPROFILE") + "\\Download\\" + String.Format(employee.EmployeeId + "_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HH"));
                //Response.ContentType = "application/zip";
                //Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                zip.Save(zipName);
                
                //Response.End();
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No files to download!');", true);
        }

    }
    protected void UploadMultipleFiles(object sender, EventArgs e)
    {
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

}