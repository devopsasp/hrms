<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="Training_requisition.aspx.cs" Inherits="Hrms_Additional_Training_requisition" %>


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
         .style13
         {
             width: 241px;
             height: 33px;
         }

         .style16
         {
             height: 32px;
         }
         .style17
         {
             width: 241px;
             height: 32px;
         }

         .style18
         {
             height: 40px;
         }
         .style19
         {
             width: 241px;
             height: 40px;
         }

         .style20
         {
             width: 227px;
         }
         .style24
         {
             width: 241px;
         }
         .style25
         {
             width: 160px;
         }
         .style26
         {
             height: 33px;
             width: 160px;
         }
         .style27
         {
             height: 40px;
             width: 160px;
         }
         .style28
         {
             height: 32px;
             width: 160px;
         }
         .style29
         {
             height: 33px;
             width: 128px;
         }
         .style30
         {
             height: 40px;
             width: 128px;
         }
         .style31
         {
             height: 32px;
             width: 128px;
         }
         .style32
         {
             height: 33px;
             width: 100px;
         }
         .style33
         {
             height: 40px;
             width: 100px;
         }
         .style34
         {
             height: 32px;
             width: 100px;
         }
         .style35
         {
             width: 100px;
         }
         .style36
         {
             width: 128px;
         }
         .style37
         {
             height: 8px;
         }

 </style>
<table cellpadding="0%" cellspacing="0%" width="100%" style="height: 30px">
<tr> <td align="left" 
                  
        style="font-family:Calibri;font-size: small;color: #808080;" 
        class="style123" width="80%">
                           <span class="Title">
                           <span 
                                
                               style="font-family:Calibri;  font-size: medium; font-weight: bold;">&nbsp;&nbsp;&nbsp;Employee Training Details                             
                             </span></td> <td align="center" 
                  
        style="font-family:Calibri;font-size: small;color: #808080;" 
        class="style123">
                           <asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True"  CssClass="form-control"
                    >
                </asp:DropDownList>
            </td></tr>
</table>
<table cellpadding="0%" cellspacing="0%" width="100%">
<tr> <td colspan="2" align="center">
                            &nbsp;<asp:Label ID="lbl_Error" CssClass="Error" runat="server" ForeColor="Red" Font-Bold="True" Width="40%"></asp:Label></td></tr>
</table>
<table cellpadding="0%" cellspacing="0%" width="100%">
<tr>
<td class="style20" colspan="2"></td>

<td class="style24"></td>

<td class="style25"></td>

<td>&nbsp;</td>
</tr>
<tr>
<td align="right" style="font-family:Calibri;font-size: small;color: #808080;" 
        class="style32">&nbsp;</td>

<td align="left" style="font-family:Calibri;font-size: small;color: #808080;" 
        class="style29">Program ID</td>

<td align="left" class="style13">
   <%-- <asp:DropDownList ID="ddl_PrgmType" runat="server" Width="200px" 
        onfocus ="Change(this, event)" onblur ="Change(this, event)" 
        CssClass="roundedbox" style="color: #808080;">
    <asp:ListItem>Select</asp:ListItem>
    </asp:DropDownList>--%><asp:TextBox ID="txtid" runat="server" Width="200px" 
        onfocus ="Change(this, event)" onblur ="Change(this, event)" 
         CssClass="form-control" style="color: #808080;" 
        ></asp:TextBox>
    </td>

<td style="font-family:Calibri;font-size: small;color: #808080;" align="left" 
        class="style26">Program Name</td>

<td class="style12">
    <asp:TextBox ID="txtpgmname" runat="server" Width="200px" 
        onfocus ="Change(this, event)" onblur ="Change(this, event)" 
         CssClass="form-control" style="color: #808080;" 
        ></asp:TextBox>
    
    </td>
</tr>
<tr>
<td align="right" style="font-family:Calibri;font-size: small;color: #808080;" 
        class="style33">&nbsp;</td>

<td align="left" style="font-family:Calibri;font-size: small;color: #808080;" 
        class="style30">Institution Name</td>

