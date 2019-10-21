<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="Earnings.aspx.cs" Inherits="Hrms_Master_Default7" Title="Welcome to HRMS" %>
<%--<link href="../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="../../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../../Scripts/Datavalid.js"></script>

<script language="javascript" type="text/javascript">

    function validate() {
        var r = confirm("Are you sure you want to delete this record?");
        if (r == true) 
        {
            return true;
        }
        else {
            return false;
        }
        }

   function show_message()
    {
        alert("Earnings Code Already Exist");
    }
    function show_message1()
    {
        alert("Order No. Already Exist");
    }
    
    function show_message2()
    {
        alert("Earnings Name Already Exist");
    }
    
    function show_Error()
    {
        alert("Enter Earnings Name");
    }
   
    function fnSave()
    {  
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$EarningsCode.value == "")
        {
            alert("Enter Earnings Code");
            aspnetForm.ctl00$ContentPlaceHolder1$EarningsCode.focus();
            return false;
        }
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_d_order.value == "")
        {
            alert("Enter d_order");
            aspnetForm.ctl00$ContentPlaceHolder1$txt_d_order.focus();
            return false;
        }
        else
        { 
            if(document.aspnetForm.ctl00$ContentPlaceHolder1$EarningsName.value == "")
            {
                alert("Enter Earnings Name");
                aspnetForm.ctl00$ContentPlaceHolder1$EarningsName.focus();
                return false;
            }
            else
            {      
               return true; 
            } 
        }
    }  
