<%@ Page MaintainScrollPositionOnPostback="true" Language="C#" AutoEventWireup="true" EnableEventValidation="false" MasterPageFile="~/HRMS.master" CodeFile="Company_Home_old.aspx.cs" Inherits="Hrms_Company_Default" Title="Welcome to HRMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 

    <link href="~/Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
  <style type="text/css">
.blink
{
text-decoration:blink
}
</style>
 
<style type="text/css">
.highlightRow
{
background-color:#ffeb95;
text-decoration:underline;
cursor:pointer;
}
    .style19
    {
        height: 18px;
    }
    .style20
    {
        height: 38px;
    }
    .style21
    {
        height: 363px;
        width: 95%;
    }
    .style22
    {
        width: 95%;
    }
    </style>


 <link href="../_assets/css/meiii.css" rel="stylesheet" type="text/css" />
 <link href="../Themes/js-image-slider.css" rel="stylesheet" type="text/css" />
    <script src="../Themes/js-image-slider.js" type="text/javascript"></script>
  <link href="../Css/GridViewStyles.css" rel="stylesheet" type="text/css" />
  <link href="../Css/GridViewStyles1.css" rel="stylesheet" type="text/css" />
    <%--<script runat="server">

      
</script>--%>

<script type="text/javascript">

function check_uncheck(Val)
{
  var ValChecked = Val.checked;
  var ValId = Val.id;
  var frm = document.forms[0];
  for (i = 0; i < frm.length; i++)
  {
    if (this != null)
    {
      if (ValId.indexOf('CheckAll') !=  - 1)
      {
        if (ValChecked)
          frm.elements[i].checked = true;
        else
          frm.elements[i].checked = false;
      }
      else if (ValId.indexOf('deleteRec') !=  - 1)
      {
        if (frm.elements[i].checked == false)
          frm.elements[1].checked = false;
      }
    } 
  } 
} 

function confirmMsg(frm)
{
  // loop through all elements
  for (i = 0; i < frm.length; i++)
  {
    // Look for our checkboxes only
    if (frm.elements[i].name.indexOf("deleteRec") !=  - 1)
    {
      // If any are checked then confirm alert, otherwise nothing happens
      if (frm.elements[i].checked)
      return confirm('Are you sure you want to delete your selection(s)?')
    }
  }
}
</script>

     
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
                    <asp:SiteMapDataSource ID="SiteMapDataSource1" SiteMapProvider="Menu1Sitemap"  runat="server" />
                    <asp:SiteMapDataSource ID="SiteMapDataSource2" SiteMapProvider="Menu2Sitemap" runat="server" />
                        <asp:DropDownList ID="DropDownList3" runat="server" Visible="False">
                        </asp:DropDownList>
