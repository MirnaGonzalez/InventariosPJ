using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using InventariosPJEH.CNegocios;

namespace InventariosPJEH.CAccesoDatos
{
    public class BdCatCONAC
    {
    

        //Lista para mostrar el grid si se hace una búsqueda con Descripción
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pDescripcion"></param>
        /// <returns></returns>
        public static List<CCONAC> ConsultarGbCONAC(string pDescripcion)
        {
            
            List<CCONAC> lista = new List<CCONAC>();
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            string cmdString = "";
            try
            {

                if (pDescripcion == string.Empty)
                {
                    cmdString = "Select * from Cat_CONAC ORDER BY IdCONAC ASC";
                }

                else
                {
                    cmdString = "Select * from Cat_CONAC where Descripcion like '%{0}%' ORDER BY IdCONAC ASC";
                }

                SqlCommand cmd = new SqlCommand(string.Format(cmdString, pDescripcion), cnn);
               
                cnn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        CCONAC conac = new CCONAC();
                        conac.IdClaveCONAC = (rd["IdClaveCONAC"]).ToString();
                        conac.IdCONAC = (rd["IdCONAC"]).ToString();
                        conac.Grupo = rd["Grupo"].ToString();
                        conac.SubGrupo = rd["SubGrupo"].ToString();
                        conac.Clase = rd["Clase"].ToString();
                        conac.Descripcion = rd["Descripcion"].ToString();
                        conac.TipoPartida = rd["TipoPartida"].ToString();
                       

                        lista.Add(conac);
                    }
                }
            }

            catch (Exception )
            {
                cnn.Close();
            }
            return lista;
        }


        //Lista para mostrar el grid si la busqueda es un nuevo registro
        public static List<CCONAC> ConsultarGbCONACNuevoRegistro(string idconac,string pGrupo, string pSubgrupo, string pclase)
        {

            List<CCONAC> lista = new List<CCONAC>();
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());

            try
            {

                SqlCommand cmd = new SqlCommand(string.Format("Select IdClaveCONAC, IdCONAC, Grupo, SubGrupo, Clase, Descripcion, TipoPartida from Cat_CONAC where IdCONAC like '%{0}%' or Grupo like '%{1}%' or SubGrupo like '%{2}%' or Clase like '%{3}%' ORDER BY IdCONAC ASC", idconac, pGrupo, pSubgrupo, pclase), cnn);
                cnn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        CCONAC conac = new CCONAC();
                        conac.IdClaveCONAC = (rd["IdClaveCONAC"]).ToString();
                        conac.IdCONAC = (rd["IdCONAC"]).ToString();
                        conac.Grupo = rd["Grupo"].ToString();
                        conac.SubGrupo = rd["SubGrupo"].ToString();
                        conac.Clase = rd["Clase"].ToString();
                        conac.Descripcion = rd["Descripcion"].ToString();
                        conac.TipoPartida = rd["TipoPartida"].ToString();
                        

                        lista.Add(conac);
                    }
                }
            }
            catch (Exception )
            {
                cnn.Close();
            }
            return lista;
        }

        //Método para ejecutar el procedimiento almacenado de eliminar un registro
        public void Eliminar_CONAC(CCONAC obJE)
        {
            SqlConnection Conn = new SqlConnection(CConexion.Obtener());
            bool success = false;
            SqlTransaction lTransaccion = null;
            int Valor_Retornado = 0;

            try
            {
                Conn.Open();
                lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                SqlCommand cmd = new SqlCommand("SP_Eliminar_CONAC", Conn, lTransaccion);
            cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdClaveCONAC", obJE.IdClaveCONAC);
                SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
                ValorRetorno.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ValorRetorno);
                cmd.ExecuteNonQuery();
                Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);
                if (Valor_Retornado == 1)
                    success = true;


            }
            catch (Exception ex )
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