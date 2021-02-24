using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using InventariosPJEH.CNegocios;
using System.Configuration;
using System.Windows;
using System.Web.UI;
using InventariosPJEH.CVista;
using InventariosPJEH.CAccesoDatos;
using System.Web.UI.WebControls;
using System.Data.Odbc;

namespace InventariosPJEH.CAccesoDatos
{
    public class BdUnidadAdmin
    {
        
        public SqlConnection conexion;
        private static string error;
        protected SqlDataAdapter adaptador;
        protected SqlDataReader reader;
        protected DataSet data;
        
        /*public BdUnidadAdmin()
        {
            this.conexion = ConexionBD.getConexion();
        }*/

        /// <summary>
        /// Selecciona los Tipos de Unidades Administrativas
        /// </summary>
        /// <returns></returns>
        /// 
        public static DataTable ClasificaciónUA()
        {
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.CommandText = "select DISTINCT idAbreviatura, Descripcion from Abreviaturas where tipo = 'ClasificaciónUA' ORDER BY Descripcion ASC";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }


        /// <summary>
        /// Selecciona Distritos
        /// </summary>
        /// <returns></returns>
        /// 

        public static DataTable ObtenerDistritos()
        {
            SqlCommand cmd = new SqlCommand();
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlDataAdapter adp = new SqlDataAdapter();

            cmd.CommandText = "select IdDistrito, Distrito from Cat_Distritos ORDER BY Distrito ASC";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;

        }
        
        public static DataTable ObtenerSubFondo()
        {
            SqlCommand cmd = new SqlCommand();
            DataTable Tabla = new DataTable();
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlDataAdapter adp = new SqlDataAdapter();

            cmd.CommandText = "Select IdSubFondo, SubFondo, Tipo From Cat_SubFondo ORDER BY SubFondo";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
;
        }

        public static DataTable ObtenerTipo()
        {
            SqlCommand cmd = new SqlCommand();
            DataTable Tabla = new DataTable();
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlDataAdapter adp = new SqlDataAdapter();

            cmd.CommandText = "Select IdTipo, Tipo From Cat_TipoUniAdmin ORDER BY Tipo";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }



        public static List<CUnidadAdmin> ConsultarGbUnidad(string DescClasific)
        {
            List<CUnidadAdmin> Uni = new List<CUnidadAdmin>();
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            //SqlCommand cmd = new SqlCommand();
            //SqlDataAdapter adp = new SqlDataAdapter();

             try
             {

             cnn.Open();
               
             SqlCommand cmd = new SqlCommand(string.Format("SELECT IdUniAdmin, UniAdmin, IdSubFondo, SubFondo, Tipo, DescTipo, Telefono, EMail, IdClasificacion, Clasificacion, DescClasific, IdEmpleado FROM Vta_UniAdmin where DescClasific like '%{0}%' ORDER BY UniAdmin ASC", DescClasific), cnn);
                
                using (var rd = cmd.ExecuteReader()) 
                  {
                    while(rd.Read())
                    {
                        CUnidadAdmin UniA = new CUnidadAdmin();
                        UniA.IdUniAdmin = int.Parse(rd["IdUniAdmin"].ToString());
                        UniA.UniAdmin = rd["UniAdmin"].ToString();
                        UniA.IdSubFondo = int.Parse(rd["IdSubFondo"].ToString());
                        UniA.SubFondo = rd["SubFondo"].ToString();
                        UniA.Tipo = rd["Tipo"].ToString();
                        UniA.DescTipo = rd["DescTipo"].ToString();
                        UniA.Telefono = rd["Telefono"].ToString();
                        UniA.Email = rd["EMail"].ToString();
                        UniA.Clasificacion = rd["Clasificacion"].ToString();
                        UniA.DescClasific = rd["DescClasific"].ToString();
                        UniA.IdClasificacion = int.Parse(rd["IdClasificacion"].ToString());
                        UniA.IdDistrito = -1;
                        Uni.Add(UniA);
                    
                    
                    }
            
                  }
             }
              catch (Exception e)

              {
                  error = e.Message;
                  Console.WriteLine(e.ToString());
                  cnn.Close();
                  throw new Exception();
              }
              finally
              {
                  cnn.Close();
              }

            return Uni;
        }

