<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="PFReport.aspx.cs" Inherits="PayrollReports_Default" Title="PF Reports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <style>
        .checkbox1 input[type="checkbox"] 
{ 
    margin-right: 5px; 
}
    </style>
    <script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>
    <script language="javascript" type="text/javascript">
     function fn_date(event,txtid)
 {  
       var len;
       var txtvalue; 
       var bool_obj; 
       var i;    
      
       txtvalue= document.getElementById(txtid).value;
       txtlen=txtvalue.length;  
       
  if(event.keyCode!=8 && event.keyCode!=46 && event.keyCode!=35 && event.keyCode!=36 && event.keyCode!=37 && event.keyCode!=38 && event.keyCode!=39 && event.keyCode!=40)     
   {        
           if(txtlen!=0)
           {       
               bool_obj=fn_validate(txtlen,txtvalue);
                          
               if(bool_obj==true)
                 {
                      if(txtlen==2 || txtlen==5)
                      {
                      document.getElementById(txtid).value=txtvalue+"/";
                      }
                      else
                      {
                      document.getElementById(txtid).value=txtvalue;
                      }
                     
                 }
                 else
                 {            
                     
                   document.getElementById(txtid).value= txtvalue.substring(0,txtlen-1);              
                 
                 }                       
                 
               
            } 
            
    }                                 
 }
   
   function fn_validate(len,tval)
    {
    var str;
    
          switch(len)
           {
     
        case 1: if(tval<=3)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                  
                  
        case 2:               
                
                if(tval<=31 && tval>0)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
                
        case 3: str=tval.charAt(2);
        
                if(str=="/")
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
      case 4: str=tval.charAt(3);
        
                if(str<=1)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
      case 5: str=tval.substring(3,5); 
        
                if(str<=12 && str>0)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
       case 6: str=tval.charAt(5);
        
                if(str=="/")
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
        case 7: str=tval.charAt(6);
        
                if(str<=9 && str>0)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
        case 8: str=tval.substring(6,8);
        
                if(str>=18)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
        case 9: str=tval.charAt(8);
        
                if(str<=9)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
        case 10: str=tval.charAt(9);
        
                if(str<=9)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
                
        default :return false;   
                 break;
           }
        
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



    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    <div><h3 class="page-header">PF Report Generation</h3></div>
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
                                             <div id="diva" runat="server" class="checkbox1" align="left" style="overflow: auto; height: 250px;">
                                                 <%--<asp:Label ID="Label2" runat="server"  ForeColor="Red" Font-Size="Medium">No Employees Found</asp:Label>--%>
                                                 <asp:CheckBoxList ID="chk_Empcode"  runat="server" 
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
                                                         <asp:ListItem Value="8">Export ECR</asp:ListItem>
                                                         <asp:ListItem Value="1">PFMonthly</asp:ListItem>
                                                         <asp:ListItem Value="2">Form3A</asp:ListItem>
                                                         <asp:ListItem Value="3">Form6A</asp:ListItem>
                                                         <asp:ListItem Value="4">Form12A</asp:ListItem>
                                                         <asp:ListItem Value="5">PfChallan</asp:ListItem>
                                                         <asp:ListItem Value="6">Form5</asp:ListItem>
                                                         <asp:ListItem Value="7">Form10</asp:ListItem>
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

                                             <tr  id="row_EpfAcno" runat="server">
                                             <td>
                                                 <asp:Label ID="lbl_EpfAcno" runat="server" Text="E.P.F.A/c No.01"></asp:Label>
                                                 &nbsp;<span style="color: #CC3300">*</span></td>
                                             <td colspan="2">
                                                 <input type="text" runat="server" id="txt_EpfAcno" class="form-control" 
                                                     style="Width:170px;" onkeydown="AllowOnlyNumeric1(event);" />
                                             </td>
                                             </tr>
                                             <tr  id="row_PfAcno" runat="server">
                                                 <td>
                                                     <asp:Label ID="lbl_PfAcno" runat="server" Text="Pension Fund A/c No.10"></asp:Label>
                                                     &nbsp;<span style="color: #CC3300">*</span></td>
                                                 <td colspan="2">
                                                 
                                                     <input type="text" runat="server" id="txt_PfAcno" class="form-control" 
                                                         style="Width:170px;" onkeydown="AllowOnlyNumeric1(event);" />
                                                     
                                                 </td>
                                             </tr>
                                             <tr id="row_DliAcno" runat="server">
                                                 <td>
                                                     <asp:Label ID="lbl_DliAcno" runat="server" Text="D.L.I.A/c No.21"></asp:Label>
                                                     &nbsp;<span style="color: #CC3300">*</span></td>
                                                 <td colspan="2">
                                                     <input type="text" runat="server" id="txt_DliAcno" class="form-control" 
                                                         style="Width:170px;" onkeydown="AllowOnlyNumeric1(event);" />
                                                 </td>
                                             </tr>
                                             <tr id="row_AdminAcno2" runat="server">
                                                 <td>
                                                     <asp:Label ID="lbl_AdminAcno2" runat="server" Text="Admin Charge A/c No.2"></asp:Label>
                                                     &nbsp;<span style="color: #CC3300">*</span></td>
                                                 <td colspan="2">
                                                     <input type="text" runat="server" id="txt_AdminAcno2" class="form-control" 
                                                         style="Width:170px;" onkeydown="AllowOnlyNumeric1(event);" />
                                                 </td>
                                             </tr>
                                             <tr id="row_AdminAcno22" runat="server">
                                                 <td>
                                                     <asp:Label ID="lbl_AdminAcno22" runat="server" Text="Admin Charge A/c No.22"></asp:Label>
                                                     &nbsp;<span style="color: #CC3300">*</span></td>
                                                 <td colspan="2">
                                                     <input type="text" runat="server" id="txt_AdminAcno22" class="form-control" 
                                                         style="Width:170px;" onkeydown="AllowOnlyNumeric1(event);" />
                                                 </td>
                                             </tr>
                                             <tr id="row_Bankname" runat="server">
                                                 <td>
                                                     <asp:Label ID="lbl_Bankname" runat="server" 
                                                         Text="Name of the Bank, Amount is remitted" Width="144px"></asp:Label>
                                                 </td>
                                                 <td colspan="2">
                                                     <asp:DropDownList ID="ddl_Bankname" runat="server" CssClass="form-control" 
                                                         Width="177px">
                                                     </asp:DropDownList>
                                                 </td>
                                             </tr>
                                             <tr id="row_Branchname" runat="server">
                                                 <td>
                                                     <asp:Label ID="lbl_Branchname" runat="server" Text="Branch Name"></asp:Label>
                                                 </td>
                                                 <td colspan="2">
                                                     <input type="text" runat="server" id="txt_Branchname" class="form-control" 
                                                         style="Width:170px;" onkeypress="AllowOnlyText();" />
                                                 </td>
                                             </tr>
                                             <tr id="row_remitdate" runat="server">
                                                 <td>
                                                     <asp:Label ID="lbl_remitdate" runat="server" Text="Date of Remittence"></asp:Label>
                                                     &nbsp;<span style="color: #CC3300">*</span></td>
                                                 <td colspan="2">
                                                     <input type="text" id="txt_remitdate" runat="server" class="form-control" 
                                                         onkeyup="fn_date(event,this.id);" maxlength="10" style="width: 170px"
                                                         />
                                                 </td>
                                             </tr>
                                             <tr id="row_lastmonth" runat="server">
                                                 <td>
                                                     <asp:Label ID="lbl_lastmonth" runat="server" Text="LastMonth"></asp:Label>
                                                 </td>
                                                 <td colspan="2">
                                                     <input type="text" runat="server" id="txt_lastmonth" class="form-control" 
                                                         style="Width:170px;" onkeydown="AllowOnlyNumeric1(event);" />
                                                 </td>
                                             </tr>
                                             <tr id="row_videform5" runat="server">
                                                 <td>
                                                     <asp:Label ID="lbl_videform5" runat="server" Text="Vide Form 5"></asp:Label>
                                                 </td>
                                                 <td colspan="2">
                                                     <input type="text" runat="server" id="txt_videform5" class="form-control" 
                                                         style="Width:170px;" onkeydown="AllowOnlyNumeric1(event);" />
                                                 </td>
                                             </tr>
                                             <tr id="row_videform10" runat="server">
                                                 <td>
                                                     <asp:Label ID="lbl_videform10" runat="server" Text="Vide Form 10"></asp:Label>
                                                 </td>
                                                 <td colspan="2">
                                                     <input type="text" runat="server" id="txt_videform10" class="form-control" 
                                                         style="Width:170px;" onkeydown="AllowOnlyNumeric1(event);" />
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td style="color: #CC3300">
                                                     * Mandatory Fields</td>
                                                 <td colspan="2">
                                                     <asp:Button ID="Button1" runat="server" Text="View Report"  class="btn btn-success" OnClick="btn_Report_Click" />
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
