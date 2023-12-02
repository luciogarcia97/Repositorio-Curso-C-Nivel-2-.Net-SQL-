using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;
using System.Net;

namespace negocio
{
    public class PokemonNegocio
    {
        public List<Pokemon> listar()
        {
            List<Pokemon> lista = new List<Pokemon>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select p.Numero, p.Nombre, p.Descripcion, p.UrlImagen, e.Descripcion Tipo, d.Descripcion Debilidad, p.IdTipo, p.IdDebilidad, p.Id from POKEMONS p, ELEMENTOS e, ELEMENTOS d where p.IdTipo = e.Id and p.IdDebilidad = d.Id and p.activo = 1 order by Numero");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pokemon aux = new Pokemon();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Numero = datos.Lector.GetInt32(0);
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    //if(!(datos.Lector.IsDBNull(datos.Lector.GetOrdinal("UrlImagen"))))
                    //    aux.UrlImagen = (string)datos.Lector["UrlImagen"];
                    if (!(datos.Lector["UrlImagen"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["UrlImagen"];

                    aux.Tipo = new Elemento();
                    aux.Tipo.Id = (int)datos.Lector["IdTipo"];
                    aux.Tipo.Descripcion = (string)datos.Lector["Tipo"];
                    
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Id = (int)datos.Lector["IdDebilidad"];
                    aux.Debilidad.Descripcion = (string)datos.Lector["Debilidad"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }

        public void agregar(Pokemon nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("insert into POKEMONS (Numero, Nombre, Descripcion, UrlImagen, IdTipo, IdDebilidad, Activo) values (@Numero@,@Nombre@,@Descripcion@,@UrlImagen@,@IdTipo@,@IdDebilidad@,1)");
                
                
                datos.setearParametro("@Numero@",nuevo.Numero);
                datos.setearParametro("@Nombre@",nuevo.Nombre);
                datos.setearParametro("@Descripcion@",nuevo.Descripcion);
                datos.setearParametro("@UrlImagen@", nuevo.UrlImagen);
                datos.setearParametro("@IdTipo@", nuevo.Tipo.Id);
                datos.setearParametro("@IdDebilidad@", nuevo.Debilidad.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally 
            { 
                datos.cerrarConexion();
            }
        }
        public void modificar(Pokemon pokemon)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update POKEMONS set Numero = @Numero@, Nombre = @Nombre@, Descripcion = @Descripcion@, UrlImagen = @UrlImagen@, IdTipo = @IdTipo@, IdDebilidad = @IdDebilidad@, Activo = 1 WHERE Id = @Id@;");

                datos.setearParametro("@Id@", pokemon.Id);
                datos.setearParametro("@Numero@", pokemon.Numero);
                datos.setearParametro("@Nombre@", pokemon.Nombre);
                datos.setearParametro("@Descripcion@", pokemon.Descripcion);
                datos.setearParametro("@UrlImagen@", pokemon.UrlImagen);
                datos.setearParametro("@IdTipo@", pokemon.Tipo.Id);
                datos.setearParametro("@IdDebilidad@", pokemon.Debilidad.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM Pokemons WHERE id = @id;");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void eliminarLogico(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update POKEMONS set Activo = 0 where id = @id;");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Pokemon> filtrar(string campo, string criterio, string filtro)
        {
            List<Pokemon> lista = new List<Pokemon>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT p.Numero, p.Nombre, p.Descripcion, p.UrlImagen, e.Descripcion AS Tipo, d.Descripcion AS Debilidad, p.IdTipo, p.IdDebilidad, p.Id FROM POKEMONS p, ELEMENTOS e, ELEMENTOS d WHERE p.IdTipo = e.Id AND p.IdDebilidad = d.Id AND p.activo = 1 AND ";

            switch (campo)
            {
                case "Numero":
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "p.Numero > @filtro";
                            break;
                        case "Menor a":
                            consulta += "p.Numero < @filtro";
                            break;
                        default:
                            consulta += "p.Numero = @filtro";
                            break;
                    }
                    break;
                case "Nombre":
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "p.Nombre LIKE @filtro + '%'";
                            break;
                        case "Termina con":
                            consulta += "p.Nombre LIKE '%' + @filtro";
                            break;
                        default:
                            consulta += "p.Nombre LIKE '%' + @filtro + '%'";
                            break;
                    }
                    break;
                default:
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "p.Descripcion LIKE @filtro + '%'";
                            break;
                        case "Termina con":
                            consulta += "p.Descripcion LIKE '%' + @filtro";
                            break;
                        default:
                            consulta += "p.Descripcion LIKE '%' + @filtro + '%'";
                            break;
                    }
                    break;
            }

                consulta += " ORDER BY Numero";


                datos.setearConsulta(consulta);
                datos.setearParametro("@filtro", filtro);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Pokemon aux = new Pokemon();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Numero = datos.Lector.GetInt32(0);
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    if (!(datos.Lector["UrlImagen"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["UrlImagen"];

                    aux.Tipo = new Elemento();
                    aux.Tipo.Id = (int)datos.Lector["IdTipo"];
                    aux.Tipo.Descripcion = (string)datos.Lector["Tipo"];

                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Id = (int)datos.Lector["IdDebilidad"];
                    aux.Debilidad.Descripcion = (string)datos.Lector["Debilidad"];

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }
    }
}
