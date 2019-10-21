<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Employee_Deducation.aspx.cs" Inherits="Hrms_Employee_Default" Title="Welcome to HRMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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

    </script>
    <div><h3 class="page-header">Deduction Details&nbsp;</h3></div>
<div align="center">
    <asp:Label ID="lbl_empcodename" runat="server" Font-Bold="True" 
        Font-Size="Medium"></asp:Label>
    </div>
    <div>
    <div style="float:left;">Date :</div> 
       
    <div style=" width:150px; float:left;">
        <asp:TextBox id="txt" type="text" runat="server" onkeyup="fn_date(event,this.id);" Width="150px"></asp:TextBox>
        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt" Format="dd/MM/yyyy" >
        </asp:CalendarExtender>      
    </div>
     <div style=" width:25px; float:left;  margin-left:10px; margin-top:3px;">                                                
                                                    <asp:Image ID="Image3" runat="server"  
                                                        Text="" Width="25px" 
                                                        ImageUrl="~/Images/calendaricon.png" />
      </div> 
       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="txt" ErrorMessage="Enter Date"></asp:RequiredFieldValidator>   
    
        
</div><br />
   
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
   
                     <div>                                                 
                         <asp:GridView ID="grd_Deducation" runat="server" AutoGenerateColumns="False"  DataKeyNames="DeductionId"  GridLines="None" 
                             onrowdatabound="grd_Deducation_RowDataBound" ShowFooter="True" Width="80%" CssClass="table table-striped table-bordered table-hover">
                             <Columns>
                                 <asp:TemplateField>
                                     <HeaderTemplate>
                                         <table cellpadding="0" cellspacing="0" width="100%">
                                             <colgroup>
                                                 <col></col>
                                             </colgroup>
                                             <thead>
                                                 <tr>
                                                     <th align="left" style="width: 100%;">
                                                         Deduction Code</th>
                                                 </tr>
                                             </thead>
                                         </table>
                                     </HeaderTemplate>
                                     <ItemTemplate>
                                         <%#Eval("DeducationCode")%>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField>
                                     <HeaderTemplate>
                                         <table cellpadding="0" cellspacing="0" width="100%">
                                             <colgroup>
                                                 <col></col>
                                             </colgroup>
                                             <thead>
                                                 <tr>
                                                     <th align="left" style="width: 100%;">
                                                         Deduction Name</th>
                                                 </tr>
                                             </thead>
                                         </table>
                                     </HeaderTemplate>
                                     <ItemTemplate>
                                         <%#Eval("DeductionName")%>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField>
                                     <HeaderTemplate>
                                         <table cellpadding="0" cellspacing="0" width="100%">
                                             <colgroup>
                                                 <col></col>
                                             </colgroup>
                                             <thead>
                                                 <tr>
                                                     <th align="left" style="width: 100%;">
                                                         Amount</th>
                                                 </tr>
                                             </thead>
                                         </table>
                                     </HeaderTemplate>
                                     <ItemTemplate>
                                      <asp:TextBox id="txt_Ded_Amt" runat="server" value='<%#Eval("Amount")%>' Text="" OnTextChanged="txt_Ded_Amt_TextChanged" AutoPostBack="true" CssClass="form-control" onkeypress="return onlyNumbersWithDot(event);" ></asp:TextBox>
                                         <%--<input type="text" runat="server" value='<%#Eval("Amount")%>' class="form-control" id="txt_Ded_Amt" onkeydown="AllowOnlyNumeric1(event);" />--%>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField>
                                     <HeaderTemplate>
                                         <table cellpadding="0" cellspacing="0" width="100%">
                                             <colgroup>
                                                 <col></col>
                                             </colgroup>
                                             <thead>
                                                 <tr>
                                                     <th align="left" style="width: 100%;">
                                                         Eligiblity</th>
                                                 </tr>
                                             </thead>
                                         </table>
                                     </HeaderTemplate>
                                     <ItemTemplate>
                                         <asp:CheckBox ID="grd_chk" runat="server" />
                                     </ItemTemplate>
                                 </asp:TemplateField>

                             </Columns>
                             
                         </asp:GridView>

                         <asp:Button ID="btn_Back" runat="server" OnClick="btn_Back_Click" class="btn btn-info" Text="Back"/>
                         <asp:Button ID="btn_skip" runat="server" CausesValidation="False" Text="Skip"
                              OnClick="btn_skip_Click" ToolTip="Skip" class="btn btn-warning" />
                         <asp:Button ID="btn_save" runat="server" OnClick="btn_save_Click" class="btn btn-success"  Text ="Save"/>
                         <asp:Button ID="btn_update" runat="server" OnClick="btn_update_Click" class="btn btn-success" Text="Update"/>
           </div>
    


    </asp:Content>
