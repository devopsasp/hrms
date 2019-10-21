<%@ WebHandler Language="C#" Class="Handler3" %>

using System;
using System.Web;
using System.Data.SqlClient;

public class Handler3 : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string imageid = context.Request.QueryString["ImID"];
        SqlConnection con = new SqlConnection(@"Data Source=SQL-SERVER;Initial Catalog=Hesperus_Hrms;Integrated Security=True;");
        con.Open();
        SqlCommand cmd = new SqlCommand("Select billimage2 from reimbursement where id='" + imageid + "'", con);
        SqlDataReader dr = cmd.ExecuteReader();
        dr.Read();
        context.Response.BinaryWrite((byte[])dr["billimage2"]);
        dr.Close();
        con.Close();
    }
 
    public bool IsReusable {
        get 
        {
            return false;
        }
    }

}