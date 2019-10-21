<%@ Page Title="" Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" 
CodeFile="EditFamilyDetails.aspx.cs" Inherits="Hrms_Employee_EditFamilyDetails" %>

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
<div>
<div class="panel-heading">
 <h4 class="panel-title">
 Update Family Details
 </h4>
</div>  


<table id="Table1" runat="server"  class="table table-striped table-bordered table-hover">
                    <tr>
                        <td class="dComposePreviewVal" >
                        </td>
                        <td >
                        </td>
                        <td class="dComposeItemLabel"  style="width: 92px">
                        </td>
                        <td >
                        </td>
                          <td >
                        </td>
                          <td >
                        </td>
                    </tr>
                    <tr>
                    <td >Father's Name
                            </td>
                        <td >
                            <input class="form-control" runat="server" id="Text1" 
                                   maxlength="30"  /></td>
                        <td >Mother&#39;s Name</td>
                        <td >
                            <input class="form-control" runat="server" id="txt_mother" 
                                   maxlength="30"  /></td>
                        <td >
                            Spouse&#39;s Name</td>
                        <td >
                            <input class="form-control" runat="server" id="txt_Spouse" 
                                   maxlength="30"  /></td>
                       
                    </tr>

                      <tr>
                       <td >No.Of Children
                            </td>
                        <td >
                            <input class="form-control" runat="server" id="txt_child" 
                               onkeypress="return isNumber(event)" maxlength="3"/></td>
                    <td colspan="2">                   
                       <asp:Button ID="Button2" runat="server" OnClick="btn_Back_Click" Text="Back" class="btn btn-info"  />
                        <asp:Button ID="btnUpdate_Familydetails" runat="server" Text="Update" 
                            class="btn btn-success" onclick="btnUpdate_Familydetails_Click"  
                             /></td>
                    <td></td>
                    <td></td>
                   
                                   
                    </tr>
                </table>   
                                               
  
                </div>  <br />
                                                 
</asp:Content>

