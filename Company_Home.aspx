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
    .auto-style1 {
        height: 134px;
        margin-left: -15px;
        margin-right: -15px;
    }
    .auto-style2 {
        width: 234px;
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

    function check_uncheck(Val) {
        var ValChecked = Val.checked;
        var ValId = Val.id;
        var frm = document.forms[0];
        for (i = 0; i < frm.length; i++) {
            if (this != null) {
                if (ValId.indexOf('CheckAll') != - 1) {
                    if (ValChecked)
                        frm.elements[i].checked = true;
                    else
                        frm.elements[i].checked = false;
                }
                else if (ValId.indexOf('deleteRec') != - 1) {
                    if (frm.elements[i].checked == false)
                        frm.elements[1].checked = false;
                }
            }
        }
    }

    function confirmMsg(frm) {
        // loop through all elements
        for (i = 0; i < frm.length; i++) {
            // Look for our checkboxes only
            if (frm.elements[i].name.indexOf("deleteRec") != - 1) {
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

<%--<div class="row">

<%--                <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Dashboard</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->
            <div class="row">
                <div class="col-lg-3 col-md-6">
                    <div class="panel panel-primary">
                        <%--<div class="panel-heading">
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
                        </div>--%>
                       <%-- <a href="#">
                            <div class="panel-footer">
                               <a href="<%# ResolveUrl("~/Hrms_Attendance/LeaveDetails.aspx") %>"><span class="pull-left">View Details</span></a>
                                <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                <div class="clearfix"></div>
                            </div>
                        </a>--%>
                   <%-- </div>
                </div>--%>
         <%--       <div class="col-lg-3 col-md-6">
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
                </div>--%>
              <%--  <div class="col-lg-3 col-md-6">
                    <div class="panel panel-yellow">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-xs-3">
                                    <i class="fa fa-users fa-5x"></i>
                                </div>
                                <div class="col-xs-9 text-right">
                                    <div class="huge">
                                        <asp:Label ID="Label1" runat="server" ></asp:Label>

                                    </div>
                                    <div>No. of Present</div>
                                </div>
                            </div>
                        </div>
                        <a href="#">
                            <div class="panel-footer">
                                <span class="pull-left">View Details</span>
                                <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                <div class="clearfix"></div>
                            </div>
                        </a>
                    </div>
                </div>--%>
    <%--            <div class="col-lg-3 col-md-6">
                    <div class="panel panel-red">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-xs-3">
                                    <i class="fa fa-users fa-5x"></i>
                                </div>
                                <div class="col-xs-9 text-right">
                                    <div class="huge">
                                        <asp:Label ID="Label2" runat="server" ></asp:Label>

                                    </div>
                                    <div>No. of Absent</div>
                                </div>
                            </div>
                        </div>
                        <a href="#">
                            <div class="panel-footer">
                                <span class="pull-left">View Details</span>
                                <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                <div class="clearfix"></div>
                            </div>
                        </a>
                    </div>
                </div>--%>
           <%-- </div>
              

           
 </div>


 <div class="row">
                <div class="col-lg-8">
                    <div class="panel panel-default">--%>
                        <%--<div class="panel-heading">
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
                        </div>--%>
                        <!-- /.panel-heading -->
                       <%-- <div class="panel-body">
                            <div class="panel panel-info panel-body" >--%>
                                <%--<asp:Chart ID="Chart2" runat="server" 
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

                            </asp:Chart>--%>
                                                         
                    <%--                                     </div>
                        </div>
                        <!-- /.panel-body -->
                    </div>
                    <!-- /.panel -->
                    <div class="panel panel-default">--%>
                        <%--<div class="panel-heading">
                            <i class="fa fa-bar-chart-o fa-fw"></i> Attendance Chart
                            <div class="pull-right">
                                <div class="btn-group">
                                    <asp:TextBox CssClass="btn btn-default btn-xs dropdown-toggle"  id="txt_date" 
                                        runat="server" Width="100px" AutoPostBack="True" 
                                        ontextchanged="txt_date_TextChanged"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txt_date" runat="server" Format="dd/MM/yyyy" />
                                </div>
                            </div>
                        </div>--%>
                        <!-- /.panel-heading -->
                        <%--<div class="panel-body">

                                        <div class="panel panel-info panel-body">--%>

                                     <%--    <asp:Chart ID="Chart1" runat="server" BackImageAlignment="Center" Width="600px">
                                            <Series>
                                                <asp:Series Name="Series1" XValueMember="Status" YValueMembers="Numbers" 
                                                    Legend="Legend1" ChartType="Bar" IsXValueIndexed="True" 
                                                    Palette="Excel" Label="#VALX #VAL" LegendText="#LEGENDTEXT" 
                                                    YValuesPerPoint="6" BorderColor="Gray">
                                                    <EmptyPointStyle AxisLabel="No Data Found.." />
                                                </asp:Series>
                                            </Series>
                                            <ChartAreas>
                                                <asp:ChartArea Name="ChartArea1">
                                                    <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="true"
                        WallWidth="0" IsClustered="False" Enable3D="true"></Area3DStyle>
                                                </asp:ChartArea>
                                            </ChartAreas>
                                        </asp:Chart>  
                                   </div>

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
                        <%--<div class="panel-heading">
                            <i class="fa fa-bell fa-fw"></i> Today&#39;s Absentees List
                        </div>--%>
                        <!-- /.panel-heading -->
   <%--                     <div class="panel-body">
                            
                            <div id="diva" runat="server" align="left" style="overflow: auto; height: 355px;">
                       <asp:GridView ID="Grid_attend" runat="server" AutoGenerateColumns="False"  CssClass="table table-bordered table-hover table-striped" Width="95%" DataKeyNames="emp_code" 
                                                        Height="290px" ShowHeader="False" ForeColor="#666666">
                                                <Columns>
                                                 <asp:TemplateField Visible="false">
                                                         
                                                    <ItemTemplate>
                                                        <asp:Label ID="lnk_leave_list" runat="server" Text='<%#Eval("emp_code")%>'></asp:Label> 
                                                            <%--<asp:Label ID="lnk_leave_list" runat="server" Text='<%# Eval("leave") %>'></asp:Label> --%>
                                                          <%--  <asp:LinkButton ID="LinkButton1" runat="server"   
                                                                CommandName="cmd"  Text='<%#Eval("emp_name")%>' ForeColor="#666666"></asp:LinkButton>
                                                        
                                                    </ItemTemplate>
        --%>

<%--                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField>    
                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="lnk_leave_list" runat="server" Text='<%# Eval("leave") %>'></asp:Label> --%>
                                                     <%--       <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="#666666"  CommandName="cmd"  Text='<%#Eval("emp_name")%>'></asp:LinkButton>
                                                        <asp:Label ID="lnk_leave_list" runat="server" Text='<%#Eval("emp_code")%>'></asp:Label> 
                                                    
                                                        </ItemTemplate>

                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                               </Columns>
                                               </asp:GridView>
                                               </div>

                        </div>
                        <!-- /.panel-body -->--%>
                   <%-- </div>
                    <!-- /.panel -->
                    <div class="panel panel-default">--%>
                        <%--<div class="panel-heading">
                            <i class="fa fa-bar-chart-o fa-fw"></i> Monthly Leave Chart
                        </div>--%>
                 <%--       <div class="panel-body">
                            <div id="morris-donut-chart">
                            --%>    <%--<asp:Chart ID="Chart3" runat="server" BackImageAlignment="Center">
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
                                 </asp:Chart>--%>
                          <%--  </div>
                        </div>
                        <!-- /.panel-body -->
                    </div>
                    <!-- /.panel -->
                    <!-- /.panel .chat-panel -->
                </div>
                <!-- /.col-lg-4 -->
            </div>--%>
                   
                    <br />
                   <div class="auto-style1">

                       <br />
                       <div class="row">
                           <table style="padding:20px">
                               <tr>
                                   <td style="padding:20px">
                                       &nbsp;<asp:Panel ID="Panel1" runat="server" BackColor="#3399FF" Height="99px" Width="318px">
                                           &nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="nofd" runat="server" Text="Number of Department" Font-Bold="True" Font-Size="X-Large"></asp:Label>
                                           <br />
                                           &nbsp;&nbsp;&nbsp;
                                           <br />
                                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                           <asp:Label ID="txtnofd" runat="server" Text="Label"></asp:Label>
                                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                       </asp:Panel>
                                   </td>
                                   <td style="padding:20px" class="auto-style2">
                                       <asp:Panel ID="Panel2" runat="server" BackColor="#FF99FF" Height="99px" Width="300px">
                                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="nofe" runat="server" Font-Bold="True" Font-Size="X-Large" Text="Number of Employee"></asp:Label>
                                           <br />
                                           &nbsp;&nbsp;&nbsp;
                                           <br />
                                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                           <asp:Label ID="txtnofe" runat="server" Text="Label"></asp:Label>
                                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                           <br />
                                       </asp:Panel>
                                   </td>
                                   <td style="padding:20px">
                                     <asp:Panel ID="Panel3" runat="server" BackColor="#00FF99" Height="99px" Width="277px">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="prsnt" runat="server" Font-Bold="True" Font-Size="X-Large" Text="Number of present"></asp:Label>
                                           <br />
                                            &nbsp;&nbsp;&nbsp;
                                           <br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                           <asp:Label ID="txtprnt" runat="server" Text="Label"></asp:Label>
                                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                       </asp:Panel>
                                  
                                       </td>
                                   </tr>
                               <tr>
                                  <td style="padding:20px">
                                     <asp:Panel ID="Panel4" runat="server" BackColor="#00FF99" Height="99px" Width="316px">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="absnt" runat="server" Font-Bold="True" Font-Size="X-Large" Text="Number of absent"></asp:Label>
                                           <br />
                                            &nbsp;&nbsp;&nbsp;
                                           <br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                           <asp:Label ID="txtabsnt" runat="server" Text="Label"></asp:Label>
                                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                       </asp:Panel>
                                   </td>
                                    <td style="padding:20px" class="auto-style2">
                                     <asp:Panel ID="Panel5" runat="server" BackColor="#00FF99" Height="99px" Width="296px">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="lve" runat="server" Font-Bold="True" Font-Size="X-Large" Text="Number of leave"></asp:Label>
                                           <br />
                                            &nbsp;&nbsp;&nbsp;
                                           <br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                           <asp:Label ID="txtlve" runat="server" Text="Label"></asp:Label>
                                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                       </asp:Panel>
                                   </td>
                                        <td style="padding:20px">
                                     <asp:Panel ID="Panel6" runat="server" BackColor="#00FF99" Height="99px" Width="278px">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="hliday" runat="server" Font-Bold="True" Font-Size="X-Large" Text="Number of holiday"></asp:Label>
                                           <br />
                                            &nbsp;&nbsp;&nbsp;
                                           <br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                           <asp:Label ID="txthliday" runat="server" Text="Label"></asp:Label>
                                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                       </asp:Panel>
                                   </td>
                               </tr>
                             
                           </table>
                           <div>
                               <div>
                                    <asp:Chart ID="Chart2" runat="server" 
                                Palette="Excel" Width="600px" ImageLocation="~/delete_icon.gif">
                                                       
                                <Series>
                                    <asp:Series Name="Series1" XValueMember="v_Departmentname" 
                                        YValueMembers="Dep_Count" IsValueShownAsLabel="true"
                             Palette="Excel" LabelForeColor="#FF3300" YValuesPerPoint="4">
                                        <EmptyPointStyle AxisLabel="No Data Found.." />
                                    </asp:Series>
                                </Series>
                                <chartareas>
                                    <asp:ChartArea Name="ChartArea1" Area3DStyle-Enable3D="false">
                                       <%--<Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False"
                        WallWidth="0" IsClustered="False" Enable3D="True"></Area3DStyle>--%>
                                    </asp:ChartArea>
                                                            
                                </chartareas>
                                <Legends>
                                <asp:Legend Name="Legend1" Alignment="Center"></asp:Legend>
                                </Legends> 

                            </asp:Chart>

                               </div>
                           </div>

                       </div>
                   </div>
            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
