<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_compliant_box.aspx.cs" MasterPageFile="~/HRMS.master" Inherits="Hrms_Operations_hr_compliant_box" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <style type="text/css" media="screen">    @import "basic.css";
    #contbl
    {
        height: 64px;
    }
    #reftbl
    {
        height: 206px;
    }
    #Text1
    {
        height: 19px;
        width: 125px;
    }
    #personal_tbl
    {
        width: 146%;
    }
    #Table1
    {
        width: 323%;
    }
        .style122
        {
            font-family: Calibri;
            font-size: x-small;
            color: #808080;
            height: 29px;
            width: 548px;
        }
        .style123
       {
           font-family: Calibri;
           font-size: x-small;
           color: #808080;
           height: 29px;
           }
        .style124
       {
           font-family: Calibri;
           font-size: x-small;
           color: #808080;
           height: 29px;
           width: 527px;
       }
        .style125
       {
           font-family: Calibri;
           font-size: x-small;
           color: #808080;
           height: 11px;
           width: 527px;
       }
       .style126
       {
           font-family: Calibri;
           font-size: x-small;
           color: #808080;
           height: 11px;
           width: 548px;
       }
        </style>
    <table cellpadding="5" cellspacing="1" width="100%" id="tbl_details" runat="server">
     <tr id="Tr9" runat="server">
                        <td>                  
                           <span class="Title"><span style="font-family: Calibri;  font-size: medium; font-weight: bold;">            
                                &nbsp;&nbsp;&nbsp;Compliant 
                            Box 
                             </span></td>
                    </tr>
  
                    <tr id="Tr1" runat="server">
                        <td colspan="2" align="center">
                            &nbsp;<asp:Label ID="lbl_Error" CssClass="Error" runat="server" ForeColor="Red" Font-Bold="True" Width="40%"></asp:Label></td>
                    </tr>
                    
        
          
                
                
                 
                 
                              <tr  id="Tr3" runat="server">
                        <td align="center" 
                  style="font-family:Calibri;font-size: small;color: #808080;" class="style123" 
                              colspan="2">
                         <asp:GridView ID="GridView1"  class="table table-striped table-bordered table-hover"
            AutoGenerateColumns="False" 
            style="Z-INDEX: 101; LEFT: 8px; TOP: 32px" 
            ShowFooter="True" Font-Size="X-Small"
            Font-Names="Verdana" runat="server" 
           
                    Width="500px" CellPadding="4" ForeColor="#333333" GridLines="None" 
                                onrowediting="GridView1_RowEditing" 
                                onrowcancelingedit="GridView1_RowCancelingEdit" 
                                onrowupdating="GridView1_RowUpdating" 
                                onrowdatabound="GridView1_RowDataBound">
            <RowStyle />
                    <PagerStyle HorizontalAlign="Center" />
                    <SelectedRowStyle  Font-Bold="True"  />
            <HeaderStyle  HorizontalAlign="Left" 
                        Font-Bold="True"/>
            <FooterStyle  Font-Bold="True" />
            <Columns>
                <asp:TemplateField HeaderText="Id" Visible="false">
           <ItemTemplate>
                        <asp:Label ID="lbl_id" Text='<%# Eval("Id1") %>' runat="server"></asp:Label>
           </ItemTemplate>
            
           </asp:TemplateField>
            <asp:TemplateField HeaderText="Employee Name">
           <ItemTemplate>
                        <asp:Label ID="lbl_empid" Text='<%# Eval("EmployeeCode") %>' runat="server"></asp:Label>
           </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Compliant Subject">
           <ItemTemplate>
                        <asp:Label ID="lbl_subject" Text='<%# Eval("Compliant_Subject1") %>' runat="server"></asp:Label>
           </ItemTemplate>
           <%--<EditItemTemplate>
               <asp:TextBox ID="txt_subject" Text='<%# Bind("Compliant_Subject") %>' runat="server"></asp:TextBox>
           </EditItemTemplate>--%>
           </asp:TemplateField>
                <asp:TemplateField HeaderText="Compliant Text">
           <ItemTemplate>
                        <asp:Label ID="lbl_text" Text='<%# Eval("Compliant_Text1") %>' runat="server"></asp:Label>
           </ItemTemplate>
           <%-- <EditItemTemplate>
               <asp:TextBox ID="txt_text" Text='<%# Bind("Compliant_Text") %>' TextMode="MultiLine" runat="server"></asp:TextBox>
           </EditItemTemplate>--%>
           </asp:TemplateField>
            <asp:TemplateField HeaderText="Status">
           <ItemTemplate>
                        <asp:Label ID="lbl_Status" Text='<%# Eval("Status21") %>' runat="server"></asp:Label>
           </ItemTemplate>
           <EditItemTemplate>
               <asp:DropDownList ID="ddl_status" runat="server">
               <asp:ListItem>Select</asp:ListItem>
               <asp:ListItem>Waiting</asp:ListItem>
               <asp:ListItem>On Process</asp:ListItem>
               <asp:ListItem>Solved</asp:ListItem>
               </asp:DropDownList>
           </EditItemTemplate>
           </asp:TemplateField>
           <asp:CommandField ShowEditButton="true" ShowCancelButton="true" />
            </Columns>
              <EditRowStyle  />
                    <AlternatingRowStyle  /></asp:GridView>
                        </td>
                    </tr>
                 
                    </table>
                    </asp:Content>