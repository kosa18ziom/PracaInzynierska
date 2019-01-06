<%@ Page Title="Planowanie" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Planowanie.aspx.cs" Inherits="Praca_Inżynierska.Planowanie" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
        

    <div><asp:DropDownList ID = "ddlCustomers" runat="server" class="btn btn-default dropdown-toggle"  AutoPostBack="true" OnSelectedIndexChanged="ddlCustomers_SelectedIndexChanged">

</asp:DropDownList>
                <asp:Panel ID=Planuj runat="server" Visible="false">
        <asp:DropDownList ID="ddlgrupy" runat="server" class="btn btn-default dropdown-toggle"  AutoPostBack="true"  OnSelectedIndexChanged="ddlgrupy_SelectedIndexChanged">
                </asp:DropDownList>
 </div>
                  
       <HeaderTemplate>
        <table class="table" cellspacing="0" rules="all" border="1">
            <thead class="thead-dark">
            <tr>
                <th scope="col" style="width: 80px">
                    Typ Zajęć
                </th>
                <th scope="col" style="width: 120px">
                    Wybór Nauczyciela
                </th>
                <th scope="col" style="width: 120px">
                    Ilość godzin
                </th>
                <th scope="col" style="width: 100px">
                    Zapisz
                </th>
                <th scope="col" style="width: 100px">
                    Status
                </th>
            </tr>
                </thead>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td>
                <asp:Label ID="lblCustomerId" runat="server" Text='Wykład' />
            </td>
            <td>
                <asp:DropDownList ID="ddlNauczyciel" runat="server" DataSourceID="SqlDataSource3" DataTextField="ImieNazwisko" DataValueField="Id" OnSelectedIndexChanged="ddlNauczyciel_SelectedIndexChanged">
                </asp:DropDownList>
                     <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [AspNetUsers] WHERE Rola=1"></asp:SqlDataSource> 
            </td>
            <td>
                <asp:Label ID="wykladh" runat="server" />
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" class="btn btn-success" Text="Zapisz" OnClick="Button1_Click"></asp:Button>
            </td>
            <td>
                <asp:Label ID="Zaplanowano" runat="server" Text="Zaplanowano" Visible="false"></asp:Label>
                <asp:Label ID="Niezaplanowane" runat="server" Text="Niezaplanowane" Visible="true"></asp:Label>

            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text='Ćwiczenia' />
            </td>
            <td>
                <asp:DropDownList ID="ddlNauczyciel1" runat="server" DataSourceID="SqlDataSource1" DataTextField="ImieNazwisko" DataValueField="Id" OnSelectedIndexChanged="ddlNauczyciel1_SelectedIndexChanged">
                </asp:DropDownList>
                     <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [AspNetUsers]"></asp:SqlDataSource> 
            </td>
            <td>
                <asp:Label ID="cwh" runat="server" />
            </td>
            <td>
                <asp:Button ID="Button2" runat="server" class="btn btn-success" Text="Zapisz" OnClick="Button2_Click"></asp:Button>
            </td>
            <td>
                <asp:Label ID="Zaplanowano1" runat="server" Text="Zaplanowano" Visible="false"></asp:Label>
                <asp:Label ID="Niezaplanowane1" runat="server" Text="Niezaplanowane" Visible="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text='Laboratorium' />
            </td>
            <td>
                <asp:DropDownList ID="ddlNauczyciel2" runat="server" DataSourceID="SqlDataSource2" DataTextField="ImieNazwisko" DataValueField="Id" OnSelectedIndexChanged="ddlNauczyciel2_SelectedIndexChanged">
                </asp:DropDownList>
                     <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [AspNetUsers]"></asp:SqlDataSource> 
            </td>
            <td>
                <asp:Label ID="labh" runat="server" />
            </td>
            <td>
                <asp:Button ID="Button3" runat="server" class="btn btn-success" Text="Zapisz" OnClick="Button3_Click"></asp:Button>
            </td>
            <td>
                <asp:Label ID="Zaplanowano2" runat="server" Text="Zaplanowano" Visible="false"></asp:Label>
                <asp:Label ID="Niezaplanowane2" runat="server" Text="Niezaplanowane" Visible="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text='Projekt' />
            </td>
            <td>
                <asp:DropDownList ID="ddlNauczyciel3" runat="server" DataSourceID="SqlDataSource4" DataTextField="ImieNazwisko" DataValueField="Id" OnSelectedIndexChanged="ddlNauczyciel3_SelectedIndexChanged">
                </asp:DropDownList>
                     <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [AspNetUsers]"></asp:SqlDataSource> 
            </td>
            <td>
                <asp:Label ID="projh" runat="server" />
            </td>
            <td>
                <asp:Button ID="Button4" class="btn btn-success"  runat="server" Text="Zapisz" OnClick="Button4_Click"></asp:Button>
            </td>
            <td>
                <asp:Label ID="Zaplanowano3" runat="server" Text="Zaplanowano" Visible="false"></asp:Label>
                <asp:Label ID="Niezaplanowane3" runat="server" Text="Niezaplanowane" Visible="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text='Seminarium' />
            </td>
            <td>
                <asp:DropDownList ID="ddlNauczyciel4" runat="server" DataSourceID="SqlDataSource5" DataTextField="ImieNazwisko" DataValueField="Id" OnSelectedIndexChanged="ddlNauczyciel4_SelectedIndexChanged">
                </asp:DropDownList>
                     <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [AspNetUsers]"></asp:SqlDataSource> 
            </td>
            <td>
                <asp:Label ID="semh" runat="server" />
            </td>
            <td>
                <asp:Button ID="Button5" runat="server" class="btn btn-success" Text="Zapisz" OnClick="Button5_Click"></asp:Button>
            </td>
            <td>
                <asp:Label ID="Zaplanowano4" runat="server" Text="Zaplanowano" Visible="false"></asp:Label>
                <asp:Label ID="Niezaplanowane4" runat="server" Text="Niezaplanowane" Visible="true"></asp:Label>
            </td>
        </tr>
        </table>
    </ItemTemplate>

    
    </asp:Panel>
    </div>
</asp:Content>
