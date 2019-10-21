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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ePayHrms.Company;
using ePayHrms.Employee;        
/// <summary>
/// Summary description for sample
/// </summary>
public class sample
{
	public sample()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private SqlConnection _connection;
    public SqlConnection fn_con()
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.AppSettings["connectionstring"];
        return con;
}
private int _eid;

public int eid
{
    get { return _eid; }
    set { _eid = value; }
}
	
private string _empid;

public string empid
{
    get { return _empid; }
    set { _empid = value; }
}
private string _empname;

public string empname
{
    get { return _empname; }
    set { _empname = value; }
}

    private int _aid;
    private double _cadays;
    private double _padays;
    private double _presdays;
    public int aid
    {
        get { return _aid; }
        set { _aid = value; }
    }    
    public double cadays
    {
        get { return _cadays; }
        set { _cadays = value; }        
    }
    public double padays
    {
        get { return _padays; }
        set { _padays = value; }
    }
    public double presdays
    {
        get { return _presdays; }
        set { _presdays = value; }
    }
    private int _EmployeeId;

    public int EmployeeId
    {
        get { return _EmployeeId; }
        set { _EmployeeId = value; }
    }
    private string _username;

    public string username
    {
        get { return _username; }
        set { _username = value; }
    }
    private string _password;

    public string password
    {
        get { return _password; }
        set { _password = value; }
    }

    private DateTime _date;

    public DateTime date
    {
        get { return _date; }
        set { _date = value; }
    }

    public string update(sample s)
    {
        try
        {
            _connection = fn_con();
            SqlCommand cmd = new SqlCommand("sp_a", _connection);
            //cmd.CommandType = "SELECT @@DATEFIRST AS '1st Day', DATEPART(dw, GETDATE())"; 
            SqlParameter[] isparam = new SqlParameter[3];            
            isparam[0] = new SqlParameter("userid", SqlDbType.VarChar);
            isparam[0].Value = s.username;
            isparam[1] = new SqlParameter("password", SqlDbType.VarChar);
            isparam[1].Value = s.password;
            isparam[2] = new SqlParameter("calcdays", SqlDbType.Int);
            isparam[2].Value = s.cadays;
            for (int i = 0; i < isparam.Length; i++)
            {
                cmd.Parameters.Add(isparam[i]);
            }
            _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
        }
        catch (Exception ex)
        {
           
                                   
        }
        return "saved";

    }
    


	
	
    public void fn_update1(sample s)
    {
        _connection = fn_con();
        SqlCommand cmd = new SqlCommand("sp_a", _connection);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlParameter[] isparam = new SqlParameter[3];
        isparam[0] = new SqlParameter("@aid", SqlDbType.Int);
        isparam[0].Value = s.aid;
        isparam[1] = new SqlParameter("@username", SqlDbType.VarChar);
        isparam[1].Value = s.username;
        isparam[2] = new SqlParameter("@password", SqlDbType.VarChar);
        isparam[2].Value = s.password;
        
        for (int i = 0; i < isparam.Length; i++)
        {
            cmd.Parameters.Add(isparam[i]);

        }
        _connection.Open();
        cmd.ExecuteNonQuery();
        _connection.Close();
    }
    public Collection<sample> supdate()
    {
        Collection<sample> slist = new Collection<sample>();
        _connection = fn_con();
        SqlCommand cmd = new SqlCommand("select * from a", _connection);
        _connection.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            sample s = new sample();
            s.aid = (int)dr["aid"];
            s.username = Convert.IsDBNull(dr["username"]) ? "" : (string)dr["username"];
            s.password = Convert.IsDBNull(dr["password"]) ? "" : (string)dr["password"];
            slist.Add(s);
        }
        return slist;
    }

	

	
    public void fn_update(sample s)
    {
        _connection = fn_con();
        SqlCommand cmd = new SqlCommand("sp_a1", _connection);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlParameter[] isparam = new SqlParameter[4];
        isparam[0] = new SqlParameter("@aid", SqlDbType.Int);
        isparam[0].Value = s.aid;
        isparam[1] = new SqlParameter("@calcdays", SqlDbType.Float);
        isparam[1].Value = s.cadays;
        isparam[2] = new SqlParameter("@paiddays", SqlDbType.Float);
        isparam[2].Value = s.padays;
        isparam[3] = new SqlParameter("@presentdays", SqlDbType.Float);
        isparam[3].Value = s.presdays;
        for (int i = 0; i < isparam.Length; i++)
        {
            cmd.Parameters.Add(isparam[i]);

        }
        _connection.Open();
        cmd.ExecuteNonQuery();
        _connection.Close();
    }

    public void fn_delete(sample s)
    {
        _connection = fn_con();
        SqlCommand cmd = new SqlCommand("sp_delete", _connection);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlParameter[] isparam = new SqlParameter[4];
        isparam[0] = new SqlParameter("@aid", SqlDbType.Int);
        isparam[0].Value = s.aid;
        isparam[1] = new SqlParameter("@calcdays", SqlDbType.Float);
        isparam[1].Value = s.cadays;
        isparam[2] = new SqlParameter("@paiddays", SqlDbType.Float);
        isparam[2].Value = s.padays;
        isparam[3] = new SqlParameter("@presentdays", SqlDbType.Float);
        isparam[3].Value = s.presdays;
        for (int i = 0; i < isparam.Length; i++)
        {
            cmd.Parameters.Add(isparam[i]);

        }
        _connection.Open();
        cmd.ExecuteNonQuery();
        _connection.Close();
        
    }
    public Collection<sample>ssupdate() 
    {
        Collection<sample> slist = new Collection<sample>();
        _connection = fn_con();
        SqlCommand cmd = new SqlCommand("select * from a1", _connection);
        _connection.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            sample s = new sample();
            s.aid = (int)dr["aid"];
            s.cadays = Convert.ToDouble(dr["calcdays"]);
            s.padays = Convert.ToDouble(dr["paiddays"]);
            s.presdays =Convert.ToDouble(dr["presentdays"]);
            slist.Add(s);
        }
        return slist;
    }

    public void update1(sample s)
    {
        _connection = fn_con();
        SqlCommand cmd = new SqlCommand("sp_aaa", _connection);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlParameter[] isparam = new SqlParameter[3];
        isparam[0] = new SqlParameter("@eid", SqlDbType.Int);
        isparam[0].Value = s.eid;
        isparam[1] = new SqlParameter("@empid", SqlDbType.VarChar);
        isparam[1].Value = s.empid;
        isparam[2] = new SqlParameter("@empname", SqlDbType.VarChar);
        isparam[2].Value = s.empname;
        isparam[3] = new SqlParameter("@calcdays", SqlDbType.Float);
        isparam[3].Value = s.cadays;
        isparam[4] = new SqlParameter("@paiddays", SqlDbType.Float);
        isparam[4].Value = s.padays;
        for (int i = 0; i < isparam.Length; i++)
        {
            cmd.Parameters.Add(isparam[i]);
        }
        _connection.Open();
        cmd.ExecuteNonQuery();
        _connection.Close();
    }
    public string convert_toname(string curname)
    {
        string name,password;
        string surname;
        if (curname != "")
        {
            name = curname.Substring(0, 2);
            surname = Convert.ToString(name);

        }
        else
        {
            surname = Convert.ToString("aru");
        }
        return surname;
    }
    public string convert_password(string password)
    {
        string pasword;
        string spassword;
        if (password != "")
        {

            pasword = password.Substring(0, 3);
            spassword = Convert.ToString(pasword);

        }
        else
        {
            spassword = Convert.ToString("XXX");
        }
        return spassword;

    }
    
    public Collection<sample>sampleemplist()
    {
        Collection<sample> EmployeeList = new Collection<sample>();
        _connection = fn_con();
        SqlCommand _Course = new SqlCommand("select pn_EmployeeID from paym_Employee", _connection);
        _connection.Open();
        SqlDataReader dr = _Course.ExecuteReader();
        while (dr.Read())
        {
            sample emp = new sample();
            emp.EmployeeId = (int)dr["pn_EmployeeID"];
            //employee..EmployeeId = (int)dr["pn_EmployeeID"];
            //employee.EmployeeCode = Convert.IsDBNull(dr["EmployeeCode"]) ? "" : (string)dr["EmployeeCode"];
            //employee.FirstName = Convert.IsDBNull(dr["Employee_First_Name"]) ? "" : (string)dr["Employee_First_Name"];

            //employee.LastName = (string)dr["EmployeeCode"] + " - " + (string)dr["Employee_First_Name"];

            EmployeeList.Add(emp);
        }
        return EmployeeList;
    }


    public void update3(sample s)
    {
        _connection = fn_con();
        SqlCommand cmd = new SqlCommand("sp_emp", _connection);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlParameter[] isp = new SqlParameter[2];
        isp[0] = new SqlParameter("@eid", SqlDbType.Int);
        isp[0].Value = s.eid;
        isp[1] = new SqlParameter("@empname", SqlDbType.VarChar);
        isp[1].Value = s.empname;
        for (int i = 0; i < isp.Length; i++)
        {
            cmd.Parameters.Add(isp[i]);
        }
        _connection.Open();
        cmd.ExecuteNonQuery();
        _connection.Close();
    }

}
