<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Master_Page/EmployeeMaster.master" CodeFile="ProjectDetail.aspx.cs" Inherits="PayrollReports_ProjectDetail" %>

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
    

    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td height="30px" align="left" bgcolor="#5D7B9D" width="80%">
                <span class="Title" 
                    style="font-family: calibri; font-size: medium; font-weight: bold; color: #FFFFFF">&nbsp;&nbsp;<img src="../Images/rp_arrow.gif" />&nbsp;Bench Projection Report</span></td>
            <td height="30px" align="center" bgcolor="#5D7B9D">
                &nbsp;<asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" CssClass="InputDefaultStyle">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="0" cellspacing="0" id="tbl_pfreport" runat="server">
        <tr valign="top">
            <td class="tdComposeHeader" valign="top" align="right">
                <table width="100%">
                    <tr>
                        <td style="width: 20%" valign="top">
                            <table cellpadding="5" cellspacing="1" width="100%" border="1">
                                <%--bordercolor="#e5a81a"--%>
                                <tr>
                                    <td align="center" class="QryTitlereports" style="width: 39%">
                                        <asp:Label ID="lbl_error" runat="server" Text="Welcome To Report Section" ForeColor="Red" Font-Bold="True" Width="75%"
                                            Font-Size="Medium"></asp:Label></td>
                                    <td align="center" class="QryTitlereports" width="40%">

                                        <asp:Label ID="Label1" runat="server" ForeColor="Red" Font-Bold="True" Width="75%"
                                            Font-Size="Medium"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td runat="server" visible="false" id="div_chk_Empcode" height="30" 
                                        valign="top" style="width: 39%; font-size:10pt;"
                                                        align="center">
                                                        <div class="qrychkbox_big" style="height: 230px; left: 0px; top: 0px;" id="div_chkempcode" visible="false" runat="server">
                                                            <asp:CheckBoxList Height="200px" ID="chk_Empcode"  runat="server" CssClass="InputDefaultStyle1"
                                                                Width="90%">
                                                               
                                                            </asp:CheckBoxList>
                                                        </div>
                                                        <input type="checkbox" visible="false" id="chkall" runat="server" onclick="javascript: fn_chkall(this.id,'ctl00_ContentPlaceHolder1_chk_Empcode')" />
                                        <asp:Label ID="lbl_selectemp" Visible="false" runat="server" Text="Select All Employees"></asp:Label> 
                                                    </td>
                                    <td runat="server" id="div_chk_Master" valign="top" align="center" width="40%">
                                        <table width="100%" cellpadding="7">
                                                                                         <tr id="Tr1" runat="server">
                                                <td class="dComposeItemLabel1" style="width: 162px">
                                                    <p style="margin-left: 50px">
                                                    <asp:Label ID="Label2" runat="server" Text="Select Type"></asp:Label>
                                                    </p>
                                                </td>
                                                <td align="left">
                                                     <asp:DropDownList ID="ddl_type" Width="175px" runat="server" 
                                                         AutoPostBack="True" CssClass="InputDefaultStyle1" 
                                                         onselectedindexchanged="ddl_type_SelectedIndexChanged">
                                                          <asp:ListItem>Select</asp:ListItem>
                                                          <asp:ListItem>Employee Vs Bench</asp:ListItem>
                                                          <asp:ListItem>Bench Vs OverHeading</asp:ListItem>
                                                          </asp:DropDownList>
                                                </td>
                                            </tr>                                                                               
                                            <tr id="row_EpfAcno" runat="server">
                                                <td class="dComposeItemLabel1" style="width: 162px">
                                                    <p style="margin-left: 50px">
                                                    <asp:Label ID="lbl_dept" runat="server" Text="Select Department"></asp:Label>
                                                    </p>
                                                </td>
                                                <td align="left">
                                                     <asp:DropDownList ID="ddl_department" Width="175px" runat="server" 
                                                         AutoPostBack="True" 
                                                         
                                                         CssClass="InputDefaultStyle1" 
                                                         onselectedindexchanged="ddl_department_SelectedIndexChanged">
                                                          <asp:ListItem>Select</asp:ListItem></asp:DropDownList>
                                                   
                                                    </td>
                                            </tr> 
                                          <tr id="Tr2" runat="server" visible="false">
                                                <td class="dComposeItemLabel1" style="width: 162px">
                                                    <p style="margin-left: 50px">
                                                    <asp:Label ID="Label3" runat="server" Text="Select Employee"></asp:Label>
                                                    </p>
                                                </td>
                                                <td align="left">
                                                     <asp:DropDownList ID="ddl_Employee" Width="175px" runat="server" 
                                                         AutoPostBack="True" 
                                                         
                                                         CssClass="InputDefaultStyle1" >
                                                        
                                                          </asp:DropDownList>
                                                   
                                                    </td>
                                            </tr>                                                                                         
                                            <tr id="row_PfAcno" runat="server">
                                                <td  class="dComposeItemLabel1" style="width: 162px">
                                                    <p style="margin-left: 50px">
                                                    <asp:Label ID="lbl_from_date" runat="server" Text="From Date"></asp:Label>
                                                    </p>
                                                </td>
                                                <td align="left" class="dComposeItemLabel1">
                                                <asp:TextBox ID="txtFromDate" runat="server" onkeyup="fn_date(event,this.id);" 
                                                        MaxLength="10" style="Width:170px;" CssClass="InputDefaultStyle1"></asp:TextBox>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                            </tr>
                                            <tr id="row_DliAcno" runat="server">
                                                <td class="dComposeItemLabel1" style="width: 162px">
                                                    <p style="margin-left: 50px">
                                                    <asp:Label ID="lbl_toDate" runat="server" Text="To Date"></asp:Label>
                                                    </p>
                                                </td>
                                                <td align="left">
                                                <asp:TextBox ID="txtToDate" runat="server" onkeyup="fn_date(event,this.id);" 
                                                        MaxLength="10" style="Width:170px;" CssClass="InputDefaultStyle1"></asp:TextBox>
                                                        &nbsp;&nbsp;
                                                    <asp:CheckBox ID="chkoverheading" Visible="false"  runat="server" 
                                                        Text="Include O/H Cost " Font-Names="Calibri" Font-Size="X-Small" />
                                                    </td>
                                                    
                                            </tr>
                                                <tr id="Tr3" runat="server" visible="false">
                                                <td class="dComposeItemLabel1" style="width: 162px">
                                                    <p style="margin-left: 50px">
                                                    <asp:Label ID="Label4" runat="server" Text="Work From Home Cost"></asp:Label>
                                                    </p>
                                                </td>
                                                <td align="left">
                                                <asp:TextBox ID="txtwork" runat="server" 
                                                        MaxLength="10" style="Width:170px;" CssClass="InputDefaultStyle1"></asp:TextBox>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    
                                            </tr>
                                            <tr>
                                   <td colspan="100%">
                                <div id="div1" style="position:relative;left:15px;OVERFLOW: auto;WIDTH:95%" >
                                    <asp:GridView ID="GridView2" Width="100%" Font-Size="X-Small"
                                        AutoGenerateColumns="False" Font-Names="Verdana" runat="server" 
                                        ShowFooter="True"
                                        GridLines="None" Visible="False" 
                                        HeaderStyle-HorizontalAlign="Left" onrowdatabound="GridView2_RowDataBound" 
                                        onselectedindexchanged="GridView2_SelectedIndexChanged" 
                                        onrowcommand="GridView2_RowCommand" onrowdeleting="GridView2_RowDeleting" 
                                        CellPadding="4" ForeColor="#333333" Height="221px">
                                        <RowStyle BackColor="#F7F6F3" Font-Size="X-Small" ForeColor="#333333" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True"/>
                                        <FooterStyle BackColor="#5D7B9D" Font-Size="X-Small" Font-Bold="True" 
                                            ForeColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="25px">
                                                <ItemTemplate>
                                                <asp:TextBox ID="txtid" Width="100%" Text='<%# Eval("id") %>' runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </ItemTemplate>
                                             
