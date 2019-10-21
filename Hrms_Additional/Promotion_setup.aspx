<%@ Page MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/HRMS.master" 
    AutoEventWireup="true" CodeFile="Promotion_setup.aspx.cs" Inherits="Hrms_Additional_Default"
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
            <tr>
            <td>
                <span class="Title">&nbsp;&nbsp;&nbsp; <span class="style1"><h3 class="page-header">Promotion 
                    Setup</h3></span></span></td>
                <td>
                    <asp:DropDownList ID="ddl_branch" runat="server" AutoPostBack="True" 
                        Width="115px" onselectedindexchanged="ddl_branch_SelectedIndexChanged"  CssClass="form-control">
                    </asp:DropDownList>
                </td>
                </tr>
                </table>

        <div style="width: 80%">
         <table class="table table-striped table-bordered table-hover" width="100%">

        <tr>
            <td>
                <table class="table table-striped table-bordered table-hover" runat="server" id="tbl_PF">
                    <tr>
                        <td>
                            <asp:Label ID="lbl_dept" runat="server" Text="Select Department"></asp:Label>
                        </td>
                        <td >
                            <asp:DropDownList ID="ddl_dept" runat="server" CssClass="form-control" AutoPostBack="True" 
                                onselectedindexchanged="ddl_dept_SelectedIndexChanged" >
                            </asp:DropDownList>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <asp:Label ID="lbl_empname" runat="server" Text="Select Employee"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddl_employee" runat="server" CssClass="form-control" AutoPostBack="True" 
                                onselectedindexchanged="ddl_employee_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:LinkButton ID="lb_Check" runat="server" onclick="lb_Check_Click">
                            Check Promotions for this Department</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="2">
                            <table id="emp_details" runat="server" class="table table-striped table-bordered table-hover" style="width: 80%">
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="Emp_title" runat="server"  Text="Employee's Details" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-family: Calibri;">
                                        <p>
                                        <asp:Label ID="lbl_designation" runat="server" Text="Designation"></asp:Label>
                                        </p>
                                    </td>
                                    <td align="center" 
                                        style="font-family: Calibri; ">
                                        <asp:Label ID="lbl_designation2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-family: Calibri; ">
                                        <p>
                                        <asp:Label ID="lbl_grade" runat="server" Text="Grade"></asp:Label>
                                        </p>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_grade2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p>
                                        <asp:Label ID="lbl_Level" runat="server" Text="Level"></asp:Label>
                                        </p>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_Level2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_basic" runat="server" Text="Basic Salary"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_basic2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_join" runat="server" Text="Joining Date"></asp:Label>
                                        </td>
                                    <td>
                                        <asp:Label ID="lbl_join2" runat="server"></asp:Label>
                                        </td>
                                </tr>
                                <tr>
                                    <td>
                                        </td>
                                    <td>
                                      </td>
                                </tr>
                            </table>
                        </td>
                        <%--<td align="left" style="height: 36px" valign="baseline"></td>--%>
                        <td nowrap="nowrap" rowspan="2">
                            <table id="promo_details" class="table table-striped table-bordered table-hover" runat="server">
                                <tr>
                                    <td  colspan="2">
                                        <asp:Label ID="Promo_title" runat="server"  Text="Promote to" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_designation1" runat="server" Text="Designation"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_desig" runat="server"  CssClass="form-control" Width="100%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_grade1" runat="server" Text="Grade"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_grade" runat="server"  CssClass="form-control" 
                                            Width="100%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_Level1" runat="server" Text="Level"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_level" runat="server"  CssClass="form-control" 
                                            Width="100%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_basic1" runat="server" Text="Increase in basic (in %)"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="txt_basic" runat="server"  CssClass="form-control" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_edate" runat="server" Text="Effective Date"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_edate" runat="server"  CssClass="form-control" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="BtnSave" runat="server" Text="Save" Height="30px" Width="100px" onclick="BtnSave_Click" class="btn btn-success"/>
                                       
                                        <asp:Button ID="btnReset" runat="server" Text="Reset"  onclick="btnReset_Click" Height="30px" Width="100px" class="btn btn-success"/>
                                   
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <%-- <td align="left" style="height: 36px" valign="baseline">
                    </td>--%>
                    </tr>
                    </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lbl_error" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
 </div>
       
    
    </asp:Content>
