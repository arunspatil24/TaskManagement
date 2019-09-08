<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="WebFormRole.aspx.cs" Inherits="TaskManagement.WebFormRole" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-2">
               <h4> <asp:Label ID="RoleLabel" runat="server" CssClass="label label-info" Text="Role Name"></asp:Label></h4>
            </div>
            <div class="col-md-3">
                <asp:TextBox ID="RoleNameTextBox" class="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
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
                <asp:GridView ID="RoleGridView" CssClass="table table-bordered table-hover table-responsive" runat="server" OnRowCommand="RoleGridView_RowCommand">
                    <Columns>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="EditLinkButton" runat="server" CausesValidation="false" class="glyphicon glyphicon-pencil" CommandName="EditCommand" Text="Edit"></asp:LinkButton>
                                <asp:LinkButton ID="DeleteLinkButton" runat="server" CausesValidation="false" class="glyphicon glyphicon-trash" OnClientClick="return confirm('Are You Sure you want to delete this iteam?');" CommandName="DeleteCommand" Text="Delete"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
