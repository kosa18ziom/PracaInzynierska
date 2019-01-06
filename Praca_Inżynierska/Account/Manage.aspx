<%@ Page Title="Panel Użytkownika" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="Praca_Inżynierska.Account.Manage" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <dt>Zalogowany Użytkownik: <%: Context.User.Identity.GetUserName()  %></dt>
    <dt>Imię: <asp:Label ID="Imie" runat="server"></asp:Label></dt>
    <dt>Nazwisko: <asp:Label ID="Nazwisko" runat="server"></asp:Label></dt>
    <dt>Pensum: <asp:Label ID="Pensum" runat="server"></asp:Label></dt>
    <dt>Zaplanowane godziny pracy: <asp:Label ID="Zaplanowane" runat="server"></asp:Label></dt>
    <div>
        <asp:PlaceHolder runat="server" ID="successMessage" Visible="false" ViewStateMode="Disabled">
            <p class="text-success"><%: SuccessMessage %></p>
        </asp:PlaceHolder>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="form-horizontal">
                <hr />
                <dl class="dl-horizontal">
                    <dt>Zmień Hasło:</dt>
                    <dd>
                        <asp:HyperLink NavigateUrl="/Account/ManagePassword" Text="[Zmień]" Visible="false" ID="ChangePassword" runat="server" />
                        <asp:HyperLink NavigateUrl="/Account/ManagePassword" Text="[Create]" Visible="false" ID="CreatePassword" runat="server" />
                    </dd>
                    
                    </dd>
                </dl>
            </div>
        </div>
    </div>

</asp:Content>
