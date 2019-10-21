<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/HRMS.master" CodeFile="EsiReport.aspx.cs" Inherits="Hrms_Reports_EsiReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <title>ESI Reports</title>
    <link href="../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
     <style>
        .checkbox1 input[type="checkbox"] 
{ 
    margin-right: 5px; 
}
    </style>
    <script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>
    <script language="javascript" type="text/javascript">
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


        //    
        //   function testing()
        //   {
        //   
        //   var str_date="01/02/2008";
        //   
        //  str_date= convert_Tosqldate(str_date);
        //  
        //  alert(str_date);
        //   
        //   }
        //   
        //   
        //   
        //    function convert_Tosqldate(arg_date)
        // {
        // 
        //  var ret_date="";
        //  var s_day="";  
        //  var s_month="";
        //  var s_year="";
        // 
        //  s_day=arg_date.substring(0,2);
        //  s_month=arg_date.substring(3,5);
        //  s_year=arg_date.substring(6,10);
        //  
        //  ret_date=s_year+"/"+s_month+"/"+s_day;
        //  
        //  return ret_date;
        //  
        //  //12/12/2008
        //  //0123456789
        // 
        // }

        function fn_chkall(chkid, chklistid) {

            var chkBoxList = document.getElementById(chklistid);
            var chkBoxCount = chkBoxList.getElementsByTagName("input");

            if (document.getElementById(chkid).checked == true) {
                for (var i = 0; i < chkBoxCount.length; i++) {
                    chkBoxCount[i].checked = true;
                }
            }
            else {
                for (var i = 0; i < chkBoxCount.length; i++) {
                    chkBoxCount[i].checked = false;
                }

            }
        }
    </script>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <div><h3 class="page-header">ESI Report Generation</h3></div>
    <div align="right">
        &nbsp;<asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" CssClass="form-control"
                    OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged">
                </asp:DropDownList>
            <br />
                            </div>
    <%--Width="139px" --%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
                     <div>                                    
                         <table style="width: 100%">
                             <tr>
                                 <td valign="top" style="width: 45%">
                                     <table class="table table-striped table-bordered table-hover">
                                         <tr>
                                             <td>
                                                 
                                                 <asp:Label ID="lbl_error" runat="server" Font-Bold="True" Font-Names="Calibri" 
                                                     Font-Size="Medium" ForeColor="Red" Width="75%"></asp:Label>
                                                 
                                             </td>
                                         </tr>
                                         <tr>
                                             <td>
                                             <div id="diva" runat="server" align="left" class="checkbox1" style="overflow: auto; height: 250px;">
                                                 <asp:CheckBoxList ID="chk_Empcode" runat="server" 
                                                     class="table table-striped table-bordered table-hover" Width="90%">
                                                     
                                                 </asp:CheckBoxList>
                                                 </div>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td>
                                                 <input type="checkbox" id="chkall" runat="server" 
                                                     onclick="javascript: fn_chkall(this.id,'ctl00_ContentPlaceHolder1_chk_Empcode')" visible="True" />
                                                 Select All</td>
                                         </tr>
                                     </table>
                                 </td>
                                 <td valign="top">
                                     <div ID="morris-area-chart" align="center" style="width: 100%">
                                         <table align="center" class="table table-striped table-bordered table-hover" 
                                             style="width: 90%">
                                             <tr>
                                                 <td colspan="3">
                                                     <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Calibri" 
                                                         Font-Size="Medium" ForeColor="Red" Width="75%"></asp:Label>
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>
                                                     Department</td>
                                                 <td colspan="2">
                                                     <span style="color: #800000">
                                                     <asp:DropDownList ID="ddl_department" runat="server" AutoPostBack="True" class="form-control" onselectedindexchanged="ddl_department_SelectedIndexChanged">
                                                     </asp:DropDownList>
                                                     </span>
                                                 </td>
                                             </tr>
                                             
                                             
                                             <tr>
                                                 <td>Select Type <span style="color: #CC3300">*</span></td>
                                                 <td colspan="2">
                                                     <asp:DropDownList ID="ddl_type" runat="server" AutoPostBack="True" CssClass="form-control" onselectedindexchanged="ddl_type_SelectedIndexChanged">
                                                         <%--Width="139px" --%>
                                                         <asp:ListItem Value="0">Select Report</asp:ListItem>
                                                         <asp:ListItem Value="5">ExportEsi</asp:ListItem>
                                                         <asp:ListItem Value="1">EsiMonthly</asp:ListItem>
                                                         <asp:ListItem Value="4">EsiChallan</asp:ListItem>
                                                         <asp:ListItem Value="2">Form6</asp:ListItem>
                                                         <asp:ListItem Value="3">Form7</asp:ListItem>
                                                     </asp:DropDownList>
                                                 </td>
                                             </tr>
                                             
                                             
                                             <tr>
                                             <td>
                                                 <asp:Label ID="lbl_month" runat="server" Text="Month"></asp:Label>
                                                 <asp:Label ID="lbl_year" runat="server" Text="Year"></asp:Label>
                                                 <asp:Label ID="lbl_fromdate" runat="server" Text="FromDate"></asp:Label>
                                                 &nbsp;<span style="color: #CC3300">*</span></td>
                                             <td>
                                                  <asp:DropDownList ID="ddl_monthlist" runat="server" 
                                                      CssClass="form-control">
                                                      <asp:ListItem Value="1">January</asp:ListItem>
                                                      <asp:ListItem Value="2">February</asp:ListItem>
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
                                                  </asp:DropDownList>
                                             </td>
                                                 <td>
                                                     <asp:DropDownList ID="ddl_yearlist" runat="server" CssClass="form-control" >
                                                     </asp:DropDownList>
                                                 </td>
                                             </tr>
                                             <tr>
                                             <td>
                                                 <asp:Label ID="lbl_todate" runat="server" Text="To Date"></asp:Label>
                                             </td>
                                             <td>
                                                 <asp:DropDownList ID="ddl_tomonthlist" runat="server" 
                                                     CssClass="form-control">
                                                     <asp:ListItem Value="1">January</asp:ListItem>
                                                     <asp:ListItem Value="2">February</asp:ListItem>
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
                                                 </asp:DropDownList>
                                                
                                             </td>
                                                 <td>
                                                     <asp:DropDownList ID="ddl_toyearlist" runat="server" 
                                                         CssClass="form-control">
                                                     </asp:DropDownList>
                                                 </td>
                                             </tr>

                                             <tr>
                                                 <td style="color: #CC3300">
                                                     * Mandatory Fields</td>
                                                 <td colspan="2">
                                                     <asp:Button ID="btn_Report" runat="server" Text="View Report"  class="btn btn-success" OnClick="btn_Report_Click" />
                                                 </td>
                                             </tr>
                                         </table>
                                     </div>
                                 </td>
                             </tr>
                         </table>
                        
           </div>
    </ContentTemplate>
    </asp:UpdatePanel>

        </asp:Content>