<br />
<div class="row">
                <div class="col-lg-9">

            <div class="row">
                <div class="col-lg-4 col-md-6">
                    <div class="panel panel-info">
                     <div class="panel-heading">
                     <div style="width:90%;">
                                <span style="font-size:medium;" class="pull-left">Application Status</span></div>
                                <span class="pull-right" >  <i class="glyphicon glyphicon-list-alt fa-2x"></i>
                                    
                                </span>
                                <div class="clearfix"></div>
                            </div>
                        <div class="panel-body">
                            <div class="row" align="left" style="padding-left:10%">
                               <asp:HyperLink ID="lnk_leave" runat="server" Font-Names="Calibri" 
                                                        ForeColor="#666666" Font-Underline="False" 
                                                        NavigateUrl="~/Hrms_Attendance/LeaveDetails.aspx">Leave Applications : </asp:HyperLink>
                                                    <asp:Label ID="lbl_app1" runat="server" Font-Names="Calibri" 
                                                        Font-Underline="False" ForeColor="#CC3300" Text="0"></asp:Label>
                                    </div>
                         <div class="row" align="left" style="padding-left:10%">
                           
                                                    <asp:HyperLink ID="lnk_loan" runat="server" Font-Names="Calibri" 
                                                        ForeColor="#666666" Font-Underline="False" 
                                                        NavigateUrl="~/Hrms_PayRoll/loanentry.aspx">Loan Applications : </asp:HyperLink>
                                                    <asp:Label ID="lbl_app2" runat="server" Font-Names="Calibri" 
                                                        Font-Underline="False" ForeColor="#CC3300" Text="0"></asp:Label>
                                                </div>
                          <div class="row" align="left" style="padding-left:10%">
                                                    <asp:HyperLink ID="lnk_claim" runat="server" Font-Names="Calibri" 
                                                        ForeColor="#666666" Font-Underline="False" 
                                                        NavigateUrl="~/Hrms_Tasks/Reimbursement.aspx">Reimbursement : </asp:HyperLink>
                                                    <asp:Label ID="lbl_app3" runat="server" Font-Names="Calibri" 
                                                        Font-Underline="False" ForeColor="#CC3300" Text="0"></asp:Label>
                                               
                           </div>
                           <br />
                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-6">
                    <div class="panel panel-info">
                     <div class="panel-heading">
                                <span style="font-size:medium;" class="pull-left">Holiday / Leave Details</span>
                                <span class="pull-right" >  <i class="glyphicon glyphicon-road fa-2x"></i>
                                    
                                </span>
                                <div class="clearfix"></div>
                            </div>
                        <div class="panel-body">
                            <div class="row" align="left" style="padding-left:10%">
                              <asp:HyperLink ID="lnk_yearholiday" runat="server" Font-Names="Calibri" 
                                                        ForeColor="#666666" Font-Underline="False" 
                                                        NavigateUrl="~/Hrms_Master/Leave/Holiday.aspx">Total Holidays / Year</asp:HyperLink>
                         </div>
                         <div class="row" align="left" style="padding-left:10%">
                           
                                                   <asp:Label ID="lnk_monthholiday" runat="server" Font-Names="Calibri" 
                                                        ForeColor="#666666" Font-Underline="False">Current Month Holidays</asp:Label>

                          </div>
                          <div class="row" align="left" style="padding-left:10%">
                                                     <asp:Label ID="lnk_leaveperm" runat="server" Font-Names="Calibri" 
                                                        ForeColor="#666666" Font-Underline="False">Leave Permitted / Month</asp:Label>
                                               
                           </div>
                           <br />
                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-6">
                    <div class="panel panel-info">
                     <div class="panel-heading">
                                <span style="font-size:medium;" class="pull-left">Job Request</span>
                                <span class="pull-right" >  <i class="glyphicon glyphicon-briefcase fa-2x"></i>
                                    
                                </span>
                                <div class="clearfix"></div>
                            </div>
                        <div class="panel-body">
                        <br />
                            <div class="row" align="left" style="padding-left:10%">
                                <asp:HyperLink ID="lnk_currentjob" runat="server" Font-Names="Calibri" 
                                                        ForeColor="#666666" Font-Underline="False" NavigateUrl="~/Hrms_Recruitment/requisition_approval.aspx">Current Requisition : </asp:HyperLink>
                                                    <asp:Label ID="lbl_app4" runat="server" Font-Names="Calibri" 
                                                        Font-Underline="False" ForeColor="#CC3300" Text="0"></asp:Label>
                         </div><br /><br />
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
             <div class="col-lg-4 col-md-6">
                    <div class="panel panel-info">
                     <div class="panel-heading">
                                <span style="font-size:medium;" class="pull-left">Statistics</span>
                                <span class="pull-right" >  <i class="glyphicon glyphicon-stats fa-2x"></i>
                                    
                                </span>
                                <div class="clearfix"></div>
                            </div>
                        <div class="panel-body">
                            <div class="row" align="left" style="padding-left:10%">
                              <asp:Label ID="lnk_totemp" runat="server" Font-Names="Calibri" 
                                                        ForeColor="#666666" Font-Underline="False">Total No. of Employees : </asp:Label>
                                                    <asp:Label ID="lbl_app5" runat="server" Font-Names="Calibri" 
                                                        Font-Underline="False" ForeColor="#CC3300" Text="0"></asp:Label>
                         </div>
                         <div class="row" align="left" style="padding-left:10%">
                           
                                             <asp:Label ID="lnk_empbench" runat="server" Font-Names="Calibri" 
                                                        ForeColor="#666666" Font-Underline="False">Employees in Bench : </asp:Label>
                                                    <asp:Label ID="lbl_app6" runat="server" Font-Names="Calibri" 
                                                        ForeColor="#CC3300" Text="0"></asp:Label>

                          </div>
                          <div class="row" align="left" style="padding-left:10%">
                                                      <asp:Label ID="lnk_emppro" runat="server" Font-Names="Calibri" 
                                                        ForeColor="#666666" Font-Underline="False">Employees in Project : </asp:Label>
                                                    <asp:Label ID="lbl_app7" runat="server" Font-Names="Calibri" 
                                                        ForeColor="#CC3300" Text="0"></asp:Label>
                                               
                           </div>
                           <br />
                        </div>
                    </div>
                </div>
             <div class="col-lg-4 col-md-6">
                    <div class="panel panel-info">
                     <div class="panel-heading">
                                <span style="font-size:medium;" class="pull-left">Complaints / Queries</span>
                                <span class="pull-right" >  <i class="glyphicon glyphicon-question-sign fa-2x"></i>
                                    
                                </span>
                                <div class="clearfix"></div>
                            </div>
                        <div class="panel-body">
                        <br />
                            <div class="row" align="left" style="padding-left:10%">
                               <asp:HyperLink ID="lnk_Complaints" runat="server" Font-Names="Calibri" 
                                                        ForeColor="#666666" Font-Underline="False">Complaints : </asp:HyperLink>
                                                    <asp:Label ID="lbl_app8" runat="server" Font-Names="Calibri" 
                                                        ForeColor="#CC3300" Text="0"></asp:Label>
                         </div>
                        
                          <div class="row" align="left" style="padding-left:10%">
                                                      <asp:HyperLink ID="lnk_Query" runat="server" Font-Names="Calibri" 
                                                        ForeColor="#666666" Font-Underline="False">Queries : </asp:HyperLink>
                                                    <asp:Label ID="lbl_app9" runat="server" Font-Names="Calibri" 
                                                        ForeColor="#CC3300" Text="0"></asp:Label>
                                               
                           </div>
                           <br />
                        </div>
                    </div>
                </div>
             <div class="col-lg-4 col-md-6">
                    <div class="panel panel-info">
                     <div class="panel-heading">
                                <span style="font-size:medium;" class="pull-left">Employee Details</span>
                                <span class="pull-right" >  <i class="fa fa-users fa-2x"></i>
                                    
                                </span>
                                <div class="clearfix"></div>
                            </div>
                        <div class="panel-body">
                       
                            <div class="row" align="left" style="padding-left:10%">
                              <asp:Label ID="lnk_permemp" runat="server" Font-Names="Calibri" 
                                                        ForeColor="#666666" Font-Underline="False">Permanent Employees : </asp:Label>
                                                    <asp:Label ID="lbl_app10" runat="server" Font-Names="Calibri" 
                                                        ForeColor="#CC3300" Text="0"></asp:Label>
                         </div>
                        
                          <div class="row" align="left" style="padding-left:10%">
                                                   <asp:Label ID="lnk_Contract" runat="server" Font-Names="Calibri" 
                                                        ForeColor="#666666" Font-Underline="False">Contract Employees : </asp:Label>
                                                    <asp:Label ID="lbl_app11" runat="server" Font-Names="Calibri" 
                                                        ForeColor="#CC3300" Text="0"></asp:Label>
                                               
                           </div>
                            <div class="row" align="left" style="padding-left:10%">
                                                 <asp:Label ID="lnk_Probation" runat="server" Font-Names="Calibri" 
                                                        ForeColor="#666666" Font-Underline="False">Probations : </asp:Label>
                                                    <asp:Label ID="lbl_app12" runat="server" Font-Names="Calibri" 
                                                        ForeColor="#CC3300" Text="0"></asp:Label>
                                               
                           </div>
                           <br />
                        </div>
                    </div>
                </div>
            </div>
            </div>
            <div class="col-lg-3">
            <div class="col-lg-12 col-md-6">
                    <div class="panel panel-info">
                     <div class="panel-heading">
                                <span style="font-size:medium;" class="pull-left">Today's Absentees</span>
                                <span class="pull-right" >  <i class="fa fa-users fa-2x"></i>
                                    
                                </span>
                                <div class="clearfix"></div>
                            </div>
                        <div class="panel-body">
                        <div id="diva" runat="server" align="left" style="overflow: auto; height: 265px;">
                       <asp:GridView ID="Grid_attend" runat="server" AutoGenerateColumns="False"  CssClass="MyGridView" Width="95%" DataKeyNames="emp_code" 
                                                        Height="290px" ShowHeader="False" ForeColor="#666666">
                                                <Columns>
                                                 <asp:TemplateField Visible="false">
                                                    <HeaderStyle BackColor="#97ABC1" HorizontalAlign="Left" />        
                                                    <ItemTemplate>
                                                        <asp:Label ID="lnk_leave_list" runat="server" Text='<%#Eval("emp_code")%>'></asp:Label> 
                                                            <%--<asp:Label ID="lnk_leave_list" runat="server" Text='<%# Eval("leave") %>'></asp:Label> --%>
                                                            <asp:LinkButton ID="LinkButton1" runat="server"   
                                                                CommandName="cmd"  Text='<%#Eval("emp_name")%>' ForeColor="#666666"></asp:LinkButton>
                                                        
                                                    </ItemTemplate>
        

                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField>    
                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="lnk_leave_list" runat="server" Text='<%# Eval("leave") %>'></asp:Label> --%>
                                                            <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="#666666"  CommandName="cmd"  Text='<%#Eval("emp_name")%>'></asp:LinkButton>
                                                        <asp:Label ID="lnk_leave_list" runat="server" Text='<%#Eval("emp_code")%>'></asp:Label> 
                                                    
                                                        </ItemTemplate>
        

