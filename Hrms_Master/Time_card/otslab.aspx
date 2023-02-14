 <%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="otslab.aspx.cs" Inherits="Hrms_Master_Default"
    Title="Welcome to HRMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/css">
    .scrollingControlContainer
    {
        overflow-x: hidden;
        overflow-y: scroll;
    }
    </script>
    <script type="text/javascript" language="javascript" src="../../Scripts/Datavalid.js"></script>
    <script language="javascript" type="text/javascript">
  
   function show_message()
    {
        alert("Course Name Already Exist");
    }
    
    function show_Error()
    {
        alert("Enter Course Name");
    }
  
    function fnSave()
    {   
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$CourseName.value == "")
        {
            alert("Enter Course Name");
            aspnetForm.ctl00$ContentPlaceHolder1$CourseName.focus();
            return false;
        }                        
        else
        { 
            return true;  
        }
    } 
    function coudnt_del()
    {
        alert("Cannot delete. It is already assigned to someone");
    }  
    
     function fn_date(event,txtid)
     {
       var len;
       var txtvalue; 
       var bool_obj; 
       var i;    
          
       txtvalue= document.getElementById(txtid).value;
       txtlen=txtvalue.length;  
       
        if(event.keyCode!=8 && event.keyCode!=46 && event.keyCode!=35 && event.keyCode!=36 && event.keyCode!=37 && event.keyCode!=38 && event.keyCode!=39 && event.keyCode!=40)     
        {    
           if(txtlen!=0)
            {               
             if(txtlen==2)
              {
              document.getElementById(txtid).value=txtvalue+":";
              }
              else
              {
              document.getElementById(txtid).value=txtvalue;
              }               
            } 
        }                                 
     }
     
    </script>

    <div ><h2 class="page-header" >Over Time Slab Details</h2></div>
    
               <asp:DropDownList ID="ddl_branch" runat="server"  CssClass="form-control"
                    onselectedindexchanged="ddl_branch_SelectedIndexChanged" 
                    AutoPostBack="True">
                </asp:DropDownList>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <table ID="branch_time" runat="server" style="width: 100%">
        <tr>
            <td align="center">
                <asp:Panel HorizontalAlign="Center" ID="Panel1" runat="server" Width="100%" 
                    Height="100%">
                    <br />
                    <br />
                    <%--<asp:Label ID="Label1" runat="server" Text="This page access for Branch Users only" Font-Bold="true" Font-Size="Larger" ></asp:Label>--%>
       <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
            AutoGenerateColumns="False"  ShowFooter="True"
            onrowcommand="GridView1_RowCommand" 
             onrowdeleting="GridView1_RowDeleting"  CssClass="table table-striped table-bordered table-hover"
            onrowdatabound="GridView1_RowDataBound" onrowediting="GridView1_RowEditing" 
    HorizontalAlign="Center" 
            onrowupdating="GridView1_RowUpdating" 
            OnRowCancelingEdit="GridView1_RowCancelingEdit" >
                       
                        <Columns>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="ID"  
                    HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_id" runat="server" Text='<%# Eval("slab_id") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="txt_id_edit" Enabled="false" runat="server" 
                            Text='<%# Bind("slab_id") %>'></asp:Label>
                                </EditItemTemplate>
                                <FooterStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Category"  
                    HeaderStyle-HorizontalAlign="Left">
                    <HeaderTemplate>
                    <asp:Label ID="lblcat" runat="server"  Text='Category'></asp:Label>
                    <asp:DropDownList ID="ddl_category" runat="server" CssClass="form-control" AutoPostBack="True"  onselectedindexchanged="ddl_category_SelectedIndexChanged1">
                    </asp:DropDownList>
                    </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_catid" runat="server"  Text='<%# Eval("pn_category") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="txt_catid_edit" Enabled="false" runat="server" 
                            Text='<%# Bind("pn_category") %>'></asp:Label>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddl_catid" runat="server" CssClass="form-control"
                                        AutoPostBack="True"></asp:DropDownList>
                                 </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="OT From Duration" 
                    HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_otfrom" runat="server" 
                            Text='<%# Eval("ot_from","{0:HH:mm}") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_otfrom_edit" runat="server" CssClass="form-control"
                            Text='<%# Bind("ot_from","{0:HH:mm}") %>' Width="100px"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_otfrom" runat="server" onkeydown="AllowOnlyNumeric1(event);"
                            onkeyup="fn_date(event,this.id);" MaxLength="5" CssClass="form-control"></asp:TextBox>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="OT To Duration"  
                    HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_otto" runat="server" 
                            Text='<%# Eval("ot_to","{0:HH:mm}") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_otto_edit" runat="server" CssClass="form-control"
                            Text='<%# Bind("ot_to","{0:HH:mm}") %>' Width="100px"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_otto" runat="server" CssClass="form-control" onkeydown="AllowOnlyNumeric1(event);" onkeyup="fn_date(event,this.id);"  MaxLength="5" ></asp:TextBox>
                                </FooterTemplate>
                               
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                               
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="OT Slab Duration"  
                    HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_otslab" runat="server" 
                            Text='<%# Eval("ot_slab","{0:HH:mm}") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_otslab_edit" runat="server" CssClass="form-control"
                            Text='<%# Bind("ot_slab","{0:HH:mm}") %>' Width="100px"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                <div style="float:left">
                                    <asp:TextBox ID="txt_otslab" runat="server" onkeydown="AllowOnlyNumeric1(event);"
                            onkeyup="fn_date(event,this.id);" MaxLength="5" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                           </div>

                                <div style="float:right">
                            <asp:LinkButton ID="Button1" CommandName="add" runat="server" 
                             onclick="Button1_Click" CssClass="btn btn-success " ><i class="glyphicon glyphicon-check"></i>Save</asp:LinkButton>
                            </div>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            

                          <asp:CommandField HeaderText="Edit"  ItemStyle-Width="25px" EditImageUrl="~/Images/edit_icon.png" ButtonType="Image" UpdateImageUrl="~/Images/save_icon.jpg" CancelImageUrl="~/Images/cancel.png" ShowEditButton="True" />
                          <asp:CommandField  HeaderText="Delete" ShowDeleteButton="True" ItemStyle-Width="25px" ButtonType="Image" DeleteImageUrl="~/Images/delete_icon.jpg" />
                        </Columns>
                        
                        <EmptyDataTemplate>
                            <asp:Label ID="lblempty" Text="No Records" runat="server"></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    
                </asp:Panel>
            </td>
        </tr>
        </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Content>







