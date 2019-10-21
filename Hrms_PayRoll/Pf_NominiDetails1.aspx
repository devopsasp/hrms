<%@ Page Language="C#" MasterPageFile="~/HRMS.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="Pf_NominiDetails1.aspx.cs"  Inherits="Hrms_PayRoll_Pf_NominiDetails" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css" media="screen">@import "tabs.css";</style>
<style type="text/css" media="screen">    @import "basic.css";
    #contbl
    {
        height: 64px;
    }
    #reftbl
    {
        height: 206px;
    }
    .style82
    {
        font-family: Calibri;
    }
    .style84
    {
        font-family: Calibri;
        font-size: x-small;
        color: #808080;
    }
    .style89
    {
        font-family: Calibri;
        font-size: x-small;
    }
      .style91
    {
        font: menu;
        border: 1px groove darkgray;
        width: 180px;
        font-size: x-small;
        color: #808080;
        font-family: Calibri;
        height: 17px;
    }
    #Text1
    {
        height: 19px;
        width: 178px;
    }
    .style92
    {
        height: 29px;
        width: 25%;
    }
    .style93
    {
        font-family: Calibri;
        font-size: x-small;
        height: 29px;
    }
    .style94
    {
        font-family: Calibri;
        font-size: x-small;
        height: 13px;
    }
    .style95
    {
        height: 13px;
    }
    </style>
<script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>
<script language="javascript" type="text/javascript">

  function show_message(msg)
    {
        alert(msg);
    }

    function address_copy()
    {

    //alert("hai");.checked==true   
 
     if(document.aspnetForm.ctl00$ContentPlaceHolder1$chk_address.checked==true)
     {
       
      document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentHouseNo.value= document.aspnetForm.ctl00$ContentPlaceHolder1$txtPresentHouseNo.value;  
      document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentStreetName.value=document.aspnetForm.ctl00$ContentPlaceHolder1$txtPresentStreetName.value;  
      document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentAddressLine1.value=document.aspnetForm.ctl00$ContentPlaceHolder1$txtPresentAddressLine1.value;  
      document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentAddressLine2.value=document.aspnetForm.ctl00$ContentPlaceHolder1$txtPresentAddressLine2.value;  
      document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentCity.value=document.aspnetForm.ctl00$ContentPlaceHolder1$txtPresentCity.value;  
      document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentState.value=document.aspnetForm.ctl00$ContentPlaceHolder1$txtPresentState.value;  
    }
    else
    {
       
      document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentHouseNo.value="";  
      document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentStreetName.value="";  
      document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentAddressLine1.value="";  
      document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentAddressLine2.value="";  
      document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentCity.value="";  
      document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentState.value="";  
    }    
    
   
    }



 function fn_date(event,txtid)
 {  
       var len;
       var txtvalue; 
       var bool_obj; 
       var i;    
      
      
          
       txtvalue= document.getElementById(txtid).value;
       txtlen=txtvalue.length;  
       
  if(event.keyCode!=8 && event.keyCode!=46 && event.keyCode!=35 && event.keyCode!=36 && event.keyCode!=37 && event.keyCode!=38 && event.keyCode!=39 && event.keyCode!=40)     
   {    
    
           if(txtlen!=0)
           {       
               bool_obj=fn_validate(txtlen,txtvalue);
                          
               if(bool_obj==true)
                 {
                      if(txtlen==2 || txtlen==5)
                      {
                      document.getElementById(txtid).value=txtvalue+"/";
                      }
                      else
                      {
                      document.getElementById(txtid).value=txtvalue;
                      }
                     
                 }
                 else
                 {            
                     
                   document.getElementById(txtid).value= txtvalue.substring(0,txtlen-1);              
                 }                       
            } 
    }                                 
 }
    
    function fn_validate(len,tval)
    {
    var str;
    
          switch(len)
           {
     
        case 1: if(tval<=3)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                  
                  
        case 2:               
                
                if(tval<=31 && tval>0)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
                
        case 3: str=tval.charAt(2);
        
                if(str=="/")
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
      case 4: str=tval.charAt(3);
        
                if(str<=1)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
      case 5: str=tval.substring(3,5); 
        
                if(str<=12 && str>0)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
       case 6: str=tval.charAt(5);
        
                if(str=="/")
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
        case 7: str=tval.charAt(6);
        
                if(str<=9 && str>0)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
        case 8: str=tval.substring(6,8);
        
                if(str>=18)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
        case 9: str=tval.charAt(8);
        
                if(str<=9)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
        case 10: str=tval.charAt(9);
        
                if(str<=9)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
                
        default :return false;   
                 break;
           }
    }
    </script>
 <script language="javascript" type="text/javascript">

  function show_message(msg)
    {
        alert(msg);
    }

    function address_copy()
    {

    //alert("hai");.checked==true   
 
     if(document.aspnetForm.ctl00$ContentPlaceHolder1$chk_address.checked==true)
     {
       
      document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentHouseNo.value= document.aspnetForm.ctl00$ContentPlaceHolder1$txtPresentHouseNo.value;  
      document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentStreetName.value=document.aspnetForm.ctl00$ContentPlaceHolder1$txtPresentStreetName.value;  
      document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentAddressLine1.value=document.aspnetForm.ctl00$ContentPlaceHolder1$txtPresentAddressLine1.value;  
      document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentAddressLine2.value=document.aspnetForm.ctl00$ContentPlaceHolder1$txtPresentAddressLine2.value;  
      document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentCity.value=document.aspnetForm.ctl00$ContentPlaceHolder1$txtPresentCity.value;  
      document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentState.value=document.aspnetForm.ctl00$ContentPlaceHolder1$txtPresentState.value;  
    }
    else
    {
       
      document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentHouseNo.value="";  
      document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentStreetName.value="";  
      document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentAddressLine1.value="";  
      document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentAddressLine2.value="";  
      document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentCity.value="";  
      document.aspnetForm.ctl00$ContentPlaceHolder1$txtPermanentState.value="";  
    }    
    
   
    }



 function fn_date(event,txtid)
 {  
       var len;
       var txtvalue; 
       var bool_obj; 
       var i;    
      
      
          
       txtvalue= document.getElementById(txtid).value;
       txtlen=txtvalue.length;  
       
  if(event.keyCode!=8 && event.keyCode!=46 && event.keyCode!=35 && event.keyCode!=36 && event.keyCode!=37 && event.keyCode!=38 && event.keyCode!=39 && event.keyCode!=40)     
   {    
    
           if(txtlen!=0)
           {       
               bool_obj=fn_validate(txtlen,txtvalue);
                          
               if(bool_obj==true)
                 {
                      if(txtlen==2 || txtlen==5)
                      {
                      document.getElementById(txtid).value=txtvalue+"/";
                      }
                      else
                      {
                      document.getElementById(txtid).value=txtvalue;
                      }
                     
                 }
                 else
                 {            
                     
                   document.getElementById(txtid).value= txtvalue.substring(0,txtlen-1);              
                 }                       
            } 
    }                                 
 }
    
    function fn_validate(len,tval)
    {
    var str;
    
          switch(len)
           {
     
        case 1: if(tval<=3)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                  
                  
        case 2:               
                
                if(tval<=31 && tval>0)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
                
        case 3: str=tval.charAt(2);
        
                if(str=="/")
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
      case 4: str=tval.charAt(3);
        
                if(str<=1)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
      case 5: str=tval.substring(3,5); 
        
                if(str<=12 && str>0)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
       case 6: str=tval.charAt(5);
        
                if(str=="/")
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
        case 7: str=tval.charAt(6);
        
                if(str<=9 && str>0)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
        case 8: str=tval.substring(6,8);
        
                if(str>=18)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
        case 9: str=tval.charAt(8);
        
                if(str<=9)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
        case 10: str=tval.charAt(9);
        
                if(str<=9)
                {
                return true; 
                }
                else
                {
                return false; 
                }                 
                break;
                
                
        default :return false;   
                 break;
           }
    }
    </script>
    <script language="javascript">
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
function check()
{	
	
	var msg="Please make sure the following fields are valid \n\n";
	var key="";
	if(document.aspnetForm.ctl00$ContentPlaceHolder1$ddl_dept.value=='sd')
		{
		key+=" Select Department\n";
		}
		if(document.aspnetForm.ctl00$ContentPlaceHolder1$ddl_employee.value=='se')
		{
		key+=" Select Employee\n";
		}
	if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtName.value)) 
		{
		key+=" Enter Name \n";
		}
		
		
	if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtAccNo.value))
	   {	
	   key+=" Enter Account Number  \n";
	   }
		
		if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtfathername .value))
	   {	
	   key+=" Enter Father Name  \n";
	   }
	   if(document.aspnetForm.ctl00$ContentPlaceHolder1$ddlgender.value==1)
		{
		key+=" Select Gender\n";
		}
	   
		
		if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtdob.value))
	   {	
	   key+=" Enter Date of Birth  \n";
	   }
		
