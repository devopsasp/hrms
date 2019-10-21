<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Employee_Profile.aspx.cs" Inherits="Hrms_Employee_Default2" Title="Welcome to HRMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css" media="screen">
        @import "tabs.css";
    </style>
    <style type="text/css" media="screen">
        @import "basic.css";

        .style40 {
            font-family: Calibri;
            font-size: x-small;
            color: #FF0000;
        }

        #contbl {
            height: 64px;
        }

        #reftbl {
            height: 206px;
        }

        .style78 {
            height: 17px;
        }

        .style89 {
            font-family: Calibri;
            font-size: x-small;
        }

        .style90 {
            font-size: x-small;
        }

        #Text1 {
            height: 19px;
            width: 125px;
        }
        /*.redonly{
            pointer-events: none;
        }*/
    </style>
    <script src="../js/jquery-1.9.1.min.js"></script>
    <script src="../JQuery/jquery-ui.js"></script>
    <script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>

    <script src="Employee.js"></script>
    <script language="javascript" type="text/javascript">


        function fn_Visible() {
            var parm = document.getElementById('<%= ddl_idtype.ClientID %>');
         if (parm.options[parm.selectedIndex].text == 'Others') {
             document.getElementById('<%= txt_otherid.ClientID %>').disabled = false;
         }
         else {
             document.getElementById('<%= txt_otherid.ClientID %>').disabled = true;
         }

     }

     function show_message(msg) {
         alert(msg);
     }

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



     function fn_date(event, txtid) {
         var len;
         var txtvalue;
         var bool_obj;
         var i;



         txtvalue = document.getElementById(txtid).value;
         txtlen = txtvalue.length;

         if (event.keyCode != 8 && event.keyCode != 46 && event.keyCode != 35 && event.keyCode != 36 && event.keyCode != 37 && event.keyCode != 38 && event.keyCode != 39 && event.keyCode != 40) {

             if (txtlen != 0) {
                 bool_obj = fn_validate(txtlen, txtvalue);

                 if (bool_obj == true) {
                     if (txtlen == 2 || txtlen == 5) {
                         document.getElementById(txtid).value = txtvalue + "/";
                     }
                     else {
                         document.getElementById(txtid).value = txtvalue;
                     }
                 }
                 else {
                     document.getElementById(txtid).value = txtvalue.substring(0, txtlen - 1);
                 }
             }
         }
     }

     function fn_validate(len, tval) {
         var str;

         switch (len) {

             case 1: if (tval <= 3) {
                 return true;
             }
             else {
                 return false;
             }
                 break;


             case 2:

                 if (tval <= 31 && tval > 0) {
                     return true;
                 }
                 else {
                     return false;
                 }
                 break;


             case 3: str = tval.charAt(2);

                 if (str == "/") {
                     return true;
                 }
                 else {
                     return false;
                 }
                 break;

             case 4: str = tval.charAt(3);

                 if (str <= 1) {
                     return true;
                 }
                 else {
                     return false;
                 }
                 break;

             case 5: str = tval.substring(3, 5);

                 if (str <= 12 && str > 0) {
                     return true;
                 }
                 else {
                     return false;
                 }
                 break;

             case 6: str = tval.charAt(5);

                 if (str == "/") {
                     return true;
                 }
                 else {
                     return false;
                 }
                 break;

             case 7: str = tval.charAt(6);

                 if (str <= 9 && str > 0) {
                     return true;
                 }
                 else {
                     return false;
                 }
                 break;

             case 8: str = tval.substring(6, 8);

                 if (str >= 18) {
                     return true;
                 }
                 else {
                     return false;
                 }
                 break;

             case 9: str = tval.charAt(8);

                 if (str <= 9) {
                     return true;
                 }
                 else {
                     return false;
                 }
                 break;

             case 10: str = tval.charAt(9);

                 if (str <= 9) {
                     return true;
                 }
                 else {
                     return false;
                 }
                 break;


             default: return false;
                 break;
         }
     }
    </script>

    <script language="javascript">

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

        function check() {
  
            if (isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtEmployeeCode.value)) {
                alert("Enter Employee Code "); return false;
            }          
            else if (isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtepwd.value)) {
                alert(" Enter Password "); return false;
            }
            else if (document.aspnetForm.ctl00$ContentPlaceHolder1$txtecpwd.value != document.aspnetForm.ctl00$ContentPlaceHolder1$txtepwd.value) {
                alert("Passwords are not same \n"); return false;
            }
            //else if (document.aspnetForm.ctl00_ContentPlaceHolder1_Rolelode.value ==0) {
            //    alert(" Select Role "); return false;
            //}
            else if (isBlank(document.aspnetForm.ctl00_ContentPlaceHolder1_txt_Readerid.value)) {
                alert(" Enter Reader ID  "); return false;
            }
            else if (isBlank(document.aspnetForm.ctl00_ContentPlaceHolder1_txt_basicsal.value)) {
                alert(" Enter Basic / Gross Salary  "); return false;
            }
            if (isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtFirstName.value)) {
                alert(" Enter Employee First Name  \n"); return false;

            }

            else if (isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtfullname.value)) {
                alert(" Enter Employee Full Name  \n");
                return false;
            }
           else if (isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_Readerid.value)) {
               alert(" Enter Readerid  \n");
               return false;
            }
            else if (isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_basicsal.value)) {
                alert(" Enter Basic Salary  \n");
                return false;
            }
            else if (isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtEmailId.value))
            {
                alert("Invalid Email ID \n");
                return false;
            }
                 
            else if (isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtCellNo.value)) {
                alert("Invalid Cell No\n");
                return false;
                }  
            else {
                return true;
            }
       
        }

        function check_update() {

            var msg = "Please make sure the following fields are valid \n\n";
            var key = "";

            if (isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtEmployeeCode.value)) {
                key += " Enter Employee Code \n";
            }


            //	if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtepwd.value))
            //	   {	
            //	   key+=" Enter Password  \n";
            //	   }
            //		
            //		if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtecpwd.value))
            //	   {	
            //	   key+=" Enter ConfirmPassword  \n";
            //	   }
            //	  

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

            //

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
                //alert("Successful");		
                //document.aspnetForm.ctl00$ContentPlaceHolder1$ToolBarCode.value=1		
                //document.aspnetForm.submit();

                return true;

            }

        }
    </script>

    <%--       <script type="text/javascript">
           $(document).ready(function () {
               alert('a');
               $('input[type=text]').addClass('form-control');
           });
