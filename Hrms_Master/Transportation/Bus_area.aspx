<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bus_area.aspx.cs" MasterPageFile="~/Hrms_Master/Employee/Data_Master.master" Inherits="Hrms_Master_Transportation_Bus_area" %>

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

        function validate() {

            var r = confirm("Are you sure you want to delete this record?");
            if (r == true) {
                return true;
            }
            else {
                return false;
            }
        }

        function show_message() {
            alert("Designation Name Already Exist");
        }

        function show_Error() {
            alert("Enter Designation Name");
        }

        function fnSave() {
            if (document.aspnetForm.ctl00$ContentPlaceHolder1$DesignationName.value == "") {
                alert("Enter Designation Name");
                aspnetForm.ctl00$ContentPlaceHolder1$DesignationName.focus();
                return false;
            }
            else {
                return true;
            }
        }    
    </script>

    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td id="tdComposeHeader" valign="top" align="center">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="background-color:#dbcdcc" height="35px" class="border">
                            <span class="Title">&nbsp;&nbsp;<span 
                                style="font-family: Calibri; color: #800000;">Designation 
                            Master</span></span></td>
                            <td style="background-color:#dbcdcc">
                                <asp:DropDownList ID="ddl_branch" runat="server" AutoPostBack="True" 
                                  Width="115px">
                                </asp:DropDownList>
                                                        </td>
                    </tr>
                </table>
                <table id="des_add" runat="server" width="100%">
                    <tr>
                        <td class="dComposeItemLabel" nowrap 
                            
                            
                            style="font-family: Calibri; font-size: x-small; color: #6A6A6A; width: 505px;">
                            &nbsp;</td>
                        <td align="left" valign="baseline" style="width: 129px">
                            <asp:Label ID="lbl_Error" runat="server" ForeColor="Red" Font-Bold="True" 
                                Font-Names="Calibri" Font-Size="X-Small"></asp:Label></td>
                        <td align="center">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td nowrap 
                            
                            
                            style="font-family: Calibri; font-size: small; color: #6A6A6A; width: 505px; font-weight: bold;" 
                            align="right">
                          Boarding Area</td>
                        <td align="left" style="width: 129px">
                            <input class="InputDefaultStyle" runat="server" id="Boarding_area" 
                                onkeypress="AllowOnlyTNS();" maxlength="20" />
                            </td>
                        <td align="left">
                            <asp:ImageButton ImageUrl="~/Images/Add.jpg" ID="Img_btn_add" runat="server" OnClientClick="return fnSave();"
                                Text="Add" ImageAlign="AbsMiddle" onclick="Img_btn_add_Click" /></td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
                <table id="des_branch" runat="server" width="100%">
                    <%--<tr valign="top">
                        <td align="center" style="font-family: Calibri; color: #993333; background-color: #dbcdcc;">
                            Designation List</td>
                        <td id="branch_header" runat="server" align="center" style="font-family: Calibri; color: #993333; background-color: #dbcdcc;">
                            <asp:Label ID="Label1" runat="server" Text="Branch List" Font-Names="Calibri" 
                                ForeColor="#993333"></asp:Label>
                        </td>
                    </tr>--%>
                    <tr valign="top">
                        <td align="center">
                            <div class="gridheight_master">
                                <asp:GridView ID="grid_Boarding_point" runat="server" 
                                    AutoGenerateColumns="False" Width="100%"
                                    DataKeyNames="Area_id"
                                    CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" 
                                    onrowediting="Edit" 
                                    onrowupdating="Update" onrowcommand="grid_Boarding_point_RowCommand" 
                                    onrowdeleting="grid_Boarding_point_RowDeleting">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                    
                                        <asp:TemplateField>
                                        <HeaderTemplate>
                                        <table cellSpacing="0" cellPadding="0" width="100%">
                                            <COLGROUP>        
                                                <COL>                                
                                            </COLGROUP>
                                            <THEAD>
                                                <TR>
                                                    <TH align="left" style="width: 80%; font-family: Calibri; color: #FFFFFF; font-weight: bold;">Boarding Point  List</TH>
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
                                                          
                                                            <td style="width: 80%;" nowrap>
                                                            <input runat="server" id="txt_id" value='<%#Eval("Area_id") %>' visible="false" />
                                                            <input  runat="server" id="txt_area" onkeypress="AllowOnlyTNS();" 
                                                                    value ='<%# Eval("Area_name") %>' 
                                                                    style="font-family: Calibri; font-size:small; width: 250px;color: #6a6a6a" Height="21px" />
                                                            <%--   <asp:TextBox runat="server" Text='<%#Eval("DesignationName")%>' ID="txtgrid" Enabled="false"></asp:TextBox>--%>
                                                            </td>
                                                            <td align="center" style="width: 5%;" nowrap>
                                                                <asp:ImageButton ID="img_update" ImageUrl="../../Images/i_Edit.gif" runat="server" Style="border: 0"
                                                                    AlternateText="" CommandName="Update" />
                                                                <asp:ImageButton ID="img_save" ImageUrl="../../Images/save.gif" runat="server" Style="border: 0"
                                                                    AlternateText="" CommandName="Edit" Visible="false" /></td>
                                                                    <td style="width: 5%; height:25px;  font-family: Calibri; color: #c18685;" nowrap
                                                                    align="right">
                                                                <%--<asp:Label ID="lbl" runat="server" Text="Delete"></asp:Label>--%>
                                                                <asp:ImageButton ImageUrl="../../Images/delete_icon.gif" ID="imgdel" runat="server" CommandName="Delete" OnClientClick="return validate()" />
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
                         <td id="branch_body" runat="server" align="center">
                             &nbsp;</td>
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
                <input id="hDesignationID" runat="server" type="hidden" value="0" /></td>
        </tr>
    </table>
</asp:Content>
