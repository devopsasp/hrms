<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Goal_settings.aspx.cs" Inherits="Hrms_Tasks_Goal_settings"  MasterPageFile="~/HRMS.master"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
     <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                            <div class="divWaiting">
                            
                            <asp:Image ID="imgWait" runat="server" ImageAlign="Middle" ImageUrl="~/Images/loading2.gif" Height="100px" Width="100px" />
                                <%--<img src="../loading.gif" alt="Loading" style="position:relative;" />--%>
                            </div>
                            </ProgressTemplate>
                            </asp:UpdateProgress>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
    <div ><h3 class="page-header">Goal Settings</h3></div>
      <div style="width: 80%">
                                    <table cellpadding="1%" cellspacing="1%" width="100%" class="table table-striped table-bordered table-hover">
                        
                        <tr>
                            <td>
                                Goal ID</td>
                            <td>
                            <input class="form-control" runat="server" id="txtgoalid" /></td>
                            
                            <td> Goal Name</td>
                            <td>
                            <input class="form-control" runat="server" id="txtgoalname" /></td>
                        </tr>
                        <tr>
                            <td >Goal Type</td>
                            <td >
                           
                            <asp:DropDownList ID="ddl_goaltype" runat="server" CssClass="form-control" >
                                <asp:ListItem Text="--Select--"></asp:ListItem>
                                <asp:ListItem Text="General"></asp:ListItem>
                                <asp:ListItem Text="Performance"></asp:ListItem>
                            </asp:DropDownList>

                            </td>
                            
                        
                            <td>Description</td>
                            <td>
                            <input id="txtdescription" class="form-control" runat="server"  maxlength="150" aria-multiline="true"/></td>
                           
                        </tr>
                        
                        <tr>
                            <td >
                                &nbsp;</td>
                            <td >
                                &nbsp;</td>
                            <td >
                                &nbsp;</td>

                            <td  style="text-align:right" >
                                 <asp:Button ID="Button1" runat="server" CommandName="add" Font-Bold="True" CssClass="btn btn-success" Text="ADD" OnClick="Button1_Click" />
                           <%-- <asp:Button ID="btn_formulaedit" runat="server" Text="Edit" onclick="btn_formulaedit_Click" class="btn btn-info"  />
                            <asp:Button ID="btn_cancel" runat="server" onclick="btn_cancel_Click" class="btn btn-danger" Text="Cancel" />--%>
                            </td>
                        </tr>

                     </table>
                     </div>
            <table style="width:100%">
                <tr>
                <td>
                   <div id="div1" runat="server">
                      <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"  class="table table-striped table-bordered table-hover" GridLines="None"  HorizontalAlign="Center" OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowUpdating="GridView1_RowUpdating" OnRowDeleting="GridView1_RowDeleting">
                                  <%--   <HeaderStyle Height="100px" />
                                     <RowStyle Height="70px" />--%>
                                     <Columns>
                                         <asp:TemplateField>
                                             <HeaderTemplate>
                                                 S.No.
                                             </HeaderTemplate>
                                             <ItemTemplate>
                                                 <asp:Label ID="lblSRNO" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Goal ID">
                                             <ItemTemplate>
                                                 <asp:Label ID="Lblgid" runat="server" Text='<%# Eval("Goal_id") %>'></asp:Label>
                                             </ItemTemplate>
                                              <EditItemTemplate>
                                                 <asp:Label ID="gidedit" runat="server" Text='<%# Bind("Goal_id") %>'></asp:Label>
                                             </EditItemTemplate>

                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Goal Name" ItemStyle-HorizontalAlign="Center">
                                             <ItemTemplate>
                                                 <asp:Label ID="Lblgname" runat="server" Text='<%# Eval("Goal_name") %>'></asp:Label>
                                             </ItemTemplate>
                                             <EditItemTemplate>
                                                 <asp:TextBox ID="gnameedit" runat="server" Text='<%# Bind("Goal_name") %>'></asp:TextBox>
                                             </EditItemTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Goal Type" ItemStyle-HorizontalAlign="Center">
                                             <ItemTemplate>
                                                 <asp:Label ID="Lblgtype" runat="server" Text='<%# Eval("Goal_type") %>'></asp:Label>
                                             </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                              <EditItemTemplate>
                                                 
                                                  <asp:DropDownList ID="gtypeedit" runat="server" DataTextField="gtype" DataValueField="gtype">
                                                      <asp:ListItem>--Select--</asp:ListItem>
                                                      <asp:ListItem>General</asp:ListItem>
                                                      <asp:ListItem>Performance</asp:ListItem>
                                                  </asp:DropDownList>
                                             </EditItemTemplate>
                                          </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="Center">
                                             <ItemTemplate>
                                                 <asp:Label ID="Lblgdesc" runat="server" Text='<%# Eval("Goal_description") %>'></asp:Label>
                                             </ItemTemplate>
                                              <EditItemTemplate>
                                                 <asp:TextBox ID="gdescedit" runat="server" Text='<%# Bind("Goal_description") %>'></asp:TextBox>
                                             </EditItemTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                        <asp:CommandField ItemStyle-Wrap="false" ButtonType="Image" EditImageUrl="~/Images/edit_icon.png" CancelImageUrl="~/Images/delete_icon.jpg" UpdateImageUrl="~/Images/save_icon.jpg" ShowEditButton="True" ShowDeleteButton="true" DeleteImageUrl="~/Images/delete_icon.jpg" /> 
                                     </Columns>
                                     <PagerStyle HorizontalAlign="Center" />
                                   
                                     <SelectedRowStyle Font-Bold="True" />
                                     <HeaderStyle Font-Bold="True" Font-Names="Calibri" />
                                     <EditRowStyle />
                                  
                                 </asp:GridView>
          </div>
        </td>
     </tr>
  </table>   
                                </ContentTemplate>
                        </asp:UpdatePanel>
</asp:Content>
