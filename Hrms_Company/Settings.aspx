<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Settings.aspx.cs" Inherits="Hrms_Company_Default" Title="Welcome to HRMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
.VeryPoorStrength
{
/*background: Red;*/
color:Red;
font-weight:bold;
}
.WeakStrength
{
/*background: Gray;*/
color:Gray;
font-weight:bold;
}
.AverageStrength
{
/*background: orange;*/
color:orange;
font-weight:bold;
}
.GoodStrength
{
/*background: blue;*/
color:blue;
font-weight:bold;
}
.ExcellentStrength

{
/*background: Green;*/
color:Green;
font-weight:bold;
}
.BarBorder
{
border-style: solid;
border-width: 1px;
width: 180px;
padding:2px;
}
</style>
    <script language="javascript" type="text/javascript">

function clearAll()
    {     
  
    //document.aspnetForm.ctl00$ContentPlaceHolder1$txtoldpwd.value = "";
    document.aspnetForm.ctl00$ContentPlaceHolder1$txtoldpwd.value = "";
    document.aspnetForm.ctl00$ContentPlaceHolder1$txtNewpwd.value="";  
    document.aspnetForm.ctl00$ContentPlaceHolder1$txtConfirmpwd.value="";  
    
    }
    
 
    function show_message(msg)    
    {
        alert(msg);
    }
    
    
    function test() 
    { 
    
    }


          
       
function chk()
    {
    var l_pwd=document.aspnetForm.ctl00$ContentPlaceHolder1$h_Login_pwd.value;
    var e_pwd=document.aspnetForm.ctl00$ContentPlaceHolder1$txtoldpwd.value;
    
    if(l_pwd!=e_pwd)
    {
   
    document.aspnetForm.ctl00$ContentPlaceHolder1$msg.value='Current Password Was not Correct';
    
    }
    
    
    
 if(document.aspnetForm.ctl00$ContentPlaceHolder1$txtoldpwd.value!="" && document.aspnetForm.ctl00$ContentPlaceHolder1$txtNewpwd.value!="" && document.aspnetForm.ctl00$ContentPlaceHolder1$txtConfirmpwd.value!="" && l_pwd==e_pwd)   
   {   
   
    return true;    
   
   }
else
    {
    
    return false;
    
    } 
 }

    </script>


    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    <div><h3 class="page-header">Change Password</h3></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div style="width: 50%">
                    <table cellpadding="1%" cellspacing="1%" width="100%" class="table table-striped table-bordered table-hover">
                        <tr>
                            <td>
                                Current Password</td>
                            <td>
                                <asp:TextBox type="password" runat="server" id="txtoldpwd" 
                                    CssClass="form-control" onclick="return txtoldpwd_onclick()" 
                                    ontextchanged="txtoldpwd_TextChanged"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ControlToValidate="txtoldpwd" ErrorMessage="Enter Current Password" 
                                    Font-Names="Calibri"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                New Password</td>
                            <td>
                                <asp:TextBox ID="txtNewpwd" runat="server" 
                                    CssClass="form-control" TextMode="Password"></asp:TextBox>
                                <asp:PasswordStrength ID="pwdStrength" runat="server" 
                                    PreferredPasswordLength="8" PrefixText="Strength:" StrengthIndicatorType="Text" 
                                    TargetControlID="txtNewpwd" 
                                    TextStrengthDescriptions="Very Poor;Weak;Average;Good;Excellent" 
                                    TextStrengthDescriptionStyles="VeryPoorStrength;WeakStrength;AverageStrength;GoodStrength;ExcellentStrength"
                                    CalculationWeightings="25;25;15;35" 
                                    >
                                </asp:PasswordStrength>
                                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="txtNewpwd" ErrorMessage="Enter New Password" 
                                    Font-Names="Calibri"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Retype Password</td>
                            <td >
                                <input type="password" runat="server" id="txtConfirmpwd" class="form-control" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                    ControlToValidate="txtConfirmpwd" ErrorMessage="Enter Confirm Password " 
                                    Font-Names="Calibri"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btn_save" runat="server" class="btn btn-success" 
                                    onclick="btn_save_Click" Text="Save"/>
                               </td>
                            <td>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                    ControlToCompare="txtNewpwd" ControlToValidate="txtConfirmpwd" 
                                    ErrorMessage="Passwords does not match" Font-Names="Calibri"></asp:CompareValidator>
                            </td>
                        </tr>
                     </table>   
                     </div>
                     </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
