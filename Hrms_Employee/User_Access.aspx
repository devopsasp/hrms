<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="User_Access.aspx.cs" Inherits="Hrms_Employee_User_role" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<html xmlns="http://www.w3.org/1999/xhtml">

<body class="body" onload="lode()"; style="overflow-x: hidden">
   
        <div class="row">
       
            <div class="col-lg-12">
                <ol class="breadcrumb">
                    <li class="active">
                        <h1>Role Access</h1>
                    </li>
                </ol>
            </div>
            <div class="col-lg-12 line">
            </div>
            <div class="col-lg-12">
                <br />
                <div class="col-lg-3">
                </div>
                <div class="col-lg-1">
                    <label for="sltRole">Role <span class="Mandatory">*</span>:</label>
                </div>
                <div class="col-lg-5">
                    <select id="sltRole" class="form-control sltRole" onchange="RoleAccessRetrieval('sltRole');" >
                        <option value="">-----Select-----</option>
                    </select>
                </div>
                <div class="col-lg-3">
                </div>
            </div>
            <div class="col-lg-12">
                <br />
                <div class="col-lg-3">
                </div>
                <div class="col-lg-6">
                    <table id="tblRoleAccess" style="word-break: break-all;"></table>
                </div>
                <div class="col-lg-3">
                </div>
            </div>
            <div id="Login_dialog" class="Fields" style="display: none;"></div>
            <div id="dialog_new" class="Fields" style="display: none;">
            </div>
            <div class="col-lg-12">
                <br />
                <div class="col-lg-4">
                    <input id="hdnRoleId" type="hidden" runat="server" value="0" />
                </div>
                <div class="col-lg-4" align="center">
                    <button id="btnSave" type="button" class="btn btn-primary" onclick="SaveRoleAccess(); return false;" <%--style="visibility: hidden"--%>>Save</button>
                    <button id="btnClear" type="button" class="btn btn-warning" onclick="ConfirmClear(); return false;">Clear</button>
                </div>
            </div>
            <div class="col-lg-12">
                <div id="divLoading" style="display: none; margin-bottom: 2px;" class="loading-bar">
                    <button class="btn btn-warning btn-block disabled">
                        <span class="glyphicon glyphicon-refresh spinning"></span>&nbsp;Please wait...
                    </button>
                </div>
            </div>

        </div>
   
</body>
<script src="../js/RoleAccess.js"></script>

</html>


</asp:Content>
