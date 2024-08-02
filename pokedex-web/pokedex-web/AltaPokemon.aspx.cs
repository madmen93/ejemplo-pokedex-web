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
        public bool ConfirmaEliminacion { get; set; } // (0-Eliminar) Creamos la propiedad para ser accedida desde AltaPokemon.aspx
        protected void Page_Load(object sender, EventArgs e)
        {
            txbId.Enabled = false; //Esta propiedad se carga de manera automática, el usuario no tiene acceso
            ConfirmaEliminacion = false; // (1-Eliminar)Estado por defecto al cargar la página
            try
            {
                //Configuración inicial para nuevo pokemon:
                if (!IsPostBack)
                {
                    ElementoNegocio negocio = new ElementoNegocio();
                    List<Elemento> listaTipo = negocio.listar(); //Con esta lista cargamos los 2 desplegables.
                    ddlTipo.DataSource = listaTipo; //Fuente de origen de los datos
                    ddlTipo.DataValueField = "Id"; //Esto nos devuelve el valor del Id. Ojo que no usamos el SelectedIndex, no aplicaría
                    ddlTipo.DataTextField = "Descripcion"; //Esto es lo que va a mostrar.
                    ddlTipo.DataBind(); //El bindeo

                    ddlDebilidad.DataSource = listaTipo;
                    ddlDebilidad.DataValueField = "Id";
                    ddlDebilidad.DataTextField = "Descripcion";
                    ddlDebilidad.DataBind();
                }

                //Configuración para un pokemon existente (modificar pokemon):
                // También se puede escribir así: string id = Request.QueryString["id] != null ? Reques.QueryString["id"].ToString() : "";
                if(Request.QueryString["id"] != null && !IsPostBack) // if(id != "" && !IsPostBack) (1)
                {
                    PokemonNegocio negocio = new PokemonNegocio(); //(2)
                    List<Pokemon> lista = negocio.listar(Request.QueryString["id"].ToString()); // (3) Nos traemos la lista de la DB. Hemos agregado el parámetro opcional id (ver Pokemon Negocio) para poder obtener el pokemon seleccionado
                    Pokemon seleccionado = lista[0]; // (4) Aunque tenga un único elemento, sigue siendo una lista, así que pasamos el índice para acceder al pokemon
                    //Se puede simplificar así: Pokemon seleccionado = (negocio.listar(id))[0];

                    //Guardamos el pokemon seleccionado en la sesión:
                    Session.Add("pokeSeleccionado", seleccionado);

                    // (5) Precargamos todos los datos: 
                    txbId.Text = seleccionado.Id.ToString();
                    txbNombre.Text = seleccionado.Nombre;
                    txbNumero.Text = seleccionado.Numero.ToString();
                    txbDescripcion.Text = seleccionado.Descripcion;
                    txbUrlImagen.Text = seleccionado.UrlImagen;

                    ddlTipo.SelectedValue = seleccionado.Tipo.Id.ToString();
                    ddlDebilidad.SelectedValue = seleccionado.Debilidad.Id.ToString();
                    txbUrlImagen_TextChanged(sender, e); //Forzamos el precargado de la imagen

                    //Configurar acciones para el estado Activo o Inactivo del pokemon:
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
                Pokemon nuevo = new Pokemon(); // (1) Creamos un objeto de tipo Pokemon
                PokemonNegocio negocio = new PokemonNegocio(); // (5) Llamamos a PokemonNegocio

                nuevo.Nombre = txbNombre.Text; // (2) Vamos llenando los campos con los valores de los controles (textBox)...
                nuevo.Numero = int.Parse(txbNumero.Text);
                nuevo.Descripcion = txbDescripcion.Text;
                nuevo.UrlImagen = txbUrlImagen.Text;

                nuevo.Tipo = new Elemento(); // (3) Recordemos que Tipo es un objeto, por lo que es necesario cargar el dato generando el new Elemento
                nuevo.Tipo.Id = int.Parse(ddlTipo.SelectedValue);
                nuevo.Debilidad = new Elemento(); //(4) Lo mismo hacemos con Debilidad
                nuevo.Debilidad.Id = int.Parse(ddlDebilidad.SelectedValue);

                if (Request.QueryString["id"]  != null) //Si modificamos un pokemon...
                {
                    nuevo.Id = int.Parse(txbId.Text); //Agregamos el id para referenciar el pokemon
                    negocio.modificarSP(nuevo); 
                }
                else
                    negocio.agregarSP(nuevo); // (6) agregamos al pokemon nuevo a nuestra DB. Hemos utilizado un método con un procedimiento almacenado
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
            ConfirmaEliminacion = true; //(2-Eliminar) Se cambia a "true" para que muestre el bloque de eliminación
        }

        protected void btnConfirmaEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (chbEliminar.Checked) //Si checkeo la confirmación, entonces procede con el método eliminar
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