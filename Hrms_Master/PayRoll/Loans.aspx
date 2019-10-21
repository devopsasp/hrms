<%@ Page Language="C#" MasterPageFile="~/HRMS.master" 
    AutoEventWireup="true" CodeFile="Loans.aspx.cs" Inherits="Bank_Loan_Default"
    Title="Welcome to HRMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="../../Scripts/Datavalid.js"></script>

    <script language="javascript" type="text/javascript">
    function show_message()
    {
        alert("Loan Code Already Exist");
    }
    
    function show_Error()
    {
        alert("Enter Value");
    }
    
    function fnSave()
    {   
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_code.value == "")
        {
            alert("Enter Loan Code");
            aspnetForm.ctl00$ContentPlaceHolder1$txt_code.focus();
            return false;
        }                        
        else
        { 
              
        if(document.aspnetForm.ctl00$ContentPlaceHolder1$txt_name.value == "")
        {
            alert("Enter Loan Name");
            aspnetForm.ctl00$ContentPlaceHolder1$txt_name.focus();
            return false;
        }                        
        else
        { 
              return true;  
        }
        
        }
    }
    </script>
    <div><h2 class="page-header">Loan Masters</h2></div>
    <div><h2>&nbsp;</h2></div>

               
    <table width="50%" class="table table-striped table-bordered table-hover">
       
        <tr>
            <td >
                <asp:Label ID="Label1" runat="server" Text="Loan code" ></asp:Label>
            </td>
            <td style="text-align: left" >
            <input id="txt_code" runat="server" class="form-control"  /></td>
           
            <td >
                <asp:Label ID="Label2" runat="server" Text="Loan Name"></asp:Label>
            </td>
            <td>
            <input id="txt_name" runat="server" class="form-control"
                    onkeydown="AllowOnlyText1(event);" 
                     /></td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <span >
                <asp:Button  ID="btn_save" runat="server" OnClientClick="return fnSave();" CssClass="btn btn-success"
                                Text="Add" OnClick="btn_save_Click" ImageAlign="AbsMiddle" />
                <%--<asp:Button ID="btn_save" runat="server" Text="Save" OnClick="btn_save_Click" />--%></span></td>
               
        </tr>

    </table>
    <table width="70%">
        <tr>
            <td>
                <asp:GridView ID="GV" runat="server" AutoGenerateColumns="False" Width="100%" DataKeyNames="loanid"
                    OnRowEditing="Edit" OnRowUpdating="Update" CellPadding="4" class="table table-striped table-bordered table-hover"
                    GridLines="None" onrowdeleting="Delete">
                    <RowStyle  />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <table cellspacing="0" cellpadding="0" width="100%">
                                    <colgroup>
                                        <col>   
                                    </colgroup>
                                    <thead>
                                        <tr>
                                            <th align="left"  style="width: 45%;" >
                                                Loan Code</th>
                                            <th align="left"  style="width: 40%;">
                                                Loan Name</th>
                                            <th style="width: 5%;">
                                                Edit</th>
                                              <th style="width: 5%;">
                                                Delete</th>
                                        </tr>
                                    </thead>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table class="dItemListContentTable" cellspacing="2%" cellpadding="2%" width="100%">
                                    <colgroup>
                                        <col class="dInboxContentTableCheckBoxCol">
                                    </colgroup>
                                    <tbody>
                                        <tr>
                                         <td style="width: 40%;">
                                         <input type="text" id="grd_txtcode" runat="server" value='<%#Eval("loancode")%>' disabled="disabled"  class="form-control" />
                                                <%--<asp:TextBox runat="server" Text='<%#Eval("loancode")%>' ID="grd_txtcode" Enabled="false"></asp:TextBox>--%>
                                            </td><td style="width: 5%;"></td>
                                            <td style="width: 40%;" nowrap>
                                            <input type="text" id="grd_txtname" runat="server" value='<%#Eval("loanname")%>' disabled="disabled" class="form-control"  />
                                                <%--<asp:TextBox runat="server" Text='<%#Eval("loanname")%>' ID="grd_txtname" Enabled="false"></asp:TextBox>--%>
                                            </td>
                                           
                                            <td align="center" style="width: 10%;">
                                                <asp:LinkButton ID="img_update"  runat="server" CssClass="btn btn-success btn-circle glyphicon glyphicon-check"
                                                     AlternateText="" CommandName="update" />
                                                <asp:LinkButton ID="img_save"  runat="server"  CssClass="btn btn-circle btn-success glyphicon glyphicon-plus-sign"
                                                    AlternateText="" CommandName="edit" Visible="false" />
                                            </td>
                                            <td align="center" style="width: 10%;">
                                                <asp:LinkButton ID="imgdel" CssClass="btn btn-danger btn-circle glyphicon glyphicon-minus-sign"  runat="server"
                                                                 CommandName="Delete" OnClientClick="return validate()"></asp:LinkButton>
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
</asp:Content>
