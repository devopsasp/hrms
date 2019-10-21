<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" MasterPageFile="~/HRMS.master" CodeFile="RequestTraining.aspx.cs" Inherits="Hrms_Additional_RequestTraining" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../datecheck.js"></script>
    <script type ="text/javascript">
    function Change(obj, evt)
    {
        if(evt.type=="focus")
            obj.style.borderColor="#E6D78A";
        else if(evt.type=="blur")
           obj.style.borderColor="#989898";
    }
   </script>

     <style type="text/css">

         .roundedbox {
    background:#fff;
    font-family:Calibri;
    font-size:9pt;
    
    margin-left:0px;
    margin-right:auto;
    margin-top:1px;
    margin-bottom:1px;
    padding:3px;
    border-top:1px solid #CCCCCC;
    border-left:1px solid #CCCCCC;
    border-right:1px solid #999999;
    border-bottom:1px solid #999999;
    -moz-border-radius:10px;
    -webkit-border-radius: 8px;
    border-top-left-radius:6px;
	border-top-right-radius:6px;
	border-bottom-left-radius:6px;
	border-bottom-right-radius:6px;
	
    
}

         .style12
         {
             height: 33px;
         }
         
         .style18
         {
             height: 40px;
         }
         
 </style>
<table cellpadding="0%" cellspacing="0%" width="100%">
<tr> <td align="left" 
                  style="font-family:Calibri;font-size: small; class="style123" 
                              colspan="2">
                           <span class="Title">
                           <span 
                                style="height: 17px; font-family: Calibri; font-size: medium; font-weight: bold; ">&nbsp;&nbsp;&nbsp;Employee Training 
                           Requsition</span></td></tr>
</table>
<table  cellpadding="0%" cellspacing="0%" width="100%">
<tr> <td colspan="2" align="center">
                            &nbsp;<asp:Label ID="lbl_Error" CssClass="Error" runat="server" ForeColor="Red" Font-Bold="True" Width="40%"></asp:Label></td></tr>
                            <tr>
<td></td>





<td><asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True"  CssClass="form-control"
                    >
                </asp:DropDownList>
            </td>
</tr>
</table>
<table cellpadding="0%" cellspacing="0%" width="100%">
<tr id="tr1" runat="server"><td>
<table cellpadding="0%" cellspacing="0%" width="95%">

<tr>

<td style="font-family:Calibri;font-size: small;color: #808080;" align="center" 
        class="style12">&nbsp;Program Name</td>

<td class="style12">
    <asp:TextBox ID="txtpgmname" runat="server" Width="200px" 
        onfocus ="Change(this, event)" onblur ="Change(this, event)" 
         CssClass="form-control" style="color: #808080;" 
        ></asp:TextBox>
    
    </td>
</tr>
<tr>

<td style="font-family:Calibri;font-size: small;color: #808080;" align="center" 
        class="style18">Program Type</td>

<td class="style18">
    <asp:DropDownList ID="ddl_PgmType" runat="server" Width="200px" onfocus ="Change(this, event)" onblur ="Change(this, event)"  CssClass="form-control" style="color: #808080;">
    <asp:ListItem Value="0">Select</asp:ListItem>
    <asp:ListItem Value="1">Training 1</asp:ListItem>
    <asp:ListItem Value="2">Training 2</asp:ListItem>
    </asp:DropDownList>
    
    </td>

</tr>
<tr>

<td style="font-family:Calibri;font-size: small;color: #808080;" align="center" 
        class="style12">Summary</td>

<td class="style12">
    <asp:TextBox ID="txtSummary" runat="server" Width="198px" 
        onfocus ="Change(this, event)" onblur ="Change(this, event)" 
         CssClass="form-control" style="color: #808080;" TextMode="MultiLine" Height="66px"
        ></asp:TextBox>
    
    </td>
</tr>
</table>

</td></tr>
<tr  id="tr2" runat="server"><td>
<table cellpadding="0px" cellspacing="0px" width="100%">
<tr>
<td align="center" style="font-family:Calibri;font-size: small;color: #808080;" 
        class="style12">
    <asp:Button ID="ImageButton1" runat="server" Text="Add" Height="31px"  OnClientClick="return check();" onclick="ImageButton1_Click" class="btn btn-success"/>
   <%--<asp:ImageButton ID="ImageButton1" runat="server" Height="31px"
                                 ImageUrl="~/Images/Add.png" Width="107px" 
        OnClientClick="return check();" onclick="ImageButton1_Click" />--%>
        
        
        </td>


</tr>

