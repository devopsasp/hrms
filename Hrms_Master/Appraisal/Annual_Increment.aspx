<%@ Page Language="C#" MasterPageFile="~/Master_Page/Master.master"
    AutoEventWireup="true" CodeFile="Annual_Increment.aspx.cs" Inherits="Hrms_Master_Default"
    Title="Welcome to HRMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../Css/m_popup.css" rel="stylesheet" type="text/css" />
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="background-color: #5D7B9D" height="35px" class="border">
                <span class="Title">&nbsp;&nbsp;<span class="style2" 
                    style="font-family: Calibri; color: #FFFFFF; font-weight: bold;">Annual Increment</span></span>
            </td>
            <td align="center" style="background-color:#5D7B9D">
                <asp:DropDownList ID="ddl_branch" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="ddl_branch_SelectedIndexChanged" Width="115px">
                </asp:DropDownList>
                                            </td>
        </tr>
        <tr>
            <td style="padding-left: 350px">
                <span class="style2">
                    <asp:Label ID="lblerror" runat="server" ForeColor="Red"></asp:Label></span>
            </td>
        </tr>
    </table>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <br />
    <div align="center">
        <asp:LinkButton ID="Link1" runat="server" OnClick="Link1_Click">Click here for overall annual increment</asp:LinkButton></div>
    <table id="overall" runat="server" style="border-width: thin; border-style: groove;
        width: 59%; height: 48px" align="center" visible="False">
        <tr>
            <td style="width: 166px; height: 40px;">
                <asp:Label ID="Label4" runat="server" Font-Names="Calibri" Text="Annual Increment (in %) :"></asp:Label>
            </td>
            <td style="width: 107px; height: 40px;">
                <asp:TextBox ID="txt_incvalue" runat="server" Font-Names="Calibri" Style="margin-left: 20px"
                    Width="72px"></asp:TextBox>
            </td>
            <td style="width: 97px; height: 40px;">
                <asp:Label ID="Label5" runat="server" Font-Names="Calibri" Text="of basic salary"></asp:Label>
            </td>
            <td style="width: 75px; height: 40px;">
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Save" />
            </td>
        </tr>
    </table>
    <br />
    <br />
    <table align="center" style="width: 315px" bgcolor="#CDCDC1" border="1">
        <tr>
            <td style="font-family: Calibri; background-color: #C0C0C0;" align="center" colspan="2">
                <asp:Label ID="lbl3" runat="server" Text="Department / Grade wise increment" Width="266px"
                    Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 321px; font-family: Calibri" align="left">
                <asp:Label ID="lbl2" runat="server" Text="Increment Mode" Width="108px"></asp:Label>&nbsp;&nbsp;
            </td>
            <td style="width: 182px; font-family: Calibri">
                <asp:DropDownList ID="ddlmode" runat="server" OnSelectedIndexChanged="ddlmode_SelectedIndexChanged"
                    AutoPostBack="true" Height="23px" Width="128px">
                    <asp:ListItem Value="0">Select</asp:ListItem>
                    <asp:ListItem Value="1">Percentage</asp:ListItem>
                    <asp:ListItem Value="2">Amount</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr id="tr_rdo_grade" runat="server" visible="false">
            <td style="width: 321px; height: 27px;" align="center">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="rdo_grade" runat="server" GroupName="grade" Text="Grade_wise"
                    AutoPostBack="true" OnCheckedChanged="rdo_grade_CheckedChanged" />
            </td>
            <td style="width: 182px; height: 27px;">
                <asp:RadioButton ID="rdo_dept" runat="server" GroupName="grade" Text="Department_vise"
                    AutoPostBack="true" OnCheckedChanged="rdo_dept_CheckedChanged" />
            </td>
        </tr>
    </table>
    <%-- <br />--%>
    <div id="pnl" runat="server">
        <table border="1" id="tbl" runat="server" align="center" bgcolor="#CDCDC1" style="width: 315px;
            font-family: Calibri; height: 161px;" visible="false">
            <tr id="tr_dept" runat="server" visible="false">
                <td style="width: 146px; font-family: Calibri">
                    <asp:Label ID="lblemp" runat="server" Text="Select Department"></asp:Label>
                    <sup><span style="color: #FF0000">*</span></sup>
                </td>
                <td style="width: 121px; font-family: Calibri">
                    <asp:DropDownList ID="ddl_dept" runat="server" Width="128px" BackColor="#EEEEE0"
                        Height="28px" Style="height: 22px; margin-bottom: 0px" AutoPostBack="True" OnSelectedIndexChanged="ddl_dept_SelectedIndexChanged">
                        <asp:ListItem>Select</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="tr_grade" runat="server" visible="false">
                <td style="width: 146px; font-family: Calibri">
                    <asp:Label ID="lblgrade" runat="server" Text="Select Grade"></asp:Label>
                    <sup><span style="color: #FF0000">*</span></sup>
                </td>
                <td style="width: 121px; font-family: Calibri">
                    <asp:DropDownList ID="ddlgrade" runat="server" Width="121" BackColor="#EEEEE0" OnSelectedIndexChanged="ddlgrade_SelectedIndexChanged"
                        AutoPostBack="true">
                        <asp:ListItem>Select</asp:ListItem>
                        <asp:ListItem>Select</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="tr_amt" runat="server" visible="false">
                <td style="width: 146px; font-family: Calibri">
                    <asp:Label ID="lblamt" runat="server" Text="Increment Amount"></asp:Label>
                </td>
                <td style="width: 121px; font-family: Calibri">
                    <asp:TextBox ID="txtamt" runat="server" BackColor="#EEEEE0" Width="121px" Style="margin-left: 0px"
                        MaxLength="6"></asp:TextBox>
                </td>
            </tr>
            <tr id="tr_per" runat="server" visible="false">
                <td style="width: 146px; font-family: Calibri; height: 28px;">
                    <asp:Label ID="lblper" runat="server" Text="Increment percentage"></asp:Label>
                </td>
                <td style="width: 121px; font-family: Calibri; height: 28px;">
                    <asp:TextBox ID="txtper" runat="server" BackColor="#EEEEE0" Width="121px" MaxLength="3"></asp:TextBox>
                </td>
            </tr>
            <tr id="tr_date" runat="server">
                <td style="width: 146px">
                    <asp:Label ID="lbldate" runat="server" Text="Appraisal Date"></asp:Label>
                </td>
                <td style="width: 121px">
                    <asp:TextBox ID="txtdate" runat="server" Width="121px" BackColor="#EEEEE0" Height="22px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 146px; height: 32px;">
                </td>
                <td style="width: 121px; font-family: Calibri; height: 32px;">
                    <asp:Button ID="cmd_ok" runat="server" Text="Save" OnClick="cmd_ok_Click" />
                    <asp:Button ID="btn_delete" runat="server" Text="Delete" />
                </td>
            </tr>
        </table>
    </div>
    </div>
    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdate"
        Animated="true" Format="dd/MM/yyyy">
    </asp:CalendarExtender>
    <br />
    <br />
    <div id="popup_container" runat="server" class="popup" visible="false" style="font-family: Calibri;
        width: 506px;">
        <asp:Panel ID="pnl_popup" runat="server" Visible="false" BorderColor="ActiveBorder"
            Height="16px" Width="487px">
            <table style="width: 501px">
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="height: 46px; width: 495px">
                        <asp:Label ID="lblpopup_dept" runat="server" Text="Already having Department-wise Allotment,settings will be replaced if you proceed"></asp:Label>
                    </td>
                </tr>
            </table>
            <table align="center" style="height: 48px">
                <tr>
                    <td style="height: 15px">
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="cmd_k" runat="server" Text="OK" OnClick="cmd_k_Click" Width="60px"
                            Height="26px" />
                    </td>
                    <td>
                        <asp:Button ID="cmd_c" runat="server" Text="Cancel" OnClick="cmd_c_Click" Width="60px"
                            Style="height: 26px" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <asp:Panel ID="pnl_popup1" runat="server" Visible="false" BorderColor="ActiveBorder"
            Style="margin-left: 20px" Width="451px">
            <table style="width: 504px">
                <tr>
                    <td>
                    </td>
                </tr>
                <tr style="height: 47px">
                    <td>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblpopup" runat="server" Text="Already having Grade-wise Allotment,settings will be replaced if you proceed"></asp:Label>
                    </td>
                </tr>
            </table>
            <table align="center">
                <tr>
                    <td style="height: 14px">
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="cmd_dept_k" runat="server" Text="OK" OnClick="cmd_dept_k_Click" Width="60px" />
                    </td>
                    <td>
                        <asp:Button ID="cmd_dept_c" runat="server" Text="Cancel" Width="60px" OnClick="cmd_dept_c_Click1" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <table align="center">
        <tr>
            <td align="center">
                <asp:GridView ID="GridView1" Font-Size="Small" runat="server" AllowSorting="True"
                    AutoGenerateColumns="False" Height="16px" Width="551px" CellPadding="4" 
                    ForeColor="#333333" OnRowDataBound="GridView1_RowDataBound" GridLines="None">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <RowStyle Font-Names="Calibri" HorizontalAlign="Center" ForeColor="#333333" 
                        BackColor="#F7F6F3" />
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Dept ID">
                            <ItemTemplate>
                                <asp:Label ID="lbl_dept" runat="server" Text='<%# Eval("deptid") %>'></asp:Label>
                            </ItemTemplate>
                            <ControlStyle Width="50" />
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Department Name">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("deptname") %>'></asp:Label>
                            </ItemTemplate>
                            <ControlStyle Width="50" />
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Value">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("inc_value") %>'></asp:Label>
                            </ItemTemplate>
                            <ControlStyle Width="50" />
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Date">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("date","{0:dd/MM/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                            <ControlStyle Width="50" />
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" 
                        Font-Names="Calibri" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
