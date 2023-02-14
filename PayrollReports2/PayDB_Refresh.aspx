<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayDB_Refresh.aspx.cs" Inherits="DB_Refresh" Title="Updating Database" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Updating Database</title>
    <link href="../Css/Cand_BaseStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
    function close_Click()
    {
       //alert("Hi");
       //window.parent.location.href="Report.aspx";
       window.close();
    }
     </script>
    
</head>
<body bgcolor="buttonface" onload="close_Click()">
    <form id="form1" runat="server">
        <div style="text-align: center">
            <table style="width: 95%; height: 25%" align="left">
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="center"></td>
                </tr>
                <tr>
                </tr>
                <tr>
                     <td align="center">
                         <asp:Label ID="lbl_refresh" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Green"
                             Width="85%"></asp:Label>&nbsp;</td>
                </tr>
                <tr>
                    <td align="right"><img id="img_close" src="~/Images/Close.png" runat="server" 
                            onclick="close_Click()" />
                        </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
