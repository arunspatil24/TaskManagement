<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="WebFormWork.aspx.cs" Inherits="TaskManagement.Pages.WebFormWork" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="ClientName" runat="server" class="label label-info" Text="Client Name"></asp:Label>
            </div>
            <div class="col-md-3">
                <asp:DropDownList ID="ClientNameDropDown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ClientNameDropDown_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="ProjectName" runat="server" class="label label-info" Text="Project Name"></asp:Label>
            </div>
            <div class="col-md-3">
                <asp:DropDownList ID="ProjectNameDropDown" runat="server">
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <br />
    <div class="container">
        <div class="row">
            <div class="col-md-3">
                <asp:TextBox ID="Work" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-5">
                 <asp:Button ID="AddButton" runat="server" class="btn btn-info" Text="Add" OnClick="AddButton_Click" />
                <asp:Button ID="Cancel" runat="server" class="btn btn-info" Text="Cancel" OnClick="Cancel_Click" />
                <asp:Button ID="Update" runat="server" class="btn btn-info" Text="Update" OnClick="Update_Click" />
            </div>
        </div>
        <div class="row">
            <div class="col-lg-4">
                <asp:Label ID="Sucess" runat="server" class="label label-info" Text="Label"></asp:Label>
            </div>
        </div>
    </div>
    
    <div class="container">
        <div class="row-no-gutters">
            <asp:GridView ID="WorkGrid" CssClass="table table-bordered table-condensed" runat="server" OnRowCommand="WorkGrid_RowCommand" >
                <Columns>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="EditWork" runat="server" CausesValidation="false" CommandName="EditWork" class="glyphicon glyphicon-pencil" Text="Edit"></asp:LinkButton>
                            <asp:LinkButton ID="DeleteWork" runat="server" CausesValidation="false" CommandName="DeleteWork" class="glyphicon glyphicon-trash" OnClientClick="return confirm('Are You Sure you want to delete this iteam?');" Text="Delete"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
