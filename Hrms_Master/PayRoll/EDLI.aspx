<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="EDLI.aspx.cs" Inherits="Bank_Loan_Default" Title="EDLI Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../../Scripts/Datavalid.js"></script>

    <%--<script type="text/javascript" language="javascript">
function timeout()
{
var t=setTimeout("",10000);
}

function hide()
{
alert(document.getElementById("lbl_Error"));
}

</script>--%>
    <script language="javascript" type="text/javascript">

        function fn_Save() {
            if (document.aspnetForm.ctl00$ContentPlaceHolder1$txtEmployee_con.value == "") {
                alert("Enter Employee Contribution Amount");
                aspnetForm.ctl00$ContentPlaceHolder1$txtEmployee_con.focus();
                return false;
            }
            else if (document.aspnetForm.ctl00$ContentPlaceHolder1$txtEloyer_con.value == "") {
                alert("Enter Employer Contribution Amount");
                aspnetForm.ctl00$ContentPlaceHolder1$txtEloyer_con.focus();
                return false;
            }
            else if (document.aspnetForm.ctl00$ContentPlaceHolder1$txtAdmin_Charges.value == "") {
                alert("Enter Admin Charges");
                aspnetForm.ctl00$ContentPlaceHolder1$txtAdmin_Charges.focus();
                return false;
            }
            else if (document.aspnetForm.ctl00$ContentPlaceHolder1$txtEligibility_Amt.value == "") {
                alert("Enter Eligibility_Amt");
                aspnetForm.ctl00$ContentPlaceHolder1$txtEligibility_Amt.focus();
                return false;
            }
            else {
                return true;
            }
        }    
    </script>

    <script language="javascript" type="text/javascript">

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

    <div class="row">
                <div class="col-lg-12">
                    <h2 class="page-header">Employee Deposit Linked Insurance</h2>
                </div>
                <!-- /.col-lg-12 -->
            </div>

            <div class="panel panel-default">
                        <div class="panel-heading">
                            EDLI Settings
                            <div class="pull-right">
                                <asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" CssClass="InputDefaultStyle"
                    OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged">
                </asp:DropDownList>
                            </div>
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <div align="center" id="morris-area-chart" style="width: 90%">
                
                               <table class="table table-striped table-bordered table-hover">
                                    <tbody>
                                        <tr>
                                            <td colspan="2" >Does the organization have group insurance?</td>
                                            <td align="left" colspan="2">
                                                <asp:RadioButtonList ID="rdo_ins" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdo_ins_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            </tr>
                                        <tr>
                                            <td>Effective Month From</td>
                                            <td>
                            <span style="font-family: Calibri"><span style="color: #444444"><span style="color: #6A6A6A">
                                <asp:DropDownList ID="ddl_month" runat="server" 
                                CssClass="form-control" ForeColor="#333333" Width="100%">
                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                    <asp:ListItem Value="1">January</asp:ListItem>
                                    <asp:ListItem Value="2">Febraury</asp:ListItem>
                                    <asp:ListItem Value="3">March</asp:ListItem>
                                    <asp:ListItem Value="4">April</asp:ListItem>
                                    <asp:ListItem Value="5">May</asp:ListItem>
                                    <asp:ListItem Value="6">June</asp:ListItem>
                                    <asp:ListItem Value="7">July</asp:ListItem>
                                    <asp:ListItem Value="8">August</asp:ListItem>
                                    <asp:ListItem Value="9">September</asp:ListItem>
                                    <asp:ListItem Value="10">October</asp:ListItem>
                                    <asp:ListItem Value="11">November</asp:ListItem>
                                    <asp:ListItem Value="12">December</asp:ListItem>
                                </asp:DropDownList>
                            </span></span></span>
                                            </td>
                                            <td colspan="2" rowspan="2">
                            <asp:RadioButtonList ID="rdo_round" runat="server" Width="223px" Font-Names="Calibri"
                                ForeColor="#333333">
                                <asp:ListItem Value="0" Selected="True">Round To Nearest Rupee</asp:ListItem>
                                <asp:ListItem Value="1">Rounded To Next Rupee</asp:ListItem>
                            </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td >Effective From Year</td>
                                            <td>
                            <span style="font-family: Calibri"><span style="color: #444444"><span style="color: #6A6A6A">
                                <asp:DropDownList ID="ddl_year" runat="server" Width="100%" 
                                CssClass="form-control" ForeColor="#333333">
                                </asp:DropDownList>
                            </span></span></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td >Lower Limit</td>
                                            <td>
                            <input runat="server" id="txtAdmin_Charges" class="form-control" onkeydown="AllowOnlyNumeric1(event);"
                                 tabindex="1" maxlength="8" onclick="return txt_pf_onclick()" /></td>
                                            <td>
                                                Upper Limit</td>
                                            <td>
                            <input runat="server" id="txtEligibility_Amt" onkeydown="AllowOnlyNumeric1(event);" 
                             class="form-control" type="text" tabindex="5" 
                                maxlength="8" /></td>
                                        </tr>
                                        <tr>
                                            <td >Employer Contribution (in %)</td>
                                            <td>
                            <input runat="server" id="txtEloyer_con" class="form-control"
                                onfocus="calc();" onkeydown="AllowOnlyNumeric1(event);"
                                tabindex="6" onclick="return txt_amount_onclick()" maxlength="5" /></td>
                                            <td>
                                                <asp:RangeValidator ID="percentageRangeValidator" runat="server" ControlToValidate="txtEloyer_con" Display="Dynamic" 
                                                        ErrorMessage="Invalid Percentage" MaximumValue="100.00" MinimumValue="0.00" 
                                                        Type="Double">Invalid Percentage</asp:RangeValidator>
                                               </td>
                                            <td>
                                                <span style="font-family: Calibri"><span style="color: #444444">
                                                <span style="color: #6A6A6A">
                                                <asp:Button ID="btn_save" runat="server" class="btn btn-success" Text="Save" 
                                                    onclick="btn_save_Click" />
                                                &nbsp;&nbsp;
                                                <asp:Button ID="btn_reset" runat="server"  class="btn btn-warning" Text="Reset" 
                                                    onclick="btn_reset_Click" />
                            </span></span></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td >&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="4" >
                                                &nbsp;</td>
                                        </tr>
                                    </tbody>
                                </table> 
                
                            </div>
                        </div>
                        <!-- /.panel-body -->
                    </div>

    </asp:Content>
