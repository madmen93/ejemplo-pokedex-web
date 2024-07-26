using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace pokedex_web
{
    public partial class AltaPokemon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txbId.Enabled = false;
            try
            {
                if (!IsPostBack)
                {
                    ElementoNegocio negocio = new ElementoNegocio();
                    List<Elemento> listaTipo = negocio.listar();
                    ddlTipo.DataSource = listaTipo;
                    ddlTipo.DataValueField = "Id";
                    ddlTipo.DataTextField = "Descripcion";
                    ddlTipo.DataBind();

                    ddlDebilidad.DataSource = listaTipo;
                    ddlDebilidad.DataValueField = "Id";
                    ddlDebilidad.DataTextField = "Descripcion";
                    ddlDebilidad.DataBind();
                }
            }
            catch (Exception ex)
            {

                Session.Add("error", ex);
                throw;
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Pokemon nuevo = new Pokemon();
                PokemonNegocio negocio = new PokemonNegocio();

                nuevo.Nombre = txbNombre.Text;
                nuevo.Numero = int.Parse(txbNumero.Text);
                nuevo.Descripcion = txbDescripcion.Text;
                nuevo.UrlImagen = txbUrlImagen.Text;

                nuevo.Tipo = new Elemento();
                nuevo.Tipo.Id = int.Parse(ddlTipo.SelectedValue);
                nuevo.Debilidad = new Elemento();
                nuevo.Debilidad.Id = int.Parse(ddlDebilidad.SelectedValue);

                negocio.agregar(nuevo);
                Response.Redirect("ListaPokemons.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
            

        }

        protected void txbUrlImagen_TextChanged(object sender, EventArgs e)
        {
            string url = txbUrlImagen.Text;
            imgPokemon.ImageUrl = url;
        }
    }
}