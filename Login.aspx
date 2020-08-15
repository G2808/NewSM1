<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="NewSM1.Login" %>

<!DOCTYPE html>
<html lang="en">
    <head>
        <meta charset="utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
        <meta name="description" content="" />
        <meta name="author" content="" />
        <title>Page Title - SB Admin</title>
        <link href="../css/styles.css" rel="stylesheet" />
        <script src="../js/all.min.js"></script>
       <style>
                body {
                  background-image: url("images/sm1.jpg");
                  background-repeat: no-repeat;
                  background-attachment: fixed;
                  background-size: cover;
                  background-position: center center;
                }
        </style>
    </head>
    <body class="img-fluid:../images/HumanResource.jpg">
        <form runat="server">
               <div id="layoutAuthentication">
            <div id="layoutAuthentication_content">
                <main>
                    <div class="container-fluid">
                        <div class="row justify-content-end" style="margin-top:6%">
                            <div class="col-lg-3">
                                <div class="card shadow-lg border-1 rounded">
                                    <div class="card-header bg-info"><h4 class="text-center font-weight-light py-1"><i class="fa fa-key"></i><b>  Login</b></h4></div>
                                    <div class="card-body">
                                            <div class="form-group">
                                                <label class="mb-1" for="inputEmailAddress">User Details</label>
                                                <asp:TextBox ID="txtLogin" runat="server" CssClass="form-control py-3" placeholder="Enter Employee code" ></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label class="mb-1" for="inputPassword">Password</label>
                                                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control p-3" placeholder="Enter password" TextMode="Password" ></asp:TextBox>
                                            </div>
                                            <div class="row">
                                                <div class="col-12" style="text-align:center">
                                                    <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group d-flex align-items-center justify-content-between mt-4 mb-0">
                                                <a class="small" href="password.html">Forgot Password?</a>
                                                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary" OnClick="btnLogin_Click" />
                                            </div>
                                    </div>
                                    <div class="card-footer text-center">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </main>
            </div>
            <div id="layoutAuthentication_footer" style="margin-top:1%">
                <footer class="py-3 bg-light mt-auto">
                    <div class="container-fluid">
                        <div class="d-flex align-items-center justify-content-between small">
                            <div class="text-muted">Copyright &copy; SalesManage 2020</div>
                            <div>
                                <a href="#">Privacy Policy</a>
                                &middot;
                                <a href="#">Terms &amp; Conditions</a>
                            </div>
                        </div>
                    </div>
                </footer>
            </div>
        </div>
        </form>
        <script src="../js/jquery-3.5.1.min.js"></script>
        <script src="../js/bootstrap.bundle.min.js"></script>
        <script src="../js/scripts.js"></script>
    </body>
</html>

