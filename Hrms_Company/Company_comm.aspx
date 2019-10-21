<%@ Page MaintainScrollPositionOnPostback="true" Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Company_comm.aspx.cs" Inherits="Hrms_Company_Default" Title="Welcome to HRMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <link href="../_assets/css/meiii.css" rel="stylesheet" type="text/css" />
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
 <style type="text/css">
.menuItem
{
background-image : url(../Images/blackbg.png);
background-repeat:repeat-x;
cursor : hand;
}
.menuItem1
{
cursor : hand;
}

        .style1
        {
            width: 13px;
            height: 146px;
        }
        .style2
        {
            width: 991px;
        }
        .style3
        {
            width: 143px;
        }
        .style4
        {
            width: 493px;
        }

        #author
        {
            width: 199px;
        }

        #text
        {
            height: 93px;
            width: 180px;
        }

        .style5
        {
            font-family: "Times New Roman", Times, serif;
        }
        .style6
        {
            color: #000000;
        }

        .style7
        {
            font-family: Calibri;
        }
        .style9
        {
            font-family: Calibri;
            font-weight: bold;
            color: #000000;
        }

    </style>
<meta name="keywords" content="the wall, free website templates, css templates, CSS, HTML" />
<meta name="description" content="The Wall is a free website template provided by templatemo.com" />
<meta name="keywords" content="Wooden Template, Outbox Website, Personal, Free CSS Template, XHTML" />
<meta name="description" content="Wooden Template, Outbox Website, Free CSS Template from TemplateMo.com" />
<link href="templatemo_style.css" rel="stylesheet" type="text/css" />

    <meta http-equiv="Content-Language" content="en-us" />
	<meta name="author" content="Niall Doherty" />
    <script src="js/jquery-1.2.1.pack.js" type="text/javascript"></script>
    <script src="js/jquery-easing.1.2.pack.js" type="text/javascript"></script>
    <script src="js/jquery-easing-compatibility.1.2.pack.js" type="text/javascript"></script>
    <script src="js/coda-slider.1.1.1.pack.js" type="text/javascript"></script>
    <!-- 
    The CSS. You can of course have this in an external .css file if you like.
    Please note that not all these styles may be necessary for your use of Coda-Slider, so feel free to take out what you don't need.
    -->
    <!-- Initialize each slider on the page. Each slider must have a unique id -->
    <script type="text/javascript">
    jQuery(window).bind("load", function() {
    jQuery("div#slider1").codaSlider()
    // jQuery("div#slider2").codaSlider()
    // etc, etc. Beware of cross-linking difficulties if using multiple sliders on one page.
    });

    </script>

