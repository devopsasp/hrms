<%@ Page Language="C#" MasterPageFile="~/HRMS.master"
    AutoEventWireup="true" CodeFile="App_Increment_New.aspx.cs" Inherits="Hrms_Master_Default"
    Title="Welcome to HRMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="../../Scripts/Datavalid.js"></script>

    <script language="javascript" type="text/javascript">
    
    function show_message()
    {
        alert("Appraisal Name Already Exist");
    }
    function show_Error1()
    {
        alert("Enter FromPoint Value");
    }
     function show_Error2()
    {
        alert("Enter ToPoint Value");
    }  
   
   function show_Error3()
    {
        alert("Enter Increment Point");
    }
    
    function fnSave()
    {   
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$txtFromPoint.value == "")
            {
                alert("Enter FromPoint Value");            
                return false;
            }                        
        else
            { 
                if(document.aspnetForm.ctl00$ContentPlaceHolder1$txtToPoint.value == "")
                    {
                        alert("Enter ToPoint Value");            
                        return false;
                    }   
                else
                    {
//                        if(document.aspnetForm.ctl00$ContentPlaceHolder1$txtPointsAmount.value == "")
//                            {
//                                alert("Enter Points Amount");            
//                                return false;
//                            }   
//                        else
//                            {
//                                return true;  
//                            }
                    }
        }
    }
    function txtToPoint_onclick() {

    }

    </script>


    <div ><h3 class="page-header">Appraisal Increment</h3></div>
                     <div> 
                            <asp:SqlDataSource ID="SqlDataSource_grade" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:connectionstring %>" 
                                
                                SelectCommand="SELECT [v_GradeName] FROM [paym_Grade] WHERE ([BranchID] = @BranchID)">
                                <SelectParameters>
                                    <asp:SessionParameter Name="BranchID" SessionField="Login_temp_BranchID" 
                                        Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSourcedept" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:connectionstring %>" 
                                SelectCommand="SELECT [v_DepartmentName] FROM [paym_Department] WHERE (([pn_CompanyID] = @pn_CompanyID) AND ([pn_BranchID] = @pn_BranchID))">
                                <SelectParameters>
                                    <asp:SessionParameter Name="pn_CompanyID" SessionField="Login_temp_companyID" 
                                        Type="Int32" />
                                    <asp:SessionParameter Name="pn_BranchID" SessionField="Login_temp_BranchID" 
                                        Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </div>
                                    <div style="width: 80%">
                                    <table cellpadding="1%" cellspacing="1%" width="100%" class="table table-striped table-bordered table-hover">
                        
                        <tr>
                            <td>
                                Value Points From</td>
                            <td>
                            <input class="form-control" runat="server" id="txtFromPoint"  onkeydown="AllowOnlyNumeric1(event);" 
                               maxlength="4" /></td>
                            <td> Value Points To</td>
                            <td>
                            <input class="form-control" runat="server" id="txtToPoint" 
                                onkeydown="AllowOnlyNumeric1(event);" maxlength="4" onclick="return txtToPoint_onclick()" /></td>
                        </tr>
                        <tr>
                            <td >
                                Department</td>
                            <td >
                           
                            <asp:DropDownList ID="ddl_dept" runat="server" CssClass="form-control"
                                DataSourceID="SqlDataSourcedept" DataTextField="v_DepartmentName" 
                                DataValueField="v_DepartmentName" AppendDataBoundItems="true" >
                                <asp:ListItem Text="Select Department"></asp:ListItem>
                            </asp:DropDownList>

                            </td>
                            <td >
                                Grade</td>
                            <td >
                            
                            <asp:DropDownList ID="ddl_grade" runat="server" CssClass="form-control"
                                DataSourceID="SqlDataSource_grade" DataTextField="v_GradeName" 
                                DataValueField="v_GradeName" AppendDataBoundItems="true">
                                 <asp:ListItem Text="Select"></asp:ListItem>
                            </asp:DropDownList>
                           
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Formula Code</td>
                            <td>
                            <input id="txt_formulaName" class="form-control" runat="server"  maxlength="4" /></td>
                            <td >
                                Increment by</td>
                            <td >
                            <asp:DropDownList ID="ddl_Inctype" runat="server" CssClass="form-control"
                                onselectedindexchanged="ddl_Inctype_SelectedIndexChanged" AutoPostBack="True" >
                                 <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="Amount">Amount</asp:ListItem>
                                <asp:ListItem Value="Percentage">Percentage</asp:ListItem>
                                <asp:ListItem Value="Whichever is higher">Whichever is higher</asp:ListItem>
                                <asp:ListItem Value="Whichever is lower">Whichever is lower</asp:ListItem>
                                <asp:ListItem Value="Average">Average</asp:ListItem>
                            </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                <span>
                                <asp:Label ID="lbl_increment" runat="server" EnableTheming="True" Text="Amount"></asp:Label>
                                </span></td>
                            <td >
                            <input class="form-control" runat="server" id="txtPointsAmount" 
                                onkeydown="AllowOnlyNumeric1(event);" maxlength="10" /></td>
                            <td >
                                <span>
                                <asp:Label ID="lbl_increment1" runat="server" Text="Percentage"></asp:Label>
                                </span></td>
                            <td >
                                <input id="txtpointamount1" class="form-control" runat="server" visible="true" maxlength="3" /></td>
                        </tr>
                        <tr>
                            <td >
                                &nbsp;</td>
                            <td >
                                &nbsp;</td>
                            <td >
                                &nbsp;</td>
                            <td >
                            <asp:Button ID="Button1" runat="server" OnClientClick="return fnSave();" Text="Add" OnClick="Button1_Click1" class="btn btn-success" />
                           <%-- <asp:Button ID="btn_formulaedit" runat="server" Text="Edit" onclick="btn_formulaedit_Click" class="btn btn-info"  />
                            <asp:Button ID="btn_cancel" runat="server" onclick="btn_cancel_Click" class="btn btn-danger" Text="Cancel" />--%>
                            </td>
                        </tr>

                     </table>
                     </div>
                     <div>
                     <table style="width: 100%">
                       <tr>
                         <td>
                           
                        
                            <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" onrowdatabound="GridView1_RowDataBound" 
                                onselectedindexchanged="GridView1_SelectedIndexChanged" onrowdeleting="GridView1_RowDeleting" onrowcommand="GridView1_RowCommand" CssClass="table table-hover table-striped" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" >
                                <HeaderStyle HorizontalAlign="Left" />
           
            <Columns>
            
            <asp:TemplateField Visible="false" ItemStyle-HorizontalAlign="Center" HeaderText="">
                
                    <ItemTemplate>
                        <asp:Label ID="lbl_id" Visible="false" runat="server" Text='<%# Eval("pn_IncrementID") %>'></asp:Label>
                    </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
            
            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Department">
                
                    <ItemTemplate>
                        <asp:Label ID="lbl_dept" runat="server" Text='<%# Eval("department") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                       <%-- <asp:Textbox ID="txt_dept" runat="server" Text='<%# Bind("department") %>'></asp:Textbox>--%>
                        <asp:DropDownList ID="ddl_deptEdit" runat="server" CssClass="form-control"
                                DataSourceID="SqlDataSourcedept" DataTextField="v_DepartmentName" 
                                DataValueField="v_DepartmentName" AppendDataBoundItems="true" SelectedValue='<%# Bind("department") %>'  >
                                <asp:ListItem Text="Select Department"></asp:ListItem>
                            </asp:DropDownList>
                    </EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField> 
                
            
                 <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Grade">
                
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Grade") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                       <%-- <asp:Textbox ID="txt_grade" runat="server" Text='<%# Bind("Grade") %>'></asp:Textbox>--%>
                        <asp:DropDownList ID="ddl_gradeEdit" runat="server" CssClass="form-control"
                                DataSourceID="SqlDataSource_grade" DataTextField="v_GradeName" 
                                DataValueField="v_GradeName" AppendDataBoundItems="true" SelectedValue='<%# Bind("Grade") %>'>
                                 <asp:ListItem Text="Select"></asp:ListItem>
                            </asp:DropDownList>
                           
                    </EditItemTemplate>