//.........................
        if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtphoneno.value))
	   {	
	   key+=" Enter Phone No  \n";
	   }
	    if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_mother  .value))
	   {	
	   key+=" Enter Mother's Name  \n";
	   }
	    if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtaddress1.value))
	   {	
	   key+=" Enter Permanent Address1  \n";
	   }
	    if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtaddress11.value))
	   {	
	   key+=" Enter Temporary Address1   \n";
	   }
	    if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtaddress2.value))
	   {	
	   key+=" Enter Permanent Address2  \n";
	   }
	    if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtaddress22.value))
	   {	
	   key+=" Enter Temporary Address2  \n";
	   }
	      if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtcity1.value))
	   {	
	   key+=" Enter Permanent City  \n";
	   }
	      if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtcity2.value))
	   {	
	   key+=" Enter Temporary City  \n";
	   }
	      if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtstate1.value))
	   {	
	   key+=" Enter Permanent State  \n";
	   }
	      if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtstate2.value))
	   {	
	   key+=" Enter Temporary State  \n";
	   }
	      if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtdistrict1.value))
	   {	
	   key+=" Enter Permanent District  \n";
	   }
         if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtdistrict2.value))
	   {	
	   key+=" Enter Temporary District  \n";
	   }
	    if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtpincode1.value))
	   {	
	   key+=" Enter Permanent Pincode  \n";
	   }
	    if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtpincode2.value))
	   {	
	   key+=" Enter Temporary pincode  \n";
	   }

  if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtemail.value))
	   {	
	   key+=" Enter Email  \n";
	   }
	    


//..............................................
   	
   	
if(!isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtemail.value))
 {
  	
	if(!valid(document.aspnetForm.ctl00$ContentPlaceHolder1$txtEmailId,"Email"))
	{
		key+="Invalid Email ID \n";
	}
	
 }
 
 
	
	if(key!="")
	{
		 	alert(msg+key+"\n ******** Unable to Create!! ******** \n");  
		 	
            return false;
	}
	else
	{	
		//alert("Successful");		
    //document.aspnetForm.ctl00$ContentPlaceHolder1$ToolBarCode.value=1		
	//document.aspnetForm.submit();
	
	return true;
		
	}
	
}


function check_epf()
{	
	
	var msg="Please make sure the following fields are valid \n\n";
	var key="";
	if(document.aspnetForm.ctl00$ContentPlaceHolder1$ddl_dept.value=='sd')
		{
		key+=" Select Department\n";
		}
		if(document.aspnetForm.ctl00$ContentPlaceHolder1$ddl_employee.value=='se')
		{
		key+=" Select Employee\n";
		}
	if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_nominee  .value))
	   {	
	   key+=" Enter Nominee Name  \n";
	   }
	   if(document.aspnetForm.ctl00$ContentPlaceHolder1$ddl_gender.value==1)
		{
		key+=" Select Gender\n";
		} 
	   if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_DOB  .value))
	   {	
	   key+=" Enter Dob  \n";
	   } if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_PF_Share   .value))
	   {	
	   key+=" Enter PF Share  \n";
	   } if(document.aspnetForm.ctl00$ContentPlaceHolder1$ddl_relationship_epf.value==1)
	   {	
	   key+=" Choose Relationship  \n";
	   } if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_address1  .value))
	   {	
	   key+=" Enter Address1  \n";
	   } if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_address2  .value))
	   {	
	   key+=" Enter Address2  \n";
	   } if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_district  .value))
	   {	
	   key+=" Enter District  \n";
	   } if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_city   .value))
	   {	
	   key+=" Enter City  \n";
	   } if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_state  .value))
	   {	
	   key+=" Enter State  \n";
	   } if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_pincode.value))
	   {	
	   key+=" Enter Pincode  \n";
	   }
 if(key!="")
	{
		 	alert(msg+key+"\n ******** Unable to Create!! ******** \n");  
		 	
            return false;
	}
	else
	{	
		//alert("Successful");		
    //document.aspnetForm.ctl00$ContentPlaceHolder1$ToolBarCode.value=1		
	//document.aspnetForm.submit();
	
	return true;
		
	}
	
}

 function check_eps()
{	
	
	var msg="Please make sure the following fields are valid \n\n";
	var key="";
	if(document.aspnetForm.ctl00$ContentPlaceHolder1$ddl_dept.value=='sd')
		{
		key+=" Select Department\n";
		}
		if(document.aspnetForm.ctl00$ContentPlaceHolder1$ddl_employee.value=='se')
		{
		key+=" Select Employee\n";
		}
	if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtnominee_name.value))
	   {	
	   key+=" Enter Nominee Name  \n";
	   }
	   if(document.aspnetForm.ctl00$ContentPlaceHolder1$ddl_gender_eps.value==1)
		{
		key+=" Select Gender\n";
		} 
	   if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtdob_eps.value))
	   {	
	   key+=" Enter Dob  \n";
	   }
	   if(document.aspnetForm.ctl00$ContentPlaceHolder1$ddl_relationship.value==1)
	   {	
	   key+=" Choose Relationship  \n";
	   } if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtaddress1_eps.value))
	   {	
	   key+=" Enter Address1  \n";
	   } if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtaddredd2_eps.value))
	   {	
	   key+=" Enter Address2  \n";
	   } if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtdistrict_eps.value))
	   {	
	   key+=" Enter District  \n";
	   } if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtcity_eps.value))
	   {	
	   key+=" Enter City  \n";
	   } if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_state_eps.value))
	   {	
	   key+=" Enter State  \n";
	   } if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_pincode_eps.value))
	   {	
	   key+=" Enter Pincode  \n";
	   }
 
 
 


 
 //............................................................................................
 
	
	if(key!="")
	{
		 	alert(msg+key+"\n ******** Unable to Create!! ******** \n");  
		 	
            return false;
	}
	else
	{	
		//alert("Successful");		
    //document.aspnetForm.ctl00$ContentPlaceHolder1$ToolBarCode.value=1		
	//document.aspnetForm.submit();
	
	return true;
		
	}
	
}




