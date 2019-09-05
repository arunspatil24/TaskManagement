<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="WebFormTaskDetails.aspx.cs" Inherits="TaskManagement.WebFormTaskDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="sucess" runat="server" Text="Label"></asp:Label>
                <h4>
                    <asp:Label Text="Client" runat="server" CssClass="label label-info"></asp:Label></h4>
            </div>
            <div class="col-md-4">
                <asp:DropDownList ID="ClientDropDownList" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ClientDropDownList_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <h4>
                    <asp:Label Text="Project" runat="server" CssClass="label label-info"></asp:Label></h4>
            </div>
            <div class="col-md-4">
                <asp:DropDownList ID="ProjectDropDownList" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ProjectDropDownList_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <h4>
                    <asp:Label Text="Work" runat="server" CssClass="label label-info"></asp:Label></h4>
            </div>
            <div class="col-md-4">
                <asp:DropDownList ID="WorkDropDownList" class="form-control" runat="server" AutoPostBack="True"></asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <h4>
                    <asp:Label Text="Date" runat="server" CssClass="label label-info"></asp:Label></h4>
            </div>
            <div class="col-md-4">
                <asp:Calendar ID="CDate" runat="server" SelectedDate="<%# DateTime.Today %>"></asp:Calendar>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <h4>
                    <asp:Label Text="Work Hours" runat="server" CssClass="label label-info"></asp:Label></h4>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="WorkHoursTextBox" Type="number" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <h4>
                    <asp:Label Text="Work Details" runat="server" CssClass="label label-info"></asp:Label></h4>
            </div>
            <div class="col-md-4">
                <textarea id="WorkDetailsTextArea" runat="server" class="form-control" cols="20" rows="2"></textarea>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <asp:Button ID="ADDButton" runat="server" CssClass="btn btn-info" Text="ADD" OnClick="ADDButton_Click" />
                <asp:Button ID="CancleButton" runat="server" CssClass="btn btn-info" Text="Cancle" />
                <asp:Button ID="UpdateButton" runat="server" CssClass="btn btn-info" Text="Update" />
            </div>
        </div>
    </div>
    <div class="container">
        <div class="row-no-gutters">
            <div class="col">
                <asp:GridView ID="TaskDetailsGridView" CssClass="table table-responsive table-bordered table-hover" runat="server" OnRowCommand="TaskDetailsGridView_RowCommand">
                    <Columns>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="EditButton" runat="server" CausesValidation="false" CommandName="EditCommand" class="glyphicon glyphicon-pencil" Text="Edit"></asp:LinkButton>
                                 <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="false" CommandName="DeleteCommand" Text="Delete" class="glyphicon glyphicon-trash" OnClientClick="return confirm('Are You Sure you want to delete this iteam?');"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