<td align="left" class="style19">
    <asp:DropDownList ID="ddl_InstName" runat="server" Width="200px" 
        onfocus ="Change(this, event)" onblur ="Change(this, event)" 
         CssClass="form-control" style="color: #808080;" AutoPostBack="True" 
        onselectedindexchanged="ddl_InstName_SelectedIndexChanged">
    <asp:ListItem>Select</asp:ListItem>
    </asp:DropDownList>
    </td>

<td style="font-family:Calibri;font-size: small;color: #808080;" align="left" 
        class="style27">Name of the Trainer</td>

<td class="style18">
    <asp:DropDownList ID="ddl_TrainerName" runat="server" Width="200px" onfocus ="Change(this, event)" onblur ="Change(this, event)"  CssClass="form-control" style="color: #808080;">
    <asp:ListItem>Select</asp:ListItem>
    </asp:DropDownList>
    
    </td>

</tr>
<tr>
<td align="right" style="font-family:Calibri;font-size: small;color: #808080;" 
        class="style32">
    &nbsp;</td>

<td align="left" style="font-family:Calibri;font-size: small;color: #808080;" 
        class="style29">
    Duration From </td>

<td align="left" class="style13">
    <asp:TextBox ID="txtDurationFrom" runat="server" Width="200px" 
        onfocus ="Change(this, event)" onblur ="Change(this, event)" 
         CssClass="form-control" style="color: #808080;" onkeyup="fn_date(event,this.id);"></asp:TextBox>
    </td>

<td style="font-family:Calibri;font-size: small;color: #808080;" align="left" 
        class="style26">
    Duration To</td>

<td class="style12" style="font-family:Calibri;font-size: small;color: #808080;">
    <asp:TextBox ID="txtDurationTo" runat="server" Width="200px" 
        onfocus ="Change(this, event)" onblur ="Change(this, event)" 
         CssClass="form-control" onkeyup="fn_date(event,this.id);"></asp:TextBox>
    
    &nbsp;&nbsp; </td>

</tr>
<tr>
<td align="right" style="font-family:Calibri;font-size: small;color: #808080;" 
        class="style34">
    &nbsp;</td>

<td align="left" style="font-family:Calibri;font-size: small;color: #808080;" 
        class="style31">
    Hours Per Day</td>

<td align="left" class="style17">
    <asp:TextBox ID="txtHrs" runat="server" Width="200px" 
        onfocus ="Change(this, event)" onblur ="Change(this, event)" 
        CssClass="form-control"></asp:TextBox>
    </td>

<td style="font-family:Calibri;font-size: small;color: #808080;" align="left" 
        class="style28">
    Select Department</td>

<td class="style16">
    
    <asp:DropDownList ID="ddl_department" runat="server" Width="200px" 
        onfocus ="Change(this, event)" onblur ="Change(this, event)" 
        style="color: #808080;" AutoPostBack="True"  CssClass="form-control"
        onselectedindexchanged="ddl_department_SelectedIndexChanged">
    <asp:ListItem>Select</asp:ListItem>
    </asp:DropDownList>
    
    </td>

</tr>
<tr>
<td align="right" style="font-family:Calibri;font-size: small;color: #808080;" 
        class="style33">
    </td>

<td align="left" style="font-family:Calibri;font-size: small;color: #808080;" 
        class="style30">
    Training Cost</td>

<td align="left" class="style19">
    <asp:TextBox ID="txtcost" runat="server" Width="200px" 
        onfocus ="Change(this, event)" onblur ="Change(this, event)" 
         CssClass="form-control" ></asp:TextBox>
    </td>

<td style="font-family:Calibri;font-size: small;color: #808080;" align="left" 
        class="style27">
    </td>

<td class="style18">
    
    </td>

</tr>
<tr>
<td align="left" style="font-family:Calibri;font-size: small;color: #808080;" 
        class="style35" rowspan="2">
    &nbsp;</td>

<td align="left" style="font-family:Calibri;font-size: small;color: #808080;" 
        valign="top" class="style37">
    Summary</td>

<td align="left" valign="top" class="style37">
    
    <asp:TextBox ID="txtsummary" runat="server" Width="200px" 
        onfocus ="Change(this, event)" onblur ="Change(this, event)" 
        CssClass="roundedbox"  TextMode="MultiLine" 
        Height="75px"></asp:TextBox>
    
    </td>

