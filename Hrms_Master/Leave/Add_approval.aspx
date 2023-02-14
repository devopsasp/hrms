<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="Add_approval.aspx.cs" Inherits="Hrms_Master_Leave_Add_approval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

      <div><h3 class="page-header">Add Approval </h3></div>
    <div class="row">
        <div class="col-md-12">
            <div class="col-sm-2"></div>
            <div class="col-sm-2">Form Name</div>
            <div class="col-sm-4">
                <select class="form-control" id="Form_name">
                    <option value="0">Select Form</option>
                    <option value="1">Leave</option>
                    <option value="2">Attendance & Permission </option>
                    <option value="3">Travel Expenses</option>                 
                    <option value="4">asset allocation</option>
                </select>
            </div>
                        <div class="col-sm-4"></div>
        </div>
         <div class="col-md-12">
       <br/>
        </div>
        <div class="col-md-12">
                        <div class="col-sm-2"></div>
            <div class="col-sm-2">Approvals Hierarchy</div>
            <div class="col-sm-4">
                <select class="form-control" id="Hierarchy">
                    <option value="0">Select Level</option>
                    <option value="1">Single Level</option>         
                    <option value="2">Two	 Levels</option>
                    <option value="3">Three  Levels</option>
                    <option value="4">Four   Levels</option>
                    <option value="5">Five   Levels</option>
                    <option value="6">Six    Levels</option>
                    <option value="7">Seven  Levels</option>
                    <option value="8">Eight  Levels</option>
                    <option value="9">Nine   Levels</option>
                    <option value="10">Ten   Levels</option>
                </select>

            </div>
                        <div class="col-sm-4"></div>
        </div>
           <div class="col-md-12">
       <br/>
        </div>
        <div class="col-md-12">
            <div class="col-sm-2"></div>
            <div class="col-sm-2">Down Time
            </div>
            <div class="col-sm-4">
            <div class="form-group">
                <div class='input-group bfh-timepicker' id='datetimepicker1'>
                    <input type="text" id="Timepic" placeholder="00:00" class="form-control" />
                    <span class="input-group-addon">
                        <span class="glyphicon glyphicon-time"></span>
                    </span>
                </div>
            </div>
        </div>
                 <div class="col-sm-4"></div>
        </div>
        <div class="col-md-12">
 <br />
        </div>
        <div class="col-md-12">
            <div class="col-sm-4"></div>
            <div class="col-sm-4">
                <center>
<input type="button" id="btnsave" class="btn btn-success" onclick="save(); return false;" value="Save" />
<input type="button" id="btnclear" class="btn btn-default" onclick="clear();retun false;" onreset="" value="Clear"/>
                    </center>
            </div>
            <div class="col-sm-4"></div>
        </div>
    </div>
    <script src="../../Scripts/Add_approval.js"></script>
</asp:Content>
