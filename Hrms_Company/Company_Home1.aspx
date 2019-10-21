<%@ Page MaintainScrollPositionOnPostback="true" Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Company_Home1.aspx.cs" Inherits="Hrms_Company_Default" Title="Welcome to HRMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
 
<head id="Head1" runat="server">
  <style type="text/css">
.blink
{

text-decoration:blink
}
</style>
 
<style type="text/css">
.highlightRow
{
background-color:#ffeb95;
text-decoration:underline;
cursor:pointer;
}
</style>


 <link href="../_assets/css/meiii.css" rel="stylesheet" type="text/css" />
 <link href="../Themes/js-image-slider.css" rel="stylesheet" type="text/css" />
    <script src="../Themes/js-image-slider.js" type="text/javascript"></script>
  <link href="../Css/GridViewStyles.css" rel="stylesheet" type="text/css" />
  <link href="../Css/GridViewStyles1.css" rel="stylesheet" type="text/css" />
    <%--<script runat="server">

      
</script>--%>

<script type="text/javascript">

function check_uncheck(Val)
{
  var ValChecked = Val.checked;
  var ValId = Val.id;
  var frm = document.forms[0];
  for (i = 0; i < frm.length; i++)
  {
    if (this != null)
    {
      if (ValId.indexOf('CheckAll') !=  - 1)
      {
        if (ValChecked)
          frm.elements[i].checked = true;
        else
          frm.elements[i].checked = false;
      }
      else if (ValId.indexOf('deleteRec') !=  - 1)
      {
        if (frm.elements[i].checked == false)
          frm.elements[1].checked = false;
      }
    } 
  } 
} 

function confirmMsg(frm)
{
  // loop through all elements
  for (i = 0; i < frm.length; i++)
  {
    // Look for our checkboxes only
    if (frm.elements[i].name.indexOf("deleteRec") !=  - 1)
    {
      // If any are checked then confirm alert, otherwise nothing happens
      if (frm.elements[i].checked)
      return confirm('Are you sure you want to delete your selection(s)?')
    }
  }
}
</script>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Welcome to HRMS</title>
 <link href="../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />   	
     </head><body bgcolor="gainsboro"><form id="form1" runat="server">


<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
                    <asp:SiteMapDataSource ID="SiteMapDataSource1" SiteMapProvider="Menu1Sitemap"  runat="server" />
                    <asp:SiteMapDataSource ID="SiteMapDataSource2" SiteMapProvider="Menu2Sitemap" runat="server" />
                        <asp:DropDownList ID="DropDownList3" runat="server" Visible="False">
                        </asp:DropDownList>
    <br />
