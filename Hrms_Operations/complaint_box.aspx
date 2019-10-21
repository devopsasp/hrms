<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="complaint_box.aspx.cs" MasterPageFile="~/HRMS.master" Inherits="Hrms_Operations_complaint_box" %>

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
           width: 412px;
       }
        .style125
       {
           font-family: Calibri;
           font-size: x-small;
           color: #808080;
           height: 11px;
           width: 412px;
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
                        <td align="left"  colspan="2">
                             
                           Compliant 
                            Box 
                             </span></td>
                    </tr>
  
                    <tr id="Tr1" runat="server">
                        <td colspan="2" align="center">
                            &nbsp;<asp:Label ID="lbl_Error" CssClass="Error" runat="server" ForeColor="Red" Font-Bold="True" Width="40%"></asp:Label></td>
                    </tr>
                     <tr id="Tr2" runat="server">
                        <td align="right" 
                  style="font-family:Calibri;font-size: small;color: #808080;" class="style125">
                           Compliant Subject</td>
                        <td align="left" style="font-family:Calibri;font-size: small;color: #808080;" 
                             class="style126">
                            <asp:TextBox ID="txt_subject" runat="server" Width="181px" CssClass="form-control"></asp:TextBox></td>
                    </tr>
                      <tr  id="Tr_type" runat="server">
                        <td align="right" 
                  style="font-family:Calibri;font-size: small;color: #808080;" class="style124">
                            Compliant&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                        <td align="left" style="font-family:Calibri;font-size: small;color: #808080;" class="style122">
                            <asp:TextBox ID="txt_compliant" runat="server" TextMode="MultiLine" CssClass="form-control"
                                Height="134px" Width="258px"></asp:TextBox>                     
                   </td>
                    </tr>
        
          
                
                
                 
                      <tr  id="Tr8" runat="server">
                        <td align="center" 
                  style="font-family:Calibri;font-size: small;color: #808080;" class="style123" 
                              colspan="2">
                            <%--<asp:ImageButton ID="btn_save" runat="server" 
                                 onclick="btn_save_Click"
                                             />--%>
                            <asp:Button ID="btn_save" runat="server" Text="Save" onclick="btn_save_Click" class="btn btn-success"/>
                                             
                                             </td>
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
                                onrowdeleting="GridView1_RowDeleting">
            <RowStyle />
                    <PagerStyle  HorizontalAlign="Center" />
                    <SelectedRowStyle Font-Bold="True" />
            <HeaderStyle  HorizontalAlign="Left" 
                        Font-Bold="True"/>
            <FooterStyle  Font-Bold="True"  />
            <Columns>
                <asp:TemplateField HeaderText="Id" Visible="false">
           <ItemTemplate>
                        <asp:Label ID="lbl_id" Text='<%# Eval("Id1") %>' runat="server"></asp:Label>
           </ItemTemplate>
          
           </asp:TemplateField>
            <asp:TemplateField HeaderText="Id">
           <ItemTemplate>
                        <asp:Label ID="lbl_empid" Text='<%# Eval("EmployeeCode") %>' runat="server"></asp:Label>
           </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Compliant Subject">
           <ItemTemplate>
                        <asp:Label ID="lbl_subject" Text='<%# Eval("Compliant_Subject1") %>' runat="server"></asp:Label>
           </ItemTemplate>
           <EditItemTemplate>
               <asp:TextBox ID="txt_subject" Text='<%# Bind("Compliant_Subject1") %>' runat="server"></asp:TextBox>
           </EditItemTemplate>
           </asp:TemplateField>
                <asp:TemplateField HeaderText="Compliant Text">
           <ItemTemplate>
                        <asp:Label ID="lbl_text" Text='<%# Eval("Compliant_Text1") %>' runat="server"></asp:Label>
           </ItemTemplate>
            <EditItemTemplate>
               <asp:TextBox ID="txt_text" Text='<%# Bind("Compliant_Text1") %>' TextMode="MultiLine" runat="server"></asp:TextBox>
           </EditItemTemplate>
           </asp:TemplateField>
            <asp:TemplateField HeaderText="Status">
           <ItemTemplate>
                        <asp:Label ID="lbl_Status" Text='<%# Eval("Status21") %>' runat="server"></asp:Label>
           </ItemTemplate>
          <%-- <EditItemTemplate>
               <asp:TextBox ID="txt_status" Text='<%# Bind("Status1") %>' TextMode="MultiLine" runat="server"></asp:TextBox>
           </EditItemTemplate>--%>
           </asp:TemplateField>
           <asp:CommandField ShowEditButton="true" ShowCancelButton="true" ShowDeleteButton="true" />
            </Columns>
              <EditRowStyle  />
                    <AlternatingRowStyle /></asp:GridView>
                        </td>
                    </tr>
                 
                    </table>
                    </asp:Content>