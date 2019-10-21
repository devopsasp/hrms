<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Payslip_process.aspx.cs" Inherits="PayRoll_Default" Title="Payslip Processing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>
    

    <script language="javascript" type="text/javascript">


        function validate() {

            var r = confirm("Are you sure, you want to undo process?");
            if (r == true) {
                return true;
            }
            else {
                return false;
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
    
    <script language="javascript" type="text/javascript">
    
       function fn_Save()
        {
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$txtEmployee_con.value == "")
        {
            alert("Enter Employee Contribution Amount");
            aspnetForm.ctl00$ContentPlaceHolder1$txtEmployee_con.focus();
            return false;
        }
        else if(document.aspnetForm.ctl00$ContentPlaceHolder1$txtEloyer_con.value=="")
        {
           alert("Enter Employer Contribution Amount");
           aspnetForm.ctl00$ContentPlaceHolder1$txtEloyer_con.focus();
            return false;
        }
        else if(document.aspnetForm.ctl00$ContentPlaceHolder1$txtAdmin_Charges.value=="")
        {
           alert("Enter Admin Charges");
           aspnetForm.ctl00$ContentPlaceHolder1$txtAdmin_Charges.focus();
            return false;
        }
        else if(document.aspnetForm.ctl00$ContentPlaceHolder1$txtEligibility_Amt.value=="")
        {
           alert("Enter Eligibility_Amt");
           aspnetForm.ctl00$ContentPlaceHolder1$txtEligibility_Amt.focus();
            return false;
        }
        else
        { 
              return true;  
        }
    }    
    </script>

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
    
    </script>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div><h2 class="page-header">Pay Slip Processing</h2></div>
    <div> <asp:DropDownList ID="ddl_branch" runat="server" AutoPostBack="True" 
                        CssClass="form-control" 
                        onselectedindexchanged="ddl_branch_SelectedIndexChanged">
                </asp:DropDownList></div>
                <div class="panel-body">
                            <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>--%>
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

    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td id="tdComposeHeader" valign="top" align="center" class="tdComposeHeader">
   

                                            <table>
                                            <tr>
                                            <td>
                        
                         <br />
                                               <div style="width: 100%">
                                                    <table class="table">
                                                        <tr>
                                                            <td >
                                                                
                                                                Period Code</td>
                                                            <td>
                                                                <asp:DropDownList ID="ddl_periodcode" runat="server" 
                                                                    CssClass="form-control" 
                                                                    onselectedindexchanged="ddl_periodcode_SelectedIndexChanged" 
                                                                    AutoPostBack="True" Width="80%">
                                                                    <asp:ListItem>Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                                 Period Type</td>
                                                            <td >
                                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                                                                   RepeatDirection="Horizontal" Enabled="False" Font-Bold="True" Width="80%">
                                                                    <asp:ListItem Selected="True">Month</asp:ListItem>
                                                                    <asp:ListItem>Week</asp:ListItem>
                                                                    <asp:ListItem>Day</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                           
                                                            <td>
                                                                
                                                                Select the year</td>
                                                            <td >
                                                                <asp:DropDownList ID="ddl_year" runat="server" 
                                                                    CssClass="form-control" Enabled="False" Width="80%">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            
                                                            <td >
                                                                
                                                                Select the month</td>
                                                            <td >
                                                                <asp:DropDownList ID="ddl_month" runat="server" CssClass="form-control" Enabled="False" Width="80%">
                                                                    <asp:ListItem Value="0">Select</asp:ListItem>
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
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Salary from date</td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_fdate" runat="server" onkeyup="fn_date(event,this.id);"  
                                                                    CssClass="form-control" ReadOnly="True" Width="80%"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                           
                                                            <td > Salary to date</td>
                                                            <td  >
                                                                <asp:TextBox ID="Txt_tdate" runat="server" onkeyup="fn_date(event,this.id);" 
                                                                    CssClass="form-control" ReadOnly="True" Width="80%"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                                 Working Days</td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_totdays" runat="server" onkeyup="fn_date(event,this.id);" 
                                                                    CssClass="form-control" ReadOnly="True" Width="80%"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                                
                                                                Pay date</td>
                                                            <td >
                                                                <asp:TextBox ID="txt_paydate" runat="server" onkeyup="fn_date(event,this.id);" 
                                                                    CssClass="form-control" ReadOnly="True" Width="80%"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td  >
                                                                 
                                                                <asp:Button ID="Btn_process" runat="server" Text="Do Process" 
                                                                     onclick="Btn_process_Click" CssClass="btn btn-primary" />
                                                                 </td><td align="center">
                                                                <asp:Button ID="Btn_undo" runat="server" Text="Undo Process" OnClientClick="return validate()"
                                                                    onclick="Btn_undo_Click"  CssClass="btn btn-warning" />
                                                                    </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" >
                                                                
                                                                <asp:Button ID="Btn_Show" runat="server" Text="Show Employees"
                                                                    onclick="Btn_Show_Click"  CssClass="btn btn-info"
                                                                     />
                                                                
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    </div>
                                                </td>
                                            <td align="center" >
                                                <table id = "Emp" runat="server">
                                                   <tr>
                                                       <td>
                                                       <div id="div1" class="scrollable-container" runat="server" style="overflow-x:auto;width:98%;overflow: auto; height: 500px;">
                                                        <asp:DropDownList ID="ddl_department" runat="server" AutoPostBack="True" 
                                                               class="form-control" Width="100%" 
                                                               onselectedindexchanged="ddl_department_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                            <input type="checkbox" id="chkall" runat="server" onclick="javascript: fn_chkall(this.id,'ctl00_ContentPlaceHolder1_chk_Empcode')" checked="checked" />Select All Employees
                                                        <asp:CheckBoxList  ID="chk_Empcode" runat="server" CssClass="form-control" Width="100%"> </asp:CheckBoxList>
                                                   
                                                     
                                                              </div>
                                                        </td>
                                                   </tr>
                                                </table>
                                            </td>
                                            </tr>
                                            </table>
                                           
                            </ContentTemplate>
                            </asp:UpdatePanel>
                            </div>
  
</asp:Content>
