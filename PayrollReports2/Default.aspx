<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Master_Page/EmployeeMaster.master" CodeFile="Default.aspx.cs" Inherits="_Default" %><%@ Register
    Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
    <table width="1024">
     <tr><td height="30px" class="border" align="left" style="background-color: #5D7B9D">
                <span class="Title" 
                    style="font-family: calibri; font-size: medium; font-weight: bold; color: #FFFFFF">&nbsp;&nbsp;<img src="../Images/rp_arrow.gif" />&nbsp;Task Schedule Report</span></td>
         <td height="30px" bgcolor="#5D7B9D">
                            &nbsp;<asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" CssClass="InputDefaultStyle" OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td></tr>
    <tr><td style="text-align: center">
        <asp:Label ID="lbl_Error" runat="server" Text="lblError" Font-Names="Calibri"></asp:Label></td></tr>
    <tr><td style="text-align: center">
        &nbsp;</td></tr>
         <tr>
    <td align="center" style="font-family: calibri">&nbsp;Select Department&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;           
    <asp:DropDownList ID="ddl_dept" runat="server" Width="193px" 
            AutoPostBack="True" onselectedindexchanged="ddl_dept_SelectedIndexChanged">
             <asp:ListItem>Select</asp:ListItem>
    </asp:DropDownList></td>
    </tr>
         <tr>
    <td align="center">&nbsp;</td>
    </tr>
    <tr>
    <td align="center" style="font-family: calibri">&nbsp; Select Employee&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;           
    <asp:DropDownList ID="ddl_Employee" runat="server" Width="193px" 
            AutoPostBack="True" 
            onselectedindexchanged="ddl_Employee_SelectedIndexChanged" >
    </asp:DropDownList></td>
    </tr>
    <tr>
    <td align="center">&nbsp;</td>
    </tr>
    <tr><td align="center">
        <asp:ImageButton ID="btn_save" runat="server" 
            ImageUrl="../Images/Show_Report.jpg"  Height="20px" onclick="btn_save_Click" />
    <tr><td>
    </td></tr>
    </table>
    </div>
</asp:Content>
