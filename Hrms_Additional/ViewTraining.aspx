<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewTraining.aspx.cs" MaintainScrollPositionOnPostback="true" MasterPageFile="~/HRMS.master" Inherits="Hrms_Additional_ViewTraining" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../datecheck.js"></script>
    <script type ="text/javascript">
    function Change(obj, evt)
    {
        if(evt.type=="focus")
            obj.style.borderColor="#E6D78A";
        else if(evt.type=="blur")
           obj.style.borderColor="#989898";
    }
   </script>
<style type="text/css">
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
        background-image: url("ratingfilled.png");
    }
    .EmptyStar 
    {
        background-image: url("ratingempty.png");
    }
    .SaveStar
    {
        background-image: url(ratingsaved.png);
    }
    .style12
         {
             height: 33px;
         }
         .style13
         {
             width: 250px;
             height: 33px;
         }
    </style>

<table cellpadding="0%" cellspacing="0%" width="100%">
 
<tr> <td align="left" style="font-family:Calibri;font-size: small; class="style123" 
                              colspan="2">
                           <span class="Title">
                           <span 
                                style="height: 17px; font-family: Calibri; font-size: medium; font-weight: bold; ">&nbsp;&nbsp;&nbsp;<h3>Employee Training Details</h3>                             
                             </span>
                             <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                </asp:ToolkitScriptManager>
                </span>
                             </td></tr>
</table>
<table cellpadding="0%" cellspacing="0%" width="100%">
<tr> <td colspan="2" align="center">
                            &nbsp;<asp:Label ID="lbl_Error" CssClass="Error" runat="server" ForeColor="Red" Font-Bold="True" Width="40%"></asp:Label></td></tr>
</table>
<table cellpadding="0%" cellspacing="0%" width="95%">

<tr>
<td></td>

<td></td>

<td></td>

<td><asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" 
         CssClass="form-control" Visible="False"
                    >
                </asp:DropDownList>
            </td>
</tr>
<tr id="tr1" runat="server" visible="false">
<td align="right" style="font-family:Calibri;font-size: small;color: #808080;" 
        class="style12">&nbsp;</td>

<td align="right" class="style13">
    
   <%-- <asp:DropDownList ID="ddl_PrgmType" runat="server" Width="200px" 
        onfocus ="Change(this, event)" onblur ="Change(this, event)" 
        CssClass="roundedbox" style="color: #808080;">
    <asp:ListItem>Select</asp:ListItem>
    </asp:DropDownList>--%>
    </td>

<td style="font-family:Calibri;font-size: small;color: #808080;" align="center" 
        class="style12">Select&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>

<td class="style12">
    <asp:DropDownList ID="ddl_select" runat="server" Width="200px" 
        onfocus ="Change(this, event)" onblur ="Change(this, event)" 
        CssClass="form-control" style="color: #808080;" AutoPostBack="True" 
        onselectedindexchanged="ddl_select_SelectedIndexChanged" >
    <asp:ListItem>Select</asp:ListItem>
    <asp:ListItem>HR Feedback</asp:ListItem>
    <asp:ListItem>Employee Feedback</asp:ListItem>
    </asp:DropDownList>
    
    </td>
</tr>
<tr id="tr2" visible="false" runat="server">
<td align="right" style="font-family:Calibri;font-size: small;color: #808080;" 
        class="style12">&nbsp;</td>

<td align="right" class="style13">
    
   <%-- <asp:DropDownList ID="ddl_PrgmType" runat="server" Width="200px" 
        onfocus ="Change(this, event)" onblur ="Change(this, event)" 
        CssClass="roundedbox" style="color: #808080;">
    <asp:ListItem>Select</asp:ListItem>
    </asp:DropDownList>--%>
    </td>

<td style="font-family:Calibri;font-size: small;color: #808080;" align="center" 
        class="style12">Program Name&nbsp;&nbsp;&nbsp;&nbsp; </td>

<td class="style12">
    <asp:DropDownList ID="ddl_prgmtype" runat="server" Width="200px" 
        onfocus ="Change(this, event)" onblur ="Change(this, event)" 
         CssClass="form-control" style="color: #808080;" AutoPostBack="True" 
        onselectedindexchanged="ddl_prgmtype_SelectedIndexChanged">
    <asp:ListItem>Select</asp:ListItem>
    </asp:DropDownList>
    
    </td>
</tr>
<tr runat="server" id="tr3" visible="false">
<td align="right" style="font-family:Calibri;font-size: small;color: #808080;" 
        class="style12">&nbsp;</td>

<td align="right" class="style13">
    
   <%-- <asp:DropDownList ID="ddl_PrgmType" runat="server" Width="200px" 
        onfocus ="Change(this, event)" onblur ="Change(this, event)" 
        CssClass="roundedbox" style="color: #808080;">
    <asp:ListItem>Select</asp:ListItem>
    </asp:DropDownList>--%>
    </td>

