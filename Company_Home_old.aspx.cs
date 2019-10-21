using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using ePayHrms.Company;
using ePayHrms.Employee;
using ePayHrms.BE.Recruitment;
using ePayHrms.Candidate;

public partial class Hrms_Company_Default : System.Web.UI.Page
{
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    //private SqlConnection _con;
    //ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    SqlDataAdapter ada = new SqlDataAdapter();
    SqlDataAdapter ada1 = new SqlDataAdapter();
    ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();
    Collection<Company> CompanyList;
    Company company = new Company();
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    Candidate c = new Candidate();
    DropDownList ddl = new DropDownList();
    string s_login_role;
    string code;
    string id, _Value ="";

    protected void Page_Load(object sender, EventArgs e)
    {
        

        
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        c.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
      
        s_login_role = Request.Cookies["Login_temp_Role"].Value;

        if (!IsPostBack)
        {
            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {

                switch (s_login_role)
                {

                    case "a":
                        this.SiteMapDataSource1.Provider = SiteMap.Providers["Menu1Sitemap"];
                        hr();

                        break;

                    case "h":
                        this.SiteMapDataSource1.Provider = SiteMap.Providers["Menu3Sitemap"];
                        Chart_options();
                        hr();
                        break;

                    case "e":
                        this.SiteMapDataSource1.Provider = SiteMap.Providers["Menu2Sitemap"];
                        
                        break;

                    case "u":

                        break;
                    default:
                        Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                        Response.Redirect("../Hrms_Master/Common/Common_Home.aspx");
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        
    }

    public void hr()
    {
        attend_load();
        bday_load();
        annoucements_load();
        Chart_Load();
        Employee_Distribution();
    }

    public void Employee_Distribution()
    {
        myConnection.Open();
        ada1 = new SqlDataAdapter("select * from Vw_Dept_cout where Dep_count<>0", myConnection);
        cmd.ExecuteNonQuery();
        DataSet ds1 = new DataSet();
        ada1.Fill(ds1, "Vw_Dept_cout");
        Chart2.DataSource = ds1;
        Chart2.DataBind();
        Chart2.ChartAreas["ChartArea1"].AxisX.Enabled = AxisEnabled.False;
        Chart2.ChartAreas["ChartArea1"].AxisY.Enabled = AxisEnabled.False;
        myConnection.Close();
    }

    public void Chart_options()
    {
        Array ChartTypes = Enum.GetValues(typeof(SeriesChartType));
        Array Palette = Enum.GetValues(typeof(ChartColorPalette));

        foreach (var items in ChartTypes)
        {
            ddl_charttype.Items.Add(items.ToString());
        }
        foreach (var colors in Palette)
        {
            ddl_Palette.Items.Add(colors.ToString());
        }
        ddl_charttype.Items.Insert(0, "-------Select-------");
        ddl_charttype.SelectedItem.Text = "Pie";
        ddl_Palette.Items.Insert(0, "-------Select-------");
        ddl_Palette.SelectedItem.Text = "Excel";

    }

    public void Chart_Load()
    {
        int cc=0;
        myConnection.Open();
        cmd = new SqlCommand("delete from temp_chart", myConnection);
        cmd.ExecuteNonQuery();
        for (int c=1;c<=3; c++)
        {
            if (c == 1)
            {
                cmd = new SqlCommand("select count(*) from time_card where pn_branchId = '" + employee.BranchId + "' and pn_Companyid = '" + employee.CompanyId + "' and dates = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' and Status = 'XX'", myConnection);
                cc = (int)cmd.ExecuteScalar();
                cmd = new SqlCommand("insert into temp_chart values('" + employee.CompanyId + "','" + employee.BranchId + "','" + DateTime.Now.ToString("yyyy/MM/dd") + "','Present','" + cc.ToString() + "')", myConnection);
                cmd.ExecuteNonQuery();
            }
            if (c == 2)
            {
                cmd = new SqlCommand("select count(*) from time_card where pn_branchId = '" + employee.BranchId + "' and pn_Companyid = '" + employee.CompanyId + "' and dates = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' and Status = 'AA'", myConnection);
                cc = (int)cmd.ExecuteScalar();
                cmd = new SqlCommand("insert into temp_chart values('" + employee.CompanyId + "','" + employee.BranchId + "','" + DateTime.Now.ToString("yyyy/MM/dd") + "','Absent','" + cc.ToString() + "')", myConnection);
                cmd.ExecuteNonQuery();
            }
            if (c == 3)
            {
                cmd = new SqlCommand("select count(*) from time_card where pn_branchId = '" + employee.BranchId + "' and pn_Companyid = '" + employee.CompanyId + "' and dates = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' and Status = 'LL'", myConnection);
                cc = (int)cmd.ExecuteScalar();
                cmd = new SqlCommand("insert into temp_chart values('" + employee.CompanyId + "','" + employee.BranchId + "','" + DateTime.Now.ToString("yyyy/MM/dd") + "','Leave','" + cc.ToString() + "')", myConnection);
                cmd.ExecuteNonQuery();
            }
        }
        
        ada = new SqlDataAdapter("select * from temp_chart where pn_branchid='" + employee.BranchId + "' and dates='" + DateTime.Now.ToString("yyyy/MM/dd") + "'", myConnection);
        DataSet ds = new DataSet();
        ada.Fill(ds, "temp_chart");
        Chart1.DataSource = ds;
        Chart1.DataBind();
        myConnection.Close();
    }


    public void admin()
    {

    }
    public void attend_load()
    {
        myConnection.Open();
        SqlCommand cmd1 = new SqlCommand("select emp_code, emp_name from time_card where pn_branchID='" + employee.BranchId + "'and dates = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' and status='AA'", myConnection);
        SqlDataAdapter da = new SqlDataAdapter(cmd1);
        DataSet ds = new DataSet();
        da.Fill(ds,"time_card");
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            Grid_attend.DataSource = ds;
            Grid_attend.DataBind();
            int columnCount = Grid_attend.Rows[0].Cells.Count;
            Grid_attend.Rows[0].Cells.Clear();
            Grid_attend.Rows[0].Cells.Add(new TableCell());
            Grid_attend.Rows[0].Cells[0].ColumnSpan = columnCount;
            Grid_attend.Rows[0].Cells[0].Text = "Download and save today's attendance to update this section";
        }
        else
        {
            Grid_attend.DataSource = ds;
            Grid_attend.DataBind();
        }
        myConnection.Close();
    }

    public void bday_load()
    {
        //myConnection.Open();
        //SqlCommand cmd1 = new SqlCommand("set dateformat dmy;select employeecode+'-'+employee_first_name as emp_code, dateofbirth from paym_employee where pn_branchID='" + employee.BranchId + "'and dateofbirth between '" + DateTime.Now.ToString("dd/MM/yyyy") + "' and '" + DateTime.Now.AddDays(7) + "' and status='y';set dateformat mdy;", myConnection);
        //SqlDataAdapter da = new SqlDataAdapter(cmd1);
        //DataSet ds = new DataSet();
        //da.Fill(ds, "paym_employee");
        //if (ds.Tables[0].Rows.Count == 0)
        //{
        //    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
        //    Grid_bday.DataSource = ds;
        //    Grid_bday.DataBind();
        //    int columnCount = Grid_bday.Rows[0].Cells.Count;
        //    Grid_bday.Rows[0].Cells.Clear();
        //    Grid_bday.Rows[0].Cells.Add(new TableCell());
        //    Grid_bday.Rows[0].Cells[0].ColumnSpan = columnCount;
        //    Grid_bday.Rows[0].Cells[0].Text = "There's no upcoming birthdays";
        //}
        //else
        //{
        //    Grid_bday.DataSource = ds;
        //    Grid_bday.DataBind();
        //}
        //myConnection.Close();
    }

    public void annoucements_load()
    {
        try
        {
            Img_btn_publish.Visible = true;
            Grid_announcements.Visible = true;
            Img_btn_update.Visible = false;
            string Today_date1 = DateTime.Now.ToString("dd/MM/yyyy");
            myConnection.Open();
            SqlCommand comm = new SqlCommand("set dateformat dmy;select (CONVERT(CHAR(10),announcementid,120)+'-'+subject) as Announcements,announcementid  from announcements where date='" + Today_date1 + "' and pn_branchID='" + employee.BranchId + "';set dateformat mdy", myConnection);
            SqlDataAdapter da4 = new SqlDataAdapter(comm);
            DataSet ds4 = new DataSet();
            da4.Fill(ds4);
            if (ds4.Tables[0].Rows.Count == 0)
            {
                ds4.Tables[0].Rows.Add(ds4.Tables[0].NewRow());
                Grid_announcements.DataSource = ds4;
                Grid_announcements.DataBind();
                int columnCount = Grid_announcements.Rows[0].Cells.Count;
                Grid_announcements.Rows[0].Cells.Clear();
                Grid_announcements.Rows[0].Cells.Add(new TableCell());
                Grid_announcements.Rows[0].Cells[0].ColumnSpan = columnCount;
                Grid_announcements.Rows[0].Cells[0].Text = "No Records Found..";
            }
            else
            {
                Grid_announcements.DataSource = ds4;
                Grid_announcements.DataBind();
            }

        }
        catch (Exception ex)
        {
            lbl_Error.Text = "<font color=Red>Error Occured</font>";       
        }
        finally
        {
            myConnection.Close();
        }
    }

    protected void Grid_announcements_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
            e.Row.CssClass = "row";
    }
    protected void Grid_announcements_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "cmd")
        {
            int rowindex = int.Parse(e.CommandArgument.ToString());
            code = ((Label)Grid_announcements.Rows[rowindex].FindControl("lblid")).Text;
            string[] id1 = code.Split('-');
            id = id1[0];
            myConnection.Open();
            SqlCommand comm = new SqlCommand("select subject,Details from announcements where announcementid='" + id + "' and pn_BranchID='" + employee.BranchId + "'", myConnection);
            SqlDataReader rdr = comm.ExecuteReader();
            while (rdr.Read())
            {
                txt_subject.Text = rdr["subject"].ToString();
                Txt_details.Text = rdr["Details"].ToString();
                txt_id.Text = id;
            }
            rdr.Close();
            myConnection.Close();
            Img_btn_publish.Visible = false;
            Grid_announcements.Visible = false;
            Img_btn_update.Visible = true;
            img_btn_cancel.Visible = true;
        }
    }
    protected void Img_btn_update_Click(object sender, EventArgs e)
    {
        try
        {
            myConnection.Open();
            SqlCommand cmd = new SqlCommand("update announcements set subject='" + txt_subject.Text + "',details='" + Txt_details.Text + "' where announcementid='" + txt_id.Text + "'", myConnection);
            cmd.ExecuteNonQuery();
            myConnection.Close();
            annoucements_load();
            clear();
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "<font color=Red>Error Occured</font>";
        }
    }
    protected void img_btn_cancel_Click(object sender, EventArgs e)
    {
        annoucements_load();
        clear();
    }
    public void clear()
    {
        txt_subject.Text = "";
        txt_id.Text = "";
        Txt_details.Text = "";
    }

    protected void ddl_charttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Chart1.Series["Series1"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), ddl_charttype.SelectedItem.Text);
            Chart1.Series["Series1"].Palette = (ChartColorPalette)Enum.Parse(typeof(ChartColorPalette), ddl_Palette.SelectedItem.Text);
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Inclination = Convert.ToInt32(ddl_inclination.SelectedValue);
            Chart_Load();
            Employee_Distribution();
        }
        catch
        {
            lbl_Error.Text = "<font color=Red>Error Occured</font>";
        }
    }
    protected void ddl_Palette_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Chart1.Series["Series1"].Palette = (ChartColorPalette)Enum.Parse(typeof(ChartColorPalette), ddl_Palette.SelectedItem.Text);
            Chart1.Series["Series1"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), ddl_charttype.SelectedItem.Text);
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Inclination = Convert.ToInt32(ddl_inclination.SelectedValue);
            Chart_Load();
            Employee_Distribution();
        }
        catch
        {
            lbl_Error.Text = "<font color=Red>Error Occured</font>";
        }
    }
    protected void ddl_inclination_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Inclination = Convert.ToInt32(ddl_inclination.SelectedValue);
            Chart1.Series["Series1"].Palette = (ChartColorPalette)Enum.Parse(typeof(ChartColorPalette), ddl_Palette.SelectedItem.Text);
            Chart1.Series["Series1"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), ddl_charttype.SelectedItem.Text);
            Chart_Load();
            Employee_Distribution();
        }
        catch
        {
            lbl_Error.Text = "<font color=Red>Error Occured</font>";
        }
    }

    protected void Chk_3d_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (Chk_3d.Checked == true)
            {
                Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                Chart1.ChartAreas["ChartArea1"].Area3DStyle.Inclination = Convert.ToInt32(ddl_inclination.SelectedValue);
            }
            else
            {
                Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
            }
            
            Chart1.Series["Series1"].Palette = (ChartColorPalette)Enum.Parse(typeof(ChartColorPalette), ddl_Palette.SelectedItem.Text);
            Chart1.Series["Series1"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), ddl_charttype.SelectedItem.Text);
            Chart_Load();
            Employee_Distribution();
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "<font color=Red>Error Occured</font>";
        }
    }

    protected void Img_btn_publish_Click(object sender, EventArgs e)
    {
        try
        {
            myConnection.Open();
            employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            employee.Announcement_id1 = 0;
            string date3 = DateTime.Now.ToString("dd/MM/yyyy");
            employee.Date = Convert.ToDateTime(date3);
            employee.Subject1 = txt_subject.Text;
            employee.Announcement1 = Txt_details.Text;
            _Value = employee._Announcements(employee);

            if (_Value != "1")
            {
                lbl_Error.Text = "<font color=Blue>Added Successfully</font>";

            }
            else
            {
                lbl_Error.Text = "<font color=Red>Error Occured</font>";

            }
            myConnection.Close();
            annoucements_load();
            clear();


        }
        catch (Exception ex)
        {
            lbl_Error.Text = "<font color=Red>Error Occured</font>";
        }
    }
    protected void ddl_lblstyle_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
