<%@ Page Title="" Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="Edit_PastEmploymentDetails.aspx.cs" Inherits="Hrms_Employee_Edit_PastEmploymentDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<br />
<script type="text/javascript">

    function isNumber(evt) {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
    function onlyAlphabets(e, t) {
        try {
            if (window.event) {
                var charCode = window.event.keyCode;
            }
            else if (e) {
                var charCode = e.which;
            }
            else { return true; }
            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123))
                return true;
            else
                return false;
        }
        catch (err) {
            alert(err.Description);
        }
    }
</script>

<div>
 <h2 class="panel-title">
  Update Past Employment Details
    </h2><br /> <br /> 
    <table id="Table1" cellspacing="0" cellpadding="0" runat="server" border="0" width="100%" class="table table-striped table-bordered table-hover">
    <tr>
    <td>SalaryStructure</td><td>
        <asp:TextBox ID="txtSalaryStructure" runat="server"></asp:TextBox></td>
    <td>Position Held</td><td>
        <asp:TextBox ID="txtPositionHeld" runat="server"></asp:TextBox></td>
    <td>Training Attended</td><td>
        <asp:TextBox ID="txtTrainingAttended" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
    <td> Training Duration</td>
    <td>
        <asp:TextBox ID="txtTrainingDuration" runat="server"></asp:TextBox></td>
    </tr>    
   <tr>
   <td colspan="6">   
   <h4>Reference1</h4>
   </td>
   </tr>
   <tr>
   <td> Person Name</td><td>
    <asp:TextBox ID="Ref1_PersonName" runat="server"></asp:TextBox></td>
    <td>Relationship</td>
    <td> <asp:TextBox ID="Ref1_Relationship" runat="server"></asp:TextBox></td>
        <td> Contact Phone No.</td><td>
            <asp:TextBox ID="Ref1_ContactPhoneNo" runat="server"></asp:TextBox></td>   
   </tr>
    <tr>
    <td> Contact Email_ID </td><td>
             <asp:TextBox ID="Ref1_ContactEmail" runat="server"></asp:TextBox></td>       
    </tr>
    <tr>
   <td colspan="6">   
   <h4>Reference2</h4>
   </td>
   </tr>
   <tr>
   <td> Person Name</td><td>
    <asp:TextBox ID="Ref2_PersonName" runat="server"></asp:TextBox></td>
    <td>Relationship</td>
    <td> <asp:TextBox ID="Ref2_Relationship" runat="server"></asp:TextBox></td>
        <td> Contact Phone No.</td><td>
            <asp:TextBox ID="Ref2_ContactPhoneNo" runat="server"></asp:TextBox></td>   
   </tr>
    <tr>
    <td> Contact Email_ID </td><td>
             <asp:TextBox ID="Ref2_ContactEmailID" runat="server"></asp:TextBox></td>       
    </tr>
         
 <tr>
  <td colspan="2">
  <asp:Button ID="btn_Back" runat="server" Text="Back" class="btn btn-info"  onclick="btn_Back_Click"  />
  <asp:Button ID="btnUpdate" runat="server" Text="Update" class="btn btn-success" onclick="btnUpdate_Click"/></td>
  <td></td>
  <td></td>
  <td></td>
  <td></td>
 </tr>    
    </table>
</div>
</asp:Content>

