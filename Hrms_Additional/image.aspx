<%@ Page Language="C#" AutoEventWireup="true" CodeFile="image.aspx.cs" Inherits="Hrms_Additional_image" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ePay-HRMS</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;<asp:FileUpload ID="FileUpload1" runat="server"  CssClass="form-control"/>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="save" OnClick="Button1_Click" />
        <div id="image" runat="server"></div>
    </div>
    </form>
</body>
</html>
