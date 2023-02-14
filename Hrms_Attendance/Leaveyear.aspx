<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="Leaveyear.aspx.cs" Inherits="Hrms_Company_Default" Title="Welcome to HRMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>
    <script language="javascript" type="text/javascript">
   
    function show_message()
    {
        alert("Department Name Already Exist");
    }
       
   function show_message1(msg)
    {
        alert(msg);
    }
    
    function fnSave()
    {   
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$DepartmentName.value == "")
        {
            alert("Enter Department Name");
            aspnetForm.ctl00$ContentPlaceHolder1$DepartmentName.focus();
            return false;
        }                        
        else
        { 
              return true;  
        }
    }    
</script>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div><h2 class="page-header">Leave Year End Process</h2>

            <div align="center">
                <asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged"
                           Width="35%"     CssClass="form-control">
                            </asp:DropDownList>
            </div>
     
    </div>

                                    <table cellpadding="0" cellspacing="1" align="center"  class="table"
                                                    style="width: 70%;">
                                                     
                                                        <tr id="row_fd" runat="server">
                                                            
                                                            <td  >
                                                                
                                                                Calendar Year Starting Date</td>
                                                            <td >
                                                                 <table><tr><td>
                                                                   <div style=" width:150px; float:left;">
                                                                <asp:TextBox ID="txt_fromDate" runat="server"  
                                                               CssClass="form-control" 
                                                                    onkeyup="fn_date(event,this.id);" AutoPostBack="True"  Width="150px"></asp:TextBox>
                                                                    </div>
                                                                    <div style=" width:25px; float:left;  margin-left:10px; margin-top:3px;">                                                
                                                                  <asp:Image ID="Image4" runat="server" Text="" Width="25px" ImageUrl="~/Images/calendaricon.png" />
                                                                  </div>

                                                                     <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txt_fromDate" Format="dd/MM/yyyy" runat="server"></ajaxToolkit:CalendarExtender>
                                                                    </td>
                                                                    
                                                                     </tr></table>
                                                            </td>
                                                            
                                                           
                                                        </tr>
                                                        <tr id="row_td" runat="server">
                                                           
                                                            <td >
                                                                
                                                                Calendar Year Ending Date</td>
                                                            <td >
                                                                 <table><tr><td>
                                                                   <div style=" width:150px; float:left;">
                                                                <asp:TextBox ID="txt_ToDate" runat="server"  
                                                               CssClass="form-control" 
                                                                    onkeyup="fn_date(event,this.id);" AutoPostBack="True"  Width="150px"></asp:TextBox>
                                                                    </div>
                                                                    <div style=" width:25px; float:left;  margin-left:10px; margin-top:3px;">                                                
                                                                  <asp:Image ID="Image2" runat="server" Text="" Width="25px" ImageUrl="~/Images/calendaricon.png" />
                                                                  </div>

                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txt_ToDate" 
                                                                    runat="server" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender></td>
                                                                    
                                                                     </tr></table>
                                                            </td>
                                                                  
                                                        </tr>
                                                        <tr>
                                                           
                                                            <td>
                                                                
                                                                Eligible
                                                                
                                                                Leave List</td>
                                                            <td 
                                                                >
                                <asp:ListBox ID="lb_Leave" runat="server" Width="351px"></asp:ListBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                <asp:Button ID="Btn_process" runat="server" CssClass="btn btn-success" 
                                                                    onclick="Btn_process_Click" Text="Process" />
                                                                <%--<asp:Button ID="Img_Delete" runat="server" CssClass="btn btn-danger" 
                                                                    onclick="Img_Delete_Click" OnClientClick="return validate()" Text="Delete" />--%>
                                                                <asp:Button ID="Img_Clear" runat="server" CssClass="btn btn-warning" Text="Clear" OnClick="Img_Clear_Click" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                          
                                                            <td  colspan="2" >
                                                                 &nbsp;</td>
                                                           
                                                        </tr>
                                                       
                                                    </table>



</asp:Content>

