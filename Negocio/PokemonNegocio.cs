﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; //TERCERO: Incluir la libreria
using dominio;
using Negocio;
using System.Security.Cryptography;

namespace negocio
{
    public class PokemonNegocio
    {
        //para leer los datos en la DB en SQL
        public List<Pokemon> listar(string id = "") //PRIMERO: crear la lista ---OJO: se le agregó el string id para cuando modifiquemos el pokemon, si no le mandas nada queda vacío y trae todos, caso contrario trae el pokemon del id 
        {
            List<Pokemon> lista = new List<Pokemon>();//PRIMERO: crear la lista
            SqlConnection conexion = new SqlConnection(); //CUARTO: Crear los objetos para la lectura de la DB (1)
            SqlCommand comando = new SqlCommand(); //(2)
            SqlDataReader lector; //(3)

            try //SEGUNDO: Agregar el try para controlar las excepciones
            {
                conexion.ConnectionString = "server=DESKTOP-A97N5T7\\SQLEXPRESS; database=POKEDEX_DB; integrated security=true"; // también puede ser ".\\SQLEXPRESS" o "(local)\\SQLEXPRESS"
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "Select Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion Tipo, D.Descripcion Debilidad, P.IdTipo, P.IdDebilidad, P.Id, Activo from POKEMONS P, ELEMENTOS E, ELEMENTOS D where E.Id = P.IdTipo AND D.Id = P.IdDebilidad "; //al final agregamos un espacion en blanco para el if:
                if (id != "")
                    comando.CommandText += "And P.Id = " + id;
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Pokemon aux = new Pokemon(); //1
                    aux.Id = (int)lector["Id"];
                    aux.Numero = lector.GetInt32(0); //2
                    aux.Nombre = (string)lector["Nombre"]; //3
                    aux.Descripcion = (string)lector["Descripcion"]; //4

                    //if(!(lector.IsDBNull(lector.GetOrdinal("UrlImagen")))) //11
                    //  aux.UrlImagen = (string)lector["UrlImagen"]; //5
                    if (!(lector["UrlImagen"] is DBNull)) //11
                        aux.UrlImagen = (string)lector["UrlImagen"]; //5


                    aux.Tipo = new Elemento(); //6
                    aux.Tipo.Id = (int)lector["IdTipo"];    
                    aux.Tipo.Descripcion = (string)lector["Tipo"]; //7
                    aux.Debilidad = new Elemento(); //8
                    aux.Debilidad.Id = (int)lector["IdDebilidad"]; 
                    aux.Debilidad.Descripcion = (string)lector["Debilidad"]; //9
                    aux.Activo = bool.Parse(lector["Activo"].ToString());

                    lista.Add(aux); //10
                }

                conexion.Close();
                return lista; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public List<Pokemon> listarconSP()
        {
            List<Pokemon> lista = new List<Pokemon>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                //string consulta = "Select Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion Tipo, D.Descripcion Debilidad, P.IdTipo, P.IdDebilidad, P.Id from POKEMONS P, ELEMENTOS E, ELEMENTOS D where E.Id = P.IdTipo AND D.Id = P.IdDebilidad And P.Activo = 1";
               
                //datos.setearConsulta(consulta);
                datos.setearProcedimiento("storedListar");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Pokemon aux = new Pokemon(); //1
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Numero = datos.Lector.GetInt32(0); //2
                    aux.Nombre = (string)datos.Lector["Nombre"]; //3
                    aux.Descripcion = (string)datos.Lector["Descripcion"]; //4

                    //if(!(lector.IsDBNull(lector.GetOrdinal("UrlImagen")))) //11
                    //  aux.UrlImagen = (string)lector["UrlImagen"]; //5
                    if (!(datos.Lector["UrlImagen"] is DBNull)) //11
                        aux.UrlImagen = (string)datos.Lector["UrlImagen"]; //5


                    aux.Tipo = new Elemento(); //6
                    aux.Tipo.Id = (int)datos.Lector["IdTipo"];
                    aux.Tipo.Descripcion = (string)datos.Lector["Tipo"]; //7
                    aux.Debilidad = new Elemento(); //8
                    aux.Debilidad.Id = (int)datos.Lector["IdDebilidad"];
                    aux.Debilidad.Descripcion = (string)datos.Lector["Debilidad"]; //9
                    aux.Activo = bool.Parse(datos.Lector["Activo"].ToString());

                    lista.Add(aux); //10
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void agregarSP(Pokemon nuevo)
        {
            AccesoDatos datos = new AccesoDatos(); //2

            try //1
            {
                //Con procedimiento almacenado, mandamos los parámetros:
                datos.setearProcedimiento("storedAltaPokemon");
                datos.setearParametro("@numero", nuevo.Numero); 
                datos.setearParametro("@nombre", nuevo.Nombre); 
                datos.setearParametro("@desc", nuevo.Descripcion); 
                datos.setearParametro("@img", nuevo.UrlImagen);
                datos.setearParametro("@idTipo", nuevo.Tipo.Id); 
                datos.setearParametro("@idDebilidad", nuevo.Debilidad.Id); 
                datos.ejecutarAccion(); 
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion(); //5
            }
        }
        public void agregar(Pokemon nuevo)
        {
            AccesoDatos datos = new AccesoDatos(); //2

            try //1
            {
                datos.setearConsulta("Insert into POKEMONS (Numero, Nombre, Descripcion, Activo, IdTipo, IdDebilidad, UrlImagen)values("+ nuevo.Numero + ", '" + nuevo.Nombre + "', '" + nuevo.Descripcion +"', 1, @idTipo, @idDebilidad, @urlImagen)"); //3
                datos.setearParametro("@idTipo", nuevo.Tipo.Id); //6
                datos.setearParametro("@idDebilidad", nuevo.Debilidad.Id); //7
                datos.setearParametro("@urlImagen", nuevo.UrlImagen); //8
                datos.ejecutarAccion(); //4
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion(); //5
            }
        }
        public void modificarSP(Pokemon modificado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("storedModificarPokemon");
                datos.setearParametro("@numero", modificado.Numero);
                datos.setearParametro("@nombre", modificado.Nombre);
                datos.setearParametro("@desc", modificado.Descripcion);
                datos.setearParametro("@img", modificado.UrlImagen);
                datos.setearParametro("@idTipo", modificado.Tipo.Id);
                datos.setearParametro("@idDebilidad", modificado.Debilidad.Id);
                datos.setearParametro("@id", modificado.Id);

                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            { datos.cerrarConexion(); }
        }
        public void modificar(Pokemon modificado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update POKEMONS set Numero = @numero, Nombre = @nombre, Descripcion = @desc, UrlImagen = @img, IdTipo = @idTipo, IdDebilidad = @idDebilidad where Id = @id");
                datos.setearParametro("@numero", modificado.Numero);
                datos.setearParametro("@nombre", modificado.Nombre);
                datos.setearParametro("@desc", modificado.Descripcion);
                datos.setearParametro("@img", modificado.UrlImagen);
                datos.setearParametro("@idTipo", modificado.Tipo.Id);
                datos.setearParametro("@idDebilidad",modificado.Debilidad.Id);
                datos.setearParametro("@id", modificado.Id);

                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally 
            { datos.cerrarConexion();}
        }

        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            
            try
            {
                datos.setearConsulta("delete from POKEMONS where id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void eliminarLogico(int id, bool activo = false) //activo = false hace que este parámetro sea opcional
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("update POKEMONS set Activo = @activo where Id = @id");
                datos.setearParametro("@id", id);
                datos.setearParametro("@activo", activo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Pokemon> filtrar(string campo, string criterio, string filtro, string estado)
        {
            List<Pokemon> lista = new List<Pokemon>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "Select Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion Tipo, D.Descripcion Debilidad, P.IdTipo, P.IdDebilidad, P.Id, P.Activo from POKEMONS P, ELEMENTOS E, ELEMENTOS D where E.Id = P.IdTipo AND D.Id = P.IdDebilidad And ";
                if (campo == "Número")
                {
                        switch (criterio)
                        {
                            case "Mayor a":
                                consulta += "Numero > " + filtro;
                                break;
                            case "Menor a":
                                consulta += "Numero < " + filtro;
                                break;
                            default:
                                consulta += "Numero = " + filtro;
                                break;
                        }

                }
                else if(campo == "Nombre")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "Nombre like '" + filtro + "%'" ;
                            break;
                        case "Termina con":
                            consulta += "Nombre like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "Nombre like '%" + filtro + "%'";
                            break;
                    }
                }
                else
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "E.Descripcion like '" + filtro + "%'";
                            break;
                        case "Termina con":
                            consulta += "E.Descripcion like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "E.Descripcion like '%" + filtro + "%'";
                            break;
                    }
                }
                if(estado == "Activo")
                {
                    consulta += " And P.Activo = 1";
                }else if(estado == "Inactivo")
                {
                    consulta += " And P.Activo = 0";
                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Pokemon aux = new Pokemon(); //1
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Numero = datos.Lector.GetInt32(0); //2
                    aux.Nombre = (string)datos.Lector["Nombre"]; //3
                    aux.Descripcion = (string)datos.Lector["Descripcion"]; //4

                    //if(!(lector.IsDBNull(lector.GetOrdinal("UrlImagen")))) //11
                    //  aux.UrlImagen = (string)lector["UrlImagen"]; //5
                    if (!(datos.Lector["UrlImagen"] is DBNull)) //11
                        aux.UrlImagen = (string)datos.Lector["UrlImagen"]; //5


                    aux.Tipo = new Elemento(); //6
                    aux.Tipo.Id = (int)datos.Lector["IdTipo"];
                    aux.Tipo.Descripcion = (string)datos.Lector["Tipo"]; //7
                    aux.Debilidad = new Elemento(); //8
                    aux.Debilidad.Id = (int)datos.Lector["IdDebilidad"];
                    aux.Debilidad.Descripcion = (string)datos.Lector["Debilidad"]; //9

                    aux.Activo = bool.Parse(datos.Lector["Activo"].ToString());

                    lista.Add(aux); //10
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }


}
