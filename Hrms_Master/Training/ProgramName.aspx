<%@ Page Language="C#" MasterPageFile="~/Master_Page/Master.master" AutoEventWireup="true" CodeFile="ProgramName.aspx.cs" Inherits="Hrms_Training_Default" Title="Welcome to HRMS Training Module"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
        alert("Course Name Already Exist");
    }
    
    function show_Error()
    {
        alert("Enter Course Name");
    }
    function fnSave()
    {   
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$pgmname.value == "")
        {
            alert("Enter Program Name");
            aspnetForm.ctl00$ContentPlaceHolder1$pgmname.focus();
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
            <td id="tdComposeHeader" valign="top" align="center" 
                style="font-family: Calibri">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="background-color:#dbcdcc" height="35px" class="border" align="left">
                            <span class="Title" style="font-family: Calibri">Program Name</span></td>
                    </tr>
                </table>
                <table cellpadding="5px" cellspacing="1px" width="100%">
                    <tr>
                        <td colspan="4" align="center">
                            &nbsp;<span style="font-family: Calibri"><span style="font-weight: normal"><asp:Label 
                                ID="lbl_Error" runat="server" CssClass="Error" 
                                ForeColor="Red" Font-Bold="True" Height="16px" Font-Names="Calibri" 
                                Font-Size="X-Small"></asp:Label></span></span></td>
                    </tr>
                    <tr>
                        <td class="dComposeItemLabel" nowrap 
                            style="width: 220px; font-family: Calibri; font-size: x-small; color: #6A6A6A;">
                            Program Name</td>
                        <td align="left" valign="baseline">
                            <input class="InputDefaultStyle" runat="server" id="pgmname" 
                                style="font-family: Calibri"  />
                            <span style="font-family: Calibri">
                            <asp:ImageButton ImageUrl="~/Images/Add.jpg" ID="Button1" runat="server" OnClientClick="return fnSave();" OnClick="Button1_Click1" ImageAlign="AbsMiddle" />
                            </span></td>
                        <td class="dComposeItemLabel" nowrap style="font-family: Calibri">
                            &nbsp;</td>
                        <td style="font-family: Calibri">
                            &nbsp;</td>
                    </tr>
                    <%--<tr>
                        <td colspan="4">
                            &nbsp;</td>
                    </tr>--%><span style="font-family: Calibri"> </span>
                </table>
                <table width="75%" align="center">
                    <tr valign="top">
                        <td width="50%" valign="top">
                            <asp:GridView ID="grid_pgmname" runat="server" AutoGenerateColumns="False" 
                                OnRowEditing="Edit" OnRowUpdating="Update" Width="100%" DataKeyNames="prgmid" 
                                style="font-family: Calibri">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table class="dItemListContentTable" cellspacing="0" cellpadding="0" width="100%">
                                                <colgroup>
                                                    <col>
                                                </colgroup>
                                                <thead>
                                                    <tr>
                                                        <th style="width: 80%;">
                                                            Program Name List</th>
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
                                                        <td align="left" style="width: 40%;" nowrap>
                                                        <input runat="server" id="txtgrid" onkeypress="AllowOnlyText();" value='<%#Eval("prgmname")%>' disabled="disabled" />
                                                            <%--<asp:TextBox runat="server" Text='<%#Eval("prgmname")%>' ID="txtgrid" Enabled="false"></asp:TextBox>--%>
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
        <tr valign="top">
            <td align="center" valign="top">
                <input type="hidden" id="ToolBarCode" name="ToolBarCode" runat="server" 
                    value="0" style="font-family: Calibri" />
                <input id="hpgmnameID" runat="server" type="hidden" value="0" 
                    style="font-family: Calibri" /></td>
        </tr>
        <tr>
            <td align="center" style="height: 19px">
                <span style="font-family: Calibri">&nbsp;<span style="font-weight: normal"><asp:Label 
                    ID="Error" runat="server" Width="50%" CssClass="Error" ForeColor="Red" 
                    Font-Bold="True" Font-Names="Calibri" Font-Size="X-Small"></asp:Label>
                </span></span>
            </td>
        </tr>
    </table>
</asp:Content>