<HeaderStyle Width="25px"></HeaderStyle>
                                             
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="Allowance" HeaderStyle-Width="200px">
                                                <ItemTemplate>
                                                <asp:TextBox ID="txtename" Width="100%" Text='<%# Eval("allowance") %>' runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                <asp:TextBox ID="txteditename" Width="100%" Text='<%# Eval("allowance") %>' runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="txtallowance" Width="100%" runat="server" BackColor="#FFCCCC"></asp:DropDownList>
                                                </FooterTemplate>
                                                
<HeaderStyle Width="200px"></HeaderStyle>
                                                
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                <asp:TextBox ID="txtvalue" Width="100%" Text='<%# Eval("amt") %>' runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                <asp:TextBox ID="txteditvalue" Width="100%" Text='<%# Eval("amt") %>' runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtvalue" Text='' Width="100%" runat="server"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                                                       
			                                <asp:TemplateField HeaderText="Delete">
                                                 <ItemTemplate>
                                                    <asp:LinkButton ID="linkDeleteCust" CommandName="Delete" runat="server" ForeColor="#5D7B9D">Delete</asp:LinkButton>
                                                 </ItemTemplate>
                                                 <FooterTemplate>
                                                    <asp:LinkButton ID="linkAddOrder" CommandName="Add" runat="server" ForeColor="#5D7B9D">Add</asp:LinkButton>
                                                 </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#999999" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                   </asp:GridView>
                                </div>
                             </td>
                                            </tr>
                                         </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="TextStyle" align="center">
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td align="center" style="width: 20%; height: 20px" valign="middle">
                                            <asp:ImageButton ID="btn_Report" runat="server" 
                                                ImageUrl="../Images/Show_Report.jpg" onclick="btn_Report_Click"
                                                 /></td>
                                    </tr>

                                </tbody>
                            </table>

                        </td>
                    </tr>
                </table>
                <input type="hidden" id="ddl_selrep" runat="server" /></td>
        </tr>
    </table>

</asp:Content>