<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>   
                
                                                         
            
                 <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="From Point">
                
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("start_point") %>'></asp:Label>
                    </ItemTemplate>
                     <EditItemTemplate>
                        <asp:Textbox ID="txt_startpoint" runat="server" Text='<%# Bind("start_point") %>'></asp:Textbox>
                    </EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="To Point">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("last_point") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Textbox ID="txt_lastpoint" runat="server" Text='<%# Bind("last_point") %>'></asp:Textbox>
                    </EditItemTemplate>
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Increment by">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("Increment_Type") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <%--<asp:Textbox ID="txt_Incretype" runat="server" Text='<%# Bind("Increment_Type") %>'></asp:Textbox> --%>
                        <asp:DropDownList ID="ddl_InctypeEdit" runat="server" DataTextField="Increment_Type" DataValueField="Increment_Type" CssClass="form-control" SelectedValue='<%# Bind("Increment_Type") %>'>
                                 <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="Amount">Amount</asp:ListItem>
                                <asp:ListItem Value="Percentage">Percentage</asp:ListItem>
                                <asp:ListItem Value="Whichever is higher">Whichever is higher</asp:ListItem>
                                <asp:ListItem Value="Whichever is lower">Whichever is lower</asp:ListItem>
                                <asp:ListItem Value="Average">Average</asp:ListItem>
                            </asp:DropDownList>
                    </EditItemTemplate>
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Increment">
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("increment") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Textbox ID="txt_increment" runat="server" Text='<%# Bind("increment") %>'></asp:Textbox>
                    </EditItemTemplate>
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Percentage">
                    <ItemTemplate>
                        <asp:Label ID="lblpercentage" runat="server" Text='<%# Eval("Percentage") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Textbox ID="txt_percentage" runat="server" Text='<%# Bind("Percentage") %>'></asp:Textbox>
                    </EditItemTemplate>
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Formula">
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# Eval("formula_name") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Textbox ID="txt_formula" runat="server" Text='<%# Bind("formula_name") %>'></asp:Textbox>
                    </EditItemTemplate>
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <%--<asp:CommandField  ShowEditButton="true" ShowDeleteButton="true" />--%>
                <asp:TemplateField>
                              <Itemtemplate>
                                 <asp:ImageButton ID="btnEdit" ImageUrl="~/Images/edit_icon.png" runat="server" CommandName="Edit" />
                                  <asp:ImageButton ID="btnDelete" ImageUrl="~/Images/delete_icon.jpg" runat="server" CommandName="Delete" />
                              </Itemtemplate>
                              <EditItemTemplate>
                                <asp:ImageButton ID="btnUpdate" ImageUrl="~/Images/save_icon.jpg" runat="server" CommandName="Update" />
                              </EditItemTemplate>
                 </asp:TemplateField>
            </Columns>


        </asp:GridView>
        
                        </td>
                    </tr>
                         </table>
                  </div>
                                         
           





    </asp:Content>
