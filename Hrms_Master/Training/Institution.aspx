<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Institution.aspx.cs" Inherits="Hrms_Training_Default" Title="Welcome to HRMS Training Module" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
function Validate_ddl(value, arg)
{
    arg.IsValid = (arg.Value != "Select");
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
        .style4
        {
            width: 193px;
        }
        .style10
        {
            width: 250px;
            height: 26px;
            color: #6A6A6A;
        }
        .style14
        {
            width: 93px;
            height: 36px;
        }
        .style15
        {
            width: 250px;
            height: 36px;
        }
        .style17
        {
            width: 134px;
            height: 17px;
        }
        .style18
        {
            height: 17px;
            width: 327px;
        }
        .style19
        {
            width: 93px;
            height: 17px;
        }
        .style20
        {
            width: 250px;
            height: 17px;
        }
        .style21
        {
            width: 134px;
            height: 37px;
        }
        .style22
        {
            width: 327px;
            height: 37px;
        }
        .style23
        {
            width: 93px;
            height: 37px;
        }
        .style24
        {
            width: 250px;
            height: 37px;
        }
        .m_aligning
        {
        	padding-left:10;
        }
        .m_aligning1
        {
        	padding-left:20;
        }
        .style29
        {
            color: #FF0000;
        }
                
        .style31
        {
            width: 327px;
        }
                
        .style34
        {
            width: 327px;
            height: 36px;
        }
        .style35
        {
            width: 134px;
        }
        .style36
        {
            width: 134px;
            height: 36px;
        }
                
        </style>



    <div><h3 class="page-header">Institution &amp; Trainer Profile
        <asp:DropDownList ID="DropDownList1" runat="server" 
                        onselectedindexchanged="DropDownList1_SelectedIndexChanged" 
                        AutoPostBack="True" CssClass="form-control">
                        <asp:ListItem>Select</asp:ListItem>
                    </asp:DropDownList>
                                                </h3></div>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">

    <ContentTemplate>--%>
        <div style="width: 70%">
                    <table cellpadding="1%" cellspacing="1%" width="100%" class="table table-striped table-bordered table-hover">
                        
                        <tr>
                            <td>
                                Institute Name<span style="color: #FF3300">*</span></td>
                            <td >
                                <asp:TextBox ID="txtname" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                            </td>
                            <td >
                                Type <span style="color: #FF3300">*</span></td>
                            <td >
                                <asp:DropDownList ID="ddltype" runat="server" CssClass="form-control"  MaxLength="20">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Institute" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Freelancer" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Address <span style="color: #FF3300">*</span></td>
                            <td>
                                <asp:TextBox ID="txtaddrs1" runat="server" CssClass="form-control" MaxLength="20" TextMode="MultiLine"></asp:TextBox>
                            </td>
                            <td>
                                Address 2 <span style="color: #FF3300">*</span></td>
                            <td>
                                &nbsp;<asp:TextBox ID="txtaddrs2" runat="server" CssClass="form-control" MaxLength="20" TextMode="MultiLine" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                City</td>
                            <td >
                                <asp:TextBox ID="txtcity" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                            </td>
                            <td >
                                State</td>
                            <td >
                                <asp:TextBox ID="txtstate" runat="server" CssClass="form-control" MaxLength="20" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Country</td>
                            <td>
                                <asp:TextBox ID="txtcountry" runat="server" CssClass="form-control" MaxLength="19" ></asp:TextBox>
                            </td>
                            <td>Phone</td>
                            <td>
                                <asp:TextBox ID="txtphone" runat="server" CssClass="form-control" MaxLength="15" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Email</td>
                            <td>
                                <asp:TextBox ID="txtemail" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                            </td>
                            <td>Website</td>
                            <td>
                                <asp:TextBox ID="txtweb" runat="server" CssClass="form-control" MaxLength="20" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Category</td>
                            <td>
                                <asp:TextBox ID="txtcategory" runat="server" CssClass="form-control" MaxLength="20" ></asp:TextBox>
                            </td>
                            <td>Grade</td>
                            <td>
                                <asp:TextBox ID="txtgrade" runat="server" CssClass="form-control" MaxLength="20" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Certifications</td>
                            <td>
                                <asp:TextBox ID="txtcertification" runat="server" CssClass="form-control" MaxLength="20" ></asp:TextBox>
                            </td>
                            <td>Awards</td>
                            <td>
                                <asp:TextBox ID="txtaward" runat="server" CssClass="form-control" MaxLength="20" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Branches</td>
                            <td>
                                <asp:TextBox ID="txtbranch" runat="server" CssClass="form-control" MaxLength="20" TextMode="MultiLine" ></asp:TextBox>
                            </td>
                            <td>Logo</td>
                            <td>
                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtname" ErrorMessage="Enter Institute Name" style="font-family: Calibri"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblerror" runat="server" Font-Names="Calibri"></asp:Label>
                            </td>
                            <td>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtemail" ErrorMessage="Invalid Email id" style="font-family: Calibri" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]
{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"></asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtweb" ErrorMessage="Website format: www.example.com" style="font-family: Calibri" ValidationExpression="([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                     </table>   
                     </div>
                     <div>  

                           <asp:GridView ID="gv_trainer" runat="server" AutoGenerateColumns="False" onrowcommand="gv_trainer_RowCommand" ShowFooter="True" CssClass="table table-striped table-bordered table-hover" >
                             <Columns>
                                 <asp:TemplateField HeaderText="Program Type">
                                     <ItemTemplate>
                                         <asp:Label ID="lbl_ptype" runat="server" CssClass="m_aligning" Text='<%#Eval("ptype")%>'></asp:Label>
                                     </ItemTemplate>
                                     <FooterTemplate>
                                         <asp:TextBox ID="txtptype" runat="server" CssClass="form-control" MaxLength="20" Width="100px"></asp:TextBox>
                                     </FooterTemplate>
                                 </asp:TemplateField>
                                <asp:TemplateField HeaderText="Trainer Name">

                                   <ItemTemplate>
                                         <asp:Label ID="lbl_fname" runat="server" CssClass="m_aligning" Text='<%#Eval("fname")%>'></asp:Label>
                                   </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtfname" runat="server" CssClass="form-control" MaxLength="20" Width="100px"></asp:TextBox>
                                    </FooterTemplate>
                               </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Experience in hrs">
                                     <ItemTemplate>
                                         <asp:Label ID="lbl_exp" runat="server" CssClass="m_aligning1" Text='<%#Eval("experience") %>'></asp:Label>
                                     </ItemTemplate>
                                     <FooterTemplate>
                                         <asp:TextBox ID="txtexp" runat="server" CssClass="form-control" MaxLength="4" Width="100px"></asp:TextBox>
                                     </FooterTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Specialization">
                                     <ItemTemplate>
                                         <asp:Label ID="lbl_spe" runat="server" CssClass="m_aligning" Text='<%#Eval("specification")%>'></asp:Label>
                                     </ItemTemplate>
                                     <FooterTemplate>
                                         <asp:TextBox ID="txtspe" runat="server" CssClass="form-control" MaxLength="20" Width="100px"></asp:TextBox>
                                     </FooterTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="WorkType">
                                     <ItemTemplate>
                                         <asp:Label ID="lbl_worktype" runat="server" CssClass="m_aligning" Text='<%#Eval("worktype") %>'></asp:Label>
                                     </ItemTemplate>
                                     <FooterTemplate>
                                         <asp:TextBox ID="txtworktype" runat="server" CssClass="form-control" MaxLength="20" Width="100px"></asp:TextBox>
                                     </FooterTemplate>
                                 </asp:TemplateField>
                             <asp:TemplateField HeaderText="Rating">
                                <ItemTemplate>
                                    <asp:Rating ID="Rating1" runat="server" CssClass="rateStar" CurrentRating='<%# String.IsNullOrEmpty(Eval("rating").ToString())?0:Eval("rating") %>' Direction="LeftToRight" EmptyStarCssClass="EmptyStar" FilledStarCssClass="FillStar" Height="10px" MaxRating="10" StarCssClass="rateItem" WaitingStarCssClass="SaveStar" Width="144px">
                                    </asp:Rating>
                                </ItemTemplate>
                                 <FooterTemplate>
                                     <asp:Rating ID="Rating2" runat="server" AutoPostBack="false" CssClass="rateStar" CurrentRating="0" Direction="LeftToRight" EmptyStarCssClass="EmptyStar" FilledStarCssClass="FillStar" Height="16px" MaxRating="10" StarCssClass="rateItem" WaitingStarCssClass="SaveStar" Width="142px">
                                     </asp:Rating>
                                 </FooterTemplate>
                            </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Rating">
                                     <ItemTemplate>
                                         <asp:Label ID="lbl_rate" runat="server" CssClass="m_aligning" Text='<%#Eval("rating") %>'></asp:Label>
                                     </ItemTemplate>
                                     <FooterTemplate>
                                         <asp:Button ID="butins" runat="server" CommandName="Insert" CssClass="btn btn-success" Font-Bold="True" Text="Add" />
                                     </FooterTemplate>
                                 </asp:TemplateField>
                            </Columns>                         
                               
                        </asp:GridView>
                      
                        <asp:GridView ID="grid_Branch" runat="server" AutoGenerateColumns="False" 
                               Width="100%" class="table table-striped table-bordered table-hover"
                                     DataKeyNames="CompanyId"  
                                     CellPadding="4" GridLines="None">
                              
                                <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <table cellspacing="0" cellpadding="0" width="100%">
                                            
                                            <thead>
                                                <tr>
                                                    <th  style="width:80%;">Branch List</th>
                                                </tr>
                                            </thead>
                                       </table>
                                   </HeaderTemplate>
                                   <ItemTemplate>
                                         <table  cellspacing="0" cellpadding="0" width="100%">
                                       
                                            <tbody>
                                                <tr>
                                                    <td style="width:10%;" align="left"><input type="checkbox" ID="Chk_Branch" runat="server" /></td>                                                    
                                                    <td style="width:80%;" nowrap="nowrap"><%#Eval("CompanyName")%></a></td>
                                                </tr>
                                            </tbody>                            
                                        </table> 
                                   </ItemTemplate>
                               </asp:TemplateField>
                            </Columns>                         
                        </asp:GridView>
                  <asp:ImageButton ImageUrl="~/Images/Assign.png" Visible="false" onmouseover="this.src='../../Images/Assignover.png';" onmouseout="this.src='../../Images/Assign.png';" ID="Button2" runat="server"  Text="Assign" /></td>                                    
           </div>
   <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>

</asp:Content>
