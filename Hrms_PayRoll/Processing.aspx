<%@ Page Language="C#" MasterPageFile="~/Hrms_PayRoll/PayRollMaster.master" AutoEventWireup="true"
    CodeFile="Processing.aspx.cs" Inherits="PayRoll_Default" Title="Processing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script language="javascript" type="text/javascript" src="../datecheck.js"></script>
    <script language="javascript" type="text/javascript">   
       function fn_chkall(chkid,chklistid)
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
            <td class="tdComposeHeader" valign="top" align="right" style="height: 489px">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr class="border">
                        <td height="35px" class="border">
                            <span class="Title">&nbsp;&nbsp;<img src="../Images/rp_arrow.gif" />&nbsp;Processing</span></td>
                        <td height="30px" class="border">
                &nbsp;<asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" CssClass="InputDefaultStyle"
                    OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
                    </tr>
                    
                </table>
                <table runat="server" id="tab_ddl" width="100%" cellpadding="5" cellspacing="1">
                <tr>
                        <td align="center" colspan="4">
                            &nbsp;<asp:Label ID="lbl_Error" runat="server" CssClass="Error" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr id="row_emp" runat="server">
                        <td id="col1_emp" runat="server" class="dComposeItemLabel" nowrap>
                            Select Employee :&nbsp;
                        </td>
                        <td class="dComposeItemLabel" align="left" style="width: 164px">
                            <div class="qrychkbox_big" style="height: 230px; left: 0px; top: 0px;">
                                <asp:CheckBoxList Height="200px" ID="chk_Empcode" runat="server" CssClass="InputDefaultStyle1"
                                    Width="90%">
                                </asp:CheckBoxList>
                            </div>
                            <input type="checkbox" id="chkall" runat="server" onclick="javascript: fn_chkall(this.id,'ctl00_ContentPlaceHolder1_chk_Empcode')" />Select
                            All Employees
                        </td>
                        <td align="center">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr runat="server">
                        <td runat="server" class="dComposeItemLabel" nowrap="nowrap">
                            From date</td>
                        <td class="dComposeItemLabel" align="center" style="width: 164px">
                        <input type="text" runat="server" class="InputDefaultStyle" ID="txtFromDate" onkeyup="fn_date(event,this.id);" maxlength="10" /><br />
                            </td>
                        <td align="center">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr runat="server">
                        <td runat="server" class="dComposeItemLabel" nowrap="nowrap">
                            To Date</td>
                        <td class="dComposeItemLabel" style="width: 164px">
                        <input type="text" runat="server" class="InputDefaultStyle" ID="txtToDate" onkeyup="fn_date(event,this.id);" maxlength="10" /></td>
                        <td align="center">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr id="row_showdet_btn" runat="server">
                        <td id="Td1" runat="server" class="dComposeItemLabel" nowrap="nowrap">
                        </td>
                        <td class="dComposeItemLabel" style="width: 164px">
                            <asp:ImageButton ImageUrl="../Images/Show_Report.jpg" ID="btn_show" runat="server"
                                OnClick="btn_show_Click" />
                            <asp:ImageButton ID="btn_update" runat="server" Visible="false" ImageUrl="../Images/Update.jpg"
                                OnClick="btn_update_Click" />
                                <asp:ImageButton ID="btn_delete" runat="server" Visible="false" ImageUrl="~/Images/Delete.jpg"
                                OnClick="btn_delete_Click" />
                                <%--<asp:Button ID="btn_delete" runat="server" Visible="false" Text="Delete" OnClick="btn_delete_Click" />--%>
                                </td>
                        <td align="center">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
