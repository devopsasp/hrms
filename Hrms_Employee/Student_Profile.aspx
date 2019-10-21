<%@ Page Title="" MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="Student_Profile.aspx.cs" Inherits="Hrms_Employee_Student_Profile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>
<script type="text/javascript">
    function isNumber(evt) {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }

</script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">      

    </asp:ToolkitScriptManager>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-12">
                        <h2 class="page-header">Student Profile</h2>
                    </div>
                    <!-- /.col-lg-12 -->
                </div>
                <!-- /.row -->
            </div>
            <!-- /.container-fluid -->

            <div class="panel panel-default">
                        <div class="panel-heading">
                            Add/View Student Information</div>
                        <!-- /.panel-heading -->
                         <div class="panel-body">

                            
                             <table cellpadding="0" cellspacing="0" style="width: 100%">
                                 <tr>
                                     <td style="width: 50%">
                                         <div align="center" style="width: 100%">
                               <table class="table table-striped table-bordered table-hover">
                                    <tbody>
                                        <tr>
                                            <td style="width: 40%">Academic Year</td>
                                            <td colspan="2">
                            <asp:DropDownList ID="ddl_Year" class="dropdown-menu pull-right" runat="server"  CssClass="form-control"
                                 Width="80%" onselectedindexchanged="ddl_Year_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 40%">Select Student</td>
                                            <td colspan="2">
                            <asp:DropDownList ID="ddl_Student" runat="server" CssClass="form-control"
                                 Width="80%" AutoPostBack="True" onselectedindexchanged="ddl_Student_SelectedIndexChanged">
                            </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 40%">Quick Search<br />
                                                (Registration No)</td>
                                            <td>
                                            <input class="form-control" runat="server" id="txtQuick" maxlength="50" /></td>
                                            <td>
                                                <asp:Button ID="btn_info" runat="server" class="btn btn-info" Text="Get Info"  onclick="btn_info_Click" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table> 
                            </div></td>
                                     <td style="width: 30%">
                                     <div style="width:95%" align="right">
                               <table class="table table-striped table-bordered table-hover" align="center">
                                    <tbody>
                                        <tr>
                                            <td style="width: 294px; height: 47px;">Import from Excel File</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 294px; height: 53px;">
                                                <asp:FileUpload class="form-control" ID="FU_Excel" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 294px">
                                                <asp:Button ID="btn_import" runat="server" class="btn btn-success"
                                                    Text="Import" onclick="btn_import_Click" />
                                                </td>
                                        </tr>
                                    </tbody>
                                </table> 
                                </div>
                                     </td>
                                     <td style="width: 20%" align="center" valign="top">
                                         <asp:Image ID="Img_Student" runat="server" Width="120px" Height="150px" 
                                             ImageAlign="Middle" Visible="False" />
                                     </td>
                                 </tr>
                             </table>

                        </div>
                        <!-- /.panel-body -->
                    </div>

            <div class="panel panel-default">
                        <div class="panel-heading">
                            Student Master</div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <div>
                                <table class="table table-striped table-bordered table-hover">
                                    
                                    <tr>
                                        <td align="center" colspan="6">
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                                ControlToValidate="txtEmailID" ErrorMessage="Email ID is not valid." 
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td style="width: 13%">
                                            Student Name<span style="color:Red;">*</span> 
                                            </td>
                                        <td style="width: 20%">
                                            <input class="form-control" runat="server" id="txtStudentName" maxlength="50"  onkeypress="AllowOnlyText3();" /></td>
                                        <td style="width: 13%">
                                            Register number<span style="color:Red;">*</span> </td>
                                        <td style="width: 20%">
                                            <input class="form-control" runat="server" id="txtRegisterNo" maxlength="30" /></td>
                                            <td style="width: 13%">
                                                Roll Number<span style="color:Red;">*</span> </td>
                                            <td style="width: 20%">
                                                <input class="form-control" runat="server" id="txtRollNo" maxlength="30" /></td>
                                    </tr>
                                    
                                    <tr>
                                        <td style="width: 275px">
                                            Date of Birth<span style="color:Red;">*</span> </td>
                                        <td>
                                               <%-- <input class="form-control" runat="server" id="txtDOB" maxlength="10" />--%>
                                                
                                                 <div style=" width:150px; float:left;">
                                                     <asp:TextBox id="txtDOB" runat="server" Width="150px" class="form-control"></asp:TextBox>

                                                     <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDOB" Format="dd/MM/yyyy">
                                                     </asp:CalendarExtender>
                                                 </div>
                                                 <div style=" width:25px; float:left;  margin-left:10px; margin-top:3px;">                                                
                                                    <asp:Image ID="Image1" runat="server"  
                                                        Text="" Width="25px" 
                                                        ImageUrl="~/Images/calendaricon.png" />
                                                </div>


                                                
                                                </td>
                                        <td>
                                            Gender<span style="color:Red;">*</span> </td>
                                        <td>
                                            <asp:RadioButtonList ID="rbGender" Width="100%" runat="server" 
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True">Male</asp:ListItem>
                                                <asp:ListItem>Female</asp:ListItem>
                                                <asp:ListItem>Others</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                            <td>
                                                Admission Date<span style="color:Red;">*</span> </td>
                                            <td>
                                               <div style=" width:150px; float:left;">                                               
                                                 <asp:TextBox id="txtAdmissionDate"  runat="server"  Width="150px" class="form-control"></asp:TextBox>                                                    
                                                 <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtAdmissionDate" Format="dd/MM/yyyy" >
                                                 </asp:CalendarExtender>
                                                    </div>

                                                    <div style=" width:25px; float:left;  margin-left:10px; margin-top:3px;">                                                
                                                    <asp:Image ID="Image2" runat="server"  
                                                        Text="" Width="25px" 
                                                        ImageUrl="~/Images/calendaricon.png" />
                                                </div>
                                                    </td>
                                                    
                                    </tr>
                                    <tr>
                                        <td style="width: 275px">
                                            Academic Year<span style="color:Red;">*</span> </td>
                                        <td>
                                            <asp:DropDownList ID="txtAcademicYear" class="form-control" runat="server" Width="100%">
                                            </asp:DropDownList></td>
                                        <td>
                                            Course<span style="color:Red;">*</span> </td>
                                        <td>
                                                <asp:DropDownList class="form-control" ID="txtClassName" Width="100%" 
                                                    DataSourceID="SqlDSCourse" runat="server" DataTextField="CourseName" 
                                                    DataValueField="CourseName" 
                                                    >
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="SqlDSCourse" runat="server" 
                                                    ConnectionString="<%$ ConnectionStrings:connectionstring %>" 
                                                    SelectCommand="SELECT [CourseName] FROM [Student_Course] WHERE ([pn_BranchID] = @pn_BranchID) order by pn_CourseID Asc">
                                                    <SelectParameters>
                                                        <asp:SessionParameter Name="pn_BranchID" SessionField="Login_Temp_BranchID" 
                                                            Type="Int32" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            <td>
                                            Department<span style="color:Red;">*</span> </td>
                                            <td>
                                                <asp:DropDownList class="form-control" ID="txtDepartment" 
                                                    DataSourceID="SqlDSDept" Width="100%" runat="server" 
                                                    DataTextField="DepartmentName" DataValueField="DepartmentName" 
                                                    >
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
                                    </tr>
                                    <tr>
                                        <td style="width: 275px">
                                            Section<span style="color:Red;">*</span> </td>
                                        <td>
                                            <asp:DropDownList ID="txtSection" runat="server" class="form-control" 
                                                Width="100%">
                                                <asp:ListItem Text='a' Value='a'></asp:ListItem>
                                                <asp:ListItem Text="B" Value="B"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            Current Year<span style="color:Red;">*</span> </td>
                                        <td>
                                            <asp:DropDownList ID="ddl_CurrentYear" runat="server" AutoPostBack="True" 
                                                class="dropdown-menu pull-right" CssClass="form-control" 
                                                onselectedindexchanged="ddl_Year_SelectedIndexChanged" Width="100%">
                                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="Alumni" Value="5"></asp:ListItem>
                                            </asp:DropDownList>
                                            <td>
                                                Phone No<span style="color:Red;">*</span> </td>
                                            <td>
                                                <input class="form-control" runat="server" id="txtContact" 
                                                    onkeypress="return isNumber(event)" maxlength="12" />
                                            </td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 275px">
                                            Home/Hostel</td>
                                        <td>
                                            <asp:DropDownList class="form-control" ID="txtResidence" runat="server" Width="100%">
                                            <asp:ListItem Text="Home" Value="Home"></asp:ListItem>
                                            <asp:ListItem Text="Hostel" Value="Hostel"></asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td>
                                            Blood Group</td>
                                        <td>
                                            <asp:DropDownList class="form-control" ID="txtBloodGroup" runat="server" Width="100%">
                                            <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                            <asp:ListItem Text="A +ve" Value="A +ve"></asp:ListItem>
                                            <asp:ListItem Text="A -ve" Value="A -ve"></asp:ListItem>
                                            <asp:ListItem Text="B +ve" Value="B +ve"></asp:ListItem>
                                            <asp:ListItem Text="B -ve" Value="B -ve"></asp:ListItem>
                                            <asp:ListItem Text="A1 +ve" Value="A1 +ve"></asp:ListItem>
                                            <asp:ListItem Text="A1 -ve" Value="A1 -ve"></asp:ListItem>
                                            <asp:ListItem Text="A2 +ve" Value="A2 +ve"></asp:ListItem>
                                            <asp:ListItem Text="A2 -ve" Value="A2 -ve"></asp:ListItem>
                                            <asp:ListItem Text="A1B +ve" Value="A1B +ve"></asp:ListItem>
                                            <asp:ListItem Text="A1B -ve" Value="A1B -ve"></asp:ListItem>
                                            <asp:ListItem Text="A2B +ve" Value="A2B +ve"></asp:ListItem>
                                            <asp:ListItem Text="A2B -ve" Value="A2B -ve"></asp:ListItem>
                                            <asp:ListItem Text="AB +ve" Value="AB +ve"></asp:ListItem>
                                            <asp:ListItem Text="AB -ve" Value="AB -ve"></asp:ListItem>
                                            <asp:ListItem Text="O +ve" Value="O +ve"></asp:ListItem>
                                            <asp:ListItem Text="O -ve" Value="O -ve"></asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td>
                                            Email ID</td>
                                        <td>
                                            <input class="form-control" runat="server" id="txtEmailID" maxlength="50" /></td>
                                    </tr>


                                    <tr>
                                        <td style="width: 275px">
                                            Faculty Advisor</td>
                                        <td>
                                                <input class="form-control" runat="server" id="txtFaculty" maxlength="50" onkeypress="AllowOnlyText3();" /></td>
                                        <td>
                                            Admission Type</td>
                                        <td>
                                                <input class="form-control" runat="server" id="txtAdmissiontype" 
                                                    maxlength="20" /></td>
                                            <td>
                                                Institution Fee</td>
                                            <td>
                                                <input class="form-control" runat="server" id="txtInstitution" 
                                                    onkeydown="AllowOnlyNumeric1(event);" maxlength="10" value="0" /></td>
                                    </tr>


                                    <tr>
                                        <td style="width: 275px">
                                            Community</td>
                                        <td>
                                                <input class="form-control" runat="server" id="txtCommunity" maxlength="30" onkeypress="AllowOnlyText3();" /></td>
                                        <td>
                                            Religion</td>
                                        <td>
                                                <input class="form-control" runat="server" id="txtReligion" maxlength="30" onkeypress="AllowOnlyText3();"/></td>
                                            <td>
                                                Nationality</td>
                                            <td>
                                                <input class="form-control" runat="server" id="txtNationality" maxlength="30" onkeypress="AllowOnlyText3();"/></td>
                                    </tr>


                                    <tr>
                                        <td style="width: 275px">
                                            Bus Route No</td>
                                        <td>
                                                <input class="form-control" runat="server" id="txtBusDetail" maxlength="30"  onkeypress="return isNumber(event)"/></td>
                                        <td>
                                            Reader ID<span style="color:Red;">*</span> </td>
                                        <td>
                                                <input class="form-control" runat="server" id="txtReaderID" maxlength="10" onkeypress="return isNumber(event)"/></td>
                                            <td>
                                                Bank AC</td>
                                            <td>
                                                <input class="form-control" runat="server" id="txtBankAC" maxlength="15" onkeypress="return isNumber(event)" /></td>
                                    </tr>


                                    <tr>
                                        <td style="width: 275px">
                                            Father&#39;s Name</td>
                                        <td>
                                                <input class="form-control" runat="server" id="txtFather" maxlength="50" onkeypress="AllowOnlyText3();"/></td>
                                        <td>
                                            Mother&#39;s Name</td>
                                        <td>
                                                <input class="form-control" runat="server" id="txtMother" maxlength="50" onkeypress="AllowOnlyText3();" /></td>
                                        <td>
                                            Mother Tongue</td>
                                        <td>
                                            <input class="form-control" runat="server" id="txtLanguage" maxlength="50" onkeypress="AllowOnlyText3();" /></td>
                                    </tr>


                                    <tr>
                                        <td style="width: 275px">
                                            Address 1</td>
                                        <td>
                                                <asp:TextBox class="form-control" runat="server" id="txtAddress1" 
                                                    maxlength="50" TextMode="MultiLine"></asp:TextBox></td>
                                        <td>
                                            Address 2</td>
                                        <td>
                                                <asp:TextBox class="form-control" runat="server" id="txtAddress2" 
                                                    maxlength="50" TextMode="MultiLine"></asp:TextBox></td>
                                        <td>
                                            City 
                                            - Pincode</td>
                                        <td>
                                            <input class="form-control" runat="server" id="txtCity" maxlength="10" onkeypress="return isNumber(event)"/></td>
                                    </tr>


                                    <tr>
                                        <td style="width: 275px">
                                            State</td>
                                        <td>
                                                <input class="form-control" runat="server" id="txtState" maxlength="50" onkeypress="AllowOnlyText3();"/></td>
                                        <td>
                                            District</td>
                                        <td>
                                                <input class="form-control" runat="server" id="txtDistrict" maxlength="50" onkeypress="AllowOnlyText3();" /></td>
                                            <td>
                                                Country</td>
                                            <td>
                                                <asp:DropDownList class="form-control" ID="txtCountry" runat="server" Width="100%">
                                                </asp:DropDownList>
                                                </td>
                                    </tr>


                                    <tr>
                                        <td style="width: 275px">
                                            Parent&#39;s Contact</td>
                                        <td>
                                                <input class="form-control" runat="server" id="txtParentsContact" 
                                                    maxlength="12" onkeypress="return isNumber(event)" /></td>
                                        <td>
                                            Status</td>
                                        <td>
                                                <asp:DropDownList class="form-control" ID="ddlStatus" runat="server" 
                                                Width="50%">
                                                    <asp:ListItem>Y</asp:ListItem>
                                                    <asp:ListItem>N</asp:ListItem>
                                                </asp:DropDownList>
                                            <asp:FileUpload ID="Img_Upload" runat="server" Width="100%" Visible="False" /></td>
                                            <td>
                                                <asp:Label 
                                ID="lbl_Error" runat="server"></asp:Label>
                                        </td>
                                            <td align="center">
                                                <asp:Button ID="btn_save" runat="server" class="btn btn-success" Text="Save" 
                                                    onclick="btn_save_Click" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btn_reset" runat="server"  class="btn btn-warning" Text="Reset" 
                                                    onclick="btn_reset_Click" />
                                        </td>
                                    </tr>


                                </table>
                            </div>
                        </div>
                        <!-- /.panel-body -->
                    </div>
</asp:Content>

