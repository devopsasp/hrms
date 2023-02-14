<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" MasterPageFile="~/Master_Page/EmployeeMaster.master" CodeFile="PF_NomineeDetails_Rpt.aspx.cs" Inherits="PayrollReports_PF_NomineeDetails_Rpt" %>


<%@ Register
    Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>
 <script language="javascript" type="text/javascript">
 function check()
{	
	
//	var msg="Please make sure the following fields are valid \n\n";
	var key="";
	
	if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$ddl_dept.value)) 
		{
		key+=" Select Department \n";
		}
		if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$ddl_Employee.value)) 
		{
		key+=" select Employee \n";
		}
}	

function openPage()
{
var answer = window.showModalDialog("Test.ASPX","dialogWidth:300px; dialogHeight:200px; center:yes");
}	
 </script>
 
    <div>
  <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td class="tdComposeHeader" valign="top" align="right">

                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" height="35px" bgcolor="#5D7B9D" style="width: 694px">
                            <span class="Title" 
                                style="font-family: calibri; font-size: medium; font-weight: bold; color: #FFFFFF">&nbsp;<img src="../Images/rp_arrow.gif" />&nbsp;PF Nominee Details<span 
                                class="style82"> Report</span></span></td>
                        <td align="center" height="35px" bgcolor="#5D7B9D">
                            <asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" CssClass="InputDefaultStyle"
                    >
                </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center" width="80%">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;<asp:Label ID="lbl_Error" runat="server" ForeColor="Red" Font-Bold="True" 
                                Font-Names="Calibri"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                        </td>
                    </tr>
                </table>
                <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                            <td style="height: 29px; width: 432px;" align="right" class="style89">
                                <a name=tab151>
                                <span class="style84" style="font-family: Calibri;
        font-size: x-small;">
                                Select Department</span></a></td>
                            <td align="justify" class="style92">
                              &nbsp;&nbsp;&nbsp;
                                <a name=tab158>  
                                <asp:DropDownList ID="ddl_dept" runat="server" CssClass="InputDefaultStyle" 
                                ForeColor="#666666" Height="18px" Width="181px" 
                                    onselectedindexchanged="ddl_dept_SelectedIndexChanged" AutoPostBack="True">
                                 <asp:ListItem Value="1" Text="Select"></asp:ListItem>
                               
                            </asp:DropDownList>
                </a>
                            &nbsp;</td>
                          
                                            
                </tr>
                <tr>
                  <td style="height: 29px; width: 432px;" class="style89" align="right">
                                <a name=tab152><span style="font-family: Calibri;
        font-size: x-small;">&nbsp;Select Employee&nbsp; </span><span 
                                class="style89">&nbsp;</span></a> </td>
                            <td style="height: 29px" align="justify">
                                &nbsp;&nbsp;&nbsp; <a name=tab159><asp:DropDownList ID="ddl_Employee" 
                                    runat="server" CssClass="InputDefaultStyle" 
                                ForeColor="#666666" Height="18px" Width="181px" >
                                 <asp:ListItem Value="1" Text="Select"></asp:ListItem>
                              
                            </asp:DropDownList>
                </a>
                                </a>&nbsp; </td>
                </tr>
                <tr>
                  <td style="height: 29px; width: 432px;" class="style89" align="right">
                                &nbsp;</td>
                            <td style="height: 29px" align="justify">
                                &nbsp;</td>
                </tr>
                </table>
       <table cellpadding="0px" cellspacing="0px" width="100%">
       <tr><td align="center" >
        <asp:ImageButton ID="btn_save" runat="server" 
            ImageUrl="../Images/Show_Report.jpg" OnClientClick="return check()"  
               Height="20px" onclick="btn_save_Click" />
           </td></tr>
    <tr><td>
       </table>
                
    </div>
</asp:Content>
