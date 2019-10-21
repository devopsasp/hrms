<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Authentications.aspx.cs" Inherits="Hrms_Company_Default" Title="User Authentications" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../datecheck.js"></script>

     <script language="javascript" type="text/javascript">
    

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
    
    function fn_chk(chkid,chklistid)
    { 
       
        var chkBoxList = document.getElementById(chkid);
        var chkBoxCount= chkBoxList.getElementsByTagName("input");

    
            for(var i=0;i<chkBoxCount.length;i++) 
            {
                if(chkBoxCount[i].checked = false)
                {
                    document.getElementById(chklistid).checked==true
                }
            }
    }    


function Checkbox4_onclick() {

}

    </script>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="row">
                <div class="col-lg-12">
                    <h2 class="page-header">User Authentication</h2>
                </div>
                <!-- /.col-lg-12 -->
            </div>
    <div class="panel panel-default">
                        <div class="panel-heading">
                            Settings
                            <div class="pull-right">
                                <asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True"  
                                    CssClass="form-control" 
                                    onselectedindexchanged="ddl_Branch_SelectedIndexChanged" Visible="False" >
                            </asp:DropDownList>
                            </div>
                        </div>
                        <!-- /.panel-heading -->
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                            <div class="divWaiting">
                            
                            <asp:Image ID="imgWait" runat="server" ImageAlign="Middle" 
                                    ImageUrl="~/Images/loading2.gif" Height="100px" Width="100px" />
                               
                            </div>
                            </ProgressTemplate>
                            </asp:UpdateProgress>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>

                        <div class="panel-body">
                            <div align="center" id="morris-area-chart" style="width: 100%">
                
                               <table class="table table-striped table-bordered table-hover">
                                    <tbody>
                                        <tr>
                                            <td colspan="4">
                                                <asp:CheckBoxList ID="ddl_Employee" runat="server" Width="100%"  RepeatColumns="3" RepeatDirection="Horizontal">
                                                </asp:CheckBoxList>
                                                <asp:Button ID="btn_details" runat="server" class="btn btn-warning" OnClick="btn_details_Click" Text="Details"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td >From Date</td>
                                            <td>
                                                <input class="form-control"  runat="server" id="txtFromdate" 
                                onkeyup="fn_date(event,this.id);" maxlength="10" style="font-family: Calibri"/>
                                            </td>
                                            <td>
                                                To Date</td>
                                            <td>
                                                <input class="form-control"  runat="server" id="txtTodate" 
                                onkeyup="fn_date(event,this.id);" maxlength="10" style="font-family: Calibri"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="4" >
                                            
                                             <table width="100%">
        
                    <tr>
                        <td width="33%" align="center" 
                            
                            style=" font-family: Calibri;  font-weight: bold;">
                            <b>Setup</b></td>
                        <td align="center" 
                            style=" font-family: Calibri;  width: 29%;">
                            <b>Settings</b></td>
                        <td align="center"  style=" font-family: Calibri; width:34%;">
                            <b>Employee</b></td>
                    </tr>
                    <tr>
                        <td width="33%" align="center" height="5%">
                            <input type="checkbox" id="chkall_Home" runat="server" 
                                onclick="javascript: fn_chkall(this.id,'ctl00_ContentPlaceHolder1_chk_home')" 
                                style="font-family: Calibri" /><span style="font-family: Calibri">Select All
                                                   
                            </span>
                                                   
                            </td>
                        <td align="center" height="5%" style="width: 29%">
                           <input type="checkbox" id="chkall_Masters" runat="server" 
                                onclick="javascript: fn_chkall(this.id,'ctl00_ContentPlaceHolder1_chk_master')" 
                                style="font-family: Calibri" /><span style="font-family: Calibri">Select All
                            
                            </span>
                            
                            </td>
                        <td width="34%" align="center" height="5%">
                           <input type="checkbox" id="chkall_Employee" runat="server" 
                                onclick="javascript: fn_chkall(this.id,'ctl00_ContentPlaceHolder1_chk_employee')" 
                                style="font-family: Calibri" /><span style="font-family: Calibri">Select All
                            
                            </span>
                            
                           </td>
                    </tr>
                    <tr>
                        <td rowspan="2" width="33%" align="center" valign="top">
                            <div class="usr_rights_page">
                                <asp:CheckBoxList ID="chk_home" runat="server" Width="95%" 
                                   class="form-control">
                                    <asp:ListItem Value="1">Head Office</asp:ListItem>
                                    <asp:ListItem Value="2">Branch</asp:ListItem>
                                    <asp:ListItem Value="3">Time Card</asp:ListItem>
                                    <asp:ListItem Value="4">Shift Pattern</asp:ListItem>
                                    <asp:ListItem Value="5">Shift Balance</asp:ListItem>
                                    <asp:ListItem Value="6">Over Time Slab</asp:ListItem>

                                </asp:CheckBoxList></div>
                            <br />
                        </td>
                        <td rowspan="2" align="center" valign="top" style="width: 29%">
                            <div class="usr_rights_page" style="left: 0px; top: 0px">
                                <asp:CheckBoxList ID="chk_master" runat="server" Width="95%" 
                                   class="form-control">
                                    
                                    <asp:ListItem Value="7">Provident Fund</asp:ListItem>
                                    <asp:ListItem Value="8">Voluntary PF</asp:ListItem>
                                    <asp:ListItem Value="9">Employee State Insurance</asp:ListItem>
                                    <asp:ListItem Value="10">Prefessional Tax</asp:ListItem>
                                    <asp:ListItem Value="11">Bonus Slab</asp:ListItem>
                                    <asp:ListItem Value="12">Change Password</asp:ListItem>

                                    
                                </asp:CheckBoxList></div>
                                </td>
                            
                        <td rowspan="2" width="34%" align="center" valign="top">
                            <div class="usr_rights_page">
                                <asp:CheckBoxList ID="chk_employee" runat="server" Width="95%" 
                                    class="form-control">
                                    <asp:ListItem Value="18">Allowances</asp:ListItem>
                                    <asp:ListItem Value="19">Deductions</asp:ListItem>
                                    <asp:ListItem Value="20">Student Course Setup</asp:ListItem>
                                    <asp:ListItem Value="21">Student Master</asp:ListItem>
                                    <asp:ListItem Value="22">Student Acedemic Change</asp:ListItem>
                                    <asp:ListItem Value="23">Employee Setup</asp:ListItem>
                                    <asp:ListItem Value="24">Employee Position</asp:ListItem>
                                    <asp:ListItem Value="25">All Employee Basic</asp:ListItem>
                                    <asp:ListItem Value="26">All Employee Allowance</asp:ListItem>
                                    <asp:ListItem Value="27">All Employee Deduction</asp:ListItem>
                                    <asp:ListItem Value="28">Leave Setup</asp:ListItem>
                                    <asp:ListItem Value="29">Holiday Setup</asp:ListItem>
                                    <asp:ListItem Value="30">Leave Allocation</asp:ListItem>
                                    <asp:ListItem Value="31">Academic Setup</asp:ListItem>
                                    <asp:ListItem Value="32">Specialization</asp:ListItem>
                                    <asp:ListItem Value="33">Skills</asp:ListItem>
                                    <asp:ListItem Value="34">Loans</asp:ListItem>
                                    <asp:ListItem Value="35">Bank</asp:ListItem>
                                    
                                </asp:CheckBoxList></div>
                        </td>
                    </tr>
                    </table>
                               <table id="module2" visible="false" runat="server" style="width: 100%;">
                <tr>
                        <td width="33%" align="center"                
                            style=" font-family: Calibri;  font-weight: bold;">
                            <b>Task Section</b></td>
                        <td align="center" 
                            style=" font-family: Calibri;  width: 29%;">
                            <b>Actions&nbsp; Section</b></td>
                        <td align="center"
                          style="font-family: Calibri; width:34%;">
                            <b>Performance&nbsp; Section</b></td>
                    </tr>
                     <tr>
                        <td width="33%" align="center" height="5%">
                            <input type="checkbox" id="chkall_task" runat="server" 
                                onclick="javascript: fn_chkall(this.id,'ctl00_ContentPlaceHolder1_chk_task')" 
                                style="font-family: Calibri" /><span style="font-family: Calibri">Select All
                                                   
                            </span>
                                                   
                            </td>
                        <td align="center" height="5%" style="width: 29%">
                           <input type="checkbox" id="chkall_Action" runat="server" 
                                onclick="javascript: fn_chkall(this.id,'ctl00_ContentPlaceHolder1_chk_Action')" 
                                style="font-family: Calibri" /><span style="font-family: Calibri">Select All
                            
                            </span>
                            
                            </td>
                        <td width="34%" align="center" height="5%">
                           <input type="checkbox" id="chkall_performance" runat="server" 
                                onclick="javascript: fn_chkall(this.id,'ctl00_ContentPlaceHolder1_chk_performance')" 
                                style="font-family: Calibri" /><span style="font-family: Calibri">Select All
                            
                            </span>
                            
                           </td>
                    </tr>
                    <tr>
                        <td rowspan="3" width="33%" align="center" valign="top">
                            <div class="usr_rights_page">
                                <asp:CheckBoxList ID="chk_task" runat="server" Width="95%" 
                                    class="form-control" Font-Names="Calibri" Font-Size="Small">
                                    <asp:ListItem Value="43">Task Scheduler</asp:ListItem>
                                    <asp:ListItem Value="44">Online Test</asp:ListItem>
                                  <asp:ListItem Value="45">Reimbursement</asp:ListItem>
                                </asp:CheckBoxList></div>
                            <br />
                        </td>
                        <td rowspan="3" align="center" valign="top" style="width: 29%">
                            <div class="usr_rights_page" style="left: 0px; top: 0px">
                                <asp:CheckBoxList ID="chk_Action" runat="server" Width="95%" 
                                   class="form-control" Font-Names="Calibri" Font-Size="Small">
                                    <asp:ListItem Value="46">User Creation</asp:ListItem>
                                    <asp:ListItem Value="47">User Authentications</asp:ListItem>
                                    <asp:ListItem Value="48">Deletion</asp:ListItem>
                                    <asp:ListItem Value="49">Pasword Change</asp:ListItem>
                                    </asp:CheckBoxList></div>
                            </td>
                        <td rowspan="3" width="34%" align="center" valign="top">
                            <div class="usr_rights_page">
                                <asp:CheckBoxList ID="chk_performance" runat="server" Width="95%" 
                                    class="form-control" Font-Names="Calibri" Font-Size="Small">
                                    <asp:ListItem Value="50">Bonus</asp:ListItem>
                                    <asp:ListItem Value="51">Training</asp:ListItem>
                                    <asp:ListItem Value="52">Project Details</asp:ListItem>
                                    <asp:ListItem Value="53">Performance Apprisal</asp:ListItem>
                                    <asp:ListItem Value="54">Promotional Setup </asp:ListItem>
                                    <asp:ListItem Value="55">Annual Increment Setup</asp:ListItem>
                                   
                                </asp:CheckBoxList></div>
                        </td>
                    </tr>
                
                </table>
                    <table>
                    <tr>
                        <td align="center" 
                            style=" font-family: Calibri;  font-weight: bold; width: 378px;">
                            <b>Attendance Section</b></td>
                        <td align="center" style=" font-family: Calibri;  font-weight: bold; width: 337px;">
                            <b>PayRoll Section</b></td>
                        <td align="center" style="font-family: Calibri;  font-weight: bold; width: 381px;">
                            <b>Reports Section</b></td>
                   </tr>
                   <tr>
                    <td width="33%" align="center" height="5%">
                            <input type="checkbox" id="chkall_Attendance" runat="server" 
                                onclick="javascript: fn_chkall(this.id,'ctl00_ContentPlaceHolder1_chk_attendance')" 
                                style="font-family: Calibri" /><span style="font-family: Calibri">Select All

                            </span>

                            </td>
                            <td width="33%" align="center" height="5%">
                            <input type="checkbox" id="chkall_Payroll" runat="server" 
                                onclick="javascript: fn_chkall(this.id,'ctl00_ContentPlaceHolder1_chk_payroll')" 
                                style="font-family: Calibri" /><span style="font-family: Calibri">Select All
                                                   
                            </span>
                                                   
                            </td>
                            <td width="33%" align="center" height="5%">
                            <input type="checkbox" id="chkall_Reports" runat="server" 
                                onclick="javascript: fn_chkall(this.id,'ctl00_ContentPlaceHolder1_chk_reports')" 
                                style="font-family: Calibri" /><span style="font-family: Calibri">Select All
                                                   
                            </span>
                                                   
                            </td>

                   </tr>
                   <tr>
                   <td rowspan="2" width="34%" align="center" valign="top">
                            <div class="usr_rights_page">
                                <asp:CheckBoxList ID="chk_attendance" runat="server" Width="95%" 
                                    class="form-control" Font-Names="Calibri" Font-Size="Small">                                    
                                    <asp:ListItem Value="36">Data Download</asp:ListItem>
                                    <asp:ListItem Value="37">Daily Time Card</asp:ListItem>
                                    <asp:ListItem Value="38">Student Time Card</asp:ListItem>
                                    <asp:ListItem Value="39">Manual Attendance</asp:ListItem>
                                    <asp:ListItem Value="40">Permission</asp:ListItem>
                                    <asp:ListItem Value="41">Leave Entry</asp:ListItem>
                                    <asp:ListItem Value="42">Leave Details</asp:ListItem>
                                </asp:CheckBoxList></div>
                        </td>
                        <td rowspan="2" width="34%" align="center" valign="top">
                            <div class="usr_rights_page">
                                <asp:CheckBoxList ID="chk_payroll" runat="server" Width="95%" 
                                   class="form-control" Font-Names="Calibri" Font-Size="Small">                                    
                                    <asp:ListItem Value="43">Salary Period</asp:ListItem>
                                    <asp:ListItem Value="44">Employee Vs Allowance</asp:ListItem>
                                    <asp:ListItem Value="45">Employee Vs Deduction</asp:ListItem>
                                    <asp:ListItem Value="46">Loan Entry</asp:ListItem>
                                    <asp:ListItem Value="47">Loan PreClosure</asp:ListItem>
                                    <asp:ListItem Value="48">Loan Post</asp:ListItem>
                                     <asp:ListItem Value="49">Loan Cancel</asp:ListItem>
                                    <asp:ListItem Value="50">Pay Slip Processing</asp:ListItem>
                                    <asp:ListItem Value="51">PF Nominee</asp:ListItem>
                                    <asp:ListItem Value="52">Full & Final Settlement</asp:ListItem>
                                </asp:CheckBoxList></div>
                                <%--<asp:ImageButton ImageUrl="~/Images/Assign.png" onmouseover="this.src='../../Images/Assignover.png';" onmouseout="this.src='../../Images/Assign.png';" ID="btn_save" 
                                runat="server" OnClick="btn_save_Click" />--%>
                            <asp:Button ID="btn_save"  runat="server" OnClick="btn_save_Click" class="btn btn-success"  Text="Assign"/>
                                </td>
                       
                        <td rowspan="2" width="34%" align="center" valign="top">
                            <div class="usr_rights_page">
                                <asp:CheckBoxList ID="chk_reports" runat="server" Width="95%" 
                                    class="form-control" Font-Names="Calibri" Font-Size="Small">                                    
                                    <asp:ListItem Value="53">Employee Reports</asp:ListItem>
                                    <asp:ListItem Value="54">Attendance Reports</asp:ListItem> 
                                    <asp:ListItem Value="55">PF Reports</asp:ListItem> 
                                    <asp:ListItem Value="56">ESI Reports</asp:ListItem>    
                                    <asp:ListItem Value="57">PaySlip Reports</asp:ListItem>                                  
                                </asp:CheckBoxList></div>
                        </td>
                   </tr>
                    </table>
                                            
                                            </td>
                                        </tr>
                                    </tbody>
                                </table> 
                
                            </div>
                        </div>

                        </ContentTemplate>
                                </asp:UpdatePanel>
                        <!-- /.panel-body -->
                    </div>


   

</asp:Content>