       public static List<CUnidadAdmin> ConsultarGbUD(string DescClasific, string Distrito)
        {
            List<CUnidadAdmin> Uni = new List<CUnidadAdmin>();
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            try
            {
                SqlCommand cmd = new SqlCommand(string.Format("Select IdUniAdmin, UniAdmin, IdSubFondo, SubFondo, Tipo, DescTipo, Telefono, EMail, IdClasificacion, Clasificacion, DescClasific, IdEmpleado, IdDistrito, Distrito FROM Vta_UniAdminDist where DescClasific like '%{0}%' and Distrito like '%{1}%' ORDER BY UniAdmin ASC", DescClasific, Distrito), cnn);
                cnn.Open();
                using(var rd = cmd.ExecuteReader())
                {
                    while(rd.Read())
                    {
                        CUnidadAdmin UniA = new CUnidadAdmin();
                        UniA.IdUniAdmin = int.Parse(rd["IdUniAdmin"].ToString());
                        UniA.UniAdmin = rd["UniAdmin"].ToString();
                        UniA.IdSubFondo = int.Parse(rd["IdSubFondo"].ToString());
                        UniA.SubFondo = rd["SubFondo"].ToString();
                        UniA.Tipo = rd["Tipo"].ToString();
                        UniA.DescTipo = rd["DescTipo"].ToString();
                        UniA.Telefono = rd["Telefono"].ToString();
                        UniA.Email = rd["EMail"].ToString();
                        UniA.Clasificacion = rd["Clasificacion"].ToString();
                        UniA.DescClasific = rd["DescClasific"].ToString();
                        UniA.IdClasificacion = int.Parse(rd["IdClasificacion"].ToString());
                        UniA.IdDistrito = int.Parse(rd["IdDistrito"].ToString());
                        UniA.Distrito = rd["Distrito"].ToString();
                        Uni.Add(UniA);
                    }
                }
            }
            catch (Exception e)

            {
                error = e.Message;
                Console.WriteLine(e.ToString());
                cnn.Close();
                throw new Exception();
            }
            finally
            {
                cnn.Close();
            }
            return Uni;
        }

        public static bool InsertarUniAdmin(int IdAbreviatura,int IdDistrito, int IdSubFondo, String IdTipo, String nomUniAdmin, String telefono, String email)
        {

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            bool sucess = false;
            SqlTransaction lTransaccion = null;

            try
            {
                cnn.Open();

                lTransaccion = cnn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                SqlCommand cmd = new SqlCommand("SP_Insertar_UniAdmn", cnn, lTransaccion);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@IdAbreviatura", IdAbreviatura);
                cmd.Parameters.AddWithValue("@IdSubFondo", IdSubFondo);
                cmd.Parameters.AddWithValue("@Tipo", IdTipo);
                cmd.Parameters.AddWithValue("@UniAdmin", nomUniAdmin);                
                cmd.Parameters.AddWithValue("@Telefono", telefono);
                cmd.Parameters.AddWithValue("@EMail", email);          

                SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
                ValorRetorno.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ValorRetorno);

                SqlParameter idUniAdminParam = new SqlParameter("@IdUniAdminInsertado", SqlDbType.Int);
                idUniAdminParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(idUniAdminParam);

                cmd.ExecuteNonQuery();

                int comprobacion_Insert_UniAdmin = Convert.ToInt32(ValorRetorno.Value);
                int Valor_Retornado_Insert_UniAdmin = Convert.ToInt32(idUniAdminParam.Value);


                if (comprobacion_Insert_UniAdmin == 1)
                {
                    sucess = true;
                }

                if (IdDistrito != 0)
                {
                    cmd = new SqlCommand("SP_Insertar_Uni_UADistrito", cnn, lTransaccion);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();

                    cmd.Parameters.AddWithValue("@IdUniAdmin", Valor_Retornado_Insert_UniAdmin);
                    cmd.Parameters.AddWithValue("@IdDistrito", IdDistrito);

                    ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
                    ValorRetorno.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(ValorRetorno);
                    cmd.ExecuteNonQuery();

                    int comprobacion_Insert_UADistrito = Convert.ToInt32(ValorRetorno.Value);
                    if (comprobacion_Insert_UniAdmin == 1 && comprobacion_Insert_UADistrito == 1)
                    {
                        sucess = true;
                    }
                    else
                    {
                        sucess = false;
                    }
                }                                
                return sucess;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception();
            }
            finally
            {
                if (sucess)
                {
                    lTransaccion.Commit();
                    cnn.Close();
                }
                else
                {
                    lTransaccion.Rollback();
                    cnn.Close();
                }
            }
        }

