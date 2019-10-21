<%@ Page MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/HRMS.master" AutoEventWireup="true"
    CodeFile="Project_Details.aspx.cs" Inherits="Hrms_Additional_Default" Title="Welcome to HRMS" Culture="en-GB" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
   
    function show_message()
    {
        alert("Leave Name Already Exist");
    }
    </script>
    <link href="../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" language="javascript" src="../Scripts/Datavalid.js"></script>
<script language="javascript" type="text/javascript" src="../datecheck.js"></script>
    <script language="javascript" type="text/javascript">
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
            img.alt = "Close to view other Customers";
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
            img.alt = "Expand to show Orders";
        }
    } 
    
     function fn_date(event,txtid)
 {  
         var len;
         alert("called");
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
    
    </script>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"> 
    </asp:ToolkitScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                            <div class="divWaiting">
                            
                            <asp:Image ID="imgWait" runat="server" ImageAlign="Middle" ImageUrl="~/Images/loading2.gif" Height="100px" Width="100px" />
                                <%--<img src="../loading.gif" alt="Loading" style="position:relative;" />--%>
                            </div>
                            </ProgressTemplate>
                            </asp:UpdateProgress>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>

    <div ><h3 class="page-header">Project Details</h3></div>
                     <div></div>
                                    <div style="width: 70%">
                      <table width="100%" class="table table-striped table-bordered table-hover">
                        <tr>
                            <td>
                                Calendar year</td>
                            <td>
                            
                            <asp:DropDownList ID="ddl_year" runat="server"  CssClass="form-control" 
                                    AutoPostBack="True" onselectedindexchanged="ddl_year_SelectedIndexChanged">
                            </asp:DropDownList>
                           </td>
                            <td>
                                Department</td>
                            <td>
                            
                            <asp:DropDownList ID="ddl_department" runat="server" AutoPostBack="True" onselectedindexchanged="ddl_department_SelectedIndexChanged" 
                                 CssClass="form-control">
                            </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="right">
                                &nbsp;</td>
                        </tr>
                     </table>
                     </div>
                     <div style="width: 70%">
                     <table style="width: 100%">
                       <tr valign="top">
                         <td>
                        
            <asp:GridView ID="GridView1" AutoGenerateColumns="False" DataKeyNames="Pid"  class="table table-striped table-bordered table-hover"
            ShowFooter="True" runat="server" OnRowDataBound="GridView1_RowDataBound" OnRowCommand = "GridView1_RowCommand" 
            OnRowDeleting = "GridView1_RowDeleting" OnRowDeleted = "GridView1_RowDeleted">
            <RowStyle/>
            <HeaderStyle HorizontalAlign="Left"/>
            <FooterStyle  />
            <Columns>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a href="javascript:expandcollapse('div<%# Eval("Pid") %>', 'one');"><img id="imgdiv<%# Eval("Pid") %>" alt="Click to show/hide benefits <%# Eval("Pid") %>"  width="10px" height="10px" border="0" src="plus.gif"/></a>
                    </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:Label ID="lblSlabID" Visible="false" Text='<%# Eval("pid") %>' runat="server"></asp:Label>
                    </ItemTemplate>                    
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Emp ID">
                    <ItemTemplate>
                        <asp:Label ID="lblempID" Text='<%# Eval("pn_EmployeeID") %>' runat="server"></asp:Label>
                    </ItemTemplate>                    
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Employee Name" >
                    <ItemTemplate>
                        <asp:Label ID="lblempname" Text='<%# Eval("pn_EmployeeName") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="txtempname" CssClass="form-control" runat="server" >
                        <asp:ListItem>Select</asp:ListItem>
                        </asp:DropDownList>                        
                    </FooterTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Project Name">
                    <ItemTemplate><%# Eval("p_Name") %></ItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="txtpname" CssClass="form-control" runat="server">
                        <asp:ListItem>Select Project</asp:ListItem>
                        </asp:DropDownList>                        
                    </FooterTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="From Date">
                    <ItemTemplate>
                    <asp:Label ID="txtfromdate" runat="server" Text='<%# Eval("from_date", "{0:dd/MM/yyyy}")%>' ></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtfdate" runat="server" CssClass="form-control"></asp:TextBox>
                       <%-- <asp:TextBox ID="txtfdate" runat="server" CssClass="form-control"
                             onkeyup="fn_date(event,this.id);" maxlength="10" AutoPostBack="true" OnTextChanged="txtfdate_TextChanged" ></asp:TextBox>--%>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                                                     TargetControlID="txtfdate" TodaysDateFormat="d MMMM, yyyy" />
                    </FooterTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="To Date">
                    <ItemTemplate>
                    <asp:Label ID="txttodate" runat="server" Text='<%# Eval("to_date", "{0:dd/MM/yyyy}")%>' ></asp:Label>

                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txttdate" runat="server" CssClass="form-control" maxlength="10" AutoPostBack="True" OnTextChanged="txttdate_TextChanged"></asp:TextBox>
                        <%--<asp:TextBox ID="txttdate" runat="server" CssClass="form-control"
                            onkeyup="fn_date(event,this.id);"  maxlength="10"  OnTextChanged="txttdate_TextChanged"></asp:TextBox>--%>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                                                     TargetControlID="txttdate" TodaysDateFormat="d MMMM, yyyy" />
                      
                    </FooterTemplate>
                </asp:TemplateField>
                

			    <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDeleteCust" CssClass="btn btn-danger btn-circle glyphicon glyphicon-minus-sign" CommandName="Delete" runat="server"></asp:LinkButton>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:LinkButton ID="linkAddCust" class="btn btn-success btn-circle glyphicon glyphicon-plus-sign" CommandName="Add" runat="server"></asp:LinkButton>
                    </FooterTemplate>
                </asp:TemplateField>
			    
			    <asp:TemplateField>
			        <ItemTemplate>
			            <tr>
                            <td colspan="100%">
                                <div id="div<%# Eval("Pid") %>" style="display:none;position:relative;left:15px;OVERFLOW: auto;WIDTH:95%"  >
                                    <asp:GridView ID="GridView2" AllowPaging="False" AllowSorting="False" Width="100%" class="table table-striped table-bordered table-hover"
                                        AutoGenerateColumns="false" runat="server" DataKeyNames="Pid" ShowFooter="true"
                                        OnRowCommand = "GridView2_RowCommand"  GridLines="Both" OnRowDataBound = "GridView2_RowDataBound"
                                        OnRowDeleting = "GridView2_RowDeleting" OnRowDeleted = "GridView2_RowDeleted" onrowediting="GridView2_RowEditing" >  
                                         
                                        <Columns>
                                        
                                        <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" Text='<%# Eval("ID") %>' Visible="false" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblearningsID" Text='<%# Eval("pn_earningsID") %>' Visible="false" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Allowance">
                                                <ItemTemplate>
                                                <asp:TextBox ID="txtename" Width="100%" Text='<%# Eval("v_EarningsName") %>' runat="server" CssClass="form-control"></asp:TextBox>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                <asp:TextBox ID="txteditename" Width="100%" Text='<%# Eval("v_EarningsName") %>' runat="server" CssClass="form-control"></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="txtallowance" Width="100%" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </FooterTemplate>                                                
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                <asp:TextBox ID="txtvalue" Width="100%" Text='<%# Eval("n_Amount") %>' runat="server" CssClass="form-control"></asp:TextBox>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                <asp:TextBox ID="txteditvalue" Width="100%" Text='<%# Eval("n_Amount") %>' runat="server" CssClass="form-control"></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtvalue0" Text='' Width="100%" runat="server" CssClass="form-control"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                                                                        
			                                <asp:TemplateField HeaderText="Delete">
                                                 <ItemTemplate>
                                                    <asp:LinkButton ID="linkDeleteCust0" CommandName="Delete" runat="server" CssClass="btn btn-danger btn-circle glyphicon glyphicon-minus-sign"></asp:LinkButton>
                                                 </ItemTemplate>
                                                 <FooterTemplate>
                                                    <asp:LinkButton ID="linkAddOrder" CommandName="Addvalue" runat="server" class="btn btn-success btn-circle glyphicon glyphicon-plus-sign"></asp:LinkButton>
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
        </asp:GridView>
                        </td>
                    </tr>
                         <tr valign="top">
                             <td >
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:connectionstring %>" 
            SelectCommand="SELECT [SlabID], [v_DesignationName], [v_GradeName] FROM [Promotion_slab]">
            </asp:SqlDataSource>
                        
                             </td>
                         </tr>
                  </table>
                  </div>
                  </ContentTemplate>
                                </asp:UpdatePanel>

    </asp:Content>
