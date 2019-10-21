<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="Allowances.aspx.cs" Inherits="Hrms_Master_Default3" Title="Welcome to HRMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../_assets/css/meiii.css" rel="stylesheet" type="text/css" />
    <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="background-color:#dbcdcc" height="35px" class="border">
                            <span class="Title">&nbsp;&nbsp;<span 
                                style="font-family: Calibri; color: #800000;">Accomodation</span></span></td>
                    </tr>
                </table>
    <asp:Label ID="lblerror" runat="server" Text=""></asp:Label>

    <br />

    <br />

<asp:DataPager id="dp1" runat="server" PagedControlID="lv_a_details" OnPreRender="pager1_perrender"></asp:DataPager>
            
            <div class="pading-left">
                <div class="wholegrid">
                    
                    <asp:ListView ID="lv_a_details" runat="server" InsertItemPosition="LastItem" 
                        onitemcommand="lv_a_details_ItemCommand" 
                        oniteminserting="lv_a_details_ItemInserting" 
                        onitemediting="lv_a_details_ItemEditing"  
                        OnItemUpdating="lv_a_details_updating" OnItemCanceling="lv_a_deatis_cancelling" 
                        onitemdeleting="lv_a_details_ItemDeleting" >
                        <LayoutTemplate>
                            <table class="" border="1" style="border-color: #C18685">
                                <tr style="background-color: #c18685">
                                    <th style="font-family: Calibri; color: #FFFFFF" align="left">Grade</th>
                                    <th style="font-family: Calibri; color: #FFFFFF" align="left">Travel Allowances</th>  
                                    <th style="font-family: Calibri; color: #FFFFFF">Delete</th>      
                                    <th style="font-family: Calibri; color: #FFFFFF">Edit</th>       
                                </tr>
                                <tr style="background-color: #c18685" id="itemplaceholder" runat="server" class="rowstyle"></tr>
                            </table>
                        
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr runat="server" class="rowstyle" >
                                <td visible="false"><asp:Label ID="lblid" runat="server" Text='<%#Eval("id") %>'></asp:Label></td>
                                <td><asp:Label ID="lblgrade" runat="server" Text='<%#Eval("grade") %>'></asp:Label></td>
                                <td><asp:Label ID="lblmaxallowance" runat="server" Text='<%#Eval("allowances") %>'></asp:Label></td>                
                                <td><asp:LinkButton ID="cmd_del" runat="server" Text="Delete" CommandName="Delete"></asp:LinkButton></td>
                                <td><asp:LinkButton ID="cmd_edit" runat="server" Text="Edit" CommandName="Edit"></asp:LinkButton> </td>
                            </tr>     
                        </ItemTemplate>
                        <InsertItemTemplate>        
                            <td><asp:TextBox ID="txtgrade" runat="server" MaxLength="10"></asp:TextBox></td>
                            <td><asp:TextBox ID="txtmaxallowance" runat="server" MaxLength="6"></asp:TextBox></td>
                            <td><asp:LinkButton ID="cmd_ins" runat="server" Text="Add" CommandName="Insert" MaxLength="20"></asp:LinkButton></td>    
                        </InsertItemTemplate>
                        
                        <EditItemTemplate>
                            <tr id="invisiblerow" runat="server">
                                <td visible="false"><asp:Label ID="lblid" runat="server" Text='<%#Eval("id") %>' Visible="false"></asp:Label></td>
                            </tr>
                            <div class="editt">
                                <tr class="editt">
                                    <td><asp:Label ID="lblgrade" runat="server" Text="Grade" ></asp:Label></td>
                                    <td><asp:TextBox ID="txtegrade" runat="server" MaxLength="10" Text='<%#Eval("grade") %>'></asp:TextBox></td>
                                    <td><asp:LinkButton ID="LinkButton1" Text="Save" CommandName="Update" runat="server" ></asp:LinkButton></td>
                                    <td></td>
                                </tr>
                                <tr class="editt">
                                    <td><asp:Label ID="lblallowance" runat="server" Text="Allowance"></asp:Label></td>
                                    <td><asp:TextBox ID="txteallowance" runat="server" MaxLength="6" Text='<%#Eval("allowances") %>'></asp:TextBox></td>
                                    <td><asp:LinkButton ID="LinkButton2" Text="Cancel" CommandName="Cancel" runat="server"></asp:LinkButton></td>
                                    <td></td>
                                </tr>
                                <%--<tr class="editt">
                                    <td visible="false">&nbsp</td>
                                    <td><asp:LinkButton ID="cmd_but" Text="Save" CommandName="Update" runat="server" ></asp:LinkButton>  
                                    <asp:LinkButton ID="cmd_can" Text="Cancel" CommandName="Cancel" runat="server"></asp:LinkButton></td>            
                                </tr>--%>
                            
                            </div>
                            
                            
                        </EditItemTemplate>
        
                    </asp:ListView>
            
    
    
</div>
<br />
</div>    
    
    
</asp:Content>