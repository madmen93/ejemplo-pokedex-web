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
        public bool ConfirmaEliminacion { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            txbId.Enabled = false;
            ConfirmaEliminacion = false;
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

                    //Guardamos el pokemon seleccionado en la sesión:
                    Session.Add("pokeSeleccionado", seleccionado);

                    txbId.Text = seleccionado.Id.ToString();
                    txbNombre.Text = seleccionado.Nombre;
                    txbNumero.Text = seleccionado.Numero.ToString();
                    txbDescripcion.Text = seleccionado.Descripcion;
                    txbUrlImagen.Text = seleccionado.UrlImagen;

                    ddlTipo.SelectedValue = seleccionado.Tipo.Id.ToString();
                    ddlDebilidad.SelectedValue = seleccionado.Debilidad.Id.ToString();
                    txbUrlImagen_TextChanged(sender, e);

                    if (!seleccionado.Activo)
                    {
                        btnInactivar.Text = "Reactivar";
                        btnInactivar.CssClass = "btn btn-success";
                    }

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

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ConfirmaEliminacion = true;
        }

        protected void btnConfirmaEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (chbEliminar.Checked)
                {
                    PokemonNegocio negocio = new PokemonNegocio();
                    negocio.eliminar(int.Parse(txbId.Text));
                    Response.Redirect("ListaPokemons.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }

        protected void btnInactivar_Click(object sender, EventArgs e)
        {
            try
            {
                PokemonNegocio negocio = new PokemonNegocio();
                Pokemon seleccionado = (Pokemon)Session["pokeSeleccionado"]; //Accedemos al pokemon guardado en la sesión
                negocio.eliminarLogico(seleccionado.Id, !seleccionado.Activo); //Se agregó parámetro activo para la función reactivar
                Response.Redirect("ListaPokemons.aspx", false );
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }
    }
}