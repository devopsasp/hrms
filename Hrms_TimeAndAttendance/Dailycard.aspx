<%@ Page EnableEventValidation="false" Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" 
    CodeFile="Dailycard.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="Hrms_Employee_Default"
    Title="Welcome to HRMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<script language="javascript" type="text/javascript">
    //This Script is used to maintain Grid Scroll on Partial Postback
    var scrollTop;
    //Register Begin Request and End Request 
    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    //Get The Div Scroll Position
    function BeginRequestHandler(sender, args) {
        var m = document.getElementById('divGrid');
        scrollTop = m.scrollTop;
    }
    //Set The Div Scroll Position
    function EndRequestHandler(sender, args) {
        var m = document.getElementById('divGrid');
        m.scrollTop = scrollTop;
    }
    function fn_date(event, txtid) {
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
                    if (txtvalue >= 24) {
                        document.getElementById(txtid).value = "";
                    }
                    else {
                        document.getElementById(txtid).value = txtvalue + ":";
                    }
                }
                //                else if (txt == 5) {
                //                    str = document.getElementById(txtid).value;
                //                    j = str.substring(4, 2);
                //                    if (j > 59) {
                //                        document.getElementById(txtid).value = "";
                //                    }
                //                }
                else {
                    document.getElementById(txtid).value = txtvalue;
                }
            }
        }
    }
