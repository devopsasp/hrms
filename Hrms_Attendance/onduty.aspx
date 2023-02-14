<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="onduty.aspx.cs" Inherits="Hrms_Company_Default" Title="Welcome to HRMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
 
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">  
    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>--%>
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
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>              
    <h2 class="page-header">Permission</h2>
    <div>
        <table class ="table table-striped table-bordered table-hover">
            <tr>
                <td>
                    From Date</td>
                <td width="210px">
                
                <div style=" width:150px; float:left;">
                <asp:TextBox ID="Txt_fdate" runat="server" onkeyup="fn_date(event,this.id);"
                    CssClass="form-control" Width="150px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender4" TargetControlID="Txt_fdate" 
                    runat="server" Format="dd/MM/yyyy" />
                    </div>
                    <div style=" width:25px; float:left;  margin-left:10px; margin-top:3px;">                                                
                    <asp:Image ID="Image1" runat="server" Text="" Width="25px" ImageUrl="~/Images/calendaricon.png" />
                    </div>
                </td>
                <td>
                    To Date</td>
                <td  width="210px">
                
                <div style=" width:150px; float:left;">
                <asp:TextBox ID="Txt_tdate" runat="server" onkeyup="fn_date(event,this.id);"
                    CssClass="form-control" Width="150px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender5" TargetControlID="Txt_tdate" 
                    runat="server" Format="dd/MM/yyyy" />
                    </div>
                    <div style=" width:25px; float:left;  margin-left:10px; margin-top:3px;">                                                
                    <asp:Image ID="Image2" runat="server" Text="" Width="25px" ImageUrl="~/Images/calendaricon.png" />
                    </div>
                </td>
                <td>
                <asp:Button  ID="btn_Details" runat="server" CssClass="btn btn-info" 
                        Text="View Details" onclick="btn_Details_Click"/>
                </td>
                <td>
                    <asp:Button ID="btn_month" runat="server" CssClass="btn btn-success" 
                        Text="Current Month Details" onclick="btn_month_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divGrid" style="overflow: auto; height: auto"> 

     <asp:GridView ID="GridViewPermission" runat="server" 
            CssClass="table table-hover table-striped" GridLines="None"
            AutoGenerateColumns="false" AllowPaging="false"   ShowFooter="True" 
            HorizontalAlign="Center" onrowcommand="GridViewPermission_RowCommand" 
            onrowediting="GridViewPermission_RowEditing" 
            onrowcancelingedit="GridViewPermission_RowCancelingEdit" 
            onrowdeleting="GridViewPermission_RowDeleting" 
            onrowupdating="GridViewPermission_RowUpdating" 
            onrowdatabound="GridViewPermission_RowDataBound"> 
            <HeaderStyle CssClass="HeaderFreez" />
            <Columns>
            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Ref. No." 
                    HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>                                
                                 <asp:Label ID="lbl_PermissionID" runat="server" Text='<%# Eval("PermissionID") %>'></asp:Label>                                
                                </ItemTemplate>                                 
                                <EditItemTemplate>
                                <asp:Label ID="lbl_PermissionID_edit" runat="server" Text='<%# Bind("PermissionID") %>'></asp:Label>
                                 <%--<asp:TextBox id="lbl_PermissionID_edit" type="text" runat="server"  Width="150px" CssClass="form-control" Text='<%# Bind("PermissionID") %>'></asp:TextBox>--%>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddl_Department" runat="server" CssClass="form-control" 
                                         onselectedindexchanged="ddl_Department_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>                                    
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Employee" 
                    HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_EmployeeCode" runat="server" Text='<%# Eval("EmployeeName") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>                                
                                <asp:Label ID="lbl_EmployeeCode" runat="server" Text='<%# Bind("EmployeeName") %>'></asp:Label>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlEmployee_Code" runat="server" CssClass="form-control" >
                                    </asp:DropDownList>                                    
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>                              

                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Date" 
                    HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Date" runat="server" Text='<%# Eval("Date", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                             <asp:TextBox id="txtDate_edit" runat="server" onkeyup="fn_date(event,this.id);" CssClass="form-control" Text='<%# Bind("Date", "{0:dd/MM/yyyy}")%>'></asp:TextBox>
        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate_edit" Format="dd/MM/yyyy">
        </asp:CalendarExtender> 
                                </EditItemTemplate>
                                <FooterTemplate>                                  
                                   
        <asp:TextBox id="txtDate" runat="server" onkeyup="fn_date(event,this.id);" CssClass="form-control"></asp:TextBox>
        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate" Format="dd/MM/yyyy" >
        </asp:CalendarExtender>                                                            
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Session" 
                    HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Session" runat="server" Text='<%# Eval("Session") %>'></asp:Label>
                                </ItemTemplate>

                                <EditItemTemplate>  
                             <asp:DropDownList ID="ddlSessio_edit" runat="server" CssClass="form-control" Text='<%# Bind("Session") %>'>
                                    <asp:ListItem>Select</asp:ListItem>
                                        <asp:ListItem>FN</asp:ListItem>
                                        <asp:ListItem>AN</asp:ListItem>
                                    </asp:DropDownList> 
                                </EditItemTemplate>

                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlSessio" runat="server" CssClass="form-control" >
                                    <asp:ListItem>Select</asp:ListItem>
                                        <asp:ListItem>FN</asp:ListItem>
                                        <asp:ListItem>AN</asp:ListItem>
                                    </asp:DropDownList>                                    
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Center" />
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
                                    <asp:ListItem>Select</asp:ListItem>
                                        <asp:ListItem>Y</asp:ListItem>
                                        <asp:ListItem>N</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <FooterTemplate>
                                <table width="100%" cellspacing="3">
                                <tr>
                                <td>
                               <%--<asp:TextBox ID="txt_Status" runat="server" CssClass="form-control" Width="150px"></asp:TextBox>--%>
                                 <asp:DropDownList ID="ddl_Status" runat="server" CssClass="form-control">
                                    <asp:ListItem>Select</asp:ListItem>
                                        <asp:ListItem>Y</asp:ListItem>
                                        <asp:ListItem>N</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td  align="Center">
                                    <asp:LinkButton ID="LinkbtnPermission" CommandName="add" runat="server" 
                              CssClass="btn btn-success "><i class="glyphicon glyphicon-check"></i>Save</asp:LinkButton>
                                   </td>                                
                                </tr>
                                </table>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                             <asp:CommandField  ShowDeleteButton="true" ButtonType="Image" EditImageUrl="~/Images/edit_icon.png" CancelImageUrl="~/Images/delete_icon.jpg" UpdateImageUrl="~/Images/save_icon.jpg" DeleteImageUrl="~/Images/delete_icon.jpg" ShowEditButton="True" />
            </Columns>             
            </asp:GridView>  
            
            </div>

  <div><h2 class="page-header"></h2></div> 
  <div><h3> &nbsp;</h3>
      
      <div style="float:right; ">
  <asp:DropDownList ID="ddl_branch" runat="server" class="form-control" AutoPostBack="True" onselectedindexchanged="ddl_branch_SelectedIndexChanged">
    </asp:DropDownList></div></div>
