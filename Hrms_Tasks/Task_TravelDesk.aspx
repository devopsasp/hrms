<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Task_TravelDesk.aspx.cs" Inherits="Hrms_Tasks_Default" Title="ePay-HRMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>
<script language="javascript" type="text/javascript" src="../datecheck.js"></script>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td id="td1" valign="top">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="35px" class="border" >
                            <span class="Title" 
                                 >&nbsp;<span class="Title"></span>&nbsp; Human Resource Management System&nbsp; </span>
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
                                                <td colspan="4">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <span class="Title">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<span 
                                                        style="font-family: calibri"><H4>Travel Request</H4></span></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="4">
                                                    <asp:Label ID="lbl_Error" runat="server" Font-Bold="False" ForeColor="Red" 
                                                        Font-Size="Small" Font-Names="Calibri"></asp:Label>&nbsp;&nbsp;
                                                    <asp:Label ID="lbl_dept" runat="server" Font-Names="Calibri"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Label ID="lbl_desg" runat="server" Font-Names="Calibri"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 312px; font-family: 'Bell MT'; font-size: small; font-weight: bold; height: 30px;">
                                                    <span style="font-style: normal"><span style="font-family: 'Bell MT'"><b>
                                                    <span style="font-size: medium"><span style="font-weight: normal"><i>
                                                    <span style="font-size: small">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Label ID="Label8" runat="server" Text="Country" style="font-style: normal" 
                                                        Font-Bold="True" Font-Names="Calibri"></asp:Label>
                                                    </span></i></span></span></b></span></span>
                                                </td>
                                                <td style="width: 123px; height: 30px;">
                                                    <input id="txt_country"  CssClass="form-control" type="text" runat="server" /></td>
                                                <td style="width: 137px; font-family: 'Bell MT'; font-size: small; height: 30px;">
                                                    <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Label ID="Label2" runat="server" Text="City" style="font-style: normal" 
                                                        Font-Names="Calibri"></asp:Label>
                                                    </b>
                                                </td>
                                                <td style="height: 30px">
                                                    <input id="txt_city"  CssClass="form-control" type="text" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 312px; font-family: 'Bell MT'; font-size: small; font-weight: bold; height: 30px;">
                                                    <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Label ID="Label3" runat="server" Text="Departure Date" 
                                                        style="font-style: normal" Font-Names="Calibri"></asp:Label>
                                                    </b>
                                                </td>
                                                <td style="width: 123px; height: 30px;">
                                                    <input id="txt_Ddate" type="text" 
                                                         CssClass="form-control" runat="server" onkeyup="fn_date(event,this.id);" maxlength="10"  /></td>
                                                <td style="width: 137px; font-family: 'Bell MT'; font-size: small; height: 30px;">
                                                    <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Label ID="Label4" runat="server" Text="Arrival Date" 
                                                        style="font-style: normal" Font-Names="Calibri"></asp:Label>
                                                    </b>
                                                </td>
                                                <td style="height: 30px">
                                                    <input id="txt_Adate" type="text"  CssClass="form-control" runat="server" onkeyup="fn_date(event,this.id);" maxlength="10" /></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 312px; font-family: 'Bell MT'; font-size: small; font-weight: bold; height: 30px;">
                                                    <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Label ID="Label5" runat="server" Text="Seat Preference" 
                                                        style="font-style: normal" Font-Names="Calibri"></asp:Label>
                                                    </b>
                                                </td>
                                                <td style="width: 123px; height: 30px;">
                                                    <span style="font-family: 'Bell MT'"><b><span style="font-size: medium"><i>
                                                    <span style="font-size: small">
                                                    <asp:DropDownList ID="ddl_pref" runat="server" Height="16px" Width="128px" 
                                                         CssClass="form-control">
                                                        <asp:ListItem>Aisle</asp:ListItem>
                                                        <asp:ListItem>Window</asp:ListItem>
                                                        <asp:ListItem>Front of plane</asp:ListItem>
                                                        <asp:ListItem>Rear of plane</asp:ListItem>
                                                    </asp:DropDownList>
                                                    </span></i></span></b></span></td>
                                                <td style="width: 137px; font-family: 'Bell MT'; font-size: small; height: 30px;">
                                                    <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Label ID="Label6" runat="server" Text="Project Name" 
                                                        style="font-style: normal" Font-Names="Calibri"></asp:Label>
                                                    </b>
                                                </td>
                                                <td style="height: 30px" >
                                                    <input id="txt_project" type="text"  CssClass="form-control" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td style="font-family: 'Bell MT'; font-size: small; font-weight: bold">
                                                    <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Label ID="Label7" runat="server" style="font-style: normal" 
                                                        Text="Other Information" Font-Names="Calibri"></asp:Label>
                                                    </b></td>
                                                <td colspan="3" 
                                                    style="font-family: 'Bell MT'; font-size: small; font-weight: bold">
                                                    <input id="txt_other"  CssClass="form-control" style="width: 404px; height: 60px" 
                                                        type="text" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="height: 29px">
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="4">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="Img_Submit" runat="server" Text="Submit" onclick="Img_Submit_Click" class="btn btn-success"/>
                                                    <%--<asp:ImageButton 
                                                        ID="Img_Submit" runat="server" ImageUrl="~/Images/Submit.png" onmouseover="this.src='../Images/Submitover.png';" onmouseout="this.src='../Images/Submit.png';"
                                                         />--%>
                                                    <asp:Button ID="Img_cancel" runat="server" Text="Cancel" 
                                                        class="btn btn-success" />
                                                  <%-- <asp:ImageButton ID="Img_cancel" runat="server" ImageUrl="~/Images/Reset.png" onmouseover="this.src='../Images/Resetover.png';" onmouseout="this.src='../Images/Reset.png';"/>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    
                                                          
                                                    </td>
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
    <table width="100%" cellpadding="0" cellspacing="0">
    <tr>
    <td class="border">
    
    <asp:GridView ID="GridView1" Font-Size="Small" runat="server" AllowSorting="True" 
            AutoGenerateColumns="False" Height="16px" class="table table-striped table-bordered table-hover"
            Width="752px" 
             onrowcommand="GridView1_RowCommand" CellPadding="4" 
            ForeColor="#333333" GridLines="None" 
            onrowdeleting="GridView1_RowDeleting" 
            onrowdatabound="GridView1_RowDataBound" onrowediting="GridView1_RowEditing" 
            onselectedindexchanged="GridView1_SelectedIndexChanged" HorizontalAlign="Center" 
                                    onrowupdating="GridView1_RowUpdating" 
                                    onrowcancelingedit="GridView1_RowCancelingEdit" 
            Font-Names="Calibri">
            <FooterStyle Font-Bold="True"  />
            <RowStyle />
            <Columns>
            
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="S.no">
                
                    <ItemTemplate>
                        <asp:Label ID="lbl_sno" runat="server" Text='<%# Eval("pn_TravelID") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:Label ID="Edit_lbl_sno" runat="server" Text='<%# Bind("pn_TravelID") %>' 
                            Height="21px" Width="98px"></asp:Label>
                    </EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
            
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Country">
                
                    <ItemTemplate>
                        <asp:Label ID="lbl_country" runat="server" Text='<%# Eval("Country") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="Edit_lbl_country" runat="server" Text='<%# Bind("Country") %>' 
                            Height="21px" Width="98px"></asp:TextBox>
                    </EditItemTemplate>
