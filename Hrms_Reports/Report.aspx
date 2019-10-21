<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="TempTable" MasterPageFile="~/HRMS.master"
    Title="Reports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    <div><h3 class="page-header">Payslip Report Generation</h3></div>
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
                                                     <asp:Button ID="btn_Report" runat="server" Text="View Report"  class="btn btn-success" />
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td colspan="4">
                                                     Click
                                                     <asp:Button ID="Img1" runat="server" OnClientClick="refresh_Click()" 
                                                         class="btn btn-info" Text="Refresh" />
                                                     , If you modified any details.</td>
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
                                                 
                                                 </div>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td>
                                                 <input type="checkbox" id="Checkbox1" runat="server" 
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





        <table id="mainLayoutTable">
            <tr>
                <td width="70%">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr valign="top">
                            <td class="tdComposeHeader" valign="top" align="right">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 20%" valign="top">
                                            <table cellpadding="5px" cellspacing="1px" width="100%" border="1" bordercolor="#e5a81a">
                                                <tr>
                                                    <td align="center" class="QryTitlereports" style="width: 44%">
                                                        </td>
                                                    <td align="center" class="QryTitlereports" width="40%">
                                                        <asp:DropDownList ID="ddl_r_Type" runat="server" BackColor="White" Width="150px"
                                                            ForeColor="Black" AutoPostBack="True" OnSelectedIndexChanged="ddl_r_Type_SelectedIndexChanged"
                                                            CssClass="InputDDLStyle">
                                                            <asp:ListItem Value="0">Select Report</asp:ListItem>
                                                            <asp:ListItem Value="1">General</asp:ListItem>
                                                            <asp:ListItem Value="2">Qualification</asp:ListItem>
                                                            <asp:ListItem Value="3">Skills</asp:ListItem>
                                                            <asp:ListItem Value="4">Work Experience</asp:ListItem>
                                                            <asp:ListItem Value="5">Training</asp:ListItem>
                                                            <asp:ListItem Value="6">Leave</asp:ListItem>
                                                            <asp:ListItem Value="7">Appraisal</asp:ListItem>
                                                            <asp:ListItem Value="8">Earnings</asp:ListItem>
                                                            <asp:ListItem Value="9">Deductions</asp:ListItem>
                                                            <asp:ListItem Value="10">Strength</asp:ListItem>
                                                        </asp:DropDownList></td>
                                                </tr>
                                                <tr id="row_strngth" runat="server">
                                                    <td class="TextStyle">
                                                        Select Option</td>
                                                    <td>
                                                        <asp:DropDownList CssClass="InputDDLStyle" ID="ddl_masters" runat="server" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddl_masters_SelectedIndexChanged">
                                                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Department" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Division" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="projectsite" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="Designation" Value="4"></asp:ListItem>
                                                            <asp:ListItem Text="Grade" Value="5"></asp:ListItem>
                                                            <asp:ListItem Text="Category" Value="6"></asp:ListItem>
                                                            <asp:ListItem Text="JobStatus" Value="7"></asp:ListItem>
                                                            <asp:ListItem Text="Shift" Value="8"></asp:ListItem>
                                                            <asp:ListItem Text="Level" Value="9"></asp:ListItem>
                                                        </asp:DropDownList></td>
                                                </tr>
                                                <tr>
                                                    <td runat="server" id="div_chk_Empcode" height="30" valign="top" style="width: 44%"
                                                        align="center">
                                                        <div class="qrychkbox_big" style="height: 230px; left: 0px; top: 0px;">
                                                            <asp:CheckBoxList Height="200px" ID="chk_Empcode" runat="server" CssClass="InputDefaultStyle1"
                                                                Width="90%">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                        <input type="checkbox" id="chkall" runat="server" onclick="javascript: fn_chkall(this.id,'chk_Empcode')" />Select
                                                        All Employees
                                                    </td>
                                                    <td runat="server" id="div_chk_Master" valign="top" align="center" width="40%">
                                                        <div class="qrychkbox_big" style="height: 230px; left: 0px; top: 0px;" align="center">
                                                            <asp:CheckBoxList ID="chk_Master" runat="server" CssClass="InputDefaultStyle1" Height="200px"
                                                                Width="90%">
                                                            </asp:CheckBoxList></div>
                                                        <input type="checkbox" id="chk_all_master" runat="server" onclick="javascript: fn_chkall(this.id,'chk_Master')" />Select
                                                        All</td>
                                                </tr>
                                                <tr id="row_leave_year" runat="server">
                                                    <td class="TextStyle">
                                                        Select Year</td>
                                                    <td align="center" valign="top" width="40%">
                                                        <asp:DropDownList ID="ddl_year" runat="server">
                                                        </asp:DropDownList></td>
                                                </tr>
                                                <tr id="row_leave_month" runat="server">
                                                    <td class="TextStyle">
                                                        Select Month</td>
                                                    <td align="center" width="40%">
                                                        <div runat="server" id="div_chk_month" class="qrychkbox_big" style="height: 100px;
                                                            left: 0px; top: 0px; width: 150px;" align="center">
                                                            <asp:CheckBoxList ID="chk_month" runat="server" Width="100%">
                                                                <asp:ListItem Value="1">January</asp:ListItem>
                                                                <asp:ListItem Value="2">Febraury</asp:ListItem>
                                                                <asp:ListItem Value="3">March</asp:ListItem>
                                                                <asp:ListItem Value="4">April</asp:ListItem>
                                                                <asp:ListItem Value="5">May</asp:ListItem>
                                                                <asp:ListItem Value="6">June</asp:ListItem>
                                                                <asp:ListItem Value="7">July</asp:ListItem>
                                                                <asp:ListItem Value="8">August</asp:ListItem>
                                                                <asp:ListItem Value="9">September</asp:ListItem>
                                                                <asp:ListItem Value="10">October</asp:ListItem>
                                                                <asp:ListItem Value="11">November</asp:ListItem>
                                                                <asp:ListItem Value="12">December</asp:ListItem>
                                                            </asp:CheckBoxList></div>
                                                        <input type="checkbox" id="chkall_months" runat="server" onclick="javascript: fn_chkall(this.id,'chk_month')" />Select
                                                        All</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TextStyle" align="center">
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td align="center" style="width: 20%; height: 20px" valign="middle">
                                                            <asp:ImageButton ID="btn_Report1" runat="server" ImageUrl="../Images/Show_Report.jpg"
                                                                OnClick="btn_Report_Click" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td id="col_refresh" runat="server" align="center" style="width: 20%; height: 15px"
                                                            valign="middle" class="TextStyle">
                                                            Click
                                                            <img id="Img11" src="../Images/Refresh.jpg" runat="server" onclick="refresh_Click()" />,
                                                            if you have modified any details.</td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            Click
                                            <asp:ImageButton ID="btn_Query" runat="server" ImageUrl="../Images/Query-Builder.jpg"
                                                OnClick="btn_Query_Click" />
                                            to search employees based on Masters.</td>
                                    </tr>
                                </table>
                                <input type="hidden" id="ddl_selrep" runat="server" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="99%" align="center">
                        <tr valign="top">
                            <td align="right" width="100%" background="../Images/bg.jpg" height="15px">
                                Powered by <a href="http://www.epayindia.com" target="_blank"><font color="#0099ff"
                                    size="4px" title="Click to Know more about ePay Solutions">ePay</font></a></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
</asp:Content>
