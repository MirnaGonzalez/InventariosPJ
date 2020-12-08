using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using InventariosPJEH.CNegocios;

namespace InventariosPJEH.CAccesoDatos
{
    public class BdCatSubClase_Partidas
    {
        //Métod para incializar la carga de partidas
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataTable InicializarCarga()
        {
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.CommandText = "select DISTINCT IdPartida, Partida from Cat_Partidas ORDER BY Partida ASC";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }

        //lista para consultar las partidas
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPartida"></param>
        /// <returns></returns>
        public static List<CSubClase_Partida> ConsultarGbPartidas(string nPartida)
        {
            List<CSubClase_Partida> lista = new List<CSubClase_Partida>();
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());

            try
            {
                SqlCommand cmd = new SqlCommand(string.Format("select * from Cat_Partidas where Partida like '%{0}%' ORDER BY IdPartida ASC", nPartida), cnn);
                cnn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        CSubClase_Partida cSubClasePartidas = new CSubClase_Partida();
                        cSubClasePartidas.IdPartida = rd["IdPartida"].ToString();
                        cSubClasePartidas.Partida = rd["Partida"].ToString();
                        cSubClasePartidas.TipoPartida = rd["TipoPartida"].ToString();
                        cSubClasePartidas.NumPartida = rd["NumPartida"].ToString();

                        lista.Add(cSubClasePartidas);
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
        /// Lista para consultar las partidas nuevas
        /// </summary>
        /// <param name="nPartida"></param>
        /// <param name="tpartida"></param>
        /// <param name="numPartida"></param>
        /// <returns></returns>
        public static List<CSubClase_Partida> ConsultarGbPartidasVuevo(string nPartida, string tpartida)
        {
            List<CSubClase_Partida> lista = new List<CSubClase_Partida>();
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());

            try
            {
       
                SqlCommand cmd = new SqlCommand(string.Format("select * from Cat_Partidas where Partida like '%{0}%' or TipoPartida like '%{1}%'  ORDER BY IdPartida ASC", nPartida, tpartida), cnn);
                cnn.Open();
  
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        CSubClase_Partida cSubClasePartidas = new CSubClase_Partida();
                        cSubClasePartidas.IdPartida = rd["IdPartida"].ToString();
                        cSubClasePartidas.Partida = rd["Partida"].ToString();
                        cSubClasePartidas.TipoPartida = rd["TipoPartida"].ToString();
                        cSubClasePartidas.NumPartida = rd["NumPartida"].ToString();

                        lista.Add(cSubClasePartidas);
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
        /// Lista para consultar las subclases
        /// </summary>
        /// <param name="nIdPartida"></param>
        /// <returns></returns>
        public static List<CSubClase_Partida> ConsultarGbSubClases(string nIdPartida)
        {
            List<CSubClase_Partida> lista = new List<CSubClase_Partida>();
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());

            try
            {
                SqlCommand cmd = new SqlCommand(string.Format("select * from VistaSubClasePartidas where IdPartida like '%{0}%' ORDER BY Partida ASC", nIdPartida), cnn);
                cnn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        CSubClase_Partida cSubClasePartidas = new CSubClase_Partida();
                        cSubClasePartidas.IdSubClase = rd["IdSubClase"].ToString();
                        cSubClasePartidas.IdPartida = rd["IdPartida"].ToString();
                        cSubClasePartidas.Folio = rd["Folio"].ToString();
                        cSubClasePartidas.Partida = rd["Partida"].ToString();
                        cSubClasePartidas.SubClase = rd["SubClase"].ToString();
                        lista.Add(cSubClasePartidas);
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
        /// Lista para consultar las subclases nuevas
        /// </summary>
        /// <param name="nIdPartida"></param>
        /// <returns></returns>
        public static List<CSubClase_Partida> ConsultarGbSubClasesNuevo(string nIdPartida)
        {
            List<CSubClase_Partida> lista = new List<CSubClase_Partida>();
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            try
            {
                
                SqlCommand cmd = new SqlCommand(string.Format("select * from VistaSubClasePartidas where IdPartida like '%{0}%' ORDER BY Partida ASC", nIdPartida), cnn);
                cnn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        CSubClase_Partida cSubClasePartidas = new CSubClase_Partida();
                        cSubClasePartidas.IdSubClase = rd["IdSubClase"].ToString();
                        cSubClasePartidas.IdPartida = rd["IdPartida"].ToString();
                        cSubClasePartidas.Folio = rd["Folio"].ToString();
                        cSubClasePartidas.Partida = rd["Partida"].ToString();
                        cSubClasePartidas.SubClase = rd["SubClase"].ToString();
                        lista.Add(cSubClasePartidas);
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
        /// Método para eliminar una partida
        /// </summary>
        /// <param name="obJE"></param>
        /// 

        public void Eliminar_Partidas(CSubClase_Partida obJE)
        {
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("SP_Eliminar_Partidas", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idPartida", obJE.IdPartida);
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }


        ////public void Eliminar_Partidas(CSubClase_Partida obJE)
        ////{
        ////    SqlConnection Conn = new SqlConnection(CConexion.Obtener());
        ////    bool success = false;
        ////    SqlTransaction lTransaccion = null;
        ////    int Valor_Retornado = 0;

        ////    try
        ////    {
        ////        Conn.Open();
        ////        lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
        ////        SqlCommand cmd = new SqlCommand("SP_Eliminar_Partidas", Conn, lTransaccion);
        ////        cmd.CommandType = CommandType.StoredProcedure;
        ////        cmd.Parameters.Clear();
        ////        cmdrRetorno.Direction = ParameterDirection.Output;
        ////        cm.Parameters.AddWithValue("@idPartida", obJE.IdPartida);
        ////        SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
        ////        Valod.Parameters.Add(ValorRetorno);
        ////        cmd.ExecuteNonQuery();
        ////        Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);
        ////        if (Valor_Retornado == 1)
        ////            success = true;
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        Conn.Close();
        ////    }
        ////    finally
        ////    {
        ////        if (success)
        ////        {
        ////            lTransaccion.Commit();
        ////            Conn.Close();
        ////        }
        ////        else
        ////        {
        ////            lTransaccion.Rollback();
        ////            Conn.Close();
        ////        }
        ////    }


        ////}

        /// <summary>
        /// Método para eliminar una subclase
        /// </summary>
        /// <param name="obJE"></param>
        /// 

        public void Eliminar_SubClases(CSubClase_Partida obJE)
        {
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("SP_Eliminar_SubClases", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idSubClase", obJE.IdSubClase);
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }


        //public void Eliminar_SubClases(CSubClase_Partida obJE)
        //{

        //    SqlConnection Conn = new SqlConnection(CConexion.Obtener());
        //    bool success = false;
        //    SqlTransaction lTransaccion = null;
        //    int Valor_Retornado = 0;

        //    try
        //    {
        //        Conn.Open();
        //        lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
        //        SqlCommand cmd = new SqlCommand("SP_Eliminar_SubClases", Conn, lTransaccion);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@idSubClase", obJE.IdSubClase);
        //    SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
        //    ValorRetorno.Direction = ParameterDirection.Output;
        //    cmd.Parameters.Add(ValorRetorno);
        //    cmd.ExecuteNonQuery();
        //    Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);
        //    if (Valor_Retornado == 1)
        //        success = true;
        //}
        //    catch (Exception ex)
        //    {
        //        Conn.Close();
        //    }
        //    finally
        //    {
        //        if (success)
        //        {
        //            lTransaccion.Commit();
        //            Conn.Close();
        //        }
        //        else
        //        {
        //            lTransaccion.Rollback();
        //            Conn.Close();
        //        }
        //    }
        //}
    }
}