<table width="100%">
<tr>
<td colspan="2"  > 

<div  style="width: 100%; visibility:hidden">
                    <table cellpadding="1%" cellspacing="1%" width="100%" class="table table-striped table-bordered table-hover">
                        
                        <tr>
                            <td>
                                Department</td>
                            <td >
                                                <span style="color: #800000">
                                                <asp:DropDownList ID="ddl_department" runat="server" AutoPostBack="True" 
                                                    class="form-control" onselectedindexchanged="ddl_department_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                </span></td>
                            <td >
                                Employee</td>
                            <td >
                                                <span style="color: #800000">
                                                <asp:DropDownList ID="ddl_ename" runat="server" 
                                    class="form-control">
                                                </asp:DropDownList>
                                                </span>
                                            </td>
                        </tr>
                        <tr>
                            <td>
                                From Date</td>
                            <td>
                                <asp:TextBox ID="txt_ondutydat" runat="server" CssClass="form-control" onkeyup="fn_date(event,this.id);" MaxLength="10" 
                        ontextchanged="txt_ondutydat_TextChanged"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txt_ondutydat" Format="dd/MM/yyyy" TodaysDateFormat="d MMMM, yyyy" runat="server">
                    </asp:CalendarExtender>
                        </td>
                            <td>
                                Status</td>
                            <td>
                                <asp:DropDownList ID="ddl_onduty_fstat" CssClass="form-control" runat="server" BackColor="White" ForeColor="Black" AutoPostBack="True" 
                      onselectedindexchanged="ddl_onduty_fstat_SelectedIndexChanged" Width="75%" >
                        <asp:ListItem>Select</asp:ListItem>
                        <asp:ListItem>First Half</asp:ListItem>
                        <asp:ListItem>Second Half</asp:ListItem>
                        <asp:ListItem>Full Day</asp:ListItem>
                    </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>
                                To Date</td>
                            <td>
                               <asp:TextBox ID="txt_todate" runat="server"  CssClass="form-control" AutoPostBack="true" onkeyup="fn_date(event,this.id);" MaxLength="10" ></asp:TextBox></td>
                            <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txt_todate" Format="dd/MM/yyyy" TodaysDateFormat="d MMMM, yyyy" runat="server">
                    </asp:CalendarExtender>
                            <td>
                                Status</td>
                            <td>
                               <asp:DropDownList ID="ddl_to_tstat" runat="server" CssClass="form-control" onselectedindexchanged="ddl_to_tstat_SelectedIndexChanged" 
                        AutoPostBack="True" Width="75%">
                        <asp:ListItem>Select</asp:ListItem>
                        <asp:ListItem>First Half</asp:ListItem>
                        <asp:ListItem>Full Day</asp:ListItem>
                    </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>
                                Total Days</td>
                            <td>
                                 <asp:TextBox ID="txt_tot_days" runat="server" CssClass="form-control" ReadOnly="True" Enabled="False"></asp:TextBox></td>
                            <td>
                                Submitted Date</td>
                            <td>
                               <asp:TextBox ID="txt_sdate" runat="server" CssClass="form-control" onkeyup="fn_date(event,this.id);"></asp:TextBox></td>
                        <asp:CalendarExtender ID="CalendarExtender3" TargetControlID="txt_sdate" Format="dd/MM/yyyy" TodaysDateFormat="d MMMM, yyyy" runat="server">
                    </asp:CalendarExtender>
                        </tr>
                        <tr>
                            <td>
                                Priority</td>
                            <td>
                                <asp:DropDownList ID="ddl_priority" CssClass="form-control" runat="server" AutoPostBack="True" Width="50%">
                        <asp:ListItem>Select</asp:ListItem>
                        <asp:ListItem>Low</asp:ListItem>
                        <asp:ListItem>Medium</asp:ListItem>
                        <asp:ListItem>High</asp:ListItem>
                    </asp:DropDownList></td>
                            <td>
                                Reason</td>
                            <td>
                                <asp:TextBox ID="txt_reason" CssClass="form-control" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                Approved by</td>
                            <td>
                                <asp:DropDownList ID="ddl_approve" CssClass="form-control" runat="server" Width="50%">
                    </asp:DropDownList></td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:Button ID="btn_save" runat="server" class="btn btn-success" 
                                    onclick="btn_save_Click" Text="Save"/>
                            </td>
                        </tr>
                        </table>   
                     </div>
 
    <asp:GridView ID="GridView_ondutyentry" runat="server" AutoGenerateColumns="False"  CssClass="table table-striped table-bordered table-hover"
        ShowFooter="True"  AllowSorting="True" HorizontalAlign="Center"  GridLines="None"
        onrowdeleting="GridView_ondutyentry_RowDeleting" onrowcancelingedit="GridView_ondutyentry_RowCancelingEdit" 
        onrowediting="GridView_ondutyentry_RowEditing" onrowupdating="GridView_ondutyentry_RowUpdating">

        <Columns>     
