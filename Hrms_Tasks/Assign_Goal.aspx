<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Assign_Goal.aspx.cs" Inherits="Hrms_Tasks_Assign_Goal" MasterPageFile="~/HRMS.master" %>
 
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <link href="../Css/sample.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript">
  
        

        function validate() {
            var r = confirm("Are you sure you want to delete this record?");
            if (r == true) {
                return true;
            }
            else {
                return false;
            }
        }
        function fn_len(event, id) {
            var value, len;
            value = document.getElementById(id).value;
            len = value.length;

            if (len > 20) {
                document.getElementById(id).value = value;
                alert("maxlimit reached");

            }
        }
        function fn_date(event, txtid) {
            var len;
            var txtvalue;
            var bool_obj;
            var i;

            txtvalue = document.getElementById(txtid).value;
            txtlen = txtvalue.length;

            if (event.keyCode != 8 && event.keyCode != 46 && event.keyCode != 35 && event.keyCode != 36 && event.keyCode != 37 && event.keyCode != 38 && event.keyCode != 39 && event.keyCode != 40) {
                if (txtlen != 0) {
                    bool_obj = true;

                    if (bool_obj == true) {
                        if (txtlen == 2 || txtlen == 5) {
                            document.getElementById(txtid).value = txtvalue + "/";
                        }
                        else {
                            document.getElementById(txtid).value = txtvalue;
                        }
                    }
                    else {
                        document.getElementById(txtid).value = txtvalue.substring(0, txtlen - 1);
                    }
                }
            }
        }
        
    </script>
    <div ><h3 class="page-header">Goal Assigning</h3></div>
      
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

            <table style="width:100%">
                <tr>
                <td>
                   <div id="div1" runat="server" style="overflow: scroll ; width:1000px;">
                      <asp:GridView ID="GridView1" ShowFooter="true" runat="server" AllowSorting="True" AutoGenerateColumns="False"  
                          class="table table-striped table-bordered table-hover" 
                          GridLines="None"  HorizontalAlign="Center" OnRowCommand="GridView1_RowCommand" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">
                                    <HeaderStyle Height="50px" />
                                     <RowStyle Height="10px" />
                                     <Columns>
                                         <asp:TemplateField>
                                             <HeaderTemplate>
                                                 S.No.
                                             </HeaderTemplate>
                                             <ItemTemplate>
                                                 <asp:Label ID="lblSRNO" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Reference ID">
                                             <ItemTemplate>
                                                 <asp:Label ID="Lblrefno" runat="server" Text='<%# Eval("Refer_id") %>'></asp:Label>
                                             </ItemTemplate>
                                              <EditItemTemplate>
                                                 <asp:Label ID="refnoedit" runat="server" Text='<%# Bind("Refer_id") %>'></asp:Label>
                                             </EditItemTemplate>
                                             <FooterTemplate>
                                                  <asp:TextBox ID="txtrefno" MaxLength="10" CssClass="form-control" runat="server"></asp:TextBox>
                                             </FooterTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Assigned_To">
                                             
                                             <ItemTemplate>
                                                 <asp:Label ID="Lblename" runat="server" Text='<%# Eval("Assigned_to") %>'></asp:Label>
                                             </ItemTemplate>
                                              <EditItemTemplate>
                                                 <asp:Label ID="enameedit" runat="server" Text='<%# Bind("Assigned_to") %>'></asp:Label>
                                             </EditItemTemplate>
                                             <FooterTemplate>
                                                 <asp:DropDownList ID="ddename" CssClass="form-control" runat="server" AutoPostBack="true" DataTextField="Goal_Name" DataValueField="Goal_Name">
                                                      <asp:ListItem>Select</asp:ListItem>
                                               </asp:DropDownList>
                                             </FooterTemplate>
                                             
                                         </asp:TemplateField>
                                          
                                         <asp:TemplateField HeaderText="Goal Type" ItemStyle-HorizontalAlign="Center">
                                             
                                             <ItemTemplate>
                                                 <asp:Label ID="Lblgtype" runat="server" Text='<%# Eval("Goal_type") %>'></asp:Label>
                                             </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                              <EditItemTemplate>
                                                        <asp:Label ID="gtypeedit" runat="server" Text='<%# Eval("Goal_type") %>'></asp:Label>
                                             </EditItemTemplate>
                                             <FooterTemplate>
                                                  <asp:DropDownList ID="ddgtype" CssClass="form-control" runat="server" DataTextField="Goal_type" DataValueField="Goal_type" AutoPostBack="True" OnSelectedIndexChanged="ddgtype_SelectedIndexChanged">
                                                      <asp:ListItem>Select</asp:ListItem>
                                                      <asp:ListItem>General</asp:ListItem>
                                                      <asp:ListItem>Performance</asp:ListItem>
                                                  </asp:DropDownList>
                                             </FooterTemplate>
                                          </asp:TemplateField>
                                        
                                         
                                         <asp:TemplateField HeaderText="Goal Name" ItemStyle-HorizontalAlign="Center">
                                             <ItemTemplate>
                                                 <asp:Label ID="Lblgname" runat="server" Text='<%# Eval("Goal_name") %>'></asp:Label>
                                             </ItemTemplate>
                                             <EditItemTemplate>
                                                  <asp:Label ID="Lblgname" runat="server" Text='<%# Bind("Goal_name") %>'></asp:Label>
                                             </EditItemTemplate>
                                             <FooterTemplate>
                                                  <asp:DropDownList ID="ddgname" CssClass="form-control" runat="server" AutoPostBack="true" DataTextField="Goal_Name" DataValueField="Goal_Name">
                                                      <asp:ListItem>Select</asp:ListItem>
                                                  </asp:DropDownList>
                                             </FooterTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Start Date">
                                             <ItemTemplate>
                                                 <asp:Label ID="lblsdate" runat="server" Text='<%# Eval("start_date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                             </ItemTemplate>
                                             <EditItemTemplate>
                                                 <asp:Label ID="sdateedit" runat="server" Width="100px" Text='<%# Bind("start_date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                
                                             </EditItemTemplate>
                                             <FooterTemplate>
                                                 <asp:TextBox ID="txtsdate" runat="server" MaxLength="10" Width="100px"
                                                     onkeyup="fn_date(event,this.id);" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtsdate_TextChanged"></asp:TextBox>
                                                 <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                                                     TargetControlID="txtsdate" TodaysDateFormat="d MMMM, yyyy" />
                                             </FooterTemplate>
                                             <HeaderStyle HorizontalAlign="Left" />
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Status">
                                             
                                             <%--<HeaderTemplate>
                                                 Status
                                                 <asp:DropDownList ID="headstatus" runat="server" CssClass="form-control">
                                                     <asp:ListItem>Select</asp:ListItem>
                                                     <asp:ListItem>New</asp:ListItem>
                                                     <asp:ListItem>On-process</asp:ListItem>
                                                     <asp:ListItem>Completed</asp:ListItem>
                                                     <asp:ListItem>Re-open</asp:ListItem>
                                                 </asp:DropDownList>
                                             </HeaderTemplate>--%>
                                             <ItemTemplate>
                                                 
                                                 <asp:Label ID="lblstat" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                             </ItemTemplate>
                                           <EditItemTemplate>
                                               <asp:Label ID="lblstatedit" Visible="false" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                 <asp:DropDownList ID="statedit" runat="server" DataTextField="Stat" 
                                                     DataValueField="Stat" CssClass="form-control">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                     <asp:ListItem>New</asp:ListItem>
                                                     <asp:ListItem>Hold</asp:ListItem>
                                                     
                                                  </asp:DropDownList>                                                                                                                                              
                                             </EditItemTemplate>
                                             <FooterTemplate>
                                                 <asp:DropDownList ID="ddstatus" runat="server" CssClass="form-control">
                                                     <asp:ListItem>Select</asp:ListItem>
                                                     <asp:ListItem>New</asp:ListItem>
                                                     
                                                     
                                                 </asp:DropDownList>
                                             </FooterTemplate>
                                             <HeaderStyle HorizontalAlign="Left" />
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Completion Date">
                                             <ItemTemplate>
                                                 <asp:Label ID="lblcdate" runat="server" Text='<%# Convert.ToString(Eval("completion_date")).Equals("01/01/1900 00:00:00")?"": Eval("completion_date","{0:dd/MM/yyyy}")%>'></asp:Label>
                                             </ItemTemplate>
                                             <EditItemTemplate>
                                                 <asp:Label ID="cdateedit" runat="server" Text='<%# Convert.ToString(Eval("completion_date")).Equals("01/01/1900 00:00:00")?"": Eval("completion_date","{0:dd/MM/yyyy}")%>'></asp:Label>
                                             </EditItemTemplate>
                                             <FooterTemplate>
                                                 <asp:TextBox ID="txtcdate" runat="server" MaxLength="10" Width="100px"
                                                     onkeyup="fn_date(event,this.id);" CssClass="form-control" AutoPostBack="True"></asp:TextBox>
                                                 <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" 
                                                     TargetControlID="txtcdate" TodaysDateFormat="d MMMM, yyyy" />
                                             </FooterTemplate>
                                             <HeaderStyle HorizontalAlign="Left" />
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Comments">
                                             <ItemTemplate>
                                                 <asp:Label ID="Lblcmnt" runat="server" Text='<%# Eval("comments") %>'></asp:Label>
                                             </ItemTemplate>
                                              <EditItemTemplate>
                                                 <asp:TextBox ID="cmntedit" runat="server" Text='<%# Bind("comments") %>'></asp:TextBox>
                                             </EditItemTemplate>
                                             <FooterTemplate>
                                                  <asp:TextBox ID="txtcmnt"  CssClass="form-control" runat="server"></asp:TextBox>
                                             </FooterTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField>
                                             <ItemTemplate>
                                                <asp:ImageButton ID="upload" CommandName="Uploaddoc" 
CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server" ImageUrl="~/Images/upload.png" OnClick="upload_Click" />
                                                 
                                                 <asp:ImageButton ID="download" runat="server" ImageUrl="~/Images/download.png" OnClick="DownloadFiles"/>
                                             </ItemTemplate>
                                             <FooterTemplate>
                                                 <asp:Button ID="Button1" runat="server" CommandName="add" Font-Bold="True" 
                                                     CssClass="btn btn-success"
                                                     Text="ADD" />
                                             </FooterTemplate>
                                         </asp:TemplateField>
                                        <%--<asp:CommandField ItemStyle-Wrap="false" ButtonType="Image" EditImageUrl="~/Images/edit_icon.png" CancelImageUrl="~/Images/delete_icon.jpg" UpdateImageUrl="~/Images/save_icon.jpg" ShowEditButton="True" ShowDeleteButton="true" DeleteImageUrl="~/Images/delete_icon.jpg" > 
                                          <ItemStyle Wrap="False"></ItemStyle>
                                         </asp:CommandField>--%>
                                         <asp:TemplateField>
                                             <ItemTemplate>
                                                 <asp:ImageButton ID="edit" CommandArgument='<%# Container.DataItemIndex%>'  CommandName="Edit" runat="server" ImageUrl="~/Images/edit_icon.png"></asp:ImageButton>
                                                 <asp:ImageButton ID="delete" CommandArgument='<%# Container.DataItemIndex%>'  CommandName="Delete" runat="server" ImageUrl="~/Images/delete_icon.jpg"></asp:ImageButton>
                                                 <asp:ImageButton ID="update" CommandArgument='<%# Container.DataItemIndex%>'  CommandName="Update" runat="server" ImageUrl="~/Images/save_icon.jpg" OnClick="update_Click"></asp:ImageButton>
                                                 <asp:ImageButton ID="cancel" CommandArgument='<%# Container.DataItemIndex%>'  CommandName="Cancel" runat="server" ImageUrl="~/Images/delete_icon.jpg"></asp:ImageButton>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                     </Columns>
                                     <PagerStyle HorizontalAlign="Center" />
                                   
                                     <SelectedRowStyle Font-Bold="True" />
                                     <HeaderStyle Font-Bold="True" Font-Names="Calibri" />
                                     <EditRowStyle />
                                  
                                 </asp:GridView>
                        <asp:Label ID="lbl_error" runat="server" Font-Bold="True" Font-Names="Calibri" 
                                     Font-Size="Small" ForeColor="#CC0000"></asp:Label>
          </div>
        </td>
     </tr>
                 </table>  
              <asp:LinkButton Text="" ID="dummy" runat="server" />
                    <asp:ModalPopupExtender  ID="modalhistory" runat="server" TargetControlID="dummy" PopupControlID="Panel1" CancelControlID="btn1" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>
                        <asp:Panel ID="Panel1" runat="server" Width="30%" Height="100px" BackColor="white" style="border:3px solid #0DA9D0 ">
                             
                        
                            <div>
                            <button id="btn1" runat="server" style="display:inline-block; margin-right:0; margin-left:100%; float:right; font-weight:bolder;border-radius:50%;background-color:gray;color:black;border:none">
                                      x </button>
                                </div>
                            <%--<asp:FileUpload ID="upload1" runat="server" style="margin-left:30px;"  />--%>
                             <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="true" style="margin-left:30px;" />
                             <asp:Button  ID="btnUpload" Text="Upload" runat="server" OnClick="UploadMultipleFiles" accept="image/gif, image/jpeg" />
                               <asp:Label ID="lblSuccess" runat="server" ForeColor="Green" />
                            <%--<div style="display: block; float: none;">
                            <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button" />
                        </div>
                            --%>
                        </asp:Panel> 
                                </div>
              </ContentTemplate>
                <Triggers>
                   <asp:PostBackTrigger ControlID="btnUpload" />
              </Triggers>
         </asp:UpdatePanel>
 
</asp:Content>
