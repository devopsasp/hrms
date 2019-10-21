<%@ Page Language="C#" MasterPageFile="~/Hrms_PayRoll/PayRoll_Settings.master" AutoEventWireup="true" CodeFile="Settings_PT.aspx.cs" Inherits="Hrms_PayRoll_Default2" Title="PT Settings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <script language="javascript" type="text/javascript">

function fn_chkall_months(chkid,chklistid)
    { 
        var chkBoxList = document.getElementById(chklistid);
        var chkBoxCount= chkBoxList.getElementsByTagName("input");

       if(document.getElementById(chkid).checked==true)
       {       
            for(var i=0;i<chkBoxCount.length;i++) 
            {
                chkBoxCount[i].checked = true;
            }                
       }
       else
       {       
            for(var i=0;i<chkBoxCount.length;i++) 
            {
                chkBoxCount[i].checked = false;
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
                        &nbsp;&nbsp;<img src="../Images/rp_arrow.gif" />&nbsp;PAY PT SETTINGS</td>
                </tr>
            </table>
            <table cellpadding="2px" cellspacing="1px" width="100%">
                <tr>
                    <%--<td align="left" class="dComposeItemLabel" nowrap="nowrap" style="width: 150px; height: 20px">
                    </td>--%>
                    <td align="center" style="width: 633px; height: 20px" valign="baseline" colspan="3">
                        <asp:Label ID="lbl_Error" runat="server" CssClass="Error" ForeColor="Red"></asp:Label></td>
                    <%--<td align="left" style="height: 20px" valign="baseline">
                    </td>--%>
                </tr>
                
                <tr>
                    <td align="left" class="dComposeItemLabel" nowrap="nowrap" style="width: 150px; height: 20px">
                        PT Type</td>
                    <td align="left" style="width: 633px; height: 20px" valign="baseline">
                        <asp:RadioButtonList ID="rrl_type" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="E" Selected="True">Earn Amount</asp:ListItem>
                        <asp:ListItem Value="A">Actual Amount</asp:ListItem>
                        </asp:RadioButtonList></td>
                    <td align="left" style="height: 20px" valign="baseline">
                    </td>
                </tr>
                <tr>
                    <td class="dComposeItemLabel" nowrap="nowrap" align="left" style="height: 34px; width: 150px;">
                        PT Months</td>
                    <td align="left" valign="baseline" style="height: 34px; width: 633px;">
                        
                    
                                                <div runat="server" id="div_chkl_month" class="qrychkbox_big" style="height: 172px;
                                                    left: 0px; top: 0px; width: 151px;" align="center">
                                                    <asp:CheckBoxList ID="chkl_month" runat="server" Width="100%">
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
                                                    </asp:CheckBoxList></div>
                                                <input type="checkbox" id="chkall_months" runat="server" onclick="javascript: fn_chkall_months(this.id,'ctl00_ContentPlaceHolder1_chkl_month')" enableviewstate="false"  />Select
                                                All
                    &nbsp;
                    <td align="left" style="height: 34px" valign="baseline">
                    </td>
                    
                </tr>
                <tr>
                    <td align="left" class="dComposeItemLabel" nowrap="nowrap" style="width: 150px; height: 34px">
                    </td>
                    <td align="left" style="width: 633px; height: 34px" valign="baseline">
                        <br />
                        <asp:Button ID="btn_save" runat="server"  Text="save" OnClick="btn_save_Click" /></td>
                    <td align="left" style="height: 34px" valign="baseline">
                    </td>
                </tr>
                
                </table>
                </td></tr>
            </table>
</asp:Content>

