<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="projectsite.aspx.cs" Inherits="Hrms_Master_Default" Title="Welcome to HRMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<script type="text/javascript" language="javascript" src="../../Scripts/Datavalid.js"></script>
<script type="text/javascript">
    window.onload = function () {
        var seconds = 5;
        setTimeout(function () {
            document.getElementById("<%=lbl_Error.ClientID %>").innerHTML = "";
        }, seconds * 1000);
    };
</script>
    <script language="javascript" type="text/javascript">
    
    function validate()
  {

    var r=confirm("Are you sure you want to delete this record?");
    if (r==true)
    {
    return true;
    }
    else
    {
    return false;
    }
  }
    
    function show_message()
    {
        alert("Projectsite Name Already Exist");
    }
    
     function show_Error()
    {
        alert("Enter Projectsite Name");
    }
    
    function fnSave()
    {   
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$projectsiteName.value == "")
        {
            alert("Enter Projectsite Name");
            aspnetForm.ctl00$ContentPlaceHolder1$projectsiteName.focus();
            return false;
        }                        
        else
        { 
              return true;  
        }
    }    
    </script>

    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td id="tdComposeHeader" valign="top" align="center">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="background-color:#5D7B9D" height="35px" class="border">
                            <span class="Title">&nbsp;&nbsp;<span 
                                style="font-family: Calibri; color:White; font-weight: bold;">Project Site Master</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </span></td>
                                <td style="background-color:#5D7B9D" width="200px" align="center">
                                    <asp:DropDownList ID="ddl_branch" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="ddl_branch_SelectedIndexChanged" Width="115px">
                                    </asp:DropDownList>
                                                        </td>
                    </tr>
                </table>
                <table id="pro_add" runat="server" width="100%">
                    <tr>
                        <td class="dComposeItemLabel" nowrap 
                            style="font-family: Calibri; font-size: x-small; color: #6A6A6A">
                            &nbsp;</td>
                        <td align="center" valign="baseline" colspan="5">
                            <span class="Title">
                            <asp:Label ID="lbl_Error" runat="server" ForeColor="Red" Font-Bold="True" 
                                Font-Names="Calibri" Font-Size="Small"></asp:Label></span></td>
                        <td style="font-family: Calibri; font-size: x-small; color: #6A6A6A">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="dComposeItemLabel" nowrap 
                            style="font-family: Calibri; font-size: x-small; color: #6A6A6A">
                            &nbsp;</td>
                        <td align="center" valign="baseline" colspan="5">
                            &nbsp;</td>
                        <td style="font-family: Calibri; font-size: x-small; color: #6A6A6A">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="dComposeItemLabel" nowrap 
                            style="font-family: Calibri; font-size: small; color: #6A6A6A">
                            Project Location</td>
                        <td align="left" valign="baseline" style="width: 124px">
                            <asp:DropDownList class="InputDefaultStyle" runat="server" id="ddlprojectsite"
                                style="font-family: Calibri; font-size: Small; color: #6A6A6A" 
                                maxlength="20" >
                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Onsite" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Offsite" Value="2"></asp:ListItem>
                                </asp:DropDownList><span 
                                style="font-family: Calibri; font-size: Small; color: #6A6A6A"> </span> </td>
                        <td align="left" valign="baseline" class="dComposeItemLabel" 
                            style="font-family: Calibri; font-size: small; color: #6A6A6A">
                            Site Name</td>
                        <td align="left" valign="baseline" style="width: 127px">
                            <input id="txt_siteName" class="InputDefaultStyle" runat="server" 
                                style="font-family: Calibri; font-size: small; color: #6A6A6A" 
                                maxlength="20" /></td>
                        <td align="left" valign="baseline" class="dComposeItemLabel" 
                            style="font-family: Calibri; font-size: small; color: #6A6A6A">
                            Address</td>
                        <td align="left" style="width: 134px">
                            <input id="txt_Address" class="InputDefaultStyle" runat="server" 
                                style="font-family: Calibri; font-size: small; color: #6A6A6A" 
                                maxlength="20" /></td>
                        <td style="font-family: Calibri; font-size: x-small; color: #6A6A6A">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="dComposeItemLabel" nowrap 
                            style="font-family: Calibri; font-size: small; color: #6A6A6A">
                            Project Name</td>
                        <td align="left" valign="baseline" style="width: 124px">
                            <input class="InputDefaultStyle" runat="server" id="txt_ProjectName" 
                                onkeypress="AllowOnlyText();" 
                                style="font-family: Calibri; font-size: small; color: #6A6A6A" 
                                maxlength="20" /></td>
                        <td align="left" valign="baseline" class="dComposeItemLabel" 
                            style="font-family: Calibri; font-size: small; color: #6A6A6A">
                            Project Head</td>
                        <td align="left" valign="baseline" style="width: 127px">
                            <asp:DropDownList class="InputDefaultStyle" runat="server" id="ddl_projectHead"
                                style="font-family: Calibri; font-size: small; color: #6A6A6A" 
                                maxlength="20" ></asp:DropDownList></td>
                        <td align="left" valign="baseline" class="dComposeItemLabel" 
                            style="font-family: Calibri; font-size: small; color: #6A6A6A">
                            Delivery Head</td>
                        <td align="left" style="width: 134px">
                            <asp:DropDownList class="InputDefaultStyle" runat="server" id="ddl_DeliveryHead" 
                                style="font-family: Calibri; font-size: small; color: #6A6A6A" 
                                maxlength="20" ></asp:DropDownList></td>
                        <td style="font-family: Calibri; font-size: x-small; color: #6A6A6A">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="dComposeItemLabel" nowrap 
                            style="font-family: Calibri; font-size: small; color: #6A6A6A">
                            Delivery Schedule</td>
                        <td align="left" valign="baseline" style="width: 124px">
                            <input class="InputDefaultStyle" runat="server" id="txt_delivery" 
                                onkeypress="AllowOnlyText();" 
                                style="font-family: Calibri; font-size: small; color: #6A6A6A" 
                                maxlength="20" /></td>
                        <td align="left" valign="baseline" class="dComposeItemLabel" 
                            style="font-family: Calibri; font-size: small; color: #6A6A6A">
                            Technologies Used</td>
                        <td align="left" valign="baseline" style="width: 127px">
                            <asp:TextBox class="InputDefaultStyle" runat="server" id="txt_technology" 
                                style="font-family: Calibri; font-size: small; color: #6A6A6A" 
                                maxlength="20" CssClass="InputDefaultStyle" Height="62px" 
                                TextMode="MultiLine" Width="170px" ></asp:TextBox></td>
                        <td align="left" valign="baseline" class="dComposeItemLabel" 
                            style="font-family: Calibri; font-size: small; color: #6A6A6A">
                            Other Information</td>
                        <td align="left" style="width: 134px">
                            <asp:TextBox class="InputDefaultStyle" runat="server" id="txt_otherinfo" 
                                style="font-family: Calibri; font-size: small; color: #6A6A6A" 
                                maxlength="20" CssClass="InputDefaultStyle" Height="62px" 
                                TextMode="MultiLine" Width="170px"></asp:TextBox></td>
                        <td style="font-family: Calibri; font-size: x-small; color: #6A6A6A">
                            <asp:ImageButton ImageUrl="~/Images/Add.png" 
                                onmouseover="this.src='../../Images/Addover.png';" 
                                onmouseout="this.src='../../Images/Add.png';" ID="Button1" runat="server" OnClientClick="return fnSave();"
                                Text="Add" OnClick="Button1_Click1" ImageAlign="AbsMiddle" Width="124px" /></td>
                    </tr>
                    <tr>
                        <td class="dComposeItemLabel" nowrap 
                            style="font-family: Calibri; font-size: small; color: #6A6A6A">
                            &nbsp;</td>
                        <td align="left" valign="baseline" style="width: 124px">
                            &nbsp;</td>
                        <td align="left" valign="baseline" class="dComposeItemLabel" 
                            style="font-family: Calibri; font-size: small; color: #6A6A6A">
                            &nbsp;</td>
                        <td align="left" valign="baseline" style="width: 127px">
                            &nbsp;</td>
                        <td align="left" valign="baseline" class="dComposeItemLabel" 
                            style="font-family: Calibri; font-size: small; color: #6A6A6A">
                            &nbsp;</td>
                        <td align="left" style="width: 134px">
                            &nbsp;</td>
                        <td style="font-family: Calibri; font-size: x-small; color: #6A6A6A">
                            &nbsp;</td>
                    </tr>
                </table>
                <table id="pro_branch" runat="server" width="100%">
                    
                    <tr valign="top">
                        <td align="center" style="width: 50%">
                            <div class="gridheight_master">
                                <asp:GridView ID="grid_projectsite" runat="server" AutoGenerateColumns="False" Width="100%"
                                    DataKeyNames="projectsiteId" OnRowDeleting="Delete" OnRowEditing="Edit" OnRowUpdating="Update" 
                                    onrowcommand="grid_projectsite_RowCommand" CellPadding="4" 
                                    ForeColor="#333333" GridLines="None">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                        <asp:TemplateField>
                                        <HeaderTemplate>
                                        <TABLE cellSpacing="0" cellPadding="0" width="100%">
                                            <COLGROUP>        
                                                <COL>                                
                                            </COLGROUP>
                                            <THEAD>
                                                <TR>
                                                    <TH align="left" style="width: 20%; font-family: Calibri; color: #FFFFFF; font-weight: bold;">Location</TH>
                                                    <TH align="left" style="width: 20%; font-family: Calibri; color: #FFFFFF; font-weight: bold;">Site Name</TH>
                                                    <TH align="left" style="width: 20%; font-family: Calibri; color: #FFFFFF; font-weight: bold;">Project Name</TH>
                                                    <TD align="right" style="width: 10%; font-family: Calibri; color: #FFFFFF; font-weight: bold;"><asp:Label ID="lbledit" Text="Edit" runat="server"></asp:Label></TD>
                                                    <TD align="right" style="width: 10%; font-family: Calibri; color: #FFFFFF; font-weight: bold;"><asp:Label ID="lbldel" Text="Delete" runat="server"></asp:Label></TD>
                                                </tr>
                                            </THEAD>
                                       </table>
                                   </HeaderTemplate>
                                            <ItemTemplate>
                                                <table class="dItemListContentTable" cellspacing="0" cellpadding="0" width="100%">
                                                    <colgroup>
                                                        <col class="dInboxContentTableCheckBoxCol">
                                                    </colgroup>
                                                    <tbody>
                                                        <tr>
                                                            <td id="chkid" runat="server" style="width: 10%;" align="left">
                                                                <input type="checkbox" id="Chk_projectsite" runat="server" /></td>
                                                            <td style="width: 40%;" nowrap>
                                                            <input runat="server" id="txtgrid" onkeypress="AllowOnlyTNS();" value ='<%#Eval("projectsiteName")%>' style="font-family: Calibri; font-weight: bold; color: #6A6A6A;" /></td>
                                                           <td style="width:20%"><asp:Label runat="server" id="Text1" onkeypress="AllowOnlyTNS();" Text ='<%#Eval("Location")%>' style="font-family: Calibri; font-weight: bold; color: #6A6A6A;" ></asp:Label></td>
                                                            <td style="width:20%"><asp:Label runat="server" id="Text2" onkeypress="AllowOnlyTNS();" Text ='<%#Eval("projectName")%>' style="font-family: Calibri; font-weight: bold; color: #6A6A6A;" ></asp:Label></td>
                                                                
                                                            
                                                            <td align="center" style="width: 10%;" nowrap>
                                                                <asp:ImageButton ID="img_update" ImageUrl="../../Images/i_Edit.gif" runat="server" Style="border: 0"
                                                                    AlternateText="" CommandName="Update" />
                                                                <asp:ImageButton ID="img_save" ImageUrl="../../Images/save.gif" runat="server" Style="border: 0"
                                                                    AlternateText="" CommandName="Edit" Visible="false" /></td>
                                                                    <td style="width: 10%; font-family: Calibri; color: #c18685;" nowrap 
                                                                align="center">
                                                                <%--<asp:Label ID="lbl" runat="server" Text="Delete"></asp:Label>--%><asp:ImageButton ImageUrl="../../Images/delete_icon.gif" ID="imgdel" runat="server" CommandName="Delete" OnClientClick="return validate()"/>
                                                             </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                </asp:GridView>
                            </div>
                        </td>
                         
                    </tr>
                </table>
                </td>
        </tr>
        <tr valign="top">
            <td align="center" valign="top">
                &nbsp;<asp:Label ID="Error" runat="server" Width="50%" Font-Bold="True" 
                    Font-Names="Calibri" Font-Size="X-Small"></asp:Label></td>
        </tr>
        <tr>
            <td align="center">
                <input type="hidden" id="ToolBarCode" name="ToolBarCode" runat="server" value="0" />&nbsp;
                <input id="hprojectsiteID" runat="server" type="hidden" value="0" /></td>
        </tr>
    </table>
</asp:Content>

