<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Transport_Allocation.aspx.cs" MasterPageFile="~/HRMS.master" Inherits="Hrms_Operations_Transport_Allocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>
<script language="javascript" type="text/javascript">

    function check_del() {
        if (document.aspnetForm.ctl00$ContentPlaceHolder1$txt_reason.value == "") {
            alert("Complete all the Details before Deleting");
            aspnetForm.ctl00$ContentPlaceHolder1$txt_reason.focus();
            return false;
        }
        else {
            return true;
        }
    }
    </script>
<table width="100%"><tr><td class="tdComposeHeader">
    <table runat="server" id="tab_combination" style="width: 100%; height: 70%">
        <tr>
            <td class="TextStyle" height="35" colspan="3" width="30%">
                <span class="Title">&nbsp;
                    Transport Allocation</span></td>
        </tr>
        <tr>
            <td colspan="3" width="30%" align="center" class="TextStyle">
                <asp:Label ID="lbl_Error" runat="server" CssClass="TextStyle" ForeColor="Red" Font-Size="Small"
                    Width="65%"></asp:Label></td>
        </tr>
        <tr id="row_vehicle" runat="server">
            <td width="30%" align="right" class="TextStyle">
                Select Vehicle </td>
            <td width="40%" align="center">
                <asp:DropDownList ID="ddl_vehicle" runat="server" CssClass="form-control" >
                  
                   
                </asp:DropDownList></td>
            <td style="width: 100px">
            </td>
        </tr>
        <tr id="row_fromoption" runat="server">
            <td align="right" width="30%" class="TextStyle">
                Select Destination</td>
            <td width="40%" align="center">
                <asp:DropDownList ID="ddl_Destination" runat="server" CssClass="form-control"> 
                    
                </asp:DropDownList></td>
            <td style="width: 100px">
            </td>
        </tr>
        <tr id="row_fromfilter"  runat="server">
            <td align="right" width="30%" class="TextStyle">
                Select Driver</td>
            <td width="40%" align="center" valign="middle">
                <asp:DropDownList ID="ddl_Driver" runat="server" CssClass="form-control">
                </asp:DropDownList></td>
            <td style="width: 100px">
            </td>
        </tr>
        <tr id="route" runat="server">
            <td align="right" valign="middle" width="30%" style="height: 61px" class="TextStyle">
                Bus Route and Timings</td>
            <td width="40%" align="center" style="height: 61px">
                <textarea id="txt_route" runat="server" cols="20" name="S1" rows="3" style="width:350px" CssClass="form-control"></textarea></td>
            <td style="width: 100px; height: 61px;">
            </td>
        </tr>
        <tr>
            <td align="center" colspan="3" valign="bottom" class="TextStyle">
                <asp:ImageButton ID="btn_back" runat="server" ImageUrl="~/Images/back.jpg" ImageAlign="AbsMiddle" />&nbsp;
                <asp:ImageButton ID="btn_transfer" runat="server" ImageUrl="~/Images/save.jpg" 
                  ImageAlign="AbsMiddle" 
                    onclick="btn_transfer_Click" />
                
            </td>
        </tr>
        <tr><td>
        </td><td>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Error" runat="server"></asp:Label></td></tr>
        </table>
    <table runat="server" id="tab_transfering" width="100%">
        <tr>
            <td align="center" valign="middle">
              <%--  <asp:ImageButton ID="btn_yes" ImageUrl="~/Images/Yes.jpg" runat="server" title="Click to Transfer" OnClick="btn_yes_Click" ImageAlign="AbsMiddle" />&nbsp;
                <asp:ImageButton ID="btn_no" ImageUrl="~/Images/No.jpg" runat="server" title="Click to Cancel Transfer" OnClick="btn_no_Click" ImageAlign="AbsMiddle" /></td>--%>
                         <asp:GridView ID="Grid_bus_allocation"  
            AutoGenerateColumns="False" class="table table-striped table-bordered table-hover"
            style="Z-INDEX: 101; LEFT: 20px; TOP: 32px" 
            ShowFooter="True" Font-Size="X-Small"
            Font-Names="Verdana" runat="server" 
           
                    Width="600px" CellPadding="4" ForeColor="#333333" GridLines="None" 
                    onrowcancelingedit="Grid_bus_allocation_RowCancelingEdit" 
                    onrowediting="Grid_bus_allocation_RowEditing" onrowupdating="Grid_bus_allocation_RowUpdating1" 
                   >
                               
            <RowStyle  />
                    <PagerStyle  HorizontalAlign="Center" />
                    <SelectedRowStyle  Font-Bold="True"  />
            <HeaderStyle  HorizontalAlign="Left" 
                        Font-Bold="True"/>
            <FooterStyle Font-Bold="True"  />
            <Columns>
                <asp:TemplateField HeaderText="Route_ID" Visible="false">
           <ItemTemplate>
                        <asp:Label ID="lbl_route_id" Text='<%# Eval("Route_ID") %>' runat="server"></asp:Label>
           </ItemTemplate>
            
           </asp:TemplateField>
            <asp:TemplateField HeaderText="Vehicle ID" Visible="false">
           <ItemTemplate>
                        <asp:Label ID="lbl_veh_id" Text='<%# Eval("Veh_id") %>' runat="server"></asp:Label>
           </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Vehicle Number">
           <ItemTemplate>
                        <asp:Label ID="lbl_veh_no" Text='<%# Eval("Veh_number") %>' runat="server"></asp:Label>
           </ItemTemplate>
           <EditItemTemplate>
   <asp:DropDownList ID="ddl_edit_veh_no" runat="server"></asp:DropDownList>
           </EditItemTemplate>
           </asp:TemplateField>
                <asp:TemplateField HeaderText="Destination Area">
           <ItemTemplate>
                        <asp:Label ID="lbl_destination" Text='<%# Eval("Area_name") %>' runat="server"></asp:Label>
           </ItemTemplate>
            <EditItemTemplate>
              <asp:DropDownList ID="ddl_edit_destination" runat="server">
               </asp:DropDownList>
           </EditItemTemplate>
           </asp:TemplateField>
            <asp:TemplateField HeaderText="Driver">
           <ItemTemplate>
                        <asp:Label ID="lbl_driver" Text='<%# Eval("EmployeeCode") %>' runat="server"></asp:Label>
           </ItemTemplate>
           <EditItemTemplate>
               <asp:DropDownList ID="ddl_edit_drivers" runat="server">
               </asp:DropDownList>
           </EditItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Route and Timing">
           <ItemTemplate>
                        <asp:Label ID="lbl_route" Text='<%# Eval("Routes") %>' runat="server"></asp:Label>
           </ItemTemplate>
            <EditItemTemplate>
               <asp:TextBox ID="txt_route" Text='<%# Bind("Routes") %>' TextMode="MultiLine" runat="server"></asp:TextBox>
           </EditItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField>
           <ItemTemplate>
           <asp:LinkButton ID="lnk_incharge" runat="server" Text="Select Bus Incharge" 
                   ForeColor="Black" onclick="lnk_incharge_Click"></asp:LinkButton>
           </ItemTemplate>
           </asp:TemplateField>
           <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" ShowCancelButton="true" />
            </Columns>
              <EditRowStyle  />
                    <AlternatingRowStyle /></asp:GridView>

                    </td>
        </tr>
    </table>
    <table runat="server" id="Table1" width="100%">
        <tr>
            <td align="center" valign="middle">
              <%--  <asp:ImageButton ID="btn_yes" ImageUrl="~/Images/Yes.jpg" runat="server" title="Click to Transfer" OnClick="btn_yes_Click" ImageAlign="AbsMiddle" />&nbsp;
                <asp:ImageButton ID="btn_no" ImageUrl="~/Images/No.jpg" runat="server" title="Click to Cancel Transfer" OnClick="btn_no_Click" ImageAlign="AbsMiddle" /></td>--%>
                         <asp:GridView ID="Grid_incharge"  
            AutoGenerateColumns="False" 
            style="Z-INDEX: 101; LEFT: 20px; TOP: 32px" class="table table-striped table-bordered table-hover"
            ShowFooter="True" Font-Size="X-Small"
            Font-Names="Verdana" runat="server" 
           
                    Width="600px" CellPadding="4" ForeColor="#333333" GridLines="None" 
                   onrowdatabound="GridView1_RowDataBound" onrowediting="GridView1_RowEditing" onrowupdating="Grid_incharge_RowUpdating" 
                   >
                               
            <RowStyle  />
                    <PagerStyle HorizontalAlign="Center" />
                    <SelectedRowStyle  Font-Bold="True" />
            <HeaderStyle  HorizontalAlign="Left" 
                        Font-Bold="True"/>
            <FooterStyle Font-Bold="True" />
            <Columns>
                <asp:TemplateField HeaderText="Route_ID" Visible="false">
           <ItemTemplate>
                        <asp:Label ID="lbl_route_id" Text='<%# Eval("Route_ID") %>' runat="server"></asp:Label>
           </ItemTemplate>
            
           </asp:TemplateField>
            <asp:TemplateField HeaderText="Vehicle ID" Visible="false">
           <ItemTemplate>
                        <asp:Label ID="lbl_veh_id" Text='<%# Eval("Veh_id") %>' runat="server"></asp:Label>
           </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Vehicle Number">
           <ItemTemplate>
                        <asp:Label ID="lbl_veh_no" Text='<%# Eval("Veh_number") %>' runat="server"></asp:Label>
           </ItemTemplate>
          
           </asp:TemplateField>
                <asp:TemplateField HeaderText="Destination Area">
           <ItemTemplate>
                        <asp:Label ID="lbl_destination" Text='<%# Eval("Area_name") %>' runat="server"></asp:Label>
           </ItemTemplate>
           
           </asp:TemplateField>
            <asp:TemplateField HeaderText="Driver">
           <ItemTemplate>
                        <asp:Label ID="lbl_driver" Text='<%# Eval("EmployeeCode") %>' runat="server"></asp:Label>
           </ItemTemplate>
           
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Route and Timing">
           <ItemTemplate>
                        <asp:Label ID="lbl_route" Text='<%# Eval("Routes") %>' runat="server"></asp:Label>
           </ItemTemplate>
          
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Bus Incharge">
           <ItemTemplate>
                        <asp:Label ID="lblfirstname" Text='<%# Eval("FirstName") %>' runat="server"></asp:Label>
           </ItemTemplate>
          <EditItemTemplate>
              <asp:DropDownList ID="DropDownList1" runat="server" DataValueField="FirstName">
              <asp:ListItem>Select</asp:ListItem>
              </asp:DropDownList>
          </EditItemTemplate>
           </asp:TemplateField>
           <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" ShowCancelButton="true" />
            </Columns>
              <EditRowStyle  />
                    <AlternatingRowStyle  /></asp:GridView>

                    </td>
        </tr>
    </table></td></tr></table>
</asp:Content>