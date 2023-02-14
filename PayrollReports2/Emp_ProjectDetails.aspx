<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master_Page/EmployeeMaster.master" CodeFile="Emp_ProjectDetails.aspx.cs" Inherits="PayrollReports_Emp_ProjectDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>
    <script language="javascript" type="text/javascript"></script>
    

    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td height="30px" align="left" bgcolor="#5D7B9D" width="80%">
                <span class="Title" 
                    style="font-family: calibri; font-size: medium; font-weight: bold; color: #FFFFFF">&nbsp;&nbsp;<img src="../Images/rp_arrow.gif" />&nbsp;Employee Project Report</span></td>
            <td height="30px" align="center" bgcolor="#5D7B9D">
                &nbsp;<asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" CssClass="InputDefaultStyle"
                    >
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="0" cellspacing="0" id="tbl_pfreport" runat="server">
        <tr valign="top">
            <td class="tdComposeHeader" valign="top" align="right">
                <table width="100%">
                    <tr>
                        <td style="width: 20%" valign="top">
                            <table cellpadding="5" cellspacing="1" width="100%" border="1">
                                <%--bordercolor="#e5a81a"--%>
                                <tr>
                                    <td align="center" class="QryTitlereports" style="width: 44%">
                                        <asp:Label ID="lbl_error" runat="server" Text="Welcome To Report Section" ForeColor="Red" Font-Bold="True" Width="75%"
                                            Font-Size="Medium"></asp:Label></td>
                                    <td align="center" class="QryTitlereports" width="40%">

                                        <asp:Label ID="Label1" runat="server" ForeColor="Red" Font-Bold="True" Width="75%"
                                            Font-Size="Medium"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td runat="server" visible="false" id="div_chk_Empcode" height="30" valign="top" style="width: 44%;font-size:10pt;"
                                                        align="center">
                                                        <div class="qrychkbox_big" style="height: 230px; left: 0px; top: 0px;" id="div_chkempcode" runat="server">
                                                            <asp:CheckBoxList Height="200px" ID="chk_Empcode"  runat="server" CssClass="InputDefaultStyle1"
                                                                Width="90%">
                                                               
                                                            </asp:CheckBoxList>
                                                        </div>
                                                        <input type="checkbox" id="chkall" runat="server" onclick="javascript: fn_chkall(this.id,'ctl00_ContentPlaceHolder1_chk_Empcode')" />
                                        <asp:Label ID="lbl_selectemp" runat="server" Text="Select All Employees" Font-Names="Calibri"></asp:Label> 
                                                    </td>
                                    <td runat="server" id="div_chk_Master" valign="top" align="center" width="40%">
                                        <table width="100%" cellpadding="7">
                                                                                         <tr id="Tr1" visible="false" runat="server">
                                                <td class="dComposeItemLabel1" style="width: 162px">
                                                    <p style="margin-left: 50px">
                                                    <asp:Label ID="Label2" runat="server" Text="Select Type"></asp:Label>
                                                    </p>
                                                </td>
                                                <td align="left">
                                                     <asp:DropDownList ID="ddl_type" Width="175px" runat="server" 
                                                         AutoPostBack="True" CssClass="InputDefaultStyle1" 
                                                        >
                                                          <asp:ListItem>Select</asp:ListItem>
                                                          <asp:ListItem>Employee Vs Bench</asp:ListItem>
                                                          <asp:ListItem>Bench Vs OverHeading</asp:ListItem>
                                                          </asp:DropDownList>
                                                   
                                                    </td>
                                            </tr>                                                                               
                                            <tr id="row_EpfAcno" runat="server">
                                                <td class="dComposeItemLabel1" style="width: 162px">
                                                    <p style="margin-left: 50px">
                                                    <asp:Label ID="lbl_dept" runat="server" Text="Select Department"></asp:Label>
                                                    </p>
                                                </td>
                                                <td align="left">
                                                     <asp:DropDownList ID="ddl_department" Width="175px" runat="server" 
                                                         AutoPostBack="True" 
                                                         
                                                         CssClass="InputDefaultStyle1" onselectedindexchanged="ddl_department_SelectedIndexChanged" 
                                                         >
                                                          <asp:ListItem>Select</asp:ListItem></asp:DropDownList>
                                                   
                                                    </td>
                                            </tr> 
                                          <tr id="Tr2" runat="server">
                                                <td class="dComposeItemLabel1" style="width: 162px">
                                                    <p style="margin-left: 50px">
                                                    <asp:Label ID="Label3" runat="server" Text="Select Employee"></asp:Label>
                                                    </p>
                                                </td>
                                                <td align="left">
                                                     <asp:DropDownList ID="ddl_Employee" Width="175px" runat="server" 
                                                         AutoPostBack="True" 
                                                         
                                                         CssClass="InputDefaultStyle1" >
                                                        
                                                          </asp:DropDownList>
                                                   
                                                    </td>
                                            </tr>                                                                                         
                                            <tr id="row_PfAcno" runat="server">
                                                <td  class="dComposeItemLabel1" style="width: 162px">
                                                    <p style="margin-left: 50px">
                                                    <asp:Label ID="lbl_from_date" runat="server" Text="From Date"></asp:Label>
                                                    </p>
                                                </td>
                                                <td align="left" class="dComposeItemLabel1">
                                                <asp:TextBox ID="txtFromDate" runat="server" onkeyup="fn_date(event,this.id);" 
                                                        MaxLength="10" style="Width:170px;" CssClass="InputDefaultStyle1"></asp:TextBox>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                            </tr>
                                            <tr id="row_DliAcno" runat="server">
                                                <td class="dComposeItemLabel1" style="width: 162px">
                                                    <p style="margin-left: 50px">
                                                    <asp:Label ID="lbl_toDate" runat="server" Text="To Date"></asp:Label>
                                                    </p>
                                                </td>
                                                <td align="left">
                                                <asp:TextBox ID="txtToDate" runat="server" onkeyup="fn_date(event,this.id);" 
                                                        MaxLength="10" style="Width:170px;" CssClass="InputDefaultStyle1"></asp:TextBox>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkoverheading" runat="server" 
                                                        Text="Include O/H Cost " Font-Names="Calibri" Font-Size="X-Small" />
                                                    </td>
                                                    
                                            </tr>
                                               
                                           
                                         </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="TextStyle" align="center">
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td align="center" style="width: 20%; height: 20px" valign="middle">
                                            <asp:ImageButton ID="btn_Report" runat="server" 
                                                ImageUrl="../Images/Show_Report.jpg" onclick="btn_Report_Click" 
                                                 /></td>
                                    </tr>

                                </tbody>
                            </table>

                        </td>
                    </tr>
                </table>
                <input type="hidden" id="ddl_selrep" runat="server" /></td>
        </tr>
    </table>

</asp:Content>