<HeaderStyle HorizontalAlign="Left" BackColor="#97ABC1"></HeaderStyle>


                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                               </Columns>
                                               </asp:GridView>
                                               </div>
                        </div>
                    </div>
                </div>
            </div>
 </div>
 <div class="row">
 <div class="col-lg-9">
 
 <div class="panel panel-info">
                     <div class="panel-heading">
                     
                                <span style="font-size:medium;" class="pull-left">Charts</span>
                                <span class="pull-right" >  <i class="fa fa-bar-chart-o fa-2x"></i>
                                    
                                </span>
                                 <div class="clearfix"></div>
                                </div>
                                 <div class="panel-heading">
                                 <div class="row">
  <span class="pull-left">
 <span class="pull-left">Chart Type</span>
                                <span class="pull-right" >   <asp:DropDownList ID="ddl_charttype" runat="server" AutoPostBack="True" 
                                                        CssClass="InputDefaultStyle" 
                                                        onselectedindexchanged="ddl_charttype_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        </span></span>
                                         <span class="pull-left" >

                                          <span  class="pull-left">Palette</span>
                                <span class="pull-right" >   <asp:DropDownList ID="ddl_Palette" runat="server" AutoPostBack="True" 
                                                        CssClass="InputDefaultStyle" 
                                                        onselectedindexchanged="ddl_Palette_SelectedIndexChanged">
                                        </asp:DropDownList>

                                         </span></span>
                                          <span class="pull-left" >

                                          <span  class="pull-left">Inclination</span>
                                <span class="pull-right" >    <asp:DropDownList ID="ddl_inclination" runat="server" AutoPostBack="True" 
                                                        CssClass="InputDefaultStyle" 
                                                        onselectedindexchanged="ddl_inclination_SelectedIndexChanged">
                                            <asp:ListItem>-------Select-------</asp:ListItem>
                                            <asp:ListItem Value="0">0 degree</asp:ListItem>
                                            <asp:ListItem Value="15">15 degree</asp:ListItem>
                                            <asp:ListItem Value="30" Selected="True">30 degree</asp:ListItem>
                                            <asp:ListItem Value="45">45 degree</asp:ListItem>
                                            <asp:ListItem Value="60">60 degree</asp:ListItem>
                                        </asp:DropDownList>
                                         </span></span>
                                         <span class="pull-left" >

                                          <span  class="pull-left">Label Style</span>
                                <span class="pull-right" >      <asp:DropDownList ID="ddl_lblstyle" runat="server" AutoPostBack="True" 
                                                        CssClass="InputDefaultStyle" >
                                            <asp:ListItem>-------Select-------</asp:ListItem>
                                            <asp:ListItem Value="Outer">Outer</asp:ListItem>
                                            <asp:ListItem Value="Inner" Selected="True">Inner</asp:ListItem>
                                            <asp:ListItem Value="Disabled">Disabled</asp:ListItem>
                                        </asp:DropDownList>
                                         </span></span>
                                          <span class="pull-left" >

                                          <span class="pull-left">  <asp:CheckBox ID="Chk_3d" runat="server" Checked="True" AutoPostBack="True" 
                                                        oncheckedchanged="Chk_3d_CheckedChanged" /></span>
                                <span class="pull-right" >Enable 3D View</span></span>

 
 </div>
                               
                            </div>
                        <div class="panel-body">
