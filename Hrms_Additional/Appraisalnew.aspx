<%@ Page MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Appraisalnew.aspx.cs" Inherits="Hrms_Additional_Default" Title="ePay-HRMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<%--<%@ Register src="starrating.ascx" tagname="RatingControl" tagprefix="uc1" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .myButton{
    background:url("../Images/Back.png");
}
.myButton:hover{
    background:url("../Images/Backover.png");
}
    .rateStar
    {
	white-space:nowrap;
	margin:1em;
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
    	 .SliderHandle
        {
            position: absolute;
            height: 22px;
            width: 10px;
        }
        .SliderRail
        {
            position: relative;
            height: 22px;
            width: 150px;
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
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    

     <div ><h3 class="page-header">Performance Appraisal</h3></div>
                     <div></div>
                                    <div>
                                    <table cellpadding="1%" cellspacing="1%" width="100%" class="table table-striped table-bordered table-hover">
                        
                        <tr>
                            <td style="height: 48px">
                                Select Department</td>
                            <td style="height: 48px" >
                            <span style="font-size: x-small">
                            <span class="style5"><span class="style6"><span class="style8">
                            <span class="style9"><asp:DropDownList ID="ddl_department" runat="server" CssClass="form-control"
                                AutoPostBack="True" OnSelectedIndexChanged="ddl_department_SelectedIndexChanged">
                            </asp:DropDownList>
                            </span></span></span></span>
                            </span>
                            </td>
                            <td style="height: 48px" >
                                Select Employee</td>
                            <td style="height: 48px" >
                            <span style="font-size: x-small">
                            <span class="style5"><span class="style6"><span class="style8">
                            <span class="style9"><asp:DropDownList ID="ddl_Employee" runat="server" CssClass="form-control" AutoPostBack="True"
                                OnSelectedIndexChanged="ddl_Employee_SelectedIndexChanged">
                            </asp:DropDownList>
                            </span></span></span></span>
                            </span>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Appraisal Type</td>
                            <td >
                            <asp:RadioButtonList ID="rdo_Appraisallist" runat="server" RepeatDirection="Horizontal"
                                CssClass="dComposeItemLabel" Width="271px" AutoPostBack="True" 
                                OnSelectedIndexChanged="rdo_Appraisallist_SelectedIndexChanged" 
                                Font-Names="Calibri">
                                <asp:ListItem Value="180" Selected="True">Appraisal 180</asp:ListItem>
                                <asp:ListItem Value="360">Appraisal 360</asp:ListItem>
                            </asp:RadioButtonList>
                            </td>
                            <td >
                                Date</td>
                            <td >
                            <asp:TextBox runat="server" id="txt_date"
                                onkeyup="fn_date(event,this.id);" maxlength="10" 
                                CssClass="form-control" ontextchanged="txt_date_TextChanged"></asp:TextBox>
    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_date" Animated="true" Format="dd/MM/yyyy">
    </asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="txt_date" ErrorMessage="Please Enter the Date" 
                                Font-Names="Calibri"></asp:RequiredFieldValidator>
                               </td>
                            <td colspan="2" align="right">
                                &nbsp;</td>
                        </tr>
                     </table>
                     </div>
                     <div >
                     <table style="width: 100%">
                       <tr valign="top">
                         <td colspan="3">
                            <asp:GridView ID="gvd_task" runat="server" AllowSorting="True" 
            AutoGenerateColumns="False" class="table table-striped table-bordered table-hover" GridLines="None"  >
            
                           
                                    
            <Columns>
            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Task ID">                
                    <ItemTemplate>
                        <asp:Label ID="lbl_id" runat="server" Text='<%# Eval("TaskID") %>'></asp:Label>
                    </ItemTemplate>                

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                 <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Task Title">
                
                    <ItemTemplate>
                        <asp:Label ID="lbl_title" runat="server" Text='<%# Eval("TSubject") %>'></asp:Label>
                    </ItemTemplate>                

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label ID="lbl_desc" runat="server" Text='<%# Eval("TDescription") %>'></asp:Label>
                    </ItemTemplate>                  

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Department">
                    <ItemTemplate>
                        <asp:Label ID="lbl_dept" runat="server" Text='<%# Eval("Dept") %>'></asp:Label>
                    </ItemTemplate>
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="lbl_status" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="DOA">
                    <ItemTemplate>
                        <asp:Label ID="lbl_doa" runat="server" Text='<%# Eval("DOA") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="DOC">
                    <ItemTemplate>
                        <asp:Label ID="lbl_doc" runat="server" Text='<%# Eval("DOC") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Submitted Date">
                    <ItemTemplate>
                        <asp:Label ID="lbl_date" runat="server" Text='<%# Eval("submitted_date","{0:M/dd/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Rating">
                <ItemTemplate>   
                    <asp:Rating ID="Rating1" runat="server" CurrentRating='<%# String.IsNullOrEmpty(Eval("rating").ToString())?0:Eval("rating") %>' 
                                MaxRating="10" CssClass="rateStar" StarCssClass="rateItem" 
                                WaitingStarCssClass="SaveStar" FilledStarCssClass="FillStar" 
                                EmptyStarCssClass="EmptyStar"  AutoPostBack="false" 
                                Direction="LeftToRight" Height="16px" Width="142px">
                            </asp:Rating>          
                        </ItemTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>

                </asp:TemplateField>
                 <asp:TemplateField ItemStyle-HorizontalAlign="Center" ControlStyle-Font-Bold="true" ControlStyle-Font-Size="Larger" HeaderText="Efficiency">
                <ItemTemplate>                            
                           <asp:Label ID="lbl_efficiency" runat="server" Font-Size="Larger"></asp:Label>
                        </ItemTemplate>

<ControlStyle Font-Bold="True" Font-Size="Larger"></ControlStyle>

<ItemStyle HorizontalAlign="Center" Font-Size="XX-Large"></ItemStyle>

                </asp:TemplateField>
              
            </Columns>
          
                                <EditRowStyle  />
            <AlternatingRowStyle />

        </asp:GridView>
                        </td>
                    </tr>
                         
                  </table>
                  </div>

                  <div style="width: 70%">
                     <table style="width: 100%">
                       <tr valign="top">
                         <td>
                            <asp:GridView ID="grid_appraisal" runat="server" AutoGenerateColumns="False"
                                DataKeyNames="AppraisalID" CellPadding="4" class="table table-striped table-bordered table-hover"  
                                GridLines="None">
                               
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table cellspacing="0" cellpadding="0" >
                                                <colgroup>
                                                    <col>
                                                </colgroup>
                                                <thead>
                                                    <tr>
                                                        <th>
                                                            Appraisal Questions
                                                        </th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table >
                                                <colgroup>
                                                    <col >
                                                </colgroup>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("AppraisalName")%>' ></asp:Label>
                                                           
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText = "Rating" >
                                        
                                        <ItemTemplate>
                                            <table>
                                               <tr>
                                                 <td>
                                                            
                                                     <asp:TextBox ID="txtslidetarget" runat="server" Visible="True" ></asp:TextBox>
                                                 </td>
                                               </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                    
                                    <asp:TemplateField HeaderText = "Points" >
                                        <ItemTemplate>
                                        <asp:TextBox ID="txtslidebound" runat="server" Visible="true" AutoPostBack="true" ForeColor="White" Font-Bold="True" Width="50px" BackColor="#333333"></asp:TextBox>
                                                            <asp:SliderExtender ID="SliderExtender1" runat="server" HandleCssClass="SliderHandle" TargetControlID="txtslidetarget" Length="150" Decimals="1" Minimum="0" Maximum="10" EnableHandleAnimation="true" BoundControlID="txtslidebound" HandleImageUrl="../Images/slider-handle.gif">
                                                            </asp:SliderExtender>
                                                            
                                                            <asp:DropDownList runat="server" ID="rdo_appraisalrating" OnSelectedIndexChanged="rdo_appraisalrating_SelectedIndexChanged"
                                                                AutoPostBack="true" Visible="false">
                                                                <asp:ListItem Value="0">0</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                                <asp:ListItem Value="6">6</asp:ListItem>
                                                                <asp:ListItem Value="7">7</asp:ListItem>
                                                                <asp:ListItem Value="8">8</asp:ListItem>
                                                                <asp:ListItem Value="9">9</asp:ListItem>
                                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                   
                                </Columns>
                                
                            </asp:GridView>
                        </td>
                    </tr>
                         
                       <tr valign="top">
                         <td>
                            <asp:Button ID="but_cal" runat="server" onclick="but_cal_Click" 
                                Text="Calculate"  class="btn btn-success"/>
                        </td>
                    </tr>
                         
                  </table>
                  </div>
                  <div style="width: 70%" align="center"> 
    </div>
                  <div runat="server" id="allotment" style="width: 70%">
                                    <table  class="table table-striped table-bordered table-hover">
                        
                        <tr>
                            <td> 
                                Total Value Points</td>
                            <td>
                           <input runat="server" id="txttot_pts" disabled="disabled" class="form-control"/></td>
                            <td>
                                <a href="Annual_increment.aspx"><span class="style6">Allot increment for this 
                                employee</span></a></td>
                        </tr>
                        <tr>
                            <td >
                            
                            <asp:Label ID="lbl_mode" runat="server" Text="Max. Alloted Amount"></asp:Label>
                            </td>
                            <td >
                            <asp:TextBox ID="txt_amt" runat="server" ontextchanged="txt_amt_TextChanged"  CssClass="form-control"></asp:TextBox>
                            </span>
                            </td>
                            <td >
                            <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click" 
                                Visible="False">View Slab</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="right">
                            <asp:Button ID="btn_Back" runat="server" Text="Back" OnClick="btn_Back_Click" CausesValidation="False"  class="btn btn-info"/>
                            <asp:Button ID="btn_save" runat="server" Text="Save" OnClick="btn_save_Click" class="btn btn-success"/>

                           
                            <asp:Button ID="btn_update" runat="server" Text="Update" onclick="btn_update_Click"  class="btn btn-info"/>
                               </td>
                            <td align="right">
                            <asp:TextBox ID="TextBox1" runat="server" Visible="False" Enabled="False" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                     </table>
                     </div>


    </asp:Content>
