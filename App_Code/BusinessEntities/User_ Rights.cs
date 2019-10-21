using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.ObjectModel;
using ePayHrms.Connection;
using ePayHrms.Company;


/// <summary>
/// Summary description for User__Rights
/// </summary>
///
namespace ePayHrms.User_authentication
{

public class User__Rights
{
    public User__Rights()
    {

    }

    private SqlConnection _Connection;
    ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();

    private int _companyid;    
    private string _username;   
    private char _status;   
    private string _EmpCode;
    private string _DepartmentName;
    private char _Role;
    private int _EmployeeID;
    private int _DepartmentID;
    private int _BranchID;
    private int _FormID;
    private DateTime _d_Date;
    private DateTime _F_Date;
    private DateTime _T_Date;  



    public int companyid
    {
        get { return _companyid; }
        set { _companyid = value; }
    }

    public string username
    {
        get { return _username; }
        set { _username = value; }
    }

    public char status
    {
        get { return _status; }
        set { _status = value; }
    }

    public string EmpCode
    {
        get { return _EmpCode; }
        set { _EmpCode = value; }
    }

    public string DepartmentName
    {
        get { return _DepartmentName; }
        set { _DepartmentName = value; }
    }

    public char Role
    {
        get { return _Role; }
        set { _Role = value; }
    }

    public int EmployeeID
    {
        get { return _EmployeeID; }
        set { _EmployeeID = value; }
    }

    public int DepartmentID
    {
        get { return _DepartmentID; }
        set { _DepartmentID = value; }
    }

    public int BranchID
    {
        get { return _BranchID; }
        set { _BranchID = value; }
    }

    public int FormID
    {
        get { return _FormID; }
        set { _FormID = value; }
    }


    public DateTime d_Date
    {
        get { return _d_Date; }
        set { _d_Date = value; }
    }


    public DateTime F_Date
    {
        get { return _F_Date; }
        set { _F_Date = value; }
    }



    public DateTime T_Date
    {
        get { return _T_Date; }
        set { _T_Date = value; }
    }

    
// -----------------------------------------------------------
    

    public void user_Creation(User__Rights s)
    {
        _Connection = Con.fn_Connection();
        SqlCommand cmd = new SqlCommand("sp_user_Creation", _Connection);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlParameter[] ispparam = new SqlParameter[9];
        ispparam[0] = new SqlParameter("@pn_companyid", SqlDbType.Int);
        ispparam[0].Value = s.companyid;
        ispparam[1] = new SqlParameter("@pn_branchid", SqlDbType.Int);
        ispparam[1].Value = s.BranchID;
        ispparam[2] = new SqlParameter("@pn_employeeid", SqlDbType.Int);
        ispparam[2].Value = s.EmployeeID;
        ispparam[3] = new SqlParameter("@username", SqlDbType.VarChar);
        ispparam[3].Value = s.username;
        ispparam[4] = new SqlParameter("@d_date", SqlDbType.DateTime);
        ispparam[4].Value = s.d_Date;
        ispparam[5] = new SqlParameter("@status", SqlDbType.Char);
        ispparam[5].Value = s.status;
        ispparam[6] = new SqlParameter("@role", SqlDbType.Char);
        ispparam[6].Value = s.Role;
        ispparam[7] = new SqlParameter("@employeecode", SqlDbType.VarChar);
        ispparam[7].Value = s.EmpCode;
        ispparam[8] = new SqlParameter("@Department", SqlDbType.VarChar);
        ispparam[8].Value = "Select";
        for (int i = 0; i < ispparam.Length; i++)
        {
            cmd.Parameters.Add(ispparam[i]);
        }
        _Connection.Open();
        cmd.ExecuteNonQuery();
        _Connection.Close();

    }


