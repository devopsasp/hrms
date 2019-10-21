<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="payinput.aspx.cs" Inherits="Hrms_PayRoll_Default" Title="Payroll Input" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>
<script type="text/javascript">
    window.onload = function () {
        var seconds = 5;
        setTimeout(function () {
            document.getElementById("<%=lbl_error.ClientID %>").innerHTML = "";
        }, seconds * 1000);
    };
</script>
    <script type="text/javascript" language="JavaScript">
        function add_paidday(rowindex) {
            try {
                var zero = 0;
                var ch = document.getElementById("ctl00_ContentPlaceHolder1_hdtxt_monthcalc").value;
                var grid_id = document.getElementById('<%=grid_input.ClientID %>');
                var isIE = navigator.appName;
                if (isIE == "Microsoft Internet Explorer" || "Netscape") {
                    var cell_calc = grid_id.rows[parseInt(rowindex) + 1].cells[1];
                    var val_calc = cell_calc.childNodes[0];

                    var cell_paid = grid_id.rows[parseInt(rowindex) + 1].cells[2];
                    var val_paid = cell_paid.childNodes[0];

                    var cell_present = grid_id.rows[parseInt(rowindex) + 1].cells[3];
                    var val_present = cell_present.childNodes[0];

                    var cell_leave = grid_id.rows[parseInt(rowindex) + 1].cells[5];
                    var val_leave = cell_leave.childNodes[0];

                    var cell_weakof = grid_id.rows[parseInt(rowindex) + 1].cells[6];
                    var val_weakof = cell_weakof.childNodes[0];

                    var cell_onduty = grid_id.rows[parseInt(rowindex) + 1].cells[8];
                    var val_onduty = cell_onduty.childNodes[0];

                    var cell_tour = grid_id.rows[parseInt(rowindex) + 1].cells[10];
                    var val_tour = cell_tour.childNodes[0];

                    var cell_comp = grid_id.rows[parseInt(rowindex) + 1].cells[9];
                    var val_comp = cell_comp.childNodes[0];

                    var cell_abs = grid_id.rows[parseInt(rowindex) + 1].cells[4];
                    var val_abs = cell_abs.childNodes[0];

                    var cell_holi = grid_id.rows[parseInt(rowindex) + 1].cells[7];
                    var val_holi = cell_holi.childNodes[0];

                    if (ch == 'D') {
                        val_paid.value = parseInt(val_present.value) + parseInt(val_leave.value) + parseInt(val_weakof.value) + parseInt(val_onduty.value) + parseInt(val_tour.value) + parseInt(val_comp.value);
                        if (parseInt(val_paid.value) > parseInt(val_calc.value)) {
                            alert("You Crossed the total calculation days of the month");
                            val_paid.value = parseInt(0);
                            val_present.value = parseInt(0);
                            val_leave.value = parseInt(0);
                            val_onduty.value = parseInt(0);
                            val_tour.value = parseInt(0);
                            val_comp.value = parseInt(0);

                            val_paid.value = parseInt(val_present.value) + parseInt(val_leave.value) + parseInt(val_weakof.value) + parseInt(val_onduty.value) + parseInt(val_tour.value) + parseInt(val_comp.value);
                        }
                        else {
                            val_abs.value = parseInt(val_calc.value) - parseInt(val_paid.value);
                        }
                    }
                    else if (ch == 'M') {
                        val_paid.value = parseInt(val_present.value) + parseInt(val_leave.value) + parseInt(val_weakof.value) + parseInt(val_onduty.value) + parseInt(val_tour.value) + parseInt(val_comp.value) + parseInt(val_holi.value);
                        if (parseInt(val_paid.value) > parseInt(val_calc.value)) {
                            alert("You Cross the total calculation days of the month");
                            val_paid.value = parseInt(0);
                            val_present.value = parseInt(0);
                            val_leave.value = parseInt(0);
                            val_onduty.value = parseInt(0);
                            val_tour.value = parseInt(0);
                            val_comp.value = parseInt(0);
                            val_paid.value = parseInt(val_present.value) + parseInt(val_leave.value) + parseInt(val_weakof.value) + parseInt(val_onduty.value) + parseInt(val_tour.value) + parseInt(val_comp.value);
                        }
                        else {
                            val_abs.value = parseInt(val_calc.value) - parseInt(val_paid.value);
                        }
                    }
                    else if (ch == 'W') {
                        val_paid.value = parseInt(val_present.value) + parseInt(val_leave.value) + parseInt(val_onduty.value) + parseInt(val_tour.value) + parseInt(val_comp.value);
                        if (parseInt(val_paid.value) > parseInt(val_calc.value)) {
                            alert("You Cross the total calculation days of the month");
                            val_paid.value = parseInt(0);
                            val_present.value = parseInt(0);
                            val_leave.value = parseInt(0);
                            val_onduty.value = parseInt(0);
                            val_tour.value = parseInt(0);
                            val_comp.value = parseInt(0);
                            val_paid.value = parseInt(val_present.value) + parseInt(val_leave.value) + parseInt(val_onduty.value) + parseInt(val_tour.value) + parseInt(val_comp.value);
                        }
                        else {
                            var sd = val_calc.value;
                            val_abs.value = parseInt(val_calc.value) - parseInt(val_paid.value);
                        }
                    }
                }
                else {
                    var cell_calc = grid_id.rows[parseInt(rowindex) + 1].cells[1];
                    var val_calc = cell_calc.childNodes[1];

                    var cell_paid = grid_id.rows[parseInt(rowindex) + 1].cells[2];
                    var val_paid = cell_paid.childNodes[1];

                    var cell_present = grid_id.rows[parseInt(rowindex) + 1].cells[3];
                    var val_present = cell_present.childNodes[1];

                    var cell_leave = grid_id.rows[parseInt(rowindex) + 1].cells[5];
                    var val_leave = cell_leave.childNodes[1];

                    var cell_weakof = grid_id.rows[parseInt(rowindex) + 1].cells[6];
                    var val_weakof = cell_weakof.childNodes[1];

                    var cell_onduty = grid_id.rows[parseInt(rowindex) + 1].cells[8];
                    var val_onduty = cell_onduty.childNodes[1];

                    var cell_tour = grid_id.rows[parseInt(rowindex) + 1].cells[10];
                    var val_tour = cell_tour.childNodes[1];

                    var cell_comp = grid_id.rows[parseInt(rowindex) + 1].cells[9];
                    var val_comp = cell_comp.childNodes[1];

                    var cell_abs = grid_id.rows[parseInt(rowindex) + 1].cells[4];
                    var val_abs = cell_abs.childNodes[1];

                    var cell_holi = grid_id.rows[parseInt(rowindex) + 1].cells[7];
                    var val_holi = cell_holi.childNodes[1];

                    if (ch == 'D') {
                        val_paid.value = parseInt(val_present.value) + parseInt(val_leave.value) + parseInt(val_weakof.value) + parseInt(val_onduty.value) + parseInt(val_tour.value) + parseInt(val_comp.value);
                        if (parseInt(val_paid.value) > parseInt(val_calc.value)) {
                            alert("You Crossed the total calculation days of the month");
                            val_paid.value = parseInt(0);
                            val_present.value = parseInt(0);
                            val_leave.value = parseInt(0);
                            val_onduty.value = parseInt(0);
                            val_tour.value = parseInt(0);
                            val_comp.value = parseInt(0);
                            val_paid.value = parseInt(val_present.value) + parseInt(val_leave.value) + parseInt(val_weakof.value) + parseInt(val_onduty.value) + parseInt(val_tour.value) + parseInt(val_comp.value);
                        }
                        else {
                            val_abs.value = parseInt(val_calc.value) - parseInt(val_paid.value);
                        }
                    }
                    else if (ch == 'M') {
                        val_paid.value = parseInt(val_present.value) + parseInt(val_leave.value) + parseInt(val_weakof.value) + parseInt(val_onduty.value) + parseInt(val_tour.value) + parseInt(val_comp.value) + parseInt(val_holi.value);
                        if (parseInt(val_paid.value) > parseInt(val_calc.value)) {
                            alert("You Cross the total calculation days of the month");
                            val_paid.value = parseInt(0);
                            val_present.value = parseInt(0);
                            val_leave.value = parseInt(0);
                            val_onduty.value = parseInt(0);
                            val_tour.value = parseInt(0);
                            val_comp.value = parseInt(0);
                            val_paid.value = parseInt(val_present.value) + parseInt(val_leave.value) + parseInt(val_weakof.value) + parseInt(val_onduty.value) + parseInt(val_tour.value) + parseInt(val_comp.value);
                        }
                        else {
                            val_abs.value = parseInt(val_calc.value) - parseInt(val_paid.value);
                        }
                    }
                    else if (ch == 'W') {
                        val_paid.value = parseInt(val_present.value) + parseInt(val_leave.value) + parseInt(val_onduty.value) + parseInt(val_tour.value) + parseInt(val_comp.value);
                        if (parseInt(val_paid.value) > parseInt(val_calc.value)) {
                            alert("You Cross the total calculation days of the month");
                            val_paid.value = parseInt(0);
                            val_present.value = parseInt(0);
                            val_leave.value = parseInt(0);
                            val_onduty.value = parseInt(0);
                            val_tour.value = parseInt(0);
                            val_comp.value = parseInt(0);
                            val_paid.value = parseInt(val_present.value) + parseInt(val_leave.value) + parseInt(val_onduty.value) + parseInt(val_tour.value) + parseInt(val_comp.value);
                        }
                        else {
                            var sd = val_calc.value;
                            val_abs.value = parseInt(val_calc.value) - parseInt(val_paid.value);
                        }
                    }
                }
            }
            catch (err) {
                alert(err);
            }
        }
   
   function fn_date(event,txtid)
     {  
       var len;
       var txtvalue; 
       var bool_obj; 
       var i;    
          
       txtvalue= document.getElementById(txtid).value;
       txtlen=txtvalue.length;  
       
        if(event.keyCode!=8 && event.keyCode!=46 && event.keyCode!=35 && event.keyCode!=36 && event.keyCode!=37 && event.keyCode!=38 && event.keyCode!=39 && event.keyCode!=40)     
        {    
           if(txtlen!=0)
            {               
             if(txtlen==2)
              {
              document.getElementById(txtid).value=txtvalue+":";
              }
              else
              {
              document.getElementById(txtid).value=txtvalue;
              }               
            } 
        }                                 
     }
    </script>
    <div><h2 class="page-header">Payroll Input Manual Entry<asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" CssClass="InputDefaultStyle"
                    OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged">
                </asp:DropDownList>
        </h2></div>
    <div>
    <h3><asp:Label ID="lbl_error" runat="server" CssClass="Error" ForeColor="Red"></asp:Label></h3>
    <div style="float:right;">
   </div>
    </div>
    
    <table width="70%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td >
            
    <table width="100%" border="0" cellpadding="5" cellspacing="1" id="tbl_details" runat="server" class="table">
        <tr valign="middle">
          
            <td align="left" >
                <asp:Label ID="Label2" runat="server" Text="Calculation Method" 
                    ></asp:Label>
            </td>
            <td colspan="2" >
                <asp:Label ID="lblcalcmethod" runat="server" CssClass="form-control" 
                    Text="None"></asp:Label>
            </td>
        </tr>
        <tr valign="middle">
          
            <td align="left" >
                <asp:Label ID="Label1" runat="server" Text="Month" ></asp:Label>
            </td>
            <td >
                <asp:DropDownList ID="ddl_Month" runat="server" CssClass="form-control" >
                    <asp:ListItem Value="1">January</asp:ListItem>
                    <asp:ListItem Value="2">February</asp:ListItem>
                    <asp:ListItem Value="3">March</asp:ListItem>
                    <asp:ListItem Value="4">April</asp:ListItem>
                    <asp:ListItem Value="5">May</asp:ListItem>
                    <asp:ListItem Value="6">June</asp:ListItem>
                    <asp:ListItem Value="7">July</asp:ListItem>
                    <asp:ListItem Value="8">August</asp:ListItem>
                    <asp:ListItem Value="9">September</asp:ListItem>
                    <asp:ListItem Value="10">October</asp:ListItem>
                    <asp:ListItem Value="11">November</asp:ListItem>
                    <asp:ListItem Value="12">December</asp:ListItem>
                </asp:DropDownList>
                </td>
            <td>            &nbsp;</td>
        </tr>
        <tr>
            <td align="left" >
                <asp:Label ID="Label4" runat="server" Text="Year" ></asp:Label></td>
            <td  colspan="2">
            <table width="50%"><tr><td>
                <asp:DropDownList ID="ddl_Year" runat="server" CssClass="form-control" >
                </asp:DropDownList>
                </td><td>
                <asp:DropDownList ID="ddl_Day" runat="server" Visible="false" 
                        CssClass="form-control">
                </asp:DropDownList>
                </td><td>
                <asp:Button ID="btn_refresh" runat="server"  Text="Show" CssClass="btn btn-primary" onclick="btn_refresh_Click1"  />
                     
                     </td>
                     </tr>
                     </table>
                     </td>

        </tr>
        
        <tr id="row_category" runat="server">
            <td align="left">
                <asp:Label ID="Label3" runat="server" Text="Category" ></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddl_category" CssClass="form-control" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="ddl_category_SelectedIndexChanged" >
                    <asp:ListItem Value="0">All</asp:ListItem>
                    <asp:ListItem Value="1">Employee</asp:ListItem>
                    <asp:ListItem Value="2">Department</asp:ListItem>
                    <asp:ListItem Value="3">Designation</asp:ListItem>
                    <asp:ListItem Value="4">Category</asp:ListItem>
                </asp:DropDownList>
                </td>
            <td>
               <input id="hdtxt_monthcalc" type="hidden" runat="server"   class="form-control" />
               </td>
               
                </tr>
                <tr>
                    <td align="left"><asp:Label ID="txt_department" runat="server" Text="Department" Visible="false"></asp:Label></td>
                <td>
                <asp:DropDownList ID="ddl_department" CssClass="form-control" runat="server"
                    AutoPostBack="True" OnSelectedIndexChanged="ddl_department_SelectedIndexChanged"
                    Visible="false" >
                </asp:DropDownList></td>
                <td></td>
            
        </tr>
    </table>
    <br />
    <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
        <contenttemplate>


    </td>
        </tr>
    </table>

    <div  id="div_grd" runat="server"  style="width:100%; overflow:auto;">
    <asp:GridView ID="grid_input" runat="server" AllowPaging="True"  
            CssClass="table table-hover table-striped" AutoGenerateColumns="false" DataKeyNames="CompanyId"  
        OnPageIndexChanging="gridinput_indexchanging" OnRowEditing="edit" 
            OnRowUpdating="update" PageSize="20"  GridLines="None" Font-Size="Small">
        <HeaderStyle Font-Size="Small" />
        <PagerSettings FirstPageImageUrl="~/Images/i_firstpage_disable.gif"
            LastPageImageUrl="~/Images/i_lastpage_disable.gif" Mode="NumericFirstLast" />
        <Columns>
            <asp:TemplateField >
                <HeaderTemplate >
                
                   <b> Empcode</b>
                  
                </HeaderTemplate>
                <ItemTemplate  >
                

                    <asp:Label id="grd_txt_employeecode" runat="server"  Enabled="false"  Text='<%#Eval("LastName") %>'></asp:Label>
                    <asp:DropDownList ID="grid_ddl_employee" runat="server" Enabled="false"  style="border:0;width:100%; background-color:White;" 
                        Visible="false" >
                    </asp:DropDownList>
             
                </ItemTemplate>
                
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-BackColor="#E6E6E6" HeaderStyle-BackColor="#E6E6E6" HeaderStyle-Width="20px">
                <HeaderTemplate>
                <center>
                 <b> Calc. Days</b>
                    </center>
                </HeaderTemplate>
                <ItemTemplate >
                </center>
                <asp:TextBox id="grd_txt_calcdays" runat="server"   Enabled="false" Text='<%#Eval("Calc_Days") %>' Width="100%"></asp:TextBox>
                        </center>
                </ItemTemplate>
                
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-BackColor="#E6E6E6" HeaderStyle-BackColor="#E6E6E6" HeaderStyle-Width="20px">
                <HeaderTemplate>
                <center>
                <b>Paid Days</b>
                    </center>
                </HeaderTemplate>
                <ItemTemplate>
                <center>
                <asp:TextBox id="grd_txt_paiddays" runat="server"  Enabled="false" Width="100%" Text='<%#Eval("Paid_Days") %>'></asp:TextBox>
                   
                      </center>
                </ItemTemplate>
                <HeaderStyle BackColor="#E6E6E6" Width="20px" />
                <ItemStyle BackColor="#E6E6E6" />
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-Width="20px">
                <HeaderTemplate >
                <center>
                <b > Prs. Days</b>
                </center>
                </HeaderTemplate>
                <ItemTemplate>
                <center>
                <asp:TextBox id="grd_txt_presdays" runat="server" AutoPostBack="true"
                        onkeydown="AllowOnlyNumeric1(event);" Width="100%" 
                        Text='<%#Eval("Present_Days") %>' ontextchanged="grd_txt_presdays_TextChanged"></asp:TextBox>
                        </center>
                </ItemTemplate>
                <HeaderStyle Width="20px" />
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-BackColor="#E6E6E6" HeaderStyle-BackColor="#E6E6E6" HeaderStyle-Width="20px">
                <HeaderTemplate>
                <center>
                <b > Abs. Days</b>
                    </center>
                </HeaderTemplate>
                <ItemTemplate>
                <center>
                <asp:TextBox id="grd_txt_absdays" runat="server" Width="100%" Enabled="false"
                        onkeydown="AllowOnlyNumeric1(event);" Text='<%#Eval("Absent_Days") %>' 
                        AutoPostBack="True" ontextchanged="grd_txt_absdays_TextChanged"></asp:TextBox>
                </center>
                </ItemTemplate>
                <HeaderStyle BackColor="#E6E6E6" Width="20px" />
                <ItemStyle BackColor="#E6E6E6" />
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-Width="20px">
                <HeaderTemplate>
                <center>
                  <b>Leave Days</b>
                   </center>
                </HeaderTemplate>
                <ItemTemplate>
                <center>
                <asp:TextBox id="txt_leavedays" runat="server" Width="100%"  AutoPostBack="True" 
                        onkeydown="AllowOnlyNumeric1(event);" Text='<%#Eval("TotLeave_Days") %>' 
                        ontextchanged="txt_leavedays_TextChanged"></asp:TextBox>
                </center>
                </ItemTemplate>
                
                <HeaderStyle Width="20px" />
                
            </asp:TemplateField>
            <%--<asp:TemplateField ItemStyle-BackColor="#E6E6E6" HeaderStyle-BackColor="#E6E6E6">
                <HeaderTemplate >
                <center>
                <b> W.off Days</b>
                   </center>
                </HeaderTemplate>
                <ItemTemplate>
                <center>
                <asp:TextBox id="text_weekdays" runat="server" Width="100%" Enabled="false"
                        onkeydown="AllowOnlyNumeric1(event);" Text='<%#Eval("WeekOffDays") %>'></asp:TextBox>
                        </center>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderStyle-Width="20px">
                <HeaderTemplate>
                <center>
                <b> Holi days</b>
                    </center>
                </HeaderTemplate>
                <ItemTemplate>
                <center>
                <asp:TextBox id="text_holiday" runat="server" Width="100%"  AutoPostBack="True"
                        onkeydown="AllowOnlyNumeric1(event);" Text='<%#Eval("Holidays") %>' 
                        ontextchanged="text_holiday_TextChanged"></asp:TextBox>
                        </center>
                </ItemTemplate>
                <HeaderStyle Width="20px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-Width="20px">
                <HeaderTemplate>
                <center>
                <b> Duty Days</b>
                    </center>
                </HeaderTemplate>
                <ItemTemplate>
                <center>
                <asp:TextBox id="txt_dutyday" runat="server" Width="100%"  AutoPostBack="True"
                        onkeydown="AllowOnlyNumeric1(event);" Text='<%#Eval("OnDuty_days") %>' 
                        ontextchanged="txt_dutyday_TextChanged"></asp:TextBox>
                        </center>
                </ItemTemplate>
                <HeaderStyle Width="20px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-Width="20px">
                <HeaderTemplate>
                <center>
                <b> Comp Days</b>
                    </center>
                </HeaderTemplate>
                <ItemTemplate>
                <center>
                <asp:TextBox id="txt_compday" runat="server" Width="100%"  AutoPostBack="True"
                        onkeydown="AllowOnlyNumeric1(event);" Text='<%#Eval("Compoff_Days") %>' 
                        ontextchanged="txt_compday_TextChanged"></asp:TextBox>
                        </center>
                </ItemTemplate>
                <HeaderStyle Width="20px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-Width="20px">
                <HeaderTemplate >
                 <center>
                 <b> Tour Days</b>
                 </center>   
                </HeaderTemplate>
                <ItemTemplate>
                <center>
                <asp:TextBox id="txt_tourday" runat="server" Width="100%"  AutoPostBack="True"
                        onkeydown="AllowOnlyNumeric1(event);" Text='<%#Eval("Tour_Days") %>' 
                        ontextchanged="txt_tourday_TextChanged"></asp:TextBox>
                        </center>
                </ItemTemplate>
                <HeaderStyle Width="20px" />
            </asp:TemplateField>
            <%--<asp:TemplateField>
                <HeaderTemplate>
                 <center>
                 <b> Attn Bonus</b>
                 </center>
                </HeaderTemplate>
                <ItemTemplate>
                <center>
                <asp:TextBox id="text_attbonus" runat="server" Width="100%" onkeydown="AllowOnlyNumeric1(event);" Text='<%#Eval("Att_Bonus") %>'></asp:TextBox>
                        </center>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderStyle-Width="20px">
                <HeaderTemplate>
                <center>
                <b> OT Hrs (HH:MM)</b>
                </center>
                </HeaderTemplate>
                <ItemTemplate>
                <center>
                <asp:TextBox id="text_othours" runat="server" Width="100%" onkeydown="AllowOnlyNumeric1(event);" Text='<%#Eval("Date","{0:HH:mm}") %>'></asp:TextBox>
                </center>
                </ItemTemplate>
                <HeaderStyle Font-Bold="False" />
            </asp:TemplateField>
            <asp:TemplateField Visible="False">
                <HeaderTemplate>
                    Edit
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="img_edit" runat="server" AlternateText=""  CssClass="btn btn-success btn-circle glyphicon glyphicon-check"
                        CommandName="update" ></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-Width="25px">
                <HeaderTemplate>
                    <asp:Button ID="btn_saveall" runat="server" Text="Save All" CssClass="btn btn-success"
                        onclick="btn_saveall_Click1" />
                </HeaderTemplate>
                <ItemTemplate>
                <center>
                    <asp:LinkButton ID="img_save" runat="server" AlternateText="" Text="Save" 
                        CommandName="edit" Height="20"></asp:LinkButton>
                        </center>
                </ItemTemplate>
                <HeaderStyle Width="25px" />
            </asp:TemplateField>
            <asp:TemplateField Visible="true">
                <ItemTemplate>
                    <input id="grdhd_txt_empid" runat="server" type="hidden" value='<%#Eval("EmployeeId") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle HorizontalAlign="Justify" VerticalAlign="Middle" Width="300px" 
            Wrap="False" />
        <RowStyle BorderColor="Silver" BorderStyle="Groove" BorderWidth="1px" />
    </asp:GridView>
            </div>
</contenttemplate>
    </asp:UpdatePanel>
</asp:Content>
