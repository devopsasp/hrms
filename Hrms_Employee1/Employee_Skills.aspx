<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="Employee_Skills.aspx.cs" Inherits="Hrms_Employee_Default" Title="Welcome to HRMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<div><h3 class="page-header">Skill Details&nbsp;</h3></div>
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
                                Skill Name</td>
                            <td >
                                <asp:ListBox ID="lblSkills" runat="server" Font-Bold="True" 
                                    Font-Names="Calibri" ForeColor="#666666" Height="150px" 
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
                                <asp:Button ID="btn_Skills" runat="server" class="btn btn-info" 
                                    OnClick="btn_Skills_Click" Text="Select Skill" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="right">
                                &nbsp;</td>
                        </tr>
                     </table>   
                     </div>
                     <div>  

                         <asp:GridView ID="grid_emp_Skills" runat="server" CssClass="table table-striped table-bordered table-hover"
                             AutoGenerateColumns="False" DataKeyNames="skillID"  GridLines="None" ShowFooter="True" Width="100%">

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
                                                         Skill Name</th>
                                                 </tr>
                                             </thead>
                                         </TABLE>
                                     </HeaderTemplate>
                                     <ItemTemplate>
                                         <asp:TextBox ID="txtCourse" CssClass="form-control" runat="server" Text='<%#Eval("SkillName")%>' 
                                             Enabled="false"></asp:TextBox>
                                     </ItemTemplate>
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
                                                     <th style="width:100%;">
                                                         Exp (Yrs)</th>
                                                 </tr>
                                             </thead>
                                         </TABLE>
                                     </HeaderTemplate>
                                     <ItemTemplate>
                                         <asp:DropDownList ID="ddl_Experience" CssClass="form-control" runat="server">
                                             <asp:ListItem Value="Select"></asp:ListItem>
                                             <asp:ListItem Value="0">Fresher</asp:ListItem>
                                             <asp:ListItem Value="1">1+</asp:ListItem>
                                             <asp:ListItem Value="2">2+</asp:ListItem>
                                             <asp:ListItem Value="3">3+</asp:ListItem>
                                             <asp:ListItem Value="4">4+</asp:ListItem>
                                             <asp:ListItem Value="5">5+</asp:ListItem>
                                             <asp:ListItem Value="6">6+</asp:ListItem>
                                             <asp:ListItem Value="7">7+</asp:ListItem>
                                             <asp:ListItem Value="8">8+</asp:ListItem>
                                             <asp:ListItem Value="9">9+</asp:ListItem>
                                             <asp:ListItem Value="10">10+</asp:ListItem>
                                             <asp:ListItem Value="11">11+</asp:ListItem>
                                             <asp:ListItem Value="12">12+</asp:ListItem>
                                             <asp:ListItem Value="13">13+</asp:ListItem>
                                             <asp:ListItem Value="14">14+</asp:ListItem>
                                             <asp:ListItem Value="15">15+</asp:ListItem>
                                             <asp:ListItem Value="16">More than 16</asp:ListItem>
                                         </asp:DropDownList>
                                     </ItemTemplate>
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
                                                     <th style="width:100%;">
                                                         Proficiency</th>
                                                 </tr>
                                             </thead>
                                         </TABLE>
                                     </HeaderTemplate>
                                     <ItemTemplate>
                                         <asp:DropDownList CssClass="form-control" ID="ddl_Proficiency" runat="server">
                                             <asp:ListItem Value="S">Select</asp:ListItem>
                                             <asp:ListItem Value="B">Beginner</asp:ListItem>
                                             <asp:ListItem Value="I">Intermediate</asp:ListItem>
                                             <asp:ListItem Value="E">Expert</asp:ListItem>
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