<td style="font-family:Calibri;font-size: small;color: #808080;" align="left" runat="server" id="lbl" visible="false" 
                                                class="style25" rowspan="2">
    Select Employee</td>

<td rowspan="2">
    <div class="qrychkbox_big" style="height: 230px; left: 0px; top: 0px;" visible="false"  id="div_chkempcode" runat="server">
                                                              <asp:CheckBoxList Height="200px" ID="chk_Empcode" runat="server" CssClass="InputDefaultStyle1"
                                                                Width="90%">
                                                               
                                                            </asp:CheckBoxList>
                                                        </div>
                                                       
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                       
                                                        <input type="checkbox" visible="false" id="chkall" runat="server" onclick="javascript: fn_chkall(this.id,'ctl00_ContentPlaceHolder1_chk_Empcode')" />
                                                        <asp:Label ID="lbl_selectemp" CssClass="InputDefaultStyle1" Visible="false" runat="server" Text="Select All Candidate"></asp:Label> 
    
    </td>

</tr>
<tr>
<td align="left" style="font-family:Calibri;font-size: small;color: #808080;" 
        class="style36" valign="top">
    &nbsp;</td>

<td align="left" class="style24" valign="top">
    &nbsp;</td>

</tr>
</table>
<table cellpadding="0px" cellspacing="0px" width="100%">
<tr>
<td align="center" style="font-family:Calibri;font-size: small;color: #808080;" 
        class="style12">
    <asp:Button ID="ImageButton1" runat="server" Text="Add" OnClientClick="return check();" onclick="ImageButton1_Click"  class="btn btn-success" />
   <%--<asp:ImageButton ID="ImageButton1" runat="server" Height="31px"
     ImageUrl="~/Images/Add.jpg" Width="107px" OnClientClick="return check();" onclick="ImageButton1_Click" />--%>
     
     </td>
