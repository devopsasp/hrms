<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Bonus.aspx.cs" Inherits="Hrms_Master_Default" Title="Welcome to HRMS" %>
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
    
    function show_message()
    {
        alert("Bonus Name Was Already Exist");
    }
        function show_Error()
        {
        alert("Enter Bonus Name");
        }
        
        function show_Error1()
        {
        alert("Enter Total Point");
        }
   
    function fnSave()
    {   
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_appraisalname.value == "")
        {
            alert("Enter Bonus Name");
            aspnetForm.ctl00$ContentPlaceHolder1$txt_appraisalname.focus();
            return false;
        }                        
        else
        { 
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$Txt_points.value == "")
        {
            alert("Enter Bonus Point");
            aspnetForm.ctl00$ContentPlaceHolder1$Txt_points.focus();
            return false;
        }                        
        else
        {
              return true;  
              }
        }
    } 
       
function Txt_points_onclick() {

}


function Txt_annual_incr_onclick() {

}

    </script>

    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td id="tdComposeHeader" valign="top" align="center">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="35px" class="border" style="width: 644px">
                            <span class="Title">&nbsp;&nbsp;<span class="style2">Bonus</span></span></td>
                            <td bgcolor="#EDEDEE">
                                <asp:DropDownList ID="ddl_branch" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddl_branch_SelectedIndexChanged" Width="115px">
                                </asp:DropDownList>
                                                        </td>
                    </tr>
                </table>
                <table id="tb_bonus" runat="server" width="100%">
                    <tr>
                        <td colspan="5" align="center">
                            &nbsp;<asp:Label ID="lbl_Error" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="dComposeItemLabel" style="width: 411px; height: 34px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Bonus Name</td>
                            
                          
                        <td align="left" valign="baseline" style="width: 118px; height: 34px;">
                            <input class="InputDefaultStyle" runat="server" id="txt_appraisalname" 
                                onkeydown="AllowOnlyText1(event);" maxlength="20" />
                       </td>
                            
                          
                        <td align="left" valign="baseline" style="width: 153px; height: 34px;">
                            &nbsp;</td>
                            
                          
                        <td align="left" valign="baseline" style="width: 174px; height: 34px;">
                            &nbsp;</td>
                    </tr>
                    <tr id="points" runat="server">
                        <td class="dComposeItemLabel" nowrap="nowrap" 
                            style="height: 35px; width: 411px;">
                            points&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                        <td align="left" valign="baseline" style="height: 35px; width: 118px;">
                            <input class="InputDefaultStyle" runat="server" id="Txt_points" 
                                onkeydown="AllowOnlyNumeric1(event);" onclick="return Txt_points_onclick()" 
                                maxlength="20" /></td>
                        <td align="left" valign="baseline" style="height: 35px; width: 153px;">
                            &nbsp;</td>
                        <td align="left" valign="baseline" style="height: 35px; width: 174px;">
                            <asp:Button ID="Button1" runat="server" OnClientClick="return fnSave();" Text="Add"
                                Width="75px" OnClick="Button1_Click1" />
                        </td>
                        <td class="dComposeItemLabel" nowrap="nowrap" 
                            style="height: 35px; ">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            &nbsp;&nbsp;<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Add.png" onmouseover="this.src='../../Images/Addover.png';" onmouseout="this.src='../../Images/Add.png';"
                                onclick="ImageButton1_Click" style="height: 30px" />
