<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Production_Costing_Software.Login" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Log in</title>

    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700&display=fallback">
    <link rel="stylesheet" href="plugins/fontawesome-free/css/all.min.css" />
    <link rel="stylesheet" href="plugins/icheck-bootstrap/icheck-bootstrap.min.css" />
    <link rel="stylesheet" href="dist/css/adminlte.min.css" />

</head>

<body class="hold-transition login-page">
    <div class="login-box" style="margin-top:-200px!important">
        <div class="login-logo" style="margin-bottom: 0px
!important">
            <%--<img src="content/images/emen logo.png" alt="EMEN RETAIL INFOTECH PVT.LTD" class="brand-image" style="opacity: .8; height: 25px; width: 100px">--%>
            <h1 class="text-success">PCS</h1>
        </div>
        <div>
            <%--<p class="login-box-msg">RETAIL INFOTECH PVT.LTD</p>--%>
        </div>
        <form runat="server">
          
                    <div class="card">
                        <div class="card-body login-card-body">
                            <p class="login-box-msg">Sign in to start your session</p>

                            <asp:ValidationSummary ID="ValidationSummary1" CssClass="text-danger" runat="server" ValidationGroup="g1" />

                            <div id="dverror" runat="server" style="display:none" class="login-box-msg text-danger">
                                Invalid UserInfo
                            </div>

                            <div class="input-group mb-3">
                                <asp:TextBox ID="txtusername" runat="server" placeholder="UserName" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rf_txtusername" runat="server" ControlToValidate="txtusername" Display="None" ValidationGroup="g1" ErrorMessage="Enter UserName" />
                                <div class="input-group-append">
                                    <div class="input-group-text">
                                        <span class="fas fa-envelope"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="input-group mb-3">
                                <asp:TextBox ID="txtpassword" runat="server" TextMode="Password" placeholder="Password" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rf_txtpassword" runat="server" ControlToValidate="txtpassword" Display="None" ValidationGroup="g1" ErrorMessage="Enter Password" />
                                <div class="input-group-append">
                                    <div class="input-group-text">
                                        <span class="fas fa-lock"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-4">                                   
                                    <asp:Button ID="btnlogin" runat="server" Text="LogIn" class="btn btn-primary btn-block" ValidationGroup="g1" CausesValidation="true" OnClick="btnlogin_Click" />
                                </div>

                            </div>
                        </div>
                    </div>
             
        </form>
    </div>


    <script src="plugins/jquery/jquery.min.js"></script>
    <script src="plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="dist/js/adminlte.min.js?v=3.2.0"></script>
</body>
</html>

