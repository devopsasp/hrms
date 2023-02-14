<%@ Page Language="C#" MasterPageFile="~/HRMS.master"
    AutoEventWireup="true" CodeFile="shiftbalanceentry.aspx.cs" Inherits="Hrms_Master_Default"
    Title="Welcome to HRMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/css">
  
        

    .scrollingControlContainer
    {
        overflow-x: hidden;
        overflow-y: scroll;
    }
    
    </script>
    <style type="text/css">
        .form-control1 {
            display: block;
            width: 100%;
            height: 34px;
            font-size: 14px;
            line-height: 1.42857143;
            color: #555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
        }



        .chkChoice label {
            margin-left: -20px;
        }

        .chkChoice td {
            padding-right: 20px;
        }

        table.test tr td label {
            margin-left: 10px;
        }
    </style>
    <link href="../../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript" src="../../Scripts/Datavalid.js"></script>

    <script language="javascript" type="text/javascript">
        function isNumberKey(key) {
            //getting key code of pressed key
            var keycode = (key.which) ? key.which : key.keyCode;
            //comparing pressed keycodes

            if (keycode > 31 && (keycode < 48 || keycode > 57) && keycode != 47) {

                return false;
            }
            else return true;
        }


        function show_message() {
            alert("Course Name Already Exist");
        }

        function show_Error() {
            alert("Enter Course Name");
        }

        function fnSave() {
            if (document.aspnetForm.ctl00$ContentPlaceHolder1$CourseName.value == "") {
                alert("Enter Course Name");
                aspnetForm.ctl00$ContentPlaceHolder1$CourseName.focus();
                return false;
            }
            else {
                return true;
            }
        }
        function coudnt_del() {
            alert("Cannot delete. It is already assigned to someone");
        }

        function fn_date(event, txtid) {
            var len;
            var txtvalue;
            var bool_obj;
            var i;

            txtvalue = document.getElementById(txtid).value;
            txtlen = txtvalue.length;

            if (event.keyCode != 8 && event.keyCode != 46 && event.keyCode != 35 && event.keyCode != 36 && event.keyCode != 37 && event.keyCode != 38 && event.keyCode != 39 && event.keyCode != 40) {
                if (txtlen != 0) {
                    if (txtlen == 2) {
                        document.getElementById(txtid).value = txtvalue + "/";
                    }
                    else {
                        document.getElementById(txtid).value = txtvalue;
                    }
                }
            }
        }

        function fn_date1(event, txtid) {
            var len;
            var txtvalue;
            var bool_obj;
            var i;

            txtvalue = document.getElementById(txtid).value;
            txtlen = txtvalue.length;

            if (event.keyCode != 8 && event.keyCode != 46 && event.keyCode != 35 && event.keyCode != 36 && event.keyCode != 37 && event.keyCode != 38 && event.keyCode != 39 && event.keyCode != 40) {

                if (txtlen != 0) {
                    bool_obj = true;

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


        function fn_limit(event, txtid) {
            var len;
            var txtvalue;
            var bool_obj;
            var i, j;
            var str = "";
            str.substring(4, 2);
            txtvalue = document.getElementById(txt_monyear).value;
            txtlen = txtvalue.length;

            if (event.keyCode != 8 && event.keyCode != 46 && event.keyCode != 35 && event.keyCode != 36 && event.keyCode != 37 && event.keyCode != 38 && event.keyCode != 39 && event.keyCode != 40) {
                if (txtlen != 0) {
                    if (txtlen == 2) {
                        if (txtvalue < 31) {
                            document.getElementById(txt_monyear).value = "";
                        }
                        else {
                            document.getElementById(txt_monyear).value = txtvalue + "";
                        }
                    }
                    else {
                        document.getElementById(txtid).value = txtvalue;
                    }
                }
            }
        }


    </script>

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

            <div class="row">
                <div class="col-lg-12">
                    <h2 class="page-header">Shift Balance Entry<span class="Title"><span
                        style="font-family: Calibri; color: #FFFFFF; font-weight: bold; font-size: medium;"><asp:SqlDataSource ID="SqlDataSource1" runat="server"
                            ConnectionString="<%$ ConnectionStrings:connectionstring %>"
                            SelectCommand="SELECT ([EmployeeCode]+'-'+[Employee_First_Name]) as Employee_first_name , [EmployeeCode] FROM [paym_Employee] WHERE (([pn_CompanyID] = @pn_CompanyID) AND ([pn_BranchID] = @pn_BranchID))">
                            <SelectParameters>
                                <asp:SessionParameter Name="pn_CompanyID" SessionField="Login_temp_CompanyID"
                                    Type="Int32" />
                                <asp:SessionParameter Name="pn_BranchID" SessionField="Login_temp_BranchID"
                                    Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </span></span></h2>
                </div>
                <!-- /.col-lg-12 -->
            </div>

            <div class="panel panel-default" style="overflow: scroll;">
                <div class="panel-heading">
                    Shift Details
                            <div class="pull-right" >
                                <div class="btn-group">
                                    <asp:DropDownList ID="ddl_branch" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddl_branch_SelectedIndexChanged" Width="115px" enabled="true" Visible="true">
                                    </asp:DropDownList>
                                    <ul class="dropdown-menu pull-right" role="menu">
                                        <li><a href="#">Action</a>
                                        </li>
                                        <li><a href="#">Another action</a>
                                        </li>
                                        <li><a href="#">Something else here</a>
                                        </li>
                                        <li class="divider"></li>
                                        <li><a href="#">Separated link</a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                </div>
                <!-- /.panel-heading -->
                <div class="panel-body">
                    <div id="morris-area-chart">

                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" onrowediting="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                            OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting" OnRowUpdating="GridView1_RowUpdating"
                            OnSelectedIndexChanged="GridView1_SelectedIndexChanged" ShowFooter="True" CssClass="table table-hover table-no-bordered" Height="435px" Width="751px">

                            <Columns>
                                <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left" HeaderText="Branch"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_branch" runat="server" Text='<%# Eval("pn_branchid") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_editbranch" runat="server" CssClass="form-control"
                                            Text='<%# Bind("pn_branchid") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txt_branch" runat="server" CssClass="form-control"></asp:TextBox>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Pattern Code"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_pattern" runat="server" Text='<%# Eval("pattern_code") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_editpattern" runat="server" CssClass="form-control"
                                            Text='<%# Bind("pattern_code") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txt_pattern" onKeyDown="return disableKeyPress(event)" runat="server" CssClass="form-control"
                                            BackColor="#FFFFCC" MaxLength="2"></asp:TextBox>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Code"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_shiftcode1" runat="server" Text='<%# Eval("shift_code1") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddl_editshiftcode1" runat="server" CssClass="form-control1"
                                            DataSourceID="SqlDataSource2" DataTextField="shift_code"
                                            DataValueField="shift_code">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                                            ConnectionString="<%$ ConnectionStrings:connectionstring %>"
                                            ProviderName="<%$ ConnectionStrings:connectionstring.ProviderName %>"
                                            SelectCommand="SELECT [shift_code] FROM [shift_details] WHERE (([pn_companyid] = @pn_companyid) AND ([pn_branchid] = @pn_branchid))">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="pn_companyid" SessionField="Login_temp_companyID"
                                                    Type="Int32" />
                                                <asp:SessionParameter Name="pn_branchid" SessionField="Login_temp_BranchID"
                                                    Type="Int32" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddl_shiftcode1" runat="server" CssClass="form-control1"
                                            BackColor="#FFFFCC">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Days"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_days1" runat="server" Text='<%# Eval("days1") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="lbl_editdays1" runat="server" CssClass="form-control"
                                            Text='<%# Bind("days1") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txt_days1" runat="server" onkeydown="AllowOnlyNumeric1(event);" CssClass="form-control"
                                            BackColor="#FFFFCC" MaxLength="1"></asp:TextBox>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Code"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_shiftcode2" runat="server" Text='<%# Eval("shift_code2") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddl_editshiftcode2" runat="server" CssClass="form-control1"
                                            DataSourceID="SqlDataSource2" DataTextField="shift_code"
                                            DataValueField="shift_code">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddl_shiftcode2" runat="server" CssClass="form-control1"
                                            BackColor="#FFFFCC">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Days"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_days2" runat="server" Text='<%# Eval("days2") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="lbl_editdays2" runat="server" CssClass="form-control"
                                            Text='<%# Bind("days2") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txt_days2" runat="server" onkeydown="AllowOnlyNumeric1(event);" CssClass="form-control"
                                            BackColor="#FFFFCC" MaxLength="1"></asp:TextBox>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Code"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_shiftcode3" runat="server" Text='<%# Eval("shift_code3") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddl_editshiftcode3" runat="server" CssClass="form-control1"
                                            DataSourceID="SqlDataSource2" DataTextField="shift_code"
                                            DataValueField="shift_code">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddl_shiftcode3" runat="server" CssClass="form-control1">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Days"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_days3" runat="server" Text='<%# Eval("days3") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="lbl_editdays3" runat="server" CssClass="form-control"
                                            Text='<%# Bind("days3") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txt_days3" CssClass="form-control" onkeydown="AllowOnlyNumeric1(event);" runat="server" MaxLength="1"></asp:TextBox>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Code"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_shiftcode4" runat="server" Text='<%# Eval("shift_code4") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddl_editshiftcode4" runat="server" CssClass="form-control1"
                                            DataSourceID="SqlDataSource2" DataTextField="shift_code"
                                            DataValueField="shift_code">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddl_shiftcode4" runat="server" CssClass="form-control1">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Days"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_days4" runat="server" Text='<%# Eval("days4") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="lbl_editdays4" runat="server" CssClass="form-control"
                                            Text='<%# Bind("days4") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txt_days4" CssClass="form-control" onkeydown="AllowOnlyNumeric1(event);" runat="server" MaxLength="1"></asp:TextBox>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Code"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_shiftcode5" runat="server" Text='<%# Eval("shift_code5") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddl_editshiftcode5" runat="server" CssClass="form-control1"
                                            DataSourceID="SqlDataSource2" DataTextField="shift_code"
                                            DataValueField="shift_code">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddl_shiftcode5" runat="server" CssClass="form-control1">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Days"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_days5" runat="server" Text='<%# Eval("days5") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="lbl_editdays5" runat="server" CssClass="form-control"
                                            Text='<%# Bind("days5") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txt_days5" CssClass="form-control" onkeydown="AllowOnlyNumeric1(event);" runat="server" MaxLength="1"></asp:TextBox>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Code"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_shiftcode6" runat="server" Text='<%# Eval("shift_code6") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddl_editshiftcode6" runat="server" CssClass="form-control1"
                                            DataSourceID="SqlDataSource2" DataTextField="shift_code"
                                            DataValueField="shift_code" Width="55px">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddl_shiftcode6" runat="server" CssClass="form-control1">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Days"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_days6" runat="server" Text='<%# Eval("days6") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="lbl_editdays6" runat="server" CssClass="form-control"
                                            Text='<%# Bind("days6") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txt_days6" CssClass="form-control" runat="server" onkeydown="AllowOnlyNumeric1(event);" MaxLength="1"></asp:TextBox>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_shiftcode7" runat="server" Text='<%# Eval("shift_code7") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddl_editshiftcode7" runat="server" CssClass="form-control1"
                                            DataSourceID="SqlDataSource2" DataTextField="shift_code"
                                            DataValueField="shift_code">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddl_shiftcode7" runat="server" CssClass="form-control1">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Days"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_days7" runat="server" Text='<%# Eval("days7") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="lbl_editdays7" runat="server" CssClass="form-control"
                                            Text='<%# Bind("days7") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txt_days7" CssClass="form-control" runat="server" onkeydown="AllowOnlyNumeric1(event);" MaxLength="1"></asp:TextBox>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_shiftcode8" runat="server" Text='<%# Eval("shift_code8") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddl_editshiftcode8" runat="server"
                                            DataSourceID="SqlDataSource2" DataTextField="shift_code"
                                            DataValueField="shift_code" CssClass="form-control1">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddl_shiftcode8" runat="server" CssClass="form-control1">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Days">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_days8" runat="server" Text='<%# Eval("days8") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="lbl_editdays8" runat="server"
                                            Text='<%# Bind("days8") %>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txt_days8" CssClass="form-control" runat="server" onkeydown="AllowOnlyNumeric1(event);" MaxLength="1"></asp:TextBox>
                                    </FooterTemplate>
                                    <ControlStyle />
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                      <Itemtemplate>
                                                <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/edit_icon.png" />
                                                <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" ImageUrl="~/Images/delete_icon.jpg" />
                                            </Itemtemplate>
                                    </ItemTemplate>
                                      <EditItemTemplate>
                                                <asp:LinkButton ID="btnUpdate" runat="server" CommandName="Update" CssClass="btn btn-xs btn-success "><i class="glyphicon glyphicon-saved"></i> Update</asp:LinkButton>
                                                <%--<asp:ImageButton ID="btnUpdate" ImageUrl="~/Images/save_icon.jpg"  runat="server" CommandName="Update" AccessKey />--%>
                                            </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnk_Add" CommandName="add" runat="server" CssClass="btn btn-sm btn-success"><span class="glyphicon glyphicon-floppy-saved"></span> Save</asp:LinkButton>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <%-- <asp:CommandField ShowDeleteButton="True" ItemStyle-Width="25px" ButtonType="Image" DeleteImageUrl="~/Images/delete_icon.jpg" />--%>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <!-- /.panel-body -->
            </div>
            <div>
                <h3>&nbsp;</h3>
            </div>
            <div style="height: 50%; overflow: auto;">
                <table style="width: 100%" class="table table-striped table-bordered">

                    <tr>
                        <td width="30%">
                            <div style="height: 100%; overflow: auto;">
                                <table style="width: 100%;" class="table table-striped table-bordered table-hover">
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Calibri" Width="100%"
                                                ForeColor="Black" Text="Shift Selection"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style2">

                                            <asp:Label ID="Label1" runat="server" Width="100%"
                                                Text="Month/Year"></asp:Label>
                                        </td>
                                        <td>
                                             <div class="form-group">
                <div class='input-group bfh-timepicker'>
                   <asp:TextBox ID='txt_monyear' placeholder="MM/YYYY" runat="server" CssClass="form-control " Width="100%"></asp:TextBox>
                    <span class="input-group-addon">
                        <span class="glyphicon glyphicon-time"></span>
                    </span>
                </div>
            </div>
                                            <%--<asp:TextBox ID="txt_monyear" placeholder="MM/YYYY" runat="server" CssClass="form-control Month_Year" Width="100%"></asp:TextBox>--%>


                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style2">

                                            <asp:Label ID="Label2" runat="server" Width="100%"
                                                Text="Pattern Code"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <span><span>
                                                <asp:DropDownList ID="ddl_patterncode" runat="server" CssClass="form-control" Width="100%">
                                                </asp:DropDownList>
                                            </span></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style2">

                                            <asp:Label ID="Label3" runat="server" Width="100%"
                                                Text="Slot no"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <span><span>
                                                <asp:DropDownList ID="txt_slot" runat="server" CssClass="form-control" Width="100%" Enabled="False">
                                                    <asp:ListItem>1</asp:ListItem>
                                                    <asp:ListItem>2</asp:ListItem>
                                                    <asp:ListItem>3</asp:ListItem>
                                                    <asp:ListItem>4</asp:ListItem>
                                                    <asp:ListItem>5</asp:ListItem>
                                                </asp:DropDownList>
                                            </span></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style2">

                                            <asp:Label ID="Label4" runat="server" Width="100%"
                                                Text="Balance Days"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <span><span>
                                                <asp:TextBox ID="txt_balance" runat="server" CssClass="form-control" Width="100%"
                                                    MaxLength="2" AutoPostBack="True" OnTextChanged="txt_balance_TextChanged" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </span></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="auto-style1">
                                            <span>
                                                <asp:Button ID="btn_reset" runat="server" class="btn btn-warning" OnClick="btn_reset_Click" Text="Reset" />
                                            </span>
                                        </td>
                                        <td align="center">
                                            <span>
                                                <asp:Button ID="btn_save" runat="server" class="btn btn-success" OnClick="btn_save_Click" Text="Save" />
                                            </span>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:Button ID="btn_bulksave" runat="server" class="btn btn-success" OnClick="btn_bulksave_Click1" Text="Bulk Entry Save" />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="Calibri"
                                                ForeColor="Black" Text="Shift Change" Width="100%"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="auto-style2">
                                            <asp:Label ID="Label9" runat="server" Text="Date" Width="100%"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <div style="width: 100%; float: left;">
                                                <asp:TextBox ID="txt_sdate" runat="server" onkeyup="fn_date1(event,this.id);" onkeydown="AllowOnlyNumeric1(event);"
                                                    CssClass="form-control" placeholder="DD/MM/YYYY" MaxLength="10" Width="100%"></asp:TextBox>
                                            </div>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="auto-style2">
                                            <asp:Label ID="Label10" runat="server" Text="Shift Code" Width="100%"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <span>
                                                <asp:DropDownList ID="ddl_shift" runat="server" CssClass="form-control" Width="100%">
                                                </asp:DropDownList>

                                                <%--   <asp:DropDownList ID="ddl_shift" AppendDataBoundItems="true" runat="server" CssClass="form-control" DataTextField="shift_code" DataValueField="shift_code"
                                Width="100%" DataSourceID="SqlDataSource2">
                                <asp:ListItem Text="Select"></asp:ListItem>
                            </asp:DropDownList>--%>
                                                <%--            <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:connectionstring %>" 
                                        ProviderName="<%$ ConnectionStrings:connectionstring.ProviderName %>" 
                                        SelectCommand="SELECT [shift_code] FROM [shift_details] WHERE (([pn_companyid] = @pn_companyid) AND ([pn_branchid] = @pn_branchid))">
                                        <SelectParameters>
                                            <asp:SessionParameter Name="pn_companyid" SessionField="Login_temp_companyID" 
                                                Type="Int32" />
                                            <asp:SessionParameter Name="pn_branchid" SessionField="Login_temp_BranchID" 
                                                Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>--%>
                                            </span>
                                        </td>
                                    </tr>
                                    <%-- <tr>
                        <td align="center">
                            <asp:Label ID="Label11" runat="server" Text="Apply to students" Width="100%"></asp:Label>
                        </td>
                        <td align="center">
                            <div style=" width:100%; float:left;">
                                <asp:CheckBox ID="chk_student" runat="server" />
                            </div>

                        </td>
                    </tr>--%>
                                    <tr>
                                        <td align="center" class="auto-style2">
                                            <span>
                                                <asp:Button ID="btn_update" runat="server" class="btn btn-success"
                                                    OnClick="btn_update_Click" Text="Update" />
                                            </span></td>
                                        <td align="center">
                                            <span>
                                                <asp:Button ID="btn_rset" runat="server" class="btn btn-warning" Text="Reset"
                                                    OnClick="btn_rset_Click" />
                                            </span></td>
                                    </tr>

                                </table>
                            </div>
                        </td>
                        <td width="35%">
                            <div style="height: 100%; overflow: auto;">
                                <table style="width: 100%;" class="table table-striped table-bordered">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label5" runat="server" Font-Bold="True"
                                                Text="Department Selection"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="overflow: auto;">
                                            <span style="color: #800000">
                                                <asp:DropDownList ID="ddl_department" runat="server" class="form-control"
                                                    OnSelectedIndexChanged="ddl_department_SelectedIndexChanged" Width="90%"
                                                    AutoPostBack="True">
                                                </asp:DropDownList>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="overflow: auto; height: 39px;">
                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True"
                                                ForeColor="Black"
                                                OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem>All</asp:ListItem>
                                                <asp:ListItem Selected="True">Selected</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div id="diva" runat="server" style="overflow: auto; height: 350px;">
                                                <asp:CheckBoxList ID="CheckBoxList1" runat="server" CssClass="test" DataSourceID="SqlDataSource1" DataTextField="Employee_First_Name"
                                                    DataValueField="EmployeeCode" AutoPostBack="True" OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged" BorderStyle="Solid">
                                                </asp:CheckBoxList>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                        <td width="35%">
                            <div style="height: 100%; overflow: auto;">
                                <table style="width: 100%;" class="table table-striped table-bordered">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label7" runat="server" Font-Bold="True"
                                                Text="Employee Selection"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                      
                                        <td align="center">
                                          
                                            <asp:DropDownList ID="ddl_empcode" placeholder="Select Employee" runat="server" AutoPostBack="True" CssClass="form-control"
                                                OnSelectedIndexChanged="ddl_empcode_SelectedIndexChanged" Width="100%">
                                                <asp:ListItem Text="Select"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Calendar ID="Calendar1" runat="server" CssClass="table table-hover table-striped table-responsive"
                                                OnDayRender="Calendar1_DayRender" ShowGridLines="true" Width="100%" OnVisibleMonthChanged="Monthchange"></asp:Calendar>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>



            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <br />
</asp:Content>