</script>
<script type="text/javascript">

    function fn_time(event, txtid) {
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

    function onlyNumbersWithDot(e) {
        var charCode;
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

</script>


 
    <script type="text/css">
    .scrollingControlContainer
    {
        overflow-x: hidden;
        overflow-y: scroll;
    }
    </script>

     <link href="../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript" language="javascript" src="../../Scripts/Datavalid.js"></script>
    
    <script language="javascript" type="text/javascript">


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
        alert("Sorry,Couldnt Delete Because it was assigned to employee");
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
           bool_obj=true;
                      
           if(bool_obj==true)
             {
                  if(txtlen==2 || txtlen==5)
                  {
                  document.getElementById(txtid).value=txtvalue+"/";
                  }
                  else
                  {
                  document.getElementById(txtid).value=txtvalue;
                  }
             }
             else
             {            
               document.getElementById(txtid).value= txtvalue.substring(0,txtlen-1);              
             }                       
        }  
    }                                 
 }
     
    </script>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    <div class="row">
                <div class="col-lg-12">
                    <h2 class="page-header">Daily Time Card<span style="color: #800000" __designer:mapid="5ad">
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server"   ConnectionString="<%$ ConnectionStrings:connectionstring %>" 
                        
                        
                            SelectCommand="SELECT [Employee_First_Name] FROM [paym_Employee] WHERE (([pn_CompanyID] = @pn_CompanyID) AND ([pn_BranchID] = @pn_BranchID))">
                        <SelectParameters>
                            <asp:SessionParameter Name="pn_CompanyID" SessionField="Login_Temp_CompanyID"
                                Type="Int32" />
                            <asp:SessionParameter Name="pn_BranchID" SessionField="Login_temp_BranchID" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:connectionstring %>" 
                        
                        
                            SelectCommand="SELECT [Employee_First_Name] FROM [paym_Employee] WHERE (([pn_CompanyID] = @pn_CompanyID) AND ([pn_BranchID] = @pn_BranchID)) order by [Employee_First_Name] asc">
                        <SelectParameters>
                            <asp:SessionParameter Name="pn_CompanyID" SessionField="Login_Temp_CompanyID"
                                Type="Int32" />
                            <asp:SessionParameter Name="pn_BranchID" SessionField="Login_temp_BranchID" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                    </span>
                    </h2>
                </div>
                <!-- /.col-lg-12 -->
            </div>

            <div class="panel panel-default">
                        <div class="panel-heading">
                            Time Card Settings
                            <div class="pull-right">
                    <asp:DropDownList ID="ddl_branch" runat="server" class="form-control" AutoPostBack="True" onselectedindexchanged="ddl_branch_SelectedIndexChanged" style="margin-left: 12px" Width="130px">
                    </asp:DropDownList>
                            </div>
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                            <div class="divWaiting">
                            
                            <asp:Image ID="imgWait" runat="server" ImageAlign="Middle" 
                                    ImageUrl="~/Images/loading2.gif" Height="100px" Width="100px" />
                               
                            </div>
                            </ProgressTemplate>
                            </asp:UpdateProgress>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                            
                            <div align="center" id="morris-area-chart" style="width: 100%">
                
                               <table class="table table-striped table-bordered table-hover">
                                    <tbody>
                                        <tr>
                                            <td>Selection Type</td>
                                            <td>
                                                
                                                <asp:DropDownList ID="ddl_type" runat="server" AutoPostBack="True" 
                                                   class="form-control" onselectedindexchanged="ddl_type_SelectedIndexChanged" 
                                                    Width="80%">
                                                    
                                                    <asp:ListItem Selected="True">All</asp:ListItem>
                                                    <asp:ListItem>Employee</asp:ListItem>
                                                </asp:DropDownList>
                                                </span></td>
                                            <td rowspan="4">
                                                    <asp:Label ID="lbl_previous" runat="server" Font-Bold="True" 
                                                        Font-Names="Calibri"></asp:Label>
                                                    <br />
                                                    <br />
                                                    <asp:ListBox ID="lb_previous" runat="server" Height="146px" 
                                                        onselectedindexchanged="lb_previous_SelectedIndexChanged" 
                                                        Width="120px"></asp:ListBox>
                                            </td>
                                            <td rowspan="4">
                                                <asp:Label ID="lbl_current" runat="server" Font-Bold="True" 
                                                    Font-Names="Calibri" ForeColor="#333333" Visible="False"></asp:Label>
                                                <br />&nbsp;<asp:ListBox 
                                                    ID="lb_current" runat="server" Height="146px" Width="120px">
                                                </asp:ListBox>
                                            </td>
                                            <td rowspan="4">
                                                <asp:Label ID="lbl_next" runat="server" Font-Bold="True" Font-Names="Calibri" 
                                                    ForeColor="#333333" Visible="False"></asp:Label>
                                                <br />&nbsp;<asp:ListBox 
                                                    ID="lb_next" runat="server" Height="146px" Width="120px">
                                                </asp:ListBox>
                                            </td>
                                            <td>
                                                Total Days</td>
                                            <td>

                                                <asp:TextBox ID="txt_tdays" runat="server" class="form-control"
                                                    Enabled="False" Width="60px"></asp:TextBox>
                                                </span></span></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <span style="color: #800000">
                                                <asp:DropDownList ID="ddl_department" runat="server" AutoPostBack="True" 
                                                    class="form-control" Enabled="False" 
                                                    onselectedindexchanged="ddl_department_SelectedIndexChanged" Width="90%">
                                                </asp:DropDownList>
                                                </span></td>
                                            <td>
                                                Present Days</td>
                                            <td>
                                                <asp:TextBox ID="txt_pdays" runat="server" class="form-control" Enabled="False" 
                                                    Width="60px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <span style="color: #800000">
                                                <asp:DropDownList ID="ddl_ename" runat="server" class="form-control" 
                                                    Enabled="False" Width="90%">
                                                </asp:DropDownList>
                                                </span>
                                            </td>
                                            <td>
                                                Absent Days</td>
                                            <td>
                                                <asp:TextBox ID="txt_adays" runat="server" class="form-control" Enabled="False" 
                                                    Width="60px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 173px">
                                                <asp:Label ID="lbl_from" runat="server" Text="Attendance From"></asp:Label>
                                            </td>
                                            <td>
                                              <div style=" width:120px; float:left;">
                                                <asp:TextBox ID="txt_fromdate" runat="server" class="form-control" 
                                                    MaxLength="10" onkeyup="fn_date(event,this.id);" Width="120px"></asp:TextBox>
                                                  <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txt_fromdate">
                                                  </asp:CalendarExtender>
                                              </div>
                                               <div style=" width:25px; float:left;  margin-left:10px; margin-top:3px;">                                                
                                                    <asp:Image ID="Image1" runat="server" Text="" Width="25px" ImageUrl="~/Images/calendaricon.png" />
                                 </div>


                                            </td>
                                            <td>
                                                Holidays</td>
                                            <td>
                                                <asp:TextBox ID="txt_hdays" runat="server" class="form-control" Enabled="False" 
                                                    Width="60px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 173px">
                                                <asp:Label ID="lbl_to" runat="server" Text="Attendance To" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                               <div style=" width:120px; float:left;">
                                                <asp:TextBox ID="txt_todate" runat="server" class="form-control" 
                                                    MaxLength="10"  Visible="False" 
                                                    Width="120px" AutoPostBack="True" ontextchanged="txt_todate_TextChanged"></asp:TextBox>
                                                   <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txt_todate">
                                                   </asp:CalendarExtender>
                                              </div>
                                              <div style=" width:25px; float:left;  margin-left:10px; margin-top:3px;">                                                
                                                    <asp:Image ID="Image2" runat="server"  
                                                        Text="" Width="25px" 
                                                        ImageUrl="~/Images/calendaricon.png" Visible="false" />
                                 </div>
                                            </td>
                                            <td>
                                                Manual Input</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                Leave Days</td>
                                            <td>
                                                <asp:TextBox ID="txt_ldays" runat="server" class="form-control" Enabled="False" 
                                                    Width="60px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 173px">
                                                <asp:Button ID="Btn_import" runat="server" class="btn btn-warning" 
                                                    onclick="Btn_import_Click" Text="Import Attendance" Width="150px" />
                                            </td>
                                            <td>
                                                <asp:Button ID="Btn_view" runat="server" class="btn btn-info" 
                                                    onclick="Btn_view_Click" Text="View" />
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddl_time" runat="server" AutoPostBack="True" 
                                                    class="form-control" Width="100%">
                                                    <asp:ListItem Selected="True">Select</asp:ListItem>
                                                    <asp:ListItem>Intime</asp:ListItem>
                                                    <asp:ListItem>Outtime</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_mtime" runat="server" class="form-control" Width="100px" onkeyup="fn_time(event,this.id);" onkeydown="AllowOnlyNumeric1(event);" onkeypress="return onlyNumbersWithDot(event);"
                                                    MaxLength="5"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="Btn_update" runat="server" class="btn btn-info"  Text="Apply" 
                                                    onclick="Btn_update_Click" />
                                            </td>
                                            <td>
                                                Week Off Days</td>
                                            <td>
                                                <asp:TextBox ID="txt_wdays" runat="server" class="form-control" Enabled="False" 
                                                    Width="60px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td >
                                                &nbsp;</td>
                                            <td>
                                                </span>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="7" >
                    
                                                
                                                <div id="divGrid" style="overflow-x: scroll; overflow-y: scroll; width:950px; height: 500px;">
                                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-striped"
                                                    AutoGenerateColumns="False" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" onrowcancelingedit="GridView1_RowCancelingEdit" 
                                                    onrowcommand="GridView1_RowCommand" onrowdatabound="GridView1_RowDataBound" 
                                                    onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing" 
                                                    onrowupdating="GridView1_RowUpdating" onselectedindexchanged="GridView1_SelectedIndexChanged">
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Emp Code" ItemStyle-HorizontalAlign="Left">
                                                            <EditItemTemplate>
                                                                <asp:Label ID="txt_empcode_edit" runat="server" Height="21px" Text='<%# Bind("emp_code") %>' Width="50px"></asp:Label>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_empcode" runat="server" Text='<%# Eval("emp_code") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Emp Name" 
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_empname" runat="server" Text='<%# Eval("emp_name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="txt_empname_edit" runat="server" Height="21px" Text='<%# Bind("emp_name") %>' Width="50px"></asp:Label>
                                                            </EditItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Shift Code" 
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_shiftcode" runat="server" Text='<%# Eval("shift_code") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="ddl_editshiftcode1" runat="server" 
                                                                    Text='<%# Eval("shift_code") %>'>
                                                                </asp:Label>
                                                            </EditItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Date" 
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <EditItemTemplate>
                                                                <asp:Label ID="txt_date_edit" runat="server" Height="21px" 
                                                                    Text='<%# Bind("dates","{0:dd/MM/yyyy}") %>' Width="50px"></asp:Label>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_date" runat="server" 
                                                                    Text='<%# Eval("dates","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Day" ItemStyle-HorizontalAlign="Left">
                                                            <EditItemTemplate>
                                                                <asp:Label ID="txt_day_edit" runat="server" Height="21px" Text='<%# Bind("days") %>' Width="50px"></asp:Label>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_day" runat="server" Text='<%# Eval("days") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="50px" 
                                                            HeaderText="In Time" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_starttime" runat="server" 
                                                                    Text='<%# Eval("intime") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txt_starttime_edit" runat="server" onkeyup="fn_time(event,this.id);" onFocus="this.select()"
                                                                    Text='<%# Bind("intime") %>' CssClass="form-control" Width="80px" MaxLength="5"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="50px" 
                                                            HeaderText="Late In" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_breaktimei" runat="server" 
                                                                    Text='<%# Eval("late_in") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txt_breaktimei_edit" runat="server" onkeyup="fn_time(event,this.id);" onFocus="this.select()"
                                                                    Text='<%# Bind("late_in") %>' CssClass="form-control" Width="80px" MaxLength="5"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="50px" 
                                                            HeaderText="Early Out" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_eout" runat="server" 
                                                                    Text='<%# Eval("early_out") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txt_eout_edit" runat="server" onkeyup="fn_time(event,this.id);" onFocus="this.select()"
                                                                    Text='<%# Bind("early_out") %>' CssClass="form-control" Width="80px" MaxLength="5"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="50px" 
                                                            HeaderText="Out Time" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_endtime" runat="server" 
                                                                    Text='<%# Eval("outtime") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txt_endtime_edit" runat="server" onkeyup="fn_time(event,this.id);" onFocus="this.select()"
                                                                    Text='<%# Bind("outtime") %>' CssClass="form-control" Width="80px" MaxLength="5"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="50px" 
                                                            HeaderText="Late Out" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txt_breaktimeo_edit" runat="server" onkeyup="fn_time(event,this.id);" onFocus="this.select()"
                                                                    Text='<%# Bind("Late_out") %>' CssClass="form-control" Width="80px" MaxLength="5"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_breaktimeo" runat="server" Text='<%# Eval("Late_out") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="OT Hrs" 
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txt_othrs_edit" Enabled="false" runat="server"  onFocus="this.select()"
                                                                    Text='<%# Bind("ot_hrs","{0:HH:mm}") %>' Width="50px"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_othrs" runat="server" 
                                                                    Text='<%# Eval("ot_hrs","{0:HH:mm}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Leave Name" 
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="ddl_leavename" CssClass="form-control" runat="server">
                                                                    <asp:ListItem Text="Select" Value="Select" />
                                                                    <asp:ListItem Text="Present" Value="Present" />
                                                                    <asp:ListItem Text="Absent" Value="Absent" />
                                                                    <asp:ListItem Text="First Half" Value="First Half" />
                                                                    <asp:ListItem Text="Second Half" Value="Second Half" />
                                                                    <asp:ListItem Text="Onduty" Value="Onduty" />
                                                                    <asp:ListItem Text="Leave" Value="Leave" />
                                                                    <asp:ListItem Text="Holiday" Value="Holiday" />
                                                                    <asp:ListItem Text="WeekOff" Value="WeekOff" />
                                                                    <asp:ListItem Text="Compoff" Value="Compoff" />
                                                                    <asp:ListItem Text="Tour" Value="Tour" />
                                                                </asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_leave" runat="server" Text='<%# Eval("leave_code") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Status" 
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="ddl_status" CssClass="form-control" runat="server">
                                                                    <asp:ListItem Text="Select" Value="Select" />
                                                                    <asp:ListItem Text="XX" Value="XX" />
                                                                    <asp:ListItem Text="AA" Value="AA" />
                                                                    <asp:ListItem Text="XA" Value="XA" />
                                                                    <asp:ListItem Text="AX" Value="AX" />
                                                                    <asp:ListItem Text="XL" Value="XL" />
                                                                    <asp:ListItem Text="LX" Value="LX" />
                                                                    <asp:ListItem Text="LL" Value="LL" />
                                                                    <asp:ListItem Text="OD" Value="DD" />
                                                                    <asp:ListItem Text="HH" Value="HH" />
                                                                    <asp:ListItem Text="WW" Value="WW" />
                                                                </asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_status" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowEditButton="True" ShowSelectButton="true" />
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <asp:Label ID="lblempty" runat="server" Text="No Records"></asp:Label>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                                </div>
                                                 <%--<input type="hidden" id="div_position" name="div_position" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="7" >
                                                &nbsp;</td>
                                        </tr>
                                    </tbody>
                                </table> 
                
                                </div>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                        </div>
                        
                    </div>

    </asp:Content>







