<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="Employee_Photo.aspx.cs" Inherits="Hrms_Employee_Default5" Title="Welcome to HRMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<div><h3 class="page-header">Upload Photo</h3></div>
<div align="center" class="page-header">
    <asp:Label ID="lbl_empcodename" runat="server" Font-Bold="True" 
        Font-Size="Medium"></asp:Label>
    </div>

        <div style="width: 70%">
                    <table cellpadding="1%" cellspacing="1%" width="100%" class="table table-striped table-bordered table-hover">
                        
                        <tr>
                            <td>
                                Employee Photo</td>
                            <td >
                                <asp:FileUpload ID="UploadImage" runat="server" />
                            </td>
                            <td >
                                Family Photo</td>
                            <td>
                                <asp:FileUpload ID="UploadImage1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:Image ID="img_emp_photo" runat="server" Height="149px" Width="152px" />
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:Image ID="img_emp_photo1" runat="server" Height="149px" Width="152px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="right">
                                <asp:Button ID="btn_Back" runat="server" class="btn btn-info" 
                                    OnClick="btn_Back_Click" Text="Back" />
                                <asp:Button ID="btn_save" runat="server" class="btn btn-success" 
                                    OnClick="btn_save_Click" Text="Save" />
                                <asp:Button ID="btn_update" runat="server" class="btn btn-success" 
                                    OnClick="btn_update_Click" Text="Update" />
                            </td>
                        </tr>
                     </table>   
                     </div>
                     <div>  

                         </td>                                    
           </div>

</asp:Content>

