<%@ Page Language="C#" MasterPageFile="~/HRMS.master" 
    AutoEventWireup="true" CodeFile="EmployeeShift.aspx.cs" Inherits="Hrms_Master_Default"
    Title="Welcome to HRMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/css">
    .scrollingControlContainer
    {
        overflow-x: hidden;
        overflow-y: scroll;
    }
    </script>
    <script type="text/javascript" language="javascript" src="../../Scripts/Datavalid.js"></script>
    <link href="../../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function isNumberKey(key) {
            //getting key code of pressed key
            var keycode = (key.which) ? key.which : key.keyCode;
            //comparing pressed keycodes

            if (keycode > 31 && (keycode < 48 || keycode > 57) && keycode != 47) {
               
                return false;
            }
            else return true;
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
       var i;   
          
       txtvalue= document.getElementById(txtid).value;
       txtlen=txtvalue.length;  
       
        if(event.keyCode!=8 && event.keyCode!=46 && event.keyCode!=35 && event.keyCode!=36 && event.keyCode!=37 && event.keyCode!=38 && event.keyCode!=39 && event.keyCode!=40)     
        {    
           if(txtlen!=0)
            {               
             if(txtlen==2)
              {
              document.getElementById(txtid).value=txtvalue+"/";
              }
              else
              {
              document.getElementById(txtid).value=txtvalue;
              }               
            } 
        }
}

function fn_date1(event, txtid) {
    var len;
    var txtvalue;
    var bool_obj;
    var i;

    txtvalue = document.getElementById(txtid).value;
    txtlen = txtvalue.length;

    if (event.keyCode != 8 && event.keyCode != 46 && event.keyCode != 35 && event.keyCode != 36 && event.keyCode != 37 && event.keyCode != 38 && event.keyCode != 39 && event.keyCode != 40) {

        if (txtlen != 0) {
            bool_obj = true;

            if (bool_obj == true) {
                if (txtlen == 2 || txtlen == 5) {
                    document.getElementById(txtid).value = txtvalue + "/";
                }
                else {
                    document.getElementById(txtid).value = txtvalue;
                }

            }
            else {

                document.getElementById(txtid).value = txtvalue.substring(0, txtlen - 1);

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
            txtvalue = document.getElementById(txt_monyear).value;
            txtlen = txtvalue.length;

            if (event.keyCode != 8 && event.keyCode != 46 && event.keyCode != 35 && event.keyCode != 36 && event.keyCode != 37 && event.keyCode != 38 && event.keyCode != 39 && event.keyCode != 40) {
                if (txtlen != 0) {
                    if (txtlen == 2) {
                        if (txtvalue < 31) {
                            document.getElementById(txt_monyear).value = "";
                        }
                        else {
                            document.getElementById(txt_monyear).value = txtvalue + "";
                        }
                    }
                    else {
                        document.getElementById(txtid).value = txtvalue;
                    }
                }
            }
        }

     
    </script>
     <div class="row">
                <div class="col-lg-12">
                    <h2 class="page-header">Shift Details<span class="Title"><span 
                    style="font-family: Calibri; color: #FFFFFF; font-weight: bold; font-size: medium;"><asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:connectionstring %>" 
                                    
                                    SelectCommand="SELECT ([EmployeeCode]+'-'+[Employee_First_Name]) as Employee_first_name , [EmployeeCode] FROM [paym_Employee] WHERE (([pn_CompanyID] = @pn_CompanyID) AND ([pn_BranchID] = @pn_BranchID))">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="pn_CompanyID" SessionField="Login_temp_CompanyID" 
                                            Type="Int32" />
                                        <asp:SessionParameter Name="pn_BranchID" SessionField="Login_temp_BranchID" 
                                            Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                        </span></span></h2>
                </div>
                <!-- /.col-lg-12 -->
            </div>

            <div class="panel panel-default">
                        <div class="panel-heading">
                            Shift Details
                            <div class="pull-right">
                                <div class="btn-group">
                            <asp:DropDownList ID="ddl_branch" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddl_branch_SelectedIndexChanged" Width="115px">
                            </asp:DropDownList>
                                    <ul class="dropdown-menu pull-right" role="menu">
                                        <li><a href="#">Action</a>
                                        </li>
                                        <li><a href="#">Another action</a>
                                        </li>
                                        <li><a href="#">Something else here</a>
                                        </li>
                                        <li class="divider"></li>
                                        <li><a href="#">Separated link</a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                            </asp:ToolkitScriptManager>
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
                            <div id="morris-area-chart">
                
                                <table class="table table-striped table-bordered table-hover" style="width: 100%; ">
                                    <tr>
                                        <td align="center" colspan="4">
                                            <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Names="Calibri" ForeColor="Black" Text="Shift Selection" Width="100%"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 145px" class="ContactPickerInlineList" >
                                            Month Year</td>
                                        <td style="width: 214px" ><span>
                                            <asp:TextBox ID="txt_monyear" runat="server" AutoPostBack="True" 
                                                CssClass="form-control" MaxLength="7" onkeydown="AllowOnlyNumeric1(event);" 
                                                onkeyup="fn_date(event,this.id);" ontextchanged="txt_monyear_TextChanged" 
                                                Width="100%"></asp:TextBox>
                                            </span></td>
                                        <td align="center" style="width: 699px"><span>
                                            <asp:Button ID="btn_save" runat="server" class="btn btn-primary" 
                                                onclick="btn_save_Click" Text="View" />
                                            </span></td>
                                        <td align="center">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4">
                                            <asp:Calendar ID="Calendar1" runat="server" CssClass="table table-hover table-striped table-responsive" ondayrender="Calendar1_DayRender" ShowGridLines="true" ShowNextPrevMonth="False"></asp:Calendar>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <!-- /.panel-body -->
                    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <br />
    </asp:Content>







