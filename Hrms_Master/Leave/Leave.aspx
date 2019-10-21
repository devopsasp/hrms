<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Leave.aspx.cs" Inherits="Hrms_Master_Default" Title="Welcome to HRMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="../../Scripts/Datavalid.js"></script>

    <link href="../../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function AllowAlphabet(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '46') || (keyEntry == '32') || keyEntry == '45')
                return true;
            else {                
                return false;
            } 
        } 
    function show_message() 
    {
        alert("Leave Name Already Exist");
    }
       
   
    function show_Error()
    {
        alert("Enter Value");
    }
  
    function fnSave()
    {   
            if(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_leavename.value == "")
                {
                    alert("Enter Leave Name");
                    aspnetForm.ctl00$ContentPlaceHolder1$txt_leavename.focus();
                    return false;
                }    
                             
            else
            { 
                if(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_LeaveCode.value == "")
                    {
                        alert("Enter Leave Code");
                        aspnetForm.ctl00$ContentPlaceHolder1$txt_leavename.focus();
                        return false;
                    }                     
                else
                {
                    if(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_count.value == "")
                        {
                            alert("Enter Leave Count");
                            aspnetForm.ctl00$ContentPlaceHolder1$txt_leavename.focus();
                            return false;
                        }                        
                    else
                        {
                        return true;  
                        }
                }
              
            }
    }    
    </script>