</head>
<body class="body">
<form id="form1" runat="server">



   <table width="100%" height="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td id="td1" valign="top">
               <table width="100%" cellpadding="0" cellspacing="0">
               <tr valign="top">
                <td width="101%" background="../Images/Header_995_new.jpg" class="style1" 
                       style="border: medium ridge #C0C0C0; color: #000080; display: block;" 
                       bgcolor="White">
                    &nbsp;</td>
                <td class="style1">
                </td>
            </tr>
            <tr>
                    <td height="35px" class="border">
                    <asp:Menu CssClass="menuItem" Orientation="Horizontal" StaticSubMenuIndent="0" StaticDisplayLevels="2"
                        MaximumDynamicDisplayLevels="2" ID="Menu1" runat="server" DataSourceID="SiteMapDataSource1"
                        Font-Names="Calibri" Font-Size="Small" ForeColor="white" DynamicPopOutImageUrl="~/Images/arr_collapsed.gif"
                        StaticPopOutImageUrl="~/Images/arr_expanded.gif" Width="100%" 
                        Height="37px">
                        <DynamicHoverStyle CssClass="menuItem1" Font-Underline="true" Font-Bold="true" />
                        <DynamicMenuItemStyle BackColor="#DCDCDC" ForeColor="Firebrick" HorizontalPadding="5px"
                            VerticalPadding="2px" Font-Bold="true" />
                        <StaticHoverStyle CssClass="menuItem1" Font-Bold="true"/>
                        <StaticMenuItemStyle HorizontalPadding="2px" VerticalPadding="2px" Font-Bold="true" />
                    </asp:Menu>
                    <asp:SiteMapDataSource ID="SiteMapDataSource1" SiteMapProvider="Menu1Sitemap"  runat="server" />
                    <asp:SiteMapDataSource ID="SiteMapDataSource2" SiteMapProvider="Menu2Sitemap" runat="server" />
                        <asp:DropDownList ID="DropDownList3" runat="server" Visible="False">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td height="35px" class="border"><SPAN class="Title">&nbsp;&nbsp; 
                        <span class="style7">Human Resource Management 
                        System ---&gt; Company</span>&nbsp;&nbsp; </SPAN>
                            </td>
                </tr>
             </table>
            
    
                    <table cellpadding="5px" cellspacing="1px" 
                    style="width: 99%; height: 52px;">
                        <tr><td>
                            <!-- end of templatemo_content_wrapper -->

                            <table class="style2" bgcolor="#dbcdcc">
                                <tr>
                                    <td align="center" class="style3" rowspan="3"><asp:Image ID="img_photo" 
                                            runat="server" Height="106px" Width="115px" />
                                        <br />
                                        <br />
                                        <asp:Label ID="lblmsg" runat="server" Font-Bold="True" Font-Size="X-Small" 
                                            ForeColor="Black" Font-Names="Calibri"></asp:Label><br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />.
                                        <br />
                                        <br />
                                        <br />
                                        </td>
                                    <%--<td class="style4">
                                        &nbsp;</td>--%>
                                </tr>
                                <tr>
                                   <%-- <td class="style4">
                                        &nbsp;</td>--%>
                                </tr>
                                 <%--Reimbursement--%>
                                
                                      <asp:Panel ID="reimbusement_container_pnl" runat="server">                                                                      
                                            <asp:Panel ID="pnl_reimbursement" runat="server" BackColor="Black" Visible="false" Width="100%" Height="50%">
                                                
                                                <asp:Literal ID="ltrlhdr_reimbursement" runat="server"></asp:Literal>
                                                                                               
                                            </asp:Panel>
                                        </asp:Panel>
                                
                                <%-- End of Reimbursement--%>
                                <tr>
                                    <td class="style4">
                                    
                                        <!-- end of templatemo_site_title_bar_wrapper -->
                                        <asp:Button ID="Button4" runat="server" BackColor="#dbcdcc" 
                                            BorderStyle="None"/>
                                            <asp:Button ID="Button7" runat="server" BackColor="#dbcdcc" 
                                            BorderStyle="None" />
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="btnCancel" OkControlID="btnOkay"  TargetControlID="Button4"
            PopupControlID="Panel1" Drag="true" PopupDragHandleControlID="PopupHeader">
                                        </asp:ModalPopupExtender>
    <div id="Panel1" style="border:0px solid black; border-top:0px; background-color: #FFCC66;">
            <div>
                <div style="background-color:#FF0000; height: 22px; width: 495px; color: #FFFFFF;"  id="PopupHeader" 
                    class="style9">
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    Reminder
                </div>
                <div>
                    <p style=" background-color:#cccccc; font-family: 'Book Antiqua'; font-weight: bold; color: #000000; width: 495px; height: 49px;" 
                        align="center">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                        <span class="style7">You have remainder request waiting </br>  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  for the leave approval..</span>
                    </p>
                </div>
                <div align="center" style="background-color:#cccccc; width: 495px">
                    <input id="btnCancel" value=" Ok " type="button" />
                    <input id="btnOkay" value="Remind Later" type="button" />
                </div>
            </div>
        </div>
        <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" CancelControlID="Button6" OkControlID="Button5"  TargetControlID="Button7"
            PopupControlID="Panel2" Drag="true" PopupDragHandleControlID="Div2">
                                        </asp:ModalPopupExtender>
        <div id="Panel2" style="border:0px solid black; border-top:0px; background-color: #FFCC66;">
            <div>
                <div style="background-color:#FF0000; height: 22px; width: 495px; color: #FFFFFF;"  id="Div2" 
                    class="style9">
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    Reminder
                </div>
                <div>
                    <p style=" background-color:#cccccc; font-family: 'Book Antiqua'; font-weight: bold; color: #000000; width: 495px; height: 49px;" 
                        align="center">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                        <span class="style7">A task has been assigned for you </br>  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Please check the 'My Task' menu..</span>
                    </p>
                </div>
                <div align="center" style="background-color:#cccccc; width: 495px">
                    <input id="Button5" value=" Ok " type="button" />
                    <input id="Button6" value="Remind Later" type="button" />
                </div>
            </div>
        </div>
       
 <div id="templatemo_content_wrapper">

  <div id="templatemo_content">
    
    <!-- start of slider -->

