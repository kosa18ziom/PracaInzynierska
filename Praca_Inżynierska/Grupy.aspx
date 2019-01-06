<%@ Page Title="Grupy" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Grupy.aspx.cs" Inherits="Praca_Inżynierska.Grupy" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="dvGrid"  style="padding: 10px; width: 450px">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" 
            DataKeyNames="Id_Grupy" OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit" 
            OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting" EmptyDataText="No records has been added."
            Width="450" CssClass="table table-striped ">
            <Columns>
                <asp:TemplateField HeaderText="Nazwa Grupy" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("Nazwa_Grupy") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtName" runat="server" Text='<%# Eval("Nazwa_Grupy") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle Width="150px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ilość Studentów" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblIlosc" runat="server" Text='<%# Eval("Ilosc_Studentow") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtIlosc" runat="server" Text='<%# Eval("Ilosc_Studentow") %>' Width="40"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle Width="150px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Stopień Studiów" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblStopien" runat="server" Text='<%# Eval("Stopien_Studiow") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtStopien" runat="server" Text='<%# Eval("Stopien_Studiow") %>' Width="40"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle Width="150px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Semestr" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblSemestr" runat="server" Text='<%# Eval("Semestr") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSemestr" runat="server" Text='<%# Eval("Semestr") %>' Width="40"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle Width="150px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Wydział" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblWydzial" runat="server" Text='<%# Eval("Wydzial") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtWydzial" runat="server" Text='<%# Eval("Wydzial") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle Width="150px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Kierunek" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblKierunek" runat="server" Text='<%# Eval("Kierunek") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtKierunek" runat="server" Text='<%# Eval("Kierunek") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle Width="150px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Specjalizacja" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblSpecjalizacja" runat="server" Text='<%# Eval("Specjalizacja") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSpecjalizacja" runat="server" Text='<%# Eval("Specjalizacja") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle Width="150px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Rodzaj Studiów" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblRodzaj" runat="server" Text='<%# Eval("Rodzaj_Studiow") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRodzaj" runat="server" Text='<%# Eval("Rodzaj_Studiow") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle Width="150px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Plan Studiów" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblPlanStudiow" runat="server" Text='<%# Eval("nazwa") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID = "ddlPlan" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="nazwa" DataValueField="Id_Planu"></asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [Plan_Studiow]"></asp:SqlDataSource>
                    </EditItemTemplate>
                    <ItemStyle Width="150px" />
                </asp:TemplateField>
                <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true"
                    ItemStyle-Width="150" >
                <ItemStyle Width="150px" />
                </asp:CommandField>
            </Columns>
        </asp:GridView>
        <div>
                    Nazwa Grupy:<br />
                    <asp:TextBox ID="txtNazwaGrupy" runat="server" Width="140" />
                </td>
                <td style="width: 50px">
                    <asp:Button ID="btnAdd" runat="server" Text="Dodaj" OnClick="Insert" />
              </div>
    </ContentTemplate>
</asp:UpdatePanel>
</div>
    <div>
    Agregacja potoków

        </div>
</asp:Content>