<div class="col-lg-6">
<div class="panel panel-info panel-heading" align="center">
                                   Attendance Chart  </div>
                                                         <div class="panel panel-info panel-body" style="overflow:scroll;">

                                                          <asp:Chart ID="Chart1" runat="server" 
                                                        BackImageAlignment="Center">
                                            <Series>
                                                <asp:Series Name="Series1" XValueMember="Status" YValueMembers="Numbers" 
                                                    IsValueShownAsLabel="True" ChartType="Pie" IsXValueIndexed="True" 
                                                    Label="#VALX #VAL" LegendText="#LEGENDTEXT" Palette="Excel" 
                                                    Legend="Legend1">
                                                </asp:Series>
                                            </Series>
                                            <ChartAreas>
                                                <asp:ChartArea Name="ChartArea1">
                                                    <Area3DStyle Enable3D="True"></Area3DStyle>
                                                </asp:ChartArea>
                                            </ChartAreas>
                                            <%--<Legends>
                                            <asp:Legend Name="Legend1" BackColor="AliceBlue" Font="Calibri, 8pt" 
                                                    IsTextAutoFit="False" TitleFont="Calibri, 8.25pt, style=Bold"></asp:Legend>
                                            </Legends>--%>
                                        </asp:Chart>  
                                                         </div>
                                                         </div>