<%--        <asp:TemplateField>
                        <HeaderTemplate>
                        S.No.</HeaderTemplate>
                        <ItemTemplate>
                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="Ref No">
            <EditItemTemplate>
                    <asp:Label ID="E_lbl_sno" runat="server" Text='<%# Bind("Ref_no") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbl_sno" runat="server" Text='<%# Eval("Ref_no") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Employee">
             
                <EditItemTemplate>
                    <asp:Label ID="E_txt_ename" runat="server" 
                        Text='<%# Bind("empname") %>'></asp:Label>
                </EditItemTemplate>

                <ItemTemplate>
                    <asp:Label ID="txt_ename" runat="server" Text='<%# Eval("empname") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="On Duty Date">
             
                <EditItemTemplate>
                    <asp:TextBox ID="E_txt_ondutydat" runat="server" 
                        Text='<%# Bind("onduty_dat","{0:dd/MM/yyyy}") %>' onkeyup="fn_date(event,this.id);"  ontextchanged="E_txt_ondutydat_TextChanged"></asp:TextBox>
                </EditItemTemplate>

                <ItemTemplate>
                    <asp:Label ID="lbl_onduty_dat" runat="server" Text='<%# Eval("onduty_dat","{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="From Status">
             
              <EditItemTemplate>
                    <asp:DropDownList ID="E_ddl_onduty_fstat" runat="server" BackColor="White" ForeColor="Black"  AutoPostBack="True"  
                        SelectedValue= '<%# Bind("fstatus")%>' 
                        onselectedindexchanged="E_ddl_onduty_fstat_SelectedIndexChanged">
                        <asp:ListItem>Select</asp:ListItem>
                        <asp:ListItem>First Half</asp:ListItem>
                        <asp:ListItem>Second Half</asp:ListItem>
                        <asp:ListItem>Full Day</asp:ListItem>                        
                    </asp:DropDownList>
                </EditItemTemplate>
             
            
                <ItemTemplate>
                    <asp:Label ID="lbl_fstat" runat="server" Text='<%# Eval("fstatus") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="To Date">
            
            <EditItemTemplate>
                    <asp:TextBox ID="E_txt_todate" runat="server" 
                        Text='<%# Bind ("todat","{0:dd/MM/yyyy}") %>' onkeyup="fn_date(event,this.id);" 
                        AutoPostBack="True"></asp:TextBox>
                </EditItemTemplate>            
             
                <ItemTemplate>
                    <asp:Label ID="lbl_tdat" runat="server" Text='<%# Eval ("todat","{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="To Status">
            
            <EditItemTemplate>
                    <asp:DropDownList ID="E_ddl_to_tstat" runat="server" SelectedValue='<%# Bind("tstatus") %>' AutoPostBack="True" 
                        onselectedindexchanged="E_ddl_to_tstat_SelectedIndexChanged" >
                        <asp:ListItem>Select</asp:ListItem>
                        <asp:ListItem>First Half</asp:ListItem>   
                        <asp:ListItem>Full Day</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
            
                <ItemTemplate>
                    <asp:Label ID="lbl_tostat" runat="server" Text='<%# Eval("tstatus") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Tot Days">
            
            <EditItemTemplate>
                    <asp:TextBox ID="E_txt_tot_days" onkeyup="fn_date(event,this.id);" Text='<%# Bind ("tot_days") %>' runat="server"></asp:TextBox>
                </EditItemTemplate>    
       
                <ItemTemplate>
                    <asp:Label ID="lbl_tot" runat="server" Text='<%# Eval ("tot_days") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Submitted on">
            
            <EditItemTemplate>
                    <asp:TextBox ID="E_txt_sdate" onkeyup="fn_date(event,this.id);" Text='<%# Bind("sub_dat","{0:dd/MM/yyyy}") %>' runat="server"></asp:TextBox>
                </EditItemTemplate>    
            
                <ItemTemplate>
                    <asp:Label ID="lbl_subon" runat="server" Text='<%# Eval("sub_dat","{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Reason">
            
            <EditItemTemplate>
                    <asp:TextBox ID="E_txt_reason" Text='<%# Bind("reason") %>' runat="server"></asp:TextBox>
                </EditItemTemplate>  
            
                <ItemTemplate>
                    <asp:Label ID="lbl_reason" runat="server" Text='<%# Eval("reason") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            
            <asp:TemplateField HeaderText="Priority">
            
            <EditItemTemplate>                   
                
                <asp:DropDownList ID="E_ddl_priority" runat="server" SelectedValue= '<%# Bind("priority") %>'
                        AutoPostBack="True">
                        <asp:ListItem>Select</asp:ListItem>
                        <asp:ListItem>Low</asp:ListItem>
                        <asp:ListItem>Medium</asp:ListItem>
                        <asp:ListItem>High</asp:ListItem>
                    </asp:DropDownList> 
                    </EditItemTemplate>
            
                <ItemTemplate>
                    <asp:Label ID="lbl_priority" runat="server" Text='<%# Eval("priority") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            
            <asp:TemplateField HeaderText="Approval ">
            
            <EditItemTemplate>
            <asp:Label ID="E_lbl_approval" runat="server" Text='<%# Bind("approval") %>'></asp:Label>
            </EditItemTemplate>
            
                <ItemTemplate>
                    <asp:Label ID="lbl_approval" runat="server" Text='<%# Eval("approval") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowDeleteButton="true" ShowEditButton="true" />          
        </Columns>
        

        <EmptyDataTemplate>
            No Records
        </EmptyDataTemplate>
    </asp:GridView>
    </td>
