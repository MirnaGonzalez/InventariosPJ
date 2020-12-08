using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using InventariosPJEH.CNegocios;
using InventariosPJEH.CAccesoDatos;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Linq;

namespace InventariosPJEH.CAccesoDatos
{
    /// <summary>
	/// Summary description for Class1
	/// </summary>
    public class BdRecibirBien
    {
        public SqlConnection conexion;
        public string error;
        protected SqlDataAdapter adaptador;
        protected SqlDataReader reader;
        protected DataSet data;

        /// <summary>
        /// Lista todos los bienes a reparar relacionados con el numero de inventario
        /// </summary>
        /// <param name="nNumeroInventario"></param>
        /// <returns></returns>  
        public static List<CRecibirBien> ConsultarGbBienesReparar(string nNumeroInventario)
        {
            List<CRecibirBien> lista = new List<CRecibirBien>();
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            try
            {
                SqlCommand cmd = new SqlCommand(string.Format("select IdRegMant,IdInventario,NumInventario,DescripcionBien, Marca, Modelo,Serie, NombrePersona, IdActividad, IdENSU, IdUsuario " +
                "from VistaConsultarBienes where NumInventario like '%{0}%'  and IdActividad=2 ORDER BY Serie ASC", nNumeroInventario), cnn);

                cnn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        CRecibirBien crecibir = new CRecibirBien();
                        crecibir.IdRegMant = rd["IdRegMant"].ToString();
                        crecibir.IdInventario = rd["IdInventario"].ToString();
                        crecibir.NumInventario = rd["NumInventario"].ToString();
                        crecibir.DescripcionBien = rd["DescripcionBien"].ToString();
                        crecibir.Marca = rd["Marca"].ToString();
                        crecibir.Modelo = rd["Modelo"].ToString();
                        crecibir.Serie = rd["Serie"].ToString();
                        crecibir.NombrePersona = rd["NombrePersona"].ToString();
                        crecibir.IdActividad = rd["IdActividad"].ToString();
                        crecibir.IdENSU = rd["IdENSU"].ToString();
                        crecibir.IdUsuario = rd["IdUsuario"].ToString();

                        lista.Add(crecibir);
                    }
                }
            }
            catch (Exception )
            {
                cnn.Close();
            }
            return lista;
        }
        public static List<CRecibirBien> ConsultarGbBienesReparar1(string nNumeroSerie)
        {
            List<CRecibirBien> lista = new List<CRecibirBien>();
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());

            try
            {
                SqlCommand cmd = new SqlCommand(string.Format("select IdRegMant,IdInventario, NumInventario, DescripcionBien, Marca, Modelo, Serie, NombrePersona, IdActividad, IdENSU, IdUsuario" +
                " from VistaConsultarBienes where serie like '%{0}%' and IdActividad=2 ORDER BY serie ASC", nNumeroSerie), cnn);

                cnn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        CRecibirBien crecibir = new CRecibirBien();
                        crecibir.IdRegMant = rd["IdRegMant"].ToString();
                        crecibir.IdInventario = rd["IdInventario"].ToString();
                        crecibir.NumInventario = rd["NumInventario"].ToString();
                        crecibir.DescripcionBien = rd["DescripcionBien"].ToString();
                        crecibir.Marca = rd["Marca"].ToString();
                        crecibir.Modelo = rd["Modelo"].ToString();
                        crecibir.Serie = rd["Serie"].ToString();
                        crecibir.NombrePersona = rd["NombrePersona"].ToString();
                        crecibir.IdActividad = rd["IdActividad"].ToString();
                        crecibir.IdENSU = rd["IdENSU"].ToString();
                        crecibir.IdUsuario = rd["IdUsuario"].ToString();

                        lista.Add(crecibir);
                    }
                }
            }
            catch (Exception )
            {
                cnn.Close();
            }
            return lista;
        }

        
    }
}
