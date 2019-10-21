<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="Assets.aspx.cs" Inherits="Hrms_Master_Employee_Assets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="../../Scripts/Datavalid.js"></script>
    <script type="text/javascript">
        window.onload = function () {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lbl_Error.ClientID %>").innerHTML = "";
            }, seconds * 1000);
        };
</script>
    <script language="javascript" type="text/javascript">
  
  function validate()
  {

    var r=confirm("Are you sure you want to delete this record?");
    if (r==true)
    {
    return true;
    }
    else
    {
    return false;
    }
  }

  
  
   function show_message()
    {
        alert("Course Name Already Exist");
    }
    
    function show_Error()
    {
        alert("Enter Course Name");
    }
  
    function fnSave()
    {   
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$CourseName.value == "")
        {
            alert("Enter Course Name");
            aspnetForm.ctl00$ContentPlaceHolder1$CourseName.focus();
            return false;
        }                        
        else
        { 
              return true;  
        }
    } 
    function coudnt_del()
    {
        alert("Cannot delete. This course is already assigned to someone");
    }   
    </script>

    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td id="tdComposeHeader" valign="top" align="right" colspan="3">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td  style="background-color:#5D7B9D; width: 680px;" height="35px" 
                            class="border">
                            <span class="Title">&nbsp;&nbsp;<span 
                                
                                style="font-family: Calibri; color: #FFFFFF; font-size: medium; font-weight: bold;">Assets</span></span></td>
                                <td style="background-color:#5D7B9D">
                                    <asp:DropDownList ID="ddl_branch" runat="server" AutoPostBack="True" 
                                         Width="115px">
                                    </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table cellpadding="5px" cellspacing="1px" width="100%">
                    <tr>
                        <td align="center" style="height: 218px">
                            &nbsp;<asp:Label ID="lbl_Error" runat="server" CssClass="Error" ForeColor="Red" 
                                Font-Bold="True" Font-Names="Calibri" Font-Size="X-Small" Height="16px"></asp:Label>
                                <asp:GridView ID="grid_assets" runat="server" AutoGenerateColumns="False" Width="60%"
                                    DataKeyNames="AssetId" OnRowEditing="Edit" OnRowUpdating="Update" 
                                  
                                    onrowcommand="grid_assets_RowCommand" 
                                    Height="100px" CellPadding="4" 
                                ForeColor="#333333" GridLines="None" 
                                onrowdeleting="grid_assets_RowDeleting">
                                    <RowStyle ForeColor="#333333" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <table cellspacing="0" cellpadding="0" width="100%">
                                                    <thead>
                                                        <tr>
                                                            <th align="left" style="width: 86%; font-family: Calibri; color: #FFFFFF; font-weight: bold;">
                                                               Asset Name</th>
                                                            <td  style="width: 7%; font-family: Calibri; color: #FFFFFF; font-weight: bold;">
                                                                <asp:Label ID="lbledit" Text="Edit" runat="server"></asp:Label></td>
                                                                 <td id="del" style="width: 7%; font-family: Calibri; color: #FFFFFF; font-weight: bold;">
                                                               <asp:Label ID="lbldel" Text="Delete" runat="server"></asp:Label></td>
                                                            
                                                        </tr>
                                                    </thead>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table class="dItemListContentTable" border="0" cellspacing="0" cellpadding="0" width="100%">
                                                    <colgroup>
                                                        <col class="dInboxContentTableCheckBoxCol">
                                                    </colgroup>
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 80%; font-family: Calibri;" 
                                                                nowrap>
                                                                <input runat="server" id="txtgrid" value='<%# Eval("AssetName") %>' Font-Bold="true" disabled="disabled" Font-Names="calibri" Font-Size="small" style="width: 100%" />
                                                             </td>
                                                            <td align="center" style="width: 10%;" >
                                                                <asp:ImageButton ID="img_update" ImageUrl="../../Images/i_Edit.gif" runat="server" Style="border: 0" AlternateText="" CommandName="Update" />
                                                                <asp:ImageButton ID="img_save" ImageUrl="../../Images/save.gif" runat="server" Style="border: 0" AlternateText="" CommandName="Edit" Visible="false" />
                                                             </td>
                                                             <td style="width:10%;" align="center">
                                                                <asp:ImageButton ImageUrl="../../Images/delete_icon.gif" ID="imgdel" runat="server" CommandName="Delete" OnClientClick="return validate()" />
                                                             </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Names="Calibri" ForeColor="White" 
                                        Font-Bold="True" />
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle ForeColor="#284775" />
                                </asp:GridView>
                            </td>
                    </tr>
                                      
                </table>
            </td>
        </tr>
        <tr>
            <td align="right" style="height: 19px" width="40%">
                <b>
                <asp:Label ID="Label1" runat="server" Font-Names="Calibri" 
                    Text="Enter Asset Name"></asp:Label>
&nbsp;</b></td>
            <td align="center" style="height: 19px">
                            <asp:TextBox class="InputDefaultStyle" 
                                
                    style="font-family: Calibri;"  ID="txt_assets" 
                                runat="server" Height="25px"  Width="155px" Font-Size="Small"></asp:TextBox>
            </td>
            <td align="left" style="height: 19px" width="40%">
                            <span style="font-family: Calibri; font-size: x-small; color: #6A6A6A">
                            <asp:ImageButton ImageUrl="~/Images/Add.png" onmouseover="this.src='../../Images/Addover.png';" onmouseout="this.src='../../Images/Add.png';" ID="btn_save" runat="server" OnClientClick="return fnSave();"
                                Text="Add" ImageAlign="AbsMiddle" onclick="btn_save_Click" /></span>
            </td>
        </tr>
        <tr>
            <td align="center" style="height: 19px" colspan="3">
                &nbsp;<asp:Label ID="Error" runat="server" Width="50%" CssClass="Error" 
                    ForeColor="Red" Font-Bold="True" Font-Names="Calibri" Font-Size="X-Small"></asp:Label>
            </td>
        </tr>
         <tr>
            <td colspan="3">
                <input id="hAssetID" runat="server" type="hidden" value="0" />
                <input type="hidden" id="ToolBarCode" name="ToolBarCode" runat="server" value="0" /></td>
        </tr>
    </table>
</asp:Content>