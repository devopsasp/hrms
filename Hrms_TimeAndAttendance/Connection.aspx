<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/HRMS.master"
 CodeFile="Connection.aspx.cs" Inherits="Hrms_TimeAndAttenence_Connection" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    function selectAll(invoker) {
        var inputElements = document.getElementsByTagName('input');
        for (var i = 0; i < inputElements.length; i++) {
            var myElement = inputElements[i];
            if (myElement.type === "checkbox") {
                myElement.checked = invoker.checked;
            }
        }
    }


    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
            return false;

        return true;
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
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;
        return true;
    }


</script>

<div class="row">
                <div class="col-lg-12">
                    <h2 class="page-header">Time Machine Management</h2>
                </div>
                <div>
                </div>
                <!-- /.col-lg-12 -->
            </div>

            <div class="panel-group" id="accordion">
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                           <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne">Device Management</a>
                                        </h4>
                                    </div>
                                    <div id="collapseOne" class="panel-collapse collapse">
                                        <div class="panel-body">
                                            &nbsp;<table style="width: 100%">
                                                <tr>
                                                    <td style="width: 50%" rowspan="3">
                                            <table align="center" class="table table-striped table-bordered table-hover" 
                                                style="width: 90%">
                                                <tr>
                                                    <td colspan="7">
                                                        Delete Enrolled Data</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        UserID</td>
                                                    <td colspan="2">
                                                        <asp:DropDownList ID="ddl_user1" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        Backup No</td>
                                                    <td colspan="2">
                                                        <asp:DropDownList ID="ddl_backup" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                            
                                <asp:Button ID="btn_deluser" runat="server" class="btn btn-warning" 
                                Text="Delete User"/>
                           
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="7">
                                                        Delete User&#39;s Finger Print Templates</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        UserID</td>
                                                    <td colspan="2">
                                                        <asp:DropDownList ID="ddl_user2" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        Finger Index</td>
                                                    <td colspan="2">
                                                        <asp:DropDownList ID="ddl_findex" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                            <span style="font-family: Calibri" __designer:mapid="5bc">
                            <span style="color: #444444" __designer:mapid="5bd">
                            <span style="color: #6A6A6A" __designer:mapid="5be">
                                <asp:Button ID="btn_deltemp" runat="server" class="btn btn-warning" 
                                Text="Delete Template" />
                            </span></span></span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="7">
                                                        Clear Data (Batch Delete)</td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2">
                            <span style="font-family: Calibri" __designer:mapid="5bc">
                            <span style="color: #444444" __designer:mapid="5bd">
                            <span style="color: #6A6A6A" __designer:mapid="5be">
                                <asp:Button ID="btn_clruser" runat="server" class="btn btn-danger" 
                                Text="Clear All Users" />
                            </span></span></span>
                                                    </td>
                                                    <td align="center" colspan="3">
                            <span style="font-family: Calibri" __designer:mapid="5bc">
                            <span style="color: #444444" __designer:mapid="5bd">
                            <span style="color: #6A6A6A" __designer:mapid="5be">
                                <asp:Button ID="btn_clrtemp" runat="server" class="btn btn-danger" 
                                Text="Clear All Templates"  />
                            </span></span></span>
                                                    </td>
                                                    <td align="center" colspan="2">
                            <span style="font-family: Calibri" __designer:mapid="5bc">
                            <span style="color: #444444" __designer:mapid="5bd">
                            <span style="color: #6A6A6A" __designer:mapid="5be">
                                <asp:Button ID="btn_clradmin" runat="server"  class="btn btn-danger"
                                Text="Clear Administrator" />
                            </span></span></span>
                                                    </td>
                                                </tr>
                                            </table>
                                                    </td>
                                                    <td>
                                                        <table style="width: 90%" class="table table-striped table-bordered table-hover" >
                                                            <tr>
                                                                <td>
                                                                    IP Address</td>
                                                                <td>
                            <asp:TextBox ID="txtiptemp" runat="server" Font-Names="Calibri" Width="176px" onkeypress="return onlyNumbersWithDot(event);"
                                    CssClass="InputDefaultStyle form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                            <span style="font-family: Calibri" __designer:mapid="5bc">
                            <span style="color: #444444" __designer:mapid="5bd">
                            <span style="color: #6A6A6A" __designer:mapid="5be">
                                <asp:Button ID="btn_Connecttemp" runat="server" class="btn btn-success" 
                                Text="Connect" onclick="btn_Connecttemp_Click"/>
                            </span></span></span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3">
                                                        <asp:GridView ID="GvTemp" runat="server">
                                                        </asp:GridView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                            <span style="font-family: Calibri" __designer:mapid="5bc">
                            <span style="color: #444444" __designer:mapid="5bd">
                            <span style="color: #6A6A6A" __designer:mapid="5be">
                                <asp:Button ID="btn_downtemp" runat="server" class="btn btn-info" 
                                Text="Download Tempalates" onclick="btn_downtemp_Click" />
                            </span></span></span>
                                                                </td>
                                                                <td>
                                                                    <span style="font-family: Calibri" __designer:mapid="5bc">
                                                                    <span style="color: #444444" __designer:mapid="5bd">
                                                                    <span style="color: #6A6A6A" __designer:mapid="5be">
                                <asp:Button ID="btn_Upbatch" runat="server" class="btn btn-info" 
                                Text="Batch Upload" />
                            </span></span></span>
                                                                </td>
                                                                <td>
                                                                    <span style="font-family: Calibri" __designer:mapid="5bc">
                                                                    <span style="color: #444444" __designer:mapid="5bd">
                                                                    <span style="color: #6A6A6A" __designer:mapid="5be">
                                <asp:Button ID="btn_upsingle" runat="server" class="btn btn-info" 
                                Text="Custom Upload" />
                            </span></span></span>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        </td>
                                              
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                            <div class="divWaiting">
                            
                            <asp:Image ID="imgWait" runat="server" ImageAlign="Middle" 
                                    ImageUrl="~/Images/loading.gif" Height="100px" Width="100px" />
                                <%--<img src="../loading.gif" alt="Loading" style="position:relative;" />--%>
                            </div>
                            </ProgressTemplate>
                            </asp:UpdateProgress>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                           <a data-toggle="collapse" data-parent="#accordion" href="#collapseTwo">Data Download</a>
                                        </h4>
                                    </div>
                                    <div id="collapseTwo" class="panel-collapse collapse in">
                                        <div class="panel-body">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 50%">

                            
                                       
                                        <table class="table table-striped table-bordered table-hover">
                                            <tr>
                                                <td colspan="3">
          <div id="diva" runat="server" align="center" style="overflow: auto; height: 460px;">
        <asp:GridView ID="GridMachine" runat="server" AutoGenerateColumns="False" CssClass="table table-hover table-striped"
                                    Width="100%" onrowediting="GridMachine_RowEditing" AllowPaging="True" 
                                    onpageindexchanging="GridMachine_PageIndexChanging" onrowcancelingedit="GridMachine_RowCancelingEdit" 
                                    onrowdeleting="GridMachine_RowDeleting" 
                  onrowupdating="GridMachine_RowUpdating" DataKeyNames="MNo" 
                                    
                  onselectedindexchanging="GridMachine_SelectedIndexChanging" 
                  GridLines="None" onrowdatabound="GridMachine_RowDataBound" >
                                    <Columns>
                                       
                                        <asp:TemplateField>
                                        <HeaderTemplate>
                                        <asp:CheckBox ID="Chkall" runat="server" Text="Select All" 
                                                 OnClick="selectAll(this)" />
                                        </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkMac" runat="server"  AutoPostBack="True" 
                                                    oncheckedchanged="ChkMac_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="Reader No">
                                        <HeaderStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblMac" runat="server" Text='<%# Eval("MNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblM" runat="server" Text='<%# Bind("MNo") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="IP Address">
                                        <HeaderStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblIP" runat="server" Text='<%# Eval("IPAddr") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:textbox ID="txtIp0" runat="server" Text ='<% # Bind("IPAddr") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Location">
                                        <HeaderStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblloc" runat="server" Text='<%# Eval("Location") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:textbox ID="txtloc" runat="server" Text ='<% # Bind("Location") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:CommandField InsertVisible="False" ShowCancelButton="True" ButtonType="Image" DeleteImageUrl="~/Images/delete_icon.jpg"
                                            ShowDeleteButton="True" EditImageUrl="~/Images/edit_icon.png" UpdateImageUrl="~/Images/save_icon.jpg" CancelImageUrl="~/Images/delete_icon.jpg"  ShowEditButton="True" />
                                    </Columns>
                                </asp:GridView>
                                
                                </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%">
                                <asp:Button ID="cmd_collect" runat="server" Text="Download" Width="74px" 
                                    BackColor="#483C48" Font-Bold="True" Font-Names="Calibri" ForeColor="White" 
                                    Height="30px" onclick="cmd_collect_Click" style="margin-left: 6px" 
                                    Visible="False" />
                                                    <asp:Label ID="lblhidden" runat="server" 
                    Visible="False" Font-Names="Calibri"></asp:Label>
                                                </td>
                                                <td style="width: 25%">
                            <span style="font-family: Calibri">
                            <span style="color: #444444">
                            <span style="color: #6A6A6A">
                                <asp:Button ID="btn_Connect" runat="server" class="btn btn-success" 
                                Text="Connect &amp; Download" onclick="btn_Connect_Click"/>
                            </span></span></span>
                                                </td>
                                                <td style="width: 25%">
                            <span style="font-family: Calibri">
                            <span style="color: #444444">
                            <span style="color: #6A6A6A">
                                <asp:Button ID="btn_disconnect" runat="server" class="btn btn-danger" 
                                Text="Disconnect" onclick="btn_disconnect_Click" />
                            </span></span></span>
                                                </td>
                                            </tr>
                                        </table>

                                       </td>
                                    <td>
                                        <div id="morris-area-chart" align="center" style="width: 100%">
                                            <table align="center" class="table table-striped table-bordered table-hover" 
                                                style="width: 90%">
                                                <tr>
                                                    <td colspan="2">
                                                        Add 
                                                        New Time Machine                                           </tr>
                                                <tr>
                                                    <td>
                                                        Machine No</td>
                                                    <td>
                            <asp:TextBox ID="txtMachine" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        IP Address</td>
                                                    <td>
                            <asp:TextBox ID="txtip" runat="server" CssClass="form-control" onkeypress="return onlyNumbersWithDot(event);" MaxLength="15"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Reader Location</td>
                                                    <td>
                            <asp:TextBox ID="txtlocation" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                            <span style="font-family: Calibri">
                            <span style="color: #444444">
                            <span style="color: #6A6A6A">
                                <asp:Button ID="btn_save" runat="server" class="btn btn-success" 
                                Text="Save" onclick="btn_save_Click" />
                            </span></span></span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseThree">Downloaded Data</a>
                                        </h4>
                                    </div>
                                    <div id="collapseThree" class="panel-collapse collapse in">
                                        <div class="panel-body">
                                            <div id="div1" runat="server" style="overflow: auto; height: 500px;">
        <asp:ListView ID="lv_fingerDetails" runat="server" 
            onselectedindexchanged="lv_fingerDetails_SelectedIndexChanged">
        <LayoutTemplate>
            <table border="1" >
                <tr style="font-family: calibri; color: Black; font-size: small; font-weight: bold;padding:initial">
                    <td>S. No.</td>
                    <td>Machine Number</td>
                    <td>Enroll Number</td>
                    <td>Name</td>
                    <td>Days</td>
                    <td>Verify Mode</td>
                    <td>InOut Mode</td>
                    <td>Date</td>
                    <td>Time</td>                
                </tr>
                <tr id="itemPlaceHolder" runat="server"></tr>
            </table>
        </LayoutTemplate>
        
        <ItemTemplate>
            <tr style="font-family: calibri">
            
                <td><asp:Label ID="lblsno" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>  </td>
                <td><asp:Label ID="txtmn" runat="server" Text='<%#Eval("machine_num") %>'></asp:Label>  </td>
                <td><asp:Label ID="txten" runat="server" Text='<%#Eval("enroll_num") %>'></asp:Label>  </td>
                <td><asp:Label ID="txtname" runat="server" Text='<%#Eval("Name") %>'></asp:Label>  </td>
                <td><asp:Label ID="txtday" runat="server" Text='<%#Eval("days") %>'></asp:Label>  </td>
                <td><asp:Label ID="txtvm" runat="server" Text='<%#Eval("VerifyMode") %>'></asp:Label>  </td>
                <td><asp:Label ID="txt_io_m" runat="server" Text='<%#Eval("InOutMode") %>'></asp:Label>  </td>                
                <td><asp:Label ID="txtdate" runat="server" Text='<%#Eval("day") +"-"+ Eval("month")+"-"+ Eval("year") %>'></asp:Label>  </td>
                <td><asp:Label ID="txttime" runat="server" Text='<%#Eval("hour") +":"+ Eval("min")+":"+ Eval("sec") %>'></asp:Label>  </td>
            </tr>
        </ItemTemplate>
        
    </asp:ListView>
    </div>
                                        </div>
                                    </div>
                                </div>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                        
</asp:Content>

