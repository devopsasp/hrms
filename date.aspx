<%@ Page Language="C#" AutoEventWireup="true" CodeFile="date.aspx.cs" Inherits="date" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ePay-HRMS</title>
    <link href="../../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
    function page_load()
    {
    //var frm=document.forms[0];
    //alert("welcome");
    if(confirm("Would u like to Process it"))
    {
    //alert("Updated");
    //window.open("dateformat.aspx","popwindow", "toolbar=no,width=300,height=120,top=320,left=370,status=1")
    
    if(document.getElementById("TextBox1").value !="")
    {
    document.form1.submit();
    }
    else
    {
    alert("Fill all details")
    }
    }
    else
    {
    alert("error");
    }
    }
    
    function click()
    {
    if(document.getElementById("hid").value=="0")
    {
    document.getElementById("Calendar2").style.visibility="hidden";
    document.getElementById("hid").value="1";
    
    }
    else if(document.getElementById("hid").value=="1")
    {
    document.getElementById("Calendar2").style.visibility="visible";
    document.getElementById("hid").value="0";
    document.getElementById("Calendar2").style.display="block";
    }
    }
    </script>

</head>
<body onload="load();">
    <form id="form1" runat="server" action="dateformat.aspx">
        <div>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br />
            <br />
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><br />
            <br />
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
            <input type="submit" value="Click Here" onclick="javascript: page_load()" />
        </div>
        <table id="mainLayoutTable">
            <tbody>
                <tr>
                    
                    <td id="tdContentLeft" style="width: 15.4em;">
                        <div id="contentLeft" style="height: 100%">
                            <div id="contentFolderList" style="height: 100%">
                                <center>
                                    <div style="text-align: center">
                                    <table class="ContentTable" width="100%" height="50%">
                                    <tr>
                                    <td>
                                    <asp:Image ID="img_photo" runat="server" Height="106px" Width="90px" /><br />
                                    </td>
                                    </tr>
                                    </table>
                                    <asp:Label ID="lblmsg" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                                    </div>
                                </center>
                            </div>
                        </div>
                    </td>
                    <td id="tdContentRight" valign="top" style="width: 90%; height: 80%;">
                    <div>
                        <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
         <asp:SiteMapPath ID="SiteMapPath1" runat="server">
        </asp:SiteMapPath>
        <br /><br />
        <input type="text" />
        &nbsp;
        <a href="javascript:click();" > <img src="Images/cal_nextMonth.gif" style="border:0px; text-decoration:none;"/></a>
        <br />
        <div style="z-index:1px; float:left;position:relative;left:0px;top:0px;" id="dv_hid">
        <input type="text" />
            <asp:Calendar ID="Calendar2" runat="server" ></asp:Calendar>
            <input type="hidden" id="hid" value="0" />
        </div>
         <br /><br /> <br /><br />
        <input type="text" style="position:absolute;" />
       <%--<input type="image" onclick="click();" src="Images/cal_nextMonth.gif" />--%>
    </form>
    
</body>
</html>
