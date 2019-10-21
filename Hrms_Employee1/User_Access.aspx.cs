using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Hrms_Employee_User_role : System.Web.UI.Page
{
    RoleAccess strRoleAccessData;


    protected void Page_Load(object sender, EventArgs e)
    {
        
    }



    [WebMethod]
    public static List<Role> RoleRetrieve()
    {
        List<Role> roleListResult = new List<Role>();

        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);

        SqlCommand cmd = new SqlCommand("spr_role_retrieve", con);
        cmd.CommandType = CommandType.StoredProcedure;     
        con.Open();
        using (SqlDataReader sdr = cmd.ExecuteReader())
        {
            DataTable dt = new DataTable();
            dt.Load(sdr);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Role roleObject = new Role();
                    roleObject.RowNo = Convert.ToInt32(row["row_no"]);
                    roleObject.RoleId = Convert.ToInt32(row["role_id"]);
                    roleObject.RoleName = Convert.ToString(row["role_name"]);
                    roleListResult.Add(roleObject);
                }
            }
        }       
        con.Close();

        return roleListResult;

    }

    [WebMethod]
    public static List<RoleAccess> RoleAccessRetrieve(RoleAccess roleAccess)
    {
        List<RoleAccess> roleAccessListResult = new List<RoleAccess>();

        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);

        SqlCommand cmd = new SqlCommand("spr_role_acess_retrieve", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@i_a_role_id", roleAccess.RoleId);
        con.Open();
        using (SqlDataReader sdr = cmd.ExecuteReader())
        {
            DataTable dt = new DataTable();
            dt.Load(sdr);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    RoleAccess roleAccessObject = new RoleAccess();
                    roleAccessObject.MenuId = Convert.ToInt32(row["PK_Menu_Id"]);
                    roleAccessObject.MenuName = Convert.ToString(row["Menu_Name"]);
                    roleAccessObject.ViewVisible = Convert.ToBoolean(row["view_visible"]);
                    roleAccessObject.SaveVisible = Convert.ToBoolean(row["save_visible"]);
                    roleAccessObject.DeleteVisible = Convert.ToBoolean(row["delete_visible"]);
                    roleAccessObject.ViewChecked = Convert.ToBoolean(row["view_checked"]);
                    roleAccessObject.SaveChecked = Convert.ToBoolean(row["save_checked"]);
                    roleAccessObject.DeleteChecked = Convert.ToBoolean(row["delete_checked"]);
                    roleAccessListResult.Add(roleAccessObject);
                }
            }
        }
        return roleAccessListResult;
    }

    [WebMethod]
    public static int RoleAccessSave(RoleAccess strRoleAccessData)
    {      
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
        int res = 0;
        SqlCommand cmd = new SqlCommand("spr_role_access_save ", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@i_a_role_id", strRoleAccessData.RoleId);
        cmd.Parameters.AddWithValue("@s_a_role_access",strRoleAccessData.RoleAccessData);
        cmd.Parameters.AddWithValue("@i_a_log_user_id", strRoleAccessData.LogUserId);
        con.Open();
        using (SqlDataReader sdr = cmd.ExecuteReader())
        {
            DataTable dt = new DataTable();
            dt.Load(sdr);
           
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    RoleAccess roleAccessObject = new RoleAccess();
                    res = Convert.ToInt32(row["result"]);
                }
            }
        }
        return res;



    }



    public class Role
    {
        public int RowNo { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int UserId { get; set; }

        // Constructor
        public Role()
        {
            this.RowNo = 0;
            this.RoleId = -1;
            this.RoleName = string.Empty;

            this.UserId = -1;
        }
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

        public string RoleAccessData { get; set; }
        public string LogUserId { get; set; }

        public RoleAccess()
        {
            this.RoleId = 0;
            this.MenuId = 0;
            this.LogUserId = string.Empty;
            this.MenuName = string.Empty;
            this.RoleAccessData = string.Empty;
            this.ViewVisible = false;
            this.SaveVisible = false;
            this.DeleteVisible = false;
            this.ReminderVisible = false;
            this.ViewChecked = false;
            this.SaveChecked = false;
            this.DeleteChecked = false;
        }
    }
}