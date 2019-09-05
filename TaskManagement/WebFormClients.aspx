<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="WebFormClients.aspx.cs" Inherits="TaskManagement.WebFormClients" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="form-group">

            <div class="row">
                <div class="col-md-2">
                    <h4>
                        <asp:Label ID="Label1" runat="server" Text="CLIENT NAME" class="label label-default"></asp:Label></h4>
                </div>

                <div class="col-md-2">
                    <asp:TextBox ID="ClientName" runat="server" class="form-control"></asp:TextBox>
                </div>

            </div>
            <div class="row">
                <div class="col-lg-4">
                    <asp:Button ID="ClientAdd" class="btn btn-primary" runat="server" Text="Add" OnClick="ClientAdd_Click" />
                    <asp:Button ID="ClientUpdate" class="btn btn-primary" runat="server" Text="Update" OnClick="ClientUpdate_Click" />
                    <asp:Button ID="ClientCancel" class="btn btn-primary" runat="server" Text="Cancel" OnClick="ClientCancel_Click" />
                   
                </div>
                <div class="col-lg-4">
                    <asp:Label ID="Sucess" class="alert alert-success" runat="server" Text="Label"></asp:Label>
                    <asp:Label ID="error" class="alert alert-danger" runat="server" Text="Label"></asp:Label>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-2">
                         <asp:Button ID="BtnLoad" class="btn btn-primary" runat="server" Text="Load" OnClick="BtnLoad_Click"  />
                      <asp:Button ID="ReloadBtn" class="btn btn-primary" runat="server" Text="Reload" OnClick="ReloadBtn_Click"  />
                
                         <asp:HiddenField ID="HiddenField1" runat="server" />
                
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-3">


                <asp:GridView ID="ClientData" CssClass="table table-bordered table-condensed" runat="server" OnSelectedIndexChanged="ClientData_SelectedIndexChanged" OnRowCommand="ClientData_RowCommand">
                    <Columns>

                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkEdit" runat="server" CausesValidation="false" CommandName="EditBtn" class="glyphicon glyphicon-pencil"></asp:LinkButton>
                                <asp:LinkButton ID="LinkDelete" runat="server" CausesValidation="false" CommandName="DelBtn" class="glyphicon glyphicon-trash" OnClientClick="return confirm('Are you sure delete this item?');"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
