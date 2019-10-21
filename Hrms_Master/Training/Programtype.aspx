<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Programtype.aspx.cs" Inherits="Hrms_Training_Default" Title="Welcome to HRMS Training Module"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../_assets/css/meiii.css" rel="stylesheet" type="text/css" />


    <div><h3 class="page-header">Institution List
                     <asp:DropDownList ID="ddl_branch" runat="server" AutoPostBack="True" 
                         onselectedindexchanged="ddl_branch_SelectedIndexChanged" Width="115px">
                     </asp:DropDownList>
                                                </h3></div>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">

    <ContentTemplate>--%>
        <div style="width: 70%">
                    <table cellpadding="1%" cellspacing="1%" width="100%" class="table table-hover table-striped">
                        
                        <tr>
                            <td>
         <asp:ListView ID="lv" runat="server" GroupItemCount="4" onselectedindexchanged="lv_SelectedIndexChanged">
            
            <LayoutTemplate>                
                    <table>
                       <tr ID="groupPlaceholder" runat="server">
                       </tr>
                    </table>                       
            </LayoutTemplate>                                              
            <GroupTemplate>
                    <tr>
                        <td ID="itemPlaceholder" runat="server">
                        </td>
                    </tr>
             </GroupTemplate>          
            <ItemTemplate>              
                 
                     <td style="padding-right:2em; padding-left: 2em;" align="Center" >
                         <asp:Image ID="imglogo" Width="100" runat="server" ImageUrl='<%#"Handler.ashx?id=" + Eval("id") %>' BorderStyle="Groove" />
                         <br />
                         <b><asp:LinkButton ID="lnk_ins_name" runat="server" Text='<%#Eval("ins_name") %>' PostBackUrl='<%#"~/Hrms_Master/Training/TrainerProfile.aspx?id="+Eval("ID") %>'></asp:LinkButton></b>
                     </td>                
                
            </ItemTemplate>
                 
         </asp:ListView>
                            </td>
                        </tr>
                        </table>   

         <div class="pading-left">
             <br />
             <br />
        </div>
</asp:Content>
