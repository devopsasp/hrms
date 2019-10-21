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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ePayHrms.Company;
using ePayHrms.Employee;
using ePayHrms.Leave;
using System.Windows.Forms;
using System.Data.SqlClient;

public partial class Hrms_Master_Appraisal_Default : System.Web.UI.Page
{
    private SqlConnection _connection;
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    Company company = new Company();
    Employee employee = new Employee();
    Leave l = new Leave();

    Collection<Leave> AppraisalList;
    Collection<Company> CompanyList;
    Collection<Employee> employeelist;

    int company_Id, branch_Id, valid, temp_valid = 0, check;
    string _Value;
    string s_login_role;
    string s_form = "", qry, subquery;
    DataSet ds_userrights;
    int index;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        

        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        l.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);

        s_login_role = Request.Cookies["Login_temp_Role"].Value;
       

        if (!IsPostBack)
        {
            
            btn_modify.Visible = false;
            chk_Certeria.Enabled = false;
            btn_assign.Visible = false;

            switch (s_login_role)
            {
                case "a": load_admin();
                    //tb_app1.Visible = false;
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
            for (int b = 0; b < grid_appraisal.Rows.Count; b++)
            {
                ((ImageButton)grid_appraisal.Rows[b].FindControl("img_update")).Visible = false;
            }
            ((System.Web.UI.Control)grid_appraisal.HeaderRow.FindControl("lbledit")).Visible = false;
        }
        rdredit.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=2 and  section_delete='No'", _connection);
        SqlDataReader rdrdel = cmd.ExecuteReader();
        if (rdrdel.Read())
        {
            // ((ImageButton)grid_Course.Rows[0].FindControl("img_update")).Visible = false;
            for (int a = 0; a < grid_appraisal.Rows.Count; a++)
            {
                ((ImageButton)grid_appraisal.Rows[a].FindControl("imgdel")).Visible = false;
            }
            ((System.Web.UI.Control)grid_appraisal.HeaderRow.FindControl("lbldel")).Visible = false;
        }
        rdrdel.Close();

    }

    public void load_admin()
    {
        _connection = con.fn_Connection();
        _connection.Open();
        SqlDataAdapter ad = new SqlDataAdapter("select * from paym_branch", _connection);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        //ddl_branch.DataTextField = "branchname";
        //ddl_branch.DataValueField = "pn_branchid";
        //ddl_branch.DataSource = ds;
        //ddl_branch.DataBind();
        //ddl_branch.Items.Insert(0, "Select Branch");
        
        _connection.Close();
    }


    public void load()
    {
        //ddl_branch.Visible = false;
        employeelist = employee.fn_Department(employee.BranchId);
        if (employeelist.Count > 0)
        {
            chk_Department.DataSource = employeelist;
            chk_Department.DataTextField = "DepartmentName";
            chk_Department.DataValueField = "DepartmentId";
            chk_Department.DataBind();

            grid_load();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Department Available!');", true);
        }
    }

    public void normal()
    {
        rdo_type.SelectedValue = "180";
        //chk_Department.SelectedValue = "";
        txt_appraisalname.Value = "";
        chk_Certeria.Enabled = false;
        for (int i = 0; i < chk_Department.Items.Count; i++)
        {
            chk_Department.Items[i].Selected = false;
        }

        for (int i = 0; i < chk_Certeria.Items.Count; i++)
        {
            chk_Certeria.Items[i].Selected = false;
        }
    }


    protected void Edit(object sender, GridViewEditEventArgs e)
    {
        l.CompanyID = employee.CompanyId;
        l.AppraisalID = Convert.ToInt32(grid_appraisal.DataKeys[e.NewEditIndex].Value);
        ViewState["AppraisalID"] = l.AppraisalID.ToString();
        
        subquery = "and a.pn_CompanyID=" + l.CompanyID + " and a.pn_AppraisalID=" + l.AppraisalID + "";
        AppraisalList = l.fn_Appraisaltype(subquery);
        if (AppraisalList.Count > 0)
        {
            rdo_type.SelectedValue = AppraisalList[0].MaxDays.ToString();
            chk_Department.SelectedValue = AppraisalList[0].Departmentid.ToString();
            txt_appraisalname.Value = AppraisalList[0].AppraisalName.ToString();
            chk_Department.Enabled = true;

            if (AppraisalList[0].Feedtype != "")
            {
                chk_Certeria.Enabled = true;
                for (int i = 0; i < chk_Certeria.Items.Count; i++)
                {
                    if (AppraisalList[0].Feedtype.ToString() == chk_Certeria.Items[i].Text)
                    {
                        chk_Certeria.Items[i].Selected = true;
                    }
                }
            }
            else
            {
                //chk_Certeria.SelectedItem.Selected = false;
                chk_Certeria.Enabled = false;
                for (int i = 0; i < chk_Certeria.Items.Count; i++)
                {
                        chk_Certeria.Items[i].Selected = false;   
                }
            }
            btn_modify.Visible = true;
            btn_add.Visible = false;
            chk_Department.Enabled = false;
        }

    }

    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        subquery = "and pn_CompanyID=" + l.CompanyID + " and pn_DepartmentID=" + ddl_department.SelectedValue + "";
        //subquery = "and pn_CompanyID=" + l.CompanyID + " and pn_DepartmentID in (" + ddl_department.SelectedValue + " ,0)";

        AppraisalList = l.fn_Appraisaltype(subquery);
        if (AppraisalList.Count > 0)
        {
            grid_appraisal.Visible = true;
            grid_appraisal.DataSource = AppraisalList;
            //grid_appraisal.DataKeyNames = "AppraisalID";
            grid_appraisal.DataBind();

            //Img_assign.Visible = true;

            //foreach (GridViewRow row in grid_appraisal.Rows)
            //{
            //    index = row.RowIndex;
            //    for (int i = 0; i < AppraisalList.Count; i++)
            //    {
            //        if (AppraisalList[i].Flag == "Y" && grid_appraisal.DataKeys[index].Value.ToString() == AppraisalList[i].AppraisalID.ToString())
            //        {
            //            ((HtmlInputCheckBox)grid_appraisal.Rows[index].FindControl("Chk_Grade")).Checked = true;
            //        }
            //    }
            //}
        }
        else
        {
            grid_appraisal.Visible = false;
            btn_assign.Visible = false;
        }
    }


    public void grid_load()
    {
        subquery = "and a.pn_branchid='"+employee.BranchId+"' and a.pn_CompanyID=" + l.CompanyID + " order by a.pn_Departmentid asc";// +" and pn_DepartmentID=" + ddl_department.SelectedValue + "";
        AppraisalList = l.fn_Appraisaltype(subquery);
        if (AppraisalList.Count > 0)
        {
            grid_appraisal.Visible = true;
            grid_appraisal.DataSource = AppraisalList;
            //grid_appraisal.DataKeyNames = "AppraisalID";
            grid_appraisal.DataBind();

            //Img_assign.Visible = true;
            //foreach (GridViewRow row in grid_appraisal.Rows)
            //{
            //    index = row.RowIndex;
            //    for (int i = 0; i < AppraisalList.Count; i++)
            //    {
            //        if (AppraisalList[i].Flag == "Y" && grid_appraisal.DataKeys[index].Value.ToString() == AppraisalList[i].AppraisalID.ToString())
            //        {
            //            ((HtmlInputCheckBox)grid_appraisal.Rows[index].FindControl("Chk_Grade")).Checked = false ;
            //        }
            //    }
            //}
        }
        else
        {
            grid_appraisal.Visible = false;
        }
    }

    protected void rdo_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdo_type.SelectedValue.ToString() == "360")
        {
            chk_Certeria.Enabled = true;
        }
        else
        {
            chk_Certeria.Enabled = false;
        }
    }
    protected void chk_Department_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grid_appraisal_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grid_appraisal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        SqlConnection mycon = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
        if (e.CommandName == "Delete")
        {
            try
            {
                //finding row index
                GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);//catching the row in which thhe link button is clicked.
                HtmlInputText lnkbtn = (HtmlInputText)gvrow.FindControl("txtgrid");
                string str = lnkbtn.Value;
                mycon.Open();
                SqlCommand cmd = new SqlCommand("delete from appraisal_type where v_AppraisalQues='" + str + "' and pn_branchid='" + employee.BranchId + "'", mycon);
                cmd.ExecuteNonQuery();
                load();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Deleted Successfully!');", true);
            }
            catch (Exception exc)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Cannot delete. Transaction Exists');", true);
            }
        }
        mycon.Close();     
    }
    protected void grid_appraisal_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }
    protected void grid_appraisal_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void ddl_branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (s_login_role == "a")
        {
            //employee.BranchId = Convert.ToInt32(ddl_branch.SelectedItem.Value);
        }
        load();
        //tb_app1.Visible = true;
    }


    protected void btn_add_Click(object sender, EventArgs e)
    {
        if (chk_Department.SelectedValue != "" && txt_appraisalname.Value != "")
        {
            if (rdo_type.SelectedValue == "180")
            {
                for (int i = 0; i < chk_Department.Items.Count; i++)
                {
                    if (chk_Department.Items[i].Selected == true)
                    {
                        qry = "insert into Appraisal_type (pn_CompanyID, pn_DepartmentID, Appraisal_type, v_AppraisalQues, Flag, status, pn_BranchID) values(" + employee.CompanyId + ", " + chk_Department.Items[i].Value + ", " + rdo_type.SelectedValue + ", '" + txt_appraisalname.Value + "','Y', 'Y','" + employee.BranchId + "')";
                        _Value = employee.fn_procappraisal(qry);
                    }
                }
            }
            else
            {
                for (int i = 0; i < chk_Department.Items.Count; i++)
                {
                    for (int j = 0; j < chk_Certeria.Items.Count; j++)
                    {
                        if (chk_Department.Items[i].Selected == true && chk_Certeria.Items[j].Selected == true)
                        {
                            qry = "insert into Appraisal_type (pn_CompanyID, pn_DepartmentID, Appraisal_type, v_AppraisalQues, Feed_type, Flag, status, pn_BranchID) values(" + employee.CompanyId + ", " + chk_Department.Items[i].Value + ", " + rdo_type.SelectedValue + ", '" + txt_appraisalname.Value + "', '" + chk_Certeria.Items[i].Text + "', 'N', 'Y', '" + employee.BranchId + "')";
                            _Value = employee.fn_procappraisal(qry);
                        }
                    }
                }
            }
            //_Value = employee.fn_procappraisal(qry) ;
            if (_Value == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Saved Successfully!');", true);
                normal();
                grid_load();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured');", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter all mandatory fields');", true);
        }
    }

    protected void btn_modify_Click(object sender, EventArgs e)
    {
        if (chk_Department.SelectedValue != "")
        {
            if (rdo_type.SelectedValue == "180")
            {
                for (int i = 0; i < chk_Department.Items.Count; i++)
                {
                    if (chk_Department.Items[i].Selected == true)
                    {
                        subquery = "update Appraisal_type set pn_DepartmentID=" + chk_Department.SelectedValue + ", Appraisal_type=" + rdo_type.SelectedValue + ", v_AppraisalQues='" + txt_appraisalname.Value + "' where pn_AppraisalID=" + ViewState["AppraisalID"].ToString() + "";
                        _Value = employee.fn_procappraisal(subquery);
                    }
                }
            }
            else
            {
                for (int i = 0; i < chk_Department.Items.Count; i++)
                {
                    for (int j = 0; j < chk_Certeria.Items.Count; j++)
                    {
                        if (chk_Department.Items[i].Selected == true && chk_Certeria.Items[j].Selected == true)
                        {
                            subquery = "update Appraisal_type set pn_DepartmentID=" + chk_Department.SelectedValue + ", Appraisal_type=" + rdo_type.SelectedValue + ", v_AppraisalQues='" + txt_appraisalname.Value + "', Feed_type='" + chk_Certeria.SelectedItem.Text + "' where pn_AppraisalID=" + ViewState["AppraisalID"].ToString() + "";
                            _Value = employee.fn_procappraisal(subquery);
                        }
                    }
                }
            }
            //_Value = employee.fn_procappraisal(subquery);
            if (_Value == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Updated Successfully');", true);
                chk_Department.Enabled = true;
                grid_load();
                normal();
                btn_add.Visible = true;
                btn_modify.Visible = false;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Updated Successfully');", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Select Department');", true);
        }
    }

    protected void btn_assign_Click(object sender, EventArgs e)
    {
        string aid = "";
        foreach (GridViewRow row in grid_appraisal.Rows)
        {
            index = row.RowIndex;

            if (((HtmlInputCheckBox)grid_appraisal.Rows[index].FindControl("Chk_Grade")).Checked == true)
            {
                if (aid != "")
                {
                    aid += ",";
                }
                aid += grid_appraisal.DataKeys[index].Value.ToString();
            }
        }

        qry = "update Appraisal_type set flag='N' where pn_DepartmentID=" + ddl_department.SelectedValue + "";
        employee.fn_reportbyid(qry);

        if (aid != "")
        {
            qry = "update Appraisal_type set flag='Y' where pn_AppraisalID in(" + aid + ")";
            _Value = employee.fn_procappraisal(qry);
            if (_Value == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Assigned Successfully');", true);
            }
        }
    }
}
