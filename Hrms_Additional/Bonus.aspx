<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Bonus.aspx.cs" Inherits="Hrms_Additional_Default" Title="Welcome to HRMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<script language="javascript" type="text/javascript">
   
    function show_message()
    {
        alert("Leave Name Already Exist");
    }
    </script>

<script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>
<script language="javascript" type="text/javascript" src="../datecheck.js"></script>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td id="tdComposeHeader" valign="top" align="right">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr class="border">
                        <td class="border" style="">
                            <span class="Title">&nbsp;&nbsp;&nbsp;<span 
                                class="style1" 
                                style="height: 17px; font-family: Calibri; font-size: medium; font-weight: bold;">Bonus</span></span></td>
                        <td align="left" style="width: 276px; height: 29px; " valign="baseline">
                            <asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged"
                                 CssClass="form-control">
                            </asp:DropDownList></td>
                    </tr>
                </table>
                <table cellpadding="5" cellspacing="1" width="100%" id="tbl_details" runat="server">
                    <tr id="Tr1" runat="server">
                        <td colspan="4" align="center">
                            &nbsp;<asp:Label ID="lbl_Error" CssClass="Error" runat="server" ForeColor="Red" Font-Bold="True" Width="40%"></asp:Label></td>
                    </tr>
                    <%--<tr id="row_branch" runat="server">
                                            <td class="dComposeItemLabel" nowrap="nowrap" style="height: 29px">
                                                Branch</td>
                                            <td align="left" style="width: 276px; height: 29px" valign="baseline">
                                                <asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged"
                                                    CssClass="InputDefaultStyle">
                                                </asp:DropDownList></td>
                                            <td class="dComposeItemLabel" nowrap="nowrap" style="height: 29px">
                                            </td>
                                            <td style="width: 113px; height: 29px">
                                            </td>
                                        </tr>--%>
                    <tr id="row_emp" runat="server">
                        <td class="dComposeItemLabel" nowrap="nowrap" 
                            
                            
                            
                            style="height: 4px; font-family: Calibri; color: #6A6A6A; font-size: small; width: 471px;">
                            Department&nbsp;&nbsp;&nbsp; </td>
                        <td align="left" style="height: 4px; width: 276px;" valign="baseline">
                            <span style="color: #444444"><span class="style1" style="height: 11px"><span style="color: #5B5B5B">
                            <span style="color: #6A6A6A"><span style="font-size: small">
                            <span style="font-size: x-small">
                            <asp:DropDownList ID="ddl_department" runat="server" 
                                 CssClass="form-control" AutoPostBack="True" 
                                Font-Names="Calibri" ForeColor="#333333" 
                                onselectedindexchanged="ddl_department_SelectedIndexChanged">
                            </asp:DropDownList></span></span></span></span></span></span></td>
                        <td class="dComposeItemLabel" nowrap="nowrap" style="height: 4px">
                            </td>
                        <td style="height: 4px; width: 113px;">
                            </td>
                    </tr>
                    <tr id="Emp_details" runat="server">
                        <td class="dComposeItemLabel" nowrap="nowrap" 
                            
                            
                            style="height: 29px; font-family: Calibri; color: #6A6A6A; font-size: small; width: 471px;">
                            Employee&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; </td>
                        <td align="left" style="height: 29px; width: 276px;" valign="baseline">
                            <span style="color: #444444"><span class="style1" style="height: 1px"><span style="color: #5B5B5B">
                            <span style="color: #6A6A6A"><span style="font-size: small">
                            <span style="font-size: x-small">
                            <asp:DropDownList ID="ddl_Employee" runat="server"  CssClass="form-control" AutoPostBack="True"
                                OnSelectedIndexChanged="ddl_Employee_SelectedIndexChanged" 
                                Font-Names="Calibri" ForeColor="#333333">
                            </asp:DropDownList></span></span></span></span></span></span></td>
                        <td class="dComposeItemLabel" nowrap="nowrap" style="height: 29px">
                            &nbsp;</td>
                        <td style="height: 29px; width: 113px;">
                            &nbsp;</td>
                    </tr>
                    <tr id="Tr2" runat="server">
                        <td class="dComposeItemLabel" nowrap="nowrap" 
                            
                            
                            style="font-family: Calibri; color: #6A6A6A; font-size: small; width: 471px;">
                            Enter Date&nbsp; &nbsp;&nbsp;&nbsp; </td>
                        <td>
                        <input runat="server"  CssClass="form-control" id="txt_date" 
                                
                                style="width:45%; font-family: Calibri; color: #6A6A6A; font-size: x-small;" 
                                onkeyup="fn_date(event,this.id);" maxlength="10"  /></td>
                        <td class="dComposeItemLabel" nowrap="nowrap" style="height: 29px">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                    </tr>
                </table>
                <table width="100%" id="tbl_grd" runat="server">
                    <tr valign="top">
                        <td align="center" valign="top" width="50%">
                            <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
                                AutoGenerateColumns="False" CellPadding="4" Font-Size="Smaller" ForeColor="#333333" 
                                Height="16px" Width="555px" GridLines="None" class="table table-striped table-bordered table-hover" >
                                <FooterStyle  Font-Bold="True"  />
                                <RowStyle Font-Names="Calibri" Font-Size="Small" 
                                    />
                                <Columns>
                                    <asp:TemplateField HeaderText="Grade" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Grade") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                
<%--
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Increment by">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("Increment_Type") %>'></asp:Label>
                    </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>--%>
                
                                    <asp:TemplateField HeaderText="Increment by" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("Bonus_Type") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Increment Value" 
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("Bonus") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Formula Code" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label6" runat="server" Text='<%# Eval("formula_name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle HorizontalAlign="Center" />
                                <SelectedRowStyle  Font-Bold="True" />
                                <HeaderStyle BorderStyle="None" Font-Bold="True" 
                                    Font-Names="Calibri" Font-Size="Small" HorizontalAlign="Left" />
                                <EditRowStyle />
                                <AlternatingRowStyle  />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td valign="top" width="50%">
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Button ID="btn_Back" runat="server" Text="Back" class="btn btn-info" CausesValidation="False" Height="30px" OnClick="btn_Back_Click"/>
                            <%--<asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/Images/Back.png" onmouseover="this.src='../Images/Backover.png';" onmouseout="this.src='../Images/Back.png';" OnClick="btn_Back_Click"
                                CausesValidation="False" Height="30px" />--%>
                            <asp:Button ID="btn_save" runat="server" Text="Save" OnClick="btn_save_Click" class="btn btn-success"/>
                           <%-- <asp:ImageButton ID="btn_save" runat="server" ImageUrl="~/Images/Save.png" onmouseover="this.src='../Images/Saveover.png';" onmouseout="this.src='../Images/Save.png';" OnClick="btn_save_Click" />--%>
                            <asp:Button ID="btn_update" runat="server" Text="Modify"  OnClick="btn_update_Click" class="btn btn-info"/>
                            <%--<asp:ImageButton ID="btn_update" runat="server" ImageUrl="~/Images/Modify.png" 
                                onmouseover="this.src='../Images/Modifyover.png';" 
                                onmouseout="this.src='../Images/Modify.png';"
                                OnClick="btn_update_Click" />--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
