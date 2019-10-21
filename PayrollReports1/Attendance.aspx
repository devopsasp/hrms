<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="Attendance.aspx.cs"  Inherits="PayrollReports_Default" Title="Attendance Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <style>
        .checkbox1 input[type="checkbox"] 
{ 
    margin-right: 5px; 
}
    </style>

     <script language="javascript">

         function CheckAll()
         {
             
             var intIndex = 0;
             var rowCount = document.getElementById('chk_Empcode').getElementsByTagName("input").length;
             for (i = 0; i < rowCount; i++) {
                 if (document.getElementById('chkall').checked == true) {
                     if (document.getElementById("chk_Empcode" + "_" + i)) {
                         if (document.getElementById("chk_Empcode" + "_" + i).disabled != true)
                             document.getElementById("chk_Empcode" + "_" + i).checked = true;
                     }
                 }
                 else {
                     if (document.getElementById("chk_Empcode" + "_" + i)) {
                         if (document.getElementById("chk_Empcode" + "_" + i).disabled != true)
                             document.getElementById("chk_Empcode" + "_" + i).checked = false;
                     }
                 }
             }

         }

         function UnCheckAll()
         {
             var intIndex = 0;
             var flag = 0;
             var rowCount = document.getElementById('chk_Empcode').getElementsByTagName("input").length;
             for (i = 0; i < rowCount; i++) {
                 if (document.getElementById("chk_Empcode" + "_" + i)) {
                     if (document.getElementById("chk_Empcode" + "_" + i).checked == true) {
                         flag = 1;
                     }
                     else {
                         flag = 0;
                         break;
                     }
                 }
             }
             if (flag == 0)
                 document.getElementById('chkall').checked = false;
             else
                 document.getElementById('chkall').checked = true;
         }


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


    <script language="javascript" type="text/javascript">

        function ValidateModuleList(source, args) {
            var chkListModules = document.getElementById('<%= chk_Empcode.ClientID %>');
        var chkListinputs = chkListModules.getElementsByTagName("input");
        for (var i = 0; i < chkListinputs.length; i++) {
            if (chkListinputs[i].checked) {
                args.IsValid = true;
                return;
            }
        }
        args.IsValid = false;
    }


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
<%--     <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
     <script type="text/javascript">
         $(function () {
             $("[id*=chkAll]").bind("click", function () {
                 if ($(this).is(":checked")) {
                     $("[id*=chk_Empcode] input").attr("checked", "checked");
                 } else {
                     $("[id*=chk_Empcode] input").removeAttr("checked");
                 }
             });
             $("[id*=chk_Empcode] input").bind("click", function () {
                 if ($("[id*=chk_Empcode] input:checked").length == $("[id*=chk_Empcode] input").length) {
                     $("[id*=chkAll]").attr("checked", "checked");
                 } else {
                     $("[id*=chkAll]").removeAttr("checked");
                 }
             });
         });
