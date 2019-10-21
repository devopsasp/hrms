<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Tasks_List.aspx.cs" Inherits="Hrms_Tasks_Default" Title="Task List" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
    <style type="text/css">
    .rateStar
    {
	    white-space:nowrap;
	    margin:0em;
	    height:14px;
	    
    }
    .rateItem 
    {
        font-size: 0pt;
        width: 13px;
        height: 12px;
        margin: 0px;
        padding: 0px;
        display: block;
        
        background-repeat: no-repeat;
	    cursor:pointer;
    }
    .FillStar
    {
        background-image: url(ratingfilled.png);
    }
    .EmptyStar 
    {
        background-image: url(ratingempty.png);
    }
    .SaveStar
    {
        background-image: url(ratingsaved.png);
    }
          </style>

           <link href="../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
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

    <div ><h3 class="page-header">Task List<asp:DropDownList ID="ddl_Branch" 
            runat="server" AutoPostBack="True" CssClass="form-control" 
            OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged">
        </asp:DropDownList>
        </h3></div>
                                    <div align="center">
                                        <asp:Label ID="lbl_Error" runat="server" CssClass="Error" ForeColor="red" 
                                            Text=""></asp:Label>
                     </div>
                   <asp:Button ID="Buttonswitch" runat="server" class="btn btn-success" OnClick="Buttonswitch_Click"  />
                     <div >
                     <table style="width: 100%">
                       <tr valign="top">
                         <td>
                             <div style="overflow-x:auto;width:990px">
                             <asp:GridView ID="grd_view" runat="server" AllowSorting="True" 
                                 AutoGenerateColumns="False"
                                 class="table table-striped table-bordered table-hover" 
                                 CssClass="table table-striped table-bordered table-hover" Font-Names="Calibri" 
                                  GridLines="None" 
                                 onrowcancelingedit="grd_view_RowCancelingEdit" 
                                 onrowediting="grd_view_RowEditing" onrowupdating="grd_view_RowUpdating" OnRowDataBound="grd_view_RowDataBound">
                                 <FooterStyle Font-Bold="True" />
                                 <RowStyle />
                                 <Columns>
                                     <asp:TemplateField visible="false">
                                             <HeaderTemplate>
                                                 task id
                                             </HeaderTemplate>
                                             <ItemTemplate>
                                                 <asp:Label ID="lbltaskid" runat="server" Text='<%# Eval("TaskID") %>'></asp:Label>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Task Title" ItemStyle-HorizontalAlign="Center">
                                         <ItemTemplate>
                                             <asp:Label ID="Label1" runat="server" Text='<%# Eval("TaskTitle") %>'></asp:Label>
                                         </ItemTemplate>
                                         <EditItemTemplate>
                                             <asp:Label ID="Tsubedit" runat="server" Height="21px" ReadOnly="True" 
                                                 Text='<%# Bind("TaskTitle") %>' Width="98px"></asp:Label>
                                         </EditItemTemplate>
                                         <ItemStyle HorizontalAlign="Left" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="Left">
                                         <ItemTemplate>
                                             <asp:Label ID="Label2" runat="server" Text='<%# Eval("TDescription") %>'></asp:Label>
                                         </ItemTemplate>
                                         <%-- <EditItemTemplate>
                    <asp:TextBox ID="Tdesedit" runat="server" Text='<%# Bind("TDescription") %>' 
                            Width="187px"></asp:TextBox>
                    </EditItemTemplate>--%>
                                         <ItemStyle HorizontalAlign="Left" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="D.O.A." ItemStyle-HorizontalAlign="Center">
                                         <ItemTemplate>
                                             <asp:Label ID="Label3" runat="server" Text='<%# Eval("DOA") %>'></asp:Label>
                                         </ItemTemplate>
                                         <%--<EditItemTemplate>
                    <asp:TextBox ID="DOAedit" runat="server" Text='<%# Bind("DOA") %>' Width="74px"></asp:TextBox>
                    </EditItemTemplate>--%>
                                         <ItemStyle HorizontalAlign="Left" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Priority" ItemStyle-HorizontalAlign="Center">
                                         <ItemTemplate>
                                             <asp:Label ID="Label5" runat="server" Text='<%# Eval("Priority") %>'></asp:Label>
                                         </ItemTemplate>
                                         <%--<EditItemTemplate>
                    <asp:DropDownList ID="prioredit" runat="server" DataTextField="Prior" 
                            DataValueField="Prior" Width="60px" >
                            <asp:ListItem>Select</asp:ListItem>
                            <asp:ListItem>Low</asp:ListItem>
                            <asp:ListItem>Medium</asp:ListItem>
                            <asp:ListItem>High</asp:ListItem>
                    </asp:DropDownList>
                    
                    </EditItemTemplate>--%>
                                         <ItemStyle HorizontalAlign="Left" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                         <ItemTemplate>
                                             <asp:Label ID="Label6" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                         </ItemTemplate>
                                         <EditItemTemplate>
                                             <asp:Label ID="lblStatedit" Visible="false" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                             <asp:DropDownList ID="statedit" runat="server" DataTextField="Stat" 
                                                 DataValueField="Stat" Width="74px">
                                                 
                                             </asp:DropDownList>
                                         </EditItemTemplate>
                                         <ItemStyle HorizontalAlign="Left" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="D.O.C." ItemStyle-HorizontalAlign="Center">
                                         <ItemTemplate>
                                             <asp:Label ID="Label7" runat="server" Text='<%# Eval("DOC") %>'></asp:Label>
                                         </ItemTemplate>
                                         <%--    <EditItemTemplate>
                    <asp:TextBox ID="DOCedit" runat="server" Text='<%# Bind("DOC") %>' Width="74px"></asp:TextBox>
                    </EditItemTemplate>--%>
                                         <ItemStyle HorizontalAlign="Left" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Remarks(Optional)" 
                                         ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50">
                                         <ItemTemplate>
                                             <asp:Label ID="Label8" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
                                         </ItemTemplate>
                                         <EditItemTemplate>
                                             <asp:TextBox ID="Remedit" runat="server" Height="22px" 
                                                 Text='<%# Bind("Remarks") %>' Width="166px"></asp:TextBox>
                                         </EditItemTemplate>
                                         <ControlStyle Width="125px" />
                                         <ItemStyle HorizontalAlign="Left" />
                                     </asp:TemplateField>
                                     <asp:TemplateField ControlStyle-BackColor="Aqua" HeaderText="Permission" 
                                         ItemStyle-HorizontalAlign="Center">
                                         <ItemTemplate>
                                             <asp:Label ID="Label9" runat="server" Text='<%# Eval("Permission") %>'></asp:Label>
                                         </ItemTemplate>
                                         <%--<EditItemTemplate>
                    <asp:TextBox ID="DOAedit" runat="server" Text='<%# Bind("DOA") %>' Width="74px"></asp:TextBox>
                    </EditItemTemplate>--%>
                                         <ControlStyle BackColor="Aqua" />
                                         <ItemStyle HorizontalAlign="Left" />
                                     </asp:TemplateField>
                                     <asp:TemplateField ControlStyle-BackColor="Aqua" HeaderText="Comments" 
                                         ItemStyle-HorizontalAlign="Center">
                                         <ItemTemplate>
                                             <asp:Label ID="Label10" runat="server" Text='<%# Eval("Comments") %>'></asp:Label>
                                         </ItemTemplate>
                                         <%--<EditItemTemplate>
                    <asp:TextBox ID="DOAedit" runat="server" Text='<%# Bind("DOA") %>' Width="74px"></asp:TextBox>
                    </EditItemTemplate>--%>
                                         <ControlStyle BackColor="Aqua" />
                                         <ItemStyle HorizontalAlign="Left" />
                                     </asp:TemplateField>
                                     <%--<asp:TemplateField HeaderText="Rating" ItemStyle-HorizontalAlign="Center">
                                         <ItemTemplate>
                                             <asp:Rating ID="Rating1" runat="server" AutoPostBack="false" 
                                                 CssClass="rateStar" 
                                                 CurrentRating='<%# String.IsNullOrEmpty(Eval("rating").ToString())?0:Eval("rating") %>' 
                                                 Direction="LeftToRight" EmptyStarCssClass="EmptyStar" 
                                                 FilledStarCssClass="FillStar" Height="16px" MaxRating="10" 
                                                 StarCssClass="rateItem" WaitingStarCssClass="SaveStar" Width="142px">
                                             </asp:Rating>
                                         </ItemTemplate>
                                         <ItemStyle HorizontalAlign="Center" />
                                     </asp:TemplateField>--%>
                                 <%--    <asp:CommandField ShowEditButton="True" />--%>
                                     <asp:CommandField ItemStyle-Wrap="false" ButtonType="Image" EditImageUrl="~/Images/edit_icon.png" CancelImageUrl="~/Images/delete_icon.jpg" UpdateImageUrl="~/Images/save_icon.jpg" ShowEditButton="True" />
                                 </Columns>
                                 <PagerStyle HorizontalAlign="Center" />
                                 <SelectedRowStyle Font-Bold="True" />
                                 <HeaderStyle Font-Bold="True" />
                                 <EditRowStyle />
                                 <AlternatingRowStyle />
                                 <EmptyDataTemplate> 
                                     <asp:Label ID="lblempty" runat="server" Text="No Records">
            </asp:Label>
                                 </EmptyDataTemplate>
                             </asp:GridView>
                                 </div>
                        </td>
                    </tr>
                         <tr valign="top">
                             <td >
                                 &nbsp;</td>
                         </tr>
                  </table>
                  </div>
                        
                            
                     <table style="width: 100%">
                       <tr valign="top">
                         <td>
                        
                             <asp:GridView ID="grd_view1" runat="server" AllowSorting="True" 
                                 AutoGenerateColumns="False"
                                 class="table table-striped table-bordered table-hover" 
                                 CssClass="table table-striped table-bordered table-hover" Font-Names="Calibri" 
                                  GridLines="None" 
                                 onrowcancelingedit="grd_view_RowCancelingEdit" 
                                 onrowediting="grd_view_RowEditing" onrowupdating="grd_view_RowUpdating" OnRowDataBound="grd_view_RowDataBound">
                                 <FooterStyle Font-Bold="True" />
                                 <RowStyle />
                                 <Columns>
                                     <asp:TemplateField visible="false">
                                             <HeaderTemplate>
                                                 task id
                                             </HeaderTemplate>
                                             <ItemTemplate>
                                                 <asp:Label ID="clbltaskid" runat="server" Text='<%# Eval("TaskID") %>'></asp:Label>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Task Title" ItemStyle-HorizontalAlign="Center">
                                         <ItemTemplate>
                                             <asp:Label ID="cLabel1" runat="server" Text='<%# Eval("TaskTitle") %>'></asp:Label>
                                         </ItemTemplate>
                                         
                                         <ItemStyle HorizontalAlign="Left" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="Left">
                                         <ItemTemplate>
                                             <asp:Label ID="cLabel2" runat="server" Text='<%# Eval("TDescription") %>'></asp:Label>
                                         </ItemTemplate>
                                         
                                         <ItemStyle HorizontalAlign="Left" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="D.O.A." ItemStyle-HorizontalAlign="Center">
                                         <ItemTemplate>
                                             <asp:Label ID="cLabel3" runat="server" Text='<%# Eval("DOA") %>'></asp:Label>
                                         </ItemTemplate>
                                         
                                         <ItemStyle HorizontalAlign="Left" />
                                     </asp:TemplateField>
                                     
                                     <asp:TemplateField HeaderText="D.O.C." ItemStyle-HorizontalAlign="Center">
                                         <ItemTemplate>
                                             <asp:Label ID="cLabel7" runat="server" Text='<%# Eval("DOC") %>'></asp:Label>
                                         </ItemTemplate>
                                         
                                         <ItemStyle HorizontalAlign="Left" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Remarks(Optional)" 
                                         ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50">
                                         <ItemTemplate>
                                             <asp:Label ID="cLabel8" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
                                         </ItemTemplate>
                                         
                                         <ControlStyle Width="125px" />
                                         <ItemStyle HorizontalAlign="Left" />
                                     </asp:TemplateField>
                                     
                                     <asp:TemplateField ControlStyle-BackColor="Aqua" HeaderText="Comments" 
                                         ItemStyle-HorizontalAlign="Center">
                                         <ItemTemplate>
                                             <asp:Label ID="cLabel10" runat="server" Text='<%# Eval("Comments") %>'></asp:Label>
                                         </ItemTemplate>
                                         
                                         <ControlStyle BackColor="Aqua" />
                                         <ItemStyle HorizontalAlign="Left" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Rating" ItemStyle-HorizontalAlign="Center">
                                         <ItemTemplate>
                                             <asp:Rating ID="Rating1" runat="server" AutoPostBack="false" 
                                                 CssClass="rateStar" 
                                                 CurrentRating='<%# String.IsNullOrEmpty(Eval("rating").ToString())?0:Eval("rating") %>' 
                                                 Direction="LeftToRight" EmptyStarCssClass="EmptyStar" 
                                                 FilledStarCssClass="FillStar" Height="16px" MaxRating="10" 
                                                 StarCssClass="rateItem" WaitingStarCssClass="SaveStar" Width="142px">
                                             </asp:Rating>
                                         </ItemTemplate>
                                         <ItemStyle HorizontalAlign="Center" />
                                     </asp:TemplateField>
                                 
                                 </Columns>
                                 <PagerStyle HorizontalAlign="Center" />
                                 <SelectedRowStyle Font-Bold="True" />
                                 <HeaderStyle Font-Bold="True" />
                                 <EditRowStyle />
                                 <AlternatingRowStyle />
                                 <EmptyDataTemplate> 
                                     <asp:Label ID="clblempty" runat="server" Text="No Records">
            </asp:Label>
                                 </EmptyDataTemplate>
                             </asp:GridView>
                        </td>
                    </tr>
                         <tr valign="top">
                             <td >
                                 &nbsp;</td>
                         </tr>
                  </table>
                  </>
                  </ContentTemplate>

                                </asp:UpdatePanel>

</asp:Content>
