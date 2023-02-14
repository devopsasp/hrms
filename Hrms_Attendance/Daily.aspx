<%@ Page Language="C#" MasterPageFile="~/HRMS.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="Daily.aspx.cs" Inherits="Hrms_Attendance_Default" 
    Title="Leave Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<link href="../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript">

  function show_message(msg)
    {
        alert(msg);
    }

    function Todate_copy()
    {
    
     if(document.aspnetForm.ctl00$ContentPlaceHolder1$chk_date.checked==true)
     {      
      document.aspnetForm.ctl00$ContentPlaceHolder1$txt_ToDate.value= document.aspnetForm.ctl00$ContentPlaceHolder1$txt_FromDate.value;  
     }
     else
     { 
      document.aspnetForm.ctl00$ContentPlaceHolder1$txt_ToDate.value="";  
     }   
      
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
               bool_obj=fn_validate(txtlen,txtvalue);
                          
               if(bool_obj==true)
                 {
                      if(txtlen==2 || txtlen==5)
                      {
                      document.getElementById(txtid).value=txtvalue+"/";
                      }
                      else
                      {
                      document.getElementById(txtid).value=txtvalue;
                      }
                     
                 }
                 else
                 {            
                   document.getElementById(txtid).value= txtvalue.substring(0,txtlen-1);              
                 }                       
            } 
    }                                 
 }
    
    function fn_validate(len,tval)
    {
    var str;
    
          switch(len)
           {
     
        case 1: if(tval<=3)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                  
                  
        case 2:               
                
                if(tval<=31 && tval>0)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
                
        case 3: str=tval.charAt(2);
        
                if(str=="/")
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
      case 4: str=tval.charAt(3);
        
                if(str<=1)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
      case 5: str=tval.substring(3,5); 
        
                if(str<=12 && str>0)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
       case 6: str=tval.charAt(5);
        
                if(str=="/")
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
        case 7: str=tval.charAt(6);
        
                if(str<=9 && str>0)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
        case 8: str=tval.substring(6,8);
        
                if(str>=18)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
        case 9: str=tval.charAt(8);
        
                if(str<=9)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
        case 10: str=tval.charAt(9);
        
                if(str<=9)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
                
        default :return false;   
                 break;
           }
        
    }
    
    </script>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    <div class="row">
                <div class="col-lg-12">
                    <h3 class="page-header">Leave</h3>
                </div>
                <div class="pull-right">
                    <asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" class="form-control" OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <!-- /.col-lg-12 -->
            </div>
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
            <div class="panel-group" id="accordion">
                                <div class="panel panel-default" style="width: 100%">
                                    <div class="panel-heading"  style="width: 100%">
                                        <h4 class="panel-title">
                                            Leave / On Duty Details
                                        </h4>
                                        
                                    </div>
                                    <div id="collapseOne" class="panel-collapse collapse in">
                    <table cellpadding="1%" cellspacing="1%" width="100%" class="table table-striped table-bordered table-hover">
                     <tr>
                        <td width="15%">Department</td>
                        <td width="40%">
                            <span style="color: #800000">
                                <asp:DropDownList ID="ddl_department" runat="server" AutoPostBack="True" class="form-control" onselectedindexchanged="ddl_department_SelectedIndexChanged" >
                                </asp:DropDownList>
                            </span>

                        </td>
                            <td width="10%">Employee</td>
                            <td width="30%">
                                <asp:DropDownList ID="ddl_Employee" runat="server" class="form-control">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Calendar Year</td>
                            <td>
                                <asp:DropDownList ID="ddl_year" runat="server" class="form-control">
                                </asp:DropDownList>
                                </td>
                            <td>Code</td>
                            <td>
                                <asp:DropDownList ID="ddl_leave" runat="server" class="form-control">  
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="left">
                                <asp:Label ID="lbl_Error" runat="server" ForeColor="Red" CssClass="Error" Font-Bold="True" Width="50%"></asp:Label>
                                <asp:Button ID="btn_Details" runat="server" class="btn btn-info" 
                                    onclick="btn_Details_Click" Text="Get Details" />
                               </td>
                        </tr>
                     </table>   
                                </div>
                            </div>

                                <br />

                            
                                <div id="leave_history" runat="server" visible="false" class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            Leave / On Duty History</h4>
                                    </div>
                                    <div id="collapseTwo" class="panel-collapse collapse in">
                                        <div style="width: 100%">
                    <table cellpadding="1%" cellspacing="1%" width="100%" class="table table-striped table-bordered table-hover">
                        <tr>
                            <td colspan="6">
                                <asp:GridView ID="grid_leave" runat="server" AutoGenerateColumns="False" DataKeyNames="leaveID" OnRowDeleting="delete" Width="90%" CssClass="table table-striped table-bordered table-hover">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                    <colgroup>
                                                        <col></col>
                                                    </colgroup>
                                                    <thead>
                                                        <tr>
                                                            <th style="width: 100%;">From Date</th>
                                                        </tr>
                                                    </thead>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table width="100%">
                                                    <colgroup>
                                                        <col></col>
                                                    </colgroup>
                                                    <tbody>
                                                        <tr>
                                                            <td nowrap style="width: 100%;">
                                                                <asp:TextBox ID="grdfromdate" runat="server" class="form-control"  Enabled="false" Text='<%#Eval("str_fromdate","{0:dd/MM/yyyy}")%>' Width="100%"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                    <colgroup>
                                                        <col></col>
                                                    </colgroup>
                                                    <thead>
                                                        <tr>
                                                            <th style="width: 100%;">To Date</th>
                                                        </tr>
                                                    </thead>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table width="100%">
                                                    <colgroup>
                                                        <col></col>
                                                    </colgroup>
                                                    <tbody>
                                                        <tr>
                                                            <td nowrap style="width: 100%;">
                                                                <asp:TextBox ID="grdtodate" runat="server" class="form-control"  Enabled="false" Text='<%#Eval("str_todate","{0:dd/MM/yyyy}")%>' Width="100%"></asp:TextBox>

                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField>
                                            <HeaderTemplate>
                                                <table>
                                                    <colgroup>
                                                        <col></col>
                                                    </colgroup>
                                                    <thead>
                                                        <tr>
                                                            <th style="width: 100%;">
                                                                ToDate</th>
                                                        </tr>
                                                    </thead>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table>
                                                    <colgroup>
                                                        <col class="dInboxContentTableCheckBoxCol"></col>
                                                    </colgroup>
                                                    <tbody>
                                                        <tr>
                                                            <td nowrap="nowrap" style="width: 100%;">
                                                                <asp:TextBox ID="grdtodate" runat="server" class="form-control" Enabled="false" Text='<%#Eval("str_fromdate","{0:dd/MM/yyyy}")%>' Width="100%"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                    <colgroup>
                                                        <col></col>
                                                    </colgroup>
                                                    <thead>
                                                        <tr title="Total Leave taken for Selected Year">
                                                            <th style="width: 25%;">Days Taken</th>
                                                        </tr>
                                                    </thead>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table>
                                                    <colgroup>
                                                        <col></col>
                                                    </colgroup>
                                                    <tbody>
                                                        <tr title="Total Leave taken for Selected Year">
                                                            <td nowrap="nowrap" style="width: 25%;">
                                                                <asp:TextBox ID="grdleavecount" runat="server" class="form-control" Enabled="false" Text='<%#Eval("Cur_Leave")%>' Width="100%"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField>
                                            <HeaderTemplate>
                                                <table>
                                                    <colgroup>
                                                        <col></col>
                                                    </colgroup>
                                                    <thead>
                                                        <tr>
                                                            <th style="width: 25%;">
                                                                Delete</th>
                                                        </tr>
                                                    </thead>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table>
                                                    <colgroup>
                                                        <col></col>
                                                    </colgroup>
                                                    <tbody>
                                                        <tr>
                                                            <td align="center" nowrap style="width: 5%;">
                                                                <asp:ImageButton ID="img_update" runat="server" AlternateText="" 
                                                                    CommandName="delete" ImageUrl="../Images/i_delete.gif" Style="border: 0" />
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Allowed days</td>
                            <td>
                                <asp:TextBox ID="txt_Allowed" runat="server" Enabled="false" Text="0" class="form-control"></asp:TextBox>
                            </td>
                            <td>
                                Total Days Taken</td>
                            <td>
                                <asp:TextBox ID="txt_Taken" runat="server" Enabled="false" Text="0" class="form-control"></asp:TextBox>
                            </td>
                            <td>
                                Balance Days</td>
                            <td>
                                <asp:TextBox ID="txt_Balance" runat="server" Enabled="false" Text="0" 
                                    class="form-control"></asp:TextBox></td>
                        </tr>
                     </table>   
                     </div>
                                    </div>
                                </div>
                                <div id="leave_entry" runat="server" visible="false" class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            Leave / On Duty Entry</h4>
                                    </div>
                                    <div id="collapseThree" class="panel-collapse collapse in">
                                        <table cellpadding="1%" cellspacing="1%" 
                                            class="table table-striped table-bordered table-hover" width="100%">
                                            <tr>
                                                <td align="left">
                                                    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" CssClass="table table-striped table-bordered table-hover"
                                                        AutoGenerateColumns="False" 
                                                        onrowcancelingedit="GridView1_RowCancelingEdit" 
                                                        onrowcommand="GridView1_RowCommand" onrowcreated="GridView1_RowCreated" 
                                                        onrowdatabound="GridView1_RowDataBound" onrowdeleting="GridView1_RowDeleting" 
                                                        onrowediting="GridView1_RowEditing" onrowupdating="GridView1_RowUpdating" 
                                                         ShowFooter="True">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label13" Visible="false" runat="server" Text='<%# Eval("sno") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="txt_sno" Visible="false" runat="server" Text='<%# Bind("sno") %>' ></asp:Label>
                                                                </EditItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="From Date" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label20" runat="server" 
                                                                        Text='<%# Eval("from_date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txt_fromdate1" runat="server"  Width="80px"
                                                                        ontextchanged="txt_fromdate1_TextChanged" class="form-control"
                                                                        Text='<%# Bind("from_date","{0:dd/MM/yyyy}") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txt_fromdate2" runat="server" AutoPostBack="True" 
                                                                        maxlength="10" onkeyup="fn_date(event,this.id);" 
                                                                        ontextchanged="txt_fromdate_TextChanged" TabIndex="1" class="form-control" Width="100px"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txt_fromdate2">
                                                                    </asp:CalendarExtender>

                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label51" runat="server" Text='<%# Eval("from_status") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddl_stat1" runat="server" AutoPostBack="True" 
                                                                        DataTextField="Prior" DataValueField="Prior" 
                                                                        onselectedindexchanged="ddl_stat1_SelectedIndexChanged" 
                                                                        SelectedValue='<%#Bind ("from_status") %>' class="form-control">
                                                                        <asp:ListItem>Select</asp:ListItem>
                                                                        <asp:ListItem>FD</asp:ListItem>
                                                                        <asp:ListItem>FH</asp:ListItem>
                                                                        <asp:ListItem>SH</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:DropDownList ID="DropDownList21" runat="server" AutoPostBack="True" Width="80px" 
                                                                        onselectedindexchanged="DropDownList21_SelectedIndexChanged" 
                                                                        TabIndex="2" class="form-control">
                                                                        <asp:ListItem>Select</asp:ListItem>
                                                                        <asp:ListItem Value="FD">Full Day</asp:ListItem>
                                                                        <asp:ListItem Value="FH">First Half</asp:ListItem>
                                                                        <asp:ListItem Value="SH">Second Half</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="To Date" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label21" runat="server" 
                                                                        Text='<%# Eval("to_date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txt_todate1" runat="server" class="form-control" Width="100px"
                                                                        Text='<%# Bind("to_date","{0:dd/MM/yyyy}") %>' ></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txt_todate2" runat="server" class="form-control" Width="100px"
                                                                        maxlength="10" onkeyup="fn_date(event,this.id);" TabIndex="3"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" runat="server" TargetControlID="txt_todate2">
                                                                    </asp:CalendarExtender>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddl_stat" runat="server" AutoPostBack="True" 
                                                                        DataTextField="Prior" DataValueField="Prior" Width="80px"
                                                                        onselectedindexchanged="ddl_stat_SelectedIndexChanged" 
                                                                        SelectedValue='<%# Bind("status") %>' class="form-control">
                                                                        <asp:ListItem>Select</asp:ListItem>
                                                                        <asp:ListItem>FD</asp:ListItem>
                                                                        <asp:ListItem>FH</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" 
                                                                        EnableTheming="True" class="form-control" Width="80px"
                                                                        onselectedindexchanged="DropDownList2_SelectedIndexChanged" TabIndex="4">
                                                                        <asp:ListItem>Select</asp:ListItem>
                                                                        <asp:ListItem Value="FD">Full Day</asp:ListItem>
                                                                        <asp:ListItem Value="FH">First Half</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="No. of Days" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_days" runat="server" Text='<%# Eval("days") %>' Width="50px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txt_days1" runat="server" Enabled="False"  Width="40px"
                                                                        Text='<%# Bind("days") %>' class="form-control"></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txt_days" runat="server" Enabled="false"  Width="60px"
                                                                        TabIndex="5" class="form-control"></asp:TextBox>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Leave" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                             <asp:Label ID="lbl_LeaveCode" runat="server" Text='<%# Eval("pn_leavecode") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>                                
                                                            <asp:Label ID="ddl_LeaveCodeedit" runat="server" Text='<%# Bind("pn_leavecode") %>'></asp:Label>
                                                                <%--<asp:DropDownList ID="ddl_LeaveCodeedit" runat="server" CssClass="form-control" Width="150px">
                                                                </asp:DropDownList>--%>
                                                            </EditItemTemplate>
                                                                <FooterTemplate>
                                                                <asp:DropDownList ID="ddl_LeaveCode" runat="server" CssClass="form-control" Width="150px"></asp:DropDownList>                                                                                                                                                                        
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                          
                                                            <asp:TemplateField HeaderText="Reason" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label22" runat="server" Text='<%# Eval("Reason") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txt_rea1" runat="server" Text='<%# Bind("Reason") %>'  Width="100px" class="form-control"></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txt_rea" runat="server" TabIndex="7" class="form-control" Width="100px"></asp:TextBox>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Approved" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label23" runat="server" Text='<%# Eval("Approve") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Button ID="Button1" runat="server" CommandName="add" CssClass="btn btn-success"
                                                                        Text="Apply" />
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                   
                                                            <asp:CommandField ShowDeleteButton="True" ShowEditButton="true" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>

                            </ContentTemplate>
                                </asp:UpdatePanel>
    </asp:Content>
