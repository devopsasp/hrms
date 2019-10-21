// JScript File

//function Number_txtbox(event,txtid)
//  {  
//       var txtlen;
//       var txtvalue; 
//       var bool_obj; 
//       var count;  
//       var str;           
//          
//       txtvalue= document.getElementById(txtid).value;
//       txtlen=txtvalue.length; 
//       count=txtlen-1;
//       
//  if(event.keyCode!=8 && event.keyCode!=46 && event.keyCode!=35 && event.keyCode!=36 && event.keyCode!=37 && event.keyCode!=38 && event.keyCode!=39 && event.keyCode!=40)     
//   {    
//  
//        str=txtvalue.charAt(count);
//        
//        if(str<=9 && str>=0)
//                {
//                document.getElementById(txtid).value=txtvalue;
//                }
//                else
//                {
//               document.getElementById(txtid).value= txtvalue.substring(0,count);   
//                }      
//        
//    }
//  }
//  
//  function isBlank(s) 
//{ 
// var len = s.length
// var i
// for (i=0;i<len;i++) 
// {
//  if(s.charAt(i)!=" ") 
//  return false
// }
// return true
//}    

//function valid(str, type)
//{    
//	var RE;
//	
//	switch (type)
//	{
//		case "Email" :
//		RE = /^[a-z][\._a-z0-9-]+@[\.a-z0-9-]+[\.]{1}[a-z]{2,4}$/;
//		if (RE.exec(str.value)){
//			return true;	
//		}else{
//			return false;	
//		}
//		case "Name" : 
//		RE = /^[a-zA-Z. ]{4,20}$/;
//		if (RE.exec(str.value)){
//			return true;	
//		}else{
//			return false;	
//		}
//		case "Phone" :
//		RE = /^[0-9]{8,15}$/;
//		if (RE.exec(str.value)){
//			return true;	
//		}else{
//			return false;	
//		}
//	}
//		
//}

function strvalid(str, type)
{
    var RE;
    var id=document.getElementById(str).value;
//    if(isBlank(document.getElementById(str).value)) 
//	{
//	alert("Enter Value")
//	}
//	else
//	{
    switch (type)
	{
    case "String":
		RE=/^[a-zA-Z. ]{0,20}$/;
		if (RE.exec(id)){
		}else{
		    alert("Enter Text Only");
		    document.getElementById(str).focus();
		}
		break;
		
		case "Number" :
		RE = /^[0-9-,]{0,31}$/;
		if (RE.exec(id)){
		}else{
			alert("Enter Number Only");
			document.getElementById(str).focus();
		}
		break;
		
		default :
			return false;
	}
//}
}

function keyup(event,txtid)
{
    var txtvalue= document.getElementById(txtid).value;
    var txtlen=txtvalue.length; 
    var count=txtlen-1;
    var str=txtvalue.charAt(count);

    if(event.keyCode!=8 && event.keyCode!=46 && event.keyCode!=35 && event.keyCode!=36 && event.keyCode!=37 && event.keyCode!=38 && event.keyCode!=39 && event.keyCode!=40)     
    {    
        if((str<='z' && str>='a') || (str<='Z' && str>='a'))
        {
            document.getElementById(txtid).value=txtvalue;
        }
        else
        {
            document.getElementById(txtid).value= txtvalue.substring(0,count);
        }
    }
}

//company, branch, Institution
//number, , ,-, 
function AllowOnlyNumeric(event)
{
    var key = window.event.keyCode;

    if ( (key > 47 && key < 58) || key==44 || key==45 )//|| key == 46 
        return;
    else
        window.event.returnValue = null; 
}

//Leave, Institutite profile, Appraisal
//number, .
function AllowOnlyNumeric1(event)
{
    try
    {
        var key1 = event || window.event;
        key = key1.keyCode || key1.charCode;
    
        if ( (key > 45 && key < 58) || key==8 || key == 190 || key == 9 || (key > 95 && key < 111) || (key > 36 && key < 41))//|| key == 46 
        {
            return; 
        }
        else
        {
            key1.preventDefault();
            key1.stopPropagation();
            key1.returnValue = null; 
        } 
    }
    catch(err)
    {
        window.event.returnValue = null; 
    }
}

