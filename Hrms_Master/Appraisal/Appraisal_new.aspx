<%@ Page Language="C#" MasterPageFile="~/Master_Page/Master.master" AutoEventWireup="true" CodeFile="Appraisal_new.aspx.cs" Inherits="Hrms_Master_Appraisal_Default" Title="ePay-HRMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td id="tdComposeHeader" valign="top" align="center" style="height: 521px">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="35px" class="border">
                            <span class="Title">&nbsp;&nbsp;Appraisal</span></td>
                    </tr>
                </table>
                <table cellpadding="5px" cellspacing="1px" width="100%">
                    <tr>
                        <td colspan="4" align="center">
                            &nbsp;<asp:Label ID="lbl_Error" runat="server" ForeColor="Red" Font-Bold="True" CssClass="Error"></asp:Label></td>
                    </tr>
                    
                     <tr>
                        <td class="dComposeItemLabel" nowrap>
                            Select Appraisal Type</td>
                        <td align="left" valign="baseline">
                            <%--<asp:DropDownList ID="ddl_type" runat="server" CssClass="InputDefaultStyle">
                                <asp:ListItem Value="st">Select Type</asp:ListItem>
                                <asp:ListItem Value="1">Appraisal 180</asp:ListItem>
                                <asp:ListItem Value="2">Appraisal 360</asp:ListItem>
                            </asp:DropDownList>--%>
                            <asp:RadioButtonList ID="rdo_type" runat="server" RepeatDirection="Horizontal" CssClass="dComposeItemLabel" Width="220px" AutoPostBack="True" OnSelectedIndexChanged="rdo_type_SelectedIndexChanged" >
                                <asp:ListItem Value="180" Selected="True">Appraisal 180</asp:ListItem>
                                <asp:ListItem Value="360">Appraisal 360</asp:ListItem>
                            </asp:RadioButtonList>
                            <td class="dComposeItemLabel" nowrap>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    
                    <tr>
                        <td class="dComposeItemLabel" nowrap>
                            Select Department</td>
                        <td align="left" valign="baseline">
                            <asp:DropDownList ID="ddl_department" runat="server" CssClass="InputDefaultStyle" AutoPostBack="True" OnSelectedIndexChanged="ddl_department_SelectedIndexChanged">
                            </asp:DropDownList>
                        <td class="dComposeItemLabel" nowrap>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    
                    <tr>
                        <td class="dComposeItemLabel" nowrap>
                            Select Type</td>
                        <td align="left" valign="baseline">
                            <asp:DropDownList ID="ddl_type" runat="server" CssClass="InputDefaultStyle">
                                <asp:ListItem Value="st">Select Type</asp:ListItem>
                                <asp:ListItem Value="1">Team members</asp:ListItem>
                                <asp:ListItem Value="2">Superior</asp:ListItem>
                                <asp:ListItem Value="3">Customers</asp:ListItem>
                                <asp:ListItem Value="4">Suppliers</asp:ListItem>
                                <asp:ListItem Value="5">Vendors</asp:ListItem>
                                <asp:ListItem Value="6">Peers</asp:ListItem>
                                <asp:ListItem Value="7">Subordinate</asp:ListItem>
                            </asp:DropDownList></td>
                        <td class="dComposeItemLabel" nowrap>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    
                    <tr>
                        <td class="dComposeItemLabel" nowrap>
                            Appraisal Question</td>
                        <td align="left" valign="baseline">
                        <textarea id="txt_appraisalname" runat="server" class="InputDefaultStyle" style="width: 304px; height: 46px" ></textarea>
                            <%--<input runat="server" id="txt_appraisalname" onkeydown="AllowOnlyText1(event);" />--%>
                        <td class="dComposeItemLabel" nowrap>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="dComposeItemLabel" nowrap="nowrap" style="height: 35px">
                            <%--points--%></td>
                        <td align="left" valign="baseline" style="height: 35px">
                            <%--<input class="InputDefaultStyle" runat="server" id="Txt_points" onkeydown="AllowOnlyNumeric1(event);" />--%>
                            <asp:Button ID="btn_add" runat="server" OnClientClick="return fnSave();" Text="Add"
                                Width="75px" OnClick="btn_add_Click" />
                                <asp:Button ID="btn_Update" runat="server" OnClientClick="return fnSave();" Text="Update"
                                Width="75px" OnClick="btn_Update_Click" />
                                </td>
                        <td class="dComposeItemLabel" nowrap="nowrap" style="height: 35px">
                        </td>
                        <td style="height: 35px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            &nbsp;&nbsp;
                            <table width="100%">
                                <tr valign="top">
                                    <td width="50%" valign="top">
                                        <asp:GridView ID="grid_appraisal" runat="server" AutoGenerateColumns="False" Width="100%" OnRowEditing="Edit" DataKeyNames="AppraisalID"> 
                                        <%--DataKeyNames="AppraisalID" OnRowUpdating="Update" OnRowEditing="Edit" OnRowDeleting="delete"--%>
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
                                                                        &nbsp;</th>
                                                                    <th style="width: 69%;">
                                                                        Appraisal Questions</th>
                                                                    <th style="width: 10%;">
                                                                        Type</th>
                                                                        <th style="width: 12%;">
                                                                        Designation</th>
                                                                        <th style="width: 5%;">
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
                                                                    <td style="width: 5%;" align="left">
                                                                        <input type="checkbox" id="Chk_Grade" runat="server" /></td>
                                                                    <td style="width: 70%;" nowrap>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("AppraisalName")%>' Width="475px" ></asp:Label><%--Style="display:block;float:left;width:175px;"--%>
                                                                    
                                                                    <%--<input runat="server" id="txtgrid" onkeydown="AllowOnlyText1(event);" value='<%#Eval("AppraisalName")%>' disabled="disabled" />--%>
                                                                        <%--<asp:TextBox runat="server" Text='<%#Eval("AppraisalName")%>' ID="txtgrid" Enabled="false"></asp:TextBox>--%>
                                                                    </td>
                                                                    <td style="width: 10%;" nowrap>
                                                                    <%#Eval("totalpoint")%>
                                                                    <%--<input runat="server" id="Txtgpoint" onkeydown="AllowOnlyNumeric1(event);" value='<%#Eval("totalpoint")%>' disabled="disabled" />
                                                                        <%--<asp:TextBox runat="server" Text='<%#Eval("totalpoint")%>' ID="Txtgpoint" Enabled="false"></asp:TextBox>--%>
                                                                    </td>
                                                                     <td style="width: 10%;" nowrap>
                                                                    <%#Eval("Feedtype")%>
                                                                    </td>
                                                                    <td align="center" style="width: 5%;" nowrap>
                                                                       <asp:ImageButton ID="img_update" ImageUrl="../../Images/i_Edit.gif" runat="server" Style="border: 0"
                                                                            AlternateText="" CommandName="Edit" />
                                                                         <%--<asp:ImageButton ID="img_save" ImageUrl="../../Images/save.gif" runat="server" Style="border: 0"
                                                                            AlternateText="" CommandName="Edit" Visible="false" /></td>--%>
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
                            <br />
                            <asp:Button ID="btn_Apply" runat="server" Text="Assign to Employees" OnClick="btn_Apply_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

