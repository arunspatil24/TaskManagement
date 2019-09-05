<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="WebFormProject.aspx.cs" Inherits="TaskManagement.Pages.WebFormProject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .mandatory::after
        {
            content:'*';
            color:red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="form-group">

            <div class="row">
                <div class="col-md-1">
                    <h5>
                        <asp:Label ID="Label1" runat="server" Text="Client Name" class="mandatory"></asp:Label>

                    </h5>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ClientName" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ClientName_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                    <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                </div>
            </div>
            <div class="row ">
                <div class="col-md-1">
                    <asp:Label ID="Label2" runat="server" Text="Project Name" class="label label-default"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="ProjectName" class="form-control" runat="server"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                </br>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <asp:Button ID="Add" class="btn btn-primary" runat="server" Text="Add Project" OnClick="Add_Click" />
                    <asp:Button ID="ProjectUpdate" class="btn btn-primary" runat="server" Text="Update" OnClick="ProjectUpdate_Click" />
                    <asp:Button ID="Cancel" class="btn btn-primary" runat="server" Text="Cancel" OnClick="Cancel_Click" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <h4><asp:Label ID="Sucess" CssClass="label label-info" runat="server" Text="Label"></asp:Label></h4>
                </div>
            </div>
        </div>
        <div class="row row-no-gutters">
            <div class="col-md-4">
                <asp:GridView ID="ProjectGridView" CssClass="table table-bordered table-condensed" runat="server" OnRowCommand="ProjectGridView_RowCommand" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="ProjectEdit" runat="server" Text="Edit" CommandName="ProjectEdit" class="glyphicon glyphicon-pencil"></asp:LinkButton>
                                <asp:LinkButton ID="ProjectDelete" runat="server" Text="Delete" CommandName="ProjectDelete" class="glyphicon glyphicon-trash" OnClientClick="return confirm('Are You Sure you want to delete this iteam?');"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                    <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FFF1D4" />
                    <SortedAscendingHeaderStyle BackColor="#B95C30" />
                    <SortedDescendingCellStyle BackColor="#F1E5CE" />
                    <SortedDescendingHeaderStyle BackColor="#93451F" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
