<%@ Page MaintainScrollPositionOnPostback="true" ValidateRequest="false" Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Tasks_onlinetest.aspx.cs" Inherits="Hrms_Tasks_Default" Title="ePay-HRMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td id="td1" valign="top">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="35px">
                            <span class="Title" 
                                style="font-family: calibri; font-size: medium; font-weight: bold;">&nbsp;<span class="Title"></span>&nbsp; Human Resource Management System -----> Tasks&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                <asp:Label ID="Label8" runat="server"></asp:Label>
                                &nbsp;</span></td>
                    </tr>
                </table>
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr valign="top">
                        <td id="tdComposeHeader" valign="top" align="center">
                            <table cellpadding="5px" cellspacing="1px" width="95%">
                                <tr>
                                    <td width="100%">
                                        <table width="100%" align="center" class="InputTextStyle">
                                            <tr>
                                                <td align="center" colspan="6">
                                                    <asp:Label ID="lbl_Error" runat="server" Font-Bold="True" ForeColor="Red" 
                                                        Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="bottom" colspan="6">
                                                    &nbsp;<asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="False"
                                                        Font-Names="Calibri" Style="font-family: Tahoma; font-size: x-small; color: #444444;"
                                                        Text="Enter the Question set code : "></asp:Label>
                                                    &nbsp;<asp:TextBox ID="txt_qsetno"
                                                        runat="server" Width="88px" ontextchanged="txt_qsetno_TextChanged" 
                                                         CssClass="form-control"></asp:TextBox>
                                                    <%--<asp:Button ID="btn_submit" runat="server" Text="Button" OnClick="btn_submit_Click" />--%>&nbsp;&nbsp;
                                                    <asp:Button ID="ImageButton2" runat="server" Text="Submit" style="height: 30px" Height="24px" Width="124px" onclick="ImageButton2_Click" class="btn btn-success"/>
                                                    <%--<asp:ImageButton ID="ImageButton2" runat="server" 
                                                        ImageUrl="~/Images/Submit.png" onmouseover="this.src='../Images/Submitover.png';" onmouseout="this.src='../Images/Submit.png';" onclick="ImageButton2_Click" 
                                                        style="height: 30px" Height="24px" Width="124px" />--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="6">
                                                    <asp:GridView ID="GridView1" Font-Size="Smaller" runat="server" AllowSorting="True" class="table table-striped table-bordered table-hover"
                                                        AutoGenerateColumns="False" Height="16px" Width="702px" BackColor="White"
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowEditing="Row_Editing"
                                                        OnRowDataBound="GridView1_RowDataBound" HorizontalAlign="Center">
                                                        <FooterStyle  />
                                                        <RowStyle  Font-Names="Calibri" />
                                                        <Columns>
                                                            <%--<asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="S.no">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_sno" runat="server" Text="1"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText=" Online Test !">
                                                                <ItemTemplate>
                                                                    &nbsp;&nbsp;
                                                                    <asp:Label ID="LabelQuestion" runat="server" Font-Size="Small" ForeColor="Black" />
                                                                    <br />
                                                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Vertical"
                                                                        Font-Size="X-Small" Width="900px" ForeColor="#666666">
                                                                    </asp:RadioButtonList>
                                                                    <br />
                                                                    <asp:Label ID="LabelAnswer" runat="server" Visible="false" Font-Size="Small" ForeColor="Black" />
                                                                    <asp:Label ID="ErrorLabel" runat="server" Text="Please select an option" Visible="false"
                                                                        ForeColor="Red" Font-Size="Small"></asp:Label>
                                                                </ItemTemplate>
                                                                <ControlStyle Width="600px" />
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:CommandField ShowEditButton="true" ShowCancelButton="false" 
                                                                EditText="Find correct answer" ControlStyle-Font-Size="Small" 
                                                                ControlStyle-Width="130px" >
<ControlStyle Font-Size="Small" Width="130px"></ControlStyle>
                                                            </asp:CommandField>
                                                        </Columns>
                                                        <PagerStyle  HorizontalAlign="Left" />
                                                        <SelectedRowStyle  Font-Bold="True"  />
                                                        <HeaderStyle 
                                                            Font-Names="Calibri" Font-Size="Small" />
                                                        <EmptyDataTemplate>
                                                            <asp:Label ID="lblempty" Text="No Records" runat="server">
                                                            </asp:Label>
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="6">
                                                    &nbsp;
                                                    <asp:Button ID="ImageButton1" runat="server" Text="End Test" class="btn btn-success" onclick="ImageButton1_Click"/>
                                                    <%--<asp:ImageButton ID="ImageButton1" runat="server" 
                                                        ImageUrl="~/Images/Test.png" onmouseover="this.src='../Images/Testover.png';" onmouseout="this.src='../Images/Test.png';" onclick="ImageButton1_Click" />--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="6">
                                                    &nbsp;
                                                    <asp:Label ID="lbl_error2" runat="server" Font-Bold="True" Font-Names="Calibri" 
                                                        Font-Size="Small" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="results" runat="server" style="font-family: Calibri; font-size: x-small; font-style: normal;">
                                                <td align="right" 
                                                    style="width: 321px; font-weight: bold; font-size: small; color: #444444">
                                                    <b>Total Number of Questions&nbsp; :</td>
                                                <td style="width: 59px; font-size: small; color: #444444">
                                                    <asp:Label ID="lbl_quest" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                                <td align="right" style="width: 215px; font-weight: bold; font-size: small; color: #444444">
                                                    Number of correct answers&nbsp; :</td>
                                                <td style="width: 73px; font-size: small; color: #444444">
                                                    <asp:Label ID="lbl_ans" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                                <td align="right" style="width: 99px; font-weight: bold; font-size: small; color: #444444">
                                                    Percentage&nbsp; :</td>
                                                <td style="font-size: small; color: #444444">
                                                    <asp:Label ID="lbl_percent" runat="server" Font-Bold="True"></asp:Label>
                                                    </b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
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
