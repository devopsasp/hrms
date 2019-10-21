<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="pf.aspx.cs" Inherits="Bank_Loan_Default" Title="PF Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="../../Scripts/Datavalid.js"></script>
    <script type="text/javascript">
        window.onload = function () {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lbl_error.ClientID %>").innerHTML = "";
            }, seconds * 1000);
        };
</script>
    <link href="../../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
function show_message(msg)
{
alert(msg);
}

function fn_date(event, txtid) {
    var len;
    var txtvalue;
    var bool_obj;
    var i, j;
    var str = "";
    str.substring(4, 2);
    txtvalue = document.getElementById(txtid).value;
    txtlen = txtvalue.length;

    if (event.keyCode != 8 && event.keyCode != 46 && event.keyCode != 35 && event.keyCode != 36 && event.keyCode != 37 && event.keyCode != 38 && event.keyCode != 39 && event.keyCode != 40) {
        if (txtlen != 0) {
            if (txtlen == 2) {
                if (txtvalue >= 24) {
                    document.getElementById(txtid).value = "";
                }
                else {
                    document.getElementById(txtid).value = txtvalue + ".";
                }
            }
            else {
                document.getElementById(txtid).value = txtvalue;
            }
        }
    }
}

function fn_Save()
{
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_pf.value == "")
        {
            alert("Enter PF");
            aspnetForm.ctl00$ContentPlaceHolder1$txt_pf.focus();
            return false;
        }
        else if(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_EPF.value=="")
        {
           alert("Enter EPF");
           aspnetForm.ctl00$ContentPlaceHolder1$txt_EPF.focus();
           return false;
        }
        else if(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_FPF.value=="")
        {
           alert("Enter FPF");
           aspnetForm.ctl00$ContentPlaceHolder1$txt_FPF.focus();
            return false;
        }
        else if(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_admincharge.value=="")
        {
           alert("Enter Admin Charge");
           aspnetForm.ctl00$ContentPlaceHolder1$txt_admincharge.focus();
            return false;
        }
        else if(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_amount.value=="")
        {
           alert("Enter Amount");
           aspnetForm.ctl00$ContentPlaceHolder1$txt_amount.focus();
            return false;
        }
        else
        { 
              return true;  
        }
}


    </script>

    <script type="text/javascript">
            function calc() {
                if (document.aspnetForm.ctl00$ContentPlaceHolder1$txt_amount.value != "") {
                    var v = document.aspnetForm.ctl00$ContentPlaceHolder1$txt_ceiling.value;
                    var i = (v * 8.33) / 100;
                    document.aspnetForm.ctl00$ContentPlaceHolder1$txt_amount.value = i.toFixed(2);
                }
            }
    </script>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="row">
                <div class="col-lg-12">
                    <h2 class="page-header">Provident Fund</h2>
                </div>
                <!-- /.col-lg-12 -->
            </div>

            <div class="panel panel-default">
                        <div class="panel-heading">
                            PF Settings
                            <div class="pull-right">
                                <asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" 
                                OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged" CssClass="form-control" >
                            </asp:DropDownList>
                            </div>
                        </div>
                        <!-- /.panel-heading -->
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                            <div class="divWaiting">
                            
                            <asp:Image ID="imgWait" runat="server" ImageAlign="Middle" 
                                    ImageUrl="~/Images/loading2.gif" Height="100px" Width="100px" />
                               
                            </div>
                            </ProgressTemplate>
                            </asp:UpdateProgress>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>

                        <div class="panel-body">
                            <div align="center" id="morris-area-chart" style="width: 90%">
                
                               <table class="table table-striped table-bordered table-hover">
                                    <tbody>
                                        <tr>
                                            <td>Effective Month From</td>
                                            <td>
                            <span style="font-family: Calibri"><span style="color: #444444"><span style="color: #6A6A6A">
                                <asp:DropDownList ID="ddl_month" runat="server" 
                                CssClass="form-control" ForeColor="#333333" Width="100%">
                                    <asp:ListItem Value="January">January</asp:ListItem>
                                    <asp:ListItem Value="Febraury">Febraury</asp:ListItem>
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
                            </span></span></span>
                                            </td>
                                            <td colspan="2" rowspan="2">
                            <asp:RadioButtonList ID="rdo_round" runat="server" Width="223px" Font-Names="Calibri"
                                ForeColor="#333333">
                                <asp:ListItem Value="0" Selected="True"> Round To Nearest Rupee</asp:ListItem>
                                <asp:ListItem Value="1"> Rounded To Next Rupee</asp:ListItem>
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
                                            <td >PF Contribution (in %)</td>
                                            <td>
                            <input runat="server" id="txt_pf" class="form-control" onkeydown="AllowOnlyNumeric1(event);"
                                onkeyup="fn_date(event,this.id);" tabindex="1" maxlength="5" onclick="return txt_pf_onclick()" /></td>
                                            <td>
                            <asp:CheckBox ID="chk_ceiling" runat="server" AutoPostBack="True"
                                ForeColor="#333333" OnCheckedChanged="chk_ceiling_CheckedChanged" Text="  Max Ceiling" />
                                            </td>
                                            <td>
                            <asp:CheckBox ID="chk_allowance" runat="server" AutoPostBack="True"  OnCheckedChanged="chk_ceiling_CheckedChanged" 
                                Text="  Inc Earnings for PF value below ceiling" Visible="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td >EPF Contribution (in %)</td>
                                            <td>
                            <input runat="server" id="txt_EPF" class="form-control" onkeydown="AllowOnlyNumeric1(event);"
                               onkeyup="fn_date(event,this.id);"  tabindex="2" maxlength="5" /></td>
                                            <td>
                                                Upper Limit</td>
                                            <td>
                            <input runat="server" id="txt_ceiling" onkeydown="AllowOnlyNumeric1(event);" onkeyup="calc();" class="form-control" type="text" tabindex="5" 
                                maxlength="7" /></td>
                                        </tr>
                                        <tr>
                                            <td >FPF Contribution (in %)</td>
                                            <td>
                            <input runat="server" id="txt_FPF" class="form-control" onkeydown="AllowOnlyNumeric1(event);"
                                 tabindex="3" onkeyup="fn_date(event,this.id);"
                                onclick="return txt_FPF_onclick()" maxlength="5" /></td>
                                            <td>
                                                Eligibility Amount</td>
                                            <td>
                            <input runat="server" id="txt_amount" class="form-control" onkeydown="AllowOnlyNumeric1(event);"
                                 tabindex="6" maxlength="7" /></td>
                                        </tr>
                                        <tr>
                                            <td >Admin Charges (in %)</td>
                                            <td>
                            <input runat="server" id="txt_admincharge" class="form-control" onkeydown="AllowOnlyNumeric1(event);"
                                onkeyup="fn_date(event,this.id);" tabindex="4" maxlength="5" /></td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <span style="font-family: Calibri"><span style="color: #444444">
                                                <span style="color: #6A6A6A">
                                                <asp:Button ID="btn_save" runat="server" class="btn btn-success" Text="Save" 
                                                    onclick="btn_save_Click" />
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btn_reset" runat="server"  class="btn btn-warning" Text="Reset" 
                                                    onclick="btn_reset_Click" />
                            </span></span></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="4" >
                            <asp:Label ID="lbl_error" runat="server" CssClass="Error" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table> 
                
                            </div>
                        </div>

                        </ContentTemplate>
                                </asp:UpdatePanel>
                        <!-- /.panel-body -->
                    </div>

    </asp:Content>