</script>--%>

 
    
        <div aria-autocomplete="none">
            <div>
                <h3 class="page-header">Employee Details</h3>
            </div>
            <div class="panel panel-default">

                <!-- /.panel-heading -->
                <div class="panel-body">
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs">
                        <li class="active"><a href="#register"  id="navReg" data-toggle="tab">Registration </a>
                        </li>
                        <li><a  id="navPer" class="redonly"  href="#personal" data-toggle="tab">Personal </a>
                        </li>
                        <li><a   id="navCon" class="redonly" href="#contact" data-toggle="tab">Contact </a>
                        </li>
                        <li><a  id="navRef" class="redonly" href="#reference" data-toggle="tab">Past Work Experience</a>
                        </li>
                        <li><a  id="navBank"  class="redonly" href="#bank" data-toggle="tab">Bank </a>
                        </li>
                        <li><a  id="navOff" class="redonly" href="#office" data-toggle="tab">Others </a>
                        </li>
                    </ul>
                    <div>
                        <asp:Label ID="lbl_Error" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                    </div>
                    <!-- Tab panes -->

                    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                    </asp:ToolkitScriptManager>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr valign="top">
                            <td class="tdComposeHeader" valign="top" align="right">

                       <div class="tab-content">
                                    <div class="tab-pane fade" id="personal">
                                        <table cellspacing="0" cellpadding="0" runat="server" border="0" width="100%" class="table table-striped table-bordered table-hover">

                                            <tr>
                                                <td>Salutation</td>
                                                <td>
                                                    <asp:RadioButtonList ID="rdo_salutation" runat="server"
                                                        RepeatDirection="Horizontal" RepeatLayout="Flow" Width="100%">
                                                        <asp:ListItem Selected="True" Value="1" Text="Mr."></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Ms."></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="Mrs."></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>Full Name<span class="style40">*</span></td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txtfullname"
                                                        onkeypress="AllowOnlyText3();" maxlength="70" /></td>
                                                <td>Blood Group<span class="style40">*</span></td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_blood"
                                                        runat="server" CssClass="form-control" ForeColor="#666666"
                                                        Font-Size="Small">
                                                        <asp:ListItem Text="None" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="A+" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="A-" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="B+" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="B-" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="AB+" Value="5"></asp:ListItem>
                                                        <asp:ListItem Text="AB-" Value="6"></asp:ListItem>
                                                        <asp:ListItem Text="O+" Value="7"></asp:ListItem>
                                                        <asp:ListItem Text="O-" Value="8"></asp:ListItem>
                                                        <asp:ListItem Text="A1+" Value="9"></asp:ListItem>
                                                        <asp:ListItem Text="A1-" Value="10"></asp:ListItem>
                                                        <asp:ListItem Text="A2+" Value="11"></asp:ListItem>
                                                        <asp:ListItem Text="A2-" Value="12"></asp:ListItem>
                                                        <asp:ListItem Text="A1B+" Value="13"></asp:ListItem>
                                                        <asp:ListItem Text="A1B-" Value="14"></asp:ListItem>
                                                        <asp:ListItem Text="A2B+" Value="15"></asp:ListItem>
                                                        <asp:ListItem Text="A2B-" Value="16"></asp:ListItem>
                                                        <asp:ListItem Text="Others" Value="17"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>First Name<span class="style40">*</span></td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txtFirstName"
                                                        onkeypress="AllowOnlyText3();" maxlength="50" /></td>
                                                <td>Middle Name
                           
                                                </td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txtMiddleName"
                                                        onkeypress="AllowOnlyText3();" maxlength="50" /></td>
                                                <td>Last Name
                           
                                                </td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txtLastName"
                                                        onkeypress="AllowOnlyText3();" maxlength="50" /></td>
                                            </tr>
                                            <tr>
                                                <td>Date of Birth<span class="style40">*</span></td>
                                                <td>
                                                    <div style="width: 150px; float: left;">
                                                        <asp:TextBox CssClass="form-control" ID="txt_dob" runat="server"
                                                            onkeyup="fn_date(event,this.id);" Width="150px"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txt_dob"
                                                            runat="server" Format="dd/MM/yyyy" TodaysDateFormat="d MMMM, yyyy" />
                                                    </div>
                                                    <div style="width: 25px; float: left; margin-left: 10px; margin-top: 3px;">
                                                        <asp:Image ID="Image1" runat="server"
                                                            Text="" Width="25px"
                                                            ImageUrl="~/Images/calendaricon.png" />
                                                    </div>
                                                </td>
                                                <td>Gender
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_gender" runat="server" CssClass="form-control"
                                                        ForeColor="#666666" Font-Size="Small">
                                                        <asp:ListItem Value="1" Text="Male"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Female"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="Others"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>Marital Status
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_marital" runat="server" CssClass="form-control"
                                                        ForeColor="#666666" Font-Size="Small">
                                                        <asp:ListItem Value="1" Text="Single"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Married"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Religion
                                                </td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txtReligion"
                                                        onkeypress="AllowOnlyText();" maxlength="50" /></td>
                                                <td>Nationality
                                                </td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txtNationality"
                                                        onkeypress="AllowOnlyText();" maxlength="50" /></td>
                                                <td>Father's Name
                                                </td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txt_father"
                                                        onkeypress="AllowOnlyText3();" maxlength="30" /></td>
                                            </tr>
                                            <tr>
                                                <td>Mother&#39;s Name</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txt_mother"
                                                        onkeypress="AllowOnlyText3();" maxlength="30" /></td>
                                                <td>Spouse&#39;s Name</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txt_Spouse"
                                                        onkeypress="AllowOnlyText3();" maxlength="30" /></td>
                                                <td>No.Of Children
                                                </td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txt_child"
                                                        onkeydown="AllowOnlyNumeric1(event);" maxlength="3" /></td>
                                            </tr>
                                            <tr>
                                                <td>ID Proof Type</td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_idtype" runat="server" CssClass="form-control" onchange="fn_Visible()"
                                                        ForeColor="#666666" Font-Size="Small">
                                                        <asp:ListItem Value="1" Text="Select"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Driving License"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="Passport"></asp:ListItem>
                                                        <asp:ListItem Value="4" Text="Pan Card"></asp:ListItem>
                                                        <asp:ListItem Value="5" Text="Voter ID Card"></asp:ListItem>
                                                        <asp:ListItem Value="6" Text="Govt. Issued ID Card"></asp:ListItem>
                                                        <asp:ListItem Value="7" Text="Bank Passbook"></asp:ListItem>
                                                        <asp:ListItem Value="8" Text="Credit Card"></asp:ListItem>
                                                        <asp:ListItem Value="9" Text="Aadhar Card"></asp:ListItem>
                                                        <asp:ListItem Value="10" Text="Others"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>Others (Specify) </td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txt_otherid"
                                                        onkeypress="AllowOnlyText3();" maxlength="30"
                                                        disabled="disabled" /></td>
                                                <td>ID Card No</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txt_idno" maxlength="30" /></td>
                                            </tr>
                                        </table>
                                    </div>

                          
                                    <div class="tab-pane fade" id="contact">
                                        <table runat="server" width="100%" cellpadding="0" cellspacing="0" class="table table-striped table-bordered table-hover">

                                            <tr>
                                                <td colspan="6">
                                                    <h5>Present Address</h5>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td>House No/Name</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txtPresentHouseNo"
                                                        onkeypress="AllowOnlyTND();" maxlength="20" /></td>
                                                <td>Address Line </td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txtPresentAddressLine1"
                                                        onkeypress="AllowOnlyText5();" maxlength="100" /></td>
                                                <td>Address Line 2</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txtPresentAddressLine2"
                                                        onkeypress="AllowOnlyText5();" maxlength="100" /></td>
                                            </tr>
                                            <tr>
                                                <td>City</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txtPresentCity"
                                                        onkeypress="AllowOnlyText();" maxlength="50" /></td>
                                                <td>State</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txtPresentState"
                                                        onkeypress="AllowOnlyText();" maxlength="50" /></td>
                                                <td>Pin Code</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txtPresentStreetName"
                                                        onkeydown="AllowOnlyNumeric1(event);" /></td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <input type="checkbox" id="chk_address" runat="server"
                                                        onclick="address_copy();" />Present and Permanent Address are same</td>

                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <h5>Permanent Address</h5>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>House No</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txtPermanentHouseNo"
                                                        onkeypress="AllowOnlyTND();" /></td>
                                                <td>Address Line</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txtPermanentAddressLine1"
                                                        onkeypress="AllowOnlyText5();" /></td>
                                                <td>Address Line 2</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txtPermanentAddressLine2"
                                                        onkeypress="AllowOnlyText5();" /></td>
                                            </tr>
                                            <tr>
                                                <td>State</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txtPermanentState"
                                                        onkeypress="AllowOnlyText();" /></td>
                                                <td>City</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txtPermanentCity"
                                                        onkeypress="AllowOnlyText();" /></td>
                                                <td>Pin Code</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txtPermanentStreetName"
                                                        onkeydown="AllowOnlyNumeric1(event);" /></td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <h5>Contact </h5>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Office no </td>
                                                <td>
                                                    <input id="txtOfficeNo" runat="server" class="form-control" type="text"
                                                        onkeyup="Number_txtbox(event,this.id);" onkeydown="AllowOnlyNumeric1(event);" /></td>
                                                <td>Residence</td>
                                                <td>
                                                    <input id="txtRecidenceNo" runat="server" onkeyup="Number_txtbox(event,this.id);"
                                                        class="form-control" type="text" onkeydown="AllowOnlyNumeric1(event);" /></td>
                                                <td>Mobile<span style="color: Red;"> *</span></td>
                                                <td>
                                                    <input id="txtCellNo" runat="server" onkeyup="Number_txtbox(event,this.id);" class="form-control"
                                                        type="text" onkeydown="AllowOnlyNumeric1(event);" /></td>
                                            </tr>
                                            <tr>
                                                <td>Alt. Office no</td>
                                                <td>
                                                    <input id="txtAltOfficeNo" runat="server" class="form-control" type="text"
                                                        onkeyup="Number_txtbox(event,this.id);" onkeydown="AllowOnlyNumeric1(event);" /></td>
                                                <td>Alt. Residence</td>
                                                <td>
                                                    <input id="txtAltRecidenceNo" runat="server" onkeyup="Number_txtbox(event,this.id);"
                                                        class="form-control" type="text" onkeydown="AllowOnlyNumeric1(event);" /></td>
                                                <td>Alt. Mobile</td>
                                                <td>
                                                    <input id="txtAltCellNo" runat="server" onkeyup="Number_txtbox(event,this.id);" class="form-control"
                                                        type="text" onkeydown="AllowOnlyNumeric1(event);" /></td>
                                            </tr>
                                            <tr>
                                                <td>Email Id<span style="color: Red;"> *</span></td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txtEmailId"
                                                        onfocusout="AllowforEmail(this.id);" /></td>
                                                <td>Alt. Email </td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txtAEmailId"
                                                        onfocusout="AllowforEmail(this.id);" /></td>
                                                <td>Fax</td>
                                                <td>
                                                    <input id="txtFaxNo" runat="server" class="form-control" type="text"
                                                        onkeydown="AllowOnlyNumeric1(event);" /></td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <h5>Emergency Contact Details</h5>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Name</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txtemgname"
                                                        onkeypress="AllowOnlyText3();" /></td>
                                                <td>Number</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txtemgno"
                                                        onkeyup="Number_txtbox(event,this.id);" onkeydown="AllowOnlyNumeric1(event);" /></td>
                                                <td>Address</td>
                                                <td>
                                                    <asp:TextBox class="form-control" ID="txtemgaddress" runat="server" Height="55px" TextMode="MultiLine"
                                                        Width="128px" Font-Names="Calibri" ForeColor="#666666" Font-Size="Small"></asp:TextBox>

                                                </td>
                                            </tr>
                                        </table>
                                    </div>


                 
                                    <div class="tab-pane fade" id="reference">
                                        <table runat="server" width="100%" cellpadding="0" cellspacing="0" class="table table-striped table-bordered table-hover">

                                            <tr>
                                                <td colspan="4">
                                                    <h5>Work 
                            Details</h5>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Training Attended</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txt_training_attend"
                                                        onkeypress="AllowOnlyText3();" /></td>
                                                <td>Training Duration</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txt_training_duration" /></td>
                                            </tr>
                                            <tr>
                                                <td>Position Held</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txt_position"
                                                        onkeypress="AllowOnlyText3();" /></td>
                                                <td>Salary</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txt_salary" onkeydown="AllowOnlyNumeric1(event);" /></td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <h5>Reference 1</h5>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Person Name</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txt_ref1_name"
                                                        onkeypress="AllowOnlyText3();" maxlength="30" /></td>
                                                <td>Relationship</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txt_ref1_relation"
                                                        onkeypress="AllowOnlyText3();" maxlength="50" /></td>
                                            </tr>
                                            <tr>
                                                <td>Contact Phone No.</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txt_ref1_phno"
                                                        onkeydown="AllowOnlyNumeric1(event);" maxlength="20" /></td>
                                                <td>Contact Email_ID</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txt_ref1_email"
                                                        onfocusout="AllowforEmail(this.id);" maxlength="50" /></td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <h5>Reference 2</h5>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Person Name</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txt_ref2_name"
                                                        onkeypress="AllowOnlyText3();" maxlength="30" /></td>
                                                <td>Relationship</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txt_ref2_relation"
                                                        onkeypress="AllowOnlyText3();" maxlength="50" /></td>
                                            </tr>
                                            <tr>
                                                <td>Contact Phone No.</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txt_ref2_phno"
                                                        onkeydown="AllowOnlyNumeric1(event);" maxlength="30" /></td>
                                                <td>Contact Email_ID</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txt_ref2_email"
                                                        onfocusout="AllowforEmail(this.id);" maxlength="50" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>

                                    <div class="tab-pane fade" id="bank">


                                        <table cellspacing="0" cellpadding="0" runat="server" border="0" width="100%" class="table table-striped table-bordered table-hover">


                                            <tr>
                                                <td>Bank Code</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txt_bankcode" onkeypress="AllowOnlyText3();" />

                                                </td>
                                                <td>Bank Name</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txt_bankname"
                                                        onkeypress="AllowOnlyText3();" /></td>
                                                <td>Branch Name</td>
                                                <td>
                                                    <input class="form-control" id="txt_branchname" type="text" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td>A/c No</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txt_actype" /></td>
                                                <td>MICR Code
                        
                                                </td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txt_micrcode"
                                                        onkeypress="AllowOnlyText4();" /></td>
                                                <td>IFSC Code
                        
                                                </td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txt_ifsccode" /></td>
                                            </tr>
                                            <tr>
                                                <td>Address </td>
                                                <td>
                                                    <asp:TextBox class="form-control" ID="txt_address" runat="server" Height="55px" TextMode="MultiLine"
                                                        Width="100%" Font-Names="Calibri" ForeColor="#666666" Font-Size="Small"></asp:TextBox>
                                                    <span class="style89"></span></td>
                                                <td>Other Info</td>
                                                <td>
                                                    <asp:TextBox class="form-control" ID="txt_otherinfo" runat="server" Height="55px"
                                                        TextMode="MultiLine" Width="100%" Font-Names="Calibri"
                                                        ForeColor="#666666" Font-Size="Small"></asp:TextBox>
                                                </td>
                                                <td colspan="2"></td>
                                            </tr>

                                        </table>
                                    </div>

                                    <div class="tab-pane fade in active" id="register">

                                        <table runat="server" width="100%" cellpadding="0" cellspacing="0" class="table table-striped table-bordered table-hover">

                                            <tr>
                                                <td>Employee Code<span style="color: Red;"> *</span> </td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txtEmployeeCode"
                                                        onkeypress="AllowOnlyText2();" tabindex="1" style="width: 210px;" />

                                                    <asp:Button ID="btn_avail" runat="server" class="btn btn-info"
                                                        Text="Check Availability" OnClick="btn_avail_Click1" />
                                                </td>

                                                <%--<td>Designation<span style="color: Red;">*</span></td>
                                                <td>
                                                  
                                                      <asp:DropDownList ID="Rolelode" runat="server" class="form-control" style="width: 180px;"  >
                                                          <asp:ListItem Value="0">-----Select-----</asp:ListItem>
                                                    </asp:DropDownList>  
                                                   
                                                </td>--%>


                                                <td colspan="2">EDLI Eligible</td>
                                                <td>
                                                    <asp:RadioButtonList ID="rdo_tds" runat="server" RepeatDirection="Horizontal"
                                                        RepeatLayout="Flow" Width="100%"
                                                        Style="color: #808080; font-size: 12px; font-weight: 700; font-family: Tahoma"
                                                        TabIndex="6">
                                                        <asp:ListItem Value="1">Y</asp:ListItem>
                                                        <asp:ListItem Value="0" Selected="True">N</asp:ListItem>
                                                    </asp:RadioButtonList></td>
                                            </tr>
                                            <tr runat="server" id="row_pwd">
                                                <td nowrap="nowrap">Employee Password <span style="color: Red;">*</span><span style="color: #ff0000"
                                                    class="style90">&nbsp; </span></td>
                                                <td>
                                                    <input type="password" class="form-control" runat="server" id="txtepwd" tabindex="2" /></td>
                                                <td>PF/ UAN No</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txt_pfno"
                                                        onkeypress="AllowOnlyText4();" tabindex="7" /></td>
                                                <td>OT Eligible</td>
                                                <td>
                                                    <asp:RadioButtonList ID="rdo_btn" runat="server" RepeatDirection="Horizontal"
                                                        RepeatLayout="Flow" AutoPostBack="True" Width="100%"
                                                        OnSelectedIndexChanged="rdo_btn_SelectedIndexChanged"
                                                        Style="color: #808080; font-size: 12px; font-weight: 700; font-family: Tahoma"
                                                        TabIndex="10">
                                                        <asp:ListItem Value="1">Y</asp:ListItem>
                                                        <asp:ListItem Value="0" Selected="True">N</asp:ListItem>

                                                    </asp:RadioButtonList>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbl_text" runat="server" Text="OT Calc" Visible="False"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddl_ot" runat="server" Visible="false">
                                                                    <asp:ListItem Value="0">0</asp:ListItem>
                                                                    <asp:ListItem Value="1">1</asp:ListItem>
                                                                    <asp:ListItem Value="2">1.5</asp:ListItem>
                                                                    <asp:ListItem Value="3">2</asp:ListItem>
                                                                    <asp:ListItem Value="4">2.5</asp:ListItem>
                                                                    <asp:ListItem Value="5">3</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>

                                            </tr>
                                            <tr runat="server" id="row_cpwd">
                                                <td nowrap="nowrap">Confirm Password <span style="color: Red;">*</span></td>
                                                <td>
                                                    <input type="password" class="form-control" runat="server" id="txtecpwd" tabindex="3" /></td>
                                                <td>ESI No
                                                </td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txt_esino"
                                                        onkeypress="AllowOnlyText4();" tabindex="8" /></td>
                                                <td>Reader ID <span style="color: Red;">*</span></td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txt_Readerid"
                                                        onkeydown="AllowOnlyNumeric1(event);" tabindex="11" /></td>
                                            </tr>
                                            <tr>
                                                <td nowrap="nowrap">Reporting Person
                                                </td>
                                                <td>
                                                    <asp:DropDownList id="txt_rep" class="form-control" runat="server"
                                                        type="text" tabindex="4" > </asp:DropDownList></td>
                                                <td>PAN No</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="txt_panno"
                                                        onkeypress="AllowOnlyText4();" tabindex="9" /></td>
                                                <td>Basic / Gross Salary <span style="color: Red;">*</span></td>
                                                <td>
                                                    <input id="txt_basicsal" runat="server" class="form-control" type="text"
                                                        tabindex="12" /></td>
                                            </tr>
                                            <tr>
                                                <td nowrap="nowrap">Salary Type</td>
                                                <td>
                                                    <asp:RadioButtonList ID="rdo_saltype" runat="server" Font-Names="Calibri"
                                                        Font-Size="Small" ForeColor="#666666" RepeatDirection="Horizontal"
                                                        Width="100%" TabIndex="5">
                                                        <asp:ListItem Selected="True">Month</asp:ListItem>
                                                        <asp:ListItem>Day</asp:ListItem>
                                                    </asp:RadioButtonList>

                                                </td>
                                                <td colspan="4"></td>
                                            </tr>
                                        </table>
                                    </div>

                                    <%--<table id="tbl_all" runat="server" width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="35px" class="border">
                            <span class="Title">&nbsp;&nbsp;<img src="../Images/rp_arrow.gif" />&nbsp;<span 
                                class="style82">Allowance / Deduction</span></span></td>
                    </tr>
                </table>--%>

                                    <div class="tab-pane fade" id="office">
                                        <table cellspacing="0" cellpadding="0" runat="server" border="0" width="100%" class="table table-striped table-bordered table-hover">
                                            <tr>
                                                <td colspan="6">
                                                    <h5>Transport Details</h5>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>Boarding Area</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="Txt_area"
                                                        onkeypress="AllowOnlyText();" /></td>
                                                <td>Bus Number</td>
                                                <td>
                                                    <asp:TextBox ID="txt_vehicle" runat="server" CssClass="form-control">
                                                    </asp:TextBox>
                                                </td>
                                                <td>Vehicle Number</td>
                                                <td>
                                                    <input class="form-control" id="Txt_veh_number" type="text" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td>Boarding Point</td>
                                                <td>
                                                    <input class="form-control" runat="server" id="Txt_point"
                                                        onkeypress="AllowOnlyText();" /></td>
                                                <td>Driver Name<a name="tab48">                       
                                                </td>
                                                <td>
                                                    <input class="form-control" runat="server" id="Txt_driver"
                                                        onkeypress="AllowOnlyText4();" /></td>
                                                <td >
                                                    <input class="form-control" id="Txt_driver_id" type="text" runat="server" visible="False" /></td>
                                           <td >       
                                                <asp:Button ID="btn_save" runat="server" class="btn btn-success" OnClientClick="return check();"   Text="Save & Continue" OnClick="btn_save_Click"/>
                                           </td>
    </tr>
                                        </table>
                                    </div>
                                </div>
                            </td>

                        </tr>
                    </table>
                </div>
            </div>
            <table width="100%">
                <tr>
                    <td>
                        <%-- <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtepwd"
                            ControlToValidate="txtecpwd" ErrorMessage="Passwords are not same"
                            Font-Names="Calibri"></asp:CompareValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td class="style78" colspan="12" rowspan="2">

                        <asp:Button ID="btn_Back" runat="server" class="btn btn-info"
                            Text="Back" OnClick="btn_Back_Click" />


                        <asp:Button ID="btn_update" runat="server" class="btn btn-warning" OnClientClick="return check_update();"
                            Text="Modify" OnClick="btn_update_Click" Visible="false" />



                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 38%;">
                       
                        <asp:Button ID="btn_edit" runat="server" class="btn btn-warning" OnClientClick="return check_update();"
                            Text="Modify" OnClick="btn_edit_Click" Visible="false" />
                    </td>
                </tr>
            </table>
        </div>
        <script src="Employee.js"></script>

</asp:Content>
