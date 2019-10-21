<%@ Page MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/HRMS.master" 
    AutoEventWireup="true" CodeFile="Annual_increment.aspx.cs" Inherits="Hrms_Additional_Default"
    Title="ePay-HRMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <script language="javascript" type="text/javascript">   
       function fn_chkall(chkid,chklistid)
       { 
       
        var chkBoxList = document.getElementById(chklistid);
        var chkBoxCount = chkBoxList.getElementsByTagName("input");

       if(document.getElementById(chkid).checked==true)
       {
            for(var i=0;i<chkBoxCount.length;i++)
            {
                chkBoxCount[i].checked = true;
            }
       }
       else
       {
            for(var i=0;i<chkBoxCount.length;i++)
            {
                chkBoxCount[i].checked = false;
            }
       }
    }
    
    </script>
    
    <style type="text/css">
        .move
        {
            position: absolute;
            top: 325px;
            width: 150px;
            height: 55px;
            padding-left: 15px;
            left: 750px;
            display: inline;
        }
        .style3
        {
            color: #FF0000;
        }
    
        .CalendarStyle   
        {  
            background-color:#E9967A;                      
        }  
        .ajax__calendar_body  
        {  
        	border:solid 1px #D2691E;
        }  
        .ajax__calendar_header  
        {  
        	background-color:Transparent;  
        }                   
        .ajax__calendar_footer  
        {  
            background-color:Transparent;  
        }              
        .style5
        {
            height: 28px;
        }
        .style6
        {
            width: 123px;
            height: 28px;
        }
        </style> 
    
    <table width="100%">
        <tr class="border">
            <td style="" class="border">
                <span class="Title" 
                    style="font-family: calibri; font-size: medium; font-weight: bold;">&nbsp;&nbsp;&nbsp;<h3 class="page-header">Annual Increment</h3></span>
            </td>

        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblerror" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
                                    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                                    </asp:ToolkitScriptManager>

    <asp:LinkButton ID="cmd_chk_allotmnet" runat="server" 
        Text="Check Allotments" OnClick="cmd_chk_allotmnet_Click" />
        <asp:CheckBox ID="chk_all" runat="server" Text="Select all Employee" onclick="javascript: fn_chkall(this.id,'ctl00_ContentPlaceHolder1_chk_emp')" 
        Checked="false" Font-Names="Calibri" Visible="False" />
    <br />
    <table class="table table-striped table-bordered table-hover">
        <tr runat="server">
            <td>
                <asp:Label ID="lblyear" runat="server" Text="Select Calendar Year"></asp:Label>
            </td>
            <td>
                            
                <asp:DropDownList ID="ddl_cyear" runat="server" AutoPostBack="True" CssClass="form-control"  onselectedindexchanged="ddl_cyear_SelectedIndexChanged">
                    <asp:ListItem>Select</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>

        <tr id="tr_dept" runat="server">
            <td>
                <asp:Label ID="lbldept" runat="server" Text="Select Department"></asp:Label>
           </td>
            <td>
                <asp:DropDownList ID="ddl_dept" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_dept_SelectedIndexChanged"
                   CssClass="form-control">
                    <asp:ListItem>Select</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>

        <tr id="tr_alloted_amt" runat="server" visible="false">
            <td>
                <asp:Label ID="lbl_alloted_amt" runat="server" Text="Allowed Increment Amount"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_alloted_amt" runat="server" Text="" CssClass="form-control"></asp:TextBox>
            </td>
        </tr>
        <tr id="tr_alloted_per" runat="server" visible="false">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Allowed Increment Percentage"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_alloted_per" runat="server" Text="" CssClass="form-control"></asp:TextBox>
            </td>
        </tr>
        <tr id="tr_ddl_emp" runat="server" >
            <td class="style5">
                <asp:Label ID="lblemp" runat="server" Text="Select Employee"></asp:Label>
            </td>
            <td>
                <asp:CheckBoxList ID="chk_emp" runat="server" onselectedindexchanged="chk_emp_SelectedIndexChanged" CssClass="form-control" BackColor="White">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr id="tr_mode" runat="server">
            <td>
                <asp:Label ID="lbl_mode" runat="server" Text="Increment Mode"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddl_mode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_dept_SelectedIndexChanged" CssClass="form-control">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>Amount</asp:ListItem>
                    <asp:ListItem>Percentage on basic</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr id="tr_value" runat="server">
            <td>
                <asp:Label ID="lbl_value" runat="server" Text="Increment Value"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_value" runat="server" CssClass="form-control"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbldate" runat="server" Text="Effective Date"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                
            </td>
            <td>
                <asp:Button ID="cmd_allot" runat="server" Text="Make Allotment" OnClick="cmd_allot_Click" class="btn btn-success" />
                <asp:Button ID="cmd_de_allot" runat="server" Text="Delete All Allotment" OnClick="cmd_de_allot_Click" class="btn btn-warning"/>
            </td>
        </tr>
    </table>
    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdate"
        Animated="true" Format="dd/MM/yyyy">
    </asp:CalendarExtender>

    <asp:ListView ID="lv_inc_details" runat="server" onitemdeleting="lv_inc_details_ItemDeleting" onitemediting="lv_inc_details_ItemEditing"  >     
        <LayoutTemplate >
            <table class="table table-striped table-bordered table-hover">
                <tr>
                    <td>Employee Name</td>
                    <td>Employee ID</td>
                    <td>Basic </td>
                    <td>Increment Amount</td>
                    <td>Revised Basic</td>
                    <td>Effective Date</td>
                     <tdEdit</td>
                     <td>Delete</td>
                </tr>
                <tr id="itemplaceholder" runat="server"></tr>
            
            </table>
            
        </LayoutTemplate>
    
        <ItemTemplate>
        <tr>
            <td><asp:Label ID="lblname" runat="server" Text='<%#Eval("empname") %>'></asp:Label></td>
            <td><asp:Label ID="lblid" runat="server" Text='<%#Eval("empid") %>'></asp:Label></td>
            <td><asp:Label ID="lblbp" runat="server" Text = '<%#Eval("basic_salary","{0:0.00}") %>'></asp:Label></td>
            <td><asp:Label ID="lblincamt" runat="server" Text='<%#Eval("allotment_amt","{0:0.00}") %>'></asp:Label></td>
            <td><asp:Label ID="lblsalary" runat="server" Text='<%#Eval("Revised_Basic","{0:0.00}") %>'></asp:Label></td>
            <td><asp:Label ID="lblyear" runat="server" Text='<%#Eval("date","{0:dd/MM/yyyy}") %>'></asp:Label></td>
            <td><asp:LinkButton runat="server" Text="Edit" ID="LinkButton1" CommandName="Edit"></asp:LinkButton></td>
            <td><asp:LinkButton runat="server" Text="Delete" ID="lnk_del" CommandName="Delete"></asp:LinkButton></td>
        </tr>        
        </ItemTemplate>
        <EditItemTemplate>
        <td><asp:Label ID="lblname1" runat="server" Text='<%#Bind("empname") %>'></asp:Label></td>
            <td><asp:Label ID="lblid1" runat="server" Text='<%#Bind("empid") %>'></asp:Label></td>
            <td><asp:TextBox ID="lblbp1" runat="server" Text = '<%#Bind("basic_salary","{0:0.00}") %>'></asp:TextBox></td>
            <td><asp:TextBox ID="lblincamt1" runat="server" Text='<%#Bind("allotment_amt","{0:0.00}") %>'></asp:TextBox></td>
            <td><asp:TextBox ID="lblsalary1" runat="server" Text='<%#Bind("Revised_Basic","{0:0.00}") %>'></asp:TextBox></td>
            <td><asp:TextBox ID="lblyear1" runat="server" Text='<%#Bind("date","{0:dd/MM/yyyy}") %>'></asp:TextBox></td>
            <td><asp:LinkButton runat="server" Text="Update" ID="Lnk_Update" CommandName="Update"></asp:LinkButton></td>
            <td><asp:LinkButton runat="server" Text="Cancel" ID="Lnk_Cancel" CommandName="Cancel"></asp:LinkButton></td>
        </EditItemTemplate>    
    
    </asp:ListView>
</asp:Content>
