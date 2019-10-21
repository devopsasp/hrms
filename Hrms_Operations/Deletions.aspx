<%@ Page Language="C#" MasterPageFile="~/HRMS.master" MaintainScrollPositionOnPostback="true"
    AutoEventWireup="true" CodeFile="Deletions.aspx.cs" Inherits="Hrms_Company_Default"
    Title="User Deletion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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

function check_del()
{
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_reason.value == "" || document.aspnetForm.ctl00$ContentPlaceHolder1$txt_empcode.value == "" || document.aspnetForm.ctl00$ContentPlaceHolder1$txt_emppwd.value == "" || document.aspnetForm.ctl00$ContentPlaceHolder1$ddl_item.value=="0" || document.aspnetForm.ctl00$ContentPlaceHolder1$ddl_delete.value=="0")
        {
            alert("Complete all the Details before Deleting");
            aspnetForm.ctl00$ContentPlaceHolder1$txt_reason.focus();
            return false;
        }                        
        else
        { 
              return true;  
        }
}
    </script>

    <table width="100%">
        <tr>
            <td class="tdComposeHeader">
                <table runat="server" id="tab_combination" style="width: 100%; height: 70%">
                    <tr>
                        <td class="TextStyle" height="35" colspan="3" width="30%" >
                            <span class="Title">&nbsp;
                               <h3> Deletions</h3> </span></td>
                    </tr>
                    <tr>
                        <td colspan="3" width="30%" align="center" class="TextStyle">
                            <asp:Label ID="lbl_Error" runat="server" CssClass="TextStyle" ForeColor="Red" Font-Size="Small"
                                Width="65%"></asp:Label></td>
                    </tr>
                    <tr class="textstyle">
                        <td align="right" class="TextStyle" colspan="3">
                                       <%-- <asp:ImageButton ID="btn_back" ImageUrl="~/Images/back.png" onmouseover="this.src='../Images/Backover.png';" onmouseout="this.src='../Images/Back.png';" runat="server" 
                                            ImageAlign="AbsMiddle" onclick="btn_back_Click" />--%>
                            <asp:Button ID="btn_back" runat="server" Text="Back" onclick="btn_back_Click" CssClass="btn btn-primary"/>
                        </td>
                    </tr>
                    <tr id="row_first" runat="server">
                        <td align="right" class="TextStyle" style="height: 21px;" width="30%">
                            <asp:Label ID="lbl_first" runat="server"></asp:Label>
                        </td>
                        <td align="center" valign="middle" style="width: 40%; height: 21px;">
                            <asp:DropDownList ID="ddl_first" runat="server" CssClass="form-control" Width="68%" 
                                AutoPostBack="True" 
                                OnSelectedIndexChanged="ddl_first_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="width: 100px; height: 21px;">
                                        &nbsp;</td>
                    </tr>
                    </table>
                   
                            <table runat="server" id="tab_Entry" style="width: 100%; height: 70%">
                            <tr >
                                    <td align="right" class="TextStyle" style="height: 21px" width="30%">
                                    </td>
                                    <td align="center" style="width: 40%; height: 21px" valign="middle">
                                    </td>
                                    <td style="width: 83px; height: 21px">
                                    </td>
                                </tr>
                                <tr runat="server" id="tr1" visible="false">
                                    <td align="right" class="TextStyle" style="height: 21px" width="30%">
                                        <asp:Label ID="lbl1" Text="Select Branch" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" style="width: 40%; height: 21px" valign="middle">
                                        <asp:DropDownList ID="ddl_branch" runat="server" CssClass="form-control"
                                            Width="68%" AutoPostBack="True" 
                                            onselectedindexchanged="ddl_branch_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 83px; height: 21px">
                                    </td>
                                </tr>
                                <tr runat="server" id="tr2" visible="false">
                                    <td align="right" class="TextStyle" style="height: 21px" width="30%">
                                        <asp:Label ID="lbl_sec" Text="Select Department" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" style="width: 40%; height: 21px" valign="middle">
                                        <asp:DropDownList ID="ddl_dept" runat="server" CssClass="form-control"
                                            Width="68%" AutoPostBack="True" 
                                            onselectedindexchanged="ddl_dept_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 83px; height: 21px">
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td align="right" class="TextStyle" style="height: 21px" width="30%">
                                        <asp:Label ID="lbl_second" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" style="width: 40%; height: 21px" valign="middle">
                                        <asp:DropDownList ID="ddl_second" runat="server" CssClass="form-control"
                                            Width="68%">
                                        </asp:DropDownList></td>
                                    <td style="width: 83px; height: 21px">
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td align="right" class="TextStyle" width="30%">
                                        Effective Date</td>
                                    <td align="center" valign="middle" style="width: 40%">
                                        <input id="txt_eff_date" CssClass="form-control" type="text" runat="server"
                                            onkeyup="fn_date(event,this.id);" maxlength="10" style="width: 68%" /></td>
                                    <td style="width: 83px">
                                    </td>
                                </tr>
                                <tr runat="server" id="tr">
                                    <td align="right" class="TextStyle" width="30%">
                                        Reason for Deletion</td>
                                    <td align="center" style="width: 40%" valign="middle">
                                        <asp:DropDownList ID="ddl_reason" runat="server" CssClass="form-control"
                                            Width="68%">
                                        <asp:ListItem Text="Retirement" Value="1"></asp:ListItem>
                                         <asp:ListItem Text="Releaving" Value="2"></asp:ListItem>
                                         <asp:ListItem Text="Others" Value="3"></asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="width: 83px">
                                    </td>
                                </tr>
                                <tr id="row_reason" runat="server">
                                    <td align="right" valign="middle" width="30%" style="height: 61px" class="TextStyle">
                                        Summary</td>
                                    <td align="center" style="height: 61px; width: 40%;">
                                        <textarea id="txt_summary" runat="server" rows="3" style="width: 68%" CssClass="form-control"></textarea></td>
                                    <td style="width: 83px; height: 61px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="3" class="TextStyle">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" class="TextStyle" colspan="3">
                                       <%-- <asp:ImageButton ID="btn_delete" ImageUrl="~/Images/Delete.png" onmouseover="this.src='../Images/Deleteover.png';" onmouseout="this.src='../Images/Delete.png';" runat="server" 
                                            ImageAlign="AbsMiddle" OnClick="btn_delete_Click" />--%>
                                        <asp:Button ID="btn_delete" runat="server" Text="Delete" OnClick="btn_delete_Click" class="btn btn-success" />
                                        </td> 
                                                                       </tr>
                                <tr>
                                    <td align="center" class="TextStyle" colspan="3">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" class="TextStyle" colspan="3" valign="bottom">
                                    </td>
                                </tr>
                            </table>
                        
                
                <table runat="server" id="tab_deletion" width="100%">
                    <tr>
                        <td align="center" >
                            <asp:Label ID="lbl_delete" runat="server"  Font-Size="Small"
                                Width="80%"></asp:Label></td>
                    </tr>
                    <tr>
                        <td align="center">
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="middle">
                            <asp:ImageButton ID="btn_yes" ImageUrl="~/Images/Yes.jpg" runat="server" OnClick="btn_yes_Click"
                                title="Click to Delete" ImageAlign="AbsMiddle" />&nbsp;
                            <asp:ImageButton ID="btn_no" ImageUrl="~/Images/No.jpg" runat="server" OnClick="btn_no_Click"
                                title="Click to Cancel Deletion" ImageAlign="AbsMiddle" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
