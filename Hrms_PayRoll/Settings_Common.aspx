<%@ Page Language="C#" MasterPageFile="~/Hrms_PayRoll/PayRoll_Settings.master" AutoEventWireup="true"
    CodeFile="Settings_Common.aspx.cs" Inherits="PayRoll_Default" Title="Common Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>
<script language="javascript" type="text/javascript">
function fn_save()
{
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$rdo_option[0].checked == true && document.aspnetForm.ctl00$ContentPlaceHolder1$rdo_option[0].value == "D")
            {
                if(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_calcdays.value == "")
                {
                    alert("Enter Calc Days");
                    aspnetForm.ctl00$ContentPlaceHolder1$txt_calcdays.focus();
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
                &nbsp;&nbsp;<img src="../Images/rp_arrow.gif" />&nbsp;PAY COMMON SETTINGS</td>
        </tr>
    </table>
    <table cellpadding="5px" cellspacing="1px" width="100%">
        <tr>
            <td colspan="4" align="center">
                <asp:Label ID="lbl_Error" runat="server" CssClass="Error" ForeColor="Red"></asp:Label></td>
        </tr>
        <tr>
            <td align="left" class="dComposeItemLabel" nowrap="nowrap" style="height: 36px; width: 150px;">
                Choose Option</td>
            <td align="left" style="width: 633px; height: 36px" valign="baseline">
                <asp:RadioButtonList ID="rdo_option" runat="server" RepeatDirection="Horizontal"
                    OnSelectedIndexChanged="rdo_option_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem Selected="True" Value="D">Days</asp:ListItem>
                    <asp:ListItem Value="M">Month</asp:ListItem>
                    <asp:ListItem Value="W">week</asp:ListItem>
                </asp:RadioButtonList></td>
            <td align="left" style="height: 36px" valign="baseline">
            </td>
        </tr>
    </table>
    <table bgcolor="aqua" id="tab_check" runat="server" cellpadding="5" cellspacing="1"
        width="100%">
        <tr>
            <%--<td class="dComposeItemLabel" nowrap="nowrap" align="left" style="height: 34px; width: 150px;">
                Calc Days</td>--%>
                <td align="right">
                    <asp:Label ID="lbl_calc" runat="server" CssClass="dComposeItemLabel" Text="calc days"></asp:Label></td>
            <td align="left" valign="baseline" style="height: 34px; width: 633px;">
            <input type="text" runat="server" id="txt_calcdays" class="InputDefaultStyle" onkeydown="AllowOnlyNumeric1(event);" />
                <%--&nbsp;<asp:TextBox ID="txt_calcdays" runat="server" CssClass="InputDefaultStyle"></asp:TextBox>--%>
                </td>
            <td align="left" style="height: 34px" valign="baseline">
            </td>
        </tr>
        <tr>
            <%--<td align="left" class="dComposeItemLabel" nowrap="nowrap" style="width: 150px">
                &nbsp;Week Holiday1</td>--%>
                <td align="right">
                    <asp:Label ID="lbl_weekholiday1" runat="server" CssClass="dComposeItemLabel" Text="Week Holiday1"></asp:Label></td>
            <td align="left" valign="baseline" style="width: 633px">
                &nbsp;<asp:DropDownList ID="ddl_week_holiday1" runat="server" CssClass="InputDefaultStyle">
                    <asp:ListItem Value="0">None</asp:ListItem>
                    <asp:ListItem Value="1">Sunday</asp:ListItem>
                    <asp:ListItem Value="2">Monday</asp:ListItem>
                    <asp:ListItem Value="3">Tuesday</asp:ListItem>
                    <asp:ListItem Value="4">wednesday</asp:ListItem>
                    <asp:ListItem Value="5">Thursday</asp:ListItem>
                    <asp:ListItem Value="6">Friday</asp:ListItem>
                    <asp:ListItem Value="7">Saturday</asp:ListItem>
                </asp:DropDownList></td>
            <td align="left" valign="baseline">
            </td>
        </tr>
        <tr>
            <td>
            <asp:Label ID="lbl_weekholiday2" runat="server" CssClass="dComposeItemLabel" Text="week Holiday2"></asp:Label></td>
            <%--<td class="dComposeItemLabel" nowrap align="left" style="width: 150px">
                &nbsp;Week Holiday2</td>--%>
            <td align="left" valign="baseline" style="width: 633px">
                &nbsp;<asp:DropDownList ID="ddl_week_holiday2" runat="server" CssClass="InputDefaultStyle">
                    <asp:ListItem Value="0">None</asp:ListItem>
                    <asp:ListItem Value="1">Sunday</asp:ListItem>
                    <asp:ListItem Value="2">Monday</asp:ListItem>
                    <asp:ListItem Value="3">Tuesday</asp:ListItem>
                    <asp:ListItem Value="4">wednesday</asp:ListItem>
                    <asp:ListItem Value="5">Thursday</asp:ListItem>
                    <asp:ListItem Value="6">Friday</asp:ListItem>
                    <asp:ListItem Value="7">Saturday</asp:ListItem>
                </asp:DropDownList>
                
            </td>
        </tr>
        <tr>
        <td></td>
        <td>
            <br />
            <asp:Button ID="btn_save" runat="server" Text="save" OnClick="btn_save_Click" OnClientClick="return fn_save();" /></td>
            <td align="left" valign="baseline"></td>
        </tr>
    </table>
    </td></tr>  
    </table>
</asp:Content>
