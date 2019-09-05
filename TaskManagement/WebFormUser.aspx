<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="WebFormUser.aspx.cs" Inherits="TaskManagement.WebFormUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-2">
               <h4> <asp:Label ID="UserNameLabel" runat="server" CssClass="label label-info" Text="User Name"></asp:Label></h4>
            </div>
            <div class="col-md-3">
                <asp:TextBox ID="UserNameTextBox" class="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
               <h4> <asp:Label ID="PasswordLabel" runat="server" CssClass="label label-info" Text="Password"></asp:Label></h4>
            </div>
            <div class="col-md-3">
                <asp:TextBox ID="PasswordTextBox" class="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
               <h4> <asp:Label ID="RoleIdLabel" runat="server" CssClass="label label-info" Text="Role Id"></asp:Label></h4>
            </div>
            <div class="col-md-3">
                <asp:TextBox ID="RoleIdTextBox" Type="number" class="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-4">
                <asp:Button ID="AddButton" CssClass="btn btn-info" runat="server" isPostBack="true" Text="ADD" OnClick="AddButton_Click" />
                 <asp:Button ID="UpdateButton" CssClass="btn btn-info" runat="server" Text="Update" />
                 <asp:Button ID="CancleButton" CssClass="btn btn-info" runat="server" Text="Cancle" />
                
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
               <h4> <asp:Label ID="sucessLabel" CssClass="label label-info" runat="server" Text="Label"></asp:Label></h4>
            </div>
        </div>
        <div class="row-no-gutters">
            <div class="col-md-5">
                <asp:GridView ID="UserLoginGridView" CssClass="table table-bordered table-hover table-responsive" runat="server"></asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
