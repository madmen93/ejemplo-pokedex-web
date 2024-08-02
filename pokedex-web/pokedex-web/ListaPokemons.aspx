<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ListaPokemons.aspx.cs" Inherits="pokedex_web.ListaPokemons" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" />
    <h2>Lista Pokemons</h2>
    <div class="row">
        <div class="col-10">
            <div class="mb-3 row">
                <label class="col-md-2 col-form-label">Filtrado rápido:</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txbFiltroRapido" CssClass="form-control" AutoPostBack="true" OnTextChanged="txbFiltroRapido_TextChanged" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-2 form-check">
                    <asp:CheckBox ID="chbFiltroAvanzado" Text="Filtro Avanzado" runat="server" CssClass="" OnCheckedChanged="chbFiltroAvanzado_CheckedChanged" AutoPostBack="true" />
                </div>
            </div>
            <%if (FiltroAvanzado)
                { %>
            <div class="g-3 row">
                <div class="col md-3">
                    <label for="ddlCampo" class="form-label">Campo</label>
                    <asp:DropDownList ID="ddlCampo" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged">
                        <asp:ListItem Text="Nombre" />
                        <asp:ListItem Text="Tipo" />
                        <asp:ListItem Text="Número" />
                    </asp:DropDownList>
                </div>
                <div class="col md-3">
                    <label for="ddlCriterio" class="form-label">Criterio</label>
                    <asp:DropDownList ID="ddlCriterio" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>
                <div class="col md-3">
                    <label for="tbxFiltro" class="form-label">Fitlro</label>
                    <asp:TextBox ID="tbxFiltro" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col md-3">
                    <label for="ddlEstado" class="form-label">Estado</label>
                    <asp:DropDownList ID="ddlEstado" CssClass="form-control" runat="server">
                        <asp:ListItem Text="Todos" />
                        <asp:ListItem Text="Activo" />
                        <asp:ListItem Text="Inactivo" />
                    </asp:DropDownList>
                </div>
            </div>
            <br />
            <asp:Button ID="btnBuscar" CssClass="btn btn-primary" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
            <asp:Button ID="btnRestablecer" CssClass="btn btn-success" runat="server" Text="Restablecer" OnClick="btnRestablecer_Click" />
        </div>

        <% } %>
    </div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <br />
            <asp:GridView ID="gdvPokemon" runat="server" DataKeyNames="Id" CssClass="table table-success table-striped" AutoGenerateColumns="false"
                OnSelectedIndexChanged="gdvPokemon_SelectedIndexChanged" OnPageIndexChanging="gdvPokemon_PageIndexChanging" AllowPaging="true" PageSize="5">
                <Columns>
                    <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                    <asp:BoundField HeaderText="Número" DataField="Numero" />
                    <asp:BoundField HeaderText="Tipo" DataField="Tipo.Descripcion" />
                    <asp:BoundField HeaderText="Debilidad" DataField="Debilidad.Descripcion" />
                    <asp:CheckBoxField HeaderText="Estado" DataField="Activo" />
                    <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="✍" />
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row">
        <div class="col">
            <a href="AltaPokemon.aspx" class="btn btn-primary">Nuevo</a>
        </div>
    </div>
</asp:Content>
