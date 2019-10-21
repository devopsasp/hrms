<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="Employee_Education.aspx.cs" Inherits="Hrms_Employee_Default3" Title="Welcome to HRMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>


    <div><h3 class="page-header">Qualification Details&nbsp;</h3></div>
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
                                Course Name</td>
                            <td>
                                <asp:ListBox ID="lblEducation" runat="server" CssClass="form-control" Height="150px" SelectionMode="Multiple" Width="200px">
                                </asp:ListBox>
                            </td>
                            <td >
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Font-Names="Calibri" 
                                    style="color: #444444" Text="Press Ctrl to Select Multiple Item"></asp:Label>
                            </td>
                            <td>
                                <asp:Button ID="Button2" runat="server" class="btn btn-info" 
                                    OnClick="Button2_Click" Text="Select Course" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="right">
                                &nbsp;</td>
                        </tr>
                     </table>   
                     </div>
                     <div>  

                         <asp:GridView ID="grid_emp_education" runat="server" CssClass="table table-striped table-bordered table-hover"
                             AutoGenerateColumns="False" DataKeyNames="PGCourseID"  GridLines="None" ShowFooter="True" Width="100%">

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
                                                         Qualification</th>
                                                 </tr>
                                             </thead>
                                         </TABLE>
                                     </HeaderTemplate>
                                     <ItemTemplate>
                                          <asp:TextBox ID="txtCourse" CssClass="form-control" runat="server" Text='<%#Eval("PGCourseName")%>' 
                                             Width="100%" Enabled="false"></asp:TextBox>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField>
                                     <HeaderTemplate>
                                         <TABLE cellPadding="0" cellSpacing="0" width="80%">
                                             <colgroup>
                                                 <col>
                                                 </col>
                                             </colgroup>
                                             <thead>
                                                 <tr>
                                                     <th style="width:80%;">
                                                         Institution Name</th>
                                                 </tr>
                                             </thead>
                                         </TABLE>
                                     </HeaderTemplate>
                                     <ItemTemplate>
                                         <input type="text" runat="server" class="form-control" id="txtInstitution" value='<%#Eval("PGInstutionName")%>' onkeydown="AllowOnlyText1(event);" />
                                         <%--<asp:TextBox runat="server" ID="txtInstitution" Text=<%#Eval("PGInstutionName")%> ></asp:TextBox>--%>
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
                                                         Specialization</th>
                                                 </tr>
                                             </thead>
                                         </TABLE>
                                     </HeaderTemplate>
                                     <ItemTemplate>
                                         <asp:DropDownList CssClass="form-control" ID="ddl_Specialization" runat="server" Width="100%">
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
                                                         Completed Year</th>
                                                 </tr>
                                             </thead>
                                         </TABLE>
                                     </HeaderTemplate>
                                     <ItemTemplate>
                                         <asp:DropDownList ID="ddl_ComYear" CssClass="form-control" runat="server" Width="100%">
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
                                                         Percent</th>
                                                 </tr>
                                             </thead>
                                         </TABLE>
                                     </HeaderTemplate>
                                     <ItemTemplate>
                                         <asp:TextBox ID="txtPercentage" CssClass="form-control" runat="server" Text='<%#Eval("PGPercentage")%>' 
                                             Width="100%"></asp:TextBox>
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
                                                         Education Mode</th>
                                                 </tr>
                                             </thead>
                                         </TABLE>
                                     </HeaderTemplate>
                                     <ItemTemplate>
                                         <asp:DropDownList ID="ddl_Mode" CssClass="form-control" runat="server" Width="100%">
                                             <asp:ListItem Text="PartTime" Value="1"></asp:ListItem>
                                             <asp:ListItem Text="FullTime" Value="2"></asp:ListItem>
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
                                                         completed</th>
                                                 </tr>
                                             </thead>
                                         </TABLE>
                                     </HeaderTemplate>
                                     <ItemTemplate>
                                         <asp:DropDownList ID="ddl_inf" CssClass="form-control" runat="server" Width="100%">
                                             <asp:ListItem Text="Completed" Value="0"></asp:ListItem>
                                             <asp:ListItem Text="InComplete" Value="1"></asp:ListItem>
                                             <asp:ListItem Text="Pursuing" Value="2"></asp:ListItem>
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