<ControlStyle Width="50px" />
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="City">
                    <ItemTemplate>
                        <asp:Label ID="lbl_city" runat="server" Text='<%# Eval("City") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="Edit_lbl_city" runat="server" Text='<%# Bind("City") %>' 
                            Width="187px"></asp:TextBox>
                    </EditItemTemplate>
<ControlStyle Width="50px" />
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Departure">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Ddate" runat="server" Text='<%# Eval("Departure_Date" , "{0:MMMM d, yyyy}")%>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="Edit_lbl_Ddate" runat="server" Text='<%# Bind("Departure_Date" , "{0:dd/MM/yyyy}") %>'  Width="74px" MaxLength="10"></asp:TextBox>
                    </EditItemTemplate>
                    
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Arrival">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Adate" runat="server" Text='<%# Eval("Arrival_Date" , "{0:MMMM d, yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="Edit_lbl_Adate" runat="server" Text='<%# Bind("Arrival_Date" , "{0:dd/MM/yyyy}") %>'  Width="74px" MaxLength="10"></asp:TextBox>
                    </EditItemTemplate>
                    
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Preference">
                    <ItemTemplate>
                        <asp:Label ID="lbl_pref" runat="server" Text='<%# Eval("Seat_Preference") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:DropDownList ID="Edit_lbl_pref" runat="server" DataTextField="Seat_Preference" 
                            DataValueField="Seat_Preference" Width="60px" >
                            <asp:ListItem>Aisle</asp:ListItem>
                            <asp:ListItem>Window</asp:ListItem>
                            <asp:ListItem>Front of plane</asp:ListItem>
                            <asp:ListItem>Rear of Plane</asp:ListItem>
                    </asp:DropDownList>
                    
                    </EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>


                <asp:TemplateField ItemStyle-HorizontalAlign="Center"  HeaderText="Project">
                    <ItemTemplate>
                        <asp:Label ID="lbl_project" runat="server" Text='<%# Eval("Project_name") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="Edit_lbl_project" runat="server" Text='<%# Bind("Project_name") %>' Width="74px"></asp:TextBox>
                    </EditItemTemplate>
<ControlStyle Width="75px" />
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center"  HeaderText="Other Information">
                     <ItemTemplate>
                        <asp:Label ID="lbl_other" runat="server" Text='<%# Eval("other_info") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="Edit_lbl_other" runat="server" Text='<%# Bind("other_info") %>' Width="74px"></asp:TextBox>
                    </EditItemTemplate>
<ControlStyle Width="150px" />
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            </Columns>
            <PagerStyle  HorizontalAlign="Center" />
            <SelectedRowStyle  Font-Bold="True"  />
            <HeaderStyle Font-Bold="True"  />
            <EditRowStyle />
            <AlternatingRowStyle  />
            <EmptyDataTemplate>
            <asp:Label ID="lblempty" Text="No Records" runat="server">
            </asp:Label> 
            
            </EmptyDataTemplate>
        </asp:GridView>
    
    </td>
    </tr>
    </table>
                                              

</asp:Content>

