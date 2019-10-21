<%@ Page Title="" MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="StudentMaster.aspx.cs" Inherits="Hrms_Employee_Student_Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>
    <style type="text/css">
    .GVFixedHeader { font-weight:bold; background-color: Green; position:relative; 
                 top:expression(this.parentNode.parentNode.parentNode.scrollTop-1);}
.GVFixedFooter { font-weight:bold; background-color: Green; position:relative;
                 bottom:expression(getScrollBottom(this.parentNode.parentNode.parentNode.parentNode));}
    </style>
    
<script language="javascript" type="text/javascript">
    function validate()
    {
        var r=confirm("Are you sure you want to delete this record?");
        if (r==true)
        {
        return true;
        }
        else
        {
        return false;
        }
    }

    </script>
    <script language="javascript" type="text/javascript">
        function getScrollBottom(p_oElem) {
            return p_oElem.scrollHeight - p_oElem.scrollTop - p_oElem.clientHeight;
        }
</script>


            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-12">
                        <h2 class="page-header">Student Details</h2>
                    </div>
                    <!-- /.col-lg-12 -->
                </div>
                <!-- /.row -->
            </div>
            <!-- /.container-fluid -->
            <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
            <div class="panel panel-default">
                        <div class="panel-heading">
                            Academic Setup</div>
                        <!-- /.panel-heading -->
                         <div class="panel-body">   
                             <table cellpadding="0" cellspacing="0" style="width: 100%">
                                 <tr>
                                     <td>
                                         <div style="width: 100%">
                                            <div  class="panel panel-default">
                                                <div class="panel-heading">
                                                    Courses
                                                </div>
                                                <div id="div1" runat="server" style="overflow: auto; height: 250px;">
                                                <div class="panel-body">
                                                <asp:GridView ID="gridCourse" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                                DataKeyNames="CourseID" ShowFooter="True" onrowcommand="gridCourse_RowCommand" 
                                                        OnRowDeleting="DeleteCourse" OnRowEditing="EditCourse" OnRowUpdating="UpdateCourse" 
                                                        Width="100%">
                             <Columns>
                             <asp:TemplateField>
                                <HeaderTemplate>
                                S.No.</HeaderTemplate>
                                <ItemTemplate>
                                <asp:Label ID="lblSRNO" runat="server" 
                                    Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="16px" />
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Course List">
                                    
                                   <ItemTemplate>
                                         <TABLE  cellSpacing="0" cellPadding="0" width="90%" border="0">
                                            
                                            <TBODY>
                                                <TR>
                                                  
                                                    <TD >
                                                    <input runat="server" id="txtCourseName" value='<%#Eval("CourseName")%>' disabled="disabled" 
                                                            class="form-control" maxlength="50" style="width: 100%" />
                                                   
                                                    </td>
                                                   
                                                    <TD align="right">
                                                    <asp:LinkButton id="img_update"  runat="server" class="btn btn-info btn-circle" CommandName="Update"><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                                    <asp:LinkButton id="img_save" runat="server" CommandName="Edit" class="btn btn-success btn-circle" Visible="false" ><i class="glyphicon glyphicon-check"></i></asp:LinkButton>
                                                    &nbsp;&nbsp;<asp:LinkButton id="img_delete" runat="server" CommandName="Delete" OnClientClick="return validate()" class="btn btn-danger btn-circle"><i class="glyphicon glyphicon-remove"></i></asp:LinkButton>
                                                    </TD>
                                                    </tr>
                                            </TBODY>                            
                                        </TABLE> 
                                   </ItemTemplate>
                                   <FooterTemplate>
                                        <TABLE  cellSpacing="0" cellPadding="0" width="100%" border="0">
                                            
                                            <TBODY>
                                                <TR>
                                                    <TD width="65%" >
                                                    <input runat="server" id="txtCourseAdd" 
                                                            class="form-control" maxlength="50" style="width: 100%"/>
                                                    </td>
                                                    <TD align="left">
                                                        &nbsp;&nbsp; &nbsp;&nbsp;
                                                    <asp:LinkButton ID="LinkButton1" CommandName="addCourse" runat="server" CssClass="btn btn-success btn-circle " ><i class="glyphicon glyphicon-check"></i></asp:LinkButton>
                                                    </TD>
                                                    </tr>
                                            </TBODY>                            
                                        </TABLE> 
                                   </FooterTemplate>
                               </asp:TemplateField>
                            </Columns>                         
                        </asp:GridView>
                                                </div>
                                                </div>
                                            </div>
                                        </div>
                                     </td>
                                     <td>
                                        <div style="width: 100%">
                                            <div  class="panel panel-default">
                                                <div class="panel-heading">
                                                    Departments
                                                </div>
                                                <div id="div2" runat="server" style="overflow: auto; height: 250px;">
                                                <div class="panel-body">
                                                <asp:GridView ID="GridDepartment" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                                DataKeyNames="DepartmentID" ShowFooter="True" 
                                                        OnRowDeleting="DeleteDepartment" OnRowEditing="EditDepartment" OnRowUpdating="UpdateDepartment" 
                                                        Width="100%" onrowcommand="GridDepartment_RowCommand">

                                                         <HeaderStyle CssClass="GVFixedHeader" />
                                                        <FooterStyle CssClass="GVFixedFooter" />
                             <Columns>
                             <asp:TemplateField>
                                <HeaderTemplate>
                                S.No.</HeaderTemplate>
                                <ItemTemplate>
                                <asp:Label ID="lblSRNO" runat="server" 
                                    Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="16px" />
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department List">
                                    
                                   <ItemTemplate>
                                         <TABLE  cellSpacing="0" cellPadding="0" width="90%" border="0">
                                            
                                            <TBODY>
                                                <TR>
                                                  
                                                    <TD >
                                                    <input runat="server" id="txtDepartmentName"  value='<%#Eval("DepartmentName")%>' disabled="disabled" 
                                                            class="form-control" maxlength="50" style="width: 100%" />
                                                   
                                                    </td>
                                                   
                                                    <TD align="right">
                                                    <asp:LinkButton id="img_update"  runat="server" class="btn btn-info btn-circle" CommandName="Update"><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                                    <asp:LinkButton id="img_save" runat="server" CommandName="Edit" class="btn btn-success btn-circle" Visible="false" ><i class="glyphicon glyphicon-check"></i></asp:LinkButton>
                                                    &nbsp;&nbsp;<asp:LinkButton id="img_delete" runat="server" CommandName="Delete" OnClientClick="return validate()" class="btn btn-danger btn-circle"><i class="glyphicon glyphicon-remove"></i></asp:LinkButton>
                                                    </TD>
                                                    </tr>
                                            </TBODY>                            
                                        </TABLE> 
                                   </ItemTemplate>
                                   <FooterTemplate>
                                        <TABLE  cellSpacing="0" cellPadding="0" width="100%" border="0">
                                            
                                            <TBODY>
                                                <TR>
                                                    <TD width="65%" >
                                                    <input runat="server" id="txtDepartmentAdd" class="form-control" maxlength="50" style="width: 100%"/>
                                                    </td>
                                                    <TD align="left">
                                                        &nbsp;&nbsp; &nbsp;&nbsp;
                                                    <asp:LinkButton ID="LinkButton1" CommandName="addDepartment" runat="server" CssClass="btn btn-success btn-circle " ><i class="glyphicon glyphicon-check"></i></asp:LinkButton>
                                                    </TD>
                                                    </tr>
                                            </TBODY>                            
                                        </TABLE> 
                                   </FooterTemplate>
                               </asp:TemplateField>
                            </Columns>                         
                        </asp:GridView>
                                                </div>
                                                </div>
                                            </div>
                                        </div>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td>
                                         &nbsp;</td>
                                     <td>

                                         &nbsp;</td>
                                 </tr>
                                 <tr>
                                     <td colspan="2">
                                     <div style="width: 100%">
                                            <div  class="panel panel-default">
                                                <div class="panel-heading">
                                                    Classes
                                                </div>
                                                <div id="div3" runat="server" style="overflow: auto; height: 250px;">
                                                <div class="panel-body">
                                     <asp:GridView ID="GridClass" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                                DataKeyNames="ClassID" ShowFooter="True" onrowcommand="gridClass_RowCommand" 
                                                        OnRowDeleting="DeleteClass" OnRowEditing="EditClass" OnRowUpdating="UpdateClass">
                             <Columns>
                             <asp:TemplateField>
                                <HeaderTemplate>
                                S.No.</HeaderTemplate>
                                <ItemTemplate>
                                <asp:Label ID="lblSRNO" runat="server" 
                                    Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField>
                                    
                                    <HeaderTemplate>
                                         <TABLE  cellSpacing="0" cellPadding="0" width="100%" border="0">
                                            
                                            <TBODY>
                                                <TR>
                                                  
                                                    <TD width="25%" align="left">
                                                        Course
                                                            </TD>
                                                    <td width="20%" align="left">
                                                        Department
                                                            </td>
                                                    <td width="10%" align="left">
                                                        Section
                                                        </td>
                                                    <td width="10%" align="left">
                                                        Year
                                                    </td>
                                                     <td width="11%" align="left">
                                                        Class Name
                                                    </td>
                                                    <td width="10%" align="left">
                                                        
                                                    </td>
                                                   
                                                    </tr>
                                            </TBODY>                            
                                        </TABLE> 
                                   </HeaderTemplate>

                                   <ItemTemplate>
                                         <TABLE  cellSpacing="0" cellPadding="0" width="100%" border="0">
                                            <TBODY>
                                                <TR>
                                                    <TD width="25%">
                                                    <asp:DropDownList runat="server" id="ddlCourseName" onkeydown="AllowOnlyText1(event);" DataSourceID="SqlDSCourse" DataTextField="CourseName" DataValueField="CourseName"  SelectedValue='<%#Eval("CourseName")%>' Enabled="false"
                                                            class="form-control" maxlength="50" style="width: 100%" ></asp:DropDownList>
                                                            <asp:SqlDataSource ID="SqlDSCourse" runat="server" 
                                                                ConnectionString="<%$ ConnectionStrings:connectionstring %>" 
                                                                SelectCommand="SELECT [CourseName] FROM [Student_Course] WHERE ([pn_BranchID] = @pn_BranchID)">
                                                                <SelectParameters>
                                                                    <asp:SessionParameter Name="pn_BranchID" SessionField="Login_Temp_BranchID" 
                                                                        Type="Int32" />
                                                                </SelectParameters>
                                                            </asp:SqlDataSource>
                                                            </TD>
                                                    <td width="28%">
                                                    <asp:DropDownList runat="server" id="ddlDepartmentName" onkeydown="AllowOnlyText1(event);" DataSourceID="SqlDSDept" DataTextField="DepartmentName" DataValueField="DepartmentName"  SelectedValue='<%#Eval("DepartmentName")%>' Enabled="false"
                                                            class="form-control" maxlength="50" style="width: 100%" ></asp:DropDownList>
                                                            <asp:SqlDataSource ID="SqlDSDept" runat="server" 
                                                                ConnectionString="<%$ ConnectionStrings:connectionstring %>" 
                                                                SelectCommand="SELECT [DepartmentName] FROM [Student_Department] WHERE ([pn_BranchID] = @pn_BranchID and [pn_DepartmentID] != @pn_DepartmentID )">
                                                                <SelectParameters>
                                                                    <asp:SessionParameter Name="pn_BranchID" SessionField="Login_Temp_BranchID" 
                                                                        Type="Int32" />
                                                                        <asp:SessionParameter Name="pn_DepartmentID" 
                                                                        Type="Int32" DefaultValue="1" />
                                                                </SelectParameters>
                                                            </asp:SqlDataSource>
                                                            </td>
                                                    <td width="11%">
                                                    <asp:DropDownList runat="server" id="ddlSection" onkeydown="AllowOnlyText1(event);" Enabled="false" 
                                                            class="form-control" maxlength="50" style="width: 100%" >
                                                            <asp:ListItem Text='a' Value='a'></asp:ListItem>
                                                            <asp:ListItem Text="B" Value="B"></asp:ListItem>
                                                            </asp:DropDownList></td>
                                                    <td width="11%">
                                                    <asp:DropDownList runat="server" id="ddlYear" onkeydown="AllowOnlyText1(event);" SelectedValue='<%# Bind("PGCompletedYear") %>' Enabled="false" 
                                                            class="form-control" maxlength="50" style="width: 100%" >
                                                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                            </asp:DropDownList></td>
                                                     <td width="11%">
                                                    <input runat="server" id="txtClassName" onkeydown="AllowOnlyText1(event);" value='<%#Eval("ClassName")%>' disabled="disabled" 
                                                            class="form-control" maxlength="50" style="width: 100%" /></td>
                                                   
                                                    <TD align="right">
                                                    <asp:LinkButton id="img_update"  runat="server" class="btn btn-info btn-circle" CommandName="Update"><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                                    <asp:LinkButton id="img_save" runat="server" CommandName="Edit" class="btn btn-success btn-circle" Visible="false" ><i class="glyphicon glyphicon-check"></i></asp:LinkButton>
                                                    &nbsp;&nbsp;<asp:LinkButton id="img_delete" runat="server" CommandName="Delete" OnClientClick="return validate()" class="btn btn-danger btn-circle"><i class="glyphicon glyphicon-remove"></i></asp:LinkButton>
                                                    </TD>
                                                    </tr>
                                            </TBODY>                            
                                        </TABLE> 
                                   </ItemTemplate>
                                   <FooterTemplate>
                                        <TABLE  cellSpacing="0" cellPadding="0" width="100%" border="0">
                                            
                                            <TBODY>
                                                <TR>
                                                    <TD  width="11%">
                                                    <asp:DropDownList runat="server" id="ddlCourseAdd" onkeydown="AllowOnlyText1(event);"  DataSourceID="SqlDSCourse" DataTextField="CourseName" DataValueField="CourseName"
                                                            class="form-control" maxlength="50" style="width: 100%" ></asp:DropDownList>
                                                            <asp:SqlDataSource ID="SqlDSCourse" runat="server" 
                                                                ConnectionString="<%$ ConnectionStrings:connectionstring %>" 
                                                                SelectCommand="SELECT [CourseName] FROM [Student_Course] WHERE ([pn_BranchID] = @pn_BranchID) order by pn_CourseID Asc">
                                                                <SelectParameters>
                                                                    <asp:SessionParameter Name="pn_BranchID" SessionField="Login_Temp_BranchID" 
                                                                        Type="Int32" />
                                                                </SelectParameters>
                                                            </asp:SqlDataSource>
                                                            </TD>
                                                    <td  width="28%">
                                                    <asp:DropDownList runat="server" id="ddlDepartmentAdd" onkeydown="AllowOnlyText1(event);" DataSourceID="SqlDSDept" DataTextField="DepartmentName" DataValueField="DepartmentName"
                                                            class="form-control" maxlength="50" style="width: 100%" ></asp:DropDownList>
                                                            <asp:SqlDataSource ID="SqlDSDept" runat="server" 
                                                                ConnectionString="<%$ ConnectionStrings:connectionstring %>" 
                                                                SelectCommand="SELECT [DepartmentName] FROM [Student_Department] WHERE ([pn_BranchID] = @pn_BranchID) order by pn_DepartmentID Asc">
                                                                <SelectParameters>
                                                                    <asp:SessionParameter Name="pn_BranchID" SessionField="Login_Temp_BranchID" 
                                                                        Type="Int32" />
                                                                </SelectParameters>
                                                            </asp:SqlDataSource>
                                                            </td>
                                                    <td  width="11%">
                                                    <asp:DropDownList runat="server" id="ddlSectionAdd" onkeydown="AllowOnlyText1(event);" 
                                                            class="form-control" maxlength="50" style="width: 100%" >
                                                            <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                                            <asp:ListItem Text='a' Value='a'></asp:ListItem>
                                                            <asp:ListItem Text="B" Value="B"></asp:ListItem>
                                                            </asp:DropDownList></td>
                                                    <td  width="11%">
                                                    <asp:DropDownList runat="server" id="ddlYearAdd" onkeydown="AllowOnlyText1(event);" 
                                                            class="form-control" maxlength="50" style="width: 100%" >
                                                            <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                            </asp:DropDownList></td>
                                                     <td  width="28%">
                                                    <input runat="server" id="txtClassAdd" class="form-control" maxlength="50" style="width: 100%" /></td>
                                                    <TD align="left">
                                                        &nbsp;&nbsp; &nbsp;&nbsp;
                                                    <asp:LinkButton ID="LinkButton1" CommandName="addClass" runat="server" CssClass="btn btn-success btn-circle " ><i class="glyphicon glyphicon-check"></i></asp:LinkButton>
                                                    </TD>
                                                    </tr>
                                            </TBODY>                            
                                        </TABLE> 
                                   </FooterTemplate>
                               </asp:TemplateField>
                            </Columns>                         
                        </asp:GridView>
                                     </div>
                                     </div>
                                     </div>
                                     </div>
                                     </td>
                                 </tr>
                             </table>

                        </div>
                        <!-- /.panel-body -->
                    </div>
                    </ContentTemplate>
                    </asp:UpdatePanel>
            </asp:Content>

