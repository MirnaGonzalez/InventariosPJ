using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using InventariosPJEH.CNegocios;


namespace InventariosPJEH.CAccesoDatos
{
    public class BdCatPropiedades
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Propiedad"></param>
        /// <returns></returns>
        public static List<CPropiedades> ConsultarGbPropiedades(string Propiedad)
        {
            List<CPropiedades> lista = new List<CPropiedades>();
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());

            try
            {

             

                SqlCommand cmd = new SqlCommand(string.Format("SELECT * from Cat_Propiedades WHERE Propiedad like '%{0}%' ORDER BY Propiedad", Propiedad), cnn);

                
                cnn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        CPropiedades cpropiedad = new CPropiedades();
                        cpropiedad.IdPropiedad = rd["IdPropiedad"].ToString();
                        cpropiedad.Propiedad = rd["Propiedad"].ToString();
                        lista.Add(cpropiedad);
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
        /// Eliminar_Propiedad
        /// </summary>
        /// <param name="obJE"></param>
        /// 

        public void Eliminar_Propiedad(CPropiedades obJE)
        {
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("SP_Eliminar_Propiedad", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idPropiedad", obJE.IdPropiedad);
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }


        //public void Eliminar_Propiedad(CPropiedades obJE)
        //{
        //    SqlConnection Conn = new SqlConnection(CConexion.Obtener());
        //    bool success = false;
        //    SqlTransaction lTransaccion = null;
        //    int Valor_Retornado = 0;

        //    try
        //    {
        //        Conn.Open();
        //        lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
        //        SqlCommand cmd = new SqlCommand("SP_Eliminar_Propiedad", Conn, lTransaccion);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@idPropiedad", obJE.IdPropiedad);
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

    }
    }
