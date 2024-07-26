using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;

namespace pokedex_web
{
    public partial class ListaPokemons : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PokemonNegocio negocio = new PokemonNegocio();
            gdvPokemon.DataSource = negocio.listarconSP();
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
    }
}