function check_update()
{	
	
	var msg="Please make sure the following fields are valid \n\n";
	var key="";
	
	if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtEmployeeCode.value)) 
		{
		key+=" Enter Employee Code \n";
		}
		
		
//	if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtepwd.value))
//	   {	
//	   key+=" Enter Password  \n";
//	   }
//		
//		if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtecpwd.value))
//	   {	
//	   key+=" Enter ConfirmPassword  \n";
//	   }
//	  
 
	   if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtFirstName.value))
	   {	
	   key+=" Enter Employee First Name  \n";
	   }
		
		
if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtfullname.value))
	   {	
	   key+=" Enter Employee Full Name  \n";
	   }
	   
	   if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_Readerid.value))
	   {	
	   key+=" Enter Reader ID  \n";
	   }
	   
   	
   	
if(!isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtEmailId.value))
 {
  	
	if(!valid(document.aspnetForm.ctl00$ContentPlaceHolder1$txtEmailId,"Email"))
	{
		key+="Invalid Email ID \n";
	}
	
 }
 
 if(!isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtAEmailId.value))
 {
  
	if(!valid(document.aspnetForm.ctl00$ContentPlaceHolder1$txtAEmailId,"Email"))
	{
		key+="Invalid Alternate Email ID \n";
	}
 }
 
 //
 
 if(!isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtOfficeNo.value))
 {
  
	if(!valid(document.aspnetForm.ctl00$ContentPlaceHolder1$txtOfficeNo,"Phone"))
	{
		key+="Invalid Office Phone No \n";
	}
 }
 
 
 if(!isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtRecidenceNo.value))
 {
  
	if(!valid(document.aspnetForm.ctl00$ContentPlaceHolder1$txtRecidenceNo,"Phone"))
	{
		key+="Invalid Recidence Phone No\n";
	}
 }
 
 
 if(!isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtCellNo.value))
 {
  
	if(!valid(document.aspnetForm.ctl00$ContentPlaceHolder1$txtCellNo,"Phone"))
	{
		key+="Invalid Cell No\n";
	}
 }
 
 if(!isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtemgno.value))
 {
  
	if(!valid(document.aspnetForm.ctl00$ContentPlaceHolder1$txtemgno,"Phone"))
	{
		key+="Invalid Emergency Phone No\n";
	}
 }
 
	
	if(key!="")
	{
		 	alert(msg+key+"\n ******** Unable to Create!! ******** \n");  
		 	
            return false;
	}
	else
	{	
		//alert("Successful");		
    //document.aspnetForm.ctl00$ContentPlaceHolder1$ToolBarCode.value=1		
	//document.aspnetForm.submit();
	
	return true;
		
	}
	
}
    </script>

    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td class="tdComposeHeader" valign="top" align="right">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center" height="35px" class="border">
                        <div id="header123">

                      
                            <asp:ImageButton ID="img_btn_memberdetails" runat="server" Height="30px" Width="160px" 
                                ImageUrl="~/Images/Member.png" onmouseover="this.src='../Images/Memberover.png';" onmouseout="this.src='../Images/Member.png';"
                                onclick="img_btn_memberdetails_Click" />
                                <asp:ImageButton ID="img_btn_epf" runat="server" Height="30px" Width="160px"  
                                    ImageUrl="~/Images/epf.png" onmouseover="this.src='../Images/epfover.png';" onmouseout="this.src='../Images/epf.png';" onclick="img_btn_epf_Click"/>
                            <asp:ImageButton ID="img_btn_eps" runat="server" Height="30px" Width="150px" 
                               ImageUrl="~/Images/eps.png" onmouseover="this.src='../Images/epsover.png';" onmouseout="this.src='../Images/eps.png';" onclick="img_btn_eps_Click" />
                           </div>
                        
                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            &nbsp;<asp:Label ID="lbl_Error" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label></td>
                    </tr>
                </table>
                <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                            <td colspan="6" style="height: 29px" align="right" class="style89">
                                <a name=tab151>
                                <span class="style84">
                                Select Department</span></a></td>
                            <td colspan="6" align="center" class="style92">
                              &nbsp;&nbsp;&nbsp;
                                <a name=tab158>  
                                <asp:DropDownList ID="ddl_dept" runat="server" CssClass="InputDefaultStyle" 
                                ForeColor="#666666" Height="18px" Width="181px" AutoPostBack="True" 
                                    onselectedindexchanged="ddl_dept_SelectedIndexChanged">
                                 
                               
                            </asp:DropDownList>
                </a>
                            &nbsp;</td>
                            <td colspan="6" style="height: 29px" class="style89" align="center">
                                <a name=tab152><span class="style84">&nbsp;Select Employee</span><span 
                                class="style89">&nbsp;</span></a></td>
                            <td colspan="6" style="height: 29px">
                                <a name=tab159>  
                                <asp:DropDownList ID="ddl_employee" runat="server" CssClass="InputDefaultStyle" 
                                ForeColor="#666666" Height="18px" Width="181px" AutoPostBack="True" 
                                    onselectedindexchanged="ddl_employee_SelectedIndexChanged1">
                                
                            </asp:DropDownList>
                </a>
                                </a></td>
                                            
                </tr></table>
                <a name=tab2>
                <table visible="false" id="memberdetails_header" runat="server" width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="35px" class="border" style="background-color: #5D7B9D">
                            <span class="Title" 
                                style="font-family: calibri; font-size: medium; font-weight: bold; color: #FFFFFF">&nbsp;&nbsp;<img src="../Images/rp_arrow.gif" />&nbsp;Member<span 
                                class="style82"> Details</span></span></td>
                    </tr>
                     <tr>
                    <td colspan="6" style="height: 29px">
                            </td></tr>
                </table>
              <table visible="false" id="memberdetails" runat="server" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                            <td colspan="6" style="height: 29px" align="right" class="style89">
                                <a name=tab151>
                                <span class="style84">
                                Name of the Employee</span></a></td>
                            <td colspan="6" style="height: 29px" align="center">
                              <a name=tab149>
                            <input class="style91" runat="server" id="txtName" 
                                onkeypress="AllowOnlyText3();" maxlength="50" /></a>
                            </td>
                            <td colspan="6" style="height: 29px" class="style89">
                                <a name=tab152><span class="style84">&nbsp;Account No</span><span 
                                class="style89">&nbsp;</span></a></td>
                            <td colspan="6" style="height: 29px">
                             <a name=tab149>
                             <input class="style91" runat="server" id="txtAccNo" 
                                onkeypress="AllowOnlyText();" maxlength="50"/></a></td>
                                            
                </tr>
                 <tr>
                            <td colspan="6" style="height: 29px" align="right" class="style89">
                                <a name=tab151>
                                <span class="style84">
                                Father Name</span></a></td>
                            <td colspan="6" style="height: 29px" align="center">
                              <a name=tab149>
                               <input class="style91" id="txtfathername" type="text" runat="server" 
                                onkeyup="fn_date(event,this.id);" maxlength="10" /></a>
                            </td>
                            <td colspan="6" style="height: 29px" class="style89">
                            &nbsp;<a name=tab152><span class="style84">Mother's Name</span><span 
                                class="style89">&nbsp;</span></a></td>
                            <td colspan="6" style="height: 29px">
                             <a name=tab142>
                             <input class="style91" runat="server" id="txt_mother" 
                                onkeypress="AllowOnlyText();" maxlength="50" />
                          </a> 
                            </td>                 
                </tr>
                <tr>
                            <td colspan="6" style="height: 29px" align="right" class="style89">
                                <a name=tab151>
                                <span class="style84">
                               Gender</span></a></td>
                            <td colspan="6" style="height: 29px" align="center">
                                <a name=tab154>  <asp:DropDownList ID="ddlgender" runat="server" CssClass="InputDefaultStyle" 
                                ForeColor="#666666" Height="18px" Width="181px">
                                 <asp:ListItem Value="1" Text="Select"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Male"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Female"></asp:ListItem>
                            </asp:DropDownList>
                </a>
                            </td>
                           <td colspan="6" style="height: 29px" class="style89">
                                <a name=tab151>
                                <span class="style84">
                               Date of Birth</span></a></td>
                            <td colspan="6" style="height: 29px">
                               <input class="style91" runat="server" onkeyup="fn_date(event,this.id);"  id="txtdob" 
                                onkeypress="AllowOnlyText();" maxlength="50"/>
                                &nbsp;</td>                 
                </tr>
                <tr>
                            <td colspan="6" style="height: 29px" align="right" class="style89">
                                <a name=tab151>
                                <span class="style84">
                               Email</span></a></td>
                            <td colspan="6" style="height: 29px" align="center">
                                <a name=tab154>  <input class="style91" runat="server" id="txtemail" 
                                onkeypress="AllowOnlyText3();" maxlength="50" />
                </a>
                            </td>
                           <td colspan="6" style="height: 29px" class="style89">
                                <a name=tab151>
                                <span class="style84">
                               Phone No</span></a></td>
                            <td colspan="6" style="height: 29px">
                               <input class="style91" runat="server" id="txtphoneno" 
                                onkeypress="AllowOnlyText();" maxlength="50"/>
                                &nbsp;</td>                 
                </tr>
                 
                                <tr>
                            <td colspan="6" style="height: 29px" align="right" class="style89">
                                <a name=tab151>
                                <span class="style84" style="font-weight:bold">
                                &nbsp;Permanant Address</span></a></td>
                            <td colspan="6" style="height: 29px" align="center" class="style89">
                                <a name=tab151>
                                <span class="style84" style="font-weight:bold">
                               <asp:CheckBox ID="chk_address" Text="Same as Permanent" runat="server" 
                                    oncheckedchanged="chk_address_CheckedChanged" AutoPostBack="True" />
                            </td>
                            <td colspan="6" style="height: 29px" class="style89">
                                <a name=tab151>
                                <span class="style84" style="font-weight:bold">
                                &nbsp;Temporary Address</span></a></td>
                            <td colspan="6" style="height: 29px">
                                &nbsp;</td>                 
                </tr>
                 <tr>
                            <td colspan="6" style="height: 29px" align="right" class="style89">
                                <a name=tab151>
                                <span class="style84">
                                Address 1</span></a></td>
                            <td colspan="6" style="height: 29px" align="center">
                              <a name=tab149>
                            <input class="style91" runat="server" id="txtaddress1" 
                                onkeypress="AllowOnlyText3();" maxlength="50" /></a>
                            </td>
                            <td colspan="6" style="height: 29px" class="style89">
                            &nbsp;<a name=tab152><span class="style84">Address 1</span><span 
                                class="style89">&nbsp;</span></a></td>
                            <td colspan="6" style="height: 29px">
                             <a name=tab142>
                            <input class="style91" runat="server" id="txtaddress11" 
                                onkeypress="AllowOnlyText3();" maxlength="50" /></a>
                            </td>                 
                </tr>
               <%-- <tr>
                            <td colspan="6" style="height: 29px" align="right" class="style89">
                                <a name=tab151>
                                <span class="style84">
                                Address 2 </span></a></td>
                            <td colspan="6" style="height: 29px" align="center">
                              <a name=tab149>
                            <input class="style91" runat="server" id="txtaddress2" 
                                onkeypress="AllowOnlyText3();" maxlength="50" /></a>
                            </td>
                            <td colspan="6" style="height: 29px" class="style89">
                            &nbsp;<a name=tab1&nbsp;<a name=tab152><span class="style84">Address 2</span></a></td>
                            <td colspan="6" style="height: 29px">
                             <a name=tab142>
                            <input class="style91" runat="server" id="txtaddress22" 
                                onkeypress="AllowOnlyText3();" maxlength="50" /></a>
                            </td>                 
                </tr>--%>
                <tr>
                            <td colspan="6" style="height: 29px" align="right" class="style89">
                                <a name=tab151>
                                <span class="style84">
                                City</span></a></td>
                            <td colspan="6" style="height: 29px" align="center">
                              <a name=tab149>
                            <input class="style91" runat="server" id="txtcity1" 
                                onkeypress="AllowOnlyText3();" maxlength="50" /></a>
                            </td>
                            <td colspan="6" style="height: 29px" class="style89">
                            &nbsp;<a name=tab152><span class="style84">City</span><span 
                                class="style89">&nbsp;</span></a></td>
                            <td colspan="6" style="height: 29px">
                             <a name=tab142>
                            <input class="style91" runat="server" id="txtcity2" 
                                onkeypress="AllowOnlyText3();" maxlength="50" /></a>
                            </td>                 
                </tr>
                <tr>
                            <td colspan="6" style="height: 29px" align="right" class="style89">
                                <a name=tab151>
                                <span class="style84">
                                District</span></a></td>
                            <td colspan="6" style="height: 29px" align="center">
                              <a name=tab149>
                            <input class="style91" runat="server" id="txtdistrict1" 
                                onkeypress="AllowOnlyText3();" maxlength="50" /></a>
                            </td>
                            <td colspan="6" style="height: 29px" class="style89">
                            &nbsp;<a name=tab152><span class="style84">District</span><span 
                                class="style89">&nbsp;</span></a></td>
                            <td colspan="6" style="height: 29px">
                             <a name=tab142>
                            <input class="style91" runat="server" id="txtdistrict2" 
                                onkeypress="AllowOnlyText3();" maxlength="50" /></a>
                            </td>                 
                </tr>
                <tr>
                            <td colspan="6" style="height: 29px" align="right" class="style89">
                                <a name=tab151>
                                <span class="style84">
                                State</span></a></td>
                            <td colspan="6" style="height: 29px" align="center">
                              <a name=tab149>
                            <input class="style91" runat="server" id="txtstate1" 
                                onkeypress="AllowOnlyText3();" maxlength="50" /></a>
                            </td>
                            <td colspan="6" style="height: 29px" class="style89">
                            &nbsp;<a name=tab152><span class="style84">State</span><span 
                                class="style89">&nbsp;</span></a></td>
                            <td colspan="6" style="height: 29px">
                             <a name=tab142>
                            <input class="style91" runat="server" id="txtstate2" 
                                onkeypress="AllowOnlyText3();" maxlength="50" /></a>
                            </td>                 
                </tr>
                
                <tr>
                            <td colspan="6" style="height: 29px" align="right" class="style89">
                                <a name=tab151>
                                <span class="style84">
                                Pin Code</span></a></td>
                            <td colspan="6" style="height: 29px" align="center">
                              <a name=tab149>
                            <input class="style91" runat="server" id="txtpincode1" 
                                onkeypress="AllowOnlyText3();" maxlength="50" /></a>
                            </td>
                            <td colspan="6" style="height: 29px" class="style89">
                            &nbsp;<a name=tab152><span class="style84">Pin Code                             </span></a></td>
                            <td colspan="6" style="height: 29px">
                             <a name=tab142>
                            <input class="style91" runat="server" id="txtpincode2" 
                                onkeypress="AllowOnlyText3();" maxlength="50" /></a>
                            </td>                 
                </tr>
                </table>
                    
                     <table visible="false" id="memberdetails_save" runat="server" cellpadding="0" cellspacing="0" width="100%">
                <tr><td colspan="6" style="height: 29px" align="center">
                            &nbsp;</td></tr>
                           
                </table>
                         <table visible="false" id="epf_header" runat="server" width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="35px" class="border" style="background-color: #5D7B9D">
                            <span class="Title" 
                                style="font-family: calibri; font-size: medium; font-weight: bold; color: #FFFFFF">&nbsp;&nbsp;<img src="../Images/rp_arrow.gif" />&nbsp;Part A 
                            EPF</span></td>
                    </tr>
                    <tr>
                    <td colspan="6" style="height: 29px">
                            </td></tr>
                </table>
             <table visible="false" id="epf" runat="server" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                            <td colspan="6" align="right" class="style93">
                                <a name=tab151>
                                <span class="style84">
                                Name of the Nominee</span></a></td>
                            <td colspan="6" style="height: 29px" align="center">
                              <a name=tab149>
                            <input class="style91" runat="server" id="txt_nominee" 
                                onkeypress="AllowOnlyText3();" maxlength="50" /></a>
                            </td>
                            <td colspan="6" style="height: 29px" class="style89">
                            &nbsp;<a name=tab152><span class="style84">Select Gender</span><span 
                                class="style89">&nbsp;</span></a></td>
                            <td colspan="6" style="height: 29px">
                             <asp:DropDownList ID="ddl_gender" runat="server" CssClass="InputDefaultStyle" 
                                ForeColor="#666666" Height="16px" Width="181px">
                                 <asp:ListItem Value="1" Text="Select"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Male"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Female"></asp:ListItem>
                            </asp:DropDownList>
                            </td>                 
                </tr>
                 <tr>
                            <td colspan="6" align="right" class="style93">
                                <a name=tab151>
                                <span class="style84">
                                Date of Birth</span></a></td>
                            <td colspan="6" style="height: 29px" align="center">
                              <a name=tab149>
                            <input class="style91" runat="server" id="txt_DOB" 
                                onkeypress="AllowOnlyText3();" onkeyup="fn_date(event,this.id);" maxlength="50" /></a>
                            </td>
                            <td colspan="6" style="height: 29px" class="style89">
                            &nbsp;<a name=tab152><span class="style84">Share in PF</span><span 
                                class="style89">&nbsp;</span></a></td>
                            <td colspan="6" style="height: 29px">
                             <a name=tab142>
                            <input class="style91" runat="server" id="txt_PF_Share" 
                                onkeypress="AllowOnlyText3();" maxlength="50" /></a> %
                            </td>                 
                </tr>
                <tr>
                            <td colspan="6" align="right" class="style93">
                                <a name=tab151>
                                <span class="style84">
                                RelationShip with Member</span></a></td>
                            <td colspan="6" style="height: 29px" align="center">
                              &nbsp;<a name=tab154><asp:DropDownList 
                                    ID="ddl_relationship_epf" runat="server" CssClass="InputDefaultStyle" 
                                ForeColor="#666666" Height="16px" Width="181px">
                                 <asp:ListItem Value="1" Text="Select"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Son"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Daughter"></asp:ListItem>
                                 <asp:ListItem Value="4" Text="Sister"></asp:ListItem>
                                  <asp:ListItem Value="5" Text="Brother"></asp:ListItem>
                                   <asp:ListItem Value="6" Text="Husband"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="Dependent Father"></asp:ListItem>
                                     <asp:ListItem Value="8" Text="Dependent Mother"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="Wife"></asp:ListItem>
                            </asp:DropDownList>
                </a>
                            </td>
                            <td colspan="6" style="height: 29px" class="style89">
                                &nbsp;</td>
                            <td colspan="6" style="height: 29px">
                                &nbsp;</td>                 
                </tr>
                                <tr>
                            <td colspan="6" align="right" class="style93">
                                <a name=tab151>
                                <span class="style84">
                                &nbsp;Address same as Member</span></a></td>
                            <td colspan="6" style="font-weight:bold;height: 29px;" class="style84">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:CheckBox ID="chkbx_Address" runat="server" AutoPostBack="True" 
                                    oncheckedchanged="chkbx_Address_CheckedChanged1" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td colspan="6" style="height: 29px" class="style89">
                                &nbsp;</td>
                            <td colspan="6" style="height: 29px">
                                &nbsp;</td>                 
                </tr>
                 <tr>
                            <td colspan="6" align="right" class="style93">
                                <a name=tab151>
                                <span class="style84">
                                Address 1</span></a></td>
                            <td colspan="6" style="height: 29px" align="center">
                              <a name=tab149>
                            <input class="style91" runat="server" id="txt_address1" 
                                onkeypress="AllowOnlyText3();" maxlength="50" /></a>
                            </td>
                            <td colspan="6" style="height: 29px" class="style89">
                                <a name=tab160>
                                <span class="style84">
                                District </span></a></td>
                            <td colspan="6" style="height: 29px">
                             &nbsp;<a name=tab161><input class="style91" runat="server" id="txt_district" 
                                onkeypress="AllowOnlyText3();" maxlength="50" /></a></td>                 
                </tr>
                <tr>
                            <td colspan="6" align="right" class="style94">
                                <a name=tab1&nbsp;&lt;a0 name=tab152><span class="style84">City</span></a></td>
                            <td colspan="6" align="center" class="style95">
                              &nbsp;<a name=tab162><input class="style91" runat="server" id="txt_city" 
                                onkeypress="AllowOnlyText3();" maxlength="50" /></a></td>
                            <td colspan="6" class="style94">
                                <a name=tab163>
                                <span class="style84">
                                State</span></a></td>
                            <td colspan="6" class="style95">
                             &nbsp;<a name=tab164><input class="style91" runat="server" id="txt_state" 
                                onkeypress="AllowOnlyText3();" maxlength="50" /></a></td>                 
                </tr>
                <tr>
                            <td colspan="6" align="right" class="style93">
                                <a name=tab165><span class="style84">Pin Code                             </span></a></td>
                            <td colspan="6" style="height: 29px" align="center">
                              &nbsp;<a name=tab166><input class="style91" runat="server" id="txt_pincode" 
                                onkeypress="AllowOnlyText3();" maxlength="50" /></a></td>
                            <td colspan="6" style="height: 29px" class="style89">
                                &nbsp;</td>
                            <td colspan="6" style="height: 29px">
                                &nbsp;</td>                 
                </tr>
                </table>
                <table visible="false" id="epf_save" runat="server" cellpadding="0" cellspacing="0" width="100%">
                <tr><td colspan="6" style="height: 29px" align="center">
                            <a name=tab26>
                <asp:ImageButton ID="btn_save_epf" runat="server" ImageUrl="../Images/Save.png" 
                                onmouseover="this.src='../Images/Saveover.png';" 
                                onmouseout="this.src='../Images/Save.png';" OnClientClick="return check_epf();"
                    Width="124px" onclick="btn_save_epf_Click" />
                            </a></td></tr>
                            <tr><td>
                            <asp:GridView ID="GridView1" Font-Size="Smaller" runat="server" AllowSorting="True" 
                    AutoGenerateColumns="False" Height="16px" ShowFooter="false" 
        Width="100%" BackColor="White" BorderStyle="None" CellPadding="4" 
                    ForeColor="#C18685" HorizontalAlign="Center"
        style="color: #808000" onrowcancelingedit="GridView1_RowCancelingEdit" 
                                    onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing" 
                                    onrowupdating="GridView1_RowUpdating">
                    <RowStyle BackColor="White" ForeColor="#666666" Font-Names="Calibri" BorderColor="#333333" 
                                    BorderStyle="None" Font-Bold="True" />
                    <Columns>
                      <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false" HeaderText="ID" HeaderStyle-HorizontalAlign="Center">
                        
                            <ItemTemplate>
                                <asp:Label ID="lblid" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                            </ItemTemplate>

                            
                           

        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                         <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false" HeaderText="Emp ID" HeaderStyle-HorizontalAlign="Center">
                        
                            <ItemTemplate>
                                <asp:Label ID="lblemp" runat="server" Text='<%# Eval("EmployeeId") %>'></asp:Label>
                            </ItemTemplate>

                            
                           

        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Nominee Name" HeaderStyle-HorizontalAlign="Center">
                        
                            <ItemTemplate>
                                <asp:Label ID="lbl_nominee" runat="server" Text='<%# Eval("Nomineename") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                            <asp:TextBox ID="txt_nominee1" CssClass="InputDefaultStyle1" runat="server" Text='<%# Bind("Nomineename") %>'></asp:TextBox>
                            </EditItemTemplate>
                            
                           

        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                                             
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Gender" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Gender" runat="server" Text='<%# Eval("Gender") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate> 
                            <asp:DropDownList ID="ddl_edit_Gender1" Height="20px" 
                                Width="100px" runat="server" CssClass="InputDefaultStyle1" DataTextField="Employee_First_Name" 
                                    DataValueField="Employee_First_Name">
                                     <asp:ListItem>Select
                                </asp:ListItem>
                                <asp:ListItem>Male
                                </asp:ListItem>
                                 <asp:ListItem>Female
                                </asp:ListItem>
                               
                                </asp:DropDownList>
                                 
                            
                            </EditItemTemplate>
                           
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="DOB" HeaderStyle-HorizontalAlign="Center">
                        
                            <ItemTemplate>
                                <asp:Label ID="lbl_DOB" runat="server" Text='<%# Eval("Dob","{0:dd/M/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                            <asp:TextBox ID="txt_DOB1" CssClass="InputDefaultStyle1" runat="server" Text='<%# Bind("Dob","{0:dd/M/yyyy}") %>'></asp:TextBox>
                            </EditItemTemplate>
                            
                           

        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Share in PF" HeaderStyle-HorizontalAlign="Center">
                        
                            <ItemTemplate>
                                <asp:Label ID="lbl_Share" runat="server" Text='<%# Eval("Pf_share") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                            <asp:TextBox ID="txt_Share1" CssClass="InputDefaultStyle1" runat="server" Text='<%# Bind("Pf_share") %>' ></asp:TextBox>
                            </EditItemTemplate>
                            
                           

        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Relationship with Member"  HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="label_Relationship" runat="server" Text='<%# Eval("Relationship") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate> 
                            <asp:DropDownList ID="ddl_edit_relationship" Height="20px" 
                                runat="server" CssClass="InputDefaultStyle1" DataTextField="Employee_First_Name" 
                                    DataValueField="Employee_First_Name">
                                     <asp:ListItem Value="1" Text="Select"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Son"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Daughter"></asp:ListItem>
                                 <asp:ListItem Value="4" Text="Sister"></asp:ListItem>
                                  <asp:ListItem Value="5" Text="Brother"></asp:ListItem>
                                   <asp:ListItem Value="6" Text="Husband"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="Dependent Father"></asp:ListItem>
                                     <asp:ListItem Value="8" Text="Dependent Mother"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="Wife"></asp:ListItem>
                                </asp:DropDownList>
                                 
                            </EditItemTemplate>
                           

     <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                           <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Address Line1" HeaderStyle-HorizontalAlign="Center">
                        
                            <ItemTemplate>
                                <asp:Label ID="lbl_address1" runat="server" Text='<%# Eval("PermanentAddress1") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                            <asp:TextBox ID="txt_address11" CssClass="InputDefaultStyle1" runat="server" Text='<%# Bind("PermanentAddress1") %>' ></asp:TextBox>
                            </EditItemTemplate>
                            
                           

        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                       <%--  <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Address Line2" HeaderStyle-HorizontalAlign="Center">
                        
                            <ItemTemplate>
                                <asp:Label ID="lbl_address2" runat="server" Text='<%# Eval("PermanentAddress2") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                            <asp:TextBox ID="txt_address21" CssClass="InputDefaultStyle1" runat="server" Text='<%# Bind("PermanentAddress2") %>' ></asp:TextBox>
                            </EditItemTemplate>
                            
                           

        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>--%>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="State" HeaderStyle-HorizontalAlign="Center">
                        
                            <ItemTemplate>
                                <asp:Label ID="lbl_State" runat="server" Text='<%# Eval("PermanentState") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                            <asp:TextBox ID="txt_State1" CssClass="InputDefaultStyle1" runat="server" Text='<%# Bind("PermanentState") %>' ></asp:TextBox>
                            </EditItemTemplate>
                            
                           

        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                       
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="District" HeaderStyle-HorizontalAlign="Center">
                        
                            <ItemTemplate>
                                <asp:Label ID="lbl_District" runat="server" Text='<%# Eval("PermanentDistrict") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                            <asp:TextBox ID="txt_District1" CssClass="InputDefaultStyle1" runat="server" Text='<%# Bind("PermanentDistrict") %>' ></asp:TextBox>
                            </EditItemTemplate>
                            
                           

        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
              
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="City" HeaderStyle-HorizontalAlign="Center">
                        
                            <ItemTemplate>
                                <asp:Label ID="lbl_city" runat="server" Text='<%# Eval("PermanentCity") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                            <asp:TextBox ID="txt_City1" CssClass="InputDefaultStyle1" runat="server" Text='<%# Bind("PermanentCity") %>' ></asp:TextBox>
                            </EditItemTemplate>
                            
                           

        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                                    
                        
                        
                        
                         <asp:TemplateField ItemStyle-HorizontalAlign="Center"  HeaderText="Pincode"  HeaderStyle-HorizontalAlign="Center">
                             <ItemTemplate>
                                <asp:Label ID="Lbl_postalcode" runat="server" Text='<%# Eval("PermanentPincode") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txt_postalcode1" Text='<%# Bind("PermanentPincode") %>' runat="server"></asp:TextBox>
                            </EditItemTemplate>
                             <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                      
                       
                        
                       
                        
                        <asp:CommandField ShowDeleteButton="true" ShowEditButton="True" />
                    </Columns>
                                   <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <EmptyDataTemplate>
                    <asp:Label ID="lblempty" Text="No Records" runat="server">
                    </asp:Label> 
                    
                    </EmptyDataTemplate>
                                   <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" 
                        ForeColor="#333333" Wrap="True" />
                                   <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" 
                                       Font-Names="Calibri" BorderStyle="None" 
                                       Font-Size="Small" />
                </asp:GridView>
                            </td>
                            </tr>
                </table>
                 <table visible="false" id="eps_header" runat="server" width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="35px" class="border" style="background-color: #5D7B9D">
                            <span class="Title" 
                                style="font-family: calibri; font-size: medium; font-weight: bold; color: #FFFFFF">&nbsp;&nbsp;<img src="../Images/rp_arrow.gif" />&nbsp;Part B 
                            EPS</span></td>
                    </tr>
                    <tr>
                    <td colspan="6" style="height: 29px">
                            </td></tr>
                </table>
                <table visible="false" id="eps" runat="server" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                            <td colspan="6" style="height: 29px" align="right" class="style89">
                                <a name=tab151>
                                <span class="style84">
                                Name of the Nominee</span></a></td>
                            <td colspan="6" style="height: 29px" align="center">
                              <a name=tab149>
                            <input class="style91" runat="server" id="txtnominee_name" 
                                onkeypress="AllowOnlyText3();" maxlength="50" /></a>
                            </td>
                            <td colspan="6" style="height: 29px" class="style89">
                            &nbsp;<a name=tab152><span class="style84">Select Gender</span><span 
                                class="style89">&nbsp;</span></a></td>
                            <td colspan="6" style="height: 29px">
                             <asp:DropDownList ID="ddl_gender_eps" runat="server" CssClass="InputDefaultStyle" 
                                ForeColor="#666666" Height="16px" Width="181px">
                                 <asp:ListItem Value="1" Text="Select"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Male"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Female"></asp:ListItem>
                            </asp:DropDownList>
                            </td>                 
                </tr>
                 <tr>
                            <td colspan="6" style="height: 29px" align="right" class="style89">
                                <a name=tab151>
                                <span class="style84">
                                Date of Birth</span></a></td>
                            <td colspan="6" style="height: 29px" align="center">
                              <a name=tab149>
                            <input class="style91" onkeyup="fn_date(event,this.id);" runat="server" id="txtdob_eps" 
                                onkeypress="AllowOnlyText3();" maxlength="50" /></a>
                            </td>
                            <td colspan="6" style="height: 29px" class="style89">
                            &nbsp;<a name=tab151><span class="style84">RelationShip with Member</span></a></td>
                            <td colspan="6" style="height: 29px">
                                <a name=tab155><asp:DropDownList ID="ddl_relationship" runat="server" CssClass="InputDefaultStyle" 
                                ForeColor="#666666" Height="16px" Width="181px">
                                 <asp:ListItem Value="1" Text="Select"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Son"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Daughter"></asp:ListItem>
                                 <asp:ListItem Value="4" Text="Sister"></asp:ListItem>
                                  <asp:ListItem Value="5" Text="Brother"></asp:ListItem>
                                   <asp:ListItem Value="6" Text="Husband"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="Dependent Father"></asp:ListItem>
                                     <asp:ListItem Value="8" Text="Dependent Mother"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="Wife"></asp:ListItem>
                                      
                            </asp:DropDownList>
                </a>
                            </td>                 
                </tr>
              
                                <tr>
                            <td colspan="6" style="height: 29px" align="right" class="style89">
                                <a name=tab151>
                                <span class="style84">
                                &nbsp;Address same as Member</span></a></td>
                            <td colspan="6" style="font-weight:bold;height: 29px;" class="style84" >
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:CheckBox ID="chk_address_eps" runat="server" AutoPostBack="True" 
                                    oncheckedchanged="chk_address_eps_CheckedChanged" />
                            </td>
                            <td colspan="6" style="height: 29px" class="style89">
                                &nbsp;</td>
                            <td colspan="6" style="height: 29px">
                                &nbsp;</td>                 
                </tr>
                 <tr>
                            <td colspan="6" style="height: 29px" align="right" class="style89">
                                <a name=tab151>
                                <span class="style84">
                                Address 1</span></a></td>
                            <td colspan="6" style="height: 29px" align="center">
                              <a name=tab149>
                            <input class="style91" runat="server" id="txtaddress1_eps" 
                                onkeypress="AllowOnlyText3();" maxlength="50" /></a>
                            </td>
                            <td colspan="6" style="height: 29px" class="style89">
                                <a name=tab167>
                                <span class="style84">
                                District </span></a></td>
                            <td colspan="6" style="height: 29px">
                             &nbsp;<a name=tab168><input class="style91" runat="server" id="txtdistrict_eps" 
                                onkeypress="AllowOnlyText3();" maxlength="50" /></a></td>                 
                </tr>
                <tr>
                            <td colspan="6" style="height: 29px" align="right" class="style89">
                                <a name=tab1&nbsp;<a name=tab152><span class="style84">City</span></a></td>
                            <td colspan="6" style="height: 29px" align="center">
                              &nbsp;<a name=tab169><input class="style91" runat="server" id="txtcity_eps" 
                                onkeypress="AllowOnlyText3();" maxlength="50" /></a></td>
                            <td colspan="6" style="height: 29px" class="style89">
                                <a name=tab170>
                                <span class="style84">
                                State</span></a></td>
                            <td colspan="6" style="height: 29px">
                             &nbsp;<a name=tab171><input class="style91" runat="server" id="txt_state_eps" 
                                onkeypress="AllowOnlyText3();" maxlength="50" /></a></td>                 
                </tr>
                <tr>
                            <td colspan="6" style="height: 29px" align="right" class="style89">
                                <a name=tab172><span class="style84">Pin Code                             </span></a></td>
                            <td colspan="6" style="height: 29px" align="center">
                              &nbsp;<a name=tab173><input class="style91" runat="server" id="txt_pincode_eps" 
                                onkeypress="AllowOnlyText3();" maxlength="50" /></a></td>
                            <td colspan="6" style="height: 29px" class="style89">
                            &nbsp;</td>
                            <td colspan="6" style="height: 29px">
                             &nbsp;</td>                 
                </tr>
                </table>
                <table visible="false" id="eps_save" runat="server" cellpadding="0" cellspacing="0" width="100%">
                <tr><td colspan="6" style="height: 29px" align="center">
                            <a name=tab26>
                <asp:ImageButton ID="btn_eps" runat="server" ImageUrl="../Images/Save.png" 
                                onmouseover="this.src='../Images/Saveover.png';" 
                                onmouseout="this.src='../Images/Save.png';"
                    OnClientClick="return  check_eps();" Width="124px" onclick="btn_eps_Click1" />
                            </a></td></tr>
                            <tr><td>
                            <asp:GridView ID="GridView3" Font-Size="Smaller" runat="server" AllowSorting="True" 
                    AutoGenerateColumns="False" Height="16px" ShowFooter="false" 
        Width="100%" BackColor="White" BorderStyle="None" CellPadding="4" 
                    ForeColor="#C18685" HorizontalAlign="Center"
        style="color: #808000" onrowediting="GridView3_RowEditing" 
                                    onrowcancelingedit="GridView3_RowCancelingEdit" 
                                    onrowupdating="GridView3_RowUpdating" 
                                    onrowdeleting="GridView3_RowDeleting">
                    <RowStyle BackColor="White" ForeColor="#666666" Font-Names="Calibri" BorderColor="#333333" 
                                    BorderStyle="None" Font-Bold="True" />
                    <Columns>
                     <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false" HeaderText="ID" HeaderStyle-HorizontalAlign="Center">
                        
                            <ItemTemplate>
                                <asp:Label ID="lblemployeeid" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                            </ItemTemplate>
                          

        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false" HeaderText="Emp ID" HeaderStyle-HorizontalAlign="Center">
                        
                            <ItemTemplate>
                                <asp:Label ID="lblemp" runat="server" Text='<%# Eval("EmployeeId") %>'></asp:Label>
                            </ItemTemplate>

                            
                           

        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Nominee Name" HeaderStyle-HorizontalAlign="Center">
                        
                            <ItemTemplate>
                                <asp:Label ID="lblnominee" runat="server" Text='<%# Eval("Nomineename") %>'></asp:Label>
                            </ItemTemplate>
                           <EditItemTemplate>
                                 <input class=".style001" runat="server" id="txtnominee"  value='<%# Bind("Nomineename") %>' 
                                onkeypress="AllowOnlyText3();" maxlength="50" />
                           </EditItemTemplate>

        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                                             
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Gender" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblgender" runat="server" Text='<%# Eval("Gender") %>'></asp:Label>
                            </ItemTemplate>
                           <EditItemTemplate>
                             <asp:DropDownList ID="ddl_gender" runat="server" CssClass="InputDefaultStyle" 
                                ForeColor="#666666" Height="16px" Width="100px">
                                 <asp:ListItem Value="1" Text="Select"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Male"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Female"></asp:ListItem>
                            </asp:DropDownList>  
                           </EditItemTemplate>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                           <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="D.O.B"  HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lbl_dob" runat="server" Text='<%# Eval("Dob","{0:dd/M/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                           <EditItemTemplate>
                             <input class="style001" runat="server" id="txtdob" value='<%# Bind("Dob","{0:dd/M/yyyy}") %>' 
                                onkeypress="AllowOnlyText3();" maxlength="50" />
                           </EditItemTemplate>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                       
                        
                         <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Relationship with Member"  HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblrelationship" runat="server" Text='<%# Eval("Relationship") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate> 
                            <asp:DropDownList ID="ddl_relationship1" Height="20px" 
                                Width="100px" runat="server" CssClass="InputDefaultStyle1" DataTextField="Employee_First_Name" 
                                    DataValueField="Employee_First_Name" 
                                     >
                                      <asp:ListItem Value="1" Text="Select"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Son"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Daughter"></asp:ListItem>
                                 <asp:ListItem Value="4" Text="Sister"></asp:ListItem>
                                  <asp:ListItem Value="5" Text="Brother"></asp:ListItem>
                                   <asp:ListItem Value="6" Text="Husband"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="Dependent Father"></asp:ListItem>
                                     <asp:ListItem Value="8" Text="Dependent Mother"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="Wife"></asp:ListItem>
                              
                                </asp:DropDownList>
                                
                            
                            </EditItemTemplate>
                           

     <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                       
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Address 1"  HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lbladdr1" runat="server" Text='<%# Eval("PermanentAddress1") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate> 
                              <input class="style001" runat="server" id="txtaddr1" value='<%# Eval("PermanentAddress1") %>' 
                                onkeypress="AllowOnlyText3();" maxlength="50" />
                            </EditItemTemplate>
                           

     <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                       
                    <%--    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Address 2"  HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lbladdr2" runat="server" Text='<%# Eval("PermanentAddress2") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate> 
                              <input class=".style001" runat="server" id="txtaddr2" value='<%# Eval("PermanentAddress2") %>' 
                                onkeypress="AllowOnlyText3();" maxlength="50" />
                            </EditItemTemplate>
                           

     <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>--%>
                       
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="District"  HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lbldistrict" runat="server" Text='<%# Eval("PermanentDistrict") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate> 
                             <input class="style001" runat="server" id="txtdistrict1"  value='<%# Eval("PermanentDistrict") %>'
                                onkeypress="AllowOnlyText3();" maxlength="50" />
                            </EditItemTemplate>
                           

     <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
              
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="City"  HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblcity" runat="server" Text='<%# Eval("PermanentCity") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate> 
                             <input class=".style001" runat="server" id="txtcity" value='<%# Eval("PermanentCity") %>' 
                                onkeypress="AllowOnlyText3();" maxlength="50" />
                            </EditItemTemplate>
                           

     <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                                    
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center"  HeaderText="State"  HeaderStyle-HorizontalAlign="Center">
                             <ItemTemplate>
                                <asp:Label ID="lblstate" runat="server" Text='<%# Eval("PermanentState") %>'></asp:Label>
                            </ItemTemplate>
                             <EditItemTemplate>
                               <input class=".style001" runat="server" id="txtstate111" value='<%# Eval("PermanentState") %>' 
                                onkeypress="AllowOnlyText3();" maxlength="50" />
                             </EditItemTemplate>
                             <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        
                        
                         <asp:TemplateField ItemStyle-HorizontalAlign="Center"  HeaderText="Pincode"  HeaderStyle-HorizontalAlign="Center">
                             <ItemTemplate>
                                <asp:Label ID="lblpincode" runat="server" Text='<%# Eval("PermanentPincode") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                              <input class=".style001" runat="server" id="txtpincode_1" value='<%# Eval("PermanentPincode") %>' 
                                onkeypress="AllowOnlyText3();" maxlength="50" />
                            </EditItemTemplate>
                             <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                      
                       
                        
                       
                        
                        <asp:CommandField ShowEditButton="True" ShowCancelButton="true" ShowDeleteButton="true" />
                    </Columns>
                                   <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <EmptyDataTemplate>
                    <asp:Label ID="lblempty" Text="No Records" runat="server">
                    </asp:Label> 
                    
                    </EmptyDataTemplate>
                                   <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" 
                        ForeColor="#333333" Wrap="True" />
                                   <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" 
                                       Font-Names="Calibri" BorderStyle="None" 
                                       Font-Size="Small" />
                </asp:GridView>
                            </td>
                            </tr>
                </table>
</asp:Content>