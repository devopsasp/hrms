<%--<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="Approval.aspx.cs" Inherits="Hrms_Master_Leave_Add_approval" %>--%>
<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="Approval.aspx.cs" Inherits="Hrms_Company_Default" Title="Welcome to HRMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <link href="../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../../Scripts/Datavalid.js"></script>
    <script language=javascript type="text/javascript">

        function show_message() {
            alert("Department Name Already Exist");
        }

        function show_message1(msg) {
            alert(msg);
        }

    //function fnSave()
    //{   
    //    if(document.aspnetForm.ctl00$ContentPlaceHolder1$DepartmentName.value == "")
    //    {
    //        alert("Enter Department Name");
    //        aspnetForm.ctl00$ContentPlaceHolder1$DepartmentName.focus();
    //        return false;
    //    }                        
    //    else
    //    { 
    //          return true;  
    //    }
    //}    
    </script>
<div><h2 class="page-header">Leave Approval</h2></div>
<div><h3><asp:Label ID="lbl_Error" runat="server" CssClass="Error" ForeColor="Red" Font-Bold="True" Width="35%"></asp:Label></h3>
        <div style="float:right;"> 
        <%--<asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged"
         CssClass="form-control">
          </asp:DropDownList>--%>
        </div>
</div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
      <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
         <ProgressTemplate>
              <div class="divWaiting">              
                 <asp:Image ID="imgWait" runat="server" ImageAlign="Middle" ImageUrl="~/Images/loading2.gif" Height="100px" Width="100px" />
                                <%--<img src="../loading.gif" alt="Loading" style="position:relative;" />--%>
                    </div>
          </ProgressTemplate>
       </asp:UpdateProgress>
   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>
      <%--   <asp:GridView ID="Grid_Details"  runat="server" AllowSorting="True"  CssClass="table table-striped table-bordered table-hover"
            AutoGenerateColumns="False" ShowFooter="True">
            
            <Columns>
            

                 <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Emp.Code">
                
                    <ItemTemplate>
                        <asp:Label ID="Labtask3" runat="server" Text='<%# Eval("Emp_Code") %>'></asp:Label>
                    </ItemTemplate>
                    
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Emp.Name">
                    <ItemTemplate>
                        <asp:Label ID="Label14" runat="server" Text='<%# Eval("Emp_Name") %>'></asp:Label>
                    </ItemTemplate>
                    
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Leave Code">
                    <ItemTemplate>
                        <asp:Label ID="Label15" runat="server" Text='<%# Eval("pn_leavecode") %>'></asp:Label>
                    </ItemTemplate>  
                    
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Leave Date">
                    <ItemTemplate>
                        <asp:Label ID="lbl_frm0" runat="server" Text='<%# Eval("from_date","{0:dd/MM/yyyy}") %>'></asp:Label>
                    </ItemTemplate>

                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="To Date">
                    <ItemTemplate>
                        <asp:Label ID="Label16" runat="server" 
                            Text='<%# Eval("to_date","{0:dd/MM/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
        
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Submitted on">
                    <ItemTemplate>
                        <asp:Label ID="Labelsubm0" runat="server"  
                            Text='<%# Eval("submitted_date","{0:dd/MM/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
        
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Reason">
                    <ItemTemplate>
                        <asp:Label ID="Label18" runat="server" Text='<%# Eval("Reason") %>'></asp:Label>
                    </ItemTemplate>    
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Approve">
                    
                    <ItemTemplate>
                        <asp:Label ID="Label19" runat="server" Text='<%# Eval("Approve") %>'></asp:Label>
                    </ItemTemplate>
                     <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList2" runat="server" Text='<%# Bind("Approve") %>' CssClass="form-control">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>Pending</asp:ListItem>
                            <asp:ListItem>Yes</asp:ListItem>
                            <asp:ListItem>No</asp:ListItem>
                            <asp:ListItem>Hold</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                

            </Columns>

        </asp:GridView>--%>
         <div>
         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  onrowcommand="GridView1_RowCommand" CellPadding="4" 
  CssClass="table table-striped table-bordered table-hover"
            onrowdeleting="GridView1_RowDeleting"  
            onrowdatabound="GridView1_RowDataBound" onrowediting="GridView1_RowEditing" 
            onrowupdating="GridView1_RowUpdating" 
            onrowcancelingedit="GridView1_RowCancelingEdit" Width="736px" Height="57px">
             <Columns>
                 <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="S.no">
                    <ItemTemplate>
                        <asp:Label ID="Labtask1" runat="server" Text='<%# Eval("Sno") %>' ></asp:Label>
                    </ItemTemplate>
                     <EditItemTemplate>
                    <asp:Label ID="Labelsubedit1" runat="server" Text='<%# Bind("Sno") %>' ></asp:Label>
                    </EditItemTemplate>
                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>

                 <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Emp.Code">
                
                    <ItemTemplate>
                        <asp:Label ID="Labtask" runat="server" Text='<%# Eval("EmpCode") %>' ></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:Label ID="Labelsubedit" runat="server" Text='<%# Bind("EmpCode") %>' ></asp:Label>
                    </EditItemTemplate>

                   <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                  
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Emp.Name">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("Emp_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:Label ID="Labelassi" runat="server" Text='<%# Bind("Emp_Name") %>' ></asp:Label>
                    </EditItemTemplate>

                  <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                 
                  
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100"  HeaderText="Code">
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# Eval("pn_Leavename") %>'></asp:Label>
                    </ItemTemplate>  
                    <EditItemTemplate>
                    <asp:Label ID="Labeledit" runat="server" Text='<%# Bind("pn_Leavename") %>'></asp:Label>
                    </EditItemTemplate> 
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                 <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Emp.Name">
                    <ItemTemplate>
                        <asp:Label ID="Label8" runat="server" Text='<%# Eval("pn_leavecode") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("pn_leavecode") %>' ></asp:Label>
                    </EditItemTemplate>
                  <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                 <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Emp.Name">
                    <ItemTemplate>
                        <asp:Label ID="Label9" runat="server" Text='<%# Eval("From_status") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("From_status") %>' ></asp:Label>
                    </EditItemTemplate>
                  <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>

                 <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Emp.Name">
                    <ItemTemplate>
                        <asp:Label ID="Label11" runat="server" Text='<%# Eval("To_Status") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("To_Status") %>' ></asp:Label>
                    </EditItemTemplate>
                  <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                 <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Leave Date">
                    <ItemTemplate>
                        <asp:Label ID="label13" runat="server" Text='<%# Eval("dayss") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:Label ID="Label13" runat="server" Text='<%# Bind("dayss") %>'></asp:Label>
                    </EditItemTemplate>
        
                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                  <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Leave Date">
                    <ItemTemplate>
                        <asp:Label ID="lbl_frm" runat="server" Text='<%# Eval("from_date","{0:dd/MM/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:Label ID="Labelleavedate" runat="server" Text='<%# Bind("from_date","{0:dd/MM/yyyy}") %>'></asp:Label>
                    </EditItemTemplate>
        
                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="To Date">
                    <ItemTemplate>
                        <asp:Label ID="Label10" runat="server" Text='<%# Eval("to_date","{0:dd/MM/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:Label ID="Labeltodate" runat="server" Text='<%# Bind("to_date","{0:dd/MM/yyyy}") %>'></asp:Label>
                    </EditItemTemplate>
        
                   <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Submitted on">
                    <ItemTemplate>
                        <asp:Label ID="Labelsubm" runat="server"  Width="75px"
                            Text='<%# Eval("submitted_date","{0:dd/MM/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:Label ID="Labelsubm" runat="server" Text='<%# Bind("submitted_date","{0:dd-MM-yyyy}") %>'></asp:Label>
                    </EditItemTemplate>
        
                  <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <%-- <asp:TemplateField ItemStyle-HorizontalAlign="Center"  HeaderText="Priority">
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("priority") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="lbl_prioredit" runat="server" Text='<%# Bind("priority") %>'></asp:Label>
                    
                    </EditItemTemplate>

                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Reason">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("Reason") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:Label ID="Labelreason" runat="server" Text='<%# Bind("Reason") %>'></asp:Label>
                    </EditItemTemplate>
         

                   <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>--%>
                 <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Approve">
                    <ItemTemplate>
                        <asp:Label ID="Label12" runat="server" Text='<%# Eval("Approve") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem>Pending</asp:ListItem>
                            <asp:ListItem>Aprroval</asp:ListItem>
                            <asp:ListItem>Regret</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    </asp:TemplateField>
                <asp:TemplateField>
                                    <ItemTemplate>
                                      <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit"  ImageUrl="~/Images/edit_icon.png" />
                                        <asp:ImageButton ID="btnDelete" ImageUrl="~/Images/delete_icon.jpg" runat="server" OnClientClick="return confirm('Are you sure you want to delete this record?');"  CommandName="Delete" />
                                    </ItemTemplate>
                                      <EditItemTemplate>
                                                <asp:LinkButton ID="btnUpdate" runat="server" CommandName="Update" CssClass="btn btn-xs btn-success "><i class="glyphicon glyphicon-saved"></i> Update</asp:LinkButton>
                                                <%--<asp:ImageButton ID="btnUpdate" ImageUrl="~/Images/save_icon.jpg"  runat="server" CommandName="Update" AccessKey />--%>
                                            </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnk_Add" CommandName="add" runat="server" CssClass="btn btn-sm btn-success"><span class="glyphicon glyphicon-floppy-saved"></span> Save</asp:LinkButton>
                                    </FooterTemplate>
                                </asp:TemplateField>
                 <%--<asp:BoundField DataField="Emp_code" HeaderText="Emp_code" SortExpression="Emp_code" />
                 <asp:BoundField DataField="Emp_name" HeaderText="Emp_name" SortExpression="Emp_name" />
                 <asp:BoundField DataField="pn_LeaveID" HeaderText="pn_LeaveID" SortExpression="pn_LeaveID" />
                 <asp:BoundField DataField="pn_Leavename" HeaderText="pn_Leavename" SortExpression="pn_Leavename" />
                 <asp:BoundField DataField="pn_leavecode" HeaderText="pn_leavecode" SortExpression="pn_leavecode" />
                 <asp:BoundField DataField="from_date" HeaderText="from_date" SortExpression="from_date" />
                 <asp:BoundField DataField="to_date" HeaderText="to_0date" SortExpression="to_date" />
                 <asp:BoundField DataField="reason" HeaderText="reason" SortExpression="reason" />
                 <asp:BoundField DataField="approve" HeaderText="approve" SortExpression="approve" />--%>
             </Columns>
         </asp:GridView>


</div>

         <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Hesperus_HrmsConnectionString %>" SelectCommand="SELECT [Emp_code], [Emp_name], [pn_LeaveID], [pn_Leavename], [pn_leavecode], [from_date], [to_date], [reason], [approve] FROM [leave_apply] WHERE ([flag] IS NULL)"></asp:SqlDataSource>
        </ContentTemplate>
          </asp:UpdatePanel>
    
</asp:Content>