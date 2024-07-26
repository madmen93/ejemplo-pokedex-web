using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;

namespace pokedex_web
{
    public partial class ListaPokemons : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PokemonNegocio negocio = new PokemonNegocio();
            Session.Add("listaPokemon", negocio.listarconSP());
            gdvPokemon.DataSource = Session["listaPokemon"];
            gdvPokemon.DataBind();
        }

        protected void gdvPokemon_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = gdvPokemon.SelectedDataKey.Value.ToString();
            Response.Redirect("AltaPokemon.aspx?id=" + id, false);
        }

        protected void gdvPokemon_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvPokemon.PageIndex = e.NewPageIndex;
            gdvPokemon.DataBind();
        }

        protected void txbFiltroRápido_TextChanged(object sender, EventArgs e)
        {
            List<Pokemon> lista = (List<Pokemon>)Session["listaPokemon"];
            List<Pokemon> listaFiltrada = lista.FindAll(x => x.Nombre.ToUpper().Contains(txbFiltroRápido.Text.ToUpper()));
            gdvPokemon.DataSource = listaFiltrada;
            gdvPokemon.DataBind();
        }
    }
}