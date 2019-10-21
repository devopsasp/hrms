<%@ Page MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/Master_Page/Master.master" AutoEventWireup="true"
    CodeFile="Department.aspx.cs" Inherits="Hrms_Master_Default" Title="Welcome to HRMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="~/Css/Cand_BaseStyle.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" language="javascript" src="~/Scripts/Datavalid.js"></script>
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
                        <td style="background-color:#5D7B9D" height="35px" class="border">
                            <span class="Title">&nbsp;&nbsp;<span 
                                style="font-family: Calibri; color:White; font-weight: bold;">Department 
                            Master</span></span></td>
                            <td style="background-color:#5D7B9D" align="center" width="200px" >
                                <asp:DropDownList ID="ddl_branch" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddl_branch_SelectedIndexChanged" Width="115px">
                                </asp:DropDownList>
                                                        </td>
                    </tr>
                </table>
                <table cellpadding="5px" cellspacing="1px" width="100%">
                    <tr runat="server">
                        <td nowrap="nowrap" style="font-size: small; color: #6A6A6A; font-family: Calibri; width: 437px; font-weight: bold;" 
                            align="right">
                            &nbsp;</td>
                        <td align="left" style="width: 130px">
                            <span style="font-size: x-small"><span style="color: #6A6A6A">
                            <asp:Label ID="lbl_Error" runat="server" ForeColor="Red" Font-Bold="True" 
                                Font-Names="Calibri" Font-Size="X-Small"></asp:Label></span></span>
                            </td>
                        <td align="left">
                            &nbsp;</td>
                        <td style="font-size: x-small; color: #6A6A6A">
                            &nbsp;</td>
                    </tr>
                    <tr id="dept_add" runat="server">
                        <td nowrap="nowrap" style="font-size: small; color: #6A6A6A; font-family: Calibri; width: 437px; font-weight: bold;" 
                            align="right">
                            Department Name</td>
                        <td align="left" style="width: 130px">
                            <input class="InputDefaultStyle" runat="server" id="DepartmentName" 
                                onkeypress="AllowOnlyTNS();" 
                                style="font-size: x-small; color: #6A6A6A; font-family: serif" 
                                maxlength="20" />
                            </td>
                        <td align="left">
                            <span style="font-size: x-small; color: #6A6A6A">
                            <asp:ImageButton ImageUrl="~/Images/Add.png" onmouseover="this.src='../../Images/Addover.png';" onmouseout="this.src='../../Images/Add.png';" ID="Button1" runat="server" OnClientClick="return fnSave();"
                                Text="Add" OnClick="Button1_Click1" ImageAlign="AbsMiddle" /></span></td>
                        <td style="font-size: x-small; color: #6A6A6A">
                            &nbsp;</td>
                    </tr>
                    <tr runat="server">
                        <td nowrap="nowrap" style="font-size: small; color: #6A6A6A; font-family: Calibri; width: 437px; font-weight: bold;" 
                            align="right">
                            &nbsp;</td>
                        <td align="left" style="width: 130px">
                            &nbsp;</td>
                        <td align="left">
                            &nbsp;</td>
                        <td style="font-size: x-small; color: #6A6A6A">
                            &nbsp;</td>
                    </tr>
                </table>
                <table width="100%">
                    
                    <tr>
                        <td id="dept_body" runat="server" align="center" width="50%">
                            <div class="gridheight_master" style="left: -1px; top: 0px">
                                <asp:GridView ID="grid_Department" runat="server" AutoGenerateColumns="False" Width="100%"
                                    DataKeyNames="DepartmentId" OnRowDeleting="Delete" OnRowEditing="Edit" 
                                    OnRowUpdating="Update" onrowcommand="grid_Department_RowCommand" 
                                    onrowdatabound="grid_Department_RowDataBound" CellPadding="4" 
                                    ForeColor="#333333" GridLines="None">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                        <asp:TemplateField>
                                        <HeaderTemplate>
                                        <table cellspacing="0" cellpadding="0" width="100%">
                                            <colgroup>        
                                                <col>                                
                                            </colgroup>
                                            <thead>
                                                <tr>
                                                    <th align="left" style="width: 80%; font-family: Calibri; color: #FFFFFF; font-weight: bold;">
                                                        Department List</th>
                                                    <td align="right" style="width: 10%; font-family: Calibri; color: #FFFFFF; font-weight: bold;"><asp:Label ID="lbledit" Text="Edit" runat="server"></asp:Label></td>
                                                    <td align="right" style="width: 10%; font-family: Calibri; color: #FFFFFF; font-weight: bold;"><asp:Label ID="lbldel" Text="Delete" runat="server"></asp:Label></td>
                                                </tr>
                                            </thead>
                                       </table>
                                   </HeaderTemplate>
                                            <ItemTemplate>
                                                <table class="dItemListContentTable" border="0" cellspacing="0" cellpadding="0" width="100%">
                                                    <colgroup>
                                                        <col class="dInboxContentTableCheckBoxCol">
                                                    </colgroup>
                                                    <tbody>
                                                        <tr>
                                                            <td id="chkid" runat="server" style="width: 10%;" align="left">
                                                                <input type="checkbox" id="Chk_Department" runat="server" /></td>
                                                            <td align="left" style="width: 80%;" nowrap="nowrap">
                                                            <input  runat="server" id="txtgrid" onkeypress="AllowOnlyTNS();" value ='<%# Eval("DepartmentName") %>' 
                                                                    style="font-family: Calibri; font-size:small; color: #6A6A6A; width: 100%; font-weight: bold;" 
                                                                    height="26px" />
                                                                <%--<asp:TextBox runat="server" Text='<%#Eval("DepartmentName")%>' ID="txtgrid" Enabled="false"></asp:TextBox>--%>
                                                            </td>
                                                            <td align="right" style="width: 10%;" nowrap="nowrap">
                                                                <asp:ImageButton ID="img_update" ImageUrl="../../Images/i_Edit.gif" runat="server" Style="border: 0"
                                                                    AlternateText="" CommandName="Update"  />
                                                                <asp:ImageButton ID="img_save" ImageUrl="../../Images/save.gif" runat="server" Style="border: 0"
                                                                    AlternateText="" CommandName="Edit" Visible="false"  />
                                                             </td>
                                                             <td style="width: 10%; font-family: Calibri;" nowrap="nowrap" align="center">
                                                                <asp:ImageButton ImageUrl="../../Images/delete_icon.gif" ID="imgdel" runat="server" 
                                                                     CommandName="Delete"  OnClientClick="return validate()" />
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
                         <td id="branch_body" runat="server" align="center">
                            <div class="gridheight_master">
                                <asp:GridView ID="grid_Branch" runat="server" AutoGenerateColumns="False" Width="50%"
                                    DataKeyNames="CompanyId" OnRowDeleting="Delete" OnRowEditing="Edit" 
                                    CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                        <asp:TemplateField>
                                        <HeaderTemplate>
                                        <table cellspacing="0" cellpadding="0" width="100%">
                                            <colgroup>        
                                                <col>                                
                                            </colgroup>
                                            <thead>
                                                <tr>
                                                    <th align="left" style="width: 80%; font-family: Calibri; color: #FFFFFF; font-weight: bold;">Branch List</th>
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
                                                                <input type="checkbox" id="Chk_Branch" runat="server" /></td>
                                                            <td style="width: 80%; font-family: Calibri; font-size: x-small; font-weight: bold;" nowrap>
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
                <br />
                            </td>
        </tr>
        <tr>
            <td align="center">
                <input type="hidden" id="ToolBarCode" name="ToolBarCode" runat="server" value="0" />&nbsp;
                <input id="hDepartmentID" runat="server" type="hidden" value="0" /></td>
        </tr>
    </table>
</asp:Content>