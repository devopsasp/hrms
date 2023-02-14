<%@ Page Language="C#" MasterPageFile="~/HRMS.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="Pf_NominiDetails.aspx.cs" Inherits="Hrms_PayRoll_Pf_NominiDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>
<script language="javascript" type="text/javascript">

    function isNumber(evt) {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }


  function show_message(msg)
    {
        alert(msg);
    }

    function address_copy()
    {

    //alert("hai");.checked==true   
 
     if(document.aspnetForm.ctl00$ContentPlaceHolder1$chk_address.checked==true) {
      
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
		
		
	   if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtFirstName.value))
	   {	
	   key+=" Enter Employee First Name  \n";
}
       		
if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txtfullname.value))
	   {	
	   key+=" Enter Employee Full Name  \n";
	   }

	   if (isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_Readerid.value)) {
	       key += " Enter Reader ID  \n";
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


 if (key != "") {
     alert(msg + key + "\n ******** Unable to Create!! ******** \n");

     return false;
 }
 else {
     //alert("Successful");		
     //document.aspnetForm.ctl00$ContentPlaceHolder1$ToolBarCode.value=1		
     //document.aspnetForm.submit();

     return true;

 }
	
}
    </script>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager> 
    
<div class="row">
                <div class="col-lg-12">
                    <h3 class="page-header">PF Nominee&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" 
                            CssClass="form-control" 
                            OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged">
                        </asp:DropDownList>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </h3>
                </div>
                <!-- /.col-lg-12 -->
            </div>
       <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
            <div class="panel-group" id="accordion">
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            Members Detail
                                        </h4>
                                    </div>
                                    <div id="collapseOne" class="panel-collapse collapse in">
                    <table cellpadding="1%" cellspacing="1%" width="100%" 
                                                class="table table-striped table-bordered table-hover">
                        
                        <tr>
                            <td>
                                Select Department</td>
                            <td>
                                <asp:DropDownList ID="ddl_dept" class="form-control" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddl_dept_SelectedIndexChanged" Width="100%">
                            </asp:DropDownList>

                            </td>
                            <td>
                                Select Employee</td>
                            <td>
                                <asp:DropDownList ID="ddl_employee" class="form-control" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddl_employee_SelectedIndexChanged1" Width="100%">
                                
                            </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Employee Name</td>
                            <td>
                            <input runat="server" id="txtName" class="form-control"
                                onkeypress="AllowOnlyText3();" maxlength="50" /></td>
                            <td>
                                Father's Name</td>
                            <td>
                               <input id="txtfathername" type="text" runat="server" class="form-control" maxlength="50" onkeypress="AllowOnlyText();" /></td>
                            <td>
                                Mother&#39;s Name</td>
                            <td>
                             <input runat="server" id="txt_mother" class="form-control"
                                onkeypress="AllowOnlyText();" maxlength="50" /></td>
                        </tr>
                        <tr>
                            <td>
                                Gender</td>
                            <td>
                             <asp:DropDownList ID="ddlgender" runat="server" class="form-control" Width="100%">
                                 <asp:ListItem Value="1" Text="Select"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Male"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Female"></asp:ListItem>
                            </asp:DropDownList>
                            </td>
                            <td>
                                Date of Birth</td>
                            <td>
                               <%--<input runat="server" onkeyup="fn_date(event,this.id);"  id="txtdob" class="form-control"
                                onkeypress="AllowOnlyText();" maxlength="50"/>--%>
                                <div style=" width:85%; float:left;">
                                <asp:TextBox  runat="server" onkeyup="fn_date(event,this.id);"  id="txtdob" class="form-control" 
                                onkeypress="AllowOnlyText();" ></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtdob"> </asp:CalendarExtender>
                                  </div> 
                                   <div style=" width:10%; float:left;  margin-left:5px; margin-top:3px;">                                                
                                                    <asp:Image ID="Image2" runat="server"  
                                                        Text="" Width="25px" 
                                                        ImageUrl="~/Images/calendaricon.png" />
                                                </div>
                                                               
                                </td>
                            <td>
                                Account No</td>
                            <td>
                             <input runat="server" id="txtAccNo" class="form-control"
                                 maxlength="20"  onkeypress="return isNumber(event)" /></td>
                        </tr>
                        <tr>
                            <td>
                                Email</td>
                            <td>
                               <input runat="server" id="txtemail" class="form-control"
                                maxlength="50" /></td>
                            <td>
                                Phone No</td>
                            <td>
                               <input runat="server" id="txtphoneno" class="form-control"
                                onkeypress="return isNumber(event)" maxlength="20"/></td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Permanent Address</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Address</td>
                            <td>
                            <input runat="server" id="txtaddress1" class="form-control"
                                maxlength="50" /></td>
                            <td>
                                City</td>
                            <td>
                            <input runat="server" id="txtcity1" class="form-control"
                                onkeypress="AllowOnlyText3();" maxlength="50" /></td>
                            <td>
                                District</td>
                            <td>
                            <input runat="server" id="txtdistrict1" class="form-control"
                                onkeypress="AllowOnlyText3();" maxlength="50" /></td>
                        </tr>
                        <tr>
                            <td>
                                State</td>
                            <td>
                            <input runat="server" id="txtstate1" class="form-control"
                                onkeypress="AllowOnlyText3();" maxlength="50" /></td>
                            <td>
                                Pin Code</td>
                            <td>
                            <input runat="server" id="txtpincode1" class="form-control"
                                 maxlength="50" onkeypress="return isNumber(event)" /></td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Temporary Address</td>
                            <td>
                               <asp:CheckBox ID="chk_address" Text="Same as Permanent" runat="server"
                                    oncheckedchanged="chk_address_CheckedChanged" AutoPostBack="True" />
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Address</td>
                            <td>
                            <input runat="server" id="txtaddress11" class="form-control"
                                 maxlength="50" /></td>
                            <td>
                                City</td>
                              <td>
                            <input runat="server" id="txtcity2" class="form-control"
                                onkeypress="AllowOnlyText3();" maxlength="50" /></td>
                            <td>
                                District</td>
                            <td>
                            <input runat="server" id="txtdistrict2" class="form-control"
                                onkeypress="AllowOnlyText3();" maxlength="50" /></td>
                        </tr>
                        <tr>
                            <td>
                                State</td>
                            <td>
                            <input runat="server" id="txtstate2" class="form-control"
                                onkeypress="AllowOnlyText3();" maxlength="50" /></td>
                            <td>
                                Pin Code</td>
                            <td>
                            <input runat="server" id="txtpincode2" class="form-control"
                               onkeypress="return isNumber(event)" maxlength="50" /></td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="6" align="left">
                  <%--      <asp:Label ID="lbl_Error" runat="server" Font-Bold="True" ForeColor="#0066FF" 
                            style="text-align: center"></asp:Label>--%>
                               </td>
                        </tr>
                     </table>   
                                    </div>
                                </div>
                                <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>--%>
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            </asp:UpdateProgress>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            Part A - EPF</h4>
                                    </div>
                                    <div id="collapseTwo" class="panel-collapse collapse in">
                                        <div style="width: 100%">
                    <table cellpadding="1%" cellspacing="1%" width="100%" class="table table-striped table-bordered table-hover">
                        
                        <tr>
                            <td>
                                Nominee Name</td>
                            <td >
                                <input class="form-control" runat="server" id="txt_nominee"  onkeypress="AllowOnlyText3();" /></td>
                            <td>
                                Gender</td>
                            <td >

                                <asp:DropDownList ID="ddl_gender" runat="server" class="form-control" Width="100%">
                                    <asp:ListItem Text="Select" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Male" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Female" Value="3"></asp:ListItem>
                                </asp:DropDownList></td>
                            <td>
                                Date of Birth</td>
                            <td>
                                <%--<input runat="server" id="txt_DOB" class="form-control" onkeypress="AllowOnlyText3();" onkeyup="fn_date(event,this.id);" 
                                    maxlength="50" />--%>
                                    
                                    <div style=" width:150px; float:left;">
                                        <asp:TextBox runat="server" id="txt_DOB" class="form-control" onkeypress="AllowOnlyText3();" onkeyup="fn_date(event,this.id);" 
                                     Width="150px"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txt_DOB">
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
                                Share in PF</td>
                            <td>
                                <input runat="server" id="txt_PF_Share" class="form-control" onkeypress="AllowOnlyText3();" maxlength="50" />
                            </td>
                            <td>
                                Relationship</td>
                            <td>
                                <a name="tab173"><asp:DropDownList ID="ddl_relationship_epf" runat="server" class="form-control" Width="100%">
                                    <asp:ListItem Text="Select" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Son" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Daughter" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Sister" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="Brother" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="Husband" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="Dependent Father" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="Dependent Mother" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="Wife" Value="9"></asp:ListItem>
                                </asp:DropDownList>
                                </a>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Same Address as Member</td>
                            <td>
                                <asp:CheckBox ID="chkbx_Address" runat="server" AutoPostBack="True"
                                  oncheckedchanged="chkbx_Address_CheckedChanged1" />
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Address</td>
                            <td>
                                <input runat="server" id="txt_address1" class="form-control"
                                maxlength="50" />

                            </td>
                            <td>
                                City</td>
                                <td>
                                <input runat="server" id="txt_city" class="form-control"
                                onkeypress="AllowOnlyText3();" maxlength="50" />
                            </td>
                            <td>
                                District</td>
                            <td>
                                <input runat="server" id="txt_district" class="form-control"
                                onkeypress="AllowOnlyText3();" maxlength="50" />
                            </td>
                        </tr>
                        <tr>
                            <td >
                                State</td>
                            <td >
                                <input runat="server" id="txt_state" class="form-control"
                                onkeypress="AllowOnlyText3();" maxlength="50" /></td>
                            <td>
                                Pin Code</td>
                            <td >
                                <input runat="server" id="txt_pincode" class="form-control"
                               onkeypress="return isNumber(event)" maxlength="50" /></td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:Button ID="btn_save_epf" runat="server" class="btn btn-success" 
                                    onclick="btn_save_epf_Click" Text="Save" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="left">
                                <a name="tab175">
                                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" CssClass="table table-hover table-striped"
                                    AutoGenerateColumns="False" onrowcancelingedit="GridView1_RowCancelingEdit" 
                                    onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing" 
                                    onrowupdating="GridView1_RowUpdating">

                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="ID" 
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Emp ID" 
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblemp" runat="server" Text='<%# Eval("EmployeeId") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" 
                                            HeaderText="Nominee Name" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_nominee" runat="server" Text='<%# Eval("Nomineename") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_nominee1" runat="server" CssClass="InputDefaultStyle1" 
                                                    Text='<%# Bind("Nomineename") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Gender" 
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Gender" runat="server" Text='<%# Eval("Gender") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddl_edit_Gender1" runat="server" 
                                                    CssClass="InputDefaultStyle1" DataTextField="Employee_First_Name" 
                                                    DataValueField="Employee_First_Name" Height="20px" Width="100px">
                                                    <asp:ListItem>Select
                                </asp:ListItem>
                                                    <asp:ListItem>Male
                                </asp:ListItem>
                                                    <asp:ListItem>Female
                                </asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DOB" 
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_DOB" runat="server" 
                                                    Text='<%# Eval("Dob","{0:dd/M/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_DOB1" runat="server" CssClass="InputDefaultStyle1" 
                                                    Text='<%# Bind("Dob","{0:dd/M/yyyy}") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" 
                                            HeaderText="Share in PF" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Share" runat="server" Text='<%# Eval("Pf_share") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_Share1" runat="server" CssClass="InputDefaultStyle1" 
                                                    Text='<%# Bind("Pf_share") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" 
                                            HeaderText="Relationship with Member" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="label_Relationship" runat="server" 
                                                    Text='<%# Eval("Relationship") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddl_edit_relationship" runat="server" 
                                                    CssClass="InputDefaultStyle1" DataTextField="Employee_First_Name" 
                                                    DataValueField="Employee_First_Name" Height="20px">
                                                    <asp:ListItem Text="Select" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Son" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Daughter" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Sister" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="Brother" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="Husband" Value="6"></asp:ListItem>
                                                    <asp:ListItem Text="Dependent Father" Value="7"></asp:ListItem>
                                                    <asp:ListItem Text="Dependent Mother" Value="8"></asp:ListItem>
                                                    <asp:ListItem Text="Wife" Value="9"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" 
                                            HeaderText="Address Line1" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_address1" runat="server" 
                                                    Text='<%# Eval("PermanentAddress1") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_address11" runat="server" CssClass="InputDefaultStyle1" 
                                                    Text='<%# Bind("PermanentAddress1") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="State" 
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_State" runat="server" Text='<%# Eval("PermanentState") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_State1" runat="server" CssClass="InputDefaultStyle1" 
                                                    Text='<%# Bind("PermanentState") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="District" 
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_District" runat="server" 
                                                    Text='<%# Eval("PermanentDistrict") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_District1" runat="server" CssClass="InputDefaultStyle1" 
                                                    Text='<%# Bind("PermanentDistrict") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="City" 
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_city" runat="server" Text='<%# Eval("PermanentCity") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_City1" runat="server" CssClass="InputDefaultStyle1" 
                                                    Text='<%# Bind("PermanentCity") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Pincode" 
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_postalcode" runat="server" 
                                                    Text='<%# Eval("PermanentPincode") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_postalcode1" runat="server" 
                                                    Text='<%# Bind("PermanentPincode") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="true" ShowEditButton="True" />
                                    </Columns>

                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblempty" runat="server" Text="No Records">
                                        </asp:Label>
                                    </EmptyDataTemplate>

                                </asp:GridView>
                                </a>
                               </td>
                        </tr>
                     </table>   
                     </div>
                                    </div>
                                </div>
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            Part B - EPS</h4>
                                    </div>
                                    <div id="collapseThree" class="panel-collapse collapse in">
                                        <table cellpadding="1%" cellspacing="1%" 
                                            class="table table-striped table-bordered table-hover" width="100%">
                                            <tr>
                                                <td>
                                                    Nominee Name</td>
                                                <td>
                                                    <input runat="server" id="txtnominee_name" class="form-control"
                                onkeypress="AllowOnlyText3();" maxlength="50" />
                                                </td>
                                                <td>
                                                    Gender</td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_gender_eps" runat="server" class="form-control" Width="100%">
                                                        <asp:ListItem Text="Select" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Male" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Female" Value="3"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    Date of Birth</td>
                                                <td>
                                                   <%-- <input runat="server" id="txtdob_eps" class="form-control"
                                onkeypress="AllowOnlyText3();" onkeyup="fn_date(event,this.id);" 
                                  maxlength="50" />--%>
                                                 <div style=" width:150px; float:left;">
                                                     <asp:TextBox   id="txtdob_eps" runat="server" class="form-control"
                                onkeypress="AllowOnlyText3();" onkeyup="fn_date(event,this.id);" Width="150px" ></asp:TextBox>
                                                     <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtdob_eps">
                                                     </asp:CalendarExtender>
                                                 </div>
                                                  <div style=" width:25px; float:left;  margin-left:10px; margin-top:3px;">                                                
                                                    <asp:Image ID="Image3" runat="server"  
                                                        Text="" Width="25px" 
                                                        ImageUrl="~/Images/calendaricon.png" />
                                                </div>



                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Relationship</td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_relationship" runat="server" class="form-control" Width="100%">
                                                        <asp:ListItem Text="Select" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Son" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Daughter" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="Sister" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="Brother" Value="5"></asp:ListItem>
                                                        <asp:ListItem Text="Husband" Value="6"></asp:ListItem>
                                                        <asp:ListItem Text="Dependent Father" Value="7"></asp:ListItem>
                                                        <asp:ListItem Text="Dependent Mother" Value="8"></asp:ListItem>
                                                        <asp:ListItem Text="Wife" Value="9"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Same Address as Member</td>
                                                <td>
                                                    <asp:CheckBox ID="chk_address_eps" runat="server" AutoPostBack="True" oncheckedchanged="chk_address_eps_CheckedChanged" />
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Address</td>
                                                <td>
                                                    <input runat="server" 
                                    id="txtaddress1_eps" class="form-control"
                                maxlength="50" /></a></td>
                                                <td> City</td>
                                                    <td>
                                                        <input runat="server" 
                                    id="txtcity_eps" class="form-control"
                                onkeypress="AllowOnlyText3();" maxlength="50" /></td>
                                                    <td>
                                                        District</td>
                                                    <td>
                                                        <a name="tab166"><input runat="server" 
                                    id="txtdistrict_eps" class="form-control"
                                onkeypress="AllowOnlyText3();" maxlength="50" /></td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    State</td>
                                                <td>
                                                    <input runat="server" id="txt_state_eps" class="form-control"
                                onkeypress="AllowOnlyText3();" maxlength="50" />
                                                </td>
                                                <td>
                                                    Pin Code</td>
                                                <td>
                                                    <input runat="server" id="txt_pincode_eps" class="form-control"
                                onkeypress="return isNumber(event)" maxlength="50" />
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    <asp:Button ID="btn_eps" runat="server" class="btn btn-success" 
                                                        onclick="btn_eps_Click" Text="Save" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" colspan="6">
                                                    <a name="tab177">
                                                    <asp:GridView ID="GridView3" runat="server" AllowSorting="True" CssClass="table table-hover table-striped"
                                                        AutoGenerateColumns="False" 
                                                        onrowcancelingedit="GridView3_RowCancelingEdit" 
                                                        onrowdeleting="GridView3_RowDeleting" onrowediting="GridView3_RowEditing" 
                                                        onrowupdating="GridView3_RowUpdating">
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="ID" 
                                                                ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblemployeeid" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Emp ID" 
                                                                ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblemp0" runat="server" Text='<%# Eval("EmployeeId") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" 
                                                                HeaderText="Nominee Name" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblnominee" runat="server" Text='<%# Eval("Nomineename") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <input runat="server" id="txtnominee"  value='<%# Bind("Nomineename") %>' 
                                                                     onkeypress="AllowOnlyText3();" maxlength="50" />
                                                                </EditItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Gender" 
                                                                ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgender" runat="server" 
                                                                        Text='<%# Eval("Gender") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddl_gender0" runat="server" CssClass="InputDefaultStyle" 
                                                                        ForeColor="#666666">
                                                                        <asp:ListItem Text="Select" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Male" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="Female" Value="3"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="D.O.B" 
                                                                ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_dob0" runat="server" 
                                                                        Text='<%# Eval("Dob","{0:dd/M/yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <input class="style001" runat="server" id="txtdob0" value='<%# Bind("Dob","{0:dd/M/yyyy}") %>' 
                                                                        onkeypress="AllowOnlyText3();" maxlength="50" />
                                                                </EditItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" 
                                                                HeaderText="Relationship with Member" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrelationship" runat="server" 
                                                                        Text='<%# Eval("Relationship") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddl_relationship1" runat="server" 
                                                                        CssClass="InputDefaultStyle1" DataTextField="Employee_First_Name" 
                                                                        DataValueField="Employee_First_Name" Height="20px" Width="100px">
                                                                        <asp:ListItem Text="Select" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Son" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="Daughter" Value="3"></asp:ListItem>
                                                                        <asp:ListItem Text="Sister" Value="4"></asp:ListItem>
                                                                        <asp:ListItem Text="Brother" Value="5"></asp:ListItem>
                                                                        <asp:ListItem Text="Husband" Value="6"></asp:ListItem>
                                                                        <asp:ListItem Text="Dependent Father" Value="7"></asp:ListItem>
                                                                        <asp:ListItem Text="Dependent Mother" Value="8"></asp:ListItem>
                                                                        <asp:ListItem Text="Wife" Value="9"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" 
                                                                HeaderText="Address 1" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbladdr1" runat="server" 
                                                                        Text='<%# Eval("PermanentAddress1") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <input class="style001" runat="server" id="txtaddr1" value='<%# Eval("PermanentAddress1") %>' 
                                onkeypress="AllowOnlyText3();" maxlength="50" />
                                                                </EditItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" 
                                                                HeaderText="District" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldistrict" runat="server" 
                                                                        Text='<%# Eval("PermanentDistrict") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <input class="style001" runat="server" id="txtdistrict2"  value='<%# Eval("PermanentDistrict") %>'
                                onkeypress="AllowOnlyText3();" maxlength="50" />
                                                                </EditItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="City" 
                                                                ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcity" runat="server" Text='<%# Eval("PermanentCity") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <input class=".style001" runat="server" id="txtcity" value='<%# Eval("PermanentCity") %>' 
                                onkeypress="AllowOnlyText3();" maxlength="50" />
                                                                </EditItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="State" 
                                                                ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblstate" runat="server" 
                                                                        Text='<%# Eval("PermanentState") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <input class=".style001" runat="server" id="txtstate111" value='<%# Eval("PermanentState") %>' 
                                onkeypress="AllowOnlyText3();" maxlength="50" />
                                                                </EditItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Pincode" 
                                                                ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblpincode" runat="server" 
                                                                        Text='<%# Eval("PermanentPincode") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <input class=".style001" runat="server" id="txtpincode_1" value='<%# Eval("PermanentPincode") %>' 
                                onkeypress="AllowOnlyText3();" maxlength="50" />
                                                                </EditItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:CommandField ShowDeleteButton="true" ShowEditButton="True" 
                                                                ShowCancelButton="true" />
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <asp:Label ID="lblempty1" runat="server" Text="No Records">
                    </asp:Label>
                                                        </EmptyDataTemplate>

                                                    </asp:GridView>
                                                    </a>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>


        </ContentTemplate>
    </asp:UpdatePanel>


    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td class="tdComposeHeader" valign="top" align="right">


                &nbsp;</td>
                </tr>
                </table>
</asp:Content>