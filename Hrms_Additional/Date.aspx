<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Date.aspx.cs" Inherits="Hrms_Additional_Default2" Title="ePay-HRMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="50%" cellpadding="2" cellspacing="2">
        <tr>
            <td>
            <asp:Label ID="Label1" runat="server" Text="select category"></asp:Label>
    <asp:DropDownList ID="ddl_all" runat="server" AutoPostBack="True"  CssClass="form-control" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
        <asp:ListItem Value="0">all</asp:ListItem>
        <asp:ListItem Value="1">category</asp:ListItem>
        <asp:ListItem Value="2">emp</asp:ListItem>
        <asp:ListItem Value="3">department</asp:ListItem>
        <asp:ListItem Value="4">designation</asp:ListItem>
    </asp:DropDownList> 
               
            </td>
        </tr>
        <tr><td>select list
            <asp:DropDownList ID="ddl_2" runat="server"  CssClass="form-control">
            </asp:DropDownList></tr>
            <tr><td> <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar></td></tr>
            
            <tr><td>
                <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click"  class="btn btn-success"/></td></tr>
            
    </table>
   
    

</asp:Content>