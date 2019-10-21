<%@ Page Language="C#" MasterPageFile="~/HRMS.master"
    AutoEventWireup="true" CodeFile="Bonus_Increment_New.aspx.cs" Inherits="Hrms_Master_Default"
    Title="Welcome to HRMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../../Scripts/Datavalid.js"></script>
    
     <script type="text/javascript" language="javascript">
         function ConfirmOnDelete() {
             if (confirm("Are you sure to delete") == true)
                 return true;
             else
                 return false;
         }
    </script>
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
            if (charCode > 31 && (charCode < 48 || charCode > 58))
                return false;
            return true;
        }
    
    function show_message()
    {
        alert("Bonus Name Already Exist");
    }
    function show_Error1()
    {
        alert("Enter FromPoint Value");
    }
    function show_Error2()
    {
        alert("Enter ToPoint Value");
    }  
   
    function show_Error3()
    {
        alert("Enter Increment Point");
    }    
    function fnSave()
    {   
                        if(document.aspnetForm.ctl00$ContentPlaceHolder1$txtPointsAmount.value == "")
                            {
                                alert("Enter Points Amount");            
                                return false;
                            }   
                        else
                            {
                                return true;  
                            }

                        }

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

    <div class="row">
                <div class="col-lg-12">
                    <h2 class="page-header">Bonus</h2>
                </div>
                <!-- /.col-lg-12 -->
            </div>

            <div class="panel panel-default">
                        <div class="panel-heading">
                            Bonus Settings
                            <div class="pull-right">
                                <asp:DropDownList ID="ddl_branch" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddl_branch_SelectedIndexChanged" Width="115px">
                                </asp:DropDownList>
                            </div>
                            <asp:SqlDataSource ID="SqlDataSource_grade" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:connectionstring %>" 
                                
                                SelectCommand="SELECT [v_GradeName] FROM [paym_Grade] WHERE ([BranchID] = @BranchID)">
                                <SelectParameters>
                                    <asp:SessionParameter Name="BranchID" SessionField="Login_temp_BranchID" 
                                        Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
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
                                        <tr>
                                            <td >Grade</td>
                                            <td>
                                                <asp:DropDownList ID="ddl_grade" runat="server"
                                                    DataSourceID="SqlDataSource_grade" DataTextField="v_GradeName" 
                                                    DataValueField="v_GradeName" CssClass="form-control">
                                                </asp:DropDownList>
                                                </td>
                                            <td>
                                                Bonus Type</td>
                                            <td>
                                                <asp:DropDownList ID="ddl_Inctype" runat="server" AutoPostBack="True" 
                                                    CssClass="form-control" onselectedindexchanged="ddl_Inctype_SelectedIndexChanged" >
                                                    <asp:ListItem>Amount</asp:ListItem>
                                                    <asp:ListItem>Percentage</asp:ListItem>
                                                    <asp:ListItem>Whichever is higher</asp:ListItem>
                                                    <asp:ListItem>Whichever is lower</asp:ListItem>
                                                    <asp:ListItem>Average</asp:ListItem>
                                                </asp:DropDownList>
                                                </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_increment" runat="server" Text="Value"></asp:Label>
                                            </td>
                                            <td>
                                                <input runat="server" id="txtPointsAmount" maxlength="7" class="form-control" onkeydown="AllowOnlyNumeric1(event);" />
                                            </td>
                                            <td>
                                                Bonus Code</td>
                                            <td>
                                                <input runat="server" id="txt_formulaName" class="form-control" maxlength="15"  />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_increment1" runat="server" Text="Amount"></asp:Label>
                                            </td>
                                            <td>
                                                <input id="txtpointamount1" class="form-control" runat="server" maxlength="10" onkeypress="return onlyNumbersWithDot(event);"
 />
                                            </td>
                                            <td>
                                                <span style="color: #5B5B5B">
                                                <asp:Label ID="Label7" runat="server" ForeColor="#FF3300"></asp:Label>
                                                </span></td>
                                            <td>
                                                <asp:Button ID="btn_Add" runat="server" class="btn btn-success" Text="Save" 
                                                    onclick="btn_Add_Click" />
                                                &nbsp;<asp:Button ID="btn_Edit" runat="server" class="btn btn-success" 
                                                    Text="Edit" onclick="btn_Edit_Click" />
                                                
                                                &nbsp;<asp:Button ID="btn_cancel" runat="server" class="btn btn-warning" 
                                                    Text="Cancel" onclick="btn_cancel_Click" />
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" > 
                                                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
                                                    AutoGenerateColumns="False" onrowdatabound="GridView1_RowDataBound" onrowdeleting="GridView1_RowDeleting" 
                                                    ShowFooter="True" CssClass="table table-hover table-striped" 
                                                    GridLines="None" onrowcancelingedit="GridView1_RowCancelingEdit" 
                                                    onrowediting="GridView1_RowEditing" onrowupdating="GridView1_RowUpdating" 
                                                    >                                                    
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_id" runat="server" Text='<%# Eval("pn_BonusID") %>' 
                                                                    Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                            <ControlStyle Width="50" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Grade" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Grade") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                            <asp:DropDownList ID="ddlGrade" runat="server" CssClass="form-control" Width="150px">
                                                             </asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
  
                                                        <asp:TemplateField HeaderText="Bonus by" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("Bonus_Type") %>'></asp:Label>
                                                            </ItemTemplate>
                                                             <EditItemTemplate>
                                                            <asp:DropDownList ID="ddlBonusType" runat="server" CssClass="form-control" Width="150px">                                    
                                                             </asp:DropDownList> 
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bonus Value" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("Bonus") %>'></asp:Label>
                                                            </ItemTemplate>
                                                             <EditItemTemplate>
                                                            <asp:TextBox id="txtBonus" type="text" runat="server"  Width="150px" CssClass="form-control" Text='<%# Bind("Bonus") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bonus Code" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("formula_name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                             <EditItemTemplate>
                                                            <asp:TextBox id="txtBonusCode" type="text" runat="server"  Width="150px" CssClass="form-control" Text='<%# Bind("formula_name") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowDeleteButton="true" ButtonType="Image" EditImageUrl="~/Images/edit_icon.png" CancelImageUrl="~/Images/delete_icon.jpg" UpdateImageUrl="~/Images/save_icon.jpg" DeleteImageUrl="~/Images/delete_icon.jpg" ShowEditButton="True" />
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
