using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ePayHrms.Company;
using ePayHrms.Employee;
using ePayHrms.Leave;

public partial class Hrms_Master_Appraisal_Default : System.Web.UI.Page
{
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
        l.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);

        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        lbl_Error.Text = "";

        if (!IsPostBack)
        {

            btn_Update.Visible = false;
            ddl_type.Enabled = false;
            btn_Apply.Visible = false;

            switch (s_login_role)
            {
                case "a": load();
                    break;

                case "h": load();
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

    public void load()
    {
        employeelist = employee.fn_getDepartmentList();
        if (employeelist.Count > 0)
        {
            for (int ddl_i = -2; ddl_i < employeelist.Count; ddl_i++)
            {
                if (ddl_i == -2)
                {
                    ListItem list = new ListItem();

                    list.Text = "Select Department";
                    list.Value = "sd";
                    ddl_department.Items.Add(list);
                }
                else if (ddl_i == -1)
                {
                    ListItem list = new ListItem();

                    list.Text = "All Department";
                    list.Value = "0";
                    ddl_department.Items.Add(list);
                }
                else
                {
                    ListItem list = new ListItem();

                    list.Text = employeelist[ddl_i].DepartmentName;
                    list.Value = employeelist[ddl_i].DepartmentId.ToString();
                    ddl_department.Items.Add(list);
                }
            }
        }
        else
        {
            lbl_Error.Text = "No Department Available";
        }
    }

    protected void btn_add_Click(object sender, EventArgs e)
    {
        if (ddl_department.SelectedValue != "sd")
        {
            if (rdo_type.SelectedValue == "180")
            {
                qry = "insert into Appraisal_type (pn_CompanyID, pn_DepartmentID, Appraisal_type, v_AppraisalQues, Flag, status) values(" + employee.CompanyId + ", " + ddl_department.SelectedValue + ", " + rdo_type.SelectedValue + ", '" + txt_appraisalname.Value + "','N', 'Y')";
            }
            else
            {
                qry = "insert into Appraisal_type (pn_CompanyID, pn_DepartmentID, Appraisal_type, v_AppraisalQues, Feed_type, Flag, status) values(" + employee.CompanyId + ", " + ddl_department.SelectedValue + ", " + rdo_type.SelectedValue + ", '" + txt_appraisalname.Value + "', '" + ddl_type.SelectedItem.Text + "', 'N', 'Y')";
            }
            _Value = employee.fn_procappraisal(qry);
            if (_Value == "0")
            {
                lbl_Error.Text = "<font color=blue>Added Successfully</font>";
                normal();
                grid_load();
            }
            else
            {
                lbl_Error.Text = "Added not Successfully";
            }
        }
        else
        {
            lbl_Error.Text = "Select Department";
        }
    }

    public void normal()
    {
        rdo_type.SelectedValue = "180";
        //ddl_department.SelectedValue = "sd";
        txt_appraisalname.Value = "";
        ddl_type.Enabled = false;
    }


    protected void Edit(object sender, GridViewEditEventArgs e)
    {
        l.CompanyID = employee.CompanyId;
        l.AppraisalID = Convert.ToInt32(grid_appraisal.DataKeys[e.NewEditIndex].Value);
        ViewState["AppraisalID"] = l.AppraisalID.ToString();

        subquery = "and pn_CompanyID=" + l.CompanyID + " and pn_AppraisalID=" + l.AppraisalID + "";
        AppraisalList = l.fn_Appraisaltype(subquery);
        if (AppraisalList.Count > 0)
        {
            rdo_type.SelectedValue = AppraisalList[0].totalpoint.ToString();
            ddl_department.SelectedValue = AppraisalList[0].Departmentid.ToString();
            txt_appraisalname.Value = AppraisalList[0].AppraisalName.ToString();

            if (AppraisalList[0].Feedtype != "")
            {
                ddl_type.Enabled = true;
                ddl_type.SelectedItem.Text = AppraisalList[0].Feedtype.ToString();
            }
            else
            {
                ddl_type.SelectedItem.Text = "Select Type";
                ddl_type.Enabled = false;
            }
            btn_Update.Visible = true;
            btn_add.Visible = false;
            ddl_department.Enabled = false;
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

            btn_Apply.Visible = true;

            foreach (GridViewRow row in grid_appraisal.Rows)
            {
                index = row.RowIndex;
                for (int i = 0; i < AppraisalList.Count; i++)
                {
                    if (AppraisalList[i].Flag == "Y" && grid_appraisal.DataKeys[index].Value.ToString() == AppraisalList[i].AppraisalID.ToString())
                    {
                        ((HtmlInputCheckBox)grid_appraisal.Rows[index].FindControl("Chk_Grade")).Checked = true;
                    }
                }
            }
        }
        else
        {
            grid_appraisal.Visible = false;
            btn_Apply.Visible = false;
        }
    }

    protected void btn_Update_Click(object sender, EventArgs e)
    {
        if (ddl_department.SelectedValue != "sd")
        {
            if (rdo_type.SelectedValue == "180")
            {
                subquery = "update Appraisal_type set pn_DepartmentID=" + ddl_department.SelectedValue + ", Appraisal_type=" + rdo_type.SelectedValue + ", v_AppraisalQues='" + txt_appraisalname.Value + "' where pn_AppraisalID=" + ViewState["AppraisalID"].ToString() + "";
            }
            else
            {
                subquery = "update Appraisal_type set pn_DepartmentID=" + ddl_department.SelectedValue + ", Appraisal_type=" + rdo_type.SelectedValue + ", v_AppraisalQues='" + txt_appraisalname.Value + "', Feed_type='" + ddl_type.SelectedItem.Text + "' where pn_AppraisalID=" + ViewState["AppraisalID"].ToString() + "";
            }
            _Value = employee.fn_procappraisal(subquery);
            if (_Value == "0")
            {
                lbl_Error.Text = "<font color=blue>Updated Successfully</font>";
                ddl_department.Enabled = true;
                grid_load();
                normal();
                btn_add.Visible = true;
                btn_Update.Visible = false;
            }
            else
            {
                lbl_Error.Text = "Updated not Successfully";
            }
        }
        else
        {
            lbl_Error.Text = "Select Department";
        }
    }

    public void grid_load()
    {
        subquery = "and pn_CompanyID=" + l.CompanyID + " and pn_DepartmentID=" + ddl_department.SelectedValue + "";
        AppraisalList = l.fn_Appraisaltype(subquery);
        if (AppraisalList.Count > 0)
        {
            grid_appraisal.Visible = true;
            grid_appraisal.DataSource = AppraisalList;
            //grid_appraisal.DataKeyNames = "AppraisalID";
            grid_appraisal.DataBind();

            foreach (GridViewRow row in grid_appraisal.Rows)
            {
                index = row.RowIndex;
                for (int i = 0; i < AppraisalList.Count; i++)
                {
                    if (AppraisalList[i].Flag == "Y" && grid_appraisal.DataKeys[index].Value.ToString() == AppraisalList[i].AppraisalID.ToString())
                    {
                        ((HtmlInputCheckBox)grid_appraisal.Rows[index].FindControl("Chk_Grade")).Checked = true;
                    }
                }
            }
        }
    }

    protected void btn_Apply_Click(object sender, EventArgs e)
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
                lbl_Error.Text = "<font color=blue>Assigned Successfully</font>";
            }
        }
    }
    protected void rdo_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdo_type.SelectedValue.ToString() == "360")
        {
            ddl_type.Enabled = true;
        }
        else
        {
            ddl_type.Enabled = false;
        }
    }
}
