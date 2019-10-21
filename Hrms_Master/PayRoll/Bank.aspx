<%@ Page Language="C#" MasterPageFile="~/HRMS.master"
    AutoEventWireup="true" CodeFile="Bank.aspx.cs" Inherits="Bank_Loan_Default" Title="Welcome to HRMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="../../Scripts/Datavalid.js"></script>
    <script type="text/javascript">
        window.onload = function () {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lbl_Error.ClientID %>").innerHTML = "";
            }, seconds * 1000);
        };
</script>
     <link href="../../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
    function show_message()
    {
        alert("Bank Code Already Exist");
    }
    
    function show_message1()
    {
        alert("Bank Name Already Exist");
    }
    
    function show_Error()
    {
        alert("Enter Value");
    }
    
    function fnSave()
    {   
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_code.value == "")
        {
            alert("Enter Bank Code");
            aspnetForm.ctl00$ContentPlaceHolder1$txt_code.focus();
            return false;
        }                        
        else
        { 
              
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_name.value == "")
        {
            alert("Enter Bank Name");
            aspnetForm.ctl00$ContentPlaceHolder1$txt_name.focus();
            return false;
        }                        
        else
        { 
              return true;  
        }
        
        }
    }    
        
    </script>
    <div><h2 class="page-header">Company Bank Details</h2></div>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td id="tdComposeHeader" valign="top" align="center">
                <table cellpadding="2" cellspacing="2" width="100%"  class="table table-striped table-bordered table-hover">
                    <tr align="center" valign="top">
                        <td colspan="6">
                            <asp:Label ID="lbl_Error" runat="server" ForeColor="Red" CssClass="Error" 
                                Font-Names="Calibri" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                   
                    <tr>
                        <td >
                           Bank Code <span style="color: #FF0000">*</span></td>
                       
                        <td>
                            <input id="txt_code" runat="server" class="form-control" 
                                maxlength="20" /></td>
                        <td>Branch Name
                            </span>
                        &nbsp;<span style="color: #FF0000">*</span></td>
                        <td>
                            <input id="txt_branchName" class="form-control"  runat="server" onkeypress="AllowOnlyText3();"
                            onkeydown="AllowOnlyText1(event);" /></td>
                        <td>
                            MICR Code 
                        </td>
                        <td>
                            <input id="txt_micr" class="form-control"  onkeydown="AllowOnlyNumeric1(event);" runat="server"  
                                maxlength="20" /></td>
                    </tr>
                    <tr>
                        <td >Bank Name <span style="color: #FF0000">*</span></td>
                        <td>
                            <input id="txt_name" runat="server" class="form-control" onkeypress="AllowOnlyText3();"  
                                onkeydown="AllowOnlyText1(event);" /></td>
                        <td>Account Type&nbsp; <span style="color: #FF0000">*</span></td>
                        <td>
                            <asp:DropDownList id="txt_actype"  runat="server"  maxlength="20" CssClass="form-control" 
                                Width="90%"  >
                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Current" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Savings" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Salary" Value="3"></asp:ListItem>
                                <asp:ListItem Text="CC" Value="4"></asp:ListItem>
                                <asp:ListItem Text="Over Draft" Value="5"></asp:ListItem>
                                <asp:ListItem Text="Others" Value="6"></asp:ListItem>
                                </asp:DropDownList></td>
                        <td>
                            IFSC Code&nbsp; <span style="color: #FF0000">*</span></td>
                        <td>
                            <input id="txt_ifsc" class="form-control"  runat="server"  
                                maxlength="20" /></td>
                    </tr>
                    <tr>
                        <td >
                            Address</td>
                        <td >
                            <asp:TextBox id="txt_addr" CssClass="form-control"  
                                
                               
                                runat="server" maxlength="200" TextMode="MultiLine" ></asp:TextBox></td>
                        <td >Other
                            Info</td>
                        <td >
                            <asp:TextBox id="txt_other"  CssClass="form-control"  
                                
                                runat="server" maxlength="200" TextMode="MultiLine"  ></asp:TextBox></td>
                        <td colspan="2" >
                            <b>
                            <asp:Button  ID="btn_save" runat="server" CssClass="btn btn-success"
                                OnClientClick="return fnSave();" Text="Add" OnClick="btn_save_Click" 
                                ImageAlign="AbsMiddle" />
                            </b>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:GridView ID="GridView1"  runat="server" AllowSorting="True" 
            AutoGenerateColumns="False"
            Width="100%" 
            onrowcommand="GridView1_RowCommand" CellPadding="4" 
          
            onrowdeleting="GridView1_RowDeleting" 
            onrowdatabound="GridView1_RowDataBound" onrowediting="GridView1_RowEditing" 
            onselectedindexchanged="GridView1_SelectedIndexChanged" HorizontalAlign="Left" 
                                    onrowupdating="GridView1_RowUpdating" 
                                    onrowcancelingedit="GridView1_RowCancelingEdit"  CssClass="table table-striped table-bordered table-hover"
                                GridLines="None" ShowFooter="True">
                           
                           <RowStyle/>
            <Columns>
            
                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Bank Code" HeaderStyle-HorizontalAlign="Left">
                
                    <ItemTemplate>
                        <asp:Label ID="lbl_bankcode" runat="server" Text='<%# Eval("v_BankCode") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:Label ID="lbl_bankcode_edit" runat="server" Text='<%# Bind("v_BankCode") %>' 
                           ></asp:Label>
                    </EditItemTemplate>


