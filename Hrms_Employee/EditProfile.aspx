<%@ Page Title="" Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="EditProfile.aspx.cs" Inherits="Hrms_Employee_EditProfile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="../JQuery/Editscript.js" type="text/javascript"></script>
    

<script type="text/javascript">
    function onlyAlphabets(e, t) {
        try {
            if (window.event) {
                var charCode = window.event.keyCode;
            }
            else if (e) {
                var charCode = e.which;
            }
            else { return true; }
            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123))
                return true;
            else
                return false;
        }
        catch (err) {
            alert(err.Description);
        }
            }

    function isNumber(evt) {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
   


    function fn_date(event, txtid) {
        var len;
        var txtvalue;
        var bool_obj;
        var i;
        txtvalue = document.getElementById(txtid).value;
        txtlen = txtvalue.length;

        if (event.keyCode != 8 && event.keyCode != 46 && event.keyCode != 35 && event.keyCode != 36 && event.keyCode != 37 && event.keyCode != 38 && event.keyCode != 39 && event.keyCode != 40) {

            if (txtlen != 0) {
                bool_obj = fn_validate(txtlen, txtvalue);

                if (bool_obj == true) {
                    if (txtlen == 2 || txtlen == 5) {
                        document.getElementById(txtid).value = txtvalue + "/";
                    }
                    else {
                        document.getElementById(txtid).value = txtvalue;
                    }

                }
                else 
                {
                    document.getElementById(txtid).value = txtvalue.substring(0, txtlen - 1);
                }
            }
        }
    }

    function fn_validate(len, tval) {
        var str;

        switch (len) {

            case 1: if (tval <= 3) {
                    return true;
                }
                else {
                    return false;
                }
                break;


            case 2:

                if (tval <= 31 && tval > 0) {
                    return true;
                }
                else {
                    return false;
                }
                break;


            case 3: str = tval.charAt(2);

                if (str == "/") {
                    return true;
                }
                else {
                    return false;
                }
                break;

            case 4: str = tval.charAt(3);

                if (str <= 1) {
                    return true;
                }
                else {
                    return false;
                }
                break;

            case 5: str = tval.substring(3, 5);

                if (str <= 12 && str > 0) {
                    return true;
                }
                else {
                    return false;
                }
                break;

            case 6: str = tval.charAt(5);

                if (str == "/") {
                    return true;
                }
                else {
                    return false;
                }
                break;

            case 7: str = tval.charAt(6);

                if (str <= 9 && str > 0) {
                    return true;
                }
                else {
                    return false;
                }
                break;

            case 8: str = tval.substring(6, 8);

                if (str >= 18) {
                    return true;
                }
                else {
                    return false;
                }
                break;

            case 9: str = tval.charAt(8);

                if (str <= 9) {
                    return true;
                }
                else {
                    return false;
                }
                break;

            case 10: str = tval.charAt(9);

                if (str <= 9) {
                    return true;
                }
                else {
                    return false;
                }
                break;


            default: return false;
                break;
        }
    }

    function isBlank(s) {
        var len = s.length
        var i
        for (i = 0; i < len; i++) {
            if (s.charAt(i) != " ")
                return false
        }
        return true
    }

    function valid(str, type) {
        var RE;
        switch (type) {
            case "Email":
                RE = /^[a-z][\._a-z0-9-]+@[\.a-z0-9-]+[\.]{1}[a-z]{2,4}$/;
                if (RE.exec(str.value)) {
                    return true;
                } else {
                    return false;
                }
            case "Name":
                RE = /^[a-zA-Z. ]{4,20}$/;
                if (RE.exec(str.value)) {
                    return true;
                } else {
                    return false;
                }

            case "Phone":
                RE = /^[0-9]{8,15}$/;
                if (RE.exec(str.value)) {
                    return true;
                } else {
                    return false;
                }

            default:
                return false;
        }
    }  
    
</script>

<script type="text/javascript">

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
	   
	   if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_Readerid.value))
	   {	
	   key+=" Enter Reader ID  \n";
}

if (isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_basicsal.value)) {
    key += " Enter Basic Salary  \n";
}


	   
	if(key!="")
	{
		 	alert(msg+key+"\n ******** Unable to Create!! ******** \n"); 
		 	
            return false;
	}
	else
	{
	
	return true;
		
	}	
}
function txtfullname_onclick() {

}