</table>
</td></tr>
<tr  id="tr3" runat="server"><td>
<table cellspacing="0px" width="100%">
<tr><td><div>
<asp:GridView ID="GridView1" Font-Size="Smaller" runat="server" AllowSorting="True" 
                    AutoGenerateColumns="False" Height="16px" class="table table-striped table-bordered table-hover"   
        Width="90%" CellPadding="4" 
                     HorizontalAlign="Center"
      onrowcancelingedit="GridView1_RowCancelingEdit" 
        onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing" 
        onrowupdating="GridView1_RowUpdating" GridLines="None" 
         >
                    <RowStyle  Font-Names="Calibri" 
                        BorderStyle="None" Font-Size="Small" />
                    <Columns>
                    
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="ID" HeaderStyle-HorizontalAlign="Center">
                        
                            <ItemTemplate>
                                <asp:Label ID="lblid" runat="server" Text='<%# Eval("TrainingId") %>'></asp:Label>
                            </ItemTemplate>
                           

        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                          <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="ProgramName" HeaderStyle-HorizontalAlign="Center">
                        
                            <ItemTemplate>
                                <asp:Label ID="lblPgmName" runat="server" Text='<%# Eval("prgmname") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                            <asp:TextBox ID="txtpgmname" runat="server" Width="200px" 
        onfocus ="Change(this, event)" onblur ="Change(this, event)" Text='<%# Eval("prgmname") %>'
        CssClass="roundedbox" style="color: #808080;" 
        ></asp:TextBox>
                            </EditItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                         <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="ProgramType" HeaderStyle-HorizontalAlign="Center">
                        
                            <ItemTemplate>
                                <asp:Label ID="lblPgmType" runat="server" Text='<%# Eval("prgmtypName") %>'></asp:Label>
                            </ItemTemplate>
                           <EditItemTemplate>
                           <asp:DropDownList ID="ddl_PgmType" runat="server" Width="200px" onfocus ="Change(this, event)" onblur ="Change(this, event)" CssClass="roundedbox" style="color: #808080;">
    <asp:ListItem Value="0">Select</asp:ListItem>
    <asp:ListItem Value="1">Training 1</asp:ListItem>
    <asp:ListItem Value="2">Training 2</asp:ListItem>
    </asp:DropDownList>
                           </EditItemTemplate>

        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                      
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Summary" HeaderStyle-HorizontalAlign="Center">                        
                            <ItemTemplate>
                                <asp:Label ID="lblsum" runat="server" Text='<%# Eval("temp_str") %>'></asp:Label>
                            </ItemTemplate>    
                            <EditItemTemplate>
                             <asp:TextBox ID="txtsum" runat="server" Text='<%# Eval("temp_str") %>' Width="200px" 
        onfocus ="Change(this, event)" onblur ="Change(this, event)" 
        CssClass="roundedbox" style="color: #808080;" ></asp:TextBox>
                            </EditItemTemplate>                      
        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>    
                          <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Status" HeaderStyle-HorizontalAlign="Center">
                        
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("IDno") %>'></asp:Label>
                            </ItemTemplate>
                          <%-- <EditItemTemplate>
                           <asp:DropDownList ID="ddl_Status" runat="server" Width="200px" onfocus ="Change(this, event)" onblur ="Change(this, event)" CssClass="roundedbox" style="color: #808080;">
    <asp:ListItem Value="0">Select</asp:ListItem>
    <asp:ListItem Value="1">Approved</asp:ListItem>
    <asp:ListItem Value="2">Rejected</asp:ListItem>
    </asp:DropDownList>
                           </EditItemTemplate>--%>

        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                         <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Reason" HeaderStyle-HorizontalAlign="Center">                        
                            <ItemTemplate>
                                <asp:Label ID="lblReason" runat="server" Text='<%# Eval("Reason") %>'></asp:Label>
                            </ItemTemplate>    
                            <%--<EditItemTemplate>
                             <asp:TextBox ID="txtReason" runat="server" Text='<%# Eval("Reason") %>' Width="200px" 
        onfocus ="Change(this, event)" onblur ="Change(this, event)" 
        CssClass="roundedbox" style="color: #808080;" ></asp:TextBox>
                            </EditItemTemplate>     --%>                 
        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>    
                        <asp:CommandField ShowEditButton="True" ShowDeleteButton="true"/>
                    </Columns>
                                   <FooterStyle BackColor="#5D7B9D" Font-Bold="True" 
                        ForeColor="White" />
                                   <PagerStyle BackColor="#284775" ForeColor="White" 
                        HorizontalAlign="Center" />
                    <EmptyDataTemplate>
                    <asp:Label ID="lblempty" Text="No Records" runat="server">
                    </asp:Label> 
                    
                    </EmptyDataTemplate>
                                   <SelectedRowStyle  Font-Bold="True" 
                        Wrap="True" />
                                   <HeaderStyle   Font-Bold="True" 
                                       Font-Names="Calibri" BorderStyle="None" 
                                       Font-Size="Small" />
                    <EditRowStyle  />
                    <AlternatingRowStyle/>
                </asp:GridView>