&nbsp;<table width="100%">
                                <tr valign="top">
                                    <td align="center" width="70%" valign="top">
                                        <asp:GridView ID="grid_appraisal" runat="server" AutoGenerateColumns="False" Width="44%"
                                            DataKeyNames="BonusID" OnRowUpdating="Update" OnRowEditing="Edit" 
                                            OnRowDeleting="delete"  
                                            onselectedindexchanged="grid_appraisal_SelectedIndexChanged" 
                                            onrowcommand="grid_appraisal_RowCommand" CellPadding="4" 
                                            ForeColor="#333333" GridLines="None">
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                            <colgroup>
                                                                <col>
                                                            </colgroup>
                                                            <thead>
                                                                <tr>
                                                                    <td style="width: 10%;">
                                                                        &nbsp;</td>
                                                                    <td align="left" style="width: 80%; font-family: Calibri; color: #FFFFFF; font-weight: bold;">Bonus List</td>
                                                                    <td  style="width: 10%; font-family: Calibri; color: #FFFFFF; font-weight: bold; "><asp:Label ID="lbledit" Text="Edit" runat="server"></asp:Label></td>
                                                    <td align="right" style="width: 10%; font-family: Calibri; color: #FFFFFF; font-weight: bold; "><asp:Label ID="lbldel" Text="Delete" runat="server"></asp:Label></td>
                                                                </tr>
                                                            </thead>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                            <colgroup>
                                                                <col class="dInboxContentTableCheckBoxCol">
                                                            </colgroup>
                                                            <tbody>
                                                                <tr>
                                                                    <td style="width: 10%;" align="left">
                                                                        <input type="checkbox" id="Chk_Grade"  runat="server" /></td>
                                                                    <td style="width: 30%;" nowrap>
                                                                    <input runat="server" id="txtgrid" onkeydown="AllowOnlyText1(event);" value='<%#Eval("BonusName")%>' disabled="disabled" style="width: 100px" maxlength="20" />
                                                                        
                                                                    </td>
                                                                    <td style="width: 30%;" nowrap>
                                                                    <input runat="server" id="Txtgpoint" onkeydown="AllowOnlyNumeric1(event);" value='<%#Eval("totalpoint")%>' disabled="disabled" style="width: 100px" />
                                                                        <%--<asp:TextBox runat="server" Text='<%#Eval("totalpoint")%>' ID="Txtgpoint" Enabled="false"></asp:TextBox>--%>
                                                                    </td>
                                                                    <td align="center" style="width: 10%;" nowrap>
                                                                        <asp:ImageButton ID="img_update" ImageUrl="../../Images/i_Edit.gif" runat="server" Style="border: 0" AlternateText="" CommandName="Update" />
                                                                        <asp:ImageButton ID="img_save" ImageUrl="../../Images/save.gif" runat="server" Style="border: 0" AlternateText="" CommandName="Edit" Visible="false" />
                                                                    </td>
                                                                    <td align="center" style="width: 15%;" nowrap><asp:ImageButton ID="imgdel" ImageUrl="../../Images/i_delete.gif" runat="server" CommandName="Delete" /></td>
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
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table id="annual_tbl" runat="server" style="background-color:#dbcdcc">
                    <tr>
                        <td class="dComposeItemLabel" nowrap 
                            
                            style="width: 161px; font-family: Calibri; font-size: x-small; color: #6A6A6A;"> 
                            Annual Increment(%)&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; </td>
                        <td><input class="InputDefaultStyle" runat="server" id="Txt_annual_incr" 
                                onkeydown="AllowOnlyNumeric1(event);" onclick="return Txt_annual_incr_onclick()" 
                                 maxlength="5" /></td>
                        <td><asp:Button ID="butadd_inc" runat="server" Text="Add" 
                                onclick="butadd_inc_Click" BackColor="#CC0000" Font-Bold="True" 
                                Font-Names="Calibri" ForeColor="White" /></td>
                     </tr>
                     <tr>
                          <td class="dComposeItemLabel" nowrap style="width: 161px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              <asp:Label ID="lbldate" runat="server" Text="Date" ForeColor="#060606" 
                                  style="color: #6A6A6A; font-family: Calibri; font-size: x-small" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                           <td><input class="InputDefaultStyle" runat="server" id="txt_inc_date" 
                                   onkeydown="AllowOnlyNumeric1(event);"  visible="false" name="20"/></td>
                    </tr>
                
                </table>
            </td>
        </tr>
        
    </table>
</asp:Content>