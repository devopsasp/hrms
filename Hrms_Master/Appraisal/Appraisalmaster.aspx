<%@ Page Language="C#" MasterPageFile="~/Master_Page/Master.master" AutoEventWireup="true" CodeFile="Appraisalmaster.aspx.cs" Inherits="Hrms_Master_Appraisal_Default" Title="ePay-HRMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript">
    
    function show_message()
    {
        alert("Appraisal Name Was Already Exist");
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
              return true;  
        }
    } 
       
    </script>

    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td id="tdComposeHeader" valign="top" align="center">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="35px" class="border">
                            <span class="Title">&nbsp;&nbsp;AppraisalMaster</span></td>
                    </tr>
                </table>
                <table cellpadding="5px" cellspacing="1px" width="100%">
                    <tr>
                        <td colspan="4" align="center">
                            &nbsp;<asp:Label ID="lbl_Error" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="dComposeItemLabel" nowrap>
                            Appraisal Master Name</td>
                        <td align="left" valign="baseline">
                            <input class="InputDefaultStyle" runat="server" id="txt_appraisalmastername" />
                        <td class="dComposeItemLabel" nowrap>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="dComposeItemLabel" nowrap="nowrap">
                            Appraisal Master Code</td>
                        <td align="left" valign="baseline">
                            <input class="InputDefaultStyle" runat="server" id="Txt_appmastercode" />
                            <asp:Button ID="Button1" runat="server" OnClientClick="return fnSave();" Text="Add"
                                Width="75px" OnClick="Button1_Click"  /></td>
                        <td class="dComposeItemLabel" nowrap="nowrap">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            &nbsp;&nbsp;
                            <table width="100%">
                                <tr valign="top">
                                    <td width="50%" valign="top">
                                        <asp:GridView ID="grid_appraisal" runat="server" AutoGenerateColumns="False" Width="100%"
                                            DataKeyNames="AppraisalmasterID" OnRowUpdating="update" OnRowEditing="edit" >
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
                                                                        AppraisalmasterName </th>
                                                                    <th style="width: 50%;">
                                                                        AppraisalmasterCode</th>
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
                                                                        <asp:TextBox runat="server" Text='<%#Eval("AppraisalmasterName")%>' ID="txtgridname" Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 50%;" nowrap>
                                                                        <asp:TextBox runat="server" Text='<%#Eval("AppraisalmasterCode")%>' ID="Txtgridcode" Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                    <td align="center" style="width: 5%;" nowrap>
                                                                        <asp:ImageButton ID="img_update" ImageUrl="../../Images/i_Edit.gif" runat="server" Style="border: 0"
                                                                            AlternateText="" CommandName="update" />
                                                                        <asp:ImageButton ID="img_save" ImageUrl="../../Images/save.gif" runat="server" Style="border: 0"
                                                                            AlternateText="" CommandName="edit" Visible="false" /></td>
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
        <tr valign="top">
            <td align="center" valign="top">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>

