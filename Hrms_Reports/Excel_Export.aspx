<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Excel_Export.aspx.cs" Inherits="Hrms_Reports_Excel_Export" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%--<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Welcome to HRMS</title>
    <link href="../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body class="body" leftmargin="5" rightmargin="5" topmargin="0">
    <form id="form1" runat="server">
        <div style="text-align: center">
            <table style="width: 100%; height: 100%">
                <tr>
                    <td align="left" background="../Images/bg.jpg" class="TextStyle" colspan="4" rowspan="1"
                        width="20%">
                        &nbsp;&nbsp;
                        <asp:Label ID="lblmsg" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>&nbsp;
                        <asp:Label ID="lbl_error" runat="server" Font-Bold="True" ForeColor="Red" Width="176px"></asp:Label>
                        <asp:ImageButton ID="btn_report" runat="server" ImageUrl="~/Images/Excel.png" onmouseover="this.src='../Images/Excelover.png';" onmouseout="this.src='../Images/Excel.png';"
                            OnClick="btn_report_Click" ImageAlign="AbsMiddle" />
                        <asp:ImageButton ID="btn_back" runat="server" ImageUrl="~/Images/Back.png" onmouseover="this.src='../Images/Backover.png';" onmouseout="this.src='../Images/Back.png';"
                            OnClick="btn_back_Click" ImageAlign="AbsMiddle" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" valign="top">
                        <asp:GridView ID="grd_execl" runat="server" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" HorizontalAlign="Center">
                        <RowStyle HorizontalAlign="Left" BackColor="#F7F6F3" ForeColor="#333333" Font-Size="Medium" /><HeaderStyle HorizontalAlign="Left" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Size="Small" />
                            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#284775" />
                            <EditRowStyle BackColor="#999999" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        </asp:GridView>
                        &nbsp;&nbsp;<br />
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="4" width="20%">
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="right" width="20%" background="../Images/bg.jpg" height="15px">
                        Powered by <a href="http://www.epayindia.com" target="_blank"><font color="#0099ff"
                            size="4px" title="Click to Know more about ePay Solutions">ePay</font></a>
                    </td>
                </tr>
            </table>
        </div>
        &nbsp;
    </form>
</body>
</html>
