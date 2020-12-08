using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventariosPJEH.CNegocios;
using System.Data.SqlClient;
using System.Data;




namespace InventariosPJEH.CAccesoDatos
{    
    public class BdHistoricoBien
    {
        private static string error;
        public static List<CHistoricoBien> MostrarHistorialBien(long NumInventario)
        {

            List<CHistoricoBien> lista = new List<CHistoricoBien>();
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
           

            try
            {

                cnn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM  Vta_HistoricoBien WHERE  NumInventario  = @Numero  ORDER BY Fecha DESC", cnn);

                cmd.Parameters.Add("@Numero", System.Data.SqlDbType.BigInt) .Value = NumInventario;
               
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        CHistoricoBien CHistorial = new CHistoricoBien();

                        CHistorial.NumInventario = BdConverter.FieldToInt64(rd["NumInventario"]);
                        CHistorial.DescripcionBien = rd["DescripcionBien"].ToString();                       
                        CHistorial.Fecha = BdConverter.FieldToDate(rd["Fecha"]);
                        CHistorial.Nombre = rd["Nombre"].ToString();
                        CHistorial.UsuarioMov = rd["UsuarioMov"].ToString();
                        CHistorial.Actividad = rd["Actividad"].ToString();
                               
                        lista.Add(CHistorial);
                    }
                }
            }
            catch (Exception e)

            {
                error = e.Message;
                cnn.Close();
            }
            return lista;
            
        }
    }
}