<%@ Page Language="C#" MasterPageFile="~/Hrms_Company/CompanyMaster.master" AutoEventWireup="true" CodeFile="Recruitment_Entry.aspx.cs" Inherits="Hrms_Company_Default" Title="Welcome to HRMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table width="100%" height="100%" cellpadding="0" cellspacing="0">
  <tr valign="top">
    <td id="tdComposeHeader" valign="top">
    <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td height="35px" class="border"><SPAN class="Title">&nbsp;&nbsp;Recruitment Entry</SPAN></td>
                </tr>
             </table>
   
   <table width="100%" class="Content" cellpadding="5px" cellspacing="1px" id="tab1">
             
             <tr>
                <td colspan="4" align="center">&nbsp;<asp:Label ID="lbl_Error" runat="server" ForeColor="Red" 
Font-Bold="True"></asp:Label></td>
           
             </tr>
                <tr runat="server" id="row_pwd2">
                    <td class="dComposeItemLabel" nowrap>
                        Employee Code</td>
                    <td><input class="InputDefaultStyle" runat="server" id="txtuserid" type="text" /></td>
                    <td class="dComposeItemLabel" nowrap><font color="red" size="3"></font>
                    </td>
                    <td></td>
                </tr>
       <tr id="Tr1" runat="server">
           <td class="dComposeItemLabel" nowrap="nowrap">
               Password</td>
           <td>
               <input type="password" runat="server" id="txtpassword" class="InputDefaultStyle" /></td>
           <td colspan="2" nowrap="nowrap">
                                </td>
       </tr>
             <tr>
                 <td class="dComposeItemLabel"></td>
                 <td align="center"><span style="color: #ff0000">
                     <asp:Label ID="lblmsg" runat="server" Width="166px"></asp:Label></span></td>
                 <td class="dComposeItemLabel"></td>
                 <td>
                     &nbsp;</td>
             </tr>
                        <tr runat="server" id="row_bottom" >
                            <td colspan="4" align="right">
                                <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/Images/Back.png" onmouseover="this.src='../Images/Backover.png';" onmouseout="this.src='../Images/Back.png';"
                                    OnClick="btn_Back_Click" />
                                <asp:ImageButton id="btn_clear" ImageUrl="~/Images/Clear.png" runat="server" onmouseover="this.src='../Images/Clearover.png';" onmouseout="this.src='../Images/Clear.png';"
                                    OnClientClick="clearAll()" />
                               <asp:ImageButton ID="btn_save" runat="server" ImageUrl="~/Images/Save.png" onmouseover="this.src='../Images/Saveover.png';" onmouseout="this.src='../Images/Save.png';"
                                    OnClick="btn_save_Click" />
                                </td>
                        </tr>
         </table>   
        </td>
      </tr>
    </table>    
</asp:Content>

