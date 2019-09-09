<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="WebFormLoginPage.aspx.cs" Inherits="TaskManagement.WebFormLoginPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="input-group col-md-4">
            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
            <asp:TextBox ID="TxtUserName" runat="server" class="form-control" name="email" placeholder="User Name"></asp:TextBox>
        </div><br />
        <div class="input-group col-md-4">
            <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
            <asp:TextBox ID="TxtPassword" runat="server" class="form-control" name="password" placeholder="Password" TextMode="Password"></asp:TextBox>
        </div>
        <div class="btn">
            <asp:Button ID="LoginButton" CssClass="btn btn-info" runat="server" Text="Login" OnClick="LoginButton_Click" />
        </div>
    </div>
</asp:Content>
