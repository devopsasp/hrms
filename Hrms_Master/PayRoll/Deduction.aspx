<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="Deduction.aspx.cs" Inherits="Hrms_Master_Default8" Title="Welcome to HRMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="../../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../../Scripts/Datavalid.js"></script>

<script language=javascript type="text/javascript">

    
    function show_message()
    {
        alert("Deduction Code Already Exist");
    }
    
    function show_message1()
    {
        alert("Deduction Name Already Exist");
    }
    
    function show_Error()
    {
        alert("Enter Deduction Name");
    }
      
    function fnSave()
    {  
     
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$txtDeductionCode.value == "")
        {
            alert("Enter Deduction Code");
            aspnetForm.ctl00$ContentPlaceHolder1$txtDeductionCode.focus();
            return false;
        }         
        else
        {
            if(document.aspnetForm.ctl00$ContentPlaceHolder1$txtDeductionName.value == "")
            {
                alert("Enter Deduction Name");
                aspnetForm.ctl00$ContentPlaceHolder1$txtDeductionName.focus();
                return false;
            }
            else
            {      
               return true;
            }   
        }
    }
</script>
                     <div ><h3 class="page-header">Deduction</h3></div>
                     <div></div>
                                    <div style="width: 80%">
                                    <table cellpadding="1%" cellspacing="1%" width="100%" class="table table-striped table-bordered table-hover">
                        
                        <tr>
                            <td style="height: 48px">
                                Deduction Code</td>
                            <td style="height: 48px" >
                                <input class="form-control" runat="server" id="txtDeductionCode" 
                                    maxlength="10" /></td>
                            <td style="height: 48px" >
                                Deduction Name</td>
                            <td style="height: 48px" >
                                <input class="form-control" runat="server" id="txtDeductionName" 
                                    onkeydown="AllowOnlyText1(event);" 
                                    maxlength="20" /></td>
                        </tr>
                        <tr>
                            <td >
                                Pre-Defined Master</td>
                            <td >
                                <asp:CheckBox ID="chk_Deducations" runat="server"/></td>
                            <td >
                                Deduction Order</td>
                            <td >
                                <input class="form-control" runat="server" id="txt_d_order" 
                                    onkeydown="AllowOnlyNumeric1(event);"
                                /></td>
                        </tr>
                        <tr>
                            <td colspan="4" align="right">
                                <asp:Button ID="btn_save" runat="server" class="btn btn-success" onclick="btn_save_Click" Text="Save" />
                               </td>
                        </tr>
                     </table>
                     </div>
                     <div style="width: 80%">
                     <table style="width: 100%">
                       <tr valign="top">
                         <td>
                           <asp:GridView ID="grid_Deduction" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                  DataKeyNames="DeductionId" 
                                 OnRowEditing="Edit" OnRowUpdating="Update" CellPadding="4" 
                                 GridLines="None" 
                                 onrowdeleting="Delete" 
                                >                             
                             <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <table cellspacing="0" cellpadding="0" >
                                            <colgroup>        
                                                <col>                                
                                            </colgroup>
                                            <thead>
                                                <tr>
                                                    <th >Deduction List</th>                                                    
                                                </tr>                                                
                                            </thead>
                                       </table>
                                   </HeaderTemplate>
                                   <ItemTemplate>
                                         <table cellspacing="0" cellpadding="0" >                                           
                                            <tbody>
                                                <tr>
                                                    <td style="width:5%;" align="center"><input type="checkbox" ID="Chk_Deduction" runat="server" visible="false"/></td>                                                    
                                                    <td style="width:30%;" nowrap="nowrap"><asp:TextBox runat="server" Text='<%#Eval("DeducationCode")%>' ID="grd_Dcode" Enabled="false" CssClass="form-control" ></asp:TextBox> </td>
                                                  
                                                     <td style="width:2%;" align="center"></td>  <td style="width:30%;" nowrap="nowrap">
                                                    <input runat="server" id="grd_DName" onkeydown="AllowOnlyText1(event);" value='<%#Eval("DeductionName")%>' disabled="disabled" class="form-control" maxlength="30" />
                                                    </td>                                                  
                                                    <td style="width:10%;" align="center"><asp:CheckBox runat="server" ID="chk_Ded" Enabled="false" /></td> 
                                                    <td style="width:10%;" nowrap="nowrap"><asp:TextBox runat="server" CssClass="form-control"
                                                            Text='<%#Eval("d_order")%>' ID="txt_order" Enabled="false"  ></asp:TextBox></td>                                                                                                     
                                                    <td align="center" style="width:5%;">
                                                    <asp:LinkButton id="img_update"  runat="server"  AlternateText="" class="btn btn-sm btn-info" CommandName="Update"><i class="glyphicon glyphicon-pencil"></i></asp:LinkButton>
                                                    <asp:LinkButton id="img_save" runat="server" CommandName="Edit" class="btn btn-sm btn-success" Visible="false" ><i class="glyphicon glyphicon-floppy-saved"></i></asp:LinkButton>
                                                    </td>
                                                     <td style="width:5%; " align="center">
                                                                <asp:LinkButton ID="imgdel" CssClass="btn btn-sm btn-danger" runat="server" CommandName="Delete" OnClientClick="return validate()"><span class="glyphicon glyphicon-trash"></span></asp:LinkButton>
                                                     </td>
                                                     </tr>
                            
                                            </tbody>                            
                                        </table> 
                                   </ItemTemplate>
                               </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        </td>
                    </tr>
                         <tr valign="top">
                             <td >
                        <asp:GridView ID="grid_Branch" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="table table-striped table-bordered table-hover"
                                     DataKeyNames="CompanyId"
                                     CellPadding="4" GridLines="None">
                                <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <table cellspacing="0" cellpadding="0" width="100%">
                                            
                                            <thead>
                                                <tr>
                                                    <th >Branch List</th>
                                                </tr>
                                            </thead>
                                       </table>
                                   </HeaderTemplate>
                                   <ItemTemplate>
                                         <table cellspacing="0" cellpadding="0" width="100%">
                                            <colgroup>        
                                                                             
                                            </colgroup>
                                            <tbody>
                                                <tr>
                                                    <td style="width:10%;" align="center"><input type="checkbox" ID="Chk_Branch" runat="server" /></td>                                                    
                                                    <td style="width:80%;" nowrap="nowrap"><%#Eval("CompanyName")%></a></td>
                                                </tr>
                                            </tbody>                            
                                        </TABLE> 
                                   </ItemTemplate>
                               </asp:TemplateField>
                            </Columns>                         
                              
                        </asp:GridView>
                             </td>
                         </tr>
                  </table>
                  </div>
                  <asp:ImageButton ID="assign" ImageUrl="~/Images/Assign.png" onmouseover="this.src='../../Images/Assignover.png';" onmouseout="this.src='../../Images/Assign.png';" runat="server" OnClick="assign_Click"  /></td>                                    
           
</asp:Content>