<div class="slider-wrap">
	<div id="slider1" class="csw">
		<div class="panelContainer">
		<div class="panel" title="Create New">
				<div class="wrapper">
					
                    <h2>Create New Message</h2>

                    <div id="contact_form">
                    
                        <form method="post" name="contact" action="">

                        To (Dept)&nbsp;&nbsp; :&nbsp; &nbsp;<asp:DropDownList ID="ddl_dept" 
                            runat="server" Width="180px" AutoPostBack="true" onselectedindexchanged="ddl_dept_SelectedIndexChanged1" 
                            >
                            <asp:ListItem>Select</asp:ListItem>
                        </asp:DropDownList>
                        <div class="cleaner_h10"></div>
                        
                                                To (Name) :&nbsp;&nbsp;                         <asp:DropDownList ID="ddl_ename" runat="server" Width="180px" on>
                        </asp:DropDownList>
                        &nbsp;<div class="cleaner_h10"></div>
                        
                        <label for="text">Message&nbsp;&nbsp; :</label>&nbsp;&nbsp; <textarea id="txt_msg" runat="server" name="text" 
                            class="required"></textarea>
                        <div class="cleaner_h10"></div>
                        
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btn_submit" runat="server" Text="Submit" Font-Bold="True" 
                            onclick="btn_submit_Click" />
                        
                        <input style="font-weight: bold;" type="reset" class="submit_btn" name="reset" id="reset" value=" Reset " />
                        
                        </form>
                    </div>

                    <div class="cleaner_h20"></div>
                  </div>
				
			</div>		
			<div class="panel" title="Inbox">
				<div class="wrapper">
				<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                    </asp:ToolkitScriptManager>
                
<%--<asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="100">
</asp:Timer>--%>
<asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
<Triggers>
<%--<asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />--%>
</Triggers>
<ContentTemplate>
    <h2>Inbox </h2>
        
<asp:GridView ID="GridView1" 
                            Font-Size="Smaller" runat="server"  
        AutoGenerateColumns="False" Height="16px" Width="700px" 
            BackColor="White" BorderColor="White" BorderStyle="Ridge" 
        BorderWidth="2px" CellPadding="3" GridLines="None" 
            HorizontalAlign="Left" PageSize="5" CellSpacing="1" 
        onrowcommand="GridView1_RowCommand" onrowdeleting="GridView1_RowDeleting" >
                                                
            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
            <RowStyle BackColor="#CCCCCC" ForeColor="Black" Font-Names="Calibri" />
            <Columns>
            
            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50" 
                    HeaderText="ID">
                <ItemTemplate>                       
                       <asp:Label ID="lblid" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
            </asp:TemplateField>
            
            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Sender">
                   <ItemTemplate>
                       <asp:Label ID="Label1" runat="server" Text='<%# Eval("Sender") %>'></asp:Label>                        
                   </ItemTemplate>
                   <ItemStyle HorizontalAlign="Left"></ItemStyle>
             </asp:TemplateField>
                
             <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Message">
                <ItemTemplate>
                       <asp:Label ID="Label2" runat="server" Text='<%# Eval("Message") %>'></asp:Label>
                 </ItemTemplate>
                    
                 <ItemStyle HorizontalAlign="Left"></ItemStyle>
              </asp:TemplateField>
              
              <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Date">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
               </asp:TemplateField>
            
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="Reply_msg" runat="server" CommandName="Delete" ForeColor="#0000CC">Reply</asp:LinkButton>
                        
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowDeleteButton="true" />
                
            </Columns>
            <%--<PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />--%>
            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#D58B8B" Font-Bold="True" ForeColor="#993333" 
                            Font-Names="Calibri" />
            <EmptyDataTemplate>
            <asp:Label ID="lblempty" Text="No Records" runat="server">
            </asp:Label> 
            
            </EmptyDataTemplate>
            
        </asp:GridView>

				
