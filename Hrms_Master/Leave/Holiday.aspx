<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="holiday.aspx.cs" Inherits="Hrms_Master_Default" Title="Welcome to HRMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="../../Scripts/Datavalid.js"></script>

    <script language="javascript" type="text/javascript">
    function show_message()
    {
        alert("Leave Name Already Exist");
    }  
   
    function show_Error()
    {
        alert("Enter Value");
    }
  
    function fnSave()
    {   
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_leavename.value == "")
                {
                    alert("Enter Leave Name");
                    aspnetForm.ctl00$ContentPlaceHolder1$txt_leavename.focus();
                    return false;
                }
                             
        else
            { 
                if(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_LeaveCode.value == "")
                    {
                        alert("Enter Leave Code");
                        aspnetForm.ctl00$ContentPlaceHolder1$txt_leavename.focus();
                        return false;
                    }                     
                else
                {
                    if(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_count.value == "")
                        {
                            alert("Enter Leave Count");
                            aspnetForm.ctl00$ContentPlaceHolder1$txt_leavename.focus();
                            return false;
                        }                        
                    else
                        {
                        return true;  
                        }
                }
              
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
               bool_obj=true;
                          
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
    
      
    </script>

    <div class="row">
                <div class="col-lg-12">
                    <h2 class="page-header">Holiday</h2>
                </div>
                <!-- /.col-lg-12 -->
            </div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
            <div class="panel panel-default">
                        <div class="panel-heading">
                            Holiday Master
                            <div class="pull-right">
                                <asp:DropDownList ID="DropDownList1" runat="server" style="margin-left: 0px" class=" form-control"
                                    Width="134px" AutoPostBack="True" 
                                    onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                          <%--  <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>--%>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>

                            <div align="center" id="morris-area-chart" style="width: 80%">
                
                               <table class="table table-striped table-bordered table-hover">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" CssClass="table table-hover table-striped"
                                                    AutoGenerateColumns="False" 
                                                    onrowcancelingedit="GridView1_RowCancelingEdit" 
                                                    onrowcommand="GridView1_RowCommand" onrowdatabound="GridView1_RowDataBound" 
                                                    onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing" 
                                                    onrowupdating="GridView1_RowUpdating" 
                                                    onselectedindexchanged="GridView1_SelectedIndexChanged" ShowFooter="True" 
                                                    GridLines="None" Width="100%">
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Holiday Code" 
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_holidaycode" runat="server" 
                                                                    Text='<%# Eval("pn_holidaycode") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lbl_holidaycode_edit" runat="server" 
                                                                    Text='<%# Bind("pn_holidaycode") %>'></asp:Label>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="Txt_hcode" runat="server" class=" form-control" onkeydown="AllowOnlyText1(event);" onkeypress="AllowOnlyText3();"
                                                                    MaxLength="10" TabIndex="1" Width="50px"></asp:TextBox>
                                                            </FooterTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Holiday Name" 
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_holidayname" runat="server" 
                                                                    Text='<%# Eval("pn_holidayName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox CssClass=" form-control" ID="txt_holidayname_edit" runat="server" 
                                                                    Text='<%# Bind("pn_holidayName") %>' Width="100%"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="Txt_hname" runat="server" class=" form-control"
                                                                    MaxLength="30" onkeydown="AllowOnlyText1(event);" onkeypress="AllowOnlyText3();"  TabIndex="2"></asp:TextBox>
                                                            </FooterTemplate>
                                                           
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Holiday From" 
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_from_date" runat="server" 
                                                                    Text='<%# Eval("from_date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txt_from_date_edit" CssClass=" form-control" runat="server" 
                                                                    Text='<%# Bind("from_date","{0:dd/MM/yyyy}") %>' Width="100px"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="Txt_fdate" runat="server" AutoPostBack="True" 
                                                                   class="form-control" MaxLength="10" onkeyup="fn_date(event,this.id);" 
                                                                    ontextchanged="Txt_fdate_TextChanged" TabIndex="3" Width="100px" ></asp:TextBox>
                                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="Txt_fdate" Format="dd/MM/yyyy" >
                                                                </asp:CalendarExtender>
                                                            </FooterTemplate>
                                                            
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Holiday To" 
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_to_date" runat="server" 
                                                                    Text='<%# Eval("to_date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txt_to_date_edit" CssClass=" form-control" runat="server" 
                                                                    Text='<%# Bind("to_date","{0:dd/MM/yyyy}") %>' Width="100px"></asp:TextBox>
                                                               
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="Txt_tdate" runat="server" AutoPostBack="True" 
                                                                   class=" form-control" MaxLength="10" onkeyup="fn_date(event,this.id);" 
                                                                    ontextchanged="Txt_tdate_TextChanged" TabIndex="4" Width="100px"></asp:TextBox>
                                                                     <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="Txt_tdate" Format="dd/MM/yyyy">
                                                                </asp:CalendarExtender>
                                                            </FooterTemplate>
                                                           
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="No. of Days">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_days" runat="server"  Text='<%# Eval("days") %>' ></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txt_days1" runat="server" CssClass=" form-control" Width="50px" Text='<%# Bind("days") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                            <table>
                                                            <tr>
                                                            <td> <asp:Label ID="txt_days" runat="server"  TabIndex="5" Width="50px"></asp:Label> </td>
                                                            <td> <asp:Button ID="bt_add" runat="server" CommandName="add" class="btn btn-success" Text="Add" /> </td>
                                                            </tr>
                                                            </table>
                                                            </FooterTemplate>
                                                          
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ButtonType="Image" EditImageUrl="~/Images/edit_icon.png" DeleteImageUrl="~/Images/delete_icon.jpg" UpdateImageUrl="~/Images/save_icon.jpg" CancelImageUrl="~/Images/delete_icon.jpg" />
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <asp:Label ID="lblempty" runat="server" Text="No Records">
            </asp:Label>
                                                    </EmptyDataTemplate>
                                                   
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" ><span style="font-weight: normal">
                                                <span style="font-family: Calibri">
                                                <asp:Label ID="lbl_Erro1r" runat="server" CssClass="Error" Font-Bold="True" 
                                                    Font-Names="Calibri" Font-Size="Small" ForeColor="Red"></asp:Label>
                                                </span></span></td>
                                        </tr>
                                    </tbody>
                                </table> 
                
                            </div>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        
                    </div>

    </asp:Content>