    public void user_Authentications(User__Rights s)
    {
        _Connection = Con.fn_Connection();
        SqlCommand cmd = new SqlCommand("sp_user_Authentications", _Connection);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlParameter[] ispparam = new SqlParameter[9];
        ispparam[0] = new SqlParameter("@pn_companyid", SqlDbType.Int);
        ispparam[0].Value = s.companyid;       
        ispparam[1] = new SqlParameter("@pn_employeeid", SqlDbType.Int);
        ispparam[1].Value = s.EmployeeID;
        ispparam[2] = new SqlParameter("@pn_FormID", SqlDbType.Int);
        ispparam[2].Value = s.FormID;
        ispparam[3] = new SqlParameter("@From_Date", SqlDbType.DateTime);
        ispparam[3].Value = s.F_Date;
        ispparam[4] = new SqlParameter("@To_Date", SqlDbType.DateTime);
        ispparam[4].Value = s.T_Date;
        ispparam[5] = new SqlParameter("@Cur_Date", SqlDbType.DateTime);
        ispparam[5].Value = s.d_Date;       
        ispparam[6] = new SqlParameter("@AH_Empcode", SqlDbType.VarChar);
        ispparam[6].Value = s.EmpCode;
        ispparam[7] = new SqlParameter("@AH_Role", SqlDbType.Char);
        ispparam[7].Value = s.Role;
        ispparam[8] = new SqlParameter("@status", SqlDbType.Char);
        ispparam[8].Value = s.status;

        for (int i = 0; i < ispparam.Length; i++)
        {
            cmd.Parameters.Add(ispparam[i]);
        }
        _Connection.Open();
        cmd.ExecuteNonQuery();
        _Connection.Close();

    }


    public Collection<User__Rights> fn_user_Authentications()
    {
        Collection<User__Rights> authenticationsList = new Collection<User__Rights>();
        _Connection = Con.fn_Connection();
        string _SqlString = "select * from user_Authentications";
        SqlCommand _SSDepartment = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Department = _SSDepartment.ExecuteReader();
        while (dr_Department.Read())
        {
            User__Rights ur = new User__Rights();

            ur.companyid = (int)dr_Department["pn_CompanyID"];           
            ur.EmployeeID= (int)dr_Department["pn_EmployeeID"];
            ur.FormID = (int)dr_Department["pn_FormID"];
            ur.F_Date = (DateTime)dr_Department["From_Date"];
            ur.T_Date = (DateTime)dr_Department["To_Date"];
            ur.d_Date = (DateTime)dr_Department["Cur_Date"];                       
            ur.EmpCode= Convert.IsDBNull(dr_Department["AH_Empcode"]) ? "" : (string)dr_Department["AH_Empcode"];
            ur.Role = Convert.ToChar(dr_Department["AH_Role"]);  

            authenticationsList.Add(ur);
        }
        return authenticationsList;
    }


    public Collection<User__Rights> fn_emp_user_Authentications(User__Rights us)
    {
        Collection<User__Rights> authenticationsList = new Collection<User__Rights>();
        _Connection = Con.fn_Connection();
        string _SqlString = "select * from user_Authentications where pn_EmployeeID=" + us.EmployeeID + " and status='Y'";
        SqlCommand _SSDepartment = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Department = _SSDepartment.ExecuteReader();
        while (dr_Department.Read())
        {
            User__Rights ur = new User__Rights();
            ur.companyid = (int)dr_Department["pn_CompanyID"];           
            ur.EmployeeID = (int)dr_Department["pn_EmployeeID"];
            ur.FormID = (int)dr_Department["pn_FormID"];
            ur.F_Date = (DateTime)dr_Department["From_Date"];
            ur.T_Date = (DateTime)dr_Department["To_Date"];
            ur.d_Date = (DateTime)dr_Department["Cur_Date"];
            ur.EmpCode = Convert.IsDBNull(dr_Department["AH_Empcode"]) ? "" : (string)dr_Department["AH_Empcode"];
            ur.Role = Convert.ToChar(dr_Department["AH_Role"]);

            authenticationsList.Add(ur);
        }
        return authenticationsList;
    }


