<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="PreviewBranch.aspx.cs" Inherits="Hrms_Company_Default2" Title="Welcome to HRMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="row">
                <div class="col-lg-12">
                    <h2 class="page-header">Branch</h2>
                </div>
                <!-- /.col-lg-12 -->
            </div>

            <div class="panel panel-default">
                        <div class="panel-heading">
                            Branch Informations
                            <div class="pull-right">
                            </div>
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>

                            <div align="center" id="morris-area-chart" style="width: 80%">
                
                               <table class="table table-striped table-bordered table-hover">
                                    <tbody>
                                        <tr id="Branch_Selection" runat="server">
                                            <td align="right" >Select Branch</td>
                                            <td>
                                                <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="True" 
                                                    CssClass="form-control" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <span style="font-family: Calibri"><span style="color: #444444">
                                                <span style="color: #6A6A6A">
                                                <asp:Button ID="btn_save" runat="server" class="btn btn-success" 
                                                    onclick="btn_save_Click" Text="Add Branch" />
                                                </span></span></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="lbl_Error" runat="server" CssClass="Error" Font-Bold="True" 
                                                    ForeColor="Red"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" >
                    <table id="tbl_branch" runat="server" class="table table-striped table-bordered table-hover">
                                    
                                    <tr>
                                        <td style="width: 13%">
                                            Branch Code</td>
                                        <td style="width: 20%">
                                            <asp:Label ID="lbl_branchcode" runat="server" Font-Names="Calibri" 
                                                ForeColor="#6A6A6A" Width="124px"></asp:Label>
                                        </td>
                                            <td style="width: 13%">
                                                Address 1</td>
                                            <td style="width: 20%">
                                                <asp:Label ID="lbl_Address1" runat="server" Font-Names="Calibri" 
                                                    ForeColor="#6A6A6A" Width="100px"></asp:Label>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td style="width: 275px">
                                            Branch Name</td>
                                        <td>
                                                <span style="font-family: Calibri"><span style="color: #6A6A6A">
                                                <asp:Label ID="lbl_branchname" runat="server"></asp:Label>
                                                </span></span></td>
                                            <td>
                                                Address 2</td>
                                            <td>
                                                <asp:Label ID="lbl_Address2" runat="server" Font-Names="Calibri" 
                                                    ForeColor="#6A6A6A" Width="100px"></asp:Label>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td style="width: 275px">
                                            PF Code</td>
                                        <td>
                                                <asp:Label ID="lbl_pfcode" runat="server" Font-Names="Calibri" 
                                                    ForeColor="#6A6A6A"></asp:Label>
                                        </td>
                                            <td>
                                                City </td>
                                            <td>
                                                <asp:Label ID="lbl_city" runat="server" Font-Names="Calibri" 
                                                    ForeColor="#6A6A6A" Width="100px"></asp:Label>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td style="width: 275px">
                                            ESI Code</td>
                                        <td>
                                                <asp:Label ID="lbl_esicode" runat="server" Font-Names="Calibri" 
                                                    ForeColor="#6A6A6A"></asp:Label>
                                        </td>
                                            <td>
                                                Pincode</td>
                                            <td>
                                                <asp:Label ID="lbl_zip" runat="server" Font-Names="Calibri" ForeColor="#6A6A6A"></asp:Label>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td style="width: 275px">
                                            Start Date</td>
                                        <td>
                                                <asp:Label ID="lbl_startdate" runat="server" Font-Names="Calibri" 
                                                    ForeColor="#6A6A6A"></asp:Label>
                                        </td>
                                            <td>
                                                State</td>
                                            <td>
                                                <asp:Label ID="lbl_state" runat="server" Font-Names="Calibri" 
                                                    ForeColor="#6A6A6A"></asp:Label>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td style="width: 275px">
                                            End Date</td>
                                        <td>
                                                <asp:Label ID="lbl_enddate" runat="server" Font-Names="Calibri" 
                                                    ForeColor="#6A6A6A"></asp:Label>
                                        </td>
                                            <td>
                                                Country</td>
                                            <td>
                                                <asp:Label ID="lbl_country" runat="server" Font-Names="Calibri" 
                                                    ForeColor="#6A6A6A" Width="112px"></asp:Label>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td style="width: 275px">
                                            Email ID</td>
                                        <td>
                                                <asp:Label ID="lbl_email" runat="server" Font-Names="Calibri" 
                                                    ForeColor="#6A6A6A"></asp:Label>
                                        </td>
                                            <td>
                                                Phone No</td>
                                            <td>
                                                <asp:Label ID="lbl_phone" runat="server" Font-Names="Calibri" 
                                                    ForeColor="#6A6A6A"></asp:Label>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td style="width: 275px">
                                            Alternate Email ID</td>
                                        <td>
                                                <asp:Label ID="lbl_altemail" runat="server" Font-Names="Calibri" 
                                                    ForeColor="#6A6A6A"></asp:Label>
                                        </td>
                                            <td>
                                                Fax No</td>
                                            <td>
                                                <asp:Label ID="lbl_fax" runat="server" Font-Names="Calibri" ForeColor="#6A6A6A"></asp:Label>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td style="width: 275px">
                                            &nbsp;</td>
                                        <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <span style="font-family: Calibri"><span style="color: #444444">
                                                <span style="color: #6A6A6A">
                                                <asp:Button ID="btn_update" runat="server" class="btn btn-success" 
                                                    Text="Modify" onclick="btn_update_Click" />
                                                </span></span></span>
                                        </td>
                                    </tr>


                                </table>
                                                </td>
                                        </tr>
                                    </tbody>
                                </table> 
                
                            </div>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        
                    </div>

    </asp:Content>

