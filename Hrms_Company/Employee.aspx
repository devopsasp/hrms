<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Employee.aspx.cs" Inherits="Hrms_Company_Default" Title="Welcome to HRMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../../Scripts/Datavalid.js"></script>
    <div>
        <h3 class="page-header">Employee Masters</h3>
    </div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="divWaiting">

                <asp:Image ID="imgWait" runat="server" ImageAlign="Middle"
                    ImageUrl="~/Images/loading1.gif" Height="100px" Width="100px" />

            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr valign="top">
                    <td class="tdComposeHeader" valign="top">
                        <table runat="server" id="tab_ddl" width="100%">
                            <tr runat="server">
                                <td nowrap="nowrap" style="padding: 3%; width: 38%;">&nbsp;</td>
                                <td class="dComposeItemLabel" style="padding: 3%; width: 269px;">&nbsp;</td>
                                <td runat="server" style="padding: 3%;">&nbsp;</td>
                                <td style="padding: 3%;">&nbsp;</td>
                            </tr>
                            <tr id="row_branch" runat="server">
                                <td align="right" style="width: 38%">Select Branch&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                <td style="width: 269px">
                                    <asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged"
                                        CssClass="form-control">
                                    </asp:DropDownList></td>
                                <td id="Td3" runat="server" style="padding: 3%;"></td>
                                <td style="padding: 3%;"></td>
                            </tr>
                            <tr runat="server">
                                <td runat="server" class="dComposeItemLabel" nowrap="nowrap" align="right"
                                    style="width: 38%; height: 50px;">Select Department&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                <td align="left" colspan="2" style="height: 50px">
                                    <span style="color: #800000">
                                        <asp:DropDownList ID="ddl_department" runat="server" AutoPostBack="True"
                                            class="form-control" OnSelectedIndexChanged="ddl_department_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </span>
                                </td>
                            </tr>
                            <tr id="row_emp" runat="server">
                                <td runat="server" class="dComposeItemLabel" nowrap="nowrap" align="right"
                                    style="width: 38%; height: 53px;">Select Employee&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                <td align="left" style="height: 53px; width: 269px">
                                    <asp:DropDownList ID="ddl_Employee" runat="server" CssClass="form-control" Width="250"
                                        OnSelectedIndexChanged="ddl_Employee_SelectedIndexChanged">
                                    </asp:DropDownList></td>
                                <td align="left" style="height: 53px">
                                    <asp:Button ID="btn_add_employee0" runat="server" CssClass="btn btn-success"
                                        OnClick="btn_add_employee_Click" Text="Add Employee" />
                                </td>
                                <td style="height: 53px"></td>
                            </tr>
                            <tr runat="server">
                                <td runat="server" class="dComposeItemLabel" nowrap="nowrap"
                                    style="width: 38%; height: 58px;"></td>
                                <td align="left" style="height: 58px; width: 269px">
                                    <asp:Button ID="Button2" runat="server" CssClass="btn btn-toolbar"
                                        ImageUrl="~/Images/Show.png" OnClick="btn_show_Click" Text="Show Details"
                                        Width="100" />
                                  
                                </td>
                                <td align="left" style="height: 58px">

                                    <asp:Button ID="Img_prev" runat="server" CssClass="btn btn-info" Text="Previous Employee"
                                        OnClick="Img_prev_Click" />
                                </td>
                                <td style="height: 58px"></td>
                            </tr>
                            <tr runat="server">
                                <td runat="server" class="dComposeItemLabel" nowrap="nowrap" style="width: 38%">&nbsp;</td>
                                <td align="center" style="width: 269px">&nbsp;</td>
                                <td align="center">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr runat="server">
                                <td runat="server" class="dComposeItemLabel" nowrap="nowrap" style="width: 38%">&nbsp;</td>
                                <td align="center" style="width: 269px">&nbsp;</td>
                                <td align="center" style="width: 312px">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>

                        </table>

                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
