using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using InventariosPJEH.CAccesoDatos;
using InventariosPJEH.CNegocios;

namespace InventariosPJEH.CAccesoDatos
{
    public class BdCatTipoDocumento
    {
        /// <summary>
        /// Obtiene la lista del tipo de documento solicitado desde la base de datos
        /// y devuelve una lista de objetos lista
        /// </summary>
        /// <param name="nTipoDocumento"></param>
        /// <returns></returns>
        public static List<CTipoDocumento> ConsultarTipoDocumento(string nTipoDocumento)
        {
            List<CTipoDocumento> lista = new List<CTipoDocumento>();
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            
            try
            {
                SqlCommand cmd = new SqlCommand(string.Format("select * from Cat_TipoDocumento where  Documento like '%{0}%' ORDER BY Documento ASC ", nTipoDocumento), cnn);
                
                cnn.Open();

                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        CTipoDocumento ctipodoc = new CTipoDocumento();
                        ctipodoc.IdTipoDoc = rd["IdTipoDoc"].ToString();
                        ctipodoc.Documento = rd["Documento"].ToString();
                        lista.Add(ctipodoc);
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
        /// Clase que permite eliminar el tipo de documento seleccionado
        /// </summary>
        /// <param name="obJE"></param>
        /// 


        public void Eliminar_TipoDoc(CTipoDocumento obJE)
        {
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("SP_Eliminar_TipoDocumento", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idTipoDoc", obJE.IdTipoDoc);
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }



       // public void Eliminar_TipoDoc(CTipoDocumento obJE)
       // {

         //   SqlConnection Conn = new SqlConnection(CConexion.Obtener());
        //    bool success = false;
        //    SqlTransaction lTransaccion = null;
        //    int Valor_Retornado = 0;

        //    try
        //    {
        //        Conn.Open();
        //        lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
        //    SqlCommand cmd = new SqlCommand("SP_Eliminar_TipoDocumento", Conn, lTransaccion);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@idTipoDoc", obJE.IdTipoDoc);
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