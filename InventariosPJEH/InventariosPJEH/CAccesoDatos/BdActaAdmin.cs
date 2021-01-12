using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventariosPJEH.CNegocios;
using System.Data.SqlClient;
using System.Data;
using InventariosPJEH.CAccesoDatos;


namespace InventariosPJEH.CAccesoDatos
{
    public class BdActaAdmin
    {

        private static string error;

        public static bool ActualizarActa(String NumActa, String fechaCancelacion, String status, String descripcion)
        {
            SqlTransaction lTransaccion = null;
            SqlConnection Conn = new SqlConnection(CConexion.Obtener());
            int Valor_Retornado = 0;
            bool success = false;
            try
            {
                Conn.Open();

                lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                SqlCommand cmd = new SqlCommand("SP_Actualizar_Actas", Conn, lTransaccion);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@NumActa", NumActa));
                cmd.Parameters.Add(new SqlParameter("@status", status));
                cmd.Parameters.Add(new SqlParameter("@Descripcion", descripcion));
                cmd.Parameters.Add(new SqlParameter("@FechaCancela", fechaCancelacion));

                SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);

                ValorRetorno.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ValorRetorno);

                cmd.ExecuteNonQuery();
                Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);

                if (Valor_Retornado == 1)
                {
                    success = true;
                }
                else
                {
                    success = false;
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

        public static bool InsertarActaInventario(int idAreaResguardante, int idResguardo, String NumActa, String fechaActa, List<CDatosBienesActa> listBienes)
        {
            SqlTransaction lTransaccion = null;
            SqlConnection Conn = new SqlConnection(CConexion.Obtener());
            int Valor_Retornado_Insert_Acta = 0;
            int Valor_Retornado_Insert_ActaInventario = 0;
            bool success = false;
            try
            {
                Conn.Open();

                lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);                
                SqlCommand cmd = new SqlCommand("SP_Insertar_Acta", Conn, lTransaccion);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@NumActa", NumActa));
                cmd.Parameters.Add(new SqlParameter("@FechaAlta", fechaActa));
                cmd.Parameters.Add(new SqlParameter("@IdUniAdmin", idAreaResguardante));
                cmd.Parameters.Add(new SqlParameter("@IdResguardo", idResguardo));

                SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);

                ValorRetorno.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ValorRetorno);

                SqlParameter idActaParam = new SqlParameter("@IdActaInsertada", SqlDbType.Int);

                idActaParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(idActaParam);

                cmd.ExecuteNonQuery();
                Valor_Retornado_Insert_Acta = Convert.ToInt32(ValorRetorno.Value);
                int idActa = Convert.ToInt32(idActaParam.Value);
                
                cmd = new SqlCommand("SP_Insertar_Acta_ActaInventario", Conn, lTransaccion);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                DataRow row;

                using (var table = new DataTable())
                {

                    table.Columns.Add("idActaT", typeof(int));
                    table.Columns.Add("idInventarioT", typeof(int));

                    foreach(var bien in listBienes)
                    {
                        row = table.NewRow();
                        row["idActaT"] = idActa;
                        row["idInventarioT"] = bien.idInventario;
                        table.Rows.Add(row);
                    }

                        

                    var pList = new SqlParameter("@listActaInventario", SqlDbType.Structured);

                    pList.TypeName = "dbo.ActaInventarioT";

                    pList.Value = table;

                    cmd.Parameters.Add(pList);

                    ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);

