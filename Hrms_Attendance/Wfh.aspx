<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="Wfh.aspx.cs" Inherits="Hrms_Company_Default" Title="Welcome to HRMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
 
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">  
    <link href="../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
<link href="../Css/jquery-ui.css"
    rel="stylesheet" type="text/css" />
    <script  type="text/css">
    .HeaderFreez
    {
       position:relative ;
       top:expression(this.offsetParent.scrollTop);
       z-index: 10;
    }
    </script>

        <script type="text/javascript">
            function checkDate(sender, args) {
                if (sender._selectedDate < new Date()) {
                    alert("Past day selections are not allowed!");
                    sender._selectedDate = new Date();
                    // set the date back to the current date
                    sender._textbox.set_Value(sender._selectedDate.format(sender._format))
                }
            }
    </script>
<script language="javascript" type="text/javascript">
// This Script is used to maintain Grid Scroll on Partial Postback
var scrollTop;
//Register Begin Request and End Request 
Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
//Get The Div Scroll Position
function BeginRequestHandler(sender, args) 
{
var m = document.getElementById('divGrid');
scrollTop=m.scrollTop;
}
//Set The Div Scroll Position
function EndRequestHandler(sender, args)
{
var m = document.getElementById('divGrid');
m.scrollTop = scrollTop;
} 
</script>

