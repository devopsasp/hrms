<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="NewBranch.aspx.cs" Inherits="Hrms_Company_Default2" Title="Welcome to HRMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>
  <script language="javascript" type="text/javascript">
    
  function Number_txtbox(event,txtid)
  {  
       var txtlen;
       var txtvalue; 
       var bool_obj; 
       var count;  
       var str;           
          
       txtvalue= document.getElementById(txtid).value;
       txtlen=txtvalue.length; 
       count=txtlen-1;
       
  if(event.keyCode!=8 && event.keyCode!=46 && event.keyCode!=35 && event.keyCode!=36 && event.keyCode!=37 && event.keyCode!=38 && event.keyCode!=39 && event.keyCode!=40)     
   {    
  
        str=txtvalue.charAt(count);
        
        if(str<=9 && str>=0)
                {
                document.getElementById(txtid).value=txtvalue;
                }
                else
                {
               document.getElementById(txtid).value= txtvalue.substring(0,count);   
                }      
    }
  }
    
    function show_message(msg)
    {
        alert(msg);
    }
       
    function clearAll()
    {
    
  document.aspnetForm.ctl00$ContentPlaceHolder1$txtBranchCode.value="";  
  document.aspnetForm.ctl00$ContentPlaceHolder1$txtBranchName.value="";  
  document.aspnetForm.ctl00$ContentPlaceHolder1$txtAddressLine1.value="";  
  document.aspnetForm.ctl00$ContentPlaceHolder1$txtAddressLine2.value="";  
  document.aspnetForm.ctl00$ContentPlaceHolder1$txtCity.value="";  
  document.aspnetForm.ctl00$ContentPlaceHolder1$txtZipCode.value="";  
  document.aspnetForm.ctl00$ContentPlaceHolder1$txtCountry.value="";  
  document.aspnetForm.ctl00$ContentPlaceHolder1$txtState.value="";  
  document.aspnetForm.ctl00$ContentPlaceHolder1$txtPhoneNo.value="";  
  document.aspnetForm.ctl00$ContentPlaceHolder1$txtFaxNo.value="";  
  document.aspnetForm.ctl00$ContentPlaceHolder1$txtEmailId.value="";  
 document.aspnetForm.ctl00$ContentPlaceHolder1$txtAlternateEmailId.value="";  
 document.aspnetForm.ctl00$ContentPlaceHolder1$txtUserID.value="";  
 document.aspnetForm.ctl00$ContentPlaceHolder1$txtPassword.value="";  
 document.aspnetForm.ctl00$ContentPlaceHolder1$txtConfirmpwd.value="";  
    
    }
    
    
    function empty_uid()
    {
    
    if(document.aspnetForm.ctl00$ContentPlaceHolder1$txtUserID.value!="")
    {
    return true;    
    }  
    else
    {
    
    alert("Enter UserID"); 
    return false;
    }  
    
    }
    
    
    
 </script>
 
 