<br />
   <table align="center" cellpadding="0" cellspacing="0" style="height: 100%; width: 90%;">
     <tr>
     <td bgcolor="White" class="style1">
     
         <table align="center" class="style4" 
             style="border: thin groove #47476B; height: 100%">
             <tr>
                 <td width="33%">
                 <img alt="Hesperus Infosys" longdesc="../Images/hesperus-trans.jpg"
                                    src="../Images/hesperus-trans.jpg" 
                         style="height: 60px; width: 140px"/>
                 </td>
                 <td width="33%" align="center">
                     <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Calibri" 
                         Text="Quick Links"></asp:Label>
                     &nbsp;&nbsp;
                     <asp:DropDownList ID="DropDownList1" runat="server" Height="16px" Width="200px">
                     </asp:DropDownList>
                         </td>
                 <td align="right">
                    <img  alt="Quick Links" src="../Images/sub-menu-white.gif" 
                                    style="width: 276px; height: 60px; margin-left: 0px; text-decoration:none;" usemap="#links" border="0" />
                 </td>
                 <map id="quick_links" style="text-decoration:none;" name="links">
                            <area shape="rect" coords="44, 22, 97, 38" alt="Home" href="" />
                            <area shape="rect" coords="113, 22, 174, 36" alt="Site Map" href="" />
                            <area shape="rect" coords="190, 21, 267, 36" alt="Contact" href="" />
                 </map>
             </tr>
         </table>
     
            </td>
        </tr>
        <tr>
            <td align="center" valign="middle" height="250px">
                <div id="sliderFrame" style="width: 99%">
        <div id="slider" style="width: 100%; top: 0px; left: 0px;";>
            <img src="../Images/m1.jpg" alt="Recruitment" />
            <img src="../Images/m2.jpg" alt="Training & Development" />
            <img src="../Images/m3.jpg" alt="Performance Appraisal" />
            <img src="../Images/m4.jpg" alt="Employee Self Service"/>
            <img src="../Images/m5.jpg" alt="Payroll Services" />
        </div>
     
    </div>
            </td>
        </tr>
        <tr>
        <td>
        <asp:Menu CssClass="menuItem" Orientation="Horizontal" StaticSubMenuIndent="0" StaticDisplayLevels="2"
                        MaximumDynamicDisplayLevels="2" ID="Menu1" runat="server" DataSourceID="SiteMapDataSource1"
                        ForeColor="white" DynamicPopOutImageUrl="~/Images/arr_collapsed1.gif"
                        StaticPopOutImageUrl="~/Images/arr_expanded.gif" Width="100%" 
                        Height="31px">
                        <DynamicHoverStyle CssClass="menuItem" Font-Underline="true" ForeColor="white" />
                        <DynamicMenuItemStyle BackColor="#DCDCDC" ForeColor="#43375B" HorizontalPadding="2px"
                            VerticalPadding="2px" Font-Bold="false" />
                        <StaticHoverStyle CssClass="menuItem" />
                        <StaticMenuItemStyle HorizontalPadding="2px" VerticalPadding="2px" Font-Bold="false" />
                    </asp:Menu>
        </td>
        </tr>
        <tr>
            <td>
                                <asp:Panel ID="main_panel" runat="server" Height="356px" Width="100%" 
                                    BackColor="White">
                                <table align="center" style="width: 100%">
                                <tr><td>
                                <asp:RoundedCornersExtender ID="RoundedCornersExtender2" Corners="Left" Radius="5" TargetControlID="panel_announcement" runat="server">
                                </asp:RoundedCornersExtender>
                                 <asp:Panel ID="panel_announcement" runat="server" Height="172px" 
                                         Width="300px" ScrollBars="Vertical" 
                                        GroupingText="Announcement" Font-Bold="True" Font-Names="Calibri">
                                <table style="height:100%; width:280px; font-family: Calibri; font-size: large; background-color:#97ABC1;" 
                                         id ="table_announcements">   
                                        <tr align="center">
                                        <td align="center" style="background-color: #97ABC1">
                                            <asp:Label ID="Lbl_announcements" runat="server"></asp:Label>
                                           
                                        </td>
                                        </tr>
                                        <tr valign="middle">
                                        <td align="center" bgcolor="#97ABC1">
                                      <asp:GridView ID="Grid_announcements" DataKeyNames="announcementid" runat="server" AutoGenerateColumns="False"  CellPadding="8" 
                                                CssClass="MyGridView" Width="100%" Height="70%" onrowcreated="Grid_announcements_RowCreated" onrowcommand="Grid_announcements_RowCommand" >
                                                
                                    
                                                
            <Columns>
            <asp:TemplateField Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblid" Text='<%#Eval("announcementid")%>' runat="server"></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
               <asp:TemplateField HeaderText="Today Announcements">
                <HeaderStyle BackColor="#97ABC1" HorizontalAlign="Center" />       
                            <ItemTemplate>
                           
                                <%--<asp:Label ID="lnk_leave_list" runat="server" Text='<%# Eval("leave") %>'></asp:Label> --%>
                                <asp:LinkButton ID="Lnk_announcements" ForeColor="Black" runat="server" CommandArgument="<%# Container.DataItemIndex %> "  CommandName="cmd"  Text='<%#Eval("Announcements")%>'></asp:LinkButton>
                            </ItemTemplate>
        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
              
            </Columns>
        </asp:GridView>
         </td></tr>
           <tr visible="false" runat="server">
         <td visible="false" runat="server">
             <asp:Label Visible="false" ID="lblid1" runat="server" Text="Id"></asp:Label>  
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <asp:TextBox ID="txt_id" runat="server" Visible="false" Width="175px"></asp:TextBox>
             </td></tr>
         <tr>
         <td>
             <asp:Label ID="Lbl_suject" runat="server" Text="Subject"></asp:Label>  
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox 
                 ID="txt_subject" runat="server" Width="175px"></asp:TextBox>
             </td></tr>
         <tr><td>
             <asp:Label ID="lbl_details" runat="server" Text="Information"></asp:Label>
             <asp:TextBox ID="Txt_details" runat="server" TextMode="MultiLine" 
                 style="top: 792px; left: 137px; height: 36px; width: 181px"></asp:TextBox>
         </td></tr>
         <tr>
         <td>
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <asp:ImageButton ID="Img_btn_publish" runat="server" 
                 ImageUrl="~/Images/Publish.jpg" onclick="Img_btn_publish_Click" Width="100px" /><asp:ImageButton
                     ID="Img_btn_update" runat="server" ImageUrl="~/Images/Update.JPG" 
                 onclick="Img_btn_update_Click" Width="100px" /><asp:ImageButton ID="img_btn_cancel"
                     runat="server" ImageUrl="~/Images/cancelorange.jpg" 
                 onclick="img_btn_cancel_Click" Width="100px" />
             &nbsp;&nbsp;<asp:Label ID="lbl_Error" runat="server" Width="50px"></asp:Label>
          
         </td>
         </tr>     
                                </table>
                                 </asp:Panel>
                                </td>
                                
                                <td style="width:300px;">
                                <asp:RoundedCornersExtender ID="RoundedCornersExtender4" Corners="Left" Radius="5" TargetControlID="requisition_panel" runat="server">
                                </asp:RoundedCornersExtender>
                                <asp:Panel ID="requisition_panel" runat="server" ScrollBars="Vertical" 
                                        Height="172px" Width="300px" Font-Bold="True" 
                                        Font-Names="Calibri" GroupingText="Job Requisition">
                                
                                    <table align="center" id="table_requisition" style="height:100%;width:280px; font-family: Calibri; font-size: large; background-color:#97ABC1;" 
                                        bgcolor="#97ABC1">   
                                        <tr align="center">
                                        <td align="center" style="border-top-style:hidden;" bgcolor="#97ABC1">
                                            <asp:Label ID="Lbl_requisition" runat="server"></asp:Label>
                                           
                                        </td>
                                        </tr>
                                        <tr valign="middle">
                                        <td align="center" bgcolor="#97ABC1">
                                      <asp:GridView ID="Grid_requisition" runat="server" AutoGenerateColumns="False" 
                                                CellPadding="8" 
            CssClass="MyGridView" Width="100%"  DataKeyNames="pn_employeeid" onrowcreated="Grid_requisition_RowCreated" 
                                                onrowcommand="Grid_requisition_RowCommand" 
                                                onrowdatabound="Grid_requisition_RowDataBound" >
                                                
                                    
                                                
            <Columns>
               <asp:TemplateField Visible="false">
                <HeaderStyle BackColor="#97ABC1" HorizontalAlign="Center" />        
                            <ItemTemplate>
                                <asp:Label ID="lnk_leave_list" runat="server" Text='<%#Eval("pn_employeeid")%>'></asp:Label> 
                            </ItemTemplate>
        

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
               <asp:TemplateField HeaderText="JobTitle and Date">
                <HeaderStyle BackColor="#97ABC1" HorizontalAlign="Center" />        
                            <ItemTemplate>
                                <%--<asp:Label ID="lnk_leave_list" runat="server" Text='<%# Eval("leave") %>'></asp:Label> --%>
                                <asp:LinkButton ID="Lnk_requisitions" ForeColor="Black" runat="server"  CommandName="cmd"  Text='<%#Eval("Requisition")%>'></asp:LinkButton>
                            </ItemTemplate>
        

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
            </Columns>
        </asp:GridView>
                                            
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                             <asp:Label ID="Lbl_total_requisitions" runat="server">
                                            </asp:Label>
                                            </td></tr>
                                           
                                </table>
                                    </asp:Panel>
                                </td>
                                
                                <td>
                                <asp:RoundedCornersExtender ID="RoundedCornersExtender10" Corners="Left" Radius="5" TargetControlID="complaints_panel" runat="server">
                                </asp:RoundedCornersExtender>
                                <asp:Panel ID="complaints_panel" runat="server" ScrollBars="Vertical" 
                                        Height="172px" Width="300px" Font-Bold="True" 
                                        Font-Names="Calibri" GroupingText="Queries">
                                
                                    
                                  
                                <table align="center" style="height:100%;width:280px; font-family: Calibri; font-size: large; background-color:#97ABC1;" 
                                        bgcolor="#97ABC1">   
                                        <tr align="center">
                                        <td align="center" style="border-top-style:hidden;" bgcolor="#97ABC1">
                                            <asp:Label ID="Lbl_complaints" runat="server"></asp:Label>
                                           
                                        </td>
                                        </tr>
                                        <tr valign="middle">
                                        <td align="center" bgcolor="#97ABC1">
                                      <asp:GridView ID="Grid_complaints" runat="server" AutoGenerateColumns="False" 
                                               CellPadding="8" 
            CssClass="MyGridView" Width="100%" 
                                               onrowcreated="Grid_complaints_RowCreated" 
                                                onrowcommand="Grid_complaints_RowCommand" 
                                                onrowdatabound="Grid_complaints_RowDataBound">
                                        
            <Columns>
               <%--<asp:TemplateField Visible="false">
                <HeaderStyle BackColor="#97ABC1" HorizontalAlign="Left" />        
                            <ItemTemplate>
                                <asp:Label ID="lnk_complaints_list" runat="server" Text='<%#Eval("pn_employeeid")%>'></asp:Label> 
                            </ItemTemplate>
        

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>--%>
               <asp:TemplateField HeaderText="Name and Complaints">
                <HeaderStyle BackColor="#97ABC1" HorizontalAlign="Left" />        
                            <ItemTemplate>
                                <%--<asp:Label ID="lnk_leave_list" runat="server" Text='<%# Eval("leave") %>'></asp:Label> --%>
                                <asp:LinkButton ID="Lnk_complaints" runat="server" ForeColor="Black"  CommandName="cmd"  Text='<%#Eval("complaints")%>'></asp:LinkButton>
                            </ItemTemplate>
        

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
            </Columns>
        </asp:GridView>
                                            
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                             <asp:Label ID="Lbl_total_complaints" runat="server">
                                            </asp:Label>
                                            </td></tr>
                                           
                                </table>
                                    
                                </asp:Panel>
                                </td>
                               </tr>
                                 <tr>
                                 <td class="style5">
                                 <asp:RoundedCornersExtender ID="RoundedCornersExtender6" Corners="Left" Radius="5" TargetControlID="leave_panel" runat="server">
                                </asp:RoundedCornersExtender>
                                <asp:Panel ID="leave_panel" runat="server" ScrollBars="Vertical" Height="172px" Width="300px" Font-Bold="True" Font-Names="Calibri" 
                                         GroupingText="Leave ">
                                
                                    
                                  
                                <table align="center" style="height:100%; width:280px; font-family: Calibri; font-size: large; background-color:#97ABC1;" 
                                        bgcolor="#97ABC1">   
                                        <tr align="center">
                                        <td style="border-top-style:hidden;" bgcolor="#97ABC1">
                                            <asp:Label ID="Lbl_leave" runat="server"></asp:Label>
                                           
                                        </td>
                                        </tr>
                                        <tr valign="middle">
                                        <td align="center" bgcolor="#97ABC1">
                                      <asp:GridView ID="Grid_leave" runat="server" AutoGenerateColumns="False" 
                                               CellPadding="8" 
            CssClass="MyGridView" Width="100%" OnRowCreated="Grid_leave_RowCreated" onrowcommand="Grid_leave_RowCommand" 
                                                onrowdatabound="Grid_leave_RowDataBound" 
                                                DataKeyNames="pn_employeeid">
                                     
                                                
            <Columns>
               <asp:TemplateField Visible="false">
                <HeaderStyle BackColor="#97ABC1" HorizontalAlign="Left" />        
                            <ItemTemplate>
                                <asp:Label ID="lnk_leave_list" runat="server" Text='<%#Eval("pn_employeeid")%>'></asp:Label> 
                            </ItemTemplate>
        

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
               <asp:TemplateField HeaderText="Name and Date">
                <HeaderStyle BackColor="#97ABC1" HorizontalAlign="Left" />        
                            <ItemTemplate>
                                <%--<asp:Label ID="lnk_leave_list" runat="server" Text='<%# Eval("leave") %>'></asp:Label> --%>
                                <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Black"  CommandName="cmd"  Text='<%#Eval("leave")%>'></asp:LinkButton>
                            </ItemTemplate>
        

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
            </Columns>
        </asp:GridView>
                                            
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                             <asp:Label ID="lbl_total_leave" runat="server">
                                            </asp:Label>
                                            </td></tr>
                                           
                                </table>
                                    
                                </asp:Panel>
                                </td>
                                <td>
                                <asp:RoundedCornersExtender ID="RoundedCornersExtender8" Corners="Left" Radius="5" TargetControlID="remibursement_panel" runat="server">
                                </asp:RoundedCornersExtender>
                                <asp:Panel ID="remibursement_panel" runat="server" ScrollBars="Vertical" 
                                        Height="172px" Width="300px" Font-Bold="True" 
                                        Font-Names="Calibri" GroupingText="Reimbursement">
                                
                                    <table align="center" style="height:100%;width:280px; font-family: Calibri; font-size: large; background-color:#97ABC1;" 
                                        bgcolor="#97ABC1">   
                                        <tr align="center">
                                        <td align="center" style="border-top-style:hidden;" bgcolor="#97ABC1">
                                            <asp:Label ID="Lbl_reimbursement" runat="server"></asp:Label>
                                           
                                        </td>
                                        </tr>
                                        <tr valign="middle">
                                        <td align="center" bgcolor="#97ABC1">
                                      <asp:GridView ID="Grid_reimbursement" runat="server" AutoGenerateColumns="False" 
                                                CellPadding="8" 
            CssClass="MyGridView" Width="100%" 

                                                onrowcreated="Grid_reimbursement_RowCreated" 
                                                onrowcommand="Grid_reimbursement_RowCommand" 
                                                onrowdatabound="Grid_reimbursement_RowDataBound" >
                                                
                                    
                                                
            <Columns>
               <%--<asp:TemplateField Visible="false">
                <HeaderStyle BackColor="#97ABC1" HorizontalAlign="Center" />        
                         <ItemTemplate>
                                <asp:Label ID="lnk_leave_list" runat="server" Text='<%#Eval("pn_employeeid")%>'></asp:Label>
                          </ItemTemplate>
        

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>--%>
               <asp:TemplateField HeaderText="Name and Expense">
                <HeaderStyle BackColor="#97ABC1" HorizontalAlign="Center" />        
                            <ItemTemplate>
                                <%--<asp:Label ID="lnk_leave_list" runat="server" Text='<%# Eval("leave") %>'></asp:Label> --%>
                                <asp:LinkButton ID="lnkreimburse" runat="server" ForeColor="Black"  CommandName="cmd"  Text='<%#Eval("reimbursement")%>'></asp:LinkButton>
                            </ItemTemplate>
        

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
            </Columns>
        </asp:GridView>
                                            
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                             <asp:Label ID="Lbl_totatl_reimbursement" runat="server">
                                            </asp:Label>
                                            </td></tr>
                                           
                                </table>
                                    </asp:Panel>
                                </td>
                               </tr>
                                
                                

