<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Training.aspx.cs" Inherits="Hrms_Additional_Default" Title="Welcome to HRMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../datecheck.js"></script>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
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
    <script language="javascript" type="text/javascript">
    
    
    function fnNew()
    {
        document.aspnetForm.ctl00$ContentPlaceHolder1$ddl_PeriodFromMonth.value="1"
        document.aspnetForm.ctl00$ContentPlaceHolder1$ddl_PeriodFromYear.value="1"
        document.aspnetForm.ctl00$ContentPlaceHolder1$ddl_PeriodToMonth.value="1"
        document.aspnetForm.ctl00$ContentPlaceHolder1$ddl_PeriodToYear.value="1"
        document.aspnetForm.ctl00$ContentPlaceHolder1$txtCompanyName.value="";
        document.aspnetForm.ctl00$ContentPlaceHolder1$txtCompanyLocation.value="";
        document.aspnetForm.ctl00$ContentPlaceHolder1$txtDesignationCode.value="";
        document.aspnetForm.ctl00$ContentPlaceHolder1$txtSalaryDrawn.value="";
        document.aspnetForm.ctl00$ContentPlaceHolder1$txtRole.value="";
        document.aspnetForm.ctl00$ContentPlaceHolder1$txtResponsibility.value="";
        document.aspnetForm.ctl00$ContentPlaceHolder1$txtCompanyName.focus();
    }
    
    function isBlank(s)
     { 
     var len = s.length
     var i
     for (i=0;i<len;i++)
      {
      if(s.charAt(i)!=" ") 
      return false
      }
     return true
    }

    function valid(str, type)
    {    
	    var RE;
	    switch (type)
	    {
		    case "Email" :
		    RE = /^[a-z][\._a-z0-9-]+@[\.a-z0-9-]+[\.]{1}[a-z]{2,4}$/;
		    if (RE.exec(str.value))
		    {
			    return true;	
		    }
		    else
		    {
			    return false;	
		    }

            case "CName" : 
		    RE = /^[a-zA-Z. ]{1,30}$/;
		    if (RE.exec(str.value))
		    {
			    return true;	
		    }
		    case "Name" : 
		    RE = /^[a-zA-Z. ]{4,30}$/;
		    if (RE.exec(str.value))
		    {
			    return true;	
		    }
		    else
		    {
			    return false;	
		    }
		    case "Username" : 
		    RE = /^[a-zA-Z0-9._]{5,15}$/;
		    if (RE.exec(str.value))
		    {
			    return true;	
		    }
		    else
		    {
			    return false;	
		    }	
		    case "ctc" :
		    RE = /^[0-9]{4,6}$/;
		    if (RE.exec(str.value))
		    {
			    return true;	
		    }
		    else
		    {
			    return false;	
		    }
		    case "year" :
		    RE = /^[0-9]{1,4}$/;
		    if (RE.exec(str.value))
		    {
			    return true;	
		    }
		    else
		    {
			    return false;	
		    }
		    default :
			    return false;
	    }
    }

    function sav(chk)
    {	
	    var msg="Please make sure the following fields are valid \n\n";
	    var key="";
	    if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtCompanyName.value) || isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtSalaryDrawn.value) || isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtDesignationCode.value))
		    key+="All fields need to be filled up \n\n";
	    if(!valid(document.aspnetForm.ctl00$ContentPlaceHolder1$txtCompanyName,"CName"))
	    {
		    key+="Invalid CompanyName \n";
	    }
	    if(!valid(document.aspnetForm.ctl00$ContentPlaceHolder1$txtCompanyLocation,"Name"))
	    {
		    key+="Invalid Company Location \n";
	    }
        if(!valid(document.aspnetForm.ctl00$ContentPlaceHolder1$txtDesignationCode,"CName"))
	    {
		    key+="Invalid Designation \n";
        }
        if(!valid(document.aspnetForm.ctl00$ContentPlaceHolder1$txtRole,"Name"))
	    {
		    key+="Invalid Role \n";
        }
        if(!valid(document.aspnetForm.ctl00$ContentPlaceHolder1$txtResponsibility,"Username"))
	    {
		    key+="Invalid Responsibility \n";
        }
	    if(!valid(document.aspnetForm.ctl00$ContentPlaceHolder1$txtSalaryDrawn,"ctc"))
	    {
		    key+="Invalid CTC \n";
	    }
        
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$ddl_PeriodFromMonth.value=="1")
        {
                key+="Select From Month \n";
        }        

        if(document.aspnetForm.ctl00$ContentPlaceHolder1$ddl_PeriodFromYear.value=="1")
        {
                key+="Select From Year \n";
        }
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$ddl_PeriodToMonth.value=="1")
        {
                key+="Select To Month \n";
        }

        if(document.aspnetForm.ctl00$ContentPlaceHolder1$ddl_PeriodToYear.value=="1")
        {
                key+="Select To Year \n";
        }

        if(key!="")
	    {
		 	    alert(msg+key+"\n ******** Unable to submit!! ******** \n");   
	    }
	    else 
	    {
	        if(chk=="Img1")
	        {
                    document.aspnetForm.ctl00_ContentPlaceHolder1_btn_chuser.value="S";
	       	        document.aspnetForm.ctl00_ContentPlaceHolder1_ToolBarCode.value=1;
        		    document.aspnetForm.submit();
		    }
		    if(chk=="Img2")
	        {
                    document.aspnetForm.ctl00_ContentPlaceHolder1_btn_chuser.value="SC";
	                document.aspnetForm.ctl00_ContentPlaceHolder1_ToolBarCode.value=1;
		            document.aspnetForm.submit();
		    }
	    }
    }
         
         
    </script>

    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td class="tdComposeHeader" valign="top" align="right">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="border" style="height: 35px;">
                            <span class="Title" 
                                style="font-family: Calibri; font-size: medium; font-weight: bold;">&nbsp;&nbsp;&nbsp;Training</span></td>
                        <td height="30px" class="border">
                            &nbsp;<asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True"  CssClass="form-control"
                                OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged">
                            </asp:DropDownList></td>
                    </tr>
                </table>
                <table cellpadding="5" cellspacing="1" width="100%" id="tbl_selection" runat="server">
                    <tr>
                        <td colspan="3" align="center">
                            <asp:Label ID="lbl_Error" runat="server" CssClass="Error" ForeColor="Red" Font-Bold="True"></asp:Label>&nbsp;
                        </td>
                    </tr>
                    <tr id="row_type" runat="server">
                        <td align="right" style="width: 25%">
                            <span class="dComposeItemLabel">Select Type&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></td>
                        <td align="right" style="width: 1%">
                            &nbsp;</td>
                        <td>
                            <asp:DropDownList ID="ddl_typelist" runat="server"  CssClass="form-control" OnSelectedIndexChanged="ddl_typelist_SelectedIndexChanged"
                                AutoPostBack="True" Height="17px" Width="225px">
                                <asp:ListItem Value="st">Select</asp:ListItem>
                                <asp:ListItem Value="add">Add Employee Details</asp:ListItem>
                                <asp:ListItem Value="view">View Employee Details</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="row_ddlemp" runat="server">
                        <td align="right" style="width: 25%">
                            <span class="dComposeItemLabel">Select Employee</span></td>
                        <td align="right" style="width: 1%">
                            &nbsp;</td>
                        <td>
                            <asp:DropDownList ID="ddl_employeelist" runat="server"  CssClass="form-control"
                                AutoPostBack="True" 
                                OnSelectedIndexChanged="ddl_employeelist_SelectedIndexChanged" Height="17px" 
                                Width="225px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <%--bysan--%>
                    <%--<tr>
            <td id="Td1" runat="server" align="center" height="30" style="width: 44%" valign="top">
                Training Code</td>
            <td id="Td2" runat="server" align="center" valign="top" style="width: 40%">
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="InputDefaultStyle">
                </asp:DropDownList></td>
        </tr>--%>
                    <tr id="row_emp" runat="server">
                        <td runat="server" id="div_chk_Empcode" valign="middle" style="height: 260px;"
                            align="center" class="dComposeItemLabel" colspan="2">
                            Select Employees&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td runat="server" id="div_chk_Master" valign="top" align="left" style="width: 40%;
                            height: 260px;">
                            <div class="qrychkbox_big" style="height: 230px; left: 0px; top: 0px;" align="center">
                                <asp:CheckBoxList ID="chk_Master" runat="server" CssClass="InputDefaultStyle1" Height="180px"
                                    Width="90%">
                                </asp:CheckBoxList></div>
                            <input type="checkbox" id="chk_all_master" runat="server" onclick="javascript: fn_chkall(this.id,'ctl00_ContentPlaceHolder1_chk_Master')" />
                            <span class="dComposeItemLabel" style="text-align: left">Select All</span>
                        </td>
                    </tr>
                </table>
                <table width="100%" cellpadding="0" cellspacing="0" id="tbl_training" runat="server">
                    <tr valign="top">
                        <td id="tdComposeHeader" valign="top" style="height: 180px">
                            <table cellpadding="10" cellspacing="1" id="tbl_details" 
                                runat="server" style=" width: 59%;" 
                                align="center">
                                <tr>
                                    <td class="dComposeItemLabel" nowrap>
                                        Institution Name</td>
                                    <td>
                                        <asp:DropDownList ID="ddl_InstName" runat="server"  CssClass="form-control"
                                            onselectedindexchanged="ddl_InstName_SelectedIndexChanged">
                                        </asp:DropDownList></td>
                                    <td class="dComposeItemLabel" nowrap>
                                        Program Type</td>
                                    <td style="width: 145px">
                                        <asp:DropDownList ID="ddl_PrgmType" runat="server"  CssClass="form-control">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td class="dComposeItemLabel" nowrap>
                                        Program Name&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_PrgmName" runat="server"  CssClass="form-control">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="dComposeItemLabel" nowrap>
                                        Trainer Name&nbsp;&nbsp; </td>
                                    <td style="width: 145px">
                                        <asp:DropDownList ID="ddl_TrainerName" runat="server"  CssClass="form-control">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td class="dComposeItemLabel" nowrap>
                                        Duration From&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                    <td>
                                        <asp:TextBox ID="txtDurationFrom" runat="server"  CssClass="form-control"></asp:TextBox>
                                                                    </td>
                                    <td class="dComposeItemLabel" nowrap>
                                        Duration To&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                    <td style="width: 145px">
                                        <asp:TextBox ID="txtDurationTo" runat="server"  CssClass="form-control"></asp:TextBox>
                                                                    </td>
                                </tr>
                                <tr>
                                    <td class="dComposeItemLabel" nowrap="nowrap" style="height: 48px">
                                        Summary&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                    <td align="center" colspan="2" nowrap="nowrap" style="height: 48px">
                                        <asp:TextBox ID="txtsummary" runat="server" TextMode="MultiLine" Width="100%"  CssClass="form-control"></asp:TextBox></td>
                                    <td style="height: 48px; width: 145px;">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="row_grid" runat="server" valign="top">
                        <td style="height: 183px">
                            <asp:GridView ID="grid_Training" runat="server" Width="100%" AutoGenerateColumns="False"
                                DataKeyNames="TrainingID" OnRowEditing="RowEditing"  class="table table-striped table-bordered table-hover" 
                                onrowcommand="grid_Training_RowCommand" CellPadding="4" 
                                ForeColor="#333333" GridLines="None">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField>
                                     <HeaderTemplate>
                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                <colgroup>
                                                    <col class="dInboxContentTableCheckBoxCol">
                                                </colgroup>
                                                <thead>
                                                    <tr class="grid" height="25px">
                                                    <th class="gridtext1" style="width: 130px;">
                                                            Training ID</th>
                                                        <th class="gridtext1" style="width: 130px;">
                                                            Institution Name</th>
                                                        <th class="gridtext1" style="width: 130px;">
                                                            Program Name</th>
                                                        <th class="gridtext1" style="width: 130px;">
                                                            Program type</th>
                                                        <th class="gridtext1" style="width: 130px;">
                                                            Trainer Name</th>
                                                        <th class="gridtext1" style="width: 130px;">
                                                            Rating</th>
                                                        <th class="gridtext1" style="width: 130px;">
                                                            Edit</th>
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
                                                    <td style="width: 130px;" nowrap>
                                                        <asp:Label ID="lbltraing_id" runat="server"  Text='<%#Eval("TrainingID")%>' ></asp:Label>
                                                        </td>
                                                        <td style="width: 130px;" nowrap>
                                                            <%#Eval("InstitutionName")%>
                                                        </td>
                                                        <td style="width: 130px;" nowrap>
                                                            <%#Eval("prgmname")%>
                                                        </td>
                                                        <td style="width: 130px;" nowrap>
                                                            <%#Eval("prgmtypName")%>
                                                        </td>
                                                        <td style="width: 130px;" nowrap>
                                                            <%#Eval("trnrName")%>
                                                        </td>
                                                        <td style="width: 130px;" nowrap>
                                                            <%#Eval("Rating")%>
                                                        </td>
                                                        <td style="width: 130px;" nowrap>
                                                            <asp:Rating ID="Rating1"  runat="server" CurrentRating='<%# String.IsNullOrEmpty(Eval("rating").ToString())?0:Eval("rating") %>' 
                                MaxRating="10" CssClass="rateStar" StarCssClass="rateItem" 
                                WaitingStarCssClass="SaveStar" FilledStarCssClass="FillStar" 
                                EmptyStarCssClass="EmptyStar"  AutoPostBack="false" 
                                Direction="LeftToRight" Height="16px" Width="130px">
                                                            </asp:Rating>                            </td>
                                                                <td style="width: 130px;" nowrap>
                                                            <asp:ImageButton ID="ImageButton1" ToolTip="Edit" ImageUrl="../Images/i_Edit.gif" runat="server"
                                                                Style="border: 0" AlternateText="" CommandName="Edit" CommandArgument='<%#Eval("TrainingID") %>' /></td>

                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle  Font-Bold="True"  />
                                <PagerStyle  HorizontalAlign="Center" />
                                <SelectedRowStyle  Font-Bold="True"  />
                                <HeaderStyle  Font-Bold="True"/>
                                <EditRowStyle />
                                <AlternatingRowStyle />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr id="row_button" runat="server">
                        <td align="right" colspan="4" style="height: 24px">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                             <asp:Button ID="btn_save_con" runat="server" Text="Save" class="btn btn-success"  OnClick="btn_save_con_Click"/>
                            <%--<asp:ImageButton ID="btn_save_con" runat="server" ImageUrl="../Images/Save&Cont_arrow.jpg"
                                OnClick="btn_save_con_Click" />--%>
                            &nbsp;<asp:Button ID="btn_update" runat="server" Text="Modify" OnClick="btn_update_Click" class="btn btn-info"/>
                           <%-- <asp:ImageButton ID="btn_update" runat="server" 
                                ImageUrl="~/Images/Modify.png" onmouseover="this.src='../Images/Modifyover.png';" onmouseout="this.src='../Images/Modify.png';" OnClick="btn_update_Click" />--%>
                            <asp:Button ID="btn_skip"  ToolTip="Skip" runat="server" Text="Skip"  OnClick="btn_skip_Click" CausesValidation="False" class="btn btn-warning"/>
                           <%-- <asp:ImageButton ToolTip="Skip" ImageUrl="~/Images/Skip.png" onmouseover="this.src='../Images/Skipover.png';" onmouseout="this.src='../Images/Skip.png';" ID="btn_skip" runat="server"
                                OnClick="btn_skip_Click" CausesValidation="False" />--%>
                            <asp:Button ID="btn_save" runat="server" Text="Save" OnClick="btn_save_Click" class="btn btn-success"/>
                            <%--<asp:ImageButton ID="btn_save" runat="server" ImageUrl="~/Images/Save.png" onmouseover="this.src='../Images/Saveover.png';" onmouseout="this.src='../Images/Save.png';" OnClick="btn_save_Click" />--%>
                            <asp:Button ID="btn_Back" runat="server" Text="Back" class="btn btn-info" OnClick="btn_Back_Click"/>
                            <%--<asp:ImageButton ID="btn_Back" runat="server" ImageUrl="~/Images/Back.png" onmouseover="this.src='../Images/Backover.png';" onmouseout="this.src='../Images/Back.png';" OnClick="btn_Back_Click" />--%>
                        </td>
                    </tr>
                </table>
                <input type="hidden" id="hSeqID" runat="server" style="width: 25px" value="0" />
            </td>
        </tr>
    </table>
</asp:Content>
