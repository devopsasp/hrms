<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Appraisal.aspx.cs" Inherits="Hrms_Additional_Default" Title="Welcome to HRMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>
<script language="javascript" type="text/javascript" src="../datecheck.js"></script>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td id="tdComposeHeader" valign="top" align="right">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr class="border">
                        <td class="border">
                            <span class="Title">&nbsp;&nbsp;&nbsp;<h3>Appraisal</h3></span></td>
                        <td align="left" style="width: 276px; height: 29px" valign="baseline">
                            <asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged"
                                CssClass="form-control">
                            </asp:DropDownList></td>
                    </tr>
                </table>
                <table cellpadding="5" cellspacing="1" width="100%" id="tbl_details" runat="server">
                    <tr id="Tr1" runat="server">
                        <td colspan="4" align="center">
                            &nbsp;<asp:Label ID="lbl_Error" CssClass="Error" runat="server" ForeColor="Red" Font-Bold="True" Width="40%"></asp:Label></td>
                    </tr>
                    <%--<tr id="row_branch" runat="server">
                                            <td class="dComposeItemLabel" nowrap="nowrap" style="height: 29px">
                                                Branch</td>
                                            <td align="left" style="width: 276px; height: 29px" valign="baseline">
                                                <asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged"
                                                    CssClass="InputDefaultStyle">
                                                </asp:DropDownList></td>
                                            <td class="dComposeItemLabel" nowrap="nowrap" style="height: 29px">
                                            </td>
                                            <td style="width: 113px; height: 29px">
                                            </td>
                                        </tr>--%>
                    <tr id="row_emp" runat="server">
                        <td class="dComposeItemLabel" nowrap="nowrap" style="height: 29px">
                            Employee</td>
                        <td align="left" style="height: 29px; width: 276px;" valign="baseline">
                            <asp:DropDownList ID="ddl_Employee" runat="server" CssClass="form-control" AutoPostBack="True"
                                OnSelectedIndexChanged="ddl_Employee_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td class="dComposeItemLabel" nowrap="nowrap" style="height: 29px">
                        </td>
                        <td style="height: 29px; width: 113px;">
                        </td>
                    </tr>
                    <tr id="Tr2" runat="server">
                        <td class="dComposeItemLabel" nowrap="nowrap">
                            Enter Date</td>
                        <td>
                        <input type="text" runat="server" CssClass="form-control" id="txt_date" style="width:55%;" onkeyup="fn_date(event,this.id);" maxlength="10" />
                            <%--<asp:TextBox ID="txt_date" runat="server" CssClass="InputDDLStyle" Width="55%"></asp:TextBox>--%>
                        </td>
                        <td class="dComposeItemLabel" nowrap="nowrap" style="height: 29px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            &nbsp;</td>
                    </tr>
                </table>
                <table width="100%" id="tbl_grd" runat="server">
                    <tr valign="top">
                        <td width="50%" valign="top">
                            <asp:GridView ID="grid_appraisal" runat="server" AutoGenerateColumns="False" Width="100%"
                                DataKeyNames="AppraisalID" class="table table-striped table-bordered table-hover"  ForeColor="#333333"  GridLines="None"
>
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table class="dItemListContentTable" cellspacing="0" cellpadding="0" width="100%">
                                                <colgroup>
                                                    <col>
                                                </colgroup>
                                                <thead>
                                                    <tr>
                                                        <th style="width: 80%;">
                                                            Appraisal Name</th>
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
                                                        <td style="width: 40%;" nowrap>
                                                            <asp:TextBox runat="server" Text='<%#Eval("AppraisalName")%>' ID="txtAppname" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table class="dItemListContentTable" cellspacing="0" cellpadding="0" width="100%">
                                                <colgroup>
                                                    <col>
                                                </colgroup>
                                                <thead>
                                                    <tr>
                                                        <th style="width: 80%;">
                                                            Total Points</th>
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
                                                        <td style="width: 40%;" nowrap>
                                                            <asp:TextBox runat="server" Text='<%#Eval("totalpoint")%>' ID="txttotpts" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table class="dItemListContentTable" cellspacing="0" cellpadding="0" width="100%">
                                                <colgroup>
                                                    <col>
                                                </colgroup>
                                                <thead>
                                                    <tr>
                                                        <th style="width: 80%;">
                                                            Allotted Points</th>
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
                                                        <td style="width: 40%;" nowrap>
                                                        <input type="text" id="txtpoints" value='<%#Eval("Count")%>' runat="server" onkeydown="AllowOnlyNumeric1(event);" />
                                                            <%--<asp:TextBox runat="server" Text='<%#Eval("Count")%>' ID="txtpoints"></asp:TextBox>--%>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td valign="top" width="50%">
                        </td>
                    </tr>
                    <tr runat="server" id="row_totpts" valign="top">
                        <td valign="top" width="50%" style="height: 22px">
                            <span class="dComposeItemLabel">&nbsp; &nbsp;&nbsp; Total Points : &nbsp;&nbsp; &nbsp;&nbsp;</span>&nbsp;<input
                                CssClass="form-control" runat="server" id="txttot_pts" disabled="disabled" /></td>
                    </tr>
                    <tr runat="server" id="row_totamt" valign="top">
                        <td valign="top" width="50%">
                            <span class="dComposeItemLabel">Allotted Amount : &nbsp; &nbsp;</span>
                            <input CssClass="form-control" runat="server" id="txttot_amt" disabled="disabled" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td valign="top" width="50%">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btn_Back" runat="server" Text="Back" OnClick="btn_Back_Click" CausesValidation="False" class="btn btn-info" />
                           <%-- <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/Images/back.jpg" OnClick="btn_Back_Click"
                                CausesValidation="False" />--%>
                            <asp:Button ID="btn_save" runat="server" Text="Save" OnClick="btn_save_Click" class="btn btn-success"/>
                            <%--<asp:ImageButton ID="btn_save" runat="server" ImageUrl="../Images/Save.jpg" OnClick="btn_save_Click" />--%>
                            <asp:Button ID="btn_update" runat="server" Text="Update" OnClick="btn_update_Click" class="btn btn-info" />
                            <%--<asp:ImageButton ID="btn_update" runat="server" ImageUrl="../Images/Update.jpg" OnClick="btn_update_Click" />--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