</tr>

<tr>
<td  align="center" colspan="2">
    <div>


            <div>                    
               <asp:GridView ID="GridView_hr" runat="server" 
                        Width="100%"  HorizontalAlign="Center"
                        OnRowCommand="GridView_hr_RowCommand" 
                        AutoGenerateColumns="false"   AllowPaging="false"
                        DataKeyNames="Sno" 
                         CssClass="table table-striped table-bordered table-hover">
                <Columns>
                
                   <asp:ButtonField CommandName="detail" 
                         ControlStyle-CssClass="btn btn-info" ButtonType="Button" 
                         Text="Detail" HeaderText="Select"/>
            <asp:BoundField DataField="Ref_no" HeaderText="Reference No" />
            <asp:BoundField DataField="empid" HeaderText="Emp ID" />
            <asp:BoundField DataField="empname" HeaderText="Emp Name" />
            <asp:BoundField DataField="onduty_dat" dataformatstring="{0:dd/MM/yyyy}" HeaderText="Date From" />
            <asp:BoundField DataField="todat" dataformatstring="{0:dd/MM/yyyy}" HeaderText="Date To" />
            <asp:BoundField DataField="tot_days" HeaderText="Total Days" />
            <asp:BoundField DataField="sub_dat" dataformatstring="{0:dd/MM/yyyy}" HeaderText="Submitted on" />
            <asp:BoundField DataField="reason" HeaderText="Reason" />
            <asp:BoundField DataField="Priority" HeaderText="Priority" />
               </Columns>
               </asp:GridView>

            </div>

    </div>
    </td>
    </tr>
