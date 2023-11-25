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
				command.CommandText = "select d.Titulo as Titulo, d.FechaLanzamiento as FechaLanzamiento, d.CantidadCanciones as CantidadCanciones, e.Descripcion as Estilo, te.Descripcion as TipoEdicion from DISCOS d\r\nleft join ESTILOS e on e.Id = d.IdEstilo\r\nleft join TIPOSEDICION te on te.Id = d.IdTipoEdicion";
				command.Connection = conexion;

                conexion.Open();
                lector = command.ExecuteReader();
                while (lector.Read())
				{
					Discos aux = new Discos();
					aux.Titulo = (string)lector["Titulo"];
					aux.FechaLanzamiento = (DateTime)lector["FechaLanzamiento"];
					aux.CantidadCanciones = (int)lector["CantidadCanciones"];
					aux.Estilo = (string)lector["Estilo"];
					aux.TipoEdicion = (string)lector["TipoEdicion"];

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
