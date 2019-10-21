<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Employee_WorkDetails.aspx.cs" Inherits="Hrms_Employee_Default4" Title="Welcome to HRMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">

 function fn_date(event,txtid)
 {  
       var len;
       var txtvalue; 
       var bool_obj; 
       var i;    
       txtvalue= document.getElementById(txtid).value;
       txtlen=txtvalue.length;  
       
       if (event.keyCode != 8 && event.keyCode != 46 && event.keyCode != 35 && event.keyCode != 36 && event.keyCode != 37 && event.keyCode != 38 && event.keyCode != 39 && event.keyCode != 40) {

           if (txtlen != 0) {
               bool_obj = fn_validate(txtlen, txtvalue);
               if (bool_obj == true) {
                   if (txtlen == 2 || txtlen == 5) {
                       document.getElementById(txtid).value = txtvalue + "/";
                   }
                   else {
                       document.getElementById(txtid).value = txtvalue;
                   }
               }
               else {
                   document.getElementById(txtid).value = txtvalue.substring(0, txtlen - 1);
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
    <div><h3 class="page-header">Work Details&nbsp;</h3></div>
<div align="center">
    <asp:Label ID="lbl_empcodename" runat="server" Font-Bold="True" 
        Font-Size="Medium"></asp:Label>
    </div>
    <div>
     <div style="float:left;">&nbsp; Entry Date&nbsp;&nbsp; </div>
                            
                              <%--  <input id="txt_date" type="text" runat="server" onkeyup="fn_date(event,this.id);" />--%>

                                <div style=" width:150px; float:left;">

                                    <asp:TextBox ID="txt_date" runat="server" class="form-control" Width="150px" 
                                        CausesValidation="True"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_date" Format="dd/MM/yyyy">
                                    </asp:CalendarExtender>

                                </div>

                                <div style=" width:25px; float:left;  margin-left:10px; margin-top:3px;">                                                
                                                    <asp:Image ID="Image3" runat="server"  
                                                        Text="" Width="25px" 
                                                        ImageUrl="~/Images/calendaricon.png" />
                                 </div>

                              


                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_date"
                                ErrorMessage="Enter Date" Font-Names="Calibri"></asp:RequiredFieldValidator>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </div>
            <br />
   <%-- <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div style="width: 70%">
                    <table cellpadding="1%" cellspacing="1%" width="100%" class="table table-striped table-bordered table-hover">
                        
                        <tr>
                            <td>
                                Division</td>
                            <td >
                                <asp:DropDownList ID="ddl_Division" runat="server" CssClass="form-control" 
                                    Font-Names="Calibri">
                                </asp:DropDownList>
                            </td>
                            <td >
                                Category</td>
                            <td>
                                <asp:DropDownList ID="ddl_Category" runat="server" CssClass="form-control" 
                                    Font-Names="Calibri">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Department</td>
                            <td>
                                <asp:DropDownList ID="ddl_Department" runat="server" CssClass="form-control" Font-Names="Calibri">
                                </asp:DropDownList>
                            </td>
                            <td>
                                Level</td>
                            <td>
                                <asp:DropDownList ID="ddl_Level" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Designation</td>
                            <td>
                                <asp:DropDownList ID="ddl_Designation" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </td>
                            <td>
                                Grade</td>
                            <td>
                                <asp:DropDownList ID="ddl_Grade" runat="server" CssClass="form-control" >
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Shift</td>
                            <td>
                                <asp:DropDownList ID="ddl_Shift" runat="server" CssClass="form-control" >
                                </asp:DropDownList>
                            </td>
                            <td>
                                Job Type</td>
                            <td>
                                <asp:DropDownList ID="ddl_JobStatus" runat="server" CssClass="form-control" >
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Project Site</td>
                            <td>
                                <asp:DropDownList ID="ddl_Project" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </td>
                            <td>
                                Reporting Designation</td>
                            <td>
                                <asp:DropDownList ID="ddl_report" runat="server" CssClass="form-control">                                    
                                </asp:DropDownList>
                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Comment (if any)</td>
                            <td>
                                <asp:TextBox ID="txt_reason" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="4" align="right">
                                <asp:Button ID="btn_Back" runat="server" class="btn btn-info" 
                                    OnClick="btn_Back_Click" Text="Back" />
                                <asp:Button ID="btn_skip" runat="server" CausesValidation="False" 
                                    class="btn btn-warning" OnClick="btn_skip_Click" Text="Skip" ToolTip="Skip" />
                                <asp:Button ID="btn_save" runat="server" class="btn btn-success" 
                                    OnClick="btn_save_Click" Text="Save" />
                                <asp:Button ID="btn_update" runat="server" class="btn btn-success" 
                                    OnClick="btn_update_Click" Text="Update" />
                            </td>
                        </tr>
                     </table>   
                     </div>
                     <div>  

                         </td>                                    
           </div>
    </ContentTemplate>
    </asp:UpdatePanel>


    </asp:Content>