</table>  

<div id="dialog">

   <div class="modal-body" >

                    <asp:DetailsView ID="DetailsView1" runat="server" 
                               CssClass="table table-bordered table-hover" 
                               BackColor="White" ForeColor="Black"
                               FieldHeaderStyle-Wrap="false" 
                               FieldHeaderStyle-Font-Bold="true"  
                               FieldHeaderStyle-BackColor="LavenderBlush" 
                               FieldHeaderStyle-ForeColor="Black"
                               BorderStyle="Groove" AutoGenerateRows="False" 
                               onmodechanging="DetailsView1_ModeChanging" 
                        onitemupdating="DetailsView1_ItemUpdating">
                        
                        <Fields>
                        <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lbl_sno" runat="server" Text='<%# Eval("sno") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lbl_Esno" runat="server" Text='<%# Bind("sno") %>'></asp:Label>
                        </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Emp ID">
                        <ItemTemplate>
                            <asp:Label ID="lbl_ID" runat="server" Text='<%# Eval("empid") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lbl_EID" runat="server" Text='<%# Bind("empid") %>'></asp:Label>
                        </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Emp Name">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Name" runat="server" Text='<%# Eval("empname") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lbl_EName" runat="server" Text='<%# Bind("empname") %>'></asp:Label>
                        </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Date From">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Fdate" runat="server" Text='<%# Eval("onduty_dat","{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_EFdate" runat="server" Height="100%" Text='<%# Bind("onduty_dat","{0:dd/MM/yyyy}") %>'></asp:TextBox>
                        </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Date To">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Tdate" runat="server" Text='<%# Eval("todat","{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_ETdate" runat="server" OnTextChanged="txt_ETdate_TextChanged"  Height="100%" Text='<%# Bind("todat","{0:dd/MM/yyyy}") %>'></asp:TextBox>
                        </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Total Days">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Totdays" runat="server" Text='<%# Eval("tot_days") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_ETotdays" runat="server"  Height="100%" Text='<%# Bind("tot_days") %>'></asp:TextBox>
                        </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Submitted on">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Subdate" runat="server" Text='<%# Eval("sub_dat","{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lbl_ESubdate" runat="server" Text='<%# Bind("sub_dat","{0:dd/MM/yyyy}") %>'></asp:Label>
                        </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Reason">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Reason" runat="server" Text='<%# Eval("reason") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lbl_EReason" runat="server" Text='<%# Bind("reason") %>'></asp:Label>
                        </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Approval Status">
                        <ItemTemplate>
                            <asp:Label ID="lbl_App" runat="server" Text='<%# Eval("approval") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddl_EApp" runat="server" Text='<%# Bind("approval") %>'>
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem>Approve</asp:ListItem>
                                    <asp:ListItem>Hold</asp:ListItem>
                                    <asp:ListItem>Pending</asp:ListItem>
                                    <asp:ListItem>Reject</asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Conversations">
                        <ItemTemplate>
                            <asp:Label ID="lbl_msg1" runat="server" Text='<%# Eval("Message1") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="lbl_Emsg1" runat="server" Width="100%"  Height="100%" Text='<%# Bind("Message1") %>'></asp:TextBox>
                        </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lbl_msg2" runat="server" Text='<%# Eval("Message2") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="lbl_Emsg2" runat="server" Width="100%"  Height="100%" Text='<%# Bind("Message2") %>'></asp:TextBox>
                        </EditItemTemplate>
                        </asp:TemplateField>
 
                        <asp:CommandField ShowEditButton="true" />
                       </Fields>
                  </asp:DetailsView>

                <%--<div class="modal-footer">
                    <button class="btn btn-info" data-dismiss="modal" 
                            aria-hidden="true">Close</button>
                </div>--%>
            </div>
             </div> 
              </ContentTemplate>
             </asp:UpdatePanel>
            </div>
</asp:Content>