    public Collection<User__Rights> fn_user_creation(int bid)
    {
        Collection<User__Rights> authenticationsList = new Collection<User__Rights>();
        _Connection = Con.fn_Connection();
        string _SqlString = "select * from user_Creation where status='Y' and pn_branchid='"+bid+"' order by username asc";
        SqlCommand _SSDepartment = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Department = _SSDepartment.ExecuteReader();
        while (dr_Department.Read())
        {
            User__Rights ur = new User__Rights();

            ur.companyid = (int)dr_Department["pn_companyid"];
            ur.BranchID = (int)dr_Department["pn_branchid"];
            ur.EmployeeID = (int)dr_Department["pn_employeeid"];
            ur.username = Convert.IsDBNull(dr_Department["username"]) ? "" : (string)dr_Department["username"];
            ur.d_Date = (DateTime)dr_Department["d_date"];
            ur.status = Convert.ToChar(dr_Department["status"]);
            ur.Role = Convert.ToChar(dr_Department["role"]);
            ur.EmpCode = Convert.IsDBNull(dr_Department["employeecode"]) ? "" : (string)dr_Department["employeecode"];
            ur.DepartmentName = Convert.IsDBNull(dr_Department["department"]) ? "" : (string)dr_Department["department"];
            authenticationsList.Add(ur);
        }
        return authenticationsList;
    }



    public Collection<User__Rights> fn_Student_Department(int bid)
    {
        Collection<User__Rights> authenticationsList = new Collection<User__Rights>();
        _Connection = Con.fn_Connection();
        string _SqlString = "select * from student_department where pn_branchid='" + bid + "' order by pn_DepartmentID asc";
        SqlCommand _SSDepartment = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Department = _SSDepartment.ExecuteReader();
        while (dr_Department.Read())
        {
            User__Rights ur = new User__Rights();

            ur.companyid = (int)dr_Department["pn_companyid"];
            ur.BranchID = (int)dr_Department["pn_branchid"];
            ur.DepartmentID = (int)dr_Department["pn_departmentid"];
            ur.DepartmentName = (string)dr_Department["DepartmentName"];

            authenticationsList.Add(ur);
        }
        return authenticationsList;
    }

    public Collection<User__Rights> fn_emp_user_creation(User__Rights us)
    {
        Collection<User__Rights> authenticationsList = new Collection<User__Rights>();
        _Connection = Con.fn_Connection();
        string _SqlString = "select * from user_Creation where pn_employeeid=" + us.EmployeeID + " and pn_branchID = '"+us.BranchID+"' and status='Y'";
        SqlCommand _SSDepartment = new SqlCommand(_SqlString, _Connection);
        _Connection.Open();
        SqlDataReader dr_Department = _SSDepartment.ExecuteReader();
        while (dr_Department.Read())
        {
            User__Rights ur = new User__Rights();

            ur.companyid = (int)dr_Department["pn_companyid"];
            ur.BranchID = (int)dr_Department["pn_branchid"];
            ur.EmployeeID = (int)dr_Department["pn_employeeid"];
            ur.username = Convert.IsDBNull(dr_Department["username"]) ? "" : (string)dr_Department["username"];
            ur.d_Date = (DateTime)dr_Department["d_date"];
            ur.status = Convert.ToChar(dr_Department["status"]);
            ur.Role = Convert.ToChar(dr_Department["role"]);
            ur.EmpCode = Convert.IsDBNull(dr_Department["employeecode"]) ? "" : (string)dr_Department["employeecode"];
            ur.DepartmentName = Convert.IsDBNull(dr_Department["department"]) ? "" : (string)dr_Department["department"];
            authenticationsList.Add(ur);
        }
        return authenticationsList;
    }


    public DataSet get_User_Access(string empid)
    {
        _Connection = Con.fn_Connection();

        string _sample = "select * from user_rights where pn_EmployeeID =" + empid + "";

        _Connection.Open();

        SqlDataAdapter _Ad_sample = new SqlDataAdapter(_sample, _Connection);

        DataSet _Ds_sample = new DataSet();

        _Ad_sample.Fill(_Ds_sample);

        _Connection.Close();

        return _Ds_sample;
    }


    public void delete_User_Access(int empid)
    {
        _Connection = Con.fn_Connection();
        string _sample = "delete from user_rights where pn_EmployeeID =" + empid + "";
        SqlCommand cmd = new SqlCommand(_sample, _Connection);
        _Connection.Open();
        cmd.ExecuteNonQuery();
        _Connection.Close();
    }

}





     






}
