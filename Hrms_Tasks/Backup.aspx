<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Backup.aspx.cs" Inherits="Hrms_Tasks_Default" Title="ePay-HRMS" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script language="javascript" type="text/javascript">
    function show_message(msg)    
    {
        alert(msg);
    }
    </script>
    <table style="width: 1019px" class="table table-striped table-bordered table-hover" >
        <tr>
            <td style="font-family: calibri;" valign="top" colspan="2">                
                <table id="personal_tbl" runat="server" width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="35px" class="border" >
                        <h3 class="page-header">
        Job Requisition
    </h3>
                          </td>
                    </tr>
                    <tr>
                        <td height="35px">
                            &nbsp;</td>
                    </tr>
                </table>
                
                </td>
        </tr>
        <tr>
            <td style="width: 356px; font-family: calibri;" valign="top">
                <fieldset>
                    <legend>Backup</legend>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <br />
                            <asp:RadioButton ID="rdo_button2" runat="server" Text="Default name with date and time"
                                Checked="true" AutoPostBack="True" OnCheckedChanged="rdo_button2_CheckedChanged" /><br />
                            <br />
                            <asp:RadioButton ID="rdo_button1" runat="server" Text="Enter the Backup Filename"
                                AutoPostBack="True" OnCheckedChanged="rdo_button1_CheckedChanged" /><br />
                            <asp:TextBox ID="txt_name" runat="server" Height="21px" Width="273px"></asp:TextBox><br />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <asp:Button ID="btn_backup" runat="server" Text="Backup" 
                        OnClick="btn_backup_Click"  class="btn btn-success" Font-Bold="True" 
                        Font-Names="Calibri" ForeColor="White" Width="80px" />
                    <br />
                </fieldset>
            </td>
            <td style="width: 374px; font-family: calibri;" valign="top">
                <fieldset>
                    <legend>Restore</legend>
                    <br />
                    <label>
                        Select the Restore Backup File<br />
                    </label><br />
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="357px"  CssClass="form-control" />
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="btn_restore" runat="server" Text="Restore" 
                        OnClick="btn_restore_Click" class="btn btn-info" Font-Bold="True" 
                        Font-Italic="False" Font-Names="Calibri" ForeColor="White" Width="80px" />
                    <br />
                </fieldset>
            </td>
        </tr>
        <tr>
            <td style="width: 356px; font-family: calibri;" valign="top">
                &nbsp;</td>
            <td style="width: 374px; font-family: calibri;" valign="top">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
