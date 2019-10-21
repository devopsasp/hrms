<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InOutMode.aspx.cs" Inherits="Hrms_TimeAndAttenence_InOutMode" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ePay-HRMS</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        
        details
        <!--listview to get shift- -->
        <asp:ListView ID="lv_shiftDetails" runat="server" InsertItemPosition="LastItem" 
            onitemcommand="lv_shiftDetails_ItemCommand">
            <LayoutTemplate>
            <table border="1">
                <tr>
                    <th>ShiftCode</th>
                    <th>StartTime</th>
                    <th>BreakTime</th>
                    <th>EndTime</th>
                    <th>ShiftIndicator</th>
                </tr>
                <tr id="itemplaceholder" runat="server"></tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
            <tr>
                <td><asp:Label ID="lblshiftcode" runat="server" Text='<%#Eval("shiftcode") %>'></asp:Label></td>
                <td><asp:Label ID="lblstarttime" runat="server" Text='<%#Eval("starttime") %>'></asp:Label></td>
                <td><asp:Label ID="lblbreaktime" runat="server" Text='<%#Eval("breaktime") %>'></asp:Label></td>
                <td><asp:Label ID="lblendtime" runat="server" Text='<%#Eval("endtime") %>'></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddl_shiftIndicator" runat="server">
                        <asp:ListItem>CurrentDay</asp:ListItem> 
                        <asp:ListItem>Night</asp:ListItem>
                        <asp:ListItem>NextDay</asp:ListItem>
                    </asp:DropDownList>
                 </td>
                 <td><asp:Button ID="cmd_insert" runat="server" Text="Insert" CommandName="Edit"/></td>
            </tr>
            </ItemTemplate>
            <InsertItemTemplate>
                <tr>
                    <td><asp:TextBox ID="txtshiftcode" runat="server"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtstarttime" runat="server"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtbreaktime" runat="server"></asp:TextBox></td>
                    <td><asp:TextBox ID="endtime" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:DropDownList ID="ddl_shiftIndicator_ins" runat="server">
                            <asp:ListItem>CurrentDay</asp:ListItem> 
                            <asp:ListItem>Night</asp:ListItem>
                            <asp:ListItem>NextDay</asp:ListItem>
                        </asp:DropDownList>
                   </td>
                   <td><asp:Button ID="cmd_insert" runat="server" Text="Insert" CommandName="Insert"/></td>
                   <td><asp:Button ID="cmd_cancel" runat="server" Text="Cancel" CommandName="Cancel" /></td>
                </tr>
            </InsertItemTemplate>
        
        
        </asp:ListView>
        
        
        
    <table cellpadding="2">
        <tr>
            <td><asp:Label ID="lblintime" runat="server" Text="Intime"></asp:Label></td>
                       
            <td>
                <asp:TextBox ID="txt_intime_hr" runat="server" style="width: 100px" Enabled="False"></asp:TextBox>
                <asp:TextBox ID="txt_intime_min" runat="server" style="width: 100px" Enabled="False"></asp:TextBox>
                <asp:TextBox ID="txt_intime_sec" runat="server" style="width: 100px" Enabled="False"></asp:TextBox>
            </td>
            
        </tr>
        <tr>
            <td><asp:Label ID="lbleilimit" runat="server" Text="Early InLimit"></asp:Label></td>
             <td>
                <asp:TextBox ID="txt_early_inlimit_hr" runat="server" style="width: 100px" Enabled="False"></asp:TextBox>
                <asp:TextBox ID="txt_early_inlimit_min" runat="server" style="width: 100px" Enabled="False"></asp:TextBox>
                <asp:TextBox ID="txt_early_inlimit_sec" runat="server" style="width: 100px" Enabled="False"></asp:TextBox>
            </td>
            <%--<td><asp:TextBox ID="txt_early_inlimit" runat="server"></asp:TextBox></td>--%>
        </tr>
        <tr>
            <td><asp:Label ID="Label1" runat="server" Text="Shift LateIn and EarlyOut Limit"></asp:Label></td>  
            <td>
                <asp:TextBox ID="txt_shift_lateIn_EarlyOut_limit_hr" runat="server" style="width: 100px" Enabled="False"></asp:TextBox>
                <asp:TextBox ID="txt_shift_lateIn_EarlyOut_limit_min" runat="server" style="width: 100px" Enabled="False"></asp:TextBox>
                <asp:TextBox ID="txt_shift_lateIn_EarlyOut_limit_sec" runat="server" style="width: 100px" Enabled="False"></asp:TextBox>
            </td>                   
            <%--<td><asp:TextBox ID="txt_shift_lateIn_EarlyOut_limit" runat="server"></asp:TextBox></td>--%>
        </tr>
        <tr>
            <td><asp:Label ID="Label2" runat="server" Text="Lunch EarlyIn and LateOut Limit"></asp:Label></td>
            <td>
                <asp:TextBox ID="txt_lunch_earlyin_lateOut_limit_hr" runat="server" style="width: 100px" Enabled="False"></asp:TextBox>
                <asp:TextBox ID="txt_lunch_earlyin_lateOut_limit_min" runat="server" style="width: 100px" Enabled="False"></asp:TextBox>
                <asp:TextBox ID="txt_lunch_earlyin_lateOut_limit_sec" runat="server" style="width: 100px" Enabled="False"></asp:TextBox>
            </td>
            <%--<td><asp:TextBox ID="txt_lunch_earlyin_lateOut_limit" runat="server"></asp:TextBox></td>--%>
        </tr>
        <tr>
            <td><asp:Label ID="Label3" runat="server" Text="HalfDay Work hours Limit"></asp:Label></td>
            <td>
                <asp:TextBox ID="txt_halfday_workHrs_limit_hr" runat="server" style="width: 100px" Enabled="False"></asp:TextBox>
                <asp:TextBox ID="txt_halfday_workHrs_limit_min" runat="server" style="width: 100px" Enabled="False"></asp:TextBox>
                <asp:TextBox ID="txt_halfday_workHrs_limit_sec" runat="server" style="width: 100px" Enabled="False"></asp:TextBox>
            </td>
            <%--<td><asp:TextBox ID="txt_halfday_workHrs_limit" runat="server"></asp:TextBox></td>--%>
        </tr>
        <tr>
            <td><asp:Label ID="Label4" runat="server" Text="OverTime Limit"></asp:Label></td>
            <td>
                <asp:TextBox ID="txt_OT_limit_hr" runat="server" style="width: 100px" Enabled="False"></asp:TextBox>
                <asp:TextBox ID="txt_OT_limit_min" runat="server" style="width: 100px" Enabled="False"></asp:TextBox>
                <asp:TextBox ID="txt_OT_limit_sec" runat="server" style="width: 100px" Enabled="False"></asp:TextBox>
            </td>
            <%--<td><asp:TextBox ID="txt_OT_limit" runat="server"></asp:TextBox></td>--%>
        </tr>
        <tr>
            <td><asp:Label ID="Label5" runat="server" Text="Permission"></asp:Label></td>
            <td>
                <asp:TextBox ID="txt_permission_limit_hr" runat="server" style="width: 100px" ></asp:TextBox>
                <asp:TextBox ID="txt_permission_limit_min" runat="server" style="width: 100px" ></asp:TextBox>
                <asp:TextBox ID="txt_permission_limit_sec" runat="server" style="width: 100px" ></asp:TextBox>
            </td>
            <%--<td><asp:TextBox ID="txt_permission_limit" runat="server"></asp:TextBox></td>--%>
        </tr>
        <tr>
            <td><asp:Label ID="Label6" runat="server" Text="Monthly Leave days Limit"></asp:Label></td>
            <td><asp:TextBox ID="txt_monthly_leaveDays_limit" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:Button ID="cmd_apply" runat="server" Text="Apply" onclick="cmd_apply_Click" /></td>
            <td><asp:Button ID="cmd_clear" runat="server" Text="Clear" /></td>
        
        </tr>
        
    </table>
    
    </div>
   
    <asp:NumericUpDownExtender ID="ntxt_intime_hr" runat="server" TargetControlID="txt_intime_hr" Maximum="12" Minimum="0" Width="75" ></asp:NumericUpDownExtender>
    <asp:NumericUpDownExtender ID="ntxt_intime_min" runat="server" TargetControlID="txt_intime_min" Maximum="60" Minimum="0" Width="75" ></asp:NumericUpDownExtender>
    <asp:NumericUpDownExtender ID="ntxt_intime_sec" runat="server" TargetControlID="txt_intime_sec" Maximum="60" Minimum="0" Width="75" ></asp:NumericUpDownExtender>
    
    <asp:NumericUpDownExtender ID="NumericUpDownExtender4" runat="server" TargetControlID="txt_early_inlimit_hr" Maximum="12" Minimum="0" Width="75" ></asp:NumericUpDownExtender>
    <asp:NumericUpDownExtender ID="NumericUpDownExtender5" runat="server" TargetControlID="txt_early_inlimit_min" Maximum="60" Minimum="0" Width="75" ></asp:NumericUpDownExtender>
    <asp:NumericUpDownExtender ID="NumericUpDownExtender6" runat="server" TargetControlID="txt_early_inlimit_sec" Maximum="60" Minimum="0" Width="75" ></asp:NumericUpDownExtender>\
    
    <asp:NumericUpDownExtender ID="NumericUpDownExtender7" runat="server" TargetControlID="txt_shift_lateIn_EarlyOut_limit_hr" Maximum="12" Minimum="0" Width="75" ></asp:NumericUpDownExtender>
    <asp:NumericUpDownExtender ID="NumericUpDownExtender8" runat="server" TargetControlID="txt_shift_lateIn_EarlyOut_limit_min" Maximum="60" Minimum="0" Width="75" ></asp:NumericUpDownExtender>
    <asp:NumericUpDownExtender ID="NumericUpDownExtender9" runat="server" TargetControlID="txt_shift_lateIn_EarlyOut_limit_sec" Maximum="60" Minimum="0" Width="75" ></asp:NumericUpDownExtender>
    
    <asp:NumericUpDownExtender ID="NumericUpDownExtender10" runat="server" TargetControlID="txt_lunch_earlyin_lateOut_limit_hr" Maximum="12" Minimum="0" Width="75" ></asp:NumericUpDownExtender>
    <asp:NumericUpDownExtender ID="NumericUpDownExtender11" runat="server" TargetControlID="txt_lunch_earlyin_lateOut_limit_min" Maximum="60" Minimum="0" Width="75" ></asp:NumericUpDownExtender>
    <asp:NumericUpDownExtender ID="NumericUpDownExtender12" runat="server" TargetControlID="txt_lunch_earlyin_lateOut_limit_sec" Maximum="60" Minimum="0" Width="75" ></asp:NumericUpDownExtender>
    
    <asp:NumericUpDownExtender ID="NumericUpDownExtender13" runat="server" TargetControlID="txt_halfday_workHrs_limit_hr" Maximum="12" Minimum="0" Width="75" ></asp:NumericUpDownExtender>
    <asp:NumericUpDownExtender ID="NumericUpDownExtender14" runat="server" TargetControlID="txt_halfday_workHrs_limit_min" Maximum="60" Minimum="0" Width="75" ></asp:NumericUpDownExtender>
    <asp:NumericUpDownExtender ID="NumericUpDownExtender15" runat="server" TargetControlID="txt_halfday_workHrs_limit_sec" Maximum="60" Minimum="0" Width="75" ></asp:NumericUpDownExtender>
    
    <asp:NumericUpDownExtender ID="NumericUpDownExtender16" runat="server" TargetControlID="txt_OT_limit_hr" Maximum="12" Minimum="0" Width="75" ></asp:NumericUpDownExtender>
    <asp:NumericUpDownExtender ID="NumericUpDownExtender17" runat="server" TargetControlID="txt_OT_limit_min" Maximum="60" Minimum="0" Width="75" ></asp:NumericUpDownExtender>
    <asp:NumericUpDownExtender ID="NumericUpDownExtender18" runat="server" TargetControlID="txt_OT_limit_sec" Maximum="60" Minimum="0" Width="75" ></asp:NumericUpDownExtender>
    
   <%-- <asp:NumericUpDownExtender ID="NumericUpDownExtender19" runat="server" TargetControlID="txt_permission_limit_hr" Maximum="12" Minimum="0" Width="75" ></asp:NumericUpDownExtender>
    <asp:NumericUpDownExtender ID="NumericUpDownExtender20" runat="server" TargetControlID="txt_permission_limit_min" Maximum="60" Minimum="0" Width="75" ></asp:NumericUpDownExtender>
    <asp:NumericUpDownExtender ID="NumericUpDownExtender21" runat="server" TargetControlID="txt_permission_limit_sec" Maximum="60" Minimum="0" Width="75" ></asp:NumericUpDownExtender>--%>
    <asp:Panel id="pn1" runat="server">
        <asp:RadioButton ID="rd1" runat="server" Text="mei" />
        <asp:RadioButton ID="rd2" runat="server" Text="arun" />
    
    </asp:Panel>
    <asp:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="txt_permission_limit_hr" PopupControlID="pn1" Position="Bottom">
    </asp:PopupControlExtender>
    </form>
</body>
</html>
