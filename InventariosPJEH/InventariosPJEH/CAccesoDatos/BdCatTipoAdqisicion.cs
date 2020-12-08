using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using InventariosPJEH.CNegocios;
using InventariosPJEH.CAccesoDatos;
using InventariosPJEH.CVista;
   
namespace InventariosPJEH.CAccesoDatos
{
    public class BdCatTipoAdqisicion
    {

        /// <summary>
        /// Obtiene la lista del tipo adquisición solicitado desde la base de datos
        /// y devuelve una lista de objetos lista
        /// </summary>
        /// <param name="nTipoAdqui"></param>
        /// <returns></returns>
        public static List<CTipoAdquisicion> ConsultarTipoAdquisicion(string nTipoAdqui)
        {
            List<CTipoAdquisicion> lista = new List<CTipoAdquisicion>();
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            try
            {

                SqlCommand cmd = new SqlCommand(string.Format("select * from Cat_TipoAdquisicion where TipoAdqui  like '%{0}%' ORDER BY TipoAdqui ASC", nTipoAdqui), cnn);           
                cnn.Open();

                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        CTipoAdquisicion ctipoAdqui = new CTipoAdquisicion();
                        ctipoAdqui.IdTipoAdqui = rd["IdTipoAdqui"].ToString();
                        ctipoAdqui.TipoAdqui = rd["TipoAdqui"].ToString();
                        lista.Add(ctipoAdqui);
                    }
                }
            }
            catch (Exception )
            {
                cnn.Close();
            }
            return lista;
        }

        /// <summary>
        /// Clase que permite eliminar el tipo adquisición seleccionado
        /// </summary>
        /// <param name="obJE"></param>
        public void Eliminar_TipoAdqui(CTipoAdquisicion obJE)
        {
            SqlConnection Conn = new SqlConnection(CConexion.Obtener());
            bool success = false;
            SqlTransaction lTransaccion = null;
            int Valor_Retornado = 0;

            try
            {
                Conn.Open();
                lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                SqlCommand cmd = new SqlCommand("SP_Eliminar_TipoAdquisicion", Conn, lTransaccion);
            cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idTipoAdqui", obJE.IdTipoAdqui);
                SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
                ValorRetorno.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ValorRetorno);
                cmd.ExecuteNonQuery();
                Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);
                if (Valor_Retornado == 1)
                    success = true;
            }
            catch (Exception )
            {
                Conn.Close();
            }
            finally
            {
                if (success)
                {
                    lTransaccion.Commit();
                    Conn.Close();
                }
                else
                {
                    lTransaccion.Rollback();
                    Conn.Close();
                }
            }
        }

    }
}