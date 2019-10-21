<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Search Criteria</title>
    <link href="../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">

  function Error()
  {
  alert("No Query Selected")
  } 
  
  function txt_visiblity()
 {
 
  var txtmin=document.getElementById("txtminvalue");
  var txtmax=document.getElementById("txtmaxvalue");
  var strrdo; 

       var con_index  = document.getElementById("ddl_condition").selectedIndex;
       var con_text = document.getElementById("ddl_condition").options[con_index].text;
       var con_value = document.getElementById("ddl_condition").options[con_index].value;
 
       if(con_text=='select')
        {
        alert("select value");
        }
        else
        {
         strrdo =con_text;
                   
        } 
  if(strrdo=='between')
   {   
   txtmin.style.visibility="visible";                 
   txtmax.style.visibility="visible";   
   }
   else
   {
    txtmin.style.visibility="visible"; 
    txtmax.style.visibility="hidden";   
        
   }  
 } 
  
function rowadd()
{

var i;
var strtemp="";
  var txtmin=document.getElementById("txtminvalue");
  var txtmax=document.getElementById("txtmaxvalue");

       var con_index  = document.getElementById("ddl_condition").selectedIndex;
       var con_text = document.getElementById("ddl_condition").options[con_index].text;
       var con_value = document.getElementById("ddl_condition").options[con_index].value;
  if(con_text=='between')
   {   
   
   if(document.getElementById("txtminvalue").value=="" || document.getElementById("txtmaxvalue").value=="")
       {
       alert("Enter Value");
       }
       else
       {      
        strtemp="selected";
       }  
   }
   else
   {
   
    if(document.getElementById("txtminvalue").value=="")
       {
       alert("Enter Value");
       }
       else
       {      
        strtemp="selected";
       } 
        
   } 
    
    if(strtemp!="")
    {
    
   var myRow = tab_query.insertRow();  
    
   var myCell1 = myRow.insertCell(0);
   var myCell2 = myRow.insertCell(1);
   var myCell3 = myRow.insertCell(2);
   var myCell4 = myRow.insertCell(3);
   var myCell5 = myRow.insertCell(4);
   var myCell6 = myRow.insertCell(5);
   var myCell7 = myRow.insertCell(6);
     myCell3.innerText=document.getElementById("txtminvalue").value;
     
     myCell4.innerText=document.getElementById("txtmaxvalue").value;
     
    
       var ddl_name_index  = document.getElementById("ddl_name").selectedIndex;
       var ddl_name_text = document.getElementById("ddl_name").options[ddl_name_index].text;
       var ddl_name_value = document.getElementById("ddl_name").options[ddl_name_index].value;
  
       var ddl_condition_index  = document.getElementById("ddl_condition").selectedIndex;
       var ddl_condition_text = document.getElementById("ddl_condition").options[ddl_condition_index].text;
       var ddl_condition_value = document.getElementById("ddl_condition").options[ddl_condition_index].value;
       if(ddl_name_text=='select')
        {
        alert("select value");
        }
        else
        {
         myCell1.innerText =ddl_name_text; 
         myCell7.innerText =ddl_name_value;  
                  
         myCell7.style.visibility="hidden";  
                 
        }          
   
 
       if(ddl_condition_text=='select')
        {
        alert("select value");
        }
        else
        {
         myCell2.innerText =ddl_condition_text;
                   
        }     
 
                 
       myCell5.innerText =document.getElementById("ddl_and_or").value                 
       
       if(myCell5.innerText =="select")
       {
       myCell5.innerText="and";     
       }   
      
       myCell6.innerHTML = "<input id='Checkbox1' type='checkbox' />"; 
      
   
       clear_all();
    
    }
}
 
 function rowDelete()
 {
 
 var i;
 var l_row=tab_query.rows.length;
 
 for(i=1;i<l_row;i++) 
    { 
        if(tab_query.rows(i).cells(5).firstChild.checked==true)
        {
        tab_query.deleteRow(i);
        }
 
    }
 
 }
 
 function Query_general()
 {
 
 var query_str="";
 var j;  
 var iden=0;
 
 var row_len=tab_query.rows.length;
 document.getElementById("hdn_Others").value="";
 for(j=1;j<row_len;j++) 
   {            
       
       if(j != row_len-1)
       {  
       
       if(tab_query.rows(j).cells(6).innerText==6 || tab_query.rows(j).cells(6).innerText==22 || tab_query.rows(j).cells(6).innerText==23 || tab_query.rows(j).cells(6).innerText==24 || tab_query.rows(j).cells(6).innerText==25 || tab_query.rows(j).cells(6).innerText==26 || tab_query.rows(j).cells(6).innerText==27 || tab_query.rows(j).cells(6).innerText==28)
        {
           if(tab_query.rows(j).cells(1).innerText == 'between')
             { 
             query_str=query_str+tab_query.rows(j).cells(0).innerText+" "+tab_query.rows(j).cells(1).innerText+" '"+convert_Tosqldate(tab_query.rows(j).cells(2).innerText)+"' and '"+convert_Tosqldate(tab_query.rows(j).cells(3).innerText)+"' "+tab_query.rows(j).cells(4).innerText+" ";          
                                     
             }
             else
             {                  
             query_str=query_str+tab_query.rows(j).cells(0).innerText+" "+tab_query.rows(j).cells(1).innerText+" '"+convert_Tosqldate(tab_query.rows(j).cells(2).innerText)+"' "+tab_query.rows(j).cells(4).innerText+" ";          
               
             } 
       
       }
       else
       {      
       
         if(tab_query.rows(j).cells(1).innerText == 'between')
             { 
             query_str=query_str+tab_query.rows(j).cells(0).innerText+" "+tab_query.rows(j).cells(1).innerText+" '"+tab_query.rows(j).cells(2).innerText+"' and '"+tab_query.rows(j).cells(3).innerText+"' "+tab_query.rows(j).cells(4).innerText+" ";          
                                     
             }
             else if(tab_query.rows(j).cells(1).innerText == 'like')
             {                  
             query_str=query_str+tab_query.rows(j).cells(0).innerText+" "+tab_query.rows(j).cells(1).innerText+" '"+tab_query.rows(j).cells(2).innerText+"%' "+tab_query.rows(j).cells(4).innerText+" ";          
               
             } 
             else
             {                  
             query_str=query_str+tab_query.rows(j).cells(0).innerText+" "+tab_query.rows(j).cells(1).innerText+" '"+tab_query.rows(j).cells(2).innerText+"' "+tab_query.rows(j).cells(4).innerText+" ";          
               
             }          
         }
       }
       else
       {
       
       if(tab_query.rows(j).cells(6).innerText==6 || tab_query.rows(j).cells(6).innerText==22 || tab_query.rows(j).cells(6).innerText==23 || tab_query.rows(j).cells(6).innerText==24 || tab_query.rows(j).cells(6).innerText==25 || tab_query.rows(j).cells(6).innerText==26 || tab_query.rows(j).cells(6).innerText==27 || tab_query.rows(j).cells(6).innerText==28)
        {
       
       
           if(tab_query.rows(j).cells(1).innerText == 'between')
             { 
             query_str=query_str+tab_query.rows(j).cells(0).innerText+" "+tab_query.rows(j).cells(1).innerText+" '"+convert_Tosqldate(tab_query.rows(j).cells(2).innerText)+"' and '"+convert_Tosqldate(tab_query.rows(j).cells(3).innerText)+"' ";          
                                      
             }
             else
             {                  
             query_str=query_str+tab_query.rows(j).cells(0).innerText+" "+tab_query.rows(j).cells(1).innerText+" '"+convert_Tosqldate(tab_query.rows(j).cells(2).innerText)+"' ";          
               
             }  
       }
       else
       {
       
          if(tab_query.rows(j).cells(1).innerText == 'between')
             { 
             query_str=query_str+tab_query.rows(j).cells(0).innerText+" "+tab_query.rows(j).cells(1).innerText+" '"+tab_query.rows(j).cells(2).innerText+"' and '"+tab_query.rows(j).cells(3).innerText+"' ";          
                                      
             }
              else if(tab_query.rows(j).cells(1).innerText == 'like')
             {                  
             query_str=query_str+tab_query.rows(j).cells(0).innerText+" "+tab_query.rows(j).cells(1).innerText+" '"+tab_query.rows(j).cells(2).innerText+"%' ";          
               
             }  
             else
             {                  
             query_str=query_str+tab_query.rows(j).cells(0).innerText+" "+tab_query.rows(j).cells(1).innerText+" '"+tab_query.rows(j).cells(2).innerText+"' ";          
               
             } 
      } 
       }
   } 
   
   document.getElementById("hdn_Others").value=query_str;
 }
 
 function convert_Tosqldate(arg_date)
 { 
  var ret_date="";
  var s_day="";  
  var s_month="";
  var s_year="";
 
  s_day=arg_date.substring(0,2);
  s_month=arg_date.substring(3,5);
  s_year=arg_date.substring(6,10);
  
  ret_date=s_year+"/"+s_month+"/"+s_day;
  
  return ret_date;
  
  //12/12/2008
  //0123456789
 
 }