<td style="font-family:Calibri;font-size: small;color: #808080;" align="center" 
        class="style12">Select Employee</td>

<td class="style12">
    <asp:DropDownList ID="ddl_employee" runat="server" Width="200px" 
        onfocus ="Change(this, event)" onblur ="Change(this, event)" 
        CssClass="form-control" style="color: #808080;" AutoPostBack="True" onselectedindexchanged="ddl_employee_SelectedIndexChanged" 
       >
    <asp:ListItem>Select</asp:ListItem>
    </asp:DropDownList>
    
    </td>
</tr>

<tr>
<td align="right" style="font-family:Calibri;font-size: small;color: #808080;" 
        class="style12">&nbsp;</td>

<td align="right" class="style13">
    
    &nbsp;</td>

<td style="font-family:Calibri;font-size: small;color: #808080;" align="center" 
        class="style12">&nbsp;</td>

<td class="style12">
    &nbsp;</td>
</tr>

</table>
<table cellpadding="0px" cellspacing="0px" width="100%">
<tr>
<td align="center" style="font-family:Calibri;font-size: small;color: #808080;" 
        class="style12">
      
           <asp:GridView ID="grid_Training" Font-Size="Small" runat="server" AllowSorting="True" 
            AutoGenerateColumns="False" Height="16px" class="table table-striped table-bordered table-hover"
            Width="858px" CellPadding="4" 
            ForeColor="#333333" 
            DataKeyNames="TrainingID" onrowcommand="grid_Training_RowCommand" 
               GridLines="None" >
            <FooterStyle  Font-Bold="True"  />
                           <RowStyle Font-Names="Calibri" Font-Size="Small" 
                   />
            <Columns>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-HorizontalAlign="Center">
                        
                            <ItemTemplate>
                                <asp:Label ID="lblempCode" runat="server" Text='<%# Eval("EmployeeId") %>'></asp:Label>
                            </ItemTemplate>
                           

        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                     <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Traning ID" HeaderStyle-HorizontalAlign="Center">
                        
                            <ItemTemplate>
                                <asp:Label ID="lblCode" runat="server" Text='<%# Eval("TrainingID") %>'></asp:Label>
                            </ItemTemplate>
                           

        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                         <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Institution Name" HeaderStyle-HorizontalAlign="Center">
                        
                            <ItemTemplate>
                                <asp:Label ID="lblInstitutionName" runat="server" Text='<%# Eval("InstitutionName") %>'></asp:Label>
                            </ItemTemplate>
                           

        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                         <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Trainer Name" HeaderStyle-HorizontalAlign="Center">
                        
                            <ItemTemplate>
                                <asp:Label ID="lbltrnrName" runat="server" Text='<%# Eval("trnrName") %>'></asp:Label>
                            </ItemTemplate>
                           

        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Employee_Name" HeaderStyle-HorizontalAlign="Center">
                        
                            <ItemTemplate>
                                <asp:Label ID="lblFirstName" runat="server" Text='<%# Eval("FirstName") %>'></asp:Label>
                            </ItemTemplate>
                           

        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

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
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:ImageButton 
                                                                     ImageUrl="../Images/Save-Icon1.JPG" ID="imgdel" runat="server" 
                                                                     CommandName="btnadd" Height="18px" />                      
                    </ItemTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
              
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle  Font-Bold="True"  />
            <HeaderStyle Font-Bold="True"  Font-Names="Calibri" 
                               Font-Size="Small" />
               <EditRowStyle />
            <AlternatingRowStyle  />

        </asp:GridView>
        
    &nbsp;</td>


</tr>
<tr>
<td align="center" style="font-family:Calibri;font-size: small;color: #808080;" 
        class="style12">
      
           <asp:GridView ID="GridView1" Font-Size="Small" runat="server" AllowSorting="True" 
            AutoGenerateColumns="False" Height="16px" 
            Width="858px" CellPadding="4" class="table table-striped table-bordered table-hover"
            ForeColor="#333333" GridLines="None" >
                       <FooterStyle  Font-Bold="True"  />
                           <RowStyle Font-Names="Calibri" Font-Size="Small"  
                            />
            <Columns>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Feed Back Question" HeaderStyle-HorizontalAlign="Center">
                        
                            <ItemTemplate>
                                <asp:Label ID="lblempCode" runat="server" Text='<%# Eval("FeedBack_ques") %>'></asp:Label>
                            </ItemTemplate>
                           

        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                     <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Feed Back Answer" HeaderStyle-HorizontalAlign="Center">
                        
                            <ItemTemplate>
                                <asp:Label ID="lblCode" runat="server" Text='<%# Eval("FeedBack_ans") %>'></asp:Label>
                            </ItemTemplate>
                           

        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

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
               
              
            </Columns>
            <PagerStyle  HorizontalAlign="Center" />
            <SelectedRowStyle Font-Bold="True"  />
            <HeaderStyle Font-Bold="True"  Font-Names="Calibri" 
                               Font-Size="Small" />
                       <EditRowStyle  />
            <AlternatingRowStyle   />

        </asp:GridView>
        
    &nbsp;</td>


