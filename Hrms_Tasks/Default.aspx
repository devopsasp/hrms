<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" MasterPageFile="~/HRMS.master" Inherits="Hrms_Tasks_Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
     <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="true" />
        <asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="UploadMultipleFiles" accept="image/gif, image/jpeg" />
        <hr />
     <asp:Button ID="btndownload" Text="Download" runat="server" OnClick="DownloadFiles" />
        <hr />
        <asp:Label ID="lblSuccess" runat="server" ForeColor="Green" />
     <%--<asp:FileUpload ID="UploadImages" runat="server" AllowMultiple="true" />
        <asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="UploadMultipleFiles" accept="image/gif, image/jpeg" />
        <hr />
          <asp:Label ID="listofuploadedfiles" runat="server" />--%>
</asp:Content>