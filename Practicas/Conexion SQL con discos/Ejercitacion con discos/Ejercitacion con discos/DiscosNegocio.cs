using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ejercitacion_con_discos
{
    internal class DiscosNegocio
    {
        public List<Discos> listarDiscos()
        {
			List<Discos> lista = new List<Discos>();
			SqlConnection conexion = new SqlConnection();
			SqlCommand command = new SqlCommand();
			SqlDataReader lector;

			try
			{
				conexion.ConnectionString = "server=.\\SQLEXPRESS; database=DISCOS_DB; integrated security=true";
				command.CommandType = System.Data.CommandType.Text;
				command.CommandText = "select d.Titulo, d.FechaLanzamiento, d.CantidadCanciones, d.UrlImagenTapa UrlImagen, e.Descripcion Estilo, t.Descripcion TipoDeEdicion from DISCOS d, ESTILOS e, TIPOSEDICION t where d.IdEstilo = e.Id and d.IdTipoEdicion = t.Id";
				command.Connection = conexion;

                conexion.Open();
                lector = command.ExecuteReader();

                while (lector.Read())
				{
					Discos aux = new Discos();
					aux.Titulo = (string)lector["Titulo"];
					aux.FechaLanzamiento = (DateTime)lector["FechaLanzamiento"];
					aux.CantidadCanciones = (int)lector["CantidadCanciones"];
					aux.UrlImagen = (string)lector["UrlImagen"];
					aux.Estilo = new Estilo();
					aux.Estilo.Descripcion = (string)lector["Estilo"];
					aux.TipoEdicion = new TiposEdicion();
					aux.TipoEdicion.Descripcion = (string)lector["TipoDeEdicion"];

					lista.Add(aux);
				}

				conexion.Close();
				return lista;
			}
			catch (Exception ex)
			{

				throw ex;
			}
        }
    }
}
