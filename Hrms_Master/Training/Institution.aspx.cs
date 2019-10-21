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
using System.IO;
using System.Drawing;

public partial class Hrms_Training_Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
   
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    SqlDataReader rea;

    Company company = new Company();
    Employee employee = new Employee();

    Collection<Company> BranchsList;
    Collection<Employee> InstitutionList;
    Collection<Employee> InstitutionName;
    Collection<Company> CompanyList;

    int company_Id, branch_Id;
    string _Value;
    string s_login_role;
    string s_form = "";
    DataSet ds_userrights;

    //
    int i = 0;
    byte[] bytearray;
    //


    protected void Page_Load(object sender, EventArgs e)
    {
        
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

       // btn_upd.Visible = false;
        //Button1.Visible = true;
        //Error.Text = "";

        if (!IsPostBack)
        {
            switch (s_login_role)
            {
                case "a": load_admin();
                    break;

                case "h": load1();
                    access();
                    break;

                case "u": s_form = "26";
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

    public void load_admin()
    {
        //branch_institution.Visible = false;
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from paym_branch", con);
        SqlDataAdapter ad = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        ad.Fill(ds, "paym_branch");

        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "branchname";
        DropDownList1.DataValueField = "pn_branchid";
        DropDownList1.DataBind();
        


        con.Close();      

        
        
    }

    public void load1()
    {
        DropDownList1.Visible = false;
        con.Open();
       
        //******************No population needed for trainer profile
             
        SqlCommand cmd1 = new SqlCommand("select * from trainer_profile1 where id=0", con);
        SqlDataAdapter ada = new SqlDataAdapter(cmd1);
        DataSet ds = new DataSet();
        ada.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gv_trainer.DataSource = ds.Tables[0];
            gv_trainer.DataBind();
            int columnCount = gv_trainer.Rows[0].Cells.Count;
            gv_trainer.Rows[0].Cells.Clear();
            gv_trainer.Rows[0].Cells.Add(new TableCell());
            gv_trainer.Rows[0].Cells[0].ColumnSpan = columnCount;
            gv_trainer.Rows[0].Cells[0].Text = "Add trainer profile..";
        }
        else
        {
            gv_trainer.DataSource = ds.Tables[0];
            gv_trainer.DataBind();
            gv_trainer.Focus();
           
        }
        con.Close();
        
    }

    public void access()
    {
       // _connection = con.fn_Connection();
       // _connection.Open();
        con.Open();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and section_view='No'",con);
        SqlDataReader rdrview = cmd.ExecuteReader();
        if (rdrview.Read())
        {
            //string accesserror = "Permission Restricted. Please Contact Administrator.";
            //MessageBox.Show(accesserror);
            Response.Redirect("~/Company_Home.aspx");
        }
        rdrview.Close();
        con.Close();
    }

    protected void load_mei()
    {

        //******************No population needed for trainer profile
       
            SqlCommand cmd_id = new SqlCommand("select id from institution_profile where ins_name='" + txtname.Text + "'", con);
            int temp = (int)cmd_id.ExecuteScalar();
            SqlCommand cmd1 = new SqlCommand("select * from trainer_profile1 where id='"+temp+"'", con);
            SqlDataAdapter ada = new SqlDataAdapter(cmd1);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            gv_trainer.DataSource = ds.Tables[0];
            gv_trainer.DataBind();
            gv_trainer.Focus();
           
    }
    

    protected bool fun_imgupload(string title, byte[] barray, int len, string imgtype)
    {
        int flag = 0;
        con.Open();
        try
        {
            SqlCommand cmd1 = new SqlCommand("select ins_name from Institution_Profile", con);
            SqlDataReader rdr = cmd1.ExecuteReader();
            while (rdr.Read())
            {
                if (txtname.Text == rdr[0].ToString())
                {
                    flag = 1;
                }
            }
            rdr.Close();
            SqlCommand cmd = new SqlCommand("insert into Institution_Profile(ins_name,imagedata,dept,address1,address2,city,country,email,state,phone,website,category,grade,certifications,awards,ins_branch,status,pn_companyid,pn_branchid) values('" + txtname.Text + "','" + barray + "','" + txtaddrs1.Text + "','" + imgtype + "','" + txtcity.Text + "','" + txtcountry.Text + "','" + txtemail.Text + "','" + txtstate.Text + "','" + txtphone.Text + "','" + txtweb.Text + "','" + txtcategory.Text + "','" + txtgrade.Text + "','" + txtcertification.Text + "','" + txtaward.Text + "','" + txtbranch.Text + "','y','" + employee.CompanyId + "','" + employee.BranchId + "') ", con);
            if (flag == 0)
            {
                cmd.ExecuteNonQuery();
            }
            else
            {
                //lbl1.Text = "Institute Name Already Exist";
            }
            return true;
        }
        catch (Exception e)
        {
            //lblname.Text = e.Message;
            return false;
        }
        finally
        {
            con.Close();
            //File.Delete(Server.MapPath("..\\Hrms_Master\\Training\\Logos") + id + "_1" + ".jpg");
        }


    }
    
    
    
     
    protected void gv_trainer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
        if (e.CommandName == "Insert")
        {
            int flag = 0;
            string fileName = FileUpload1.PostedFile.FileName;
            string relativePath = @"~\Hrms_Master\Training\Logos\" + fileName;
            FileUpload1.SaveAs(Server.MapPath(relativePath));
            //FileUpload1.PostedFile.SaveAs("~/Logos/" + fileName);
            Bitmap bm_image = new Bitmap(Server.MapPath(relativePath));
            MemoryStream strem = new MemoryStream();//Take memory strem
            System.Drawing.Image img =bm_image.GetThumbnailImage(100, 100, null, IntPtr.Zero);
         
            img.Save(strem, System.Drawing.Imaging.ImageFormat.Gif); //saving

            byte[] content = strem.ToArray();

            con.Open();
            //checking institute name already exist or not
            SqlCommand cmd_chk = new SqlCommand("select ins_name from Institution_Profile", con);
            SqlDataReader rdr = cmd_chk.ExecuteReader();
            while (rdr.Read())
            {
                if (txtname.Text == rdr[0].ToString())
                {
                    flag = 1;
                }
            }
            rdr.Close();
            

            //*******************************************************
                        
            if (flag == 0)
            {
                if (ddltype.SelectedIndex != 0)
                {

                    if (s_login_role == "a")
                    {
                        SqlCommand insert = new SqlCommand("insert into institution_profile(ins_name,type,imagedata,address1,address2,city,country,email,state,phone,website,category,grade,certifications,awards,Ins_branch,status,pn_companyid,pn_branchid) values ('" + txtname.Text + "','" + ddltype.SelectedItem.Text + "',@image,'" + txtaddrs1.Text + "','" + txtaddrs2.Text + "','" + txtcity.Text + "','" + txtcountry.Text + "','" + txtemail.Text + "','" + txtstate.Text + "','" + txtphone.Text + "','" + txtweb.Text + "','" + txtcategory.Text + "','" + txtgrade.Text + "','" + txtcertification.Text + "','" + txtaward.Text + "','" + txtbranch.Text + "','y'," + employee.CompanyId + ",'" + DropDownList1.SelectedItem.Value + "')", con);
                        //parameters
                        SqlParameter imageParameter = insert.Parameters.Add("@image", SqlDbType.Binary);
                        imageParameter.Value = content;
                        imageParameter.Size = content.Length;
                        //Executing
                        insert.ExecuteNonQuery();
                        //deleting the image from folder
                    }
                    else if (s_login_role == "h")
                    {
                        SqlCommand insert = new SqlCommand("insert into institution_profile(ins_name,type,imagedata,address1,address2,city,country,email,state,phone,website,category,grade,certifications,awards,Ins_branch,status,pn_companyid,pn_branchid) values ('" + txtname.Text + "','" + ddltype.SelectedItem.Text + "',@image,'" + txtaddrs1.Text + "','" + txtaddrs2.Text + "','" + txtcity.Text + "','" + txtcountry.Text + "','" + txtemail.Text + "','" + txtstate.Text + "','" + txtphone.Text + "','" + txtweb.Text + "','" + txtcategory.Text + "','" + txtgrade.Text + "','" + txtcertification.Text + "','" + txtaward.Text + "','" + txtbranch.Text + "','y'," + employee.CompanyId + "," + employee.BranchId + ")", con);
                        //parameters
                        SqlParameter imageParameter = insert.Parameters.Add("@image", SqlDbType.Binary);
                        imageParameter.Value = content;
                        imageParameter.Size = content.Length;
                        //Executing
                        insert.ExecuteNonQuery();
                        //deleting the image from folder
                    }
                }
                else
                {
                    lblerror.Text = "Select Institute Type";
                }
                

            }
                //inserting trainer profile
                
                //Get the ID from "institution_Profile" and insert the record in "trainer_profile" table
                SqlCommand cmd1 = new SqlCommand("select id from institution_profile where ins_name='" + txtname.Text + "'", con);
                int temp = (int)cmd1.ExecuteScalar();

                TextBox txtpname = (TextBox)gv_trainer.FooterRow.FindControl("txtpname");
                TextBox txtptype = (TextBox)gv_trainer.FooterRow.FindControl("txtptype");
                TextBox txtfname = (TextBox)gv_trainer.FooterRow.FindControl("txtfname");
                TextBox txtlname = (TextBox)gv_trainer.FooterRow.FindControl("txtlname");
                TextBox txtexp = (TextBox)gv_trainer.FooterRow.FindControl("txtexp");
                TextBox txtspe = (TextBox)gv_trainer.FooterRow.FindControl("txtspe");
                TextBox txtworktype = (TextBox)gv_trainer.FooterRow.FindControl("txtworktype");

                int rating = ((AjaxControlToolkit.Rating)gv_trainer.FooterRow.FindControl("Rating2")).CurrentRating;

                SqlCommand cmd2 = new SqlCommand("insert into trainer_profile1(id,fname,experience,specification,worktype,rating,ptype) values('" + temp + "','" + txtfname.Text + "','" + txtexp.Text + "','" + txtspe.Text + "','" + txtworktype.Text + "','" + rating + "','" + txtptype.Text + "')", con);
                try
                {
                    cmd1.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                    load_mei();
                    //lbl1.Text = "Added Successfully";
                    lblerror.Text = "";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Saved Successfully.');", true);
                }
                catch (Exception ex)
                {
                   // lbl1.Text = ex.Message;
                }
            
            con.Close();
        }
    }


    protected void gv_trainer_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gv_trainer_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        con.Open();
        if (s_login_role == "a")
        {
            employee.BranchId = Convert.ToInt32(DropDownList1.SelectedItem.Value);
        }
                    
        //******************No population needed for trainer profile

        SqlCommand cmd1 = new SqlCommand("select * from trainer_profile1 where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID = '" + employee.BranchId + "' and id=0 ", con);
        SqlDataAdapter ada = new SqlDataAdapter(cmd1);
        DataSet ds = new DataSet();
        ada.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gv_trainer.DataSource = ds.Tables[0];
            gv_trainer.DataBind();
            int columnCount = gv_trainer.Rows[0].Cells.Count;
            gv_trainer.Rows[0].Cells.Clear();
            gv_trainer.Rows[0].Cells.Add(new TableCell());
            gv_trainer.Rows[0].Cells[0].ColumnSpan = 1;
            gv_trainer.Rows[0].Cells[0].Text = "Add trainer profile..";
        }
        else
        {
            gv_trainer.DataSource = ds.Tables[0];
            gv_trainer.DataBind();
            gv_trainer.Focus();

        }
        con.Close();
    }
    protected void ddl_dept_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