<div><h3 class="page-header">Leave Master</h3></div>
                                <asp:DropDownList ID="ddl_branch" runat="server" AutoPostBack="True" CssClass="form-control"
                                    onselectedindexchanged="ddl_branch_SelectedIndexChanged" Width="115px">
                                </asp:DropDownList>
                      <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>                                 
                <table id="tb_leave" runat="server" width="100%" class="table table-striped table-bordered table-hover">
                   
                    <tr>
                      
                        <td >
                            Leave Code</td>
                        <td >
                            <input class="form-control" runat="server" id="txt_LeaveCode" 
                                maxlength="10" tabindex="1" /></td>
                        <td>
                            Leave Options</td>
                        <td colspan="2">
                            <asp:DropDownList ID="ddl_annualleave" runat="server" 
                                onselectedindexchanged="ddl_annualleave_SelectedIndexChanged" 
                                AutoPostBack="True" CssClass="form-control" 
                                TabIndex="4">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>Carry Forward</asp:ListItem>
                                <asp:ListItem>Encashment</asp:ListItem>
                                <asp:ListItem>None</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            Leave Name</td>
                        <td >
                            <input class=" form-control" runat="server" id="txt_leavename" 
                                maxlength="30" tabindex="2" onkeypress="return AllowAlphabet(event)" /></td>
                        <td>
                            <asp:Label ID="lbl_days" runat="server" Text="Max. Days"></asp:Label>
                        </td>
                        <td colspan="2" >
                            <asp:TextBox CssClass="form-control" runat="server" id="txt_maxdays"  maxlength="2" AutoPostBack="true"  tabindex="5" OnTextChanged="txt_maxdays_TextChanged" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Leave Count (Per Year)</td>
                        <td>
                            <input class=" form-control" runat="server" id="txt_count" onkeydown="AllowOnlyNumeric1(event);" maxlength="2" tabindex="3" /></td>
                        <td>Applicable to</td>
                                <td>
                                    <asp:DropDownList ID="ddl_gender" runat="server" AutoPostBack="True" 
                                        CssClass="form-control" TabIndex="6">
                                        <asp:ListItem>Both</asp:ListItem>
                                        <asp:ListItem>Male</asp:ListItem>
                                        <asp:ListItem>Female</asp:ListItem>
                                    </asp:DropDownList>
                        </td>
                               
                                </tr><tr>

                        <td colspan="4" align="center">

                        <asp:Button ID="btn_add" runat="server" class="btn btn-success" OnClientClick="return fnSave();"
                                    onclick="btn_add_Click" Text="Save" TabIndex="7" />
                            <asp:CheckBox ID="Chk_onduty" runat="server"
                                        Text="On Duty" TabIndex="7" Visible="False" />
                        </td>
                    </tr>
                </table>
                <table id="tb_leave1" runat="server" width="100%">
                    <tr valign="top">
                        <td align="center" width="100%" valign="top">
                            <asp:GridView ID="GridView1" runat="server"  AllowSorting="True" 
                                AutoGenerateColumns="False" onrowcommand="GridView1_RowCommand"
                                onrowdeleting="GridView1_RowDeleting" onrowdatabound="GridView1_RowDataBound" 
                                onrowediting="GridView1_RowEditing" onselectedindexchanged="GridView1_SelectedIndexChanged" 
                                onrowupdating="GridView1_RowUpdating" onrowcancelingedit="GridView1_RowCancelingEdit" 
                                HorizontalAlign="Center" GridLines="None" CssClass="table table-striped table-bordered table-hover" TabIndex="8">
                           
                         
            <Columns>
            
                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Leave Code" HeaderStyle-HorizontalAlign="Left">
                
                    <ItemTemplate>
                        <asp:Label ID="lbl_leavecode" runat="server" Text='<%# Eval("pn_leaveCode") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:Label ID="lbl_leavecode_edit" runat="server" Text='<%# Bind("pn_leaveCode") %>'></asp:Label>
                    </EditItemTemplate>
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Leave Name"  HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lbl_leavename" runat="server" Text='<%# Eval("v_leaveName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="txt_leavename_edit" runat="server" Text='<%# Bind("v_leaveName") %>' 
                      CssClass="form-control" MaxLength="20"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="No. of Days" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lbl_count" runat="server" Text='<%# Eval("pn_Count") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="txt_count_edit" Width="75px" runat="server" Text='<%# Bind("pn_Count") %>'
                     CssClass="form-control" MaxLength="3"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                
                
                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Annual Leave"  HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lbl_annual" runat="server" Text='<%# Eval("Annual_leave") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:DropDownList ID="ddl_annualleave" runat="server" DataTextField="Annual_leave" CssClass="form-control"
                            DataValueField="Annual_leave">
                                <asp:ListItem>Carry Forward</asp:ListItem>
                                <asp:ListItem>Encashment</asp:ListItem>
                                <asp:ListItem>Carry Forward & Encashment</asp:ListItem>
                                <asp:ListItem>None</asp:ListItem>
                    </asp:DropDownList>                    
                    </EditItemTemplate>
                </asp:TemplateField>
                
                 <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Max. Days"  HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lbl_maxdays" runat="server" Text='<%# Eval("Max_days") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                     <asp:TextBox ID="txt_maxdays_edit" Width="75px" runat="server" Text='<%# Bind("Max_days") %>' onkeydown="AllowOnlyNumeric1(event);"
                      CssClass="form-control" MaxLength="2"></asp:TextBox>                    
                    </EditItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Applicable"  HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lbl_EL" runat="server" Text='<%# Eval("EL") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                     <asp:DropDownList ID="txt_EL_edit" runat="server" Text='<%# Bind("EL") %>'  CssClass="form-control"
                      MaxLength="3">
                    <asp:ListItem Text='Both' Value='Both'></asp:ListItem>
                    <asp:ListItem Text='Male' Value='Male'></asp:ListItem>
                    <asp:ListItem Text='Female' Value='Female'></asp:ListItem>
                    </asp:DropDownList>                    
                    </EditItemTemplate>
                </asp:TemplateField>
                

                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Type"  HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lbl_type" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                     <asp:DropDownList ID="txt_Type" runat="server" Text='<%# Bind("Type") %>'  CssClass="form-control"
                      MaxLength="3">
                    <asp:ListItem Text='Leave' Value='Leave'></asp:ListItem>
                    <asp:ListItem Text='On Duty' Value='On Duty'></asp:ListItem>
                    
                    </asp:DropDownList>                    
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:CommandField ItemStyle-Width="25px" EditImageUrl="~/Images/edit_icon.png" ButtonType="Image" UpdateImageUrl="~/Images/save_icon.jpg" CancelImageUrl="~/Images/cancel.png" ShowEditButton="True" />
            <asp:CommandField ShowDeleteButton="True" ItemStyle-Width="25px" ButtonType="Image" DeleteImageUrl="~/Images/delete_icon.jpg" />
            </Columns>
            <EmptyDataTemplate>
            <asp:Label ID="lblempty" Text="No Records" runat="server">
            </asp:Label>            
            </EmptyDataTemplate>
                         
        </asp:GridView>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td align="center" width="50%" valign="top">
                            &nbsp;</td>
                    </tr>
                </table>
                </ContentTemplate>
                </asp:UpdatePanel>
</asp:Content>
