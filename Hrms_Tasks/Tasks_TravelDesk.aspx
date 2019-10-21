<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Tasks_TravelDesk.aspx.cs" Inherits="Hrms_Tasks_Default" Title="ePay-HRMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td id="td1" valign="top">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="35px" class="border">
                            <span class="Title" 
                                style="font-family: Calibri; font-size: medium; font-weight: bold">&nbsp;&nbsp; Human Resource Management System -----&gt; Travel Desk </span>
                        </td>
                    </tr>
                </table>
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr valign="top">
                        <td id="tdComposeHeader" valign="top" align="center">
                            <table cellpadding="5px" cellspacing="1px" width="95%">
                                <tr>
                                    <td width="100%">
                                        <table width="100%" align="center" 
                                            style="color: #6A6A6A; font-size: 15pt; font-weight: 700; font-family: 'Book Antiqua'" >
                                            <tr>
                                                <td>
                                                    <span class="Title">&nbsp;&nbsp;&nbsp;<span 
                                                        
                                                        style="font-family: calibri; font-size: medium; font-weight: bold; "><h3>Travel Request</h3></span></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lbl_Error" runat="server" Font-Bold="True" ForeColor="Red" 
                                                        Font-Names="Calibri"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    
                                            <asp:GridView ID="GridView1" Font-Size="X-Small" runat="server" AllowSorting="True" 
            AutoGenerateColumns="False" Height="16px" ShowFooter="True" class="table table-striped table-bordered table-hover"
            Width="918px" CellPadding="4" 
            ForeColor="#333333" GridLines="None">
            <FooterStyle  Font-Bold="True" ForeColor="White" />
                                                <RowStyle Font-Names="Calibri"  />
            <Columns>
            
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Emp ID">
                
                    <ItemTemplate>
                        <asp:Label ID="lbl_empid" runat="server" Text='<%# Eval("pn_EmployeeID") %>'></asp:Label>
                    </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Emp Name">
                
                    <ItemTemplate>
                        <asp:Label ID="lbl_empname" runat="server" Text='<%# Eval("Employee_name") %>'></asp:Label>
                    </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                   <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Department">
                
                    <ItemTemplate>
                        <asp:Label ID="lbl_dept" runat="server" Text='<%# Eval("Department") %>'></asp:Label>
                    </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Designation">
                
                    <ItemTemplate>
                        <asp:Label ID="lbl_desg" runat="server" Text='<%# Eval("Designation") %>'></asp:Label>
                    </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Country">
                
                    <ItemTemplate>
                        <asp:Label ID="lbl_country" runat="server" Text='<%# Eval("Country") %>'></asp:Label>
                    </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="City">
                
                    <ItemTemplate>
                        <asp:Label ID="lbl_city" runat="server" Text='<%# Eval("City") %>'></asp:Label>
                    </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Departure">
                
                    <ItemTemplate>
                        <asp:Label ID="lbl_Ddate" runat="server" Text='<%#Eval("Departure_Date" , "{0:MMMM d, yyyy}") %>'></asp:Label>
                    </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Arrival">
                
                    <ItemTemplate>
                        <asp:Label ID="lbl_Adate" runat="server" Text='<%# Eval("Arrival_Date" , "{0:MMMM d, yyyy}") %>'></asp:Label>
                    </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Preference">
                
                    <ItemTemplate>
                        <asp:Label ID="lbl_pref" runat="server" Text='<%# Eval("Seat_Preference") %>'></asp:Label>
                    </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Project">
                
                    <ItemTemplate>
                        <asp:Label ID="lbl_project" runat="server" Text='<%# Eval("Project_name") %>'></asp:Label>
                    </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Others">
                
                    <ItemTemplate>
                        <asp:Label ID="lbl_other" runat="server" Text='<%# Eval("other_info") %>'></asp:Label>
                    </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
            </Columns>
            <PagerStyle  HorizontalAlign="Center" />
            <SelectedRowStyle  Font-Bold="True"  />
            <HeaderStyle  Font-Bold="True"  Font-Names="Calibri" />
                                                <EditRowStyle  />
            <AlternatingRowStyle />
            <EmptyDataTemplate>
            <asp:Label ID="lblempty" Text="No Records" runat="server">
            </asp:Label> 
            
            </EmptyDataTemplate>
        </asp:GridView>
                                                    
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

</asp:Content>
