<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="CreateUser.aspx.cs" Inherits="Hrms_Operations_Default" Title="User Creation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td class="tdComposeHeader" valign="top" align="right" style="height: 232px">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr class="border">
                        <td  height="35px" class="border">
                        <div><h2 class="page-header">User Creation</h2></div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                </table>
                <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                </asp:ToolkitScriptManager>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                            <div class="divWaiting">
                            
                            <asp:Image ID="imgWait" runat="server" ImageAlign="Middle" 
                                    ImageUrl="~/Images/loading2.gif" Height="100px" Width="100px" />
                               
                            </div>
                            </ProgressTemplate>
                            </asp:UpdateProgress>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>

                <table runat="server" id="tab_ddl" width="100%" cellpadding="5" cellspacing="1">
                    <tr id="row_branch" runat="server">
                        <td class="dComposeItemLabel" nowrap="nowrap" align="right" >
                            &nbsp;
                            <asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged" CssClass="form-control">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="row_emp" runat="server">
                        <td id="col1_emp" runat="server">
                        <div id="div1" class="scrollable-container" runat="server" style="overflow-x:auto;overflow: auto;width :500px;   height: 500px;">
                           <asp:DropDownList ID="ddl_department" runat="server" AutoPostBack="True" class="form-control" Width="100%"
                                onselectedindexchanged="ddl_department_SelectedIndexChanged"></asp:DropDownList>
                            <asp:CheckBoxList ID="ddl_Employee" runat="server" CssClass="form-control" 
                                Font-Names="Calibri" 
                                onselectedindexchanged="ddl_Employee_SelectedIndexChanged" 
                                AutoPostBack="True">
                            </asp:CheckBoxList>
                            </div>
                            
                        </td>

                        <td align="center">
                        <div id="div2" class="scrollable-container" runat="server" style="overflow-x:auto;overflow: auto;width :500px;   height: 500px;">
                           <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
                                                    AutoGenerateColumns="False" CssClass="table table-hover table-striped" 
                                                    GridLines="None" ShowFooter="True">                                                    
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Employee Name" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_empname" runat="server" Text='<%# Eval("username") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
  
                                                        <asp:TemplateField HeaderText="Student Department" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddl_department"  runat="server"></asp:DropDownList>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Button ID="btn_save" runat="server" CssClass="btn btn-success" Text="Save" 
                                                                    onclick="btn_save_Click" />
                                                            </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                    </Columns>                                                    
                                                </asp:GridView>
                            </div>
                            
                        </td>

                    </tr>
                    <tr runat="server" id="row_user">
                        <td runat="server" class="dComposeItemLabel" nowrap="nowrap"
                            style="font-family: calibri">
                            &nbsp;</td>
                    </tr>
                    <tr id="row_showdet_btn" runat="server">
                        <td id="Td1" runat="server" class="dComposeItemLabel" nowrap="nowrap" style="height: 30px">
                            <%--<asp:ImageButton ImageUrl="~/Images/Assign.png" onmouseover="this.src='../Images/Assignover.png';" onmouseout="this.src='../Images/Assign.png';" ID="btn_save" runat="server" OnClick="btn_save_Click" />--%>
                        </td>
                    </tr>
                </table>
              </ContentTemplate>
             </asp:UpdatePanel>
           </td>
        </tr>
    </table>


</asp:Content>

