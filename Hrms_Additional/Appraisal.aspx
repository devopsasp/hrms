<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Appraisal.aspx.cs" Inherits="Hrms_Additional_Default" Title="Welcome to HRMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
   
    function show_message()
    {
        alert("Leave Name Already Exist");
    }
    </script>

<script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>
<script language="javascript" type="text/javascript" src="../datecheck.js"></script>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td id="tdComposeHeader" valign="top" align="right">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr class="border">
                        <td style="" class="border">
                            <span class="Title">&nbsp;&nbsp;&nbsp;<span 
                                class="style1"><h3>Appraisal</h3></span></span></td>
                        <td align="left" style="width: 276px; height: 29px" valign="baseline">
                            <asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged"
                                CssClass="form-control">
                            </asp:DropDownList></td>
                    </tr>
                </table>
                <table cellpadding="5" cellspacing="1" width="100%" id="tbl_details" runat="server">
                    <tr id="Tr1" runat="server">
                        <td colspan="4" align="center">
                            &nbsp;<asp:Label ID="lbl_Error" CssClass="Error" runat="server" ForeColor="Red" Font-Bold="True" Width="40%"></asp:Label></td>
                    </tr>
                    <%--<tr id="row_branch" runat="server">
                                            <td class="dComposeItemLabel" nowrap="nowrap" style="height: 29px">
                                                Branch</td>
                                            <td align="left" style="width: 276px; height: 29px" valign="baseline">
                                                <asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged"
                                                    CssClass="InputDefaultStyle">
                                                </asp:DropDownList></td>
                                            <td class="dComposeItemLabel" nowrap="nowrap" style="height: 29px">
                                            </td>
                                            <td style="width: 113px; height: 29px">
                                            </td>
                                        </tr>--%>
                    <tr id="row_emp" runat="server">
                        <td class="dComposeItemLabel" nowrap="nowrap" 
                            style="height: 29px; font-family: Calibri; font-size: x-small;">
                            Employee</td>
                        <td align="left" style="height: 29px; width: 276px;" valign="baseline">
                            <asp:DropDownList ID="ddl_Employee" runat="server"  CssClass="form-control" AutoPostBack="True"
                                OnSelectedIndexChanged="ddl_Employee_SelectedIndexChanged" 
                                Font-Names="Calibri" Font-Size="X-Small">
                            </asp:DropDownList></td>
                        <td class="dComposeItemLabel" nowrap="nowrap" style="height: 29px">
                        </td>
                        <td style="height: 29px; width: 113px;">
                        </td>
                    </tr>
                    <tr id="Tr2" runat="server">
                        <td class="dComposeItemLabel" nowrap="nowrap" 
                            style="font-family: Calibri; font-size: x-small">
                            Enter Date</td>
                        <td>
                        <input type="text" runat="server"  CssClass="form-control" id="txt_date" 
                                style="width:45%;" onkeyup="fn_date(event,this.id);" maxlength="10" />
                            <%--<asp:TextBox ID="txt_date" runat="server" CssClass="InputDDLStyle" Width="55%"></asp:TextBox>--%>
                        </td>
                        <td class="dComposeItemLabel" nowrap="nowrap" style="height: 29px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            &nbsp;</td>
                    </tr>
                </table>
                <table width="100%" id="tbl_grd" runat="server">
                    <tr valign="top">
                        <td width="50%" valign="top">
                            <asp:GridView ID="grid_appraisal" runat="server" AutoGenerateColumns="False" Width="100%"
                                DataKeyNames="AppraisalID"  ForeColor="#333333">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                <colgroup>
                                                    <col>
                                                </colgroup>
                                                <thead>
                                                    <tr>
                                                        <th style="width: 80%; font-family: Calibri;">
                                                            Appraisal Name</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table class="dItemListContentTable" cellspacing="0" cellpadding="0" width="100%">
                                                <colgroup>
                                                    <col class="dInboxContentTableCheckBoxCol">
                                                </colgroup>
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 40%;" nowrap>
                                                            <asp:TextBox runat="server" Text='<%#Eval("AppraisalName")%>' ID="txtAppname" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                <colgroup>
                                                    <col>
                                                </colgroup>
                                                <thead>
                                                    <tr>
                                                        <th style="width: 80%; font-family: Calibri;">
                                                            Total Points</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table class="dItemListContentTable" cellspacing="0" cellpadding="0" width="100%">
                                                <colgroup>
                                                    <col class="dInboxContentTableCheckBoxCol">
                                                </colgroup>
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 40%;" nowrap>
                                                        <%--<input type="text" id="txttotpts" value='<%#Eval("totalpoint")%>' runat="server" onkeydown="AllowOnlyNumeric1(event);" />--%>
                                                            <asp:TextBox runat="server" Text='<%#Eval("totalpoint")%>' ID="txttotpts"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                <colgroup>
                                                    <col>
                                                </colgroup>
                                                <thead>
                                                    <tr>
                                                        <th style="width: 80%; font-family: Calibri;">
                                                            Allotted Points</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table class="dItemListContentTable" cellspacing="0" cellpadding="0" width="100%">
                                                <colgroup>
                                                    <col class="dInboxContentTableCheckBoxCol">
                                                </colgroup>
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 40%;" nowrap>
                                                        <input type="text" id="txtpoints" value='<%#Eval("Count")%>' runat="server" onkeydown="AllowOnlyNumeric1(event);" />
                                                            <%--<asp:TextBox runat="server" Text='<%#Eval("Count")%>' ID="txtpoints"></asp:TextBox>--%>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle  Font-Names="Calibri" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td valign="top" width="50%">
                        </td>
                    </tr>
                    <tr runat="server" id="row_totpts" valign="top">
                        <td valign="top" width="50%" style="height: 22px">
                            <span class="dComposeItemLabel">&nbsp; &nbsp;&nbsp;<br />
