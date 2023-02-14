<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master_Page/EmployeeMaster.master" CodeFile="Bench_Cost_Analysis.aspx.cs" Inherits="PayrollReports_Bench_Cost_Analysis" %>
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
                    style="font-family: calibri; font-size: medium; font-weight: bold; color: #FFFFFF">&nbsp;&nbsp;<img src="../Images/rp_arrow.gif" />&nbsp;Bench Cost Analysis Report</span></td>
            <td height="30px" align="center" bgcolor="#5D7B9D" width="20%">
                &nbsp;<asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" CssClass="InputDefaultStyle"
                    >
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
                                    <td align="center" class="QryTitlereports" style="width: 44%">
                                        <asp:Label ID="lbl_error" runat="server" Text="Welcome To Report Section" ForeColor="Red" Font-Bold="True" Width="75%"
                                            Font-Size="Medium"></asp:Label></td>
                                    <td align="center" class="QryTitlereports" width="40%">

                                        <asp:Label ID="Label1" runat="server" ForeColor="Red" Font-Bold="True" Width="75%"
                                            Font-Size="Medium"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td runat="server" id="div_chk_Empcode" height="30" Visible="false" valign="top" style="width: 44%;font-size:10pt;"
                                                        align="center">
                                                        <div class="qrychkbox_big" style="height: 230px; left: 0px; top: 0px;" id="div_chkempcode" visible="false" runat="server">
                                                            <asp:CheckBoxList Height="200px" ID="chk_Empcode"  runat="server" CssClass="InputDefaultStyle1"
                                                                Width="90%">
                                                               
                                                            </asp:CheckBoxList>
                                                        </div>
                                                        <input type="checkbox" visible="false" id="chkall" runat="server" onclick="javascript: fn_chkall(this.id,'ctl00_ContentPlaceHolder1_chk_Empcode')" />
                                        <asp:Label ID="lbl_selectemp" runat="server" Visible="false" Text="Select All Employees"></asp:Label> 
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
                                                         AutoPostBack="True" onselectedindexchanged="ddl_type_SelectedIndexChanged" 
                                                         CssClass="InputDefaultStyle">
                                                         <asp:ListItem>Select</asp:ListItem>
                                                         <asp:ListItem>Bench Report</asp:ListItem> 
                                                         <asp:ListItem>Cost Analaysis Report</asp:ListItem>
                                                         <asp:ListItem>Graphical Representation Report</asp:ListItem>
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
                                                         onselectedindexchanged="ddl_department_SelectedIndexChanged" 
                                                         CssClass="InputDefaultStyle1">
                                                          <asp:ListItem>Select</asp:ListItem></asp:DropDownList>
                                                   
                                                    </td>
                                            </tr>
                                            <tr id="Tr3" runat="server" visible="false">
                                                <td class="dComposeItemLabel1" style="width: 162px">
                                                    <p style="margin-left: 50px">
                                                    <asp:Label ID="lbl_Emp" runat="server" Text="Select Employee"></asp:Label>
                                                    </p>
                                                </td>
                                                <td align="left">
                                                     <asp:DropDownList ID="ddl_employee" Width="175px" runat="server" 
                                                         AutoPostBack="True" CssClass="InputDefaultStyle1" 
                                                         onselectedindexchanged="ddl_employee_SelectedIndexChanged">
                                                          <asp:ListItem>Select</asp:ListItem></asp:DropDownList>
                                                    </td>
                                            </tr>
                                             <tr id="Tr2" visible="false" runat="server">
                                                <td  class="dComposeItemLabel1" style="width: 162px">
                                                    <p style="margin-left: 50px">
                                                    <asp:Label ID="Label3" runat="server" Text="No of days"></asp:Label>
                                                    </p>
                                                 </td>
                                                <td align="left" class="dComposeItemLabel1">
                                                <asp:TextBox ID="txt_days" runat="server" style="Width:170px;" 
                                                        CssClass="InputDefaultStyle1"></asp:TextBox>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:CheckBox ID="chk_successive"  runat="server" Text="Continuous Days" />
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
                                                        <asp:CheckBox ID="chkoverheading"  runat="server" 
                                                        Text="Include O/H Cost " />
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
                                                        &nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    
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

