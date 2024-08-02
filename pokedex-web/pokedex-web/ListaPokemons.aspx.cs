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
        public bool FiltroAvanzado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            FiltroAvanzado = chbFiltroAvanzado.Checked;
            if (!IsPostBack)
            {
                PokemonNegocio negocio = new PokemonNegocio();
                Session.Add("listaPokemon", negocio.listarconSP());
                gdvPokemon.DataSource = Session["listaPokemon"];
                gdvPokemon.DataBind();

            }
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

        protected void txbFiltroRapido_TextChanged(object sender, EventArgs e)
        {
            List<Pokemon> lista = (List<Pokemon>)Session["listaPokemon"];
            List<Pokemon> listaFiltrada = lista.FindAll(x => x.Nombre.ToUpper().Contains(txbFiltroRapido.Text.ToUpper()));
            gdvPokemon.DataSource = listaFiltrada;
            gdvPokemon.DataBind();
        }

        protected void chbFiltroAvanzado_CheckedChanged(object sender, EventArgs e)
        {
            FiltroAvanzado = chbFiltroAvanzado.Checked;
            txbFiltroRapido.Enabled = !FiltroAvanzado;
        }

        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCriterio.Items.Clear();
            if(ddlCampo.SelectedItem.ToString() == "Número")
            {
                ddlCriterio.Items.Add("Igual a");
                ddlCriterio.Items.Add("Mayor a");
                ddlCriterio.Items.Add("Menor a");
            }
            else
            {
                ddlCriterio.Items.Add("Contiene");
                ddlCriterio.Items.Add("Comienza con");
                ddlCriterio.Items.Add("Termina con");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                PokemonNegocio negocio = new PokemonNegocio();
                gdvPokemon.DataSource = negocio.filtrar(ddlCampo.SelectedItem.ToString(), ddlCriterio.SelectedItem.ToString(), tbxFiltro.Text, ddlEstado.SelectedItem.ToString());
                gdvPokemon.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }

        protected void btnRestablecer_Click(object sender, EventArgs e)
        {
            try
            {
                PokemonNegocio negocio = new PokemonNegocio();
                gdvPokemon.DataSource = negocio.listarconSP();
                gdvPokemon.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }
    }
}