<script type="text/javascript">
 $(document).ready(function () {
    function ShowModal() {
        $("#dialog").dialog({ title: "View Details", buttons: { Ok: function () { $(this).dialog('close'); } }, modal: true });
        return false;
    };
});
</script>

  <script type="text/javascript">
      function fn_date(event, txtid) {
          var len;
          var txtvalue;
          var bool_obj;
          var i;

          txtvalue = document.getElementById(txtid).value;
          txtlen = txtvalue.length;

          if (event.keyCode != 8 && event.keyCode != 46 && event.keyCode != 35 && event.keyCode != 36 && event.keyCode != 37 && event.keyCode != 38 && event.keyCode != 39 && event.keyCode != 40) {
              if (txtlen != 0) {
                  bool_obj = fn_validate(txtlen, txtvalue);

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

      function fn_validate(len, tval) {
          var str;
          switch (len) {

              case 1: if (tval <= 3) {
                      return true;
                  }
                  else {
                      return false;
                  }
                  break;

              case 2:

                  if (tval <= 31 && tval > 0) {
                      return true;
                  }
                  else {
                      return false;
                  }
                  break;


              case 3: str = tval.charAt(2);

                  if (str == "/") {
                      return true;
                  }
                  else {
                      return false;
                  }
                  break;

              case 4: str = tval.charAt(3);

                  if (str <= 1) {
                      return true;
                  }
                  else {
                      return false;
                  }
                  break;

              case 5: str = tval.substring(3, 5);

                  if (str <= 12 && str > 0) {
                      return true;
                  }
                  else {
                      return false;
                  }
                  break;

              case 6: str = tval.charAt(5);

                  if (str == "/") {
                      return true;
                  }
                  else {
                      return false;
                  }
                  break;

              case 7: str = tval.charAt(6);

                  if (str <= 9 && str > 0) {
                      return true;
                  }
                  else {
                      return false;
                  }
                  break;

              case 8: str = tval.substring(6, 8);

                  if (str >= 18) {
                      return true;
                  }
                  else {
                      return false;
                  }
                  break;

              case 9: str = tval.charAt(8);

                  if (str <= 9) {
                      return true;
                  }
                  else {
                      return false;
                  }
                  break;

              case 10: str = tval.charAt(9);

                  if (str <= 9) {
                      return true;
                  }
                  else {
                      return false;
                  }
                  break;

              default: return false;
                  break;
          }
      }
  </script>
          <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
      <div>   
          <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                            <div class="divWaiting">
                            
                            <asp:Image ID="imgWait" runat="server" ImageAlign="Middle" 
                                    ImageUrl="~/Images/loading2.gif" Height="100px" Width="100px" />
                                <%--<img src="../loading.gif" alt="Loading" style="position:relative;" />--%>
                            </div>
                            </ProgressTemplate>
                            </asp:UpdateProgress>
                            
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>              
    <h2 class="page-header">Work From Home Request</h2>
    <div style="width: 70%">
        <table cellpadding="1%" cellspacing="1%" width="100%" class="table table-striped table-bordered table-hover">
                        
                        <tr>
                            <td>
                                Employee Code</td>
                            <td >
                                <asp:Label ID="lblEmpCode" runat="server"></asp:Label>
                            </td>
                            <td >
                                Employee Name</td>
                            <td >
                                <asp:Label ID="lblEmpName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                From Date</td>
                            <td>
                            <div style=" width:120px; float:left;">
                                                <asp:TextBox ID="txt_fromdate" runat="server" class="form-control" 
                                                    MaxLength="10" onkeydown="return false" Width="120px"></asp:TextBox>
                                                  <asp:CalendarExtender ID="CalendarExtender4" runat="server"  Format="dd/MM/yyyy" TargetControlID="txt_fromdate">
                                                  </asp:CalendarExtender>
                                              </div>
                                               <div style=" width:25px; float:left;  margin-left:10px; margin-top:3px;">                                                
                                                    <asp:Image ID="Image1" runat="server" Text="" Width="25px" ImageUrl="~/Images/calendaricon.png" />
                                 </div>
                                
                            </td>
                            <td>
                                To Date</td>
                            <td>
                               <div style=" width:120px; float:left;">
                                                <asp:TextBox ID="txt_todate" runat="server" class="form-control"  onkeydown="return false" Width="120px" MaxLength="10"></asp:TextBox>
                                                  <asp:CalendarExtender ID="CalendarExtender5" runat="server"  Format="dd/MM/yyyy" TargetControlID="txt_todate">
                                                  </asp:CalendarExtender>
                                              </div>
                                               <div style=" width:25px; float:left;  margin-left:10px; margin-top:3px;">                                                
                                                    <asp:Image ID="Image2" runat="server" Text="" Width="25px" ImageUrl="~/Images/calendaricon.png" />
                                 </div>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Reason</td>
                            <td >
                                <asp:TextBox ID="txtreason" CssClass="form-control" runat="server" 
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                            <td >
                                <asp:Button ID="btnSave" runat="server" class="btn btn-success" Text="Save" 
                                    onclick="btnSave_Click" />
                            </td>
                            <td >
                                &nbsp;</td>
                        </tr>
                     </table>
    </div>
    <div id="divGrid" > 
    <asp:GridView ID="GridViewPermission" runat="server" 
            CssClass="table table-hover table-striped" GridLines="None"
            AutoGenerateColumns="false" AllowPaging="false"   ShowFooter="True" 
            HorizontalAlign="Center" OnRowEditing="GridViewPermission_RowEditing" 
            OnRowCancelingEdit="GridViewPermission_RowCancelingEdit" 
            OnRowDeleting="GridViewPermission_RowDeleting" 
            OnRowUpdating="GridViewPermission_RowUpdating" 
            onrowdatabound="GridViewPermission_RowDataBound"> 
            <HeaderStyle CssClass="HeaderFreez" />
            <Columns>
            <asp:TemplateField ItemStyle-HorizontalAlign="Left" Visible="false" HeaderText="Ref. No." 
                    HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>                                
                                 <asp:Label ID="lbl_PermissionID" runat="server" Visible="false" Text='<%# Eval("WfhID") %>'></asp:Label>                                
                                </ItemTemplate>                                 
                                <EditItemTemplate>
                                <asp:Label ID="lbl_PermissionID_edit" Visible="false" runat="server" Text='<%# Bind("WfhID") %>'></asp:Label>
                                 <%--<asp:TextBox id="lbl_PermissionID_edit" type="text" runat="server"  Width="150px" CssClass="form-control" Text='<%# Bind("PermissionID") %>'></asp:TextBox>--%>
                                </EditItemTemplate>

                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Left" Visible="false" HeaderText="Emp ID" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>                                
                                 <asp:Label ID="lbl_EmpID" runat="server" Visible="false" Text='<%# Eval("EmployeeID") %>'></asp:Label>                                
                                </ItemTemplate>                                 
                                <EditItemTemplate>
                                <asp:Label ID="lbl_EmpID_edit" Visible="false" runat="server" Text='<%# Bind("EmployeeID") %>'></asp:Label>
                                 <%--<asp:TextBox id="lbl_PermissionID_edit" type="text" runat="server"  Width="150px" CssClass="form-control" Text='<%# Bind("PermissionID") %>'></asp:TextBox>--%>
                                </EditItemTemplate>

                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Employee Code" 
                    HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_EmployeeCode" runat="server" Text='<%# Eval("EmployeeCode") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>                                
                                <asp:Label ID="lbl_EmployeeCodeedit" runat="server" Text='<%# Bind("EmployeeCode") %>'></asp:Label>
                                </EditItemTemplate>

                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField> 

                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Employee" 
                    HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_EmployeeName" runat="server" Text='<%# Eval("EmployeeName") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>                                
                                <asp:Label ID="lbl_EmployeeNameedit" runat="server" Text='<%# Bind("EmployeeName") %>'></asp:Label>
                                </EditItemTemplate>

                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>                              

                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="From Date" 
                    HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_fDate" runat="server" Text='<%# Eval("FromDate", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                             <asp:Label id="lblfDate_edit" runat="server" onkeyup="fn_date(event,this.id);" Text='<%# Bind("FromDate", "{0:dd/MM/yyyy}")%>'></asp:Label>
       
                                </EditItemTemplate>
                                
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="To Date" 
                    HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_tDate" runat="server" Text='<%# Eval("ToDate", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                             <asp:Label id="lbltDate_edit" runat="server"  Text='<%# Bind("ToDate", "{0:dd/MM/yyyy}")%>'></asp:Label>
       
                                </EditItemTemplate>

                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Reason" 
                    HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_reason" runat="server" Text='<%# Eval("Reason") %>'></asp:Label>
                                </ItemTemplate>

                                <EditItemTemplate>  
                            <asp:Label id="lblreason_edit" runat="server" Text='<%# Bind("Reason")%>'></asp:Label>
                                </EditItemTemplate>

                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>

                             <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Status" 
                    HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Status" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                 <%--<asp:TextBox ID="txt_Status_edit" runat="server" CssClass="form-control" Width="150px" Text='<%# Bind("Status") %>'></asp:TextBox> --%>                                                              
                                  <asp:DropDownList ID="ddl_Status_edit" runat="server" CssClass="form-control" Text='<%# Bind("Status") %>'>
                                    <asp:ListItem>NA</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                        <asp:ListItem>No</asp:ListItem>
                                    </asp:DropDownList> 
                                </EditItemTemplate>
     
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                             <asp:CommandField ShowDeleteButton="false" ButtonType="Image" ShowEditButton="true" EditImageUrl="~/Images/edit_icon.png" UpdateImageUrl="~/Images/save_icon.jpg" CancelImageUrl="~/Images/delete_icon.jpg" DeleteImageUrl="~/Images/delete_icon.jpg" />
            </Columns>             
            </asp:GridView>
            </div>

  <div>
    <h3 class="page-header">My Request</h3>

      <div id="div1" style="overflow: auto; height: 500px"> 
    <asp:GridView ID="grid_WFH" runat="server" 
            CssClass="table table-hover table-striped" GridLines="None"
            AutoGenerateColumns="false" AllowPaging="false"   ShowFooter="True" 
            HorizontalAlign="Center"> 
            <HeaderStyle CssClass="HeaderFreez" />
            <Columns>
            <asp:TemplateField ItemStyle-HorizontalAlign="Left" Visible="false" HeaderText="Ref. No." 
                    HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>                                
                                 <asp:Label ID="lbl_PermissionID" runat="server" Visible="false" Text='<%# Eval("WfhID") %>'></asp:Label>                                
                                </ItemTemplate>                                 
                                <EditItemTemplate>
                                <asp:Label ID="lbl_PermissionID_edit" Visible="false" runat="server" Text='<%# Bind("WfhID") %>'></asp:Label>
                                 <%--<asp:TextBox id="lbl_PermissionID_edit" type="text" runat="server"  Width="150px" CssClass="form-control" Text='<%# Bind("PermissionID") %>'></asp:TextBox>--%>
                                </EditItemTemplate>

                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Left" Visible="false" HeaderText="Emp ID" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>                                
                                 <asp:Label ID="lbl_EmpID" runat="server" Visible="false" Text='<%# Eval("EmployeeID") %>'></asp:Label>                                
                                </ItemTemplate>                                 
                                <EditItemTemplate>
                                <asp:Label ID="lbl_EmpID_edit" Visible="false" runat="server" Text='<%# Bind("EmployeeID") %>'></asp:Label>
                                 <%--<asp:TextBox id="lbl_PermissionID_edit" type="text" runat="server"  Width="150px" CssClass="form-control" Text='<%# Bind("PermissionID") %>'></asp:TextBox>--%>
                                </EditItemTemplate>

                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Employee Code" 
                    HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_EmployeeCode" runat="server" Text='<%# Eval("EmployeeCode") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>                                
                                <asp:Label ID="lbl_EmployeeCodeedit" runat="server" Text='<%# Bind("EmployeeCode") %>'></asp:Label>
                                </EditItemTemplate>

                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField> 

                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Employee" 
                    HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_EmployeeName" runat="server" Text='<%# Eval("EmployeeName") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>                                
                                <asp:Label ID="lbl_EmployeeNameedit" runat="server" Text='<%# Bind("EmployeeName") %>'></asp:Label>
                                </EditItemTemplate>

                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>                              

                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="From Date" 
                    HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_fDate" runat="server" Text='<%# Eval("FromDate", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                             <asp:Label id="lblfDate_edit" runat="server" onkeyup="fn_date(event,this.id);" Text='<%# Bind("FromDate", "{0:dd/MM/yyyy}")%>'></asp:Label>
       
                                </EditItemTemplate>
                                
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="To Date" 
                    HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_tDate" runat="server" Text='<%# Eval("ToDate", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                             <asp:Label id="lbltDate_edit" runat="server"  Text='<%# Bind("ToDate", "{0:dd/MM/yyyy}")%>'></asp:Label>
       
                                </EditItemTemplate>

                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Reason" 
                    HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_reason" runat="server" Text='<%# Eval("Reason") %>'></asp:Label>
                                </ItemTemplate>

                                <EditItemTemplate>  
                            <asp:Label id="lblreason_edit" runat="server" Text='<%# Bind("Reason")%>'></asp:Label>
                                </EditItemTemplate>

                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>

                             <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Status" 
                    HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Status" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                 <%--<asp:TextBox ID="txt_Status_edit" runat="server" CssClass="form-control" Width="150px" Text='<%# Bind("Status") %>'></asp:TextBox> --%>                                                              
                                  <asp:DropDownList ID="ddl_Status_edit" runat="server" CssClass="form-control" Text='<%# Bind("Status") %>'>
                                    <asp:ListItem>NA</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                        <asp:ListItem>No</asp:ListItem>
                                    </asp:DropDownList> 
                                </EditItemTemplate>
     
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                             <asp:CommandField ShowDeleteButton="false" ButtonType="Image" ShowEditButton="true" EditImageUrl="~/Images/edit_icon.png" UpdateImageUrl="~/Images/save_icon.jpg" CancelImageUrl="~/Images/delete_icon.jpg" DeleteImageUrl="~/Images/delete_icon.jpg" />
            </Columns>             
            </asp:GridView>
            </div>
    </div> 
              </ContentTemplate>
             </asp:UpdatePanel>
            </div>
</asp:Content>

