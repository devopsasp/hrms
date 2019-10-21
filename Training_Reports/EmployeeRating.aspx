<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Master_Page/EmployeeMaster.master" CodeFile="EmployeeRating.aspx.cs" Inherits="Training_Reports_EmployeeRating" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript" language="javascript" src="../Scripts/jquery-1.3.2.min.js"></script>
<script type="text/javascript" language="javascript" src="../Scripts/jquery.uploadify.js"></script>
<link href="../Css/uploadify.css" rel="stylesheet" type="text/css" />
    <%--<script type="text/javascript" src="scripts/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="scripts/jquery.uploadify.js"></script>--%>
     <%--<script type="text/javascript" src="scripts/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="scripts/jquery.uploadify.js"></script>--%>
        <script type="text/javascript" language="javascript" src="../datecheck.js"></script>
      <script type ="text/javascript">
    function Change(obj, evt)
    {
        if(evt.type=="focus")
            obj.style.borderColor="#E6D78A";
        else if(evt.type=="blur")
            obj.style.borderColor="#989898";
    }
   </script> 
<style type="text/css" media="screen">    @import "basic.css";
    #contbl
    {
        height: 64px;
    }
    #reftbl
    {
        height: 206px;
    }
    #Text1
    {
        height: 19px;
        width: 125px;
    }
    #personal_tbl
    {
        width: 136%;
    }
    #Table1
    {
        width: 323%;
    }
    .style119
    {
        width: 391px;
    }
    
         .roundedbox {
    background:#fff;
    font-family:Calibri;
    font-size:9pt;
    
    margin-left:0px;
    margin-right:auto;
    margin-top:1px;
    margin-bottom:1px;
    padding:3px;
    border-top:1px solid #CCCCCC;
    border-left:1px solid #CCCCCC;
    border-right:1px solid #999999;
    border-bottom:1px solid #999999;
    -moz-border-radius:10px;
    -webkit-border-radius: 8px;
    border-top-left-radius:6px;
	border-top-right-radius:6px;
	border-bottom-left-radius:6px;
	border-bottom-right-radius:6px;
	
    
}

         </style>
<table cellpadding="5" cellspacing="1" width="100%" id="tbl_details" runat="server">
     <tr id="Tr9" runat="server">
                        <td align="left" 
                  style="font-family:Calibri;font-size: small;color: #808080;background-color:#5D7B9D" class="style123" 
                              colspan="2">
                           <span class="Title"><span 
                                
                                style="font-family: Calibri; color: #FFFFFF; font-size: medium; font-weight: bold;">&nbsp;&nbsp;<img src="../Images/rp_arrow.gif" />&nbsp;Trainer 
                            Rating</span></td>
                    </tr>
  
                    <tr id="Tr1" runat="server">
                        <td colspan="2" align="center">
                            &nbsp;<asp:Label ID="lbl_Error" CssClass="Error" runat="server" ForeColor="Red" Font-Bold="True" Width="40%"></asp:Label></td>
                    </tr>
                     <tr id="Tr2" runat="server">
                        <td align="right" 
                  style="font-family:Calibri;font-size: small;color: #808080;" class="style119">
                            Select Program Name</td>
                        <td align="left" style="font-family:Calibri;font-size: small;color: #808080;" class="style122">
                            <asp:DropDownList ID="ddl_prgmtype" runat="server"   onfocus ="Change(this, event)" onblur ="Change(this, event)" 
        CssClass="roundedbox" 
                                style="height: 25px; font-family:Calibri;font-size: small;color: #808080;" Width="200px" 
                     
                      AutoPostBack="True" onselectedindexchanged="ddl_Inst_SelectedIndexChanged">
                  
                  
                  </asp:DropDownList></td>
                    </tr>
                      <tr  id="Tr_type" runat="server" visible="false">
                        <td align="right" 
                  style="font-family:Calibri;font-size: small;color: #808080;" class="style119">
                            Select Employee&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                        <td align="left" style="font-family:Calibri;font-size: small;color: #808080;" class="style122">
                           <asp:DropDownList ID="ddl_employee" runat="server"   
                                onfocus ="Change(this, event)" onblur ="Change(this, event)" 
        CssClass="roundedbox"  style="height: 25px; font-family:Calibri;font-size: small;color: #808080;" 
                                Width="200px" 
                    >
                 
                  </asp:DropDownList></td>
                    </tr>
                    
                      <tr  id="Tr8" runat="server">
                        <td align="center" 
                  style="font-family:Calibri;font-size: small;color: #808080;" class="style123" 
                              colspan="2">
                            <asp:ImageButton ID="btn_emp_report" runat="server" 
                                ImageUrl="../Images/Show_Report.jpg"  style="height: 20px" onclick="btn_emp_report_Click" 
                                             /></td>
                    </tr>
                 
                    </table>
</asp:Content>