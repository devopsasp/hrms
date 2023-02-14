<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" 
CodeFile="Loan_cancel.aspx.cs" Inherits="Bank_Loan_Default" Title="Loan Cancel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<div><h2 class="page-header">Loan Cancel</h2></div>
<div><asp:Label  runat="server" CssClass="Error" ForeColor="Red"></asp:Label><div style="float:right;"><asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" CssClass="InputDefaultStyle form-control"
                    OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged" >
                </asp:DropDownList></div>
    </div>
  
                
                <table runat="server" id="tab_ddl" style="width:70%;"  cellspacing="1" class="table">
               
                    <tr runat="server">
                        <td runat="server">
                           Cancel Year</td>
                        <td runat="server">
                            <asp:DropDownList ID="ddl_year" runat="server" 
                                CssClass="form-control">
                            </asp:DropDownList></td>
                       <td></td>
                    </tr>
                    <tr runat="server">
                        <td runat="server" >
                           Cancel Month</td>
                        <td runat="server" >
                            <asp:DropDownList ID="ddl_month" runat="server"
                                CssClass="form-control">
                             <asp:ListItem Value="01">January</asp:ListItem>
                                            <asp:ListItem Value="02">Febraury</asp:ListItem>
                                            <asp:ListItem Value="03">March</asp:ListItem>
                                            <asp:ListItem Value="04">April</asp:ListItem>
                                            <asp:ListItem Value="05">May</asp:ListItem>
                                            <asp:ListItem Value="06">June</asp:ListItem>
                                            <asp:ListItem Value="07">July</asp:ListItem>
                                            <asp:ListItem Value="08">August</asp:ListItem>
                                            <asp:ListItem Value="09">September</asp:ListItem>
                                            <asp:ListItem Value="10">October</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">December</asp:ListItem>
                            
                            </asp:DropDownList></td>
                        <td runat="server" >
                        <asp:Button ID="btn_details" runat="server" Text="Details" 
                                OnClick="btn_details_Click"  CssClass="btn btn-warning"  /></td>
                       
                    </tr>
                    <tr id="row_emp" runat="server">
                        <td>
                            Loan Code 
                        </td>
                        <td id="col2_emp" runat="server" >
                            <asp:DropDownList ID="ddl_Loancode" runat="server" CssClass="form-control">
                            </asp:DropDownList></td>
                            <td></td>
                            
                    </tr>
                    <tr runat="server">
                        <td class="" nowrap="nowrap">
                            Employee Code:</td>
                        <td >
                        <asp:DropDownList ID="ddl_empcode" runat="server" CssClass="form-control">
                        </asp:DropDownList></td>
                        <td></td>
                       
                    </tr>
                    <tr runat="server">
                        <td runat="server" class="dComposeItemLabel" nowrap="nowrap">
                        </td>
                        <td runat="server" style="width: 294px" align="left">
                            <asp:Button ID="btn_save" runat="server" Text="Save" OnClick="btn_save_Click" 
                                CssClass="btn btn-success" /></td>
                                <td></td>
                        
                    </tr>
                    <tr><td colspan="3"></td></tr>
                </table>
            
        <%--<tr valign="top">
            <td align="right" class="tdComposeHeader" valign="top"></td>
        </tr>--%>
    
                   <asp:GridView ID="grid_loan" runat="server" AutoGenerateColumns="False" DataKeyNames="loanid"
                            OnRowEditing="edit" OnRowUpdating="update"  CssClass="table table-striped table-bordered table-hover" GridLines="None">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td style="width:25;color:Black;" class="dComposeItemLabel">
                                                    LoanName</td>
                                                <td style="width:20;color:Black;" class="dComposeItemLabel">
                                                    EmpName</td>
                                                <td style="width:32;color:Black;" class="dComposeItemLabel">
                                                    Year</td>
                                                <td style="width:32;color:Black;" class="dComposeItemLabel">
                                                    Month</td>
                                                
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table width="100%">
                                            <tr>
                                               <td>
                                                 <asp:DropDownList ID="grd_Loan" runat="server" Enabled="false"  CssClass="form-control" ></asp:DropDownList>    </td>
                                            <td>
                                                 <asp:DropDownList ID="grd_Employee" runat="server" Enabled="false"  CssClass="form-control" ></asp:DropDownList> </td>
                                            
                                             <td >
                                                 <asp:DropDownList ID="grd_Year" runat="server" Enabled="false"  CssClass="form-control" ></asp:DropDownList> </td>
                                            <td >
                                                 <asp:DropDownList ID="grd_Month" runat="server" Enabled="false"  CssClass="form-control" >
                                                  <asp:ListItem Value="01">January</asp:ListItem>
                                                 <asp:ListItem Value="02">Febraury</asp:ListItem>
                                            <asp:ListItem Value="03">March</asp:ListItem>
                                            <asp:ListItem Value="04">April</asp:ListItem>
                                            <asp:ListItem Value="05">May</asp:ListItem>
                                            <asp:ListItem Value="06">June</asp:ListItem>
                                            <asp:ListItem Value="07">July</asp:ListItem>
                                            <asp:ListItem Value="08">August</asp:ListItem>
                                            <asp:ListItem Value="09">September</asp:ListItem>
                                            <asp:ListItem Value="10">October</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">December</asp:ListItem>
                                                 </asp:DropDownList> </td> 
                                                     <td>
                                                    <asp:LinkButton ID="img_update"  runat="server" Style="border: 0"  CssClass="btn btn-circle btn-warning glyphicon glyphicon-check" 
                                                        AlternateText="" CommandName="update" />
                                                    <asp:LinkButton ID="img_save"  runat="server" Style="border: 0"  CssClass="btn btn-circle btn-success glyphicon glyphicon-save" 
                                                        AlternateText="" CommandName="edit" Visible="false" /></td>
                                                               </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                           
                        </asp:GridView>

</asp:Content>

