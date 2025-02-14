<%@ Page Language="C#" MasterPageFile="~/HRMS.master" 
    AutoEventWireup="true" CodeFile="Computation.aspx.cs" Inherits="Hrms_Master_Default"
    Title="Welcome to HRMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/css">
    .scrollingControlContainer
    {
        overflow-x: hidden;
        overflow-y: scroll;
    }
    </script>
    <script type="text/javascript" language="javascript" src="../../Scripts/Datavalid.js"></script>
    <script language="javascript" type="text/javascript">
  
   function show_message()
    {
        alert("Course Name Already Exist");
    }
    
    function show_Error()
    {
        alert("Enter Course Name");
    }
  
    function fnSave()
    {   
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$CourseName.value == "")
        {
            alert("Enter Course Name");
            aspnetForm.ctl00$ContentPlaceHolder1$CourseName.focus();
            return false;
        }                        
        else
        { 
            return true;  
        }
    } 
    function coudnt_del()
    {
        alert("Cannot delete. It is already assigned to someone");
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
             if(txtlen==2)
              {
              document.getElementById(txtid).value=txtvalue+":";
              }
              else
              {
              document.getElementById(txtid).value=txtvalue;
              }               
            } 
        }                                 
     }
     
    </script>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div><h2 class="page-header">Salary Breakups</h2></div>
                <asp:DropDownList ID="ddl_branch" runat="server"  CssClass="form-control"
                    onselectedindexchanged="ddl_branch_SelectedIndexChanged" 
                    AutoPostBack="True">
                </asp:DropDownList>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                            <div class="divWaiting">
                            
                            <asp:Image ID="imgWait" runat="server" ImageAlign="Middle" 
                                    ImageUrl="~/Images/loading2.gif" Height="100px" Width="100px" />
                                <%--<img src="../loading.gif" alt="Loading" style="position:relative;" />--%>
                            </div>
                            </ProgressTemplate>
                            </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <table ID="branch_time" runat="server" style="width: 100%">
        <tr>
            <td align="center">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="left">
               <%--  <asp:Label ID="Label2" runat="server" Text=";Salary Computation by"></asp:Label>--%>
                &nbsp;Salary Computation by
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="RadioButtonList1_SelectedIndexChanged" 
                    RepeatDirection="Horizontal" Width="25%">
                    <asp:ListItem Selected="True" Value="0">Basic Salary</asp:ListItem>
                    <asp:ListItem Value="1">Gross Salary</asp:ListItem>
                </asp:RadioButtonList>
                &nbsp;<br />
                <asp:Button  ID="btn_refresh"  Text="Refresh Employee Allowance" CssClass="btn btn-info"
                                runat="server" onclick="btn_refresh_Click" Visible="False" />
                <br />
                <br />
                </td>
        </tr>
        <tr>
            <td align="center">
                   
                    <%--<asp:Label ID="Label1" runat="server" Text="This page access for Branch Users only" Font-Bold="true" Font-Size="Larger" ></asp:Label>--%>
                <asp:Panel ID="Panel1" runat="server" Height="100%" HorizontalAlign="Center" 
                    Width="100%" Wrap="False">
                    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
                        AutoGenerateColumns="False" 
                        CssClass="table table-striped table-bordered table-hover" 
                        HorizontalAlign="Left" onrowcancelingedit="GridView1_RowCancelingEdit" 
                        onrowcommand="GridView1_RowCommand" 
                        onrowdatabound="GridView1_RowDataBound" ShowFooter="True" 
                        Width="50%" onrowdeleting="GridView1_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Earnings Name" 
                                ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                <asp:Label ID="lbl_id" runat="server" Text='<%# Eval("id") %>' Visible="False"></asp:Label>
                                    <asp:Label ID="lbl_ecode" runat="server" Text='<%# Eval("pn_EarningsCode") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddl_ecode" runat="server" AutoPostBack="True" 
                                        CssClass="form-control">
                                    </asp:DropDownList>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="% of CTC"  
                    HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_value" runat="server" 
                                        Text='<%# Eval("value") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                <table>
                                <tr><td>
                                    <asp:TextBox ID="txt_value" runat="server" CssClass="form-control" 
                                        MaxLength="2" Width="80%" onkeydown="AllowOnlyNumeric1(event);"></asp:TextBox>
                            </td><td align="right">
                            <asp:LinkButton ID="Button1" CommandName="add" runat="server" 
                             onclick="Button1_Click" CssClass="btn btn-success" ><i class="glyphicon glyphicon-plus"></i></asp:LinkButton>
                             </td>
                             </tr>
                             </table>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/delete_icon.jpg" 
                                ItemStyle-Width="25px" ShowDeleteButton="True" />
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="lblempty" runat="server" Text="No Records"></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
        </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Content>







