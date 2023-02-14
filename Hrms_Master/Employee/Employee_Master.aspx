<%@ Page MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Employee_Master.aspx.cs" Inherits="Hrms_Master_Default" Title="Welcome to HRMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../../js/jquery.min.js"></script>
    <script type="text/javascript" src="WaterMark.min.js"></script>
   
    
    <script type = "text/javascript">
    var ddlText, ddlValue, ddl,txt, lblMesg;
    
    function FilterItems(value) 
    {
        ddl.style.display = 'inherit'; 
        ddl.options.length = 0;
        for (var i = 0; i < ddlText.length; i++) 
        {
            if (ddlText[i].toLowerCase().indexOf(value) != -1) 
            {
                AddItem(ddlText[i], ddlValue[i]);
            }
        }

        if (ddl.options.length == 0) {
            ddl.style.display = 'none';
        }
        if (value.length == 0) {
            ddl.style.display = 'none';
        }
    }
    
    function AddItem(text, value) {
        var opt = document.createElement("option");
        opt.text = text;
        opt.value = value;
        ddl.options.add(opt);
    }
</script>
    <style type="text/css">
        div.myautoscroll1 {
             overflow: auto;
             color: Gray;
        }
        div.myautoscroll {
            height: 25ex;

            overflow: hidden;
        }
        div.myautoscroll:hover {
            overflow: auto;
        }

        div.myautoscroll:hover p {
            padding-right: 5px;
        }
    </style>
    <script type="text/javascript">
    $(function () {
        $("[id*=txt_dept], [id*=txt_desg], [id*=txt_div], [id*=txt_lvl], [id*=txt_grd], [id*=txt_cat], [id*=txt_jt], [id*=txt_oc], [id*=txt_ast]").WaterMark();
    });
    </script>
    
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
    
    function show_message()
    {
        alert("Added successfully");
    }
    
    function show_Error()
    {
        alert("Enter Department Name");
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
 <asp:ScriptManager ID="ScriptManager1" runat="server"/>
 
 <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-12">
                        <h2 class="page-header">Employee Position Master</h2>
                     
                    </div>
                    <span class="pull-right" > <asp:DropDownList ID="ddl_branch" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddl_branch_SelectedIndexChanged" Width="115px" Visible="true">
                                </asp:DropDownList>
                         <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                            ConnectionString="<%$ ConnectionStrings:connectionstring %>"
                            SelectCommand="SELECT ([EmployeeCode]+'-'+[Employee_First_Name]) as Employee_first_name , [EmployeeCode] FROM [paym_Employee] WHERE (([pn_CompanyID] = @pn_CompanyID) AND ([pn_BranchID] = @pn_BranchID))">
                            <SelectParameters>
                                <asp:SessionParameter Name="pn_CompanyID" SessionField="Login_temp_CompanyID"
                                    Type="Int32" />
                                <asp:SessionParameter Name="pn_BranchID" SessionField="Login_temp_BranchID"
                                    Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </span>
                    <!-- /.col-lg-12 -->
                </div>
                <!-- /.row -->
            </div>

    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td id="tdComposeHeader" valign="top" align="center">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                <fieldset>
                <table style="width: 95%">
                    <tr>
                        <td width="33%">
                            &nbsp;</td>
                        <td width="33%">
                            &nbsp;</td>
                        <td width="33%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="33%">
                            <table width="100%" class="table table-striped table-bordered table-hover">
                                <tr>
                                    <td colspan="3" height: 25px;">
                                        &nbsp;&nbsp;&nbsp; Department Master</td>
                                </tr>
                                <tr>
                                    <td width="75%" style="height: 35px" align="center">
                                        <asp:TextBox CssClass="form-control" ID="txt_dept" runat="server" ToolTip="Enter Department Name" Width="90%"  onkeyup = "FilterItems(this.value)"></asp:TextBox>
                                        
                                    </td>
                                    <td width="12.5%" style="height: 35px">
                                     <asp:LinkButton id="btn_add" onclick="btn_add_Click"  runat="server" class="btn btn-success btn-circle"><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                        
                                        </td>
                                    <td width="12.5%" style="height: 35px">
                                    <asp:LinkButton id="Btn_del" onclick="Btn_del_Click" OnClientClick="return validate()" runat="server" class="btn btn-danger btn-circle" CommandName="Update"><i class="glyphicon glyphicon-remove"></i></asp:LinkButton>
                                        </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                    <div class="myautoscroll">
                                        <asp:GridView ID="Gv_Dept"  runat="server" AutoGenerateColumns="False" Width="100%"
	                                          HorizontalAlign="Center" GridLines="None" Height="100%" ShowHeader="False" 
                                            onrowdatabound="Gv_Dept_RowDataBound" 
                                            onselectedindexchanged="Dept_update">
                                                   <FooterStyle BackColor="#5D7B9D" ForeColor="White" BorderStyle="None" Font-Size="Small" Font-Bold="True" />
                                                   <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Names="Calibri" BorderColor="#333333" BorderStyle="None" Font-Size="Small" />
                                                    <Columns>
                                                    
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Did" Visible="false" runat="server" Text='<%# Eval("pn_DepartmentID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:TemplateField>

	                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Dname" runat="server" Text='<%# Eval("v_DepartmentName") %>'></asp:Label>
                                                            </ItemTemplate>
	                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:TemplateField>
                                                        </Columns>
                                        </asp:GridView>
                                    </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="33%">
                            <table width="100%" class="table table-striped table-bordered table-hover">
                                <tr>
                                    <td colspan="3" height: "25px;">
                                        &nbsp;&nbsp;&nbsp; Designation Master</td>
                                </tr>
                                <tr>
                                    <td align="center" style="height: 35px" width="75%">
                                        <asp:TextBox ID="txt_desg" runat="server" CssClass="form-control"
                                            ToolTip="Enter Designation Name" Width="90%"></asp:TextBox>
                                    </td>
                                    <td style="height: 35px" width="12.5%">
                                    <asp:LinkButton id="btn_adddesg" onclick="btn_adddesg_Click"  runat="server" class="btn btn-success btn-circle"><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>

                                    </td>
                                    <td style="height: 35px" width="12.5%">
                                    <asp:LinkButton id="Btn_deldesg" onclick="Btn_deldesg_Click" OnClientClick="return validate()" runat="server" class="btn btn-danger btn-circle" CommandName="Update"><i class="glyphicon glyphicon-remove"></i></asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div class="myautoscroll">
                                            <asp:GridView ID="Gv_Desg" runat="server" AutoGenerateColumns="False" 
                                                CellPadding="4" Font-Size="Small" ForeColor="#333333" GridLines="None" 
                                                Height="100%" HorizontalAlign="Center" 
                                                onrowdatabound="Gv_Desg_RowDataBound"  ShowHeader="False" 
                                                style="color: #808000" Width="100%" 
                                                onselectedindexchanged="desg_update">
                                                <FooterStyle BackColor="#5D7B9D" BorderStyle="None" Font-Bold="True" 
                                                    Font-Size="Small" ForeColor="White" />
                                                <RowStyle BackColor="#F7F6F3" BorderColor="#333333" BorderStyle="None" 
                                                    Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" />
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Dsid" runat="server" Text='<%# Eval("pn_DesignationID") %>' 
                                                                Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Dsname" runat="server" 
                                                                Text='<%# Eval("v_DesignationName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="33%">
                            <table width="100%" class="table table-striped table-bordered table-hover">
                                <tr>
                                    <td colspan="3" height: 25px;">
                                        &nbsp;&nbsp;&nbsp; Division Master</td>
                                </tr>
                                <tr>
                                    <td align="center" style="height: 35px" width="75%">
                                        <asp:TextBox ID="txt_div" runat="server" CssClass="form-control" 
                                            ToolTip="Enter Division Name" Width="90%"></asp:TextBox>
                                    </td>
                                    <td style="height: 35px" width="12.5%">
                                    <asp:LinkButton id="btn_adddiv" onclick="btn_adddiv_Click"  runat="server" class="btn btn-success btn-circle"><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                    </td>
                                    <td style="height: 35px" width="12.5%">
                                    <asp:LinkButton id="Btn_deldiv" onclick="Btn_deldiv_Click" OnClientClick="return validate()" runat="server" class="btn btn-danger btn-circle" CommandName="Update"><i class="glyphicon glyphicon-remove"></i></asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div class="myautoscroll">
                                            <asp:GridView ID="Gv_Div" runat="server" AutoGenerateColumns="False" 
                                                CellPadding="4" Font-Size="Small" ForeColor="#333333" GridLines="None" 
                                                Height="100%" HorizontalAlign="Center" onrowdatabound="Gv_Div_RowDataBound" 
                                                onselectedindexchanged="Gv_Div_SelectedIndexChanged" ShowHeader="False" 
                                                style="color: #808000" Width="100%">
                                                <FooterStyle BackColor="#5D7B9D" BorderStyle="None" Font-Bold="True" 
                                                    Font-Size="Small" ForeColor="White" />
                                                <RowStyle BackColor="#F7F6F3" BorderColor="#333333" BorderStyle="None" 
                                                    Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" />
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Dvid" runat="server" Text='<%# Eval("pn_DivisionID") %>' 
                                                                Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Dvname" runat="server" 
                                                                Text='<%# Eval("v_DivisionName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table class="table table-striped table-bordered table-hover"
                                width="100%">
                                <tr>
                                    <td colspan="3"  height: "25px;">
                                        &nbsp;&nbsp;&nbsp; Level Master</td>
                                </tr>
                                <tr>
                                    <td align="center" style="height: 35px" width="75%">
                                        <asp:TextBox ID="txt_lvl" runat="server" CssClass="form-control" 
                                            ToolTip="Enter Level Name" Width="90%"></asp:TextBox>
                                    </td>
                                    <td style="height: 35px" width="12.5%">
                                    <asp:LinkButton id="btn_addlvl" onclick="btn_addlvl_Click"  runat="server" class="btn btn-success btn-circle"><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                    </td>
                                    <td style="height: 35px" width="12.5%">
                                    <asp:LinkButton id="Btn_dellvl" onclick="Btn_dellvl_Click" OnClientClick="return validate()" runat="server" class="btn btn-danger btn-circle" CommandName="Update"><i class="glyphicon glyphicon-remove"></i></asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div class="myautoscroll">
                                            <asp:GridView ID="Gv_Lvl" runat="server" AutoGenerateColumns="False" 
                                                CellPadding="4" Font-Size="Small" ForeColor="#333333" GridLines="None" 
                                                Height="100%" HorizontalAlign="Center" onrowdatabound="Gv_Lvl_RowDataBound" 
                                                onselectedindexchanged="Gv_Lvl_SelectedIndexChanged" ShowHeader="False" 
                                                style="color: #808000" Width="100%">
                                                <FooterStyle BackColor="#5D7B9D" BorderStyle="None" Font-Bold="True" 
                                                    Font-Size="Small" ForeColor="White" />
                                                <RowStyle BackColor="#F7F6F3" BorderColor="#333333" BorderStyle="None" 
                                                    Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" />
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Lid" runat="server" Text='<%# Eval("pn_LevelID") %>' 
                                                                Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Lname" runat="server" 
                                                                Text='<%# Eval("v_LevelName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table class="table table-striped table-bordered table-hover" 
                                width="100%">
                                <tr>
                                    <td colspan="3" height: "25px;">
                                        &nbsp;&nbsp;&nbsp; Grade Master</td>
                                </tr>
                                <tr>
                                    <td align="center" style="height: 35px" width="75%">
                                        <asp:TextBox ID="txt_grd" runat="server" CssClass="form-control" 
                                            ToolTip="Enter Grade Name" Width="90%"></asp:TextBox>
                                    </td>
                                    <td style="height: 35px" width="12.5%">
                                    <asp:LinkButton id="btn_addgrd" onclick="btn_addgrd_Click"  runat="server" class="btn btn-success btn-circle"><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                    </td>
                                    <td style="height: 35px" width="12.5%">
                                    <asp:LinkButton id="Btn_delgrd" onclick="Btn_delgrd_Click" OnClientClick="return validate()" runat="server" class="btn btn-danger btn-circle" CommandName="Update"><i class="glyphicon glyphicon-remove"></i></asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div class="myautoscroll">
                                            <asp:GridView ID="Gv_Grd" runat="server" AutoGenerateColumns="False" 
                                                CellPadding="4" Font-Size="Small" ForeColor="#333333" GridLines="None" 
                                                Height="100%" HorizontalAlign="Center" onrowdatabound="Gv_Grd_RowDataBound" 
                                                onselectedindexchanged="Gv_Grd_SelectedIndexChanged" ShowHeader="False" 
                                                style="color: #808000" Width="100%">
                                                <FooterStyle BackColor="#5D7B9D" BorderStyle="None" Font-Bold="True" 
                                                    Font-Size="Small" ForeColor="White" />
                                                <RowStyle BackColor="#F7F6F3" BorderColor="#333333" BorderStyle="None" 
                                                    Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" />
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Gid" runat="server" Text='<%# Eval("pn_GradeID") %>' 
                                                                Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Gname" runat="server" 
                                                                Text='<%# Eval("v_GradeName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table class="table table-striped table-bordered table-hover"
                                width="100%">
                                <tr>
                                    <td colspan="3" height: "25px;">
                                        &nbsp;&nbsp;&nbsp; Category Master</td>
                                </tr>
                                <tr>
                                    <td align="center" style="height: 35px" width="75%">
                                        <asp:TextBox ID="txt_cat" runat="server" CssClass="form-control" 
                                            ToolTip="Enter Category Name" Width="90%"></asp:TextBox>
                                    </td>
                                    <td style="height: 35px" width="12.5%">
                                    <asp:LinkButton id="btn_addcat" onclick="btn_addcat_Click"  runat="server" class="btn btn-success btn-circle"><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                    </td>
                                    <td style="height: 35px" width="12.5%">
                                    <asp:LinkButton id="Btn_delcat" onclick="Btn_delcat_Click" OnClientClick="return validate()" runat="server" class="btn btn-danger btn-circle" CommandName="Update"><i class="glyphicon glyphicon-remove"></i></asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div class="myautoscroll">
                                            <asp:GridView ID="Gv_Cat" runat="server" AutoGenerateColumns="False" 
                                                CellPadding="4" Font-Size="Small" ForeColor="#333333" GridLines="None" 
                                                Height="100%" HorizontalAlign="Center" onrowdatabound="Gv_Cat_RowDataBound" 
                                                onselectedindexchanged="Gv_Cat_SelectedIndexChanged" ShowHeader="False" 
                                                style="color: #808000" Width="100%">
                                                <FooterStyle BackColor="#5D7B9D" BorderStyle="None" Font-Bold="True" 
                                                    Font-Size="Small" ForeColor="White" />
                                                <RowStyle BackColor="#F7F6F3" BorderColor="#333333" BorderStyle="None" 
                                                    Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" />
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Cid" runat="server" Text='<%# Eval("pn_CategoryID") %>' 
                                                                Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Cname" runat="server" 
                                                                Text='<%# Eval("v_CategoryName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table class="table table-striped table-bordered table-hover"
                                width="100%">
                                <tr>
                                    <td colspan="3" 
                                        style="height: 25px;">
                                        &nbsp;&nbsp;&nbsp; Job Type Master</td>
                                </tr>
                                <tr>
                                    <td align="center" style="height: 35px" width="75%">
                                        <asp:TextBox ID="txt_jt" runat="server" CssClass="form-control" 
                                            ToolTip="Enter Job Type Name" Width="90%"></asp:TextBox>
                                    </td>
                                    <td style="height: 35px" width="12.5%">
                                    <asp:LinkButton id="btn_addjt" onclick="btn_addjt_Click"  runat="server" class="btn btn-success btn-circle"><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                    </td>
                                    <td style="height: 35px" width="12.5%">
                                    <asp:LinkButton id="Btn_deljt" onclick="Btn_deljt_Click" OnClientClick="return validate()" runat="server" class="btn btn-danger btn-circle" CommandName="Update"><i class="glyphicon glyphicon-remove"></i></asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div class="myautoscroll">
                                            <asp:GridView ID="Gv_Jt" runat="server" AutoGenerateColumns="False" 
                                                CellPadding="4" Font-Size="Small" ForeColor="#333333" GridLines="None" 
                                                Height="100%" HorizontalAlign="Center" onrowdatabound="Gv_Jt_RowDataBound" 
                                                onselectedindexchanged="Gv_Jt_SelectedIndexChanged" ShowHeader="False" 
                                                style="color: #808000" Width="100%">
                                                <FooterStyle BackColor="#5D7B9D" BorderStyle="None" Font-Bold="True" 
                                                    Font-Size="Small" ForeColor="White" />
                                                <RowStyle BackColor="#F7F6F3" BorderColor="#333333" BorderStyle="None" 
                                                    Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" />
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Jid" runat="server" Text='<%# Eval("pn_JobStatusID") %>' 
                                                                Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Jname" runat="server" 
                                                                Text='<%# Eval("v_JobStatusName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table class="table table-striped table-bordered table-hover"
                                width="100%">
                                <tr>
                                    <td colspan="3" 
                                        style=" height: 25px;">
                                        &nbsp;&nbsp;&nbsp; Overhead Cost Master</td>
                                </tr>
                                <tr>
                                    <td align="center" style="height: 35px" width="75%">
                                        <asp:TextBox ID="txt_oc" runat="server" CssClass="form-control" 
                                            ToolTip="Overhead Cost" Width="90%"></asp:TextBox>
                                    </td>
                                    <td style="height: 35px" width="12.5%">
                                    <asp:LinkButton id="btn_addoc" onclick="btn_addoc_Click"  runat="server" class="btn btn-success btn-circle"><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                    </td>
                                    <td style="height: 35px" width="12.5%">
                                    <asp:LinkButton id="Btn_deloc" onclick="Btn_deloc_Click" OnClientClick="return validate()" runat="server" class="btn btn-danger btn-circle" CommandName="Update"><i class="glyphicon glyphicon-remove"></i></asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div class="myautoscroll">
                                            <asp:GridView ID="Gv_Oc" runat="server" AutoGenerateColumns="False" 
                                                CellPadding="4" Font-Size="Small" ForeColor="#333333" GridLines="None" 
                                                Height="100%" HorizontalAlign="Center" onrowdatabound="Gv_Oc_RowDataBound" 
                                                onselectedindexchanged="Gv_Oc_SelectedIndexChanged" ShowHeader="False" 
                                                style="color: #808000" Width="100%">
                                                <FooterStyle BackColor="#5D7B9D" BorderStyle="None" Font-Bold="True" 
                                                    Font-Size="Small" ForeColor="White" />
                                                <RowStyle BackColor="#F7F6F3" BorderColor="#333333" BorderStyle="None" 
                                                    Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" />
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Oid" runat="server" Text='<%# Eval("OverHeadingID") %>' 
                                                                Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Oname" runat="server" 
                                                                Text='<%# Eval("OverHeadingName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table class="table table-striped table-bordered table-hover" 
                                width="100%">
                                <tr>
                                    <td colspan="3" 
                                        style=" height: 25px;">
                                        &nbsp;&nbsp;&nbsp; Asset Master</td>
                                </tr>
                                <tr>
                                    <td align="center" style="height: 35px" width="75%">
                                        <asp:TextBox ID="txt_ast" runat="server" CssClass="form-control" 
                                            ToolTip="Enter Asset Name" Width="90%"></asp:TextBox>
                                    </td>
                                    <td style="height: 35px" width="12.5%">
                                    <asp:LinkButton id="btn_addast" onclick="btn_addast_Click"  runat="server" class="btn btn-success btn-circle"><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                    </td>
                                    <td style="height: 35px" width="12.5%">
                                    <asp:LinkButton id="Btn_delast" onclick="Btn_delast_Click" OnClientClick="return validate()" runat="server" class="btn btn-danger btn-circle" CommandName="Update"><i class="glyphicon glyphicon-remove"></i></asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div class="myautoscroll">
                                            <asp:GridView ID="Gv_Ast" runat="server" AutoGenerateColumns="False" 
                                                CellPadding="4" Font-Size="Small" ForeColor="#333333" GridLines="None" 
                                                Height="100%" HorizontalAlign="Center" onrowdatabound="Gv_Ast_RowDataBound" 
                                                onselectedindexchanged="Gv_Ast_SelectedIndexChanged" ShowHeader="False" 
                                                style="color: #808000" Width="100%">
                                                <FooterStyle BackColor="#5D7B9D" BorderStyle="None" Font-Bold="True" 
                                                    Font-Size="Small" ForeColor="White" />
                                                <RowStyle BackColor="#F7F6F3" BorderColor="#333333" BorderStyle="None" 
                                                    Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" />
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Aid" runat="server" Text='<%# Eval("pn_AssetID") %>' 
                                                                Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Aname" runat="server" 
                                                                Text='<%# Eval("Asset_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
            </table>
            </fieldset>
            </ContentTemplate>
            </asp:UpdatePanel>
             </td>
         </tr>
    </table>
</asp:Content>