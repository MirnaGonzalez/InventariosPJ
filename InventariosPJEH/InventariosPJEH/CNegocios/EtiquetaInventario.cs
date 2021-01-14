using InventariosPJEH.CAccesoDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InventariosPJEH.CNegocios
{
    public class EtiquetaInventario
    {
        public Int64 NumInventario { get; set; }
        public string DescripcionBien { get; set; }
        public DateTime FechaAdquisicion { get; set; }
        public string Propiedad { get; set; }
        public string NumDocumento { get; set; }
        public string filtroBusqueda { get; set; }
        public int NoResguardo { get; set; }
        public int IdUnidad { get; set; }




        public static List<EtiquetaInventario> etiquetaInventarios(EtiquetaInventario etiquetaInventario)
        {
            SqlConnection cnn = new SqlConnection(Conexion.Obtener());
            SqlCommand cmd = new SqlCommand();
            SqlDataReader rdr = null;
            string qry;


            List<EtiquetaInventario> lista = new List<EtiquetaInventario>();

            if (etiquetaInventario.filtroBusqueda == "documento")
            {
                qry = "  SELECT [NumInventario] , [DescripcionBien] , [FechaAdquisicion] , [Propiedad]  ";
                qry += " FROM   [Vta_EtiquetaInventario] ";
                qry += " WHERE NumDocumento=@NumDocumento ";


                cmd.Connection = cnn;
                cmd.CommandText = qry;

                cmd.Parameters.Add("@NumDocumento", System.Data.SqlDbType.NVarChar).Value = etiquetaInventario.NumDocumento;

            }
         

            else if (etiquetaInventario.filtroBusqueda == "inventario")
            {
                qry = "  SELECT [NumInventario] , [DescripcionBien] , [FechaAdquisicion] , [Propiedad] ";
                qry += " FROM   [Vta_EtiquetaInventario] ";
                qry += " WHERE NumInventario=@NumInventario ";


                cmd.Connection = cnn;
                cmd.CommandText = qry;

                cmd.Parameters.Add("@NumInventario", System.Data.SqlDbType.NVarChar).Value = etiquetaInventario.NumInventario;

            }
            else if (etiquetaInventario.filtroBusqueda == "resguardo")
            {
                qry = "  SELECT [NumInventario] , [DescripcionBien] , [FechaAdquisicion] , [Propiedad]  ";
                qry += " FROM   [EtiquetaResguardo] ";
                qry += " WHERE IdResguardo=@IdResguardo ";


                cmd.Connection = cnn;
                cmd.CommandText = qry;

                cmd.Parameters.Add("@IdResguardo", System.Data.SqlDbType.Int).Value = etiquetaInventario.NoResguardo;

            }
            else if (etiquetaInventario.filtroBusqueda == "area")
            {
                qry = "  SELECT [NumInventario] , [DescripcionBien] , [FechaAdquisicion] , [Propiedad] ";
                qry += " FROM   [EtiquetaResguardo] ";
                qry += " WHERE IdUniAdmin=@IdUniAdmin ";


                cmd.Connection = cnn;
                cmd.CommandText = qry;

                cmd.Parameters.Add("@IdUniAdmin", System.Data.SqlDbType.Int).Value = etiquetaInventario.IdUnidad;

            }




            try
            {
                cnn.Open();
                rdr = cmd.ExecuteReader();
            }

            catch (Exception ex)
            {
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
               
                return lista;
            }

            try
            {
                while (rdr.Read())
                {

                    EtiquetaInventario etiqueta = new EtiquetaInventario();
                    etiqueta.NumInventario = rdr.GetInt64(0);
                    etiqueta.DescripcionBien = rdr.GetString(1);
                    etiqueta.FechaAdquisicion = rdr.GetDateTime(2);
                    etiqueta.Propiedad = rdr.GetString(3);
                                       lista.Add(etiqueta);
                }

                cnn.Close();

                  
                    return lista;
                
            }
            catch (Exception ex)
            {
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
              
                return lista;

            }





        }





    }

}