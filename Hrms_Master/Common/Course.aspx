<%@ Page Language="C#" MasterPageFile="~/HRMS.master"
    AutoEventWireup="true" CodeFile="Course.aspx.cs" Inherits="Hrms_Master_Default" Title="Welcome to HRMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
    <h2 class="page-header">Courses</h2></div>

    <div style="float:right;"> 
    <asp:DropDownList ID="ddl_branch" runat="server" AutoPostBack="True"
       onselectedindexchanged="ddl_branch_SelectedIndexChanged" Width="115px">
      </asp:DropDownList></div>
      <div><h3> &nbsp;</h3></div>
    <table width="100%" cellpadding="0" cellspacing="0" >
        <tr valign="top">
            <td id="tdComposeHeader" valign="top" align="right">
               
                <table cellpadding="2%" cellspacing="1%" width="100%" class="table">
                  <tr>
                        <td align="right" width="45%" >
                            <b>Degree / Diploma / Course Name&nbsp;&nbsp;&nbsp; </b></td>
                        <td  align="center" width="25%" >
                            <input class="form-control" runat="server" id="CourseName" onkeypress="return isNumberKey(event)"                               
                                maxlength="20" /></td>
                        <td   align="left" width="33%" >
                            <asp:Button ID="Button1" runat="server"  OnClientClick="return fnSave();" CssClass="btn btn-success"
                                Text="Add" OnClick="Button1_Click1"  />
                            </td>
                       
                    </tr>
                    <tr ><td colspan="4"></td></tr>
                    
                    </table>
                    </td>
                    </tr>
                     <tr valign="top">
            <td id="td1" valign="top" align="right">
                    <table cellpadding="2%" cellspacing="1%" width="100%">
                    <tr>
                        <td colspan="4" align="center">
                           
                                <asp:GridView ID="grid_Course" runat="server" AutoGenerateColumns="False" Width="60%"
                                    DataKeyNames="CourseId" OnRowEditing="Edit" OnRowUpdating="Update" 
                                    onselectedindexchanged="grid_Course_SelectedIndexChanged" 
                                    onrowcommand="grid_Course_RowCommand"  CellPadding="4" CssClass="table table-striped table-bordered table-hover"
                                 GridLines="None" onrowdeleting="grid_Course_RowDeleting">
                                    <RowStyle  />
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <table cellspacing="0" cellpadding="0" width="100%">
                                                    <%--<colgroup>
                                                        <col>
                                                    </colgroup>--%>
                                                    <thead>
                                                        <tr>
                                                            <th align="left" style="width: 86%; ">
                                                                Course List</th>
                                                            <td  style="width: 7%; ">
                                                                <asp:Label ID="lbledit" Text="Edit" runat="server"></asp:Label></td>
                                                                 <td id="del" style="width: 7%; ">
                                                               <asp:Label ID="lbldel" Text="Delete" runat="server"></asp:Label></td>
                                                        </tr>
                                                    </thead>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table class="dItemListContentTable" border="0" cellspacing="0" cellpadding="0" width="100%">
                                                    <colgroup>
                                                        <col class="dInboxContentTableCheckBoxCol">
                                                    </colgroup>
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 86%;">
                                                                <input runat="server" id="txtgrid" value='<%# Eval("CourseName") %>' disabled="disabled"  class="form-control"  style="width:100%;" />
                                                             </td>
                                                            <td align="center"  >
                                                                <asp:LinkButton ID="img_update"  runat="server"  CssClass="btn btn-success btn-circle glyphicon glyphicon-check" CommandName="Update"></asp:LinkButton>
                                                                <asp:LinkButton ID="img_save" runat="server"  CssClass="btn btn-success btn-circle glyphicon glyphicon-plus-sign" AlternateText="" CommandName="Edit" Visible="false"></asp:LinkButton>
                                                             </td>
                                                             <td style="width:7%; " align="center">
                                                                <asp:LinkButton ID="imgdel" CssClass="btn btn-danger btn-circle glyphicon glyphicon-minus-sign"  runat="server"
                                                                 CommandName="Delete" OnClientClick="return validate()"></asp:LinkButton>
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
                   
                    </table>
                    </td>
                    </tr>
        <tr>
            <td align="center" >
                &nbsp;</td>
        </tr>
         <tr>
            <td>
                <input id="hCourseID" runat="server" type="hidden" value="0" />
                <input type="hidden" id="ToolBarCode" name="ToolBarCode" runat="server" value="0" /></td>
        </tr>
    </table>
</asp:Content>







