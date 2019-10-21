<%@ WebHandler Language="C#" Class="DownloadECR" %>

using System;
using System.Web;
using System.Net;

public class DownloadECR : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) 
    {
        
        System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
        System.Web.HttpRequest request = System.Web.HttpContext.Current.Request;
        string rt = request.QueryString["rt"];
        string fileName = DateTime.Now.ToString("MMMyy") + rt;
        response.ClearContent();
        response.Clear();
        response.ContentType = "text/plain";
        response.AddHeader("Content-Disposition",
                           "attachment; filename=" + fileName + ";");
        response.TransmitFile(HttpContext.Current.Server.MapPath("~//Files//" + rt));
        response.Flush();
        response.End();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}