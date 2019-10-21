<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="NewCompany.aspx.cs" Inherits="Hrms_Company_Default" Title="Welcome to HRMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>
  <script language="javascript" type="text/javascript">
    
 function Number_txtbox(event,txtid)
  {  
       var txtlen;
       var txtvalue; 
       var bool_obj; 
       var count;  
       var str;           
          
       txtvalue= document.getElementById(txtid).value;
       txtlen=txtvalue.length; 
       count=txtlen-1;
       
  if(event.keyCode!=8 && event.keyCode!=46 && event.keyCode!=35 && event.keyCode!=36 && event.keyCode!=37 && event.keyCode!=38 && event.keyCode!=39 && event.keyCode!=40)     
   {    
  
        str=txtvalue.charAt(count);
        
        if(str<=9 && str>=0)
                {
                document.getElementById(txtid).value=txtvalue;
                }
                else
                {
               document.getElementById(txtid).value= txtvalue.substring(0,count);   
                }         
    }
  }
    
    function show_message(msg)
    {
        alert(msg);
        
    }
       
    function clearAll()
    {
    
    //document.aspnetForm.ctl00$ContentPlaceHolder1$CompanyCode.value="";           
    
  //document.aspnetForm.ctl00$ContentPlaceHolder1$txtCompanyCode.value="";  
  document.aspnetForm.ctl00$ContentPlaceHolder1$txtCompanyName.value="";  
  document.aspnetForm.ctl00$ContentPlaceHolder1$txtAddressLine1.value="";  
  document.aspnetForm.ctl00$ContentPlaceHolder1$txtAddressLine2.value="";  
  document.aspnetForm.ctl00$ContentPlaceHolder1$txtCity.value="";  
  document.aspnetForm.ctl00$ContentPlaceHolder1$txtZipCode.value="";  
  document.aspnetForm.ctl00$ContentPlaceHolder1$txtCountry.value="";  
  document.aspnetForm.ctl00$ContentPlaceHolder1$txtState.value="";  
  document.aspnetForm.ctl00$ContentPlaceHolder1$txtPhoneNo.value="";  
  document.aspnetForm.ctl00$ContentPlaceHolder1$txtFaxNo.value="";  
  document.aspnetForm.ctl00$ContentPlaceHolder1$txtEmailId.value="";  
  document.aspnetForm.ctl00$ContentPlaceHolder1$txtAlternateEmailId.value="";  
  //document.aspnetForm.ctl00$ContentPlaceHolder1$txtHeadCode.value="";  
  document.aspnetForm.ctl00$ContentPlaceHolder1$txtHeadName.value="";  
  
    
    }
    
 </script>
 
 
<script language="javascript">

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
		if (RE.exec(str.value)){
			return true;	
		}else{
			return false;	
		}
		case "Name" : 
		RE = /^[a-zA-Z. ]{4,20}$/;
		if (RE.exec(str.value)){
			return true;	
		}else{
			return false;	
		}
		case "Phone" :
		RE = /^[0-9]{8,15}$/;
		if (RE.exec(str.value)){
			return true;	
		}else{
			return false;	
		}
		
		
		
		default :
			return false;
	}
}

function create()
{	
	
	var msg="Please make sure the following fields are valid \n\n";
	var key="";
	
	if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtCompanyCode.value)) 
		{
		key+=" Enter Company Code \n";
		}
		
		
	if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtCompanyName.value))
	   {	
	   key+=" Enter Company Name  \n";
	   }
		
		
		if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtHeadCode.value)) 
		{
		key+=" Enter HeadOffice Code \n";
		}
		
		
	if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtHeadName.value))
	   {	
	   key+=" Enter HeadOffice Location  \n";
	   }
		
		
if(!isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtPhoneNo.value))
 {
    if(!valid(document.aspnetForm.ctl00$ContentPlaceHolder1$txtPhoneNo,"Phone"))
	{
		key+="Invalid PhoneNo \n";
   	}
   	
 }
   	
   	
if(!isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtEmailId.value))
 {
  	
	if(!valid(document.aspnetForm.ctl00$ContentPlaceHolder1$txtEmailId,"Email"))
	{
		key+="Invalid Email ID \n";
	}
	
 }
 
 if(!isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtAlternateEmailId.value))
 {
  
	if(!valid(document.aspnetForm.ctl00$ContentPlaceHolder1$txtAlternateEmailId,"Email"))
	{
		key+="Invalid Alternate Email ID \n";
	}
 }
 
 
	
	if(key!="")
	{
		 	alert(msg+key+"\n ******** Unable to Create!! ******** \n");   
	}
	else
	{	
		//alert("Successful");		
    document.aspnetForm.ctl00$ContentPlaceHolder1$ToolBarCode.value=1		
	document.aspnetForm.submit();
		
	}
	
}