&nbsp;&nbsp;&nbsp; Total Points&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; : </span>&nbsp;<input
                                 CssClass="form-control" runat="server" id="txttot_pts" disabled="disabled" /></td>
                    </tr>
                    <tr runat="server" id="row_totamt" valign="top">
                        <td valign="top" width="50%">
                            <span class="dComposeItemLabel">&nbsp;&nbsp;&nbsp; Increment(Rs) : </span>&nbsp;<input  CssClass="form-control" runat="server" id="txttot_amt" disabled="disabled" />
                            <asp:HyperLink 
                                ID="HyperLink1" runat="server" Font-Names="Calibri" 
                                Font-Size="Small" 
                                NavigateUrl="~/Hrms_Additional/Annual_increment.aspx" Width="212px">View 
                            Allot Increment for this employee</asp:HyperLink>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr valign="top">
                        <td valign="top" width="50%">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <asp:Button ID="btn_Back" runat="server" Text="Back"  CausesValidation="False" OnClick="btn_Back_Click" class="btn btn-info"/>
                           <%-- <asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/Images/Back.png" onmouseover="this.src='Backover.png';" onmouseout="this.src='Back.png';" OnClick="btn_Back_Click"
                                CausesValidation="False" />--%>
                            <asp:Button ID="btn_save" runat="server" Text="Save" OnClick="btn_save_Click" class="btn btn-success"/>
                           <%-- <asp:ImageButton ID="btn_save" runat="server" ImageUrl="~/Images/Save.png" onmouseover="this.src='Saveover.png';" onmouseout="this.src='Save.png';" 
                                OnClick="btn_save_Click" />--%>
                            <asp:Button ID="btn_update" runat="server" Text="Update"  OnClick="btn_update_Click" class="btn btn-info"/>
                           <%-- <asp:ImageButton ID="btn_update" runat="server" ImageUrl="~/Images/Modify.png" onmouseover="this.src='Modifyover.png';" onmouseout="this.src='Modify.png';" 
                                OnClick="btn_update_Click" />--%>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
