<%@ Page Language="C#" MasterPageFile="~/HRMS.master"
    AutoEventWireup="true" CodeFile="DeletionEntry.aspx.cs" Inherits="Hrms_Operations_Default"
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
                        <td  class="TextStyle" height="35" colspan="3" 
                            width="30%" >
                            <span class="Title">&nbsp;
                                <span style="font-family: Calibri; font-size: medium; font-weight: bold; ">Deletions</span></span></td>
                    </tr>
                    <tr>
                        <td colspan="3" width="30%" align="center" class="TextStyle">
                            <asp:Label ID="lbl_Error" runat="server" CssClass="TextStyle" ForeColor="Red" Font-Size="Small"
                                Width="65%"></asp:Label></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3" class="TextStyle">
                            <table runat="server" id="tab_emp">
                                <tr id="row_mMydet" runat="server">
                                    <td align="center" colspan="3" class="TextStyle">
                                    </td>
                                </tr>
                                <tr id="row_empcode" runat="server">
                                    <td align="right" class="TextStyle" style="width: 39%">
                                        Emp Code</td>
                                    <td style="width: 30%">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:TextBox ID="txt_empcode" runat="server" CssClass="form-control"
                                            Width="50%"></asp:TextBox></td>
                                    <td style="width: 265px">
                                    </td>
                                </tr>
                                <tr id="row_emppwd" runat="server">
                                    <td align="right" class="TextStyle" style="width: 39%">
                                        Password</td>
                                    <td style="width: 30%">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:TextBox ID="txt_emppwd" runat="server" CssClass="form-control"
                                            Width="50%" TextMode="Password" Height="17px"></asp:TextBox></td>
                                    <td style="width: 265px">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="TextStyle" style="width: 39%">
                                    </td>
                                    <td align="center" style="width: 30%">
                                    </td>
                                    <td style="width: 265px">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="3" valign="bottom" class="TextStyle">
                                        <%--<asp:ImageButton ID="btn_yes" ImageUrl="~/Images/Continue.png" onmouseover="this.src='../Images/Continueover.png';" onmouseout="this.src='../Images/Continue.png';" runat="server" OnClick="btn_yes_Click"
                                            title="Click to Delete" ImageAlign="AbsMiddle"  />--%>
                                        <asp:Button ID="btn_yes" runat="server" Text="Save" OnClick="btn_yes_Click" class="btn btn-success"/>
                                            &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="30%" class="TextStyle">
                        </td>
                        <td align="center" style="width: 40%">
                        </td>
                        <td style="width: 100px">
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="TextStyle" colspan="3" valign="bottom">
                            <asp:Button ID="btn_Employee" runat="server" Text="Employee Deletion" 
                                OnClick="btn_Employee_Click" class="btn btn-success" Font-Bold="True" 
                                Font-Names="Calibri" ForeColor="White" />
                            <asp:Button ID="btn_Masters" runat="server" Text="Masters Deletion" 
                                OnClick="btn_Masters_Click" class="btn btn-success" Font-Bold="True" 
                                Font-Names="Calibri" ForeColor="White" />

                            <asp:Button ID="btn_branch" runat="server" Text="Branch Deletion" 
                                OnClick="btn_branch_Click"  Font-Bold="True" class="btn btn-success"
                                Font-Names="Calibri" ForeColor="White" />                                                     
                            <%--<asp:ImageButton 
                                ImageUrl="~/Images/Back.png" id="btn" runat="server" 
                                onmouseover="this.src='../Images/Backover.png';" onmouseout="this.src='../Images/Back.png';"
                                style="border-width:0px; vertical-align:bottom;" />--%>
                                <asp:Button id="btn" runat="server" Text="Back" 
                                style="border-width:0px; vertical-align:bottom;" CssClass="btn btn-primary" 
                                onclick="btn_Click"/>
                                </a>&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="TextStyle" colspan="3" valign="bottom">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