function Update()
{	
	
	var msg="Please make sure the following fields are valid \n\n";
	var key="";
	
	if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtCompanyCode.value)) 
		{
		key+=" Enter Company Code \n";
		}
		
		
	if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtCompanyName.value))
	   {	
	   key+=" Enter Company Name  \n";
	   }
		
		
		
		if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtHeadCode.value)) 
		{
		key+=" Enter HeadOffice Code \n";
		}
		
		
	if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtHeadName.value))
	   {	
	   key+=" Enter HeadOffice Location  \n";
	   }
		
		
if(!isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtPhoneNo.value))
 {
    if(!valid(document.aspnetForm.ctl00$ContentPlaceHolder1$txtPhoneNo,"Phone"))
	{
		key+="Invalid PhoneNo \n";
   	}
   	
 }
   	
   	
if(!isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtEmailId.value))
 {
  	
	if(!valid(document.aspnetForm.ctl00$ContentPlaceHolder1$txtEmailId,"Email"))
	{
		key+="Invalid Email ID \n";
	}
	
 }
 
 if(!isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtAlternateEmailId.value))
 {
  
	if(!valid(document.aspnetForm.ctl00$ContentPlaceHolder1$txtAlternateEmailId,"Email"))
	{
		key+="Invalid Alternate Email ID \n";
	}
 }
 
 
	
	if(key!="")
	{
		 	alert(msg+key+"\n ******** Unable to Create!! ******** \n");   
	}
	else
	{	
		//alert("Successful");		
    document.aspnetForm.ctl00$ContentPlaceHolder1$ToolBarCode.value=2		
	document.aspnetForm.submit();
		
	}
	
}


</script>