function AllowOnlyNumeric2(event)
{
    try
    {
        var key1 = event || window.event;
        key = key1.keyCode || key1.charCode;
    
        if ( (key > 45 && key < 58) || key==8 || key == 190 || key == 9 || (key > 95 && key < 111) || (key > 36 && key < 41))//|| key == 46 
        {
            return; 
        }
        else
        {
            key1.preventDefault();
            key1.stopPropagation();
            key1.returnValue = null; 
        } 
    }
    catch(err)
    {
        window.event.returnValue = null; 
    }
}

//company, branch, Programname, Programtype
function AllowOnlyText()
{
    var key = window.event.keyCode;

    if ( (key > 96 && key < 123) || (key > 64 && key < 91) || key == 9 || key == 32 || key == 8 || (key > 36 && key < 41)) //|| key == 46
        return;
    else
        window.event.returnValue = null; 
}

//Appraisal, Trainer profile, Bank, Loan, Holiday
//text, space

function AllowOnlyText1(event)
{
    try
    {
    var key1 = event || window.event;
        key = key1.keyCode || key1.charCode;

    if ( (key > 96 && key < 123) || (key > 64 && key < 91) || key == 9 || key == 32 || key == 8 || (key > 36 && key < 41)) //|| key == 46
        return;
    else
        {
            key1.preventDefault();
            key1.stopPropagation();
            key1.returnValue = null; 
        }
        
    }
    catch(err)
    {
        window.event.returnValue = null; 
    }
}

//function AllowOnlyText1()
//{
//    var key = window.event.keyCode;

//    if ( (key > 96 && key < 123) || (key > 64 && key < 91) || key == 32) //|| key == 46
//        return;
//    else
//        window.event.returnValue = null; 
//}

//number,text,-
function AllowOnlyText2()
{
    var key = window.event.keyCode;

    if ( (key > 47 && key < 58) || (key > 96 && key < 123) || (key > 64 && key < 91) || key == 45 ) 
        return;
    else
        window.event.returnValue = null; 
}

//text, space, .
function AllowOnlyText3()
{
    var key = window.event.keyCode;

    if ( (key > 96 && key < 123) || (key > 64 && key < 91) || key == 32 || key == 46) //|| key == 46
        return;
    else
        window.event.returnValue = null; 
}

//number,text,-, /
function AllowOnlyText4()
{
    var key = window.event.keyCode;

    if ( (key > 47 && key < 58) || (key > 96 && key < 123) || (key > 64 && key < 91) || key == 45 || key == 47 ) 
        return;
    else
        window.event.returnValue = null; 
}

//number,text,.,-,space, comma
function AllowOnlyText5()
{
    var key = window.event.keyCode;

    if ( (key > 47 && key < 58) || (key > 96 && key < 123) || (key > 64 && key < 91) || key == 46 || key == 45 || key == 32 || key == 44) 
        return;
    else
        window.event.returnValue = null; 
}

//course, skills, Institution
//number,text,.,-,space,
function AllowOnlyTND()
{
    var key = window.event.keyCode;

    if ( (key > 47 && key < 58) || (key > 96 && key < 123) || (key > 64 && key < 91) || key == 46 || key == 45 || key == 32) 
        return;
    else
        window.event.returnValue = null; 
}
//Level, Designation, Department, Division, shift, projectsit, jobstatus, grade, category
//number,text,.,-,space,&,(,)
function AllowOnlyTNS()
{
    var key = window.event.keyCode;

    if ( (key > 47 && key < 58) || (key > 96 && key < 123) || (key > 64 && key < 91) || key == 46 || key == 45 || key == 32 || key == 38 || key == 40 || key == 41) 
        return;
    else
        window.event.returnValue = null; 
}

//Institution
function AllowforEmail(id)
{
var str=document.getElementById(id).value;
var RE = /^[a-z][\._a-z0-9-]+@[\.a-z0-9-]+[\.]{1}[a-z]{2,4}$/;
if(str!="")
{
    	if (RE.exec(str)){
			//return true;	
		}else{
			//return false;	
			alert("Enter valid Email Address");
			document.getElementById(id).focus();
		}
}
}