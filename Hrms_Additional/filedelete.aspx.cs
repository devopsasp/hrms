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

namespace example_fup
{
    public partial class filedelete : System.Web.UI.Page
    {
        private static string GetContentTypeFromFileExt(string fileExtension)
        {
            RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(fileExtension);
            try
            {
                return regKey.GetValue("Content Type", "application/octet-stream").ToString();
            }
            catch (Exception)
            {
                return "application/octet-stream";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string folderName = Request.Params["folder_name"];
            string fileName = Request.Params["file_name"];
            string mode = Request.Params["mode"];

            HandleFileAction(folderName, fileName, mode);
        }
        private void HandleFileAction(string folderName, string fileName, string mode)
        {
            FileInfo file = new FileInfo(MapPath("~/"+folderName + "/" + fileName));
            if (file != null)
            {
                switch (mode)
                {
                    case "view":
                        ViewFile(file);
                        break;
                    case "delete":
                        DeleteFile(file);
                        break;
                    case "download":
                        DownloadFile(file);
                        break;
                    default:
                        Response.Redirect("Fileupload_asp.aspx");
                        break;
                }
            }
            else
            {
                Response.Redirect("Fileupload_asp.aspx");
            }
        }

        private void ViewFile(FileInfo file)
        {
            Response.Clear();
            Response.AppendHeader("Content-Disposition", "filename=" + file.Name);
            Response.ContentType = GetContentTypeFromFileExt(file.Extension);
            Response.WriteFile(file.FullName);
        }

        private void DeleteFile(FileInfo file)
        {
            file.Delete();
            Response.Redirect("Fileupload_asp.aspx");
        }

        private void DownloadFile(FileInfo file)
        {
            Response.Clear();
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + file.Name);
            Response.ContentType = GetContentTypeFromFileExt(file.Extension);
            Response.WriteFile(file.FullName);
        }

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }
    }
}