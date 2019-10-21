<%@ Page Language="C#" MasterPageFile="~/HRMS.master" CodeFile="Employee_Earnings.aspx.cs" Inherits="Bank_Loan_Default" Title="EmployeeVSAllowance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>
<link href="../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    window.onload = function () {
        var seconds = 5;
        setTimeout(function () {
            document.getElementById("<%=lbl_Error.ClientID %>").innerHTML = "";
        }, seconds * 1000);
    };
</script>

<script language="javascript" type="text/javascript">

    function fn_chkall(chkid, chklistid) {

        var chkBoxList = document.getElementById(chklistid);
        var chkBoxCount = chkBoxList.getElementsByTagName("input");

        if (document.getElementById(chkid).checked == true) {
            for (var i = 0; i < chkBoxCount.length; i++) {
                chkBoxCount[i].checked = true;
            }
        }
        else {
            for (var i = 0; i < chkBoxCount.length; i++) {
                chkBoxCount[i].checked = false;
            }
        }


    }       
    
    </script>

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
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
<div><h2 class="page-header">Employee Vs Allowance<asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" 
                            CssClass="form-control" 
                            OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged">
                        </asp:DropDownList></h2></div>
<div><asp:Label ID="lbl_Error" runat="server" style="text-align: center"></asp:Label><div style="float:left;"> </div>
    <asp:SqlDataSource ID="SqlDataSourceemp" runat="server" 
        ConnectionString="<%$ ConnectionStrings:connectionstring %>" 
        SelectCommand="SELECT * FROM [Paym_Earnings] WHERE ([pn_BranchID] = @pn_BranchID)">
        <SelectParameters>
            <asp:SessionParameter Name="pn_BranchID" SessionField="Login_Temp_BranchID" 
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    </div>
   <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                            <div class="divWaiting">
                            
                            <asp:Image ID="imgWait" runat="server" ImageAlign="Middle" 
                                    ImageUrl="~/Images/loading2.gif" Height="100px" Width="100px" />
                               
                            </div>
                            </ProgressTemplate>
                            </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <fieldset style="border-style: none">

    <table width="100%" id="tbl_deductions" runat="server" class="table">
        
        <tr>
            
            <td>
                Period Code </td>
            <td>
                <asp:DropDownList ID="ddl_periodcode" runat="server" AutoPostBack="True" 
                    CssClass="form-control" 
                    onselectedindexchanged="ddl_periodcode_SelectedIndexChanged">
                    <asp:ListItem>Select</asp:ListItem>
                </asp:DropDownList>
            </td>
            
            <td id="Row_Emplist" runat="server" rowspan="6">
                 <div class="qrychkbox_big" style="height: 250px; left: 0px; top: 0px;">
                    <asp:CheckBoxList ID="chk_Empcode" runat="server" CssClass="InputDefaultStyle1"
                                    Width="100%">
                                </asp:CheckBoxList>
                        </div>
                        <input type="checkbox" id="chkall" runat="server" onclick="javascript: fn_chkall(this.id,'ctl00_ContentPlaceHolder1_chk_Empcode')" checked="checked" />
                        Select All
                
                </td>
            
        </tr>
        <tr>
            <td >
                From Date </td>
            <td>
                <asp:TextBox ID="txt_fromdate" runat="server" CssClass="form-control" 
                    Enabled="False" ReadOnly="True"></asp:TextBox>
            </td>
         
        </tr>
        <tr>
            <td>
                To Date </td>
            <td>
                <asp:TextBox ID="txt_todate" runat="server" CssClass="form-control" 
                    Enabled="False" ReadOnly="True"></asp:TextBox>
            </td>
            
        </tr>
        <tr>
            <td>
                Select Department </td>
            <td>
                <asp:DropDownList ID="ddl_department" runat="server" AutoPostBack="True" 
                    CssClass="form-control" Enabled="False" 
                    onselectedindexchanged="ddl_department_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            
        </tr>
        <tr>
            <td>
                Select Employee </td>
            <td>
                <asp:DropDownList ID="ddl_Employee" runat="server" AutoPostBack="True" 
                    CssClass="form-control" Enabled="False" 
                    onselectedindexchanged="ddl_Employee_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            
        </tr>
        <tr>
            
            <td  colspan="2">
                <asp:Button ID="Btn_calc" runat="server" CssClass="btn btn-warning" 
                    onclick="Btn_calc_Click1" Text="Calculate" />
            </td>
            
        </tr>
       
        <tr id="tr_chk" runat="server">
            <td  colspan="2">                   
                               <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
                                   AutoGenerateColumns="False" 
                                   CssClass="table table-striped table-bordered table-hover" GridLines="None" 
                                   onrowcancelingedit="GridView1_RowCancelingEdit" 
                                   onrowcommand="GridView1_RowCommand" onrowdatabound="GridView1_RowDataBound" 
                                   onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing" 
                                   onrowupdating="GridView1_RowUpdating" Width="80%"
                                   onselectedindexchanged="GridView1_SelectedIndexChanged" ShowFooter="True">
                                   <Columns>
                                       <asp:TemplateField>
                                           <ItemTemplate>
                                               <asp:Label ID="lbl_all" runat="server" Text='<%# Eval("pn_EarningsID") %>' 
                                                   Visible="false"></asp:Label>
                                           </ItemTemplate>
                                           <EditItemTemplate>
                                               <asp:Label ID="lbl_alledit" runat="server" Text='<%# Bind("pn_EarningsID") %>' 
                                                   Visible="false"></asp:Label>
                                           </EditItemTemplate>
                                           <HeaderStyle HorizontalAlign="Left" />
                                           <ItemStyle HorizontalAlign="Left" />
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Allowances" 
                                           ItemStyle-HorizontalAlign="Left">
                                           <ItemTemplate>
                                               <asp:Label ID="lbl_allname" runat="server" Text='<%# Eval("v_EarningsCode") %>'></asp:Label>
                                           </ItemTemplate>
                                           <EditItemTemplate>
                                               <asp:Label ID="ddl_eallowance" runat="server" 
                                                   Text='<%# Bind("v_EarningsCode") %>'></asp:Label>
                                           </EditItemTemplate>
                                           <FooterTemplate>
                                               <asp:DropDownList ID="ddl_allowance" runat="server" CssClass="form-control" 
                                                   DataSourceID="SqlDataSourceemp" DataTextField="v_EarningsCode" 
                                                   DataValueField="pn_EarningsID">
                                               </asp:DropDownList>
                                           </FooterTemplate>
                                           <HeaderStyle HorizontalAlign="Left" />
                                           <ItemStyle HorizontalAlign="Left" />
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Amount" 
                                           ItemStyle-HorizontalAlign="Left">
                                           <ItemTemplate>
                                               <asp:Label ID="Label12" runat="server" Text='<%# Eval("Amount","{0:##.##}") %>'></asp:Label>
                                           </ItemTemplate>
                                           <EditItemTemplate>
                                               <asp:TextBox ID="txt_eamount" runat="server" CssClass="form-control" 
                                                   Text='<%# Bind("Amount","{0:##.##}") %>'></asp:TextBox>
                                           </EditItemTemplate>
                                           <FooterTemplate>
                                               <asp:TextBox ID="txt_amount" runat="server" CssClass="form-control"></asp:TextBox>
                                               <asp:Button ID="Button1" runat="server" CommandName="add" Font-Bold="True" 
                                                   Font-Names="Calibri" Font-Size="Small" ForeColor="Black" Height="23px" 
                                                   Text="ADD" />
                                           </FooterTemplate>
                                           <HeaderStyle HorizontalAlign="Left" />
                                           <ItemStyle HorizontalAlign="Left" />
                                       </asp:TemplateField>
                                       <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                                   </Columns>
                                   <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                   <EmptyDataTemplate>
                                       <asp:Label ID="lblempty" runat="server" Text="No Records">
                    </asp:Label>
                                   </EmptyDataTemplate>
                               </asp:GridView>
                   
            </td>
            <td>
                &nbsp;</td>
        </tr>
        </table>


    </fieldset>
    </ContentTemplate>
    </asp:UpdatePanel>
   
</asp:Content>
