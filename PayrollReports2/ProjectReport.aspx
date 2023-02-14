<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectReport.aspx.cs" MasterPageFile="~/PayrollReports/Report.master" Inherits="PayrollReports_ProjectReport" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table width="1024px"><tr>
<td>
    <asp:Label ID="lbl_Error" runat="server" Text="Label" Visible="False"></asp:Label>
</td>
</tr>
<tr>
<td align="center" style="height: 43px">
        Select Department
        <asp:DropDownList ID="ddl_department" Width="175px" runat="server" 
            onselectedindexchanged="DropDownList1_SelectedIndexChanged">
        </asp:DropDownList>
    </td>
    
</tr>
<tr>
<td align="center" style="height: 43px">
        Enter no of days
    <asp:TextBox ID="txt_days" runat="server"></asp:TextBox>
    </td>
    
</tr>
<tr>
<td align="center" style="height: 43px">
       From Date
    <asp:TextBox ID="txt_from_date" runat="server" onkeyup="fn_date(event,this.id);" MaxLength="10"></asp:TextBox>
    </td>
    
</tr>
<tr>
<td align="center" style="height: 43px">
       To Date
    <asp:TextBox ID="txt_to_date" runat="server" onkeyup="fn_date(event,this.id);" MaxLength="10"></asp:TextBox>
    </td>
    
</tr>
<tr>
<td align="center">
    <asp:CheckBox ID="chk_successive"  runat="server" Text="Successive " />
</td>
</tr>
<tr>
<td align="center">
 <asp:ImageButton ID="btn_Report" runat="server" 
        ImageUrl="../Images/Show_Report.jpg" onclick="btn_Report_Click" />
</td>
</tr>
<tr>
<td>
</td>
</tr>

</table>
</asp:Content>

