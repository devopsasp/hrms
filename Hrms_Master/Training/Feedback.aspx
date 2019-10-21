<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Feedback.aspx.cs" MasterPageFile="~/HRMS.master" Inherits="Hrms_Master_Training_Feedback" %>

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
        alert("Cannot delete. It is already assigned to someone");
    }   
    </script>

    <div><h3 class="page-header">Training Feedback<asp:DropDownList ID="ddl_branch" runat="server" AutoPostBack="True" 
                                         Width="115px">
                                    </asp:DropDownList>
                                                </h3></div>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">

    <ContentTemplate>
        <div style="width: 70%">
                    <table cellpadding="1%" cellspacing="1%" width="100%" class="table table-striped table-bordered table-hover">
                        
                        <tr>
                            <td>
                                Enter Feedback Question</td>
                            <td >
                                <asp:TextBox ID="txt_ques" runat="server" class="InputDefaultStyle" Height="44px" style="font-family: Calibri; font-size: small;" TextMode="MultiLine" Width="345px"></asp:TextBox>
                            </td>
                            <td >
                                 <asp:Button ID="btn_save" Text="Add" runat="server" class="btn btn-success" OnClick="btn_save_Click1"/>
                                </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="lbl_Error" runat="server" CssClass="Error" Font-Bold="True" Font-Names="Calibri" Font-Size="Small" ForeColor="Red" Height="16px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:GridView ID="grid_feedback" runat="server" AutoGenerateColumns="False" CssClass="table table-hover table-striped" DataKeyNames="FeedbackID"  onrowcommand="grid_feedback_RowCommand" onrowdeleting="grid_feedback_RowDeleting" OnRowEditing="Edit" OnRowUpdating="Update" >

                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <table >
                                                    <%--<colgroup>
                                                        <col>
                                                    </colgroup>--%>
                                                    <thead>
                                                        <tr>
                                                            <th >Feedback Questions</th>
                                                            <td >
                                                                <asp:Label ID="lbledit" runat="server" Text="Edit"></asp:Label>
                                                            </td>
                                                            <td id="del" >
                                                                <asp:Label ID="lbldel" runat="server" Text="Delete"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </thead>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table>
                                                    <colgroup>
                                                        <col class="dInboxContentTableCheckBoxCol"></col>
                                                    </colgroup>
                                                    <tbody>
                                                        <tr>
                                                            <td >
                                                                <input runat="server" id="txtgrid" value='<%# Eval("FeedbackQues") %>' Font-Bold="true" disabled="disabled" Font-Names="calibri" Font-Size="small" style="width: 100%" />
                                                            </td>
                                                            <td >
                                                                <asp:ImageButton ID="img_update" runat="server" AlternateText="" CommandName="Update" ImageUrl="../../Images/i_Edit.gif" Style="border: 0" />
                                                                <asp:ImageButton ID="img_save" runat="server" AlternateText="" CommandName="Edit" ImageUrl="../../Images/save.gif" Style="border: 0" Visible="false" />
                                                            </td>
                                                            <td >
                                                                <asp:ImageButton ID="imgdel" runat="server" CommandName="Delete" ImageUrl="../../Images/delete_icon.gif" OnClientClick="return validate()" />
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                  
                                </asp:GridView>
                            </td>
                        </tr>
                     </table>   
                     </div>
                     <div>  

             <input id="hFeedbackID" runat="server" type="hidden" value="0" />
                <input type="hidden" id="ToolBarCode" name="ToolBarCode" runat="server" value="0" />
                       
                                                      
           </div>
    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
