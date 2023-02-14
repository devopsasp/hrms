<%@ Page Language="C#" MasterPageFile="~/HRMS.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="Final_settlement_new.aspx.cs" Inherits="Hrms_PayRoll_Final_settlement_new" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

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


    function fn_date(event, txtid) {
        var len;
        var txtvalue;
        var bool_obj;
        var i;

        txtvalue = document.getElementById(txtid).value;
        txtlen = txtvalue.length;

        if (event.keyCode != 8 && event.keyCode != 46 && event.keyCode != 35 && event.keyCode != 36 && event.keyCode != 37 && event.keyCode != 38 && event.keyCode != 39 && event.keyCode != 40) {
            if (txtlen != 0) {
                bool_obj = true;

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
</script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
<script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>

            <div class="row">
                <div class="col-lg-12">
                    <h3 class="page-header">Final Settlement</h3>
                </div>
                <!-- /.col-lg-12 -->
            </div>

            <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            Settlement Process
                                        </h4>
                                    </div>
                                    <div id="collapseOne" class="panel-collapse collapse in">
                    <table cellpadding="1%" cellspacing="1%" width="100%" 
                                                class="table table-striped table-bordered table-hover">
                        
                        <tr>
                            <td>
                                Settlement Type</td>
                            <td colspan="3">
                <asp:RadioButtonList align="left" ID="rd_final_process"   RepeatDirection="Horizontal" 
                    runat="server" Width="60%" ForeColor="Black" 
                        AutoPostBack="True" 
                        onselectedindexchanged="rd_final_process_SelectedIndexChanged" 
                        CellPadding="1" CellSpacing="1">
  <asp:ListItem Value="1">Retirement</asp:ListItem>
                    <asp:ListItem Value="2">Death</asp:ListItem>
                    <asp:ListItem Value="3">Voluntary Retirement</asp:ListItem>
                    <asp:ListItem Value="4" Selected="True">None</asp:ListItem>
                </asp:RadioButtonList>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                Reference No</td>
                            <td>
                <asp:TextBox ID="txt_refno" runat="server" class="form-control"></asp:TextBox>
                
                            </td>
                            <td>
                                Select Year</td>
                            <td>
                
                <asp:DropDownList ID="ddl_year" runat="server" class="form-control" Width="100%" 
                    AutoPostBack="True" onselectedindexchanged="ddl_year_SelectedIndexChanged">
                </asp:DropDownList>
                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Select Department</td>
                            <td>
                 <asp:DropDownList ID="ddl_dept" runat="server"
                                Width="100%" AutoPostBack="True" class="form-control"
                                    onselectedindexchanged="ddl_dept_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                            <td>
                                Select Employee</td>
                            <td>
                <asp:DropDownList ID="ddl_employee" runat="server" Width="100%" class="form-control" AutoPostBack="True" onselectedindexchanged="ddl_employee_SelectedIndexChanged">
                                
                            </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Joining Date</td>
                            <td>
               <%--<asp:TextBox ID="txt_joiningDate" runat="server" class="form-control"></asp:TextBox>--%>

                <div style=" width:85%; float:left;">
                    <asp:TextBox ID="txt_joiningDate" runat="server" class="form-control"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txt_joiningDate">
                    </asp:CalendarExtender>
                </div>
                 <div style=" width:10%; float:left;  margin-left:5px; margin-top:3px;">                                                
                                                    <asp:Image ID="Image2" runat="server"  
                                                        Text="" Width="25px" 
                                                        ImageUrl="~/Images/calendaricon.png" />
                                                </div>

                              </td>                                                  
                            <td>
                                Relieving Date</td>
                            <td>
                             <div style=" width:85%; float:left;">
                            <asp:TextBox ID="txt_date" runat="server" class="form-control" onkeyup="fn_date(event,this.id);" 
                                ontextchanged="txt_date_TextChanged" AutoPostBack="True"></asp:TextBox>
                               <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txt_date">
                                            </asp:CalendarExtender>             
                                </div>
                                <div style=" width:10%; float:left;  margin-left:5px; margin-top:3px;">                                                
                                                    <asp:Image ID="Image1" runat="server"  
                                                        Text="" Width="25px" 
                                                        ImageUrl="~/Images/calendaricon.png" />
                                                </div>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                No of Years Served</td>
                            <td>
                <asp:TextBox ID="txt_service" runat="server" class="form-control"  onkeypress="return isNumber(event)"></asp:TextBox>
                                                        </td>
                            <td>
                                Basic + DA</td>
                            <td>
                        <asp:TextBox ID="txt_basic_pay" runat="server" class="form-control" onkeypress="return onlyNumbersWithDot(event);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Total PF Amount</td>
                            <td>
                            <asp:TextBox  ID="txt_pf" runat="server" class="form-control" onkeypress="return onlyNumbersWithDot(event);"></asp:TextBox>
                                                                                </td>
                            <td>
                                Gratuity Amount</td>
                            <td>
                <asp:TextBox ID="txt_grauity" runat="server" class="form-control" onkeypress="return onlyNumbersWithDot(event);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Encashment Amount</td>
                            <td>
                                <asp:TextBox ID="txt_encashment" runat="server" class="form-control" onkeypress="return onlyNumbersWithDot(event);"></asp:TextBox>
                                                                                </td>
                            <td>
                                Loan Amount</td>
                            <td>
                                <asp:TextBox ID="txt_loan" runat="server" class="form-control" onkeypress="return onlyNumbersWithDot(event);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Deduct Salary</td>
                            <td>
                                <asp:CheckBox ID="ckh_deduct" runat="server" AutoPostBack="True" 
                                    oncheckedchanged="ckh_deduct_CheckedChanged"/>
                            </td>
                            <td>
                                Deduct Amount</td>
                            <td>
                               <asp:TextBox Visible="false"  ID="txt_deduct_amt" runat="server" class="form-control" onkeypress="return onlyNumbersWithDot(event);">0.0</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Final Amount</td>
                            <td>
                <asp:TextBox ID="txt_final_amt" runat="server" class="form-control" onkeypress="return onlyNumbersWithDot(event);"></asp:TextBox>
                <asp:TextBox ID="txt_extra" runat="server" class="form-control" Visible="False"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btn_save" runat="server" class="btn btn-success"  Text="Save" 
                                      onclick="btn_save_Click" />
                            </td>
                              <td>
                                <asp:Button ID="btn_clear" runat="server" class="btn btn-warning"  Text="Clear" 
                                      onclick="btn_clear_Click"/>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="4">

                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                

                <asp:GridView ID="grid_settlement" AutoGenerateColumns="False" runat="server" CssClass="table table-hover table-striped"
                    onrowediting="grid_settlement_RowEditing" 
                    onrowcancelingedit="grid_settlement_RowCancelingEdit" 
                    onrowupdating="grid_settlement_RowUpdating" >

            <Columns>
                <asp:TemplateField HeaderText="Employee">
           <ItemTemplate>
                        <asp:Label ID="lbl_id" Text='<%# Eval("EmployeeName") %>'  runat="server"></asp:Label>
           </ItemTemplate>
          <ItemStyle HorizontalAlign="Center" />
           </asp:TemplateField>
            <asp:TemplateField HeaderText="Joining Date" ControlStyle-Width="100px">
           <ItemTemplate>
                        <asp:Label ID="lbl_joining_date" Text='<%# Eval("Joining_date") %>' runat="server"></asp:Label>
           </ItemTemplate>
           <ItemStyle HorizontalAlign="Center" />
           </asp:TemplateField> 
           <asp:TemplateField HeaderText="Retirement Date" Visible="false">
           <ItemTemplate>
                        <asp:Label ID="lbl_retirement_date" Text='<%# Eval("Retire_date  ") %>' runat="server"></asp:Label>
           </ItemTemplate>
          <%-- <EditItemTemplate>
               <asp:TextBox ID="txt_subject" Text='<%# Bind("Compliant_Subject") %>' runat="server"></asp:TextBox>
           </EditItemTemplate>--%>
           </asp:TemplateField>
                <asp:TemplateField HeaderText="Last Working Date">
                 <ItemStyle Wrap="true" Width="100px" />
           <ItemTemplate>
                        <asp:Label ID="lbl_last_date" style="word-wrap:break-word" Text='<%# Eval("Last_working_date ") %>' runat="server"></asp:Label>
           </ItemTemplate>
           <ItemStyle HorizontalAlign="Center" />
          
           </asp:TemplateField>
       <asp:TemplateField HeaderText="No of Years Serviced" Visible="false">
           <ItemTemplate>
                        <asp:Label ID="lbl_years_serviced" Text='<%# Eval("Year") %>' runat="server"></asp:Label>
           </ItemTemplate>
           <ItemStyle HorizontalAlign="Center" />
         <%-- <EditItemTemplate>
               <asp:TextBox ID="txt_status" Text='<%# Bind("Status1") %>' TextMode="MultiLine" runat="server"></asp:TextBox>
           </EditItemTemplate>--%>
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Grauity Amount" >
           <ItemTemplate>
                        <asp:Label ID="lbl_gruity_amt" Text='<%# Eval("Grauity") %>' runat="server"></asp:Label>
           </ItemTemplate>
           <ItemStyle HorizontalAlign="Center" />
          <%-- <EditItemTemplate>
               <asp:TextBox ID="txt_status" Text='<%# Bind("Status1") %>' TextMode="MultiLine" runat="server"></asp:TextBox>
           </EditItemTemplate>--%>
           </asp:TemplateField>
              <asp:TemplateField HeaderText="PF Amount" >
           <ItemTemplate>
                        <asp:Label ID="lbl_pf_amt" Text='<%# Eval("Pf_share  ") %>' runat="server"></asp:Label>
           </ItemTemplate>
           <ItemStyle HorizontalAlign="Center" />
          <%-- <EditItemTemplate>
               <asp:TextBox ID="txt_status" Text='<%# Bind("Status1") %>' TextMode="MultiLine" runat="server"></asp:TextBox>
           </EditItemTemplate>--%>
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Encashment Amount">
           <ItemTemplate>
                        <asp:Label ID="lbl_encash_amt" Text='<%# Eval("Encashment_amt") %>' runat="server"></asp:Label>
           </ItemTemplate>
           <ItemStyle HorizontalAlign="Center" />
          <%-- <EditItemTemplate>
               <asp:TextBox ID="txt_status" Text='<%# Bind("Status1") %>' TextMode="MultiLine" runat="server"></asp:TextBox>
           </EditItemTemplate>--%>
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Loan Amount">
           <ItemTemplate>
                        <asp:Label ID="lbl_loab_amt" Text='<%# Eval("loan_amt  ") %>' runat="server"></asp:Label>
           </ItemTemplate>
           <ItemStyle HorizontalAlign="Center" />
          <%-- <EditItemTemplate>
               <asp:TextBox ID="txt_status" Text='<%# Bind("Status1") %>' TextMode="MultiLine" runat="server"></asp:TextBox>
           </EditItemTemplate>--%>
           </asp:TemplateField>  
               <asp:TemplateField HeaderText="Deducted Salary Amount">
           <ItemTemplate>
                        <asp:Label ID="lbl_deductsalary_amt" Text='<%# Eval("Deduct_salary_amt") %>' runat="server"></asp:Label>
           </ItemTemplate>
           <ItemStyle HorizontalAlign="Center" />
          <%-- <EditItemTemplate>
               <asp:TextBox ID="txt_status" Text='<%# Bind("Status1") %>' TextMode="MultiLine" runat="server"></asp:TextBox>
           </EditItemTemplate>--%>
           </asp:TemplateField>
               <asp:TemplateField HeaderText="Final Amount">
           <ItemTemplate>
                        <asp:Label ID="lbl_final_amt" Text='<%# Eval("Final_amt") %>' runat="server"></asp:Label>
           </ItemTemplate>
           <ItemStyle HorizontalAlign="Center" />
          <%-- <EditItemTemplate>
               <asp:TextBox ID="txt_status" Text='<%# Bind("Status1") %>' TextMode="MultiLine" runat="server"></asp:TextBox>
           </EditItemTemplate>--%>
           </asp:TemplateField>
         <asp:TemplateField HeaderText="Status">
           <ItemTemplate>
                        <asp:Label ID="lbl_status" Text='<%# Eval("Status") %>' runat="server"></asp:Label>
           </ItemTemplate>
           <ItemStyle HorizontalAlign="Center" />
           <EditItemTemplate>
               <asp:TextBox ID="txt_status" Text='<%# Bind("Status") %>' TextMode="MultiLine" runat="server"></asp:TextBox>
           </EditItemTemplate>
           </asp:TemplateField>
         <asp:CommandField ShowCancelButton="true" ShowEditButton="true" ShowDeleteButton="true" />
            </Columns>
             </asp:GridView>


                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="left">
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#0066FF" 
                            style="text-align: center"></asp:Label>
                               </td>
                        </tr>
                     </table>   
                                    </div>
                                </div>


 </asp:Content>
