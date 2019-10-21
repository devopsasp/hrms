<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportSelection.aspx.cs" MasterPageFile="~/HRMS.master" Inherits="Hrms_Master_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


    
<%--<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
function refresh_Click()
{
    window.open("DB_Refresh.aspx", "popwindow", "toolbar=no,width=300,height=120,top=320,left=370,status=1");
}

function chk_empselect()
{
    
}

function fn_chkall(chkid,chklistid)
    { 
       
        var chkBoxList = document.getElementById(chklistid);
        var chkBoxCount= chkBoxList.getElementsByTagName("input");

       if(document.getElementById(chkid).checked==true)
       {       
            for(var i=0;i<chkBoxCount.length;i++) 
            {
                chkBoxCount[i].checked = true;
            }                
       }
       else
       {       
            for(var i=0;i<chkBoxCount.length;i++) 
            {
                chkBoxCount[i].checked = false;
            }               
       
       }             
       

    }       
    
   


    </script>

    <style type="text/css">
.menuItem
{
background-image : url(../Images/header1.gif);
background-repeat:repeat-x;
cursor : hand;
}
.menuItem1
{
background-image : url(../Images/header1.gif);
background-repeat:repeat-x;	
cursor : hand;
}

</style>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div><h3 class="page-header">Employee General Report</h3></div>
    <div align="right">
        &nbsp;<br />
                            &nbsp;&nbsp;</div>

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
                     <div>                                    
                         <table style="width: 100%">
                             <tr>
                                 <td style="width: 55%">
                                 <table align="center" class="table table-striped table-bordered table-hover" 
                                             style="width: 90%">
                                             <tr>
                                                 <td colspan="4">
                                                     <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Calibri" 
                                                         Font-Size="Medium" ForeColor="Red" Width="75%"></asp:Label>
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>
                                                     Department</td>
                                                 <td colspan="3">
                                                     &nbsp;</td>
                                             </tr>
                                             <tr>
                                                 <td>
                                                     Category</td>
                                                 <td colspan="3">
                                                     &nbsp;</td>
                                             </tr>
                                             <tr>
                                                 <td>
                                                     Report Type</td>
                                                 <td colspan="3">
                                                     &nbsp;</td>
                                             </tr>
                                             <tr>
                                                 <td>
                                                     Month</td>
                                                 <td>
                                                     &nbsp;</td>
                                                 <td>
                                                     <asp:Label ID="lbl_month" runat="server" Text="To Month" Visible="False"></asp:Label>
                                                 </td>
                                                 <td>
                                                     &nbsp;</td>
                                             </tr>
                                             <tr>
                                                 <td>
                                                     Year</td>
                                                 <td>
                                                     &nbsp;</td>
                                                 <td>
                                                     <asp:Label ID="lbl_year" runat="server" Text="To Year" Visible="False"></asp:Label> 
                                                     </td>
                                                 <td>
                                                     &nbsp;</td>
                                             </tr>
                                             <tr>
                                                 <td>
                                                     &nbsp;</td>
                                                 <td colspan="3">
                                                     &nbsp;</td>
                                             </tr>
                                             <tr>
                                                 <td colspan="4">
                                                     Click , If you modified any details.</td>
                                             </tr>
                                         </table>
                                     
                                 </td>
                                 <td>
                                    <table class="table table-striped table-bordered table-hover">
                                         <tr>
                                             <td>
                                                 
                                                 <asp:Label ID="lbl_error" runat="server" Font-Bold="True" Font-Names="Calibri" 
                                                     Font-Size="Medium" ForeColor="Red" Width="75%"></asp:Label>
                                                 
                                             </td>
                                         </tr>
                                         <tr>
                                             <td>
                                             <div id="diva" runat="server" align="left" style="overflow: auto; height: 300px;">
                                                 <asp:CheckBoxList ID="chk_Empcode" runat="server" CssClass="form-control" 
                                                     Width="90%" CellPadding="2" CellSpacing="2">
                                                     <asp:ListItem></asp:ListItem>
                                                 </asp:CheckBoxList>
                                                 </div>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td>
                                                 <input type="checkbox" id="chkall" runat="server" 
                                                     onclick="javascript: fn_chkall(this.id,'ctl00_ContentPlaceHolder1_chk_Empcode')" />
                                                 Select All</td>
                                         </tr>
                                     </table>     

                                 </td>
                             </tr>
                         </table>
                        
           </div>
    </ContentTemplate>
    </asp:UpdatePanel>




        <table align="center" id="mainLayoutTable">
            <tr>
                <td valign="top" width="10%">
                    <div id="contentLeft" style="height: 100%">
                        <div id="contentFolderList" style="height: 100%">
                            <div style="text-align: center; vertical-align: top">
  
                            </div>
                        </div>
                    </div>
                </td>
                <td width="70%" valign="top">
                    <table width="100%">
                        <tr>
                            <td style="width: 20%" valign="top">
                                <table cellpadding="5px" cellspacing="1px" width="100%" border="1" bordercolor="#e5a81a">
                                    <tr>
                                        <td align="center" class="QryTitlereports" colspan="2">
                                            <asp:DropDownList ID="ddl_reportselection" runat="server" OnSelectedIndexChanged="ddl_reportselection_SelectedIndexChanged"
                                                AutoPostBack="True">
                                                <asp:ListItem Text="Select Report" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Current Employees" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Old Employees" Value="2"></asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

    </asp:Content>