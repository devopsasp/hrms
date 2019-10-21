<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Task_Scheduler1.aspx.cs" Inherits="Hrms_Tasks_Default" Title="Task Scheduler" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <link href="../Css/sample.css" type="text/css" rel="stylesheet" />
  <%--  <link href="../Css/toastr.css" type="text/css" rel="stylesheet" />--%>
   <%-- <link href="../Css/toastrsample.css" type="text/css" rel="stylesheet" />
    <script src="../js/jquery-3.1.1.min.js" type="text/javascript"></script>
    <script src="../js/toastr.js" type="text/javascript"></script>
    <script src="../js/toastrsample.js" type="text/javascript"></script>--%>
    <script language="javascript" type="text/javascript">
        
        function testing () 
        {
        
                
                toastr.options =
                  {
                      "debug": false,
                      "positionClass": "toast-top-left",
                      "onclick": null,
                      "fadeIn": 300,
                      "fadeOut": 100,
                      "timeOut": 3000,
                      "extendedTimeOut": 1000
                  }

                var d = Date();
                toastr["success"](d,"Current Day & Time");
            }

    function validate()
    {
        var r=confirm("Are you sure you want to delete this record?");
        if (r==true)
        {
        return true;
        }
        else
        {
        return false;
        }
    }
    function fn_len(event, id)
    {
        var value, len;
        value = document.getElementById(id).value;
        len = value.length;
        if (id == "TextBox1")
        {
            if (len > 20) {
                document.getElementById(txtid).value = txtvalue;
                alert("maxlimit reached");
            }
        }
        if (id == "TextBox2")
        {
            if (len > 35) {
                document.getElementById(txtid).value = txtvalue;
                alert("maxlimit reached");
            }
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
     window.onload = function () {
         
         var div = document.getElementById("div1");
         var div_position = document.getElementById("div_position");
         var position = parseInt('<%=Request.Form["div_position"] %>');
    if (isNaN(position)) {
        position = 0;
    }
    div.scrollTop = position;
    div.onscroll = function () {
        alert("hello");
        div_position.value = div.scrollTop;
    };
        };
    </script>
<style type="text/css">
    .HeaderFreez
{
   position:relative ;
   top:expression(this.offsetParent.scrollTop);
   z-index: 10;
}
    .rateStar
    {
	    white-space:nowrap;
	    margin:0em;
	    height:14px;
	    
    }
    .rateItem 
    {
        font-size: 0pt;
        width: 13px;
        height: 12px;
        margin: 0px;
        padding: 0px;
        display: block;
        
        background-repeat: no-repeat;
	    cursor:pointer;
    }
    .FillStar
    {
        background-image: url(ratingfilled.png);
    }
    .EmptyStar 
    {
        background-image: url(ratingempty.png);
    }
    .SaveStar
    {
        background-image: url(ratingsaved.png);
    }
          </style>


    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                            <div class="divWaiting">
                            
                            <asp:Image ID="imgWait" runat="server" ImageAlign="Middle" ImageUrl="~/Images/loading2.gif" Height="100px" Width="100px" />
                                <%--<img src="../loading.gif" alt="Loading" style="position:relative;" />--%>
                            </div>
                            </ProgressTemplate>
                            </asp:UpdateProgress>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>

    <div ><h3 class="page-header">Task Scheduler</h3></div>
                     <div><span class="Title">
                         <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                             ConnectionString="<%$ ConnectionStrings:connectionstring %>" 
                             SelectCommand="select Employee_First_Name from paym_employee where pn_BranchID=@pn_BranchID ">
                             <SelectParameters>
                                 <asp:Parameter Name="pn_BranchID" />
                             </SelectParameters>
                         </asp:SqlDataSource>
                         <asp:SqlDataSource ID="SqlDataSource2" runat="server"></asp:SqlDataSource>
                         </span></div>
             
              <nav style="display:inline-flexbox;" >
                  <%--<ul style="list-style:none;">
                   <li style="float:left;"><asp:RadioButton ID="rdobtn1" runat="server" GroupName="taskset" Text="Task Assigned by me" AutoPostBack="true" OnCheckedChanged="rdobtn1_CheckedChanged"/></li>
                   <li><asp:RadioButton ID="rdobtn2" runat="server" GroupName="taskset" Text="Task Assigned to me" AutoPostBack="true" OnCheckedChanged="rdobtn2_CheckedChanged"/> </li>
                </ul>--%>
                  <div>
                      <ul style="list-style:none;">
                         <li  style="float:right;padding-left:10px;"> <asp:ImageButton ID="intasks" runat="server" ImageUrl="~/Images/taskgiven.jpg" OnClick="intasks_Click" ToolTip="tasks you assigned" /></li>
                           <li  style="float:right;padding-left:10px;"><asp:ImageButton ID="outtasks" runat="server" ImageUrl="~/Images/mytask.png" OnClick="outtasks_Click" ToolTip="tasks to do"/> </li>            
                        </ul>
                    </div>
                  <ul style=" list-style:none;">
                     
                            <li  style="float:right;padding-left:10px;">  </li>                   
                           <li style="float:right;padding-left:10px;"> <asp:DropDownList ID="filterdata" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="filterdata_SelectedIndexChanged">
                                     <asp:ListItem>Select</asp:ListItem>
                                 </asp:DropDownList></li>     
                  
                          <li style="float:right; padding-left:10px;">  
                              <asp:DropDownList ID="filterid" runat="server" CssClass="form-control"  OnSelectedIndexChanged="filterid_SelectedIndexChanged" AutoPostBack="True">
                                    <%-- <asp:ListItem>Select</asp:ListItem>
                                     <asp:ListItem>TaskTitle</asp:ListItem>
                                     <asp:ListItem>Assigned</asp:ListItem>
                                     <asp:ListItem>DOA</asp:ListItem>
                                     <asp:ListItem>Status</asp:ListItem>
                                     <asp:ListItem>Priority</asp:ListItem>
                                     <asp:ListItem>TaskLevel</asp:ListItem>
                                     <asp:ListItem>DueDate</asp:ListItem>
                                      <asp:ListItem>Refresh</asp:ListItem>--%>
                                 </asp:DropDownList></li>
                   
                         <li style="float:right;padding-left:10px;"><asp:Label runat="server" id="lblfilter">Filter:</asp:Label></li>    
                       
                        
                       </ul>
                 </nav>
               
                    <table style="width: 100%">
                       <tr valign="top">
                         <td>
                             <span class="Title" 
                                 style="font-size: small; font-family: Calibri; text-align: right;">D.O.A. - 
                             Date Of Assignment&nbsp; ,&nbsp; D.O.C. - Date Of Completion&nbsp; ,&nbsp; R.D.O.C. - Revised Date 
                             Of Completion</span></td>
                       </tr>
                        <tr valign="top">
                             <td >
                                <div id="div1" runat="server" style="overflow: scroll ; width:1000px; height:300px;">
                                  <%--<input type="text" id="div_position" name="div_position" />--%>
                                 <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" 
                                     class="table table-striped table-bordered table-hover" GridLines="None" HorizontalAlign="Center" 
                                     onrowcancelingedit="GridView1_RowCancelingEdit" onrowcommand="GridView1_RowCommand" onrowdatabound="GridView1_RowDataBound" 
                                     onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing" onrowupdating="GridView1_RowUpdating" 
                                     onselectedindexchanged="GridView1_SelectedIndexChanged" ShowFooter="True" Width="100%" OnSorting="GridView1_Sorting1">
                                      <HeaderStyle CssClass="HeaderFreez" /> 
                                     <Columns>
                                         <asp:TemplateField>
                                             <HeaderTemplate>
                                                 S.No.
                                             </HeaderTemplate>
                                             <ItemTemplate>
                                                 <asp:Label ID="lblSRNO0" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField visible="False">
                                             <HeaderTemplate>
                                                 task id
                                             </HeaderTemplate>
                                             <ItemTemplate>
                                                 <asp:Label ID="lbltaskid" runat="server" Text='<%# Eval("TaskID") %>'></asp:Label>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Reference Number">
                                             <ItemTemplate>
                                                 <asp:Label ID="Lblref" runat="server" Text='<%# Eval("Reference_No") %>'></asp:Label>
                                             </ItemTemplate>
                                             <EditItemTemplate>

                                                 <asp:Label ID="refedit"  runat="server" Text='<%# Bind("Reference_No") %>'></asp:Label>
                                             </EditItemTemplate>
                                             <FooterTemplate>
                                                 <asp:TextBox ID="TextBoxref" CssClass="form-control" runat="server" Width="100px"></asp:TextBox>
                                             </FooterTemplate>
                                             <HeaderStyle HorizontalAlign="Left" />
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Task Title">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label1" runat="server" Text='<%# Eval("TaskTitle") %>'></asp:Label>
                                             </ItemTemplate>
                                             <EditItemTemplate>
                                                 <asp:Label ID="Tsubedit"  runat="server" Text='<%# Bind("TaskTitle") %>'></asp:Label>
                                             </EditItemTemplate>
                                             <FooterTemplate>
                                                 <asp:TextBox MaxLength="20" ID="TextBox1" onkeyup="fn_len(event,id)" CssClass="form-control" runat="server" Width="100px" k></asp:TextBox>
                                             </FooterTemplate>
                                             <HeaderStyle HorizontalAlign="Left" />
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Description">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label16" runat="server" Text='<%# Eval("TDescription") %>'></asp:Label>
                                             </ItemTemplate>
                                             <EditItemTemplate>
                                                 <asp:Label ID="Tdesedit" runat="server" Text='<%# Bind("TDescription") %>' 
                                                     Width="187px"></asp:Label>
                                             </EditItemTemplate>
                                             <FooterTemplate>
                                                 <asp:TextBox ID="TextBox2" CssClass="form-control" runat="server"></asp:TextBox>
                                             </FooterTemplate>
                                             <HeaderStyle HorizontalAlign="Left" />
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="D.O.A." SortExpression="DOA">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label3" runat="server" Text='<%# Eval("DOA") %>'></asp:Label>
                                             </ItemTemplate>
                                             <EditItemTemplate>
                                                 <asp:Label ID="DOAedit" runat="server" MaxLength="10"
                                                   Width="100px"  onkeyup="fn_date(event,this.id);" Text='<%# Bind("DOA") %>'></asp:Label>
                                                 <%--<asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                                                     TargetControlID="DOAedit" TodaysDateFormat="d MMMM, yyyy" />--%>
                                             </EditItemTemplate>
                                             <FooterTemplate>
                                                 <asp:TextBox ID="TextBox3" runat="server" MaxLength="10" Width="100px"
                                                     onkeyup="fn_date(event,this.id);" CssClass="form-control" OnTextChanged="TextBox3_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                 <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                                                     TargetControlID="TextBox3" TodaysDateFormat="d MMMM, yyyy" />
                                             </FooterTemplate>
                                             <HeaderStyle HorizontalAlign="Left" />
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <%--<asp:TemplateField HeaderText="Department">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label11" runat="server" Text='<%# Eval("Department") %>'></asp:Label>
                                             </ItemTemplate>
                                             <EditItemTemplate>
                                                 <asp:Label ID="deptedit" runat="server" Text='<%# Bind("Department") %>'></asp:Label>
                                                 
                                             </EditItemTemplate>
                                             <FooterTemplate>
                                                 <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True" 
                                                     CssClass="form-control" onselectedindexchanged="DropDownList4_SelectedIndexChanged">
                                                     <asp:ListItem>Select</asp:ListItem>
                                                 </asp:DropDownList>
                                             </FooterTemplate>
                                             <HeaderStyle HorizontalAlign="Left" />
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>--%>
                                         <asp:TemplateField HeaderText="Assigned to">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label12" runat="server" Text='<%# Eval("Assigned") %>'></asp:Label>
                                             </ItemTemplate>
                                             <EditItemTemplate>
                                                   <asp:Label ID="assiedit" runat="server" Text='<%# Bind("Assigned") %>'></asp:Label>
                                                 <%--<asp:DropDownList ID="assiedit" runat="server" CssClass="form-control" DataSourceID="SqlDataSourceemp" 
                                                     DataTextField="Employee_First_Name" DataValueField="Employee_First_Name">
                                                 </asp:DropDownList>
                                                 <asp:SqlDataSource ID="SqlDataSourceemp" runat="server" 
                                                     ConnectionString="<%$ ConnectionStrings:connectionstring %>" 
                                                     SelectCommand="SELECT [Employee_First_Name] FROM [paym_Employee] WHERE ([pn_BranchID] = @pn_BranchID)">
                                                     <SelectParameters>
                                                         <asp:SessionParameter Name="pn_BranchID" SessionField="Login_Temp_BranchID" 
                                                             Type="Int32" />
                                                     </SelectParameters>
                                                 </asp:SqlDataSource>--%>
                                             </EditItemTemplate>
                                             <FooterTemplate>
                                                 <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                                                     <asp:ListItem>Select</asp:ListItem>
                                                 </asp:DropDownList>
                                             </FooterTemplate>
                                             <HeaderStyle HorizontalAlign="Left" />
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Priority">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label5" runat="server" Text='<%# Eval("Priority") %>'></asp:Label>
                                             </ItemTemplate>
                                             <EditItemTemplate>
                                                   <asp:Label ID="prioredit" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                                                 <%--<asp:DropDownList ID="prioredit" runat="server" DataTextField="Prior" 
                                                     DataValueField="Prior" CssClass="form-control">
                                                     <asp:ListItem>Select</asp:ListItem>
                                                     <asp:ListItem>Low</asp:ListItem>
                                                     <asp:ListItem>Medium</asp:ListItem>
                                                     <asp:ListItem>High</asp:ListItem>
                                                 </asp:DropDownList>--%>
                                             </EditItemTemplate>
                                             <FooterTemplate>
                                                 <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control">
                                                     <asp:ListItem>Select</asp:ListItem>
                                                     <asp:ListItem>Low</asp:ListItem>
                                                     <asp:ListItem>Medium</asp:ListItem>
                                                     <asp:ListItem>High</asp:ListItem>
                                                 </asp:DropDownList>
                                             </FooterTemplate>
                                             <HeaderStyle HorizontalAlign="Left" />
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="level">
                                             <ItemTemplate>
                                                 <asp:Label ID="LabelLevel" runat="server" Text='<%# Eval("TaskLevel") %>'></asp:Label>
                                             </ItemTemplate>
                                             <EditItemTemplate>
                                                   <asp:Label ID="leveledit" runat="server" Text='<%# Bind("Tasklevel") %>'></asp:Label>
                                                 <%--<asp:DropDownList ID="leveledit" runat="server" DataTextField="Tlevel" 
                                                     DataValueField="Tlevel" CssClass="form-control">
                                                     <asp:ListItem>Select</asp:ListItem>
                                                     <asp:ListItem>Easy</asp:ListItem>
                                                     <asp:ListItem>Medium</asp:ListItem>
                                                     <asp:ListItem>Difficult</asp:ListItem>
                                                 </asp:DropDownList>--%>
                                             </EditItemTemplate>
                                             <FooterTemplate>
                                                 <asp:DropDownList ID="DropDownListLevel" runat="server" CssClass="form-control">
                                                     <asp:ListItem>Select</asp:ListItem>
                                                     <asp:ListItem>Easy</asp:ListItem>
                                                     <asp:ListItem>Medium</asp:ListItem>
                                                     <asp:ListItem>Difficult</asp:ListItem>
                                                 </asp:DropDownList>
                                             </FooterTemplate>
                                             <HeaderStyle HorizontalAlign="Left" />
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Status">
                                             <%--<HeaderTemplate>
                                                 Status
                                                 <asp:DropDownList ID="headstatus" runat="server" CssClass="form-control">
                                                     <asp:ListItem>Select</asp:ListItem>
                                                     <asp:ListItem>New</asp:ListItem>
                                                     <asp:ListItem>On-process</asp:ListItem>
                                                     <asp:ListItem>Completed</asp:ListItem>
                                                     <asp:ListItem>Re-open</asp:ListItem>
                                                 </asp:DropDownList>
                                             </HeaderTemplate>--%>
                                             <ItemTemplate>
                                                 <asp:Label ID="Label13" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                             </ItemTemplate>
                                           <EditItemTemplate>
                                             <%--<InsertItemTemplate>--%>
                                                <asp:Label ID="lblStatedit" Visible="false" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                 <asp:DropDownList ID="statedit" runat="server" DataTextField="Stat" 
                                                     DataValueField="Stat" CssClass="form-control">
                                                  
                                                  </asp:DropDownList>   
                                             <%--</InsertItemTemplate>--%>
                                                 
                                                 
                                             </EditItemTemplate>
                                             <FooterTemplate>
                                                 <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control">
                                                     <asp:ListItem>Select</asp:ListItem>
                                                     <asp:ListItem>New</asp:ListItem>
                                                     <%--<asp:ListItem>Hold</asp:ListItem>
                                                     <asp:ListItem>On Process</asp:ListItem>
                                                     <asp:ListItem>Completed</asp:ListItem>--%>
                                                     
                                                 </asp:DropDownList>
                                             </FooterTemplate>
                                             <HeaderStyle HorizontalAlign="Left" />
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Due Date" SortExpression="duedate">
                                             <ItemTemplate>
                                                 <asp:Label ID="Labeldue" runat ="server" Text='<%# Eval("duedate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                             </ItemTemplate>
                                             <%--<EditItemTemplate>
                                                 <asp:TextBox ID="DueEdit" runat="server" MaxLength="10" Width="100px"
                                                     onkeyup="fn_date(event,this.id);" Text='<%# Bind("duedate") %>' CssClass="form-control"></asp:TextBox>
                                                 <asp:CalendarExtender ID="CalendarExtenderdue" runat="server" Format="dd/MM/yyyy" 
                                                     TargetControlID="DueEdit" TodaysDateFormat="d MMMM, yyyy" />
                                             </EditItemTemplate>--%>
                                             <FooterTemplate>
                                                 <asp:TextBox ID="TextBoxdue" runat="server" MaxLength="10" CssClass="form-control"
                                                   Width="100px"  onkeyup="fn_date(event,this.id);" AutoPostBack="True" OnTextChanged="TextBoxdue_TextChanged"></asp:TextBox>
                                                 <asp:CalendarExtender ID="CalendarExtenderduef" runat="server" Format="dd/MM/yyyy" 
                                                     TargetControlID="TextBoxdue" TodaysDateFormat="d MMMM, yyyy" />
                                             </FooterTemplate>
                                             <HeaderStyle HorizontalAlign="Left" />
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Comment">
                                             <ItemTemplate>
                                                 <asp:Label ID="Labelcmnt" runat="server" Text='<%# Eval("Comments") %>'></asp:Label>
                                             </ItemTemplate>
                                             <EditItemTemplate>
                                                 <asp:TextBox ID="Cmntedit" CssClass="form-control" runat="server"></asp:TextBox>
                                                
                                            </EditItemTemplate>
                                             
                                             <ControlStyle Width="125px"></ControlStyle>
                                             <HeaderStyle HorizontalAlign="Left" />
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="F.D.O.C.">
                                             <ItemTemplate>
                                                 <asp:Label ID="Labelfdoc" runat="server" Text='<%# Eval("FDOC") %>'></asp:Label>
                                             </ItemTemplate>
                                             <EditItemTemplate>
                                                 <asp:TextBox ID="FDOCedit" runat="server" MaxLength="10" Width="100px"
                                                     onkeyup="fn_date(event,this.id);" Text='<%# Bind("FDOC") %>' CssClass="form-control"></asp:TextBox>
                                                 <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" 
                                                     TargetControlID="FDOCedit" TodaysDateFormat="d MMMM, yyyy" />
                                             </EditItemTemplate>
                                             <FooterTemplate>
                                                 <asp:Button ID="Button1" runat="server" CommandName="add" Font-Bold="True" 
                                                     CssClass="btn btn-success"
                                                     Text="ADD" OnClientClick="javascript:testing()" />
                                             </FooterTemplate>
                                             <HeaderStyle HorizontalAlign="Left" />
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                          
                                          
                                         <asp:CommandField ShowDeleteButton="True" ShowSelectButton="True" SelectImageUrl="~/Images/history.jpg" ButtonType="Image" EditImageUrl="~/Images/edit_icon.png" CancelImageUrl="~/Images/delete_icon.jpg" UpdateImageUrl="~/Images/save_icon.jpg" DeleteImageUrl="~/Images/delete_icon.jpg" ShowEditButton="True" >
                                         <ItemStyle Wrap="False" />
                                         </asp:CommandField>
                                     </Columns>
                                     <PagerStyle HorizontalAlign="Center" />
                                     <EmptyDataTemplate>
                                         <asp:Label ID="lblempty" runat="server" Text="No Records">
                    </asp:Label>
                                     </EmptyDataTemplate>
                                 
                                 </asp:GridView>
                                   </div>
                                <asp:GridView ID="grd_view1" runat="server" AllowSorting="True" 
                                 AutoGenerateColumns="False"
                                 class="table table-striped table-bordered table-hover" 
                                 CssClass="table table-striped table-bordered table-hover"
                                  GridLines="None" 
                                 onrowcancelingedit="grd_view_RowCancelingEdit" 
                                 onrowediting="grd_view_RowEditing" onrowupdating="grd_view_RowUpdating" OnRowDataBound="grd_view_RowDataBound" >
                                 <FooterStyle Font-Bold="True" />
                                 <RowStyle />
                                 <Columns>
                                     <asp:TemplateField visible="false">
                                             <HeaderTemplate>
                                                 task id
                                             </HeaderTemplate>
                                             <ItemTemplate>
                                                 <asp:Label ID="clbltaskid" runat="server" Text='<%# Eval("TaskID") %>'></asp:Label>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Reference Number">
                                             <ItemTemplate>
                                                 <asp:Label ID="cLblref" runat="server" Text='<%# Eval("Reference_No") %>'></asp:Label>
                                             </ItemTemplate>
                                             
                                             <HeaderStyle HorizontalAlign="Left" />
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Task Title" ItemStyle-HorizontalAlign="Center">
                                         <ItemTemplate>
                                             <asp:Label ID="cLabel1" runat="server" Text='<%# Eval("TaskTitle") %>'></asp:Label>
                                         </ItemTemplate>
                                         
                                         <ItemStyle HorizontalAlign="Left" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="Left">
                                         <ItemTemplate>
                                             <asp:Label ID="cLabel2" runat="server" Text='<%# Eval("TDescription") %>'></asp:Label>
                                         </ItemTemplate>
                                         
                                         <ItemStyle HorizontalAlign="Left" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="D.O.A." ItemStyle-HorizontalAlign="Center">
                                         <ItemTemplate>
                                             <asp:Label ID="cLabel3" runat="server" Text='<%# Eval("DOA") %>'></asp:Label>
                                         </ItemTemplate>
                                         
                                         <ItemStyle HorizontalAlign="Left" />
                                     </asp:TemplateField>
                                     
                                     <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                         <ItemTemplate>
                                             <asp:Label ID="cLabel7" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                         </ItemTemplate>
                                         <EditItemTemplate>
                                             <asp:Label ID="lblStatedit" Visible="false" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                             <asp:DropDownList ID="DropDownStat" runat="server" CssClass="form-control">
                                                    
                                                 </asp:DropDownList>
                                         </EditItemTemplate>
                                         <ItemStyle HorizontalAlign="Left" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Remarks(Optional)" 
                                         ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50">
                                         <ItemTemplate>
                                             <asp:Label ID="cLabel8" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
                                         </ItemTemplate>
                                         <EditItemTemplate>
                                             <asp:TextBox ID="remedit" runat="server" Text='<%# Bind("Remarks") %>'></asp:TextBox>
                                         </EditItemTemplate>
                                         <ControlStyle Width="125px" />
                                         <ItemStyle HorizontalAlign="Left" />
                                     </asp:TemplateField>
                                     
                                     <asp:TemplateField ControlStyle-BackColor="Aqua" HeaderText="Comments" 
                                         ItemStyle-HorizontalAlign="Center">
                                         <ItemTemplate>
                                             <asp:Label ID="cLabel10" runat="server" Text='<%# Eval("Comments") %>'></asp:Label>
                                         </ItemTemplate>
                                         
                                         <ControlStyle BackColor="Aqua" />
                                         <ItemStyle HorizontalAlign="Left" />
                                     </asp:TemplateField>
                                     <%--<asp:TemplateField HeaderText="Rating" ItemStyle-HorizontalAlign="Center">
                                         <ItemTemplate>
                                             <asp:Rating ID="Rating1" runat="server" AutoPostBack="false" 
                                                 CssClass="rateStar" 
                                                 CurrentRating='<%# String.IsNullOrEmpty(Eval("rating").ToString())?0:Eval("rating") %>' 
                                                 Direction="LeftToRight" EmptyStarCssClass="EmptyStar" 
                                                 FilledStarCssClass="FillStar" Height="16px" MaxRating="10" 
                                                 StarCssClass="rateItem" WaitingStarCssClass="SaveStar" Width="142px">
                                             </asp:Rating>
                                         </ItemTemplate>
                                         <ItemStyle HorizontalAlign="Center" />
                                     </asp:TemplateField>--%>
                                 <asp:CommandField ButtonType="Image" ShowEditButton="true" ShowCancelButton="true" UpdateImageUrl="~/Images/save_icon.jpg" EditImageUrl="~/Images/edit_icon.png" CancelImageUrl="~/Images/delete_icon.jpg" />
                                 </Columns>
                                 <PagerStyle HorizontalAlign="Center" />
                                 <SelectedRowStyle Font-Bold="True" />
                                 <HeaderStyle Font-Bold="True" />
                                 <EditRowStyle />
                                 <AlternatingRowStyle />
                                 <EmptyDataTemplate> 
                                     <asp:Label ID="clblempty" runat="server" Text="No Records">
            </asp:Label>
                                 </EmptyDataTemplate>
                             </asp:GridView>
                              
                                
                             </td>
                         </tr>
                         <tr valign="top">
                             <td>
                                 <asp:Button ID="Button5" runat="server" class="btn btn-success" onclick="Button5_Click" Text="Completed Tasks" />
                                 <asp:Button ID="Button2" runat="server" class="btn btn-info" onclick="Button2_Click" Text="View Notifications" />
                                 <asp:Button ID="Button4" runat="server" class="btn btn-success" onclick="Button4_Click" Text="Completed Tasks" />
                                 <asp:Button ID="Button3" runat="server" class="btn btn-info" onclick="Button3_Click" Text="Show Log"  />
                                 <%-- <asp:Button ID="Button6" runat="server" class="btn btn-info"  OnClientClick="testing()" Text="test1" />--%>
                             </td>
                         </tr>
                  </table>
                    
                  <div style="overflow-x:auto;width:990px">
                     <table style="width: 100%">
                         <tr valign="top">
                             <td >
                                
                                 <asp:GridView ID="GridView2" runat="server" AllowSorting="True" 
                                     AutoGenerateColumns="False" 
                                     class="table table-striped table-bordered table-hover" 
                                      GridLines="None" HorizontalAlign="Center"
                                     onrowcancelingedit="GridView2_RowCancelingEdit" 
                                     onrowcommand="GridView2_RowCommand" 
                                     onrowdeleting="GridView2_RowDeleting" onrowediting="GridView2_RowEditing" 
                                     onrowupdating="GridView2_RowUpdating" 
                                     onselectedindexchanged="GridView1_SelectedIndexChanged" ShowFooter="True" >
                                    
                                     <Columns>
                                         <asp:TemplateField>
                                             <HeaderTemplate>
                                                 S.No.
                                             </HeaderTemplate>
                                             <ItemTemplate>
                                                 <asp:Label ID="lblSRNO1" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Reference Number">
                                             <ItemTemplate>
                                                 <asp:Label ID="Lblref0" runat="server" Text='<%# Eval("Reference_No") %>'></asp:Label>
                                             </ItemTemplate>
                                             <EditItemTemplate>
                                                 <asp:Label ID="refedit0"  runat="server" Text='<%# Bind("Reference_No") %>'></asp:Label>
                                             </EditItemTemplate>
                                             
                                             <HeaderStyle HorizontalAlign="Left" />
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Task Title" 
                                             ItemStyle-HorizontalAlign="Center">
                                             <ItemTemplate>
                                                 <asp:Label ID="Labtask0" runat="server" Text='<%# Eval("TaskTitle") %>'></asp:Label>
                                             </ItemTemplate>
                                             <EditItemTemplate>
                                                 <asp:Label ID="Labelsubedit0" runat="server" 
                                                     Text='<%# Bind("TaskTitle") %>' ></asp:Label>
                                             </EditItemTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="D.O.C" 
                                             ItemStyle-HorizontalAlign="Center">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label18" runat="server" Text='<%# Eval("DOC") %>'></asp:Label>
                                             </ItemTemplate>
                                             <EditItemTemplate>
                                                 <asp:Label ID="Labeledit" runat="server" Text='<%# Bind("DOC") %>' Width="74px"></asp:Label>
                                             </EditItemTemplate>
                                             <%--<ControlStyle Width="125px"></ControlStyle>--%>
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Assigned to" 
                                             ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label17" runat="server" Text='<%# Eval("Assigned") %>'></asp:Label>
                                             </ItemTemplate>
                                             <EditItemTemplate>
                                                 <asp:Label ID="Labelassi" runat="server"
                                                     Text='<%# Bind("Assigned") %>' ></asp:Label>
                                             </EditItemTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Remarks" 
                                             ItemStyle-HorizontalAlign="Center">
                                             <ItemTemplate>
                                                 <asp:CheckBox ID="Check_yes" runat="server" Text="Yes/No" />
                                             </ItemTemplate>
                                             <EditItemTemplate>
                                                 <asp:CheckBox ID="Check_yes1" runat="server" Text="Yes/No" />
                                             </EditItemTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Approve" 
                                             ItemStyle-HorizontalAlign="Center">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label19" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
                                             </ItemTemplate>
                                             <EditItemTemplate>
                                                 <asp:Label ID="Labelrem" runat="server" Text='<%# Bind("Remarks") %>' ></asp:Label>
                                             </EditItemTemplate>
                                          
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="R.D.O.C." 
                                             ItemStyle-HorizontalAlign="Center">
                                             <ItemTemplate>
                                                 <asp:Label ID="Labelcomm" runat="server" Text='<%# Eval("Comments") %>' 
                                                     Height="21px" Width="193px"></asp:Label>
                                             </ItemTemplate>
                                             <EditItemTemplate>
                                                 <asp:TextBox ID="txtcom" runat="server" Text='<%# Bind("Comments") %>'></asp:TextBox>
                                             </EditItemTemplate>
                                             <ItemStyle HorizontalAlign="Center" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Comments" 
                                             ItemStyle-HorizontalAlign="Center">
                                             <ItemTemplate>
                                                 <asp:Label ID="Lrdoc" runat="server" Text='<%# Eval("RDOC") %>'></asp:Label>
                                             </ItemTemplate>
                                             <EditItemTemplate>
                                                 <asp:TextBox ID="LRDOCedit" runat="server" Text='<%# Bind("RDOC") %>' ></asp:TextBox>
                                                 <asp:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd/MM/yyyy" 
                                                     TargetControlID="LRDOCedit" TodaysDateFormat="d MMMM, yyyy" />
                                             </EditItemTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:CommandField ShowEditButton="True" />
                                     </Columns>
                                     <PagerStyle HorizontalAlign="Center" />
                                     <SelectedRowStyle Font-Bold="True" />
                                    
                                     <EditRowStyle />
                                     <AlternatingRowStyle />
                                     
                                 </asp:GridView>
                       
                             </td>
                         </tr>
                         <tr valign="top">
                             <td>
                                 <asp:GridView ID="GridView3" runat="server" AllowSorting="True" AutoGenerateColumns="False" 
                                     class="table table-striped table-bordered table-hover" 
                                     GridLines="None" HorizontalAlign="Center"
                                     onrowcancelingedit="GridView2_RowCancelingEdit" 
                                     onrowcommand="GridView3_RowCommand" onrowdeleting="GridView2_RowDeleting" 
                                     onrowediting="GridView3_RowEditing" onrowupdating="GridView2_RowUpdating" 
                                     onselectedindexchanged="GridView1_SelectedIndexChanged" ShowFooter="True">    
                                     <Columns>
                                         <asp:TemplateField>
                                             <HeaderTemplate>
                                                 S.No.
                                             </HeaderTemplate>
                                             <ItemTemplate>
                                                 <asp:Label ID="lblSRNO2" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                             <ItemTemplate>
                                                 <asp:Label ID="lbltaskid" runat="server" Text='<%# Eval("TaskID") %>' 
                                                     Visible="false"></asp:Label>
                                             </ItemTemplate>
                                            
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Reference Number">
                                             <ItemTemplate>
                                                 <asp:Label ID="Lblref" runat="server" Text='<%# Eval("Reference_No") %>'></asp:Label>
                                             </ItemTemplate>
                                             
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Task Title" ItemStyle-HorizontalAlign="Center">
                                             <ItemTemplate>
                                                 <asp:Label ID="Labtask1" runat="server" Text='<%# Eval("TaskTitle") %>'></asp:Label>
                                             </ItemTemplate>
                                             <%-- <EditItemTemplate>
                    <asp:Label ID="Labelsubedit" runat="server" Text='<%# Bind("TSubject") %>' 
                            Height="21px" Width="98px"></asp:Label>
                    </EditItemTemplate>--%>
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="Center">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label20" runat="server" Text='<%# Eval("TDescription") %>'></asp:Label>
                                             </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Department" ItemStyle-HorizontalAlign="Center">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label21" runat="server" Text='<%# Eval("Department") %>'></asp:Label>
                                             </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Assigned to" ItemStyle-HorizontalAlign="Center">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label22" runat="server" Text='<%# Eval("Assigned") %>'></asp:Label>
                                             </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label23" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                             </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="DOA" ItemStyle-HorizontalAlign="Center">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label7" runat="server" Text='<%# Eval("DOA") %>'></asp:Label>
                                             </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="DOC" ItemStyle-HorizontalAlign="Center">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label8" runat="server" Text='<%# Eval("DOC") %>'></asp:Label>
                                             </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="F.D.O.C" 
                                             ItemStyle-HorizontalAlign="Center">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label10" runat="server" 
                                                     Text='<%# Eval("FDOC") %>'></asp:Label>
                                             </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Rating" ItemStyle-HorizontalAlign="Center">
                                             <ItemTemplate>
                                                 <asp:Rating ID="Rating1" runat="server" AutoPostBack="false" 
                                                     CssClass="rateStar" 
                                                     CurrentRating='<%# String.IsNullOrEmpty(Eval("rating").ToString())?0:Eval("rating") %>' 
                                                     Direction="LeftToRight" EmptyStarCssClass="EmptyStar" 
                                                     FilledStarCssClass="FillStar" Height="16px" MaxRating="10" 
                                                     StarCssClass="rateItem" WaitingStarCssClass="SaveStar" Width="142px" OnChanged="Rating1_Changed">
                                                 </asp:Rating>
                                             </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Center" />
                                         </asp:TemplateField>
                                         <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                             <ItemTemplate>
                                                 <asp:ImageButton ID="imgdel" runat="server" CommandName="btnadd" ToolTip="Save" Height="18px" 
                                                     ImageUrl="~/Images/save_icon.jpg" />
                                             </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Center" />
                                         </asp:TemplateField>
                                     </Columns>
                                     <PagerStyle HorizontalAlign="Center" />
                                     <SelectedRowStyle Font-Bold="True" />
                                     <HeaderStyle Font-Bold="True" Font-Names="Calibri" />
                                     <EditRowStyle />
                                     <AlternatingRowStyle />
                                     
                                 </asp:GridView>
                             </td>
                         </tr>
                         <tr valign="top">
                             <td>
                                 <asp:GridView ID="GridView4" runat="server" AllowSorting="True" 
                                     AutoGenerateColumns="False"
                                     class="table table-striped table-bordered table-hover" 
                                     GridLines="None"  HorizontalAlign="Center"
                                     onrowcancelingedit="GridView2_RowCancelingEdit" 
                                     onrowcommand="GridView2_RowCommand" onrowdeleting="GridView2_RowDeleting" 
                                     onrowediting="GridView2_RowEditing" onrowupdating="GridView2_RowUpdating" 
                                     onselectedindexchanged="GridView1_SelectedIndexChanged" ShowFooter="True">
                                     <HeaderStyle Height="100px" />
                                     <RowStyle Height="70px" />
                                     <Columns>
                                         <asp:TemplateField>
                                             <HeaderTemplate>
                                                 S.No.
                                             </HeaderTemplate>
                                             <ItemTemplate>
                                                 <asp:Label ID="lblSRNO" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Reference Number">
                                             <ItemTemplate>
                                                 <asp:Label ID="Lblref" runat="server" Text='<%# Eval("Reference_No") %>'></asp:Label>
                                             </ItemTemplate>
                                             <EditItemTemplate>
                                                 <asp:Label ID="refedit"  runat="server" Text='<%# Bind("Reference_No") %>'></asp:Label>
                                             </EditItemTemplate>
                                             
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Task Title" ItemStyle-HorizontalAlign="Center">
                                             <ItemTemplate>
                                                 <asp:Label ID="Labtask" runat="server" Text='<%# Eval("TaskTitle") %>'></asp:Label>
                                             </ItemTemplate>
                                             <EditItemTemplate>
                                                 <asp:Label ID="Labelsubedit" runat="server" Text='<%# Bind("TaskTitle") %>'></asp:Label>
                                             </EditItemTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="Center">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label2" runat="server" Text='<%# Eval("TDescription") %>'></asp:Label>
                                             </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Department" ItemStyle-HorizontalAlign="Center">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label9" runat="server" Text='<%# Eval("Department") %>'></asp:Label>
                                             </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Assigned to" ItemStyle-HorizontalAlign="Center">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label4" runat="server" Text='<%# Eval("Assigned") %>'></asp:Label>
                                             </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label6" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                             </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                     </Columns>
                                     <PagerStyle HorizontalAlign="Center" />
                                   
                                     <SelectedRowStyle Font-Bold="True" />
                                     <HeaderStyle Font-Bold="True" Font-Names="Calibri" />
                                     <EditRowStyle />
                                  
                                 </asp:GridView>
                             </td>
                         </tr>
                         <tr valign="top">
                             <td>
                                 <asp:Calendar ID="Calendar1" runat="server" BackColor="White" 
                                     BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" 
                                     Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" 
                                     Visible="False" Width="120px">
                                     <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                     <SelectorStyle BackColor="#CCCCCC" />
                                     <WeekendDayStyle BackColor="#FFFFCC" />
                                     <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                     <OtherMonthDayStyle ForeColor="#808080" />
                                     <NextPrevStyle VerticalAlign="Bottom" />
                                     <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                     <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                 </asp:Calendar>
                                 <asp:Label ID="lbl_error" runat="server" Font-Bold="True" Font-Names="Calibri" 
                                     Font-Size="Small" ForeColor="#CC0000"></asp:Label>
                             </td>
                         </tr>
                  </table>
                  </div>
             <asp:LinkButton Text="" ID = "dummy" runat="server" />
             <asp:ModalPopupExtender ID="modalhistory" runat="server" TargetControlID="dummy" BackgroundCssClass="modalBackground" CancelControlID="btn1" PopupControlID="panelhist"></asp:ModalPopupExtender>
                                         <div>
                                             <asp:Panel ID="panelhist" runat="server" CssClass="modalPopup">
                                                <table>
                                                    <tr>
                                                        <th style="padding-left:200px;">Task History</th>
                                   
                                                        
                                                            <button id="btn1" runat="server" style="margin-right:0; margin-left:100%; float:right; font-weight:bolder;border-radius:50%;background-color:gray;color:black;border:none">
                                                                ×
                                                            </button>
                                                            <br />
                                                            <asp:Label runat="server">Employee Name:</asp:Label>
                                                            <asp:Label ID="lablname" runat="server"></asp:Label>
                                                       
                                                     </tr>     
                                                </table>
                                                 <%--<div style="overflow-x:auto">--%>
                                                 <asp:GridView ID="GridViewHistry" runat="server" 
                                                     AutoGenerateColumns="False" SkinID="Professional" class="table table-striped table-bordered table-hover" 
                                                     Width="80%" HorizontalAlign="Center" GridLines="None">
                                                 
                                                     <Columns>  
                                                         
                                                          <asp:TemplateField HeaderText="ID" ItemStyle-Height="2px">  
                                                                <ItemTemplate>  
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("task_id") %>'></asp:Label>  
                                                                </ItemTemplate>  
                                                            </asp:TemplateField>  
                                                            <%--<asp:TemplateField HeaderText="Reference Number">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Lblref" runat="server" Text='<%# Bind("Reference_No") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
		                                                    <asp:TemplateField HeaderText="Status" ItemStyle-Height="2px">  
                                                                        <ItemTemplate>  
                                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("status") %>'></asp:Label>  
                                                                        </ItemTemplate>  
                                                            </asp:TemplateField>  --%>
		                                                    <asp:TemplateField HeaderText="R.D.O.C" ItemStyle-Height="2px">  
                                                                        <ItemTemplate>  
                                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("RDOC") %>'></asp:Label>  
                                                                        </ItemTemplate>  
                                                            </asp:TemplateField>  
                                                           <asp:TemplateField HeaderText="Comment" ItemStyle-Height="2px">  
                                                                <ItemTemplate>  
                                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("comment") %>'></asp:Label>  
                                                                </ItemTemplate>  
                                                            </asp:TemplateField> 
                                                         <%--<asp:TemplateField HeaderText="Remarks" ItemStyle-Height="2px">  
                                                                <ItemTemplate>  
                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("remarks") %>'></asp:Label>  
                                                                </ItemTemplate>  
                                                            </asp:TemplateField> --%>
                                                    </Columns>
                                                     <PagerStyle HorizontalAlign="Center" />
                                     <SelectedRowStyle Font-Bold="True" />
                                     <HeaderStyle Font-Bold="True" />
                                                 </asp:GridView>
                                                 <%-- <asp:Button ID="butnclose" runat="server"  CssClass="button" Text="Close"/>--%> 
                                             </asp:Panel>
                                             
                                         <%--</div>--%>
                  </ContentTemplate>
                                </asp:UpdatePanel>

            
</asp:Content>
