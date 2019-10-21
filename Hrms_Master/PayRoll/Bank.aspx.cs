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


public partial class Bank_Loan_Default : System.Web.UI.Page
{
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    //private SqlConnection _con;
    //ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    SqlDataReader rea;
    Company company = new Company();
    Employee employee = new Employee();

    PayRoll pay = new PayRoll();

    Collection<PayRoll> banklist;

   
    int company_Id, branch_Id, valid, temp_valid = 0, check;
    string _Value;
    string s_login_role;

    string s_form = "";
    DataSet ds_userrights;

    

    protected void Page_Load(object sender, EventArgs e)
    {
        
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        lbl_Error.Text = "";       

        if (!IsPostBack)
        {
            switch (s_login_role)
            {
                case "a": grid_load();
                    break;

                case "h": grid_load1();
                    break;

                case "u": s_form = "35";
                    ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                    if (ds_userrights.Tables[0].Rows.Count > 0)
                    {
                        grid_load();

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


            //grid_load();


        }      



        //if (!IsPostBack)
        //{
        //    banklist = b.fn_bank();
        //    GV.DataSource = banklist;
        //    GV.DataBind();
        //}

    }


    //protected void Edit(object sender, GridViewEditEventArgs e)
    //{

    //    pay.bankid = Convert.ToInt32(GV.DataKeys[e.NewEditIndex].Value);
    //    pay.bankname = ((HtmlInputText)GV.Rows[e.NewEditIndex].FindControl("grd_txtname")).Value;
    //    pay.bankcode = ((HtmlInputText)GV.Rows[e.NewEditIndex].FindControl("grd_txtcode")).Value;
    //    pay.status = 'Y';

    //    if (pay.bankcode != "" && pay.bankname != "")
    //    {
    //        check = name_validate(pay.bankcode);

    //        if (check == 0)
    //        {
    //            check = name_validate(pay.bankname);

    //            if (check == 0)
    //            {

    //                _Value = pay.bankupdate(pay);
    //                if (_Value != "1")
    //                {
    //                    lbl_Error.Text = "<font color=Blue>Updated Successfully</font>";


    //                    ((ImageButton)GV.Rows[e.NewEditIndex].FindControl("img_update")).Visible = true;
    //                    ((ImageButton)GV.Rows[e.NewEditIndex].FindControl("img_save")).Visible = false;
    //                    ((HtmlInputText)GV.Rows[e.NewEditIndex].FindControl("grd_txtname")).Disabled = true;
    //                    ((HtmlInputText)GV.Rows[e.NewEditIndex].FindControl("grd_txtcode")).Disabled = true;
    //                    grid_load();
                        
    //                }
    //                else
    //                {
    //                    lbl_Error.Text = "Data saved not successfully";
    //                }
    //            }
    //            else
    //            {
    //                ClientScriptManager manager = Page.ClientScript;
    //                manager.RegisterStartupScript(this.GetType(), "Call", "show_message1();", true);
    //                grid_load();
    //            }
    //        }
    //        else
    //        {
    //            ClientScriptManager manager = Page.ClientScript;
    //            manager.RegisterStartupScript(this.GetType(), "Call", "show_message();", true);
    //            grid_load();
    //        }
    //    }
    //    else
    //    {
    //        ClientScriptManager manager = Page.ClientScript;
    //        manager.RegisterStartupScript(this.GetType(), "Call", "show_Error();", true);
    //    }
        

    //}

    //protected void Update(object sender, GridViewUpdateEventArgs e)
    //{
    //    ((HtmlInputText)GV.Rows[e.RowIndex].FindControl("grd_txtcode")).Disabled = false;
    //    ((HtmlInputText)GV.Rows[e.RowIndex].FindControl("grd_txtname")).Disabled = false;
    //    ((ImageButton)GV.Rows[e.RowIndex].FindControl("img_save")).Visible = true;
    //    ((ImageButton)GV.Rows[e.RowIndex].FindControl("img_update")).Visible = false;
    //}

  
    public void grid_load()
    {
        SqlDataAdapter ad1 = new SqlDataAdapter("SELECT * FROM paym_bank where pn_BranchID = '" + employee.BranchId + "'", myConnection);

        DataSet ds1 = new DataSet();

        ad1.Fill(ds1, "paym_bank");


        if (ds1.Tables[0].Rows.Count == 0)
        {
            ds1.Tables[0].Rows.Add(ds1.Tables[0].NewRow());
            GridView1.DataSource = ds1;
            GridView1.DataBind();
            int columnCount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columnCount;
            GridView1.Rows[0].Cells[0].Text = "No Records Found..";
        }
        else
        {
            GridView1.DataSource = ds1;
            GridView1.DataBind();

        }

    }

    public void grid_load1()
    {
        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM paym_bank where pn_BranchID = '" + employee.BranchId + "'", myConnection);

        DataSet ds = new DataSet();

        ad.Fill(ds, "paym_bank");


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

    }

    //protected void btn_save_Click(object sender, EventArgs e)
    //{
    //     try
    //    {
    //        check = name_validate(txt_code.Value);
    //        if (check == 0)
    //        {
    //            check = name_validate(txt_name.Value);
    //            if (check == 0)
    //            {
    //                pay.bankid = 0;
    //                pay.bankcode = txt_code.Value;
    //                pay.bankname = txt_name.Value;
    //                pay.status = 'Y';
    //                pay.bankupdate(pay);
    //                grid_load();
    //                lbl_Error.Text = "<font color=Blue>Added Successfully</font>";
    //                txt_name.Value = "";
    //                txt_code.Value = "";
    //            }
    //            else
    //            {
    //                ClientScriptManager manager = Page.ClientScript;
    //                manager.RegisterStartupScript(this.GetType(), "Call", "show_message1();", true);
    //            }
    //        }
    //        else
    //        {
    //            ClientScriptManager manager = Page.ClientScript;
    //            manager.RegisterStartupScript(this.GetType(), "Call", "show_message();", true);
    //        }
    //}
    //catch (Exception ex)
    //{
    //    lbl_Error.Text = "<font color=Red>Error Occured</font>";
    //}
    //}

    public int name_validate(string m_name)
    {

        banklist = pay.fn_bank();

        if (banklist.Count > 0)
        {
            for (valid = 0; valid < banklist.Count; valid++)
            {

                if (banklist[valid].bankcode == m_name)
                {
                    temp_valid++;

                }

            }

        }
        return temp_valid;
    }

    public int name_validate1(string b_name)
    {

        banklist = pay.fn_bank();

        if (banklist.Count > 0)
        {
            for (valid = 0; valid < banklist.Count; valid++)
            {

                if (banklist[valid].bankname == b_name)
                {
                    temp_valid++;

                }

            }

        }
        return temp_valid;
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {

            check = name_validate(txt_code.Value);

            if (check == 0)
            {
                check = name_validate(txt_name.Value);

                if (check == 0)
                {
                    pay.bankid = 0;
                    pay.bankcode = txt_code.Value;
                    pay.bankname = txt_name.Value;
                    pay.status = 'Y';
                    pay.Branch_Name = txt_branchName.Value;
                    pay.Account_Type = txt_actype.SelectedItem.Text;
                    pay.Micr_Code = txt_micr.Value;
                    pay.Ifsc_Code = txt_ifsc.Value;
                    pay.Address = txt_addr.Text;
                    pay.others = txt_other.Text;
                    if (pay.bankcode == "" || pay.bankname == "" || pay.Branch_Name == "" || pay.Account_Type == "Select" || pay.Ifsc_Code == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter All Mandatory Fields');", true);
                        return;
                    }
                    _Value = pay.bankupdate(pay);
                    if (_Value != "1")
                    {
                        grid_load1();
                        lbl_Error.Text = "<font color=Black>Added Successfully</font>";
                        txt_name.Value = "";
                        txt_code.Value = "";
                        txt_actype.SelectedIndex = 0;
                        txt_addr.Text = "";
                        txt_branchName.Value = "";
                        txt_ifsc.Value = "";
                        txt_micr.Value = "";
                        txt_other.Text = "";
                    }
                    else
                    {
                        lbl_Error.Text = "Data Added not Successfully";
                    }
                }
                else
                {

                    ClientScriptManager manager = Page.ClientScript;
                    manager.RegisterStartupScript(this.GetType(), "Call", "show_message1();", true);
                }

            }
            else
            {

                ClientScriptManager manager = Page.ClientScript;
                manager.RegisterStartupScript(this.GetType(), "Call", "show_message();", true);
            }

        }
        catch (Exception ex)
        {
            lbl_Error.Text = "<font color=Red>Error Occured</font>";
        }
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        grid_load1();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // loop all data rows
            foreach (DataControlFieldCell cell in e.Row.Cells)
            {
                // check all cells in one row
                foreach (Control control in cell.Controls)
                {
                    // Must use LinkButton here instead of ImageButton
                    // if you are having Links (not images) as the command button.
                    ImageButton button = control as ImageButton;
                    if (button != null && button.CommandName == "Delete")
                        // Add delete confirmation
                        button.OnClientClick = "if (!confirm('Are you sure " +
                               "you want to delete this record?')) return;";
                }
            }
        }

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string ID = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lbl_bankcode")).Text;

        DeleteRecord(ID);
        grid_load1();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex; // turn to edit mode
        grid_load1();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow Gvrow = GridView1.Rows[e.RowIndex];
        //Label8.Text = Gvrow.ToString();
        if (Gvrow != null)
        {
            string bankcode = ((Label)Gvrow.FindControl("lbl_bankcode_edit")).Text;
            string bankname = ((TextBox)Gvrow.FindControl("txt_bankname_edit")).Text;
            string branchname = ((TextBox)Gvrow.FindControl("txt_branchname_edit")).Text;
            string account = ((TextBox)Gvrow.FindControl("txt_accounttype_edit")).Text;
            string micrcode = ((TextBox)Gvrow.FindControl("txt_micrcode_edit")).Text;
            string ifsccode = ((TextBox)Gvrow.FindControl("txt_ifsccode_edit")).Text;
            string address = ((TextBox)Gvrow.FindControl("txt_address_edit")).Text;
            string others = ((TextBox)Gvrow.FindControl("txt_others_edit")).Text;
            
            myConnection.Open();
            cmd = new SqlCommand("update paym_bank set v_BankName='" + bankname + "',Branch_Name='" + branchname + "',Account_Type='" + account + "', Micr_Code='" + micrcode + "', Ifsc_Code='" + ifsccode + "', Address='" + address + "', Others='" + others + "' where v_BankCode='" + bankcode + "'", myConnection);
            cmd.ExecuteNonQuery();
            myConnection.Close();
            lbl_Error.Text = "<font color=Black>Updated Successfully</font>";
            GridView1.EditIndex = -1; // turn to edit mode
            grid_load1();

        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void DeleteRecord(string ID)
    {

        string sqlStatement = "DELETE FROM paym_bank WHERE v_BankCode=@v_BankCode";
        try
        {
            myConnection.Open();
            SqlCommand cmd = new SqlCommand(sqlStatement, myConnection);
            cmd.Parameters.AddWithValue("@v_BankCode", ID);
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
}