</tr>
</table>
<table  cellpadding="0px" cellspacing="0px" width="100%">
<tr><td><div>
<asp:GridView ID="GridView1" Font-Size="Smaller" runat="server" AllowSorting="True" 
                    AutoGenerateColumns="False" Height="16px" class="table table-striped table-bordered table-hover" 
        Width="100%" CellPadding="4" 
                    HorizontalAlign="Center"
        onrowediting="GridView1_RowEditing" 
        onprerender="GridView1_PreRender" onrowdatabound="GridView1_RowDataBound" 
        onrowcancelingedit="GridView1_RowCancelingEdit" 
        onrowdeleting="GridView1_RowDeleting" 
        onrowupdating="GridView1_RowUpdating">
                    <FooterStyle Font-Bold="True" />
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
                                <asp:Label ID="lblPgmName" runat="server" Text='<%# Eval("ProgramName") %>'></asp:Label>
                            </ItemTemplate>
                           

        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                       <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Duration From" HeaderStyle-HorizontalAlign="Center">
                        
                            <ItemTemplate>
                                <asp:Label ID="lblfromdate" runat="server" Text='<%# Eval("v_durationfrom","{0:dd/MM/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                           
                        <EditItemTemplate>
                                <asp:TextBox ID="txtfromdate" runat="server" onfocus ="Change(this, event)" onblur ="Change(this, event)" 
        CssClass="roundedbox" style="color: #808080;" Text='<%# Eval("v_durationfrom","{0:dd/MM/yyyy}") %>'  onkeyup="fn_date(event,this.id);"></asp:TextBox>
                        </EditItemTemplate>
        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Duration To" HeaderStyle-HorizontalAlign="Center">
                        
                            <ItemTemplate>
                                <asp:Label ID="lblTodate" runat="server" Text='<%# Eval("v_durationto","{0:dd/MM/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                              
 <EditItemTemplate>
                                <asp:TextBox ID="txtTodate" runat="server" onfocus ="Change(this, event)" onblur ="Change(this, event)" 
        CssClass="roundedbox" style="color: #808080;" Text='<%# Eval("v_durationto","{0:dd/MM/yyyy}") %>'  onkeyup="fn_date(event,this.id);"></asp:TextBox>
                        </EditItemTemplate>
        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Hours Per Day" HeaderStyle-HorizontalAlign="Center">
                        
                            <ItemTemplate>
                                <asp:Label ID="lblhrs" runat="server" Text='<%# Eval("TrainingHrs") %>'></asp:Label>
                            </ItemTemplate>
                              
 <EditItemTemplate>
                                <asp:TextBox ID="txthrs" runat="server" onfocus ="Change(this, event)" onblur ="Change(this, event)" 
        CssClass="roundedbox" style="color: #808080;" Text='<%# Eval("TrainingHrs") %>' ></asp:TextBox>
                        </EditItemTemplate>
        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Institution" HeaderStyle-HorizontalAlign="Center">
                        
                            <ItemTemplate>
                                <asp:Label ID="lblIstname" runat="server" Text='<%# Eval("ins_name") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
 <asp:DropDownList ID="ddl_dinst" runat="server" Width="100px" 
        onfocus ="Change(this, event)" onblur ="Change(this, event)" 
        CssClass="roundedbox" style="color: #808080;" AutoPostBack="True" OnSelectedIndexChanged="ddl_dinst_selectedindex_changed" >    <asp:ListItem>Select</asp:ListItem>
    </asp:DropDownList>                        </EditItemTemplate>

        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Trainer" HeaderStyle-HorizontalAlign="Center">                        
                            <ItemTemplate>
                                <asp:Label ID="lbltrainer" runat="server" Text='<%# Eval("fname") %>'></asp:Label>
                            </ItemTemplate>                           
  <EditItemTemplate>
 <asp:DropDownList ID="ddl_traner" runat="server" Width="100px" onfocus ="Change(this, event)" onblur ="Change(this, event)" CssClass="roundedbox" style="color: #808080;" AutoPostBack="True" >  
   <asp:ListItem>Select</asp:ListItem></asp:DropDownList>                    
       </EditItemTemplate>
        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        
                          <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Cost" HeaderStyle-HorizontalAlign="Center">                        
                            <ItemTemplate>
                                <asp:Label ID="lblcost" runat="server" Text='<%# Eval("TrainingCost") %>'></asp:Label>
                            </ItemTemplate>                           
                            <EditItemTemplate>
                                <asp:TextBox ID="txt_tCost" runat="server" onfocus ="Change(this, event)" onblur ="Change(this, event)" CssClass="roundedbox" style="color: #808080;" Text='<%# Eval("TrainingCost") %>'></asp:TextBox>
                        </EditItemTemplate>
        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                      
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Employee" HeaderStyle-HorizontalAlign="Center">                        
                            <ItemTemplate>
                                <asp:Label ID="lblemp" runat="server" Text='<%# Eval("EmployeeName") %>'></asp:Label>
                            </ItemTemplate>                          
        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>                                                                 
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Summary" HeaderStyle-HorizontalAlign="Center">                        
                            <ItemTemplate>
                                <asp:Label ID="lblsum" runat="server" Text='<%# Eval("v_summary") %>'></asp:Label>
                            </ItemTemplate>    
                            <EditItemTemplate>
                             <asp:TextBox ID="txtsum" runat="server" Text='<%# Eval("v_summary") %>'></asp:TextBox>
                            </EditItemTemplate>                      
        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>    
                        <asp:CommandField ShowEditButton="True" ShowDeleteButton="true"/>
                    </Columns>
                                   <PagerStyle BackColor="#284775" ForeColor="White" 
                        HorizontalAlign="Center" />
                    <EmptyDataTemplate>
                    <asp:Label ID="lblempty" Text="No Records" runat="server">
                    </asp:Label>                     
                    </EmptyDataTemplate>
                                   <SelectedRowStyle  Font-Bold="True" 
                         Wrap="True" />
                                   <HeaderStyle  Font-Bold="True" 
                                       Font-Names="Calibri" BorderStyle="None" 
                                       Font-Size="Small" />
                    <EditRowStyle  />
                    <AlternatingRowStyle  />
                </asp:GridView>
</div></td>
</tr>
</table>
</asp:Content>


