<%@ Page Language="C#" MasterPageFile="~/HRMS.master"
    AutoEventWireup="true" CodeFile="timecardsetup.aspx.cs" Inherits="Hrms_Master_Default"
    Title="Welcome to HRMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/css">
    .scrollingControlContainer
    {
        overflow-x: hidden;
        overflow-y: scroll;
    }
    </script>
   
    <script language="javascript" type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return true;

            return false;
        }

        function onlyNumbersWithDot(e) {
            if (e.keyCode > 0) {
                charCode = e.which || e.keyCode;
            }
            else if (typeof (e.charCode) != "undefined") {
                charCode = e.which || e.keyCode;
            }
            if (charCode == 46)
                return true
            if (charCode > 31 && (charCode < 48 || charCode > 58))
                return false;
            return true;
        }
  
   function show_message()
    {
        alert("Course Name Already Exist");
    }
    
    function show_Error()
    {
        alert("Enter Course Name");
    }
  
    function fnSave()
    {   
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$CourseName.value == "")
        {
            alert("Enter Course Name");
            aspnetForm.ctl00$ContentPlaceHolder1$CourseName.focus();
            return false;
        }                        
        else
        { 
              return true;  
        }
    } 
    function coudnt_del()
    {
       alert("Cannot delete. It is already assigned to someone");
    }  
    
     function fn_date(event,txtid)
     {  
       var len;
       var txtvalue; 
       var bool_obj; 
       var i,j;    
       var str = "";
       str.substring(4,2);
       txtvalue= document.getElementById(txtid).value;
       txtlen=txtvalue.length;  
      // alert(event.keyCode);
       if (event.keyCode == 186 && !event.shiftKey) {
           return;
       }
       if (event.keyCode != 8 && event.keyCode != 186 && event.keyCode != 16 && event.keyCode != 46 && event.keyCode != 35 && event.keyCode != 36 && event.keyCode != 37 && event.keyCode != 38 && event.keyCode != 39 && event.keyCode != 40)
       {
          
           if(txtlen!=0)
            {
                if (txtlen == 2) 
                {
                    if (txtvalue >= 24) {
                        document.getElementById(txtid).value = "";
                    }
                    else {
                        document.getElementById(txtid).value = txtvalue + ":";
                    }
                }

                else {
                    document.getElementById(txtid).value = txtvalue;
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
         txtvalue = document.getElementById(txtid).value;
         txtlen = txtvalue.length;

         if (event.keyCode != 8 && event.keyCode != 46 && event.keyCode != 35 && event.keyCode != 36 && event.keyCode != 37 && event.keyCode != 38 && event.keyCode != 39 && event.keyCode != 40) {
             if (txtlen != 0) {
                 if (txtlen == 2) {
                     if (txtvalue > 31) {
                         document.getElementById(txtid).value = "";
                     }
                     else {
                         document.getElementById(txtid).value = txtvalue + "";
                     }
                 }
                 else {
                     document.getElementById(txtid).value = txtvalue;
                 }
             }
         }
     }
     function validateTime(txt,txttime) {

         if (txt.value.length == 5) {

             //var newreg = /^[0-2][0-3]:[0-5][0-9]$/;
             var regex = /^(([0-1][0-9])|(2[0-3])):[0-5][0-9]$/;

             var first = txt.value.split(":")[0];
             var second = txt.value.split(":")[1];

             if (first > 24 || second > 59) {
                 alert("Invalid time format");
                 document.getElementById(txttime).value = "";
                 document.getElementById(txttime).focus();
             }


             else if (!regex.test(txt.value)) {

                 alert("Invalid time format");
                 document.getElementById(txttime).value = "";
                 document.getElementById(txttime).focus();
             }

         }
         else if (txt.value != 0) {
             alert("Invalid time format");
             document.getElementById(txttime).value = "";
             document.getElementById(txttime).focus();
         }

         return false;
     }
     function Autocolon(e,getid) {

         //document.getElementById('hdnChange').value = "1";

         var x = document.getElementById(getid);

         var key = window.event ? e.keyCode : e.which;

         if (key != '8' && key != '46') {

             if (x.value.length == 2) {
                 //  x.value += ":";
                 if (x.value >= 24) {
                     x.value = "";
                 }
                 else {
                     x.value += ":";
                 }
             }
         }
     }
     
    </script>
    <div class="row">
                <div class="col-lg-12">
                    <h2 class="page-header">Time Card Setup</h2>
                </div>
                <!-- /.col-lg-12 -->
            </div>

              <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
    <div class="panel panel-default">
                        <div class="panel-heading">
                            Shift Details
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
                            <span></span>
                             <div class="pull-right" style="margin-bottom: 0px">
                                <div class="btn-group">
                                    <div class="btn-group" style="left: -14px; top: 0px; height: 22px; width: 129px">
                                        <asp:DropDownList ID="ddl_branch" runat="server" AutoPostBack="True" enabled="true" OnSelectedIndexChanged="ddl_branch_SelectedIndexChanged" Visible="true" Width="115px">
                                            <asp:ListItem>select branch</asp:ListItem>
                                        </asp:DropDownList>
                                        <ul class="dropdown-menu pull-right" role="menu">
                                            <li><a href="#">Action</a> </li>
                                            <li><a href="#">Another action</a> </li>
                                            <li><a href="#">Something else here</a> </li>
                                            <li class="divider"></li>
                                            <li><a href="#">Separated link</a> </li>
                                        </ul>
                                    </div>
                                    <%--<ul class="dropdown-menu pull-right" role="menu">
                                        <li><a href="#">Action</a>
                                        </li>
                                        <li><a href="#">Another action</a>
                                        </li>
                                        <li><a href="#">Something else here</a>
                                        </li>
                                        <li class="divider"></li>
                                        <li><a href="#">Separated link</a>
                                        </li>
                                    </ul>--%>
                                </div>
                            </div>
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <div id="morris-area-chart" style="width:100%" aria-required="true">
                                <div class="panel-body" >
                                
                           
                    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" CssClass="table table-hover table-striped" GridLines="None"
            AutoGenerateColumns="False" ShowFooter="True" onrowcommand="GridView1_RowCommand" onrowdeleting="GridView1_RowDeleting" 
            onrowdatabound="GridView1_RowDataBound" onrowediting="GridView1_RowEditing" onselectedindexchanged="GridView1_SelectedIndexChanged" HorizontalAlign="Center" 
            onrowupdating="GridView1_RowUpdating" onrowcancelingedit="GridView1_RowCancelingEdit" visible="true">    
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Shift Code" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_shiftcode" runat="server" Text='<%# Eval("shift_code") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="txt_shiftcode_edit" runat="server" Text='<%# Bind("shift_code") %>' Width="50px"></asp:Label>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_shiftcode" runat="server" CssClass="form-control" MaxLength="2" placeholder="G" Width="65px"></asp:TextBox>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Start Time" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_starttime" runat="server" Text='<%# Eval("start_time","{0:HH:mm}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_starttime_edit" runat="server" CssClass="form-control" Text='<%# Bind("start_time","{0:HH:mm}") %>' Width="65px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_starttime" runat="server" CssClass="form-control" MaxLength="5" onblur="validateTime(this,this.id);" onkeypress="return onlyNumbersWithDot(event);" onkeyup="Autocolon(event,this.id);" placeholder="24:00" Width="65px"></asp:TextBox>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Break(O)" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_breaktimeo" runat="server" Text='<%# Eval("break_time_out","{0:HH:mm}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_breaktimeo_edit" runat="server" CssClass="form-control" Text='<%# Bind("break_time_out","{0:HH:mm}") %>' Width="65px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_breaktimeo" runat="server" CssClass="form-control" MaxLength="5" onblur="validateTime(this,this.id);" onkeypress="return onlyNumbersWithDot(event);" onkeyup="Autocolon(event,this.id);" placeholder="24:00" Width="65px"></asp:TextBox>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Break(I)" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_breaktimei" runat="server" Text='<%# Eval("break_time_in","{0:HH:mm}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_breaktimei_edit" runat="server" CssClass="form-control" Text='<%# Bind("break_time_in","{0:HH:mm}") %>' Width="65px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_breaktimei" runat="server" CssClass="form-control" MaxLength="5" onblur="validateTime(this,this.id);" onkeypress="return onlyNumbersWithDot(event);" onkeyup="Autocolon(event,this.id);" ontextchanged="txt_breaktimei_TextChanged" placeholder="24:00" Width="65px"></asp:TextBox>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="End Time" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_endtime" runat="server" Text='<%# Eval("end_time","{0:HH:mm}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_endtime_edit" runat="server" CssClass="form-control" Text='<%# Bind("end_time","{0:HH:mm}") %>' Width="65px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_endtime" runat="server" CssClass="form-control" MaxLength="5" onblur="validateTime(this,this.id);" onkeypress="return onlyNumbersWithDot(event);" onkeyup="Autocolon(event,this.id);" placeholder="24:00" Width="65px"></asp:TextBox>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Shift Indicator" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_shiftindicator" runat="server" Text='<%# Eval("shift_indicator") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddl_shiftindicator_edit" runat="server" CssClass="form-control" DataTextField="Stat" DataValueField="shift_indicator" SelectedValue='<%#Eval("shift_indicator")%>'>
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Current Day</asp:ListItem>
                                                    <asp:ListItem>Night</asp:ListItem>
                                                    <asp:ListItem>Next Day</asp:ListItem>
                                                    <asp:ListItem>Flexi Time</asp:ListItem>
                                                    <asp:ListItem>Day Count</asp:ListItem>
                                                    <asp:ListItem>Student</asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddl_shiftindicator" runat="server" CssClass="form-control">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Current Day</asp:ListItem>
                                                    <asp:ListItem>Night</asp:ListItem>
                                                    <asp:ListItem>Next Day</asp:ListItem>
                                                    <asp:ListItem>Flexi Time</asp:ListItem>
                                                    <asp:ListItem>Day Count</asp:ListItem>
                                                    <asp:ListItem>Student</asp:ListItem>
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <Itemtemplate>
                                                <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/edit_icon.png" />
                                                <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" ImageUrl="~/Images/delete_icon.jpg" />
                                            </Itemtemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="btnUpdate" runat="server" CommandName="Update" CssClass="btn btn-xs btn-success "><i class="glyphicon glyphicon-saved"></i> Update</asp:LinkButton>
                                                <%--<asp:ImageButton ID="btnUpdate" ImageUrl="~/Images/save_icon.jpg"  runat="server" CommandName="Update" AccessKey />--%>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="add" CssClass="btn btn-sm btn-success "><i class="glyphicon glyphicon-floppy-disk "></i> Save</asp:LinkButton>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <!-- /.panel-body -->
                            
                    </div>

                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Time Attendance &amp; Payroll Setup</div>
                        <!-- /.panel-heading -->
                       
                        
                        <div class="panel-body">
                            <div id="Div1">
    <table ID="branch_time" runat="server" class="table table-striped table-bordered" style="width: 100%">
        <tr>
            <td align="center" colspan="3">
                    &nbsp;</td>
        </tr>
        <tr>
            <td align="center" style=" width: 33%;">
                
                
                <span class="style2" style="font-weight: bold; ">Attendance 
                Time Information</span></td>
            <td align="center"  style=" width: 34%">
                
                <span class="style2" style=" font-weight: bold">Month Days 
                and Over Time Calculations</span></td>
            <td align="center"  style=" width: 33%;">
  
                <span class="style2" 
                    style=" font-weight: bold;">
                Professional Tax Criteria</span></td>
        </tr>
        <tr>
            <td align="center"  style="width: 33%">
                
                
                <table border="1" class="table table-striped table-bordered table-hover" style="width: 100%; height:100%;">
                    <tr>
                        <td style="width:73%;">
                            <span style="color: Black;">
                            Intime Limit</span></td>
                        <td align="center" style="width:27%;" >
                            <span ><span >
                            <asp:TextBox CssClass="form-control" ID="txt_intimelt" placeholder="00:00" runat="server" Width="100%"  MaxLength="5" onkeydown="AllowOnlyNumeric1(event);" onkeypress="return onlyNumbersWithDot(event);"
                                onkeyup="fn_date(event,this.id);" ></asp:TextBox>
                            </span></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:73%;">
                            <span style="color: Black;"> Early Intime</span></td>
                        <td align="center"  style="width:27%;">
                            <span><span>
                            <asp:TextBox CssClass="form-control" ID="txt_earlyin" placeholder="00:00" runat="server" MaxLength="5" Width="100%" onkeydown="AllowOnlyNumeric1(event);" 
                                onkeyup="fn_date(event,this.id);" onkeypress="return onlyNumbersWithDot(event);"></asp:TextBox>
                            </span></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:73%;">
                            <span style="color: Black; "><span > Shift 
                            LateIn/EarlyOut Limit</span></span></td>
                        <td align="center" style="width:27%;" >
                            <span ><span>
                            <asp:TextBox  ID="txt_shiftLin" runat="server" placeholder="00:00" MaxLength="5" onkeydown="AllowOnlyNumeric1(event);" Width="100%"
                                onkeyup="fn_date(event,this.id);" CssClass="form-control" onkeypress="return onlyNumbersWithDot(event);"></asp:TextBox>
                            </span></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:73%;">
                            <span style="color: Black;"> Lunch EarlyIn and LateOut 
                            Limit</span></td>
                        <td align="center" style="width:27%;">
                            <span ><span >
                            <asp:TextBox CssClass="form-control" ID="txt_lunchEin" placeholder="00:00" runat="server" MaxLength="5" Width="100%" onkeydown="AllowOnlyNumeric1(event);"
                                onkeyup="fn_date(event,this.id);" onkeypress="return onlyNumbersWithDot(event);"
></asp:TextBox>
                            </span></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:73%;">
                            <span style="color: Black; "><span > Half Day Work 
                            Hours Eligibility</span></span></td>
                        <td align="center"style="width:27%;" >
                            <span ><span >
                            <asp:TextBox CssClass="form-control" ID="txt_halfdaylt" placeholder="00:00" runat="server" MaxLength="5" Width="100%" onkeydown="AllowOnlyNumeric1(event);"
                                onkeyup="fn_date(event,this.id);" onkeypress="return onlyNumbersWithDot(event);"
></asp:TextBox>
                            </span></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:73%;">
                            <span style="color: Black;"> Over Time Limit</span></td>
                        <td align="center" style="width:27%;">
                            <span ><span >
                            <asp:TextBox CssClass="form-control" ID="txt_otlt" placeholder="00:00" runat="server" MaxLength="5" Width="100%" onkeydown="AllowOnlyNumeric1(event);"
                                onkeyup="fn_date(event,this.id);" onkeypress="return onlyNumbersWithDot(event);"
></asp:TextBox>
                            </span></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:73%;">
                            <span style="color: Black;"> Permission Limit</span></td>
                        <td align="center" style="width:27%;">
                            <span ><span >
                            <asp:TextBox CssClass="form-control" ID="txt_perlt" placeholder="00:00" runat="server"  onkeydown="AllowOnlyNumeric1(event);" Width="100%"
                                onkeyup="fn_date(event,this.id);"  MaxLength="5" onkeypress="return onlyNumbersWithDot(event);"
></asp:TextBox>
                            </span></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:73%" >
                            <span style="color: Black;"><span > 
                            Month Leave Days Limit</span></span></td>
                        <td align="center" style="width:27%;" >
                            <span ><span >
                            <asp:TextBox CssClass="form-control" ID="txt_leavelt" placeholder="00" Width="100%" runat="server" onkeydown="AllowOnlyNumeric1(event)" onkeyup="fn_limit(event,this.id);"  MaxLength="2" onkeypress="return onlyNumbersWithDot(event);"></asp:TextBox>
                            </span></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:73%;">
                            <span style="color: Black; "> Morning Over Time </span></td>
                                                        <td align="center" style="width:27%;" >
                                                            <span >
                                                            <asp:CheckBox ID="chk_morningot" runat="server" />
                                                            </span>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
            <td align="center" style="width: 34%">
                
                <table border="1" class="table table-striped table-bordered" style="width: 100%; height:100%;">
                    <tr>
                        <td colspan="2" >
<table width="100%" class="table"><tr><td align="center">  <asp:Button ID="btn_manualdays" runat="server" CssClass="btn btn-info"
                                onclick="btn_manualdays_Click" Text="Manual Days"  />
</td><td align="center"> <asp:Button ID="btn_monthdays" runat="server" CssClass="btn btn-info"
                                onclick="btn_monthdays_Click" Text="Month Days"/>
</td></tr><tr><td colspan="2" align="center"><asp:Button ID="btn_weekoff" 
            runat="server" CssClass="btn btn-info"
                                onclick="btn_weekoff_Click" 
            Text="Week off excluded days"  />
</td></tr></table>
                           </td>
                    </tr>
                    <tr>
                        <td >
                            <span style="color: Black">
                            <asp:Label ID="lbl_weekoff1" runat="server" 
                                Text="Week Off 1" ></asp:Label>
                            </span>
                        </td>
                        <td align="center" >
                            <asp:DropDownList CssClass="form-control" ID="ddl_weeloff1" runat="server" 
                                onselectedindexchanged="ddl_weeloff1_SelectedIndexChanged" >
                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="Sunday">Sunday</asp:ListItem>
                                <asp:ListItem Value="Monday">Monday</asp:ListItem>
                                <asp:ListItem Value="Tuesday">Tuesday</asp:ListItem>
                                <asp:ListItem Value="Wednesday">Wednesday</asp:ListItem>
                                <asp:ListItem Value="Thursday">Thursday</asp:ListItem>
                                <asp:ListItem Value="Friday">Friday</asp:ListItem>
                                <asp:ListItem Value="Saturday">Saturday</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span style="color: Black">
                            <asp:Label ID="lbl_weekoff2" runat="server" 
                                Text="Week Off 2" ForeColor="Black"></asp:Label>
                            </span>
                        </td>
                        <td align="center" >
                            <asp:DropDownList CssClass="form-control" ID="ddl_weekoff2" runat="server" 
                                onselectedindexchanged="ddl_weekoff2_SelectedIndexChanged" >
                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="Sunday">Sunday</asp:ListItem>
                                <asp:ListItem Value="Monday">Monday</asp:ListItem>
                                <asp:ListItem Value="Tuesday">Tuesday</asp:ListItem>
                                <asp:ListItem Value="Wednesday">Wednesday</asp:ListItem>
                                <asp:ListItem Value="Thursday">Thursday</asp:ListItem>
                                <asp:ListItem Value="Friday">Friday</asp:ListItem>
                                <asp:ListItem Value="Saturday">Saturday</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>

                            <asp:Label ID="lbl_manual" runat="server"  
                                ForeColor="Black" Text="Manual Days"></asp:Label>
                        </td>
                        <td align="center" >
                            <asp:TextBox CssClass="form-control" ID="txt_manual" runat="server" MaxLength="2" onkeydown="AllowOnlyNumeric1(event)"
                                ontextchanged="txt_manual_TextChanged" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  >

                            <asp:Label ID="lbl_otday" runat="server"  
                                ForeColor="Black" Text="OT days/month"></asp:Label></td><td>
<asp:TextBox CssClass="form-control" ID="txt_otdays" runat="server" onkeyup="fn_limit(event,this.id);" MaxLength="2" ontextchanged="txt_otdays_TextChanged" onkeydown="AllowOnlyNumeric1(event)" onkeypress="return onlyNumbersWithDot(event);"

                                ></asp:TextBox></td></tr><tr><td>
                            <asp:Label ID="lbl_othr" runat="server"  
                                ForeColor="Black" Text="OT hrs/day"></asp:Label></td><td>
<asp:TextBox CssClass="form-control" ID="txt_othrs" runat="server" MaxLength="5" ontextchanged="txt_othrs_TextChanged"  onkeyup="fn_date(event,this.id);"  onkeypress="return onlyNumbersWithDot(event);"

                                ></asp:TextBox>
                        </td>
                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="2">
                                                            <asp:RadioButtonList ID="rdo_timecard" runat="server"  
                                                                ForeColor="Black" RepeatDirection="Horizontal" Width="100%" >
                                                                <asp:ListItem Selected="True" Value="Daily Time Card">Daily Time Card</asp:ListItem>
                                                                <asp:ListItem Value="Cummulative Time Card">Day Count Card</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align:center" colspan="2">
                                                            <asp:Button ID="btn_save" runat="server" CssClass="btn btn-success" onclick="btn_save_Click" Text="Save" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="btn_reset" runat="server" CssClass="btn btn-warning" onclick="btn_reset_Click" Text="Reset" />
                                                        </td>
                                                    </tr>
                                                </table>
                
            </td>
            <td align="center"  style="width: 33%">
  
                <table border="1" style="width: 100%; height:100%;" class="table table-striped table-bordered" >
                    <tr>
                        <td align="center" colspan="2">
                            <asp:CheckBox ID="chk_allmonths" runat="server" AutoPostBack="True" 
                                 ForeColor="Black" 
                                oncheckedchanged="chk_allmonths_CheckedChanged" Text="Select All" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" >

                                <asp:CheckBoxList ID="chk_months" runat="server"  
                                    ForeColor="Black">
                                    <asp:ListItem Value="01">January</asp:ListItem>
                                    <asp:ListItem Value="02">February</asp:ListItem>
                                    <asp:ListItem Value="03">March</asp:ListItem>
                                    <asp:ListItem Value="04">April</asp:ListItem>
                                    <asp:ListItem Value="05">May</asp:ListItem>
                                    <asp:ListItem Value="06">June</asp:ListItem>
                                    <asp:ListItem Value="07">July</asp:ListItem>
                                    <asp:ListItem Value="08">August</asp:ListItem>
                                    <asp:ListItem Value="09">September</asp:ListItem>
                                    <asp:ListItem Value="10">October</asp:ListItem>
                                    <asp:ListItem Value="11">November</asp:ListItem>
                                    <asp:ListItem Value="12">December</asp:ListItem>
                                </asp:CheckBoxList>

                        </td>
                    </tr>
                    <tr>
                        <td align="center" >
                            <span style=" color: Black;">Reader Name</span></td>
                        <td align="center" >
                            <asp:DropDownList CssClass="form-control" ID="ddl_reader" runat="server" 
                                onselectedindexchanged="ddl_reader_SelectedIndexChanged" >
                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="None">None</asp:ListItem>
                                <asp:ListItem Value="Mono Display">Mono Display</asp:ListItem>
                                <asp:ListItem Value="Color Display">Color Display</asp:ListItem>
                                <asp:ListItem Value="Text Input">Text Input</asp:ListItem>
                                <asp:ListItem Value="Excel Input">Excel Input</asp:ListItem>
                                <asp:ListItem Value="CSV    Input">CSV Input</asp:ListItem>
                                <asp:ListItem Value="Others">Others</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                </table>
  
            </td>
        </tr>
    </table>
    
                            </div>
                        </div>
                        
                        <!-- /.panel-body -->
                    </div>
                    </ContentTemplate>
                        </asp:UpdatePanel>
    </asp:Content>







