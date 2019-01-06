<%@ Page Title="Import" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Import.aspx.cs" Inherits="Praca_Inżynierska.Import" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <h2><%: Title %></h2>
    <div>Przedmioty
    <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="true" />
<asp:Button Text="Upload" OnClick = "Upload" runat="server" />
        </div>
    <div>Grupy
    <asp:FileUpload ID="FileUpload2" runat="server" />
<asp:Button Text="Upload" OnClick = "Upload2" runat="server" />
        </div>
    <div>
    Nauczyciele
    <asp:FileUpload ID="FileUpload4" runat="server" />
<asp:Button Text="Upload" OnClick = "Upload4" runat="server" />
        </div>

    </asp:Content>
