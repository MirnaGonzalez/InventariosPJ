using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using InventariosPJEH.CNegocios;

namespace InventariosPJEH.CAccesoDatos
{
    public class BdCatINCP
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataTable Iniciar()
        {
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.CommandText = "select DISTINCT Anio from Cat_INPC ORDER BY Anio ASC";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAnio"></param>
        /// <returns></returns>
        public static List<CINCP> ConsultarGbINCP(string pAnio)
        {
            List<CINCP> lista = new List<CINCP>();
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            string cmdString = "";

            try
            {

                if (pAnio == "Seleccionar")
                {
                    cmdString = "Select IdINCP,Anio,IVA,NumUMAS,ValorUMA,INCP from Cat_INPC ORDER BY Anio ASC";
                }

                else
                {
                    cmdString = "Select IdINCP,Anio,IVA,NumUMAS,ValorUMA,INCP from Cat_INPC where Anio like '%{0}%' ORDER BY Anio ASC";
                }


                SqlCommand cmd = new SqlCommand(string.Format(cmdString, pAnio), cnn);
                            
                cnn.Open();

                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        CINCP incp = new CINCP();
                        incp.IdINCP = (rd["IdINCP"]).ToString();
                        incp.Anio = rd["Anio"].ToString();
                        incp.IVA = rd["IVA"].ToString().Replace(",", ".");
                        incp.NumUMAS = rd["NumUMAS"].ToString();
                        incp.ValorUMA = rd["ValorUMA"].ToString().Replace(",", ".");
                        incp.INCP = Convert.ToDecimal(rd["INCP"]).ToString("N2", new CultureInfo("en-US"));
                        lista.Add(incp);

                    }
                }
            }
            catch (Exception )
            {
                cnn.Close();
            }
            
            return lista;
        }

       
        public void Eliminar_INCP(CINCP obJE)
        {
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            cnn.Open();
            try
            {

                SqlCommand cmd = new SqlCommand("DELETE FROM Cat_INPC where IdINCP=@idINCP", cnn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@idINCP", obJE.IdINCP);

                cmd.ExecuteNonQuery();
            }
            catch (Exception )
            {
                cnn.Close();
            }
           
        }
    }
}