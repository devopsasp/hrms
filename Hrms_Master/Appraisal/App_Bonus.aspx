<%@ Page Language="C#" MasterPageFile="~/HRMS.master" 
    AutoEventWireup="true" CodeFile="App_Bonus.aspx.cs" Inherits="Hrms_Master_Appraisal_Default"
    Title="ePay-HRMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td id="tdComposeHeader" valign="top" align="center" style="height: 521px">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="35px" class="border">
                            <span class="Title">&nbsp;&nbsp;Appraisal Bonus</span>
                        </td>
                    </tr>
                </table>
                <table cellpadding="5px" cellspacing="1px" width="100%">
                    <tr>
                        <td colspan="4" align="center">
                            &nbsp;<asp:Label ID="lbl_Error" runat="server" ForeColor="Red" Font-Bold="True" CssClass="Error"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="dComposeItemLabel" nowrap style="width: 132px">
                            <asp:Label ID="lbl_type" runat="server" Text="Type :"></asp:Label>
                        </td>
                        <td align="left" valign="baseline" style="width: 278px">
                            <asp:DropDownList ID="ddl_type" runat="server" CssClass="InputDefaultStyle" AutoPostBack="True"
                                OnSelectedIndexChanged="ddl_type_SelectedIndexChanged">
                                <asp:ListItem Value="0">Type....</asp:ListItem>
                                <asp:ListItem Value="1">Bonus Band</asp:ListItem>
                                <asp:ListItem Value="2">Salary Band</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="dComposeItemLabel" nowrap style="width: 47px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            &nbsp;&nbsp;
                            <table width="100%">
                                <tr valign="top">
                                    <td width="50%" valign="top">
                                        <asp:GridView ID="grid_appraisal" runat="server" AutoGenerateColumns="False" Width="73%"
                                            DataKeyNames="Bonus_id" onrowupdating="Update">
                                            <%--OnRowEditing="Edit"--%>
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <table class="dItemListContentTable" cellspacing="0" cellpadding="0" width="100%">
                                                            <colgroup>
                                                                <col>
                                                            </colgroup>
                                                            <thead>
                                                                <tr>
                                                                <th style="width: 5%;">
                                                                    </th>
                                                                    <th style="width: 25%;">
                                                                        Band
                                                                    </th>
                                                                    <th style="width: 25%;">
                                                                        From
                                                                    </th>
                                                                    <th style="width: 20%;">
                                                                        To
                                                                    </th>
                                                                    <th style="width: 15%;">
                                                                        Points
                                                                    </th>
                                                                    <th style="width: 10%;">
                                                                    Save
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table class="dItemListContentTable" cellspacing="0" cellpadding="0" width="100%">
                                                            <colgroup>
                                                                <col class="dInboxContentTableCheckBoxCol">
                                                            </colgroup>
                                                            <tbody>
                                                                <tr>
                                                                <td align="center" style="width: 7%;" nowrap>
                                                                    <td style="width: 20%;" nowrap>
                                                                        <%#Eval("Fond")%>
                                                                    </td>
                                                                    <td style="width: 25%;" nowrap>
                                                                        <input type="text" id="txt_fromvalue" runat="server" value='<%#Eval("From_value")%>' class="InputDefaultStyle" style="width:87px;" />
                                                                    </td>
                                                                    <td style="width: 25%;" nowrap>
                                                                        <input type="text" id="txt_tovalue" runat="server" value='<%#Eval("To_value")%>' class="InputDefaultStyle" style="width:87px;" />
                                                                    </td>
                                                                    <td style="width: 10%;" nowrap>
                                                                        <%#Eval("Bonus_Points")%>
                                                                    </td>
                                                                    <td align="center" style="width: 13%;" nowrap>
                                                                           <asp:ImageButton ID="img_save" ImageUrl="~/Images/save.gif" runat="server" CommandName="Update" AlternateText="Save"  />
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
