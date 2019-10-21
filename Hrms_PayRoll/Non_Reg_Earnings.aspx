<%@ Page Language="C#" MasterPageFile="~/Hrms_PayRoll/PayRollMaster.master" AutoEventWireup="true"
    CodeFile="Non_Reg_Earnings.aspx.cs" Inherits="Bank_Loan_Default" Title="Non Earnings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>
<script type="text/javascript" language="javascript">
function fn_save()
{
 if(document.aspnetForm.ctl00$ContentPlaceHolder1$Txtamount.value == "")
        {
            alert("Enter Amount");
            aspnetForm.ctl00$ContentPlaceHolder1$Txtamount.focus();
            return false;
        }
        else
        { 
              return true;  
        }

}

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
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td valign="top" align="center" class="tdComposeHeader">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr bgcolor="Tan">
            <td height="35px" class="border">
                <span class="Title" style="font-family:  Bell MT; color:#800000">&nbsp;&nbsp;<img src="../Images/rp_arrow.gif" />&nbsp;Employee 
                VS Allowance</span></td>
            <td height="35px" class="border">
                &nbsp;<asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" CssClass="InputDefaultStyle"
                    OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged">
                </asp:DropDownList></td>
        </tr>
    </table>
    <table cellpadding="5" cellspacing="1" width="100%" id="tbl_earnings" runat="server">
        <tr>
            <td colspan="6" align="center">
                <asp:Label ID="lbl_Error" runat="server" CssClass="Error" ForeColor="Red"></asp:Label>&nbsp;</td>
            <%--<td align="center" colspan="1">
                    </td>
                    <td align="center" colspan="1" style="width: 11px">
                    </td>--%>
        </tr>
        <tr runat="server" id="row_selectopt2">
            <td align="right" class="dComposeItemLabel" nowrap="nowrap" 
                style="height: 30px; width: 234px;">
                    Period Code
            </td>
            <td align="right" class="dComposeItemLabel" nowrap="nowrap" 
                style="height: 30px; width: 122px;">
                <asp:DropDownList ID="ddl_periodcode" runat="server" 
                    CssClass="InputDefaultStyle" 
                    onselectedindexchanged="ddl_periodcode_SelectedIndexChanged" 
                    AutoPostBack="True">
                    <asp:ListItem>Select</asp:ListItem>
                   
                </asp:DropDownList>
            </td>
            <td align="left" valign="baseline" class="dComposeItemLabel" 
                style="height: 30px; width: 97px;">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            <td align="left" valign="baseline" style="height: 30px; width: 102px">
                <asp:Button ID="btn_details" runat="server" Text="Details" 
                    OnClick="btn_details_Click" Width="60px" />
            </td>
            <td align="left" valign="baseline" style="width: 193px; height: 30px;">
                </td>
            <td align="left" valign="baseline" style="width: 11px; height: 30px;">
            </td>
        </tr>
       
        <tr runat="server" id="row_selectopt">
            <td align="right" class="dComposeItemLabel" nowrap="nowrap" 
                style="width: 234px">
                    Select Month
            </td>
            <td align="left" nowrap="nowrap" 
                style="width: 122px">
                <asp:DropDownList ID="ddl_month" runat="server" CssClass="InputDefaultStyle" 
                    Enabled="False">
                    <asp:ListItem Value="01">Select</asp:ListItem>
                     <asp:ListItem Value="02">January</asp:ListItem>
                    <asp:ListItem Value="03">Febraury</asp:ListItem>
                    <asp:ListItem Value="04">March</asp:ListItem>
                    <asp:ListItem Value="05">April</asp:ListItem>
                    <asp:ListItem Value="06">May</asp:ListItem>
                    <asp:ListItem Value="07">June</asp:ListItem>
                    <asp:ListItem Value="08">July</asp:ListItem>
                    <asp:ListItem Value="09">August</asp:ListItem>
                    <asp:ListItem Value="10">September</asp:ListItem>
                    <asp:ListItem Value="11">October</asp:ListItem>
                    <asp:ListItem Value="12">November</asp:ListItem>
                    <asp:ListItem Value="13">December</asp:ListItem>
                </asp:DropDownList> </td>
            <td valign="baseline" class="dComposeItemLabel" 
                style="width: 97px">
                &nbsp;Select Year</td>
            <td valign="baseline" style="width: 102px" align="\">
                <asp:DropDownList ID="ddl_year" runat="server" CssClass="InputDefaultStyle" 
                    Enabled="False">
                </asp:DropDownList> 
                </td>
            <td align="left" valign="baseline" style="width: 193px">
                &nbsp;</td>
            <td align="left" valign="baseline" style="width: 11px">
            </td>
        </tr>
         <tr>
            <td align="right" class="dComposeItemLabel" nowrap="nowrap" 
                style="width: 234px; height: 30px;">
                    From Date
             </td>
            <td align="right" class="dComposeItemLabel" nowrap="nowrap" 
                style="width: 122px; height: 30px;">
                <asp:TextBox ID="txt_fromdate" runat="server" CssClass="InputDefaultStyle" onkeyup="fn_date(event,this.id);"
                    MaxLength="10" Enabled="False"></asp:TextBox>
            </td>
            <td valign="baseline" style="width: 97px; height: 30px;" 
                class="dComposeItemLabel">
                To Date</td>
            <td align="left" valign="baseline" style="width: 102px; height: 30px;">
                <asp:TextBox ID="txt_todate" runat="server" CssClass="InputDefaultStyle" onkeyup="fn_date(event,this.id);" 
                    MaxLength="10" Enabled="False"></asp:TextBox>
            </td>
            <td align="left" valign="baseline" style="width: 193px; height: 30px;">
                &nbsp;</td>
            <td align="left" valign="baseline" style="width: 11px; height: 30px;">
            </td>
        </tr>
        
         <tr>
            <td align="right" class="dComposeItemLabel" nowrap="nowrap" 
                style="width: 234px; height: 30px;">
                Select Department
             </td>
            <td align="right" class="dComposeItemLabel" nowrap="nowrap" 
                style="width: 122px; height: 30px;">
                <asp:DropDownList ID="ddl_department" runat="server" CssClass="InputDefaultStyle"  
                    onselectedindexchanged="ddl_department_SelectedIndexChanged" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td valign="baseline" style="width: 97px; height: 30px;" 
                class="dComposeItemLabel">
                Amount</td>
            <td align="left" valign="baseline" style="width: 102px; height: 30px;">
            <input type="text" runat="server" id="Txtamount" class="InputDefaultStyle" onkeydown="AllowOnlyNumeric1(event);" /></td>
            <td align="left" valign="baseline" style="width: 193px; height: 30px;">
                &nbsp;</td>
            <td align="left" valign="baseline" style="width: 11px; height: 30px;">
                &nbsp;</td>
        </tr>
        
        <tr runat="server" id="row_selectopt1">
            <td align="right" class="dComposeItemLabel" nowrap="nowrap" colspan="2" 
                style="height: 30px">
                Employee Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
            <td align="left" valign="baseline" colspan="2" class="dComposeItemLabel" 
                style="height: 30px">
                Allowance Code&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
            <td align="left" valign="baseline" style="width: 193px; height: 30px;">
                </td>
            <td align="left" valign="baseline" style="width: 11px; height: 30px;">
                </td>
        </tr>
        <tr id="tr_chk" runat="server">
            <td nowrap="nowrap" align="right" style="height: 30px;
                " colspan="2">
                <div class="qrychkbox_big" style="height: 260px; left: 0px; top: 0px; border-right: thin solid;
                    border-top: thin solid; border-left: thin solid; border-bottom: thin solid; width: 250px;"
                    align="center">
                    <asp:CheckBox ID="Chkall" runat="server" AutoPostBack="True" 
                        Font-Names="Calibri" Font-Size="X-Small" 
                        oncheckedchanged="Chkall_CheckedChanged" Text=" Select All" />
                    <br />
                    <br />
                    <asp:CheckBoxList align="center" ID="chk_empname" runat="server" Height="16px" 
                        Width="91%" CssClass="InputDefaultstyle1" >
                       
                    </asp:CheckBoxList></div>
            </td>
            <td align="left" valign="baseline" style="height: 30px; " colspan="2">
                <div class="qrychkbox_big" style="height: 260px; left: 0px; top: 0px; border-right: thin solid;
                    border-top: thin solid; border-left: thin solid; border-bottom: thin solid; width: 250px;"
                    align="center">
                    <asp:CheckBox ID="Chkall1" runat="server" AutoPostBack="True" 
                        Font-Names="Calibri" Font-Size="X-Small" 
                        oncheckedchanged="Chkall1_CheckedChanged" Text=" Select All" />
                    <br />
                    <br />
                    <asp:CheckBoxList align="center" ID="chk_allowance" runat="server" 
                        Height="16px" Width="91%" CssClass="InputDefaultstyle1">
                    </asp:CheckBoxList>
                </div>
            </td>
            <td align="left" style="height: 30px; width: 193px;" valign="baseline">
            </td>
            <td align="left" style="height: 30px; width: 11px;" valign="baseline">
            </td>
        </tr>
        <tr>
            <td class="dComposeItemLabel" nowrap align="right" colspan="2">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
            <td align="left" valign="baseline" colspan="2">
            &nbsp;<%--<asp:TextBox ID="Txtamount" runat="server" CssClass="InputDefaultStyle"></asp:TextBox>--%>
            </td>
            <td align="left" valign="baseline" style="width: 193px">
            </td>
            <td align="left" valign="baseline" style="width: 11px">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
            <td colspan="2">
                <asp:Button ID="btn_save" runat="server" Text="save" OnClick="btn_save_Click" 
                    OnClientClick="return fn_save();" Width="60px" />
            </td>
            <td style="width: 193px">
            </td>
            <td style="width: 11px">
            </td>
        </tr>
    </table>
    <asp:GridView ID="grd_earnings" runat="server" AutoGenerateColumns="False" Width="75%"
        DataKeyNames="EarningsId" BackColor="LightGoldenrodYellow" BorderColor="Tan"
        BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" OnRowEditing="edit"
        OnRowUpdating="update">
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <table class="dItemListContentTable" cellspacing="0" cellpadding="0" width="100%">
                        <colgroup>
                            <col>
                        </colgroup>
                        <thead>
                            <tr>
                                <th style="width: 5%;">
                                    EID</th>
                                <th style="width: 15%;">
                                    Date</th>
                                <th style="width: 25%;">
                                    Employeecode</th>
                                <th style="width: 25%;">
                                    EarningsCode</th>
                                <th style="width: 25%;">
                                    Amount</th>
                                <th style="width: 5%;">
                                    &nbsp;</th>
                            </tr>
                        </thead>
                    </table>
                </HeaderTemplate>
                <ItemTemplate>
                    <table class="dItemListContentTable" cellspacing="0" cellpadding="0" width="100%">
                        <colgroup>
                            <col class="dInboxContentTableCheckBoxCol">
                        </colgroup>
                        <tbody>
                            <tr>
                                <td style="width: 5%;" nowrap>
                                    <asp:Label runat="server" Text='<%#Eval("EmployeeId")%>' ID="grdempid" Visible="true"></asp:Label>
                                </td>
                                <td style="width: 15%;" nowrap>
                                    <asp:Label runat="server" Text='<%#Eval("d_date","{0:dd/MM/yyyy}")%>' ID="grddate" Visible="true"></asp:Label>
                                </td>
                                <td style="width: 25%;" nowrap>
                                    <asp:TextBox runat="server" Text='<%#Eval("EmployeeCode")%>' ID="grdempcode" Enabled="false"></asp:TextBox>
                                </td>
                                <td style="width: 25%;" nowrap>
                                    <asp:TextBox runat="server" Text='<%#Eval("EarningsCode")%>' ID="grdearncode" Enabled="false"></asp:TextBox>
                                </td>
                                <td style="width: 25%;" nowrap>
                                <input type="text" runat="server" id="grdamount" onkeydown="AllowOnlyNumeric1(event);" value='<%#Eval("Amount")%>' disabled="disabled" />
                                    <%--<asp:TextBox runat="server" Text='<%#Eval("Amount")%>' ID="grdamount" Enabled="false"></asp:TextBox>--%>
                                </td>
                                <td align="center" style="width: 5%;" nowrap>
                                    <asp:ImageButton ID="img_update" ImageUrl="../Images/i_Edit.gif" runat="server" Style="border: 0"
                                        AlternateText="" CommandName="update" />
                                    <asp:ImageButton ID="img_save" ImageUrl="../Images/save.gif" runat="server" Style="border: 0"
                                        AlternateText="" CommandName="edit" Visible="false" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="Tan" />
        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
        <HeaderStyle BackColor="Tan" Font-Bold="True" />
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
    </asp:GridView>
    </td></tr>
    </table>
</asp:Content>
