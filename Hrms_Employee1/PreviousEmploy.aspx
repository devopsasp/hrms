<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="PreviousEmploy.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="Hrms_Employee_PreviousEmploy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
<h3 class="page-header">Previous Employee </h3>
</div>
 <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td class="tdComposeHeader" valign="top">               
                <table runat="server" id="tab_ddl" width="100%" >
                    <tr id="Tr1" runat="server">
                        <td nowrap="nowrap" style="padding: 3%; width: 33%;">
                            &nbsp;</td>
                        <td class="dComposeItemLabel" style="padding: 3%; width: 25%;" >
                            <asp:Label ID="lbl_Error" runat="server" CssClass="Error" ForeColor="Red" Font-Bold="true"></asp:Label>
                        </td>
                        <td id="Td1" runat="server" style="padding: 3%;">
                            &nbsp;</td>
                        <td style="padding: 3%;">
                            &nbsp;</td>
                    </tr>
                    <tr id="row_branch" runat="server">
                        <td align="right">
                            Select Branch&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :</td>
                        <td >
                            <asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged"
                                CssClass="form-control" >
                            </asp:DropDownList></td>
                        <td id="Td3" runat="server" style="padding: 3%;">
                        </td>
                        <td style="padding: 3%;">
                        </td>
                    </tr>
                    <tr id="row_emp" runat="server">
                        <td id="col1_emp" runat="server" style="padding: 3%;" align="right" >
                            Select
                            Employee :
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddl_Employee" runat="server" CssClass="form-control" Width="250">
                            </asp:DropDownList></td>
                        <td align="left" style="padding-left:15px">
                           <asp:Button ID="Button1" ImageUrl="~/Images/Show.png" Text="Show Details" CssClass="btn btn-toolbar" Width="100"
                                runat="server" OnClick="btn_show_Click" />&nbsp;
                            <asp:Button ID="btnBack" CssClass="btn btn-primary" runat="server" Text="Back" OnClick="btnBack_Click" />
                                
                                </td>
                        <td style="padding: 3%;">
                        </td>
                    </tr>
                    <tr id="Tr2"  runat="server">
                        <td id="Td2" runat="server" class="dComposeItemLabel" nowrap="nowrap">
                            &nbsp;</td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                
                                </td>      
                        <td align="left" style="padding-left:15px">
                            &nbsp;</td>      
                    </tr>
                    <tr id="Tr3"  runat="server">
                        <td runat="server" class="dComposeItemLabel" nowrap="nowrap">
                            &nbsp;</td>
                        <td align="center"  >
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr id="Tr4"  runat="server">
                        <td id="Td4" runat="server" class="dComposeItemLabel" nowrap="nowrap">
                            &nbsp;</td>
                        <td align="center" >
                            &nbsp;</td>
                        <td align="center" >
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr id="Tr5" runat="server">
                        <td id="Td5" runat="server" class="dComposeItemLabel" nowrap="nowrap">
                            &nbsp;</td>
                        <td align="center" >
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr id="Tr6" runat="server">
                        <td id="Td6" runat="server" class="dComposeItemLabel" nowrap="nowrap">
                            &nbsp;</td>
                        <td align="center"  style="width: 132px">
                            &nbsp;</td>
                        <td align="center" style="width: 312px">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>

                </table>
              
            </td>
        </tr>
    </table>
</asp:Content>
