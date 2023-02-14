<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Transport.aspx.cs" Inherits="PayrollReports_Transport" MasterPageFile="~/PayrollReports/Report.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>
<script language="javascript" type="text/javascript">

    function check_del() {
        if (document.aspnetForm.ctl00$ContentPlaceHolder1$txt_reason.value == "") {
            alert("Complete all the Details before Deleting");
            aspnetForm.ctl00$ContentPlaceHolder1$txt_reason.focus();
            return false;
        }
        else {
            return true;
        }
    }
    </script>
<table width="100%"><tr><td class="tdComposeHeader">
    <table runat="server" id="tab_combination" style="width: 100%; height: 70%">
        <tr>
            <td class="TextStyle" height="35" colspan="3" width="30%">
                <span class="Title">&nbsp;<img src="../Images/rp_arrow.gif" />
                    Transport Allocation</span></td>
        </tr>
        <tr>
            <td colspan="3" width="30%" align="center" class="TextStyle">
                <asp:Label ID="lbl_Error" runat="server" CssClass="TextStyle" ForeColor="Red" Font-Size="Small"
                    Width="65%"></asp:Label></td>
        </tr>
        <tr id="row_vehicle" runat="server">
            <td width="30%" align="right" class="TextStyle">
                Select By&nbsp; </td>
            <td width="40%" align="center">
                <asp:DropDownList ID="ddl_options" runat="server" CssClass="InputDDLStyle" 
                    AutoPostBack="True" onselectedindexchanged="ddl_options_SelectedIndexChanged" >
                    <asp:ListItem>Select </asp:ListItem>
                    <asp:ListItem>Report of Vehicles</asp:ListItem>
                    <asp:ListItem>Report of Employees</asp:ListItem>
                  
                   
                </asp:DropDownList></td>
            <td style="width: 100px">
            </td>
        </tr>
        <tr id="row_fromoption" runat="server">
            <td align="right" width="30%" class="TextStyle">
                Select Bus Number&nbsp; </td>
            <td width="40%" align="center">
                <asp:DropDownList ID="ddl_vehicles" runat="server" CssClass="InputDDLStyle"> 
                    
                </asp:DropDownList></td>
            <td style="width: 100px">
            </td>
        </tr>
        <tr id="route" runat="server">
            <td align="right" valign="middle" width="30%" style="height: 61px" class="TextStyle">
                &nbsp;</td>
            <td width="40%" align="center" style="height: 61px">
                &nbsp;</td>
            <td style="width: 100px; height: 61px;">
            </td>
        </tr>
        <tr>
            <td align="center" colspan="3" valign="bottom" class="TextStyle">
                &nbsp;
                <asp:ImageButton ID="btn_transfer" runat="server" ImageUrl="~/Images/Show_Report.jpg" 
                  ImageAlign="AbsMiddle" onclick="btn_transfer_Click"/>
                
            </td>
        </tr>
        <tr><td>
        </td><td>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Error" runat="server"></asp:Label></td></tr>
        </table>
    <table runat="server" id="tab_transfering" width="100%">
        <tr>
            <td align="center" valign="middle">
                <%--  <asp:ImageButton ID="btn_yes" ImageUrl="~/Images/Yes.jpg" runat="server" title="Click to Transfer" OnClick="btn_yes_Click" ImageAlign="AbsMiddle" />&nbsp;
                <asp:ImageButton ID="btn_no" ImageUrl="~/Images/No.jpg" runat="server" title="Click to Cancel Transfer" OnClick="btn_no_Click" ImageAlign="AbsMiddle" /></td>--%>

                    </td>
        </tr>
    </table>
    <table runat="server" id="Table1" width="100%">
        <tr>
            <td align="center" valign="middle">
              <%--  <asp:ImageButton ID="btn_yes" ImageUrl="~/Images/Yes.jpg" runat="server" title="Click to Transfer" OnClick="btn_yes_Click" ImageAlign="AbsMiddle" />&nbsp;
                <asp:ImageButton ID="btn_no" ImageUrl="~/Images/No.jpg" runat="server" title="Click to Cancel Transfer" OnClick="btn_no_Click" ImageAlign="AbsMiddle" /></td>--%>

                    </td>
        </tr>
    </table></td></tr></table>
</asp:Content>