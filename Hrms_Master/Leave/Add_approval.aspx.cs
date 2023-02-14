using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using ePayHrms.Employee;
public partial class Hrms_Master_Leave_Add_approval : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Employee employee = new Employee();
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        employee.EmployeeId= Convert.ToInt32(Request.Cookies["userid"].Value);
    }
 
    [WebMethod]
    public static int FormSave(Add_approval Form)
    {        
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
        SqlCommand cmd = new SqlCommand("sp_save_Add_approval", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@form_id", Form.FormId);
        cmd.Parameters.AddWithValue("@form_name", Form.FormName);
        cmd.Parameters.AddWithValue("@Level", Form.Level);
        cmd.Parameters.AddWithValue("@time", Form.Time);
        cmd.Parameters.AddWithValue("@user_id", Form.Userid);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        return 1;
   }
  
    public static void clearr(Add_approval Form)
    {
        Form.FormName = "";
        Form.Level = 00;
        //Form.Time = 00;
        Form.Userid =0;
    }
    public class Add_approval
    {
        public int FormId { get; set; }
        public string FormName { get; set; }
        public int Level { get; set; }
        public TimeSpan Time { get; set; }
        public int Userid { get; set; }
    }

}