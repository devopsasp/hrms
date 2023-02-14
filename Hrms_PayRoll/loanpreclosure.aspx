<%@ Page MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="LoanPreclosure.aspx.cs" Inherits="Bank_Loan_Default" Title="Loan Preclosure" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>
<script language="javascript" type="text/javascript" src="../datecheck.js"></script>


<div><h3 class="page-header">Loan Preclosure</h3></div>
    <div class=pull-right><asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" CssClass="form-control"
                    OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged">
                </asp:DropDownList>
        </div>
   <%-- <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div style="width: 90%">
                    <table cellpadding="1%" cellspacing="1%" width="100%" class="table table-striped table-bordered table-hover">
                        
                        <tr>
                            <td>
                                Application Date</td>
                            <td style="width:30%;";>
                            <div style=" float:left;">
                                <asp:TextBox ID="Txtappdate" runat="server" CssClass="form-control" maxlength="10" 
                                    onkeyup="fn_date(event,this.id);"></asp:TextBox> 
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="Txtappdate" Format="dd/MM/yyyy">
                                </asp:CalendarExtender>
                                    </div>
                                    <div style=" width:25px; float:left;  margin-left:10px; margin-top:3px;">                                                
                                                    <asp:Image ID="Image2" runat="server"  
                                                        Text="" Width="25px" 
                                                        ImageUrl="~/Images/calendaricon.png" />
                                                </div>
                                  <br />
                                
                             
                                  <asp:RequiredFieldValidator ID="val_app_date" runat="server" 
                                    ControlToValidate="Txtappdate" ErrorMessage="Application Date is Required" 
                                    Font-Names="Calibri"></asp:RequiredFieldValidator>
                               
                              
                            </td>
                            <td >
                                Closure Type</td>
                            <td >
                                <asp:DropDownList ID="ddl_closure_type" runat="server" AutoPostBack="True" 
                                    CssClass="form-control" onselectedindexchanged="ddl_closure_type_SelectedIndexChanged">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem Value="Pre Closure">Pre Closure</asp:ListItem>
                                    <asp:ListItem>Force Closure</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Select Employee</td>
                            <td>
                                <asp:DropDownList ID="ddl_employee" runat="server" AutoPostBack="True" 
                                    CssClass="form-control" onselectedindexchanged="ddl_employee_SelectedIndexChanged" TabIndex="1">
                                </asp:DropDownList>
                            </td>
                            <td>
                                Loan ID</td>
                            <td >
                                <asp:DropDownList ID="ddl_loan" runat="server" AutoPostBack="True" 
                                    CssClass="form-control" onselectedindexchanged="ddl_loan_SelectedIndexChanged">
                                </asp:DropDownList>
                              
                                <asp:RequiredFieldValidator ID="val_loan_process" runat="server" 
                                    ControlToValidate="txt_loan_process" ErrorMessage="Select the Loan Id" 
                                    Font-Names="Calibri"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Loan Name</td>
                            <td>
                                <asp:TextBox ID="txt_lname" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td>
                                Loan Process</td>
                            <td >
                                <asp:TextBox ID="txt_loan_process" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Loan Amount</td>
                            <td>
                                <asp:TextBox ID="txtamount" runat="server" CssClass="form-control" 
                                    onkeydown="AllowOnlyNumeric1(event);" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td>
                                Loan Intererst</td>
                            <td >
                                <asp:TextBox ID="txt_int_amt" runat="server" CssClass="form-control"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Paid Amount</td>
                            <td>
                                <asp:TextBox ID="txtpaidamount" runat="server" CssClass="form-control"
                                    onkeydown="AllowOnlyNumeric1(event);" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td>
                                Balance Amount</td>
                            <td >
                                <asp:TextBox ID="txtbalamount" runat="server" CssClass="form-control"
                                    onkeydown="AllowOnlyNumeric1(event);" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Closure Amount</td>
                            <td>
                                <asp:TextBox ID="txtcloseamount" runat="server" CssClass="form-control"
                                    onkeydown="AllowOnlyNumeric1(event);"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td >
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Closure by</td>
                            <td>
                                <asp:RadioButtonList ID="rdo_check" runat="server" AutoPostBack="True" 
                                    Font-Names="Calibri" OnSelectedIndexChanged="rdo_check_SelectedIndexChanged">
                                    <asp:ListItem>Cash Pre Closure</asp:ListItem>
                                    <asp:ListItem>Cash Force Closure</asp:ListItem>
                                    <asp:ListItem>Cheque Pre Closure</asp:ListItem>
                                    <asp:ListItem>Cheque Force Closure</asp:ListItem>
                                </asp:RadioButtonList>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="rdo_check" ErrorMessage="Select any option" 
                                    Font-Names="Calibri"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                Remarks</td>
                            <td >
                                <asp:TextBox ID="txtremarks" runat="server" CssClass="form-control"
                                   onkeypress="AllowOnlyText3();" TextMode="MultiLine" type="text"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Cheque No</td>
                            <td>
                                <input type="text" runat="server" id="txtchkno" class="form-control"
                                    onkeydown="AllowOnlyNumeric1(event);" />
                            </td>
                            <td>
                                Cheque Date</td>
                            <td >
                                 <div style="float:left;">
                             
                                     <asp:TextBox  id="txtchkdate" runat="server" class="form-control"
                                    onkeyup="fn_date(event,this.id);" maxlength="10" ></asp:TextBox>

                                     <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtchkdate" Format="dd/MM/yyyy">
                                     </asp:CalendarExtender>
                                        </div>   
                                    <div style=" width:25px; float:left;  margin-left:10px; margin-top:3px;">                                                
                                                    <asp:Image ID="Image1" runat="server"  
                                                        Text="" Width="25px" 
                                                        ImageUrl="~/Images/calendaricon.png" />
                                                </div>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                Cheque Amount</td>
                            <td>
                                <input type="text" runat="server" id="txtchkamount" class="form-control"
                                    onkeydown="AllowOnlyNumeric1(event);" />
                            </td>
                            <td>
                                Bank Name</td>
                            <td >
                                <input type="text" runat="server" id="txtchkbankname" class="form-control" onkeypress="AllowOnlyText3();"
                                    onkeydown="AllowOnlyText1(event);" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;</td>                                         
                            <td colspan="2">
                                <asp:Button ID="loan_details" runat="server" CssClass="btn btn-info"
                                    OnClick="loan_details_Click" Text="Loan Details" Visible="false" />
                           
                                <asp:Button ID="btn_save" runat="server" CssClass="btn btn-success" 
                                    OnClick="btn_save_Click" Text="Save" />
                            </td>
                        </tr>
              
                     </table>   
                     </div>
                                <asp:GridView ID="grid_closere" runat="server" AutoGenerateColumns="False" CssClass="table table-hover table-striped"
                                    GridLines="None" onrowcancelingedit="grid_closere_RowCancelingEdit" 
                                    onrowediting="grid_closere_RowEditing" onrowupdating="grid_closere_RowUpdating">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Loan Id">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_loan_id" runat="server" Text='<%# Eval("loan_mas_id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Eff Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_eff_date" runat="server" 
                                                    Text='<%#Eval("strdateapplication","{0:dd/MM/yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Loan Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_loan_amt" runat="server" Text='<%# Eval("loan_amt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Balance Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_balance_amt" runat="server" 
                                                    Text='<%# Eval("balanceamount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Paid Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_paid_amt" runat="server" Text='<%# Eval("paidamount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cheque No">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_check_no" runat="server" class="form-control"
                                                    Text='<%# Bind("checkno") %>' Width="80px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_check_no" runat="server" Text='<%# Eval("checkno") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cheque Date">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_check_date" runat="server" class="form-control"
                                                    onkeyup="fn_date(event,this.id);" 
                                                    Text='<%# Bind("strcheckdate","{0:dd/MM/yyyy}") %>' Width="80px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_check_date" runat="server" 
                                                    Text='<%# Eval("strcheckdate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cheque Amt">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_check_amt" runat="server" class="form-control"
                                                    Text='<%# Bind("checkamount") %>' Width="81px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_check_amount" runat="server" 
                                                    Text='<%# Eval("checkamount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bank Name">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_bank_nam" runat="server" class="form-control"
                                                    Text='<%# Bind("bankname") %>' Width="81px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_bank_nam" runat="server" Text='<%# Eval("bankname") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pay Mode">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddl_pay_mode" runat="server" AutoPostBack="True" 
                                                    DataTextField='<%# Bind("pay_mode") %>' class="form-control"
                                                    SelectedValue='<%# Bind("pay_mode") %>' Width="90px">
                                                    <asp:ListItem>Cash Pre Closure</asp:ListItem>
                                                    <asp:ListItem>Cash Force Closure</asp:ListItem>
                                                    <asp:ListItem>Cheque Pre Closure</asp:ListItem>
                                                    <asp:ListItem>Cheque Force Closure</asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_pay_mode" runat="server" Text='<%# Eval("pay_mode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                         <asp:CommandField HeaderText="Edit"  ItemStyle-Width="25px" EditImageUrl="~/Images/edit_icon.png" ButtonType="Image" UpdateImageUrl="~/Images/save_icon.jpg" CancelImageUrl="~/Images/cancel.png" ShowEditButton="True" />
                        
                                    </Columns>
                                </asp:GridView>
                        
    </ContentTemplate>
    </asp:UpdatePanel>


    </asp:Content>