</script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <body >
 <h3 class="page-header"> Update Profile</h3><br />

                <div class="tab-pane fade in active" id="register">               

                <div> <asp:Label ID="lbl_Error" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label></div>
                                          

                 <table id="Table2" runat="server" width="100%" cellpadding="0" cellspacing="0" class="table table-striped table-bordered table-hover">
                    <tr>
                         <td ><b>Employee Code:</b></td>
                             
                            <td > <input runat="server" class="form-control" id="txtEmployeeCode" readonly /> </td>
                            
                         
                   
                       <%--  <td>Designation <span style="color: Red;">*</span></td>
                                                <td>
                                                    <asp:DropDownList ID="Rolelode" runat="server" class="form-control" >
                                                          <asp:ListItem Value="0">-----Select-----</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>--%>
                        <td > EDLI Eligible</td>
                        <td>
                            <asp:RadioButtonList ID="rdo_tds" runat="server" RepeatDirection="Horizontal" 
                                RepeatLayout="Flow"   Width="100%"
                                
                                
                                style="color: #808080; font-size: 12px; font-weight: 700; font-family: Tahoma" 
                                TabIndex="6" >
                                <asp:ListItem Value="N" Selected="True">N</asp:ListItem>
                                <asp:ListItem Value="Y">Y</asp:ListItem>
                            </asp:RadioButtonList></td>
                             <td >OT Eligible</td>
                        <td >
                            <asp:RadioButtonList ID="rdo_btn" runat="server" RepeatDirection="Horizontal" 
                                RepeatLayout="Flow" AutoPostBack="True"  Width="100%"
                                OnSelectedIndexChanged="rdo_btn_SelectedIndexChanged" 
                                
                                style="color: #808080; font-size: 12px; font-weight: 700; font-family: Tahoma" 
                                TabIndex="10">
                                <asp:ListItem Value="N" Selected="True">N</asp:ListItem>
                                <asp:ListItem Value="Y">Y</asp:ListItem>
                            </asp:RadioButtonList>
                            <table><tr><td >
                                <asp:Label ID="lbl_text" runat="server" Text="OT Calc" Visible="False" 
                                    ></asp:Label>
                        </td>
                        <td >
                            <asp:DropDownList ID="ddl_ot" runat="server" Visible="false">
                                <asp:ListItem Value="0">0</asp:ListItem>
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">1.5</asp:ListItem>
                                <asp:ListItem Value="3">2</asp:ListItem>
                                <asp:ListItem Value="4">2.5</asp:ListItem>
                                <asp:ListItem Value="5">3</asp:ListItem>
                            </asp:DropDownList>
                        </td></tr></table>
                        </td>
                    </tr>
                    <tr runat="server" id="row_pwd">
                        <td >Full Name<span class="style40"> <span style="color: #FF0000">*</span></span></td>
                        <td >
                            <input class="form-control" runat="server" id="txtfullname" 
                                 maxlength="70"   /></td>
                                 <td >
                           First Name</td>
                        <td >
                            <input class="form-control" runat="server" id="txtFirstName" 
                                onkeydown="AllowOnlyText1(event);" maxlength="50" /></td>
                        <td >
                           Middle Name</td>
                        <td >
                            <input class="form-control" runat="server" id="txtMiddleName" 
                                onkeydown="AllowOnlyText1(event);" maxlength="50"   /></td>                     
                       </tr>
                    <tr>
                     <td >Last Name</td>
                        <td >
                            <input class="form-control" runat="server" id="txtLastName" 
                               onkeydown="AllowOnlyText1(event);" maxlength="50"   /></td>
                        <td >PF No</td>
                        <td >
                            <input class="form-control" runat="server" id="txt_pfno" 
                                onkeypress="AllowOnlyText4();" tabindex="7"  /></td>
                                 <td>ESI No</td>
                        <td >
                            <input class="form-control" runat="server" id="txt_esino" 
                                onkeypress="AllowOnlyText4();" tabindex="8"  /></td>
                    </tr>
                    <tr runat="server" id="row_cpwd">
                       
                       <td >PAN No</td>
                        <td >
                            <input class="form-control" runat="server" id="txt_panno" 
                                onkeypress="AllowOnlyText4();" tabindex="9"  /></td>
                        <td >Reader ID <span style="color:Red;" >*</span></td>
                        <td >
                            <input class="form-control" runat="server" id="txt_Readerid" 
                                onkeydown="AllowOnlyNumeric1(event);" tabindex="11" /></td>
                                <td  nowrap="nowrap">Reporting person</td>
                        <td >
                            <asp:DropDownList ID="ddl_rep" runat="server" CssClass="form-control" 
                                ForeColor="#666666" Font-Size="Small">
                                
                            </asp:DropDownList>
                            </td>
                    </tr>
                    <tr>
                        
                        
                        <td > Basic Salary <span style="color:Red;" >*</span></td>
                        <td >
                            <input id="txt_basicsal" runat="server" class="form-control" type="text" 
                                tabindex="12" onkeypress="return isNumber(event)" /></td>
                                <td >Salutation</td>
                        <td >
                            <asp:RadioButtonList ID="rdo_salutation" runat="server" 
                                RepeatDirection="Horizontal" RepeatLayout="Flow"  Width="100%">
                                <asp:ListItem Selected="True" Value="1" Text="Mr."></asp:ListItem>
                                <asp:ListItem Value="2" Text="Ms."></asp:ListItem>
                                <asp:ListItem Value="3" Text="Mrs."></asp:ListItem>
                            </asp:RadioButtonList>
                                                       </td>
                       
                        <td >Reporting Email</td>
                        <td >
                            <input id="txt_rep" class="form-control" runat="server" 
                                type="text" tabindex="4"   /></td>
                    </tr>                   

                    <tr>
                        <td >Date of Birth</td>
                        <td >
                          <div style=" width:150px; float:left;">
                            <asp:TextBox CssClass="form-control"  id="txt_dob" runat="server" 
                                onkeyup="fn_date(event,this.id);"  Width="150px"></asp:TextBox>

                               

                                <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txt_dob" 
                                                                    runat="server" Format="dd/MM/yyyy" />
