<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="onboarding.aspx.cs" Inherits="onboarding" %>--%>
  <%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"  CodeFile="onboarding.aspx.cs" Inherits="Hrms_Employee_Default5" Title="Welcome to HRMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
           <div>
               <asp:Label ID="Label1" runat="server" >Name</asp:Label>
               <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
           </div>
            <div>
                <asp:Label ID="Label2" runat="server" >Age</asp:Label>
               <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="Label3" runat="server" >Qualification</asp:Label>
               <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="Label4" runat="server" >Mobile No</asp:Label>
               <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="Label5" runat="server" >E-Mail</asp:Label>
               <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="Label6" runat="server" >Resume Upload</asp:Label>
               <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
            </div>
             <div>
                 <asp:Button ID="Button1" runat="server" Text="Save" />
            </div>
        </div>
    </form>
</body>
</html>
    </asp:content>
