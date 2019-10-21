<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="skill.aspx.cs" Inherits="Hrms_Master_Default" Title="Welcome to HRMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div>
    <h2 class="page-header">Skills</h2></div>
<div style="float:left;">   <asp:DropDownList ID="ddl_branch" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="ddl_branch_SelectedIndexChanged" Width="115px">
                                    </asp:DropDownList></div>
                                    <div>  </div>
<table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td id="tdComposeHeader" valign="top" align="center">
             
                   

                    <table cellpadding="5%" cellspacing="1%" width="100%" class="table">
                        
                        <tr>
                            <td align="right" width="25%">
                                Skill Name</td>
                            <td align="center" width="45%">
                            <input class="form-control" runat="server" id="skillName" maxlength="20"/></td>
                            <td align="left" width="33%">                                
                                <asp:LinkButton ID="Button1" runat="server"  CssClass="btn btn-success" OnClientClick="return fnSave();" Text="Add" OnClick="Button1_Click1"/>
                                </td>
                        </tr>                        
                               <tr><td colspan="3"></td></tr>      
                     </table>
                     <table align="center" style="width: 62%">
                       <tr valign="top">
                         <td width="40%" valign="top">
                           <asp:GridView ID="grid_skill" runat="server" AutoGenerateColumns="False" 
                                 Width="100%" DataKeyNames="skillId" OnRowEditing="Edit" OnRowUpdating="Update" 
                                 onrowcommand="grid_skill_RowCommand"  
                                 onrowdeleting="grid_skill_RowDeleting" CellPadding="4"   CssClass="table table-striped table-bordered table-hover"
                                 GridLines="None">
                               <RowStyle />
                             <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <table cellSpacing="0" cellPadding="0" width="100%">
                                            
                                            <thead>
                                                <tr>
                                                    <td align="left" style="width: 86%;">Skill List</td>
                                                    <td  style="width: 7%;">
                                                                <asp:Label ID="lbledit" Text="Edit" runat="server"></asp:Label></td>
                                                                 <td id="del" align="center" 
                                                        style="width: 7%;">
                                                               <asp:Label ID="lbldel" Text="Delete" runat="server"></asp:Label></td>
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

                                                     <td style="width: 86%;">
                                                            <input runat="server" id="txtgrid" onkeypress="AllowOnlyTND();" disabled="disabled" value='<%# Eval("skillName") %>'  style="width:100%;"  class="form-control"
                                                            
                                                                />
                                                     </td>
                                                        <td align="center" style="width: 7%;" >
                                                        <asp:LinkButton id="img_update" runat="server" CssClass="btn btn-success btn-circle glyphicon glyphicon-plus-sign"
                                                                Text="" CommandName="Update"  > </asp:LinkButton>
                                                        <asp:LinkButton id="img_save"  runat="server"  CssClass="btn btn-success btn-circle glyphicon glyphicon-plus-sign"
                                                                Text="" CommandName="Edit" Visible="false"  ></asp:LinkButton>
                                                     </td>
                                                     <td style="width:7%;" align="center">
                                                        <asp:LinkButton ID="imgdel" runat="server"  CssClass="btn btn-danger btn-circle glyphicon glyphicon-minus-sign"
                                                             CommandName="Delete" 
                                                             OnClientClick="return validate()" ></asp:LinkButton>
                                                     </td>
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
                <td align="center" valign="top"><input type=hidden id="ToolBarCode" name="ToolBarCode" runat="server" value="0" />
                    <input id="hskillID" runat="server" type="hidden" value="0" /></td>
            </tr>
            <tr>
            
                <td align="center">
                    &nbsp;</td>
            </tr>
        </table>
</asp:Content>

