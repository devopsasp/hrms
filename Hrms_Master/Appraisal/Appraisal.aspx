<%@ Page Language="C#" MasterPageFile="~/Master_Page/Master.master" AutoEventWireup="true"
    CodeFile="Appraisal.aspx.cs" Inherits="Hrms_Master_Default" Title="Welcome to HRMS" %>
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
        alert("Appraisal Name Was Already Exist");
    }
        function show_Error()
        {
        alert("Enter Appraisal Name");
        }
        
        function show_Error1()
        {
        alert("Enter Total Point");
        }
   
    function fnSave()
    {   
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_appraisalname.value == "")
        {
            alert("Enter Appraisal Name");
            aspnetForm.ctl00$ContentPlaceHolder1$txt_appraisalname.focus();
            return false;
        }                        
        else
        { 
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$Txt_points.value == "")
        {
            alert("Enter Appraisal Point");
            aspnetForm.ctl00$ContentPlaceHolder1$Txt_points.focus();
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
            <td id="tdComposeHeader" valign="top" align="center">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td  style="background-color:#dbcdcc" height="35px" class="border">
                            <span class="Title">&nbsp;&nbsp;Appraisal</span></td>
                    </tr>
                </table>
                <table cellpadding="5px" cellspacing="1px" width="100%">
                    <tr>
                        <td colspan="4" align="center">
                            &nbsp;<asp:Label ID="lbl_Error" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="dComposeItemLabel" nowrap style="width: 356px">
                            Appraisal Criteria</td>
                        <td align="left" valign="baseline">
                            <input class="InputDefaultStyle" runat="server" id="txt_appraisalname" onkeydown="AllowOnlyText1(event);" />
                            <asp:Button ID="Button1" runat="server" OnClientClick="return fnSave();" Text="Add"
                                Width="64px" OnClick="Button1_Click1" Height="22px" />
                        <td class="dComposeItemLabel" nowrap>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <%--<tr>
                        <td class="dComposeItemLabel" nowrap="nowrap" style="height: 35px">
                            points</td>
                        <td align="left" valign="baseline" style="height: 35px">
                            <input class="InputDefaultStyle" runat="server" id="Txt_points" onkeydown="AllowOnlyNumeric1(event);" />
                            </td>
                        <td class="dComposeItemLabel" nowrap="nowrap" style="height: 35px">
                        </td>
                        <td style="height: 35px">
                        </td>
                    </tr>--%>
                    <tr>
                        <td colspan="4">
                            &nbsp;&nbsp;
                            <table width="100%">
                                <tr valign="top">
                                    <td width="50%" valign="top">
                                        <asp:GridView ID="grid_appraisal" runat="server" AutoGenerateColumns="False" Width="100%"
                                            DataKeyNames="AppraisalID" OnRowUpdating="Update" OnRowEditing="Edit" OnRowDeleting="delete">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <table class="dItemListContentTable" cellspacing="0" cellpadding="0" width="100%">
                                                            <colgroup>
                                                                <col>
                                                            </colgroup>
                                                            <thead>
                                                                <tr>
                                                                    <th style="width: 10%;">
                                                                        &nbsp;</th>
                                                                    <th style="width: 30%;">
                                                                        Appraisal List</th>
                                                                    <th style="width: 50%;">
                                                                        Points</th>
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
                                                                    <td style="width: 10%;" align="left">
                                                                        <input type="checkbox" id="Chk_Grade" runat="server" /></td>
                                                                    <td style="width: 30%;" nowrap>
                                                                    <input runat="server" id="txtgrid" onkeydown="AllowOnlyText1(event);" value='<%#Eval("AppraisalName")%>' disabled="disabled" />
                                                                        <%--<asp:TextBox runat="server" Text='<%#Eval("AppraisalName")%>' ID="txtgrid" Enabled="false"></asp:TextBox>--%>
                                                                    </td>
                                                                    <td style="width: 50%;" nowrap>
                                                                    <input runat="server" id="Txtgpoint" onkeydown="AllowOnlyNumeric1(event);" value='<%#Eval("totalpoint")%>' disabled="disabled" />
                                                                        <%--<asp:TextBox runat="server" Text='<%#Eval("totalpoint")%>' ID="Txtgpoint" Enabled="false"></asp:TextBox>--%>
                                                                    </td>
                                                                    <td align="center" style="width: 5%;" nowrap>
                                                                        <asp:ImageButton ID="img_update" ImageUrl="../../Images/i_Edit.gif" runat="server" Style="border: 0"
                                                                            AlternateText="" CommandName="Update" />
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
                </table>
            </td>
        </tr>
        <%--<tr valign="top">
            <td align="center" valign="top">
                &nbsp;</td>
        </tr>--%>
    </table>
</asp:Content>
