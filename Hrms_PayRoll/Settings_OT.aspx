<%@ Page Language="C#" MasterPageFile="~/Hrms_PayRoll/PayRoll_Settings.master" AutoEventWireup="true" CodeFile="Settings_OT.aspx.cs" Inherits="PayRoll_Default" Title="OT Settings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>
<script language="javascript" type="text/javascript">
function fn_save()
{
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_otdays.value == "")
        {
            alert("Enter OT Days");
            aspnetForm.ctl00$ContentPlaceHolder1$txt_otdays.focus();
            return false;
        }
        else 
            {
                if(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_othours.value == "")
                {
                    alert("Enter OT Hours");
                    aspnetForm.ctl00$ContentPlaceHolder1$txt_othours.focus();
                    return false;
                }
                else
                { 
                    return true;  
                }
            }
}
</script>

<table width="100%" cellpadding="0" cellspacing="0">
                <tr valign="top">
                    <td valign="top" align="center" class="tdComposeHeader">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr bgcolor="Tan">
                    <td align="left" style="height: 35px; color: #ffffff;font-weight:bold;">
                        &nbsp;&nbsp;<img src="../Images/rp_arrow.gif" />&nbsp;PAY INPUT OT SETTINGS</td>
                </tr>
            </table>
            <table cellpadding="5px" cellspacing="1px" width="100%">
                <tr>
                    <td colspan="4" align="center">
                        <asp:Label ID="lbl_Error" runat="server" CssClass="Error" ForeColor="Red"></asp:Label>&nbsp;</td>
                </tr>
                <tr>
                    <td align="left" class="dComposeItemLabel" nowrap="nowrap" style="height: 36px; width: 150px;">
                        OT Days</td>
                    <td align="left" style="width: 633px; height: 36px" valign="baseline">
                        <input type="text" runat="server" id="txt_otdays" class="InputDefaultStyle" onkeydown="AllowOnlyNumeric1(event);" />
                        <%--<asp:TextBox ID="txt_otdays" runat="server" CssClass="InputDefaultStyle"></asp:TextBox>--%>
                        </td>
                    <td align="left" style="height: 36px" valign="baseline">
                    </td>
                </tr>
                <tr>
                    <td class="dComposeItemLabel" nowrap="nowrap" align="left" style="height: 34px; width: 150px;">
                        OT Hours</td>
                    <td align="left" valign="baseline" style="height: 34px; width: 633px;">
                    <input type="text" runat="server" id="txt_othours" class="InputDefaultStyle" onkeydown="AllowOnlyNumeric1(event);" />
                        <%--<asp:TextBox ID="txt_othours" runat="server" CssClass="InputDefaultStyle"></asp:TextBox>--%>
                        </td>
                    <td align="left" style="height: 34px" valign="baseline">
                    </td>
                    
                </tr>
                <tr>
                <td></td>
                <td><asp:Button ID="Button1" runat="server" OnClick="btn_save_Click" Text="save" OnClientClick="return fn_save();" /></td>
                    <td align="left" style="height: 34px" valign="baseline">
                </tr>
                </table>
                </td></tr>
            </table>
</asp:Content>

