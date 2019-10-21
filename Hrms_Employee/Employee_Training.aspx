<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="Employee_Training.aspx.cs" Inherits="Hrms_Employee_Default" Title="Welcome to HRMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script language="javascript" type="text/javascript" src="../datecheck.js"></script>
    <script language="javascript" type="text/javascript">
    /*function fn_chkall(chkid,chklistid)
    { 
        var chkBoxList = document.getElementById(chklistid);
        var chkBoxCount= chkBoxList.getElementsByTagName("input");

       if(document.getElementById(chkid).checked==true)
       {       
            for(var i=0;i<chkBoxCount.length;i++) 
            {
                chkBoxCount[i].checked = true;
            }                
       }
       else
       {       
            for(var i=0;i<chkBoxCount.length;i++) 
            {
                chkBoxCount[i].checked = false;
            }               
       }             
    } */      
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


     <div><h3 class="page-header">Training Details</h3></div>
<div align="center" class="page-header">
    <asp:Label ID="lbl_empcodename" runat="server" Font-Bold="True" 
        Font-Size="Medium"></asp:Label>
    </div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div style="width: 70%">
                    <table cellpadding="1%" cellspacing="1%" width="100%" class="table table-striped table-bordered table-hover">
                        
                        <tr>
                            <td>
                                Institution Name</td>
                            <td >
                                &nbsp;<asp:DropDownList ID="ddl_InstName" runat="server" 
                                    CssClass="form-control">
                                </asp:DropDownList>
                            </td>
                            <td >
                                Program Type</td>
                            <td>
                                &nbsp;<asp:DropDownList ID="ddl_PrgmType" runat="server" 
                                    CssClass="form-control" >
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Program Name</td>
                            <td>
                                <asp:DropDownList ID="ddl_PrgmName" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </td>
                            <td>
                                Trainer Name</td>
                            <td>
                                <asp:DropDownList ID="ddl_TrainerName" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Duration From</td>
                            <td>
                            <div style="width:120px; float:left;">                               
                                <asp:TextBox ID="txtDurationFrom" runat="server" class="form-control" Width="120px"></asp:TextBox>

                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDurationFrom" Format="dd/MM/yyyy">
                                </asp:CalendarExtender>
                                    </div>
                                    <div style=" width:25px; float:left;  margin-left:10px; margin-top:3px;">                                                
             <asp:Image ID="Image3" runat="server"  
                                                        Text="" Width="25px" 
                                                        ImageUrl="~/Images/calendaricon.png" />                                       
                                 </div>
                            </td>
                            <td>
                                Duration To</td>
                            <td>
                            <div style="width:120px; float:left;">                               
                                <asp:TextBox ID="txtDurationTo" runat="server" class="form-control" Width="120px" ></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDurationTo" Format="dd/MM/yyyy">
                                </asp:CalendarExtender>
                            </div>
                            <div style=" width:25px; float:left;  margin-left:10px; margin-top:3px;">                                                
             <asp:Image ID="Image1" runat="server"  
                                                        Text="" Width="25px" 
                                                        ImageUrl="~/Images/calendaricon.png" />                                       
                                 </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Summary</td>
                            <td>
                                &nbsp;<asp:TextBox ID="txtsummary" CssClass="form-control" runat="server" TextMode="MultiLine" Width="75%"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
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
                         <asp:GridView ID="grid_Training" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                             DataKeyNames="TrainingID" ForeColor="#333333" GridLines="None" OnRowEditing="RowEditing" ShowFooter="True" Width="100%">
                             <Columns>
                                 <asp:TemplateField>
                                     <HeaderTemplate>
                                         <table cellpadding="0" cellspacing="0" class="dItemListContentTable" 
                                             width="100%">
                                             <colgroup>
                                                 <col class="dInboxContentTableCheckBoxCol"></col>
                                             </colgroup>
                                             <thead>
                                                 <tr class="grid" height="25px">
                                                     <th class="gridtext1" style="width: 120px;">
                                                         Institution Name</th>
                                                     <th class="gridtext1" style="width: 150px;">
                                                         Program Name</th>
                                                     <th class="gridtext1" style="width: 130px;">
                                                         Program type</th>
                                                     <th class="gridtext1" style="width: 130px;">
                                                         Trainer Name</th>
                                                     <th class="gridtext1" style="width: 30px;">
                                                         Edit</th>
                                                 </tr>
                                             </thead>
                                         </table>
                                     </HeaderTemplate>
                                     <ItemTemplate>
                                         <table cellpadding="0" cellspacing="0" class="dItemListContentTable" 
                                             width="100%">
                                             <colgroup>
                                                 <col class="dInboxContentTableCheckBoxCol"></col>
                                             </colgroup>
                                             <tbody>
                                                 <tr>
                                                     <td nowrap style="width: 120px;">
                                                         <%#Eval("InstitutionName")%>
                                                     </td>
                                                     <td nowrap style="width: 150px;">
                                                         <%#Eval("prgmname")%>
                                                     </td>
                                                     <td nowrap style="width: 130px;">
                                                         <%#Eval("prgmtypName")%>
                                                     </td>
                                                     <td nowrap style="width: 130px;">
                                                         <%#Eval("trnrName")%>
                                                     </td>
                                                     <td nowrap style="width: 30px;">
                                                         <asp:ImageButton ID="Edit" runat="server" AlternateText="" 
                                                             CommandArgument='<%#Eval("TrainingID") %>' CommandName="Edit" 
                                                             ImageUrl="~/Images/save_icon.jpg" Style="border: 0" ToolTip="Edit" />
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
  
</asp:Content>

