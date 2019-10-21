<%@ Page Language="C#" MasterPageFile="~/HRMS.master"
    AutoEventWireup="true" CodeFile="Appraisal1.aspx.cs" Inherits="Hrms_Master_Appraisal_Default"
    Title="ePay-HRMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div ><h3 class="page-header">Appraisal Setup</h3></div>
                     <div></div>
                                    <div >
                                    <table class="table table-striped table-bordered table-hover">
                        <tr>
                            <td style="height: 48px">
                                Appraisal Type<span style="color: #FF0000">*</span></td>
                            <td style="height: 48px" >
                            <asp:RadioButtonList ID="rdo_type" runat="server" RepeatDirection="Horizontal" CssClass="dComposeItemLabel" AutoPostBack="True" 
                                OnSelectedIndexChanged="rdo_type_SelectedIndexChanged">
                                <asp:ListItem Value="180" Selected="True">Appraisal 180</asp:ListItem>
                                <asp:ListItem Value="360">Appraisal 360</asp:ListItem>
                            </asp:RadioButtonList></td>
                            <td style="height: 48px" >
                                &nbsp;</td>
                            <td style="height: 48px" >
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td >
                                Department<span style="color: #FF0000">*</span></td>
                            <td >
                        <div id="diva" align="center" style="overflow: auto; Height: 275px;">
                            <asp:CheckBoxList ID="chk_Department" runat="server"  CssClass="form-control" onselectedindexchanged="chk_Department_SelectedIndexChanged">
                            </asp:CheckBoxList>
                            </div>
                            <asp:DropDownList ID="ddl_department" runat="server" CssClass="form-control"
                                AutoPostBack="True" OnSelectedIndexChanged="ddl_department_SelectedIndexChanged" Visible="false" 
                                Height="20px" Width="212px">
                            </asp:DropDownList></td>
                            <td >
                                Criteria</td>
                            <td >
                            <div>
                                <asp:CheckBoxList ID="chk_Certeria" runat="server" CssClass="form-control" >
                                <%--<asp:ListItem Value="st">Select Type</asp:ListItem>--%>
                                <asp:ListItem Value="1">Team members</asp:ListItem>
                                <asp:ListItem Value="2">Superior</asp:ListItem>
                                <asp:ListItem Value="3">Customers</asp:ListItem>
                                <asp:ListItem Value="4">Suppliers</asp:ListItem>
                                <asp:ListItem Value="5">Vendors</asp:ListItem>
                                <asp:ListItem Value="6">Peers</asp:ListItem>
                                <asp:ListItem Value="7">Subordinate</asp:ListItem>
                            </asp:CheckBoxList>
                            </div>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Evaluation Point<span style="color: #FF0000">*</span></td>
                            <td >
                            <textarea id="txt_appraisalname" runat="server" class="InputDefaultStyle" style="width: 304px;
                                height: 46px" cols="20" name="S1" rows="1"></textarea></td>
                            <td >
                                &nbsp;</td>
                            <td >
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td >
                                &nbsp;</td>
                            <td align="right" >
                            <asp:Button ID="btn_add" runat="server" class="btn btn-success"  Text="Save" 
                                    onclick="btn_add_Click" />
                                <asp:Button ID="btn_modify" runat="server" class="btn btn-info"  Text="Modify" 
                                    onclick="btn_modify_Click" />
                            </td>
                            <td >
                                &nbsp;</td>
                            <td >
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="4" align="right">
                                &nbsp;</td>
                        </tr>
                     </table>
                     </div>
                     <div style="width: 70%">
                     <table style="width: 100%">
                       <tr valign="top">
                         <td>
                                        <asp:GridView ID="grid_appraisal" runat="server" AutoGenerateColumns="False" Width="100%" OnRowEditing="Edit" DataKeyNames="AppraisalID" 
                                            onselectedindexchanged="grid_appraisal_SelectedIndexChanged" onrowcommand="grid_appraisal_RowCommand" CssClass="table table-striped table-bordered table-hover"
                                            onrowdeleted="grid_appraisal_RowDeleted" onrowdeleting="grid_appraisal_RowDeleting" GridLines="None" ShowFooter="True">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Department">
                                                <ItemTemplate>
                                                <%#Eval("Departmentname")%>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Evaluation Point">
                                                <ItemTemplate>
                                                    
                                               <input runat="server"  id="txtgrid" value='<%#Eval("AppraisalName")%>' />
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Type">
                                                <ItemTemplate>
                                                <%#Eval("MaxDays")%>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                 <asp:LinkButton id="img_update"  runat="server"  AlternateText="" class="btn btn-info btn-circle" CommandName="Edit"><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                <asp:LinkButton id="imgdel" runat="server" CommandName="Delete" CssClass="btn btn-danger btn-circle glyphicon glyphicon-minus-sign" Visible="true" ></asp:LinkButton>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                        
                                               
                                            </Columns>
                                            
                                        </asp:GridView>
                        </td>
                    </tr>
                       <tr valign="top">
                         <td align="center">
                         <asp:Button ID="btn_assign" runat="server" class="btn btn-info"  Text="Assign" 
                                 onclick="btn_assign_Click"  />
                        </td>
                    </tr>
                         </table>
                  </div>
                  </td>                                    
           


    </asp:Content>
