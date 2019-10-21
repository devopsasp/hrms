<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="Employee_Assets.aspx.cs" Inherits="Hrms_Employee_Employee_Assets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<div><h3 class="page-header">Asset Details&nbsp;</h3></div>
<div align="center" class="page-header">
    <asp:Label ID="lbl_empcodename" runat="server" Font-Bold="True" 
        Font-Size="Medium"></asp:Label>
    </div>
    <div align="center">
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div style="width: 70%">
                    <table cellpadding="1%" cellspacing="1%" width="100%" class="table table-striped table-bordered table-hover">
                        
                        <tr>
                            <td>
                                Asset Name</td>
                            <td >
                                <asp:ListBox ID="list_asset" runat="server" Font-Bold="True" CssClass="form-control" Height="150px" 
                                    SelectionMode="Multiple" Width="200px"></asp:ListBox>
                            </td>
                            <td >
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Font-Names="Calibri" 
                                    style="color: #444444" Text="Press Ctrl to Select Multiple Item"></asp:Label>
                            </td>
                            <td>
                                <asp:Button ID="btn_Assets" runat="server" class="btn btn-info" 
                                    OnClick="btn_Assets_Click" Text="Select Asset" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="right">
                                &nbsp;</td>
                        </tr>
                     </table>   
                     </div>
                     <div>  

                         <asp:GridView ID="grid_emp_Asset" runat="server" AutoGenerateColumns="False"  CssClass="table table-striped table-bordered table-hover"
                            DataKeyNames="AssetId" GridLines="None" Width="50%">
                             <Columns>
                                 <asp:TemplateField>
                                     <HeaderTemplate>
                                         <TABLE cellPadding="0" cellSpacing="0" width="100%">
                                             <colgroup>
                                                 <col>
                                                 </col>
                                             </colgroup>
                                             <thead>
                                                 <tr>
                                                     <th style="width:100%;">
                                                         Asset Name</th>
                                                 </tr>
                                             </thead>
                                         </TABLE>
                                     </HeaderTemplate>
                                     <ItemTemplate>
                                         <asp:TextBox ID="txtCourse" runat="server" CssClass="form-control" 
                                             Enabled="false" Text='<%#Eval("AssetName")%>'></asp:TextBox>
                                     </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Center" />
                                 </asp:TemplateField>
                                 <asp:TemplateField>
                                     <HeaderTemplate>
                                         <TABLE cellPadding="0" cellSpacing="0" width="100%">
                                             <colgroup>
                                                 <col>
                                                 </col>
                                             </colgroup>
                                             <thead>
                                                 <tr>
                                                     <th align="left" style="width: 100%;">
                                                         Number of Assets</th>
                                                 </tr>
                                             </thead>
                                         </TABLE>
                                     </HeaderTemplate>
                                     <ItemTemplate>
                                         <asp:DropDownList ID="ddl_assetno" runat="server" CssClass="form-control" Width="100%">
                                             <asp:ListItem Value="Select"></asp:ListItem>
                                             <asp:ListItem Value="0">1</asp:ListItem>
                                             <asp:ListItem Value="1">2</asp:ListItem>
                                             <asp:ListItem Value="2">3</asp:ListItem>
                                             <asp:ListItem Value="3">4</asp:ListItem>
                                             <asp:ListItem Value="4">5</asp:ListItem>
                                             <asp:ListItem Value="5">6</asp:ListItem>
                                             <asp:ListItem Value="6">7</asp:ListItem>
                                             <asp:ListItem Value="7">8</asp:ListItem>
                                             <asp:ListItem Value="8">9</asp:ListItem>
                                             <asp:ListItem Value="9">10</asp:ListItem>
                                         </asp:DropDownList>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                             </Columns>
                             
                         </asp:GridView>
                         </td>                                    
                         <asp:Button ID="btn_Back" runat="server" OnClick="btn_Back_Click" class="btn btn-info" Text="Back"/>
                         <asp:Button ID="btn_skip" runat="server" CausesValidation="False" Text="Skip"
                              OnClick="btn_skip_Click" ToolTip="Skip" class="btn btn-warning" />
                         <asp:Button ID="btn_save" runat="server" OnClick="btn_save_Click" class="btn btn-success"  Text ="Save"/>
                         <asp:Button ID="btn_update" runat="server" OnClick="btn_update_Click" class="btn btn-success" Text="Update"/>
           </div>
    </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
