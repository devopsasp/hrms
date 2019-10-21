using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePayHrms.Connection;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Machine
/// </summary>
public class Machine
{
    public Machine()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private SqlConnection _connect;
    ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();
    private string MNo;
    private string IP;
    public string mno
    {
        get { return mno; }
        set { mno = value; }
    }
    public string ip
    {
        get { return ip; }
        set { ip = value; }
    }
    public string MacAdd(int comid, int bracnchid, string No, string ipaddr, string Loc)
    {
        try
        {
            _connect = con.fn_Connection();
            _connect.Open();
            string sql = "insert into Machine(pn_CompanyID, pn_BranchID, Mno, IPAddr, Location) values('" + comid + "','" + bracnchid + "','" + No + "','" + ipaddr + "','" + Loc + "')";
            SqlCommand cmd = new SqlCommand(sql, _connect);
            cmd.ExecuteNonQuery();
            _connect.Close();
            return "1";
        }
        catch (Exception e)
        {
            return "0";
        }
    }
    public DataSet Grid_Output(string query)
    {

        _connect = con.fn_Connection();
        _connect.Open();
        SqlDataAdapter da = new SqlDataAdapter(query, _connect);
        DataSet ds = new DataSet();
        da.Fill(ds, "Machine");
        _connect.Close();
        return ds;

    }
    public string RowDelete(string id, string query)
    {
        try
        {
            _connect = con.fn_Connection();
            _connect.Open();
            SqlCommand cmd = new SqlCommand(query, _connect);
            cmd.ExecuteNonQuery();
            _connect.Close();
            return "1";
        }
        catch (Exception e)
        {
            return "0";
        }
    }
    public string RowUpdate(string query)
    {
        try
        {
            _connect = con.fn_Connection();
            _connect.Open();
            SqlCommand cmd = new SqlCommand(query, _connect);
            cmd.ExecuteNonQuery();
            _connect.Close();
            return "1";
        }
        catch (Exception e)
        {
            return "0";
        }
    }
}