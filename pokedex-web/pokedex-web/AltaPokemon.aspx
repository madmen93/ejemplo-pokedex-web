<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AltaPokemon.aspx.cs" Inherits="pokedex_web.AltaPokemon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="scriptmng" runat="server" />
    <asp:Label runat="server" Text="Nuevo Pokemon" ID="lbTitulo" CssClass="fs-2"></asp:Label>
    <div class="row">
        <div class="col">
            <div class="mb-3 row">
                <label for="txbId" class="col-sm-2 col-form-label">Id:</label>
                <div class="col-sm-10">
                    <asp:TextBox ID="txbId" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="mb-3 row">
                <label for="txbNombre" class="col-sm-2 col-form-label">Nombre:</label>
                <div class="col-sm-10">
                    <asp:TextBox ID="txbNombre" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="mb-3 row">
                <label for="txbNumero" class="col-sm-2 col-form-label">Número:</label>
                <div class="col-sm-10">
                    <asp:TextBox ID="txbNumero" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="mb-3 row">
                <label for="ddlTipo" class="col-sm-2 col-form-label">Tipo:</label>
                <div class="col-sm-10">
                    <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>
            </div>
            <div class="mb-3 row">
                <label for="ddlDebilidad" class="col-sm-2 col-form-label">Debilidad:</label>
                <div class="col-sm-10">
                    <asp:DropDownList ID="ddlDebilidad" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="col">
            <div class="mb-3 row">
                <label for="txbDescripcion" class="col-sm-2 col-form-label">Descripción:</label>
                <div class="col-sm-10">
                    <asp:TextBox ID="txbDescripcion" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="mb-3 row">
                        <label for="txbUrlImagen" class="col-sm-2 col-form-label">Url imagen:</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txbUrlImagen" CssClass="form-control" runat="server" OnTextChanged="txbUrlImagen_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="mb-3 row">
                        <asp:Image ID="imgPokemon" runat="server" ImageUrl="https://www.globalwatersolutions.com/wp-content/uploads/placeholder-4.png" Width="60%" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-primary" OnClick="btnAceptar_Click" />
    <a href="ListaPokemons.aspx">Cancelar</a>
</asp:Content>
