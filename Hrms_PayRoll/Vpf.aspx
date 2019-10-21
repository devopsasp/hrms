<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="Vpf.aspx.cs" Inherits="Bank_Loan_Default" Title="PT Details" %>
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
                    <h2 class="page-header">Voluntary Provident Fund</h2>
                </div>
                <!-- /.col-lg-12 -->
            </div>

            <div class="panel panel-default">
                        <div class="panel-heading">
                            VPF Settings
                            <div class="pull-right">
                    <asp:DropDownList ID="ddl_branch" runat="server" 
                        style="margin-left: 0px; width:auto" AutoPostBack="True" 
                        onselectedindexchanged="ddl_branch_SelectedIndexChanged">
                    </asp:DropDownList>
                            </div>
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>

                            <div align="center" id="morris-area-chart" style="width: 80%">                
                               <table class="table table-striped table-bordered table-hover">
                                    <tbody>
                                    <tr><td>Department<asp:DropDownList 
                                            ID="ddlDepartment" runat="server" Width="250"                                             
                                            AutoPostBack="True" CssClass="form-control"
                                            onselectedindexchanged="ddlDepartment_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td></tr>
                                        <tr>
                                            <td >Monthly Contribution in :
                                                <asp:RadioButtonList ID="Radio_calc" runat="server" 
                            AppendDataBoundItems="True" RepeatDirection="Horizontal" AutoPostBack="True" 
                            onselectedindexchanged="Radio_calc_SelectedIndexChanged" Font-Bold="False">
                            <asp:ListItem Value="Amount" Selected="True">By Amount wise</asp:ListItem>
                            <asp:ListItem Value="Percentage">By Percentage wise</asp:ListItem>
                        </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td >
                                            <div style=" width:800px; height:400px; overflow: auto;">
                    
                    <asp:GridView ID="GridView1" runat="server"  AllowSorting="True" CssClass="table table-hover table-striped"
            AutoGenerateColumns="False" ShowFooter="True" onselectedindexchanged="GridView1_SelectedIndexChanged">                          
            <Columns>
            
            
                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Employee Code" HeaderStyle-HorizontalAlign="Left">
                
                    <ItemTemplate>
                        <asp:Label ID="lbl_empcode" runat="server" Text='<%# Eval("EmployeeCode") %>'></asp:Label>
                    </ItemTemplate>
                    
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Employee Name"  HeaderStyle-HorizontalAlign="Left">
                    <FooterTemplate>
                        <asp:Label ID="lbl_empall" runat="server"
                            Text="All Employees" Font-Size="Small"></asp:Label>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbl_empname" runat="server" Text='<%# Eval("Employee_First_Name") %>'></asp:Label>
                    </ItemTemplate> 
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>


                <asp:TemplateField ItemStyle-HorizontalAlign="Left"  HeaderText="Monthly Contribution"  HeaderStyle-HorizontalAlign="Left">
                    <FooterTemplate>
                        <asp:TextBox ID="txt_empall" runat="server" AutoPostBack="True" 
                            ontextchanged="txt_empall_TextChanged" CssClass="form-control"></asp:TextBox>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:TextBox ID="txt_contribution" runat="server" Text="" OnTextChanged="txt_contribution_TextChanged" AutoPostBack="true" CssClass="form-control" onkeypress="return onlyNumbersWithDot(event);" ></asp:TextBox>                         
                    </ItemTemplate>                  

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>

                                   <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Salary From"  HeaderStyle-HorizontalAlign="Left">
                                    <FooterTemplate>
                                        
                                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_sal" runat="server" Enabled="false" CssClass="form-control">
                            <asp:ListItem Selected="True" Value="Basic Salary">Basic Salary</asp:ListItem>
                            <asp:ListItem Value="Gross Salary">Gross Salary</asp:ListItem>
                        </asp:DropDownList>
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
                    <td align="center" >
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

