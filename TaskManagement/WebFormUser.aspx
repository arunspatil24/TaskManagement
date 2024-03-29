﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="WebFormUser.aspx.cs" Inherits="TaskManagement.WebFormUser" %>
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
                <asp:RequiredFieldValidator ID="RequiredFieldValidUserName" runat="server"  ErrorMessage="please enter the valid user name" ControlToValidate="UserNameTextBox" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
               <h4> <asp:Label ID="PasswordLabel" runat="server" CssClass="label label-info" Text="Password"></asp:Label></h4>
            </div>
            <div class="col-md-3">
                <asp:TextBox ID="PasswordTextBox" class="form-control" runat="server" TextMode="Password" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidPassword" runat="server" ErrorMessage="Please provide correct password" ControlToValidate="PasswordTextBox" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
               <h4> <asp:Label ID="RoleIdLabel" runat="server" CssClass="label label-info" Text="Role Id"></asp:Label></h4>
            </div>
            <div class="col-md-3">
                <asp:DropDownList ID="RoleDropDownList" CssClass="form-control" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidDrpDwnRole" runat="server" ErrorMessage="Select the role" ControlToValidate="RoleDropDownList" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-4">
                <asp:Button ID="AddButton" CssClass="btn btn-info" runat="server" isPostBack="true" Text="ADD" OnClick="AddButton_Click" />
                 <asp:Button ID="UpdateButton" CssClass="btn btn-info" runat="server" Text="Update" OnClick="UpdateButton_Click" />
                 <asp:Button ID="CancleButton" CssClass="btn btn-info" runat="server" Text="Cancle" OnClick="CancleButton_Click" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
               <h4> <asp:Label ID="sucessLabel" CssClass="label label-info" runat="server" Text="Label"></asp:Label></h4>
            </div>
        </div>
        <div class="row-no-gutters">
            <div class="col-md-5">
                <asp:GridView ID="UserLoginGridView" CssClass="table table-bordered table-hover table-responsive" runat="server" OnRowCommand="UserLoginGridView_RowCommand" >
                    <Columns>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="EditLinkButton" runat="server" CausesValidation="false" CommandName="EditCommand" class="glyphicon glyphicon-pencil" ></asp:LinkButton>
                                <asp:LinkButton ID="DeleteLinkButton" runat="server" CausesValidation="false" CommandName="DeleteCommand" class="glyphicon glyphicon-trash" OnClientClick="return confirm('Are You Sure you want to delete this iteam?');" ></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