        public static bool ActualizarUniAdmin(int idUniAdmin, int IdAbreviatura, int IdDistrito, int IdSubFondo, String IdTipo, String nomUniAdmin, String telefono, String email)
        {
            SqlTransaction lTransaccion = null;
            SqlConnection Conn = new SqlConnection(CConexion.Obtener());
            int Valor_Retornado = 0;
            int Valor_Retornado_UAD = 0;
            bool success = false;
            try
            {
                Conn.Open();

                lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                SqlCommand cmd = new SqlCommand("SP_Actualizar_UniAdmin", Conn, lTransaccion);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@IdUniAdmin", idUniAdmin));
                cmd.Parameters.Add(new SqlParameter("@IdSubFondo", IdSubFondo));
                cmd.Parameters.Add(new SqlParameter("@UniAdmin", nomUniAdmin));
                cmd.Parameters.Add(new SqlParameter("@Tipo", IdTipo));
                cmd.Parameters.Add(new SqlParameter("@Telefono", telefono));
                cmd.Parameters.Add(new SqlParameter("@EMail", email));
                cmd.Parameters.Add(new SqlParameter("@IdAbreviatura", IdAbreviatura));

                SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);

                ValorRetorno.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ValorRetorno);

                cmd.ExecuteNonQuery();
                Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);

                if (Valor_Retornado == 1)
                {
                    success = true;
                }

                if (IdDistrito != 0)
                {
                    cmd = new SqlCommand("SP_Actualizar_UADistrito", Conn, lTransaccion);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@IdUniAdmin", idUniAdmin));
                    cmd.Parameters.Add(new SqlParameter("@IdDistrito", IdDistrito));

                    SqlParameter ValorRetornoUA = new SqlParameter("@Comprobacion", SqlDbType.Int);

                    ValorRetornoUA.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(ValorRetornoUA);

                    cmd.ExecuteNonQuery();
                    Valor_Retornado_UAD = Convert.ToInt32(ValorRetornoUA.Value);

                    if (Valor_Retornado == 1 && Valor_Retornado_UAD == 1)
                    {
                        success = true;
                    }
                    else
                    {
                        success = false;
                    }
                }

                
                return success;
            }
            catch (Exception e)
            {
                error = e.Message;
                Console.WriteLine(e.ToString());
                Conn.Close();
                return false;
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


        public static bool EliminarUnidad(int idUniAdmin)
        {
            SqlTransaction lTransaccion = null;
            SqlConnection Conn = new SqlConnection(CConexion.Obtener());
            bool success = false;
            try
            {
                Conn.Open();

                lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                SqlCommand cmd = new SqlCommand("SP_Eliminar_UniAdmin", Conn, lTransaccion);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@IdUniAdmin", idUniAdmin));

                SqlParameter ValorRetornoUA = new SqlParameter("@Comprobacion", SqlDbType.Int);

                ValorRetornoUA.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ValorRetornoUA);

                cmd.ExecuteNonQuery();
                int res = Convert.ToInt32(ValorRetornoUA.Value);

                if (res == 1)
                {
                    success = true;
                }
                return success;
            }
            catch (Exception e)
            {
                error = e.Message;
                Console.WriteLine(e.ToString());
                Conn.Close();
                return false;
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