<%--onkeyup="keyup(event,this.id);" --%>
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

                            <div align="center" id="morris-area-chart" style="width: 100%">
                
                               <table class="table table-striped table-bordered table-hover">
                                    <tbody>
                                        <tr>
                                            <td align="center">
                                                <asp:RegularExpressionValidator ID="txtmultiline_validator" runat="server" 
                                                    ControlToValidate="txtAddressLine1" 
                                                    Text="Permenent address exceeding 100 characters" 
                                                    ValidationExpression="^[\s\S]{0,99}$" />
                                                <asp:RegularExpressionValidator ID="txtmultiline_validator1" runat="server" 
                                                    ControlToValidate="txtAddressLine2" 
                                                    Text="Temporary address exceeding 100 characters" 
                                                    ValidationExpression="^[\s\S]{0,49}$" />
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                                    ControlToCompare="txtPassword" ControlToValidate="txtConfirmpwd" 
                                                    ErrorMessage="Password  and Confirm Password does not match"></asp:CompareValidator>
                                            </td>
                                            <td>
                                                <input id="ToolBarCode" runat="server" 
                                                    style="width: 27px; font-family: Calibri;" type="hidden" value="0" />
                                                <input id="hBranchID" runat="server" 
                                                    style="width: 27px; font-family: Calibri;" type="hidden" value="0" />
                                                *<span style="color: #ff0000; font-family: Calibri;"> Mandatory Fields</span></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" >
                    <table id="tbl_branch" runat="server" class="table table-striped table-bordered table-hover">
                                    
                                    <tr>
                                        <td>
                                            Branch Code *</td>
                                        <td>
                                            <input Class="form-control" runat="server" id="txtBranchCode" maxlength="10" tabindex="1" />
                                        </td>
                                            <td>
                                                Address 1 </td>
                                            <td>
                                                
                                                <asp:TextBox ID="txtAddressLine1" runat="server" Class="form-control" 
                                                    MaxLength="99" TabIndex="11" TextMode="MultiLine"></asp:TextBox>
                                                
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td>
                                            Branch Name *</td>
                                        <td>
                                                <input class=" form-control"  runat="server" id="txtBranchName" maxlength="50" tabindex="2"/>
                                        </td>
                                            <td>
                                                Address 2</td>
                                            <td>
                                                <asp:TextBox ID="txtAddressLine2" runat="server" Class="form-control" MaxLength="99" 
                                                    TabIndex="12" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td>
                                            PF Code</td>
                                        <td>
                                                <input Class="form-control" runat="server" id="txtPFCode" maxlength="20" tabindex="3" />
                                        </td>
                                            <td>
                                                City </td>
                                            <td>
                                                <input Class="form-control" runat="server" id="txtCity" 
                                    onkeypress="AllowOnlyText();" onfocusout="strvalid(this.id, 'String');" maxlength="20" tabindex="13" />
                                        </td>
                                    </tr>


                                    <tr>
                                        <td>
                                            ESI Code</td>
                                        <td>
                                                <input Class="form-control" runat="server" id="txtESICode" maxlength="20" tabindex="4" />
                                        </td>
                                            <td>
                                                Pincode</td>
                                            <td>
                                                <input Class="form-control" runat="server" id="txtZipCode" 
                                    onkeydown="AllowOnlyNumeric1(event);" onfocusout="strvalid(this.id, 'Number');" maxlength="6" tabindex="14" />
                                        </td>
                                    </tr>


                                    <tr>
                                        <td>
                                            Start Date *</td>
                                        <td>
                                                <input Class="form-control" runat="server" id="txtStartDate" maxlength="10" tabindex="5" />
                                        </td>
                                            <td>
                                                State</td>
                                            <td>
                                                <input id="txtState" runat="server" Class="form-control"
                                    onkeypress="AllowOnlyText();"  onfocusout="strvalid(this.id, 'String');" maxlength="20" tabindex="15" />
                                        </td>
                                    </tr>


                                    <tr>
                                        <td>
                                            End Date *</td>
                                        <td>
                                                <input Class="form-control" runat="server" id="txtEndDate" maxlength="10" tabindex="6" />
                                        </td>
                                            <td>
                                                Country</td>
                                            <td>
                                                <input Class="form-control" runat="server" 
                                    id="txtCountry" onkeypress="AllowOnlyText();" onfocusout="strvalid(this.id, 'String');" maxlength="10" tabindex="16" />
                                        </td>
                                    </tr>


                                    <tr>
                                        <td>
                                            Email ID</td>
                                        <td>
                                                <input Class="form-control" runat="server" id="txtEmailId" maxlength="20" tabindex="7" />
                                        </td>
                                            <td>
                                                Phone No</td>
                                            <td>
                                                <input Class="form-control" runat="server" id="txtPhoneNo" onkeydown="AllowOnlyNumeric1(event);" 
                                    onfocusout="strvalid(this.id, 'Number');" maxlength="10" tabindex="17" />
                                        </td>
                                    </tr>


                                    <tr>
                                        <td>
                                            Alternate Email ID</td>
                                        <td>
                                                <input Class="form-control" runat="server" id="txtAlternateEmailId" maxlength="20" tabindex="8" />
                                        </td>
                                            <td>
                                                Fax No</td>
                                            <td>
                                                <input Class="form-control" runat="server" id="txtFaxNo" 
                                    onkeydown="AllowOnlyNumeric1(event);" onfocusout="strvalid(this.id, 'Number');" maxlength="20" tabindex="16" />
                                        </td>
                                    </tr>


                                    <tr>
                                        <td>
                                            User ID *</td>
                                        <td>
                                            <input Class="form-control" runat="server" id="txtUserID" maxlength="20" tabindex="9" />
                                            
                                            <br />
                                            <asp:Button ID="btn_info" runat="server" class="btn btn-info" 
                                                onclick="btn_info_Click" Text="Check Availability" />
                                        </td>
                                        <td>
                                            Password *</td>
                                        <td>
                                            <input Class="form-control" runat="server" 
                            id="txtPassword" type="password" maxlength="20" tabindex="19" />
                                            <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                ControlToValidate="txtPassword" ErrorMessage="Password cannot be blank" 
                                                Font-Size="Small"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td>
                                            Confirm Password *</td>
                                        <td>
                                                <input type="password" runat="server" id="txtConfirmpwd" Class="form-control" maxlength="20" tabindex="10" />
                                        </td>
                                            <td align="center">
                                                &nbsp;</td>
                                            <td align="left">
                                                <span style="font-family: Calibri"><span style="color: #444444">
                                                <span style="color: #6A6A6A">
                                                <asp:Button ID="btn_reset0" runat="server" class="btn btn-warning" 
                                                    onclick="btn_reset_Click" Text="Reset" />
                                                &nbsp;<asp:Button ID="btn_update" runat="server" class="btn btn-success" 
                                                    Text="Modify" onclick="btn_update_Click" TabIndex="21" />
                                                &nbsp;<asp:Button ID="btn_save" runat="server" class="btn btn-success" 
                                                    onclick="btn_save_Click" Text="Save" TabIndex="20" />
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