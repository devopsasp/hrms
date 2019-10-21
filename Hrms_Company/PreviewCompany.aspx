<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="PreviewCompany.aspx.cs" Inherits="Hrms_Company_Default" Title="Welcome to HRMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table width="100%" height="100%" cellpadding="0" cellspacing="0">
  <tr valign="top">
    <td id="tdComposeHeader" valign="top">
        <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td height="35px" class="border"><SPAN class="Title">&nbsp;&nbsp;Head Office</SPAN></td>
                </tr>
        </table>        
        
       <table id="tab_Preview" runat="server" class="Content" width="100%" cellpadding="5" cellspacing="1">
             <tr>
                 <td colspan="6" align="center">&nbsp;<asp:Label ID="lbl_Error" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label></td>
             </tr>
             <tr>
                 <td class="dComposeItemLabel" nowrap style="width: 72px" >&nbsp;</td>
                 <td class="dComposeItemLabel1" nowrap >Company Code &nbsp;</td>
                 <td class="dComposePreviewVal"></td>
                 <td class="dComposeItemLabel" nowrap>&nbsp;</td>
                 <td class="dComposeItemLabel1" nowrap>Company Name &nbsp;&nbsp;</td>
                 <td class="dComposePreviewVal"></td>
             </tr>                        
             <tr>
                 <td class="dComposeItemLabel" nowrap style="width: 72px">&nbsp;</td>
                 <td class="dComposeItemLabel1" nowrap>Head Office Code &nbsp;&nbsp;</td>
                 <td class="dComposePreviewVal"></td>
                 <td class="dComposeItemLabel" nowrap>&nbsp;</td>
                 <td class="dComposeItemLabel1" nowrap>Head Office Name &nbsp;&nbsp;</td>
                 <td class="dComposePreviewVal"></td>
             </tr>
             <tr>
                 <td class="dComposeItemLabel" nowrap style="width: 72px">&nbsp;</td>
                 <td class="dComposeItemLabel1" nowrap>Address Line 1 &nbsp;&nbsp;</td>
                 <td class="dComposePreviewVal"></td>
                 <td class="dComposeItemLabel" nowrap>&nbsp;</td>
                 <td class="dComposeItemLabel1" nowrap>Address Line2 &nbsp;&nbsp;</td>
                 <td class="dComposePreviewVal"></td>
             </tr>                        
             <tr>
                 <td class="dComposeItemLabel" nowrap style="width: 72px">&nbsp;</td>
                 <td class="dComposeItemLabel1" nowrap>City &nbsp;&nbsp;</td>
                 <td class="dComposePreviewVal"></td>
                 <td class="dComposeItemLabel" nowrap>&nbsp;</td>
                 <td class="dComposeItemLabel1" nowrap>ZipCode &nbsp;&nbsp;</td>
                 <td class="dComposePreviewVal"></td>
             </tr>
             <tr>
                 <td class="dComposeItemLabel" nowrap style="width: 72px">&nbsp;</td>
                 <td class="dComposeItemLabel1" nowrap>Country &nbsp;&nbsp;</td>
                 <td class="dComposePreviewVal"></td>
                 <td class="dComposeItemLabel" nowrap>&nbsp;</td>
                 <td class="dComposeItemLabel1" nowrap>State &nbsp;&nbsp;</td>
                 <td class="dComposePreviewVal"></td>
             </tr>
            <tr>
                <td class="dComposeItemLabel" nowrap style="width: 72px">&nbsp;</td>
                <td class="dComposeItemLabel1" nowrap>Phone No &nbsp;&nbsp;</td>
                <td class="dComposePreviewVal"></td>
                <td class="dComposeItemLabel" nowrap>&nbsp;</td>
                <td class="dComposeItemLabel1" nowrap>Fax No &nbsp;&nbsp;</td>
                <td class="dComposePreviewVal"></td>
            </tr>
            <tr>
                <td class="dComposeItemLabel" nowrap style="width: 72px">&nbsp;</td>
                <td class="dComposeItemLabel1" nowrap>Email Id &nbsp;&nbsp;</td>
                <td class="dComposePreviewVal"></td>
                <td class="dComposeItemLabel" nowrap>&nbsp;</td>
                <td class="dComposeItemLabel1" nowrap>Alternate EmailId &nbsp;&nbsp;</td>
                <td class="dComposePreviewVal"></td>
            </tr>                        
            <tr>
                <td class="dComposeItemLabel" nowrap style="width: 72px">&nbsp;</td>
                <td class="dComposeItemLabel1" nowrap>PF number</td>
                <td class="dComposePreviewVal">&nbsp;</td>
                <td class="dComposeItemLabel" nowrap>&nbsp;</td>
                <td class="dComposeItemLabel1" nowrap>ESI number</td>
                <td class="dComposePreviewVal">&nbsp;</td>
            </tr>                        
            <tr>
                <td class="dComposeItemLabel" nowrap style="width: 72px">&nbsp;</td>
                <td class="dComposeItemLabel1" nowrap>Starting Date</td>
                <td class="dComposePreviewVal">&nbsp;</td>
                <td class="dComposeItemLabel" nowrap>&nbsp;</td>
                <td class="dComposeItemLabel1" nowrap>Ending Date</td>
                <td class="dComposePreviewVal">&nbsp;</td>
            </tr>                        
            <tr>
                <td class="dComposeItemLabel" colspan="2" >&nbsp;</td>
                <td style="text-align: center" colspan="3"><span style="color: red"></span></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                 <td colspan="6">&nbsp;</td>
            </tr>
           <tr>
               <td colspan="6" align="right">
                    <asp:ImageButton ID="btn_update" runat="server" ImageUrl="~/Images/Modify.png" onmouseover="this.src='../Images/Modifyover.png';" onmouseout="this.src='../Images/Modify.png';"
                        OnClick="btn_update_Click" />&nbsp;
                    <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/Images/Back.png" onmouseover="this.src='../Images/Backover.png';" onmouseout="this.src='../Images/Back.png';"
                        OnClick="btn_Back_Click" />
               </td>
           </tr>
          </table>       
        </td>
      </tr>
    </table>

</asp:Content>

