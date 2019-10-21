<%@ Page MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/HRMS.master" 
    AutoEventWireup="true" CodeFile="Promotion.aspx.cs" Inherits="Hrms_Additional_Default"
    Title="ePay-HRMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .move
        {
            position: absolute;
            top: 325px;
            width: 150px;
            height: 55px;
            padding-left: 15px;
            left: 750px;
            display: inline;
        }

    </style>
    <table width="100%">
        <tr >
            <td>
                <span class="Title">&nbsp;&nbsp;&nbsp; <span class="style1"><h3 class="page-header">Promotion</h3></span></span>
            </td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lblerror" runat="server" ForeColor="Red"></asp:Label>
                <asp:DropDownList ID="ddlemp_virtual" runat="server" Visible="false"  CssClass="form-control">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    
    <div id="div_content_grade" style="width: 60%">
        <table class="table table-striped table-bordered table-hover">
            <tr id="tr_ddl_grade" runat="server">
                <td><asp:Label ID="lblgrade" runat="server" Text="Select Grade"></asp:Label></td>
                <td><asp:DropDownList ID="ddlgrade" runat="server" CssClass="form-control" 
                        AutoPostBack="true" onselectedindexchanged="ddlgrade_SelectedIndexChanged"><asp:ListItem>Select</asp:ListItem></asp:DropDownList></td>
            </tr>
            
            <tr id="tr_ddl_dept" runat="server">
                <td><asp:Label ID="lbldept" runat="server" Text="Select Department"></asp:Label></td>
                <td><asp:DropDownList ID="ddldept" runat="server" AutoPostBack="true" onselectedindexchanged="ddldept_SelectedIndexChanged"  CssClass="form-control"
                        Enabled="False"><asp:ListItem>Select</asp:ListItem></asp:DropDownList></td>
            </tr>
            
            <tr id="tr_chk_emp" runat="server" visible="false">
                <td><asp:Label  ID="lblemp" runat="server" Text="Employees"></asp:Label></td>
                <td><asp:CheckBoxList ID="chk_emp" CssClass="form-control" runat="server"></asp:CheckBoxList></td>
            </tr>        
              
            <tr id="tr_chk_virtual" runat="server" visible="false">
                <td>Virtual chk basic(text) empid(val)</td>
                <td><asp:CheckBoxList ID="chk_bp_empid" CssClass="form-control" runat="server"></asp:CheckBoxList></td>
            </tr>
            
            <tr id="tr_chk_emp_unalloted" runat="server">
                <td><asp:Label  ID="Label2" runat="server" Text="Unalloted Employees"></asp:Label></td>
                <td>
                    <asp:CheckBox ID="chk_sel_al" runat="server" Checked="false" 
                        AutoPostBack="true" Text="Select All" CssClass="form-control"
                        oncheckedchanged="chk_sel_al_CheckedChanged" />
                    <asp:CheckBoxList ID="chk_emp_unalloted" CssClass="form-control" runat="server"></asp:CheckBoxList>
                </td>
            </tr>
            
            <tr id="tr_chk_emp_alloted" runat="server">
                <td><asp:Label  ID="Label1" runat="server" Text="Alloted Employees"></asp:Label></td>
                <td>
                    <asp:CheckBox ID="chk_sel_al2" runat="server" Checked="false" Text="Select All" CssClass="form-control" AutoPostBack="true" oncheckedchanged="chk_sel_al2_CheckedChanged" />
                    <asp:CheckBoxList ID="chk_emp_alloted" CssClass="form-control" runat="server"></asp:CheckBoxList>
                </td>
            </tr>
            <tr id="tr_inc_per" runat="server" visible="False">
                <td><asp:Label ID="lblper" runat="server" Text="Allowed Increment"></asp:Label></td>
                <td><asp:TextBox ID="txtper" runat="server" CssClass="form-control"></asp:TextBox></td>
            </tr>
            <tr id="tr_date" runat="server">
                <td><asp:Label ID="Label8" runat="server" Text="Date(mm/dd/yyyy)"></asp:Label></td>
                <td><asp:TextBox ID="txtdate" runat="server" CssClass="form-control"></asp:TextBox></td>
            </tr>
            <tr id="tr_cmd" runat="server">
                <td><asp:Button ID="cmd_allot" runat="server" Text="Make Allotment" onclick="cmd_allot_Click" class="btn btn-success"/></td>
                <td><asp:Button ID="cmd_deallot" runat="server" Text="Delete Allotment" onclick="cmd_deallot_Click" class="btn btn-success"/></td>
            </tr>
            <tr id="tr1" runat="server">
                <td colspan="2" align="center"><asp:Button ID="Button1" runat="server" Text="View Allotments" Visible="True" onclick="Button1_Click1" class="btn btn-info"/></td>
            </tr>
        </table>
    </div>
    <div id="div_chk_allotmet" runat="server" visible="false">
        <asp:LinkButton ID="lnk_chk_allot" runat="server" Text="Check Allotments" onclick="lnk_chk_allot_Click"></asp:LinkButton>
    </div>
    <br />
    <asp:ListView ID="lv_details" runat="server">
        <LayoutTemplate>
            <table class="table table-striped table-bordered table-hover">
                <tr>
                    <td>EmployeeName</td>
                    <td>Employee ID</td>
                    <td>Basic</td>
                    <td>Increment_Percentage</td>
                    <td>Salary</td>
                    <td>Date(dd/mm/yyy)</td>
                    
                </tr>
                <tr id="itemplaceholder" runat="server"></tr>
            
            </table>
        </LayoutTemplate>
        <ItemTemplate>
        <tr>
            <td><asp:Label ID="lbl1" runat="server" Text='<%#Eval("employeename")%>'></asp:Label></td>
            <td><asp:Label ID="Label3" runat="server" Text='<%#Eval("empid") %>'></asp:Label></td>
            <td><asp:Label ID="Label4" runat="server" Text='<%#Eval("basic") %>'></asp:Label></td>
            <td><asp:Label ID="Label5" runat="server" Text='<%#Eval("inc_percentage") %>'></asp:Label></td>
            <td><asp:Label ID="Label6" runat="server" Text='<%#Eval("cal_amt") %>'></asp:Label></td>
            <td><asp:Label ID="Label7" runat="server" Text='<%#Eval("date","{0:dd-M-yyyy}") %>'></asp:Label></td>
        </tr>
        </ItemTemplate>
    
    </asp:ListView>
   
    
    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Animated="true" TargetControlID="txtdate">
    </asp:CalendarExtender>
    
    
</asp:Content>
