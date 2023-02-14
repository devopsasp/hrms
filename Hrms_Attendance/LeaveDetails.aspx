<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="LeaveDetails.aspx.cs" Inherits="Hrms_Company_Default" Title="Welcome to HRMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../../Scripts/Datavalid.js"></script>
    <script language=javascript type="text/javascript">
   
    function show_message()
    {
        alert("Department Name Already Exist");
    }
       
   function show_message1(msg)
    {
        alert(msg);
    }
    
    function fnSave()
    {   
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$DepartmentName.value == "")
        {
            alert("Enter Department Name");
            aspnetForm.ctl00$ContentPlaceHolder1$DepartmentName.focus();
            return false;
        }                        
        else
        { 
              return true;  
        }
    }    
</script>
<div><h2 class="page-header">Leave Details</h2></div>
<div><h3><asp:Label ID="lbl_Error" runat="server" CssClass="Error" ForeColor="Red" Font-Bold="True" Width="35%"></asp:Label></h3>
        <div style="float:right;"> 
        <asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged"
         CssClass="form-control">
          </asp:DropDownList>
        </div>
</div>
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
<table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td id="tdComposeHeader" valign="top" align="center">

                    <table  width="100%" class="table">
                        <tr runat="server">
                            <td >
                                Department</td>
                            <td >
                                                <span style="color: #800000" __designer:mapid="ba">
                                                <asp:DropDownList ID="ddl_department" runat="server" AutoPostBack="True" 
                                                    class="form-control" 
                                                    
                                    onselectedindexchanged="ddl_department_SelectedIndexChanged" Width="100%">
                                                </asp:DropDownList>
                                                </span></td>
                            <td >
                                &nbsp;</td>
                            <td >
                                &nbsp;</td>
                        </tr>
                        
                        <tr id="row_emp" runat="server">
                            <td >
                            Employee</td>
                            <td >
                            <asp:DropDownList ID="ddl_Employee" runat="server" CssClass="form-control">
                            </asp:DropDownList></td>
                            <td >
                            </td>
                            <td >
                            </td>
                        </tr>
                        
                        <tr id="row_year" runat="server">
                            <td>
                                Year </td>
                            <td>
                                <asp:DropDownList ID="ddl_Year" runat="server" CssClass="form-control">
                                </asp:DropDownList></td>
                            <td></td>
                            <td ></td>
                        </tr>                        
                        <tr id="row_month" runat="server">
                            <td >
                                Month </td>
                            <td >
                                <asp:DropDownList ID="ddl_Month" runat="server"  CssClass="form-control">
                                    <asp:ListItem Value="01">January</asp:ListItem>
                                    <asp:ListItem Value="02">February</asp:ListItem>
                                    <asp:ListItem Value="03">March</asp:ListItem>
                                    <asp:ListItem Value="04">April</asp:ListItem>
                                    <asp:ListItem Value="05">May</asp:ListItem>
                                    <asp:ListItem Value="06">June</asp:ListItem>
                                    <asp:ListItem Value="07">July</asp:ListItem>
                                    <asp:ListItem Value="08">August</asp:ListItem>
                                    <asp:ListItem Value="09">September</asp:ListItem>
                                    <asp:ListItem Value="10">October</asp:ListItem>
                                    <asp:ListItem Value="11">November</asp:ListItem>
                                    <asp:ListItem Value="12">December</asp:ListItem>
                                </asp:DropDownList></td>
                            <td >
                                <asp:Button  ID="Button1" runat="server" OnClick="btn_year_Click"  CssClass="btn btn-success" Text="View Details"/>
                                </td>
                            <td >
                                <asp:Button  ID="btn_view" 
                                    runat="server" OnClick="btn_view_Click" Visible="False"  Text="View Details" CssClass="btn btn-success" />
                            </td>
                        </tr>
                        <tr>
                              <td colspan="4"></td>

                        </tr>
                     </table>     
                     
                      <table width="100%">
                       <tr >
                         <td >
                        
                           <asp:GridView ID="grid_leave" runat="server" AutoGenerateColumns="False" Width="100%" DataKeyNames="leaveID"  CssClass="table table-striped table-bordered table-hover">
                             <Columns>
                             <asp:TemplateField>
                                    <HeaderTemplate>
                                        Leave Code
                                        </HeaderTemplate>
                                   <ItemTemplate>
                                         <asp:Label 
                                        runat="server" Text=<%#Eval("leaveCode")%> ID="grdleavecode" Enabled="false"  ></asp:Label>
                                   </ItemTemplate>
                                   </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Leave Name
                                        </HeaderTemplate>
                                   <ItemTemplate>
                                         <asp:Label 
                                        runat="server" Text=<%#Eval("leaveName")%> ID="grdleavename" Enabled="false" ></asp:Label>
                                   </ItemTemplate>
                                   </asp:TemplateField>
                                 
                                    <asp:TemplateField>
                                    <HeaderTemplate>
                                        Actual Days
                                   </HeaderTemplate>
                                      <ItemTemplate>
                                         <asp:Label Width="100%" runat="server" Text=<%#Eval("Count")%> ID="grdactual" Enabled="false" ></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>

                                    <asp:TemplateField>
                                    <HeaderTemplate>
                                        Allowed Days
                                   </HeaderTemplate>
                                   
                                      <ItemTemplate>
                                     <asp:Label Width="100%" runat="server" Text="0" ID="grdallowed" Enabled="false" ></asp:Label>
                                   </ItemTemplate>

                               </asp:TemplateField>
                               
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Days Taken
                                   </HeaderTemplate>
                                   <ItemTemplate>
                                         <asp:Label Width="100%" runat="server" Text="0" ID="grdcount" Enabled="false" ></asp:Label>
                                   </ItemTemplate>
                                   </asp:TemplateField>
                                   
                                    <asp:TemplateField>
                                    <HeaderTemplate>
                                        Date
                                   </HeaderTemplate>
                                   
                                   <ItemTemplate>
                                         <asp:TextBox Width="100%" CssClass="form-control" runat="server" ID="grddate" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                   </ItemTemplate>         
                               </asp:TemplateField>
                            </Columns>                         
                               
                        </asp:GridView>
                              
        			
				
                        
                        </td>
                       
                    </tr>
                       <tr >
                         <td >
                        
                    <asp:GridView ID="Grid_Details"  runat="server" AllowSorting="True"  CssClass="table table-striped table-bordered table-hover"
            AutoGenerateColumns="False" ShowFooter="True">
            
            <Columns>
            

                 <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Emp.Code">
                
                    <ItemTemplate>
                        <asp:Label ID="Labtask3" runat="server" Text='<%# Eval("Emp_Code") %>'></asp:Label>
                    </ItemTemplate>
                    
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Emp.Name">
                    <ItemTemplate>
                        <asp:Label ID="Label14" runat="server" Text='<%# Eval("Emp_Name") %>'></asp:Label>
                    </ItemTemplate>
                    
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Leave Code">
                    <ItemTemplate>
                        <asp:Label ID="Label15" runat="server" Text='<%# Eval("pn_leavecode") %>'></asp:Label>
                    </ItemTemplate>  
                    
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Leave Date">
                    <ItemTemplate>
                        <asp:Label ID="lbl_frm0" runat="server" Text='<%# Eval("from_date","{0:dd/MM/yyyy}") %>'></asp:Label>
                    </ItemTemplate>

                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="To Date">
                    <ItemTemplate>
                        <asp:Label ID="Label16" runat="server" 
                            Text='<%# Eval("to_date","{0:dd/MM/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
        
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Submitted on">
                    <ItemTemplate>
                        <asp:Label ID="Labelsubm0" runat="server"  
                            Text='<%# Eval("submitted_date","{0:dd/MM/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
        
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Reason">
                    <ItemTemplate>
                        <asp:Label ID="Label18" runat="server" Text='<%# Eval("Reason") %>'></asp:Label>
                    </ItemTemplate>    
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Approve">
                    
                    <ItemTemplate>
                        <asp:Label ID="Label19" runat="server" Text='<%# Eval("Approve") %>'></asp:Label>
                    </ItemTemplate>

                </asp:TemplateField>
                

            </Columns>

        </asp:GridView>
                        
                        </td>
                       
                    </tr>

                    
                  </table>
                  </td>                                    
            </tr>
            <tr>
            <td>
            <div><h3 class="page-header"><asp:Label  runat="server" ID="lreq">Leave Request</asp:Label></h3></div>
            </td>
            </tr>
            <tr>
            
                <td align="center">
                <div id="div1" class="scrollable-container" runat="server" style="overflow-x:auto;width:98%;overflow: auto; height: 500px;">
                    <asp:GridView ID="GridView2" Font-Size="Smaller" runat="server" AllowSorting="True" 
            AutoGenerateColumns="False" ShowFooter="True" 
            Width="100%" 
            onrowcommand="GridView2_RowCommand" CellPadding="4" 
            GridLines="None" 
            onrowdeleting="GridView2_RowDeleting"  CssClass="table table-striped table-bordered table-hover"
            onrowdatabound="GridView2_RowDataBound" onrowediting="GridView2_RowEditing" 
            onrowupdating="GridView2_RowUpdating" 
            onrowcancelingedit="GridView2_RowCancelingEdit">
            
            <Columns>
            
            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="S.no">
                
                    <ItemTemplate>
                        <asp:Label ID="Labtask1" runat="server" Text='<%# Eval("Sno") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:Label ID="Labelsubedit1" runat="server" Text='<%# Bind("Sno") %>' ></asp:Label>
                    </EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                 <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Emp.Code">
                
                    <ItemTemplate>
                        <asp:Label ID="Labtask" runat="server" Text='<%# Eval("Emp_Code") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:Label ID="Labelsubedit" runat="server" Text='<%# Bind("Emp_Code") %>' ></asp:Label>
                    </EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Emp.Name">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("Emp_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:Label ID="Labelassi" runat="server" Text='<%# Bind("Emp_Name") %>' ></asp:Label>
                    </EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25"  HeaderText="Code">
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# Eval("pn_leaveID") %>'></asp:Label>
                    </ItemTemplate>  
                    <EditItemTemplate>
                    <asp:Label ID="Labeledit" runat="server" Text='<%# Bind("pn_leaveID") %>'></asp:Label>
                    </EditItemTemplate> 


