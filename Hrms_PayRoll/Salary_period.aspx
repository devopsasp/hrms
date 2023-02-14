<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="salary_period.aspx.cs" Inherits="Bank_Loan_Default" Title="Salary Period" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>
    <link rel="stylesheet" type="text/css" href="../Css/Cand_BaseStyle.css" />
    <script language="javascript" type="text/javascript">
        function validate() {

            var r = confirm("Are you sure you want to delete this record?");
            if (r == true) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>



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
    <div>
        <h2 class="page-header">Salary Period</h2>
        <div>
            <h3>&nbsp;</h3>
            <div style="float: left;">
                <asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" CssClass="InputDefaultStyle" OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="divWaiting">

                <asp:Image ID="imgWait" runat="server" ImageAlign="Middle" ImageUrl="~/Images/loading2.gif" Height="100px" Width="100px" />
                <%--<img src="../loading.gif" alt="Loading" style="position:relative;" />--%>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <table cellpadding="0" cellspacing="1" align="center" class="table"
                style="width: 80%;">
                <tr>
                    <td>Selection</td>
                    <td colspan="">
                        <table>
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server"
                                        RepeatDirection="Horizontal" Font-Size="Small" Width="100%">
                                        <asp:ListItem Selected="True" Value="0">Month</asp:ListItem>
                                        <asp:ListItem Value="1">Day</asp:ListItem>
                                    </asp:RadioButtonList></td>
                                <td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="Selection_check" runat="server"
                            ControlToValidate="RadioButtonList1" ErrorMessage="Select any one option"></asp:RequiredFieldValidator></td>


                    </td>
                </tr>
                <tr>

                    <td>Select the year</td>
                    <td>
                        <asp:DropDownList ID="ddl_select_year" runat="server"
                            CssClass="form-control">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>

                    <td>Select the month</td>
                    <td>
                        <asp:DropDownList ID="ddl_select_month" runat="server" CssClass="form-control" AutoPostBack="True"
                            OnSelectedIndexChanged="ddl_select_month_SelectedIndexChanged">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                            <asp:ListItem Value="January">January</asp:ListItem>
                            <asp:ListItem Value="February">February</asp:ListItem>
                            <asp:ListItem Value="March">March</asp:ListItem>
                            <asp:ListItem Value="April">April</asp:ListItem>
                            <asp:ListItem Value="May">May</asp:ListItem>
                            <asp:ListItem Value="June">June</asp:ListItem>
                            <asp:ListItem Value="July">July</asp:ListItem>
                            <asp:ListItem Value="August">August</asp:ListItem>
                            <asp:ListItem Value="September">September</asp:ListItem>
                            <asp:ListItem Value="October">October</asp:ListItem>
                            <asp:ListItem Value="November">November</asp:ListItem>
                            <asp:ListItem Value="December">December</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>

                    <td>Period Code</td>
                    <td>

                        <asp:TextBox ID="Txt_spcode" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>

                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="Period_check" runat="server"
                            ControlToValidate="Txt_spcode" ErrorMessage="Enter the period code"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr id="row_fd" runat="server">

                    <td>Salary from date</td>
                    <td>


                        <div style="width: 85%; float: left;">
                            <asp:TextBox ID="Txt_fdate" runat="server" onkeyup="fn_date(event,this.id);"
                                CssClass="form-control" OnTextChanged="Txt_fdate_TextChanged"></asp:TextBox>
                        </div>
                        <div style="width: 10%; float: left; margin-left: 10px; margin-top: 3px;">
                            <asp:Image ID="Image1" runat="server" Text="" Width="25px" ImageUrl="~/Images/calendaricon.png" />
                        </div>
                    </td>
                    <td>
                        <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="Txt_fdate"
                            runat="server" Format="dd/MM/yyyy" TodaysDateFormat="d MMMM, yyyy" />
                        <asp:RequiredFieldValidator ID="fromdate_check" runat="server"
                            ControlToValidate="Txt_fdate" ErrorMessage="Enter From Date"></asp:RequiredFieldValidator>
                    </td>

                </tr>
                <tr id="row_td" runat="server">

                    <td>Salary to date</td>
                    <td>

                        <div style="width: 85%; float: left;">
                            <asp:TextBox ID="Txt_tdate" runat="server"
                                CssClass="form-control" OnTextChanged="Txt_tdate_TextChanged"
                                onkeyup="fn_date(event,this.id);" AutoPostBack="True"></asp:TextBox>
                        </div>
                        <div style="width: 10%; float: left; margin-left: 10px; margin-top: 3px;">
                            <asp:Image ID="Image2" runat="server" Text="" Width="25px" ImageUrl="~/Images/calendaricon.png" />
                        </div>

                        <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="Txt_tdate"
                            runat="server" Format="dd/MM/yyyy" />
                    </td>

                    <td>

                        <asp:RequiredFieldValidator ID="Todate_check" runat="server"
                            ControlToValidate="Txt_tdate" ErrorMessage="Enter To Date"></asp:RequiredFieldValidator></td>



                </tr>
                <tr>

                    <td>Total Working Days</td>
                    <td>
                        <asp:TextBox ID="Txt_wdays" runat="server" CssClass="form-control"
                            OnTextChanged="Txt_wdays_TextChanged" Enabled="False"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>

                    <td>Pay date</td>
                    <td>
                        <div style="width: 85%; float: left;">
                            <asp:TextBox ID="txt_paydate" runat="server" onkeyup="fn_date(event,this.id);" CssClass="form-control"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender3" TargetControlID="txt_paydate"
                                runat="server" Format="dd/MM/yyyy" />
                        </div>
                        <div style="width: 10%; float: left; margin-left: 5px; margin-top: 3px;">
                            <asp:Image ID="Image3" runat="server" Text="" Width="25px" ImageUrl="~/Images/calendaricon.png" />
                        </div>
                    </td>
                    <td>
                        <asp:CompareValidator ID="CompareValidator1" runat="server"
                            ErrorMessage="Pay date should be greater than the processing date"
                            ControlToValidate="txt_paydate" Operator="GreaterThanEqual"
                            ControlToCompare="Txt_tdate" Display="Dynamic" Type="Date"></asp:CompareValidator>
                    </td>

                </tr>
                <tr>

                    <td>OT include</td>
                    <td>
                        <asp:DropDownList CssClass="form-control" ID="Chk_ot" runat="server">
                            <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                            <asp:ListItem Text="No" Value="N"></asp:ListItem>
                        </asp:DropDownList>

                    </td>

                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="Img_Save" runat="server" CssClass="btn btn-success"
                            OnClick="Img_Save_Click" Text="Save" />
                        <%--<asp:Button ID="Img_Delete" runat="server" CssClass="btn btn-danger" 
                                                                    onclick="Img_Delete_Click" OnClientClick="return validate()" Text="Delete" />--%>
                        <asp:Button ID="Img_Clear" runat="server" CssClass="btn btn-warning"
                            OnClick="Img_Clear_Click" Text="Clear" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>

                    <td colspan="3">&nbsp;</td>

                </tr>

            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
