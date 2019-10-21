<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Transfer.aspx.cs" Inherits="Hrms_Company_Default" Title="Welcome to HRMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>
<script language="javascript" type="text/javascript">

function check_del()
{
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_reason.value == "")
        {
            alert("Complete all the Details before Deleting");
            aspnetForm.ctl00$ContentPlaceHolder1$txt_reason.focus();
            return false;
        }                        
        else
        { 
              return true;  
        }
}
    </script>
<table width="100%"><tr><td class="tdComposeHeader">
    <table runat="server" id="tab_combination" style="width: 100%; height: 70%">
        <tr>
            <td class="TextStyle" height="35" colspan="4" width="30%" 
                >
                <span class="Title" 
                    style="font-family: Calibri; font-size: medium; font-weight: bold;>&nbsp;
                    Transfer Employees</span></td>
        </tr>
        <tr>
            <td colspan="4" width="30%" align="center" class="TextStyle">
                <asp:Label ID="lbl_Error" runat="server" CssClass="TextStyle" ForeColor="Red" Font-Size="Small"
                    Width="65%" Font-Names="Calibri"></asp:Label></td>
        </tr>
        <tr id="row_from" runat="server">
            <td align="right" class="TextStyle" style="font-family: calibri; width: 14%;">
                &nbsp;</td>
            <td align="left" class="TextStyle" style="font-family: calibri; width: 11%;">
                Select From Branch</td>
            <td align="left" style="width: 10%">
                <asp:DropDownList ID="ddl_frombranch" runat="server" 
                    CssClass="form-control" AutoPostBack="True" 
                    OnSelectedIndexChanged="ddl_frombranch_SelectedIndexChanged" Height="16px" 
                    Width="200px">
                   
                </asp:DropDownList></td>
            <td style="width: 100px">
            </td>
        </tr>
        <tr id="row_fromoption" runat="server">
            <td align="right" class="TextStyle" style="font-family: calibri; width: 14%;">
                &nbsp;</td>
            <td align="left" class="TextStyle" style="font-family: calibri; width: 11%;">
                Select From Option</td>
            <td align="left" style="width: 10%">
                <asp:DropDownList ID="ddl_fromoption" runat="server" 
                    CssClass="form-control" AutoPostBack="True" 
                    OnSelectedIndexChanged="ddl_fromoption_SelectedIndexChanged" Width="200px">
                    <asp:ListItem Value="0">Select</asp:ListItem>
                    <asp:ListItem Value="1">Show All Employees</asp:ListItem>
                    <asp:ListItem Value="2">Department</asp:ListItem>
                    <asp:ListItem Value="3">Division</asp:ListItem>
                    <asp:ListItem Value="4">Level</asp:ListItem>
                    <asp:ListItem Value="5">Designation</asp:ListItem>
                    <asp:ListItem Value="6">Grade</asp:ListItem>
                    <asp:ListItem Value="7">Category</asp:ListItem>
                    <asp:ListItem Value="8">Job Type</asp:ListItem>
                    <asp:ListItem Value="9">Shift</asp:ListItem>
                    <asp:ListItem Value="10">Project-Site</asp:ListItem>
                </asp:DropDownList></td>
            <td style="width: 100px">
            </td>
        </tr>
        <tr id="row_fromfilter"  runat="server">
            <td align="right" class="TextStyle" style="font-family: calibri; width: 14%;">
                &nbsp;</td>
            <td align="left" class="TextStyle" style="font-family: calibri; width: 11%;">
                Select From</td>
            <td align="left" valign="middle" style="width: 10%">
                <asp:DropDownList ID="ddl_fromfilter" runat="server" 
                    CssClass="form-control"
                    OnSelectedIndexChanged="ddl_fromfilter_SelectedIndexChanged" 
                    AutoPostBack="True" Height="18px" Width="200px">
                </asp:DropDownList></td>
            <td style="width: 100px">
            </td>
        </tr>
        <tr id="row_chkemployee" runat="server">
            <td align="right" class="TextStyle" style="font-family: calibri; width: 14%;">
                &nbsp;</td>
            <td align="left" class="TextStyle" style="font-family: calibri; width: 11%;">
                Employee List</td>
            <td align="left" valign="middle" style="width: 10%">
                <asp:CheckBox ID="chkall_Employee" runat="server" AutoPostBack="True" 
                    CssClass="report_chkall" Text="Select All Employees" Width="41%" 
                    OnCheckedChanged="chkall_Employee_CheckedChanged" Font-Names="Calibri" />
                <div class="qrychkbox_big" style="left: 0px; top: 0px; height: 170px">
                    <asp:CheckBoxList ID="chk_Empcode" runat="server" CssClass="form-control" Height="200px"
                        Width="95%">
                    </asp:CheckBoxList>
                </div>
                </td>
            <td style="width: 100px">
            </td>
        </tr>
        <tr id="row_tobranch" runat="server">
            <td align="right" class="TextStyle" style="font-family: calibri; width: 14%;">
                &nbsp;</td>
            <td align="left" class="TextStyle" style="font-family: calibri; width: 11%;">
                Transfer To Branch</td>
            <td align="left" style="width: 10%">
                <asp:DropDownList ID="ddl_tobranch" runat="server" CssClass="form-control"
                    AutoPostBack="True" 
                    OnSelectedIndexChanged="ddl_tobranch_SelectedIndexChanged" Width="200px">
                </asp:DropDownList></td>
            <td style="width: 100px">
            </td>
        </tr>
        <tr id="row_tooption" runat="server">
            <td align="right" class="TextStyle" style="font-family: calibri; width: 14%;">
                &nbsp;</td>
            <td align="left" class="TextStyle" style="font-family: calibri; width: 11%;">
                Transfer To Option</td>
            <td align="left" style="width: 10%">
                <asp:DropDownList ID="ddl_tooption" runat="server" CssClass="form-control"
                    AutoPostBack="True" 
                    OnSelectedIndexChanged="ddl_tooption_SelectedIndexChanged" Width="200px">
                 <asp:ListItem Value="0">Select</asp:ListItem>
                    <asp:ListItem Value="2">Department</asp:ListItem>
                    <asp:ListItem Value="3">Division</asp:ListItem>
                    <asp:ListItem Value="4">Level</asp:ListItem>
                    <asp:ListItem Value="5">Designation</asp:ListItem>
                    <asp:ListItem Value="6">Grade</asp:ListItem>
                    <asp:ListItem Value="7">Category</asp:ListItem>
                    <asp:ListItem Value="8">Job Type</asp:ListItem>
                    <asp:ListItem Value="9">Shift</asp:ListItem>
                    <asp:ListItem Value="10">Project-Site</asp:ListItem>
                </asp:DropDownList></td>
            <td style="width: 100px">
            </td>
        </tr>
        <tr id="row_tofilter" runat="server">
            <td align="right" class="TextStyle" style="font-family: calibri; width: 14%;">
                &nbsp;</td>
            <td align="left" class="TextStyle" style="font-family: calibri; width: 11%;">
                Transfer To</td>
            <td align="left" style="width: 10%">
                <asp:DropDownList ID="ddl_tofilter" runat="server" CssClass="form-control" 
                    Width="200px">
                </asp:DropDownList></td>
            <td style="width: 100px">
            </td>
        </tr>
        <tr id="row_reason" runat="server">
            <td align="right" valign="middle" 
                style="height: 61px; font-family: calibri; width: 14%;" class="TextStyle">
                &nbsp;</td>
            <td align="left" valign="middle" 
                style="height: 61px; font-family: calibri; width: 11%;" class="TextStyle">
                Reason for Transferring</td>
            <td align="left" style="height: 61px; width: 10%;">
                <textarea id="txt_reason" runat="server" rows="3" style="width: 200px" CssClass="form-control"></textarea></td>
            <td style="width: 100px; height: 61px;">
            </td>
        </tr>
        <tr id="row_mMydet" runat="server">
            <td align="center" colspan="4" class="TextStyle" style="font-family: calibri">
                My Details</td>
        </tr>
        <tr id="row_empcode" runat="server">
            <td align="right" class="TextStyle" style="font-family: calibri" colspan="2">
                Emp Code&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
            <td align="left" style="width: 10%">
            <input type="text" id="txt_empcode" runat="server" CssClass="form-control"
                    onkeydown="AllowOnlyNumeric1(event);" style="width: 200px" />
                <%--<asp:TextBox ID="txt_empcode" runat="server" CssClass="InputDefaultStyle" Width="45%"></asp:TextBox>--%>
                </td>
            <td style="width: 100px">
            </td>
        </tr>
        <tr id="row_emppwd" runat="server">
            <td align="right" class="TextStyle" style="font-family: calibri" colspan="2">
                Password&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
            <td align="left" style="width: 10%">
                <asp:TextBox ID="txt_emppwd" runat="server" CssClass="form-control"
                    Width="200px"></asp:TextBox></td>
            <td style="width: 100px">
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4" valign="bottom" class="TextStyle">
               <%-- <asp:ImageButton ID="btn_back" runat="server" ImageUrl="~/Images/back.png" onmouseover="this.src='../Images/Backover.png';" onmouseout="this.src='../Images/Back.png';" ImageAlign="AbsMiddle" />--%>
                <asp:Button ID="btn_back" runat="server" Text="Back" CssClass="btn btn-primary" 
                      />
                &nbsp 
                <%--<asp:ImageButton ID="btn_transfer" runat="server" 
                    ImageUrl="~/Images/Transfer.png" onmouseover="this.src='../Images/Transferover.png';" onmouseout="this.src='../Images/Transfer.png';" OnClick="btn_transfer_Click" 
                    OnClientClick="return check_del();" ImageAlign="AbsMiddle" />--%>
                <asp:Button ID="btn_transfer" runat="server" Text="Tansfer" OnClientClick="return check_del();" OnClick="btn_transfer_Click" class="btn btn-success" />
                    </td>
        </tr>
    </table>
    <table runat="server" id="tab_transfering" width="100%">
        <tr>
            <td align="center" >
                <asp:Label ID="lbl_confirmation" runat="server"  Font-Size="Small"
                    Width="80%" ></asp:Label></td>
        </tr>
        <tr>
            <td align="center" valign="middle">
                <asp:ImageButton ID="btn_yes" ImageUrl="~/Images/Yes.jpg" runat="server" title="Click to Transfer" OnClick="btn_yes_Click" ImageAlign="AbsMiddle" />&nbsp;
                <asp:ImageButton ID="btn_no" ImageUrl="~/Images/No.jpg" runat="server" title="Click to Cancel Transfer" OnClick="btn_no_Click" ImageAlign="AbsMiddle" /></td>
        </tr>
    </table></td></tr></table>
</asp:Content>
