<%@ Page Language="C#" MasterPageFile="~/HRMS.master"  AutoEventWireup="true"
    CodeFile="Reimbursement.aspx.cs" Inherits="Hrms_Additional_Default" Title="Welcome to HRMS" %>

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
       
  if(event.keyCode!=8 && event.keyCode!=46 && event.keyCode!=35 && event.keyCode!=36 && event.keyCode!=37 && event.keyCode!=38 && event.keyCode!=39 && event.keyCode!=40)     
   {    
       if(txtlen!=0)
       {       
           bool_obj=true;
                      
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
     
    </script>
    
    <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style= height="35px" class="border">
                            <span class="Title">&nbsp;&nbsp;<span 
                                style="font-family: Calibri; ">Reimbursement</span></span></td>
                    </tr>
                </table>
                <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="lblerror" runat="server" Font-Names="Calibri" ForeColor="Red"></asp:Label>
            <br />
    <table id="tbt_emp" runat="server" border="2" align="center" 
        style="border-color: #C0C0C0; width: 331px; height: 263px;">
        <tr>
            <td style="width: 406px; ">
                <asp:Label ID="Label10" runat="server" Text="From Date" Font-Names="Calibri"></asp:Label>
                <span style="color: #FF0000">*</span></td>
            <td style="width: 200px" align="left">
                <asp:TextBox ID="txtfrom" runat="server" onkeyup="fn_date(event,this.id);"  
                    Width="181px"  BorderWidth="0px" MaxLength="10"  CssClass="form-control" 
                    AutoPostBack="True" ontextchanged="txtfrom_TextChanged"></asp:TextBox>
            </td>
            
        </tr>
        <tr>
            <td style="width: 406px; ">
                <asp:Label ID="Label11" runat="server" Text="To Date" Font-Names="Calibri"></asp:Label>
                <span style="color: #FF0000">*</span></td>
            <td style="width: 200px" align="left">
                <asp:TextBox ID="txtto" runat="server" onkeyup="fn_date(event,this.id);"  
                    Width="181px"  CssClass="form-control" BorderWidth="0px" AutoPostBack="True" 
                    MaxLength="10" ontextchanged="txtto_TextChanged"></asp:TextBox>
            </td>
            
        </tr>
        <tr>
            <td style="width: 406px;   height: 28px;">
                <asp:Label ID="Label2" runat="server" Text="Total Days" Font-Names="Calibri"></asp:Label>
                <span style="color: #FF0000">*</span></td>
            <td style="margin-left: 120px; width: 200px; height: 28px;" align="left">
                <asp:TextBox ID="txtdays" runat="server"  
                    Width="181px"  CssClass="form-control" BorderWidth="0px"></asp:TextBox>
            </td>
           
            
        </tr>
        <tr>
            <td style="width: 406px; ">
                <asp:Label ID="Label3" runat="server" Text="Travelling Mode" 
                    Font-Names="Calibri"></asp:Label>
                <span style="color: #FF0000">*</span></td>
            <td style="margin-left: 40px; width: 200px;" align="left">
                <asp:DropDownList ID="ddlmode" runat="server"  CssClass="form-control"
                     Height="26px" 
                    Width="181px" Font-Bold="False" Font-Names="Calibri" Font-Size="X-Small">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>Air</asp:ListItem>
                    <asp:ListItem>Sea</asp:ListItem>
                    <asp:ListItem>Road</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 406px;  ">
                <asp:Label ID="Label1" runat="server" Text="Destination" Font-Names="Calibri"></asp:Label>
                <span style="color: #FF0000">*</span></td>
            <td style="margin-left: 40px; width: 200px;" align="left">
                <asp:TextBox ID="txtdest" runat="server" Width="181px" BorderColor="#000066" 
                    BorderWidth="0px" ontextchanged="txtdest_TextChanged"  CssClass="form-control"
                    ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 406px;">
                <asp:Label ID="Label4" runat="server" Text="Total Expenses" 
                    Font-Names="Calibri"></asp:Label>
                <span style="color: #FF0000">*</span></td>
            <td style="width: 200px" align="left">
                <asp:TextBox ID="txtexp" runat="server" Width="181px" BorderColor="#000066"  CssClass="form-control"
                    BorderWidth="0px" ontextchanged="txtexp_TextChanged"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 406px;  ">
                <asp:Label ID="Label5" runat="server" Text="Purpose" Font-Names="Calibri"></asp:Label>
            </td>
            <td style="width: 200px" align="left">
                <asp:TextBox ID="txtpurpose" runat="server" TextMode="MultiLine"  CssClass="form-control"
                    BorderColor="#000066" BorderWidth="0px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="border-style: none; border-width: thin; width: 406px;  ">
                <asp:Label ID="Label6" runat="server" Text="Others" Font-Names="Calibri"></asp:Label>
            </td>
            <td style="width: 200px" align="left">
                <asp:TextBox ID="txtothers" runat="server" TextMode="MultiLine"  CssClass="form-control"
                    BorderColor="#000066" BorderWidth="0px"></asp:TextBox>
            </td>
        </tr>
        <tr>
        
        <td style="border-style: none; border-width: thin; width: 406px;">
         <asp:Label ID="Label12" runat="server" Text="Bill copy" Font-Names="Calibri"></asp:Label>
        </td>
        <td style="width: 200px" align="left">
            <asp:Label ID="Label13" runat="server" Text="No of Bills"></asp:Label>
&nbsp;&nbsp;
            <asp:TextBox ID="txt_nobills" runat="server"   CssClass="form-control"
                 Width="84px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Btn_add" runat="server" onclick="Btn_add_Click" Text="Add" class="btn btn-success"/>
            <br />
        <asp:FileUpload ID="Fileupload1" runat="server"  class="multi" 
                CssClass="form-control" BorderWidth="0px" />
            <br />
                <asp:FileUpload ID="Fileupload2" runat="server"  CssClass="form-control"
                BorderWidth="0px" />
            <br />
            <asp:FileUpload ID="Fileupload3" runat="server"  CssClass="form-control"
                BorderWidth="0px" />
            <br />
            <asp:FileUpload ID="Fileupload4" runat="server"  CssClass="form-control" 
                BorderWidth="0px" />
            <br />
            <asp:FileUpload ID="Fileupload5" runat="server"  CssClass="form-control"
                BorderWidth="0px" />
        </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btn_Submit" runat="server" Text="Submit" onclick="btn_Submit_Click" class="btn btn-success"/>
                <%--<asp:ImageButton ID="btn_Submit" runat="server" ImageUrl="~/Images/Submit.png" 
                    onclick="btn_Submit_Click" />--%>
                <asp:Button ID="btn_Clear" runat="server" Text="Clear" onclick="btn_Clear_Click" class="btn btn-success"/>
               <%-- <asp:ImageButton ID="btn_Clear" runat="server" ImageUrl="~/Images/Clear.png" 
                    onclick="btn_Clear_Click" />--%>
            </td>
        </tr>
        

            <tr>
            <td>
                    &nbsp;</td>
            </tr>
            
            
        </table>
                    &nbsp;
                    <asp:Image ID="Image1" runat="server" Height="450px" 
        Width="450px" 
        ImageUrl='<%# "~/Handler2.ashx?ImID=" + Eval("ID")%>' />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Image 
        ID="Image2" runat="server" Height="450px" 
        Width="450px" 
        ImageUrl='<%# "~/Handler2.ashx?ImID=" + Eval("ID")%>' />
    &nbsp;&nbsp;&nbsp;&nbsp;
    <br />
    <br />
    <br /> 
    
   

<HeaderStyle BackColor="#7779AF" ForeColor="White"></HeaderStyle>
                    <br />
                    <asp:ListView ID="lv_hr" runat="server" 
        onselectedindexchanged="lv_hr_SelectedIndexChanged" 
        onitemediting="lv_hr_ItemEditing" onitemcanceling="lv_hr_ItemCanceling" 
        DataKeyNames="Id" onitemcommand="lv_hr_ItemCommand" onitemdatabound="lv_hr_ItemDataBound" >
       
                    <LayoutTemplate>
                    
                        <table   border="1" align="center">
                            <tr >
                            <td style="font-family: Calibri;   font-weight: bold;">ID</td>
                                <td style="font-family: Calibri;   font-weight: bold;">Emp.ID</td>
                                <td style="font-family: Calibri;   font-weight: bold;">Emp.Name</td>
                                <td style="font-family: Calibri;   font-weight: bold;">From</td>
                                <td style="font-family: Calibri;   font-weight: bold;">To</td>            
                                <td style="font-family: Calibri;   font-weight: bold;">Total Days</td>
                                <td style="font-family: Calibri;   font-weight: bold;">Travel Mode</td>
                                <td style="font-family: Calibri;   font-weight: bold;">Destination</td> 
                                <td style="font-family: Calibri;   font-weight: bold;">Total Expenses</td>
                                <td style="font-family: Calibri;   font-weight: bold;">Purpose</td>                                
                                <td style="font-family: Calibri;  font-weight: bold;">Status</td>
                                <td style="font-family: Calibri;   font-weight: bold;">Approve</td>                            
                                <td style="font-family: Calibri;   font-weight: bold;">Reject</td>
                                <td style="font-family: Calibri;   font-weight: bold;">View Bill</td>
                            </tr>
                            <tr id="itemplaceholder" runat="server"></tr>                     
                        </table>           
                    </LayoutTemplate>
                    
                    <ItemTemplate>
                        <tr id="temp_id" runat="server">
                            <%--<asp:Label ID="lblid" runat="server" Text='<%#Eval("id") %>' Visible="false"></asp:Label>--%>
                            <td style="font-family: Calibri"><asp:Label ID="lblBillId" runat="server" Text='<%#Eval("ID") %>'></asp:Label></td>
                            <td style="font-family: Calibri"><asp:Label ID="lblempid" runat="server" Text='<%#Eval("pn_employeeid") %>'></asp:Label></td>
                            <td style="font-family: Calibri"><asp:Label ID="Label7" runat="server" Text='<%#Eval("pn_employeename") %>'></asp:Label></td>
                            <td style="font-family: Calibri"><asp:Label ID="Label8" runat="server" Text='<%#Eval("from_date","{0:dd/MM/yyyy}") %>'></asp:Label></td>
                            <td style="font-family: Calibri"><asp:Label ID="Label9" runat="server" Text='<%#Eval("to_date","{0:dd/MM/yyyy}") %>'></asp:Label></td>
                            <td style="font-family: Calibri"><asp:Label ID="lbldays" runat="server" Text='<%#Eval("total_days") %>'></asp:Label></td>
                            <td style="font-family: Calibri"><asp:Label ID="lblmode" runat="server" Text='<%#Eval("mode") %>'></asp:Label></td>
                            <td style="font-family: Calibri"><asp:Label ID="lbldest" runat="server" Text='<%#Eval("destination") %>'></asp:Label></td>
                            <td style="font-family: Calibri"><asp:Label ID="lblexp" runat="server" Text='<%#Eval("expense") %>'></asp:Label></td>
                            <td style="font-family: Calibri"><asp:Label ID="lblpurpose" runat="server" Text='<%#Eval("purpose") %>'></asp:Label></td>                           
                            <td style="font-family: Calibri"><asp:Label ID="lblresult" runat="server" Text='<%#Eval("Status") %>'></asp:Label></td>                       
                            <td style="font-family: Calibri"><asp:LinkButton ID="lnk_approve" runat="server" Text="Approve" CommandName="Edit"></asp:LinkButton></td>
                            <td style="font-family: Calibri"><asp:LinkButton ID="LinkButton1" runat="server" Text="Reject" CommandName="Reject"></asp:LinkButton></td>                        
                            <td style="font-family: Calibri"><asp:LinkButton ID="lnk_view" runat="server" Text="View Bill" CommandName="View" CommandArgument= "<%# Container.DataItemIndex %>"></asp:LinkButton></td>                     
                        </tr>   
                                    
                    </ItemTemplate>        
                    <EmptyItemTemplate>
                        <asp:Label ID="lblempty" runat="server" Text="Updated Successfully" ForeColor="Red"></asp:Label> 
                        <%--<asp:LinkButton ID="lnk_bk" runat="server" Text="Back" PostBackUrl="~/Hrms_Additional/Reimbursement.aspx"></asp:LinkButton> --%>
                        <a href ="Reimbursement.aspx">Back</a>
                        
                    </EmptyItemTemplate>
                    <EmptyDataTemplate>
                        <asp:Label ID="lblempty1" runat="server" Text="Updated Successfully" ForeColor="Red"></asp:Label>
                        <a href ="Reimbursement.aspx">Back</a> 
                       <%-- <asp:LinkButton ID="lnk_bk" runat="server" Text="Back" PostBackUrl="~/Hrms_Additional/Reimbursement.aspx"></asp:LinkButton>--%>
                    </EmptyDataTemplate>
                    
                </asp:ListView>
                
                 <div>
                     <br />
</div>

</asp:Content>
