<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="WorkExperience.aspx.cs" Inherits="Hrms_Employee_Default" Title="Welcome to HRMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>
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


    <div><h3 class="page-header">Work History&nbsp;</h3></div>
<div align="center" class="page-header">
    <asp:Label ID="lbl_empcodename" runat="server" Font-Bold="True" 
        Font-Size="Medium"></asp:Label>
    </div>
    <div align="right">
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div style="width: 70%">
                    <table cellpadding="1%" cellspacing="1%" width="100%" class="table table-striped table-bordered table-hover">
                        
                        <tr>
                            <td>
                                Company Name</td>
                            <td >
                                <input class="form-control" runat="server" id="txtCompanyName" 
                                    onkeydown="AllowOnlyText1(event);" />
                            </td>
                            <td >
                                Location</td>
                            <td>
                                <input class="form-control" runat="server" id="txtCompanyLocation" 
                                    onkeydown="AllowOnlyText1(event);" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Period From Month</td>
                            <td>
                                <asp:DropDownList ID="ddl_PeriodFromMonth" runat="server" 
                                    class="form-control" Font-Names="Calibri" Width="120px">
                                    <asp:ListItem Value="1">Select</asp:ListItem>
                                    <asp:ListItem Value="2">January</asp:ListItem>
                                    <asp:ListItem Value="3">Febraury</asp:ListItem>
                                    <asp:ListItem Value="4">March</asp:ListItem>
                                    <asp:ListItem Value="5">April</asp:ListItem>
                                    <asp:ListItem Value="6">May</asp:ListItem>
                                    <asp:ListItem Value="7">June</asp:ListItem>
                                    <asp:ListItem Value="8">July</asp:ListItem>
                                    <asp:ListItem Value="9">August</asp:ListItem>
                                    <asp:ListItem Value="10">September</asp:ListItem>
                                    <asp:ListItem Value="11">October</asp:ListItem>
                                    <asp:ListItem Value="12">November</asp:ListItem>
                                    <asp:ListItem Value="13">December</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                Period From Year</td>
                            <td>
                                <asp:DropDownList ID="ddl_PeriodFromYear" runat="server" 
                                    class="form-control" Font-Names="Calibri">
                                    <asp:ListItem Value="1">Select</asp:ListItem>
                                    <asp:ListItem Value="1991">1991</asp:ListItem>
                                    <asp:ListItem Value="1992">1992</asp:ListItem>
                                    <asp:ListItem Value="1993">1993</asp:ListItem>
                                    <asp:ListItem Value="1994">1994</asp:ListItem>
                                    <asp:ListItem Value="1995">1995</asp:ListItem>
                                    <asp:ListItem Value="1996">1996</asp:ListItem>
                                    <asp:ListItem Value="1997">1997</asp:ListItem>
                                    <asp:ListItem Value="1998">1998</asp:ListItem>
                                    <asp:ListItem Value="1999">1999</asp:ListItem>
                                    <asp:ListItem Value="2000">2000</asp:ListItem>
                                    <asp:ListItem Value="2001">2001</asp:ListItem>
                                    <asp:ListItem Value="2002">2002</asp:ListItem>
                                    <asp:ListItem Value="2003">2003</asp:ListItem>
                                    <asp:ListItem Value="2004">2004</asp:ListItem>
                                    <asp:ListItem Value="2005">2005</asp:ListItem>
                                    <asp:ListItem Value="2006">2006</asp:ListItem>
                                    <asp:ListItem Value="2007">2007</asp:ListItem>
                                    <asp:ListItem Value="2008">2008</asp:ListItem>
                                    <asp:ListItem Value="2009">2009</asp:ListItem>
                                    <asp:ListItem Value="2010">2010</asp:ListItem>
                                    <asp:ListItem Value="2011">2011</asp:ListItem>
                                    <asp:ListItem Value="2012">2012</asp:ListItem>
                                    <asp:ListItem Value="2013">2013</asp:ListItem>
                                    <asp:ListItem Value="2014">2014</asp:ListItem>
                                    <asp:ListItem Value="2015">2015</asp:ListItem>
                                    <asp:ListItem Value="2016">2016</asp:ListItem>
                                    <asp:ListItem Value="2017">2017</asp:ListItem>
                                    <asp:ListItem Value="2018">2018</asp:ListItem>
                                    <asp:ListItem Value="2019">2019</asp:ListItem>
                                    <asp:ListItem Value="2020">2020</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Period To Month</td>
                            <td>
                                <asp:DropDownList ID="ddl_PeriodToMonth" runat="server" 
                                    class="form-control" Font-Names="Calibri"  Width="120px">
                                    <asp:ListItem Value="1">Select</asp:ListItem>
                                    <asp:ListItem Value="2">January</asp:ListItem>
                                    <asp:ListItem Value="3">Febraury</asp:ListItem>
                                    <asp:ListItem Value="4">March</asp:ListItem>
                                    <asp:ListItem Value="5">April</asp:ListItem>
                                    <asp:ListItem Value="6">May</asp:ListItem>
                                    <asp:ListItem Value="7">June</asp:ListItem>
                                    <asp:ListItem Value="8">July</asp:ListItem>
                                    <asp:ListItem Value="9">August</asp:ListItem>
                                    <asp:ListItem Value="10">September</asp:ListItem>
                                    <asp:ListItem Value="11">October</asp:ListItem>
                                    <asp:ListItem Value="12">November</asp:ListItem>
                                    <asp:ListItem Value="13">December</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                Period To Year</td>
                            <td>
                                <asp:DropDownList ID="ddl_PeriodToYear" runat="server" 
                                   class="form-control" Font-Names="Calibri">
                                    <asp:ListItem Value="1">Select</asp:ListItem>
                                    <asp:ListItem Value="1991">1991</asp:ListItem>
                                    <asp:ListItem Value="1992">1992</asp:ListItem>
                                    <asp:ListItem Value="1993">1993</asp:ListItem>
                                    <asp:ListItem Value="1994">1994</asp:ListItem>
                                    <asp:ListItem Value="1995">1995</asp:ListItem>
                                    <asp:ListItem Value="1996">1996</asp:ListItem>
                                    <asp:ListItem Value="1997">1997</asp:ListItem>
                                    <asp:ListItem Value="1998">1998</asp:ListItem>
                                    <asp:ListItem Value="1999">1999</asp:ListItem>
                                    <asp:ListItem Value="2000">2000</asp:ListItem>
                                    <asp:ListItem Value="2001">2001</asp:ListItem>
                                    <asp:ListItem Value="2002">2002</asp:ListItem>
                                    <asp:ListItem Value="2003">2003</asp:ListItem>
                                    <asp:ListItem Value="2004">2004</asp:ListItem>
                                    <asp:ListItem Value="2005">2005</asp:ListItem>
                                    <asp:ListItem Value="2006">2006</asp:ListItem>
                                    <asp:ListItem Value="2007">2007</asp:ListItem>
                                    <asp:ListItem Value="2008">2008</asp:ListItem>
                                    <asp:ListItem Value="2009">2009</asp:ListItem>
                                    <asp:ListItem Value="2010">2010</asp:ListItem>
                                    <asp:ListItem Value="2011">2011</asp:ListItem>
                                    <asp:ListItem Value="2012">2012</asp:ListItem>
                                    <asp:ListItem Value="2013">2013</asp:ListItem>
                                    <asp:ListItem Value="2014">2014</asp:ListItem>
                                    <asp:ListItem Value="2015">2015</asp:ListItem>
                                    <asp:ListItem Value="2016">2016</asp:ListItem>
                                    <asp:ListItem Value="2017">2017</asp:ListItem>
                                    <asp:ListItem Value="2018">2018</asp:ListItem>
                                    <asp:ListItem Value="2019">2019</asp:ListItem>
                                    <asp:ListItem Value="2020">2020</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Designation</td>
                            <td>
                                <input class="form-control" runat="server" id="txtDesignationCode" 
                                    onkeydown="AllowOnlyText1(event);" />
                            </td>
                            <td>
                                Cost To Company</td>
                            <td>
                                <input class="form-control" runat="server" id="txtSalaryDrawn" 
                                    onkeydown="AllowOnlyNumeric1(event);" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Role</td>
                            <td>
                                <input class="form-control" runat="server" id="txtRole" 
                                    onkeydown="AllowOnlyText1(event);" />
                            </td>
                            <td>
                                Responsibility</td>
                            <td>
                                <input class="form-control" runat="server" id="txtResponsibility" 
                                    onkeydown="AllowOnlyText1(event);" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="right">
                                <asp:Button ID="btn_Back" runat="server" class="btn btn-info" 
                                    OnClick="btn_Back_Click" Text="Back" />
                                <asp:Button ID="btn_skip" runat="server" CausesValidation="False" 
                                    class="btn btn-warning" OnClick="btn_skip_Click" Text="Skip" ToolTip="Skip" />
                                <asp:Button ID="btn_save" runat="server" class="btn btn-success" 
                                    OnClick="btn_save_Click" Text="Save" />
                                <asp:Button ID="btn_update" runat="server" class="btn btn-success" 
                                    OnClick="btn_update_Click" Text="Update" />
                            </td>
                        </tr>
                     </table>   
                     </div>
                     <div>  

                         </td>                                    
                         <asp:GridView ID="grid_WorkHistory" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                             DataKeyNames="WorkHistorySeqID" GridLines="None" OnRowEditing="RowEditing" 
                             ShowFooter="True" Width="70%" onrowdeleting="grid_WorkHistory_RowDeleting">

                             <Columns>
                                 <asp:TemplateField>
                                     <HeaderTemplate>
                                         <table cellpadding="0" cellspacing="0" width="100%">
                                         
                                             <thead>
                                                 <tr height="25px">
                                                     <th align="left" style="width: 130px;">
                                                         Company Name</th>
                                                     <th align="left" style="width: 150px;">
                                                         Designation</th>
                                                     <th align="left" style="width: 100px;">
                                                         Current CTC</th>
                                                     <th style="width: 20px;">
                                                         Edite</th>
                                                      <th style="width: 20px;">
                                                         Delete</th>
                                                 </tr>
                                             </thead>
                                         </table>
                                     </HeaderTemplate>
                                     <ItemTemplate>
                                         <table cellpadding="0" cellspacing="0" width="100%">
                                          <%--   <colgroup>
                                                 <col class="dInboxContentTableCheckBoxCol"></col>
                                             </colgroup>--%>
                                             <tbody>
                                                 <tr>
                                                     <td  style="width: 80px;">
                                                         <%#Eval("CompanyName")%>
                                                     </td>
                                                     <td  style="width: 100px;">
                                                         <%#Eval("DesignationCode")%>
                                                     </td>
                                                     <td  style="width: 70px;">
                                                         <%#Eval("Salary")%>
                                                     </td>
                                                      <td  style="width: 10px;">
                                                           <asp:ImageButton ID="Edit" runat="server" AlternateText="" CommandName="Edit" 
                                                             ImageUrl="~/Images/edit_icon.png"  ToolTip="Edit" />
                                                        </td>   
                                                     <td style="width: 10px;" >
                                                           <asp:ImageButton ID="imgdel" runat="server" AlternateText="" CommandName="Delete" 
                                                             ImageUrl="~/Images/delete_icon.jpg"  ToolTip="Edit" />
                                                            
                                                             </td>
                                                 </tr>
                                             </tbody>
                                         </table>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                             </Columns>
                             
                         </asp:GridView>
           </div>
    </ContentTemplate>
    </asp:UpdatePanel>


    <input type="hidden" id="hSeqID" runat="server" style="width: 25px" value="0" />
    <input type="hidden" id="btn_chuser" runat="server" style="width: 25px" value="" />
    <input type="hidden" id="hCompanyID" runat="server" style="width: 25px" value="1" />
    <input type="hidden" id="ToolBarCode" runat="server" style="width: 25px" value="0" />
    <input type="hidden" id="hCandidateID" runat="server" style="width: 25px" value="1" />
</asp:Content>
