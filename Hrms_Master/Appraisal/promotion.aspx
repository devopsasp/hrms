<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="promotion.aspx.cs" Inherits="Hrms_Master_Default" Title="Welcome to HRMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
  
   function show_message()
    {
        alert("Permission Restricted. Please Contact Administrator.");
    }
   </script>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="background-color:#dbcdcc" height="35px" class="border"><span class="Title">&nbsp;&nbsp;<span class="style2">Promotion</span></span></td>     
        <td style="background-color:#dbcdcc">
            <asp:DropDownList ID="ddl_branch" runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddl_branch_SelectedIndexChanged" Width="115px">
            </asp:DropDownList>
        </td>   
    </tr>
    <tr>
        <td style="padding-left:350px"><span class="style2"><asp:Label ID="lblerror" runat="server" ForeColor="Red"></asp:Label></span></td>
    </tr>
</table>
<br />

    <asp:ListView ID="lv_promotion" runat="server" 
        onitemediting="lv_promotion_ItemEditing" 
        onitemupdating="lv_promotion_ItemUpdating" 
        onitemdeleting="lv_promotion_ItemDeleting" 
        onitemcommand="lv_promotion_ItemCommand" InsertItemPosition="LastItem" 
        oniteminserting="lv_promotion_ItemInserting" 
        onitemcanceling="lv_promotion_ItemCanceling" 
        onselectedindexchanged="lv_promotion_SelectedIndexChanged" >
        <LayoutTemplate>
            <table border="1" align="center">
                <tr style="background-color:#c18685; color:White; font-family:Calibri">
                    <th>Department</th>
                    <th>Grade</th>
                    <th>Basic</th>
                    <th>Upto_Amount</th>
                    <th>Percentage</th> 
                    <th></th>               
                </tr>
                <tr id="itemplaceholder" runat="server"></tr>            
            </table>
        </LayoutTemplate>
        
        <EmptyItemTemplate>
            <table><tr><td><asp:Label ID="lbl_empty" runat="server" Text="No Record Found"></asp:Label></td></tr></table>            
        </EmptyItemTemplate>
        <EmptyDataTemplate>
            <table>
                <tr>
                    <td><asp:Label ID="lbl_empty" runat="server" Text="Norecord Found"></asp:Label></td>
                </tr>
                
            </table>
        </EmptyDataTemplate>
        
        <ItemTemplate>
            <tr style="font-family:Calibri">  
                <asp:Label ID="lbl_ihiddenid" runat="server" Text='<%#Eval("id") %>' Visible="false"></asp:Label>              
                <td><asp:Label ID="lbldept" runat="server" Text='<%#Eval("pn_departmentname") %>' Width="75px"></asp:Label></td>
                <td><asp:Label ID="lblgrade" runat="server" Text='<%#Eval("grade")%>' Width="100px"></asp:Label></td>                
                <td><asp:Label ID="lblbasic" runat="server" Text='<%#Eval("basic") %>' Width="100px"></asp:Label></td>    
                <td><asp:Label ID="lblupto_amt" runat="server" Text='<%#Eval("upto_amount") %>' Width="100px"></asp:Label></td>
                <td><asp:Label ID="lblpercentage" runat="server" Text='<%#Eval("percentage") %>' Width="100px"></asp:Label></td>
                <td><asp:LinkButton ID="cmd_edit" runat="server" Text="Edit" CommandName="Edit" Width="40px"></asp:LinkButton>
                <asp:LinkButton ID="cmd_del" runat="server" Text="Delete" CommandName="Delete" Width="40px"></asp:LinkButton></td>            
            </tr>
        
        </ItemTemplate>
        
        <EditItemTemplate>
        <tr style="font-family:Calibri;background-color:#eee8dc">
            <asp:Label ID="lbl_hiddenid" runat="server" Text='<%#Eval("id") %>' Visible="false"></asp:Label>
            <td><asp:DropDownList ID="ddldept" runat="server" DataTextField="v_departmentname" DataValueField="v_departmentname" DataSourceID="sqlds_ddldept" AutoPostBack="false" Width="100px"><asp:ListItem>Select</asp:ListItem></asp:DropDownList></td>
            <td><asp:DropDownList ID="ddlgrade" runat="server" DataTextField="v_gradename" DataValueField="v_gradename" DataSourceID="sqlds_ddlgrade" AutoPostBack="false" Width="100px"><asp:ListItem>Select</asp:ListItem></asp:DropDownList></td>
            <td><asp:TextBox ID="txtbasic" runat="server" Text='<%#Eval("basic") %>' Width="100px" MaxLength="6"></asp:TextBox></td>    
            <td><asp:TextBox ID="txtupto_amt" runat="server" Text='<%#Eval("upto_amount") %>' Width="100px" MaxLength="6"></asp:TextBox></td>
            <td><asp:TextBox ID="txtpercentage" runat="server" Text='<%#Eval("percentage") %>' Width="100px" MaxLength="3"></asp:TextBox></td>
            <td><asp:LinkButton ID="cmd_Update" runat="server" Text="Update" CommandName="Update" Width="40px"></asp:LinkButton>
            <asp:LinkButton ID="cmd_cancel" runat="server" Text="Cancel" CommandName="Cancel" Width="40px"></asp:LinkButton></td>              
            </tr>
        </EditItemTemplate>
        
        <InsertItemTemplate>
        <tr></tr>
        <tr style="background-color:#cdcdc1;font-family:Calibri">
            <td><asp:DropDownList ID="ddlidept" runat="server" DataTextField="v_departmentname" DataValueField="v_departmentname" DataSourceID="sqlds_ddldept" AutoPostBack="false" Width="120px" BackColor="#cdcdc1"><asp:ListItem>Select</asp:ListItem></asp:DropDownList></td>
            <td><asp:DropDownList ID="ddligrade" runat="server" DataTextField="v_gradename" DataValueField="v_gradename" DataSourceID="sqlds_ddlgrade" AutoPostBack="false" Width="120px" BackColor="#cdcdc1"><asp:ListItem>Select</asp:ListItem></asp:DropDownList></td>
            <td><asp:TextBox ID="txtibasic" runat="server" Width="120px" BackColor="#cdcdc1" MaxLength="6"></asp:TextBox></td>    
            <td><asp:TextBox ID="txtiupto_amt" runat="server" Width="120px" BackColor="#cdcdc1" MaxLength="6"></asp:TextBox></td>
            <td><asp:TextBox ID="txtipercentage" runat="server" Width="120px" BackColor="#cdcdc1" MaxLength="3"></asp:TextBox></td>
            <td><asp:LinkButton ID="cmd_Insert" runat="server" Text="Insert" CommandName="Insert" Width="75px"></asp:LinkButton></td>  
        </tr>      
        </InsertItemTemplate>
    
    
    </asp:ListView>
    
    <asp:SqlDataSource ID="sqlds_ddldept" runat="server" ConnectionString="<%$ConnectionStrings:connectionstring %>" SelectCommand="select * from paym_department where pn_branchid=@pn_BranchID">
    <SelectParameters>
        <asp:SessionParameter Name="pn_BranchID" SessionField="Login_Temp_BranchID" Type="Int32" />    
    </SelectParameters>
    
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlds_ddlgrade" runat="server" ConnectionString="<%$ConnectionStrings:connectionstring %>" SelectCommand="select * from paym_grade where branchid=@pn_branchid">
        <SelectParameters>
            <asp:SessionParameter Name="pn_BranchID" SessionField="Login_Temp_BranchID" Type="Int32" />
        </SelectParameters>
    
    </asp:SqlDataSource>
    
    
    <br /><br />
    
   
</asp:Content>

