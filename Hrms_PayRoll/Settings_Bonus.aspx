<%@ Page Language="C#" MasterPageFile="~/Hrms_PayRoll/PayRoll_Settings.master" AutoEventWireup="true"
    CodeFile="Settings_Bonus.aspx.cs" Inherits="Hrms_PayRoll_Default" Title="Bonus Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>
<script language="javascript" type="text/javascript">
function fn_save()
{
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_bonuslimit.value == "")
        {
            alert("Enter Bonus Limit");
            aspnetForm.ctl00$ContentPlaceHolder1$txt_bonuslimit.focus();
            return false;
        }
        else
        { 
              return true;  
        }
}
</script>

    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td valign="top" align="center" class="tdComposeHeader">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr bgcolor="Tan">
            <td align="left" style="height: 35px; color: #ffffff;font-weight:bold;">
                &nbsp;&nbsp;<img src="../Images/rp_arrow.gif" />&nbsp;SETTINGS BONUS</td>
        </tr>
    </table>
    <table cellpadding="5px" cellspacing="1px" width="100%">
        <tr>
            <td colspan="4" align="center" style="">
                <asp:Label ID="lbl_Error" runat="server" CssClass="Error" ForeColor="Red"></asp:Label></td>
        </tr>
        <tr>
            <td align="left" class="dComposeItemLabel" nowrap="nowrap" style="height: 36px; width: 150px;">
                Bonus From Month</td>
            <td align="left" style="width: 633px; height: 36px" valign="baseline">
                &nbsp;<asp:DropDownList ID="ddl_f_month" runat="server" CssClass="InputDefaultStyle">
                    <asp:ListItem Value="01">January</asp:ListItem>
                    <asp:ListItem Value="02">Febraury</asp:ListItem>
                    <asp:ListItem Value="03">March</asp:ListItem>
                    <asp:ListItem Value="04">April</asp:ListItem>
                    <asp:ListItem Value="05">May</asp:ListItem>
                    <asp:ListItem Value="06">June</asp:ListItem>
                    <asp:ListItem Value="07">July</asp:ListItem>
                    <asp:ListItem Value="08">August</asp:ListItem>
                    <asp:ListItem Value="09">September</asp:ListItem>
                    <asp:ListItem Value="10">October</asp:ListItem>
                    <asp:ListItem Value="11">November</asp:ListItem>
                    <asp:ListItem Value="12">December</asp:ListItem>
                </asp:DropDownList></td>
            <td align="left" style="height: 36px" valign="baseline">
            </td>
        </tr>
        <tr>
            <td class="dComposeItemLabel" nowrap="nowrap" align="left" style="height: 34px; width: 150px;">
                Bonus To Month</td>
            <td align="left" valign="baseline" style="height: 34px; width: 633px;">
                &nbsp;<asp:DropDownList ID="ddl_t_month" runat="server" CssClass="InputDefaultStyle">
                    <asp:ListItem Value="01">January</asp:ListItem>
                    <asp:ListItem Value="02">Febraury</asp:ListItem>
                    <asp:ListItem Value="03">March</asp:ListItem>
                    <asp:ListItem Value="04">April</asp:ListItem>
                    <asp:ListItem Value="05">May</asp:ListItem>
                    <asp:ListItem Value="06">June</asp:ListItem>
                    <asp:ListItem Value="07">July</asp:ListItem>
                    <asp:ListItem Value="08">August</asp:ListItem>
                    <asp:ListItem Value="09">September</asp:ListItem>
                    <asp:ListItem Value="10">October</asp:ListItem>
                    <asp:ListItem Value="11">November</asp:ListItem>
                    <asp:ListItem Value="12">December</asp:ListItem>
                </asp:DropDownList>
            <td align="left" style="height: 34px" valign="baseline">
            </td>
        </tr>
        <tr>
            <td align="left" class="dComposeItemLabel" nowrap="nowrap" style="width: 150px; height: 34px">
                Bonus Type</td>
            <td align="left" style="width: 150px; height: 34px;" valign="baseline">
                <asp:RadioButtonList ID="rr_type" runat="server" RepeatDirection="Horizontal" Width="111px">
                    <asp:ListItem Value="1">Y</asp:ListItem>
                    <asp:ListItem Value="2">M</asp:ListItem>
                </asp:RadioButtonList></td>
            <td align="left" style="height: 34px" valign="baseline">
            </td>
        </tr>
        <tr>
            <td class="dComposeItemLabel" nowrap align="left" style="width: 150px; height: 29px;">
                Bonus Limit</td>
            <td align="left" valign="baseline" style="width: 633px; height: 29px;">
            <input type="text" runat="server" id="txt_bonuslimit" class="InputDefaultStyle" onkeydown="AllowOnlyNumeric1(event);" />
                <%--&nbsp;<asp:TextBox ID="txt_bonuslimit" runat="server" CssClass="InputDefaultStyle"></asp:TextBox>--%>
                <br />
            </td>
        </tr><%--<tr style="width: 150px; height: 2px;"><td style="height: 9px"></td><td style="height: 9px"></td><td style="height: 9px"></td></tr>--%>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="btn_save" runat="server" Text="save" OnClick="btn_save_Click" OnClientClick="return fn_save();" /></td>
            <td align="left" valign="baseline">
            </td>
        </tr>
    </table>
    </td></tr>  
    </table>
</asp:Content>
