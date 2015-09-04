<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Hangman.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Assignment 4 | Hangman</title>
    <link rel="stylesheet" href="CSS/bootstrap.min.css" />
    <link rel="stylesheet" href="CSS/hangman.css" />
</head>
<body>
    <nav class="navbar navbar-inverse navbar-fixed-top">
      <div class="container">
        <div class="navbar-header">
          <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
          </button>
          <a class="navbar-brand" href="#">Hangman</a>
        </div>
        <div id="navbar" class="collapse navbar-collapse">
          <ul class="nav navbar-nav">
            <li class="active"><a href="#">Home</a></li>
          </ul>
        </div><!--/.nav-collapse -->
      </div>
    </nav>

    <div class="row">
        <form id="form1" runat="server">
        <div class="col-md-3 col-md-offset-1">
            <asp:RadioButtonList ID="rblist" runat="server">
                <asp:ListItem Text="Easy" Selected="True" Value="easy" />
                <asp:ListItem Text="Medium" Value="medium" />
                <asp:ListItem Text="Hard" Value="hard" />
            </asp:RadioButtonList>
            <asp:Button ID="difficultyButton" class="btn btn-sm btn-primary" Text=" Select Difficulty " OnClick="change_Difficulty" runat="server" />
        </div>
        <div class="col-md-3 col-md-offset-1">
            <asp:Image ID="hangman" runat="server" ImageUrl="Images/0.gif" />
            <asp:Label ID="hide" runat="server" />
            <br />
            <asp:Label ID="round" runat="server" />
        </div>
        <div class="col-md-3 col-md-offset-1">
            <asp:TextBox ID="userInput" placeholder="Guess Here..." runat="server" />
            <asp:Button text=" Submit " class="btn btn-sm btn-primary" ID="submit" OnClick="submit_Click" runat="server" />
            <br />
            <asp:Label ID="statusLabel" runat="server" />
        </div>
        </form>
    </div>
    <script src="JS/bootstrap.min.js"></script>
    <script src="JS/jquery-1.11.3.min.js"></script>
</body>
</html>
