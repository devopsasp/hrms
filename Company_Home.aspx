<%@ Page MaintainScrollPositionOnPostback="true" Language="C#" AutoEventWireup="true" EnableEventValidation="false" MasterPageFile="~/HRMS.master" CodeFile="Company_Home.aspx.cs" Inherits="Hrms_Company_Default" Title="Welcome to HRMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:content Id="content1"  ContentPlaceHolderId="contentPlaceHolder1" runat="server">
 
    <br />
 

    <link href="Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
  <style type="text/css">
.blink
{
text-decoration:"blink";
    }
</style>
 
<style type="text/css">
.highlightRow
{
background-color:#ffeb95;
text-decoration:underline;
cursor:pointer;
}
    </style>


<%-- <link href="../_assets/css/meiii.css" rel="stylesheet" type="text/css" />--%>
 <link href="Themes/js-image-slider.css" rel="stylesheet" type="text/css" /> 
 <script src="Themes/js-image-slider.js" type="text/javascript"></script>
  <link href="Css/GridViewStyles.css" rel="stylesheet" type="text/css" />
  <link href="Css/GridViewStyles1.css" rel="stylesheet" type="text/css" />
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
                    <asp:SiteMapDataSource ID="SiteMapDataSource1" SiteMapProvider="Menu1Sitemap"  runat="server" />
                    <asp:SiteMapDataSource ID="SiteMapDataSource2" SiteMapProvider="Menu2Sitemap" runat="server" />
                        <asp:DropDownList ID="DropDownList3" runat="server" Visible="False">
                        </asp:DropDownList>

