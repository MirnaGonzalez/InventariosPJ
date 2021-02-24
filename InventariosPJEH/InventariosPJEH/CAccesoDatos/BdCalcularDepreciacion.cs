using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using InventariosPJEH.CNegocios;
using Microsoft.Office.Interop.Excel;




namespace InventariosPJEH.CAccesoDatos
{
    public class BdCalcularDepreciacion
    {
        private static string error;
        public static List<CCalculoDepreciacion> MostraDepreciacion(DateTime FechaIT, DateTime FechaCT, int Status, string Tipo, string NoBajaDefinitiva)
        {
            
             List<CCalculoDepreciacion> lista = new List<CCalculoDepreciacion>();
             SqlConnection cnn = new SqlConnection(CConexion.Obtener());
             
            try
            {
                SqlCommand cmd;
                cnn.Open();
                if (NoBajaDefinitiva=="")
                {
                   cmd = new SqlCommand("stp_CalculoDepreciacion", cnn);

                }
                else
                {
                    cmd = new SqlCommand("stp_CalculoDepreciacionInactivos", cnn);
                    cmd.Parameters.Add("@OficioBaja", System.Data.SqlDbType.VarChar).Value = NoBajaDefinitiva;
                }

                cmd.Parameters.Add("@FechaIT", System.Data.SqlDbType.DateTime).Value = FechaIT;
                cmd.Parameters.Add("@FechaCT", System.Data.SqlDbType.DateTime).Value = FechaCT;
                cmd.Parameters.Add("@Status", System.Data.SqlDbType.Int).Value = Status;
                cmd.Parameters.Add("@Tipo", System.Data.SqlDbType.VarChar).Value = Tipo;

                cmd.CommandType = CommandType.StoredProcedure;

                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        CCalculoDepreciacion CCalculo = new CCalculoDepreciacion();

                        CCalculo.DescripcionBien = rd["DescripcionBien"].ToString();
                        CCalculo.NumInventario = BdConverter.FieldToInt64(rd["NumInventario"]);

                        CCalculo.MOI = Convert.ToDecimal(rd["Moi"]);
                        CCalculo.FechaAdquisicion = BdConverter.FieldToDate(rd["FechaAdquisicion"]);
                        CCalculo.Añocompra = BdConverter.FieldToInt(rd["Añocompra"]);
                        CCalculo.VidaUtil = BdConverter.FieldToFloat(rd["VidaUtil"]);
                        CCalculo.MontoDepreciar = Convert.ToDecimal(rd["MontoDepreciar"]);
                        CCalculo.FID = BdConverter.FieldToDate(rd["FID"]);
                        CCalculo.FFD = BdConverter.FieldToDate(rd["FFD"]);
                        CCalculo.Formula = BdConverter.FieldToFloat(rd["Formula"]);
                        CCalculo.Porcentaje = BdConverter.FieldToFloat(rd["Porcentaje"]);
                        CCalculo.TDias = BdConverter.FieldToInt(rd["TDias"]);
                        CCalculo.DiferenciaDias = BdConverter.FieldToInt(rd["DiferenciaDias"]);
                        CCalculo.FVI = BdConverter.FieldToInt(rd["FVI"]);
                        CCalculo.FVT = BdConverter.FieldToInt(rd["FVT"]);
                        CCalculo.SAC = BdConverter.FieldToInt(rd["SAC"]);
                        CCalculo.DepreciacionTrimestre = BdConverter.FieldToDouble(rd["DepreciacionTrimestre"]);
                        CCalculo.SaldoAcumulado = BdConverter.FieldToDouble(rd["SaldoAcumulado"]);
                        CCalculo.SaldoXDepreciar = BdConverter.FieldToDouble(rd["SaldoXDepreciar"]);
                        CCalculo.Grupo = BdConverter.FieldToInt(rd["Grupo"]);
                        CCalculo.SubGrupo = BdConverter.FieldToInt(rd["SubGrupo"]);
                        CCalculo.Clase = BdConverter.FieldToInt(rd["Clase"]);
                        CCalculo.IdSubClase = BdConverter.FieldToInt(rd["IdSubClase"]);
                        

                        lista.Add(CCalculo);
                    }
                }
            }
            catch (Exception e)

            {
                error = e.Message;
                cnn.Close();
            }
            return lista;
            //}

        }

        public static List<CReporteDepreciacion> ReporteDepreciacion(DateTime FechaIT, DateTime FechaCT, int Status, string Tipo, string NoBajaDefinitiva)
        {

            List<CReporteDepreciacion> lista = new List<CReporteDepreciacion>();
             SqlConnection cnn = new SqlConnection(CConexion.Obtener());

            try
            {
                SqlCommand cmd;
                cnn.Open();
                if (NoBajaDefinitiva == "")
                {
                    cmd = new SqlCommand("stp_CalculoReporteDepreciacion", cnn);

                }
                else
                {
                    cmd = new SqlCommand("stp_CalculoReporteDepreciacionInactivos", cnn);
                    cmd.Parameters.Add("@OficioBaja", System.Data.SqlDbType.VarChar).Value = NoBajaDefinitiva;
                }

                cmd.Parameters.Add("@FechaIT", System.Data.SqlDbType.DateTime).Value = FechaIT;
                cmd.Parameters.Add("@FechaCT", System.Data.SqlDbType.DateTime).Value = FechaCT;
                cmd.Parameters.Add("@Status", System.Data.SqlDbType.Int).Value = Status;
                cmd.Parameters.Add("@Tipo", System.Data.SqlDbType.VarChar).Value = Tipo;

                cmd.CommandType = CommandType.StoredProcedure;


                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        CReporteDepreciacion CCalculo = new CReporteDepreciacion();

                        CCalculo.Partida = rd["Partida"].ToString();
                        CCalculo.NumPartida = BdConverter.FieldToInt(rd["NumPartida"]);
                        CCalculo.DepreciacionTrimestre = BdConverter.FieldToFloat(rd["DepreciacionTrimestre"]);
                        CCalculo.SaldoAcumulado = BdConverter.FieldToFloat(rd["SaldoAcumulado"]);
                        CCalculo.SaldoXDepreciar = BdConverter.FieldToFloat(rd["SaldoXDepreciar"]);
                        CCalculo.AgrupaAnio = rd["AgrupaAnio"].ToString();

                        lista.Add(CCalculo);
                    }
                }
            }
            catch (Exception e)

            {
                error = e.Message;
                cnn.Close();
            }
            return lista;
            //}

        }

    }
}