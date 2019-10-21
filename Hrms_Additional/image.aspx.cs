using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using Microsoft.Win32;
using System.ComponentModel;
using System.Drawing;
using System.Web.SessionState;

public partial class Hrms_Additional_image : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        image.InnerHtml = imageload();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        HttpPostedFile uploadedFile = FileUpload1.PostedFile;
        int lastPos = uploadedFile.FileName.LastIndexOf('\\');
        string fileName = uploadedFile.FileName.Substring(++lastPos);
        string str=MapPath("~/Photo/" + fileName);
        FileUpload1.SaveAs(str);

    }

    public string imageload()
    {
        StringBuilder buffer = new StringBuilder(1024);
    DirectoryInfo dir = new DirectoryInfo(MapPath("~/Photo"));
    FileInfo[] files = dir.GetFiles();

        buffer.Append("<table align=\"center\" width=\"100%\">");  
     buffer.Append("<table align=\"center\" width=\"100%\" border=\"0\" ");
    buffer.Append("cellspacing=\"2\" cellpadding=\"2\">");
    buffer.Append("<tr><td width=\"20%\"><span ");
    buffer.Append("class=\"dimColor\">Picture</span></td>");
    buffer.Append("<td width=\"50%\" align=\"center\"><span ");
    buffer.Append("class=\"dimColor\">Name</span></td>");
    buffer.Append("<td width=\"30%\" align=\"center\"><span ");
    buffer.Append("class=\"dimColor\">Size</span></td>");
    buffer.Append("</tr>");

foreach (FileInfo file in files)
{
    string urlEncFolderName = Server.UrlEncode("Photo");
    string urlEncFileName = Server.UrlEncode(file.Name);
    
    buffer.Append("<a href=\"filedelete.aspx?folder_name=");
    buffer.Append(urlEncFolderName);
    buffer.Append("&file_name=");
    buffer.Append(urlEncFileName);
    buffer.Append("&mode=view\">");
    buffer.Append("<font size=4>");
    buffer.Append(file.Name);
    buffer.Append("</font><br>");
}
buffer.Append("</table>");
return buffer.ToString();
    }
}
