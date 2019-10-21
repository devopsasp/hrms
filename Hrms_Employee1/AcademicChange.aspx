<%@ Page Title="" Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="AcademicChange.aspx.cs" Inherits="Hrms_Employee_AcademicChange" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language="javascript" type="text/javascript">

        function selectAll(invoker) {
            var inputElements = document.getElementsByTagName('input');
            for (var i = 0; i < inputElements.length; i++) {
                var myElement = inputElements[i];
                if (myElement.type === "checkbox") {
                    myElement.checked = invoker.checked;
                }
            }
        }

    function fn_chkall(chkid, chklistid) {

        var chkBoxList = document.getElementById(chklistid);
        var chkBoxCount = chkBoxList.getElementsByTagName("input");

        if (document.getElementById(chkid).checked == true) {
            for (var i = 0; i < chkBoxCount.length; i++) {
                chkBoxCount[i].checked = true;
            }
        }
        else {
            for (var i = 0; i < chkBoxCount.length; i++) {
                chkBoxCount[i].checked = false;
            }
        }
    }       
    
function chkall_onclick() {

}

    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<div class="row">
                <div class="col-lg-12">
                    <h2 class="page-header">Students Academic Change</h2>
                </div>
                <!-- /.col-lg-12 -->               
            </div>
            <div class="panel panel-default">
              <div class="panel-heading">
              Academic Change
              </div>
              <asp:UpdatePanel ID="UpdatePanel1" runat="server">
               <ContentTemplate>
                <div> 
                 <table style="width: 100%">
                 <tr>
                 <td>
                  <table id="studentlist" runat="server" class="table table-striped table-bordered table-hover">
                                         <tr>
                                             <td style="width: 241px">
                                                 
                                                 <asp:Label ID="lbl_error" runat="server" Font-Bold="True" Font-Size="Medium" 
                                                     ForeColor="Red" Width="75%"></asp:Label>
                                                 
                                                 <br />
                                                 <%--<input type="checkbox" id="chkall" runat="server" 
                                                     onclick="javascript: fn_chkall(this.id,'ctl00_ContentPlaceHolder1_chk_StudentCode')" visible="True" onclick="return chkall_onclick()" />--%>
                                                <asp:CheckBox ID="chkall" runat="server" Text="Select All" 
                                                     OnClick="selectAll(this)" Visible="False" />
                                             </td>
                                         </tr>
                                         <tr>
                                             <td style="width: 241px">
                                             <div id="diva" runat="server" align="left" style="overflow: auto; height: 340px;">
                                                 <asp:CheckBoxList ID="chk_StudentCode"  runat="server" AutoPostBack="true"  oncheckedchanged="chk_StudentCode_CheckedChanged"  class="table table-striped table-bordered table-hover" Width="90%">
                                                     
                                                 </asp:CheckBoxList>
                                                 </div>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td style="width: 241px">
                                                 &nbsp;</td>
                                         </tr>
                                     </table>
                 </td>
                 <td>
                  <div align="center" id="morris-area-chart" style="width: 100%">
                    <table align="center" class="table table-striped table-bordered table-hover" 
                                             style="width: 90%">
                      <tr>
                       <td>Course</td><td>
                      <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control" 
                          Width="150px" DataSourceID="SqlDataSource1" DataTextField="CourseName" 
                          DataValueField="pn_CourseID">
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                          ConnectionString="<%$ ConnectionStrings:connectionstring %>" 
                          SelectCommand="SELECT * FROM [Student_Course]"></asp:SqlDataSource>
                  </td>
                  </tr>
                  <tr>
                  <td>Department</td>
                      <td>
                          <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" 
                              Width="150px" DataSourceID="SqlDataSource2" DataTextField="DepartmentName" 
                              DataValueField="DepartmentName">
                          </asp:DropDownList>
                          <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                              ConnectionString="<%$ ConnectionStrings:connectionstring %>" 
                              SelectCommand="SELECT * FROM [Student_Department]"></asp:SqlDataSource>
                      </td>
                  </tr>
                  <tr>
                   <td>Section</td>
                          <td>
                              <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control" Width="150">
                               <asp:ListItem Selected="True">Select</asp:ListItem>
                               <asp:ListItem>A</asp:ListItem>
                               <asp:ListItem>B</asp:ListItem>
                              </asp:DropDownList>
                          </td>                  
                  </tr>
                  <tr>
                   <td>Current Year</td><td>
                      <asp:DropDownList ID="ddlCurrentyear" runat="server" CssClass="form-control" AutoPostBack="True" 
                          Width="150" onselectedindexchanged="ddlCurrentyear_SelectedIndexChanged">
                       <asp:ListItem Selected="True">Select</asp:ListItem>
                       <asp:ListItem>1</asp:ListItem>
                       <asp:ListItem>2</asp:ListItem>
                       <asp:ListItem>3</asp:ListItem>
                       <asp:ListItem>4</asp:ListItem> 
                       <asp:ListItem>Alumni</asp:ListItem>                                                   
                      </asp:DropDownList>
                  </td>
                  </tr>
                  <tr>
                    <td>Academic Change</td><td>
                      <asp:DropDownList ID="ddlChangeAcademic" runat="server" CssClass="form-control" 
                          Width="150">
                      <asp:ListItem Selected="True">Select</asp:ListItem>
                       <asp:ListItem>1</asp:ListItem>
                       <asp:ListItem>2</asp:ListItem>
                       <asp:ListItem>3</asp:ListItem>
                       <asp:ListItem>4</asp:ListItem> 
                       <asp:ListItem>Alumni</asp:ListItem>                      
                      </asp:DropDownList>
                  </td>                   
                  </tr>
                  <tr>
                  <td colspan="6">
                  <asp:Button ID="btnUpdate" runat="server" Text="Update" class="btn btn-success" 
                          onclick="btnUpdate_Click" Visible="false"/>
                          <asp:Button ID="btnSelectChange" runat="server"  Text="Clear Selection" 
                          class="btn btn-info" onclick="btnSelectChange_Click" Visible="false"  />
                  </td>                   
                  </tr>   
                   </table>                                         
                                             
                 </div>
                 </td>
                 </tr>
                 </table>  
                </div>
               </ContentTemplate>
               </asp:UpdatePanel>
              </div>
              
</asp:Content>

