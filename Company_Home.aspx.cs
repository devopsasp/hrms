using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Drawing;
using ePayHrms.Company;
using ePayHrms.Leave;
using ePayHrms.Employee;
using ePayHrms.BE.Recruitment;
using ePayHrms.Candidate;
using System.Web.Services;
using System.Collections.Generic;
using System.Linq;

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
    Collection<Leave> LeaveMasterList;
    Company company = new Company();
    Employee employee = new Employee();
    Leave leave = new Leave();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    Candidate c = new Candidate();
    DropDownList ddl = new DropDownList();
    string s_login_role;

    public string date { get; private set; }
    public string count1 { get; private set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        

        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        leave.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        leave.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        c.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);


        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        Chart_Leave();
        Employee_Distribution();
        attend_load();
        if (!IsPostBack)
        {
            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {

                switch (s_login_role)
                {

                    case "a":
                        this.SiteMapDataSource1.Provider = SiteMap.Providers["Menu1Sitemap"];
                       // hr();

                        break;

                    case "h":
                        //this.SiteMapDataSource1.Provider = SiteMap.Providers["Menu3Sitemap"];
                        txt_date.Text = DateTime.Now.ToShortDateString();
                        hr();
                        break;

                    case "d":
                        txt_date.Text = DateTime.Now.ToShortDateString();
                        hr();
                        break;

                    case "r":
                        txt_date.Text = DateTime.Now.ToShortDateString();
                        hr();
                        break;

                    case "e":
                        //this.SiteMapDataSource1.Provider = SiteMap.Providers["Menu2Sitemap"];
                        
                        break;

                    case "u":

                        break;
                    default:
                        Response.Cookies["Msg_Session"].Value = "Permission Restricted. Please Contact Administrator";
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
        Load_Values();
        bday_load();
        Chart_Load(employee.Convert_ToSqlDatestring(txt_date.Text));   
    }

    public void Employee_Distribution()
    {
        myConnection.Open();
        ada1 = new SqlDataAdapter(" select * from Vw_Dept_cout where Dep_count<>0", myConnection);
        DataSet ds1 = new DataSet();
        ada1.Fill(ds1, "Vw_Dept_cout");
        Chart2.DataSource = ds1;
        Chart2.DataBind();
        Chart2.ChartAreas["ChartArea1"].AxisX.Enabled = AxisEnabled.False;
        Chart2.ChartAreas["ChartArea1"].AxisY.Enabled = AxisEnabled.False;
        myConnection.Close();
    }

    public void Load_Values()
    {
        try
        {
            int leave = 0, holiday = 0;
            myConnection.Open();
            SqlCommand cmd = new SqlCommand("select count(*) from leave_apply where flag is null and pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID = '" + employee.BranchId + "'", myConnection);
            leave = (int)cmd.ExecuteScalar();
            lbl_leave.Text = leave.ToString();
            cmd = new SqlCommand("select sum(days) from paym_holiday where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID = '" + employee.BranchId + "'", myConnection);
            holiday = (int)cmd.ExecuteScalar();
            lbl_holiday.Text = holiday.ToString();
            myConnection.Close();
        }
        catch
        {

        }
    }

    public void Chart_Load(string date)
    {
        try
        {
            int cc = 0, oc = 0;
            string datee;
            DateTime temp;
            myConnection.Open();
            cmd = new SqlCommand("delete from temp_chart", myConnection);
            cmd.ExecuteNonQuery();
            for (int c = 1; c <= 5; c++)
            {
                if (c == 1)
                {
                    cmd = new SqlCommand("select count(*) from time_card where pn_branchId = '" + employee.BranchId + "' and pn_Companyid = '" + employee.CompanyId + "' and dates = '" + date + "' and Status = 'XX'", myConnection);
                    cc = (int)cmd.ExecuteScalar();
                    oc += cc;
                    cmd = new SqlCommand("insert into temp_chart values('" + employee.CompanyId + "','" + employee.BranchId + "','" + date + "','Present','" + cc.ToString() + "')", myConnection);
                    cmd.ExecuteNonQuery();
                }
                if (c == 2)
                {
                    cmd = new SqlCommand("select count(*) from time_card where pn_branchId = '" + employee.BranchId + "' and pn_Companyid = '" + employee.CompanyId + "' and dates = '" + date + "' and Status = 'AA'", myConnection);
                    cc = (int)cmd.ExecuteScalar();
                    oc += cc;
                    cmd = new SqlCommand("insert into temp_chart values('" + employee.CompanyId + "','" + employee.BranchId + "','" + date + "','Absent','" + cc.ToString() + "')", myConnection);
                    cmd.ExecuteNonQuery();
                }
                if (c == 3)
                {
                    cmd = new SqlCommand("select count(*) from time_card where pn_branchId = '" + employee.BranchId + "' and pn_Companyid = '" + employee.CompanyId + "' and dates = '" + date + "' and Status = 'LL'", myConnection);
                    cc = (int)cmd.ExecuteScalar();
                    oc += cc;
                    cmd = new SqlCommand("insert into temp_chart values('" + employee.CompanyId + "','" + employee.BranchId + "','" + date + "','Leave','" + cc.ToString() + "')", myConnection);
                    cmd.ExecuteNonQuery();
                }
                if (c == 4)
                {
                    cmd = new SqlCommand("select count(*) from time_card where pn_branchId = '" + employee.BranchId + "' and pn_Companyid = '" + employee.CompanyId + "' and dates = '" + date + "' and Status = 'DD'", myConnection);
                    cc = (int)cmd.ExecuteScalar();
                    oc += cc;
                    cmd = new SqlCommand("insert into temp_chart values('" + employee.CompanyId + "','" + employee.BranchId + "','" + date + "','On Duty','" + cc.ToString() + "')", myConnection);
                    cmd.ExecuteNonQuery();
                }
                if (c == 5)
                {
                    cmd = new SqlCommand("select count(*) from time_card where pn_branchId = '" + employee.BranchId + "' and pn_Companyid = '" + employee.CompanyId + "' and dates = '" + date + "' and Status = 'AX' or Status = 'XA'", myConnection);
                    cc = (int)cmd.ExecuteScalar();
                    oc += cc;
                    cmd = new SqlCommand("insert into temp_chart values('" + employee.CompanyId + "','" + employee.BranchId + "','" + date + "','HalfDay_Present','" + cc.ToString() + "')", myConnection);
                    cmd.ExecuteNonQuery();
                }
            }
            int count1 = 0;
            temp = Convert.ToDateTime(txt_date.Text);
            datee = temp.ToString("yyyy/MM/dd");
            cmd = new SqlCommand("select count(*) from time_card  where Dates='" + datee + "'and pn_branchid='" + employee.BranchId + "'", myConnection);
            int s = (int)cmd.ExecuteScalar();
            count1 = s;
            
            ada = new SqlDataAdapter("select * from temp_chart where pn_branchid='" + employee.BranchId + "' and dates='" + date + "'", myConnection);
            DataSet ds = new DataSet();
            ada.Fill(ds, "temp_chart");
            Chart1.DataSource = ds;
            Chart1.DataBind();
            if (oc == 0)
            {
                date = "No attendance Data Found";
            }
           
            //Chart1.ChartAreas["ChartArea1"].AxisX.Enabled = AxisEnabled.False;
            //Chart1.ChartAreas["ChartArea1"].AxisY.Enabled = AxisEnabled.False;
            Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
            Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Enabled = false;
            Chart1.ChartAreas["ChartArea1"].AxisY.Title = "Total No. of Employees"+" "+":" + count1.ToString();
            Chart1.Titles.Add(new Title(date, Docking.Top, new Font("Verdana", 8f, FontStyle.Bold), System.Drawing.Color.Black));
            myConnection.Close();
        }

        catch
        {

        }
    }
    //public int count()
    //{
    //    int count1 = 0;
    //    //myConnection.Open();
    //    cmd = new SqlCommand("select count(*) from temp_chart ", myConnection);
    //    int s =(int) cmd.ExecuteScalar();
    //    count1 = s;
    //    return count1;

    //}
    public void Chart_Leave()
    {
        try
        {
            int cc = 0;
            string month = Convert.ToString(DateTime.Now.Month);
            string year = Convert.ToString(DateTime.Now.Year);
            string fr_date = year + "/" + month + "/" + "01";
            LeaveMasterList = leave.fn_paym_leavelist(leave);
            myConnection.Open();
            cmd = new SqlCommand("delete from temp_chart_leave", myConnection);
            cmd.ExecuteNonQuery();

            for (int c = 0; c < LeaveMasterList.Count; c++)
            {
                cmd = new SqlCommand("select count(*) from time_card where pn_branchId = '" + employee.BranchId + "' and pn_Companyid = '" + employee.CompanyId + "' and dates between '" + fr_date + "' and '" + DateTime.Now.ToString("yyyy/MM/dd") + "' and leave_code = '" + LeaveMasterList[c].leaveName + "'", myConnection);
                cc = (int)cmd.ExecuteScalar();
                cmd = new SqlCommand("insert into temp_chart_leave values('" + employee.CompanyId + "','" + employee.BranchId + "','" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + LeaveMasterList[c].leaveName + "','" + cc.ToString() + "')", myConnection);
                cmd.ExecuteNonQuery();
            }

            ada = new SqlDataAdapter("select * from temp_chart_leave where pn_branchid='" + employee.BranchId + "' and dates='" + DateTime.Now.ToString("yyyy/MM/dd") + "'", myConnection);
            DataSet ds = new DataSet();
            ada.Fill(ds, "temp_chart_leave");
            Chart3.DataSource = ds;
            Chart3.DataBind();
            Chart3.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            Chart3.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
            Chart3.ChartAreas["ChartArea1"].AxisX.LabelStyle.Enabled = false;
            Chart3.ChartAreas["ChartArea1"].AxisY.Title = "Total No. of Employees";
           
            myConnection.Close();
        }
        catch
        {
        }
    }

    public void admin()
    {

    }
    public void attend_load()
    {
        myConnection.Open();
        SqlCommand cmd1 = new SqlCommand("select emp_code, emp_name,status from time_card where pn_branchID='" + employee.BranchId + "'and dates = '" + DateTime.Now.ToString("yyyy/MM/dd") + "'and status='AA'", myConnection);
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


    protected void txt_date_TextChanged(object sender, EventArgs e)
    {
        Chart_Load(employee.Convert_ToSqlDatestring(txt_date.Text));
    }

    [WebMethod]
    public static string RoleMenuRetrieve(int intRoleId, string strMenuCode)
    {
        List<RoleMenu> roleListResult = new List<RoleMenu>();
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);

        SqlCommand cmd = new SqlCommand("spr_role_menu_load", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@i_a_role_id", intRoleId);
        cmd.Parameters.AddWithValue("@s_a_menu_code", strMenuCode);
        con.Open();
        using (SqlDataReader sdr = cmd.ExecuteReader())
        {
            DataTable dt = new DataTable();
            dt.Load(sdr);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    RoleMenu roleObject = new RoleMenu();
                    roleObject.MenuId = Convert.ToInt32(row["pk_menu_id"]);
                    roleObject.MenuName = Convert.ToString(row["menu_name"]);
                    roleObject.MenuCode = Convert.ToString(row["menu_code"]);
                    roleObject.MenuLink = Convert.ToString(row["menu_link"]);
                    roleObject.ParentId = row["parent_id"] != DBNull.Value ? Convert.ToInt32(row["parent_id"]) : (int?)null;
                    roleListResult.Add(roleObject);
                }
            }
            int? i = null;
            var mainList = LoadChildMenu(roleListResult, i);
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();

            return jss.Serialize(mainList);
        }
    }
    public static List<RoleMenu> LoadChildMenu(List<RoleMenu> list, int? parent)
    {
        return list.Where(x => x.ParentId == parent).Select(x => new RoleMenu
        {
            MenuId = x.MenuId,
            MenuName = x.MenuName,
            MenuCode = x.MenuCode,
            MenuLink = x.MenuLink,
            ParentId = x.ParentId,
            lstMenu = LoadChildMenu(list, x.MenuId)
        }).ToList();
    }



    public class RoleAccess
    {
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public bool ViewVisible { get; set; }
        public bool SaveVisible { get; set; }
        public bool DeleteVisible { get; set; }
        public bool ReminderVisible { get; set; }
        public bool ViewChecked { get; set; }
        public bool SaveChecked { get; set; }
        public bool DeleteChecked { get; set; }
        public bool ReminderChecked { get; set; }
        public string RoleAccessData { get; set; }
        public int LogUserId { get; set; }
        public RoleAccess()
        {
            this.RoleId = 0;
            this.MenuId = 0;
            this.LogUserId = 0;
            this.MenuName = string.Empty;
            this.RoleAccessData = string.Empty;
            this.ViewVisible = false;
            this.SaveVisible = false;
            this.DeleteVisible = false;
            this.ReminderVisible = false;
            this.ViewChecked = false;
            this.SaveChecked = false;
            this.DeleteChecked = false;
            this.ReminderChecked = false;
        }
    }
    public class RoleMenu
    {
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuCode { get; set; }
        public string MenuLink { get; set; }
        public int? ParentId { get; set; }
        public List<RoleMenu> lstMenu { get; set; }

        // Constructor
        public RoleMenu()
        {
            this.RoleId = -1;
            this.MenuId = 0;
            this.MenuName = string.Empty;
            this.MenuCode = string.Empty;
            this.MenuLink = string.Empty;
            this.ParentId = null;
        }
    }

}
