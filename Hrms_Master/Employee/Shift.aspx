<%@ Page Language="C#" MasterPageFile="~/Master_Page/Master.master"
    AutoEventWireup="true" CodeFile="Shift.aspx.cs" Inherits="Hrms_Master_Default"
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
    
    function validate()
  {

    var r=confirm("Are you sure you want to delete this record?");
    if (r==true)
    {
    return true;
    }
    else
    {
    return false;
    }
  }
    
    function show_message()
    {
        alert("Shift Name Already Exist");
    }
    
    function show_Error()
    {
        alert("Enter Shift Name");
    }
    
    function fnSave()
    {   
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$ShiftName.value == "" || document.aspnetForm.ctl00$ContentPlaceHolder1$ShiftFrom.value == "" || document.aspnetForm.ctl00$ContentPlaceHolder1$ShiftTo.value == "")
        {
            alert("Make sure all the details are Entered");
            aspnetForm.ctl00$ContentPlaceHolder1$ShiftName.focus();
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
                        <td style="background-color:#5D7B9D" height="35px" class="border">
                            <span class="Title">&nbsp;&nbsp;<span 
                                style="font-family: Calibri; color:White; font-weight: bold;">Shift Master</span></span></td>
                    </tr>
                </table>
                <table cellpadding="5px" cellspacing="1px" width="100%">
                    <tr>
                        <td colspan="4" align="center">
                            &nbsp;<asp:Label ID="lbl_Error" runat="server" ForeColor="Red" Font-Bold="True" 
                                Font-Names="Calibri" Font-Size="X-Small"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="dComposeItemLabel" nowrap 
                            style="font-family: Calibri; font-size: x-small; color: #6A6A6A">
                            Shift Name</td>
                        <td align="left" valign="middle">
                            <input class="InputDefaultStyle" runat="server" id="ShiftName" 
                                onkeypress="AllowOnlyTNS();" 
                                style="font-family: Calibri; font-size: x-small; color: #6A6A6A" 
                                maxlength="20" /></td>
                        <td nowrap>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="dComposeItemLabel" nowrap="nowrap" 
                            style="font-family: Calibri; font-size: x-small; color: #6A6A6A">
                            Shift From&nbsp;&nbsp; </td>
                        <td align="left" valign="middle">
                            <input class="InputDefaultStyle" runat="server" id="ShiftFrom" 
                                style="font-family: Calibri; font-size: x-small; color: #6A6A6A" /><span 
                                style="font-family: Calibri"><span style="font-size: x-small"><span 
                                style="color: #6A6A6A">
                            &nbsp; &nbsp; &nbsp;&nbsp;
                            <asp:ImageButton ImageUrl="~/Images/Add.jpg" ID="Button1" runat="server" OnClientClick="return fnSave();"
                                OnClick="Button1_Click" ImageAlign="AbsMiddle" /></span></span></span></td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="dComposeItemLabel" nowrap="nowrap" 
                            style="font-family: Calibri; font-size: x-small; color: #6A6A6A">
                            Shift To&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                        <td align="left" valign="middle">
                            <input class="InputDefaultStyle" runat="server" id="ShiftTo" 
                                style="font-family: Calibri; font-size: x-small; color: #6A6A6A" /></td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="dComposeItemLabel" nowrap="nowrap" 
                            style="font-family: Calibri; font-size: x-small; color: #6A6A6A">
                            Category&nbsp;&nbsp;&nbsp;&nbsp; </td>
                        <td align="left" valign="middle">
                            <asp:DropDownList ID="DropDownList1" runat="server" 
                                CssClass="InputDefaultStyle" Font-Names="Calibri" Height="19px" Width="125px">
                                <asp:ListItem>Normal Shift</asp:ListItem>
                                <asp:ListItem>Break Shift</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="dComposeItemLabel" nowrap="nowrap">
                            &nbsp;</td>
                        <td align="left" valign="middle">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
                <table width="100%">
                    <tr valign="top">
                        <td width="50%" valign="top">
                            <asp:GridView ID="grid_Shift" runat="server" AutoGenerateColumns="False" Width="50%"
                                DataKeyNames="ShiftId" OnRowDeleting="Delete" OnRowEditing="Edit" 
                                OnRowUpdating="Update" onrowcommand="grid_Shift_RowCommand">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                        <TABLE cellSpacing="0" cellPadding="0" width="100%">
                                            <COLGROUP>        
                                                <COL>                                
                                            </COLGROUP>
                                            <THEAD>
                                                <TR>
                                                    <TH align="left" style="width: 20%; font-family: Calibri; color: #FFFFFF; font-weight: bold; border: thick solid #c18685">Shift List</TH>
                                                    <TH align="left" style="width: 20%; font-family: Calibri; color: #FFFFFF; font-weight: bold; border: thick solid #c18685">Start Time</TH>
                                                    <TH align="left" style="width: 20%; font-family: Calibri; color: #FFFFFF; font-weight: bold; border: thick solid #c18685">End Time</TH>
                                                    <TH align="left" style="width: 20%; font-family: Calibri; color: #FFFFFF; font-weight: bold; border: thick solid #c18685">Category</TH>
                                                    <TD align="right" style="width: 10%; font-family: Calibri; color: #FFFFFF; font-weight: bold; border: thick solid #c18685"><asp:Label ID="lbledit" Text="Edit" runat="server"></asp:Label></TD>
                                                    <TD align="right" style="width: 10%; font-family: Calibri; color: #FFFFFF; font-weight: bold; border: thick solid #c18685"><asp:Label ID="lbldel" Text="Delete" runat="server"></asp:Label></TD>
                                                </tr>
                                            </THEAD>
                                       </table>
                                   </HeaderTemplate>
                                        <ItemTemplate>
                                            <table class="dItemListContentTable" cellspacing="0" cellpadding="0" width="100%">
                                                <colgroup>
                                                    <col class="dInboxContentTableCheckBoxCol">
                                                </colgroup>
                                                <tbody>
                                                    <tr>
                                                        <td id="chkid" runat="server" style="width: 10%;" align="left">
                                                            <input type="checkbox" id="Chk_Shift" runat="server" /></td>
                                                        <td style="width: 20%;" nowrap>
                                                        <input runat="server" id="txtgrid" onkeypress="AllowOnlyTNS();" value='<%#Eval("ShiftName")%>' style=" width:100%; font-family: Calibri; color: #999999;"/>
                                                            <%--<asp:TextBox runat="server" Text='<%#Eval("ShiftName")%>' ID="txtgrid" Enabled="false"></asp:TextBox>--%>
                                                        </td>
                                                        <td style="width: 20%;" nowrap>
                                                            <asp:TextBox runat="server" Text='<%#Eval("ShiftFrom")%>' ID="txtgrid1" 
                                                                Enabled="false" Height="22px" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 20%;" nowrap>
                                                            <asp:TextBox runat="server" Text='<%#Eval("ShiftTo")%>' ID="txtgrid2" 
                                                                Enabled="false" Height="22px" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 20%;" nowrap>
                                                            <asp:TextBox runat="server" Text='<%#Eval("ShiftCategory")%>' ID="txtgrid3" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td align="center" style="width: 10%;" nowrap>
                                                            <asp:ImageButton ID="img_update" ImageUrl="../../Images/i_Edit.gif" runat="server"
                                                                Style="border: 0" AlternateText="" CommandName="Update" />
                                                            <asp:ImageButton ID="img_save" ImageUrl="../../Images/save.gif" runat="server" Style="border: 0"
                                                                AlternateText="" CommandName="Edit" Visible="false" /></td>
                                                                <td style="width: 10%; font-family: Calibri; color: #c18685;" nowrap 
                                                            align="center">
                                                                <asp:ImageButton ImageUrl="../../Images/delete_icon.gif" ID="imgdel" runat="server" CommandName="Delete" OnClientClick="return validate()" />
                                                             </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="#C18685" />
                            </asp:GridView>
                        </td>
                        <td valign="top">
                            <asp:GridView ID="grid_Branch" runat="server" AutoGenerateColumns="False" Width="100%"
                                DataKeyNames="CompanyId" OnRowDeleting="Delete" OnRowEditing="Edit">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                        <TABLE cellSpacing="0" cellPadding="0" width="100%">
                                            <COLGROUP>        
                                                <COL>                                
                                            </COLGROUP>
                                            <THEAD>
                                                <TR>
                                                    <TH align="left" style="width: 80%; font-family: Calibri; color: #FFFFFF; font-weight: bold; border: thick solid #c18685">Branch List</TH>
                                                    <TH align="right" style="width: 80%; font-family: Calibri; color: #FFFFFF; font-weight: bold; border: thick solid #c18685">Edit</TH>
                                                </tr>
                                            </THEAD>
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
                                                            <input type="checkbox" id="Chk_Branch" runat="server" /></td>
                                                        <td style="width: 80%; font-family: Calibri; color: #C18685; font-size: x-small; font-weight: bold" nowrap>
                                                            <%#Eval("CompanyName")%>
                                                            </a></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="#C18685" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td align="center" valign="top" colspan="2">
                            <asp:ImageButton ImageUrl="~/Images/Assign.jpg" ID="Button2" runat="server" OnClick="Button2_Click" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr valign="top">
            <td align="center" valign="top">
                <input type="hidden" id="ToolBarCode" name="ToolBarCode" runat="server" value="0" />
                <input id="hShiftID" runat="server" type="hidden" value="0" /></td>
        </tr>
        <tr>
            <td align="center">
                &nbsp;<asp:Label ID="Error" runat="server" Width="50%" Font-Bold="True" 
                    Font-Names="Calibri" Font-Size="X-Small"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
