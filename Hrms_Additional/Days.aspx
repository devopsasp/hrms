<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Days.aspx.cs" Inherits="Hrms_Additional_Default2" Title="ePay-HRMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
function show_message(msg)
    {
        alert(msg);
    }

    </script>

    <script language="javascript" type="text/javascript">
  
function isBlank(s) 
{ 
 var len = s.length;
 var i;
 for (i=0;i<len;i++) 
 {
  if(s.charAt(i)!=" ") 
  return false;
 }
 return true;
}


function check()
{	
	
	var msg="Please make sure the following fields are valid \n\n";
	var key="";
	
	if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_calc.value)) 
		{
		key+=" Enter Calcdays\n";
		}
		
		
	if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_paid.value))
	   {	
	   key+=" Enter paiddays \n";
	   }
	   if(isBlank(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_pres.value))
	   {	
	   key+=" Enter presentdays \n";
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

function modify(rindex)
    {
     var cid=document.getElementById('<%=grid_check.ClientID%>');
     var cell = cid.rows[parseInt(rindex)+1].cells[0];     
     var hidID = cell.childNodes[0];   
         alert(hidID.value);
    
     var cell1 = cid.rows[parseInt(rindex)+1].cells[1];     
     var hidID1 = cell1.childNodes[0];   
     hidID1.value= parseInt(hidID.value)+parseInt(hidID1.value);    
    }


    </script>

    <table width="50%" cellpadding="2px" cellspacing="2px">
        <tr>
            <td>
                Calcdays</td>
            <td>
                <input runat="server" id="txt_calc" type="text"  CssClass="form-control"/></td>
        </tr>
        <tr>
            <td>
                Paiddays</td>
            <td>
                <input runat="server" id="txt_paid" type="text"  CssClass="form-control"/></td>
        </tr>
        <tr>
            <td>
                presentdays</td>
            <td>
                <input runat="server" id="txt_pres" type="text"  CssClass="form-control"/></td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="ImageButton1" runat="server" Text="save" OnClick="ImageButton1_Click"  OnClientClick="return check();"  class="btn btn-success"/>
                <%--<asp:ImageButton ID="ImageButton1" ImageUrl="../Images/Save.jpg" runat="server" OnClick="ImageButton1_Click"
                    OnClientClick="return check();" />--%></td>
        </tr>
        </table>
    <table width="50%" cellpadding="2px" cellspacing="2px">
        <tr>
            <td>
                <asp:GridView ID="grid_check" runat="server" DataKeyNames="aid" AutoGenerateColumns="false"
                    OnRowEditing="edit" OnRowUpdating="update" OnRowDeleting="delete" OnRowDataBound="grdTest_RowDataBound" class="table table-striped table-bordered table-hover"  ForeColor="#333333"  GridLines="None">
                    <SelectedRowStyle />
                    <PagerStyle HorizontalAlign="Left" />
                    <PagerStyle VerticalAlign="Middle" />
                    <Columns>
                        <%--<asp:TemplateField>
                        
                            <HeaderTemplate>
                                <thead>
                                    <th>
                                        calcdays</th>
                                    <th>
                                        paiddays</th>
                                        <th>presentdays</th>
                                        <th>Edit</th>
                                        <th>Update</th>
                                </thead>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tbody>
                                    <tr>
                                        <td>
                                            <input runat="server" id="grid_calc" value='<%#Eval("cadays")%>' type="text" disabled="disabled" /></td>
                                        
                                        <td>
                                            <input runat="server" value=<%#Eval("padays")%> id="grid_paid" type="text" disabled="disabled" /></td>
                                            <td>
                                            <input runat="server" id="grid_pres" value='<%#Eval("presdays")%>'  type="text" disabled="disabled" /></td>
                                            
                                        <td>
                                            <asp:ImageButton ID="img_edit" ImageUrl="../Images/i_Edit.gif"  AlternateText="" CommandName="update" runat="server" /></td>
                                            
                                            <td>
                                            <asp:ImageButton ID="img_update" ImageUrl="../Images/save.gif"  AlternateText="" CommandName="edit" runat="server" Visible="false" /></td>
                                    </tr>
                                </tbody>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                calcdays</HeaderTemplate>
                            <ItemTemplate>
                                <input runat="server" id="grid_calc" value='<%#Eval("cadays")%>' type="text" disabled="disabled" /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                paiddays</HeaderTemplate>
                            <ItemTemplate>
                                <input runat="server" value='<%#Eval("padays")%>' id="grid_paid" type="text" disabled="disabled" /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                presentdays</HeaderTemplate>
                            <ItemTemplate>
                                <input runat="server" id="grid_pres" value='<%#Eval("presdays")%>' type="text" disabled="disabled" /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Edit</HeaderTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="img_edit" ImageUrl="../Images/i_Edit.gif" AlternateText="" CommandName="update"
                                    runat="server" OnClientClick="return modify();" /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Update</HeaderTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="img_update" ImageUrl="../Images/save.gif" AlternateText="" CommandName="edit"
                                    runat="server" Visible="false" /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                DELETE</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton ID="img_delete" ImageUrl="../Images/Delete.jpg" CommandName="delete" runat="server" /></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
