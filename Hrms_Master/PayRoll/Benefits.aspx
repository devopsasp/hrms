<%@ Page MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true" CodeFile="Benefits.aspx.cs" Inherits="Hrms_Master_Default4" Title="Welcome to HRMS" %>
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
    <script language=javascript type="text/javascript">
    function expandcollapse(obj,row)
    {
        var div = document.getElementById(obj);
        var img = document.getElementById('img' + obj);
          
        if (div.style.display == "none")
        {
            div.style.display = "block";
            if (row == 'alt')
            {
                img.src = "minus.gif";
            }
            else
            {
                img.src = "minus.gif";
            }
            img.alt = "Close to view other Slabs";
        }
        else
        {
            div.style.display = "none";
            if (row == 'alt')
            {
                img.src = "plus.gif";
            }
            else
            {
                img.src = "plus.gif";
            }
            img.alt = "Expand to show Slabs";
        }
    } 
    </script>
    <br />
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td id="tdComposeHeader">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="background-color:#5D7B9D" height="35px" class="border">
                            <span class="Title">&nbsp;&nbsp;
                            <span               
                                style="font-family: Calibri; color: #FFFFFF; font-size: medium; font-weight: bold;">Employee Benefits</span></span></td>
                    </tr>
                </table>
                <br />
                
                <tr><td align="center"><asp:Label ID="lbl_Error" runat="server" 
                        ForeColor="Red" CssClass="Error" Font-Bold="True" Font-Names="Calibri" 
                        Font-Size="X-Small"></asp:Label></td></tr>
                    <tr>
                   <td align="center">                    
                    
                </td>
                </tr>
        <tr>
            <td align="center">
            <asp:GridView ID="GridView1"  
            AutoGenerateColumns="False" DataKeyNames="SlabID"
            style="Z-INDEX: 101; LEFT: 8px; TOP: 32px" 
            ShowFooter="True" Font-Size="Small"
            Font-Names="Verdana" runat="server" OnRowDataBound="GridView1_RowDataBound" 
            OnRowCommand = "GridView1_RowCommand"
            OnRowDeleting = "GridView1_RowDeleting" OnRowDeleted = "GridView1_RowDeleted" 
                    Width="500px" CellPadding="4" ForeColor="#333333" GridLines="None">
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" ForeColor="White" HorizontalAlign="Left" 
                        Font-Bold="True"/>
            <FooterStyle  Font-Bold="True" ForeColor="White" />

            <Columns>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a href="javascript:expandcollapse('div<%# Eval("SlabID") %>', 'one');"><img id="imgdiv<%# Eval("SlabID") %>" alt="Click to show/hide benefits <%# Eval("SlabID") %>"  width="15px" height="15px" border="0" src="plus.gif"/></a>
                    </ItemTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Slab ID">
                    <ItemTemplate>
                        <asp:Label ID="lblSlabID" Text='<%# Eval("SlabID") %>' runat="server"></asp:Label>
                    </ItemTemplate>                    
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Designation">
                    <ItemTemplate>
                        <asp:Label ID="lblDesignation" Text='<%# Eval("v_DesignationName") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="txtDesignation" Width="100%" runat="server" 
                            ForeColor="Black">
                        <asp:ListItem>Select</asp:ListItem>
                        </asp:DropDownList>                        
                    </FooterTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Grade ">
                    <ItemTemplate><%# Eval("v_GradeName") %></ItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="txtGrade" Width="100%" runat="server" ForeColor="Black">
                        <asp:ListItem>Select</asp:ListItem>
                        </asp:DropDownList>
                        
                    </FooterTemplate>
                </asp:TemplateField>
                

			    <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDeleteCust" CommandName="Delete" runat="server">Delete</asp:LinkButton>
                    </ItemTemplate>
                    <FooterTemplate>
                        &nbsp;&nbsp;<asp:LinkButton ID="linkAddCust" CommandName="Add" runat="server" ForeColor="black">Add </asp:LinkButton>
                    </FooterTemplate>
                </asp:TemplateField>
			    
			    <asp:TemplateField>
			        <ItemTemplate>
			            <tr>
                            <td colspan="100%">
                                <div id="div<%# Eval("SlabID") %>" style="display:none;position:relative;left:15px;OVERFLOW: auto;WIDTH:95%" >
                                    <asp:GridView ID="GridView2" AllowPaging="False" AllowSorting="False" BackColor="#CCCCCC" Width="100%" Font-Size="X-Small"
                                        AutoGenerateColumns="false" Font-Names="Verdana" runat="server" DataKeyNames="SlabID" ShowFooter="true"
                                        OnRowCommand = "GridView2_RowCommand"  GridLines="Both" OnRowDataBound = "GridView2_RowDataBound"
                                        OnRowDeleting = "GridView2_RowDeleting" OnRowDeleted = "GridView2_RowDeleted"
                                        BorderStyle="Double" BorderColor="#0083C1" HeaderStyle-HorizontalAlign="Left">
                                        <RowStyle BackColor="Gainsboro" />
                                        <HeaderStyle BackColor="Maroon" ForeColor="White"/>
                                        <FooterStyle BackColor="White" />
                                        <Columns>                                            
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPromotionID" Text='<%# Eval("PromotionID") %>' Visible="false" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Allowance" HeaderStyle-Width="200px" >
                                                <ItemTemplate><%# Eval("allowance")%></ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="txtallowance" Width="100%" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </FooterTemplate>                                                
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Value" >
                                                <ItemTemplate><%# Eval("value")%></ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtvalue" Text='' Width="100%" runat="server"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                                                                        
			                                <asp:TemplateField HeaderText="Delete">
                                                 <ItemTemplate>
                                                    <asp:LinkButton ID="linkDeleteCust" CommandName="Delete" runat="server">Delete</asp:LinkButton>
                                                 </ItemTemplate>
                                                 <FooterTemplate>
                                                    <asp:LinkButton ID="linkAddOrder" CommandName="Addvalue" runat="server" ForeColor="black">Add</asp:LinkButton>
                                                 </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                   </asp:GridView>
                                </div>
                             </td>
                        </tr>
			        </ItemTemplate>			       
			    </asp:TemplateField>			    
			</Columns>
              <EditRowStyle BackColor="#999999" />
             <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:connectionstring %>" 
            SelectCommand="SELECT [SlabID], [v_DesignationName], [v_GradeName] FROM [Promotion_slab]">
            </asp:SqlDataSource>
                     
                </td>
        </tr>
        <tr valign="top">
            <td align="center" valign="top">
                &nbsp;<asp:Label ID="Error" runat="server" Width="50%" CssClass="Error" 
                    ForeColor="Red" Font-Bold="True" Font-Names="Calibri" Font-Size="Small"></asp:Label></td>
        </tr>
         </td>
                    </tr>
                    
                </table>
                   
</asp:Content>