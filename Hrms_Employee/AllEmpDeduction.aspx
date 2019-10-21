<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="AllEmpDeduction.aspx.cs" Inherits="Bank_Loan_Default" Title="HRMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>
<script language="javascript" type="text/javascript" src="../datecheck.js"></script>
<script language="javascript" type="text/javascript">
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
    function load() {
        setTimeout("document.getElementById('lbl_Error').style.visibility='hidden';", 5000);
    }

       function fn_Save()
        {
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_date.value == "")
        {
            alert("Enter Date");
            aspnetForm.ctl00$ContentPlaceHolder1$txt_date.focus();
            return false;
        }
        else if(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_fromamt.value=="")
        {
           alert("Enter From Amount");
           aspnetForm.ctl00$ContentPlaceHolder1$txt_fromamt.focus();
            return false;
        }
        else if(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_toamt.value=="")
        {
           alert("Enter To Amount");
           aspnetForm.ctl00$ContentPlaceHolder1$txt_toamt.focus();
            return false;
        }
        else if(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_ptaxamount.value=="")
        {
           alert("Enter Tax Amount");
           aspnetForm.ctl00$ContentPlaceHolder1$txt_ptaxamount.focus();
            return false;
        }
        else
        { 
              return true;  
        }
    }    
    </script>

    <script>
        $(document).ready(function () {

            $('#txt_contribution').keypress(function (event) {
                if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
                    if (event.keyCode !== 8 && event.keyCode !== 46) { //exception
                        event.preventDefault();
                    }
                }
                if (($(this).val().indexOf('.') != -1) && ($(this).val().substring($(this).val().indexOf('.'), $(this).val().indexOf('.').length).length > 2)) {
                    if (event.keyCode !== 8 && event.keyCode !== 46) { //exception
                        event.preventDefault();
                    }
                  }
            });
        });
    </script>

    <div class="row">
                <div class="col-lg-12">
                    <h2 class="page-header">All Employee Deductions</h2>
                </div>
                <!-- /.col-lg-12 -->
            </div>

            <div class="panel panel-default">
                        <div class="panel-heading">
                            Deduction Settings
                            <div class="pull-right">
                    <asp:DropDownList ID="ddl_branch" CssClass="form-control" runat="server" 
                        style="margin-left: 0px; width:auto" AutoPostBack="True">
                    </asp:DropDownList>
                            </div>
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                            </asp:ToolkitScriptManager>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>

                            <div align="center" id="morris-area-chart" style="width: 80%">                
                               <table class="table table-striped table-bordered table-hover">
                                    <tbody>
                                    <tr><td style="width: 720px">Department&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                        <td style="width: 720px">
                                            <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" 
                                                CssClass="form-control" 
                                                onselectedindexchanged="ddlDepartment_SelectedIndexChanged" Width="250">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 720px">
                                            Amount</td>
                                        <td>
                                            <asp:TextBox ID="txt_amount" CssClass="form-control" runat="server" AutoPostBack="True" ontextchanged="txt_amount_TextChanged"></asp:TextBox>
                                        </td>
                                        </tr>
                                        <tr>
                                            <td >Deduction Code</td>
                                            <td>
                                                <asp:DropDownList ID="ddl_deductioncode" runat="server" AutoPostBack="True" 
                                                    CssClass="form-control"  Width="250" 
                                                    onselectedindexchanged="ddl_deductioncode_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                Date</td>
                                            <td>
                                                <div style=" width:120px; float:left;">
                                                <asp:TextBox ID="txt_date" runat="server" CssClass="form-control" onkeyup="fn_date(event,this.id);" Width="120px"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txt_date">
                                                </asp:CalendarExtender>
                                                    </div>
                                                <div style=" width:30px; float:left;  margin-left:10px; margin-top:3px;">                                                
                                                <asp:Image ID="Image3" runat="server" Text="" Width="25px"  ImageUrl="~/Images/calendaricon.png" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" >
                                            <div style=" width:800px; height:400px; overflow: auto;">
                    
                    <asp:GridView ID="GridView1" runat="server" DataKeyNames="pn_EmployeeID"  AllowSorting="True" CssClass="table table-hover table-striped"
            AutoGenerateColumns="False" ShowFooter="True">                          
            <Columns>
            
            
                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Employee Code" HeaderStyle-HorizontalAlign="Left">
                
                    <ItemTemplate>
                        <asp:Label ID="lbl_empcode" runat="server" Text='<%# Eval("EmployeeCode") %>'></asp:Label>
                    </ItemTemplate>
                    
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Employee Name"  HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lbl_empname" runat="server" Text='<%# Eval("Employee_First_Name") %>'></asp:Label>
                    </ItemTemplate> 
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>


                <asp:TemplateField ItemStyle-HorizontalAlign="Left"  HeaderText="Amount"  HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:TextBox ID="txt_contribution" runat="server" Text="" CssClass="form-control" onkeypress="return onlyNumbersWithDot(event);" ></asp:TextBox>                         
                        <%--<asp:TextBox ID="TextBox1" runat="server" Text="" OnTextChanged="txt_contribution_TextChanged" AutoPostBack="true" CssClass="form-control" onkeypress="return onlyNumbersWithDot(event);" ></asp:TextBox>                         --%>
                    </ItemTemplate>                  

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>          
                                            
            </Columns>
            <EmptyDataTemplate>
            <asp:Label ID="lblempty" Text="No Records" runat="server">
            </asp:Label>             
            </EmptyDataTemplate>
        </asp:GridView>                    
                 </div>
                 </td>
                   </tr>
                   
                    <tr>
                    <td align="center" colspan="4" >
                        <asp:Button ID="Gbtn_Save" runat="server" Text="Save" class="btn btn-success" 
                                            onclick="Gbtn_Save_Click" />
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

