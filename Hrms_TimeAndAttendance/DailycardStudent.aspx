<%@ Page EnableEventValidation="false" Language="C#" MasterPageFile="~/HRMS.master"
    AutoEventWireup="true" CodeFile="DailycardStudent.aspx.cs" Inherits="Hrms_Employee_Default"
    Title="Welcome to HRMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    &nbsp;<script  language="javascript">
    function do_totals1() {
        document.all.pleasewaitScreen.style.visibility = "visible";
        window.setTimeout('do_totals2()', 1)
    }

    function do_totals2() {
        calc_totals();
        document.all.pleasewaitScreen.style.visibility = "hidden";
    }
</script><script type="text/css">
    .scrollingControlContainer
    {
        overflow-x: hidden;
        overflow-y: scroll;
    }
    </script><link href="../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" /><script type="text/javascript" language="javascript" src="../../Scripts/Datavalid.js"></script><script type="text/javascript">
        window.onload = function () {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lbl_Error.ClientID %>").innerHTML = "";
            }, seconds * 1000);
        };
</script><script language="javascript" type="text/javascript">
  
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
        alert("Sorry,Couldnt Delete Because it was assigned to Someone");
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
                    <h2 class="page-header">Students Time Card</h2>
                </div>
                <!-- /.col-lg-12 -->
            </div>

            <div class="panel panel-default">
                        <div class="panel-heading">
                            Time Card
                            <div class="pull-right">
                    <asp:DropDownList ID="ddl_branch" runat="server" class="form-control" AutoPostBack="True" onselectedindexchanged="ddl_branch_SelectedIndexChanged" style="margin-left: 12px" Width="130px">
                    </asp:DropDownList>
                            </div>
                            <asp:Timer ID="Timer1" runat="server" ontick="Timer1_Tick" Enabled="False">
                            </asp:Timer>
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>--%>
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                            <div class="divWaiting">
                            
                            <asp:Image ID="imgWait" runat="server" ImageAlign="Middle" 
                                    ImageUrl="~/Images/loading2.gif" Height="100px" Width="100px" />
                                <%--<img src="../loading.gif" alt="Loading" style="position:relative;" />--%>
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
                                                
                                                </span>
                                                <asp:DropDownList ID="ddl_type" runat="server" AutoPostBack="True" 
                                                    class="form-control" onselectedindexchanged="ddl_type_SelectedIndexChanged" 
                                                    Width="90%">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Daily</asp:ListItem>
                                                    <asp:ListItem>Hourly</asp:ListItem>
                                                    <asp:ListItem>View</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                    Class Hour</td>
                                            <td>
                                                <asp:DropDownList ID="ddl_hour" runat="server" 
                                                    class="form-control" onselectedindexchanged="ddl_type_SelectedIndexChanged" 
                                                    Width="90%">
                                                    <asp:ListItem Selected="True">Select</asp:ListItem>
                                                    <asp:ListItem>1</asp:ListItem>
                                                    <asp:ListItem>2</asp:ListItem>
                                                    <asp:ListItem>3</asp:ListItem>
                                                    <asp:ListItem>4</asp:ListItem>
                                                    <asp:ListItem>5</asp:ListItem>
                                                    <asp:ListItem>6</asp:ListItem>
                                                    <asp:ListItem>7</asp:ListItem>
                                                    <asp:ListItem>8</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                Total Hours</td>
                                            <td>

                                                <asp:TextBox ID="txt_tdays" runat="server" class="form-control"
                                                    Enabled="False" Width="60px"></asp:TextBox>
                                                </span></span></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 173px">
                                                Course</td>
                                            <td>
                                                <asp:DropDownList ID="ddl_course" runat="server" class="form-control" 
                                                    DataTextField="CourseName" DataValueField="CourseName" 
                                                    onselectedindexchanged="ddl_course_SelectedIndexChanged" Width="90%" 
                                                    AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                Class Name</td>
                                            <td>
                                                <asp:DropDownList ID="ddl_classname" runat="server" class="form-control" 
                                                    Width="90%">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                Present Hours</td>
                                            <td>
                                                <asp:TextBox ID="txt_pdays" runat="server" class="form-control"
                                                    Enabled="False" Width="60px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td >
                                                Department</td>
                                            <td>
                                                <asp:DropDownList ID="ddl_department" runat="server" class="form-control" 
                                                    DataSourceID="SqlDSDept" DataTextField="DepartmentName" 
                                                    DataValueField="DepartmentName" Width="90%">
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="SqlDSDept" runat="server" 
                                                    ConnectionString="<%$ ConnectionStrings:connectionstring %>" 
                                                    SelectCommand="SELECT [DepartmentName] FROM [Student_Department] WHERE ([pn_BranchID] = @pn_BranchID) order by pn_DepartmentID Asc">
                                                    <SelectParameters>
                                                        <asp:SessionParameter Name="pn_BranchID" SessionField="Login_Temp_BranchID" 
                                                            Type="Int32" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </td>
                                            <td>
                                                Register No</td>
                                            <td>
                                                <span style="color: #800000">
                                                <asp:DropDownList ID="ddl_ename" runat="server" class="form-control" 
                                                    Width="90%" Enabled="False">
                                                </asp:DropDownList>
                                                </span>
                                            </td>
                                            <td>
                                                Absent Hours</td>
                                            <td>
                                                <asp:TextBox ID="txt_adays" runat="server" class="form-control"
                                                    Enabled="False" Width="60px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Section</td>
                                            <td>
                                                <asp:DropDownList ID="ddl_Section" runat="server" class="form-control" 
                                                    Width="90%">
                                                    <asp:ListItem Selected="True">Select</asp:ListItem>
                                                    <asp:ListItem>A</asp:ListItem>
                                                    <asp:ListItem>B</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_from" runat="server" Text="Attendance Date"></asp:Label>
                                            </td>
                                            <td>
                                             <div style=" width:150px; float:left;">
                                                <asp:TextBox ID="txt_fromdate" runat="server" class="form-control" 
                                                    onkeyup="fn_date(event,this.id);" maxlength="10" Width="150px"/>                                                                                                   
                                                <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txt_fromdate" Format="dd/MM/yyyy" runat="server">
                                                </asp:CalendarExtender>
                                                </div>
                                                <div style=" width:25px; float:left;  margin-left:10px; margin-top:3px;">                                                
                                                    <asp:Image ID="Image1" runat="server"  
                                                        Text="" Width="25px" 
                                                        ImageUrl="~/Images/calendaricon.png" />
                                                </div>                                            

                                            </td>
                                            <td>
                                                <asp:Button ID="Btn_Sms" runat="server" Visible="False" class="btn btn-success"  
                                                    Text="Send SMS" onclick="Btn_Sms_Click" />
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Current Year</td>
                                            <td>
                                                <asp:DropDownList ID="ddl_currentyear" runat="server" AutoPostBack="True" 
                                                    class="form-control" 
                                                    onselectedindexchanged="ddl_currentyear_SelectedIndexChanged" Width="90%">
                                                    <asp:ListItem Selected="True">Select</asp:ListItem>
                                                    <asp:ListItem>1</asp:ListItem>
                                                    <asp:ListItem>2</asp:ListItem>
                                                    <asp:ListItem>3</asp:ListItem>
                                                    <asp:ListItem>4</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_to" runat="server" Text="Attendance To" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                            <div style=" width:150px; float:left;">
                                                <asp:TextBox ID="txt_todate" runat="server" class="form-control" Width="150px"  
                                                    onkeyup="fn_date(event,this.id);" Visible="False"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txt_todate" Format="dd/MM/yyyy">
                                                </asp:CalendarExtender>
                                                    </div>
                                                    <div style=" width:25px; float:left;  margin-left:10px; margin-top:3px;">                                                
                                                    <asp:Image ID="Image2" runat="server"  
                                                        Text="" Width="25px" 
                                                        ImageUrl="~/Images/calendaricon.png" Visible="False" />
                                                </div>  

                                            </td>
                                            <td>
                                                <asp:Button ID="Btn_import" runat="server" class="btn btn-warning" 
                                                    onclick="Btn_import_Click" Text="Import"/>
                                                <asp:Button ID="Btn_Generate" runat="server" class="btn btn-info"  
                                                    Text="Generate" onclick="Btn_Generate_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="Btn_view" runat="server" class="btn btn-info" 
                                                    onclick="Btn_view_Click" Text="View" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" >
                    
                                                
                                                <div id="div1" runat="server" style="overflow: auto; height: 500px;">
                                                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" CssClass="table table-hover table-striped"
                                                    AutoGenerateColumns="False" 
                                                    onrowcancelingedit="GridView1_RowCancelingEdit" 
                                                    onrowcommand="GridView1_RowCommand" onrowdatabound="GridView1_RowDataBound" 
                                                    onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing" 
                                                    onrowupdating="GridView1_RowUpdating">
                                                        
                                                   
                                                    <Columns>

                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Register No" 
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <EditItemTemplate>
                                                                <asp:Label ID="txt_empcode_edit" runat="server" Height="21px" 
                                                                    Text='<%# Bind("emp_code") %>' Width="50px"></asp:Label>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_empcode" runat="server" Text='<%# Eval("RegisterNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Student Name" 
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_empname" runat="server" Text='<%# Eval("StudentName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="txt_empname_edit" runat="server" Height="21px" 
                                                                    Text='<%# Bind("emp_name") %>' Width="50px"></asp:Label>
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
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Day" 
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <EditItemTemplate>
                                                                <asp:Label ID="txt_day_edit" runat="server" Height="21px" 
                                                                    Text='<%# Bind("days") %>' Width="50px"></asp:Label>
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
                                                                <asp:TextBox ID="txt_starttime_edit" runat="server" 
                                                                    Text='<%# Bind("intime") %>' Width="50px"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="50px" 
                                                            HeaderText="Late (In)" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_breaktimei" runat="server" 
                                                                    Text='<%# Eval("late_in") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txt_breaktimei_edit" runat="server" 
                                                                    Text='<%# Bind("late_in") %>' Width="50px"></asp:TextBox>
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
                                                                <asp:TextBox ID="txt_endtime_edit" runat="server" 
                                                                    Text='<%# Bind("outtime") %>' Width="50px"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="50px" 
                                                            HeaderText="late (Out)" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txt_breaktimeo_edit" runat="server" 
                                                                    Text='<%# Bind("late_out") %>' Width="50px"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_breaktimeo" runat="server" 
                                                                    Text='<%# Eval("late_out") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                        </asp:TemplateField>
                                                        <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="OT Hrs" 
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txt_othrs_edit" runat="server" 
                                                                    Text='<%# Bind("ot_hrs") %>' Width="50px"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_othrs" runat="server" 
                                                                    Text='<%# Eval("ot_hrs") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Leave Name" 
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="ddl_leavename" runat="server">
                                                                    <asp:ListItem Text="Select" Value="Select" />
                                                                    <asp:ListItem Text="Present" Value="Present" />
                                                                    <asp:ListItem Text="Absent" Value="Absent" />
                                                                    <asp:ListItem Text="First Half" Value="First Half" />
                                                                    <asp:ListItem Text="Second Half" Value="Second Half" />
                                                                    <asp:ListItem Text="Onduty" Value="Onduty" />
                                                                    <asp:ListItem Text="Holiday" Value="Holiday" />
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
                                                                <asp:DropDownList ID="ddl_status" selectedvalue='<%# Eval("status") %>' runat="server">
                                                                    <asp:ListItem Text="Select" Value="Select" />
                                                                    <asp:ListItem Text="XX" Value="XX" />
                                                                    <asp:ListItem Text="AA" Value="AA" />
                                                                    <asp:ListItem Text="XA" Value="XA" />
                                                                    <asp:ListItem Text="AX" Value="AX" />
                                                                    <asp:ListItem Text="OD" Value="OD" />
                                                                    <asp:ListItem Text="HH" Value="HH" />
                                                                    <asp:ListItem Text="CC" Value="CC" />
                                                                    <asp:ListItem Text="TT" Value="TT" />
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
                                                    <br />
                                                    <asp:GridView ID="GridHour" runat="server" AllowSorting="True" 
                                                        AutoGenerateColumns="False" CssClass="table table-hover table-striped" 
                                                        >
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Register No" 
                                                                ItemStyle-HorizontalAlign="Left">
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="txt_empcode_edit0" runat="server" Height="21px" 
                                                                        Text='<%# Bind("emp_code") %>' Width="50px"></asp:Label>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_empcode0" runat="server" Text='<%# Eval("RegisterNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Student Name" 
                                                                ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_empname0" runat="server" Text='<%# Eval("StudentName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="txt_empname_edit0" runat="server" Height="21px" 
                                                                        Text='<%# Bind("emp_name") %>' Width="50px"></asp:Label>
                                                                </EditItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Shift Code" 
                                                                ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_shiftcode0" runat="server" Text='<%# Eval("shift_code") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="ddl_editshiftcode2" runat="server" 
                                                                        Text='<%# Eval("shift_code") %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                </asp:Label>
                                                                </EditItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Date" 
                                                                ItemStyle-HorizontalAlign="Left">
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="txt_date_edit0" runat="server" Height="21px" 
                                                                        Text='<%# Bind("dates","{0:dd/MM/yyyy}") %>' Width="50px"></asp:Label>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_date0" runat="server" 
                                                                        Text='<%# Eval("dates","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Day" 
                                                                ItemStyle-HorizontalAlign="Left">
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="txt_day_edit0" runat="server" Height="21px" 
                                                                        Text='<%# Bind("days") %>' Width="50px"></asp:Label>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_day0" runat="server" Text='<%# Eval("days") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Status" 
                                                                ItemStyle-HorizontalAlign="Left">
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddl_status0" runat="server">
                                                                        <asp:ListItem Text="Select" Value="Select" />
                                                                        <asp:ListItem Text="XX" Value="XX" />
                                                                        <asp:ListItem Text="AA" Value="AA" />
                                                                        <asp:ListItem Text="XA" Value="XA" />
                                                                        <asp:ListItem Text="AX" Value="AX" />
                                                                        <asp:ListItem Text="OD" Value="OD" />
                                                                        <asp:ListItem Text="HH" Value="HH" />
                                                                        <asp:ListItem Text="CC" Value="CC" />
                                                                        <asp:ListItem Text="TT" Value="TT" />
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="lbl_status0" runat="server">
                                                                        <asp:ListItem Text="XX" Value="XX" />
                                                                        <asp:ListItem Text="AA" Value="AA" />
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Button ID="btn_saveall"  runat="server" class="btn btn-success"  
                                                                        Text="Save All" onclick="btn_saveall_Click" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton id="img_update"  runat="server"  AlternateText="" class="btn btn-info btn-circle" CommandName="Update"><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="6">
                        <asp:Label ID="lbl_Error" runat="server" ForeColor="Red" Font-Bold="True" Font-Names="Calibri" 
                                                    CssClass="Error" Font-Size="Small" Height="16px" ></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table> 
                
                            </div>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        
                    </div>

    </asp:Content>







