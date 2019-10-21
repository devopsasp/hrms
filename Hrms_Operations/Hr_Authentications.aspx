<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Hr_Authentications.aspx.cs" Inherits="Hrms_Company_Default" Title="User Authentications" %>

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
        alert("Department Name Already Exist");
    }
    
    function show_Error()
    {
        alert("Enter Department Name");
    }
    
    function fnSave()
    {   
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$DepartmentName.value == "")
        {
            alert("Enter Department Name");
            aspnetForm.ctl00$ContentPlaceHolder1$DepartmentName.focus();
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
                        <td  height="35px" class="border">
                            <span class="Title">&nbsp;&nbsp;<span 
                                
                                style="font-family: Calibri; font-size: medium; font-weight: bold;">Authentication</span></span></td>
                    </tr>
                </table>
                <table cellpadding="5px" cellspacing="1px" width="100%">
                    <tr>
                        <td class="dComposeItemLabel" nowrap 
                            
                            style="font-size: x-small; color: #6A6A6A; font-family: Calibri; width: 355px;">
                            <span style="font-size: x-small"><span style="color: #6A6A6A">
                            <asp:Label ID="lbl_Error" runat="server" ForeColor="Red" Font-Bold="True" 
                                Font-Names="Calibri" Font-Size="X-Small"></asp:Label></span></span></td>
                        <td align="left" valign="baseline">
                            &nbsp;</td>
                        <td align="center">
                            <span style="font-size: x-small; color: #6A6A6A">&nbsp;</span></td>
                        <td style="font-size: x-small; color: #6A6A6A">
                            &nbsp;</td>
                    </tr>
                </table>
                <table width="100%">
                    
                    <tr valign="top">
                        <td align="center">
                            <div class="gridheight_master" style="left: -1px; top: 0px">
                                <asp:GridView ID="grid_sections" runat="server" AutoGenerateColumns="False" Width="100%"
                                    DataKeyNames="SectionId" OnRowDeleting="Delete" OnRowEditing="Edit" class="table table-striped table-bordered table-hover"
                                    OnRowUpdating="Update" CellPadding="4"  
                                    GridLines="None">
                                    <RowStyle  />
                                    <Columns>
                                        <asp:TemplateField>
                                        <HeaderTemplate>
                                        <TABLE cellSpacing="0" cellPadding="0" width="100%">
                                            <COLGROUP>        
                                                <COL>                                
                                            </COLGROUP>
                                            <THEAD>
                                                <TR>
                                                    <TH align="left" style="width: 80%; font-family: Calibri; color: #FFFFFF; font-weight: bold;">
                                                        Sections</TH>
                                                    <TH align="right" style="width: 80%; font-family: Calibri; color: #FFFFFF; font-weight: bold;">View</TH>
                                                   <TH align="right" style="width: 80%; font-family: Calibri; color: #FFFFFF; font-weight: bold;">Edit</TH>
                                                   <TH align="right" style="width: 80%; font-family: Calibri; color: #FFFFFF; font-weight: bold;">Delete</TH>
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
                                                                <input type="checkbox" id="Chk_sections" runat="server" /></td>
                                                            <td align="left" style="width: 40%;" nowrap>
                                                            <input type="text"  runat="server" id="txtgrid" onkeypress="AllowOnlyTNS();" value='<%#Eval("SectionName")%>' style="font-family: Calibri; color: #C18685; width: 320px; font-weight: bold;" />
                                                                <%--<asp:TextBox runat="server" Text='<%#Eval("DepartmentName")%>' ID="txtgrid" Enabled="false"></asp:TextBox>--%>
                                                            </td>
                                                            <td id="viewchk" runat="server" style="width: 10%;" align="left">
                                                                <input type="checkbox" id="Checkbox1" runat="server" /></td>
                                                                <td id="editchk" runat="server" style="width: 10%;" align="left">
                                                                <input type="checkbox" id="Checkbox2" runat="server" /></td>
                                                                <td id="deletechk" runat="server" style="width: 10%;" align="left">
                                                                <input type="checkbox" id="Checkbox3" runat="server" /></td>
                                                            <%--<td align="center" style="width: 5%;" nowrap>
                                                                <asp:ImageButton ID="img_update" ImageUrl="../Images/i_Edit.gif" runat="server" Style="border: 0"
                                                                    AlternateText="" CommandName="Update" />
                                                                <asp:ImageButton ID="img_save" ImageUrl="../Images/save.gif" runat="server" Style="border: 0"
                                                                    AlternateText="" CommandName="Edit" Visible="false" />
                                                             </td>--%>
                                                             
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle  Font-Bold="True"/>
                                    <PagerStyle  HorizontalAlign="Center" />
                                    <SelectedRowStyle  Font-Bold="True"  />
                                    <HeaderStyle  Font-Bold="True"/>
                                    <EditRowStyle/>
                                    <AlternatingRowStyle/>
                                </asp:GridView>
                            </div>
                        </td>
                         <td id="branch_body" runat="server" align="center">
                            <div class="gridheight_master">
                                <asp:GridView ID="grid_Branch" runat="server" AutoGenerateColumns="False" Width="100%"
                                    DataKeyNames="CompanyId" OnRowDeleting="Delete" OnRowEditing="Edit" class="table table-striped table-bordered table-hover"
                                    CellPadding="4"  GridLines="None" 
                                    >
                                    <RowStyle/>
                                    <Columns>
                                        <asp:TemplateField>
                                        <HeaderTemplate>
                                        <TABLE cellSpacing="0" cellPadding="0" width="100%">
                                            <COLGROUP>        
                                                <COL>                                
                                            </COLGROUP>
                                            <THEAD>
                                                <TR>
                                                    <TH align="left" style="width: 80%; font-family: Calibri; color: #FFFFFF; font-weight: bold;">Branch List</TH>
                                                    <%--<TH align="right" style="width: 80%; font-family: Calibri; color: #FFFFFF; font-weight: bold; border: thick solid #c18685">Edit</TH>--%>
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
                                                            <td style="width: 80%; font-family: Calibri; color: #666666; font-size: x-small; font-weight: bold;" nowrap>
                                                                <%#Eval("CompanyName")%>
                                                                </a></td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle  Font-Bold="True"  />
                                    <PagerStyle  HorizontalAlign="Center" />
                                    <SelectedRowStyle  Font-Bold="True"  />
                                    <HeaderStyle  Font-Bold="True"  />
                                    <EditRowStyle/>
                                    <AlternatingRowStyle/>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
               <%-- <asp:ImageButton ImageUrl="~/Images/Assign.png" onmouseover="this.src='../Images/Assignover.png';" onmouseout="this.src='../Images/Assign.png';" ID="Button2" runat="server"/>--%>
                <asp:Button ID="Button2" runat="server"  class="btn btn-success" Text="Assign" 
                    onclick="Button2_Click1"  />
                </td>
        </tr>
        <tr valign="top">
            <td align="center" valign="top">
                &nbsp;<asp:Label ID="Error" runat="server" Width="50%" Font-Bold="True" 
                    Font-Names="Calibri" Font-Size="X-Small"></asp:Label></td>
        </tr>
        <tr>
            <td align="center">
                <input type="hidden" id="ToolBarCode" name="ToolBarCode" runat="server" value="0" />&nbsp;
                <input id="hDepartmentID" runat="server" type="hidden" value="0" /></td>
        </tr>
    </table>
</asp:Content>