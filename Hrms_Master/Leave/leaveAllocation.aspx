<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="leaveAllocation.aspx.cs" Inherits="Hrms_Master_Default" Title="Welcome to HRMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript" src="../../Scripts/Datavalid.js"></script>
   <link href="../../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
   
    function show_message()
    {
        alert("Leave Name Already Exist");
    }
         
    function show_Error()
    {
        alert("No Employees Available");
    }
  
    function fnSave()
    {   
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$DepartmentName.value == "")
        {
            alert("Enter Leave Name");
            aspnetForm.ctl00$ContentPlaceHolder1$DepartmentName.focus();
            return false;
        }                        
        else
        { 
              return true;  
        }
    }
    function txtcount_onclick() {

    }

    </script>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="row">
                <div class="col-lg-12">
                    <h2 class="page-header">Leave Allocation</h2>
                </div>
                <!-- /.col-lg-12 -->
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

            <div class="panel panel-default">
                        <div class="panel-heading">
                            Leave
                            <div class="pull-right">
                            <asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged"
                                CssClass="InputDefaultStyle" Height="22px">
                            </asp:DropDownList>
                            </div>
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                         

                            <div align="center" style="width: 80%">
                
                               <table class="table table-striped table-bordered table-hover">
                                    <tbody>
                                        <tr>
                                            <td style="width: 25%">
                                                Allocate Leave by</td>
                                            <td style="width: 50%">
                                                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                                                    onselectedindexchanged="DropDownList1_SelectedIndexChanged" CssClass="form-control">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>All</asp:ListItem>
                                                                   <%--               <asp:ListItem>Department</asp:ListItem>
                                  <asp:ListItem>Division</asp:ListItem>
                                <asp:ListItem>Category</asp:ListItem>--%>
                                                    <asp:ListItem>Individuals</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%">
                                                Department</td>
                                            <td style="width: 50%">
                                                <span style="color: #800000">
                                                <asp:DropDownList ID="ddl_department" runat="server" AutoPostBack="True" 
                                                    class="form-control" 
                                                    onselectedindexchanged="ddl_department_SelectedIndexChanged" Width="100%">
                                                </asp:DropDownList>
                                                </span>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_allo_type" runat="server" Font-Size="Medium"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddl_Employee" runat="server" AutoPostBack="True" 
                                                    CssClass="form-control" OnSelectedIndexChanged="ddl_Employee_SelectedIndexChanged" >
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:GridView ID="grid_Leave" runat="server" AutoGenerateColumns="False" 
                                                    CssClass="table table-hover table-striped" DataKeyNames="leaveID" 
                                                    OnRowEditing="Edit" OnRowUpdating="Update" Width="100%" GridLines="None">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                    <colgroup>
                                                                        <col></col>
                                                                    </colgroup>
                                                                    <thead>
                                                                        <tr>
                                                                            <th align="left" style="width: 80%;">
                                                                                Leave Name</th>
                                                                        </tr>
                                                                    </thead>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtcode" runat="server" Enabled="false" 
                                                                    Text='<%#Eval("leaveName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                    <colgroup>
                                                                        <col></col>
                                                                    </colgroup>
                                                                    <thead>
                                                                        <tr>
                                                                            <th align="left" style="width: 80%;">
                                                                                Actual Days</th>
                                                                        </tr>
                                                                    </thead>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="txttotal" runat="server" Enabled="false" 
                                                                    Text='<%#Eval("Count")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                    <colgroup>
                                                                        <col></col>
                                                                    </colgroup>
                                                                    <thead>
                                                                        <tr>
                                                                            <th align="left" style="width: 80%;">
                                                                                Allowed Days</th>
                                                                        </tr>
                                                                    </thead>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <input type="text" class=" form-control" runat="server" id="txtcount" onkeydown="AllowOnlyNumeric1(event);" maxlength="5" onclick="return txtcount_onclick()" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" >
                                                <asp:Button ID="btn_reset" runat="server" class="btn btn-warning" Text="Reset" 
                                                    onclick="btn_reset_Click" />
                                               
                                                <asp:Button ID="btn_save" runat="server" class="btn btn-success" Text="Save" 
                                                    onclick="btn_save_Click" />
                                                &nbsp;<%--<asp:Button ID="btn_modify" runat="server" class="btn btn-success" 
                                                    Text="Modify" onclick="btn_modify_Click" />--%></td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="3">
                                                &nbsp;</td>
                                        </tr>
                                    </tbody>
                                </table> 
                
                            </div>
                            
                        </div>
                        
                    </div>
                    </ContentTemplate>
                                </asp:UpdatePanel>
    </asp:Content>
