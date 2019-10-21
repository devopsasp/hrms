<%@ Page Title="" Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="EditBankDetails.aspx.cs" Inherits="Hrms_Employee_EditBankDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><br />
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

  <div class="panel-heading">
    <h4 class="panel-title">
   Update Bank Details
    </h4><br />     
    
        <table id="Table1" cellspacing="0" cellpadding="0" runat="server" border="0" width="100%" class="table table-striped table-bordered table-hover">
                    
                    <tr>
                        <td >Bank 
                            Code</td>
                        <td >
                            <input class="form-control" runat="server" id="txt_bankcode" 
                                onkeypress="AllowOnlyText3();"  /></td>
                        <td >Bank Name</td>
                        <td > 
                            <input class="form-control" runat="server" id="txt_bankname" 
                                   /></td>
                        <td >Branch Name</td>
                        <td >
                            <input class="form-control" id="txt_branchname" type="text" runat="server"    
                                 /></td>
                    </tr>
                    <tr>
                        <td >A/c No</td>
                        <td >
                            <input class="form-control" runat="server" id="txt_actype" onkeypress="return isNumber(event)"/></td>
                        <td >MICR Code
                        
                            </td>
                        <td >
                            <input class="form-control" runat="server" id="txt_micrcode" 
                                onkeypress="AllowOnlyText4();" /></td>
                        <td >IFSC Code
                        
                            </td>
                        <td >
                            <input class="form-control" runat="server" id="txt_ifsccode"  
                                /></td>
                    </tr>
                    <tr>
                        <td>Address </td>
                        <td >
                            <asp:TextBox class="form-control" ID="txt_address" runat="server" Height="55px" TextMode="MultiLine" 
                                Width="100%" Font-Names="Calibri" ForeColor="#666666" Font-Size="Small"></asp:TextBox>
                            <span class="style89"></span></td>
                        <td >Other Info</td>
                        <td >
                            <asp:TextBox class="form-control" ID="txt_otherinfo" runat="server" Height="55px" 
                                TextMode="MultiLine" Width="100%" Font-Names="Calibri" 
                                ForeColor="#666666" Font-Size="Small"></asp:TextBox>
                        </td>
                        <td colspan="2"></td>
                    </tr>

                    <tr>
                    <td colspan="2">                   
                       <asp:Button ID="btn_Back" runat="server" OnClick="btn_Back_Click" Text="Back" class="btn btn-info"  />
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" class="btn btn-success" onclick="btnUpdate_Click" 
                           /></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    
                    
                    </tr>
                           
                    </table>
               </div> 
               <br />
</asp:Content>

