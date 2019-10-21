<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="pt.aspx.cs" Inherits="Bank_Loan_Default" Title="PT Details" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>
<script language="javascript" type="text/javascript" src="../datecheck.js"></script>
<script src="../JQuery/jquery-2.1.3.js" type="text/javascript"></script>
<%--<script type="text/javascript">
    $(document).ready(

    /* This is the function that will get executed after the DOM is fully loaded */
  function () {
      $("#txt_date").datepicker({
          changeMonth: true, //this option for allowing user to select month
          changeYear: true //this option for allowing user to select from year range
      });
  }

);
</script>--%>
<script language="javascript" type="text/javascript">
    
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
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    <div class="row">
                <div class="col-lg-12">
                    <h2 class="page-header">Professional Tax</h2>
                </div>
                <!-- /.col-lg-12 -->
            </div>


            <div class="panel panel-default">
                        <div class="panel-heading">
                            PT Settings
                            <div class="pull-right">
                                <asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" CssClass="InputDefaultStyle"
                    OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged">
                </asp:DropDownList>
                            </div>
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>--%>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>

                            <div align="center" id="morris-area-chart" style="width: 100%">
                
                               <table class="table table-striped table-bordered table-hover">
                                    <tbody>
                                        <tr>
                                            <td >From Value</td>
                                            <td>
                                                <input type="text" runat="server" id="txt_fromamt" class="form-control" 
                                                    onkeydown="AllowOnlyNumeric1(event);" />
                                            </td>
                                            <td>
                                                To Value</td>
                                            <td>
                                                <input type="text" runat="server" id="txt_toamt" class="form-control" 
                                                    onkeydown="AllowOnlyNumeric1(event);" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Effective Date</td>
                                            <td>
                                            <div style=" width:88%; float:left;">
                                                <asp:TextBox ID="txt_date" runat="server" class="form-control" 
                                                    onkeyup="fn_date(event,this.id);" maxlength="10" />                                               
                                                <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txt_date" Format="dd/MM/yyyy" runat="server">
                                                </asp:CalendarExtender>
                                                </div>
                                                <div style=" width:25px; float:left;  margin-left:10px; margin-top:3px;">                                                
                                                    <asp:Image ID="Image1" runat="server"  
                                                        Text="" Width="25px" 
                                                        ImageUrl="~/Images/calendaricon.png" />
                                                </div>
                                            </td>
                                            <td>
                                                Half Yearly Amount</td>
                                            <td>
                                                <input type="text" runat="server" id="txt_ptaxamount" class="form-control"
                                                    onkeydown="AllowOnlyNumeric1(event);" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="lbl_branch" runat="server" Font-Italic="True" 
                                                    Font-Names="Calibri" Font-Size="Small" Text="Label"></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <asp:Button ID="btn_save" runat="server" class="btn btn-success" 
                                                    onclick="btn_save_Click" Text="Save" />
                                                &nbsp;<asp:Button ID="btn_reset" runat="server" class="btn btn-warning" 
                                                    onclick="btn_reset_Click" Text="Reset" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" >
                    
                                                <asp:GridView ID="grid_pt" runat="server" AutoGenerateColumns="False" DataKeyNames="PTcount"
                                                    OnRowEditing="edit" OnRowUpdating="update" CssClass="table table-hover table-striped" GridLines="Both">
                                                   
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <table>
                                                                    <tbody>
                                                                        <tr>
                                                                            <td align="left" style="width: 120px;">
                                                                                From Amount</td>
                                                                            <td align="left" style="width: 120px;">
                                                                                To Amount</td>
                                                                            <td align="left" style="width: 120px;">
                                                                                HalfYearly</td>
                                                                            <td align="left" style="width: 120px;">
                                                                                Monthly</td>
                                                                            <td align="left" style="width: 120px;">
                                                                                Quaterly</td>
                                                                            <td align="left" style="width: 120px;">
                                                                                Annually</td>
                                                                            <%--<td align="left" style="border-style:groove;width: 120px;">
                                                                            </td>--%>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <table cellpadding="0" cellspacing="0" class="dItemListContentTable">
                                                                    <colgroup>
                                                                        <col class="dInboxContentTableCheckBoxCol"></col>
                                                                    </colgroup>
                                                                    <tbody>
                                                                        <tr>
                                                                            <td >
                                                                                <asp:TextBox ID="grd_fromamount" runat="server" Enabled="false" 
                                                                                    onkeydown="AllowOnlyNumeric1(event);" Text='<%#Eval("F_Amount")%>' 
                                                                                    CssClass="form-control" Width="120px"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="grd_toamount" runat="server" Enabled="false" 
                                                                                    onkeydown="AllowOnlyNumeric1(event);" Text='<%#Eval("T_Amount")%>' 
                                                                                    CssClass="form-control" Width="120px"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="grd_taxamount" runat="server" Enabled="false" 
                                                                                    Text='<%#Eval("ProTaxAmt")%>' CssClass="form-control" Width="120px"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="grd_monAmt" runat="server" Enabled="false" MaxLength="5" 
                                                                                    Text='<%#Eval("half_monthly")%>' CssClass="form-control" Width="120px"></asp:TextBox>
                                                                            </td>
                                                                            <td>

                                                                                <asp:TextBox ID="grd_quaterlyAmt" runat="server" Enabled="false" MaxLength="5" 
                                                                                    Text='<%#Eval("Quaterly")%>' CssClass="form-control" Width="120px"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="grd_AnnaulAmt" runat="server" Enabled="false" MaxLength="5" 
                                                                                    Text='<%#Eval("Annual")%>' CssClass="form-control" Width="120px"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="img_update" runat="server" AlternateText="" 
                                                                                    CommandName="update" ImageUrl="~/Images/edit_icon.png" Style="border: 0" />
                                                                                <asp:ImageButton ID="img_save" runat="server" AlternateText="" 
                                                                                    CommandName="edit" ImageUrl="~/Images/save_icon.jpg" Style="border: 0" 
                                                                                    Visible="false" />
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                  
                                                </asp:GridView>
                    
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="4" >
                                                &nbsp;</td>
                                        </tr>
                                    </tbody>
                                </table> 
                
                            </div>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        
                    </div>

</asp:Content>

