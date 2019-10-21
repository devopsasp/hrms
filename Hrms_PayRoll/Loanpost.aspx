<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="Loanpost.aspx.cs" Inherits="Hrms_PayRoll_Loanpost" Title="Loan Post" %>



<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>



<asp:Content ID="Content1"  ContentPlaceHolderID="ContentPlaceHolder1"  Runat="Server">
    <script type="text/javascript">

        function onlyNumbersWithDot(e) {
            var charCode;
            if (e.keyCode > 0) {
                charCode = e.which || e.keyCode;
            }
            else if (typeof (e.charCode) != "undefined") {
                charCode = e.which || e.keyCode;
            }
            if (charCode == 46)
                return true
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return true;

            return false;
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
   function validate()
   {
     if(document.getElementById("<%=txt_loan_req.ClientID%>").value=="")
    {
     alert("Don't Leave empty");
     document.getElementById("<%=txt_loan_req.ClientID%>").focus();     
     return false;
    }   
    
    else
    {
    return true;
    }
   }

   </script>
   <asp:ToolkitScriptManager 
                    ID="ToolkitScriptManager1" runat="server">
                </asp:ToolkitScriptManager>
   <div><h2 class="page-header">Loan Post</h2></div>
   <div><asp:Label ID="lbl_error" runat="server" CssClass="Error" ForeColor="Red"></asp:Label><div style="float:right;"></div></div>
    <table style="width: 100%;"  class="table">
   
        <tr>
            <td >
                Loan Request No </td>
            <td >
            <table><tr><td>
                <asp:TextBox ID="txt_loan_req"  runat="server" maxlength="30" 
                     ontextchanged="txt_loan_req_TextChanged"  CssClass="form-control"
                    AutoPostBack="True" ></asp:TextBox></td><td>
                <asp:LinkButton ID="ImageButton1" runat="server" 
                     onclick="ImageButton1_Click"   CssClass="btn btn-circle btn-default glyphicon glyphicon-search"
                     ></asp:LinkButton></td><td>
                <asp:ListBox ID="ListBox1" runat="server" AutoPostBack="True" CssClass="form-control"
                    onselectedindexchanged="ListBox1_SelectedIndexChanged"></asp:ListBox>
                    </td></tr></table>
            </td>
            <td >
                Date </td>
            <td >
            <div style=" width:150px; float:left;">
               <asp:TextBox ID="txt_req_date" runat="server" MaxLength="10" CssClass="form-control" Width="150px"  onkeyup="fn_date(event,this.id);"></asp:TextBox>

                <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                    TargetControlID="txt_req_date" Format="MM/dd/yyyy" >
                </asp:CalendarExtender>
                </div>
                 <div style=" width:25px; float:left;  margin-left:10px; margin-top:3px;">                                                
                                                    <asp:Image ID="Image2" runat="server"  
                                                        Text="" Width="25px" 
                                                        ImageUrl="~/Images/calendaricon.png" />
                                                </div>

            </td>
        </tr>
        <tr>
            <td >
                Employee Id </td>
            <td >
                <asp:DropDownList ID="ddl_emp" runat="server" AutoPostBack="True" CssClass="form-control"
                    onselectedindexchanged="ddl_emp_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td >
                Employee Name</td>
            <td >
                <input type="text" ID="txt_empnam" runat="server" class="form-control"
                    disabled="disabled" />
            </td>
        </tr>
        <tr>
            <td >
                Loan Id </td>
            <td>
                <asp:DropDownList ID="ddl_loan" runat="server" AutoPostBack="True" CssClass="form-control"
                    onselectedindexchanged="ddl_loan_SelectedIndexChanged" >
                </asp:DropDownList>
            </td>
            <td>
                Loan Type</td>
            <td>
                <input type="text" ID="txt_loan_type" runat="server" class="form-control"
                    disabled="disabled" />
            </td>
        </tr>
        <tr>
            <td>
                Loan Name </td>
            <td style="width: 272px;">
                <input type="text" ID="txt_loan_name" runat="server" class="form-control"
                    disabled="disabled" />
            </td>
            <td >
                Loan Amount</td>
            <td >
                <input type="text" ID="txt_loan_amt" runat="server" class="form-control"
                    disabled="disabled" />
            </td>
        </tr>
        <tr>
            <td>
                Month To Posted </td>
            <td>
                <asp:DropDownList ID="ddl_mon_topost" runat="server" CssClass="form-control" AutoPostBack="True" onselectedindexchanged="ddl_mon_topost_SelectedIndexChanged" >
                </asp:DropDownList>
            </td>
            <td>
                Month Posted On</td>
            <td>
                <asp:DropDownList ID="ddl_mon_posted" runat="server"  AutoPostBack="True" CssClass="form-control">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Remaining Months </td>
            <td>
                <input type="text" ID="txt_rem_mon" runat="server" class="form-control"
                    disabled="disabled" />
            </td>
            <td>
                Posted Amount</td>
            <td>
               <input type ="text" ID="txt_post_amt" runat="server" class="form-control" onkeypress="return onlyNumbersWithDot(event);"/></td>
        </tr>
        <tr>
            <td>
                Balance Amount </td>
            <td colspan="3">
                <input type="text" ID="txt_bal_amt" runat="server" class="form-control" onkeypress="return onlyNumbersWithDot(event);"/></td>
           
        </tr>
        <tr>
            <td>
                Approved By
            </td>
            <td>
                <input type="text" ID="txt_app_by" runat="server" class="form-control"  onkeypress="return isNumberKey(event)"/>
            </td>
            <td colspan="2"></td>
        </tr>
        <tr>
            <td colspan="4">
                <table style="width: 60%" align="center">
                    <tr>
                        <td >
                <asp:Button ID="btn_save" runat="server" Text="Save"  CssClass="btn btn-success"
                    onclick="btn_save_Click"  />
                        </td>
                        <td>
                <asp:Button ID="btn_undo" runat="server" Text="Undo"  CssClass="btn btn-primary"
                                onclick="btn_undo_Click"  />
                        </td>
                        <td >
                <asp:Button ID="btn_clear" runat="server" Text="Clear" CssClass="btn btn-warning"
                    onclick="btn_clear_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
   
</asp:Content>

