<%@ Page Title="" Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="EditContactInformation.aspx.cs" Inherits="Hrms_Employee_EditContactInformation" %>

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
    function Number_txtbox(event, txtid) {
        var txtlen;
        var txtvalue;
        var bool_obj;
        var count;
        var str;

        txtvalue = document.getElementById(txtid).value;
        txtlen = txtvalue.length;
        count = txtlen - 1;

        if (event.keyCode != 8 && event.keyCode != 46 && event.keyCode != 35 && event.keyCode != 36 && event.keyCode != 37 && event.keyCode != 38 && event.keyCode != 39 && event.keyCode != 40) {

            str = txtvalue.charAt(count);

            if (str <= 9 && str >= 0) {
                document.getElementById(txtid).value = txtvalue;
            }
            else {
                document.getElementById(txtid).value = txtvalue.substring(0, count);
            }

        }


    }


    function isBlank(s) {
        var len = s.length
        var i
        for (i = 0; i < len; i++) {
            if (s.charAt(i) != " ")
                return false
        }
        return true
    }

    function valid(str, type) {
        var RE;
        switch (type) {
            case "Email":
                RE = /^[a-z][\._a-z0-9-]+@[\.a-z0-9-]+[\.]{1}[a-z]{2,4}$/;
                if (RE.exec(str.value)) {
                    return true;
                } else {
                    return false;
                }
            case "Name":
                RE = /^[a-zA-Z. ]{4,20}$/;
                if (RE.exec(str.value)) {
                    return true;
                } else {
                    return false;
                }

            case "Phone":
                RE = /^[0-9]{8,15}$/;
                if (RE.exec(str.value)) {
                    return true;
                } else {
                    return false;
                }

            default:
                return false;
        }
    }

    function check_update() {

        var msg = "Please make sure the following fields are valid \n\n";
        var key = "";

        if (isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtEmployeeCode.value)) {
            key += " Enter Employee Code \n";
        }



        if (isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtFirstName.value)) {
            key += " Enter Employee First Name  \n";
        }


        if (isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtfullname.value)) {
            key += " Enter Employee Full Name  \n";
        }

        if (isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_Readerid.value)) {
            key += " Enter Reader ID  \n";
        }



        if (!isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtEmailId.value)) {

            if (!valid(document.aspnetForm.ctl00$ContentPlaceHolder1$txtEmailId, "Email")) {
                key += "Invalid Email ID \n";
            }

        }

        if (!isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtAEmailId.value)) {

            if (!valid(document.aspnetForm.ctl00$ContentPlaceHolder1$txtAEmailId, "Email")) {
                key += "Invalid Alternate Email ID \n";
            }
        }       

        if (!isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtOfficeNo.value)) {

            if (!valid(document.aspnetForm.ctl00$ContentPlaceHolder1$txtOfficeNo, "Phone")) {
                key += "Invalid Office Phone No \n";
            }
        }

        if (!isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtRecidenceNo.value)) {

            if (!valid(document.aspnetForm.ctl00$ContentPlaceHolder1$txtRecidenceNo, "Phone")) {
                key += "Invalid Recidence Phone No\n";
            }
        }


        if (!isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtCellNo.value)) {

            if (!valid(document.aspnetForm.ctl00$ContentPlaceHolder1$txtCellNo, "Phone")) {
                key += "Invalid Cell No\n";
            }
        }

        if (!isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtemgno.value)) {

            if (!valid(document.aspnetForm.ctl00$ContentPlaceHolder1$txtemgno, "Phone")) {
                key += "Invalid Emergency Phone No\n";
            }
        }


        if (key != "") {
            alert(msg + key + "\n ******** Unable to Create!! ******** \n");

            return false;
        }
        else {

            return true;

        }
    }

</script>   

<script language=javascript>
    function address_copy() {

        //alert("hai");.checked==true   

        if (document.aspnetForm.ctl00$ContentPlaceHolder1$chk_address.checked == true) {

            document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentHouseNo.value = document.aspnetForm.ctl00$ContentPlaceHolder1$txtPresentHouseNo.value;
            document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentStreetName.value = document.aspnetForm.ctl00$ContentPlaceHolder1$txtPresentStreetName.value;
            document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentAddressLine1.value = document.aspnetForm.ctl00$ContentPlaceHolder1$txtPresentAddressLine1.value;
            document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentAddressLine2.value = document.aspnetForm.ctl00$ContentPlaceHolder1$txtPresentAddressLine2.value;
            document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentCity.value = document.aspnetForm.ctl00$ContentPlaceHolder1$txtPresentCity.value;
            document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentState.value = document.aspnetForm.ctl00$ContentPlaceHolder1$txtPresentState.value;
        }
        else {

            document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentHouseNo.value = "";
            document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentStreetName.value = "";
            document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentAddressLine1.value = "";
            document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentAddressLine2.value = "";
            document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentCity.value = "";
            document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentState.value = "";
        }


    }
