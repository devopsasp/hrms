<%@ Page Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Tasks_Online.aspx.cs" Inherits="Hrms_Tasks_Default" Title="ePay-HRMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">

  function show_message(msg)
    {
        alert(msg);
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
           bool_obj=true;
                      
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
</script>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td id="td1" valign="top">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="35px" width="80%">
                            <span class="Title">&nbsp;&nbsp; 
                            <span style="font-family: Calibri; font-size: medium; font-weight: bold; ">Human Resource Management System -----&gt; Online Training</span></span></td>
                        <td height="35px" width="20%" align="center">
                            <span class="Title"><asp:Label ID="Label8" runat="server" Font-Names="Calibri" ></asp:Label> </span>
                        </td>
                    </tr>
                </table>
                
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr valign="top">
                        <td id="tdComposeHeader" valign="top" align="center">
                            <table cellpadding="5px" cellspacing="1px" width="100%">
                                <tr>
                                    <td width="100%">
                                        <table width="100%" align="center" class="InputTextStyle">
                                            <tr>
                                                <td align="center" colspan="4">
                                                    <asp:Label ID="lbl_Error" runat="server" Font-Bold="True" ForeColor="Red" 
                                                        style="font-weight: 400; font-style: normal"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="height: 20px; width: 771px;">
                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="False" 
                                                        Font-Size="Small" Text="Enter the question set no :" Font-Names="Calibri"></asp:Label>&nbsp;<%--<asp:Button ID="btn_qsubmit" runat="server" onclick="btn_qsubmit_Click" 
                                                        Text="Submit" style="height: 26px" />--%>
                                                    <asp:DropDownList 
                                                        ID="ddl_qsetcode" runat="server" AutoPostBack="True"  CssClass="form-control"
                                                        onselectedindexchanged="ddl_qsetcode_SelectedIndexChanged" Width="102px" 
                                                        Font-Names="Calibri" Height="17px" Font-Size="Small">
                                                        <asp:ListItem>Select</asp:ListItem>
                                                        <asp:ListItem>New</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:TextBox 
                                                        ID="txt_qsetno" runat="server" Width="102px" Height="23px"  CssClass="form-control"
                                                        Font-Size="Small"></asp:TextBox>
                                                    </td>
                                                <td align="left" style="height: 20px; width: 123px;">
                                                    <asp:Button ID="ImageButton1" runat="server" Text="Submit" onclick="ImageButton1_Click" class="btn btn-success"/>
                                                    <%--<asp:ImageButton ID="ImageButton1" runat="server" 
                                                        ImageUrl="~/Images/Submit.png" onmouseover="this.src='../Images/Submitover.png';" onmouseout="this.src='../Images/Submit.png';" onclick="ImageButton1_Click" />--%>
                                                </td>
                                                <td align="center" style="height: 20px; width: 107px;">
                                                    &nbsp;</td>
                                                <td align="center" style="height: 20px;">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" align="center">
                                                    
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="4">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="35px">
                            <span class="Title">
                            <span style="font-family: Calibri; font-weight: bold;"><h4>Online Test Assignment</h4>                        
                                            </tr>
                                            <tr>
                                                <td style="border-style: none; ">
                                                    
                                                    <asp:GridView ID="GridView1" Font-Size="Small" runat="server" AllowSorting="True" 
            AutoGenerateColumns="False" Height="16px" ShowFooter="True" class="table table-striped table-bordered table-hover"
            Width="850px" 
            onrowcommand="GridView1_RowCommand" CellPadding="4" 
            onrowdeleting="GridView1_RowDeleting" 
            onrowdatabound="GridView1_RowDataBound" onrowediting="GridView1_RowEditing" HorizontalAlign="Center" 
                                    onrowupdating="GridView1_RowUpdating" 
                                    onrowcancelingedit="GridView1_RowCancelingEdit" ForeColor="#333333" GridLines="None">
            <FooterStyle  Font-Bold="True" />
            <RowStyle  Font-Names="Calibri" Font-Size="Small"  />
            <Columns>
            
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="S.no">
                
                    <ItemTemplate>
                        <asp:Label ID="lbl_sno" runat="server" Text='<%# Eval("Sno") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:Label ID="lbl_sno_edit" runat="server" Text='<%# Bind("Sno") %>' 
                            Height="21px" Width="50px"></asp:Label>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="ddl_sno" runat="server" DataTextField="sno"  CssClass="form-control"
                            DataValueField="sno" Width="30px" ReadOnly="True"></asp:TextBox>
                    </FooterTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Enter the questions">
                    <ItemTemplate>
                        <asp:Label ID="lbl_que" runat="server" Text='<%# Eval("questions") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="txt_que_edit" runat="server" Text='<%# Bind("questions") %>' 
                            Width="187px"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_que" runat="server"  CssClass="form-control" Width="250px"></asp:TextBox>
                    </FooterTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Option 1">
                    <ItemTemplate>
                        <asp:Label ID="lbl_opt1" runat="server" Text='<%# Eval("option1") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="txt_opt1_edit" runat="server" Text='<%# Bind("option1") %>' Width="74px"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_opt1" runat="server"  CssClass="form-control" Width="120px"></asp:TextBox>
                    </FooterTemplate>
                    
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                 <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Option 2">
                    <ItemTemplate>
                        <asp:Label ID="lbl_opt2" runat="server" Text='<%# Eval("option2") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="txt_opt2_edit" runat="server" Text='<%# Bind("option2") %>' Width="74px"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_opt2" runat="server"  CssClass="form-control" Width="120px"></asp:TextBox>
                    </FooterTemplate>
                    
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                 <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Option 3">
                    <ItemTemplate>
                        <asp:Label ID="lbl_opt3" runat="server" Text='<%# Eval("option3") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="txt_opt3_edit" runat="server" Text='<%# Bind("option3") %>' Width="74px"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_opt3" runat="server"  CssClass="form-control" Width="120px"></asp:TextBox>
                    </FooterTemplate>
                    
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                 <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Option 4">
                    <ItemTemplate>
                        <asp:Label ID="lbl_opt4" runat="server" Text='<%# Eval("option4") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="txt_opt4_edit" runat="server" Text='<%# Bind("option4") %>' Width="74px"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_opt4" runat="server"  CssClass="form-control" Width="120px"></asp:TextBox>
                    </FooterTemplate>
                    
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Answer">
                    <ItemTemplate>
                        <asp:Label ID="lbl_ans" runat="server" Text='<%# Eval("answer") %>' 
                            Height="16px" Width="80px"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:DropDownList ID="ddl_ans_edit" runat="server" DataTextField="answer" 
                            DataValueField="answer" Width="74px">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                    </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddl_ans" runat="server"  CssClass="form-control" Height="20px" Width="38px">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="Button1" CommandName="add" runat="server" Text="ADD" 
                            Font-Bold="True" Font-Size="X-Small" ForeColor="Black" Width="36px" class="btn btn-success"
                            Font-Names="Cambria" Height="20px" />
                    </FooterTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
             
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="False" />
            </Columns>
            <PagerStyle  HorizontalAlign="Center" />
            <SelectedRowStyle  Font-Bold="True"  />
            <HeaderStyle  Font-Bold="True"  Font-Names="Calibri" Font-Size="X-Small" />
            <EmptyDataTemplate>
            <asp:Label ID="lblempty" Text="No Records" runat="server">
            </asp:Label> 
            
            </EmptyDataTemplate>
            <EditRowStyle  />
            <AlternatingRowStyle   />
            </asp:GridView><br /> 
                                                               
            <asp:GridView ID="GridView2" Font-Size="Small" runat="server" AllowSorting="True" class="table table-striped table-bordered table-hover"
            AutoGenerateColumns="False" Height="16px" ShowFooter="True" 
            Width="767px" 
            onrowcommand="GridView2_RowCommand" CellPadding="4" 
            ForeColor="#333333" onrowdeleting="GridView2_RowDeleting" 
            onrowdatabound="GridView2_RowDataBound" onrowediting="GridView2_RowEditing" 
            HorizontalAlign="Center" onrowupdating="GridView2_RowUpdating" 
            onrowcancelingedit="GridView2_RowCancelingEdit"  GridLines="None">
            
           <FooterStyle 
               BorderStyle="None" Font-Size="Small" Font-Bold="True" />
           <RowStyle  
               Font-Names="Calibri"  BorderStyle="None" Font-Size="Small" />
            <Columns>
            
                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Sno" HeaderStyle-HorizontalAlign="Left">                
                    <ItemTemplate>
                        <asp:Label ID="lbl_sno" runat="server" Text='<%# Eval("Sno") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:Label ID="lbl_snoedit" runat="server" Text='<%# Bind("Sno") %>' 
                            Height="21px" Width="98px"></asp:Label>
                    </EditItemTemplate>
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Question Set no"  HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lbl_qid" runat="server" Text='<%# Eval("pn_questid") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:DropDownList ID="ddl_qidedit" runat="server"  
                            Height="18px" Width="128px" DataSourceID="SqlDataSourceqid" DataTextField="pn_questID" 
                            DataValueField="pn_questID"></asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSourceqid" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:connectionstring %>" 
                            
                            SelectCommand="SELECT DISTINCT [pn_questID] FROM [online] WHERE ([pn_BranchID] = @pn_BranchID)">
                            <SelectParameters>
                                <asp:SessionParameter Name="pn_BranchID" SessionField="Login_temp_BranchID" 
                                    Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddl_qid" runat="server" Height="18px" Width="128px"  CssClass="form-control"
                            DataSourceID="SqlDataSourceq" DataTextField="pn_questID" 
                            DataValueField="pn_questID">
                            <asp:ListItem>Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSourceq" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:connectionstring %>" 
                            SelectCommand="SELECT DISTINCT [pn_questID] FROM [online] WHERE ([pn_BranchID] = @pn_BranchID)">
                            <SelectParameters>
                                <asp:SessionParameter Name="pn_BranchID" SessionField="Login_Temp_BranchID" 
                                    Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </FooterTemplate>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>

                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Department"  HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="Label9" runat="server" Text='<%# Eval("pn_DepartmentName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:DropDownList ID="deptedit" runat="server" DataTextField="v_DepartmentName" 
                            DataValueField="v_DepartmentName" Height="18px" Width="128px" AutoPostBack="true" DataSourceID="SqlDataSourcedept" 
                            onselectedindexchanged="deptedit_SelectedIndexChanged"><asp:ListItem>Select</asp:ListItem></asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSourcedept" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:connectionstring %>" 
                            
                            SelectCommand="SELECT DISTINCT [v_DepartmentName] FROM [paym_Department] WHERE ([pn_BranchID] = @pn_BranchID)">
                            <SelectParameters>
                                <asp:SessionParameter Name="pn_BranchID" SessionField="Login_temp_BranchID" 
                                    Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="DropDownList4" runat="server" Height="18px" Width="128px"  CssClass="form-control" 
                            AutoPostBack="True" DataSourceID="SqlDataSourcedl" 
                            DataTextField="v_DepartmentName" DataValueField="v_DepartmentName" onselectedindexchanged="DropDownList4_SelectedIndexChanged">
                            
                            <asp:ListItem>Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSourcedl" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:connectionstring %>" 
                            SelectCommand="SELECT [v_DepartmentName] FROM [paym_Department] WHERE ([pn_BranchID] = @pn_BranchID)">
                            <SelectParameters>
                                <asp:SessionParameter Name="pn_BranchID" SessionField="Login_Temp_BranchID" 
                                    Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </FooterTemplate>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Assigned to"  HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("pn_EmployeeName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:DropDownList ID="assiedit" runat="server" Height="18px" Width="128px" DataTextField="Employee_First_Name" 
                            DataValueField="Employee_First_Name"></asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="DropDownList1" runat="server" Height="18px"  CssClass="form-control" Width="128px">
                            <asp:ListItem>Select</asp:ListItem>
                        </asp:DropDownList>
                    </FooterTemplate>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Date"  HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server"  Text='<%# Eval("test_date","{0:dd/MM/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="tdateedit" runat="server" Text='<%# Bind("test_date","{0:dd/MM/yyyy}") %>' Width="74px"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="tdate" runat="server" onkeyup="fn_date(event,this.id);"  CssClass="form-control" Width="72px"></asp:TextBox>
                    </FooterTemplate>
                    
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
               <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="No. of questions"  HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lbl_total" runat="server" Text='<%# Eval("total_quest") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="lbl_totaledit" runat="server" Width="120px" Text='<%# Bind("total_quest") %>' ></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddl_total"   CssClass="form-control" runat="server" Width="50px">
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>15</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>25</asp:ListItem>
                            <asp:ListItem>30</asp:ListItem>
                            <asp:ListItem>35</asp:ListItem>
                            <asp:ListItem>40</asp:ListItem>
                            <asp:ListItem>45</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="Button1" CommandName="add1" runat="server" Text="ADD" 
                            Font-Bold="True" Font-Size="Small" ForeColor="Black" Width="50px" 
                            Font-Names="Calibri" Height="22px" class="btn btn-success"/>
                    </FooterTemplate>
<FooterStyle HorizontalAlign="Center" />
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                
                <asp:CommandField ShowDeleteButton="True" />
            </Columns>
                           <PagerStyle  HorizontalAlign="Center" />
            <EmptyDataTemplate>
            <asp:Label ID="lblempty" Text="No Records" runat="server">
            </asp:Label> 
            
            </EmptyDataTemplate>
                           <SelectedRowStyle Font-Bold="True"  />
                           <HeaderStyle  Font-Bold="True" 
                               Font-Names="Calibri" BorderStyle="None" 
                               Font-Size="Small" />
                                                        <EditRowStyle  />
                                                        <AlternatingRowStyle  />
        </asp:GridView>
                                                    
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
