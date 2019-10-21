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

public partial class Hrms_Master_Default4 : System.Web.UI.Page
{
    private SqlConnection _connection;
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    SqlDataReader rea;
    Company company = new Company();
    Employee employee = new Employee();

    Collection<Company> BranchsList;
    Collection<Employee> CategoryList;
    Collection<Company> CompanyList;

    GridView gv = new GridView();
    int company_Id, branch_Id, valid, temp_valid = 0, check;
    string _Value;
    string s_login_role;
    string s_form = "";
    DataSet ds_userrights;

    #region Variables
    string gvUniqueID = String.Empty;
    int gvNewPageIndex = 0;
    int gvEditIndex = -1;
    string gvSortExpr = String.Empty;
    private string gvSortDir
    {

        get { return ViewState["SortDirection"] as string ?? "ASC"; }

        set { ViewState["SortDirection"] = value; }

    }
    #endregion


    private SqlDataSource ChildDataSource(string strSlabId, string strSort)
    {
        string strQRY = "";
        SqlDataSource dsTemp = new SqlDataSource();
        dsTemp.ConnectionString = ConfigurationManager.AppSettings["Connectionstring"];
        strQRY = "SELECT [Promotion_benefit].[SlabID],[Promotion_benefit].[PromotionID]," +
                                "[Promotion_benefit].[allowance],[Promotion_benefit].[value] FROM [Promotion_benefit]" +
                                " WHERE [Promotion_benefit].[SlabID] = '" + strSlabId + "'" +
                                "UNION ALL " +
                                "SELECT '" + strSlabId + "','','','' FROM [Promotion_benefit] WHERE [Promotion_benefit].[SlabID] = '" + strSlabId + "'" +
                                "HAVING COUNT(*)=0 " + strSort;

        dsTemp.SelectCommand = strQRY;
        return dsTemp;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        lbl_Error.Text = "";
        Error.Text = "";

        if (!IsPostBack)
        {
            switch (s_login_role)
            {
                case "a": load();
                    break;

                case "h": hr();                  
                    
                    break;

                case "u": s_form = "13";
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
        
    }

    public void hr()
    {
        myConnection.Open();
        SqlDataAdapter ad1 = new SqlDataAdapter("SELECT * FROM promotion_slab where pn_BranchID ='" + employee.BranchId + "'and pn_CompanyID='" + employee.CompanyId + "'", myConnection);

        DataSet ds = new DataSet();
        ad1.Fill(ds, "promotion_slab");
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
        
        cmd1 = new SqlCommand("select * from paym_designation where BranchId='" + employee.BranchId + "' and pn_CompanyID = '" + employee.CompanyId + "'", myConnection);
        rea = cmd1.ExecuteReader();
        while (rea.Read())
        {
            DropDownList dept = (DropDownList)GridView1.FooterRow.FindControl("txtDesignation");
            dept.Items.Add(rea["v_DesignationName"].ToString());
        }
        rea.Close();
        cmd1 = new SqlCommand("select * from paym_grade where BranchId='" + employee.BranchId + "' and pn_CompanyID = '" + employee.CompanyId + "'", myConnection);
        rea = cmd1.ExecuteReader();
        while (rea.Read())
        {
            DropDownList grade = (DropDownList)GridView1.FooterRow.FindControl("txtGrade");
            grade.Items.Add(rea["v_GradeName"].ToString());
        }
        rea.Close();
        
        myConnection.Close();
    }   


    #region GridView1 Event Handlers
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridViewRow row = e.Row;
        string strSort = string.Empty;

        if (row.DataItem == null)
        {
            return; 
        }
        
        gv = (GridView)row.FindControl("GridView2");

        if (gv.UniqueID == gvUniqueID)
        {
            ClientScript.RegisterStartupScript(GetType(), "Expand", "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + ((DataRowView)e.Row.DataItem)["SlabID"].ToString() + "','one');</script>");
        }

        gv.DataSource = ChildDataSource(((DataRowView)e.Row.DataItem)["SlabID"].ToString(), strSort);
        gv.DataBind();

        cmd1 = new SqlCommand("select * from paym_earnings where pn_branchId='" + employee.BranchId + "' and pn_CompanyID = '" + employee.CompanyId + "' and c_regular = 'Y'", myConnection);
        rea = cmd1.ExecuteReader();
        while (rea.Read())
        {
            DropDownList earn = (DropDownList)gv.FooterRow.FindControl("txtallowance");
            earn.Items.Add(rea["v_EarningsName"].ToString());
        }
        rea.Close();

        LinkButton l = (LinkButton)e.Row.FindControl("linkDeleteCust");
        l.Attributes.Add("onclick", "javascript:return " +
        "confirm('Are you sure you want to delete this Slab " +
        DataBinder.Eval(e.Row.DataItem, "SlabID") + "')");
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            try
            {
                int slabid = 0;
                myConnection.Open();
                cmd1 = new SqlCommand("select top 1 SlabID from promotion_slab where pn_branchId='" + employee.BranchId + "' and pn_CompanyID = '" + employee.CompanyId + "' order by SlabID desc", myConnection);
                rea = cmd1.ExecuteReader();
                if (!rea.HasRows)
                {
                    slabid = 1;
                }
                else
                {
                    if (rea.Read())
                    {
                        slabid = Convert.ToInt32(rea[0]);
                        slabid = slabid + 1;
                    }
                }
                rea.Close();
                myConnection.Close();
                string strDesignation = ((DropDownList)GridView1.FooterRow.FindControl("txtDesignation")).Text;
                string strGrade = ((DropDownList)GridView1.FooterRow.FindControl("txtGrade")).Text;
                string strSQL = "";
                strSQL = "INSERT INTO Promotion_Slab (pn_CompanyID, pn_BranchID, SlabID, v_DesignationName, " +
                        "v_GradeName) VALUES ('" +employee.CompanyId+ "','" +employee.BranchId+ "'," + slabid + ",'" + strDesignation + "','" + strGrade + "')";

                SqlDataSource1.InsertCommand = strSQL;
                SqlDataSource1.Insert();
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert(' Added successfully ');</script>");
                hr();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message.ToString().Replace("'", "") + "');</script>");
            }
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       
        string strSlabID = ((Label)GridView1.Rows[e.RowIndex].FindControl("lblSlabID")).Text;
        string strSQL = "";

        try
        {
            strSQL = "DELETE from Promotion_slab WHERE SlabID = '" + strSlabID + "' and pn_CompanyID='" + employee.CompanyId + "' and pn_BranchID = '" + employee.BranchId + "'";
            strSQL = strSQL + "DELETE from Promotion_benefit WHERE SlabID = '" + strSlabID + "' and pn_CompanyID='" + employee.CompanyId + "' and pn_BranchID = '" + employee.BranchId + "'";
            SqlDataSource1.DeleteCommand = strSQL;
            SqlDataSource1.Delete();
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted successfully');</script>");
            hr();
        }
        catch (Exception ex)
        { }
    }

    protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        if (e.Exception != null)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + e.Exception.Message.ToString().Replace("'", "") + "');</script>");
            e.ExceptionHandled = true;
        }
    }
    #endregion

    #region GridView2 Event Handlers

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView gvTemp = (GridView)sender;
        gvUniqueID = gvTemp.UniqueID;
        gvNewPageIndex = e.NewPageIndex;
        GridView1.DataBind();
    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Addvalue")
        {
            try
            {
                int proid = 0;
                myConnection.Open();
                cmd1 = new SqlCommand("select top 1 PromotionID from promotion_benefit where pn_branchId='" + employee.BranchId + "' and pn_CompanyID = '" + employee.CompanyId + "' order by PromotionID desc", myConnection);
                rea = cmd1.ExecuteReader();
                if (!rea.HasRows)
                {
                    proid = 1;
                }
                else
                {
                    if (rea.Read())
                    {
                        proid = Convert.ToInt32(rea[0]);
                        proid = proid + 1;
                    }
                }
                rea.Close();
                myConnection.Close();
                GridView gvTemp = (GridView)sender;
                gvUniqueID = gvTemp.UniqueID;

                string strSlabID = gvTemp.DataKeys[0].Value.ToString();  
                string strallowance = ((DropDownList)gvTemp.FooterRow.FindControl("txtallowance")).Text;
                string strvalue = ((TextBox)gvTemp.FooterRow.FindControl("txtvalue")).Text;

                string strSQL = "";
                strSQL = "INSERT INTO Promotion_Benefit (pn_CompanyID, pn_BranchID, SlabID, " +
                        "PromotionID, allowance, value) VALUES ('" + employee.CompanyId + "','" + employee.BranchId + "','" + strSlabID + "'," + proid + ",'" + strallowance + "','" +
                        strvalue + "')";

                SqlDataSource1.InsertCommand = strSQL;
                SqlDataSource1.Insert();
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Added successfully');</script>");
                hr();                
                
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message.ToString().Replace("'", "") + "');</script>");
            }
        }
    }

    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridView gvTemp = (GridView)sender;
        gvUniqueID = gvTemp.UniqueID;       
        string strPromotionID = ((Label)gvTemp.Rows[e.RowIndex].FindControl("lblPromotionID")).Text;
        string strSQL = "";
        try
        {
            strSQL = "DELETE from Promotion_benefit WHERE PromotionID = " + strPromotionID + " and pn_CompanyID='" + employee.CompanyId + "' and pn_BranchID = '" + employee.BranchId + "'";
            SqlDataSource dsTemp = new SqlDataSource();
            dsTemp.ConnectionString = ConfigurationManager.AppSettings["Connectionstring"];
            dsTemp.DeleteCommand = strSQL;
            dsTemp.Delete();
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted successfully');</script>");
            hr();
        }
        catch(Exception ex) 
        { }
    }

    protected void GridView2_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        if (e.Exception != null)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + e.Exception.Message.ToString().Replace("'", "") + "');</script>");
            e.ExceptionHandled = true;
        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((DataRowView)e.Row.DataItem)["PromotionID"].ToString() == String.Empty) e.Row.Visible = false;
        }
    }

    #endregion

    
}