</div></td>
</tr>
</table>
</td></tr>
<tr  id="tr4" runat="server"><td>
<table cellpadding="0%" cellspacing="0%" width="100%">
<tr><td><div>
<asp:GridView ID="GridView2" Font-Size="Smaller" runat="server" AllowSorting="True" 
                    AutoGenerateColumns="False" Height="16px" class="table table-striped table-bordered table-hover"
        Width="90%" CellPadding="4"  
                    HorizontalAlign="Center"
        onrowcancelingedit="GridView2_RowCancelingEdit" 
       onrowediting="GridView2_RowEditing" 
        onrowupdating="GridView2_RowUpdating" GridLines="None" >
                    <RowStyle  Font-Names="Calibri" 
                        BorderStyle="None" Font-Size="Small" />
                    <Columns>                    
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="ID" HeaderStyle-HorizontalAlign="Center">
                        
                            <ItemTemplate>
                                <asp:Label ID="lblid" runat="server" Text='<%# Eval("TrainingId") %>'></asp:Label>
                            </ItemTemplate>                         

        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                          <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="ProgramName" HeaderStyle-HorizontalAlign="Center">
                        
                            <ItemTemplate>
                                <asp:Label ID="lblPgmName" runat="server" Text='<%# Eval("prgmname") %>'></asp:Label>
                            </ItemTemplate>
                            <%--<EditItemTemplate>
                            <asp:TextBox ID="txtpgmname" runat="server" Width="200px" 
        onfocus ="Change(this, event)" onblur ="Change(this, event)" Text='<%# Eval("prgmname") %>'
        CssClass="roundedbox" style="color: #808080;" 
        ></asp:TextBox>
                            </EditItemTemplate>--%><HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                         <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="ProgramType" HeaderStyle-HorizontalAlign="Center">
                        
                            <ItemTemplate>
                                <asp:Label ID="lblPgmType" runat="server" Text='<%# Eval("prgmtypName") %>'></asp:Label>
                            </ItemTemplate>
                          <%-- <EditItemTemplate>
                           <asp:DropDownList ID="ddl_PgmType" runat="server" Width="200px" onfocus ="Change(this, event)" onblur ="Change(this, event)" CssClass="roundedbox" style="color: #808080;">
    <asp:ListItem Value="0">Select</asp:ListItem>
    <asp:ListItem Value="1">Training 1</asp:ListItem>
    <asp:ListItem Value="2">Training 2</asp:ListItem>
    </asp:DropDownList>
                           </EditItemTemplate>--%>

        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                      
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Summary" HeaderStyle-HorizontalAlign="Center">                        
                            <ItemTemplate>
                                <asp:Label ID="lblsum" runat="server" Text='<%# Eval("temp_str") %>'></asp:Label>
                            </ItemTemplate>    
                            <%--<EditItemTemplate>
                             <asp:TextBox ID="txtsum" runat="server" Text='<%# Eval("temp_str") %>' Width="200px" 
        onfocus ="Change(this, event)" onblur ="Change(this, event)" 
        CssClass="roundedbox" style="color: #808080;" ></asp:TextBox>
                            </EditItemTemplate>       --%>               
        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>    
                          <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Status" HeaderStyle-HorizontalAlign="Center">
                        
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("IDno") %>'></asp:Label>
                            </ItemTemplate>
                           <EditItemTemplate>
                           <asp:DropDownList ID="ddl_Status" runat="server" Width="100px" onfocus ="Change(this, event)" onblur ="Change(this, event)" CssClass="roundedbox" style="color: #808080;">
    <asp:ListItem Value="0">Select</asp:ListItem>
    <asp:ListItem Value="1">Approved</asp:ListItem>
    <asp:ListItem Value="2">Rejected</asp:ListItem>
    </asp:DropDownList>
                           </EditItemTemplate>

        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                         <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Reason" HeaderStyle-HorizontalAlign="Center">                        
                            <ItemTemplate>
                                <asp:Label ID="lblReason" runat="server" Text='<%# Eval("Reason") %>'></asp:Label>
                            </ItemTemplate>    
                            <EditItemTemplate>
                             <asp:TextBox ID="txtReason" runat="server" Text='<%# Eval("Reason") %>' Width="150px" TextMode="MultiLine" 
        onfocus ="Change(this, event)" onblur ="Change(this, event)" 
        CssClass="roundedbox" style="color: #808080;" ></asp:TextBox>
                            </EditItemTemplate>                      
        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>    
                        <asp:CommandField ShowEditButton="True"/>
                    </Columns>
                                   <FooterStyle BackColor="#5D7B9D" Font-Bold="True" 
                        ForeColor="White" />
                                   <PagerStyle BackColor="#284775" ForeColor="White" 
                        HorizontalAlign="Center" />
                    <EmptyDataTemplate>
                    <asp:Label ID="lblempty" Text="No Records" runat="server">
                    </asp:Label> 
                    
                    </EmptyDataTemplate>
                                   <SelectedRowStyle  Font-Bold="True" 
                         Wrap="True" />
                                   <HeaderStyle Font-Bold="True" 
                                       Font-Names="Calibri" BorderStyle="None" 
                                       Font-Size="Small" />
                    <EditRowStyle  />
                    <AlternatingRowStyle  />
                </asp:GridView>
</div></td>
</tr>
</table>
</td></tr>
</table>
</asp:Content>