                    ValorRetorno.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(ValorRetorno);                    
                }

                cmd.ExecuteNonQuery();
                Valor_Retornado_Insert_ActaInventario = Convert.ToInt32(ValorRetorno.Value);

                if (Valor_Retornado_Insert_Acta == 1 && Valor_Retornado_Insert_ActaInventario == 1)
                {
                    success = true;
                }
                else
                {
                    success = false;
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

        public static Dictionary<CDatosResguardante, List<CDatosBienesActa>> buscarBienes(long IdResguardo)
        {

            CComparatorResguardante cComparatorResguardante = new CComparatorResguardante();
            Dictionary<CDatosResguardante, List<CDatosBienesActa>> resultadoDatos = new Dictionary<CDatosResguardante, List<CDatosBienesActa>>(cComparatorResguardante);
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());

            try
            {

                cnn.Open();

                SqlCommand cmd = new SqlCommand("SELECT IdEmpleado, IdInventario, Nombre, IdUniAdmin, UniAdmin, Cargo, IdResguardo, Actividad, NumInventario, DescripcionBien, Marca, Modelo, Serie, TipoPartida FROM Vta_buscarNolocalizados WHERE IdResguardo = @IdResguardo  ", cnn);

                cmd.Parameters.Add("@IdResguardo", SqlDbType.BigInt).Value = IdResguardo;

                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        CDatosResguardante datosResguardante = new CDatosResguardante();
                        CDatosBienesActa cDatosBienActa = new CDatosBienesActa();

                        datosResguardante.txtONombreResguardante = rd["Nombre"].ToString();
                        datosResguardante.txtOAreaAdscripcionResguardo = rd["UniAdmin"].ToString();
                        datosResguardante.txtOCargoResguardo = rd["Cargo"].ToString();
                        datosResguardante.idResguardo = int.Parse(rd["IdResguardo"].ToString());
                        datosResguardante.idAreaAdscrip = int.Parse(rd["IdUniAdmin"].ToString());

                        cDatosBienActa.idInventario = int.Parse(rd["IdInventario"].ToString());
                        cDatosBienActa.txtOInventarioResguardo = rd["NumInventario"].ToString();
                        cDatosBienActa.txtONombreBienResguardo = rd["DescripcionBien"].ToString();
                        cDatosBienActa.txtOMarcaResguardo = rd["Marca"].ToString();
                        cDatosBienActa.txtOModeloResguardo = rd["Modelo"].ToString();
                        cDatosBienActa.txtOSerieResguardo = rd["Serie"].ToString();

                        List<CDatosBienesActa> listaBienesResult = new List<CDatosBienesActa>();

                        if (resultadoDatos.TryGetValue(datosResguardante, out listaBienesResult))
                        {
                            listaBienesResult.Add(cDatosBienActa);
                        }
                        else
                        {
                            List<CDatosBienesActa> listaBienes = new List<CDatosBienesActa>();
                            listaBienes.Add(cDatosBienActa);
                            resultadoDatos.Add(datosResguardante, listaBienes);
                        }

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
            return resultadoDatos;

        }

        public static List<CActa> ConsultarActas(String numActa, String numInventario, String fechaIni, String fechaFin)
        {
            List<CActa> actas = new List<CActa>();
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            try
            {
                cnn.Open();

                SqlCommand cmd = generarObjectoSQLConsultaActa(numActa,numInventario,fechaIni,fechaFin,cnn);

                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        CActa acta = new CActa();
                        acta.idActa = int.Parse(rd["IdActa"].ToString());
                        acta.strNumResguardo = rd["IdResguardo"].ToString();
                        acta.strNumActa = rd["NumActa"].ToString();
                        acta.strNombreBien = rd["DescripcionBien"].ToString();
                        acta.strNombreResguardante = rd["Nombre"].ToString();
                        acta.strUniAdmin = rd["UniAdmin"].ToString();
                        acta.strFechaActa = rd["FechaAlta"].ToString();
                        acta.strNumInventario = rd["NumInventario"].ToString();
                        acta.strMarca = rd["Marca"].ToString();
                        acta.strModelo = rd["Modelo"].ToString();
                        acta.strSerie = rd["Serie"].ToString();
                        acta.strStatus = rd["status"].ToString();
                        acta.strDescripcionActa = rd["Descripcion"].ToString();
                        acta.strFechaCancela = rd["FechaCancela"].ToString(); 

                        actas.Add(acta);
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
            return actas;
        }

        public static String generarQueryConsultaActa(String numActa, String numInventario, String fechaIni, String fechaFin)
        {
            String query = "SELECT NumActa, Nombre, UniAdmin, CONVERT(varchar,FechaAlta,103) as FechaAlta, NumInventario, DescripcionBien, Marca, Modelo, Serie, IdResguardo, IdActa, status, Descripcion, FechaCancela FROM Vta_ConsultarActas WHERE";

            if (!String.IsNullOrWhiteSpace(numActa))
            {
                query += " NumActa = @numActa AND";
            }
            if (!String.IsNullOrWhiteSpace(numInventario))
            {
                query += " NumInventario = @numInventario AND";
            }
            if (!String.IsNullOrWhiteSpace(fechaIni) && !String.IsNullOrWhiteSpace(fechaFin))
            {
                query += " FechaAlta between @fechaIni AND @fechaFin";
            }
            if (query.EndsWith("D"))
            {
                query = query.Substring(0, query.Length - 4);
            }

            return query;
        }

        public static SqlCommand generarObjectoSQLConsultaActa(String numActa, String numInventario, String fechaIni, String fechaFin, SqlConnection cnn)
        {
            SqlCommand cmd = new SqlCommand(generarQueryConsultaActa(numActa, numInventario, fechaIni, fechaFin), cnn);

            if (!String.IsNullOrWhiteSpace(numActa))
            {
                cmd.Parameters.Add("@NumActa", SqlDbType.VarChar).Value = numActa;
            }
            if (!String.IsNullOrWhiteSpace(numInventario))
            {
                cmd.Parameters.Add("@NumInventario", SqlDbType.BigInt).Value = numInventario;
            }
            if (!String.IsNullOrWhiteSpace(fechaIni) && !String.IsNullOrWhiteSpace(fechaFin))
            {
                cmd.Parameters.Add("@fechaIni", SqlDbType.Date).Value = fechaIni;
                cmd.Parameters.Add("@fechaFin", SqlDbType.DateTime).Value = fechaFin + " 23:59:59.999";
            }
                                    
            return cmd;
        }

    }
}