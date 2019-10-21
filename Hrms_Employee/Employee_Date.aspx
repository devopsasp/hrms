<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="Employee_Date.aspx.cs" Inherits="Hrms_Employee_Default" Title="ePay-HRMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script language="javascript" type="text/javascript">

function fn_date(event,txtid)
{  
    var len,txtvalue,bool_obj,i;
    txtvalue= document.getElementById(txtid).value;
    txtlen=txtvalue.length; 
    if(event.keyCode!=8 && event.keyCode!=46 && event.keyCode!=35 && event.keyCode!=36 && event.keyCode!=37 && event.keyCode!=38 && event.keyCode!=39 && event.keyCode!=40)     
        
        if((txtlen!=0) && (txtvalue.charAt(txtlen)!=" "))
            bool_obj=fn_validate(txtlen,txtvalue);
        
            if(bool_obj==true)
        
                if(txtlen==2 || txtlen==5)
                    document.getElementById(txtid).value=txtvalue+"/";
        
                else if (txtlen==10 && (txtvalue.charAt(2)!="/" || txtvalue.charAt(5)!="/"))
                    document.getElementById(txtid).value=txtvalue.substring(0,txtlen-txtlen);              
        
                else if (txtlen==10)
                    //document.getElementById(txtid).value=txtvalue.substring(6,10)+"/"+txtvalue.substring(3,5)+"/"+txtvalue.substring(0,2);              
                    document.getElementById(txtid).value=txtvalue;
        
                else
                    document.getElementById(txtid).value=txtvalue;
        
            else
                //document.getElementById(txtid).value=txtvalue.substring(0,txtlen-txtlen);
        
                 if (txtlen<=10 && (txtvalue.charAt(2)=="/" || txtvalue.charAt(5)=="/"))
                    document.getElementById(txtid).value=txtvalue.substring(0,txtlen-1);
        
                else 
                    document.getElementById(txtid).value=txtvalue.substring(0,txtlen-txtlen);
                
}
function fn_validate(len,tval)
{
    var str,dat,mon,yea,Cdat;
    dat=tval.substring(0,2);    mon=tval.substring(3,5);    yea=tval.substring(6,10);
     switch(len)
    {
    case 1: str=tval.charAt(0);
            if(tval>=0 && tval<=3 && str!=" ")  return true;    else    return false;   break;
    case 2: str=tval.charAt(1);
            if(dat<=31 && dat>0 && str!=" ")    return true;    else    return false;   break;
    case 3: str=tval.charAt(2);
            if(str=="/" && str!=" ")            return true;    else    return false;   break;
    case 4: str=tval.charAt(3);
            if(str<=1 && str!=" ")              return true;    else    return false;   break;
    case 5: str=tval.charAt(4);
            if((mon<=12 && mon>0) && str!=" ")
            {   if((mon==1 || mon==3 || mon==5 || mon==7 || mon==8 || mon==10 || mon==12 )&& (dat<=31))    return true;   
                else if ((mon==4 || mon==6 || mon==9 || mon==11)&& (dat<=30))    return true;
                else if ((mon==2) && (dat<=29))  return true;
                else    return false;   break;
            }   else    return false;   break;
    case 6: str=tval.charAt(5);
            if(str=="/" && str!=" ")            return true;    else    return false;   break;
    case 7:str=tval.charAt(6);
            if(str<=2 && str>0 && str!=" ")     return true;    else    return false;   break;
    case 8: str=tval.substring(6,8);
            if((str>=19 && str<=21) && str!=" ")return true;    else    return false;   break;
    case 9: str=tval.charAt(8);
            if(str>=0 && str<=9 && str!=" ")    return true;    else    return false;   break;
    case 10: str=tval.charAt(9);
            if(str>=0 && str<=9 && str!=" ")
                if((yea%4)!=0)
                    if ((mon==2) && (dat>28))   return false;   
                    else                        return true;
                else                            return true;
            else                                return false;   break;
    }
} 

