<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Employee_Deductions.aspx.cs" Inherits="Bank_Loan_Default" Title="EmployeeVSDeductions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>
<script type="text/javascript" language="javascript">
function fn_save()
{
 if(document.aspnetForm.ctl00$ContentPlaceHolder1$Txtamount.value == "")
        {
            alert("Enter Amount");
            aspnetForm.ctl00$ContentPlaceHolder1$Txtamount.focus();
            return false;
        }
        else
        { 
            return true;  
        }

}

 function fn_date(event,txtid)
 {  
       var len;
       var txtvalue; 
       var bool_obj; 
       var i;    
          
       txtvalue= document.getElementById(txtid).value;
       txtlen=txtvalue.length;  
       
  if(event.keyCode!=8 && event.keyCode!=46 && event.keyCode!=35 && event.keyCode!=36 && event.keyCode!=37 && event.keyCode!=38 && event.keyCode!=39 && event.keyCode!=40)     
   {    
           if(txtlen!=0)
           {       
               bool_obj=fn_validate(txtlen,txtvalue);
                          
               if(bool_obj==true)
                 {
                      if(txtlen==2 || txtlen==5)
                      {
                      document.getElementById(txtid).value=txtvalue+"/";
                      }
                      else
                      {
                      document.getElementById(txtid).value=txtvalue;
                      }
                 }
                 else
                 {            
                   document.getElementById(txtid).value= txtvalue.substring(0,txtlen-1);              
                 }                       
            } 
    }                                 
 }
 
 function fn_validate(len,tval)
    {
    var str;
    
          switch(len)
           {
     
        case 1: if(tval<=3)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                  
                  
        case 2:               
                
                if(tval<=31 && tval>0)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
                
        case 3: str=tval.charAt(2);
        
                if(str=="/")
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
      case 4: str=tval.charAt(3);
        
                if(str<=1)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
      case 5: str=tval.substring(3,5); 
        
                if(str<=12 && str>0)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
       case 6: str=tval.charAt(5);
        
                if(str=="/")
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
        case 7: str=tval.charAt(6);
        
                if(str<=9 && str>0)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
        case 8: str=tval.substring(6,8);
        
                if(str>=18)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
        case 9: str=tval.charAt(8);
        
                if(str<=9)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
        case 10: str=tval.charAt(9);
        
                if(str<=9)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
                
        default :return false;   
                 break;
           }
        
    }
</script>


<div><h2 class="page-header">Employee Vs Deduction</h2></div>
<div><asp:Label ID="lbl_Error" runat="server" style="text-align: center"></asp:Label><div style="float:left;">
     <asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" 
                            CssClass="form-control" 
                            OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged">
                        </asp:DropDownList></div></div>

   
    <table width="100%" id="tbl_deductions" runat="server" class="table">
        
        <tr>
            
            <td>
                Period Code </td>
            <td>
                <asp:DropDownList ID="ddl_periodcode" runat="server" 
                    CssClass="form-control" 
                    onselectedindexchanged="ddl_periodcode_SelectedIndexChanged" 
                    AutoPostBack="True">
                    <asp:ListItem>Select</asp:ListItem>
                   
                </asp:DropDownList>
            </td>
            
        </tr>
        <tr>
            <td >
                From Date </td>
            <td>
                <asp:TextBox ID="txt_fromdate" runat="server" CssClass="form-control" onkeyup="fn_date(event,this.id);"
                    MaxLength="10" Enabled="False"></asp:TextBox>
            </td>
         
        </tr>
        <tr>
            <td>
                To Date </td>
            <td>
                <asp:TextBox ID="txt_todate" runat="server" CssClass="form-control" onkeyup="fn_date(event,this.id);" 
                    MaxLength="10" Enabled="False"></asp:TextBox>
            </td>
            
        </tr>
        <tr>
            <td>
                Select Department </td>
            <td>
                <asp:DropDownList ID="ddl_department" runat="server" CssClass="form-control"
                    onselectedindexchanged="ddl_department_SelectedIndexChanged" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
            
        </tr>
        <tr>
            <td>
                Select Employee </td>
            <td>
                <asp:DropDownList ID="ddl_Employee" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </td>
            
        </tr>
        <tr>
            
            <td  colspan="2">
                <asp:Button ID="Btn_calc" runat="server" onclick="Btn_calc_Click" 
                    Text="Calculate"   CssClass="btn btn-warning"/>
            </td>
            
        </tr>
       
        <tr id="tr_chk" runat="server">
            <td  colspan="2">                   
                               <asp:GridView ID="GridView1"  runat="server" AllowSorting="True"  CssClass="table table-striped table-bordered table-hover"
                    AutoGenerateColumns="False" ShowFooter="True" Width="50%" 
                                   onrowcommand="GridView1_RowCommand" CellPadding="4" 
               onrowdeleting="GridView1_RowDeleting" 
                                   onrowdatabound="GridView1_RowDataBound" onrowediting="GridView1_RowEditing" 
                    onselectedindexchanged="GridView1_SelectedIndexChanged" HorizontalAlign="Center" 
                                   onrowupdating="GridView1_RowUpdating"  
                                   onrowcancelingedit="GridView1_RowCancelingEdit" 
                                   GridLines="None">
                                  
                    <Columns>
                    
                        <asp:TemplateField >
                        
                            <ItemTemplate>
                                <asp:Label ID="lbl_all" Visible="false" runat="server" Text='<%# Eval("pn_deductionID") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                            <asp:Label ID="lbl_alledit" runat="server" Visible="false" Text='<%# Bind("pn_deductionID") %>'></asp:Label>
     
                            </EditItemTemplate>

                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        
                        </asp:TemplateField>
                        
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Deductions" HeaderStyle-HorizontalAlign="Left">
                        
                            <ItemTemplate>
                                <asp:Label ID="lbl_allname" runat="server" Text='<%# Eval("v_deductionCode") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                            <asp:Label ID="ddl_eallowance" runat="server" Text='<%# Bind("v_deductionCode") %>'></asp:Label>
     
                            </EditItemTemplate>
                            <FooterTemplate>
                            <asp:DropDownList ID="ddl_allowance" runat="server"  CssClass="form-control" ></asp:DropDownList>
                                
                            </FooterTemplate>

                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        
                        </asp:TemplateField>
                        
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Amount"  HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("Amount","{0:##.##}") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                            <asp:TextBox ID="txt_eamount" runat="server" Text ='<%# Bind("Amount","{0:##.##}") %>' CssClass="form-control" ></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                            <table><tr><td>
                                <asp:TextBox ID="txt_amount" runat="server" CssClass="form-control" ></asp:TextBox></td><td>
                                <asp:Button ID="Button1" CommandName="add" runat="server" Text="Add"  CssClass="btn btn-success"
                                   /></td></tr></table>
                            </FooterTemplate>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
        
                        </asp:TemplateField>

                        
                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    </Columns>
                                   
                    <EmptyDataTemplate>
                    <asp:Label ID="lblempty" Text="No Records" runat="server">
                    </asp:Label> 
                    
                    </EmptyDataTemplate>
                                
                </asp:GridView>
                 <asp:SqlDataSource ID="SqlDataSourceemp" runat="server" ConnectionString="<%$ ConnectionStrings:connectionstring %>"    
                    SelectCommand="SELECT [v_deductionCode],[pn_deductionID] FROM [paym_deduction] WHERE ([pn_BranchID] = @pn_BranchID)">
                    <SelectParameters>
                        <asp:SessionParameter Name="pn_BranchID" SessionField="Login_Temp_BranchID" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                   
            </td>
        </tr>
        </table>
    
   
</asp:Content>