<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Bank Name"  HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lbl_bankname" runat="server" Text='<%# Eval("v_BankName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="txt_bankname_edit" runat="server" Text='<%# Bind("v_BankName") %>'  CssClass="form-control"
                           ></asp:TextBox>
                    </EditItemTemplate>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Branch Name"  HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lbl_branchname" runat="server" Text='<%# Eval("Branch_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="txt_branchname_edit" runat="server" Text='<%# Bind("Branch_Name") %>' CssClass="form-control"
                    ></asp:TextBox>
                    </EditItemTemplate>
                    
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>


                <asp:TemplateField ItemStyle-HorizontalAlign="Left"  HeaderText="Account Type"  HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lbl_accounttype" runat="server" Text='<%# Eval("Account_Type") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="txt_accounttype_edit" runat="server" Text='<%# Bind("Account_Type") %>' CssClass="form-control"
                    ></asp:TextBox>
                    </EditItemTemplate>

<%--<ControlStyle Width="125px"></ControlStyle>--%>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Left"  HeaderText="MICR Code"  HeaderStyle-HorizontalAlign="Left">
                     <ItemTemplate>
                        <asp:Label ID="lbl_micrcode" runat="server" Text='<%# Eval("Micr_Code") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="txt_micrcode_edit" runat="server" Text='<%# Bind("Micr_Code") %>' CssClass="form-control"
                    ></asp:TextBox>
                    </EditItemTemplate>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left"  HeaderText="IFSC Code"  HeaderStyle-HorizontalAlign="Left">
                     <ItemTemplate>
                        <asp:Label ID="lbl_ifsccode" runat="server" Text='<%# Eval("Ifsc_Code") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="txt_ifsccode_edit" runat="server" Text='<%# Bind("Ifsc_Code") %>' CssClass="form-control"
                    ></asp:TextBox>
                    </EditItemTemplate>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left"  HeaderText="Address"  HeaderStyle-HorizontalAlign="Left">
                     <ItemTemplate>
                        <asp:Label ID="lbl_address" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="txt_address_edit" runat="server" Text='<%# Bind("Address") %>' CssClass="form-control"
                    ></asp:TextBox>
                    </EditItemTemplate>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left"  HeaderText="Other Info"  HeaderStyle-HorizontalAlign="Left">
                     <ItemTemplate>
                        <asp:Label ID="lbl_others" runat="server" Text='<%# Eval("Others") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="txt_others_edit" runat="server" Text='<%# Bind("Others") %>' CssClass="form-control"
                    ></asp:TextBox>
                    </EditItemTemplate>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>           
                
              <asp:CommandField ItemStyle-Width="25px" EditImageUrl="~/Images/edit_icon.png" ButtonType="Image" UpdateImageUrl="~/Images/save_icon.jpg" ShowEditButton="True" CancelImageUrl="~/Images/cancel.PNG" />
            <asp:CommandField ShowDeleteButton="True" ItemStyle-Width="25px" ButtonType="Image" DeleteImageUrl="~/Images/delete_icon.jpg" />
            </Columns>
                              
            <EmptyDataTemplate>
            <asp:Label ID="lblempty" Text="No Records" runat="server">
            </asp:Label> 
            
            </EmptyDataTemplate>
        </asp:GridView>
                        </td>
                        
                    </tr>
                </table>
    </table>
</asp:Content>