<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Leave Date">
                    <ItemTemplate>
                        <asp:Label ID="lbl_frm" runat="server" Text='<%# Eval("from_date","{0:dd/MM/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:Label ID="Labelleavedate" runat="server" Text='<%# Bind("from_date","{0:dd/MM/yyyy}") %>'></asp:Label>
                    </EditItemTemplate>
        
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="To Date">
                    <ItemTemplate>
                        <asp:Label ID="Label10" runat="server" Text='<%# Eval("to_date","{0:dd/MM/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:Label ID="Labeltodate" runat="server" Text='<%# Bind("to_date","{0:dd/MM/yyyy}") %>'></asp:Label>
                    </EditItemTemplate>
        
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Submitted on">
                    <ItemTemplate>
                        <asp:Label ID="Labelsubm" runat="server"  Width="75px"
                            Text='<%# Eval("submitted_date","{0:dd/MM/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:Label ID="Labelsubm" runat="server" Text='<%# Bind("submitted_date","{0:dd-MM-yyyy}") %>'></asp:Label>
                    </EditItemTemplate>
        
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                 <asp:TemplateField ItemStyle-HorizontalAlign="Center"  HeaderText="Priority">
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("priority") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="lbl_prioredit" runat="server" Text='<%# Bind("priority") %>'></asp:Label>
                    
                    </EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Reason">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("Reason") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:Label ID="Labelreason" runat="server" Text='<%# Bind("Reason") %>'></asp:Label>
                    </EditItemTemplate>
         

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Approve">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList2" runat="server" Text='<%# Bind("Approve") %>' CssClass="form-control">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>Pending</asp:ListItem>
                            <asp:ListItem>Yes</asp:ListItem>
                            <asp:ListItem>No</asp:ListItem>
                            <asp:ListItem>Hold</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label12" runat="server" Text='<%# Eval("Approve") %>'></asp:Label>
                    </ItemTemplate>
 

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Remind on">
                    <ItemTemplate>
                        <asp:Label ID="Label13" runat="server" Text='<%# Eval("Reminder","{0:dd/MM/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="txt_remind" runat="server"  CssClass="form-control"
                            Text='<%# Bind("Reminder","{0:dd-MM-yyyy}") %>'></asp:TextBox>
                    </EditItemTemplate>


<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center"  HeaderText="Comments">
                    <ItemTemplate>
                         <asp:Label ID="Labelcomm" runat="server" Text='<%# Eval("Comments") %>' 
                            ></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="txtcom" runat="server" Text='<%# Bind("Comments") %>'  CssClass="form-control"
                            ></asp:TextBox>
                    </EditItemTemplate>
                    
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>                                
                <asp:CommandField ShowEditButton="True" ShowDeleteButton="true"/>
            </Columns>
          
        </asp:GridView>
        </div>
                </td>
            </tr>
        </table>
        </ContentTemplate>
          </asp:UpdatePanel>
</asp:Content>

