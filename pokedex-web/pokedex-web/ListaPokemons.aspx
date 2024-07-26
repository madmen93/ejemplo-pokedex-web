<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ListaPokemons.aspx.cs" Inherits="pokedex_web.ListaPokemons" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Lista Pokemons</h2>
    <asp:GridView ID="gdvPokemon" runat="server" DataKeyNames="Id" CssClass="table table-success table-striped" AutoGenerateColumns="false" 
        OnSelectedIndexChanged="gdvPokemon_SelectedIndexChanged" OnPageIndexChanging="gdvPokemon_PageIndexChanging" AllowPaging="true" PageSize="5">
        <Columns>
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Número" DataField="Numero" />
            <asp:BoundField HeaderText="Tipo" DataField="Tipo.Descripcion" />
            <asp:BoundField HeaderText="Debilidad" DataField="Debilidad.Descripcion" />
            <asp:CheckBoxField HeaderText="Estado" DataField="Activo"  />
            <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="✍" />
        </Columns>
    </asp:GridView>
    <a href="AltaPokemon.aspx" class="btn btn-primary">Nuevo</a>
</asp:Content>
