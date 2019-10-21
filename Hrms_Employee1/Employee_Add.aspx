<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Employee_Add.aspx.cs" Inherits="Hrms_Employee_Default2" Title="Welcome to HRMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../Css/Cand_BaseStyle.css" rel="Stylesheet" type="text/css" />
    <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Conditional">
    <ContentTemplate>
    <fieldset style="border-style: none">
            <table id="emp_add" runat="server" class="roundedTable" style="border-style: groove; border-width: thin; width: 100%; font-family: Calibri; font-size: small;" 
                align="center" bgcolor="#F9F9F9">
                <tr>
                    <td colspan="5" bgcolor="#E2E2E2" height="30px">
                        &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large" 
                            ForeColor="#666666" Text="Add Employee"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%">
                        &nbsp;</td>
                    <td align="center">
                        <span style="color: #CC3300">*</span><asp:Label ID="Label3" runat="server" ForeColor="Gray" 
                            style="font-style: italic" Text="First Name"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:Label ID="Label4" runat="server" ForeColor="Gray" 
                            style="font-style: italic" Text="Middle Name"></asp:Label>
                    </td>
                    <td align="center">
                        <span style="color: #CC3300">*</span><asp:Label ID="Label5" runat="server" Font-Italic="True" ForeColor="Gray" 
                            Text="Last Name"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="height: 26px; width: 15%;">
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label2" runat="server" Text="Employee Full Name" 
                            Font-Size="Medium" ForeColor="#666666"></asp:Label>
                    </td>
                    <td style="height: 26px; width: 20%;" align="center">
                        <asp:TextBox ID="TxtFirstName" CssClass="roundedbox" runat="server" Width="90%"></asp:TextBox>
                        </td>
                    <td style="height: 26px; width: 20%;" align="center">
                        <asp:TextBox ID="TxtMiddleName" CssClass="roundedbox" runat="server" 
                            Width="90%"></asp:TextBox>
                        </td>
                    <td style="height: 26px; width: 20%;" align="center">
                        <asp:TextBox ID="TxtLastName" CssClass="roundedbox" runat="server" Width="90%"></asp:TextBox>
                        </td>
                    <td style="height: 26px; width: 20%;">
                    
                    
                        </td>
                </tr>
                <tr style="font-size:6px">
                    <td style="height: 12px; width: 15%">
                    </td>
                    <td style="height: 12px">
                    </td>
                    <td style="height: 12px">
                    </td>
                    <td style="height: 12px">
                    </td>
                    <td style="height: 12px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label6" runat="server" Font-Size="Medium" 
                            ForeColor="#666666" Text="Employee ID"></asp:Label>
                        <span style="color: #CC3300">&nbsp;*</span></td>
                    <td align="center">
                        <asp:TextBox ID="TxtEmployeeID" runat="server" CssClass="roundedbox" 
                            Width="90%"></asp:TextBox>
                    </td>
                    <td align="center">
                        &nbsp;<asp:Label ID="Label7" runat="server" Font-Size="Medium" ForeColor="#666666" 
                            Text="Employee Card No"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="TxtCardNo" runat="server" CssClass="roundedbox" Width="90%"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr style="font-size:6px">
                    <td style="width: 15%">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 15%">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label8" runat="server" Font-Size="Medium" 
                            ForeColor="#666666" Text="Photograph"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="roundedbox" Width="90%" />
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr style="font-size:6px">
                    <td style="width: 15%">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 15%">
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label9" runat="server" Font-Size="Medium" ForeColor="#666666" 
                            style="font-weight: 700" Text="Create Login Details"></asp:Label>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr style="font-size:6px">
                    <td style="width: 15%">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td> 
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 15%">
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label10" runat="server" Font-Size="Medium" ForeColor="#666666" 
                            Text="User Name"></asp:Label>
                        &nbsp;<span style="color: #CC3300">*</span></td>
                    <td align="center">
                        <asp:TextBox ID="TxtUserName" runat="server" CssClass="roundedbox" Width="90%"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr style="font-size:6px">
                    <td style="width: 15%">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 15%">
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label11" runat="server" Font-Size="Medium" ForeColor="#666666" 
                            Text="Password"></asp:Label>
                        &nbsp;<span style="color: #CC3300">*</span></td>
                    <td align="center">
                        <asp:TextBox ID="TxtPassword" runat="server" TextMode="Password" CssClass="roundedbox" Width="90%"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                
               
                <tr style="font-size:6px">
                    <td style="width: 15%">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 15%">
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label12" runat="server" Font-Size="Medium" ForeColor="#666666" 
                            Text="Re-type Password"></asp:Label>
                        &nbsp;<span style="color: #CC3300">*</span></td>
                    <td align="center">
                        <asp:TextBox ID="TxtRePassword" runat="server" TextMode="Password" CssClass="roundedbox" 
                            Width="90%"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 15%">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                
                <tr>
                    <td style="width: 15%">
                        &nbsp;&nbsp;&nbsp;&nbsp;<span style="color: #CC3300">*</span>
                        <asp:Label ID="Label13" runat="server" Font-Italic="True" ForeColor="Gray" 
                            Text="Mandatory Fields"></asp:Label>
                        &nbsp;</td>
                    <td>
                        &nbsp;&nbsp;&nbsp;
                        <asp:ImageButton ID="Img_Save" runat="server" ImageUrl="~/hrimages/Save.png" 
                            onmouseover="this.src='../hrimages/SaveOver.png';" 
                            onmouseout="this.src='../hrimages/Save.png';" onclick="Img_Save_Click" />
                    </td>
                    <td style="text-align: center">
                        <asp:Label ID="lbl_error" runat="server" Font-Bold="True" ForeColor="#0066FF" 
                            style="text-align: center"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
               
            </table>    
    </fieldset>
    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