function query_output()
 {
 
 Query_general();
 
 return true; 
 
 }
 function clear_all()
 {   
 
   document.getElementById("txtminvalue").value="";
   document.getElementById("txtmaxvalue").value="";
   document.getElementById("txtminvalue").style.visibility="hidden";
   document.getElementById("txtmaxvalue").style.visibility="hidden";
   document.getElementById("ddl_condition").selectedIndex=0; 
 } 
 
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" border="1"> 
            <tr>
                <td>
                    <table width="100%" height="75px">
                        <tr valign="top">
                            <td>
                            </td>
                            <td width="100%" background="../Images/Checking.jpg">
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr align="center">
                            <td style="width: 100%; text-align: center;" align="center">
                                <table style="width: 100%; height: 100%">
                                    <tr>
                                        <td class="SrchTitle" colspan="5" style="width: 20%;">
                                            Search Criteria</td>
                                    </tr>
                                    <tr>
                                        <td colspan="5" style="width: 20%" align="left">
                                            &nbsp;
                                            <asp:Label ID="lblmsg" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>&nbsp;
                                            <asp:Label ID="lbl_error" runat="server" ForeColor="red" Text=""></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="SrchTitle_grid" style="width: 20%">
                                            Branch</td>
                                        <td class="SrchTitle_grid" style="width: 20%">
                                            Department</td>
                                        <td class="SrchTitle_grid" style="width: 20%">
                                            Designation</td>
                                        <td class="SrchTitle_grid" style="width: 20%">
                                            Project Site</td>
                                        <td class="SrchTitle_grid" style="width: 20%">
                                            Grade</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">
                                            <div class="qrychkbox" style="left: 1px; top: 0px">
                                                <asp:CheckBoxList ID="Chk_Branch" runat="server" CssClass="InputDefaultStyle1" Width="90%">
                                                </asp:CheckBoxList>
                                            </div>
                                        </td>
                                        <td style="width: 20%">
                                            <div class="qrychkbox">
                                                <asp:CheckBoxList ID="Chk_Department" runat="server" CssClass="InputDefaultStyle1"
                                                    Width="90%">
                                                </asp:CheckBoxList>
                                            </div>
                                        </td>
                                        <td style="width: 20%">
                                            <div class="qrychkbox">
                                                <asp:CheckBoxList ID="Chk_Designation" runat="server" CssClass="InputDefaultStyle1"
                                                    Width="90%">
                                                </asp:CheckBoxList>
                                            </div>
                                        </td>
                                        <td style="width: 20%">
                                         <div class="qrychkbox">
                                                <asp:CheckBoxList ID="chk_projectsite" runat="server" CssClass="InputDefaultStyle1"
                                                    Width="90%">
                                                </asp:CheckBoxList>
                                            </div>
                                        </td>
                                        <td style="width: 20%">
                                            <table style="width: 100%; height: 100%">
                                                <tr>
                                                    <td style="width: 20%">
                                                        <div class="qrychkbox">
                                                            <asp:CheckBoxList ID="chk_Grade" runat="server" CssClass="InputDefaultStyle1" Width="90%">
                                                            </asp:CheckBoxList></div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="SrchTitle_grid" style="width: 20%">
                                            Category</td>
                                        <td class="SrchTitle_grid" style="width: 20%">
                                            Division</td>
                                        <td class="SrchTitle_grid" style="width: 20%">
                                            Level</td>
                                        <td class="SrchTitle_grid" style="width: 20%">
                                            Shift</td>
                                        <td class="SrchTitle_grid" style="width: 20%">
                                            Job Status</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">
                                            <div class="qrychkbox" style="left: 0px; top: 0px">
                                                <asp:CheckBoxList ID="chk_Category" runat="server" CssClass="InputDefaultStyle1"
                                                    Width="90%">
                                                </asp:CheckBoxList></div>
                                        </td>
                                        <td style="width: 20%">
                                            <table style="width: 100%; height: 100%">
                                                <tr>
                                                    <td style="width: 20%">
                                                        <div class="qrychkbox" style="left: 1px; top: 0px">
                                                            <asp:CheckBoxList ID="chk_division" runat="server" CssClass="InputDefaultStyle1"
                                                                Width="90%">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 20%">
                                            <table style="width: 100%; height: 100%">
                                                <tr>
                                                    <td style="width: 20%">
                                                        <div class="qrychkbox">
                                                            <asp:CheckBoxList ID="chk_Level" runat="server" CssClass="InputDefaultStyle1" Width="90%">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 20%">
                                            <table style="width: 100%; height: 100%">
                                                <tr>
                                                    <td style="width: 20%">
                                                        <div class="qrychkbox" style="left: 1px; top: 0px">
                                                            <asp:CheckBoxList ID="chk_Shift" runat="server" CssClass="InputDefaultStyle1" Width="90%">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 20%">
                                            <table style="width: 100%; height: 100%">
                                                <tr>
                                                    <td style="width: 19%">
                                                        <div class="qrychkbox">
                                                            <asp:CheckBoxList ID="chk_Jobstatus" runat="server" CssClass="InputDefaultStyle1"
                                                                Width="90%">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="SrchTitle" colspan="5" style="width: 20%">
                                            Selection Criteria</td>
                                    </tr>
                                    <tr>
                                        <td colspan="5" style="width: 20%">
                                            <table style="width: 100%; height: 100%; text-align: center" border="1">
                                                <tr>
                                                    <td class="SrchTitle_grid" style="width: 20%; background-image: url(../Images/bg.jpg);">
                                                        Field List</td>
                                                    <td class="SrchTitle_grid" style="background-image: url(../Images/bg.jpg); width: 15%">
                                                        Condition</td>
                                                    <td class="SrchTitle_grid" style="background-image: url(../Images/bg.jpg); width: 20%">
                                                        Value 1</td>
                                                    <td class="SrchTitle_grid" style="width: 20%; background-image: url(../Images/bg.jpg);" align="center">
                                                        Value 2</td>
                                                    <td class="SrchTitle_grid" style="background-image: url(../Images/bg.jpg); width: 15%;">
                                                        Select</td>
                                                        <td style="width: 10%; background-image: url(../Images/bg.jpg);" align="center" class="SrchTitle_grid">
                                                        Continue</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%" align="center">
                                                        <asp:DropDownList ID="ddl_name" runat="server" BackColor="MenuBar">
                                                        <asp:ListItem Value="select">Select</asp:ListItem>                                                            
                                                               </asp:DropDownList></td>
                                                    <td align="center" style="width: 15%">
                                                        <Select style="width: 91px" id="ddl_condition" onchange="txt_visiblity();">
                                                            <option selected="selected" value="0">Select</option>
                                                            <option value="=">=</option>
                                                            <option value="!=">!=</option>
                                                            <option value="lt">&lt;</option>
                                                            <option value="gt">&gt;</option>
                                                            <option value="lte">&lt;=</option>
                                                            <option value="gte">&gt;=</option>
                                                            <option value="between">between</option>
                                                             <option value="like">like</option>
                                                        </select>
                                                    </td>
                                                    <td align="left" style="width: 20%">
                                                        <asp:TextBox ID="txtminvalue" runat="server"></asp:TextBox></td>
                                                    
                                                    <td style="width: 20%;" align="left">
                                                        <asp:TextBox ID="txtmaxvalue" runat="server"></asp:TextBox>&nbsp;</td>
                                                        <td align="center" style="width: 15%">
                                                        <asp:DropDownList ID="ddl_and_or" runat="server">
                                                            <asp:ListItem Value="select">Select</asp:ListItem>
                                                            <asp:ListItem Value="and">And</asp:ListItem>
                                                            <asp:ListItem Value="or">Or</asp:ListItem>
                                                        </asp:DropDownList></td>
                                                    <td style="width: 10%" align="center">
                                                        <input id="btn_add" onclick="rowadd();" type="button" value="Add" />&nbsp;</td>
                                                </tr>
                                                <%--<tr>
                                                    <td style="width: 20%; background-image: url(../Images/bg.jpg);" class="SrchTitle_grid">
                                                    </td>
                                                    <td class="SrchTitle_grid" style="background-image: url(../Images/bg.jpg); width: 15%;" colspan="2">
                                                        Select</td>
                                                    <td style="width: 35%; background-image: url(../Images/bg.jpg);" align="center" class="SrchTitle_grid">
                                                        Continue</td>
                                                </tr>--%>
                                                <%--<tr>
                                                    <td style="width: 20%" align="center">
                                                        &nbsp;</td>
                                                    <td align="center" style="width: 15%">
                                                        <asp:DropDownList ID="ddl_and_or" runat="server">
                                                            <asp:ListItem Value="select">Select</asp:ListItem>
                                                            <asp:ListItem Value="and">And</asp:ListItem>
                                                            <asp:ListItem Value="or">Or</asp:ListItem>
                                                        </asp:DropDownList></td>
                                                    <td style="width: 35%" align="center">
                                                        <input id="btn_add" onclick="rowadd();" type="button" value="Add Criteria">&nbsp;</td>
                                                </tr>--%>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="SrchTitle" colspan="5" style="width: 20%">
                                            Selected Criteria</td>
                                    </tr>
                                    <tr>
                                        <td colspan="5" style="width: 20%" class="TextStyle">
                                            <table border="0" id="tab_query" runat="server" width="100%">
                                                <tr>
                                                    <td style="width: 3px">
                                                    </td>
                                                    <td style="width: 3px">
                                                    </td>
                                                    <td style="width: 3px">
                                                    </td>
                                                    <td style="width: 48px">
                                                    </td>
                                                    <td>
                                                    </td>
                                                     <td>
                                                    </td>
                                                    <td style="width: 3px">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="SrchTitle" colspan="5" style="width: 20%">
                                            Actions</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">
                                            <asp:ImageButton ImageUrl="../Images/back.jpg" ID="btn_back" runat="server" OnClick="btn_back_Click" /></td>
                                        <td colspan="3" style="width: 20%">
                                            <asp:ImageButton ImageUrl="../Images/Show_Employees.jpg" ID="btn_query" runat="server" OnClick="btn_query_Click" OnClientClick="return query_output();" /></td>
                                        <td style="width: 20%">
                                            <input id="btn_Delete" onclick="rowDelete();" type="button" value="Delete_Row" /></td>
                                    </tr>
                                </table>
                                            <input id="hdn_Others" runat="server" type="hidden" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>








