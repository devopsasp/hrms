<%@ Page Title="" Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" 
CodeFile="EditTransportDetails.aspx.cs" Inherits="Hrms_Employee_EditTransportDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="panel-heading">
    <h4 class="panel-title">
   Update Transport Details
    </h4><br />
    <table id="Table1" cellspacing="0" cellpadding="0" runat="server" border="0" width="100%" class="table table-striped table-bordered table-hover">

    <tr>
                        <td> Area Name</td>
                        <td>
                            <input class="form-control" runat="server" id="Txt_area"/></td>
                                
                        <td>Bus Number</td>
                        <td> 
                            <input class="form-control" runat="server" id="txt_vehicle"/></td>
                                
                        <td >Vehicle Number</td>
                        <td >
                            <input class="form-control" runat="server" id="Txt_veh_number" /></td>                          
                    </tr>
                    <tr>
                    <td>Boarding Point</td>
                          <td> <input class="form-control" runat="server" id="Txt_point" /></td>

                     <td>Driver Name</td>     
                          <td> <input class="form-control" runat="server" id="Txt_driver" /></td>
                    </tr>

                    <tr>
                    <td colspan="2">                   
                       <asp:Button ID="btn_Back" runat="server" OnClick="btn_Back_Click" Text="Back" class="btn btn-info"  />
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" class="btn btn-success" onclick="btnUpdate_Click" 
                           /></td>
                    </tr>
      </table>
               </div>      

</asp:Content>