</script>--%>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    <div><h3 class="page-header">Attendance Report Generation</h3></div>
    <div align="right">
        &nbsp;<asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" CssClass="form-control"
                    OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged">
                </asp:DropDownList>
            <br />
                            </div>
    <%-- <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
                     <div>                                    
                         <table style="width: 100%">
                             <tr>
                                 <td style="width: 50%">
                                 <table align="center" class="table table-striped table-bordered table-hover" 
                                             style="width: 90%">
                                             <tr>
                                                 <td colspan="2">
                                                     <asp:Label ID="lbl_error" runat="server" Font-Bold="True" Font-Names="Calibri" 
                                                     Font-Size="Medium" ForeColor="Red" Width="75%"></asp:Label>
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>
                                                     Category</td>
                                                 <td>
                                                     <asp:DropDownList ID="ddl_category" runat="server" CssClass="form-control" 
                                                         onselectedindexchanged="ddl_category_SelectedIndexChanged" 
                                                         AutoPostBack="True" Width="50%">
                                                         <asp:ListItem Value="0">Select</asp:ListItem>
                                                         <asp:ListItem Value="1">Staff</asp:ListItem>
                                                         <asp:ListItem Value="2">Student</asp:ListItem>
                                                     </asp:DropDownList>
                                                 </td>
                                             </tr>
                                             
                                             
                                             <tr>
                                             <td>Course</td>
                                             <td>
                                                  <asp:DropDownList class="form-control" ID="ddl_Courselist" Width="50%" 
                                                    DataSourceID="SqlDSCourse" runat="server" DataTextField="CourseName" 
                                                    DataValueField="CourseName">
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="SqlDSCourse" runat="server" 
                                                    ConnectionString="<%$ ConnectionStrings:connectionstring %>" 
                                                    SelectCommand="SELECT [CourseName] FROM [Student_Course] WHERE ([pn_BranchID] = @pn_BranchID) order by pn_CourseID Asc">
                                                    <SelectParameters>
                                                        <asp:SessionParameter Name="pn_BranchID" SessionField="Login_Temp_BranchID" 
                                                            Type="Int32" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                             </td>
                                             </tr>
                                             <tr>
                                             <td>
                                                Department
                                             </td>
                                             <td>
                                                 <asp:DropDownList class="form-control" ID="ddl_Departmentlist" 
                                                     Width="50%" runat="server" AutoPostBack="true"
                                                    onselectedindexchanged="ddl_Departmentlist_SelectedIndexChanged">
                                                    <asp:ListItem>All</asp:ListItem>
                                                </asp:DropDownList>
                                             </td>
                                             </tr>

                                             <tr>
                                             <td>Current Year</td>
                                             <td>
                                                 <asp:DropDownList ID="ddl_CurrentYearlist" runat="server" 
                                                     CssClass="form-control" Autopostback="true"
                                                     onselectedindexchanged="ddl_CurrentYearlist_SelectedIndexChanged" 
                                                     Width="50%">
                                                     <asp:ListItem Value="0">select</asp:ListItem>
                                                  <asp:ListItem Value="1">1</asp:ListItem>
                                                 <asp:ListItem Value="2">2</asp:ListItem>
                                                 <asp:ListItem Value="3">3</asp:ListItem>
                                                 <asp:ListItem Value="4">4</asp:ListItem>
                                                 </asp:DropDownList>
                                             </td>
                                             </tr>
                                             <tr>
                                                 <td>
                                                     Report Type</td>
                                                 <td>
                                                 <div style="float:left">
                                                     <asp:DropDownList ID="ddl_report" runat="server" CssClass="form-control" 
                                                          AutoPostBack="True" 
                                                         onselectedindexchanged="ddl_report_SelectedIndexChanged" >
                                                         <asp:ListItem Value="0">Select</asp:ListItem>
                                                         <asp:ListItem Value="1">Daily Attendance</asp:ListItem>
                                                         <asp:ListItem Value="2">Morning Attendance</asp:ListItem>
                                                         <asp:ListItem Value="3">Muster Roll</asp:ListItem>
                                                         <asp:ListItem Value="4">Present</asp:ListItem>
                                                         <asp:ListItem Value="5">Absent</asp:ListItem>
                                                         <asp:ListItem Value="6">Late in</asp:ListItem>
                                                         <asp:ListItem Value="7">Leave</asp:ListItem>
                                                         <asp:ListItem Value="8">On Duty</asp:ListItem>
                                                         <asp:ListItem Value="9">Missing Staff</asp:ListItem>
                                                         <asp:ListItem Value="10">Consolidate</asp:ListItem>
                                                     </asp:DropDownList>
                                                     </div>
                                                     <div align ="center" style="float:none">
                                                     <asp:RequiredFieldValidator ID="Reportchk" runat="server" ControlToValidate ="ddl_report" InitialValue="0" ErrorMessage="Select Report Type"></asp:RequiredFieldValidator>
                                                     </div>
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>
                                                     Date</td>
                                                 <td>
                                                 <div style=" width:150px; float:left;">
                                                     <asp:TextBox ID="txt_date" runat="server" CssClass="form-control" Width="150px"></asp:TextBox>
                                                     <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_date" Format="dd/MM/yyyy" >
                                                     </asp:CalendarExtender>
                                                     
                                                     </div>
                                                     <div style=" width:25px; float:left;  margin-left:10px; margin-top:3px;">                                                
                                                    <asp:Image ID="Image1" runat="server"  
                                                        Text="" Width="25px" 
                                                        ImageUrl="~/Images/calendaricon.png" />
                                                         
                                                </div>
                                                     <div align ="center" style="float:none">
                                                     <asp:RequiredFieldValidator ID="fdatechk" runat="server" ControlToValidate ="txt_date" ErrorMessage="Choose date"></asp:RequiredFieldValidator>
                                                     </div>
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>
                                                     To Date</td>
                                                 <td>
                                                 <div style=" width:150px; float:left;">
                                                     <asp:TextBox ID="txt_tdate" runat="server" CssClass="form-control" 
                                                         Width="150px"></asp:TextBox>
                                                     <asp:CalendarExtender ID="txt_tdate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txt_tdate">
                                                     </asp:CalendarExtender>
                                                     </div>
                                                      <div style=" width:25px; float:left;  margin-left:10px; margin-top:3px;">
                                                     <asp:Image ID="Image2" runat="server" Text="" Width="25px" ImageUrl="~/Images/calendaricon.png" />
                                                </div>
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>
                                                     &nbsp;</td>
                                                 <td>
                                                     <asp:Button ID="btn_Report" runat="server" Text="View Report"  class="btn btn-success" OnClick="btn_Report_Click" />
                                                 </td>
                                             </tr>
                                         </table>
                                     
                                 </td>
                                 <td>
                                     <div ID="morris-area-chart" align="center" style="width: 100%">
                                         
                                         <table class="table table-striped table-bordered table-hover">
                                         <tr>
                                             <td>
                                                 
                                                 <asp:CustomValidator runat="server" ID="cvmodulelist" ClientValidationFunction="ValidateModuleList"
                                                    ErrorMessage="Please Select Employee" ></asp:CustomValidator>
                                                 
                                             </td>
                                         </tr>
                                         <tr>
                                             <td>
                                             <div id="diva" runat="server" align="left" class="checkbox1" style="overflow: auto; height: 340px;">
                                                 <asp:CheckBoxList ID="chk_Empcode" onclick="javascript: UnCheckAll()" runat="server" class="table table-striped table-bordered table-hover" Width="90%">
                                                     
                                                 </asp:CheckBoxList>
                                                 </div>
                                                 
                                             </td>
                                         </tr>
                                         <tr>
                                             <td>

                                              <%--<asp:CheckBox ID = "chkAll" runat="server" Text = "Select All" />--%>
                                                 <input type="checkbox" id="chkall" runat="server" 
                                                     onclick="javascript: fn_chkall(this.id, 'ctl00_ContentPlaceHolder1_chk_Empcode')" visible="True" />
                                                 Select All</td>
                                         </tr>
                                     </table>


                                     </div>
                                 </td>
                             </tr>
                         </table>
                        
           </div>
    </ContentTemplate>
    </asp:UpdatePanel>

   <table width="100%" cellpadding="0" cellspacing="0" id="tbl_attreport" runat="server">
        <tr valign="top">
            <td class="tdComposeHeader" valign="top" align="right">
                <input type="hidden" id="ddl_selrep" runat="server" /></td>
        </tr>
    </table>
</asp:Content>

