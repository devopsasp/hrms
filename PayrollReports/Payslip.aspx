<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Payslip.aspx.cs" Inherits="PayrollReports_Default" Title="Payslip Reports" %>

    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <style>
        .checkbox1 input[type="checkbox"] 
{ 
    margin-right: 5px; 
}
    </style>
    <script language="javascript" type="text/javascript">
    function refresh_Click()
    {
        window.open("PayDB_Refresh.aspx", "popwindow", "toolbar=no,width=300,height=120,top=320,left=370,status=1");
    }
    function fn_chkall(chkid,chklistid)
    { 
       
        var chkBoxList = document.getElementById(chklistid);
        var chkBoxCount = chkBoxList.getElementsByTagName("input");

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
    <link href="../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div><h3 class="page-header">Payslip Report Generation</h3></div>
    <div align="right">
        &nbsp;<asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" CssClass="form-control"
                    OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged">
                </asp:DropDownList>
            <br />
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
                                                     <asp:Label ID="lbl_error" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="Medium" ForeColor="Red" Width="75%"></asp:Label>
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>
                                                     Department</td>
                                                 <td colspan="3">
                                                     <span style="color: #800000">
                                                     <asp:DropDownList ID="ddl_department" runat="server" AutoPostBack="True" 
                                                         class="form-control" 
                                                         onselectedindexchanged="ddl_department_SelectedIndexChanged">
                                                     </asp:DropDownList>
                                                     </span>
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>
                                                     Category</td>
                                                 <td colspan="3">
                                                     <span style="color: #800000">
                                                     <asp:DropDownList ID="ddl_category" runat="server" AutoPostBack="True" 
                                                         class="form-control" 
                                                         onselectedindexchanged="ddl_category_SelectedIndexChanged">
                                                     </asp:DropDownList>
                                                     </span>
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>
                                                     Report Type</td>
                                                 <td colspan="3">
                                                     <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" 
                                                         onselectedindexchanged="DropDownList1_SelectedIndexChanged" 
                                                         AutoPostBack="True">
                                                         <%--Width="127px" --%>
                                                         <asp:ListItem Value="0">Acquittance</asp:ListItem>
                                                         <asp:ListItem Value="1">Pay Bill</asp:ListItem>
                                                         <asp:ListItem Value="2">PaySlip</asp:ListItem>
                                                         <asp:ListItem Value="3">Pay Register</asp:ListItem>
                                                         <asp:ListItem Value="4">Bank Report</asp:ListItem>
                                                         <asp:ListItem Value="5">Salary Register</asp:ListItem>
                                                     </asp:DropDownList>
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>
                                                     Month</td>
                                                 <td>
                                                     <asp:DropDownList ID="ddl_monthlist" runat="server" CssClass="form-control">
                                                         <%--Width="127px" --%>
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
                                                     </asp:DropDownList>
                                                 </td>
                                                 <td>
                                                     <asp:Label ID="lbl_month" runat="server" Text="To Month" Visible="False"></asp:Label>
                                                 </td>
                                                 <td>
                                                     <asp:DropDownList ID="ddl_monthlist1" runat="server" CssClass="form-control" 
                                                        Visible="False">
                                                         <%--Width="127px" --%>
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
                                                     </asp:DropDownList>
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>
                                                     Year</td>
                                                 <td>
                                                     <asp:DropDownList ID="ddl_yearlist" runat="server" CssClass="form-control">
                                                         <%--Width="127px" --%>
                                                     </asp:DropDownList>
                                                 </td>
                                                 <td>
                                                     <asp:Label ID="lbl_year" runat="server" Text="To Year" Visible="False"></asp:Label> 
                                                     </td>
                                                 <td>
                                                     <asp:DropDownList ID="ddl_yearlist1" runat="server" CssClass="form-control" 
                                                         Visible="False">
                                                     </asp:DropDownList>
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>
                                                     &nbsp;</td>
                                                 <td colspan="3">
                                                     <asp:Button ID="btn_Report" runat="server" Text="View Report"  class="btn btn-success" OnClick="btn_Report_Click" />
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
                                                 
                                                 <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="Medium" ForeColor="Red" Width="75%"></asp:Label>
                                                 
                                             </td>
                                         </tr>
                                         <tr>
                                             <td>
                                             <div id="diva" runat="server" align="left" class="checkbox1" style="overflow: auto; height: 300px;">
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

    </asp:Content>