</script>
<div><h3 class="page-header">Allowances</h3></div>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div style="width: 70%">
                    <table cellpadding="1%" cellspacing="1%" width="100%" class="table table-striped table-bordered table-hover">
                        
                        <tr>
                            <td>
                                Allowance Code <span style="color: #FF3300">*</span></td>
                            <td >
                                <input class="form-control" runat="server" id="EarningsCode" maxlength="25" 
                                   /></td>
                            <td >
                                Allowance Name <span style="color: #FF3300">*</span></td>
                            <td >
                                <input class="form-control"  runat="server" id="EarningsName" 
                                    onkeydown="AllowOnlyText1(event);" maxlength="30" onkeypress="AllowOnlyText3();"  /></td>
                        </tr>
                        <tr>
                            <td>
                                Allowance Type <span style="color: #FF3300">*</span></td>
                            <td>
                                <asp:DropDownList ID="ddl_earntype" runat="server" CssClass="form-control" Width="90%">
                                    <asp:ListItem Value="Y">Monthly</asp:ListItem>
                                    <asp:ListItem Value="N">Custom</asp:ListItem>
                                    <%--<asp:ListItem Value="S">Statutory</asp:ListItem>--%>
                                </asp:DropDownList>
                            </td>
                            <td>
                                Allowance Order <span style="color: #FF3300">*</span></td>
                            <td>
                                <input class="form-control" runat="server" id="txt_d_order" 
                                    onkeydown="AllowOnlyNumeric1(event);" maxlength="3" />
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Eligiblity Criteria <span style="color: #FF3300">*</span></td>
                            <td >
                                <asp:RadioButtonList ID="rdo_Earnings" runat="server" 
                                    RepeatDirection="Horizontal" Width="100%">
                                    <asp:ListItem Value="0">PF</asp:ListItem>
                                    <asp:ListItem Value="1">ESI</asp:ListItem>
                                    <asp:ListItem Value="2">PF &amp; ESI</asp:ListItem>
                                     <asp:ListItem Value="3" Selected="True">None</asp:ListItem>
                                </asp:RadioButtonList></td>
                            <td >
                                Include for</td>
                            <td >
                                <asp:CheckBoxList ID="check_earn" runat="server" RepeatDirection="horizontal" Width="100%" 
                                    >
                                    <asp:ListItem Value="1">OT</asp:ListItem>
                                    <asp:ListItem Value="2" Selected="True">LOP</asp:ListItem>
                                    <asp:ListItem Value="3">PT</asp:ListItem>
                             </asp:CheckBoxList></td>
                        </tr>
                        <tr>
                            <td colspan="4" align="right">
                                <asp:Button ID="btn_save" runat="server" class="btn btn-success" 
                                    onclick="btn_save_Click" Text="Save"/>
                               </td>
                        </tr>
                     </table>   
                     </div>
                     <div>  

                           <asp:GridView ID="grid_Earnings" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                 Width="100%" DataKeyNames="EarningsId" OnRowDeleting="Delete" 
                               OnRowEditing="Edit" OnRowUpdating="Update" 
                               CellPadding="4"  
                               >                                                             
                             <Columns>
                             <asp:TemplateField>
                                <HeaderTemplate>
                                S.No.</HeaderTemplate>
                                <ItemTemplate>
                                <asp:Label ID="lblSRNO" runat="server" 
                                    Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="16px" />
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Allowance List">
                                    
                                   <ItemTemplate>
                                         <TABLE  cellSpacing="0" cellPadding="0" width="100%" border="0">
                                            
                                            <TBODY>
                                                <TR>
                                                    <TD ><input type="checkbox" ID="Chk_Earnings" runat="server" visible="false"/></td>                                                   
                                                    <TD ><asp:TextBox runat="server" Text=<%#Eval("EarningsCode")%> ID="grd_ECode" 
                                                            Enabled="false" CssClass="form-control" Width="100px"></asp:TextBox> </td>                                                   
                                                    <TD >
                                                    <input runat="server" id="grd_EName" onkeydown="AllowOnlyText1(event);" value='<%#Eval("EarningsName")%>' disabled="disabled" 
                                                            class="form-control" maxlength="30" />
                                                    <%--<asp:TextBox runat="server" Text=<%#Eval("EarningsName")%> ID="grd_EName" Enabled="false" ></asp:TextBox> --%>
                                                    </td>
                                                    <TD><asp:DropDownList ID="grd_ddl" runat="server" CssClass="form-control" Enabled="false"> 
                                                    <asp:ListItem Value="Y">Monthly</asp:ListItem>
                                                    <asp:ListItem Value="N">Custom</asp:ListItem>
                                                    <%--<asp:ListItem Value="S">Statutory</asp:ListItem>--%></asp:DropDownList> </td>                                                   
                                                    <TD><asp:RadioButtonList ID="grd_Rdo" runat="server" RepeatDirection="Horizontal" Enabled="false" >
                                                    <asp:ListItem Value="0">PF</asp:ListItem>
                                                    <asp:ListItem Value="1">ESI</asp:ListItem>
                                                    <asp:ListItem Value="2">PF &amp; ESI</asp:ListItem>
                                                    <asp:ListItem Value="3" Selected="True">None</asp:ListItem>
                                                    </asp:RadioButtonList> </td>
                                                    
                                                    <td>
                                                        <asp:CheckBoxList ID="rdo_earn" runat="server" RepeatDirection="Horizontal" 
                                                            Enabled="False" >
                                                            <asp:ListItem  Value="0">LOP</asp:ListItem>
                                                            <asp:ListItem  Value="1">OT</asp:ListItem>
                                                            <asp:ListItem  Value="2">PT</asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </td>
                                                    <td>
                                                    <input runat="server" id="txt_order" onkeydown="AllowOnlyNumeric(event);" 
                                                            value='<%#Eval("d_order")%>' disabled="disabled" maxlength="2" style="width: 50px" class="form-control"  />
                                                    <%--<asp:TextBox runat="server" Text=<%#Eval("EarningsName")%> ID="grd_EName" Enabled="false" ></asp:TextBox> --%>
                                                    </td>
                                                    
                                                    <TD >

                                                       <asp:ImageButton ID="img_update" ImageUrl="~/Images/edit_icon.png" runat="server" CommandName="Update" />
                                                       <asp:LinkButton ID="img_save" CommandName="Edit" runat="server" CssClass="btn btn-xs btn-success " Visible="false"><i class="glyphicon glyphicon-saved"></i> Update</asp:LinkButton>

                                                       <asp:ImageButton ID="imgdel" ImageUrl="~/Images/delete_icon.jpg" runat="server" CommandName="Delete" OnClientClick="return validate()" />

                                                    <%--<asp:LinkButton id="img_update"  runat="server"  AlternateText="" class="btn btn-info btn-circle" CommandName="Update"><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                                    <asp:LinkButton id="img_save" runat="server" CommandName="Edit" class="btn btn-xs btn-success" Visible="false" ><i class="glyphicon glyphicon-saved"></i> Update</asp:LinkButton>
                                                     <asp:LinkButton ID="imgdel" CssClass="btn btn-danger btn-circle glyphicon glyphicon-minus-sign"  runat="server" CommandName="Delete" OnClientClick="return validate()"></asp:LinkButton>
                                                    --%>
                                                    </td>
                            </TD>
                                                    </tr>
                                            </TBODY>                            
                                        </TABLE> 
                                   </ItemTemplate>
                               </asp:TemplateField>
                            </Columns>                         
                        </asp:GridView>
                      
                        <asp:GridView ID="grid_Branch" runat="server" AutoGenerateColumns="False" 
                               Width="100%" class="table table-striped table-bordered table-hover"
                                     DataKeyNames="CompanyId" OnRowDeleting="Delete" OnRowEditing="Edit" 
                                     CellPadding="4" GridLines="None" 
                               >
                              
                                <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <table cellspacing="0" cellpadding="0" width="100%">
                                            
                                            <thead>
                                                <tr>
                                                    <th  style="width:80%;">Branch List</th>
                                                </tr>
                                            </thead>
                                       </table>
                                   </HeaderTemplate>
                                   <ItemTemplate>
                                         <table  cellspacing="0" cellpadding="0" width="100%">
                                       
                                            <tbody>
                                                <tr>
                                                    <td style="width:10%;" align="left"><input type="checkbox" ID="Chk_Branch" runat="server" /></td>                                                    
                                                    <td style="width:80%;" nowrap="nowrap"><%#Eval("CompanyName")%></a></td>
                                                </tr>
                                            </tbody>                            
                                        </table> 
                                   </ItemTemplate>
                               </asp:TemplateField>
                            </Columns>                         
                        </asp:GridView>
                  <asp:ImageButton ImageUrl="~/Images/Assign.png"  onmouseover="this.src='../../Images/Assignover.png';" onmouseout="this.src='../../Images/Assign.png';" ID="Button2" runat="server" OnClick="Button2_Click" Text="Assign" /></td>                                    
           </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    

            <table>
            <tr valign="top">
                <td align="center" valign="top"><input type="hidden" id="ToolBarCode" name="ToolBarCode" runat="server" value="0" />
                    <input id="hEarningsID" runat="server" type="hidden" value="0" /></td>
            </tr>
            <tr>
            
                <td align="center">
                    &nbsp;</td>
            </tr>
        </table>
</asp:Content>