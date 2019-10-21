<%@ Page MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="LoanEntry.aspx.cs" Inherits="Bank_Loan_Default" Title="Loan Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    &nbsp;&nbsp;&nbsp;
    <script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>
<script language="javascript" type="text/javascript" src="../datecheck.js"></script>


<script language="javascript" type="text/javascript">
    
    function isNumber(evt) {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
    function IsEmpty()
   {
     if (document.getElementById("<%=Txt_loan_id.ClientID%>").value=="")
      {
         alert("Enter the Loan Id");
         document.getElementById("<%=Txt_loan_id.ClientID%>").focus();
         return false;
      }
      
     else if(document.getElementById("<%=Txt_loan_sdate.ClientID%>").value=="")
     {
        alert("Enter the loan Sanction date");
        document.getElementById("<%=Txt_loan_sdate.ClientID%>").focus();
        return false;
     }
     
     else if(document.getElementById("<%=Txt_empnam.ClientID%>").value=="")
     {
        alert("Enter the employee name");
        document.getElementById("<%=Txt_empnam.ClientID%>").focus();
        return false;   
     }
   
     else if(document.getElementById("<%=Txt_loan_effdate.ClientID %>").value=="")
     {
       alert("Enter the Loan Effective Date");
       document.getElementById("<%=Txt_loan_effdate.ClientID%>").focus();
       return false;  
     }     
   
   else
    {
      return true;
    }
  }
  
  function check()
  {
  if(document.getElementById("<%=Txt_loan_amt.ClientID %>").value=="")
  {
  alert("Enter the Amount");
  document.getElementById("<%=Txt_loan_amt.ClientID%>").focus();
  return false;
  }
  
  else if(document.getElementById("<%=Txt_interest.ClientID %>").value=="")
  {
  alert("Enter the Interest");
  document.getElementById("<%=Txt_interest.ClientID %>").focus();
  return false;
  } 
  
  else
  {
  return true;
  }
  } 
  
  function round(event,txtid)
  {
   var a =document.getElementById("<%=Txt_loan_amt.ClientID %>").value;
   /*document.aspnetForm.ctl00$ContentPlaceHolder1$txtid.value;*/
   var b;
   b.innerHTML = a.toFixed(2);
   return b;
  } 
//  function myFunction()
//{
//var num = 10000;
//var x = document.getElementById("demo");
//x.innerHTML=num.toFixed(2);
//}

    </script>
    

    <div><h3 class="page-header">Loan Entry</h3></div>
    <div class=pull-right><asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" CssClass="form-control"
                    OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged" >
                </asp:DropDownList></div>
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div style="width: 80%">
                    <table width="100%" class="table table-striped table-bordered table-hover">
                        
                        <tr>
                            <td>
                                Loan Process</td>
                            <td >
                                <asp:RadioButtonList ID="rd_loan_process" runat="server" 
                                    atomicselection="false" AutoPostBack="True" 
                                    Font-Names="Calibri" onselectedindexchanged="rd_loan_process_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value=" Flat ">By Flat Rate</asp:ListItem>
                                    <asp:ListItem Value=" Diminishing">By Diminishing Rate</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td >
                                Calculation</td>
                            <td colspan="3" >
                                <asp:RadioButtonList ID="rd_calc" runat="server" AutoPostBack="True" 
                                    Font-Names="Calibri">
                                    <asp:ListItem Value="Month">Monthly Based</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="Amount">Amount Based</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Department</td>
                            <td>
                                <span style="color: #800000">
                                <asp:DropDownList ID="ddl_department" runat="server" AutoPostBack="True" class="form-control" 
                                    onselectedindexchanged="ddl_department_SelectedIndexChanged" Width="200px">
                                </asp:DropDownList>
                                </span>
                            </td>
                            <td>
                                Employee Name</td>
                            <td colspan="3">
                                <asp:TextBox ID="Txt_empnam" runat="server" CssClass="form-control" 
                                    onkeypress="AllowOnlyText3();" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Employee ID</td>
                            <td>
                                <asp:DropDownList ID="ddl_empcode" runat="server" AutoPostBack="True" 
                                    CssClass="form-control"  Width="200px"
                                    onselectedindexchanged="ddl_empcode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                Loan ID</td>
                            <td>
                                <asp:TextBox ID="Txt_loan_id" runat="server" AutoPostBack="True" 
                                    ontextchanged="Txt_loan_id_TextChanged" CssClass="form-control"/>
                            </td>
                            <td>
                            <asp:LinkButton ID="Img_btn" runat="server" 
                             CssClass="btn btn-circle btn-default glyphicon glyphicon-search" 
                                    onclick="Img_btn_Click"></asp:LinkButton>

                            </td>
                            <td>
                                <asp:ListBox ID="ListBox1" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ListBox1_SelectedIndexChanged"></asp:ListBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Loan Name</td>
                            <td>
                                <asp:DropDownList ID="ddl_Loancode" runat="server" CssClass="form-control" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btn_LoanDetails" runat="server" CssClass="btn btn-info" 
                                     OnClick="btn_LoanDetails_Click" Text="Loan Details" />
                            </td>
                            <td colspan="3">
                                <asp:Button ID="btn_EmpDetails" runat="server" CssClass="btn btn-info" 
                                    Enabled="False" OnClick="btn_EmpDetails_Click" Text=" Emp Details" Visible="False"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Sanction Date</td>
                            <td>
                            <div style=" width:150px; float:left;">
                                <asp:TextBox ID="Txt_loan_sdate" runat="server" 
                                    onkeyup="fn_date(event,this.id);" CssClass="form-control" Width="150px" />
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="Txt_loan_sdate" Format="dd/MM/yyyy">
                                </asp:CalendarExtender>
                             </div>

                              <div style=" width:25px; float:left;  margin-left:10px; margin-top:3px;">                                                
                                                    <asp:Image ID="Image1" runat="server"  
                                                        Text="" Width="25px" 
                                                        ImageUrl="~/Images/calendaricon.png" />
                             </div>

                            </td>
                            <td>
                                Effective Date</td>
                            <td colspan="3">
                            <div style=" width:150px; float:left;">
                                <asp:TextBox ID="Txt_loan_effdate" runat="server" AutoPostBack="True" 
                                    CssClass="form-control" maxlength="10" onkeyup="fn_date(event,this.id);" 
                                    ontextchanged="Txt_loan_effdate_TextChanged" Width="150px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="Txt_loan_effdate" Format="dd/MM/yyyy" >
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
                            <td>
                                Amount</td>
                            <td>
                                <asp:TextBox ID="Txt_loan_amt" runat="server" AutoPostBack="True" 
                                    CssClass="form-control" onkeydown="AllowOnlyNumeric1(event);" 
                                    ontextchanged="Txt_loan_amt_TextChanged"></asp:TextBox>
                            </td>
                            <td>
                                Interest (Annum)</td>
                            <td colspan="3">
                                <asp:TextBox ID="Txt_interest" runat="server" AutoPostBack="True" CssClass="form-control"
                                    onkeydown="AllowOnlyNumeric1(event);" ontextchanged="Txt_interest_TextChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Interest Amount</td>
                            <td>
                                <asp:TextBox ID="Txt_interest_amt" runat="server" AutoPostBack="True" 
                                    onkeydown="AllowOnlyNumeric1(event);" CssClass="form-control"
                                    ontextchanged="Txt_interest_amt_TextChanged"></asp:TextBox>
                            </td>
                            <td>
                                No of Deductions</td>
                            <td colspan="3">
                                <asp:TextBox ID="Txt_no_dedu" runat="server" AutoPostBack="True" onkeypress="return isNumber(event)" onkeydown="AllowOnlyNumeric1(event);" 
                                    ontextchanged="Txt_no_dedu_TextChanged" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Current Balance</td>
                            <td>
                                <asp:TextBox ID="Txt_cur_bal" runat="server" AutoPostBack="True" 
                                    CssClass="form-control" onkeypress="return isNumber(event)" onkeydown="AllowOnlyNumeric1(event);"
                                    ontextchanged="Txt_cur_bal_TextChanged"></asp:TextBox>
                            </td>
                            <td>
                                Deduction Amount</td>
                            <td colspan="3">
                                <asp:TextBox ID="Txt_dedu_mon" runat="server" AutoPostBack="True" onkeypress="return isNumber(event)" onkeydown="AllowOnlyNumeric1(event);"
                                    ontextchanged="Txt_dedu_mon_TextChanged" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Remaining Months</td>
                            <td>
                                <asp:TextBox ID="Txt_rem_count" runat="server" CssClass="form-control" onkeypress="return isNumber(event)" onkeydown="AllowOnlyNumeric1(event);" MaxLength="5"></asp:TextBox>
                            </td>
                            <td>
                                Remarks</td>
                            <td colspan="3">
                                <asp:TextBox ID="Txt_comments" runat="server" CssClass="form-control" onkeypress="AllowOnlyText3();"
                                   ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="right">
                                <asp:Button ID="Btn_save" runat="server" CssClass="btn btn-success"  onclick="Btn_save_Click" 
                                    OnClientClick="return check();" Text="Save"/>
                                <asp:Button ID="Btn_delete" runat="server" CssClass="btn btn-danger" onclick="Btn_delete_Click" 
                                    Text="Delete"/>
                                <asp:Button ID="Btn_cancel" runat="server"
                                    CssClass="btn btn-warning" onclick="Btn_cancel_Click" Text="Cancel" 
                                    Visible="False"/>
                                <asp:Button ID="Btn_clear" runat="server" 
                                    CssClass="btn btn-info"  onclick="Btn_clear_Click" Text="Clear"/>
                               </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="6">
                                <asp:GridView ID="grid_loan" runat="server" AllowSorting="True" CssClass="table table-striped table-bordered table-hover"
                                    AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Loan Appid">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_lappid" runat="server" CssClass="form-control"
                                                    Text='<%# Bind("loanappid") %>' Width="70px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_lappid" runat="server" Text='<%# Eval("loanappid") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Loan Name">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddl_lid" runat="server" CssClass="form-control" Width="80px">
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                                                    ConnectionString="<%$ ConnectionStrings:Hesperus_HrmsConnectionString24 %>" 
                                                    SelectCommand="SELECT [v_LoanName], [pn_LoanID] FROM [paym_Loan]">
                                                </asp:SqlDataSource>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="grd_loan" runat="server" Enabled="false" CssClass="form-control"
                                                    Width="80px">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Loan Process">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_lprocess" runat="server" CssClass="form-control"
                                                    Text='<%# Bind("loanprocess") %>' Width="70px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_lprocess" runat="server" Text='<%# Eval("loanprocess") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Emp Name">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddl_emp_name" runat="server" 
                                                    DataSourceID="SqlDataSource4" DataTextField="Employee_First_Name" 
                                                    DataValueField="pn_EmployeeID" CssClass="form-control" Width="80px">
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                                                    ConnectionString="<%$ ConnectionStrings:Hesperus_HrmsConnectionString26 %>" 
                                                    SelectCommand="SELECT [pn_EmployeeID], [Employee_First_Name] FROM [paym_Employee] WHERE ([pn_BranchID] = @pn_BranchID) ORDER BY [pn_EmployeeID]">
                                                    <SelectParameters>
                                                        <asp:SessionParameter DefaultValue="0" Name="pn_BranchID" 
                                                            SessionField="Login_temp_BranchID" Type="Int32" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="grd_employee" runat="server" CssClass="form-control" Width="80px">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="App Date">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_lappdat" runat="server" CssClass="form-control"
                                                    Text='<%Eval("strdateapplication","{0:dd/MM/yyyy}") %>' Width="70px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_lappdat" runat="server" 
                                                    Text='<%#Eval("strdateapplication","{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Eff Date">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_effdat" runat="server" CssClass="form-control"
                                                    Text='<%# Bind("streffectivedate","{0:dd/MM/yyyy}") %>' Width="70px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_effdat" runat="server" 
                                                    Text='<%# Eval("streffectivedate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Loan Amount">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_lamt" runat="server" CssClass="form-control"
                                                    Text='<%# Bind("Amount") %>' Width="70px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_lamt" runat="server" Text='<%# Eval("amount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Loan Interest">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_linterest" runat="server" CssClass="form-control"
                                                    Text='<%# Bind("loaninterest") %>' Width="70px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_linterest" runat="server" 
                                                    Text='<%# Eval("loaninterest") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ins Amount">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_insamt" runat="server" CssClass="form-control"
                                                    Text='<%# Bind("installmentcount") %>' Width="70px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_insamt" runat="server" 
                                                    Text='<%# Eval("installmentcount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Count">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_tcount" runat="server" CssClass="form-control" Width="70px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_tcount" runat="server" Text="Label"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowEditButton="True" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                     </table>   
                     </div>
    </ContentTemplate>
    </asp:UpdatePanel>


    </asp:Content>