</div>
                        <div style=" width:25px; float:left;  margin-left:10px; margin-top:3px;">                                                
                                                    <asp:Image ID="Image1" runat="server"  
                                                        Text="" Width="25px" 
                                                        ImageUrl="~/Images/calendaricon.png" />
                                                </div>
                               </td>
                        <td >Gender</td>
                        <td > 
                            <asp:DropDownList ID="ddl_gender" runat="server" CssClass="form-control" 
                                ForeColor="#666666" Font-Size="Small" 
                                >
                                <asp:ListItem Value="Male" Text="Male"></asp:ListItem>
                                <asp:ListItem Value="Female" Text="Female"></asp:ListItem>
                                <asp:ListItem Value="Others" Text="Others"></asp:ListItem>
                            </asp:DropDownList>
                            </td>
                        <td >Blood Group</td>
                        <td >
                            <asp:DropDownList ID="ddl_blood" 
                                runat="server" CssClass="form-control" ForeColor="#666666" 
                                Font-Size="Small">
                                <asp:ListItem Text="None" Value="None"></asp:ListItem>
                                <asp:ListItem Text="A+" Value="A+"></asp:ListItem>
                                <asp:ListItem Text="A-" Value="A-"></asp:ListItem>
                                <asp:ListItem Text="B+" Value="B+"></asp:ListItem>
                                <asp:ListItem Text="B-" Value="B-"></asp:ListItem>
                                <asp:ListItem Text="AB+" Value="AB+"></asp:ListItem>
                                <asp:ListItem Text="AB-" Value="AB-"></asp:ListItem>
                                <asp:ListItem Text="O+" Value="O+"></asp:ListItem>
                                <asp:ListItem Text="O-" Value="O-"></asp:ListItem>
                                <asp:ListItem Text="A1+" Value="A1+"></asp:ListItem>
                                <asp:ListItem Text="A1-" Value="A1-"></asp:ListItem>
                                <asp:ListItem Text="A2+" Value="A2+"></asp:ListItem>
                                <asp:ListItem Text="A2-" Value="A2-"></asp:ListItem>
                                <asp:ListItem Text="A1B+" Value="A1B+"></asp:ListItem>
                                <asp:ListItem Text="A1B-" Value="A1B-"></asp:ListItem>
                                <asp:ListItem Text="A2B+" Value="A2B+"></asp:ListItem>
                                <asp:ListItem Text="A2B-" Value="A2B-"></asp:ListItem>
                                <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
                            </asp:DropDownList>
                            </td>
                    </tr>
                    <tr>
                        <td >Religion</td>
                        <td >
                            <input class="form-control" runat="server" id="txtReligion" 
                              maxlength="50"   /></td>
                        <td >Nationality</td>
                        <td >
                            <input class="form-control" runat="server" id="txtNationality" 
                                maxlength="50" /></td>
                                <td >Marital Status</td>
                        <td >
                          <asp:DropDownList ID="ddl_marital" runat="server" CssClass="form-control" 
                                ForeColor="#666666" Font-Size="Small" 
                                >
                                <asp:ListItem Value="1" Text="Single"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Married"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                       
                    </tr>
                   
                    <tr>
                        
                        <td >
                            ID Proof</td>
                        <td >
                            <asp:DropDownList ID="ddl_idtype" runat="server" CssClass="form-control" 
                                Font-Size="Small" ForeColor="#666666" onchange="fn_Visible()">
                                <asp:ListItem Text="Select" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Driving License" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Passport" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Pan Card" Value="4"></asp:ListItem>
                                <asp:ListItem Text="Voter ID Card" Value="5"></asp:ListItem>
                                <asp:ListItem Text="Govt. Issued ID Card" Value="6"></asp:ListItem>
                                <asp:ListItem Text="Bank Passbook" Value="7"></asp:ListItem>
                                <asp:ListItem Text="Credit Card" Value="8"></asp:ListItem>
                                <asp:ListItem Text="Aadhar Card" Value="9"></asp:ListItem>
                                <asp:ListItem Text="Others" Value="10"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        
                             <td nowrap="nowrap">Salary Type</td>
                        <td >
                            <asp:RadioButtonList ID="rdo_saltype" runat="server" Font-Names="Calibri" 
                                Font-Size="Small" ForeColor="#666666" RepeatDirection="Horizontal"  
                                Width="100%" TabIndex="5" 
                                >
                                <asp:ListItem Selected="True">Month</asp:ListItem>
                                <asp:ListItem>Day</asp:ListItem>
                            </asp:RadioButtonList>
                            
                            </td>
                            <td>Email ID</td>
                            <td>
                                <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
                          <%--<asp:DropDownList ID="ddl_idtype" runat="server" CssClass="form-control" onchange="fn_Visible()"
                                ForeColor="#666666" Font-Size="Small">
                                <asp:ListItem Value="Select" Text="Select"></asp:ListItem>
                                <asp:ListItem Value="Driving License" Text="Driving License"></asp:ListItem>
                                <asp:ListItem Value="Passport" Text="Passport"></asp:ListItem>
                                <asp:ListItem Value="Pan Card" Text="Pan Card"></asp:ListItem>
                                <asp:ListItem Value="Voter ID Card" Text="Voter ID Card"></asp:ListItem>
                                <asp:ListItem Value="Govt. Issued ID Card" Text="Govt. Issued ID Card"></asp:ListItem>
                                <asp:ListItem Value="Bank Passbook" Text="Bank Passbook"></asp:ListItem>
                                <asp:ListItem Value="Credit Card" Text="Credit Card"></asp:ListItem>
                                <asp:ListItem Value="Aadhar Card" Text="Aadhar Card"></asp:ListItem>
                                <asp:ListItem Value="Others" Text="Others"></asp:ListItem>
                            </asp:DropDownList>--%>
                            </td>
                    </tr>
                   
                    <tr>
                        
                        <td >
                            ID No</td>
                        <td >
                            <input class="form-control" runat="server" id="txtIDNo" 
                              maxlength="50"   /></td>
                        
                             <td nowrap="nowrap" colspan="2">
                        <asp:CheckBox ID="chk_rep" runat="server" />
&nbsp;Reporting Person</td>
                            <td>Status</td>
                            <td>
                          <asp:DropDownList ID="ddl_status" runat="server" CssClass="form-control" 
                                ForeColor="#666666" Font-Size="Small" 
                                >
                                <asp:ListItem>Y</asp:ListItem>
                                <asp:ListItem>W</asp:ListItem>
                                <asp:ListItem>N</asp:ListItem>
                            </asp:DropDownList>
                            </td>
                    </tr>
                    <tr>
                    <td colspan="2">                   
                       <asp:Button ID="btn_Back" runat="server" OnClick="btn_Back_Click" Text="Back" class="btn btn-info"  />
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" class="btn btn-success" 
                            onclick="btnUpdate_Click" OnClientClick="return check_update();"/></td>                          
                   
                    <td colspan="2">
                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                          &nbsp;</td>
                    
                    </tr>
                </table>
                 
                <br  />
                </div></body>
    

    <script src="Employee.js"></script>
</asp:Content>