</ContentTemplate>

</asp:UpdatePanel> 

                   
                  
                    
<br />
                    <br />
                    <br /><br /><br /><br /><br />
                    <%--<PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />--%>
</div>

</div>

<div class="panel" title="Outbox">
	<div class="wrapper">
	<h2>&nbsp;&nbsp;&nbsp;Outboxh2>
	
	<asp:GridView ID="GridView2" Font-Size="Smaller" runat="server"  
        AutoGenerateColumns="False" Height="16px" Width="700px" 
            BackColor="White" BorderColor="White" BorderStyle="Ridge" 
        BorderWidth="2px" CellPadding="3" GridLines="None" 
            HorizontalAlign="Left" PageSize="5" CellSpacing="1" 
        onrowcommand="GridView1_RowCommand" onrowdeleting="GridView1_RowDeleting" >
                                                
            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
            <RowStyle BackColor="#CCCCCC" ForeColor="Black" Font-Names="Calibri" />
            <Columns>
            
            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50" 
                    HeaderText="ID">
                <ItemTemplate>                       
                       <asp:Label ID="lblid" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
            </asp:TemplateField>
            
            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Receiver">
                   <ItemTemplate>
                       <asp:Label ID="Label1" runat="server" Text='<%# Eval("Receiver") %>'></asp:Label>                        
                   </ItemTemplate>
                   <ItemStyle HorizontalAlign="Left"></ItemStyle>
             </asp:TemplateField>
                
             <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Message">
                <ItemTemplate>
                       <asp:Label ID="Label2" runat="server" Text='<%# Eval("Message") %>'></asp:Label>
                 </ItemTemplate>
                    
                 <ItemStyle HorizontalAlign="Left"></ItemStyle>
              </asp:TemplateField>
              
              <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Date">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
               </asp:TemplateField>
                                         
                
            </Columns>
            <%--<PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />--%>
            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#D58B8B" Font-Bold="True" ForeColor="#993333" 
                            Font-Names="Calibri" />
            <EmptyDataTemplate>
            <asp:Label ID="lblempty" Text="No Records" runat="server">
            </asp:Label> 
            
            </EmptyDataTemplate>
            
        </asp:GridView>
	                
       
    </div>
</div>


<div class="panel" title="Deleted Items">
	<div class="wrapper">
		
        <h2>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Deleted Items</h2>
        
      <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No items found..</p>  
	</div>
</div>
               
			
		</div><!-- .panelContainer -->
	</div><!-- #slider1 -->
</div><!-- .slider-wrap -->

<p id="cross-links" style="width:0px; height: 0px; font-size:0; overflow: hidden;">
	Same-page cross-link controls:<br />
	<a href="#1" class="cross-link">Page 1</a> | <a href="#2" class="cross-link">
    Page 2</a> | <a href="#3" class="cross-link">Page 3</a> | <a href="#4" class="cross-link">
    Page 4</a> | <a href="#5" class="cross-link">Page 5</a>
</p>

    <!-- end of slider -->
	</div> 
	<!-- end of templatemo_content -->
</div>
                                    </td>
                                </tr>
                            </table>
                        
                        
                         </td>
</tr> </table>
</td>


</tr>
<tr>
<td align="right" width="100%" background="../Images/bg.jpg" height="15px" 
        class="style5">
                    <span class="style6">Powered by</span> <a href="http://www.epayindia.com" target="_blank"><font color="#0099ff"
                        size="4px" title="Click to Know more about ePay Solutions">ePay</font></a></td>
</tr>

</tr>


</table>
</form>
</body>
</html>

