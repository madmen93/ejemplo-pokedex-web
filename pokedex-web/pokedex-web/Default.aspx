<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="pokedex_web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Bienvenido(a) a tu Pokedex</h2>
    <p>Tu página favorita para encontrar información sobre pokemons.</p>
    <div class="row row-cols-1 row-cols-md-3 g-4">
        <%--<% foreach (dominio.Pokemon poke in ListaPokemon)
            { %>
        <div class="col">
            <div class="card">
                <img src="<%: poke.UrlImagen %>" class="card-img-top" alt="...">
                <div class="card-body">
                    <h5 class="card-title"><%: poke.Nombre %></h5>
                    <p class="card-text"><%: poke.Descripcion %></p>
                    <div class="badge text-bg-success text-wrap" style="width: 6rem;">
                        <%: poke.Tipo.Descripcion %>
                    </div>
                    <br />
                    <a href="DetallePokemon.aspx?id=<%: poke.Id %>">Ver detalle</a>
                </div>
            </div>
        </div>
        <% } %>--%>
        <asp:Repeater ID="repRepitidor" runat="server">
            <ItemTemplate>
                <div class="col">
                    <div class="card">
                        <img src="<%#Eval("UrlImagen") %>" class="card-img-top" alt="...">
                        <div class="card-body">
                            <h5 class="card-title"><%#Eval("Nombre") %></h5>
                            <p class="card-text"><%#Eval("Descripcion") %></p>
                            <div class="badge text-bg-success text-wrap" style="width: 6rem;">
                                <%#Eval("Tipo.Descripcion") %>
                            </div>
                            <br />
                            <a href="DetallePokemon.aspx?id=<%#Eval("Id") %>">Ver detalle</a>
                            <asp:Button ID="btnEjemplo" runat="server" Text="Botón Ejemplo" CssClass="btn btn-primary" CommandArgument='<%#Eval("Id") %>' CommandName="PokemonId" OnClick="btnEjemplo_Click" />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

    </div>
</asp:Content>
