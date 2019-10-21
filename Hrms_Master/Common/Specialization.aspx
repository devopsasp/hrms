<%@ Page Title="" Language="C#" MasterPageFile="~/HRMS.master" 
AutoEventWireup="true" CodeFile="Specialization.aspx.cs" Inherits="Hrms_Master_Common_Specialization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language="javascript" type="text/javascript">
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return true;
        return false;
    }
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
        alert("Specialization Name Already Exist");
    }

    function show_Error() {
        alert("Enter Specialization Name");
    }

    function fnSave() {
        if (document.aspnetForm.ctl00$ContentPlaceHolder1$SpecializationName.value == "") {
            alert("Enter Specialization Name");
            aspnetForm.ctl00$ContentPlaceHolder1$SpecializationName.focus();
            return false;
        }
        else {
            return true;
        }
    }
    function coudnt_del() {
        alert("Cannot delete. This Specialization is already assigned to someone");
    }   
    </script>
    <div><h2 class="page-header">Specialization</h2></div>

     <table width="100%" cellpadding="0" cellspacing="0" >
        <tr valign="top">
            <td id="tdComposeHeader" valign="top" align="right">               
                <table cellpadding="2%" cellspacing="1%" width="100%" class="table">
                  <tr>
                        <td align="right" width="25%">
                            <b> Specialization Name&nbsp;&nbsp;&nbsp; </b></td>
                        <td  align="center" width="45%" >
                            <input class="form-control" runat="server" id="SpecializationName" onkeypress="return isNumberKey(event)"                               
                                maxlength="20" /></td>
                        <td   align="left" width="33%" >
                            <asp:Button ID="btnAddSpecialization" runat="server"  
                                OnClientClick="return fnSave();" CssClass="btn btn-success"
                                Text="Add" onclick="btnAddSpecialization_Click"/>
                            </td>                       
                    </tr>
                 
                     <tr valign="top">
            <td colspan="4" id="td1" valign="top" align="right">
                    <table cellpadding="2%" cellspacing="1%" width="100%">
                    <tr>
                        <td colspan="4" align="center">                           
                                <asp:GridView ID="Grid_Specialization" runat="server" AutoGenerateColumns="False" Width="60%"
                                    DataKeyNames="CourseId" CellPadding="4" CssClass="table table-striped table-bordered table-hover" 
                                    onrowcommand="Grid_Specialization_RowCommand"  onrowupdating="Update" OnRowDeleting="Grid_Specialization_RowDeleting1" OnRowEditing="Edit" >                                 
                                    <RowStyle  />
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <table cellspacing="0" cellpadding="0" width="100%">                                                   
                                                    <thead>
                                                        <tr>
                                                            <th align="left" style="width: 86%; ">
                                                                Specialization List</th>
                                                            <td  style="width: 7%; ">
                                                                <asp:Label ID="lbledit" Text="Edit" runat="server"></asp:Label></td>
                                                                 <td id="del" style="width: 7%; ">
                                                               <asp:Label ID="lbldel" Text="Delete" runat="server"></asp:Label></td>
                                                        </tr>
                                                    </thead>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table class="dItemListContentTable" border="0" cellspacing="0" cellpadding="0" width="100%">
                                                    <colgroup>
                                                        <col class="dInboxContentTableCheckBoxCol">
                                                    </colgroup>
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 86%;">
                                                                <input runat="server" id="txtgrid" value='<%# Eval("SpecializationName") %>' disabled="disabled"  class="form-control"  style="width:100%;" />
                                                             </td>
                                                            <td align="center"  >
                                                                <asp:LinkButton ID="img_update"  runat="server"  CssClass="btn btn-success btn-circle glyphicon glyphicon-check" CommandName="Update"></asp:LinkButton>
                                                                <asp:LinkButton ID="img_save"  CssClass="btn btn-success btn-circle glyphicon glyphicon-plus-sign" runat="server"   AlternateText="" CommandName="Edit" Visible="false"></asp:LinkButton>
                                                             </td>
                                                             <td style="width:7%; " align="center">
                                                                <asp:LinkButton ID="imgdel" CssClass="btn btn-danger btn-circle glyphicon glyphicon-minus-sign"  runat="server"
                                                                 CommandName="Delete" OnClientClick="return validate()"></asp:LinkButton>
                                                             </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                    </tr>
                   
                    </table>
                    </td>
                    </tr>
                       <tr ><td colspan="4"></td></tr>                    
                    </table>
                    </td>
                    </tr>
        <tr>
            <td align="center" >
                &nbsp;</td>
        </tr>
         <tr>
            <td>
                <input id="hSpecializationID" runat="server" type="hidden" value="0" />
                <input type="hidden" id="ToolBarCode" name="ToolBarCode" runat="server" value="0" />
            </td>
        </tr>
    </table>
</asp:Content>

