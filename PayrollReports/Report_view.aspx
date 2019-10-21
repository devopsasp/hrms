<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report_view.aspx.cs" Inherits="Hrms_Reports_Report_view" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Report View</title>
    
    <link href="../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        .style1
        {
            width: 786px;
            height: 555px;
        }
    </style>
    <style type="text/css">
    .verticaltext
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 100%; height: 100%">
                <tr>
                    <td align="left" background="../Images/bg.jpg" class="TextStyle" style="width: 786px">
                        <asp:Label ID="lblmsg" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>&nbsp;
                        <asp:Label ID="lbl_error" runat="server" Font-Bold="True" ForeColor="Red" Width="176px"></asp:Label>
                        <%--<asp:ImageButton ID="btn_report" runat="server" ImageUrl="../Images/Excel_Report.jpg"
                            OnClick="btn_report_Click" ImageAlign="AbsMiddle" />--%>
                        
                        <asp:Button ID="btnBack" runat="server" CssClass="btn btn-info"  Text="Back" OnClick="btnBack_Click" />
                        <%--<asp:ImageButton ID="btn_back" runat="server" ImageUrl="~/Images/back.jpg" OnClick="btn_back_Click"
                            ImageAlign="AbsMiddle" />--%>
                    </td>
                </tr>
                <tr align="center">
                    <td align="center" class="style1">
                        <%--<CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" Height="50px" Width="350px" />--%>
                        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                            AutoDataBind="true" BorderColor="#6E6E6E" BorderStyle="Solid" BorderWidth="1px" 
                            GroupTreeStyle-BorderColor="#8F8F8F" GroupTreeStyle-BorderStyle="Solid" 
                            GroupTreeStyle-BorderWidth="1px" HasCrystalLogo="False" Height="50px" 
                            Width="350px" GroupTreeStyle-BackColor="#E0E0E0" />
                        <%--<CR:CrystalReportViewer ID="CrystalReportViewer2" runat="server" AutoDataBind="true" />--%></td>
                </tr>
                <tr>
                    <td colspan="4" align="right" width="20%" background="../Images/bg.jpg" height="1px">
                        Powered by <a href="http://www.epayindia.com" target="_blank"><font color="#0099ff"
                            size="4px" title="Click to Know more about ePay Solutions">ePay</font></a>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
