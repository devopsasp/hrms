<%@ Page MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Employee_Preview.aspx.cs" Inherits="Hrms_Employee_Default" Title="Welcome to HRMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div > <h3 class="page-header"> Employee Details</h3>
                        </div>
                         <center>
                        <div><h3><asp:Label ID="lbl_Error" runat="server" CssClass="Error" 
                                ForeColor="Red" ></asp:Label>
                            <asp:Label ID="Label1" runat="server" ></asp:Label></h3></div>
                            <div><h3><asp:Label ForeColor="#5D7B9D" ID="lbl_empcodename"
                                runat="server"></asp:Label></h3></div>
                                </center>
                          <div class="panel-group" id="accordion">
                           <div id="ProfileGrp" runat="server" class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                           <a data-toggle="collapse" data-parent="#accordion" href="#profile">Profile</a>
                                        </h4>
                                    </div>
                                    <div id="profile"  class="panel-collapse collapse" style="width: 60%">
                                    <table id="tab_profile" runat="server" class="table table-hover table-striped">
                    <tr>
                        <td > Salutation</td>
                        <td >
                            :</td>
                        <td >
                            </td>
                        <td >
                            Employee Code</td>
                        <td >
                            :</td>
                        <td >
                            </td>
                    </tr>
                    <tr>
                        <td >
                            Full Name</td>
                        <td  >
                            :</td>
                        <td >
                            </td>
                        <td  >
                            First Name </td>
                        <td  >
                            :</td>
                        <td >
                            </td>
                    </tr>
                    <tr>
                        <td  
                            >
                             Middle Name</td>
                        <td  
                            >
                            :</td>
                        <td >
                            </td>
                        <td  >
                                                        Last Name </td>
                        <td  >
                                                        :</td>
                        <td >
                            </td>
                    </tr>
                    <tr>
                        <td  >
                             Date of Birth</td>
                        <td  >
                            :</td>
                        <td >
                            </td>
                        <td  >
                            Gender</td>
                        <td  >
                            :</td>
                        <td >
                            </td>
                    </tr>
                    <tr>
                        <td  >
                             Blood Group</td>
                        <td  >
                            :</td>
                        <td >
                            </td>
                        <td  >
                            Marital Status</td>
                        <td  >
                            :</td>
                        <td >
                            </td>
                    </tr>
                    <tr>
                        <td  >
                             Nationality</td>
                        <td  >
                            :</td>
                        <td >
                            </td>
                        <td  >
                            Religion</td>
                        <td  >
                            :</td>
                        <td >
                            </td>
                    </tr>
                    <tr>
                        <td  
                            >
                             ReaderID</td>
                        <td  
                           >
                            :</td>
                        <td >
                            </td>
                        <td  
                           >
                            OT_Eligible</td>
                        <td  
                           >
                            :</td>
                        <td >
                            </td>
                    </tr>
                    <tr>
                        <td  
                            >
                             PF no </td>
                        <td  
                           >
                            :</td>
                        <td >
                            </td>
                        <td  
                           >
                            OT_calc</td>
                        <td  
                           >
                            :</td>
                        <td >
                            </td>
                    </tr>
                    <tr>
                        <td  >
                            
                            ESI no</td>
                        <td  >
                            :</td>
                        <td >
                            </td>
                        <td 
                            >
                             Reporting Person</td>
                        <td 
                            >
                             :</td>
                        <td >
                            </td>
                    </tr>
                    <tr>
                        <td  
                            >
                             PAN no</td>
                        <td  
                           >
                            :</td>
                        <td >
                            </td>
                        <td  
                           >
                             Basic/Gross Pay </td>
                        <td  
                           >
                             :</td>
                        <td >
                            </td>
                    </tr>
                    <tr>
                        <td  >
                             EDLI Eligibility</td>
                        <td  >
                            :</td>
                        <td >
                            </td>
                        <td  
                            >
                             Salary Type </td>
                        <td  
                            >
                             :</td>
                        <td >
                            </td>
                    </tr>
                     <tr>
                        <td >
                       
                        </td>
                        <td colspan="4" >
                            
                            <b><asp:LinkButton runat="server" ID="LinkButton6" Text="Edit Profile" 
                             OnClick="LinkButton3_Click" style="font-size: small"></asp:LinkButton></b></td>
                        <td class="dComposePreviewVal">
                        </td>

                        
                    </tr>
                </table>

                                    </div>
                                    </div>


                                      <div id="Contactinfo" runat="server" class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                           <a data-toggle="collapse" data-parent="#accordion" href="#Cntinfo">Contact Information</a>
                                        </h4>
                                    </div>
                                    <div id="Cntinfo"  class="panel-collapse collapse" style="width: 60%">

                                    <table id="tab_address" runat="server" class="table table-striped table-bordered table-hover">
                    <tr>
                        <td class="dComposePreviewVal">
                            
                            <br />
 Present Address</td>
                        <td class="dComposePreviewVal">
                            </td>
                        <td class="dComposePreviewVal">
                            </td>
                        <td class="dComposeItemLabel">
                            </td>
                        <td class="dComposeItemLabel">
                            </td>
                        <td class="dComposePreviewVal">
                            </td>
                    </tr>
                    <tr>
                        <td>
                            House No  
                             </td>
                        <td>
                            :</td>
                        <td>
                            </td>
                        <td>
                            Address Line 1 </td>
                        <td>
                            :</td>
                        <td>
                            </td>
                    </tr>
                    <tr>
                        <td>
                            Address Line 2 </td>
                        <td>
                            :</td>
                        <td >
                            </td>
                        <td>
                            State </td>
                        <td>
                            :</td>
                        <td 
                            >
                            </td>
                    </tr>
                    <tr>
                        <td>
                            City    </td>
                        <td>
                            :</td>
                        <td>
                            </td>
                        <td>
                            Pincode </td>
                        <td>
                            :</td>
                        <td>
                            </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            </td>
                        <td>
                            </td>
                        <td>
                            </td>
                    </tr>
                    <tr>
                        <td>
                            
                            Permanent Address</td>
                        <td>
                            </td>
                        <td>
                        </td>
                        <td>
                            </td>
                        <td>
                            </td>
                        <td>
                            </td>
                    </tr>
                    <tr>
                        <td>
                            House No    </td>
                        <td>
                            :</td>
                        <td>
                            </td>
                        <td>
                            Address Line 1 </td>
                        <td>
                            :</td>
                        <td>
                            </td>
                    </tr>
                    <tr>
                        <td>
                            Address Line 2    </td>
                        <td>
                            :</td>
                        <td >
                            </td>
                        <td>
                            State  
                            </td>
                        <td>
                            :</td>
                        <td >
                            </td>
                    </tr>
                    <tr>
                        <td>
                            
                            City     
                            </td>
                        <td>
                            :</td>
                        <td>
                            </td>
                        <td>
                            Pincode  
                            </td>
                        <td>
                            :</td>
                        <td>
                            </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                             <span >Contact</span></td>
                        <td>
                            </td>
                        <td >
                        </td>
                        <td>
                            </td>
                        <td>
                            </td>
                        <td>
                            </td>
                    </tr>
                    <tr>
                        <td>
                            Office   </td>
                        <td>
                            :</td>
                        <td>
                            </td>
                        <td>
                            Residence </td>
                        <td>
                            :</td>
                        <td >
                            </td>
                    </tr>
                    <tr>
                        <td>
                            Mobile    </td>
                        <td>
                            :</td>
                        <td>
                            </td>
                        <td>
                            Fax </td>
                        <td>
                            :</td>
                        <td>
                            </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
 Email</td>
                        <td>
                            </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Email Id   </td>
                        <td>
                            :</td>
                        <td>
                            </td>
                        <td>
                            Alternate Email Id </td>
                        <td>
                            :</td>
                        <td>
                            </td>
                    </tr>
                    <tr>
                        <td>
                            
                            <br />
 Emergency Contact</td>
                        <td>
                            </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td >
                        </td>
                    </tr>
                    <tr>
                        <td>
                                                        Name   </td>
                        <td>
                            : </td>
                        <td>
                            </td>
                        <td>
                            Phone </td>
                        <td >
                            :</td>
                        <td>
                            </td>
                    </tr>
                    <tr>
                        <td colspan="6" >
                            </td>
                    </tr>

                     <tr>
                        <td >
                       
                        </td>
                        <td colspan="4" >
                            
                            <b><asp:LinkButton runat="server" ID="LinkButton7" Text="Edit Contact Information" 
                             OnClick="LbtnContactInformation_Click" style="font-size: small"></asp:LinkButton></b></td>
                        <td class="dComposePreviewVal">
                        </td>

                        
                    </tr>

                </table>
                                    </div>
                                    </div>



                                      <div id="BankDetails" runat="server" class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                           <a data-toggle="collapse" data-parent="#accordion" href="#Bnkdtls">Bank Details</a>
                                        </h4>
                                    </div>
                                    <div id="Bnkdtls" style="width: 60%" class="panel-collapse collapse">

                                     <table id="tab_bank" runat="server"  class="table table-striped table-bordered table-hover">
                    <tr>
                        <td class="dComposePreviewVal"  >
                        </td>
                        <td style="width: 171px">
                        </td>
                        <td class="dComposeItemLabel" >
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td >
                             Bank Code 
                             </td>
                        <td >
                            </td>
                        <td  >
                                                        Bank Name </td>
                        <td >
                            </td>
                    </tr>
                    <tr>
                        <td  >
                            Branch Name 
                            </td>
                        <td >
                            </td>
                        <td  >
                                                        Account No  </td>
                        <td >
                            </td>
                    </tr>
                    <tr>
                        <td  >
                             MICR Code</td>
                        <td >
                            </td>
                        <td  >
                                                        IFSC Code</td>
                        <td >
                            </td>
                    </tr>
                    <tr>
                        <td>
                             Address</td>
                        <td  >
                            </td>
                        <td  >
                                                        Other Info</td>
                        <td >
                            </td>
                    </tr>
                    <tr>
                        
                        <td colspan="4" >                            
                            <b><asp:LinkButton runat="server" ID="LinkButton8" Text="Edit Bank Details" 
                             OnClick="LbtnBankDetails_Click" style="font-size: small"></asp:LinkButton></b></td>
                       

                        
                    </tr>



                </table>
                                    </div>
                                    </div>


                                      <div id="FamilyDetails" runat="server" class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                           <a data-toggle="collapse" data-parent="#accordion" href="#fmdetails">Family Details</a>
                                        </h4>
                                    </div>
                                    <div id="fmdetails" style="width: 60%" class="panel-collapse collapse">
                                    <table id="tab_family" runat="server"  class="table table-striped table-bordered table-hover">
                    <tr>
                        <td class="dComposePreviewVal" >
                        </td>
                        <td >
                        </td>
                        <td class="dComposeItemLabel"  style="width: 92px">
                        </td>
                        <td >
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Father&#39;s Name</td>
                        <td >
                            </td>
                        <td  >
                                                                                                                Mother&#39;s Name</td>
                        <td>
                            </td>
                    </tr>
                    <tr>
                        <td >
                            Spouse Name</td>
                        <td>
                            </td>
                        <td >
                           No.Of Children</td>
                        <td >
                            </td>
                    </tr>
                    <tr>
                        <td >
                       
                        </td>
                        <td colspan="4" >
                            
                            <b><asp:LinkButton runat="server" ID="LinkButton9" Text="Edit Family Detail" 
                             OnClick="lnkbtnFamilyDetails_Click" style="font-size: small"></asp:LinkButton></b></td>
                    </tr>   
                </table>
                                    </div>
                                    </div>


                                      <div id="PastempDetails" runat="server" class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                           <a data-toggle="collapse" data-parent="#accordion" href="#pstempdtls">Past Employment Details</a>
                                        </h4>
                                    </div>
                                    <div id="pstempdtls" style="width: 60%" class="panel-collapse collapse">
                                    
                <table id="tab_ref" runat="server"  class="table table-striped table-bordered table-hover">
                    <tr>
                        <td >
                             SalaryStructure</td>
                        <td>
                            </td>
                        <td >
                            Position Held</td>
                        <td >
                            </td>
                    </tr>
                    <tr>
                        <td >
                             Training Attended</td>
                        <td >
                            </td>
                        <td >
                            Training Duration</td>
                        <td >
                            </td>
                    </tr>
                    <tr>
                        <td  >
                            <b>
                            <span > Reference 1</span></b> </td>
                        <td >
                        </td>
                        <td class="dComposeItemLabel"  >
                        </td>
                        <td >
                        </td>
                    </tr>
                    <tr>
                        <td >
                            
                            Person Name </td>
                        <td >
                            </td>
                        <td  >
                            Relationship </td>
                        <td>
                            </td>
                    </tr>
                    <tr>
                        <td  >
                             
                            Contact Phone No.</td>
                        <td >
                            </td>
                        <td  >
                            Contact Email_ID </td>
                        <td >
                            </td>
                    </tr>
                    <tr>
                        <td  
                            
                            >
                            <b>
                            <span >
                            Reference 2</span></b> </td>
                        <td >
                        </td>
                        <td >
                        </td>
                        <td >
                        </td>
                    </tr>
                    <tr>
                        <td  >
                             
                            Person Name </td>
                        <td >
                            </td>
                        <td  >
                            Relationship </td>
                        <td >
                            </td>
                    </tr>
                    <tr>
                        <td >
                             Contact Phone No. </td>
                        <td >
                            </td>
                        <td  >
                            Contact Email_ID </td>
                        <td >
                            </td>
                    </tr>
                    <tr>
                        <td class="dComposeItemLabel" >
                        </td>
                        <td >
                        </td>
                        <td class="dComposeItemLabel"  >
                        </td>
                        <td >
                        </td>
                    </tr>
                    <tr>
                        <td  >
                        </td>
                        <td colspan="2">
                            
                            <b><asp:LinkButton runat="server" ID="LinkButton3"  Text="Edit Profile" 
                                OnClick="LinkButton3_Click" style="font-size: small"></asp:LinkButton></b></td>
                        <td class="dComposePreviewVal" style="width: 127px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" >
                            </td>
                    </tr>
                </table>
                                    </div>
                                    </div>



                                      <div id="workdetails" runat="server" class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                           <a data-toggle="collapse" data-parent="#accordion" href="#wrkdtls">Work Details</a>
                                        </h4>
                                    </div>
                                    <div id="wrkdtls" style="width: 60%" class="panel-collapse collapse">
                                    
                <table id="tab_workdet" runat="server"  class="table table-striped table-bordered table-hover">
                    <tr>
                        <td  >
                            Division  </td>
                        <td  >
                            </td>
                        <td>
                                                        Category </td>
                        <td >
                            </td>
                    </tr>
                    <tr>
                        <td >
                             Department  
                            </td>
                        <td  >
                            </td>
                        <td  >
                                                        Level  </td>
                        <td >
                            </td>
                    </tr>
                    <tr>
                        <td >
                             Designation  </td>
                        <td >
                            </td>
                        <td >
                                                        Grade  </td>
                        <td >
                            </td>
                    </tr>
                    <tr>
                        <td  >
                            Shift  
                            </td>
                        <td  >
                            </td>
                        <td >
                                                        Job Status  </td>
                        <td >
                            </td>
                    </tr>
                    <tr>
                        <td >
                             Projectsite  
                            
                        </td>
                        <td>
                            </td>
                        <td  >
                            </td>
                        <td >
                            </td>
                    </tr>
                    <tr>
                        <td >
                        </td>
                        <td colspan="2"  >
                            
                            <b><asp:LinkButton runat="server" ID="lbtn_WorkDetails" Text="Edit WorkDetails" 
                                OnClick="lbtn_WorkDetails_Click" style="font-size: small"></asp:LinkButton>
                            </b></td>
                        <td class="dComposePreviewVal">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            </td>
                    </tr>
                </table>
                                    </div>
                                    </div>


                                      <div id="datedetails" runat="server" class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                           <a data-toggle="collapse" data-parent="#accordion" href="#dtdtls">Date Details</a>
                                        </h4>
                                    </div>
                                    <div id="dtdtls" style="width: 60%" class="panel-collapse collapse">
                                    
                <table id="tab_date" runat="server"  class="table table-striped table-bordered table-hover">
                    <tr>
                        <td  >
                            
                            Date Of Joining  
                            </td>
                        <td  >
                            </td>
                        <td  >
                                                        Extended Upto  </td>
                        <td >
                            </td>
                    </tr>
                    <tr>
                        <td   >
                            Confirmation Date  </td>
                        <td  >
                            </td>
                        <td  >
                                                        Retirement Date  </td>
                        <td >
                            </td>
                    </tr>
                    <tr>
                        <td  >
                            
                            Con. Renew Date  </td>
                        <td >
                            </td>
                        <td>
                                                        Probation Upto  </td>
                        <td >
                            </td>
                    </tr>
                    <tr>
                        <td>
                            
                            Date Of Seperation  
                        </td>
                        <td>
                            </td>
                        <td  >
                                                    Reason For Change </td>
                        <td>
                            </td>
                    
                        </tr>
                    <tr>
                        <td >
                        </td>
                        <td colspan="2" >
                            
                            <b><asp:LinkButton runat="server" ID="lbtn_date" Text="Edit Date" 
                             OnClick="lbtn_date_Click" style="font-size: small"></asp:LinkButton></b></td>
                        <td class="dComposePreviewVal">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" >
                            </td>
                    </tr>
                </table>
                                    </div>
                                    </div>



                                      <div id="workexpdetails" runat="server" class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                           <a data-toggle="collapse" data-parent="#accordion" href="#wrkexp">Work Experience Details</a>
                                        </h4>
                                    </div>
                                    <div id="wrkexp" style="width: 60%" class="panel-collapse collapse">
                                    
                <table runat="server" id="tab_workexp"  class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>
                            <asp:GridView ID="grid_WorkHistory" CssClass="table table-striped table-bordered table-hover" runat="server" Width="100%" 
                                AutoGenerateColumns="False"  CellPadding="4" 
                                GridLines="None" ShowFooter="True" 
                                onrowdatabound="grid_WorkHistory_RowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <thead>
                                                    <tr>
                                                        <th >
                                                            S.No</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%">
   
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                             <asp:Label ID="lblSerial_WH" runat="server"></asp:Label>
                                                            </a></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <thead>
                                                    <tr>
                                                        <th >
                                                            Company Name</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%">
   
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                              <%#Eval("CompanyName")%></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <thead>
                                                    <tr>
                                                        <th >
                                                            Designation</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%">
   
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                              <%#Eval("DesignationCode")%></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <thead>
                                                    <tr>
                                                        <th >
                                                             Current CTC</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%">
   
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                             <%#Eval("Salary")%></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                
                                   
                                </Columns>
                                <FooterStyle />
                                <PagerStyle />
                                <SelectedRowStyle  />
                                <HeaderStyle  />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="dComposePreviewEdit" align="center" colspan="2"  
                            style="height: 33px; font-family: Calibri;">
                            <asp:LinkButton runat="server" ID="lbtn_workexp" Text="Edit WorkExperience" 
                                OnClick="lbtn_workexp_Click" style="font-size: small"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            </td>
                    </tr>
                </table>
                                    </div>
                                    </div>



                                      <div id="Qualifications" runat="server" class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                           <a data-toggle="collapse" data-parent="#accordion" href="#qual">Qualifications</a>
                                        </h4>
                                    </div>
                                    <div id="qual" class="panel-collapse collapse">
                                    
                <table runat="server" id="tab_edu"  class="table table-striped table-bordered table-hover">
                    <tr>
                        <td style="height: 115px">
                            <asp:GridView ID="grid_emp_education" runat="server" AutoGenerateColumns="False"
                                Width="86%" DataKeyNames="PGCourseID" 
                                CssClass="table table-striped table-bordered table-hover" GridLines="None" 
                                onrowdatabound="grid_emp_education_RowDataBound" 
                                onrowdeleting="grid_emp_education_RowDeleting">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <thead>
                                                    <tr>
                                                        <th style="width:100%;" >
                                                            S.No</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                             <asp:Label ID="lblSerial_Q" runat="server"></asp:Label>
                                                            </a></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField Visible="false">
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <thead>
                                                    <tr>
                                                        <th style="width:100%;" >
                                                            </th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                         <td >
                                                            <asp:Label ID="lblEmployeeID" runat="server" Text='<%# Eval("EmployeeId") %>' Visible="false"></asp:Label>
                                                            
                                                            </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField Visible="false">
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <thead>
                                                    <tr>
                                                        <th style="width:100%;" >
                                                            </th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                       <td >
                                                           <asp:Label ID="lblPGCourseId" runat="server" Text='<%# Eval("PGCourseID") %>' Visible="false"></asp:Label></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table>
                                               
                                                <thead>
                                                    <tr>
                                                        <th>
                                                            Qualification</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                            <%#Eval("PGCourseName")%>
                                                            </a></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <thead>
                                                    <tr>
                                                        <th >
                                                            Institution</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                            <%#Eval("PGInstutionName")%>
                                                            </a></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <thead>
                                                    <tr>
                                                        <th >
                                                            Specialization
                                                        </th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                            <%#Eval("specializationName")%>
                                                            </a></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <thead>
                                                    <tr>
                                                        <th >
                                                            Passed Out</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                            <%#Eval("PGCompletedYear")%>
                                                            </a></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table>
                                                <thead>
                                                    <tr>
                                                        <th >
                                                            Percentage</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <%#Eval("PGPercentage")%>
                                                            </a></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <thead>
                                                    <tr>
                                                        <th >
                                                            Mode</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                            <%#Eval("mode")%>
                                                            </a></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <thead>
                                                    <tr>
                                                        <th>
                                                            Information</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                            <%#Eval("PGCompletedinf")%>
                                                            </a></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="true" />
                                </Columns>
                              
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="dComposePreviewEdit" align="center" colspan="2"  
                            style="font-family: Calibri">
                            <asp:LinkButton runat="server" ID="LinkButton1" Text="Edit Qualification" 
                                OnClick="LinkButton1_Click" style="font-size: small"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" >
                            </td>
                    </tr>
                </table>
                                    </div>
                                    </div>
                                   


                                       <div id="Skilldetails" runat="server" class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                           <a data-toggle="collapse" data-parent="#accordion" href="#skldtls">Skills Details</a>
                                        </h4>
                                    </div>
                                    <div id="skldtls" class="panel-collapse collapse">
                                    
                <table id="tab_Skills" runat="server"  class="table table-striped table-bordered table-hover">
                    <tr>
                        <td style="font-family: Calibri">
                            </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="grid_emp_Skills" CssClass="table table-striped table-bordered table-hover" runat="server" AutoGenerateColumns="False" Width="100%"
                                DataKeyNames="skillID"
                                onrowdatabound="grid_emp_Skills_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <thead>
                                                    <tr>
                                                        <th style="width:100%;">
                                                            S.No</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                             <asp:Label ID="lblSerial_SD" runat="server"></asp:Label>
                                                            </a></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <thead>
                                                    <tr>
                                                        <th >
                                                            Skill Name</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <%#Eval("SkillName")%>
                                                            </a></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <thead>
                                                    <tr>
                                                        <th >
                                                            Exp (Yrs)</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                            <%#Eval("Experience")%>
                                                            </a></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <thead>
                                                    <tr>
                                                        <th >
                                                            Proficiency</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 120px; font-family: Calibri; font-size: medium;" >
                                                            <%#Eval("Proficiency")%>
                                                            </a></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="dComposePreviewEdit" align="center" colspan="2"  
                            style="font-family: Calibri">
                            <asp:LinkButton runat="server" ID="lbtn_skills" Text="Edit Skills" 
                                OnClick="lbtn_skills_Click" style="font-size: small"></asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="font-family: Calibri">
                            </td>
                    </tr>
                </table>
                                    </div>
                                    </div>

                                      <div id="earningdetails" runat="server" class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                           <a data-toggle="collapse" data-parent="#accordion" href="#erndtls">Earnings Details</a>
                                        </h4>
                                    </div>
                                    <div id="erndtls" class="panel-collapse collapse">
                                    
                <table id="tab_Earn" runat="server"  class="table table-striped table-bordered table-hover">
                    <tr>
                        <td style="width: 751px">
                            <asp:GridView ID="grid_Earnings" CssClass="table table-striped table-bordered table-hover" runat="server" AutoGenerateColumns="False" Width="100%"
                                DataKeyNames="EarningsId"   
                                onrowdatabound="grid_Earnings_RowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <thead>
                                                    <tr>
                                                        <th style="width: 100%;">
                                                            S.No</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                             <asp:Label ID="lblSerial_ED" runat="server"></asp:Label>
                                                            </a></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <thead>
                                                    <tr>
                                                        <th >
                                                            Earnings Code</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                            <%#Eval("EarningsCode")%>
                                                            </a></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <thead>
                                                    <tr>
                                                        <th >
                                                            Earnings Name</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                            <%#Eval("EarningsName")%>
                                                            </a></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <thead>
                                                    <tr>
                                                        <th >
                                                            Amount</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                            <%#Eval("Amount")%>
                                                            </a></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <thead>
                                                    <tr>
                                                        <th >
                                                            Eligiblity</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="grd_chk_earn" Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="dComposePreviewEdit" align="center" colspan="2"  
                            style="font-family: Calibri; height: 19px; width: 751px;">
                            <asp:LinkButton runat="server" ID="lbtn_Earnings" Text="Edit Earnings" 
                                OnClick="lbtn_Earnings_Click" style="font-size: small"></asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            </td>
                    </tr>
                </table>
                                    </div>
                                    </div>

                                       <div id="deductiondetails" runat="server" class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                           <a data-toggle="collapse" data-parent="#accordion" href="#detudtls">Deduction Details</a>
                                        </h4>
                                    </div>
                                    <div id="detudtls" class="panel-collapse collapse">
                                    
                <table id="tab_Dedu" runat="server"  class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>
                            
                            <asp:GridView ID="grd_Deducation" CssClass="table table-striped table-bordered table-hover" runat="server" AutoGenerateColumns="False" Width="100%"
                                DataKeyNames="DeductionId" 
                                onselectedindexchanged="grd_Deducation_SelectedIndexChanged"  
                                onrowdatabound="grd_Deducation_RowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <thead>
                                                    <tr>
                                                        <th style="width:100%;">
                                                            S.No</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                             <asp:Label ID="lblSerial_DD" runat="server"></asp:Label>
                                                            </a></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <thead>
                                                    <tr>
                                                        <th >
                                                            Deduction Code</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                            <%#Eval("DeducationCode")%>
                                                            </a></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <thead>
                                                    <tr>
                                                        <th >
                                                            Deduction Name</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                            <%#Eval("DeductionName")%>
                                                            </a></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                             
                             
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <thead>
                                                    <tr>
                                                        <th >
                                                            Amount</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                            <%#Eval("Amount")%>
                                                            </a></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                             
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table  width="100%">
                                                <thead>
                                                    <tr>
                                                        <th >
                                                            Eligiblity</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="grd_chk_ded" Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="dComposePreviewEdit" align="center" colspan="2"  
                            style="font-family: Calibri">
                            <asp:LinkButton runat="server" ID="lbtn_Deduction" Text="Edit Deduction" 
                                OnClick="lbtn_Deduction_Click" style="font-size: small"></asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td colspan="4" >
                            </td>
                    </tr>
                </table>
                                    </div>
                                    </div>



                                      <div id="overheadcosting" runat="server" class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                           <a data-toggle="collapse" data-parent="#accordion" href="#ovrhd">Overhead Costing Details</a>
                                        </h4>
                                    </div>
                                    <div id="ovrhd" class="panel-collapse collapse">
                                    
                 <table id="tab_Overhead" runat="server"  class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>
                            <asp:GridView ID="gdv_overhead" runat="server" AutoGenerateColumns="False" Width="100%"
                                DataKeyNames="OverHeadingId" CssClass="table table-striped table-bordered table-hover"
                                onrowdatabound="gdv_overhead_RowDataBound" >
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <thead>
                                                    <tr>
                                                        <th style="width:100%;">
                                                            S.No</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                             <asp:Label ID="lblSerial_OH" runat="server"></asp:Label>
                                                            </a></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <thead>
                                                    <tr>
                                                        <th >
                                                            OverHeading Cost Code</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                            <%#Eval("Overheadingid")%>
                                                            </a></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <thead>
                                                    <tr>
                                                        <th >
                                                            OverHeading Cost Name</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                            <%#Eval("OverheadingName")%>
                                                            </a></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                             
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <thead>
                                                    <tr>
                                                        <th>
                                                            Amount</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                            <%#Eval("Amount")%>
                                                            </a></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="dComposePreviewEdit" align="center" colspan="2"  
                            style="font-family: Calibri">
                            <asp:LinkButton runat="server" ID="LinkButton4" Text="Edit OverHeadingCost" 
                                style="font-size: small" onclick="LinkButton4_Click"></asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td colspan="4" >
                            </td>
                    </tr>
                </table>
                                    </div>
                                    </div>

                                     <%--  <div id="training" runat="server" class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                           <a data-toggle="collapse" data-parent="#accordion" href="#train">Training Details</a>
                                        </h4>
                                    </div>
                                    <div id="train" class="panel-collapse collapse">
                                    
                <table id="tab_Train" runat="server"  class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>
                            <asp:GridView ID="grid_Training" runat="server" Width="100%" AutoGenerateColumns="False"
                                DataKeyNames="TrainingID" CellPadding="4" 
                              GridLines="None" ShowFooter="True" 
                                onrowdatabound="grid_Training_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                <colgroup>
                                                    <col class="dInboxContentTableCheckBoxCol">
                                                </colgroup>
                                                <thead>
                                                    <tr>
                                                        <th >
                                                            S.No</th>
                                                        <th >
                                                            Institution Name</th>
                                                        <th >
                                                            Program Name</th>
                                                        <th >
                                                            Program type</th>
                                                        <th >
                                                            Trainer Name</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table class="dItemListContentTable" cellspacing="0" cellpadding="0" width="100%">
                                                <colgroup>
                                                    <col class="dInboxContentTableCheckBoxCol">
                                                </colgroup>
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                         <asp:Label ID="lblSerial_TD" runat="server"></asp:Label>   
                                                        </td>
                                                        <td>
                                                            <%#Eval("InstitutionName")%>
                                                        </td>
                                                        <td >
                                                            <%#Eval("prgmname")%>
                                                        </td>
                                                        <td>
                                                            <%#Eval("prgmtypName")%>
                                                        </td>
                                                        <td>
                                                            <%#Eval("trnrName")%>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="dComposePreviewEdit" align="center" colspan="2"  
                            >
                            <asp:LinkButton runat="server" ID="lbtn_training" Text="Edit Training" 
                                OnClick="lbtn_training_Click" style="font-size: small"></asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td colspan="4" >
                            </td>
                    </tr>
                </table>
                                    </div>
                                    </div>--%>

                                      <div id="asset" runat="server" class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                           <a data-toggle="collapse" data-parent="#accordion" href="#astdt">Asset Details</a>
                                        </h4>
                                    </div>
                                    <div id="astdt" class="panel-collapse collapse">
                                    
                <table id="tab_asset" runat="server"  class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>
                            <asp:GridView ID="grid_aseet" CssClass="table table-striped table-bordered table-hover" runat="server" Width="100%" AutoGenerateColumns="False"
                                DataKeyNames="AssetId" onrowdatabound="grid_aseet_RowDataBound">
                                <RowStyle  />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <thead>
                                                    <tr>
                                                        <th style="width:100%;">
                                                            S.No</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table  width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                             <asp:Label ID="lblSerial_AD" runat="server"></asp:Label>
                                                            </a></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <thead>
                                                    <tr >
                                                        <th >
                                                           Asset Name</th>
                                                        <th >
                                                            Asset No</th>
                                                     
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                            <%#Eval("AssetName")%>
                                                        </td>
                                                        <td >
                                                            <%#Eval("AssetNo")%>
                                                        </td>
                                                        
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="dComposePreviewEdit" align="center" colspan="2" >

                            <b><asp:LinkButton runat="server" ID="LinkButton5" Text="Edit Asset"  
                                style="font-size: small" onclick="LinkButton5_Click"></asp:LinkButton></b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" >
                            </td>
                    </tr>
                </table>
                                    </div>
                                    </div>

                                    <div id="transportdetails" runat="server" class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                           <a data-toggle="collapse" data-parent="#accordion" href="#trans">Transport Details</a>
                                        </h4>
                                    </div>
                                    <div id="trans" class="panel-collapse collapse">
                                    
                <table id="Table_bus" class= "table table-striped table-bordered table-hover" runat="server">
                 <tr>
                        <td>
                            <asp:GridView ID="Grid_bus" runat="server" Width="80%" 
                                AutoGenerateColumns="False" GridLines="None">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table class= "table table-striped table-bordered table-hover" width="100%">

                                                <thead>
                                                    <tr >
                                                    
                                                        <td style="width:20%">
                                                            Area Name</td>
                                                        <td style="width:20%">
                                                             Bus Number</td>
                                                        <td style="width:20%">
                                                          Vehicle Number</td>
                                                        <td style="width:20%">
                                                       Boarding Point</td>
                                                        <td style="width:20%">
                                                           Driver Name</td>
                                                           
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table class="table table-striped table-bordered table-hover" width="100%">

                                                <tbody>
                                                    <tr style="width:100%">
                                                        <td style="width:20%">
                                                            <%#Eval("Area_name")%>
                                                        </td>
                                                        <td style="width:20%">
                                                            <%#Eval("Veh_id")%>
                                                        </td>
                                                        <td style="width:20%">
                                                            <%#Eval("Veh_number")%>
                                                        </td>
                                                        <td style="width:20%">
                                                            <%#Eval("Boarding_point")%>
                                                        </td>
                                                         <td style="width:20%">
                                                            <%#Eval("Driver_name")%>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                    <td>
                    
                    <asp:Label ID="lbl_transport" runat="server" ></asp:Label>
                    </td>
                    </tr>
                    <tr>
                        <td class="dComposePreviewEdit" align="center" colspan="2">
                            <asp:LinkButton runat="server" ID="Lnk_edit_bus" Text="Edit Transport details" 
                              style="font-size: small" onclick="Lnk_edit_bus_Click"></asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td colspan="4" >
                            </td>
                    </tr>
                </table>
                                    </div>
                                    </div>


                                       <div id="empphoto" runat="server" class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                           <a data-toggle="collapse" data-parent="#accordion" href="#emppto">Employee Photo</a>
                                        </h4>
                                    </div>
                                    <div id="emppto" class="panel-collapse collapse">
                                    
                <table  id="tab_photo" runat="server" 
                     class="table table-striped table-bordered table-hover">
                    <tr>
                        <td class="dComposeItemLabel">
                            </td>
                        <td align="center" >
                            </td>
                        <td colspan="2" >
                            </td>
                    </tr>
                    <tr>
                        <td class="dComposeItemLabel"  >
                            </td>
                        <td align="center" >
                            <asp:Image BorderWidth="1" ID="img_emp_photo" runat="server" Height="149px" Width="152px"
                                BorderColor="Navy" BorderStyle="Dotted" /></td>
                        <td colspan="2" style="font-family: Calibri; width: 73px;">
                            </td>
                    </tr>
                    <tr>
                    <td  style="width: 22%">
                        </td>
                        <td class="dComposePreviewEdit" align="center" colspan="2"  
                            style="font-family: Calibri">
                            <asp:LinkButton runat="server" ID="LinkButton2" Text="Edit Photo" 
                                OnClick="LinkButton2_Click" ></asp:LinkButton></td>
                        <td class="dComposePreviewVal">
                        </td>
                    </tr>
                    <tr>
                    <td  style="width: 22%">
                        </td>
                        <td class="dComposePreviewEdit" align="center" colspan="2"  >
                            </td>
                        <td class="dComposePreviewVal">
                            </td>
                    </tr>
                </table>
                                    </div>
                                    </div>


                          </div>

                          <center>
                          <div> <asp:Button ID="btn_Back" runat="server" CssClass="btn btn-primary" OnClick="btn_Back_Click" Text="Back" /></div>
                    </center>

</asp:Content>
