<%@ Page Language="C#" MasterPageFile="~/Master_Page/Master.master" AutoEventWireup="true" CodeFile="Grade.aspx.cs" Inherits="Hrms_Master_Default3" Title="Welcome to HRMS" %>
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
        alert("Grade Name Already Exist");
    }
    
     function show_Error()
    {
        alert("Enter Grade Name");
    }
    
    function fnSave()
    {   
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$GradeName.value == "")
        {
            alert("Enter Grade Name");
            aspnetForm.ctl00$ContentPlaceHolder1$GradeName.focus();
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
                            <span class="Title" style="font-family: Calibri">&nbsp;&nbsp;</span><span class="Title" 
                                style="font-family: Calibri; color:White; font-weight: bold;">Grade Master</span></td>
                             <td style="background-color:#5D7B9D" width="200px" align="center">
                                 <asp:DropDownList ID="ddl_branch" runat="server" AutoPostBack="True" 
                                     onselectedindexchanged="ddl_branch_SelectedIndexChanged" Width="115px">
                                 </asp:DropDownList>
                                                        </td>
                    </tr>
                </table>
                <table id="grade_add" runat="server" width="100%">
                    <tr>
                        <td class="dComposeItemLabel" nowrap 
                            
                            
                            style="font-family: Calibri; font-size: x-small; color: #6A6A6A; height: 21px; width: 496px;">
                            &nbsp;</td>
                        <td align="left" valign="baseline" style="height: 21px; width: 127px;">
                            <span style="font-family: Calibri">
                            <asp:Label ID="lbl_Error" runat="server" ForeColor="Red" Font-Bold="True" 
                                Font-Names="Calibri" Font-Size="X-Small"></asp:Label></span></td>
                        <td align="center" style="height: 21px">
                            &nbsp;</td>
                        <td style="font-family: Calibri; height: 21px">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td nowrap 
                            
                            
                            style="font-family: Calibri; font-size: small; color: #6A6A6A; height: 21px; width: 496px; font-weight: bold;" 
                            align="right">
                            Grade Name</td>
                        <td align="left" style="height: 21px; width: 127px;">
                            <input class="InputDefaultStyle" runat="server" id="GradeName" 
                                onkeypress="AllowOnlyTNS();" style="font-family: Calibri" maxlength="20" /></td>
                        <td align="left" style="height: 21px">
                            <span style="font-family: Calibri">
                            <asp:ImageButton ImageUrl="~/Images/Add.png" onmouseover="this.src='../../Images/Addover.png';" onmouseout="this.src='../../Images/Add.png';" ID="Button1" runat="server" OnClientClick="return fnSave();"
                                Text="Add" OnClick="Button1_Click1" ImageAlign="AbsMiddle" /></span></td>
                        <td style="font-family: Calibri; height: 21px">
                            </td>
                    </tr>
                    <tr>
                        <td nowrap 
                            
                            
                            style="font-family: Calibri; font-size: small; color: #6A6A6A; height: 21px; width: 496px; font-weight: bold;" 
                            align="right">
                            &nbsp;</td>
                        <td align="left" style="height: 21px; width: 127px;">
                            &nbsp;</td>
                        <td align="left" style="height: 21px">
                            &nbsp;</td>
                        <td style="font-family: Calibri; height: 21px">
                            &nbsp;</td>
                    </tr>
                </table>
                <table id="grade_branch" runat="server" width="100%">
<%--                    <tr valign="top">
                        <td align="center" style="font-family: Calibri; color: #993333; background-color: #DBCDCC">
                            Grade List</td>
                        <td id="branch_header" runat="server" align="center" style="background-color: #DBCDCC">
                            <asp:Label ID="Label1" runat="server" Text="Branch List" Font-Names="Calibri" 
                                ForeColor="#993333"></asp:Label>
                        </td>
                    </tr>--%>
                    <tr valign="top">
                        <td align="center" width="50%">
                            <div class="gridheight_master">
                                <asp:GridView ID="grid_Grade" runat="server" AutoGenerateColumns="False" Width="100%"
                                    DataKeyNames="GradeId" OnRowDeleting="Delete" OnRowEditing="Edit" 
                                    OnRowUpdating="Update" onrowcommand="grid_Grade_RowCommand" 
                                    CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                        <asp:TemplateField>
                                        <HeaderTemplate>
                                        <TABLE cellSpacing="0" cellPadding="0" width="100%">
                                            <COLGROUP>        
                                                <COL>                                
                                            </COLGROUP>
                                            <THEAD>
                                                <TR>
                                                    <TH align="left" style="width: 80%; font-family: Calibri; color: #FFFFFF; font-weight: bold;">Grade List</TH>
                                                    <TD align="right" style="width: 10%; font-family: Calibri; color: #FFFFFF; font-weight: bold;"><asp:Label ID="lbledit" Text="Edit" runat="server"></asp:Label></TD>
                                                    <TD align="right" style="width: 10%; font-family: Calibri; color: #FFFFFF; font-weight: bold;"><asp:Label ID="lbldel" Text="Delete" runat="server"></asp:Label></TD>
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
                                                                <input type="checkbox" id="Chk_Grade" runat="server" /></td>
                                                            <td style="width: 80%;" nowrap>
                                                            <input runat="server" id="txtgrid" onkeypress="AllowOnlyTNS();" 
                                                                    value ='<%# Eval("GradeName") %>' 
                                                                    style="font-family: Calibri; font-size:small; color: #6A6A6A; width: 100%; font-weight: bold;"  />
                                                                <%--<asp:TextBox runat="server" Text='<%#Eval("GradeName")%>' ID="txtgrid" Enabled="false"></asp:TextBox>--%>
                                                            </td>
                                                            <td align="right" style="width: 5%;" nowrap>
                                                                <asp:ImageButton ID="img_update" ImageUrl="../../Images/i_Edit.gif" runat="server" Style="border: 0"
                                                                    AlternateText="" CommandName="Update" />
                                                                <asp:ImageButton ID="img_save" ImageUrl="../../Images/save.gif" runat="server" Style="border: 0"
                                                                    AlternateText="" CommandName="Edit" Visible="false" /></td>
                                                                    <td style="width: 10%; font-family: Calibri;" 
                                                                align="center">
                                                                <%--<asp:Label ID="lbl" runat="server" Text="Delete"></asp:Label>--%><asp:ImageButton ImageUrl="../../Images/delete_icon.gif" ID="imgdel" runat="server" CommandName="Delete" OnClientClick="return validate()" />
                                                             </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                </asp:GridView>
                            </div>
                        </td>
                         <td id="branch_body" runat="server" align="center" width="50%">
                            <div class="gridheight_master">
                                <asp:GridView ID="grid_Branch" runat="server" AutoGenerateColumns="False" Width="50%"
                                    DataKeyNames="CompanyId" OnRowDeleting="Delete" OnRowEditing="Edit" 
                                    CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
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
                                                    <TH align="right" style="width: 80%; font-family: Calibri; color: #FFFFFF; font-weight: bold;">Edit</TH>
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
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
                <asp:ImageButton ImageUrl="~/Images/Assign.png" onmouseover="this.src='../Images/Assignover.png';" onmouseout="this.src='../Images/Assign.png';" ID="Button2" runat="server" OnClick="Button2_Click" /></td>
        </tr>
        <tr valign="top">
            <td align="center" valign="top">
                &nbsp;<asp:Label ID="Error" runat="server" Width="50%" Font-Bold="True" 
                    Font-Names="Calibri" Font-Size="X-Small"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <input type="hidden" id="ToolBarCode" name="ToolBarCode" runat="server" value="0" />&nbsp;
                <input id="hGradeID" runat="server" type="hidden" value="0" /></td>
        </tr>
    </table>
</asp:Content>