</tr>
</table>
<table cellpadding="0px" cellspacing="0px" width="100%">
<tr>
<td align="center" style="font-family:Calibri;font-size: small;color: #808080;" 
        class="style12">
           <asp:GridView ID="grid_emp_feedback" Font-Size="Small" runat="server" AllowSorting="True" 
            AutoGenerateColumns="False" Height="16px" class="table table-striped table-bordered table-hover"
            Width="858px" CellPadding="4"
            ForeColor="#333333" 
        onrowcommand="grid_emp_feedback_RowCommand" 
        onrowediting="grid_emp_feedback_RowEditing" 
        onrowcancelingedit="grid_emp_feedback_RowCancelingEdit" 
        onrowupdating="grid_emp_feedback_RowUpdating" GridLines="None" ShowFooter="True">
                       <FooterStyle   Font-Bold="True" />
                           <RowStyle Font-Names="Calibri" Font-Size="Small" 
                           />
            <Columns>
            
                     <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Id">
                    <ItemTemplate>
                        <asp:Label ID="lbl_id" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                    </ItemTemplate>
                   
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                 <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false" HeaderText="Emp Id">
                    <ItemTemplate>
                        <asp:Label ID="lbl_empid" runat="server" Text='<%# Eval("Pn_employeeid") %>'></asp:Label>
                    </ItemTemplate>
                   
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                 <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Trainer Name" HeaderStyle-HorizontalAlign="Center">
                        
                            <ItemTemplate>
                                <asp:Label ID="lbl_trnrName1" runat="server" Text='<%# Eval("trnrName") %>'></asp:Label>
                            </ItemTemplate>
                           

        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Feedback Questions">
                    <ItemTemplate>
                        <asp:Label ID="lbl_que" runat="server" Text='<%# Eval("FeedbackQues") %>'></asp:Label>
                    </ItemTemplate>
                   
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Answer" ControlStyle-Width="100px">
                    <ItemTemplate>
                         <asp:TextBox ID="txt_answer" runat="server" Text='<%# Eval("Feedback_ans") %>' Width="74px"></asp:TextBox>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="txt_answer1" Enabled="true" runat="server" Text='<%# Bind("Feedback_ans") %>'  CssClass="form-control" Width="74px"></asp:TextBox>

                    </EditItemTemplate>
                    <FooterTemplate>
                            <asp:Button ID="btn_submit" CommandName="submit" Text="Submit" runat="server" class="btn btn-success"/>
                        </FooterTemplate>
                    
<ControlStyle Width="100px"></ControlStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                 
                 
                     
                     <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Rating">
                <ItemTemplate>   
                    <asp:Rating ID="Rating_emp" runat="server" CurrentRating='<%# String.IsNullOrEmpty(Eval("Rating").ToString())?0:Eval("Rating") %>' 
                                MaxRating="10" CssClass="rateStar" StarCssClass="rateItem" 
                                WaitingStarCssClass="SaveStar" FilledStarCssClass="FillStar" 
                                EmptyStarCssClass="EmptyStar"  AutoPostBack="false" 
                                Direction="LeftToRight" Height="16px" Width="142px">
                            </asp:Rating>          
                        </ItemTemplate>
                      <%--  <EditItemTemplate>
                         <asp:Rating ID="Rating_emp_edit" runat="server" CurrentRating='<%# String.IsNullOrEmpty(Eval("Rating").ToString())?0:Eval("Rating") %>' 
                                MaxRating="10" CssClass="rateStar" StarCssClass="rateItem" 
                                WaitingStarCssClass="SaveStar" FilledStarCssClass="FillStar" 
                                EmptyStarCssClass="EmptyStar"  AutoPostBack="false" 
                                Direction="LeftToRight" Height="16px" Width="142px">
                            </asp:Rating>   
                        </EditItemTemplate>--%>
                       

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>               
                <asp:CommandField  ShowEditButton="true" />
            </Columns>
             <PagerStyle  HorizontalAlign="Center" />
            <SelectedRowStyle  Font-Bold="True" />
            <HeaderStyle  Font-Bold="True" Font-Names="Calibri" 
                               Font-Size="Small" />
                       <EditRowStyle />
            <AlternatingRowStyle />

            <EmptyDataTemplate>
            <asp:Label ID="lblempty" Text="No Records" runat="server">
            </asp:Label>             
            </EmptyDataTemplate>
        </asp:GridView>
    &nbsp;</td>
</tr>
</table>
</asp:Content>