<table width="100%" height="100%" cellpadding="0" cellspacing="0">
  <tr valign="top">
    <td id="tdComposeHeader" valign="top">
        <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td height="35px" class="border"><SPAN class="Title">&nbsp;&nbsp;Head Office</SPAN></td>
                </tr>
        </table>        
        
       <table class="Content" cellpadding="5px" cellspacing="1px" width="100%">
             <tr>
                <td colspan="6" align="center">&nbsp;<asp:Label ID="lbl_Error" runat="server" ForeColor="Red" CssClass="Error"></asp:Label></td>
             </tr>
             <tr>
                 <td class="dComposeItemLabel" nowrap style="width: 56px; height: 30px"></td>
                 <td class="dComposeItemLabel1" nowrap style="height: 30px">Company Code <font color="red" size="3">*</font></td>
                 <td style="height: 30px"><input class="InputDefaultStyle" runat="server" id="txtCompanyCode" /></td>
                 <td class="dComposeItemLabel" nowrap style="height: 30px"></td>
                 <td class="dComposeItemLabel1" nowrap style="height: 30px">Company Name <span style="color: #ff0000; font-size: 12pt;">*</span></td>
                 <td style="font-size: 12pt; height: 30px;"><input class="InputDefaultStyle" runat="server" id="txtCompanyName" /></td>
             </tr>                        
             <tr style="font-size: 12pt">
                 <td class="dComposeItemLabel" nowrap style="width: 56px">&nbsp;</td>
                 <td class="dComposeItemLabel1" nowrap>Head Office Code <font color="red" size="3">*</font></td>
                 <td><input class="InputDefaultStyle" runat="server" id="txtHeadCode" /></td>
                 <td class="dComposeItemLabel" nowrap>&nbsp;</td>
                 <td class="dComposeItemLabel1" nowrap>Head Office Location<font color="red" size="3">*</font></td>
                 <td><input class="InputDefaultStyle" runat="server" id="txtHeadName" /></td>
             </tr>
             <tr>
                 <td class="dComposeItemLabel" nowrap style="width: 56px">&nbsp;</td>
                 <td class="dComposeItemLabel1" nowrap>Address Line 1</td>
                 <td><input class="InputDefaultStyle" runat="server" id="txtAddressLine1" /></td>
                 <td class="dComposeItemLabel" nowrap>&nbsp;</td>
                 <td class="dComposeItemLabel1" nowrap>Address Line2</td>
                 <td><input class="InputDefaultStyle" runat="server" id="txtAddressLine2" /></td>
             </tr>                        
             <tr>
                 <td class="dComposeItemLabel" nowrap style="width: 56px">&nbsp;</td>
                 <td class="dComposeItemLabel1" nowrap>City</td>
                 <td><input class="InputDefaultStyle" runat="server" id="txtCity" onkeypress="AllowOnlyText();" onfocusout="strvalid(this.id, 'String');" /></td> <%--onkeyup="keyup(event,this.id);" --%>
                 <td class="dComposeItemLabel" nowrap>&nbsp;</td>
                 <td class="dComposeItemLabel1" nowrap>Zip Code</td>
                 <td><input class="InputDefaultStyle" runat="server" id="txtZipCode" onkeydown="AllowOnlyNumeric1(event);" onfocusout="strvalid(this.id, 'Number');" /></td><%--onkeyup="Number_txtbox(event,this.id);" --%>
             </tr>
             <tr>                 
                 <td class="dComposeItemLabel" nowrap style="width: 56px">&nbsp;</td>
                 <td class="dComposeItemLabel1" nowrap>State</td>
                 <td><input id="txtState" runat="server" class="InputDefaultStyle" onkeypress="AllowOnlyText();" onfocusout="strvalid(this.id, 'String');" /></td>
                 <td class="dComposeItemLabel" nowrap>&nbsp;</td>
                 <td class="dComposeItemLabel1" nowrap>Country</td>
                 <td><input class="InputDefaultStyle" runat="server" id="txtCountry" onkeypress="AllowOnlyText();" onfocusout="strvalid(this.id, 'String');" /></td>
             </tr>
            <tr>
                <td class="dComposeItemLabel" nowrap style="width: 56px">&nbsp;</td>
                <td class="dComposeItemLabel1" nowrap>Phone No</td>
                <td><input class="InputDefaultStyle" runat="server" id="txtPhoneNo" onkeydown="AllowOnlyNumeric1(event);" onfocusout="strvalid(this.id, 'Number');" /></td>
                <td class="dComposeItemLabel" nowrap>&nbsp;</td>
                <td class="dComposeItemLabel1" nowrap>Fax No</td>
                <td><input class="InputDefaultStyle" runat="server" id="txtFaxNo" onkeydown="AllowOnlyNumeric1(event);" onfocusout="strvalid(this.id, 'Number');" /></td>
            </tr>
            <tr>
                <td class="dComposeItemLabel" nowrap style="width: 56px">&nbsp;</td>
                <td class="dComposeItemLabel1" nowrap>Email Id</td>
                <td><input class="InputDefaultStyle" runat="server" id="txtEmailId" /></td>
                <td class="dComposeItemLabel" nowrap>&nbsp;</td>
                <td class="dComposeItemLabel1" nowrap>Alternate Email Id</td>
                <td><input class="InputDefaultStyle" runat="server" id="txtAlternateEmailId" /></td>
            </tr>                        
            <tr>
                <td class="dComposeItemLabel" nowrap style="width: 56px">&nbsp;</td>
                <td class="dComposeItemLabel1" nowrap>PF number</td>
                <td><input class="InputDefaultStyle" runat="server" id="txtPFno" /></td>
                <td class="dComposeItemLabel" nowrap>&nbsp;</td>
                <td class="dComposeItemLabel1" nowrap>ESI number</td>
                <td><input class="InputDefaultStyle" runat="server" id="txtESIno" /></td>
            </tr>                        
            <tr>
                <td class="dComposeItemLabel" nowrap style="width: 56px">&nbsp;</td>
                <td class="dComposeItemLabel1" nowrap>Starting Date</td>
                <td><input class="InputDefaultStyle" runat="server" id="txtstartdate" /></td>
                <td class="dComposeItemLabel" nowrap>&nbsp;</td>
                <td class="dComposeItemLabel1" nowrap>Ending Date</td>
                <td><input class="InputDefaultStyle" runat="server" id="txtenddate" /></td>
            </tr>                        
           <tr>
               <td class="dComposeItemLabel" nowrap="nowrap" colspan="2"></td>
               <td><font color="red" size="3">*Mandatory Fields</font></td>
               <td class="dComposeItemLabel" nowrap="nowrap" colspan="2"></td>
               <td><input id="ToolBarCode" type="hidden" value="0" runat="server" style="width: 67px" /><input id="hCompanyId" type="hidden" value="1" runat="server" style="width: 67px" /></td>
           </tr>
            <tr>
                 <td colspan="6">&nbsp;</td>
            </tr>
           <tr>
               <td colspan="6" align="right">
                   <asp:ImageButton id="btncompany" ImageUrl="../Images/Save.png" runat="server" OnClientClick="javascript:create();" style="cursor:hand;" />
                   <asp:ImageButton id="Img2" ImageUrl="../Images/Modify.png" runat="server" OnClientClick="javascript:Update();" style="cursor:hand;" />&nbsp;
                   <asp:ImageButton ID="Back" runat="server" ImageUrl="../Images/Back.png" onmouseover="this.src='../Images/Backover.png';" onmouseout="this.src='../Images/Back.png';" OnClientClick="Back_Click" />&nbsp;
                   <asp:ImageButton id="img_clear" ImageUrl="~/Images/Clear.png" runat="server" 
                       OnClientClick="javascript:clearAll();" style="cursor:hand;" />
               </td>
           </tr>
          </table>       
        </td>
      </tr>
    </table>
</asp:Content>

