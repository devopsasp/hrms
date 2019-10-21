<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Tasks_log.aspx.cs" Inherits="Hrms_Tasks_Default" Title="Task Log" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <table cellpadding="0" cellspacing="0" style="width: 100%; height: 274px;">
        <tr align="center" valign="top">
            <td class="tdComposeHeader" valign="top" align="center">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr valign="top">
                        <td valign="top">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                <td height="30px" align="left">
                                    <span class="Title" 
                                        style="font-family: calibri; font-size: medium; font-weight: bold; ">&nbsp;<h3>Task 
                                    Log</h3>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label8" runat="server"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <br />
                                    
                                    
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:connectionstring %>" 
                                        SelectCommand="select Employee_First_Name from paym_employee where pn_BranchID=@pn_BranchID ">
                                        <SelectParameters>
                                            <asp:Parameter Name="pn_BranchID" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server"></asp:SqlDataSource>
                                    
                                    
                                    </td>
                                   
                                </tr>
                            
                        </td>
                    </tr>
                   
                   <div>
                   
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                   
                       <asp:GridView ID="GridView1" Font-Size="Small" runat="server" AllowSorting="True" 
            AutoGenerateColumns="False" Height="16px" class="table table-striped table-bordered table-hover"
            Width="100%" 
            onrowcommand="GridView1_RowCommand" CellPadding="4" 
            ForeColor="#333333" GridLines="None" 
            onrowdeleting="GridView1_RowDeleting" 
            onrowdatabound="GridView1_RowDataBound" onrowediting="GridView1_RowEditing" 
            onselectedindexchanged="GridView1_SelectedIndexChanged" HorizontalAlign="Center" 
                                    onrowupdating="GridView1_RowUpdating" 
                                    onrowcancelingedit="GridView1_RowCancelingEdit" Font-Names="Calibri">
            <FooterStyle Font-Bold="True" />
                           <RowStyle Font-Names="Calibri"  />
            <Columns>
            
                <asp:TemplateField>
                        <HeaderTemplate>
                        S.No.</HeaderTemplate>
                        <ItemTemplate>
                        <asp:Label ID="lblSRNO" runat="server" 
                            Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                 <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Reference number">
                
                    <ItemTemplate>
                        <asp:Label ID="Labelref" runat="server" Text='<%# Eval("Reference_No") %>'></asp:Label>
                    </ItemTemplate>
                    
                    
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
            
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Task Title">
                
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("TaskTitle") %>'></asp:Label>
                    </ItemTemplate>
                    
                    
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("TDescription") %>'></asp:Label>
                    </ItemTemplate>
                    
                    

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="D.O.A.">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("DOA") %>'></asp:Label>
                    </ItemTemplate>
                    
                    
                    
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Department">
                    <ItemTemplate>
                        <asp:Label ID="Label9" runat="server" Text='<%# Eval("Department") %>'></asp:Label>
                    </ItemTemplate>
                    
                   

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Assigned to">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("Assigned") %>'></asp:Label>
                    </ItemTemplate>
                    
                   

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center"  HeaderText="Priority">
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("Priority") %>'></asp:Label>
                    </ItemTemplate>
                    
                    

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                    </ItemTemplate>
                    
                    
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center"  HeaderText="D.O.C.">
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# Eval("DOC") %>'></asp:Label>
                    </ItemTemplate>
                  
                    

<ControlStyle ></ControlStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center"  HeaderText="Remarks(Employee)">
                    <ItemTemplate>
                        <asp:Label ID="Label8" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
                    </ItemTemplate>
                    
                    

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center"  HeaderText="Permission">
                    <ItemTemplate>
                        <asp:Label ID="Label9" runat="server" Text='<%# Eval("Permission") %>'></asp:Label>
                    </ItemTemplate>
                    

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center"  HeaderText="Comments">
                    <ItemTemplate>
                        <asp:Label ID="Label10" runat="server" Text='<%# Eval("Comments") %>'></asp:Label>
                    </ItemTemplate>
                    
                    

<ControlStyle Width="125px"></ControlStyle>
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>                
            </Columns>
            <PagerStyle  HorizontalAlign="Center" />
            <SelectedRowStyle  Font-Bold="True"/>
            <HeaderStyle  Font-Bold="True" Font-Names="Calibri" />
                           <EditRowStyle  />
            <AlternatingRowStyle />
            <EmptyDataTemplate>
            <asp:Label ID="lblempty" Text="No Records" runat="server">
            </asp:Label> 
            
            </EmptyDataTemplate>
        </asp:GridView>
                   
                                <tr>
                                    <td>
                                        <br />
                                        
                                        <br />
                                    </td>
                                   
                                </tr>
                                                  <%--#5D7B9D--%>
                   </div>
                   <div>
                   <tr>
                   <td align="left">
            </td>
                   </tr>
                   </div>
                   
                </table>
            <asp:Button ID="Button1" runat="server" class="btn btn-success"  Text="Completed Tasks" OnClick="Button1_Click" />
                            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True" GroupTreeImagesFolderUrl="" Height="50px" ReportSourceID="CrystalReportSource1" ToolbarImagesFolderUrl="" ToolPanelWidth="200px" Width="350px" />
                            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                                <Report FileName="crystalreports\CrystalReport.rpt">
                                </Report>
                            </CR:CrystalReportSource>

</asp:Content>
