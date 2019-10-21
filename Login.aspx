<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="flash" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
<head>

    <meta>
    <title>Human Resource Management Solution</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="Human Resource Management System, HRMS Solution, HR Software,Personnel Software, Employee Software, HRDatabase, HRsystem, INFORMATIONTECHNOLOGY , PROJECT MANAGEMENT ,TRAINING ,Payroll module,Recruitment, selection , applicant database module, Benefits administration module
Training , staff development module, Performance Management, appraisal , performance planning , Payroll ,Time and attendance ,Performance appraisal ,Benefits administration
Recruiting , Learning management, Performance record , Employee self-service ,Scheduling ,Absence management ,Analytics, Student Registration , AdministrationTraining ,  Event Management  , Curriculum , Certification Management ,Skills , Competencies Management, Skill Gap Analysis, Reporting, Training Record Management , ResourceManagement">
    <meta name="author" content="Hesperus Infosys" />

    <link href="css/bootstrap-cerulean.min.css" rel="stylesheet" />


    <!-- MetisMenu CSS -->
    <link id="Link2" href="Styles/css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" runat="server" />

    <!-- Custom CSS -->
    <link id="Link3" href="Styles/css/sb-admin-2.css" rel="stylesheet" runat="server" />
    <link id="Link1" href="Styles/css/bootstrap.min.css" rel="stylesheet" runat="server" />
    <!-- Custom Fonts -->
    <link id="Link4" href="Styles/font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" runat="server" />

    <%-- <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />--%>
    <link id="Link5" href="js/bootstrap.min.css" rel="stylesheet" runat="server" />


    <link href="JQuery/jquery-ui.min.css" rel="stylesheet" />
    <link href="JQuery/jquery-ui.css" rel="stylesheet" />
    <link href="Scripts/Index.css" rel="stylesheet" />


    <link href="css/charisma-app.css" rel="stylesheet" />
    <link href='css/jquery.noty.css' rel='stylesheet' />
    <link href='css/noty_theme_default.css' rel='stylesheet' />
    <link href='css/elfinder.min.css' rel='stylesheet' />

    <link href='css/jquery.iphone.toggle.css' rel='stylesheet' />
    <link href='css/uploadify.css' rel='stylesheet' />
    <link href='css/animate.min.css' rel='stylesheet' />

    <link rel="shortcut icon" href="Images/favicon.ico" />

    <script type="text/javascript">
        function showPass() {
            var cookies = document.cookie;
            var allcookie = cookies.split(";");
            for (ctr = 0; ctr < allcookie.length; ctr++) {
                var dt = allcookie[ctr];
                var str = dt.split("=");

                
                if (str[0].trim() == document.getElementById("emp_userid").value.trim()) {
                    document.getElementById("emp_password").value = str[1];
                    break;
                }
            }
        }

        var cookies = document.cookie.split(";");     
        for (var i = 0; i < cookies.length; i++)
        {
            var cookiename =  cookies[i].split("=");     
            document.cookie = cookiename[0] + '=; expires=Thu, 01 Jan 1970 00:00:01 GMT; path=/';
       }
        


        //function showBoth() {
        //    var cookies = document.cookie;
        //    var allcookie = cookies.split(";");
        //    for (c = 0; c < allcookie.length; c++) {
        //        var a = allcookie[c];
        //        var v = a.split("=");
        //        if (v[0].trime() == "lastid")
        //            document.getElementById("emp_userid").value = v[1];
        //        if (v[0].trim() == "lastpass") {
        //            document.getElementById("emp_password").value = v[1];
        //            break;
        //        }
        //    }
        //}

    </script>




</head>

<body>
    <%--    <script language="javascript" type="text/javascript">
  
      var sessionTimeout = "<%= Session.Timeout %>";
        setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000)
        function RedirectToWelcomePage() {
            alert("Session expired. You will be redirected to welcome page");
            window.location = "/HRMS/Login.aspx";
        }
</script>--%>
    <div class="ch-container">
        <div class="row">

            <div class="col-md-12">
                <div  align="center" class="row" style="width: 100%">
                    <h2>
                        <asp:Image ID="Img_logo" runat="server" ImageUrl="~/Images/logohrms.jpg"
                            Height="146px" Width="120px" />
                    </h2>
                </div>
            </div>

            <div class="col-md-12"  align="center">
                <div align="center" class="row" style="width: 80%">
                    <div class="well col-md-5 center login-box" style="box-shadow: inset 0 -3em 3em rgba(214, 210, 210, 0.52), 0 0 0 2px rgb(255,255,255), 0.3em 0.3em 1em rgba(0,0,0,0.3)"> 
                        <div class="alert alert-info">
                            ENTERPRISE EDITION
                        </div>
                        <form id="form1" runat="server">
                            <fieldset>
                                <div class="input-group input-group-lg">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    <input type="text" id="emp_userid" runat="server" class="form-control" placeholder="Username" />
                                </div>
                                <div class="clearfix"></div>
                                <br />

                                <div class="input-group input-group-lg">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-lock "></i></span>
                                    <input type="password" id="emp_password" onfocus="showPass()" runat="server" class="form-control" placeholder="Password" />
                                </div>
                                <div class="clearfix"></div>

                                <div class="clearfix">
                                    <asp:CheckBox ID="rememberme" runat="server" check="true" /><label class="remember" for="remember">Remember Me</label>
                                    <!-- <label class="remember" for="remember"><input type="checkbox" id="remember" /> Remember me</label> -->
                                </div>
                                <div class="clearfix">
                                    <asp:Label ID="lbl" runat="server" ForeColor="red" Font-Bold="True" CssClass="Error"></asp:Label>
                                </div>

                                <p class="center col-md-5">
                                    <asp:Button ID="btn_Employee" runat="server" class="btn btn-primary" Text="Login" OnClick="btn_Employee_Click1" />
                                </p>
                            </fieldset>
                        </form>
                    </div>
                </div>
            </div>
            <!--/row-->
        </div>
        <!--/fluid-row-->
       
    </div>
    <!--/.fluid-container-->
            <script src='<%= ResolveUrl("~/JQuery/jquery-1.10.2.js") %>'></script>
        <script src='<%= ResolveUrl("~/Libraries/jquery-1.9.1.min.js") %>'></script>
        <script src='<%= ResolveUrl("~/JQuery/jquery-ui.min.js") %>'></script>
        <script src='<%= ResolveUrl("~/Libraries/bootstrap.min.js") %>'></script>
     <script src='<%= ResolveUrl("~/Scripts/Base.js") %>'></script>
     <footer>
                <p> <a style="padding-top: 7%; float: right;">© 2015 hesperusinfo.com - All rights reserved.</a> </p>
            </footer>
</body>
</html>
