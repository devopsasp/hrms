<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="EmployeeGeneral.aspx.cs" Inherits="PayrollReports_Default" Title="Payslip Reports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript" src="../js/jquery-1.9.1.min.js"></script>
    <%--http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js--%>
    <script type="text/javascript">
        $(function () {
            $("[id*=chkall]").bind("click", function () {
                if ($(this).is(":checked")) {
                    $("[id*=chk_Empcode] input").prop("checked", "checked");
                } else {
                    $("[id*=chk_Empcode] input").removeAttr("checked");
                }
            });
            $("[id*=chk_Empcode] input").bind("click", function () {
                if ($("[id*=chk_Empcode] input:checked").length == $("[id*=chk_Empcode] input").length) {
                    $("[id*=chkall]").prop("checked", "checked");
                } else {
                    $("[id*=chkall]").removeAttr("checked");
                }
            });
        });
    </script>

    <style>
        .checkbox1 input[type="checkbox"] {
            margin-right: 5px;
        }
    </style>


    <script language="javascript" type="text/javascript">
        function refresh_Click() {
            window.open("PayDB_Refresh.aspx", "popwindow", "toolbar=no,width=300,height=120,top=320,left=370,status=1");
        }
        function fn_chkall(chkid, chklistid) {

            var chkBoxList = document.getElementById(chklistid);
            var chkBoxCount = chkBoxList.getElementsByTagName("input");

            if (document.getElementById(chkid).checked == true) {
                for (var i = 0; i < chkBoxCount.length; i++) {
                    chkBoxCount[i].checked = true;
                }
            }
            else {
                for (var i = 0; i < chkBoxCount.length; i++) {
                    chkBoxCount[i].checked = false;
                }
            }
        }
    </script>
    <link href="../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div>
        <h3 class="page-header">Employee Report Generation</h3>
    </div>
    <div align="right">
        <asp:DropDownList ID="ddl_Branch" runat="server" CssClass="form-control"
            OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged">
        </asp:DropDownList>
        &nbsp;
    </div>

    <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                            <div class="divWaiting">
                            
                            <asp:Image ID="imgWait" runat="server" ImageAlign="Middle" 
                                    ImageUrl="~/Images/loading2.gif" Height="100px" Width="100px" />

                            </div>
                            </ProgressTemplate>
                            </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
    <div>

        <table align="left" class="table table-striped table-bordered table-hover"
            style="width: 50%">
            <tr>
                <td>
                    <asp:Label ID="lbl_error" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="Medium" ForeColor="Red" Width="75%"></asp:Label>
                </td>
            </tr>
          
            <tr>
                <td>
                    <span style="color: #800000">
                        <asp:DropDownList ID="ddl_department" runat="server" AutoPostBack="True" class="form-control" OnSelectedIndexChanged="ddl_department_SelectedIndexChanged">
                        </asp:DropDownList>
                    </span></td>
            </tr>
              <%-- <tr>
                <td>
                    <span style="color: #800000">
                        <asp:DropDownList ID="ddl_category"  runat="server" AutoPostBack="True" class="form-control" OnSelectedIndexChanged="ddl_department_SelectedIndexChanged">
                        <asp:ListItem>Select Category</asp:ListItem>
                        <asp:ListItem>Employee Basic Details</asp:ListItem>
                        <asp:ListItem>Designation</asp:ListItem>
                        <asp:ListItem>Division</asp:ListItem>
                        <asp:ListItem>Category</asp:ListItem>
                        </asp:DropDownList>
                    </span></td>
            </tr>--%>
            <tr>
                <td>
                    <div id="diva" runat="server" class="checkbox1" align="left" style="overflow: auto; height: 300px;">
                        &nbsp;&nbsp;
                                                         <asp:CheckBoxList ID="chk_Empcode" runat="server" CssClass="form-control" Width="90%">
                                                             <asp:ListItem></asp:ListItem>
                                                         </asp:CheckBoxList>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="chkall" runat="server" Text="Select All" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btn_Report" runat="server" class="btn btn-success" OnClick="btn_Report_Click" Text="View Report" />
                    <br />
                    <br />
                </td>

            </tr>
             <tr>
                                                 <td>
                                                     <asp:Button ID="Img1" runat="server" class="btn btn-info" OnClientClick="refresh_Click()" Text="Refresh" />
                                                     Click, If you modified any details.</td>
                                               
                                             </tr>
        </table>



    </div>
    <%--    </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
