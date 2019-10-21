<%@ Page Language="C#" MasterPageFile="~/Master_Page/Master.master"
    AutoEventWireup="true" CodeFile="App_Increment.aspx.cs" Inherits="Hrms_Master_Default"
    Title="Welcome to HRMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript" language="javascript" src="../../Scripts/Datavalid.js"></script>
<script type="text/javascript">
    window.onload = function () {
        var seconds = 5;
        setTimeout(function () {
            document.getElementById("<%=lbl_Error.ClientID %>").innerHTML = "";
        }, seconds * 1000);
    };
</script>
    <script language="javascript" type="text/javascript">
    
    function show_message()
    {
        alert("Appraisal Name Already Exist");
    }
    function show_Error1()
    {
        alert("Enter FromPoint Value");
    }
     function show_Error2()
    {
        alert("Enter ToPoint Value");
    }  
   
   function show_Error3()
    {
        alert("Enter Increment Point");
    }
    
    function fnSave()
    {   
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$txtFromPoint.value == "")
            {
                alert("Enter FromPoint Value");            
                return false;
            }                        
        else
            { 
                if(document.aspnetForm.ctl00$ContentPlaceHolder1$txtToPoint.value == "")
                    {
                        alert("Enter ToPoint Value");            
                        return false;
                    }   
                else
                    {
                        if(document.aspnetForm.ctl00$ContentPlaceHolder1$txtPointsAmount.value == "")
                            {
                                alert("Enter Points Amount");            
                                return false;
                            }   
                        else
                            {
                                return true;  
                            }
                    }
        }
    }    
    </script>

    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td id="tdComposeHeader" valign="top" align="center">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="35px" class="border">
                            <span class="Title">&nbsp;&nbsp;Increment</span></td>
                    </tr>
                </table>
                <table cellpadding="5px" cellspacing="1px" width="100%">
                    <tr>
                        <td colspan="4" align="center">
                            &nbsp;<asp:Label ID="lbl_Error" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="dComposeItemLabel" nowrap>
                            From Point</td>
                        <td align="left" valign="baseline">
                            <input class="InputDefaultStyle" runat="server" id="txtFromPoint" onkeydown="AllowOnlyNumeric1(event);" />
                        </td>
                        <td class="dComposeItemLabel" nowrap>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="dComposeItemLabel" nowrap="nowrap">
                            To Point</td>
                        <td align="left" valign="baseline">
                            <input class="InputDefaultStyle" runat="server" id="txtToPoint" onkeydown="AllowOnlyNumeric1(event);" /></td>
                        <td class="dComposeItemLabel" nowrap="nowrap">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="dComposeItemLabel" nowrap="nowrap">
                            Points in Percentage</td>
                        <td align="left" valign="baseline">
                            <input class="InputDefaultStyle" runat="server" id="txtPointsAmount" onkeydown="AllowOnlyNumeric1(event);" /></td>
                        <td class="dComposeItemLabel" nowrap="nowrap">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="dComposeItemLabel" nowrap="nowrap">
                        </td>
                        <td align="left" valign="baseline">
                            <asp:Button ID="Button1" runat="server" OnClientClick="return fnSave();" Text="Add"
                                Width="75px" OnClick="Button1_Click1" /></td>
                        <td class="dComposeItemLabel" nowrap="nowrap">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            &nbsp;</td>
                    </tr>
                </table>
                <table width="100%">
                    <tr valign="top">
                        <td width="50%" valign="top">
                            <asp:GridView ID="grid_Increment" runat="server" AutoGenerateColumns="False" Width="100%"
                                DataKeyNames="IncrementID" OnRowDeleting="Delete" OnRowEditing="Edit" OnRowUpdating="Update">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table class="dItemListContentTable" cellspacing="0" cellpadding="0" width="100%">
                                                <colgroup>
                                                    <col>
                                                </colgroup>
                                                <thead>
                                                    <tr>
                                                        <th style="width: 5%;">
                                                            &nbsp;</th>
                                                        <th style="width: 30%;">
                                                            From Point</th>
                                                        <th style="width: 30%;">
                                                            To Point</th>
                                                        <th style="width: 30%;">
                                                            Point Amount</th>
                                                        <th style="width: 5%;">
                                                            Edit</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table class="dItemListContentTable" cellspacing="0" cellpadding="0" width="100%">
                                                <colgroup>
                                                    <col class="dInboxContentTableCheckBoxCol">
                                                </colgroup>
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 5%;" align="left">
                                                            <input type="checkbox" id="Chk_Department" runat="server" /></td>
                                                        <td style="width: 30%;" nowrap>
                                                        <input runat="server" id="txtgridfrompoint" onkeydown="AllowOnlyNumeric1(event);" value='<%#Eval("startpoint")%>' disabled="disabled" />
                                                            <%--<asp:TextBox runat="server" Text='<%#Eval("startpoint")%>' ID="txtgridfrompoint"
                                                                Enabled="false"></asp:TextBox>--%>
                                                        </td>
                                                        <td style="width: 30%;" nowrap>
                                                        <input runat="server" id="txtgridtopoint" onkeydown="AllowOnlyNumeric1(event);" value='<%#Eval("lastpoint")%>' disabled="disabled" />
                                                            <%--<asp:TextBox runat="server" Text='<%#Eval("lastpoint")%>' ID="txtgridtopoint" Enabled="false"></asp:TextBox>--%>
                                                        </td>
                                                        <td style="width: 30%;" nowrap>
                                                        <input runat="server" id="txtgridamount" onkeydown="AllowOnlyNumeric1(event);" value='<%#Eval("increment")%>' disabled="disabled" />
                                                            <%--<asp:TextBox runat="server" Text='<%#Eval("increment")%>' ID="txtgridamount" Enabled="false"></asp:TextBox>--%>
                                                        </td>
                                                        <td align="center" style="width: 5%;" nowrap>
                                                            <asp:ImageButton ID="img_update" ImageUrl="../../Images/i_Edit.gif" runat="server"
                                                                Style="border: 0" AlternateText="" CommandName="Update" />
                                                            <asp:ImageButton ID="img_save" ImageUrl="../../Images/save.gif" runat="server" Style="border: 0"
                                                                AlternateText="" CommandName="Edit" Visible="false" /></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="top">
            <td align="center" valign="top">
                <asp:Label ID="Error" runat="server" Width="50%"></asp:Label>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