<div class="row">

                <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Dashboard</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->
            <div class="row">
                <div class="col-lg-3 col-md-6">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-xs-3">
                                    <i class="fa fa-pencil-square-o fa-5x"></i>
                                </div>
                                <div class="col-xs-9 text-right">
                                    <div class="huge">
                                        <asp:Label ID="lbl_leave" runat="server" Text=""></asp:Label></div>
                                    <div>New Leave Requests!</div>
                                </div>
                            </div>
                        </div>
                        <a href="#">
                            <div class="panel-footer">
                               <a href="<%# ResolveUrl("~/Hrms_Attendance/LeaveDetails.aspx") %>"><span class="pull-left">View Details</span></a>
                                <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                <div class="clearfix"></div>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6">
                    <div class="panel panel-green">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-xs-3">
                                    <i class="fa fa-tasks fa-5x"></i>
                                </div>
                                <div class="col-xs-9 text-right">
                                    <div class="huge"><asp:Label ID="lbl_holiday" runat="server" Text=""></asp:Label></div>
                                    <div>Holidays!</div>
                                </div>
                            </div>
                        </div>
                        <a href="#">
                            <div class="panel-footer">
                                <a href="<%# ResolveUrl("~/Hrms_Master/Leave/Holiday.aspx") %>"><span class="pull-left">View Details</span></a>
                                <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                <div class="clearfix"></div>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6">
                    <div class="panel panel-yellow">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-xs-3">
                                    <i class="fa fa-users fa-5x"></i>
                                </div>
                                <div class="col-xs-9 text-right">
                                    <div class="huge">124</div>
                                    <div>No. of Present</div>
                                </div>
                            </div>
                        </div>
                        <a href="#">
                            <div class="panel-footer">
                                <span class="pull-left">View Details</span>  &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp;  &nbsp; 
                                 <asp:ImageButton ID="ImageButton2" runat="server" Height="16px" ImageUrl="~/Images/Arrow_Yelloe.png" Width="15px" />
                                <%--<span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>--%>
                                <div class="clearfix"></div>
                            </div>
                        </a>
                        <%--<span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>--%>
                        <div class="clearfix">
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6">
                    <div class="panel panel-red">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-xs-3">
                                    <i class="fa fa-users fa-5x"></i>
                                </div>
                                <div class="col-xs-9 text-right">
                                    <div class="huge">
                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                        </div>
                                    <div>No. of Absent</div>
                                </div>
                            </div>
                        </div>
                        <a href="#">
                            <div class="panel-footer">
                                <span class="pull-left">View Details</span> &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; 
                                <asp:ImageButton ID="ImageButton1" runat="server" Height="16px" ImageUrl="~/Images/arrow_red.gif" Width="29px" />
                                <%--<span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>--%>
                                <div class="clearfix"></div>
                            </div>
                        </a>
                        </a>
                    </div>
                </div>
            </div>
              

           
 </div>


 <div class="row">
                <div class="col-lg-8">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <i class="fa fa-bar-chart-o fa-fw"></i> Employee Distribution by Department
                            <div class="pull-right">
                                <div class="btn-group">
                                    
                                    <ul class="dropdown-menu pull-right" role="menu">
                                        <li><a href="#">Action</a>
                                        </li>
                                        <li><a href="#">Another action</a>
                                        </li>
                                        <li><a href="#">Something else here</a>
                                        </li>
                                        <li class="divider"></li>
                                        <li><a href="#">Separated link</a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <!-- /.panel-heading --> 
                        <div class="panel-body">
                            <div class="panel panel-info panel-body" >
                                <asp:Chart ID="Chart2" runat="server" 
                                Palette="Excel" Width="600px">
                                                        
                                <Series>
                                    <asp:Series Name="Series1" XValueMember="v_Departmentname" 
                                        YValueMembers="Dep_Count" IsValueShownAsLabel="true"
                             Palette="Excel"  ChartType="Doughnut" LabelForeColor="#FF3300">
                                        <EmptyPointStyle AxisLabel="No Data Found.." />
                                    </asp:Series>
                                </Series>
                                <chartareas>
                                    <asp:ChartArea Name="ChartArea1" Area3DStyle-Enable3D="True">
                                       <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False"
                        WallWidth="0" IsClustered="False" Enable3D="True"></Area3DStyle>
                                    </asp:ChartArea>
                                                            
                                </chartareas>
                                <Legends>
                                <asp:Legend Name="Legend1" Alignment="Center"></asp:Legend>
                                </Legends> 

                            </asp:Chart>
                                                         
                                                         </div>
                        </div>
                        <!-- /.panel-body -->
                    </div>
                    <!-- /.panel -->
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <i class="fa fa-bar-chart-o fa-fw"></i> Attendance Chart
                            <div class="pull-right">
                                <div class="btn-group">
                                    <asp:TextBox CssClass="btn btn-default btn-xs dropdown-toggle"  id="txt_date" 
                                        runat="server" Width="100px" AutoPostBack="True" 
                                        ontextchanged="txt_date_TextChanged"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txt_date" runat="server" Format="dd/MM/yyyy" />
                                </div>
                            </div>
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body" style="font-family: 'Times New Roman', Times, serif; font-size: medium; font-weight: normal; font-style: normal; font-variant: inherit; text-transform: inherit">

                                        <div class="panel panel-info panel-body">

                                         <asp:Chart ID="Chart1" runat="server" BackImageAlignment="Center" Width="600px">
                                            <Series>
                                                <asp:Series Name="Series1" XValueMember="Status" YValueMembers="Numbers" 
                                                    Legend="Present" ChartType="Bar" IsXValueIndexed="True" 
                                                    Palette="Excel" Label="#VALX #VAL" LegendText="#LEGENDTEXT" 
                                                    YValuesPerPoint="6" BorderColor="Gray" ChartArea="ChartArea1">
                                                    <EmptyPointStyle AxisLabel="No Data Found.." />
                                                </asp:Series>
                                            </Series>
                                            <ChartAreas>
                                                <asp:ChartArea Name="ChartArea1">
                                                    <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="true"
                        WallWidth="0" IsClustered="False" Enable3D="true"></Area3DStyle>
                                                  <AxisY2></AxisY2>
                                                </asp:ChartArea>
                                            </ChartAreas>
                                        </asp:Chart>  
                                      
                                         </div>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <div class="col-lg-8">
                                    <div id="morris-bar-chart"></div>
                                </div>
                                <!-- /.col-lg-8 (nested) -->
                            <!-- /.row -->
                        </div>
                        <!-- /.panel-body -->
                    </div>
                    <!-- /.panel -->
                    <!-- /.panel -->
                    </div>
                <!-- /.col-lg-8 -->
                <div class="col-lg-4">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <i class="fa fa-bell fa-fw"></i> Today&#39;s Absentees List
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            
                            <div id="diva" runat="server" align="left" style="overflow: auto; height: 355px;">
                       <asp:GridView ID="Grid_attend" runat="server" AutoGenerateColumns="False"  CssClass="table table-bordered table-hover table-striped" Width="95%" DataKeyNames="emp_code" 
                                                        Height="290px" ShowHeader="False" ForeColor="#666666">
                                                <Columns>
                                                 <asp:TemplateField Visible="false">
                                                         
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

                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                               </Columns>
                                               </asp:GridView>
                                               </div>

                        </div>
                        <!-- /.panel-body -->
                    </div>
                    <!-- /.panel -->
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <i class="fa fa-bar-chart-o fa-fw"></i> Monthly Leave Chart
                        </div>
                        <div class="panel-body">
                            <div id="morris-donut-chart">
                                <asp:Chart ID="Chart3" runat="server" BackImageAlignment="Center" Palette="SemiTransparent">
                                            <Series>
                                                <asp:Series Name="Series1" XValueMember="Status" YValueMembers="Numbers" 
                                                    Legend="Legend1" IsXValueIndexed="True" 
                                                    Palette="Excel" Label="#VALX #VAL" LegendText="#LEGENDTEXT" 
                                                    YValuesPerPoint="6" BorderColor="Gray">
                                                    <EmptyPointStyle AxisLabel="No Data Found.." />
                                                </asp:Series>
                                            </Series>
                                            <ChartAreas>
                                                <asp:ChartArea Name="ChartArea1">
                                                    <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="true"
                                                     WallWidth="0" IsClustered="False" Enable3D="false"></Area3DStyle>
                                                </asp:ChartArea>
                                            </ChartAreas>
                                 </asp:Chart>
                            </div>
                        </div>
                        <!-- /.panel-body -->
                    </div>
                    <!-- /.panel -->
                    <!-- /.panel .chat-panel -->
                </div>
                <!-- /.col-lg-4 -->
            </div>
            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