function fn_datecheck(event, txtid)
{
    var txtvalue, txtlen;
    txtvalue= document.getElementById(txtid).value;
    txtlen=txtvalue.length;
    var str;
    if(txtlen==10 && (txtvalue.charAt(2)=="/" && txtvalue.charAt(5)=="/"))
    {
    str="The Date format is correct";
    }
    else if(txtlen==0)
    {
    str="Empty Text";
    }
    else
    {
    alert("The Date format is incorrect");
    document.getElementById(txtid).value=txtvalue.substring(0,txtlen-txtlen);
    }
//    alert(str);
}
</script>


<div><h3 class="page-header">Effective Dates&nbsp;</h3></div>
<div align="center" class="page-header">
    <asp:Label ID="lbl_empcodename" runat="server" Font-Bold="True" 
        Font-Size="Medium"></asp:Label>
    </div>
    <div align="right">
    </div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
   <%-- <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div style="width: 70%">
                    <table cellpadding="1%" cellspacing="1%" width="100%" class="table table-striped table-bordered table-hover">
                        
                        <tr>
                            <td>
                                Initial Joining Date</td>
                            <td >
                               <%-- <input id="txtjoin" class="form-control" type="text" runat="server" 
                                    onfocusout="fn_datecheck(event,this.id);" onkeyup="fn_date(event,this.id);" 
                                    maxlength="10" />--%>
                                    <div style=" width:150px; float:left;">
                                        <asp:TextBox ID="txtjoin" class="form-control" type="text" runat="server" 
                                    onfocusout="fn_datecheck(event,this.id);" onkeyup="fn_date(event,this.id);" Width="150px" >
                                        </asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtjoin" Format="dd/MM/yyyy" >
                                        </asp:CalendarExtender>
                                    </div>
                                    <div style=" width:25px; float:left;  margin-left:10px; margin-top:3px;">                                                
                                                    <asp:Image ID="Image3" runat="server"  
                                                        Text="" Width="25px" 
                                                        ImageUrl="~/Images/calendaricon.png" />
                                 </div>


                            </td>
                            <td >
                                Date of Separation</td>
                            <td>
                               <%-- <input id="txtoffer" class="form-control" type="text" runat="server" 
                                    onfocusout="fn_datecheck(event,this.id);" onkeyup="fn_date(event,this.id);" 
                                    maxlength="10" />--%>
                                    <div style=" width:150px; float:left;">
                                <asp:TextBox ID="txtoffer" class="form-control" type="text" runat="server" 
                                    onfocusout="fn_datecheck(event,this.id);" onkeyup="fn_date(event,this.id);" Width="150px">
                                    </asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtoffer" Format="dd/MM/yyyy" >
                                        </asp:CalendarExtender>
                                    </div>
                                     <div style=" width:25px; float:left;  margin-left:10px; margin-top:3px;">                                                
                                                    <asp:Image ID="Image1" runat="server"  
                                                        Text="" Width="25px" 
                                                        ImageUrl="~/Images/calendaricon.png" />
                                 </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Probation Upto</td>
                            <td>
                            <div style=" width:150px; float:left;">
                                <asp:TextBox  id="txtprobation" class="form-control" type="text" runat="server" 
                                    onfocusout="fn_datecheck(event,this.id);" onkeyup="fn_date(event,this.id);" 
                                    maxlength="10" Width="150px"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtprobation" Format="dd/MM/yyyy" >
                                        </asp:CalendarExtender>
                                    </div>
                                    <div style=" width:25px; float:left;  margin-left:10px; margin-top:3px;">                                                
                                                    <asp:Image ID="Image6" runat="server"  
                                                        Text="" Width="25px" 
                                                        ImageUrl="~/Images/calendaricon.png" />
                            </td>
                            <td>
                                Extended Upto</td>
                            <td>
                             <div style=" width:150px; float:left;">
                                <asp:TextBox id="txtextended" class="form-control" type="text" runat="server" 
                                    onfocusout="fn_datecheck(event,this.id);" onkeyup="fn_date(event,this.id);" 
                                    maxlength="10" Width="150px"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtextended" Format="dd/MM/yyyy" >
                                        </asp:CalendarExtender>
                                    </div>
                                    <div style=" width:25px; float:left;  margin-left:10px; margin-top:3px;">                                                
                                                    <asp:Image ID="Image7" runat="server"  
                                                        Text="" Width="25px" 
                                                        ImageUrl="~/Images/calendaricon.png" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Confirmation Date</td>
                            <td>
                                <%--<input id="txtconfirm" class="form-control" type="text" runat="server" 
                                    onfocusout="fn_datecheck(event,this.id);" onkeyup="fn_date(event,this.id);" 
                                    maxlength="10" />--%>
                                    <div style=" width:150px; float:left;">
                                        <asp:TextBox ID="txtconfirm" class="form-control" type="text" runat="server" 
                                    onfocusout="fn_datecheck(event,this.id);" onkeyup="fn_date(event,this.id);" Width="150px"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtconfirm" Format="dd/MM/yyyy">
                                        </asp:CalendarExtender>                                    
                                    </div>
                                    <div style=" width:25px; float:left;  margin-left:10px; margin-top:3px;">                                                
                                                    <asp:Image ID="Image2" runat="server"  
                                                        Text="" Width="25px" 
                                                        ImageUrl="~/Images/calendaricon.png" />
                                 </div>


                            </td>
                            <td>
                                Retirement Date</td>
                            <td>
                                <%--<input id="txtretire" class="form-control" type="text" runat="server" 
                                    onfocusout="fn_datecheck(event,this.id);" onkeyup="fn_date(event,this.id);" 
                                    maxlength="10" />--%>
                                    <div style=" width:150px; float:left;">
                                        <asp:TextBox ID="txtretire" class="form-control" type="text" runat="server" 
                                    onfocusout="fn_datecheck(event,this.id);" onkeyup="fn_date(event,this.id);" Width="150px"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtretire" Format="dd/MM/yyyy" >
                                        </asp:CalendarExtender>
                                    </div>
                                    <div style=" width:25px; float:left;  margin-left:10px; margin-top:3px;">                                                
                                                    <asp:Image ID="Image4" runat="server"  
                                                        Text="" Width="25px" 
                                                        ImageUrl="~/Images/calendaricon.png" />
                                 </div>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                Confirmation<br /> Renewal Date</td>
                            <td>
                                <%--<input id="txtrenew" class="form-control" type="text" runat="server" 
                                    onfocusout="fn_datecheck(event,this.id);" onkeyup="fn_date(event,this.id);" 
                                    maxlength="10" />--%>
                                      <div style=" width:150px; float:left;">
                                          <asp:TextBox id="txtrenew" class="form-control" type="text" runat="server" 
                                    onfocusout="fn_datecheck(event,this.id);" onkeyup="fn_date(event,this.id);" Width="150px"></asp:TextBox>
                                          <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtrenew" Format="dd/MM/yyyy">
                                          </asp:CalendarExtender>
                                      </div>
                                    <div style=" width:25px; float:left;  margin-left:10px; margin-top:3px;">                                                
                                                    <asp:Image ID="Image5" runat="server"  
                                                        Text="" Width="25px" 
                                                        ImageUrl="~/Images/calendaricon.png" />
                                 </div>
                            </td>
                            <td>
                                Reason for Change</td>
                            <td>
                                <asp:TextBox ID="txt_reason" class="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="right">
                                <asp:Button ID="btn_Back" runat="server" class="btn btn-info" 
                                    OnClick="btn_Back_Click" Text="Back" />
                                <asp:Button ID="btn_skip" runat="server" CausesValidation="False" 
                                    class="btn btn-warning" OnClick="btn_skip_Click" Text="Skip" ToolTip="Skip" />
                                <asp:Button ID="btn_save" runat="server" class="btn btn-success" 
                                    OnClick="btn_save_Click" Text="Save" />
                                <asp:Button ID="btn_update" runat="server" class="btn btn-success" 
                                    OnClick="btn_update_Click" Text="Update" />
                            </td>
                        </tr>
                     </table>   
                     </div>
                     <div>  

                         </td>                                    
           </div>
           </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
