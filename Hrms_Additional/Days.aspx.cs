using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Hrms_Additional_Default2 : System.Web.UI.Page
{
    sample s = new sample();
    Collection<sample> emplist;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //emplist = s.ssupdate();
            //grid_check.DataSource = emplist;
            //grid_check.DataBind();
       }
        }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        s.aid = 0;
        s.cadays = Convert.ToDouble(txt_calc.Value);
        s.padays = Convert.ToDouble(txt_paid.Value);
        s.presdays = Convert.ToDouble(txt_pres.Value);
        s.fn_update(s);
        emplist = s.ssupdate();
        grid_check.DataSource = emplist;
        grid_check.DataBind();

    }
    protected void edit(object sender, GridViewEditEventArgs e)
    {
        s.aid = Convert.ToInt32(grid_check.DataKeys[e.NewEditIndex].Value);
        s.cadays = Convert.ToDouble(((HtmlInputText)grid_check.Rows[e.NewEditIndex].FindControl("grid_calc")).Value);
        s.padays = Convert.ToDouble(((HtmlInputText)grid_check.Rows[e.NewEditIndex].FindControl("grid_paid")).Value);
        s.presdays = Convert.ToDouble(((HtmlInputText)grid_check.Rows[e.NewEditIndex].FindControl("grid_pres")).Value);
        s.fn_update(s);
        ((HtmlInputText)grid_check.Rows[e.NewEditIndex].FindControl("grid_calc")).Disabled = true;
        ((HtmlInputText)grid_check.Rows[e.NewEditIndex].FindControl("grid_paid")).Disabled = true;
        ((HtmlInputText)grid_check.Rows[e.NewEditIndex].FindControl("grid_pres")).Disabled = true;
        ((ImageButton)grid_check.Rows[e.NewEditIndex].FindControl("img_edit")).Visible = true;
        ((ImageButton)grid_check.Rows[e.NewEditIndex].FindControl("img_update")).Visible = false;
    }
    protected void update(object sender, GridViewUpdateEventArgs e)
    {
        ((HtmlInputText)grid_check.Rows[e.RowIndex].FindControl("grid_calc")).Disabled = false;
        ((HtmlInputText)grid_check.Rows[e.RowIndex].FindControl("grid_paid")).Disabled = false;
        ((HtmlInputText)grid_check.Rows[e.RowIndex].FindControl("grid_pres")).Disabled = false;
        ((ImageButton)grid_check.Rows[e.RowIndex].FindControl("img_edit")).Visible = false;
        ((ImageButton)grid_check.Rows[e.RowIndex].FindControl("img_update")).Visible = true;
        //string str = e.RowIndex.ToString();
        //((HtmlInputText)grid_check.Rows[e.RowIndex].FindControl("grid_calc")).Attributes.Add("onfocusout", "modify(" + str + ");");

   }
    protected void delete(object sender, GridViewDeleteEventArgs e)
    {
        s.aid = Convert.ToInt32(grid_check.DataKeys[e.RowIndex].Value);
        s.cadays = Convert.ToDouble(((HtmlInputText)grid_check.Rows[e.RowIndex].FindControl("grid_calc")).Value);
        s.padays = Convert.ToDouble(((HtmlInputText)grid_check.Rows[e.RowIndex].FindControl("grid_paid")).Value);
        s.presdays = Convert.ToDouble(((HtmlInputText)grid_check.Rows[e.RowIndex].FindControl("grid_pres")).Value);
        s.fn_delete(s);
        emplist = s.ssupdate();
        grid_check.DataSource = emplist;
        grid_check.DataBind();

    }
    protected void grdTest_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        foreach (Control control in e.Row.Cells[0].Controls)
       {
           LinkButton DeleteButton = control as LinkButton;
           if (DeleteButton != null && DeleteButton.Text == "Delete")
           {
               DeleteButton.OnClientClick = "return(confirm('Are you sure you want to delete this record?'))";
           }
       }

   }

    }

