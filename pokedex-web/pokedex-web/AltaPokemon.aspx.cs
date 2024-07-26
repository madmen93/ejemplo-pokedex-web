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
                //Configuración inicial para nuevo pokemon:
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

                //Configuración para un pokemon existente (modificar pokemon):
                if(Request.QueryString["id"] != null && !IsPostBack)
                {
                    PokemonNegocio negocio = new PokemonNegocio();
                    List<Pokemon> lista = negocio.listar(Request.QueryString["id"].ToString());
                    Pokemon seleccionado = lista[0];

                    txbId.Text = seleccionado.Id.ToString();
                    txbNombre.Text = seleccionado.Nombre;
                    txbNumero.Text = seleccionado.Numero.ToString();
                    txbDescripcion.Text = seleccionado.Descripcion;
                    txbUrlImagen.Text = seleccionado.UrlImagen;

                    ddlTipo.SelectedValue = seleccionado.Tipo.Id.ToString();
                    ddlDebilidad.SelectedValue = seleccionado.Debilidad.Id.ToString();
                    txbUrlImagen_TextChanged(sender, e);
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

                if (Request.QueryString["id"]  != null)
                {
                    nuevo.Id = int.Parse(txbId.Text);
                    negocio.modificarSP(nuevo);
                }
                else
                    negocio.agregarSP(nuevo);
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