using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using InventariosPJEH.CNegocios;

namespace InventariosPJEH.CAccesoDatos
{
    public class BdCatMarca
    {
        /// <summary>
        /// Clase para iniciar llenado de la lista SubClase
        /// </summary>
        /// <returns></returns>
        public static DataTable IniciarLlenado()
        {
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.CommandText = "select DISTINCT idSubClase, Subclase from Cat_SubClase ORDER BY SubClase ASC";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }

        /// <summary>
        /// Obtiene la lista de las marcas solicitadas desde la base de datos
        /// y devuelve una lista de objetos lista
        /// </summary>
        /// <param name="nSubclase"></param>
        /// <param name="nMarca"></param>
        /// <returns></returns>
        public static List<CMarca> ConsultarGbSubclase(string nSubclase)
        {
            List<CMarca> lista = new List<CMarca>();
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());

            try
            {

                SqlCommand cmd = new SqlCommand(string.Format("select  * from VistaMarcaSubClase where SubClase like '%{0}%' ORDER BY Descripcion ASC ", nSubclase), cnn);
                cnn.Open();

                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        CMarca cMarca = new CMarca();
                        cMarca.Descripcion = rd["Descripcion"].ToString();
                        //cMarca.IdMarSub = rd["IdMarSub"].ToString();
                        cMarca.IdSubClase = rd["IdSubClase"].ToString();
                        cMarca.IdMarca = rd["IdMarca"].ToString();
                        cMarca.Partida = rd["Partida"].ToString();
                        cMarca.SubClase = rd["SubClase"].ToString();
                        cMarca.IdPartida = rd["IdPartida"].ToString();
                        lista.Add(cMarca);
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
        /// Obtiene la lista de las marcas solicitadas desde la base de datos
        /// y devuelve una lista de objetos lista
        /// </summary>
        /// <param name="nSubclase"></param>
        /// <param name="nMarca"></param>
        /// <returns></returns>
        public static List<CMarca> ConsultarGbMarca(string nSubclase, string nMarca)
        {
            List<CMarca> lista = new List<CMarca>();
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());

            try
            {
                
                SqlCommand cmd = new SqlCommand(string.Format("select  IdMarca, IdMarSub, Descripcion, IdSubClase, SubClase, IdPartida, Partida from VistaMarcaSubClase where SubClase like '%{0}%' or Descripcion like '%{1}%' ORDER BY Descripcion ASC ", nSubclase, nMarca), cnn);
                cnn.Open();

                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        CMarca cMarca = new CMarca();
                        cMarca.Descripcion = rd["Descripcion"].ToString();
                        cMarca.IdMarSub = rd["IdMarSub"].ToString();
                        cMarca.IdSubClase = rd["IdSubClase"].ToString();
                        cMarca.IdMarca = rd["IdMarca"].ToString();
                        cMarca.Partida = rd["Partida"].ToString();
                        cMarca.SubClase = rd["SubClase"].ToString();
                        cMarca.IdPartida = rd["IdPartida"].ToString();
                        lista.Add(cMarca);
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
        /// Clase que permite eliminar la marca seleccionada
        /// </summary>
        /// <param name="obJE"></param>
        /// 

        public void Eliminar_Marca(CMarca obJE)
        {
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("SP_Eliminar_Marca", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idMarca", obJE.IdMarca);
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }





        //public void Eliminar_Marca(CMarca obJE)
        //{
        //    SqlConnection Conn = new SqlConnection(CConexion.Obtener());           
        //    bool success = false;
        //    SqlTransaction lTransaccion = null;
        //    int Valor_Retornado = 0;

        //    try
        //    {
        //        Conn.Open();
        //        lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
        //        SqlCommand cmd = new SqlCommand("SP_Eliminar_Marca", Conn, lTransaccion);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@idMarca", obJE.IdMarca);
        //        SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
        //        ValorRetorno.Direction = ParameterDirection.Output;
        //        cmd.Parameters.Add(ValorRetorno);
        //        cmd.ExecuteNonQuery();
        //        Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);
        //        if (Valor_Retornado == 1)
        //            success = true;              
        //    }
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