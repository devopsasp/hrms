<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;


public class Handler : IHttpHandler {

    SqlConnection myconn = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    public void ProcessRequest (HttpContext context) 
    {
        
        myconn.Open();
        SqlCommand cmd = new SqlCommand("select imagedata,address2 from institution_profile where id='"+context.Request.QueryString["id"]+"'", myconn);
        SqlDataReader rdr = cmd.ExecuteReader();
        if (rdr.Read())
        {
            context.Response.ContentType = rdr["address2"].ToString();         
            context.Response.BinaryWrite((byte[])rdr["imagedata"]);

        }

        rdr.Close();
        myconn.Close();

        //**********************************************************************
        //both ways are working
        //int tempid =0 ;
        //if (context.Request.QueryString["id"] != null)
        //{
        //    tempid = Convert.ToInt32(context.Request.QueryString["id"]);

        //}
        //else
        //{
        //    throw new ArgumentException("Id not arrived to ashx");
        //}

        //Stream strm = ShowEmpImage(tempid);
        //byte[] buffer = new byte[4096];
        //int byteSeq = strm.Read(buffer, 0, 4096);
        //while (byteSeq > 0)
        //{
        //    context.Response.OutputStream.Write(buffer, 0, byteSeq);
        //    byteSeq = strm.Read(buffer, 0, 4096);

        //}        
        //**********************************************************************
   
    }
    public Stream ShowEmpImage(int tempid)
    {
        string sql = "select imagedata from institution_profile WHERE id = @ID";
        SqlCommand cmd = new SqlCommand(sql, connection);
        //cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@ID", tempid);
        connection.Open();
        object img = cmd.ExecuteScalar();
        try
        {
            return new MemoryStream((byte[])img);

        }
        catch
        {
            return null;

        }
        finally
        {
            connection.Close();
        }
    } 
 
    public bool IsReusable {
        get 
        {
            return false;
        }
    }

}