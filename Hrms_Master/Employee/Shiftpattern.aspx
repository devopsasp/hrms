<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="Shiftpattern.aspx.cs" Inherits="Hrms_Master_Default"
    Title="Welcome to HRMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<script type="text/javascript" language="javascript" src="../../Scripts/Datavalid.js"></script>

    <script language="javascript" type="text/javascript">
        function disableKeyPress(e) {
            var key;
            if (window.event)
                key = window.event.keyCode;
            else
                key = e.which;
            return (key != 13);
        }
    
    function show_message()
    {
        alert("Shift Name Already Exist");
    }
    
    function show_Error()
    {
        alert("Enter Shift Name");
    }
    
    function fnSave()
    {   
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$ShiftName.value == "" || document.aspnetForm.ctl00$ContentPlaceHolder1$ShiftFrom.value == "" || document.aspnetForm.ctl00$ContentPlaceHolder1$ShiftTo.value == "")
        {
            alert("Make sure all the details are Entered");
            aspnetForm.ctl00$ContentPlaceHolder1$ShiftName.focus();
            return false;
        }                        
        else
        { 
              return true;  
        }
    }    
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
                <div class="col-lg-12">
                    <h2 class="page-header">Attendance Bonus </h2>
                </div>
                <!-- /.col-lg-12 -->
            </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div style="width: 40%">
                    <table cellpadding="1%" cellspacing="1%" width="100%" class="table table-striped table-bordered table-hover">
                        <tr>
                            <td>
                                Category Name</td>
                            <td>
                               <asp:DropDownList ID="ddl_category" runat="server" CssClass="form-control" 
                                    AutoPostBack="True" onselectedindexchanged="ddl_category_SelectedIndexChanged">
                    </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Full Attendance</td>
                            <td>
                                <asp:TextBox ID="txtFull" runat="server" CssClass="form-control">0.00</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Half day Absent/Leave</td>
                            <td >
                                <input type="text" runat="server" id="txtHalf" class="form-control" value="0.00" />
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 48px">
                                One day Absent/Leave</td>
                            <td style="height: 48px">
                                <input type="text" runat="server" id="txtOne" class="form-control" value="0.00" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2">
                              <div align="right">
                                <asp:Button ID="btn_save" runat="server" class="btn btn-success"  Text="Save" onclick="btn_save_Click" /></div>
                            </td>
                        </tr>
                     </table>   
                     </div>
                     </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Content>