</table>
</asp:Panel>
                    
            </td>
        </tr>
        
        <tr>
            <td colspan="2">
            <table width = "100%" style=" border-style:none">
                        <tr>
                            <td width="75%">
                                <asp:RoundedCornersExtender ID="RoundedCornersExtender1" Corners="Left" Radius="5" TargetControlID="main_panel" runat="server">
                                </asp:RoundedCornersExtender>
</td>
</tr>
<tr>
<td>
<asp:RoundedCornersExtender ID="RoundedCornersExtender11" Corners="Left" Radius="5" TargetControlID="sub_panel" runat="server">
                                </asp:RoundedCornersExtender>
                                <asp:Panel ID="sub_panel" runat="server" Height="356px" Width="100%" BackColor="White">
                                <table align="center">
                                <tr><td>
                                <asp:RoundedCornersExtender ID="RoundedCornersExtender12" Corners="Left" Radius="5" TargetControlID="panel_employee_annouceents" runat="server">
                                </asp:RoundedCornersExtender>
                                 <asp:Panel ID="panel_employee_annouceents" runat="server" Height="172px" Width="300px" ScrollBars="Vertical">
                                <table  align="center" id="table1" style="height:100%;width:280px; font-family: Calibri; font-size: large; background-color:#5D7B9D;" 
                                        bgcolor="#5D7B9D" ID="table_announcements">   
                                        <tr align="center">
                                        <td style="border-top-style:hidden;" bgcolor="#5D7B9D">
                                            <asp:Label ID="Lbl_today_announcement" Text="Today's Announcements" runat="server"></asp:Label>
                                           
                                        </td>
                                        </tr>
                                        <tr valign="middle">
                                        <td bgcolor="#5D7B9D">
                                      <asp:GridView ID="Grid_annoucment1" DataKeyNames="announcementid" runat="server" AutoGenerateColumns="False" 
                                                CellPadding="8"  CssClass="MyGridView1"
             Width="100%" Height="70%"
                                                onrowcreated="Grid_annoucment1_RowCreated" 
                                                onrowcommand="Grid_annoucment1_RowCommand">
                                                
                                    
                                                
            <Columns>
            <asp:TemplateField Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblid1" Text='<%#Eval("announcementid")%>' runat="server"></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
               <asp:TemplateField HeaderText="Today Announcements">
                <HeaderStyle BackColor="#5D7B9D" HorizontalAlign="Center" />       
                            <ItemTemplate>
                           
                                <%--<asp:Label ID="lnk_leave_list" runat="server" Text='<%# Eval("leave") %>'></asp:Label> --%>
                                <asp:LinkButton ID="Lnk_announcements1" ForeColor="Black" runat="server" CommandArgument="<%# Container.DataItemIndex %> "  CommandName="cmd"  Text='<%#Eval("Announcements1")%>'></asp:LinkButton>
                            </ItemTemplate>
        

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
              
            </Columns>
        </asp:GridView>
         </td></tr><tr>
         <td>
             <asp:Label ID="lbl_employee_announcement" runat="server" Text="Label"></asp:Label>
         </td>
         </tr>
                                </table>
                                       </asp:Panel>
                                       </td></tr>
                                       </table>
                                       </asp:Panel>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td style="background-color: #483C48; color: #FFFFFF;" height="25px">
     
                                 © 2011 hesperusinfo.com - All rights reserved.      
     </td>
     </tr>
</table>

</form>
</body>
</html>