</script>


    <h3 class="page-header"> Update Contact Information</h3><br /><br />


                <table id="Table1"  runat="server" width="100%" cellpadding="0" cellspacing="0" class="table table-striped table-bordered table-hover">
                   
                    <tr>
                        <td colspan="6"><h5>Present Address</h5></td>
                       
                    </tr>
                    <tr>
                        <td >House No/Name</td>
                        <td >
                            <input class="form-control" runat="server" id="txtPresentHouseNo" 
                                onkeypress="AllowOnlyTND();" maxlength="20"  /></td>
                        <td >Address Line </td>
                        <td >
                            <input class="form-control" runat="server" id="txtPresentAddressLine1" 
                                onkeypress="AllowOnlyText5();" maxlength="100"  /></td>
                        <td >Address Line 2</td>
                        <td >
                            <input class="form-control" runat="server" id="txtPresentAddressLine2" 
                                onkeypress="AllowOnlyText5();" maxlength="100"  /></td>
                    </tr>
                    <tr>
                        <td >City</td>
                        <td >
                            <input class="form-control" runat="server" id="txtPresentCity" 
                                 maxlength="50"  /></td>
                        <td >State</td>
                        <td >
                            <input class="form-control" runat="server" id="txtPresentState" 
                                  maxlength="50"  /></td>
                        <td >Pin Code</td>
                        <td >
                            <input class="form-control" runat="server" id="txtPresentStreetName" 
                              onkeypress="return isNumber(event)" /></td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <input type="checkbox" id="chk_address" runat="server" 
                                onclick="address_copy();"  />Present and Permanent Address are same</td>
                      
                    </tr>
                    <tr>
                        <td  colspan="6"><h5>Permanent Address</h5></td>
                    </tr>
                    <tr>
                        <td >House No/Name</td>
                        <td>
                            <input class="form-control" runat="server" id="txtPermanentHouseNo" 
                                onkeypress="AllowOnlyTND();"  /></td>
                        <td >Address Line</td>
                        <td >
                            <input class="form-control" runat="server" id="txtPermanentAddressLine1" 
                                onkeypress="AllowOnlyText5();"  /></td>
                        <td >Address Line 2</td>
                        <td >
                            <input class="form-control" runat="server" id="txtPermanentAddressLine2" 
                                onkeypress="AllowOnlyText5();"  /></td>
                    </tr>
                    <tr>
                     <td >City</td>
                        <td>
                            <input class="form-control" runat="server" id="txtPermanentCity" 
                                   /></td>
                        <td >State</td>
                        <td >
                            <input class="form-control" runat="server" id="txtPermanentState" 
                                   /></td>
                       
                        <td >Pin Code</td>
                        <td >
                            <input class="form-control" runat="server" id="txtPermanentStreetName" 
                               onkeypress="return isNumber(event)"  /></td>
                    </tr>
                    <tr>
                        <td  colspan="6"><h5>Contact </h5></td>
                    </tr>
                    <tr>
                        <td >Office no </td>
                        <td >
                            <input id="txtOfficeNo" runat="server" class="form-control" type="text" 
                                onkeyup="Number_txtbox(event,this.id);"   onkeypress="return isNumber(event)"
                                 /></td>
                        <td >Residence</td>
                        <td >
                            <input id="txtRecidenceNo" runat="server" onkeyup="Number_txtbox(event,this.id);"
                                class="form-control" type="text"   onkeypress="return isNumber(event)"
                                 /></td>
                        <td >Mobile</td>
                        <td >
                            <input id="txtCellNo" runat="server" onkeyup="Number_txtbox(event,this.id);" class="form-control"
                                type="text"   onkeypress="return isNumber(event)"  /></td>
                    </tr>
                    <tr>
                        <td >Alt. Office no</td>
                        <td >
                            <input id="txtAltOfficeNo" runat="server" class="form-control" type="text" 
                                onkeyup="Number_txtbox(event,this.id);"   onkeypress="return isNumber(event)"
                                 /></td>
                        <td >Alt. Residence</td>
                        <td >
                            <input id="txtAltRecidenceNo" runat="server" onkeyup="Number_txtbox(event,this.id);"
                                class="form-control" type="text"   onkeypress="return isNumber(event)"
                                 /></td>
                        <td >Alt. Mobile</td>
                        <td >
                            <input id="txtAltCellNo" runat="server" onkeyup="Number_txtbox(event,this.id);" class="form-control"
                                type="text"   onkeypress="return isNumber(event)"
                                 /></td>
                    </tr>
                    <tr>
                        <td >Email Id</td>
                        <td >
                            <input class="form-control" runat="server" id="txtEmailId" 
                                onfocusout="AllowforEmail(this.id);"  /></td>
                        <td >Alt. Email </td>
                        <td >
                            <input class="form-control" runat="server" id="txtAEmailId" 
                                onfocusout="AllowforEmail(this.id);"  /></td>
                        <td >Fax</td>
                        <td >
                            <input id="txtFaxNo" runat="server" class="form-control" type="text" 
                                onkeydown="AllowOnlyNumeric1(event);"  /></td>
                    </tr>
                    <tr>
                        <td  colspan="6"><h5>Emergency Contact Details</h5></td>
                    </tr>
                    <tr>
                        <td > Name</td>
                        <td >
                            <input class="form-control" runat="server" id="txtemgname" 
                                   /></td>
                        <td >Number</td>
                        <td >
                            <input class="form-control" runat="server" id="txtemgno" 
                                onkeyup="Number_txtbox(event,this.id);"   onkeypress="return isNumber(event)"
                                 /></td>
                        <td >Address</td>
                        <td >
                            <asp:TextBox class="form-control" ID="txtemgaddress" runat="server" Height="55px" TextMode="MultiLine" 
                                Width="128px" Font-Names="Calibri" ForeColor="#666666" Font-Size="Small"></asp:TextBox>
                            
                                                        </td>
                    </tr>

                    <tr>
                    <td colspan="2">                   
                       <asp:Button ID="btn_Back" runat="server" OnClick="btn_Back_Click" Text="Back" class="btn btn-info"  />
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" class="btn btn-success" onclick="btnUpdate_Click" OnClientClick="return check_update();"
                             /></td>
                    
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>                    
                    </tr>
                </table>
            
                <br />

</asp:Content>

