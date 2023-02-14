<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master_Page/EmployeeMaster.master" CodeFile="EncashmentReport.aspx.cs" Inherits="PayrollReports_EncashmentReport" %>
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
		if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$ddl_Year.value)) 
		{
		key+=" select Year \n";
		}
}		
 </script>
    <div>
    <table width="1024">
     <tr><td height="30px" align="left" bgcolor="#5D7B9D" width="80%">
                <span class="Title" 
                    style="font-family: calibri; font-size: medium; font-weight: bold; color: #FFFFFF">&nbsp;&nbsp;<img src="../Images/rp_arrow.gif" />&nbsp;Encashment Report</span></td>
         <td height="30px" align="center" bgcolor="#5D7B9D">
                            &nbsp;<asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" CssClass="InputDefaultStyle" OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td></tr>
    <tr><td style="text-align: center">
        <asp:Label ID="lbl_Error" runat="server" Text="lblError" Font-Names="Calibri"></asp:Label></td></tr>
    <tr><td style="text-align: center" align="center">
        &nbsp;</td></tr>
         <tr>
    <td style="font-family: Calibri">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        Select Department&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;           
    <asp:DropDownList ID="ddl_dept" runat="server" Width="193px" 
            AutoPostBack="True" onselectedindexchanged="ddl_dept_SelectedIndexChanged">
             <asp:ListItem>Select</asp:ListItem>
    </asp:DropDownList></td>
    </tr>
             <tr>
    <td style="font-family: calibri">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        Select Year&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;           
    <asp:DropDownList ID="ddl_Year" runat="server" Width="193px" 
            AutoPostBack="True">
             <asp:ListItem>Select</asp:ListItem>
    </asp:DropDownList></td>
    </tr>
         <tr>
    <td align="center">&nbsp;</td>
    </tr>
    <tr>
                   <td runat="server" id="div_chk_Empcode" height="30" valign="top" style="width: 44%;font-size:10pt;"
                                                        align="center">
                                                        <div class="qrychkbox_big" style="height: 230px; left: 0px; top: 0px;" id="div_chkempcode" runat="server">
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:CheckBoxList Height="200px" ID="chk_Empcode" runat="server" CssClass="InputDefaultStyle1"
                                                                Width="90%">
                                                                <%--<asp:ListItem></asp:ListItem>--%>
                                                            </asp:CheckBoxList>
                                                        </div>
                                                        <input type="checkbox" id="chkall" runat="server" onclick="javascript: fn_chkall(this.id,'ctl00_ContentPlaceHolder1_chk_Empcode')" />
                                        <asp:Label ID="lbl_selectemp" runat="server" Text="Select All Employees" Font-Names="Calibri"></asp:Label> 
                                                    </td>
    </tr>
    <tr><td align="center">
        <asp:ImageButton ID="btn_save" runat="server" 
            ImageUrl="../Images/Show_Report.jpg" OnClientClick="return check()"  Height="20px" onclick="btn_save_Click" /></td></tr>
    <tr><td>
    </td></tr>
    </table>
    </div>
</asp:Content>