<div class="col-lg-6">
<div class="panel panel-info panel-heading" align="center">
                                   Employee Distribution  </div>
                                                         <div class="panel panel-info panel-body" style="overflow:scroll;">
                                                         <asp:Chart ID="Chart2" runat="server" 
                                                        Palette="Excel">
                                                        <Series>
                                                            <asp:Series Name="Series1" XValueMember="v_Departmentname" 
                                                                YValueMembers="Dep_Count" ChartType="Bar" IsXValueIndexed="True" 
                                                    Label="#VALX #VAL" LegendText="#LEGENDTEXT" Palette="Excel" 
                                                    Legend="Legend1">
                                                                <EmptyPointStyle AxisLabel="No Data Found.." />
                                                            </asp:Series>
                                                        </Series>
                                                        <chartareas>
                                                            <asp:ChartArea Name="ChartArea1">
                                                                <Area3DStyle Enable3D="True" LightStyle="Realistic" />
                                                            </asp:ChartArea>
                                                        </chartareas>
                                                    </asp:Chart>
                                                         
                                                         </div>
                                                         </div>

   </div>                                                
 </div>
 </div>


 <div class="col-lg-3">
  <div class="row">
     <asp:Label ID="lbl_Error" runat="server" Text=""></asp:Label></div>
 <div class="panel panel-info">
                     <div class="panel-heading">
                                <span style="font-size:medium;" class="pull-left">Announcements</span>
                                <span class="pull-right" >  <i class="glyphicon glyphicon-bullhorn fa-2x"></i>
                                    
                                </span>
                                <div class="clearfix"></div>
                            </div>
                        <div class="panel-body">
                     <div class="row">
                     
                     <asp:GridView ID="Grid_announcements" DataKeyNames="announcementid" 
                                                        runat="server" AutoGenerateColumns="False"  CellPadding="3" 
                                                CssClass="MyGridView" Width="100%" Height="160px" 
                                                        onrowcreated="Grid_announcements_RowCreated" 
                                                        onrowcommand="Grid_announcements_RowCommand" ShowHeader="False" 
                                                        ForeColor="#666666" >
                                                
                                                      <RowStyle BackColor="White" ForeColor="#666666" />
                                                
                                                    <Columns>
                                                    <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                    <asp:Label ID="lblid" Text='<%#Eval("announcementid")%>' runat="server"></asp:Label>
                           
                                <%--<asp:Label ID="lnk_leave_list" runat="server" Text='<%# Eval("leave") %>'></asp:Label> --%>
                                <asp:LinkButton ID="Lnk_announcements" ForeColor="#666666" runat="server" 
                                    CommandArgument="<%# Container.DataItemIndex %> "  CommandName="cmd"  
                                    Text='<%#Eval("Announcements")%>'></asp:LinkButton>
                            
                                                    </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" BackColor="#97ABC1"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Today Announcements">
                                                        <HeaderStyle BackColor="#97ABC1" HorizontalAlign="Center" />       
                                                                    <ItemTemplate>
                                                                   
                                                                        <%--<asp:Label ID="lnk_leave_list" runat="server" Text='<%# Eval("leave") %>'></asp:Label> --%>
                                                                        <asp:LinkButton ID="Lnk_announcements" ForeColor="#666666" runat="server" CommandArgument="<%# Container.DataItemIndex %> "  CommandName="cmd"  Text='<%#Eval("Announcements")%>'></asp:LinkButton>
            <asp:Label ID="lblid" Text='<%# Eval("announcementid") %>' runat="server" ForeColor="#666666"></asp:Label>
            
                                                                    </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                </asp:TemplateField>
                                                      
                                                    </Columns>
                                                    </asp:GridView> 
                     </div>
                     <div class="row">
                       <span style="font-size:medium;" class="pull-left">   <asp:Label ID="lbl_branch0" runat="server" 
                                                        Text="ID"></asp:Label></span>
                                <span class="pull-right" >  <asp:TextBox ID="txt_id" runat="server" 
                                                        CssClass="form-control"></asp:TextBox></span>
                     </div>

                      <div class="row">
                       <span style="font-size:medium;" class="pull-left">   <asp:Label ID="lbl_branch1" runat="server" 
                                                        Text="Subject"></asp:Label></span>
                                <span class="pull-right" >  <asp:TextBox ID="txt_subject" runat="server" Width="150px" 
                                                        CssClass="form-control"></asp:TextBox>
                                                        </span>
                     </div>

                     <div class="row">
                       <span style="font-size:medium;" class="pull-left">   
                        <asp:Label ID="lbl_branch2" runat="server" 
                                                        Text="Text"></asp:Label>
                                                        </span>
                                <span class="pull-right" >  
                               <asp:TextBox ID="Txt_details" runat="server" 
                                                        TextMode="MultiLine" 
                                                        CssClass="form-control"></asp:TextBox>
                                                        </span>
                     </div>
                     <br />
                     <div class="row">
                     
                     <asp:Button ID="Img_btn_update" runat="server" 
                                                         CssClass="btn btn-info" onclick="Img_btn_update_Click" 
                                                        Text="Add" />
                                                    <asp:Button ID="Img_btn_publish" runat="server" 
                                                         CssClass="btn btn-warning" onclick="Img_btn_publish_Click" 
                                                        Text="Publish" />
                                                    <asp:Button ID="img_btn_cancel" runat="server" 
                                                         onclick="img_btn_cancel_Click"  Text="Cancel"
                                                         CssClass="btn btn-danger " />
                     
                     
                   <%--  <asp:ImageButton ID="Img_btn_update" runat="server" 
                                                         CssClass="btn btn-info btn-circle" onclick="Img_btn_update_Click" 
                                                        Width="75px" Height="25px" />
                                                    <asp:ImageButton ID="Img_btn_publish" runat="server" 
                                                        ImageUrl="~/Images/Add_btn.jpg" onclick="Img_btn_publish_Click" 
                                                        Width="60px" Height="26px" />
                                                    <asp:ImageButton ID="img_btn_cancel" runat="server" 
                                                        ImageUrl="~/Images/Clear_btn.jpg" onclick="img_btn_cancel_Click" 
                                                        Width="60px" Height="27px" />--%>
                                                        </div>
                        </div>
                        </div>
                    </div>
 
 </div>


</asp:Content>
